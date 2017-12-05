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

namespace ATS_Framework
{
    public class MAP200CompSwtich : OpticalSwitch
    {

        public Algorithm algorithm = new Algorithm();



        class PersonAttribute : CommandInf
        {
            public string DeviceChannel;
            public string InputChannel;

            public string InputSlot;
            public string OutputSlot;

            public string LongChannel;
            public string ShortChannel;
        }

        PersonAttribute MyInf;


        public MAP200CompSwtich(logManager logmanager)
        {
            logger = logmanager;
            MyInf = new PersonAttribute();
            MyInf.DeviceChannel = "1";
        }

        #region 公共方法
        public override bool Initialize(TestModeEquipmentParameters[] EquipmentInfStruct)
        {
            try
            {
                int i = 0;

                if (algorithm.FindFileName(EquipmentInfStruct, "ADDR", out i))
                {
                    MyInf.Addr = Convert.ToByte(EquipmentInfStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }

                if (algorithm.FindFileName(EquipmentInfStruct, "IOTYPE", out i))
                {
                    MyInf.IOType = EquipmentInfStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }

                //if (algorithm.FindFileName(EquipmentInfStruct, "RESET", out i))
                //{
                //    MyInf.Reset = Convert.ToBoolean(EquipmentInfStruct[i].DefaultValue);
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no RESET");
                //    return false;
                //}

                if (algorithm.FindFileName(EquipmentInfStruct, "InputSlot", out i))
                {
                    MyInf.InputSlot = EquipmentInfStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALSWITCHSLOT");
                    return false;
                }

                if (algorithm.FindFileName(EquipmentInfStruct, "OutputSlot", out i))
                {
                    MyInf.OutputSlot = EquipmentInfStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALSWITCHSLOT");
                    return false;
                }


                if (algorithm.FindFileName(EquipmentInfStruct, "LongfiberChannel", out i))
                {
                    MyInf.LongChannel = EquipmentInfStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no LongChannel");
                    return false;
                }

                if (algorithm.FindFileName(EquipmentInfStruct, "ShortfiberChannel", out i))
                {
                    MyInf.ShortChannel = EquipmentInfStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ShortChannel");
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

                //if (algorithm.FindFileName(EquipmentInfStruct, "TOCHANNEL", out i))
                //{
                //    MyInf.ToChannel = EquipmentInfStruct[i].DefaultValue;
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no TOCHANNEL");
                //    return false;
                //}

                if (algorithm.FindFileName(EquipmentInfStruct, "NAME", out i))
                {
                    MyInf.Name = EquipmentInfStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }

                //if (algorithm.FindFileName(EquipmentInfStruct, "BidiTxChannel", out i))
                //{

                //    BidiTx_Channel = EquipmentInfStruct[i].DefaultValue.Split(',');
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no BidiTxChannel");
                //    return false;
                //}
                //if (algorithm.FindFileName(EquipmentInfStruct, "BidiRxChannel", out i))
                //{

                //    BidiRx_Channel = EquipmentInfStruct[i].DefaultValue.Split(',');
                //}
                //else
                //{
                //    logger.AdapterLogString(4, "there is no BidiRxChannel");
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
                switch (MyInf.IOType.Trim())
                {
                    case "GPIB":
                        MyIO = new IOPort(MyInf.IOType, "GPIB0::" + MyInf.Addr.ToString(), logger);

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
        ///// <summary>
        ///// 切换模块通道
        ///// </summary>
        ///// <param name="channel"></param>
        ///// <param name="syn"></param>
        ///// <returns></returns>
        //public override bool ChangeChannel(string channel, int syn = 0) 
        //{
        //    return Switchchannel(channel, syn);
        //}
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
        /// 
        public override bool Switchchannel(int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "OpticalSwitch change channel to " + ToChannel);
                    return MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
                        + "," + MyInf.InputSlot + "," + ToChannel);
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
                        + "," + MyInf.InputSlot + "," + ToChannel);

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
                                  MyInf.DeviceChannel + "," + MyInf.InputChannel);
                            // MyInf.
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
                    return MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
                        + "," + 0 + "," + channel);
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
                            + "," + MyInf.InputChannel + "," + channel);

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
                                  MyInf.DeviceChannel + "," + channel);

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
        /// <summary>
        /// 切换模式 (长光纤或者短光纤模式)
        /// </summary>
        /// <param name="Type">类别 0=长光纤;1=短光纤</param>
        /// <param name="syn">同步 1=同步;0=异步</param>
        /// <returns></returns>
       override public bool SelectMode(byte Type, int syn = 0)
        {
            bool flag = false;

            //string InputSlot = "1";
            //string OutputSlot = "1";
            string Ch = "1";

            switch (Type)
            {
                case 0:// LongFibber
                    Ch = MyInf.LongChannel;
                    break;
                default://shortfibber
                    Ch = MyInf.ShortChannel;
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "OpticalSwitch change channel to ");

                    MyIO.WriteString(":ROUT:CLOS " + MyInf.InputSlot + "," + MyInf.DeviceChannel
                        + ",1," + Ch);

                    MyIO.WriteString(":ROUT:CLOS " + MyInf.OutputSlot + "," + MyInf.DeviceChannel
                        + ",1," + Ch);

                    return true;
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        bool flag1 = MyIO.WriteString(":ROUT:CLOS " + MyInf.InputSlot + "," + MyInf.DeviceChannel
                             + ",1," + Ch);
                        bool flag2 = MyIO.WriteString(":ROUT:CLOS " + MyInf.OutputSlot + "," + MyInf.DeviceChannel
                            + ",1," + Ch);

                        if (flag1 && flag1)
                        {
                            tempFlag = true;
                            break; ;
                        }
                    }

                    if (tempFlag)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            bool flagInput = false;
                            bool flagOutput = false;

                            if (!flagInput)
                            {

                                Thread.Sleep(100);
                                MyIO.WriteString(":ROUT:CLOS? " + MyInf.InputSlot + "," +
                                      MyInf.DeviceChannel + ",1");


                                readtemp = MyIO.ReadString().Replace("\n", "").Trim();


                                if (Convert.ToByte(readtemp) == Convert.ToByte(Ch))
                                {
                                    flagInput = true;
                                    break;
                                }
                            }
                            if (!flagOutput)
                            {

                                Thread.Sleep(100);
                                MyIO.WriteString(":ROUT:CLOS? " + MyInf.OutputSlot + "," +
                                      MyInf.DeviceChannel + ",1");


                                readtemp = MyIO.ReadString().Replace("\n", "").Trim();


                                if (Convert.ToByte(readtemp) == Convert.ToByte(Ch))
                                {
                                    flagOutput = true;
                                    break;
                                }
                            }
                            if (flagInput && flagOutput)
                            {
                                flag = true;
                            }

                        }

                        if (k <= 2)
                        {
                            logger.AdapterLogString(0, "OpticalSwitch change channel to " + Ch);

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

        public override bool CheckEquipmentRole(byte TestModelType, byte Channel)
        {//// 0=NA,1=TX,2=RX
            string StrChannel = "1";

            if (Role == 0)//TX Rx 公用
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

