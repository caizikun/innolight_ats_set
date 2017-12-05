using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using System.Reflection;
using ATS_Framework;
using System.Windows.Forms;
namespace ATS_Driver
{
  
 
    public class E3631: Powersupply
    {
        private static object syncRoot = new object();//used for thread synchronization

        public override bool Initialize(TestModeEquipmentParameters[] PSStruct)
        {
            try
            {
               
                int i = 0;
                if (Algorithm.FindFileName(PSStruct,"ADDR",out i))
                {
                    Addr = Convert.ToByte(PSStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"IOTYPE",out i))
                {
                    IOType = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(PSStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"DUTCHANNEL",out i))
                {
                    DutChannel = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DUTCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"OPTSOURCECHANNEL",out i))
                {
                    OptSourceChannel = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTSOURCECHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"DUTVOLTAGE",out i))
                {
                    DutVoltage = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DUTVOLTAGE");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"DUTCURRENT",out i))
                {
                    DutCurrent = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DUTCURRENT");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"OPTVOLTAGE",out i))
                {
                    OptVoltage = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTVOLTAGE");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"OPTCURRENT",out i))
                {
                    OptCurrent = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTCURRENT");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct,"NAME",out i))
                {
                    Name = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct, "VOLTAGEOFFSET", out i))
                {
                    //voltageoffset = PSStruct[i].DefaultValue;
                    voltageoffset = "0";
                }
                else
                {
                    Log.SaveLogToTxt("there is no VOLTAGEOFFSET");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct, "CURRENTOFFSET", out i))
                {
                    currentoffset = PSStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no CURRENTOFFSET");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct, "OPENDELAY", out i))
                {
                    opendelay = Convert.ToInt32(PSStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPENDELAY");
                    return false;
                }
                if (Algorithm.FindFileName(PSStruct, "CLOSEDELAY", out i))
                {
                    closedelay = Convert.ToInt32(PSStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no CLOSEDELAY");
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
                            this.EquipmentConnectflag = content.Contains("E3631");



                            if (!EquipmentConnectflag)
                            {
                                Log.SaveLogToTxt(ExceptionDictionary.Code._UnControl_0x05001 + "无法连接仪器");
                                EquipmentConnectflag = false;
                                throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);
                            }
                        }
                        break;
                    default:
                        Log.SaveLogToTxt("GPIB类型错误");
                        break;
                }
                 return EquipmentConnectflag;
            }
            catch (InnoExCeption error)
            {
                throw error;
              //  throw error;
            }
            catch (Exception error)
            {
                Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._UnConnect_0x05000 + "Reason=" + error.TargetSite.Name + "Fail");
                EquipmentConnectflag = false;
               // throw new InnoExCeption(EquipmentErrorCode.LostOfControl, "无法连接仪器");
                throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);
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
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            lock (syncRoot)
            {
                voltageoffset = offset;
                return true;
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
                        if ((Convert.ToInt16(DutChannel) == 1) || (Convert.ToInt16(DutChannel) == 2))
                        {
                            ConfigVoltageCurrent(DutChannel, DutVoltage, DutCurrent);
                        }
                        if ((Convert.ToInt16(OptSourceChannel) == 1) || (Convert.ToInt16(OptSourceChannel) == 2))
                        {
                            ConfigVoltageCurrent(OptSourceChannel, OptVoltage, OptCurrent);
                        }

                        OutPutSwitch(true, syn);
                        EquipmentConfigflag = true;
                    }
                    return true;

                }
                catch (InnoExCeption error)
                {
                    throw error;

                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002);
                    // throw new InnoExCeption(ex);
                }
            }
        }
       // OutPutSwitch
        public override bool OutPutSwitch(bool Switch, int syn = 0)
        {
            lock (syncRoot)
            {
                // "ON "" OFF"
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string Switch_in;
                int delay = 0;
                string intswitch;
                if (Switch)
                {
                    Switch_in = "ON";
                    intswitch = "1";
                    delay = opendelay;
                }
                else
                {
                    Switch_in = "OFF";
                    intswitch = "0";
                    delay = closedelay;
                }
                try
                {

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("E3631 Switch is" + Switch_in);
                        flag = this.WriteString("OUTP " + Switch_in);
                        Thread.Sleep(delay);
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("OUTP " + Switch_in);

                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("OUTP?");
                                readtemp = this.ReadString();
                                if (readtemp == intswitch + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("E3631 Switch is" + Switch_in);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("E3631 Switch wrong");

                            }

                        }
                        Thread.Sleep(delay);
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
        protected  bool ConfigVoltageCurrent(string channel, string voltage, string current)
        {
            lock (syncRoot)
            {
                double vol = Convert.ToDouble(voltage);
                double offset = Convert.ToDouble(voltageoffset);
                string volandoffset = (vol + offset).ToString();

                bool flag = false;
                string Str_Write = "";//= "APPL P6V," + str_V + "," + Str_I;
                string str_channel = "";
                if (Convert.ToInt16(channel) == 1)
                {
                    Str_Write = "APPL P6V," + volandoffset + "," + current;
                    str_channel = "APPL P6V";
                }
                else if (Convert.ToInt16(channel) == 2)
                {
                    Str_Write = "APPL P25V," + volandoffset + "," + current;
                    str_channel = "APPL P25V";
                }

                try
                {
                    flag = this.WriteString(Str_Write);
                    Log.SaveLogToTxt("E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current);
                    return flag;

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
        protected bool ConfigVoltageCurrent(string channel, string voltage, string current, int syn = 0)
        {
            lock (syncRoot)
            {
                double vol = Convert.ToDouble(voltage);
                double offset = Convert.ToDouble(voltageoffset);
                string volandoffset = (vol + offset).ToString();

                bool flag = false;
                string Str_Write = "";//= "APPL P6V," + str_V + "," + Str_I;
                string str_channel = "";
                if (Convert.ToInt16(channel) == 1)
                {
                    Str_Write = "APPL P6V," + volandoffset + "," + current;
                    str_channel = "APPL P6V";
                }
                else if (Convert.ToInt16(channel) == 2)
                {
                    Str_Write = "APPL P25V," + volandoffset + "," + current;
                    str_channel = "APPL P25V";
                }

                try
                {
                    if (syn == 0)
                    {

                        flag = this.WriteString(Str_Write);
                        string Str = GetVoltage().ToString();
                        Log.SaveLogToTxt("E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current);
                        return flag;
                    }
                    else
                    {
                        flag = false;

                        for (int i = 0; i < 3; i++)
                        {

                            flag = this.WriteString(Str_Write);
                            Thread.Sleep(500);
                            this.WriteString(str_channel + "?");
                            string StrVoltage = this.ReadString();
                            double SetVoltage = vol + offset;
                            double MeasurtVoltage = Convert.ToDouble(StrVoltage);
                            if (MeasurtVoltage <= SetVoltage * 1.005 && MeasurtVoltage >= SetVoltage * 0.995)
                            {
                                flag = true;
                                Log.SaveLogToTxt("E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current);

                                break;
                            }
                        }
                        if (!flag)
                        {

                            Log.SaveLogToTxt("E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current + "ERROR");
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

        public override bool ConfigVoltageCurrent(string voltage,int syn = 0)
        {
            lock (syncRoot)
            {
                double vol = Convert.ToDouble(voltage);
                double offset = Convert.ToDouble(voltageoffset);
                double volandoffset = vol + offset;
                bool flag = false;
                string Str_Write = "";//= "APPL P6V," + str_V + "," + Str_I;
                string str_channel = "";
                if (Convert.ToInt16(DutChannel) == 1)
                {
                    Str_Write = "APPL P6V," + volandoffset + "," + DutCurrent;
                    str_channel = "APPL P6V";
                }
                else if (Convert.ToInt16(DutChannel) == 2)
                {
                    Str_Write = "APPL P25V," + volandoffset + "," + DutCurrent;
                    str_channel = "APPL P25V";
                }
                syn = 0;
                try
                {
                    if (syn == 0)
                    {

                        this.WriteString(Str_Write);
                        this.WriteString("*opc?");
                        string StrTemp = this.ReadString().Replace("\n", "");
                        if (StrTemp == "1")
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }

                        // string Str = GetVoltage().ToString();
                        Log.SaveLogToTxt("E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + DutCurrent);
                        return flag;
                    }
                    else// 因为读取当前的电压值 会导致仪器脱离程控, 不适用同步模式
                    {
                        flag = false;

                        for (int i = 0; i < 3; i++)
                        {

                            flag = this.WriteString(Str_Write);
                            Thread.Sleep(500);
                            // this.WriteString(str_channel + "?");
                            string StrVoltage = GetVoltage().ToString();
                            // double SetVoltage = vol + offset;
                            double MeasurtVoltage = Convert.ToDouble(StrVoltage);
                            if (MeasurtVoltage <= volandoffset * 1.005 && MeasurtVoltage >= volandoffset * 0.995)
                            {
                                flag = true;
                                Log.SaveLogToTxt("E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + DutCurrent);

                                break;
                            }
                        }
                        if (!flag)
                        {

                            Log.SaveLogToTxt("E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + DutCurrent + "ERROR");
                        }
                        return flag;
                    }
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                    throw error;
                    //  throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt(ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
            }
        }
        public override double GetCurrent()
        {
            lock (syncRoot)
            {
                double current = 0;
                try
                {

                    this.WriteString("MEAS:CURR?");
                    current = (Convert.ToDouble((this.ReadString(25)))) * 1000;
                    current = current - Convert.ToDouble(currentoffset);
                    // 因为读取当前的电压值(电流)，会导致仪器脱离程控(一直在执行指令), 所以加延时等仪器恢复。                    
                    Thread.Sleep(10000);
                    return current;

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
        public override double GetVoltage()
        {
            lock (syncRoot)
            {
                double Voltage = 0;
                try
                {

                    this.WriteString("MEAS:VOLT? P6V");
                    // this.WriteString("MEAS:VOLT?");
                    Voltage = Convert.ToDouble((this.ReadString(10)));
                    // Voltage = Voltage - Convert.ToDouble(voltageoffset);
                    // 因为读取当前的电压值(电流)，会导致仪器脱离程控(一直在执行指令), 所以加延时等仪器恢复。                    
                    Thread.Sleep(10000);
                    return Voltage;

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
