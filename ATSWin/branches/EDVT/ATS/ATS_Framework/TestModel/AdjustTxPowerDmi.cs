﻿using System;
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
   public class AdjustTxPowerDmi : TestModelBase
    {       
#region Attribute
       private SortedList<string, AdjustTxPowerDmitValueRecordsStruct> adjustTxPowerDmitValueRecordsStruct=new SortedList<string, AdjustTxPowerDmitValueRecordsStruct>();
        private AdjustTxPowerDmiStruct adjustTxPowerDmiStruct=new AdjustTxPowerDmiStruct();
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private ArrayList tempratureADCArrayList = new ArrayList(); 
        private double[] txPowerADC;
        private double[] iBiasADC;
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
        private ArrayList tempCoefBArray = new ArrayList();
        private ArrayList tempCoefCArray = new ArrayList();
        private ArrayList txDmiSlopeCoefArray = new ArrayList();
        private ArrayList txDmiOffsetCoefArray = new ArrayList();
       // cal coef
       
#endregion
        
#region Method
        public AdjustTxPowerDmi(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
            adjustTxPowerDmitValueRecordsStruct.Clear();
            tempratureADCArrayList.Clear();
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("AUTOTUNE");
            inPutParametersNameArray.Add("ARRAYIBIAS(MA)");
            inPutParametersNameArray.Add("FIXEDMODDAC(MA)");
            inPutParametersNameArray.Add("IBIASADCORTXPOWERADC");
            inPutParametersNameArray.Add("ISTEMPRELATIVE");
            inPutParametersNameArray.Add("1STOR2STORPID");
            inPutParametersNameArray.Add("DCTODC");
            inPutParametersNameArray.Add("FIXEDCROSSDAC");
           
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
                    if (tempKeys[i].ToUpper().Contains("SCOPE"))
                    {
                        selectedEquipList.Add("SCOPE", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                    
                }
                
               
                if (selectedEquipList["SCOPE"] != null && selectedEquipList["POWERSUPPLY"] != null)
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
            if (AnalysisInputParameters(inputParameters)==false)
            {
                logger.FlushLogBuffer();
                return false;
            }
            if (PrepareEnvironment(selectedEquipList)==false)
            {
                 logger.FlushLogBuffer();
                 return false;
            }
            if (selectedEquipList["SCOPE"] != null && selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                bool isWriteCoefCOk = false;
                bool isWriteCoefBOk = false;
                bool isWriteCoefAOk = false;
                bool isWriteCoefOffsetCOk = false;
                bool isWriteCoefOffsetBOk = false;
                bool isWriteCoefOffsetAOk = false;
               
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                // close apc 
                string apcstring = null;
                dut.APCStatus(out  apcstring);
                if (apcstring == "ON" || apcstring == "FF")
                {
                    logoStr += logger.AdapterLogString(0,  "Step2...Start close apc");
                              
                    dut.APCOFF();
                    logoStr += logger.AdapterLogString(0,  "Power off");
                               
                    tempps.Switch(false);
                    Thread.Sleep(200);
                    logoStr += logger.AdapterLogString(0, "Power on");
                    tempps.Switch(true);
                    Thread.Sleep(500);
                    bool isclosed = dut.APCStatus(out  apcstring);
                    if (apcstring == "OFF")
                    {
                        logoStr += logger.AdapterLogString(1, "APC OFF");
                                       
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(3, "APC NOT OFF");
                       
                    }
                }
                // close apc
                Thread.Sleep(1000);
                txPowerADC = new double[adjustTxPowerDmiStruct.ArrayIbias.Count];
                txPoweruw = new double[adjustTxPowerDmiStruct.ArrayIbias.Count];
                iBiasADC = new double[adjustTxPowerDmiStruct.ArrayIbias.Count];
                logoStr += logger.AdapterLogString(0,"Step3...Start Adjust TxPower Dmi");
                dut.WriteModDac(adjustTxPowerDmiStruct.FixedModDac);
                for (byte i = 0; i < adjustTxPowerDmiStruct.ArrayIbias.Count; i++)
                {                    
                    dut.WriteBiasDac((adjustTxPowerDmiStruct.ArrayIbias[i]));
                    tempScope.ClearDisplay();
                    tempScope.SetScaleOffset();
                    Thread.Sleep(200);
                    UInt16 Temp;
                    dut.ReadTxpADC(out Temp);
                    Thread.Sleep(100);
                    txPowerADC[i] = Convert.ToDouble(Temp);
                    dut.ReadBiasADC(out Temp);
                    Thread.Sleep(100);
                    iBiasADC[i] = Convert.ToDouble(Temp);
                    txPoweruw[i] = tempScope.GetAveragePowerWatt();
                    Thread.Sleep(1000);
                    logoStr += logger.AdapterLogString(1, "CurrentChannel:" + GlobalParameters.CurrentChannel.ToString());
                    logoStr += logger.AdapterLogString(1,  "BiasDac:" + adjustTxPowerDmiStruct.ArrayIbias[i].ToString() + " " + "txPowerADC:" + txPowerADC[i].ToString() + " " + "iBiasADC:" + iBiasADC[i].ToString() + " " + "txPoweruw:" + txPoweruw[i].ToString());
                }
#region JustArrayIsMonotonic
               
                {
                    if (algorithm.MonotonicIncreasingfun(txPoweruw, txPoweruw.Length) && algorithm.MonotonicIncreasingfun(iBiasADC, iBiasADC.Length) && algorithm.MonotonicIncreasingfun(txPowerADC, txPowerADC.Length))
                    {
                        logoStr += logger.AdapterLogString(1, "IsMonotonic OK!");
                        isAdjustTxPowerDmiOk = true;
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(4, "Is not Monotonic FAIL!");
                        isAdjustTxPowerDmiOk = false;
                        return false;
                    }
                }
                
 #endregion 

#region  CheckTempChange
                if (!tempratureADCArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0,  "Step4...TempChanged Read tempratureADC");
                    logoStr += logger.AdapterLogString(1,"realtemprature=" + GlobalParameters.CurrentTemp.ToString());
                   
                    UInt16 tempratureADC;
                    dut.ReadTempADC(out tempratureADC,1);
                    logoStr += logger.AdapterLogString(1, "tempratureADC=" + tempratureADC.ToString());
                    tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper(), tempratureADC.ToString().Trim());
                    tempratureADCArrayList.Add(tempratureADC);
                } 
#endregion
#region  add current channel

                if (!adjustTxPowerDmitValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step6...add current channel records");
                    logoStr += logger.AdapterLogString(1,"GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                    AdjustTxPowerDmitValueRecordsStruct tempstruct = new AdjustTxPowerDmitValueRecordsStruct();
                    tempstruct.DataTableTxLop = new DataTable();
                    tempstruct.DataTableTxPowerAdc = new DataTable();
                    tempstruct.DataTableIBiasAdc = new DataTable();
                    adjustTxPowerDmitValueRecordsStruct.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempstruct);
#region  add column
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Columns.Add("0", typeof(double));
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Columns.Add("1", typeof(double));
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Columns.Add("2", typeof(double));

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
                    DataRow rowIbasAdc = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.NewRow();
                    for (byte i = 0; i< txPowerADC.Length; i++)
                    {
                        rowTxPowerAdc[i.ToString()] = txPowerADC[i];
                        rowPower[i.ToString()] = txPoweruw[i];
                        rowIbasAdc[i.ToString()] = iBiasADC[i];
                       
                    }
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Add(rowTxPowerAdc);
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows.Add(rowPower);
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows.Add(rowIbasAdc);
                    Thread.Sleep(100);
                    
#endregion          
                   
                }
                else
                {
                    #region  add row
                    DataRow rowTxPowerAdc = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.NewRow();
                    DataRow rowPower = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.NewRow();
                    DataRow rowIbasAdc = adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.NewRow();
                    for (byte i = 0; i < txPowerADC.Length; i++)
                    {
                        rowTxPowerAdc[i.ToString()] = txPowerADC[i];
                        rowPower[i.ToString()] = txPoweruw[i];
                        rowIbasAdc[i.ToString()] = iBiasADC[i];

                    }
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Add(rowTxPowerAdc);
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows.Add(rowPower);
                    adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows.Add(rowIbasAdc);
                    Thread.Sleep(100);
                    #endregion
                }
                #endregion
#region  CurveCoef
                if (adjustTxPowerDmitValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))                   
                    {
#region IbiasADCCoef

                        if (adjustTxPowerDmiStruct.IBiasADCorTxPowerADC == 0)
                        {
                            if (adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows.Count >= 2 && adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows.Count >= 2)
                            {
                                double[] txPowerAdc = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Columns.Count];
                                double[] tePowerUw = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count];
                                logoStr += logger.AdapterLogString(0, "Step7...Start Fitting Curve");
                                
   #region isTempRelativeFALSE
                               
                                if (adjustTxPowerDmiStruct.isTempRelative == false)
                                {
                                    logoStr += logger.AdapterLogString(0, "isTempRelative :false");
                                    for (byte i = 0; i < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count; i++)
                                    {
                                        txPowerAdc[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows[0][i].ToString());
                                        tePowerUw[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());
                                        logoStr += logger.AdapterLogString(1, "txPowerAdc:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows[0][i].ToString() + " tePowerUw:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());
                                    }
                                    if (adjustTxPowerDmiStruct.is1Stor2StorPid == 1)
                                    {
                                        double[] tempCoefArray = algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 1);
                                        txDmiSlopeCoefB = (float)tempCoefArray[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray[0];
                                        
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i< txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));
                                          
                                        }
                                        logoStr += logger.AdapterLogString(0,"Step8...WriteCoef");       
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());       
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());       
                                            
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());    
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());
                                       
                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());    
                                            
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());    
                                            
                                        }
                                        #endregion

                                        if (isWriteCoefBOk & isWriteCoefCOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());  
                                           
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());  
                                        }
                                    }
                                    else if (adjustTxPowerDmiStruct.is1Stor2StorPid == 2)
                                    {
                                        double[] tempCoefArray = algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 2);
                                        txDmiSlopeCoefA = (float)tempCoefArray[2];
                                        txDmiSlopeCoefB = (float)tempCoefArray[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray[0];
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));  
                                          
                                        }
                                        logoStr += logger.AdapterLogString(0,"Step8...WriteCoef");  
                                       
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());
                                       
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());  
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());  
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());
                                      
                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());  
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());  
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefa
                                        isWriteCoefAOk = dut.SetTxSlopcoefa(txDmiSlopeCoefA.ToString());
                                       
                                        if (isWriteCoefAOk)
                                        {
                                            isWriteCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());  
                                          
                                        }
                                        else
                                        {
                                            isWriteCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());  
                                        }
                                        #endregion
                                        if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());  
                                          
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());  
                                        }
                                    }
                                }
   #endregion
    #region isTempRelativeTRUE                            
    
                                else
                                {
                                    logoStr += logger.AdapterLogString(0, "isTempRelative :true");      
                                    tempCoefBArray.Clear();
                                    tempCoefCArray.Clear();
                                    for (byte i = 0; i< adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows.Count; i++)
                                    {
                                        for (byte j = 0; j < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Columns.Count; j++)
                                        {
                                            txPowerAdc[j] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows[i][j].ToString());
                                            tePowerUw[j] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[i][j].ToString());
                                            tePowerUw[j] = tePowerUw[j] * 10;
                                            logoStr += logger.AdapterLogString(1,"txPowerAdc:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableIBiasAdc.Rows[i][j].ToString() + " tePowerUw:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[i][j].ToString()); 
                                        }
                                        
                                        double[] tempCoefArray = algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 1);
                                        tempCoefBArray.Add(tempCoefArray[1]);
                                        tempCoefCArray.Add(tempCoefArray[0]);
                                    }
                                    if (adjustTxPowerDmiStruct.is1Stor2StorPid == 1)
                                    {
                                        double[] tempAdc = new double[tempratureADCArrayList.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempAdc[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                        double[] tempCoefArray1 = algorithm.MultiLine(tempAdc, (double[])tempCoefBArray.ToArray(typeof(double)), tempratureADCArray.Count, 1);
                                        txDmiSlopeCoefB = (float)tempCoefArray1[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray1[0];
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray1);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i]))); 
                                        }
                                        double[] tempCoefArray2 = algorithm.MultiLine(tempAdc, (double[])tempCoefCArray.ToArray(typeof(double)), tempratureADCArray.Count, 1);
                                        txDmiOffsetCoefB = (float)tempCoefArray2[1];
                                        txDmiOffsetCoefC = (float)tempCoefArray2[0];
                                        txDmiOffsetCoefArray = ArrayList.Adapter(tempCoefArray2);
                                        txDmiOffsetCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiOffsetCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiOffsetCoefArray[" + i.ToString() + "]=" + txDmiOffsetCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiOffsetCoefArray[i]))); 
                                           
                                        }
                                        logoStr += logger.AdapterLogString(0,"Step8...WriteCoef"); 
                                      
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());

                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString()); 
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString()); 
                                           
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());
                                      
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString()); 
                                         
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString()); 
                                        }
                                        #endregion

                                        #region W&R TxOffsetcoefb
                                        isWriteCoefOffsetBOk = dut.SetTxOffsetcoefb(txDmiOffsetCoefB.ToString());
                                        
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
                                        #region W&R TxOffsetcoefc
                                        isWriteCoefOffsetCOk = dut.SetTxOffsetcoefc(txDmiOffsetCoefC.ToString());
                                        //dut.ReadTxOffsetcoefc(out tempString);
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
                                        if (isWriteCoefBOk & isWriteCoefCOk & isWriteCoefOffsetCOk & isWriteCoefOffsetBOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString()); 
                                           
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString()); 
                                        }
                                    }
                                    else if (adjustTxPowerDmiStruct.is1Stor2StorPid == 2)
                                    {
                                        double[] tempAdc = new double[tempratureADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempAdc[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }

                                        double[] tempCoefArray1 = algorithm.MultiLine(tempAdc, (double[])tempCoefBArray.ToArray(typeof(double)), tempratureADCArray.Count, 2);
                                        txDmiSlopeCoefA = (float)tempCoefArray1[2];
                                        txDmiSlopeCoefB = (float)tempCoefArray1[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray1[0];
                                      
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray1);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));
                                           
                                        }
                                        double[] tempCoefArray2 = algorithm.MultiLine(tempAdc, (double[])tempCoefCArray.ToArray(typeof(double)), tempratureADCArray.Count, 2);
                                        txDmiOffsetCoefA = (float)tempCoefArray2[2];
                                        txDmiOffsetCoefB = (float)tempCoefArray2[1];
                                        txDmiOffsetCoefC = (float)tempCoefArray2[0];
                                        //txDmiOffsetCoefArray.Clear();
                                        txDmiOffsetCoefArray = ArrayList.Adapter(tempCoefArray2);
                                        txDmiOffsetCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiOffsetCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiOffsetCoefArray[" + i.ToString() + "]=" + txDmiOffsetCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiOffsetCoefArray[i])));
                                            
                                        }
                                        logoStr += logger.AdapterLogString(0,"Step8..WriteCoef");
                                       
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());
                                        //dut.ReadTxSlopcoefc(out tempString);
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());  
                                            
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());      
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());
                                       
                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());      
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());      
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefa
                                        isWriteCoefAOk = dut.SetTxSlopcoefa(txDmiSlopeCoefA.ToString());
                                        if (isWriteCoefAOk)
                                        {
                                            isWriteCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());      
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString()); 
                                        }
                                        #endregion
                                        #region W&R TxOffsetcoefa
                                        isWriteCoefOffsetAOk = dut.SetTxOffsetcoefa(txDmiOffsetCoefA.ToString());      
                                        if (isWriteCoefOffsetAOk)
                                        {
                                            isWriteCoefOffsetAOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefA:" + isWriteCoefOffsetAOk.ToString());      
                                            
                                        }
                                        else
                                        {
                                            isWriteCoefOffsetAOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefA:" + isWriteCoefOffsetAOk.ToString());      
                                        }
                                        #endregion
                                        #region W&R TxOffsetcoefb
                                        isWriteCoefOffsetBOk = dut.SetTxOffsetcoefb(txDmiOffsetCoefB.ToString());     
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
                                        #region W&R TxOffsetcoefc
                                        isWriteCoefOffsetCOk = dut.SetTxOffsetcoefc(txDmiOffsetCoefC.ToString());    
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
                                        if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk & isWriteCoefOffsetCOk & isWriteCoefOffsetBOk & isWriteCoefOffsetAOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());      
                                           
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());      
                                          
                                        }

                                    }

                                }
    #endregion
                            }

                        }
