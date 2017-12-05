using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using ATS_Framework;
using System.IO;
using System.Windows.Forms;

namespace Driver
{
   public class N4960PPG:PPG
    {

      
           public Algorithm algorithm = new Algorithm();

           public N4960PPG(logManager logmanager)
           {
               logger = logmanager;
           }
           public override bool Initialize(TestModeEquipmentParameters[] N4960PPGStruct)
           {

               int i = 0;
               if (algorithm.FindFileName(N4960PPGStruct, "ADDR", out i))
               {
                   Addr = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no ADDR");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "IOTYPE", out i))
               {
                   IOType = N4960PPGStruct[i].DefaultValue;
               }
               else
               {
                   logger.AdapterLogString(4, "there is no IOTYPE");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "RESET", out i))
               {
                   Reset = Convert.ToBoolean(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no RESET");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "NAME", out i))
               {
                   Name = N4960PPGStruct[i].DefaultValue;
               }
               else
               {
                   logger.AdapterLogString(4, "there is no NAME");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATARATE", out i))
               {
                   dataRate = N4960PPGStruct[i].DefaultValue;
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATARATE");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATALEVELGUARDAMPMAX", out i))
               {
                   dataLevelGuardAmpMax = Convert.ToDouble(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATALEVELGUARDAMPMAX");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATALEVELGUARDOFFSETMAX", out i))
               {
                   dataLevelGuardOffsetMax = Convert.ToDouble(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATALEVELGUARDOFFSETMAX");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATALEVELGUARDOFFSETMIN", out i))
               {
                   dataLevelGuardOffsetMin = Convert.ToDouble(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATALEVELGUARDOFFSETMIN");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATAAMPLITUDE", out i))
               {
                   dataAmplitude = Convert.ToDouble(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATAAMPLITUDE");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATACROSSPOINT", out i))
               {
                   dataCrossPoint = Convert.ToDouble(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATACROSSPOINT");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "CONFIGFILEPATH", out i))
               {
                   configFilePath = N4960PPGStruct[i].DefaultValue;
               }
               else
               {
                   logger.AdapterLogString(4, "there is no CONFIGFILEPATH");
                   return false;
               }

               if (algorithm.FindFileName(N4960PPGStruct, "SLOT", out i))
               {
                   slot = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no SLOT");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "CLOCKSOURCE", out i))
               {
                   clockSource = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no CLOCKSOURCE");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "AUXOUTPUTCLKDIV", out i))
               {
                   auxOutputClkDiv = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no AUXOUTPUTCLKDIV");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "TOTALCHANNEL", out i))
               {
                   totalChannels = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no TOTALCHANNEL");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "PRBSLENGTH", out i))
               {
                   prbsLength = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no PRBSLENGTH");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "PATTERNTYPE", out i))
               {
                   patternType = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no PATTERNTYPE");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATASWITCH", out i))
               {
                   dataSwitch = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATASWITCH");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATATRACKINGSWITCH", out i))
               {
                   dataTrackingSwitch = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATATRACKINGSWITCH");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATALEVELGUARDSWITCH", out i))
               {
                   dataLevelGuardSwitch = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATALEVELGUARDSWITCH");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATAACMODESWITCH", out i))
               {
                   dataAcModeSwitch = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATAACMODESWITCH");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "DATALEVELMODE", out i))
               {
                   dataLevelMode = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DATALEVELMODE");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "CLOCKSWITCH", out i))
               {
                   clockSwitch = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no CLOCKSWITCH");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "OUTPUTSWITCH", out i))
               {
                   outputSwitch = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no OUTPUTSWITCH");
                   return false;
               }
               if (algorithm.FindFileName(N4960PPGStruct, "PATTERNFILE", out i))
               {
                   patternfile = N4960PPGStruct[i].DefaultValue;
               }
               else
               {
                   logger.AdapterLogString(4, "there is no PATTERNFILE");
                   return false;
               }
               if (!Connect()) return false;
               return true;
           }
           public bool ReSet()
           {
               if (MyIO.WriteString("*RST"))
               {
                   Thread.Sleep(3000);
                   return true;
               }
               else
               {
                   return false;
               }

           }
           public override bool ChangeChannel(string channel, int syn = 0)
           {
               byte channelbyte = Convert.ToByte(channel);

               return ConfigureChannel(channelbyte);
           }
           public override bool configoffset(string channel, string offset, int syn = 0)
           {
               return true;
           }
           public override bool Connect()
           {
               try
               {
                   // IO_Type

                   switch (IOType)
                   {
                       case "GPIB":
                           MyIO = new IOPort(IOType, "GPIB::" + Addr.ToString(), logger);
                           MyIO.IOConnect();
                           MyIO.WriteString("*IDN?");
                           EquipmentConnectflag = MyIO.ReadString().Contains("1800");
                           break;
                       default:
                           logger.AdapterLogString(4, "GPIB类型错误");
                           EquipmentConnectflag = false;
                           break;
                   }

                   return EquipmentConnectflag;
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           public override bool Configure(int syn = 0)
           {
               try
               {

                   if (EquipmentConfigflag)//曾经设定过
                   {
                       return true;
                   }
                   else//未曾经设定过
                   {
                       //ReSet();
                       if (Reset == true)
                       {
                           ReSet();
                       }
                    
                       ConfigureOutputSwitch(outputSwitch, syn);
                       EquipmentConfigflag = true;
                   }
                   return true;

               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           /// <summary>
           /// 按照设备属性中配置文件地址对设备进行配置
           /// </summary>
           /// <param name="SetupFile"></param>
           /// <returns></returns>
           private bool ConfigureFromFile()
           {
               try
               {
                   if (EquipmentConfigflag)//曾经设定过
                   {
                       return true;
                   }
                   else
                   {
                       byte b = 165;

                       string x = ":SYSTem:MMEMory:QRECall " + "\"" + configFilePath.Replace("\\", ((char)b).ToString()) + "\"" + "\n";
                       MyIO.WriteString(x);
                       EquipmentConfigflag = true;
                   }
                   return true;
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }

           private bool SetCrossing()
           {
               double CrossingPoint=25;
             return  MyIO.WriteString(":PG:DATA:XOV " + CrossingPoint);
           }
           private bool ConfigureDataRate(int syn = 0)//PPG比特率，单位为Gbps.
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "MP1800ppg dataRate is" + dataRate);
                       return MyIO.WriteString(":OUTPut:DATA:BITRate " + dataRate);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:BITRate " + dataRate);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:BITRate?");
                               readtemp = MyIO.ReadString();
                               if (Convert.ToDouble(readtemp).ToString("0.00") == Convert.ToDouble(dataRate).ToString("0.00"))
                                   break;
                           }
                           if (k < 3)
                           {
                               logger.AdapterLogString(0, "MP1800ppg dataRate is" + dataRate);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set MP1800ppg dataRate wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureChannel(byte channel, int syn = 0)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "MP1800ppg channel is" + channel);
                       return MyIO.WriteString(":INTerface:ID " + channel.ToString());
                   }
                   else
                   {

                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":INTerface:ID " + channel.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":INTerface:ID?");
                               readtemp = MyIO.ReadString();
                               if (readtemp == channel.ToString() + "\n")
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "MP1800ppg channel is" + channel);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set MP1800ppg channel wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool SetDataLogicLeve_Offset( double OffsetVoltage)
           {             
               MyIO.WriteString(":PG:DATA:LLEVel:OFFSet "+OffsetVoltage+"V");


               MyIO.WriteString(":PG:DATA:LLEVel:OFFSet?");

               string Str=  MyIO.ReadString();

               if (Str == OffsetVoltage+"V")
               {
                  return true;
               }
               else
               {
                   return false;
               }

              
           }
           private bool PPG_Set_Data_termination(double Termination)
           {

               MyIO.WriteString(":PG:DATA:LLEV:TERM " + Termination + "V");


               MyIO.WriteString(":PG:DATA:LLEV:TERM?");

               string Str = MyIO.ReadString();

               if (Str == Termination + "V")
               {
                  return true;
               }
               else
               {
                   return false;
               }

              
          
           }
           public override bool OutPutSwitch(bool Switch, int syn = 0)
           {
               string StrSwitch;

               if (Switch)
               {
                   StrSwitch="ON";
               }
               else
               {
                   StrSwitch="OFF";
               }


               MyIO.WriteString(":PG:DATA:OUTP" + StrSwitch);
               return true;
           }
           private bool SetAmplitude(double Amplitude)//单位是V
           {

               MyIO.WriteString(":PG:DATA:LLEVel:AMPLitude" + Amplitude+"V");
               return true;
           }
           private bool SetPRBS()//单位是V
           {
              // :PG:DATA:PATT:NAME 
               MyIO.WriteString(":PG:DATA:PATT:NAME PRBS2^" + prbsLength+"-1");
              MyIO.WriteString(":PG:DATA:PATT:NAME ?");
              string Str = MyIO.ReadString();
              if (Str.Contains("PRBS2^" + prbsLength + "-1"))
              {
                  return true;
              }
              else
              { 
                  return false;
              }
              
           }
      
           public override bool ConfigureETxPolarity(bool polarity)
           {
               //:PG:DATA:PATT:POL INV
               string StrPolarity;

              if (polarity)
              {
                  StrPolarity = "INV";
              }
               else
              {
                  StrPolarity = "NONI";
              }

              // polarity

             return  MyIO.WriteString(":PG:DATA:PATT:POL" + "StrPolarity");
           }
        
           private bool ConfigureDataTracking(byte dataTrackingSwitch, int syn = 0)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               string strDataTrackingSwitch;
               switch (dataTrackingSwitch)
               {
                   case 0:
                       strDataTrackingSwitch = "OFF";
                       break;
                   case 1:
                       strDataTrackingSwitch = "ON";
                       break;
                   default:
                       strDataTrackingSwitch = "ON";
                       break;
               }
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataTracking is" + strDataTrackingSwitch);
                       return MyIO.WriteString(":OUTPut:DATA:TRACking " + strDataTrackingSwitch);

                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:TRACking " + strDataTrackingSwitch);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:TRACking?");
                               readtemp = MyIO.ReadString();
                               if (readtemp == dataTrackingSwitch.ToString() + "\n")
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataTracking is" + strDataTrackingSwitch);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "DataTracking wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }//0=TrcakingOff,1=TrackingON
           private bool ConfigureDataLevelGuardAmpMax(double ampMax, int syn = 0)//ampMax单位为mV
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataLevelGuardAmpMax is" + (ampMax / 1000).ToString());

                       return MyIO.WriteString(":OUTPut:DATA:LIMitter:AMPLitude " + (ampMax / 1000).ToString());
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:LIMitter:AMPLitude " + (ampMax / 1000).ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:LIMitter:AMPLitude?");
                               readtemp = MyIO.ReadString();
                               if (Convert.ToDouble(readtemp) == Convert.ToDouble((ampMax / 1000).ToString()))
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataLevelGuardAmpMax is" + (ampMax / 1000).ToString());
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set DataLevelGuardAmpMax wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureDataLevelGuardOffset(double offsetMax, double offsetMin, int syn = 0)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataLevelGuardoffsetMax is" + (offsetMax / 1000).ToString() + "DataLevelGuardoffsetMin is" + (offsetMin / 1000).ToString());
                       return MyIO.WriteString(":OUTPut:DATA:LIMitter:OFFSet " + (offsetMax / 1000).ToString() + "," + (offsetMin / 1000).ToString());


                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:LIMitter:OFFSet " + (offsetMax / 1000).ToString() + "," + (offsetMin / 1000).ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:LIMitter:OFFSet?");
                               readtemp = MyIO.ReadString();
                               double temp1 = Convert.ToDouble((offsetMax / 1000));
                               double temp2 = Convert.ToDouble((offsetMin / 1000));
                               if (readtemp == temp1.ToString("0.000") + "," + temp2.ToString("0.000") + "\n")
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataLevelGuardoffsetMax is" + (offsetMax / 1000).ToString() + "DataLevelGuardoffsetMin is" + (offsetMin / 1000).ToString());
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set DataLevelGuardoffset wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }//形参单位全部为mV
           private bool ConfigureDataLevelGuardSwitch(byte lvGuardSwitch, int syn = 0)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               string strLvGuardSwitch;
               switch (lvGuardSwitch)
               {
                   case 0:
                       strLvGuardSwitch = "OFF";
                       break;
                   case 1:
                       strLvGuardSwitch = "ON";
                       break;
                   default:
                       strLvGuardSwitch = "ON";
                       break;
               }
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataLevelGuardSwitch is" + strLvGuardSwitch);
                       return MyIO.WriteString(":OUTPut:DATA:LEVGuard " + strLvGuardSwitch);

                   }
                   else
                   {

                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:LEVGuard " + strLvGuardSwitch);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:LEVGuard?");
                               readtemp = MyIO.ReadString();
                               if (readtemp == lvGuardSwitch.ToString() + "\n")
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataLevelGuardSwitch is" + strLvGuardSwitch);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set DataLevelGuardSwitch wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureDataAcModeSwitch(byte acSwitch, int syn = 0)//0=DC Mode，1=AC Mode
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               string strAcSwitch;
               switch (acSwitch)
               {
                   case 0:
                       strAcSwitch = "OFF";
                       break;
                   case 1:
                       strAcSwitch = "ON";
                       break;
                   default:
                       strAcSwitch = "ON";
                       break;
               }
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataAcModeSwitch is" + strAcSwitch);
                       return MyIO.WriteString(":OUTPut:DATA:AOFFset " + strAcSwitch);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:AOFFset " + strAcSwitch);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:AOFFset?");
                               readtemp = MyIO.ReadString();
                               if (readtemp == acSwitch.ToString())
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataAcModeSwitch is" + strAcSwitch);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set DataAcModeSwitch wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureDataLevelMode(byte elecLevelMode, int syn = 0)//0=VARiable,1=NECL,2=PCML,3=NCML,4=SCFL,5=LVPecl,6=LVDS200,7=LVDS400
           {//无6 7选项,mode选择与levelguard范围有关
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               string strElecLevelMode;
               switch (elecLevelMode)
               {
                   case 0:
                       strElecLevelMode = "VAR";
                       break;
                   case 1:
                       strElecLevelMode = "NECL";
                       break;
                   case 2:
                       strElecLevelMode = "PCML";
                       break;
                   case 3:
                       strElecLevelMode = "NCML";
                       break;
                   case 4:
                       strElecLevelMode = "SCFL";
                       break;
                   case 5:
                       strElecLevelMode = "LVP";
                       break;
                   //case 6:
                   //    strElecLevelMode = "LVDS200";
                   //    break;
                   //case 7:
                   //    strElecLevelMode = "LVDS400";
                   //    break;
                   default:
                       strElecLevelMode = "VAR";
                       break;
               }
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataLevelMode is" + strElecLevelMode);
                       return MyIO.WriteString(":OUTPut:DATA:LEVel DATA," + strElecLevelMode);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:LEVel DATA," + strElecLevelMode);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:LEVel? DATA");
                               readtemp = MyIO.ReadString();
                               if (readtemp == strElecLevelMode + "\n")
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataLevelMode is" + strElecLevelMode);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set DataLevelMode wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureDataAmplitude(double amplitude, int syn = 0)//单位为mV
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataAmplitude is" + (amplitude / 1000).ToString());
                       return MyIO.WriteString(":OUTPut:DATA:AMPLitude DATA," + (amplitude / 1000).ToString());
                   }
                   else
                   {

                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:AMPLitude DATA," + (amplitude / 1000).ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:AMPLitude? DATA");
                               readtemp = MyIO.ReadString();
                               if (Convert.ToDouble(readtemp) == Convert.ToDouble((amplitude / 1000).ToString() + "\n"))
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataAmplitude is" + (amplitude / 1000).ToString());
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set DataAmplitude wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureDataCrossPoint(double crossPoint, int syn = 0)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "DataCrossPoint is" + crossPoint.ToString());
                       return MyIO.WriteString(":OUTPut:DATA:CPOint DATA," + crossPoint.ToString());
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:DATA:CPOint DATA," + crossPoint.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:DATA:CPOint? DATA");
                               readtemp = MyIO.ReadString();
                               if (Convert.ToDouble(readtemp) == Convert.ToDouble(crossPoint.ToString() + "\n"))
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "DataCrossPoint is" + crossPoint.ToString());
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set DataCrossPoint wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureClockSwitch(byte clkSwitch, int syn = 0)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               string strClkSwitch;
               switch (clkSwitch)
               {
                   case 0:
                       strClkSwitch = "OFF";
                       break;
                   case 1:
                       strClkSwitch = "ON";
                       break;
                   default:
                       strClkSwitch = "ON";
                       break;
               }
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "ClockSwitch is" + strClkSwitch);
                       return MyIO.WriteString(":OUTPut:CLOCk:OUTPut " + strClkSwitch);

                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":OUTPut:CLOCk:OUTPut " + strClkSwitch);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":OUTPut:CLOCk:OUTPut?");
                               readtemp = MyIO.ReadString();
                               if (readtemp == clkSwitch.ToString() + "\n")
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "ClockSwitch is" + strClkSwitch);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set ClockSwitch wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }
           private bool ConfigureOutputSwitch(byte outSwitch, int syn = 0)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               string strOutSwitch;
               switch (outSwitch)
               {
                   case 0:
                       strOutSwitch = "OFF";
                       break;
                   case 1:
                       strOutSwitch = "ON";
                       break;
                   default:
                       strOutSwitch = "ON";
                       break;
               }
               try
               {
                   if (syn == 0)
                   {
                       logger.AdapterLogString(0, "OutputSwitch is" + strOutSwitch);
                       return MyIO.WriteString(":SOURce:OUTPut:ASET " + strOutSwitch);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = MyIO.WriteString(":SOURce:OUTPut:ASET " + strOutSwitch);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               MyIO.WriteString(":SOURce:OUTPut:ASET?");
                               readtemp = MyIO.ReadString();
                               if (readtemp == outSwitch.ToString() + "\n")
                                   break;
                           }
                           if (k <= 3)
                           {
                               logger.AdapterLogString(0, "OutputSwitch is" + strOutSwitch);
                               flag = true;
                           }
                           else
                           {
                               logger.AdapterLogString(3, "set OutputSwitch wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }
           }

       }
    }

