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
    public class N4960APPG : PPG
    {
        #region N4960APPG
        public byte channel;  // 0=channel 0 (Jitter), 1=channel 1 (Delay), 2=channel 0 (Jitter) and channel 1 (Delay)

        public byte delayedOutputSwitch;
        public double delayAmplitude;
        public byte delayCoupling;
        public double delayOffset;
        public double delayTermination;
        public double delay;

        public byte dividedOutputSwitch;
        public double subrateAmplitude;
        public byte subrateCoupling;
        public double subrateOffset;
        public double subrateTermination;
        public int subrateDivider;

        public double amplitude;
        public double dataAtten;
        public byte dataLogicLevel;
        public double dataOffset;
        public double dataTermination;
        public int dataCrossover;

        public byte dataPattern;
        public byte invert;
        #endregion

        public string PPGChannel; 
        public string PPGChannelInfo; 
        public Algorithm algorithm = new Algorithm();
        public N4960APPG(logManager logmanager)
        {
            logger = logmanager;
        }

        public override bool Initialize(TestModeEquipmentParameters[] N4960APPGStruct)
        {
            int i = 0;
            if (algorithm.FindFileName(N4960APPGStruct, "ADDR", out i))
            {
                Addr = N4960APPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no ADDR");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "IOType", out i))
            {
                IOType = N4960APPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "RESET", out i))
            {
                Reset = Convert.ToBoolean(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "NAME", out i))
            {
                Name = N4960APPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "channel".ToUpper(), out i))
            {
                channel = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no channel");
            }

            if (algorithm.FindFileName(N4960APPGStruct, "DELAYEDOUTPUTSWITCH", out i))
            {
                delayedOutputSwitch = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no delayedOutputSwitch");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "delayAmplitude".ToUpper(), out i))
            {
                delayAmplitude = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no delayAmplitude");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "delayCoupling".ToUpper(), out i))
            {
                delayCoupling = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no delayCoupling");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "delayOffset".ToUpper(), out i))
            {
                delayOffset = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no delayOffset");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "delayTermination".ToUpper(), out i))
            {
                delayTermination = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no delayTermination");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "delay".ToUpper(), out i))
            {
                delay = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no delay");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dividedOutputSwitch".ToUpper(), out i))
            {
                dividedOutputSwitch = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dividedOutputSwitch");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "subrateAmplitude".ToUpper(), out i))
            {
                subrateAmplitude = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no subrateAmplitude");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "subrateCoupling".ToUpper(), out i))
            {
                subrateCoupling = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no subrateCoupling");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "subrateOffset".ToUpper(), out i))
            {
                subrateOffset = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no subrateOffset");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "subrateTermination".ToUpper(), out i))
            {
                subrateTermination = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no subrateTermination");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "subrateDivider".ToUpper(), out i))
            {
                subrateDivider = Convert.ToInt32(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no subrateDivider");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dataSwitch".ToUpper(), out i))
            {
                dataSwitch = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataSwitch");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "amplitude".ToUpper(), out i))
            {
                amplitude = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no amplitude");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dataAtten".ToUpper(), out i))
            {
                dataAtten = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataAtten");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dataLogicLevel".ToUpper(), out i))
            {
                dataLogicLevel = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataLogicLevel");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dataOffset".ToUpper(), out i))
            {
                dataOffset = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataOffset");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dataTermination".ToUpper(), out i))
            {
                dataTermination = Convert.ToDouble(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataTermination");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dataCrossover".ToUpper(), out i))
            {
                dataCrossover = Convert.ToInt32(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataCrossover");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "DATARATE", out i))
            {
                dataRate = N4960APPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no DATARATE");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "dataPattern".ToUpper(), out i))
            {
                dataPattern = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataPattern");
                return false;
            }

            if (algorithm.FindFileName(N4960APPGStruct, "invert".ToUpper(), out i))
            {
                invert = Convert.ToByte(N4960APPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no invert");
                return false;
            }

            if (!Connect())
            {
                return false;
            }

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
                if (EquipmentConfigflag) // 曾经设定过
                {
                    return true;
                }
                else // 未曾经设定过
                {
                    if (Reset == true)
                    {
                        ReSet();
                    }

                    ConfigureChannel(channel);

                    #region ConfigureDelayedOutput
                    if (delayedOutputSwitch == 1) // 打开DelayedOutput
                    {
                        ConfigureOutDelayAmplitude(delayAmplitude);
                        ConfigureOutDelayCoupling(delayCoupling); // 0=AC, 1=DC

                        if (delayCoupling == 1) // 只有当delayCoupling为DC时，才可以设置DelayOffset及DelayTermination
                        {
                            ConfigureOutDelayOffset(delayOffset);
                            ConfigureOutDelayTermination(delayTermination);
                        }
                        
                        ConfigureOutDelay(delay);
                        ConfigureDelayedOutputSwitch(delayedOutputSwitch);
                    }
                    else 
                    {
                        ConfigureDelayedOutputSwitch(delayedOutputSwitch);
                    }
                   
                    #endregion

                    #region ConfigureDividedOutput
                    if (dividedOutputSwitch == 1) // 打开DividedOutput 
                    {
                        ConfigureOutSubrateAmplitude(subrateAmplitude);
                        ConfigureOutSubrateCoupling(subrateCoupling);

                        if (subrateCoupling == 1) // 只有当subrateCoupling为DC时，才可以设置SubrateOffset及SubrateTermination
                        {
                            ConfigureOutSubrateOffset(subrateOffset);
                            ConfigureOutSubrateTermination(subrateTermination);
                        }
                       
                        ConfigureOutSubrateDivider(subrateDivider);
                        ConfigureDividedOutputSwitch(dividedOutputSwitch);
                    }
                    else
                    {
                        ConfigureDividedOutputSwitch(dividedOutputSwitch);
                    }
                    
                    #endregion

                    #region ConfigurePattern
                    ConfigurePattern(dataPattern);
                    ConfigurePatternInvert(invert);
                    #endregion

                    #region ConfigureDataOutput
                    if (dataLogicLevel == 3) // 3=CUST, the logic level is "CUSTom"
                    {
                        ConfigureDataLogicLevel(dataLogicLevel);
                        ConfigureDataAmplitude(amplitude);
                        ConfigureDataAtten(dataAtten);
                        ConfigureDataOffset(dataOffset);
                        ConfigureDataTermination(dataTermination);
                        ConfigureDataCrossover(dataCrossover);
                        ConfigureDataRate(Convert.ToDouble(dataRate));
                    }
                    else
                    {
                        ConfigureDataLogicLevel(dataLogicLevel);
                    }

                    ConfigureDataOutputSwitch(dataSwitch);
                    #endregion

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
                        MyIO = new IOPort(IOType, "GPIB::" + Addr.ToString(), logger);

                        if (MyIO.IOConnect()) // 判断仪器是否连接成功
                        {
                            string strIDN = "";
                            MyIO.WriteString("*IDN?"); // 读仪器标识
                            strIDN = MyIO.ReadString();

                            if (strIDN.Contains("N4960A")) // 读仪器标识，再次确认仪器是否已经连接成功
                            {
                                EquipmentConnectflag = true;
                            }
                            else
                            {
                                EquipmentConnectflag = false;
                            }
                        }
                        else
                        {
                            EquipmentConnectflag = false;
                        }
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

        #region 私有方法
        /// <summary>
        /// Configure channel for N4960APPG
        /// </summary>
        /// <param name="channel">0=channel 0 (Jitter), 1=channel 1 (Delay), 
        /// 2=channel 0 (Jitter) and channel 1 (Delay)</param>
        /// <returns></returns>
        private bool ConfigureChannel(byte channel)
        {
            switch (channel)
            {
                case 0:
                    PPGChannel = "(@0)";
                    PPGChannelInfo = "channel 0 (Jitter).";
                    break;
                case 1:
                    PPGChannel = "(@1)";
                    PPGChannelInfo = "channel 1 (Delay).";
                    break;
                case 2:
                    PPGChannel = "(@0,1)";
                    PPGChannelInfo = "channel 0 (Jitter) and channel 1 (Delay).";
                    break;
                default:
                    PPGChannel = "(@0)";
                    PPGChannelInfo = "channel 0 (Jitter).";
                    break;
            }

            logger.AdapterLogString(0, "N4960APPG channel is " + PPGChannelInfo);
            return true;
        }

        #region DelayedOutput
        /// <summary>
        /// Turn the output delay clock ON or OFF. The default is OFF.
        /// </summary>
        /// <param name="delayedOutputSwitch">0=OFF, 1=ON</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDelayedOutputSwitch(byte delayedOutputSwitch, int syn = 0)
        {
            bool flag = false;
            string strDelayedOutputSwitch;

            switch (delayedOutputSwitch)
            {
                case 0:
                    strDelayedOutputSwitch = "OFF";
                    break;
                case 1:
                    strDelayedOutputSwitch = "ON";
                    break;
                default:
                    strDelayedOutputSwitch = "OFF";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Turn the output delay clock " + strDelayedOutputSwitch);
                    return MyIO.WriteString(":OUTD:OUTP " + strDelayedOutputSwitch);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the amplitude of the output delay logic level from 0.3 V to 1.7 V 
        /// in 0.005 V increments. The optional units are V (default) and mV. 
        /// The default value is 0.7 V.
        /// </summary>
        /// <param name="delayAmplitude">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutDelayAmplitude(double delayAmplitude, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the amplitude of the output delay logic level to "
                        + delayAmplitude + " V");
                    return MyIO.WriteString(":OUTD:AMPL " + delayAmplitude);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the position of the internal switch to AC (default) or DC. 
        /// Offset or termination must be set to 0 V to change coupling to AC 
        /// or a setting conflict error message will be generated. 
        /// If coupling is already set to AC, it can be changed to
        /// DC without any dependencies.
        /// </summary>
        /// <param name="delayCoupling">0=AC, 1=DC</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutDelayCoupling(byte delayCoupling, int syn = 0)
        {
            bool flag = false;
            string strDelayCoupling;

            switch (delayCoupling)
            {
                case 0:
                    strDelayCoupling = "AC";
                    break;
                case 1:
                    strDelayCoupling = "DC";
                    break;
                default:
                    strDelayCoupling = "AC";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the position of the internal switch to " + strDelayCoupling);

                    if (delayCoupling == 0)
                    {
                        // 需要设置delayCoupling为AC时，必须首先设置DelayOffset
                        // 及DelayTermination都为0，才能设置delayCoupling为AC，否则设置无效
                        MyIO.WriteString(":OUTD:OFFS 0");
                        MyIO.WriteString(":OUTD:TERM 0");
                    }

                    return MyIO.WriteString(":OUTD:COUP " + strDelayCoupling);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the offset voltage of the output delay logic level 
        /// from –2.4 V to +2.4 V in 0.005 V increments. The optional units
        /// are V (default) and mV. The default value/unit is 0 V. 
        /// If the desired offset voltage is incompatible with the current 
        /// termination voltage, a setting conflict error message will be 
        /// generated and the offset cannot be set. If the offset is changed to
        /// a value other than 0 V while the coupling is set to AC, 
        /// a setting conflict error message will be generated and the offset 
        /// cannot be set. If the coupling is set to DC, then offset can be changed.
        /// </summary>
        /// <param name="delayOffset">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutDelayOffset(double delayOffset, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the offset voltage of the output delay logic level to "
                        + delayOffset + " V");
                    return MyIO.WriteString(":OUTD:OFFS " + delayOffset);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the termination voltage of the output delay logic level 
        /// from –2.4 V to +2.4 V in 0.005 V increments. The optional units 
        /// are V (default) and mV. The default value/unit is 0 V. 
        /// If the desired termination voltage is incompatible with 
        /// the current offset voltage, a setting conflict error message
        /// will be generated and the termination cannot be set. 
        /// If the termination is changed to a value other than 0 V 
        /// while the coupling is set to AC, a setting conflict error message
        /// will be generated and the termination cannot be set. 
        /// If the coupling is set to DC, then termination can be changed.
        /// </summary>
        /// <param name="delayTermination">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutDelayTermination(double delayTermination, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the termination voltage of the output delay logic level to "
                        + delayTermination + " V");
                    return MyIO.WriteString(":OUTD:TERM " + delayTermination);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the precision delay value of the clock output from -1000 UI to 1000 UI
        /// in 0.001 UI increments. The optional units are UI (Unit Interval) 
        /// and mUI (milli Unit Interval). The default value/unit is 0 UI.
        /// </summary>
        /// <param name="delay">单位为UI(Unit Interval)</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutDelay(double delay, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the precision delay value of the clock output to "
                        + delay + " UI");

                    // Attention: It will take approximately a longer time to to set the delay.
                    // The user should use "*OPC?" or "*WAI" after the SCPI command or
                    // "*OPC" followed by "*ESR?" for this commands.
                    return MyIO.WriteString(":OUTD:DEL " + delay + ";*OPC?"); 
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion

        #region DividedOutput
        /// <summary>
        /// Turn the divided clock output ON or OFF. The default is OFF. 
        /// </summary>
        /// <param name="dividedOutputSwitch">0=OFF, 1=ON</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDividedOutputSwitch(byte dividedOutputSwitch, int syn = 0)
        {
            bool flag = false;
            string strDividedOutputSwitch;

            switch (dividedOutputSwitch)
            {
                case 0:
                    strDividedOutputSwitch = "OFF";
                    break;
                case 1:
                    strDividedOutputSwitch = "ON";
                    break;
                default:
                    strDividedOutputSwitch = "OFF";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Turn the divided clock output to " + strDividedOutputSwitch);
                    return MyIO.WriteString(":OUTS:OUTP " + strDividedOutputSwitch);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the amplitude of the divided clock output logic level 
        /// from 0.3 V to 1.7 V in 0.005 V increments. The optional units are V (default) 
        /// and mV. The default value/unit is 0.7 V.
        /// </summary>
        /// <param name="subrateAmplitude">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutSubrateAmplitude(double subrateAmplitude, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the amplitude of the divided clock output logic level to "
                        + subrateAmplitude + " V");
                    return MyIO.WriteString(":OUTS:AMPL " + subrateAmplitude);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the position of the internal switch to AC (default) or DC. 
        /// Offset or termination must be set to 0 V to change coupling to AC 
        /// or a setting conflict error message will be generated. If coupling 
        /// is already set to AC, it can be changed to DC without any dependencies.
        /// </summary>
        /// <param name="subrateCoupling">0=AC, 1=DC</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutSubrateCoupling(byte subrateCoupling, int syn = 0)
        {
            bool flag = false;
            string strSubrateCoupling;

            switch (subrateCoupling)
            {
                case 0:
                    strSubrateCoupling = "AC";
                    break;
                case 1:
                    strSubrateCoupling = "DC";
                    break;
                default:
                    strSubrateCoupling = "AC";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the position of the internal switch to " 
                        + strSubrateCoupling);

                    if (subrateCoupling == 0)
                    {
                        // 需要设置subrateCoupling为AC时，必须首先设置SubrateOffset
                        // 及SubrateTermination都为0，才能设置subrateCoupling为AC，否则设置无效
                        MyIO.WriteString(":OUTS:OFFS 0");
                        MyIO.WriteString(":OUTS:TERM 0");
                    }

                    return MyIO.WriteString(":OUTS:COUP " + strSubrateCoupling);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the offset voltage of the divided clock output logic level 
        /// from –2.4 V to +2.4 V in 0.005 V increments. The optional units
        /// are V (default) and mV. The default value/unit is 0 V. 
        /// If the desired offset voltage is incompatible with the current 
        /// termination voltage, a setting conflict error message will be generated
        /// and the offset cannot be set. If the offset is changed to a value 
        /// other than 0 V while the coupling is set to AC, a setting conflict 
        /// error message will be generated and the offset cannot be set. 
        /// If the coupling is set to DC, then offset can be changed.
        /// </summary>
        /// <param name="subrateOffset">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutSubrateOffset(double subrateOffset, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the offset voltage of the divided clock output logic level to "
                        + subrateOffset + " V");
                    return MyIO.WriteString(":OUTS:OFFS " + subrateOffset);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the termination voltage of the divided clock output logic level
        /// from –2.4 V to +2.4 V in 0.005 V increments. The optional units
        /// are V (default) and mV. The default value/unit is 0 V. 
        /// If the desired termination voltage is incompatible with 
        /// the current offset voltage, a setting conflict error message will
        /// be generated and the termination cannot be set. If the termination
        /// is changed to a value other than 0 V while the coupling is set to AC,
        /// a setting conflict error message will be generated and the termination
        /// cannot be set. If the coupling is set to DC, then termination can be changed.
        /// </summary>
        /// <param name="subrateTermination">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutSubrateTermination(double subrateTermination, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the termination voltage of the divided clock output logic level to "
                        + subrateTermination + " V");
                    return MyIO.WriteString(":OUTS:TERM " + subrateTermination);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the divider value for the divided clock output from 1 to 99999999 
        /// in increments of 1. The default value is 4. 
        /// </summary>
        /// <param name="subrateDivider">from 1 to 99999999 in increments of 1</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureOutSubrateDivider(int subrateDivider, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the divider value for the divided clock output to "
                        + subrateDivider);
                    return MyIO.WriteString(":OUTS:DIV " + subrateDivider);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion

        #region DataOutput
        /// <summary>
        /// Turn the pattern generator data outputs ON or OFF.
        /// The default is OFF upon cycling the power, 
        /// executing a *RST command, or performing an instrument preset.
        /// </summary>
        /// <param name="dataSwitch">0=OFF, 1=ON</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataOutputSwitch(byte dataSwitch, int syn = 0)
        {
            bool flag = false;
            string strDataSwitch;

            switch (dataSwitch)
            {
                case 0:
                    strDataSwitch = "OFF";
                    break;
                case 1:
                    strDataSwitch = "ON";
                    break;
                default:
                    strDataSwitch = "OFF";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the data outputs to " + strDataSwitch 
                        + " for the pattern generators connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:OUTP " + strDataSwitch + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Adjust the amplitude of the data eye presented at the data outputs
        /// from 0.1 V to 1 V (N4951A) or 0.3 V to 3.0 V (N4951B) in 0.005 V increments.
        /// This is the single ended amplitude. The units are V and mV. 
        /// The default value/unit is 0.5 V.
        /// </summary>
        /// <param name="amplitude">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataAmplitude(double amplitude, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the amplitude to " + amplitude
                        + " V for the pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:LLEV:AMPL " + amplitude + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataAtten"></param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataAtten(double dataAtten, int syn = 0)
        {
            return true;
        }
        /// <summary>
        /// Set the data logic level. Selecting a logic level defines a commonly
        /// understood output amplitude, offset, and termination voltage. 
        /// Editing any parameter will automatically change the logic level to “CUSTom”.
        /// The default is CUSTom.
        /// </summary>
        /// <param name="dataLogicLevel">0=LVPECL,1=LVDS,2=ECL,3=CUST</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataLogicLevel(byte dataLogicLevel, int syn = 0)
        {
            bool flag = false;
            string strDataLogicLevel;

            switch (dataLogicLevel)
            {
                case 0:
                    strDataLogicLevel = "LVPECL";
                    break;
                case 1:
                    strDataLogicLevel = "LVDS";
                    break;
                case 2:
                    strDataLogicLevel = "ECL";
                    break;
                case 3:
                    strDataLogicLevel = "CUST";
                    break;
                default:
                    strDataLogicLevel = "CUST";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the logic level to " + strDataLogicLevel
                        + " on pattern generators connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:LLEV " + strDataLogicLevel + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Adjust the DC offset of the data eye presented at the data outputs
        /// from –2.0 V to 2.0 V in 0.005 V increments. The units are V and mV.
        /// The default value/unit is 0.0 V.
        /// </summary>
        /// <param name="dataOffset">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataOffset(double dataOffset, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the DC offset to " + dataOffset 
                        + " V for the pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:LLEV:OFFS " + dataOffset + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the DC output offset of the data eye to support a specified
        /// termination voltage of a DUT having a 50 Ω input port.
        /// The range is –2.0 V to 2.0 V in 0.005 V increments.
        /// The units are V and mV. The default value/unit is 0.0 V.
        /// </summary>
        /// <param name="dataTermination">单位为V</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataTermination(double dataTermination, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the termination voltage to " + dataTermination
                        + " V for the pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:LLEV:TERM " + dataTermination + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the data crossover from 25% to 75% in 1% increments (integers only).
        /// The default is 50%.
        /// </summary>
        /// <param name="dataCrossover">integers only</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataCrossover(int dataCrossover, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the crossover to " + dataCrossover
                        + "% for the pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:XOV " + dataCrossover + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the internal clock frequency from 1 to 16 GHz (without remote heads connected)
        /// in 1 kHz increments. The optional units are Hz, kHz, MHz, and GHz. 
        /// The default value/unit is 5 GHz.
        /// </summary>
        /// <param name="dataRate">单位为GHz</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureDataRate(double dataRate, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the internal clock frequency to " + dataRate + " GHz");
                    // Attention: It will take approximately a longer time to to set the internal clock frequency.
                    // The user should use "*OPC?" or "*WAI" after the SCPI command or
                    // "*OPC" followed by "*ESR?" for this commands.
                    return MyIO.WriteString(":SOUR:FREQ " + dataRate + ";*OPC?");
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion

        #region Pattern
        /// <summary>
        /// Select the data pattern name. The default is PRBS2^7-1. 
        /// The list of PRBS and factory patterns are as follows:
        /// PRBS2^7-1, PRBS2^9-1, PRBS2^10-1, PRBS2^11-1, PRBS2^15-1,
        /// PRBS2^23-1, PRBS2^29-1, PRBS2^31-1, PRBS2^33-1, PRBS2^35-1,
        /// PRBS2^39-1, PRBS2^41-1, PRBS2^45-1, PRBS2^47-1, PRBS2^49-1, 
        /// PRBS2^51-1, 10.FPT, 1100.FPT, 111000.FPT, 11110000.FPT, 81s_80s.FPT,
        /// 161_160.FPT, 321_320.FPT, 641_640.FPT, CJPAT.FPT, CJTPAT.FPT,
        /// CRPAT.FPT, JSPAT.FPT, JTSPAT.FPT, K28-3.FPT, K28-5.FPT,K28-7.FPT.
        /// </summary>
        /// <param name="dataPattern">0=PRBS2^7-1, 1=PRBS2^9-1, 2=PRBS2^10-1, 
        /// 3=PRBS2^11-1, 4=PRBS2^15-1, 5=PRBS2^23-1, 6=PRBS2^29-1, 7=PRBS2^31-1, 
        /// 8=PRBS2^33-1, 9=PRBS2^35-1, 10=PRBS2^39-1, 11=PRBS2^41-1, 12=PRBS2^45-1,
        /// 13=PRBS2^47-1, 14=PRBS2^49-1, 15=PRBS2^51-1, 16=10.FPT, 17=1100.FPT,
        /// 18=111000.FPT, 19=11110000.FPT, 20=81s_80s.FPT, 21=161_160.FPT,
        /// 22=321_320.FPT, 23=641_640.FPT, 24=CJPAT.FPT, 25=CJTPAT.FPT,
        /// 26=CRPAT.FPT, 27=JSPAT.FPT, 28=JTSPAT.FPT, 29=K28-3.FPT, 
        /// 30=K28-5.FPT,31=K28-7.FPT.</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigurePattern(byte dataPattern, int syn = 0)
        {
            bool flag = false;
            string strDataPattern;

            #region Select the data pattern name
            switch (dataPattern)
            {
                case 0:
                    strDataPattern = "PRBS2^7-1";
                    break;
                case 1:
                    strDataPattern = "PRBS2^9-1";
                    break;
                case 2:
                    strDataPattern = "PRBS2^10-1";
                    break;
                case 3:
                    strDataPattern = "PRBS2^11-1";
                    break;
                case 4:
                    strDataPattern = "PRBS2^15-1";
                    break;
                case 5:
                    strDataPattern = "PRBS2^23-1";
                    break;
                case 6:
                    strDataPattern = "PRBS2^29-1";
                    break;
                case 7:
                    strDataPattern = "PRBS2^31-1";
                    break;
                case 8:
                    strDataPattern = "PRBS2^33-1";
                    break;
                case 9:
                    strDataPattern = "PRBS2^35-1";
                    break;
                case 10:
                    strDataPattern = "PRBS2^39-1";
                    break;
                case 11:
                    strDataPattern = "PRBS2^41-1";
                    break;
                case 12:
                    strDataPattern = "PRBS2^45-1";
                    break;
                case 13:
                    strDataPattern = "PRBS2^47-1";
                    break;
                case 14:
                    strDataPattern = "PRBS2^49-1";
                    break;
                case 15:
                    strDataPattern = "PRBS2^51-1";
                    break;
                case 16:
                    strDataPattern = "10.FPT";
                    break;
                case 17:
                    strDataPattern = "1100.FPT";
                    break;
                case 18:
                    strDataPattern = "111000.FPT";
                    break;
                case 19:
                    strDataPattern = "11110000.FPT";
                    break;
                case 20:
                    strDataPattern = "81s_80s.FPT";
                    break;
                case 21:
                    strDataPattern = "161_160.FPT";
                    break;
                case 22:
                    strDataPattern = "321_320.FPT";
                    break;
                case 23:
                    strDataPattern = "641_640.FPT";
                    break;
                case 24:
                    strDataPattern = "CJPAT.FPT";
                    break;
                case 25:
                    strDataPattern = "CJTPAT.FPT";
                    break;
                case 26:
                    strDataPattern = "CRPAT.FPT";
                    break;
                case 27:
                    strDataPattern = "JSPAT.FPT";
                    break;
                case 28:
                    strDataPattern = "JTSPAT.FPT";
                    break;
                case 29:
                    strDataPattern = "K28-3.FPT";
                    break;
                case 30:
                    strDataPattern = "K28-5.FPT";
                    break;
                case 31:
                    strDataPattern = "K28-7.FPT";
                    break;
                default:
                    strDataPattern = "PRBS2^7-1";
                    break;
            }
            #endregion

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Selects the pattern name " + strDataPattern 
                        + " for the pattern generator connected to " + PPGChannelInfo);
                    // Attention: It will take approximately a longer time to to select the data pattern name.
                    // The user should use "*OPC?" or "*WAI" after the SCPI command or
                    // "*OPC" followed by "*ESR?" for this commands.
                    return MyIO.WriteString(":PG:DATA:PATT:NAME " + strDataPattern + PPGChannel + ";*OPC?");
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the data pattern polarity to INVert, to invert the data pattern
        /// presented at the data outputs, or NONInvert. The default is NONInvert.
        /// </summary>
        /// <param name="invert">0=INVert, 1=NONInvert</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigurePatternInvert(byte invert, int syn = 0)
        {
            bool flag = false;
            string strInvert;

            switch (invert)
            {
                case 0:
                    strInvert = "INVert";
                    break;
                case 1:
                    strInvert = "NONInvert";
                    break;
                default:
                    strInvert = "NONInvert";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the pattern polarity to " + strInvert + " for the pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:PATT:POL " + strInvert + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion

        #region ErrorInjection(TBD)
        /// <summary>
        /// Set the continuous error injection ON or OFF. Single errors can be
        /// injected even when this command is set to OFF. The rate is set using
        /// the :PG:DATA:PATTern:ERRInjection:RATE command.
        /// </summary>
        /// <param name="errInjectSwitch">0=OFF, 1=ON</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureErrorInjectionSwitch(byte errInjectSwitch, int syn = 0)
        {
            bool flag = false;
            string strErrInjectSwitch;

            switch (errInjectSwitch)
            {
                case 0:
                    strErrInjectSwitch = "OFF";
                    break;
                case 1:
                    strErrInjectSwitch = "ON";
                    break;
                default:
                    strErrInjectSwitch = "OFF";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Sets the error injection to " + strErrInjectSwitch 
                        + " for the pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:PATT:ERRI " + strErrInjectSwitch + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Inject a single error. A single error can be injected 
        /// regardless of whether error injection is on or off.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureSingleErrorInjection(int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Injects a single error from the " 
                    + "pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:PATT:ERRI:SING" + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the error injection rate. The error injection rates are as follows:
        /// E-3
        /// E-4
        /// E-5
        /// E-6
        /// E-7
        /// E-8
        /// E-9
        /// The default is E-3 (at least 1 error bit in 1000 bits).
        /// </summary>
        /// <param name="errInjectRate">0=E-3, 1=E-4, 2=E-5, 3=E-6,
        /// 4=E-7, 5=E-8, 6=E-9</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureErrorInjectionRate(byte errInjectRate, int syn = 0)
        {
            bool flag = false;
            string strErrInjectRate;

            # region Set the error injection rate
            switch (errInjectRate)
            {
                case 0:
                    strErrInjectRate = "E-3";
                    break;
                case 1:
                    strErrInjectRate = "E-4";
                    break;
                case 2:
                    strErrInjectRate = "E-5";
                    break;
                case 3:
                    strErrInjectRate = "E-6";
                    break;
                case 4:
                    strErrInjectRate = "E-7";
                    break;
                case 5:
                    strErrInjectRate = "E-8";
                    break;
                case 6:
                    strErrInjectRate = "E-9";
                    break;
                default:
                    strErrInjectRate = "E-3";
                    break;
            }
            #endregion

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the error injection rate " + strErrInjectRate 
                        + " for the pattern generator connected to " + PPGChannelInfo);
                    return MyIO.WriteString(":PG:DATA:PATT:ERRI:RATE " + strErrInjectRate + PPGChannel);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion
        #endregion
    }

    public class N4960AED : ErrorDetector
    {
        #region N4960AED
        public string instantBER;
        public string berAccumulatedResult;
        public string bitCount;
        public string errorCount;
        public string errorOnes;
        public string errorZeros;
        public string elapsedTime;
        public string statusOfSamplingDelay;
        public string dataRate;

        public byte polarity; // 0=INVert, 1=NONInvert
        public byte stopCriteriaMode; // 0=MANual,1=DURation,2=BITS,3=ERRors
        public string stopDuration; // 单位为s
        public string stopBitCount;
        public string stopErrorCount;
        public byte dataPattern;
        public byte autoAlign;
        #endregion

        public Algorithm algorithm = new Algorithm();

        public N4960AED(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] N4960AEDStruct)
        {
            int i = 0;

            if (algorithm.FindFileName(N4960AEDStruct, "ADDR", out i))
            {
                Addr = N4960AEDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no ADDR");
                return false;
            }

            if (algorithm.FindFileName(N4960AEDStruct, "IOTYPE", out i))
            {
                IOType = N4960AEDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }

            if (algorithm.FindFileName(N4960AEDStruct, "RESET", out i))
            {
                Reset = Convert.ToBoolean(N4960AEDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }

            if (algorithm.FindFileName(N4960AEDStruct, "NAME", out i))
            {
                Name = N4960AEDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }

            if (algorithm.FindFileName(N4960AEDStruct, "stopCriteriaMode".ToUpper(), out i))
            {
                stopCriteriaMode = Convert.ToByte(N4960AEDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no stopCriteriaMode");
                return false;
            }

            if (algorithm.FindFileName(N4960AEDStruct, "polarity".ToUpper(), out i))
            {
                polarity = Convert.ToByte(N4960AEDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no polarity");
                return false;
            }

            if (algorithm.FindFileName(N4960AEDStruct, "autoAlign".ToUpper(), out i))
            {
                autoAlign = Convert.ToByte(N4960AEDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no autoAlign");
                return false;
            }

            if (algorithm.FindFileName(N4960AEDStruct, "dataPattern".ToUpper(), out i))
            {
                dataPattern = Convert.ToByte(N4960AEDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no dataPattern");
                return false;
            }

            if (!Connect())
            {
                return false;
            }

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

                    ConfigurePattern(dataPattern);
                    ConfigurePatternPolarity(polarity);
                    ConfigureAutoAlignment();
                    ConfigureStopCriteriaMode(stopCriteriaMode);

                    switch (stopCriteriaMode)
                    {
                        case 0:
                            break;
                        case 1:
                            ConfigureStopCriteriaDuration(stopDuration);
                            break;
                        case 2:
                            ConfigureStopCriteriaBitCount(stopBitCount);
                            break;
                        case 3:
                            ConfigureStopCriteriaErrorCount(stopErrorCount);
                            break;
                        default:
                            break;
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
        public override bool Connect()
        {
            try
            {
                // IO_Type
                switch (IOType)
                {
                    case "GPIB":
                        MyIO = new IOPort(IOType, "GPIB::" + Addr.ToString(), logger);

                        if (MyIO.IOConnect()) // 判断仪器是否连接成功
                        {
                            string strIDN = "";
                            MyIO.WriteString("*IDN?"); // 读仪器标识
                            strIDN = MyIO.ReadString();

                            if (strIDN.Contains("N4960A")) // 读仪器标识，再次确认仪器是否已经连接成功
                            {
                                EquipmentConnectflag = true;
                            }
                            else
                            {
                                EquipmentConnectflag = false;
                            }
                        }
                        else
                        {
                            EquipmentConnectflag = false;
                        }
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

        #region 私有方法
        #region InputAdjust
        #region Pattern
        /// <summary>
        /// Select the data pattern name. The default is PRBS7. The list of PRBS
        /// and factory patterns are as follows: PRBS2^7-1, PRBS2^9-1, PRBS2^10-1,
        /// PRBS2^11-1, PRBS2^15-1, PRBS2^23-1, PRBS2^29-1, PRBS2^31-1, PRBS2^33-1,
        /// PRBS2^35-1, PRBS2^39-1, PRBS2^41-1, PRBS2^45-1, PRBS2^47-1, PRBS2^49-1, 
        /// PRBS2^51-1, 10.FPT, 1100.FPT, 111000.FPT, 11110000.FPT, 81s_80s.FPT, 
        /// 161_160.FPT, 321_320.FPT, 641_640.FPT, CJPAT.FPT, CJTPAT.FPT, CRPAT.FPT,
        /// JSPAT.FPT, JTSPAT.FPT, K28-3.FPT, K28-5.FPT, K28-7.FPT </summary>
        /// <param name="dataPattern">0=PRBS2^7-1, 1=PRBS2^9-1, 2=PRBS2^10-1, 
        /// 3=PRBS2^11-1, 4=PRBS2^15-1, 5=PRBS2^23-1, 6=PRBS2^29-1, 7=PRBS2^31-1, 
        /// 8=PRBS2^33-1, 9=PRBS2^35-1, 10=PRBS2^39-1, 11=PRBS2^41-1, 12=PRBS2^45-1,
        /// 13=PRBS2^47-1, 14=PRBS2^49-1, 15=PRBS2^51-1, 16=10.FPT, 17=1100.FPT,
        /// 18=111000.FPT, 19=11110000.FPT, 20=81s_80s.FPT, 21=161_160.FPT,
        /// 22=321_320.FPT, 23=641_640.FPT, 24=CJPAT.FPT, 25=CJTPAT.FPT,
        /// 26=CRPAT.FPT, 27=JSPAT.FPT, 28=JTSPAT.FPT, 29=K28-3.FPT, 
        /// 30=K28-5.FPT,31=K28-7.FPT.</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigurePattern(byte dataPattern, int syn = 0)
        {
            bool flag = false;
            string strDataPattern;

            #region Select the data pattern name
            switch (dataPattern)
            {
                case 0:
                    strDataPattern = "PRBS2^7-1";
                    break;
                case 1:
                    strDataPattern = "PRBS2^9-1";
                    break;
                case 2:
                    strDataPattern = "PRBS2^10-1";
                    break;
                case 3:
                    strDataPattern = "PRBS2^11-1";
                    break;
                case 4:
                    strDataPattern = "PRBS2^15-1";
                    break;
                case 5:
                    strDataPattern = "PRBS2^23-1";
                    break;
                case 6:
                    strDataPattern = "PRBS2^29-1";
                    break;
                case 7:
                    strDataPattern = "PRBS2^31-1";
                    break;
                case 8:
                    strDataPattern = "PRBS2^33-1";
                    break;
                case 9:
                    strDataPattern = "PRBS2^35-1";
                    break;
                case 10:
                    strDataPattern = "PRBS2^39-1";
                    break;
                case 11:
                    strDataPattern = "PRBS2^41-1";
                    break;
                case 12:
                    strDataPattern = "PRBS2^45-1";
                    break;
                case 13:
                    strDataPattern = "PRBS2^47-1";
                    break;
                case 14:
                    strDataPattern = "PRBS2^49-1";
                    break;
                case 15:
                    strDataPattern = "PRBS2^51-1";
                    break;
                case 16:
                    strDataPattern = "10.FPT";
                    break;
                case 17:
                    strDataPattern = "1100.FPT";
                    break;
                case 18:
                    strDataPattern = "111000.FPT";
                    break;
                case 19:
                    strDataPattern = "11110000.FPT";
                    break;
                case 20:
                    strDataPattern = "81s_80s.FPT";
                    break;
                case 21:
                    strDataPattern = "161_160.FPT";
                    break;
                case 22:
                    strDataPattern = "321_320.FPT";
                    break;
                case 23:
                    strDataPattern = "641_640.FPT";
                    break;
                case 24:
                    strDataPattern = "CJPAT.FPT";
                    break;
                case 25:
                    strDataPattern = "CJTPAT.FPT";
                    break;
                case 26:
                    strDataPattern = "CRPAT.FPT";
                    break;
                case 27:
                    strDataPattern = "JSPAT.FPT";
                    break;
                case 28:
                    strDataPattern = "JTSPAT.FPT";
                    break;
                case 29:
                    strDataPattern = "K28-3.FPT";
                    break;
                case 30:
                    strDataPattern = "K28-5.FPT";
                    break;
                case 31:
                    strDataPattern = "K28-7.FPT";
                    break;
                default:
                    strDataPattern = "PRBS2^7-1";
                    break;
            }
            #endregion

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Selects the data pattern name " + strDataPattern
                        + " for the N4960ED");
                    // Attention: It will take approximately a longer time to to select the data pattern name.
                    // The user should use "*OPC?" or "*WAI" after the SCPI command or
                    // "*OPC" followed by "*ESR?" for this commands.
                    return MyIO.WriteString(":ED:DATA:PATTern:NAME " + strDataPattern + ";*OPC?");
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Set the data pattern polarity to INVert, to invert the data pattern 
        /// presented at the data outputs, or NONInvert. The default is NONInvert.
        /// </summary>
        /// <param name="polarity">0=INVert, 1=NONInvert</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigurePatternPolarity(byte polarity, int syn = 0)
        {
            bool flag = false;
            string strPolarity;

            switch (polarity)
            {
                case 0:
                    strPolarity = "INVert";
                    break;
                case 1:
                    strPolarity = "NONInvert";
                    break;
                default:
                    strPolarity = "NONInvert";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the data pattern polarity to " + strPolarity);
                    return MyIO.WriteString(":ED:DATA:PATT:POL " + strPolarity);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion

        #region Alignment
        /// <summary>
        /// Initiate an auto alignment. During an auto alignment the optimal 
        /// sampling delay and threshold voltage are found.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureAutoAlignment(int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Initiate an auto alignment.");
                    // Attention: It will take approximately a longer time to initiate an auto alignment.
                    // The user should use "*OPC?" or "*WAI" after the SCPI command or
                    // "*OPC" followed by "*ESR?" for this commands.
                    return MyIO.WriteString(":ED:DATA:AAL:EXEC" + ";*OPC?");
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion
        #endregion

        #region Measurement
        /// <summary>
        /// Return the instantaneous BER. The instantaneous BER is continuously calculated.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private double GetInstantBER(int syn = 0)
        {
            try
            {
                logger.AdapterLogString(0, "Query the instantaneous BER");
                MyIO.WriteString(":ED:DATA:INPut:IBER?");
                instantBER = MyIO.ReadString();
                logger.AdapterLogString(0, "The instantaneous BER is " + instantBER);

                return Convert.ToDouble(instantBER);
            }
            catch (Exception ex)
            {
                instantBER = "0.0";
                logger.AdapterLogString(3, ex.ToString());
                return Convert.ToDouble(instantBER);
            }
        }

        #region Accumulated
        /// <summary>
        /// Start an accumulated BER measurement.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool MeasurementRun(int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Start an accumulated BER measurement.");
                    return MyIO.WriteString(":ED:DATA:ACC:STAR");
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Stop an accumulated BER measurement. This command can be issued
        /// regardless of the accumulation stop criteria.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool MeasurementStop(int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Stop an accumulated BER measurement.");
                    return MyIO.WriteString(":ED:DATA:ACC:STOP");
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Return the accumulated BER results.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private double GetBERAccumulatedResult(int syn = 0)
        {
            try
            {
                logger.AdapterLogString(0, "Query the accumulated BER results.");
                MyIO.WriteString(":ED:DATA:ACC:RES:ABER?");
                berAccumulatedResult = MyIO.ReadString();
                logger.AdapterLogString(0, "The accumulated BER results is " + berAccumulatedResult);

                return Convert.ToDouble(berAccumulatedResult);
            }
            catch (Exception ex)
            {
                berAccumulatedResult = "0.0";
                logger.AdapterLogString(3, ex.ToString());
                return Convert.ToDouble(berAccumulatedResult);
            }
        }
        /// <summary>
        /// Return the total accumulated number of bits.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private double GetBitCount(int syn = 0)
        {
            try
            {
                logger.AdapterLogString(0, "Query the total accumulated number of bits.");
                MyIO.WriteString(":ED:DATA:ACC:RES:BITS?");
                bitCount = MyIO.ReadString();
                logger.AdapterLogString(0, "The total accumulated number of bits is " + bitCount);

                return Convert.ToDouble(bitCount);
            }
            catch (Exception ex)
            {
                bitCount = "0.0";
                logger.AdapterLogString(3, ex.ToString());
                return Convert.ToDouble(bitCount);
            }
        }
        /// <summary>
        /// Return the accumulated number of bit errors.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private double GetErrorCount(int syn = 0)
        {
            try
            {
                logger.AdapterLogString(0, "Query the accumulated number of bit errors.");
                MyIO.WriteString(":ED:DATA:ACC:RES:ERR?");
                errorCount = MyIO.ReadString();
                logger.AdapterLogString(0, "The accumulated number of bit errors is " + errorCount);

                return Convert.ToDouble(errorCount);
            }
            catch (Exception ex)
            {
                errorCount = "0.0";
                logger.AdapterLogString(3, ex.ToString());
                return Convert.ToDouble(errorCount);
            }
        }
        /// <summary>
        /// Return the accumulated number of 1s resulting in bit errors.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private double GetErrorOnes(int syn = 0)
        {
            try
            {
                logger.AdapterLogString(0, "Query the accumulated number of 1s resulting in bit errors.");
                MyIO.WriteString(":ED:DATA:ACC:RES:ERON?");
                errorOnes = MyIO.ReadString();
                logger.AdapterLogString(0, "The accumulated number of 1s resulting in bit errors is " + errorOnes);

                return Convert.ToDouble(errorOnes);
            }
            catch (Exception ex)
            {
                errorOnes = "0.0";
                logger.AdapterLogString(3, ex.ToString());
                return Convert.ToDouble(errorOnes);
            }
        }
        /// <summary>
        /// Return the accumulated number of 0s resulting in bit errors.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private double GetErrorZeros(int syn = 0)
        {
            try
            {
                logger.AdapterLogString(0, "Query the accumulated number of 0s resulting in bit errors.");
                MyIO.WriteString(":ED:DATA:ACC:RES:ERZ?");
                errorZeros = MyIO.ReadString();
                logger.AdapterLogString(0, "The accumulated number of 0s resulting in bit errors is " + errorZeros);

                return Convert.ToDouble(errorZeros);
            }
            catch (Exception ex)
            {
                errorZeros = "0.0";
                logger.AdapterLogString(3, ex.ToString());
                return Convert.ToDouble(errorZeros);
            }
        }
        /// <summary>
        /// Return the elapsed time since the BER measurement was started.
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        private string GetElapsedTime(int syn = 0)
        {
            try
            {
                logger.AdapterLogString(0, "Query the elapsed time since the BER measurement was started.");
                MyIO.WriteString(":ED:DATA:ACC:RES:ELAP?");
                elapsedTime = MyIO.ReadString();
                logger.AdapterLogString(0, "The elapsed time since the BER measurement was started is " + elapsedTime);

                return elapsedTime;
            }
            catch (Exception ex)
            {
                elapsedTime = "0000:00:00:00";
                logger.AdapterLogString(3, ex.ToString());
                return elapsedTime;
            }
        }
        #endregion

        #region StopCriteria
        /// <summary>
        /// Set the stop criteria mode used to define the duration of the measurement.
        /// The options are as follows:
        /// MANual (infinite duration)
        /// DURation (duration as set by the :ED:DATA:ACCumulation:SCONfig:DURation command)
        /// BITS (duration determined by total number of accumulated bits as set by the :ED:DATA:ACCumulation:SCONfig:BITS command)
        /// ERRors (duration determined by total number of accumulated errors as set by the :ED:DATA:ACCumulation:SCONfig:ERRors command)
        /// The default is MANual.
        /// </summary>
        /// <param name="mode">0=MANual,1=DURation,2=BITS,3=ERRors</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureStopCriteriaMode(byte mode, int syn = 0)
        {
            bool flag = false;
            string strMode = "";

            switch (mode)
            {
                case 0:
                    strMode = "MANual";
                    break;
                case 1:
                    strMode = "DURation";
                    break;
                case 2:
                    strMode = "BITS";
                    break;
                case 3:
                    strMode = "ERRors";
                    break;
                default:
                    strMode = "MANual";
                    break;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Set the stop criteria mode to " + strMode);
                    return MyIO.WriteString(":ED:DATA:ACC:SCON " + strMode);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// When the stop criteria mode is set to DURation, this command 
        /// defines the duration of the measurement in seconds. The range
        /// is 1 s through 9999999 s in 1 s increments. The default is 10 s.
        /// </summary>
        /// <param name="duration">单位为s</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureStopCriteriaDuration(string duration, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Defines the duration of the measurement in "
                        + duration + "s");
                    return MyIO.WriteString(":ED:DATA:ACC:SCON:DUR " + duration);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// When the stop criteria mode is set to BITS, this command defines 
        /// the duration of the measurement by the number of accumulated bits. 
        /// The range is 1e8 through 1e17. The default is 1e9.
        /// </summary>
        /// <param name="bitCount">The default is 1e9</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureStopCriteriaBitCount(string bitCount, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Defines the duration of the measurement by the number of accumulated bits: " + bitCount);
                    return MyIO.WriteString(":ED:DATA:ACC:SCON:BITS " + bitCount);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// When the stop criteria mode is set to ERRors, this command defines
        /// the duration of the measurement by the number of accumulated error bits.
        /// The range is 1 through 99999999. The default is 1.
        /// </summary>
        /// <param name="errorCount">The default is 1</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool ConfigureStopCriteriaErrorCount(string errorCount, int syn = 0)
        {
            bool flag = false;

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "Defines the duration of the measurement by the number of accumulated error bits: " + errorCount);
                    return MyIO.WriteString(":ED:DATA:ACC:SCON:ERR " + errorCount);
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                return false;
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
