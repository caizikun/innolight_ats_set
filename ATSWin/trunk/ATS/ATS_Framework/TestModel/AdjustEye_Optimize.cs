
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
    public class AdjustEyeOptimize : TestModelBase
    {
        #region Attribute

       private struct Spec
        {
          
           public double CrossingMax, CrossingMin, CrossingTypcalValue;
           public double JitterMax, JitterMin, JitterTypcalValue;
        }

        private OptimizsEyeEYEStruce MyStruct = new OptimizsEyeEYEStruce();
        private Spec MySpec= new Spec();
        
       // private OptimizsEyeEYEStruce MyStruct = new OptimizsEyeEYEStruce();

        private Int32 TargetCrossingDac;
        private Int32 TargetJitterDac;

        private ArrayList inPutParametersNameArray = new ArrayList();

        //equipments
        private Powersupply pPs;
        private Scope pScope;
        private Int32[] ScanDacArray;
        private double[] ScanItemArray;

        private DataTable dtCrossing=new DataTable();
        private DataTable dtJitter = new DataTable();
        private byte Index;

        private Int32[] PrecisDAC_Array = new Int32[10];
        private double[] PrecisItem_Array = new double[10];
        private Int32 PrecisStep;
        #endregion
        #region Method

        public AdjustEyeOptimize(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;

            dut = inPuDut;

            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("FlagAdjustCrossing");
            inPutParametersNameArray.Add("CrossingDACMax");
            inPutParametersNameArray.Add("CrossingDACStart");
            inPutParametersNameArray.Add("CrossingDacStep");
            inPutParametersNameArray.Add("CrossingDACMin");

           inPutParametersNameArray.Add("FlagAdjustJitter");
            inPutParametersNameArray.Add("JitterDacMax");
            inPutParametersNameArray.Add("JitterDacStart");
            inPutParametersNameArray.Add("JitterDacMin");
            inPutParametersNameArray.Add("JitterDacStep");


            dtCrossing.Columns.Add("Channel");
            dtCrossing.Columns.Add("TargetCrossingDac");
            dtCrossing.Columns.Add("TargetCrossing");
            dtCrossing.Columns.Add("TempPrameter");

            dtJitter.Columns.Add("Channel");
            dtJitter.Columns.Add("TargetJitterDac");
            dtJitter.Columns.Add("TargetJitter");
            dtJitter.Columns.Add("TempPrameter");

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


                for (byte i = 0; i < aEquipList.Count; i++)
                {

                    if (aEquipList.Keys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        pPs = (Powersupply)aEquipList.Values[i];
                        isOK = true;
                    }
                    if (aEquipList.Keys[i].ToUpper().Contains("SCOPE"))
                    {
                        pScope = (Scope)aEquipList.Values[i];

                    }
                    if (aEquipList.Keys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        aEquipList.Values[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }

                if (pPs != null && pScope != null)
                {
                    isOK = true;
                }
                else
                {
                    if (pPs == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    if (pScope == null)
                    {
                        logoStr += logger.AdapterLogString(3, "pScope =NULL");
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


                algorithm.GetSpec(specParameters, "Crossing(%)", 0, out MySpec.CrossingMax, out MySpec.CrossingTypcalValue, out MySpec.CrossingMin);

                algorithm.GetSpec(specParameters, "JitterRMS(ps)", 0, out MySpec.JitterMax, out MySpec.JitterTypcalValue, out MySpec.JitterMin);
               
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        protected bool Test()
        {
            if (!LoadPNSpec()) return false;
            
            if (!AnalysisInputParameters(inputParameters)) return false;

            if (!Adjust()) return false;

            if (!CurveAndWriteCoefs()) return false;
           
            return true;
        }

        #region  Adjust

        private bool Adjust()
        {
            if (MyStruct.flagAdjustCrossing)
            {

                if (!AdjustCrossing()) return false;

            }
            if (MyStruct.flagAdjustJitter)
            {
                if (!AdjustJitter()) return false;
            }
            return true;
        }

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

            for (int i = 0; i < Y.Length;i++ )
            {
                Diff.Add( Math.Abs(TypcalValue - Y[i]));
            }
            algorithm.SelectMinValue(Diff, out index);

            return index;
        }

        #region  AdjustCrossing

        private bool AdjustCrossing()
        {
            switch (MyStruct.AdjustCrossingMethod)
            {
                case 0:

                     if(!AdjustCrossing_Method_1())return false;

                    break;
                default:
                   
                    break;
            }
           
            return true;
        }

        private double GetCrossing()
        {
            byte Index;
            double[] Array1 = new double[5];
            
            for (int j = 0; j < 5; j++)
            {
                Thread.Sleep(500);
                Array1[j]= pScope.GetCrossing();
            }
            Array1.Min();


            return Convert.ToDouble(Array1.Min());
        }
         private bool AdjustCrossing_Method_1()//
        {
            bool flag = false;
            Int32 BestDac=0;
            double BestItemValue = 0;

            try
            {



                Int32 StartDac = MyStruct.CrossingDACStart;

                dut.WriteCrossDac(MyStruct.CrossingDACStart);
                pScope.AutoScale();
                Thread.Sleep(3000);
                double CurrentCrossing = GetCrossing();

                // TargetApdDac = MyStruct.WavelengthDacStart; 

                if (CurrentCrossing > MySpec.CrossingMin && CurrentCrossing < MySpec.CrossingMax && !MyStruct.flagPrecisionCrossing)
                {

                    logoStr += logger.AdapterLogString(4, "CurrentCrossing is " + CurrentCrossing);
                    return false;
                }
                else
                {
                    int DataCount = 1 + (MyStruct.CrossingDACMax - MyStruct.CrossingDACMin) / MyStruct.CrossingDacStep;
                    ScanDacArray = new Int32[DataCount];
                    ScanItemArray = new double[DataCount];

                    for (int i = 0; i < DataCount; i++)
                    {
                        ScanDacArray[i] = MyStruct.CrossingDACMin + MyStruct.CrossingDacStep * i;

                       
                        Thread.Sleep(200);
                        dut.WriteCrossDac(ScanDacArray[i]);

                        ScanItemArray[i] = GetCrossing();

                        logoStr += logger.AdapterLogString(1, "ScanDacArray[" + i + "] =" + ScanDacArray[i].ToString() + " " + "Crossing[" + i + "] =" + ScanItemArray[i].ToString());
                 
                    }

                }


                Index = GetTargetPointIndex(MySpec.CrossingTypcalValue, Array.ConvertAll(ScanItemArray, s => Convert.ToDouble(s)));

               

                if (Index == 0)//如果最好值为最小的一个点
                {
                    PrecisStep = (Int32)((ScanDacArray[1] - ScanDacArray[0]) / 10);

                   

                    for (int i = 0; i < 10; i++)
                    {

                        PrecisDAC_Array[i] = ScanDacArray[0] + i * PrecisStep;
                    }
                }
                else if (Index == ScanItemArray.Length)// 如果最好值为最大点
                {

                    PrecisStep = (Int32)((ScanDacArray[ScanDacArray.Length - 1] - ScanDacArray[ScanDacArray.Length - 2]) / 10);

                   for (int i = 0; i < 10; i++)
                   {

                       PrecisDAC_Array[i] = ScanDacArray[0] + i * PrecisStep;
                   }
                }
                else
                {


                    Int32 MinValue = (ScanDacArray[Index] + ScanDacArray[Index - 1]) / 2;
                    Int32 MaxValue = (ScanDacArray[Index + 1] + ScanDacArray[Index]) / 2;
                    PrecisStep = (MaxValue - MinValue) / 10;

                     for (int i = 0; i < 10; i++)
                     {

                         PrecisDAC_Array[i] = MinValue + i * PrecisStep;
                     }

                }

                for (int i = 0; i < 10; i++)
                {
                   
                    dut.WriteCrossDac(PrecisDAC_Array[i]);
                    Thread.Sleep(200);
                    pScope.ClearDisplay();
                    Thread.Sleep(2000);
                    PrecisItem_Array[i] = GetCrossing();
                    logoStr += logger.AdapterLogString(1, "PrecisDAC_Array[" + i.ToString() + "]=" + PrecisDAC_Array[i].ToString() + " " + "PrecisItem_Array[" + i.ToString() + "]=" + PrecisItem_Array[i].ToString());
                }

                Index = GetTargetPointIndex(MySpec.CrossingTypcalValue, Array.ConvertAll(PrecisItem_Array, s => Convert.ToDouble(s)));

                dut.StoreCrossDac(PrecisDAC_Array[Index]);
                BestItemValue = GetCrossing();
                logoStr += logger.AdapterLogString(1, "BestCrossingDAC=" + PrecisDAC_Array[Index].ToString() + " " + "BestCrossing=" + BestItemValue.ToString());
              


            }
             catch
            {
                return false;
            }

            return true;


        }
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

                 logoStr += logger.AdapterLogString(0, "ScanCrossingDAC =" + ScanArray[bIndex].ToString());
                 logoStr += logger.AdapterLogString(0, "ScanCrossing =" + Item[bIndex].ToString());
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

        #region  AdjustJitter

        private bool AdjustJitter()
        {
            switch (MyStruct.AdjustJitterMethod)
            {
                case 0:

                    if (!AdjustJitter_Method_1()) return false;

                    break;
                default:

                    break;
            }

            return true;
        }

        private bool AdjustJitter_Method_1()//
        {
            bool flag = false;
            Int32 BestDac = 0;
            double BestItemValue = 0;

            try
            {

                Int32 StartDac = MyStruct.JitterDacStart;

                dut.WriteEA(1,MyStruct.CrossingDACStart);

                double CurrentJitterRMS = pScope.GetJitterRSM();

                // TargetApdDac = MyStruct.WavelengthDacStart; 

                if (CurrentJitterRMS > MySpec.JitterMin && CurrentJitterRMS < MySpec.JitterMax && !MyStruct.flagPrecisionJitter)
                {

                    logoStr += logger.AdapterLogString(4, "CurrentJitterRMS is " + CurrentJitterRMS);

                }
                else
                {

                    int DataCount = (MyStruct.JitterDacMax - MyStruct.JitterDacMin) / MyStruct.JitterDacStep;

                    ScanDacArray = new Int32[DataCount];
                    ScanItemArray = new double[DataCount];

                    for (int i = 0; i < DataCount; i++)
                    {
                        ScanDacArray[i] = MyStruct.JitterDacMin + MyStruct.CrossingDacStep * i;
                        dut.WriteEA(1,ScanDacArray[i]);
                        Thread.Sleep(200);
                        ScanItemArray[i] = pScope.GetJitterRSM();
                        logoStr += logger.AdapterLogString(1, "ScanDacArray[" + i.ToString() + "]=" + ScanDacArray[i].ToString() + " " + "ScanItemArray[" + i.ToString() + "]=" + ScanItemArray[i].ToString());
                    }

                   

                  
                    GetTargetPointIndex(MySpec.JitterTypcalValue, Array.ConvertAll(ScanDacArray, s => Convert.ToDouble(s)));


                        BestItemValue = pScope.GetJitterRSM();

                        logoStr += logger.AdapterLogString(1, "CalculateBestPoint=" + BestDac);

                        if (BestDac >= MyStruct.JitterDacMax)
                        {
                            BestDac = MyStruct.JitterDacMax;
                        }
                        else if (BestDac <= MyStruct.JitterDacMin)
                        {
                            BestDac = MyStruct.JitterDacMin;
                        }

                        logoStr += logger.AdapterLogString(1, "BestDac=" + BestDac);

                        dut.WriteEA(2, BestDac);
                       
                    

                }

                flag = true;
            }
            catch
            {

                flag = true;
            }
            finally
            {

                DataRow dr = dtJitter.NewRow();

                dr["Channel"] = GlobalParameters.CurrentChannel;
                dr["TargetJitterDac"] = BestDac;
                dr["TargetJitter"] = BestItemValue;

                if (!GlobalParameters.UsingCelsiusTemp)
                {
                    ushort TempAdc;
                    dut.ReadTempADC(out TempAdc);
                    dr["TempPrameter"] = TempAdc;
                }
            }

            if (flag)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

#endregion

        #region  Curve

        private bool CurveAndWriteCoefs()
        {
            if (MyStruct.flagAdjustCrossing)
            {

                switch (MyStruct.AdjustCrossingMethod)
                {
                    case 0:// 无系数可写
                        break;
                    default:
                        break;
                }

               
            }

            if (MyStruct.flagAdjustJitter)
            {

                switch (MyStruct.AdjustJitterMethod)
                {
                    case 0:// 无系数可写
                        break;
                    default:
                        break;
                }


            }

            return true;
        }
#endregion
       
           
      
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
               
                bool isParametersComplete = true;

                if (isParametersComplete)
                {
                        string StrTemp;

                        if (!algorithm.Getconf(InformationList, "flagAdjustCrossing", false, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "flagAdjustCrossing is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.flagAdjustCrossing = bool.Parse(StrTemp);
                        }

                        //if (!algorithm.Getconf(InformationList, "flagPrecisionCrossing", true, out StrTemp))
                        //{
                        //    logoStr += logger.AdapterLogString(4, "flagPrecisionCrossing is wrong!");
                        //    return false;
                        //}

                        //else
                        //{
                        //    MyStruct.flagPrecisionCrossing = bool.Parse(StrTemp);
                        //}

                        MyStruct.flagPrecisionCrossing = true;
                        if (!algorithm.Getconf(InformationList, "CrossingDACMax", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "CrossingDacMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.CrossingDACMax = Int32.Parse(StrTemp);
                        }


                        if (!algorithm.Getconf(InformationList, "CrossingDACMin", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "CrossingDACMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.CrossingDACMin = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "CrossingDACStart", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "CrossingDACMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.CrossingDACStart = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "CrossingDacStep", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "CrossingDACMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.CrossingDacStep = Int32.Parse(StrTemp);
                        }


                        if (!algorithm.Getconf(InformationList, "flagAdjustJitter", false, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "WavelengthDacMax is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.flagAdjustJitter = bool.Parse(StrTemp);
                        }

                        //if (!algorithm.Getconf(InformationList, "flagPrecisionJitter", true, out StrTemp))
                        //{
                        //    logoStr += logger.AdapterLogString(4, "WavelengthDacMax is wrong!");
                        //    return false;
                        //}

                        //else
                        //{
                        //    MyStruct.flagPrecisionJitter = bool.Parse(StrTemp);
                        //}

                        MyStruct.flagPrecisionJitter = true;
                        if (!algorithm.Getconf(InformationList, "JitterDacMax", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "JitterDacMax is wrong!");
                            return false;
                        }
                        else
                        {
                            MyStruct.JitterDacMax = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "JitterDacStart", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "JitterDacStart is wrong!");
                            return false;
                        }
                        else
                        {
                            MyStruct.JitterDacStart = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "JitterDacMin", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "JitterDacMin is wrong!");
                            return false;
                        }
                        else
                        {
                            MyStruct.JitterDacMin = Int32.Parse(StrTemp);
                        }

                        if (!algorithm.Getconf(InformationList, "JitterDacStep", true, out StrTemp))
                        {
                            logoStr += logger.AdapterLogString(4, "JitterStep is wrong!");
                            return false;
                        }
                        else
                        {
                            MyStruct.JitterDacStep = Int32.Parse(StrTemp);
                        }



                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }

        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {



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
