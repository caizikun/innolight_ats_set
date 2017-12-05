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
       public AdjustWavelength(DUT inPuDut)
        {
          
            
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
                        Log.SaveLogToTxt("POWERSUPPLY =NULL");
                    }
                    if (pWavemeter == null)
                    {
                        Log.SaveLogToTxt("pWavemeter =NULL");
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

                Log.SaveLogToTxt("CurrentWavelength is " + CurrentWavelength);            
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
                   Log.SaveLogToTxt("ScanDacArray[" + i.ToString() + "]=" + ScanDacArray[i].ToString() + " " + "ScanWavelengthArray[" + i.ToString() + "]=" + ScanWavelengthArray[i].ToString());   
                }

                BestDac = Convert.ToInt32(CalculateBestPoint(MyStruct.WavelengthTypcalValue, ScanDacArray, ScanWavelengthArray));
              
                Log.SaveLogToTxt("CalculateBestPoint=" + BestDac);   

                if (BestDac>=MyStruct.WavelengthDacMax)
                {
                    BestDac = MyStruct.WavelengthDacMax;
                }
                else if (BestDac<=MyStruct.WavelengthDacMin)
                {
                    BestDac = MyStruct.WavelengthDacMin;
                }

                Log.SaveLogToTxt("BestDac=" + BestDac);   
             
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

            Algorithm.LinearRegression(X, Y, out slope, out intercept);

            BestDacPoint = slope + (intercept * TypcalValue);

            return BestDacPoint;
        }
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
           
            Log.SaveLogToTxt("Step1...Check InputParameters");
           
            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                Log.SaveLogToTxt("InputParameters are not enough!");
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

                        if (!Algorithm.Getconf(InformationList, "WavelengthDacMax",true, out StrTemp))
                        {
                            Log.SaveLogToTxt("WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthDacMax = Int32.Parse(StrTemp);
                        }

                        if (!Algorithm.Getconf(InformationList, "WavelengthDacMin", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthDacMin = Int32.Parse(StrTemp);
                        }

                        if (!Algorithm.Getconf(InformationList, "WavelengthDacStart", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthDacStart = Int32.Parse(StrTemp);
                        }

                        if (!Algorithm.Getconf(InformationList, "WavelengthTargetMax", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthTargetMax = double.Parse(StrTemp);
                        }

                        if (!Algorithm.Getconf(InformationList, "WavelengthTargetMin", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("WavelengthDacMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.WavelengthTargetMin = double.Parse(StrTemp);
                        } 
                    }

                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        }

   
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            

                return true;




        }

        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputProcData(procData);
                
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02FFF, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02FFF, error.StackTrace); 
            }
        }

        public override List<InnoExCeption> GetException()
        {
            return base.GetException();
        }
#endregion
    }
}
