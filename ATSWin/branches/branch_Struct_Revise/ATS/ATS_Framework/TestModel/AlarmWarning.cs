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
   public class AlarmWarning:TestModelBase
    {
#region Attribute       
        
        private bool tempHA;
        private bool tempHW;
        private bool tempLA;
        private bool tempLW;

        private bool vccHA;
        private bool vccHW;
        private bool vccLA;
        private bool vccLW;

        private bool ibiasHA;
        private bool ibiasHW;
        private bool ibiasLA;
        private bool ibiasLW;

        private bool txPowerHA;
        private bool txPowerHW;
        private bool txPowerLA;
        private bool txPowerLW;

        private bool rxPowerHA;
        private bool rxPowerHW;
        private bool rxPowerLA;
        private bool rxPowerLW;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private TestAlarmWarningStruct TestAlarmWarningStruct = new TestAlarmWarningStruct();  
       //equipments
        private Powersupply tempps;
        private Attennuator tempAtten;
#endregion

#region Method
        public AlarmWarning(DUT inPuDut)
        {
            
            logoStr = null;
            dut = inPuDut;
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("RXPOWERPOINT(DBM)");
        }
        override protected bool PrepareTest()
        {
            //note: for inherited class, they need to do its own preparation process task,
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

                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        isOK = true;
                    }
                }
                tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                if (tempps != null && tempAtten != null)
                {
                    isOK = true;

                }
                else
                {
                    if (tempAtten == null)
                    {
                        Log.SaveLogToTxt("ATTEN =NULL");
                    }
                    if (tempps == null)
                    {
                        Log.SaveLogToTxt("POWERSUPPLY =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
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
        override protected bool CheckEquipmentReadiness()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].bReady) return false;

            }

            return true;
        }


        protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].Configure()) return false;
            }

            return true;
        }

        override protected bool PostTest()
        {
            //note: for inherited class, they need to call base function first,
            //then do other post-test process task
            bool flag = DeassembleEquipment();

            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].DecreaseReferencedTimes();

            }


            return flag;
        }
        protected override bool AssembleEquipment()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
            }
            return true;
        }
        protected override bool StartTest()
        {
            
            logoStr = "";
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }
           
            if (tempps != null && tempAtten != null)
            {                
                // open apc 
              
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                }
                
                // open apc
              
                Log.SaveLogToTxt("Step3...CheckTempHighAlarm");
                Log.SaveLogToTxt( CheckTempHighAlarm(dut).ToString());

                Log.SaveLogToTxt( "Step4...CheckTempHighWarning");
                Log.SaveLogToTxt(CheckTempHighWarning(dut).ToString());

                Log.SaveLogToTxt("Step5...CheckTempLowAlarm");
                Log.SaveLogToTxt(CheckTempLowAlarm(dut).ToString());

                Log.SaveLogToTxt( "Step6...CheckTempLowWarning");
                Log.SaveLogToTxt(CheckTempLowWarning(dut).ToString());
               
               
               
                //vcc
                Log.SaveLogToTxt("Step7...CheckVccLowWarning");
                Log.SaveLogToTxt( CheckVccLowWarning(dut).ToString());

                Log.SaveLogToTxt("Step8...CheckVccLowAlarm");
                Log.SaveLogToTxt( CheckVccLowAlarm(dut).ToString());

                Log.SaveLogToTxt("Step9...CheckVccHighAlarm");
                Log.SaveLogToTxt(CheckVccHighAlarm(dut).ToString());


                Log.SaveLogToTxt( "Step10...CheckVccHighWarning");
                Log.SaveLogToTxt(CheckVccHighWarning(dut).ToString());
              
              
               
                //ibias
                Log.SaveLogToTxt("Step11...CheckBiasHighAlarm");
                Log.SaveLogToTxt(CheckBiasHighAlarm(dut).ToString());

                Log.SaveLogToTxt("Step12...CheckBiasHighWarning");
                Log.SaveLogToTxt(CheckBiasHighWarning(dut).ToString());

                Log.SaveLogToTxt( "Step13...CheckBiasLowWarning");
                Log.SaveLogToTxt(CheckBiasLowWarning(dut).ToString());

                Log.SaveLogToTxt( "Step14...CheckBiasLowAlarm");
                Log.SaveLogToTxt(CheckBiasLowAlarm(dut).ToString());
               
                // tx power
                Log.SaveLogToTxt("Step15...CheckTxPowerHighAlarm");
                Log.SaveLogToTxt(CheckTxPowerHighAlarm(dut).ToString());

                Log.SaveLogToTxt( "Step16...CheckTxPowerHighWarning");
                Log.SaveLogToTxt(CheckTxPowerHighWarning(dut).ToString());

                Log.SaveLogToTxt("Step17...CheckTxPowerLowWarning");
                Log.SaveLogToTxt(CheckTxPowerLowWarning(dut).ToString());

                Log.SaveLogToTxt( "Step18...CheckTxPowerLowAlarm");
                Log.SaveLogToTxt(CheckTxPowerLowAlarm(dut).ToString());
               
               
              
                // rx power
                tempAtten.AttnValue(Convert.ToString(TestAlarmWarningStruct.RxPowerAWPoint),1);
                Log.SaveLogToTxt("Step19...CheckRxPowerHighAlarm");
                Log.SaveLogToTxt(CheckRxPowerHighAlarm(dut).ToString());

                Log.SaveLogToTxt("Step20...CheckRxPowerHighWarning");
                Log.SaveLogToTxt(CheckRxPowerHighWarning(dut).ToString());

                Log.SaveLogToTxt("Step21...CheckRxPowerLowWarning");
                Log.SaveLogToTxt(CheckRxPowerLowWarning(dut).ToString());

                Log.SaveLogToTxt("Step22...CheckRxPowerLowAlarm");
                Log.SaveLogToTxt( CheckRxPowerLowAlarm(dut).ToString());

                OutPutandFlushLog();
                return true;
            }

            else
            {
               
                Log.SaveLogToTxt("Equipments are not enough!");
                OutPutandFlushLog();
                return false;
            }
        }
        // temp A&W     
        public bool CheckTempHighAlarm(DUT dut)
        { 
            tempHA = dut.ChkTempHA();
            Thread.Sleep(50);
            tempHA = dut.ChkTempHA();
            return !tempHA;
        }
        protected bool CheckTempHighWarning(DUT dut)
        {
            tempHW = dut.ChkTempHW();
            Thread.Sleep(50);
            tempHW = dut.ChkTempHW();
            return !tempHW;
        }
        public bool CheckTempLowAlarm(DUT dut)
        {
            tempLA = dut.ChkTempLA();
            Thread.Sleep(50);
            tempLA = dut.ChkTempLA();
            return !tempLA;
        }
        protected bool CheckTempLowWarning(DUT dut)
        {
            tempLW = dut.ChkTempLW();
            Thread.Sleep(50);
            tempLW = dut.ChkTempLW();
            return !tempLW;
        }
        // vcc A&W
        
        protected bool CheckVccLowWarning(DUT dut)
        {
            vccLW = dut.ChkVccLW();
            Thread.Sleep(50);
            vccLW = dut.ChkVccLW();
            return !vccLW;
        }
        protected bool CheckVccLowAlarm(DUT dut)
        {
            vccLA = dut.ChkVccLA();
            Thread.Sleep(50);
            vccLA = dut.ChkVccLA();
            return !vccLA;
        }
        protected bool CheckVccHighAlarm(DUT dut)
        {
            vccHA = dut.ChkVccHA();
            Thread.Sleep(50);
            vccHA = dut.ChkVccHA();
            return !vccHA;
        }
        protected bool CheckVccHighWarning(DUT dut)
        {
            vccHW = dut.ChkVccHW();
            Thread.Sleep(50);
            vccHW = dut.ChkVccHW();
            return !vccHW;
        }
        // bias A&W       
        protected bool CheckBiasHighAlarm(DUT dut)
        {
            ibiasHA = dut.ChkBiasHA();
            Thread.Sleep(50);
            ibiasHA = dut.ChkBiasHA();
            return !ibiasHA;
        }
        protected bool CheckBiasHighWarning(DUT dut)
        {
            ibiasHW = dut.ChkBiasHW();
            Thread.Sleep(50);
            ibiasHW = dut.ChkBiasHW();
            return !ibiasHW;
        }
        public bool CheckBiasLowWarning(DUT dut)
        {
            ibiasLW = dut.ChkBiasLW();
            Thread.Sleep(50);
            ibiasLW = dut.ChkBiasLW();
            return !ibiasLW;
        }
        protected bool CheckBiasLowAlarm(DUT dut)
        {
            ibiasLA = dut.ChkBiasLA();
            Thread.Sleep(50);
            ibiasLA = dut.ChkBiasLA();
            return !ibiasLA;
        }
        // txpower A&W       
        protected bool CheckTxPowerHighAlarm(DUT dut)
        {
            txPowerHA = dut.ChkTxpHA();
            Thread.Sleep(50);
            txPowerHA = dut.ChkTxpHA();
            return !txPowerHA;
        }
        public bool CheckTxPowerHighWarning(DUT dut)
        {
            
            txPowerHW = dut.ChkTxpHW();
            Thread.Sleep(50);
            txPowerHW = dut.ChkTxpHW();
            return !txPowerHW;
        }
        protected bool CheckTxPowerLowWarning(DUT dut)
        {
            txPowerLW = dut.ChkTxpLW();
            Thread.Sleep(50);
            txPowerLW = dut.ChkTxpLW();
            return !txPowerLW;
        }
        public bool CheckTxPowerLowAlarm(DUT dut)
        {
            txPowerLA = dut.ChkTxpLA();
            Thread.Sleep(50);
            txPowerLA = dut.ChkTxpLA();
            return !txPowerLA;
        }
        // rxpower A&W       
        protected bool CheckRxPowerHighAlarm(DUT dut)
        {
            rxPowerHA = dut.ChkRxpHA();
            Thread.Sleep(50);
            rxPowerHA = dut.ChkRxpHA();
            return !rxPowerHA;
        }
        protected bool CheckRxPowerHighWarning(DUT dut)
        {
            rxPowerHW = dut.ChkRxpHW();
            Thread.Sleep(50);
            rxPowerHW = dut.ChkRxpHW();
            return !rxPowerHW;
        }
        public bool CheckRxPowerLowWarning(DUT dut)
        {
            rxPowerLW = dut.ChkRxpLW();
            Thread.Sleep(50);
            rxPowerLW = dut.ChkRxpLW();
            return !rxPowerLW;
        }
        protected bool CheckRxPowerLowAlarm(DUT dut)
        {
            rxPowerLA = dut.ChkRxpLA();
            Thread.Sleep(50);
            rxPowerLA = dut.ChkRxpLA();
            return !rxPowerLA;
        }
        
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[20];
                outputParameters[0].FiledName = "TEMPHA";
                outputParameters[0].DefaultValue = Convert.ToString(Convert.ToDouble(!tempHA)).ToString().Trim();
                outputParameters[1].FiledName = "TEMPHW";
                outputParameters[1].DefaultValue = Convert.ToString(Convert.ToDouble(!tempHW)).ToString().Trim();
                outputParameters[2].FiledName = "TEMPLA";
                outputParameters[2].DefaultValue = Convert.ToString(Convert.ToDouble(!tempLA)).ToString().Trim();
                outputParameters[3].FiledName = "TEMPLW";
                outputParameters[3].DefaultValue = Convert.ToString(Convert.ToDouble(!tempLW)).ToString().Trim();
                outputParameters[4].FiledName = "VCCHA";
                outputParameters[4].DefaultValue = Convert.ToString(Convert.ToDouble(!vccHA)).ToString().Trim();
                outputParameters[5].FiledName = "VCCHW";
                outputParameters[5].DefaultValue = Convert.ToString(Convert.ToDouble(!vccHW)).ToString().Trim();
                outputParameters[6].FiledName = "VCCLA";
                outputParameters[6].DefaultValue = Convert.ToString(Convert.ToDouble(!vccLA)).ToString().Trim();
                outputParameters[7].FiledName = "VCCLW";
                outputParameters[7].DefaultValue = Convert.ToString(Convert.ToDouble(!vccLW)).ToString().Trim();
                outputParameters[8].FiledName = "IBIASHA";
                outputParameters[8].DefaultValue = Convert.ToString(Convert.ToDouble(!ibiasHA)).ToString().Trim();
                outputParameters[9].FiledName = "IBIASHW";
                outputParameters[9].DefaultValue = Convert.ToString(Convert.ToDouble(!ibiasHW)).ToString().Trim();
                outputParameters[10].FiledName = "IBIASLA";
                outputParameters[10].DefaultValue = Convert.ToString(Convert.ToDouble(!ibiasLA)).ToString().Trim();
                outputParameters[11].FiledName = "IBIASLW";
                outputParameters[11].DefaultValue = Convert.ToString(Convert.ToDouble(!ibiasLW)).ToString().Trim();
                outputParameters[12].FiledName = "TXPOWERHA";
                outputParameters[12].DefaultValue = Convert.ToString(Convert.ToDouble(!txPowerHA)).ToString().Trim();
                outputParameters[13].FiledName = "TXPOWERHW";
                outputParameters[13].DefaultValue = Convert.ToString(Convert.ToDouble(!txPowerHW)).ToString().Trim();
                outputParameters[14].FiledName = "TXPOWERLA";
                outputParameters[14].DefaultValue = Convert.ToString(Convert.ToDouble(!txPowerLA)).ToString().Trim();
                outputParameters[15].FiledName = "TXPOWERLW";
                outputParameters[15].DefaultValue = Convert.ToString(Convert.ToDouble(!txPowerLW)).ToString().Trim();
                outputParameters[16].FiledName = "RXPOWERHA";
                outputParameters[16].DefaultValue = Convert.ToString(Convert.ToDouble(!rxPowerHA)).ToString().Trim();
                outputParameters[17].FiledName = "RXPOWERHW";
                outputParameters[17].DefaultValue = Convert.ToString(Convert.ToDouble(!rxPowerHW)).ToString().Trim();
                outputParameters[18].FiledName = "RXPOWERLA";
                outputParameters[18].DefaultValue = Convert.ToString(Convert.ToDouble(!rxPowerLA)).ToString().Trim();
                outputParameters[19].FiledName = "RXPOWERLW";
                outputParameters[19].DefaultValue = Convert.ToString(Convert.ToDouble(!rxPowerLW)).ToString().Trim();
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
                InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F04, error.StackTrace);
                //Log.SaveLogToTxt(ex.ID + ": " + ex.Message + "\r\n" + ex.StackTrace);
                exceptionList.Add(ex);
                return false;
                //the other way is: should throw exception, rather than the above three code. see below:
                //throw new InnoExCeption(ExceptionDictionary.Code._0x02F04, error.StackTrace); 
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

                        if (Algorithm.FindFileName(InformationList, "RXPOWERPOINT(DBM)", out index))
                        {
                            double temp= Convert.ToDouble(InformationList[index].DefaultValue);
                            if (temp>0)
                            {
                                temp=-temp;
                            }
                            TestAlarmWarningStruct.RxPowerAWPoint = temp;
                        }   
                       
                    }

                }
                Log.SaveLogToTxt("OK!");
                return true;
            }
        }
       
        
        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
                
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
