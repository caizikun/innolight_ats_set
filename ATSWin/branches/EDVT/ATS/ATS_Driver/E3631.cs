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
        }
        public Algorithm algorithm = new Algorithm();
        public override bool Initialize(TestModeEquipmentParameters[] PSStruct)
        {
            try
            {
               
                int i = 0;
                if (algorithm.FindFileName(PSStruct,"ADDR",out i))
                {
                    Addr = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"IOTYPE",out i))
                {
                    IOType = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(PSStruct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"DUTCHANNEL",out i))
                {
                    DutChannel = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"OPTSOURCECHANNEL",out i))
                {
                    OptSourceChannel = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"DUTVOLTAGE",out i))
                {
                    DutVoltage = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"DUTCURRENT",out i))
                {
                    DutCurrent = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"OPTVOLTAGE",out i))
                {
                    OptVoltage = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"OPTCURRENT",out i))
                {
                    OptCurrent = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(PSStruct,"NAME",out i))
                {
                    Name = PSStruct[i].DefaultValue;
                }
                else
                    return false;
                if (!Connect()) return false;

            }

            catch (Error_Message error)
            {

                logger.AdapterLogString(3, error.ToString());
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

                        MyIO = new IOPort(IOType, "GPIB::" + Addr);
                        EquipmentConnectflag = true;;
                        break;
                    default:
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
        public override bool ChangeChannel(string channel)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset)
        {
            return true;
        }
        public override bool Configure()
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
                        ConfigVoltageCurrent(OptSourceChannel, OptVoltage,OptCurrent);
                    }

                    Switch(true);
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
        public override bool Switch(bool Switch)
        {
            // "ON "" OFF"
            string Switch_in;
            if (Switch)
            {
                Switch_in = "ON";
            }
            else
            {
                Switch_in = "OFF";
            }
            try
            {

                return MyIO.WriteString("OUTP " + Switch_in);

            }
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        protected  bool ConfigVoltageCurrent(string channel, string voltage, string current)
        {
            bool flag = false;
            string Str_Write="";//= "APPL P6V," + str_V + "," + Str_I;
            string str_channel="" ;
            if (Convert.ToInt16( channel) == 1)
            {
                Str_Write = "APPL P6V," + voltage + "," + current;
                str_channel = "APPL P6V";
            }
            else if (Convert.ToInt16(channel) == 2)
            {
                Str_Write = "APPL P25V," + voltage+ "," + current;
                str_channel = "APPL P25V";
            }
         
            try
            {
              flag=  MyIO.WriteString(Str_Write);
              logger.AdapterLogString(0, "channel is" + str_channel + "voltage is" + voltage + "current is" + current);
              return flag;
               
            }

            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ConfigVoltageCurrent(string voltage)
        {
            bool flag = false;
            string Str_Write = "";//= "APPL P6V," + str_V + "," + Str_I;
            string str_channel = "";
            if (Convert.ToInt16(DutChannel) == 1)
            {
                Str_Write = "APPL P6V," + voltage + "," + DutCurrent;
                str_channel = "APPL P6V";
            }
            else if (Convert.ToInt16(DutChannel) == 2)
            {
                Str_Write = "APPL P25V," + voltage + "," + DutCurrent;
                str_channel = "APPL P25V";
            }

            try
            {
                flag = MyIO.WriteString(Str_Write);
                logger.AdapterLogString(3, "channel is" + str_channel + "voltage is" + voltage + "current is" + DutCurrent);
                return flag;

            }

            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
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

                MyIO.WriteString("MEAS:VOLT?");
                Voltage = Convert.ToDouble((MyIO.ReadString(10)));
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
