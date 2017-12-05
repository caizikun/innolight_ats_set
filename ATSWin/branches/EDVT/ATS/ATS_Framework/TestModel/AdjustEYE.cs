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
    public class AdjustEye : TestModelBase
    { 
#region Attribute
        private SortedList<string, AdjustEyeTargetValueRecordsStruct> adjustEyeTargetValueRecordsStructArray=new SortedList<string, AdjustEyeTargetValueRecordsStruct>();
        private AdjustEYEStruce adjustEYEStruce=new AdjustEYEStruce();       
              
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private ArrayList tempratureADCArrayList = new ArrayList();
        private ArrayList realtempratureArrayList = new ArrayList(); 
        private DataTable txPowerAdcArray = new DataTable();
        private DataTable txPoweruwArray = new DataTable();
        private ArrayList txPowerADC = new ArrayList();
        private ArrayList erortxPowerValueArray = new ArrayList();
        private ArrayList inPutParametersNameArray = new ArrayList(); 
       
        //private byte beCalledCount;
        private bool isTxPowerAdjustOk=false;
        private bool isErAdjustOk = false;
       
        //......
        private UInt32 ibiasDacTarget = 0;
        private UInt32 imodDacTarget = 0;
        private UInt32 txpowerAdcTarget = 0;
        private double targetLOP = -1;
        private double targetER = -1;
        
        //.....
      
        // cal txpower
        private float openLoopTxPowerCoefA;
        private float openLoopTxPowerCoefB;
        private float openLoopTxPowerCoefC;
        private float closeLoopTxPowerCoefA;
        private float closeLoopTxPowerCoefB;
        private float closeLoopTxPowerCoefC;       
        private ArrayList openLoopTxPowerCoefArray = new ArrayList();
        private ArrayList closeLoopTxPowerCoefArray = new ArrayList();
        private ArrayList pidTxPowerTempCoefCoefArray = new ArrayList();
        private bool isCalTxPowerOk;
        // cal txpower
        // cal er
        private float erModulationCoefA;
        private float erModulationCoefB;
        private float erModulationCoefC;
        private ArrayList modulationCoefArray = new ArrayList();
        private bool isCalErOk; 
        // cal er           
           
#endregion
        
#region Method
        public AdjustEye(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut; 
            txPowerAdcArray.Clear();           
            txPoweruwArray.Clear();
            tempratureADCArray.Clear();
            adjustEyeTargetValueRecordsStructArray.Clear();
            openLoopTxPowerCoefArray.Clear();
            closeLoopTxPowerCoefArray.Clear();
            pidTxPowerTempCoefCoefArray.Clear();
            tempratureADCArrayList.Clear();
            realtempratureArrayList.Clear();
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("AUTOTUNE");
            inPutParametersNameArray.Add("TXLOPTARGET(UW)");
            inPutParametersNameArray.Add("TXLOPTOLERANCE(UW)");
            inPutParametersNameArray.Add("FIXEDMOD(MA)");
            inPutParametersNameArray.Add("IBIASMAX(MA)");
            inPutParametersNameArray.Add("IBIASMIN(MA)");
            inPutParametersNameArray.Add("IBIASSTART(MA)");
            inPutParametersNameArray.Add("IBIASSTEP(MA)");
            inPutParametersNameArray.Add("IBIASMETHOD");
            inPutParametersNameArray.Add("IMODMAX(MA)");
            inPutParametersNameArray.Add("IMODMIN(MA)");
            inPutParametersNameArray.Add("TXERTARGET(DB)");
            inPutParametersNameArray.Add("TXERTOLERANCE(DB)");
            inPutParametersNameArray.Add("IMODMETHOD");
            inPutParametersNameArray.Add("IMODSTEP");
            inPutParametersNameArray.Add("IMODSTART(MA)");
            inPutParametersNameArray.Add("ISOPENLOOPORCLOSELOOPORBOTH");
            inPutParametersNameArray.Add("1STOR2STORPIDER");
            inPutParametersNameArray.Add("1STOR2STORPIDTXLOP");
            inPutParametersNameArray.Add("DCTODC");
            inPutParametersNameArray.Add("FIXEDCROSSDAC");
            inPutParametersNameArray.Add("PIDCOEFARRAY");
            //...
            
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
                selectedEquipList.Clear();
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
            bool isWriteCloseLoopCoefCOk = false;
            bool isWriteCloseLoopCoefBOk = false;
            bool isWriteCloseLoopCoefAOk = false;
            bool isWriteOpenLoopCoefCOk = false;
            bool isWriteOpenLoopCoefBOk = false;
            bool isWriteOpenLoopCoefAOk = false;
            bool isWriteCoefCOk = false;
            bool isWriteCoefBOk = false;
            bool isWriteCoefAOk = false;
           
            bool isPidPCoefOk = false;
            bool isPidICoefOk = false;
            bool isPidDCoefOk = false;
           
            if (PrepareEnvironment(selectedEquipList)==false)
            {
                logger.FlushLogBuffer();
                return false;
            }
            if (AnalysisInputParameters(inputParameters)==false)
            {
                logger.FlushLogBuffer();
                return false;
            }
           
            if (selectedEquipList["SCOPE"] != null && selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                // close apc 
                string apcstring = null;
                dut.APCStatus(out  apcstring);
                if (apcstring == "ON" || apcstring == "FF")
                {
                    logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                    dut.APCOFF();
                    logoStr += logger.AdapterLogString(0, "Power off");
                    tempps.Switch(false);
                    Thread.Sleep(200);
                    logoStr += logger.AdapterLogString(0, "Power on");
                    tempps.Switch(true);
                    Thread.Sleep(200);
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
#region NODCtoDC

                if (adjustEYEStruce.isDCtoDC==false)
                {
                    logoStr += logger.AdapterLogString(0, "Step3...Fix ImodValue");
                    logoStr += logger.AdapterLogString(1, "FixedMod=" + adjustEYEStruce.FixedMod.ToString());
                    dut.WriteModDac(adjustEYEStruce.FixedMod); // add parameters               
                    Thread.Sleep(200);
                    logoStr += logger.AdapterLogString(1, "SetScaleOffset");
                    tempScope.SetScaleOffset();
                    logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");

                    ArrayList tempProcessDate = new ArrayList();
                    UInt32 terminalValue = 0;
                    UInt32 tempTxPowerAdc = 0;
                    isTxPowerAdjustOk = OnesectionMethod(adjustEYEStruce.IbiasStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.TxLOPTolerance, adjustEYEStruce.IbiasMax, adjustEYEStruce.IbiasMin, adjustEYEStruce.TxLOPULSPEC, adjustEYEStruce.TxLOPLLSPEC, tempScope, dut, 0, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetLOP);
                    ibiasDacTarget = terminalValue;
                    txpowerAdcTarget = tempTxPowerAdc;




                    #region  add clomn
                    ////byte tempCount=Convert.ToByte(Math.Max(txPowerADC.Count,erortxPowerValueArray.Count));
                    Byte tempColumn = Convert.ToByte(txPoweruwArray.Columns.Count);
                    for (byte i = 0; i < erortxPowerValueArray.Count - tempColumn; i++)
                    {
                        txPoweruwArray.Columns.Add((tempColumn + i).ToString(), typeof(double));
                    }
                    tempColumn = Convert.ToByte(txPowerAdcArray.Columns.Count);
                    for (byte i = 0; i < txPowerADC.Count - tempColumn; i++)
                    {
                        txPowerAdcArray.Columns.Add((tempColumn + i).ToString(), typeof(double));

                    }

                    #endregion
                    #region  add row

                    {
                        DataRow rowAdc = txPowerAdcArray.NewRow();
                        DataRow rowPower = txPoweruwArray.NewRow();
                        for (byte j = 0; j < txPowerADC.Count; j++)
                        {
                            rowAdc[j.ToString()] = txPowerADC[j];
                        }
                        for (byte j = 0; j < erortxPowerValueArray.Count; j++)
                        {

                            rowPower[j.ToString()] = erortxPowerValueArray[j];

                        }
                        txPowerAdcArray.Rows.Add(rowAdc);
                        txPoweruwArray.Rows.Add(rowPower);

                    }
                    #endregion


                    Thread.Sleep(2000);
                    logoStr += logger.AdapterLogString(0, "Step6...StartAdjustEr");

                    isErAdjustOk = OnesectionMethod(adjustEYEStruce.ImodStart, adjustEYEStruce.ImodStep, adjustEYEStruce.TxErTarget, adjustEYEStruce.TxErTolerance, adjustEYEStruce.ImodMax, adjustEYEStruce.ImodMin, adjustEYEStruce.ErULSPEC, adjustEYEStruce.ErLLSPEC, tempScope, dut, 1, out txPowerADC, out erortxPowerValueArray, out tempTxPowerAdc, out tempProcessDate, out terminalValue, out targetER);
                    imodDacTarget = terminalValue;
                    Thread.Sleep(100);
                    if (isTxPowerAdjustOk == false || isErAdjustOk == false)
                    {
                        if (isTxPowerAdjustOk == false)
                        {
                            logoStr += logger.AdapterLogString(3, "isTxPowerAdjustOk=" + isTxPowerAdjustOk.ToString());
                        }
                        if (isErAdjustOk == false)
                        {
                            logoStr += logger.AdapterLogString(3, "isErAdjustOk=" + isErAdjustOk.ToString());
                        }
                        logger.FlushLogBuffer();
                        return false;
                    }
                    else if (isTxPowerAdjustOk && isErAdjustOk)
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
                            realtempratureArrayList.Add(GlobalParameters.CurrentTemp);
                        }

                        #endregion
                        #region  add current channel
                        if (!adjustEyeTargetValueRecordsStructArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                        {
                            logoStr += logger.AdapterLogString(0, "Step5...add current channel records");
                            logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());

                            AdjustEyeTargetValueRecordsStruct tempstruct = new AdjustEyeTargetValueRecordsStruct();
                            tempstruct.ibiasDacArray = new ArrayList();
                            tempstruct.imodulaDacArray = new ArrayList();
                            tempstruct.targetTxPowerADCArray = new ArrayList();
                            adjustEyeTargetValueRecordsStructArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempstruct);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Add(txpowerAdcTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Add(ibiasDacTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Add(imodDacTarget);
                        }
                        else
                        {
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Add(txpowerAdcTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Add(ibiasDacTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Add(imodDacTarget);
                        }
                        #region AddAdjustTxPowerLogo
                        for (byte i = 0; i < tempProcessDate.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "Ibias:" + tempProcessDate[i].ToString());

                        }
                        for (byte i = 0; i < erortxPowerValueArray.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, erortxPowerValueArray[i].ToString());

                        }
                        for (byte i = 0; i < txPowerADC.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "TxPowerAdc:" + txPowerADC[i].ToString());

                        }
                        logoStr += logger.AdapterLogString(1, "TargetIbiasDac=" + adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].ibiasDacArray[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].ibiasDacArray.Count - 1].ToString());
                        logoStr += logger.AdapterLogString(1, "TargetTxPowerAdc=" + adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].targetTxPowerADCArray[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].targetTxPowerADCArray.Count - 1].ToString());
                        logoStr += logger.AdapterLogString(1, isTxPowerAdjustOk.ToString());

                        #endregion
                        #region AddAdjustErLogo
                        for (byte i = 0; i < tempProcessDate.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "Modulation:" + tempProcessDate[i].ToString());

                        }
                        for (byte i = 0; i < erortxPowerValueArray.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "Er:" + erortxPowerValueArray[i].ToString());
                        }

                        logoStr += logger.AdapterLogString(1, "TargetIModDac=" + adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].imodulaDacArray[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].imodulaDacArray.Count - 1].ToString());
                        logoStr += logger.AdapterLogString(1, isErAdjustOk.ToString());


                        #endregion
                        #endregion
                        #region  CurveCoef
                        if (adjustEyeTargetValueRecordsStructArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                        {

                            if (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count >= 2 && tempratureADCArray.Count >= 2 ||
                                (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count >= 2 && tempratureADCArray.Count >= 2))
                            {
                                logoStr += logger.AdapterLogString(0, "Step8...CurveCoef current channel");
                                logoStr += logger.AdapterLogString(1, "CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());


                                #region openloop&closeloop
                                if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 0)
                                {
                                    if (adjustEYEStruce.isTxPower1Stor2StorPid == 2)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];        
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                       
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefA = (float)coefArray[2];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefa
                                        isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());
                                        if (isWriteOpenLoopCoefAOk)
                                        {
                                            isWriteOpenLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        #endregion


                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefA = (float)coefArray1[2];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);
                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        #endregion
                                        #region W&R TxPowerAdcccoefa
                                        isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());

                                        if (isWriteCloseLoopCoefAOk)
                                        {
                                            isWriteCloseLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                                        }
                                        #endregion
                                        if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk & isWriteCloseLoopCoefAOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }


                                    }
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 1)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                       
                                        
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                       
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 1);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                                        }

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);
                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion


                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 1);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");
                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);
                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(200);
                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());
                                        }
                                        #endregion

                                        if (isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                    }

                                }

                                #endregion
                                #region openloop

                                if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 1)
                                {
                                    if (adjustEYEStruce.isTxPower1Stor2StorPid == 2)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                       
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];

                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                       
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }

                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefA = (float)coefArray[2];

                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(400);
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefa
                                        isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefAOk)
                                        {
                                            isWriteOpenLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }

                                        #endregion
                                    }
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 1)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                       
                                        
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];

                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                      
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }

                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + "" + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");
                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion

                                        if (isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                    }


                                }
                                #endregion
                                #region close loop

                                if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 2)
                                {
                                    if (adjustEYEStruce.isTxPower1Stor2StorPid == 2)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                       
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefA = (float)coefArray1[2];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());

                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdcccoefa
                                        isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefAOk)
                                        {
                                            isWriteCloseLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                                        }
                                        if (isWriteCloseLoopCoefAOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                        #endregion
                                    }
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 1)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];   
                                        
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                       
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");
                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());
                                        }
                                        #endregion

                                        if (isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                    }

                                }
                                #endregion
                            }
                            #region coef er
                            if (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count >= 2 && tempratureADCArray.Count >= 2)
                            {
                                logoStr += logger.AdapterLogString(0, "Step10...CurveCoef ER");

                                if (adjustEYEStruce.isEr1Stor2StorPid == 1)
                                {
                                    double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                   
                                    double[] tempModulationDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count];

                                    for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                    {
                                        tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                    }

                                    for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count; i++)
                                    {
                                        tempModulationDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray[i].ToString());
                                    }
                                    double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempModulationDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count), 1);
                                    erModulationCoefC = (float)coefArray[0];
                                    erModulationCoefB = (float)coefArray[1];
                                    modulationCoefArray = ArrayList.Adapter(coefArray);
                                    modulationCoefArray.Reverse();
                                    for (byte i = 0; i < modulationCoefArray.Count; i++)
                                    {
                                        logoStr += logger.AdapterLogString(1, "modulationCoefArray[" + i.ToString() + "]=" + modulationCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(modulationCoefArray[i])));

                                    }
                                    logoStr += logger.AdapterLogString(0, "Step11...WriteCoef");
                                    #region W&R Moddaccoefc
                                    isWriteCoefCOk = dut.SetModdaccoefc(erModulationCoefC.ToString());
                                    Thread.Sleep(400);

                                    if (isWriteCoefCOk)
                                    {
                                        isWriteCoefCOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    else
                                    {
                                        isWriteCoefCOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    #endregion
                                    #region W&R Moddaccoefb
                                    isWriteCoefBOk = dut.SetModdaccoefb(erModulationCoefB.ToString());
                                    if (isWriteCoefBOk)
                                    {
                                        isWriteCoefBOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefBOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());

                                    }
                                    #endregion
                                    if (isWriteCoefBOk & isWriteCoefCOk)
                                    {
                                        isCalErOk = true;
                                        logoStr += logger.AdapterLogString(1, "isCalErOk:" + isCalErOk.ToString());

                                    }
                                    else
                                    {
                                        isCalErOk = false;
                                        logoStr += logger.AdapterLogString(3, "isCalErOk:" + isCalErOk.ToString());
                                    }

                                }
                                else if (adjustEYEStruce.isEr1Stor2StorPid == 2)
                                {
                                    double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                    double[] tempModulationDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count];

                                    for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                    {
                                        tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                    }
                                    for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count; i++)
                                    {
                                        tempModulationDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray[i].ToString());
                                    }
                                    double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempModulationDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count), 2);
                                    erModulationCoefC = (float)coefArray[0];
                                    erModulationCoefB = (float)coefArray[1];
                                    erModulationCoefA = (float)coefArray[2];
                                    modulationCoefArray = ArrayList.Adapter(coefArray);
                                    modulationCoefArray.Reverse();
                                    for (byte i = 0; i < modulationCoefArray.Count; i++)
                                    {
                                        logoStr += logger.AdapterLogString(1, "modulationCoefArray[" + i.ToString() + "]=" + modulationCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(modulationCoefArray[i])));
                                    }
                                    logoStr += logger.AdapterLogString(0, "Step12...WriteCoef");

                                    #region W&R Moddaccoefc
                                    isWriteCoefCOk = dut.SetModdaccoefc(erModulationCoefC.ToString());
                                    Thread.Sleep(400);

                                    if (isWriteCoefCOk)
                                    {
                                        isWriteCoefCOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    else
                                    {
                                        isWriteCoefCOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    #endregion
                                    #region W&R Moddaccoefb
                                    isWriteCoefBOk = dut.SetModdaccoefb(erModulationCoefB.ToString());
                                    if (isWriteCoefBOk)
                                    {
                                        isWriteCoefBOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefBOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());
                                    }
                                    #endregion
                                    #region W&R Moddaccoefa
                                    isWriteCoefAOk = dut.SetModdaccoefa(erModulationCoefA.ToString());

                                    if (isWriteCoefAOk)
                                    {
                                        isWriteCoefAOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefA:" + isWriteCoefAOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefAOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefA:" + isWriteCoefAOk.ToString());
                                    }
                                    #endregion

                                    if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                                    {
                                        isCalErOk = true;
                                        logoStr += logger.AdapterLogString(1, "isCalErOk:" + isCalErOk.ToString());
                                    }
                                    else
                                    {
                                        isCalErOk = false;
                                        logoStr += logger.AdapterLogString(3, "isCalErOk:" + isCalErOk.ToString());
                                    }

                                }
                            }
                            #endregion


                        }

                        #endregion
                        AnalysisOutputParameters(outputParameters);
                        logger.FlushLogBuffer();
                        return true;
                    }
                } 
