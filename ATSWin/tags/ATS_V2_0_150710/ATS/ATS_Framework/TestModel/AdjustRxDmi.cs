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
  public  class AdjustRxDmi: TestModelBase
    {
#region Attribute
        private CalRxDmiStruct calRxDmiStruct = new CalRxDmiStruct();
        private float rxDmiCoefA;
        private float rxDmiCoefB;
        private float rxDmiCoefC;        
        private ArrayList rxDmiCoefArray = new ArrayList();
        private bool isCalRxDmiOk;
        private double[] rxPoweruwArray;
        private double[] rxPowerAdcArray;
        private ArrayList rxNoPowerADCArray = new ArrayList();
        private ArrayList inPutParametersNameArray = new ArrayList();
        private SortedList<string, string> channelArray = new SortedList<string, string>();
        private double[] vccADC=new double[]{};
        private double[] RxRawADC=new double[]{};
        private SortedList<string, AdjustRxPowerDmitValueRecordsStruct> adjustRxPowerDmitValueRecordsStruct = new SortedList<string, AdjustRxPowerDmitValueRecordsStruct>();
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private SortedList<string, ArrayList> AllChannelMaxRxADCOffset = new SortedList<string, ArrayList>();
        private ArrayList tempratureADCArrayList = new ArrayList();
        private ArrayList tempCoefBArray = new ArrayList();
        private ArrayList tempCoefCArray = new ArrayList();
        private ArrayList RxDmiSlopeCoefArray = new ArrayList();
        private ArrayList RxDmiOffsetCoefArray = new ArrayList();
        private float rxDmiSlopeCoefA;
        private float rxDmiSlopeCoefB;
        private float rxDmiSlopeCoefC;

        private float rxDmiOffsetCoefA;
        private float rxDmiOffsetCoefB;
        private float rxDmiOffsetCoefC;
        bool isWriteCoefCOk = false;
        bool isWriteCoefBOk = false;
        bool isWriteCoefAOk = false;
        private double tempRxADCd = 0;
      //equipments
       private Attennuator tempAtten;
       private Powersupply tempps;
#endregion
#region Method
        public AdjustRxDmi(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;
            inPutParametersNameArray.Clear();
            channelArray.Clear();
            rxNoPowerADCArray.Clear();
            AllChannelMaxRxADCOffset.Clear();
            inPutParametersNameArray.Add("RXPOWERARRAYLIST(DBM)");           
            //inPutParametersNameArray.Add("HASOFFSET");
            inPutParametersNameArray.Add("READRXADCCOUNT");
            inPutParametersNameArray.Add("SLEEPTIME");
            inPutParametersNameArray.Add("MINRXPOWER");
            inPutParametersNameArray.Add("TEMPCORRELVCCARRAYLIST(V)");
            inPutParametersNameArray.Add("TEMPVCCCORRELCHANNELNAMES");
            
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
                if (!selectedEquipList.Values[i].Switch(true)) return false;
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

                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                }
                 tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempAtten != null && tempps != null)
                {
                    isOK = true;
                }
                else
                {

                    if (tempAtten == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
                    }
                    if (tempps == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
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
            logger.FlushLogBuffer();
            logoStr = "";
            AddCurrentTemprature();      
            if (AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }
            
            if (tempAtten != null && tempps != null)
            {
                

                {
                    if (calRxDmiStruct.RelatedChannels!=null)
                   {
                       if (calRxDmiStruct.RelatedChannels.Contains(Convert.ToString(GlobalParameters.CurrentChannel)))
                       {
                           ReadVccADCandRawRXADC(tempAtten, tempps);
                           CollectCurvingParameters();
                           if (!CurvingSlopandOffsetandWriteCoefs(tempps))
                           {
                               OutPutandFlushLog();
                               return false;
                           }
                       }
                       else
                       {
                        SetRXOffset(tempAtten); 
                       }
                   } 
                   else
                   {
                       SetRXOffset(tempAtten); 
                   }  
                }
                if (!channelArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    channelArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), GlobalParameters.CurrentChannel.ToString().Trim().ToUpper());
                    rxPoweruwArray = new double[calRxDmiStruct.ArrayListRxPower.Count];
                    rxPowerAdcArray = new double[calRxDmiStruct.ArrayListRxPower.Count];
                    for (byte i = 0; i < calRxDmiStruct.ArrayListRxPower.Count; i++)
                    {
                        rxPoweruwArray[i] = algorithm.ChangeDbmtoUw(Convert.ToDouble(calRxDmiStruct.ArrayListRxPower[i])) * 10;
                        tempAtten.AttnValue(calRxDmiStruct.ArrayListRxPower[i].ToString(), 0);
                        UInt16 Temp;
                        dut.ReadRxpADC(out Temp);
                        rxPowerAdcArray[i] = Convert.ToDouble(Temp);
                    }
                    logoStr += logger.AdapterLogString(0, "Step3...Start Fitting Curve");
                    if (!CurveRxPowerDMIandWriteCoefs())
                    {
                        logoStr += logger.AdapterLogString(3, "write coefs error");
                        OutPutandFlushLog();
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                        
        }
            else
            {
                isCalRxDmiOk = false;
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                OutPutandFlushLog();
                return isCalRxDmiOk;
            }
            OutPutandFlushLog();
            return true;
        }        
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");
            
            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                logoStr += logger.AdapterLogString(4,  "InputParameters are not enough!");
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

                        if (algorithm.FindFileName(InformationList, "RXPOWERARRAYLIST(DBM)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            calRxDmiStruct.ArrayListRxPower = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            for (byte j = 0; j < calRxDmiStruct.ArrayListRxPower.Count; j++)
                            {
                                double temp = Convert.ToDouble(calRxDmiStruct.ArrayListRxPower[j]);
                                if (temp > 0)
                                {
                                    temp = -temp;
                                    calRxDmiStruct.ArrayListRxPower[j] = temp;

                                }
                            }
                            
                        }                      
                      
                        if (algorithm.FindFileName(InformationList, "READRXADCCOUNT", out index))
                        {
                            calRxDmiStruct.ReadRxADCCount = Convert.ToByte(InformationList[index].DefaultValue);
                            if (calRxDmiStruct.ReadRxADCCount <= 0)
                            {
                                calRxDmiStruct.ReadRxADCCount = 1;
                            }
                        }
                        
                        if (algorithm.FindFileName(InformationList, "SLEEPTIME", out index))
                        {
                            calRxDmiStruct.SleepTime = Convert.ToUInt16(InformationList[index].DefaultValue);
                        }
                        if (algorithm.FindFileName(InformationList, "MINRXPOWER", out index))
                        {
                            SByte temp = Convert.ToSByte(InformationList[index].DefaultValue);
                            if (temp > 0)
                            {
                                temp = (SByte)(-temp);
                            }
                            calRxDmiStruct.minRxPowerInut=temp;
                        }
                        if (algorithm.FindFileName(InformationList, "TEMPCORRELVCCARRAYLIST(V)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAR = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAR==null)
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is  null!");

                            } 
                            else
                            {
                                calRxDmiStruct.ArrayListVcc = tempAR;
                            }
                            
                           
                        }
                        if (algorithm.FindFileName(InformationList, "TEMPVCCCORRELCHANNELNAMES", out index))
                        {
                            char[] tempCharArray = new char[] { ',' }; 
                             ArrayList tempAR=algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                             if (tempAR==null)
                            {
                                logoStr += logger.AdapterLogString(0, InformationList[index].FiledName + "is  null!");
                            } 
                            else
                            {
                                calRxDmiStruct.RelatedChannels = tempAR;
                            }
                             

                        }
                       
                       
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
                procData = new TestModeEquipmentParameters[6];
                procData[0].FiledName = "RXPWRARRAY";
                if (rxPoweruwArray == null || rxPoweruwArray.Length==0)
                {
                    procData[0].DefaultValue = "";
                } 
                else
                {
                    procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(ArrayList.Adapter(rxPoweruwArray), ",");
                }
                
                procData[1].FiledName = "RXADCARRAY";
                if (rxPowerAdcArray == null || rxPowerAdcArray.Length==0)
                {
                    procData[1].DefaultValue = "";
                } 
                else
                {
                    procData[1].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(ArrayList.Adapter(rxPowerAdcArray), ",");
                }                
                procData[2].FiledName = "NOINPUTRXADARRAY";               
                procData[2].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(rxNoPowerADCArray, ",");
                procData[3].FiledName = "TEMPCORRELVCCADARRAY";
                if (vccADC == null || vccADC.Length==0)
                {
                    procData[3].DefaultValue = "";
                } 
                else
                {
                    procData[3].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(ArrayList.Adapter(vccADC), ",");
                }
                
                procData[4].FiledName = "TEMPCORRELRXRAWADARRAY";
                if (RxRawADC == null || RxRawADC.Length==0)
                {
                    procData[4].DefaultValue = "";
                } 
                else
                {
                    procData[4].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(ArrayList.Adapter(RxRawADC), ",");
                }
                
                procData[5].FiledName = "TEMPADC";
                if (tempratureADCArray == null || tempratureADCArray.Count == 0)
                {
                    procData[5].DefaultValue = "";
                }
                else
                {
                    procData[5].DefaultValue = tempratureADCArray[Convert.ToString(GlobalParameters.CurrentTemp)];
                }
                
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }



        }
        protected void SetSleep(UInt16 sleeptime = 50)
        {
            Thread.Sleep(sleeptime);
        }
       protected bool CurveRxPowerDMIandWriteCoefs()
        {
           try
           {
               {
                   for (int j = 0; j < calRxDmiStruct.ArrayListRxPower.Count; j++)
                   {
                       logoStr += logger.AdapterLogString(1, "rxPowerAdcArray " + j + " :" + rxPowerAdcArray[j].ToString() + "****rxPoweruwArray " + j + ":" + rxPoweruwArray[j]);
                   }

                   double[] coefArray = algorithm.MultiLine(rxPowerAdcArray, rxPoweruwArray, calRxDmiStruct.ArrayListRxPower.Count, 2);
                   rxDmiCoefC = (float)coefArray[0];
                   rxDmiCoefB = (float)coefArray[1];
                   rxDmiCoefA = (float)coefArray[2];
                   rxDmiCoefArray = ArrayList.Adapter(coefArray);
                   rxDmiCoefArray.Reverse();
                   for (byte i = 0; i < rxDmiCoefArray.Count; i++)
                   {
                       logoStr += logger.AdapterLogString(1, "rxDmiCoefArray[" + i.ToString() + "]=" + rxDmiCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(rxDmiCoefArray[i])));

                   }
                   logoStr += logger.AdapterLogString(0, "Step4...WriteCoef");

                   #region W&RRxpcoefc
                   isWriteCoefCOk = dut.SetRxpcoefc(rxDmiCoefC.ToString());

                   if (isWriteCoefCOk)
                   {
                       logoStr += logger.AdapterLogString(1, "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                   }
                   else
                   {
                       logoStr += logger.AdapterLogString(3, "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                   }
                   #endregion
                   #region W&R Rxpcoefb
                   isWriteCoefBOk = dut.SetRxpcoefb(rxDmiCoefB.ToString());
                   //dut.ReadRxpcoefb(out tempString);
                   if (isWriteCoefBOk)
                   {
                       logoStr += logger.AdapterLogString(1, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                   }
                   else
                   {
                       logoStr += logger.AdapterLogString(3, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                   }
                   #endregion
                   #region W&R Rxpcoefa
                   isWriteCoefAOk = dut.SetRxpcoefa(rxDmiCoefA.ToString());

                   if (isWriteCoefAOk)
                   {                       
                       logoStr += logger.AdapterLogString(1, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                   }
                   else
                   {
                       logoStr += logger.AdapterLogString(3, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                   }
                   #endregion
                   if (isWriteCoefCOk & isWriteCoefBOk & isWriteCoefAOk)
                   {
                       logoStr += logger.AdapterLogString(1, "isCalRxDmiOk:" + true.ToString());
                   }
                   else
                   {
                      
                       logoStr += logger.AdapterLogString(3, "isCalRxDmiOk:" + false.ToString());
                       return false;
                   }

               }
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
       protected void AddCurrentTemprature()
       {
           try
           {
               #region  CheckTempChange
               if (!tempratureADCArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
               {
                   logoStr += logger.AdapterLogString(0, "Step4...TempChanged Read tempratureADC");
                   logoStr += logger.AdapterLogString(1, "realtemprature=" + GlobalParameters.CurrentTemp.ToString());

                   UInt16 tempratureADC;
                   dut.ReadTempADC(out tempratureADC, 1);
                   logoStr += logger.AdapterLogString(1, "tempratureADC=" + tempratureADC.ToString());
                   tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper(), tempratureADC.ToString().Trim());
                   tempratureADCArrayList.Add(tempratureADC);
               }
               #endregion
           }
           catch (System.Exception ex)
           {
               throw ex;
           }

       }
       protected void ReadVccADCandRawRXADC(Attennuator tempAtten, Powersupply tempps)
       {
           try
           {
               if (calRxDmiStruct.ArrayListVcc==null)
               {
                   return;
               }
               tempAtten.Switch(false, 1);
               for (byte i = 0; i < calRxDmiStruct.ArrayListVcc.Count; i++)
               {
                   tempps.ConfigVoltageCurrent(Convert.ToString(calRxDmiStruct.ArrayListVcc[i]));
                   ushort tempvccadc = 0;
                   ushort tempvccadc1 = 0;
                   dut.ReadRxpADC(out tempvccadc);
                   dut.ReadVccADC(out tempvccadc1, 1);
                   RxRawADC[i] = tempvccadc;
                   vccADC[i] = tempvccadc1;
                   logoStr += logger.AdapterLogString(1, tempvccadc.ToString());
                   logoStr += logger.AdapterLogString(1, tempvccadc1.ToString());
               }
               tempAtten.Switch(true, 1);
               tempps.ConfigVoltageCurrent(Convert.ToString(GlobalParameters.CurrentVcc));
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       protected void CollectCurvingParameters()
       {
           try
           {
               #region  add current channel

               if (!adjustRxPowerDmitValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
               {
                   logoStr += logger.AdapterLogString(0, "Step6...add current channel records");
                   logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                   AdjustRxPowerDmitValueRecordsStruct tempstruct = new AdjustRxPowerDmitValueRecordsStruct();
                   tempstruct.DataTableRxRawAdc = new DataTable();
                   tempstruct.DataTableVccADC = new DataTable();

                   adjustRxPowerDmitValueRecordsStruct.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempstruct);
                   #region  add column
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Columns.Add("0", typeof(double));
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Columns.Add("0", typeof(double));


                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Columns.Add("1", typeof(double));
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Columns.Add("1", typeof(double));

                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Columns.Add("2", typeof(double));
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Columns.Add("2", typeof(double));



                   #endregion

                   #region  add row
                   DataRow rowRxRawAdc = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.NewRow();
                   DataRow rowVccAdc = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.NewRow();

                   for (byte i = 0; i < calRxDmiStruct.ArrayListVcc.Count; i++)
                   {
                       rowVccAdc[i.ToString()] = vccADC[i];
                       rowRxRawAdc[i.ToString()] = RxRawADC[i];

                   }
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Rows.Add(rowRxRawAdc);
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Rows.Add(rowVccAdc);


                   #endregion

               }
               else
               {
                   #region  add row
                   DataRow rowRxRawAdc = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.NewRow();
                   DataRow rowVccAdc = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.NewRow();

                   for (byte i = 0; i < calRxDmiStruct.ArrayListVcc.Count; i++)
                   {
                       rowVccAdc[i.ToString()] = vccADC[i];
                       rowRxRawAdc[i.ToString()] = RxRawADC[i];


                   }
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Rows.Add(rowRxRawAdc);
                   adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Rows.Add(rowVccAdc);

                   #endregion
               }
               #endregion
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       protected bool CurvingSlopandOffsetandWriteCoefs(Powersupply tempps)
       {
           bool isWriteSlopCoefCOk = false;
           bool isWriteSlopCoefBOk = false;
           bool isWriteSlopCoefAOk = false;
           bool isWriteCoefOffsetCOk = false;
           bool isWriteCoefOffsetBOk = false;
           bool isWriteCoefOffsetAOk = false;
           try
           {
               #region RxPowerADCOffset


               {
                   if (adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Rows.Count >= 1 && adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Rows.Count >= 1)
                   {
                       double[] VCCAdc = new double[adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Columns.Count];
                       double[] RXADC = new double[adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Columns.Count];
                       logoStr += logger.AdapterLogString(0, "Step7...Start Fitting Curve");

                       #region isTempRelativeTRUE

                       {
                           logoStr += logger.AdapterLogString(0, "isTempRelative:true");

                           tempCoefBArray.Clear();
                           tempCoefCArray.Clear();
                           for (byte i = 0; i < adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Rows.Count; i++)
                           {
                               for (byte j = 0; j < adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Columns.Count; j++)
                               {
                                   VCCAdc[j] = double.Parse(adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Rows[i][j].ToString());
                                   RXADC[j] = double.Parse(adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Rows[i][j].ToString());

                                   logoStr += logger.AdapterLogString(0, "isTempRelative:true");
                                   logoStr += logger.AdapterLogString(1, "VCCAdc:" + adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccADC.Rows[i][j].ToString() + " rxrawADC:" + adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableRxRawAdc.Rows[i][j].ToString());
                               }

                               double[] tempCoefArray = algorithm.MultiLine(VCCAdc, RXADC, RXADC.Length, 1);
                               tempCoefBArray.Add(tempCoefArray[1]);
                               tempCoefCArray.Add(tempCoefArray[0]);
                           }

                           {
                               double[] tempAdc = new double[tempratureADCArrayList.Count];
                               for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                               {
                                   tempAdc[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                               }
                               double[] tempCoefArray1 = algorithm.MultiLine(tempAdc, (double[])tempCoefBArray.ToArray(typeof(double)), tempratureADCArray.Count, 1);
                               rxDmiSlopeCoefC = (float)tempCoefArray1[0];
                               rxDmiSlopeCoefB = (float)tempCoefArray1[1];                               
                               //rxDmiSlopeCoefA = (float)tempCoefArray1[2];
                               RxDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray1);
                               RxDmiSlopeCoefArray.Reverse();
                               for (byte i = 0; i < RxDmiSlopeCoefArray.Count; i++)
                               {
                                   logoStr += logger.AdapterLogString(1, "txDmiSlopeCoefArray[" + i.ToString() + "]=" + RxDmiSlopeCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(RxDmiSlopeCoefArray[i])));

                               }
                               double[] tempCoefArray2 = algorithm.MultiLine(tempAdc, (double[])tempCoefCArray.ToArray(typeof(double)), tempratureADCArray.Count,1);
                               rxDmiOffsetCoefC = (float)tempCoefArray2[0];
                               rxDmiOffsetCoefB = (float)tempCoefArray2[1];
                               //rxDmiOffsetCoefA = (float)tempCoefArray2[2];
                               RxDmiOffsetCoefArray.Clear();
                               RxDmiOffsetCoefArray = ArrayList.Adapter(tempCoefArray2);
                               RxDmiOffsetCoefArray.Reverse();
                               for (byte i = 0; i < RxDmiOffsetCoefArray.Count; i++)
                               {
                                   logoStr += logger.AdapterLogString(1, "txDmiOffsetCoefArray[" + i.ToString() + "]=" + RxDmiOffsetCoefArray[i].ToString() + " " +
algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(RxDmiOffsetCoefArray[i])));

                               }
                               logoStr += logger.AdapterLogString(0, "Step8...WriteCoef");

                               #region W&R RxSlopcoefb
                               isWriteSlopCoefBOk = dut.SetRxAdCorSlopcoefb(rxDmiSlopeCoefB.ToString());

                               if (isWriteSlopCoefBOk)
                               {                                   
                                   logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteSlopCoefBOk.ToString());

                               }
                               else
                               {                                  
                                   logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteSlopCoefBOk.ToString());
                               }
                               #endregion
                               #region W&R RxSlopcoefc
                               isWriteSlopCoefCOk = dut.SetRxAdCorSlopcoefc(rxDmiSlopeCoefC.ToString());

                               if (isWriteSlopCoefCOk)
                               {                                   
                                   logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefCOk.ToString());
                               }
                               else
                               {                                  
                                   logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefCOk.ToString());

                               }
                               #endregion
                               //#region W&R RxSlopcoefa
                               //isWriteSlopCoefAOk = dut.SetRxAdCorSlopcoefc(rxDmiSlopeCoefA.ToString());

                               //if (isWriteSlopCoefAOk)
                               //{
                               //    logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefAOk.ToString());
                               //}
                               //else
                               //{
                               //    logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefAOk.ToString());

                               //}
                               //#endregion
                               #region W&R RxOffsetcoefb
                               isWriteCoefOffsetBOk = dut.SetRxAdCorOffscoefb(rxDmiOffsetCoefB.ToString());
                               if (isWriteCoefOffsetBOk)
                               {
                                 
                                   logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefB:" + isWriteCoefOffsetBOk.ToString());

                               }
                               else
                               {
                                   
                                   logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefB:" + isWriteCoefOffsetBOk.ToString());
                               }
                               #endregion
                               #region W&R RxOffsetcoefc
                               isWriteCoefOffsetCOk = dut.SetRxAdCorOffscoefc(rxDmiOffsetCoefC.ToString());
                               if (isWriteCoefOffsetCOk)
                               {                                   
                                   logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());

                               }
                               else
                               {                                   
                                   logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());
                               }
                               #endregion
                               //#region W&R RxOffsetcoefA
                               //isWriteCoefOffsetAOk = dut.SetRxAdCorOffscoefc(rxDmiOffsetCoefA.ToString());
                               //if (isWriteCoefOffsetAOk)
                               //{
                               //    logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetAOk.ToString());

                               //}
                               //else
                               //{
                               //    logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetAOk.ToString());
                               //}
                               //#endregion
                               if (isWriteSlopCoefBOk & isWriteSlopCoefCOk & isWriteCoefOffsetCOk & isWriteCoefOffsetBOk)
                               {
                                   
                                   logoStr += logger.AdapterLogString(1, "isCalRXADCOffset:" + true.ToString());

                               }
                               else
                               {

                                   logoStr += logger.AdapterLogString(3, "isCalRXADCOffset:" + false.ToString());
                               }
                           }


                       }
                       #endregion
                   }
               }
               #endregion
               return true;
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       protected void SetRXOffset(Attennuator tempAtten)
       {
           try
           {
               UInt16 tempRxADC;
               byte index = 0;
               if (calRxDmiStruct.minRxPowerInut<=-40)
               {
                   tempAtten.Switch(false, 1);
               } 
               else
               {
                   tempAtten.AttnValue(Convert.ToString(calRxDmiStruct.minRxPowerInut));
               }
               
               rxNoPowerADCArray.Clear();
               for (byte i = 0; i < calRxDmiStruct.ReadRxADCCount; i++)
               {
                   if (dut.ReadRxpADC(out tempRxADC))
                   {
                       rxNoPowerADCArray.Add(tempRxADC);
                       SetSleep(calRxDmiStruct.SleepTime);
                   }
                   else
                   {
                       rxNoPowerADCArray.Add(0);
                       MessageBox.Show("ReadRxADCError!");
                   }
               }
               for (int i = 0; i < rxNoPowerADCArray.Count; i++)
               {
                   logoStr += logger.AdapterLogString(3, "rxADCArray" + i + "=" + rxNoPowerADCArray[i]);

               }

               tempRxADCd = algorithm.SelectMaxValue(rxNoPowerADCArray, out index);             
               CollectAllChannelMaxRXADCOffset();
               tempRxADCd = algorithm.SelectMaxValue(AllChannelMaxRxADCOffset[Convert.ToString(GlobalParameters.CurrentChannel).ToUpper().Trim()], out index);
               if (tempRxADCd > 255)
               {
                   logoStr += logger.AdapterLogString(3, "MaxrxADC" + tempRxADCd);

                   MessageBox.Show("小光时RXADC采样异常.....");
               }
               dut.SetRXPadcThreshold((byte)tempRxADCd);
               if (calRxDmiStruct.minRxPowerInut <= -40)
               {
                   tempAtten.Switch(true, 1);
               } 
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
       protected void CollectAllChannelMaxRXADCOffset()
       {
           try
           {
               #region  add current channel

               if (!AllChannelMaxRxADCOffset.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
               {
                   logoStr += logger.AdapterLogString(0, "Step6...add current channel records");
                   logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                   ArrayList tempArr = new ArrayList();

                   AllChannelMaxRxADCOffset.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempArr);
                   AllChannelMaxRxADCOffset[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(tempRxADCd);
               }
               else
               {
                   AllChannelMaxRxADCOffset[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(tempRxADCd);                   
               }
               #endregion
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
#endregion
    }
}
