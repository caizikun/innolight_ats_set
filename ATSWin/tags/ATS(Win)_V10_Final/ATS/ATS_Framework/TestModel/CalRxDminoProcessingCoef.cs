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
  public  class CalRxDminoProcessingCoef: TestModelBase
    {
#region Attribute
        private byte ExtraOffset = 0;
        private CalRxDmiStruct calRxDmiStruct = new CalRxDmiStruct();
        private float rxDmiCoefA;
        private float rxDmiCoefB;
        private float rxDmiCoefC;        
        private ArrayList rxDmiCoefArray = new ArrayList();
        private bool isCalRxDmiOk;
        private double[] rxPoweruwArray;
        private double[] rxPowerAdcArray;
        private ArrayList inPutParametersNameArray = new ArrayList();
        private SortedList<string, string> channelArray = new SortedList<string, string>();
#endregion
#region Method
        public CalRxDminoProcessingCoef(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;
            inPutParametersNameArray.Clear();
            channelArray.Clear();
            inPutParametersNameArray.Add("ARRAYLISTRXPOWER(DBM)");
            inPutParametersNameArray.Add("1STOR2STORPID");
            inPutParametersNameArray.Add("HASOFFSET");
            inPutParametersNameArray.Add("READRXADCCOUNT");
            inPutParametersNameArray.Add("SLEEPTIME");
            inPutParametersNameArray.Add("EXTRAOFFSET");
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

                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                        isOK = true;
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
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
            if (!channelArray.ContainsKey(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper()))
            {
                channelArray.Add(GlobalParameters.CurrentChannel.ToString().Trim().ToUpper(), GlobalParameters.CurrentChannel.ToString().Trim().ToUpper());
            }
            else
            {
                logger.AdapterLogString(0, "Curren Channel had exist");
                return true;
            }
            if (AnalysisInputParameters(inputParameters) == false)
            {
                logger.FlushLogBuffer();
                return false;
            }
            if (selectedEquipList["ATTEN"] != null && selectedEquipList["DUT"] != null && selectedEquipList["POWERSUPPLY"] != null)
            {
                bool isWriteCoefCOk = false;
                bool isWriteCoefBOk = false;
                bool isWriteCoefAOk = false;
              
                Attennuator tempAtten = (Attennuator)selectedEquipList["ATTEN"];                
                Powersupply tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                // close apc 
               
                // close apc
                if (calRxDmiStruct.HasOffset==true)
                {  
                    UInt16 tempRxADC;
                    byte index=0;
                    tempAtten.Switch(false,1);
                    ArrayList rxADCArray = new ArrayList();
                    rxADCArray.Clear();
                    for (byte i = 0; i < calRxDmiStruct.ReadRxADCCount; i++)
                    {
                        if (dut.ReadRxpADC(out tempRxADC))
                        {
                            rxADCArray.Add(tempRxADC);
                            SetSleep(calRxDmiStruct.SleepTime);
                        }
                        else
                        {
                            MessageBox.Show("ReadRxADCError!");
                        }
                    }
                    for (int i = 0; i < rxADCArray.Count;i++ )
                    {
                        logoStr += logger.AdapterLogString(3, "rxADCArray" +i+"="+rxADCArray[i]);
                   
                    }

                   double tempRxADCd=algorithm.SelectMaxValue(rxADCArray, out index);

                   if (tempRxADCd>255)
                    {
                        logoStr += logger.AdapterLogString(3, "MaxrxADC" + tempRxADCd);

                            MessageBox.Show("RXADC采样异常.....");
                            AnalysisOutputParameters(outputParameters);
                            logger.FlushLogBuffer();
                            return false;
                    }
                   dut.SetRXPadcThreshold((byte)(tempRxADCd + calRxDmiStruct.ExtraOffset));
                    tempAtten.Switch(true,1);                    
                }                
                rxPoweruwArray = new double[calRxDmiStruct.ArrayListRxPower.Count];
                rxPowerAdcArray = new double[calRxDmiStruct.ArrayListRxPower.Count];
                for (byte i = 0; i < calRxDmiStruct.ArrayListRxPower.Count; i++)
                {
                    rxPoweruwArray[i] = algorithm.ChangeDbmtoUw(Convert.ToDouble(calRxDmiStruct.ArrayListRxPower[i]))*10;
                    tempAtten.AttnValue(calRxDmiStruct.ArrayListRxPower[i].ToString(),0);                    
                    UInt16 Temp;
                    dut.ReadRxpADC(out Temp);
                    rxPowerAdcArray[i] = Convert.ToDouble(Temp);
                }
                logoStr += logger.AdapterLogString(0, "Step3...Start Fitting Curve");
                if (calRxDmiStruct.is1Stor2StorPid==1)
                {
                    for (int j = 0; j < calRxDmiStruct.ArrayListRxPower.Count; j++)
                    {
                        logoStr += logger.AdapterLogString(1, "rxPowerAdcArray " + j + " :" + rxPowerAdcArray[j].ToString() + "****rxPoweruwArray " + j + ":" + rxPoweruwArray[j]);
                    }
                    double[] coefArray = algorithm.MultiLine(rxPowerAdcArray, rxPoweruwArray, calRxDmiStruct.ArrayListRxPower.Count, 1);
                    rxDmiCoefC = (float)coefArray[0];
                    rxDmiCoefB = (float)coefArray[1];
                    rxDmiCoefA = 0;
                    double[] tempCoefArray = new double[2]{ rxDmiCoefC, rxDmiCoefB };
                    
                    rxDmiCoefArray = ArrayList.Adapter(tempCoefArray);
                    rxDmiCoefArray.Reverse();
                    for (byte i = 0; i < rxDmiCoefArray.Count; i++)
                    {
                        logoStr += logger.AdapterLogString(1, "rxDmiCoefArray[" + i.ToString() + "]=" + rxDmiCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(rxDmiCoefArray[i])));
                       
                    }
                    logoStr += logger.AdapterLogString(0, "Step4...WriteCoef");
                    #region W&RRxpcoefc
                   isWriteCoefCOk=dut.SetRxpcoefc(rxDmiCoefC.ToString());
                 
                   if (isWriteCoefCOk)
                    {
                        isWriteCoefCOk = true;
                        logoStr += logger.AdapterLogString(1,"WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                      
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                       
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                    }
                    #endregion
                    #region W&R Rxpcoefb
                   isWriteCoefBOk=dut.SetRxpcoefb(rxDmiCoefB.ToString());

                   if (isWriteCoefBOk)
                    {
                        isWriteCoefBOk = true;
                        logoStr += logger.AdapterLogString(1, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    #endregion
                   #region W&R Rxpcoefa
                   isWriteCoefAOk = dut.SetRxpcoefa(rxDmiCoefA.ToString());

                   if (isWriteCoefAOk)
                   {
                       isWriteCoefAOk = true;
                       logoStr += logger.AdapterLogString(1, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                   }
                   else
                   {
                       isWriteCoefAOk = false;
                       logoStr += logger.AdapterLogString(3, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                   }
                   #endregion
                   if (isWriteCoefCOk & isWriteCoefBOk & isWriteCoefAOk)
                   {
                       isCalRxDmiOk = true;
                       logoStr += logger.AdapterLogString(1, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }
                   else
                   {
                       isCalRxDmiOk = false;
                       logoStr += logger.AdapterLogString(3, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }

                }
                else if (calRxDmiStruct.is1Stor2StorPid ==2)
                {
                    for (int j = 0; j < calRxDmiStruct.ArrayListRxPower.Count;j++ )
                    {
                        logoStr += logger.AdapterLogString(1, "rxPowerAdcArray " + j + " :" + rxPowerAdcArray[j].ToString() + "****rxPoweruwArray " + j + ":" + rxPoweruwArray[j]);
                    }
                 
                    double[] coefArray = algorithm.MultiLine(rxPowerAdcArray, rxPoweruwArray, calRxDmiStruct.ArrayListRxPower.Count, 2);
                    rxDmiCoefC = (float)coefArray[0];
                    rxDmiCoefB = (float)coefArray[1];
                    rxDmiCoefA = (float)coefArray[2];
                    rxDmiCoefArray = ArrayList.Adapter(coefArray);
                    rxDmiCoefArray.Reverse();
                    for (byte i = 0; i < rxDmiCoefArray.Count; i++)
                    {
                        logoStr += logger.AdapterLogString(1, "rxDmiCoefArray[" + i.ToString() + "]=" + rxDmiCoefArray[i].ToString() + " " + algorithm.ByteArraytoString(2, ",", algorithm.INT16To2Bytes(rxDmiCoefArray[i])));
                       
                    }
                    logoStr += logger.AdapterLogString(0, "Step4...WriteCoef");
                  
                    #region W&RRxpcoefc
                    isWriteCoefCOk=dut.SetRxpcoefc(rxDmiCoefC.ToString());
                   
                    if (isWriteCoefCOk)
                    {
                        isWriteCoefCOk = true; 
                        logoStr += logger.AdapterLogString(1,  "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());   
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefC:" + isWriteCoefCOk.ToString());
                    }
                    #endregion
                    #region W&R Rxpcoefb
                   isWriteCoefBOk= dut.SetRxpcoefb(rxDmiCoefB.ToString());
                    //dut.ReadRxpcoefb(out tempString);
                   if (isWriteCoefBOk)
                    {
                        isWriteCoefBOk = true;
                        logoStr += logger.AdapterLogString(1, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    else
                    {
                        isWriteCoefCOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefB:" + isWriteCoefBOk.ToString());
                    }
                    #endregion
                    #region W&R Rxpcoefa
                   isWriteCoefAOk=dut.SetRxpcoefa(rxDmiCoefA.ToString());
                  
                   if (isWriteCoefAOk)
                    {
                        isWriteCoefAOk = true;
                        logoStr += logger.AdapterLogString(1, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                    }
                    else
                    {
                        isWriteCoefAOk = false;
                        logoStr += logger.AdapterLogString(3, "WriterxDmiCoefA:" + isWriteCoefAOk.ToString());
                    }
                    #endregion
                   if (isWriteCoefCOk & isWriteCoefBOk & isWriteCoefAOk)
                   {
                       isCalRxDmiOk = true;
                       logoStr += logger.AdapterLogString(1, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }
                   else
                   {
                       isCalRxDmiOk = false;
                       logoStr += logger.AdapterLogString(3, "isCalRxDmiOk:" + isCalRxDmiOk.ToString());
                   }

                }
        }
            else
            {
                isCalRxDmiOk = false;
                logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
                logger.FlushLogBuffer();
                return isCalRxDmiOk;
            }
            AnalysisOutputParameters(outputParameters);
            logger.FlushLogBuffer();
            return isCalRxDmiOk;
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
                    if (algorithm.FindFileName(InformationList, "ARRAYLISTDMIRXCOEF", out index))
                    {
                        InformationList[index].DefaultValue = algorithm.ArrayListToStringArraySegregateByPunctuations(rxDmiCoefArray, ",");
                       
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
                logoStr += logger.AdapterLogString(4,  "InputParameters are not enough!");
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

                        if (algorithm.FindFileName(InformationList, "ARRAYLISTRXPOWER(DBM)", out index))
                        {
                            char[] tempCharArray = new char[] { ',' };
                            calRxDmiStruct.ArrayListRxPower = algorithm.StringtoArraylistDeletePunctuations(InformationList[index].DefaultValue, tempCharArray);
                             ;
                        }
                        if (algorithm.FindFileName(InformationList, "1STOR2STORPID", out index))
                        {
                            calRxDmiStruct.is1Stor2StorPid = Convert.ToByte(InformationList[index].DefaultValue);
                            
                        }
                        if (algorithm.FindFileName(InformationList, "HASOFFSET", out index))
                        {
                            calRxDmiStruct.HasOffset = Convert.ToBoolean(InformationList[index].DefaultValue);

                        }
                        if (algorithm.FindFileName(InformationList, "READRXADCCOUNT", out index))
                        {
                            calRxDmiStruct.ReadRxADCCount = Convert.ToByte(InformationList[index].DefaultValue);
                            if (calRxDmiStruct.ReadRxADCCount <= 0)
                            {
                                calRxDmiStruct.ReadRxADCCount = 1;
                            }
                        }
                        if (algorithm.FindFileName(InformationList, "SLEEPTIME", out index))
                        {
                            calRxDmiStruct.SleepTime = Convert.ToUInt16(InformationList[index].DefaultValue);
                        }
                         if (algorithm.FindFileName(InformationList, "EXTRAOFFSET", out index))
                        {
                            calRxDmiStruct.ExtraOffset = Convert.ToByte(InformationList[index].DefaultValue);
                        }
                      //  ExtraOffset
                    }

                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }
        }
        protected void SetSleep(UInt16 sleeptime = 50)
        {
            Thread.Sleep(sleeptime);
        }
#endregion
    }
}
