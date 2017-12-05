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
using Bert25GNoPolarity;


namespace ATS_Driver
{
    public enum DataRate : ushort
    {
        Rate25 = 0,
        Rate28 = 1,
    }
    public enum ED_Version : ushort
    {
        R1 = 0,
        R2 = 1,
    }

    public class Inno25GBert_V2_PPG : PPG
    {

        private byte IN012525_PHYCIAL_ADDRESS = 0;

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
            Rate25G = 0,
            Rate28G = 1
        }
        public enum PRBS : ushort
        {
            PRBS31 = 4,
            PRBS23 = 5,
            PRBS15 = 6,
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
        public enum TriggerSelect : ushort
        {
            TX1 = 0,
            TX2 = 1,
            TX3 = 2,
            TX4 = 3,
        }

        public int DeviceIndex;
        public IOPort.CFKType FCK = IOPort.CFKType._100K;
        public byte IN012525_Phycial_Add = 0;
        public Tx_Channel pTx_Channel;
        public TxPrbsType pTxPrbsType;
        public PG_Inverted PPGInvert;
        public DataRate pDataRate;
        public TX_Set_Swing pSwing;
        public TriggerSelect TriggerOutput = TriggerSelect.TX1;
        private byte currPPGChannel = 1; // 1,2,3,4
        //private Device pDevice;
        private LVEnum_1 pDevice;
        private ED_Version pED_Version;
        // private Pre_Cursor_Type Pre_Cursor = Pre_Cursor_Type.Value_0; // 设置为0%
        // private PostcursorType Post_Cursor = PostcursorType.Value_10; // 设置为10%

        private string[] TriggerOutputList;

        public Algorithm algorithm = new Algorithm();

