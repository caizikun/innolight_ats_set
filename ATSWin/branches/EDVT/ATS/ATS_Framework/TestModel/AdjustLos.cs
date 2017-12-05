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
   public class AdjustLos : TestModelBase
    {
#region Attribute
        
        private AdjustLosStruct adjustLosStruct = new AdjustLosStruct();
        
        private UInt32 targetLosDac;
        private UInt32 targetLosAac;
        private bool isLosA;
        private bool isLosD;
       
        private ArrayList inPutParametersNameArray = new ArrayList();    
     
#endregion
#region Method
        public AdjustLos(DUT inPuDut, logManager logmanager)
        {
           logger = logmanager;
           logoStr = null;
           dut = inPuDut; 
           inPutParametersNameArray.Clear();
           inPutParametersNameArray.Add("AUTOTUNE");
           inPutParametersNameArray.Add("LOSAINPUTPOWER");
           inPutParametersNameArray.Add("LOSAVOLTAGESTARTVALUE(V)");
           inPutParametersNameArray.Add("LOSAVOLTAGEUPERLIMIT(V)");
           inPutParametersNameArray.Add("LOSAVOLTAGELOWLIMIT(V)");
           inPutParametersNameArray.Add("LOSAVOLTAGETUNESTEP(V)");
           inPutParametersNameArray.Add("LOSTOLERANCESTEP(V)");
           inPutParametersNameArray.Add("LOSDINPUTPOWER");
           inPutParametersNameArray.Add("LOSDVOLTAGESTARTVALUE(V)");
           inPutParametersNameArray.Add("LOSDVOLTAGEUPERLIMIT(V)");
           inPutParametersNameArray.Add("LOSDVOLTAGELOWLIMIT(V)");
           inPutParametersNameArray.Add("LOSDVOLTAGETUNESTEP(V)");
           inPutParametersNameArray.Add("ISLOSALOSDCOMBIN");
           
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
                   if (tempKeys[i].ToUpper().Contains("ATTEN"))
                   {
                       selectedEquipList.Add("ATTEN", tempValues[i]);
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
          if (AnalysisInputParameters(inputParameters)==false)
          {
              logger.FlushLogBuffer();
              return false;
          }
          if (selectedEquipList["DUT"] != null && selectedEquipList["ATTEN"] != null && selectedEquipList["POWERSUPPLY"] != null)
           {               
               Attennuator tempAtt = (Attennuator)selectedEquipList["ATTEN"];
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
                   Thread.Sleep(200);
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
               logoStr += logger.AdapterLogString(0, "Step3...Start Adjust LosA");
               if (adjustLosStruct.islosalosdcombin)
               {
                   logoStr += logger.AdapterLogString(1,"Set LosA RxPower:" + adjustLosStruct.LosAInputPower.ToString());
                   tempAtt.AttnValue(adjustLosStruct.LosAInputPower.ToString());
                   isLosA = LosAEqualLosDAdjust(adjustLosStruct.LosAVoltageStartValue, adjustLosStruct.LosAVoltageTuneStep, adjustLosStruct.LosAVoltageUperLimit, adjustLosStruct.LosAVoltageLowLimit, dut, adjustLosStruct.LosToleranceStep, out targetLosAac);
                   logoStr += logger.AdapterLogString(1, "targetLosADac=" + targetLosDac.ToString());
                   logoStr += logger.AdapterLogString(1, isLosA.ToString());
               } 
               else
               {
                   logoStr += logger.AdapterLogString(1, "Set LosA RxPower:" + adjustLosStruct.LosAInputPower.ToString());
                
                   tempAtt.AttnValue(adjustLosStruct.LosAInputPower.ToString());
                   isLosA = OneSectionMethodLosAdjust(adjustLosStruct.LosAVoltageStartValue, adjustLosStruct.LosAVoltageTuneStep, adjustLosStruct.LosAVoltageUperLimit, adjustLosStruct.LosAVoltageLowLimit, dut, adjustLosStruct.LosToleranceStep, out targetLosDac);
                   logoStr += logger.AdapterLogString(1, "targetLosADac=" + targetLosDac.ToString());
                   logoStr += logger.AdapterLogString(1, isLosA.ToString());
                   logoStr += logger.AdapterLogString(0, "Step4...Start Adjust LosD");
                   logoStr += logger.AdapterLogString(1,"Set LosD RxPower:" + adjustLosStruct.LosDInputPower.ToString());
                  
                   tempAtt.AttnValue(adjustLosStruct.LosDInputPower.ToString());
                   isLosD = OneSectionMethodLosDAdjust(adjustLosStruct.LosDVoltageStartValue, adjustLosStruct.LosDVoltageTuneStep, adjustLosStruct.LosDVoltageUperLimit, adjustLosStruct.LosDVoltageLowLimit, dut, adjustLosStruct.LosToleranceStep, out targetLosDac);

                   logoStr += logger.AdapterLogString(1, "targetLosDDac=" + targetLosDac.ToString());
                   logoStr += logger.AdapterLogString(1, isLosD.ToString());
               }             
             
               AnalysisOutputParameters(outputParameters);
               if (adjustLosStruct.islosalosdcombin)
              {
                  logger.FlushLogBuffer();
                  return isLosA;
              } 
              else
              {
                  logger.FlushLogBuffer();
                  return isLosA == true && isLosD == true;
              }
           }
          else
          {
              logoStr += logger.AdapterLogString(4, "Equipments is not enough!");
              logger.FlushLogBuffer();
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
               bool isParametersComplete = false;
               for (byte i = 0; i < inPutParametersNameArray.Count; i++)
               {
                   if (algorithm.FindFileName(InformationList, inPutParametersNameArray[i].ToString(), out index) == false)
                   {
                       logoStr += logger.AdapterLogString(4,inPutParametersNameArray[i].ToString() + "is not exist");               
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
                           adjustLosStruct.AutoTune = Convert.ToBoolean(InformationList[index].DefaultValue); 
                       }
                       if (algorithm.FindFileName(InformationList, "LOSAINPUTPOWER", out index))
                       {
                           adjustLosStruct.LosAInputPower = Convert.ToDouble(InformationList[index].DefaultValue);
                       }
                       if (algorithm.FindFileName(InformationList, "LOSAVOLTAGESTARTVALUE(V)", out index))
                       {
                           adjustLosStruct.LosAVoltageStartValue = Convert.ToUInt32(InformationList[index].DefaultValue);
                       }
                       
                       if (algorithm.FindFileName(InformationList, "LOSAVOLTAGEUPERLIMIT(V)", out index))
                       {
                           adjustLosStruct.LosAVoltageUperLimit = Convert.ToUInt32(InformationList[index].DefaultValue);
                       }
                       
                       if (algorithm.FindFileName(InformationList, "LOSAVOLTAGELOWLIMIT(V)", out index))
                       {
                           adjustLosStruct.LosAVoltageLowLimit = Convert.ToUInt32(InformationList[index].DefaultValue);
                       }
                        
                       if (algorithm.FindFileName(InformationList, "LOSAVOLTAGETUNESTEP(V)", out index))
                       {
                           adjustLosStruct.LosAVoltageTuneStep = Convert.ToByte(InformationList[index].DefaultValue);
                       }
                      
                       if (algorithm.FindFileName(InformationList, "LOSTOLERANCESTEP(V)", out index))
                       {
                           adjustLosStruct.LosToleranceStep = Convert.ToByte(InformationList[index].DefaultValue);
                       }
                       
                       if (algorithm.FindFileName(InformationList, "LOSDINPUTPOWER", out index))
                       {
                           adjustLosStruct.LosDInputPower = Convert.ToDouble(InformationList[index].DefaultValue);
                       }                       
                       if (algorithm.FindFileName(InformationList, "LOSDVOLTAGESTARTVALUE(V)", out index))
                       {
                           adjustLosStruct.LosDVoltageStartValue = Convert.ToUInt32(InformationList[index].DefaultValue);
                       }
                       
                       if (algorithm.FindFileName(InformationList, "LOSDVOLTAGEUPERLIMIT(V)", out index))
                       {
                           adjustLosStruct.LosDVoltageUperLimit = Convert.ToUInt32(InformationList[index].DefaultValue);
                       }
                       

                       if (algorithm.FindFileName(InformationList, "LOSDVOLTAGELOWLIMIT(V)", out index))
                       {
                           adjustLosStruct.LosDVoltageLowLimit = Convert.ToUInt32(InformationList[index].DefaultValue);
                       }
                      
                       if (algorithm.FindFileName(InformationList, "LOSDVOLTAGETUNESTEP(V)", out index))
                       {
                           adjustLosStruct.LosDVoltageTuneStep = Convert.ToByte(InformationList[index].DefaultValue);
                       }
                       
                       if (algorithm.FindFileName(InformationList, "ISLOSALOSDCOMBIN", out index))
                       {
                           adjustLosStruct.islosalosdcombin = Convert.ToBoolean(InformationList[index].DefaultValue);                        
                       }                      
                       
                       

                   }

               }
               logoStr += logger.AdapterLogString(1, "OK!");  
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
             
               return true;
           }
       }
       protected bool OneSectionMethodLosAdjust(UInt32 startValue, byte step, UInt32 uperLimit, UInt32 lowLimit, DUT dut, byte tolerenceStep, out UInt32 targetLosDac)
       {
           byte adjustCount = 0;
           byte backUpCount = 0;
           bool isLos = false;
           byte tempTolerenceStep = tolerenceStep;
           tolerenceStep = Convert.ToByte(Math.Floor(Math.Log(Convert.ToDouble(tempTolerenceStep), 2)));
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
               Thread.Sleep(500);
               dut.StoreLOSDac(startValue);
               Thread.Sleep(1000);
               isLos = dut.ChkRxLos();
               Thread.Sleep(1000);
               isLos = dut.ChkRxLos();
               if ((isLos == false))
               {
                   int tempValue =(int) (startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= 4294967295 ? 4294967295 : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                   startValue = (UInt32)tempValue;
               }
               else if ((isLos == true) && ((totalExponentiationCount - backUpCount + 1) > tolerenceStep))
               {
                   int tempValue = (int)((startValue - (UInt16)Math.Pow(2, totalExponentiationCount - backUpCount)) >= 0 ? (startValue - (UInt16)Math.Pow(2, totalExponentiationCount - backUpCount)) : 0);
                   startValue = (UInt16)tempValue;
                   backUpCount++;
                   tempValue = (int)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= 4294967295 ? 4294967295 : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                   startValue = (UInt16)tempValue;
               }
               if (isLos == false || (totalExponentiationCount - backUpCount + 1) >= tolerenceStep)
               {
                   adjustCount++;
               }

           } while (adjustCount <= 30 && (isLos == false || (totalExponentiationCount - backUpCount+1) > tolerenceStep));
           targetLosDac = startValue;
           return isLos;
       }
       protected bool OneSectionMethodLosDAdjust(UInt32 startValue, byte step, UInt32 uperLimit, UInt32 lowLimit, DUT dut, byte tolerenceStep, out UInt32 targetLosDac)
       {
           byte adjustCount = 0;
           byte backUpCount = 0;
           bool isLos = false;
           byte tempTolerenceStep = tolerenceStep;
           tolerenceStep = Convert.ToByte(Math.Floor(Math.Log(Convert.ToDouble(tempTolerenceStep), 2)));
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
               Thread.Sleep(500);
               dut.StoreLOSDac(algorithm.Uint16DataConvertoBytes(Convert.ToUInt16(startValue)));
               Thread.Sleep(1000);
               isLos = dut.ChkRxLos();
               Thread.Sleep(1000);
               isLos = dut.ChkRxLos();
               if ((isLos == true))
               {
                   int tempValue = (int)(startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount) >= 4294967295 ? 4294967295 : startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount));
                   startValue = (UInt32)tempValue;
               }
               else if ((isLos == false) && ((totalExponentiationCount - backUpCount + 1) > tolerenceStep))
               {
                   int tempValue = (int)((startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) >= 0 ? (startValue - (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) : 0);
                   startValue = (UInt32)tempValue;
                   backUpCount++;
                   tempValue = (int)((startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)) >= 4294967295 ? 4294967295 : (startValue + (UInt32)Math.Pow(2, totalExponentiationCount - backUpCount)));
                   startValue = (UInt32)tempValue;
               }
               if (isLos == true || (totalExponentiationCount - backUpCount + 1) >= tolerenceStep)
               {
                   adjustCount++;
               }

           } while (adjustCount <= 30 && (isLos == true || (totalExponentiationCount - backUpCount + 1) > tolerenceStep));
           targetLosDac = startValue;
           return isLos==false;

       }
       protected bool LosAEqualLosDAdjust(UInt32 startValue, byte step, UInt32 uperLimit, UInt32 lowLimit, DUT dut, byte tolerenceStep, out UInt32 targetLosDac)
       {
           dut.WriteLOSDac(startValue);       
           targetLosDac = startValue;
           return true;

       }
#endregion
    }
}
