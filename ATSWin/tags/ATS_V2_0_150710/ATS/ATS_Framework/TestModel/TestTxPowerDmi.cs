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
    public class TestTxPowerDmi : TestModelBase
    {
 #region Attribute
        private double txDmiPowerErr;
        private double txPowerDmi;
        private double txDCAPowerDmi;
        private ArrayList inPutParametersNameArray = new ArrayList(); 
        //equipments
       private Powersupply tempps;
       private Scope tempScope;
          
#endregion
#region Method
        public TestTxPowerDmi(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inPuDut;
            logoStr = null;            
            inPutParametersNameArray.Clear();
            
        }
        protected override bool CheckEquipmentReadiness()
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
        protected override  bool PrepareTest()
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
                        isOK = true;
                    }
                }
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 tempScope = (Scope)selectedEquipList["SCOPE"];
                 if (tempps != null && tempScope != null)
                {
                    isOK = true;

                }
                 else
                 {

                     if (tempScope == null)
                     {
                         logoStr += logger.AdapterLogString(3, "SCOPE =NULL");
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
                    
                }

                return isOK;
            }
        }

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";            
            if (PrepareEnvironment(selectedEquipList)==false)
           {
               OutPutandFlushLog();
               return false;
           }
            
            if (tempps != null && tempScope != null)
            {                
                // open apc                
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                }
                
                // open apc
                logoStr += logger.AdapterLogString(0, "Step3...Read DCA TxPower");
                txDCAPowerDmi = tempScope.GetAveragePowerdbm();
                logoStr += logger.AdapterLogString(1, "txDCAPowerDmi:" + txDCAPowerDmi.ToString());
                logoStr += logger.AdapterLogString(0, "Step4...Read DUT Txpower");
                txPowerDmi = dut.ReadDmiTxp();
                logoStr += logger.AdapterLogString(1, "txPowerDmi:" + txPowerDmi.ToString());
                txDmiPowerErr = txPowerDmi - txDCAPowerDmi;
                logoStr += logger.AdapterLogString(1, "txDmiPowerErr:" + txDmiPowerErr.ToString());
                OutPutandFlushLog();
                return true;
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                OutPutandFlushLog();
                return false;
            }
           
        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                outputParameters = new TestModeEquipmentParameters[3];

                outputParameters[0].FiledName = "DMITXPOWER(DBM)";
                txPowerDmi = algorithm.ISNaNorIfinity(txPowerDmi);
                outputParameters[0].DefaultValue = Math.Round(txPowerDmi, 4).ToString().Trim();
                outputParameters[1].FiledName = "DCATXPOWER(DBM)";
                txDCAPowerDmi = algorithm.ISNaNorIfinity(txDCAPowerDmi);
                outputParameters[1].DefaultValue = Math.Round(txDCAPowerDmi, 4).ToString().Trim();
                outputParameters[2].FiledName = "DMITXPOWERERR(DB)";
                txDmiPowerErr = algorithm.ISNaNorIfinity(txDmiPowerErr);
                outputParameters[2].DefaultValue = Math.Round(txDmiPowerErr, 4).ToString().Trim();
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
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
                    tempScope.AutoScale(1)
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
        
        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
#endregion
    }
}
