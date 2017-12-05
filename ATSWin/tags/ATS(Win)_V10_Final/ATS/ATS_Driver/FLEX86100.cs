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
        protected string[] dcacurrentchannel = new string[4]; 
        public Algorithm algorithm = new Algorithm();
        public string[] strOpticalChannel = new string[4];
        public string[] strElecChannel = new string[4];

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
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"IOTYPE",out i))
                {
                    IOType = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"NAME",out i))
                {
                    Name = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"CONFIGFILEPATH",out i))
                {
                    configFilePath = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no CONFIGFILEPATH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXDCADATARATE",out i))
                {
                    FlexDcaDataRate = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXDCADATARATE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FILTERSWITCH",out i))
                {
                    FilterSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FILTERSWITCH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXFILTERFREQ",out i))
                {
                    FlexFilterFreq = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXFILTERFREQ");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"TRIGGERSOURCE",out i))
                {
                    triggerSource = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no TRIGGERSOURCE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXTRIGGERBWLIMIT",out i))
                {
                    FlexTriggerBwlimit = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXTRIGGERBWLIMIT");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALSLOT",out i))
                {
                    opticalSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALSLOT");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"ELECSLOT",out i))
                {
                    elecSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ELECSLOT");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXDCAWAVELENGTH",out i))
                {
                    FlexDcaWavelength = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXDCAWAVELENGTH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALATTSWITCH",out i))
                {
                    opticalAttSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALATTSWITCH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"ERFACTORSWITCH",out i))
                {
                    erFactorSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ERFACTORSWITCH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXDCAATT",out i))
                {
                    FlexDcaAtt = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXDCAATT");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"ERFACTOR",out i))
                {
                    erFactor = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }

                else
                {
                    logger.AdapterLogString(4, "there is no ERFACTOR");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXSCALE",out i))
                {
                    FlexScale = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXSCALE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXOFFSET",out i))
                {
                    FlexOffset = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXOFFSET");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"THRESHOLD",out i))
                {
                    Threshold = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no THRESHOLD");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXOPTCHANNEL",out i))
                {
                    FlexOptChannel = FLEX86100Struct[i].DefaultValue;
                    strOpticalChannel = FlexOptChannel.Split(',');
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXOPTCHANNEL");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"FLEXELECCHANNEL",out i))
                {
                    FlexElecChannel = FLEX86100Struct[i].DefaultValue;
                    strElecChannel = FlexElecChannel.Split(',');
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXELECCHANNEL");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASEMODULESLOT",out i))
                {
                    precisionTimebaseModuleSlot = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no PRECISIONTIMEBASEMODULESLOT");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASESYNCHMETHOD",out i))
                {
                    precisionTimebaseSynchMethod = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no PRECISIONTIMEBASESYNCHMETHOD");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"RAPIDEYESWITCH",out i))
                {
                    rapidEyeSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RAPIDEYESWITCH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"REFERENCE",out i))
                {
                    reference = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no REFERENCE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINTYPE",out i))
                {
                    marginType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINTYPE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINHITTYPE",out i))
                {
                    marginHitType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINHITTYPE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"ACQLIMITTYPE",out i))
                {
                    acqLimitType = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ACQLIMITTYPE");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"PRECISIONTIMEBASEREFCLK",out i))
                {
                    precisionTimebaseRefClk = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no PRECISIONTIMEBASEREFCLK");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINHITRATIO",out i))
                {
                    marginHitRatio = Convert.ToDouble(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINHITRATIO");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALMASKNAME",out i))
                {
                    opticalMaskName = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALMASKNAME");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"ELECMASKNAME",out i))
                {
                    elecMaskName = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ELECMASKNAME");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"OPTICALEYESAVEPATH",out i))
                {
                    opticalEyeSavePath = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPTICALEYESAVEPATH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"ELECEYESAVEPATH",out i))
                {
                    elecEyeSavePath = FLEX86100Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ELECEYESAVEPATH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"MARGINHITCOUNT",out i))
                {
                    marginHitCount = Convert.ToInt16(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no MARGINHITCOUNT");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct,"ACQLIMITNUMBER",out i))
                {
                    acqLimitNumber = Convert.ToInt16(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ACQLIMITNUMBER");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct, "FLEXSETSCALEDELAY", out i))
                {
                    flexsetscaledelay = Convert.ToInt32(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no FLEXSETSCALEDELAY");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct, "DIFFSWITCH", out i))
                {
                    DiffSwitch = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no DIFFSWITCH");
                    return false;
                }
                if (algorithm.FindFileName(FLEX86100Struct, "BANDWIDTH", out i))
                {
                    BandWidth = Convert.ToByte(FLEX86100Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no BANDWIDTH");
                    return false;
                }
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
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            string offset = ""; 
            CurrentChannel = channel;
            if (offsetlist.ContainsKey(CurrentChannel))
                offset = offsetlist[CurrentChannel];
            double temp = Convert.ToDouble(offset);
            logger.AdapterLogString(0, "FLEX86100 Offset is" + offset);
            SetAttenuation(strOpticalChannel[Convert.ToByte(CurrentChannel)-1], temp, 1, syn);
            SetAttenuation(strElecChannel[Convert.ToByte(CurrentChannel) - 1], temp, 1, syn);
           
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

                        MY_Scope = new IOPort(IOType, "GPIB::" + Addr, logger);
                        MY_Scope.IOConnect();
                        EquipmentConnectflag = MY_Scope.Connect_flag;
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

                    for (byte i = 0; i < 4; i++)
                    {
                        MY_Scope.WriteString(":SYSTem:MODel? SLOT" + (i + 1).ToString());
                        if (!MY_Scope.ReadString().Contains("Not Present"))
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public bool ConfigurePreSet(int syn = 0)
        {
          //  MY_Scope.WriteString(":SYSTem:DEFault");
            MY_Scope.WriteString(":ACQuire:STOP");
            SetMode(0, syn);
           // MY_Scope.WriteString(":DISPlay:ETUNing ON");
            MY_Scope.WriteString(":DISPlay:TOVerlap OFF");
            RapidEyeSetup(FlexDcaDataRate, 0, syn);

            return true;
        }

        public bool ConfigureInputSignal(int syn = 0)
        {
            string returnmod;
            bool useptb = false;

            try
            {
                if (triggerSource == 0)// Trigger
                {

                    TriggerSetup(triggerSource, FlexTriggerBwlimit, syn);//FrontPanel
                    //添加判断，使用PTB or86107A
                }
                else// FreeRun PTB
                {
                    TriggerSetup(triggerSource, FlexTriggerBwlimit, syn);

                    #region//  查询仪器前面板支持接入PTB 信号
                    MY_Scope.WriteString(":SYSTem:OPT?");
                    string returnptb = MY_Scope.ReadString();
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
                        MY_Scope.WriteString(":system:mod? slot" + precisionTimebaseModuleSlot);
                        returnmod = MY_Scope.ReadString();

                        if (returnmod.ToUpper().Contains("86107A"))
                        {
                            PrecisionTimebaseSetup(precisionTimebaseModuleSlot, precisionTimebaseRefClk, 1, precisionTimebaseSynchMethod, syn);
                        }
                        else
                        {
                            logger.AdapterLogString(3, "I Can't find 86107A");
                            return false;
                                
                        }
                        
                    }
                }

              
            }
            catch
            {
                return false;
            }

            return true;
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
          
            return true;
        }

        public bool ConfigureElecChannel(List<string> ech, int syn = 0)
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

            return true;
        }

        public bool ConfigureCommonSetting(int syn = 0)
        {
            MessureThresholdSetup(Threshold, reference, syn);
            MaskONOFF(false, syn);
            MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
            AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
            AcquisitionLimitTestSwitch(0, syn);
            AcquisitionControl(0);
            CenterEye();

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
        /// <summary>
        /// 测试光眼图，测量结果以数组形式返回，依次为AP,ER,OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
        /// </summary>
        /// <returns></returns>
        public override bool OpticalEyeTest(out double[]testResults, int syn=0)
        {
            testResults = new double[9];
            int count = 0;
            bool isOk = false;
            bool SaveFlag = false;
            int i;
            try
            {
                string strChannel = "1A";
                strChannel = strOpticalChannel[Convert.ToByte(CurrentChannel)-1];
                EyeTuningDisplay(0, 1);
                MY_Scope.WriteString(":ACQuire:REYE ON");
                MY_Scope.WriteString(":DISPlay:TOVerlap OFF");
              // SetAttenuation(strChannel, FlexDcaAtt, opticalAttSwitch);//channel change have setted
                SetMode(0, syn);
                AcquisitionControl(2);
                AcquisitionLimitTestSwitch(0,syn);
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
                AcquisitionControl(0);
                AutoScale(syn);
                AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
                AcquisitionLimitTestSwitch(1, syn);
                AcquisitionControl(2);
                LoadMask(strChannel, opticalMaskName, syn);
                MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                do
                {
                    testResults[0] = ReadPower(1);
                    testResults[1] = ReadER(0);
                    testResults[2] = ReadAMPLitude();
                    testResults[3] = ReadCrossing();
                    testResults[4] = ReadJitter(0);
                    testResults[5] = ReadJitter(1);
                    testResults[6] = ReadRiseFallTime(0);
                    testResults[7] = ReadRiseFallTime(1);
                    testResults[8] = ReadMaskMargin();
                    for (i = 0; i < testResults.Length; i++)
                    {
                        if (testResults[i]>=10000000) break;
                    }
                    if(i >=testResults.Length) 
                    { 
                        isOk = true; 
                        break;
                    }
                    else
                        Thread.Sleep(200);

                } while (count++ < 3);

                if (isOk == true)

                    for (int k = 0; k < 3; k++)
                    {
                        SaveFlag = SaveEyeDiagram(pglobalParameters.StrPathOEyeDiagram, 0, 0);
                        if (SaveFlag) break;
                    }
                if (!SaveFlag)
                {
                    MessageBox.Show("眼图保存失败!!!");
                    logger.AdapterLogString(3, "Save Eye Image Error");
                    isOk = false;
                }
                AcquisitionLimitTestSwitch(0, syn);
                AcquisitionControl(0);
                MaskONOFF(false, syn);
                //MY_Scope.WriteString(":DISPlay:ETUNing ON");
                MY_Scope.WriteString(":ACQuire:REYE OFF");//Leo 修改
                return isOk;
            }
            catch (Exception error)
            {
                MY_Scope.WriteString(":ACQuire:REYE OFF");//Leo 修改
                logger.AdapterLogString(3, error.ToString());
                return isOk;
            }
        }
        /// <summary>
        /// 测试电眼图，测量结果以数组形式返回，依次为OMA,Crossing,JitterPP,JitterRMS,RisTime,FallTime,EMM
        /// </summary>
        /// <returns></returns>
        public override bool ElecEyeTest(out double[]testResults, int syn=0)
        {
            testResults = new double[9];
            int count = 0;
            bool isOk = false;
            bool SaveFlag = false;
            int i;
            try
            {
                string strChannel = "2A";
                strChannel = strElecChannel[Convert.ToByte(CurrentChannel)-1];

                EyeTuningDisplay(0, 1);
                MY_Scope.WriteString(":ACQuire:REYE ON");
                MY_Scope.WriteString(":DISPlay:TOVerlap OFF");
                SetMode(0, syn);
                AcquisitionControl(2);
                AcquisitionLimitTestSwitch(0, syn);
                MaskONOFF(false, syn);
                DisplayClearAlllist();
                DisplayRiseFallTime(1);
                DisplayRiseFallTime(0);
                DisplayJitter(1);
                DisplayJitter(0);
                MY_Scope.WriteString(":MEASure:EYE:EWIDth");
                MY_Scope.WriteString(":MEASure:EYE:EHEight");
                DisplayCrossing();
                DisplayOther(1);
                AcquisitionControl(0);
                AutoScale(syn);
                AcquisitionLimitTestSetup(acqLimitType, acqLimitNumber, syn);
                AcquisitionLimitTestSwitch(1, syn);
                AcquisitionControl(2);
                LoadMask(strChannel, elecMaskName, syn);
                MaskTestMarginSetup(1, marginType, 0, marginHitType, marginHitRatio, marginHitCount, syn);
                do
                {
                    Thread.Sleep(200);
                    testResults[0] = ReadAMPLitude();
                    testResults[1] = ReadCrossing();
                    testResults[2] = ReadJitter(0);
                    testResults[3] = ReadJitter(1);
                    testResults[4] = ReadRiseFallTime(0);
                    testResults[5] = ReadRiseFallTime(1);
                    testResults[6] = ReadMaskMargin();
                    testResults[7] = ReadEyeHeight();
                    testResults[8] = ReadEyeWidth();
                    for (i = 0; i < testResults.Length; i++)
                    {
                        if (testResults[i] >=10000000) break;
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
                    logger.AdapterLogString(3, "Save Eye Image Error");
                    isOk = false;
                }
                AcquisitionLimitTestSwitch(0, syn);
                AcquisitionControl(0);
                MaskONOFF(false, syn);
            
                MY_Scope.WriteString(":ACQuire:REYE OFF");//Leo 修改
                return isOk;
            }
            catch (Exception error)
            {
                MY_Scope.WriteString(":ACQuire:REYE OFF");//Leo 修改
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
        private bool TriggerSetup(byte triggerSource, byte triggerBandwidth, int syn = 0)
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
                    MY_Scope.WriteString(":TRIGger:SOURce FPANel");
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
                        logger.AdapterLogString(0, "TriggerBwlimit is" + str_TriggerBwlimit);
                        return MY_Scope.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":TRIGGER:BWLIMIT " + str_TriggerBwlimit);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":TRIGGER:BWLIMIT?");
                                readtemp = MY_Scope.ReadString();
                                if (readtemp == returntrigbw)
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "TriggerBwlimit is" + str_TriggerBwlimit);
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set TriggerBwlimit wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "TRIGger SOURce is FRUN");

                        return MY_Scope.WriteString(":TRIGger:SOURce FRUN");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":TRIGger:SOURce FRUN");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":TRIGger:SOURce?");
                                readtemp = MY_Scope.ReadString();
                                if (readtemp == "FRUN\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "TRIGger SOURce is FRUN");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "set TRIGger SOURce wrong");

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
        private bool DIFFWaveformSwitch(string channel,byte DiffSwitch, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                if (DiffSwitch == 1)
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "differential waveform is on");
                        return MY_Scope.WriteString(":DIFF" + channel +":DMODe ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":DIFF" + channel + ":DMODe ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":DIFF" + channel + ":DMODe?");
                                readtemp = MY_Scope.ReadString();
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
                        return MY_Scope.WriteString(":DIFF" + channel + ":DMODe OFF");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":DIFF" + channel + ":DMODe OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":DIFF" + channel + ":DMODe?");
                                readtemp = MY_Scope.ReadString();
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
        private bool BandwidthSet(string channel, byte bandwidth, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            try
            {
                
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "bandwidth is" + bandwidth);
                    return MY_Scope.WriteString(":CHAN" + channel + ":BANDwidth" + " BANDwidth" + bandwidth);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":CHAN" + channel + ":BANDwidth" + " BANDwidth" + bandwidth);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":CHAN" + channel + ":BANDwidth?");
                            readtemp = MY_Scope.ReadString();
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
        /// 波长（0=850,1=1310,2=1550）
        /// </param>
        /// <returns></returns>
        private bool WavelengthSelect(string channel, byte waveLength, int syn = 0)
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
                    logger.AdapterLogString(0, "Wavelength_index is" + Wavelength_index);
                    return MY_Scope.WriteString(":CHAN" + channel + ":WAVelength" + " WAVelength" + Wavelength_index);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":CHAN" + channel + ":WAVelength" + " WAVelength" + Wavelength_index);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":CHAN" + channel + ":WAVelength?");
                            readtemp = MY_Scope.ReadString();
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
        private bool FileterSwitch(string channel, byte filterSwith, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            string index="";
            try
            {
                if (filterSwith == 1)
                    index = "ON";
                if (filterSwith == 0)
                    index = "OFF";
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "filterSwith is" + index);
                    return MY_Scope.WriteString(":CHAN" + channel + ":FILTer " + index);

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":CHAN" + channel + ":FILTer " + index);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":CHAN" + channel + ":FILTer?");
                            readtemp = MY_Scope.ReadString();
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
        private bool SetAttenuation(string channel, double attenuation, byte attSwitch, int syn = 0)
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

                        MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe ON");
                        MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:DECibels " + attenuation.ToString() + "\n");
                        logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "attenuation is" + attenuation);
                        return true;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag2 = MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe ON");
                            flag1 = MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:DECibels " + attenuation.ToString() + "\n");
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe?");
                                readtemp = MY_Scope.ReadString();
                                MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:DECibels?");
                                readtemp2 = MY_Scope.ReadString();

                                if ((readtemp == "1\n") && (Convert.ToDouble(readtemp2) == Convert.ToDouble(attenuation.ToString())))
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "attenuation is" + attenuation);
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "FLEX86100 set attenuation wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, "FLEX86100 ATTenuator STATe IS off");
                        return MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe OFF");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":CHAN" + channel + ":ATTenuator:STATe?");
                                readtemp = MY_Scope.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "FLEX86100 ATTenuator STATe IS off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, "FLEX86100 set ATTenuator STATe wrong");

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
                        flag2 = MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor ON");

                        flag1 = MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor  " + (erFactorPercent).ToString() + "\r");
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag2 = MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor ON");

                            flag1 = MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor  " + (erFactorPercent).ToString() + "\r");
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":ACFactor?");
                                readtemp = MY_Scope.ReadString();
                                MY_Scope.WriteString(":MEASure:ERATio:CHAN" + channel + ":CFACtor?");
                                readtemp2 = MY_Scope.ReadString();

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
                        return MY_Scope.WriteString(":MEASure:ERATio:CHAN1A:ACFactor OFF");     

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":MEASure:ERATio:CHAN1A:ACFactor OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":MEASure:ERATio:CHAN1A:ACFactor?");
                                readtemp = MY_Scope.ReadString();
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
                        flag1 = MY_Scope.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod " + strSynchMethod + ";" + ":PTIMebase" + slot.ToString() + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                        flag2 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "ON", 0.01, 5);
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod " + strSynchMethod + ";" + ":PTIMebase" + slot.ToString() + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                            flag2 = WriteOpc(":PTIMebase" + slot.ToString() + ":STATe " + "ON", 0.01, 5);
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":PTIMebase" + slot.ToString() + ":RMEThod?");
                                readtemp = MY_Scope.ReadString();
                                MY_Scope.WriteString(":PTIMebase" + slot.ToString() + ":RFRequency?");
                                readtemp1 = MY_Scope.ReadString();
                                MY_Scope.WriteString(":PTIMebase" + slot.ToString() + ":STATe?");
                                readtemp2 = MY_Scope.ReadString();
                                if ((readtemp == strSynchMethod + "\n") && (Convert.ToDouble(readtemp1) == Convert.ToDouble(refClkFrequency.ToString("E"))) && (readtemp2 == "ON\n"))
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "PTIMebase Slot is" + slot.ToString() + "RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString("E")  + "STATe is on");
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
                                MY_Scope.WriteString(":PTIMebase" + slot.ToString() + ":STATe?");
                                readtemp = MY_Scope.ReadString();
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

        private bool PTBSetup(double refClkFrequency, byte Switch, byte synchMethod, int syn = 0)
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
                        logger.AdapterLogString(0, "PTB RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString() + "STATe is on");
                        flag1 = MY_Scope.WriteString(":TIM:PTIMebase" + ":RMEThod " + strSynchMethod + ";" + ":TIM:PTIMebase" + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                        flag2 = MY_Scope.WriteString(":TIM:PTIMebase" + ":STATe " + "ON");
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {

                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":TIM:PTIMebase" + ":RMEThod " + strSynchMethod + ";" + ":TIM:PTIMebase" + ":RFRequency " + (refClkFrequency).ToString("E") + "\n");
                            flag2 = MY_Scope.WriteString(":TIM:PTIMebase" + ":STATe " + "ON"); 
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":TIM:PTIMebase" + ":RMEThod?");
                                readtemp = MY_Scope.ReadString();
                                MY_Scope.WriteString(":TIM:PTIMebase" + ":RFRequency?");
                                readtemp1 = MY_Scope.ReadString();
                                MY_Scope.WriteString(":TIM:PTIMebase" + ":STATe?");
                                readtemp2 = MY_Scope.ReadString();
                                if ((readtemp == strSynchMethod + "\n") && (Convert.ToDouble(readtemp1) == Convert.ToDouble(refClkFrequency.ToString("E"))) && (readtemp2 == "ON\n"))
                                    break;
                                
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "PTB RMEThod IS" + strSynchMethod + "RFRequency IS" + (refClkFrequency).ToString() + "STATe is on");
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
                        logger.AdapterLogString(0, "PTIMebase STATe is off");
                        return MY_Scope.WriteString(":TIM:PTIMebase" + ":STATe " + "OFF");

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":TIM:PTIMebase" + ":STATe " + "OFF");
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MY_Scope.WriteString(":TIM:PTIMebase" + ":STATe?");
                                readtemp = MY_Scope.ReadString();
                                if (readtemp2 == "OFF\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "PTIMebase STATe is off");
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
                        logger.AdapterLogString(0, "FLEX86100 bitRate is" + bitRate + "rapidEyeSwitch is" + "ON");
                       flag2 = MY_Scope.WriteString(":ACQ:REYE " + "ON");
                       // flag2 = MY_Scope.WriteString(":ACQ:REYE " + "OFF");
                        flag1 = MY_Scope.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag2 = MY_Scope.WriteString(":ACQ:REYE " + "ON");

                            flag1 = MY_Scope.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MY_Scope.WriteString(":ACQ:REYE?");
                                readtemp = MY_Scope.ReadString();
                                MY_Scope.WriteString(":TRIG:BRAT?");
                                readtemp2 = MY_Scope.ReadString();

                                if ((readtemp == "1\n") && (Convert.ToDouble(readtemp2) == Convert.ToDouble(bitRate.ToString("E"))))
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, "FLEX86100 bitRate is" + bitRate + "rapidEyeSwitch is" + "ON");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, " RapidEyeSetup wrong");

                            }

                        }
                        return flag;
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        flag1 = MY_Scope.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                        logger.AdapterLogString(0, " RapidEyeSetup STATe IS off");
                        flag2 = MY_Scope.WriteString(":ACQ:REYE " + "OFF");
                        if ((flag1 == true) && (flag2 == true))
                        { flag = true; }
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                           
                            flag2 = MY_Scope.WriteString(":ACQ:REYE " + "OFF");
                            flag1 = MY_Scope.WriteString(":TRIG:BRAT " + bitRate.ToString("E"));
                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if (flag1 == true && flag2==true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":ACQ:REYE?");
                                readtemp = MY_Scope.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, " RapidEyeSetup STATe IS off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, " RapidEyeSetup wrong");

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
        /// 眼图实时刷新显示方式开关
        /// </summary>
        /// <param name="Switch">
        /// 1=ON,0=OFF
        /// </param>
        /// <returns></returns>
        private bool EyeTuningDisplay(byte Switch, int syn = 0)
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
                        return MY_Scope.WriteString(":DISPlay:ETUNing ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":DISPlay:ETUNing ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":DISPlay:ETUNing?");
                                readtemp = MY_Scope.ReadString();
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
                        return MY_Scope.WriteString(":DISPlay:ETUNing OFF");

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":DISPlay:ETUNing OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":DISPlay:ETUNing?");
                                readtemp = MY_Scope.ReadString();
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
                        strthreshold = "UDEF";
                        break;
                    default:
                        strthreshold = "P205080";
                        break;
                }

                if (syn == 0)
                {
                    logger.AdapterLogString(0, " THReshold reference is" + strreference);
                    logger.AdapterLogString(0, " THReshold METHod is" + strthreshold);

                    flag1= MY_Scope.WriteString(":MEASure:THReshold:EREFerence " + strreference);
                     flag2=MY_Scope.WriteString(":MEASure:THReshold:METHod " + strthreshold);
                     if ((flag1 == true) && (flag2 == true))
                     { flag = true; }
                     return flag;

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":MEASure:THReshold:EREFerence " + strreference);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MY_Scope.WriteString(":MEASure:THReshold:EREFerence?");
                            readtemp = MY_Scope.ReadString();
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
                        flag1 = MY_Scope.WriteString(":MEASure:THReshold:METHod " + strthreshold);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            MY_Scope.WriteString(":MEASure:THReshold:METHod?");
                            readtemp = MY_Scope.ReadString();
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
                EyeChannelDisplaySwitch(strElecChannel[i], 0, syn   );
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
        private bool EyeChannelDisplaySwitch(string channel, byte Switch, int syn = 0)
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
                            logger.AdapterLogString(0, " ChannelDisplaySwitch is on");

                            flag = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "1");


                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                // flag1 = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "1");

                                flag1 = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "1");

                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (k = 0; k < 3; k++)
                                {
                                    MY_Scope.WriteString(":CHAN" + channel + ":DISPlay?");
                                    readtemp = MY_Scope.ReadString();
                                    if (readtemp == "1\n")
                                        break;
                                }
                                if (k <= 3)
                                {
                                    logger.AdapterLogString(0, " ChannelDisplaySwitch is on");
                                    flag = true;
                                }
                                else
                                {
                                    logger.AdapterLogString(3, " ChannelDisplaySwitch wrong");

                                }

                            }
                        }
                    }
                    else
                    {
                        if (syn == 0)
                        {
                            logger.AdapterLogString(0, " ChannelDisplaySwitch is off");
                            flag = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                flag1 = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                                if (flag1 == true)
                                    break;
                            }
                            if (flag1 == true)
                            {
                                for (j = 0; j < 3; j++)
                                {
                                    MY_Scope.WriteString(":CHAN" + channel + ":DISPlay?");
                                    readtemp = MY_Scope.ReadString();
                                    if (readtemp == "0\n")
                                        break;
                                }
                                if (j <= 3)
                                {
                                    logger.AdapterLogString(0, " ChannelDisplaySwitch is off");
                                    flag = true;
                                }
                                else
                                {
                                    logger.AdapterLogString(3, " ChannelDisplaySwitch wrong");

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
                      flag=  MY_Scope.WriteString(":DIff" + channel + ":Display ON");
                    }
                    else
                    {
                        flag = MY_Scope.WriteString(":DIff" + channel + ":Display OFF");
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
            private bool ChannelDisplaySwitch(string channel, byte Switch, int syn = 0)
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
                        logger.AdapterLogString(0, " ChannelDisplaySwitch is on");
                        flag = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "1");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "1");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MY_Scope.WriteString(":CHAN" + channel + ":DISPlay?");
                                readtemp = MY_Scope.ReadString();
                                if (readtemp == "1\n")
                                    break;
                            }
                            if (k <= 3)
                            {
                                logger.AdapterLogString(0, " ChannelDisplaySwitch is on");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, " ChannelDisplaySwitch wrong");

                            }

                        }
                    }
                }
                else
                {
                    if (syn == 0)
                    {
                        logger.AdapterLogString(0, " ChannelDisplaySwitch is off");
                        flag= MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":CHAN" + channel + ":DISPlay " + "0");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (j = 0; j < 3; j++)
                            {
                                MY_Scope.WriteString(":CHAN" + channel + ":DISPlay?");
                                readtemp = MY_Scope.ReadString();
                                if (readtemp == "0\n")
                                    break;
                            }
                            if (j <= 3)
                            {
                                logger.AdapterLogString(0, " ChannelDisplaySwitch is off");
                                flag = true;
                            }
                            else
                            {
                                logger.AdapterLogString(3, " ChannelDisplaySwitch wrong");

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
            int j=0;
            string readtemp = "";
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "FLEX86100 channel is" + channel + "scale is" + (scale / 1000000.0).ToString() + "offset is" + (offset / 1000000.0).ToString());
                    return MY_Scope.WriteString(":CHAN" + channel + ":YSCale " + (scale / 1000000.0).ToString() + "\n" + ":CHAN" + channel + ":YOFFset " + (offset / 1000000.0).ToString());
  
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":CHAN" + channel + ":YSCale " + (scale / 1000000.0).ToString() + "\n" + ":CHAN" + channel + ":YOFFset " + (offset / 1000000.0).ToString());

                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":CHAN" + channel + ":YSCale?");
                            readtemp = MY_Scope.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble((scale / 1000000.0).ToString()))
                                break;
                        }
                        for (j = 0; j < 3; j++)
                        {

                            MY_Scope.WriteString(":CHAN" + channel + ":YOFFset?");
                            readtemp = MY_Scope.ReadString();
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
        public override bool SetScaleOffset(double PowerVaule,int syn=0)
        {
            double avgpow ;
            try
            {
                string OpticalChannel = strOpticalChannel[Convert.ToByte(CurrentChannel) - 1];
                avgpow = GetAveragePowerWatt();

                if (avgpow < 1E+6 && avgpow>1)
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
                Thread.Sleep(flexsetscaledelay);
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
                string AA = @MaskName;
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "FLEX86100 Channel is" + Channel + "MaskName is" + MaskName);

                    flag1 = MY_Scope.WriteString(":MTESt:SOURce CHAN" + Channel);
                    flag2 = MY_Scope.WriteString(":MTESt:LOAD:FNAMe " + "\"" + AA + "\"");
                    MY_Scope.WriteString(":MTESt:LOAD");
                    if ((flag1 == true) && (flag2 == true))
                    { flag = true; }
                    return flag;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":MTESt:SOURce CHAN" + Channel);
                        flag2 = MY_Scope.WriteString(":MTESt:LOAD:FNAMe " + "\"" + AA + "\"");
                        MY_Scope.WriteString(":MTESt:LOAD");
                        if ((flag1 == true) && (flag2 == true))
                            break;
                    }
                    if ((flag1 == true) && (flag2 == true))
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":MTESt:SOURce?");
                            readtemp = MY_Scope.ReadString();
                            if (readtemp == "CHAN" + Channel + "\n")
                                break;
                        }
                        for (j = 0; j < 3; j++)
                        {

                            MY_Scope.WriteString(":MTESt:LOAD:FNAMe?");
                            readtemp = MY_Scope.ReadString();
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
                        flag= MY_Scope.WriteString(":mtest:DISP " + "ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":mtest:DISP " + "ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MY_Scope.WriteString(":mtest:DISP?");
                                readtemp = MY_Scope.ReadString();
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
                        flag = MY_Scope.WriteString(":mtest:DISP " + "OFF");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":mtest:DISP " + "OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {
                                MY_Scope.WriteString(":mtest:DISP?");
                                readtemp = MY_Scope.ReadString();
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
        private bool MaskTestMarginSetup(byte marginOnOff, byte marginAutoManul, int manualMarginPercent, byte autoMarginType, double hitRatio, int hitCount, int syn = 0)
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
                         MY_Scope.WriteString(":MTESt:MARGin:STATe ON\n");
                    }
                    else
                    {
                       MY_Scope.WriteString(":MTESt:MARGin:STATe OFF\n");
                    }
                    if (marginAutoManul != 1)//marginType=0  ManulMargin
                    {
                        MY_Scope.WriteString(":MTESt:MARGin:METHod MANual\n");
                        MY_Scope.WriteString(":MTESt:MARGin:PERCent " + manualMarginPercent.ToString("E") + "\n");
                    }
                    else//marginType=1  AutoMargin
                    {

                        if (autoMarginType != 1)//autoMarginType=0 HitCount
                        {
                             MY_Scope.WriteString(":MTESt:MARGin:METHod AUTO\n");
                             MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod HITS\n");
                             MY_Scope.WriteString(":MTESt:MARGin:AUTo:HITs " + hitCount.ToString() + "\n");
                        }
                        else
                        {
                            MY_Scope.WriteString(":MTESt:MARGin:METHod AUTO\n");
                            MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod HRATio\n");
                            MY_Scope.WriteString(":MTESt:MARGin:AUTo:HRATio " + hitRatio.ToString("E") + "\n");
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
                            flag1 = MY_Scope.WriteString(":MTESt:MARGin:STATe ON\n");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":MTESt:MARGin:STATe?");
                                readtemp = MY_Scope.ReadString();
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
                            flag1 = MY_Scope.WriteString(":MTESt:MARGin:STATe OFF\n");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":MTESt:MARGin:STATe?");
                                readtemp = MY_Scope.ReadString();
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
                            flag1 = MY_Scope.WriteString(":MTESt:MARGin:METHod MANual\n");
                            flag2 = MY_Scope.WriteString(":MTESt:MARGin:PERCent " + manualMarginPercent.ToString("E") + "\n");

                            if ((flag1 == true) && (flag2 == true))
                                break;
                        }
                        if ((flag1 == true) && (flag2 == true))
                        {
                            for (j = 0; j < 3; j++)
                            {

                                MY_Scope.WriteString(":MTESt:MARGin:METHod?");
                                readtemp = MY_Scope.ReadString();
                                MY_Scope.WriteString(":MTESt:MARGin:PERCent?");
                                readtemp1 = MY_Scope.ReadString();
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
                                flag1 = MY_Scope.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                flag2 = MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod HITS\n");
                                flag3 = MY_Scope.WriteString(":MTESt:MARGin:AUTo:HITs " + hitCount.ToString() + "\n");

                                if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                            {
                                for (j = 0; j < 3; j++)
                                {

                                    MY_Scope.WriteString(":MTESt:MARGin:METHod?");
                                    readtemp = MY_Scope.ReadString();
                                    MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod?");
                                    readtemp1 = MY_Scope.ReadString();
                                    MY_Scope.WriteString(":MTESt:MARGin:AUTo:HITs?");
                                    readtemp2 = MY_Scope.ReadString();
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
                                flag1 = MY_Scope.WriteString(":MTESt:MARGin:METHod AUTO\n");
                                flag2 = MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod HRATio\n");
                                flag3 = MY_Scope.WriteString(":MTESt:MARGin:AUTo:HRATio " + hitRatio.ToString("E") + "\n");

                                if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                                    break;
                            }
                            if ((flag1 == true) && (flag2 == true) && (flag3 == true))
                            {
                                for (j = 0; j < 3; j++)
                                {

                                    MY_Scope.WriteString(":MTESt:MARGin:METHod?");
                                    readtemp = MY_Scope.ReadString();
                                    MY_Scope.WriteString(":MTESt:MARGin:AUTo:METHod?");
                                    readtemp1 = MY_Scope.ReadString();
                                    MY_Scope.WriteString(":MTESt:MARGin:AUTo:HRATio?");
                                    readtemp2 = MY_Scope.ReadString();
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
                return WriteOpc(":ACQuire:" + strcontrol, 1, 30);
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
                    return MY_Scope.WriteString(":system:mode " + index);
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":system:mode " + index);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":system:mode?");
                            readtemp = MY_Scope.ReadString();
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
                    return MY_Scope.WriteString(":LTESt:ACQuire:CTYPe " + index + ";" + ":LTESt:ACQuire:CTYPe:" + index + " " + limitNumber.ToString());

                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":LTESt:ACQuire:CTYPe " + index + ";" + ":LTESt:ACQuire:CTYPe:" + index + " " + limitNumber.ToString());

                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":LTESt:ACQuire:CTYPe?");
                            readtemp = MY_Scope.ReadString();
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
                    logger.AdapterLogString(0, "display eye num is" +(3.0000).ToString());
                    return MY_Scope.WriteString(":TIMebase:BRANge "+(3.0000).ToString());
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        flag1 = MY_Scope.WriteString(":TIMebase:BRANge " + (3.0000).ToString());
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {

                            MY_Scope.WriteString(":TIMebase:BRANge?");
                            readtemp = MY_Scope.ReadString();
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
                        return MY_Scope.WriteString(":LTESt:ACQuire:STATe ON");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":LTESt:ACQuire:STATe ON");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":LTESt:ACQuire:STATe?");
                                readtemp = MY_Scope.ReadString();
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
                        return MY_Scope.WriteString(":LTESt:ACQuire:STATe OFF");
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = MY_Scope.WriteString(":LTESt:ACQuire:STATe OFF");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                MY_Scope.WriteString(":LTESt:ACQuire:STATe?");
                                readtemp = MY_Scope.ReadString();
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
                //MY_Scope.WriteString(":DISK:SIMage:FNAMe 'screenshot.gif';" + ":DISK:SIMage:INVert " + INVert + ";" + ":DISK:SIMage:MONochrome " + MONochrome + "\n");
                MY_Scope.WriteString(":DISK:SIMage:FNAMe 'screenshot.gif'");

                MY_Scope.WriteString(":DISK:SIMage:FNAMe?");
                string ImagePath = MY_Scope.ReadString().Replace("\n", "");


                MY_Scope.WriteString(":DISK:SIMage:INVert " + INVert + ";" + ":DISK:SIMage:MONochrome " + MONochrome + "\n");
                
                MY_Scope.WriteString(":DISK:SIMage:SAVE;*OPC?");
                for (int j = 0; j < 20; j++)
                {
                    try
                    {
                        if (MY_Scope.ReadString(100).Contains("1"))
                        {
                            logger.AdapterLogString(0, "Scope savePath is " + ImagePath);
                            break;
                        }
                        else
                        {
                            Thread.Sleep(100);
                            if (j == 19)
                            {
                                logger.AdapterLogString(3, "save Eye Error");
                                return false;

                            }
                        }
                    }
                    catch (Exception ee)
                    {

                        logger.AdapterLogString(3, ee.Message);
                        return false;
                    }
                }
                MY_Scope.WriteString(":DISK:BFILe? " + ImagePath);
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
                File.WriteAllBytes(savePath + diagramName, (byte[])MY_Scope.myDmm.ReadIEEEBlock(IEEEBinaryType.BinaryType_UI1));
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
                    MY_Scope.WriteString(":MEASure:EYE:APOWer:Status?");
                    string ss= MY_Scope.ReadString();
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
                    logger.AdapterLogString(0,  "power is not read");
                
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
                    MY_Scope.WriteString(":MEASure:EYE:ERATio:Status?");
                    string ss = MY_Scope.ReadString();
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
        /// 读取眼图OMA，返回值为mW或者mV
        /// </summary>
        /// <returns></returns>
        private double ReadAMPLitude()
        {
            double AMPLitude = 0.0;
            try
            {
                MY_Scope.WriteString(":MEASure:EYE:AMPLitude?");
                AMPLitude = Convert.ToDouble(MY_Scope.ReadString(32)) * 1000;
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
         /// <summary>
        /// 读取眼高
        /// </summary>
        /// <returns></returns>
        private double ReadEyeHeight()
        {
            double height = 0.0;
            try
            {
                MY_Scope.WriteString(":MEASure:EYE:EHEight?");
                height = Convert.ToDouble(MY_Scope.ReadString(32))*1000.0;
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
        /// 读取眼宽
        /// </summary>
        /// <returns></returns>
        private double ReadEyeWidth()
        {
            double width = 0.0;
            try
            {
                MY_Scope.WriteString(":MEASure:EYE:EWIDth?");
                width = Convert.ToDouble(MY_Scope.ReadString(32)) * (1E+12);
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
    }

}
