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

namespace ATS_Driver
{
    public class MAP200Atten : Attennuator
    {
        private double currentOffset = 0;
        private string[] AttSlotList; // 衰减器的槽位
        private string[] DeviceChannelList; // 每个通道衰减器模组所使用的Device列表
        private string DeviceChannel; // 每个衰减器模组所使用的Device

        public Algorithm algorithm = new Algorithm();

        public MAP200Atten(logManager logmanager)
        {
            logger = logmanager;
        }

        #region 公共方法
        public override bool Initialize(TestModeEquipmentParameters[] MAP200AttenStruct)
        {
            try
            {
                int i = 0;

                if (algorithm.FindFileName(MAP200AttenStruct,"ADDR",out i))
                {
                    Addr = Convert.ToByte(MAP200AttenStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ADDR");
                    return false;
                }
                    
                if (algorithm.FindFileName(MAP200AttenStruct,"IOTYPE",out i))
                {
                    IOType = MAP200AttenStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no IOTYPE");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(MAP200AttenStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no RESET");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct,"ATTSLOT",out i))
                {
                    AttSlotList = MAP200AttenStruct[i].DefaultValue.Split(',');
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ATTSLOT");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct, "DEVICECHANNEL", out i))
                {
                    DeviceChannelList = MAP200AttenStruct[i].DefaultValue.Split(',');
                }
                else
                {
                    logger.AdapterLogString(4, "there is no DEVICECHANNEL");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct,"ATTVALUE",out i))
                {
                    AttValue = MAP200AttenStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no ATTVALUE");
                    return false;
                }
               
                if (algorithm.FindFileName(MAP200AttenStruct,"TOTALCHANNEL",out i))
                {
                    TotalChannel = MAP200AttenStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no TOTALCHANNEL");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct,"WAVELENGTH",out i))
                {
                    Wavelength = MAP200AttenStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no WAVELENGTH");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct,"NAME",out i))
                {
                    Name = MAP200AttenStruct[i].DefaultValue;
                }
                else
                {
                    logger.AdapterLogString(4, "there is no NAME");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct, "OPENDELAY", out i))
                {
                    opendelay = Convert.ToInt32(MAP200AttenStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no OPENDELAY");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct, "CLOSEDELAY", out i))
                {
                    closedelay = Convert.ToInt32(MAP200AttenStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no CLOSEDELAY");
                    return false;
                }

                if (algorithm.FindFileName(MAP200AttenStruct, "SETATTDELAY", out i))
                {
                    setattdelay = Convert.ToInt32(MAP200AttenStruct[i].DefaultValue);
                }
                else
                {
                    logger.AdapterLogString(4, "there is no SETATTDELAY");
                    return false;
                }

                if (!Connect())
                {
                    return false;
                }
            }
            catch (Error_Message error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
            return true;
        }
        public override  bool Connect()
        {
            try
            {
                switch (IOType)
                {
                    case "GPIB":
                        MyIO = new IOPort(IOType, "GPIB::" + Addr.ToString(), logger);

                        if (MyIO.IOConnect()) // 判断仪器是否连接成功
                        {
                            string strIDN = "";
                            MyIO.WriteString("*IDN?"); // 读仪器标识
                            strIDN = MyIO.ReadString();

                            if (strIDN.Contains("JDSU,MAP-200")) // 读仪器标识，再次确认仪器是否已经连接成功
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
                logger.FlushLogBuffer();
                EquipmentConnectflag = false;
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
            List<string> ch = new List<string>(); // 衰减器槽位
            List<string> Dev = new List<string>(); // 衰减器DevChannel
            try
            {
                if (EquipmentConfigflag)
                {
                    return true;
                }
                else
                {
                    if (Reset == true)
                    {
                        ReSet();
                    }

                    ConfigLegacyModeOff();

                    for (int i = 0; i < AttSlotList.Length; i++)
                    {
                        //if (!(ch.Contains(AttSlotList[i]))&&!(ch.Contains(AttSlotList[i])))
                        //{
                        //    //d
                            Dev.Add(DeviceChannelList[i]);
                            ch.Add(AttSlotList[i]);
                            AttSlot = AttSlotList[i].ToString();
                            CurrentChannel = Convert.ToString(i + 1);
                            DeviceChannel = DeviceChannelList[i].ToString();
                            ConfigWavelength(CurrentChannel, syn);
                            AttnValue(AttValue, 0);
                            OutPutSwitch(true, syn);
                        //}
                    }
                    EquipmentConfigflag = true;
                } 
                
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
       
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            bool flag1, flag2;
            string offset="";
            CurrentChannel = channel;
            try
            {
                //for (int i = 0; i < Convert.ToInt32(TotalChannel); i++)
                ////{
                //    currentOffset = 0;
                //    AttSlot = AttSlotList[Convert.ToInt16(CurrentChannel)-1].ToString();
                //    DeviceChannel = DeviceChannelList[Convert.ToInt16(CurrentChannel) - 1].ToString();
                //    AttnValue(AttValue, syn);
               // }

                AttSlot = AttSlotList[Convert.ToByte(CurrentChannel) - 1].ToString();
                DeviceChannel = DeviceChannelList[Convert.ToByte(CurrentChannel) - 1].ToString();
                AttnValue("-10", 0);

                flag1 = ConfigWavelength(CurrentChannel, syn);

                if (offsetlist.ContainsKey(CurrentChannel))
                {
                    offset = offsetlist[CurrentChannel];
                }

                flag2 = ConfigureOffset("0", syn); // 将所有 Offset设置为0
                if (offset != "")
                {
                    currentOffset = Convert.ToDouble(offset);
                }
                logger.AdapterLogString(0, "Succeed in changing Att current channel to " + CurrentChannel);

                if (flag1 && flag2)
                {
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
                logger.FlushLogBuffer();
                return false;
            }
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            offsetlist.Add(channel, offset);
            return true;
        }
        /// <summary>
        /// 调用该方法前，必须保证仪器基类的CurrentChannel已经正确赋值
        /// </summary>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool ConfigWavelength(int syn=0)
        {
            bool flag = false;
            try
            {
                flag = ConfigWavelength(CurrentChannel, syn);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        private void CalculateSleepTime(double Att)
        {
            if (LastAttChannel.ToString() == CurrentChannel)
            {

                PowerVariation = Math.Abs(Att - LastAttValue);
                SleepTime = Convert.ToInt16( PowerVariation *250);
                if (SleepTime > 10000) SleepTime = 5000;//防止出现错误的入射光，导致衰减器等待太久
            }
            else
            {
                SleepTime = 5000;//防止出现错误的入射光，导致衰减器等待太久
            }
        }
        public override bool AttnValue(string InputPow, int syn = 1) 
        {
            bool flag1 = false;
            double dAttValue = 0;
            try
            {

                dAttValue = currentOffset-Convert.ToDouble(InputPow) ;// 仪器中的Offset将不在添加.在计算中使用.

                dAttValue = Math.Round(dAttValue, 3);
                if (dAttValue < 0)
                {
                    dAttValue = 0;
                    logger.AdapterLogString(3, "Light Source Power Too Smal");

                }
          
                try
                {
                   
                }
                catch
                {
                    SleepTime = 2000;
                }

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                        + DeviceChannel + ", ATT value is " + dAttValue);
                    flag1 = MyIO.WriteString(":OUTP:ATT " + AttSlot + "," + DeviceChannel + "," + dAttValue.ToString());
                    //Thread.Sleep(SleepTime);
                   // Thread.Sleep(setattdelay);
                  
                }
                else
                {
                    bool flag2 = false;
                    int k = 0;
                    string readtemp = "";


                    string Str = "";

                    try
                    {


                        int M = 0;
                        do
                        {

                            // MyIO.WriteString(":OUTP:ATT?" + AttSlot + "," + DeviceChannel);//   MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            Str = MyIO.ReadString();
                            Thread.Sleep(200);
                            M++;
                        } while ((Str == null||Str=="") && M < 3);

                        double TempVale = double.Parse(Str);
                        SleepTime = Convert.ToInt16(Math.Abs(double.Parse(AttValue) - TempVale) * 150);
                    }
                    catch
                    {
                        SleepTime = 2000;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        flag2 = MyIO.WriteString(":OUTP:ATT " + AttSlot + "," + DeviceChannel + "," + dAttValue.ToString());

                        if (flag2)
                        {
                            break;
                        }
                    }

                    if (flag2)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(SleepTime);
                            MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            readtemp = MyIO.ReadString();

                            if (Convert.ToDouble(readtemp) == dAttValue)
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            flag1 = true;
                            logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                                + DeviceChannel + ", ATT value is " + dAttValue.ToString());
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set ATT VALUE wrong");
                        }
                    }

                }
               
               // Thread.Sleep(setattdelay);
                return flag1;
            }
            catch (Exception error)
            {
              
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
          
                return false;
            }
        }
        public override bool  SetInputPow(double InputPower, int syn = 1)

        {
            bool flag1 = false;
            double dAttValue = 0;
            try
            {

                dAttValue = currentOffset - InputPower;// 仪器中的Offset将不在添加.在计算中使用.

                dAttValue = Math.Round(dAttValue, 3);
                if (dAttValue < 0)
                {
                    dAttValue = 0;
                    logger.AdapterLogString(3, "Light Source Power Too Smal");

                }

                try
                {

                }
                catch
                {
                    SleepTime = 2000;
                }

                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                        + DeviceChannel + ", ATT value is " + dAttValue);
                    flag1 = MyIO.WriteString(":OUTP:ATT " + AttSlot + "," + DeviceChannel + "," + dAttValue.ToString());
                    //Thread.Sleep(SleepTime);
                    // Thread.Sleep(setattdelay);

                }
                else
                {
                    bool flag2 = false;
                    int k = 0;
                    string readtemp = "";


                    string Str = "";

                    try
                    {


                        int M = 0;
                        do
                        {

                            // MyIO.WriteString(":OUTP:ATT?" + AttSlot + "," + DeviceChannel);//   MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            Str = MyIO.ReadString();
                            Thread.Sleep(200);
                            M++;
                        } while ((Str == null || Str == "") && M < 3);

                        double TempVale = double.Parse(Str);
                        SleepTime = Convert.ToInt16(Math.Abs(double.Parse(AttValue) - TempVale) * 150);
                    }
                    catch
                    {
                        SleepTime = 2000;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        flag2 = MyIO.WriteString(":OUTP:ATT " + AttSlot + "," + DeviceChannel + "," + dAttValue.ToString());

                        if (flag2)
                        {
                            break;
                        }
                    }

                    if (flag2)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(SleepTime);
                            MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            readtemp = MyIO.ReadString();

                            if (Convert.ToDouble(readtemp) == dAttValue)
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            flag1 = true;
                            logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                                + DeviceChannel + ", ATT value is " + dAttValue.ToString());
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set ATT VALUE wrong");
                        }
                    }

                }

                // Thread.Sleep(setattdelay);
                return flag1;
            }
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        public override bool SetAttnValue(double AttValue, int syn = 1)
        {
            bool flag1 = false;
            
            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                        + DeviceChannel + ", ATT value is " + AttValue);
                    flag1 = MyIO.WriteString(":OUTP:ATT " + AttSlot + "," + DeviceChannel + "," + AttValue.ToString());
                    //Thread.Sleep(SleepTime);
                    // Thread.Sleep(setattdelay);

                }
                else
                {
                    bool flag2 = false;
                    int k = 0;
                    string readtemp = "";


                    string Str = "";

                    try
                    {


                        int M = 0;
                        do
                        {

                            // MyIO.WriteString(":OUTP:ATT?" + AttSlot + "," + DeviceChannel);//   MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            Str = MyIO.ReadString().Replace("\n","");
                            Thread.Sleep(200);
                            M++;
                        } while ((Str == null || Str == "") && M < 3);

                        double TempVale = double.Parse(Str);
                        SleepTime = Convert.ToInt16(Math.Abs(AttValue - TempVale) * 150);
                    }
                    catch
                    {
                        SleepTime = 2000;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        flag2 = MyIO.WriteString(":OUTP:ATT " + AttSlot + "," + DeviceChannel + "," + AttValue.ToString());

                        if (flag2)
                        {
                            break;
                        }
                    }

                    if (flag2)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(SleepTime);
                            MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                            readtemp = MyIO.ReadString().Replace("\n", "");

                            if (Math.Abs(Convert.ToDouble(readtemp) - AttValue)<0.1)
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            flag1 = true;
                            logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                                + DeviceChannel + ", ATT value is " + AttValue.ToString());
                        }
                        else
                        {
                            logger.AdapterLogString(3, "set ATT VALUE wrong");
                        }
                    }

                }

                // Thread.Sleep(setattdelay);
                return flag1;
            }
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }

        public override bool AdjustAttnValue(double AttValue, int syn = 1)
        {
            string Str;
            double TempVale;
            try
            {


                int M = 0;
                do
                {

                    // MyIO.WriteString(":OUTP:ATT?" + AttSlot + "," + DeviceChannel);//   MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                    MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                    Str = MyIO.ReadString();
                    Thread.Sleep(200);
                    M++;
                } while ((Str == null || Str == "") && M < 3);

                 TempVale = double.Parse(Str);
              
            }
            catch
            {
                SleepTime = 2000;
                return false;
            }

            return SetAttnValue((TempVale+AttValue), syn);
        }


        public override bool OutPutSwitch(bool Swith, int syn = 0)
        {
            bool state;

            if (Swith) // 打开，0 - Deactivated; Beam is unblocked. 
            {
                state = false;
            }
            else // 关闭， 1 - Activated; Beam is blocked
            {
                state = true;
            }

            try
            {
                return BeamBlock(state, syn);
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        public override double GetAtten()
        {
            double atten = 0;

            try
            {
                MyIO.WriteString(":OUTP:ATT? " + AttSlot + "," + DeviceChannel);
                atten =Convert.ToDouble(MyIO.ReadString(16));
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return atten;
            }
            return atten;
        }
        #endregion

        #region 私有方法
        private bool ConfigureOffset(string offset, int syn = 0) 
        {
            bool flag = false;

            try
            {
                // offset = Math.Abs((Convert.ToDouble(AttValue) + Convert.ToDouble(offset))).ToString();
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is " 
                        + DeviceChannel + ", offset is " + offset);
                    return MyIO.WriteString("OUTP:ATT:OFFS " + AttSlot + "," + DeviceChannel + "," + offset);
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    double temp = Convert.ToDouble(offset);
                    temp = temp * -1;
                    offset = Convert.ToString(temp);

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString("OUTP:ATT:OFFS " + AttSlot + "," + DeviceChannel + "," + offset);

                        if (tempFlag)
                        {
                            break;
                        }
                    }

                    if (tempFlag)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(100);
                            MyIO.WriteString("OUTP:ATT:OFFS? " + AttSlot + "," + DeviceChannel);

                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble(offset))
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            flag = true;
                            logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                                + DeviceChannel + ", offset is" + offset);
                        }
                        else
                        {
                            logger.AdapterLogString(3, "ATT set offset wrong");
                        }
                    }

                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        private bool ConfigWavelength(string dutCurrentChannel, int syn = 0) 
        {
            bool flag = false;
            string[] wavelengthList = new string[4];
            byte i = Convert.ToByte(Convert.ToInt16(dutCurrentChannel) - 1);

            Wavelength = Wavelength.Trim();
            wavelengthList = Wavelength.Split(',');

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                        + DeviceChannel + ", Wavelength is " + wavelengthList[i] + "nm");
                    return MyIO.WriteString(":OUTP:WAV " + AttSlot + "," + DeviceChannel + "," + wavelengthList[i]);
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int j = 0; j < 3; j++)
                    {
                        tempFlag = MyIO.WriteString(":OUTP:WAV " + AttSlot + "," + DeviceChannel + "," + wavelengthList[i]);

                        if (tempFlag)
                        {
                            break;
                        }
                    }

                    if (tempFlag)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(100);
                            MyIO.WriteString(":OUTP:WAV? " + AttSlot + "," + DeviceChannel);

                            readtemp = MyIO.ReadString();
                            if (Convert.ToDouble(readtemp) == Convert.ToDouble(wavelengthList[i]))
                            {
                                break;
                            }
                        }

                        if (k <= 2)
                        {
                            logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                                + DeviceChannel + ", Wavelength is " + wavelengthList[i] + "nm");
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "ConfigWavelength wrong");
                        }
                    }
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        /// <summary>
        /// Set the beam block to one of two states:
        /// - Beam block activated (beam is blocked). No light is emitted from output port.
        /// - Beam block deactivated (beam is unblocked). Light may be emitted from the output
        /// port (depending on input signal or internal source state).
        /// When system is powered up or reset, the beam block is activated (default state).
        /// </summary>
        /// <param name="state">0 - Deactivated; Beam is unblocked. 
        /// 1 - Activated; Beam is blocked</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        private bool BeamBlock(bool state, int syn = 0)
        {
            bool flag = false;
            int delay = 0;
            string tempSwitch = "";
            string strState;

            if (state) // Beam is blocked
            {
                strState = "Beam is blocked";
                tempSwitch = "1";
                delay = closedelay;
            }
            else // Beam is unblocked
            {
                strState = "Beam is unblocked";
                tempSwitch = "0";
                delay = opendelay;
            }

            try
            {
                if (syn == 0)
                {
                    logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                        + DeviceChannel + ", " + strState);
                    flag = MyIO.WriteString(":OUTP:BBL " + AttSlot + "," + DeviceChannel + "," + tempSwitch);
                    Thread.Sleep(delay);
                    return flag;
                }
                else
                {
                    bool tempFlag = false;
                    string readtemp = "";
                    int k = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        tempFlag = MyIO.WriteString(":OUTP:BBL " + AttSlot + "," + DeviceChannel + "," + tempSwitch);

                        if (tempFlag)
                        {
                            break;
                        }
                    }

                    if (tempFlag)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            Thread.Sleep(delay);
                            MyIO.WriteString(":OUTP:BBL? " + AttSlot + "," + DeviceChannel);

                            readtemp = MyIO.ReadString();
                            if (Convert.ToByte(readtemp) == Convert.ToByte(tempSwitch))
                            {
                                break;
                            } 
                        }

                        if (k <= 2)
                        {
                            logger.AdapterLogString(0, "AttSlot is " + AttSlot + ", DeviceChannel is "
                                + DeviceChannel + ", " + strState);
                            flag = true;
                        }
                        else
                        {
                            logger.AdapterLogString(3, "sets the laser beam block wrong");
                        }
                    }
                    return flag;
                }
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }

        public override bool SetAllChannnel_RxOverLoad(float RxOverLoad)
        {
            for (int i = 0; i < Convert.ToInt32(TotalChannel); i++)
            {

                AttSlot = AttSlotList[i].ToString();
                DeviceChannel = DeviceChannelList[i].ToString();
               
               
                AttnValue(RxOverLoad.ToString(), 0);
            }
            return true;
        }
        /// <summary>
        /// 查询JDSU是否处于LegacyMode，如果是，则关闭JDSULegacyMode
        /// </summary>
        /// <returns></returns>
        private bool ConfigLegacyModeOff()
        {
            string readtemp = "";
            bool flag = false;
            int k = 0;

            try
            {
                for (k = 0; k < 3; k++)
                {
                    MyIO.WriteString("SYST:LEGA:MODE?");
                    readtemp = MyIO.ReadString();

                    if (Convert.ToInt32(readtemp) == 0)
                    {
                        break;
                    }
                    else
                    {
                        MyIO.WriteString("SYST:LEGA:MODE 0");
                    }
                }

                if (k <= 2)
                {
                    logger.AdapterLogString(0, "Disable JDSU legacy mode");
                    flag = true;
                }
                else
                {
                    logger.AdapterLogString(3, "Configure JDSU legacy mode wrong");
                }

                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        #endregion
    }
}
