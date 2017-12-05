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
    
    public class AdjustAPD : TestModelBase
    {
#region Attribute       
        
        private SortedList<string, ArrayList> adjustAPDtValueRecordsStruct = new SortedList<string, ArrayList>();
        private AdjustAPDStruct adjustAPDStruct = new AdjustAPDStruct();
        private bool isApdAdjustOK;     
        private SortedList<string, string> tempratureADCArray = new SortedList<string, string>();
        private ArrayList tempratureADCArrayList = new ArrayList(); 
        private ArrayList inPutParametersNameArray = new ArrayList();       
        //cal apd
        private float apdPowerCoefA;
        private float apdPowerCoefB;
        private float apdPowerCoefC;
        private bool isCalApdPowerOk;
        private ArrayList apdCoefArray = new ArrayList();
        //cal apd           
       
#endregion
#region Method
        public AdjustAPD(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
            adjustAPDtValueRecordsStruct.Clear();
            tempratureADCArrayList.Clear();
            // input parameters
            inPutParametersNameArray.Clear();           
            inPutParametersNameArray.Add("AUTOTUNE");
            inPutParametersNameArray.Add("APDCALPOINT(DBM)");
            inPutParametersNameArray.Add("ARRAYLISTAPDBIASPOINTS(V)");
            inPutParametersNameArray.Add("APDBIASSTEP(V)");
            inPutParametersNameArray.Add("1STOR2STORPID");            
            //...
           
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
            dut.FullFunctionEnable();
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
                    if (tempKeys[i].ToUpper().Contains("ERRORDETE"))
                    {
                        selectedEquipList.Add("ED", tempValues[i]);

                    }
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }

                }
                if (selectedEquipList["ERRORDETE"] != null && selectedEquipList["ATTEN"] != null && selectedEquipList["POWERSUPPLY"] != null)
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
                } 
                else
                {
                    isOK = false;
                }
                return isOK;
            }
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
            
            if (selectedEquipList["ERRORDETE"] != null && selectedEquipList["ATTEN"] != null && selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                bool isWriteCoefCOk = false;
                bool isWriteCoefBOk = false;
                bool isWriteCoefAOk = false;
               
                ErrorDetector tempED = (ErrorDetector)selectedEquipList["ERRORDETE"];
                Attennuator tempAtt = (Attennuator)selectedEquipList["ATTEN"];
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                // add logo infor               
                logoStr += logger.AdapterLogString(0, "Setp2...SetAttenPoint:" + adjustAPDStruct.ApdCalPoint.ToString());
                // add logo infor
                bool ok = tempAtt.AttnValue(adjustAPDStruct.ApdCalPoint.ToString(),0);
               // add logo infor
                logoStr += logger.AdapterLogString(0, ok.ToString());
               // close apc 
                byte apcStatus = 0;
                dut.APCStatus(out  apcStatus);
                if (GlobalParameters.ApcStyle == 0)
                {
                    if (apcStatus == 0x11)
                    {
                        logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                        dut.APCOFF(0x11);
                        logoStr += logger.AdapterLogString(0, "Power off");
                        tempps.Switch(false,1);                        
                        logoStr += logger.AdapterLogString(0, "Power on");
                        tempps.Switch(true,1);                       
                        bool isclosed = dut.APCStatus(out  apcStatus);
                        if (apcStatus == 0x00)
                        {
                            logoStr += logger.AdapterLogString(1, "APC OFF");

                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "APC NOT OFF");

                        }
                    }
                }
                else if (GlobalParameters.ApcStyle == 1)
                {
                    if (apcStatus == 0x11 || apcStatus == 0x10 || apcStatus == 0x01)
                    {
                        logoStr += logger.AdapterLogString(0, "Step2...Start close apc");
                        dut.APCOFF(0x11);
                        logoStr += logger.AdapterLogString(0, "Power off");
                        tempps.Switch(false,1);                        
                        logoStr += logger.AdapterLogString(0, "Power on");
                        tempps.Switch(true,1);                       
                        bool isclosed = dut.APCStatus(out  apcStatus);
                        if (apcStatus == 0x00)
                        {
                            logoStr += logger.AdapterLogString(1, "APC OFF");

                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "APC NOT OFF");

                        }
                    }

                }
                
               // close apc
                           
                UInt32[] tempApdArray = new UInt32[adjustAPDStruct.ArrayListApdBiasPoints.Count];
                for (byte i = 0; i < adjustAPDStruct.ArrayListApdBiasPoints.Count; i++)
                {
                    tempApdArray[i] = Convert.ToUInt32(adjustAPDStruct.ArrayListApdBiasPoints[i].ToString());
                }
                logoStr += logger.AdapterLogString(0, "Step4...StartCalApd");
                ArrayList tempBerArray = new ArrayList();
                ArrayList tempApdDacArray = new ArrayList();
                double minBer = -1;
               UInt32 targetAPDAC=  EnumMethod(tempApdArray, dut, tempED, adjustAPDStruct.ApdBiasStep, out tempBerArray, out tempApdDacArray, out minBer);
               minBer = Math.Round(minBer, 5);
               for (byte i = 0; i < Math.Min(tempBerArray.Count, tempApdDacArray.Count); i++)
               {
                   logoStr += logger.AdapterLogString(1,  GlobalParameters.CurrentChannel.ToString());
                   logoStr += logger.AdapterLogString(1,  tempApdDacArray[i].ToString() + "  " + tempApdDacArray[i].ToString());                   
               }
               logoStr += logger.AdapterLogString(1,"minApdDac=" + targetAPDAC.ToString() + "  minBer=" + minBer.ToString());
               isApdAdjustOK = true;
 #region  CheckTempChange
                if (!tempratureADCArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step4...TempChanged Read tempratureADC");
                    logoStr += logger.AdapterLogString(1, "realtemprature=" + GlobalParameters.CurrentTemp.ToString());
                    
                    UInt16 tempratureADC;
                    dut.ReadTempADC(out tempratureADC,1);
                    logoStr += logger.AdapterLogString(1, "tempratureADC=" + tempratureADC.ToString());                                
                    tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper(), tempratureADC.ToString().Trim());
                    tempratureADCArrayList.Add(tempratureADC);
                } 
