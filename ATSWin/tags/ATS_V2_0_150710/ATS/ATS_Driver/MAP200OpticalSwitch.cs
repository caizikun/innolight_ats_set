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
    public class MAP200OpticalSwitch : OpticalSwitch
    {
        public string DeviceChannel = "1";
        public string InputChannel = "1";
        public Algorithm algorithm = new Algorithm();

        public MAP200OpticalSwitch(logManager logmanager)
        {
            logger = logmanager;
        }

        #region 公共方法
        public override bool Initialize(TestModeEquipmentParameters[] MAP200OpticalSwitchStruct)
        {
            try
            {
                int i=0;

                if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(MAP200OpticalSwitchStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }

                if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "IOTYPE", out i))
                {
                    IOType = MAP200OpticalSwitchStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }

                if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(MAP200OpticalSwitchStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }

                if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "OPTICALSWITCHSLOT", out i))
                {
                    OpticalSwitchSlot = MAP200OpticalSwitchStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALSWITCHSLOT");
                    return false;
                }

                //if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "SWITCHCHANNEL", out i))
                //{
                //    SwitchChannel = MAP200OpticalSwitchStruct[i].DefaultValue;
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no SWITCHCHANNEL");
                //    return false;
                //}

                if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "TOCHANNEL", out i))
                {
                    ToChannel = MAP200OpticalSwitchStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no TOCHANNEL");
                    return false;
                }

                if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "NAME", out i))
                {
                    Name = MAP200OpticalSwitchStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }

                //if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "DEVICECHANNEL", out i))
                //{
                //    DeviceChannel = MAP200OpticalSwitchStruct[i].DefaultValue;
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no DEVICECHANNEL");
                //    return false;
                //}

                //if (algorithm.FindFileName(MAP200OpticalSwitchStruct, "INPUTCHANNEL", out i))
                //{
                //    InputChannel = MAP200OpticalSwitchStruct[i].DefaultValue;
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no INPUTCHANNEL");
                //    return false;
                //}

                if (!Connect())
                {
                    return false;
                }
            }
            catch (Exception error)
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
                if (EquipmentConfigflag) // 曾经设定过
                {
                    return true;
                }
                else // 未曾经设定过
                {
                    if (Reset == true)
                    {
                        ReSet();
                    }

                    ConfigLegacyModeOff();

                    Switchchannel(syn);
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
        /// <summary>
        /// 切换模块通道
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool ChangeChannel(string channel, int syn = 0) 
        {
            return Switchchannel(channel, syn);
        }
        /// <summary>
        /// 切换模块通道时，收集OFFSET
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="offset"></param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }
        /// <summary>
        /// 切换通道
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool Switchchannel(int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "OpticalSwitch change channel to " + ToChannel);
                    return MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
                        + "," + InputChannel + "," + ToChannel);
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
                            + "," + InputChannel + "," + ToChannel);

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
                            MyIO.WriteString(":ROUT:CLOS? " + OpticalSwitchSlot + "," +
                                DeviceChannel + "," + InputChannel);

                            readtemp = MyIO.ReadString();
                            if (Convert.ToByte(readtemp) == Convert.ToByte(ToChannel))
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            logger.AdapterLogString(0, "OpticalSwitch change channel to " + ToChannel);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "OpticalSwitch change channel wrong");
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
        /// <summary>
        /// 切换通道
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public bool Switchchannel(string channel, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "OpticalSwitch change channel to " + channel);
                    return MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
                        + "," + InputChannel + "," + channel);
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
                            + "," + InputChannel + "," + channel);

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
                            MyIO.WriteString(":ROUT:CLOS? " + OpticalSwitchSlot + "," +
                                DeviceChannel + "," + channel);

                            readtemp = MyIO.ReadString();
                            if (Convert.ToByte(readtemp) == Convert.ToByte(channel))
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            logger.AdapterLogString(0, "OpticalSwitch change channel to " + channel);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "OpticalSwitch change channel wrong");
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
