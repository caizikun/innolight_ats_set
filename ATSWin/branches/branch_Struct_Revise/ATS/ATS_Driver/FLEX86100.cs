using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using System.Text.RegularExpressions;
using ATS_Framework;
using System.Windows.Forms;
using System.IO;

namespace ATS_Driver
{
    public class FLEX86100 : Scope
    {
        private static object syncRoot = new object();//used for thread synchronization

       // public IOPort MyIO;
        protected string[] dcacurrentchannel = new string[4]; 
        public string[] strOpticalChannel = new string[4];
        public string[] strElecChannel = new string[4];
        private double MaskValue;

        public override bool Initialize(TestModeEquipmentParameters[] FLEX86100Struct)
        {
            try
            {
               
                int i = 0;
                if (Algorithm.FindFileName(FLEX86100Struct,"ADDR",out i))
                {
                    Addr =Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"IOTYPE",out i))
                {
                    IOType = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"NAME",out i))
                {
                    Name = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"CONFIGFILEPATH",out i))
                {
                    configFilePath = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no CONFIGFILEPATH");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FLEXDCADATARATE",out i))
                {
                    FlexDcaDataRate = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXDCADATARATE");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FILTERSWITCH",out i))
                {
                    FilterSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no FILTERSWITCH");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FLEXFILTERFREQ",out i))
                {
                    FlexFilterFreq = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXFILTERFREQ");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"TRIGGERSOURCE",out i))
                {
                    triggerSource = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no TRIGGERSOURCE");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FLEXTRIGGERBWLIMIT",out i))
                {
                    FlexTriggerBwlimit = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXTRIGGERBWLIMIT");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"OPTICALSLOT",out i))
                {
                    opticalSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTICALSLOT");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"ELECSLOT",out i))
                {
                    elecSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ELECSLOT");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FLEXDCAWAVELENGTH",out i))
                {
                    FlexDcaWavelength = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXDCAWAVELENGTH");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"OPTICALATTSWITCH",out i))
                {
                    opticalAttSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTICALATTSWITCH");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"ERFACTORSWITCH",out i))
                {
                    erFactorSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ERFACTORSWITCH");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FLEXDCAATT",out i))
                {
                    FlexDcaAtt = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXDCAATT");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"ERFACTOR",out i))
                {
                    erFactor = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }

                else
                {
                    Log.SaveLogToTxt("there is no ERFACTOR");
                    return false;
                }
                //if (Algorithm.FindFileName(FLEX86100Struct,"FLEXSCALE",out i))
                //{
                //    FlexScale = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                //}
                //else
                //{
                //    Log.SaveLogToTxt("there is no FLEXSCALE");
                //    return false;
                //}
                //if (Algorithm.FindFileName(FLEX86100Struct,"FLEXOFFSET",out i))
                //{
                //    FlexOffset = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                //}
                //else
                //{
                //    Log.SaveLogToTxt("there is no FLEXOFFSET");
                //    return false;
                //}
                if (Algorithm.FindFileName(FLEX86100Struct,"THRESHOLD",out i))
                {
                    Threshold = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no THRESHOLD");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FLEXOPTCHANNEL",out i))
                {
                    FlexOptChannel = FLEX86100Struct[i].DefaultValue;
                    strOpticalChannel = FlexOptChannel.Split(',');
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXOPTCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"FLEXELECCHANNEL",out i))
                {
                    FlexElecChannel = FLEX86100Struct[i].DefaultValue;
                    strElecChannel = FlexElecChannel.Split(',');
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXELECCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASEMODULESLOT",out i))
                {
                    precisionTimebaseModuleSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no PRECISIONTIMEBASEMODULESLOT");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASESYNCHMETHOD",out i))
                {
                    precisionTimebaseSynchMethod = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no PRECISIONTIMEBASESYNCHMETHOD");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"RAPIDEYESWITCH",out i))
                {
                    rapidEyeSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RAPIDEYESWITCH");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"REFERENCE",out i))
                {
                    reference = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no REFERENCE");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"MARGINTYPE",out i))
                {
                    marginType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no MARGINTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"MARGINHITTYPE",out i))
                {
                    marginHitType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no MARGINHITTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"ACQLIMITTYPE",out i))
                {
                    acqLimitType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ACQLIMITTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASEREFCLK",out i))
                {
                    precisionTimebaseRefClk = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no PRECISIONTIMEBASEREFCLK");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"MARGINHITRATIO",out i))
                {
                    marginHitRatio = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no MARGINHITRATIO");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"OPTICALMASKNAME",out i))
                {
                    opticalMaskName = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTICALMASKNAME");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct, "OPTICALMASKNAME2", out i))
                {
                    opticalMaskName2 = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPTICALMASKNAME2");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"ELECMASKNAME",out i))
                {
                    elecMaskName = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no ELECMASKNAME");
                    return false;
                }
             
                if (Algorithm.FindFileName(FLEX86100Struct,"MARGINHITCOUNT",out i))
                {
                    marginHitCount = Convert.ToInt16(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no MARGINHITCOUNT");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct,"ACQLIMITNUMBER",out i))
                {
                    acqLimitNumber = Convert.ToInt16(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ACQLIMITNUMBER");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct, "FLEXSETSCALEDELAY", out i))
                {
                    flexsetscaledelay = Convert.ToInt32(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no FLEXSETSCALEDELAY");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct, "DIFFSWITCH", out i))
                {
                    DiffSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no DIFFSWITCH");
                    return false;
                }
                if (Algorithm.FindFileName(FLEX86100Struct, "BANDWIDTH", out i))
                {
                    BandWidth = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no BANDWIDTH");
                    return false;
                }
                if (!Connect()) return false;

            }

            catch (InnoExCeption error)
            {
                Log.SaveLogToTxt("ErrorCode="+  ExceptionDictionary.Code._Funtion_Fatal_0x05002 +"Reason=" +error.TargetSite.Name + "Fail");
                  throw error;
            }

            catch (Exception error)
            {

                Log.SaveLogToTxt("ErrorCode="+  ExceptionDictionary.Code._Funtion_Fatal_0x05002 +"Reason=" +error.TargetSite.Name + "Fail");
                throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                // throw new InnoExCeption(ex);
            }
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
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {


                    string offset = "";
                    CurrentChannel = channel;
                    if (offsetlist.ContainsKey(CurrentChannel))
                        offset = offsetlist[CurrentChannel];
                    double temp = Convert.ToDouble(offset);
                    Log.SaveLogToTxt("FLEX86100 Offset is" + offset);
                    SetAttenuation(strOpticalChannel[Convert.ToByte(CurrentChannel) - 1], temp, 1, syn);
                    SetAttenuation(strElecChannel[Convert.ToByte(CurrentChannel) - 1], temp, 1, syn);
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
                return true;
            }
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {


                    offsetlist.Add(channel, offset);
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {
                    // ExceptionDictionary.Code._0x05002
                    // InnoExCeption ex = new InnoExCeption(ExceptionDictionary.Code._0x02F04, error.StackTrace);
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    //  throw new InnoExCeption(ExceptionDictionary.Code._0x05002, error.StackTrace);

                    // throw new InnoExCeption(ex);
                }
                return true;
            }
        }
        override public bool Connect()
        {
            lock (syncRoot)
            {
                try
                {

                    switch (IOType)
                    {
                        case "GPIB":

                            lock (syncRoot)
                            {
                                this.WriteString("*IDN?");
                                string content = this.ReadString();
                                this.EquipmentConnectflag = content.Contains("86100");

                                if (!EquipmentConnectflag)
                                {
                                    Log.SaveLogToTxt(ExceptionDictionary.Code._UnConnect_0x05000 + "无法连接仪器");
                                    EquipmentConnectflag = false;
                                    throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);
                                }
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
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    EquipmentConnectflag = false;
                    throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);

                    //  return false;
                }
            }
        }
        /// <summary>
        /// 按照设备属性中的配置文件地址进行设备配置
        /// </summary>
        /// <returns></returns>
        private bool ConfigFromFile()
        {
            lock (syncRoot)
            {
                try
                {
                    if (EquipmentConfigflag) { return true; }
                    else
                    {
                        this.WriteString(":DISK:SETup:RECall \"" + configFilePath + "\"\n");
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
        public override bool Configure(int syn = 0)
        {
            lock (syncRoot)
            {
                List<string> ech = new List<string>();
                List<string> och = new List<string>();
                List<string> ptbch = new List<string>();

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

                        // PreSet
                        if (!ConfigurePreSet(syn))
                        {
                            return false;
                        }


                        // 设置Trigger
                        if (!ConfigureInputSignal(syn))
                        {
                            return false;
                        }

                        // 设置光眼
                        if (!ConfigureOpticalChannel(och, syn))
                        {
                            return false;
                        }

                        // 设置电眼
                        if (!ConfigureElecChannel(ech, syn))
                        {
                            return false;
                        }

                        RapidEyeSetup(FlexDcaDataRate, 1, syn);   //加载电眼配置文件会覆盖RapidEye速率

                        for (byte i = 0; i < 4; i++)
                        {
                            this.WriteString(":SYSTem:MODel? SLOT" + (i + 1).ToString());
                            if (!this.ReadString().Contains("Not Present"))
                            {
                                if (isModuleNeedCalibrate((byte)(i + 1))) CalibrateModel((byte)(i + 1));
                            }
                        }

                        // CommonSetting
                        if (!ConfigureCommonSetting(syn))
                        {
                            return false;
                        }
                        return true;
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

        public bool ConfigurePreSet(int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {


                    // this.WriteString(":SYSTem:DEFault");
                    this.WriteString(":ACQuire:STOP");
                    SetMode(0, syn);
                    this.WriteString(":DISPlay:ETUNing ON");
                    this.WriteString(":DISPlay:TOVerlap OFF");
                    RapidEyeSetup(FlexDcaDataRate, 1, syn);
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

                return true;
            }
        }

        public bool ConfigureInputSignal(int syn = 0)
        {
            lock (syncRoot)
            {
                string returnmod;
                bool useptb = false;

                try
                {
                    if (triggerSource == 0)// Trigger
                    {
                        // PrecisionTimebaseSetup(precisionTimebaseModuleSlot, precisionTimebaseRefClk, 0, precisionTimebaseSynchMethod, syn);
                        SetTimeBase(precisionTimebaseRefClk, 0, precisionTimebaseSynchMethod, syn);
                        TriggerSetup(triggerSource, FlexTriggerBwlimit, syn);//FrontPanel
                        //添加判断，使用PTB or86107A
                    }
                    else// FreeRun PTB
                    {
                        TriggerSetup(triggerSource, FlexTriggerBwlimit, syn);

                        #region//  查询仪器前面板支持接入PTB 信号
                        this.WriteString(":SYSTem:OPT?");
                        string returnptb = this.ReadString();
                        string temp = returnptb.ToUpper().Replace(" ", "");
                        useptb = temp.Contains("PTB");
                        #endregion

                        if (temp.Contains("PTB"))// 前面板有PTB 信号输入功能
                        {

                            PTBSetup(precisionTimebaseRefClk, 1, precisionTimebaseSynchMethod, syn);
                            // logger
                        }
                        else// 需要外接86107A模组
                        {
                            this.WriteString(":system:mod? slot" + precisionTimebaseModuleSlot);
                            returnmod = this.ReadString();

                            if (returnmod.ToUpper().Contains("86107A"))
                            {
                                PrecisionTimebaseSetup(precisionTimebaseModuleSlot, precisionTimebaseRefClk, 1, precisionTimebaseSynchMethod, syn);
                            }
                            else
                            {
                                Log.SaveLogToTxt("I Can't find 86107A");
                                return false;

                            }

                        }
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

                return true;
            }
        }

        public bool ConfigureOpticalChannel(List<string> och, int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {


                    for (byte i = 0; i < 4; i++)
                    {
                        if (!(och.Contains(strOpticalChannel[i])))
                        {
                            if (strOpticalChannel[i].ToUpper() != "0")
                            {
                                och.Add(strOpticalChannel[i]);
                                WavelengthSelect(strOpticalChannel[i], FlexDcaWavelength, syn);
                                FileterSelect(strOpticalChannel[i], FlexDcaDataRate);
                                FileterSwitch(strOpticalChannel[i], FilterSwitch, syn);
                                SetAttenuation(strOpticalChannel[i], FlexDcaAtt, opticalAttSwitch, syn);
                                SetERCorrectFactor(strOpticalChannel[i], erFactor, erFactorSwitch, syn);
                                ChannelDisplaySwitch(strOpticalChannel[i], 1, syn);
                                EyeScaleOffset(strOpticalChannel[i], FlexScale, FlexOffset, syn);


                                if (!LoadMask(strOpticalChannel[i], opticalMaskName, syn))
                                {
                                    // 载入模板出错，返回false
                                    return false;
                                }

                            }
                        }
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

                return true;
            }
        }

        public bool ConfigureElecChannel(List<string> ech, int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {


                    for (byte i = 0; i < 4; i++)
                    {
                        if (!(ech.Contains(strElecChannel[i])))
                        {
                            if (strElecChannel[i].ToUpper() != "0")
                            {
                                ech.Add(strElecChannel[i]);
                                DIFFWaveformSwitch(strElecChannel[i], DiffSwitch, syn);
                                BandwidthSet(strElecChannel[i], BandWidth, syn);

                                if (elecMaskName != "") // 若电眼图模板存在，则设置电眼图模板
                                {
                                    if (!LoadMask(strElecChannel[i], elecMaskName, syn))
                                    {
                                        // 载入模板出错，返回false
                                        return false;
                                    }
                                }
                            }
                        }
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
                return true;
            }
        }

        public bool ConfigureCommonSetting(int syn = 0)
        {

            lock (syncRoot)
            {
                try
                {


                    // 载入电眼校验文件
                    string Str1 = @":DISK:SETup:RECall ""D:\user files\setups\fixture_deskew_ODVT.setx""";
                    this.WriteString(Str1);

                    this.WriteString(":display:persistence cgrade");//眼图显示为 多彩
                    MessureThresholdSetup(Threshold, reference, syn);
                    MaskONOFF(false, syn);
                    MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
                    AcquisitionLimitTestSwitch(0, syn);
                    AcquisitionControl(0);
                    CenterEye();
                    EyeTuningDisplay(1);
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
                return true;
            }
        }

        /// <summary>
        /// 读取光功率单位uW
        /// </summary>
        /// <returns></returns>
        public override double GetAveragePowerWatt()
        {

            lock (syncRoot)
            {
                double power = 0;

                try
                {
                    power = ReadPower(0);
                    Log.SaveLogToTxt("AveragePowerWatt is" + power);
                    return power;
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
        public override double GetAveragePowerdbm()
        {
            lock (syncRoot)
            {
                double power = 0;

                try
                {
                    power = ReadPower(1);
                    Log.SaveLogToTxt("AveragePowerdbm is" + power);
                    return power;
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
        /// <summary>
        /// 读取ER单位dB
        /// </summary>
        /// <returns></returns>
        public override double GetEratio()
        {
            lock (syncRoot)
            {
                double er = 0;
                try
                {
                    er = ReadER(0);
                    Log.SaveLogToTxt("Eratio is" + er);
                    return er;
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
        public override bool SetMaskLimmit(int acqLimitNumber)
        {
            lock (syncRoot)
            {
                try
                {


                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, 0);
                    AcquisitionLimitTestSwitch(1, 0);
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
      public override int  GetMask()
      {
          lock (syncRoot)
          {
              // double CurrentMask = -100;
              int MaskArray = -1;
              double CurrentMask;
              int i = 0;
              try
              {
                  do
                  {
                      AcquisitionLimitTestSwitch(1, 0);
                      AcquisitionControl(0);
                      CurrentMask = ReadMaskMargin();
                      i++;
                  } while ((CurrentMask > 100 || CurrentMask < 0) && i < 3);

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

              if (CurrentMask > 100) //没抓到
              {
                  CurrentMask = -10;
              }
              return Convert.ToInt16(CurrentMask);
          }
        }

        /// <summary>
        /// 测试光眼图，测量结果以数组形式返回，依次为AP,ER,OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
        /// </summary>
        /// <returns></returns>
        public override bool OpticalEyeTest(out double[]testResults, int syn=0)
        {
            lock (syncRoot)
            {
                testResults = new double[12];
                int count = 0;
                bool isOk = false;
                bool SaveFlag1 = false;
                bool SaveFlag2 = false;
                int i;
                double MaskMargin1 = 0;
                double MaskMargin2 = 0;
                try
                {
                    EyeTuningDisplay(1);
                    string strChannel = "1A";
                    strChannel = strOpticalChannel[Convert.ToByte(CurrentChannel) - 1];
                    //  this.WriteString(":DISPlay:ETUNing OFF");

                    this.WriteString(":ACQuire:REYE ON");
                    this.WriteString(":DISPlay:TOVerlap OFF");
                    // SetAttenuation(strChannel, FlexDcaAtt, opticalAttSwitch);//channel change have setted
                    SetMode(0, syn);
                    AcquisitionControl(2);
                    AcquisitionLimitTestSwitch(0, syn);
                    MaskONOFF(false, syn);
                    DisplayClearAlllist();
                    DisplayRiseFallTime(1);
                    DisplayRiseFallTime(0);
                    DisplayJitter(1);
                    DisplayJitter(0);
                    DisplayCrossing();
                    DisplayOther(1);
                    DisplayER1(0);
                    DisplayPower(1);
                    DisplayEyeHeight();
                    DisplayBitRate();
                    // DISPL
                    AcquisitionControl(0);
                    AutoScale(syn);
                    ClearDisplay();
                    Thread.Sleep(2000);// 因为Mask 变得很大,在此时反复清屏,以求让眼图显示稳定
                    ClearDisplay();
                    Thread.Sleep(2000);
                    EyeTuningDisplay(0);// 关闭眼图刷新
                    Thread.Sleep(2000);
                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
                    count = 0;
                    do
                    {
                        AutoScale(syn);
                        ClearDisplay();
                        Thread.Sleep(2000);// 因为Mask 变得很大,在此时反复清屏,以求让眼图显示稳定
                        ClearDisplay();
                        Thread.Sleep(2000);

                        //AcquisitionLimitTestSwitch(1, syn);
                        //AcquisitionControl(2);
                        AcquisitionLimitTestSwitch(1, syn);
                        int time = acqLimitNumber / 100 * 7000;
                        Thread.Sleep(time);
                        AcquisitionControl(0);


                        testResults[0] = ReadPower(1);
                        testResults[1] = ReadER(0);
                        testResults[2] = ReadAMPLitude();
                        testResults[3] = GetCrossing();
                        testResults[4] = ReadJitter(0);
                        testResults[5] = ReadJitter(1);
                        testResults[6] = ReadRiseFallTime(0);
                        testResults[7] = ReadRiseFallTime(1);
                        testResults[8] = 0;
                        testResults[9] = ReadEyeHeight();
                        testResults[10] = ReadBitRate();

                        for (i = 0; i < testResults.Length; i++)
                        {
                            if (testResults[i] >= 10000000) isOk = false;
                        }

                        if (i >= testResults.Length)
                        {
                            isOk = true;

                            break;
                        }
                        else
                            Thread.Sleep(200);
                        count++;
                    } while (count++ < 3);

                    LoadMask(strChannel, opticalMaskName, syn);
                    MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                    MaskMargin1 = ReadMaskMargin();
                    testResults[8] = MaskMargin1;

                    if (isOk == true)
                    {

                        LoadMask(strChannel, opticalMaskName, syn);
                        MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                        MaskMargin1 = ReadMaskMargin();
                        testResults[8] = MaskMargin1;
                        for (int k = 0; k < 3; k++)
                        {
                            SaveFlag1 = SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram, 0, 0);
                            if (SaveFlag1) break;
                        }
                        MaskONOFF(false, 1);
                        if (opticalMaskName2.Substring(0, 1).ToUpper() == "C")
                        {

                            LoadMask(strChannel, opticalMaskName2, syn);
                            MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                            MaskMargin2 = ReadMaskMargin();
                            testResults[11] = MaskMargin2;
                            for (int k = 0; k < 3; k++)
                            {
                                SaveFlag2 = SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram, 0, 0);
                                if (SaveFlag2) break;
                            }
                            MaskONOFF(false, syn);
                        }
                        else
                        {
                            SaveFlag2 = true;
                        }
                    }
                    if (!SaveFlag1 || !SaveFlag2)
                    {
                        MessageBox.Show("眼图保存失败!!!");
                        Log.SaveLogToTxt("Save Eye Image Error");
                        isOk = false;
                    }
                    AcquisitionLimitTestSwitch(0, syn);
                    AcquisitionControl(0);
                    MaskONOFF(false, syn);
                    // this.WriteString(":DISPlay:ETUNing ON");
                    this.WriteString(":ACQuire:REYE OFF");//Leo 修改
                    return isOk;
                }
                catch (InnoExCeption error)
                {
                    this.WriteString(":ACQuire:REYE OFF");//Leo 修改
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {
                    this.WriteString(":ACQuire:REYE OFF");//Leo 修改
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
            }
        }
        /// <summary>
        /// 测试电眼图，测量结果以数组形式返回，依次为OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
        /// </summary>
        /// <returns></returns>
        public override bool ElecEyeTest(out double[] testResults, int syn = 0)
        {
            lock (syncRoot)
            {
                testResults = new double[9];
                int count = 0;
                bool isOk = false;
                bool SaveFlag = false;
                int i;
                try
                {
                    string strChannel = "2A";
                    strChannel = strElecChannel[Convert.ToByte(CurrentChannel) - 1];

                    EyeTuningDisplay(0, 1);
                    this.WriteString(":ACQuire:REYE ON");
                    this.WriteString(":DISPlay:TOVerlap OFF");
                    SetMode(0, syn);
                    AcquisitionControl(2);
                    AcquisitionLimitTestSwitch(0, syn);
                    MaskONOFF(false, syn);
                    DisplayClearAlllist();
                    DisplayRiseFallTime(1);
                    DisplayRiseFallTime(0);
                    DisplayJitter(1);
                    DisplayJitter(0);
                    this.WriteString(":MEASure:EYE:EWIDth");
                    this.WriteString(":MEASure:EYE:EHEight");
                    DisplayCrossing();
                    DisplayOther(1);
                    AcquisitionControl(0);
                    AutoScale(syn);
                    Thread.Sleep(2000);// 因为Mask 变得很大,在此时反复清屏,以求让眼图显示稳定
                    ClearDisplay();
                    Thread.Sleep(2000);
                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
                    AcquisitionLimitTestSwitch(1, syn);
                    AcquisitionControl(2);
                    LoadMask(strChannel, elecMaskName, syn);
                    MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                    do
                    {
                        Thread.Sleep(200);
                        testResults[0] = ReadAMPLitude();
                        testResults[1] = GetCrossing();
                        testResults[2] = ReadJitter(0);
                        testResults[3] = ReadJitter(1);
                        testResults[4] = ReadRiseFallTime(0);
                        testResults[5] = ReadRiseFallTime(1);
                        testResults[6] = ReadMaskMargin();
                        testResults[7] = ReadEyeHeight();
                        testResults[8] = ReadEyeWidth();
                        for (i = 0; i < testResults.Length; i++)
                        {
                            if (testResults[i] >= 10000000) break;
                        }
                        if (i >= testResults.Length)
                        {
                            isOk = true;
                            break;
                        }
                        else
                            Thread.Sleep(200);
                    } while (count++ < 3);


                    //if (isOk == true)

                    for (int k = 0; k < 3; k++)
                    {
                        SaveFlag = SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
                        if (SaveFlag) break;
                    }
                    if (!SaveFlag)
                    {
                        MessageBox.Show("眼图保存失败!!!");
                        Log.SaveLogToTxt("Save Eye Image Error");
                        isOk = false;
                    }
                    AcquisitionLimitTestSwitch(0, syn);
                    AcquisitionControl(0);
                    MaskONOFF(false, syn);

                    this.WriteString(":ACQuire:REYE OFF");//Leo 修改
                    return isOk;
                }
                catch (InnoExCeption error)
                {
                    this.WriteString(":ACQuire:REYE OFF");//Leo 修改
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {
                    this.WriteString(":ACQuire:REYE OFF");//Leo 修改
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
                //this.WriteString(":ACQuire:REYE OFF");//Leo 修改
            }
        }
        #region 私有方法
        /// <summary>
        /// 带检查是否执行完成的IO输入指令(指令+OPC)
        /// </summary>
        /// <param name="command">
        /// 指令字符串
        /// </param>
        /// <param name="readTimeDelay">单次读取等待时间(s)</param>
        /// <param name="totalWaitTime">总共等待时间(s)</param>
        /// <returns></returns>
        private bool WriteOpc(string command, double readTimeDelay, UInt32 totalWaitTime)
        {
            lock (syncRoot)
            {
                bool OPCflag = false;
                int looptime = Convert.ToInt16(totalWaitTime / readTimeDelay);
                try
                {
                    this.WriteString(command + ";*OPC?\n");
                    for (int i = 0; i < looptime; i++)
                    {
                        Thread.Sleep(Convert.ToInt16(readTimeDelay * 1000));
                        try
                        {
                            string returndata = this.ReadString(100);
                            if (returndata.Contains("1"))
                            {
                                OPCflag = true;
                                break;
                            }
                        }
                        catch { }
                    }
                    return OPCflag;
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

                }
            }
        }
        /// <summary>
        /// 组装槽位和通道，输出标准通道字符串
        /// </summary>
        /// <param name="slot">
        /// 槽位(1~4)
        /// </param>
        /// <param name="channel">
        /// 通道(1~4)
        /// </param>
        /// <returns></returns>
        private string AssembleChannelString(byte slot, byte channel)
        {
            lock (syncRoot)
            {
                string strChannel = "";

                try
                {


                    if (slot < 1 || slot > 4) MessageBox.Show("Slot为{0}，不在1~4之间", slot.ToString());
                    else
                    {
                        if (channel < 1 || channel > 4) MessageBox.Show("Channel为{0}不在1~4之间！", channel.ToString());
                        else
                        {
                            switch (channel)
                            {
                                case 1:
                                    strChannel = slot.ToString() + "A";
                                    break;
                                case 2:
                                    strChannel = slot.ToString() + "B";
                                    break;
                                case 3:
                                    strChannel = slot.ToString() + "C";
                                    break;
                                case 4:
                                    strChannel = slot.ToString() + "D";
                                    break;
                                default:
                                    strChannel = slot.ToString() + "A";
                                    break;
                            }
                            // string[] slotchannel=flexslotchannel.Split(',');
                            //strChannel=slotchannel[channel].Trim();

                        }
                    }
                    Log.SaveLogToTxt("FLEX86100 Channel is" + strChannel);
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

                }
                return strChannel;
            }
        }
        /// <summary>
        /// 检查模组是否需要校验
        /// </summary>
        /// <param name="slot">
        /// 模组所处槽位
        /// </param>
        /// <returns></returns>
        private bool isModuleNeedCalibrate(byte slot)
        {
            lock (syncRoot)
            {
                string status;
                try
                {
                    this.WriteString(":CALibrate:SLOT" + slot.ToString() + ":STATus?");
                    status = this.ReadString(16);

                    if (status.Contains("UNCALIBRATED"))
                        return true;
                    else
                        return false;
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

                }
            }
        }
        /// <summary>
        /// 触发信号设置
        /// </summary>
        /// <param name="triggerSource">
        /// 触发源选择(0=FrontPanel,1=FreeRun)
        /// </param>
        /// <param name="triggerBandwidth">
        /// 触发带宽选择(0=FilterdEdge,1=StandardEdge,2=Clock/Divided)
        /// </param>
        /// <returns></returns>
        private bool TriggerSetup(byte triggerSource, byte triggerBandwidth, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string str_TriggerBwlimit = "";
                string returntrigbw = "";
                try
                {
                    if (triggerSource == 0)
                    {
                        // this.WriteString(":TIM:PTIMebase" + ":STATe " + "OFF");
                        this.WriteString(":TRIGger:SOURce FPANel");

                        switch (triggerBandwidth)
                        {
                            case 0:
                                str_TriggerBwlimit = "FILTered";
                                returntrigbw = "FILT\n";
                                break;
                            case 1:
                                str_TriggerBwlimit = "EDGE";
                                returntrigbw = "EDGE\n";
                                break;
                            case 2:
                                str_TriggerBwlimit = "CLOCK";
                                returntrigbw = "CLOC\n";
                                break;
                            default:
                                str_TriggerBwlimit = "FILTered";
                                returntrigbw = "FILT\n";
                                break;
                        }
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("TriggerBwlimit is" + str_TriggerBwlimit);
                            return this.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":TRIGGER:BWLIMIT?");
                                    readtemp = this.ReadString();
                                    if (readtemp == returntrigbw)
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("TriggerBwlimit is" + str_TriggerBwlimit);
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set TriggerBwlimit wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("TRIGger SOURce is FRUN");

                            return this.WriteString(":TRIGger:SOURce FRUN");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":TRIGger:SOURce FRUN");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":TRIGger:SOURce?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "FRUN\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("TRIGger SOURce is FRUN");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set TRIGger SOURce wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
        /// <summary>
        /// 校验模组
        /// </summary>
        /// <param name="slot">
        /// 模组所处槽位
        /// </param>
        /// <returns></returns>
        private bool CalibrateModel(byte slot)
        {
            lock (syncRoot)
            {
                try
                {
                    MessageBox.Show("准备校验slot{0}的模组，请拔除该模组上的所有输入信号再继续！", slot.ToString());
                    this.WriteString(":CALibrate:SLOT" + slot.ToString() + ":STARt");
                    this.WriteString(":CALibrate:SDONe?");
                    Thread.Sleep(5000);
                    if (this.ReadString() == "Failed") return false;

                    this.WriteString(":CALibrate:CONTinue");
                    this.WriteString(":CALibrate:SDONe?");
                    Thread.Sleep(200000);
                    if (this.ReadString() == "Failed") return false;

                    this.WriteString(":CALibrate:CONTinue");
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

                }
            }
        }
        private bool DIFFWaveformSwitch(string channel,byte DiffSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    if (DiffSwitch == 1)
                    {

                        // 载入电眼校验文件
                        string Str1 = @":DISK:SETup:RECall ""D:\user files\setups\fixture_deskew.setx""";
                        this.WriteString(Str1);


                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("differential waveform is on");
                            return this.WriteString(":DIFF" + channel + ":DMODe ON");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":DIFF" + channel + ":DMODe ON");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":DIFF" + channel + ":DMODe?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "1\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("differential waveform is on");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set differential waveform Switch wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("differential waveform is off");
                            return this.WriteString(":DIFF" + channel + ":DMODe OFF");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":DIFF" + channel + ":DMODe OFF");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":DIFF" + channel + ":DMODe?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("differential waveform Switch is off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set differential waveform Switch wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
        private bool BandwidthSet(string channel, byte bandwidth, int syn = 0)
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
                        Log.SaveLogToTxt("bandwidth is" + bandwidth);
                        return this.WriteString(":CHAN" + channel + ":BANDwidth" + " BANDwidth" + bandwidth);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":CHAN" + channel + ":BANDwidth" + " BANDwidth" + bandwidth);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":CHAN" + channel + ":BANDwidth?");
                                readtemp = this.ReadString();
                                if (readtemp.Trim() == ("BAND" + bandwidth + "\n").Trim())
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("bandwidth is" + bandwidth);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set bandwidth wrong");

                            }

                        }
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

                }
            }
        }
        /// <summary>
        /// 波长选择
        /// </summary>
        /// <param name="channel">
        /// 通道，例如"1A"
        /// </param>
        /// <param name="waveLength">
        /// 波长（0=850,1=1310,2=1550）
        /// </param>
        /// <returns></returns>
        private bool WavelengthSelect(string channel, byte waveLength, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string Wavelength_index = "1";
                try
                {
                    switch (waveLength)
                    {
                        case 0:
                            Wavelength_index = "1";
                            break;
                        case 1:
                            Wavelength_index = "2";
                            break;
                        case 2:
                            Wavelength_index = "3";
                            break;
                        default:
                            Wavelength_index = "1";
                            break;
                    }
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("Wavelength_index is" + Wavelength_index);
                        return this.WriteString(":CHAN" + channel + ":WAVelength" + " WAVelength" + Wavelength_index);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":CHAN" + channel + ":WAVelength" + " WAVelength" + Wavelength_index);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":CHAN" + channel + ":WAVelength?");
                                readtemp = this.ReadString();
                                if (readtemp.Trim() == ("WAV" + Wavelength_index + "\n").Trim())
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("Wavelength_index is" + Wavelength_index);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set WAVelength wrong");

                            }

                        }
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

                }
            }
        }
        /// <summary>
        /// 滤波器速率选择
        /// </summary>
        /// <param name="channel">
        /// 通道，例如"1A"
        /// </param>
        /// <param name="bitRate">
        /// 速率选择，例如10.3125E9
        /// </param>
        /// <returns></returns>
        private bool FileterSelect(string channel, double bitRate)
        {
            lock (syncRoot)
            {
                int filterIndex = 0;
                bool fileterExitflag = false;
                double rate = 0;
                double lastRate = 0;
                double thisRate = 0;
                try
                {
                    do
                    {
                        filterIndex = filterIndex + 1;
                        lastRate = rate;
                        this.WriteString(":CHAN" + channel + ":FSELect FILTer" + filterIndex.ToString());
                        this.WriteString(":CHAN" + channel + ":FSELect:RATe?");
                        rate = Convert.ToDouble(this.ReadString(20));
                        thisRate = rate;
                        if (Math.Abs(rate - bitRate) < 13000000)
                        {
                            fileterExitflag = true;
                            break;
                        }
                    } while (thisRate != lastRate);

                    return fileterExitflag;
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

                }
            }
        }
        /// <summary>
        /// 滤波器开关
        /// </summary>
        /// <param name="channel">
        /// 通道，例如"1A"
        /// </param>
        /// <param name="filterSwith">
        /// 开关，1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        private bool FileterSwitch(string channel, byte filterSwith, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string index = "";
                try
                {
                    if (filterSwith == 1)
                        index = "ON";
                    if (filterSwith == 0)
                        index = "OFF";
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("FLEX86100 channel is" + channel + "filterSwith is" + index);
                        return this.WriteString(":CHAN" + channel + ":FILTer " + index);

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":CHAN" + channel + ":FILTer " + index);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":CHAN" + channel + ":FILTer?");
                                readtemp = this.ReadString();
                                if (readtemp == filterSwith.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("FLEX86100 channel is" + channel + "filterSwith is" + index);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set filterSwith wrong");

                            }

                        }
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

                }
            }
        }
        /// <summary>
        /// 光功率补偿值设置
        /// </summary>
        /// <param name="channel">
        /// 通道，例如"1A"
        /// </param>
        /// <param name="attenuation">
        /// 补偿值单位dB,例如 3
        /// </param>
        /// <param name="attSwitch">
        /// 开关，1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        private bool SetAttenuation(string channel, double attenuation, byte attSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                int k = 0;
                string readtemp = "";
                string readtemp2 = "";
                try
                {
                    if (attSwitch == 1)
                    {
                        if (syn == 0)
                        {

                            this.WriteString(":CHAN" + channel + ":ATTenuator:STATe ON");
                            this.WriteString(":CHAN" + channel + ":ATTenuator:DECibels " + attenuation.ToString() + "\n");
                            Log.SaveLogToTxt("FLEX86100 channel is" + channel + "attenuation is" + attenuation);
                            return true;
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag2 = this.WriteString(":CHAN" + channel + ":ATTenuator:STATe ON");
                                flag1 = this.WriteString(":CHAN" + channel + ":ATTenuator:DECibels " + attenuation.ToString() + "\n");
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":CHAN" + channel + ":ATTenuator:STATe?");
                                    readtemp = this.ReadString();
                                    this.WriteString(":CHAN" + channel + ":ATTenuator:DECibels?");
                                    readtemp2 = this.ReadString();

                                    if ((readtemp == "1\n") && (Convert.ToDouble(readtemp2) == Convert.ToDouble(attenuation.ToString())))
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("FLEX86100 channel is" + channel + "attenuation is" + attenuation);
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("FLEX86100 set attenuation wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("FLEX86100 ATTenuator STATe IS off");
                            return this.WriteString(":CHAN" + channel + ":ATTenuator:STATe OFF");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":CHAN" + channel + ":ATTenuator:STATe OFF");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":CHAN" + channel + ":ATTenuator:STATe?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("FLEX86100 ATTenuator STATe IS off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("FLEX86100 set ATTenuator STATe wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
        /// <summary>
        /// ER修正因子设置
        /// </summary>
        /// <param name="channel">
        /// 测试通道，例如"1A"
        /// </param>
        /// <param name="erFactorPercent">
        /// 修正量单位%，例如3
        /// </param>
        /// <param name="erFactorSwitch">
        /// 开关，1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        private bool SetERCorrectFactor(string channel, double erFactorPercent, byte erFactorSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                int k = 0;
                string readtemp = "";
                string readtemp2 = "";
                try
                {
                    if (erFactorSwitch == 1)
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("FLEX86100 channel is" + channel + "erFactorPercent is" + erFactorPercent);
                            flag2 = this.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor ON");

                            flag1 = this.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor  " + (erFactorPercent).ToString() + "\r");
                            if ((flag1 == true) && (flag2 == true))
                            { flag = true; }
                            return flag;
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag2 = this.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor ON");

                                flag1 = this.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor  " + (erFactorPercent).ToString() + "\r");
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor?");
                                    readtemp = this.ReadString();
                                    this.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor?");
                                    readtemp2 = this.ReadString();

                                    if ((readtemp == "1\n") && (Convert.ToDouble(readtemp2) == Convert.ToDouble((erFactorPercent).ToString())))
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("FLEX86100 channel is" + channel + "erFactorPercent is" + erFactorPercent);
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set FLEX86100 erFactorPercent wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("ACFactor STATe IS off");
                            return this.WriteString(":MEASure:ERATio:CHAN1A:ACFactor OFF");

                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":MEASure:ERATio:CHAN1A:ACFactor OFF");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":MEASure:ERATio:CHAN1A:ACFactor?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("ACFactor STATe IS off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set ACFactor STATe wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }


        /// <summary>
        /// 精准时基单元的设置
        /// </summary>
        /// <param name="slot">
        /// 单元所处槽位
        /// </param>
        /// <param name="refClkFrequency">
        /// 参考时钟例如10.3125E9
        /// </param>
        /// <param name="Switch">
        /// 开关，1=ON,0=OFF
        /// </param>
        /// <param name="synchMethod">
        /// 同步方式选择0=OLIN,1=FAST
        /// </param>
        /// <returns></returns>
        private bool PrecisionTimebaseSetup(byte slot, double refClkFrequency, byte Switch, byte synchMethod, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                int k = 0;
                string readtemp = "";
                string readtemp1 = "";
                string readtemp2 = "";
                string strSynchMethod;
                try
                {
                    if (Switch == 1)
                    {
                        switch (synchMethod)
                        {
                            case 0:
                                strSynchMethod = "OLIN";
                                break;
                            case 1:
                                strSynchMethod = "FAST";
                                break;
                            default:
                                strSynchMethod = "OLIN";
                                break;
                        }
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("PTIMebase Slot is" + slot.ToString() + "RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString() + "STATe is on");
                            flag1 = this.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod " + strSynchMethod + ";" + ":PTIMebase" + slot.ToString() + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                            flag2 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "ON", 0.01, 5);
                            if ((flag1 == true) && (flag2 == true))
                            { flag = true; }
                            return flag;
                        }
                        else
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod " + strSynchMethod + ";" + ":PTIMebase" + slot.ToString() + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                                flag2 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "ON", 0.01, 5);
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod?");
                                    readtemp = this.ReadString();
                                    this.WriteString(":PTIMebase" + slot.ToString() + ":RFRequency?");
                                    readtemp1 = this.ReadString();
                                    this.WriteString(":PTIMebase" + slot.ToString() + ":STATe?");
                                    readtemp2 = this.ReadString();
                                    if ((readtemp == strSynchMethod + "\n") && (Convert.ToDouble(readtemp1) == Convert.ToDouble(refClkFrequency.ToString("E"))) && (readtemp2 == "ON\n"))
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("PTIMebase Slot is" + slot.ToString() + "RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString("E") + "STATe is on");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set PTB wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("PTIMebase Slot is" + slot.ToString() + "STATe is off");
                            return WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "OFF", 0.01, 5);

                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "OFF", 0.01, 5);
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":PTIMebase" + slot.ToString() + ":STATe?");
                                    readtemp = this.ReadString();
                                    if (readtemp2 == "OFF\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("PTIMebase Slot is" + slot.ToString() + "STATe is off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set PTB wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
       
         private bool SetTimeBase( double refClkFrequency, byte Switch, byte synchMethod, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                int k = 0;
                string readtemp = "";
                string readtemp1 = "";
                string readtemp2 = "";
                string strSynchMethod;
                try
                {
                    switch (synchMethod)
                    {
                        case 0:
                            strSynchMethod = "OLIN";
                            break;
                        case 1:
                            strSynchMethod = "FAST";
                            break;
                        default:
                            strSynchMethod = "OLIN";
                            break;
                    }
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("PTIMebase " + "RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString() + "STATe is on");
                        flag1 = this.WriteString(":Timebase:PTIMebase:RMEThod " + strSynchMethod + ";" + ":Timebase:PTIMebase:RFRequency " + (refClkFrequency).ToString("E") + "\n");
                        //  flag2 = WriteOpc(":Timebase:PTIMebase:STATe " + "ON", 0.01, 5);
                        if (flag1 == true)
                        { flag = true; }
                        return flag;
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":Timebase:PTIMebase:RMEThod " + strSynchMethod + ";" + ":Timebase:PTIMebase:RFRequency " + (refClkFrequency).ToString("E") + "\n");
                            // flag2 = WriteOpc(":Timebase:PTIMebase:STATe " + "ON", 0.01, 5);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":Timebase:PTIMebase:RMEThod?");
                                readtemp = this.ReadString();
                                this.WriteString(":Timebase:PTIMebase:RFRequency?");
                                readtemp1 = this.ReadString();

                                if ((readtemp == strSynchMethod + "\n") && (Convert.ToDouble(readtemp1) == Convert.ToDouble(refClkFrequency.ToString("E"))))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("PTIMebase RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString("E") + "STATe is on");
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set PTB wrong");

                            }

                        }

                    }
                    if (Switch == 1)
                    {

                        if (syn == 0)
                        {
                            flag2 = WriteOpc(":Timebase:PTIMebase:STATe " + "ON", 0.01, 5);
                            if (flag2 == true)
                            { flag = true; }
                            return flag;
                        }
                        else
                        {


                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {


                                    this.WriteString(":Timebase:PTIMebase:STATe?");
                                    readtemp2 = this.ReadString();
                                    if (readtemp2 == "ON\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    //Log.SaveLogToTxt("PTIMebase RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString("E")  + "STATe is on");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set PTB wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("PTIMebase STATe is off");
                            return WriteOpc(":Timebase:PTIMebase:STATe " + "OFF", 0.01, 5);

                        }
                        else
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = WriteOpc(":Timebase:PTIMebase:STATe " + "OFF", 0.01, 5);
                                if (flag1 == true)
                                    break;
                            }

                            return flag1;
                        }
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

                }
            }
        }
        
        private bool PTBSetup(double refClkFrequency, byte Switch, byte synchMethod, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                int k = 0;
                string readtemp = "";
                string readtemp1 = "";
                string readtemp2 = "";
                string strSynchMethod;
                try
                {
                    if (Switch == 1)
                    {
                        switch (synchMethod)
                        {
                            case 0:
                                strSynchMethod = "OLIN";
                                break;
                            case 1:
                                strSynchMethod = "FAST";
                                break;
                            default:
                                strSynchMethod = "OLIN";
                                break;
                        }
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("PTB RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString() + "STATe is on");
                            flag1 = this.WriteString(":TIM:PTIMebase" + ":RMEThod " + strSynchMethod + ";" + ":TIM:PTIMebase" + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                            flag2 = this.WriteString(":TIM:PTIMebase" + ":STATe " + "ON");
                            if ((flag1 == true) && (flag2 == true))
                            { flag = true; }
                            return flag;
                        }
                        else
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":TIM:PTIMebase" + ":RMEThod " + strSynchMethod + ";" + ":TIM:PTIMebase" + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                                flag2 = this.WriteString(":TIM:PTIMebase" + ":STATe " + "ON");
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":TIM:PTIMebase" + ":RMEThod?");
                                    readtemp = this.ReadString();
                                    this.WriteString(":TIM:PTIMebase" + ":RFRequency?");
                                    readtemp1 = this.ReadString();
                                    this.WriteString(":TIM:PTIMebase" + ":STATe?");
                                    readtemp2 = this.ReadString();
                                    if ((readtemp == strSynchMethod + "\n") && (Convert.ToDouble(readtemp1) == Convert.ToDouble(refClkFrequency.ToString("E"))) && (readtemp2 == "ON\n"))
                                        break;

                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("PTB RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString() + "STATe is on");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set PTB wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("PTIMebase STATe is off");
                            return this.WriteString(":TIM:PTIMebase" + ":STATe " + "OFF");

                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":TIM:PTIMebase" + ":STATe " + "OFF");
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":TIM:PTIMebase" + ":STATe?");
                                    readtemp = this.ReadString();
                                    if (readtemp2 == "OFF\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("PTIMebase STATe is off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set PTB wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
        
        /// <summary>
        /// 快速眼图测试的设置
        /// </summary>
        /// <param name="bitRate">
        /// 测试速率选择，例如10.3125E9
        /// </param>
        /// <param name="rapidEyeSwitch">
        /// 1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        private bool RapidEyeSetup(double bitRate, byte rapidEyeSwitch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                int k = 0;
                string readtemp = "";
                string readtemp2 = "";
                try
                {
                    if (rapidEyeSwitch == 1)
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("FLEX86100 bitRate is" + bitRate + "rapidEyeSwitch is" + "ON");
                            flag2 = this.WriteString(":ACQ:REYE " + "ON");
                            // flag2 = this.WriteString(":ACQ:REYE " + "OFF");
                            flag1 = this.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                            if ((flag1 == true) && (flag2 == true))
                            { flag = true; }
                            return flag;
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag2 = this.WriteString(":ACQ:REYE " + "ON");

                                flag1 = this.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":ACQ:REYE?");
                                    readtemp = this.ReadString();
                                    this.WriteString(":TRIG:BRAT?");
                                    readtemp2 = this.ReadString();

                                    if ((readtemp == "1\n") && (Convert.ToDouble(readtemp2) == Convert.ToDouble(bitRate.ToString("E"))))
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("FLEX86100 bitRate is" + bitRate + "rapidEyeSwitch is" + "ON");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt(" RapidEyeSetup wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            flag1 = this.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                            Log.SaveLogToTxt(" RapidEyeSetup STATe IS off");
                            flag2 = this.WriteString(":ACQ:REYE " + "OFF");
                            if ((flag1 == true) && (flag2 == true))
                            { flag = true; }
                            return flag;
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {

                                flag2 = this.WriteString(":ACQ:REYE " + "OFF");
                                flag1 = this.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if (flag1 == true && flag2 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":ACQ:REYE?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt(" RapidEyeSetup STATe IS off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt(" RapidEyeSetup wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
        /// <summary>
        /// 眼图实时刷新显示方式开关
        /// </summary>
        /// <param name="Switch">
        /// 1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        public override bool EyeTuningDisplay(byte Switch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    if (Switch == 1)
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt(" EyeTuningDisplay IS ON");
                            return this.WriteString(":DISPlay:ETUNing ON");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":DISPlay:ETUNing ON");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":DISPlay:ETUNing?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "1\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt(" EyeTuningDisplay IS ON");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt(" EyeTuningDisplay wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt(" EyeTuningDisplay IS OFF");
                            return this.WriteString(":DISPlay:ETUNing OFF");

                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":DISPlay:ETUNing OFF");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":DISPlay:ETUNing?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt(" EyeTuningDisplay IS OFF");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt(" EyeTuningDisplay wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
        /// <summary>
        /// RiseTime FallTime测试的参考点和阈值设置
        /// </summary>
        /// <param name="threshold">
        /// 阈值设置（0=80%50%20%,1=90%50%10%）
        /// </param>
        /// <param name="reference">
        /// 参考点设置（0=OneZero,1=VtopVbase）
        /// </param>
        /// <returns></returns>
        private bool MessureThresholdSetup(byte threshold, byte reference, int syn = 0)
        {
            lock (syncRoot)
            {
                string strthreshold;
                string strreference;
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                int k = 0;
                int j = 0;
                string readtemp = "";
                try
                {
                    switch (reference)
                    {
                        case 0://One,Zero
                            strreference = "OZER";
                            break;
                        case 1://Vtop,Vbase
                            strreference = "TBAS";
                            break;
                        default:
                            strreference = "OZER";
                            break;
                    }
                    switch (threshold)
                    {
                        case 0://"80,50,20":
                            strthreshold = "P205080";
                            break;
                        case 1://"90,50,10":
                            strthreshold = "UDEF";
                            break;
                        default:
                            strthreshold = "P205080";
                            break;
                    }

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt(" THReshold reference is" + strreference);
                        Log.SaveLogToTxt(" THReshold METHod is" + strthreshold);

                        flag1 = this.WriteString(":MEASure:THReshold:EREFerence " + strreference);
                        flag2 = this.WriteString(":MEASure:THReshold:METHod " + strthreshold);
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":MEASure:THReshold:EREFerence " + strreference);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {
                                this.WriteString(":MEASure:THReshold:EREFerence?");
                                readtemp = this.ReadString();
                                if (readtemp == strreference + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt(" THReshold reference is" + strreference);

                            }
                            else
                            {
                                Log.SaveLogToTxt(" THReshold EREFerence wrong");

                            }
                        }


                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":MEASure:THReshold:METHod " + strthreshold);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (j = 0; j < 3; j++)
                            {
                                this.WriteString(":MEASure:THReshold:METHod?");
                                readtemp = this.ReadString();
                                if (readtemp == strthreshold + "\n")
                                    break;
                            }
                            if (j <= 3)
                            {
                                Log.SaveLogToTxt(" THReshold METHod is" + strthreshold);

                            }
                            else
                            {
                                Log.SaveLogToTxt(" THReshold METHod wrong");

                            }


                        }

                        if ((j <= 3) && (k <= 3))
                        { flag = true; }
                        else
                        { flag = false; }
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

                }
            }
        }
        /// <summary>
        /// 通道显示开关
        /// </summary>
        /// <param name="channel">
        /// 通道选择，例如"1A"
        /// </param>
        /// <param name="Switch">
        /// 1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        override public bool OpenOpticalChannel(bool Switch, int syn = 0)//true optical  false elec
        {
            lock (syncRoot)
            {
                try
                {


                    for (int i = 0; i < 4; i++)
                    {
                        EyeChannelDisplaySwitch(strElecChannel[i], 0, syn);
                        ChannelDisplaySwitch(strOpticalChannel[i], 0, syn);
                    }
                    if (Switch)
                    {
                        return ChannelDisplaySwitch(strOpticalChannel[Convert.ToByte(CurrentChannel) - 1], 1, syn);
                    }
                    else
                    {
                        return EyeChannelDisplaySwitch(strElecChannel[Convert.ToByte(CurrentChannel) - 1], 1, syn);
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
                }
            }
        }
        private bool EyeChannelDisplaySwitch(string channel, byte Switch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                int j = 0;
                string readtemp = "";


                try
                {
                    if (DiffSwitch == 0)
                    {
                        if (Switch == 1)
                        {
                            if (syn == 0)
                            {
                                Log.SaveLogToTxt(" ChannelDisplaySwitch is on");

                                flag = this.WriteString(":CHAN" + channel + ":DISPlay " + "1");


                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    // flag1 = this.WriteString(":CHAN" + channel + ":DISPlay " + "1");

                                    flag1 = this.WriteString(":CHAN" + channel + ":DISPlay " + "1");

                                    if (flag1 == true)
                                        break;
                                }
                                if (flag1 == true)
                                {
                                    for (k = 0; k < 3; k++)
                                    {
                                        this.WriteString(":CHAN" + channel + ":DISPlay?");
                                        readtemp = this.ReadString();
                                        if (readtemp == "1\n")
                                            break;
                                    }
                                    if (k <= 3)
                                    {
                                        Log.SaveLogToTxt(" ChannelDisplaySwitch is on");
                                        flag = true;
                                    }
                                    else
                                    {
                                        Log.SaveLogToTxt(" ChannelDisplaySwitch wrong");

                                    }

                                }
                            }
                        }
                        else
                        {
                            if (syn == 0)
                            {
                                Log.SaveLogToTxt(" ChannelDisplaySwitch is off");
                                flag = this.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    flag1 = this.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                                    if (flag1 == true)
                                        break;
                                }
                                if (flag1 == true)
                                {
                                    for (j = 0; j < 3; j++)
                                    {
                                        this.WriteString(":CHAN" + channel + ":DISPlay?");
                                        readtemp = this.ReadString();
                                        if (readtemp == "0\n")
                                            break;
                                    }
                                    if (j <= 3)
                                    {
                                        Log.SaveLogToTxt(" ChannelDisplaySwitch is off");
                                        flag = true;
                                    }
                                    else
                                    {
                                        Log.SaveLogToTxt(" ChannelDisplaySwitch wrong");

                                    }

                                }
                            }
                        }
                        return flag;
                    }
                    else
                    {
                        if (Switch == 1)
                        {
                            flag = this.WriteString(":DIff" + channel + ":Display ON");
                        }
                        else
                        {
                            flag = this.WriteString(":DIff" + channel + ":Display OFF");
                        }

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
                }
            }
        }
        private bool ChannelDisplaySwitch(string channel, byte Switch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                int j = 0;
                string readtemp = "";
                try
                {
                    if (Switch == 1)
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt(" ChannelDisplaySwitch is on");
                            flag = this.WriteString(":CHAN" + channel + ":DISPlay " + "1");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":CHAN" + channel + ":DISPlay " + "1");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":CHAN" + channel + ":DISPlay?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "1\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt(" ChannelDisplaySwitch is on");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt(" ChannelDisplaySwitch wrong");

                                }

                            }
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt(" ChannelDisplaySwitch is off");
                            flag = this.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (j = 0; j < 3; j++)
                                {
                                    this.WriteString(":CHAN" + channel + ":DISPlay?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (j <= 3)
                                {
                                    Log.SaveLogToTxt(" ChannelDisplaySwitch is off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt(" ChannelDisplaySwitch wrong");

                                }

                            }
                        }
                    }
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

                }
            }
        }
        /// <summary>
        /// 设置示波器显示的Scale和Offset
        /// </summary>
        /// <param name="channel">
        /// 设置哪个通道，例如"1A"
        /// </param>
        /// <param name="scale">
        /// 单位为uW或者uV
        /// </param>
        /// <param name="offset">
        /// 单位为uW或者uV
        /// </param>
        /// <returns></returns>
        private bool EyeScaleOffset(string channel, double scale, double offset, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                int j = 0;
                string readtemp = "";
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("FLEX86100 channel is" + channel + "scale is" + (scale / 1000000.0).ToString() + "offset is" + (offset / 1000000.0).ToString());
                        return this.WriteString(":CHAN" + channel + ":YSCale " + (scale / 1000000.0).ToString() + "\n" + ":CHAN" + channel + ":YOFFset " + (offset / 1000000.0).ToString());

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":CHAN" + channel + ":YSCale " + (scale / 1000000.0).ToString() + "\n" + ":CHAN" + channel + ":YOFFset " + (offset / 1000000.0).ToString());

                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":CHAN" + channel + ":YSCale?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble((scale / 1000000.0).ToString()))
                                    break;
                            }
                            for (j = 0; j < 3; j++)
                            {

                                this.WriteString(":CHAN" + channel + ":YOFFset?");
                                readtemp = this.ReadString();
                                if (readtemp == (offset / 1000000.0).ToString())
                                    break;
                            }

                        }
                        if ((k <= 3) && (j <= 3))
                        {
                            Log.SaveLogToTxt("FLEX86100 channel is" + channel + "scale is" + (scale / 1000000.0).ToString() + "offset is" + (offset / 1000000.0).ToString());
                            flag = true;
                        }
                        else
                        {
                            Log.SaveLogToTxt("set FLEX86100 switch wrong");

                        }
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

                }
            }
        }
        public override bool SetScaleOffset(double PowerVaule,int syn=0)
        {
            lock (syncRoot)
            {
                double avgpow;
                PowerVaule = Algorithm.ChangeDbmtoUw(PowerVaule);
                try
                {
                    string OpticalChannel = strOpticalChannel[Convert.ToByte(CurrentChannel) - 1];
                    avgpow = GetAveragePowerWatt();

                    if (avgpow < 1E+6 && avgpow > 1)
                    {
                        double scaletemp = avgpow / 3;
                        double offsettemp = avgpow / 2;
                        Log.SaveLogToTxt("scale is" + scaletemp + "offset is" + offsettemp);
                        EyeScaleOffset(OpticalChannel, scaletemp, offsettemp, syn);
                    }
                    else
                    {
                        avgpow = PowerVaule;
                        if (avgpow != 0 && avgpow < 1E+6)
                        {
                            double scaletemp = avgpow / 3;
                            double offsettemp = avgpow / 2;
                            Log.SaveLogToTxt("scale is" + scaletemp + "offset is" + offsettemp);
                            EyeScaleOffset(OpticalChannel, scaletemp, offsettemp, syn);
                        }
                    }
                    Thread.Sleep(flexsetscaledelay);
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

                }
            }
        }
        public override bool LoadMask()
        {
            lock (syncRoot)
            {
                try
                {
                    AcquisitionLimitTestSwitch(0, 1);
                    ClearDisplay();
                    Thread.Sleep(2000);
                    if (opticalMaskName != null && opticalMaskName != "")
                    {
                        LoadMask(CurrentChannel, opticalMaskName, 1);
                        MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, 1);
                        AcquisitionControl(0);
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

                }
            }
        }
        /// <summary>
        /// 装载模板
        /// </summary>
        /// <param name="Channel">
        /// 测试哪个通道，例如"1A"
        /// </param>
        /// <param name="MaskName">
        /// 模板路径，例如@"D\User Files\Masks\Ethernet\010.3125-10Gbe_10_3125_May02.mskx"
        /// </param>
        /// <returns></returns>
        private bool LoadMask(string Channel, string MaskName, int syn = 0)
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
                    string AA = @MaskName;
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("FLEX86100 Channel is" + Channel + "MaskName is" + MaskName);

                        flag1 = this.WriteString(":MTESt:SOURce CHAN" + Channel);
                        flag2 = this.WriteString(":MTESt:LOAD:FNAMe " + "\"" + AA + "\"");
                        this.WriteString(":MTESt:LOAD");
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":MTESt:SOURce CHAN" + Channel);
                            flag2 = this.WriteString(":MTESt:LOAD:FNAMe " + "\"" + AA + "\"");
                            this.WriteString(":MTESt:LOAD");
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":MTESt:SOURce?");
                                readtemp = this.ReadString();

                                if (readtemp.Contains(Channel.ToUpper().Trim()))
                                {
                                    break;
                                }
                                //if (readtemp == "CHAN" + Channel + "\n")
                                //    break;
                            }
                            for (j = 0; j < 3; j++)
                            {

                                this.WriteString(":MTESt:LOAD:FNAMe?");
                                readtemp = this.ReadString();
                                if (readtemp == "\"" + MaskName + "\"" + "\n")
                                    break;
                            }
                            if ((k <= 3) && (j <= 3))
                            {
                                Log.SaveLogToTxt("FLEX86100 Channel is" + Channel + "MaskName is" + MaskName);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("  LoadMask wrong");

                            }

                        }
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

                }
            }
        }
        /// <summary>
        /// 模板测试开关
        /// </summary>
        /// <param name="MaskON">
        /// 1=ON，0=OFF
        /// </param>
        /// <returns></returns>
        override public bool MaskONOFF(bool MaskON, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    if (MaskON == true)
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("MaskONOFF is on");
                            flag = this.WriteString(":mtest:DISP " + "ON");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":mtest:DISP " + "ON");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":mtest:DISP?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "1\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("MaskONOFF is on");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set MaskONOFF wrong");

                                }

                            }
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("MaskONOFF is off");
                            flag = this.WriteString(":mtest:DISP " + "OFF");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":mtest:DISP " + "OFF");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    this.WriteString(":mtest:DISP?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("MaskONOFF is off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set MaskONOFF wrong");

                                }

                            }
                        }
                    }
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

                }
            }
        }
        /// <summary>
        /// 模板余量测试方法设置
        /// </summary>
        /// <param name="marginOnOff">
        /// 是否打开Margin,1=ON,0=OFF
        /// </param>
        /// <param name="marginAutoManul">
        /// 手动测试Margin还是自动测试，1=自动，0=手动
        /// </param>
        /// <param name="manualMarginPercent">
        /// 手动测试Margin时，输入Margin值，自动测试时忽略该参数
        /// </param>
        /// <param name="autoMarginType">
        /// 自动测试Margin时，Margin类型选择，1=HitRatio,0=HitCount
        /// </param>
        /// <param name="hitRatio">
        /// HitRatio的数值，例如1E-6
        /// </param>
        /// <param name="hitCount">
        /// HitCount的数值，例如0
        /// </param>
        /// <returns></returns>
     override   public bool MaskTestMarginSetup(byte marginOnOff, byte marginAutoManul, int manualMarginPercent, byte autoMarginType, double hitRatio, int hitCount, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                bool flag2 = false;
                bool flag3 = false;
                int k = 0;
                int j = 0;
                string readtemp = "";
                string readtemp1 = "";
                string readtemp2 = "";
                try
                {
                    if (syn == 0)
                    {
                        if (marginOnOff == 1)
                        {
                            this.WriteString(":MTESt:MARGin:STATe ON\n");
                        }
                        else
                        {
                            this.WriteString(":MTESt:MARGin:STATe OFF\n");
                        }
                        if (marginAutoManul != 1)//marginType=0  ManulMargin
                        {
                            this.WriteString(":MTESt:MARGin:METHod MANual\n");
                            this.WriteString(":MTESt:MARGin:PERCent " + manualMarginPercent.ToString("E") + "\n");
                        }
                        else//marginType=1  AutoMargin
                        {

                            if (autoMarginType != 1)//autoMarginType=0 HitCount
                            {
                                this.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                this.WriteString(":MTESt:MARGin:AUTo:METHod HITS\n");
                                this.WriteString(":MTESt:MARGin:AUTo:HITs " + hitCount.ToString() + "\n");
                            }
                            else
                            {
                                this.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                this.WriteString(":MTESt:MARGin:AUTo:METHod HRATio\n");
                                this.WriteString(":MTESt:MARGin:AUTo:HRATio " + hitRatio.ToString("E") + "\n");
                            }

                        }
                        return true;
                    }
                    else
                    {
                        if (marginOnOff == 1)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":MTESt:MARGin:STATe ON\n");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":MTESt:MARGin:STATe?");
                                    readtemp = this.ReadString();
                                    if (readtemp == marginOnOff.ToString() + "\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("marginOnOff is ON");
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set marginOnOff wrong");
                                }

                            }
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":MTESt:MARGin:STATe OFF\n");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":MTESt:MARGin:STATe?");
                                    readtemp = this.ReadString();
                                    if (readtemp == marginOnOff.ToString() + "\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("marginOnOff is OFF");
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set marginOnOff wrong");
                                }

                            }

                        }
                        if (marginAutoManul != 1)//marginType=0  ManulMargin
                        {

                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":MTESt:MARGin:METHod MANual\n");
                                flag2 = this.WriteString(":MTESt:MARGin:PERCent " + manualMarginPercent.ToString("E") + "\n");

                                if ((flag1 == true) && (flag2 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true))
                            {
                                for (j = 0; j < 3; j++)
                                {

                                    this.WriteString(":MTESt:MARGin:METHod?");
                                    readtemp = this.ReadString();
                                    this.WriteString(":MTESt:MARGin:PERCent?");
                                    readtemp1 = this.ReadString();
                                    if ((readtemp == "MANual\n") && (readtemp1 == manualMarginPercent.ToString("E") + "\n"))
                                        break;
                                }
                                if (j <= 3)
                                {
                                    Log.SaveLogToTxt("MARGin  METHod is MANual");
                                    Log.SaveLogToTxt("MARGin  PERCent is" + manualMarginPercent.ToString("E"));
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set MARGin  METHod wrong");
                                    Log.SaveLogToTxt("set PERCent  METHod wrong");

                                }

                            }
                        }
                        else//marginType=1  AutoMargin
                        {

                            if (autoMarginType != 1)//autoMarginType=0 HitCount
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    flag1 = this.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                    flag2 = this.WriteString(":MTESt:MARGin:AUTo:METHod HITS\n");
                                    flag3 = this.WriteString(":MTESt:MARGin:AUTo:HITs " + hitCount.ToString() + "\n");

                                    if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                        break;
                                }
                                if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                {
                                    for (j = 0; j < 3; j++)
                                    {

                                        this.WriteString(":MTESt:MARGin:METHod?");
                                        readtemp = this.ReadString();
                                        this.WriteString(":MTESt:MARGin:AUTo:METHod?");
                                        readtemp1 = this.ReadString();
                                        this.WriteString(":MTESt:MARGin:AUTo:HITs?");
                                        readtemp2 = this.ReadString();
                                        if ((readtemp == "AUTO\n") && (readtemp1 == "HITS\n") && (readtemp2 == hitCount.ToString() + "\n"))
                                            break;
                                    }
                                    if (j <= 3)
                                    {
                                        Log.SaveLogToTxt("MARGin  METHod is AUTO");
                                        Log.SaveLogToTxt("MARGin  AUTo METHod is HITS" + "HITs is " + hitCount.ToString());
                                    }
                                    else
                                    {
                                        Log.SaveLogToTxt("set MARGin  METHod wrong");
                                        Log.SaveLogToTxt("set AUTo  METHod  HITS wrong");

                                    }

                                }
                            }
                            else//autoMarginType=1 HitRatio
                            {


                                for (int i = 0; i < 3; i++)
                                {
                                    flag1 = this.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                    flag2 = this.WriteString(":MTESt:MARGin:AUTo:METHod HRATio\n");
                                    flag3 = this.WriteString(":MTESt:MARGin:AUTo:HRATio " + hitRatio.ToString("E") + "\n");

                                    if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                        break;
                                }
                                if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                {
                                    for (j = 0; j < 3; j++)
                                    {

                                        this.WriteString(":MTESt:MARGin:METHod?");
                                        readtemp = this.ReadString();
                                        this.WriteString(":MTESt:MARGin:AUTo:METHod?");
                                        readtemp1 = this.ReadString();
                                        this.WriteString(":MTESt:MARGin:AUTo:HRATio?");
                                        readtemp2 = this.ReadString();
                                        if ((readtemp == "AUTO\n") && (readtemp1 == "HRATio\n") && (readtemp2 == hitRatio.ToString("E") + "\n"))
                                            break;
                                    }
                                    if (j <= 3)
                                    {
                                        Log.SaveLogToTxt("MARGin  METHod is AUTO");
                                        Log.SaveLogToTxt("MARGin  AUTo METHod is HRATio" + "HRATio is " + hitRatio.ToString("E"));
                                    }
                                    else
                                    {
                                        Log.SaveLogToTxt("set MARGin  METHod wrong");
                                        Log.SaveLogToTxt("set AUTo  METHod  HRATio wrong");

                                    }

                                }

                            }


                        }
                        if ((k <= 3) && (j <= 3))
                        { flag = true; }
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

                }
            }
        }
        override public bool SetMaskAlignMethod(byte method, int syn = 0)
        {
            return true;
        }
        /// <summary>
        /// 示波器运行控制指令(Run,Stop,Clear)
        /// </summary>
        /// <param name="control">
        /// 0=Run,1=Stop,2=Clear
        /// </param>
        /// <returns></returns>
        override public bool RunStop(bool run)// true "RUN"  false  "Stop"
        {
            lock (syncRoot)
            {
                try
                {


                    if (run)
                    {

                        return AcquisitionControl(0);


                    }
                    else
                    {

                        return AcquisitionControl(1);


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

                }
            }
        }
        private bool AcquisitionControl(byte control)
        {
            lock (syncRoot)
            {
                string strcontrol;
                try
                {
                    switch (control)
                    {
                        case 0:
                            strcontrol = "RUN";
                            break;
                        case 1:
                            strcontrol = "STOP";
                            break;
                        case 2:
                            strcontrol = "CDISplay";
                            break;
                        default:
                            strcontrol = "RUN";
                            break;
                    }
                    Log.SaveLogToTxt("AcquisitionControl is" + strcontrol);
                    return WriteOpc(":ACQuire:" + strcontrol, 5, 30);
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

                }
            }
        }
        /// <summary>
        /// 使眼图调整到中间，但幅度维持不变
        /// </summary>
        /// <param name="bitRate">
        /// 数据通信速率单位为bps，例如10.3125E9
        /// </param>
        /// <returns></returns>
        private bool CenterEye()
        {
            lock (syncRoot)
            {
                double bitRate = Convert.ToDouble(FlexDcaDataRate);
                double scale;
                string strscale;
                try
                {
                    double data4;
                    int i = 0;
                    do
                    {
                        scale = (1.667 * (1 / bitRate)) / 10.0;
                        strscale = scale.ToString("E");
                        this.WriteString(":TIMebase:SCALe " + strscale);
                        this.WriteString(":TIMebase:REFerence CENTer");
                        this.WriteString(":MEASure:EYE:ECTime?");
                        double data1 = Convert.ToDouble(this.ReadString(20));
                        this.WriteString(":TIMebase:POSition?");
                        double data2 = Convert.ToDouble(this.ReadString(20));
                        double data3 = data2 - data1 - ((1 / bitRate) / 2);
                        data4 = data2 - data3;
                        if (data4 < data2)
                        {
                            data4 = data4 + (1 / bitRate);
                        }
                        strscale = data4.ToString("E");
                        i++;
                    } while (data4 > 1e10 && i < 5);

                    this.WriteString(":TIMebase:POSition " + strscale);
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

                }
            }
        }
        /// <summary>
        /// 示波器模式选择
        /// </summary>
        /// <param name="Mode">
        /// 0=EyeMode,1=OscMode,2=JitterMode
        /// </param>
        /// <returns></returns>
        override public bool SetMode(byte Mode, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string index;
                try
                {
                    switch (Mode)
                    {
                        case 0:
                            index = "EYE";
                            break;
                        case 1:
                            index = "OSCilloscope";
                            break;
                        case 2:
                            index = "JITTer";
                            break;
                        default:
                            index = "EYE";
                            break;
                    }
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("Mode is" + index);
                        return this.WriteString(":system:mode " + index);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":system:mode " + index);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":system:mode?");
                                readtemp = this.ReadString();
                                if (readtemp == index + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("Mode is" + index);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("set Mode wrong");

                            }

                        }
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

                }
            }
        }
        /// <summary>
        /// 显示RisTime或者FallTime
        /// </summary>
        /// <param name="RFtime">
        /// 0=RiseTime,1=FallTime
        /// </param>
        /// <returns></returns>
        private bool DisplayRiseFallTime(byte RFtime)
        {
            lock (syncRoot)
            {
                string index;
                try
                {
                    switch (RFtime)
                    {
                        case 0:
                            index = "RISetime";
                            break;
                        case 1:
                            index = "FALLtime";
                            break;
                        default:
                            index = "RISetime";
                            break;
                    }
                    Log.SaveLogToTxt("RiseFallTime is" + index);
                    return this.WriteString(":MEASure:EYE:" + index);// + ":SOURce1 CHAN" + channel
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

                }
            }
        }
        /// <summary>
        /// 显示JitterPP或者JitterRMS
        /// </summary>
        /// <param name="jitterFormat">
        /// 0=JitterPP,1=JitterRMS
        /// </param>
        /// <returns></returns>
        public override bool DisplayJitter(byte jitterFormat)////[20160419]Nate: change private to public
        {
            lock (syncRoot)
            {
                string index;
                try
                {
                    switch (jitterFormat)
                    {
                        case 0:
                            index = "PP";
                            break;
                        case 1:
                            index = "RMS";
                            break;
                        default:
                            index = "PP";
                            break;
                    }
                    Log.SaveLogToTxt("jitterFormat is " + index);
                    this.WriteString(":MEASure:EYE:JITTer" + ":FORMat " + index);
                    return this.WriteString(":MEASure:EYE:JITTer");
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

                }
            }
        }
        public override bool DisplayEyeHeight()
        {
            lock (syncRoot)
            {
                try
                {
                    return this.WriteString(":MEASure:EYE:EyeHeight");
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

                }
            }
        }
        public override bool DisplayEyeWidth()
        {
            lock (syncRoot)
            {
                try
                {
                    return this.WriteString(":MEASure:EYE:EyeWidth");
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

                }
            }
        }
      
        public override bool DisplayCrossing()
        {
            lock (syncRoot)
            {
                try
                {
                    return this.WriteString(":MEASure:EYE:" + "CROSsing");
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

                }
            }
        }
        /// <summary>
        /// 显示其他指标
        /// </summary>
        /// <param name="measureItem">
        /// 0=BitRate,1=Amplitude,2=CrossingTime,3=SNR,4=OneLevel,5=ZeroLevel
        /// </param>
        /// <returns></returns>
        private bool DisplayOther(byte measureItem)
        {
            lock (syncRoot)
            {
                string index;
                try
                {
                    switch (measureItem)
                    {
                        case 0:
                            index = "BITRate";
                            break;
                        case 1:
                            index = "AMPLitude";
                            break;
                        case 2:
                            index = "ECTime";
                            break;
                        case 3:
                            index = "ESN";
                            break;
                        case 4:
                            index = "OLEVel";
                            break;
                        case 5:
                            index = "ZLEVel";
                            break;
                        default:
                            index = "BITRate";
                            break;
                    }
                    Log.SaveLogToTxt("measureItem is" + index);
                    this.WriteString(":MEASure:EYE:" + index);
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

                }
            }
        }
        /// <summary>
        /// 显示ER
        /// </summary>
        /// <param name="erUnit">
        /// 0=Decibel,1=Percent,2=Ratio
        /// </param>
        /// <returns></returns>
        private bool DisplayER1(byte erUnit)
        {
            lock (syncRoot)
            {
                string index;
                try
                {
                    switch (erUnit)
                    {
                        case 0:
                            index = "DECibel";
                            break;
                        case 1:
                            index = "PERCent";
                            break;
                        case 2:
                            index = "RATio";
                            break;
                        default:
                            index = "DECibel";
                            break;
                    }
                    Log.SaveLogToTxt("erUnit is" + index);
                    this.WriteString(":MEASure:EYE:ERATio" + ":UNITs " + index);
                    return this.WriteString(":MEASure:EYE:ERATio");
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

                }
            }
        }
        /// <summary>
        /// 显示ER
        /// </summary>
        /// <param name="erUnit">
        /// 0=Decibel,1=Percent,2=Ratio
        /// </param>
        /// <returns></returns>
        private bool DisplayBitRate()
        {
            lock (syncRoot)
            {
                try
                {

                    return this.WriteString(":MEASure:EYE:BITRate");
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

                }
            }
        }
      override public bool DisplayER()
      {
          lock (syncRoot)
          {
              return DisplayER1(0);
          }
      }
        /// <summary>
        /// 显示平均光功率
        /// </summary>
        /// <param name="powerUnits">
        /// 0=Watt,1=dBm
        /// </param>
        /// <returns></returns>
        private bool DisplayPower(byte powerUnits)
        {
            lock (syncRoot)
            {
                string index;
                try
                {
                    switch (powerUnits)
                    {
                        case 0:
                            index = "WATT";
                            break;
                        case 1:
                            index = "DBM";
                            break;
                        default:
                            index = "WATT";
                            break;
                    }
                    Log.SaveLogToTxt("powerUnits is" + index);
                    this.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + index);
                    return this.WriteString(":MEASure:EYE:APOWer");
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

                }
            }
        }
        /// <summary>
        /// 清除所有显示项
        /// </summary>
        /// <returns></returns>
        private bool DisplayClearAlllist()
        {
            lock (syncRoot)
            {
                try
                {
                    return this.WriteString(":MEASure:EYE:LIST:CLEar");
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

                }
            }
        }
        override public bool ClearDisplay()
        {
            lock (syncRoot)
            {
                try
                {
                    return this.WriteString(":ACQuire:CDISplay");
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

                }
            }
        }
        /// <summary>
        /// 眼图累积限制方式选择
        /// </summary>
        /// <param name="limitCondition">
        /// 累积方式（0=Waveforms,1=Samples）
        /// </param>
        /// <param name="limitNumber">
        /// 累积数
        /// </param>
        /// <returns></returns>
        private bool AcquisitionLimitTestSetup(byte limitCondition, int limitNumber, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string index;
                try
                {
                    switch (limitCondition)
                    {
                        case 0:
                            index = "WAV";
                            break;
                        case 1:
                            index = "SAMP";
                            break;
                        default:
                            index = "WAV";
                            break;
                    }
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("limitCondition is" + index + "limitNumber is" + limitNumber);
                        return this.WriteString(":LTESt:ACQuire:CTYPe " + index + ";" + ":LTESt:ACQuire:CTYPe:" + index + " " + limitNumber.ToString());

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":LTESt:ACQuire:CTYPe " + index + ";" + ":LTESt:ACQuire:CTYPe:" + index + " " + limitNumber.ToString());

                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":LTESt:ACQuire:CTYPe?");
                                readtemp = this.ReadString();
                                if (readtemp == index + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("limitCondition is" + index + "limitNumber is" + limitNumber);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("AcquisitionLimitTestSetup wrong");

                            }

                        }
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

                }
            }
        }
        public override bool AutoScale(int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {

                    return WriteOpc(":SYSTem:AUToscale", 0.01, 5);

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

                }
            }
        }
        public override bool DisplayThreeEyes(int syn = 0)
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
                        Log.SaveLogToTxt("display eye num is" + (3.0000).ToString());
                        return this.WriteString(":TIMebase:BRANge " + (3.0000).ToString());
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":TIMebase:BRANge " + (3.0000).ToString());
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":TIMebase:BRANge?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble((3.0000).ToString()))
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("display eye num is" + (3.0000).ToString());
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("display eye num set is wrong");

                            }

                        }
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

                }
            }
        }
        /// <summary>
        /// 眼图累积限制开关
        /// </summary>
        /// <param name="Switch">
        /// 1=On,0=Off
        /// </param>
        /// <returns></returns>

        override public bool SetRunTilOff(int syn = 0)
        {
            lock (syncRoot)
            {
                return AcquisitionLimitTestSwitch(0);
            }
        }
        private bool AcquisitionLimitTestSwitch(byte Switch, int syn = 0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    if (Switch == 1)
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("AcquisitionLimitTestSwitch is on");
                            return this.WriteString(":LTESt:ACQuire:STATe ON");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":LTESt:ACQuire:STATe ON");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":LTESt:ACQuire:STATe?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "1\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("AcquisitionLimitTestSwitch is on");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set AcquisitionLimitTestSwitch wrong");

                                }

                            }
                            return flag;
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            Log.SaveLogToTxt("AcquisitionLimitTestSwitch is off");
                            return this.WriteString(":LTESt:ACQuire:STATe OFF");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = this.WriteString(":LTESt:ACQuire:STATe OFF");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {

                                    this.WriteString(":LTESt:ACQuire:STATe?");
                                    readtemp = this.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    Log.SaveLogToTxt("AcquisitionLimitTestSwitch is off");
                                    flag = true;
                                }
                                else
                                {
                                    Log.SaveLogToTxt("set AcquisitionLimitTestSwitch wrong");

                                }

                            }
                            return flag;
                        }
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

                }
            }
        }
        /// <summary>
        /// 保存眼图到电脑指定文件
        /// </summary>
        /// <param name="savePath">
        /// 眼图保存完整路径，例如@"D:\EyeDiagram\eye.gif"
        /// </param>
        /// <param name="picColor">
        /// 图片颜色（0=彩色,1=黑白）
        /// </param>
        /// <param name="bgColor">
        /// 背景色（0=黑色，1=白色）
        /// </param>
        /// <returns></returns>
        private bool SaveEyeDiagram(string savePath, byte picColor, byte bgColor)
        {
            lock (syncRoot)
            {
                string diagramName = "";
                string INVert = "OFF";
                string MONochrome = "OFF";
                if (picColor == 1) MONochrome = "ON";
                if (bgColor == 1) INVert = "ON";
                try
                {
                    //this.WriteString(":DISK:SIMage:FNAMe 'screenshot.gif';" + ":DISK:SIMage:INVert " + INVert + ";" + ":DISK:SIMage:MONochrome " + MONochrome + "\n");
                    //this.WriteString(":DISK:SIMage:FNAMe 'screenshot.gif'");
                    this.WriteString(@":DISK:SIMage:FNAMe 'D:\User Files\screenshot.gif'");
                    this.WriteString(":DISK:SIMage:FNAMe?");
                    string ImagePath = this.ReadString().Replace("\n", "");


                    this.WriteString(":DISK:SIMage:INVert " + INVert + ";" + ":DISK:SIMage:MONochrome " + MONochrome + "\n");

                    this.WriteString(":DISK:SIMage:SAVE;*OPC?");
                    for (int j = 0; j < 20; j++)
                    {
                        try
                        {
                            if (this.ReadString(100).Contains("1"))
                            {
                                Log.SaveLogToTxt("Scope savePath is " + ImagePath);
                                break;
                            }
                            else
                            {
                                Thread.Sleep(100);
                                if (j == 19)
                                {
                                    Log.SaveLogToTxt("save Eye Error");
                                    return false;

                                }
                            }
                        }
                        catch (Exception ee)
                        {

                            Log.SaveLogToTxt(ee.Message);
                            return false;
                        }
                    }
                    //this.WriteString(":DISK:BFILe? " + ImagePath);
                    //savePath = pglobalParameters.StrPathEyeDiagram; 
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    System.DateTime CurrentTime = new System.DateTime();
                    CurrentTime = System.DateTime.Now;
                    string Str_Time = CurrentTime.ToString();
                    string Str_Year = CurrentTime.Year.ToString();
                    string Str_Month = CurrentTime.Month.ToString();
                    string Str_Day = CurrentTime.Day.ToString();
                    string Str_Hour = CurrentTime.Hour.ToString();
                    string Str_minute = CurrentTime.Minute.ToString();
                    string Str_sencond = CurrentTime.Second.ToString();
                    diagramName = pglobalParameters.CurrentSN + "-" + pglobalParameters.CurrentTemp + "-" + pglobalParameters.CurrentVcc + "-" + pglobalParameters.CurrentChannel + "-" + Str_Year + "_" + Str_Month + "_" + Str_Day + "_" + Str_Hour + "_" + Str_minute + "_" + Str_sencond + ".bmp";
                    File.WriteAllBytes(savePath + diagramName, GetImage(ImagePath));
                    Log.SaveLogToTxt("savePath is" + savePath + diagramName);
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

                }
            }
        }

        private byte[] GetImage(string imagePath)
        {
            lock (syncRoot)
            {
                return (byte[])myIO.ReadIEEEBlock(IOPort.Type.GPIB, "GPIB0::" + Addr, ":DISK:BFILe? " + imagePath);
            }
        }

        /// <summary>
        /// 读取平均光功率
        /// </summary>
        /// <param name="powerUnit">
        /// 0=uW,1=dBm
        /// </param>
        /// <returns></returns>
        private double ReadPower(byte powerUnit)
        {
            lock (syncRoot)
            {
                double power = 0.0;
                string unit;
                bool flag = false;
                try
                {
                    switch (powerUnit)
                    {
                        case 0:
                            unit = "WATT";
                            break;
                        case 1:
                            unit = "DBM";
                            break;
                        default:
                            unit = "DBM";
                            break;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        this.WriteString(":MEASure:EYE:APOWer:Status?");
                        string ss = this.ReadString();
                        if (ss.Contains("CORR"))
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }

                    }
                    if (flag == false)
                    {
                        Log.SaveLogToTxt("power is not read");

                    }
                    this.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + unit);
                    this.WriteString(":MEASure:EYE:APOWer?");
                    string s = this.ReadString(32);
                    double p1 = Convert.ToDouble(s);
                    if (unit == "WATT")
                        power = p1 * 1000000.0;
                    else
                        power = p1;
                    Log.SaveLogToTxt("powerUnit is" + unit + "power is" + power);
                    return power;
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

                }
            }
        }
        public override bool DisplayPowerWatt()
        {
            lock (syncRoot)
            {
                try
                {
                    this.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + "WATT");
                    this.WriteString(":MEASure:EYE:APOWer");
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

                }
            }
        }
        public override bool DisplayPowerdbm()
        {
            lock (syncRoot)
            {
                try
                {
                    this.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + "DBM");
                    this.WriteString(":MEASure:EYE:APOWer");
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

                }
            }
        }

        /// <summary>
        /// 读取ER
        /// </summary>
        /// <param name="erUnits">
        /// 0=Decibel,1=Percent,2=Ratio
        /// </param>
        /// <returns></returns>
        private double ReadER(byte erUnits)
        {
            lock (syncRoot)
            {
                string index;
                double er = 0.0;
                bool flag = false;
                try
                {
                    switch (erUnits)
                    {
                        case 0:
                            index = "DECibel";
                            break;
                        case 1:
                            index = "PERCen";
                            break;
                        case 2:
                            index = "RATio";
                            break;
                        default:
                            index = "DECibel";
                            break;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        this.WriteString(":MEASure:EYE:ERATio:Status?");
                        string ss = this.ReadString();
                        if (ss.Contains("CORR"))
                        {
                            flag = true;
                            break;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }

                    }
                    if (flag == false)
                    {
                        Log.SaveLogToTxt("power is not read");

                    }
                    this.WriteString(":MEASure:EYE:ERATio" + ":UNITs " + index);
                    this.WriteString(":MEASure:EYE:ERATio?");
                    er = Convert.ToDouble(this.ReadString(256));
                    Log.SaveLogToTxt("erUnits is" + index + "er is" + er);
                    return er;
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

                }
            }
        }
        public override double GetCrossing()
        {
            lock (syncRoot)
            {
                double crossing = 0.0;
                try
                {
                    this.WriteString(":MEASure:EYE:CROSsing?");
                    crossing = Convert.ToDouble(this.ReadString(256));
                    Log.SaveLogToTxt("crossing is" + crossing);
                    return crossing;
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

                }
            }
        }
        /// <summary>
        /// 读取眼图OMA，返回值为mW或者mV
        /// </summary>
        /// <returns></returns>
        private double ReadAMPLitude()
        {
            lock (syncRoot)
            {
                double AMPLitude = 0.0;
                try
                {
                    this.WriteString(":MEASure:EYE:AMPLitude?");
                    AMPLitude = Convert.ToDouble(this.ReadString(32)) * 1000;
                    Log.SaveLogToTxt("AMPLitude is" + AMPLitude);
                    return AMPLitude;
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

                }
            }
        }
        /// <summary>
        /// 读取JitterPP或者JitterRMS
        /// </summary>
        /// <param name="jitterformat">
        /// 0=JitterPP,1=JitterRMS
        /// </param>
        /// <returns></returns>
        private double ReadJitter(byte jitterformat)
        {
            lock (syncRoot)
            {
                double jitter = 0.0;
                string index;
                try
                {
                    switch (jitterformat)
                    {
                        case 0:
                            index = "PP";
                            break;
                        case 1:
                            index = "RMS";
                            break;
                        default:
                            index = "PP";
                            break;
                    }
                    this.WriteString(":MEASure:EYE:JITTer" + ":FORMat " + index);
                    this.WriteString(":MEASure:EYE:JITTer?");
                    jitter = (Convert.ToDouble(this.ReadString(16))) * 1000000000000.0;
                    Log.SaveLogToTxt("jitter is" + jitter);
                    return jitter;
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

                }
            }
        }
        /// <summary>
        /// 读取眼图上升沿或者下降沿时间
        /// </summary>
        /// <param name="RFtime">
        /// 0=RiseTime,1=FallTime
        /// </param>
        /// <returns></returns>
        private double ReadRiseFallTime(byte RFtime)
        {
            lock (syncRoot)
            {
                double time = 0.0;
                string index;
                try
                {
                    switch (RFtime)
                    {
                        case 0:
                            index = "RISetime";
                            break;
                        case 1:
                            index = "FALLtime";
                            break;
                        default:
                            index = "RISetime";
                            break;
                    }
                    this.WriteString(":MEASure:EYE:" + index + "?");
                    time = (Convert.ToDouble(this.ReadString(16))) * (1E+12);
                    Log.SaveLogToTxt("RFtime is" + index + "time is" + time);
                    return time;
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

                }
            }
        }
        /// <summary>
        /// 读取模板余量
        /// </summary>
        /// <returns></returns>
        private double ReadMaskMargin()
        {
            lock (syncRoot)
            {
                double Margin = 0.0;
                try
                {
                    this.WriteString(":MEASure:MTESt:MARgin?");
                    Margin = Convert.ToDouble(this.ReadString(32));
                    Log.SaveLogToTxt("Margin is" + Margin);
                    return Margin;
                }
                catch (InnoExCeption error)
                {
                    // Log.SaveLogToTxt("ErrorCode="+  ExceptionDictionary.Code._0x05002 +"Reason=" +error.TargetSite.Name + "Fail");
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
         /// <summary>
        /// 读取眼高
        /// </summary>
        /// <returns></returns>
        private double ReadEyeHeight()
        {
            lock (syncRoot)
            {
                double height = 0.0;
                try
                {
                    this.WriteString(":MEASure:EYE:EHEight?");
                    height = Convert.ToDouble(this.ReadString(32)) * 1000.0;
                    Log.SaveLogToTxt("eye height is" + height);
                    return height;
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

                }
            }
        }
        /// <summary>
        /// 读取眼高
        /// </summary>
        /// <returns></returns>
        private double ReadBitRate()
        {
            lock (syncRoot)
            {
                double biTRate = 0.0;
                try
                {
                    this.WriteString(":MEASure:EYE:BitRate?");
                    double Str = Convert.ToDouble(this.ReadString());
                    biTRate = Str / (1E+9);
                    //height = Convert.ToDouble(this.ReadString(32)) * 1000.0;
                    Log.SaveLogToTxt("biTRate is" + biTRate + "GB/S");
                    return biTRate;
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

                }
            }
        }
        /// <summary>
        /// 读取眼宽
        /// </summary>
        /// <returns></returns>
        private double ReadEyeWidth()
        {
            lock (syncRoot)
            {
                double width = 0.0;
                try
                {
                    this.WriteString(":MEASure:EYE:EWIDth?");
                    width = Convert.ToDouble(this.ReadString(32)) * (1E+12);
                    Log.SaveLogToTxt("eye width is" + width);
                    return width;
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

                }
            }
        }
        #endregion

        public override double GetTxRinOmA()
        {
            lock (syncRoot)
            {
                try
                {


                    this.WriteString(":MEASure:AMPLitude:DEFine:ANALysis ON");
                    this.WriteString(":MEASure:AMPLitude:DEFine:LOCATION 50");
                    this.WriteString(":DISPLAY:AMPLitude:LEVel BOTH");
                    this.WriteString(":MEASure:AMPLitude:DEFine:UNITs WATT");
                    this.WriteString(":MEASure:AMPLitude:DEFine:RINoise:TYPe OMA");
                    this.WriteString(":MEASure:AMPLitude:DEFine:RINoise:UNITs DBHZ");
                    this.WriteString(":MEASure:AMPLitude:DEFine:LEVel CIDigits");
                    this.WriteString(":MEASure:AMPLitude:DEFine:LEVel:CIDigits:LAGGing 2");
                    this.WriteString(":MEASure:AMPLitude:DEFine:LEVel:CIDigits:LEADing 2");
                    this.WriteString(":MEASure:AMPLitude:RINoise?");

                    string buf = this.ReadString(8);
                    double rinOMA = -1.0;
                    bool result = double.TryParse(buf, out rinOMA);
                    if (result)
                    {
                        return rinOMA;
                    }
                    else
                    {
                        return 100;
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

                }
            }
        }


        public override void GetAllChannel_Mask_OMA( int ChannelCount,out double[] MaskArray, out double[] OMAArray)
        {
            lock (syncRoot)
            {
                MaskArray = new double[4];
                OMAArray = new double[4];
                for (int i = 1; i < 2; i++)
                {
                    LoadMask(i.ToString(), opticalMaskName, 0);
                    MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, 0);
                    //MaskTestMarginSetup(1,1,0,)
                    MaskArray[i - 1] = ReadMaskMargin();
                    OMAArray[i - 1] = ReadAMPLitude();
                }
            }
          
          // LoadMask()
        }

        public override double[] MeasureVCMI()//nate add
        {
            lock (syncRoot)
            {
                try
                {


                    string strChannel = strElecChannel[Convert.ToByte(CurrentChannel) - 1];//"3A"
                    char[] subChannel = strChannel.ToCharArray();
                    subChannel[1]++;

                    string[] channel = new string[2] { strChannel, subChannel[0] + subChannel[1].ToString() };//{3A, 3B}

                    SetMode(1);//scope mode

                    ClearDisplay();

                    this.WriteString(":FUNCtion1:FOPerator CMODe");
                    this.WriteString(":FUNCtion1:OPERand1 CHAN" + channel[0]);
                    this.WriteString(":FUNCtion1:OPERand2 CHAN" + channel[1]);
                    this.WriteString(":FUNCtion1:COLor TCOLor6");
                    this.WriteString(":FUNCtion1:DISPlay ON");
                    this.WriteString(":MEASure:OSCilloscope:VRMS:AREa REGion");
                    this.WriteString(":MEASure:OSCilloscope:VRMS:TYPe AC");
                    this.WriteString(":MEASure:OSCilloscope:VRMS");
                    this.WriteString(":MEASure:OSCilloscope:VRMS?");
                    Thread.Sleep(2000);

                    string buf = this.ReadString();
                    double[] vcmi = { -1.0, -1.0 };
                    bool result = double.TryParse(buf, out vcmi[0]);
                    if (result == false)
                    {
                        vcmi[0] = -1.0;
                    }
                    else
                    {
                        vcmi[0] = vcmi[0] * 1000;
                    }

                    this.WriteString(":MEASure:OSCilloscope:VRMS:TYPe DC");
                    this.WriteString(":MEASure:OSCilloscope:VRMS");
                    this.WriteString(":MEASure:OSCilloscope:VRMS?");
                    Thread.Sleep(2000);

                    buf = this.ReadString();
                    result = double.TryParse(buf, out vcmi[1]);
                    if (result == false)
                    {
                        vcmi[1] = -1.0;
                    }
                    else
                    {
                        vcmi[1] = vcmi[1] * 1000;
                    }

                    bool flag = false;
                    for (int k = 0; k < 3; k++)
                    {
                        flag = SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
                        if (flag) break;
                    }
                    if (!flag)
                    {
                        MessageBox.Show("眼图保存失败!!!");
                        Log.SaveLogToTxt("Save Eye Image Error");
                    }

                    this.WriteString(":FUNCtion1:FOPerator NONE");
                    //this.WriteString(":DIFF3A:DISPlay ON");

                    return vcmi;
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

                }
            }
        }

        public override bool MeasureVEC(out double[] result, int syn = 0)//nate add
        {
            lock (syncRoot)
            {
                result = new double[3];
                int count = 0;
                bool isOk = false;
                int i;
                string strChannel = "3A";

                try
                {
                    strChannel = strElecChannel[Convert.ToByte(CurrentChannel) - 1];

                    ClearDisplay();
                    ChannelDisplaySwitch(strChannel, 0, 1);

                    this.WriteString(":DIFF" + strChannel + ":DISPlay ON");

                    EyeTuningDisplay(0, 1);
                    this.WriteString(":ACQuire:REYE ON");
                    this.WriteString(":DISPlay:TOVerlap OFF");
                    SetMode(0, syn);
                    AcquisitionControl(2);
                    AcquisitionLimitTestSwitch(0, syn);
                    MaskONOFF(false, syn);
                    DisplayClearAlllist();
                    DisplayRiseFallTime(1);
                    DisplayRiseFallTime(0);
                    DisplayJitter(1);
                    DisplayJitter(0);
                    this.WriteString(":MEASure:EYE:EWIDth");
                    this.WriteString(":MEASure:EYE:EHEight");
                    DisplayCrossing();
                    DisplayOther(1);
                    AcquisitionControl(0);
                    AutoScale(syn);
                    Thread.Sleep(2000);// 因为Mask 变得很大,在此时反复清屏,以求让眼图显示稳定
                    ClearDisplay();
                    Thread.Sleep(2000);
                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
                    AcquisitionLimitTestSwitch(1, syn);
                    AcquisitionControl(2);
                    //LoadMask(strChannel, elecMaskName, syn);
                    //MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                    do
                    {
                        Thread.Sleep(200);
                        result[0] = ReadEyeHeight();
                        result[1] = GetAMP_5();
                        result[2] = 20 * Math.Log10(result[1] / result[0]);

                        bool flag = false;
                        for (int k = 0; k < 3; k++)
                        {
                            flag = SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
                            if (flag) break;
                        }
                        if (!flag)
                        {
                            MessageBox.Show("眼图保存失败!!!");
                            Log.SaveLogToTxt("Save Eye Image Error");
                        }

                        for (i = 0; i < result.Length; i++)
                        {
                            if (result[i] >= 10000000) break;
                        }
                        if (i >= result.Length)
                        {
                            isOk = true;
                            break;
                        }
                        else
                            Thread.Sleep(200);
                    } while (count++ < 3);

                    AcquisitionLimitTestSwitch(0, syn);
                    AcquisitionControl(0);
                    MaskONOFF(false, syn);

                    this.WriteString(":ACQuire:REYE OFF");
                    this.WriteString(":DIFF" + strChannel + ":DISPlay OFF");
                    return isOk;
                }

                catch (InnoExCeption error)
                {
                    this.WriteString(":ACQuire:REYE OFF");
                    this.WriteString(":DIFF" + strChannel + ":DISPlay OFF");
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {
                    this.WriteString(":ACQuire:REYE OFF");
                    this.WriteString(":DIFF" + strChannel + ":DISPlay OFF");
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }//nate add

        public override double GetAMP_5()//nate add
        {
            lock (syncRoot)
            {
                double amp_5 = 0.0;
                bool flag = false;
                try
                {
                    this.WriteString(":MEASure:EBOundary:LEFT 5.25E+1");
                    this.WriteString(":MEASure:EBOundary:RIGHt 5.75E+1");
                    this.WriteString(":MEASure:EYE:AMPLitude");
                    this.WriteString(":MEASure:EYE:AMPLitude?");
                    amp_5 = Convert.ToDouble(this.ReadString(32)) * 1000;
                    Log.SaveLogToTxt("AMPLitude of 5% is" + amp_5);

                    //for (int k = 0; k < 3; k++)
                    //{
                    //    flag = SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
                    //    if (flag) break;
                    //}
                    //if (!flag)
                    //{
                    //    MessageBox.Show("眼图保存失败!!!");
                    //    Log.SaveLogToTxt("Save Eye Image Error");                    
                    //}
                    return amp_5;
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

                }
                //finally
                //{
                //    this.WriteString(":MEASure:EBOundary:LEFT 4.00E+1");
                //    this.WriteString(":MEASure:EBOundary:RIGHt 6.00E+1");
                //}
            }
        }

        public override double GetJitterPP()//nate add
        {
            lock (syncRoot)
            {
                double jitterPP = 0.0;
                try
                {
                    jitterPP = this.ReadJitter(0);
                    Log.SaveLogToTxt("JitterPP is " + jitterPP);
                    return jitterPP;
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

                }
            }
        }

        public override bool SavaScreen(string filePath)
        {
            lock (syncRoot)
            {
                //return this.SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
                return this.SaveEyeDiagram(filePath, 0, 0);
            }
        }

        public override bool PtimebaseStatue(bool isON)
        {
            lock (syncRoot)
            {
                try
                {


                    if (isON)
                    {
                        return this.WriteString(":TIMebase:PTImebase:STATe ON");
                    }
                    else
                    {
                        return this.WriteString(":TIMebase:PTImebase:STATe OFF");
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

                }
            }
        }

        public override bool TriggerSourceFpanel()
        {
            lock (syncRoot)
            {
                try
                {


                    return this.WriteString(":TRIGger:SOURce FPANel");
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

                }
            }
        }

        public override bool TriggerPlock(bool isON)
        {
            lock (syncRoot)
            {
                try
                {


                    if (isON)
                    {
                        return this.WriteString(":TRIGger:PLOCK ON");
                    }
                    else
                    {
                        return this.WriteString(":TRIGger:PLOCK OFF");
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

                }
            }
        }

        public override double GetTxTDEC(int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {
                    string strChannel = "1A";
                    strChannel = strOpticalChannel[Convert.ToByte(CurrentChannel) - 1];

                    FileterSelect(strChannel, 16.8E+9);   //设置TDEC滤波速率:TDEC(12.6 GHz)

                    EyeTuningDisplay(1);

                    this.WriteString(":ACQuire:REYE ON");
                    this.WriteString(":DISPlay:TOVerlap OFF");

                    SetMode(0, syn);
                    AcquisitionControl(2);
                    AcquisitionLimitTestSwitch(0, syn);
                    MaskONOFF(false, syn);
                    DisplayClearAlllist();

                    this.WriteString(":MEASure:EYE:TDEC");   //显示TDEC项

                    AcquisitionControl(0);
                    AutoScale(syn);
                    ClearDisplay();
                    Thread.Sleep(2000);
                    EyeTuningDisplay(0);// 关闭眼图刷新
                    Thread.Sleep(2000);
                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);

                    AutoScale(syn);
                    ClearDisplay();
                    Thread.Sleep(2000);

                    AcquisitionLimitTestSwitch(1, syn);
                    AcquisitionControl(2);

                    this.WriteString(":MEASure:EYE:TDEC?");

                    string s = this.ReadString();
                    double TDEC = -1.0;

                    bool result = double.TryParse(s, out TDEC);

                    AcquisitionLimitTestSwitch(0, syn);
                    AcquisitionControl(0);

                    FileterSelect(strChannel, FlexDcaDataRate);              //恢复示波器滤波速率

                    bool SaveFlag = false;
                    for (int k = 0; k < 3; k++)
                    {
                        SaveFlag = SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram, 0, 0);//保存眼图
                        if (SaveFlag) break;
                    }

                    if (result)
                    {
                        return TDEC;
                    }
                    else
                    {
                        return 100;
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

                }
            }
        }

        public override bool MeasureRxEyeVecEW(ref Dictionary<string,double> result, int syn = 0)
        {
            lock (syncRoot)
            {
                int count = 0;
                bool isOk = false;
                string strChannel = "3A";
                try
                {
                    strChannel = strElecChannel[Convert.ToByte(CurrentChannel) - 1];

                    ClearDisplay();
                    ChannelDisplaySwitch(strChannel, 0, 1);

                    this.WriteString(":DIFF" + strChannel + ":DISPlay ON");

                    EyeTuningDisplay(0, 1);
                    this.WriteString(":ACQuire:REYE OFF");
                    this.WriteString(":ACQuire:RSPec RLENgth");
                    this.WriteString(":ACQuire:RLENgth:MODE MANual");
                    this.WriteString(":ACQuire:RLENgth 10000");

                    this.WriteString(":DISPlay:TOVerlap OFF");
                    SetMode(0, syn);
                    AcquisitionControl(2);
                    AcquisitionLimitTestSwitch(0, syn); ;
                    DisplayClearAlllist();
                    AcquisitionControl(0);
                    AutoScale(syn);
                    ClearDisplay();
                    Thread.Sleep(2000);
                    EyeTuningDisplay(0);// 关闭眼图刷新
                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
                    do
                    {
                        AutoScale(syn);
                        ClearDisplay();
                        Thread.Sleep(2000);

                        AcquisitionLimitTestSwitch(1, syn);
                        int time = acqLimitNumber / 100 * 30000;
                        Thread.Sleep(time);
                        AcquisitionControl(0);

                        Thread.Sleep(200);
                        double eyeWidth_4 = GetEyeWidthWithProbability(4);
                        double eyeHeight_4 = GetEyeHeightWithProbability(4);
                        double eyeWidth_6 = GetEyeWidthWithProbability(6);
                        double eyeHeight_6 = GetEyeHeightWithProbability(6);
                        double eyeHeight_15 = eyeHeight_6 - 3.19 * (eyeHeight_4 - eyeHeight_6);
                        double eyeWidth_15 = eyeWidth_6 - 3.19 * (eyeWidth_4 - eyeWidth_6);
                        double amp_5 = GetAMP_5();
                        double vec = (eyeHeight_15 == 0) ? 0 : 20 * Math.Log(amp_5 / eyeHeight_15, 10);

                        result.Add("amp_5", amp_5);
                        result.Add("EyeHeight_4", eyeHeight_4);
                        result.Add("EyeHeight_6", eyeHeight_6);
                        result.Add("EyeHeight_15(mV)", eyeHeight_15);
                        result.Add("EyeWidth_4", eyeWidth_4);
                        result.Add("EyeWidth_6", eyeWidth_6);
                        result.Add("EyeWidth_15(ps)", eyeWidth_15);
                        result.Add("VEC(dB)", vec);

                        DisplayEyeContours(4, 1);
                        DisplayEyeContours(6, 2);

                        bool flag = false;
                        for (int k = 0; k < 3; k++)
                        {
                            flag = SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
                            if (flag) break;
                        }
                        if (!flag)
                        {
                            MessageBox.Show("眼图保存失败!!!");
                            Log.SaveLogToTxt("Save Eye Image Error");
                        }

                        int j = 0;
                        foreach (string key in result.Keys)
                        {
                            if (result[key] >= 10000000) break;
                            j++;
                        }
                        if (j >= result.Keys.Count)
                        {
                            isOk = true;
                            break;
                        }
                        else
                            Thread.Sleep(200);
                    } while (count++ < 3);

                    AcquisitionLimitTestSwitch(0, syn);
                    AcquisitionControl(0);
                    MaskONOFF(false, syn);

                    this.WriteString(":ACQuire:REYE OFF");
                    this.WriteString(":DIFF" + strChannel + ":DISPlay OFF");
                    return isOk;
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

                }
            }
        }

        private double GetEyeHeightWithProbability(int index)
        {
            lock (syncRoot)
            {
                double height = 0.0;
                try
                {
                    this.WriteString(":MEASure:EYE:PAM:EHeight:DEFine:EOPening PROBability");
                    this.WriteString(":MEASure:EYE:PAM:EHeight:DEFine:EOPening:PROBability 1.0E-" + index);
                    this.WriteString(":MEASure:EYE:PAM:EHEight");
                    this.WriteString(":MEASure:EYE:PAM:EHEight?");
                    height = Convert.ToDouble(this.ReadString(32)) * 1000.0;
                    Log.SaveLogToTxt("eye height is" + height + " with PROBability 1.0E-" + index);
                    return height;
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

                }
            }
        }

        private double GetEyeWidthWithProbability(int index)
        {
            lock (syncRoot)
            {
                double width = 0.0;
                try
                {
                    this.WriteString(":MEASure:EYE:PAM:EWIDth:DEFine:EOPening PROBability");
                    this.WriteString(":MEASure:EYE:PAM:EWIDth:DEFine:EOPening:PROBability 1.0E-" + index);
                    this.WriteString(":MEASure:EYE:PAM:EWIDth");
                    this.WriteString(":MEASure:EYE:PAM:EWIDth?");
                    width = Convert.ToDouble(this.ReadString(32)) * (1E+12);
                    Log.SaveLogToTxt("eye width is" + width + " with PROBability 1.0E-" + index);
                    return width;
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

                }
            }
        }

        private void DisplayEyeContours(int indexOfBERates, int numOfECOtour)
        {
            lock (syncRoot)
            {
                try
                {
                    this.WriteString(":ECONtour" + numOfECOtour + ":DISplay ON");
                    this.WriteString(":ECONtour" + numOfECOtour + ":BERates 1.0E-" + indexOfBERates);
                    string command = ":ECONtour" + numOfECOtour + ":PSPec PROBability";
                    this.WriteString(command);
                    WriteOpc(command, 2, 4);
                    //this.WriteString(":ECONtour" + numOfECOtour + ":DISplay OFF");
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

                }
            }
        }
    }    
}
