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

namespace ATS_Driver
{
    public class MAP200PowerMeter : PowerMeter
    {
        public string DeviceChannel = "1";

        public Algorithm algorithm = new Algorithm();
        public MAP200PowerMeter(logManager logmanager)
        {
            logger = logmanager;
        }

        #region 公共方法
        public override bool Initialize(TestModeEquipmentParameters[] MAP200PowerMeterStruct)
        {
            try
            {
                int i = 0;

                if (algorithm.FindFileName(MAP200PowerMeterStruct, "ADDR", out i))
                {
                    Addr = MAP200PowerMeterStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }

                if (algorithm.FindFileName(MAP200PowerMeterStruct, "IOTYPE", out i))
                {
                    IOType = MAP200PowerMeterStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }

                if (algorithm.FindFileName(MAP200PowerMeterStruct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(MAP200PowerMeterStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }

                if (algorithm.FindFileName(MAP200PowerMeterStruct, "POWERMETERSLOT", out i))
                {
                    PowerMeterSlot = MAP200PowerMeterStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no POWERMETERSLOT");
                    return false;
                }

                //if (algorithm.FindFileName(MAP200PowerMeterStruct, "DEVICECHANNEL", out i))
                //{
                //    DeviceChannel = MAP200PowerMeterStruct[i].DefaultValue;
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no DEVICECHANNEL");
                //    return false;
                //}

                if (algorithm.FindFileName(MAP200PowerMeterStruct, "POWERMETERWAVELENGTH", out i))
                {
                    PowerMeterWavelength = MAP200PowerMeterStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no POWERMETERWAVELENGTH");
                    return false;
                }

                if (algorithm.FindFileName(MAP200PowerMeterStruct, "UNITTYPE", out i))
                {
                    UnitType = MAP200PowerMeterStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no UNITTYPE");
                    return false;
                }

                if (algorithm.FindFileName(MAP200PowerMeterStruct, "NAME", out i))
                {
                    Name = MAP200PowerMeterStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }

                if (!Connect())
                {
                    return false;
                }
            }
            catch (Error_Message error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
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

                        if (MyIO.IOConnect()) // 判断仪器是否连接成功
                        {
                            string strIDN = "";
                            MyIO.WriteString("*IDN?"); // 读仪器标识
                            strIDN = MyIO.ReadString();

                            if (strIDN.Contains("JDSU,MAP-200")) // 读仪器标识，再次确认仪器是否已经连接成功
                            {
                                EquipmentConnectflag = true;
                            }
                            else
                            {
                                EquipmentConnectflag = false;
                            }
                        }
                        else
                        {
                            EquipmentConnectflag = false;
                        }
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
                logger.FlushLogBuffer();
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

                    ConfigLegacyModeOff();

                    ConfigWavelength(syn);
                    Selectunit(syn);
                    EquipmentConfigflag = true;
                }
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }
        public override bool ConfigWavelength(int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PowerMeterSlot is " + PowerMeterSlot +
                        ", DeviceChannel is " + DeviceChannel + ", Wavelength is " + PowerMeterWavelength + "nm");
                    flag = MyIO.WriteString(":SENS:POW:WAV " + PowerMeterSlot + "," + DeviceChannel + "," + PowerMeterWavelength);
                    return flag;
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString(":SENS:POW:WAV " + PowerMeterSlot + "," + DeviceChannel + "," + PowerMeterWavelength);

                        if (tempFlag)
                        {
                            break;
                        }
                    }

                    if (tempFlag)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(100);
                            MyIO.WriteString(":SENS:POW:WAV? " + PowerMeterSlot + "," + DeviceChannel);

                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble(PowerMeterWavelength))
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            logger.AdapterLogString(0, "PowerMeterSlot is " + PowerMeterSlot +
                                ", DeviceChannel is " + DeviceChannel + ", Wavelength is " + PowerMeterWavelength + "nm");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "PowerMeter ConfigWavelength wrong");
                        }
                    }

                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        } 
        public override double ReadPower()
        {
            double power = 0;

            try
            {
                MyIO.WriteString(":FETch:POWer? " + PowerMeterSlot + "," + DeviceChannel);
                power = Convert.ToDouble(MyIO.ReadString(32));
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return power;
            }
            return power;
        }
        public override bool Selectunit(int syn = 0) // 0 "dBm", 1 "W"  
        {
            bool flag = false;
            string strUnitType = "";

            switch (UnitType)
            {
                case "0":
                    strUnitType = "dBm";
                    break;
                case "1":
                    strUnitType = "W";
                    break;
                default:
                    strUnitType = "dBm";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PowerMeterSlot is " + PowerMeterSlot +
                        ", DeviceChannel is " + DeviceChannel + ", UnitType is " + strUnitType);
                    return MyIO.WriteString(":SENSe:POWer:UNIT " + PowerMeterSlot + "," + DeviceChannel + "," + UnitType);
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString(":SENSe:POWer:UNIT " + PowerMeterSlot + "," + DeviceChannel + "," + UnitType);

                        if (tempFlag)
                        {
                            break;
                        }
                    }

                    if (tempFlag)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(100);
                            MyIO.WriteString(":SENSe:POWer:UNIT? " + PowerMeterSlot + "," + DeviceChannel);

                            readtemp = MyIO.ReadString();
                            if (readtemp.Trim().ToUpper() == UnitType.Trim().ToUpper())
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            logger.AdapterLogString(0, "PowerMeterSlot is " + PowerMeterSlot +
                                ", DeviceChannel is " + DeviceChannel + ", UnitType is " + strUnitType);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "PowerMeter Select UnitType wrong");
                        }
                    }

                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 查询JDSU是否处于LegacyMode，如果是，则关闭JDSULegacyMode
        /// </summary>
        /// <returns></returns>
        private bool ConfigLegacyModeOff()
        {
            string readtemp = "";
            bool flag = false;
            int k = 0;

            try
            {
                for (k = 0; k < 3; k++)
                {
                    MyIO.WriteString("SYST:LEGA:MODE?");
                    readtemp = MyIO.ReadString();

                    if (Convert.ToInt32(readtemp) == 0)
                    {
                        break;
                    }
                    else
                    {
                        MyIO.WriteString("SYST:LEGA:MODE 0");
                    }
                }

                if (k <= 2)
                {
                    logger.AdapterLogString(0, "Disable JDSU legacy mode");
                    flag = true;
                }
                else
                {
                    logger.AdapterLogString(3, "Configure JDSU legacy mode wrong");
                }

                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
    }
}