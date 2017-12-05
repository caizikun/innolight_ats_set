using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
//using Ivi.Visa.Interop;
using System.Collections;
namespace ATS_Framework
{
    public enum AdjustTxPowerDmiSpecs : byte
    {
        AP      
    }
   public class AdjustTxPowerDmi : TestModelBase
    {       
#region Attribute
       private SortedList<string, AdjustTxPowerDmitValueRecordsStruct> adjustTxPowerDmitValueRecordsStruct=new SortedList<string, AdjustTxPowerDmitValueRecordsStruct>();
        private AdjustTxPowerDmiStruct adjustTxPowerDmiStruct=new AdjustTxPowerDmiStruct();
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private SortedList<string, string> allChannelFixedIMod = new SortedList<string, string>();
        private ArrayList tempratureADCArrayList = new ArrayList();
        private ArrayList realtempratureArrayList = new ArrayList();
        private double[] txPowerADC;       
        private double[] txPoweruw;      
        private bool isAdjustTxPowerDmiOk = false;        
        private ArrayList inPutParametersNameArray = new ArrayList();        
       // cal coef
        private float txDmiSlopeCoefA;
        private float txDmiSlopeCoefB;
        private float txDmiSlopeCoefC;
        private float txDmiOffsetCoefA;
        private float txDmiOffsetCoefB;
        private float txDmiOffsetCoefC;
        private bool isCalTxDmiOk;
        private bool isWriteCoefCOk = false;
        private bool isWriteCoefBOk = false;
        private bool isWriteCoefAOk = false;
        private bool isWriteCoefOffsetCOk = false;
        private bool isWriteCoefOffsetBOk = false;
        private bool isWriteCoefOffsetAOk = false;
        private bool isWriteA = false;
        private ArrayList tempCoefBArray = new ArrayList();
        private ArrayList tempCoefCArray = new ArrayList();
        private ArrayList txDmiSlopeCoefArray = new ArrayList();
        private ArrayList txDmiOffsetCoefArray = new ArrayList();
        private double TempReference;
        private double TxLOPTarget = 0;
       // cal coef
       //equipments
        private Scope tempScope;
        private Powersupply tempps;
        private SortedList<byte, string> SpecNameArray = new SortedList<byte, string>();
#endregion
        
#region Method
        public AdjustTxPowerDmi(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;  
            adjustTxPowerDmitValueRecordsStruct.Clear();
            tempratureADCArrayList.Clear();
            inPutParametersNameArray.Clear();
            allChannelFixedIMod.Clear();
            realtempratureArrayList.Clear();
            SpecNameArray.Clear();
            inPutParametersNameArray.Add("BIASSETPOINTS");
            inPutParametersNameArray.Add("FIXEDIMODDACARRAY");
            inPutParametersNameArray.Add("ISTRACINGERR");
            inPutParametersNameArray.Add("ISNEWAlgorithm");
            inPutParametersNameArray.Add("HIGHESTCALTEMP");
            inPutParametersNameArray.Add("LOWESTCALTEMP");
            inPutParametersNameArray.Add("SLEEPTIME");


            SpecNameArray.Add((byte)AdjustEyeSpecs.AP, "AP(dBm)");
           
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

                 tempScope = (Scope)selectedEquipList["SCOPE"];
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempScope != null && tempps != null)
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
        protected override bool StartTest()
        {
            
            logoStr = "";
            GenerateSpecList(SpecNameArray);
            AddCurrentTemprature();
            if (AnalysisInputParameters(inputParameters) == false || LoadPNSpec()==false)
            {
                OutPutandFlushLog();
                return false;
            }
            if (PrepareEnvironment(selectedEquipList)==false)
            {               
                Log.SaveLogToTxt("PrepareEnvironment Error!");
                OutPutandFlushLog();
                return false;
            }
            if (AdapterAllChannelFixedIBiasImod()==false)
            {                
                Log.SaveLogToTxt("PrepareEnvironment Error!");
                OutPutandFlushLog();
                return false;
            }
            
            if (tempScope != null && tempps != null)
            {
                // close apc 
                
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF)); 
                }
                 
