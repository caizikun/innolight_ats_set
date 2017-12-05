using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace ATS_Framework
{
    public struct TestRxEyeVecEWStruct
    {
        public double SensePoint { get; set; }
    }

    public class TestRxEyeVecEW : TestModelBase
    {
        private DataTable dataTable = new DataTable();

        private ArrayList inList = new ArrayList();

        private TestRxEyeVecEWStruct vcmiVecStruct = new TestRxEyeVecEWStruct();

        private Scope scope;

        private Attennuator attRx;

        private Powersupply supply;

        private Dictionary<string,double> testOutputData;

        public TestRxEyeVecEW(DUT inDut)
        {
            
            dut = inDut;
            logoStr = null;
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
            try
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
                        Log.SaveLogToTxt("Step3...Set AttValue" + Convert.ToString(vcmiVecStruct.SensePoint) + "DBM");
                        attRx.AttnValue(Convert.ToString(vcmiVecStruct.SensePoint), 1);
                    }
                    Log.SaveLogToTxt("Step4...StartTest Electricl Eye EW and Vec");
                    Log.SaveLogToTxt("EWVecTest");

                    RxEyeVecEWTest();

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
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace); 
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
                            vcmiVecStruct.SensePoint = -20;
                        }
                        else
                        {
                            if (temp > 0)
                            {
                                temp = -temp;
                            }
                            vcmiVecStruct.SensePoint = temp;
                        }
                    }
                    else
                    {
                        return false;
                    }
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

        protected bool RxEyeVecEWTest()
        {
            try
            {
                scope.pglobalParameters = GlobalParameters;
                double eyeHeight_15_max, eyeHeight_15_min, eyeWidth_15_max, eyeWidth_15_min, vec_max, vec_min;
                Algorithm.GetSpec(specParameters, "EyeHeight_15(mV)", 0, out eyeHeight_15_max, out eyeHeight_15_min);
                Algorithm.GetSpec(specParameters, "EyeWidth_15(ps)", 0, out eyeWidth_15_max, out eyeWidth_15_min);
                Algorithm.GetSpec(specParameters, "VEC(dB)", 0, out vec_max, out vec_min);
                testOutputData = new Dictionary<string, double>();
                int i = 0;
                bool result = false;
                do
                {
                    testOutputData.Clear();
                    result = scope.MeasureRxEyeVecEW(ref testOutputData);

                    result = result && (testOutputData["EyeHeight_15(mV)"] <= eyeHeight_15_max) && (testOutputData["EyeHeight_15(mV)"] >= eyeHeight_15_min);
                    result = result && (testOutputData["EyeWidth_15(ps)"] <= eyeWidth_15_max) && (testOutputData["EyeWidth_15(ps)"] >= eyeWidth_15_min);
                    result = result && (testOutputData["VEC(dB)"] <= vec_max) && (testOutputData["VEC(dB)"] >= vec_min);

                    if (result == true)
                    {
                        break;
                    }
                    i++;

                } while (i < 3);

                foreach (string key in testOutputData.Keys)
                {
                    Log.SaveLogToTxt(key + testOutputData[key].ToString("f4"));
                }

                return result;
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace); 
            }
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
                outputParameters = new TestModeEquipmentParameters[testOutputData.Keys.Count];

                int i = 0;
                foreach (string key in testOutputData.Keys)
                {
                   outputParameters[i].FiledName = key;
                   double temp = Algorithm.ISNaNorIfinity(testOutputData[key]);
                   outputParameters[i++].DefaultValue = Math.Round(temp, 4).ToString().Trim();
                }             
                
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
