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
       public TestTxReturnLostTolerance(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
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

            logger.FlushLogBuffer();
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

                logoStr += logger.AdapterLogString(0, "Step2...SetAttenValue");
                tempAttenRX.AttnValue(testTxReturnLostToleranceStruct.CsenAlignRxPwr.ToString());
                logoStr += logger.AdapterLogString(0, "Step3...AutoAlaign");

                bool isAutoAlaign = tempED.AutoAlaign(true);
                if (isAutoAlaign)
                {
                    logoStr += logger.AdapterLogString(1, isAutoAlaign.ToString());
                    logoStr += logger.AdapterLogString(0, "Step4...TestTxReturnLostTolerance");

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
                        logoStr += logger.AdapterLogString(1, "SetAttenRX=" + RxPower.ToString());
                           
                        tempED.EdGatingStart();    //刷新误码数
                        for (int j = 0; j < LoopCount; j++)
                        {
                            Thread.Sleep(Convert.ToInt32(testTxReturnLostToleranceStruct.GatingTime * 1000));
                            ber = tempED.QureyEdErrorRatio();                                
                            logoStr += logger.AdapterLogString(1, "BerRX=" + ber.ToString());
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
                        logoStr += logger.AdapterLogString(1, "NoneBerPoint= " + NoneBerPoint.ToString());
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
                            logoStr += logger.AdapterLogString(1, "BerRX=" + ber.ToString());
                            if (ber != 0)
                            {
                                break;
                            }                            
                        }
                        
                        if(ber == 0)
                        {
                            TxReturnLosTolerance = tempPowerMeter.ReadPower();
                            logoStr += logger.AdapterLogString(1, "TxReturnLosTolerance= " + TxReturnLosTolerance.ToString());
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
                    logoStr += logger.AdapterLogString(4, isAutoAlaign.ToString());
                    TxReturnLosTolerance = -1000;
                    OutPutandFlushLog();
                    return isAutoAlaign;
                }
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                //AnalysisOutputProcData(procData);
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();               
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
                        logoStr += logger.AdapterLogString(3, "PPG =NULL");
                    }
                    if (tempED == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ERRORDETE =NULL");
                    }
                    if (tempAttenRX == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN_RX =NULL");
                    }
                    if (tempAttenTX == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN_TX =NULL");
                    }
                    if (tempPowerMeter == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERMETER =NULL");
                    }
                    if (tempps == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
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
                TxReturnLosTolerance = algorithm.ISNaNorIfinity(TxReturnLosTolerance);
                outputParameters[0].DefaultValue = TxReturnLosTolerance.ToString().Trim();

                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");
            
            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                logoStr += logger.AdapterLogString(4, "InputParameters are not enough!");
                return false;
            }
            else
            {
                int index = -1;
                bool isParametersComplete = true;

                if (isParametersComplete)
                {
                    if (algorithm.FindFileName(InformationList, "TargetPower", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.TargetPower = temp;
                        }
                    }
                    if (algorithm.FindFileName(InformationList, "ReturnLosTolerancePRBS", out index))
                    {
                        byte temp = Convert.ToByte(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.ReturnLosTolerancePRBS = temp;
                        }
                    }
                    if (algorithm.FindFileName(InformationList, "RXAttStep", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.RXAttStep = temp;
                        }
                    }
                    if (algorithm.FindFileName(InformationList, "TXAttStep", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.TXAttStep = temp;
                        }

                    }
                    if (algorithm.FindFileName(InformationList, "CsenAlignRxPwr", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.CsenAlignRxPwr = temp;
                        }
                    }
                    if (algorithm.FindFileName(InformationList, "StartRxPwr", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.StartRxPwr = temp;
                        }
                    }
                    if (algorithm.FindFileName(InformationList, "LoopTime", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.LoopTime = temp;
                        }
                    }
                    if (algorithm.FindFileName(InformationList, "GatingTime", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testTxReturnLostToleranceStruct.GatingTime = temp;
                        }
                    }
                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }                        
        //protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        //{
        //    try
        //    {
        //        procData = new TestModeEquipmentParameters[2];
        //        procData[0].FiledName = "RxPowerArray";
        //        procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(RxPowerArray, ",");
        //        procData[1].FiledName = "BerArray";
        //        procData[1].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(BerArray, ",");
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
