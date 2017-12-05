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
   public class CalVccDmi : TestModelBase
    {
#region Attribute
        private CalVccDmiStruct calVccDmiStruct = new CalVccDmiStruct();      
        private float vccDmiCoefA;
        private float vccDmiCoefB;
        private float vccDmiCoefC;
        private ArrayList vccDmiCoefArray = new ArrayList();
        bool isCalVccDmiOk = false;
        private double[,] vccAdcArray;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private byte calledCount;
        
#endregion
#region Method
        public CalVccDmi(DUT inPuDut, logManager logmanager)
        {
           calledCount = 0;
           logger = logmanager;
           logoStr = null;
           dut = inPuDut;           
           inPutParametersNameArray.Clear();
           inPutParametersNameArray.Add("ARRAYLISTVCC(V)");
           inPutParametersNameArray.Add("1STOR2STORPID");           
           inPutParametersNameArray.Add("GENERALVCC(V)");
          
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
                        isOK = true;
                    }
                   
                }
                if (selectedEquipList["POWERSUPPLY"] != null)
                {
                    isOK = true;
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
            calledCount++;
            if (calledCount>1)
            {
                logger.AdapterLogString(0, "calledCount>1");
                return true;
            }
            if (AnalysisInputParameters(inputParameters)==false)
            {
                return false;
            }
            if (selectedEquipList["POWERSUPPLY"] != null && selectedEquipList["DUT"] != null)
            {
                bool isWriteCoefCOk = false;
                bool isWriteCoefBOk = false;
                bool isWriteCoefAOk = false;

                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                // close apc 
            
                // close apc
                vccAdcArray = new double[GlobalParameters.TotalVccCount,calVccDmiStruct.ArrayListVcc.Count];
                ReadVccADCTM(dut, tempps);
                logoStr += logger.AdapterLogString(0,  "Step3...Start Fitting Curve");
                double[] tempVccArray = new double[calVccDmiStruct.ArrayListVcc.Count];
                for (byte i = 0; i < calVccDmiStruct.ArrayListVcc.Count;i++ )
                {
                    tempVccArray[i] = Convert.ToDouble(calVccDmiStruct.ArrayListVcc[i].ToString());
                }
                tempps.ConfigVoltageCurrent(Convert.ToString(calVccDmiStruct.generalVcc));
                if (calVccDmiStruct.is1Stor2StorPid==1)
                {
                    for (byte i = 0; i < tempVccArray.Length; i++)
                    {
                        tempVccArray[i] = tempVccArray[i] * 10000;
                    }
                    for (byte i = 0; i < GlobalParameters.TotalVccCount;i++)
                    {
                        double[]tempAdc=new double[calVccDmiStruct.ArrayListVcc.Count];
                        for (byte j=0;j<calVccDmiStruct.ArrayListVcc.Count;j++)
                        {
                            tempAdc[j]=vccAdcArray[i,j];
                        }
                        double[] coefArray = algorithm.MultiLine(tempAdc, tempVccArray, calVccDmiStruct.ArrayListVcc.Count, 1);
                        vccDmiCoefC = (float)coefArray[0];
                        vccDmiCoefB = (float)coefArray[1];
                        vccDmiCoefB = vccDmiCoefB * 256;
                        double[] tempCoefArray = new double[2] { vccDmiCoefC, vccDmiCoefB };
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
                            isWriteCoefCOk = true;
                            logoStr += logger.AdapterLogString(1, "WritevccDmiCoefC:" + isWriteCoefCOk.ToString());

                        }
                        else
                        {
                            isWriteCoefCOk = false;
                            logoStr += logger.AdapterLogString(3, "WritevccDmiCoefC:" + isWriteCoefCOk.ToString());

                        }
                        #endregion
                        #region W&R Vcccoefb
                        isWriteCoefBOk = dut.SetVcccoefb(vccDmiCoefB.ToString(), (byte)(i + 1));

                        if (isWriteCoefBOk)
                        {
                            isWriteCoefBOk = true;
                            logoStr += logger.AdapterLogString(1, "WritevccDmiCoefB:" + isWriteCoefBOk.ToString());
                        }

                        else
                        {
                            isWriteCoefBOk = false;
                            logoStr += logger.AdapterLogString(3, "WritevccDmiCoefB:" + isWriteCoefBOk.ToString());
                        }
                        #endregion
                        if (isWriteCoefBOk & isWriteCoefCOk)
                        {
                            isCalVccDmiOk = true;

                            logoStr += logger.AdapterLogString(1, "isCalVccDmiOk:" + isCalVccDmiOk.ToString());
                        }
                        else
                        {
                            isCalVccDmiOk = false;
                            logoStr += logger.AdapterLogString(3, "isCalVccDmiOk:" + isCalVccDmiOk.ToString());
                        }
                    }
                   
                 
                }
                else if (calVccDmiStruct.is1Stor2StorPid == 2)
                {
                    for (byte i = 0; i < tempVccArray.Length; i++)
                    {
                        tempVccArray[i] = tempVccArray[i] * 10000;
                    }
                    for (byte i = 0; i < GlobalParameters.TotalVccCount; i++)
                    {
                        double[] tempAdc = new double[calVccDmiStruct.ArrayListVcc.Count];
                       
                        for (byte j = 0; j < calVccDmiStruct.ArrayListVcc.Count; j++)
                        {
                            tempAdc[j] = vccAdcArray[i, j];
                        }
                        double[] coefArray = algorithm.MultiLine(tempAdc, tempVccArray, calVccDmiStruct.ArrayListVcc.Count, 2);
                        vccDmiCoefC = (float)coefArray[0];
                        vccDmiCoefB = (float)coefArray[1];
                        vccDmiCoefA = (float)coefArray[2];

                        vccDmiCoefArray = ArrayList.Adapter(coefArray);
                        vccDmiCoefArray.Reverse();
                        for (byte k = 0; k< vccDmiCoefArray.Count; k++)
                        {
                            logoStr += logger.AdapterLogString(1, "vccDmiCoefArray[" + k.ToString() + "]=" + vccDmiCoefArray[k].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(vccDmiCoefArray[k])));
                        }
                        logoStr += logger.AdapterLogString(0, "Step4...WriteCoef");
                        #region W&R Vcccoefc
                        isWriteCoefCOk = dut.SetVcccoefc(vccDmiCoefC.ToString());
                        if (isWriteCoefCOk)
                        {
                            isWriteCoefCOk = true;
                            logoStr += logger.AdapterLogString(1, "WritevccDmiCoefC:" + isWriteCoefCOk.ToString());

                        }
                        else
                        {
                            isWriteCoefCOk = false;
                            logoStr += logger.AdapterLogString(3, "WritevccDmiCoefC:" + isWriteCoefCOk.ToString());
                        }
                        #endregion
                        #region W&R Vcccoefb
                        isWriteCoefBOk = dut.SetVcccoefb(vccDmiCoefB.ToString(), (byte)(i + 1));

                        if (isWriteCoefBOk)
                        {
                            isWriteCoefBOk = true;

                            logoStr += logger.AdapterLogString(1, "WritevccDmiCoefB:" + isWriteCoefBOk.ToString());
                        }
                        else
                        {
                            isWriteCoefBOk = false;
                            logoStr += logger.AdapterLogString(3, "WritevccDmiCoefB:" + isWriteCoefBOk.ToString());
                        }
                        #endregion
                        #region W&R Vcccoefa
                        //isWriteCoefAOk = dut.SetVcccoefa(vccDmiCoefA.ToString(), i + 1);
                        if (isWriteCoefAOk)
                        {
                            isWriteCoefAOk = true;
                            logoStr += logger.AdapterLogString(1, "WritevccDmiCoefA:" + isWriteCoefAOk.ToString());

                        }
                        else
                        {
                            isWriteCoefAOk = false;
                            logoStr += logger.AdapterLogString(3, "WritevccDmiCoefA:" + isWriteCoefAOk.ToString());
                        }
                        #endregion
                        if (isWriteCoefBOk & isWriteCoefCOk & isWriteCoefAOk)
                        {
                            isCalVccDmiOk = true;
                            logoStr += logger.AdapterLogString(1, "isCalVccDmiOk:" + isCalVccDmiOk.ToString());
                        }
                        else
                        {
                            isCalVccDmiOk = false;
                            logoStr += logger.AdapterLogString(3, "isCalVccDmiOk:" + isCalVccDmiOk.ToString());
                        }
                    }

                  
                }

              
            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                isCalVccDmiOk = false;
                logger.FlushLogBuffer();
                return isCalVccDmiOk;
            }
            AnalysisOutputParameters(outputParameters);
            logger.FlushLogBuffer();
            return isCalVccDmiOk;
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
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTDMIVCCCOEF", out index))
                    {
                        InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(vccDmiCoefArray, ",");
                        
                    }

                }
                
                return true;
            }
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

                        if (algorithm.FindFileName(InformationList, "ARRAYLISTVCC(V)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' }; calVccDmiStruct.ArrayListVcc = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPID", out index))
                        {
                            calVccDmiStruct.is1Stor2StorPid = Convert.ToByte(InformationList[index].DefaultValue);
                           
                        }
                        
                        if (algorithm.FindFileName(InformationList, "GENERALVCC(V)", out index))
                        {
                            calVccDmiStruct.generalVcc = Convert.ToDouble(InformationList[index].DefaultValue);
                           
                        }
                    }

                }
                logoStr += logger.AdapterLogString(1,"OK!");
                return true;
            }
        }
        protected bool ReadVccADCTM(DUT inputDut,Powersupply inputPowerSupply)
        {
           
            for (byte i = 0; i < GlobalParameters.TotalVccCount; i++)
            {
                for (byte j=0;j<calVccDmiStruct.ArrayListVcc.Count;j++)
                {
                    inputPowerSupply.ConfigVoltageCurrent(Convert.ToString(Convert.ToDouble(calVccDmiStruct.ArrayListVcc[j])));
                    UInt16 temp;
                    dut.ReadVccADC(out temp, (byte)(i + 1));
                    vccAdcArray[i,j] = Convert.ToDouble(temp);
                }
            }
            return true;
        }
#endregion
    }
}
