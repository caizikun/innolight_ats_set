using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivi.Visa.Interop;
using System.Collections;

namespace ATS_Framework
{
  public  class TestTxDmiPowCurve:TestModelBase
    {
        private Powersupply tempps;

        private double txpProportionLessCoef;
        private double txpProportionGreatCoef;

        public TestTxDmiPowCurve(DUT inPuDut)
        {
            
            dut = inPuDut;
            logoStr = null;            
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
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                       // isOK = true;
                    }
                }
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempps != null)
                {
                    isOK = true;

                }
                 else
                 {
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
            
            logoStr = "";            
            if (tempps != null)
            {                
                // open apc                
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                }
                // open apc
                Log.SaveLogToTxt("Step1...Read DUT DmiTxPowCurveLT");
                txpProportionLessCoef = dut.GetTxpProportionLessCoef();
                Log.SaveLogToTxt("DmiTxPowCurveLT:" + txpProportionLessCoef.ToString());
                Log.SaveLogToTxt("Step2...Read DUT DmiTxPowCurveHT");
                txpProportionGreatCoef = dut.GetTxpProportionGreatCoef();
                Log.SaveLogToTxt("DmiTxPowCurveHT:" + txpProportionGreatCoef.ToString());
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

        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[2];
                outputParameters[0].FiledName = "DmiTxPowCurveLT";
                txpProportionLessCoef = Algorithm.ISNaNorIfinity(txpProportionLessCoef);
                outputParameters[0].DefaultValue = Math.Round(txpProportionLessCoef, 6).ToString().Trim();
                outputParameters[1].FiledName = "DmiTxPowCurveHT";
                txpProportionGreatCoef = Algorithm.ISNaNorIfinity(txpProportionGreatCoef);
                outputParameters[1].DefaultValue = Math.Round(txpProportionGreatCoef, 6).ToString().Trim();
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
                AnalysisOutputParameters(outputParameters);
                
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

    }
}
