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
    public class AQ2211OpticalSwitch : OpticalSwitch
    {
        public Algorithm algorithm = new Algorithm();
        public AQ2211OpticalSwitch(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] AQ2211OpticalSwitchstruct)
        {
            try
            {
                int i=0;
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"ADDR",out i))
                {
                    Addr = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"IOTYPE",out i))
                {
                    IOType = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(AQ2211OpticalSwitchstruct[i].DefaultValue);
                    
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"OPTICALSWITCHSLOT",out i))
                {
                    OpticalSwitchSlot = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"SWITCHCHANNEL",out i))
                {
                    SwitchChannel = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"TOCHANNEL",out i))
                {
                    ToChannel = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"NAME",out i))
                {
                    Name = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                    return false;

                if (!Connect()) return false;
            }
            catch (Exception error)
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
        public override bool ChangeChannel(string channel) 
        { 
            return Switchchannel(channel);
        }
        public override bool configoffset(string channel, string offset)
        {
            return true;
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
                    Switchchannel();
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
       protected bool Switchchannel(string channel)
        {
            bool flag = false;
            try
            {
                flag = MyIO.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + channel);
                
                logger.AdapterLogString(0, "change channel to" + channel);  
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool Switchchannel()
        {
            bool flag = false;
            try
            {
                flag = MyIO.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + ToChannel);
                
                logger.AdapterLogString(0, "change channel to" + ToChannel);
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
