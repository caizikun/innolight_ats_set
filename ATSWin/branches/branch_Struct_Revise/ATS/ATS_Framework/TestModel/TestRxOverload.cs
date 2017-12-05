using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using Ivi.Visa.Interop;
namespace ATS_Framework
{
    public class TestRxOverload : TestModelBase
    {
#region Attribute     
        private TestRxOverloadStruct testRxOverloadStruct = new TestRxOverloadStruct();
        
        private ArrayList inPutParametersNameArray = new ArrayList();
  
        private double OverloadPoint;
        private double OverloadOMA;
        
        private ArrayList RxPowerArray = new ArrayList();
        private ArrayList BerArray = new ArrayList();
        private ArrayList OSERValueArray = new ArrayList();
        //equipments
       private Attennuator tempAtten;
       private ErrorDetector tempED;
       private Powersupply tempps;
#endregion

#region Method
       public TestRxOverload(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;

            OSERValueArray.Clear();
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("LoopTime");
            inPutParametersNameArray.Add("AttStep");
            inPutParametersNameArray.Add("GatingTime");
            //inPutParametersNameArray.Add("IsOptSourceUnitOMA");
            //inPutParametersNameArray.Add("SpecDelta");
            inPutParametersNameArray.Add("CsenAlignRxPwr");                     
        }
        override protected bool CheckEquipmentReadiness()
        {
            //check if all equipments are ready for this test; 
            //increase equipment referenced_times if ready
            //for (int i = 0; i < pEquipList.Count; i++)
            //    if (!pEquipList.Values[i].bReady) return false;

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

        protected override bool StartTest()
        {
            RxPowerArray.Clear();
            BerArray.Clear();
            
            logoStr = "";
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }

            //if (testRxOverloadStruct.IsOptSourceUnitOMA)
            //{
            //    testRxOverloadStruct.SpecDelta = Algorithm.CalculateFromOMAtoDBM(testRxOverloadStruct.SpecDelta, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
            //    testRxOverloadStruct.SpecDelta = Math.Round(testRxOverloadStruct.SpecDelta, 4);          
            //    testRxOverloadStruct.CsenAlignRxPwr = Algorithm.CalculateFromOMAtoDBM(testRxOverloadStruct.CsenAlignRxPwr, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
            //    testRxOverloadStruct.CsenAlignRxPwr = Math.Round(testRxOverloadStruct.CsenAlignRxPwr, 4);
            //}
            
            if (tempAtten != null && tempED != null && tempps != null)
            {                
                // open apc              
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                }               
                // open apc

                //tempAtten.SetAllChannnel_RxOverLoad(10);
                Log.SaveLogToTxt("Step2...SetAttenValue");
                tempAtten.AttnValue(testRxOverloadStruct.CsenAlignRxPwr.ToString());
                Log.SaveLogToTxt("Step3...AutoAlaign");
                bool isAutoAlaign = tempED.AutoAlaign(true);
                if (isAutoAlaign)
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                    Log.SaveLogToTxt("Step4...StartTestRxOverload");

                    double ber = -1;
                    int i = 0;
                    int LoopCount;
                    double RxPower = 0;
                    double countMol = testRxOverloadStruct.LoopTime % testRxOverloadStruct.GatingTime;
                    if (countMol == 0)
                    {
                        LoopCount = Convert.ToInt32(testRxOverloadStruct.LoopTime / testRxOverloadStruct.GatingTime);
                    }
                    else
                    {
                        LoopCount = Convert.ToInt32((testRxOverloadStruct.LoopTime- countMol) / testRxOverloadStruct.GatingTime) + 1;               
                    }

                    double startPower = 0;
                    if (tempAtten.offsetlist.ContainsKey(tempAtten.CurrentChannel))
                    {
                        startPower = Convert.ToDouble(tempAtten.offsetlist[tempAtten.CurrentChannel]);
                    }
                    
                    do
                    {
                        RxPower = startPower - testRxOverloadStruct.AttStep * i;

                        if (RxPower < testRxOverloadStruct.CsenAlignRxPwr)
                        {
                            OverloadPoint = testRxOverloadStruct.CsenAlignRxPwr;
                            Log.SaveLogToTxt("Can't find OverloadPoint...");
                            break;
                        }
                        else
                        {
                            tempAtten.AttnValue(RxPower.ToString());
                            RxPowerArray.Add(RxPower);
                            Log.SaveLogToTxt("SetAtten=" + RxPower.ToString());
                           
                            tempED.EdGatingStart();    //刷新误码数
                            for (int j = 0; j < LoopCount; j++)
                            {                               
                                Thread.Sleep(Convert.ToInt32(testRxOverloadStruct.GatingTime * 1000));
                                ber = tempED.QureyEdErrorRatio();                                
                                Log.SaveLogToTxt("Ber=" + ber.ToString());
                                if (ber != 0)
                                {
                                    BerArray.Add(ber);
                                    break;
                                }
                                else
                                {
                                    if( j == LoopCount -1 )
                                    {
                                       BerArray.Add(ber);
                                    }
                                }
                            }
                        }

                        i++;
                    }
                    while (ber != 0);


                    if (ber == 0 && i != 1)
                    {
                        i = 0;
                        startPower = RxPower + testRxOverloadStruct.AttStep;
                        do
                        {
                            RxPower = startPower - 0.2 * i;

                            if (RxPower < testRxOverloadStruct.CsenAlignRxPwr)
                            {
                                OverloadPoint = testRxOverloadStruct.CsenAlignRxPwr;
                                Log.SaveLogToTxt("Can't find OverloadPoint...");
                                break;
                            }
                            else
                            {
                                tempAtten.AttnValue(RxPower.ToString());
                                RxPowerArray.Add(RxPower);
                                Log.SaveLogToTxt("SetAtten=" + RxPower.ToString());

                                tempED.EdGatingStart();    //刷新误码数
                                for (int j = 0; j < LoopCount; j++)
                                {
                                    Thread.Sleep(Convert.ToInt32(testRxOverloadStruct.GatingTime * 1000));
                                    ber = tempED.QureyEdErrorRatio();
                                    Log.SaveLogToTxt("Ber=" + ber.ToString());
                                    if (ber != 0)
                                    {
                                        BerArray.Add(ber);
                                        break;
                                    }
                                    else
                                    {
                                        if (j == LoopCount - 1)
                                        {
                                            BerArray.Add(ber);
                                        }
                                    }
                                }
                            }

                            i++;
                        }
                        while (ber != 0);


                        if (ber == 0)
                        {
                            OverloadPoint = RxPower;
                            Log.SaveLogToTxt("OverloadPoint= " + OverloadPoint.ToString());
                        }
                    }
                    else          //光源点无误码
                    {
                        OverloadPoint = RxPower;
                        Log.SaveLogToTxt("OverloadPoint= " + OverloadPoint.ToString() + " LightSource= " + OverloadPoint.ToString());
                    }

                    OutPutandFlushLog();
                    return true;                   
                } 
                else
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                    OverloadPoint = -1000;
                    OutPutandFlushLog();
                    return isAutoAlaign;
                }
            }
            else
            {
                Log.SaveLogToTxt("Equipments are not enough!");
                AnalysisOutputProcData(procData);
                AnalysisOutputParameters(outputParameters);
                               
                return false;
            }
        }

        override protected bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            bool flag = DeassembleEquipment();

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].DecreaseReferencedTimes();

            }
            return flag;
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
                return false;
            }
            else
            {
                bool isOK = false;
                selectedEquipList.Clear();
                IList<string> tempKeys = aEquipList.Keys;
                IList<EquipmentBase> tempValues = aEquipList.Values;
                for (byte i = 0; i < aEquipList.Count; i++)
                {
                    if (tempKeys[i].ToUpper().Contains("ERRORDETE"))
                   
                    {
                        selectedEquipList.Add("ERRORDETE", tempValues[i]);

                    }
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                 
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(2, GlobalParameters.CurrentChannel);
                    }
                }
                 tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                 tempED = (ErrorDetector)selectedEquipList["ERRORDETE"];
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempAtten != null && tempED != null && tempps != null)
                {
                    isOK = true;

                }
                else
                {
                    if (tempED == null)
                    {
                        Log.SaveLogToTxt("ERRORDETE =NULL");
                    }
                    if (tempAtten == null)
                    {
                        Log.SaveLogToTxt("ATTEN =NULL");
                    }
                    if (tempps == null)
                    {
                        Log.SaveLogToTxt("POWERSUPPLY =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
                    isOK = false;
                }
                if (isOK)
                {

                    selectedEquipList.Add("DUT", dut);
                }
                else
                {
                    isOK = false;
                }
                return isOK;
            }
        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[2];
                outputParameters[0].FiledName = "Overload(dbm)";
                OverloadPoint = Algorithm.ISNaNorIfinity(OverloadPoint);
                outputParameters[0].DefaultValue = Math.Round(OverloadPoint, 4).ToString().Trim();

                outputParameters[1].FiledName = "OverloadOMA(dbm)";
                OverloadOMA = Algorithm.CalculateOMA(OverloadPoint, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                outputParameters[1].DefaultValue = Math.Round(OverloadOMA, 4).ToString().Trim();
                return true;
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F04, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02F04, error.StackTrace); 
            }
        }
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            Log.SaveLogToTxt("Step1...Check InputParameters");

            if (GlobalParameters.OpticalSourseERArray == null || GlobalParameters.OpticalSourseERArray == "")
            {
                return false;
            }
            else
            {
                char[] tempCharArray = new char[] { ',' };
                OSERValueArray = Algorithm.StringtoArraylistDeletePunctuations(GlobalParameters.OpticalSourseERArray, tempCharArray);
            }
            
            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                Log.SaveLogToTxt("InputParameters are not enough!");
                return false;
            }
            else
            {
                int index = -1;
                bool isParametersComplete = true;

                if (isParametersComplete)
                {
                    if (Algorithm.FindFileName(InformationList, "LoopTime", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxOverloadStruct.LoopTime = temp;
                        }
                    }
                    if (Algorithm.FindFileName(InformationList, "AttStep", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxOverloadStruct.AttStep = temp;
                        }
                    }
                    if (Algorithm.FindFileName(InformationList, "GatingTime", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxOverloadStruct.GatingTime = temp;
                        }
                    }
                    //if (Algorithm.FindFileName(InformationList, "IsOptSourceUnitOMA", out index))
                    //{
                    //    string temp = InformationList[index].DefaultValue;
                    //    if (temp.Trim().ToUpper() == "0" || temp.Trim().ToUpper() == "FALSE")
                    //    {
                    //        testRxOverloadStruct.IsOptSourceUnitOMA = false;
                    //    }
                    //    else
                    //    {
                    //        testRxOverloadStruct.IsOptSourceUnitOMA = true;
                    //    }
                    //}
                    //if (Algorithm.FindFileName(InformationList, "SpecDelta", out index))
                    //{
                    //    double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                    //    if (double.IsInfinity(temp) || double.IsNaN(temp))
                    //    {
                    //        Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                    //        return false;
                    //    }
                    //    else
                    //    {
                    //        testRxOverloadStruct.SpecDelta = temp;
                    //    }

                    //}
                    if (Algorithm.FindFileName(InformationList, "CsenAlignRxPwr", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxOverloadStruct.CsenAlignRxPwr = temp;
                        }
                    }
                }
                Log.SaveLogToTxt("OK!");
                return true;
            }


        }
       
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                procData = new TestModeEquipmentParameters[2];
                procData[0].FiledName = "RxPowerArray";
                procData[0].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(RxPowerArray, ",");
                procData[1].FiledName = "BerArray";
                procData[1].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(BerArray, ",");
                return true;

            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F05, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02F05, error.StackTrace); 
            }
        }
    
        protected void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
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
