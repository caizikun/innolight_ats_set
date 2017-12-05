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
  public  class TestRxPowerDmi : TestModelBase
    {

#region Attribute
        private TestRxPowerDmiStruct testRxPowerDmiStruct = new TestRxPowerDmiStruct();
                
        
        private double ErrMaxPoint;
        private double MaxErr;
        private double RxNopticalPoint;
        private ArrayList inPutParametersNameArray = new ArrayList();
      // equipments
       private  Powersupply tempps;
       private Attennuator tempAtten;
#endregion
#region Method
        public TestRxPowerDmi(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inPuDut;
           logoStr = null;           

           inPutParametersNameArray.Clear();
           inPutParametersNameArray.Add("RXPOWERARRLIST(DBM)");           
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
                logoStr += logger.AdapterLogString(0, "Step3...Start Test RxDmi");
                double[] tempRxPowerDmiArray = new double[testRxPowerDmiStruct.ArrayListRxInputPower.Count];
                double[] tempRxPowerErrArray = new double[testRxPowerDmiStruct.ArrayListRxInputPower.Count];
                double[] tempRxPowerErrRawArray = new double[testRxPowerDmiStruct.ArrayListRxInputPower.Count];
                tempAtten.AttnValue(testRxPowerDmiStruct.ArrayListRxInputPower[0].ToString(), 1);
                Thread.Sleep(3000);
                for (byte i = 0; i < testRxPowerDmiStruct.ArrayListRxInputPower.Count; i++)
                {
                    tempAtten.AttnValue(testRxPowerDmiStruct.ArrayListRxInputPower[i].ToString(),1);  
                    tempRxPowerDmiArray[i]=dut.ReadDmiRxp();
                    tempRxPowerErrArray[i] = Math.Abs(Convert.ToDouble(testRxPowerDmiStruct.ArrayListRxInputPower[i].ToString()) - tempRxPowerDmiArray[i]);
                    tempRxPowerErrRawArray[i] = Convert.ToDouble(testRxPowerDmiStruct.ArrayListRxInputPower[i].ToString()) - tempRxPowerDmiArray[i];
                    logoStr += logger.AdapterLogString(1, "testRxPowerDmiStruct.ArrayListRxInputPower[" + i.ToString() + "]:" + testRxPowerDmiStruct.ArrayListRxInputPower[i].ToString() + "tempRxPowerDmiArray[" + i.ToString() + "]:" + tempRxPowerDmiArray[i].ToString() + "tempRxPowerErrArray[" + i.ToString() + "]" + tempRxPowerErrArray[i].ToString());
                }
                byte maxIndex;
               algorithm.SelectMaxValue(ArrayList.Adapter(tempRxPowerErrArray), out maxIndex);
               MaxErr = tempRxPowerErrRawArray[maxIndex];
               ErrMaxPoint = Convert.ToDouble(testRxPowerDmiStruct.ArrayListRxInputPower[maxIndex].ToString());
               logoStr += logger.AdapterLogString(1, "ErrMaxPoint=" + ErrMaxPoint.ToString() + "  MaxErr" + MaxErr.ToString());
               tempAtten.OutPutSwitch(false);
               Thread.Sleep(2000);
               RxNopticalPoint=dut.ReadDmiRxp();
               tempAtten.OutPutSwitch(true);
              
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
        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                outputParameters = new TestModeEquipmentParameters[3];

                outputParameters[0].FiledName = "DMIRXPWRMAXERRPOINT(DBM)";
                ErrMaxPoint = algorithm.ISNaNorIfinity(ErrMaxPoint);
                outputParameters[0].DefaultValue = Math.Round(ErrMaxPoint, 4).ToString().Trim();
                outputParameters[1].FiledName = "DMIRXPWRERR(DBM)";
                MaxErr = algorithm.ISNaNorIfinity(MaxErr);
                outputParameters[1].DefaultValue = Math.Round(MaxErr, 4).ToString().Trim();
                outputParameters[2].FiledName = "DMIRXNOPTICAL(DBM)";
                RxNopticalPoint = algorithm.ISNaNorIfinity(RxNopticalPoint);
                outputParameters[2].DefaultValue = Math.Round(RxNopticalPoint, 4).ToString().Trim();
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
                    //for (byte i = 0; i < InformationList.Length; i++)
                    {
                        if (algorithm.FindFileName(InformationList, "RXPOWERARRLIST(DBM)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAR= algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAR==null)
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is Null!");
                                return false;
                            } 
                            else
                            {
                                testRxPowerDmiStruct.ArrayListRxInputPower = tempAR;
                            }
                            
                            for (byte j = 0; j < testRxPowerDmiStruct.ArrayListRxInputPower.Count; j++)
                            {
                                double temp = Convert.ToDouble(testRxPowerDmiStruct.ArrayListRxInputPower[j]);
                                if (temp > 0)
                                {
                                    temp = -temp;
                                    testRxPowerDmiStruct.ArrayListRxInputPower[j] = temp;
                                }
                            }
                             
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
