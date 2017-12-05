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
    public class AQ2211PowerMeter : PowerMeter
    {
        private static object syncRoot = SyncRoot_AQ2211.Get_SyncRoot_AQ2211();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] AQ2211PowerMeterstruct)
        {
            try
            {
                int i = 0;
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"ADDR",out i))
                {
                    Addr = Convert.ToByte(AQ2211PowerMeterstruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"IOTYPE",out i))
                {
                    IOType = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(AQ2211PowerMeterstruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERSLOT",out i))
                {
                    PowerMeterSlot = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no POWERMETERSLOT");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERWAVELENGTH",out i))
                {
                    PowerMeterWavelength = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no POWERMETERWAVELENGTH");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"POWERMETERCHANNEL",out i))
                {
                    PowerMeterChannel =AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no POWERMETERCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"UNITTYPE",out i))
                {
                    UnitType = AQ2211PowerMeterstruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no UNITTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211PowerMeterstruct,"NAME",out i))
                {
                    Name = AQ2211PowerMeterstruct[i].DefaultValue;
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

                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                Log.SaveLogToTxt(error.ToString());
                EquipmentConnectflag = false;
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
                        ConfigWavelength(syn);
                        Selectunit(syn);
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
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                double waveinput;
                double waveoutput;
                try
                {
                    //flag = this.WriteString(":sense" + AttSlot + ":channel " + AttChannel + ":power:wavelength" + wavtemp[i] + "E-9");
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PowerMeterSlot is" + PowerMeterSlot + "Wavelength is" + PowerMeterWavelength + "nm");
                        return this.WriteString(":INP" + PowerMeterSlot + ":WAV " + PowerMeterWavelength + "nm");
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            flag1 = this.WriteString(":INP" + PowerMeterSlot + ":WAV " + PowerMeterWavelength + "nm");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INP" + PowerMeterSlot + ":WAV?");
                                readtemp = this.ReadString();
                                waveinput = Convert.ToDouble(PowerMeterWavelength);
                                waveoutput = Convert.ToDouble(readtemp) * Math.Pow(10, 9);
                                if (waveinput == waveoutput)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PowerMeterSlot is" + PowerMeterSlot + "Wavelength is" + PowerMeterWavelength + "nm");

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
        public override double ReadPower()
        {
            lock (syncRoot)
            {
                double power = 0;
                try
                {
                    this.WriteString(":fetch%d:channel%d:power?" + PowerMeterSlot + PowerMeterChannel + ":ATT?");
                    power = Convert.ToDouble(this.ReadString(32));
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
                return power;
            }
        }
        public override bool Selectunit(int syn = 0)//0 "dBm",1 "W"
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
                        Log.SaveLogToTxt("PowerMeterSlot is" + PowerMeterSlot + "Channel is" + PowerMeterChannel + "UnitType is" + UnitType);
                        return this.WriteString("SENS" + PowerMeterSlot + ":CHAN" + PowerMeterChannel + ":POW:UNIT " + UnitType);
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            flag1 = this.WriteString("SENS" + PowerMeterSlot + ":CHAN" + PowerMeterChannel + ":POW:UNIT " + UnitType);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("SENS" + PowerMeterSlot + ":CHAN" + PowerMeterChannel + ":POW:UNIT?");
                                readtemp = this.ReadString();
                                if (readtemp == UnitType)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PowerMeterSlot is" + PowerMeterSlot + "Channel is" + PowerMeterChannel + "UnitType is" + UnitType);
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