#endregion 
#region DCTODC

                else
                {
                    logoStr += logger.AdapterLogString(0, "Step3...Fix ImodValue");
                    logoStr += logger.AdapterLogString(0, "Step4...Start Adjust TxPower");
                    logoStr += logger.AdapterLogString(0, "Step5...SetScaleOffset");
                    tempScope.SetScaleOffset();
                    OnesectionMethodERandPower(adjustEYEStruce.IbiasStart, adjustEYEStruce.ImodStart, adjustEYEStruce.IbiasStep, adjustEYEStruce.ImodStep, adjustEYEStruce.TxLOPTarget, adjustEYEStruce.TxLOPTolerance, adjustEYEStruce.IbiasMax, adjustEYEStruce.IbiasMin, adjustEYEStruce.TxErTarget, adjustEYEStruce.TxErTolerance, tempScope, dut, adjustEYEStruce.ImodMax, adjustEYEStruce.ImodMin, out ibiasDacTarget, out imodDacTarget, out targetER, out txpowerAdcTarget, out targetLOP, out isErAdjustOk, out isTxPowerAdjustOk); 
                    Thread.Sleep(200);
                    logoStr += logger.AdapterLogString(0, "Step6...StartAdjustEr");
                    Thread.Sleep(100);
                    if (isTxPowerAdjustOk == false || isErAdjustOk == false)
                    {
                        if (isTxPowerAdjustOk == false)
                        {
                            logoStr += logger.AdapterLogString(3, "isTxPowerAdjustOk=" + isTxPowerAdjustOk.ToString());
                        }
                        if (isErAdjustOk == false)
                        {
                            logoStr += logger.AdapterLogString(3, "isErAdjustOk=" + isErAdjustOk.ToString());
                        }
                        logger.FlushLogBuffer();
                        return false;
                    }
                    else if (isTxPowerAdjustOk && isErAdjustOk)
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
                        #region  add current channel
                        if (!adjustEyeTargetValueRecordsStructArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                        {
                            logoStr += logger.AdapterLogString(0, "Step5...add current channel records");
                            logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());

                            AdjustEyeTargetValueRecordsStruct tempstruct = new AdjustEyeTargetValueRecordsStruct();
                            tempstruct.ibiasDacArray = new ArrayList();
                            tempstruct.targetTxPowerUWArray = new ArrayList();
                            tempstruct.imodulaDacArray = new ArrayList();
                            tempstruct.targetTxPowerADCArray = new ArrayList();
                            adjustEyeTargetValueRecordsStructArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), tempstruct);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Add(txpowerAdcTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Add(ibiasDacTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerUWArray.Add(targetLOP);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Add(imodDacTarget);
                        }
                        else
                        {
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Add(txpowerAdcTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Add(ibiasDacTarget);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerUWArray.Add(targetLOP);
                            adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Add(imodDacTarget);
                        }
                        #region AddAdjustTxPowerLogo
                       
                        logoStr += logger.AdapterLogString(1, "TargetIbiasDac=" + adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].ibiasDacArray[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].ibiasDacArray.Count - 1].ToString());
                        logoStr += logger.AdapterLogString(1, "TargetTxPowerAdc=" + adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].targetTxPowerADCArray[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].targetTxPowerADCArray.Count - 1].ToString());                
                        logoStr += logger.AdapterLogString(1, isTxPowerAdjustOk.ToString());
                       
                        #endregion
                        #region AddAdjustErLogo                        

                        logoStr += logger.AdapterLogString(1, "TargetIModDac=" + adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].imodulaDacArray[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim()].imodulaDacArray.Count - 1].ToString());
                        logoStr += logger.AdapterLogString(1, isErAdjustOk.ToString());


                        #endregion
                        #endregion
                        #region  CurveCoef
                        if (adjustEyeTargetValueRecordsStructArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                        {
                            if (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count >= 2 && tempratureADCArray.Count >= 2 ||
                                (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count >= 2 && tempratureADCArray.Count >= 2))
                            {
                                logoStr += logger.AdapterLogString(0, "Step8...CurveCoef current channel");
                                logoStr += logger.AdapterLogString(1, "CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                                #region openloop&closeloop
                                if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 0)
                                {
                                    if (adjustEYEStruce.isTxPower1Stor2StorPid == 2)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];                                       
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                       
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefA = (float)coefArray[2];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefa
                                        isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());
                                        if (isWriteOpenLoopCoefAOk)
                                        {
                                            isWriteOpenLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        #endregion


                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefA = (float)coefArray1[2];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);
                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        #endregion
                                        #region W&R TxPowerAdcccoefa
                                        isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());

                                        if (isWriteCloseLoopCoefAOk)
                                        {
                                            isWriteCloseLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                                        }
                                        #endregion
                                        if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk & isWriteCloseLoopCoefAOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }


                                    }
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 1)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                       
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 1);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                                        }

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                       
                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion


                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 1);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");
                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                       
                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        
                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());
                                        }
                                        #endregion

                                        if (isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                    }
