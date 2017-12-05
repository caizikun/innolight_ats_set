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
        public Algorithm algorithm = new Algorithm();
        public MP1800PPG(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] MP1800PPGStruct)
        {
            
            int i = 0;
            if (algorithm.FindFileName(MP1800PPGStruct,"ADDR",out i))
            {
                Addr = MP1800PPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"IOTYPE",out i))
            {
                IOType = MP1800PPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"NAME",out i))
            {
                Name = MP1800PPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATARATE",out i))
            {
                dataRate = MP1800PPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDAMPMAX",out i))
            {
                dataLevelGuardAmpMax = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDOFFSETMAX",out i))
            {
                dataLevelGuardOffsetMax = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDOFFSETMIN",out i))
            {
                dataLevelGuardOffsetMin = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATAAMPLITUDE",out i))
            {
                dataAmplitude = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATACROSSPOINT",out i))
            {
                dataCrossPoint = Convert.ToDouble(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"CONFIGFILEPATH",out i))
            {
                configFilePath = MP1800PPGStruct[i].DefaultValue;
            }
            else
                return false;

            if (algorithm.FindFileName(MP1800PPGStruct,"SLOT",out i))
            {
                slot = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"CLOCKSOURCE",out i))
            {
                clockSource = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"AUXOUTPUTCLKDIV",out i))
            {
                auxOutputClkDiv = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"TOTALCHANNEL",out i))
            {
                totalChannels = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"PRBSLENGTH",out i))
            {
                prbsLength = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"PATTERNTYPE",out i))
            {
                patternType = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATASWITCH",out i))
            {
                dataSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATATRACKINGSWITCH",out i))
            {
                dataTrackingSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATALEVELGUARDSWITCH",out i))
            {
                dataLevelGuardSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATAACMODESWITCH",out i))
            {
                dataAcModeSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"DATALEVELMODE",out i))
            {
                dataLevelMode = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"CLOCKSWITCH",out i))
            {
                clockSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct,"OUTPUTSWITCH",out i))
            {
                outputSwitch = Convert.ToByte(MP1800PPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800PPGStruct, "PATTERNFILE", out i))
            {
                patternfile = MP1800PPGStruct[i].DefaultValue;
            }
            else
                return false;

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
                        EquipmentConnectflag = true;
                        break;
                    default:
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
                    ConfigureSlot(slot, syn);
                    ConfigureClockSource(clockSource, syn);
                    ConfigureAuxOutputClkDiv(auxOutputClkDiv, syn);
                    ConfigureDataRate(syn);
                    for (byte i = 1; i <= totalChannels; i++)
                    {
                        ConfigureChannel(i,syn);
                        ConfigurePatternType(patternType, syn);
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

         private bool ConfigureSlot(byte slot, int syn = 0)
         {
             bool flag = false;
             bool flag1 = false;
             int k = 0;
             string readtemp = "";
             try
             {
                 if (syn == 0)
                 {
                     logger.AdapterLogString(0, "slot is" + slot);
                     return MyIO.WriteString(":MODule:ID " + slot.ToString() + "\n");
                 }
                 else
                 {
                     for (int i = 0; i < 3; i++)
                     {
                         flag1 = MyIO.WriteString(":MODule:ID " + slot.ToString() + "\n");
                         if (flag1 == true)
                             break;
                     }
                     if (flag1 == true)
                     {
                         for (k = 0; k < 3; k++)
                         {

                             MyIO.WriteString(":MODule:ID?");
                             readtemp = MyIO.ReadString();
                             if (readtemp == slot.ToString() + "\n")
                                 break;
                         }
                         if (k <= 3)
                         {
                             flag = true;
                             logger.AdapterLogString(0, "slot is" + slot);

                         }
                         else
                         {
                             logger.AdapterLogString(3, "set slot wrong");
                         }
                     }

                     return flag;
                 }
             }
             catch (Exception error)
             {
                 throw error;
             }
        }
         private bool ConfigureClockSource(byte clkSource, int syn = 0)//0=InternalClock
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
                default:
                    strClkSource = "INTernal1";
                    break;
            }
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "ClockSource is" + strClkSource);
                    return MyIO.WriteString(":SYSTem:INPut:CSELect " + strClkSource + "\n");
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SYSTem:INPut:CSELect " + strClkSource + "\n");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SYSTem:INPut:CSELect?");
                            readtemp = MyIO.ReadString();
                            // if (readtemp == strClkSource)
                            if (readtemp == "INT1,\"1:2 MU181000A\"\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "ClockSource is" + strClkSource);

                        }
                        else
                        {
                            logger.AdapterLogString(3, "set ClockSource wrong");
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
         private bool ConfigureAuxOutputClkDiv(byte clkDiv, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AuxOutputClkDiv is" + clkDiv);
                    return MyIO.WriteString(":OUTPut:SYNC:SOURce NCLock," + clkDiv.ToString() + "\n");
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":OUTPut:SYNC:SOURce NCLock," + clkDiv.ToString() + "\n");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":OUTPut:SYNC:SOURce?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == "CLOC" + clkDiv.ToString() + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "AuxOutputClkDiv is" + clkDiv);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set AuxOutputClkDiv wrong");

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
        }//PPG AuxOutput,形参为分频数
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
                    logger.AdapterLogString(0, "DATA is" + dataRate);
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
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "DATA is" + dataRate);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set dataRate wrong");

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
        private bool ConfigureChannel(byte channel,int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "channel is" + channel);
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
                            logger.AdapterLogString(0, "channel is" + channel);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set channel wrong");

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
        private bool configureusertype(string filename)//patternType=2,选择DATA,user pattern
        {
            bool flag = false;
            string AA = @filename;
            flag= MyIO.WriteString(":SYSTem:MMEMory:PATTern:RECall " +"\""+ AA +"\""+ ",BIN");
           // Thread.Sleep(1000);
            return flag;

        }
        private bool ConfigurePatternType(byte patternType, int syn = 0)//0=PRBS,1=ZSUBstitution,2=DATA,3=ALT,4=MIXData,5=MIXalt,6=SEQuence
        {//其他选项datasheet里也没有包含，实测返回值也没有，所以删掉，直接是mixd
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
                    strPatternType ="PRBS";// "MIXalt";
                    break;
                case 6:
                    strPatternType ="PRBS";// "SEQuence";
                    break;
                default:
                    strPatternType = "PRBS";
                    break;
            }
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PatternType is" + strPatternType);
                    return MyIO.WriteString(":SOURce:PATTern:TYPE " + strPatternType);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SOURce:PATTern:TYPE " + strPatternType);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SOURce:PATTern:TYPE?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == strPatternType + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "PatternType is" + strPatternType);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set PatternType wrong");

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
        private bool ConfigurePrbsLength(byte prbsLength, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PrbsLength is" + prbsLength);
                    return MyIO.WriteString(":SOURce:PATTern:PRBS:LENGth " + prbsLength.ToString());  
                }
                else
                {

                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SOURce:PATTern:PRBS:LENGth " + prbsLength.ToString());
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SOURce:PATTern:PRBS:LENGth?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == prbsLength.ToString() + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "PrbsLength is" + prbsLength);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set PrbsLength wrong");

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
        private bool ConfigureDataSwitch(byte dataSwitch, int syn = 0)
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
                    logger.AdapterLogString(0, "DataSwitch is" + strDataSwitch);
                    return MyIO.WriteString(":OUTPut:DATA:OUTPut " + strDataSwitch);     

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":OUTPut:DATA:OUTPut " + strDataSwitch);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":OUTPut:DATA:OUTPut?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == dataSwitch.ToString() + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "DataSwitch is" + strDataSwitch);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "DataSwitch wrong");

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
                            if (readtemp == strElecLevelMode+"\n")
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
    public class MP1800ED : ErrorDetector
    {
        public Algorithm algorithm = new Algorithm();
        public MP1800ED(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] MP1800EDStruct)
        {
           
            int i = 0;
            if (algorithm.FindFileName(MP1800EDStruct,"ADDR",out i))
            {
                Addr = MP1800EDStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"IOTYPE",out i))
            {
                IOType = MP1800EDStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            
            if (algorithm.FindFileName(MP1800EDStruct,"NAME",out i))
            {
                Name = MP1800EDStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"SLOT",out i))
            {
                slot = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"TOTALCHANNEL",out i))
            {
                totalChannels = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"CURRENTCHANNEL",out i))
            {
                currentChannel = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"DATAINPUTINTERFACE",out i))
            {
                dataInputInterface = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"PRBSLENGTH",out i))
            {
                prbsLength = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"ERRORRESULTZOOM",out i))
            {
                errorResultZoom = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"EDGATINGMODE",out i))
            {
                edGatingMode = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"EDGATINGUNIT",out i))
            {
                edGatingUnit = Convert.ToByte(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct,"EDGATINGTIME",out i))
            {
                edGatingTime = Convert.ToInt16(MP1800EDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(MP1800EDStruct, "PATTERNFILE", out i))
            {
                patternfile = MP1800EDStruct[i].DefaultValue;
            }
            else
                return false;
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
                        MyIO = new IOPort(IOType, "GPIB::" + Addr, logger);
                        MyIO.IOConnect();
                        EquipmentConnectflag = true;
                        break;
                    default:
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
                    }
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
        public override bool ChangeChannel(string channel, int syn = 0)
         {
             ConfigureSlot(slot, syn);
             byte channelbyte = Convert.ToByte(channel);
             logger.AdapterLogString(0, "channel is" + channel);
             currentChannel = channelbyte;
             currentChannel = channelbyte;
             return ConfigureChannel(channelbyte, syn);
         }
         public override bool configoffset(string channel, string offset, int syn = 0)
         {
             return true;
         }
         public override bool AutoAlaign(bool becenter)
        {
            bool autoAjustResult = false;
            //EdAutoSearchSetAll();
            MyIO.WriteString(":SYSTem:CFUNction ASE32");  //open autosearch interface
            MyIO.WriteString(":SENSe:MEASure:ASEarch:SLAReset"); //reset
            MyIO.WriteString(":SENSe:MEASure:ASEarch:SELSlot SLOT" + slot.ToString() + "," + currentChannel.ToString() + "," + "ON"); //auto search currentChannel
            EdAutoSearchStart();
            for (int i = 0; i < 15 && !autoAjustResult; i++)
            {
                Thread.Sleep(2000);
                autoAjustResult = (EdAutoSearchState() == "Pass");
            }
            MyIO.WriteString(":SYSTem:CFUNction OFF");  //close autosearch interface
            return autoAjustResult;
        }
        public override double GetErrorRate(int syn=0)
        {
            int readerr=0;
            ConfigureSlot(slot, syn);
            ConfigureChannel(currentChannel, syn);
            ConfigureEdGatingMode(edGatingMode, syn);
            ConfigureEdGatingUnit(edGatingUnit, syn);
            ConfigureEdGatingPeriod(edGatingTime, syn);
            EdGatingStart();
            double errcount=0;
            double errratio=0;
           
            for (int i = 0; i < 12; i++)
            {

                Thread.Sleep(Convert.ToInt32(edGatingTime * 1000));
                errcount= QureyEdErrorCount();
                errratio= QureyEdErrorRatio();
                if (errratio == 1)
                    readerr++;
                if (readerr > 3)
                    break;
                if (errcount > 3)
                    break;
            }
                return errratio;
        }
        //快速误码测试
        public override double RapidErrorRate(int syn = 0)
        {
            ConfigureSlot(slot, syn);
            ConfigureChannel(currentChannel, syn);
            ConfigureEdGatingMode(edGatingMode, syn);
            ConfigureEdGatingUnit(edGatingUnit, syn);
            ConfigureEdGatingPeriod(edGatingTime, syn);
            EdGatingStart();
            //double errcount = 0;
            double errratio = 0;
            Thread.Sleep(Convert.ToInt32(edGatingTime * 1000));
            //errcount = QureyEdErrorCount();
            errratio = QureyEdErrorRatio();
            return errratio;
        }
         /// <summary>
        /// 设置ED的通道
        /// </summary>
        /// <param name="slot"> </param>
        /// <returns> </returns>
        private bool ConfigureSlot(byte slot, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "slot is" + slot);
                    return MyIO.WriteString(":MODule:ID " + slot.ToString() + "\n");
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":MODule:ID " + slot.ToString() + "\n");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":MODule:ID?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == slot.ToString() + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "slot is" + slot);

                        }
                        else
                        {
                            logger.AdapterLogString(3, "set slot wrong");
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
        private bool ConfigureChannel(int channel, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "channel is" + channel);
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
                            logger.AdapterLogString(0, "channel is" + channel);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set channel wrong");

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataInterface"></param>
        /// <returns></returns>
        private bool ConfigureDataInputInterface(byte dataInterface, int syn = 0)//0=SingleEnd,1=Diff 50ohm,2=Diff 100ohm;
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
                    logger.AdapterLogString(0, "DataInputInterface is" + DataInterface);
                    return MyIO.WriteString(":INPut:DATA:INTerface " + DataInterface);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":INPut:DATA:INTerface " + DataInterface);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":INPut:DATA:INTerface?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == returninterface)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "DataInputInterface is" + DataInterface);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set DataInputInterface wrong");

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
       

        private bool configureusertype(string filename)//patternType=2,选择DATA,user pattern
        {
            bool flag = false;
            string AA = @filename;
            flag = MyIO.WriteString(":SYSTem:MMEMory:PATTern:RECall " + "\"" + AA + "\"" + ",BIN");
            // Thread.Sleep(1000);
            return flag;
        }
        private bool ConfigurePatternType(byte patternType, int syn = 0)//0=PRBS,1=Zero Subsitution,2=Data,3=Alternate,4=Mixed Data,5=Sequense
        {//3 5 not used
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
                    logger.AdapterLogString(0, "PatternType is" + PatternType);
                    return MyIO.WriteString(":SENSe:PATTern:TYPE " + PatternType);
                }
                else
                {

                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SENSe:PATTern:TYPE " + PatternType);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SENSe:PATTern:TYPE?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == PatternType + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "PatternType is" + PatternType);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set PatternType wrong");

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
        private bool ConfigurePrbsLength(byte prbsLength, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "prbsLength is" + prbsLength);
                    return MyIO.WriteString(":SENSe:PATTern:PRBS:LENGth " + prbsLength.ToString());

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SENSe:PATTern:PRBS:LENGth " + prbsLength.ToString());
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SENSe:PATTern:PRBS:LENGth?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == prbsLength.ToString() + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "prbsLength is" + prbsLength);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set prbsLength wrong");

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
        private bool ConfigureErrorResultZoomSwitch(byte ZoomSwitch, int syn = 0)
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
                    logger.AdapterLogString(0, "ErrorResultZoomSwitch is" + strSwitch);
                    return MyIO.WriteString(":DISPlay:RESult:ZOOM " + strSwitch);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":DISPlay:RESult:ZOOM " + strSwitch);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":DISPlay:RESult:ZOOM?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == ZoomSwitch.ToString() + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "ErrorResultZoomSwitch is" + strSwitch);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set ErrorResultZoomSwitch wrong");

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
        private bool EdAutoSearchSetAll()
        {
            try
            {

                return MyIO.WriteString(":SENSe:MEASure:ASEarch:SLASet");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool EdAutoSearchStart()
        {
            try
            {
                return MyIO.WriteString(":SENSe:MEASure:ASEarch:STARt");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private string  EdAutoSearchState()
        {            
            string searchState = "Unkown";
            try
            {
                MyIO.WriteString(":SENSe:MEASure:ASEarch:STATe?");
                Thread.Sleep(50);
                switch (MyIO.ReadString(10))
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
                logger.AdapterLogString(0, "searchState is" + searchState);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return searchState;
            }
            
            return searchState;
        }
        private bool ConfigureEdGatingMode(byte gatingMode, int syn = 0)//0=REPeat,1=UNTimed,2=SINGle
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
                    logger.AdapterLogString(0, "EdGatingMode is" + strGatingMode);
                    return MyIO.WriteString(":SENSe:MEASure:EALarm:MODE " + strGatingMode);
                }
                else
                {

                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SENSe:MEASure:EALarm:MODE " + strGatingMode);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SENSe:MEASure:EALarm:MODE?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == strGatingMode + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "EdGatingMode is" + strGatingMode);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set EdGatingMode wrong");

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
        private bool ConfigureEdGatingUnit(byte gatingUnit, int syn = 0)//0=TIME,1=BLOCk,2=CLOCk,3=ERRor
        {//无block回读值？
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
                    logger.AdapterLogString(0, "EdGatingUnit is" + strGatingUnit);
                    return MyIO.WriteString(":SENSe:MEASure:EALarm:UNIT " + strGatingUnit);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SENSe:MEASure:EALarm:UNIT " + strGatingUnit);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SENSe:MEASure:EALarm:UNIT?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == strGatingUnit + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "EdGatingUnit is" + strGatingUnit);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set EdGatingUnit wrong");

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
        private bool ConfigureEdGatingPeriod(int  gatingTime,int syn=0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            string time = "0,0,0,0";
            int   day, hour, min, sec;
            day = gatingTime / 86400;
            hour = (gatingTime - day * 86400) / 3600;
            min = (gatingTime - day * 86400 - hour * 3600) / 60;
            sec = gatingTime - day * 86400 - hour * 3600 - min * 60;
            time = day.ToString() + "," + hour.ToString() + "," + min.ToString() + "," + sec.ToString();
            try
            {

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "EdGatingPeriod is" + time);
                    return MyIO.WriteString(":SENSe:MEASure:EALarm:PERiod " + time);      
                }
                else
                {

                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":SENSe:MEASure:EALarm:PERiod " + time);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":SENSe:MEASure:EALarm:PERiod?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == time + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "EdGatingPeriod is" + time);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set EdGatingPeriod wrong");

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
        private bool EdGatingStart()
        {
            try
            {

                return MyIO.WriteString(":SENSe:MEASure:STARt");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private double QureyEdErrorRatio()
        {
            string Ber;
            double ber=0;
            try
            {
                MyIO.WriteString(":CALCulate:DATA:EALarm? \"CURRent:ER:TOTal\"");
               // Thread.Sleep(50);
                Ber =MyIO.ReadString(100).Replace("\"","").Replace("\n","");
                if (Ber.Contains("---"))
                {
                    ber = 1;
                    logger.AdapterLogString(0, "ErrorRatio is" + ber);
                }
                else
                {
                    ber = Convert.ToDouble(Ber);
                    logger.AdapterLogString(0, "ErrorRatio is" + ber);
                }
            }
            catch (Exception error)
            {
                ber = 1;
                logger.AdapterLogString(3, error.ToString());
                return ber;
            }
            return ber ;
        }
        private double QureyEdErrorCount()
        {
            string Ber;
            double ber = 0;
            try
            {
                MyIO.WriteString(":CALCulate:DATA:EALarm? \"CURRent:EC:TOTal\"");
                //Thread.Sleep(50);
                Ber = MyIO.ReadString(100).Replace("\"", "").Replace("\n", "");
                if (Ber.Contains("---"))
                {
                    ber = 9.999E+17;
                    logger.AdapterLogString(0, "ErrorCount is" + ber);
                }
                else
                {
                    ber = Convert.ToDouble(Ber);
                    logger.AdapterLogString(0, "ErrorCount is" + ber);
                }
            }
            catch (Exception error)
            {
                ber = 9.999E+17;
                logger.AdapterLogString(3, error.ToString());
                return ber;
            }
            return ber;
        }
      
}
  
}