#endregion
#region  add current channel
                if (!adjustAPDtValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step6...add current channel records");
                    logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());                    
                        adjustAPDtValueRecordsStruct.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), new ArrayList());
                        adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(targetAPDAC);
                       
                }
                else
                {
                    adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(targetAPDAC);
                }
              
               
#endregion
#region  CurveCoef                
                
                if (adjustAPDtValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel .ToString().Trim().ToUpper()))
                    {
                        if (tempratureADCArray.Count >= 2 && adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Count >= 2)
                        {
                            int tempCount = Math.Min(tempratureADCArray.Count, adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Count);
                            double[] tempAPDDacArray = new double[tempCount];
                            double[] tempTempAdcArray = new double[tempCount];
                            for (byte i = 0; i < tempCount; i++)
                            {
                                tempAPDDacArray[i] = double.Parse(adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()][i].ToString());
                                tempTempAdcArray[i] = double.Parse(tempratureADCArrayList[i].ToString());
                            }
                            logoStr += logger.AdapterLogString(0, "Step8...Start Fitting Curve");  
                                                     
                            if (adjustAPDStruct.is1Stor2StorPid == 2)
                            {
                                double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempAPDDacArray, tempratureADCArray.Count, 2);
                                apdPowerCoefC = (float)coefArray[0];
                                apdPowerCoefB = (float)coefArray[1];
                                apdPowerCoefA = (float)coefArray[2];
                                apdCoefArray = ArrayList.Adapter(coefArray);
                                apdCoefArray.Reverse();
                                for (byte i = 0; i < apdCoefArray.Count; i++)
                                {
                                    logoStr += logger.AdapterLogString(1,  "apdCoefArray[" + i.ToString() + "]=" + apdCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(apdCoefArray[i])));
                                }
                                logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");
                                                   
                                #region W&R Apddaccoefc
                              isWriteCoefCOk=dut.SetBiasdaccoefc(apdPowerCoefC.ToString());
                              if (isWriteCoefCOk)
                                {
                                    isWriteCoefCOk = true;
                                    logoStr += logger.AdapterLogString(1, "isWriteCoefCOk:" + isWriteCoefCOk.ToString());
                                                   
                                }
                                else
                                {
                                    isWriteCoefCOk = false;
                                    logoStr += logger.AdapterLogString(3, "isWriteCoefCOk:" + isWriteCoefCOk.ToString());
                                }
                                #endregion
                                #region W&R Apddaccoefb
                              isWriteCoefBOk=dut.SetBiasdaccoefb(apdPowerCoefB.ToString());
                              if (isWriteCoefBOk)
                                {
                                    isWriteCoefBOk = true;
                                    logoStr += logger.AdapterLogString(1, "isWriteCoefBOk:" + isWriteCoefBOk.ToString());

                                }
                                else
                                {
                                    isWriteCoefBOk = false;
                                    logoStr += logger.AdapterLogString(3, "isWriteCoefBOk:" + isWriteCoefBOk.ToString());
                                }
                                #endregion
                                #region W&R Apddaccoefa
                              isWriteCoefAOk=dut.SetBiasdaccoefa(apdPowerCoefA.ToString());

                              if (isWriteCoefAOk)
                                {
                                    isWriteCoefAOk = true;
                                    logoStr += logger.AdapterLogString(1, "isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                                }
                                else
                                {
                                    isWriteCoefAOk = false;
                                    logoStr += logger.AdapterLogString(3, "isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                                }
                                #endregion
                                if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                                {
                                    isCalApdPowerOk = true;
                                    logoStr += logger.AdapterLogString(1, "isCalApdPowerOk:" + isCalApdPowerOk.ToString());
                                }
                                else
                                {
                                    isCalApdPowerOk = false;
                                    logoStr += logger.AdapterLogString(3, "isCalApdPowerOk:" + isCalApdPowerOk.ToString());
                                }
                            }
                            else if (adjustAPDStruct.is1Stor2StorPid == 1)
                            {
                                double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempAPDDacArray, tempratureADCArray.Count, 1);
                                apdPowerCoefC = (float)coefArray[0];
                                apdPowerCoefB = (float)coefArray[1];
                                apdPowerCoefA = 0; 
                                apdCoefArray = ArrayList.Adapter(coefArray);
                                apdCoefArray.Reverse();
                                for (byte i = 0; i < apdCoefArray.Count; i++)
                                {
                                    logoStr += logger.AdapterLogString(1,  "apdCoefArray[" + i.ToString() + "]=" + apdCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(apdCoefArray[i])));                                   
                                    
                                }
                                logoStr += logger.AdapterLogString(0, "Step9...WriteCoef"); 
                               
                                #region W&R Apddaccoefc
                               isWriteCoefCOk= dut.SetBiasdaccoefc(apdPowerCoefC.ToString());

                               if (isWriteCoefCOk)
                                {
                                    isWriteCoefCOk = true;
                                    logoStr += logger.AdapterLogString(1, "isWriteCoefCOk:" + isWriteCoefCOk.ToString()); 
                                }
                                else
                                {
                                    isWriteCoefCOk = false;
                                    logoStr += logger.AdapterLogString(3, "isWriteCoefCOk:" + isWriteCoefCOk.ToString()); 
                                }
                                #endregion
                                #region W&R Apddaccoefb
                               isWriteCoefBOk= dut.SetBiasdaccoefb(apdPowerCoefB.ToString());
                               if (isWriteCoefBOk)
                                {
                                    isWriteCoefBOk = true;
                                    logoStr += logger.AdapterLogString(0, "isWriteCoefBOk:" + isWriteCoefBOk.ToString()); 
                                }
                                else
                                {
                                    isWriteCoefBOk = false;
                                    logoStr += logger.AdapterLogString(3, "isWriteCoefBOk:" + isWriteCoefBOk.ToString()); 
                                }
                                #endregion
                               #region W&R Apddaccoefa
                               isWriteCoefAOk = dut.SetBiasdaccoefa(apdPowerCoefA.ToString());

                               if (isWriteCoefAOk)
                               {
                                   isWriteCoefAOk = true;
                                   logoStr += logger.AdapterLogString(1, "isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                               }
                               else
                               {
                                   isWriteCoefAOk = false;
                                   logoStr += logger.AdapterLogString(3, "isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                               }
                               #endregion
                               if (isWriteCoefBOk & isWriteCoefCOk & isWriteCoefAOk)
                                {
                                    isCalApdPowerOk = true;
                                    logoStr += logger.AdapterLogString(1, "isCalApdPowerOk:" + isCalApdPowerOk.ToString()); 
                                }
                                else
                                {
                                    isCalApdPowerOk = false;
                                    logoStr += logger.AdapterLogString(3, "isCalApdPowerOk:" + isCalApdPowerOk.ToString()); 
                                }
                            }
                            
                            
                        }
                    }
               
#endregion
                AnalysisOutputParameters(outputParameters);
                logger.FlushLogBuffer();
                return isApdAdjustOK;
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!"); 
                isApdAdjustOK = false;
                logger.FlushLogBuffer();
                return isApdAdjustOK;
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
                    if (algorithm.FindFileName(InformationList,inPutParametersNameArray[i].ToString(),out index)==false)
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
                        if (algorithm.FindFileName(InformationList, "AUTOTUNE", out index))
                        {
                            adjustAPDStruct.AutoTune = Convert.ToBoolean(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "APDCALPOINT(DBM)", out index))
                        {
                            adjustAPDStruct.ApdCalPoint = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                        }
                        if (algorithm.FindFileName(InformationList, "ARRAYLISTAPDBIASPOINTS(V)", out index))
                        {
                            adjustAPDStruct.ArrayListApdBiasPoints = ArrayList.Adapter(InformationList[index].DefaultValue.Split(new char[] { ',' }));                            
                            

                        }
                        if (algorithm.FindFileName(InformationList, "APDBIASSTEP(V)", out index))
                        {
                            adjustAPDStruct.ApdBiasStep = Convert.ToByte(InformationList[index].DefaultValue);                         
                          
                        }
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPID", out index))
                        {
                            adjustAPDStruct.is1Stor2StorPid = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                   
                    }

                }
                logoStr += logger.AdapterLogString(0, "OK!");  
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
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTAPDCOEF", out index))
                    {
                        InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(apdCoefArray, ",");
                        
                    }
                }                   
                return true;
            }
                    
        }
        protected UInt32 EnumMethod(UInt32[] inPutArray, DUT dut, ErrorDetector ed, byte substep, out ArrayList berArray, out ArrayList apdDacArray, out double minBer)
        {
            berArray = new ArrayList();
            apdDacArray = new ArrayList();
            UInt32 minimumDAC;
            for (byte i = 0; i < inPutArray.Length; i++)
            {
                // add write APD
                dut.WriteAPDDac(inPutArray[i]);
                apdDacArray.Add(inPutArray[i]);
                ed.GetErrorRate();
                berArray.Add(ed.GetErrorRate());

            }
            minBer = -1;
            byte minIndex = 0;
            #region Select minimum value
            minBer = algorithm.SelectMinValue(berArray, out minIndex);
            minimumDAC = inPutArray[minIndex];
            #endregion

            #region Fine Tune Target Value
            dut.WriteAPDDac(algorithm.Uint16DataConvertoBytes((UInt16)(inPutArray[minIndex] + substep)));

            double tempBer1 = ed.GetErrorRate();
            dut.WriteAPDDac(algorithm.Uint16DataConvertoBytes((UInt16)(inPutArray[minIndex] - substep)));
            double tempBer2 = ed.GetErrorRate();
            double mintempBer = Math.Min(tempBer1, tempBer2);
            if (mintempBer == tempBer1)
            {
                if (minBer > tempBer1)
                {
                    minimumDAC = Convert.ToUInt16(minimumDAC + substep);
                    apdDacArray.Add(minimumDAC);
                    dut.WriteAPDDac(minimumDAC);
                    minBer = ed.GetErrorRate();
                    berArray.Add(minBer);
                }

            }
            else
            {

                if (minBer > tempBer2)
                {
                    minimumDAC = Convert.ToUInt16(minimumDAC - substep);
                    apdDacArray.Add(minimumDAC);
                    dut.WriteAPDDac(minimumDAC);
                    minBer = ed.GetErrorRate();
                    berArray.Add(minBer);
                }
            }


            #endregion
            return minimumDAC;

        }
       
#endregion
    }
}