#endregion
#region TxPowerADCCoef

                        else if (adjustTxPowerDmiStruct.IBiasADCorTxPowerADC == 1)
                        {
                            if (adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows.Count >= 2 && adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Count >= 2)
                            {
                                double[] txPowerAdc = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Count];
                                double[] tePowerUw = new double[adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count];
                                logoStr += logger.AdapterLogString(0, "Step7...Start Fitting Curve");       
                              
 #region isTempRelativeFALSE
                                
                                if (adjustTxPowerDmiStruct.isTempRelative == false)
                                {
                                    logoStr += logger.AdapterLogString(0, "isTempRelative:true");
                                    for (byte i = 0; i < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Columns.Count; i++)
                                    {
                                        txPowerAdc[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[0][i].ToString());
                                        tePowerUw[i] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());
                                        logoStr += logger.AdapterLogString(1, "txPowerAdc:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[0][i].ToString() + " tePowerUw:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[0][i].ToString());
                                       
                                    }
                                    if (adjustTxPowerDmiStruct.is1Stor2StorPid == 1)
                                    {
                                        double[] tempCoefArray = algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 1);
                                        txDmiSlopeCoefB = (float)tempCoefArray[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray[0];
                                       
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));
                                           
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step8...WriteCoef");
                                       
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());
                                       
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                           
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());
                                       
                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true; 
                                           
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                        }
                                        #endregion

                                        if (isWriteCoefBOk & isWriteCoefCOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                          
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                        }
                                    }
                                    else if (adjustTxPowerDmiStruct.is1Stor2StorPid == 2)
                                    {
                                        double[] tempCoefArray = algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 2);
                                        txDmiSlopeCoefA = (float)tempCoefArray[2];
                                        txDmiSlopeCoefB = (float)tempCoefArray[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray[0];                                       
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));
                                           
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step8...WriteCoef");
                                       
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());
                                    
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());
                                      
                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefa
                                        isWriteCoefAOk = dut.SetTxSlopcoefa(txDmiSlopeCoefA.ToString());
                                     
                                        if (isWriteCoefAOk)
                                        {
                                            isWriteCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());
                                          
                                        }
                                        else
                                        {
                                            isWriteCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());
                                        }
                                        #endregion
                                        if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                          
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                        }
                                    }
                                }
