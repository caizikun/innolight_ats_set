using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ATS_Framework
{
    public class TestEleEye : TestModelBase
    {
 #region Attribute
        private TestEyeStruct testEyeStruct = new TestEyeStruct();
        
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
        private bool HaveAttennuator_Flag = false;
        
 #endregion

 #region Method
        public TestEleEye(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("CENSIPOINT(DBM)");            

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
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                      //  HaveAttennuator_Flag = true;
                        isOK = true;
                    }
                    else
                    {

                    }

                }
                if ( selectedEquipList["POWERSUPPLY"] != null && selectedEquipList["SCOPE"] != null)
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
                logger.FlushLogBuffer();
                return false;
            }
            if (PrepareEnvironment(selectedEquipList) == false)
            {
                logger.FlushLogBuffer();
                return false;
            }
            if (selectedEquipList["POWERSUPPLY"] != null && selectedEquipList["SCOPE"] != null)
            {
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                Attennuator tempAtten = new Attennuator(); ;// = (Attennuator)selectedEquipList["ATTEN"];


                if (selectedEquipList.Keys.Contains("ATTEN"))
                {
                    tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                    HaveAttennuator_Flag = true;
                }
                // open apc 

                byte apcStatus = 0;
                dut.APCStatus(out  apcStatus);
                if (GlobalParameters.ApcStyle == 0)
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
                logoStr += logger.AdapterLogString(0, "Step3...Set AttValue" + Convert.ToString(TestEleEyeStruct.CensePoint)+"DBM");
                if (HaveAttennuator_Flag) tempAtten.AttnValue(Convert.ToString(TestEleEyeStruct.CensePoint), 1);
                logoStr += logger.AdapterLogString(0, "Step4...StartTest Electricl Eye"); 
               
                    logoStr += logger.AdapterLogString(0, "ElecEyeTest");
                    ElecTest();                   
               
                AnalysisOutputParameters(outputParameters);//test
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
            if (selectedEquipList["SCOPE"] != null && selectedEquipList["DUT"] != null)
            {
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];
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
           
             else
            {
                logoStr += logger.AdapterLogString(4,  "Equipments is not enough!");
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
            dut.FullFunctionEnable();
            return true;

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
                    if (algorithm.FindFileName(InformationList, "EECROSSING(%)", out index))
                    {
                        crossing = algorithm.ISNaNorIfinity(crossing);
                        InformationList[index].DefaultValue = Math.Round(crossing,4).ToString().Trim();
                      
                    }
                    if (algorithm.FindFileName(InformationList, "EEMASKMARGIN(%)", out index))
                    {
                        marginVaulue = algorithm.ISNaNorIfinity(marginVaulue);
                        InformationList[index].DefaultValue =Math.Round(marginVaulue,4).ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "EEJITTERRMS(PS)", out index))
                    {
                        jitterRMS = algorithm.ISNaNorIfinity(jitterRMS);
                        InformationList[index].DefaultValue = Math.Round(jitterRMS,4).ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "EEJITTERPP(PS)", out index))
                    {
                        jitterPP = algorithm.ISNaNorIfinity(jitterPP);
                        InformationList[index].DefaultValue = Math.Round(jitterPP,4).ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "EERISETIME(PS)", out index))
                    {
                        riseTime = algorithm.ISNaNorIfinity(riseTime);
                        InformationList[index].DefaultValue = Math.Round(riseTime,4).ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "EEFALLTIME(PS)", out index))
                    {
                        fallTime = algorithm.ISNaNorIfinity(fallTime);
                        InformationList[index].DefaultValue = Math.Round(fallTime,4).ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "EETXAMP(MV)", out index))
                    {
                        amp = algorithm.ISNaNorIfinity(amp);
                        InformationList[index].DefaultValue = Math.Round(amp, 4).ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "EEEYEHIGHT(MV)", out index))
                    {
                        eyeHight = algorithm.ISNaNorIfinity(eyeHight);
                        InformationList[index].DefaultValue = Math.Round(eyeHight, 4).ToString().Trim();

                    }
                    if (algorithm.FindFileName(InformationList, "EEEYEWIDTH(PS)", out index))
                    {
                        eyeWidth = algorithm.ISNaNorIfinity(eyeWidth);
                        InformationList[index].DefaultValue = Math.Round(eyeWidth, 4).ToString().Trim();

                    }
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
                bool isParametersComplete = false;
                for (byte i = 0; i < inPutParametersNameArray.Count; i++)
                {
                    if (algorithm.FindFileName(InformationList, inPutParametersNameArray[i].ToString(), out index) == false)
                    {
                        logoStr += logger.AdapterLogString(4, inPutParametersNameArray[i].ToString() + "is not exist");
                        isParametersComplete = false;
                        return isParametersComplete;
                    }
                    else
                    {
                        isParametersComplete = true;
                        continue;
                    }

                }
                if (isParametersComplete)
                {
                    for (byte i = 0; i < InformationList.Length; i++)
                    {

                        if (algorithm.FindFileName(InformationList, "CENSIPOINT(DBM)", out index))
                        {
                            TestEleEyeStruct.CensePoint = Convert.ToDouble(InformationList[index].DefaultValue);
                        }

                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }
        protected bool PrepareEnvironment(EquipmentList aEquipList)
        {
            if (selectedEquipList["SCOPE"] != null)
            {
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];                
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
#endregion
        
       
    
    }
}
