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
using InnoBert_25G_V1;

namespace ATS_Driver
{
    //public enum DataRate : ushort
    //{
    //    Rate25 = 0,
    //    Rate28 = 1,
    //}
    public class Inno25GBertPPG : PPG
    {

           public enum TxSelect : ushort
           {
               TX1 = 0,
               TX2 = 1,
               TX3 = 2,
               TX4 = 3,
               TXAll = 4
           }
           public enum BitSelect : ushort
        {
            Rate25G=0,
            Rate28G = 1
        }
        public enum PRBS : ushort
        {
            PRBS31=0,
            PRBS23 = 5,
            PRBS15= 6,
            PRBS7 = 7
        
        }

        public enum PG_Inverted : byte
        {
            Inverted = 1,
            NO_Inverted = 0,
        }
        public enum TxPrbsType : ushort
        {
            prbs31 = 0,
            prbs9 = 1,
            //prbs31 = 4,
            prbs23 = 5,
            prbs15 = 6,
            prbs7 = 7
        }
 
        public enum Tx_Channel : ushort 
        {
            TX1 = 0,
            TX2 = 1,
            TX3 = 2,
            TX4 = 3,
            AllTX = 4,
        }
        public enum TX_Set_Swing : ushort 
        {
            Swing_0 = 0,
            Swing_25 = 1,
            Swing_50 = 2,
            Swing_75 = 3,
            Swing_100 = 4,
        }
        //public enum Pre_Cursor_Type : byte 
        //{
        //    Value_0 = 0,
        //    Value_5 = 1,
        //    Value_10 = 2,
        //    Value_15 = 3,
        //}


        public enum TriggerSelect : ushort 
        {
            TX1 = 0,
            TX2 = 1,
            TX3 = 2,
            TX4 = 3,
        }
       
        public int DeviceIndex;
        public IOPort.CFKType FCK = IOPort.CFKType._100K;
        public int IN012525_Phycial_Add = 0;
        public Tx_Channel pTx_Channel;
        public TxPrbsType pTxPrbsType;
        public PG_Inverted PPGInvert;
        public DataRate pDataRate;
        public TX_Set_Swing pSwing;
        public TriggerSelect TriggerOutput = TriggerSelect.TX1;
        private byte currPPGChannel = 1; // 1,2,3,4
        //private Device pDevice;
        private LVEnum_1 pDevice;
       // private Pre_Cursor_Type Pre_Cursor = Pre_Cursor_Type.Value_0; // 设置为0%
       // private PostcursorType Post_Cursor = PostcursorType.Value_10; // 设置为10%

        private string[] TriggerOutputList;
        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization
        
