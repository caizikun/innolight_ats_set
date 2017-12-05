using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace ATS_Framework
{
    class AdjustTempDmiByDie : TestModelBase
    {
        private struct InPara
        {
            public ushort DmiCoefB { get; set; }

            public ushort DmiCoefC { get; set; }
        }

        #region Attribute        
        private float tempDmiCoefA;
        private float tempDmiCoefB;
        private float tempDmiCoefC;
        private ArrayList tempDmiCoefArray = new ArrayList();
        private SortedList<string,string> dieTempratureArray = new SortedList<string,string>();
        private SortedList<string, string> realTempratureArray = new SortedList<string, string>();
        private ArrayList allDieTempratureArray = new ArrayList();
        private bool isCalTempDmiOk;
        private bool isWriteCoefCOk = false;
        private bool isWriteCoefBOk = false;
        private bool isWriteCoefAOk = false;       
        private ArrayList inPutParametersNameArray = new ArrayList();
       private Powersupply tempps;
       private InPara myStruct;
#endregion
#region  Method
       public AdjustTempDmiByDie(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;         
            inPutParametersNameArray.Clear();
            allDieTempratureArray.Clear();            
            dieTempratureArray.Clear();
            realTempratureArray.Clear();           
        }

       protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
       {
           try
           {
                logoStr += logger.AdapterLogString(0, "Step1...Check InputParameters");
                              
                int index = -1;
                bool isParaCompleted = true;

                if (isParaCompleted)
                {
                    if (algorithm.FindFileName(InformationList, "DMICOEFB", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                        if (temp < 0)
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                            return false;
                        }
                        else
                        {
                            myStruct.DmiCoefB = (ushort)Convert.ToDouble(temp);
                        }

                    }

                    if (algorithm.FindFileName(InformationList, "DMICOEFC", out index))
                    {
                        double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                        if (temp < 0)
                        {
                            logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                            return false;
                        }
                        else
                        {
                            myStruct.DmiCoefC = (ushort)Convert.ToDouble(temp);
                        }

                    }

                    logoStr += logger.AdapterLogString(1, "OK!");
                    return true;
                }
                return false;
               
           }
           catch (Exception ex)
           {
               logoStr += logger.AdapterLogString(4, "Input parameter error!");
               logoStr += logger.AdapterLogString(4, ex.Message);
               return false;
           }
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
                if (!selectedEquipList.Values[i].OutPutSwitch(true)) return false;
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
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }
                }
                 tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                 if (tempps != null)
                {
                    isOK = true;
                }
                else
                {
                   
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
                    OutPutandFlushLog();
                    return true;
                }
            }

            if (AnalysisInputParameters(inputParameters) == false)
            {
                OutPutandFlushLog();
                return false;
            }
           
            if (tempps != null)
            {
                InitialCoeff();//
                AddCurrentTemprature();
                if (!CurveTempDmiandWriteCoefs())
               {
                   OutPutandFlushLog();
                   tempps.OutPutSwitch(false);
                   tempps.OutPutSwitch(true);
                   dut.FullFunctionEnable();
                   return false;
               }
                tempps.OutPutSwitch(false);
                tempps.OutPutSwitch(true);
                dut.FullFunctionEnable();
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");                
                isCalTempDmiOk = false;            
                OutPutandFlushLog();
                return isCalTempDmiOk;
            }
            OutPutandFlushLog();
            return true;
        }

        private void InitialCoeff()
        {
            byte totalTempCount;
            if (GlobalParameters.TotalTempCount <= 0)
            {
                totalTempCount = 1;
            }
            else
            {
                totalTempCount = GlobalParameters.TotalTempCount;
            }

            for (byte i = 0; i < totalTempCount; i++)
            {
                isWriteCoefCOk = dut.SetTempcoefc(myStruct.DmiCoefC.ToString(), (byte)(i + 1));

                if (isWriteCoefCOk)
                {
                    logoStr += logger.AdapterLogString(1, "InitialtempDmiCoefC:" + isWriteCoefCOk.ToString());
                }
                else
                {
                    logoStr += logger.AdapterLogString(3, "InitialtempDmiCoefC:" + isWriteCoefCOk.ToString());

                }

                isWriteCoefBOk = dut.SetTempcoefb(myStruct.DmiCoefB.ToString(), (byte)(i + 1));

                if (isWriteCoefBOk)
                {
                    logoStr += logger.AdapterLogString(1, "InitialtempDmiCoefB:" + isWriteCoefBOk.ToString());
                }
                else
                {

                    logoStr += logger.AdapterLogString(3, "InitialtempDmiCoefB:" + isWriteCoefBOk.ToString());

                }
            }
            tempps.OutPutSwitch(false);
            tempps.OutPutSwitch(true);
            dut.FullFunctionEnable();
        }
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                procData = new TestModeEquipmentParameters[2];
                procData[0].FiledName = "DIETEMP";
                procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(allDieTempratureArray, ",");
                procData[1].FiledName = "REALTEMPARATURE";
                procData[1].DefaultValue = Convert.ToString(GlobalParameters.CurrentTemp);

                return true;

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

                if (!realTempratureArray.ContainsKey(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper()))
                {
                    byte tempcount;
                    if (GlobalParameters.TotalTempCount<=0)
                    {
                        tempcount = 1;
                    } 
                    else
                    {
                        tempcount = GlobalParameters.TotalTempCount;
                    }
                    logoStr += logger.AdapterLogString(0, "Step3...TempChanged Read Die Temp");
                    for (byte i = 0; i < tempcount; i++)
                    {
                        realTempratureArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper() + i.ToString().ToUpper().Trim(), GlobalParameters.CurrentTemp.ToString().Trim());
                        logoStr += logger.AdapterLogString(1, "realtemprature=" + i.ToString() + GlobalParameters.CurrentTemp.ToString());
                        //UInt16 tempratureADC;
                        //dut.ReadTempADC(out tempratureADC, (byte)(i + 1));
                        Thread.Sleep(30000);
                        double dieTemp = dut.ReadDmiTemp();
                        int count = 0;
                        while (count < 10)
                        {
                            Thread.Sleep(20);
                            double value = dut.ReadDmiTemp();
                            if (Math.Abs(value- dieTemp) < 1)
                            {
                                dieTemp = value;
                                break;
                            }
                            count++;                            
                        }
                        

                        
                        logoStr += logger.AdapterLogString(1, "die temp" + i.ToString() + dieTemp.ToString());
                        dieTempratureArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper() + i.ToString().ToUpper().Trim(), dieTemp.ToString().Trim());
                        allDieTempratureArray.Add(dieTemp);
                    }


                }
                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
       protected bool CurveTempDmiandWriteCoefs()
        {
           try
           {
               int tempCount = 0;
               if (GlobalParameters.TotalTempCount<=0)
               {
                   tempCount = Math.Min(dieTempratureArray.Count /1, realTempratureArray.Count /1);
               } 
               else
               {
                   tempCount = Math.Min(dieTempratureArray.Count / GlobalParameters.TotalTempCount, realTempratureArray.Count / GlobalParameters.TotalTempCount);
               }
               double[,] tempdieTempArray;
               double[,] tempTempValueArray;
               if (GlobalParameters.TotalTempCount<=0)
               {
                   tempdieTempArray = new double[1, tempCount];
                   tempTempValueArray = new double[1, tempCount];
               } 
               else
               {
                   tempdieTempArray = new double[GlobalParameters.TotalTempCount, tempCount];
                   tempTempValueArray = new double[GlobalParameters.TotalTempCount, tempCount];
               }
               byte totalTempCount;
               if (GlobalParameters.TotalTempCount<=0)
               {
                   totalTempCount = 1;
               } 
               else
               {
                   totalTempCount = GlobalParameters.TotalTempCount;
               }
               logoStr += logger.AdapterLogString(0, "Step4...Start Fitting Curve");
               {
                   for (byte i = 0; i < totalTempCount; i++)
                   {
                       int tempcount1 = 0;
                       for (byte j = 0; j < Math.Min(realTempratureArray.Count, dieTempratureArray.Count); j++)
                       {
                           int tempstr2 = dieTempratureArray.Keys[j].ToUpper().Length;
                           string tempstring = dieTempratureArray.Keys[j].ToUpper().Substring(dieTempratureArray.Keys[j].ToUpper().Length - 1, 1);
                           string iStr = i.ToString().ToUpper().Trim();
                           if (tempstring == iStr)
                           {
                               tempdieTempArray[i, tempcount1] = double.Parse(dieTempratureArray.Values[j]);
                               tempTempValueArray[i, tempcount1] = double.Parse(realTempratureArray.Values[j]);
                               tempcount1++;
                           }
                       }
                   }
                   for (byte i = 0; i < totalTempCount; i++)
                   {
                       for (byte j = 0; j < tempCount; j++)
                       {
                           tempTempValueArray[i, j] = tempTempValueArray[i, j] * 256;
                       }

                   }
                   double[] dieTempArray = new double[tempCount];
                   double[] realArray = new double[tempCount];                  
                   {
                       for (byte i = 0; i < totalTempCount; i++)
                       {

                           for (byte j = 0; j < tempCount; j++)
                           {
                               dieTempArray[j] = tempdieTempArray[i, j];
                               realArray[j] = tempTempValueArray[i, j];
                           }

                           for (byte k = 0; k < dieTempArray.Length; k++)
                           {
                               logoStr += logger.AdapterLogString(1, "dieTempArray[" + k.ToString() + "]=" + dieTempArray[k].ToString() + " " + "realArray[" + k.ToString() + "]=" + realArray[k].ToString());

                           }
                          
                           double[] coefArray = algorithm.MultiLine(dieTempArray, realArray, tempCount, 1);

                           tempDmiCoefC = Convert.ToSingle(coefArray[0]);
                           tempDmiCoefB = Convert.ToSingle(coefArray[1]);

                           tempDmiCoefArray = ArrayList.Adapter(coefArray);
                           tempDmiCoefArray.Reverse();
                           for (byte k = 0; k < tempDmiCoefArray.Count; k++)
                           {
                              // logoStr += logger.AdapterLogString(1, "tempDmiCoefArray[" + k.ToString() + "]=" + tempDmiCoefArray[k].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(tempDmiCoefArray[k])));
                               logoStr += logger.AdapterLogString(1, "tempDmiCoefArray[" + k.ToString() + "]=" + tempDmiCoefArray[k].ToString() + " " + algorithm.ByteArraytoString(4, ",", algorithm.FloatToIEE754(tempDmiCoefArray[k])));

                           }
                           logoStr += logger.AdapterLogString(0, "Step5...WriteCoef");
                           #region W&R Tempcoefc
                           isWriteCoefCOk = dut.SetTempcoefc(tempDmiCoefC.ToString(),(byte)(i + 1));
                     
                           if (isWriteCoefCOk)
                           {
                               logoStr += logger.AdapterLogString(1, "WritetempDmiCoefC:" + isWriteCoefCOk.ToString());
                           }
                           else
                           {
                               logoStr += logger.AdapterLogString(3, "WritetempDmiCoefC:" + isWriteCoefCOk.ToString());

                           }
                           #endregion
                           #region W&R Tempcoefb
                           isWriteCoefBOk = dut.SetTempcoefb(tempDmiCoefB.ToString(), (byte)(i + 1));
                         
                           if (isWriteCoefBOk)
                           {
                               logoStr += logger.AdapterLogString(1, "WritetempDmiCoefB:" + isWriteCoefBOk.ToString());
                           }
                           else
                           {
                              
                               logoStr += logger.AdapterLogString(3, "WritetempDmiCoefB:" + isWriteCoefBOk.ToString());

                           }
                           #endregion
                       
                           if (isWriteCoefBOk & isWriteCoefCOk)
                           {
                               
                               logoStr += logger.AdapterLogString(1, "isCalTempDmiOk:" + true.ToString());

                           }
                           else
                           {                              
                               logoStr += logger.AdapterLogString(3, "isCalTempDmiOk:" + false.ToString());
                               return false;

                           }

                       }

                   }                  
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
#endregion
    }
}
