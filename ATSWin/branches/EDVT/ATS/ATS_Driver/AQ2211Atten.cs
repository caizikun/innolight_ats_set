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
        public Algorithm algorithm = new Algorithm();
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
                    AttSlot = AQ2211list[i].DefaultValue;
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

                        MyIO = new IOPort(IOType, "GPIB::" + Addr);
                       
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
                    ConfigWavelength();
                    AttnValue(AttValue);
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
        public override bool ChangeChannel(string  channel)
        {
            
            string Wavelength = "";
            bool flag1, flag2;
            string offset="";
            CurrentChannel = channel;
            try
            {
                flag1 = ConfigWavelength(CurrentChannel);
                if (offsetlist.ContainsKey(CurrentChannel))
                    offset = offsetlist[CurrentChannel];
                flag2 = SetOffset(offset);
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
       

        public override bool configoffset(string channel, string offset)
        {
            offsetlist.Add(channel, offset);
            return true;
        }
        protected bool SetOffset(string offset)
        {
            bool flag = false;
            try
            {
                //offset = Math.Abs((Convert.ToDouble(AttValue) + Convert.ToDouble(offset))).ToString();
                double temp = (Convert.ToDouble(offset));
                temp = temp * -1;
                offset = Convert.ToString(temp);
                flag = MyIO.WriteString(":INP" + AttSlot + ":OFFS " + offset);
                logger.AdapterLogString(0, "AttSlot is" + AttSlot + "offset is" + offset);   
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        
        
        }
       
        
        protected bool ConfigWavelength(string dutcurrentchannel)
        {
            //string CurrnentWavelength = "";
            bool flag = false;
            string[] wavtemp = new string[4];
            Wavelength = Wavelength.Trim();
            wavtemp = Wavelength.Split(new char[] { ',' });
            byte i = Convert.ToByte(Convert.ToInt16(dutcurrentchannel)-1);
            try
            {
                //flag = MyIO.WriteString(":sense" + AttSlot + ":channel " + AttChannel + ":power:wavelength" + wavtemp[i] + "E-9");
                flag = MyIO.WriteString(":INP" + AttSlot + ":WAV " + wavtemp[i] + "nm");  
                logger.AdapterLogString(0, "AttSlot is" + AttSlot + "Wavelength is" + wavtemp[i] + "nm");
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        
        }
        public override bool ConfigWavelength()
        {
            bool flag = false;
            try
            {
                flag = ConfigWavelength("1");
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool AttnValue(string Value)
        {
            bool flag = false;
            try
            {
                
                Value = Math.Abs((Convert.ToDouble(Value))).ToString();
                flag = MyIO.WriteString(":INP" + AttSlot + ":ATT " + Value);
                logger.AdapterLogString(0, "AttSlot is" + AttSlot + "ATT VALUE IS" + Value);    
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
       
        public override bool Switch(bool Swith)
        {
            string index;
            if (Swith)
            {
                index = "ON";
            }
            else
            {
                index = "OFF";
            }
            try
            {
                MyIO.WriteString("OUTP" + AttSlot + ":STAT " + index);
                return true;
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
