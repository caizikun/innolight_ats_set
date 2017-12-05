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
    public class CFPExclusiveEDinputParameter : CommandInf
    {
       // public byte Addr;
        public int GatingTime;
        public byte PRBSLength;
        public double dDataRate;
        public byte deviceIndex = 0;
        public int phycialAdress = 7;
        public byte MoudleChannel = 1;
    }

    public class CFPExclusiveED:ErrorDetector
    {
        public bool awstatus_flag = false;// aw status check
        private static object syncRoot = SyncRoot_CFP_ED.Get_SyncRoot_CFP_ED();//used for thread synchronization
        public  CFPExclusiveEDinputParameter MyParameter = new CFPExclusiveEDinputParameter();
      //  private double dDataRate;
        
        public override bool Connect()
        {

            try
            {
                EquipmentConnectflag = true;
                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                Log.SaveLogToTxt(error.ToString());
                return false;

            }
        }
        public override bool Initialize(TestModeEquipmentParameters[] Inno_25GBert_EDStruct)
        {
            int i = 0;

            if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "Addr", out i))
            {
                MyParameter.Addr = Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
 
            }
            else
            {
                Log.SaveLogToTxt("there is no Addr");
                return false;
            }

            if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "IOTYPE", out i))
            {
                MyParameter.IOType = Inno_25GBert_EDStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no IOTYPE");
                return false;
            }
            if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "GatingTime", out i))
            {
                MyParameter.GatingTime = Convert.ToInt16(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no GATINGTIME");
                return false;
            }



            if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "NAME", out i))
            {
                MyParameter.Name = Inno_25GBert_EDStruct[i].DefaultValue;
            }
            else
            {
                Log.SaveLogToTxt("there is no NAME");
                return false;
            }

            if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "PRBSLength", out i))
            {
                MyParameter.PRBSLength = Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no PRBSLength");
                return false;
            }

            if (Algorithm.FindFileName(Inno_25GBert_EDStruct, "DATARATE", out i))
            {
                MyParameter.dDataRate = Convert.ToByte(Inno_25GBert_EDStruct[i].DefaultValue);
            }
            else
            {
                Log.SaveLogToTxt("there is no DATARATE");
                return false;
            }     

            if (!Connect())
            {
                return false;
            }

            return true;
        }
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                MyParameter.MoudleChannel = Convert.ToByte(channel);
                return true;
            }
        }
     
        public override bool Configure(int syn = 0)
        {
            return true;
        }

        public override bool AutoAlaign(bool becenter)
        {
            return true;
        }
        private bool SetDutRxPRBS(byte bprbsLength)
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, 1);

                    buff[0] = Convert.ToUInt16(buff[0] & 0XCFFF);

                    switch (bprbsLength)
                    {
                        case 7://00
                        case 9://00

                            buff[0] = Convert.ToUInt16(buff[0] | (0x00 << 12));
                            IOPort.WriteMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 15://01
                            buff[0] = Convert.ToUInt16(buff[0] | (0x01 << 12));
                            IOPort.WriteMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 23://10
                            buff[0] = Convert.ToUInt16(buff[0] | (0x10 << 12));
                            IOPort.WriteMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 31://11
                            buff[0] = Convert.ToUInt16(buff[0] | (0x11 << 12));
                            IOPort.WriteMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        default://11
                            break;
                    }
                    return true;
                    // IOPort.WriteMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
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

        private  void Engmod()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[2];
                buff[0] = 0xca2d;
                buff[1] = 0x815f;
                IOPort.WriteMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA000, IOPort.MDIOSoftHard.SOFTWARE, buff);
            }
        }

        public  bool EdGatingStart(bool bSwitch)
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    Engmod();
                    //   SetDutRxPRBS(prbsLength);
                    // sn = CFP.ReadSn(deviceIndex, 1, 0x8044, phycialAdress, 1);
                    ushort[] buff2 = IOPort.ReadMDIO(0, 1, 7, 0x8044, IOPort.MDIOSoftHard.SOFTWARE, 16);
                    buff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, 1);

                    buff[0] = Convert.ToUInt16(buff[0] & 0XBFFF);

                    switch (bSwitch)
                    {
                        case false://此时已经是关闭状态
                            //buff[0] = Convert.ToUInt16(buff[0] | (0x0 << 12));
                            break;

                        default://开启
                            buff[0] = Convert.ToUInt16(buff[0] | (0x1 << 14));
                            break;

                    }
                    for (int i = 0; i < 3; i++)
                    {
                        IOPort.WriteMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, buff);
                        Thread.Sleep(100);
                        ushort[] Readbuff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, 1);

                        if (Readbuff[0] == buff[0])
                        {
                            return true;
                        }
                    }

                    Log.SaveLogToTxt("Operat ED Check False");
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

        public override double RapidErrorRate(int syn = 0)
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];

                double ErrorRate = 1;


                UInt16 Mantissa;
                UInt16 Exponent;
                try
                {
                    EdGatingStart(false);
                    Thread.Sleep(50);
                    EdGatingStart(true);

                    Thread.Sleep(edGatingTime * 1000);

                    switch (MyParameter.MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA290, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            break;
                        case 2:
                            buff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA291, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            break;
                        case 3:
                            buff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA292, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            break;
                        case 4:
                            buff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA293, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            break;
                        default:
                            buff = IOPort.ReadMDIO(MyParameter.deviceIndex, 1, MyParameter.phycialAdress, 0xA290, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            break;
                    }

                    Exponent = Convert.ToUInt16(buff[0] >> 10);//Hig  6bit

                    Mantissa = Convert.ToUInt16(buff[0] & 0x3FF);//Low  10bit

                    ErrorRate = Mantissa * (Math.Pow(2, Exponent)) / (MyParameter.dDataRate * MyParameter.GatingTime * 1E+9);

                    return ErrorRate;

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
