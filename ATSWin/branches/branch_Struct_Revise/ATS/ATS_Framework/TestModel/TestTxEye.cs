using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ATS_Framework
{
    public class TestTxEye : TestModelBase
    {
 #region Attribute       
        private double apDBM;
        private double er;
        private double crossing;
        private double marginVaulue;
        private double jitterRMS;
        private double jitterPP;
        private double OEOMA;
        private double OEOMADBM;
        private double riseTime;
        private double fallTime;
        private double EyeWidth;
        private double EyeHeight;
        private double bitRate;
        private double XmarginVaulue2;
        private double MaskSpecMin;
        private double MaskSpecMax;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private Powersupply tempps;
        private Scope tempScope;

 #endregion

 #region Method
        public TestTxEye(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;  
            inPutParametersNameArray.Clear();                   
        }
        public override bool SelectEquipment(EquipmentList aEquipList)
        {
            try
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

                        if (tempKeys[i].ToUpper().Contains("SCOPE"))
                        {
                            selectedEquipList.Add("SCOPE", tempValues[i]);
                            isOK = true;
                        }
                        if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                        {
                            selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        }
                        if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                        {
                            tempValues[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                        }
                    }
                    tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                    tempScope = (Scope)selectedEquipList["SCOPE"];
                    if (tempps != null && tempScope != null)
                    {
                        isOK = true;

                    }
                    else
                    {

                        if (tempScope == null)
                        {
                            Log.SaveLogToTxt("SCOPE =NULL");
                        }
                        if (tempps == null)
                        {
                            Log.SaveLogToTxt("POWERSUPPLY =NULL");
                        }
                        isOK = false;
                        OutPutandFlushLog();

                    }
                    if (isOK)
                    {
                        selectedEquipList.Add("DUT", dut);
                        return isOK;
                    }

                    return isOK;
                }
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
            }
            catch(Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F00, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02F00, error.StackTrace); 
            }            
        }

        override protected bool CheckEquipmentReadiness()
        {
            lock (tempScope)
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
            lock (tempScope)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    selectedEquipList.Values[i].IncreaseReferencedTimes();
                }
                return AssembleEquipment();
            }
        }

        protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {

            //for (int i = 0; i < selectedEquipList.Count; i++)
            //{
            //    if (!selectedEquipList.Values[i].Configure()) return false;

            //}//test

            return true;
        }

        protected bool LoadMaskSpec()
        {
            lock (tempScope)
            {
                try
                {
                    Algorithm.GetSpec(specParameters, "MASKMARGIN(%)", 0, out MaskSpecMax, out MaskSpecMin);
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
                    InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02000, error.StackTrace);
                    //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                    exceptionList.Add(ex);
                    return false;
                    //the other way is: should throw exception, rather than the above three code. see below:
                    //throw new InnoExCeption(ExceptionDictionary.Code._0x02000, error.StackTrace); 
                }
            }
        }
        protected override bool StartTest()
        {
            lock (tempScope)
            {
                try
                {

                    logoStr = "";
                    //// 是否要测试需要添加判定            

                    if (PrepareEnvironment(selectedEquipList) == false)
                    {
                        OutPutandFlushLog();
                        return false;
                    }

                    if (tempps != null)
                    {
                        // open apc 

                        {
                            CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                        }

                        // open apc
                        Log.SaveLogToTxt("Step3...StartTestOptical Eye");
                        Log.SaveLogToTxt("OpticalEyeTest");
                        LoadMaskSpec();
                        OpticalTest();
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
        }
        protected bool OpticalTest()
        {
            lock (tempScope)
            {
                try
                {
                    bool flagMask = false;

                    apDBM = InnoExCeption.NaN;
                    er = InnoExCeption.NaN;
                    crossing = InnoExCeption.NaN;
                    marginVaulue = InnoExCeption.NaN;
                    jitterRMS = InnoExCeption.NaN;
                    jitterPP = InnoExCeption.NaN;
                    OEOMA = InnoExCeption.NaN;
                    OEOMADBM = InnoExCeption.NaN;
                    riseTime = InnoExCeption.NaN;
                    fallTime = InnoExCeption.NaN;
                    EyeHeight = InnoExCeption.NaN;
                    bitRate = InnoExCeption.NaN;
                    XmarginVaulue2 = InnoExCeption.NaN;

                    //throw new InnoExCeption(ExceptionDictionary.Code._0x02000, "abc");
                    //int ni = 0;
                    //int nj = 5 / ni;

                    //Scope tempScope = (Scope)selectedEquipList["SCOPE"];
                    if (tempScope != null)
                    {
                        tempScope.pglobalParameters = GlobalParameters;
                        double[] tempDoubleArray;

                        //for (int K = 0; K < 4;K++ )
                        //{
                        if (!tempScope.OpticalEyeTest(out tempDoubleArray, 1))
                        {
                            for (byte i = 0; i < tempDoubleArray.Length; i++)
                            {
                                tempDoubleArray[i] = InnoExCeption.NaN;
                            }

                        }
                        if (tempDoubleArray[8] > MaskSpecMax || tempDoubleArray[8] < MaskSpecMin)
                        {
                            flagMask = false;
                        }
                        else
                        {
                            flagMask = true;
                        }
                        if (!flagMask)//后三次测试有一次失败就不在测试
                        {

                            for (int k = 0; k < 3; k++)
                            {
                                if (!tempScope.OpticalEyeTest(out tempDoubleArray))
                                {
                                    for (byte i = 0; i < tempDoubleArray.Length; i++)
                                    {
                                        tempDoubleArray[i] = InnoExCeption.NaN;
                                    }
                                }
                                if (tempDoubleArray[8] > MaskSpecMax || tempDoubleArray[8] < MaskSpecMin)
                                {
                                    break;
                                }
                            }

                        }


                        OEOMA = tempDoubleArray[2];
                        OEOMADBM = Algorithm.ChangeUwtoDbm(OEOMA * 1000);
                        jitterRMS = tempDoubleArray[5];
                        jitterPP = tempDoubleArray[4];
                        riseTime = tempDoubleArray[6];
                        fallTime = tempDoubleArray[7];
                        crossing = tempDoubleArray[3];
                        er = tempDoubleArray[1];
                        apDBM = tempDoubleArray[0];
                        marginVaulue = tempDoubleArray[8];
                        EyeHeight = tempDoubleArray[9];
                        bitRate = tempDoubleArray[10];
                        XmarginVaulue2 = tempDoubleArray[11];
                        //  EyeWidth = tempDoubleArray[10];
                        Log.SaveLogToTxt("apDBM:" + apDBM.ToString());
                        Log.SaveLogToTxt("er:" + er.ToString());
                        Log.SaveLogToTxt("crossing:" + crossing.ToString());
                        Log.SaveLogToTxt("maskVaulue:" + marginVaulue.ToString());
                        Log.SaveLogToTxt("jitterRMS:" + jitterRMS.ToString());
                        Log.SaveLogToTxt("jitterPP:" + jitterPP.ToString());
                        Log.SaveLogToTxt("OEOMA:" + OEOMA.ToString());
                        Log.SaveLogToTxt("OEOMADBM:" + OEOMADBM.ToString());
                        Log.SaveLogToTxt("riseTime:" + riseTime.ToString());
                        Log.SaveLogToTxt("fallTime:" + fallTime.ToString());
                        Log.SaveLogToTxt("bitRate:" + bitRate.ToString());
                        Log.SaveLogToTxt("maskVaulue2:" + XmarginVaulue2.ToString());
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Equipments are not enough!");
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
        }     
        override protected bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            lock (tempScope)
            {
                bool flag = DeassembleEquipment();

                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    selectedEquipList.Values[i].DecreaseReferencedTimes();

                }


                return flag;
            }
        }

        protected override bool AssembleEquipment()
        {
            lock (tempScope)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;

                }
                return true;
            }
        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            lock (tempScope)
            {
                try
                {
                    outputParameters = new TestModeEquipmentParameters[12];
                    outputParameters[0].FiledName = "AP(DBM)";
                    apDBM = Algorithm.ISNaNorIfinity(apDBM);
                    outputParameters[0].DefaultValue = Math.Round(apDBM, 4).ToString().Trim();
                    outputParameters[1].FiledName = "ER(DB)";
                    er = Algorithm.ISNaNorIfinity(er);
                    outputParameters[1].DefaultValue = Math.Round(er, 4).ToString().Trim();
                    outputParameters[2].FiledName = "CROSSING(%)";
                    crossing = Algorithm.ISNaNorIfinity(crossing);
                    outputParameters[2].DefaultValue = Math.Round(crossing, 4).ToString().Trim();
                    outputParameters[3].FiledName = "MASKMARGIN(%)";
                    marginVaulue = Algorithm.ISNaNorIfinity(marginVaulue);
                    outputParameters[3].DefaultValue = Math.Round(marginVaulue, 4).ToString().Trim();
                    outputParameters[4].FiledName = "JITTERRMS(PS)";
                    jitterRMS = Algorithm.ISNaNorIfinity(jitterRMS);
                    outputParameters[4].DefaultValue = Math.Round(jitterRMS, 4).ToString().Trim();
                    outputParameters[5].FiledName = "JITTERPP(PS)";
                    jitterPP = Algorithm.ISNaNorIfinity(jitterPP);
                    outputParameters[5].DefaultValue = Math.Round(jitterPP, 4).ToString().Trim();
                    outputParameters[6].FiledName = "RISETIME(PS)";
                    riseTime = Algorithm.ISNaNorIfinity(riseTime);
                    outputParameters[6].DefaultValue = Math.Round(riseTime, 4).ToString().Trim();
                    outputParameters[7].FiledName = "FALLTIME(PS)";
                    fallTime = Algorithm.ISNaNorIfinity(fallTime);
                    outputParameters[7].DefaultValue = Math.Round(fallTime, 4).ToString().Trim();

                    outputParameters[8].FiledName = "TXOMA(DBM)";
                    OEOMA = Algorithm.ISNaNorIfinity(OEOMADBM);
                    outputParameters[8].DefaultValue = Math.Round(OEOMADBM, 4).ToString().Trim();

                    outputParameters[9].FiledName = "EYEHEIGHT(MW)"; //EyeHeight
                    EyeHeight = Algorithm.ISNaNorIfinity(EyeHeight);
                    outputParameters[9].DefaultValue = Math.Round(EyeHeight, 4).ToString().Trim();

                    outputParameters[10].FiledName = "BitRate(GB/S)"; //EyeHeight
                    bitRate = Algorithm.ISNaNorIfinity(bitRate);
                    outputParameters[10].DefaultValue = Math.Round(bitRate, 4).ToString().Trim();

                    outputParameters[11].FiledName = "XMASKMARGIN2(%)"; //EyeHeight
                    bitRate = Algorithm.ISNaNorIfinity(XmarginVaulue2);
                    outputParameters[11].DefaultValue = Math.Round(XmarginVaulue2, 4).ToString().Trim();

                    
                    for (int i = 0; i < outputParameters.Length; i++)
                    {
                        Log.SaveLogToTxt(outputParameters[i].FiledName + " : " + outputParameters[i].DefaultValue);
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
        }        
        protected bool PrepareEnvironment(EquipmentList aEquipList)
        {
            lock (tempScope)
            {
                try
                {

                    if (tempScope != null)
                    {
                        if (tempScope.SetMaskAlignMethod(1) &&
                        tempScope.SetMode(0) &&
                        tempScope.MaskONOFF(false) &&
                        tempScope.SetRunTilOff() &&
                        tempScope.RunStop(true) &&
                        tempScope.OpenOpticalChannel(true) &&
                        tempScope.RunStop(true) &&
                        tempScope.ClearDisplay() &&
                        tempScope.AutoScale()
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
                catch (InnoExCeption ex)//from driver
                {
                    //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                    exceptionList.Add(ex);
                    return false;
                }
                catch (Exception error)//from itself
                {
                    //one way: deal this exception itself
                    InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F03, error.StackTrace);
                    //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                    exceptionList.Add(ex);
                    return false;
                    //the other way is: should throw exception, rather than the above three code. see below:
                    //throw new InnoExCeption(ExceptionDictionary.Code._0x02F03, error.StackTrace); 
                }
            }
        }        
        private void OutPutandFlushLog()
        {
            lock (tempScope)
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
        }

        public override List<InnoExCeption> GetException()
        {
            lock (tempScope)
            {
                return base.GetException();
            }
        }
#endregion
    }
}
