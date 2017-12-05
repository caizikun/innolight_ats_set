using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
namespace ATS_Framework
{
    public class AdjustWavelength: TestModelBase
    {
#region Attribute
       private CalWavelengthStruct MyStruct = new CalWavelengthStruct();

       private Int32 TargetWavelengthDac;
       private Int32 BestDac;

        private ArrayList inPutParametersNameArray = new ArrayList();
      
       //equipments
       private Powersupply pPs;
       private Spectrograph pWavemeter;
       private double[] ScanDacArray;
       private double[] ScanWavelengthArray;

#endregion
#region Method
       public AdjustWavelength(DUT inPuDut, logManager logmanager)
        {
          
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;   
        
           inPutParametersNameArray.Clear();

           inPutParametersNameArray.Add("WavelengthDacMax");
           inPutParametersNameArray.Add("WavelengthDacMin");
           inPutParametersNameArray.Add("WavelengthDacStep");
           inPutParametersNameArray.Add("WavelengthDacStart");
           inPutParametersNameArray.Add("WavelengthTargetMax");
           inPutParametersNameArray.Add("WavelengthTargetMin");

          

   
        }
        override protected bool CheckEquipmentReadiness()
        {
           
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].bReady) return false;

            }

            return true;
        }
       override protected bool PrepareTest()
        {//note: for inherited class, they need to do its own preparation process task,
            //then call this base function
            //for (int i = 0; i < pEquipList.Count; i++)
            ////pEquipList.Values[i].IncreaseReferencedTimes();
            //{

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].IncreaseReferencedTimes();
            }

            return AssembleEquipment();
        }
       protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].Configure()) return false;

            }

            return true;
        }
        protected override bool AssembleEquipment()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
            }
            return true;
        }
        public override bool SelectEquipment(EquipmentList aEquipList)
        {
            selectedEquipList.Clear();
            if (aEquipList.Count == 0)
            {
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;
              
             
                for (byte i = 0; i < aEquipList.Count; i++)
                {

                    if (aEquipList.Keys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        pPs = (Powersupply)aEquipList["POWERSUPPLY"];
                      isOK = true;
                    }
                    if (aEquipList.Keys[i].ToUpper().Contains("SPECTROGRAPH"))
                    {
                        pWavemeter = (Spectrograph)aEquipList["WAVEMETER"];
                       
                    }
                    if (aEquipList.Keys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                       
                        aEquipList.Values[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }

                if (pPs != null && pWavemeter!=null)
                {
                    isOK = true;
                }
                else
                {
                    if (pPs == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    if (pWavemeter == null)
                    {
                        logoStr += logger.AdapterLogString(3, "pWavemeter =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
                }
                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);                  
                }

                return isOK;
            }
        }

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";

         
            
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }
            


            return true;
        }

       public override bool  Test()

        {

            dut.WriteWavelengthDac(MyStruct.WavelengthDacStart);

            AdjustWavelengthMethod_1();

            return true;
        }


        protected bool AdjustWavelengthMethod_1()
        {
          

            Int32 StartDac = MyStruct.WavelengthDacStart;

            dut.WriteWavelengthDac(MyStruct.WavelengthDacStart);
            double CurrentWavelength = pWavemeter.GetCenterWavelength();
            TargetWavelengthDac = MyStruct.WavelengthDacStart;

            if (CurrentWavelength>MyStruct.WavelengthTargetMin&&CurrentWavelength<MyStruct.WavelengthTargetMax)
            {

                logoStr += logger.AdapterLogString(4, "CurrentWavelength is " + CurrentWavelength);            
                return true;
            }
            else
            {

              

               int DataCount = (MyStruct.WavelengthDacMax - MyStruct.WavelengthDacMin) / MyStruct.WavelengthDacStep;
               ScanDacArray = new double[DataCount];
               ScanWavelengthArray = new double[DataCount];

                for (int i = 0; i < DataCount;i++ )
                {
                    ScanDacArray[i] = MyStruct.WavelengthDacMin + MyStruct.WavelengthDacStep * i;
                    dut.WriteWavelengthDac(ScanDacArray[i]);
                    Thread.Sleep(200);
                   ScanWavelengthArray[i] =pWavemeter.GetCenterWavelength();
                   logoStr += logger.AdapterLogString(1, "ScanDacArray[" + i.ToString() + "]=" + ScanDacArray[i].ToString() + " " + "ScanWavelengthArray[" + i.ToString() + "]=" + ScanWavelengthArray[i].ToString());   
                }

                BestDac = Convert.ToInt32(CalculateBestPoint(MyStruct.WavelengthTypcalValue, ScanDacArray, ScanWavelengthArray));
              
                logoStr += logger.AdapterLogString(1, "CalculateBestPoint=" + BestDac);   

                if (BestDac>=MyStruct.WavelengthDacMax)
                {
                    BestDac = MyStruct.WavelengthDacMax;
                }
                else if (BestDac<=MyStruct.WavelengthDacMin)
                {
                    BestDac = MyStruct.WavelengthDacMin;
                }

                logoStr += logger.AdapterLogString(1, "BestDac=" + BestDac);   
             
                dut.WriteWavelengthDac(BestDac);
                 return true;
            }
         
        }

        private  double CalculateBestPoint(double TypcalValue, double[] X, double[] Y)
        {
            bool flag=false;
            int i=1;

            double BestDacPoint;

            while (!flag&&i<Y.Length) 
            {
                if (TypcalValue>=Y[i-1]&&TypcalValue<=Y[i])
                {    
                    flag=true;
                    break;
                }
                i++;
            }

            double slope=0, intercept=0;

            algorithm.LinearRegression(X, Y, out slope, out intercept);

            BestDacPoint = slope + (intercept * TypcalValue);

            return BestDacPoint;
        }
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
           
            logoStr += logger.AdapterLogString(0,"Step1...Check InputParameters");
           
            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                logoStr += logger.AdapterLogString(4, "InputParameters are not enough!");
                return false;
            }
            else
            {
                //int index = -1;
                bool isParametersComplete = true;
              
                if (isParametersComplete)
                {
                    //for (byte i = 0; i < InformationList.Length; i++)
                    {
                        //inPutParametersNameArray.Add("WavelengthDacMax");
                        //inPutParametersNameArray.Add("WavelengthDacMin");
                        //inPutParametersNameArray.Add("WavelengthDacStep");
                        //inPutParametersNameArray.Add("WavelengthDacStart");
                        //inPutParametersNameArray.Add("WavelengthTargetMax");
                        //inPutParametersNameArray.Add("WavelengthTargetMin");

                        string StrTemp;

                        if (!algorithm.Getconf(InformationList, "WavelengthDacMax",true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthDacMax = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "WavelengthDacMin", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthDacMin = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "WavelengthDacStart", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthDacStart = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "WavelengthTargetMax", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthTargetMax = double.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "WavelengthTargetMin", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "WavelengthDacMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthTargetMin = double.Parse(StrTemp);
                        } 
                    }

                }
                logoStr += logger.AdapterLogString(1,"OK!");
                return true;
            }
        }

   
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
              
            

                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }



        }

        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputProcData(procData);
                logger.FlushLogBuffer();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
#endregion
    }
}
