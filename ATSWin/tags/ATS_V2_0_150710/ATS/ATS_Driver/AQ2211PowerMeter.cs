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
    public class AQ2211PowerMeter : PowerMeter
    {
        public Algorithm algorithm = new Algorithm();
        public AQ2211PowerMeter(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] AQ2211PowerMeterstruct)
        {
            try
            {
                int i = 0;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"ADDR",out i))
                {
                    Addr = Convert.ToByte(AQ2211PowerMeterstruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"IOTYPE",out i))
                {
                    IOType = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(AQ2211PowerMeterstruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERSLOT",out i))
                {
                    PowerMeterSlot = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no POWERMETERSLOT");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERWAVELENGTH",out i))
                {
                    PowerMeterWavelength = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no POWERMETERWAVELENGTH");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERCHANNEL",out i))
                {
                    PowerMeterChannel =AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no POWERMETERCHANNEL");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"UNITTYPE",out i))
                {
                    UnitType = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no UNITTYPE");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"NAME",out i))
                {
                    Name = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }

                if (!Connect()) return false;
            }
            catch (Error_Message error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
            return true;
        }
        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":

                        MyIO = new IOPort(IOType, "GPIB::" + Addr, logger);
                        MyIO.IOConnect();
                        MyIO.WriteString("*IDN?");
                        EquipmentConnectflag = MyIO.ReadString().Contains("AQ2211");
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
                EquipmentConnectflag = false;
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
                    ConfigWavelength(syn);
                    Selectunit(syn);
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
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }

        public override bool ConfigWavelength(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double waveinput;
            double waveoutput;
            try
            {
                //flag = MyIO.WriteString(":sense" + AttSlot + ":channel " + AttChannel + ":power:wavelength" + wavtemp[i] + "E-9");
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PowerMeterSlot is" + PowerMeterSlot + "Wavelength is" + PowerMeterWavelength + "nm");
                    return MyIO.WriteString(":INP" + PowerMeterSlot + ":WAV " + PowerMeterWavelength + "nm");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":INP" + PowerMeterSlot + ":WAV " + PowerMeterWavelength + "nm");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":INP" + PowerMeterSlot + ":WAV?");
                            readtemp = MyIO.ReadString();
                            waveinput = Convert.ToDouble(PowerMeterWavelength);
                            waveoutput = Convert.ToDouble(readtemp) * Math.Pow(10, 9);
                            if (waveinput == waveoutput)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "PowerMeterSlot is" + PowerMeterSlot + "Wavelength is" + PowerMeterWavelength + "nm");

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
                return false;
            }
        
        }
        public override double ReadPower()
        {
            double power=0;
            try
            {
                MyIO.WriteString(":fetch%d:channel%d:power?" + PowerMeterSlot + PowerMeterChannel + ":ATT?");
                power = Convert.ToDouble(MyIO.ReadString(32));
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());

                return power;
            }
            return power;
        }
        public override bool Selectunit(int syn = 0)//0 "dBm",1 "W"
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PowerMeterSlot is" + PowerMeterSlot + "Channel is" + PowerMeterChannel + "UnitType is" + UnitType);
                    return MyIO.WriteString("SENS" + PowerMeterSlot + ":CHAN" + PowerMeterChannel + ":POW:UNIT " + UnitType);
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString("SENS" + PowerMeterSlot + ":CHAN" + PowerMeterChannel + ":POW:UNIT " + UnitType);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("SENS" + PowerMeterSlot + ":CHAN" + PowerMeterChannel + ":POW:UNIT?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == UnitType)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "PowerMeterSlot is" + PowerMeterSlot + "Channel is" + PowerMeterChannel + "UnitType is" + UnitType);
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
                return false;
            }

        }
    }
}
