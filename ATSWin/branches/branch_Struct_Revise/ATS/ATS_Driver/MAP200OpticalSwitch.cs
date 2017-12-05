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
        private static object syncRoot = SyncRoot_MAP200.Get_SyncRoot_MAP200();//used for thread synchronization

        #region 公共方法
        public override bool Initialize(TestModeEquipmentParameters[] MAP200OpticalSwitchStruct)
        {
            lock (syncRoot)
            {
                try
                {
                    int i = 0;

                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "ADDR", out i))
                    {
                        Addr = Convert.ToByte(MAP200OpticalSwitchStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ADDR");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "IOTYPE", out i))
                    {
                        IOType = MAP200OpticalSwitchStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(MAP200OpticalSwitchStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "OPTICALSWITCHSLOT", out i))
                    {
                        OpticalSwitchSlot = MAP200OpticalSwitchStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no OPTICALSWITCHSLOT");
                        return false;
                    }

                    //if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "SWITCHCHANNEL", out i))
                    //{
                    //    SwitchChannel = MAP200OpticalSwitchStruct[i].DefaultValue;
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no SWITCHCHANNEL");
                    //    return false;
                    //}

                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "TOCHANNEL", out i))
                    {
                        ToChannel = MAP200OpticalSwitchStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TOCHANNEL");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "NAME", out i))
                    {
                        Name = MAP200OpticalSwitchStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "BidiTxChannel", out i))
                    {

                        BidiTx_Channel = MAP200OpticalSwitchStruct[i].DefaultValue.Split(',');
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no BidiTxChannel");
                        return false;
                    }
                    if (Algorithm.FindFileName(MAP200OpticalSwitchStruct, "BidiRxChannel", out i))
                    {

                        BidiRx_Channel = MAP200OpticalSwitchStruct[i].DefaultValue.Split(',');
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no BidiRxChannel");
                        return false;
                    }

                    if (!Connect())
                    {
                        return false;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());

                    return false;
                }

                return true;
            }
        }
        public override bool Connect()
        {
            try
            {
                // IO_Type
                switch (IOType)
                {
                    case "GPIB":
                        lock (syncRoot)
                        {
                            this.WriteString("*IDN?");
                            string content = this.ReadString();
                            this.EquipmentConnectflag = content.Contains("JDSU,MAP-200");

                        }
                        break;
                    default:
                        Log.SaveLogToTxt("GPIB类型错误");
                        EquipmentConnectflag = false;
                        break;
                }

                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                Log.SaveLogToTxt(error.ToString());
                
                return false;
            }
        }
        public bool ReSet()
        {
            lock (syncRoot)
            {
                if (this.WriteString("*RST"))
                {
                    Thread.Sleep(3000);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public override bool Configure(int syn = 0)
        {
            lock (syncRoot)
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
                    Log.SaveLogToTxt(error.ToString());

                    return false;
                }
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
            lock (syncRoot)
            {
                return Switchchannel(channel, syn);
            }
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
        /// 
        public override bool Switchchannel(int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;

                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("OpticalSwitch change channel to " + ToChannel);
                        return this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
                            + "," + InputChannel + "," + ToChannel);
                    }
                    else
                    {
                        bool tempFlag = false;
                        string readtemp = "";
                        int k = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            tempFlag = this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
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
                                this.WriteString(":ROUT:CLOS? " + OpticalSwitchSlot + "," +
                                    DeviceChannel + "," + InputChannel);

                                readtemp = this.ReadString();
                                if (Convert.ToByte(readtemp) == Convert.ToByte(ToChannel))
                                {
                                    break;
                                }
                            }

                            if (k <= 2)
                            {
                                Log.SaveLogToTxt("OpticalSwitch change channel to " + ToChannel);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("OpticalSwitch change channel wrong");
                            }
                        }

                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());

                    return false;
                }
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
            lock (syncRoot)
            {
                bool flag = false;

                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("OpticalSwitch change channel to " + channel);
                        return this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
                            + "," + InputChannel + "," + channel);
                    }
                    else
                    {
                        bool tempFlag = false;
                        string readtemp = "";
                        int k = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            tempFlag = this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + DeviceChannel
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
                                this.WriteString(":ROUT:CLOS? " + OpticalSwitchSlot + "," +
                                    DeviceChannel + "," + channel);

                                readtemp = this.ReadString();
                                if (Convert.ToByte(readtemp) == Convert.ToByte(channel))
                                {
                                    break;
                                }
                            }

                            if (k <= 2)
                            {
                                Log.SaveLogToTxt("OpticalSwitch change channel to " + channel);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("OpticalSwitch change channel wrong");
                            }
                        }

                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());

                    return false;
                }
            }
        } 
        #endregion

        /// <summary>
        /// 查询JDSU是否处于LegacyMode，如果是，则关闭JDSULegacyMode
        /// </summary>
        /// <returns></returns>
        private bool ConfigLegacyModeOff()
        {
            lock (syncRoot)
            {
                string readtemp = "";
                bool flag = false;
                int k = 0;

                try
                {
                    for (k = 0; k < 3; k++)
                    {
                        this.WriteString("SYST:LEGA:MODE?");
                        readtemp = this.ReadString();

                        if (Convert.ToInt32(readtemp) == 0)
                        {
                            break;
                        }
                        else
                        {
                            this.WriteString("SYST:LEGA:MODE 0");
                        }
                    }

                    if (k <= 2)
                    {
                        Log.SaveLogToTxt("Disable JDSU legacy mode");
                        flag = true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Configure JDSU legacy mode wrong");
                    }

                    return flag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());


                    return false;
                }
            }
        }

        public override bool CheckEquipmentRole(byte TestModelType, byte Channel)
        {//// 0=NA,1=TX,2=RX
            lock (syncRoot)
            {
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
}
