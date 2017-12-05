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
       public TestRxOverload(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
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
            logger.FlushLogBuffer();
            logoStr = "";
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }

            //if (testRxOverloadStruct.IsOptSourceUnitOMA)
            //{
            //    testRxOverloadStruct.SpecDelta = algorithm.CalculateFromOMAtoDBM(testRxOverloadStruct.SpecDelta, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
            //    testRxOverloadStruct.SpecDelta = Math.Round(testRxOverloadStruct.SpecDelta, 4);          
            //    testRxOverloadStruct.CsenAlignRxPwr = algorithm.CalculateFromOMAtoDBM(testRxOverloadStruct.CsenAlignRxPwr, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
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
                logoStr += logger.AdapterLogString(0, "Step2...SetAttenValue");
                tempAtten.AttnValue(testRxOverloadStruct.CsenAlignRxPwr.ToString());
                logoStr += logger.AdapterLogString(0, "Step3...AutoAlaign");
                bool isAutoAlaign = tempED.AutoAlaign(true);
                if (isAutoAlaign)
                {
                    logoStr += logger.AdapterLogString(1, isAutoAlaign.ToString());
                    logoStr += logger.AdapterLogString(0, "Step4...StartTestRxOverload");

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
                            logoStr += logger.AdapterLogString(1, "Can't find OverloadPoint...");
                            break;
                        }
                        else
                        {
                            tempAtten.AttnValue(RxPower.ToString());
                            RxPowerArray.Add(RxPower);
                            logoStr += logger.AdapterLogString(1, "SetAtten=" + RxPower.ToString());
                           
                            tempED.EdGatingStart();    //刷新误码数
                            for (int j = 0; j < LoopCount; j++)
                            {                               
                                Thread.Sleep(Convert.ToInt32(testRxOverloadStruct.GatingTime * 1000));
                                ber = tempED.QureyEdErrorRatio();                                
                                logoStr += logger.AdapterLogString(1, "Ber=" + ber.ToString());
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
                                logoStr += logger.AdapterLogString(1, "Can't find OverloadPoint...");
                                break;
                            }
                            else
                            {
                                tempAtten.AttnValue(RxPower.ToString());
                                RxPowerArray.Add(RxPower);
                                logoStr += logger.AdapterLogString(1, "SetAtten=" + RxPower.ToString());

                                tempED.EdGatingStart();    //刷新误码数
                                for (int j = 0; j < LoopCount; j++)
                                {
                                    Thread.Sleep(Convert.ToInt32(testRxOverloadStruct.GatingTime * 1000));
                                    ber = tempED.QureyEdErrorRatio();
                                    logoStr += logger.AdapterLogString(1, "Ber=" + ber.ToString());
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
                            logoStr += logger.AdapterLogString(1, "OverloadPoint= " + OverloadPoint.ToString());
                        }
                    }
                    else          //光源点无误码
                    {
                        OverloadPoint = RxPower;
                        logoStr += logger.AdapterLogString(1, "OverloadPoint= " + OverloadPoint.ToString() + " LightSource= " + OverloadPoint.ToString());
                    }

                    OutPutandFlushLog();
                    return true;                   
                } 
                else
                {
                    logoStr += logger.AdapterLogString(4, isAutoAlaign.ToString());
                    OverloadPoint = -1000;
                    OutPutandFlushLog();
                    return isAutoAlaign;
                }
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                AnalysisOutputProcData(procData);
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();               
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
                        logoStr += logger.AdapterLogString(3, "ERRORDETE =NULL");
                    }
                    if (tempAtten == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
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
                outputParameters = new TestModeEquipmentParameters[2];
                outputParameters[0].FiledName = "Overload(dbm)";
                OverloadPoint = algorithm.ISNaNorIfinity(OverloadPoint);
                outputParameters[0].DefaultValue = Math.Round(OverloadPoint, 4).ToString().Trim();

                outputParameters[1].FiledName = "OverloadOMA(dbm)";
                OverloadOMA = algorithm.CalculateOMA(OverloadPoint, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                outputParameters[1].DefaultValue = Math.Round(OverloadOMA, 4).ToString().Trim();
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

            if (GlobalParameters.OpticalSourseERArray == null || GlobalParameters.OpticalSourseERArray == "")
            {
                return false;
            }
            else
            {
                char[] tempCharArray = new char[] { ',' };
                OSERValueArray = algorithm.StringtoArraylistDeletePunctuations(GlobalParameters.OpticalSourseERArray, tempCharArray);
            }
            
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
                            testRxOverloadStruct.LoopTime = temp;
                        }
                    }
                    if (algorithm.FindFileName(InformationList, "AttStep", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxOverloadStruct.AttStep = temp;
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
                            testRxOverloadStruct.GatingTime = temp;
                        }
                    }
                    //if (algorithm.FindFileName(InformationList, "IsOptSourceUnitOMA", out index))
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
                    //if (algorithm.FindFileName(InformationList, "SpecDelta", out index))
                    //{
                    //    double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                    //    if (double.IsInfinity(temp) || double.IsNaN(temp))
                    //    {
                    //        logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                    //        return false;
                    //    }
                    //    else
                    //    {
                    //        testRxOverloadStruct.SpecDelta = temp;
                    //    }

                    //}
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
                            testRxOverloadStruct.CsenAlignRxPwr = temp;
                        }
                    }
                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }


        }
       
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                procData = new TestModeEquipmentParameters[2];
                procData[0].FiledName = "RxPowerArray";
                procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(RxPowerArray, ",");
                procData[1].FiledName = "BerArray";
                procData[1].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(BerArray, ",");
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    
        protected void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
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
