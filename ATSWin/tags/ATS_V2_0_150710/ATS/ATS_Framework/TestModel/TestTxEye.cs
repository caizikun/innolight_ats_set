using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ATS_Framework
{
    public class TestTxEye : TestModelBase
    {
 #region Attribute       
        private double apDBM;
        private double er;
        private double crossing;
        private double marginVaulue;
        private double jitterRMS;
        private double jitterPP;
        private double OEOMA;
        private double OEOMADBM;
        private double riseTime;
        private double fallTime;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private Powersupply tempps;
        private Scope tempScope;
 #endregion

 #region Method
        public TestTxEye(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
            inPutParametersNameArray.Clear();
          
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
                logoStr += logger.AdapterLogString(0, "Step3...StartTestOptical Eye");
                logoStr += logger.AdapterLogString(0, "OpticalEyeTest");
                OpticalTest();
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
        protected bool OpticalTest()
        {
            apDBM = 0;
            er = 0;
            crossing = 0;
            marginVaulue = 0;
            jitterRMS = 0;
            jitterPP = 0;
            OEOMA = 0;
            OEOMADBM = 0;
            riseTime = 0;
            fallTime = 0;
            Scope tempScope = (Scope)selectedEquipList["SCOPE"];
            if (tempScope != null)
            {             
                tempScope.pglobalParameters = GlobalParameters;
                double[]tempDoubleArray;
                if (tempScope.OpticalEyeTest(out tempDoubleArray))
                {
                    OEOMA = tempDoubleArray[2];
                    OEOMADBM = algorithm.ChangeUwtoDbm(OEOMA * 1000);
                    jitterRMS = tempDoubleArray[5];
                    jitterPP = tempDoubleArray[4];
                    riseTime = tempDoubleArray[6];
                    fallTime = tempDoubleArray[7];
                    crossing = tempDoubleArray[3];
                    er = tempDoubleArray[1];
                    apDBM = tempDoubleArray[0];
                    marginVaulue = tempDoubleArray[8];

                } 
                else
                {
                    for (byte i = 0; i < tempDoubleArray.Length;i++)
                    {
                        tempDoubleArray[i] = 0;
                    }
                    tempScope.OpticalEyeTest(out tempDoubleArray, 1);
                    OEOMA = tempDoubleArray[2];
                    OEOMADBM = algorithm.ChangeUwtoDbm(OEOMA * 1000);
                    jitterRMS = tempDoubleArray[5];
                    jitterPP = tempDoubleArray[4];
                    riseTime = tempDoubleArray[6];
                    fallTime = tempDoubleArray[7];
                    crossing = tempDoubleArray[3];
                    er = tempDoubleArray[1];
                    apDBM = tempDoubleArray[0];
                    marginVaulue = tempDoubleArray[8];
                }
               
                
                logoStr += logger.AdapterLogString(1, "apDBM:" + apDBM.ToString());
                logoStr += logger.AdapterLogString(1,"er:" + er.ToString());
                logoStr += logger.AdapterLogString(1, "crossing:" + crossing.ToString());
                logoStr += logger.AdapterLogString(1, "maskVaulue:" + marginVaulue.ToString());
                logoStr += logger.AdapterLogString(1, "jitterRMS:" + jitterRMS.ToString());
                logoStr += logger.AdapterLogString(1,  "jitterPP:" + jitterPP.ToString());
                logoStr += logger.AdapterLogString(1, "OEOMA:" + OEOMA.ToString());
                logoStr += logger.AdapterLogString(1, "OEOMADBM:" + OEOMADBM.ToString());
                logoStr += logger.AdapterLogString(1, "riseTime:" + riseTime.ToString());
                logoStr += logger.AdapterLogString(1, "fallTime:" + fallTime.ToString());
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
                if (!selectedEquipList.Values[i].Switch(true)) return false;

            }
            return true;

        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[10];
                outputParameters[0].FiledName = "AP(DBM)";             
                apDBM = algorithm.ISNaNorIfinity(apDBM);
                outputParameters[0].DefaultValue = Math.Round(apDBM, 4).ToString().Trim();
                outputParameters[1].FiledName = "ER(DB)";               
                er = algorithm.ISNaNorIfinity(er);
                outputParameters[1].DefaultValue = Math.Round(er, 4).ToString().Trim();
                outputParameters[2].FiledName = "CROSSING(%)";              
                crossing = algorithm.ISNaNorIfinity(crossing);
                outputParameters[2].DefaultValue = Math.Round(crossing, 4).ToString().Trim();
                outputParameters[3].FiledName = "MASKMARGIN(%)";
                marginVaulue = algorithm.ISNaNorIfinity(marginVaulue);
                outputParameters[3].DefaultValue = Math.Round(marginVaulue, 4).ToString().Trim();
                outputParameters[4].FiledName = "JITTERRMS(PS)";
                jitterRMS = algorithm.ISNaNorIfinity(jitterRMS);
                outputParameters[4].DefaultValue = Math.Round(jitterRMS, 4).ToString().Trim();
                outputParameters[5].FiledName = "JITTERPP(PS)";
                jitterPP = algorithm.ISNaNorIfinity(jitterPP);
                outputParameters[5].DefaultValue = Math.Round(jitterPP, 4).ToString().Trim();
                outputParameters[6].FiledName = "RISETIME(PS)";
                riseTime = algorithm.ISNaNorIfinity(riseTime);
                outputParameters[6].DefaultValue = Math.Round(riseTime, 4).ToString().Trim();
                outputParameters[7].FiledName = "FALLTIME(PS)";
                fallTime = algorithm.ISNaNorIfinity(fallTime);
                outputParameters[7].DefaultValue = Math.Round(fallTime, 4).ToString().Trim();
                outputParameters[8].FiledName = "TXOMA(MW)";
                OEOMA = algorithm.ISNaNorIfinity(OEOMA);
                outputParameters[8].DefaultValue = Math.Round(OEOMA, 4).ToString().Trim();
                outputParameters[9].FiledName = "TXOMA(DBM)";
                OEOMADBM = algorithm.ISNaNorIfinity(OEOMADBM);
                outputParameters[9].DefaultValue = Math.Round(OEOMADBM, 4).ToString().Trim();
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
