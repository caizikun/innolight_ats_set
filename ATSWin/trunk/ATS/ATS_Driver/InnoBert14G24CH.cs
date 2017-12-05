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
using InnoBertCh24;

namespace ATS_Driver
{  
    public class InnoBert14G24CH_PPG:PPG
    {
        private LVEnum_1 device;

        private Algorithm algorithm = new Algorithm();

        private PG_Inverted invent;

        private TxPRBS txPRBS;

        private DataRate rate;

        private byte isON = 1;

        private ushort subBoardNum = 0;

        private ushort gt1706_CLK = 1;

        public InnoBert14G24CH_PPG(logManager logmanager)
        {
            logger = logmanager;
        }

        public enum PG_Inverted : byte
        {
            Inverted = 1,
            NO_Inverted = 0,
        }

        public enum TxPRBS : ushort
        {
            PRBS7 = 0,
            PRBS9 = 1,
            PRBS10 = 2,
            PRBS11 = 3,
            PRBS15 = 4,
            PRBS23 = 5,
            PRBS31 = 6
        }

        public enum DataRate : ushort
        {
            Rate103125 = 12,
            Rate14 = 24,
        }

        public override bool Initialize(TestModeEquipmentParameters[] inPara)
        {
            int i = 0;            
            if (algorithm.FindFileName(inPara, "Addr", out i))
            {
                Addr = Convert.ToByte(inPara[i].DefaultValue);
                switch (Addr)
                {
                    case 0:
                        device = LVEnum_1.Device_0;
                        break;
                    case 1:
                        device = LVEnum_1.Device_1;
                        break;
                    case 2:
                        device = LVEnum_1.Device_2;
                        break;
                    case 3:
                        device = LVEnum_1.Device_3;
                        break;
                    default:
                        device = LVEnum_1.Device_1;
                        break;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no Addr");
                return false;
            }

            if (algorithm.FindFileName(inPara, "IOTYPE", out i))
            {
                IOType = inPara[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }

            if (algorithm.FindFileName(inPara, "RESET", out i))
            {
                Reset = Convert.ToBoolean(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }

            if (algorithm.FindFileName(inPara, "NAME", out i))
            {
                Name = inPara[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }

            if (algorithm.FindFileName(inPara, "DATARATE", out i))
            {
                dataRate = inPara[i].DefaultValue;
                switch (dataRate)
                {
                    case "10.3125":
                        rate = DataRate.Rate103125;
                        break;
                    case "14":
                        rate = DataRate.Rate14;
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

            if (algorithm.FindFileName(inPara, "TotalChannel".ToUpper(), out i))
            {
                totalChannels = Convert.ToByte(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no TotalChannel");
                return false;
            }

            if (algorithm.FindFileName(inPara, "PPGInvert".ToUpper(), out i))
            {
                invent = (PG_Inverted)Convert.ToByte(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no PPGInvert");
                return false;
            }

            if (algorithm.FindFileName(inPara, "PPGPattern".ToUpper(), out i))
            {
                txPRBS = (TxPRBS)Convert.ToByte(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no PPGPattern");
                return false;
            }

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

                    if (!Initialize_PPG())
                    {
                        return false;
                    }
                    // if (!ConfigureChannel(syn)) return false;
                    //if (!TriggerOutputSelect(1)) return false;
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

        public bool ReSet()
        {
            bool result;
            InnoBert14GCh24.InitializeFW(subBoardNum, device, out result);

            if (!result)
            {
                return false;
            }

            return true;
        }       

        public bool Initialize_PPG()
        {
            bool result;
            bool[] txLock;            

            InnoBert14GCh24.TimeBase((ushort)rate, gt1706_CLK, subBoardNum, device, out result);
            if (!result)
            {
                return false;
            }

            InnoBert14GCh24.InitializeTX((ushort)rate, gt1706_CLK, subBoardNum, device, out txLock);
            for (int i = 0; i < txLock.Length; i++)
            {
                if (!txLock[i])
                {
                    return false;
                }                 
            }

            InnoBert14GCh24.TXSet((byte)invent, isON, totalChannels, subBoardNum, (byte)txPRBS, device);

            return true;
        }
    }

    public class InnoBert14G24CH_ED : ErrorDetector
    {
        private LVEnum_1 device;

        private Algorithm algorithm = new Algorithm();

        private RxPRBS rxPRBS;

        private DataRate rate;

        private ED_Inverted invent;

        private byte currEDChannel = 1; // 1,2,3,4

        private string[] outChannel;

        private ushort subBoardNum = 0;        

        private byte isON = 1;        

        public InnoBert14G24CH_ED(logManager logmanager)
        {
            logger = logmanager;
        }

        public enum RxPRBS : byte
        {
            PRBS7 = 0,
            PRBS9 = 1,
            PRBS10 = 2,
            PRBS11 = 3,
            PRBS15 = 4,
            PRBS23 = 5,
            PRBS31 = 6           
        }

        public enum DataRate : ushort
        {
            Rate103125 = 12,
            Rate14 = 24,
        }

        public enum ED_Inverted : byte
        {
            Inverted = 1,
            NO_Inverted = 0,
        }

        public override bool Initialize(TestModeEquipmentParameters[] inPara)
        {
            int i = 0;

            if (algorithm.FindFileName(inPara, "Addr", out i))
            {
                Addr = Convert.ToByte(inPara[i].DefaultValue);

                switch (Addr)
                {
                    case 0:
                        device = LVEnum_1.Device_0;
                        break;
                    case 1:
                        device = LVEnum_1.Device_1;
                        break;
                    case 2:
                        device = LVEnum_1.Device_2;
                        break;
                    case 3:
                        device = LVEnum_1.Device_3;
                        break;
                    default:
                        device = LVEnum_1.Device_1;
                        break;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no Addr");
                return false;
            }

            if (algorithm.FindFileName(inPara, "IOTYPE", out i))
            {
                IOType = inPara[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no IOTYPE");
                return false;
            }

            if (algorithm.FindFileName(inPara, "EDGATINGTIME", out i))
            {
                edGatingTime = Convert.ToInt16(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no EDGATINGTIME");
                return false;
            }

            if (algorithm.FindFileName(inPara, "RESET", out i))
            {
                Reset = Convert.ToBoolean(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no RESET");
                return false;
            }

            if (algorithm.FindFileName(inPara, "TriggerOutputList".ToUpper(), out i))
            {
                outChannel = inPara[i].DefaultValue.Split(',');
            }
            else
            {
                logger.AdapterLogString(4, "there is no TriggerOutputList");
                return false;
            }

            if (algorithm.FindFileName(inPara, "NAME", out i))
            {
                Name = inPara[i].DefaultValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no NAME");
                return false;
            }

            if (algorithm.FindFileName(inPara, "EDPattern", out i))
            {
                rxPRBS = (RxPRBS)Convert.ToByte(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no EDPattern");
                return false;
            }

            if (algorithm.FindFileName(inPara, "DATARATE", out i))
            {
                string dataRate = inPara[i].DefaultValue;
                switch (dataRate)
                {
                    case "10.3125":
                        rate = DataRate.Rate103125;
                        break;
                    case "14":
                        rate = DataRate.Rate14;
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

            if (algorithm.FindFileName(inPara, "EDInvert", out i))
            {
                invent = (ED_Inverted)Convert.ToByte(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no EDInvert");
                return false;
            }

            if (algorithm.FindFileName(inPara, "TotalChannel".ToUpper(), out i))
            {
                totalChannels = Convert.ToByte(inPara[i].DefaultValue);
            }
            else
            {
                logger.AdapterLogString(4, "there is no TotalChannel");
                return false;
            }            

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
            bool result;

            InnoBert14GCh24.InitializeFW(subBoardNum, device, out result);

            if (!result)
            {
                return false;
            }

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

                    if (!IniTialize_ED())
                    {
                        return false;
                    }
                    // ConfigureAllChannel();
                    EquipmentConfigflag = true;
                }

                //double[] abc = RapidErrorRate_AllCH();
                //double a = GetErrorRate();
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                logger.FlushLogBuffer();
                return false;
            }
        }

        public bool IniTialize_ED()
        {            

            InnoBert14GCh24.InitializeRX(subBoardNum, (ushort)rate, device);

            InnoBert14GCh24.RXSet(isON, (byte)rxPRBS, (byte)invent, device, subBoardNum, totalChannels);

            //double ber = this.RapidErrorRate();

            return true;
        }

        public override double GetErrorRate(int syn = 0)
        {
            return RapidErrorRate();
        }

        //快速误码测试
        public override double RapidErrorRate(int syn = 0)
        {
            // FlowControl传入的channel范围为1~4，Driver内部需要处理为0~3，故需要做currentChannel - 1
            double[] ber = RapidErrorRate_AllCH();

            return ber[currEDChannel - 1];
        }

        public override double[] RapidErrorRate_AllCH(int syn = 0)
        {
            double[] readData = new double[6];
            string[,] ber = new string[6, 6];
            string[,] errorCount = new string[6, 6];

            //InnoBert14GCh24.ClearECAllCH(device);
            Thread.Sleep(1000);       
            InnoBert14GCh24.__504hBER_accurateBERSimple((ushort)rate, edGatingTime, device, out errorCount, out ber);

            // Bert_25G_V2.GetBER()
            for (int i = 0; i < 6; i++)
            {
                try
                {
                    double k = Convert.ToDouble(ber[0, i]);

                    if (k >= 0 && k <= 1)
                    {
                        readData[i] = k;
                    }
                    else
                    {
                        readData[i] = 1;
                    }
                }
                catch
                {
                    readData[i] = 1;
                }
            }

            return readData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel">channel取值范围为1,2,3,4</param>
        /// <param name="syn"></param>
        /// <returns></returns>
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            byte lightSourceChannel = Convert.ToByte(outChannel[Convert.ToInt32(channel) - 1]);
            currEDChannel = lightSourceChannel;

            return true;
        }

        public override bool AutoAlaign(bool becenter)
        {
            // Bert_25G_V2
            //Bert25GNoPolarityClass.ClearECAllCH(device);
            return true;
        }
    }
}
