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
        public override bool ChangeChannel(string channel)
        {
            byte channelbyte = Convert.ToByte(channel);

            return ConfigureChannel(channelbyte);
        }
        public override bool configoffset(string channel, string offset)
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
                        MyIO = new IOPort(IOType, "GPIB::" + Addr.ToString());
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
                    //ReSet();
                    if (Reset == true)
                    {
                        ReSet();
                    }
                    ConfigureSlot(slot);
                    ConfigureClockSource(clockSource);
                    ConfigureAuxOutputClkDiv(auxOutputClkDiv);
                    ConfigureDataRate();
                    for (byte i = 1; i <= totalChannels; i++)
                    {
                        ConfigureChannel(i);
                        ConfigurePatternType(patternType);
                        ConfigurePrbsLength(prbsLength);
                        ConfigureDataSwitch(dataSwitch);
                        ConfigureDataTracking(dataTrackingSwitch);
                        ConfigureDataLevelGuardAmpMax(dataLevelGuardAmpMax);
                        ConfigureDataLevelGuardOffset(dataLevelGuardOffsetMax, dataLevelGuardOffsetMin);
                        ConfigureDataLevelGuardSwitch(dataLevelGuardSwitch);
                        ConfigureDataAcModeSwitch(dataAcModeSwitch);
                        ConfigureDataLevelMode(dataLevelMode);
                        ConfigureDataAmplitude(dataAmplitude);
                        ConfigureDataCrossPoint(dataCrossPoint);
                    }
                    ConfigureChannel(1);
                    ConfigureClockSwitch(clockSwitch);
                    ConfigureOutputSwitch(outputSwitch);
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

         private bool ConfigureSlot(byte slot)
        {
            try
            {
                logger.AdapterLogString(0, "slot is" + slot);
                return MyIO.WriteString(":MODule:ID " + slot.ToString()+"\n");

            }
            catch (Exception error)
            {
                throw error;
            }
        }
        private bool ConfigureClockSource(byte clkSource)//0=InternalClock
        {
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
                logger.AdapterLogString(0, "ClockSource is" + strClkSource);
                return MyIO.WriteString(":SYSTem:INPut:CSELect " + strClkSource + "\n");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureAuxOutputClkDiv(byte clkDiv)
        {
            try
            {
                logger.AdapterLogString(0, "AuxOutputClkDiv is" + clkDiv);
                return MyIO.WriteString(":OUTPut:SYNC:SOURce NCLock," + clkDiv.ToString()+"\n");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }//PPG AuxOutput,形参为分频数
        private bool ConfigureDataRate()//PPG比特率，单位为Gbps.
        {
            try
            {
                logger.AdapterLogString(0, "DATA is" + dataRate);
                return MyIO.WriteString(":OUTPut:DATA:BITRate " + dataRate);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureChannel(byte channel)
        {
            try
            {
                logger.AdapterLogString(0, "channel is" + channel);
                return MyIO.WriteString(":INTerface:ID " + channel.ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigurePatternType(byte patternType)//0=PRBS,1=ZSUBstitution,2=DATA,3=ALT,4=MIXData,5=MIXalt,6=SEQuence
        {
            string strPatternType;
            switch (patternType)
            {
                case 0:
                    strPatternType = "PRBS";
                    break;
                case 1:
                    strPatternType = "ZSUBstitution";
                    break;
                case 2:
                    strPatternType = "DATA";
                    break;
                case 3:
                    strPatternType = "ALT";
                    break;
                case 4:
                    strPatternType = "MIXData";
                    break;
                case 5:
                    strPatternType = "MIXalt";
                    break;
                case 6:
                    strPatternType = "SEQuence";
                    break;
                default:
                    strPatternType = "PRBS";
                    break;
            }
            try
            {
                logger.AdapterLogString(0, "PatternType is" + strPatternType);
                return MyIO.WriteString(":SOURce:PATTern:TYPE " + strPatternType);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigurePrbsLength(byte prbsLength)
        {
            try
            {
                logger.AdapterLogString(0, "PrbsLength is" + prbsLength);
                return MyIO.WriteString(":SOURce:PATTern:PRBS:LENGth " + prbsLength.ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureDataSwitch(byte dataSwitch)
        {
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
                logger.AdapterLogString(0, "DataSwitch is" + strDataSwitch);
                return MyIO.WriteString(":OUTPut:DATA:OUTPut " + strDataSwitch);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureDataTracking(byte dataTrackingSwitch)
        {
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
                logger.AdapterLogString(0, "DataTracking is" + strDataTrackingSwitch);
                return MyIO.WriteString(":OUTPut:DATA:TRACking " + strDataTrackingSwitch);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }//0=TrcakingOff,1=TrackingON
        private bool ConfigureDataLevelGuardAmpMax(double ampMax)//ampMax单位为mV
        {
            try
            {
                logger.AdapterLogString(0, "DataLevelGuardAmpMax is" + (ampMax / 1000).ToString());
                return MyIO.WriteString(":OUTPut:DATA:LIMitter:AMPLitude " + (ampMax / 1000).ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureDataLevelGuardOffset(double offsetMax, double offsetMin)
        {
            try
            {
                logger.AdapterLogString(0, "DataLevelGuardoffsetMax is" + (offsetMax / 1000).ToString() + "DataLevelGuardoffsetMin is" + (offsetMin / 1000).ToString());
                return MyIO.WriteString(":OUTPut:DATA:LIMitter:OFFSet " + (offsetMax / 1000).ToString() + "," + (offsetMin / 1000).ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }//形参单位全部为mV
        private bool ConfigureDataLevelGuardSwitch(byte lvGuardSwitch)
        {
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
                logger.AdapterLogString(0, "DataLevelGuardSwitch is" + strLvGuardSwitch);
                return MyIO.WriteString(":OUTPut:DATA:LEVGuard " + strLvGuardSwitch);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureDataAcModeSwitch(byte acSwitch)//0=DC Mode，1=AC Mode
        {
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
                logger.AdapterLogString(0, "DataAcModeSwitch is" + strAcSwitch);
                return MyIO.WriteString(":OUTPut:DATA:AOFFset " + strAcSwitch);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureDataLevelMode(byte elecLevelMode)//0=VARiable,1=NECL,2=PCML,3=NCML,4=SCFL,5=LVPecl,6=LVDS200,7=LVDS400
        {
            string strElecLevelMode;
            switch (elecLevelMode)
            {
                case 0:
                    strElecLevelMode = "VARiable";
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
                    strElecLevelMode = "LVPecl";
                    break;
                case 6:
                    strElecLevelMode = "LVDS200";
                    break;
                case 7:
                    strElecLevelMode = "LVDS400";
                    break;
                default:
                    strElecLevelMode = "VARiable";
                    break;
            }
            try
            {
                logger.AdapterLogString(0, "DataLevelMode is" + strElecLevelMode);
                return MyIO.WriteString(":OUTPut:DATA:LEVel DATA," + strElecLevelMode);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureDataAmplitude(double amplitude)//单位为mV
        {
            try
            {
                logger.AdapterLogString(0, "DataAmplitude is" + (amplitude / 1000).ToString());
                return MyIO.WriteString(":OUTPut:DATA:AMPLitude DATA," + (amplitude / 1000).ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureDataCrossPoint(double crossPoint)
        {
            try
            {
                logger.AdapterLogString(0, "DataCrossPoint is" + crossPoint.ToString());
                return MyIO.WriteString(":OUTPut:DATA:CPOint DATA," + crossPoint.ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureClockSwitch(byte clkSwitch)
        {
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
                logger.AdapterLogString(0, "ClockSwitch is" + strClkSwitch);
                return MyIO.WriteString(":OUTPut:CLOCk:OUTPut " + strClkSwitch);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureOutputSwitch(byte outSwitch)
        {
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
                logger.AdapterLogString(0, "OutputSwitch is" + strOutSwitch);
                return MyIO.WriteString(":SOURce:OUTPut:ASET " + strOutSwitch);
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
                        MyIO = new IOPort(IOType, "GPIB::" + Addr);//new IO_Port(IO_Type, Addr);
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
                    ConfigureSlot(slot);
                    for (byte i = 1; i <= totalChannels; i++)
                    {
                        ConfigureChannel(i);
                        ConfigureDataInputInterface(dataInputInterface);
                        ConfigurePatternType(patternType);
                        ConfigurePrbsLength(prbsLength);
                        ConfigureErrorResultZoomSwitch(errorResultZoom);
                    }
                   // ConfigureChannel(1);
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
             ConfigureSlot(slot);
             byte channelbyte = Convert.ToByte(channel);
             logger.AdapterLogString(0, "channel is" + channel);
              //ConfigureChannel(1);
             return ConfigureChannel(channelbyte);
              
         }
         public override bool configoffset(string channel, string offset)
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
        public override double GetErrorRate()
        {
            ConfigureSlot(slot);
            ConfigureChannel(currentChannel);
            ConfigureEdGatingMode(edGatingMode);
            ConfigureEdGatingUnit(edGatingUnit);
            ConfigureEdGatingPeriod(edGatingTime);
            EdGatingStart();
            Thread.Sleep(Convert.ToInt32(edGatingTime * 1000));
            return QureyEdErrorRatio();
        }
         /// <summary>
        /// 设置ED的通道
        /// </summary>
        /// <param name="slot"> </param>
        /// <returns> </returns>
        private bool ConfigureSlot(byte slot)
        {
            try
            {
                logger.AdapterLogString(0, "slot is" + slot);
                return MyIO.WriteString(":MODule:ID " + slot.ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureChannel(int  channel)
        {
            try
            {
                logger.AdapterLogString(0, "channel is" + channel);
                return MyIO.WriteString(":INTerface:ID " + channel.ToString());
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
        private bool ConfigureDataInputInterface(byte dataInterface)//0=SingleEnd,1=Diff 50ohm,2=Diff 100ohm;
        {
            string DataInterface;
            switch (dataInterface)
            {
                case 0:
                    DataInterface = "SINGle";
                    break;
                case 1:
                    DataInterface = "DIF50ohm";
                    break;
                case 2:
                    DataInterface = "DIF100ohm";
                    break;
                default:
                    DataInterface = "SINGle";
                    break;
            }

            try
            {
                logger.AdapterLogString(0, "DataInputInterface is" + DataInterface);
                return MyIO.WriteString(":INPut:DATA:INTerface " + DataInterface);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigurePatternType(byte patternType)//0=PRBS,1=Zero Subsitution,2=Data,3=Alternate,4=Mixed Data,5=Sequense
        {
            string PatternType;
            switch (patternType)
            {
                case 0:
                    PatternType = "PRBS";
                    break;
                case 1:
                    PatternType = "ZSUBstitution";
                    break;
                case 2:
                    PatternType = "DATA";
                    break;
                case 3:
                    PatternType = "ALT";
                    break;
                case 4:
                    PatternType = "MIXData";
                    break;
                case 5:
                    PatternType = "SEQuence";
                    break;
                default:
                    PatternType = "PRBS";
                    break;
            }
            try
            {
                logger.AdapterLogString(0, "PatternType is" + PatternType);
                return MyIO.WriteString(":SENSe:PATTern:TYPE " + PatternType);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigurePrbsLength(byte prbsLength)
        {
            try
            {
                logger.AdapterLogString(0, "prbsLength is" + prbsLength);
                return MyIO.WriteString(":SENSe:PATTern:PRBS:LENGth " + prbsLength.ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureErrorResultZoomSwitch(byte ZoomSwitch)
        {
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
                logger.AdapterLogString(0, "ErrorResultZoomSwitch is" + strSwitch);
                return MyIO.WriteString(":DISPlay:RESult:ZOOM " + strSwitch);
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
        private bool ConfigureEdGatingMode(byte gatingMode)//0=REPeat,1=UNTimed,2=SINGle
        {
            string strGatingMode;
            switch (gatingMode)
            {
                case 0:
                    strGatingMode = "REPeat";
                    break;
                case 1:
                    strGatingMode = "UNTimed";
                    break;
                case 2:
                    strGatingMode = "SINGle";
                    break;
                default:
                    strGatingMode = "REPeat";
                    break;
            }
            try
            {
                logger.AdapterLogString(0, "EdGatingMode is" + strGatingMode);
                return MyIO.WriteString(":SENSe:MEASure:EALarm:MODE " + strGatingMode);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureEdGatingUnit(byte gatingUnit)//0=TIME,1=BLOCk,2=CLOCk,3=ERRor
        {
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
                    strGatingUnit = "CLOCk";
                    break;
                case 3:
                    strGatingUnit = "ERRor";
                    break;
                default:
                    strGatingUnit = "TIME";
                    break;
            }
            try
            {
                logger.AdapterLogString(0, "EdGatingUnit is" + strGatingUnit);
                return MyIO.WriteString(":SENSe:MEASure:EALarm:UNIT " + strGatingUnit);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureEdGatingPeriod(int  gatingTime)
        {
            string time = "0,0,0,0";
            int   day, hour, min, sec;
            day = gatingTime / 86400;
            hour = (gatingTime - day * 86400) / 3600;
            min = (gatingTime - day * 86400 - hour * 3600) / 60;
            sec = gatingTime - day * 86400 - hour * 3600 - min * 60;
            time = day.ToString() + "," + hour.ToString() + "," + min.ToString() + "," + sec.ToString();
            try
            {
                logger.AdapterLogString(0, "EdGatingPeriod is" + time);
                return MyIO.WriteString(":SENSe:MEASure:EALarm:PERiod " + time);
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
                Thread.Sleep(50);
                Ber =MyIO.ReadString(100).Replace("\"","").Replace("\n","");
                ber = Convert.ToDouble(Ber);
                logger.AdapterLogString(0, "ErrorRatio is" + ber);
            }
            catch (Exception error)
            {
                ber = 1;
                logger.AdapterLogString(3, error.ToString());
                return ber;
            }
            return ber ;
        }
      
}
  
}
