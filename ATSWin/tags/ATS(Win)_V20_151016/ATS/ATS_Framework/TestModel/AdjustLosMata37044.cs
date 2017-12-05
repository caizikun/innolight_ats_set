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


    public class AdjustLosMata37044 : TestModelBase
    {
        #region Attribute

        private AdjustLosStruct adjustLosStruct = new AdjustLosStruct();

        private UInt32 targetLosADac;
        private UInt32 targetLosDDac;
        private bool isLosA;
        private bool isLosD;
        private double losAMax;
        private double losDMin;
        private ArrayList inPutParametersNameArray = new ArrayList();
        //equipments
        private Attennuator tempAtt;
        private Powersupply tempps;
        private SortedList<byte, string> SpecNameArray = new SortedList<byte, string>();
        #endregion
        #region Method
        public AdjustLosMata37044(DUT inPuDut, logManager logmanager)
        {
            logger = logmanager;
            logoStr = null;
            dut = inPuDut;
            SpecNameArray.Clear();
            //inPutParametersNameArray.Clear();
            //inPutParametersNameArray.Add("LOSA_DAC_START");
            //inPutParametersNameArray.Add("LOSA_DAC_Max");
            //inPutParametersNameArray.Add("LOSA_DAC_Min");
            //inPutParametersNameArray.Add("LOSA_TUNESTEP");

            //inPutParametersNameArray.Add("LOSD_DAC_START");
            //inPutParametersNameArray.Add("LOSD_DAC_MAX");
            //inPutParametersNameArray.Add("LOSD_DAC_MIN");
            //inPutParametersNameArray.Add("LOSD_TUNESTEP");
            //inPutParametersNameArray.Add("ISADJUSTLOSA");
            //inPutParametersNameArray.Add("ISADJUSTLOSD");
            inPutParametersNameArray.Add("LOSAINPUTPOWER");
            SpecNameArray.Add((byte)AdjustLosSpecs.LOSA, "LOSA(dBm)");
            SpecNameArray.Add((byte)AdjustLosSpecs.LOSD, "LOSD(dBm)");

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
                    if (tempKeys[i].ToUpper().Contains("ATTEN"))
                    {
                        selectedEquipList.Add("ATTEN", tempValues[i]);
                    }
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }

                }
                tempAtt = (Attennuator)selectedEquipList["ATTEN"];
                tempps = (Powersupply)selectedEquipList["POWERSUPPLY"];
                if (tempAtt != null && tempps != null)
                {
                    isOK = true;

                }
                else
                {
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
                else
                {
                    isOK = false;
                }
                return isOK;
            }
        }

        protected override bool StartTest()
        {

            bool  flagLos=false;
            byte[]StepArray= new byte[]{ 64,16,4,1};
            logger.FlushLogBuffer();
            logoStr = "";
            GenerateSpecList(SpecNameArray);
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

            if (tempAtt != null && tempps != null)
            {
                if (GlobalParameters.CurrentChannel == 1)
                {
                    dut.WriteLOSDac(0);
                }

                byte[]Dac= new byte[1];
                dut.ReadLOSDac(1,out Dac);
                byte startValue = Dac[0];
             
                //switch(GlobalParameters.CurrentChannel)
                //{
                //    case 1:
                //        startValue = Convert.ToByte(Dac[0] & 0x3F);
                //        break;
                //    case 2:
                //        startValue = Convert.ToByte(Dac[0] & 0x3F);
                //        break;
                //    default:
                //        break;
                //}

              //  GlobalParameters.CurrentChannel

                tempAtt.AttnValue(adjustLosStruct.LosASetPower.ToString());
                Thread.Sleep(2000);
                logoStr += logger.AdapterLogString(0, "Step3...Start Adjust LosA");
                byte i = GlobalParameters.CurrentChannel;

                //for (int i = 1; i < 5; i++)
                //{
                    int AdjustCout=0;
                    dut.ChangeChannel(i.ToString());
                     Thread.Sleep(100);
                    flagLos = dut.ChkRxLos();
                    Thread.Sleep(50);
                    flagLos = dut.ChkRxLos();
                    //byte[]Dac= new byte[1];
                    //dut.ReadLOSDac(1,out Dac);

                  

                    while (!flagLos && AdjustCout<4)
                    {
                        AdjustCout++;

                        startValue += StepArray[i - 1];

                        dut.WriteLOSDac(startValue);
                        Thread.Sleep(100);
                        dut.ChkRxLos();
                        Thread.Sleep(50);
                        flagLos = dut.ChkRxLos();
                    }
                     dut.StoreLOSDac(startValue);
                     if (AdjustCout>2)
                     {
                         adjustLosStruct.isAdjustLosA = false;
                     }
                     else
                     {
                         adjustLosStruct.isAdjustLosA = true;
                     }
               // adjustLosStruct.isAdjustLosA
               // }
                    //if (adjustLosStruct.isAdjustLosA)
                    //{
                    //    logoStr += logger.AdapterLogString(1, "Set LosA RxPower:" + adjustLosStruct.LosASetPower.ToString());
                    //    tempAtt.AttnValue(adjustLosStruct.LosASetPower.ToString(), 1);
                    //    dut.WriteLOSDac(adjustLosStruct.LosAVoltageStartValue);
                    //    dut.StoreLOSDac(adjustLosStruct.LosAVoltageStartValue);
                    //    Thread.Sleep(100);
                    //    bool isLos = dut.ChkRxLos();
                    //    Thread.Sleep(50);
                    //    isLos = dut.ChkRxLos();
                    //    if (!isLos)
                    //    {
                    //        isLosA = OneSectionMethodLosAdjust(adjustLosStruct.LosAVoltageStartValue, adjustLosStruct.LosAVoltageTuneStep, adjustLosStruct.LosAVoltageUperLimit, adjustLosStruct.LosAVoltageLowLimit, dut, out targetLosADac);
                    //        // isLosA = OneSectionMethodLosAdjust(losAMax, adjustLosStruct.LosAVoltageTuneStep, adjustLosStruct.LosAVoltageUperLimit, adjustLosStruct.LosAVoltageLowLimit, dut,  out targetLosADac);
                    //        logoStr += logger.AdapterLogString(1, "targetLosADac=" + targetLosDDac.ToString());
                    //        logoStr += logger.AdapterLogString(1, isLosA.ToString());
                    //    }


                    //}
                    //if (adjustLosStruct.isAdjustLosD)
                    //{
                    //    logoStr += logger.AdapterLogString(1, "Set LosA RxPower:" + losDMin.ToString());
                    //    tempAtt.AttnValue(losDMin.ToString(), 0);
                    //    dut.WriteLOSDac(adjustLosStruct.LosAVoltageStartValue);
                    //    dut.StoreLOSDac(adjustLosStruct.LosAVoltageStartValue);
                    //    Thread.Sleep(100);
                    //    bool isLos = dut.ChkRxLos();
                    //    Thread.Sleep(50);
                    //    isLos = dut.ChkRxLos();
                    //    if (isLos)
                    //    {
                    //        isLosD = OneSectionMethodLosDAdjust(losDMin, adjustLosStruct.LosDVoltageTuneStep, adjustLosStruct.LosDVoltageUperLimit, adjustLosStruct.LosDVoltageLowLimit, dut, out targetLosDDac);
                    //        logoStr += logger.AdapterLogString(1, "targetLosADac=" + targetLosDDac.ToString());
                    //        logoStr += logger.AdapterLogString(1, isLosD.ToString());
                    //    }
                    //}
                    //if (!adjustLosStruct.isAdjustLosD && !adjustLosStruct.isAdjustLosA)
                    //{
                    //    logoStr += logger.AdapterLogString(1, "Set LosA RxPower:" + adjustLosStruct.LosAVoltageStartValue.ToString());
                    //    logoStr += logger.AdapterLogString(1, "Set LosD RxPower:" + adjustLosStruct.LosDVoltageStartValue.ToString());
                    //    isLosA = WriteFixedLosValue(dut);
                    //    logoStr += logger.AdapterLogString(1, "targetLosDDac=" + targetLosADac.ToString());
                    //    logoStr += logger.AdapterLogString(1, "targetLosDDac=" + targetLosDDac.ToString());

                    //}
                    OutPutandFlushLog();
                if (adjustLosStruct.isAdjustLosA || adjustLosStruct.isAdjustLosD)
                {
                    logger.FlushLogBuffer();
                    return true;
                }
                else
                {
                    logger.FlushLogBuffer();
                    return false;
                }

            }
            else
            {
                logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                OutPutandFlushLog();
                return false;
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
                   

                     

                      
                        if (algorithm.FindFileName(InformationList, "LOSAINPUTPOWER", out index))
                        {
                            double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                          
                                adjustLosStruct.LosASetPower = Convert.ToDouble(temp);
                            

                        } 
                }
                logoStr += logger.AdapterLogString(1, "OK!");
                return true;
            }


        }

        protected bool OneSectionMethodLosAdjust(UInt32 startValue, byte step, UInt32 uperLimit, UInt32 lowLimit, DUT dut, out UInt32 targetLosDac)
        {
            byte adjustCount = 0;
            bool isLos = false;
            byte totalExponentiationCount = Convert.ToByte(Math.Floor(Math.Log(Convert.ToDouble(step), 2)));
            do
            {
                if (startValue > uperLimit)
                {
                    startValue = uperLimit;
                }
                else if (startValue < lowLimit)
                {
                    startValue = lowLimit;
                }
                dut.WriteLOSDac(startValue);
                dut.StoreLOSDac(startValue);
                Thread.Sleep(100);
                isLos = dut.ChkRxLos();
                Thread.Sleep(50);
                isLos = dut.ChkRxLos();
                if ((isLos == false))
                {
                    UInt32 tempValue = (UInt32)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount) >= uperLimit ? uperLimit : startValue + (UInt32)Math.Pow(2, totalExponentiationCount));
                    startValue = (UInt32)tempValue;
                }

                if (isLos == false)
                {
                    adjustCount++;
                }

            } while (adjustCount <= 30 && (isLos == false));
            targetLosDac = Convert.ToUInt32(startValue);
            return isLos;
        }
        protected bool OneSectionMethodLosDAdjust(UInt32 startValue, byte step, UInt32 uperLimit, UInt32 lowLimit, DUT dut, out UInt32 targetLosDac)
        {
            byte adjustCount = 0;
            bool isLos = false;
            byte totalExponentiationCount = Convert.ToByte(Math.Floor(Math.Log(Convert.ToDouble(step), 2)));
            do
            {
                if (startValue > uperLimit)
                {
                    startValue = uperLimit;
                }
                else if (startValue < lowLimit)
                {
                    startValue = lowLimit;
                }
                dut.WriteLOSDDac(startValue);
                dut.StoreLOSDDac(startValue);
                Thread.Sleep(100);
                isLos = dut.ChkRxLos();
                Thread.Sleep(50);
                isLos = dut.ChkRxLos();
                if ((isLos == true))
                {
                    UInt32 tempValue = (UInt32)(startValue - (UInt32)Math.Pow(2, totalExponentiationCount) <= lowLimit ? lowLimit : startValue - (UInt32)Math.Pow(2, totalExponentiationCount));
                    startValue = (UInt32)tempValue;
                }

                if (isLos == true)
                {
                    adjustCount++;
                }

            } while (adjustCount <= 30 && (isLos == true));
            targetLosDac = startValue;
            return isLos == false;

        }
        protected bool WriteFixedLosValue(DUT dut)
        {
            bool isWriteOk = dut.WriteLOSDac(adjustLosStruct.LosAVoltageStartValue);
            bool isStoreOk = dut.StoreLOSDac(adjustLosStruct.LosAVoltageStartValue);
            bool isWriteOk1 = dut.WriteLOSDDac(adjustLosStruct.LosAVoltageStartValue);
            bool isStoreOk1 = dut.StoreLOSDDac(adjustLosStruct.LosAVoltageStartValue);
            Thread.Sleep(100);
            targetLosADac = adjustLosStruct.LosAVoltageStartValue;
            targetLosDDac = adjustLosStruct.LosDVoltageStartValue;
            return isWriteOk && isStoreOk && isWriteOk1 && isStoreOk1;

        }
        protected bool LoadPNSpec()
        {
            try
            {
                if (algorithm.FindDataInDataTable(specParameters, SpecTableStructArray, Convert.ToString(GlobalParameters.CurrentChannel)) == null)
                {

                    return false;

                }
                losAMax = Convert.ToDouble(SpecTableStructArray[(byte)AdjustLosSpecs.LOSA].MaxValue);
                losDMin = Convert.ToDouble(SpecTableStructArray[(byte)AdjustLosSpecs.LOSD].MinValue);
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
                procData[0].FiledName = "TARGETLOSADAC";
                procData[0].DefaultValue = Convert.ToString(targetLosADac);
                procData[1].FiledName = "TARGETLOSDDAC";
                procData[1].DefaultValue = Convert.ToString(targetLosDDac);

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