        public Inno25GBert_V2_PPG(logManager logmanager)
        {
            logger = logmanager;
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
                    if (!IniTialize_Bert()) return false;
                   // if (!ConfigureChannel(syn)) return false;
                    if (!TriggerOutputSelect(1)) return false;
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
                currPPGChannel = Convert.ToByte(TriggerOutputList[Convert.ToInt32(channel)-1]);
                //TriggerOutputSelect(0);
                //TriggerOutputSelect(1);
                //TriggerOutputSelect(2);
                return TriggerOutputSelect(currPPGChannel);

            }
            catch (Exception ex)
            {
                logger.AdapterLogString(3, ex.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }

        public override bool Initialize(TestModeEquipmentParameters[] Inno_25GBert_PPGStruct)
        {

            int i = 0;
            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "Addr", out i))
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
                logger.AdapterLogString(4, "there is no Addr");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "ED_Version", out i))
            {
                byte Ed_Value = Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
                switch (Ed_Value)
                {
                    case 0:
                        pED_Version = ED_Version.R1;
                        break;
                    case 1:
                        pED_Version = ED_Version.R2;
                        break;
               
                    default:
                        pED_Version = ED_Version.R2;
                        break;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no Addr");
                return false;
            }


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
                logger.AdapterLogString(4, "there is no DATARATE");
                return false;
            }

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
                pTxPrbsType = (TxPrbsType)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no PPGPattern");
                return false;
            }

            if (algorithm.FindFileName(Inno_25GBert_PPGStruct, "SWING", out i))
            {
                pSwing = (TX_Set_Swing)Convert.ToByte(Inno_25GBert_PPGStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no SWING");
                return false;
            }

            if (!Connect()) return false;
            return true;
        }

        public override bool ConfigureOTxPolarity(bool polarity)
        {

            ushort Swing,uPrbs;
            byte[] ReadInverted=new byte[4];
            //polarity= 正, Anritsu为标准 自制BERT =Inverted=1 否则 BERT =NoInverted=0
            //TxALLChannel=4,phycialAddress=0,PRBS31=4,Swing100=4, polarity
            byte Inverted=0;
            if (polarity)
            {
                Inverted=1;
                for (int i = 0; i < 4; i++)
                {

                    ReadInverted[i] = 0;
                }
            }
            else
            {
                 Inverted=0;
                 for (int i = 0; i < 4; i++)
                 {

                     ReadInverted[i] = 1;
                 }
            }
            //polarity= 正, Anritsu为标准 自制BERT =Inverted=1 否则 BERT =NoInverted=0
            //TxALLChannel=4,phycialAddress=0,PRBS31=4,Swing100=4, polarity

            Bert25GNoPolarityClass.PPGWrite(4, 0, 4, 4, Inverted, pDevice);

            for (byte i = 0; i < 4; i++)
            {

               // Bert25GNoPolarityClass.PPGWrite(4, 0, 4, 4, Inverted, pDevice);
                Thread.Sleep(100);
                Bert25GNoPolarityClass.PPGRead(0, i, pDevice, out Swing, out uPrbs, out ReadInverted[i]);


                
            }

            if (ReadInverted[0] == Inverted && ReadInverted[1] == Inverted && ReadInverted[2] == Inverted && ReadInverted[3] == Inverted)
            {
                return true;
            }
            else
            {


                return false;
            }

        }
        #region 私有方法
        private bool ConfigureChannel(int syn = 1) //此函数将不在使用
        {
            try
            {
              

                //ushort Swing1, PPGType1;
                //byte PGInvert1;
                //// 光源的 2,4 通道反相
                //Bert_25G_V2.PPGWrite(1, 1, (ushort)(pTxPrbsType), (ushort)(TX_Set_Swing.Swing_100), (byte)(1), pDevice);
                //Bert_25G_V2.PPGWrite(0, 1, (ushort)(pTxPrbsType), (ushort)(TX_Set_Swing.Swing_100), (byte)(1), pDevice);

                //Bert_25G_V2.PPGRead(1, (ushort)1, pDevice, out Swing1, out PPGType1, out PGInvert1);

                //if (Swing1 != (ushort)(TX_Set_Swing.Swing_100) || PPGType1 != (ushort)(pTxPrbsType) || PGInvert1 != (byte)(PG_Inverted.Inverted))
                //{
                //    return false;
                //}
                //Bert_25G_V2.PPGRead(1, (ushort)0, pDevice, out Swing1, out PPGType1, out PGInvert1);

                //if (Swing1 != (ushort)(TX_Set_Swing.Swing_100) || PPGType1 != (ushort)(pTxPrbsType) || PGInvert1 != (byte)(PG_Inverted.Inverted))
                //{
                //    return false;
                //}



                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        private bool IniTialize_Bert() // OK
        {
            bool flag_REF32 = false;
            bool[] PPG32 ;
            bool[] ED32 ;
            bool[] flag_GT1724 = new bool[2];
             bool[] flag_PPG_OK;
             bool[] flag_ED_OK;
               bool flag_PRBS_OK=false;
             String   BertSN ="";
            
             bool   flagTotal =true;

             try
             {



                 Bert25GNoPolarityClass.InitializeUpdateForAnritsuPRBSDefinition(1, (byte)pDataRate, pDevice, out flag_REF32, out flag_PPG_OK, out flag_ED_OK, out BertSN, out flag_PRBS_OK);

                 if (!flag_REF32) return false;

                 for (int i = 0; i < flag_PPG_OK.Length; i++)
                 {
                     if (!flag_PPG_OK[i])
                     {
                         logger.AdapterLogString(1, "PPG Error");
                         return false;
                     }

                 }

                 for (int i = 0; i < flag_ED_OK.Length; i++)
                 {
                     if (!flag_ED_OK[i])
                     {
                         logger.AdapterLogString(1, "ED Error");
                         return false;
                     }
                 }
                 if (BertSN == null || BertSN == "")
                 {
                     logger.AdapterLogString(1, "SN Error");
                     return false;
                 }
                 if (!flag_PRBS_OK)
                 {
                     logger.AdapterLogString(1, "PRBS Error");
                     return false;
                 }

                 return true;
             }
            catch(Exception EX)
             {
                 logger.AdapterLogString(1, EX.Message);
                 logger.FlushLogBuffer();
                 return false;
             }
           // return flagTotal;

        }
    
        private bool TriggerOutputSelect(int Channel) // ok
        {
            try
            {
                ushort TempValue;
                Bert25GNoPolarityClass.TriggerOutputWrite(IN012525_Phycial_Add, (ushort)(Channel - 1), pDevice);
                Thread.Sleep(200);
                Bert25GNoPolarityClass.TriggerOutputRead(IN012525_Phycial_Add, pDevice, out TempValue);
                if ((Channel - 1) == (ushort)TempValue)
                {
                    return true;
                }
                return false;
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

    public class Inno25GBert_V2_ED : ErrorDetector
    {
        public enum ED_Inverted : byte
        {
            NO_Inverted = 0,
            Inverted = 1,
        }
        public enum PrbsType : byte
        {//0->PRBS31;1->9;5->23;6->15;7->7
          
            PRBS9 = 0,
            PRBS15 = 1,
            PRBS31 = 2
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

        public Algorithm algorithm = new Algorithm();
        private LVEnum_1 pDevice;
        private DataRate pDataRate;
       // private LVEnum_1 pDevice;
        public Inno25GBert_V2_ED(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] Inno_25GBert_EDStruct)
        {
            int i = 0;

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "Addr", out i))
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

          

            if (algorithm.FindFileName(Inno_25GBert_EDStruct, "EDGATINGTIME", out i))
            {
                edGatingTime = Convert.ToInt16(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no EDGATINGTIME");
                return false;
            }

            //if (algorithm.FindFileName(Inno_25GBert_EDStruct, "RESET", out i))
            //{
            //    Reset = Convert.ToBoolean(Inno_25GBert_EDStruct[i].DefaultValue);
            //}
            //else
            //{
            //    logger.AdapterLogString(4, "there is no RESET");
            //    return false;
            //}

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

            totalChannels = 4;

            //if (algorithm.FindFileName(Inno_25GBert_EDStruct, "EDInvert", out i))
            //{
            //    EDInvert = (ED_Inverted)Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            //}
            //else
            //{
            //    logger.AdapterLogString(4, "there is no EDInvert");
            //    return false;
            //}

          //  DeviceIndex = Convert.ToByte(Addr);

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
        private bool IniTialize_Bert() // 150513
        {
            //bool flag_N1035;
            //Bert25GNoPolarityClass.H1035LockDetect(pDevice, out flag_N1035);
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
                   // ConfigureAllChannel();
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

        private bool ConfigureChannel(int syn = 1) //OK
        {

            //// public enum TX_Set_Swing : ushort
        
            //Swing_0 = 0,
            //Swing_25 = 1,
            //Swing_50 = 2,
            //Swing_75 = 3,
            //Swing_100 = 4,
   
            try
            {
                
               // currEDChannel=
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();

                return false;
            }
        }
        public bool ConfigureAllChannel()//此函数将不在使用
        {// 第一个参数4 表示RXAllChannel,第二个参数 1=RxEnable 写死在程序中

            byte bRx_Enable, bED_Inverted,patternSelect;

            //Bert_25G_V2.EDWrite(0, 1, (byte)EDPattern, 0, pDevice);//Ch1
            //Bert_25G_V2.EDRead(0, pDevice, out bRx_Enable, out bED_Inverted, out patternSelect);

            //if (bRx_Enable != 1 || bED_Inverted != 0 || patternSelect != (byte)EDPattern)
            //{
            //    return false;
            //}


            //Bert_25G_V2.EDWrite(1, 1, (byte)EDPattern, 1, pDevice);//Ch2

            //Bert_25G_V2.EDRead(1, pDevice, out bRx_Enable, out bED_Inverted, out patternSelect);

            //if (bRx_Enable != 1 || bED_Inverted != 1 || patternSelect != (byte)EDPattern)
            //{
            //    return false;
            //}
            ////Bert_25G_V2.EDWrite(2, 1, (byte)EDPattern, 0, pDevice);//Ch3

            ////Bert_25G_V2.EDRead(2, pDevice, out bRx_Enable, out bED_Inverted, out patternSelect);

            ////if (bRx_Enable != 1 || bED_Inverted != 0 || patternSelect != (byte)EDPattern)
            ////{
            ////    return false;
            ////}

            //Bert_25G_V2.EDWrite(3, 1, (byte)EDPattern, 1, pDevice);//Ch4

            //Bert_25G_V2.EDRead(3, pDevice, out bRx_Enable, out bED_Inverted, out patternSelect);

            //if (bRx_Enable != 1 || bED_Inverted != 1 || patternSelect != (byte)EDPattern)
            //{
            //    return false;
            //}



            //--------------------------------------------------------------------原本代码 RX接线 全部反接
            //Bert_25G_V2.EDWrite(4, 1, (byte)EDPattern, 0, pDevice);//Ch4


            //for (int i = 0; i < 4; i++)
            //{
            //    Bert_25G_V2.EDRead((ushort)i, pDevice, out bRx_Enable, out bED_Inverted, out patternSelect);

            //    if (bRx_Enable != 1 || bED_Inverted != 0 || patternSelect != (byte)EDPattern)
            //    {
            //        return false;
            //    }
            //}

            ////----------------因为要使用内置光源, 整体RX AllChannel 反制, 测试接线 正接着正  负接负
            //Bert25GNoPolarityClass.EDWrite(4, 1, (byte)EDPattern, 1, pDevice);//Ch4


            //for (int i = 0; i < 4;i++ )
            //{
            //    Bert25GNoPolarityClass.EDRead((ushort)i, pDevice, out bRx_Enable, out bED_Inverted, out patternSelect);

            //    if (bRx_Enable != 1 || bED_Inverted != 1 || patternSelect != (byte)EDPattern)
            //    {
            //        return false;
            //    }
            //}

           
            return true;
        }
        public override double GetErrorRate(int syn = 0)
        {
            return RapidErrorRate();
        }
        //快速误码测试
        public override double QureyEdErrorRatio()
        {
            return this.RapidErrorRate();
        }
        public override double RapidErrorRate(int syn = 0)
        {
            double errratio = 0;

            // FlowControl传入的channel范围为1~4，Driver内部需要处理为0~3，故需要做currentChannel - 1

            double[] ArrarBer = RapidErrorRate_AllCH();
            errratio = ArrarBer[currEDChannel - 1];
            return errratio;
        }
        public override double[] RapidErrorRate_AllCH(int syn = 0)
        {
            // byte bitRateSelect;
            double[] ErrRata = new double[4];

            Bert25GNoPolarityClass.ClearECAllCH(pDevice);
            Thread.Sleep(1000);
            bool[] Flag_Lock = new bool[4];
            string[] ArrayBer = new string[4];
            string[] ArrayErrorCount = new string[4];
            string StrBer, StrErrorCount;
            Bert25GNoPolarityClass.GetBER(edGatingTime, (byte)pDataRate, pDevice, out Flag_Lock, out ArrayBer, out ArrayErrorCount, out StrBer, out StrErrorCount);
           // Bert_25G_V2.GetBER()
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
        public override double[] RapidErrorCount_AllCH(int syn = 0, bool IsClear = false)
        {
            double[] ErrCount = new double[4] { 1E+9, 1E+9, 1E+9, 1E+9 };

            if (IsClear)
            {
                Bert25GNoPolarityClass.ClearECAllCH(pDevice);
            }
            Thread.Sleep(1000);
        
            ulong[] ArrayErrorCount = new ulong[4];
       
            Bert25GNoPolarityClass.GetErrorCount(pDevice, out ArrayErrorCount);

            ErrCount = Array.ConvertAll<ulong, double>(ArrayErrorCount, s => double.Parse(s.ToString()));

            return ErrCount;
        }
        //public override double[] RapidErrorCount_AllCH(int syn = 0)
        //{
        //    double[] ErrCount = new double[4];

        //    Bert25GNoPolarityClass.ClearECAllCH(pDevice);
        //    Thread.Sleep(1000);
        //    bool[] Flag_Lock = new bool[4];
        //    string[] ArrayBer = new string[4];
        //    string[] ArrayErrorCount = new string[4];
        //    string StrBer, StrErrorCount;
        //    Bert25GNoPolarityClass.GetBER(edGatingTime, (byte)pDataRate, pDevice, out Flag_Lock, out ArrayBer, out ArrayErrorCount, out StrBer, out StrErrorCount);
        //    // Bert_25G_V2.GetBER()
        //    for (int i = 0; i < 4; i++)
        //    {
        //        try
        //        {

        //            double k = Convert.ToDouble(ArrayErrorCount[i]);

        //            if (Flag_Lock[i] && k >= 0)
        //            {
        //                ErrCount[i] = k;
        //            }
        //            else
        //            {
        //                ErrCount[i] = 1000000;

        //            }

        //        }
        //        catch
        //        {
        //            ErrCount[i] = 1000000;
        //        }
        //    }

        //    return ErrCount;
            
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel">channel取值范围为1,2,3,4</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool ChangeChannel(string channel, int syn = 0)
        {        
             byte  LightSourceChannel = Convert.ToByte(TriggerOutputList[Convert.ToInt32(channel) - 1]);

             currEDChannel = LightSourceChannel;
             return true;
        }
        public override bool AutoAlaign(bool becenter)
        {
           // Bert_25G_V2
            Bert25GNoPolarityClass.ClearECAllCH(pDevice);
            return true;
        }
        public override bool ConfigureERxPolarity(bool polarity)
        {
            byte Enable,  uPrbs;
            byte[] ReadInverted = new byte[4];
            //polarity= 正, Anritsu为标准 自制BERT =Inverted=1 否则 BERT =NoInverted=0
            //TxALLChannel=4,phycialAddress=0,PRBS31=4,Swing100=4, polarity
            byte Inverted = 0;
            if (polarity)
            {
                Inverted = 1;
                for (int i = 0; i < 4; i++)
                {

                    ReadInverted[i] = 0;
                }
            }
            else
            {
                Inverted = 0;
                for (int i = 0; i < 4; i++)
                {

                    ReadInverted[i] = 1;
                }
            }
            //polarity= 正, Anritsu为标准 自制BERT =Inverted=1 否则 BERT =NoInverted=0
            //RxALLChannel=4,PRBS31=2, Enable=1

            Bert25GNoPolarityClass.EDWrite(4, 1, 2,  Inverted, pDevice);

            for (ushort i = 0; i < 4; i++)
            {

                // Bert25GNoPolarityClass.PPGWrite(4, 0, 4, 4, Inverted, pDevice);
                Thread.Sleep(100);
                Bert25GNoPolarityClass.EDRead(i, pDevice, out Enable,out ReadInverted[i],out uPrbs);



            }

            if (ReadInverted[0] == Inverted && ReadInverted[1] == Inverted && ReadInverted[2] == Inverted && ReadInverted[3] == Inverted)
            {
                return true;
            }
            else
            {


                return false;
            }
             
        }

    }
}
