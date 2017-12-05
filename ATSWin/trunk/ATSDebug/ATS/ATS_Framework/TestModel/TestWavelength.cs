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
    public class TestWavelength : TestModelBase
    {
        private Powersupply pPS;
        private Spectrograph pWaveMeter;


        private double Wavelength=-1;
        private double SpanWide = -1;
        private double SMSR = -1;
        private double OSNR = -1;

        private DataTable TestDatad = new DataTable();

        private ArrayList inPutParametersNameArray = new ArrayList();

        private bool flagNormalTest = true;// true=Wavelength ;False= ALL

        public TestWavelength(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;

            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("IsNormalTest");

            TestDatad.Columns.Add("ItemName");

            TestDatad.Columns.Add("ItemValue");

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

        protected override bool StartTest()
        {



            //---------------------------------
            pWaveMeter.analysisMode = 2;//单模
            //-------------------------------

            TestDatad.Clear();

         
         
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
     
     
        public override bool Test()
        {

           



            if (AnalysisInputParameters(inputParameters) == false)
            {
                return false;
            }
            if (!CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON))) return false;//开启APC

            if (!pWaveMeter.StartSweep())
            {
                fitDataTable("Wavelength", Wavelength.ToString());



                if (!flagNormalTest)//全测试
                {

                    fitDataTable("SpanWide", SpanWide.ToString());

                    if (pWaveMeter.analysisMode == 2)//单模
                    {

                        fitDataTable("SMSR",SMSR.ToString());
                        fitDataTable("OSNR", OSNR.ToString());
                    }
                }


                return false;
            }


            Wavelength = pWaveMeter.GetCenterWavelength();
            fitDataTable("Wavelength", Wavelength.ToString());
            
             

            if (!flagNormalTest)//全测试
            {
                SpanWide = pWaveMeter.GetSpectralWidth();
                fitDataTable("SpanWide", SpanWide.ToString());
               
                if (pWaveMeter.analysisMode == 2)//单模
                {
                    SMSR = pWaveMeter.GetSMSR();
                    fitDataTable("SMSR",SMSR.ToString());
                    OSNR = pWaveMeter.GetOSNR();
                    fitDataTable("OSNR", OSNR.ToString());
                }
            }
            return true;
        }
    
        private bool fitDataTable(string ItemName,string ItemValue)
        {
            try
            {
                DataRow dr = TestDatad.NewRow();

                dr["ItemName"] = ItemName;
                dr["ItemValue"] = ItemValue;
                TestDatad.Rows.Add(dr);

                return true;
            }
            catch
            { 
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
            pPS = null;
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
                  
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        pPS = (Powersupply)tempValues[i];
                    }
                    if (tempKeys[i].ToUpper().Contains("SPECTROGRAPH"))
                    {
                        selectedEquipList.Add("WAVEMETER", tempValues[i]);
                        pWaveMeter = (Spectrograph)tempValues[i];
                    }
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }
                if (pPS != null && pWaveMeter!=null)
                {
                    isOK = true;

                }
                else
                {
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
                //outputParameters = new TestModeEquipmentParameters[1];
                //outputParameters[0].FiledName = "Wavelength(nm)";
                //outputParameters[0].DefaultValue = Wavelength.ToString();

                outputParameters = new TestModeEquipmentParameters[TestDatad.Rows.Count];

                for (int i = 0; i < TestDatad.Rows.Count;i++ )
                {
                    outputParameters[i].FiledName = TestDatad.Rows[i]["ItemName"].ToString();
                    outputParameters[i].DefaultValue = TestDatad.Rows[i]["ItemValue"].ToString();
                }
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
                    if (algorithm.FindFileName(InformationList, "IsNormalTest", out index))
                    {

                        flagNormalTest = Convert.ToBoolean(InformationList[index].DefaultValue);
                    }
                    else
                    {
                        return false;
                    }

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
    }
}
