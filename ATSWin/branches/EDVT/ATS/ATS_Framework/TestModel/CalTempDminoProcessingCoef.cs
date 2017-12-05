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
   public class CalTempDminoProcessingCoef : TestModelBase
    {
#region Attribute
        private CalTempDmiStruct calTempDmiStruct = new CalTempDmiStruct();
        private float tempDmiCoefA;
        private float tempDmiCoefB;
        private float tempDmiCoefC;
        private ArrayList tempDmiCoefArray = new ArrayList();
        private SortedList<string,string> tempratureADCArray = new SortedList<string,string>();
        private SortedList<string, string> realTempratureArray = new SortedList<string, string>();
        private bool isCalTempDmiOk;        
      
        private ArrayList inPutParametersNameArray = new ArrayList();
#endregion
#region  Method
        public CalTempDminoProcessingCoef(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;         
            inPutParametersNameArray.Clear();
            inPutParametersNameArray.Add("1STOR2STORPID");
            tempratureADCArray.Clear();
            realTempratureArray.Clear();           
        }
        override protected bool CheckEquipmentReadiness()
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
        override protected bool PrepareTest()
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
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                }
                if ( selectedEquipList["POWERSUPPLY"] != null)
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
            IList<string> tempKeys = realTempratureArray.Keys;
            for (byte i = 0; i < tempKeys.Count; i++)
            {
                if (tempKeys[i].ToUpper().Substring(0, tempKeys[i].ToUpper().Length - 1) == (GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    logger.AdapterLogString(0, "Current temprature had exist");
                    return true;
                }
            }
            
           
            if (AnalysisInputParameters(inputParameters)==false)
            {
                return false;
            }
            if (selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                bool isWriteCoefCOk = false;
                bool isWriteCoefBOk = false;
                bool isWriteCoefAOk = false;              
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                // close apc 
                string apcstring = null;
                dut.APCStatus(out  apcstring);
                if (apcstring == "ON" || apcstring == "FF")
                {
                    logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                    dut.APCOFF();
                    logoStr += logger.AdapterLogString(0, "Power off");
                    tempps.Switch(false);
                    Thread.Sleep(200);
                    logoStr += logger.AdapterLogString(0, "Power on");
                    tempps.Switch(true);
                    Thread.Sleep(500);
                    bool isclosed = dut.APCStatus(out  apcstring);
                    if (apcstring == "OFF")
                    {
                        logoStr += logger.AdapterLogString(1, "APC OFF");
                       
                    }
                    else
                    {
                        logoStr += logger.AdapterLogString(3, "APC NOT OFF");
                    }
                }
                // close apc
 #region  CheckTempChange

                if (!realTempratureArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step3...TempChanged Read tempratureADC");
                    for (byte i = 0; i < GlobalParameters.TotalTempCount;i++)
                    {
                        realTempratureArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper() + i.ToString().ToUpper().Trim(), GlobalParameters.CurrentTemp.ToString().Trim());
                        logoStr += logger.AdapterLogString(1, "realtemprature="+i.ToString() + GlobalParameters.CurrentTemp.ToString());
                        UInt16 tempratureADC;
                        dut.ReadTempADC(out tempratureADC, (byte)(i + 1));
                        logoStr += logger.AdapterLogString(1, "tempratureADC" + i.ToString() + tempratureADC.ToString());
                        tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper() + i.ToString().ToUpper().Trim(), tempratureADC.ToString().Trim());
                    }
                   
                  
                }               
 #endregion
                int tempCount = 0;
                tempCount = Math.Min(tempratureADCArray.Count / GlobalParameters.TotalTempCount, realTempratureArray.Count / GlobalParameters.TotalTempCount);
                double[,] tempTempAdcArray = new double[GlobalParameters.TotalTempCount, tempCount];
                double[,] tempTempValueArray = new double[GlobalParameters.TotalTempCount, tempCount];
                logoStr += logger.AdapterLogString(0, "Step4...Start Fitting Curve");
                if (tempCount >= 2)
                {
                    for (byte i = 0; i < GlobalParameters.TotalTempCount; i++)
                    {
                        int tempcount1 = 0;
                        for (byte j = 0; j < Math.Min(realTempratureArray.Count, tempratureADCArray.Count); j++)
                        {
                            int tempstr2 = tempratureADCArray.Keys[j].ToUpper().Length;
                            string tempstring = tempratureADCArray.Keys[j].ToUpper().Substring(tempratureADCArray.Keys[j].ToUpper().Length - 1, 1);
                            string iStr = i.ToString().ToUpper().Trim();
                            if (tempstring == iStr)
                            {
                                tempTempAdcArray[i, tempcount1] = double.Parse(tempratureADCArray.Values[j]);
                                tempTempValueArray[i, tempcount1] = double.Parse(realTempratureArray.Values[j]);
                                tempcount1++;
                            }
                        }
                    }
                    for (byte i = 0; i < GlobalParameters.TotalTempCount; i++)
                    {
                        for (byte j = 0; j < tempCount; j++)
                        {
                            tempTempValueArray[i, j] = tempTempValueArray[i, j] * 256;
                        }

                    }
                    double[] adcArray = new double[tempCount];
                    double[] realArray = new double[tempCount];
                    if (calTempDmiStruct.is1Stor2StorPid == 2)
                    {                        
                        for (byte i = 0; i < GlobalParameters.TotalTempCount; i++)
                        {
                            
                            for (byte j = 0; j < tempCount; j++)
                            {
                                adcArray[j]=tempTempAdcArray[i, j];
                                realArray[j] = tempTempValueArray[i, j];
                            }
                            double[] coefArray = algorithm.MultiLine(adcArray, realArray, tempCount, 2);
                            tempDmiCoefC = (float)coefArray[0];
                            tempDmiCoefB = (float)coefArray[1];
                            tempDmiCoefA = (float)coefArray[2];

                            tempDmiCoefArray = ArrayList.Adapter(coefArray);
                            tempDmiCoefArray.Reverse();
                            for (byte k = 0; k < tempDmiCoefArray.Count; k++)
                            {
                                logoStr += logger.AdapterLogString(1, "tempDmiCoefArray[" + k.ToString() + "]=" + tempDmiCoefArray[k].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(tempDmiCoefArray[k])));

                            }
                            logoStr += logger.AdapterLogString(0, "Step5...WriteCoef");
                            #region W&R Tempcoefc
                            isWriteCoefCOk = dut.SetTempcoefc(tempDmiCoefC.ToString(), (byte)(i + 1));
                            if (isWriteCoefCOk)
                            {
                                isWriteCoefCOk = true;
                                logoStr += logger.AdapterLogString(1, "WritetempDmiCoefC:" + isWriteCoefCOk.ToString());
                            }
                            else
                            {
                                isWriteCoefCOk = false;
                                logoStr += logger.AdapterLogString(3, "WritetempDmiCoefC:" + isWriteCoefCOk.ToString());

                            }
                            #endregion
                            #region W&R Tempcoefb
                            isWriteCoefBOk = dut.SetTempcoefb(tempDmiCoefB.ToString(), (byte)(i + 1));
                            if (isWriteCoefBOk)
                            {
                                isWriteCoefBOk = true;
                                logoStr += logger.AdapterLogString(1, "WritetempDmiCoefB:" + isWriteCoefBOk.ToString());
                            }
                            else
                            {
                                isWriteCoefBOk = false;
                                logoStr += logger.AdapterLogString(3, "WritetempDmiCoefB:" + isWriteCoefBOk.ToString());

                            }
                            #endregion
                            #region W&R Tempcoefa
                            //isWriteCoefAOk = dut.SetTempcoefa(tempDmiCoefA.ToString(), i + 1);
                            if (isWriteCoefAOk)
                            {
                                isWriteCoefAOk = true;
                                logoStr += logger.AdapterLogString(1, "WritetempDmiCoefA:" + isWriteCoefAOk.ToString());

                            }
                            else
                            {
                                isWriteCoefAOk = false;
                                logoStr += logger.AdapterLogString(3, "WritetempDmiCoefA:" + isWriteCoefAOk.ToString());
                            }
                            #endregion
                            if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                            {
                                isCalTempDmiOk = true;
                                logoStr += logger.AdapterLogString(1, "isCalTempDmiOk:" + isCalTempDmiOk.ToString());

                            }
                            else
                            {
                                isCalTempDmiOk = false;
                                logoStr += logger.AdapterLogString(3, "isCalTempDmiOk:" + isCalTempDmiOk.ToString());

                            }

                        }
                       
                    }
                    else if (calTempDmiStruct.is1Stor2StorPid == 1)
                    {
                      
                        
                        for (byte i = 0; i < GlobalParameters.TotalTempCount; i++)
                        {
                            for (byte j = 0; j < tempCount; j++)
                            {
                                adcArray[j] = tempTempAdcArray[i, j];
                                realArray[j] = tempTempValueArray[i, j];
                            }
                            double[] coefArray = algorithm.MultiLine(adcArray, realArray, tempCount, 1);
                            tempDmiCoefC = (float)coefArray[0];
                            tempDmiCoefB = (float)coefArray[1];
                            double[] tempCoefArray = new double[2] { tempDmiCoefC, tempDmiCoefB };

                            tempDmiCoefArray = ArrayList.Adapter(tempCoefArray);
                            tempDmiCoefArray.Reverse();
                            for (byte k = 0; k < tempDmiCoefArray.Count; k++)
                            {
                                logoStr += logger.AdapterLogString(1, "tempDmiCoefArray[" + k.ToString() + "]=" + tempDmiCoefArray[k].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(tempDmiCoefArray[k])));
                            }
                            logoStr += logger.AdapterLogString(0, "Step5...WriteCoef");

                            #region W&R Tempcoefc
                            isWriteCoefCOk = dut.SetTempcoefc(tempDmiCoefC.ToString(), (byte)(i + 1));
                            if (isWriteCoefCOk)
                            {
                                isWriteCoefCOk = true;
                                logoStr += logger.AdapterLogString(1, "WritetempDmiCoefC:" + isWriteCoefCOk.ToString());

                            }
                            else
                            {
                                isWriteCoefCOk = false;
                                logoStr += logger.AdapterLogString(3, "WritetempDmiCoefC:" + isWriteCoefCOk.ToString());

                            }
                            #endregion
                            #region W&R Tempcoefb
                            isWriteCoefBOk = dut.SetTempcoefb(tempDmiCoefB.ToString(), (byte)(i + 1));
                            if (isWriteCoefBOk)
                            {
                                isWriteCoefBOk = true;
                                logoStr += logger.AdapterLogString(1, "WritetempDmiCoefB:" + isWriteCoefBOk.ToString());
                            }
                            else
                            {
                                isWriteCoefBOk = false;
                                logoStr += logger.AdapterLogString(3, "WritetempDmiCoefB:" + isWriteCoefBOk.ToString());
                            }
                            #endregion
                            if (isWriteCoefBOk & isWriteCoefCOk)
                            {
                                isCalTempDmiOk = true;
                                logoStr += logger.AdapterLogString(1, "isCalTempDmiOk:" + isCalTempDmiOk.ToString());
                            }
                            else
                            {
                                isCalTempDmiOk = false;
                                logoStr += logger.AdapterLogString(3, "isCalTempDmiOk:" + isCalTempDmiOk.ToString());
                            }
                        }
                      

                    }
                }
                else
                {
                    logoStr += logger.AdapterLogString(0, "TempCount<2:");
                    isCalTempDmiOk = true;
                }
               
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                isCalTempDmiOk = false;
                logger.FlushLogBuffer();
                return isCalTempDmiOk;
            }
            AnalysisOutputParameters(outputParameters);
            logger.FlushLogBuffer();
            return isCalTempDmiOk;
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
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTDMITEMPCOEF", out index))
                    {
                        InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(tempDmiCoefArray, ",");
                        
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
                       
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPID", out index))
                        {
                            calTempDmiStruct.is1Stor2StorPid = Convert.ToByte(InformationList[index].DefaultValue);

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
