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
  
 
    public class DP811A: Powersupply
    {
        public DP811A(logManager logmanager)
        {
            logger = logmanager;

            bReusable = true;
        }
        public Algorithm algorithm = new Algorithm();

        public override bool Initialize(TestModeEquipmentParameters[] PowStruct)
        {
            try
            {
               
                int i = 0;
                if (algorithm.FindFileName(PowStruct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(PowStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(PowStruct, "IOTYPE", out i))
                {
                    IOType = PowStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }
                if (!Connect()) return false;
            }

            catch (Exception error)
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
                    case "NIUSB":
                        //"USB0::0x1AB1::0x0E11::DP8D172400131::0::INSTR";
                        MyIO = new IOPort(IOType, "USB" + Addr + "::0x1AB1::0x0E11::DP8D172400131::0::INSTR", logger);
                        MyIO.IOConnect();
                        EquipmentConnectflag = true;
                        MyIO.WriteString("*IDN?");
                        EquipmentConnectflag = MyIO.ReadString().Contains("DP8");
                        break;
                    default:
                        logger.AdapterLogString(4, "NIUSB类型错误");
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

                    if (Reset == true)
                    {
                        ReSet();
                    }

                    OpenOcp(true);//开启电流限制功能
                    OpenSenseMode();//开启电压反馈功能
                    OutPutSwitch(false, syn);
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
                    logger.AdapterLogString(0, "DP811A Switch is" + Switch_in);


                    // MyIO.WriteString(":OUTP CH1,ON");

                    flag = MyIO.WriteString(":OUTP CH1," + Switch_in);
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
                            logger.AdapterLogString(0, "DP811A Switch is" + Switch_in);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "DP811A Switch wrong");

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
            try
            {
               string Str_Write = "APPL CH1," + voltage + "," + current;
               bool  flag=  MyIO.WriteString(Str_Write);
              logger.AdapterLogString(0, "DP811A channel is" + "voltage is" + voltage + "current is" + current);
              return flag;
               
            }

            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

      ////  public override bool ConfigVoltageCurrent(string voltage)
      //  {

      //      string Str_Write;
      //      try
      //      {
      //          Str_Write = ":VOLT " + voltage;
      //         bool flag = MyIO.WriteString(Str_Write);
      //         logger.AdapterLogString(3, "DP811A  voltage is" + voltage);
      //          return flag;

      //      }

      //      catch (Exception error)
      //      {

      //          logger.AdapterLogString(3, error.ToString());
      //          return false;
      //      }
      //  }

        public override double GetCurrent()
        {
            double current = 0;
            try
            {
                MyIO.WriteString(":MEAS:CURR? CH1");
                current = (Convert.ToDouble((MyIO.ReadString(25)))) * 1000;
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
            double Voltage = 0;
            try
            {
                MyIO.WriteString("MEAS:VOLT? CH1");
                Voltage = Convert.ToDouble((MyIO.ReadString(10)));
                Voltage = Voltage - Convert.ToDouble(voltageoffset);
                return Voltage;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return Voltage;
            }
        }

        private bool OpenSenseMode()//:OUTP:RANG P20V
        {
            MyIO.WriteString(":OUTP:SENS CH1,ON");

            return true;
        }

        public bool OpenOcp(bool flag)// 开启通道的过电流保护模式
        {
            string StrWrite="";
            if (flag)
            {
                StrWrite = ":OUTP:OCP CH1,ON ";
            }
            else
            {
                StrWrite = ":OUTP:OCP CH1,OFF ";
            }
            MyIO.WriteString(StrWrite);
       
            return true;
        }

        public bool SelectRang()// 选择输出电压的档位
        {
            MyIO.WriteString(":OUTP:RANG P20V");
            return true;
        }

    }

    
}
