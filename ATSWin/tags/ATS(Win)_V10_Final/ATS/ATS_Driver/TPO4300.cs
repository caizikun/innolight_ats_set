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
        public Algorithm algorithm = new Algorithm();
        public TPO4300(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":

                        MyIO = new IOPort(IOType, "GPIB::" + Addr, logger);
                        MyIO.IOConnect();
                        EquipmentConnectflag = true; 
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
            try
            {
            int i = 0;
            if (algorithm.FindFileName(TPO4300Struct,"ADDR",out i))
            {
                Addr = TPO4300Struct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no ADDR");
                return false;
            }
            if (algorithm.FindFileName(TPO4300Struct,"IOTYPE",out i))
            {
                IOType = TPO4300Struct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }
            if (algorithm.FindFileName(TPO4300Struct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(TPO4300Struct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }
            if (algorithm.FindFileName(TPO4300Struct,"FLSE",out i))
            {
                FLSE = TPO4300Struct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no FLSE");
                return false;
            }
            if (algorithm.FindFileName(TPO4300Struct,"ULIM",out i))
            {
                ULIM = TPO4300Struct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no ULIM");
                return false;
            }
            if (algorithm.FindFileName(TPO4300Struct,"LLIM",out i))
            {
                LLIM = TPO4300Struct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LLIM");
                return false;
            }
            if (algorithm.FindFileName(TPO4300Struct,"SENSOR",out i))
            {
                Sensor = TPO4300Struct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no SENSOR");
                return false;
            }
            if (algorithm.FindFileName(TPO4300Struct,"NAME",out i))
            {
                Name = TPO4300Struct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }
            if (!Connect()) return false;
        }
            catch (Error_Message error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
            return true;
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
                MyIO.WriteString("LGIN X-Stream");
                Thread.Sleep(500);
                MyIO.WriteString("DTYP 1");   
                MyIO.WriteString("DUTC 70");
                AirFlowSetting();
                AirTempsUpperlimit(syn);
                AirTempslowerlimit(syn);
                SensorType(syn);//no sensor 时，DUTControlModeOnOFF打不开，返回为0
                MyIO.WriteString("WNDW 0.5");
                MyIO.WriteString("TRKL 1");
                DUTControlModeOnOFF(1, syn);//nosensor 返回为0
                EquipmentConfigflag = true;
            }
            return true;
        }

        public override bool DUTControlModeOnOFF(byte Switch, int syn = 0)//1 ON,0 OFF
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";

            try
            {

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "DUTControlModeOnOFF is" + Switch.ToString());
                    return MyIO.WriteString("DUTM " + Switch.ToString());

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("DUTM " + Switch.ToString());
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("DUTM?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == Switch.ToString() + "\r\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "DUTControlModeOnOFF is" + Switch.ToString());

                            flag = true;
                        }
                        else
                        {

                            logger.AdapterLogString(3, "set DUTControlModeOnOFF wrong");
                        }

                    }
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override string ReadCurrentTemp()
        {
            string Str_Temp = "0";
            try
            {
                MyIO.WriteString("TEMP?");
                Str_Temp = MyIO.ReadString(32);

            }
            catch
            {
                Str_Temp = "10000";
            }
            return Str_Temp;
        }

        public override bool AirFlowSetting()
        {
            bool flag = false;
            try
            {
                flag= MyIO.WriteString("FLSE " + FLSE);
                logger.AdapterLogString(0, "AirFlowSet is" + FLSE);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool AirTempsUpperlimit(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "TempsUpperlimit is" + ULIM);
                    return MyIO.WriteString("ULIM " + ULIM);

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("ULIM " + ULIM);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("ULIM?");
                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble(ULIM))
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "TempsUpperlimit is" + ULIM);

                            flag = true;
                        }
                        else
                        {

                            logger.AdapterLogString(3, "set TempsUpperlimit wrong");
                        }

                    }


                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool AirTempslowerlimit(int syn = 0)
        {
           
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Tempslowerlimit is" + LLIM);
                    return MyIO.WriteString("LLIM " + LLIM);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("LLIM " + LLIM);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("LLIM?");
                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble(LLIM))
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "Tempslowerlimit is" + LLIM);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set Tempslowerlimit wrong");


                        }

                    }


                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool SensorType(int syn = 0)//0 No Sensor,1 T,2 k,3 rtd,4 diode 
        {
           
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sensor is" + Sensor);
                    return MyIO.WriteString("DSNS " + Sensor);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("DSNS " + Sensor);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("DSNS?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == Sensor + "\r\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "Sensor is" + Sensor);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set Sensor wrong");
                        }

                    }


                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool lockHeadPosition(byte Switch)//1 UP,0 DOWN
        {
            
            try
            {
              return  MyIO.WriteString("HDLK " + Switch.ToString());
                
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override string ReadSetpoint()
        {
            string Str_Read = "";
            try
            {
                MyIO.WriteString("SETN?");
                Str_Read = MyIO.ReadString();
                return Str_Read;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return Str_Read;
            }
        }

        public override string ReadSetpointTemp()
        {
            string Str_Read = "";
            try
            {
                MyIO.WriteString("SETP?");
                Str_Read = MyIO.ReadString();
                return Str_Read;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return Str_Read;
            }
        }

        public override bool SetPositionUPDown(string position, int syn = 0)//0 UP,1 DOWN
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "position is" + position);
                    return MyIO.WriteString("HEAD " + position);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("HEAD " + position);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("HEAD?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == position + "\r\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "position is" + position);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set position wrong");


                        }

                    }
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }

        public override bool SetPoint(string Point, int syn = 0)//0 HOT,1 AMB,2 code
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "point is " + Point);
                    return MyIO.WriteString("SETN " + Point);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("SETN " + Point);
                        MyIO.WriteString("SOAK 15");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("SETN?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == Point + "\r\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "point is " + Point);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set point wrong");

                        }

                    }
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool SetPointTemp(double Temp, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                string Str = string.Format("{0:N1}", Temp);

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Temp is " + Temp);
                    return MyIO.WriteString("SETP " + Str);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("SETP " + Str);

                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("SETP?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == Str + "\r\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "Temp is " + Temp);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set Temp wrong");


                        }

                    }
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
    }

   
}
