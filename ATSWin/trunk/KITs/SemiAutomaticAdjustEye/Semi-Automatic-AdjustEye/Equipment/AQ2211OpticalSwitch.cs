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
                    Addr = Convert.ToByte(AQ2211OpticalSwitchstruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct, "IOTYPE", out i))
                {
                    IOType = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"OPTICALSWITCHSLOT",out i))
                {
                    OpticalSwitchSlot = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALSWITCHSLOT");
                    return false;
                }
                if (algorithm.FindFileName(AQ2211OpticalSwitchstruct,"SWITCHCHANNEL",out i))
                {
                    SwitchChannel = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no SWITCHCHANNEL");
                    return false;
                }
                
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

                        MyIO = new IOPort(IOType, "GPIB::" + Addr, logger);
                        MyIO.IOConnect();
                        MyIO.WriteString("*IDN?");
                        EquipmentConnectflag = MyIO.ReadString().Contains("AQ22");
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
        public override bool ChangeChannel(string channel, int syn = 0) 
        {
            return Switchchannel(channel, syn);
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
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
        public bool Switchchannel(string channel, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "OpticalSwitch change channel to" + channel);
                    return MyIO.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + channel);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + channel);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":route" + OpticalSwitchSlot + "?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == " A," + channel)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "OpticalSwitch change channel to" + channel);
                            flag = true;
                        }
                        else
                        {

                            logger.AdapterLogString(3, "OpticalSwitch switchchannel wrong");
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
        public override bool Switchchannel(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "OpticalSwitch change channel to" + ToChannel);
                    return MyIO.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + ToChannel);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + ToChannel);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":route" + OpticalSwitchSlot + "?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == "A," + ToChannel)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "OpticalSwitch change channel to" + ToChannel);

                            flag = true;
                        }
                        else
                            logger.AdapterLogString(3, "OpticalSwitch change channel wrong");

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

        public override bool CheckEquipmentRole(byte TestModelType, byte Channel)
        {//// 0=NA,1=TX,2=RX
            string StrChannel="1";

            if (Role==0)//TX Rx 公用
            {
                if (TestModelType == 1)//Tx
                {

                    StrChannel = BidiTx_Channel[Channel - 1].ToString();
                }
                if (TestModelType == 2)//Rx
                {

                    StrChannel = BidiRx_Channel[Channel - 1].ToString();
                }
                ChangeChannel(StrChannel, 1);
            }

            return true;

           
        }
    }
}
