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
    public class ElectricalSwitch : ElecSwitch
    {
        public Algorithm algorithm = new Algorithm();
        public ElectricalSwitch(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] SwitchStruct)
        {
            try
            {
               
                int i = 0;
                if (algorithm.FindFileName(SwitchStruct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(SwitchStruct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(SwitchStruct, "IOTYPE", out i))
                {
                    IOType = SwitchStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(SwitchStruct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(SwitchStruct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(SwitchStruct, "ELECSWITCHCHANNEL", out i))
                {
                    ElecSwitchChannel = SwitchStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(SwitchStruct, "NAME", out i))
                {
                    Name = SwitchStruct[i].DefaultValue;
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
        //IOPort USBIO;
        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":
                        MyIO = new IOPort(IOType, "GPIB::" + Addr);
                        EquipmentConnectflag = true; ;
                        break;
                        
                    case "USB":
                        MyIO = new IOPort(IOType, Addr.ToString());
                        EquipmentConnectflag = true; ;
                        break;
                    default:
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
            return IOPort.resetdevice(Addr);
        }
        public override bool ChangeChannel(string channel)
        {
            return ChangeElecSwitchChannel(channel);
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
                    ChangeElecSwitchChannel();
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

        protected bool ChangeElecSwitchChannel(string channel)
        {
            bool flag = false;
            int index;
            int intchannel;
            intchannel = Convert.ToInt16(channel) - 1;
            try
            {
                switch (intchannel)
                {
                    case 0:
                        index = 0x11;
                        break;
                    case 1:
                        index = 0x22;
                        break;
                    case 2:
                        index = 0x44;
                        break;
                    default:
                        index = 0x88;
                        break;
                }
                flag = MyIO.WritePort(0, Addr, index, 0xFF);
                logger.AdapterLogString(0, "ElecSwitchChannel is" + channel);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }


        }
        public override bool ChangeElecSwitchChannel()
        {
            bool flag = false;
            int index;
            int intchannel;
            intchannel = Convert.ToInt16(ElecSwitchChannel) - 1;
            try
            {
                switch (intchannel)
                {
                    case 0:
                        index = 0x11;
                        break;
                    case 1:
                        index = 0x22;
                        break;
                    case 2:
                        index = 0x44;
                        break;
                    default:
                        index = 0x88;
                        break;
                }
                flag= MyIO.WritePort(0, Addr, index, 0xFF);
                logger.AdapterLogString(0, "ElecSwitchChannel is" + ElecSwitchChannel);
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