#endregion
#region isTempRelativeTRUE

                                else
                                {
                                    logoStr += logger.AdapterLogString(0,"isTempRelative:true");
                                    
                                    tempCoefBArray.Clear();
                                    tempCoefCArray.Clear();
                                    for (byte i = 0; i < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows.Count; i++)
                                    {
                                        for (byte j = 0; j < adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Columns.Count; j++)
                                        {
                                            txPowerAdc[j] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[i][j].ToString());
                                            tePowerUw[j] = double.Parse(adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[i][j].ToString());
                                            tePowerUw[j] = tePowerUw[j] * 10;
                                             logoStr += logger.AdapterLogString(0,"isTempRelative:true");
                                             logoStr += logger.AdapterLogString(1,"txPowerAdc:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxPowerAdc.Rows[i][j].ToString() + " tePowerUw:" + adjustTxPowerDmitValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].DataTableTxLop.Rows[i][j].ToString()); 
                                        }
                                       
                                        double[] tempCoefArray = algorithm.MultiLine(txPowerAdc, tePowerUw, txPowerAdc.Length, 1);
                                        tempCoefBArray.Add(tempCoefArray[1]);
                                        tempCoefCArray.Add(tempCoefArray[0]);
                                    }
                                    if (adjustTxPowerDmiStruct.is1Stor2StorPid == 1)
                                    {
                                        double[] tempAdc = new double[tempratureADCArrayList.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempAdc[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                        double[] tempCoefArray1 = algorithm.MultiLine(tempAdc, (double[])tempCoefBArray.ToArray(typeof(double)), tempratureADCArray.Count, 1);
                                        txDmiSlopeCoefB = (float)tempCoefArray1[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray1[0];
                                       
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray1);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i]))); 
                                           
                                        }
                                        double[] tempCoefArray2 = algorithm.MultiLine(tempAdc, (double[])tempCoefCArray.ToArray(typeof(double)), tempratureADCArray.Count, 1);
                                        txDmiOffsetCoefB = (float)tempCoefArray2[1];
                                        txDmiOffsetCoefC = (float)tempCoefArray2[0];
                                        txDmiOffsetCoefArray.Clear();
                                        txDmiOffsetCoefArray = ArrayList.Adapter(tempCoefArray2);
                                        txDmiOffsetCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiOffsetCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiOffsetCoefArray[" + i.ToString() + "]=" + txDmiOffsetCoefArray[i].ToString() + " "+
algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiOffsetCoefArray[i]))); 
                                            
                                        }
                                        logoStr += logger.AdapterLogString(0,"Step8...WriteCoef"); 
                                        
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());

                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());
                                      
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                        
                                        }
                                        #endregion

                                        #region W&R TxOffsetcoefb
                                        isWriteCoefOffsetBOk = dut.SetTxOffsetcoefb(txDmiOffsetCoefB.ToString());
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
                                        #region W&R TxOffsetcoefc
                                        isWriteCoefOffsetCOk = dut.SetTxOffsetcoefc(txDmiOffsetCoefC.ToString());      
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
                                        if (isWriteCoefBOk & isWriteCoefCOk & isWriteCoefOffsetCOk & isWriteCoefOffsetBOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                           
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString());
                                        }
                                    }
                                    else if (adjustTxPowerDmiStruct.is1Stor2StorPid == 2)
                                    {
                                        double[] tempAdc = new double[tempratureADCArrayList.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempAdc[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }

                                        double[] tempCoefArray1 = algorithm.MultiLine(tempAdc, (double[])tempCoefBArray.ToArray(typeof(double)), tempratureADCArray.Count, 2);
                                        txDmiSlopeCoefA = (float)tempCoefArray1[2];
                                        txDmiSlopeCoefB = (float)tempCoefArray1[1];
                                        txDmiSlopeCoefC = (float)tempCoefArray1[0];
                                        
                                        txDmiSlopeCoefArray = ArrayList.Adapter(tempCoefArray1);
                                        txDmiSlopeCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiSlopeCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "txDmiSlopeCoefArray[" + i.ToString() + "]=" + txDmiSlopeCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiSlopeCoefArray[i])));
                                          
                                        }
                                        double[] tempCoefArray2 = algorithm.MultiLine(tempAdc, (double[])tempCoefCArray.ToArray(typeof(double)), tempratureADCArray.Count, 2);
                                        txDmiOffsetCoefA = (float)tempCoefArray2[2];
                                        txDmiOffsetCoefB = (float)tempCoefArray2[1];
                                        txDmiOffsetCoefC = (float)tempCoefArray2[0];
                                        txDmiOffsetCoefArray.Clear();
                                        txDmiOffsetCoefArray = ArrayList.Adapter(tempCoefArray2);
                                        txDmiOffsetCoefArray.Reverse();
                                        for (byte i = 0; i < txDmiOffsetCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1,"txDmiOffsetCoefArray[" + i.ToString() + "]=" + txDmiOffsetCoefArray[i].ToString() + " "+algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(txDmiOffsetCoefArray[i])));
                                               
                                        }
                                        logoStr += logger.AdapterLogString(0,"Step7...WriteCoef");
                                       
                                        #region W&R TxSlopcoefc
                                        isWriteCoefCOk = dut.SetTxSlopcoefc(txDmiSlopeCoefC.ToString());
                                       
                                        if (isWriteCoefCOk)
                                        {
                                            isWriteCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                            
                                        }
                                        else
                                        {
                                            isWriteCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefC:" + isWriteCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefb
                                        isWriteCoefBOk = dut.SetTxSlopcoefb(txDmiSlopeCoefB.ToString());
                                    
                                        if (isWriteCoefBOk)
                                        {
                                            isWriteCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefB:" + isWriteCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxSlopcoefa
                                        isWriteCoefAOk = dut.SetTxSlopcoefa(txDmiSlopeCoefA.ToString());
                                       
                                        if (isWriteCoefAOk)
                                        {
                                            isWriteCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());
                                          
                                        }
                                        else
                                        {
                                            isWriteCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiSlopeCoefA:" + isWriteCoefAOk.ToString());
                                          
                                        }
                                        #endregion
                                        #region W&R TxOffsetcoefa
                                        isWriteCoefOffsetAOk = dut.SetTxOffsetcoefa(txDmiOffsetCoefA.ToString());
                                        
                                        if (isWriteCoefOffsetAOk)
                                        {
                                            isWriteCoefOffsetAOk = true;
                                            logoStr += logger.AdapterLogString(1, "WritetxDmiOffsetCoefA:" + isWriteCoefOffsetAOk.ToString());
                                           
                                        }
                                        else
                                        {
                                            isWriteCoefOffsetAOk = false;
                                            logoStr += logger.AdapterLogString(3, "WritetxDmiOffsetCoefA:" + isWriteCoefOffsetAOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxOffsetcoefb
                                        isWriteCoefOffsetBOk = dut.SetTxOffsetcoefb(txDmiOffsetCoefB.ToString());
                                      
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
                                        #region W&R TxOffsetcoefc
                                        isWriteCoefOffsetCOk = dut.SetTxOffsetcoefc(txDmiOffsetCoefC.ToString());
                                       
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
                                        if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk & isWriteCoefOffsetCOk & isWriteCoefOffsetBOk & isWriteCoefOffsetAOk)
                                        {
                                            isCalTxDmiOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxDmiOk:" + isCalTxDmiOk.ToString()); 
                                          
                                        }
                                        else
                                        {
                                            isCalTxDmiOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxDmiOk:" + isCalTxDmiOk.ToString()); 
                                        }

                                    }

                                }
