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
    /// <summary>
    /// 类名:D86100
    /// 由于实验室已经没有这个型号设备i，所以报错系统不需要填写
    /// </summary>
    public class D86100:Scope
    {
        private static object syncRoot = new object();//used for thread synchronization


        public override bool Initialize(TestModeEquipmentParameters[] D86100Struct)
        {
            try
            {
                int i = 0;
                if (Algorithm.FindFileName(D86100Struct,"ADDR",out i))
                {
                    Addr = Convert.ToByte(D86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"IOTYPE",out i))
                {
                    IOType = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(D86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"OPTCHANNEL",out i))
                {
                    OptChannel = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"ELECCHANNEL",out i))
                {
                    ElecChannel = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no ELECCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"SCALE",out i))
                {
                    Scale = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no SCALE");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"OFFSET",out i))
                {
                    Offset = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OFFSET");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"DCAATT",out i))
                {
                    DcaAtt = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DCAATT");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct, "OPTICALMASKNAME", out i))
                {
                    opticalMaskName = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTICALMASKNAME");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct, "ELECMASKNAME", out i))
                {
                    elecMaskName = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no ELECMASKNAME");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"FILTERFREQ",out i))
                {
                    FilterFreq = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no FILTERFREQ");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"PERCENTAGE",out i))
                {
                    Percentage =Convert.ToByte( D86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no PERCENTAGE");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"DCATHRESHOLD",out i))
                {
                    DcaThreshold = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DCATHRESHOLD");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"TRIGGERBWLIMIT",out i))
                {
                    TriggerBwlimit = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no TRIGGERBWLIMIT");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"DCAWAVELENGTH",out i))
                {
                    DcaWavelength = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DCAWAVELENGTH");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"DCADATARATE",out i))
                {
                    DcaDataRate = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DCADATARATE");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"NAME",out i))
                {
                    Name = D86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct,"WAVEFORMCOUNT",out i))
                {
                    WaveformCount = Convert.ToInt32(D86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no WAVEFORMCOUNT");
                    return false;
                }
                if (Algorithm.FindFileName(D86100Struct, "SETSCALEDELAY", out i))
                {
                    setscaledelay = Convert.ToInt32(D86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no SETSCALEDELAY");
                    return false;
                }
                if (!Connect()) return false;

            }

            catch (InnoExCeption error)
            {
                Log.SaveLogToTxt("ErrorCode=" + error.ID + "Reason=" + error.TargetSite.Name + "Fail");
                throw error;
            }

            catch (Exception error)
            {

                Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                // throw new InnoExCeption(ex);
            }
            return true;
        }

        private bool OpenCurrentChannel(string currentChannel,int syn=0)
        {
            lock (syncRoot)
            {
                bool isset = false;
                for (byte i = 1; i <= 4; i++)
                {
                    if (i == Convert.ToInt16(currentChannel))
                    {
                        isset = SetChannel(i.ToString(), true, syn);
                    }
                    else
                    {
                        isset = SetChannel(i.ToString(), false, syn);
                    }
                }

                return isset;
            }
        }
       protected string dcacurrentchannel;
       override public bool OpenOpticalChannel(bool Switch,int syn=0)//true optical  false elec
       {
           lock (syncRoot)
           {
               if (Switch)
               {
                   dcacurrentchannel = OptChannel;
                   return OpenCurrentChannel(dcacurrentchannel, syn);
               }
               else
               {
                   dcacurrentchannel = ElecChannel;
                   return OpenCurrentChannel(dcacurrentchannel, syn);
               }
           }
       }


       override public bool Connect()
       {
           try
           {

               switch (IOType)
               {
                   case "GPIB":

                       lock (syncRoot)
                       {
                           this.WriteString("*IDN?");
                           string content = this.ReadString();
                           this.EquipmentConnectflag = content.Contains("D86100");
                       }

                       break;
                   default:
                       Log.SaveLogToTxt("GPIB类型错误");
                       EquipmentConnectflag = false;
                       break;
               }

               return EquipmentConnectflag;
           }
           catch (Exception error)
           {
               EquipmentConnectflag = false;
               Log.SaveLogToTxt(error.ToString());
               return false;
           }

       }
       protected bool ReSet()
       {
           lock (syncRoot)
           {
               if (this.WriteString("*RST"))
               {
                   Thread.Sleep(3000);
                   return true;
               }
               else
               {
                   return false;
               }
           }
       }
       public override bool Configure(int syn = 0)
       {
           lock (syncRoot)
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
                       SetMode(0, syn);
                       SetChannel(OptChannel, true, syn);
                       SetChannel(ElecChannel, true, syn);
                       SetAttenuation(OptChannel, DcaAtt, syn);
                       SetAttenuation(ElecChannel, DcaAtt, syn);
                       SetOffset(OptChannel, Offset, syn);
                       SetOffset(ElecChannel, Offset, syn);
                       Setscale(OptChannel, Scale, DcaDataRate, syn);
                       Setscale(ElecChannel, Scale, DcaDataRate, syn);
                       SetTriggerBwlimit(syn);
                       //DCA_Calibration(D86100Struct.OPT_Channel);
                       //LoadMask(opticalMaskName);
                       //LoadMask(elecMaskName);
                       FileterSelect(OptChannel, FilterFreq, syn);
                       FileterSelect(ElecChannel, FilterFreq, syn);
                       FileterSwitch(OptChannel, true);
                       FileterSwitch(ElecChannel, true);
                       SetMaskMargin(true);
                       WavelengthSelect(OptChannel, DcaWavelength, syn);
                       WavelengthSelect(ElecChannel, DcaWavelength, syn);
                       MaskONOFF(false, syn);
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
                       SetDCAThreshold(syn);
                       SetMaskAlignMethod(0, syn);
                       OpenOpticalChannel(true, syn);
                       EquipmentConfigflag = true;
                   }
                   return true;

               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }

           }

       }
       public override bool ChangeChannel(string channel, int syn = 0)
       {
           lock (syncRoot)
           {
               string offset = ""; ;
               CurrentChannel = channel;
               if (offsetlist.ContainsKey(CurrentChannel))
                   offset = offsetlist[CurrentChannel];
               Log.SaveLogToTxt("Offset is" + offset);
               return SetAttenuation(dcacurrentchannel, offset, syn);
           }
       }
       public override bool configoffset(string channel, string offset, int syn = 0)
       {
           lock (syncRoot)
           {
               offsetlist.Add(channel, offset);
               return true;
           }
       }
       override public bool ClearDisplay()
       {
           lock (syncRoot)
           {
               try
               {
                   return this.WriteString("cdisplay"); //Reset the device
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }

       }
       override public bool SetMode(byte Mode, int syn = 0)
       {//0="EYE";1="OSC";2="TDR" 
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
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
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("DCA Mode is" + index);
                       return this.WriteString(":system:mode " + index);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":system:mode " + index);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":system:mode?");
                               readtemp = this.ReadString();
                               if (readtemp == index)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("DCA Mode is" + index);

                               flag = true;
                           }
                           else
                           {

                               Log.SaveLogToTxt("set DCA Mode wrong");
                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }

       }

       private bool SetChannel(string channel, bool onoff, int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               int j = 0;
               string readtemp = "";
               try
               {
                   if (onoff)
                   {
                       if (syn == 0)
                       {
                           flag = this.WriteString(":channel" + channel + ":display 1;" + ":measure:source " + "channel" + channel.ToString());

                       }
                       else
                       {
                           for (int i = 0; i < 3; i++)
                           {
                               flag1 = this.WriteString(":channel" + channel + ":display 1;" + ":measure:source " + "channel" + channel.ToString());
                               if (flag1 == true)
                                   break;
                           }
                           if (flag1 == true)
                           {
                               for (k = 0; k < 3; k++)
                               {

                                   this.WriteString(":channel" + channel + ":display?");
                                   readtemp = this.ReadString();
                                   if (readtemp == "1")
                                       break;
                               }
                               for (j = 0; j < 3; j++)
                               {

                                   this.WriteString(":measure:source?");
                                   readtemp = this.ReadString();
                                   if (readtemp == "channel" + channel.ToString())
                                       break;
                               }
                               if ((k <= 3) && (j < 3))
                                   flag = true;

                           }
                       }
                   }
                   else
                   {
                       if (syn == 0)
                       {
                           flag = this.WriteString(":channel" + channel + ":display 0;" + ":measure:source " + "channel" + channel.ToString());

                       }
                       else
                       {
                           for (int i = 0; i < 3; i++)
                           {
                               flag1 = this.WriteString(":channel" + channel + ":display 0;" + ":measure:source " + "channel" + channel.ToString());
                               if (flag1 == true)
                                   break;
                           }
                           if (flag1 == true)
                           {
                               for (k = 0; k < 3; k++)
                               {

                                   this.WriteString(":channel" + channel + ":display?");
                                   readtemp = this.ReadString();
                                   if (readtemp == "0")
                                       break;
                               }
                               for (j = 0; j < 3; j++)
                               {

                                   this.WriteString(":measure:source?");
                                   readtemp = this.ReadString();
                                   if (readtemp == "channel" + channel.ToString())
                                       break;
                               }
                               if ((k <= 3) && (j < 3))
                                   flag = true;

                           }

                       }
                   }

                   if (flag)
                       Log.SaveLogToTxt("channel is" + channel);
                   else
                       Log.SaveLogToTxt("set channel wrong");

                   return flag;

               }

               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private bool SetRefPos(byte pos, int syn = 0)//pos=0 left pos =1 center
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   switch (pos)
                   {
                       case 0:
                           if (syn == 0)
                           {
                               Log.SaveLogToTxt("timebase reference is left");
                               return this.WriteString(":timebase:reference left");
                           }
                           else
                           {
                               for (int i = 0; i < 3; i++)
                               {
                                   flag1 = this.WriteString(":timebase:reference left");
                                   if (flag1 == true)
                                       break;
                               }
                               if (flag1 == true)
                               {
                                   for (k = 0; k < 3; k++)
                                   {

                                       this.WriteString(":timebase:reference?");
                                       readtemp = this.ReadString();
                                       if (readtemp == "left")
                                           break;
                                   }
                                   if (k <= 3)
                                   {
                                       Log.SaveLogToTxt("timebase reference is left");
                                       flag = true;
                                   }
                                   else
                                   {

                                       Log.SaveLogToTxt("set timebase reference wrong");
                                   }

                               }

                               return flag;
                           }
                       case 1:
                           if (syn == 0)
                           {
                               Log.SaveLogToTxt("timebase reference is left");

                               return this.WriteString(":timebase:reference center");
                           }
                           else
                           {
                               for (int i = 0; i < 3; i++)
                               {
                                   flag1 = this.WriteString(":timebase:reference center");
                                   if (flag1 == true)
                                       break;
                               }
                               if (flag1 == true)
                               {
                                   for (k = 0; k < 3; k++)
                                   {

                                       this.WriteString(":timebase:reference?");
                                       readtemp = this.ReadString();
                                       if (readtemp == "center")
                                           break;
                                   }
                                   if (k <= 3)
                                   {
                                       Log.SaveLogToTxt("timebase reference is left");

                                       flag = true;
                                   }
                                   else
                                   {
                                       Log.SaveLogToTxt("set timebase reference wrong");


                                   }

                               }
                               return flag;
                           }
                       default:
                           if (syn == 0)
                           {
                               Log.SaveLogToTxt("timebase reference is left");

                               return this.WriteString(":timebase:reference left");
                           }
                           else
                           {
                               for (int i = 0; i < 3; i++)
                               {
                                   flag1 = this.WriteString(":timebase:reference left");
                                   if (flag1 == true)
                                       break;
                               }
                               if (flag1 == true)
                               {
                                   for (k = 0; k < 3; k++)
                                   {

                                       this.WriteString(":timebase:reference?");
                                       readtemp = this.ReadString();
                                       if (readtemp == "left")
                                           break;
                                   }
                                   if (k <= 3)
                                   {
                                       Log.SaveLogToTxt("timebase reference is left");
                                       flag = true;
                                   }
                                   else
                                   {

                                       Log.SaveLogToTxt("set timebase reference wrong");
                                   }

                               }
                               return flag;
                           }

                   }

               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }

           }
       }
       private bool SetPos(double pos, int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("timebase position is" + pos.ToString());
                       return this.WriteString(":timebase:position " + pos.ToString());
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":timebase:position " + pos.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":timebase:position?");
                               readtemp = this.ReadString();
                               double temp = Convert.ToDouble(readtemp);
                               if (temp == pos)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("timebase position is" + pos.ToString());
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set timebase position wrong");
                           }


                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private bool SetHistMod(bool onoff, int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
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
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("histogram mode is" + strON_OFF);
                       return this.WriteString(":histogram:mode " + strON_OFF);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":histogram:mode " + strON_OFF);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":histogram:mode?");
                               readtemp = this.ReadString();
                               if (readtemp == strON_OFF)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("histogram mode is" + strON_OFF);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set histogram mode wrong");


                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }

       }
       private bool SetHistSource(int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("window source is" + dcacurrentchannel);
                       return this.WriteString(":histogram:window:source channel" + dcacurrentchannel);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":histogram:window:source channel" + dcacurrentchannel);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":histogram:window:source?");
                               readtemp = this.ReadString();
                               if (readtemp == "channel" + dcacurrentchannel)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("window source is" + dcacurrentchannel);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set window source wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }

       private bool SetHistWindow(double x1, double x2, double y1, double y2,int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               int j = 0;
               int m = 0;
               int n = 0;
               try
               {
                   if (syn == 0)
                   {
                       this.WriteString(":histogram:window:x1position " + x1.ToString());
                       this.WriteString(":histogram:window:x2position " + x2.ToString());
                       this.WriteString(":histogram:window:y1position " + y1.ToString());
                       this.WriteString(":histogram:window:y2position " + y2.ToString());
                       return true;
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":histogram:window:x1position " + x1.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":histogram:window:x1position?");
                               readtemp = this.ReadString();
                               double temp = Convert.ToDouble(readtemp);
                               if (temp == x1)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("x1position is" + x1.ToString());

                           }

                       }
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":histogram:window:x2position " + x2.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (j = 0; j < 3; j++)
                           {

                               this.WriteString(":histogram:window:x2position?");
                               readtemp = this.ReadString();
                               double temp = Convert.ToDouble(readtemp);
                               if (temp == x2)
                                   break;
                           }
                           if (j <= 3)
                           {
                               Log.SaveLogToTxt("x2position is" + x2.ToString());

                           }

                       }
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":histogram:window:y1position " + y1.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (m = 0; m < 3; m++)
                           {

                               this.WriteString(":histogram:window:y1position?");
                               readtemp = this.ReadString();
                               double temp = Convert.ToDouble(readtemp);
                               if (temp == y1)
                                   break;
                           }
                           if (m <= 3)
                           {
                               Log.SaveLogToTxt("y1position is" + y1.ToString());

                           }

                       }
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":histogram:window:y2position " + y2.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (n = 0; n < 3; n++)
                           {

                               this.WriteString(":histogram:window:y2position?");
                               readtemp = this.ReadString();
                               double temp = Convert.ToDouble(readtemp);
                               if (temp == y2)
                                   break;
                           }
                           if (n <= 3)
                           {
                               Log.SaveLogToTxt("y2position is" + y2.ToString());

                           }

                       }
                       if ((k <= 3) && (j <= 3) && (m <= 3) && (n <= 3))
                       {
                           flag = true;
                       }
                       else
                       {
                           Log.SaveLogToTxt("SetHistWindow wrong");
                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private bool GetHistMedian(out double median)
       {
           lock (syncRoot)
           {
               if (this.WriteString(":measure:histogram:median?"))
               {
                   string strMedian;
                   strMedian = this.ReadString(24);
                   median = Convert.ToDouble(strMedian);
                   return true;
               }
               else
               {
                   median = 0;
                   return false;
               }
           }
       }
       private bool SetTimeDelay(double delayTime)
       {
           lock (syncRoot)
           {
               try
               {
                   return this.WriteString(":timebase:delay " + delayTime.ToString());

               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private bool GetTimeDelay(out double delayTime)
       {
           lock (syncRoot)
           {
               this.WriteString(":timebase:delay? ");
               string strDelayTime;
               strDelayTime = this.ReadString(32);
               delayTime = Convert.ToDouble(strDelayTime);


               return true;
           }
       }
       private bool CenterEye(int syn = 0)
       {
           lock (syncRoot)
           {
               double freq = Convert.ToDouble(DcaDataRate);
               double tempAvgWat = 0;
               double median = 0.00;
               double delayTime = 0.000E+0;
               SetRefPos(0, syn);//pos=0 left; pos =1 center
               SetPos(2.40E-8, syn);
               SetHistMod(true, syn);
               this.WriteString(":Hist:axis horizontal");
               SetHistSource(syn);

               switch (dcacurrentchannel)
               {
                   case "1":
                   case "3":
                       {
                           tempAvgWat = GetAveragePowerWatt();
                           SetHistWindow(2.40E-8, (1 / freq) + 2.40E-8, tempAvgWat - 1.00E-6, tempAvgWat + 1.00E-6, syn);
                           break;
                       }

                   case "2":
                   case "4":
                       {
                           SetHistWindow(2.40E-8, (1 / freq + 2.40E-8), -0.01, 0.01, syn);
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
               SetRefPos(1, syn);//pos=0 left; pos =1 center  
               Thread.Sleep(200);
               SetPos((1 / freq) + median, syn);
               GetTimeDelay(out delayTime);
               SetTimeDelay(delayTime + (1 / freq) / 2.00);
               SetHistMod(false, syn);
               Thread.Sleep(500);
               return true;
           }
       }
       
       override public bool AutoScale(int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("autoscale");
                       return this.WriteString(":autoscale " + DcaDataRate);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":autoscale " + DcaDataRate);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":autoscale?");
                               readtemp = this.ReadString();
                               if (Convert.ToDouble(readtemp) == Convert.ToDouble(DcaDataRate))
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("autoscale");
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set autoscale wrong ");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       public override bool DisplayThreeEyes(int syn = 0)
       { return true; }
       override public  bool RunStop(bool run)// true "RUN"  false  "Stop"
       {
           lock (syncRoot)
           {
               if (run)
               {
                   try
                   {
                       return this.WriteString(":" + "RUN");

                   }
                   catch (Exception error)
                   {
                       Log.SaveLogToTxt(error.ToString());
                       return false;
                   }

               }
               else
               {
                   try
                   {
                       return this.WriteString(":" + "STOP");

                   }
                   catch (Exception error)
                   {
                       Log.SaveLogToTxt(error.ToString());
                       return false;
                   }

               }
           }
       }
      
       private bool WavelengthSelect(string channel, string Wavelength,int syn = 0)//1 850,2 1310,3 1550 default 850
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
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
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("channel is" + channel + "Wavelength is" + Wavelength_index);
                       return this.WriteString(":Channel" + channel + ":WAVelength" + " WAV" + Wavelength);
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":Channel" + channel + ":WAVelength" + " WAV" + Wavelength);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":Channel" + channel + ":WAVelength?");
                               readtemp = this.ReadString();
                               if (readtemp == "WAV" + Wavelength)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("channel is" + channel + "Wavelength is" + Wavelength_index);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("WavelengthSelect wrong");

                           }

                       }

                       return flag;

                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }

       private bool SetruntilMod(int waveformcount, byte offwaveformsample, int syn = 0)// 0="OFF" 1="WAV" 2="SAMP"
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
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
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("waveformcount is" + waveformcount.ToString());
                       return this.WriteString("acq:runtil " + strTemp + waveformcount.ToString());
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString("acq:runtil " + strTemp + waveformcount.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString("acq:runtil?");
                               readtemp = this.ReadString();
                               if (readtemp == strTemp + waveformcount.ToString())
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("waveformcount is" + waveformcount.ToString());
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set waveformcount wrong");

                           }

                       }
                       return flag;
                   }
               }
               else
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("runtil is off");
                       return this.WriteString("acq:runtil off");
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString("acq:runtil off");
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString("acq:runtil?");
                               readtemp = this.ReadString();
                               if (readtemp.ToUpper() == "off".ToUpper())
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("runtil is off");
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("SetruntilMod wrong");

                           }

                       }
                       return flag;
                   }
               }
           }
       }

       private bool ClearMesures()
       {
           lock (syncRoot)
           {
               try
               {
                   return this.WriteString(":measure:clear");
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       public override double GetCrossing()
       {
           lock (syncRoot)
           {
               try
               {

                   if (this.WriteString(":measure:cgrade:crossing " + "Channel" + dcacurrentchannel))
                   {
                       this.WriteString("MEASure:CGRade:CROSsing?");
                       string S = this.ReadString(400);
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
       }
      
       override public double GetEratio()
       {
           lock (syncRoot)
           {
               try
               {
                   this.WriteString(":measure:cgrade:eratio dec");
                   if (this.WriteString(":MEASURE:CGRADE:ERATIO? DECibel," + "channel" + dcacurrentchannel))
                   {

                       string STR = this.ReadString(400);
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
       }
      
       override public bool DisplayER()
       {
           lock (syncRoot)
           {
               try
               {
                   return this.WriteString(":measure:cgrade:eratio dec");
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private bool DisplayEyeAmplitude()
       {
           lock (syncRoot)
           {
               try
               {
                   return this.WriteString(":measure:cgrade:amplitude");
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private double ReadEyeAmplitude()
       {
           lock (syncRoot)
           {
               this.WriteString(":measure:cgrade:amplitude?");
               string S = this.ReadString(15);
               return Convert.ToDouble(S);
           }
       }

       public override bool DisplayCrossing()
       {
           lock (syncRoot)
           {
               if (this.WriteString(":measure:cgrade:crossing channel" + dcacurrentchannel))
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
       }

       private bool WaitUntilComplete()
       {
           lock (syncRoot)
           {
               int waveForm = 0;
               string tempString = null;
               this.WriteString(":ALER?");
               tempString = this.ReadString(32);
               do
               {
                   this.WriteString(":ALER?");
                   tempString = this.ReadString(32);
                   waveForm = Convert.ToInt32(tempString);
                   Thread.Sleep(1000);
               } while ((waveForm & 1) <= 0);

               return true;
           }

       }
       //new add end
  

       private double GetJitterRMS()
       {
           lock (syncRoot)
           {
               try
               {
                   this.WriteString(":measure:CGRADE:JITTER RMS," + "Channel" + dcacurrentchannel);
                   if (this.WriteString(":measure:CGRADE:JITTER? RMS," + "Channel" + dcacurrentchannel))
                   {
                       string Str = this.ReadString();
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
       }

       private double GetFalltime()
       {
           lock (syncRoot)
           {
               try
               {//":measure:CGRADE:failtime "
                   if (this.WriteString(":measure:falltime " + "Channel" + dcacurrentchannel))
                   {
                       this.WriteString(":MEASure:FALLtime?");

                       string Str = this.ReadString(50);
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
       }


       private double GetRisetime()
       {
           lock (syncRoot)
           {
               try
               {

                   if (this.WriteString(":measure:Risetime channel" + dcacurrentchannel))
                   {
                       this.WriteString(":MEASure:RISEtime?");
                       string Str = this.ReadString(50);
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
       }
       
       override public double GetAveragePowerWatt()
       {
           lock (syncRoot)
           {
               string strAvgPower = null;
               this.WriteString(":measure:apower? WATT, channel" + dcacurrentchannel);
               strAvgPower = this.ReadString(16);
               return Convert.ToDouble(strAvgPower) * 1000000;
           }
       }

       override public double GetAveragePowerdbm()
       {
           lock (syncRoot)
           {
               try
               {

                   if (this.WriteString(":measure:apow dec," + "Channel" + dcacurrentchannel))
                   {
                       this.WriteString(":measure:apower? decibel," + "Channel" + dcacurrentchannel);
                       return Convert.ToDouble(this.ReadString(400));
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
       }
       public override bool DisplayPowerWatt()
       {
           lock (syncRoot)
           {
               try
               {
                   this.WriteString(":measure:apower WATT, channel" + dcacurrentchannel);
                   return true;
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       public override bool DisplayPowerdbm()
       {
           lock (syncRoot)
           {
               try
               {
                   this.WriteString(":measure:apow dec," + "Channel" + dcacurrentchannel);
                   return true;
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       
       private bool FileterSwitch(string channel, bool Swith)
       {
           lock (syncRoot)
           {
               string index;
               try
               {
                   if (Swith)
                       index = "ON";
                   else
                       index = "OFF";
                   return this.WriteString(":channel" + channel + ":Filter " + index);
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private bool SaveEyeDiagram(string savePath)
       {
           lock (syncRoot)
           {
               byte[] bmpData;
               string diagramName = "";
               //this.WriteString(":Disp:Data? BMP,SCREEN");

               //System.Threading.Thread.Sleep(2000);
               bmpData = GetImage();
               int len = bmpData.Length;
               //savePath = Application.StartupPath + "\\EyeDiagram\\";
               //savePath = pglobalParameters.StrPathEyeDiagram; 
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
               string Str_sencond = CurrentTime.Second.ToString();
               diagramName = pglobalParameters.CurrentSN + "-" + pglobalParameters.CurrentTemp + "-" + pglobalParameters.CurrentVcc + "-" + pglobalParameters.CurrentChannel + "-" + Str_Year + "_" + Str_Month + "_" + Str_Day + "_" + Str_Hour + "_" + Str_minute + "_" + Str_sencond + ".bmp";
               File.WriteAllBytes(@savePath + diagramName, bmpData);
               return true;
           }
       }

       private byte[] GetImage()
       {
           lock (syncRoot)
           {
               return (byte[])myIO.ReadIEEEBlock(IOPort.Type.GPIB, "GPIB0::" + Addr, ":Disp:Data? BMP,SCREEN");
           }
       }

       private bool FileterSelect(string channel, string FilterFreq, int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               int i = 0;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               int Filterindex = 0;
               bool FileterExitflag = false;
               try
               {

                   if (this.WriteString(":channel" + channel + ":FDEScription?"))
                   {
                       string Str1 = this.ReadString();
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

                       if (syn == 0)
                       {
                           Log.SaveLogToTxt("FilterFrequence is" + FilterFreq);

                           flag = this.WriteString(":channel" + channel + ":FSELect FILTer" + Filterindex.ToString());
                       }
                       else
                       {
                           for (int j = 0; j < 3; j++)
                           {
                               flag = this.WriteString(":channel" + channel + ":FSELect FILTer" + Filterindex.ToString());
                               if (flag1 == true)
                                   break;
                           }
                           if (flag1 == true)
                           {
                               for (k = 0; k < 3; k++)
                               {

                                   this.WriteString(":channel" + channel + ":FSELect?");
                                   readtemp = this.ReadString();
                                   if (readtemp == "FILTer" + Filterindex.ToString())
                                       break;
                               }
                               if (k <= 3)
                               {
                                   flag = true;
                                   Log.SaveLogToTxt("FilterFrequence is" + FilterFreq);

                               }
                               else
                               {
                                   flag = false;
                                   Log.SaveLogToTxt("FilterFrequence is wrong");

                               }

                           }

                       }

                   }
                   return flag;
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }

       }
       private bool ClearMeasurements()
       {
           lock (syncRoot)
           {
               try
               {
                   return this.WriteString(":MEASure:CLEAR");

               }
               catch
               {
                   return false;
               }
           }
       }
       private bool SelectChannel(int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("DCA channel is " + dcacurrentchannel);
                       return this.WriteString(":measure:source" + "Channel" + dcacurrentchannel);

                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":measure:source" + "Channel" + dcacurrentchannel);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":measure:source?");
                               readtemp = this.ReadString();
                               if (readtemp == "Channel" + dcacurrentchannel)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("DCA channel is " + dcacurrentchannel);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("SelectChannel wrong");

                           }

                       }
                       return flag;

                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }

       private int Gettotalmaskerrors()
       {
           lock (syncRoot)
           {
               string strTotalErrors = "";
               try
               {
                   this.WriteString(":mtest:count:fsamples?");
                   strTotalErrors = this.ReadString(16);//  read 16bytes 
                   // return Convert.ToInt32(strTotalErrors);
                   double temp = Convert.ToDouble(strTotalErrors);
                   return (int)temp;
               }
               catch
               {
                   return -1000;
               }
           }
       }
       private byte MaskTest(string maskname,string savePath,int syn=0)
       {
           lock (syncRoot)
           {
               byte margin = 0; //new
               try
               {
                   LoadMask(maskname);
                   MaskONOFF(true, syn);
                   MaskAlign();
                   RunStop(false);
                   SetMaskMargin(true, syn);
                   margin = GetTestMargin(Percentage);
                   SaveEyeDiagram(savePath);
                   MaskONOFF(false, syn);
                   return margin;
               }
               catch
               {
                   return 100;
               }
           }
       }
       private int Getregionerrors(string region)//new
       {
           lock (syncRoot)
           {
               double regionErrors = 0;
               this.WriteString(":mtest:count:failures? region" + region);
               this.ReadString(32);
               regionErrors = Convert.ToDouble(this.ReadString(32));
               return (int)regionErrors;
           }
       }
       private bool SetMaskMargin(bool Switch,int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               string stronoff = null;
               bool flag1 = false;
               int k = 0;
               int j = 0;
               string readtemp = "";
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
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("MaskMargin is" + Percentage.ToString());
                       return this.WriteString(":mtest:mmargin:state " + stronoff + ";" + ":mtest:mmargin:perc " + Percentage.ToString());
                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":mtest:mmargin:state " + stronoff + ";" + ":mtest:mmargin:perc " + Percentage.ToString());
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":mtest:mmargin:state?");
                               readtemp = this.ReadString();
                               if (readtemp == stronoff)
                                   break;
                           }
                           for (j = 0; j < 3; j++)
                           {

                               this.WriteString(":mtest:mmargin:perc?");
                               readtemp = this.ReadString();
                               if (readtemp == Percentage.ToString())
                                   break;
                           }
                           if ((k <= 3) && (j <= 3))
                           {
                               Log.SaveLogToTxt("MaskMargin is" + Percentage.ToString());
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set MaskMargin wrong");

                           }

                       }

                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }

       private bool LoadMask(string maskname)
       {
           lock (syncRoot)
           {
               bool flag = false;
               try
               {
                   flag = this.WriteString(":mtest:load " + "\"" + maskname + "\"");
                   Log.SaveLogToTxt("MaskName is" + maskname);
                   return flag;
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }

       }
       private bool MaskAlign()
       {
           lock (syncRoot)
           {
               for (byte i = 0; i < 2; i++)
               {
                   this.WriteString(":mtest:align");
               }
               return true;
           }
       }
       override public bool SetMaskAlignMethod(byte method, int syn = 0)//"0"DISPLAY,"1",EWINDOW
       {
           lock (syncRoot)
           {
               bool alignreturn = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   switch (method)
                   {
                       case 0:
                           if (syn == 0)
                           {
                               Log.SaveLogToTxt("MaskAlignMethod is DISPLAY");
                               alignreturn = this.WriteString(":mtest:yalign DISPLAY");

                           }
                           else
                           {
                               for (int i = 0; i < 3; i++)
                               {
                                   flag1 = this.WriteString(":mtest:yalign DISPLAY");
                                   if (flag1 == true)
                                       break;
                               }
                               if (flag1 == true)
                               {
                                   for (k = 0; k < 3; k++)
                                   {

                                       this.WriteString(":mtest:yalign?");
                                       readtemp = this.ReadString();
                                       if (readtemp == "DISPLAY")
                                           break;
                                   }
                                   if (k <= 3)
                                   {
                                       Log.SaveLogToTxt("MaskAlignMethod is DISPLAY");
                                       alignreturn = true;
                                   }
                                   else
                                   {
                                       Log.SaveLogToTxt("set MaskAlignMethod wrong");

                                   }

                               }
                           }
                           break;

                       case 1:
                           if (syn == 0)
                           {
                               Log.SaveLogToTxt("MaskAlignMethod is EWINDOW");
                               alignreturn = this.WriteString(":mtest:yalign EWINDOW");

                           }
                           else
                           {
                               for (int i = 0; i < 3; i++)
                               {
                                   flag1 = this.WriteString(":mtest:yalign EWINDOW");
                                   if (flag1 == true)
                                       break;
                               }
                               if (flag1 == true)
                               {
                                   for (k = 0; k < 3; k++)
                                   {

                                       this.WriteString(":mtest:yalign?");
                                       readtemp = this.ReadString();
                                       if (readtemp == "EWINDOW")
                                           break;
                                   }
                                   if (k <= 3)
                                   {
                                       Log.SaveLogToTxt("MaskAlignMethod is EWINDOW");
                                       alignreturn = true;
                                   }
                                   else
                                   {
                                       Log.SaveLogToTxt("set MaskAlignMethod wrong");

                                   }
                               }
                           }
                           break;
                       default:
                           if (syn == 0)
                           {
                               Log.SaveLogToTxt("MaskAlignMethod is EWINDOW");
                               alignreturn = this.WriteString(":mtest:yalign EWINDOW");

                           }
                           else
                           {
                               for (int i = 0; i < 3; i++)
                               {
                                   flag1 = this.WriteString(":mtest:yalign EWINDOW");
                                   if (flag1 == true)
                                       break;
                               }
                               if (flag1 == true)
                               {
                                   for (k = 0; k < 3; k++)
                                   {

                                       this.WriteString(":mtest:yalign?");
                                       readtemp = this.ReadString();
                                       if (readtemp == "EWINDOW")
                                           break;
                                   }
                                   if (k <= 3)
                                   {
                                       Log.SaveLogToTxt("MaskAlignMethod is EWINDOW");
                                       alignreturn = true;
                                   }
                                   else
                                   {
                                       Log.SaveLogToTxt("set MaskAlignMethod wrong");

                                   }
                               }
                           }
                           break;

                   }
                   return alignreturn;
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       override public bool SetRunTilOff(int syn = 0)
      {
          lock (syncRoot)
          {
              bool flag = false;
              bool flag1 = false;
              int k = 0;
              string readtemp = "";
              try
              {
                  if (syn == 0)
                  {
                      Log.SaveLogToTxt("runtil OFF ");
                      return this.WriteString(":acquire:runtil OFF");
                  }
                  else
                  {
                      for (int i = 0; i < 3; i++)
                      {
                          flag1 = this.WriteString(":acquire:runtil OFF");
                          if (flag1 == true)
                              break;
                      }
                      if (flag1 == true)
                      {
                          for (k = 0; k < 3; k++)
                          {

                              this.WriteString(":acquire:runtil?");
                              readtemp = this.ReadString();
                              if (readtemp.ToUpper() == "off".ToUpper())
                                  break;
                          }
                          if (k <= 3)
                          {
                              Log.SaveLogToTxt("runtil OFF ");
                              flag = true;
                          }
                          else
                          {
                              Log.SaveLogToTxt("SetRunTilOff wrong ");

                          }

                      }
                      return flag;
                  }
              }
              catch (Exception error)
              {
                  Log.SaveLogToTxt(error.ToString());
                  return false;
              }
          }
       }
       override public bool MaskONOFF(bool MaskON, int syn = 0) //true="ON"
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {

                   if (MaskON)//new
                   {
                       if (syn == 0)
                       {
                           Log.SaveLogToTxt("MaskON");
                           return this.WriteString(":mtest:test " + "ON");
                       }
                       else
                       {
                           for (int i = 0; i < 3; i++)
                           {
                               flag1 = this.WriteString(":mtest:test " + "ON");
                               if (flag1 == true)
                               {
                                   Thread.Sleep(100);
                                   break;
                               }
                           }
                           if (flag1 == true)
                           {
                               for (k = 0; k < 3; k++)
                               {

                                   this.WriteString(":mtest:test?");
                                   readtemp = this.ReadString();
                                   if (readtemp == "ON")
                                       break;
                               }
                               if (k <= 3)
                               {
                                   Log.SaveLogToTxt("MaskON");
                                   flag = true;
                               }
                               else
                               {
                                   Log.SaveLogToTxt("set MaskON wrong");


                               }

                           }
                           return flag;
                       }
                   }
                   else//new
                   {
                       if (syn == 0)
                       {
                           Log.SaveLogToTxt("MaskOFF");

                           return this.WriteString(":mtest:test " + "OFF");
                       }
                       else
                       {
                           for (int i = 0; i < 3; i++)
                           {
                               flag1 = this.WriteString(":mtest:test " + "OFF");
                               if (flag1 == true)
                               {
                                   Thread.Sleep(100);
                                   break;
                               }
                           }
                           if (flag1 == true)
                           {
                               for (k = 0; k < 3; k++)
                               {

                                   this.WriteString(":mtest:test?");
                                   readtemp = this.ReadString();
                                   if (readtemp == "OFF")
                                       break;
                               }
                               if (k <= 3)
                               {
                                   Log.SaveLogToTxt("MaskOFF");
                                   flag = true;
                               }
                               else
                               {
                                   Log.SaveLogToTxt("set MaskOFF wrong");


                               }
                           }
                           return flag;
                       }
                   }

               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private bool MaskTestStart()
       {
           lock (syncRoot)
           {
               try
               {

                   if (this.WriteString(":mtest:start"))
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
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       private byte GetTestMargin(byte iniMargin)
       {
           lock (syncRoot)
           {
               int totalErrors = 0;
               Percentage = iniMargin;
               do
               {
                   bool iset = SetMaskMargin(true);
                   Thread.Sleep(200);
                   totalErrors = Gettotalmaskerrors();
                   if (totalErrors == 0)
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
               Log.SaveLogToTxt("CurrentMargin is" + Percentage);
               return Percentage;
           }
       }
       private double MaskGetWaveform()
       {
           lock (syncRoot)
           {
               double Total_Wfms_NO = 0, fail_Smpls = 0;

               string S1, S2;
               try
               {
                   do
                   {
                       // Clear_Display();
                       Thread.Sleep(200);
                       this.WriteString(":mtest:count:waveforms?");

                       S1 = this.ReadString();
                       // S1 = S1.Substring(0, S1.Length - 2);
                       Total_Wfms_NO = double.Parse(S1);


                       this.WriteString(":mtest:count:fsamples?");

                       S2 = this.ReadString();
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
       }


       private bool SetAttenuation(string channel, string DCAAttenuation, int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("channel is" + channel + "DCAAttenuation is" + DCAAttenuation);
                       return this.WriteString(":channel" + channel + ":PROBe " + DCAAttenuation + ",DECibel");

                   }
                   else
                   {

                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":channel" + channel + ":PROBe " + DCAAttenuation + ",DECibel");
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":channel" + channel + ":PROBe?");
                               readtemp = this.ReadString();
                               if (readtemp == DCAAttenuation + ",DECibel")
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("channel is" + channel + "DCAAttenuation is" + DCAAttenuation);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set DCAAttenuation wrong");


                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }

       private bool SetOffset(string channel, string Offset, int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               try
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("OFFSet is" + Offset);
                       return this.WriteString(":channel" + channel + ":OFFSet" + Offset);

                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":channel" + channel + ":OFFSet" + Offset);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":channel" + channel + ":OFFSet?");
                               readtemp = this.ReadString();
                               if (Convert.ToDouble(readtemp) == Convert.ToDouble(Offset))
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("OFFSet is" + Offset);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set OFFSet wrong");

                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       public override bool SetScaleOffset(double PowerVaule,int syn = 0)
       {
           lock (syncRoot)
           {
               SetOffset(OptChannel, Offset, syn);
               Setscale(OptChannel, Scale, DcaDataRate, syn);
               Thread.Sleep(setscaledelay);
               return true;
           }
       }

       private bool Setscale(string channel, string Scale, string DataRate, int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               int m = 0;
               string readtemp = "";
               double j = (1 / Convert.ToDouble(DataRate)) * 0.22;
               try
               {
                   if (syn == 0)
                   {
                       this.WriteString(":channel" + channel + ":scale" + Scale);
                       this.WriteString(":TIMEBASE:SCALE " + j);
                       return true;
                   }
                   else
                   {

                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":channel" + channel + ":scale" + Scale);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":channel" + channel + ":scale?");
                               readtemp = this.ReadString();
                               if (Convert.ToDouble(readtemp) == Convert.ToDouble(Scale))
                                   break;
                           }

                       }
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":TIMEBASE:SCALE " + j);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (m = 0; m < 3; m++)
                           {

                               this.WriteString(":channel" + channel + ":scale?");
                               readtemp = this.ReadString();
                               if (Convert.ToDouble(readtemp) == Convert.ToDouble(Scale))
                                   break;
                           }

                       }
                       if ((k <= 3) && (m <= 3) && SetTriggerBwlimit())
                       {
                           flag = true;
                           Log.SaveLogToTxt("scale is" + Scale + "TIMEBASE SCALE IS" + j);
                       }
                       else
                       {
                           Log.SaveLogToTxt("set TIMEBASE SCALE wrong");

                           flag = false;
                       }


                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }

       private bool SetTriggerBwlimit(int syn = 0)// 0 HIGH\1 LOW\2 DIV
       {
           lock (syncRoot)
           {
               bool flag = false;
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
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
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("TriggerBwlimit is" + str_TriggerBwlimit);
                       return this.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);

                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":TRIGGER:BWLIMIT?");
                               readtemp = this.ReadString();
                               if (readtemp == str_TriggerBwlimit)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("TriggerBwlimit is" + str_TriggerBwlimit);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set TriggerBwlimit wrong");


                           }

                       }
                       return flag;
                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }

       private bool DCACalibration()
       {
           lock (syncRoot)
           {
               string Str_Mode;
               try
               {

                   this.WriteString(":Calibrate:Status?");
                   string Str1 = this.ReadString(20);
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
                       this.WriteString(":Calibrate:module:Status?  " + Str_Mode);
                       Thread.Sleep(50);
                       string status = this.ReadString(30);
                       this.WriteString(":Calibrate:module:vertical " + Str_Mode);
                       this.WriteString(":Calibrate:sdone?");
                       Thread.Sleep(1000);
                       this.WriteString(":Calibrate:continue");
                       this.WriteString(":Calibrate:sdone?");
                       int i = 0;
                       do
                       {
                           Thread.Sleep(3000);
                           Str1 = this.ReadString(1024);
                           i++;
                           if (i > 60) break;
                       }
                       while (Str1 == "");

                       this.WriteString(":Calibrate:eratio:start " + "channel" + dcacurrentchannel);

                       this.WriteString(":Calibrate:sdone?");
                       Thread.Sleep(1000);
                       this.WriteString(":Calibrate:continue");

                       this.WriteString(":Calibrate:sdone?");
                       i = 0;
                       do
                       {
                           Thread.Sleep(3000);
                           Str1 = this.ReadString(1024);
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
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }


       private bool SetDCAThreshold(int syn = 0)
       {
           lock (syncRoot)
           {
               bool flag1 = false;
               int k = 0;
               string readtemp = "";
               bool flag = false;
               try
               {
                   if (syn == 0)
                   {
                       Log.SaveLogToTxt("DCAThreshold is" + DcaThreshold);
                       return this.WriteString(":measure:define thresholds, percent, " + DcaThreshold);

                   }
                   else
                   {
                       for (int i = 0; i < 3; i++)
                       {
                           flag1 = this.WriteString(":measure:define thresholds, percent, " + DcaThreshold);
                           if (flag1 == true)
                               break;
                       }
                       if (flag1 == true)
                       {
                           for (k = 0; k < 3; k++)
                           {

                               this.WriteString(":measure:define?");
                               readtemp = this.ReadString();
                               if (readtemp == "thresholds, percent," + DcaThreshold)
                                   break;
                           }
                           if (k <= 3)
                           {
                               Log.SaveLogToTxt("DCAThreshold is" + DcaThreshold);
                               flag = true;
                           }
                           else
                           {
                               Log.SaveLogToTxt("set DCAThreshold wrong");

                           }

                       }
                       return flag;

                   }
               }
               catch (Exception error)
               {
                   Log.SaveLogToTxt(error.ToString());
                   return false;
               }
           }
       }
       /// 测试光眼图，测量结果以数组形式返回，依次为AP,ER,OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
      
       public override  bool OpticalEyeTest(out double[] testResults,int syn=0)
       {
           lock (syncRoot)
           {
               testResults = new double[9];
               int count = 0;
               bool isOk = false;
               int i;
               byte marginVaulue;
               try
               {
                   SetMaskAlignMethod(1, syn);
                   SetMode(0, syn);
                   MaskONOFF(false, syn);
                   SetRunTilOff(syn);
                   RunStop(true);
                   OpenOpticalChannel(true, syn);
                   AutoScale(syn);
                   CenterEye(syn);
                   AutoScale(syn);
                   SetruntilMod(WaveformCount, 1, syn);
                   RunStop(true);
                   ClearDisplay();
                   DisplayER();
                   DisplayEyeAmplitude();
                   DisplayCrossing();
                   Thread.Sleep(200);
                   ClearDisplay();
                   Thread.Sleep(200);
                   WaitUntilComplete();
                   // byte marginVaulue = MaskTest(opticalMaskName,pglobalParameters.StrPathOEyeDiagram, syn);
                   LoadMask(opticalMaskName);
                   MaskONOFF(true, syn);
                   MaskAlign();
                   RunStop(false);
                   SetMaskMargin(true, syn);
                   marginVaulue = GetTestMargin(Percentage);
                   // SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram);
                   Thread.Sleep(200);
                   do
                   {
                       testResults[0] = GetAveragePowerdbm();
                       testResults[1] = GetEratio();
                       testResults[2] = GetAveragePowerWatt();
                       testResults[3] = GetCrossing();
                       testResults[4] = GetJitterPP();
                       testResults[5] = GetJitterRMS();
                       testResults[6] = GetRisetime();
                       testResults[7] = GetFalltime();
                       testResults[8] = Convert.ToDouble(marginVaulue);
                       for (i = 0; i < 9; i++)
                       {
                           if (testResults[i] >= 10000000) break;
                       }
                       if (i >= 9) { isOk = true; break; }
                   } while (count++ < 3);

                   if (isOk == true)
                       SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram);
                   SetruntilMod(0, 0, syn);
                   RunStop(false);
                   ClearMesures();
                   RunStop(true);
                   SetruntilMod(0, 0, syn);
                   AutoScale(syn);

                   return isOk;
               }
               catch
               {
                   return isOk;
               }
           }
       }

       /// 测试电眼图，测量结果以数组形式返回，依次为OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
       /// </summary>
       /// <returns></returns>
       public override  bool ElecEyeTest(out double[] testResults, int syn=0)
       {
           lock (syncRoot)
           {
               testResults = new double[7];
               int count = 0;
               bool isOk = false;
               int i;
               byte marginVaulue;
               try
               {

                   SetMaskAlignMethod(1, syn);
                   SetMode(0, syn);
                   MaskONOFF(false, syn);
                   SetRunTilOff(syn);
                   RunStop(true);
                   OpenOpticalChannel(false, syn);
                   AutoScale(syn);
                   CenterEye(syn);
                   AutoScale(syn);
                   SetruntilMod(WaveformCount, 1, syn);
                   RunStop(true);
                   ClearDisplay();

                   DisplayER();
                   DisplayEyeAmplitude();
                   DisplayCrossing();
                   Thread.Sleep(200);
                   ClearDisplay();
                   Thread.Sleep(200);
                   WaitUntilComplete();
                   // byte marginVaulue = MaskTest(elecMaskName,pglobalParameters.StrPathEEyeDiagram, syn);

                   LoadMask(opticalMaskName);
                   MaskONOFF(true, syn);
                   MaskAlign();
                   RunStop(false);
                   SetMaskMargin(true, syn);
                   marginVaulue = GetTestMargin(Percentage);
                   // SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram);
                   Thread.Sleep(200);
                   do
                   {
                       testResults[0] = GetAveragePowerWatt();
                       testResults[1] = GetCrossing();
                       testResults[2] = GetJitterPP();
                       testResults[3] = GetJitterRMS();
                       testResults[4] = GetRisetime();
                       testResults[5] = GetFalltime();
                       testResults[6] = Convert.ToDouble(marginVaulue);
                       for (i = 0; i < 7; i++)
                       {
                           if (testResults[i] >= 10000000) break;
                       }
                       if (i >= 7) { isOk = true; break; }
                   } while (count++ < 3);

                   if (isOk == true)
                       SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram);

                   SetruntilMod(0, 0, syn);
                   RunStop(false);
                   ClearMesures();
                   RunStop(true);
                   SetruntilMod(0, 0, syn);
                   AutoScale(syn);

                   return isOk;
               }
               catch
               {
                   return isOk;
               }
           }
       }
    }

}