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
     
#endregion

#region Method
        public AlarmWarning(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
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
                    
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }

                }


                if (selectedEquipList["POWERSUPPLY"] != null)
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
                if (!selectedEquipList.Values[i].Switch(true)) return false;
            }
            return true;
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

            if (selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                // open apc 
                byte apcStatus = 0;
                dut.APCStatus(out  apcStatus);
                if (GlobalParameters.ApcStyle == 0)
                {
                    if (apcStatus == 0x00)
                    {
                        logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                        dut.APCON(0x11);
                        logoStr += logger.AdapterLogString(0, "Power off");
                        tempps.Switch(false,1);                       
                        logoStr += logger.AdapterLogString(0, "Power on");
                        tempps.Switch(true,1);                        
                        bool isclosed = dut.APCStatus(out  apcStatus);
                        if (apcStatus == 0x11)
                        {
                            logoStr += logger.AdapterLogString(1, "APC ON");

                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "APC NOT ON");

                        }
                    }
                }
                else if (GlobalParameters.ApcStyle == 1)
                {
                    if (apcStatus != 0x11)
                    {
                        logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                        dut.APCON(0x11);
                        logoStr += logger.AdapterLogString(0, "Power off");
                        tempps.Switch(false,1);                       
                        logoStr += logger.AdapterLogString(0, "Power on");
                        tempps.Switch(true,1);                       
                        bool isclosed = dut.APCStatus(out  apcStatus);
                        if (apcStatus == 0x11)
                        {
                            logoStr += logger.AdapterLogString(1, "APC ON");

                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "APC ON");

                        }
                    }

                }
                // open apc
                //temp
                logoStr += logger.AdapterLogString(0, "Step3...CheckTempHighAlarm");
                logoStr += logger.AdapterLogString(1,  CheckTempHighAlarm(dut).ToString());

                logoStr += logger.AdapterLogString(0,  "Step4...CheckTempHighWarning");
                logoStr += logger.AdapterLogString(1, CheckTempHighWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0, "Step5...CheckTempLowAlarm");
                logoStr += logger.AdapterLogString(1,CheckTempLowAlarm(dut).ToString());

                logoStr += logger.AdapterLogString(0,  "Step6...CheckTempLowWarning");
                logoStr += logger.AdapterLogString(1, CheckTempLowWarning(dut).ToString());
               
               
               
                //vcc
                logoStr += logger.AdapterLogString(0, "Step7...CheckVccLowWarning");
                logoStr += logger.AdapterLogString(1,  CheckVccLowWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0, "Step8...CheckVccLowAlarm");
                logoStr += logger.AdapterLogString(1,  CheckVccLowAlarm(dut).ToString());

                logoStr += logger.AdapterLogString(0, "Step9...CheckVccHighAlarm");
                logoStr += logger.AdapterLogString(1, CheckVccHighAlarm(dut).ToString());


                logoStr += logger.AdapterLogString(0,  "Step10...CheckVccHighWarning");
                logoStr += logger.AdapterLogString(1, CheckVccHighWarning(dut).ToString());
              
              
               
                //ibias
                logoStr += logger.AdapterLogString(0, "Step11...CheckBiasHighAlarm");
                logoStr += logger.AdapterLogString(1, CheckBiasHighAlarm(dut).ToString());

                logoStr += logger.AdapterLogString(0,"Step12...CheckBiasHighWarning");
                logoStr += logger.AdapterLogString(1, CheckBiasHighWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0,  "Step13...CheckBiasLowWarning");
                logoStr += logger.AdapterLogString(1, CheckBiasLowWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0,  "Step14...CheckBiasLowAlarm");
                logoStr += logger.AdapterLogString(1, CheckBiasLowAlarm(dut).ToString());
               
                // tx power
                logoStr += logger.AdapterLogString(0, "Step15...CheckTxPowerHighAlarm");
                logoStr += logger.AdapterLogString(1, CheckTxPowerHighAlarm(dut).ToString());

                logoStr += logger.AdapterLogString(0,  "Step16...CheckTxPowerHighWarning");
                logoStr += logger.AdapterLogString(1,CheckTxPowerHighWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0, "Step17...CheckTxPowerLowWarning");
                logoStr += logger.AdapterLogString(1, CheckTxPowerLowWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0,  "Step18...CheckTxPowerLowAlarm");
                logoStr += logger.AdapterLogString(1, CheckTxPowerLowAlarm(dut).ToString());
               
               
              
                // rx power
                logoStr += logger.AdapterLogString(0, "Step19...CheckRxPowerHighAlarm");
                logoStr += logger.AdapterLogString(1, CheckRxPowerHighAlarm(dut).ToString());

                logoStr += logger.AdapterLogString(0, "Step20...CheckRxPowerHighWarning");
                logoStr += logger.AdapterLogString(1, CheckRxPowerHighWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0, "Step21...CheckRxPowerLowWarning");
                logoStr += logger.AdapterLogString(1, CheckRxPowerLowWarning(dut).ToString());

                logoStr += logger.AdapterLogString(0, "Step22...CheckRxPowerLowAlarm");
                logoStr += logger.AdapterLogString(1,  CheckRxPowerLowAlarm(dut).ToString());

              
               
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();
                return true;
            }

            else
            {
               
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                logger.FlushLogBuffer();
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
            if (InformationList.Length == 0)//InformationList is null
            {
               
                return false;
            }
            else//  InformationList is not null
            {
                int index = -1;
                for (byte i = 0; i < InformationList.Length; i++)
                {
                    if (algorithm.FindFileName(InformationList, "TEMPHA", out index))
                    {
                        InformationList[index].DefaultValue = tempHA.ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "TEMPHW", out index))
                    {
                        InformationList[index].DefaultValue = tempHW.ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "TEMPLA", out index))
                    {
                        InformationList[index].DefaultValue = tempLA.ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "TEMPLW", out index))
                    {
                        InformationList[index].DefaultValue = tempLW.ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "VCCHA", out index))
                    {
                        InformationList[index].DefaultValue = vccHA.ToString().Trim();
                        
                    }                    
                    if (algorithm.FindFileName(InformationList, "VCCHW", out index))
                    {
                        InformationList[index].DefaultValue = vccHW.ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "VCCLA", out index))
                    {
                        InformationList[index].DefaultValue = vccLA.ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "VCCLW", out index))
                    {
                        InformationList[index].DefaultValue = vccLW.ToString().Trim();
                       
                    }
                }
               
                return true;
            }
           

        }
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            logoStr += logger.AdapterLogString(0,  "Step1...Check InputParameters");
            logoStr += logger.AdapterLogString(1, "OK!");
            return true;
        }
#endregion
    }
}
