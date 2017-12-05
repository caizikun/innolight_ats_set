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

namespace ATS_Driver
{
    public class MP1800PPG : PPG
    {
        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] MP1800PPGStruct)
        {
            
            int i = 0;
            if (Algorithm.FindFileName(MP1800PPGStruct,"ADDR",out i))
            {
                Addr = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no ADDR");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"IOTYPE",out i))
            {
                IOType = MP1800PPGStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no IOTYPE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no RESET");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"NAME",out i))
            {
                Name = MP1800PPGStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no NAME");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATARATE",out i))
            {
                dataRate = MP1800PPGStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no DATARATE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDAMPMAX",out i))
            {
                dataLevelGuardAmpMax = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATALEVELGUARDAMPMAX");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDOFFSETMAX",out i))
            {
                dataLevelGuardOffsetMax = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATALEVELGUARDOFFSETMAX");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDOFFSETMIN",out i))
            {
                dataLevelGuardOffsetMin = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATALEVELGUARDOFFSETMIN");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATAAMPLITUDE",out i))
            {
                dataAmplitude = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATAAMPLITUDE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATACROSSPOINT",out i))
            {
                dataCrossPoint = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATACROSSPOINT");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"CONFIGFILEPATH",out i))
            {
                configFilePath = MP1800PPGStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no CONFIGFILEPATH");
                return false;
            }

            if (Algorithm.FindFileName(MP1800PPGStruct,"SLOT",out i))
            {
                slot = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no SLOT");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"CLOCKSOURCE",out i))
            {
                clockSource = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no CLOCKSOURCE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"AUXOUTPUTCLKDIV",out i))
            {
                auxOutputClkDiv = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no AUXOUTPUTCLKDIV");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"TOTALCHANNEL",out i))
            {
                totalChannels = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no TOTALCHANNEL");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"PRBSLENGTH",out i))
            {
                prbsLength = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no PRBSLENGTH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"PATTERNTYPE",out i))
            {
                patternType = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no PATTERNTYPE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATASWITCH",out i))
            {
                dataSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATASWITCH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATATRACKINGSWITCH",out i))
            {
                dataTrackingSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATATRACKINGSWITCH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDSWITCH",out i))
            {
                dataLevelGuardSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATALEVELGUARDSWITCH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATAACMODESWITCH",out i))
            {
                dataAcModeSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATAACMODESWITCH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"DATALEVELMODE",out i))
            {
                dataLevelMode = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATALEVELMODE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"CLOCKSWITCH",out i))
            {
                clockSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no CLOCKSWITCH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct,"OUTPUTSWITCH",out i))
            {
                outputSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no OUTPUTSWITCH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct, "PATTERNFILE", out i))
            {
                patternfile = MP1800PPGStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no PATTERNFILE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800PPGStruct, "OperationBitrate", out i))
            {
                OperationBitrate = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no OperationBitrate");
                return false;
            }
            if (!Connect()) return false;
            return true;
        }
        public bool ReSet()
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
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                byte channelbyte = Convert.ToByte(channel);

                return ConfigureChannel(channelbyte);
            }
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
                        lock (syncRoot)
                        {
                            this.WriteString("*IDN?");
                            string content = this.ReadString();
                            this.EquipmentConnectflag = content.Contains("1800");
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
                Log.SaveLogToTxt(error.ToString());
                return false;
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
                        //ReSet();
                        if (Reset == true)
                        {
                            ReSet();
                        }
                        ConfigureSlot(slot, syn);
                        ConfigureClockSource(clockSource, syn);
                        ConfigureAuxOutputClkDiv(auxOutputClkDiv, syn);
                        if (clockSource == 0)//内部时钟
                        {

                            ConfigureDataRate(syn);
                        }
                        else
                        {
                            ConfigureOperationBitrate(OperationBitrate);
                        }
                        for (byte i = 1; i <= totalChannels; i++)
                        {
                            ConfigureChannel(i, syn);
                            ConfigurePatternType(patternType, syn);
                            ConfigurePolarity(true);
                            if (patternType == 0)
                            {
                                ConfigurePrbsLength(prbsLength, syn);

                            }
                            if (patternType == 2)
                            {
                                configureusertype(patternfile);

                            }
                            ConfigureDataSwitch(dataSwitch, syn);
                            ConfigureDataTracking(dataTrackingSwitch, syn);
                            ConfigureDataLevelGuardAmpMax(dataLevelGuardAmpMax, syn);
                            ConfigureDataLevelGuardOffset(dataLevelGuardOffsetMax, dataLevelGuardOffsetMin, syn);
                            ConfigureDataLevelGuardSwitch(dataLevelGuardSwitch, syn);
                            ConfigureDataAcModeSwitch(dataAcModeSwitch, syn);
                            ConfigureDataLevelMode(dataLevelMode, syn);
                            ConfigureDataAmplitude(dataAmplitude, syn);
                            ConfigureDataCrossPoint(dataCrossPoint, syn);
                        }
                        ConfigureChannel(1, syn);
                        ConfigureClockSwitch(clockSwitch, syn);
                        ConfigureOutputSwitch(outputSwitch, syn);
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
        /// <summary>
        /// 按照设备属性中配置文件地址对设备进行配置
        /// </summary>
        /// <param name="SetupFile"></param>
        /// <returns></returns>
         private bool ConfigureFromFile()
        {
            lock (syncRoot)
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
                        this.WriteString(x);
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

         private bool ConfigureSlot(byte slot, int syn = 0)
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
                         Log.SaveLogToTxt("MP1800ppg slot is" + slot);
                         return this.WriteString(":MODule:ID " + slot.ToString() + "\n");
                     }
                     else
                     {
                         for (int i = 0; i < 3; i++)
                         {
                             flag1 = this.WriteString(":MODule:ID " + slot.ToString() + "\n");
                             if (flag1 == true)
                                 break;
                         }
                         if (flag1 == true)
                         {
                             for (k = 0; k < 3; k++)
                             {

                                 this.WriteString(":MODule:ID?");
                                 readtemp = this.ReadString();
                                 if (readtemp == slot.ToString() + "\n")
                                     break;
                             }
                             if (k <= 3)
                             {
                                 flag = true;
                                 Log.SaveLogToTxt("MP1800ppg slot is" + slot);

                             }
                             else
                             {
                                 Log.SaveLogToTxt("set MP1800ppg slot wrong");
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
         private bool ConfigureClockSource(byte clkSource, int syn = 0)//0=InternalClock
         {
             lock (syncRoot)
             {
                 bool flag = false;
                 bool flag1 = false;
                 int k = 0;
                 string readtemp = "";
                 string strClkSource;
                 switch (clkSource)
                 {
                     case 0:
                         strClkSource = "INTernal1";
                         break;
                     case 1:
                         strClkSource = "EXTernal";
                         break;
                     default:
                         strClkSource = "INTernal1";
                         break;
                 }
                 try
                 {
                     if (syn == 0)
                     {
                         Log.SaveLogToTxt("MP1800ppg ClockSource is" + strClkSource);
                         return this.WriteString(":SYSTem:INPut:CSELect " + strClkSource + "\n");
                     }
                     else
                     {
                         for (int i = 0; i < 3; i++)
                         {
                             flag1 = this.WriteString(":SYSTem:INPut:CSELect " + strClkSource + "\n");
                             if (flag1 == true)
                                 break;
                         }
                         if (flag1 == true)
                         {
                             this.WriteString(":SYSTem:INPut:CSELect?");
                             readtemp = this.ReadString();

                             if (clkSource == 0)
                             {


                                 for (k = 0; k < 3; k++)
                                 {

                                     this.WriteString(":SYSTem:INPut:CSELect?");
                                     readtemp = this.ReadString();
                                     // if (readtemp == strClkSource)
                                     if (readtemp.Contains("INT1"))
                                         break;
                                 }
                             }
                             if (k <= 3)
                             {
                                 flag = true;
                                 Log.SaveLogToTxt("MP1800ppg ClockSource is" + strClkSource);

                             }
                             else
                             {
                                 Log.SaveLogToTxt("set MP1800ppg ClockSource wrong");
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

         private bool ConfigureAuxOutputClkDiv(byte clkDiv, int syn = 0)
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
                        Log.SaveLogToTxt("AuxOutputClkDiv is" + clkDiv);
                        return this.WriteString(":OUTPut:SYNC:SOURce NCLock," + clkDiv.ToString() + "\n");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:SYNC:SOURce NCLock," + clkDiv.ToString() + "\n");//:OUTPut:SYNC:SOURce NCLock,4
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:SYNC:SOURce?");
                                readtemp = this.ReadString();
                                if (readtemp == "CLOC" + clkDiv.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("AuxOutputClkDiv is" + clkDiv);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set AuxOutputClkDiv wrong");

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
        }//PPG AuxOutput,形参为分频数
        private bool ConfigureDataRate(int syn = 0)//PPG比特率，单位为Gbps.
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {

                    string Str = ":OUTPut:DATA:STANdard \"Variable\"";
                    this.WriteString(Str);

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("MP1800ppg dataRate is" + dataRate);
                        return this.WriteString(":OUTPut:DATA:BITRate " + dataRate);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:BITRate " + dataRate);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:BITRate?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp).ToString("0.00") == Convert.ToDouble(dataRate).ToString("0.00"))
                                    break;
                            }
                            if (k < 3)
                            {
                                Log.SaveLogToTxt("MP1800ppg dataRate is" + dataRate);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set MP1800ppg dataRate wrong");

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

        private bool ConfigureOperationBitrate(byte OperationRate, int syn = 0)//PPG比特率，单位为Gbps.
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string StrRate;
                try
                {
                    switch (OperationRate)
                    {
                        case 0:
                            StrRate = "HALF";
                            break;
                        case 1:
                            StrRate = "HIGH1";
                            break;
                        default:
                            StrRate = "HIGH1";
                            break;
                    }
                    Log.SaveLogToTxt("MP1800ppg OperationRate is" + StrRate);

                    this.WriteString(":SYSTem:OUTPut:BITRate " + StrRate);

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("MP1800ppg OperationRate is" + StrRate);
                        // string STR = ":SYSTem:OUTPut:BITRate " + StrRate;
                        return this.WriteString(":SYSTem:OUTPut:BITRate " + StrRate);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SYSTem:OUTPut:BITRate?");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:BITRate?");
                                readtemp = this.ReadString();
                                if (readtemp == StrRate)
                                    break;
                            }
                            if (k < 3)
                            {
                                Log.SaveLogToTxt("MP1800ppg OperationRate is" + dataRate);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set MP1800ppg OperationRate wrong");

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
        private bool ConfigureChannel(byte channel,int syn = 0)
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
                        Log.SaveLogToTxt("MP1800ppg channel is" + channel);
                        return this.WriteString(":INTerface:ID " + channel.ToString());
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":INTerface:ID " + channel.ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INTerface:ID?");
                                readtemp = this.ReadString();
                                if (readtemp == channel.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("MP1800ppg channel is" + channel);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set MP1800ppg channel wrong");

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

        private bool configureusertype(string filename)//patternType=2,选择DATA,user pattern
        {
            lock (syncRoot)
            {
                bool flag = false;
                string AA = @filename;
                flag = this.WriteString(":SYSTem:MMEMory:PATTern:RECall " + "\"" + AA + "\"" + ",BIN");
                // Thread.Sleep(1000);
                return flag;
            }
        }
        
      override public   bool ConfigurePatternType(byte patternType, int syn = 0)//0=PRBS,1=ZSUBstitution,2=DATA,3=ALT,4=MIXData,5=MIXalt,6=SEQuence
        {//其他选项datasheet里也没有包含，实测返回值也没有，所以删掉，直接是mixd
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strPatternType;
                switch (patternType)
                {
                    case 0:
                        strPatternType = "PRBS";
                        break;
                    case 1:
                        strPatternType = "ZSUB";
                        break;
                    case 2:
                        strPatternType = "DATA";
                        break;
                    case 3:
                        strPatternType = "PRBS";//"ALT";
                        break;
                    case 4:
                        strPatternType = "MIXD";
                        break;
                    case 5:
                        strPatternType = "PRBS";// "MIXalt";
                        break;
                    case 6:
                        strPatternType = "PRBS";// "SEQuence";
                        break;
                    default:
                        strPatternType = "PRBS";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PatternType is" + strPatternType);
                        return this.WriteString(":SOURce:PATTern:TYPE " + strPatternType);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SOURce:PATTern:TYPE " + strPatternType);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SOURce:PATTern:TYPE?");
                                readtemp = this.ReadString();
                                if (readtemp == strPatternType + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PatternType is" + strPatternType);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set PatternType wrong");

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
       // override public 
      override public bool ConfigurePrbsLength(byte prbsLength, int syn = 0)
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
                        Log.SaveLogToTxt("PrbsLength is" + prbsLength);
                        return this.WriteString(":SOURce:PATTern:PRBS:LENGth " + prbsLength.ToString());
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SOURce:PATTern:PRBS:LENGth " + prbsLength.ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SOURce:PATTern:PRBS:LENGth?");
                                readtemp = this.ReadString();
                                if (readtemp == prbsLength.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PrbsLength is" + prbsLength);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set PrbsLength wrong");

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

      public override bool RecallPrbsLength()
      {
          lock (syncRoot)
          {
              ConfigurePatternType(3);//[20160428]nate: add
              return ConfigurePrbsLength(prbsLength);
          }
      }

        private bool ConfigureDataSwitch(byte dataSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strDataSwitch;
                switch (dataSwitch)
                {
                    case 0:
                        strDataSwitch = "OFF";
                        break;
                    case 1:
                        strDataSwitch = "ON";
                        break;
                    default:
                        strDataSwitch = "ON";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("DataSwitch is" + strDataSwitch);
                        return this.WriteString(":OUTPut:DATA:OUTPut " + strDataSwitch);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:OUTPut " + strDataSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:OUTPut?");
                                readtemp = this.ReadString();
                                if (readtemp == dataSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataSwitch is" + strDataSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("DataSwitch wrong");

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
        private bool ConfigureDataTracking(byte dataTrackingSwitch, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("DataTracking is" + strDataTrackingSwitch);
                        return this.WriteString(":OUTPut:DATA:TRACking " + strDataTrackingSwitch);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:TRACking " + strDataTrackingSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:TRACking?");
                                readtemp = this.ReadString();
                                if (readtemp == dataTrackingSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataTracking is" + strDataTrackingSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("DataTracking wrong");

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
        }//0=TrcakingOff,1=TrackingON
        private bool ConfigureDataLevelGuardAmpMax(double ampMax, int syn = 0)//ampMax单位为mV
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
                        Log.SaveLogToTxt("DataLevelGuardAmpMax is" + (ampMax / 1000).ToString());

                        return this.WriteString(":OUTPut:DATA:LIMitter:AMPLitude " + (ampMax / 1000).ToString());
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LIMitter:AMPLitude " + (ampMax / 1000).ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LIMitter:AMPLitude?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble((ampMax / 1000).ToString()))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelGuardAmpMax is" + (ampMax / 1000).ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelGuardAmpMax wrong");

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
        private bool ConfigureDataLevelGuardOffset(double offsetMax, double offsetMin, int syn = 0)
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
                        Log.SaveLogToTxt("DataLevelGuardoffsetMax is" + (offsetMax / 1000).ToString() + "DataLevelGuardoffsetMin is" + (offsetMin / 1000).ToString());
                        return this.WriteString(":OUTPut:DATA:LIMitter:OFFSet " + (offsetMax / 1000).ToString() + "," + (offsetMin / 1000).ToString());


                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LIMitter:OFFSet " + (offsetMax / 1000).ToString() + "," + (offsetMin / 1000).ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LIMitter:OFFSet?");
                                readtemp = this.ReadString();
                                double temp1 = Convert.ToDouble((offsetMax / 1000));
                                double temp2 = Convert.ToDouble((offsetMin / 1000));
                                if (readtemp == temp1.ToString("0.000") + "," + temp2.ToString("0.000") + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelGuardoffsetMax is" + (offsetMax / 1000).ToString() + "DataLevelGuardoffsetMin is" + (offsetMin / 1000).ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelGuardoffset wrong");

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
        }//形参单位全部为mV
        private bool ConfigureDataLevelGuardSwitch(byte lvGuardSwitch, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("DataLevelGuardSwitch is" + strLvGuardSwitch);
                        return this.WriteString(":OUTPut:DATA:LEVGuard " + strLvGuardSwitch);

                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LEVGuard " + strLvGuardSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LEVGuard?");
                                readtemp = this.ReadString();
                                if (readtemp == lvGuardSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelGuardSwitch is" + strLvGuardSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelGuardSwitch wrong");

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
        private bool ConfigureDataAcModeSwitch(byte acSwitch, int syn = 0)//0=DC Mode，1=AC Mode
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("DataAcModeSwitch is" + strAcSwitch);
                        return this.WriteString(":OUTPut:DATA:AOFFset " + strAcSwitch);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:AOFFset " + strAcSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:AOFFset?");
                                readtemp = this.ReadString();
                                if (readtemp == acSwitch.ToString())
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataAcModeSwitch is" + strAcSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataAcModeSwitch wrong");

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
        private bool ConfigureDataLevelMode(byte elecLevelMode, int syn = 0)//0=VARiable,1=NECL,2=PCML,3=NCML,4=SCFL,5=LVPecl,6=LVDS200,7=LVDS400
        {//无6 7选项,mode选择与levelguard范围有关
            lock (syncRoot)
            {
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
                        Log.SaveLogToTxt("DataLevelMode is" + strElecLevelMode);
                        return this.WriteString(":OUTPut:DATA:LEVel DATA," + strElecLevelMode);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LEVel DATA," + strElecLevelMode);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LEVel? DATA");
                                readtemp = this.ReadString();
                                if (readtemp == strElecLevelMode + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelMode is" + strElecLevelMode);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelMode wrong");

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
        private bool ConfigureDataAmplitude(double amplitude, int syn = 0)//单位为mV
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
                        Log.SaveLogToTxt("DataAmplitude is" + (amplitude / 1000).ToString());
                        return this.WriteString(":OUTPut:DATA:AMPLitude DATA," + (amplitude / 1000).ToString());
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:AMPLitude DATA," + (amplitude / 1000).ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:AMPLitude? DATA");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble((amplitude / 1000).ToString() + "\n"))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataAmplitude is" + (amplitude / 1000).ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataAmplitude wrong");

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
        private bool ConfigureDataCrossPoint(double crossPoint, int syn = 0)
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
                        Log.SaveLogToTxt("DataCrossPoint is" + crossPoint.ToString());
                        return this.WriteString(":OUTPut:DATA:CPOint DATA," + crossPoint.ToString());
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:CPOint DATA," + crossPoint.ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:CPOint? DATA");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(crossPoint.ToString() + "\n"))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataCrossPoint is" + crossPoint.ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataCrossPoint wrong");

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
        private bool ConfigureClockSwitch(byte clkSwitch, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("ClockSwitch is" + strClkSwitch);
                        return this.WriteString(":OUTPut:CLOCk:OUTPut " + strClkSwitch);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:CLOCk:OUTPut " + strClkSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:CLOCk:OUTPut?");
                                readtemp = this.ReadString();
                                if (readtemp == clkSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("ClockSwitch is" + strClkSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set ClockSwitch wrong");

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
        private bool ConfigureOutputSwitch(byte outSwitch, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("OutputSwitch is" + strOutSwitch);
                        return this.WriteString(":SOURce:OUTPut:ASET " + strOutSwitch);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SOURce:OUTPut:ASET " + strOutSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SOURce:OUTPut:ASET?");
                                readtemp = this.ReadString();
                                if (readtemp == outSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("OutputSwitch is" + strOutSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set OutputSwitch wrong");

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

        public override bool DataPattern_DataLength(string length)
        {
            lock (syncRoot)
            {
                return this.WriteString(":SOURce:PATTern:DATA:LENGth " + length);
            }
        }

        public override bool DataPattern_SetPatternData(string start, string end, string data)
        {
            lock (syncRoot)
            {
                return this.WriteString(":SOURce:PATTern:DATA:WHOLe " + start + "," + end + ",\"" + data + "\"");
            }
        }

        public override bool ConfigureOTxPolarity(bool polarity)
        {
            lock (syncRoot)
            {
                // :SOURce:PATTern:LOGic NEGative
                ConfigureSlot(slot, 1);
                ConfigureChannel(1, 1);// 先进入通道,后才能设置极性

                string StrPolarity;

                if (polarity)
                {
                    StrPolarity = "POS";
                }
                else
                {
                    StrPolarity = "NEG";
                }
                this.WriteString(":SOURce:PATTern:LOGic " + StrPolarity);
                Thread.Sleep(200);
                this.WriteString(":SOURce:PATTern:LOGic?");
                string Str = this.ReadString();
                if (Str.Contains(StrPolarity))
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
        private bool ConfigurePolarity(bool polarity)// 它没有通道好的选择,必须依赖别人修改通道号
        {
            lock (syncRoot)
            {
                // :SOURce:PATTern:LOGic NEGative



                string StrPolarity;

                if (polarity)
                {
                    StrPolarity = "POS";
                }
                else
                {
                    StrPolarity = "NEG";
                }
                this.WriteString(":SOURce:PATTern:LOGic " + StrPolarity);
                Thread.Sleep(200);
                this.WriteString(":SOURce:PATTern:LOGic?");
                string Str = this.ReadString();
                if (Str.Contains(StrPolarity))
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
    }
    public class MP1800ED : ErrorDetector
    {
        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] MP1800EDStruct)
        {
           
            int i = 0;
            if (Algorithm.FindFileName(MP1800EDStruct,"ADDR",out i))
            {
                Addr = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no ADDR");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"IOTYPE",out i))
            {
                IOType = MP1800EDStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no IOTYPE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no RESET");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"NAME",out i))
            {
                Name = MP1800EDStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no NAME");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"SLOT",out i))
            {
                slot = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no SLOT");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"TOTALCHANNEL",out i))
            {
                totalChannels = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no TOTALCHANNEL");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"CURRENTCHANNEL",out i))
            {
                currentChannel = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no CURRENTCHANNEL");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"DATAINPUTINTERFACE",out i))
            {
                dataInputInterface = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATAINPUTINTERFACE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"PRBSLENGTH",out i))
            {
                prbsLength = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no PRBSLENGTH");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"ERRORRESULTZOOM",out i))
            {
                errorResultZoom = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no ERRORRESULTZOOM");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"EDGATINGMODE",out i))
            {
                edGatingMode = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no EDGATINGMODE");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"EDGATINGUNIT",out i))
            {
                edGatingUnit = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no EDGATINGUNIT");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct,"EDGATINGTIME",out i))
            {
                edGatingTime = Convert.ToInt16(MP1800EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no EDGATINGTIME");
                return false;
            }
            if (Algorithm.FindFileName(MP1800EDStruct, "PATTERNFILE", out i))
            {
                patternfile = MP1800EDStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no PATTERNFILE");
                return false;
            }
            if (!Connect()) return false;
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
                        lock (syncRoot)
                        {
                            this.WriteString("*IDN?");
                            string content = this.ReadString();
                            this.EquipmentConnectflag = content.Contains("1800");
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
                Log.SaveLogToTxt(error.ToString());
                return false;
            }
        }
        public bool ReSet()
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
                        ConfigureSlot(slot, syn);
                        for (byte i = 1; i <= totalChannels; i++)
                        {
                            ConfigureChannel(i, syn);
                            ConfigureDataInputInterface(dataInputInterface, syn);
                            ConfigurePatternType(patternType, syn);
                            if (patternType == 0)
                            {
                                ConfigurePrbsLength(prbsLength, syn);

                            }
                            if (patternType == 2)
                            {
                                configureusertype(patternfile);

                            }
                            ConfigureErrorResultZoomSwitch(errorResultZoom, syn);
                            //配置极性的指令有问题，所以注释掉
                            //ConfigurePolarity(true);
                        }
                        AutoAdjust();
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
                 ConfigureSlot(slot, syn);
                 byte channelbyte = Convert.ToByte(channel);
                 Log.SaveLogToTxt("channel is" + channel);
                 currentChannel = channelbyte;
                 currentChannel = channelbyte;

                 ConfigureChannel(channelbyte, syn);

                 ConfigureEdGatingMode(edGatingMode, syn);
                 ConfigureEdGatingUnit(edGatingUnit, syn);

                 return true;
             }
         }
         public override bool configoffset(string channel, string offset, int syn = 0)
         {
             return true;
         }
         public override bool AutoAlaign(bool becenter)
         {
             //bool autoAjustResult = false;

             //this.WriteString(":SYSTem:CFUNction ASE32");  //open autosearch interface

             //this.WriteString(":SENSe:MEASure:ASEarch:SLAReset"); //reset
             //this.WriteString(":SENSe:MEASure:ASEarch:SELSlot SLOT" + slot.ToString() + "," + currentChannel.ToString() + "," + "ON"); //auto search currentChannel

             //EdAutoSearchStart();
             //for (int i = 0; i < 15 && !autoAjustResult; i++)
             //{
             //    Thread.Sleep(2000);
             //    autoAjustResult = (EdAutoSearchState() == "Pass");
             //}
             //this.WriteString(":SYSTem:CFUNction OFF");  //close autosearch interface
             //return autoAjustResult;

             lock (syncRoot)
             {
                 this.WriteString(":SENSe:MEASure:AADJust32:STATe?");
                 string Str = this.ReadString();
                 if (Str != "1\n")
                 {
                     AutoAdjust();
                     Thread.Sleep(200);
                     this.WriteString(":SENSe:MEASure:AADJust32:STATe?");
                     Str = this.ReadString();
                     if (Str != "1\n") return false;

                 }

                 return true;
             }
         }
         private  bool AutoAdjust()
         {
             //bool autoAjustResult = false;
             //this.WriteString(":SYSTem:CFUNction AADJ32");  //open autosearch interface
             //this.WriteString(":SENSe:MEASure:AADJust32:SLASet");//set all
             //Thread.Sleep(200);
             //this.WriteString(":SENSe:MEASure:AADJust32:STARt");//start
             //this.WriteString(":SYSTem:CFUNction OFF");  //close autosearch interface
             //return autoAjustResult;
             lock (syncRoot)
             {
                 this.WriteString(":SENSe:MEASure:AADJust32:STATe?");
                 string Str = this.ReadString();
                 if (Str != "1\n")
                 {
                     for (int i = 0; i < 3; i++)
                     {
                         this.WriteString(":SYSTem:CFUNction AADJ32");  //open autosearch interface
                         this.WriteString(":SENSe:MEASure:AADJust32:SLASet");//set all
                         Thread.Sleep(200);
                         this.WriteString(":SENSe:MEASure:AADJust32:STARt");//start
                         this.WriteString(":SYSTem:CFUNction OFF");  //close autosearch interface
                         Thread.Sleep(200);
                         this.WriteString(":SENSe:MEASure:AADJust32:STATe?");
                         Str = this.ReadString();
                         if (Str.Contains("1")) return true;
                     }
                     return false;
                 }
                 else
                 {
                     return true;
                 }

             }


         }
        public override double GetErrorRate(int syn=0)
        {
            lock (syncRoot)
            {
                int readerr = 0;
                ConfigureSlot(slot, syn);
                ConfigureChannel(currentChannel, syn);
                ConfigureEdGatingMode(edGatingMode, syn);
                ConfigureEdGatingUnit(edGatingUnit, syn);
                ConfigureEdGatingPeriod(edGatingTime, syn);
                EdGatingStart();
                double errcount = 0;
                double errratio = 0;

                for (int i = 0; i < 12; i++)
                {

                    Thread.Sleep(Convert.ToInt32(edGatingTime * 1000));
                    errcount = QureyEdErrorCount();
                    errratio = QureyEdErrorRatio();
                    if (errratio == 1)
                        readerr++;
                    if (readerr > 3)
                        break;
                    if (errcount > 3)
                        break;
                }
                return errratio;
            }
        }
        //快速误码测试
        public override double RapidErrorRate(int syn = 0)
        {
            //ConfigureSlot(slot, syn);
            //ConfigureChannel(currentChannel, syn);
            //ConfigureEdGatingMode(edGatingMode, syn);
            //ConfigureEdGatingUnit(edGatingUnit, syn);
           // ConfigureEdGatingPeriod(edGatingTime, syn);
            lock (syncRoot)
            {
                EdGatingStart();
                //double errcount = 0;
                double errratio = 0;
                Thread.Sleep(Convert.ToInt32(edGatingTime * 1000));
                //errcount = QureyEdErrorCount();
                errratio = QureyEdErrorRatio();
                return errratio;
            }
        }

        public override double[] RapidErrorRate_AllCH(int syn = 0)
        {
            lock (syncRoot)
            {
                double[] ErrRata = new double[4];
                for (int i = 0; i < 4; i++)
                {
                    ConfigureChannel(currentChannel, syn);
                    ConfigureEdGatingMode(edGatingMode, syn);
                    ConfigureEdGatingUnit(edGatingUnit, syn);
                    ConfigureEdGatingPeriod(edGatingTime, syn);
                    EdGatingStart();
                }

                Thread.Sleep(Convert.ToInt32(edGatingTime * 1000));

                for (int i = 0; i < 4; i++)
                {
                    ConfigureChannel(i + 1, syn);
                    ErrRata[i] = QureyEdErrorRatio();
                }
                return ErrRata;
            }
        }
        public override double[] RapidErrorCount_AllCH(int syn = 0, bool IsClear = false)
        {
            lock (syncRoot)
            {
                double[] ErrCount = new double[4];
                ConfigureSlot(slot, syn);
                if (IsClear)
                {
                    EdGatingStart();
                }

                for (int i = 0; i < 4; i++)
                {
                    // ConfigureChannel(channelbyte, syn);
                    ConfigureChannel(i + 1, syn);
                    ErrCount[i] = QureyEdErrorCount();

                }

                return ErrCount;
            }
        }
         /// <summary>
        /// 设置ED的通道
        /// </summary>
        /// <param name="slot"> </param>
        /// <returns> </returns>
        private bool ConfigureSlot(byte slot, int syn = 0)
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
                        Log.SaveLogToTxt("MP1800 ED slot is" + slot);
                        return this.WriteString(":MODule:ID " + slot.ToString() + "\n");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":MODule:ID " + slot.ToString() + "\n");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":MODule:ID?");
                                readtemp = this.ReadString();
                                if (readtemp == slot.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                flag = true;
                                Log.SaveLogToTxt("MP1800 ED slot is" + slot);

                            }
                            else
                            {
                                Log.SaveLogToTxt("MP1800 ED set slot wrong");
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
        private bool ConfigureChannel(int channel, int syn = 0)
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
                        Log.SaveLogToTxt("MP1800 ED channel is" + channel);
                        return this.WriteString(":INTerface:ID " + channel.ToString());

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":INTerface:ID " + channel.ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INTerface:ID?");
                                readtemp = this.ReadString();
                                if (readtemp == channel.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("MP1800 ED channel is" + channel);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set MP1800 ED channel wrong");

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataInterface"></param>
        /// <returns></returns>
        private bool ConfigureDataInputInterface(byte dataInterface, int syn = 0)//0=SingleEnd,1=Diff 50ohm,2=Diff 100ohm;
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string DataInterface;
                string returninterface = "";
                switch (dataInterface)
                {
                    case 0:
                        DataInterface = "SINGle";
                        returninterface = "SING\n";
                        break;
                    case 1:
                        DataInterface = "DIF50ohm";
                        returninterface = "DIF50\n";
                        break;
                    case 2:
                        DataInterface = "DIF100ohm";
                        returninterface = "DIF100\n";
                        break;
                    default:
                        DataInterface = "SINGle";
                        returninterface = "SING\n";
                        break;
                }

                try
                {

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("DataInputInterface is" + DataInterface);
                        return this.WriteString(":INPut:DATA:INTerface " + DataInterface);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":INPut:DATA:INTerface " + DataInterface);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INPut:DATA:INTerface?");
                                readtemp = this.ReadString();
                                if (readtemp == returninterface)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataInputInterface is" + DataInterface);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataInputInterface wrong");

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
       

        private bool configureusertype(string filename)//patternType=2,选择DATA,user pattern
        {
            lock (syncRoot)
            {
                bool flag = false;
                string AA = @filename;
                flag = this.WriteString(":SYSTem:MMEMory:PATTern:RECall " + "\"" + AA + "\"" + ",BIN");
                // Thread.Sleep(1000);
                return flag;
            }
        }
        private bool ConfigurePatternType(byte patternType, int syn = 0)//0=PRBS,1=Zero Subsitution,2=Data,3=Alternate,4=Mixed Data,5=Sequense
        {//3 5 not used
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string PatternType;
                switch (patternType)
                {
                    case 0:
                        PatternType = "PRBS";
                        break;
                    case 1:
                        PatternType = "ZSUB";
                        break;
                    case 2:
                        PatternType = "DATA";
                        break;
                    case 3:
                        PatternType = "PRBS";//"ALT";
                        break;
                    case 4:
                        PatternType = "MIXD";
                        break;
                    case 5:
                        PatternType = "PRBS";//"SEQuence";
                        break;
                    default:
                        PatternType = "PRBS";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PatternType is" + PatternType);
                        return this.WriteString(":SENSe:PATTern:TYPE " + PatternType);
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SENSe:PATTern:TYPE " + PatternType);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SENSe:PATTern:TYPE?");
                                readtemp = this.ReadString();
                                if (readtemp == PatternType + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PatternType is" + PatternType);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set PatternType wrong");

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
        private bool ConfigurePrbsLength(byte prbsLength, int syn = 0)
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
                        Log.SaveLogToTxt("prbsLength is" + prbsLength);
                        return this.WriteString(":SENSe:PATTern:PRBS:LENGth " + prbsLength.ToString());

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SENSe:PATTern:PRBS:LENGth " + prbsLength.ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SENSe:PATTern:PRBS:LENGth?");
                                readtemp = this.ReadString();
                                if (readtemp == prbsLength.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("prbsLength is" + prbsLength);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set prbsLength wrong");

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
        private bool ConfigureErrorResultZoomSwitch(byte ZoomSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strSwitch;
                switch (ZoomSwitch)
                {
                    case 0:
                        strSwitch = "OFF";
                        break;
                    case 1:
                        strSwitch = "ON";
                        break;
                    default:
                        strSwitch = "OFF";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("ErrorResultZoomSwitch is" + strSwitch);
                        return this.WriteString(":DISPlay:RESult:ZOOM " + strSwitch);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":DISPlay:RESult:ZOOM " + strSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":DISPlay:RESult:ZOOM?");
                                readtemp = this.ReadString();
                                if (readtemp == ZoomSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("ErrorResultZoomSwitch is" + strSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set ErrorResultZoomSwitch wrong");

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
        private bool EdAutoSearchSetAll()
        {
            lock (syncRoot)
            {
                try
                {

                    return this.WriteString(":SENSe:MEASure:ASEarch:SLASet");
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        private bool EdAutoSearchStart()
        {
            lock (syncRoot)
            {
                try
                {
                    return this.WriteString(":SENSe:MEASure:ASEarch:STARt");
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        private string  EdAutoSearchState()
        {
            lock (syncRoot)
            {
                string searchState = "Unkown";
                try
                {
                    this.WriteString(":SENSe:MEASure:ASEarch:STATe?");
                    Thread.Sleep(50);
                    switch (this.ReadString(10))
                    {
                        case "0\n":
                            searchState = "Pass";
                            break;
                        case "1\n":
                            searchState = "Searching";
                            break;
                        case "-1\n":
                            searchState = "Fail";
                            break;
                        default:
                            searchState = "Unkown";
                            break;
                    }
                    Log.SaveLogToTxt("searchState is" + searchState);
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return searchState;
                }

                return searchState;
            }
        }
        private bool ConfigureEdGatingMode(byte gatingMode, int syn = 0)//0=REPeat,1=UNTimed,2=SINGle
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strGatingMode;
                switch (gatingMode)
                {
                    case 0:
                        strGatingMode = "REP";
                        break;
                    case 1:
                        strGatingMode = "UNT";
                        break;
                    case 2:
                        strGatingMode = "SING";
                        break;
                    default:
                        strGatingMode = "REP";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("EdGatingMode is" + strGatingMode);
                        return this.WriteString(":SENSe:MEASure:EALarm:MODE " + strGatingMode);
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SENSe:MEASure:EALarm:MODE " + strGatingMode);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SENSe:MEASure:EALarm:MODE?");
                                readtemp = this.ReadString();
                                if (readtemp == strGatingMode + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("EdGatingMode is" + strGatingMode);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set EdGatingMode wrong");

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
        private bool ConfigureEdGatingUnit(byte gatingUnit, int syn = 0)//0=TIME,1=BLOCk,2=CLOCk,3=ERRor
        {//无block回读值？
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strGatingUnit;
                switch (gatingUnit)
                {
                    case 0:
                        strGatingUnit = "TIME";
                        break;
                    case 1:
                        strGatingUnit = "BLOCk";
                        break;
                    case 2:
                        strGatingUnit = "CLOC";
                        break;
                    case 3:
                        strGatingUnit = "ERR";
                        break;
                    default:
                        strGatingUnit = "TIME";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("EdGatingUnit is" + strGatingUnit);
                        return this.WriteString(":SENSe:MEASure:EALarm:UNIT " + strGatingUnit);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SENSe:MEASure:EALarm:UNIT " + strGatingUnit);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SENSe:MEASure:EALarm:UNIT?");
                                readtemp = this.ReadString();
                                if (readtemp == strGatingUnit + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("EdGatingUnit is" + strGatingUnit);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set EdGatingUnit wrong");

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
        private bool ConfigureEdGatingPeriod(int  gatingTime,int syn=0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string time = "0,0,0,0";
                int day, hour, min, sec;
                day = gatingTime / 86400;
                hour = (gatingTime - day * 86400) / 3600;
                min = (gatingTime - day * 86400 - hour * 3600) / 60;
                sec = gatingTime - day * 86400 - hour * 3600 - min * 60;
                time = day.ToString() + "," + hour.ToString() + "," + min.ToString() + "," + sec.ToString();
                try
                {

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("EdGatingPeriod is" + time);
                        return this.WriteString(":SENSe:MEASure:EALarm:PERiod " + time);
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":SENSe:MEASure:EALarm:PERiod " + time);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":SENSe:MEASure:EALarm:PERiod?");
                                readtemp = this.ReadString();
                                if (readtemp == time + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("EdGatingPeriod is" + time);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set EdGatingPeriod wrong");

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
        public override bool EdGatingStart()
        {
            lock (syncRoot)
            {
                try
                {

                    return this.WriteString(":SENSe:MEASure:STARt");
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override double QureyEdErrorRatio()
        {
            lock (syncRoot)
            {
                string StrBer = "1";
                double ber = 0;
                try
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            this.WriteString(":CALCulate:DATA:EALarm? \"CURRent:ER:TOTal\"");
                            Thread.Sleep(100);
                            StrBer = this.ReadString(100).Replace("\"", "").Replace("\n", "");
                            Thread.Sleep(100);

                            if (StrBer.Contains("---") || StrBer == "" || StrBer == null)
                            {
                                ber = 9.999E+17;

                            }
                            else
                            {
                                break;
                            }


                        }
                        catch (Exception ex)
                        {

                            Log.SaveLogToTxt(ex.Message);
                        }

                    }
                    if (StrBer.Contains("---") || StrBer == "" || StrBer == null)
                    {
                        ber = 9.999E+17;
                        Log.SaveLogToTxt("ErrorCount is" + ber);
                    }
                    else
                    {
                        ber = Convert.ToDouble(StrBer);
                        // Log.SaveLogToTxt("ErrorCount is" + ber);
                    }
                }
                catch (Exception error)
                {
                    ber = 9.999E+17;
                    Log.SaveLogToTxt(error.ToString());
                    return ber;
                }
                return ber;

            }
        }
        private double QureyEdErrorCount()
        {
            lock (syncRoot)
            {
                string Ber = "-1";
                double ber = 0;
                try
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {

                            this.WriteString(":CALCulate:DATA:EALarm? \"CURRent:EC:TOTal\"");
                            Thread.Sleep(500);
                            Ber = this.ReadString(100).Replace("\"", "").Replace("\n", "");
                        }
                        catch (Exception ex)
                        {

                            Log.SaveLogToTxt(ex.Message);
                            this.WriteString("*CLS");
                            Thread.Sleep(2000);

                        }
                        if (Ber.Contains("---") || Ber == "" || Ber == null)
                        {
                            ber = 9.999E+17;

                        }
                        else
                        {
                            if (Ber == "-1")
                            {
                                this.WriteString("*CLS");
                                Thread.Sleep(2000);

                            }
                            else
                            {


                                break;
                            }
                        }
                        Thread.Sleep(500);
                    }
                    ////this.WriteString(":CALCulate:DATA:EALarm? \"CURRent:EC:TOTal\"");
                    //////Thread.Sleep(50);
                    ////Ber = this.ReadString(100).Replace("\"", "").Replace("\n", "");



                    if (Ber.Contains("---") || Ber == "" || Ber == null)
                    {
                        ber = 9.999E+17;
                        Log.SaveLogToTxt("ErrorCount is" + ber);
                    }
                    else
                    {
                        ber = Convert.ToDouble(Ber);
                        Log.SaveLogToTxt("ErrorCount is" + ber);
                    }
                }
                catch (Exception error)
                {
                    ber = 9.999E+17;
                    Log.SaveLogToTxt(error.ToString());
                    // return ber;
                }
                finally
                {


                }
                return ber;
            }
        }
        public override bool ConfigureERxPolarity(bool polarity)
        {
            lock (syncRoot)
            {
                // :SOURce:PATTern:LOGic NEGative
                ConfigureSlot(slot, 0);
                ConfigureChannel(1, 1);// 先进入通道,后才能设置极性

                string StrPolarity;

                if (polarity)
                {
                    StrPolarity = "POS";
                }
                else
                {
                    StrPolarity = "NEG";
                }
                this.WriteString(":SENSe:PATTern:LOGic " + StrPolarity);
                Thread.Sleep(200);
                this.WriteString(":SENSe:PATTern:LOGic?");
                string Str = this.ReadString();
                if (Str.Contains(StrPolarity))
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
        private  bool ConfigurePolarity(bool polarity)// 它没有通道好的选择,必须依赖别人修改通道号
        {
            lock (syncRoot)
            {
                // :SOURce:PATTern:LOGic NEGative



                string StrPolarity;

                if (polarity)
                {
                    StrPolarity = "POS";
                }
                else
                {
                    StrPolarity = "NEG";
                }
                this.WriteString(":SOURce:PATTern:LOGic " + StrPolarity);
                Thread.Sleep(200);
                this.WriteString(":SOURce:PATTern:LOGic?");
                string Str = this.ReadString();
                if (Str.Contains(StrPolarity))
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
      
}
  
}
