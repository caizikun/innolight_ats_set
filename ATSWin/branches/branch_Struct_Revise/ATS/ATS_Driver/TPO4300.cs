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

    public class TPO4300 : Thermocontroller
    {
        private static object syncRoot = new object();//used for thread synchronization

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
                            this.EquipmentConnectflag = content.Contains("TEMPTRONIC");
                        }

                       // EquipmentConnectflag = true; 
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
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }
        public override bool Initialize(TestModeEquipmentParameters[] TPO4300Struct)
        {
            lock (syncRoot)
            {
                try
                {
                    int i = 0;
                    if (Algorithm.FindFileName(TPO4300Struct, "ADDR", out i))
                    {
                        Addr = Convert.ToByte(TPO4300Struct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ADDR");
                        return false;
                    }
                    if (Algorithm.FindFileName(TPO4300Struct, "IOTYPE", out i))
                    {
                        IOType = TPO4300Struct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }
                    if (Algorithm.FindFileName(TPO4300Struct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(TPO4300Struct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }
                    if (Algorithm.FindFileName(TPO4300Struct, "FLSE", out i))
                    {
                        StrAirFlow = TPO4300Struct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no FLSE");
                        return false;
                    }
                    if (Algorithm.FindFileName(TPO4300Struct, "ULIM", out i))
                    {
                        ULIM = TPO4300Struct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ULIM");
                        return false;
                    }
                    if (Algorithm.FindFileName(TPO4300Struct, "LLIM", out i))
                    {
                        LLIM = TPO4300Struct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no LLIM");
                        return false;
                    }
                    if (Algorithm.FindFileName(TPO4300Struct, "SENSOR", out i))
                    {
                        Sensor = TPO4300Struct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no SENSOR");
                        return false;
                    }
                    if (Algorithm.FindFileName(TPO4300Struct, "NAME", out i))
                    {
                        Name = TPO4300Struct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
                        return false;
                    }
                    if (!Connect()) return false;
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
                if (EquipmentConfigflag)//曾经设定过
                {

                    return true;

                }
                else
                {
                    if (Reset == true)
                    {
                        ReSet();
                    }
                    this.WriteString("LGIN X-Stream");
                    Thread.Sleep(500);
                    this.WriteString("DTYP 1");
                    this.WriteString("DUTC 70");
                    AirFlowSetting();
                    AirTempsUpperlimit(syn);
                    AirTempslowerlimit(syn);
                    SensorType(syn);//no sensor 时，DUTControlModeOnOFF打不开，返回为0
                    this.WriteString("WNDW 0.5");
                    this.WriteString("TRKL 1");
                    DUTControlModeOnOFF(1, syn);//nosensor 返回为0
                    EquipmentConfigflag = true;
                }
                return true;
            }
        }

        public override bool DUTControlModeOnOFF(byte Switch, int syn = 0)//1 ON,0 OFF
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
                        Log.SaveLogToTxt("DUTControlModeOnOFF is" + Switch.ToString());
                        return this.WriteString("DUTM " + Switch.ToString());

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("DUTM " + Switch.ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("DUTM?");
                                readtemp = this.ReadString();
                                if (readtemp == Switch.ToString() + "\r\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DUTControlModeOnOFF is" + Switch.ToString());

                                flag = true;
                            }
                            else
                            {

                                Log.SaveLogToTxt("set DUTControlModeOnOFF wrong");
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
        public override string ReadCurrentTemp()
        {
            lock (syncRoot)
            {
                string Str_Temp = "0";
                try
                {
                    this.WriteString("TEMP?");
                    Str_Temp = this.ReadString(32);

                }
                catch
                {
                    Str_Temp = "10000";
                }
                return Str_Temp;
            }
        }

        public override bool AirFlowSetting()
        {
            lock (syncRoot)
            {
                bool flag = false;
                try
                {
                    flag = this.WriteString("FLSE " + StrAirFlow);
                    Log.SaveLogToTxt("AirFlowSet is" + StrAirFlow);
                    return flag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }

        public override bool AirTempsUpperlimit(int syn = 0)
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
                        Log.SaveLogToTxt("TempsUpperlimit is" + ULIM);
                        return this.WriteString("ULIM " + ULIM);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("ULIM " + ULIM);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("ULIM?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(ULIM))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("TempsUpperlimit is" + ULIM);

                                flag = true;
                            }
                            else
                            {

                                Log.SaveLogToTxt("set TempsUpperlimit wrong");
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

        public override bool AirTempslowerlimit(int syn = 0)
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
                        Log.SaveLogToTxt("Tempslowerlimit is" + LLIM);
                        return this.WriteString("LLIM " + LLIM);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("LLIM " + LLIM);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("LLIM?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(LLIM))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("Tempslowerlimit is" + LLIM);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set Tempslowerlimit wrong");


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

        public override bool SensorType(int syn = 0)//0 No Sensor,1 T,2 k,3 rtd,4 diode 
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
                        Log.SaveLogToTxt("Sensor is" + Sensor);
                        return this.WriteString("DSNS " + Sensor);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("DSNS " + Sensor);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("DSNS?");
                                readtemp = this.ReadString();
                                if (readtemp == Sensor + "\r\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("Sensor is" + Sensor);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set Sensor wrong");
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

        public override bool lockHeadPosition(byte Switch)//1 UP,0 DOWN
        {
            lock (syncRoot)
            {
                try
                {
                    return this.WriteString("HDLK " + Switch.ToString());

                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }

        public override string ReadSetpoint()
        {
            lock (syncRoot)
            {
                string Str_Read = "";
                try
                {
                    this.WriteString("SETN?");
                    Str_Read = this.ReadString();
                    return Str_Read;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return Str_Read;
                }
            }
        }

        public override string ReadSetpointTemp()
        {
            lock (syncRoot)
            {
                string Str_Read = "";
                try
                {
                    this.WriteString("SETP?");
                    Str_Read = this.ReadString();
                    return Str_Read;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return Str_Read;
                }
            }
        }

        public override bool SetPositionUPDown(string position, int syn = 0)//0 UP,1 DOWN
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
                        Log.SaveLogToTxt("position is" + position);
                        return this.WriteString("HEAD " + position);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("HEAD " + position);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("HEAD?");
                                readtemp = this.ReadString();
                                if (readtemp == position + "\r\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("position is" + position);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set position wrong");


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

        public override bool SetPoint(string Point, int syn = 0)//0 HOT,1 AMB,2 code
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
                        Log.SaveLogToTxt("point is " + Point);
                        return this.WriteString("SETN " + Point);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("SETN " + Point);
                            this.WriteString("SOAK 15");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("SETN?");
                                readtemp = this.ReadString();
                                if (readtemp == Point + "\r\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("point is " + Point);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set point wrong");

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
        public override bool SetPointTemp(double Temp, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";

                try
                {
                    string Str = string.Format("{0:N1}", Temp);
                    SetPositionUPDown("1");
                    if (Temp < 20)// Low
                    {
                        SetPoint("2");

                    }
                    else if (Temp > 30)//Hig
                    {
                        SetPoint("0");
                    }
                    else//AMB
                    {
                        SetPoint("1");
                    }


                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("Temp is " + Temp);
                        return this.WriteString("SETP " + Str);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("SETP " + Str);

                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("SETP?");
                                readtemp = this.ReadString();
                                if (readtemp == Str + "\r\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("Temp is " + Temp);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set Temp wrong");


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
    }

   
}
