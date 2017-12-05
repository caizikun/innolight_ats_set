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
    public class MAP200PowerMeter : PowerMeter
    {
        public string DeviceChannel = "1";
        private static object syncRoot = SyncRoot_MAP200.Get_SyncRoot_MAP200();//used for thread synchronization

        #region 公共方法
        public override bool Initialize(TestModeEquipmentParameters[] MAP200PowerMeterStruct)
        {
            lock (syncRoot)
            {
                try
                {
                    int i = 0;

                    if (Algorithm.FindFileName(MAP200PowerMeterStruct, "ADDR", out i))
                    {
                        Addr = Convert.ToByte(MAP200PowerMeterStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ADDR");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200PowerMeterStruct, "IOTYPE", out i))
                    {
                        IOType = MAP200PowerMeterStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200PowerMeterStruct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(MAP200PowerMeterStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200PowerMeterStruct, "SLOT", out i))
                    {
                        PowerMeterSlot = MAP200PowerMeterStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no POWERMETERSLOT");
                        return false;
                    }

                    //if (Algorithm.FindFileName(MAP200PowerMeterStruct, "DEVICECHANNEL", out i))
                    //{
                    //    DeviceChannel = MAP200PowerMeterStruct[i].DefaultValue;
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no DEVICECHANNEL");
                    //    return false;
                    //}

                    if (Algorithm.FindFileName(MAP200PowerMeterStruct, "WAVELENGTH", out i))
                    {
                        PowerMeterWavelength = MAP200PowerMeterStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no POWERMETERWAVELENGTH");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200PowerMeterStruct, "UNITTYPE", out i))
                    {
                        UnitType = MAP200PowerMeterStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no UNITTYPE");
                        return false;
                    }

                    if (Algorithm.FindFileName(MAP200PowerMeterStruct, "NAME", out i))
                    {
                        Name = MAP200PowerMeterStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
                        return false;
                    }

                    if (!Connect())
                    {
                        return false;
                    }
                }
                catch (Error_Message error)
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

                        ConfigLegacyModeOff();

                        ConfigWavelength(syn);
                        Selectunit(syn);
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
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }
        public override bool ConfigWavelength(int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;

                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PowerMeterSlot is " + PowerMeterSlot +
                            ", DeviceChannel is " + DeviceChannel + ", Wavelength is " + PowerMeterWavelength + "nm");
                        flag = this.WriteString(":SENS:POW:WAV " + PowerMeterSlot + "," + DeviceChannel + "," + PowerMeterWavelength);
                        return flag;
                    }
                    else
                    {
                        bool tempFlag = false;
                        string readtemp = "";
                        int k = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            tempFlag = this.WriteString(":SENS:POW:WAV " + PowerMeterSlot + "," + DeviceChannel + "," + PowerMeterWavelength);

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
                                this.WriteString(":SENS:POW:WAV? " + PowerMeterSlot + "," + DeviceChannel);

                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(PowerMeterWavelength))
                                {
                                    break;
                                }
                            }

                            if (k <= 2)
                            {
                                Log.SaveLogToTxt("PowerMeterSlot is " + PowerMeterSlot +
                                    ", DeviceChannel is " + DeviceChannel + ", Wavelength is " + PowerMeterWavelength + "nm");
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("PowerMeter ConfigWavelength wrong");
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
        public override double ReadPower()
        {
            lock (syncRoot)
            {
                double power = 0;

                try
                {
                    this.WriteString(":FETch:POWer? " + PowerMeterSlot + "," + DeviceChannel);
                    power = Convert.ToDouble(this.ReadString(32));
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());

                    return power;
                }
                return power;
            }
        }
        public override bool Selectunit(int syn = 0) // 0 "dBm", 1 "W"  
        {
            lock (syncRoot)
            {
                bool flag = false;
                string strUnitType = "";

                switch (UnitType)
                {
                    case "0":
                        strUnitType = "dBm";
                        break;
                    case "1":
                        strUnitType = "W";
                        break;
                    default:
                        strUnitType = "dBm";
                        break;
                }

                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PowerMeterSlot is " + PowerMeterSlot +
                            ", DeviceChannel is " + DeviceChannel + ", UnitType is " + strUnitType);
                        return this.WriteString(":SENSe:POWer:UNIT " + PowerMeterSlot + "," + DeviceChannel + "," + UnitType);
                    }
                    else
                    {
                        bool tempFlag = false;
                        string readtemp = "";
                        int k = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            tempFlag = this.WriteString(":SENSe:POWer:UNIT " + PowerMeterSlot + "," + DeviceChannel + "," + UnitType);

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
                                this.WriteString(":SENSe:POWer:UNIT? " + PowerMeterSlot + "," + DeviceChannel);

                                readtemp = this.ReadString();
                                if (readtemp.Trim().ToUpper() == UnitType.Trim().ToUpper())
                                {
                                    break;
                                }
                            }

                            if (k <= 2)
                            {
                                Log.SaveLogToTxt("PowerMeterSlot is " + PowerMeterSlot +
                                    ", DeviceChannel is " + DeviceChannel + ", UnitType is " + strUnitType);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("PowerMeter Select UnitType wrong");
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
    }
}