
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
    public struct AdjustJitterStruct
    {
        private int max, min, start, step, methodID;

        #region attribute
        public int DACMax
        {
            get
            {
                return this.max;
            }
            set
            {
                if (value < 0)
                {
                    throw new IndexOutOfRangeException("Input value is out of range!");
                }
                this.max = value;
            }
        }

        public int DACMin
        {
            get
            {
                return this.min;
            }
            set
            {
                if (value < 0)
                {
                    throw new IndexOutOfRangeException("Input value is out of range!");
                }
                this.min = value;
            }
        }

        public int DACStart
        {
            get
            {
                return this.start;
            }
            set
            {
                if (value < 0)
                {
                    throw new IndexOutOfRangeException("Input value is out of range!");
                }
                this.start = value;
            }
        }

        public int DACStep
        {
            get
            {
                return this.step;
            }
            set
            {
                this.step = value;
            }
        }

        public int AdjustMethodID
        {
            get
            {
                return this.methodID;
            }
            set
            {
                this.methodID = value;
            }
        }
        #endregion

        public AdjustJitterStruct(int[] value)
        {
            max = value[0];
            min = value[1];
            if (min > max)
            {
                throw new Exception("Min is more than max!");
            }
            start = value[2];
            step = value[3];
            methodID = value[4];
        }        
    }

    public class AdjustJitter : TestModelBase
    {
        #region Attribute

        private struct JitterSpec
        {
            public double SpecMax { get; set; }

            public double SpecMin { get; set; }

            public double TypcalValue { get; set; }

            public double AdjustSpecMax { get; set; }

            public double AdjustSpecMin { get; set; }
        }

        private AdjustJitterStruct myStruct;

        private JitterSpec mySpec = new JitterSpec();

        private Int32 outDAC;

        private double outJitter;

        private ArrayList inParaList = new ArrayList();

        private Powersupply supply;

        private Scope scope;

        private DataTable dtJitter = new DataTable();

        private Int32 precisStep;

        private ArrayList logList = new ArrayList();
        #endregion

        #region Method

        public AdjustJitter(DUT inDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inDut;

            inParaList.Clear();

            inParaList.Add("DACMax");
            inParaList.Add("DACStart");
            inParaList.Add("DACStep");
            inParaList.Add("DACMin");           
            inParaList.Add("AdjustMethod");

            dtJitter.Columns.Add("Channel");
            dtJitter.Columns.Add("JitterPP");
            dtJitter.Columns.Add("JitterDAC");
            dtJitter.Columns.Add("TempADC");
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

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));

            bool result = Test();

            OutPutandFlushLog();

            return result;
        }

        private bool LoadPNSpec()
        {
            try
            {
                double max, min, target;
                algorithm.GetSpec(specParameters, "JitterPP(PS)", 0, out max, out target, out min);

                mySpec.SpecMax = max;
                mySpec.SpecMin = min;
                mySpec.TypcalValue = target;
                mySpec.AdjustSpecMax = mySpec.SpecMax;
                mySpec.AdjustSpecMin = mySpec.SpecMin;              

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Test()
        {
            if (!AnalysisInputParameters(inputParameters))
            {
                return false;
            }

            if (!LoadPNSpec())
            {
                return false;
            }

            if (!Adjust())
            {
                outDAC = myStruct.DACStart;
                dut.StoreEA(myStruct.DACStart);
                outJitter = this.GetJitterPP();
                return false;
            }
            else
            {
                DataRow dr = dtJitter.NewRow();
                dr["Channel"] = GlobalParameters.CurrentChannel;
                dr["JitterPP"] = outJitter;
                dr["JitterDAC"] = outDAC;
             
                ushort TempValue;
                dut.ReadTempADC(out TempValue);
                dr["TempADC"] = TempValue;
                dtJitter.Rows.Add(dr);
            }

            if (!CalculatedCoefs())
            {
                return false;
            }

            return true;
        }
        
        private bool Adjust()
        {
            switch (myStruct.AdjustMethodID)
            {
                case 1:
                    if (!SearchJitter())
                    {
                        return false;
                    }
                    break;
                default:
                    if (!SearchJitter())
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }

        private bool SearchJitter()
        {      
            try
            {
                outJitter = mySpec.SpecMax;
                outDAC = myStruct.DACStart;
                logList.Clear();

                for (int dac = myStruct.DACStart; dac <= myStruct.DACMax; dac = dac + myStruct.DACStep)
                {
                    dut.WriteEA(dac);
                    scope.ClearDisplay();
                    scope.AutoScale();
                    Thread.Sleep(3000);
                    double value = this.GetJitterPP();
                    logList.Add("DAC=" + dac + "  Jitter=" + value);
                    if (value < outJitter)
                    {
                        outJitter = value;
                        outDAC = dac;
                    }
                }

                for (int i = 0; i < logList.Count; i++)
                {
                    logoStr += logger.AdapterLogString(1, logList[i].ToString());
                }

                if (outJitter >= mySpec.SpecMin && outJitter <= mySpec.SpecMax)
                {
                    dut.StoreEA(outDAC);
                    logoStr += logger.AdapterLogString(1, "BestJitterDAC=" + outDAC + " " + "BestJitter=" + outJitter.ToString());
                    return true;
                }
                else
                {
                    logoStr += logger.AdapterLogString(3, "I Can't find Best DAC ");
                    return false;
                }  
            }
            catch
            {
                return false;
            }
        }

        private double GetJitterPP()
        {
            List<double> list = new List<double>();
            scope.DisplayJitter(0);
            for (int j = 0; j < 5; j++)
            {
                Thread.Sleep(500);
                double value = scope.GetJitterPP();
                if(value <1000)
                {
                    list.Add(value);
                }
            }

            return list.Max();
        }

        #region prepare to delete
        #region  AdjustJitter--no modify
        private double CalculateBestPoint(double TypcalValue, double[] X, double[] Y)
        {


            double BestDacPoint;

            double slope = 0, intercept = 0;

            algorithm.LinearRegression(X, Y, out slope, out intercept);

            BestDacPoint = slope + (intercept * TypcalValue);

            return BestDacPoint;
        }

        private byte GetTargetPointIndex(Double TypcalValue, Double[] Y)
        {
            byte index;

            ArrayList Diff = new ArrayList();

            for (int i = 0; i < Y.Length; i++)
            {
                Diff.Add(Math.Abs(TypcalValue - Y[i]));
            }
            algorithm.SelectMinValue(Diff, out index);

            return index;
        }        

        private bool AdjustJitter_Precision(double TargetValue, Int32 index, Int32[] DACArray, double[] itemArray)
        {
            int PrecisDAC_Array_length = 0;
            Int32 MinValue, MaxValue;
            Int32[] PrecisDAC_Array;//= new Int32[10];
            double[] PrecisItem_Array;// = new double[10];

            try
            {

                if (index == 0)//如果最好值为最小的一个点
                {
                    MinValue = DACArray[0];
                    MaxValue = DACArray[1];
                }
                else if (index == itemArray.Length - 1)// 如果最好值为最大点
                {
                    MinValue = DACArray[DACArray.Length - 2];
                    MaxValue = DACArray[DACArray.Length - 1];
                }
                else// 中间值
                {
                    MinValue = (DACArray[index] + DACArray[index - 1]) / 2;
                    MaxValue = (DACArray[index + 1] + DACArray[index]) / 2;
                }

                precisStep = (Int32)((MaxValue - MinValue) / 5);

                if (precisStep <= 1)
                {
                    precisStep = 1;
                    PrecisDAC_Array_length = (Int32)((MaxValue - MinValue) / 1);

                }
                else
                {
                    PrecisDAC_Array_length = 5;
                }

                PrecisDAC_Array = new Int32[PrecisDAC_Array_length];
                PrecisItem_Array = new double[PrecisDAC_Array_length];
                for (int i = 0; i < PrecisDAC_Array_length; i++)
                {
                    PrecisDAC_Array[i] = MinValue + i * precisStep;
                }
                for (int i = 0; i < PrecisDAC_Array_length; i++)
                {
                    dut.WriteEA(PrecisDAC_Array[i]);
                    Thread.Sleep(200);
                    scope.ClearDisplay();
                    Thread.Sleep(2000);
                    PrecisItem_Array[i] = scope.GetJitterPP();
                    logoStr += logger.AdapterLogString(1, "PrecisDAC_Array[" + i.ToString() + "]=" + PrecisDAC_Array[i].ToString() + " " + "PrecisItem_Array[" + i.ToString() + "]=" + PrecisItem_Array[i].ToString());
                }

                byte Valueindex = GetTargetPointIndex(mySpec.TypcalValue, Array.ConvertAll(PrecisItem_Array, s => Convert.ToDouble(s)));

                dut.StoreEA(PrecisDAC_Array[Valueindex]);
                outDAC = PrecisDAC_Array[Valueindex];
                TargetValue = PrecisItem_Array[Valueindex];

            }
            catch (System.Exception ex)
            {
                logoStr += logger.AdapterLogString(3, ex.Message);
                return false;

            }
            return true;
        }

        private bool AdjustMethod_1()//
        {
           
             //return   AdjustJitter_Precision(mySpec.TypcalValue,)
            return false;


        }

        protected byte SearchJiiterPoint(Int32[] ScanArray, out double[] Item)
        {


            Item = new double[ScanArray.Length];

            try
            {
                ArrayList StrLog = new ArrayList();

                for (int i = 0; i < ScanArray.Length; i++)
                {

                    dut.WriteEA(ScanArray[i]);

                    Thread.Sleep(200);
                    

                    Item[i] = scope.GetJitterPP();

                    StrLog.Add("ScanArray[" + i + "] =" + ScanArray[i].ToString() + " " + "Jiiter[" + i + "] =" + Item[i].ToString());

                }



                for (int i = 0; i < StrLog.Count; i++)
                {
                    logoStr += logger.AdapterLogString(0, StrLog[i].ToString());
                }

                byte bIndex;


                ArrayList TempArray = new ArrayList();
                TempArray.Clear();
                for (int i = 0; i < ScanArray.Length; i++)
                {
                    TempArray.Add(Item[i]);
                }


                algorithm.SelectMinValue(TempArray, out bIndex);

                logoStr += logger.AdapterLogString(0, "ScanMaskDAC =" + ScanArray[bIndex].ToString());
                logoStr += logger.AdapterLogString(0, "ScanMask =" + Item[bIndex].ToString());
                //logoStr += logger.AdapterLogString(0, "ErrorRate[" + bIndex + "] =" + TempValue);

                return bIndex;

            }
            catch (System.Exception ex)
            {
                logoStr += logger.AdapterLogString(3, "Can't find the best Value");
                return 0;
            }


        }
        #endregion

        #region  calculated && Write coefficient--no modify
        private bool CalculatedCoefs()
        {
            switch (myStruct.AdjustMethodID)
            {
                case 0:
                    string SelectCondition = "Channel=" + GlobalParameters.CurrentChannel;

                    DataRow[] drArray = dtJitter.Select(SelectCondition);

                    if (drArray.Length < 2)
                    {
                        break;
                    }

                    #region  TempCout>=2

                    string[] TempADCArray = drArray.Select(A => A["TempADC"].ToString()).ToArray();
                    double[] X = Array.ConvertAll<String, double>(TempADCArray, s => double.Parse(s));

                    string[] TargetMaskDacArray = drArray.Select(A => A["TargetMaskDac"].ToString()).ToArray();
                    double[] Y = Array.ConvertAll<String, double>(TargetMaskDacArray, s => double.Parse(s));

                    double[] coefArray = algorithm.MultiLine(X, Y, X.Length, 2);

                    //coefArray.Reverse();

                    Array.Reverse(coefArray);
                    float[] floatArray = Array.ConvertAll<double, float>(coefArray, s => float.Parse(s.ToString()));

                    WriteCoef(floatArray);
                    supply.OutPutSwitch(false);
                    supply.OutPutSwitch(true);
                    #endregion

                    break;
                case 1:// 无系数可写

                    break;
                default:
                    break;
            }





            return true;
        }

        private bool WriteCoef(float[] Coef)
        {

            float CoefA = (float)Coef[0];
            float CoefB = (float)Coef[1];
            float CoefC = (float)Coef[2];


            logoStr += logger.AdapterLogString(1, "CoefA=" + CoefA + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(CoefA)));
            logoStr += logger.AdapterLogString(1, "CoefB=" + CoefB + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(CoefB)));
            logoStr += logger.AdapterLogString(1, "CoefC=" + CoefC + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(CoefC)));


            //if (!dut.SetMaskcoefa(CoefA))
            //{
            //    logoStr += logger.AdapterLogString(3, "CoefA Write Error!");

            //    return false;
            //}
            //if (!dut.SetMaskcoefb(CoefB))
            //{
            //    logoStr += logger.AdapterLogString(3, "CoefB Write Error!");

            //    return false;
            //}

            //if (!dut.SetMaskcoefc(CoefC))
            //{
            //    logoStr += logger.AdapterLogString(3, "CoefC Write Error!");

            //    return false;
            //}

            return true;
        }
        #endregion
        #endregion

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
                string[] search = new string[] { "DACMax", "DACMin", "DACStart", "DACStep", "AdjustMethod" };
                int[] paraValue = new int[search.Length];

                for (int i = 0; i < search.Length; i++)
                {
                    string outString;
                    bool isGetten = algorithm.Getconf(infoList, search[i], true, out outString);
                    int temp = 0;
                    isGetten = isGetten && Int32.TryParse(outString, out temp);
                    if (isGetten)
                    {
                        paraValue[i] = temp;
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(4, search[i] + " is wrong!");
                        return false;
                    }
                }

                myStruct = new AdjustJitterStruct(paraValue);

                #region prepare to replaced by the above code
                //if (!algorithm.Getconf(infoList, "DACMax", true, out outString))
                //{
                //    logoStr += logger.AdapterLogString(4, "MaxDacMin is wrong!");
                //    return false;
                //}
                //else
                //{
                //    myStruct.DACMax = Int32.Parse(outString);
                //}

                //if (!algorithm.Getconf(infoList, "DACMin", true, out outString))
                //{
                //    logoStr += logger.AdapterLogString(4, "MinDACMin is wrong!");
                //    return false;
                //}
                //else
                //{
                //    myStruct.DACMin = Int32.Parse(outString);
                //}

                //if (!algorithm.Getconf(infoList, "DACStart", true, out outString))
                //{
                //    logoStr += logger.AdapterLogString(4, "DACMin is wrong!");
                //    return false;
                //}
                //else
                //{
                //    myStruct.DACStart = Int32.Parse(outString);
                //}

                //if (!algorithm.Getconf(infoList, "AdjustMethod", true, out outString))
                //{
                //    logoStr += logger.AdapterLogString(4, "AdjustMethod is wrong!");
                //    return false;
                //}
                //else
                //{
                //    myStruct.AdjustMethod = byte.Parse(outString);
                //}

                //if (!algorithm.Getconf(infoList, "DacStep", true, out outString))
                //{
                //    logoStr += logger.AdapterLogString(4, "DacStep is wrong!");
                //    return false;
                //}
                //else
                //{
                //    myStruct.DACStep = Int32.Parse(outString);
                //}
                #endregion
                
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }

        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] infoList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[1];

                outputParameters[0].FiledName = "JitterPP(PS)";
                outputParameters[0].DefaultValue = outJitter.ToString();

                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
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
        #endregion
    }

}