                // close apc
                // write IModDAC
                if (allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    dut.WriteModDac(Convert.ToUInt32(allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()]));
                } 
                else
                {
                    dut.WriteModDac(0);
                }
                
                // write IModDAC
               if (adjustTxPowerDmiStruct.ISNewAlgorithm)
               {
                 
                   if (tempratureADCArrayList.Count==1)
                   {
                       ReadTxPowerFromDCA(tempScope);
                       CollectCurvingParameters();
                       if (!IsMonotonically())
                       {
                           MessageBox.Show("txpower is not monotonically");
                       }
                   }
                   else if (tempratureADCArrayList.Count >= 2)
                   {
                       dut.WriteBiasDac((adjustTxPowerDmiStruct.ArrayIbias[0]));
                       dut.WriteModDac((adjustTxPowerDmiStruct.ArrayFixedModDac[0]));
                       SetSleep(adjustTxPowerDmiStruct.SleepTime);
                   }
                  
               } 
               else
               {
                   ReadTxPowerFromDCA(tempScope);                 
                   CollectCurvingParameters();
                   if (!IsMonotonically())
                   {
                       MessageBox.Show("txpower is not monotonically");
                   }
               }

               if (!CurveTxPowerDMIandWriteCoefs(tempps, tempScope))
              {                  
                  Log.SaveLogToTxt("write coefs Error!");
                  OutPutandFlushLog();
                  return false;
              }
               OutPutandFlushLog();
                return true;
            }
            else
            {
                isAdjustTxPowerDmiOk = false;
                Log.SaveLogToTxt("Equipments are not enough!");
                OutPutandFlushLog();
                return isAdjustTxPowerDmiOk;
            }
        }
       
        protected bool PrepareEnvironment(EquipmentList aEquipList,byte mode=0)
        {
            Scope tempScope = (Scope)selectedEquipList["SCOPE"];
            if (tempScope != null)
            {
                
                if (tempScope.SetMaskAlignMethod(1) &&
                    tempScope.SetMode(mode) &&
                    tempScope.MaskONOFF(false) &&
                    tempScope.SetRunTilOff() &&
                    tempScope.RunStop(true) &&
                    tempScope.OpenOpticalChannel(true) &&
                    tempScope.RunStop(true) &&
                    tempScope.ClearDisplay()&&
                    tempScope.DisplayPowerdbm() &&
                      tempScope.EyeTuningDisplay(1)          
                    )
                {
                    return true;
                }
                else
                {
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



                        if (Algorithm.FindFileName(InformationList, "BIASSETPOINTS", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAR = Algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAR==null||tempAR.Count<1)
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is null!");
                                return false;
                            }
                            else if ( tempAR.Count>3)
                            {
                                adjustTxPowerDmiStruct.ArrayIbias = new ArrayList();
                                for (int i = 0; i < 3;i++ )
                                {
                                    adjustTxPowerDmiStruct.ArrayIbias.Add(tempAR[i]);
                                }
                                
                            }
                            else
                            {
                                adjustTxPowerDmiStruct.ArrayIbias = tempAR;
                            }
                         
                           
                        }
                        if (Algorithm.FindFileName(InformationList, "FIXEDIMODDACARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAR= Algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);   
                            if (tempAR==null)
                            {
                                Log.SaveLogToTxt(InformationList[index].FiledName + "is null!");
                                return false;
                            }
                            else if (tempAR.Count > GlobalParameters.TotalChCount)
                            {
                                adjustTxPowerDmiStruct.ArrayFixedModDac = new ArrayList();
                                for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                                {
                                    adjustTxPowerDmiStruct.ArrayFixedModDac.Add(tempAR[i]);
                                }
                            }
                            else
                            {
                                adjustTxPowerDmiStruct.ArrayFixedModDac = tempAR;  
                            }
                        }


                        if (Algorithm.FindFileName(InformationList, "ISTRACINGERR", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.ToUpper().Trim() == "1" || temp.ToUpper().Trim() == "TRUE")
                            {
                                adjustTxPowerDmiStruct.isTracingErr=true;
                            } 
                            else
                            {
                                adjustTxPowerDmiStruct.isTracingErr = false;
                            }
                           
                        }


                        if (Algorithm.FindFileName(InformationList, "HIGHESTCALTEMP", out index))
                        {
                            adjustTxPowerDmiStruct.HighestCalTemp = Convert.ToDouble(InformationList[index].DefaultValue);

                        }
                        if (Algorithm.FindFileName(InformationList, "LOWESTCALTEMP", out index))
                        {
                            adjustTxPowerDmiStruct.LowestCalTemp = Convert.ToDouble(InformationList[index].DefaultValue);

                        }
                        if (Algorithm.FindFileName(InformationList, "ISNEWAlgorithm", out index))
                        {
                            string temp = InformationList[index].DefaultValue;
                            if (temp.ToUpper().Trim() == "1"||temp.ToUpper().Trim() =="TRUE")
                            {
                                adjustTxPowerDmiStruct.ISNewAlgorithm  = true;
                            }
                            else
                            {
                                 adjustTxPowerDmiStruct.ISNewAlgorithm  = false;
                            }
                         
                        }
                        if (Algorithm.FindFileName(InformationList, "SLEEPTIME", out index))
                        {
                            adjustTxPowerDmiStruct.SleepTime = Convert.ToUInt16(InformationList[index].DefaultValue);

                        }
                    }

                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        }
       private double CalTempFitA(double currentTemp,Scope inputScope,DUT inputDut)
       {
           double TempTxPowerUW = 0;
           double TempTxDmi = 0;
           double a=0;
            TempTxPowerUW=inputScope.GetAveragePowerWatt();
            if (TempTxPowerUW >= 10000000)
           {
               for (byte j = 0; j < 4; j++)
               {
                   inputScope.SetScaleOffset(TxLOPTarget,1);
                   TempTxPowerUW = inputScope.GetAveragePowerWatt();
                   if (TempTxPowerUW >= 10000000)
                   {
                       inputScope.AutoScale(1);
                       SetSleep(adjustTxPowerDmiStruct.SleepTime);
                       TempTxPowerUW = inputScope.GetAveragePowerWatt();
                       if (TempTxPowerUW >= 10000000)
                       {
                           SetSleep(adjustTxPowerDmiStruct.SleepTime);
                           continue;
                       }
                   }
                   else
                   {
                       break;
                   }
               }
               if (TempTxPowerUW >= 10000000)
               {
                   MessageBox.Show("DCA ReadTxPowerError");
               } 
           }
           TempTxDmi = inputDut.ReadDmiTxp();
           a = ((TempTxPowerUW * 10 - Algorithm.ChangeDbmtoUw(TempTxDmi) * 10) / (Algorithm.ChangeDbmtoUw(TempTxDmi) * 10) )/ (currentTemp * 256 - TempReference);
           return a;
       }
       private void SetSleep(UInt16 sleeptime = 100)
       {
           Thread.Sleep(sleeptime);
       }     
       protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
       {

           try
           {
               procData = new TestModeEquipmentParameters[4];
               procData[0].FiledName = "DCA_TXPWER_BY_SETPOINTS";
               if (txPoweruw == null || txPoweruw.Length==0)
               {
                   procData[0].DefaultValue = "";
               } 
               else
               {
                   procData[0].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(ArrayList.Adapter(txPoweruw), ",");
               }               
               procData[1].FiledName = "TXPOWERADCARRAY";
               if (txPowerADC == null || txPowerADC.Length==0)
               {
                   procData[1].DefaultValue = "";
               } 
               else
               {
                   procData[1].DefaultValue = Algorithm.ArrayListToStringArraySegregateByPunctuations(ArrayList.Adapter(txPowerADC), ",");
               }
               
               procData[2].FiledName = "TEMPADC";
               if (tempratureADCArray == null || tempratureADCArray.Count == 0)
               {
                   procData[2].DefaultValue = "";
               }
               else
               {
                   procData[2].DefaultValue = tempratureADCArray[Convert.ToString(GlobalParameters.CurrentTemp)];
               }
               
               procData[3].FiledName = "REFERENCETEMPERATURE";
               procData[3].DefaultValue = Convert.ToString(TempReference);
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
       private bool AdapterAllChannelFixedIBiasImod()
       {
           if (adjustTxPowerDmiStruct.ArrayFixedModDac == null || adjustTxPowerDmiStruct.ArrayFixedModDac.Count == 0)
           {
               return false;
           }
           
           if (!allChannelFixedIMod.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
           {

               allChannelFixedIMod.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), adjustTxPowerDmiStruct.ArrayFixedModDac[allChannelFixedIMod.Count].ToString().Trim());

           }
           else
           {
               allChannelFixedIMod[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()] = adjustTxPowerDmiStruct.ArrayFixedModDac[GlobalParameters.CurrentChannel - 1].ToString().Trim();
           }

           return true;
       }
       protected bool IsMonotonically()
       {

           try
           {
               #region JustArrayIsMonotonic

               {

                   if (Algorithm.MonotonicIncreasingfun(txPoweruw, txPoweruw.Length) || Algorithm.MonotonicIncreasingfun(txPowerADC, txPowerADC.Length))
                   {
                       Log.SaveLogToTxt("IsMonotonic OK!");
                       return true;
                   }
                   else
                   {
                       Log.SaveLogToTxt("Is not Monotonic FAIL!");                       
                       return false;
                   }
               }

               #endregion 
             
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
                   realtempratureArrayList.Add(GlobalParameters.CurrentTemp);
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
       protected void ReadTxPowerFromDCA(Scope tempScope)
       {
           try
           {
               txPowerADC = new double[adjustTxPowerDmiStruct.ArrayIbias.Count];
               txPoweruw = new double[adjustTxPowerDmiStruct.ArrayIbias.Count];

               Log.SaveLogToTxt("Step3...Start Adjust TxPower Dmi");
               tempScope.DisplayThreeEyes(1);
            
               for (byte i = 0; i < adjustTxPowerDmiStruct.ArrayIbias.Count; i++)
               {
                   dut.WriteBiasDac((adjustTxPowerDmiStruct.ArrayIbias[i]));
                   SetSleep(adjustTxPowerDmiStruct.SleepTime);
                   if (i == 0)
                   {
                       tempScope.AutoScale(1);
                       SetSleep(adjustTxPowerDmiStruct.SleepTime);
                   }
                   UInt16 Temp;
                   dut.ReadTxpADC(out Temp);
                   txPowerADC[i] = Convert.ToDouble(Temp);
                   for (byte j = 0; j < 4; j++)
                   {
                       tempScope.SetScaleOffset(TxLOPTarget, 1);
                       tempScope.DisplayPowerWatt();
                       txPoweruw[i] = tempScope.GetAveragePowerWatt();
                       if (txPoweruw[i] >= 10000000)
                       {
                           tempScope.AutoScale(1);
                           SetSleep(adjustTxPowerDmiStruct.SleepTime);
                           txPoweruw[i] = tempScope.GetAveragePowerWatt();
                           if (txPoweruw[i] >= 10000000)
                           {
                               SetSleep(adjustTxPowerDmiStruct.SleepTime);
                               continue;
                           }
                       }
                       else
                       {
                           break;
                       }
                   }
                   if (txPoweruw[i] >= 10000000)
                   {
                       MessageBox.Show("DCA ReadTxPowerError");
                   }
                   Log.SaveLogToTxt("CurrentChannel:" + GlobalParameters.CurrentChannel.ToString());
                   Log.SaveLogToTxt("txPowerADC:" + txPowerADC[i].ToString() + " " + "txPoweruw:" + txPoweruw[i].ToString());
               }
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

               if (!adjustTxPowerDmitValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
               {
                   Log.SaveLogToTxt("Step6...add current channel records");
                   Log.SaveLogToTxt("GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                   AdjustTxPowerDmitValueRecordsStruct tempstruct = new AdjustTxPowerDmitValueRecordsStruct();
                   tempstruct.DataTableTxLop = new DataTable();
                   tempstruct.DataTableTxPowerAdc = new DataTable();
                   adjustTxPowerDmitValueRecordsStruct.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempstruct);
                   #region  add column


                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Add("0", typeof(double));
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Add("1", typeof(double));
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Add("2", typeof(double));

                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Add("0", typeof(double));
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Add("1", typeof(double));
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Add("2", typeof(double));
                   #endregion

                   #region  add row
                   DataRow rowTxPowerAdc = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.NewRow();
                   DataRow rowPower = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.NewRow();

                   for (byte i = 0; i < txPowerADC.Length; i++)
                   {
                       rowTxPowerAdc[i.ToString()] = txPowerADC[i];
                       rowPower[i.ToString()] = txPoweruw[i];

                   }
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Add(rowTxPowerAdc);
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows.Add(rowPower);


                   #endregion

               }
               else
               {
                   #region  add row
                   DataRow rowTxPowerAdc = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.NewRow();
                   DataRow rowPower = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.NewRow();

                   for (byte i = 0; i < txPowerADC.Length; i++)
                   {
                       rowTxPowerAdc[i.ToString()] = txPowerADC[i];
                       rowPower[i.ToString()] = txPoweruw[i];

                   }
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Add(rowTxPowerAdc);
                   adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows.Add(rowPower);

                   #endregion
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
       protected bool CurveTxPowerDMIandWriteCoefs(Powersupply tempps, Scope tempScope)
      {
           try
           {
               #region  CurveCoef
               if (adjustTxPowerDmitValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
               {
                   #region  ISNewAlgorithm

                   if (adjustTxPowerDmiStruct.ISNewAlgorithm == true)
                   {

                       #region TxPowerADCCoef

                       {
                           if (tempratureADCArrayList.Count>= 1)
                           {
                               double[] txPowerAdc = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Count];
                               double[] tePowerUw = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count];
                               Log.SaveLogToTxt("Step7...Start Fitting Curve");

                               #region isTempCout等于1

                               if (tempratureADCArrayList.Count == 1)
                               {
                                   dut.SetTxpFitsCoefa("0");
                                   dut.SetTxpFitsCoefb("0");
                                   dut.SetTxpFitsCoefc("0");
                                   TempReference = GlobalParameters.CurrentTemp * 256;
                                   dut.SetReferenceTemp(Convert.ToString(TempReference));
                                   dut.SetTxpProportionLessCoef("0");
                                   dut.SetTxpProportionGreatCoef("0");                                  
                                   Log.SaveLogToTxt("isTempRelative:true");
                                   for (byte i = 0; i < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count; i++)
                                   {
                                       txPowerAdc[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[0][i].ToString());
                                       tePowerUw[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());
                                       tePowerUw[i] = tePowerUw[i] * 10;
                                       Log.SaveLogToTxt("txPowerAdc:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[0][i].ToString() + " tePowerUw:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());

                                   }

                                   {
                                       double[] tempCoefArray = Algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 2);
                                       txDmiSlopeCoefA = (float)tempCoefArray[2];
                                       txDmiSlopeCoefB = (float)tempCoefArray[1];
                                       txDmiSlopeCoefC = (float)tempCoefArray[0];
                                       txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray);
                                       txDmiSlopeCoefArray.Reverse();
                                       for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                       {
                                           Log.SaveLogToTxt("TxpFitsCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));

                                       }
                                       Log.SaveLogToTxt("Step8...WriteCoef");

                                       #region W&R TxSlopcoefc
                                       isWriteCoefCOk = dut.SetTxpFitsCoefc(txDmiSlopeCoefC.ToString());

                                       if (isWriteCoefCOk)
                                       {                                         
                                           Log.SaveLogToTxt("SetTxpFitsCoefc:" + isWriteCoefCOk.ToString());

                                       }
                                       else
                                       {                                           
                                           Log.SaveLogToTxt("SetTxpFitsCoefc:" + isWriteCoefCOk.ToString());
                                       }
                                       #endregion
                                       #region W&R TxSlopcoefb
                                       isWriteCoefBOk = dut.SetTxpFitsCoefb(txDmiSlopeCoefB.ToString());

                                       if (isWriteCoefBOk)
                                       {                                           
                                           Log.SaveLogToTxt("SetTxpFitsCoefb:" + isWriteCoefBOk.ToString());

                                       }
                                       else
                                       {
                                           
                                           Log.SaveLogToTxt("SetTxpFitsCoefb:" + isWriteCoefBOk.ToString());
                                       }
                                       #endregion
                                       #region W&R TxSlopcoefa
                                       isWriteCoefAOk = dut.SetTxpFitsCoefa(txDmiSlopeCoefA.ToString());

                                       if (isWriteCoefAOk)
                                       {                                           
                                           Log.SaveLogToTxt("SetTxpFitsCoefa:" + isWriteCoefAOk.ToString());

                                       }
                                       else
                                       {
                                          
                                           Log.SaveLogToTxt("SetTxpFitsCoefa:" + isWriteCoefAOk.ToString());
                                       }
                                       #endregion
                                       if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                                       {                                          
                                           Log.SaveLogToTxt("isCalTxDmiOk:" + true.ToString());

                                       }
                                       else
                                       { 
                                           Log.SaveLogToTxt("isCalTxDmiOk:" + false.ToString());
                                           return false;
                                       }
                                       tempps.OutPutSwitch(false, 1);
                                       tempps.OutPutSwitch(true, 1);                                       
                                       dut.FullFunctionEnable();

                                   }
                               }
                               #endregion
                               #region TempCount大于等于2

                               else if (tempratureADCArrayList.Count >= 2)
                               {
                                   if (GlobalParameters.CurrentTemp == adjustTxPowerDmiStruct.HighestCalTemp)
                                   {
                                       double highA = CalTempFitA(GlobalParameters.CurrentTemp, tempScope, dut);
                                       isWriteA = dut.SetTxpProportionGreatCoef(highA.ToString());
                                       if (isWriteA)
                                       {                                          
                                           Log.SaveLogToTxt("highA:" + highA.ToString() + "isWriteTxTempCoefA:" + isWriteA.ToString());
                                       }
                                       else
                                       {
                                           
                                           Log.SaveLogToTxt("highA:" + highA.ToString() + "isWriteTxTempCoefA:" + isWriteA.ToString());
                                       }
                                       //tempps.OutPutSwitch(false, 1);
                                       //tempps.OutPutSwitch(true, 1);
                                       //dut.FullFunctionEnable();

                                   }
                                   else if (GlobalParameters.CurrentTemp == adjustTxPowerDmiStruct.LowestCalTemp)
                                   {
                                       double lowA = CalTempFitA(GlobalParameters.CurrentTemp, tempScope, dut);
                                       isWriteA = dut.SetTxpProportionLessCoef(lowA.ToString());
                                       if (isWriteA)
                                       {
                                          
                                           Log.SaveLogToTxt("lowA:" + lowA.ToString() + "isWriteTxTempCoefA:" + isWriteA.ToString());
                                       }
                                       else
                                       {
                                           
                                           Log.SaveLogToTxt("lowA:" + lowA.ToString() + "isWriteTxTempCoefA:" + isWriteA.ToString());
                                       }
                                       //tempps.OutPutSwitch(false, 1);
                                       //tempps.OutPutSwitch(true, 1);
                                       //dut.FullFunctionEnable();

                                   }
                                   tempps.OutPutSwitch(false);
                                   tempps.OutPutSwitch(true);
                                   dut.FullFunctionEnable();

                               }
                               #endregion


                           }
                       }
                       #endregion
                   }
                   #endregion
                   else
                   {

                       #region TxPowerADCCoef

                       {
                           if (adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows.Count >= 2 && adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Count >= 2)
                           {
                               double[] txPowerAdc = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Count];
                               double[] tePowerUw = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count];
                               Log.SaveLogToTxt("Step7...Start Fitting Curve");

                               #region isTracingErrorFALSE

                               if (adjustTxPowerDmiStruct.isTracingErr == false)
                               {
                                   Log.SaveLogToTxt("isTempRelative:true");
                                   for (byte i = 0; i < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count; i++)
                                   {
                                       txPowerAdc[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[0][i].ToString());
                                       tePowerUw[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());
                                       Log.SaveLogToTxt("txPowerAdc:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[0][i].ToString() + " tePowerUw:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());

                                   }

                                   {
                                       double[] tempCoefArray = Algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 2);
                                       txDmiSlopeCoefA = (float)tempCoefArray[2];
                                       txDmiSlopeCoefB = (float)tempCoefArray[1];
                                       txDmiSlopeCoefC = (float)tempCoefArray[0];
                                       txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray);
                                       txDmiSlopeCoefArray.Reverse();
                                       for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                       {
                                           Log.SaveLogToTxt("txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));

                                       }
                                       Log.SaveLogToTxt("Step8...WriteCoef");

                                       #region W&R TxSlopcoefc
                                       isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());

                                       if (isWriteCoefCOk)
                                       {                                          
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());

                                       }
                                       else
                                       {                                           
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                       }
                                       #endregion
                                       #region W&R TxSlopcoefb
                                       isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());

                                       if (isWriteCoefBOk)
                                       {                                      
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());

                                       }
                                       else
                                       {                                          
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                       }
                                       #endregion
                                       #region W&R TxSlopcoefa
                                       isWriteCoefAOk = dut.SetTxSlopcoefa(txDmiSlopeCoefA.ToString());

                                       if (isWriteCoefAOk)
                                       {                                         
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());

                                       }
                                       else
                                       {                                         
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());
                                       }
                                       #endregion
                                       tempps.OutPutSwitch(false);
                                       tempps.OutPutSwitch(true);
                                       dut.FullFunctionEnable();
                                       if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                                       {                                          
                                           Log.SaveLogToTxt("isCalTxDmiOk:" + isCalTxDmiOk.ToString());

                                       }
                                       else
                                       {                                          
                                           Log.SaveLogToTxt("isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                           return false;
                                       }
                                   }
                               }
                               #endregion
                               #region isTracingErrTRUE
                               else if (adjustTxPowerDmiStruct.isTracingErr)
                               {
                                   Log.SaveLogToTxt("isTempRelative:true");

                                   tempCoefBArray.Clear();
                                   tempCoefCArray.Clear();
                                   for (byte i = 0; i < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Count; i++)
                                   {
                                       for (byte j = 0; j < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Count; j++)
                                       {
                                           txPowerAdc[j] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[i][j].ToString());
                                           tePowerUw[j] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[i][j].ToString());
                                           tePowerUw[j] = tePowerUw[j] * 10;
                                           Log.SaveLogToTxt("isTempRelative:true");
                                           Log.SaveLogToTxt("txPowerAdc:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[i][j].ToString() + " tePowerUw:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[i][j].ToString());
                                       }

                                       double[] tempCoefArray = Algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 1);
                                       tempCoefBArray.Add(tempCoefArray[1]);
                                       tempCoefCArray.Add(tempCoefArray[0]);
                                   }

                                   {
                                       double[] tempTempArray = new double[tempratureADCArrayList.Count];
                                       for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                       {
                                           if (GlobalParameters.UsingCelsiusTemp)
                                           {
                                               tempTempArray[i] = Convert.ToDouble(realtempratureArrayList[i].ToString()) * 256;
                                           }
                                           else if (GlobalParameters.UsingCelsiusTemp == false)
                                           {
                                               tempTempArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                           }  
                                           
                                       }

                                       double[] tempCoefArray1 = Algorithm.MultiLine(tempTempArray, (double[])tempCoefBArray.ToArray(typeof(double)), tempratureADCArray.Count, 2);
                                       txDmiSlopeCoefA = (float)tempCoefArray1[2];
                                       txDmiSlopeCoefB = (float)tempCoefArray1[1];
                                       txDmiSlopeCoefC = (float)tempCoefArray1[0];

                                       txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray1);
                                       txDmiSlopeCoefArray.Reverse();
                                       for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                       {
                                           Log.SaveLogToTxt("txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));

                                       }
                                       double[] tempCoefArray2 = Algorithm.MultiLine(tempTempArray, (double[])tempCoefCArray.ToArray(typeof(double)), tempratureADCArray.Count, 2);
                                       txDmiOffsetCoefA = (float)tempCoefArray2[2];
                                       txDmiOffsetCoefB = (float)tempCoefArray2[1];
                                       txDmiOffsetCoefC = (float)tempCoefArray2[0];
                                      
                                       txDmiOffsetCoefArray = ArrayList.Adapter(tempCoefArray2);
                                       txDmiOffsetCoefArray.Reverse();
                                       for (byte i = 0; i < txDmiOffsetCoefArray.Count; i++)
                                       {
                                           Log.SaveLogToTxt("txDmiOffsetCoefArray[" + i.ToString() + "]=" + txDmiOffsetCoefArray[i].ToString() + " " + Algorithm.ByteArraytoString(2, ",", Algorithm.FloatToIEE754(txDmiOffsetCoefArray[i])));

                                       }
                                       Log.SaveLogToTxt("Step7...WriteCoef");

                                       #region W&R TxSlopcoefc
                                       isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());

                                       if (isWriteCoefCOk)
                                       {                                           
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());

                                       }
                                       else
                                       {                                           
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                       }
                                       #endregion
                                       #region W&R TxSlopcoefb
                                       isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());

                                       if (isWriteCoefBOk)
                                       {                                           
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());

                                       }
                                       else
                                       {                                         
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                       }
                                       #endregion
                                       #region W&R TxSlopcoefa
                                       isWriteCoefAOk = dut.SetTxSlopcoefa(txDmiSlopeCoefA.ToString());

                                       if (isWriteCoefAOk)
                                       {
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());

                                       }
                                       else
                                       {
                                         
                                           Log.SaveLogToTxt("WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());

                                       }
                                       #endregion
                                       #region W&R TxOffsetcoefa
                                       isWriteCoefOffsetAOk = dut.SetTxOffsetcoefa(txDmiOffsetCoefA.ToString());

                                       if (isWriteCoefOffsetAOk)
                                       {                                          
                                           Log.SaveLogToTxt("WritetxDmiOffsetCoefA:" + isWriteCoefOffsetAOk.ToString());

                                       }
                                       else
                                       {                                           
                                           Log.SaveLogToTxt("WritetxDmiOffsetCoefA:" + isWriteCoefOffsetAOk.ToString());
                                       }
                                       #endregion
                                       #region W&R TxOffsetcoefb
                                       isWriteCoefOffsetBOk = dut.SetTxOffsetcoefb(txDmiOffsetCoefB.ToString());

                                       if (isWriteCoefOffsetBOk)
                                       {                                          
                                           Log.SaveLogToTxt("WritetxDmiOffsetCoefB:" + isWriteCoefOffsetBOk.ToString());
                                       }
                                       else
                                       {                                           
                                           Log.SaveLogToTxt("WritetxDmiOffsetCoefB:" + isWriteCoefOffsetBOk.ToString());

                                       }
                                       #endregion
                                       #region W&R TxOffsetcoefc
                                       isWriteCoefOffsetCOk = dut.SetTxOffsetcoefc(txDmiOffsetCoefC.ToString());

                                       if (isWriteCoefOffsetCOk)
                                       {
                                           Log.SaveLogToTxt("WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());

                                       }
                                       else
                                       {
                                          
                                           Log.SaveLogToTxt("WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());
                                       }
                                       #endregion
                                       tempps.OutPutSwitch(false);
                                       tempps.OutPutSwitch(true);
                                       dut.FullFunctionEnable();
                                       if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk & isWriteCoefOffsetCOk & isWriteCoefOffsetBOk & isWriteCoefOffsetAOk)
                                       {
                                         
                                           Log.SaveLogToTxt("isCalTxDmiOk:" + isCalTxDmiOk.ToString());

                                       }
                                       else
                                       {                                           
                                           Log.SaveLogToTxt("isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                           return false;
                                       }

                                   }

                               }
                               #endregion
                           }
                       }
                       #endregion
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
       protected bool LoadPNSpec()
       {

           try
           {
               if (Algorithm.FindDataInDataTable(specParameters, SpecTableStructArray, Convert.ToString(GlobalParameters.CurrentChannel)) == null)
               {
                   return false;
               }

              TxLOPTarget = SpecTableStructArray[(byte)AdjustEyeSpecs.AP].TypicalValue;
               
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

       public override List<InnoExCeption> GetException()
       {
           return base.GetException();
       }
#endregion
    }
}
