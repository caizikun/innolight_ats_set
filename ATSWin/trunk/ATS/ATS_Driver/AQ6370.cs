using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using System.Reflection;
using ATS_Framework;
using System.Windows.Forms;

namespace ATS_Driver
{
    public class AQ6370 : Spectrograph
    {
        public AQ6370(logManager logmanager)
        {
            logger = logmanager;
            bReusable = true;
        }

        public Algorithm algorithm = new Algorithm();

        public override bool Initialize(TestModeEquipmentParameters[] AQ6370Struct)
        {
            try
            {               
                int i = 0;

                if (algorithm.FindFileName(AQ6370Struct, "ADDR", out i))
                {
                    Addr = Convert.ToByte(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "IOTYPE", out i))
                {
                    IOType = AQ6370Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "NAME", out i))
                {
                    Name = AQ6370Struct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "CENTERWAVELENGTH", out i))
                {
                    centerWavelength = Convert.ToDouble(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no CENTERWAVELENGTH");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "SPAN", out i))
                {
                    Span = Convert.ToDouble(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no SPAN");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "RESOLUTION", out i))
                {
                    Resolution = Convert.ToDouble(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESOLUTION");
                    return false;
                }                
                if (algorithm.FindFileName(AQ6370Struct, "REFLEVEL", out i))
                {
                    refLevel = Convert.ToDouble(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no REFLEVEL");
                    return false;
                }                
                if (algorithm.FindFileName(AQ6370Struct, "LOGSCALE", out i))
                {
                    logScale = Convert.ToDouble(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no LOGSCALE");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "SENSITIVITY", out i))
                {
                    sensitivity = Convert.ToByte(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no SENSITIVITY");
                    return false;
                }    
                if (algorithm.FindFileName(AQ6370Struct, "THRESHLEVEL", out i))
                {
                    threshLevel = Convert.ToDouble(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no THRESHLEVEL");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "KFACTOR", out i))
                {
                    kFactor = Convert.ToDouble(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no KFACTOR");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "ANALYSISMODE", out i))
                {
                    analysisMode = Convert.ToInt32(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ANALYSISMODE");
                    return false;
                }
                if (algorithm.FindFileName(AQ6370Struct, "DESTINATION", out i))
                {
                    Destination = Convert.ToByte(AQ6370Struct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no DESTINATION");
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
             
        public override bool Connect()
        {
            try
            {
                switch (IOType)
                {
                    case "GPIB":
                        MyIO = new IOPort(IOType, "GPIB::" + Addr, logger);
                        MyIO.IOConnect();
                        EquipmentConnectflag = true;
                        MyIO.WriteString("*IDN?");
                        EquipmentConnectflag = MyIO.ReadString().Contains("AQ6370");
                        break;
                    default:
                        logger.AdapterLogString(4, "GPIB类型错误");
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

                    // 配置中心波长
                    if (!ConfigCenterWavelength(centerWavelength, syn))
                    {
                        return false;
                    }
                    // 配置粗扫描波长范围,100nm
                    if (!ConfigSpan(100, syn))
                    {
                        return false;
                    }
                    // 配置分辨率
                    if (!ConfigResolution(syn))
                    {
                        return false;
                    }
                    // 配置参考功率
                    if (!ConfigRefLevel(syn))
                    {
                        return false;
                    }
                    // 配置对数刻度
                    if (!ConfigLogScale(syn))
                    {
                        return false;
                    }
                    // 配置灵敏度
                    if (!ConfigSensitivity(sensitivity,syn))
                    {
                        return false;
                    }
                    // 配置分析参数（THRESH LEVEL）
                    if (!ConfigThreshLevel(syn))
                    {
                        return false;
                    }
                    // 配置分析参数（K FACTOR）
                    if (!ConfigK(syn))
                    {
                        return false;
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

        /// <summary>
        /// 配置中心波长
        /// 范围600nm—1700nm，3位小数
        /// </summary>
        /// <param name="center">
        /// 中心波长，单位nm
        /// </param>
        /// <returns></returns>
        private bool ConfigCenterWavelength(double center,int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double centeroutput;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "CenterWavelength is " + center + "nm");
                    return MyIO.WriteString(":SENS:WAV:CENT " + center.ToString() + "NM");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":SENS:WAV:CENT " + center.ToString() + "NM");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":SENS:WAV:CENT?");
                            readtemp = MyIO.ReadString();
                            centeroutput = Convert.ToDouble(readtemp) * Math.Pow(10, 9);
                            if (center == centeroutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "CenterWavelength is " + center + "nm");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigCenterWavelength wrong");
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
        /// 配置扫描波长范围
        /// 范围0nm—1100nm，1位小数
        /// </summary>
        /// <param name="span">
        /// 扫描波长范围，单位nm
        /// </param>
        /// <returns></returns>
        private bool ConfigSpan(double span, int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double spanoutput;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "SPAN is " + span + "nm");
                    return MyIO.WriteString(":SENS:WAV:SPAN " + span.ToString() + "NM");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":SENS:WAV:SPAN " + span.ToString() + "NM");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":SENS:WAV:SPAN?");
                            readtemp = MyIO.ReadString();
                            spanoutput = Convert.ToDouble(readtemp) * Math.Pow(10, 9);
                            if (span == spanoutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "SPAN is " + span + "nm");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigSpan wrong");
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
        /// 配置分辨率，单位nm
        /// 只能设置为如下值：0.020nm、0.050nm、0.100nm、0.200nm、0.500nm、1.000nm、2.000nm
        /// </summary>
        /// <returns></returns>
        private bool ConfigResolution(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double ResolutionOutput;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Resolution is " + Resolution + "nm");
                    return MyIO.WriteString(":SENS:BAND:RES " + Resolution.ToString() + "NM");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":SENS:BAND:RES " + Resolution.ToString() + "NM");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":SENS:BAND:RES?");
                            readtemp = MyIO.ReadString();
                            ResolutionOutput = Convert.ToDouble(readtemp) * Math.Pow(10, 9);
                            if (Resolution == ResolutionOutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "Resolution is " + Resolution + "nm");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigResolution wrong");
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
        /// 配置参考功率，单位dbm
        /// 范围-90dbm—30dbm，1位小数
        /// </summary>
        /// <returns></returns>
        private bool ConfigRefLevel(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double RefLevelOutput;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "RefLevel is " + refLevel + "dbm");
                    return MyIO.WriteString(":DISP:TRAC:Y1:SCAL:RLEV " + refLevel.ToString() + "DBM");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":DISP:TRAC:Y1:SCAL:RLEV " + refLevel.ToString() + "DBM");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":DISP:TRAC:Y1:SCAL:RLEV?");
                            readtemp = MyIO.ReadString();
                            RefLevelOutput = Convert.ToDouble(readtemp);
                            if (refLevel == RefLevelOutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "RefLevel is " + refLevel + "dbm");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigRefLevel wrong");
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
        /// 配置对数刻度（垂直轴），单位db/D
        /// 范围0.1db/D—10db/D，1位小数
        /// </summary>
        /// <returns></returns>
        private bool ConfigLogScale(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double logScaleOutput;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "LogScale is " + logScale + "db/D");
                    return MyIO.WriteString(":DISP:TRAC:Y1:SCAL:PDIV " + logScale.ToString() + "DB");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":DISP:TRAC:Y1:SCAL:PDIV " + logScale.ToString() + "DB");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":DISP:TRAC:Y1:SCAL:PDIV?");
                            readtemp = MyIO.ReadString();
                            logScaleOutput = Convert.ToDouble(readtemp);
                            if (logScale == logScaleOutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "LogScale is " + logScale + "db/D");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigLogScale wrong");
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
        /// 配置灵敏度，共有七种灵敏度选项
        /// 0~6对应NORM/HOLD、NORM/AUTO、MID、HIGH1、HIGH2、HIGH3、NORMAL
        /// </summary>
        /// <returns></returns>
        private bool ConfigSensitivity(byte sensitivity,int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            byte sensitivityOutput;

            try
            {
                string sens = "";
                switch (sensitivity)
                {
                    case 0:
                        sens = "NHLD";
                        break;
                    case 1:
                        sens = "NAUT";
                        break;
                    case 2:
                        sens = "MID";
                        break;
                    case 3:
                        sens = "HIGH1";
                        break;
                    case 4:
                        sens = "HIGH2";
                        break;
                    case 5:
                        sens = "HIGH3";
                        break;
                    case 6:
                        sens = "NORMAL";
                        break;
                    default:
                        sens = "NORMAL";
                        break;
                }
                
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sensitivity is " + sens);
                    return MyIO.WriteString(":SENS:SENS " + sens);
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":SENS:SENS " + sens);
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":SENS:SENS?");
                            readtemp = MyIO.ReadString();
                            sensitivityOutput = Convert.ToByte(readtemp);
                            if (sensitivity == sensitivityOutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "Sensitivity is " + sens);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigSensitivity wrong");
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
        /// 配置分析参数（THRESH LEVEL），单位db，仅多模时需要设置
        /// </summary>
        /// <returns></returns>
        private bool ConfigThreshLevel(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double threshLevelOutput;
            string mode = "";

            try
            {
                switch (analysisMode)    //仅多模
                {
                    case 1:
                        mode = "SWRM";
                        break;
                    case 3:
                        mode = "SWTH";
                        break;
                    case 4:
                        mode = "SWPK";
                        break;
                    default:
                        mode = "SWRM";
                        break;
                }
                
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "ThreshLevel is " + threshLevel + "db");
                    return MyIO.WriteString(":CALCulate:PARameter:" + mode + ":TH " + threshLevel.ToString() + "DB");
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":CALCulate:PARameter:" + mode + ":TH " + threshLevel.ToString() + "DB");
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":CALCulate:PARameter:" + mode + ":TH?");
                            readtemp = MyIO.ReadString();
                            threshLevelOutput = Convert.ToDouble(readtemp);
                            if (threshLevel == threshLevelOutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "ThreshLevel is " + threshLevel + "db");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigThreshLevel wrong");
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
        /// 配置分析参数（K Factor），仅多模时需要设置
        /// </summary>
        /// <returns></returns>
        private bool ConfigK(int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            double kOutput;
            string mode = "";

            try
            {
                switch (analysisMode)    //仅多模
                {
                    case 1:
                        mode = "SWRM";
                        break;
                    case 3:
                        mode = "SWTH";
                        break;
                    case 4:
                        mode = "SWPK";
                        break;
                    default:
                        mode = "SWRM";
                        break;
                }
                
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "K is " + kFactor);
                    return MyIO.WriteString(":CALCulate:PARameter:" + mode + ":K " + kFactor.ToString());
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":CALCulate:PARameter:" + mode + ":K " + kFactor.ToString());
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":CALCulate:PARameter:" + mode + ":K?");
                            readtemp = MyIO.ReadString();
                            kOutput = Convert.ToDouble(readtemp);
                            if (kFactor == kOutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "K is " + kFactor);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigK wrong");
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
        /// 配置自动分析
        /// </summary>
        /// <param name="AutoAnalysis">
        /// 是否自动分析，false：OFF，true：ON
        /// </param>
        /// <returns></returns>
        private bool ConfigAutoAnalysis(bool AutoAnalysis,int syn = 0)
        {
            bool flag = false;
            bool flag1 = false;
            int k = 0;
            string readtemp = "";
            bool autoAnalysOutput;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AutoAnalysis is " + AutoAnalysis);
                    return MyIO.WriteString(":CALCulate:AUTO " + AutoAnalysis.ToString());
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        flag1 = MyIO.WriteString(":CALCulate:AUTO " + AutoAnalysis.ToString());
                        if (flag1 == true)
                            break;
                    }
                    if (flag1 == true)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            MyIO.WriteString(":CALCulate:AUTO?");
                            readtemp = MyIO.ReadString();
                            autoAnalysOutput = Convert.ToBoolean(readtemp);
                            if (AutoAnalysis == autoAnalysOutput)
                                break;
                        }

                        if (k < 3)
                        {
                            logger.AdapterLogString(0, "AutoAnalysis is " + AutoAnalysis);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "Spectrograph ConfigAutoAnalysis wrong");
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
        /// 单次扫描（SINGLE），扫描结束后自动停止（STOP）
        /// </summary>
        /// <returns></returns>
        private bool SweepSingle()
        {
            try
            {
                MyIO.WriteString(":INIT:SMOD SING;:INIT");

                for (int i = 0; i < 20; i++)
                {
                    MyIO.WriteString("*OPC?");

                    if (MyIO.ReadString().Contains("1"))
                    {
                        return true;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        if (i == 19)
                        {
                            logger.AdapterLogString(3, "SWEEP SINGLE Error");
                            return false;
                        }
                    }
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
        /// 自动扫描（AUTO），扫描结束后自动进入（REPEAT），需停止（STOP）
        /// </summary>
        /// <returns></returns>
        private bool SweepAuto()
        {
            try
            {
                MyIO.WriteString(":INIT:SMOD AUTO;:INIT");

                Thread.Sleep(10000);

                for (int i = 0; i < 50; i++)
                {
                    MyIO.WriteString(":INIT:SMOD?");

                    if (MyIO.ReadString() == "3")
                    {
                        MyIO.WriteString(":ABOR");
                        return true;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        if (i == 49)
                        {
                            logger.AdapterLogString(3, "SWEEP AUTO Error");
                            MyIO.WriteString(":ABOR");
                            return false;
                        }
                    }
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
        /// 开始扫描测量
        /// </summary>
        /// <returns></returns>
        public override bool StartSweep()
        {
            try
            {
                double SpecWd = 0;
                double CenterWl = 0;

                ConfigSpan(100);
                ConfigCenterWavelength(centerWavelength);
                ConfigSensitivity(1);

                if (!SweepSingle())   //按设置的中心波长及span=100，大范围扫描一次
                {
                    return false;
                }
               
                for (int i = 0; i < 2; i++)
                {
                    SpecWd = GetSpectralWidth();

                    if (SpecWd > 10)           //中心波长不在大范围扫描范围内
                    {
                        if (i == 1)           //自动扫描后，没有找到中心波长
                        {
                            return false;
                        }
                        else
                        {
                            if (!ConfigAutoAnalysis(true))   //开启自动分析
                            {
                                return false;
                            }
                            if (!SweepAuto())             //自动扫描
                            {
                                return false;
                            }                       
                        }
                    }
                    else                  //中心波长在大范围扫描范围内
                    {
                        CenterWl = GetCenterWavelength();

                        if (!ConfigCenterWavelength(CenterWl))
                        {
                            return false;
                        }
                        if (!ConfigSpan(Span))
                        {
                            return false;
                        }
                        if (!ConfigSensitivity(sensitivity))
                        {
                            return false;
                        }
                        if (!SweepSingle())   //按设置的扫描范围及读取的中心波长，小范围扫描一次
                        {
                            return false;
                        }

                        return true;
                    }

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
        /// 读取谱宽,单位nm，保留小数点后4位
        /// </summary>
        /// <returns></returns>
        public override double GetSpectralWidth()
        {
            double SpectralWidth = 0;
            int modeIndex;
            try
            {
                switch (analysisMode) 
                {
                    case 1:
                        modeIndex = 2;    //RMS
                        break;
                    case 2:
                        modeIndex = 5;    //DFB-LD
                        break;
                    case 3:
                        modeIndex = 0;    //THRESH
                        break;
                    case 4:
                        modeIndex = 3;    //PEAK RMS
                        break;
                    default:
                        modeIndex = 2;
                        break;
                }
                
                MyIO.WriteString(":CALC:CAT " + modeIndex.ToString());
                MyIO.WriteString(":CALC;:CALC:DATA?");
                
                string[] ss = MyIO.ReadString().Split(',');

                if (analysisMode ==1 || analysisMode == 3 || analysisMode == 4)  //RMS、THRESH、PEAK RMS  ReadString：<center wl>,<spec wd>
                {
                     SpectralWidth = Convert.ToDouble(ss[1]) * Math.Pow(10, 9);
                }
                else                                                              //DFB-LD :DATA?:<spec wd>,<center wl>
                {
                    SpectralWidth = Convert.ToDouble(ss[0]) * Math.Pow(10, 9);  
                }

                if (SpectralWidth.ToString().Contains('.'))
                {
                    string[] ssSpec = SpectralWidth.ToString().Split('.');
                    string decimalPart = ssSpec[1].Substring(0, 4);
                    SpectralWidth = Convert.ToDouble(ssSpec[0] + "." + decimalPart);
                }

                logger.AdapterLogString(0, "SpectralWidth is " + SpectralWidth + "nm");
                return SpectralWidth;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return SpectralWidth;
            }
        }

        /// <summary>
        /// 读取中心波长,单位nm，保留小数点后4位
        /// </summary>
        /// <returns></returns>
        public override double GetCenterWavelength()
        {
            double CenterWl = 0;
            int modeIndex;
            try
            {
                switch (analysisMode) 
                {
                    case 1:
                        modeIndex = 2;    //RMS
                        break;
                    case 2:
                        modeIndex = 5;    //DFB-LD
                        break;
                    case 3:
                        modeIndex = 0;    //THRESH
                        break;
                    case 4:
                        modeIndex = 3;    //PEAK RMS
                        break;
                    default:
                        modeIndex = 2;
                        break;
                }

                MyIO.WriteString(":CALC:CAT " + modeIndex.ToString());
                MyIO.WriteString(":CALC;:CALC:DATA?");
                
                string[] ss = MyIO.ReadString().Split(',');

                if (analysisMode ==1 || analysisMode == 3 || analysisMode == 4)  //RMS、THRESH、PEAK RMS  ReadString：<center wl>,<spec wd>
                {
                     CenterWl = Convert.ToDouble(ss[0]) * Math.Pow(10, 9);
                }
                else                                                             //DFB-LD :DATA?:<spec wd>,<center wl>
                {
                    CenterWl = Convert.ToDouble(ss[1]) * Math.Pow(10, 9);
                }

                if (CenterWl.ToString().Contains('.'))
                {
                    string[] ssCenter = CenterWl.ToString().Split('.');
                    string decimalPart = ssCenter[1].Substring(0, 4);
                    CenterWl = Convert.ToDouble(ssCenter[0] + "." + decimalPart);
                }

                logger.AdapterLogString(0, "CenterWavelength is " + CenterWl + "nm");
                return CenterWl;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return CenterWl;
            }
        }

        /// <summary>
        /// 读取SMSR,单位db，保留小数点后2位
        /// </summary>
        /// <returns></returns>
        public override double GetSMSR()
        {
            double SMSR = 0;
            int modeIndex;

            try
            {
                switch (analysisMode) 
                {
                    case 1:
                        modeIndex = 2;    //RMS
                        break;
                    case 2:
                        modeIndex = 5;    //DFB-LD
                        break;
                    case 3:
                        modeIndex = 0;    //THRESH
                        break;
                    case 4:
                        modeIndex = 3;    //PEAK RMS
                        break;
                    default:
                        modeIndex = 2;
                        break;
                }
                              
                if (analysisMode == 2)             //DFB-LD：<>,<>,<center wl>,<spec wd>,<SMSR>,<>,<>,<>,<OSNR>          
                {
                     MyIO.WriteString(":CALC:CAT " + modeIndex.ToString());
                     MyIO.WriteString(":CALC;:CALC:DATA:DFBL?");
                
                     string[] ss = MyIO.ReadString().Split(','); 
                    
                     SMSR = Convert.ToDouble(ss[4]);
                }


                if (SMSR.ToString().Contains('.'))
                {
                    string[] ssSMSR = SMSR.ToString().Split('.');
                    string decimalPart = ssSMSR[1].Substring(0, 2);
                    SMSR = Convert.ToDouble(ssSMSR[0] + "." + decimalPart);
                }

                logger.AdapterLogString(0, "SMSR is " + SMSR + "db");
                return SMSR;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return SMSR;
            }
        }

        /// <summary>
        /// 读取OSNR,单位db(/0.10nm)，保留小数点后2位
        /// </summary>
        /// <returns></returns>
        public override double GetOSNR()
        {
            double OSNR = 0;
            int modeIndex;
            try
            {
                switch (analysisMode) 
                {
                    case 1:
                        modeIndex = 2;    //RMS
                        break;
                    case 2:
                        modeIndex = 5;    //DFB-LD
                        break;
                    case 3:
                        modeIndex = 0;    //THRESH
                        break;
                    case 4:
                        modeIndex = 3;    //PEAK RMS
                        break;
                    default:
                        modeIndex = 2;
                        break;
                }
                
                if (analysisMode == 2)             //DFB-LD：<>,<>,<center wl>,<spec wd>,<SMSR>,<>,<>,<>,<OSNR>          
                {
                    MyIO.WriteString(":CALC:CAT " + modeIndex.ToString());
                    MyIO.WriteString(":CALC;:CALC:DATA:DFBL?");

                    string[] ss = MyIO.ReadString().Split(','); 
                    OSNR = Convert.ToDouble(ss[8]);
                }

                if (OSNR.ToString().Contains('.'))
                {
                    string[] ssOSNR = OSNR.ToString().Split('.');
                    string decimalPart = ssOSNR[1].Substring(0, 2);
                    OSNR = Convert.ToDouble(ssOSNR[0] + "." + decimalPart);
                }

                logger.AdapterLogString(0, "OSNR is " + OSNR + "db");
                return OSNR;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return OSNR;
            }
        }

        /// <summary>
        /// 保存屏幕图像数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveWaveformScreen()
        {
            string fileName = "";
            string color = "COL";
            string fileType = "BMP";
            string currentDrive = "EXT";
           
            if (Destination == 0) currentDrive = "INT";

            try
            {
                System.DateTime CurrentTime = new System.DateTime();
                CurrentTime = System.DateTime.Now;
                string Str_Year = CurrentTime.Year.ToString();
                string Str_Month = CurrentTime.Month.ToString();
                string Str_Day = CurrentTime.Day.ToString();
                string Str_Hour = CurrentTime.Hour.ToString();
                string Str_minute = CurrentTime.Minute.ToString();
                string Str_sencond = CurrentTime.Second.ToString();
                fileName = Str_Year + "_" + Str_Month + "_" + Str_Day + "-" + pglobalParameters.CurrentSN + "-" + pglobalParameters.CurrentTemp + "-" + pglobalParameters.CurrentVcc + "-" + pglobalParameters.CurrentChannel + "-" + Str_Hour + "_" + Str_minute + "_" + Str_sencond;

                if (currentDrive == "EXT")
                {
                    MyIO.WriteString(":MMEMory:CDRive INT");
                }

                MyIO.WriteString(":MMEMory:STORe:GRAPhics " + color + "," + fileType + ",\"" + fileName + "\"," + currentDrive);

                for (int i = 0; i < 3; i++)
                {
                    MyIO.WriteString(":MMEMory:CDRive?");
                    string ssDrive = MyIO.ReadString();
                    if (ssDrive != currentDrive && ssDrive == "INT")
                    {
                        if (i < 2)
                        {
                            MessageBox.Show("请确定已插入U盘后，再点击确定按钮继续!");
                            MyIO.WriteString(":MMEMory:STORe:GRAPhics " + color + "," + fileType + ",\"" + fileName + "\"," + currentDrive);
                        }
                        else
                        {
                            logger.AdapterLogString(3, "SaveWaveformScreen Error");
                            return false;
                        }                                              
                    }
                    else
                    {
                        logger.AdapterLogString(0, " SaveWaveformScreen Path is " + fileName);
                        return true;
                    }
                }

                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

    }    
}
