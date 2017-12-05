using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ATS_Framework
{
    public class TestTxEyeTDEC : TestModelBase
    {
 #region Attribute       
        private double TDEC;

        private Powersupply tempps;
        private Scope tempScope;
 #endregion

 #region Method
        public TestTxEyeTDEC(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;            
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
        override protected bool PrepareTest()
        {//note: for inherited class, they need to do its own preparation process task,
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].IncreaseReferencedTimes();
            }
            return AssembleEquipment();
        }

        protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {

            //for (int i = 0; i < selectedEquipList.Count; i++)
            //{
            //    if (!selectedEquipList.Values[i].Configure()) return false;

            //}//test

            return true;
        }

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            //// 是否要测试需要添加判定            
          
            if (PrepareEnvironment(selectedEquipList) == false)
            {
                OutPutandFlushLog();
                return false;
            }
            
            if (tempps != null)
            {
                // open apc 
               
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                }
              
                // open apc
                logoStr += logger.AdapterLogString(0, "Step3...StartTestOpticalEye TDEC");
                logoStr += logger.AdapterLogString(0, "OpticalEyeTest_TDEC");
                TDECTest();
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
        protected bool TDECTest()
        {
            TDEC = 0;

            Scope tempScope = (Scope)selectedEquipList["SCOPE"];
            if (tempScope != null)
            {
                tempScope.pglobalParameters = GlobalParameters;
                TDEC = tempScope.GetTxTDEC();
                logoStr += logger.AdapterLogString(1, "TDEC:" + TDEC.ToString());

                return true;
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                
                return false;
            }
        }     
        override protected bool PostTest()
        {//note: for inherited class, they need to call base function first,
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
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[1];
                outputParameters[0].FiledName = "TDEC(dB)";
                TDEC = algorithm.ISNaNorIfinity(TDEC);
                outputParameters[0].DefaultValue = Math.Round(TDEC, 2).ToString().Trim();

                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }        
        protected bool PrepareEnvironment(EquipmentList aEquipList)
        {
           
            if (tempScope != null)
            { 
                  if (tempScope.SetMaskAlignMethod(1) &&
                  tempScope.SetMode(0) &&
                  tempScope.MaskONOFF(false) &&
                  tempScope.SetRunTilOff() &&
                  tempScope.RunStop(true) &&
                  tempScope.OpenOpticalChannel(true)&&
                  tempScope.RunStop(true) &&
                  tempScope.ClearDisplay() &&
                  tempScope.AutoScale()
                  )
                    {
                        logoStr += logger.AdapterLogString(1, "PrepareEnvironment OK!"); 
                        return true;
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(4,  "PrepareEnvironment Fail!"); 
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
