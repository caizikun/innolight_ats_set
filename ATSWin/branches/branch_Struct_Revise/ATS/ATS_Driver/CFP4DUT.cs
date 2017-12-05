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
    public class CFP4DUT : DUT
    {
        private static object syncRoot = SyncRoot_CFP_ED.Get_SyncRoot_CFP_ED();//used for thread synchronization
        public bool awstatus_flag = false;// aw status check
        
        public override bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList, DriverInitializeStruct[] DriverinitList, DutEEPROMInitializeStuct[] EEpromInitList, string AuxAttribles)
        {
            lock (syncRoot)
            {
                try
                {
                    DutStruct = DutList;
                    DriverStruct = DriverList;
                    DriverInitStruct = DriverinitList;
                    EEpromInitStruct = EEpromInitList;
                    string temp = AuxAttribles.ToUpper().Replace(" ", "");
                    ChipsetControll = temp.Contains("OLDDRIVER=1");

                    phycialAdress = 7;
                    if (!Connect()) return false;



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
                    EquipmentConnectflag = EVBInitialize();  //add EVB initialize for HW txdis&rxlos
                    return EquipmentConnectflag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                MoudleChannel = Convert.ToByte(channel);
                return true;
            }
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }
        public override bool Configure(int syn = 0)
        {
            return true;
        }
     
        public override void Engmod(byte engpage)
        {
            lock (syncRoot)
            {
                try
                {



                    UInt16[] buff = new UInt16[2];
                    buff[0] = 0xca2d;
                    buff[1] = 0x815f;
                    IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA000, IOPort.MDIOSoftHard.SOFTWARE, buff);
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
        #region dmi
        public override double ReadDmiTemp()
        {//A02F
            lock (syncRoot)
            {
                try
                {
                    return EEPROM.readdmitemp(DUT.deviceIndex, 1, 0xA02F, phycialAdress, 1);
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
        public override double ReadDmiVcc()
        {//A030
            lock (syncRoot)
            {
                try
                {
                    return EEPROM.readdmivcc(DUT.deviceIndex, 1, 0xA030, phycialAdress, 1);
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
        public override double ReadDmiBias()
        {//A2A0,A2A1,A2A2,A2A3
            lock (syncRoot)
            {
                double dmibias = 0.0;
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 1, 0xA2A0, phycialAdress, 1);
                            break;
                        case 2:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 1, 0xA2A1, phycialAdress, 1);
                            break;
                        case 3:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 1, 0xA2A2, phycialAdress, 1);
                            break;
                        case 4:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 1, 0xA2A3, phycialAdress, 1);
                            break;
                        default:
                            break;
                    }
                    return dmibias;
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
        public override double ReadDmiTxp()
        {//A2B0,A2B1,A2B2,A2B3
            lock (syncRoot)
            {
                double dmitxp = 0.0;
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 1, 0xA2B0, phycialAdress, 1);
                            break;
                        case 2:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 1, 0xA2B1, phycialAdress, 1);
                            break;
                        case 3:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 1, 0xA2B2, phycialAdress, 1);
                            break;
                        case 4:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 1, 0xA2B3, phycialAdress, 1);
                            break;
                        default:
                            break;
                    }
                    return dmitxp;
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
        public override double ReadDmiRxp()
        {//A2D0,A2D1,A2D2,A2D3
            lock (syncRoot)
            {
                double dmirxp = 0.0;
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 1, 0xA2D0, phycialAdress, 1);
                            break;
                        case 2:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 1, 0xA2D1, phycialAdress, 1);
                            break;
                        case 3:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 1, 0xA2D2, phycialAdress, 1);
                            break;
                        case 4:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 1, 0xA2D3, phycialAdress, 1);
                            break;
                        default:
                            break;
                    }
                    return dmirxp;
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
        public override double ReadDmiLaTemp()
        {//A2C0,A2C1,A2C2,A2C3
            lock (syncRoot)
            {
                double dmirxp = 0.0;
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            dmirxp = EEPROM.readdmitemp(DUT.deviceIndex, 1, 0xA2C0, phycialAdress, 1);
                            break;
                        case 2:
                            dmirxp = EEPROM.readdmitemp(DUT.deviceIndex, 1, 0xA2C1, phycialAdress, 1);
                            break;
                        case 3:
                            dmirxp = EEPROM.readdmitemp(DUT.deviceIndex, 1, 0xA2C2, phycialAdress, 1);
                            break;
                        case 4:
                            dmirxp = EEPROM.readdmitemp(DUT.deviceIndex, 1, 0xA2C3, phycialAdress, 1);
                            break;
                        default:
                            break;
                    }
                    return dmirxp;
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
        //a/w 未全部实现，只是用了check功能，read以及set不用
        #region read aw
        public override double ReadTempHA()
        {
            double tempha = 0.0;
            //Engmod(3);
            //tempha = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 128);
            return tempha;
        }
        public override double ReadTempLA()
        {
            double templa = 0.0;
            //Engmod(3);
            //templa = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 130);
            return templa;
        }
        public override double ReadTempLW()
        {
            double templw = 0.0;
            //Engmod(3);
            //templw = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 134);
            return templw;

        }
        public override double ReadTempHW()
        {
            double temphw = 0.0;
            //Engmod(3);
            //temphw = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 132);
            return temphw;
        }
        public override double ReadVccLA()
        {
            double vccla = 0.0;
            //Engmod(3);
            //vccla = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 146);
            return vccla;

        }
        public override double ReadVccHA()
        {
            double vccha = 0.0;
            //Engmod(3);
            //vccha = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 144);
            return vccha;

        }
        public override double ReadVccLW()
        {
            double vcclw = 0.0;
            //Engmod(3);
            //vcclw = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 150);
            return vcclw;

        }
        public override double ReadVccHW()
        {
            double vcchw = 0.0;
            //Engmod(3);
            //vcchw = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 148);
            return vcchw;

        }
        public override double ReadBiasLA()
        {
            double biasla = 0.0;
            //Engmod(3);
            //biasla = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 186);
            return biasla;

        }
        public override double ReadBiasHA()
        {
            double biasha = 0.0;
            //Engmod(3);
            //biasha = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 184);
            return biasha;

        }
        public override double ReadBiasLW()
        {
            double biaslw = 0.0;
            //Engmod(3);
            //biaslw = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 190);
            return biaslw;

        }
        public override double ReadBiasHW()
        {
            double biashw = 0.0;
            //Engmod(3);
            //biashw = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 188);
            return biashw;

        }
        public override double ReadTxpLA()
        {
            double txpla = 0.0;
            //Engmod(3);
            //txpla = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 194);
            return txpla;

        }
        public override double ReadTxpLW()
        {
            double txplw = 0.0;
            //Engmod(3);
            //txplw = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 198);
            return txplw;

        }
        public override double ReadTxpHA()
        {
            double txpha = 0.0;
            //Engmod(3);
            //txpha = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 192);
            return txpha;

        }
        public override double ReadTxpHW()
        {
            double txphw = 0.0;
            //Engmod(3);
            //txphw = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 196);
            return txphw;

        }
        public override double ReadRxpLA()
        {
            double rxpla = 0.0;
            //Engmod(3);
            //rxpla = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 178);
            return rxpla;

        }
        public override double ReadRxpLW()
        {
            double rxplw = 0.0;
            //Engmod(3);
            //rxplw = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 182);
            return rxplw;

        }
        public override double ReadRxpHA()
        {
            double rxpha = 0.0;
            //Engmod(3);
            //rxpha = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 176);
            return rxpha;

        }
        public override double ReadRxpHW()
        {
            double rxphw = 0.0;
            //Engmod(3);
            //rxphw = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 180);
            return rxphw;

        }

        #endregion
        #region check a/w
        private int biasawaddr()
        {
            lock (syncRoot)
            {
                int biasawadd;

                switch (MoudleChannel)
                {
                    case 1:
                        biasawadd = 0xA200;
                        break;
                    case 2:
                        biasawadd = 0xA201;
                        break;
                    case 3:
                        biasawadd = 0xA202;
                        break;
                    case 4:
                        biasawadd = 0xA203;
                        break;
                    default:
                        biasawadd = 0xA200;
                        break;
                }
                return biasawadd;
            }
        }

        public override bool ChkTempHA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0800) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkTempLA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0100) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkVccHA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0080) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkVccLA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0010) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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

        public override bool ChkBiasHA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x8000) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkBiasLA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x1000) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkTxpHA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0800) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkTxpLA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0100) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkRxpHA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0008) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkRxpLA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0001) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkTempHW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0400) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkTempLW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0200) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkVccHW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0040) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkVccLW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0020) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkBiasHW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x4000) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkBiasLW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x2000) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkTxpHW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0400) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkTxpLW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0200) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkRxpHW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0004) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkRxpLW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0002) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkLaTempHA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0080) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkLaTempLA()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0010) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkLaTempHW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0040) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        public override bool ChkLaTempLW()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = biasawaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
                    if ((buff[0] & 0x0020) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag;

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
        #region read optional status /control bit
        public override bool ChkTxDis()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            if ((buff[0] & 0x0001) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            if ((buff[0] & 0x0002) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            if ((buff[0] & 0x0004) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            if ((buff[0] & 0x0008) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        default:
                            break;
                    }
                    return awstatus_flag;

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
        private int txrxlosaddr()
        {
            lock (syncRoot)
            {
                int biasawadd;

                switch (MoudleChannel)
                {
                    case 1:
                        biasawadd = 0xA210;
                        break;
                    case 2:
                        biasawadd = 0xA211;
                        break;
                    case 3:
                        biasawadd = 0xA212;
                        break;
                    case 4:
                        biasawadd = 0xA213;
                        break;
                    default:
                        biasawadd = 0xA210;
                        break;
                }
                return biasawadd;
            }
        }
        public override bool ChkTxFault()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = txrxlosaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);

                    if ((buff[0] & 0x2000) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }

                    return awstatus_flag;

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
        public override bool ChkRxLos()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {
                    int biashaadd = txrxlosaddr();
                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);

                    if ((buff[0] & 0x0010) != 0)
                    {
                        awstatus_flag = true;
                    }

                    else
                    {
                        awstatus_flag = false;
                    }
                    return awstatus_flag && ChkRxLosHW();

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
        #region set a/w
        public override void SetTempLA(decimal templa)
        {
            //Engmod(3);
            //EEPROM.settempaw(DUT.deviceIndex, 0xA0, 130, templa);
        }
        public override void SetTempHA(decimal tempha)
        {
            //Engmod(3);
            //EEPROM.settempaw(DUT.deviceIndex, 0xA0, 128, tempha);
        }
        public override void SetTempLW(decimal templw)
        {
            //Engmod(3);
            //EEPROM.settempaw(DUT.deviceIndex, 0xA0, 134, templw);
        }
        public override void SetTempHW(decimal temphw)
        {
            //Engmod(3);
            //EEPROM.settempaw(DUT.deviceIndex, 0xA0, 132, temphw);
        }
        public override void SetVccHW(decimal vcchw)
        {
            //Engmod(3);
            //EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 148, vcchw);
        }
        public override void SetVccLW(decimal vcclw)
        {
            //Engmod(3);
            //EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 150, vcclw);
        }
        public override void SetVccLA(decimal vccla)
        {
            //Engmod(3);
            //EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 146, vccla);
        }
        public override void SetVccHA(decimal vccha)
        {
            //Engmod(3);
            //EEPROM.setvccaw(DUT.deviceIndex, 0xA2, 144, vccha);
        }
        public override void SetBiasLA(decimal biasla)
        {
            //Engmod(3);
            //EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 186, biasla);
        }
        public override void SetBiasHA(decimal biasha)
        {
            //Engmod(3);
            //EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 184, biasha);
        }
        public override void SetBiasHW(decimal biashw)
        {
            //Engmod(3);
            //EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 188, biashw);
        }
        public override void SetBiasLW(decimal biaslw)
        {
            //Engmod(3);
            //EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 190, biaslw);
        }
        public override void SetTxpLW(decimal txplw)
        {
            //Engmod(3);
            //EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 198, txplw);
        }
        public override void SetTxpHW(decimal txphw)
        {
            //Engmod(3);
            //EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 196, txphw);
        }
        public override void SetTxpLA(decimal txpla)
        {
            //Engmod(3);
            //EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 194, txpla);
        }
        public override void SetTxpHA(decimal txpha)
        {
            //Engmod(3);
            //EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 192, txpha);
        }
        public override void SetRxpHA(decimal rxpha)
        {
            //Engmod(3);
            //EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 176, rxpha);
        }
        public override void SetRxpLA(decimal rxpla)
        {
            //Engmod(3);
            //EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 178, rxpla);
        }
        public override void SetRxpHW(decimal rxphw)
        {
            //Engmod(3);
            //EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 180, rxphw);
        }
        public override void SetRxpLW(decimal rxplw)
        {
            //Engmod(3);
            //EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 182, rxplw);
        }

        public override bool SetSoftTxDis()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {


                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            buff[0] = (UInt16)(buff[0] | 0x0001);
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 2:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            buff[0] = (UInt16)(buff[0] | 0x0002);
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 3:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            buff[0] = (UInt16)(buff[0] | 0x0004);
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 4:
                            buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                            buff[0] = (UInt16)(buff[0] | 0x0008);
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        default:
                            break;
                    }
                    return true;

                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool TxAllChannelEnable()
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                UInt16[] buffRead;
                buff[0] = 0;
                IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                Thread.Sleep(50);
                buffRead = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                if (buffRead[0] == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion
        //w/r  sn/pn 只需实现读取既可以
        #region w/r  sn/pn
        public override string ReadSn()
        {
            lock (syncRoot)
            {
                try
                {
                    string sn = "";
                    sn = EEPROM.ReadSn(DUT.deviceIndex, 1, 0x8044, phycialAdress, 1);
                    return sn;
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
        public override string ReadPn()
        {
            lock (syncRoot)
            {
                try
                {


                    string pn = "";
                    pn = EEPROM.ReadPn(DUT.deviceIndex, 1, 0x8034, phycialAdress, 1);
                    return pn;
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
        public override void SetSn(string sn)
        {
            //Engmod(0);
            //EEPROM.SetSn(DUT.deviceIndex, 0xA0, 196, sn);
            ////System.Threading.Thread.Sleep(1000);

        }
        public override void SetPn(string pn)
        {
            //Engmod(0);
            //EEPROM.SetPn(DUT.deviceIndex, 0xA0, 168, pn);
            ////System.Threading.Thread.Sleep(1000);

        }
        #endregion
        //read manufacture data
        #region fwrev
        public override string ReadFWRev()
        {
            lock (syncRoot)
            {
                try
                {


                    string fwrev = "";
                    Engmod(3);
                    fwrev = EEPROM.ReadFWRev(DUT.deviceIndex, 1, 0x9000, phycialAdress, 1);
                    return fwrev;
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
        #region ADC
        public override bool ReadTempADC(out UInt16 tempadc)
        {
            lock (syncRoot)
            {
                tempadc = 0;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            tempadc = EEPROM.readadc(DUT.deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                            Log.SaveLogToTxt("Current TEMPERATUREADC is" + tempadc);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TEMPERATUREADC");
                        return true;
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
        public override bool ReadTempADC(out UInt16 tempadc, byte TempSelect)
        {
            lock (syncRoot)
            {
                try
                {
                    return ReadTempADC(out tempadc);
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
        public override bool ReadLaTempADC(out UInt16 latempadc)
        {
            lock (syncRoot)
            {
                latempadc = 0;
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "LATEMPERATUREADC"))
                    {
                        Engmod(3);
                        try
                        {
                            latempadc = EEPROM.readadc(DUT.deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                            Log.SaveLogToTxt("Current LATEMPERATUREADC is" + latempadc);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no LATEMPERATUREADC");
                        return true;
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
        public override bool ReadVccADC(out UInt16 vccadc)
        {//vccselect 1 VCC1ADC
            lock (syncRoot)
            {
                vccadc = 0;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "VCCADC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            vccadc = EEPROM.readadc(DUT.deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                            Log.SaveLogToTxt("Current VCCADC is" + vccadc);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no VCCADC");
                        return true;
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
        public override bool ReadVccADC(out UInt16 vccadc, byte vccselect)
        {
            lock (syncRoot)
            {
                try
                {
                    return ReadVccADC(out vccadc);
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
        public override bool ReadBiasADC(out UInt16 biasadc)
        {
            lock (syncRoot)
            {
                biasadc = 0;
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXBIASADC"))
                    {
                        Engmod(3);
                        try
                        {
                            biasadc = EEPROM.readadc(DUT.deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                            Log.SaveLogToTxt("Current TXBIASADC is" + biasadc);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXBIASADC");
                        return true;
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
        public override bool ReadTxpADC(out UInt16 txpadc)
        {
            lock (syncRoot)
            {
                txpadc = 0;
                int i = 0;

                try
                {
                    if (FindFiledNameChannel(out i, "TXPOWERADC"))
                    {
                        Engmod(3);
                        try
                        {
                            txpadc = EEPROM.readadc(DUT.deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                            Log.SaveLogToTxt("Current TXPOWERADC is " + txpadc);

                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXPOWERADC");
                        return true;
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
        public override bool ReadRxpADC(out UInt16 rxpadc)
        {
            lock (syncRoot)
            {
                rxpadc = 0;
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "RXPOWERADC"))
                    {
                        Engmod(3);
                        try
                        {
                            rxpadc = EEPROM.readadc(DUT.deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                            Log.SaveLogToTxt("Current RXPOWERADC is" + rxpadc);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RXPOWERADC");
                        return true;
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
        #endregion
        #region read/write reg/port
        public override byte[] ReadReg(int deviceIndex, int deviceAddress, int regAddress, int readLength)
        {
            return new byte[16];

        }
        public override byte[] WriteReg(int deviceIndex, int deviceAddress, int regAddress, byte[] dataToWrite)
        {
            return new byte[16];
        }
        public override bool WritePort(int id, int deviceIndex, int Port, int DDR)
        {
            return false;
        }
        public override byte[] ReadPort(int id, int deviceIndex, int Port, int DDR)
        {
            return new byte[16]; 
        }
        public override UInt16[] ReadMDIO(int deviceIndex, int deviceAddress, int regAddress, int readLength)
        {
            lock (syncRoot)
            {
                try
                {


                    return IOPort.ReadMDIO(DUT.deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, readLength);
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
        public override byte[] WriteMDIO(int deviceIndex, int deviceAddress, int regAddress, UInt16[] dataToWrite)
        {
            lock (syncRoot)
            {
                try
                {


                    return IOPort.WriteMDIO(DUT.deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, dataToWrite);
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
       
       
        #region driver
        public UInt16 MDIOWriteDriver(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, UInt16 dataToWrite)
        {
            lock (syncRoot)
            {
                try
                {
                    return EEPROM.ReadWriteDriverCFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x02, chipset, dataToWrite, phycialAdress);
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
        public UInt16 MDIOReadDriver(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset)
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ReadWriteDriverCFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x01, chipset, 0, phycialAdress);
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
        public UInt16 MDIOStoreDriver(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, UInt16 dataToWrite)
        {
            lock (syncRoot)
            {
                try
                {
                    return EEPROM.ReadWriteDriverCFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x04, chipset, dataToWrite, phycialAdress);
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
        //driver innitialize
        public override bool DriverInitialize()
        {//database 0: LDD 1: AMP 2: DAC 3: CDR
            lock (syncRoot)
            {
                ////chipset 1ldd,2amp,3cdr,0dac
                int j = 0;
                int k = 0;
                //byte engpage = 0;
                int startaddr = 0;
                byte chipset = 0x01;
                UInt16 temp;
                bool flag = true;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    if (DriverInitStruct.Length == 0)
                    {
                        return true;
                    }
                    else
                    {
                        for (int i = 0; i < DriverInitStruct.Length; i++)
                        {
                            if (DriverInitStruct[i].Length == 0)
                            {
                                continue;
                            }
                            else
                            {
                                double tempinput = Convert.ToDouble(DriverInitStruct[i].ItemValue);
                                UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                                switch (DriverInitStruct[i].DriverType)
                                {
                                    case 0:
                                        chipset = 0x01;
                                        break;
                                    case 1:
                                        chipset = 0x02;
                                        break;
                                    case 2:
                                        chipset = 0x00;
                                        break;
                                    case 3:
                                        chipset = 0x03;
                                        break;
                                    default:
                                        chipset = 0x01;
                                        break;

                                }
                                Engmod(3);
                                for (k = 0; k < 3; k++)
                                {
                                    MDIOWriteDriver(DUT.deviceIndex, 1, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                                    // Thread.Sleep(200);  
                                    MDIOStoreDriver(DUT.deviceIndex, 1, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                                    // Thread.Sleep(200); 
                                    temp = MDIOReadDriver(DUT.deviceIndex, 1, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset);

                                    if (temp == WriteBiasDacByteArray)
                                        break;
                                }
                                if (k >= 3)
                                    flag = false;
                            }
                        }
                        return flag;
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
        //eeprominit
        public override bool EEpromInitialize()
        {
            lock (syncRoot)
            {
                int j = 0;
                int k = 0;
                byte engpage = 0;
                UInt16[] temp;
                bool flag = true;
                try
                {


                    if (EEpromInitStruct.Length == 0)
                    {
                        return true;
                    }
                    else
                    {
                        for (int i = 0; i < EEpromInitStruct.Length; i++)
                        {
                            byte[] WriteeeArray = Algorithm.ObjectTOByteArray(EEpromInitStruct[i].ItemValue, 1, true);
                            ushort[] WriteBiasDacByteArray = new ushort[1];
                            WriteBiasDacByteArray[0] = (ushort)(WriteeeArray[0]);
                            engpage = EEpromInitStruct[j].Page;
                            Engmod(engpage);
                            for (k = 0; k < 3; k++)
                            {
                                WriteMDIO(DUT.deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, WriteBiasDacByteArray);
                                temp = new ushort[1];
                                temp = ReadMDIO(DUT.deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, 1);
                                if (temp[0] == WriteBiasDacByteArray[0])
                                    break;
                            }
                            if (k >= 3)
                                flag = false;

                        }
                        return flag;
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
        //set biasmoddac
        public override bool WriteCrossDac(object crossdac)
        {//database 0: LDD 1: AMP 2: DAC 3: CDR
            lock (syncRoot)
            {
                ////chipset 1tx,2rx,4dac
                int j = 0;
                //byte engpage=0;
                int startaddr = 0;
                byte chipset = 0x01;
                //if (FindFiledName(out j, "DEBUGINTERFACE"))
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");

                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(crossdac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOWriteDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no CROSSDAC");
                        return true;
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
        public override bool WriteBiasDac(object biasdac)
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            lock (syncRoot)
            {
                ////chipset 1tx,2rx,4dac
                int j = 0;
                //byte engpage=0;
                int startaddr = 0;
                byte chipset = 0x01;
                //if (FindFiledName(out j, "DEBUGINTERFACE"))
                try
                {
                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");

                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "BIASDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(biasdac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);

                        MDIOWriteDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no BIASDAC");
                        return true;
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
        public override bool WriteModDac(object moddac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");

                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "MODDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(moddac);



                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOWriteDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no MODDAC");
                        return true;
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
        public override bool WriteLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "LOSDAC"))
                    {

                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(losdac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOWriteDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no LOSDAC");
                        return true;
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
        public override bool WriteLOSDDac(object losddac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "LOSDDAC"))
                    {

                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(losddac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOWriteDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no LOSDDAC");
                        return true;
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
        public override bool WriteAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                ////byte engpage = 0;
                int startaddr = 0;
                try
                {
                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "APDDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(apddac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOWriteDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no APDDAC");
                        return true;
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
        public override bool StoreCrossDac(object crossdac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(crossdac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOStoreDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no CROSSDAC");
                        return true;
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
        public override bool StoreBiasDac(object biasdac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "BIASDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(biasdac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOStoreDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no BIASDAC");
                        return true;
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
        public override bool StoreModDac(object moddac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;

                    if (FindFiledNameChannelDAC(out i, "MODDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(moddac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOStoreDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no MODDAC");
                        return true;
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
        public override bool StoreLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;

                    if (FindFiledNameChannelDAC(out i, "LOSDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(losdac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOStoreDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no LOSDAC");
                        return true;
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
        public override bool StoreLOSDDac(object losddac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;

                    if (FindFiledNameChannelDAC(out i, "LOSDDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(losddac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOStoreDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no LOSDDAC");
                        return true;
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
        public override bool StoreAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "APDDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        double tempinput = Convert.ToDouble(apddac);
                        UInt16 WriteBiasDacByteArray = (UInt16)(tempinput);
                        MDIOStoreDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no APDDAC");
                        return true;
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
        //read biasmoddac
        public override bool ReadCrossDac(int length, out byte[] crossdac)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                crossdac = new byte[length];
                int j = 0;
                //byte engpage = 0;
                int startaddr = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        //engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
                    {
                        switch (DriverStruct[i].DriverType)
                        {
                            case 0:
                                chipset = 0x01;
                                break;
                            case 1:
                                chipset = 0x02;
                                break;
                            case 2:
                                chipset = 0x00;
                                break;
                            case 3:
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(3);
                        UInt16 temp = MDIOReadDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset);
                        crossdac[0] = (byte)(temp / 256);
                        crossdac[1] = (byte)(temp & 0xFF);
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no CROSSDAC");
                        return true;
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
    
        //public override bool ReadModDac(int length, out byte[] ModDac)
        //{
        //    byte chipset = 0x01;
        //    ModDac = new byte[length];
        //    int j = 0;
        //    //byte engpage = 0;
        //    int startaddr = 0;
        //    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        //engpage = DutStruct[j].EngPage;
        //        startaddr = DutStruct[j].StartAddress;

        //    }
        //    else
        //    {
        //        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
        //    }
        //    int i = 0;
        //    if (FindFiledNameChannelDAC(out i, "MODDAC"))
        //    {
        //        switch (DriverStruct[i].DriverType)
        //        {
        //            case 0:
        //                chipset = 0x01;
        //                break;
        //            case 1:
        //                chipset = 0x02;
        //                break;
        //            case 2:
        //                chipset = 0x00;
        //                break;
        //            case 3:
        //                chipset = 0x03;
        //                break;
        //            default:
        //                chipset = 0x01;
        //                break;

        //        }
        //        Engmod(3);
        //        UInt16 temp = MDIOReadDriver(DUT.deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset);
        //        ModDac[0] = (byte)(temp / 256);
        //        ModDac[1] = (byte)(temp & 0xFF);
        //        return true;
        //    }
        //    else
        //    {
        //        Log.SaveLogToTxt("there is no MODDAC");
        //        return true;
        //    }
        //}
      
        #endregion

        #region set coef


        public override bool SetTempcoefa(string tempcoefa)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFA", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, tempcoefa, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMITEMPCOEFA To" + tempcoefa);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITEMPCOEFA");
                        return true;
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
        public override bool SetTempcoefb(string tempcoefb, byte TempSelect)
        {
            lock (syncRoot)
            {
                try
                {


                    return SetTempcoefb(tempcoefb);
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
        public override bool SetTempcoefb(string tempcoefb)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMITEMPCOEFB To" + tempcoefb);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITEMPCOEFB");
                        return true;
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
        public override bool ReadTempcoefb(out string strcoef, byte TempSelect)
        {
            lock (syncRoot)
            {
                try
                {
                    return ReadTempcoefb(out strcoef);
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
        public override bool ReadTempcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFA", out i))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITEMPCOEFA");
                        return true;
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
        public override bool SetTempcoefc(string tempcoefc, byte TempSelect)
        {
            lock (syncRoot)
            {
                try
                {


                    return SetTempcoefc(tempcoefc);
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
        public override bool ReadTempcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITEMPCOEFB");
                        return true;
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
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        {
            lock (syncRoot)
            {
                try
                {


                    return ReadTempcoefc(out strcoef);
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
        public override bool SetTempcoefc(string tempcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMITEMPCOEFC To" + tempcoefc);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITEMPCOEFC");
                        return true;
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
        public override bool SetVcccoefb(string vcccoefb, byte vccselect)
        {
            lock (syncRoot)
            {
                try
                {


                    return SetVcccoefb(vcccoefb);
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
        public override bool ReadTempcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITEMPCOEFC");
                        return true;
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
        public override bool ReadVcccoefb(out string strcoef, byte vccselect)
        {
            lock (syncRoot)
            {
                try
                {


                    return ReadVcccoefb(out strcoef);
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
        public override bool SetVcccoefc(string vcccoefc, byte vccselect)
        {
            lock (syncRoot)
            {
                try
                {


                    return SetVcccoefc(vcccoefc);
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
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        {
            lock (syncRoot)
            {
                try
                {


                    return ReadVcccoefc(out strcoef);
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
        public override bool SetVcccoefa(string vcccoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFA", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, vcccoefa, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMIVCCCOEFA To" + vcccoefa);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIVCCCOEFA");
                        return true;
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
        public override bool SetVcccoefb(string vcccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                    {
                        Engmod(3);

                        flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format, phycialAdress, 1);
                        Log.SaveLogToTxt("SET DMIVCCCOEFB To" + vcccoefb);
                        return flag;


                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIVCCCOEFB");
                        return true;
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
        public override bool ReadVcccoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFA", out i))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIVCCCOEFA");
                        return true;
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
        public override bool ReadVcccoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIVCCCOEFB");
                        return true;
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
        public override bool SetVcccoefc(string vcccoefc)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMIVCCCOEFC To" + vcccoefc);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIVCCCOEFC");
                        return true;
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
        public override bool ReadVcccoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIVCCCOEFC");
                        return true;
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

        #region  DmiRxPower

        public override bool SetRxpcoefa(string rxcoefa)
        {

            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
                    {
                        Engmod(3);
                        double mRxCoefaToWrite = DutStruct[i].AmplifyCoeff * Convert.ToDouble(rxcoefa);//amplify coefficient before write
                        string sRxCoefaToWrite = mRxCoefaToWrite.ToString();
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, sRxCoefaToWrite, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMIRXPOWERCOEFA To" + rxcoefa);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIRXPOWERCOEFA");
                        return true;
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
        public override bool ReadRxpcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIRXPOWERCOEFA");
                        return true;
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
        public override bool SetRxpcoefb(string rxcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, rxcoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMIRXPOWERCOEFB To" + rxcoefb);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIRXPOWERCOEFB");

                        return true;
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
        public override bool ReadRxpcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIRXPOWERCOEFB");

                        return true;
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
        public override bool SetRxpcoefc(string rxcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, rxcoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMIRXPOWERCOEFC To" + rxcoefc);

                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIRXPOWERCOEFC");
                        return true;
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

        public override bool ReadRxpcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFC"))
                    {

                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIRXPOWERCOEFC");
                        return true;
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


#endregion

        //NEW ADD
      

        public override bool SetTxAuxcoefa(string TxAuxcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXAUXCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, TxAuxcoefa, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMITXAUXCOEFA To" + TxAuxcoefa);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXAUXCOEFA");
                        return true;
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
        public override bool ReadTxAuxcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXAUXCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXAUXCOEFA");
                        return true;
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
        public override bool SetTxAuxcoefb(string TxAuxcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXAUXCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, TxAuxcoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMITXAUXCOEFB To" + TxAuxcoefb);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXAUXCOEFB");
                        return true;
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
        public override bool ReadTxAuxcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "DMITXAUXCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXAUXCOEFB");
                        return true;
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
        public override bool SetTxAuxcoefc(string TxAuxcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXAUXCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, TxAuxcoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMITXAUXCOEFC To" + TxAuxcoefc);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXAUXCOEFC");
                        return true;
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
        public override bool ReadTxAuxcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "DMITXAUXCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXAUXCOEFC");
                        return true;
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
        public override bool SetLaTempcoefa(string LaTempcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMILATMPCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, LaTempcoefa, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMILATMPCOEFA To" + LaTempcoefa);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMILATMPCOEFA");
                        return true;
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
        public override bool ReadLaTempcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "DMILATMPCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMILATMPCOEFA");
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
        public override bool SetLaTempcoefb(string LaTempcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMILATMPCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, LaTempcoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMILATMPCOEFB To" + LaTempcoefb);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMILATMPCOEFB");
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
        public override bool ReadLaTempcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "DMILATMPCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMILATMPCOEFB");
                        return true;
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
        public override bool SetLaTempcoefc(string LaTempcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMILATMPCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, LaTempcoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMILATMPCOEFC To" + LaTempcoefc);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMILATMPCOEFC");
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
        public override bool ReadLaTempcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMILATMPCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMILATMPCOEFC");
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
       




        #endregion
        public override bool ReadALLcoef(out DutCoefValueStuct[] DutList)
        {
            lock (syncRoot)
            {
                string strcoef = "";
                int coefsum = 0;
                int coefcount = 0;
                try
                {


                    if (DutStruct.Length > 0)
                    {
                        for (int j = 0; j < DutStruct.Length; j++)
                        {
                            if (DutStruct[j].CoefFlag == true)
                            {
                                coefsum++;
                            }
                        }
                    }
                    DutList = new DutCoefValueStuct[coefsum];
                    if (DutList.Length > 0)
                    {
                        for (int j = 0; j < DutStruct.Length; j++)
                        {
                            if (DutStruct[j].CoefFlag == true)
                            {
                                Engmod(3);
                                strcoef = EEPROM.ReadALLCoef(DUT.deviceIndex, 1, DutStruct[j].StartAddress, DutStruct[j].Format, phycialAdress, 1);
                                DutList[coefcount].Page = DutStruct[j].EngPage;
                                DutList[coefcount].StartAddress = DutStruct[j].StartAddress;
                                DutList[coefcount].Length = DutStruct[j].Length;
                                DutList[coefcount].CoefValue = "0x" + strcoef;
                                coefcount++;
                            }

                        }
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no coefficent data");
                        return true;

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
        public override bool ReadALLEEprom(out DutEEPROMInitializeStuct[] DutList)
        {
            lock (syncRoot)
            {
                string strcoef = "";
                int coefcount = 0;
                DutList = new DutEEPROMInitializeStuct[EEpromInitStruct.Length];
                try
                {


                    if (EEpromInitStruct.Length > 0)
                    {
                        for (int j = 0; j < EEpromInitStruct.Length; j++)
                        {
                            Engmod(EEpromInitStruct[j].Page);
                            strcoef = EEPROM.ReadALLEEprom(DUT.deviceIndex, EEpromInitStruct[j].SlaveAddress, EEpromInitStruct[j].StartAddress, phycialAdress, 1);
                            DutList[coefcount].Page = EEpromInitStruct[j].Page;
                            DutList[coefcount].SlaveAddress = EEpromInitStruct[j].SlaveAddress;
                            DutList[coefcount].StartAddress = EEpromInitStruct[j].StartAddress;
                            DutList[coefcount].Length = EEpromInitStruct[j].Length;
                            DutList[coefcount].ItemValue = "0x" + strcoef;
                            coefcount++;

                        }
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no eeprom data");
                        return true;

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
        #region pid
        public override bool PIDCloseOpen(bool pidswitch)
        {
            lock (syncRoot)
            {
                UInt16[] bcoefmdio = new UInt16[1];
                try
                {


                    Engmod(3);
                    if (pidswitch)
                    {

                        bcoefmdio = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0x93F6, IOPort.MDIOSoftHard.SOFTWARE, 1);
                        bcoefmdio[0] |= 0x0080;
                    }
                    else
                    {

                        bcoefmdio = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0x93F6, IOPort.MDIOSoftHard.SOFTWARE, 1);
                        bcoefmdio[0] &= 0xff7f;
                    }


                    IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0x93F6, IOPort.MDIOSoftHard.SOFTWARE, bcoefmdio);
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
        public override bool SetcoefP1(string coefp)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "COEFP1", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, coefp, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET COEFP1" + coefp);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no COEFP1");

                        return true;
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
        public override bool SetcoefI1(string coefi)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "COEFI1", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, coefi, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET COEFI1" + coefi);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no COEFI1");

                        return true;
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
        public override bool SetcoefD1(string coefd)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "COEFD1", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, coefd, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET COEFD1" + coefd);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no COEFD1");

                        return true;
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

        public override bool SetPIDSetpoint1(string setpoint)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "SETPOINT1", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET SETPOINT1" + setpoint);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no SETPOINT1");

                        return true;
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
        public override bool SetcoefP2(string coefp)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "COEFP2", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, coefp, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET COEFP2" + coefp);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no COEFP2");

                        return true;
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
        public override bool SetcoefI2(string coefi)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "COEFI2", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, coefi, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET COEFI2" + coefi);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no COEFI2");

                        return true;
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
        public override bool SetcoefD2(string coefd)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "COEFD2", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, coefd, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET COEFD2" + coefd);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no COEFD2");

                        return true;
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

        public override bool SetPIDSetpoint2(string setpoint)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "SETPOINT2", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET SETPOINT2" + setpoint);
                            return flag;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no SETPOINT2");

                        return true;
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
        #endregion
        #region apc
        public override bool APCON(byte apcswitch)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] buff = new byte[1];
                buff[0] = 0x00;
                try
                {


                    if ((apcswitch & 0x01) == 0x01)
                    {
                        buff[0] |= 0x05;
                    }
                    if ((apcswitch & 0x10) == 0x10)
                    {
                        buff[0] |= 0x50;
                    }

                    if (Algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
                    {
                        Engmod(3);
                        try
                        {

                            bool flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, buff[0].ToString(), DutStruct[i].Format, phycialAdress, 1);

                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no APCCONTROLL");

                        return true;
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
        public override bool APCOFF(byte apcswitch)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] buff = new byte[1];
                buff[0] = 0xff;
                try
                {


                    if ((apcswitch & 0x01) == 0x01)
                    {
                        buff[0] &= 0xf0;

                    }
                    if ((apcswitch & 0x10) == 0x10)
                    {
                        buff[0] &= 0x0f;

                    }
                    if (Algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            bool flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, buff[0].ToString(), DutStruct[i].Format, phycialAdress, 1);

                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no APCCONTROLL");

                        return true;
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

        public override bool APCStatus(out byte apcflag)
        {
            lock (syncRoot)
            {
                //0 OFF.1 ON
                apcflag = 0x00;
                int i = 0;

                byte[] buff = new byte[1];
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            bool flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, buff[0].ToString(), DutStruct[i].Format, phycialAdress, 1);

                            string strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);

                            buff[0] = Convert.ToByte(strcoef);

                            if ((buff[0] & 0x0f) == 0x05)
                            {
                                apcflag |= 0x01;
                            }
                            if ((buff[0] & 0xf0) == 0x50)
                            {
                                apcflag |= 0x10;
                            }


                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no APCCONTROLL");

                        return true;
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
        public override bool APCCloseOpen(bool apcswitch) { return false; }
        #endregion
        public override bool SetdmiRxOffset(string dmiRxOffset)
        {//format 2 uint16
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMIRXOFFSET"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, dmiRxOffset, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET DMIRXOFFSET To" + dmiRxOffset);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMIRXOFFSET");
                        return true;
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
        #region close loop
        public override bool SetCloseLoopcoefa(string CloseLoopcoefa)
        {
            return false;
        }
        public override bool SetCloseLoopcoefb(string CloseLoopcoefb)
        {
            return false;
        }
        public override bool SetCloseLoopcoefc(string CloseLoopcoefc)
        {
            return false;
        }

        #endregion
        #region bias adc threshold
        public override bool SetBiasadcThreshold(byte threshold)
        {
            return false;
        }
        public override bool SetRXPadcThreshold(UInt16 threshold)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "RXADCTHRESHOLD"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, threshold.ToString(), DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET threshold To" + threshold);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RXADCTHRESHOLD");
                        return true;
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
        #endregion
        #region new add as cgr4 new map

        #region  DmiTxPower

        public override bool SetReferenceTemp(string referencetempcoef)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    //if (FindFiledNameChannel(out i, "REFERENCETEMPERATURECOEF"))
                    if (Algorithm.FindFileName(DutStruct, "REFERENCETEMPERATURECOEF", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, referencetempcoef, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET REFERENCETEMPERATURECOEF To" + referencetempcoef);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no REFERENCETEMPERATURECOEF");
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


        public override bool SetTxpProportionLessCoef(string Lesscoef)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXPPROPORTIONLESSCOEF"))
                    // if (Algorithm.FindFileName(DutStruct, "TXPPROPORTIONLESSCOEF", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, Lesscoef, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TXPPROPORTIONLESSCOEF To" + Lesscoef);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXPPROPORTIONLESSCOEF");
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

        public override bool SetTxpProportionGreatCoef(string Greatcoef)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "TXPPROPORTIONGREATCOEF"))
                    //if (Algorithm.FindFileName(DutStruct, "TXPPROPORTIONGREATCOEF", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, Greatcoef, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TXPPROPORTIONGREATCOEF To" + Greatcoef);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXPPROPORTIONGREATCOEF");
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

        public override bool SetTxpFitsCoefa(string Txpcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, Txpcoefa, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET Txpfitscoefa To" + Txpcoefa);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no Txpfitscoefa");
                        return true;
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
        public override bool SetTxpFitsCoefb(string Txpcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, Txpcoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET Txpfitscoefb To" + Txpcoefb);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no Txpfitscoefb");
                        return true;
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

        public override bool SetTxpFitsCoefc(string Txpcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, Txpcoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TxpfitscoefC To" + Txpcoefc);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TxpfitscoefC");
                        return true;
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


        public override bool ReadTxpFitsCoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXPFITSCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXPOWERCOEFA");
                        return true;
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

     
        public override bool ReadTxpFitsCoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXPOWERCOEFB");
                        return true;
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

        public override bool ReadTxpFitsCoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DMITXPOWERCOEFC");
                        return true;
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


#endregion



        public override bool SetRxAdCorSlopcoefb(string rxadcorslopcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;

                //AmplifyCoeff
                try
                {
                    if (FindFiledNameChannel(out i, "RXADCORSLOPCOEFB"))
                    {
                        Engmod(3);

                        double mRxADCSlopCoefBToWrite = DutStruct[i].AmplifyCoeff * Convert.ToDouble(rxadcorslopcoefb);//amplify coefficient before write
                        string sRxADCSlopCoefBToWrite = mRxADCSlopCoefBToWrite.ToString();
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, sRxADCSlopCoefBToWrite, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET RXADCORSLOPCOEFB To" + rxadcorslopcoefb);
                            return flag;
                        }


                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RXADCORSLOPCOEFB");
                        return true;
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
        public override bool SetRxAdCorSlopcoefc(string rxadcorslopcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "RXADCORSLOPCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, rxadcorslopcoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET RXADCORSLOPCOEFC To" + rxadcorslopcoefc);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RXADCORSLOPCOEFC");
                        return true;
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
        public override bool SetRxAdCorOffscoefb(string rxadcoroffsetcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "RXADCOROFFSCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, rxadcoroffsetcoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET RXADCOROFFSCOEFB To" + rxadcoroffsetcoefb);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RXADCOROFFSCOEFB");
                        return true;
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
        public override bool SetRxAdCorOffscoefc(string rxadcoroffsetcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "RXADCOROFFSCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, rxadcoroffsetcoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET RXADCOROFFSCOEFC To" + rxadcoroffsetcoefc);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no RXADCOROFFSCOEFC");
                        return true;
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
        public override bool ReadRx2RawADC(out UInt16 rxrawadc)
        {
            rxrawadc = 0;
            return false;
        }
        #endregion
        #region new add
        public override double ReadEvbVcc()
        {
            lock (syncRoot)
            {
                double EvbVcc = 0;
                //Int32 VAdc=0;

                for (int i = 0; i < 3; i++)
                {

                    IOPort.OpenDevice(0);

                    int k = IOPort.ReadID(0);

                    byte[] Arr = new byte[] { 170, 170, 170, 7, 4 };

                    IOPort.CH375WriteData(0, Arr);

                    byte[] ArrRead = new byte[2];

                    IOPort.CH375ReadData(0, ArrRead);
                    IOPort.CloseDevice(0);

                    Log.SaveLogToTxt("EvbVadc= " + ArrRead[0] + "," + ArrRead[1]);

                    if (EvbVolTageCoef_b == 0 && EvbVolTageCoef_c == 0)
                    {


                        if (!ReadEvbVoltage_Coef())
                        {
                            Log.SaveLogToTxt("EvbCoef 读取失败");
                            // return 0;
                        }
                        else
                        {
                            EvbVcc = (ArrRead[1] * 256 + ArrRead[0]) * EvbVolTageCoef_b + EvbVolTageCoef_c;
                            Log.SaveLogToTxt("EvbVolTageCoef_b=" + EvbVolTageCoef_b + " EvbVolTageCoef_c=" + EvbVolTageCoef_c);
                            // Log.SaveLogToTxt("EvbCoef 读取失败");
                        }

                    }
                    else
                    {
                        EvbVcc = (ArrRead[1] * 256 + ArrRead[0]) * EvbVolTageCoef_b + EvbVolTageCoef_c;
                        Log.SaveLogToTxt("EvbVolTageCoef_b=" + EvbVolTageCoef_b + " EvbVolTageCoef_c=" + EvbVolTageCoef_c);

                    }


                    if (EvbVcc > 2)
                    {
                        break;
                    }

                }
                return EvbVcc;
            }
        }

        private bool ReadEvbVoltage_Coef()
        {
            lock (syncRoot)
            {
                float[] Coef = new float[2];

                IOPort.OpenDevice(0);

                byte[] Arr = new byte[] { 0, 0, 178, 0, 0, 0, 8 };//byte4:Read=0/Write=1; Byte5:RegistAdd/256,Byte6:RegistAdd%256,Byte7=ReadLength

                IOPort.CH375WriteData(0, Arr);

                byte[] ArrRead = new byte[8];

                IOPort.CH375ReadData(0, ArrRead);

                IOPort.CloseDevice(0);

                for (int i = 0; i < 2; i++)
                {
                    byte[] Arr1 = new byte[4];
                    Arr1[0] = ArrRead[i * 4];
                    Arr1[1] = ArrRead[i * 4 + 1];
                    Arr1[2] = ArrRead[i * 4 + 2];
                    Arr1[3] = ArrRead[i * 4 + 3];

                    Coef[i] = BitConverter.ToSingle(Arr1, 0);
                    // logger.AdapterLogString(1,)
                }

                EvbVolTageCoef_b = Coef[0];
                EvbVolTageCoef_c = Coef[1];

                string StrMess = "";
                for (int i = 0; i < 4; i++)
                {
                    StrMess += " OX" + Convert.ToString(ArrRead[i], 16).ToUpper();
                }

                Log.SaveLogToTxt("EvbVolTageCoef_b=" + EvbVolTageCoef_b + StrMess);


                StrMess = "";
                for (int i = 0; i < 4; i++)
                {
                    StrMess += " OX" + Convert.ToString(ArrRead[i + 4], 16).ToUpper();
                }

                Log.SaveLogToTxt("EvbVolTageCoef_c=" + EvbVolTageCoef_c + StrMess);


                return true;
            }
        }
        public override bool CheckEvbVcADC_Coef_flag()
        {
            lock (syncRoot)
            {
                IOPort.OpenDevice(0);

                int k = IOPort.ReadID(0);

                byte[] Arr = new byte[] { 0, 0, 178, 0, 0, 8, 1 };

                IOPort.CH375WriteData(0, Arr);

                byte[] ArrRead = new byte[1];

                IOPort.CH375ReadData(0, ArrRead);

                IOPort.CloseDevice(0);

                if (ArrRead[0] == 0X55)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }


        }
        public override bool SetTxSlopcoefa(string txslopcoefa)
        {
            return false;
        }
        public override bool ReadTxSlopcoefa(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxSlopcoefb(string txslopcoefb)
        {
            return false;
        }
        public override bool ReadTxSlopcoefb(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxSlopcoefc(string txslopcoefc)
        {
            return false;
        }
        public override bool ReadTxSlopcoefc(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxOffsetcoefa(string txoffsetcoefa)
        {
            return false;
        }
        public override bool ReadTxOffsetcoefa(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxOffsetcoefb(string txoffsetcoefb)
        {
            return false;
        }
        public override bool ReadTxOffsetcoefb(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxOffsetcoefc(string txoffsetcoefc)
        {
            return false;
        }
        public override bool ReadTxOffsetcoefc(out string strcoef)
        {
            strcoef = "";
            return false;
        }

        public override bool SetBiasdaccoefa(string biasdaccoefa)
        {
            lock (syncRoot)
            {

                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, biasdaccoefa, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TxTargetBiasDacCoefA  To" + biasdaccoefa);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TxTargetBiasDacCoefA");
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
        public override bool ReadBiasdaccoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETBIASDACCOEFA");
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
        public override bool SetBiasdaccoefb(string biasdaccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, biasdaccoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TxTargetBiasDacCoefB   To" + biasdaccoefb);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TxTargetBiasDacCoefB ");
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
        public override bool ReadBiasdaccoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETBIASDACCOEFB");
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
        public override bool SetBiasdaccoefc(string biasdaccoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, biasdaccoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TXTARGETBIASDACCOEFC   To" + biasdaccoefc);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETBIASDACCOEFC ");
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
        public override bool ReadBiasdaccoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETBIASDACCOEFC");
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
        public override bool SetModdaccoefa(string moddaccoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, moddaccoefa, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TXTARGETMODDACCOEFB   To" + moddaccoefa);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFA ");
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
        public override bool ReadModdaccoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFA");
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
        public override bool SetModdaccoefb(string moddaccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, moddaccoefb, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TXTARGETMODDACCOEFB   To" + moddaccoefb);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFB ");
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
        public override bool ReadModdaccoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFB");
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
        public override bool SetModdaccoefc(string moddaccoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, moddaccoefc, DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET TXTARGETMODDACCOEFB   To" + moddaccoefc);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFC ");
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
        public override bool ReadModdaccoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
                    {
                        Engmod(3);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFC");
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


        public override bool SetTargetLopcoefa(string targetlopcoefa)
        {
            return false;
        }
        public override bool ReadTargetLopcoefa(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTargetLopcoefb(string targetlopcoefb)
        {
            return false;
        }
        public override bool ReadTargetLopcoefb(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTargetLopcoefc(string targetlopcoefc)
        {
            return false;
        }
        public override bool ReadTargetLopcoefc(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool FullFunctionEnable()
        {
            lock (syncRoot)
            {
                try
                {


                    if (DutPRBS != 0)
                    {
                        NetwokLanPRBS(true);
                        SetDutTxPRBS(DutPRBS);
                        SetDutRxPRBS(DutPRBS);
                    }

                    if (DutDatarate != 0)
                    {
                        SetDutDataRate(DutDatarate.ToString());
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
        public override bool SetTxEQcoefa(string TxEQcoefa)
        {
            return false;
        }
        public override bool SetTxEQcoefb(string TxEQcoefb)
        {
            return false;
        }
        public override bool SetTxEQcoefc(string TxEQcoefc)
        {
            return false;
        }
        #endregion

        //configure module work datarate
        private bool NetwokLanPRBS(bool bSwitch)
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {


                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0x8071, IOPort.MDIOSoftHard.SOFTWARE, 1);

                    buff[0] = Convert.ToUInt16(buff[0] & 0xFFF7);

                    if (bSwitch)
                    {
                        buff[0] = Convert.ToUInt16(buff[0] | 0X8);
                    }

                    IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0x8071, IOPort.MDIOSoftHard.SOFTWARE, buff);

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
       private bool SetDutTxPRBS(byte bprbsLength)
        {
            lock (syncRoot)
            {
                UInt16[] buff = new UInt16[1];
                try
                {

                    buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA011, IOPort.MDIOSoftHard.SOFTWARE, 1);

                    buff[0] = Convert.ToUInt16(buff[0] & 0XCFFF);

                    switch (bprbsLength)
                    {
                        case 7://00
                        case 9://00

                            buff[0] = Convert.ToUInt16(buff[0] | (0x4 << 12));
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA011, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 15://01
                            buff[0] = Convert.ToUInt16(buff[0] | (0x5 << 12));
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA011, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 23://10
                            buff[0] = Convert.ToUInt16(buff[0] | (0x6 << 12));
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA011, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        case 31://11
                            buff[0] = Convert.ToUInt16(buff[0] | (0x7 << 12));
                            IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA011, IOPort.MDIOSoftHard.SOFTWARE, buff);
                            break;
                        default://11
                            break;
                    }
                    return true;
                    // IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
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
       private bool SetDutRxPRBS(byte bprbsLength)
       {
           lock (syncRoot)
           {
               UInt16[] buff = new UInt16[1];
               try
               {
                   buff = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, 1);

                   buff[0] = Convert.ToUInt16(buff[0] & 0XCFFF);

                   switch (bprbsLength)
                   {
                       case 7://00
                       case 9://00

                           buff[0] = Convert.ToUInt16(buff[0] | (0x00 << 12));
                           IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, buff);
                           break;
                       case 15://01
                           buff[0] = Convert.ToUInt16(buff[0] | (0x01 << 12));
                           IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, buff);
                           break;
                       case 23://10
                           buff[0] = Convert.ToUInt16(buff[0] | (0x2 << 12));
                           IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, buff);
                           break;
                       case 31://11
                           buff[0] = Convert.ToUInt16(buff[0] | (0x3 << 12));
                           IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA012, IOPort.MDIOSoftHard.SOFTWARE, buff);
                           break;
                       default://11
                           break;
                   }
                   return true;
                   // IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
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
        public  bool SetDutDataRate(string datarate)
        {
            lock (syncRoot)
            {
                try
                {


                    UInt16[] buf = new UInt16[2];
                    buf = IOPort.ReadMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA011, IOPort.MDIOSoftHard.SOFTWARE, 2);//A011:TX,A012:RX
                    switch (datarate)
                    {
                        case "10.31":
                        case "25":
                            buf[0] = (UInt16)(buf[0] & 0xFFF1);       //[3:1] set to 000b
                            buf[1] = (UInt16)(buf[0] & 0xFFF1);
                            break;
                        case "11.2":
                        case "28":
                            buf[0] = (UInt16)(buf[0] & 0xFFF1 | 0x0006);   //[3:1] set to 011b
                            buf[1] = (UInt16)(buf[0] & 0xFFF1 | 0x0006);
                            break;
                        default:
                            return false;
                    }

                    byte[] read = new byte[4];
                    for (int i = 0, j = 0; i < buf.Length; i++)
                    {
                        read[j] = (byte)(buf[i] / 256);
                        read[j + 1] = (byte)(buf[i] & 0xFF);
                        j = j + 2;
                    }

                    IOPort.WriteMDIO(DUT.deviceIndex, 1, phycialAdress, 0xA011, IOPort.MDIOSoftHard.SOFTWARE, buf);
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


        /*cmd for PinCtrl
         * 命令：0xBA
         * bit7    bit6     bit5     bit4      bit3          bit2      bit1         bit0
            sck    alrm2   alrm3     alrm1     glb_alrm       rx_los   mod_Rst  lp_mode
         * 命令：0xA2
         * bit7       bit6     bit5           bit4       bit3      bit2      bit1         bit0
            mdio2   dr        mdio1      MDCIN   Ctrl3    Ctrl2     Ctrl1        TxDis
         * 1:write   0:read    
         * DDR   1:output    0:input
         */

        //control pin return if cmd execute successful,ie.txdisable,lpmode
        //status pin return the pin status,ie.rxlos
        public override bool EVBInitialize()
        {
            lock (syncRoot)
            {
                PinCtrl(CFPEVBPin.TxDisable, false);
                PinCtrl(CFPEVBPin.LpMode, false);
                return true;
            }
        }
        public override bool PinCtrl(CFPEVBPin pin, bool enable)
        {
            lock (syncRoot)
            {
                byte cmd, rw, ddr, setdata, getdata, successState = 0;
                switch (pin)
                {
                    case CFPEVBPin.TxDisable:
                        cmd = 0xA2; ddr = 1; rw = 1; setdata = (byte)(enable ? 1 : 0); successState = 0xAA;
                        break;
                    case CFPEVBPin.LpMode:
                        cmd = 0xBA; ddr = 1; rw = 1; setdata = (byte)(enable ? 1 : 0); successState = 0xAA;
                        break;
                    case CFPEVBPin.RxLos:
                        cmd = 0xBA; ddr = 1; rw = 0; setdata = 0; successState = 0x04;
                        break;
                    default:
                        cmd = 0; ddr = 0; rw = 0; setdata = 0; successState = 0;
                        Log.SaveLogToTxt("Not define this pin ctrl");
                        return false;
                }
                IOPort.CFPCtrlNorPinBase(DUT.deviceIndex, cmd, rw, ddr, setdata, out getdata);
                if ((getdata & successState) == successState)
                    return true;
                return false;
            }
        }

        public override bool ChkRxLosHW()
        {
            lock (syncRoot)
            {
                return PinCtrl(CFPEVBPin.RxLos, true);
            }
        } 
        public override bool SetHWTxDis(bool enable)
        {
            lock (syncRoot)
            {
                return PinCtrl(CFPEVBPin.TxDisable, enable);
            }
        }

        public override bool SetBiasAdcOffset(UInt16 value)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "BIASOFFSETADC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 1, DutStruct[i].StartAddress, value.ToString(), DutStruct[i].Format, phycialAdress, 1);
                            Log.SaveLogToTxt("SET BIASOFFSETADC To" + value);
                            return flag;
                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no BIASOFFSETADC");
                        return true;
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

        public override bool ReadBiasAdcOffset(out ushort biasOffsetAdc)
        {
            lock (syncRoot)
            {
                biasOffsetAdc = 0;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "BIASOFFSETADC", out i))
                    {
                        Engmod(3);
                        try
                        {
                            biasOffsetAdc = EEPROM.readadc(DUT.deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                            Log.SaveLogToTxt("Current biasOffsetAdc is" + biasOffsetAdc);
                            return true;

                        }
                        catch (Exception error)
                        {
                            Log.SaveLogToTxt(error.ToString());
                            return false;
                        }
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no biasOffsetAdc");
                        return true;
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
    }
}
