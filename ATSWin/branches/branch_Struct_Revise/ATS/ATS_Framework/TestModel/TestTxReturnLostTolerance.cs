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
    public class TestTxReturnLostTolerance : TestModelBase
    {
#region Attribute     
        private TestTxReturnLostToleranceStruct testTxReturnLostToleranceStruct = new TestTxReturnLostToleranceStruct();
        
        private ArrayList inPutParametersNameArray = new ArrayList();
  
        private double NoneBerPoint;
        private double StartTxPwr;
        private double TxReturnLosTolerance;
        
        private ArrayList RxPowerArray = new ArrayList();
        private ArrayList BerArrayRX = new ArrayList();
        private ArrayList TxPowerArray = new ArrayList();
        private ArrayList BerArrayTX = new ArrayList();
        //equipments
       private Attennuator tempAttenRX;
       private Attennuator tempAttenTX;
       private PowerMeter tempPowerMeter;
       private ErrorDetector tempED;
       private PPG tempPPG;
       private Powersupply tempps;
#endregion

#region Method
       public TestTxReturnLostTolerance(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;

            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("TargetPower");
            inPutParametersNameArray.Add("ReturnLosTolerancePRBS");
            inPutParametersNameArray.Add("RXAttStep");
            inPutParametersNameArray.Add("TXAttStep");
            inPutParametersNameArray.Add("CsenAlignRxPwr");
            inPutParametersNameArray.Add("StartRxPwr");
            inPutParametersNameArray.Add("LoopTime");
            inPutParametersNameArray.Add("GatingTime");                   
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
            BerArrayRX.Clear();
            TxPowerArray.Clear();
            BerArrayTX.Clear();

            
            logoStr = "";
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }

            if (tempED != null && tempAttenRX != null && tempAttenTX != null && tempPowerMeter != null && tempps != null)
            {
                if (!CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON))) return false;//开启APC

                if (!tempPPG.ConfigurePrbsLength(Convert.ToByte(testTxReturnLostToleranceStruct.ReturnLosTolerancePRBS))) return false; //Bert码型为PRBS31

                //if (!SetTargetPower(testTxReturnLostToleranceStruct.TargetPower)) return false; //调节反射器处的衰减器，使光功率计值降TargetPower以下

                tempAttenTX.OutPutSwitch(false);

                Log.SaveLogToTxt("Step2...SetAttenValue");
                tempAttenRX.AttnValue(testTxReturnLostToleranceStruct.CsenAlignRxPwr.ToString());
                Log.SaveLogToTxt("Step3...AutoAlaign");

                bool isAutoAlaign = tempED.AutoAlaign(true);
                if (isAutoAlaign)
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                    Log.SaveLogToTxt("Step4...TestTxReturnLostTolerance");

                    double ber = -1;
                    int i = 0;
                    double RxPower = 0;
                    int LoopCount=0;
                    double countMol = testTxReturnLostToleranceStruct.LoopTime % testTxReturnLostToleranceStruct.GatingTime;
                    if (countMol == 0)
                    {
                        LoopCount = Convert.ToInt32(testTxReturnLostToleranceStruct.LoopTime / testTxReturnLostToleranceStruct.GatingTime);
                    }
                    else
                    {
                        LoopCount = Convert.ToInt32((testTxReturnLostToleranceStruct.LoopTime - countMol) / testTxReturnLostToleranceStruct.GatingTime) + 1;               
                    }

                    do
                    {
                        RxPower = testTxReturnLostToleranceStruct.StartRxPwr + testTxReturnLostToleranceStruct.RXAttStep * i;

                        tempAttenRX.AttnValue(RxPower.ToString());
                        RxPowerArray.Add(RxPower);
                        Log.SaveLogToTxt("SetAttenRX=" + RxPower.ToString());
                           
                        tempED.EdGatingStart();    //刷新误码数
                        for (int j = 0; j < LoopCount; j++)
                        {
                            Thread.Sleep(Convert.ToInt32(testTxReturnLostToleranceStruct.GatingTime * 1000));
                            ber = tempED.QureyEdErrorRatio();                                
                            Log.SaveLogToTxt("BerRX=" + ber.ToString());
                            if (ber != 0)
                            {
                                BerArrayRX.Add(ber);
                                break;
                            }
                            else
                            {
                                if( j == LoopCount -1 )
                                {
                                    BerArrayRX.Add(ber);
                                }
                            }
                        }
                        
                        i++;
                    }
                    while (ber != 0);

                    if(ber == 0)
                    {
                        NoneBerPoint = RxPower;
                        //tempAttenRX.AttnValue(NoneBerPoint.ToString());
                        Log.SaveLogToTxt("NoneBerPoint= " + NoneBerPoint.ToString());
                    }

                    //tempED.EdGatingStart();    //刷新误码数

                    double value = 11.5;

                    do
                    {
                        StartTxPwr = dut.ReadDmiTxp() - value;
                        if (!this.SetTargetPower(StartTxPwr))
                        {
                            return false;
                        }

                        tempED.EdGatingStart();    //刷新误码数
                        for (int j = 0; j < LoopCount; j++)
                        {
                            Thread.Sleep(Convert.ToInt32(testTxReturnLostToleranceStruct.GatingTime * 1000));
                            ber = tempED.QureyEdErrorRatio();
                            Log.SaveLogToTxt("BerRX=" + ber.ToString());
                            if (ber != 0)
                            {
                                break;
                            }                            
                        }
                        
                        if(ber == 0)
                        {
                            TxReturnLosTolerance = tempPowerMeter.ReadPower();
                            Log.SaveLogToTxt("TxReturnLosTolerance= " + TxReturnLosTolerance.ToString());
                            break;
                        }
                        //tempAttenTX.SetAttnValue(40);                        
                        value += 0.5;
                    } while (value< 13);

                    TxReturnLosTolerance = value;

                    OutPutandFlushLog();
                    return true;                   
                } 
                else
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                    TxReturnLosTolerance = -1000;
                    OutPutandFlushLog();
                    return isAutoAlaign;
                }
            }
            else
            {
                Log.SaveLogToTxt("Equipments are not enough!");
                //AnalysisOutputProcData(procData);
                AnalysisOutputParameters(outputParameters);
                               
                return false;
            }
        }

        private bool SetTargetPower(double TargerPower)
        {
            double TempValue;

            tempAttenTX.AttnValue(TargerPower.ToString());

            TempValue = tempPowerMeter.ReadPower();

            int i = 0;

            while (Math.Abs(TempValue - TargerPower)>0.5)
            {
                tempAttenTX.AttnValue((TargerPower + (TargerPower - TempValue)).ToString());
                StartTxPwr = TargerPower + (TargerPower - TempValue);
                TempValue = tempPowerMeter.ReadPower();
                i++;
                if (i > 10)
                {
                    return false;
                }
            }

            return true;
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
                    if (tempKeys[i].ToUpper().Contains("PPG"))
                    {
                        selectedEquipList.Add("PPG", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("ERRORDETE"))                   
                    {
                        selectedEquipList.Add("ERRORDETE", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("ATTEN_RX"))                 
                    {
                        selectedEquipList.Add("ATTEN_RX", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("ATTEN_TX"))
                    {
                        selectedEquipList.Add("ATTEN_TX", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERMETER"))
                    {
                        selectedEquipList.Add("POWERMETER", tempValues[i]);
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

                tempPPG = (PPG)selectedEquipList["PPG"];
                tempED = (ErrorDetector)selectedEquipList["ERRORDETE"];
                tempAttenRX = (Attennuator)selectedEquipList["ATTEN_RX"];
                tempAttenTX = (Attennuator)selectedEquipList["ATTEN_TX"];
                tempPowerMeter = (PowerMeter)selectedEquipList["POWERMETER"];
                tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                if (tempPPG != null && tempED != null && tempAttenRX != null && tempAttenTX != null && tempPowerMeter != null && tempps != null)
                {
                    isOK = true;
                }
                else
                {
                    if (tempPPG == null)
                    {
                        Log.SaveLogToTxt("PPG =NULL");
                    }
                    if (tempED == null)
                    {
                        Log.SaveLogToTxt("ERRORDETE =NULL");
                    }
                    if (tempAttenRX == null)
                    {
                        Log.SaveLogToTxt("ATTEN_RX =NULL");
                    }
                    if (tempAttenTX == null)
                    {
                        Log.SaveLogToTxt("ATTEN_TX =NULL");
                    }
                    if (tempPowerMeter == null)
                    {
                        Log.SaveLogToTxt("POWERMETER =NULL");
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
                outputParameters = new TestModeEquipmentParameters[1];
                outputParameters[0].FiledName = "TxReturnLosTolerance(dbm))";
                TxReturnLosTolerance = Algorithm.ISNaNorIfinity(TxReturnLosTolerance);
                outputParameters[0].DefaultValue = TxReturnLosTolerance.ToString().Trim();

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
                    if (Algorithm.FindFileName(InformationList, "TargetPower", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.TargetPower = temp;
                        }
                    }
                    if (Algorithm.FindFileName(InformationList, "ReturnLosTolerancePRBS", out index))
                    {
                        byte temp = Convert.ToByte(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.ReturnLosTolerancePRBS = temp;
                        }
                    }
                    if (Algorithm.FindFileName(InformationList, "RXAttStep", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.RXAttStep = temp;
                        }
                    }
                    if (Algorithm.FindFileName(InformationList, "TXAttStep", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.TXAttStep = temp;
                        }

                    }
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
                            testTxReturnLostToleranceStruct.CsenAlignRxPwr = temp;
                        }
                    }
                    if (Algorithm.FindFileName(InformationList, "StartRxPwr", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.StartRxPwr = temp;
                        }
                    }
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
                            testTxReturnLostToleranceStruct.LoopTime = temp;
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
                            testTxReturnLostToleranceStruct.GatingTime = temp;
                        }
                    }
                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        }                        
        //protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        //{
        //    try
        //    {
        //        procData = new TestModeEquipmentParameters[2];
        //        procData[0].FiledName = "RxPowerArray";
        //        procData[0].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(RxPowerArray, ",");
        //        procData[1].FiledName = "BerArray";
        //        procData[1].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(BerArray, ",");
        //        return true;

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    
        protected void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
                //AnalysisOutputProcData(procData);
                
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
