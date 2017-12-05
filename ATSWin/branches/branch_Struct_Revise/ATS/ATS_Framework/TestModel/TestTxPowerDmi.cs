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
        private double TxDisablePower=0;
        private ArrayList inPutParametersNameArray = new ArrayList(); 
        //equipments
       private Powersupply tempps;
       private Scope tempScope;
          
#endregion
#region Method
        public TestTxPowerDmi(DUT inPuDut)
        {
            
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

            lock (tempScope)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    if (!selectedEquipList.Values[i].bReady) return false;

                }

                return true;
            }
        }
        protected override  bool PrepareTest()
        {//note: for inherited class, they need to do its own preparation process task,
            //then call this base function
            //for (int i = 0; i < pEquipList.Count; i++)
            ////pEquipList.Values[i].IncreaseReferencedTimes();
            //{
            lock (tempScope)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    selectedEquipList.Values[i].IncreaseReferencedTimes();

                }


                return AssembleEquipment();
            }
        }
        protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {
            lock (tempScope)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    if (!selectedEquipList.Values[i].Configure()) return false;

                }

                return true;
            }
        }
        protected override bool AssembleEquipment()
        {
            lock (tempScope)
            {
                for (int i = 0; i < selectedEquipList.Count; i++)
                {
                    if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
                }
                return true;
            }
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
                       // isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                       // isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
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
                    
                }

                return isOK;
            }
        }

        protected override bool StartTest()
        {
            lock (tempScope)
            {
                logoStr = "";
                if (PrepareEnvironment(selectedEquipList) == false)
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
                    Log.SaveLogToTxt("Step3...Read DCA TxPower");
                    txDCAPowerDmi = tempScope.GetAveragePowerdbm();
                    Log.SaveLogToTxt("txDCAPowerDmi:" + txDCAPowerDmi.ToString());
                    Log.SaveLogToTxt("Step4...Read DUT Txpower");
                    txPowerDmi = dut.ReadDmiTxp();
                    Log.SaveLogToTxt("txPowerDmi:" + txPowerDmi.ToString());
                    txDmiPowerErr = txPowerDmi - txDCAPowerDmi;
                    Log.SaveLogToTxt("txDmiPowerErr:" + txDmiPowerErr.ToString());
                    ReadTxDisablePower();

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
        }
        private bool ReadTxDisablePower()
        {
            lock (tempScope)
            {
                lock (dut)
                {
                    dut.SetSoftTxDis();
                    tempScope.ClearDisplay();
                    TxDisablePower = tempScope.GetAveragePowerdbm();
                    Thread.Sleep(200);
                    if (!dut.TxAllChannelEnable())
                    {
                        tempScope.ClearDisplay();
                        Thread.Sleep(200);
                        return false;
                    }
                }
                tempScope.ClearDisplay();
                Thread.Sleep(200);
                return true;
            }
        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            lock (tempScope)
            {
                try
                {
                    outputParameters = new TestModeEquipmentParameters[4];

                    outputParameters[0].FiledName = "DMITXPOWER(DBM)";
                    txPowerDmi = Algorithm.ISNaNorIfinity(txPowerDmi);
                    outputParameters[0].DefaultValue = Math.Round(txPowerDmi, 4).ToString().Trim();
                    outputParameters[1].FiledName = "DCATXPOWER(DBM)";
                    txDCAPowerDmi = Algorithm.ISNaNorIfinity(txDCAPowerDmi);
                    outputParameters[1].DefaultValue = Math.Round(txDCAPowerDmi, 4).ToString().Trim();
                    outputParameters[2].FiledName = "DMITXPOWERERR(Dbm)";
                    txDmiPowerErr = Algorithm.ISNaNorIfinity(txDmiPowerErr);
                    outputParameters[2].DefaultValue = Math.Round(txDmiPowerErr, 4).ToString().Trim();

                    //---------------
                    outputParameters[3].FiledName = "TxDisablePower(Dbm)";
                    TxDisablePower = Algorithm.ISNaNorIfinity(TxDisablePower);
                    outputParameters[3].DefaultValue = Math.Round(TxDisablePower, 4).ToString().Trim();
                    // TxDisablePower
                    //--------------

                    
                    for (int i = 0; i < outputParameters.Length; i++)
                    {
                        Log.SaveLogToTxt(outputParameters[i].FiledName + " : " + outputParameters[i].DefaultValue);
                    }
                    

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
        }
       
        protected bool PrepareEnvironment(EquipmentList aEquipList)
        {
            lock (tempScope)
            {
                if (selectedEquipList["SCOPE"] != null)
                {
                    //Scope tempScope = (Scope)selectedEquipList["SCOPE"];
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
        }
        
        private void OutPutandFlushLog()
        {
            lock (tempScope)
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
        }

        public override List<InnoExCeption> GetException()
        {
            lock (tempScope)
            {
                return base.GetException();
            }
        }
#endregion
    }
}
