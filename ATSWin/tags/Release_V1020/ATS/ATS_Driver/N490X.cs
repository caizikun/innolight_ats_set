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
            {
                logger.AdapterLogString(4, "there is no ADDR");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"IOTYPE",out i))
            {
                IOType = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(N490XPPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"TRIGGERDRATIO",out i))
            {
                TriggerDRatio = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no TRIGGERDRATIO");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"TRIGGERMODE",out i))
            {
                TriggerMode = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no TRIGGERMODE");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"CLOCKHIGVOLTAGE",out i))
            {
                ClockHigVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CLOCKHIGVOLTAGE");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"DATALOWVOLTAGE",out i))
            {
                DataLowVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no DATALOWVOLTAGE");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"DATAHIGVOLTAGE",out i))
            {
                DataHigVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no DATAHIGVOLTAGE");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"CLOCKLOWVOLTAGE",out i))
            {
                ClockLowVoltage = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CLOCKLOWVOLTAGE");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"BERTDATARATE",out i))
            {
                BertDataRate = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no BERTDATARATE");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"PRBS",out i))
            {
                PRBS = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no PRBS");
                return false;
            }
            if (algorithm.FindFileName(N490XPPGStruct,"NAME",out i))
            {
                Name = N490XPPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }
            if (!Connect()) return false;
            return  true;
        }

        public override bool ChangeChannel(string channel, int syn = 0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
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
                    ConfigureDataRate(BertDataRate, syn);
                    ConfigurePRBS(PRBS, syn);
                    ConfigureDataVoltage(DataHigVoltage, DataLowVoltage, syn);
                    ConfigureClockVoltage(ClockHigVoltage, ClockLowVoltage, syn);
                    ConfigureTriggerMode(TriggerMode, syn);
                    ConfigureTriggerDRatio(TriggerDRatio, syn);
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

        private bool ConfigureDataRate(string datarate,int syn=0)
        {
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            bool flag = false;
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "datarate is" + BertDataRate);
                    return MyIO.WriteString("source9:frequency " + datarate);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("source9:frequency " + datarate);
                        System.Threading.Thread.Sleep(2000);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("source9:frequency?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == datarate)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "datarate is" + BertDataRate);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "ConfigureDataRate wrong");

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

        private bool ConfigurePRBS(string PRBS, int syn = 0)
        {
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            bool flag = false;
            try
            {

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PRBS is" + PRBS);
                    return MyIO.WriteString("source1:pattern:select PRBS" + PRBS);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("source1:pattern:select PRBS" + PRBS);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("source1:pattern:select?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == "PRBS" + PRBS)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "PRBS is" + PRBS);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set PRBS wrong");

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

        private bool ConfigureDataVoltage(string DataHigVoltage, string DataLowVoltage, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            bool flag2=false;
            int k = 0;
            int j = 0;
            string readtemp = "";
            try
            {

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "DataHigVoltage is" + DataHigVoltage + "DatalowVoltage is" + DataLowVoltage);
                    flag1 = MyIO.WriteString("source1:voltage:High " + DataHigVoltage);
                    flag2 = MyIO.WriteString("source1:voltage:Low " + DataLowVoltage);
                    if ((flag1 == true) && (flag2 == true))
                    { flag = true; }
                    return flag;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("source1:voltage:High " + DataHigVoltage);
                        flag2 = MyIO.WriteString("source1:voltage:Low " + DataLowVoltage);
                        if ((flag1 == true) && (flag2 == true))
                            break;
                    }
                    if ((flag1 == true) && (flag2 == true))
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("source1:voltage:High?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == DataHigVoltage)
                                break;
                        }
                        for (j = 0; j < 3; j++)
                        {

                            MyIO.WriteString("source1:voltage:Low?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == DataLowVoltage)
                                break;
                        }
                        if ((k <= 3) && (j <= 3))
                        {
                            logger.AdapterLogString(0, "DataHigVoltage is" + DataHigVoltage + "DatalowVoltage is" + DataLowVoltage);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "ConfigureDataVoltage wrong");

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
        private bool ConfigureClockVoltage(string ClockHigVoltage, string ClockLowVoltage, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            bool flag2 = false;
            int k = 0;
            int j = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "ClockHigVoltage is" + ClockHigVoltage + "ClocklowVoltage is" + ClockLowVoltage);
                    flag1 = MyIO.WriteString("source2:voltage:Low " + ClockLowVoltage);
                    flag2 = MyIO.WriteString("source2:voltage:High " + ClockHigVoltage);
                    if ((flag1 == true) && (flag2 == true))
                    { flag = true; }
                    return flag;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("source2:voltage:Low " + ClockLowVoltage);
                        flag2 = MyIO.WriteString("source2:voltage:High " + ClockHigVoltage);
                        if ((flag1 == true) && (flag2 == true))
                            break;
                    }
                    if ((flag1 == true) && (flag2 == true))
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("source2:voltage:Low?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == ClockLowVoltage)
                                break;
                        }
                        for (j = 0; j < 3; j++)
                        {

                            MyIO.WriteString("source2:voltage:High?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == ClockHigVoltage)
                                break;
                        }
                        if ((k <= 3) && (j <= 3))
                        {
                            logger.AdapterLogString(0, "ClockHigVoltage is" + ClockHigVoltage + "ClocklowVoltage is" + ClockLowVoltage);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "ConfigureClockVoltage wrong");

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
        private bool ConfigureTriggerMode(string PGTriggerMode, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {


                if (syn == 0)
                {
                    logger.AdapterLogString(0, "TriggerMode is" + TriggerMode);
                    return MyIO.WriteString("source3:trigger:mode " + PGTriggerMode);   

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("source3:trigger:mode " + PGTriggerMode);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("source3:trigger:mode?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == PGTriggerMode)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "TriggerMode is" + TriggerMode);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set TriggerMode wrong");

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
        private bool ConfigureTriggerDRatio(string TriggerDRatio, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {


                if (syn == 0)
                {
                    logger.AdapterLogString(0, "TriggerDRatio is" + TriggerDRatio);
                    return MyIO.WriteString("source3:trigger:DCDRatio " + TriggerDRatio);

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("source3:trigger:DCDRatio " + TriggerDRatio);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("source3:trigger:CTDRatio?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == TriggerDRatio)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "TriggerDRatio is" + TriggerDRatio);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set TriggerDRatio wrong");

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
            {
                logger.AdapterLogString(4, "there is no ADDR");
                return false;
            }
            if (algorithm.FindFileName(N490XEDStruct,"IOTYPE",out i))
            {
                IOType = N490XEDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }
            if (algorithm.FindFileName(N490XEDStruct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(N490XEDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }
            if (algorithm.FindFileName(N490XEDStruct,"CDRSWITCH",out i))
            {
                CDRSwitch = Convert.ToBoolean(N490XEDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no CDRSWITCH");
                return false;
            }
            if (algorithm.FindFileName(N490XEDStruct,"CDRFREQ",out i))
            {
                CDRFreq = N490XEDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CDRFREQ");
                return false;
            }
            if (algorithm.FindFileName(N490XEDStruct,"PRBS",out i))
            {
                PRBS =N490XEDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no PRBS");
                return false;
            }
            if (algorithm.FindFileName(N490XEDStruct,"NAME",out i))
            {
                Name = N490XEDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }
            if (!Connect()) return false;
            return true;
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
                if (!ConfigurePRBS(PRBS, syn)) return false;
                if (!ConfigureCDRONOFF(CDRSwitch, syn)) return false;
                if (!ConfigureCDRFreq(CDRFreq, syn)) return false;
                EquipmentConfigflag = true;
            }
            return true;


        }
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
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
                        MyIO = new IOPort(IOType, "GPIB::" + Addr, logger);//new IO_Port(IO_Type, Addr);
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

        private bool ConfigurePRBS(string PRBS, int syn = 0)
        {
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            bool flag = false;
            try
            {

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "PRBS is" + PRBS);

                    return MyIO.WriteString("source1:pattern:select PRBS" + PRBS);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("source1:pattern:select PRBS" + PRBS);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("source1:pattern:select?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == "PRBS" + PRBS)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "PRBS is" + PRBS);

                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set PRBS wrong");

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
        private bool ConfigureCDRONOFF(bool Switch, int syn = 0)
        {
            string index;
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (Switch)
                { index = "ON"; }
                else
                { index = "OFF"; }
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "ConfigureCDRONOFF is" + index);
                    return MyIO.WriteString("SENS2:Frequency:CDR " + index);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("SENS2:Frequency:CDR " + index);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("SENS2:Frequency:CDR?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == index)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "ConfigureCDRONOFF is" + index);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "ConfigureCDRONOFF wrong");

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
        private bool ConfigureCDRFreq(string CDRFreq, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "CDRFrequence is" + CDRFreq);
                    return MyIO.WriteString("sens1:frequency " + CDRFreq);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString("sens1:frequency " + CDRFreq);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString("sens1:frequency?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == CDRFreq)
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "CDRFrequence is" + CDRFreq);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set CDRFrequence wrong");

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
        public override double GetErrorRate(int syn=0)
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

        public override double RapidErrorRate(int syn = 0)
        {
            return 1;
        
        }
    }
}
