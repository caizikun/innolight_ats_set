﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using Ivi.Visa.Interop;
namespace ATS_Framework
{
    public struct TestBerIntensifyStruct
    {
        public double CsenAlignRxPwr;// Auto Align时候的入射光
        public double CsenStartingRxPwr;// 搜寻第一点的起始光大小或者卡点测试入射光大小
        public double FirstPointErrorRateUL;// 搜寻第一点的误码率上限
        public double FirstPointErrorRateLL;// 搜寻第一点的误码率下限
        public double FirstPointStep;// 搜寻第一点的步长
        public double FirstPointRxPowerUL;//搜寻第一点的入射光上限
        public double FirstPointRxPowerLL;//搜寻第一点的入射光下限
        public double CoefCsenSubStep;//搜寻拟合点 入射光减小的步长
        public double CoefCsenAddStep;//搜寻拟合点入射光 增加的步长
        public bool IsBerQuickTest;//是否卡点测试hi
        public bool IsOpticalSourceUnitOMA;//输入光源是否为OmA
        public byte SearchTargetBerMethod;//第一点搜寻方法
        public double Ber_ERP;//目标误码点

        public double CoefErrorRateUL;//搜寻拟合点误码率上限
        public double CoefErrorRateLL;//搜寻拟合点误码率下限
       // public double InputParameterCount = 15;
    }
    public class TestBerIntensify : TestModelBase
    {
#region Attribute     

        private TestBerIntensifyStruct testBerStruct = new TestBerIntensifyStruct();
          
        private double sensitivityPoint;
        private double ber_erp = 1E-12;
        private double sensOMA;
        public int IsCoefErrorRateUpwards;//搜寻拟合点是否朝误码增大的方向
        private ArrayList inPutParametersNameArray = new ArrayList();
        private ArrayList OSERValueArray = new ArrayList();
        private ArrayList serchAttPoints = new ArrayList();
        private ArrayList serchRxDmidPoints = new ArrayList();
        private ArrayList serchBerPoints = new ArrayList();
        private ArrayList attPoints = new ArrayList();
        private ArrayList berPoints = new ArrayList();
        private double attPoint = 0;
        //equipments
       private Attennuator tempAtten;
       private ErrorDetector tempED;
       private Powersupply tempps;

#endregion

#region Method
       public TestBerIntensify(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;
            OSERValueArray.Clear();
            inPutParametersNameArray.Clear();
            
            //ber_erp
            //SearchTargetBerMethod
        //     public double CsenAlignRxPwr;// Auto Align时候的入射光
        //public double CsenStartingRxPwr;// 搜寻第一点的起始光大小或者卡点测试入射光大小
        //public double FirstPointErrorRateUL;// 搜寻第一点的误码率上限
        //public double FirstPointErrorRateLL;// 搜寻第一点的误码率下限
        //public double FirstPointStep;// 搜寻第一点的步长
        //public double FirstPointRxPowerUL;//搜寻第一点的入射光上限
        //public double FirstPointRxPowerLL;//搜寻第一点的入射光下限
        //public double CoefCsenSubStep;//搜寻拟合点 入射光减小的步长
        //public double CoefCsenAddStep;//搜寻拟合点入射光 增加的步长
        //public bool IsBerQuickTest;//是否卡点测试hi
        //public bool IsOpticalSourceUnitOMA;//输入光源是否为OmA
        //public byte SearchTargetBerMethod;//第一点搜寻方法
        //public double Ber_ERP;//目标误码点

        //public double CoefErrorRateUL;//搜寻拟合点误码率上限
        //public double CoefErrorRateLL;//搜寻拟合点误码率下限

            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("CsenAlignRxPwr");
            inPutParametersNameArray.Add("CsenStartingRxPwr");
            inPutParametersNameArray.Add("FirstPointErrorRateUL");
            inPutParametersNameArray.Add("FirstPointErrorRateLL");
            inPutParametersNameArray.Add("FirstPointStep");
            inPutParametersNameArray.Add("FirstPointRxPowerUL");
            inPutParametersNameArray.Add("FirstPointRxPowerLL");
            inPutParametersNameArray.Add("CoefCsenSubStep");
            inPutParametersNameArray.Add("CoefCsenAddStep");
            inPutParametersNameArray.Add("IsBerQuickTest");
            inPutParametersNameArray.Add("IsOpticalSourceUnitOMA");
            inPutParametersNameArray.Add("BER_ERP");
            inPutParametersNameArray.Add("SearchTargetBerMethod");
           inPutParametersNameArray.Add("CoefErrorRateUL");
           inPutParametersNameArray.Add("CoefErrorRateLL");
           
        }
        override protected bool CheckEquipmentReadiness()
        {
            //check if all equipments are ready for this test; 
            //increase equipment referenced_times if ready
            //for (int i = 0; i < pEquipList.Count; i++)
            //    if (!pEquipList.Values[i].bReady) return false;

            lock (tempAtten)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    if (!selectedEquipList.Values[i].bReady) return false;

                }

