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
  public  class CalRxDminoProcessingCGR4: TestModelBase
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
        private ArrayList inPutParametersNameArray = new ArrayList();
        private SortedList<string, string> channelArray = new SortedList<string, string>();
        private double[] vccADC;
        private double[] vccADC1;
        private double[] vccRealValue=new double[2] { 3.2,3.5 };
        private SortedList<string, AdjustRxPowerDmitValueRecordsStruct> adjustRxPowerDmitValueRecordsStruct = new SortedList<string, AdjustRxPowerDmitValueRecordsStruct>();
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private ArrayList tempratureADCArrayList = new ArrayList();
        private ArrayList tempCoefBArray = new ArrayList();
        private ArrayList tempCoefCArray = new ArrayList();
        private ArrayList RxDmiSlopeCoefArray = new ArrayList();
        private ArrayList RxDmiOffsetCoefArray = new ArrayList();
        
        private float rxDmiSlopeCoefB;
        private float rxDmiSlopeCoefC;
       
        private float rxDmiOffsetCoefB;
        private float rxDmiOffsetCoefC;
#endregion
#region Method
        public CalRxDminoProcessingCGR4(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;
            inPutParametersNameArray.Clear();
            channelArray.Clear();
            inPutParametersNameArray.Add("ARRAYLISTRXPOWER(DBM)");
            inPutParametersNameArray.Add("1STOR2STORPID");
            inPutParametersNameArray.Add("HASOFFSET");
            inPutParametersNameArray.Add("READRXADCCOUNT");
            inPutParametersNameArray.Add("SLEEPTIME");
            adjustRxPowerDmitValueRecordsStruct.Clear();
            tempratureADCArray.Clear();
            tempratureADCArrayList.Clear();
            tempCoefBArray.Clear();
            tempCoefCArray.Clear();
            RxDmiSlopeCoefArray.Clear();
            RxDmiOffsetCoefArray.Clear();
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
                if (selectedEquipList["ATTEN"] != null && selectedEquipList["POWERSUPPLY"] != null)
                {
                    isOK = true;
                }
                else
                {
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

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            if (!channelArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
            {
                channelArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), GlobalParameters.CurrentChannel.ToString().Trim().ToUpper());
            }
            else if (GlobalParameters.CurrentChannel!=2)
            {
                logger.AdapterLogString(0, "Curren Channel had exist");
                return true;
            }           
            if (AnalysisInputParameters(inputParameters) == false)
            {
                logger.FlushLogBuffer();
                return false;
            }
            if (selectedEquipList["ATTEN"] != null && selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                bool isWriteCoefCOk = false;
                bool isWriteCoefBOk = false;
                bool isWriteCoefAOk = false;
                bool isWriteSlopCoefCOk = false;
                bool isWriteSlopCoefBOk = false;
                
                bool isWriteCoefOffsetCOk = false;
                bool isWriteCoefOffsetBOk = false;
                
                Attennuator tempAtten = (Attennuator)selectedEquipList["ATTEN"];                
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                vccADC = new double[vccRealValue.Length];
                vccADC1 = new double[vccRealValue.Length];   
                if(GlobalParameters.CurrentChannel == 2)
                {
                    tempAtten.Switch(false,1);
                    for (byte i = 0; i < vccRealValue.Length;i++ )
                    {
                        tempps.ConfigVoltageCurrent(Convert.ToString(vccRealValue[i]));
                        ushort tempvccadc=0;
                        ushort tempvccadc1=0;
                        dut.ReadRx2RawADC(out tempvccadc);
                        dut.ReadVccADC(out tempvccadc1,1);
                         vccADC1[i] = tempvccadc1;
                        vccADC[i] = tempvccadc;
                        logoStr += logger.AdapterLogString(1, tempvccadc.ToString());
                        logoStr += logger.AdapterLogString(1, tempvccadc1.ToString());
                    }
                    tempAtten.Switch(true,1);
                    tempps.ConfigVoltageCurrent("3.3");
                }
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
                if (GlobalParameters.CurrentChannel == 2)
                {
                    #region  add current channel

                    if (!adjustRxPowerDmitValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                    {
                        logoStr += logger.AdapterLogString(0, "Step6...add current channel records");
                        logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                        AdjustRxPowerDmitValueRecordsStruct tempstruct = new AdjustRxPowerDmitValueRecordsStruct();
                        tempstruct.DataTableVccAdc = new DataTable();
                        tempstruct.DataTableVccRealValue = new DataTable();
                        
                        adjustRxPowerDmitValueRecordsStruct.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempstruct);
                        #region  add column
                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Columns.Add("0", typeof(double));
                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Columns.Add("0", typeof(double));


                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Columns.Add("1", typeof(double));
                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Columns.Add("1", typeof(double));


                      
                        
                        #endregion

                        #region  add row
                        DataRow rowVccAdc = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.NewRow();
                        DataRow rowVccPower = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.NewRow();

                        for (byte i = 0; i < vccRealValue.Length; i++)
                        {
                            rowVccAdc[i.ToString()] = vccADC[i];
                            rowVccPower[i.ToString()] = vccADC1[i];
                           
                        }
                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Rows.Add(rowVccAdc);
                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Rows.Add(rowVccPower);
                       

                        #endregion

                    }
                    else
                    {
                        #region  add row
                        DataRow rowVccAdc = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.NewRow();
                        DataRow rowVccPower = adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.NewRow();
                       
                        for (byte i = 0; i < vccRealValue.Length; i++)
                        {
                            rowVccAdc[i.ToString()] = vccADC[i];
                            rowVccPower[i.ToString()] = vccADC1[i];
                          

                        }
                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Rows.Add(rowVccAdc);
                        adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Rows.Add(rowVccPower);
                       
                        #endregion
                    }
                    #endregion
                    #region RxPowerADCOffset


                    {
                        if (adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Rows.Count >= 2 && adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Rows.Count >= 2)
                        {
                            double[] VCCPowerAdc = new double[adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Columns.Count];
                            double[] VCCPower = new double[adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Columns.Count];
                            logoStr += logger.AdapterLogString(0, "Step7...Start Fitting Curve");


                            #region isTempRelativeTRUE

                            {
                                logoStr += logger.AdapterLogString(0, "isTempRelative:true");

                                tempCoefBArray.Clear();
                                tempCoefCArray.Clear();
                                for (byte i = 0; i < adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Rows.Count; i++)
                                {
                                    for (byte j = 0; j < adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Columns.Count; j++)
                                    {
                                        VCCPowerAdc[j] = double.Parse(adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Rows[i][j].ToString());
                                        VCCPower[j] = double.Parse(adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Rows[i][j].ToString());

                                        logoStr += logger.AdapterLogString(0, "isTempRelative:true");
                                        logoStr += logger.AdapterLogString(1, "VCCPowerAdc:" + adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccAdc.Rows[i][j].ToString() + " VCCPower:" + adjustRxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableVccRealValue.Rows[i][j].ToString());
                                    }

                                    double[] tempCoefArray = algorithm.MultiLine(VCCPower, VCCPowerAdc, VCCPower.Length, 1);
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
                                    rxDmiSlopeCoefB = (float)tempCoefArray1[1];
                                    rxDmiSlopeCoefC = (float)tempCoefArray1[0];

                                    RxDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray1);
                                    RxDmiSlopeCoefArray.Reverse();
                                    for (byte i = 0; i < RxDmiSlopeCoefArray.Count; i++)
                                    {
                                        logoStr += logger.AdapterLogString(1, "txDmiSlopeCoefArray[" + i.ToString() + "]=" + RxDmiSlopeCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(RxDmiSlopeCoefArray[i])));

                                    }
                                    double[] tempCoefArray2 = algorithm.MultiLine(tempAdc, (double[])tempCoefCArray.ToArray(typeof(double)), tempratureADCArray.Count, 1);
                                    rxDmiOffsetCoefB = (float)tempCoefArray2[1];
                                    rxDmiOffsetCoefC = (float)tempCoefArray2[0];

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
                                        isWriteSlopCoefBOk = true;
                                        logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteSlopCoefBOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteSlopCoefBOk = false;
                                        logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteSlopCoefBOk.ToString());
                                    }
                                    #endregion
                                    #region W&R RxSlopcoefc
                                    isWriteSlopCoefCOk = dut.SetRxAdCorSlopcoefc(rxDmiSlopeCoefC.ToString());

                                    if (isWriteSlopCoefCOk)
                                    {
                                        isWriteSlopCoefCOk = true;
                                        logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefCOk.ToString());
                                    }
                                    else
                                    {
                                        isWriteSlopCoefCOk = false;
                                        logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteSlopCoefCOk.ToString());

                                    }
                                    #endregion

                                    #region W&R RxOffsetcoefb
                                    isWriteCoefOffsetBOk = dut.SetRxAdCorOffscoefb(rxDmiOffsetCoefB.ToString());
                                    if (isWriteCoefOffsetBOk)
                                    {
                                        isWriteCoefOffsetBOk = true;
                                        logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefB:" + isWriteCoefOffsetBOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefOffsetBOk = false;
                                        logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefB:" + isWriteCoefOffsetBOk.ToString());
                                    }
                                    #endregion
                                    #region W&R RxOffsetcoefc
                                    isWriteCoefOffsetCOk = dut.SetRxAdCorOffscoefc(rxDmiOffsetCoefC.ToString());
                                    if (isWriteCoefOffsetCOk)
                                    {
                                        isWriteCoefOffsetCOk = true;
                                        logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefOffsetCOk = false;
                                        logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefC:" + isWriteCoefOffsetCOk.ToString());
                                    }
                                    #endregion

                                    if (isWriteSlopCoefBOk & isWriteSlopCoefCOk & isWriteCoefOffsetCOk & isWriteCoefOffsetBOk)
                                    {
                                        isCalRxDmiOk = true;
                                        logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalRxDmiOk.ToString());

                                    }
                                    else
                                    {
                                        isCalRxDmiOk = false;
                                        logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalRxDmiOk.ToString());
                                    }
                                }


                            }
                            #endregion
                            tempps.Switch(false,1);
                            tempps.Switch(true,1);
                        }
                    }
                    #endregion
                }
               
                if (calRxDmiStruct.HasOffset==true)
                {  
                    UInt16 tempRxADC;
                    byte index=0;
                    tempAtten.Switch(false,1);
                    ArrayList rxADCArray = new ArrayList();
                    rxADCArray.Clear();
                    for (byte i = 0; i < calRxDmiStruct.ReadRxADCCount; i++)
                    {
                        if (dut.ReadRxpADC(out tempRxADC))
                        {
                            rxADCArray.Add(tempRxADC);
                            SetSleep(calRxDmiStruct.SleepTime);
                            logoStr += logger.AdapterLogString(0, Convert.ToString(GlobalParameters.CurrentChannel)+"count" + i.ToString() + Convert.ToString(tempRxADC));

                        }
                        else
                        {
                            MessageBox.Show("ReadRxADCError!");
                        }
                    }
                   double tempRxADCd=algorithm.SelectMaxValue(rxADCArray, out index);
                   dut.SetRXPadcThreshold((byte)(tempRxADCd));
                   tempAtten.Switch(true,1);                    
                }                
                rxPoweruwArray = new double[calRxDmiStruct.ArrayListRxPower.Count];
                rxPowerAdcArray = new double[calRxDmiStruct.ArrayListRxPower.Count];
                for (byte i = 0; i < calRxDmiStruct.ArrayListRxPower.Count; i++)
                {
                    rxPoweruwArray[i] = algorithm.ChangeDbmtoUw(Convert.ToDouble(calRxDmiStruct.ArrayListRxPower[i]))*10;
                    tempAtten.AttnValue(calRxDmiStruct.ArrayListRxPower[i].ToString(),0);                    
                    UInt16 Temp;
                    dut.ReadRxpADC(out Temp);
                    rxPowerAdcArray[i] = Convert.ToDouble(Temp);
                }
                
                
                logoStr += logger.AdapterLogString(0, "Step3...Start Fitting Curve");
                if (calRxDmiStruct.is1Stor2StorPid==1)
                {
                    double[] coefArray = algorithm.MultiLine(rxPowerAdcArray, rxPoweruwArray, calRxDmiStruct.ArrayListRxPower.Count, 1);
                    rxDmiCoefC = (float)coefArray[0];
                    rxDmiCoefB = (float)coefArray[1];
                    rxDmiCoefA = 0;
                    double[] tempCoefArray = new double[2]{ rxDmiCoefC, rxDmiCoefB };
                    
                    rxDmiCoefArray = ArrayList.Adapter(tempCoefArray);
                    rxDmiCoefArray.Reverse();
                    for (byte i = 0; i < rxDmiCoefArray.Count; i++)
                    {
                        logoStr += logger.AdapterLogString(1, "rxDmiCoefArray[" + i.ToString() + "]=" + rxDmiCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(rxDmiCoefArray[i])));
                       
                    }
                    logoStr += logger.AdapterLogString(0, "Step4...WriteCoef");
                    #region W&RRxpcoefc
                   isWriteCoefCOk=dut.SetRxpcoefc(rxDmiCoefC.ToString());
                 
                   if (isWriteCoefCOk)
                    {
                        isWriteCoefCOk = true;
                        logoStr += logger.AdapterLogString(1,"WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                      
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                       
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                    }
                    #endregion
                    #region W&R Rxpcoefb
                   isWriteCoefBOk=dut.SetRxpcoefb(rxDmiCoefB.ToString());

                   if (isWriteCoefBOk)
                    {
                        isWriteCoefBOk = true;
                        logoStr += logger.AdapterLogString(1, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    #endregion
                   #region W&R Rxpcoefa
                   isWriteCoefAOk = dut.SetRxpcoefa(rxDmiCoefA.ToString());

                   if (isWriteCoefAOk)
                   {
                       isWriteCoefAOk = true;
                       logoStr += logger.AdapterLogString(1, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                   }
                   else
                   {
                       isWriteCoefAOk = false;
                       logoStr += logger.AdapterLogString(3, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                   }
                   #endregion
                   if (isWriteCoefCOk & isWriteCoefBOk & isWriteCoefAOk)
                   {
                       isCalRxDmiOk = true;
                       logoStr += logger.AdapterLogString(1, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }
                   else
                   {
                       isCalRxDmiOk = false;
                       logoStr += logger.AdapterLogString(3, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }

                }
                else if (calRxDmiStruct.is1Stor2StorPid ==2)
                {
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
                    isWriteCoefCOk=dut.SetRxpcoefc(rxDmiCoefC.ToString());
                   
                    if (isWriteCoefCOk)
                    {
                        isWriteCoefCOk = true; 
                        logoStr += logger.AdapterLogString(1,  "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());   
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                    }
                    #endregion
                    #region W&R Rxpcoefb
                   isWriteCoefBOk= dut.SetRxpcoefb(rxDmiCoefB.ToString());
                    //dut.ReadRxpcoefb(out tempString);
                   if (isWriteCoefBOk)
                    {
                        isWriteCoefBOk = true;
                        logoStr += logger.AdapterLogString(1, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    #endregion
                    #region W&R Rxpcoefa
                   isWriteCoefAOk=dut.SetRxpcoefa(rxDmiCoefA.ToString());
                  
                   if (isWriteCoefAOk)
                    {
                        isWriteCoefAOk = true;
                        logoStr += logger.AdapterLogString(1, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                    }
                    else
                    {
                        isWriteCoefAOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                    }
                    #endregion
                   if (isWriteCoefCOk & isWriteCoefBOk & isWriteCoefAOk)
                   {
                       isCalRxDmiOk = true;
                       logoStr += logger.AdapterLogString(1, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }
                   else
                   {
                       isCalRxDmiOk = false;
                       logoStr += logger.AdapterLogString(3, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }

                }
        }
            else
            {
                isCalRxDmiOk = false;
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                logger.FlushLogBuffer();
                return isCalRxDmiOk;
            }
            AnalysisOutputParameters(outputParameters);
            logger.FlushLogBuffer();
            return isCalRxDmiOk;
        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            if (InformationList.Length == 0)//InformationList is null
            {
               
                return false;
            }
            else//  InformationList is not null
            {
                int index = -1;
                for (byte i = 0; i < InformationList.Length; i++)
                {
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTDMIRXCOEF", out index))
                    {
                        InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(rxDmiCoefArray, ",");
                       
                    }

                }
               
                return true;
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
                bool isParametersComplete = false;
                for (byte i = 0; i < inPutParametersNameArray.Count; i++)
                {
                    if (algorithm.FindFileName(InformationList, inPutParametersNameArray[i].ToString(), out index) == false)
                    {
                        logoStr += logger.AdapterLogString(4, inPutParametersNameArray[i].ToString() + "is not exist");
                        isParametersComplete = false;
                        return isParametersComplete;
                    }
                    else
                    {
                        isParametersComplete = true;
                        continue;
                    }
                }
                if (isParametersComplete)
                {
                    for (byte i = 0; i < InformationList.Length; i++)
                    {                       

                        if (algorithm.FindFileName(InformationList, "ARRAYLISTRXPOWER(DBM)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            calRxDmiStruct.ArrayListRxPower = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                             ;
                        }
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPID", out index))
                        {
                            calRxDmiStruct.is1Stor2StorPid = Convert.ToByte(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "HASOFFSET", out index))
                        {
                            calRxDmiStruct.HasOffset = Convert.ToBoolean(InformationList[index].DefaultValue);

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
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }
        protected void SetSleep(UInt16 sleeptime = 50)
        {
            Thread.Sleep(sleeptime);
        }
#endregion
    }
}
