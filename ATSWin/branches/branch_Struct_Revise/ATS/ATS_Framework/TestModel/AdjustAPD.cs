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
    public enum AdjustAPDSpecs : byte
    {
        Csen
    }
    public class AdjustAPD : TestModelBase
    {
#region Attribute       
        
        private SortedList<string, ArrayList> adjustAPDtValueRecordsStruct = new SortedList<string, ArrayList>();
        private AdjustAPDStruct adjustAPDStruct = new AdjustAPDStruct();
        private bool isApdAdjustOK;     
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private ArrayList tempratureADCArrayList = new ArrayList(); 
        private ArrayList inPutParametersNameArray = new ArrayList();       
        //cal apd
        private float apdPowerCoefA;
        private float apdPowerCoefB;
        private float apdPowerCoefC;
        private bool isCalApdPowerOk;
        private ArrayList apdCoefArray = new ArrayList();
        //cal apd           
       // procdata
        private double targetAttValue;
        private double startvalue = -25;
        private Int32 targetApd;
        private Int32 TargetErrorRate;
        ArrayList tempBerArray = new ArrayList();
        ArrayList tempApdDacArray = new ArrayList();
        // procdata
        private bool isWriteCoefCOk = false;
        private bool isWriteCoefBOk = false;
        private bool isWriteCoefAOk = false;
        private SortedList<byte, string> SpecNameArray = new SortedList<byte, string>();
       // equipments
        ErrorDetector pED;
        Attennuator pAtt;
        Powersupply pPS;

        private Int32[] ScanArray;
        private DataTable dtProcess;
        private ushort[] referenceRxADC ;
        private ushort[] referenceRxADC_First;
        private double referenceTemp;
#endregion

#region Method
        public AdjustAPD(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;  
            adjustAPDtValueRecordsStruct.Clear();
            tempratureADCArrayList.Clear();
            SpecNameArray.Clear();
            // input parameters
            inPutParametersNameArray.Clear();

            inPutParametersNameArray.Add("RxInputPower");
            inPutParametersNameArray.Add("SetPoints");
            inPutParametersNameArray.Add("ScanCount");
            inPutParametersNameArray.Add("Formula_X_Type");
            inPutParametersNameArray.Add("AdjustMethod");

            dtProcess = new DataTable();

            dtProcess.Columns.Add("Temp");
            dtProcess.Columns.Add("Channel");
            dtProcess.Columns.Add("APDDAC");
            dtProcess.Columns.Add("Formula_X");
            dtProcess.Columns.Add("RxADC");
      


            //...
            SpecNameArray.Add((byte)AdjustAPDSpecs.Csen, "Csen(dBm)");
           
        }
        override protected bool CheckEquipmentReadiness()
        {
            //check if all equipments are ready for this test; 
            //increase equipment referenced_times if ready
            //for (int i = 0; i < pEquipList.Count; i++)
            //    if (!pEquipList.Values[i].bReady) return false;

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
                    if (tempKeys[i].ToUpper().Contains("ERRORDETE"))
                    {
                        pED = (ErrorDetector)aEquipList.Values[i];

                    }
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                       // selectedEquipList.Add("ATTEN", tempValues[i]);
                        pAtt = (Attennuator)aEquipList.Values[i];
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        pPS = (Powersupply)aEquipList.Values[i];
                    }
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(2,GlobalParameters.CurrentChannel);
                    }
                    //OpticalSwitch 
                }
                 
                 if (pED != null && pAtt != null && pPS != null)
                {
                    isOK = true;

                }
                else
                {
                    if (pED == null)
                     {
                         Log.SaveLogToTxt("ERRORDETE =NULL");
                     }
                    if (pAtt == null)
                    {
                        Log.SaveLogToTxt("ATTEN =NULL");
                    }
                    if (pPS == null)
                    {
                        Log.SaveLogToTxt("POWERSUPPLY =NULL");
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
        public override bool Test()
       
        {
            
            logoStr = "";
            GenerateSpecList(SpecNameArray);
            AddCurrentTemprature();
            if (AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }
            if (!LoadPNSpec())
            {
                OutPutandFlushLog();
                return false;
            }            
            if (pED != null && pAtt != null &&pPS != null)
            {
               // close apc 
               
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));
                }               
               // close apc

                bool isAutoAlaign = pED.AutoAlaign(true);

                if (isAutoAlaign)
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                }
                else
                {
                    Log.SaveLogToTxt(isAutoAlaign.ToString());
                   
                    OutPutandFlushLog();
                    return isAutoAlaign;
                }
                pAtt.AttnValue(adjustAPDStruct.RxInputPower.ToString());

                ArrayList serchTargetAtts = new ArrayList();
                ArrayList serchTargetBers = new ArrayList();
                serchTargetAtts.Clear();
                serchTargetBers.Clear();


                switch(adjustAPDStruct.AdjustMethod)
                {
                    case 1:
                        if (!AdjustAPDMethod1()) return false;
                        break;
                    default:
                        if (!AdjustAPDMethod1()) return false;
                        break;
                }

                DataRow dr = dtProcess.NewRow();

                dr["Temp"] = GlobalParameters.CurrentTemp;
                dr["Channel"] = GlobalParameters.CurrentChannel;
                dr["APDDAC"] = targetApd;

                ushort k;
               
                switch (adjustAPDStruct.Formula_X_Type)
                { //0=TempAdc;1=ApdTempAdc;2=Current*256 
                    case 0:
                        dut.ReadTempADC(out k);
                        dr["Formula_X"] = k;
                        break;
                    case 1:
                        int TempK;
                        dut.ReadAPDTempAdc(out TempK);
                        dr["Formula_X"] = TempK;
                        break;
                    default:
                        dut.ReadTempADC(out k);
                        dr["Formula_X"] = k;
                        break;
                }
                //dr["Formula_X"] = k;

                dtProcess.Rows.Add(dr);



               if (!CurveAPDandWriteCoefs())
               {
                   OutPutandFlushLog();
               }
               OutPutandFlushLog();
               return true;
            }
            else
            {
                Log.SaveLogToTxt("Equipments are not enough!"); 
                isApdAdjustOK = false;               
                OutPutandFlushLog();
                return isApdAdjustOK;
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

        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            Log.SaveLogToTxt("Step1...Check InputParameters");

            try
            {


                if (InformationList.Length < inPutParametersNameArray.Count)
                {
                    Log.SaveLogToTxt("InputParameters are not enough!");
                    return false;
                }
                else
                {
                    int index = -1;
                    bool isParametersComplete = true;

                    if (isParametersComplete)
                    {

                        //for (byte i = 0; i < InformationList.Length; i++)
                        {

                            if (Algorithm.FindFileName(InformationList, "RxInputPower", out index))
                            {
                                adjustAPDStruct.RxInputPower = Convert.ToDouble(InformationList[index].DefaultValue);
                            }

                            if (Algorithm.FindFileName(InformationList, "SETPOINTS", out index))
                            {
                                char[] tempCharArray = new char[] { ',' };
                                ArrayList tempAL = Algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                                if (tempAL == null)
                                {
                                    Log.SaveLogToTxt(InformationList[index].FiledName + "is null");
                                    return false;
                                }
                                else
                                {
                                    adjustAPDStruct.SetPoints = new Int32[tempAL.Count];
                                    for (int i = 0; i < tempAL.Count;i++ )
                                    {
                                        adjustAPDStruct.SetPoints[i] = Int32.Parse(tempAL[i].ToString());
                                    }


                                 
                                }
                            }

                            if (Algorithm.FindFileName(InformationList, "ScanCount", out index))
                            {
                                adjustAPDStruct.ScanCount = Convert.ToByte(InformationList[index].DefaultValue);

                            }

                            if (Algorithm.FindFileName(InformationList, "Formula_X_Type", out index))
                            {
                                adjustAPDStruct.Formula_X_Type = Convert.ToByte(InformationList[index].DefaultValue);

                            }
                        }

                    }
                    Log.SaveLogToTxt("OK!");
                    return true;
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02001, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02001, error.StackTrace); 
            }

        }
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                procData = new TestModeEquipmentParameters[5];
                procData[0].FiledName = "TaretAPDDAC";
                procData[0].DefaultValue = targetApd.ToString();
              
              
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
        protected UInt32 EnumMethod(UInt32[] inPutArray, DUT dut, ErrorDetector ed, byte substep, out ArrayList berArray, out ArrayList apdDacArray, out double minBer)
        {
            berArray = new ArrayList();
            apdDacArray = new ArrayList();
            UInt32 minimumDAC;

            for (byte i = 0; i < inPutArray.Length; i++)
            {
                // add write APD
                dut.WriteAPDDac(inPutArray[i]);
                apdDacArray.Add(inPutArray[i]);
                ed.GetErrorRate();
                berArray.Add(ed.GetErrorRate());

            }

            minBer = -1;
            byte minIndex = 0;
            #region Select minimum value
            minBer = Algorithm.SelectMinValue(berArray, out minIndex);
            minimumDAC = inPutArray[minIndex];
            #endregion

            #region Fine Tune Target Value
            dut.WriteAPDDac(Algorithm.Uint16DataConvertoBytes((UInt16)(inPutArray[minIndex] + substep)));

            double tempBer1 = ed.GetErrorRate();
            dut.WriteAPDDac(Algorithm.Uint16DataConvertoBytes((UInt16)(inPutArray[minIndex] - substep)));
            double tempBer2 = ed.GetErrorRate();
            double mintempBer = Math.Min(tempBer1, tempBer2);
            if (mintempBer == tempBer1)
            {
                if (minBer > tempBer1)
                {
                    minimumDAC = Convert.ToUInt16(minimumDAC + substep);
                    apdDacArray.Add(minimumDAC);
                    dut.WriteAPDDac(minimumDAC);
                    minBer = ed.GetErrorRate();
                    berArray.Add(minBer);
                }

            }
            else
            {

                if (minBer > tempBer2)
                {
                    minimumDAC = Convert.ToUInt16(minimumDAC - substep);
                    apdDacArray.Add(minimumDAC);
                    dut.WriteAPDDac(minimumDAC);
                    minBer = ed.GetErrorRate();
                    berArray.Add(minBer);
                }
            }


            #endregion
            return minimumDAC;

        }
        protected bool SerchErrPoint()
        {

            return true;

        }
        protected bool SearchTargetPoint(out byte Index)
        {
          
            Index = 0;
            ArrayList serchAttPointsTemp=new ArrayList();
            serchAttPointsTemp.Clear();
            ArrayList serchAttPoints = serchAttPointsTemp;
            ArrayList serchBerPoints = serchAttPointsTemp;

         

            try
            {
                
           
                //double currentCense = 0;
                //byte count = 0;
                serchAttPoints = new ArrayList();
                serchBerPoints = new ArrayList();
                serchAttPoints.Clear();
                serchBerPoints.Clear();

                ArrayList StrLog = new ArrayList();
                ushort[] rxADCs = new ushort[adjustAPDStruct.SetPoints.Length];
                int count = 0;
                for (int i = 0; i < adjustAPDStruct.SetPoints.Length;i++ )
                {

                    dut.WriteAPDDac(adjustAPDStruct.SetPoints[i]);
                    serchAttPoints.Add(adjustAPDStruct.SetPoints[i]);
                    Thread.Sleep(200);

                    double TempValue= pED.GetErrorRate();

                    if (Math.Abs(TempValue - 9.999E+17) < 1.0E-6)
                    {
                        count++;//记录误码不通的数量
                    }

                    serchBerPoints.Add(TempValue);
                    dut.ReadRxpADC(out rxADCs[i]);
                    StrLog.Add("adjustAPDStruct.SetPoints[" + i + "] = " + adjustAPDStruct.SetPoints[i].ToString() + " " + " serchBerPoints[" + i + "] = " + TempValue + " RxADC[" + i + "] = " + rxADCs[i]);

                    
                }

                for (int i=0;i<StrLog.Count;i++)
                {
                    Log.SaveLogToTxt(StrLog[i].ToString());
                }

                if (count == adjustAPDStruct.SetPoints.Length)//如果全部不通
                {
                    return false;
                }
               // byte Index;

                Algorithm.SelectMinValue(serchBerPoints, out Index);

                if (referenceRxADC_First == null)
                {
                    referenceRxADC_First = new ushort[GlobalParameters.TotalChCount];
                    referenceTemp = GlobalParameters.CurrentTemp;
                }

                if (referenceTemp == GlobalParameters.CurrentTemp)
                {
                    referenceRxADC_First[GlobalParameters.CurrentChannel - 1] = rxADCs[Index];
                    return true;
                }

                ushort deltaRxADC = 65535;
                int index = Index;
                for (int i = 0; i < serchBerPoints.Count; i++)
                {
                    if (Math.Abs(Convert.ToDouble(serchBerPoints[i]) -Convert.ToDouble(serchBerPoints[Index])) < 10 * Convert.ToDouble(serchBerPoints[Index]))
                    {
                        int delta = Math.Abs(rxADCs[i] - referenceRxADC_First[GlobalParameters.CurrentChannel - 1]);
                        if (delta < deltaRxADC)
                        {
                            deltaRxADC = (ushort)delta;
                            index = i;
                        }
                    }
                }
                Log.SaveLogToTxt("BestAPD = " + adjustAPDStruct.SetPoints[index] + " serchBerPoints = " + serchBerPoints[index] + " RxADC = " + rxADCs[index]);

                Index = (byte)index;
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace); 
            }
           

        }
        /// <summary>
        /// 精细查找最好的那个点
        /// </summary>
        /// <param name="ScanArray">采样数组</param>
        /// <returns></returns>
        protected Int32 SearchBestPoint( Int32[] ScanArray)
        {
           
          
            double[]ErrorRate=new double[ScanArray.Length];
            ushort[] rxADCs = new ushort[ScanArray.Length];

            try
            {
                ArrayList StrLog = new ArrayList();
                for (int i = 0; i < ScanArray.Length; i++)
                {
                    dut.WriteAPDDac(ScanArray[i]);                   
                    Thread.Sleep(200);             
                    ErrorRate[i]= pED.GetErrorRate();
                    dut.ReadRxpADC(out rxADCs[i]);
                    StrLog.Add("ScanArray[" + i + "] = " + ScanArray[i] + " " + " ErrorRate[" + i + "] = " + ErrorRate[i] + " RxADC[" + i + "] = " + rxADCs[i]);                                   
                }              

              

                for (int i = 0; i < StrLog.Count; i++)
                {
                    Log.SaveLogToTxt(StrLog[i].ToString());
                }

                 byte bIndex;


                 ArrayList TempArray=new ArrayList();
                 TempArray.Clear();
                 for (int i = 0; i < ScanArray.Length;i++ )
                 {
                     TempArray.Add(ErrorRate[i]);
                 }


                 Algorithm.SelectMinValue(TempArray, out bIndex);

                 Log.SaveLogToTxt("BestAPD =" + ScanArray[bIndex].ToString());
                 Log.SaveLogToTxt("MinErrorRate =" + ErrorRate[bIndex].ToString());
                 //Log.SaveLogToTxt("ErrorRate[" + bIndex + "] =" + TempValue);

                if (referenceRxADC == null)
                {
                    referenceRxADC = new ushort[GlobalParameters.TotalChCount];
                    referenceTemp = GlobalParameters.CurrentTemp;
                }                

                if (referenceTemp == GlobalParameters.CurrentTemp)
                {
                    referenceRxADC[GlobalParameters.CurrentChannel - 1] = rxADCs[bIndex];                    
                    return int.Parse(ScanArray[bIndex].ToString());
                }                
                 
                ushort deltaRxADC = 65535;
                int index = bIndex;
                for (int i = 0; i < ErrorRate.Length; i++)
                {
                    if (Math.Abs(ErrorRate[i] - ErrorRate[bIndex]) < 10 * ErrorRate[bIndex])
                    {
                        int delta = Math.Abs(rxADCs[i] - referenceRxADC[GlobalParameters.CurrentChannel - 1]);
                        if (delta < deltaRxADC)
                        {
                            deltaRxADC = (ushort)delta;
                            index = i;
                        }
                    }
                }
                Log.SaveLogToTxt("BestAPD = " + ScanArray[index] + " ErrorRate = " + ErrorRate[index] + " RxADC = " + rxADCs[index]); 

                return int.Parse(ScanArray[index].ToString());      
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return InnoExCeption.NaN;
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return InnoExCeption.NaN;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02108, error.StackTrace); 
            }


        }        
        protected bool LoadPNSpec()
        {
            
            try
            {
                if (Algorithm.FindDataInDataTable(specParameters, SpecTableStructArray,Convert.ToString(GlobalParameters.CurrentChannel)) == null)
                {
                    return false;
                }
                
                {                

                    startvalue = SpecTableStructArray[(byte)AdjustAPDSpecs.Csen].TypicalValue;
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02000, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02000, error.StackTrace); 
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
        protected void AddCurrentTemprature()
        {
            try
            {
                #region  CheckTempChange
                if (!tempratureADCArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    Log.SaveLogToTxt("Step4...TempChanged Read tempratureADC");
                    Log.SaveLogToTxt("realtemprature=" + GlobalParameters.CurrentTemp.ToString());

                    UInt16 tempratureADC;
                    dut.ReadTempADC(out tempratureADC, 1);
                    Log.SaveLogToTxt("tempratureADC=" + tempratureADC.ToString());
                    tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper(), tempratureADC.ToString().Trim());
                    tempratureADCArrayList.Add(tempratureADC);
                }
                #endregion
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0xFFFFF, error.StackTrace); 
            }

        }
        protected void CollectCurvingParameters()
        {
            try
            {
                #region  add current channel
                if (!adjustAPDtValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    Log.SaveLogToTxt("Step6...add current channel records");
                    Log.SaveLogToTxt("GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                    adjustAPDtValueRecordsStruct.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), new ArrayList());
                    adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(targetApd);

                }
                else
                {
                    adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(targetApd);
                }


                #endregion
            }
            catch (InnoExCeption ex)//from driver
            {
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
            }
            catch (Exception error)//from itself
            {
                //one way: deal this exception itself
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02102, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02102, error.StackTrace); 
            }
        }
        protected bool CurveAPDandWriteCoefs()
        {
            try
            {
                #region  CurveCoef

                  string SelectCondition = "Channel=" + GlobalParameters.CurrentChannel;

                  DataRow[] drArray = dtProcess.Select(SelectCondition);


                  if (drArray.Length >= 2)
                  {
                      double[] Y = new double[drArray.Length];
                      double[] X = new double[drArray.Length];

                      for (byte i = 0; i < drArray.Length; i++)
                      {
                          Y[i] = double.Parse(drArray[i]["APDDAC"].ToString());
                          X[i] = double.Parse(drArray[i]["Formula_X"].ToString());//Formula_X
                      }

                      for (int i = 0; i < X.Length;i++ )
                      {
                             
                          Log.SaveLogToTxt("X[" + i.ToString() + "]=" + X[i].ToString() + " " + "Y[" + i.ToString() + "]=" + Y[i].ToString());
                   
                      }
                      double[] coefArray = Algorithm.MultiLine(X, Y, tempratureADCArray.Count, 2);
                      // double[] coefArray = Algorithm.MultiLine(tempTempAdcArray, tempAPDDacArray, tempratureADCArray.Count, 2);
                      apdPowerCoefC = (float)coefArray[0];
                      apdPowerCoefB = (float)coefArray[1];
                      apdPowerCoefA = (float)coefArray[2];
                      apdCoefArray = ArrayList.Adapter(coefArray);
                      apdCoefArray.Reverse();
                      for (byte i = 0; i < apdCoefArray.Count; i++)
                      {
                          Log.SaveLogToTxt("apdCoefArray[" + i.ToString() + "]=" + apdCoefArray[i].ToString() + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(apdCoefArray[i])));
                      }
                      Log.SaveLogToTxt("Step9...WriteCoef");

                      #region W&R Apddaccoefc
                      isWriteCoefCOk = dut.SetAPDdaccoefc(apdPowerCoefC.ToString());
                      if (isWriteCoefCOk)
                      {
                          Log.SaveLogToTxt("isWriteCoefCOk:" + isWriteCoefCOk.ToString());

                      }
                      else
                      {

                          Log.SaveLogToTxt("isWriteCoefCOk:" + isWriteCoefCOk.ToString());
                      }
                      #endregion
                      #region W&R Apddaccoefb
                      isWriteCoefBOk = dut.SetAPDdaccoefb(apdPowerCoefB.ToString());
                      if (isWriteCoefBOk)
                      {

                          Log.SaveLogToTxt("isWriteCoefBOk:" + isWriteCoefBOk.ToString());

                      }
                      else
                      {

                          Log.SaveLogToTxt("isWriteCoefBOk:" + isWriteCoefBOk.ToString());
                      }
                      #endregion
                      #region W&R Apddaccoefa
                      isWriteCoefAOk = dut.SetAPDdaccoefa(apdPowerCoefA.ToString());

                      if (isWriteCoefAOk)
                      {
                          Log.SaveLogToTxt("isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                      }
                      else
                      {
                          Log.SaveLogToTxt("isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                      }
                      #endregion
                      if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                      {
                          Log.SaveLogToTxt("isCalApdPowerOk:" + true.ToString());
                      }
                      else
                      {
                          Log.SaveLogToTxt("isCalApdPowerOk:" + false.ToString());
                          return false;
                      }


                  }

                #endregion
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02100, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02100, error.StackTrace); 
            }
        }

        private bool AdjustAPDMethod1()
        {
            try
            {
                byte Index;
                if (!SearchTargetPoint(out Index))// 搜寻Testplan 中填写的 参考点
                {
                    return false;
                }

                ScanArray = new Int32[adjustAPDStruct.ScanCount];

                if (Index == 0)//如果最好值为最小的一个点
                {
                    Int32 Step = (adjustAPDStruct.SetPoints[1] - adjustAPDStruct.SetPoints[0]) / adjustAPDStruct.ScanCount;


                    for (int i = 0; i < ScanArray.Length; i++)
                    {

                        ScanArray[i] = adjustAPDStruct.SetPoints[0] + i * Step;
                    }
                }
                else if (Index == adjustAPDStruct.SetPoints.Length - 1)// 如果最好值为最大点
                {

                    Int32 Step = (adjustAPDStruct.SetPoints[Index] - adjustAPDStruct.SetPoints[Index - 1]) / adjustAPDStruct.ScanCount;


                    for (int i = 0; i < ScanArray.Length; i++)
                    {

                        ScanArray[i] = adjustAPDStruct.SetPoints[Index - 1] + i * Step;
                    }
                }
                else
                {


                    Int32 MinValue = (adjustAPDStruct.SetPoints[Index] + adjustAPDStruct.SetPoints[Index - 1]) / 2;
                    Int32 MaxValue = (adjustAPDStruct.SetPoints[Index + 1] + adjustAPDStruct.SetPoints[Index]) / 2;
                    Int32 Step = (MaxValue - MinValue) / adjustAPDStruct.ScanCount;

                    for (int i = 0; i < ScanArray.Length; i++)
                    {
                        ScanArray[i] = MinValue + i * Step;
                    }

                }


                targetApd = SearchBestPoint(ScanArray.ToArray());


                dut.StoreAPDDac(targetApd);

                isApdAdjustOK = true;
                Thread.Sleep(30000);
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02109, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02109, error.StackTrace); 
            }
        }

        public override List<InnoExCeption> GetException()
        {
            return base.GetException();
        }
#endregion
    }
}
