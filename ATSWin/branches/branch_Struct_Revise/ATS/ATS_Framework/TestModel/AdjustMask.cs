
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
    public class AdjustMask : TestModelBase
    {
        #region Attribute

       private struct Spec
        {
          
           public double SpecMax, SpecMin, TypcalValue;
           public double AdjustSpecMax, AdjustSpecMin;
         
        }

       private AdjustMaskStuct MyStruct = new AdjustMaskStuct();
       private Spec MySpec= new Spec();
        
       // private OptimizsEyeEYEStruce MyStruct = new OptimizsEyeEYEStruce();

        private Int32 TargetDac;
        private double TargetMask;
        
        private ArrayList inPutParametersNameArray = new ArrayList();

        //equipments
        private Powersupply pPs;
        private Scope pScope;
     

        private DataTable dtMask=new DataTable();
        private DataTable dtJitter = new DataTable();
        private byte DacIndex;

      
        private Int32 PrecisStep;
        private ArrayList logArray = new ArrayList();
        #endregion

        #region Method

        public AdjustMask(DUT inPuDut)
        {
            
            logoStr = null;

            dut = inPuDut;

            inPutParametersNameArray.Clear();
           
            inPutParametersNameArray.Add("MaskDACMax");
            inPutParametersNameArray.Add("MaskDACStart");
            inPutParametersNameArray.Add("MaskDACStep");
            inPutParametersNameArray.Add("MaskDACMin");
            inPutParametersNameArray.Add("MaskLimit");
            inPutParametersNameArray.Add("AdjustMaskMethod");
            inPutParametersNameArray.Add("ICType");
           

            dtMask.Columns.Add("Channel");
            dtMask.Columns.Add("TargetMaskDac");
            dtMask.Columns.Add("TargetMask");
            dtMask.Columns.Add("TempPrameter");
            dtMask.Columns.Add("TempADC");
        

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
                        Log.SaveLogToTxt("POWERSUPPLY =NULL");
                    }
                    if (pScope == null)
                    {
                        Log.SaveLogToTxt("pScope =NULL");
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
            CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));
            if (!Test())
            {
                pScope.SetRunTilOff();
                pScope.MaskONOFF(false);
                pScope.RunStop(false);
                OutPutandFlushLog();
                return false;

            }
            else
            {
                pScope.SetRunTilOff();
                pScope.MaskONOFF(false);
                pScope.RunStop(false);
                OutPutandFlushLog();
                return true;
            }

        }
        private bool LoadPNSpec()
        {

            try
            {


                Algorithm.GetSpec(specParameters, "MaskMargin(%)", 0, out MySpec.SpecMax, out MySpec.TypcalValue, out MySpec.SpecMin);

                MySpec.AdjustSpecMax = MySpec.SpecMax ;
                MySpec.AdjustSpecMin = MySpec.SpecMin ;
                //Algorithm.GetSpec(specParameters, "JitterRMS(ps)", 0, out MySpec.JitterMax, out MySpec.JitterTypcalValue, out MySpec.JitterMin);
               
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
        
      public override bool  Test()

        {
          
            
            if (!AnalysisInputParameters(inputParameters)) return false;
            if (!LoadPNSpec()) return false;
            if (!Adjust())
            {
                TargetDac = MyStruct.DACStart;
                dut.StoreMaskDac(MyStruct.DACStart);
                TargetMask = GetMask();
                return false;
            }
            else
            {
                DataRow dr = dtMask.NewRow();
                dr["Channel"] = GlobalParameters.CurrentChannel;
                dr["TargetMaskDac"] = TargetDac;
                dr["TargetMask"] = TargetMask;
                ushort TempValue;
                dut.ReadTempADC(out TempValue);
                dr["TempADC"] = TempValue;
                dtMask.Rows.Add(dr);
             }
            if (!CalculatedCoefs()) return false;
           
            return true;
        }

        #region  Adjust

        private bool Adjust()
        {
          

            switch (MyStruct.AdjustMethod)
            {
                case 0:
                    if (!AdjustMethod_0()) return false;
                    break;
                default:
                    if (!AdjustMethod_0()) return false;
                    break;
            }

            return true;
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

            ArrayList Diff = new ArrayList();

            for (int i = 0; i < Y.Length;i++ )
            {
                Diff.Add( Math.Abs(TypcalValue - Y[i]));
            }
            Algorithm.SelectMinValue(Diff, out index);

            return index;
        }

        #region  AdjustMask

 
        private int GetMask()
        {
            byte Index;
            int[] Array1 = new int[2];
           // pScope.DisplayMask();
            for (int j = 0; j < 2; j++)
            {
               // Thread.Sleep(100);
                pScope.AutoScale();
                Array1[j] = pScope.GetMask();
            }
           // Array1.Min();


            return Convert.ToInt16(Array1.Min());
        }

        private bool AdjustMask_Precision(double TargetMask, Int32 index, Int32[] DACArray, double[] itemArray)
        {
            int PrecisDAC_Array_length=0;
            Int32 MinValue, MaxValue;
            Int32[] PrecisDAC_Array ;//= new Int32[10];
            double[] PrecisItem_Array;// = new double[10];

            try
            {

                if (index == 0)//如果最好值为最小的一个点
                {
                    MinValue = DACArray[0];
                    MaxValue = DACArray[1];
                }
                else if (index == itemArray.Length-1)// 如果最好值为最大点
                {
                    MinValue = DACArray[DACArray.Length - 2];
                    MaxValue = DACArray[DACArray.Length - 1];
                }
                else// 中间值
                {
                     MinValue = (DACArray[index] + DACArray[index - 1]) / 2;
                     MaxValue = (DACArray[index + 1] + DACArray[index]) / 2;
                }

                PrecisStep = (Int32)((MaxValue - MinValue) / 5);

                if (PrecisStep <= 1)
                {
                    PrecisStep = 1;
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
                    PrecisDAC_Array[i] = MinValue + i * PrecisStep;
                }
                for (int i = 0; i < PrecisDAC_Array_length; i++)
                {
                   // dut.WriteMaskDac(PrecisDAC_Array[i]);
                    WriteMaskDAC(PrecisDAC_Array[i]);
                    Thread.Sleep(200);
                    pScope.ClearDisplay();
                    Thread.Sleep(2000);
                    PrecisItem_Array[i] = GetMask();
                    Log.SaveLogToTxt("PrecisDAC_Array[" + i.ToString() + "]=" + PrecisDAC_Array[i].ToString() + " " + "PrecisItem_Array[" + i.ToString() + "]=" + PrecisItem_Array[i].ToString());
                }

               byte Maskindex = GetTargetPointIndex(MySpec.TypcalValue, Array.ConvertAll(PrecisItem_Array, s => Convert.ToDouble(s)));


               //dut.StoreMaskDac(PrecisDAC_Array[Maskindex]);
               StoreMaskDAC(PrecisDAC_Array[Maskindex]);
               TargetDac = PrecisDAC_Array[Maskindex];
               TargetMask = PrecisItem_Array[Maskindex];

             
              


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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02111, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02111, error.StackTrace); 
            }
            return true;
        }

       
        private bool AdjustMethod_0()//
        {
            bool flag = false;
            Int32 BestDac = 0;
            double BestItemValue = 0;
            Int32[] ScanDacArray;
            double[] ScanItemArray;

            //try
            //{

                Int32 StartDac = MyStruct.DACStart;

               // dut.WriteMaskDac(MyStruct.DACStart);
                WriteMaskDAC(MyStruct.DACStart);
               pScope. EyeTuningDisplay(0);
                pScope.AutoScale();
                Thread.Sleep(3000);
                pScope.SetMaskLimmit(MyStruct.MaskLimit);
                pScope.LoadMask();
                
               // pScope.AcquisitionLimitTestSwitch(1, syn);
              //  pScope.RunStop(true);
                double CurrentMask = GetMask();

                // TargetApdDac = MyStruct.WavelengthDacStart; 

                if (CurrentMask > MySpec.AdjustSpecMax && CurrentMask < MySpec.AdjustSpecMin)
                {

                    Log.SaveLogToTxt("CurrentMask is " + CurrentMask);
                    return false;
                }
                else
                {
                    int DataCount = 1 + (MyStruct.DACMax - MyStruct.DACMin) / MyStruct.DacStep;
                    ScanDacArray = new Int32[DataCount];
                    ScanItemArray = new double[DataCount];

                    for (int i = 0; i < DataCount; i++)
                    {
                        ScanDacArray[i] = MyStruct.DACMin + MyStruct.DacStep * i;


                        Thread.Sleep(200);
                       // dut.WriteMaskDac(ScanDacArray[i]);
                        WriteMaskDAC(ScanDacArray[i]);
                        ScanItemArray[i] = GetMask();

                        logArray.Clear();
                        logArray.Add("ScanDacArray[" + i + "] =" + ScanDacArray[i].ToString() + " " + "Mask[" + i + "] =" + ScanItemArray[i].ToString());
                        //Log.SaveLogToTxt("ScanDacArray[" + i + "] =" + ScanDacArray[i].ToString() + " " + "Mask[" + i + "] =" + ScanItemArray[i].ToString());

                    }

                    for (int i = 0; i < logArray.Count; i++)
                    {

                        Log.SaveLogToTxt(logArray[i].ToString());
                    }

                }

                DacIndex = GetTargetPointIndex(MySpec.TypcalValue, Array.ConvertAll(ScanItemArray, s => Convert.ToDouble(s)));
               // dut.StoreMaskDac(ScanDacArray[DacIndex]);

               
                TargetDac = ScanDacArray[DacIndex];
                TargetMask = ScanItemArray[DacIndex];
                StoreMaskDAC(ScanDacArray[DacIndex]);

                if (MyStruct.DacStep>4)
                {
                    AdjustMask_Precision(MySpec.TypcalValue, DacIndex, ScanDacArray, ScanItemArray);
                }


            return true;


        }
         protected byte SearchMaskPoint(Int32[] ScanArray,out double[]Item)
         {


            Item = new double[ScanArray.Length];

             try
             {
                 ArrayList StrLog = new ArrayList();

                 for (int i = 0; i < ScanArray.Length; i++)
                 {

                     dut.WriteMaskDac(ScanArray[i]);

                     Thread.Sleep(200);


                     Item[i] = GetMask();

                     StrLog.Add("ScanArray[" + i + "] =" + ScanArray[i].ToString() + " " + "Mask[" + i + "] =" + Item[i].ToString());

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

                 Log.SaveLogToTxt("ScanMaskDAC =" + ScanArray[bIndex].ToString());
                 Log.SaveLogToTxt("ScanMask =" + Item[bIndex].ToString());
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

#region TxIc GN1180  ,此方法为 IC1180 定制,使用在制定的方法一中

        private bool WriteMaskDAC(int DAC)
        {
            //string IcName="NA";

            
            dut.WriteMaskDac(DAC);
                 
          
            return true;
        }
        private bool StoreMaskDAC(int DAC)
        {
           
                    dut.StoreMaskDac(DAC);
            
          
         
            return true;
        }
#endregion

#endregion

        #region  calculated && Write coefficient

        private bool CalculatedCoefs()
        {
            switch (MyStruct.AdjustMethod)
            {
                case 0:  
                      string SelectCondition = "Channel=" + GlobalParameters.CurrentChannel;
                
                       DataRow[] drArray = dtMask.Select(SelectCondition);

                       if (drArray.Length < 2)
                       {
                           break;
                       }

                     #region  TempCout>=2

                       string[] TempADCArray = drArray.Select(A => A["TempADC"].ToString()).ToArray();
                       double[] X = Array.ConvertAll<String, double>(TempADCArray, s => double.Parse(s));

                       string[] TargetMaskDacArray = drArray.Select(A => A["TargetMaskDac"].ToString()).ToArray();
                       double[] Y = Array.ConvertAll<String, double>(TargetMaskDacArray, s => double.Parse(s));

                       double[] coefArray = Algorithm.MultiLine(X, Y,X.Length, 2);

                       //coefArray.Reverse();

                       Array.Reverse(coefArray);
                       float[] floatArray = Array.ConvertAll<double, float>(coefArray, s => float.Parse(s.ToString()));

                       WriteCoef(floatArray);
                       pPs.OutPutSwitch(false);
                       pPs.OutPutSwitch(true);
                     #endregion

                    break;
                case 1:// 无系数可写

                    break;
                default:
                    break;
            }

               
            


            return true;
        }

        private bool WriteCoef(float[]Coef)
        {

            float CoefA = (float)Coef[0]; 
            float CoefB = (float)Coef[1];
            float CoefC = (float)Coef[2];


            Log.SaveLogToTxt("CoefA=" + CoefA + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(CoefA)));
            Log.SaveLogToTxt("CoefB=" + CoefB + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(CoefB)));
            Log.SaveLogToTxt("CoefC=" + CoefC + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(CoefC)));


            if (!dut.SetMaskcoefa(CoefA))
            {
                Log.SaveLogToTxt("CoefA Write Error!");
          
                return false;
            }
            if (!dut.SetMaskcoefb(CoefB))
            {
                Log.SaveLogToTxt("CoefB Write Error!");

                return false;
            }

            if (!dut.SetMaskcoefc(CoefC))
            {
                Log.SaveLogToTxt("CoefC Write Error!");

                return false;
            }

            return true;
        }
#endregion
       
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {

            Log.SaveLogToTxt("Step1...Check InputParameters");

            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                Log.SaveLogToTxt("InputParameters are not enough!");
                return false;
            }
            else
            {
               
                bool isParametersComplete = true;

                if (isParametersComplete)
                {
                        string StrTemp;

                        if (!Algorithm.Getconf(InformationList, "MaskLimit", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("MaskLimit is wrong!");
                            return false;
                        }

                        else
                        {
                             MyStruct.MaskLimit = int.Parse(StrTemp);
                        }


                        if (!Algorithm.Getconf(InformationList, "MaskDACMax", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("MaskDacMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.DACMax = Int32.Parse(StrTemp);
                        }

                        if (!Algorithm.Getconf(InformationList, "MaskDACMin", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("MaskDACMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.DACMin = Int32.Parse(StrTemp);
                        }

                        if (!Algorithm.Getconf(InformationList, "MaskDACStart", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("MaskDACMin is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.DACStart = Int32.Parse(StrTemp);
                        }
                        if (!Algorithm.Getconf(InformationList, "AdjustMaskMethod", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("AdjustMethod is wrong!");
                            return false;
                        }

                        else
                        {
                            MyStruct.AdjustMethod = byte.Parse(StrTemp);
                        }  
                    //
                        if (!Algorithm.Getconf(InformationList, "MaskDacStep", true, out StrTemp))
                        {
                            Log.SaveLogToTxt("MaskDACMin is wrong!");
                            return false;
                        }
                        else
                        {
                            MyStruct.DacStep = Int32.Parse(StrTemp);
                        }

                        //if (!Algorithm.Getconf(InformationList, "AdjustMaskMethod", true, out StrTemp))////AdjustMaskMethod
                        //{
                        //    Log.SaveLogToTxt("AdjustMaskMethod is wrong!");
                        //    return false;
                        //}
                        //else
                        //{
                        //    MyStruct.AdjustMethod = byte.Parse(StrTemp);
                        //}
                        //ICType
                        if (!Algorithm.Getconf(InformationList, "ICType", true, out StrTemp))////AdjustMaskMethod
                        {
                            Log.SaveLogToTxt("ICType is wrong!");
                            return false;
                        }
                        else
                        {
                            MyStruct.IC_Type = byte.Parse(StrTemp);
                        }
                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        }

        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                outputParameters = new TestModeEquipmentParameters[2];
            
                outputParameters[1].FiledName = "TargetDAC";
                outputParameters[1].DefaultValue =TargetDac.ToString();
                outputParameters[0].FiledName = "Mask(%)";
                outputParameters[0].DefaultValue = TargetMask.ToString();


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
