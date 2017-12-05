﻿using System;
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
    public class N1092:Scope
    {
        //public Paremeter MyParemeter = new Paremeter();
        public N1092(logManager logmanager)
        {
            logger = logmanager;
        }
        // public IOPort MyIO;
        protected string[] dcacurrentchannel = new string[4];
        public Algorithm algorithm = new Algorithm();
        public ArrayList ArrayOpticalChannel;

        public string[] strOpticalChannel = new string[4];


        public override bool Initialize(TestModeEquipmentParameters[] N1092DStruct)
        {
            try
            {

                int i = 0;
                if (algorithm.FindFileName(N1092DStruct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "IOTYPE", out i))
                {
                    IOType = N1092DStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "NAME", out i))
                {
                    Name = N1092DStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }

                if (algorithm.FindFileName(N1092DStruct, "DCADATARATE", out i))
                {
                    FlexDcaDataRate = Convert.ToDouble(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no DCADATARATE");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "FILTERSWITCH", out i))
                {
                    FilterSwitch = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FILTERSWITCH");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "FILTERFREQ", out i))
                {
                    FlexFilterFreq = Convert.ToDouble(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FILTERFREQ");
                    return false;
                }

                if (algorithm.FindFileName(N1092DStruct, "DCAWAVELENGTH", out i))
                {
                    FlexDcaWavelength = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no DCAWAVELENGTH");
                    return false;
                }

                if (algorithm.FindFileName(N1092DStruct, "THRESHOLD", out i))
                {
                    Threshold = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no THRESHOLD");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "OPTCHANNEL", out i))
                {
                    FlexOptChannel = N1092DStruct[i].DefaultValue;
                    strOpticalChannel = FlexOptChannel.Split(',');
                    // ArrayOpticalChannel = new ArrayList((N1092DStruct[i].DefaultValue).Split(','));
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTCHANNEL");
                    return false;
                }

                if (algorithm.FindFileName(N1092DStruct, "RAPIDEYESWITCH", out i))
                {
                    rapidEyeSwitch = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RAPIDEYESWITCH");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "REFERENCE", out i))
                {
                    reference = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no REFERENCE");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "MARGINTYPE", out i))
                {
                    marginType = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINTYPE");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "MARGINHITTYPE", out i))
                {
                    marginHitType = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINHITTYPE");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "ACQLIMITTYPE", out i))
                {
                    acqLimitType = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ACQLIMITTYPE");
                    return false;
                }

                if (algorithm.FindFileName(N1092DStruct, "MARGINHITRATIO", out i))
                {
                    marginHitRatio = Convert.ToDouble(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINHITRATIO");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "OPTICALMASKNAME", out i))
                {
                    opticalMaskName = N1092DStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALMASKNAME");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "OPTICALMASKNAME2", out i))
                {
                    opticalMaskName2 = N1092DStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALMASKNAME2");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "MARGINHITCOUNT", out i))
                {
                    marginHitCount = Convert.ToInt16(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINHITCOUNT");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "ACQLIMITNUMBER", out i))
                {
                    acqLimitNumber = Convert.ToInt16(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ACQLIMITNUMBER");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "SETSCALEDELAY", out i))
                {
                    flexsetscaledelay = Convert.ToInt32(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no SETSCALEDELAY");
                    return false;
                }
                if (algorithm.FindFileName(N1092DStruct, "BANDWIDTH", out i))
                {
                    BandWidth = Convert.ToByte(N1092DStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no BANDWIDTH");
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

        public bool ReSet()
        {//":SYSTem:DEFault"
            for (int i = 0; i < 4; i++)
            {
                
            }
            if (MyIO.WriteString(":SYSTem:DEFault"))
            {
                Thread.Sleep(3000);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            MyIO.WriteString(":MEASure:EYE:LIST:CLEar");
            string offset = "";
            CurrentChannel = channel;
            if (offsetlist.ContainsKey(CurrentChannel))
                offset = offsetlist[CurrentChannel];

            double temp = Convert.ToDouble(offset);
            logger.AdapterLogString(0, "N1092 Offset is" + offset);
            SetAttenuation(strOpticalChannel[Convert.ToByte(CurrentChannel) - 1], temp);
            //SetAttenuation(CurrentEleChannel, temp);
            //   SetAttenuation(strElecChannel[Convert.ToByte(CurrentChannel) - 1], temp);

            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
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

                        MyIO = new IOPort(IOType, "TCPIP0::localhost::hislip0,4880::INSTR", logger);
                        MyIO.IOConnect();
                        // MyIO.WriteString("*IDN?");
                        string StrName = MyIO.GPIBQuery("*IDN?");
                        // EquipmentConnectflag = StrName.Contains("86100");
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

        public override bool Configure(int syn = 0)
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
                    //if (!ConfigurePreSet(syn))
                    //{
                    //    return false;
                    //}


                    // 设置Trigger
                    SetMode(0);
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
                    //if (!ConfigureElecChannel(syn))
                    //{
                    //    return false;
                    //}

                    RapidEyeSetup(1, syn);   //加载电眼配置文件会覆盖RapidEye速率

                    for (byte i = 0; i < 4; i++)
                    {
                        MyIO.WriteString(":SYSTem:MODel? SLOT" + (i + 1).ToString());
                        if (!MyIO.ReadString().Contains("Not Present"))
                        {
                            if (isModuleNeedCalibrate((byte)(i + 1)))
                                CalibrateModel((byte)(i + 1));
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public bool ConfigurePreSet(int syn = 0)
        {
            // MyIO.WriteString(":SYSTem:DEFault");
            MyIO.WriteString(":ACQuire:STOP");
            SetMode(0, syn);
            MyIO.WriteString(":DISPlay:ETUNing ON");
            MyIO.WriteString(":DISPlay:TOVerlap OFF");
            RapidEyeSetup(1, syn);

            return true;
        }

        public bool ConfigureInputSignal(int syn = 0)
        {


            try
            {


                if (SetTimeBase(syn)
                    && TriggerSetup()
                    )//FrontPanel
                    return true;
                else
                {
                    logger.AdapterLogString(3, "Set ConfigureInputSignal Error");
                    return false;
                }
            }
            catch
            {
                logger.AdapterLogString(3, "Set ConfigureInputSignal Error");
                return false;
            }


        }

        public bool ConfigureOpticalChannel(List<string> och, int syn = 0)
        {
            for (byte i = 0; i < 4; i++)
            {
                if (!(och.Contains(strOpticalChannel[i])))
                {
                    if (strOpticalChannel[i].ToUpper() != "0")
                    {
                        och.Add(strOpticalChannel[i]);
                        WavelengthSelect(strOpticalChannel[i], FlexDcaWavelength, syn);
                        FileterSelect(strOpticalChannel[i]);
                        FileterSwitch(strOpticalChannel[i], FilterSwitch, syn);


                        ChannelDisplaySwitch(strOpticalChannel[i], 1, syn);


                        if (!LoadMask(strOpticalChannel[i], opticalMaskName, syn))
                        {
                            // 载入模板出错，返回false
                            return false;
                        }
                        ExitMasK();
                    }
                }
            }

            return true;
        }


        public bool ConfigureCommonSetting(int syn = 0)
        {
            // 载入电眼校验文件
            string Str1 = @":DISK:SETup:RECall ""D:\user files\setups\fixture_deskew_ODVT.setx""";
            MyIO.WriteString(Str1);

            MyIO.WriteString(":display:persistence cgrade");//眼图显示为 多彩
            MessureThresholdSetup(Threshold, reference, syn);
            MaskONOFF(false, syn);
            MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
            AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
            AcquisitionLimitTestSwitch(0, syn);
            AcquisitionControl(0);
            CenterEye();
            EyeTuningDisplay(1);
            return true;
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
        public override bool SetMaskLimmit(int acqLimitNumber)
        {
            try
            {


                AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, 0);
                AcquisitionLimitTestSwitch(1, 0);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public override int GetMask()
        {
            // double CurrentMask = -100;
            int MaskArray = -1;
            double CurrentMask;
            int i = 0;
            do
            {
                AcquisitionLimitTestSwitch(1, 0);
                AcquisitionControl(0);
                CurrentMask = ReadMaskMargin();
                i++;
            } while ((CurrentMask > 100 || CurrentMask < 0) && i < 3);


            if (CurrentMask > 100) //没抓到
            {
                CurrentMask = -10;
            }
            return Convert.ToInt16(CurrentMask);

        }

        /// <summary>
        /// 测试光眼图，测量结果以数组形式返回，依次为AP,ER,OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
        /// </summary>
        /// <returns></returns>
        public override bool OpticalEyeTest(out double[] testResults, int syn = 0)
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
                MyIO.WriteString(":MEASure:EYE:LIST:CLEar");//新增 防止眼图卡死状态
                EyeTuningDisplay(1);
                string strChannel = "1A";
                strChannel = strOpticalChannel[Convert.ToByte(CurrentChannel) - 1];
                
                MyIO.WriteString(":ACQuire:REYE ON");
                MyIO.WriteString(":ACQuire:RUN");
                MyIO.WriteString(":DISPlay:TOVerlap OFF");//

                // SetAttenuation(strChannel, FlexDcaAtt, opticalAttSwitch);//channel change have setted
                SetMode(0, syn);
                AcquisitionControl(2);
                AcquisitionLimitTestSwitch(0, syn);
                MaskONOFF(false, syn);//
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

                    AcquisitionLimitTestSwitch(1, syn);
                    AcquisitionControl(2);
                    //string K=  MyIO.GPIBQuery(":ACQuire:COUNt?");  :ACQuire:RUNTil?

                    string K = MyIO.GPIBQuery(":ACQuire:RUNTil?");


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
                        if (SaveFlag1)
                        { break; }
                        else
                        {
                            Thread.Sleep(1000);
                            logger.AdapterLogString(3, "Save Fail Time="+i.ToString());
                        }
                    }
                    MaskONOFF(false, 1);
                    if (opticalMaskName2.Substring(0, 1).ToUpper() == "0")
                    {

                        LoadMask(strChannel, opticalMaskName2, syn);
                        LoadMask(strChannel, opticalMaskName2, syn);
                        MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                        MaskMargin2 = ReadMaskMargin();
                        testResults[11] = MaskMargin2;
                        for (int k = 0; k < 3; k++)
                        {
                            SaveFlag2 = SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram, 0, 0);
                            if (SaveFlag2)
                            {
                                break;
                            }
                            else
                            {
                                
                               Thread.Sleep(1000);
                              logger.AdapterLogString(3, "Save Eye2 Fail Time="+i.ToString());
                        
                            }

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
                    logger.AdapterLogString(3, "Save Eye Image Error");
                    isOk = false;
                }
                AcquisitionLimitTestSwitch(0, syn);
                AcquisitionControl(0);
                MaskONOFF(false, syn);
                // MyIO.WriteString(":DISPlay:ETUNing ON");
                MyIO.WriteString(":ACQuire:REYE OFF");//Leo 修改
                return isOk;
            }
            catch (Exception error)
            {
                MyIO.WriteString(":ACQuire:REYE OFF");//Leo 修改
                logger.AdapterLogString(3, error.ToString());
                return isOk;
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
                MyIO.WriteString(command + ";*OPC?\n");
                for (int i = 0; i < looptime; i++)
                {
                    Thread.Sleep(Convert.ToInt16(readTimeDelay * 1000));
                    try
                    {
                        string returndata = MyIO.ReadString(100);
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
            string strChannel = "";

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
            logger.AdapterLogString(0, "FLEX86100 Channel is" + strChannel);
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
                MyIO.WriteString(":CALibrate:SLOT" + slot.ToString() + ":STATus?");
                status = MyIO.ReadString(16);

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
        private bool TriggerSetup()
        {

            try
            {

                if (MyIO.WriteString(":TRIGger:SOURce FPANel", 1) &&
                    MyIO.WriteString(":TRIGger:MODE EDGE", 1))
                {
                    return true;
                }
                else
                {
                    logger.AdapterLogString(3, "Set TriggerSetup Error");
                    return false;
                }

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
                MyIO.WriteString(":CALibrate:SLOT" + slot.ToString() + ":STARt");
                MyIO.WriteString(":CALibrate:SDONe?");
                for (int i = 0; i < 10; i++)
                {
                    String Str = MyIO.GPIBQuery(":CALibrate:SDONe?");
                    if (Str.Contains("1")) break;
                    else Thread.Sleep(500);
                }
                MyIO.WriteString(":CALibrate:CONTinue");
                MyIO.WriteString(":CALibrate:SDONe?");
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(3000);
                    try
                    {
                        if (MyIO.ReadString() != "") break;
                    }
                    catch
                    {
                    }
                }
                MyIO.WriteString(":CALibrate:CONTinue");
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool DIFFWaveformSwitch(string channel, byte DiffSwitch, int syn = 0)
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
                    MyIO.WriteString(Str1);


                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "differential waveform is on");
                        return MyIO.WriteString(":DIFF" + channel + ":DMODe ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":DIFF" + channel + ":DMODe ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":DIFF" + channel + ":DMODe?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "1\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "differential waveform is on");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set differential waveform Switch wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "differential waveform is off");
                        return MyIO.WriteString(":DIFF" + channel + ":DMODe OFF");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":DIFF" + channel + ":DMODe OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":DIFF" + channel + ":DMODe?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "differential waveform Switch is off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set differential waveform Switch wrong");

                            }

                        }
                        return flag;
                    }
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private bool BandwidthSet(string channel, double bandwidth, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";


            try
            {

                if (syn == 0)
                {
                    //:CHAN1C:BANDwidth:FREQuency 33.0E+9

                    logger.AdapterLogString(0, "bandwidth is" + bandwidth);
                    return MyIO.WriteString(":CHAN" + channel + ":BANDwidth:FREQuency " + bandwidth);
                    //  return MyIO.WriteString(":CHAN1C:BANDwidth:FREQuency 40.0E+9");
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":CHAN" + channel + ":BANDwidth" + " BANDwidth" + bandwidth);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":CHAN" + channel + ":BANDwidth?");
                            readtemp = MyIO.ReadString();
                            if (readtemp.Trim() == ("BAND" + bandwidth + "\n").Trim())
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "bandwidth is" + bandwidth);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set bandwidth wrong");

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
        /// <summary>
        /// 波长选择
        /// </summary>
        /// <param name="channel">
        /// 通道，例如"1A"
        /// </param>
        /// <param name="waveLength">
        /// 波长（3=850,1=1310,2=1550）
        /// </param>
        /// <returns></returns>
        private bool WavelengthSelect(string channel, int waveLength, int syn = 0)
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
                    case 1310:
                        Wavelength_index = "1";
                        break;
                    case 1550:
                        Wavelength_index = "2";
                        break;
                    case 850:
                        Wavelength_index = "3";
                        break;
                    default:
                        Wavelength_index = "1";
                        break;
                }
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Wavelength_index is" + Wavelength_index);
                    return MyIO.WriteString(":CHAN" + channel + ":WAVelength" + " WAVelength" + Wavelength_index);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":CHAN" + channel + ":WAVelength" + " WAVelength" + Wavelength_index);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":CHAN" + channel + ":WAVelength?");
                            readtemp = MyIO.ReadString();
                            if (readtemp.Trim() == ("WAV" + Wavelength_index + "\n").Trim())
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "Wavelength_index is" + Wavelength_index);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set WAVelength wrong");

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
        private bool FileterSelect(string channel)
        {

            try
            {

                MyIO.WriteString(":CHAN1B:FSELect:RATe " + FlexFilterFreq);
                return MyIO.QueryOpc();
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
        private bool FileterSelect(string channel, double FilterFreq)
        {

            try
            {

                MyIO.WriteString(":CHAN1B:FSELect:RATe " + FilterFreq);
                return MyIO.QueryOpc();
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
        private bool FileterSwitch(string channel, byte filterSwith, int syn = 0)
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
                    logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "filterSwith is" + index);
                    return MyIO.WriteString(":CHAN" + channel + ":FILTer " + index);

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":CHAN" + channel + ":FILTer " + index);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":CHAN" + channel + ":FILTer?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == filterSwith.ToString() + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "filterSwith is" + index);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set filterSwith wrong");

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
        private bool SetAttenuation(string channel, double attenuation, int syn = 0)
        {


            try
            {

                if (syn == 0)
                {

                    MyIO.WriteString(":CHAN" + channel + ":ATTenuator:STATe ON");

                    if (!MyIO.QueryOpc())
                    {
                        return false;
                    }

                    MyIO.WriteString(":CHAN" + channel + ":ATTenuator:DECibels " + attenuation.ToString() + "\n");

                    if (!MyIO.QueryOpc())
                    {
                        return false;
                    }
                    logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "attenuation is" + attenuation);
                    return true;
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
                        logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "erFactorPercent is" + erFactorPercent);
                        flag2 = MyIO.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor ON");

                        flag1 = MyIO.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor  " + (erFactorPercent).ToString() + "\r");
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag2 = MyIO.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor ON");

                            flag1 = MyIO.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor  " + (erFactorPercent).ToString() + "\r");
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MyIO.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor?");
                                readtemp = MyIO.ReadString();
                                MyIO.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor?");
                                readtemp2 = MyIO.ReadString();

                                if ((readtemp == "1\n") && (Convert.ToDouble(readtemp2) == Convert.ToDouble((erFactorPercent).ToString())))
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "erFactorPercent is" + erFactorPercent);
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set FLEX86100 erFactorPercent wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "ACFactor STATe IS off");
                        return MyIO.WriteString(":MEASure:ERATio:CHAN1B:ACFactor OFF");

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":MEASure:ERATio:CHAN1B:ACFactor OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":MEASure:ERATio:CHAN1B:ACFactor?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "ACFactor STATe IS off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set ACFactor STATe wrong");

                            }

                        }
                        return flag;
                    }
                }
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
        private bool PrecisionTimebaseSetup(byte slot, double refClkFrequency, byte Switch, byte synchMethod, int syn = 0)
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
                        logger.AdapterLogString(0, "PTIMebase Slot is" + slot.ToString() + "RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString() + "STATe is on");
                        flag1 = MyIO.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod " + strSynchMethod + ";" + ":PTIMebase" + slot.ToString() + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                        flag2 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "ON", 0.01, 5);
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod " + strSynchMethod + ";" + ":PTIMebase" + slot.ToString() + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                            flag2 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "ON", 0.01, 5);
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod?");
                                readtemp = MyIO.ReadString();
                                MyIO.WriteString(":PTIMebase" + slot.ToString() + ":RFRequency?");
                                readtemp1 = MyIO.ReadString();
                                MyIO.WriteString(":PTIMebase" + slot.ToString() + ":STATe?");
                                readtemp2 = MyIO.ReadString();
                                if ((readtemp == strSynchMethod + "\n") && (Convert.ToDouble(readtemp1) == Convert.ToDouble(refClkFrequency.ToString("E"))) && (readtemp2 == "ON\n"))
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "PTIMebase Slot is" + slot.ToString() + "RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString("E") + "STATe is on");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set PTB wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "PTIMebase Slot is" + slot.ToString() + "STATe is off");
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
                                MyIO.WriteString(":PTIMebase" + slot.ToString() + ":STATe?");
                                readtemp = MyIO.ReadString();
                                if (readtemp2 == "OFF\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "PTIMebase Slot is" + slot.ToString() + "STATe is off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set PTB wrong");

                            }

                        }
                        return flag;
                    }
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        private bool SetTimeBase(int syn = 0)
        {
            bool flag = false;

            try
            {

                MyIO.WriteString(":TIMebase:BRATe " + DcaDataRate);

                for (int i = 0; i < 3; i++)
                {

                    // MyIO.WriteString(":TIMebase:BRATe " + MyParemeter.DataRate);
                    Thread.Sleep(50);
                    string s = MyIO.GPIBQuery("*OPC?");

                    if (s.Contains("1"))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    logger.AdapterLogString(3, "SetTimeBase Error");
                }
                return flag;



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
        private bool RapidEyeSetup(byte rapidEyeSwitch, int syn = 0)
        {


            try
            {

                if (rapidEyeSwitch == 1)
                {

                    MyIO.WriteString(":ACQuire:REYE " + "ON");
                }
                else
                {
                    MyIO.WriteString(":ACQuire:REYE " + "OFF");
                }

                if (!MyIO.QueryOpc())
                {
                    logger.AdapterLogString(3, "RapidEyeSetup Error!");
                    return false;
                }
                else
                {
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
        /// 眼图实时刷新显示方式开关
        /// </summary>
        /// <param name="Switch">
        /// 1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        public override bool EyeTuningDisplay(byte Switch, int syn = 0)
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
                        logger.AdapterLogString(0, " EyeTuningDisplay IS ON");
                        return MyIO.WriteString(":DISPlay:ETUNing ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":DISPlay:ETUNing ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":DISPlay:ETUNing?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "1\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, " EyeTuningDisplay IS ON");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, " EyeTuningDisplay wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, " EyeTuningDisplay IS OFF");
                        return MyIO.WriteString(":DISPlay:ETUNing OFF");

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":DISPlay:ETUNing OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":DISPlay:ETUNing?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, " EyeTuningDisplay IS OFF");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, " EyeTuningDisplay wrong");

                            }

                        }
                        return flag;
                    }
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
        private bool MessureThresholdSetup(byte threshold, byte reference, int syn = 0)
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
                        strthreshold = "P105090";
                        break;
                    default:
                        strthreshold = "P205080";
                        break;
                }

                if (syn == 0)
                {
                    logger.AdapterLogString(0, " THReshold reference is" + strreference);
                    logger.AdapterLogString(0, " THReshold METHod is" + strthreshold);

                    flag1 = MyIO.WriteString(":MEASure:THReshold:EREFerence " + strreference);
                    flag2 = MyIO.WriteString(":MEASure:THReshold:METHod " + strthreshold);
                    if ((flag1 == true) && (flag2 == true))
                    { flag = true; }
                    return flag;

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":MEASure:THReshold:EREFerence " + strreference);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":MEASure:THReshold:EREFerence?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == strreference + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, " THReshold reference is" + strreference);

                        }
                        else
                        {
                            logger.AdapterLogString(3, " THReshold EREFerence wrong");

                        }
                    }


                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":MEASure:THReshold:METHod " + strthreshold);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            MyIO.WriteString(":MEASure:THReshold:METHod?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == strthreshold + "\n")
                                break;
                        }
                        if (j <= 3)
                        {
                            logger.AdapterLogString(0, " THReshold METHod is" + strthreshold);

                        }
                        else
                        {
                            logger.AdapterLogString(3, " THReshold METHod wrong");

                        }


                    }

                    if ((j <= 3) && (k <= 3))
                    { flag = true; }
                    else
                    { flag = false; }
                    return flag;

                }
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
        override public bool OpenOpticalChannel(bool Switch, int syn = 0)//true optical  false elec
        {
            for (int i = 0; i < 4; i++)
            {
                //EyeChannelDisplaySwitch(CurrentEleChannel, 0, syn);
                ChannelDisplaySwitch(strOpticalChannel[i], 0, syn);
                ChannelDisplaySwitch(strOpticalChannel[i], 0, syn);
            }
            if (Switch)
            {
                return ChannelDisplaySwitch(strOpticalChannel[Convert.ToByte(CurrentChannel) - 1], 1, syn);
            }
            else
            {
                return EyeChannelDisplaySwitch(strOpticalChannel[Convert.ToByte(CurrentChannel) - 1], 1, syn);
            }
        }
        private bool EyeChannelDisplaySwitch(string channel, byte Switch, int syn = 0)
        {
            return ChannelDisplaySwitch(channel, Switch);
        }
        private bool ChannelDisplaySwitch(string channel, byte Switch, int syn = 0)
        {

            try
            {
                if (Switch == 1)
                {
                    MyIO.WriteString(":CHAN" + channel + ":DISPlay " + "1");
                }
                else
                {
                    MyIO.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                }
                return MyIO.QueryOpc();
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
        private bool EyeScaleOffset(string channel, double scale, double offset, int syn = 0)
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
                    logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "scale is" + (scale / 1000000.0).ToString() + "offset is" + (offset / 1000000.0).ToString());
                    return MyIO.WriteString(":CHAN" + channel + ":YSCale " + (scale / 1000000.0).ToString() + "\n" + ":CHAN" + channel + ":YOFFset " + (offset / 1000000.0).ToString());

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":CHAN" + channel + ":YSCale " + (scale / 1000000.0).ToString() + "\n" + ":CHAN" + channel + ":YOFFset " + (offset / 1000000.0).ToString());

                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":CHAN" + channel + ":YSCale?");
                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble((scale / 1000000.0).ToString()))
                                break;
                        }
                        for (j = 0; j < 3; j++)
                        {

                            MyIO.WriteString(":CHAN" + channel + ":YOFFset?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == (offset / 1000000.0).ToString())
                                break;
                        }

                    }
                    if ((k <= 3) && (j <= 3))
                    {
                        logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "scale is" + (scale / 1000000.0).ToString() + "offset is" + (offset / 1000000.0).ToString());
                        flag = true;
                    }
                    else
                    {
                        logger.AdapterLogString(3, "set FLEX86100 switch wrong");

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
        public override bool SetScaleOffset(double PowerVaule, int syn = 0)
        {
            double avgpow;
            PowerVaule = algorithm.ChangeDbmtoUw(PowerVaule);
            try
            {
                string OpticalChannel = strOpticalChannel[Convert.ToByte(CurrentChannel) - 1];
                avgpow = GetAveragePowerWatt();

                if (avgpow < 1E+6 && avgpow > 1)
                {
                    double scaletemp = avgpow / 3;
                    double offsettemp = avgpow / 2;
                    logger.AdapterLogString(0, "scale is" + scaletemp + "offset is" + offsettemp);
                    EyeScaleOffset(OpticalChannel, scaletemp, offsettemp, syn);
                }
                else
                {
                    avgpow = PowerVaule;
                    if (avgpow != 0 && avgpow < 1E+6)
                    {
                        double scaletemp = avgpow / 3;
                        double offsettemp = avgpow / 2;
                        logger.AdapterLogString(0, "scale is" + scaletemp + "offset is" + offsettemp);
                        EyeScaleOffset(OpticalChannel, scaletemp, offsettemp, syn);
                    }
                }
                //  Thread.Sleep(flexsetscaledelay);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool LoadMask()
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
            catch
            {
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
        private bool LoadMask(string Channel, string MaskName, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            bool flag2 = false;
            int k = 0;
            int j = 0;
            string readtemp = "";
            try
            {
                string AA = @"%DEMO_DIR%\Masks\Ethernet\" + MaskName;
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "FLEX86100 Channel is" + Channel + "MaskName is" + MaskName);

                    flag1 = MyIO.WriteString(":MTESt:SOURce CHAN" + Channel);
                    flag2 = MyIO.WriteString(":MTESt:LOAD:FNAMe " + "\"" + AA + "\"");
                    MyIO.WriteString(":MTESt:LOAD");
                    if ((flag1 == true) && (flag2 == true))
                    { flag = true; }
                    return flag;
                }
                else
                {
                    for (int i = 0; i < 3; i++)

                    {
                        flag1 = MyIO.WriteString(":MTESt:SOURce CHAN" + Channel);
                        flag2 = MyIO.WriteString(":MTESt:LOAD:FNAMe " + "\"" + AA + "\"");
                        MyIO.WriteString(":MTESt:LOAD");
                        MyIO.WriteString(":DISPlay:TMASk MASK");
                        if ((flag1 == true) && (flag2 == true))
                            break;
                    }
                    if ((flag1 == true) && (flag2 == true))
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":MTESt:SOURce?");
                            readtemp = MyIO.ReadString();

                            if (readtemp.Contains(Channel.ToUpper().Trim()))
                            {
                                break;
                            }
                            //if (readtemp == "CHAN" + Channel + "\n")
                            //    break;
                        }
                        for (j = 0; j < 3; j++)
                        {

                            MyIO.WriteString(":MTESt:LOAD:FNAMe?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == "\"" + MaskName + "\"" + "\n")
                                break;
                        }
                        if ((k <= 3) && (j <= 3))
                        {
                            logger.AdapterLogString(0, "FLEX86100 Channel is" + Channel + "MaskName is" + MaskName);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "  LoadMask wrong");

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
        private bool ExitMasK()
        {
            return MyIO.WriteString(":MTESt:DISPlay OFF");
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
                        logger.AdapterLogString(0, "MaskONOFF is on");
                        flag = MyIO.WriteString(":mtest:DISP " + "ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":mtest:DISP " + "ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MyIO.WriteString(":mtest:DISP?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "1\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "MaskONOFF is on");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set MaskONOFF wrong");

                            }

                        }
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "MaskONOFF is off");
                        flag = MyIO.WriteString(":mtest:DISP " + "OFF");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":mtest:DISP " + "OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MyIO.WriteString(":mtest:DISP?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "MaskONOFF is off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set MaskONOFF wrong");

                            }

                        }
                    }
                }
                return flag;
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
        override public bool MaskTestMarginSetup(byte marginOnOff, byte marginAutoManul, int manualMarginPercent, byte autoMarginType, double hitRatio, int hitCount, int syn = 0)
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
                        MyIO.WriteString(":MTESt:MARGin:STATe ON\n");
                    }
                    else
                    {
                        MyIO.WriteString(":MTESt:MARGin:STATe OFF\n");
                    }
                    if (marginAutoManul != 1)//marginType=0  ManulMargin
                    {
                        MyIO.WriteString(":MTESt:MARGin:METHod MANual\n");
                        MyIO.WriteString(":MTESt:MARGin:PERCent " + manualMarginPercent.ToString("E") + "\n");
                    }
                    else//marginType=1  AutoMargin
                    {

                        if (autoMarginType != 1)//autoMarginType=0 HitCount
                        {
                            MyIO.WriteString(":MTESt:MARGin:METHod AUTO\n");
                            MyIO.WriteString(":MTESt:MARGin:AUTo:METHod HITS\n");
                            MyIO.WriteString(":MTESt:MARGin:AUTo:HITs " + hitCount.ToString() + "\n");
                        }
                        else
                        {
                            MyIO.WriteString(":MTESt:MARGin:METHod AUTO\n");
                            MyIO.WriteString(":MTESt:MARGin:AUTo:METHod HRATio\n");
                            MyIO.WriteString(":MTESt:MARGin:AUTo:HRATio " + hitRatio.ToString("E") + "\n");
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
                            flag1 = MyIO.WriteString(":MTESt:MARGin:STATe ON\n");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":MTESt:MARGin:STATe?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == marginOnOff.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "marginOnOff is ON");
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set marginOnOff wrong");
                            }

                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":MTESt:MARGin:STATe OFF\n");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":MTESt:MARGin:STATe?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == marginOnOff.ToString() + "\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "marginOnOff is OFF");
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set marginOnOff wrong");
                            }

                        }

                    }
                    if (marginAutoManul != 1)//marginType=0  ManulMargin
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":MTESt:MARGin:METHod MANual\n");
                            flag2 = MyIO.WriteString(":MTESt:MARGin:PERCent " + manualMarginPercent.ToString("E") + "\n");

                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (j = 0; j < 3; j++)
                            {

                                MyIO.WriteString(":MTESt:MARGin:METHod?");
                                readtemp = MyIO.ReadString();
                                MyIO.WriteString(":MTESt:MARGin:PERCent?");
                                readtemp1 = MyIO.ReadString();
                                if ((readtemp == "MANual\n") && (readtemp1 == manualMarginPercent.ToString("E") + "\n"))
                                    break;
                            }
                            if (j <= 3)
                            {
                                logger.AdapterLogString(0, "MARGin  METHod is MANual");
                                logger.AdapterLogString(0, "MARGin  PERCent is" + manualMarginPercent.ToString("E"));
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set MARGin  METHod wrong");
                                logger.AdapterLogString(3, "set PERCent  METHod wrong");

                            }

                        }
                    }
                    else//marginType=1  AutoMargin
                    {

                        if (autoMarginType != 1)//autoMarginType=0 HitCount
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = MyIO.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                flag2 = MyIO.WriteString(":MTESt:MARGin:AUTo:METHod HITS\n");
                                flag3 = MyIO.WriteString(":MTESt:MARGin:AUTo:HITs " + hitCount.ToString() + "\n");

                                if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                            {
                                for (j = 0; j < 3; j++)
                                {

                                    MyIO.WriteString(":MTESt:MARGin:METHod?");
                                    readtemp = MyIO.ReadString();
                                    MyIO.WriteString(":MTESt:MARGin:AUTo:METHod?");
                                    readtemp1 = MyIO.ReadString();
                                    MyIO.WriteString(":MTESt:MARGin:AUTo:HITs?");
                                    readtemp2 = MyIO.ReadString();
                                    if ((readtemp == "AUTO\n") && (readtemp1 == "HITS\n") && (readtemp2 == hitCount.ToString() + "\n"))
                                        break;
                                }
                                if (j <= 3)
                                {
                                    logger.AdapterLogString(0, "MARGin  METHod is AUTO");
                                    logger.AdapterLogString(0, "MARGin  AUTo METHod is HITS" + "HITs is " + hitCount.ToString());
                                }
                                else
                                {
                                    logger.AdapterLogString(3, "set MARGin  METHod wrong");
                                    logger.AdapterLogString(3, "set AUTo  METHod  HITS wrong");

                                }

                            }
                        }
                        else//autoMarginType=1 HitRatio
                        {


                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = MyIO.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                flag2 = MyIO.WriteString(":MTESt:MARGin:AUTo:METHod HRATio\n");
                                flag3 = MyIO.WriteString(":MTESt:MARGin:AUTo:HRATio " + hitRatio.ToString("E") + "\n");

                                if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                            {
                                for (j = 0; j < 3; j++)
                                {

                                    MyIO.WriteString(":MTESt:MARGin:METHod?");
                                    readtemp = MyIO.ReadString();
                                    MyIO.WriteString(":MTESt:MARGin:AUTo:METHod?");
                                    readtemp1 = MyIO.ReadString();
                                    MyIO.WriteString(":MTESt:MARGin:AUTo:HRATio?");
                                    readtemp2 = MyIO.ReadString();
                                    if ((readtemp == "AUTO\n") && (readtemp1 == "HRATio\n") && (readtemp2 == hitRatio.ToString("E") + "\n"))
                                        break;
                                }
                                if (j <= 3)
                                {
                                    logger.AdapterLogString(0, "MARGin  METHod is AUTO");
                                    logger.AdapterLogString(0, "MARGin  AUTo METHod is HRATio" + "HRATio is " + hitRatio.ToString("E"));
                                }
                                else
                                {
                                    logger.AdapterLogString(3, "set MARGin  METHod wrong");
                                    logger.AdapterLogString(3, "set AUTo  METHod  HRATio wrong");

                                }

                            }

                        }


                    }
                    if ((k <= 3) && (j <= 3))
                    { flag = true; }
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
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
                return true;
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
            double bitRate = Convert.ToDouble(DcaDataRate);
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
                    MyIO.WriteString(":TIMebase:SCALe " + strscale);
                    MyIO.WriteString(":TIMebase:REFerence CENTer");
                    MyIO.WriteString(":MEASure:EYE:ECTime?");
                    double data1 = Convert.ToDouble(MyIO.ReadString(20));
                    MyIO.WriteString(":TIMebase:POSition?");
                    double data2 = Convert.ToDouble(MyIO.ReadString(20));
                    double data3 = data2 - data1 - ((1 / bitRate) / 2);
                    data4 = data2 - data3;
                    if (data4 < data2)
                    {
                        data4 = data4 + (1 / bitRate);
                    }
                    strscale = data4.ToString("E");
                    i++;
                } while (data4 > 1e10 && i < 5);

                MyIO.WriteString(":TIMebase:POSition " + strscale);
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
        override public bool SetMode(byte Mode, int syn = 0)
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
                    logger.AdapterLogString(0, "Mode is" + index);
                    return MyIO.WriteString(":system:mode " + index);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":system:mode " + index);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":system:mode?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == index + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "Mode is" + index);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set Mode wrong");

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
                return MyIO.WriteString(":MEASure:EYE:" + index);// + ":SOURce1 CHAN" + channel
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
        public override bool DisplayJitter(byte jitterFormat)////[20160419]Nate: change private to public
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
                logger.AdapterLogString(0, "jitterFormat is " + index);
                MyIO.WriteString(":MEASure:EYE:JITTer" + ":FORMat " + index);
                return MyIO.WriteString(":MEASure:EYE:JITTer");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool DisplayEyeHeight()
        {
            try
            {
                return MyIO.WriteString(":MEASure:EYE:EyeHeight");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool DisplayEyeWidth()
        {
            try
            {
                return MyIO.WriteString(":MEASure:EYE:EyeWidth");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool DisplayCrossing()
        {
            try
            {
                return MyIO.WriteString(":MEASure:EYE:" + "CROSsing");
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
                MyIO.WriteString(":MEASure:EYE:" + index);
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
                MyIO.WriteString(":MEASure:EYE:ERATio" + ":UNITs " + index);
                return MyIO.WriteString(":MEASure:EYE:ERATio");
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
        private bool DisplayBitRate()
        {
            try
            {

                return MyIO.WriteString(":MEASure:EYE:BITRate");
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
                MyIO.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + index);
                return MyIO.WriteString(":MEASure:EYE:APOWer");
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
        public bool DisplayClearAlllist()
        {
            try
            {
                return MyIO.WriteString(":MEASure:EYE:LIST:CLEar");
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
                MyIO.WriteString(":ACQuire:RUN");               
                return MyIO.WriteString(":ACQuire:CDISplay");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }

        //override public bool Clear()
        //{
        //    try
        //    {
        //        MyIO.WriteString(":ACQuire:RUN");
        //        MyIO.WriteString(":MEASure:EYE:LIST:CLEar");
        //        return MyIO.WriteString(":ACQuire:CDISplay");
        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());
        //        return false;
        //    }

        //}
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
                    logger.AdapterLogString(0, "limitCondition is" + index + "limitNumber is" + limitNumber);
                    return MyIO.WriteString(":LTESt:ACQuire:CTYPe " + index + ";" + ":LTESt:ACQuire:CTYPe:" + index + " " + limitNumber.ToString());

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":LTESt:ACQuire:CTYPe " + index + ";" + ":LTESt:ACQuire:CTYPe:" + index + " " + limitNumber.ToString());

                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":LTESt:ACQuire:CTYPe?");
                            readtemp = MyIO.ReadString();
                            if (readtemp == index + "\n")
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "limitCondition is" + index + "limitNumber is" + limitNumber);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "AcquisitionLimitTestSetup wrong");

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
        public override bool AutoScale(int syn = 0)
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
        public override bool DisplayThreeEyes(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";

            try
            {

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "display eye num is" + (3.0000).ToString());
                    return MyIO.WriteString(":TIMebase:BRANge " + (3.0000).ToString());
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MyIO.WriteString(":TIMebase:BRANge " + (3.0000).ToString());
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MyIO.WriteString(":TIMebase:BRANge?");
                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble((3.0000).ToString()))
                                break;
                        }
                        if (k <= 3)
                        {
                            logger.AdapterLogString(0, "display eye num is" + (3.0000).ToString());
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "display eye num set is wrong");

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
        /// <summary>
        /// 眼图累积限制开关
        /// </summary>
        /// <param name="Switch">
        /// 1=On,0=Off
        /// </param>
        /// <returns></returns>

        override public bool SetRunTilOff(int syn = 0)
        {
            return AcquisitionLimitTestSwitch(0);

        }
        private bool AcquisitionLimitTestSwitch(byte Switch, int syn = 0)
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
                        logger.AdapterLogString(0, "AcquisitionLimitTestSwitch is on");
                        return MyIO.WriteString(":LTESt:ACQuire:STATe ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":LTESt:ACQuire:STATe ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":LTESt:ACQuire:STATe?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "1\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "AcquisitionLimitTestSwitch is on");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set AcquisitionLimitTestSwitch wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "AcquisitionLimitTestSwitch is off");
                        return MyIO.WriteString(":LTESt:ACQuire:STATe OFF");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MyIO.WriteString(":LTESt:ACQuire:STATe OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MyIO.WriteString(":LTESt:ACQuire:STATe?");
                                readtemp = MyIO.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "AcquisitionLimitTestSwitch is off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set AcquisitionLimitTestSwitch wrong");

                            }

                        }
                        return flag;
                    }
                }
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
            string diagramName = "";
            string INVert = "OFF";
            string MONochrome = "OFF";
            if (picColor == 1) MONochrome = "ON";
            if (bgColor == 1) INVert = "ON";
            try
            {                
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
                string Str_sencond = CurrentTime.Second.ToString();//millisecond
                string Str_millisencond = CurrentTime.Millisecond.ToString();
                diagramName = pglobalParameters.CurrentSN + "-" + pglobalParameters.CurrentTemp + "-" + pglobalParameters.CurrentVcc + "-" + pglobalParameters.CurrentChannel + "-" + Str_Year + "_" + Str_Month + "_" + Str_Day + "_" + Str_Hour + "_" + Str_minute + "_" + Str_sencond + "_"+Str_millisencond + ".bmp";


                string s = @":DISK:SIMage:FNAMe " + "'" + savePath + diagramName + "'";
                MyIO.WriteString(s);
                MyIO.WriteString(":DISK:SIMage:SAVE");
                bool k = MyIO.QueryOpc();

                logger.AdapterLogString(0, "savePath is" + savePath + diagramName);
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
                    MyIO.WriteString(":ACQuire:RUN");
                    MyIO.WriteString(":MEASure:EYE:APOWer:Status?");
                    string ss = MyIO.ReadString();
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
                    logger.AdapterLogString(0, "power is not read");

                }
                MyIO.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + unit);
                MyIO.WriteString(":MEASure:EYE:APOWer?");
                string s = MyIO.ReadString(32);
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
                MyIO.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + "WATT");
                MyIO.WriteString(":MEASure:EYE:APOWer");
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
                MyIO.WriteString(":ACQuire:RUN");
                MyIO.WriteString(":MEASure:EYE:APOWer" + ":UNITs " + "DBM");
                MyIO.WriteString(":MEASure:EYE:APOWer");
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
                    MyIO.WriteString(":MEASure:EYE:ERATio:Status?");
                    string ss = MyIO.ReadString();
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
                    logger.AdapterLogString(0, "power is not read");

                }
                MyIO.WriteString(":MEASure:EYE:ERATio" + ":UNITs " + index);
                MyIO.WriteString(":MEASure:EYE:ERATio?");
                er = Convert.ToDouble(MyIO.ReadString(256));
                logger.AdapterLogString(0, "erUnits is" + index + "er is" + er);
                return er;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        public override double GetCrossing()
        {
            double crossing = 0.0;
            try
            {
                MyIO.WriteString(":MEASure:EYE:CROSsing?");
                crossing = Convert.ToDouble(MyIO.ReadString(256));
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
        /// 读取眼图OMA，返回值为mW或者mV
        /// </summary>
        /// <returns></returns>
        private double ReadAMPLitude()
        {
            double AMPLitude = 0.0;
            try
            {
                MyIO.WriteString(":MEASure:EYE:AMPLitude?");
                AMPLitude = Convert.ToDouble(MyIO.ReadString(32)) * 1000;
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
                MyIO.WriteString(":MEASure:EYE:JITTer" + ":FORMat " + index);
                MyIO.WriteString(":MEASure:EYE:JITTer?");
                jitter = (Convert.ToDouble(MyIO.ReadString(16))) * 1000000000000.0;
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
                MyIO.WriteString(":MEASure:EYE:" + index + "?");
                time = (Convert.ToDouble(MyIO.ReadString(16))) * (1E+12);
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
                MyIO.WriteString(":MEASure:MTESt:MARgin?");
                Margin = Convert.ToDouble(MyIO.ReadString(32));
                logger.AdapterLogString(0, "Margin is" + Margin);
                return Margin;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 读取眼高
        /// </summary>
        /// <returns></returns>
        private double ReadEyeHeight()
        {
            double height = 0.0;
            try
            {
                MyIO.WriteString(":MEASure:EYE:EHEight?");
                height = Convert.ToDouble(MyIO.ReadString(32)) * 1000.0;
                logger.AdapterLogString(0, "eye height is" + height);
                return height;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 读取眼高
        /// </summary>
        /// <returns></returns>
        private double ReadBitRate()
        {
            double biTRate = 0.0;
            try
            {
                MyIO.WriteString(":MEASure:EYE:BitRate?");
                double Str = Convert.ToDouble(MyIO.ReadString());
                biTRate = Str / (1E+9);
                //height = Convert.ToDouble(MyIO.ReadString(32)) * 1000.0;
                logger.AdapterLogString(0, "biTRate is" + biTRate + "GB/S");
                return biTRate;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 读取眼宽
        /// </summary>
        /// <returns></returns>
        private double ReadEyeWidth()
        {
            double width = 0.0;
            try
            {
                MyIO.WriteString(":MEASure:EYE:EWIDth?");
                width = Convert.ToDouble(MyIO.ReadString(32)) * (1E+12);
                logger.AdapterLogString(0, "eye width is" + width);
                return width;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }
        #endregion

        public override double GetTxRinOmA()
        {
            try
            {


                MyIO.WriteString(":MEASure:AMPLitude:DEFine:ANALysis ON");
                MyIO.WriteString(":MEASure:AMPLitude:DEFine:LOCATION 50");
                MyIO.WriteString(":DISPLAY:AMPLitude:LEVel BOTH");
                MyIO.WriteString(":MEASure:AMPLitude:DEFine:UNITs WATT");
                MyIO.WriteString(":MEASure:AMPLitude:DEFine:RINoise:TYPe OMA");
                MyIO.WriteString(":MEASure:AMPLitude:DEFine:RINoise:UNITs DBHZ");
                MyIO.WriteString(":MEASure:AMPLitude:DEFine:LEVel CIDigits");
                MyIO.WriteString(":MEASure:AMPLitude:DEFine:LEVel:CIDigits:LAGGing 2");
                MyIO.WriteString(":MEASure:AMPLitude:DEFine:LEVel:CIDigits:LEADing 2");
                MyIO.WriteString(":MEASure:AMPLitude:RINoise?");

                string buf = MyIO.ReadString(8);
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
            catch
            {
                return 100;
            }
        }

        public override void GetAllChannel_Mask_OMA(int ChannelCount, out double[] MaskArray, out double[] OMAArray)
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


            // LoadMask()
        }

        public override bool MeasureVEC(out double[] result, int syn = 0)//nate add
        {
            result = new double[3];
            int count = 0;
            bool isOk = false;
            int i;
            string strChannel = "3A";
            try
            {
                // strChannel = strElecChannel[Convert.ToByte(CurrentChannel) - 1];
                //strChannel = MyParemeter.CurrentEleChannel;
                ClearDisplay();
                ChannelDisplaySwitch(strChannel, 0, 1);

                MyIO.WriteString(":DIFF" + strChannel + ":DISPlay ON");

                EyeTuningDisplay(0, 1);
                MyIO.WriteString(":ACQuire:REYE ON");
                MyIO.WriteString(":DISPlay:TOVerlap OFF");
                SetMode(0, syn);
                AcquisitionControl(2);
                AcquisitionLimitTestSwitch(0, syn);
                MaskONOFF(false, syn);
                DisplayClearAlllist();
                DisplayRiseFallTime(1);
                DisplayRiseFallTime(0);
                DisplayJitter(1);
                DisplayJitter(0);
                MyIO.WriteString(":MEASure:EYE:EWIDth");
                MyIO.WriteString(":MEASure:EYE:EHEight");
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
                        logger.AdapterLogString(3, "Save Eye Image Error");
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

                MyIO.WriteString(":ACQuire:REYE OFF");
                MyIO.WriteString(":DIFF" + strChannel + ":DISPlay OFF");
                return isOk;
            }
            catch (Exception error)
            {
                MyIO.WriteString(":ACQuire:REYE OFF");
                MyIO.WriteString(":DIFF" + strChannel + ":DISPlay OFF");
                logger.AdapterLogString(3, error.ToString());
                return isOk;
            }
        }//nate add

        public override double GetAMP_5()//nate add
        {
            double amp_5 = 0.0;
            bool flag = false;
            try
            {
                MyIO.WriteString(":MEASure:EBOundary:LEFT 5.25E+1");
                MyIO.WriteString(":MEASure:EBOundary:RIGHt 5.75E+1");
                MyIO.WriteString(":MEASure:EYE:AMPLitude");
                MyIO.WriteString(":MEASure:EYE:AMPLitude?");
                amp_5 = Convert.ToDouble(MyIO.ReadString(32)) * 1000;
                logger.AdapterLogString(0, "AMPLitude of 5% is" + amp_5);

                //for (int k = 0; k < 3; k++)
                //{
                //    flag = SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
                //    if (flag) break;
                //}
                //if (!flag)
                //{
                //    MessageBox.Show("眼图保存失败!!!");
                //    logger.AdapterLogString(3, "Save Eye Image Error");                    
                //}
                return amp_5;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
            //finally
            //{
            //    MyIO.WriteString(":MEASure:EBOundary:LEFT 4.00E+1");
            //    MyIO.WriteString(":MEASure:EBOundary:RIGHt 6.00E+1");
            //}
        }

        public override double GetJitterPP()//nate add
        {
            double jitterPP = 0.0;
            try
            {
                jitterPP = this.ReadJitter(0);
                logger.AdapterLogString(0, "JitterPP is " + jitterPP);
                return jitterPP;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return jitterPP;
            }
        }

        public override bool SavaScreen(string filePath)
        {
            //return this.SaveEyeDiagram(pglobalParameters.StrPathEEyeDiagram, 0, 0);
            return this.SaveEyeDiagram(filePath, 0, 0);
        }

        public override bool PtimebaseStatue(bool isON)
        {
            if (isON)
            {
                return MyIO.WriteString(":TIMebase:PTImebase:STATe ON");
            }
            else
            {
                return MyIO.WriteString(":TIMebase:PTImebase:STATe OFF");
            }

        }

        public override bool TriggerSourceFpanel()
        {
            return MyIO.WriteString(":TRIGger:SOURce FPANel");
        }

        public override bool TriggerPlock(bool isON)
        {
            if (isON)
            {
                return MyIO.WriteString(":TRIGger:PLOCK ON");
            }
            else
            {
                return MyIO.WriteString(":TRIGger:PLOCK OFF");
            }
        }

        public override double GetTxTDEC(int syn = 0)
        {
            try
            {
                string strChannel = "1B";

                strChannel = strOpticalChannel[Convert.ToByte(CurrentChannel) - 1];

                FileterSelect(strChannel, 16.8E+9);   //设置TDEC滤波速率:TDEC(12.6 GHz)

                EyeTuningDisplay(1);

                MyIO.WriteString(":ACQuire:REYE ON");
                MyIO.WriteString(":DISPlay:TOVerlap OFF");

                SetMode(0, syn);
                AcquisitionControl(2);
                AcquisitionLimitTestSwitch(0, syn);
                MaskONOFF(false, syn);
                DisplayClearAlllist();

                MyIO.WriteString(":MEASure:EYE:TDEC");   //显示TDEC项

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

                MyIO.WriteString(":MEASure:EYE:TDEC?");

                string s = MyIO.ReadString();
                double TDEC = -1.0;

                bool result = double.TryParse(s, out TDEC);

                AcquisitionLimitTestSwitch(0, syn);
                AcquisitionControl(0);

                FileterSelect(strChannel, Convert.ToDouble( DcaDataRate));              //恢复示波器滤波速率

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
            catch (Exception error)
            {
                MyIO.WriteString("GetTxTDEC Fail");
                logger.AdapterLogString(3, error.ToString());
                return 100;
            }
        }

      
        private double GetEyeHeightWithProbability(int index)
        {
            double height = 0.0;
            try
            {
                MyIO.WriteString(":MEASure:EYE:PAM:EHeight:DEFine:EOPening PROBability");
                MyIO.WriteString(":MEASure:EYE:PAM:EHeight:DEFine:EOPening:PROBability 1.0E-" + index);
                MyIO.WriteString(":MEASure:EYE:PAM:EHEight");
                MyIO.WriteString(":MEASure:EYE:PAM:EHEight?");
                height = Convert.ToDouble(MyIO.ReadString(32)) * 1000.0;
                logger.AdapterLogString(0, "eye height is" + height + " with PROBability 1.0E-" + index);
                return height;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }

        private double GetEyeWidthWithProbability(int index)
        {
            double width = 0.0;
            try
            {
                MyIO.WriteString(":MEASure:EYE:PAM:EWIDth:DEFine:EOPening PROBability");
                MyIO.WriteString(":MEASure:EYE:PAM:EWIDth:DEFine:EOPening:PROBability 1.0E-" + index);
                MyIO.WriteString(":MEASure:EYE:PAM:EWIDth");
                MyIO.WriteString(":MEASure:EYE:PAM:EWIDth?");
                width = Convert.ToDouble(MyIO.ReadString(32)) * (1E+12);
                logger.AdapterLogString(0, "eye width is" + width + " with PROBability 1.0E-" + index);
                return width;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return 0;
            }
        }

        private void DisplayEyeContours(int indexOfBERates, int numOfECOtour)
        {
            try
            {
                MyIO.WriteString(":ECONtour" + numOfECOtour + ":DISplay ON");
                MyIO.WriteString(":ECONtour" + numOfECOtour + ":BERates 1.0E-" + indexOfBERates);
                string command = ":ECONtour" + numOfECOtour + ":PSPec PROBability";
                MyIO.WriteString(command);
                WriteOpc(command, 2, 4);
                //MyIO.WriteString(":ECONtour" + numOfECOtour + ":DISplay OFF");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                MyIO.WriteString(":ECONtour" + numOfECOtour + ":DISplay OFF");
            }
        }


        public void DebugFuntion()
        {
            pglobalParameters = new globalParameters();
            pglobalParameters.CurrentSN = "N01";
            pglobalParameters.CurrentTemp = 20;
            pglobalParameters.StrPathOEyeDiagram = @"D:\PrtSc\EyeDiagram";
        }


    }
}
