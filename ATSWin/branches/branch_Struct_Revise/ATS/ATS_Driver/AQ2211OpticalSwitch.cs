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
        private static object syncRoot = SyncRoot_AQ2211.Get_SyncRoot_AQ2211();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] AQ2211OpticalSwitchstruct)
        {
            try
            {
                int i=0;
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct,"ADDR",out i))
                {
                    Addr = Convert.ToByte(AQ2211OpticalSwitchstruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct,"IOTYPE",out i))
                {
                    IOType = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(AQ2211OpticalSwitchstruct[i].DefaultValue);
                    
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct,"OPTICALSWITCHSLOT",out i))
                {
                    OpticalSwitchSlot = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTICALSWITCHSLOT");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct,"SWITCHCHANNEL",out i))
                {
                    SwitchChannel = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no SWITCHCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct,"TOCHANNEL",out i))
                {
                    ToChannel = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no TOCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct,"NAME",out i))
                {
                    Name = AQ2211OpticalSwitchstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }

                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct, "BidiTxChannel", out i))
                {

                    BidiTx_Channel = AQ2211OpticalSwitchstruct[i].DefaultValue.Split(',');
                }
                else
                {
                    Log.SaveLogToTxt("there is no BidiTxChannel");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211OpticalSwitchstruct, "BidiRxChannel", out i))
                {

                    BidiRx_Channel = AQ2211OpticalSwitchstruct[i].DefaultValue.Split(',');
                }
                else
                {
                    Log.SaveLogToTxt("there is no BidiRxChannel");
                    return false;
                }

                if (!Connect()) return false;
            }
            catch (InnoExCeption error)
            {
                throw error;
            }

            catch (Exception error)
            {

                Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Los_parameter_0x05102 + "Reason=" + error.TargetSite.Name + "Fail");
                throw new InnoExCeption(ExceptionDictionary.Code._Los_parameter_0x05102, error.StackTrace);
                // throw new InnoExCeption(ex);
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

                        lock (syncRoot)
                        {
                            this.WriteString("*IDN?");
                            string content = this.ReadString();
                            this.EquipmentConnectflag = content.Contains("AQ22");
                        }
                        break;
                    default:
                        Log.SaveLogToTxt("GPIB类型错误");
                        EquipmentConnectflag = false;
                        break;
                }
                if (!EquipmentConnectflag)
                {
                    Log.SaveLogToTxt(ExceptionDictionary.Code._UnConnect_0x05000 + "无法连接仪器");
                    EquipmentConnectflag = false;
                    throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);
                }
                return EquipmentConnectflag;
               
            }
           
                catch (Exception error)
            {
                Log.SaveLogToTxt("ErrorCode="+  ExceptionDictionary.Code._Funtion_Fatal_0x05002 +"Reason=" +error.TargetSite.Name + "Fail");
                EquipmentConnectflag = false;
                throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);

              //  return false;
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
        public override bool ChangeChannel(string channel, int syn = 0) 
        {
            lock (syncRoot)
            {
                return Switchchannel(channel, syn);
            }
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }
        public override bool Configure(int syn = 0)
        {
            lock (syncRoot)
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
                        Switchchannel(syn);
                        EquipmentConfigflag = true;
                    }
                    return true;

                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
            }
        }
        protected bool Switchchannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("OpticalSwitch change channel to" + channel);
                        return this.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + channel);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + channel);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":route" + OpticalSwitchSlot + "?");
                                readtemp = this.ReadString();
                                if (readtemp == " A," + channel)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("OpticalSwitch change channel to" + channel);
                                flag = true;
                            }
                            else
                            {

                                Log.SaveLogToTxt("OpticalSwitch switchchannel wrong");
                            }

                        }

                        return flag;
                    }
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
            }
        }
        public override bool Switchchannel(int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("OpticalSwitch change channel to" + ToChannel);
                        return this.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + ToChannel);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":route" + OpticalSwitchSlot + ":chan" + SwitchChannel + " A," + ToChannel);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":route" + OpticalSwitchSlot + "?");
                                readtemp = this.ReadString();
                                if (readtemp == "A," + ToChannel)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("OpticalSwitch change channel to" + ToChannel);

                                flag = true;
                            }
                            else
                                Log.SaveLogToTxt("OpticalSwitch change channel wrong");

                        }

                        return flag;
                    }
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
            }
        }

        public override bool CheckEquipmentRole(byte TestModelType, byte Channel)
        {//// 0=NA,1=TX,2=RX
            lock (syncRoot)
            {
                string StrChannel = "1";
                try
                {


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
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
            }
           
        }
    }
}
