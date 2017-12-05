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
    public struct TestRxEyeVcmiVecStruct
    {
        public double CensePoint{get;set;}
    }

    public class TestRxEyeVcmiVec : TestModelBase
    {
        private DataTable dataTable = new DataTable();

        private ArrayList inList = new ArrayList();

        private TestRxEyeVcmiVecStruct vcmiVecStruct = new TestRxEyeVcmiVecStruct();

        private Scope scope;

        private Attennuator attRx;

        private Powersupply supply;

        private double eyeHeight;

        private double amp_5;

        private double[] vcmi;

        private double vec;

        public TestRxEyeVcmiVec(DUT inDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inDut;
            logoStr = null;

            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("ItemValue");

            inList.Clear();
            inList.Add("RxInputPower");
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

        public override bool SelectEquipment(EquipmentList eqList)
        {
            selectedEquipList.Clear();
            if (eqList.Count == 0)
            {
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;
                selectedEquipList.Clear();
                IList<string> tempKeys = eqList.Keys;
                IList<EquipmentBase> tempValues = eqList.Values;
                for (byte i = 0; i < eqList.Count; i++)
                {
                    if (tempKeys[i].ToUpper().Contains("ATTEN_RX"))
                    {
                        selectedEquipList.Add("ATTEN_RX", tempValues[i]);
                        isOK = true;
                    }

                    if (tempKeys[i].ToUpper().Contains("SCOPE"))
                    {
                        selectedEquipList.Add("SCOPE", tempValues[i]);
                        isOK = true;
                    }

                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                }

                attRx = (Attennuator)selectedEquipList["ATTEN_RX"];
                scope = (Scope)selectedEquipList["SCOPE"];
                supply = (Powersupply)selectedEquipList["POWERSUPPLY"];

                if (attRx != null && scope != null && supply != null)
                {
                    isOK = true;
                }
                else
                {
                    if (attRx == null)
                    {
                        logoStr += logger.AdapterLogString(3, "attRx =NULL");
                    }

                    if (scope == null)
                    {
                        logoStr += logger.AdapterLogString(3, "scope =NULL");
                    }

                    if (supply == null)
                    {
                        logoStr += logger.AdapterLogString(3, "supply =NULL");
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

        protected override bool StartTest()
        {
            dataTable.Clear();
            logger.FlushLogBuffer();
            logoStr = "";
            if (!Test())
            {
                //OutPutandFlushLog();
                return false;
            }
            else
            {
                //OutPutandFlushLog();
                return true;
            }
        }

        public override bool Test()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            //// 是否要测试需要添加判定

            if (AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }

            if (PrepareEnvironment(selectedEquipList) == false)
            {
                OutPutandFlushLog();
                return false;
            }

            if (supply != null)
            {
                // open apc 
                CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                if (attRx != null)
                {
                    logoStr += logger.AdapterLogString(0, "Step3...Set AttValue" + Convert.ToString(vcmiVecStruct.CensePoint) + "DBM");
                    attRx.AttnValue(Convert.ToString(vcmiVecStruct.CensePoint), 1);
                }
                logoStr += logger.AdapterLogString(0, "Step4...StartTest Electricl Eye Vcmi and Vec");
                logoStr += logger.AdapterLogString(0, "VcmiVecTest");

                VcmiVecTest();

                OutPutandFlushLog();
                return true;
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                OutPutandFlushLog();
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
                  scope.OpenOpticalChannel(false) &&
                  scope.RunStop(true) &&
                  scope.ClearDisplay() &&
                  scope.AutoScale(1)
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

        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");

            if (InformationList.Length < inList.Count)
            {
                logoStr += logger.AdapterLogString(4, "InputParameters are not enough!");
                return false;
            }
            else
            {
                int index = -1;
                bool isParaComplete = true;

                if (isParaComplete)
                {
                    if (algorithm.FindFileName(InformationList, "INPUTRXPWR(DBM)", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            vcmiVecStruct.CensePoint = -20;
                        }
                        else
                        {
                            if (temp > 0)
                            {
                                temp = -temp;
                            }
                            vcmiVecStruct.CensePoint = temp;
                        }
                    }
                    else
                    {
                        return false;
                    }

                    //if (algorithm.FindFileName(InformationList, "IsTestVcmi", out index))
                    //{
                    //   vcmiVecStruct.IsTestVcmi = InformationList[index].DefaultValue.ToString();
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                    //if (algorithm.FindFileName(InformationList, "IsTestVec", out index))
                    //{
                    //    vcmiVecStruct.IsTestVec = Convert.ToDouble(InformationList[index].DefaultValue);
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
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

        protected bool VcmiVecTest()
        {
            amp_5 = 0;
            eyeHeight = 0;
            vcmi = new double[2];
            vcmi[0] = 0;
            vcmi[1] = 0;
            vec = 0;

            scope.pglobalParameters = GlobalParameters;
            double[] data;
            if (scope.MeasureVEC(out data))
            {
                eyeHeight = data[0];
                amp_5 = data[1];
                vec = data[2];   
            }
            else
            {
                for (byte i = 0; i < data.Length; i++)
                {
                    data[i] = 0;
                }
                scope.MeasureVEC(out data, 1);
                eyeHeight = data[0];
                amp_5 = data[1];
                vec = data[2];
            }

            vcmi = scope.MeasureVCMI();

            logoStr += logger.AdapterLogString(0, "vec:" + vec.ToString());
            logoStr += logger.AdapterLogString(0, "vcmi_AC:" + vcmi[0].ToString());
            logoStr += logger.AdapterLogString(0, "vcmi_DC:" + vcmi[1].ToString());
            logoStr += logger.AdapterLogString(1, "eyeHeight:" + eyeHeight.ToString());
            logoStr += logger.AdapterLogString(1, "amp of 5%:" + amp_5.ToString());

            return true;
        }

        protected override bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            bool flag = DeassembleEquipment();
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].DecreaseReferencedTimes();

            }
            return flag;
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

        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] infoList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[5];

                outputParameters[0].FiledName = "EYEHeight";
                eyeHeight = algorithm.ISNaNorIfinity(eyeHeight);
                outputParameters[0].DefaultValue = Math.Round(eyeHeight, 4).ToString().Trim();

                outputParameters[1].FiledName = "AMP_5";
                amp_5 = algorithm.ISNaNorIfinity(amp_5);
                outputParameters[1].DefaultValue = Math.Round(amp_5, 4).ToString().Trim();

                outputParameters[2].FiledName = "VEC";
                vec = algorithm.ISNaNorIfinity(vec);
                outputParameters[2].DefaultValue = Math.Round(vec, 4).ToString().Trim();

                outputParameters[3].FiledName = "VCMI_AC(mV)";
                vcmi[0] = algorithm.ISNaNorIfinity(vcmi[0]);
                outputParameters[3].DefaultValue = Math.Round(vcmi[0], 4).ToString().Trim();

                outputParameters[4].FiledName = "VCMI_DC(mV)";
                vcmi[1] = algorithm.ISNaNorIfinity(vcmi[1]);
                outputParameters[4].DefaultValue = Math.Round(vcmi[1], 4).ToString().Trim();
                
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
