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
        private ArrayList inPutParametersNameArray = new ArrayList();
#endregion
#region Method
        public TestRxPowerDmi(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inPuDut;
           logoStr = null;           

           inPutParametersNameArray.Clear();
           inPutParametersNameArray.Add("ARRAYLISTRXINPUTPOWER(DBM)");           
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
                if (!selectedEquipList.Values[i].Switch(true)) return false;
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

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            if (AnalysisInputParameters(inputParameters)==false)
            {
                logger.FlushLogBuffer();
                return false;
            }
            if (selectedEquipList["DUT"] != null && selectedEquipList["ATTEN"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                Attennuator tempAtten = (Attennuator)selectedEquipList["ATTEN"];
                // open apc 
                string apcstring = null;
                dut.APCStatus(out  apcstring);
                if (apcstring == "OFF" || apcstring == "FF")
                {
                    logoStr += logger.AdapterLogString(0, "Step2...Start Open apc");
                   
                    dut.APCON();
                    logoStr += logger.AdapterLogString(0, "Power off");
                    tempps.Switch(false);
                    Thread.Sleep(200);
                    logoStr += logger.AdapterLogString(0, "Power on");
                    tempps.Switch(true);
                    Thread.Sleep(500);
                    bool isOpen = dut.APCStatus(out  apcstring);
                    if (apcstring == "ON")
                    {
                        logoStr += logger.AdapterLogString(1, "APC ON");
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(3, "APC NOT ON");
                    }
                }
                // open apc
                logoStr += logger.AdapterLogString(0, "Step3...Start Test RxDmi");
                double[] tempRxPowerDmiArray = new double[testRxPowerDmiStruct.ArrayListRxInputPower.Count];
                double[] tempRxPowerErrArray = new double[testRxPowerDmiStruct.ArrayListRxInputPower.Count];
                for (byte i = 0; i < testRxPowerDmiStruct.ArrayListRxInputPower.Count; i++)
                {
                    tempAtten.AttnValue(testRxPowerDmiStruct.ArrayListRxInputPower[i].ToString());
                    Thread.Sleep(1000);
                    tempRxPowerDmiArray[i]=dut.ReadDmiRxp();
                    tempRxPowerErrArray[i] = Math.Abs(Convert.ToDouble(testRxPowerDmiStruct.ArrayListRxInputPower[i].ToString()) - tempRxPowerDmiArray[i]);
                    logoStr += logger.AdapterLogString(1, "testRxPowerDmiStruct.ArrayListRxInputPower[" + i.ToString() + "]:" + testRxPowerDmiStruct.ArrayListRxInputPower[i].ToString() + "tempRxPowerDmiArray[" + i.ToString() + "]:" + tempRxPowerDmiArray[i].ToString() + "tempRxPowerErrArray[" + i.ToString() + "]" + tempRxPowerErrArray[i].ToString());
                }
                byte maxIndex;
               MaxErr= algorithm.SelectMaxValue(ArrayList.Adapter(tempRxPowerErrArray), out maxIndex);
               ErrMaxPoint = Convert.ToDouble(testRxPowerDmiStruct.ArrayListRxInputPower[maxIndex].ToString());
               logoStr += logger.AdapterLogString(1, "ErrMaxPoint=" + ErrMaxPoint.ToString() + "  MaxErr" + MaxErr.ToString());
               AnalysisOutputParameters(outputParameters);
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

                    if (algorithm.FindFileName(InformationList, "DMIRXPWRMAXERRPOINT(DBM)", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(ErrMaxPoint,4).ToString().Trim();
                         
                    }
                    if (algorithm.FindFileName(InformationList, "DMIRXPWRMAXERR", out index))
                    {
                        InformationList[index].DefaultValue = Math.Round(MaxErr,4).ToString().Trim();
                         
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
                        if (algorithm.FindFileName(InformationList, "ARRAYLISTRXINPUTPOWER(DBM)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' }; testRxPowerDmiStruct.ArrayListRxInputPower = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);                            
                             
                        }
                       
                       
                        
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!"); 
                return true;
            }           
        }
#endregion
    }
}
