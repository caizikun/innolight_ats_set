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

        private static object syncRoot = new object();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] PowStruct)
        {
            try
            {
                bReusable = true;
                int i = 0;
                if (Algorithm.FindFileName(PowStruct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(PowStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(PowStruct, "IOTYPE", out i))
                {
                    IOType = PowStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(PowStruct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(PowStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(PowStruct, "DUTCHANNEL", out i))
                {
                    DutChannel = PowStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DUTCHANNEL");
                    return false;
                }
              
                if (Algorithm.FindFileName(PowStruct, "DUTVOLTAGE", out i))
                {
                    DutVoltage = PowStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DUTVOLTAGE");
                    return false;
                }
                if (Algorithm.FindFileName(PowStruct, "DUTCURRENT", out i))
                {
                    DutCurrent = PowStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no DUTCURRENT");
                    return false;
                }
               
              
                if (Algorithm.FindFileName(PowStruct, "NAME", out i))
                {
                    Name = PowStruct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }
             
                
                if (Algorithm.FindFileName(PowStruct, "OPENDELAY", out i))
                {
                    opendelay = Convert.ToInt32(PowStruct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPENDELAY");
                    return false;
                }
                if (Algorithm.FindFileName(PowStruct, "CLOSEDELAY", out i))
                {
                    closedelay = Convert.ToInt32(PowStruct[i].DefaultValue);
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
        
       

        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "NIUSB":
                        //"USB0::0x1AB1::0x0E11::DP8D172400131::0::INSTR";
                        //MyIO = new IOPort(IOType, "USB" + Addr + "::0x1AB1::0x0E11::DP8D172400131::0::INSTR", logger);

                        lock (syncRoot)
                        {
                            this.WriteString("*IDN?");
                            string content = this.ReadString();
                            this.EquipmentConnectflag = content.Contains("DP8");
                        }
                        break;
                    default:
                        Log.SaveLogToTxt("NIUSB类型错误");
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

        protected override bool WriteString(string str_Write)
        {
            lock (syncRoot)
            {
                try
                {
                    return myIO.WriteString(IOPort.Type.NIUSB, "USB" + Addr + "::0x1AB1::0x0E11::DP8D172400131::0::INSTR", str_Write);
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

        protected override string ReadString(int count = 0)
        {
            lock (syncRoot)
            {
                try
                {
                    return myIO.ReadString(IOPort.Type.NIUSB, "USB" + Addr + "::0x1AB1::0x0E11::DP8D172400131::0::INSTR", count);
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

                        SelectRang();//选择量程为20V
                        ConfigVoltageCurrent("1", DutVoltage, DutCurrent);//设输出电压 和限制电流
                        OpenOcp(true);//开启电流限制功能
                        OpenSenseMode();//开启电压反馈功能
                        OutPutSwitch(false, syn);
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
                        Log.SaveLogToTxt("DP811A Switch is" + Switch_in);


                        // this.WriteString(":OUTP CH1,ON");

                        flag = this.WriteString(":OUTP CH1," + Switch_in);
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
                                Log.SaveLogToTxt("DP811A Switch is" + Switch_in);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("DP811A Switch wrong");

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
                try
                {
                    string Str_Write = "APPL CH1," + voltage + "," + current;
                    bool flag = this.WriteString(Str_Write);
                    Log.SaveLogToTxt("DP811A channel is" + "voltage is" + voltage + "current is" + current);
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

      ////  public override bool ConfigVoltageCurrent(string voltage)
      //  {

      //      string Str_Write;
      //      try
      //      {
      //          Str_Write = ":VOLT " + voltage;
      //         bool flag = this.WriteString(Str_Write);
      //         Log.SaveLogToTxt("DP811A  voltage is" + voltage);
      //          return flag;

      //      }

      //      catch (Exception error)
      //      {

      //          Log.SaveLogToTxt(error.ToString());
      //          return false;
      //      }
      //  }

        public override double GetCurrent()
        {
            lock (syncRoot)
            {
                double current = 0;
                try
                {
                    this.WriteString(":MEAS:CURR? CH1");
                    current = (Convert.ToDouble((this.ReadString(25)))) * 1000;
                    current = current - Convert.ToDouble(currentoffset);
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
                    this.WriteString("MEAS:VOLT? CH1");
                    Voltage = Convert.ToDouble((this.ReadString(10)));
                    Voltage = Voltage - Convert.ToDouble(voltageoffset);
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

        private bool OpenSenseMode()//:OUTP:RANG P20V
        {
            lock (syncRoot)
            {
                try
                {
                    this.WriteString(":OUTP:SENS CH1,ON");

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

        public bool OpenOcp(bool flag)// 开启通道的过电流保护模式
        {
            lock (syncRoot)
            {
                string StrWrite = "";
                try
                {
                    if (flag)
                    {
                        StrWrite = ":OUTP:OCP CH1,ON ";
                    }
                    else
                    {
                        StrWrite = ":OUTP:OCP CH1,OFF ";
                    }
                    this.WriteString(StrWrite);

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

        public bool SelectRang()// 选择输出电压的档位
        {
            lock (syncRoot)
            {
                try
                {


                    this.WriteString(":OUTP:RANG P20V");
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

    }

    
}
