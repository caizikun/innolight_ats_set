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
    public enum TestLosSpecs : byte
    {
        LOSA,
        LOSD
    }
   public class TestRXLosAD : TestModelBase
    {
#region Attribute
        private TestRXLosADStruct testRXLosADStruct = new TestRXLosADStruct();
        private double losA;
        private double losD;
        private double losAOma;
        private double losDOma;
        private double losH;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private ArrayList OSERValueArray = new ArrayList();
       private Attennuator tempAtten;
       private Powersupply tempps;
       private SortedList<byte, string> SpecNameArray = new SortedList<byte, string>();
#endregion
#region Method
        public TestRXLosAD(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inPuDut;
            logoStr = null;            
            inPutParametersNameArray.Clear();
            OSERValueArray.Clear();
            SpecNameArray.Clear();
            
            inPutParametersNameArray.Add("LOSADTUNESTEP");
            inPutParametersNameArray.Add("ISLOSDETAIL");

            SpecNameArray.Add((byte)AdjustLosSpecs.LOSA, "LOSA(dBm)");
            SpecNameArray.Add((byte)AdjustLosSpecs.LOSD, "LOSD(dBm)");
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
                 tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempAtten != null && tempps != null)
                {
                    isOK = true;

                }
                else
                {                   
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
                if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
            }
            return true;
        }
        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            GenerateSpecList(SpecNameArray);
            if (AnalysisInputParameters(inputParameters) == false || LoadPNSpec()==false)
            {
                OutPutandFlushLog();
                return false;
            }
            bool isLosA = false;
            bool isLosD = true;
            double tempRxPoint;
            tempRxPoint = testRXLosADStruct.LosDMax;
           
            if (tempAtten != null && tempps != null)
            {                
                // open apc 
               
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                }
                
                // open apc
               
                logoStr += logger.AdapterLogString(0, "Step3...TestLosA");
                tempAtten.AttnValue("-6");
                dut.ChkRxLos();//清理Luanch
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
                    OutPutandFlushLog();
                    return true;
                }
                losH = losD - losA;
                logoStr += logger.AdapterLogString(1, "losD=" + losD.ToString() + "losH=" + losH.ToString());
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
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
            logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");
            if (GlobalParameters.OpticalSourseERArray == null || GlobalParameters.OpticalSourseERArray == "")
            {
                return false;
            }
            else
            {
                char[] tempCharArray = new char[] { ',' };
                OSERValueArray = algorithm.StringtoArraylistDeletePunctuations(GlobalParameters.OpticalSourseERArray, tempCharArray);
            }
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
                        if (algorithm.FindFileName(InformationList, "LOSADTUNESTEP", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);

                            if (double.IsInfinity(temp) || double.IsNaN(temp))
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                                testRXLosADStruct.LosADStep = 0.5;
                            }
                            else
                            {
                                if (temp < 0)
                                {
                                    temp = -temp;
                                }
                                 testRXLosADStruct.LosADStep = temp;
                            }                                                       
                            
                        }
                        if (algorithm.FindFileName(InformationList, "ISLOSDETAIL", out index))
                        {
                            string temp=InformationList[index].DefaultValue;

                            if (temp.ToUpper().Trim() == "0" || temp.ToUpper().Trim() == "FALSE")
                            {
                                testRXLosADStruct.isLosDetail = false;
                            }
                            else
                            {
                                testRXLosADStruct.isLosDetail = true;
                            }
                            

                        }
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }

        }
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[5];
                outputParameters[0].FiledName = "LOSA(DBM)";
                losA = algorithm.ISNaNorIfinity(losA);
                outputParameters[0].DefaultValue = Math.Round(losA, 4).ToString().Trim();

                outputParameters[1].FiledName = "LOSD(DBM)";
                losD = algorithm.ISNaNorIfinity(losD);
                outputParameters[1].DefaultValue = Math.Round(losD, 4).ToString().Trim();
                outputParameters[2].FiledName = "LOSH(DBM)";
                losH = algorithm.ISNaNorIfinity(losH);
                outputParameters[2].DefaultValue = Math.Round(losH, 4).ToString().Trim();
                outputParameters[3].FiledName = "LOSA_OMA(DBM)";
                losAOma = algorithm.CalculateOMA(losA, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                outputParameters[3].DefaultValue = Math.Round(losAOma, 4).ToString().Trim();

                outputParameters[4].FiledName = "LOSD_OMA(DBM)";
                losDOma = algorithm.CalculateOMA(losD, Convert.ToDouble(OSERValueArray[GlobalParameters.CurrentChannel - 1]));
                outputParameters[4].DefaultValue = Math.Round(losDOma, 4).ToString().Trim();
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        private bool LOSADetailTest(Attennuator inputatt, double startAttValue, bool isLosAINput)
        {
            bool isLosA = isLosAINput;
            int i = 0;
            int RunCount= Convert.ToInt16((testRXLosADStruct.LosDMax + 1 - testRXLosADStruct.LosAMin) / testRXLosADStruct.LosADStep);
            do
            {
                inputatt.AttnValue(startAttValue.ToString());
                isLosA = dut.ChkRxLos();
                Thread.Sleep(100);
                isLosA = dut.ChkRxLos();
                if (isLosA == false)
                {
                    startAttValue -= testRXLosADStruct.LosADStep;
                    i++;
                }

                losA = startAttValue;
            } while (isLosA == false && startAttValue >= testRXLosADStruct.LosAMin && i < RunCount);
            return isLosA;
        }
        private bool LOSDDetailTest(Attennuator inputatt, double startAttValue, bool isLosDinput)
        {
            bool isLosD = isLosDinput;
            int i = 0;
           
            do
            {
                inputatt.AttnValue(startAttValue.ToString());
               isLosD = dut.ChkRxLos();
                Thread.Sleep(100);
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
            inputatt.AttnValue(startAttValue.ToString(),1);
            Thread.Sleep(300);
            isLosA = dut.ChkRxLos();
            losA = startAttValue;
            return isLosA;
        }
       private bool LOSDQuickTest(Attennuator inputatt, double startAttValue, bool isLosDinput)
       {
           bool isLosD = isLosDinput;
           inputatt.AttnValue(startAttValue.ToString(),1);
           isLosD = dut.ChkRxLos();
           Thread.Sleep(50);
           isLosD = dut.ChkRxLos();
           losD = startAttValue;
           return isLosD==false;
       }
       
       protected bool LoadPNSpec()
       {
           
           try
           {
               if (algorithm.FindDataInDataTable(specParameters, SpecTableStructArray, Convert.ToString(GlobalParameters.CurrentChannel)) == null)
               {
                       return false;
               }

               testRXLosADStruct.LosAMin = SpecTableStructArray[(byte)TestLosSpecs.LOSA].MinValue;
               testRXLosADStruct.LosAMax = SpecTableStructArray[(byte)TestLosSpecs.LOSA].MaxValue;
               testRXLosADStruct.LosDMax = SpecTableStructArray[(byte)TestLosSpecs.LOSD].MaxValue;
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
