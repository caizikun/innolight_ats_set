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
using System.IO;
using System.Windows.Forms;

namespace ATS_Driver
{

    public class N490XPPG : PPG
    {
        public Algorithm algorithm = new Algorithm();
        public N490XPPG(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] N490XPPGStruct)
        {
            
            int i = 0;
            if (algorithm.FindFileName(N490XPPGStruct,"ADDR",out i))
            {
                Addr = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"IOTYPE",out i))
            {
                IOType = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(N490XPPGStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"TRIGGERDRATIO",out i))
            {
                TriggerDRatio = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"TRIGGERMODE",out i))
            {
                TriggerMode = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"CLOCKHIGVOLTAGE",out i))
            {
                ClockHigVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"DATALOWVOLTAGE",out i))
            {
                DataLowVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"DATAHIGVOLTAGE",out i))
            {
                DataHigVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"CLOCKLOWVOLTAGE",out i))
            {
                ClockLowVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"BERTDATARATE",out i))
            {
                BertDataRate = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"PRBS",out i))
            {
                PRBS = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XPPGStruct,"NAME",out i))
            {
                Name = N490XPPGStruct[i].DefaultValue;
            }
            else
                return false;
            if (!Connect()) return false;
            return  true;
        }

        public override bool ChangeChannel(string channel)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset)
        {
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
                    ConfigureDataRate(BertDataRate);
                    ConfigurePRBS(PRBS);
                    ConfigureDataVoltage(DataHigVoltage,DataLowVoltage);
                    ConfigureClockVoltage(ClockHigVoltage, ClockLowVoltage);
                    ConfigureTriggerMode(TriggerMode);
                    ConfigureTriggerDRatio(TriggerDRatio);
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
       
        public override bool Connect()
        {
            try
            {
               // IO_Type

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
                return false;
            }
        }

        private bool ConfigureDataRate(string datarate)
        {
            bool flag = false;
            try
            {

                flag= MyIO.WriteString("source9:frequency " + datarate);
                System.Threading.Thread.Sleep(2000);
                logger.AdapterLogString(0, "datarate is" + BertDataRate);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            
            }

        }

        private bool ConfigurePRBS(string PRBS)
        {
            bool flag = false;
            try
            {

                flag= MyIO.WriteString("source1:pattern:select PRBS" + PRBS);               
                logger.AdapterLogString(0, "PRBS is" + PRBS);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
              
            }

        }

        private bool ConfigureDataVoltage(string DataHigVoltage, string DataLowVoltage)
        {
            try
            {

                if (MyIO.WriteString("source1:voltage:Low " + DataLowVoltage)
                && MyIO.WriteString("source1:voltage:High " + DataHigVoltage))
                {
                    logger.AdapterLogString(0, "DataHigVoltage is" + DataHigVoltage + "DatalowVoltage is" + DataLowVoltage);
                    return true;
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
               
            }
        }
        private bool ConfigureClockVoltage(string ClockHigVoltage, string ClockLowVoltage)
        {
            try
            {


                if (MyIO.WriteString("source2:voltage:Low " + ClockLowVoltage)
                    && MyIO.WriteString("source2:voltage:High " + ClockHigVoltage))
                {
                    
                    logger.AdapterLogString(0, "ClockHigVoltage is" + ClockHigVoltage + "ClocklowVoltage is" + ClockLowVoltage);
                    return true;
                }
                else
                {
                    return false;
                }
               // 

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
               
            }
        }
        private bool ConfigureTriggerMode(string PGTriggerMode)
        {
            bool flag = false;
            try
            {

                flag=  MyIO.WriteString("source3:trigger:mode " + PGTriggerMode);
                logger.AdapterLogString(0, "TriggerMode is" + TriggerMode);
                    
              return flag;  
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool ConfigureTriggerDRatio(string TriggerDRatio)
        {
            bool flag = false;
            try
            {

                flag= MyIO.WriteString("source3:trigger:DCDRatio " + TriggerDRatio);
                logger.AdapterLogString(0, "TriggerDRatio is" + TriggerDRatio);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
               
            }
        }
        
        
    }
    public class N490XED : ErrorDetector
    {
        public Algorithm algorithm = new Algorithm();
        public N490XED(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] N490XEDStruct)
        {
           
            int i = 0;
            if (algorithm.FindFileName(N490XEDStruct,"ADDR",out i))
            {
                Addr = N490XEDStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XEDStruct,"IOTYPE",out i))
            {
                IOType = N490XEDStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XEDStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(N490XEDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(N490XEDStruct,"CDRSWITCH",out i))
            {
                CDRSwitch = Convert.ToBoolean(N490XEDStruct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(N490XEDStruct,"CDRFREQ",out i))
            {
                CDRFreq = N490XEDStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XEDStruct,"PRBS",out i))
            {
                PRBS =N490XEDStruct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(N490XEDStruct,"NAME",out i))
            {
                Name = N490XEDStruct[i].DefaultValue;
            }
            else
                return false;
            if (!Connect()) return false;
            return true;
        }



        public override bool Configure()
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
                if (!ConfigurePRBS(PRBS)) return false;
                if (!ConfigureCDRONOFF(CDRSwitch)) return false;
                if (!ConfigureCDRFreq(CDRFreq)) return false;
                EquipmentConfigflag = true;
            }
            return true;


        }
        public override bool ChangeChannel(string channel)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset)
        {
            return true;
        }
        public override bool Connect()
        {
            try
            {
                // IO_Type

                switch (IOType)
                {
                    case "GPIB":
                        MyIO = new IOPort(IOType, "GPIB::" + Addr);//new IO_Port(IO_Type, Addr);
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

        private bool ConfigurePRBS(string PRBS)
        {
            bool flag = false;
            try
            {

                flag= MyIO.WriteString("source1:pattern:select PRBS" + PRBS);
                logger.AdapterLogString(0, "PRBS is" + PRBS);
                return flag;


            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool AutoAlaign(bool becenter)
        {
            string temp = null;
            int isin = 0;
            try
            {
                if (becenter)
                {
                    if (MyIO.WriteString("sense1:eye:tcenter 1"))
                    {
                        byte i = 0;
                        do
                        {
                            MyIO.WriteString("sense1:eye:tcenter?");
                            Thread.Sleep(300);
                            temp = MyIO.ReadString(19);
                            Thread.Sleep(300);
                             isin = temp.IndexOf("SUCCESSFUL");
                             i++;
                        } while (i < 20 && isin == -1);
                        MyIO.WriteString("sense1:eye:align:auto " + "1");
                        i = 0;
                        temp = null;
                        do
                        {
                            MyIO.WriteString("sense1:eye:align:auto?");
                            Thread.Sleep(200);
                            temp = MyIO.ReadString(19);
                            Thread.Sleep(200);
                            isin = temp.IndexOf("SUCCESSFUL");
                            i++;
                        } while (i < 30 && isin == -1);
                        MyIO.WriteString("sense1:eye:align:auto " + "0");
                        if (isin!=-1)
                        {
                            return true;
                        }
                        else return false;
                    }
                    else return false;

                }
                else
                {
                    byte i = 0;
                    MyIO.WriteString("sense1:eye:align:auto " + "1");
                    i = 0;
                    temp = null;
                    do
                    {
                        MyIO.WriteString("sense1:eye:align:auto?");
                        temp = MyIO.ReadString(19);
                        isin = temp.IndexOf("SUCCESSFUL");
                        i++;
                    } while (i < 30 && isin == -1);
                    MyIO.WriteString("sense1:eye:align:auto " + "0");
                    if (temp == "SUCCESSFUL")
                    {
                        return true;
                    }
                    else return false;
                }
              
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
            
        }
        private bool ConfigureCDRONOFF(bool Switch)
        {
            string index;
            try
            {
                if (Switch)
                { index = "ON"; }
                else
                { index = "OFF"; }
                MyIO.WriteString("SENS2:Frequency:CDR " + index);
                    
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        private bool ConfigureCDRFreq(string CDRFreq)
        {
            bool flag = false;
            try
            {
                flag= MyIO.WriteString("sens1:frequency " + CDRFreq);
                logger.AdapterLogString(0, "CDRFrequence is" + CDRFreq);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override double GetErrorRate()
        {
            
            double ErrorRate = 0;
            string strErrorRate = "1";
            try
            {

                MyIO.WriteString("sense1:gate:state ON");
                Thread.Sleep(1000);
                MyIO.WriteString("sense1:gate:state OFF");
                MyIO.WriteString("fetch:sense1:eratio:total?");
                strErrorRate = MyIO.ReadString(32);
            }
            catch (Exception error)
            {
                strErrorRate = "2";
                logger.AdapterLogString(3, error.ToString());
                return ErrorRate;
            }
            return ErrorRate = double.Parse(strErrorRate);
        }
    }
}
