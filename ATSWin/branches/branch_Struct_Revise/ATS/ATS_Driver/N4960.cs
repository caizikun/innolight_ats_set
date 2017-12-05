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
    public class N4960PPG : PPG
    {
        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization

        private double DataOutPutAmp, DataOutPutAtt;



        public override bool Initialize(TestModeEquipmentParameters[] N4960PPGStruct)
        {
            lock (syncRoot)
            {
                int i = 0;
                try
                {
                    if (Algorithm.FindFileName(N4960PPGStruct, "ADDR", out i))
                    {
                        Addr = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no ADDR");
                        return false;
                    }
                    if (Algorithm.FindFileName(N4960PPGStruct, "IOTYPE", out i))
                    {
                        IOType = N4960PPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N4960PPGStruct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(N4960PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }
                    if (Algorithm.FindFileName(N4960PPGStruct, "Name", out i))
                    {
                        Name = N4960PPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
                        return false;
                    }
                    if (Algorithm.FindFileName(N4960PPGStruct, "DataRate", out i))
                    {
                        dataRate = N4960PPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DATARATE");
                        return false;
                    }
                    if (Algorithm.FindFileName(N4960PPGStruct, "DataOutPutAmp", out i))
                    {
                        DataOutPutAmp = Convert.ToDouble(N4960PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DataOutPutAmp");
                        return false;
                    }
                    if (Algorithm.FindFileName(N4960PPGStruct, "DataOutPutAtt", out i))
                    {
                        DataOutPutAtt = Convert.ToDouble(N4960PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DataOutPutAmp");
                        return false;
                    }
                    if (Algorithm.FindFileName(N4960PPGStruct, "PrbsLength", out i))
                    {
                        prbsLength = Convert.ToByte(N4960PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no PrbsLength");
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
      
        public bool ReSet()
        {
            //:HEADER OFF;*ESE 60;*SRE 48;*CLS;
            lock (syncRoot)
            {
                if (this.WriteString(":HEADER OFF") &&
                 this.WriteString("*ESE 60") &&
                 this.WriteString("*SRE 48") &&
                 this.WriteString("*CLS"))
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
                        
                            lock (syncRoot)
                            {
                                this.WriteString("*IDN?");
                                string content = this.ReadString();
                                this.EquipmentConnectflag = content.Contains("N4960");
                            }                       

                        //this.WriteString("*IDN?");
                        //string ss = this.ReadString();
                        //EquipmentConnectflag = ss.Contains("N4960");
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
                        //ReSet();
                        if (Reset == true)
                        {
                            ReSet();
                        }
                        ConfigureDataRate();
                        SetPRBS();

                        DataOutPutAmp = DataOutPutAmp / (Math.Pow(10, DataOutPutAtt / 20));//add attenuation
                        ConfigureDataAmplitude(DataOutPutAmp, 0);

                        ConfigureOutputSwitch(1, syn);
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
   
        private bool ConfigureDataRate(int syn = 0)//PPG比特率，单位为Gbps.
        {
            //bool flag = false;
            //bool flag1 = false;
            //int k = 0;
            lock (syncRoot)
            {
                //string readtemp = "";
                try
                {

                    Decimal dData = 0.0M;

                    if (dataRate.Contains("E"))
                    {
                        dData = Convert.ToDecimal(Decimal.Parse(dataRate.ToString(), System.Globalization.NumberStyles.Float));
                    }
                    else
                    {
                        dData = Convert.ToDecimal(dataRate);
                    }



                    Log.SaveLogToTxt("N4960 dataRate is" + dataRate);
                    return this.WriteString(":SOUR:FREQ " + dData / 2);

                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }

        public override bool OutPutSwitch(bool Switch, int syn = 0)// 不让信号关闭
        {
            lock (syncRoot)
            {
                string StrSwitch;

                //if (Switch)
                //{
                //    StrSwitch = "ON";
                //}
                //else
                //{
                //    StrSwitch = "OFF";
                //}

                StrSwitch = "ON";
                this.WriteString(":OUTS:OUTP " + StrSwitch);//divided clock outputs
                this.WriteString(":OUTD:OUTP " + StrSwitch);//delayed clock outputs
                this.WriteString(":PG:DATA:OUTP " + StrSwitch);//PPG
                return true;
            }
        }

        private bool SetAmplitude(double Amplitude)//单位是V
        {
            lock (syncRoot)
            {
                this.WriteString(":PG:DATA:LLEVel:AMPLitude" + Amplitude + "V");
                return true;
            }
        }

        private bool SetPRBS()//单位是V
        {
            // :PG:DATA:PATT:NAME ///":PG:DATA:PATT:NAME PRBS2^9-1; *OPC?"
         //   this.WriteString(":PG:DATA:PATT:NAME PRBS2^" + prbsLength + "-1" + "(@1)");
            //:PG:DATA:PATT:NAME PRBS2^31-1(@0)
           // this.WriteString(":PG:DATA:PATT:NAME? (@1)");
            lock (syncRoot)
            {
                this.WriteString(":PG:DATA:PATT:NAME PRBS2^" + prbsLength + "-1; *OPC?");
                string Str = this.ReadString();
                if (Str.Contains("PRBS2^" + prbsLength + "-1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool ConfigureETxPolarity(bool polarity)
        {
            //:PG:DATA:PATT:POL INV
            lock (syncRoot)
            {
                bool flag = false;
                string strInvert;

                switch (polarity)
                {
                    case false:
                        strInvert = "INVert";
                        break;
                    case true:
                        strInvert = "NONInvert";
                        break;
                    default:
                        strInvert = "NONInvert";
                        break;
                }

                try
                {

                    Log.SaveLogToTxt("Sets the pattern polarity to " + strInvert);
                    return this.WriteString(":PG:DATA:PATT:POL " + strInvert);

                }
                catch (Exception ex)
                {
                    Log.SaveLogToTxt(ex.ToString());
                    return false;
                }
            }
        }

        public override bool ConfigureOTxPolarity(bool polarity)
        {
            lock (syncRoot)
            {
                return ConfigureETxPolarity(polarity);
            }
        }

        private bool ConfigureDataTracking(byte dataTrackingSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strDataTrackingSwitch;
                switch (dataTrackingSwitch)
                {
                    case 0:
                        strDataTrackingSwitch = "OFF";
                        break;
                    case 1:
                        strDataTrackingSwitch = "ON";
                        break;
                    default:
                        strDataTrackingSwitch = "ON";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("DataTracking is" + strDataTrackingSwitch);
                        return this.WriteString(":OUTPut:DATA:TRACking " + strDataTrackingSwitch);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:TRACking " + strDataTrackingSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:TRACking?");
                                readtemp = this.ReadString();
                                if (readtemp == dataTrackingSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataTracking is" + strDataTrackingSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("DataTracking wrong");

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
        }//0=TrcakingOff,1=TrackingON

        private bool ConfigureDataLevelGuardAmpMax(double ampMax, int syn = 0)//ampMax单位为mV
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
                        Log.SaveLogToTxt("DataLevelGuardAmpMax is" + (ampMax / 1000).ToString());

                        return this.WriteString(":OUTPut:DATA:LIMitter:AMPLitude " + (ampMax / 1000).ToString());
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LIMitter:AMPLitude " + (ampMax / 1000).ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LIMitter:AMPLitude?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble((ampMax / 1000).ToString()))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelGuardAmpMax is" + (ampMax / 1000).ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelGuardAmpMax wrong");

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

        private bool ConfigureDataLevelGuardOffset(double offsetMax, double offsetMin, int syn = 0)
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
                        Log.SaveLogToTxt("DataLevelGuardoffsetMax is" + (offsetMax / 1000).ToString() + "DataLevelGuardoffsetMin is" + (offsetMin / 1000).ToString());
                        return this.WriteString(":OUTPut:DATA:LIMitter:OFFSet " + (offsetMax / 1000).ToString() + "," + (offsetMin / 1000).ToString());


                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LIMitter:OFFSet " + (offsetMax / 1000).ToString() + "," + (offsetMin / 1000).ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LIMitter:OFFSet?");
                                readtemp = this.ReadString();
                                double temp1 = Convert.ToDouble((offsetMax / 1000));
                                double temp2 = Convert.ToDouble((offsetMin / 1000));
                                if (readtemp == temp1.ToString("0.000") + "," + temp2.ToString("0.000") + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelGuardoffsetMax is" + (offsetMax / 1000).ToString() + "DataLevelGuardoffsetMin is" + (offsetMin / 1000).ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelGuardoffset wrong");

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
        }//形参单位全部为mV

        private bool ConfigureDataLevelGuardSwitch(byte lvGuardSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strLvGuardSwitch;
                switch (lvGuardSwitch)
                {
                    case 0:
                        strLvGuardSwitch = "OFF";
                        break;
                    case 1:
                        strLvGuardSwitch = "ON";
                        break;
                    default:
                        strLvGuardSwitch = "ON";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("DataLevelGuardSwitch is" + strLvGuardSwitch);
                        return this.WriteString(":OUTPut:DATA:LEVGuard " + strLvGuardSwitch);

                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LEVGuard " + strLvGuardSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LEVGuard?");
                                readtemp = this.ReadString();
                                if (readtemp == lvGuardSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelGuardSwitch is" + strLvGuardSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelGuardSwitch wrong");

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

        private bool ConfigureDataAcModeSwitch(byte acSwitch, int syn = 0)//0=DC Mode，1=AC Mode
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strAcSwitch;
                switch (acSwitch)
                {
                    case 0:
                        strAcSwitch = "OFF";
                        break;
                    case 1:
                        strAcSwitch = "ON";
                        break;
                    default:
                        strAcSwitch = "ON";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("DataAcModeSwitch is" + strAcSwitch);
                        return this.WriteString(":OUTPut:DATA:AOFFset " + strAcSwitch);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:AOFFset " + strAcSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:AOFFset?");
                                readtemp = this.ReadString();
                                if (readtemp == acSwitch.ToString())
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataAcModeSwitch is" + strAcSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataAcModeSwitch wrong");

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

        private bool ConfigureDataLevelMode(byte elecLevelMode, int syn = 0)//0=VARiable,1=NECL,2=PCML,3=NCML,4=SCFL,5=LVPecl,6=LVDS200,7=LVDS400
        {//无6 7选项,mode选择与levelguard范围有关
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strElecLevelMode;
                switch (elecLevelMode)
                {
                    case 0:
                        strElecLevelMode = "VAR";
                        break;
                    case 1:
                        strElecLevelMode = "NECL";
                        break;
                    case 2:
                        strElecLevelMode = "PCML";
                        break;
                    case 3:
                        strElecLevelMode = "NCML";
                        break;
                    case 4:
                        strElecLevelMode = "SCFL";
                        break;
                    case 5:
                        strElecLevelMode = "LVP";
                        break;
                    //case 6:
                    //    strElecLevelMode = "LVDS200";
                    //    break;
                    //case 7:
                    //    strElecLevelMode = "LVDS400";
                    //    break;
                    default:
                        strElecLevelMode = "VAR";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("DataLevelMode is" + strElecLevelMode);
                        return this.WriteString(":OUTPut:DATA:LEVel DATA," + strElecLevelMode);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:LEVel DATA," + strElecLevelMode);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:LEVel? DATA");
                                readtemp = this.ReadString();
                                if (readtemp == strElecLevelMode + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataLevelMode is" + strElecLevelMode);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataLevelMode wrong");

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

        private bool ConfigureDataAmplitude(double amplitude, int syn = 0)//单位为mV
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
                    {//:PG:DATA:LLEV:AMPL 0.500000(@1)
                        Log.SaveLogToTxt("DataAmplitude is" + amplitude.ToString());
                        //":PG :DATA:LLEV:AMPL 0.5"
                        // return this.WriteString(":PG:DATA:LLEV:AMPL " + amplitude + "(@1)");
                        //return this.WriteString(":PG:DATA:LLEV:AMPL " + amplitude);//:PG:DATA:LLEV:AMPL 1.3
                        this.WriteString(":PG:DATA:LLEV:AMPL " + amplitude.ToString("f3"));
                        Thread.Sleep(100);
                        this.WriteString(":PG:DATA:LLEV:AMPL?");
                        readtemp = this.ReadString(5);

                        if (Math.Abs(Convert.ToDouble(readtemp) - (amplitude / 1000)) < 50)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else//操作手册无这些指令
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:AMPLitude DATA," + (amplitude / 1000).ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:AMPLitude? DATA");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble((amplitude / 1000).ToString() + "\n"))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataAmplitude is" + (amplitude / 1000).ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataAmplitude wrong");

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

        private bool ConfigureDataCrossPoint(double crossPoint, int syn = 0)
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
                        Log.SaveLogToTxt("DataCrossPoint is" + crossPoint.ToString());
                        return this.WriteString(":OUTPut:DATA:CPOint DATA," + crossPoint.ToString());
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:DATA:CPOint DATA," + crossPoint.ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:DATA:CPOint? DATA");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(crossPoint.ToString() + "\n"))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("DataCrossPoint is" + crossPoint.ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set DataCrossPoint wrong");

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

        private bool ConfigureClockSwitch(byte clkSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string strClkSwitch;
                switch (clkSwitch)
                {
                    case 0:
                        strClkSwitch = "OFF";
                        break;
                    case 1:
                        strClkSwitch = "ON";
                        break;
                    default:
                        strClkSwitch = "ON";
                        break;
                }
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("ClockSwitch is" + strClkSwitch);
                        return this.WriteString(":OUTPut:CLOCk:OUTPut " + strClkSwitch);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTPut:CLOCk:OUTPut " + strClkSwitch);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTPut:CLOCk:OUTPut?");
                                readtemp = this.ReadString();
                                if (readtemp == clkSwitch.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("ClockSwitch is" + strClkSwitch);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set ClockSwitch wrong");

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

        private bool ConfigureOutputSwitch(byte outSwitch, int syn = 0)// 不让信号关闭
        {
            lock (syncRoot)
            {
                //bool flag = false;
                //bool flag1 = false;
                //int k = 0;
                //string readtemp = "";
                string strOutSwitch;
                //switch (outSwitch)
                //{
                //    case 0:
                //        strOutSwitch = "OFF";
                //        break;
                //    case 1:
                //        strOutSwitch = "ON";
                //        break;
                //    default:
                //        strOutSwitch = "ON";
                //        break;
                //}
                strOutSwitch = "ON";
                try
                {

                    Log.SaveLogToTxt("OutputSwitch is" + strOutSwitch);
                    //N4960A.write(":OUTS:OUTP ON")
                    //N4960A.write(":OUTD:OUTP ON")
                    // N4960A.write(":OUTJ:OUTP ON")
                    //  this.WriteString(":OUTJ:OUTP ON");

                    this.WriteString(":OUTD:OUTP " + strOutSwitch);//DeleyOutPut
                    this.WriteString(":OUTS:OUTP " + strOutSwitch);//divided clock outputs:",:OUTS:OUTP ON
                    this.WriteString(":PG:DATA:OUTP " + strOutSwitch);//DataOutput

                    return true;




                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
    }


    public class N4960ED : ErrorDetector
    {
        class PersonAttribute : CommandInf
        {
            //public int dataRate;
            public byte prbsLength;
            public int GatingTime;
        }

        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization
        private PersonAttribute MyPersonAttribute=new PersonAttribute();

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
                        //ReSet();

                        EquipmentConfigflag = SetPRBS();

                    }
                    return EquipmentConfigflag;

                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }

        public override bool Initialize(TestModeEquipmentParameters[] N4960Struct)
        {
            lock (syncRoot)
            {
                int i = 0;
                if (Algorithm.FindFileName(N4960Struct, "ADDR", out i))
                {
                    MyPersonAttribute.Addr = Convert.ToByte(N4960Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(N4960Struct, "IOTYPE", out i))
                {
                    MyPersonAttribute.IOType = N4960Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }

                if (Algorithm.FindFileName(N4960Struct, "Name", out i))
                {
                    MyPersonAttribute.Name = N4960Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }
                //if (Algorithm.FindFileName(N4960PPGStruct, "DataRate", out i))
                //{
                //    MyPersonAttribute.dataRate =Convert.ToInt32( N4960PPGStruct[i].DefaultValue);
                //}
                //else
                //{
                //    Log.SaveLogToTxt("there is no DATARATE");
                //    return false;
                //}

                if (Algorithm.FindFileName(N4960Struct, "PrbsLength", out i))
                {
                    MyPersonAttribute.prbsLength = Convert.ToByte(N4960Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no PrbsLength");
                    return false;
                }
                if (Algorithm.FindFileName(N4960Struct, "GatingTime ", out i))
                {
                    MyPersonAttribute.GatingTime = Convert.ToInt16(N4960Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no GatingTime");
                    return false;
                }
                if (!Connect()) return false;
                return true;
            }
        }

        public override bool Connect()
        {
            lock (syncRoot)
            {
                try
                {
                    // IO_Type

                    switch (MyPersonAttribute.IOType)
                    {
                        case "GPIB":
                            lock (syncRoot)
                            {
                                this.WriteString("*IDN?");
                                string content = this.ReadString();
                                this.EquipmentConnectflag = content.Contains("N4960");
                            }
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

        private bool SetPRBS()//单位是V
        {
            lock (syncRoot)
            {
                // :PG:DATA:PATT:NAME ///":PG:DATA:PATT:NAME PRBS2^9-1; *OPC?"
                //   this.WriteString(":PG:DATA:PATT:NAME PRBS2^" + prbsLength + "-1" + "(@1)");
                //:PG:DATA:PATT:NAME PRBS2^31-1(@0)
                // this.WriteString(":PG:DATA:PATT:NAME? (@1)");//(":ED:DATA:PATT:NAME PRBS2^9-1; *OPC?")
                this.WriteString(":ED:DATA:PATT:NAME PRBS2^" + MyPersonAttribute.prbsLength + "-1; *OPC?");
                string Str = this.ReadString();
                if (Str.Contains("1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool AutoAlaign(bool becenter)
        {
            lock (syncRoot)
            {
                for (int i = 0; i < 10; i++)
                {
                    bool result = this.WriteString(":ED:DATA:AALign:EXECute");
                    Thread.Sleep(200);
                    /// 清空数据
                    result = result && this.WriteString(":ED:DATA:ACC:CLR");
                    if (result == true)// 清除累积的误码结果
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public override double GetErrorRate(int syn = 0)
        {
            lock (syncRoot)
            {
                this.WriteString(":ED:DATA:ACC:CLR");
                double ErrCount = 0;
                double bitCount = 0;
                string state = "";
                ///执行ALIGN
                //  this.WriteString(":ED:DATA:AALign:EXECute");
                /// 清空数据
                this.WriteString(":ED:DATA:ACC:CLR");// 清除累积的误码结果
                /// 启动误码测试
                this.WriteString(":ED:DATA:ACCumulation:STARt");
                Thread.Sleep(MyPersonAttribute.GatingTime * 1000);
                /// 读取次数
                int readTime = 0;
                do
                {
                    this.WriteString(":ED:DATA:ACC:RES:ALL?");
                    string result_read = this.ReadString();
                    if (!string.IsNullOrEmpty(result_read))
                    {
                        string[] strs = result_read.Split(',');
                        bitCount = Convert.ToDouble(strs[0]);
                        ErrCount = Convert.ToDouble(strs[1]);
                        state = strs[4];
                    }
                    readTime++;
                }
                while (ErrCount <= 6 && state.ToLower().Trim() == "run" && readTime < 3);
                if (state.ToLower().Contains("run"))
                {
                    this.WriteString(":ED:DATA:ACC:STOP");
                }
                else
                {
                    return 1;
                }
                if (ErrCount <= 6)
                {
                    return 0;
                }
                return (ErrCount / bitCount);
            }
        }

        public override double RapidErrorRate(int syn = 0)
        {
            lock (syncRoot)
            {
                return this.GetErrorRate(syn);
            }
        }
        public override double[] RapidErrorCount_AllCH(int syn = 0, bool IsClear = false)
        {
            lock (syncRoot)
            {
                if (IsClear)
                {

                    this.WriteString(":ED:DATA:ACC:CLR");
                    this.WriteString(":ED:DATA:ACC:CLR");// 清除累积的误码结果
                }
                double[] ErrCountArray = new double[1];
                double ErrCount = 0;
                double bitCount = 0;
                string state = "";
                ///执行ALIGN
                //  this.WriteString(":ED:DATA:AALign:EXECute");
                /// 清空数据

                /// 启动误码测试
                this.WriteString(":ED:DATA:ACCumulation:STARt");
                Thread.Sleep(MyPersonAttribute.GatingTime * 1000);
                /// 读取次数
                int readTime = 0;

                this.WriteString(":ED:DATA:ACC:RES:ALL?");
                string result_read = this.ReadString();
                if (!string.IsNullOrEmpty(result_read))
                {
                    string[] strs = result_read.Split(',');
                    bitCount = Convert.ToDouble(strs[0]);
                    ErrCount = Convert.ToDouble(strs[1]);
                    state = strs[4];
                }
                readTime++;


                if (state.ToLower().Contains("run"))
                {
                    this.WriteString(":ED:DATA:ACC:STOP");
                }
                else
                {
                    ErrCount = 1E+9;
                }
                if (ErrCount <= 6)
                {
                    ErrCount = 1E+9;
                }
                ErrCountArray[0] = ErrCount;
                return ErrCountArray;
            }
        }
    }
}