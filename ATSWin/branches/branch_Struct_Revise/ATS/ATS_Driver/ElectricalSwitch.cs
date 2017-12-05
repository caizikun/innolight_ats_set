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
        private static object syncRoot = new object();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] SwitchStruct)
        {
            try
            {
               
                int i = 0;
                if (Algorithm.FindFileName(SwitchStruct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(SwitchStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(SwitchStruct, "IOTYPE", out i))
                {
                    IOType = SwitchStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(SwitchStruct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(SwitchStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(SwitchStruct, "ELECSWITCHCHANNEL", out i))
                {
                    ElecSwitchChannel = SwitchStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no ELECSWITCHCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(SwitchStruct, "NAME", out i))
                {
                    Name = SwitchStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }
               

                if (!Connect()) return false;
            }
            catch (Error_Message error)
            {

                Log.SaveLogToTxt(error.ToString());
                return false;
            }
            return true;
        }
        //IOPort EEPROM;
        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":                        
                        EquipmentConnectflag = true;
                        break;
                        
                    case "USB":
                        
                        EquipmentConnectflag = true;
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
            return true;
            //return IOPort.resetdevice(Addr);
        }
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                return ChangeElecSwitchChannel(channel);
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
                        ChangeElecSwitchChannel();
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

        protected bool ChangeElecSwitchChannel(string channel)
        {
            lock (syncRoot)
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
                    flag = IOPort.WritePort(0, Addr, index, 0xFF);
                    Log.SaveLogToTxt("ElecSwitchChannel is" + channel);
                    return flag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }

        }
        public override bool ChangeElecSwitchChannel()
        {
            lock (syncRoot)
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
                    flag = IOPort.WritePort(0, Addr, index, 0xFF);
                    Log.SaveLogToTxt("ElecSwitchChannel is" + ElecSwitchChannel);
                    return flag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }

            }
        }

    }
}
