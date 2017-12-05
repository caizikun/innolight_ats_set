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
   public class AdjustVccDmi: TestModelBase
    {
#region Attribute
       private CalVccDmiStruct calVccDmiStruct = new CalVccDmiStruct();      
        private float vccDmiCoefA;
        private float vccDmiCoefB;
        private float vccDmiCoefC;
        private ArrayList vccDmiCoefArray = new ArrayList();
        bool isCalVccDmiOk = false;
        private double[,] vccAdcArray;
        private double[] tempVccArray;
        private ArrayList allVccAdcArray = new ArrayList();
        private ArrayList inPutParametersNameArray = new ArrayList();
        private byte calledCount;
        private bool isWriteCoefCOk = false;
        private bool isWriteCoefBOk = false;
        private bool isWriteCoefAOk = false;
       //equipments
       private Powersupply tempps;
#endregion
#region Method
        public AdjustVccDmi(DUT inPuDut, logManager logmanager)
        {
            calledCount = 0;
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;           
           inPutParametersNameArray.Clear();
           allVccAdcArray.Clear();
           inPutParametersNameArray.Add("VCCARRAYLIST(V)");
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
                        isOK = true;
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
                }

                return isOK;
            }
        }

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";
            calledCount++;
            if (calledCount > 1)
            {
                logger.AdapterLogString(0, "calledCount>1");
                OutPutandFlushLog();
                return true;
            }
            if (AnalysisInputParameters(inputParameters)==false)
            {
                OutPutandFlushLog();
                return false;
            }
            
            if (tempps != null)
            {
                if (GlobalParameters.TotalVccCount<=0)
                {
                    vccAdcArray = new double[1, calVccDmiStruct.ArrayListVcc.Count];
                } 
                else
                {
                    vccAdcArray = new double[GlobalParameters.TotalVccCount, calVccDmiStruct.ArrayListVcc.Count];
                }
                
                ReadVccADC(dut, tempps);
                logoStr += logger.AdapterLogString(0,  "Step3...Start Fitting Curve");
                tempVccArray = new double[calVccDmiStruct.ArrayListVcc.Count];
                for (byte i = 0; i < calVccDmiStruct.ArrayListVcc.Count;i++ )
                {
                    tempVccArray[i] = Convert.ToDouble(calVccDmiStruct.ArrayListVcc[i].ToString());
                }
                tempps.ConfigVoltageCurrent(Convert.ToString(GlobalParameters.CurrentVcc));

                if (!CurveVccDMIandWriteCoefs())
                {
                    OutPutandFlushLog();
                    return false;
                }
                tempps.OutPutSwitch(false);
                tempps.OutPutSwitch(true);
                dut.FullFunctionEnable();
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                isCalVccDmiOk = false;                
                OutPutandFlushLog();
                return isCalVccDmiOk;
            }
            OutPutandFlushLog();
            return true;
        }        
        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
           
            logoStr += logger.AdapterLogString(0,"Step1...Check InputParameters");
           
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

                        if (algorithm.FindFileName(InformationList, "VCCARRAYLIST(V)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' }; 
                            ArrayList tempAR= algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            if (tempAR==null)
                            {

                                logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is null!");
                                return false;
                            } 
                            else
                            {
                                calVccDmiStruct.ArrayListVcc = tempAR;
                            }
                        }
                      
                        
                    }

                }
                logoStr += logger.AdapterLogString(1,"OK!");
                return true;
            }
        }
        protected bool ReadVccADC(DUT inputDut,Powersupply inputPowerSupply)
        {
            byte tempCount;
            if (GlobalParameters.TotalVccCount<=0)
            {
                tempCount = 1;
            } 
            else
            {
                tempCount = GlobalParameters.TotalVccCount;
            }

            for (byte i = 0; i < tempCount; i++)
            {
                for (byte j=0;j<calVccDmiStruct.ArrayListVcc.Count;j++)
                {
                    inputPowerSupply.ConfigVoltageCurrent(calVccDmiStruct.ArrayListVcc[j].ToString(),0);
                    UInt16 temp;
                    Thread.Sleep(1000);
                    dut.ReadVccADC(out temp, (byte)(i + 1));
                    vccAdcArray[i,j] = Convert.ToDouble(temp);
                }
            }
            return true;
        }
        protected bool CurveVccDMIandWriteCoefs()
        {
            byte tempCOUNT;
            if (GlobalParameters.TotalVccCount<=0)
            {
                tempCOUNT = 1;
            } 
            else
            {
                tempCOUNT = GlobalParameters.TotalVccCount;
            }
            try
            {
                {
                    for (byte i = 0; i < tempVccArray.Length; i++)
                    {
                        tempVccArray[i] = tempVccArray[i] * 10000;
                    }
                    for (byte i = 0; i < tempCOUNT; i++)
                    {
                        double[] tempAdc = new double[calVccDmiStruct.ArrayListVcc.Count];

                        for (byte j = 0; j < calVccDmiStruct.ArrayListVcc.Count; j++)
                        {
                            tempAdc[j] = vccAdcArray[i, j];
                            allVccAdcArray.Add(vccAdcArray[i, j]);
                        }
                        double[] coefArray = algorithm.MultiLine(tempAdc, tempVccArray, calVccDmiStruct.ArrayListVcc.Count, 1);
                        vccDmiCoefC = (float)coefArray[0];
                        vccDmiCoefB = (float)coefArray[1];
                        //vccDmiCoefA = (float)coefArray[2];

                        vccDmiCoefArray = ArrayList.Adapter(coefArray);
                        vccDmiCoefArray.Reverse();
                        for (byte k = 0; k < vccDmiCoefArray.Count; k++)
                        {
                            logoStr += logger.AdapterLogString(1, "vccDmiCoefArray[" + k.ToString() + "]=" + vccDmiCoefArray[k].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(vccDmiCoefArray[k])));
                        }
                        logoStr += logger.AdapterLogString(0, "Step4...WriteCoef");
                        #region W&R Vcccoefc
                        isWriteCoefCOk = dut.SetVcccoefc(vccDmiCoefC.ToString(), (byte)(i + 1));
                        if (isWriteCoefCOk)
                        {
                           
                            logoStr += logger.AdapterLogString(1, "WritevccDmiCoefC:" + isWriteCoefCOk.ToString());

                        }
                        else
                        {
                           
                            logoStr += logger.AdapterLogString(3, "WritevccDmiCoefC:" + isWriteCoefCOk.ToString());
                        }
                        #endregion
                        #region W&R Vcccoefb
                        isWriteCoefBOk = dut.SetVcccoefb(vccDmiCoefB.ToString(), (byte)(i + 1));

                        if (isWriteCoefBOk)
                        {
                            logoStr += logger.AdapterLogString(1, "WritevccDmiCoefB:" + isWriteCoefBOk.ToString());
                        }
                        else
                        {
                           
                            logoStr += logger.AdapterLogString(3, "WritevccDmiCoefB:" + isWriteCoefBOk.ToString());
                        }
                        #endregion
                        //#region W&R Vcccoefa
                        ////isWriteCoefAOk = dut.SetVcccoefa(vccDmiCoefA.ToString(), i + 1);
                        //if (isWriteCoefAOk)
                        //{                          
                        //    isWriteCoefAOk = true;
                        //    logoStr += logger.AdapterLogString(1, "WritevccDmiCoefA:" + isWriteCoefAOk.ToString());

                        //}
                        //else
                        //{
                           
                        //    logoStr += logger.AdapterLogString(3, "WritevccDmiCoefA:" + isWriteCoefAOk.ToString());
                        //}
                        //#endregion
                        if (isWriteCoefBOk & isWriteCoefCOk)
                        {
                            logoStr += logger.AdapterLogString(1, "isCalVccDmiOk:" + true.ToString());
                        }
                        else
                        {
                            logoStr += logger.AdapterLogString(3, "isCalVccDmiOk:" + false.ToString());
                            return false;
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
        protected override bool AnalysisOutputProcData(TestModeEquipmentParameters[] InformationList)
        {

            try
            {
                procData = new TestModeEquipmentParameters[2];
                procData[0].FiledName = "VCCADARRAY";
                procData[0].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(allVccAdcArray, ",");
                procData[1].FiledName = "REALVCCARRAY";
                procData[1].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(calVccDmiStruct.ArrayListVcc, ",");
          

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
