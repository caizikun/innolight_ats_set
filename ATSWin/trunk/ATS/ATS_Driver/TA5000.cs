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
    public class TA5000 : Thermocontroller
    {
        public Algorithm algorithm = new Algorithm();
        public TA5000(logManager logmanager)
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

                        MyIO = new IOPort(IOType, "GPIB0::" + Addr+"::INSTR", logger);
                        MyIO.IOConnect();
                        MyIO.WriteString("*IDN?");
                        EquipmentConnectflag = MyIO.ReadString().Contains("TA5000");

                       // EquipmentConnectflag = true; 
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
                EquipmentConnectflag = false;
                return false;
            }
        }
        public override bool Initialize(TestModeEquipmentParameters[] TA5000Struct)
        {
            try
            {
                int i = 0;
                if (algorithm.FindFileName(TA5000Struct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(TA5000Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(TA5000Struct, "IOTYPE", out i))
                {
                    IOType = TA5000Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }
                if (algorithm.FindFileName(TA5000Struct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(TA5000Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }
                if (algorithm.FindFileName(TA5000Struct, "AIRFLOW", out i))
                {
                    StrAirFlow = TA5000Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no AIRFLOW");
                    return false;
                }
                if (algorithm.FindFileName(TA5000Struct, "ULIM", out i))
                {
                    ULIM = TA5000Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ULIM");
                    return false;
                }
                if (algorithm.FindFileName(TA5000Struct, "LLIM", out i))
                {
                    LLIM = TA5000Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no LLIM");
                    return false;
                }
                
                if (algorithm.FindFileName(TA5000Struct, "NAME", out i))
                {
                    Name = TA5000Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }
                if (algorithm.FindFileName(TA5000Struct, "SENSOR", out i))
                {
                    Sensor = TA5000Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no SENSOR");
                    return false;
                }
                if (!Connect()) return false;
            }
            catch (Exception  ex)
            {

                logger.AdapterLogString(3, ex.Message);
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
          
                MyIO.WriteString("DUTM 1"); //DUT   Mode
                AirFlowSetting();
                AirTempsUpperlimit(syn);
                AirTempslowerlimit(syn);
                MyIO.WriteString("WNDW 0.5"); //窗体温度在差值0.5内稳定
                MyIO.WriteString("TRKL 1"); //热流仪升起后设置的小气流
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
                flag= MyIO.WriteString("FLWM " + StrAirFlow);
                logger.AdapterLogString(0, "AirFlowSet is" + StrAirFlow);
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
               // SetPositionUPDown("1");

                //if (Temp < 20)// Low
                //{
                //   SetPoint("2");

                //}
                //else if (Temp > 30)//Hig
                //{
                //    SetPoint("0");
                //}
                //else//AMB
                //{
                //   SetPoint("1");
                //}


                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Temp is " + Temp);
                    return MyIO.WriteString("SETP " + Str) ;//&& SetPositionUPDown("1") ;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(1000);
                        flag1 = MyIO.WriteString("SETP " + Str);
                        Thread.Sleep(1000);
                      //  flag1 =flag1&& SetPositionUPDown("1");
                   
                      //  Thread.Sleep(2000);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("SETP?");
                            readtemp = MyIO.ReadString();
                           // if (readtemp == Str + "\r\n")
                            if (readtemp.Contains(Temp.ToString()))
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
