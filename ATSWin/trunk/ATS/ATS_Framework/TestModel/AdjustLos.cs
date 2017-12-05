﻿using System;
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
    
    public enum AdjustLosSpecs : byte
    {
        LOSA,
        LOSD
    }
   public class AdjustLos : TestModelBase
    {
#region Attribute
        
        private AdjustLosStruct adjustLosStruct = new AdjustLosStruct();
        
        private double targetLosADac;
        private double targetLosDDac;
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
        public AdjustLos(DUT inPuDut, logManager logmanager)
        {
           logger = logmanager;
           logoStr = null;
           dut = inPuDut;
           SpecNameArray.Clear();
           inPutParametersNameArray.Clear();
           inPutParametersNameArray.Add("LOSA_DAC_START");
           inPutParametersNameArray.Add("LOSA_DAC_Max");
           inPutParametersNameArray.Add("LOSA_DAC_Min");
           inPutParametersNameArray.Add("LOSA_TUNESTEP");

           inPutParametersNameArray.Add("LOSD_DAC_START");
           inPutParametersNameArray.Add("LOSD_DAC_MAX");
           inPutParametersNameArray.Add("LOSD_DAC_MIN");
           inPutParametersNameArray.Add("LOSD_TUNESTEP");
           inPutParametersNameArray.Add("ISADJUSTLOSA");
           inPutParametersNameArray.Add("ISADJUSTLOSD");
           inPutParametersNameArray.Add("LOSAINPUTPOWER");
           inPutParametersNameArray.Add("LOSDINPUTPOWER");
           SpecNameArray.Add((byte)AdjustLosSpecs.LOSA, " LOSA(dBm)");
           SpecNameArray.Add((byte)AdjustLosSpecs.LOSD, " LOSD(dBm)");
               
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
                   if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                   {
                       tempValues[i].CheckEquipmentRole(2, GlobalParameters.CurrentChannel);
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
           logger.FlushLogBuffer();
           logoStr = "";
           GenerateSpecList(SpecNameArray);
          if (AnalysisInputParameters(inputParameters)==false)
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
               if (adjustLosStruct.isAdjustLosA)
               {
                   //clear loss status before adjust
                   for (int i = 0; i < 10; i++)
                   {
                       tempAtt.AttnValue("0");
                       Thread.Sleep(100);
                       dut.WriteLOSDac(adjustLosStruct.LosAVoltageStartValue);
                       Thread.Sleep(100);
                       if (dut.ChkRxLos() == false)
                       {
                           break;
                       }
                       Thread.Sleep(100);
                   }

                   logoStr += logger.AdapterLogString(0, "Step3...Start Adjust LosA");

                   logoStr += logger.AdapterLogString(1, "Set LosA RxPower:" + adjustLosStruct.LosASetPower.ToString());
                   tempAtt.AttnValue(adjustLosStruct.LosASetPower.ToString(), 1);
                   dut.WriteLOSDac(adjustLosStruct.LosAVoltageStartValue);
                   dut.StoreLOSDac(adjustLosStruct.LosAVoltageStartValue);
                   Thread.Sleep(100);
                   bool isLos = dut.ChkRxLos();
                   Thread.Sleep(50);
                   isLos = dut.ChkRxLos();
                  
                   if (!isLos)

                   {
                       isLosA = OneSectionMethodLosAdjust(adjustLosStruct.LosAVoltageStartValue, adjustLosStruct.LosAVoltageTuneStep, adjustLosStruct.LosAVoltageUperLimit, adjustLosStruct.LosAVoltageLowLimit, dut, out targetLosADac);
                      // isLosA = OneSectionMethodLosAdjust(losAMax, adjustLosStruct.LosAVoltageTuneStep, adjustLosStruct.LosAVoltageUperLimit, adjustLosStruct.LosAVoltageLowLimit, dut,  out targetLosADac);
                       logoStr += logger.AdapterLogString(1, "targetLosADac=" + targetLosADac.ToString());
                       logoStr += logger.AdapterLogString(1, isLosA.ToString());
                   }
                   else
                   {
                       isLosA = false;
                   }

                 
               }
               if (adjustLosStruct.isAdjustLosD)
               {
                   logoStr += logger.AdapterLogString(0, "Step3...Start Adjust LosD");
                   logoStr += logger.AdapterLogString(1, "Set LosA RxPower:" + adjustLosStruct.LosDSetPower.ToString());
                   tempAtt.AttnValue(adjustLosStruct.LosDSetPower.ToString());
                   dut.WriteLOSDDac(adjustLosStruct.LosDVoltageStartValue);
                   dut.StoreLOSDDac(adjustLosStruct.LosDVoltageStartValue);
                   Thread.Sleep(100);
                   bool isLos = dut.ChkRxLos();
                   Thread.Sleep(50);
                   isLos = dut.ChkRxLos();
                   if (isLos)
                   
                   {
                       isLosD = OneSectionMethodLosDAdjust(adjustLosStruct.LosDVoltageStartValue, adjustLosStruct.LosDVoltageTuneStep, adjustLosStruct.LosDVoltageUperLimit, adjustLosStruct.LosDVoltageLowLimit, dut, out targetLosDDac);
                       logoStr += logger.AdapterLogString(1, "targetLosDDac=" + targetLosDDac.ToString());
                       logoStr += logger.AdapterLogString(1, isLosD.ToString());
                   }
                   else
                   {
                       isLosD = false;
                   }
               }
               if (!adjustLosStruct.isAdjustLosD && !adjustLosStruct.isAdjustLosA)
               {
                   logoStr += logger.AdapterLogString(1, "Set LosA RxPower:" + adjustLosStruct.LosAVoltageStartValue.ToString());
                   logoStr += logger.AdapterLogString(1, "Set LosD RxPower:" + adjustLosStruct.LosDVoltageStartValue.ToString());
                   isLosA=WriteFixedLosValue(dut);
                   logoStr += logger.AdapterLogString(1, "targetLosADac=" + targetLosADac.ToString());
                   logoStr += logger.AdapterLogString(1, "targetLosDDac=" + targetLosDDac.ToString());
                 
               }
               OutPutandFlushLog();
               if (adjustLosStruct.isAdjustLosA || adjustLosStruct.isAdjustLosD)
              {
                  logger.FlushLogBuffer();
                  return isLosA || isLosD;
              } 
              else
              {
                  logger.FlushLogBuffer();
                  return isLosA;
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
                   //for (byte i = 0; i < InformationList.Length; i++)
                   {

                       if (algorithm.FindFileName(InformationList, "LOSA_DAC_START", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp<=0)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;
                           } 
                           else
                           {
                               adjustLosStruct.LosAVoltageStartValue = Convert.ToDouble(temp);
                           }
                           
                       }

                       if (algorithm.FindFileName(InformationList, "LOSA_DAC_MAX", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp<=0)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;
                           } 
                           else
                           {
                               adjustLosStruct.LosAVoltageUperLimit = Convert.ToDouble(temp);
                           }
                         
                       }

                           if (algorithm.FindFileName(InformationList, "LOSA_DAC_MIN", out index))
                           {
                               double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                               if (temp <= 0)
                               {
                                   logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                                   return false;

                               }
                               else
                               {
                                   adjustLosStruct.LosAVoltageLowLimit = Convert.ToUInt32(temp);
                               }

                           }

                       if (algorithm.FindFileName(InformationList, "LOSA_TUNESTEP", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp<=0)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;
                           } 
                           else
                           {
                               adjustLosStruct.LosAVoltageTuneStep = Convert.ToByte(temp);
                           }
                           
                       }
                       if (algorithm.FindFileName(InformationList, "LOSAINPUTPOWER", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp <= -40)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;

                           }
                           else
                           {
                               adjustLosStruct.LosASetPower = Convert.ToDouble(temp);
                           }

                       }
                       if (algorithm.FindFileName(InformationList, "LOSD_DAC_START", out index))
                       {
                           double temp=Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp<=0)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;
                           } 
                           else
                           {
                               adjustLosStruct.LosDVoltageStartValue = Convert.ToDouble(temp);
                           }
                          
                       }

                       if (algorithm.FindFileName(InformationList, "LOSD_DAC_MAX", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp<=0)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;
                           } 
                           else
                           {
                               adjustLosStruct.LosDVoltageUperLimit = Convert.ToDouble(temp);
                           }
                           
                       }


                       if (algorithm.FindFileName(InformationList, "LOSD_DAC_MIN", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp<=0)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;
                           } 
                           else
                           {
                               adjustLosStruct.LosDVoltageLowLimit = Convert.ToDouble(temp);
                           }
                          
                       }

                       if (algorithm.FindFileName(InformationList, "LOSD_TUNESTEP", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp<=0)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;
                           } 
                           else
                           {
                               adjustLosStruct.LosDVoltageTuneStep = Convert.ToByte(temp);
                           }
                           
                       }


                       if (algorithm.FindFileName(InformationList, "LOSDINPUTPOWER", out index))
                       {
                           double temp = Convert.ToDouble(InformationList[index].DefaultValue);
                           if (temp <= -40)
                           {
                               logoStr += logger.AdapterLogString(4, InformationList[index].FiledName + "is equal or lesser than 0");
                               return false;

                           }
                           else
                           {
                               adjustLosStruct.LosDSetPower = Convert.ToDouble(temp);
                           }

                       }


                       if (algorithm.FindFileName(InformationList, "ISADJUSTLOSA", out index))
                       {
                           string temp = InformationList[index].DefaultValue;
                           if (temp.ToUpper().Trim() == "1" || temp.ToUpper().Trim() == "TRUE")
                           {
                               adjustLosStruct.isAdjustLosA =true; 
                           } 
                           else
                           {
                               adjustLosStruct.isAdjustLosA =false; 
                           }
                                                  
                       }

                       if (algorithm.FindFileName(InformationList, "ISADJUSTLOSD", out index))
                       {
                           string temp = InformationList[index].DefaultValue;
                           if (temp.ToUpper().Trim() == "1" || temp.ToUpper().Trim() == "TRUE")
                           {
                               adjustLosStruct.isAdjustLosD = true;
                           }
                           else
                           {
                               adjustLosStruct.isAdjustLosD = false;
                           }
                           
                       }                      
                       

                   }
                   if (adjustLosStruct.LosAVoltageStartValue > adjustLosStruct.LosAVoltageUperLimit || adjustLosStruct.LosAVoltageStartValue < adjustLosStruct.LosAVoltageLowLimit || adjustLosStruct.LosAVoltageUperLimit <= adjustLosStruct.LosAVoltageLowLimit||
                       adjustLosStruct.LosDVoltageStartValue > adjustLosStruct.LosDVoltageUperLimit || adjustLosStruct.LosDVoltageStartValue < adjustLosStruct.LosDVoltageLowLimit || adjustLosStruct.LosDVoltageUperLimit <= adjustLosStruct.LosDVoltageLowLimit)
                   {
                       logoStr += logger.AdapterLogString(4, "input data is wrong!");
                       return false;
                   }
               }
               logoStr += logger.AdapterLogString(1, "OK!");  
               return true;
           }        


       }

       protected bool OneSectionMethodLosAdjust(double startValue, byte step, double uperLimit, double lowLimit, DUT dut, out double targetLosDac)
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
             
               if (isLos == false )
               {
                   adjustCount++;
               }

           } while (adjustCount <= 30 && (isLos == false));
           targetLosDac =Convert.ToUInt32( startValue);
           return isLos;
       }
       protected bool OneSectionMethodLosDAdjust(double startValue, byte step, double uperLimit, double lowLimit, DUT dut, out double targetLosDac)
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
           return isLos==false;

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