#region PID
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 3)
                                    {
                                        writeCurrentChannelPID(dut);
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }

                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefA = (float)coefArray[2];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefa
                                        isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());
                                        if (isWriteOpenLoopCoefAOk)
                                        {
                                            isWriteOpenLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        #endregion


                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefA = (float)coefArray1[2];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString()); 
                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        #endregion
                                        #region W&R TxPowerAdcccoefa
                                        isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());

                                        if (isWriteCloseLoopCoefAOk)
                                        {
                                            isWriteCloseLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                                        }
                                        #endregion
                                        if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk & isWriteCloseLoopCoefAOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk & isPidPCoefOk & isPidICoefOk & isPidDCoefOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }
                                    }
#endregion
                                }

                                #endregion
                                #region openloop

                                if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 1)
                                {
                                    if (adjustEYEStruce.isTxPower1Stor2StorPid == 2)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];

                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }

                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefA = (float)coefArray[2];

                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(400);
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefa
                                        isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefAOk)
                                        {
                                            isWriteOpenLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }

                                        #endregion
                                    }
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 1)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];

                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }

                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + "" + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));
                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");
                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(400);

                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion

                                        if (isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                    }
                                    #region PID
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 3)
                                    {
                                        writeCurrentChannelPID(dut);
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }

                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefA = (float)coefArray[2];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefa
                                        isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());
                                        if (isWriteOpenLoopCoefAOk)
                                        {
                                            isWriteOpenLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        #endregion


                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefA = (float)coefArray1[2];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        #endregion
                                        #region W&R TxPowerAdcccoefa
                                        isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());

                                        if (isWriteCloseLoopCoefAOk)
                                        {
                                            isWriteCloseLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                                        }
                                        #endregion
                                        if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk & isWriteCloseLoopCoefAOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk & isPidPCoefOk & isPidICoefOk & isPidDCoefOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                                #region close loop

                                if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 2)
                                {
                                    if (adjustEYEStruce.isTxPower1Stor2StorPid == 2)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }

                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefA = (float)coefArray1[2];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());

                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdcccoefa
                                        isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefAOk)
                                        {
                                            isWriteCloseLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                                        }
                                        if (isWriteCloseLoopCoefAOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                        #endregion
                                    }
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 1)
                                    {
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }

                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");
                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        Thread.Sleep(200);

                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());
                                        }
                                        #endregion

                                        if (isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }

                                    }
 #region PID
                                    else if (adjustEYEStruce.isTxPower1Stor2StorPid == 3)
                                    {
                                        writeCurrentChannelPID(dut);
                                        double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                        double[] tempIbiasDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count];
                                        double[] tempTxPowerAdcArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count];
                                        for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                        {
                                            tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                        }

                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count; i++)
                                        {
                                            tempIbiasDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray[i].ToString());
                                        }
                                        for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count; i++)
                                        {
                                            tempTxPowerAdcArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray[i].ToString());
                                        }
                                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempIbiasDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].ibiasDacArray.Count), 2);
                                        openLoopTxPowerCoefC = (float)coefArray[0];
                                        openLoopTxPowerCoefB = (float)coefArray[1];
                                        openLoopTxPowerCoefA = (float)coefArray[2];
                                        openLoopTxPowerCoefArray = ArrayList.Adapter(coefArray);
                                        openLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < openLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "openLoopTxPowerCoefArray[" + i.ToString() + "]=" + openLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(openLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R Biasdaccoefc
                                        isWriteOpenLoopCoefCOk = dut.SetBiasdaccoefc(openLoopTxPowerCoefC.ToString());
                                        if (isWriteOpenLoopCoefCOk)
                                        {
                                            isWriteOpenLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefCOk:" + isWriteOpenLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefb
                                        isWriteOpenLoopCoefBOk = dut.SetBiasdaccoefb(openLoopTxPowerCoefB.ToString());
                                        if (isWriteOpenLoopCoefBOk)
                                        {
                                            isWriteOpenLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefBOk:" + isWriteOpenLoopCoefBOk.ToString());
                                        }
                                        #endregion
                                        #region W&R Biasdaccoefa
                                        isWriteOpenLoopCoefAOk = dut.SetBiasdaccoefa(openLoopTxPowerCoefA.ToString());
                                        if (isWriteOpenLoopCoefAOk)
                                        {
                                            isWriteOpenLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteOpenLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteOpenLoopCoefAOk:" + isWriteOpenLoopCoefAOk.ToString());
                                        }
                                        #endregion


                                        double[] coefArray1 = algorithm.MultiLine(tempTempAdcArray, tempTxPowerAdcArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].targetTxPowerADCArray.Count), 2);
                                        closeLoopTxPowerCoefC = (float)coefArray1[0];
                                        closeLoopTxPowerCoefB = (float)coefArray1[1];
                                        closeLoopTxPowerCoefA = (float)coefArray1[2];
                                        closeLoopTxPowerCoefArray = ArrayList.Adapter(coefArray1);
                                        closeLoopTxPowerCoefArray.Reverse();
                                        for (byte i = 0; i < closeLoopTxPowerCoefArray.Count; i++)
                                        {
                                            logoStr += logger.AdapterLogString(1, "closeLoopTxPowerCoefArray[" + i.ToString() + "]=" + closeLoopTxPowerCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(closeLoopTxPowerCoefArray[i])));

                                        }
                                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                                        #region W&R TxPowerAdccoefc
                                        isWriteCloseLoopCoefCOk = dut.SetCloseLoopcoefc(closeLoopTxPowerCoefC.ToString());
                                        if (isWriteCloseLoopCoefCOk)
                                        {
                                            isWriteCloseLoopCoefCOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefCOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefCOk:" + isWriteCloseLoopCoefCOk.ToString());
                                        }
                                        #endregion
                                        #region W&R TxPowerAdccoefb
                                        isWriteCloseLoopCoefBOk = dut.SetCloseLoopcoefb(closeLoopTxPowerCoefB.ToString());
                                        if (isWriteCloseLoopCoefBOk)
                                        {
                                            isWriteCloseLoopCoefBOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefBOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefBOk:" + isWriteCloseLoopCoefBOk.ToString());

                                        }
                                        #endregion
                                        #region W&R TxPowerAdcccoefa
                                        isWriteCloseLoopCoefAOk = dut.SetCloseLoopcoefa(closeLoopTxPowerCoefA.ToString());

                                        if (isWriteCloseLoopCoefAOk)
                                        {
                                            isWriteCloseLoopCoefAOk = true;
                                            logoStr += logger.AdapterLogString(1, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());

                                        }
                                        else
                                        {
                                            isWriteCloseLoopCoefAOk = false;
                                            logoStr += logger.AdapterLogString(3, "isWriteCloseLoopCoefAOk:" + isWriteCloseLoopCoefAOk.ToString());
                                        }
                                        #endregion
                                        if (isWriteOpenLoopCoefAOk & isWriteOpenLoopCoefBOk & isWriteOpenLoopCoefCOk & isWriteCloseLoopCoefAOk & isWriteCloseLoopCoefBOk & isWriteCloseLoopCoefCOk & isPidPCoefOk & isPidICoefOk & isPidDCoefOk)
                                        {
                                            isCalTxPowerOk = true;
                                            logoStr += logger.AdapterLogString(1, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());

                                        }
                                        else
                                        {
                                            isCalTxPowerOk = false;
                                            logoStr += logger.AdapterLogString(3, "isCalTxPowerOk:" + isCalTxPowerOk.ToString());
                                        }
                                    }
 #endregion
                                }
                                #endregion
                            }
                            #region coef er
                            if (adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count >= 2 && tempratureADCArray.Count >= 2)
                            {
                                logoStr += logger.AdapterLogString(0, "Step10...CurveCoef ER");

                                if (adjustEYEStruce.isEr1Stor2StorPid == 1)
                                {
                                    double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                    double[] tempModulationDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count];

                                    for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                    {
                                        tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                    }
                                    for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count; i++)
                                    {
                                        tempModulationDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray[i].ToString());
                                    }
                                    double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempModulationDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count), 1);
                                    erModulationCoefC = (float)coefArray[0];
                                    erModulationCoefB = (float)coefArray[1];
                                    modulationCoefArray = ArrayList.Adapter(coefArray);
                                    modulationCoefArray.Reverse();
                                    for (byte i = 0; i < modulationCoefArray.Count; i++)
                                    {
                                        logoStr += logger.AdapterLogString(1, "modulationCoefArray[" + i.ToString() + "]=" + modulationCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(modulationCoefArray[i])));

                                    }
                                    logoStr += logger.AdapterLogString(0, "Step11...WriteCoef");
                                    #region W&R Moddaccoefc
                                    isWriteCoefCOk = dut.SetModdaccoefc(erModulationCoefC.ToString());
                                    Thread.Sleep(400);

                                    if (isWriteCoefCOk)
                                    {
                                        isWriteCoefCOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    else
                                    {
                                        isWriteCoefCOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    #endregion
                                    #region W&R Moddaccoefb
                                    isWriteCoefBOk = dut.SetModdaccoefb(erModulationCoefB.ToString());
                                    if (isWriteCoefBOk)
                                    {
                                        isWriteCoefBOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefBOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());

                                    }
                                    #endregion
                                    if (isWriteCoefBOk & isWriteCoefCOk)
                                    {
                                        isCalErOk = true;
                                        logoStr += logger.AdapterLogString(1, "isCalErOk:" + isCalErOk.ToString());

                                    }
                                    else
                                    {
                                        isCalErOk = false;
                                        logoStr += logger.AdapterLogString(3, "isCalErOk:" + isCalErOk.ToString());
                                    }

                                }
                                else if (adjustEYEStruce.isEr1Stor2StorPid == 2)
                                {
                                    double[] tempTempAdcArray = new double[tempratureADCArray.Count];
                                    double[] tempModulationDacArray = new double[adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count];

                                    for (byte i = 0; i < tempratureADCArrayList.Count; i++)
                                    {
                                        tempTempAdcArray[i] = Convert.ToDouble(tempratureADCArrayList[i].ToString());
                                    }
                                    for (byte i = 0; i < adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count; i++)
                                    {
                                        tempModulationDacArray[i] = Convert.ToDouble(adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray[i].ToString());
                                    }
                                    double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempModulationDacArray, Math.Min(tempratureADCArray.Count, adjustEyeTargetValueRecordsStructArray[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].imodulaDacArray.Count), 2);
                                    erModulationCoefC = (float)coefArray[0];
                                    erModulationCoefB = (float)coefArray[1];
                                    erModulationCoefA = (float)coefArray[2];
                                    modulationCoefArray = ArrayList.Adapter(coefArray);
                                    modulationCoefArray.Reverse();
                                    for (byte i = 0; i < modulationCoefArray.Count; i++)
                                    {
                                        logoStr += logger.AdapterLogString(1, "modulationCoefArray[" + i.ToString() + "]=" + modulationCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(modulationCoefArray[i])));
                                    }
                                    logoStr += logger.AdapterLogString(0, "Step12...WriteCoef");

                                    #region W&R Moddaccoefc
                                    isWriteCoefCOk = dut.SetModdaccoefc(erModulationCoefC.ToString());
                                    Thread.Sleep(400);

                                    if (isWriteCoefCOk)
                                    {
                                        isWriteCoefCOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    else
                                    {
                                        isWriteCoefCOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefC:" + isWriteCoefCOk.ToString());
                                    }
                                    #endregion
                                    #region W&R Moddaccoefb
                                    isWriteCoefBOk = dut.SetModdaccoefb(erModulationCoefB.ToString());
                                    if (isWriteCoefBOk)
                                    {
                                        isWriteCoefBOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefBOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefB:" + isWriteCoefBOk.ToString());
                                    }
                                    #endregion
                                    #region W&R Moddaccoefa
                                    isWriteCoefAOk = dut.SetModdaccoefa(erModulationCoefA.ToString());

                                    if (isWriteCoefAOk)
                                    {
                                        isWriteCoefAOk = true;
                                        logoStr += logger.AdapterLogString(1, "WriteCoeferModulationCoefA:" + isWriteCoefAOk.ToString());

                                    }
                                    else
                                    {
                                        isWriteCoefAOk = false;
                                        logoStr += logger.AdapterLogString(3, "WriteCoeferModulationCoefA:" + isWriteCoefAOk.ToString());
                                    }
                                    #endregion

                                    if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                                    {
                                        isCalErOk = true;
                                        logoStr += logger.AdapterLogString(1, "isCalErOk:" + isCalErOk.ToString());
                                    }
                                    else
                                    {
                                        isCalErOk = false;
                                        logoStr += logger.AdapterLogString(3, "isCalErOk:" + isCalErOk.ToString());
                                    }

                                }
                            }
                            #endregion


                        }

                        #endregion
                        AnalysisOutputParameters(outputParameters);
                        logger.FlushLogBuffer();
                        return true;
                    }
                }
