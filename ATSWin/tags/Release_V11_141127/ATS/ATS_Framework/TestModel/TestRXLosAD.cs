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
   public class TestRXLosAD : TestModelBase
    {
#region Attribute
        private TestRXLosADStruct testRXLosADStruct = new TestRXLosADStruct();
        private double losA;
        private double losD;
        private double losH;
        private ArrayList inPutParametersNameArray = new ArrayList();
#endregion
#region Method
        public TestRXLosAD(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inPuDut;
            logoStr = null;            
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("LOSAMAX");
            inPutParametersNameArray.Add("LOSAMIN");
            inPutParametersNameArray.Add("LOSDMAX");
            inPutParametersNameArray.Add("LOSADSTEP");
            inPutParametersNameArray.Add("ISLOSDETAIL"); 
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
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                        isOK = true;
                    }
                }
                if (selectedEquipList["ATTEN"] != null && selectedEquipList["POWERSUPPLY"] != null)
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

        protected override  bool PostTest()
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
            if (AnalysisInputParameters(inputParameters) == false)
            {
                logger.FlushLogBuffer();
                return false;
            }
            bool isLosA = false;
            bool isLosD = true;
            double tempRxPoint;
            tempRxPoint = testRXLosADStruct.LosAMax;
            if (selectedEquipList["DUT"] != null && selectedEquipList["ATTEN"] != null && selectedEquipList["POWERSUPPLY"] != null)
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
                Attennuator tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                logoStr += logger.AdapterLogString(0, "Step3...TestLosA");
                if (testRXLosADStruct.isLosDetail) 
                {
                    isLosA = LOSADetailTest(tempAtten, tempRxPoint, false);
                } 
                else
                {
                    isLosA = LOSAQuickTest(tempAtten, testRXLosADStruct.LosAMin, false);
                }
              
                if (isLosA==false)
                {
                    logoStr += logger.AdapterLogString(3, "losA=" + isLosA.ToString());
                    losH = losD - losA;                   
                    logger.FlushLogBuffer();                    
                }
                tempRxPoint = losA;
                logoStr += logger.AdapterLogString(1, "losA=" + losA.ToString());
                logoStr += logger.AdapterLogString(0, "Step5...TestLosD");
                if (testRXLosADStruct.isLosDetail)
                {
                    isLosD = LOSDDetailTest(tempAtten, tempRxPoint, true);
                }
                else
                {
                    isLosD = LOSDQuickTest(tempAtten, testRXLosADStruct.LosDMax, true);
                }
              
                if (isLosD == false)
                {
                    logoStr += logger.AdapterLogString(3, "losD=" + (!isLosD).ToString());
                    losH = losD - losA;
                    AnalysisOutputParameters(outputParameters);
                    logger.FlushLogBuffer();
                    return false;
                }
                losH = losD - losA;
                logoStr += logger.AdapterLogString(1, "losD=" + losD.ToString() + "losH=" + losH.ToString());
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();
                return (isLosD==true)&&(isLosA==true);
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                logger.FlushLogBuffer();
                return false;
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
                        if (algorithm.FindFileName(InformationList, "LOSAMAX", out index))
                        {
                            testRXLosADStruct.LosAMax = Convert.ToDouble(InformationList[index].DefaultValue);                           
                             
                        }
                        if (algorithm.FindFileName(InformationList, "LOSAMIN", out index))
                        {
                            testRXLosADStruct.LosAMin = Convert.ToDouble(InformationList[index].DefaultValue);                            
                            
                        }
                        if (algorithm.FindFileName(InformationList, "LOSDMAX", out index))
                        {
                            testRXLosADStruct.LosDMax = Convert.ToDouble(InformationList[index].DefaultValue);                            
                             
                        }
                        if (algorithm.FindFileName(InformationList, "LOSADSTEP", out index))
                        {
                            testRXLosADStruct.LosADStep = Convert.ToDouble(InformationList[index].DefaultValue);                            
                            
                        }
                        if (algorithm.FindFileName(InformationList, "ISLOSDETAIL", out index))
                        {
                            testRXLosADStruct.isLosDetail = Convert.ToBoolean(InformationList[index].DefaultValue);

                        }
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }

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

                    if (algorithm.FindFileName(InformationList, "LOSA", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(losA,4).ToString().Trim();
                    }
                    if (algorithm.FindFileName(InformationList, "LOSD", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(losD,4).ToString().Trim();      
                    }
                    if (algorithm.FindFileName(InformationList, "LOSH", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(losH,4).ToString().Trim();
                    }
                }
                return true;
            }
        }
        private bool LOSADetailTest(Attennuator inputatt, double startAttValue, bool isLosAINput)
        {
            bool isLosA = isLosAINput;
            int i = 0;
            do
            {
                inputatt.AttnValue(startAttValue.ToString(),0);
                isLosA = dut.ChkRxLos();
                Thread.Sleep(50);
                isLosA = dut.ChkRxLos();
                if (isLosA == false)
                {
                    startAttValue -= testRXLosADStruct.LosADStep;
                    i++;
                }

                losA = startAttValue;
            } while (isLosA == false && startAttValue >= testRXLosADStruct.LosAMin&&i<30);
            return isLosA;
        }
        private bool LOSDDetailTest(Attennuator inputatt, double startAttValue, bool isLosDinput)
        {
            bool isLosD = isLosDinput;
            int i = 0;
            do
            {
                inputatt.AttnValue(startAttValue.ToString(),0);
                isLosD = dut.ChkRxLos();
                Thread.Sleep(50);
                isLosD = dut.ChkRxLos();

                if (isLosD == true)
                {
                    startAttValue += testRXLosADStruct.LosADStep;
                }
                losD = startAttValue;
            } while (isLosD == true && startAttValue <= testRXLosADStruct.LosDMax&&i<30);
            return isLosD == false;
        }
       private bool LOSAQuickTest(Attennuator inputatt, double startAttValue, bool isLosAINput)
        {
            bool isLosA = isLosAINput;
            inputatt.AttnValue(startAttValue.ToString(),0);
            isLosA = dut.ChkRxLos();
            Thread.Sleep(50);
            isLosA = dut.ChkRxLos();
            losA = startAttValue;
            return isLosA;
        }
       private bool LOSDQuickTest(Attennuator inputatt, double startAttValue, bool isLosDinput)
       {
           bool isLosD = isLosDinput;
           inputatt.AttnValue(startAttValue.ToString(),0);
           isLosD = dut.ChkRxLos();
           Thread.Sleep(50);
           isLosD = dut.ChkRxLos();
           losD = startAttValue;
           return isLosD==false;
       }
#endregion
        
    }
}
