using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using System.Text.RegularExpressions;
using ATS_Framework;
using System.Windows.Forms;
using System.IO;

namespace ATS_Driver
{
  
    public class D86100:Scope
    {
        public IOPort MY_Scope;

        public D86100(logManager logmanager)
        {
            logger = logmanager;
        }
        public Algorithm algorithm = new Algorithm();


        public override bool Initialize(TestModeEquipmentParameters[] D86100Struct)
        {
            try
            {
               
                int i = 0;
                if (algorithm.FindFileName(D86100Struct,"ADDR",out i))
                {
                    Addr = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"IOTYPE",out i))
                {
                    IOType = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(D86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"OPTCHANNEL",out i))
                {
                    OptChannel = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"ELECCHANNEL",out i))
                {
                    ElecChannel = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"SCALE",out i))
                {
                    Scale = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"OFFSET",out i))
                {
                    Offset = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"DCAATT",out i))
                {
                    DcaAtt = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct, "OPTICALMASKNAME", out i))
                {
                    opticalMaskName = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct, "ELECMASKNAME", out i))
                {
                    elecMaskName = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"FILTERFREQ",out i))
                {
                    FilterFreq = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"PERCENTAGE",out i))
                {
                    Percentage =Convert.ToByte( D86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"DCATHRESHOLD",out i))
                {
                    DcaThreshold = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"TRIGGERBWLIMIT",out i))
                {
                    TriggerBwlimit = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"DCAWAVELENGTH",out i))
                {
                    DcaWavelength = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"DCADATARATE",out i))
                {
                    DcaDataRate = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"NAME",out i))
                {
                    Name = D86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(D86100Struct,"WAVEFORMCOUNT",out i))
                {
                    WaveformCount = Convert.ToInt32(D86100Struct[i].DefaultValue);
                }
                else
                    return false;
                
                if (!Connect()) return false;

            }

            catch (Error_Message error)
            {
                logger.AdapterLogString(3, error.ToString());
                //throw new Error_Message(this.GetType().ToString() + " " + error.Message, error);
                return false;
            }
            return true;
        }

        private bool OpenCurrentChannel(string currentChannel)
        {
            bool isset = false;
            for (byte i = 1; i <= 4; i++)
            {
                if (i == Convert.ToInt16(currentChannel))
                {
                    isset = SetChannel(i.ToString(), true);
                }
                else
                {
                    isset = SetChannel(i.ToString(), false);
                }
            }

            return isset;
        }
       protected string dcacurrentchannel;
       override public bool OpenOpticalChannel(bool Switch)//true optical  false elec
       {
           if (Switch)
           {
               dcacurrentchannel = OptChannel;
               return OpenCurrentChannel(dcacurrentchannel);
           }
           else
           {
               dcacurrentchannel = ElecChannel;
               return OpenCurrentChannel(dcacurrentchannel);
           }
       }


       override public bool Connect()
       {
           try
           {

               switch (IOType)
               {
                   case "GPIB":

                       MY_Scope = new IOPort(IOType, "GPIB::" + Addr);

                       EquipmentConnectflag = MY_Scope.Connect_flag;

                       break;
                   default:
                       EquipmentConnectflag = false;
                       break;
               }

               return EquipmentConnectflag;
           }
           catch (Exception error)
           {
               EquipmentConnectflag = false;
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       protected bool ReSet()
       {
           if (MY_Scope.WriteString("*RST"))
           {
               Thread.Sleep(3000);
               return true;
           }
           else
           {
               return false;
           }

       }
       public override bool Configure()
       {


           try
           {

               if (EquipmentConfigflag)//曾经设定过
               {

                   return true;

               }
               else//未曾经设定过
               {
                   if (Reset == true)
                   {
                       ReSet();
                   }
                   SetMode(0);
                   SetChannel(OptChannel, true);
                   SetChannel(ElecChannel, true);
                   SetAttenuation(OptChannel, DcaAtt);
                   SetAttenuation(ElecChannel, DcaAtt);
                   SetOffset(OptChannel, Offset);
                   SetOffset(ElecChannel, Offset);
                   Setscale(OptChannel, Scale, DcaDataRate);
                   Setscale(ElecChannel, Scale, DcaDataRate);
                   SetTriggerBwlimit();
                   //DCA_Calibration(D86100Struct.OPT_Channel);
                   //LoadMask(opticalMaskName);
                   //LoadMask(elecMaskName);
                   FileterSelect(OptChannel, FilterFreq);
                   FileterSelect(ElecChannel, FilterFreq);
                   FileterSwitch(OptChannel, true);
                   FileterSwitch(ElecChannel, true);
                   SetMaskMargin(true);
                   WavelengthSelect(OptChannel, DcaWavelength);
                   WavelengthSelect(ElecChannel, DcaWavelength);
                   MaskONOFF(false);
                   ClearDisplay();
                   //SetChannel(OptChannel, true);
                   //SetChannel(ElecChannel, true);
                   //SetOffset(OptChannel, Offset);
                   //SetOffset(ElecChannel, Offset);
                   //Setscale(OptChannel, Scale, DcaDataRate);
                   //Setscale(ElecChannel, Scale, DcaDataRate);
                   //SetTriggerBwlimit();
                   //SetChannel(OptChannel, false);
                   //SetChannel(ElecChannel, false);
                   //SetChannel(OptChannel, true);
                   //SetChannel(ElecChannel, true);
                   SetDCAThreshold();
                   SetMaskAlignMethod(0);
                   OpenOpticalChannel(true);
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
       public override bool ChangeChannel(string channel)
       {

           string offset = ""; ;
           CurrentChannel = channel;
           if (offsetlist.ContainsKey(CurrentChannel))
               offset = offsetlist[CurrentChannel];
           logger.AdapterLogString(0, "Offset is" + offset);
           return SetAttenuation(dcacurrentchannel, offset);

       }
       public override bool configoffset(string channel, string offset)
       {
           offsetlist.Add(channel, offset);
           return true;
       }
       override public bool ClearDisplay()
       {
           try
           {
               return MY_Scope.WriteString("cdisplay"); //Reset the device
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }


       }
       override public bool SetMode(byte Mode)
       {//0="EYE";1="OSC";2="TDR" 
           bool flag = false;
           string index;
           try
           {
               switch (Mode)
               {
                   case 0:
                       index = "EYE";
                       break;
                   case 1:
                       index = "OSC";
                       break;
                   case 2:
                       index = "TDR";
                       break;
                   default:
                       index = "EYE";
                       break;
               }
               flag = MY_Scope.WriteString(":system:mode " + index);
               logger.AdapterLogString(0, "DCA Mode is" + index);
               return flag;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }


       }
      
       private bool SetChannel(string channel, bool onoff)
       {
           bool flag = false;

           try
           {
               if (onoff)
               {
                   flag = MY_Scope.WriteString(":channel" + channel + ":display 1;" + ":measure:source " + "channel" + channel.ToString());
               }
               else
               {
                   flag = MY_Scope.WriteString(":channel" + channel + ":display 0;" + ":measure:source " + "channel" + channel.ToString());
               }
               logger.AdapterLogString(0, "channel is" + channel);
               return flag;

           }

           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private bool SetRefPos(byte pos)//pos=0 left pos =1 center
       {
           try
           {
               switch (pos)
               {
                   case 0:
                       return MY_Scope.WriteString(":timebase:reference left");
                   case 1:
                       return MY_Scope.WriteString(":timebase:reference center");
                   default:
                       return MY_Scope.WriteString(":timebase:reference left");

               }
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }


       }
       private bool SetPos(double pos)
       {

           try
           {
               return MY_Scope.WriteString(":timebase:position " + pos.ToString());

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private bool SetHistMod(bool onoff)
       {
           string strON_OFF;
           if (onoff)
           {
               strON_OFF = "1";
           }
           else
           {
               strON_OFF = "0";
           }
           try
           {
               return MY_Scope.WriteString(":histogram:mode " + strON_OFF);

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }


       }
       private bool SetHistSource()
       {

           try
           {
               return MY_Scope.WriteString(":histogram:window:source channel" + dcacurrentchannel);

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }

       private bool SetHistWindow(double x1, double x2, double y1, double y2)
       {

           MY_Scope.WriteString(":histogram:window:x1position " + x1.ToString());
           MY_Scope.WriteString(":histogram:window:x2position " + x2.ToString());
           MY_Scope.WriteString(":histogram:window:y1position " + y1.ToString());
           MY_Scope.WriteString(":histogram:window:y2position " + y2.ToString());
           return true;

       }
       private bool GetHistMedian(out double median)
       {
           if (MY_Scope.WriteString(":measure:histogram:median?"))
           {
               string strMedian;
               strMedian = MY_Scope.ReadString(24);
               median = Convert.ToDouble(strMedian);
               return true;
           }
           else
           {
               median = 0;
               return false;
           }

       }
       private bool SetTimeDelay(double delayTime)
       {
           try
           {
               return MY_Scope.WriteString(":timebase:delay " + delayTime.ToString());

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }
       private bool GetTimeDelay(out double delayTime)
       {

           MY_Scope.WriteString(":timebase:delay? ");
           string strDelayTime;
           strDelayTime = MY_Scope.ReadString(32);
           delayTime = Convert.ToDouble(strDelayTime);


           return true;
       }
       private bool CenterEye()
       {
           double freq = Convert.ToDouble(DcaDataRate);
           double tempAvgWat = 0;
           double median = 0.00;
           double delayTime = 0.000E+0;
           SetRefPos(0);//pos=0 left; pos =1 center
           SetPos(2.40E-8);
           SetHistMod(true);
           MY_Scope.WriteString(":Hist:axis horizontal");
           SetHistSource();

           switch (dcacurrentchannel)
           {
               case "1":
               case "3":
                   {
                       tempAvgWat = GetAveragePowerWatt();
                       SetHistWindow(2.40E-8, (1 / freq) + 2.40E-8, tempAvgWat - 1.00E-6, tempAvgWat + 1.00E-6);
                       break;
                   }

               case "2":
               case "4":
                   {
                       SetHistWindow(2.40E-8, (1 / freq + 2.40E-8), -0.01, 0.01);
                       break;
                   }

               default:
                   {
                       break;
                   }

           }

           Thread.Sleep(200);
           GetHistMedian(out median);
           Thread.Sleep(200);
           SetRefPos(1);//pos=0 left; pos =1 center  
           Thread.Sleep(200);
           SetPos((1 / freq) + median);
           GetTimeDelay(out delayTime);
           SetTimeDelay(delayTime + (1 / freq) / 2.00);
           SetHistMod(false);
           Thread.Sleep(500);
           return true;
       }
       
       override public bool AutoScale()
       {
           try
           {
               return MY_Scope.WriteString(":autoscale " + DcaDataRate.ToString());
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       override public  bool RunStop(bool run)// true "RUN"  false  "Stop"
       {

           if (run)
           {
               try
               {
                   return MY_Scope.WriteString(":" + "RUN");

               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }

           }
           else
           {
               try
               {
                   return MY_Scope.WriteString(":" + "STOP");

               }
               catch (Exception error)
               {
                   logger.AdapterLogString(3, error.ToString());
                   return false;
               }

           }

       }
      
       private bool WavelengthSelect(string channel, string Wavelength)//1 850,2 1310,3 1550 default 850
       {
           bool flag = false;
           string Wavelength_index = "";
           try
           {
               switch (Wavelength)
               {
                   case "1":
                       Wavelength_index = "850";
                       break;
                   case "2":
                       Wavelength_index = "1310";
                       break;
                   case "3":
                       Wavelength_index = "1550";
                       break;
                   default:
                       Wavelength_index = "850";
                       break;
               }
               flag = MY_Scope.WriteString(":Channel" + channel + ":WAVelength" + " WAV" + Wavelength);
               logger.AdapterLogString(0, "channel is" + channel + "Wavelength is" + Wavelength_index);
               return flag;

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }

       private bool SetruntilMod(int waveformcount, byte offwaveformsample)// 0="OFF" 1="WAV" 2="SAMP"
       {
           string strTemp = null;
           switch (offwaveformsample)
           {
               case 0:
                   strTemp = "OFF";
                   break;
               case 1:
                   strTemp = "WAV,";
                   break;
               case 2:
                   strTemp = "SAMP,";
                   break;
               default:
                   break;
           }
           if (strTemp != "OFF")
           {

               return MY_Scope.WriteString("acq:runtil " + strTemp + waveformcount.ToString());

           }
           else
           {
               return MY_Scope.WriteString("acq:runtil off");

           }

       }

       private bool ClearMesures()
       {
           try
           {
               return MY_Scope.WriteString(":measure:clear");
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private double GetCrossing()
       {
           try
           {

               if (MY_Scope.WriteString(":measure:cgrade:crossing " + "Channel" + dcacurrentchannel))
               {
                   MY_Scope.WriteString("MEASure:CGRade:CROSsing?");
                   string S = MY_Scope.ReadString(400);
                   return Convert.ToDouble(S);
               }
               else
               {
                   return 10000;
               }
           }
           catch
           {
               return 10000;
           }
       }
      
       override public double GetEratio()
       {

           try
           {
               MY_Scope.WriteString(":measure:cgrade:eratio dec");
               if (MY_Scope.WriteString(":MEASURE:CGRADE:ERATIO? DECibel," + "channel" + dcacurrentchannel))
               {

                   string STR = MY_Scope.ReadString(400);
                   return Convert.ToDouble(STR);
               }
               else
               {
                   return 10000;
               }
           }
           catch
           {
               return 10000;
           }
       }
      
       override public bool DisplayER()
       {
           try
           {
               return MY_Scope.WriteString(":measure:cgrade:eratio dec");
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private bool DisplayEyeAmplitude()
       {
           try
           {
               return MY_Scope.WriteString(":measure:cgrade:amplitude");
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private double ReadEyeAmplitude()
       {
           MY_Scope.WriteString(":measure:cgrade:amplitude?");
           string S = MY_Scope.ReadString(15);
           return Convert.ToDouble(S);

       }

       private bool DisplayCrossing()
       {

           if (MY_Scope.WriteString(":measure:cgrade:crossing channel" + dcacurrentchannel))
           {
               return true;
           }
           else
           {
               return false;
           }
       }

       private bool WaitUntilComplete()
       {
           int waveForm = 0;
           string tempString = null;
           MY_Scope.WriteString(":ALER?");
           tempString = MY_Scope.ReadString(32);
           do
           {
               MY_Scope.WriteString(":ALER?");
               tempString = MY_Scope.ReadString(32);
               waveForm = Convert.ToInt32(tempString);
               Thread.Sleep(1000);
           } while ((waveForm & 1) <= 0);

           return true;


       }
       //new add end
       private double GetJitterPP()
       {
           try
           {

               MY_Scope.WriteString(":measure:CGRADE:JITTER pp," + "Channel" + dcacurrentchannel);

               if (MY_Scope.WriteString(":measure:CGRADE:JITTER? pp," + "Channel" + dcacurrentchannel))
               {
                   string Str = MY_Scope.ReadString(400);
                   return Convert.ToDouble(Str) * 1000000000000;
               }
               else
               {
                   return 100000;
               }
           }
           catch
           {
               return 100000;
           }
       }

       private double GetJitterRMS()
       {
           try
           {
               MY_Scope.WriteString(":measure:CGRADE:JITTER RMS," + "Channel" + dcacurrentchannel);
               if (MY_Scope.WriteString(":measure:CGRADE:JITTER? RMS," + "Channel" + dcacurrentchannel))
               {
                   string Str = MY_Scope.ReadString();
                   return Convert.ToDouble(Str) * 1000000000000;
               }
               else
               {
                   return 100000;
               }
           }
           catch
           {
               return 100000;
           }
       }

       private double GetFalltime()
       {
           try
           {//":measure:CGRADE:failtime "
               if (MY_Scope.WriteString(":measure:falltime " + "Channel" + dcacurrentchannel))
               {
                   MY_Scope.WriteString(":MEASure:FALLtime?");

                   string Str = MY_Scope.ReadString(50);
                   return Convert.ToDouble(Str) * 1000000000000;
               }
               else
               {
                   return 10000;
               }
           }
           catch
           {
               return 10000;
           }
       }


       private double GetRisetime()
       {
           try
           {

               if (MY_Scope.WriteString(":measure:Risetime channel" + dcacurrentchannel))
               {
                   MY_Scope.WriteString(":MEASure:RISEtime?");
                   string Str = MY_Scope.ReadString(50);
                   return Convert.ToDouble(Str) * 1000000000000;

               }
               else
               {
                   return 10000;
               }
           }
           catch
           {
               return 10000;
           }
       }
       
       override public double GetAveragePowerWatt()
       {
           string strAvgPower = null;
           MY_Scope.WriteString(":measure:apower? WATT, channel" + dcacurrentchannel);
           strAvgPower = MY_Scope.ReadString(16);
           return Convert.ToDouble(strAvgPower) * 1000000;
       }

       override public double GetAveragePowerdbm()
       {

           try
           {

               if (MY_Scope.WriteString(":measure:apow dec," + "Channel" + dcacurrentchannel))
               {
                   MY_Scope.WriteString(":measure:apower? decibel," + "Channel" + dcacurrentchannel);
                   return Convert.ToDouble(MY_Scope.ReadString(400));
               }
               else
               {
                   return 10000;
               }
           }
           catch
           {
               return 10000;
           }

       }
       public override bool DisplayPowerWatt()
       {
           try
           {
               MY_Scope.WriteString(":measure:apower WATT, channel" + dcacurrentchannel);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       public override bool DisplayPowerdbm()
       {
           try
           {
               MY_Scope.WriteString(":measure:apow dec," + "Channel" + dcacurrentchannel);
               return true;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }
       }
       
       private bool FileterSwitch(string channel, bool Swith)
       {
           string index;
           try
           {
               if (Swith)
                   index = "ON";
               else
                   index = "OFF";
               return MY_Scope.WriteString(":channel" + channel + ":Filter " + index);
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private bool SaveEyeDiagram()
       {
           byte[] bmpData;
           string savePath = null;
           string diagramName = null;
           MY_Scope.WriteString(":Disp:Data? BMP,SCREEN");

           //System.Threading.Thread.Sleep(2000);
           bmpData =(byte[])  MY_Scope.myDmm.ReadIEEEBlock(IEEEBinaryType.BinaryType_UI1);
           int len = bmpData.Length;
           savePath = Application.StartupPath + "\\EyeDiagram\\";
           if (!Directory.Exists(savePath))
           {
               Directory.CreateDirectory(savePath);
           }
           System.DateTime CurrentTime = new System.DateTime();
           CurrentTime = System.DateTime.Now;
           string Str_Time = CurrentTime.ToString();
           string Str_Year = CurrentTime.Year.ToString();
           string Str_Month = CurrentTime.Month.ToString();
           string Str_Day = CurrentTime.Day.ToString();
           string Str_Hour = CurrentTime.Hour.ToString();
           string Str_minute = CurrentTime.Minute.ToString();
           diagramName = Str_Year + "_" + Str_Month + "_" + Str_Day + "_" + Str_Hour + "_" + Str_minute + ".bmp";
           File.WriteAllBytes(@savePath + diagramName, bmpData);
           return true;
       }
       

       private bool FileterSelect(string channel, string FilterFreq)
       {
           bool flag = false;
           int i = 0;
           int Filterindex = 0;
           bool FileterExitflag = false;
           try
           {

               if (MY_Scope.WriteString(":channel" + channel + ":FDEScription?"))
               {
                   string Str1 = MY_Scope.ReadString();
                   string[] sArray = Regex.Split(Str1, ",", RegexOptions.IgnoreCase);
                   for (i = 1; i < sArray.Length; i++)
                   {
                       if (sArray[i] == FilterFreq + " Gb/s")
                       {
                           Filterindex = i;
                           FileterExitflag = true;
                       }
                   }
                   if (!FileterExitflag)
                   {

                       return false;
                   }

                   flag = MY_Scope.WriteString(":channel" + channel + ":FSELect FILTer" + Filterindex.ToString());
                   logger.AdapterLogString(0, "FilterFrequence is" + FilterFreq);
                   return flag;
               }
               return false;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }


       }
       private bool ClearMeasurements()
       {
           try
           {
               return MY_Scope.WriteString(":MEASure:CLEAR");

           }
           catch
           {
               return false;
           }
       }
       private bool SelectChannel()
       {
           bool flag = false;
           try
           {
               flag = MY_Scope.WriteString(":measure:source" + "Channel" + dcacurrentchannel);
               
               logger.AdapterLogString(0, "DCA channel is " + dcacurrentchannel);
               return flag;

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }

       private int Gettotalmaskerrors()
       {
           string strTotalErrors = "";
           try
           {
               MY_Scope.WriteString(":mtest:count:fsamples?");
               strTotalErrors = MY_Scope.ReadString(16);//  read 16bytes 
               // return Convert.ToInt32(strTotalErrors);
               double temp = Convert.ToDouble(strTotalErrors);
               return (int)temp;
           }
           catch
           {
               return -1000;
           }
       }
       private byte MaskTest(string maskname)
       {
           byte margin = 0; //new
           try
           {
               LoadMask(maskname);
               MaskONOFF(true);
               MaskAlign();
               RunStop(false);
               SetMaskMargin(true);
               margin = GetTestMargin(Percentage);
               SaveEyeDiagram();
               MaskONOFF(false);
               return margin;
           }
           catch
           {
               return 100;
           }
       }
       private int Getregionerrors(string region)//new
       {
           double regionErrors = 0;
           MY_Scope.WriteString(":mtest:count:failures? region" + region);
           MY_Scope.ReadString(32);
           regionErrors = Convert.ToDouble(MY_Scope.ReadString(32));
           return (int)regionErrors;
       }
       private bool SetMaskMargin(bool Switch)
       {
           bool flag = false;
           string stronoff = null;
           if (Switch)
           {
               stronoff = "1";
           }
           else
           {
               stronoff = "0";
           }

           try
           {
               flag = MY_Scope.WriteString(":mtest:mmargin:state " + stronoff + ";" + ":mtest:mmargin:perc " + Percentage.ToString());
               logger.AdapterLogString(0, "MaskMargin is" + Percentage.ToString());
               return flag;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }

       private bool LoadMask(string maskname)
       {
           bool flag = false;
           try
           {
               flag = MY_Scope.WriteString(":mtest:load " + "\"" + maskname + "\"");
               logger.AdapterLogString(0, "MaskName is" + maskname);
               return flag;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }


       }
       private bool MaskAlign()
       {
           for (byte i = 0; i < 2; i++)
           {
               //  myDmm.WriteString("",
               MY_Scope.WriteString(":mtest:align");
           }
           return true;
       }
      override public  bool SetMaskAlignMethod(byte method)//"0"DISPLAY,"1",EWINDOW
       {
           bool alignreturn;
           try
           {
               switch (method)
               {
                   case 0:

                       alignreturn = MY_Scope.WriteString(":mtest:yalign DISPLAY");
                       break;
                   case 1:
                       alignreturn = MY_Scope.WriteString(":mtest:yalign EWINDOW");
                       break;
                   default:
                       alignreturn = MY_Scope.WriteString(":mtest:yalign EWINDOW");
                       break;

               }
               return alignreturn;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       override public bool SetRunTilOff()
       {
           try
           {
               if (MY_Scope.WriteString(":acquire:runtil OFF"))
               {
                   return true;
               }
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               throw error;
           }

           return true;
       }
       override public bool MaskONOFF(bool MaskON) //true="ON"
       {
           try
           {
               //  myDmm.WriteString("",
               if (MaskON)//new
               {
                   if (MY_Scope.WriteString(":mtest:test " + "ON"))
                   {
                       Thread.Sleep(100);
                       return true;
                   }
                   else
                   {
                       return false;
                   }
               }
               else//new
               {
                   if (MY_Scope.WriteString(":mtest:test " + "OFF"))
                   {
                       Thread.Sleep(100);
                       return true;
                   }
                   else
                   {
                       return false;

                   }
               }


           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private bool MaskTestStart()
       {
           try
           {

               if (MY_Scope.WriteString(":mtest:start"))
               {
                   Thread.Sleep(100);
                   return true;
               }
               else
               {
                   return false;

               }
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       private byte GetTestMargin(byte iniMargin)
       {

           int totalErrors = 0;
           Percentage = iniMargin;
           do
           {
               bool iset = SetMaskMargin(true);
               Thread.Sleep(200);
               totalErrors = Gettotalmaskerrors();
               if (totalErrors==0)
               {
                   Percentage += 5;
               }
              
           } while ((totalErrors == 0) & (Percentage < 100));


           do
           {
               if (totalErrors > 0)
               {
                   Percentage -= 1;
               }
               
               SetMaskMargin(true);
               Thread.Sleep(200);
               totalErrors = Gettotalmaskerrors();

           } while ((totalErrors > 0) & (Percentage > 0) & (Percentage < 100));
           logger.AdapterLogString(0, "CurrentMargin is" + Percentage);
           return Percentage;
       }
       private double MaskGetWaveform()
       {

           double Total_Wfms_NO = 0, fail_Smpls = 0;

           string S1, S2;
           try
           {
               do
               {
                   // Clear_Display();
                   Thread.Sleep(200);
                   MY_Scope.WriteString(":mtest:count:waveforms?");

                   S1 = MY_Scope.ReadString();
                   // S1 = S1.Substring(0, S1.Length - 2);
                   Total_Wfms_NO = double.Parse(S1);


                   MY_Scope.WriteString(":mtest:count:fsamples?");

                   S2 = MY_Scope.ReadString();
                   fail_Smpls = double.Parse(S2);
               }
               while (fail_Smpls == 0 && Total_Wfms_NO < 700);
               return fail_Smpls;
           }
           catch
           {
               return 10000;
           }
       }
       

       private bool SetAttenuation(string channel, string DCAAttenuation)
       {
           bool flag = false;
           try
           {
               flag = MY_Scope.WriteString(":channel" + channel + ":PROBe " + DCAAttenuation + ",DECibel");
               logger.AdapterLogString(0, "channel is" + channel + "DCAAttenuation is" + DCAAttenuation);
               return flag;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       
       private bool SetOffset(string channel, string Offset)
       {
           try
           {

               return MY_Scope.WriteString(":channel" + channel + ":OFFSet" + Offset);

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       public override bool SetScaleOffset()
       {
           return true;

       }

       private bool Setscale(string channel, string Scale, string DataRate)
       {

           double j = (1 / Convert.ToDouble(DataRate)) * 0.22;
           try
           {

               if (MY_Scope.WriteString(":channel" + channel + ":scale" + Scale) &&
                MY_Scope.WriteString(":TIMEBASE:SCALE " + j) &&
                SetTriggerBwlimit())
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }

       private bool SetTriggerBwlimit()// 0 HIGH\1 LOW\2 DIV
       {
           bool flag = false;
           string str_TriggerBwlimit = "";
           try
           {
               switch (TriggerBwlimit)
               {
                   case "0":
                       str_TriggerBwlimit = "HIGH";
                       break;
                   case "1":
                       str_TriggerBwlimit = "LOW";
                       break;
                   case "2":
                       str_TriggerBwlimit = "DIV";
                       break;
                   default:
                       str_TriggerBwlimit = "DIV";
                       break;
               }
               flag = MY_Scope.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);
               logger.AdapterLogString(0, "TriggerBwlimit is" + str_TriggerBwlimit);
               return flag;
           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }

       private bool DCACalibration()
       {
           string Str_Mode;
           try
           {

               MY_Scope.WriteString(":Calibrate:Status?");
               string Str1 = MY_Scope.ReadString(20);
               Str1 = Str1.Substring(0, 1);
               if (Str1 == "1" || Str1 == "0")
               {
                   if (Convert.ToInt16(dcacurrentchannel) < 2)
                   {
                       Str_Mode = "lmodule";
                   }
                   else
                   {
                       Str_Mode = "rmodule";
                   }
                   MY_Scope.WriteString(":Calibrate:module:Status?  " + Str_Mode);
                   Thread.Sleep(50);
                   string status = MY_Scope.ReadString(30);
                   MY_Scope.WriteString(":Calibrate:module:vertical " + Str_Mode);
                   MY_Scope.WriteString(":Calibrate:sdone?");
                   Thread.Sleep(1000);
                   MY_Scope.WriteString(":Calibrate:continue");
                   MY_Scope.WriteString(":Calibrate:sdone?");
                   int i = 0;
                   do
                   {
                       Thread.Sleep(3000);
                       Str1 = MY_Scope.ReadString(1024);
                       i++;
                       if (i > 60) break;
                   }
                   while (Str1 == "");

                   MY_Scope.WriteString(":Calibrate:eratio:start " + "channel" + dcacurrentchannel);

                   MY_Scope.WriteString(":Calibrate:sdone?");
                   Thread.Sleep(1000);
                   MY_Scope.WriteString(":Calibrate:continue");

                   MY_Scope.WriteString(":Calibrate:sdone?");
                   i = 0;
                   do
                   {
                       Thread.Sleep(3000);
                       Str1 = MY_Scope.ReadString(1024);
                       i++;
                       if (i > 60) break;
                   }
                   while (Str1 == "");

                   return true;
               }
               else
               {
                   return false;
               }

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }


       private bool SetDCAThreshold()
       {
           bool flag = false;
           try
           {

               flag = MY_Scope.WriteString(":measure:define thresholds, percent, " + DcaThreshold);
               logger.AdapterLogString(0, "DCAThreshold is" + DcaThreshold);
               return flag;

           }
           catch (Exception error)
           {
               logger.AdapterLogString(3, error.ToString());
               return false;
           }

       }
       /// 测试光眼图，测量结果以数组形式返回，依次为AP,ER,OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
      
       public override double[] OpticalEyeTest()
       {
           double[] testResults = new double[9];
           try
           {
               SetMaskAlignMethod(1) ;
               SetMode(0);
               MaskONOFF(false);
               SetRunTilOff();
               RunStop(true);
               OpenOpticalChannel(true);
               AutoScale();
               CenterEye();
               AutoScale();
               SetruntilMod(WaveformCount,1);
               RunStop(true);
               ClearDisplay();
               DisplayER();
               DisplayEyeAmplitude();
               DisplayCrossing();
               Thread.Sleep(200);
               ClearDisplay();
               Thread.Sleep(200);
               WaitUntilComplete();
               byte marginVaulue = MaskTest(opticalMaskName);
               SetruntilMod(0, 0);
               RunStop(false);
               ClearMesures();
               RunStop(true);
               SetruntilMod(0, 0);
               AutoScale();
               Thread.Sleep(200);
               testResults[0] = GetAveragePowerdbm();
               testResults[1] = GetEratio();
               testResults[2] = GetAveragePowerWatt();
               testResults[3] = GetCrossing();
               testResults[4] = GetJitterPP();
               testResults[5] = GetJitterRMS();
               testResults[6] = GetRisetime();
               testResults[7] = GetFalltime();
               testResults[8] = Convert.ToDouble(marginVaulue);

               return testResults;
           }
           catch
           {
               return testResults;
           }
       
       }

       /// 测试电眼图，测量结果以数组形式返回，依次为OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
       /// </summary>
       /// <returns></returns>
       public override double[] ElecEyeTest()
       {
           double[] testResults = new double[9];
           try
           {

               SetMaskAlignMethod(1);
               SetMode(0);
               MaskONOFF(false);
               SetRunTilOff();
               RunStop(true);
               OpenOpticalChannel(false);
               AutoScale();
               CenterEye();
               AutoScale();
               SetruntilMod(WaveformCount, 1);
               RunStop(true);
               ClearDisplay();

               DisplayER();
               DisplayEyeAmplitude();
               DisplayCrossing();
               Thread.Sleep(200);
               ClearDisplay();
               Thread.Sleep(200);
               WaitUntilComplete();
               byte marginVaulue = MaskTest(elecMaskName);
               SetruntilMod(0, 0);
               RunStop(false);
               ClearMesures();
               RunStop(true);
               SetruntilMod(0, 0);
               AutoScale();
               Thread.Sleep(200);

               testResults[0] = GetAveragePowerWatt();
               testResults[1] = GetCrossing();
               testResults[2] = GetJitterPP();
               testResults[3] = GetJitterRMS();
               testResults[4] = GetRisetime();
               testResults[5] = GetFalltime();
               testResults[6] = Convert.ToDouble(marginVaulue);

               return testResults;
           }
           catch
           {
               return testResults;
           }
       }
    }

}