                return true;
            }
        }
        override protected bool PrepareTest()
        {//note: for inherited class, they need to do its own preparation process task,
            //then call this base function
            //for (int i = 0; i < pEquipList.Count; i++)
            ////pEquipList.Values[i].IncreaseReferencedTimes();
            //{
            lock (tempAtten)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    selectedEquipList.Values[i].IncreaseReferencedTimes();

                }


                return AssembleEquipment();
            }
        }

        protected override bool StartTest()
        {
            lock (tempAtten)
            {
                logoStr = "";
                if (AnalysisInputParameters(inputParameters) == false)
                {
                    OutPutandFlushLog();
                    return false;
                }

                GlobalBER_EXPtoRealBer();

                if (testBerStruct.IsOpticalSourceUnitOMA)
                {
                    testBerStruct.CsenAlignRxPwr = Algorithm.CalculateFromOMAtoDBM(testBerStruct.CsenAlignRxPwr, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                    testBerStruct.CsenStartingRxPwr = Algorithm.CalculateFromOMAtoDBM(testBerStruct.CsenStartingRxPwr, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                    testBerStruct.FirstPointRxPowerUL = Algorithm.CalculateFromOMAtoDBM(testBerStruct.FirstPointRxPowerUL, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                    testBerStruct.FirstPointRxPowerLL = Algorithm.CalculateFromOMAtoDBM(testBerStruct.FirstPointRxPowerLL, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                }

                if (tempAtten != null && tempED != null && tempps != null)
                {
                    // open apc 

                    {
                        CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                    }

                    // open apc

                    Log.SaveLogToTxt("Step3...SetAttenValue");
                    SetAttenValue(tempAtten, testBerStruct.CsenAlignRxPwr);
                    Log.SaveLogToTxt("Step4...AutoAlaign");
                    bool isAutoAlaign = tempED.AutoAlaign(true);
                    if (isAutoAlaign)
                    {
                        Log.SaveLogToTxt(isAutoAlaign.ToString());
                    }
                    else
                    {
                        Log.SaveLogToTxt(isAutoAlaign.ToString());
                        sensitivityPoint = -5000;
                        OutPutandFlushLog();
                        return isAutoAlaign;
                    }
                    if (isAutoAlaign)
                    {
                        if (testBerStruct.IsBerQuickTest == true)
                        {
                            QuickTest(tempAtten, tempED);
                            OutPutandFlushLog();
                            return true;
                        }
                        else
                        {
                            if (!SerchTargetBer(tempAtten, tempED))
                            {
                                OutPutandFlushLog();
                                return true;
                            }

                            GettingCurvingPointsandFitting(tempAtten, tempED);
                            if (double.IsNaN(sensitivityPoint) || double.IsInfinity(sensitivityPoint))
                            {
                                sensitivityPoint = -1000;
                                OutPutandFlushLog();
                            }
                            sensitivityPoint = Math.Round(sensitivityPoint, 2);
                            Log.SaveLogToTxt("sensitivityPoint= " + sensitivityPoint.ToString());
                            OutPutandFlushLog();
                            return true;
                        }

                    }
                    else
                    {
                        AnalysisOutputProcData(procData);
                        AnalysisOutputParameters(outputParameters);

                        return false;
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
        }
        protected void SetAttenValue(Attennuator tempAtt, double attValue)
        {
            lock (tempAtten)
            {
                tempAtt.AttnValue(attValue.ToString());
            }
        }

        override protected bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            lock (tempAtten)
            {
                bool flag = DeassembleEquipment();

                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    selectedEquipList.Values[i].DecreaseReferencedTimes();

                }
                return flag;
            }
        }
        protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {
            lock (tempAtten)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    if (!selectedEquipList.Values[i].Configure()) return false;

                }

                return true;
            }
        }

        protected override bool AssembleEquipment()
        {
            lock (tempAtten)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
                }
                return true;
            }
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
            lock (tempAtten)
            {
                try
                {
                    outputParameters = new TestModeEquipmentParameters[2];
                    outputParameters[0].FiledName = "CSEN_Intensify(DBM)";
                    sensitivityPoint = Algorithm.ISNaNorIfinity(sensitivityPoint);
                    if (testBerStruct.IsBerQuickTest == true)
                    {
                        outputParameters[0].DefaultValue = sensitivityPoint.ToString().Trim();
                    }
                    else
                    {
                        outputParameters[0].DefaultValue = Math.Round(sensitivityPoint, 4).ToString().Trim();
                    }


                    outputParameters[1].FiledName = "CSENOMA_Intensify(DBM)";
                    sensOMA = Algorithm.CalculateOMA(sensitivityPoint, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                    outputParameters[1].DefaultValue = Math.Round(sensOMA, 4).ToString().Trim();
                    return true;

                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            lock (tempAtten)
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
                bool isParametersComplete = false;
                if (InformationList.Length < inPutParametersNameArray.Count)
                {
                    isParametersComplete = false;
                    Log.SaveLogToTxt("inputData is NotEnough!");
                    return false;
                }
                else
                {
                    isParametersComplete = true;
                }
                int index = -1;
                //  isParametersComplete = true;

                if (isParametersComplete)
                {
                    //for (byte i = 0; i < InformationList.Length; i++)
                    {

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
                                if (temp > 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.CsenAlignRxPwr = temp;
                            }


                        }
                        if (Algorithm.FindFileName(InformationList, "CsenStartingRxPwr", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp > 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.CsenStartingRxPwr = temp;
                            }



                        }
                        if (Algorithm.FindFileName(InformationList, "FirstPointErrorRateUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.FirstPointErrorRateUL = temp;
                            }
                        }
                        if (Algorithm.FindFileName(InformationList, "FirstPointErrorRateLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.FirstPointErrorRateLL = temp;
                            }

                        }
                        if (Algorithm.FindFileName(InformationList, "FirstPointRxPowerUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp > 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.FirstPointRxPowerUL = temp;
                            }


                        }
                        if (Algorithm.FindFileName(InformationList, "FirstPointRxPowerLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                                if (temp > 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.FirstPointRxPowerLL = temp;
                            }

                        }

                        if (Algorithm.FindFileName(InformationList, "CoefCsenSubStep", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                testBerStruct.CoefCsenSubStep = 0.5;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.CoefCsenSubStep = temp;
                            }

                        }
                        if (Algorithm.FindFileName(InformationList, "CoefCsenAddStep", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                testBerStruct.CoefCsenAddStep = 0.5;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                                testBerStruct.CoefCsenAddStep = temp;
                            }

                        }
                        if (Algorithm.FindFileName(InformationList, "IsBerQuickTest", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.Trim().ToUpper() == "0" || temp.Trim().ToUpper() == "FALSE")
                            {
                                testBerStruct.IsBerQuickTest = false;
                            }
                            else
                            {
                                testBerStruct.IsBerQuickTest = true;
                            }

                        }
                        if (Algorithm.FindFileName(InformationList, "IsOpticalSourceUnitOMA", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.Trim().ToUpper() == "0" || temp.Trim().ToUpper() == "FALSE")
                            {
                                testBerStruct.IsOpticalSourceUnitOMA = false;
                            }
                            else
                            {
                                testBerStruct.IsOpticalSourceUnitOMA = true;
                            }

                        }
                        //SearchTargetBerStep
                        if (Algorithm.FindFileName(InformationList, "FirstPointStep", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                testBerStruct.FirstPointStep = 0.5;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    testBerStruct.FirstPointStep = 0.5;
                                }
                                testBerStruct.FirstPointStep = temp;
                            }
                        }
                        //testBerStruct.SearchTargetBerMethod
                        if (Algorithm.FindFileName(InformationList, "SearchTargetBerMethod", out index))
                        {
                            byte temp = Convert.ToByte(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                testBerStruct.SearchTargetBerMethod = 0;
                            }
                            else
                            {
                                testBerStruct.SearchTargetBerMethod = temp;
                            }
                        }
                        //BER_ERP

                        if (Algorithm.FindFileName(InformationList, "BER_ERP", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                testBerStruct.Ber_ERP = 1E-12;
                            }
                            else
                            {
                                testBerStruct.Ber_ERP = temp;
                            }
                        }
                        if (Algorithm.FindFileName(InformationList, "CoefErrorRateUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                testBerStruct.CoefErrorRateUL = temp;
                            }
                            else
                            {
                                testBerStruct.CoefErrorRateUL = temp;
                            }
                        }
                        if (Algorithm.FindFileName(InformationList, "CoefErrorRateLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
                                testBerStruct.Ber_ERP = 1E-11;
                            }
                            else
                            {
                                testBerStruct.CoefErrorRateLL = temp;
                            }
                        }
                    }

                }
                if (testBerStruct.FirstPointErrorRateUL <= testBerStruct.FirstPointErrorRateLL || testBerStruct.FirstPointRxPowerUL <= testBerStruct.FirstPointRxPowerLL)
                {
                    Log.SaveLogToTxt("inputData is wrong!");
                    return false;
                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        }
        protected double SerchTargetPoint(Attennuator tempAtt, ErrorDetector tempED, double attValue, double targetBerLL, double targetBerUL, double addStep, double sumStep)
        {
            lock (tempAtten)
            {
                byte i = 0;
                double currentCense = 0;
                do
                {
                    tempAtt.AttnValue(attValue.ToString());
                    currentCense = tempED.RapidErrorRate(1);
                    if (currentCense > targetBerUL)
                    {
                        attValue += addStep;
                    }
                    else
                    {
                        attValue -= sumStep;
                    }
                    i++;

                } while (i < 20 && (currentCense > targetBerUL || currentCense < targetBerLL));

                return attValue;
            }
        }
        protected bool SerchCoefPointsTarget1E3(Attennuator tempAtt, ErrorDetector tempED, double startAttValue, double targetBer, double addStep, double sumStep, out ArrayList AttenPoints, out ArrayList BerPints)
        {
            lock (tempAtten)
            {
                byte i = 0;
                double currentBer = 0;
                //AttenPoints[]=
                AttenPoints = new ArrayList();
                BerPints = new ArrayList();
                AttenPoints.Clear();
                BerPints.Clear();
                do
                {
                    tempAtt.AttnValue(startAttValue.ToString());
                    currentBer = tempED.RapidErrorRate();
                    if (currentBer != 0)
                    {
                        AttenPoints.Add(startAttValue);
                        BerPints.Add(currentBer);
                    }
                    if (currentBer > targetBer)
                    {
                        startAttValue += addStep;
                    }
                    else
                    {
                        startAttValue -= sumStep;
                    }
                    i++;

                } while (i < 8 && (currentBer < (targetBer * 1E-1)));

                return true;
            }
        }
        protected bool SerchCoefPoints(Attennuator tempAtt, ErrorDetector tempED, double startAttValue, double targetBer, double addStep, double sumStep, out ArrayList AttenPoints, out ArrayList BerPints)
        {
            lock (tempAtten)
            {
                byte i = 0;

                bool terminalFlag = false;
                double currentBer = 0;
                bool isEnd = false;
                //AttenPoints[]=
                AttenPoints = new ArrayList();
                BerPints = new ArrayList();
                AttenPoints.Clear();
                BerPints.Clear();


                tempAtt.AttnValue(startAttValue.ToString(), 0);
                Thread.Sleep(1500);
                currentBer = tempED.RapidErrorRate();

                if (currentBer < this.testBerStruct.Ber_ERP)
                {
                    this.IsCoefErrorRateUpwards = 1;
                }
                else
                {
                    this.IsCoefErrorRateUpwards = -1;
                }

                double CurrentStartAttValue = startAttValue;

                #region  Add InputPower

                terminalFlag = true;
                int Runcount = 1;

                do
                {
                    tempAtt.AttnValue(CurrentStartAttValue.ToString(), 0);
                    Thread.Sleep(1000);
                    double TempRxDmiPower = dut.ReadDmiRxp();

                    Log.SaveLogToTxt("RxInputPower=" + CurrentStartAttValue);
                    Log.SaveLogToTxt("RxDmiPower=" + TempRxDmiPower.ToString());

                    currentBer = tempED.RapidErrorRate();

                    terminalFlag = (currentBer > this.testBerStruct.CoefErrorRateLL && currentBer < this.testBerStruct.CoefErrorRateUL);

                    if (terminalFlag == true && currentBer != 0)
                    {
                        AttenPoints.Add(CurrentStartAttValue);
                        BerPints.Add(currentBer);
                    }

                    if (IsCoefErrorRateUpwards > 0 && currentBer == 0)
                    {
                        isEnd = true;
                    }

                    if (!terminalFlag)//搜点到达边缘
                    {

                        if (AttenPoints.Count >= 5) //点数够了 可以结束
                        {
                            isEnd = true;
                        }
                        else
                        {
                            if (Runcount < 2)//点数不够，需要反方向搜寻,至少5个点
                            {
                                IsCoefErrorRateUpwards *= -1;
                                CurrentStartAttValue = startAttValue;
                                terminalFlag = true;
                                Runcount = 2;
                            }
                            else//已经经过来反转
                            {
                                isEnd = true;
                            }
                        }


                    }

                    if (IsCoefErrorRateUpwards > 0)
                    {
                        CurrentStartAttValue -= sumStep;

                    }
                    else
                    {
                        CurrentStartAttValue += addStep;
                    }


                } while (!isEnd);

                #endregion




                return true;
            }
        }
        protected  bool deleteErrBerPoints(ArrayList inputARR1, ArrayList inputARR2)
        {
            lock (tempAtten)
            {
                if (inputARR1.Count != inputARR2.Count
                   || inputARR1.Count == 0
                   || inputARR2.Count == 0
                   || inputARR1 == null
                   || inputARR2 == null)
                {
                    return false;
                }
                for (int i = inputARR1.Count - 1; i >= 0; i--)
                {
                    if (Convert.ToDouble(inputARR1[i]) >= 1
                        || (double.IsInfinity(Convert.ToDouble(inputARR1[i])))
                        || (double.IsNaN(Convert.ToDouble(inputARR1[i])))
                        || (inputARR1[i] == null)
                        || (inputARR2[i] == null)
                        || (double.IsInfinity(Convert.ToDouble(inputARR2[i])))
                        || (double.IsNaN(Convert.ToDouble(inputARR2[i]))))
                    {
                        inputARR1.RemoveAt(i);
                        inputARR2.RemoveAt(i);
                    }

                }

                return true;
            }
        }
        protected double StepSearchTargetPoint(double StartInputPower, double LLimit, double ULimit, double targetBerLL, double targetBerUL, Attennuator tempAtt, ErrorDetector tempED, out ArrayList serchAttPoints, out ArrayList serchRxDmiPoints, out ArrayList serchBerPoints)
        {
            //double low = LLimit;
            //double high = ULimit;
            lock (tempAtten)
            {
                double currentCense = 0;
                byte Rcount = 0;
                serchAttPoints = new ArrayList();
                serchRxDmiPoints = new ArrayList();
                serchBerPoints = new ArrayList();
                serchAttPoints.Clear();
                serchBerPoints.Clear();

                double CurrentInputPower = StartInputPower;

                // tempAtt

                while (CurrentInputPower <= ULimit && CurrentInputPower >= LLimit && Rcount <= 20)
                {
                    tempAtt.AttnValue(CurrentInputPower.ToString());
                    double TempRxDmiPower = dut.ReadDmiRxp();
                    serchAttPoints.Add(CurrentInputPower);
                    serchRxDmiPoints.Add(TempRxDmiPower);
                    currentCense = tempED.RapidErrorRate();
                    serchBerPoints.Add(currentCense);

                    if (currentCense < targetBerLL)// 误码太小->光太大->减小光
                    {
                        CurrentInputPower -= testBerStruct.FirstPointStep;
                        Rcount++;
                    }
                    else if (currentCense > targetBerUL)// 误码太大->光太小->加大入射光
                    {
                        CurrentInputPower += testBerStruct.FirstPointStep;
                        Rcount++;
                    }
                    else
                        return CurrentInputPower;

                }
                return -10000;
            }
        }

        protected double binarySearchTargetPoint(double StartInputPower, double LLimit, double ULimit, double targetBerLL, double targetBerUL, Attennuator tempAtt, ErrorDetector tempED, out ArrayList serchAttPoints, out ArrayList serchRxDmiPoints, out ArrayList serchBerPoints)
        {
            lock (tempAtten)
            {
                double low = LLimit;
                double high = ULimit;
                double currentCense = 0;
                byte count = 0;
                serchAttPoints = new ArrayList();
                serchBerPoints = new ArrayList();
                serchRxDmiPoints = new ArrayList();
                serchAttPoints.Clear();
                serchBerPoints.Clear();

                double CurrentInputPower = StartInputPower;

                tempAtt.AttnValue(((high + low) / 2).ToString(), 1);
                //Thread.Sleep(2000);
                double TempValue = tempED.RapidErrorRate();

                Log.SaveLogToTxt("RxInputPower=" + ((high + low) / 2).ToString());
                Log.SaveLogToTxt("CurrentErrorRate=" + TempValue.ToString());

                while (low <= high && count <= 20)
                {
                    if (Math.Abs(low - high) < 0.2) break;

                    double mid = low + ((high - low) / 2);
                    tempAtt.AttnValue(mid.ToString(), 1);
                    double TempRxDmiPower = dut.ReadDmiRxp();



                    serchAttPoints.Add(mid);
                    serchRxDmiPoints.Add(TempRxDmiPower);
                    currentCense = tempED.RapidErrorRate();
                    //Log.SaveLogToTxt("RxInputPower=" + mid);
                    //Log.SaveLogToTxt("RxDmiPower=" + TempRxDmiPower.ToString());
                    //Log.SaveLogToTxt("CurrentErrorRate=" + currentCense.ToString());

                    serchBerPoints.Add(currentCense);
                    if (currentCense < targetBerLL)
                    {
                        high = mid;
                        count++;
                    }
                    else if (currentCense > targetBerUL)
                    {
                        low = mid;
                        count++;
                    }
                    else
                        return mid;
                }
                return -10000;
            }
        }
     
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {
            lock (tempAtten)
            {
                try
                {
                    procData = new TestModeEquipmentParameters[4];
                    procData[0].FiledName = "SearTargetBerRxPowerArray";
                    procData[0].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(serchAttPoints, ",");
                    procData[1].FiledName = "SearTargetBerArray";
                    procData[1].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(serchBerPoints, ",");
                    procData[2].FiledName = "CurvingRxPowerArray";
                    procData[2].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(attPoints, ",");
                    procData[3].FiledName = "CurvingBerArray";
                    procData[3].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(berPoints, ",");
                    return true;

                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }


        }
        protected void QuickTest(Attennuator tempAtten, ErrorDetector tempED)
        {
            lock (tempAtten)
            {
                try
                {
                    tempAtten.AttnValue(testBerStruct.CsenStartingRxPwr.ToString());
                    double sensitivity = tempED.RapidErrorRate();
                    Log.SaveLogToTxt("SetAtten=" + testBerStruct.CsenStartingRxPwr.ToString());
                    Log.SaveLogToTxt("QUICBER=" + sensitivity.ToString());
                    if (sensitivity >= 1)
                    {
                        sensitivityPoint = 1;
                        AnalysisOutputParameters(outputParameters);
                        AnalysisOutputProcData(procData);

                        return;
                    }

                    {
                        if (sensitivity <= ber_erp)
                        {
                            sensitivityPoint = testBerStruct.CsenStartingRxPwr;
                        }
                        else
                        {
                            sensitivityPoint = sensitivity;
                            Log.SaveLogToTxt("AttPoint=" + testBerStruct.CsenStartingRxPwr.ToString() + ber_erp.ToString());

                        }
                    }
                    Log.SaveLogToTxt("sensitivityPoint= " + sensitivityPoint.ToString());
                    AnalysisOutputParameters(outputParameters);
                    AnalysisOutputProcData(procData);

                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected bool SerchTargetBer(Attennuator tempAtten, ErrorDetector tempED)
        {
            lock (tempAtten)
            {
                try
                {

                    Log.SaveLogToTxt("Step5...SerchTargetPoint");

                    switch (testBerStruct.SearchTargetBerMethod)
                    {
                        case 1:
                            attPoint = StepSearchTargetPoint(testBerStruct.CsenStartingRxPwr, testBerStruct.FirstPointRxPowerLL, testBerStruct.FirstPointRxPowerUL, testBerStruct.FirstPointErrorRateLL, testBerStruct.FirstPointErrorRateUL, tempAtten, tempED, out serchAttPoints, out serchRxDmidPoints, out serchBerPoints);

                            break;
                        default:
                            attPoint = binarySearchTargetPoint(testBerStruct.CsenStartingRxPwr, testBerStruct.FirstPointRxPowerLL, testBerStruct.FirstPointRxPowerUL, testBerStruct.FirstPointErrorRateLL, testBerStruct.FirstPointErrorRateUL, tempAtten, tempED, out serchAttPoints, out serchRxDmidPoints, out serchBerPoints);
                            break;

                    }


                    Log.SaveLogToTxt("SetAtten=" + attPoint.ToString());
                    for (byte i = 0; i < serchAttPoints.Count; i++)
                    {
                        Log.SaveLogToTxt("serchAttPoints[ " + i.ToString() + "]" + double.Parse(serchAttPoints[i].ToString()) + "  " + "serchRxDmidPoints[ " + i.ToString() + "]" + double.Parse(serchRxDmidPoints[i].ToString()) + "  " + "serchBerPoints[ " + i.ToString() + "]" + double.Parse(serchBerPoints[i].ToString()));
                    }
                    if (attPoint == -10000)
                    {
                        sensitivityPoint = -10000;
                        AnalysisOutputProcData(procData);
                        AnalysisOutputParameters(outputParameters);

                        return false;
                    }
                    return true;
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected void GettingCurvingPointsandFitting(Attennuator tempAtten, ErrorDetector tempED)
        {
            lock (tempAtten)
            {
                try
                {
                    double intercept;
                    double slope;
                    byte tmepCount = 0;
                    Log.SaveLogToTxt("Step6...SerchCoefPoints");
                    {
                        SerchCoefPoints(tempAtten, tempED, attPoint, ber_erp, testBerStruct.CoefCsenAddStep, testBerStruct.CoefCsenSubStep, out attPoints, out berPoints);
                        tmepCount = (byte)Math.Min(attPoints.Count, berPoints.Count);
                        double[] tempattPoints = new double[tmepCount];
                        double[] tempberPoints = new double[tmepCount];
                        for (byte i = 0; i < tmepCount; i++)
                        {
                            tempattPoints[i] = double.Parse(attPoints[i].ToString());
                            tempberPoints[i] = double.Parse(berPoints[i].ToString());
                            Log.SaveLogToTxt("attPoints[ " + i.ToString() + "]" + double.Parse(attPoints[i].ToString()) + "  " + "berPoints[ " + i.ToString() + "]" + double.Parse(berPoints[i].ToString()));
                        }
                        Algorithm.LinearRegression(Algorithm.Getlog10(Algorithm.GetNegative(Algorithm.Getlog10(tempberPoints))), tempattPoints, out slope, out intercept);
                        sensitivityPoint = slope + (intercept * System.Math.Log10(System.Math.Log10(ber_erp) * (-1)));

                        Log.SaveLogToTxt("LinearSlope=" + intercept + " LinearOffset= " + slope);// 因为Offset 和 Slope 的幅值颠倒 Leo


                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected void OutPutandFlushLog()
        {
            lock (tempAtten)
            {
                try
                {
                    AnalysisOutputParameters(outputParameters);
                    AnalysisOutputProcData(procData);

                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected void GlobalBER_EXPtoRealBer()
        {
            lock (tempAtten)
            {
                ber_erp = testBerStruct.Ber_ERP;
            }
        }
#endregion
       
      
    }
}
