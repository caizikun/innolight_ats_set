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


    public class AQ2211Atten : Attennuator
    {
        private double CurrentOffset;
        public Algorithm algorithm = new Algorithm();
      //  private string StrAttSlot;
        private string[] SlotList;
        public AQ2211Atten(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] AQ2211list)
        {
            try
            {
                int i = 0;
                if (algorithm.FindFileName(AQ2211list,"ADDR",out i))
                {
                    Addr = AQ2211list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list,"IOTYPE",out i))
                {
                    IOType = AQ2211list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(AQ2211list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list,"ATTSLOT",out i))
                {
                   // AttSlot = AQ2211list[i].DefaultValue;
                   // StrAttSlot = AQ2211list[i].DefaultValue;
                    SlotList = AQ2211list[i].DefaultValue.Split(',');
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list,"ATTCHANNEL",out i))
                {
                    AttChannel = AQ2211list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list,"ATTVALUE",out i))
                {
                    AttValue = AQ2211list[i].DefaultValue;
                }
                else
                    return false;
               
                if (algorithm.FindFileName(AQ2211list,"TOTALCHANNEL",out i))
                {
                    TotalChannel = AQ2211list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list,"WAVELENGTH",out i))
                {
                    Wavelength = AQ2211list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list,"NAME",out i))
                {
                    Name = AQ2211list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list, "OPENDELAY", out i))
                {
                    opendelay = Convert.ToInt32(AQ2211list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list, "CLOSEDELAY", out i))
                {
                    closedelay = Convert.ToInt32(AQ2211list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(AQ2211list, "SETATTDELAY", out i))
                {
                    setattdelay = Convert.ToInt32(AQ2211list[i].DefaultValue);
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
        
        public override  bool Connect()
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
            List<string> ch = new List<string>();

            try
            {


                if (EquipmentConfigflag)
                {

                    return true;

                }
                else
                {
                    if (Reset == true)
                    {
                        ReSet();
                    }
                    for (int i = 0; i < SlotList.Length; i++)
                    {
                        if (!(ch.Contains(SlotList[i])))
                        {
                            ch.Add(SlotList[i]);
                            AttSlot = SlotList[i].ToString();
                            ConfigWavelength(AttChannel, syn);
                            AttnValue(AttValue, syn);
                            Switch(true, syn);
                        }
                    }
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
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            
            string Wavelength = "";
            bool flag1, flag2;
            string offset="";
            CurrentChannel = channel;
            try
            {
                for (int i = 0; i < Convert.ToInt32(TotalChannel); i++)
                {
                    CurrentOffset = 0;
                    AttSlot = SlotList[i].ToString();
                    AttnValue(AttValue, syn);
                }
                AttSlot = SlotList[Convert.ToByte(CurrentChannel) - 1].ToString();
                flag1 = ConfigWavelength(CurrentChannel);
                if (offsetlist.ContainsKey(CurrentChannel))
                    offset = offsetlist[CurrentChannel];
                flag2 = SetOffset("0");//将所有 Offset设置为了 0
                CurrentOffset = Convert.ToDouble(offset);
                logger.AdapterLogString(0, "channel is" + CurrentChannel + "Wavelength is" + Wavelength + "offset is" + offset);

                
                if (flag1 && flag2)
                    return true;
                else
                    return false;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }


        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            offsetlist.Add(channel, offset);
            return true;
        }
        protected bool SetOffset(string offset,int syn=0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                //offset = Math.Abs((Convert.ToDouble(AttValue) + Convert.ToDouble(offset))).ToString();
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is" + AttSlot + "offset is" + offset);
                    return MyIO.WriteString(":INP" + AttSlot + ":OFFS " + offset);
                }
                else
                {
                    double temp = (Convert.ToDouble(offset));
                    temp = temp * -1;
                    offset = Convert.ToString(temp);
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":INP" + AttSlot + ":OFFS " + offset);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":INP" + AttSlot + ":OFFS?");
                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble(offset))
                                break;
                        }
                        if (k <= 3)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "AttSlot is" + AttSlot + "offset is" + offset);
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set offset wrong");
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


        protected bool ConfigWavelength(string dutcurrentchannel, int syn = 0)
        {
            //string CurrnentWavelength = "";
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double waveinput;
            double waveoutput;
            string[] wavtemp = new string[4];
            Wavelength = Wavelength.Trim();
            wavtemp = Wavelength.Split(new char[] { ',' });
            byte i = Convert.ToByte(Convert.ToInt16(dutcurrentchannel)-1);
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is" + AttSlot + "Wavelength is" + wavtemp[i] + "nm");
                    return MyIO.WriteString(":INP" + AttSlot + ":WAV " + wavtemp[i] + "nm");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":INP" + AttSlot + ":WAV " + wavtemp[i] + "nm");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":INP" + AttSlot + ":WAV?");
                            readtemp = MyIO.ReadString();
                            waveinput = Convert.ToDouble(wavtemp[i]);
                            waveoutput = Convert.ToDouble(readtemp) * Math.Pow(10, 9);
                            if (waveinput == waveoutput)
                                break;
                        }
                        if (k <= 3)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "AttSlot is" + AttSlot + "Wavelength is" + wavtemp[i] + "nm");
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set Wavelength wrong");

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
        public override bool ConfigWavelength(int syn=0)
        {
            bool flag = false;
            try
            {
                flag = ConfigWavelength(AttChannel, syn);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool AttnValue(string Value, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {

                Value = Math.Abs((Convert.ToDouble(Value) - CurrentOffset)).ToString();// 仪器中的Offset将不在添加.在计算中使用.
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is" + AttSlot + "ATT VALUE IS" + Value);
                    flag= MyIO.WriteString(":INP" + AttSlot + ":ATT " + Value);
                    Thread.Sleep(setattdelay);
                    return flag;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":INP" + AttSlot + ":ATT " + Value);
                        
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":INP" + AttSlot + ":ATT?");
                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble(Value))
                                break;
                        }
                        if (k <= 3)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "AttSlot is" + AttSlot + "ATT VALUE IS" + Value);
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set ATT VALUE wrong");
                        }
                    }
                    Thread.Sleep(setattdelay);
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool Switch(bool Swith, int syn = 0)
        {
            string index;
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            string  intswitch="";
            int delay = 0;
            if (Swith)
            {
                index = "ON";
                intswitch = "1";
                delay = opendelay;
            }
            else
            {
                index = "OFF";
                intswitch = "0";
                delay = closedelay;
            }
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is" + AttSlot + "state IS" + index);
                    flag= MyIO.WriteString("OUTP" + AttSlot + ":STAT " + index);
                    Thread.Sleep(delay);
                    return flag;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("OUTP" + AttSlot + ":STAT " + index);
                        
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("OUTP" + AttSlot + ":STAT?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == intswitch)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "AttSlot is" + AttSlot + "state IS" + index);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set switch wrong");

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
        public override bool AddCalFactor(string CalFactor)
        {
            bool flag = false;
            try
            {
                flag = MyIO.WriteString(":sens" + AttSlot + ":channel" + AttChannel + ":CORR:LOSS:INP:MAGN" + CalFactor);
                logger.AdapterLogString(0, "AttSlot is" + AttSlot + "Channel is" + AttChannel + "CalFactor is" + CalFactor);    
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
            
        }
        public override double GetAtten()
        {
            double atten=0;
            try
            {
                MyIO.WriteString(":INP" + AttSlot+ ":ATT?");
                atten =Convert.ToDouble(MyIO.ReadString(16));
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return atten;
            }
            return atten;
        }













    }


}
