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
    public class TestRxDmiPowErrorCurve : TestModelBase
    {

#region Attribute
        private TestRxDmiPowErrorCurveStruct testRxDmiPowErrorCurveStruct = new TestRxDmiPowErrorCurveStruct();
                       
        private double ErrMaxPoint;
        private double MaxErr;

        private ArrayList inPutParametersNameArray = new ArrayList();

        double[] InputPowerArray;
        double[] RXDmiPowArray;
        double[] DiffArray;
        double[] DiffRawArray;

        string sInputPowerArray;
        string sRXDmiPowArray;
        string sDiffArray;
      // equipments
       private  Powersupply tempps;
       private Attennuator tempAtten;
#endregion
#region Method
       public TestRxDmiPowErrorCurve(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inPuDut;
            logoStr = null;           

            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("RxInputPowerMax");
            inPutParametersNameArray.Add("RxInputPowerMin");
            inPutParametersNameArray.Add("AttStep"); 
       }
       protected override  bool CheckEquipmentReadiness()
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
       protected override bool PrepareTest()
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
                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(2, GlobalParameters.CurrentChannel);
                    }
                }
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                 if (tempps != null && tempAtten != null)
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

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";

            sInputPowerArray = "";
            sRXDmiPowArray = "";
            sDiffArray = "";
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }
           
            if (tempps != null && tempAtten != null)
            {                
                // open apc 
                //CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                // open apc
                logoStr += logger.AdapterLogString(0, "Step3...Start Test TestRxDmiPowErrorCurve");
             
                int TestCount;
                double countMol = (Math.Abs(testRxDmiPowErrorCurveStruct.RxInputPowerMax - testRxDmiPowErrorCurveStruct.RxInputPowerMin)) % testRxDmiPowErrorCurveStruct.AttStep;
                if (countMol == 0)
                {
                    TestCount = Convert.ToInt32((Math.Abs(testRxDmiPowErrorCurveStruct.RxInputPowerMax - testRxDmiPowErrorCurveStruct.RxInputPowerMin)) / testRxDmiPowErrorCurveStruct.AttStep);
                }
                else
                {
                    TestCount = Convert.ToInt32((Math.Abs(testRxDmiPowErrorCurveStruct.RxInputPowerMax - testRxDmiPowErrorCurveStruct.RxInputPowerMin) - countMol) / testRxDmiPowErrorCurveStruct.AttStep) + 1;               
                }

                InputPowerArray = new double[TestCount + 1];
                RXDmiPowArray = new double[TestCount + 1];
                DiffArray = new double[TestCount + 1];
                DiffRawArray = new double[TestCount + 1];

                for (int i = 0; i < TestCount + 1; i++)
                {                    
                    if (i != TestCount)
                    {
                        InputPowerArray[i] = testRxDmiPowErrorCurveStruct.RxInputPowerMax - i * testRxDmiPowErrorCurveStruct.AttStep;
                    }
                    else
                    {
                        if (testRxDmiPowErrorCurveStruct.RxInputPowerMax - i * testRxDmiPowErrorCurveStruct.AttStep < testRxDmiPowErrorCurveStruct.RxInputPowerMin)
                        {
                            InputPowerArray[i] = testRxDmiPowErrorCurveStruct.RxInputPowerMin;
                        }
                        else
                        {
                            InputPowerArray[i] = testRxDmiPowErrorCurveStruct.RxInputPowerMax - i * testRxDmiPowErrorCurveStruct.AttStep;
                        }
                    }
                }

                tempAtten.AttnValue(InputPowerArray[0].ToString(), 1);
                Thread.Sleep(3000);
                for (int j = 0; j < InputPowerArray.Length; j++)
                {
                    tempAtten.AttnValue(InputPowerArray[j].ToString(), 1);
                    RXDmiPowArray[j] = dut.ReadDmiRxp();
                    DiffArray[j] = Math.Abs(InputPowerArray[j] - RXDmiPowArray[j]);
                    DiffRawArray[j] = InputPowerArray[j] - RXDmiPowArray[j];

                    if (j != InputPowerArray.Length - 1)
                    {
                        sInputPowerArray += InputPowerArray[j] + ",";
                        sRXDmiPowArray += RXDmiPowArray[j] + ",";
                        sDiffArray += DiffArray[j] + ",";
                    }
                    else
                    {
                        sInputPowerArray += InputPowerArray[j];
                        sRXDmiPowArray += RXDmiPowArray[j];
                        sDiffArray += DiffArray[j];
                    }

                    logoStr += logger.AdapterLogString(1, "InputPowerArray[" + j.ToString() + "]:" + InputPowerArray[j].ToString() + "RXDmiPowArray[" + j.ToString() + "]:" + RXDmiPowArray[j].ToString() + "DiffArray[" + j.ToString() + "]" + DiffArray[j].ToString());
                }
                byte maxIndex;
                algorithm.SelectMaxValue(ArrayList.Adapter(DiffArray), out maxIndex);
                MaxErr = DiffRawArray[maxIndex];
                ErrMaxPoint = InputPowerArray[maxIndex];
                logoStr += logger.AdapterLogString(1, "ErrMaxPoint=" + ErrMaxPoint.ToString() + "  MaxErr" + MaxErr.ToString());
                //tempAtten.OutPutSwitch(false);
                //Thread.Sleep(2000);
                //RxNopticalPoint=dut.ReadDmiRxp();
                //tempAtten.OutPutSwitch(true);
              
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

        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                procData = new TestModeEquipmentParameters[3];

                procData[0].FiledName = "InputPowerArray";
                procData[0].DefaultValue = sInputPowerArray;
                procData[1].FiledName = "RXDmiPowArray";
                procData[1].DefaultValue = sRXDmiPowArray;
                procData[2].FiledName = "DiffArray";
                procData[2].DefaultValue = sDiffArray;

                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }



        }

        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[2];

                outputParameters[0].FiledName = "DMIRXPOWCURVEMAXERRPOINT(DBM)";
                ErrMaxPoint = algorithm.ISNaNorIfinity(ErrMaxPoint);
                outputParameters[0].DefaultValue = Math.Round(ErrMaxPoint, 4).ToString().Trim();
                outputParameters[1].FiledName = "DMIRXPOWCURVEMAXERR(DBM)";
                MaxErr = algorithm.ISNaNorIfinity(MaxErr);
                outputParameters[1].DefaultValue = Math.Round(MaxErr, 4).ToString().Trim();            
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
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
                    if (algorithm.FindFileName(InformationList, "RxInputPowerMax", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxDmiPowErrorCurveStruct.RxInputPowerMax = temp;
                        }                                                   
                    }

                    if (algorithm.FindFileName(InformationList, "RxInputPowerMin", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxDmiPowErrorCurveStruct.RxInputPowerMin = temp;
                        }

                        if (testRxDmiPowErrorCurveStruct.RxInputPowerMin > testRxDmiPowErrorCurveStruct.RxInputPowerMax)
                        {
                            testRxDmiPowErrorCurveStruct.RxInputPowerMin = testRxDmiPowErrorCurveStruct.RxInputPowerMax;
                        }                        
                    }

                    if (algorithm.FindFileName(InformationList, "AttStep", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                        if (double.IsInfinity(temp) || double.IsNaN(temp))
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is wrong!");
                            return false;
                        }
                        else
                        {
                            testRxDmiPowErrorCurveStruct.AttStep = temp;
                        }  
                    }
                }
                logoStr += logger.AdapterLogString(1, "OK!"); 
                return true;
            }
        }       
        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputProcData(procData);
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
