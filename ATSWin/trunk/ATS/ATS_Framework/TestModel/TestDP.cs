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

    public struct MyInputParameter
    {
        public double CsenAlignRxPwr;
        public double CsenStartingRxPwr;
        public double SearchTargetBerUL;
        public double SearchTargetBerLL;
        public double SearchTargetBerStep;
        public double SearchTargetBerRxpowerUL;
        public double SearchTargetBerRxpowerLL;
        public double CoefCsenSubStep;
        public double CoefCsenAddStep;
        public bool IsBerQuickTest;
        public bool IsOpticalSourceUnitOMA;
        public byte SearchTargetBerMethod;
    }

    public class TestTxDP : TestModelBase
    {
        #region  Attribute

        struct InputParameter
        {
            public ArrayList inPutParametersNameArray;

            public double CsenAlignRxPwr;
            public double CsenStartingRxPwr;
            public double SearchTargetBerUL;
            public double SearchTargetBerLL;
            public double SearchTargetBerStep;
            public double SearchTargetBerRxpowerUL;
            public double SearchTargetBerRxpowerLL;
            public double CoefCsenSubStep;
            public double CoefCsenAddStep;
            public bool IsBerQuickTest;
            public bool IsOpticalSourceUnitOMA;
            public byte SearchTargetBerMethod;
            public double FiberLoss;
        }

        class TestModeparameter
        {
            public double sensitivityPoint=-100;
            public double ber_erp = 0;
            public double sensOMA;
            public double FirstBerPointRxPower = 0;
            public double LightSource;
            public double LongFiberCsen;
            public double ShortFiberCsen;
            public double DiffCsen;
           // public double TxPowerDmi;
           // public bool IsLongfiberModel = false;

        }

        struct Equpment
        {
            public Attennuator pATT;
            public ErrorDetector pED;
            public Powersupply pPowerSupply;
            public DUT pDut;
            public OpticalSwitch pOptSwitch;

        }

        struct ProcessData
        {

            public ArrayList OSERValueArray;
            public ArrayList serchAttPoints;
            public ArrayList serchRxDmidPoints;
            public ArrayList serchBerPoints;
            public ArrayList attPoints;
            public ArrayList berPoints;
        }

        TestModeparameter MyTestModePrameter = new TestModeparameter();

        InputParameter MyInputParameter = new InputParameter();

        Equpment MyEqupment = new Equpment();

        ProcessData MyProcessData = new ProcessData();

        #endregion

        #region Method

        public TestTxDP(DUT inPuDut, logManager logmanager)
        {

             MyProcessData.OSERValueArray = new ArrayList();
             MyProcessData.serchAttPoints = new ArrayList();
             MyProcessData.serchRxDmidPoints = new ArrayList();
             MyProcessData.serchBerPoints = new ArrayList();
             MyProcessData.attPoints = new ArrayList();
             MyProcessData.berPoints = new ArrayList();


            logger = logmanager;
            logoStr = null;
            MyEqupment.pDut = inPuDut;

            dut = inPuDut;
           
            MyInputParameter.inPutParametersNameArray=new ArrayList() ;

            MyProcessData.OSERValueArray.Clear();

            MyInputParameter.inPutParametersNameArray.Clear();
            MyInputParameter.inPutParametersNameArray.Add("CsenAlignRXPWR(DBM)");
            MyInputParameter.inPutParametersNameArray.Add("CsenStartRXPWR(DBM)");
            MyInputParameter.inPutParametersNameArray.Add("SearchTargetBERUL");
            MyInputParameter.inPutParametersNameArray.Add("SearchTargetBERLL");
            MyInputParameter.inPutParametersNameArray.Add("SearchTargetRXPWRLL");
            MyInputParameter.inPutParametersNameArray.Add("SearchTargetRXPWRUL");
            MyInputParameter.inPutParametersNameArray.Add("CoefCsenSubStep(DBM)");
            MyInputParameter.inPutParametersNameArray.Add("CoefCsenAddStep(DBM)");
            MyInputParameter.inPutParametersNameArray.Add("IsBerQuickTest");
            // MyInputParameter.inPutParametersNameArray.Add("ISOPTICALSOURCEUNITOMA");
            MyInputParameter.inPutParametersNameArray.Add("IsOptSourceUnitOMA");
            MyInputParameter.inPutParametersNameArray.Add("SearchTargetBerMethod");
            MyInputParameter.inPutParametersNameArray.Add("SearchTargetBerStep");
            MyInputParameter.inPutParametersNameArray.Add("FiberLoss");

             //FiberLoss
            //SearchTargetBerMethod
            
           
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

        public override bool Test()
        {
            logger.FlushLogBuffer();
            logoStr = "";

            MyBackGroundLog.Clear();

            try
            {


                if (AnalysisInputParameters(inputParameters) == false)
                {
              
                    return false;
                }

               // GlobalBER_EXPtoRealBer();

                if (MyInputParameter.IsOpticalSourceUnitOMA)
                {
                    MyInputParameter.CsenAlignRxPwr = algorithm.CalculateFromOMAtoDBM(MyInputParameter.CsenAlignRxPwr, Convert.ToDouble(MyProcessData.OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                    MyInputParameter.CsenStartingRxPwr = algorithm.CalculateFromOMAtoDBM(MyInputParameter.CsenStartingRxPwr, Convert.ToDouble(MyProcessData.OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                    MyInputParameter.SearchTargetBerRxpowerUL = algorithm.CalculateFromOMAtoDBM(MyInputParameter.SearchTargetBerRxpowerUL, Convert.ToDouble(MyProcessData.OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                    MyInputParameter.SearchTargetBerRxpowerLL = algorithm.CalculateFromOMAtoDBM(MyInputParameter.SearchTargetBerRxpowerLL, Convert.ToDouble(MyProcessData.OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                }

                CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));  // open apc

                MyEqupment.pOptSwitch.SelectMode(1, 1);//选择短光纤模式

                logoStr += logger.AdapterLogString(0, "Step3...SetAttenValue");


                MyTestModePrameter.LightSource = MyEqupment.pDut.ReadDmiTxp();

                SetAttenValue(MyInputParameter.CsenAlignRxPwr);
                logoStr += logger.AdapterLogString(0, "Step4...AutoAlaign");

                bool isAutoAlaign = MyEqupment.pED.AutoAlaign(true);

                if (isAutoAlaign)
                {
                    logoStr += logger.AdapterLogString(1, isAutoAlaign.ToString());
                }
                else
                {
                    logoStr += logger.AdapterLogString(4, isAutoAlaign.ToString());
                    MyTestModePrameter.sensitivityPoint = -5000;
                    return false;
                }

               // MyEqupment.pATT.o

                MyTestModePrameter.LightSource = MyEqupment.pDut.ReadDmiTxp();
                MyEqupment.pOptSwitch.SelectMode(1, 1);//选择短光纤模式
                NormalTest();

                MyProcessData.serchAttPoints.Clear();
                MyProcessData.serchBerPoints.Clear();



                MyInputParameter.CsenStartingRxPwr = MyTestModePrameter.FirstBerPointRxPower;
                MyTestModePrameter.ShortFiberCsen = MyTestModePrameter.sensitivityPoint;

                logoStr += logger.AdapterLogString(1, "ShortFiberCsen=" + MyTestModePrameter.ShortFiberCsen);

                MyEqupment.pOptSwitch.SelectMode(0, 1);//选择长光纤模式
                MyTestModePrameter.LightSource = MyEqupment.pDut.ReadDmiTxp()-MyInputParameter.FiberLoss;
                NormalTest();
  
                MyTestModePrameter.LongFiberCsen = MyTestModePrameter.sensitivityPoint;
                logoStr += logger.AdapterLogString(1, "LongFiberCsen=" + MyTestModePrameter.LongFiberCsen);
                MyTestModePrameter.DiffCsen = MyTestModePrameter.LongFiberCsen - MyTestModePrameter.ShortFiberCsen;

                return true;
            }
            catch(Exception ex)
            {
                logoStr += logger.AdapterLogString(3, ex.Message);
                return false;
            }
        }
        private bool NormalTest()
        {
           
            if (!SerchTargetBer())
            {
               
                return false;
            }

            GettingCurvingPointsandFitting();
            if (double.IsNaN(MyTestModePrameter.sensitivityPoint) || double.IsInfinity(MyTestModePrameter.sensitivityPoint))
            {
                MyTestModePrameter.sensitivityPoint = -1000;
              
            }
            MyTestModePrameter.sensitivityPoint = Math.Round(MyTestModePrameter.sensitivityPoint, 2);
            logoStr += logger.AdapterLogString(1, "sensitivityPoint= " + MyTestModePrameter.sensitivityPoint.ToString());

            return true;
        }
        protected override bool StartTest()
        {

            try
            {
                if (Test())
                {
                    OutPutandFlushLog();
                }
                else
                {
                    OutPutandFlushLog();
                }
                return true;

            }
            catch
            {
                return false;
            }
          
           
        }

        protected void SetAttenValue(double InputPower)
        {

            double TempValue = MyTestModePrameter.LightSource - InputPower;

            if (TempValue<0)
            {
                TempValue = 0;
            }
            TempValue = Math.Round(TempValue, 1);
            MyEqupment.pATT.SetAttnValue(TempValue);

        }

        private double LongFiberTest()
        {

            return 0;
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
                    if (tempKeys[i].ToUpper().Contains("COMPSWTICH"))
                    {
                        selectedEquipList.Add("COMPSWTICH", tempValues[i]);
                    }
                }
                MyEqupment.pATT = (Attennuator)selectedEquipList["ATTEN"];
                MyEqupment.pED = (ErrorDetector)selectedEquipList["ERRORDETE"];
                MyEqupment.pPowerSupply = (Powersupply)selectedEquipList["POWERSUPPLY"];
                MyEqupment.pOptSwitch = (OpticalSwitch)selectedEquipList["COMPSWTICH"];

                if (MyEqupment.pATT != null && MyEqupment.pED != null && MyEqupment.pPowerSupply != null)
                {
                    isOK = true;

                }
                else
                {
                    if (MyEqupment.pED == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ERRORDETE =NULL");
                    }
                    if (MyEqupment.pATT == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
                    }
                    if (MyEqupment.pPowerSupply == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    if (MyEqupment.pOptSwitch == null)
                    {
                        logoStr += logger.AdapterLogString(3, "OptSwitch =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
                    isOK = false;
                }
                if (isOK)
                {

                    selectedEquipList.Add("DUT", MyEqupment.pDut);
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
                outputParameters[0].FiledName = "DiffCsen(DBM)";
                MyTestModePrameter.sensitivityPoint = algorithm.ISNaNorIfinity(MyTestModePrameter.DiffCsen);
                if (MyInputParameter.IsBerQuickTest == true)
                {
                    outputParameters[0].DefaultValue = MyTestModePrameter.sensitivityPoint.ToString().Trim();
                }
                else
                {
                    outputParameters[0].DefaultValue = Math.Round(MyTestModePrameter.sensitivityPoint, 4).ToString().Trim();
                }


                //outputParameters[1].FiledName = "DiffCsenOMA(DBM)";
                //MyTestModePrameter.sensOMA = algorithm.CalculateOMA(MyTestModePrameter.DiffCsen, Convert.ToDouble(MyProcessData.OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                //outputParameters[1].DefaultValue = Math.Round(MyTestModePrameter.sensOMA, 4).ToString().Trim();
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
                MyProcessData.OSERValueArray = algorithm.StringtoArraylistDeletePunctuations(GlobalParameters.OpticalSourseERArray, tempCharArray);
            }
            if (InformationList.Length < MyInputParameter.inPutParametersNameArray.Count)
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
                    //for (byte i = 0; i < InformationList.Length; i++)
                    {

                        if (algorithm.FindFileName(InformationList, "CsenAlignRXPWR(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                              
                                MyInputParameter.CsenAlignRxPwr = temp;
                            }


                        }
                        if (algorithm.FindFileName(InformationList, "CsenStartRXPWR(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                               
                                MyInputParameter.CsenStartingRxPwr = temp;
                            }



                        }
                        if (algorithm.FindFileName(InformationList, "SearchTargetBERUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                               
                                MyInputParameter.SearchTargetBerUL = temp;
                            }
                        }
                        if (algorithm.FindFileName(InformationList, "SearchTargetBERLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                              
                                MyInputParameter.SearchTargetBerLL = temp;
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "SearchTargetRXPWRUL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                               
                                MyInputParameter.SearchTargetBerRxpowerUL = temp;
                            }


                        }
                        if (algorithm.FindFileName(InformationList, "SearchTargetRXPWRLL", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                return false;
                            }
                            else
                            {
                               
                                MyInputParameter.SearchTargetBerRxpowerLL = temp;
                            }

                        }

                        if (algorithm.FindFileName(InformationList, "CoefCsenSubStep(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                MyInputParameter.CoefCsenSubStep = 0.5;
                            }
                            else
                            {
                                
                                MyInputParameter.CoefCsenSubStep = temp;
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "CoefCsenAddStep(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                MyInputParameter.CoefCsenAddStep = 0.5;
                            }
                            else
                            {
                                
                                MyInputParameter.CoefCsenAddStep = temp;
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "IsBerQuickTest", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.Trim().ToUpper() == "0" || temp.Trim().ToUpper() == "FALSE")
                            {
                                MyInputParameter.IsBerQuickTest = false;
                            }
                            else
                            {
                                MyInputParameter.IsBerQuickTest = true;
                            }

                        }
                        if (algorithm.FindFileName(InformationList, "IsOptSourceUnitOMA", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.Trim().ToUpper() == "0" || temp.Trim().ToUpper() == "FALSE")
                            {
                                MyInputParameter.IsOpticalSourceUnitOMA = false;
                            }
                            else
                            {
                                MyInputParameter.IsOpticalSourceUnitOMA = true;
                            }

                        }
                        //SearchTargetBerStep
                        //if (algorithm.FindFileName(InformationList, "SEARCHTARGETSTEP", out index))
                        //{
                        //    double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        //    if (double.IsInfinity(temp) || double.IsNaN(temp))
                        //    {
                        //        logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                        //        MyInputParameter.SearchTargetBerStep = 0.5;
                        //    }
                        //    else
                        //    {
                        //        if (temp < 0)
                        //        {
                        //            MyInputParameter.SearchTargetBerStep = 0.5;
                        //        }
                        //        MyInputParameter.SearchTargetBerStep = temp;
                        //    }
                        //}
                        //MyInputParameter.SearchTargetBerMethod
                        if (algorithm.FindFileName(InformationList, "SearchTargetBerMethod", out index))
                        {
                            byte temp = Convert.ToByte(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                MyInputParameter.SearchTargetBerMethod = 0;
                            }
                            else
                            {
                                MyInputParameter.SearchTargetBerMethod = temp;
                            }
                        }

                        //SearchTargetBerStep
                        if (algorithm.FindFileName(InformationList, "SearchTargetBerStep", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                MyInputParameter.FiberLoss = 1;
                            }
                            else
                            {
                                MyInputParameter.SearchTargetBerStep = temp;
                            }
                        }
                        if (algorithm.FindFileName(InformationList, "FiberLoss", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                MyInputParameter.FiberLoss = 1;
                            }
                            else
                            {
                                MyInputParameter.FiberLoss = temp;
                            }
                        }
                    }

                }
                if (MyInputParameter.SearchTargetBerUL <= MyInputParameter.SearchTargetBerLL || MyInputParameter.SearchTargetBerRxpowerUL <= MyInputParameter.SearchTargetBerRxpowerLL)
                {
                    logoStr += logger.AdapterLogString(4, "inputData is wrong!");
                    return false;
                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }
        protected double SerchTargetPoint()
        {
            byte i = 0;
            double currentCense = 0;
            double attValue = MyInputParameter.CsenStartingRxPwr;
            do
            {
                MyEqupment.pATT.AttnValue(attValue.ToString());
                currentCense = MyEqupment.pED.RapidErrorRate(1);
                if (currentCense < MyInputParameter.SearchTargetBerLL)
                {
                    attValue += MyInputParameter.SearchTargetBerStep;
                }
                else
                {
                    attValue -= MyInputParameter.SearchTargetBerStep;
                }
                i++;

            } while (i < 20 && (currentCense > MyInputParameter.SearchTargetBerUL || currentCense < MyInputParameter.SearchTargetBerLL));

            return attValue;

        }
        protected bool SerchCoefPointsTarget1E3()
        {
            byte i = 0;
            double currentBer = 0;
            //AttenPoints[]=
           
             double startAttValue=MyInputParameter.CsenStartingRxPwr;
            do
            {
                MyEqupment.pATT.AttnValue(startAttValue.ToString());
                currentBer = MyEqupment.pED.RapidErrorRate();
                if (currentBer != 0)
                {
                  // MyProcessData.AttenPoints.Add(startAttValue);
                   MyProcessData.attPoints.Add(startAttValue);
                   MyProcessData.berPoints.Add(startAttValue);
              
                }
              //  if (currentBer > targetBer)
                    if (currentBer > MyTestModePrameter.ber_erp)
                {
                    startAttValue += MyInputParameter.CoefCsenAddStep;
                   // MyInputParameter.CoefCsenAddStep;
                }
                else
                {
                    startAttValue -= MyInputParameter.CoefCsenSubStep;
                }
                i++;

            } while (i < 8 && (currentBer < (MyTestModePrameter.ber_erp * 1E-1)));

            return true;
        }
        protected bool SerchCoefPoints(double inputPower)// 需要一个输入参数
        {


           

            byte i = 0;
            double terminativeBer = 0;
            bool terminalFlag = false;
            double currentBer = 0;

            double startAttValue = inputPower;

            MyBackGroundLog.Clear();
            //for (i = 0; i < MyBackGroundLog.Count; i++)
            //{
            //    logoStr += logger.AdapterLogString(1, MyBackGroundLog.Dequeue());

            //}

            if(MyTestModePrameter.ber_erp < MyInputParameter.SearchTargetBerLL)// 拟合的目标为小误码
            {

                if (MyTestModePrameter.ber_erp > 1E-12)//如果是大于E-12 ,并且比第一点的下限要小 ,那么就以目标值为下限
                {
                    terminativeBer = MyTestModePrameter.ber_erp;
                }
                else////如果是小于E-12 ,并且比第一点的下限要小 ,那么就以-为 11下限
                {
                    terminativeBer = 1E-11;
                }
            }
            else if (MyTestModePrameter.ber_erp > MyInputParameter.SearchTargetBerUL)// 拟合的目标为大误码
            {
                // terminativeBer = targetBer * 1E-1;
                terminativeBer = MyTestModePrameter.ber_erp;
            }


            do
            {
                SetInputPower(startAttValue);

                Thread.Sleep(1000);
                double TempRxDmiPower = dut.ReadDmiRxp();

                currentBer = MyEqupment.pED.RapidErrorRate();


                MyBackGroundLog.Enqueue("RxInputPower=" + startAttValue + "->RxDmiPower=" + TempRxDmiPower.ToString() + "->ErrorRate=" + currentBer);
              
                if (MyTestModePrameter.ber_erp < MyInputParameter.SearchTargetBerLL)
                {
                    terminalFlag = currentBer > terminativeBer;
                }
                else if (MyTestModePrameter.ber_erp > MyInputParameter.SearchTargetBerUL)
                {
                    terminalFlag = currentBer < terminativeBer;

                }
                if (terminalFlag == true && currentBer != 0)
                {
                   MyProcessData.attPoints.Add(startAttValue);
                   MyProcessData.berPoints.Add(currentBer);
                }

                if (currentBer > MyTestModePrameter.ber_erp)
                {
                    startAttValue += MyInputParameter.CoefCsenAddStep;
;
                }
                else
                {
                    startAttValue -= MyInputParameter.CoefCsenSubStep;
                }
                i++;

            } while (i < 8 && terminalFlag);


            for (i = 0; i < MyBackGroundLog.Count; i++)
            {
                logoStr += logger.AdapterLogString(1, MyBackGroundLog.Dequeue());

            }
                return true;
        }
        protected bool deleteErrBerPoints(ArrayList inputARR1, ArrayList inputARR2)
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

        protected double StepSearchTargetPoint()
        {


            double currentCense = 0;
            byte Rcount = 0;

            double CurrentInputPower = MyInputParameter.CsenStartingRxPwr;

            while (CurrentInputPower < MyInputParameter.SearchTargetBerRxpowerUL && CurrentInputPower > MyInputParameter.SearchTargetBerRxpowerLL && Rcount <= 20)
            {

                SetInputPower(CurrentInputPower);

                //double TempRxDmiPower = dut.ReadDmiRxp();

                MyProcessData.serchAttPoints.Add(CurrentInputPower);
               // MyProcessData.serchRxDmidPoints.Add(TempRxDmiPower);
                currentCense = MyEqupment.pED.RapidErrorRate();
                MyProcessData.serchBerPoints.Add(currentCense);

                if (currentCense < MyInputParameter.SearchTargetBerLL)// 误码太小->光太大->减小光
                {
                    CurrentInputPower -= MyInputParameter.SearchTargetBerStep;
                    Rcount++;
                }
                else if (currentCense > MyInputParameter.SearchTargetBerUL)// 误码太大->光太小->加大入射光
                {
                    CurrentInputPower += MyInputParameter.SearchTargetBerStep;
                    Rcount++;
                }
                else
                    return CurrentInputPower;

            }
            return -10000;

        }

     
        private bool SetInputPower(double InputPower)
        {

            MyEqupment.pATT.SetAttnValue(MyTestModePrameter.LightSource - InputPower,1);

            return true;
        }

        protected double binarySearchTargetPoint()
        {
           
            double currentCense = 0;
            byte count = 0;


            double UpPower = MyInputParameter.SearchTargetBerRxpowerUL;
            double LowPower = MyInputParameter.SearchTargetBerRxpowerLL;

            double CurrentInputPower = (UpPower + LowPower) / 2;

            SetInputPower(CurrentInputPower);

           

            double TempRxDmiPower = dut.ReadDmiRxp();
            currentCense = MyEqupment.pED.RapidErrorRate();

            MyProcessData.serchAttPoints.Add(CurrentInputPower);
            MyProcessData.serchRxDmidPoints.Add(TempRxDmiPower);
          
            MyProcessData.serchBerPoints.Add(currentCense);

            logoStr += logger.AdapterLogString(1, "RxInputPower=" + ((MyInputParameter.SearchTargetBerRxpowerUL + MyInputParameter.SearchTargetBerRxpowerLL) / 2).ToString());
            logoStr += logger.AdapterLogString(1, "CurrentErrorRate=" + currentCense.ToString());

            while (MyInputParameter.SearchTargetBerRxpowerLL <= MyInputParameter.SearchTargetBerRxpowerUL && count <= 20)
            {
                if (Math.Abs(MyInputParameter.SearchTargetBerRxpowerLL - MyInputParameter.SearchTargetBerRxpowerUL) < 0.2) break;

                double mid = (UpPower + LowPower) / 2;
              
                SetInputPower(mid);

                TempRxDmiPower = dut.ReadDmiRxp();
                currentCense = MyEqupment.pED.RapidErrorRate();

                MyProcessData.serchAttPoints.Add(mid);
                MyProcessData.serchRxDmidPoints.Add(TempRxDmiPower);
               
                MyProcessData.serchBerPoints.Add(currentCense);

                if (currentCense < MyInputParameter.SearchTargetBerLL)
                {
                  
                    LowPower = mid;
                    count++;
                }
                else if (currentCense > MyInputParameter.SearchTargetBerUL)
                {
                    UpPower = mid;
                
                    count++;
                }
                else
                    return mid;
            }
            return -10000;

        }

        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                procData = new TestModeEquipmentParameters[4];
                procData[0].FiledName = "SearTargetBerRxPowerArray";
                procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(MyProcessData.serchAttPoints, ",");
                procData[1].FiledName = "SearTargetBerArray";
                procData[1].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(MyProcessData.serchBerPoints, ",");
                procData[2].FiledName = "CurvingRxPowerArray";
                procData[2].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(MyProcessData.attPoints, ",");
                procData[3].FiledName = "CurvingBerArray";
                procData[3].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(MyProcessData.berPoints, ",");
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }



        }
        protected void QuickTest()
        {
            try
            {
                MyEqupment.pATT.AttnValue(MyInputParameter.CsenStartingRxPwr.ToString());
                double sensitivity = MyEqupment.pED.RapidErrorRate();
                logoStr += logger.AdapterLogString(1, "SetAtten=" + MyInputParameter.CsenStartingRxPwr.ToString());
                logoStr += logger.AdapterLogString(1, "QUICBER=" + sensitivity.ToString());
                if (sensitivity >= 1)
                {
                    MyTestModePrameter.sensitivityPoint = 1;
                    AnalysisOutputParameters(outputParameters);
                    AnalysisOutputProcData(procData);
                    logger.FlushLogBuffer();
                    return;
                }

                {   
                    if (sensitivity <= MyTestModePrameter.ber_erp)
                    {
                        MyTestModePrameter.sensitivityPoint = MyInputParameter.CsenStartingRxPwr;
                    }
                    else
                    {
                        MyTestModePrameter.sensitivityPoint = sensitivity;
                        logoStr += logger.AdapterLogString(4, "AttPoint=" + MyInputParameter.CsenStartingRxPwr.ToString() + MyTestModePrameter.ber_erp.ToString());

                    }
                }
                logoStr += logger.AdapterLogString(1, "sensitivityPoint= " + MyTestModePrameter.sensitivityPoint.ToString());
                AnalysisOutputParameters(outputParameters);
                AnalysisOutputProcData(procData);
                logger.FlushLogBuffer();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected bool SerchTargetBer()
        {
            try
            {

                logoStr += logger.AdapterLogString(0, "Step5...SerchTargetPoint");

                switch (MyInputParameter.SearchTargetBerMethod)
                {
                    case 1:
                        MyTestModePrameter.FirstBerPointRxPower = StepSearchTargetPoint();

                        break;
                    default:
                        MyTestModePrameter.FirstBerPointRxPower = binarySearchTargetPoint();
                        break;

                }

                if (MyTestModePrameter.FirstBerPointRxPower == -10000)
                {
                    return false;
                }

                logoStr += logger.AdapterLogString(1, "FirstBerPointRxPower=" + MyTestModePrameter.FirstBerPointRxPower.ToString());
                for (byte i = 0; i < MyProcessData.serchAttPoints.Count; i++)
                {
                    logoStr += logger.AdapterLogString(1, "serchAttPoints[ " + i.ToString() + "]" + MyProcessData.serchAttPoints[i].ToString() + "  " + "serchRxDmidPoints[ " + i.ToString() + "]"  + "serchBerPoints[ " + i.ToString() + "]" + MyProcessData.serchBerPoints[i].ToString());
                }
              
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected void GettingCurvingPointsandFitting()
        {
            try
            {
                double intercept;
                double slope;
                byte tmepCount = 0;

               MyProcessData.attPoints.Clear();
               MyProcessData.berPoints.Clear();

                logoStr += logger.AdapterLogString(0, "Step6...SerchCoefPoints");
                {
                    SerchCoefPoints(MyTestModePrameter.FirstBerPointRxPower);

                    tmepCount = (byte)Math.Min(MyProcessData.attPoints.Count, MyProcessData.berPoints.Count);
                    double[] TempRxPower = new double[tmepCount];
                    double[] tempberPoints = new double[tmepCount];

                    for (byte i = 0; i < tmepCount; i++)
                    {
                        TempRxPower[i] = double.Parse(MyProcessData.attPoints[i].ToString());
                        tempberPoints[i] = double.Parse(MyProcessData.berPoints[i].ToString());

                        MyBackGroundLog.Enqueue("inputPower[ " + i.ToString() + "]" + MyProcessData.attPoints[i].ToString() + "  " + "berPoints[ " + i.ToString() + "]" + MyProcessData.berPoints[i].ToString());    
                    }
                    algorithm.LinearRegression(algorithm.Getlog10(algorithm.GetNegative(algorithm.Getlog10(tempberPoints))), TempRxPower, out slope, out intercept);
                    MyTestModePrameter.sensitivityPoint = slope + (intercept * System.Math.Log10(System.Math.Log10(MyTestModePrameter.ber_erp) * (-1)));
                
                }

                for (int i=0;i<MyBackGroundLog.Count;i++)
                {
                    logoStr += logger.AdapterLogString(1, MyBackGroundLog.Dequeue());
             
                }
                //MyBackGroundLog.Count
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

        protected void GlobalBER_EXPtoRealBer()
        {
            try
            {
                switch (GlobalParameters.BER_exp)
                {
                    case (byte)BER_ERP.NegativeZero:
                        {
                            MyTestModePrameter.ber_erp = 5E-5;
                            break;
                        }
                    case (byte)BER_ERP.NegativeOne:
                        {
                            MyTestModePrameter.ber_erp = 1E-1;
                            break;
                        }
                    case (byte)BER_ERP.NegativeTwo:
                        {
                            MyTestModePrameter.ber_erp = 1E-2;
                            break;
                        }
                    case (byte)BER_ERP.NegativeThree:
                        {
                            MyTestModePrameter.ber_erp = 1E-3;
                            break;
                        }
                    case (byte)BER_ERP.NegativeFour:
                        {
                            MyTestModePrameter.ber_erp = 1E-4;
                            break;
                        }
                    case (byte)BER_ERP.NegativeFive:
                        {
                            MyTestModePrameter.ber_erp = 1E-5;
                            break;
                        }
                    case (byte)BER_ERP.NegativeSix:
                        {
                            MyTestModePrameter.ber_erp = 1E-6;
                            break;
                        }
                    case (byte)BER_ERP.NegativeSeven:
                        {
                            MyTestModePrameter.ber_erp = 1E-7;
                            break;
                        }
                    case (byte)BER_ERP.NegativeEight:
                        {
                            MyTestModePrameter.ber_erp = 1E-8;
                            break;
                        }
                    case (byte)BER_ERP.NegativeNine:
                        {
                            MyTestModePrameter.ber_erp = 1E-9;
                            break;
                        }
                    case (byte)BER_ERP.NegativeTen:
                        {
                            MyTestModePrameter.ber_erp = 1E-10;
                            break;
                        }
                    case (byte)BER_ERP.NegativeEleven:
                        {
                            MyTestModePrameter.ber_erp = 1E-11;
                            break;
                        }
                    case (byte)BER_ERP.NegativeTwelve:
                        {
                            MyTestModePrameter.ber_erp = 1E-12;
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }// 无效  Leo 2016-11-18

        #endregion


    }
}
