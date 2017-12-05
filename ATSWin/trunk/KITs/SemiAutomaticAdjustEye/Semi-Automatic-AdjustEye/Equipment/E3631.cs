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
        public E3631(logManager logmanager)
        {
            logger = logmanager;

            bReusable = true;
        }
        public Algorithm algorithm = new Algorithm();

        public override bool Initialize(TestModeEquipmentParameters[] PSStruct)
        {
            try
            {
               
                int i = 0;
                if (algorithm.FindFileName(PSStruct,"ADDR",out i))
                {
                    Addr = Convert.ToByte(PSStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(PSStruct, "IOTYPE", out i))
                {
                    IOType = PSStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
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
        
        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":
                        MyIO = new IOPort(IOType, "GPIB0::" + Addr, logger);
                        MyIO.IOConnect();
                        EquipmentConnectflag = true;
                        MyIO.WriteString("*IDN?");
                        string S = MyIO.ReadString();
                        EquipmentConnectflag = S.Contains("E3631");
                        break;
                    default:
                        logger.AdapterLogString(4, "GPIB类型错误");
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
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            voltageoffset = offset;
            return true;
        }
        public override bool Configure(int syn = 0)
        {

            try
            {

                if (EquipmentConfigflag)//曾经设定过
                {
                    return true;

                }
                else//未曾经设定过
                {
                    OutPutSwitch(true, syn);
                    EquipmentConfigflag = true;
                }
                return true;

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }



        }
       // OutPutSwitch
        public override bool OutPutSwitch(bool Switch, int syn = 0)
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
                    logger.AdapterLogString(0, "E3631 Switch is" + Switch_in);
                    flag= MyIO.WriteString("OUTP " + Switch_in);
                    Thread.Sleep(delay);
                    return flag;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("OUTP " + Switch_in);
                        
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("OUTP?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == intswitch + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "E3631 Switch is" + Switch_in);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "E3631 Switch wrong");

                        }

                    }
                    Thread.Sleep(delay);
                    return flag;
                }
            }
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        protected  bool ConfigVoltageCurrent(string channel, string voltage, string current)
        {
            double vol = Convert.ToDouble(voltage);
            double offset = Convert.ToDouble(voltageoffset);
            string volandoffset = (vol + offset).ToString();

            bool flag = false;
            string Str_Write="";//= "APPL P6V," + str_V + "," + Str_I;
            string str_channel="" ;
            if (Convert.ToInt16( channel) == 1)
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
              flag=  MyIO.WriteString(Str_Write);
              logger.AdapterLogString(0, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current);
              return flag;
               
            }

            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        protected bool ConfigVoltageCurrent(string channel, string voltage, string current, int syn = 0)
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

                    flag = MyIO.WriteString(Str_Write);
                    string Str = GetVoltage().ToString();
                    logger.AdapterLogString(0, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current);
                    return flag;
                }
                else
                {
                    flag = false;

                    for (int i = 0; i < 3; i++)
                    {

                        flag = MyIO.WriteString(Str_Write);
                        Thread.Sleep(500);
                        MyIO.WriteString(str_channel + "?");
                        string StrVoltage = MyIO.ReadString();
                        double SetVoltage = vol + offset;
                        double MeasurtVoltage = Convert.ToDouble(StrVoltage);
                        if (MeasurtVoltage <= SetVoltage * 1.005 && MeasurtVoltage >= SetVoltage * 0.995)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current);
                 
                            break;
                        }
                    }
                    if (!flag)
                    {

                        logger.AdapterLogString(3, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + current+"ERROR");
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

        public override bool ConfigVoltageCurrent(string voltage,int syn = 0)
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

                     MyIO.WriteString(Str_Write);
                     MyIO.WriteString("*opc?");
                     string StrTemp = MyIO.ReadString().Replace("\n","");
                     if (StrTemp == "1")
                     {
                         flag = true;
                     }
                     else
                     {
                         flag = false;
                     }

                    // string Str = GetVoltage().ToString();
                    logger.AdapterLogString(0, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + DutCurrent);
                    return flag;
                }
                else// 因为读取当前的电压值 会导致仪器脱离程控, 不适用同步模式
                {
                    flag = false;

                    for (int i = 0; i < 3; i++)
                    {

                        flag = MyIO.WriteString(Str_Write);
                        Thread.Sleep(500);
                        // MyIO.WriteString(str_channel + "?");
                        string StrVoltage = GetVoltage().ToString();
                       // double SetVoltage = vol + offset;
                        double MeasurtVoltage = Convert.ToDouble(StrVoltage);
                        if (MeasurtVoltage <= volandoffset * 1.005 && MeasurtVoltage >= volandoffset * 0.995)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + DutCurrent);

                            break;
                        }
                    }
                    if (!flag)
                    {

                        logger.AdapterLogString(3, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + DutCurrent + "ERROR");
                    }
                    return flag;
                }
            }
            catch
            {
                logger.AdapterLogString(3, "E3631 channel is" + str_channel + "voltage is" + volandoffset + "current is" + DutCurrent + "ERROR");
                return false;
            }
        }
        public override double GetCurrent()
        {
            double current=0;
            try
            {

                MyIO.WriteString("MEAS:CURR?");
                current=(Convert.ToDouble((MyIO.ReadString(25))))*1000;
                current = current - Convert.ToDouble(currentoffset);
                return current;

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return current;
            }

        }
        public override double GetVoltage()
        {
            double Voltage=0;
            try
            {

                MyIO.WriteString("MEAS:VOLT? P6V");
               // MyIO.WriteString("MEAS:VOLT?");
                Voltage = Convert.ToDouble((MyIO.ReadString(10)));
               // Voltage = Voltage - Convert.ToDouble(voltageoffset);
                return Voltage;

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return Voltage;
            }

        }
    }

    
}
