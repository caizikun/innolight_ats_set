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
    public enum AdjustAPDSpecs : byte
    {
        Csen
    }
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
       // procdata
        private double targetAttValue;
        private double startvalue = -25;
        private UInt32 targetApd;
        ArrayList tempBerArray = new ArrayList();
        ArrayList tempApdDacArray = new ArrayList();
        // procdata
        private bool isWriteCoefCOk = false;
        private bool isWriteCoefBOk = false;
        private bool isWriteCoefAOk = false;
        private SortedList<byte, string> SpecNameArray = new SortedList<byte, string>();
       // equipments
        ErrorDetector tempED;
        Attennuator tempAtt;
        Powersupply tempps;
#endregion
#region Method
        public AdjustAPD(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;  
            adjustAPDtValueRecordsStruct.Clear();
            tempratureADCArrayList.Clear();
            SpecNameArray.Clear();
            // input parameters
            inPutParametersNameArray.Clear();

            inPutParametersNameArray.Add("ATTSTEP");
            inPutParametersNameArray.Add("SETPOINTS");
            inPutParametersNameArray.Add("TUNESTEP");
                    
            //...
            SpecNameArray.Add((byte)AdjustAPDSpecs.Csen, "Csen(dBm)");
           
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
                 tempED = (ErrorDetector)selectedEquipList["ERRORDETE"];
                 tempAtt = (Attennuator)selectedEquipList["ATTEN"];
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempED != null && tempAtt != null && tempps != null)
                {
                    isOK = true;

                }
                else
                {
                    if (tempED == null)
                     {
                         logoStr += logger.AdapterLogString(3, "ERRORDETE =NULL");
                     }
                    if (tempAtt == null)
                    {
                        logoStr += logger.AdapterLogString(3, "ATTEN =NULL");
                    }
                    if (tempps == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
                }
                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);
                }                
                return isOK;
            }
        }

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            GenerateSpecList(SpecNameArray);
            AddCurrentTemprature();
            if (AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }
            if (!LoadPNSpec())
            {
                OutPutandFlushLog();
                return false;
            }            
            if (tempAtt != null && tempAtt != null &&tempps != null)
            {
               // close apc 
               
                {
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODOFF));
                }               
               // close apc
                ArrayList serchTargetAtts = new ArrayList();
                ArrayList serchTargetBers = new ArrayList();
                serchTargetAtts.Clear();
                serchTargetBers.Clear();               
                if (!SearchTargetPoint(tempAtt, tempED, out serchTargetAtts, out serchTargetBers,out targetAttValue ))
                {
                    OutPutandFlushLog();
                    return true;
                }
                      
                UInt32[] tempApdArray = new UInt32[adjustAPDStruct.ArrayListApdBiasPoints.Count];
                for (byte i = 0; i < adjustAPDStruct.ArrayListApdBiasPoints.Count; i++)
                {
                    tempApdArray[i] = Convert.ToUInt32(adjustAPDStruct.ArrayListApdBiasPoints[i].ToString());
                }
                logoStr += logger.AdapterLogString(0, "Step4...StartCalApd");
               
                tempBerArray.Clear();
                tempApdDacArray.Clear();
                double minBer = -1;
                targetApd = EnumMethod(tempApdArray, dut, tempED, adjustAPDStruct.ApdBiasStep, out tempBerArray, out tempApdDacArray, out minBer);
              
               minBer = Math.Round(minBer, 5);
               for (byte i = 0; i < Math.Min(tempBerArray.Count, tempApdDacArray.Count); i++)
               {
                   logoStr += logger.AdapterLogString(1,  GlobalParameters.CurrentChannel.ToString());
                   logoStr += logger.AdapterLogString(1,  tempApdDacArray[i].ToString() + "  " + tempApdDacArray[i].ToString());                   
               }
               logoStr += logger.AdapterLogString(1, "minApdDac=" + targetApd.ToString() + "  minBer=" + minBer.ToString());
               isApdAdjustOK = true;
              
               CollectCurvingParameters();
               if (!CurveAPDandWriteCoefs())
               {
                   OutPutandFlushLog();
               }
               OutPutandFlushLog();
               return true;
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!"); 
                isApdAdjustOK = false;               
                OutPutandFlushLog();
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
                bool isParametersComplete = true;
               
                if (isParametersComplete)
                {
                   
                    //for (byte i = 0; i < InformationList.Length; i++)
                    {

                        if (algorithm.FindFileName(InformationList, "ATTSTEP", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (temp<=0)
                            {
                                adjustAPDStruct.ApdCalAttStep = 0.5;
                            } 
                            else
                            {
                                adjustAPDStruct.ApdCalAttStep = temp;
                            }
                            
                           
                        }
                        if (algorithm.FindFileName(InformationList, "SETPOINTS", out index))
                        {                             
                            char[] tempCharArray = new char[] { ',' };
                            ArrayList tempAL = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAL== null)
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is null");
                                return false;
                            } 
                            else
                            {
                                adjustAPDStruct.ArrayListApdBiasPoints = tempAL;
                            }
                        }
                        if (algorithm.FindFileName(InformationList, "TUNESTEP", out index))
                        {
                            double Temp = Convert.ToDouble(InformationList[index].DefaultValue);
                            if (Temp<=0)
                            {
                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                                return false;
                            } 
                            else
                            {
                                adjustAPDStruct.ApdBiasStep = Convert.ToByte(Temp);                         
                          
                            }
                           
                        }
                        
                   
                    }

                }
                logoStr += logger.AdapterLogString(0, "OK!");  
                return true;
            }

        }
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {
            try
            {
                procData = new TestModeEquipmentParameters[5];
                procData[0].FiledName = "BASERXPOWER";
                procData[0].DefaultValue = Convert.ToString(targetAttValue);
                procData[1].FiledName = "TARGETAPDDAC";
                procData[1].DefaultValue = targetApd.ToString();
                procData[2].FiledName = "TEMPADC";
                procData[2].DefaultValue = tempratureADCArray[Convert.ToString(GlobalParameters.CurrentTemp)];
                procData[3].FiledName = "PROCAPDDACARRAY";
                procData[3].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(tempBerArray, ",");
                procData[4].FiledName = "PROCBERARRAY";
                procData[4].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(tempApdDacArray, ",");
                return true;

            }
            catch (System.Exception ex)
            {
                throw ex;
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
        protected bool SerchErrPoint()
        {
            try
            {
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected bool SearchTargetPoint( Attennuator tempAtt, ErrorDetector tempED, out ArrayList serchAttPoints, out ArrayList serchBerPoints,out double target)
        {
           ArrayList serchAttPointsTemp=new ArrayList();
            serchAttPointsTemp.Clear();
            serchAttPoints =serchAttPointsTemp;
            serchBerPoints = serchAttPointsTemp;           
            try
            {
                
           
                double currentCense = 0;
                byte count = 0;
                serchAttPoints = new ArrayList();
                serchBerPoints = new ArrayList();
                serchAttPoints.Clear();
                serchBerPoints.Clear();
                while (count <= 5)
                {

                    tempAtt.AttnValue(startvalue.ToString(), 0);
                    serchAttPoints.Add(startvalue);
                    currentCense = tempED.RapidErrorRate();
                    serchBerPoints.Add(currentCense);
                    if (currentCense <= 0)
                    {
                        startvalue = startvalue - adjustAPDStruct.ApdCalAttStep;
                        count++;
                    }
                    else
                    {
                        target = startvalue;
                        return true;
                    }
                        
                }
                target = -10000;
                return false;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           

        }        
        protected bool LoadPNSpec()
        {
            
            try
            {
                if (algorithm.FindDataInDataTable(specParameters, SpecTableStructArray) == null)
                {
                    return false;
                }
                
                {                

                    startvalue = SpecTableStructArray[(byte)AdjustAPDSpecs.Csen].TypicalValue;
                }
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
                AnalysisOutputProcData(procData);
                logger.FlushLogBuffer();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected void AddCurrentTemprature()
        {
            try
            {
                #region  CheckTempChange
                if (!tempratureADCArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step4...TempChanged Read tempratureADC");
                    logoStr += logger.AdapterLogString(1, "realtemprature=" + GlobalParameters.CurrentTemp.ToString());

                    UInt16 tempratureADC;
                    dut.ReadTempADC(out tempratureADC, 1);
                    logoStr += logger.AdapterLogString(1, "tempratureADC=" + tempratureADC.ToString());
                    tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper(), tempratureADC.ToString().Trim());
                    tempratureADCArrayList.Add(tempratureADC);
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        protected void CollectCurvingParameters()
        {
            try
            {
                #region  add current channel
                if (!adjustAPDtValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
                {
                    logoStr += logger.AdapterLogString(0, "Step6...add current channel records");
                    logoStr += logger.AdapterLogString(1, "GlobalParameters.CurrentChannel=" + GlobalParameters.CurrentChannel.ToString());
                    adjustAPDtValueRecordsStruct.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), new ArrayList());
                    adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(targetApd);

                }
                else
                {
                    adjustAPDtValueRecordsStruct[GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()].Add(targetApd);
                }


                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        protected bool CurveAPDandWriteCoefs()
        {
            try
            {
                #region  CurveCoef

                if (adjustAPDtValueRecordsStruct.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
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
                        double[] coefArray = algorithm.MultiLine(tempTempAdcArray, tempAPDDacArray, tempratureADCArray.Count, 2);
                        apdPowerCoefC = (float)coefArray[0];
                        apdPowerCoefB = (float)coefArray[1];
                        apdPowerCoefA = (float)coefArray[2];
                        apdCoefArray = ArrayList.Adapter(coefArray);
                        apdCoefArray.Reverse();
                        for (byte i = 0; i < apdCoefArray.Count; i++)
                        {
                            logoStr += logger.AdapterLogString(1, "apdCoefArray[" + i.ToString() + "]=" + apdCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.FloatToIEE754(apdCoefArray[i])));
                        }
                        logoStr += logger.AdapterLogString(0, "Step9...WriteCoef");

                        #region W&R Apddaccoefc
                        isWriteCoefCOk = dut.SetBiasdaccoefc(apdPowerCoefC.ToString());
                        if (isWriteCoefCOk)
                        {                           
                            logoStr += logger.AdapterLogString(1, "isWriteCoefCOk:" + isWriteCoefCOk.ToString());

                        }
                        else
                        {
                           
                            logoStr += logger.AdapterLogString(3, "isWriteCoefCOk:" + isWriteCoefCOk.ToString());
                        }
                        #endregion
                        #region W&R Apddaccoefb
                        isWriteCoefBOk = dut.SetBiasdaccoefb(apdPowerCoefB.ToString());
                        if (isWriteCoefBOk)
                        {
                          
                            logoStr += logger.AdapterLogString(1, "isWriteCoefBOk:" + isWriteCoefBOk.ToString());

                        }
                        else
                        {
                            
                            logoStr += logger.AdapterLogString(3, "isWriteCoefBOk:" + isWriteCoefBOk.ToString());
                        }
                        #endregion
                        #region W&R Apddaccoefa
                        isWriteCoefAOk = dut.SetBiasdaccoefa(apdPowerCoefA.ToString());

                        if (isWriteCoefAOk)
                        {                           
                            logoStr += logger.AdapterLogString(1, "isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                        }
                        else
                        {                           
                            logoStr += logger.AdapterLogString(3, "isWriteCoefAOk:" + isWriteCoefAOk.ToString());
                        }
                        #endregion
                        if (isWriteCoefAOk & isWriteCoefBOk & isWriteCoefCOk)
                        {                            
                            logoStr += logger.AdapterLogString(1, "isCalApdPowerOk:" + true.ToString());
                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "isCalApdPowerOk:" + false.ToString());
                            return false;
                        }
                    }
                }

                #endregion
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
#endregion
    }
}
