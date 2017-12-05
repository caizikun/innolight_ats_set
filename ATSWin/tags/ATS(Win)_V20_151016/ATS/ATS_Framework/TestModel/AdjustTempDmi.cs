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
   public class AdjustTempDmi : TestModelBase
    {
#region Attribute        
        private float tempDmiCoefA;
        private float tempDmiCoefB;
        private float tempDmiCoefC;
        private ArrayList tempDmiCoefArray = new ArrayList();
        private SortedList<string,string> tempratureADCArray = new SortedList<string,string>();
        private SortedList<string, string> realTempratureArray = new SortedList<string, string>();
        private ArrayList allTempADCArray = new ArrayList();
        private bool isCalTempDmiOk;
        private bool isWriteCoefCOk = false;
        private bool isWriteCoefBOk = false;
        private bool isWriteCoefAOk = false;       
        private ArrayList inPutParametersNameArray = new ArrayList();
       private Powersupply tempps;
#endregion
#region  Method
        public AdjustTempDmi(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;         
            inPutParametersNameArray.Clear();
            allTempADCArray.Clear();            
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
           
            if (tempps != null)
            {
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
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                procData = new TestModeEquipmentParameters[2];
                procData[0].FiledName = "TEMPADC";
                procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(allTempADCArray, ",");
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
                    logoStr += logger.AdapterLogString(0, "Step3...TempChanged Read tempratureADC");
                    for (byte i = 0; i < tempcount; i++)
                    {
                        realTempratureArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper() + i.ToString().ToUpper().Trim(), GlobalParameters.CurrentTemp.ToString().Trim());
                        logoStr += logger.AdapterLogString(1, "realtemprature=" + i.ToString() + GlobalParameters.CurrentTemp.ToString());
                        UInt16 tempratureADC;
                        dut.ReadTempADC(out tempratureADC, (byte)(i + 1));
                        logoStr += logger.AdapterLogString(1, "tempratureADC" + i.ToString() + tempratureADC.ToString());
                        tempratureADCArray.Add(GlobalParameters.CurrentTemp.ToString().Trim().ToUpper() + i.ToString().ToUpper().Trim(), tempratureADC.ToString().Trim());
                        allTempADCArray.Add(tempratureADC);
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
                   tempCount = Math.Min(tempratureADCArray.Count /1, realTempratureArray.Count /1);
               } 
               else
               {
                   tempCount = Math.Min(tempratureADCArray.Count / GlobalParameters.TotalTempCount, realTempratureArray.Count / GlobalParameters.TotalTempCount);
               }
               double[,] tempTempAdcArray ;
               double[,] tempTempValueArray;
               if (GlobalParameters.TotalTempCount<=0)
               {
                    tempTempAdcArray = new double[1, tempCount];
                   tempTempValueArray = new double[1, tempCount];
               } 
               else
               {
                   tempTempAdcArray = new double[GlobalParameters.TotalTempCount, tempCount];
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
                   for (byte i = 0; i < totalTempCount; i++)
                   {
                       for (byte j = 0; j < tempCount; j++)
                       {
                           tempTempValueArray[i, j] = tempTempValueArray[i, j] * 256;
                       }

                   }
                   double[] adcArray = new double[tempCount];
                   double[] realArray = new double[tempCount];                  
                   {
                       for (byte i = 0; i < totalTempCount; i++)
                       {

                           for (byte j = 0; j < tempCount; j++)
                           {
                               adcArray[j] = tempTempAdcArray[i, j];
                               realArray[j] = tempTempValueArray[i, j];
                           }
                           double[] coefArray = algorithm.MultiLine(adcArray, realArray, tempCount, 1);
                           tempDmiCoefC = (float)coefArray[0];
                           tempDmiCoefB = (float)coefArray[1];
                           //tempDmiCoefA = (float)coefArray[2];

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
                           //#region W&R Tempcoefa
                           ////isWriteCoefAOk = dut.SetTempcoefa(tempDmiCoefA.ToString(), i + 1);
                           //if (isWriteCoefAOk)
                           //{
                           //    logoStr += logger.AdapterLogString(1, "WritetempDmiCoefA:" + isWriteCoefAOk.ToString());

                           //}
                           //else
                           //{
                           //    logoStr += logger.AdapterLogString(3, "WritetempDmiCoefA:" + isWriteCoefAOk.ToString());
                           //}
                           //#endregion

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
