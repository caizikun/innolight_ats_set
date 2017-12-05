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
                string apcstring = null;
                dut.APCStatus(out  apcstring);
                if (apcstring == "OFF" || apcstring == "FF")
                {
                    logoStr += logger.AdapterLogString(0, "Step2...Start Open apc");  
                    dut.APCON();
                    logoStr += logger.AdapterLogString(0, "Power off");  
                    tempps.Switch(false);
                    Thread.Sleep(200);
                    logoStr += logger.AdapterLogString(0, "Power on"); 
                    tempps.Switch(true);
                    Thread.Sleep(200);
                    bool isOpen = dut.APCStatus(out  apcstring);
                    if (apcstring == "ON")
                    {
                        logoStr += logger.AdapterLogString(1, "APC ON"); 
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(1, "APC NOT ON"); 
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
            bool isAlarm = false;
            isAlarm = dut.ChkTempHA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkTempHA();
            return isAlarm;
        }
        protected bool CheckTempHighWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkTempHW();
            Thread.Sleep(1000);
            isWarning = dut.ChkTempHW();
            return isWarning;
        }
        public bool CheckTempLowAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkTempLA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkTempLA();
            return isAlarm;
        }
        protected bool CheckTempLowWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkTempLW();
            Thread.Sleep(1000);
            isWarning = dut.ChkTempLW();
            return isWarning;
        }
        // vcc A&W
        protected bool CheckVccLowWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkVccLW();
            Thread.Sleep(1000);
            isWarning = dut.ChkVccLW();
            return isWarning;
        }
        protected bool CheckVccLowAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkVccLA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkVccLA();
            return isAlarm;
        }
        protected bool CheckVccHighAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkVccHA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkVccHA();
            return isAlarm;
        }
        protected bool CheckVccHighWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkVccHW();
            Thread.Sleep(1000);
            isWarning = dut.ChkVccHW();
            return isWarning;
        }
        // bias A&W
        protected bool CheckBiasHighAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkBiasHA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkBiasHA();
            return isAlarm;
        }
        protected bool CheckBiasHighWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkBiasHW();
            Thread.Sleep(1000);
            isWarning = dut.ChkBiasHW();
            return isWarning;
        }
        public bool CheckBiasLowWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkBiasLW();
            Thread.Sleep(1000);
            isWarning = dut.ChkBiasLW();
            return isWarning;
        }
        protected bool CheckBiasLowAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkBiasLA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkBiasLA();
            return isAlarm;
        }
        // txpower A&W
        protected bool CheckTxPowerHighAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkTxpHA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkTxpHA();
            return isAlarm;
        }
        public bool CheckTxPowerHighWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkTxpHW();
            Thread.Sleep(1000);
            isWarning = dut.ChkTxpHW();
            return isWarning;
        }
        protected bool CheckTxPowerLowWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkTxpLW();
            Thread.Sleep(1000);
            isWarning = dut.ChkTxpLW();
            return isWarning;
        }
        public bool CheckTxPowerLowAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkTxpLA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkTxpLA();
            return isAlarm;
        }
        // rxpower A&W
        protected bool CheckRxPowerHighAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkRxpHA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkRxpHA();
            return isAlarm;
        }
        protected bool CheckRxPowerHighWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkRxpHW();
            Thread.Sleep(1000);
            isWarning = dut.ChkRxpHW();
            return isWarning;
        }
        public bool CheckRxPowerLowWarning(DUT dut)
        {
            bool isWarning = false;
            isWarning = dut.ChkRxpLW();
            Thread.Sleep(1000);
            isWarning = dut.ChkRxpLW();
            return isWarning;
        }
        protected bool CheckRxPowerLowAlarm(DUT dut)
        {
            bool isAlarm = false;
            isAlarm = dut.ChkRxpLA();
            Thread.Sleep(1000);
            isAlarm = dut.ChkRxpLA();
            return isAlarm;
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
