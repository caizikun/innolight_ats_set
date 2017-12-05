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
        public FLEX86100(logManager logmanager)
        {
            logger = logmanager;
        }
        public IOPort MY_Scope;
        protected string dcacurrentchannel;   
        public Algorithm algorithm = new Algorithm();
        public override bool Initialize(TestModeEquipmentParameters[] FLEX86100Struct)
        {
            try
            {
               
                int i = 0;
                if (algorithm.FindFileName(FLEX86100Struct,"ADDR",out i))
                {
                    Addr =FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"IOTYPE",out i))
                {
                    IOType = FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"NAME",out i))
                {
                    Name = FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"CONFIGFILEPATH",out i))
                {
                    configFilePath = FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXDCADATARATE",out i))
                {
                    FlexDcaDataRate = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FILTERSWITCH",out i))
                {
                    FilterSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXFILTERFREQ",out i))
                {
                    FlexFilterFreq = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"TRIGGERSOURCE",out i))
                {
                    triggerSource = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXTRIGGERBWLIMIT",out i))
                {
                    FlexTriggerBwlimit = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALSLOT",out i))
                {
                    opticalSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"ELECSLOT",out i))
                {
                    elecSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXDCAWAVELENGTH",out i))
                {
                    FlexDcaWavelength = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALATTSWITCH",out i))
                {
                    opticalAttSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"ERFACTORSWITCH",out i))
                {
                    erFactorSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXDCAATT",out i))
                {
                    FlexDcaAtt = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"ERFACTOR",out i))
                {
                    erFactor = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }

                else
                    return false;

                if (algorithm.FindFileName(FLEX86100Struct,"FLEXSCALE",out i))
                {
                    FlexScale = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXOFFSET",out i))
                {
                    FlexOffset = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"THRESHOLD",out i))
                {
                    Threshold = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXOPTCHANNEL",out i))
                {
                    FlexOptChannel = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXELECCHANNEL",out i))
                {
                    FlexElecChannel = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASEMODULESLOT",out i))
                {
                    precisionTimebaseModuleSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASESYNCHMETHOD",out i))
                {
                    precisionTimebaseSynchMethod = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"RAPIDEYESWITCH",out i))
                {
                    rapidEyeSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"REFERENCE",out i))
                {
                    reference = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINTYPE",out i))
                {
                    marginType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINHITTYPE",out i))
                {
                    marginHitType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"ACQLIMITTYPE",out i))
                {
                    acqLimitType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASEREFCLK",out i))
                {
                    precisionTimebaseRefClk = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINHITRATIO",out i))
                {
                    marginHitRatio = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALMASKNAME",out i))
                {
                    opticalMaskName = FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"ELECMASKNAME",out i))
                {
                    elecMaskName = FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALEYESAVEPATH",out i))
                {
                    opticalEyeSavePath = FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"ELECEYESAVEPATH",out i))
                {
                    elecEyeSavePath = FLEX86100Struct[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINHITCOUNT",out i))
                {
                    marginHitCount = Convert.ToInt16(FLEX86100Struct[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(FLEX86100Struct,"ACQLIMITNUMBER",out i))
                {
                    acqLimitNumber = Convert.ToInt16(FLEX86100Struct[i].DefaultValue);
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
        public bool ReSet()
        {
            if (MY_Scope.WriteString("*RST"))
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
            string offset = ""; 
            CurrentChannel = channel;
            if (offsetlist.ContainsKey(CurrentChannel))
                offset = offsetlist[CurrentChannel];
            double temp = Convert.ToDouble(offset);
            logger.AdapterLogString(0, "Offset is" + offset);
            return SetAttenuation(dcacurrentchannel, temp, 1);
           

        }
        public override bool configoffset(string channel, string offset)
        {
            offsetlist.Add(channel, offset);
            return true;
        }
        override public bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":

                        MY_Scope = new IOPort(IOType, "GPIB::" + Addr);

                        EquipmentConnectflag = MY_Scope.Connect_flag;

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
        /// <summary>
        /// 按照设备属性中的配置文件地址进行设备配置
        /// </summary>
        /// <returns></returns>
        private bool ConfigFromFile()
        {
            try
            {
                if (EquipmentConfigflag) { return true; }
                else
                {
                    MY_Scope.WriteString(":DISK:SETup:RECall \"" + configFilePath + "\"\n");
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
                    string strOpticalChannel = AssembleChannelString(opticalSlot, FlexOptChannel);
                    string strElecChannel = AssembleChannelString(elecSlot, FlexElecChannel);

                    MY_Scope.WriteString(":SYSTem:DEFault");
                    MY_Scope.WriteString(":ACQuire:STOP");
                    SetMode(0);
                    MY_Scope.WriteString(":DISPlay:ETUNing ON");
                    MY_Scope.WriteString(":DISPlay:TOVerlap OFF");
                    RapidEyeSetup(FlexDcaDataRate, rapidEyeSwitch);
                    TriggerSetup(triggerSource, FlexTriggerBwlimit);
                    WavelengthSelect(strOpticalChannel, FlexDcaWavelength);
                    FileterSelect(strOpticalChannel, FlexDcaDataRate);
                    FileterSwitch(strOpticalChannel, FilterSwitch);
                    for (byte i = 1; i < 5; i++)
                    {
                        MY_Scope.WriteString(":SYSTem:MODel? SLOT" + i.ToString());
                        if (MY_Scope.ReadString().Contains("Not Present")) { }
                        else
                        {
                            if (isModuleNeedCalibrate(i)) CalibrateModel(i);
                        }
                    }
                    SetAttenuation(strOpticalChannel, FlexDcaAtt, opticalAttSwitch);
                    SetERCorrectFactor(strOpticalChannel, erFactor, erFactorSwitch);
                    ///精准时基单元未添加
                    ///
                    PrecisionTimebaseSetup(precisionTimebaseModuleSlot, precisionTimebaseRefClk, 1, precisionTimebaseSynchMethod);
                    MessureThresholdSetup(Threshold, reference);
                    ChannelDisplaySwitch(strOpticalChannel, 1);
                    EyeScaleOffset(strOpticalChannel, FlexScale, FlexOffset);
                    LoadMask(strOpticalChannel, opticalMaskName);
                    MaskONOFF(false);
                    MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount);
                    AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber);
                    AcquisitionLimitTestSwitch(0);
                    AcquisitionControl(0);
                    CenterEye();
                    return true;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }


        /// <summary>
        /// 读取光功率单位uW
        /// </summary>
        /// <returns></returns>
        public override double GetAveragePowerWatt()
        {
            double power = 0;

            try
            {
                power = ReadPower(0);
                logger.AdapterLogString(0, "AveragePowerWatt is" + power);
                return power;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return power;
            }
        }
        public override double GetAveragePowerdbm()
        {
            double power = 0;

            try
            {
                power = ReadPower(1);
                logger.AdapterLogString(0, "AveragePowerdbm is" + power);
                return power;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return power;
            }
        }
        /// <summary>
        /// 读取ER单位dB
        /// </summary>
        /// <returns></returns>
        public override double GetEratio()
        {
            double er = 0;
            try
            {
                er = ReadER(0);
                logger.AdapterLogString(0, "Eratio is" + er);
                return er;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return er;
            }
        }
        /// <summary>
        /// 测试光眼图，测量结果以数组形式返回，依次为AP,ER,OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
        /// </summary>
        /// <returns></returns>
        public override double[] OpticalEyeTest()
        {
            double[] testResults = new double[9];
            try
            {
                string strChannel = "1A";
                strChannel = AssembleChannelString(opticalSlot, FlexOptChannel);
                MY_Scope.WriteString(":DISPlay:ETUNing OFF");
                MY_Scope.WriteString(":ACQuire:REYE ON");
                MY_Scope.WriteString(":DISPlay:TOVerlap OFF");
                SetAttenuation(strChannel, FlexDcaAtt, opticalAttSwitch);
                SetMode(0);
                AcquisitionControl(2);
                AcquisitionLimitTestSwitch(0);
                MaskONOFF(false);
                DisplayClearAlllist();
                DisplayRiseFallTime(1);
                DisplayRiseFallTime(0);
                DisplayJitter(1);
                DisplayJitter(0);
                DisplayCrossing();
                DisplayOther(1);
                DisplayER1(0);
                DisplayPower(1);
                AcquisitionControl(0);
                AutoScale();
                AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber);
                AcquisitionLimitTestSwitch(1);
                AcquisitionControl(2);
                LoadMask(strChannel, opticalMaskName);
                MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount);
                SaveEyeDiagram(opticalEyeSavePath, 0, 0);
                testResults[0] = ReadPower(1);
                testResults[1] = ReadER(0);
                testResults[2] = ReadAMPLitude() / 1000;
                testResults[3] = ReadCrossing();
                testResults[4] = ReadJitter(0);
                testResults[5] = ReadJitter(1);
                testResults[6] = ReadRiseFallTime(0);
                testResults[7] = ReadRiseFallTime(1);
                testResults[8] = ReadMaskMargin();
                AcquisitionLimitTestSwitch(0);
                AcquisitionControl(0);
                MaskONOFF(false);
                MY_Scope.WriteString(":DISPlay:ETUNing ON");
                return testResults;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return testResults;
            }
        }
        /// <summary>
        /// 测试电眼图，测量结果以数组形式返回，依次为OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
        /// </summary>
        /// <returns></returns>
        public override double[] ElecEyeTest()
        {
            double[] testResults = new double[7];
            try
            {
                string strChannel = "2A";
                strChannel = AssembleChannelString(elecSlot, FlexElecChannel);
                MY_Scope.WriteString(":DISPlay:ETUNing OFF");
                MY_Scope.WriteString(":ACQuire:REYE ON");
                MY_Scope.WriteString(":DISPlay:TOVerlap OFF");
                SetMode(0);
                AcquisitionControl(2);
                AcquisitionLimitTestSwitch(0);
                MaskONOFF(false);
                DisplayClearAlllist();
                DisplayRiseFallTime(1);
                DisplayRiseFallTime(0);
                DisplayJitter(1);
                DisplayJitter(0);
                DisplayCrossing();
                DisplayOther(1);
                AcquisitionControl(0);
                AutoScale();
                AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber);
                AcquisitionLimitTestSwitch(1);
                AcquisitionControl(2);
                LoadMask(strChannel, elecMaskName);
                MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount);
                SaveEyeDiagram(elecEyeSavePath, 0, 0);
                testResults[0] = ReadAMPLitude();
                testResults[1] = ReadCrossing();
                testResults[2] = ReadJitter(0);
                testResults[3] = ReadJitter(1);
                testResults[4] = ReadRiseFallTime(0);
                testResults[5] = ReadRiseFallTime(1);
                testResults[6] = ReadMaskMargin();
                AcquisitionLimitTestSwitch(0);
                AcquisitionControl(0);
                MaskONOFF(false);
                MY_Scope.WriteString(":DISPlay:ETUNing ON");
                return testResults;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return testResults;
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
            bool OPCflag = false;
            int looptime = Convert.ToInt16(totalWaitTime / readTimeDelay);
            try
            {
                MY_Scope.WriteString(command + ";*OPC?\n");
                for (int i = 0; i < looptime; i++)
                {
                    Thread.Sleep(Convert.ToInt16(readTimeDelay * 1000));
                    try
                    {
                        string returndata = MY_Scope.ReadString(100);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
            string strChannel = "1A";
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
                }
            }
            logger.AdapterLogString(0, "Channel is" + strChannel);
            return strChannel;
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
            string status;
            try
            {
                MY_Scope.WriteString(":CALibrate:SLOT" + slot.ToString() + ":STATus?");
                status = MY_Scope.ReadString(16);

                if (status.Contains("UNCALIBRATED"))
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
        private bool TriggerSetup(byte triggerSource, byte triggerBandwidth)
        {
            string str_TriggerBwlimit = "";
            try
            {
                if (triggerSource == 0)
                {
                    MY_Scope.WriteString(":TRIGger:SOURce FPANel");
                    switch (triggerBandwidth)
                    {
                        case 0:
                            str_TriggerBwlimit = "FILTered";
                            break;
                        case 1:
                            str_TriggerBwlimit = "EDGE";
                            break;
                        case 2:
                            str_TriggerBwlimit = "CLOCK";
                            break;
                        default:
                            str_TriggerBwlimit = "FILTered";
                            break;
                    }
                    logger.AdapterLogString(0, "TriggerBwlimit is" + str_TriggerBwlimit);
                    return MY_Scope.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);
                }
                else return MY_Scope.WriteString(":TRIGger:SOURce FRUN");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
            try
            {
                MessageBox.Show("准备校验slot{0}的模组，请拔除该模组上的所有输入信号再继续！", slot.ToString());
                MY_Scope.WriteString(":CALibrate:SLOT" + slot.ToString() + ":STARt");
                MY_Scope.WriteString(":CALibrate:SDONe?");
                for (int i = 0; i < 10; i++)
                {
                    if (MY_Scope.ReadString() != "") break;
                    else Thread.Sleep(500);
                }
                MY_Scope.WriteString(":CALibrate:CONTinue");
                MY_Scope.WriteString(":CALibrate:SDONe?");
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(3000);
                    try
                    {
                        if (MY_Scope.ReadString() != "") break;
                    }
                    catch
                    {
                    }
                }
                MY_Scope.WriteString(":CALibrate:CONTinue");
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool WavelengthSelect(string channel, byte waveLength)
        {
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
                logger.AdapterLogString(0, "Wavelength_index is" + Wavelength_index);
                return MY_Scope.WriteString(":CHAN" + channel + ":WAVelength" + " WAVelength" + Wavelength_index);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
                    MY_Scope.WriteString(":CHAN" + channel + ":FSELect FILTer" + filterIndex.ToString());
                    MY_Scope.WriteString(":CHAN" + channel + ":FSELect:RATe?");
                    rate = Convert.ToDouble(MY_Scope.ReadString(20));
                    thisRate = rate;
                    if (Math.Abs(rate - bitRate) < 13000000)
                    {
                        fileterExitflag = true;
                        break;
                    }
                } while (thisRate != lastRate);

                return fileterExitflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool FileterSwitch(string channel, byte filterSwith)
        {
            string index;
            try
            {
                if (filterSwith == 1)
                    index = "ON";
                else
                    index = "OFF";
                logger.AdapterLogString(0, "channel is" + channel + "filterSwith is" + index);
                return MY_Scope.WriteString(":CHAN" + channel + ":FILTer " + index);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool SetAttenuation(string channel, double attenuation, byte attSwitch)
        {
            try
            {
                if (attSwitch == 1)
                {
                    MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe ON");
                    logger.AdapterLogString(0, "channel is" + channel + "attenuation is" + attenuation);
                    return MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:DECibels " + attenuation.ToString() + "\n");
                }
                else
                    return MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe OFF");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool SetERCorrectFactor(string channel, double erFactorPercent, byte erFactorSwitch)
        {
            try
            {
                if (erFactorSwitch == 1)
                {
                    MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor ON");
                    logger.AdapterLogString(0, "channel is" + channel + "erFactorPercent is" + erFactorPercent);
                    return MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor  " + (erFactorPercent).ToString() + "\r");
                }
                else
                    return MY_Scope.WriteString(":MEASure:ERATio:CHAN1A:ACFactor OFF");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool PrecisionTimebaseSetup(byte slot, double refClkFrequency, byte Switch, byte synchMethod)
        {
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
                    MY_Scope.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod " + strSynchMethod + ";" + ":PTIMebase" + slot.ToString() + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                    bool flag1 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "ON", 0.01, 5);
                    if (flag1)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "OFF", 0.01, 5);
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool RapidEyeSetup(double bitRate, byte rapidEyeSwitch)
        {
            try
            {
                if (rapidEyeSwitch == 1)
                {
                    MY_Scope.WriteString(":ACQ:REYE " + "ON");
                    logger.AdapterLogString(0, "bitRate is" + bitRate + "rapidEyeSwitch is" + "ON");
                    return MY_Scope.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                }
                else
                    return MY_Scope.WriteString(":ACQ:REYE " + "OFF");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        /// <summary>
        /// 眼图实时刷新显示方式开关
        /// </summary>
        /// <param name="Switch">
        /// 1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        private bool EyeTuningDisplay(byte Switch)
        {
            try
            {

                if (Switch == 1)
                {
                    return MY_Scope.WriteString(":DISPlay:ETUNing ON");
                }
                else
                {
                    return MY_Scope.WriteString(":DISPlay:ETUNing OFF");
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool MessureThresholdSetup(byte threshold, byte reference)
        {
            string strthreshold;
            string strreference;
            try
            {
                switch (reference)
                {
                    case 0://One,Zero
                        strreference = "OZERo";
                        break;
                    case 1://Vtop,Vbase
                        strreference = "TBASe";
                        break;
                    default:
                        strreference = "OZERo";
                        break;
                }
                MY_Scope.WriteString(":MEASure:THReshold:EREFerence " + strreference);
                switch (threshold)
                {
                    case 0://"80,50,20":
                        strthreshold = "P205080";
                        break;
                    case 1://"90,50,10":
                        strthreshold = "UDEFined";
                        break;
                    default:
                        strthreshold = "P205080";
                        break;
                }
                MY_Scope.WriteString(":MEASure:THReshold:METHod " + strthreshold);
                logger.AdapterLogString(0, "reference is" + strreference + "threshold is" + strthreshold);
                return true;

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
                
            override public bool OpenOpticalChannel(bool Switch)//true optical  false elec
        {
            if (Switch)
            {
              dcacurrentchannel=  AssembleChannelString(opticalSlot, FlexOptChannel);
              return ChannelDisplaySwitch(dcacurrentchannel, 1);
            }
            else
            {
                dcacurrentchannel = AssembleChannelString(elecSlot, FlexElecChannel);
                return ChannelDisplaySwitch(dcacurrentchannel, 1);
            }
       

        
        }

        private bool ChannelDisplaySwitch(string channel, byte Switch)
        {
            try
            {
                if (Switch == 1)
                {
                    MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "1");
                }
                else
                {
                    MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                }
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool EyeScaleOffset(string channel, double scale, double offset)
        {
            try
            {
                logger.AdapterLogString(0, "channel is" + channel + "scale is" + (scale / 1000000.0).ToString() + "offset is" + (offset / 1000000.0).ToString());
                return MY_Scope.WriteString(":CHAN" + channel + ":YSCale " + (scale / 1000000.0).ToString() + "\n" + ":CHAN" + channel + ":YOFFset " + (offset / 1000000.0).ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool SetScaleOffset()
        {
            try
            {
                string strOpticalChannel = AssembleChannelString(opticalSlot, FlexOptChannel);
                double avgpow = GetAveragePowerWatt();
                double scaletemp = avgpow / 4;
                double offsettemp = avgpow / 2;
                logger.AdapterLogString(0, "scale is" + scaletemp + "offset is" + offsettemp);
                EyeScaleOffset(strOpticalChannel, scaletemp, offsettemp);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool LoadMask(string Channel, string MaskName)
        {
            try
            {
                MY_Scope.WriteString(":MTESt:SOURce1 CHAN" + Channel);
               // MY_Scope.WriteString(":MTESt:LOAD:FNAMe " + "\"" + MaskName + "\"" + "\n");
				string AA = @MaskName;
                MY_Scope.WriteString(":MTESt:LOAD:FNAMe " + "\"" + AA+ "\"");
                logger.AdapterLogString(0, "Channel is" + Channel + "MaskName is" + MaskName);
                return MY_Scope.WriteString(":MTESt:LOAD");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        /// <summary>
        /// 模板测试开关
        /// </summary>
        /// <param name="MaskON">
        /// 1=ON，0=OFF
        /// </param>
        /// <returns></returns>
        override public bool MaskONOFF(bool MaskON)
        {
            try
            {
                if (MaskON == true)
                {
                    return MY_Scope.WriteString(":mtest:DISP " + "ON");
                }
                else
                {
                    return MY_Scope.WriteString(":mtest:DISP " + "OFF");
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool MaskTestMarginSetup(byte marginOnOff, byte marginAutoManul, int manualMarginPercent, byte autoMarginType, double hitRatio, int hitCount)
        {
            try
            {
                if (marginOnOff == 1)
                    MY_Scope.WriteString(":MTESt:MARGin:STATe ON\n");
                else
                    MY_Scope.WriteString(":MTESt:MARGin:STATe OFF\n");
                if (marginAutoManul != 1)//marginType=0  ManulMargin
                {
                    MY_Scope.WriteString(":MTESt:MARGin:METHod MANual\n");
                    MY_Scope.WriteString(":MTESt:MARGin:PERCent " + manualMarginPercent.ToString("E") + "\n");
                }
                else//marginType=1  AutoMargin
                {
                    MY_Scope.WriteString(":MTESt:MARGin:METHod AUTO\n");
                    if (autoMarginType != 1)//autoMarginType=0 HitCount
                    {
                        MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod HITS\n");
                        MY_Scope.WriteString(":MTESt:MARGin:AUTo:HITs " + hitCount.ToString() + "\n");
                    }
                    else//autoMarginType=1 HitRatio
                    {
                        MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod HRATio\n");
                        MY_Scope.WriteString(":MTESt:MARGin:AUTo:HRATio " + hitRatio.ToString("E") + "\n");
                    }
                }
                logger.AdapterLogString(0, "manualMarginPercent is" + manualMarginPercent + "autoMarginType is" + autoMarginType + "hitRatio is" + hitRatio + "hitCount is" + hitCount);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        override public bool SetMaskAlignMethod(byte method)
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

            if (run)
            {
                try
                {
                    return AcquisitionControl(0);

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                try
                {
                    return AcquisitionControl(1);

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
        }
        private bool AcquisitionControl(byte control)
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
                logger.AdapterLogString(0, "AcquisitionControl is" + strcontrol);
                return WriteOpc(":ACQuire:" + strcontrol, 1, 30);
                //return MY_Scope.WriteString(":ACQuire:" + strcontrol);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
                    MY_Scope.WriteString(":TIMebase:SCALe " + strscale);
                    MY_Scope.WriteString(":TIMebase:REFerence CENTer");
                    MY_Scope.WriteString(":MEASure:EYE:ECTime?");
                    double data1 = Convert.ToDouble(MY_Scope.ReadString(20));
                    MY_Scope.WriteString(":TIMebase:POSition?");
                    double data2 = Convert.ToDouble(MY_Scope.ReadString(20));
                    double data3 = data2 - data1 - ((1 / bitRate) / 2);
                    data4 = data2 - data3;
                    if (data4 < data2)
                    {
                        data4 = data4 + (1 / bitRate);
                    }
                    strscale = data4.ToString("E");
                    i++;
                } while (data4 > 1e10 && i < 5);

                MY_Scope.WriteString(":TIMebase:POSition " + strscale);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        /// <summary>
        /// 示波器模式选择
        /// </summary>
        /// <param name="Mode">
        /// 0=EyeMode,1=OscMode,2=JitterMode
        /// </param>
        /// <returns></returns>
        override public bool SetMode(byte Mode)
        {
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
                logger.AdapterLogString(0, "Mode is" + index);
                return MY_Scope.WriteString(":system:mode " + index);
                //return WriteOpc(":system:mode " + index, 0.01, 2);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
                logger.AdapterLogString(0, "RiseFallTime is" + index);
                return MY_Scope.WriteString(":MEASure:EYE:" + index);// + ":SOURce1 CHAN" + channel
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        /// <summary>
        /// 显示JitterPP或者JitterRMS
        /// </summary>
        /// <param name="jitterFormat">
        /// 0=JitterPP,1=JitterRMS
        /// </param>
        /// <returns></returns>
        private bool DisplayJitter(byte jitterFormat)
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
                logger.AdapterLogString(0, "jitterFormat is" + index);
                MY_Scope.WriteString(":MEASure:EYE:JITTer" + ":FORMat " + index);
                return MY_Scope.WriteString(":MEASure:EYE:JITTer");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        private bool DisplayCrossing()
        {
            try
            {
                return MY_Scope.WriteString(":MEASure:EYE:" + "CROSsing");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
                logger.AdapterLogString(0, "measureItem is" + index);
                MY_Scope.WriteString(":MEASure:EYE:" + index);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
                logger.AdapterLogString(0, "erUnit is" + index);
                MY_Scope.WriteString(":MEASure:EYE:ERATio" + ":UNITs " + index);
                return MY_Scope.WriteString(":MEASure:EYE:ERATio");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
      override public bool DisplayER()
      {
         return DisplayER1(0);
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
                logger.AdapterLogString(0, "powerUnits is" + index);
                MY_Scope.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + index);
                return MY_Scope.WriteString(":MEASure:EYE:APOWer");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        /// <summary>
        /// 清除所有显示项
        /// </summary>
        /// <returns></returns>
        private bool DisplayClearAlllist()
        {
            try
            {
                return MY_Scope.WriteString(":MEASure:EYE:LIST:CLEar");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        override public bool ClearDisplay()
        {
            try
            {
                return MY_Scope.WriteString(":ACQuire:CDISplay");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
        private bool AcquisitionLimitTestSetup(byte limitCondition, int limitNumber)
        {
            string index;
            try
            {
                switch (limitCondition)
                {
                    case 0:
                        index = "WAVeforms";
                        break;
                    case 1:
                        index = "SAMPles";
                        break;
                    default:
                        index = "WAVeforms";
                        break;
                }
                logger.AdapterLogString(0, "limitCondition is" + index + "limitNumber is" + limitNumber);
                return MY_Scope.WriteString(":LTESt:ACQuire:CTYPe " + index + ";" + ":LTESt:ACQuire:CTYPe:" + index + " " + limitNumber.ToString());
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool AutoScale()
        {
            try
            {
                return WriteOpc(":SYSTem:AUToscale", 0.01, 5);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        /// <summary>
        /// 眼图累积限制开关
        /// </summary>
        /// <param name="Switch">
        /// 1=On,0=Off
        /// </param>
        /// <returns></returns>

        override public bool SetRunTilOff()
        { 
          return AcquisitionLimitTestSwitch(0);

        }
        private bool AcquisitionLimitTestSwitch(byte Switch)
        {
            try
            {
                if (Switch == 1) return MY_Scope.WriteString(":LTESt:ACQuire:STATe ON");
                else return MY_Scope.WriteString(":LTESt:ACQuire:STATe OFF");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
            string INVert = "OFF";
            string MONochrome = "OFF";
            if (picColor == 1) MONochrome = "ON";
            if (bgColor == 1) INVert = "ON";
            try
            {
                MY_Scope.WriteString(":DISK:SIMage:FNAMe 'screenshot.gif';" + ":DISK:SIMage:INVert " + INVert + ";" + ":DISK:SIMage:MONochrome " + MONochrome + "\n");
                MY_Scope.WriteString(":DISK:SIMage:SAVE;*OPC?");
                for (int j = 0; j < 20; j++)
                {
                    try
                    {
                        if (MY_Scope.ReadString(100).Contains("1")) break;
                        else Thread.Sleep(100);
                    }
                    catch { }
                }
                MY_Scope.WriteString(":DISK:BFILe? 'screenshot.gif'");
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                File.WriteAllBytes(savePath, (byte[])MY_Scope.myDmm.ReadIEEEBlock(IEEEBinaryType.BinaryType_UI1));
               // File.WriteAllBytes(savePath,
                logger.AdapterLogString(0, "savePath is" + savePath);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
            double power = 0.0;
            string unit;
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
                MY_Scope.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + unit);
                MY_Scope.WriteString(":MEASure:EYE:APOWer?");
                string s = MY_Scope.ReadString(32);
                double p1 = Convert.ToDouble(s);
                if (unit == "WATT")
                    power = p1 * 1000000.0;
                else
                    power = p1;
                logger.AdapterLogString(0, "powerUnit is" + unit + "power is" + power);
                return power;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        public override bool DisplayPowerWatt()
        {
            try
            {
                MY_Scope.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + "WATT");
                MY_Scope.WriteString(":MEASure:EYE:APOWer");
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
            
        }
        public override bool DisplayPowerdbm()
        {
            try
            {
                MY_Scope.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + "DBM");
                MY_Scope.WriteString(":MEASure:EYE:APOWer");
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
            string index;
            double er = 0.0;
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
                MY_Scope.WriteString(":MEASure:EYE:ERATio" + ":UNITs " + index);
                MY_Scope.WriteString(":MEASure:EYE:ERATio?");
                er = Convert.ToDouble(MY_Scope.ReadString(256));
                logger.AdapterLogString(0, "erUnits is" + index + "er is" + er);
                return er;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        private double ReadCrossing()
        {
            double crossing = 0.0;
            try
            {
                MY_Scope.WriteString(":MEASure:EYE:CROSsing?");
                crossing = Convert.ToDouble(MY_Scope.ReadString(256));
                logger.AdapterLogString(0, "crossing is" + crossing);
                return crossing;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 读取眼图OMA，返回值为uW或者uV
        /// </summary>
        /// <returns></returns>
        private double ReadAMPLitude()
        {
            double AMPLitude = 0.0;
            try
            {
                MY_Scope.WriteString(":MEASure:EYE:AMPLitude?");
                AMPLitude = Convert.ToDouble(MY_Scope.ReadString(32)) * 1000000;
                logger.AdapterLogString(0, "AMPLitude is" + AMPLitude);
                return AMPLitude;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
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
                MY_Scope.WriteString(":MEASure:EYE:JITTer" + ":FORMat " + index);
                MY_Scope.WriteString(":MEASure:EYE:JITTer?");
                jitter = (Convert.ToDouble(MY_Scope.ReadString(16))) * 1000000000000.0;
                logger.AdapterLogString(0, "jitter is" + jitter);
                return jitter;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
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
                MY_Scope.WriteString(":MEASure:EYE:" + index + "?");
                time = (Convert.ToDouble(MY_Scope.ReadString(16))) * (1E+12);
                logger.AdapterLogString(0, "RFtime is" + index + "time is" + time);
                return time;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }

        }
        /// <summary>
        /// 读取模板余量
        /// </summary>
        /// <returns></returns>
        private double ReadMaskMargin()
        {
            double Margin = 0.0;
            try
            {
                MY_Scope.WriteString(":MEASure:MTESt:MARgin?");
                Margin = Convert.ToDouble(MY_Scope.ReadString(32));
                logger.AdapterLogString(0, "Margin is" + Margin);
                return Margin;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        #endregion
    }

}