#endregion
                            }
                        }
#endregion
                    }
                
#endregion
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();
                return isAdjustTxPowerDmiOk;
            }
            else
            {
                isAdjustTxPowerDmiOk = false;
                logoStr += logger.AdapterLogString(4,"Equipments is not enough!");
                logger.FlushLogBuffer();
                return isAdjustTxPowerDmiOk;
            }
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
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTXDMICOEF", out index))
                    {
                        if (adjustTxPowerDmiStruct.isTempRelative == false)
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(txDmiSlopeCoefArray, ",");
                            
                        }
                        else
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(txDmiSlopeCoefArray, ",") + "," + algorithm.ArrayListToStringArraySegregateByPunctuations(txDmiOffsetCoefArray, ",");
                           
                        }
                    }                  

                }
               
                return true;
            }
           

        }
        public bool PrepareEnvironment(EquipmentList aEquipList)
        {
            if (selectedEquipList["SCOPE"] != null)
            {
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];
                if (tempScope.SetMaskAlignMethod(1) &&
                    tempScope.SetMode(0) &&
                    tempScope.MaskONOFF(false) &&
                    tempScope.SetRunTilOff() &&
                    tempScope.RunStop(true) &&
                    tempScope.OpenOpticalChannel(true) &&
                    tempScope.RunStop(true) &&
                    tempScope.ClearDisplay()&&
                    tempScope.AutoScale()&&
                    tempScope.SetScaleOffset()
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
            logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");
                 
            if (InformationList.Length < inPutParametersNameArray.Count)
            {
                logoStr += logger.AdapterLogString(4,"InputParameters are not enough!");
                return false;
            }
            else
            {
                int index = -1;
                bool isParametersComplete = false;
                for (byte i = 0; i < inPutParametersNameArray.Count; i++)
                {                    
                    if (algorithm.FindFileName(InformationList,inPutParametersNameArray[i].ToString(),out index)==false)
                    {
                        logoStr += logger.AdapterLogString(4,inPutParametersNameArray[i].ToString() + "is not exist");      
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
                        if (algorithm.FindFileName(InformationList, "AUTOTUNE", out index))
                        {
                            adjustTxPowerDmiStruct.AutoTune = Convert.ToBoolean(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "FIXEDMODDAC(MA)", out index))
                        {
                            adjustTxPowerDmiStruct.FixedModDac = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        
                        if (algorithm.FindFileName(InformationList, "ARRAYIBIAS(MA)", out index))
                        {
                            adjustTxPowerDmiStruct.ArrayIbias =  ArrayList.Adapter(InformationList[index].DefaultValue.Split(new char[] { ',' }));
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IBIASADCORTXPOWERADC", out index))
                        {
                            adjustTxPowerDmiStruct.IBiasADCorTxPowerADC =Convert.ToByte(InformationList[index].DefaultValue); 
                           
                        }
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPID", out index))
                        {
                            adjustTxPowerDmiStruct.is1Stor2StorPid =Convert.ToByte(InformationList[index].DefaultValue); 
                           
                        }
                        if (algorithm.FindFileName(InformationList, "ISTEMPRELATIVE", out index))
                        {
                            adjustTxPowerDmiStruct.isTempRelative =Convert.ToBoolean(InformationList[index].DefaultValue); 
                           
                        }
                        if (algorithm.FindFileName(InformationList, "DCTODC", out index))
                        {
                            adjustTxPowerDmiStruct.isDCtoDC = Convert.ToBoolean(InformationList[index].DefaultValue);

                        }
                        if (algorithm.FindFileName(InformationList, "FIXEDCROSSDAC", out index))
                        {
                            adjustTxPowerDmiStruct.FIXEDCrossDac = Convert.ToUInt32(InformationList[index].DefaultValue);

                        }
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }
#endregion
    }
}