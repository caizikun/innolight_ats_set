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
        public TestTxEyeTDEC(DUT inPuDut)
        {
            
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
                Log.SaveLogToTxt("Step3...StartTestOpticalEye TDEC");
                Log.SaveLogToTxt("OpticalEyeTest_TDEC");
                TDECTest();
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
        protected bool TDECTest()
        {
            TDEC = 0;

            Scope tempScope = (Scope)selectedEquipList["SCOPE"];
            if (tempScope != null)
            {
                tempScope.pglobalParameters = GlobalParameters;
                TDEC = tempScope.GetTxTDEC();
                Log.SaveLogToTxt("TDEC:" + TDEC.ToString());

                return true;
            }
            else
            {
                Log.SaveLogToTxt("Equipments are not enough!");
                
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
                TDEC = Algorithm.ISNaNorIfinity(TDEC);
                outputParameters[0].DefaultValue = Math.Round(TDEC, 2).ToString().Trim();

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
                        Log.SaveLogToTxt("PrepareEnvironment OK!"); 
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt( "PrepareEnvironment Fail!"); 
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
