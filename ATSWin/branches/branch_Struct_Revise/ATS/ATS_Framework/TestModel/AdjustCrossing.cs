
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
    public class AdjustCrossing : TestModelBase
    {
        #region Attribute

        private struct CrossingSpec
        {
            public double SpecMax { get; set; }

            public double SpecMin { get; set; }

            public double TypcalValue { get; set; }

            public double AdjustSpecMax { get; set; }

            public double AdjustSpecMin { get; set; }         
        }

        private AdjustCrossingStuct myStruct = new AdjustCrossingStuct();

        private CrossingSpec mySpec = new CrossingSpec();        

        private Int32 outDAC;

        private double outCrossing;
        
        private ArrayList inParaList = new ArrayList();

        private Powersupply supply;

        private Scope scope;

        private DataTable dtCrossing = new DataTable();

        private DataTable dtJitter = new DataTable();

        private byte indexDAC;
      
        private Int32 precisStep;

        private ArrayList logList = new ArrayList();

        #endregion

        #region Method

        public AdjustCrossing(DUT inDut)
        {
            
            logoStr = null;
            dut = inDut;

            inParaList.Clear();
           
            inParaList.Add("CrossingDACMax");
            inParaList.Add("CrossingDACStart");
            inParaList.Add("CrossingDacStep");
            inParaList.Add("CrossingDACMin");

            inParaList.Add("SpecMax_PR");
            inParaList.Add("SpecMin_PR");
            inParaList.Add("AdjustMethod");

            dtCrossing.Columns.Add("Channel");
            dtCrossing.Columns.Add("outCrossingDac");
            dtCrossing.Columns.Add("outCrossing");
            dtCrossing.Columns.Add("TempPrameter");  
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
                        Log.SaveLogToTxt("POWERSUPPLY =NULL");
                    }
                    if (scope == null)
                    {
                        Log.SaveLogToTxt("scope =NULL");
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

        private bool LoadPNSpec()
        {
            try
            {
                double max, min, target;
                Algorithm.GetSpec(specParameters, "Crossing(%)", 0, out max, out target, out min);

                mySpec.SpecMax = max;
                mySpec.SpecMin = min;
                mySpec.TypcalValue = target;

                mySpec.AdjustSpecMax = mySpec.SpecMax * myStruct.SpecMax_PR/100;
                mySpec.AdjustSpecMin = mySpec.SpecMin * myStruct.SpecMin_PR/100;

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
        
        public override bool Test()
        {
            CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));

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
                dut.StoreCrossDac(myStruct.DACStart);
                outCrossing = GetCrossing();
                return false;
            }

            if (!CurveAndWriteCoefs())
            {
                return false;
            }
           
            return true;
        }

        #region  Adjust

        private bool Adjust()
        {
            switch (myStruct.AdjustMethod)
            {
                case 1:
                    if (!AdjustMethod_1())
                    {
                        return false;
                    }
                    break;

                case 2:
                    if (!AdjustMethodByBinarySearch())
                    {
                        return false;
                    }
                    break;

                default:
                    if (!AdjustMethod_1())
                    {
                        return false;
                    }
                    break;
            }
          
            return true;
        }

        private bool AdjustMethodByBinarySearch()
        {
            bool result = false;
            double value, rang, targetValue, targetMin, targetMax;
            rang = 2.0;
            targetValue = mySpec.TypcalValue;
            targetMin = targetValue - rang;
            targetMax = targetValue + rang;

            try
            {
                Int32 startDAC = myStruct.DACStart;
                dut.WriteCrossDac(startDAC);
                scope.AutoScale();
                Thread.Sleep(3000);
                value = GetCrossing();

                if (value <= targetMax && value >= targetMin)
                {
                    Log.SaveLogToTxt("value is " + value);
                    dut.StoreCrossDac(startDAC);

                    outDAC = myStruct.DACStart;
                    outCrossing = value;

                    return true;
                }
                else
                {
                    int i = 0;
                    logList.Clear();

                    int max = myStruct.DACMax;
                    int min = myStruct.DACMin;

                    while (!result && min < max)
                    {
                        if (startDAC > myStruct.DACMax || startDAC < myStruct.DACMin || i > 20)
                        {
                            break;
                        }   
                        startDAC = min + (max - min) / 2;

                        dut.WriteCrossDac(startDAC);
                        scope.ClearDisplay();
                        Thread.Sleep(2000);
                        value = GetCrossing();
                        logList.Add("DAC=" + startDAC + "  Crossing=" + value);

                        if (value < targetMin)
                        {
                            min = startDAC;
                        }
                        else if (value > targetMax)
                        {
                            max = startDAC;
                        }
                        else
                        {
                            dut.StoreCrossDac(startDAC);
                            result = true;
                            break;
                        }
                    }
                }                        

                for (int i = 0; i < logList.Count; i++)
                {
                    Log.SaveLogToTxt(logList[i].ToString());

                }

                if (value >= mySpec.SpecMin && value <= mySpec.SpecMax)
                {
                    dut.StoreCrossDac(startDAC);
                    result = true;

                    outDAC = startDAC;
                    outCrossing = value;

                    Log.SaveLogToTxt("BestCrossingDAC=" + startDAC + " " + "BestCrossing=" + value.ToString());
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("I Can't find Best DAC ");
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace); 
            }
        }

        private double GetCrossing()
        {
            double[] array = new double[5];
            scope.DisplayCrossing();
            for (int j = 0; j < 5; j++)
            {
                Thread.Sleep(500);
                array[j] = scope.GetCrossing();
            }
            array.Min();

            return Convert.ToDouble(array.Min());
        }

        #region no modify
        private bool AdjustMethod_1()//
        {
            bool flag = false;

            double CurrentCrossing;

            try
            {
                //scope.EyeTuningDisplay(0);
                Int32 StartDac = myStruct.DACStart;
                dut.WriteCrossDac(myStruct.DACStart);
                scope.AutoScale();
                Thread.Sleep(3000);
                CurrentCrossing = GetCrossing();

                if (CurrentCrossing <= mySpec.AdjustSpecMax && CurrentCrossing >= mySpec.AdjustSpecMin)
                {
                    Log.SaveLogToTxt("CurrentCrossing is " + CurrentCrossing);
                    dut.StoreCrossDac(StartDac);
                    outDAC = myStruct.DACStart;
                    outCrossing = CurrentCrossing;
                    flag = true;
                    return flag;
                }
                else
                {
                    flag = false;
                    int i = 0;
                    logList.Clear();
                    while (!flag)
                    {
                        //if (StartDac > myStruct.DACMax || StartDac < myStruct.DACMin)
                        //{
                        //    break;
                        //}

                        //if (i > 20)
                        //{
                        //    break;
                        //}

                        dut.WriteCrossDac(StartDac);
                        scope.ClearDisplay();
                        Thread.Sleep(1000);
                        CurrentCrossing = GetCrossing();
                        outDAC = StartDac;

                        logList.Add("DAC=" + StartDac + "  Crossing=" + CurrentCrossing);

                        if (CurrentCrossing > mySpec.AdjustSpecMax)
                        {
                            StartDac -= myStruct.DacStep;
                        }
                        else if (CurrentCrossing < mySpec.AdjustSpecMin)
                        {
                            StartDac += myStruct.DacStep;
                        }
                        else
                        {
                            dut.StoreCrossDac(StartDac);
                            flag = true;
                            break;
                        }

                        if (StartDac > myStruct.DACMax || StartDac < myStruct.DACMin)
                        {
                            break;
                        }

                        if (i > 20)
                        {
                            break;
                        }

                        i++;
                    }
                }

                for (int i = 0; i < logList.Count; i++)
                {
                    Log.SaveLogToTxt(logList[i].ToString());

                }

                // outDAC = myStruct.DACStart;

                //outCrossing = GetCrossing();


                if (!flag && CurrentCrossing >= mySpec.SpecMin && CurrentCrossing <= mySpec.SpecMax)
                {                    
                    flag = true;

                }

                dut.StoreCrossDac(outDAC);
                outCrossing = CurrentCrossing;
                if (flag)
                {                    
                    Log.SaveLogToTxt("BestCrossingDAC=" + outDAC + " " + "BestCrossing=" + outCrossing.ToString());
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("I Can't find Best DAC ");

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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace); 
            }
        }
        
        private double CalculateBestPoint(double TypcalValue, double[] X, double[] Y)
        {
            double BestDacPoint;

            double slope = 0, intercept = 0;

            Algorithm.LinearRegression(X, Y, out slope, out intercept);

            BestDacPoint = slope + (intercept * TypcalValue);

            return BestDacPoint;
        }

        private byte GetTargetPointIndex(Double TypcalValue, Double[] Y)
        {
            byte index;

            ArrayList list = new ArrayList();

            for (int i = 0; i < Y.Length;i++ )
            {
                list.Add(Math.Abs(TypcalValue - Y[i]));
            }
            Algorithm.SelectMinValue(list, out index);

            return index;
        }

        private bool AdjustCrossing_Precision(double outCrossing, Int32 index, Int32[] DACArray, double[] itemArray)
        {
            Int32[] PrecisDAC_Array = new Int32[10];
            double[] PrecisItem_Array = new double[10];
            try
            {

                if (index == 0)//如果最好值为最小的一个点
                {
                    precisStep = (Int32)((DACArray[1] - DACArray[0]) / 10);



                    for (int i = 0; i < 10; i++)
                    {

                        PrecisDAC_Array[i] = DACArray[0] + i * precisStep;
                    }
                }
                else if (index == itemArray.Length)// 如果最好值为最大点
                {

                    precisStep = (Int32)((DACArray[DACArray.Length - 1] - DACArray[DACArray.Length - 2]) / 10);

                    for (int i = 0; i < 10; i++)
                    {

                        PrecisDAC_Array[i] = DACArray[0] + i * precisStep;
                    }
                }
                else
                {


                    Int32 MinValue = (DACArray[index] + DACArray[index - 1]) / 2;
                    Int32 MaxValue = (DACArray[index + 1] + DACArray[index]) / 2;
                    precisStep = (MaxValue - MinValue) / 10;

                    for (int i = 0; i < 10; i++)
                    {

                        PrecisDAC_Array[i] = MinValue + i * precisStep;
                    }

                }

                for (int i = 0; i < 10; i++)
                {
                    dut.WriteCrossDac(PrecisDAC_Array[i]);
                    Thread.Sleep(200);
                    scope.ClearDisplay();
                    Thread.Sleep(2000);
                    PrecisItem_Array[i] = GetCrossing();
                    Log.SaveLogToTxt("PrecisDAC_Array[" + i.ToString() + "]=" + PrecisDAC_Array[i].ToString() + " " + "PrecisItem_Array[" + i.ToString() + "]=" + PrecisItem_Array[i].ToString());
                }

                byte Crossingindex = GetTargetPointIndex(mySpec.TypcalValue, Array.ConvertAll(PrecisItem_Array, s => Convert.ToDouble(s)));

                dut.StoreCrossDac(PrecisDAC_Array[Crossingindex]);
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false; ;
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace); 
            }
            return true;
        }        
        
        // private bool AdjustMethod_2()//
        //{
        //    bool flag = false;
        //    Int32 BestDac=0;
        //    double BestItemValue = 0;
        //    Int32[] ScanDacArray;
        //    double[] ScanItemArray;

        //    try
        //    {

        //        Int32 StartDac = myStruct.CrossingDACStart;

        //        dut.WriteCrossDac(myStruct.CrossingDACStart);
        //        scope.AutoScale();
        //        Thread.Sleep(3000);
        //        double CurrentCrossing = GetCrossing();

        //        // TargetApdDac = myStruct.WavelengthDacStart; 

        //        if (CurrentCrossing > mySpec.AdjustSpecMax && CurrentCrossing < mySpec.AdjustSpecMin )
        //        {

        //            Log.SaveLogToTxt("CurrentCrossing is " + CurrentCrossing);
        //            return false;
        //        }
        //        else
        //        {
        //            int DataCount = 1 + (myStruct.CrossingDACMax - myStruct.CrossingDACMin) / myStruct.CrossingDacStep;
        //            ScanDacArray = new Int32[DataCount];
        //            ScanItemArray = new double[DataCount];

        //            for (int i = 0; i < DataCount; i++)
        //            {
        //                ScanDacArray[i] = myStruct.CrossingDACMin + myStruct.CrossingDacStep * i;

                       
        //                Thread.Sleep(200);
        //                dut.WriteCrossDac(ScanDacArray[i]);

        //                ScanItemArray[i] = GetCrossing();

        //                logList.Clear();
        //                logList.Add("ScanDacArray[" + i + "] =" + ScanDacArray[i].ToString() + " " + "Crossing[" + i + "] =" + ScanItemArray[i].ToString());
        //                //Log.SaveLogToTxt("ScanDacArray[" + i + "] =" + ScanDacArray[i].ToString() + " " + "Crossing[" + i + "] =" + ScanItemArray[i].ToString());
                 
        //            }

        //            for (int i=0;i<logList.Count;i++)
        //            {
                        
        //              Log.SaveLogToTxt(logList[i].ToString());
        //            }

        //        }


        //        indexDAC = GetTargetPointIndex(mySpec.TypcalValue, Array.ConvertAll(ScanItemArray, s => Convert.ToDouble(s)));
        //        dut.StoreCrossDac(ScanDacArray[indexDAC]);

        //        BestDac = ScanDacArray[indexDAC];
        //        BestItemValue = GetCrossing();

        //        if (myStruct.flagPrecisionCrossing)
        //        {
        //            AdjustCrossing_Precision(mySpec.TypcalValue, indexDAC, ScanDacArray, ScanItemArray);
        //        }
        //        else
        //        {
        //            Log.SaveLogToTxt("BestCrossingDAC=" + BestDac + " " + "BestCrossing=" + BestItemValue.ToString());
        //        }


        //    }
        //     catch
        //    {
        //        return false;
        //    }

        //    return true;


        //}
        protected byte SearchCrossingPoint(Int32[] ScanArray,out double[]Item)
            {


            Item = new double[ScanArray.Length];

                try
                {
                    ArrayList StrLog = new ArrayList();

                    for (int i = 0; i < ScanArray.Length; i++)
                    {

                        dut.WriteCrossDac(ScanArray[i]);

                        Thread.Sleep(200);


                        Item[i] = GetCrossing();

                        StrLog.Add("ScanArray[" + i + "] =" + ScanArray[i].ToString() + " " + "Crossing[" + i + "] =" + Item[i].ToString());

                    }



                    for (int i = 0; i < StrLog.Count; i++)
                    {
                        Log.SaveLogToTxt(StrLog[i].ToString());
                    }

                    byte bIndex;


                    ArrayList TempArray = new ArrayList();
                    TempArray.Clear();
                    for (int i = 0; i < ScanArray.Length; i++)
                    {
                        TempArray.Add(Item[i]);
                    }


                    Algorithm.SelectMinValue(TempArray, out bIndex);

                    Log.SaveLogToTxt("ScanCrossingDAC =" + ScanArray[bIndex].ToString());
                    Log.SaveLogToTxt("ScanCrossing =" + Item[bIndex].ToString());
                    //Log.SaveLogToTxt("ErrorRate[" + bIndex + "] =" + TempValue);

                    return bIndex;

                }
                catch (InnoExCeption ex)//from driver
                {
                    //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                    exceptionList.Add(ex);
                    return 0;
                }
                catch (Exception error)//from itself
                {
                    //one way: deal this exception itself
                    InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace);
                    //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                    exceptionList.Add(ex);
                    return 0;
                    //the other way is: should throw exception, rather than the above three code. see below:
                    //throw new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace); 
                }


            }        
        #endregion
    #endregion
    
        #region  Curve
        private bool CurveAndWriteCoefs()
        {
            switch (myStruct.AdjustMethod)
            {
                case 0:// 无系数可写
                    break;
                default:
                    break;
            }
            return true;
        }
        #endregion

        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] infoList)
        {
            Log.SaveLogToTxt("Step1...Check InputParameters");

            if (infoList.Length < inParaList.Count)
            {
                Log.SaveLogToTxt("InputParameters are not enough!");
                return false;
            }
            else
            {               
                bool isParaComplete = true;

                if (isParaComplete)
                {
                    string outString;

                    if (!Algorithm.Getconf(infoList, "SpecMax_PR", true, out outString))
                    {
                            Log.SaveLogToTxt("SpecMax_PR is wrong!");
                            return false;
                    }
                    else
                    {
                        myStruct.SpecMax_PR = byte.Parse(outString);
                    }

                    if (!Algorithm.Getconf(infoList, "SpecMin_PR", true, out outString))
                    {
                        Log.SaveLogToTxt("SpecMin_PR is wrong!");
                        return false;
                    }
                    else
                    {
                        myStruct.SpecMin_PR = byte.Parse(outString);
                    }


                    if (!Algorithm.Getconf(infoList, "CrossingDACMax", true, out outString))
                    {
                        Log.SaveLogToTxt("CrossingDacMin is wrong!");
                        return false;
                    }
                    else
                    {
                        myStruct.DACMax = Int32.Parse(outString);
                    }

                    if (!Algorithm.Getconf(infoList, "CrossingDACMin", true, out outString))
                    {
                        Log.SaveLogToTxt("CrossingDACMin is wrong!");
                        return false;
                    }
                    else
                    {
                        myStruct.DACMin = Int32.Parse(outString);
                    }

                    if (!Algorithm.Getconf(infoList, "CrossingDACStart", true, out outString))
                    {
                        Log.SaveLogToTxt("CrossingDACMin is wrong!");
                        return false;
                    }
                    else
                    {
                        myStruct.DACStart = Int32.Parse(outString);
                    }

                    if (!Algorithm.Getconf(infoList, "AdjustMethod", true, out outString))
                    {
                        Log.SaveLogToTxt("AdjustMethod is wrong!");
                        return false;
                    }
                    else
                    {
                        myStruct.AdjustMethod = byte.Parse(outString);
                    }  

                    if (!Algorithm.Getconf(infoList, "CrossingDacStep", true, out outString))
                    {
                        Log.SaveLogToTxt("CrossingDACMin is wrong!");
                        return false;
                    }
                    else
                    {
                        myStruct.DacStep = Int32.Parse(outString);
                    }
                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        }

        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] infoList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[2];            
                outputParameters[1].FiledName = "CrossingDAC";
                outputParameters[1].DefaultValue = outDAC.ToString();
                outputParameters[0].FiledName = "Crossing(%)";
                outputParameters[0].DefaultValue = outCrossing.ToString();
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F05, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02F05, error.StackTrace); 
            }
        }

        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputProcData(procData);
                
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

        public override List<InnoExCeption> GetException()
        {
            return base.GetException();
        }
        #endregion
    }
}
