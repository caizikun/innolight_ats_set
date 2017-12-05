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
    public class Inno25GBertPPG : PPG
    {
        public enum PG_Inverted : byte
        {
            Inverted = 1,
            NO_Inverted = 0,
        }
        public enum PrbsType : byte
        {
            prbs31 = 0,
            prbs9 = 1,
            //prbs31 = 4,
            prbs23 = 5,
            prbs15 = 6,
            prbs7 = 7
        }
        public enum Tx_Channel : byte 
        {
            TX1 = 0,
            TX2 = 1,
            TX3 = 2,
            TX4 = 3,
            AllTX = 4,
        }
        public enum TX_Set_Swing : byte 
        {
            Swing_0 = 0,
            Swing_25 = 1,
            Swing_50 = 2,
            Swing_75 = 3,
            Swing_100 = 4,
        }
        public enum Pre_Cursor_Type : byte 
        {
            Value_0 = 0,
            Value_5 = 1,
            Value_10 = 2,
            Value_15 = 3,
        }
        public enum PostcursorType : byte 
        {
            Value_0 = 0,
            Value_5 = 1,
            Value_10 = 2,
            Value_15 = 3,
            Value_20 = 4,
            Value_25 = 5,
            Value_30 = 6,
            Value_35 = 7
        }
        public enum IOLeverType : byte 
        {
            Clock_Sen = 0,
            Clock_Power_EN = 1,
            Bert_Power_EN = 2,
            IPHYCDR_3V3_RST1 = 3,
            IPHYCDR_3V3_RST2 = 4,
            GT1724_RST1 = 5,
            GT1724_RST2 = 6
        }
        public enum TriggerSelect : byte 
        {
            TX1 = 0,
            TX2 = 1,
            TX3 = 2,
            TX4 = 3,
        }

        public int DeviceIndex;
        public IOPort.CFKType FCK = IOPort.CFKType._100K;
        public int IN012525_Phycial_Add = 0;
        public Tx_Channel TXLane;
        public PrbsType PPGPattern;
        public PG_Inverted PPGInvert;
        public TX_Set_Swing Swing;
        public TriggerSelect TriggerOutput = TriggerSelect.TX1;
        private byte currPPGChannel = 1; // 1,2,3,4

        private Pre_Cursor_Type Pre_Cursor = Pre_Cursor_Type.Value_0; // 设置为0%
        private PostcursorType Post_Cursor = PostcursorType.Value_10; // 设置为10%

        private string[] TriggerOutputList;
      
        public Algorithm algorithm = new Algorithm();

        public Inno25GBertPPG(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] Inno_25GBert_PPGStruct) 
        {

            int i = 0;
            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "Addr", out i))
            {
                Addr = Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no Addr");
                return false;
            }

            DeviceIndex = Convert.ToInt32(Addr);

            //if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "deviceIndex".ToUpper(), out i))
            //{
            //    DeviceIndex = Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            //}
            //else
            //{
            //    logger.AdapterLogString(4, "there is no deviceIndex");
            //    return false;
            //}

            //if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "TriggerOutput".ToUpper(), out i))
            //{
            //    TriggerOutput = (TriggerSelect)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            //}
            //else
            //{
            //    logger.AdapterLogString(4, "there is no TriggerOutput");
            //    return false;
            //}

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "TriggerOutputList".ToUpper(), out i))
            {
                TriggerOutputList = Inno_25GBert_PPGStruct[i].DefaultValue.Split(',');
            }
            else
            {
                logger.AdapterLogString(4, "there is no TriggerOutputList");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "IOTYPE", out i))
            {
                IOType = Inno_25GBert_PPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "RESET", out i))
            {
                Reset = Convert.ToBoolean(Inno_25GBert_PPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "NAME", out i))
            {
                Name = Inno_25GBert_PPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "DATARATE", out i))
            {
                dataRate = Inno_25GBert_PPGStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no DATARATE");
                return false;
            }
            
            //if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "FCK", out i))
            //{
            //    FCK = (IOPort.CFKType)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            //}
            //else
            //{
            //    logger.AdapterLogString(4, "there is no FCK");
            //    return false;
            //}

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "PPGInvert".ToUpper(), out i))
            {
                PPGInvert = (PG_Inverted)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no PPGInvert");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "PPGPattern".ToUpper(), out i))
            {
                PPGPattern = (PrbsType)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no PPGPattern");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "SWING", out i))
            {
                Swing = (TX_Set_Swing)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no SWING");
                return false;
            }
           
            if (!Connect()) return false;
            return true;
        }
        public bool ReSet()
        {
            return true;
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

                    IniTialize_Bert();

                    //TriggerOutputSelect(Convert.ToInt32(TriggerOutput));

                    ConfigureChannel(4, syn);

                    TriggerOutputSelect(0);

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
        public override bool Connect()
        {
            try
            {
                MyIO = new IOPort("USB", Addr.ToString(), logger);
                MyIO.IOConnect();
                EquipmentConnectflag = true;

                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        /// <summary>
        /// 切换TriggerOutput通道
        /// </summary>
        /// <param name="channel">channel取值为1,2,3,4</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            try
            {
                currPPGChannel = Convert.ToByte(TriggerOutputList[Convert.ToInt32(channel) - 1]);

                logger.AdapterLogString(0, "TriggerOutput channel is " + currPPGChannel);

                TriggerOutput = (TriggerSelect)(currPPGChannel - 1);

                return TriggerOutputSelect(currPPGChannel - 1);
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        #region 私有方法
        private bool ConfigureChannel(byte channel, int syn = 0)
        {
            try
            {
                if (syn == 0)
                {
                    TXLane = (Tx_Channel)channel;
                    PPG_Set(TXLane, PPGPattern, Swing, PPGInvert);
                }
                else
                {
                    TXLane = (Tx_Channel)channel;
                    PPG_Set(TXLane, PPGPattern, Swing, PPGInvert);

                    PrbsType Pattern = PrbsType.prbs15;
                    PostcursorType PostCursorType = PostcursorType.Value_0;
                    Pre_Cursor_Type PreCursorType = Pre_Cursor_Type.Value_0;
                    TX_Set_Swing TXSwing = TX_Set_Swing.Swing_0;
                    PG_Inverted Invert = PG_Inverted.Inverted;

                    PPG_Read(TXLane, ref Pattern, ref PostCursorType, ref PreCursorType, ref TXSwing, ref Invert);
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
        #region H1035
        private void DataRateSet() // 150513
        {
            if (dataRate == "25.78")
            {
                FrequencySet(322.26562, 1000);
            }
            else
            {
                FrequencySet(350, 1000);
            }
        }
        /// <summary>
        /// 初始化H1035
        /// </summary>
        private void LoadDefaultToH1035() // 150513
        {
            int[] RegistArray = new int[35] { 0x0, 0x1, 0x2, 0x3, 0x5, 0x5, 0x5, 0x5, 0x6, 0x7, 0x8, 0x9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x4 };
            int[] Dat_Reg_Input = new int[35] { 0x94075, 0x2, 0x1, 0x2A, 0x90, 0xF98, 0x48B8, 0x0, 0x200B4A, 0x14D, 0xC1BEFF, 0x30ED5A, 0x2006, 0xF8061, 0x0, 0x0, 0x0, 0x1, 0x20, 0x7FFFF, 0x0, 0x1259, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x666666 };

            for (int i = 0; i < Dat_Reg_Input.Length; i++)
            {
                MyIO.ClockChip_WriteData_24bit(DeviceIndex, RegistArray[i], 
                    Dat_Reg_Input[i], IOPort.ReadWrite.Write);
            }
        }
        private bool LockDetect(int WriteData, int Swing, double Input1, int Input2)//输入参数1 黄线 参数2=蓝线  150513
        {
            bool Lock_Detect = false;
            int SwingIndex;
            switch (Swing)
            {
                case 700:
                    SwingIndex = 0;
                    break;
                case 800:
                    SwingIndex = 1;
                    break;
                case 900:
                    SwingIndex = 2;
                    break;
                case 1000:
                    SwingIndex = 3;
                    break;
                case 1100:
                    SwingIndex = 4;
                    break;
                case 1250:
                    SwingIndex = 5;
                    break;
                case 1400:
                    SwingIndex = 6;
                    break;
                case 1700:
                    SwingIndex = 7;
                    break;
                case 1800:
                    SwingIndex = 8;
                    break;
                case 2000:
                    SwingIndex = 9;
                    break;
                case 2250:
                    SwingIndex = 10;
                    break;
                case 2550:
                    SwingIndex = 11;
                    break;
                default:
                    SwingIndex = 10;
                    break;
            }

            try
            {
                MyIO.ClockChip_WriteData_24bit(DeviceIndex, 3, WriteData, IOPort.ReadWrite.Write);
                MyIO.ClockChip_WriteData_24bit(DeviceIndex, 5, 0, IOPort.ReadWrite.Write);
                MyIO.ClockChip_WriteData_24bit(DeviceIndex, 4, Convert.ToInt32(Math.Floor(Input1 * 16777216)), IOPort.ReadWrite.Write);
                MyIO.ClockChip_WriteData_24bit(DeviceIndex, 5, Input2, IOPort.ReadWrite.Write);
                MyIO.ClockChip_WriteData_24bit(DeviceIndex, 5, SwingIndex * 128 + 0x38, IOPort.ReadWrite.Write);
                int K = 0;

                K = MyIO.ClockChip_WriteData_24bit(DeviceIndex, 0X12, 0, IOPort.ReadWrite.Read);
                if (Convert.ToInt16(K / 2) % 2 == 1)
                {
                    Lock_Detect = true;
                    logger.AdapterLogString(0, "Lock Fre Detect");
                }
                else
                {
                    logger.AdapterLogString(3, "UnLock Fre Detect");
                    logger.FlushLogBuffer();
                    // MessageBox.Show("UnLock Fre Detect");
                    Lock_Detect = false;
                }
            }
            catch (System.Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
                Lock_Detect = false;
            }

            return Lock_Detect;
        }
        private void FrequencySet(double Frequency, int IoutputSwing) // 150513
        {
            if (Frequency > 150)
            {
                if (Frequency <= 300)
                {
                    LockDetect(Convert.ToInt16(Math.Floor(Frequency / 5)), IoutputSwing, (Frequency % 5) / 5, 1296);
                }
                else if (Frequency <= 400 && Frequency > 300)
                {
                    LockDetect(Convert.ToInt16(Math.Floor(Frequency / 8.3333333)), IoutputSwing, (Frequency % 8.3333333) / 8.3333333, 0x310);
                }
                else if (Frequency > 400)
                {
                    LockDetect(Convert.ToInt16(Math.Floor(Frequency / 12.5)), IoutputSwing, (Frequency % 12.5) / 12.5, 0X210);
                }
            }
            else
            {
                if (Frequency >= 70)
                {
                    LockDetect(Convert.ToInt16(Math.Floor(Frequency / 2.5)), IoutputSwing, (Frequency % 2.5) / 2.5, 0XA10);
                }
                else if (Frequency < 70 && Frequency >= 35)
                {
                    LockDetect(Convert.ToInt16(Math.Floor(Frequency / 1.25)), IoutputSwing, (Frequency % 1.25) / 1.25, 0X1410);

                }
                else if (Frequency < 35)
                {
                    LockDetect(Convert.ToInt16(Math.Floor(Frequency / 0.833333333)), IoutputSwing, (Frequency % 0.833333333) / 0.833333333, 0X1E10);
                }
            }
        }
        #endregion

        #region IN012525
        private void PPG_Set(Tx_Channel TxLane, PrbsType Pattern, TX_Set_Swing Swing, PG_Inverted invert) // 150513
        {
            TX_Set(TxLane, Pattern, Swing, invert);
        }
        private void PPG_Read(Tx_Channel TXLane, ref PrbsType Pattern, ref PostcursorType PostCursorType,
            ref Pre_Cursor_Type PreCursorType, ref TX_Set_Swing TXSwing, ref PG_Inverted Invert) // 150513
        {
            TX_Read(TXLane, ref Pattern, ref PostCursorType, ref PreCursorType, ref TXSwing, ref Invert);
        }
        /// <summary>
        /// 设置PPG的TX通道的值
        /// </summary>
        /// <param name="TxLane"></param>
        /// <param name="Pattern"></param>
        /// <param name="Swing"></param>
        /// <param name="invert"></param>
        private void TX_Set(Tx_Channel TxLane, PrbsType Pattern, TX_Set_Swing Swing, PG_Inverted invert) // 150513
        {
            int RunCount = 1; // 循环次数

            int[] RegistArray = new int[3];
            int[] WriteDataArray = new int[3];
            int[] ReadData = new int[3];

            try
            {
                if (Convert.ToInt16(TxLane) == 4)
                {
                    RunCount = 4;
                }

                for (int i = 0; i < RunCount; i++)
                {
                    if (Convert.ToInt16(TxLane) == 4)
                    {
                        RegistArray[0] = i + 0x10;
                        RegistArray[1] = i * 256 + 0x101;
                        RegistArray[2] = i * 256 + 0x102;
                    }
                    else
                    {
                        RegistArray[0] = Convert.ToInt16(TxLane) + 0x10;
                        RegistArray[1] = Convert.ToInt16(TxLane) * 256 + 0x101;
                        RegistArray[2] = Convert.ToInt16(TxLane) * 256 + 0x102;
                    }

                    WriteDataArray[0] = Convert.ToInt16(invert) * 8 + 0x10 + Convert.ToInt32(Pattern);
                    WriteDataArray[1] = Convert.ToInt16(Convert.ToInt32(Post_Cursor) * 256 + Convert.ToInt16(Pre_Cursor));
                    WriteDataArray[2] = Convert.ToInt16(Swing);

                    for (int j = 0; j < 3; j++)
                    {
                        MyIO.WriteMDIO_Bert_25G(DeviceIndex, 30, IN012525_Phycial_Add, RegistArray[j], WriteDataArray[j], 1);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
            }
        }
        /// <summary>
        /// 读取PPG某一TX通道的设置值
        /// </summary>
        /// <param name="TXLane"></param>
        /// <param name="Pattern"></param>
        /// <param name="PostCursorType"></param>
        /// <param name="PreCursorType"></param>
        /// <param name="TXSwing"></param>
        /// <param name="Invert"></param>
        private void TX_Read(Tx_Channel TXLane, ref PrbsType Pattern, ref PostcursorType PostCursorType,
            ref Pre_Cursor_Type PreCursorType, ref TX_Set_Swing TXSwing, ref PG_Inverted Invert) // 150513
        {
            try
            {
                int[] RegistArray = new int[3];
                int[] ReadData = new int[3];

                RegistArray[0] = Convert.ToInt16(TXLane) + 0x10;
                RegistArray[1] = Convert.ToInt16(TXLane) * 256 + 0x101;
                RegistArray[2] = Convert.ToInt16(TXLane) * 256 + 0x102;

                for (int j = 0; j < 3; j++)
                {
                    ReadData[j] = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, IN012525_Phycial_Add, RegistArray[j], 1);
                }

                if (Convert.ToInt16(TXLane) != 4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                        {
                            Pattern = (PrbsType)(ReadData[i] % 8);
                            Invert = (PG_Inverted)(ReadData[i] / 8 % 2);
                        }
                        else if (i == 1)
                        {
                            PreCursorType = (Pre_Cursor_Type)(ReadData[i] % 4);
                            PostCursorType = (PostcursorType)(ReadData[i] / 256 % 8);
                        }
                        else
                        {
                            TXSwing = (TX_Set_Swing)(ReadData[i] % 5);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
            }
        }
        #endregion

        private void IniTialize_Bert() // 150513
        {
            #region  Loop 1
            MyIO.IoLevelControl(DeviceIndex, 1, Convert.ToByte(IOLeverType.Clock_Power_EN));
            Thread.Sleep(200);

            for (int i = 0; i < 2; i++)
            {
                MyIO.IoLevelControl(DeviceIndex, Convert.ToByte((i + 1) % 2), Convert.ToByte(IOLeverType.Clock_Sen));
                Thread.Sleep(10);
            }
            LoadDefaultToH1035();
            DataRateSet();

            #endregion
            #region  Loop2

            MyIO.IoLevelControl(DeviceIndex, 0, Convert.ToByte(IOLeverType.Bert_Power_EN));
            Thread.Sleep(1000);
            MyIO.IoLevelControl(DeviceIndex, 0, Convert.ToByte(IOLeverType.IPHYCDR_3V3_RST2));
            Thread.Sleep(20);
            MyIO.IoLevelControl(DeviceIndex, 1, Convert.ToByte(IOLeverType.IPHYCDR_3V3_RST2));
            Thread.Sleep(20);
            Thread.Sleep(100);
            bool K = Initalize_ON_Power_Up_OR_Reset(Convert.ToInt32(TriggerOutput), 0);

            if (!K)
            {
                // MessageBox.Show("Initalize_ON_Power_Up_OR_Reset ERROR!");
                logger.AdapterLogString(3, "Initalize_ON_Power_Up_OR_Reset ERROR!");
                logger.FlushLogBuffer();
            }

            for (int i = 0; i < 4; i++)
            {
                Int32 ReadData = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, IN012525_Phycial_Add, i * 0x100 + 0x123, 1);

                if (Convert.ToInt16(ReadData / 32768) % 2 == 1)
                {
                    logger.AdapterLogString(0, i + "OK----");
                    logger.FlushLogBuffer();
                }
            }
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TriggeroutPutSelect">  0=TX1, 1=TX2,2=TX3,3=TX4 </param>
        /// <param name="Phycial_address">1=Clock Power EN ,0=Clock_Sen ,2=Bert_Power_EN,3=3V3_IPHYCDR_RST1,4=3V3_IPHYCDR_RST2,5=GT1724_RST1,6=GT1724_RST2</param>
        private bool Initalize_ON_Power_Up_OR_Reset(int TriggeroutPutSelect, int Phycial_address) // 150513
        {
            bool flag = true;
            Int32 Readbuff = 0;

            int[] RegistArray;
            Int32[] WriteDataArray;

            if (dataRate == "25.78")
            {
                #region  25G

                RegistArray = new int[34] { 0x0, 0x0, 0xB3, 0xB5, 0x0, 0xB4, 0x2C, 0x0, 0x521, 0x14, 0x2C, 0x2C, 0x2C, 0x2C, 0x581, 0x598, 0x580, 0x2C, 0x580, 0x580, 0x580, 0x580, 0x580, 0x580, 0x598, 0x14, 0x1E, 0x1F, 0x20, 0x2D, 0x101, 0x201, 0x301, 0x401 };
                WriteDataArray = new Int32[34] { 0x1220, 0x220, 0x8000, 0x1, 0x200, 0x2423, 0x9C08, 0x8000, 0x810, 0x10, 0x9C00, 0x8C00, 0x8400, 0x8000, 0x2, 0x2028, 0x418, 0x0, 0x410, 0x412, 0x410, 0x10, 0x110, 0x10, 0x28, 0x5010, 0xAAAA, 0xAAAA, 0xAA, 0x8000, 0x200, 0x200, 0x200, 0x200 };

                #endregion
            }
            else
            {
                #region 28G
                #region         填写数组

                RegistArray = new int[34];
                RegistArray[0] = 0;
                RegistArray[1] = 0;
                RegistArray[2] = 0xb3;
                RegistArray[3] = 0xb5;
                RegistArray[4] = 0;
                RegistArray[5] = 0xb4;
                RegistArray[6] = 0x2c;
                RegistArray[7] = 0;

                RegistArray[8] = 0X521;
                RegistArray[9] = 0x14;
                RegistArray[10] = 0X2C;
                RegistArray[11] = 0x2C;
                RegistArray[12] = 0X2C;
                RegistArray[13] = 0x2C;
                RegistArray[14] = 0X581;
                RegistArray[15] = 0x598;


                RegistArray[16] = 0X580;
                RegistArray[17] = 0x2C;
                RegistArray[18] = 0X580;
                RegistArray[19] = 0X580;
                RegistArray[20] = 0X580;
                RegistArray[21] = 0X580;
                RegistArray[22] = 0X580;
                RegistArray[23] = 0X580;


                RegistArray[24] = 0X598;
                RegistArray[25] = 0x14;
                RegistArray[26] = 0X1E;
                RegistArray[27] = 0X1F;
                RegistArray[28] = 0X20;
                RegistArray[29] = 0X2D;
                RegistArray[30] = 0X101;
                RegistArray[31] = 0X201;

                RegistArray[32] = 0X301;
                RegistArray[33] = 0X401;


                WriteDataArray = new int[34];

                WriteDataArray[0] = 0X5220;
                WriteDataArray[1] = 0X4220;
                WriteDataArray[2] = 0x8000;
                WriteDataArray[3] = 0x1;
                WriteDataArray[4] = 0X200;
                WriteDataArray[5] = 0x2423;
                WriteDataArray[6] = 0x9c08;
                WriteDataArray[7] = 0X8000;

                WriteDataArray[8] = 0X810;
                WriteDataArray[9] = 0x10;
                WriteDataArray[10] = 0X9C00;
                WriteDataArray[11] = 0x8C00;
                WriteDataArray[12] = 0X8400;
                WriteDataArray[13] = 0x8000;
                WriteDataArray[14] = 0X2;
                WriteDataArray[15] = 0x2028;


                WriteDataArray[16] = 0X418;
                WriteDataArray[17] = 0x0;
                WriteDataArray[18] = 0X410;
                WriteDataArray[19] = 0X412;
                WriteDataArray[20] = 0X410;
                WriteDataArray[21] = 0X10;
                WriteDataArray[22] = 0X110;
                WriteDataArray[23] = 0X10;


                WriteDataArray[24] = 0X28;
                WriteDataArray[25] = 0x5010;
                WriteDataArray[26] = 0XAAAA;
                WriteDataArray[27] = 0XAAAA;
                WriteDataArray[28] = 0XAA;
                WriteDataArray[29] = 0X8000;
                WriteDataArray[30] = 0X200;
                WriteDataArray[31] = 0X200;

                WriteDataArray[32] = 0X200;
                WriteDataArray[33] = 0X200;

                if (TriggeroutPutSelect == 1)
                {
                    WriteDataArray[29] = 0X8001;
                }
                else if (TriggeroutPutSelect == 0)
                {
                    WriteDataArray[29] = 0X8000;
                }
                else if (TriggeroutPutSelect == 2)
                {
                    WriteDataArray[29] = 0X8002;
                }
                else if (TriggeroutPutSelect == 3)
                {
                    WriteDataArray[29] = 0X8003;
                }
                #endregion

                #region 去除
                //for (int j = 0; j < 34; j++)
                //{
                //    ushort[] Writebuff = new ushort[1];

                //    Writebuff[0] = Convert.ToByte(WriteDataArray[j]);
                //    MyIO.WriteMDIO(DeviceIndex, 30, Phycial_address, RegistArray[j], IOPort.MDIOSoftHard.SOFTWARE, Writebuff);
                //    if (j == 4)
                //    {

                //        for (int K = 0; K < 4; K++)
                //        {

                //            Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0x600, 1);
                //            if (Convert.ToInt16(Readbuff / 8192) % 2 == 1)
                //            {
                //                break;
                //            }
                //            if (K > 3)
                //            {
                //                flag = false; ;
                //            }
                //            Thread.Sleep(1000);
                //        }

                //    }
                //    if (j == 6)
                //    {
                //        for (int K = 0; K < 4; K++)
                //        {

                //            Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0x2c, 1);
                //            if (Convert.ToInt16(Readbuff / 16) % 2 == 1)
                //            {
                //                break;

                //            }
                //            if (K > 3)
                //            {
                //                flag = false; ;
                //            }
                //            Thread.Sleep(1000);
                //        }
                //    }
                //    if (j == 11)
                //    {
                //        for (int K = 0; K < 4; K++)
                //        {

                //            Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0x2c, 1);
                //            if (Convert.ToInt16(Readbuff / 64) % 2 == 1)
                //            {

                //                break;

                //            }
                //            else
                //            {


                //                if (K > 3)
                //                {
                //                    flag = false; ;
                //                }
                //            }
                //            Thread.Sleep(1000);
                //        }
                //    }
                //    if (j == 20)
                //    {
                //        for (int K = 0; K < 4; K++)
                //        {

                //            Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, K * 0X100 + 0X180, 1);
                //            if (Convert.ToInt16(Readbuff / 16) % 2 == 1)
                //            {
                //                break;
                //            }
                //            else
                //            {
                //                if (K > 3) flag = false;
                //            }
                //            Thread.Sleep(1000);
                //        }
                //    }
                //    if (j == 23)
                //    {
                //        for (int K = 0; K < 4; K++)
                //        {
                //            Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0X2C, 1);
                //            if (Convert.ToInt16(Readbuff / 32) % 2 == 1)
                //            {
                //                flag = true;
                //                break;
                //            }
                //            else
                //            {
                //                if (K > 3) flag = false;
                //            }
                //            Thread.Sleep(1000);
                //        }
                //    }
                //}
                #endregion
                #endregion
            }

            for (int j = 0; j < 34; j++)
            {
                Readbuff = 0;
                MyIO.WriteMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, RegistArray[j], WriteDataArray[j], 1);
                Thread.Sleep(10);

                int K = 0;

                if (j == 4)
                {
                    for (K = 0; K < 4; K++)
                    {
                        Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0X600, 1);
                        if (Convert.ToInt16(Readbuff / 8192) % 2 == 1)
                        {
                            break;
                        }
                        
                        Thread.Sleep(1000);
                    }

                }
                if (j == 6)
                {
                    for (K = 0; K < 4; K++)
                    {
                        Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0X2C, 1);

                        if (Convert.ToInt16(Readbuff / 16) % 2 == 1)
                        {
                            break;
                        }

                        Thread.Sleep(1000);
                    }
                }
                if (j == 11)
                {
                    for (K = 0; K < 4; K++)
                    {
                        Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0X2C, 1);
                        if (Convert.ToInt16(Readbuff / 64) % 2 == 1)
                        {
                            break;
                        }

                        Thread.Sleep(1000);
                    }
                }
                if (j == 20)
                {
                    for (K = 0; K < 4; K++)
                    {
                        Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, K * 0X100 + 0X180, 1);
                        if (Convert.ToInt16(Readbuff / 16) % 2 == 1)
                        {
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }
                if (j == 23)
                {
                    for (K = 0; K < 4; K++)
                    {
                        Readbuff = MyIO.ReadMDIO_Bert_25G(DeviceIndex, 30, Phycial_address, 0X2C, 1);

                        if (Convert.ToInt16(Readbuff / 32) % 2 == 1)
                        {
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }

                if (K > 3)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerOutputSelect">0=TX1,1=TX2,2=TX3,3=TX4</param>
        /// <returns></returns>
        private bool TriggerOutputSelect(int triggerOutputSelect) // 150511
        {
            try
            {
                int dataToWrite = triggerOutputSelect + 0x8000;
                MyIO.WriteMDIO_Bert_25G(DeviceIndex, 30, IN012525_Phycial_Add, 45, dataToWrite, 1);

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        #endregion
    }

    public class Inno25GBertED : ErrorDetector
    {
        public enum ED_Inverted : byte
        {
            NO_Inverted = 0,
            Inverted = 1,
        }
        public enum PrbsType : byte
        {
            prbs9 = 0,
            prbs15 = 1,
            prbs31 = 2,
        }
        public enum RX_Channel : byte
        {
            RX1 = 0,
            RX2 = 1,
            RX3 = 2,
            RX4 = 3,
            AllRX = 4,
        }
        public enum RXEnable : byte
        {
            Disable = 0,
            Enable = 1,
        }
        public enum IC1724 : byte
        {
            IC1724_1 = 0,
            IC1724_2 = 1,
        }

        public string dataRate;
        public RX_Channel RXSelect;
        public RXEnable RXSwitch = RXEnable.Enable;
        public PrbsType EDPattern;
        public ED_Inverted EDInvert; 
        public byte DeviceIndex;
        public IOPort.CFKType FCK = IOPort.CFKType._100K;
        public int IN012525_Phycial_Add = 0;
        private byte currEDChannel = 1; // 1,2,3,4
        private string[] TriggerOutputList;
        private byte[] MarcoData = new byte[5826] { 0x47, 0x4E, 0x32, 0x31, 0x30, 0x34, 0x0, 0x45, 0x0, 0x42, 0xE0, 0xC5, 0x16, 0xB1, 0xB1, 0x2, 0xE8, 0xF, 0x0, 0x45, 0x0, 0x42, 0xEF, 0x4B, 0xFF, 0xEE, 0x4A, 0xFE, 0xED, 0x49, 0xFD, 0xEC, 0x48, 0xFC, 0x22, 0x60, 0x2, 0x64, 0x3, 0x68, 0x3, 0x6C, 0x4, 0x6F, 0x4, 0x73, 0x5, 0x77, 0x5, 0x7B, 0x6, 0x7F, 0x6, 0x82, 0x7, 0x86, 0x7, 0x8A, 0x8, 0x8E, 0x8, 0x91, 0x9, 0x95, 0x9, 0x99, 0xA, 0x9D, 0xA, 0x90, 0x4, 0x90, 0xE0, 0x7F, 0x1, 0x20, 0xE0, 0x2, 0x7F, 0x0, 0x12, 0xF4, 0xB7, 0xE4, 0xFD, 0xED, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x25, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x44, 0x1, 0xF0, 0xEF, 0x24, 0x31, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x54, 0xF3, 0x44, 0x8, 0x31, 0xA0, 0x24, 0x1F, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x44, 0x1F, 0xF0, 0xD, 0xBD, 0x4, 0xCC, 0x90, 0x4, 0x61, 0xE0, 0x54, 0xE0, 0x44, 0x14, 0xF0, 0xA3, 0xE0, 0x54, 0xC0, 0x44, 0x18, 0xF0, 0xA3, 0xE0, 0x54, 0xC0, 0x44, 0x1B, 0xF0, 0xA3, 0xE0, 0x54, 0xC0, 0x44, 0x12, 0xF0, 0x7E, 0x0, 0x7F, 0x21, 0x7D, 0x0, 0x7B, 0x1, 0x7A, 0xE0, 0x79, 0x10, 0x12, 0x3, 0x6A, 0x90, 0xE0, 0xA2, 0x74, 0x77, 0xF0, 0x12, 0xF0, 0x8A, 0xE4, 0xF5, 0x22, 0xF5, 0x23, 0x90, 0x4, 0x52, 0xE0, 0x54, 0x7F, 0x30, 0xE6, 0x7, 0xE0, 0x54, 0x3F, 0xF5, 0x21, 0x80, 0xA, 0x90, 0x4, 0x52, 0xE0, 0x54, 0x3F, 0xF4, 0x4, 0xF5, 0x21, 0x75, 0x22, 0x2, 0x75, 0x23, 0xDA, 0x22, 0x3E, 0xF5, 0x83, 0xE0, 0xE4, 0xF0, 0xED, 0x7F, 0x0, 0xFE, 0xEF, 0x22, 0xAD, 0x7, 0x90, 0xE0, 0x7C, 0xED, 0xF0, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x10, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x54, 0xF0, 0xF0, 0xEF, 0x24, 0x14, 0xF5, 0x82, 0x74, 0x0, 0x31, 0x9B, 0x24, 0x13, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0xE4, 0xF0, 0x24, 0x12, 0xF5, 0x82, 0x74, 0x0, 0x31, 0x9B, 0x24, 0x11, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x74, 0xA8, 0xF0, 0x12, 0xEF, 0xD1, 0x51, 0x4, 0x44, 0x1, 0xF0, 0x22, 0xF0, 0x54, 0x7F, 0xFD, 0x90, 0xE0, 0xA3, 0xE0, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x2F, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x54, 0x80, 0x22, 0x71, 0x9D, 0xC3, 0x13, 0x54, 0x1, 0x4F, 0xFF, 0x90, 0xE0, 0xA3, 0xF0, 0xE4, 0xA3, 0xF0, 0xEF, 0x31, 0xFA, 0xF0, 0xF1, 0x6C, 0x90, 0xE0, 0xA5, 0x74, 0x7F, 0xF0, 0x90, 0xE0, 0xA3, 0xE0, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x2F, 0xF5, 0x82, 0x74, 0x0, 0x12, 0xF7, 0x6E, 0xF1, 0x74, 0x51, 0xE5, 0x50, 0x2, 0x80, 0x1, 0xC3, 0x92, 0x0, 0x20, 0x0, 0x10, 0x51, 0xE5, 0x70, 0xC, 0x12, 0xF7, 0x50, 0x51, 0xEE, 0x50, 0x2, 0x80, 0x1, 0xC3, 0x92, 0x0, 0x30, 0x0, 0xA, 0x90, 0xE0, 0xA5, 0x74, 0x3F, 0xF0, 0xC2, 0x1, 0x80, 0x8, 0x90, 0xE0, 0xA4, 0x74, 0x3F, 0xF0, 0xD2, 0x1, 0x31, 0xF6, 0x44, 0x3F, 0xF0, 0x30, 0x1, 0x31, 0xF1, 0x6C, 0x51, 0xE5, 0x50, 0x2, 0x80, 0x1, 0xC3, 0x92, 0x0, 0x20, 0x0, 0x10, 0x51, 0xE5, 0x70, 0xC, 0x12, 0xF7, 0x50, 0x51, 0xEE, 0x50, 0x2, 0x80, 0x1, 0xC3, 0x92, 0x0, 0x30, 0x0, 0xB, 0x12, 0xEA, 0xDB, 0x31, 0xF2, 0x4D, 0xF0, 0xC2, 0x1, 0x80, 0xD1, 0x90, 0xE0, 0xA5, 0x80, 0x3C, 0xF1, 0x74, 0xF1, 0xEA, 0x51, 0xEE, 0x50, 0x2, 0x80, 0x1, 0xC3, 0x92, 0x0, 0x20, 0x0, 0x18, 0xF1, 0xEA, 0x51, 0xEE, 0x70, 0x12, 0x90, 0xE0, 0xAA, 0x12, 0x2, 0x60, 0x90, 0xE0, 0xB2, 0x51, 0xEE, 0x50, 0x2, 0x80, 0x1, 0xC3, 0x92, 0x0, 0x30, 0x0, 0xE, 0x12, 0xEA, 0xDB, 0x90, 0xE0, 0xA4, 0x31, 0xF2, 0x4D, 0xF0, 0xD2, 0x1, 0x80, 0x93, 0x90, 0xE0, 0xA4, 0xE0, 0x31, 0xF3, 0x4D, 0xF0, 0x22, 0x90, 0xE0, 0xAE, 0x12, 0x2, 0x60, 0x90, 0xE0, 0xA6, 0x12, 0x2, 0x6C, 0xC3, 0x2, 0x2, 0x3C, 0xE4, 0xF5, 0x75, 0x71, 0xA2, 0x20, 0xE0, 0x5, 0x71, 0xC3, 0x30, 0xE0, 0x78, 0x71, 0xC3, 0x20, 0xE0, 0x43, 0xC2, 0x0, 0x12, 0xF0, 0x9D, 0x90, 0x0, 0x10, 0x7D, 0x0, 0xE5, 0x75, 0x75, 0xF0, 0x16, 0xA4, 0x24, 0x35, 0xF9, 0x74, 0xE0, 0x35, 0xF0, 0xFA, 0x7B, 0x1, 0xAE, 0x83, 0xAF, 0x82, 0x12, 0x3, 0x6A, 0xAF, 0x75, 0x12, 0x3D, 0x63, 0x71, 0xA2, 0xC4, 0x54, 0xF, 0x20, 0xE0, 0x7, 0xAF, 0x75, 0x12, 0x3D, 0x8D, 0x51, 0xB, 0xAF, 0x75, 0x12, 0x3C, 0x78, 0xD2, 0x2, 0xF1, 0x89, 0x71, 0xC3, 0x44, 0x1, 0xF0, 0x71, 0xB3, 0xE0, 0x20, 0xE0, 0x9, 0x71, 0xA2, 0x30, 0xE0, 0x4, 0xF1, 0xF7, 0x50, 0x12, 0xC2, 0x2, 0xF1, 0x89, 0xAF, 0x75, 0x12, 0x33, 0xB4, 0x71, 0xA2, 0x30, 0xE0, 0x4, 0xD2, 0x2, 0xF1, 0x89, 0x71, 0xA2, 0x20, 0xE0, 0xA, 0xD2, 0x0, 0x12, 0xF0, 0x9D, 0x71, 0xC3, 0x54, 0xFE, 0xF0, 0x71, 0xB3, 0xE0, 0x30, 0xE0, 0x11, 0xAF, 0x75, 0x12, 0x3C, 0x3D, 0x71, 0xB3, 0xE0, 0x54, 0xFE, 0xF0, 0x71, 0xB3, 0xE0, 0x44, 0x8, 0xF0, 0x5, 0x75, 0xE5, 0x75, 0xC3, 0x94, 0x2, 0x50, 0x2, 0x41, 0xF8, 0x22, 0xE5, 0x75, 0x25, 0xE0, 0xFF, 0xE5, 0x75, 0x75, 0xF0, 0x16, 0xA4, 0x24, 0x32, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE0, 0x22, 0xE5, 0x75, 0x75, 0xF0, 0x16, 0xA4, 0x24, 0x33, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0x22, 0xE5, 0x75, 0x75, 0xF0, 0x16, 0xA4, 0x24, 0x34, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE0, 0x22, 0x90, 0xE0, 0x20, 0xE0, 0xFF, 0xE4, 0xF5, 0x11, 0xEF, 0x54, 0xFE, 0x70, 0x2, 0xE1, 0x6B, 0xEF, 0x12, 0x3, 0x11, 0xE4, 0x3F, 0x10, 0xE4, 0x4A, 0x18, 0xE4, 0x6C, 0x20, 0xE4, 0x84, 0x28, 0xE4, 0x93, 0x41, 0xE4, 0xA6, 0x42, 0xE4, 0xBA, 0x50, 0xE4, 0xFF, 0x51, 0xE5, 0x2E, 0x52, 0xE5, 0x72, 0x53, 0xE6, 0xD, 0x58, 0xE6, 0x17, 0x59, 0xE6, 0x20, 0x5A, 0xE6, 0x2F, 0x5E, 0xE6, 0x39, 0x5F, 0xE6, 0x43, 0x60, 0xE6, 0x51, 0x61, 0xE6, 0x56, 0x62, 0xE6, 0x7B, 0x63, 0xE6, 0x89, 0x64, 0xE6, 0x97, 0x65, 0xE6, 0x9C, 0x66, 0xE6, 0xD8, 0x68, 0xE6, 0xE0, 0x69, 0xE7, 0x2, 0x6A, 0xE7, 0x2A, 0x6B, 0xE7, 0x32, 0x6C, 0xE7, 0x3A, 0x6D, 0x0, 0x0, 0xE7, 0x62, 0x12, 0x8, 0xCC, 0x90, 0xE0, 0x20, 0xE0, 0xF5, 0x11, 0xE1, 0x65, 0x90, 0xE0, 0xC8, 0xE4, 0x93, 0x90, 0xE0, 0x21, 0xF0, 0x90, 0xE0, 0xC9, 0xE4, 0x93, 0x90, 0xE0, 0x22, 0xF0, 0x90, 0xE0, 0xCA, 0xE4, 0x93, 0x90, 0xE0, 0x23, 0xF0, 0x90, 0xE0, 0xCB, 0xE4, 0x93, 0xE1, 0x5C, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x12, 0xE9, 0xEC, 0x12, 0xF0, 0xB, 0xC2, 0x26, 0x12, 0x2D, 0xCA, 0x90, 0xE0, 0x21, 0xE5, 0x74, 0xF0, 0xE1, 0x65, 0x12, 0xE8, 0xEF, 0xE5, 0x22, 0x90, 0xE0, 0x21, 0xF0, 0xA3, 0xE5, 0x23, 0xF0, 0xE1, 0x65, 0x90, 0xE0, 0x21, 0x74, 0xF7, 0xF0, 0xA3, 0x74, 0x80, 0xF0, 0xA3, 0xE4, 0xF0, 0xA3, 0x74, 0x80, 0xF0, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x31, 0xA7, 0x7E, 0xE0, 0x7F, 0x10, 0x7C, 0xE0, 0x7D, 0x21, 0x12, 0xEC, 0x2A, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xFE, 0x54, 0xF, 0x90, 0xE0, 0x32, 0xF0, 0xEE, 0xC4, 0x54, 0xF, 0x90, 0xE0, 0x48, 0xF0, 0x90, 0xE0, 0x11, 0xE0, 0xFC, 0x7E, 0x0, 0x30, 0xE0, 0x2, 0x7E, 0x1, 0xEE, 0x54, 0x1, 0x33, 0x33, 0x33, 0x54, 0xF8, 0xFE, 0x90, 0x4, 0x2C, 0xE0, 0x54, 0xF7, 0x4E, 0xF0, 0xEC, 0x7F, 0x0, 0x30, 0xE1, 0x2, 0x7F, 0x1, 0x12, 0xF4, 0x51, 0x90, 0x4, 0x3E, 0xE0, 0x54, 0xF7, 0x4F, 0xF0, 0x80, 0x6F, 0x90, 0xE0, 0x48, 0xE0, 0xC4, 0x54, 0xF0, 0xFF, 0x90, 0xE0, 0x32, 0xE0, 0x54, 0xF, 0x4F, 0x90, 0xE0, 0x21, 0xF0, 0x90, 0x4, 0x3E, 0xE0, 0x13, 0x13, 0x13, 0x54, 0x1, 0x25, 0xE0, 0xFF, 0x90, 0x4, 0x2C, 0xE0, 0x13, 0x13, 0x13, 0x54, 0x1, 0x4F, 0x90, 0xE0, 0x22, 0xF0, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xFC, 0x54, 0x1, 0xFE, 0x90, 0xE0, 0x32, 0xF1, 0xE1, 0x30, 0xE1, 0x2, 0x7F, 0x1, 0xF1, 0xC7, 0x90, 0xE0, 0x32, 0xE0, 0x54, 0xEF, 0x4F, 0xF0, 0x90, 0xE0, 0x10, 0xE0, 0xFC, 0x7E, 0x0, 0x30, 0xE4, 0x2, 0x7E, 0x1, 0xEE, 0x54, 0x1, 0xFE, 0x90, 0xE0, 0x48, 0xF1, 0xE1, 0x30, 0xE5, 0x2, 0x7F, 0x1, 0xF1, 0xC7, 0x90, 0xE0, 0x48, 0xE0, 0x54, 0xEF, 0x4F, 0xF0, 0x51, 0xF5, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x54, 0x1, 0xF5, 0x10, 0xEF, 0x54, 0x6, 0x44, 0x1, 0xFF, 0xE5, 0x10, 0x71, 0xB5, 0xEF, 0xF0, 0x51, 0xF5, 0xE5, 0x10, 0x71, 0xB5, 0xE0, 0xFF, 0x13, 0x13, 0x13, 0x54, 0x1F, 0x30, 0xE0, 0x60, 0xEF, 0xC3, 0x13, 0xF1, 0xCF, 0xBF, 0x2, 0x2D, 0xA4, 0x24, 0x35, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0x12, 0x2, 0x60, 0x90, 0xE0, 0x21, 0x12, 0x2, 0x78, 0xE5, 0x10, 0x75, 0xF0, 0x16, 0xA4, 0x24, 0x39, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0x12, 0x2, 0x60, 0x90, 0xE0, 0x25, 0x12, 0x2, 0x78, 0xE1, 0x65, 0xA4, 0x24, 0x45, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE0, 0xFC, 0xA3, 0xE0, 0xFD, 0xEC, 0xF1, 0xCF, 0xA4, 0x24, 0x47, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE0, 0x25, 0xE0, 0x25, 0xE0, 0x4F, 0x90, 0xE0, 0x21, 0xF0, 0xA3, 0xED, 0xF0, 0xE1, 0x65, 0x90, 0xE0, 0x21, 0x12, 0x2, 0x84, 0x0, 0x0, 0x0, 0x0, 0x90, 0xE0, 0x25, 0x12, 0x2, 0x84, 0x0, 0x0, 0x0, 0x0, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x12, 0xF5, 0x1B, 0xE1, 0x65, 0xF1, 0xF4, 0x90, 0xE0, 0x21, 0xEF, 0xF0, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xC3, 0x60, 0x1, 0xD3, 0x92, 0x0, 0x12, 0xF5, 0x9E, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x12, 0xF4, 0xB7, 0xE1, 0x65, 0xA2, 0x3, 0xE4, 0x33, 0x90, 0xE0, 0x21, 0xF0, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x12, 0xF2, 0x89, 0x50, 0x2, 0xE1, 0x65, 0xE1, 0x62, 0x12, 0x8, 0xCC, 0xE1, 0x65, 0x20, 0x3, 0x20, 0xE4, 0xF5, 0x10, 0x74, 0x10, 0x25, 0x10, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE0, 0xFD, 0xAF, 0x10, 0x12, 0xF6, 0x5A, 0x5, 0x10, 0xE5, 0x10, 0xC3, 0x94, 0x4, 0x40, 0xE5, 0xE1, 0x65, 0xE1, 0x62, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x12, 0xF1, 0x54, 0x50, 0x2, 0xE1, 0x65, 0xE1, 0x62, 0x90, 0xE0, 0x10, 0xE0, 0xFF, 0x12, 0xF5, 0xDF, 0x50, 0x2, 0xE1, 0x65, 0xE1, 0x62, 0x12, 0xF1, 0xFE, 0xE1, 0x65, 0x90, 0xE0, 0x10, 0xE0, 0x54, 0x1F, 0xFF, 0x90, 0x0, 0x37, 0xE0, 0x54, 0xE0, 0x4F, 0xF0, 0x90, 0xE0, 0x11, 0x12, 0xF7, 0x64, 0x90, 0x1, 0x37, 0xE0, 0x54, 0xE0, 0x4F, 0xF0, 0x90, 0xE0, 0x12, 0xE0, 0x54, 0x1F, 0xFF, 0x90, 0x2, 0x37, 0xE0, 0x54, 0xE0, 0x4F, 0xF0, 0x90, 0xE0, 0x13, 0x12, 0xF7, 0x64, 0x90, 0x3, 0x37, 0xE0, 0x54, 0xE0, 0x4F, 0xF0, 0xE1, 0x65, 0x90, 0xE0, 0x21, 0xE5, 0x1D, 0xF0, 0xE1, 0x65, 0x90, 0x0, 0x33, 0xE0, 0x90, 0xE0, 0x21, 0xF0, 0x90, 0x1, 0x33, 0xE0, 0x90, 0xE0, 0x22, 0xF0, 0x90, 0x2, 0x33, 0xE0, 0x90, 0xE0, 0x23, 0xF0, 0x90, 0x3, 0x33, 0xE0, 0x90, 0xE0, 0x24, 0xF0, 0x80, 0x63, 0x90, 0x0, 0x7, 0xE0, 0x90, 0xE0, 0x21, 0xF0, 0x90, 0x1, 0x7, 0xE0, 0xFF, 0xC3, 0x74, 0xFF, 0x9F, 0x90, 0xE0, 0x22, 0xF0, 0x90, 0x2, 0x7, 0xE0, 0x90, 0xE0, 0x23, 0xF0, 0x90, 0x3, 0x7, 0xE0, 0xFF, 0xC3, 0x74, 0xFF, 0x9F, 0x80, 0x32, 0x90, 0xE0, 0x21, 0xE5, 0x1E, 0xF0, 0x80, 0x33, 0x90, 0xE0, 0x21, 0xE5, 0x1F, 0xF0, 0x80, 0x2B, 0x90, 0x0, 0x37, 0xE0, 0x54, 0x1F, 0x90, 0xE0, 0x21, 0xF0, 0x90, 0x1, 0x37, 0xF1, 0xD8, 0x90, 0xE0, 0x22, 0xF0, 0x90, 0x2, 0x37, 0xE0, 0x54, 0x1F, 0x90, 0xE0, 0x23, 0xF0, 0x90, 0x3, 0x37, 0xF1, 0xD8, 0x90, 0xE0, 0x24, 0xF0, 0x80, 0x3, 0x75, 0x11, 0x1, 0x90, 0xE0, 0x20, 0xE5, 0x11, 0xF0, 0x22, 0x7B, 0x1, 0x7A, 0xE0, 0x79, 0xA6, 0x80, 0x6, 0x7B, 0x1, 0x7A, 0xE0, 0x79, 0xAE, 0xD2, 0x2, 0xF1, 0x89, 0xF1, 0xF7, 0x50, 0xFC, 0xE4, 0xFD, 0xAF, 0x75, 0x2, 0x32, 0xF4, 0xE5, 0x75, 0x70, 0x14, 0x90, 0x4, 0x34, 0xF1, 0xBE, 0x54, 0xFE, 0xF0, 0x30, 0x2, 0x1C, 0x90, 0x4, 0x34, 0xF1, 0xB5, 0x44, 0x1, 0xF0, 0x22, 0x90, 0x4, 0x46, 0xF1, 0xBE, 0x54, 0xFD, 0xF0, 0x30, 0x2, 0x8, 0x90, 0x4, 0x46, 0xF1, 0xB5, 0x44, 0x2, 0xF0, 0x22, 0xE0, 0x44, 0x1, 0xF0, 0x90, 0x4, 0x50, 0xE0, 0x22, 0xE0, 0x54, 0xFE, 0xF0, 0x90, 0x4, 0x50, 0xE0, 0x22, 0xEF, 0x54, 0x1, 0xC4, 0x54, 0xF0, 0xFF, 0x22, 0x54, 0x3, 0xFF, 0xE5, 0x10, 0x75, 0xF0, 0x16, 0x22, 0xE0, 0x54, 0x1F, 0xFF, 0xC3, 0x74, 0x12, 0x9F, 0x22, 0xE0, 0x54, 0xFE, 0x4E, 0xF0, 0xEC, 0x7F, 0x0, 0x22, 0x90, 0xE0, 0xA6, 0x12, 0x2, 0x60, 0x90, 0xE0, 0xAE, 0x22, 0xAF, 0x76, 0x22, 0xE5, 0x75, 0x90, 0x4, 0x51, 0x70, 0x3, 0xE0, 0x13, 0x22, 0xE0, 0xC3, 0x13, 0x13, 0x22, 0x12, 0x7, 0xA4, 0x12, 0xE3, 0xD4, 0x2, 0x7, 0x88, 0xE5, 0x2D, 0x54, 0xFC, 0x44, 0x1, 0xF5, 0x2D, 0x75, 0x30, 0xE0, 0x75, 0x31, 0xFB, 0x43, 0x2D, 0xC, 0x75, 0x32, 0xE8, 0x75, 0x33, 0x6, 0x22, 0x90, 0x4, 0x5C, 0xE0, 0xFD, 0xA3, 0xE0, 0xFE, 0xAF, 0x5, 0x22, 0x8E, 0x1B, 0x8F, 0x1C, 0x51, 0xAC, 0x44, 0x4, 0x51, 0xB3, 0x85, 0x1C, 0x82, 0x8E, 0x83, 0xE0, 0xC4, 0x54, 0xF0, 0x44, 0x4, 0x54, 0xFE, 0x90, 0x4, 0x57, 0xF0, 0xA3, 0xE0, 0xE4, 0xF0, 0xA3, 0xE0, 0xE4, 0xF0, 0x85, 0x1C, 0x82, 0x8E, 0x83, 0x11, 0xE6, 0xE0, 0x75, 0xF0, 0x40, 0xA4, 0xFF, 0xAE, 0xF0, 0xE4, 0xFC, 0xFD, 0x7B, 0xC, 0xFA, 0xF9, 0xF8, 0x12, 0x1, 0x9C, 0xC2, 0x26, 0x12, 0x13, 0x51, 0x51, 0xCF, 0x90, 0x4, 0x5B, 0xE0, 0x20, 0xE0, 0x5, 0x12, 0x17, 0xF3, 0x50, 0xF4, 0x12, 0x10, 0x16, 0x90, 0x4, 0x5B, 0xE0, 0x30, 0xE0, 0x51, 0x11, 0x27, 0xAD, 0x7, 0xAC, 0x6, 0x85, 0x1C, 0x82, 0x85, 0x1B, 0x83, 0xE0, 0x25, 0xE0, 0x24, 0x3, 0x12, 0xF0, 0x2, 0x80, 0x5, 0xC3, 0x33, 0xCE, 0x33, 0xCE, 0xD8, 0xF9, 0xFF, 0xC3, 0xED, 0x9F, 0xEC, 0x9E, 0x50, 0xE, 0xC3, 0xEF, 0x9D, 0xFD, 0xEE, 0x9C, 0x11, 0xE5, 0xA3, 0x74, 0x1, 0xF0, 0x80, 0xB, 0xC3, 0xED, 0x9F, 0xFD, 0xEC, 0x9E, 0x11, 0xE5, 0xA3, 0xE4, 0xF0, 0xE5, 0x1C, 0x24, 0x4, 0x31, 0xDB, 0xEC, 0xF0, 0xE5, 0x1C, 0x24, 0x5, 0x31, 0xDB, 0xED, 0xF0, 0x7F, 0x1, 0x22, 0x7F, 0x50, 0x12, 0x1F, 0xA8, 0x7F, 0x0, 0x22, 0xFC, 0x85, 0x1C, 0x82, 0x85, 0x1B, 0x83, 0xA3, 0xA3, 0x22, 0x75, 0x19, 0xF5, 0x75, 0x1A, 0x10, 0xE4, 0xF5, 0x22, 0xF5, 0x23, 0x12, 0xF7, 0x16, 0x54, 0xFE, 0x31, 0xD3, 0x74, 0x4, 0xF0, 0xA3, 0x74, 0x6, 0x31, 0xD3, 0xA3, 0xA3, 0x74, 0x40, 0xF0, 0xAF, 0x1A, 0xAE, 0x19, 0x11, 0x32, 0xEF, 0x64, 0x1, 0x70, 0x20, 0x31, 0xD4, 0xA3, 0xA3, 0xA3, 0xE0, 0x7E, 0x0, 0x60, 0x2, 0x7E, 0x1, 0x8E, 0x16, 0xE5, 0x1A, 0x24, 0x4, 0x31, 0xE3, 0xF5, 0x17, 0xE5, 0x1A, 0x24, 0x5, 0x31, 0xE3, 0xF5, 0x18, 0x80, 0x7, 0xE4, 0xF5, 0x16, 0xF5, 0x17, 0xF5, 0x18, 0x51, 0xAC, 0x44, 0x1, 0x51, 0xB3, 0xE5, 0x16, 0x33, 0x33, 0x33, 0x54, 0xF8, 0x44, 0x44, 0x54, 0xFE, 0xA3, 0xF0, 0xE5, 0x18, 0xFF, 0xA3, 0xE0, 0xEF, 0xF0, 0xE5, 0x17, 0xFF, 0xA3, 0xE0, 0xEF, 0xF0, 0xC2, 0x26, 0x7F, 0xAB, 0x7E, 0xF6, 0x7D, 0xFF, 0x7C, 0xFF, 0x51, 0xC1, 0x80, 0x3, 0xEF, 0x70, 0x5, 0x12, 0xF6, 0xCC, 0x80, 0xF8, 0xEF, 0x64, 0x1, 0x70, 0x50, 0x11, 0x27, 0xE4, 0x8F, 0x15, 0x8E, 0x14, 0xF5, 0x13, 0xF5, 0x12, 0xAF, 0x21, 0xEF, 0x33, 0x95, 0xE0, 0xFE, 0xFD, 0xFC, 0xEF, 0x25, 0x15, 0xFF, 0xEE, 0x35, 0x14, 0xFE, 0xED, 0x35, 0x13, 0xFD, 0xEC, 0x35, 0x12, 0xFC, 0xE4, 0x7B, 0x52, 0x7A, 0x3, 0xF9, 0xF8, 0x12, 0x1, 0x11, 0xE4, 0xFB, 0x7A, 0x10, 0xF9, 0xF8, 0x12, 0x1, 0x9C, 0xEF, 0x24, 0xA9, 0xFF, 0xEE, 0x34, 0x1, 0xFE, 0xE4, 0x3D, 0xFD, 0xE4, 0x3C, 0x8F, 0x15, 0x8E, 0x14, 0x8D, 0x13, 0xF5, 0x12, 0x8E, 0x22, 0x8F, 0x23, 0x12, 0xF6, 0xE6, 0x44, 0x1, 0xF0, 0x22, 0xF0, 0x85, 0x1A, 0x82, 0x85, 0x19, 0x83, 0x22, 0xF5, 0x82, 0xE4, 0x35, 0x1B, 0xF5, 0x83, 0x22, 0xF5, 0x82, 0xE4, 0x35, 0x19, 0xF5, 0x83, 0xE0, 0x22, 0x8F, 0x12, 0x75, 0x15, 0xF5, 0x75, 0x16, 0x10, 0xE5, 0x12, 0xAF, 0x12, 0x70, 0x2, 0x7F, 0x1, 0x8F, 0x12, 0xE4, 0x90, 0xE0, 0xA2, 0xF0, 0x12, 0xF7, 0x16, 0x54, 0xFD, 0x51, 0xA4, 0x74, 0x2, 0xF0, 0xA3, 0xE4, 0x51, 0xA4, 0xA3, 0xA3, 0x74, 0xB4, 0xF0, 0xAF, 0x16, 0xAE, 0x15, 0x11, 0x32, 0xAD, 0x7, 0xED, 0x64, 0x1, 0x70, 0x1E, 0x51, 0xA5, 0xA3, 0xA3, 0xA3, 0xE0, 0x7F, 0x0, 0x60, 0x2, 0x7F, 0x1, 0x8F, 0x13, 0xE5, 0x16, 0x24, 0x5, 0xF5, 0x82, 0xE4, 0x35, 0x15, 0xF5, 0x83, 0xE0, 0xF5, 0x14, 0x80, 0x5, 0xE4, 0xF5, 0x13, 0xF5, 0x14, 0x51, 0xAC, 0x51, 0xB3, 0xE5, 0x13, 0x33, 0x33, 0x33, 0x54, 0xF8, 0x44, 0x24, 0x54, 0xFE, 0xA3, 0xF0, 0xE5, 0x14, 0xFF, 0xA3, 0xE0, 0xEF, 0xF0, 0xA3, 0xE0, 0xE4, 0xF0, 0xE5, 0x12, 0x60, 0x37, 0xC2, 0x26, 0x7F, 0xC0, 0x7E, 0x3, 0x7D, 0x0, 0x7C, 0x0, 0x51, 0xC1, 0x80, 0x6, 0xED, 0x70, 0xA, 0x12, 0x7, 0xE8, 0x12, 0x2E, 0xA3, 0xAD, 0x7, 0x80, 0xF3, 0xED, 0x64, 0x1, 0x70, 0x13, 0xE5, 0x74, 0x90, 0x4, 0x5C, 0x70, 0x5, 0xE0, 0xF5, 0x74, 0x80, 0x7, 0xE0, 0x25, 0x74, 0x51, 0xE3, 0xF5, 0x74, 0x15, 0x12, 0x80, 0xC5, 0x12, 0xF6, 0xE6, 0x44, 0x2, 0xF0, 0x22, 0xF0, 0x85, 0x16, 0x82, 0x85, 0x15, 0x83, 0x22, 0x90, 0x4, 0x54, 0xE0, 0x54, 0xF8, 0x22, 0xF0, 0xA3, 0xE0, 0x54, 0xF8, 0x44, 0x2, 0xF0, 0xA3, 0xE0, 0x54, 0xF8, 0xF0, 0x22, 0x12, 0x13, 0x51, 0x90, 0x4, 0x57, 0xE0, 0x44, 0x1, 0xF0, 0xE0, 0x54, 0xFE, 0xF0, 0x90, 0x4, 0x5A, 0xE0, 0x54, 0xFE, 0xF0, 0xE0, 0x44, 0x1, 0xF0, 0x22, 0x90, 0xE0, 0xA4, 0xE0, 0xFF, 0xA3, 0xE0, 0x2F, 0xFF, 0xE4, 0x33, 0xC3, 0x13, 0xEF, 0x13, 0x22, 0x90, 0xE0, 0x7C, 0xE0, 0xFD, 0xA3, 0xE0, 0xFF, 0x90, 0xE0, 0x7F, 0xE0, 0xC3, 0x9F, 0x50, 0x4, 0xE0, 0x24, 0x80, 0xF0, 0x90, 0xE0, 0x7E, 0xE0, 0xFF, 0x90, 0xE0, 0x80, 0xE0, 0xC3, 0x9F, 0x50, 0x4, 0xE0, 0x24, 0x80, 0xF0, 0x90, 0xE0, 0x7D, 0xE0, 0xFF, 0xA3, 0xE0, 0xC3, 0x9F, 0x50, 0xB, 0xE0, 0x24, 0x80, 0xF0, 0x90, 0xE0, 0x80, 0xE0, 0x24, 0x80, 0xF0, 0xED, 0x71, 0xBA, 0x54, 0x3, 0xFF, 0x70, 0x1A, 0x90, 0xE0, 0x7E, 0xE0, 0xFE, 0x71, 0x98, 0xF5, 0x83, 0xEE, 0xF0, 0x90, 0xE0, 0x80, 0xE0, 0xFE, 0x71, 0xA5, 0x34, 0xE0, 0xF5, 0x83, 0xEE, 0xF0, 0x80, 0x3C, 0xBF, 0x1, 0xB, 0x90, 0xE0, 0x7F, 0xE0, 0x71, 0x97, 0x71, 0xB0, 0xE0, 0x80, 0x25, 0x90, 0xE0, 0x7E, 0xE0, 0xFF, 0xA3, 0xE0, 0xFB, 0xEF, 0x2B, 0x51, 0xE3, 0x71, 0x97, 0x71, 0xB0, 0xE0, 0x24, 0x80, 0xFF, 0xE4, 0x33, 0xFE, 0x90, 0xE0, 0x80, 0xE0, 0x7A, 0x0, 0x2F, 0xFF, 0xEA, 0x3E, 0xC3, 0x13, 0xEF, 0x13, 0xFF, 0x71, 0xA5, 0x34, 0xE0, 0xF5, 0x83, 0xEF, 0xF0, 0x71, 0x98, 0xF5, 0x83, 0xE0, 0x54, 0x7F, 0xF0, 0x71, 0xA5, 0x34, 0xE0, 0xF5, 0x83, 0xE0, 0x54, 0x7F, 0xF0, 0x22, 0xFF, 0xED, 0x75, 0xF0, 0x3, 0xA4, 0x24, 0x8A, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0x22, 0xED, 0x75, 0xF0, 0x3, 0xA4, 0x24, 0x8B, 0xF5, 0x82, 0xE4, 0x22, 0xF5, 0x83, 0xEF, 0xF0, 0x90, 0xE0, 0x7D, 0x22, 0xE5, 0x12, 0x75, 0xF0, 0x3, 0xA4, 0x24, 0x89, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE0, 0x22, 0x90, 0xE0, 0x7C, 0xE0, 0xF5, 0x12, 0xE5, 0x12, 0x7F, 0x0, 0xFE, 0xF1, 0xD1, 0x12, 0xE2, 0x4, 0x44, 0x1, 0xF0, 0x71, 0xB8, 0x54, 0x3, 0xF5, 0x13, 0x7E, 0x0, 0x7F, 0x20, 0x12, 0xF7, 0x39, 0x71, 0xB8, 0x54, 0xFC, 0x44, 0x1, 0xF1, 0x62, 0xB1, 0x1F, 0x7C, 0xE0, 0x7D, 0x7D, 0x7A, 0xE0, 0x7B, 0x7F, 0x7F, 0x80, 0x7E, 0xF7, 0x12, 0x38, 0xD4, 0x71, 0xB8, 0x54, 0xFC, 0xF1, 0x62, 0x7F, 0x90, 0x7E, 0xF7, 0xB1, 0x23, 0x7C, 0xE0, 0x7D, 0x7E, 0x7A, 0xE0, 0x7B, 0x80, 0x7F, 0x90, 0x7E, 0xF7, 0x12, 0x38, 0xD4, 0xE5, 0x13, 0x54, 0x3, 0xFF, 0x71, 0xB8, 0x54, 0xFC, 0x4F, 0xF0, 0x41, 0xEB, 0x90, 0xE0, 0xA3, 0xEC, 0xF0, 0xA3, 0xED, 0xD1, 0x10, 0xE0, 0x90, 0xE0, 0xA5, 0xD1, 0x10, 0xA3, 0xE0, 0x90, 0xE0, 0xA6, 0xD1, 0x10, 0xA3, 0xA3, 0xE0, 0x90, 0xE0, 0xA7, 0xF0, 0xEF, 0x24, 0x4, 0xB1, 0x17, 0xFD, 0x70, 0x6, 0x7C, 0x1, 0x7D, 0x1, 0x80, 0x0, 0x90, 0xE0, 0xA8, 0xED, 0xF0, 0xEF, 0x24, 0x5, 0xB1, 0x17, 0x90, 0xE0, 0xA9, 0xF0, 0xEF, 0x24, 0x6, 0xB1, 0x17, 0x90, 0xE0, 0xAA, 0xF0, 0xEF, 0x24, 0x7, 0xB1, 0x17, 0xFD, 0xC3, 0x94, 0x3, 0x40, 0x6, 0x7E, 0x0, 0x7F, 0x8, 0x80, 0x11, 0x74, 0x1, 0x7E, 0x0, 0xA8, 0x5, 0x8, 0x80, 0x5, 0xC3, 0x33, 0xCE, 0x33, 0xCE, 0xD8, 0xF9, 0xFF, 0x90, 0xE0, 0xAB, 0xEF, 0xF0, 0xC2, 0x0, 0x91, 0xF9, 0x90, 0xE0, 0x7C, 0xE0, 0x71, 0xBA, 0xFF, 0x13, 0x13, 0x54, 0x3F, 0x30, 0xE0, 0x2, 0x71, 0xC9, 0xF1, 0x4F, 0xF1, 0xD2, 0x12, 0xF7, 0x6E, 0x7E, 0x0, 0x7F, 0x80, 0x12, 0xF7, 0x39, 0x90, 0xE0, 0xA5, 0xE0, 0xFD, 0xA3, 0xE0, 0xFB, 0xA3, 0xE0, 0x90, 0xE0, 0xB0, 0xF0, 0x90, 0xE0, 0xA8, 0xE0, 0x90, 0xE0, 0xB1, 0xF0, 0x90, 0xE0, 0xA9, 0xE0, 0x90, 0xE0, 0xB2, 0xF0, 0x90, 0xE0, 0xAA, 0xE0, 0x90, 0xE0, 0xB3, 0xF0, 0x90, 0xE0, 0xAB, 0xE0, 0x90, 0xE0, 0xB4, 0xF0, 0xB1, 0x1F, 0xEF, 0x24, 0x80, 0xFF, 0xEE, 0x34, 0x8, 0xFE, 0x90, 0xE0, 0xA3, 0xF1, 0xE2, 0xEE, 0xF0, 0xA3, 0xEF, 0xF0, 0xD2, 0x0, 0x20, 0x0, 0xA, 0x90, 0xE0, 0x7C, 0xE0, 0x12, 0xF4, 0x86, 0x54, 0xDF, 0xF0, 0xF1, 0xCA, 0x25, 0xE0, 0xFD, 0x90, 0xE0, 0x7C, 0xE0, 0x12, 0xF0, 0xF2, 0x54, 0xFD, 0x4D, 0xF0, 0x22, 0xF5, 0x82, 0xE4, 0x3E, 0xF5, 0x83, 0xE0, 0x22, 0x7F, 0x80, 0x7E, 0xF7, 0x90, 0xE0, 0xAC, 0xEE, 0xF0, 0xA3, 0xEF, 0xF0, 0xA3, 0xED, 0xF0, 0xA3, 0xEB, 0xF0, 0x90, 0xE0, 0xB4, 0xE0, 0xFF, 0xC3, 0x74, 0x8, 0x9F, 0xA3, 0xF0, 0xA3, 0xF0, 0x90, 0xE0, 0xB1, 0xE0, 0x90, 0xE0, 0xB8, 0xF0, 0x90, 0xE0, 0xB2, 0xE0, 0xFF, 0x90, 0xE0, 0xB8, 0xE0, 0xFE, 0xD3, 0x9F, 0x40, 0x2, 0xA1, 0xFE, 0xEE, 0x54, 0x7F, 0xF1, 0x4E, 0x24, 0xD, 0xF5, 0x82, 0x74, 0x0, 0x12, 0xE2, 0x4, 0x4D, 0xF0, 0x90, 0xE0, 0xAE, 0xE0, 0x90, 0xE0, 0xB7, 0xF0, 0x90, 0xE0, 0xAF, 0xE0, 0xFF, 0x90, 0xE0, 0xB7, 0xE0, 0xFE, 0xD3, 0x9F, 0x50, 0x6C, 0xEE, 0x54, 0x7F, 0xFD, 0x90, 0xE0, 0x7C, 0x12, 0xE1, 0xF9, 0x4D, 0xF0, 0x90, 0xE0, 0xB4, 0xE0, 0xFF, 0xD1, 0x1F, 0xEF, 0x60, 0x20, 0x90, 0xE0, 0xAC, 0xF1, 0xE2, 0xC0, 0x83, 0xC0, 0x82, 0xE0, 0xFE, 0x90, 0xE0, 0xB6, 0xE0, 0xFD, 0xEF, 0xA8, 0x5, 0x8, 0x80, 0x2, 0xC3, 0x33, 0xD8, 0xFC, 0x4E, 0xD0, 0x82, 0xD0, 0x83, 0xF0, 0x90, 0xE0, 0xB6, 0xE0, 0xFF, 0x90, 0xE0, 0xB4, 0xE0, 0xFE, 0xC3, 0xEF, 0x9E, 0x90, 0xE0, 0xB6, 0xF0, 0xC3, 0x64, 0x80, 0x94, 0x80, 0x50, 0x11, 0x90, 0xE0, 0xB5, 0xE0, 0xA3, 0xF0, 0xF1, 0x58, 0xD1, 0x17, 0x70, 0x3, 0xEE, 0x64, 0xF8, 0x60, 0xD, 0x90, 0xE0, 0xB0, 0xE0, 0xFF, 0x90, 0xE0, 0xB7, 0xE0, 0x2F, 0xF0, 0x80, 0x86, 0xD1, 0x17, 0x70, 0x3, 0xEE, 0x64, 0xF8, 0x60, 0xD, 0x90, 0xE0, 0xB3, 0xE0, 0xFF, 0x90, 0xE0, 0xB8, 0xE0, 0x2F, 0xF0, 0xA1, 0x46, 0x90, 0xE0, 0xB6, 0xE0, 0xFF, 0x90, 0xE0, 0xB5, 0xE0, 0x6F, 0x60, 0x2, 0xF1, 0x58, 0xD1, 0x17, 0xFF, 0x22, 0xF0, 0x8F, 0x82, 0x8E, 0x83, 0xA3, 0x22, 0x90, 0xE0, 0xAC, 0xE0, 0xFE, 0xA3, 0xE0, 0x22, 0x90, 0xE0, 0xB9, 0xEF, 0xF0, 0x90, 0xE0, 0x7C, 0xE0, 0xFF, 0x90, 0xE0, 0xBA, 0xF0, 0xE4, 0xA3, 0xF0, 0xA3, 0xF0, 0x12, 0x34, 0xC0, 0x12, 0xF7, 0x1, 0xF9, 0x12, 0xF7, 0x2A, 0xFD, 0x90, 0xE0, 0xBB, 0xE4, 0xF0, 0xA3, 0xED, 0xF0, 0x12, 0xF7, 0x44, 0xFE, 0x90, 0xE0, 0xBB, 0xE0, 0x4E, 0xF0, 0xA3, 0xE0, 0xF0, 0x90, 0xE0, 0xBB, 0xE0, 0x70, 0x2, 0xA3, 0xE0, 0x60, 0x52, 0xE9, 0x71, 0xBA, 0x54, 0x3, 0x64, 0x2, 0x70, 0x49, 0xD2, 0x0, 0xF1, 0x7A, 0x12, 0xF7, 0x1, 0x12, 0xF7, 0x2A, 0xFF, 0xE4, 0xFC, 0xFD, 0xFE, 0x12, 0xF7, 0x5A, 0x12, 0x2, 0x6C, 0x90, 0xE0, 0xBA, 0xE0, 0x7F, 0x0, 0xFE, 0x12, 0xF7, 0x44, 0x7F, 0x0, 0xFE, 0xE4, 0xFC, 0xFD, 0x12, 0xE0, 0xCC, 0x90, 0xE0, 0xC1, 0x12, 0x2, 0x78, 0xF1, 0x46, 0xFF, 0xE4, 0xFC, 0xFD, 0x90, 0xE0, 0xC1, 0x12, 0xE2, 0xEE, 0x40, 0x5, 0xF1, 0x46, 0xFF, 0x80, 0x6, 0x90, 0xE0, 0xC1, 0x12, 0x2, 0x60, 0xF1, 0xEB, 0x90, 0xE0, 0xB9, 0xE0, 0xFF, 0xB4, 0x1, 0x21, 0xD3, 0x90, 0xE0, 0xBC, 0xE0, 0x94, 0x80, 0x90, 0xE0, 0xBB, 0xE0, 0x94, 0x0, 0x7C, 0x0, 0x40, 0x4, 0x7D, 0x1, 0x80, 0x2, 0x7D, 0x0, 0x90, 0xE0, 0xBB, 0xEC, 0xF0, 0xA3, 0xED, 0xF0, 0x80, 0x6B, 0xEF, 0x64, 0x10, 0x60, 0x66, 0x90, 0xE0, 0xB9, 0xE0, 0x12, 0xF0, 0x2, 0x80, 0x5, 0xC3, 0x33, 0xCE, 0x33, 0xCE, 0xD8, 0xF9, 0x24, 0xFF, 0xFF, 0xEE, 0x34, 0xFF, 0x90, 0xE0, 0xBD, 0xF0, 0xFC, 0xA3, 0xEF, 0xF0, 0xFD, 0x7E, 0xFF, 0x7F, 0xFF, 0x12, 0x0, 0x7A, 0xA3, 0xEE, 0xF0, 0xA3, 0xEF, 0xF0, 0xF1, 0x46, 0xFB, 0xAA, 0x6, 0xE4, 0xF9, 0xF8, 0x90, 0xE0, 0xBF, 0xF1, 0xD9, 0x12, 0x1, 0x4, 0xEF, 0x24, 0xFF, 0xFB, 0xEE, 0x34, 0xFF, 0xFA, 0xED, 0x34, 0xFF, 0xF9, 0xEC, 0x34, 0xFF, 0xF8, 0x90, 0xE0, 0xBD, 0xF1, 0xD9, 0x12, 0x1, 0x11, 0xE4, 0x7B, 0xFF, 0x7A, 0xFF, 0xF9, 0xF8, 0x12, 0x1, 0x9C, 0x12, 0xF7, 0x5A, 0x12, 0x2, 0x60, 0xF1, 0xEB, 0xF1, 0x46, 0xFF, 0x22, 0x90, 0xE0, 0xBB, 0xE0, 0xFE, 0xA3, 0xE0, 0x22, 0xFD, 0x90, 0xE0, 0x7C, 0xE0, 0x7F, 0x0, 0xFE, 0xEF, 0x22, 0x90, 0xE0, 0xAC, 0xE4, 0x75, 0xF0, 0x1, 0x2, 0x0, 0xCF, 0xF0, 0x90, 0xE0, 0xB0, 0x74, 0x1, 0xF0, 0xA3, 0x74, 0x40, 0xF0, 0xA3, 0xF0, 0xA3, 0x74, 0x1, 0xF0, 0xA3, 0xF0, 0x7B, 0x7F, 0xE4, 0xFD, 0x22, 0xF1, 0xCA, 0xF1, 0x4E, 0x24, 0xE, 0xF5, 0x82, 0x74, 0x0, 0x12, 0xF4, 0xB0, 0x4D, 0xF0, 0x90, 0x4, 0x19, 0xE0, 0x54, 0x3, 0x70, 0x12, 0x7D, 0x0, 0x30, 0x0, 0x2, 0x7D, 0x4, 0xED, 0x54, 0x7, 0x25, 0xE0, 0xFD, 0x74, 0xF, 0x2F, 0x80, 0x1A, 0x90, 0x4, 0x19, 0xE0, 0x54, 0x3, 0x64, 0x1, 0x70, 0x1C, 0x7F, 0x0, 0x20, 0x0, 0x2, 0x7F, 0x4, 0xEF, 0x54, 0x7, 0x25, 0xE0, 0xF1, 0x4E, 0x24, 0xF, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x54, 0xF1, 0x4D, 0xF0, 0x22, 0xA2, 0x0, 0xE4, 0x33, 0x54, 0x1, 0x22, 0xEF, 0x24, 0x16, 0xF5, 0x82, 0x74, 0x0, 0x22, 0xE0, 0xFE, 0xA3, 0xE0, 0xFF, 0xE4, 0xFC, 0xFD, 0x22, 0xE0, 0xFC, 0xA3, 0xE0, 0xF5, 0x82, 0x8C, 0x83, 0x22, 0x90, 0xE0, 0xBB, 0xEE, 0xF0, 0xA3, 0xEF, 0xF0, 0x22, 0x54, 0xEF, 0xF0, 0xE0, 0x54, 0xDF, 0xF0, 0x22, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xFF, 0x74, 0x1, 0x7E, 0x0, 0xA8, 0x7, 0x8, 0x22, 0xAB, 0x74, 0x90, 0xE0, 0xD9, 0xE4, 0x93, 0xC3, 0x13, 0xFF, 0xEB, 0xC3, 0x9F, 0x40, 0x6F, 0x90, 0xE0, 0xF9, 0xE4, 0x93, 0xFF, 0xE4, 0x13, 0xFE, 0xEF, 0x13, 0xFF, 0x7C, 0x0, 0x7D, 0x3, 0x12, 0x0, 0x68, 0xEB, 0xD3, 0x9F, 0xEC, 0x9E, 0x50, 0x55, 0x7C, 0x10, 0xE4, 0xFE, 0xEE, 0x25, 0xE0, 0x24, 0xD9, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE4, 0x93, 0xFD, 0xEB, 0xD3, 0x9D, 0x50, 0x4, 0xAC, 0x6, 0x80, 0x4, 0xE, 0xBE, 0x11, 0xE4, 0xEC, 0x75, 0xF0, 0x9, 0xA4, 0x24, 0x27, 0xF5, 0x82, 0xE4, 0x34, 0x5, 0xF5, 0x83, 0xE4, 0x93, 0x90, 0xE0, 0xA2, 0xF0, 0xEC, 0x25, 0xE0, 0x24, 0xDA, 0xF5, 0x82, 0xE4, 0x34, 0xE0, 0xF5, 0x83, 0xE4, 0x93, 0x54, 0xF, 0xFE, 0x90, 0x4, 0x20, 0xE0, 0x54, 0xF0, 0x4E, 0xF0, 0x90, 0x4, 0x1E, 0xE0, 0x54, 0xF0, 0x4E, 0xF0, 0x22, 0x90, 0xE0, 0x89, 0x74, 0x7, 0xF0, 0x90, 0xE0, 0x8C, 0xF0, 0x90, 0xE0, 0x8F, 0xF0, 0x90, 0xE0, 0x92, 0xF0, 0x22, 0x12, 0xE3, 0x9D, 0xC3, 0x13, 0x54, 0x1, 0x4F, 0xFD, 0x20, 0x0, 0x25, 0x91, 0x86, 0x54, 0xDF, 0x11, 0xF0, 0x54, 0xBF, 0xF0, 0xAF, 0x5, 0xEF, 0x7F, 0x0, 0xFE, 0x31, 0x48, 0x54, 0xEF, 0xF0, 0xE5, 0x75, 0x90, 0x4, 0x7A, 0x70, 0x6, 0xE0, 0x54, 0xFD, 0xF0, 0x80, 0x4, 0xE0, 0x54, 0xEF, 0xF0, 0xE5, 0x75, 0x70, 0xF, 0xA2, 0x0, 0x33, 0x54, 0x1, 0xFF, 0x90, 0x4, 0x7A, 0xE0, 0x54, 0xFE, 0x4F, 0xF0, 0x22, 0xA2, 0x0, 0xE4, 0x33, 0x91, 0x52, 0x90, 0x4, 0x7A, 0xE0, 0x54, 0xF7, 0x4F, 0xF0, 0x22, 0xF0, 0xED, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x3F, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x22, 0xAD, 0x7, 0xED, 0x7F, 0x0, 0xFE, 0xEF, 0x30, 0x1, 0xC, 0x11, 0xF6, 0x54, 0xBF, 0xF0, 0x91, 0x89, 0x54, 0xDF, 0xF0, 0x80, 0x5, 0x31, 0x49, 0x54, 0xEF, 0xF0, 0xED, 0xC3, 0x94, 0x2, 0xED, 0x50, 0x15, 0x54, 0x1, 0xFC, 0x54, 0x1, 0xFF, 0x90, 0x4, 0x2C, 0x71, 0xB9, 0x91, 0x5B, 0x54, 0xFD, 0xF0, 0xE0, 0x54, 0xFB, 0xF0, 0x22, 0x54, 0x1, 0xFC, 0x54, 0x1, 0xFF, 0x90, 0x4, 0x3E, 0x71, 0xB9, 0x91, 0x5B, 0x2, 0xEF, 0xF4, 0xEF, 0x24, 0x42, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x22, 0xD2, 0x0, 0x8F, 0x1E, 0xAF, 0x1E, 0xEF, 0x13, 0x13, 0x54, 0x7, 0x14, 0x60, 0x21, 0x14, 0x60, 0x26, 0x14, 0x60, 0x2D, 0x14, 0x60, 0x2C, 0x24, 0x4, 0x70, 0x39, 0x51, 0x68, 0x31, 0x1, 0x31, 0xF1, 0x7F, 0x2, 0x71, 0x12, 0xC2, 0x1, 0x71, 0xD6, 0xE4, 0xFF, 0x71, 0x43, 0x80, 0x27, 0x51, 0x68, 0x31, 0x1, 0x31, 0xF1, 0x80, 0x17, 0x51, 0x68, 0x31, 0x1, 0x31, 0xF1, 0x7F, 0x2, 0x80, 0xF, 0xC2, 0x0, 0xE5, 0x1E, 0x54, 0x3, 0xFF, 0xC2, 0x1, 0x31, 0x1, 0x91, 0x65, 0xE4, 0xFF, 0x71, 0x12, 0x80, 0x2, 0xC3, 0x22, 0xE5, 0x1E, 0x54, 0x3, 0xC3, 0x94, 0x2, 0x50, 0x4, 0x7F, 0x1, 0x80, 0x2, 0xE4, 0xFF, 0x71, 0x78, 0xD3, 0x22, 0xAC, 0x7, 0x90, 0x4, 0x8E, 0xE0, 0x70, 0x4, 0x71, 0x4, 0x80, 0x22, 0xEC, 0x70, 0x4, 0x7E, 0x8, 0x80, 0x3, 0xEC, 0x14, 0xFE, 0xEE, 0x54, 0xF, 0xFE, 0x90, 0x4, 0x1C, 0xE0, 0x54, 0xF0, 0x4E, 0xF0, 0xEC, 0x7F, 0x0, 0x70, 0x2, 0x7F, 0x1, 0x91, 0x51, 0x71, 0xB, 0x4F, 0xF0, 0xE0, 0x54, 0xEF, 0xF0, 0x22, 0xAF, 0x1E, 0xEF, 0xC4, 0x13, 0x54, 0x7, 0xFF, 0x31, 0xBD, 0xC2, 0x1, 0x22, 0xE4, 0xFD, 0xED, 0x91, 0xA6, 0x91, 0x9D, 0x12, 0xE2, 0x4, 0x44, 0x18, 0x11, 0xF0, 0x54, 0xEF, 0xF0, 0x11, 0xF5, 0x44, 0x40, 0xF0, 0xED, 0x91, 0x86, 0x44, 0x20, 0xF0, 0x31, 0x48, 0x44, 0x10, 0xF0, 0xD, 0xBD, 0x4, 0xDD, 0x90, 0x4, 0x1C, 0xE0, 0x54, 0xF0, 0x44, 0x4, 0xF0, 0xE0, 0x12, 0xEF, 0xF4, 0xE0, 0x54, 0xBF, 0xF0, 0xA3, 0xE0, 0x54, 0xEF, 0xF0, 0x90, 0x4, 0x1F, 0xE0, 0x54, 0xEF, 0x91, 0x95, 0x44, 0x40, 0xF0, 0x90, 0x4, 0x2C, 0x91, 0x7A, 0xF0, 0x90, 0x4, 0x3E, 0x91, 0x7A, 0xF0, 0x90, 0x4, 0x77, 0x51, 0x70, 0xE0, 0x44, 0x40, 0xF0, 0xA3, 0x51, 0x70, 0x90, 0x4, 0x7A, 0xE0, 0x44, 0x2, 0xF0, 0xE0, 0x44, 0x4, 0x80, 0x17, 0xE5, 0x1E, 0x54, 0x3, 0xFF, 0xD2, 0x1, 0x22, 0xE0, 0x44, 0x1, 0xF0, 0xE0, 0x44, 0x2, 0xF0, 0xE0, 0x44, 0x4, 0xF0, 0xE0, 0x44, 0x8, 0xF0, 0xE0, 0x44, 0x10, 0xF0, 0xE0, 0x44, 0x20, 0xF0, 0x22, 0x8F, 0x12, 0xD2, 0x0, 0x85, 0x12, 0x1D, 0xE5, 0x12, 0x12, 0x3, 0x11, 0xF2, 0xBA, 0x1, 0xF2, 0xBA, 0x2, 0xF2, 0xBA, 0x3, 0xF2, 0xC1, 0x4, 0xF2, 0xC1, 0x5, 0xF2, 0xD5, 0x6, 0xF2, 0xD7, 0x7, 0xF2, 0xE4, 0x8, 0xF2, 0xE6, 0x9, 0xF2, 0xEE, 0xA, 0xF2, 0xF0, 0xB, 0x0, 0x0, 0xF3, 0x0, 0xAF, 0x12, 0x12, 0x8, 0x48, 0x80, 0x41, 0xAF, 0x12, 0x12, 0x8, 0x48, 0x90, 0x4, 0x8E, 0xE0, 0x70, 0x36, 0x71, 0x4, 0xF0, 0xE0, 0x54, 0xEF, 0xF0, 0x80, 0x2D, 0xC2, 0x0, 0xA2, 0x0, 0x92, 0x1, 0x7F, 0x1, 0x71, 0x74, 0xE4, 0x31, 0xF8, 0x80, 0x16, 0xC2, 0x0, 0xA2, 0x0, 0x92, 0x1, 0x7F, 0x1, 0x80, 0xE, 0xC2, 0x0, 0xD2, 0x1, 0x71, 0xD6, 0x7F, 0x2, 0x71, 0x74, 0x91, 0x65, 0x7F, 0x2, 0x71, 0x12, 0x80, 0x2, 0xC3, 0x22, 0xD3, 0x22, 0x90, 0x4, 0x1C, 0xE0, 0x54, 0xF0, 0xF0, 0x90, 0x4, 0x78, 0xE0, 0x54, 0xF7, 0x22, 0x91, 0x6E, 0xFE, 0x90, 0x4, 0x1D, 0xE0, 0x54, 0xEF, 0x4E, 0xF0, 0x7F, 0x0, 0xBC, 0x1, 0x2, 0x7F, 0x1, 0xEF, 0x54, 0x1, 0xFF, 0x90, 0x4, 0x77, 0xE0, 0x54, 0xFE, 0x4F, 0xF0, 0xEC, 0x7F, 0x0, 0x70, 0x2, 0x7F, 0x1, 0x91, 0x49, 0xFF, 0x90, 0x4, 0x78, 0xE0, 0x54, 0xDF, 0x4F, 0xF0, 0x22, 0x91, 0x6E, 0xFE, 0x90, 0x4, 0x1F, 0xE0, 0x54, 0xEF, 0x4E, 0xF0, 0xEC, 0x7F, 0x0, 0x70, 0x2, 0x7F, 0x1, 0xEF, 0x71, 0xCD, 0x90, 0x4, 0x77, 0xE0, 0x54, 0xBF, 0x4F, 0xF0, 0x7F, 0x0, 0xBC, 0x1, 0x2, 0x7F, 0x1, 0xEF, 0x54, 0x1, 0xFF, 0x90, 0x4, 0x78, 0xE0, 0x54, 0xFE, 0x4F, 0xF0, 0x22, 0x71, 0x43, 0x7F, 0x2, 0xAC, 0x7, 0x7E, 0x0, 0xBC, 0x1, 0x2, 0x7E, 0x1, 0xEE, 0x91, 0x4A, 0xFE, 0x90, 0x4, 0x1C, 0xE0, 0x54, 0xDF, 0x4E, 0xF0, 0xEC, 0x7F, 0x0, 0x70, 0x2, 0x7F, 0x1, 0xEF, 0x71, 0xCD, 0x90, 0x4, 0x1C, 0xE0, 0x54, 0xBF, 0x4F, 0xF0, 0xEC, 0x7F, 0x0, 0x70, 0x2, 0x7F, 0x1, 0xEF, 0x54, 0x1, 0x25, 0xE0, 0xFF, 0x90, 0x4, 0x78, 0xE0, 0x54, 0xFD, 0x4F, 0xF0, 0xE0, 0x54, 0xFB, 0xF0, 0x22, 0xE0, 0x54, 0xFE, 0x4F, 0xF0, 0xEC, 0x54, 0x3, 0x25, 0xE0, 0xFF, 0xE0, 0x54, 0xF9, 0x4F, 0xF0, 0xA2, 0x1, 0xE4, 0x33, 0x54, 0x1, 0xC4, 0x33, 0x33, 0x54, 0xC0, 0xFF, 0x22, 0xE4, 0xF5, 0x14, 0x71, 0xDE, 0xC2, 0x1, 0x22, 0x91, 0x70, 0xFF, 0x90, 0x4, 0x1C, 0xE0, 0x54, 0xEF, 0x4F, 0xF0, 0x90, 0x4, 0x77, 0xE0, 0x54, 0xFD, 0xF0, 0xE5, 0x14, 0x7F, 0x1, 0x70, 0x1, 0xFF, 0xEF, 0x54, 0x1, 0x25, 0xE0, 0x25, 0xE0, 0xFF, 0x90, 0x4, 0x77, 0xE0, 0x54, 0xFB, 0x4F, 0xF0, 0xE5, 0x14, 0xD3, 0x94, 0x1, 0x7F, 0x1, 0x50, 0x2, 0x7F, 0x0, 0x91, 0x51, 0x90, 0x4, 0x77, 0xE0, 0x54, 0xF7, 0x4F, 0xF0, 0xE5, 0x14, 0xD3, 0x94, 0x2, 0x7F, 0x1, 0x50, 0x2, 0x7F, 0x0, 0x12, 0xE7, 0xC7, 0x90, 0x4, 0x77, 0xE0, 0x54, 0xEF, 0x4F, 0xF0, 0xE5, 0x14, 0xD3, 0x94, 0x3, 0x7F, 0x1, 0x50, 0x2, 0x7F, 0x0, 0x91, 0x49, 0xFF, 0x90, 0x4, 0x77, 0xE0, 0x54, 0xDF, 0x4F, 0xF0, 0x22, 0xEF, 0x54, 0x1, 0xC4, 0x33, 0x54, 0xE0, 0x22, 0xEF, 0x54, 0x1, 0x33, 0x33, 0x33, 0x54, 0xF8, 0xFF, 0x22, 0xE0, 0x54, 0xBF, 0x4F, 0xF0, 0x90, 0x4, 0x7A, 0xE0, 0x22, 0xE4, 0xFF, 0x31, 0xBD, 0xA2, 0x0, 0x92, 0x1, 0x22, 0xAC, 0x7, 0xA2, 0x1, 0xE4, 0x33, 0x54, 0x1, 0xC4, 0x54, 0xF0, 0x22, 0xE0, 0x54, 0xFE, 0xF0, 0xE0, 0x54, 0xF9, 0xF0, 0xE0, 0x54, 0xBF, 0x22, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x41, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x22, 0xF0, 0x90, 0x4, 0x23, 0xE0, 0x54, 0xF, 0x22, 0xF0, 0xEF, 0x24, 0x3E, 0xF5, 0x82, 0x74, 0x0, 0x22, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x9, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x54, 0xFE, 0x22, 0xEF, 0xC3, 0x60, 0x1, 0xD3, 0x92, 0x3, 0xE4, 0xF5, 0x77, 0x7F, 0x3, 0x30, 0x3, 0x2, 0x7F, 0x0, 0xEF, 0x54, 0x3, 0xFD, 0xE5, 0x77, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x6, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x54, 0xFC, 0x4D, 0xF0, 0x7D, 0x1, 0x30, 0x3, 0x2, 0x7D, 0x0, 0xED, 0x54, 0x1, 0xFD, 0x74, 0x8, 0x2F, 0xF5, 0x82, 0x74, 0x0, 0x91, 0xB0, 0x4D, 0xF0, 0x7D, 0x7F, 0xAF, 0x77, 0xD1, 0x5A, 0x5, 0x77, 0xE5, 0x77, 0xC3, 0x94, 0x4, 0x40, 0xBC, 0x22, 0xAF, 0x76, 0xEF, 0xC4, 0x13, 0x54, 0x7, 0x22, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x8F, 0x76, 0x90, 0x4, 0x79, 0xE0, 0x54, 0xFE, 0xF0, 0xE5, 0x76, 0x54, 0x2, 0xC3, 0x13, 0xFF, 0x90, 0x4, 0x24, 0xE0, 0x54, 0xFE, 0x4F, 0xF0, 0xE5, 0x76, 0x30, 0xE1, 0x15, 0x90, 0x4, 0x79, 0xE0, 0x54, 0xFB, 0xF0, 0xE5, 0x76, 0x54, 0x1C, 0x91, 0x54, 0x90, 0x4, 0x28, 0xE0, 0x54, 0x1F, 0x4F, 0xF0, 0xB1, 0x6, 0xFF, 0xD3, 0x94, 0x2, 0x50, 0x13, 0xEF, 0x4, 0x54, 0x3, 0xFF, 0x90, 0x4, 0x23, 0xE0, 0x54, 0xFC, 0x4F, 0xF0, 0xE0, 0x54, 0xF3, 0xF0, 0x80, 0x2D, 0xB1, 0x6, 0xFD, 0x90, 0x4, 0x79, 0xE0, 0xBD, 0x3, 0xF, 0x54, 0xFD, 0x91, 0x95, 0x44, 0x80, 0xF0, 0xE0, 0x54, 0xF3, 0x44, 0x8, 0xF0, 0x80, 0x14, 0x54, 0xFD, 0xF0, 0xED, 0x24, 0xFC, 0x54, 0xF, 0xC4, 0x54, 0xF0, 0xFF, 0x91, 0x96, 0x4F, 0xF0, 0xE0, 0x44, 0xC, 0xF0, 0xE5, 0x76, 0xC3, 0x30, 0xE0, 0x1, 0xD3, 0x92, 0x0, 0x12, 0xEF, 0xCA, 0xFF, 0xE5, 0x76, 0x54, 0xFE, 0x4F, 0xF5, 0x76, 0x30, 0xE0, 0x2B, 0x90, 0x4, 0x79, 0xE0, 0x54, 0xF7, 0xF0, 0x90, 0x4, 0x22, 0xE0, 0x44, 0x1, 0xF0, 0xC2, 0x26, 0x7F, 0x15, 0x7E, 0x2, 0x7D, 0x0, 0x7C, 0x0, 0x12, 0x13, 0x51, 0x12, 0x17, 0xF3, 0x50, 0xFB, 0x12, 0x10, 0x16, 0x90, 0x4, 0x22, 0xE0, 0x54, 0xFE, 0xF0, 0x22, 0x90, 0x4, 0x79, 0xE0, 0x44, 0x8, 0xF0, 0x22, 0x8F, 0x1F, 0xAF, 0x1F, 0xEF, 0xC4, 0x54, 0x7, 0x14, 0x60, 0xD, 0x4, 0x70, 0x13, 0xC2, 0x1, 0x7F, 0x1, 0x71, 0x12, 0xC2, 0x1, 0x80, 0x2, 0xD2, 0x1, 0x85, 0x12, 0x14, 0x71, 0xDE, 0x80, 0x2, 0xC3, 0x22, 0xE4, 0xF5, 0x12, 0x74, 0x1, 0x7E, 0x0, 0xA8, 0x12, 0x8, 0x80, 0x5, 0xC3, 0x33, 0xCE, 0x33, 0xCE, 0xD8, 0xF9, 0xD1, 0xC3, 0x70, 0x7, 0x5, 0x12, 0xE5, 0x12, 0xB4, 0x4, 0xE5, 0x5, 0x12, 0xE4, 0xF5, 0x13, 0x74, 0x1, 0x7E, 0x0, 0xA8, 0x13, 0x8, 0x80, 0x5, 0xC3, 0x33, 0xCE, 0x33, 0xCE, 0xD8, 0xF9, 0xD1, 0xC3, 0x60, 0x18, 0xE5, 0x1F, 0x54, 0x80, 0xC4, 0x13, 0x13, 0x13, 0x54, 0x1, 0xFD, 0xE5, 0x13, 0x91, 0xA6, 0x4D, 0x91, 0x9D, 0x12, 0xE2, 0x4, 0x44, 0x2, 0xF0, 0x5, 0x13, 0xE5, 0x13, 0xB4, 0x4, 0xCD, 0xD3, 0x22, 0xAC, 0x7, 0xEC, 0x30, 0xE0, 0x7, 0xC3, 0x74, 0xFF, 0x9D, 0xFF, 0x80, 0x2, 0xAF, 0x5, 0xEF, 0xFB, 0xEC, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x7, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0xEB, 0xF0, 0xED, 0xC3, 0x94, 0x78, 0x40, 0xC, 0xED, 0xD3, 0x94, 0x88, 0x50, 0x6, 0xD1, 0xB3, 0x44, 0x3, 0xF0, 0x22, 0xED, 0xC3, 0x94, 0x60, 0x40, 0x5, 0xED, 0x94, 0x78, 0x40, 0xC, 0xED, 0xD3, 0x94, 0x88, 0x40, 0xE, 0xED, 0xD3, 0x94, 0xA2, 0x50, 0x8, 0xD1, 0xB3, 0x54, 0xFC, 0x44, 0x1, 0xF0, 0x22, 0xD1, 0xB3, 0x54, 0xFC, 0xF0, 0x22, 0xEC, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0xC, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x22, 0xFF, 0xE5, 0x1F, 0x54, 0xF, 0xFD, 0xEF, 0x5D, 0x22, 0x90, 0x4, 0x5B, 0xE0, 0x30, 0xE0, 0x3, 0x7F, 0x1, 0x22, 0x12, 0x17, 0xF3, 0x50, 0x8, 0x7F, 0x40, 0x12, 0x1F, 0xA8, 0x7F, 0xFF, 0x22, 0x7F, 0x0, 0x22, 0x12, 0x10, 0x16, 0xA2, 0x0, 0xE4, 0x33, 0x54, 0x1, 0xFF, 0x90, 0x4, 0x76, 0xE0, 0x54, 0xFE, 0x4F, 0xF0, 0x90, 0x4, 0x7B, 0xE0, 0x44, 0x4, 0xF0, 0xE0, 0x22, 0x90, 0xE0, 0xBA, 0xE0, 0xFF, 0x12, 0x36, 0x79, 0x90, 0xE0, 0xBA, 0xE0, 0xFF, 0x12, 0x36, 0xBD, 0x90, 0xE0, 0xBA, 0xE0, 0x22, 0x90, 0x4, 0x76, 0xE0, 0x13, 0x92, 0x0, 0xE0, 0x54, 0xFE, 0xF0, 0x90, 0x4, 0x7B, 0xE0, 0x54, 0xFB, 0xF0, 0xE0, 0x22, 0x7F, 0x0, 0xFE, 0xEF, 0x24, 0x1B, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x22, 0x7D, 0x0, 0x7B, 0x1, 0x7A, 0xF7, 0x79, 0x80, 0x2, 0x3, 0x6A, 0xEF, 0x24, 0x1C, 0xF5, 0x82, 0x74, 0x0, 0x3E, 0xF5, 0x83, 0xE0, 0x22, 0x90, 0xE0, 0xB2, 0x12, 0x2, 0x60, 0x90, 0xE0, 0xAA, 0x22, 0x90, 0xE0, 0xC1, 0x12, 0x2, 0x78, 0x90, 0xE0, 0xC1, 0x22, 0xE0, 0xFF, 0xC3, 0x74, 0x12, 0x9F, 0x54, 0x1F, 0xFF, 0x22, 0x3E, 0xF5, 0x83, 0xE0, 0x44, 0x7F, 0xF0, 0x22, 0x0, 0x0 };

        public Algorithm algorithm = new Algorithm();
        public Inno25GBertED(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] Inno_25GBert_EDStruct)
        {
            int i = 0;

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "Addr", out i))
            {
                Addr = Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no Addr");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "IOTYPE", out i))
            {
                IOType = Inno_25GBert_EDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "DATARATE", out i))
            {
                dataRate = Inno_25GBert_EDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no DATARATE");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "EDGATINGTIME", out i))
            {
                edGatingTime = Convert.ToInt16(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no EDGATINGTIME");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "RESET", out i))
            {
                Reset = Convert.ToBoolean(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "NAME", out i))
            {
                Name = Inno_25GBert_EDStruct[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "EDPattern", out i))
            {
                EDPattern = (PrbsType)Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no EDPattern");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "EDInvert", out i))
            {
                EDInvert = (ED_Inverted)Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no EDInvert");
                return false;
            }

            DeviceIndex = Convert.ToByte(Addr);

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "TriggerOutputList".ToUpper(), out i))
            {
                TriggerOutputList = Inno_25GBert_EDStruct[i].DefaultValue.Split(',');
            }
            else
            {
                logger.AdapterLogString(4, "there is no TriggerOutputList");
                return false;
            }

            //if (algorithm.FindFileName(Inno_25GBert_EDStruct, "deviceIndex", out i))
            //{
            //    DeviceIndex = Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            //}
            //else
            //{
            //    logger.AdapterLogString(4, "there is no deviceIndex");
            //    return false;
            //}

            //if (algorithm.FindFileName(Inno_25GBert_EDStruct, "FCK", out i))
            //{
            //    FCK = (IOPort.CFKType)Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            //}
            //else
            //{
            //    logger.AdapterLogString(4, "there is no FCK");
            //    return false;
            //}

            if (!Connect())
            {
                return false;
            }

            return true;
        }
        public override bool Connect()
        {
            try
            {
                MyIO = new IOPort("USB", Addr.ToString(), logger);
                MyIO.IOConnect();
                EquipmentConnectflag = true;

                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        public bool ReSet()
        {
            return true;
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

                    IniTialize_1724();

                    ConfigureChannel(4, syn);

                    TriggerOutputSelect(0);

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
        public override double GetErrorRate(int syn=0)
        {
            double errratio = 0;
            byte bitRateSelect;
            byte requestType = 0; // 0=ErrorCount, 1=BitCount

            if (dataRate.Trim().ToUpper() == "25.78".ToUpper())
            {
                bitRateSelect = 0;
            }
            else
            {
                bitRateSelect = 1;
            }

            // FlowControl传入的channel范围为1~4，Driver内部需要处理为0~3，故需要做currentChannel - 1
            errratio = GetSingleChannelBER(DeviceIndex, requestType, 
                FCK, bitRateSelect, edGatingTime * 1000, Convert.ToByte(currEDChannel - 1));  
                
            return errratio;
        }
        //快速误码测试
        public override double RapidErrorRate(int syn = 0)
        {
            double errratio = 0;
            byte bitRateSelect;
            byte requestType = 0; // 0=ErrorCount, 1=BitCount

            if (dataRate.Trim().ToUpper() == "25.78".ToUpper())
            {
                bitRateSelect = 0;
            }
            else
            {
                bitRateSelect = 1;
            }

            // FlowControl传入的channel范围为1~4，Driver内部需要处理为0~3，故需要做currentChannel - 1
            errratio = GetSingleChannelBER(DeviceIndex, requestType,
                FCK, bitRateSelect, edGatingTime * 1000, Convert.ToByte(currEDChannel - 1));

            return errratio;
        }
        public override double[] RapidErrorRate_AllCH(int syn = 0)
        {
            byte bitRateSelect;
            double[] ErrRata;
            byte requestType = 0; // 0=ErrorCount, 1=BitCount

            if (dataRate.Trim().ToUpper() == "25.78".ToUpper())
            {
                bitRateSelect = 0;
            }
            else
            {
                bitRateSelect = 1;
            }

            ErrRata = GetAllChannelBER(DeviceIndex, requestType, FCK, bitRateSelect, edGatingTime * 1000);
           
            return ErrRata;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel">channel取值范围为1,2,3,4</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            try
            {
                currEDChannel = Convert.ToByte(TriggerOutputList[Convert.ToInt32(channel) - 1]);

                byte channelbyte = Convert.ToByte(channel);
                logger.AdapterLogString(0, "TriggerOutput channel is " + currEDChannel);
                currentChannel = channelbyte;
                return TriggerOutputSelect(currEDChannel - 1);
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }
        public override bool AutoAlaign(bool becenter)
        {
            return ClearAllChannelErrorCount(DeviceIndex);
        }

        #region 私有方法 // 150508
        /// <summary>
        /// 四通道并行累计误码，获取累计时间为accumTime的四个通道的BER
        /// </summary>
        /// <param name="DeviceSelect"></param>
        /// <param name="RequestType">0=ErrorCount, 1=BitCount</param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="bitRateSelect">0=25.78, 1=28</param>
        /// <param name="accumTime">累计误码时间，单位ms</accumTime>
        /// <returns></returns>
        private double[] GetAllChannelBER(byte DeviceSelect, byte RequestType,
            IOPort.CFKType FCK, byte bitRateSelect, int accumTime) // 150525
        {
            double[] BER = new double[4] { 1, 1, 1, 1 };
            double[] initialErrorCount = new double[4] { 0, 0, 0, 0 }; // 寄存器初始误码
            double[] accumErrorCount; // 累计误码
            double[] ErrorCount = new double[4] { 0, 0, 0, 0 };  // 累计实际误码
            bool[] flag = new bool[4] { false, false, false, false };
            double bitRate;

            try
            {
                // 所有通道寄存器清零
                ClearAllChannelErrorCount(DeviceSelect);

                for (int i = 0; i < 4; i++)
                {
                    initialErrorCount[i] = GetInitialErrorCount(DeviceSelect, RequestType, FCK, (byte)i);
                }

                accumErrorCount = GetAllChannelAccumErrorCount(DeviceSelect, RequestType, FCK, accumTime);

                for (int i = 0; i < 4; i++)
                {
                    ErrorCount[i] = accumErrorCount[i] - initialErrorCount[i];

                    bool tempFlag = false;
                    byte DeviceAddress;
                    byte Select1724;

                    if (i >= 2)
                    {
                        DeviceAddress = (byte)0x2C;
                        Select1724 = (byte)IC1724.IC1724_2;
                    }
                    else
                    {
                        DeviceAddress = (byte)0x30;
                        Select1724 = (byte)IC1724.IC1724_1;
                    }

                    byte dataLength = 1;
                    int registerAddress = 7;
                    byte[] dataToRead;

                    dataToRead = MyIO.USBI2CRead(DeviceAddress, registerAddress, dataLength,
                        FCK, DeviceSelect, Select1724, 1);

                    if (dataToRead[0] == 0x7F)
                    {
                        tempFlag = true;
                    }

                    bool LOSDetector;
                    bool LOLDetector;

                    LosLoLStatus(DeviceSelect, DeviceAddress, FCK, Select1724,
                        (byte)i, out LOSDetector, out LOLDetector);

                    if (LOLDetector && tempFlag)
                    {
                        flag[i] = true;
                    }

                    if (bitRateSelect == 0)
                    {
                        bitRate = 25.78 * 1000000;
                    }
                    else
                    {
                        bitRate = 28 * 1000000;
                    }

                    if (flag[i])
                    {
                        BER[i] = ErrorCount[i] / accumTime / bitRate;
                    }
                    else
                    {
                        byte errorChannel = Convert.ToByte(i + 1);
                        BER[i] = 1;
                        logger.AdapterLogString(3, "Channel " + errorChannel.ToString() + " Unlock!");
                        logger.FlushLogBuffer();
                        // MessageBox.Show("Channel " + errorChannel.ToString() + " Unlock!");
                    }
                }

                return BER;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return BER;
            }
        }
        /// <summary>
        /// 获取channel通道，累计时间为accumTime的BER
        /// </summary>
        /// <param name="DeviceSelect"></param>
        /// <param name="RequestType">0=ErrorCount, 1=BitCount</param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="bitRateSelect">0=25.78, 1=28</param>
        /// <param name="accumTime">累计误码时间，单位ms</param>
        /// <param name="channel">通道0,1,2,3</param>
        /// <returns></returns>
        private double GetSingleChannelBER(byte DeviceSelect, byte RequestType,
            IOPort.CFKType FCK, byte bitRateSelect, int accumTime, byte channel) // 150508 根据GT1724芯片手册修改
        {
            double initialErrorCount = 0; // 寄存器初始误码
            double accumErrorCount = 0; // 累计误码
            double ErrorCount = 0;  // 累计实际误码
            double BER = 1;
            double bitRate;

            try
            {
                // 所有通道寄存器清零
                ClearAllChannelErrorCount(DeviceSelect);
                initialErrorCount = GetInitialErrorCount(DeviceSelect, RequestType, FCK, channel);
                accumErrorCount = GetAccumErrorCount(DeviceSelect, RequestType, FCK, accumTime, channel);
                ErrorCount = accumErrorCount - initialErrorCount;

                bool tempFlag = false;
                byte DeviceAddress;
                byte Select1724;

                if (channel >= 2)
                {
                    DeviceAddress = (byte)0x2C;
                    Select1724 = (byte)IC1724.IC1724_2;
                }
                else
                {
                    DeviceAddress = (byte)0x30;
                    Select1724 = (byte)IC1724.IC1724_1;
                }

                byte dataLength = 1;
                int registerAddress = 7;
                byte[] dataToRead;

                dataToRead = MyIO.USBI2CRead(DeviceAddress, registerAddress, dataLength,
                    FCK, DeviceSelect, Select1724, 1);

                if (dataToRead[0] == 0x7F)
                {
                    tempFlag = true;
                }

                bool LOSDetector;
                bool LOLDetector;

                LosLoLStatus(DeviceSelect, DeviceAddress, FCK, Select1724,
                    channel, out LOSDetector, out LOLDetector);

                bool flag = false;

                if (LOLDetector && tempFlag)
                {
                    flag = true;
                }

                if (bitRateSelect == 0)
                {
                    bitRate = 25.78 * 1000000;
                }
                else
                {
                    bitRate = 28 * 1000000;
                }

                if (flag)
                {
                    BER = ErrorCount / accumTime / bitRate;
                }
                else
                {
                    byte errorChannel = Convert.ToByte(channel + 1);
                    BER = 1;
                    logger.AdapterLogString(3, "Channel " + errorChannel.ToString() + " Unlock!");
                    logger.FlushLogBuffer();
                    // MessageBox.Show("Channel " + errorChannel.ToString() + " Unlock!");                
                }

                return BER;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return BER;
            }
        }
        /// <summary>
        /// 获取某一通道，寄存器的初始误码,支持同时抓四个通道的误码
        /// </summary>
        /// <param name="DeviceSelect"></param>
        /// <param name="RequestType">0=ErrorCount, 1=BitCount</param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="channel">0~3</param>
        /// <returns>初始误码</returns>
        private double GetInitialErrorCount(byte DeviceSelect, byte RequestType,
            IOPort.CFKType FCK, byte channel) // 150508
        {
            byte PRBSCHKSelect = (byte)(channel % 2);
            byte DeviceAddress;
            byte Select1724;
            double initialErrorCount;
            byte Exponent = 0;
            double Mantissa = 0;

            try
            {
                if (channel >= 2)
                {
                    DeviceAddress = (byte)0x2C;
                    Select1724 = (byte)IC1724.IC1724_2;
                }
                else
                {
                    DeviceAddress = (byte)0x30;
                    Select1724 = (byte)IC1724.IC1724_1;
                }

                CheckerReading(DeviceSelect, PRBSCHKSelect, RequestType, DeviceAddress,
                    FCK, Select1724, out Exponent, out Mantissa, out initialErrorCount);

                return initialErrorCount;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return 0;
            }
        }
        private bool ConfigureChannel(int channel, int syn = 0)
        {
            try
            {
                if (syn == 0)
                {
                    RXSelect = (RX_Channel)channel;

                    EDSet();
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
        private bool loadMarco(int deviceAddr, IOPort.CFKType FCK, byte Select1724) // 150508
        {
            int Datalength = 0;

            int RegistAddr = 0;

            for (int i = 0; i < 364; i++)
            {
                Datalength = 16;
                RegistAddr = 0XFB0000 + i * 16;
                int[] WriteArray = new int[Datalength];

                for (int j = 0; j < Datalength; j++)
                {
                    WriteArray[j] = MarcoData[i * 16 + j];
                }
                MyIO.Regist_24Bit_GT174(DeviceIndex, 0, WriteArray, WriteArray.Length, RegistAddr, IOPort.ReadWrite.Write, Convert.ToByte(deviceAddr), IOPort.CFKType._100K, Select1724);
            }

            RegistAddr = 0XFB0000 + 364 * 16;
            int[] WriteArray1 = new int[2];

            for (int j = 0; j < 2; j++)
            {
                WriteArray1[j] = MarcoData[364 * 16 + j];
            }
            MyIO.Regist_24Bit_GT174(DeviceIndex, 0, WriteArray1, WriteArray1.Length, RegistAddr, IOPort.ReadWrite.Write, Convert.ToByte(deviceAddr), IOPort.CFKType._100K, Select1724);

            return true;
        }
        private void Config1724_1() // 150508
        {
            #region  Config 1724_1
            try
            {
                MyIO.IoLevelControl(DeviceIndex, 0, 5);
                Thread.Sleep(10);
                MyIO.IoLevelControl(DeviceIndex, 1, 5);
                Thread.Sleep(10);
                Thread.Sleep(100);

                //Int32[] KK = MyIO.Regist_16Bit_GT174(0, 0, null, 8, 0, IOPort.ReadWrite.Read, 0X30, IOPort.CFKType._100K, 0);

                loadMarco(0x30, IOPort.CFKType._100K, Convert.ToByte(IC1724.IC1724_1));//0==1724_1
                Thread.Sleep(100);
                PowerOn_All_Block(0x30, IOPort.CFKType._100K, IC1724.IC1724_1);//0==1724_1
                Int32[] ReadData1 = MyIO.Regist_16Bit_GT174(DeviceIndex, 0, null, 1, 7, IOPort.ReadWrite.Read, 0x30, IOPort.CFKType._100K, Convert.ToByte(IC1724.IC1724_1));

                if (ReadData1[0] == 0x7f)
                {
                    logger.AdapterLogString(1, "Gt1724_1 设置成功");
                    logger.FlushLogBuffer();
                }
                else
                {
                    logger.AdapterLogString(3, "Gt1724_1 设置失败");
                    logger.FlushLogBuffer();
                    // MessageBox.Show("Gt1724_1 设置失败");
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
            }
            #endregion
        }
        private void Config1724_2() // 150508
        {
            #region  Config 1724_2
            try
            {
                MyIO.IoLevelControl(DeviceIndex, 0, 6);
                Thread.Sleep(10);
                MyIO.IoLevelControl(DeviceIndex, 1, 6); 
                Thread.Sleep(10);
                Thread.Sleep(100);
                //// Int32[] KK = MyIO.Regist_16Bit_GT174(0, 0, null, 8, 0, IOPort.ReadWrite.Read, 0X30, IOPort.CFKType._100K, 2);

                loadMarco(0x2c, IOPort.CFKType._100K, Convert.ToByte(IC1724.IC1724_2));//0==1724_1
                Thread.Sleep(100);
                PowerOn_All_Block(0x2c, IOPort.CFKType._100K, IC1724.IC1724_2);//0==1724_1
                Int32[] ReadData1 = MyIO.Regist_16Bit_GT174(DeviceIndex, 0, null, 1, 7, IOPort.ReadWrite.Read, 0x2c, IOPort.CFKType._100K, Convert.ToByte(IC1724.IC1724_2));

                if (ReadData1[0] == 0x7f)
                {
                    logger.AdapterLogString(1, "Gt1724_2 设置成功");
                    logger.FlushLogBuffer();
                }
                else
                {
                    logger.AdapterLogString(3, "Gt1724_2 设置失败");
                    logger.FlushLogBuffer();
                    // MessageBox.Show("Gt1724_2 设置失败");
                }
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
            }
            #endregion
        }
        private bool PowerOn_All_Block(byte deviceAddr, IOPort.CFKType cfk, IC1724 Select1724) // 芯片
        {
            int[] WriteArray1 = new int[1];
            WriteArray1[0] = 0;

            // RegAddress = 1142, When set to 1, powers down all analog blocks.
            MyIO.Regist_16Bit_GT174(DeviceIndex, 0, WriteArray1, 1, 1142, IOPort.ReadWrite.Write, deviceAddr, cfk, Convert.ToByte(Select1724));

            Int32[] WriteArray2 = new Int32[2] { 0, 0 };

            // RegAddress = 60, When set to 1, powers down the entire lane.
            // RegAddress = 61, When set to 1, powers down the entire LOS detector.
            MyIO.Regist_16Bit_GT174(DeviceIndex, 0, WriteArray2, 2, 256 * 0 + 60, IOPort.ReadWrite.Write, deviceAddr, cfk, Convert.ToByte(Select1724));
            WriteArray2 = new Int32[2] { 1, 1 };
            MyIO.Regist_16Bit_GT174(DeviceIndex, 0, WriteArray2, 2, 256 * 1 + 60, IOPort.ReadWrite.Write, deviceAddr, cfk, Convert.ToByte(Select1724));
            WriteArray2 = new Int32[2] { 0, 0 };
            MyIO.Regist_16Bit_GT174(DeviceIndex, 0, WriteArray2, 2, 256 * 2 + 60, IOPort.ReadWrite.Write, deviceAddr, cfk, Convert.ToByte(Select1724));
            WriteArray2 = new Int32[2] { 1, 1 };
            MyIO.Regist_16Bit_GT174(DeviceIndex, 0, WriteArray2, 2, 256 * 3 + 60, IOPort.ReadWrite.Write, deviceAddr, cfk, Convert.ToByte(Select1724));

            for (int i = 0; i < 4; i++)
            {
                int[] WriteArray3 = new int[1];
                WriteArray3[0] = 0;
                MyIO.Regist_16Bit_GT174(DeviceIndex, 0, WriteArray3, 1, 256 * i + 66, IOPort.ReadWrite.Write, deviceAddr, cfk, Convert.ToByte(Select1724));
            }

            return true;
        }
        private void IniTialize_1724() // 150508
        {
            Config1724_1();
            Config1724_2();
        }
        // 配置ED
        private bool EDSet() // 150508
        {
            byte Select1724; // 0=1724_1, 1=1724_2
            byte DeviceAddress; // 16进制

            byte IL01Enable;
            byte IL23Enable;
            byte IL01PatternSelect;
            byte IL23PatternSelect;
            byte IL01Invert;
            byte IL23Invert;

            try
            {
                if ((byte)RXSelect == 4)
                {
                    #region RXSelect=4
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            Select1724 = (byte)IC1724.IC1724_1;
                            DeviceAddress = (byte)0x30; // 16进制
                        }
                        else
                        {
                            Select1724 = (byte)IC1724.IC1724_2;
                            DeviceAddress = (byte)0x2C; // 16进制
                        }

                        PRBSCheckerSet(Select1724, DeviceIndex, DeviceAddress,
                            FCK, (byte)RXSwitch, (byte)RXSwitch, (byte)EDPattern, (byte)EDPattern,
                            (byte)EDInvert, (byte)EDInvert);
                    }
                    #endregion
                }
                else
                {
                    #region RXSelect!=4
                    if ((byte)RXSelect >= 2)
                    {
                        Select1724 = (byte)IC1724.IC1724_2;
                        DeviceAddress = (byte)0x2C; // 16进制
                    }
                    else
                    {
                        Select1724 = (byte)IC1724.IC1724_1;
                        DeviceAddress = (byte)0x30; // 16进制
                    }

                    PRBSCheckerStatus(Select1724, DeviceIndex, DeviceAddress,
                        FCK, out IL01Enable, out IL23Enable, out IL01PatternSelect,
                        out IL23PatternSelect, out IL01Invert, out IL23Invert);

                    if ((byte)RXSelect % 2 == 0)
                    {
                        PRBSCheckerSet(Select1724, DeviceIndex, DeviceAddress,
                            FCK, (byte)RXSwitch, IL23Enable, (byte)EDPattern,
                            IL23PatternSelect, (byte)EDInvert, IL23Invert);
                    }
                    else if ((byte)RXSelect % 2 == 1)
                    {
                        PRBSCheckerSet(Select1724, DeviceIndex, DeviceAddress,
                            FCK, IL01Enable, (byte)RXSwitch, IL01PatternSelect,
                            (byte)EDPattern, IL01Invert, (byte)EDInvert);
                    }
                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        // 读取ED设置
        private bool EDRead() // 150508
        {
            byte Select1724; // 0=1724_1, 1=1724_2
            byte DeviceAddress; // 16进制

            byte IL01Enable;
            byte IL23Enable;
            byte IL01PatternSelect;
            byte IL23PatternSelect;
            byte IL01Invert;
            byte IL23Invert;

            try
            {
                if ((byte)RXSelect != 4)
                {
                    #region RXSelect!=4
                    if ((byte)RXSelect >= 2)
                    {
                        Select1724 = (byte)IC1724.IC1724_2;
                        DeviceAddress = (byte)0x2C; // 16进制
                    }
                    else
                    {
                        Select1724 = (byte)IC1724.IC1724_1;
                        DeviceAddress = (byte)0x30; // 16进制
                    }

                    if ((byte)RXSelect % 2 == 0)
                    {
                        PRBSCheckerStatus(Select1724, DeviceIndex, DeviceAddress,
                            FCK, out IL01Enable, out IL23Enable, out IL01PatternSelect,
                            out IL23PatternSelect, out IL01Invert, out IL23Invert);
                        RXSwitch = (RXEnable)IL01Enable;
                        EDPattern = (PrbsType)IL01PatternSelect;
                        EDInvert = (ED_Inverted)IL01Invert;
                    }
                    else if ((byte)RXSelect % 2 == 1)
                    {
                        PRBSCheckerStatus(Select1724, DeviceIndex, DeviceAddress,
                            FCK, out IL01Enable, out IL23Enable, out IL01PatternSelect,
                            out IL23PatternSelect, out IL01Invert, out IL23Invert);
                        RXSwitch = (RXEnable)IL23Enable;
                        EDPattern = (PrbsType)IL23PatternSelect;
                        EDInvert = (ED_Inverted)IL23Invert;
                    }
                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Select1724">0=1724_1, 1=1724_2</param>
        /// <param name="DeviceSelect"></param>
        /// <param name="DeviceAddress">I2C从机地址</param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="IL01Enable">0=Disable, 1=Enable</param>
        /// <param name="IL23Enable">0=Disable, 1=Enable</param>
        /// <param name="IL01PatternSelect">0=PRBS Pattern 9, 1=PRBS Pattern 15, 2=PRBS Pattern 31</param>
        /// <param name="IL23PatternSelect">0=PRBS Pattern 9, 1=PRBS Pattern 15, 2=PRBS Pattern 31</param>
        /// <param name="IL01Invert">0=Not Inverted, 1=Inverted</param>
        /// <param name="IL23Invert">0=Not Inverted, 1=Inverted</param>
        /// <returns></returns>
        private bool PRBSCheckerStatus(byte Select1724, byte DeviceSelect, byte DeviceAddress,
            IOPort.CFKType FCK, out byte IL01Enable, out byte IL23Enable, out byte IL01PatternSelect,
            out byte IL23PatternSelect, out byte IL01Invert, out byte IL23Invert) // 150428 芯片
        {
            try
            {
                int registerAddress = 0xC10;
                byte dataLength = 1;
                byte regAddressWide = 1;

                byte[] dataToWrite = new byte[dataLength];
                byte[] dataToRead = new byte[dataLength];

                // This optional macro can be used to retrieve the current PRBS Checker configuration.
                // Macro Code: 0x51
                dataToWrite[0] = 0x51;

                MyIO.USBI2CWrite(DeviceAddress, registerAddress, dataLength, FCK, 
                    DeviceSelect, Select1724, regAddressWide, dataToWrite);

                for (int i = 0; i < 100; i++)
                {
                    dataToRead = MyIO.USBI2CRead(DeviceAddress, registerAddress, 
                        dataLength, FCK, DeviceSelect, Select1724, regAddressWide);

                    // If the macro register reads 0 then the previous macro has completed
                    // successfully and any output from the macro is available in the output 
                    // data buffer. If the macro register reads a 1 then the previous macro
                    // has been acknowledged but it failed to complete
                    if (dataToRead[0] == 0)
                    {
                        break;
                    }

                    // It is not recommended to poll more than every 10ms.
                    Thread.Sleep(10);
                }

                registerAddress = 0xC11;
                dataLength = 16;
                regAddressWide = 1;
                dataToRead = new byte[dataLength];

                dataToRead = MyIO.USBI2CRead(DeviceAddress, registerAddress, dataLength,
                    FCK, DeviceSelect, Select1724, regAddressWide);

                IL01Enable = (byte)(dataToRead[0] % 2);
                IL23Enable = (byte)(dataToRead[0] / 16 % 2);

                IL01PatternSelect = (byte)(dataToRead[0] / 4 % 4);
                IL23PatternSelect = (byte)(dataToRead[0] / 64 % 4); 

                IL01Invert = (byte)(dataToRead[1] % 2);
                IL23Invert = (byte)(dataToRead[1] / 2 % 2);

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                IL01Enable = 0;
                IL23Enable = 0;

                IL01PatternSelect = 0;
                IL23PatternSelect = 0;

                IL01Invert = 0;
                IL23Invert = 0;

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Select1724">0=1724_1, 1=1724_2</param>
        /// <param name="DeviceSelect"></param>
        /// <param name="DeviceAddress"></param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="IL01Enable">0=Disable, 1=Enable</param>
        /// <param name="IL23Enable">0=Disable, 1=Enable</param>
        /// <param name="IL01PatternSelect">0=PRBS Pattern 9, 1=PRBS Pattern 15, 2=PRBS Pattern 31</param>
        /// <param name="IL23PatternSelect">0=PRBS Pattern 9, 1=PRBS Pattern 15, 2=PRBS Pattern 31</param>
        /// <param name="IL01Invert">0=Not Inverted, 1=Inverted</param>
        /// <param name="IL23Invert">0=Not Inverted, 1=Inverted</param>
        /// <returns></returns>
        private bool PRBSCheckerSet(byte Select1724, byte DeviceSelect, byte DeviceAddress,
            IOPort.CFKType FCK, byte IL01Enable, byte IL23Enable, byte IL01PatternSelect,
            byte IL23PatternSelect, byte IL01Invert, byte IL23Invert) // 150428 芯片
        {
            try
            {
                int registerAddress = 0xC00;
                byte dataLength = 16;
                byte[] dataToWrite = new byte[dataLength];
                byte regAddressWide = 1;

                dataToWrite[0] = (byte)(IL01Enable + 0 + IL01PatternSelect * 4
                    + IL23Enable * 16 + 0 + IL23PatternSelect * 64);
                dataToWrite[1] = (byte)(IL01Invert + IL23Invert * 2);

                for (int i = 0; i < 14; i++)
                {
                    dataToWrite[i + 2] = 0;
                }

                MyIO.USBI2CWrite(DeviceAddress, registerAddress, dataLength, FCK,
                    DeviceSelect, Select1724, regAddressWide, dataToWrite);

                registerAddress = 0xC10;
                dataLength = 1;

                byte[] dataToRead = new byte[dataLength];
                dataToWrite = new byte[dataLength];

                // Configure and Enable PRBS Checker
                // Macro Code: 0x50
                dataToWrite[0] = 0x50;

                MyIO.USBI2CWrite(DeviceAddress, registerAddress, dataLength, FCK,
                    DeviceSelect, Select1724, regAddressWide, dataToWrite);

                for (int i = 0; i < 100; i++)
                {
                    dataToRead = MyIO.USBI2CRead(DeviceAddress, registerAddress, dataLength,
                        FCK, DeviceSelect, Select1724, regAddressWide);

                    if (dataToRead[0] == 0)
                    {
                        break;
                    }

                    Thread.Sleep(10);
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        /// <summary>
        /// 返回某一通道，在累计时间为accumTime的ErrorCount
        /// </summary>
        /// <param name="DeviceSelect"></param>
        /// <param name="RequestType"></param>
        /// <param name="FCK"></param>
        /// <param name="accumTime">累计误码时间，单位为ms</param>
        /// <param name="channel">0~3</param>
        /// <returns>累计时间为accumTime的ErrorCount</returns>
        private double GetAccumErrorCount(byte DeviceSelect, byte RequestType, 
            IOPort.CFKType FCK, int accumTime, byte channel) // 150428 芯片
        {
            double accumErrorCount;

            byte DeviceAddress;
            byte Select1724;
            byte Exponent = 0;
            double Mantissa = 0;

            try
            {
                byte PRBSCHKSelect = (byte)(channel % 2);

                if (channel >= 2)
                {
                    DeviceAddress = (byte)0x2C;
                    Select1724 = (byte)IC1724.IC1724_2;
                }
                else
                {
                    DeviceAddress = (byte)0x30;
                    Select1724 = (byte)IC1724.IC1724_1;
                }

                Thread.Sleep(accumTime);

                CheckerReading(DeviceSelect, PRBSCHKSelect, RequestType, DeviceAddress,
                       FCK, Select1724, out Exponent, out Mantissa, out accumErrorCount);

                return accumErrorCount;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return 0;
            }
        }
        /// <summary>
        /// 同时获取四通道，在累计时间为accumTime的ErrorCount
        /// </summary>
        /// <param name="DeviceSelect"></param>
        /// <param name="RequestType"></param>
        /// <param name="FCK"></param>
        /// <param name="accumTime">累计误码时间，单位为ms</param>
        /// <returns>返回四个通道累计时间为accumTime的ErrorCount数组</returns>
        private double[] GetAllChannelAccumErrorCount(byte DeviceSelect, byte RequestType,
            IOPort.CFKType FCK, int accumTime)
        {
            double[] accumErrorCount = new double[4];
            byte[] Exponent = new byte[4] {0, 0, 0, 0};
            double[] Mantissa = new double[4] {0, 0, 0, 0};
            byte[] DeviceAddress = new byte[4];
            byte[] Select1724 = new byte[4];
            byte[] PRBSCHKSelect = new byte[4];
            byte channel;

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    channel = Convert.ToByte(i);
                    PRBSCHKSelect[i] = (byte)(channel % 2);

                    if (channel >= 2)
                    {
                        DeviceAddress[i] = (byte)0x2C;
                        Select1724[i] = (byte)IC1724.IC1724_2;
                    }
                    else
                    {
                        DeviceAddress[i] = (byte)0x30;
                        Select1724[i] = (byte)IC1724.IC1724_1;
                    }
                }

                Thread.Sleep(accumTime);

                for (int i = 0; i < 4; i++)
                {
                    CheckerReading(DeviceSelect, PRBSCHKSelect[i], RequestType, DeviceAddress[i],
                           FCK, Select1724[i], out Exponent[i], out Mantissa[i], out accumErrorCount[i]);
                }

                return accumErrorCount;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                accumErrorCount = new double[4] {0, 0, 0, 0};

                return accumErrorCount;
            }
        }
        /// <summary>
        /// This macro retrieves a PRBS Checker count value from the requested inter-lane PRBS
        /// Checker. The host is able to request the error count or the bit count. In order to retrieve
        /// correlated error and bit counts, the host must disable the PRBS Checker first via the
        /// “0x52 – Control PRBS Checker Enable” macro.
        /// </summary>
        /// <param name="DeviceSelect"></param>
        /// <param name="PRBSCHKSelect">0=Checker IL01, 1=Checker IL23</param>
        /// <param name="RequestType">0=Error Count, 1=Bit Count</param>
        /// <param name="DeviceAddress"></param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="Select1724">0=1724_1, 1=1724_2</param>
        /// <param name="Exponent"></param>
        /// <param name="Mantissa"></param>
        /// <param name="ErrorCount"></param>
        /// <returns></returns>
        private bool CheckerReading(byte DeviceSelect, byte PRBSCHKSelect,
            byte RequestType, byte DeviceAddress, IOPort.CFKType FCK, byte Select1724,
            out byte Exponent, out double Mantissa, out double ErrorCount)  // 未验证 150428 芯片
        {
            try
            {
                byte dataLength = 16;
                int regAddress = 0xC00;
                byte regAddressWide = 1;
                byte[] dataToWrite = new byte[dataLength];
                byte[] dataToRead;

                dataToWrite[0] = (byte)(PRBSCHKSelect + RequestType * 2);

                for (int i = 1; i < 16; i++)
                {
                    dataToWrite[i] = 0;
                }

                MyIO.USBI2CWrite(DeviceAddress, regAddress, dataLength, FCK,
                    DeviceSelect, Select1724, regAddressWide, dataToWrite);

                dataLength = 1;
                regAddress = 0xC10;
                dataToWrite = new byte[dataLength];
                dataToWrite[0] = 0x53;

                MyIO.USBI2CWrite(DeviceAddress, regAddress, dataLength, FCK,
                    DeviceSelect, Select1724, regAddressWide, dataToWrite);

                for (int i = 0; i < 100; i++)
                {
                    dataToRead = MyIO.USBI2CRead(DeviceAddress, regAddress, dataLength,
                        FCK, DeviceSelect, Select1724, regAddressWide);

                    if (dataToRead[0] == 0)
                    {
                        break;
                    }

                    Thread.Sleep(10);
                }

                dataLength = 16;
                regAddress = 0xC11;
                dataToRead = MyIO.USBI2CRead(DeviceAddress, regAddress, dataLength,
                    FCK, DeviceSelect, Select1724, regAddressWide);

                Exponent = (byte)(Rotate(dataToRead[0], 6) % 64);
                Mantissa = dataToRead[0] % 4 * 256 + dataToRead[1];
                ErrorCount = Math.Pow(2, Exponent) * Mantissa;

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                Exponent = 0;
                Mantissa = 0;
                ErrorCount = 0;

                return false;
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="DeviceSelect"></param>
        ///// <param name="DeviceAddress"></param>
        ///// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        ///// <param name="Select1724">0=1724_1, 1=1724_2</param>
        ///// <param name="LOSDetector"></param>
        ///// <param name="LOLDetector"></param>
        ///// <returns></returns>
        //private bool LosLoLStatus(byte DeviceSelect, byte DeviceAddress, IOPort.CFKType FCK,
        //    byte Select1724, out bool[] LOSDetector, out bool[] LOLDetector)  // 150428
        //{
        //    try
        //    {
        //        byte dataLength = 1;
        //        int registerAddress = 1041;
        //        byte regAddressWide = 1;
        //        byte[] dataToRead;

        //        LOSDetector = new bool[4] { false, false, false, false };
        //        LOLDetector = new bool[4] { false, false, false, false };

        //        dataToRead = MyIO.USBI2CRead(DeviceAddress, registerAddress, dataLength,
        //            FCK, DeviceSelect, Select1724, regAddressWide);

        //        byte[] tempArray = new byte[8];

        //        for (int i = 0; i < 8; i++)
        //        {
        //            tempArray[i] = (byte)(Rotate(dataToRead[0], (byte)(i + 1)) % 2);
        //        }

        //        Array.Reverse(tempArray);

        //        bool[] tempBoolArray = new bool[8];

        //        for (int i = 0; i < 8; i++)
        //        {
        //            tempBoolArray[i] = (tempArray[i] == 0) ? true : false;
        //        }

        //        for (int i = 0; i < 4; i++)
        //        {
        //            LOSDetector[i] = tempBoolArray[i];
        //            LOLDetector[i] = tempBoolArray[i + 4];
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.AdapterLogString(3, ex.ToString());
        //        logger.FlushLogBuffer();

        //        LOSDetector = new bool[4] { false, false, false, false };
        //        LOLDetector = new bool[4] { false, false, false, false };

        //        return false;
        //    }
        //}
        /// <summary>
        /// 返回某一通道LOL status，LOS detector output status.
        /// </summary>
        /// <param name="DeviceSelect"></param>
        /// <param name="DeviceAddress"></param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="Select1724">0=1724_1, 1=1724_2</param>
        /// <param name="channel">通道0,1,2,3</param>
        /// <param name="LOSDetector"></param>
        /// <param name="LOLDetector"></param>
        /// <returns></returns>
        private bool LosLoLStatus(byte DeviceSelect, byte DeviceAddress, 
            IOPort.CFKType FCK, byte Select1724, byte channel, out bool LOSDetector, 
            out bool LOLDetector)  // 150508 根据GT1724芯片手册，优化代码
        {
            byte dataLength = 1;
            int registerAddress = 1041;
            byte regAddressWide = 1;
            byte[] dataToRead;

            LOSDetector = false;
            LOLDetector = false;

            try
            {
                dataToRead = MyIO.USBI2CRead(DeviceAddress, registerAddress, dataLength,
                    FCK, DeviceSelect, Select1724, regAddressWide);

                if (channel % 2 == 0)
                {
                    // channel=0&channel=2，分别使用两个GT1724芯片的Lane 0通道
                    if (Convert.ToString(dataToRead[0], 2).PadLeft(8, '0').Substring(7, 1) == "0")
                    {
                        // 0 = input signal present
                        // 1 = loss of input signal
                        LOSDetector = true;
                    }

                    if (Convert.ToString(dataToRead[0], 2).PadLeft(8, '0').Substring(3, 1) == "0")
                    {
                        // 0 = CDR locked
                        // 1 = CDR loss of lock
                        LOLDetector = true;
                    }
                }
                else
                {
                    // channel=1&channel=3，分别使用两个GT1724芯片的Lane 2通道
                    if (Convert.ToString(dataToRead[0], 2).PadLeft(8, '0').Substring(5, 1) == "0")
                    {
                        // 0 = input signal present
                        // 1 = loss of input signal
                        LOSDetector = true;
                    }

                    if (Convert.ToString(dataToRead[0], 2).PadLeft(8, '0').Substring(1, 1) == "0")
                    {
                        // 0 = CDR locked
                        // 1 = CDR loss of lock
                        LOLDetector = true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        /// <summary>
        /// 使x循环移动y个位数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y">y大于表示左移位，y小于0表示右移</param>
        private static byte Rotate(byte x, byte y)
        {
            byte z;

            if (y > 0)
            {
                z = (byte)((x >> (8 - y)) | (x << y));
            }
            else if (y < 0)
            {
                z = (byte)((x >> y) | (x << (8 - y)));
            }
            else
            {
                z = x;
            }

            return z;
        }
        public bool ClearAllChannelErrorCount(byte deviceIndex) // 150428 芯片
        {
            bool flag = false;

            flag = ClearECAllChannel(deviceIndex);

            return flag;
        }
        private bool ClearECAllChannel(byte deviceIndex) // 150619
        {
            byte IL01Enable;
            byte IL23Enable;
            byte deviceAddress;
            byte Select1724;
            byte Timeout; // 0=0.01s, 1=1s, 2=2s

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i >= 2)
                    {
                        Select1724 = Convert.ToByte(IC1724.IC1724_2);
                        deviceAddress = 0x2C;
                    }
                    else
                    {
                        Select1724 = Convert.ToByte(IC1724.IC1724_1);
                        deviceAddress = 0x30;
                    }

                    IL01Enable = (byte)(i % 2);
                    IL23Enable = (byte)(i % 2);

                    Timeout = 0;
                    CheckerEnable(deviceIndex, IL01Enable, IL23Enable, deviceAddress, FCK, Select1724, Timeout);
                }

                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {
                        Select1724 = Convert.ToByte(IC1724.IC1724_2);
                        deviceAddress = 0x2C;
                    }
                    else
                    {
                        Select1724 = Convert.ToByte(IC1724.IC1724_1);
                        deviceAddress = 0x30;
                    }

                    IL01Enable = 1;
                    IL23Enable = 1;

                    Timeout = 0;
                    CheckerEnable(deviceIndex, IL01Enable, IL23Enable, deviceAddress, FCK, Select1724, Timeout);

                    byte[] dataToRead;
                    byte dataLength = 1;
                    int regAddress = 0xC10;
                    byte regAddressWide = 1;

                    for (int j = 0; j < 300; j++)
                    {
                        dataToRead = MyIO.USBI2CRead(deviceAddress, regAddress, dataLength, FCK, deviceIndex, Select1724, regAddressWide);

                        if (dataToRead[0] == 0)
                        {
                            break;
                        }

                        Thread.Sleep(10);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        /// <summary>
        /// This macro is used to enable or disable either PRBS Checker using 
        /// the previously set configuration
        /// </summary>
        /// <param name="deviceIndex"></param>
        /// <param name="IL01Enable"></param>
        /// <param name="IL23Enable"></param>
        /// <param name="deviceAddress"></param>
        /// <param name="FCK"></param>
        /// <param name="Select1724"></param>
        /// <param name="Timeout">0=0.01s, 1=1s, 2=2s</param>
        /// <returns></returns>
        private bool CheckerEnable(byte deviceIndex, byte IL01Enable, byte IL23Enable,
            byte deviceAddress, IOPort.CFKType FCK, byte Select1724, byte Timeout) // 150619 芯片
        {
            byte dataLength = 16;
            int regAddress = 0xC00;
            byte regAddressWide = 1;

            byte[] dataToWrite = new byte[dataLength];

            try
            {
                dataToWrite[0] = (byte)(IL01Enable + IL23Enable * 16);
                for (int i = 0; i < 15; i++)
                {
                    dataToWrite[i + 1] = 0;
                }

                MyIO.USBI2CWrite(deviceAddress, regAddress, dataLength, FCK, deviceIndex,
                    Select1724, regAddressWide, dataToWrite);

                dataLength = 1;
                regAddress = 0xC10;
                dataToWrite = new byte[dataLength];
                dataToWrite[0] = 0x52; // Control PRBS Checker Enable
                MyIO.USBI2CWrite(deviceAddress, regAddress, dataLength, FCK, deviceIndex,
                    Select1724, regAddressWide, dataToWrite);

                byte[] dataToRead;

                int count = 1;

                if (Timeout == 0)
                {
                    count = 1;
                }
                else if (Timeout == 1)
                {
                    count = 100;
                }
                else if (Timeout == 2)
                {
                    count = 200;
                }

                for (int i = 0; i < count; i++)
                {
                    // Note 2: When the PRBS Checker is configured for a PRBS31
                    // pattern and then enabled using this macro, there is a delay of
                    // up to 2 seconds before the macro acknowledge code (0x00 or 0x01)
                    // is returned while the checker does alignment to the PRBS pattern.
                    //if (EDPattern == PrbsType.prbs31)
                    //{
                    //    Thread.Sleep(10);
                    //}

                    dataToRead = MyIO.USBI2CRead(deviceAddress, regAddress, dataLength, FCK, deviceIndex, Select1724, regAddressWide);

                    if (dataToRead[0] == 0)
                    {
                        break;
                    }

                    Thread.Sleep(10);
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerOutputSelect">0=TX1,1=TX2,2=TX3,3=TX4</param>
        /// <returns></returns>
        private bool TriggerOutputSelect(int triggerOutputSelect) // 150511
        {
            try
            {
                int dataToWrite = triggerOutputSelect + 0x8000;
                MyIO.WriteMDIO_Bert_25G(DeviceIndex, 30, IN012525_Phycial_Add, 45, dataToWrite, 1);

                return true;
            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        #endregion
    }
}
