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

        public TestRxEyeVcmiVec(DUT inDut)
        {
            
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
                        Log.SaveLogToTxt("attRx =NULL");
                    }

                    if (scope == null)
                    {
                        Log.SaveLogToTxt("scope =NULL");
                    }

                    if (supply == null)
                    {
                        Log.SaveLogToTxt("supply =NULL");
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

        protected override bool StartTest()
        {
            dataTable.Clear();
            
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
                    Log.SaveLogToTxt("Step3...Set AttValue" + Convert.ToString(vcmiVecStruct.CensePoint) + "DBM");
                    attRx.AttnValue(Convert.ToString(vcmiVecStruct.CensePoint), 1);
                }
                Log.SaveLogToTxt("Step4...StartTest Electricl Eye Vcmi and Vec");
                Log.SaveLogToTxt("VcmiVecTest");

                VcmiVecTest();

                OutPutandFlushLog();
                return true;
            }
            else
            {
                Log.SaveLogToTxt("Equipments are not enough!");
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
                    Log.SaveLogToTxt("PrepareEnvironment OK!");
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("PrepareEnvironment Fail!");
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
            Log.SaveLogToTxt("Step1...Check InputParameters");

            if (InformationList.Length < inList.Count)
            {
                Log.SaveLogToTxt("InputParameters are not enough!");
                return false;
            }
            else
            {
                int index = -1;
                bool isParaComplete = true;

                if (isParaComplete)
                {
                    if (Algorithm.FindFileName(InformationList, "INPUTRXPWR(DBM)", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            Log.SaveLogToTxt(InformationList[index].FiledName + "is wrong!");
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

                    //if (Algorithm.FindFileName(InformationList, "IsTestVcmi", out index))
                    //{
                    //   vcmiVecStruct.IsTestVcmi = InformationList[index].DefaultValue.ToString();
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                    //if (Algorithm.FindFileName(InformationList, "IsTestVec", out index))
                    //{
                    //    vcmiVecStruct.IsTestVec = Convert.ToDouble(InformationList[index].DefaultValue);
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                }
                Log.SaveLogToTxt("OK!");
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

            Log.SaveLogToTxt("vec:" + vec.ToString());
            Log.SaveLogToTxt("vcmi_AC:" + vcmi[0].ToString());
            Log.SaveLogToTxt("vcmi_DC:" + vcmi[1].ToString());
            Log.SaveLogToTxt("eyeHeight:" + eyeHeight.ToString());
            Log.SaveLogToTxt("amp of 5%:" + amp_5.ToString());

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
                eyeHeight = Algorithm.ISNaNorIfinity(eyeHeight);
                outputParameters[0].DefaultValue = Math.Round(eyeHeight, 4).ToString().Trim();

                outputParameters[1].FiledName = "AMP_5";
                amp_5 = Algorithm.ISNaNorIfinity(amp_5);
                outputParameters[1].DefaultValue = Math.Round(amp_5, 4).ToString().Trim();

                outputParameters[2].FiledName = "VEC";
                vec = Algorithm.ISNaNorIfinity(vec);
                outputParameters[2].DefaultValue = Math.Round(vec, 4).ToString().Trim();

                outputParameters[3].FiledName = "VCMI_AC(mV)";
                vcmi[0] = Algorithm.ISNaNorIfinity(vcmi[0]);
                outputParameters[3].DefaultValue = Math.Round(vcmi[0], 4).ToString().Trim();

                outputParameters[4].FiledName = "VCMI_DC(mV)";
                vcmi[1] = Algorithm.ISNaNorIfinity(vcmi[1]);
                outputParameters[4].DefaultValue = Math.Round(vcmi[1], 4).ToString().Trim();
                
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

        public override List<InnoExCeption> GetException()
        {
            return base.GetException();
        }
    }
}
