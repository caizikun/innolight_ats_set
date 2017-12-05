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
                    Addr = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"IOTYPE",out i))
                {
                    IOType = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(AQ2211PowerMeterstruct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERSLOT",out i))
                {
                    PowerMeterSlot = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERWAVELENGTH",out i))
                {
                    PowerMeterWavelength = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERCHANNEL",out i))
                {
                    PowerMeterChannel =AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"UNITTYPE",out i))
                {
                    UnitType = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211PowerMeterstruct,"NAME",out i))
                {
                    Name = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                    return false;

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

                        MyIO = new IOPort(IOType, "GPIB::" + Addr);

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
                    ConfigWavelength();
                    Selectunit();
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
            return true;
        }
        public override bool configoffset(string channel, string offset)
        {
            return true;
        }
        
        public override bool ConfigWavelength()
        {
            bool flag = false;
            flag = MyIO.WriteString(":INP" + PowerMeterSlot + ":WAV " + PowerMeterWavelength + "nm"); 
            logger.AdapterLogString(0, "AttSlot is" + PowerMeterSlot + "Wavelength is" + PowerMeterWavelength + "nm");
            return flag;
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
        public override bool Selectunit()//0 "dBm",1 "W"
        {
            bool flag = false;
            try
            {

                flag = MyIO.WriteString("SENS" + PowerMeterSlot + ":CHAN" + PowerMeterChannel + ":POW:UNIT " + UnitType);
                logger.AdapterLogString(0, "AttSlot is" + PowerMeterSlot + "Channel is" + PowerMeterChannel + "UnitType is" + UnitType);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
    }
}
