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
        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] N490XPPGStruct)
        {
            lock (syncRoot)
            {
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(N490XPPGStruct, "ADDR", out i))
                    {
                        Addr = Convert.ToByte(N490XPPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ADDR");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "IOTYPE", out i))
                    {
                        IOType = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(N490XPPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "TRIGGERDRATIO", out i))
                    {
                        TriggerDRatio = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TRIGGERDRATIO");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "TRIGGERMODE", out i))
                    {
                        TriggerMode = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TRIGGERMODE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "CLOCKHIGVOLTAGE", out i))
                    {
                        ClockHigVoltage = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no CLOCKHIGVOLTAGE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "DATALOWVOLTAGE", out i))
                    {
                        DataLowVoltage = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DATALOWVOLTAGE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "DATAHIGVOLTAGE", out i))
                    {
                        DataHigVoltage = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DATAHIGVOLTAGE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "CLOCKLOWVOLTAGE", out i))
                    {
                        ClockLowVoltage = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no CLOCKLOWVOLTAGE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "BERTDATARATE", out i))
                    {
                        BertDataRate = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no BERTDATARATE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "PRBS", out i))
                    {
                        PRBS = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no PRBS");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XPPGStruct, "NAME", out i))
                    {
                        Name = N490XPPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
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
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
       
        public override bool Connect()
        {
            lock (syncRoot)
            {
                try
                {
                    // IO_Type

                    switch (IOType)
                    {
                        case "GPIB":
                            EquipmentConnectflag = true;
                            break;
                        default:
                            Log.SaveLogToTxt("GPIB类型错误");
                            EquipmentConnectflag = false;
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
        }

        private bool ConfigureDataRate(string datarate,int syn=0)
        {
            lock (syncRoot)
            {
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                bool flag = false;
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("datarate is" + BertDataRate);
                        return this.WriteString("source9:frequency " + datarate);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("source9:frequency " + datarate);
                            System.Threading.Thread.Sleep(2000);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("source9:frequency?");
                                readtemp = this.ReadString();
                                if (readtemp == datarate)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("datarate is" + BertDataRate);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("ConfigureDataRate wrong");

                            }

                        }
                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }

        private bool ConfigurePRBS(string PRBS, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                bool flag = false;
                try
                {

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PRBS is" + PRBS);
                        return this.WriteString("source1:pattern:select PRBS" + PRBS);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("source1:pattern:select PRBS" + PRBS);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("source1:pattern:select?");
                                readtemp = this.ReadString();
                                if (readtemp == "PRBS" + PRBS)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PRBS is" + PRBS);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set PRBS wrong");

                            }

                        }
                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }

        private bool ConfigureDataVoltage(string DataHigVoltage, string DataLowVoltage, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("DataHigVoltage is" + DataHigVoltage + "DatalowVoltage is" + DataLowVoltage);
                        flag1 = this.WriteString("source1:voltage:High " + DataHigVoltage);
                        flag2 = this.WriteString("source1:voltage:Low " + DataLowVoltage);
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("source1:voltage:High " + DataHigVoltage);
                            flag2 = this.WriteString("source1:voltage:Low " + DataLowVoltage);
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("source1:voltage:High?");
                                readtemp = this.ReadString();
                                if (readtemp == DataHigVoltage)
                                    break;
                            }
                            for (j = 0; j < 3; j++)
                            {

                                this.WriteString("source1:voltage:Low?");
                                readtemp = this.ReadString();
                                if (readtemp == DataLowVoltage)
                                    break;
                            }
                            if ((k <= 3) && (j <= 3))
                            {
                                Log.SaveLogToTxt("DataHigVoltage is" + DataHigVoltage + "DatalowVoltage is" + DataLowVoltage);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("ConfigureDataVoltage wrong");

                            }

                        }
                        return flag;

                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        private bool ConfigureClockVoltage(string ClockHigVoltage, string ClockLowVoltage, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("ClockHigVoltage is" + ClockHigVoltage + "ClocklowVoltage is" + ClockLowVoltage);
                        flag1 = this.WriteString("source2:voltage:Low " + ClockLowVoltage);
                        flag2 = this.WriteString("source2:voltage:High " + ClockHigVoltage);
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("source2:voltage:Low " + ClockLowVoltage);
                            flag2 = this.WriteString("source2:voltage:High " + ClockHigVoltage);
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("source2:voltage:Low?");
                                readtemp = this.ReadString();
                                if (readtemp == ClockLowVoltage)
                                    break;
                            }
                            for (j = 0; j < 3; j++)
                            {

                                this.WriteString("source2:voltage:High?");
                                readtemp = this.ReadString();
                                if (readtemp == ClockHigVoltage)
                                    break;
                            }
                            if ((k <= 3) && (j <= 3))
                            {
                                Log.SaveLogToTxt("ClockHigVoltage is" + ClockHigVoltage + "ClocklowVoltage is" + ClockLowVoltage);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("ConfigureClockVoltage wrong");

                            }

                        }
                        return flag;


                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        private bool ConfigureTriggerMode(string PGTriggerMode, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {


                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("TriggerMode is" + TriggerMode);
                        return this.WriteString("source3:trigger:mode " + PGTriggerMode);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("source3:trigger:mode " + PGTriggerMode);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("source3:trigger:mode?");
                                readtemp = this.ReadString();
                                if (readtemp == PGTriggerMode)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("TriggerMode is" + TriggerMode);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set TriggerMode wrong");

                            }

                        }
                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        private bool ConfigureTriggerDRatio(string TriggerDRatio, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {


                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("TriggerDRatio is" + TriggerDRatio);
                        return this.WriteString("source3:trigger:DCDRatio " + TriggerDRatio);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("source3:trigger:DCDRatio " + TriggerDRatio);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("source3:trigger:CTDRatio?");
                                readtemp = this.ReadString();
                                if (readtemp == TriggerDRatio)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("TriggerDRatio is" + TriggerDRatio);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set TriggerDRatio wrong");

                            }

                        }
                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        
        
    }
    public class N490XED : ErrorDetector
    {
        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization
        public override bool Initialize(TestModeEquipmentParameters[] N490XEDStruct)
        {
            lock (syncRoot)
            {
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(N490XEDStruct, "ADDR", out i))
                    {
                        Addr = Convert.ToByte(N490XEDStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ADDR");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XEDStruct, "IOTYPE", out i))
                    {
                        IOType = N490XEDStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XEDStruct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(N490XEDStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XEDStruct, "CDRSWITCH", out i))
                    {
                        CDRSwitch = Convert.ToBoolean(N490XEDStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no CDRSWITCH");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XEDStruct, "CDRFREQ", out i))
                    {
                        CDRFreq = N490XEDStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no CDRFREQ");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XEDStruct, "PRBS", out i))
                    {
                        PRBS = N490XEDStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no PRBS");
                        return false;
                    }
                    if (Algorithm.FindFileName(N490XEDStruct, "NAME", out i))
                    {
                        Name = N490XEDStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
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
        }



        public override bool Configure(int syn = 0)
        {
            lock (syncRoot)
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
            lock (syncRoot)
            {
                try
                {
                    // IO_Type

                    switch (IOType)
                    {
                        case "GPIB":

                            EquipmentConnectflag = true;
                            break;
                        default:
                            Log.SaveLogToTxt("GPIB类型错误");
                            EquipmentConnectflag = false;
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

        private bool ConfigurePRBS(string PRBS, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                bool flag = false;
                try
                {

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PRBS is" + PRBS);

                        return this.WriteString("source1:pattern:select PRBS" + PRBS);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("source1:pattern:select PRBS" + PRBS);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("source1:pattern:select?");
                                readtemp = this.ReadString();
                                if (readtemp == "PRBS" + PRBS)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PRBS is" + PRBS);

                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set PRBS wrong");

                            }

                        }
                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool AutoAlaign(bool becenter)
        {
            lock (syncRoot)
            {
                string temp = null;
                int isin = 0;
                try
                {
                    if (becenter)
                    {
                        if (this.WriteString("sense1:eye:tcenter 1"))
                        {
                            byte i = 0;
                            do
                            {
                                this.WriteString("sense1:eye:tcenter?");
                                Thread.Sleep(300);
                                temp = this.ReadString(19);
                                Thread.Sleep(300);
                                isin = temp.IndexOf("SUCCESSFUL");
                                i++;
                            } while (i < 20 && isin == -1);
                            this.WriteString("sense1:eye:align:auto " + "1");
                            i = 0;
                            temp = null;
                            do
                            {
                                this.WriteString("sense1:eye:align:auto?");
                                Thread.Sleep(200);
                                temp = this.ReadString(19);
                                Thread.Sleep(200);
                                isin = temp.IndexOf("SUCCESSFUL");
                                i++;
                            } while (i < 30 && isin == -1);
                            this.WriteString("sense1:eye:align:auto " + "0");
                            if (isin != -1)
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
                        this.WriteString("sense1:eye:align:auto " + "1");
                        i = 0;
                        temp = null;
                        do
                        {
                            this.WriteString("sense1:eye:align:auto?");
                            temp = this.ReadString(19);
                            isin = temp.IndexOf("SUCCESSFUL");
                            i++;
                        } while (i < 30 && isin == -1);
                        this.WriteString("sense1:eye:align:auto " + "0");
                        if (temp == "SUCCESSFUL")
                        {
                            return true;
                        }
                        else return false;
                    }

                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        private bool ConfigureCDRONOFF(bool Switch, int syn = 0)
        {
            lock (syncRoot)
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
                        Log.SaveLogToTxt("ConfigureCDRONOFF is" + index);
                        return this.WriteString("SENS2:Frequency:CDR " + index);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("SENS2:Frequency:CDR " + index);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("SENS2:Frequency:CDR?");
                                readtemp = this.ReadString();
                                if (readtemp == index)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("ConfigureCDRONOFF is" + index);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("ConfigureCDRONOFF wrong");

                            }

                        }
                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        private bool ConfigureCDRFreq(string CDRFreq, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("CDRFrequence is" + CDRFreq);
                        return this.WriteString("sens1:frequency " + CDRFreq);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString("sens1:frequency " + CDRFreq);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString("sens1:frequency?");
                                readtemp = this.ReadString();
                                if (readtemp == CDRFreq)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("CDRFrequence is" + CDRFreq);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set CDRFrequence wrong");

                            }

                        }
                        return flag;
                    }
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override double GetErrorRate(int syn=0)
        {
            lock (syncRoot)
            {
                double ErrorRate = 0;
                string strErrorRate = "1";
                try
                {

                    this.WriteString("sense1:gate:state ON");
                    Thread.Sleep(1000);
                    this.WriteString("sense1:gate:state OFF");
                    this.WriteString("fetch:sense1:eratio:total?");
                    strErrorRate = this.ReadString(32);
                }
                catch (Exception error)
                {
                    strErrorRate = "2";
                    Log.SaveLogToTxt(error.ToString());
                    return ErrorRate;
                }
                return ErrorRate = double.Parse(strErrorRate);
            }
        }

        public override double RapidErrorRate(int syn = 0)
        {
            return 1;
        
        }
    }
}
