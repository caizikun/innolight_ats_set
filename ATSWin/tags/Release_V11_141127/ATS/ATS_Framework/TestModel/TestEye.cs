using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace ATS_Framework
{
    public class TestEye : TestModelBase
    {
 #region Attribute
        private TestEyeStruct testEyeStruct = new TestEyeStruct();
        
        private double apDBM;
        private double er;
        private double crossing;
        private double marginVaulue;
        private double jitterRMS;
        private double jitterPP;
        private double OEOMA;
        private double riseTime;
        private double fallTime;
        private ArrayList inPutParametersNameArray = new ArrayList();
 #endregion

 #region Method
        public TestEye(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("ISOPTICALEYEORELECEYE");            

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
                if (selectedEquipList["POWERSUPPLY"] != null && selectedEquipList["SCOPE"] != null)
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
                logoStr += logger.AdapterLogString(0, "Step3...StartTestOptical Eye"); 
                if (testEyeStruct.isOpticalEyeORElecEye)
                {
                    logoStr += logger.AdapterLogString(0, "OpticalEyeTest");
                    OpticalTest();
                    
                }                
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
        protected bool OpticalTest()
        {
            apDBM = 0;
            er = 0;
            crossing = 0;
            marginVaulue = 0;
            jitterRMS = 0;
            jitterPP = 0;
            OEOMA = 0;
            riseTime = 0;
            fallTime = 0;
            if (selectedEquipList["SCOPE"] != null && selectedEquipList["DUT"] != null)
            {
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];
                tempScope.pglobalParameters = GlobalParameters;
                double[]tempDoubleArray=tempScope.OpticalEyeTest();
                OEOMA = tempDoubleArray[2];
                jitterRMS = tempDoubleArray[5];
                jitterPP = tempDoubleArray[4];
                riseTime = tempDoubleArray[6];
                fallTime = tempDoubleArray[7];
                crossing = tempDoubleArray[3];
                er = tempDoubleArray[1];
                apDBM = tempDoubleArray[0];
                marginVaulue = tempDoubleArray[8];
                logoStr += logger.AdapterLogString(1, "apDBM:" + apDBM.ToString());
                logoStr += logger.AdapterLogString(1,"er:" + er.ToString());
                logoStr += logger.AdapterLogString(1, "crossing:" + crossing.ToString());
                logoStr += logger.AdapterLogString(1, "maskVaulue:" + marginVaulue.ToString());
                logoStr += logger.AdapterLogString(1, "jitterRMS:" + jitterRMS.ToString());
                logoStr += logger.AdapterLogString(1,  "jitterPP:" + jitterPP.ToString());
                logoStr += logger.AdapterLogString(1, "OEOMA:" + OEOMA.ToString());
                logoStr += logger.AdapterLogString(1, "riseTime:" + riseTime.ToString());
                logoStr += logger.AdapterLogString(1, "fallTime:" + fallTime.ToString());
                return true;
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                
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
            if (InformationList.Length == 0)//InformationList is null
            {                
                return false;
            }
            else//  InformationList is not null
            {
                int index = -1;
                for (byte i = 0; i < InformationList.Length; i++)
                {

                    if (algorithm.FindFileName(InformationList, "AP(DBM)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(apDBM,4).ToString().Trim();           
                    }
                    if (algorithm.FindFileName(InformationList, "ER(DB)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(er,4).ToString().Trim();              
                    }
                    if (algorithm.FindFileName(InformationList, "CROSSING(%)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(crossing,4).ToString().Trim();
                      
                    }
                    if (algorithm.FindFileName(InformationList, "MASKMARGIN(%)", out index))
                    {
                        InformationList[index].DefaultValue =Math.Round(marginVaulue,4).ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "JITTERRMS(PS)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(jitterRMS,4).ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "JITTERPP(PS)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(jitterPP,4).ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "RISETIME(PS)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(riseTime,4).ToString().Trim();
                        
                    }
                    if (algorithm.FindFileName(InformationList, "FALLTIME(PS)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(fallTime,4).ToString().Trim();
                       
                    }
                    if (algorithm.FindFileName(InformationList, "TXOMA(MW)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(OEOMA, 4).ToString().Trim();
                        
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
                        if (algorithm.FindFileName(InformationList, "ISOPTICALEYEORELECEYE", out index))
                        {
                            testEyeStruct.isOpticalEyeORElecEye = Convert.ToBoolean(InformationList[index].DefaultValue);
                             ;
                        }
                    }

                }
                logoStr += logger.AdapterLogString(1,"OK!"); 
                return true;
            }

        }
        protected bool PrepareEnvironment(EquipmentList aEquipList)
        {
            if (selectedEquipList["SCOPE"] != null)
            {
                Scope tempScope = (Scope)selectedEquipList["SCOPE"];
                if (testEyeStruct.isOpticalEyeORElecEye)
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
                    if (tempScope.SetMaskAlignMethod(1) &&
                  tempScope.SetMode(0) &&
                  tempScope.MaskONOFF(false) &&
                  tempScope.SetRunTilOff() &&
                  tempScope.RunStop(true) &&
                  tempScope.OpenOpticalChannel(false) &&
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
