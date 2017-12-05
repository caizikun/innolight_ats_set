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
        private byte RXAdcThreshold = 0;
      //equipments
       private Attennuator tempAtten;
       private Powersupply tempps;

       private bool IsRxAdcChangeWithVcc = false;
       private DataTable dtSourceData = new DataTable();
       private DataTable dtCalculateData = new DataTable();
       private UInt16 tempratureADC;

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
            inPutParametersNameArray.Add("EXTRAOFFSET");
            inPutParametersNameArray.Add("READRXADCCOUNT");
            inPutParametersNameArray.Add("SLEEPTIME");
            inPutParametersNameArray.Add("MINRXPOWER");
            inPutParametersNameArray.Add("TEMPCORRELVCCARRAYLIST(V)");
            inPutParametersNameArray.Add("TEMPVCCCORRELCHANNELNAMES");


            dtSourceData.Columns.Add("Temp");
            dtSourceData.Columns.Add("Vcc");
            dtSourceData.Columns.Add("VccADC");

            dtSourceData.Columns.Add("CH");
            dtSourceData.Columns.Add("RxAdcThreshold");
            dtSourceData.Columns.Add("TempADC");

            dtCalculateData.Columns.Add("Temp");
            dtCalculateData.Columns.Add("CH");
            dtCalculateData.Columns.Add("Slope");
            dtCalculateData.Columns.Add("Offset");
            dtCalculateData.Columns.Add("TempADC");
            
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

                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(2, GlobalParameters.CurrentChannel);
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
            IsRxAdcChangeWithVcc = false;

            if (AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }

            CheckAdcChangeByVcc();
            if (tempAtten != null && tempps != null)
            {


                {
                    if (IsRxAdcChangeWithVcc)
                    {
                        // ReadVccADCandRawRXADC(tempAtten, tempps);

                        ReadVccADCandRawRXADC();

                        // CollectCurvingParameters();
                        if (!CurvingSlopandOffsetandWriteCoefs())
                        {
                            OutPutandFlushLog();
                            return false;
                        }
                    }
                    else
                    {
                        if (!SetRXOffset(tempAtten)) return false;

                    }
                }
                if (!channelArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    channelArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), GlobalParameters.CurrentChannel.ToString().Trim().ToUpper());
                    rxPoweruwArray = new double[calRxDmiStruct.ArrayListRxPower.Count];
                    rxPowerAdcArray = new double[calRxDmiStruct.ArrayListRxPower.Count];

                    //add to improve accuracy of RxDMI
                    tempAtten.AttnValue(calRxDmiStruct.minRxPowerInut.ToString(), 1);
                    ushort rxoffsetadc;                 
                    dut.ReadRxpADC(out rxoffsetadc);    


                    for (byte i = 0; i < calRxDmiStruct.ArrayListRxPower.Count; i++)
                    {
                        rxPoweruwArray[i] = algorithm.ChangeDbmtoUw(Convert.ToDouble(calRxDmiStruct.ArrayListRxPower[i])) * 10;
                        tempAtten.AttnValue(calRxDmiStruct.ArrayListRxPower[i].ToString());
                        UInt16 Temp;
                        dut.ReadRxpADC(out Temp);

                        rxPowerAdcArray[i] = Convert.ToDouble(Temp + calRxDmiStruct.ExtraOffset);
                        rxPowerAdcArray[i] = Convert.ToDouble(Temp);
                        rxPowerAdcArray[i] = Convert.ToDouble(Temp - rxoffsetadc); //minus offset, need correspond with FW 
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
        private void CheckAdcChangeByVcc()
        {
            try
            {
                if (calRxDmiStruct.RelatedChannels != null)
                {
                    if (calRxDmiStruct.RelatedChannels.Contains(Convert.ToString(GlobalParameters.CurrentChannel)))
                    {
                        IsRxAdcChangeWithVcc = true;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
           
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
                        //EXTRAOFFSET

                        if (algorithm.FindFileName(InformationList, "EXTRAOFFSET", out index))
                        {
                            calRxDmiStruct.ExtraOffset = Convert.ToByte(InformationList[index].DefaultValue);
                            
                              
                            
                        }
                        if (algorithm.FindFileName(InformationList, "SLEEPTIME", out index))
                        {
                            calRxDmiStruct.SleepTime = Convert.ToUInt16(InformationList[index].DefaultValue);
                        }
                        if (algorithm.FindFileName(InformationList, "MINRXPOWER", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (temp > 0)
                            {
                                temp = temp*(-1);
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
                   if (IsRxAdcChangeWithVcc)
                   {

                       DataRow[] drArray = dtSourceData.Select("CH='" + GlobalParameters.CurrentChannel + "' and Vcc='3.3'");

                       Int32 TempRxthrod = Convert.ToInt32(drArray[0]["RxAdcThreshold"]);
                       for (int i = 0; i < rxPowerAdcArray.Length; i++)
                       {
                           rxPowerAdcArray[i] -= TempRxthrod;
                       }
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
               tempAtten.AttnValue(calRxDmiStruct.minRxPowerInut.ToString(), 1);

               RxRawADC = new double[calRxDmiStruct.ArrayListVcc.Count];
               vccADC = new double[calRxDmiStruct.ArrayListVcc.Count];

               for (byte i = 0; i < calRxDmiStruct.ArrayListVcc.Count; i++)
               {
                   tempps.ConfigVoltageCurrent(Convert.ToString(calRxDmiStruct.ArrayListVcc[i]));
                   ushort tempRxadc1 = 0;
                   ushort tempvccadc1 = 0;
                   dut.ReadRxpADC(out tempRxadc1);
                   dut.ReadVccADC(out tempvccadc1, 1);

                   RxRawADC[i] = tempRxadc1;

                  

                   RxRawADC[i] = tempvccadc1 + calRxDmiStruct.ExtraOffset;

                   vccADC[i] = tempvccadc1;
                   logoStr += logger.AdapterLogString(1, tempRxadc1.ToString());
                   logoStr += logger.AdapterLogString(1, tempvccadc1.ToString());
               }
               
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
          // bool isWriteSlopCoefAOk = false;
           bool isWriteCoefOffsetCOk = false;
           bool isWriteCoefOffsetBOk = false;
         //  bool isWriteCoefOffsetAOk = false;
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

                              // RxDmiOffsetCoefArray.Clear();

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
       protected bool SetRXOffset(Attennuator tempAtten)
       {
           try
           {
               UInt16 tempRxADC=0;
               byte index = 0;
               if (calRxDmiStruct.minRxPowerInut <= -40)
               {
                   tempAtten.OutPutSwitch(false, 1);
               }
               else
               {
                   tempAtten.AttnValue(Convert.ToString(calRxDmiStruct.minRxPowerInut));
               }

               Thread.Sleep(1000);
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

               double tempValue = algorithm.SelectMaxValue(rxNoPowerADCArray, out index);
               logoStr += logger.AdapterLogString(0, "Max NoPowerADC=" + tempValue);
               logoStr += logger.AdapterLogString(0, "ExtraOffset=" + calRxDmiStruct.ExtraOffset);
               //   CollectAllChannelMaxRXADCOffset();
               //  tempValue= algorithm.SelectMaxValue(AllChannelMaxRxADCOffset[Convert.ToString(GlobalParameters.CurrentChannel).ToUpper().Trim()], out index);
               tempValue = tempValue + calRxDmiStruct.ExtraOffset;

               if (tempValue > 255)//需要重新修改 ，暂时先用，需要修改DUT底层的处理
               {
                   logoStr += logger.AdapterLogString(3, "MaxrxADC" + tempRxADC);
                   dut.SetRXPadcThreshold(tempRxADC);
                   return false;
                   // MessageBox.Show("小光时RXADC采样异常.....");
               }
               else
               {
                   tempRxADC = Convert.ToByte(tempValue);
                   dut.SetRXPadcThreshold(tempRxADC);
                   logoStr += logger.AdapterLogString(0, "RxThresholdADC=" + tempRxADC);
                   return true;
               }
           }
           catch (System.Exception ex)
           {
               //throw ex;
               return false;
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
                 //  AllChannelMaxRxADCOffset[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(RXAdcThreshold);
               }
               else
               {
                   AllChannelMaxRxADCOffset[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(RXAdcThreshold);                   
               }
               #endregion
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }
#endregion
#region  Leo Add

       protected void ReadVccADCandRawRXADC()//Leo 8-5
       {
           
           try
           {
               DataRow dr;

               dut.ReadTempADC(out tempratureADC, 1);

               if (calRxDmiStruct.ArrayListVcc == null)
               {
                   return;
               }

               if (calRxDmiStruct.minRxPowerInut <= -40)
               {
                   tempAtten.OutPutSwitch(false, 1);
               }
               else
               {
                   tempAtten.AttnValue(Convert.ToString(calRxDmiStruct.minRxPowerInut));
               }
             //  tempAtten.OutPutSwitch(false, 1);

               RxRawADC = new double[calRxDmiStruct.ArrayListVcc.Count];
               vccADC = new double[calRxDmiStruct.ArrayListVcc.Count];

               for (byte i = 0; i < calRxDmiStruct.ArrayListVcc.Count; i++)
               {
                   tempps.ConfigVoltageCurrent(Convert.ToString(calRxDmiStruct.ArrayListVcc[i]));
                   Thread.Sleep(200);
                 
                   ushort SampleRxadc = 0;
                   ushort SamplevccAdc = 0;
                   dut.ReadRxpADC(out SampleRxadc);
                   dut.ReadVccADC(out SamplevccAdc);

                   RxRawADC[i] = SampleRxadc;
                   vccADC[i] = SamplevccAdc;
                   dr = dtSourceData.NewRow();


                  dr["Temp"] = GlobalParameters.CurrentTemp;
                  dr["Vcc"] = calRxDmiStruct.ArrayListVcc[i];
                  dr["VccADC"] = SamplevccAdc;
                  dr["CH"] = GlobalParameters.CurrentChannel;
                  Int32 TempRxAdcThreshold=Convert.ToInt32( SampleRxadc + calRxDmiStruct.ExtraOffset);
                  dr["RxAdcThreshold"]= TempRxAdcThreshold;
                  dr["TempADC"] = tempratureADC;

                   dtSourceData.Rows.Add(dr);
                 
                   //logoStr += logger.AdapterLogString(1, "Temp=" + GlobalParameters.CurrentTemp + " CH=" + GlobalParameters.CurrentChannel + "Vcc=" + calRxDmiStruct.ArrayListVcc[i] + " VCCADC=" + SamplevccAdc + " RxAdcThreshold=" + TempRxAdcThreshold + " TempADC=" + tempratureADC);
                 
                   //if (GlobalParameters.CurrentTemp == 70 && GlobalParameters.CurrentChannel == 4)
                   //{
                   //    MessageBox.Show("Have a look!");
                   //}

               }
               logoStr += logger.AdapterLogString(1, "Temp=" + GlobalParameters.CurrentTemp + " CH=" + GlobalParameters.CurrentChannel+" SourceData:" );
                 
               double[] Coef = Caculate(dtSourceData, "Temp='" + GlobalParameters.CurrentTemp + "'", "VccADC", "RxAdcThreshold");

               dr = dtCalculateData.NewRow();
               dr["Slope"] = Coef[0];
               dr["Offset"] = Coef[1];
               dr["CH"] = GlobalParameters.CurrentChannel;
               dr["Temp"] = GlobalParameters.CurrentTemp;
               dr["TempADC"] = tempratureADC;
               dtCalculateData.Rows.Add(dr);

               tempAtten.OutPutSwitch(true, 1);
               tempps.ConfigVoltageCurrent(Convert.ToString(GlobalParameters.CurrentVcc));
           }
           catch (System.Exception ex)
           {
               throw ex;
           }
       }

       protected bool CurvingSlopandOffsetandWriteCoefs()
       {

            DataRow dr ;
            try
            {
                string S = "CH='" + GlobalParameters.CurrentChannel+"'";
                DataRow[] drArray = dtCalculateData.Select(S);//已经测试了两个温度

                if (drArray.Length < 2)
                {
                    return true;
                }
                else
                {
                    logoStr += logger.AdapterLogString(1, "Temp=" + GlobalParameters.CurrentTemp + " CH=" + GlobalParameters.CurrentChannel + " dtCalculateData:");
             
                    double[] CoefSlope = Caculate(dtCalculateData, "CH='" + GlobalParameters.CurrentChannel + "'", "TempADC", "Slope");// SlopA.B

                    rxDmiSlopeCoefB =Convert.ToSingle( CoefSlope[0]);
                    rxDmiSlopeCoefC = Convert.ToSingle(CoefSlope[1]);

                    logoStr += logger.AdapterLogString(1, "Temp=" + GlobalParameters.CurrentTemp + " CH=" + GlobalParameters.CurrentChannel + " dtCalculateData:");
                    double[] CoefOffset = Caculate(dtCalculateData, "CH='" + GlobalParameters.CurrentChannel + "'", "TempADC", "Offset");// SlopC.D

                    rxDmiOffsetCoefB = Convert.ToSingle(CoefOffset[0]);
                    rxDmiOffsetCoefC = Convert.ToSingle(CoefOffset[1]);

                    #region W&R RxSlopcoef b

                    bool   isWriteSlopCoefBOk = dut.SetRxAdCorSlopcoefb(rxDmiSlopeCoefB.ToString());

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
                    bool isWriteSlopCoefCOk = dut.SetRxAdCorSlopcoefc(rxDmiSlopeCoefC.ToString());

                    if (isWriteSlopCoefCOk)
                    {
                        logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefCOk.ToString());
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefCOk.ToString());

                    }
                    #endregion

                    #region W&R RxOffsetcoefb
                  bool  isWriteCoefOffsetBOk = dut.SetRxAdCorOffscoefb(rxDmiOffsetCoefB.ToString());
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
                    bool isWriteCoefOffsetCOk = dut.SetRxAdCorOffscoefc(rxDmiOffsetCoefC.ToString());
                    if (isWriteCoefOffsetCOk)
                    {
                        logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());

                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());
                    }
                    #endregion

                    if (isWriteSlopCoefBOk && isWriteSlopCoefCOk && isWriteCoefOffsetBOk && isWriteCoefOffsetCOk)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
               
               
            }
           catch
            {
                return false;
            }
       }

       private double[] Caculate(DataTable dt, string StrSelect, string Xitem, string Yitem)
       {
        

           DataRow[] Rows = dt.Select(StrSelect);

           string[] Strx = Rows.Select(x => x[Xitem].ToString()).ToArray();
           double[] X = Array.ConvertAll<String, double>(Strx, s => double.Parse(s));

           string[] StrY = Rows.Select(x => x[Yitem].ToString()).ToArray();
           double[] Y = Array.ConvertAll<String, double>(StrY, s => double.Parse(s));

           double[] Coef = algorithm.MultiLine(X, Y, X.Length, 1);


           for (int i=0;i<X.Length;i++)
           {
               logoStr += logger.AdapterLogString(1, Xitem + "[" + i + "]=" +X[i] +" "+ Yitem + "[" + i + "]=" + Y[i]);
           }

           Array.Reverse(Coef);

           for (int i = 0; i < Coef.Length; i++)
           {
               logoStr += logger.AdapterLogString(1, Coef + "[" + i + "]=" +Coef[i]);
           }

           return Coef;
       }

     
#endregion

    }
}
