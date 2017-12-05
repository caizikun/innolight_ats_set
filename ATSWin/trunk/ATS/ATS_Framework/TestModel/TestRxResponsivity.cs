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
    public class TestRxResponsivity : TestModelBase
    {
        #region Attribute
        private double Ber = 2;
        private double DmiRxPower, DmiTxPower;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private Powersupply pPS;
        private ErrorDetector pED;

        private Attennuator  pAtt;
        private bool flagSingleTest = false;
        private double Vref;
        private double Rref;
        private double Resolution;
        private double Ratio;
        private byte ReadRxPowerAdcCount;

        private bool flagReadTxpowerDmi = false;
        private bool flagReadRxpowerDmi = false;
        private DataTable DtTestData;
        private DataTable DtSamplingData;
        private double LastTemp = -100;

        private double RxInputPower;

        private double Responsivity = -100;

        #endregion

        #region Method

        public TestRxResponsivity(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;
            DtTestData = new DataTable();
            DtSamplingData = new DataTable();

            DtTestData.Columns.Add("Temp");
            DtTestData.Columns.Add("Channel");
            DtTestData.Columns.Add("Vcc");
            DtTestData.Columns.Add("Response");


            DtSamplingData.Columns.Add("Channel");
            DtSamplingData.Columns.Add("RxInputPower");
            DtSamplingData.Columns.Add("RxPowerADC");


            inPutParametersNameArray.Clear();

            inPutParametersNameArray.Add("SingleThread");
            inPutParametersNameArray.Add("RxInputPower(DBM)");
            inPutParametersNameArray.Add("Rref");
            inPutParametersNameArray.Add("Resolution");
            inPutParametersNameArray.Add("Vref");
            inPutParametersNameArray.Add("ReadRxPowerAdcCount");
            inPutParametersNameArray.Add("Ratio");
           
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
            Ber = 1;
            logger.FlushLogBuffer();
            logoStr = "";
            if (!Test())
            {
                OutPutandFlushLog();
                return false;

            }
            else
            {
                OutPutandFlushLog();
                return true;
            }

        }

        private bool MultChannelTest()//MultChannelTest
        {

        

            ushort tempRxPowerADC=0;
           
           // double RxResponsivity;

          

            pAtt.SetAllChannnel_RxOverLoad(Convert.ToSingle(RxInputPower));

            Thread.Sleep(2000);


             for (int j = 1; j < 5; j++)
                {
                    dut.ChangeChannel(j.ToString());

                    for (byte i = 0; i < ReadRxPowerAdcCount; i++)
                    {
                   
                        Thread.Sleep(200);

                       
                        dut.ReadRxpADC(out tempRxPowerADC);

                        logoStr += logger.AdapterLogString(1, "Channnel=" + j.ToString() +  " tempRxPowerADC:" + tempRxPowerADC.ToString());

                    }

                    Responsivity = algorithm.CalculateRxResponsivity(Convert.ToDouble(RxInputPower), Convert.ToDouble(tempRxPowerADC), Vref, Rref, Resolution,Ratio);

                    logoStr += logger.AdapterLogString(1, "Channel=" + j + " Responsivity= " + Responsivity.ToString());

                    DataRow drResponsivity = DtTestData.NewRow();

                    drResponsivity["Temp"] = GlobalParameters.CurrentTemp;
                    drResponsivity["Vcc"] = GlobalParameters.CurrentVcc;
                    drResponsivity["Channel"] = j;
                    drResponsivity["Response"] = Responsivity;
                    DtTestData.Rows.Add(drResponsivity);

            }

            return true;
        }
        private bool SingleChannelTest()
        {
          
            ushort tempRxPowerADC=0 ;

            pAtt.AttnValue(RxInputPower.ToString(), 1);

            Thread.Sleep(2000);

            for (byte i = 0; i < ReadRxPowerAdcCount; i++)
            {
               Thread.Sleep(200);
                dut.ReadRxpADC(out tempRxPowerADC);
               logoStr += logger.AdapterLogString(1,"tempRxPowerADCArray:" + tempRxPowerADC.ToString() );
            }

            Responsivity = algorithm.CalculateRxResponsivity(Convert.ToDouble(RxInputPower), Convert.ToDouble(tempRxPowerADC), Vref, Rref, Resolution, Ratio);

            logoStr += logger.AdapterLogString(1, "Responsivity= " + Responsivity.ToString());

            return true;
        }
        public override bool Test()
        {
            if (AnalysisInputParameters(inputParameters) == false)
            {
                return false;
            }
            if (!CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON))) return false;//开启APC

            if (ReadyData())
            {
                return true;//已经填充好了数据,后面不用测试

            }
            else// 没提前准备好数据,需要实际测量
            {

                logoStr += logger.AdapterLogString(0, "Step3...SetAttenValue");
                logoStr += logger.AdapterLogString(0, "Step4...AutoAlaign");



                logger.FlushLogBuffer();

                if (flagSingleTest)
                {
                    SingleChannelTest();
                }
                else
                {
                    MultChannelTest();
                }
            }
            return true;
        }
        private bool ReadyData()
        {
            if (flagSingleTest)
            {
                return false;
            }
            else
            {

                if (GlobalParameters.CurrentTemp != LastTemp)
                {
                    DtTestData.Clear();
                    LastTemp = GlobalParameters.CurrentTemp;
                }
                DataRow[] drArray;

                string SelectCondition = "Temp=" + GlobalParameters.CurrentTemp + " and Vcc=" + GlobalParameters.CurrentVcc + " and Channel=" + GlobalParameters.CurrentChannel;
                drArray = DtTestData.Select(SelectCondition);
                if (drArray.Length == 0)
                {
                    return false;
                }
                if (drArray.Length == 1)
                {
                    Responsivity = Convert.ToDouble(drArray[0]["Response"]);
                    logger.AdapterLogString(0, "");
                }


                return true;
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
                selectedEquipList.Add("DUT", dut);
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

                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(2, GlobalParameters.CurrentChannel);
                    }
                }
                pPS = (Powersupply)selectedEquipList["POWERSUPPLY"];
                pAtt = (Attennuator)selectedEquipList["ATTEN"];
                if (pPS != null && pAtt != null)
                {
                    isOK = true;

                }
                else
                {
                    if (pAtt == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
                    }
                    if (pPS == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    OutPutandFlushLog();
                    isOK = false;
                }
                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);
                    return isOK;
                }
                return isOK;
            }
        }

        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                outputParameters = new TestModeEquipmentParameters[1];
                outputParameters[0].FiledName = "RxResponsivity";
                Responsivity = algorithm.ISNaNorIfinity(Responsivity);
                outputParameters[0].DefaultValue = Responsivity.ToString().Trim();


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
                bool isParametersComplete = false;
                for (byte i = 0; i < inPutParametersNameArray.Count; i++)
                {
                    if (algorithm.FindFileName(InformationList, inPutParametersNameArray[i].ToString(), out index) == false)
                    {
                        logoStr += logger.AdapterLogString(4, inPutParametersNameArray[i].ToString() + "is not exist");
                        isParametersComplete = false;
                        return isParametersComplete;
                    }
                    else
                    {
                        isParametersComplete = true;
                        continue;
                    }

                }
                if (isParametersComplete)
                {
                   

                        if (algorithm.FindFileName(InformationList, "SingleThread", out index))
                        {
                            flagSingleTest = Convert.ToBoolean(InformationList[index].DefaultValue);
                        }
                  

                        if (algorithm.FindFileName(InformationList, "RxInputPower(DBM)", out index))
                        {
                            RxInputPower = Convert.ToDouble(InformationList[index].DefaultValue);
                        }

                        if (algorithm.FindFileName(InformationList, "Rref", out index))
                        {
                            //string s = InformationList[index].DefaultValue;
                            decimal dData = Convert.ToDecimal(Decimal.Parse(InformationList[index].DefaultValue, System.Globalization.NumberStyles.Float));

                            Rref = Convert.ToDouble(dData);

                         }
                        if (algorithm.FindFileName(InformationList, "Resolution", out index))
                        {
                            Resolution = Convert.ToDouble(InformationList[index].DefaultValue);
                        }
                        if (algorithm.FindFileName(InformationList, "Vref", out index))
                        {
                            Vref = Convert.ToDouble(InformationList[index].DefaultValue);
                        }
                        if (algorithm.FindFileName(InformationList, "ReadRxPowerAdcCount", out index))
                        {
                            ReadRxPowerAdcCount = Convert.ToByte(InformationList[index].DefaultValue);
                        }
                        if (algorithm.FindFileName(InformationList, "Ratio", out index))
                        {
                            Ratio = Convert.ToByte(InformationList[index].DefaultValue);
                        }
                    
                    //inPutParametersNameArray.Add("SINGLEORMULTI");
                    //inPutParametersNameArray.Add("RxInputPower(DBM)");
                    //inPutParametersNameArray.Add("Rref");
                    //inPutParametersNameArray.Add("Resolution");
                    //inPutParametersNameArray.Add("Vref");
                    //inPutParametersNameArray.Add("ReadRxPowerAdcCount");
                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }

        }
       
        
        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
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