#endregion

            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                logger.FlushLogBuffer();
                return false;
            }
            return true;
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
                    if (algorithm.FindFileName(InformationList, "TXLOPTARGET(UW)", out index))
                    {
                        InformationList[index].DefaultValue = targetLOP.ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "TXERTARGET(DB)", out index))
                    {
                        InformationList[index].DefaultValue = targetER.ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTTXMODCOEF", out index))
                    {
                        InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(modulationCoefArray, ",");
                        
                    }

                    if (algorithm.FindFileName(InformationList, "ARRAYLISTTXPOWERCOEF", out index))
                    {
                        if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 0)
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(openLoopTxPowerCoefArray, ",") + "," + algorithm.ArrayListToStringArraySegregateByPunctuations(closeLoopTxPowerCoefArray, ",");

                            
                        }
                        if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 1)
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(openLoopTxPowerCoefArray, ",");
                            
                        }
                        else if (adjustEYEStruce.isOpenLooporCloseLooporBoth == 2)
                        {
                            InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(closeLoopTxPowerCoefArray, ",");
                           
                        }

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
                logoStr += logger.AdapterLogString(4, "InputParameters are not enough!"); 
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
                            adjustEYEStruce.AutoTune = Convert.ToBoolean(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "TXLOPTARGET(UW)", out index))
                        {
                            adjustEYEStruce.TxLOPTarget = Convert.ToDouble(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "TXLOPTOLERANCE(UW)", out index))
                        {
                            adjustEYEStruce.TxLOPTolerance = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "FIXEDMOD(MA)", out index))
                        {
                            adjustEYEStruce.FixedMod = Convert.ToUInt32(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "FIXEDMOD(MA)", out index))
                        {
                            adjustEYEStruce.FixedMod = Convert.ToUInt32(InformationList[index].DefaultValue);
                          
                        }
                        if (algorithm.FindFileName(InformationList, "IBIASMAX(MA)", out index))
                        {
                            adjustEYEStruce.IbiasMax = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IBIASMIN(MA)", out index))
                        {
                            adjustEYEStruce.IbiasMin = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IBIASSTART(MA)", out index))
                        {
                            adjustEYEStruce.IbiasStart = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IBIASMETHOD", out index))
                        {
                            adjustEYEStruce.IbiasMethod = Convert.ToByte(InformationList[index].DefaultValue);
                          
                        }
                        if (algorithm.FindFileName(InformationList, "IBIASSTEP(MA)", out index))
                        {
                            adjustEYEStruce.IbiasStep = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IMODMAX(MA)", out index))
                        {
                            adjustEYEStruce.ImodMax = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IMODMIN(MA)", out index))
                        {
                            adjustEYEStruce.ImodMin = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "TXERTARGET(DB)", out index))
                        {
                            adjustEYEStruce.TxErTarget = Convert.ToDouble(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "TXERTOLERANCE(DB)", out index))
                        {
                            adjustEYEStruce.TxErTolerance = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IMODSTART(MA)", out index))
                        {
                            adjustEYEStruce.ImodStart = Convert.ToUInt32(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "IMODMETHOD", out index))
                        {
                            adjustEYEStruce.ImodMethod = Convert.ToByte(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "IMODSTEP", out index))
                        {
                            adjustEYEStruce.ImodStep = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "ISOPENLOOPORCLOSELOOPORBOTH", out index))
                        {
                            adjustEYEStruce.isOpenLooporCloseLooporBoth = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPIDER", out index))
                        {
                            adjustEYEStruce.isEr1Stor2StorPid = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPIDTXLOP", out index))
                        {
                            adjustEYEStruce.isTxPower1Stor2StorPid = Convert.ToByte(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "DCTODC", out index))
                        {
                            adjustEYEStruce.isDCtoDC = Convert.ToBoolean(InformationList[index].DefaultValue);

                        }
                        if (algorithm.FindFileName(InformationList, "FIXEDCROSSDAC", out index))
                        {
                            adjustEYEStruce.FIXEDCrossDac = Convert.ToUInt32(InformationList[index].DefaultValue);

                        }
                        if (algorithm.FindFileName(InformationList, "PIDCOEFARRAY", out index))
                        {
                            char[] tempCharArray = new char[] { ',' }; 
                            adjustEYEStruce.pidCoefArray = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);

                        }
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");  
                return true;
            }
        }
        protected bool PrepareEnvironment(EquipmentList aEquipList)
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
                   tempScope.ClearDisplay() &&
                   tempScope.AutoScale()
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
        protected bool OnesectionMethod(UInt32 startValue, byte step, double targetValue, double tolerence, UInt32 uperLimit, UInt32 lowLimit, double Ulspec, double Llspec, Scope scope, DUT dut, byte IbiasModulation, out ArrayList xArray, out ArrayList yArray, out UInt32 ibiasTargetADC, out ArrayList adjustProcessData, out UInt32 terminalValue, out double targetLOPorERValue)//ibias=0;modulation=1
        {
            ibiasTargetADC = 0;
            adjustProcessData = new ArrayList();
            xArray = new ArrayList();
            yArray = new ArrayList();
            xArray.Clear();
            yArray.Clear();
            adjustProcessData.Clear();
            byte adjustCount = 0;
            byte backUpCount = 0;
            byte totalExponentiationCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(step), 2))));
            double currentLOPValue = -1;
            double lastPointLOPValue = -1;
            double TxPowerADC = -1;
            byte[] writeData = new byte[1];
            do
            {
                {
                    switch (IbiasModulation)
                    {
                        case 0:
                            {
                                dut.WriteBiasDac(startValue);
                                scope.ClearDisplay();
                                Thread.Sleep(200);
                                scope.SetScaleOffset();                               
                                currentLOPValue = scope.GetAveragePowerWatt();
                                scope.DisplayPowerWatt();
                                UInt16 Temp;
                                dut.ReadTxpADC(out Temp);
                                TxPowerADC = Convert.ToDouble(Temp);
                                Thread.Sleep(100);
                                break;
                            }
                        case 1:
                            {
                                dut.WriteModDac(startValue);
                                scope.ClearDisplay();
                                Thread.Sleep(200);
                                scope.SetScaleOffset();  
                                scope.DisplayER();
                                Thread.Sleep(200);
                                currentLOPValue = scope.GetEratio();
                                //Thread.Sleep(1000);
                                break;
                            }

                        default:
                            {
                                break;
                            }

                    }
                    adjustProcessData.Add(startValue);
                    if ((startValue == uperLimit) && (currentLOPValue < ((targetValue - tolerence))) || (startValue == lowLimit) && (currentLOPValue > ((targetValue + tolerence))))
                    {
                        terminalValue = startValue;
                        targetLOPorERValue = currentLOPValue;
                        if (currentLOPValue > ((targetValue + tolerence)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                        }
                        else if (currentLOPValue < ((targetValue - tolerence)))
                        {
                            logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                        }
                        logger.FlushLogBuffer();
                        return false;
                    }
                    if (adjustCount == 0)
                    {
                        if (currentLOPValue < ((targetValue - tolerence)))
                        {
                            int tempValue = (int)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= uperLimit ? uperLimit : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            startValue = (UInt32)tempValue;
                           
                            lastPointLOPValue = currentLOPValue;
                            if (IbiasModulation == 0)
                            {
                                xArray.Add(TxPowerADC);

                            }
                            yArray.Add(currentLOPValue);
                        }
                        else if (currentLOPValue > ((targetValue + tolerence)))
                        {
                            do
                            {
                                int tempValue = (int)(startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) <=lowLimit ? lowLimit : startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                                startValue = (UInt32)tempValue;
                                lastPointLOPValue = currentLOPValue;

                                if (IbiasModulation == 0)
                                {
                                    dut.WriteBiasDac(startValue);
                                    scope.ClearDisplay();
                                    Thread.Sleep(200);
                                    scope.SetScaleOffset();  
                                    currentLOPValue = scope.GetAveragePowerWatt();
                                    scope.DisplayPowerWatt();  
                                }
                                if (IbiasModulation == 1)
                                {
                                    dut.WriteModDac(startValue);
                                    scope.ClearDisplay();
                                    Thread.Sleep(200);
                                    scope.SetScaleOffset();   
                                    scope.DisplayER();
                                    Thread.Sleep(200);
                                    currentLOPValue = scope.GetEratio();
                                }
                                yArray.Add(currentLOPValue);
                                if ((startValue == lowLimit) && (currentLOPValue > ((targetValue + tolerence))))
                                {
                                    terminalValue = startValue;
                                    targetLOPorERValue = currentLOPValue;
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                            } while ((currentLOPValue > ((targetValue + tolerence))));
                            if (IbiasModulation == 0)
                            {
                                UInt16 Temp;
                                dut.ReadTxpADC(out Temp);
                                TxPowerADC = Convert.ToDouble(Temp);
                                Thread.Sleep(100);
                                xArray.Add(TxPowerADC);
                            }
                            yArray.Add(currentLOPValue);
                        }

                    }
                    else
                    {
                        if ((currentLOPValue < (targetValue - tolerence)))
                        {
                            int tempValue = (int)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= uperLimit ? uperLimit : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                            startValue = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue > (targetValue + tolerence)))
                        {
                            startValue -= (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount);
                            backUpCount++;
                            byte tempValue = (byte)((backUpCount) >= (byte)(totalExponentiationCount - 1) ? (byte)(totalExponentiationCount - 1) : backUpCount);
                            backUpCount = tempValue;
                            if (backUpCount < (byte)(totalExponentiationCount - 1))
                            {
                                int tempValue2 = (int)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= uperLimit ? uperLimit : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                                startValue = (UInt32)tempValue2;
                            }
                            
                        }
                        if (IbiasModulation == 0)
                        {
                            xArray.Add(TxPowerADC);
                        }

                        yArray.Add(currentLOPValue);
                        lastPointLOPValue = currentLOPValue;
                    }
                    if ((currentLOPValue < (targetValue - tolerence) || currentLOPValue > (targetValue + tolerence)))
                    {
                        adjustCount++;
                    }

                }

            } while (adjustCount <= 30 && (currentLOPValue < (targetValue - tolerence) || currentLOPValue > (targetValue + tolerence)));
            if (IbiasModulation == 0)
            {
                UInt16 Temp;
                dut.ReadTxpADC(out Temp);
                //Convert.ToDouble(Temp);
                ibiasTargetADC = Temp;
            }
            if (startValue > uperLimit || startValue < lowLimit)
            {
                if (startValue > uperLimit)
                {
                    startValue = uperLimit;
                    if (IbiasModulation == 0)
                    {
                        dut.WriteBiasDac(startValue);
                        scope.ClearDisplay();
                        Thread.Sleep(200);
                        scope.SetScaleOffset();
                        terminalValue = startValue;
                        scope.DisplayPowerWatt();
                        currentLOPValue = scope.GetAveragePowerWatt();
                    }
                    else
                    {
                        dut.WriteModDac(startValue);
                        terminalValue = startValue;
                        scope.ClearDisplay();
                        Thread.Sleep(200);
                        scope.SetScaleOffset();   
                        scope.DisplayER();
                        currentLOPValue = scope.GetEratio();

                    }
                }
                else if (startValue < lowLimit)
                {
                    startValue = lowLimit;
                    if (IbiasModulation == 0)
                    {
                        dut.WriteBiasDac(startValue);
                        scope.ClearDisplay();
                        Thread.Sleep(200);
                        scope.SetScaleOffset();
                        terminalValue = startValue;
                        scope.DisplayPowerWatt();
                        currentLOPValue = scope.GetAveragePowerWatt();
                        
                    }
                    else
                    {
                        dut.WriteModDac(startValue);
                        terminalValue = startValue;
                        scope.ClearDisplay();
                        Thread.Sleep(200);
                        scope.SetScaleOffset();                       
                        scope.DisplayER();
                        currentLOPValue = scope.GetEratio();

                    }
                }
            }

            targetLOPorERValue = currentLOPValue;
            if (currentLOPValue >= (targetValue - tolerence) && currentLOPValue <= (targetValue + tolerence))
            {
                terminalValue = startValue;
                return true;
            }
            else
            {
                terminalValue = startValue;
                return false;
            }

        }
        protected bool OnesectionMethodERandPower(UInt32 startValueIbias, UInt32 startValueMod, byte stepIbias, byte stepImod, double targetValueIbias, double tolerenceIbias, UInt32 uperLimitIbias, UInt32 lowLimitIbias, double targetValueIMod, double tolerenceIMod, Scope scope, DUT dut, UInt32 uperLimitIMod, UInt32 lowLimitIMod,out UInt32 ibiasDacTarget,out UInt32 imodDacTarget,out double targetERValue, out UInt32 TxPowerAdc,out double targetLOPValue,out bool isERok,out bool isLopok)//ibias=0;modulation=1
        {
            isERok = false;
            isLopok = false;
            byte adjustCount = 0;
            byte backUpCountLop = 0;
            byte backUpCountEr = 0;
            byte totalExponentiationLopCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepIbias), 2))));
            byte totalExponentiationERCount = Convert.ToByte(Math.Floor((Math.Log(Convert.ToDouble(stepImod), 2))));
            double currentLOPValue = -1;
            double currentERValue = -1;
            targetLOPValue = -1;
            targetERValue = -1;
            TxPowerAdc = 0;
            byte[] writeData = new byte[1];
            do
            {
                if (startValueIbias > uperLimitIbias)
                {
                    startValueIbias = uperLimitIbias;

                }
                if (startValueMod > uperLimitIMod)
                {
                    startValueMod = uperLimitIMod;
                }
                if (startValueIbias < lowLimitIbias)
                {
                    startValueIbias = lowLimitIbias;

                }
                if (startValueMod < lowLimitIMod)
                {
                    startValueMod = lowLimitIMod;
                }
                {  
                           {
                                dut.WriteBiasDac(startValueIbias);
                                scope.ClearDisplay();
                                Thread.Sleep(100);
                                scope.SetScaleOffset();
                                scope.DisplayPowerWatt();
                                currentLOPValue = scope.GetAveragePowerWatt();
                                
                            }
                        
                            {
                                dut.WriteModDac(startValueMod);
                                scope.ClearDisplay();
                                scope.SetScaleOffset();
                                scope.DisplayER();
                                Thread.Sleep(200);
                                currentERValue = scope.GetEratio();
                               
                            }
                            if ((startValueIbias == uperLimitIbias) && (currentLOPValue < ((targetLOPValue - tolerenceIbias))) || (startValueIbias == lowLimitIbias) && (currentLOPValue > ((targetLOPValue + tolerenceIbias))))
                            {

                                if (currentLOPValue > ((targetLOPValue + tolerenceIbias)))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                }
                                else if (currentLOPValue < ((targetLOPValue - tolerenceIbias)))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                                }
                                ibiasDacTarget = startValueIbias;
                                imodDacTarget = startValueMod;
                                logger.FlushLogBuffer();
                                return false;
                            }
                            if ((startValueMod == uperLimitIMod) && (currentERValue < ((targetERValue - tolerenceIMod))) || (startValueMod == lowLimitIMod) && (currentERValue > ((targetERValue + tolerenceIMod))))
                            {
                                if (currentERValue > ((targetERValue + tolerenceIMod)))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                }
                                else if (currentERValue < ((targetERValue - tolerenceIMod)))
                                {
                                    logoStr += logger.AdapterLogString(4, "DataBase input Parameters uperLimit is too small!");
                                }
                                ibiasDacTarget = startValueIbias;
                                imodDacTarget = startValueMod;
                                logger.FlushLogBuffer();
                                return false;
                            }
                    if (adjustCount == 0)
                    {     
                        if (currentLOPValue < ((targetValueIbias - tolerenceIbias)))
                        {
                            int tempValue = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                            startValueIbias = (UInt32)tempValue;
                        }
                        if (currentERValue < ((targetValueIMod - tolerenceIMod)))
                        {
                            int tempValue = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                            startValueMod = (UInt32)tempValue;
                        }
                        if (currentLOPValue > ((targetValueIbias + tolerenceIbias)))
                        {
                            do
                            {
                                int tempValue = (int)((startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount)) >= lowLimitIbias ? (startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount)) : lowLimitIbias);
                                startValueIbias = (UInt32)tempValue;
                                {
                                    dut.WriteBiasDac(startValueIbias);
                                    scope.ClearDisplay();
                                    scope.SetScaleOffset();
                                    scope.DisplayPowerWatt();
                                    currentLOPValue = scope.GetAveragePowerWatt();
                                }

                                if ( (startValueIbias == lowLimitIbias) && (currentLOPValue > ((targetLOPValue + tolerenceIbias))))
                                {
                                        logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                        logger.FlushLogBuffer();
                                    ibiasDacTarget = startValueIbias;
                                    imodDacTarget = startValueMod;
                                    return false;
                                }
                            } while ((currentLOPValue > ((targetValueIbias + tolerenceIbias))) );
                          
                        }
                         if ( (currentERValue > ((targetValueIMod + tolerenceIMod))))
                        {
                            do
                            {

                                int tempValue = (int)((startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount)) >= lowLimitIMod ? (startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount)) : lowLimitIMod);
                                startValueMod = (UInt32)tempValue;
                                {
                                    dut.WriteModDac(startValueMod);
                                    scope.ClearDisplay();
                                    scope.SetScaleOffset();
                                    scope.DisplayER();
                                    Thread.Sleep(200);
                                    currentERValue = scope.GetEratio();

                                }
                                if ((startValueMod == lowLimitIMod) && (currentERValue > ((targetERValue + tolerenceIMod))))
                                {
                                     logoStr += logger.AdapterLogString(4, "DataBase input Parameters lowLimit is too large!");
                                    ibiasDacTarget = startValueIbias;
                                    imodDacTarget = startValueMod;
                                    logger.FlushLogBuffer();
                                    return false;
                                }
                            } while ( (currentERValue > ((targetValueIMod + tolerenceIMod))));

                        }
                    }
                    else
                    {
                        if ((currentLOPValue < (targetValueIbias - tolerenceIbias)))
                        {
                            int tempValue = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                            startValueIbias = (UInt32)tempValue;
                        }
                        else if ((currentLOPValue > (targetValueIbias + tolerenceIbias)))
                        {
                            int tempValue = (int)((startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop)) >= lowLimitIbias ? (startValueIbias - (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop)) : lowLimitIbias);
                            startValueIbias = (UInt32)tempValue;
                            backUpCountLop++;
                            byte tempValue1 = (byte)((backUpCountLop) >= (byte)(totalExponentiationLopCount - 3) ? (byte)(totalExponentiationLopCount - 3) : backUpCountLop);
                            backUpCountLop = tempValue1;
                            if (backUpCountLop < (byte)(totalExponentiationLopCount - 3))
                            {
                                int tempValue2 = (int)(startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop) >= uperLimitIbias ? uperLimitIbias : startValueIbias + (UInt32)Math.Pow(2, totalExponentiationLopCount - backUpCountLop));
                                startValueIbias = (UInt32)tempValue2;
                            }
                           
                        }
                        if ((currentERValue < (targetValueIMod - tolerenceIMod)))
                        {
                            int tempValue = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                            startValueMod = (UInt32)tempValue;
                           
                        }
                        else if ((currentERValue > (targetValueIMod + tolerenceIMod)))
                        {
                            int tempValue = (int)((startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr)) >= lowLimitIMod ? (startValueMod - (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr)) : lowLimitIMod);
                            startValueMod = (UInt32)tempValue;
                            backUpCountEr++;
                            byte tempValue1 = (byte)((backUpCountEr) >= (byte)(totalExponentiationERCount - 3) ? (byte)(totalExponentiationERCount - 3) : backUpCountEr);
                            backUpCountEr = tempValue1;
                            if (backUpCountEr < (byte)(totalExponentiationERCount - 3))
                            {
                                int tempValue2 = (int)(startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr) >= uperLimitIMod ? uperLimitIMod : startValueMod + (UInt32)Math.Pow(2, totalExponentiationERCount - backUpCountEr));
                                startValueMod = (UInt32)tempValue2;
                            }
                           
                        }
                    }
                    if ((currentLOPValue < (targetValueIbias - tolerenceIbias) || currentLOPValue > (targetValueIbias + tolerenceIbias)) || (currentERValue < (targetValueIMod - tolerenceIMod) || currentERValue > (targetValueIMod + tolerenceIMod)))
                    {
                        adjustCount++;
                    }
                }

            } while (adjustCount <= 100 && (currentLOPValue < (targetValueIbias - tolerenceIbias) || currentLOPValue > (targetValueIbias + tolerenceIbias) || currentERValue < (targetValueIMod - tolerenceIMod) || currentERValue > (targetValueIMod + tolerenceIMod)));
            targetLOPValue = currentLOPValue;
            targetERValue = currentERValue;
            UInt16 Temp;
            dut.ReadTxpADC(out Temp);
            TxPowerAdc = Convert.ToUInt32(Temp);
            
            if (startValueIbias > uperLimitIbias || startValueIbias < lowLimitIbias)
            {
                if (startValueIbias > uperLimitIbias)
                {
                    startValueIbias = uperLimitIbias;
                    {
                        dut.WriteBiasDac(startValueIbias);
                        scope.ClearDisplay();
                        scope.SetScaleOffset();
                        scope.DisplayPowerWatt();
                        ibiasDacTarget = startValueIbias;
                        currentLOPValue = scope.GetAveragePowerWatt();
                    }
                }
                else if (startValueIbias < lowLimitIbias)
                {
                    startValueIbias = lowLimitIbias;
                    {
                        dut.WriteBiasDac(startValueIbias);
                        scope.ClearDisplay();
                        scope.SetScaleOffset();
                        scope.DisplayPowerWatt();
                        ibiasDacTarget = startValueIbias;
                        currentLOPValue = scope.GetAveragePowerWatt();
                    }
                }
            }
            if (startValueMod > uperLimitIMod || startValueMod < lowLimitIMod)
            {
                if (startValueMod > uperLimitIMod)
                {
                    startValueMod = uperLimitIMod;

                    {
                        dut.WriteModDac(startValueMod);
                        imodDacTarget = startValueMod;
                        scope.ClearDisplay();
                        Thread.Sleep(100);
                        scope.SetScaleOffset();
                        scope.DisplayER();
                        currentERValue = scope.GetEratio();
                        targetERValue = currentERValue;
                    }
                }
                else if (startValueMod < lowLimitIMod)
                {
                    startValueMod = lowLimitIMod;

                    {
                        dut.WriteModDac(startValueMod);
                        imodDacTarget = startValueMod;
                        scope.ClearDisplay();
                        Thread.Sleep(200);
                        scope.SetScaleOffset();
                        scope.DisplayER();
                        currentERValue = scope.GetEratio();
                        targetERValue = currentERValue;
                    }
                }
            }
            if (currentLOPValue >= (targetValueIbias - tolerenceIbias) && currentLOPValue <= (targetValueIbias + tolerenceIbias))
            {
                ibiasDacTarget = startValueIbias;
                isLopok = true;
            }
            else
            {
                ibiasDacTarget = startValueIbias;
                isLopok = false;
            }
            if (currentERValue >= (targetValueIMod - tolerenceIMod) && currentERValue <= (targetValueIMod + tolerenceIMod))
            {
                imodDacTarget = startValueMod;
                isERok = true;
            }
            else
            {
                imodDacTarget = startValueMod;
                isERok = false;
            }
            if (isERok&&isLopok)
            {
                ibiasDacTarget = startValueIbias;
                imodDacTarget = startValueMod;
                dut.ReadTxpADC(out Temp);
                TxPowerAdc = Convert.ToUInt32(Temp);
                return true;
            } 
            else
            {
                
                ibiasDacTarget = startValueIbias;
                imodDacTarget = startValueMod;
                dut.ReadTxpADC(out Temp);
                TxPowerAdc = Convert.ToUInt32(Temp);
                return false;
            }
        }
        private bool writeCurrentChannelPID(DUT inputDut)
        {
            bool isWriteCoefP = false;
            bool isWriteCoefI = false;
            bool isWriteCoefD = false;
            try
            {
                isWriteCoefP=inputDut.SetcoefP(adjustEYEStruce.pidCoefArray[0].ToString());
                isWriteCoefI=inputDut.SetcoefI(adjustEYEStruce.pidCoefArray[1].ToString());
                isWriteCoefD=inputDut.SetcoefD(adjustEYEStruce.pidCoefArray[2].ToString());
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
           
            return isWriteCoefP && isWriteCoefI&&isWriteCoefD;
        }
#endregion
        
    }
}