        public override bool Initialize(TestModeEquipmentParameters[] Inno_25GBert_PPGStruct) 
        {
            lock (syncRoot)
            {
                int i = 0;
                try
                {

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "Addr", out i))
                    {
                        Addr = Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
                        switch (Addr)
                        {
                            case 0:
                                pDevice = LVEnum_1.Device_0;
                                break;
                            case 1:
                                pDevice = LVEnum_1.Device_1;
                                break;
                            case 2:
                                pDevice = LVEnum_1.Device_2;
                                break;
                            case 3:
                                pDevice = LVEnum_1.Device_3;
                                break;
                            default:
                                pDevice = LVEnum_1.Device_1;
                                break;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no Addr");
                        return false;
                    }



                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "TriggerOutputList".ToUpper(), out i))
                    {
                        TriggerOutputList = Inno_25GBert_PPGStruct[i].DefaultValue.Split(',');
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TriggerOutputList");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "IOTYPE", out i))
                    {
                        IOType = Inno_25GBert_PPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(Inno_25GBert_PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "NAME", out i))
                    {
                        Name = Inno_25GBert_PPGStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "DATARATE", out i))
                    {
                        dataRate = Inno_25GBert_PPGStruct[i].DefaultValue;
                        // pDataRate = (DataRate)Convert.ToDouble(dataRate);
                        switch (dataRate)
                        {
                            case "25.78":
                                pDataRate = DataRate.Rate25;
                                break;
                            case "28":
                                pDataRate = DataRate.Rate28;
                                break;
                            default:
                                break;

                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DATARATE");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "PPGInvert".ToUpper(), out i))
                    {
                        PPGInvert = (PG_Inverted)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no PPGInvert");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "PPGPattern".ToUpper(), out i))
                    {
                        pTxPrbsType = (TxPrbsType)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no PPGPattern");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_PPGStruct, "SWING", out i))
                    {
                        pSwing = (TX_Set_Swing)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no SWING");
                        return false;
                    }
                    totalChannels = 4;
                    if (!Connect()) return false;
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + error.ID + "Reason=" + error.TargetSite.Name + "Fail");
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
        public bool ReSet()
        {
            return true;
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
                        if (!IniTialize_Bert()) return false;
                        if (!ConfigureChannel(syn)) return false;
                        if (!TriggerOutputSelect(1)) return false;
                        EquipmentConfigflag = true;
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
                    // throw new InnoExCeption(ex);
                }
            }
        }
        public override bool Connect()
        {
            lock (syncRoot)
            {
                try
                {

                    EquipmentConnectflag = true;

                    return EquipmentConnectflag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    //ogger.FlushLogBuffer();

                    return false;
                }
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
            lock (syncRoot)
            {
                try
                {
                    currPPGChannel = Convert.ToByte(TriggerOutputList[Convert.ToInt32(channel) - 1]);

                    return TriggerOutputSelect(currPPGChannel);

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
        #region 私有方法
        private bool ConfigureChannel( int syn = 1)
        {
            lock (syncRoot)
            {
                try
                {
                    if (syn == 0)
                    {

                        Bert_25G_V1.PPGWrite((ushort)pTx_Channel, (ushort)pTxPrbsType, (ushort)pSwing, (byte)PPGInvert, pDevice);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {

                            Bert_25G_V1.PPGWrite((ushort)pTx_Channel, (ushort)pTxPrbsType, (ushort)pSwing, (byte)PPGInvert, pDevice);
                            Thread.Sleep(1000);
                            ushort Swingindex, PRBSIndex;
                            byte PPGInvertIndex;
                            Bert_25G_V1.PPGRead((ushort)pTx_Channel, pDevice, out Swingindex, out PRBSIndex, out PPGInvertIndex);
                            if (Swingindex == (ushort)pSwing && (byte)PPGInvert == PPGInvertIndex && (ushort)pTxPrbsType == PRBSIndex)
                            {
                                return true;
                            }
                        }
                        return false;
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
                    // throw new InnoExCeption(ex);
                }
            }
        }
        private bool IniTialize_Bert() // 150513
        {
            lock (syncRoot)
            {
                try
                {


                    bool flag_H1023 = false;
                    bool[] flag_N1025 = new bool[2];
                    bool[] flag_GT1724 = new bool[2];
                    Bert_25G_V1.Initialize(Convert.ToByte(pDataRate.GetHashCode()), pDevice, out flag_H1023, out flag_N1025, out flag_GT1724);

                    if (flag_H1023 && flag_N1025[0] && flag_N1025[1])
                    {
                        //  MessageBox.Show("PPG ok");
                        return true;
                    }
                    else
                    {
                        return false;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Channel">Dut Channel</param>
        /// <returns></returns>
        private bool TriggerOutputSelect(int Channel) // 150511
        {
            lock (syncRoot)
            {
                try
                {
                    int TempValue = Convert.ToInt16(Channel - 1);
                    Bert_25G_V1.TriggerOutputWrite((ushort)TempValue, pDevice);

                    ushort TriggerIndex;

                    Bert_25G_V1.TriggerOutputRead(pDevice, out TriggerIndex);

                    if (TriggerIndex == (ushort)TempValue)
                    {
                        return true;
                    }

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
                    // throw new InnoExCeption(ex);
                }
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


        private LVEnum_1 pDevice;
        private DataRate pDataRate;
        private static object syncRoot = SyncRoot_PPG_ED.Get_SyncRoot_PPG_ED();//used for thread synchronization

        public override bool Initialize(TestModeEquipmentParameters[] Inno_25GBert_EDStruct)
        {
            lock (syncRoot)
            {
                int i = 0;
                try
                {



                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "Addr", out i))
                    {
                        Addr = Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);

                        switch (Addr)
                        {
                            case 0:
                                pDevice = LVEnum_1.Device_0;
                                break;
                            case 1:
                                pDevice = LVEnum_1.Device_1;
                                break;
                            case 2:
                                pDevice = LVEnum_1.Device_2;
                                break;
                            case 3:
                                pDevice = LVEnum_1.Device_3;
                                break;
                            default:
                                pDevice = LVEnum_1.Device_1;
                                break;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no Addr");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "IOTYPE", out i))
                    {
                        IOType = Inno_25GBert_EDStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no IOTYPE");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "DATARATE", out i))
                    {
                        dataRate = Inno_25GBert_EDStruct[i].DefaultValue;
                        //* */ Switch()
                        switch (dataRate)
                        {
                            case "25.78":
                                pDataRate = DataRate.Rate25;
                                break;
                            case "28":
                                pDataRate = DataRate.Rate28;
                                break;

                            default:
                                pDataRate = DataRate.Rate25;
                                break;
                        }

                        //pDataRate
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DATARATE");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "EDGATINGTIME", out i))
                    {
                        edGatingTime = Convert.ToInt16(Inno_25GBert_EDStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no EDGATINGTIME");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "RESET", out i))
                    {
                        Reset = Convert.ToBoolean(Inno_25GBert_EDStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RESET");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "NAME", out i))
                    {
                        Name = Inno_25GBert_EDStruct[i].DefaultValue;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no NAME");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "EDPattern", out i))
                    {
                        EDPattern = (PrbsType)Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no EDPattern");
                        return false;
                    }

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "EDInvert", out i))
                    {
                        EDInvert = (ED_Inverted)Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no EDInvert");
                        return false;
                    }

                    DeviceIndex = Convert.ToByte(Addr);

                    if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "TriggerOutputList".ToUpper(), out i))
                    {
                        TriggerOutputList = Inno_25GBert_EDStruct[i].DefaultValue.Split(',');
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TriggerOutputList");
                        return false;
                    }

                    //if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "deviceIndex", out i))
                    //{
                    //    DeviceIndex = Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no deviceIndex");
                    //    return false;
                    //}

                    //if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "FCK", out i))
                    //{
                    //    FCK = (IOPort.CFKType)Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
                    //}
                    //else
                    //{
                    //    Log.SaveLogToTxt("there is no FCK");
                    //    return false;
                    //}

                    if (!Connect())
                    {
                        return false;
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
        public override bool Connect()
        {
            lock (syncRoot)
            {
                try
                {

                    EquipmentConnectflag = true;

                    return EquipmentConnectflag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    //

                    return false;
                }
            }
        }
        public bool ReSet()
        {
            return true;
        }
        private bool IniTialize_Bert() // 150513
        {
            lock (syncRoot)
            {
                try
                {
                    bool flag_H1023 = false;
                    bool[] flag_N1025 = new bool[2];
                    bool[] flag_GT1724 = new bool[2];
                    Bert_25G_V1.Initialize(Convert.ToByte(pDataRate.GetHashCode()), pDevice, out flag_H1023, out flag_N1025, out flag_GT1724);

                    if (flag_H1023 && flag_GT1724[0] && flag_GT1724[1])
                    {
                        //  MessageBox.Show("PPG ok");
                        return true;
                    }
                    else
                    {
                        return false;
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
                        if (!IniTialize_Bert()) return false;
                        if (!ConfigureAllChannel()) return false;

                        EquipmentConfigflag = true;
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
                    // throw new InnoExCeption(ex);
                }
            }
        }
        public bool ConfigureAllChannel()
        {// 第一个参数4 表示RXAllChannel,第二个参数 1=RxEnable 写死在程序中
            lock (syncRoot)
            {
                try
                {


                    Bert_25G_V1.EDWrite(4, 1, (byte)EDPattern, (byte)EDInvert, pDevice);
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
        public override double GetErrorRate(int syn=0)
        {
            lock (syncRoot)
            {
                try
                {
                    return RapidErrorRate();
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
        //快速误码测试
        public override double RapidErrorRate(int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {


                    double errratio = 0;

                    // FlowControl传入的channel范围为1~4，Driver内部需要处理为0~3，故需要做currentChannel - 1

                    double[] ArrarBer = RapidErrorRate_AllCH();
                    errratio = ArrarBer[currentChannel - 1];
                    return errratio;
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
        public override double[] RapidErrorRate_AllCH(int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {


                    // byte bitRateSelect;
                    double[] ErrRata = new double[4];
                    Bert_25G_V1.ClearECAllCH(pDevice);
                    Thread.Sleep(1000);
                    bool[] Flag_Lock = new bool[4];
                    string[] ArrayBer = new string[4];
                    string[] ArrayErrorCount = new string[4];
                    string StrBer, StrErrorCount;
                    Bert_25G_V1.GetBER(edGatingTime, (byte)pDataRate, pDevice, out Flag_Lock, out ArrayBer, out ArrayErrorCount, out StrBer, out StrErrorCount);

                    for (int i = 0; i < 4; i++)
                    {
                        try
                        {

                            double k = Convert.ToDouble(ArrayBer[i]);

                            if (Flag_Lock[i] && k >= 0 && k <= 1)
                            {
                                ErrRata[i] = k;
                            }
                            else
                            {
                                ErrRata[i] = 1;

                            }

                        }
                        catch
                        {
                            ErrRata[i] = 1;
                        }
                    }

                    return ErrRata;
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
        /// 
        /// </summary>
        /// <param name="channel">channel取值范围为1,2,3,4</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {
                    currEDChannel = Convert.ToByte(TriggerOutputList[Convert.ToInt32(channel) - 1]);
                    byte channelbyte = Convert.ToByte(channel);
                    Log.SaveLogToTxt("TriggerOutput channel is " + currEDChannel);
                    currentChannel = channelbyte;
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
        public override bool AutoAlaign(bool becenter)
        {
            lock (syncRoot)
            {
                try
                {


                    Bert_25G_V1.ClearECAllCH(pDevice);
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

    }
}
