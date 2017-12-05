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
    public class MAP200CompSwtich : OpticalSwitch
    {




        
        class PersonAttribute : CommandInf
        {
            public string DeviceChannel;
            public string InputChannel;

            public string InputSlot;
            public string OutputSlot;

            public string LongChannel;
            public string ShortChannel;
        }


        private static object syncRoot = SyncRoot_MAP200.Get_SyncRoot_MAP200();//used for thread synchronizat


        private PersonAttribute MyInf = new PersonAttribute();
          
        

        #region 公共方法
        public override bool Initialize(TestModeEquipmentParameters[] EquipmentInfStruct)
        {
            lock (syncRoot)
            {
                try
                {
                    int i = 0;
                    MyInf.DeviceChannel = "1";
                    if (Algorithm.FindFileName(EquipmentInfStruct, "ADDR", out i))
                    {
                        MyInf.Addr = Convert.ToByte(EquipmentInfStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ADDR");
                        return false;
                    }

                    if (Algorithm.FindFileName(EquipmentInfStruct, "IOTYPE", out i))
                    {
                        MyInf.IOType = EquipmentInfStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }

                    //if (Algorithm.FindFileName(EquipmentInfStruct, "RESET", out i))
                    //{
                    //    MyInf.Reset = Convert.ToBoolean(EquipmentInfStruct[i].DefaultValue);
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no RESET");
                    //    return false;
                    //}

                    if (Algorithm.FindFileName(EquipmentInfStruct, "InputSlot", out i))
                    {
                        MyInf.InputSlot = EquipmentInfStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no OPTICALSWITCHSLOT");
                        return false;
                    }

                    if (Algorithm.FindFileName(EquipmentInfStruct, "OutputSlot", out i))
                    {
                        MyInf.OutputSlot = EquipmentInfStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no OPTICALSWITCHSLOT");
                        return false;
                    }


                    if (Algorithm.FindFileName(EquipmentInfStruct, "LongfiberChannel", out i))
                    {
                        MyInf.LongChannel = EquipmentInfStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no LongChannel");
                        return false;
                    }

                    if (Algorithm.FindFileName(EquipmentInfStruct, "ShortfiberChannel", out i))
                    {
                        MyInf.ShortChannel = EquipmentInfStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ShortChannel");
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

                    //if (Algorithm.FindFileName(EquipmentInfStruct, "TOCHANNEL", out i))
                    //{
                    //    MyInf.ToChannel = EquipmentInfStruct[i].DefaultValue;
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no TOCHANNEL");
                    //    return false;
                    //}

                    if (Algorithm.FindFileName(EquipmentInfStruct, "NAME", out i))
                    {
                        MyInf.Name = EquipmentInfStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
                        return false;
                    }

                    //if (Algorithm.FindFileName(EquipmentInfStruct, "BidiTxChannel", out i))
                    //{

                    //    BidiTx_Channel = EquipmentInfStruct[i].DefaultValue.Split(',');
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no BidiTxChannel");
                    //    return false;
                    //}
                    //if (Algorithm.FindFileName(EquipmentInfStruct, "BidiRxChannel", out i))
                    //{

                    //    BidiRx_Channel = EquipmentInfStruct[i].DefaultValue.Split(',');
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no BidiRxChannel");
                    //    return false;
                    //}

                    if (!Connect())
                    {
                        return false;
                    }
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + error.ID + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Los_parameter_0x05102 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }

                return true;
            }
        }
        public override bool Connect()
        {
            try
            {
                // IO_Type
                switch (MyInf.IOType.Trim())
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
            lock (syncRoot)
            {
                bool flag = false;

                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("OpticalSwitch change channel to " + ToChannel);
                        return this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
                            + "," + MyInf.InputSlot + "," + ToChannel);
                    }
                    else
                    {
                        bool tempFlag = false;
                        string readtemp = "";
                        int k = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            tempFlag = this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
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
                                this.WriteString(":ROUT:CLOS? " + OpticalSwitchSlot + "," +
                                      MyInf.DeviceChannel + "," + MyInf.InputChannel);
                                // MyInf.
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
                        return this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
                            + "," + 0 + "," + channel);
                    }
                    else
                    {
                        bool tempFlag = false;
                        string readtemp = "";
                        int k = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            tempFlag = this.WriteString(":ROUT:CLOS " + OpticalSwitchSlot + "," + MyInf.DeviceChannel
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
                                this.WriteString(":ROUT:CLOS? " + OpticalSwitchSlot + "," +
                                      MyInf.DeviceChannel + "," + channel);

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
        /// <summary>
        /// 切换模式 (长光纤或者短光纤模式)
        /// </summary>
        /// <param name="Type">类别 0=长光纤;1=短光纤</param>
        /// <param name="syn">同步 1=同步;0=异步</param>
        /// <returns></returns>
       override public bool SelectMode(byte Type, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("OpticalSwitch change channel to ");

                        this.WriteString(":ROUT:CLOS " + MyInf.InputSlot + "," + MyInf.DeviceChannel
                            + ",1," + Ch);

                        this.WriteString(":ROUT:CLOS " + MyInf.OutputSlot + "," + MyInf.DeviceChannel
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
                            bool flag1 = this.WriteString(":ROUT:CLOS " + MyInf.InputSlot + "," + MyInf.DeviceChannel
                                 + ",1," + Ch);
                            bool flag2 = this.WriteString(":ROUT:CLOS " + MyInf.OutputSlot + "," + MyInf.DeviceChannel
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
                                    this.WriteString(":ROUT:CLOS? " + MyInf.InputSlot + "," +
                                          MyInf.DeviceChannel + ",1");


                                    readtemp = this.ReadString().Replace("\n", "").Trim();


                                    if (Convert.ToByte(readtemp) == Convert.ToByte(Ch))
                                    {
                                        flagInput = true;
                                        break;
                                    }
                                }
                                if (!flagOutput)
                                {

                                    Thread.Sleep(100);
                                    this.WriteString(":ROUT:CLOS? " + MyInf.OutputSlot + "," +
                                          MyInf.DeviceChannel + ",1");


                                    readtemp = this.ReadString().Replace("\n", "").Trim();


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
                                Log.SaveLogToTxt("OpticalSwitch change channel to " + Ch);

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

