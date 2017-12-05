using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ATS_Framework
{
    public class TestRxEye : TestModelBase
    {
 #region Attribute      
        
        private double apDBM;
        private double er;
        private double crossing;
        private double marginVaulue;
        private double jitterRMS;
        private double jitterPP;
        private double amp;
        private double riseTime;
        private double fallTime;
        private double eyeHight;
        private double eyeWidth;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private TestEleEyeStruct TestEleEyeStruct = new TestEleEyeStruct();
        private Powersupply tempps;
        private Attennuator tempAtten;
        private Scope tempScope;
 #endregion

 #region Method
        public TestRxEye(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("INPUTRXPWR(DBM)");            

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
                   
                    if (tempKeys[i].ToUpper().Contains("SCOPE"))
                    {
                        selectedEquipList.Add("SCOPE", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);                        
                    }
                }
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                 tempScope = (Scope)selectedEquipList["SCOPE"];
                 if (tempps != null && tempScope != null)
                {
                    if (tempAtten == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
                    }
                    isOK = true;

                }
                else
                {
                    if (tempScope == null)
                    {
                        logoStr += logger.AdapterLogString(3, "SCOPE =NULL");
                    }
                    if (tempAtten == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
                    }
                    if (tempps == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    
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
            
            if (AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }
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
                if (tempAtten != null)
                {
                    logoStr += logger.AdapterLogString(0, "Step3...Set AttValue" + Convert.ToString(TestEleEyeStruct.CensePoint) + "DBM");
                    tempAtten.AttnValue(Convert.ToString(TestEleEyeStruct.CensePoint), 1);
                }                
                logoStr += logger.AdapterLogString(0, "Step4...StartTest Electricl Eye"); 
               
                    logoStr += logger.AdapterLogString(0, "ElecEyeTest");
                    ElecTest();

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
        protected bool ElecTest()
        {
            apDBM = 0;
            er = 0;
            crossing = 0;
            marginVaulue = 0;
            jitterRMS = 0;
            jitterPP = 0;
            amp = 0;
            riseTime = 0;
            fallTime = 0;
            eyeHight = 0;
            eyeWidth = 0;
            
            {
                
                tempScope.pglobalParameters = GlobalParameters;
                double[] tempDoubleArray;
                if (tempScope.ElecEyeTest(out tempDoubleArray))
                {
                    amp = tempDoubleArray[0];
                    jitterRMS = tempDoubleArray[3];
                    jitterPP = tempDoubleArray[2];
                    riseTime = tempDoubleArray[4];
                    fallTime = tempDoubleArray[5];
                    crossing = tempDoubleArray[1];
                    marginVaulue = tempDoubleArray[6];
                    eyeHight = tempDoubleArray[7];
                    eyeWidth = tempDoubleArray[8];
                } 
                else
                {
                    for (byte i = 0; i < tempDoubleArray.Length;i++ )
                    {
                        tempDoubleArray[i] = 0;
                    }
                    tempScope.ElecEyeTest(out tempDoubleArray, 1);
                    amp = tempDoubleArray[0];
                    jitterRMS = tempDoubleArray[3];
                    jitterPP = tempDoubleArray[2];
                    riseTime = tempDoubleArray[4];
                    fallTime = tempDoubleArray[5];
                    crossing = tempDoubleArray[1];
                    marginVaulue = tempDoubleArray[6];
                    eyeHight = tempDoubleArray[7];
                    eyeWidth = tempDoubleArray[8];
                }
                logoStr += logger.AdapterLogString(0,  "crossing:" + crossing.ToString());
                logoStr += logger.AdapterLogString(0, "jitterRMS:" + jitterRMS.ToString());
                logoStr += logger.AdapterLogString(0, "jitterPP:" + jitterPP.ToString());
                logoStr += logger.AdapterLogString(0, "amp:" + amp.ToString());
               logoStr += logger.AdapterLogString(0, "riseTime:" + riseTime.ToString());
               logoStr += logger.AdapterLogString(0, "fallTime:" + fallTime.ToString());
               logoStr += logger.AdapterLogString(1, "maskVaulue:" + marginVaulue.ToString());
               logoStr += logger.AdapterLogString(1, "eyeHight:" + eyeHight.ToString());
               logoStr += logger.AdapterLogString(1, "eyeWidth:" + eyeWidth.ToString());
                return true;
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
                outputParameters = new TestModeEquipmentParameters[9];
                outputParameters[0].FiledName = "EETXAMP(MV)";
                amp = algorithm.ISNaNorIfinity(amp);
                outputParameters[0].DefaultValue = Math.Round(amp, 4).ToString().Trim();
                outputParameters[1].FiledName = "EEEYEHIGHT(MV)";
                eyeHight = algorithm.ISNaNorIfinity(eyeHight);
                outputParameters[1].DefaultValue = Math.Round(eyeHight, 4).ToString().Trim();
                outputParameters[2].FiledName = "EECROSSING(%)";
                crossing = algorithm.ISNaNorIfinity(crossing);
                outputParameters[2].DefaultValue = Math.Round(crossing, 4).ToString().Trim();
                outputParameters[3].FiledName = "EEMASKMARGIN(%)";
                marginVaulue = algorithm.ISNaNorIfinity(marginVaulue);
                outputParameters[3].DefaultValue = Math.Round(marginVaulue, 4).ToString().Trim();
                outputParameters[4].FiledName = "EEJITTERRMS(PS)";
                jitterRMS = algorithm.ISNaNorIfinity(jitterRMS);
                outputParameters[4].DefaultValue = Math.Round(jitterRMS, 4).ToString().Trim();
                outputParameters[5].FiledName = "EEJITTERPP(PS)";
                jitterPP = algorithm.ISNaNorIfinity(jitterPP);
                outputParameters[5].DefaultValue = Math.Round(jitterPP, 4).ToString().Trim();
                outputParameters[6].FiledName = "EERISETIME(PS)";
                riseTime = algorithm.ISNaNorIfinity(riseTime);
                outputParameters[6].DefaultValue = Math.Round(riseTime, 4).ToString().Trim();
                outputParameters[7].FiledName = "EEFALLTIME(PS)";
                fallTime = algorithm.ISNaNorIfinity(fallTime);
                outputParameters[7].DefaultValue = Math.Round(fallTime, 4).ToString().Trim();
                outputParameters[8].FiledName = "EEEYEWIDTH(PS)";
                eyeWidth = algorithm.ISNaNorIfinity(eyeWidth);
                outputParameters[8].DefaultValue = Math.Round(eyeWidth, 4).ToString().Trim();
                
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            if (InformationList.Length == 0)//InformationList is null
            {                
                return false;
            }
            else//  InformationList is not null
            {
                int index = -1;
                for (byte i = 0; i < InformationList.Length; i++)
                {
                  
                   
                   
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
                bool isParametersComplete = true;
               
                if (isParametersComplete)
                {
                    //for (byte i = 0; i < InformationList.Length; i++)
                    {

                        if (algorithm.FindFileName(InformationList, "INPUTRXPWR(DBM)", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                TestEleEyeStruct.CensePoint = -20;
                            }
                            else
                            {
                                if (temp > 0)
                                {
                                    temp = -temp;
                                }
                                TestEleEyeStruct.CensePoint = temp;
                            }
                             
                        }

                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }
        protected bool PrepareEnvironment(EquipmentList aEquipList)
        {
            
            if (tempScope != null)
            {                             
                {
                  if (tempScope.SetMaskAlignMethod(1) &&
                  tempScope.SetMode(0) &&
                  tempScope.MaskONOFF(false) &&
                  tempScope.SetRunTilOff() &&
                  tempScope.RunStop(true) &&
                  tempScope.OpenOpticalChannel(false) &&
                  tempScope.RunStop(true) &&
                  tempScope.ClearDisplay() &&
                  tempScope.AutoScale(1)
                  )
                    {
                        logoStr += logger.AdapterLogString(1, "PrepareEnvironment OK!"); 
                        
                        return true;
                       
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(4, "PrepareEnvironment Fail!");
                        return false;
                    }
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
