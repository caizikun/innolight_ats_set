using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace ATS_Framework
{
    public struct TestPolarityStruct
    {
        private string data;

        private int length;

        public String Data
        {
            set
            {
                this.data = value;
            }
            get
            {
                return this.data;
            }
        }

        public int Length
        {
            set
            {
                this.length = value;
            }
            get
            {
                return this.length;
            }
        }

        public TestPolarityStruct(int patternLength, string pattern)
        {
            length = patternLength;
            data = pattern;            
        }
    }

   public class TestPolarity : TestModelBase
    {
        private struct PolaritySpec
        {
            public double TxPolaritySpecMax { get; set; }

            public double TxPolaritySpecMin { get; set; }

            public double TxPolarityTypcalValue { get; set; }

            public double RxPolaritySpecMax { get; set; }

            public double RxPolaritySpecMin { get; set; }

            public double RxPolarityTypcalValue { get; set; }
        }

        private ArrayList inParaList = new ArrayList();

        private Powersupply supply;

        private Scope scope;

        private PPG ppg;

        private TestPolarityStruct myStruct;

        private int txPolarity = 1;//pass: 0; fail: 1

        private int rxPolarity = 1;//pass: 0; fail: 1

        private PolaritySpec mySpec = new PolaritySpec();

        private int count = 0;

        public TestPolarity(DUT inDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inDut;

            inParaList.Clear();

            inParaList.Add("PRBS");            
        }

        protected override bool CheckEquipmentReadiness()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].bReady)
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool PrepareTest()
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
                if (!selectedEquipList.Values[i].Configure())
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool AssembleEquipment()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].OutPutSwitch(true))
                {
                    return false;
                }
            }
            return true;
        }

        public override bool SelectEquipment(EquipmentList equipmentList)
        {
            selectedEquipList.Clear();

            if (equipmentList.Count == 0)
            {
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;

                for (byte i = 0; i < equipmentList.Count; i++)
                {
                    if (equipmentList.Keys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        supply = (Powersupply)equipmentList.Values[i];
                        isOK = true;
                    }

                    if (equipmentList.Keys[i].ToUpper().Contains("SCOPE"))
                    {
                        scope = (Scope)equipmentList.Values[i];
                    }

                    if (equipmentList.Keys[i].ToUpper().Contains("PPG"))
                    {
                        ppg = (PPG)equipmentList.Values[i];
                        isOK = true;
                    }

                    if (equipmentList.Keys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        equipmentList.Values[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }

                if (supply != null && scope != null)
                {
                    isOK = true;
                }
                else
                {
                    if (supply == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }

                    if (scope == null)
                    {
                        logoStr += logger.AdapterLogString(3, "SCOPE =NULL");
                    }

                    if (ppg == null)
                    {
                        logoStr += logger.AdapterLogString(3, "PPG =NULL");
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

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";

            if (!AnalysisInputParameters(inputParameters))
            {
                return false;
            }

            if (!LoadPNSpec())
            {
                return false;
            }

            if (PrepareEnvironment(selectedEquipList) == false)
            {
                OutPutandFlushLog();
                return false;
            }

            if (supply != null)
            {
                //CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));

                bool result = Test();
                ppg.RecallPrbsLength();

                OutPutandFlushLog();

                return result;
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");                
                return false;
            }            
        }

        protected bool PrepareEnvironment(EquipmentList aEquipList)
        {

            if (scope != null)
            {
                if (scope.SetMaskAlignMethod(1) &&
                scope.SetMode(0) &&
                scope.MaskONOFF(false) &&
                scope.SetRunTilOff() &&
                scope.RunStop(true) &&
                scope.OpenOpticalChannel(true) &&
                scope.RunStop(true) &&
                scope.ClearDisplay() &&
                scope.AutoScale()
                )
                {
                    logoStr += logger.AdapterLogString(1, "PrepareEnvironment OK!");
                    return true;
                }
                else
                {
                    logoStr += logger.AdapterLogString(4, "PrepareEnvironment Fail!");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }        

        private bool LoadPNSpec()
        {
            try
            {
                double max, min, target;
                algorithm.GetSpec(specParameters, "TxPolarity", 0, out max, out target, out min);

                mySpec.TxPolaritySpecMax = max;
                mySpec.TxPolaritySpecMin = min;
                mySpec.TxPolarityTypcalValue = target;

                algorithm.GetSpec(specParameters, "RxPolarity", 0, out max, out target, out min);

                mySpec.RxPolaritySpecMax = max;
                mySpec.RxPolaritySpecMin = min;
                mySpec.RxPolarityTypcalValue = target;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Test()
        {
            try
            {


                ppg.ConfigurePatternType(2);
                ppg.DataPattern_DataLength(myStruct.Length.ToString());
                ppg.DataPattern_SetPatternData("#H0", "#H" + (myStruct.Length - 1), "B" + myStruct.Data.ToString());
                scope.SetMode(1);

                count++;
                DialogResult dialogResult;
                if (count == 1)
                {
                    dialogResult = MessageBox.Show("Please unplug and plug DUT", "Infomation", MessageBoxButtons.OK);

                    if (dialogResult != DialogResult.OK)
                    {
                        return false;
                    }
                }

                //scope.MyIO.WriteString(":TIMebase:PTImebase:STATe OFF");
                //scope.MyIO.WriteString(":TRIGger:SOURce FPANel");
                //scope.MyIO.WriteString(":TRIGger:PLOCK ON");
                //scope.MyIO.WriteString("*OPC?");

                scope.PtimebaseStatue(false);
                scope.TriggerSourceFpanel();
                scope.TriggerPlock(true);

                Thread.Sleep(50);
                scope.AutoScale();
               // scope.setcommand(":TIMebase:PTImebase:STATe OFF");

                dialogResult = MessageBox.Show("Begin to test polarity?", "Infomation", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    scope.pglobalParameters = GlobalParameters;

                    txPolarity = TestTxPolarity();

                    rxPolarity = TestRxPolarity();
                }
                else
                {
                    return false;
                }

                if ((txPolarity <= mySpec.TxPolaritySpecMax && txPolarity >= mySpec.TxPolaritySpecMin) &&
                (rxPolarity <= mySpec.RxPolaritySpecMax && rxPolarity >= mySpec.RxPolaritySpecMin))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private int TestTxPolarity()
        {            
            scope.OpenOpticalChannel(true);
            
            Thread.Sleep(100);
            scope.AutoScale();
            Thread.Sleep(100);            

            DialogResult dialogResult = MessageBox.Show("Please check Tx polarity on the scope screen! Is result right?", "Result", MessageBoxButtons.YesNo);
            //scope.SavaScreen(@"Screen\Tx\");//
            scope.SavaScreen(scope.pglobalParameters.StrPathPolarityEyeDiagram + @"Tx\");//

            if (dialogResult == DialogResult.Yes)
            {
                return 1;
            }
            else
            {
                return 0;
            }                
        }

        private int TestRxPolarity()
        {
            scope.OpenOpticalChannel(false);
            scope.AutoScale();
            Thread.Sleep(100);

            DialogResult dialogResult = MessageBox.Show("Please check Rx polarity on the scope screen! Is result right?", "Result", MessageBoxButtons.YesNo);
            //scope.SavaScreen(@"Screen\Rx\");//
            scope.SavaScreen(scope.pglobalParameters.StrPathPolarityEyeDiagram + @"Rx\");//

            if (dialogResult == DialogResult.Yes)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] infoList)
        {
            logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");

            if (infoList.Length < inParaList.Count)
            {
                logoStr += logger.AdapterLogString(4, "InputParameters are not enough!");
                return false;
            }
            else
            {
                string outString;
                bool isGetten = algorithm.Getconf(infoList, "Length", true, out outString);

                int getLength = 0;
                isGetten = isGetten && Int32.TryParse(outString, out getLength);
                if (!isGetten)
                {
                    logoStr += logger.AdapterLogString(4, "Length is wrong!");
                    return false;
                }

                isGetten = algorithm.Getconf(infoList, "Data", true, out outString);
                if (outString=="")
                {
                    logoStr += logger.AdapterLogString(4, "Data is wrong!");
                    return false;
                }

                myStruct = new TestPolarityStruct(getLength, outString);                
                logoStr += logger.AdapterLogString(1, "OK!");

                return true;
            }
        }

        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] infoList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[2];

                outputParameters[0].FiledName = "TxPolarity";
                outputParameters[0].DefaultValue = txPolarity.ToString();

                outputParameters[1].FiledName = "RxPolarity";
                outputParameters[1].DefaultValue = rxPolarity.ToString();

                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
