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
    public class QSFP28DUT:DUT
    {
        enum Writer_Store : byte
        {
            Writer = 0,
            Store = 1
        }

        public bool awstatus_flag = false;// aw status check
        private static object syncRoot = new Object();//used for thread synchronization

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

                    if (!Connect()) return false;
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._UnConnect_0x05000 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
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
                    return false;

                }
            }
        }
        public override bool ChangeChannel(string channel,int syn=0)
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
                    byte[] buff = new byte[5];
                    buff[0] = 0xca;
                    buff[1] = 0x2d;
                    buff[2] = 0x81;
                    buff[3] = 0x5f;
                    buff[4] = engpage;
                    IOPort.WriteReg(DUT.deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
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
        //public override void Engmod()
        //{
        //    byte[] buff = new byte[4];
        //    buff[0] = 0xca;
        //    buff[1] = 0x2d;
        //    buff[2] = 0x81;
        //    buff[3] = 0x5f;
        //    IOPort.WriteReg(DUT.deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //}

        #region dmi
        public override double ReadDmiTemp()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readdmitemp(DUT.deviceIndex, 0xA0, 22);
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
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readdmivcc(DUT.deviceIndex, 0xA0, 26);
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
        {
            lock (syncRoot)
            {
                double dmibias = 0.0;
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 0xA0, 42);
                            break;
                        case 2:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 0xA0, 44);
                            break;
                        case 3:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 0xA0, 46);
                            break;
                        case 4:
                            dmibias = EEPROM.readdmibias(DUT.deviceIndex, 0xA0, 48);
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
        {
            lock (syncRoot)
            {
                double dmitxp = 0.0;
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 0xA0, 50);
                            break;
                        case 2:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 0xA0, 52);
                            break;
                        case 3:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 0xA0, 54);
                            break;
                        case 4:
                            dmitxp = EEPROM.readdmitxp(DUT.deviceIndex, 0xA0, 56);
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
        {
            lock (syncRoot)
            {
                double dmirxp = 0.0;
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 0xA0, 34);
                            break;
                        case 2:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 0xA0, 36);
                            break;
                        case 3:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 0xA0, 38);
                            break;
                        case 4:
                            dmirxp = EEPROM.readdmirxp(DUT.deviceIndex, 0xA0, 40);
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
        #region read a/w
        public override double ReadTempHA()
        {
            lock (syncRoot)
            {
                try
                {


                    double tempha = 0.0;
                    Engmod(3);
                    tempha = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 128);
                    return tempha;
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
        public override double ReadTempLA()
        {
            lock (syncRoot)
            {
                try
                {


                    double templa;
                    Engmod(3);
                    templa = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 130);
                    return templa;
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
        public override double ReadTempLW()
        {
            lock (syncRoot)
            {
                try
                {


                    double templw = 0.0;
                    Engmod(3);
                    templw = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 134);
                    return templw;
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
        public override double ReadTempHW()
        {
            lock (syncRoot)
            {
                try
                {


                    double temphw = 0.0;
                    Engmod(3);
                    temphw = EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 132);
                    return temphw;
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
        public override double ReadVccLA()
        {
            lock (syncRoot)
            {
                try
                {


                    double vccla = 0.0;
                    Engmod(3);
                    vccla = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 146);
                    return vccla;
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
        public override double ReadVccHA()
        {
            lock (syncRoot)
            {
                try
                {


                    double vccha = 0.0;
                    Engmod(3);
                    vccha = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 144);
                    return vccha;
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
        public override double ReadVccLW()
        {
            lock (syncRoot)
            {
                try
                {


                    double vcclw = 0.0;
                    Engmod(3);
                    vcclw = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 150);
                    return vcclw;
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
        public override double ReadVccHW()
        {
            lock (syncRoot)
            {
                try
                {


                    double vcchw = 0.0;
                    Engmod(3);
                    vcchw = EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 148);
                    return vcchw;
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
        public override double ReadBiasLA()
        {
            lock (syncRoot)
            {
                try
                {


                    double biasla = 0.0;
                    Engmod(3);
                    biasla = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 186);
                    return biasla;
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
        public override double ReadBiasHA()
        {
            lock (syncRoot)
            {
                try
                {
                    double biasha = 0.0;
                    Engmod(3);
                    biasha = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 184);
                    return biasha;
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
        public override double ReadBiasLW()
        {
            lock (syncRoot)
            {
                try
                {


                    double biaslw = 0.0;
                    Engmod(3);
                    biaslw = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 190);
                    return biaslw;
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
        public override double ReadBiasHW()
        {
            lock (syncRoot)
            {
                try
                {


                    double biashw = 0.0;
                    Engmod(3);
                    biashw = EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 188);
                    return biashw;
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
        public override double ReadTxpLA()
        {
            lock (syncRoot)
            {
                try
                {


                    double txpla = 0.0;
                    Engmod(3);
                    txpla = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 194);
                    return txpla;
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
        public override double ReadTxpLW()
        {
            lock (syncRoot)
            {
                try
                {


                    double txplw = 0.0;
                    Engmod(3);
                    txplw = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 198);
                    return txplw;
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
        public override double ReadTxpHA()
        {
            lock (syncRoot)
            {
                try
                {


                    double txpha = 0.0;
                    Engmod(3);
                    txpha = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 192);
                    return txpha;
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
        public override double ReadTxpHW()
        {
            lock (syncRoot)
            {
                try
                {


                    double txphw = 0.0;
                    Engmod(3);
                    txphw = EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 196);
                    return txphw;
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
        public override double ReadRxpLA()
        {
            lock (syncRoot)
            {
                try
                {


                    double rxpla = 0.0;
                    Engmod(3);
                    rxpla = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 178);
                    return rxpla;
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
        public override double ReadRxpLW()
        {
            lock (syncRoot)
            {
                try
                {


                    double rxplw = 0.0;
                    Engmod(3);
                    rxplw = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 182);
                    return rxplw;
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
        public override double ReadRxpHA()
        {
            lock (syncRoot)
            {
                try
                {


                    double rxpha = 0.0;
                    Engmod(3);
                    rxpha = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 176);
                    return rxpha;
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
        public override double ReadRxpHW()
        {
            lock (syncRoot)
            {
                try
                {


                    double rxphw = 0.0;
                    Engmod(3);
                    rxphw = EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 180);
                    return rxphw;
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
        #region check a/w
        public override bool ChkTempHA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x80) != 0)
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
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkTempLA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x40) != 0)
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
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkVccHA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x80) != 0)
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

                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkVccLA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x40) != 0)
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
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkBiasHA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x80) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x80) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkBiasLA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x40) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x40) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkTxpHA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x80) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x80) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkTxpLA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x40) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x40) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkRxpHA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x80) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x80) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkRxpLA()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x40) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x40) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkTempHW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x20) != 0)
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
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkTempLW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x10) != 0)
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
                    Log.SaveLogToTxt(error.ToString());
                    return false;

                }
            }
        }
        public override bool ChkVccHW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x20) != 0)
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

                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool ChkVccLW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if ((buff[0] & 0x10) != 0)
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

                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool ChkBiasHW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x20) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x20) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
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
                catch (Exception error)
                {

                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool ChkBiasLW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x10) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x10) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool ChkTxpHW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x20) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x20) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool ChkTxpLW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x10) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x10) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool ChkRxpHW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x20) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x20) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
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
                catch (Exception error)
                {

                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool ChkRxpLW()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x10) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x10) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
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
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        #endregion
        #region read optional status /control bit
        public override bool ChkTxDis()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
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
        public override bool ChkTxFault()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
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
        public override bool ChkRxLos()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {

                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x01) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x02) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x04) != 0)
                            {
                                awstatus_flag = true;
                            }

                            else
                            {
                                awstatus_flag = false;
                            }
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if ((buff[0] & 0x08) != 0)
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
        #endregion
        #region set a/w
        public override void SetTempLA(decimal templa)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settempaw(DUT.deviceIndex, 0xA0, 130, templa);
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
        public override void SetTempHA(decimal tempha)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settempaw(DUT.deviceIndex, 0xA0, 128, tempha);
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
        public override void SetTempLW(decimal templw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settempaw(DUT.deviceIndex, 0xA0, 134, templw);
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
        public override void SetTempHW(decimal temphw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settempaw(DUT.deviceIndex, 0xA0, 132, temphw);
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
        public override void SetVccHW(decimal vcchw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 148, vcchw);
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
        public override void SetVccLW(decimal vcclw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 150, vcclw);
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
        public override void SetVccLA(decimal vccla)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 146, vccla);
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
        public override void SetVccHA(decimal vccha)
        {
            lock (syncRoot)
            {
                try
                {
                    Engmod(3);
                    EEPROM.setvccaw(DUT.deviceIndex, 0xA2, 144, vccha);
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
        public override void SetBiasLA(decimal biasla)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 186, biasla);
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
        public override void SetBiasHA(decimal biasha)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 184, biasha);
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
        public override void SetBiasHW(decimal biashw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 188, biashw);
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
        public override void SetBiasLW(decimal biaslw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 190, biaslw);
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
        public override void SetTxpLW(decimal txplw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 198, txplw);
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
        public override void SetTxpHW(decimal txphw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 196, txphw);
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
        public override void SetTxpLA(decimal txpla)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 194, txpla);
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
        public override void SetTxpHA(decimal txpha)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 192, txpha);
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
        public override void SetRxpHA(decimal rxpha)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 176, rxpha);
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
        public override void SetRxpLA(decimal rxpla)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 178, rxpla);
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
        public override void SetRxpHW(decimal rxphw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 180, rxphw);
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
        public override void SetRxpLW(decimal rxplw)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(3);
                    EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 182, rxplw);
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

        public override bool SetSoftTxDis()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {


                    switch (MoudleChannel)
                    {
                        case 1:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            buff[0] = (byte)(buff[0] | 0x01);
                            IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        case 2:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            buff[0] = (byte)(buff[0] | 0x02);
                            IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        case 3:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            buff[0] = (byte)(buff[0] | 0x04);
                            IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        case 4:
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            buff[0] = (byte)(buff[0] | 0x08);
                            IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        default:
                            break;
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

        public override bool SetSingleChannelTxEnable(byte Channel)
        {
            lock (syncRoot)
            {
                byte[] Readbuff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                byte[] WriterBuff = new byte[1];
                try
                {
                    switch (Channel)
                    {
                        case 1:
                            //  buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            WriterBuff[0] = (byte)((Readbuff[0] | 0X0F) & 0XFE);
                            //    IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        case 2:
                            WriterBuff[0] = (byte)((Readbuff[0] | 0X0F) & 0XFD);
                            // buff[0] = 0XFD;
                            //  IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        case 3:
                            WriterBuff[0] = (byte)((Readbuff[0] | 0X0F) & 0XFB);
                            //buff[0] = 0XFB;
                            // IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        case 4:
                            WriterBuff[0] = (byte)((Readbuff[0] | 0X0F) & 0XF7);
                            //  buff[0] = 0XF7;
                            // IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            break;
                        default:// 0
                            WriterBuff[0] = (byte)((Readbuff[0] | 0X0F) & 0XF0);
                            break;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        IOPort.WriteReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, WriterBuff);
                        Thread.Sleep(100);
                        Readbuff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

                        if (Readbuff[0] == WriterBuff[0])
                        {
                            return true;
                        }
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
        #region w/r  sn/pn
        public override string ReadSn()
        {
            lock (syncRoot)
            {
                try
                {


                    string sn = "";
                    Engmod(0);
                    sn = EEPROM.ReadSn(DUT.deviceIndex, 0xA0, 196);
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
                    Engmod(0);
                    pn = EEPROM.ReadPn(DUT.deviceIndex, 0xA0, 168);
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
            lock (syncRoot)
            {
                try
                {


                    Engmod(0);
                    EEPROM.SetSn(DUT.deviceIndex, 0xA0, 196, sn);
                    //System.Threading.Thread.Sleep(1000);
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
        public override void SetPn(string pn)
        {
            lock (syncRoot)
            {
                try
                {


                    Engmod(0);
                    EEPROM.SetPn(DUT.deviceIndex, 0xA0, 168, pn);
                    //System.Threading.Thread.Sleep(1000);
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
        //read manufacture data
        #region fwrev
        public override string ReadFWRev()
        {
            lock (syncRoot)
            {
                try
                {


                    string fwrev = "";
                    Engmod(4);
                    fwrev = EEPROM.ReadFWRev(DUT.deviceIndex, 0xA0, 128);
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
        #region  adc
        public override bool ReadTempADC( out UInt16 tempadc,byte tempselect)
        {
            lock (syncRoot)
            {
                tempadc = 0;
                int i = 0;
                try
                {


                    switch (tempselect)
                    {
                        case 1:
                            if (Algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    tempadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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

                        case 2:
                            if (Algorithm.FindFileName(DutStruct, "TEMPERATURE2ADC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    tempadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
                                    Log.SaveLogToTxt("Current TEMPERATURE2ADC is" + tempadc);
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
                                Log.SaveLogToTxt("there is no TEMPERATURE2ADC");
                                return true;
                            }



                        default:
                            if (Algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    tempadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            tempadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        public override bool ReadVccADC(out UInt16 vccadc, byte vccselect)
        {//vccselect 1 VCC1ADC
            lock (syncRoot)
            {
                vccadc = 0;
                int i = 0;
                try
                {


                    switch (vccselect)
                    {
                        case 1:
                            if (Algorithm.FindFileName(DutStruct, "VCCADC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    vccadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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

                        case 2:
                            if (Algorithm.FindFileName(DutStruct, "VCC2ADC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    vccadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
                                    Log.SaveLogToTxt("Current VCC2ADC is" + vccadc);
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
                                Log.SaveLogToTxt("there is no VCC2ADC");
                                return true;
                            }

                        case 3:
                            if (Algorithm.FindFileName(DutStruct, "VCC3ADC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    vccadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
                                    Log.SaveLogToTxt("Current VCC3ADC is" + vccadc);
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
                                Log.SaveLogToTxt("there is no VCC3ADC");
                                return true;
                            }

                        default:
                            if (Algorithm.FindFileName(DutStruct, "VCCADC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    vccadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        {
            lock (syncRoot)
            {
                vccadc = 0;
                int i = 0;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "VCCADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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

        public override bool ReadAPDTempAdc(out int APDTempAdc)
        {
            lock (syncRoot)
            {
                APDTempAdc = 0;
                int i = 0;
                try
                {
                    if (FindFiledNameChannel(out i, "APDTempADC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            ushort TempValue = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);

                            if (TempValue > 127 * 256)
                            {
                                APDTempAdc = TempValue - 256 * 256;
                            }
                            else
                            {
                                APDTempAdc = TempValue;
                            }

                            Log.SaveLogToTxt("Current APDTempAdc is" + APDTempAdc);
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
                        Log.SaveLogToTxt("there is no APDTempAdc");
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

        public override bool ReadBiasADC( out UInt16 biasadc)
        {
            lock (syncRoot)
            {
                biasadc = 0;
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TXBIASADC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            biasadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        public override bool ReadTxpADC( out UInt16 txpadc)
        {
            lock (syncRoot)
            {
                txpadc = 0;
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "TXPOWERADC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            txpadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        public override bool ReadRxpADC( out UInt16 rxpadc)
        {
            lock (syncRoot)
            {
                rxpadc = 0;
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "RXPOWERADC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            rxpadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        public override bool ReadTECADC(out ushort Tecadc)
        {
            lock (syncRoot)
            {

                Tecadc = 0;
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "TECCURRENTADC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            Tecadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            Log.SaveLogToTxt("Current Tecadc is" + Tecadc);
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
                        Log.SaveLogToTxt("there is no Tecadc");
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
            lock (syncRoot)
            {
                try
                {


                    return IOPort.ReadReg(DUT.deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, readLength);
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
        public override byte[] WriteReg(int deviceIndex, int deviceAddress, int regAddress, byte[] dataToWrite)
        {
            lock (syncRoot)
            {
                try
                {


                    return IOPort.WriteReg(DUT.deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
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
        public override bool WritePort(int id, int deviceIndex, int Port, int DDR)
        {
            lock (syncRoot)
            {
                try
                {


                    return IOPort.WritePort(id, deviceIndex, Port, DDR);
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
        public override byte[] ReadPort(int id, int deviceIndex, int Port, int DDR)
        {
            lock (syncRoot)
            {
                try
                {


                    return IOPort.ReadPort(id, deviceIndex, Port, DDR);
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
        
       
        #region set coef
        public override bool SetTempcoefb(string tempcoefb, byte TempSelect) 
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    switch (TempSelect)
                    {
                        case 1:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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
                        case 2:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMP2COEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMITEMP2COEFB To" + tempcoefb);
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
                                Log.SaveLogToTxt("there is no DMITEMP2COEFB");
                                return true;
                            }
                        default:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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
                strcoef = "";
                int i = 0;
                try
                {


                    switch (TempSelect)
                    {
                        case 1:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        case 2:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMP2COEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                                Log.SaveLogToTxt("there is no DMITEMP2COEFB");
                                return true;
                            }
                        default:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetTempcoefc(string tempcoefc, byte TempSelect) 
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    switch (TempSelect)
                    {
                        case 1:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
                        case 2:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMP2COEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMITEMP2COEFC To" + tempcoefc);
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
                                Log.SaveLogToTxt("there is no DMITEMP2COEFC");
                                return true;
                            }
                        default:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    switch (TempSelect)
                    {
                        case 1:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        case 2:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMP2COEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                                Log.SaveLogToTxt("there is no DMITEMP2COEFC");
                                return true;
                            }
                        default:
                            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetVcccoefb(string vcccoefb, byte vccselect)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    switch (vccselect)
                    {
                        case 1:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMIVCCCOEFB To" + vcccoefb);
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
                                Log.SaveLogToTxt("there is no DMIVCCCOEFB");
                                return true;
                            }
                        case 2:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC2COEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMIVCC2COEFB To" + vcccoefb);
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
                                Log.SaveLogToTxt("there is no DMIVCC2COEFB");
                                return true;
                            }
                        case 3:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC3COEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMIVCC3COEFB To" + vcccoefb);
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
                                Log.SaveLogToTxt("there is no DMIVCC3COEFB");
                                return true;
                            }
                        default:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMIVCCCOEFB To" + vcccoefb);
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
                                Log.SaveLogToTxt("there is no DMIVCCCOEFB");
                                return true;
                            }
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DMIVCCCOEFB To" + vcccoefb);
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
        public override bool ReadVcccoefb(out string strcoef, byte vccselect)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    switch (vccselect)
                    {
                        case 1:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        case 2:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC2COEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                                Log.SaveLogToTxt("there is no DMIVCC2COEFB");
                                return true;
                            }
                        case 3:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC3COEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                                Log.SaveLogToTxt("there is no DMIVCC3COEFB");
                                return true;
                            }
                        default:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetVcccoefc(string vcccoefc, byte vccselect)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    switch (vccselect)
                    {
                        case 1:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
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
                        case 2:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC2COEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMIVCC2COEFC To" + vcccoefc);
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
                                Log.SaveLogToTxt("there is no DMIVCC2COEFC");
                                return true;
                            }
                        case 3:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC3COEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                                    Log.SaveLogToTxt("SET DMIVCC3COEFC To" + vcccoefc);
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
                                Log.SaveLogToTxt("there is no DMIVCC3COEFC");
                                return true;
                            }
                        default:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
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
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    switch (vccselect)
                    {
                        case 1:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        case 2:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC2COEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                                Log.SaveLogToTxt("there is no DMIVCC2COEFC");
                                return true;
                            }
                        case 3:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCC3COEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                                Log.SaveLogToTxt("there is no DMIVCC3COEFC");
                                return true;
                            }
                        default:
                            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                            if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                            {
                                Engmod(DutStruct[i].EngPage);
                                try
                                {
                                    strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefa, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefb, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefc, DutStruct[i].Format);
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

                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetTxSlopcoefa(string txslopcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefa, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DMITXPOWERSLOPCOEFA To" + txslopcoefa);
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
                        Log.SaveLogToTxt("there is no DMITXPOWERSLOPCOEFA");

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
        public override bool ReadTxSlopcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no DMITXPOWERSLOPCOEFA");

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
        public override bool SetTxSlopcoefb(string txslopcoefb)
        {
            lock (syncRoot)
            {

                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DMITXPOWERSLOPCOEFB To" + txslopcoefb);
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
                        Log.SaveLogToTxt("there is no DMITXPOWERSLOPCOEFB");
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
        public override bool ReadTxSlopcoefb(out string strcoef)
        {
            lock (syncRoot)
            {

                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no DMITXPOWERSLOPCOEFB");
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
        public override bool SetTxSlopcoefc(string txslopcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DMITXPOWERSLOPCOEFC To" + txslopcoefc);
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
                        Log.SaveLogToTxt("there is no DMITXPOWERSLOPCOEFC");
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
        public override bool ReadTxSlopcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no DMITXPOWERSLOPCOEFC");
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
        public override bool SetTxOffsetcoefa(string txoffsetcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefa, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DMITXPOWEROFFSETCOEFA To" + txoffsetcoefa);
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
                        Log.SaveLogToTxt("there is no DMITXPOWEROFFSETCOEFA");

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
        public override bool ReadTxOffsetcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no DMITXPOWEROFFSETCOEFA");

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
        public override bool SetTxOffsetcoefb(string txoffsetcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DMITXPOWEROFFSETCOEFB To" + txoffsetcoefb);
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
                        Log.SaveLogToTxt("there is no DMITXPOWEROFFSETCOEFB");
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
        public override bool ReadTxOffsetcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no DMITXPOWEROFFSETCOEFB");
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
        public override bool SetTxOffsetcoefc(string txoffsetcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {



                    if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DMITXPOWEROFFSETCOEFC To" + txoffsetcoefc);
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
                        Log.SaveLogToTxt("there is no DMITXPOWEROFFSETCOEFC");

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
        public override bool ReadTxOffsetcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no DMITXPOWEROFFSETCOEFC");

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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefa, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXTARGETBIASDACCOEFA To" + biasdaccoefa);
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
                        Log.SaveLogToTxt("there is no TXTARGETBIASDACCOEFA");
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXTARGETBIASDACCOEFB To" + biasdaccoefb);
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
                        Log.SaveLogToTxt("there is no TXTARGETBIASDACCOEFB");
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXTARGETBIASDACCOEFC To" + biasdaccoefc);
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
                        Log.SaveLogToTxt("there is no TXTARGETBIASDACCOEFC");

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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefa, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXTARGETMODDACCOEFA To" + moddaccoefa);

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
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFA");

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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXTARGETMODDACCOEFB To" + moddaccoefb);
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
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFB");

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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXTARGETMODDACCOEFC To" + moddaccoefc);
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
                        Log.SaveLogToTxt("there is no TXTARGETMODDACCOEFC");

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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetTargetLopcoefa(string targetlopcoefa)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TARGETLOPCOEFA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefa, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TARGETLOPCOEFA To" + targetlopcoefa);
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
                        Log.SaveLogToTxt("there is no TARGETLOPCOEFA");
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
        public override bool ReadTargetLopcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TARGETLOPCOEFA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no TARGETLOPCOEFA");
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
        public override bool SetTargetLopcoefb(string targetlopcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TARGETLOPCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TARGETLOPCOEFB To" + targetlopcoefb);
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
                        Log.SaveLogToTxt("there is no TARGETLOPCOEFB");
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
        public override bool ReadTargetLopcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {


                    if (FindFiledNameChannel(out i, "TARGETLOPCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no TARGETLOPCOEFB");
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
        public override bool SetTargetLopcoefc(string targetlopcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TARGETLOPCOEFC To" + targetlopcoefc);
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
                        Log.SaveLogToTxt("there is no TARGETLOPCOEFC");

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
        public override bool ReadTargetLopcoefc( out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                try
                {
                    if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        Log.SaveLogToTxt("there is no TARGETLOPCOEFC");

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
        
        public override bool SetTempcoefa(string tempcoefa)
        {
            return false;
        }
        public override bool ReadTempcoefa(out string strcoef)
        {
            strcoef = ""; return false;
        }
        public override bool SetVcccoefa(string vcccoefa)
        {
            return false;
        }
        public override bool ReadVcccoefa(out string strcoef)
        {
            strcoef = ""; return false;
        }

        public override bool SetAPDdaccoefa(string apddaccoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DmiAPDCoefA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, apddaccoefa, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DmiAPDCoefA To" + apddaccoefa);
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
                        Log.SaveLogToTxt("there is no DmiAPDCoefA");
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

        public override bool SetAPDdaccoefb(string apddaccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DmiAPDCoefB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, apddaccoefb, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET DmiAPDCoefB To" + apddaccoefb);
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
                    Log.SaveLogToTxt("there is no DmiAPDCoefB");
                    return true;
                }
            }
        }

        public override bool SetAPDdaccoefc(string apddaccoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "DmiAPDCoefC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, apddaccoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET DmiAPDCoefC To" + apddaccoefc);
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
                        Log.SaveLogToTxt("there is no DmiAPDCoefC");
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
                        Engmod(DutStruct[i].EngPage);

                        IOPort.WriteReg(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        return true;

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
                            IOPort.WriteReg(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
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

        public override bool APCStatus(out byte apcflag)
        {
            //0 OFF.1 ON
            lock (syncRoot)
            {
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
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

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

        #region close loop
        public override bool SetCloseLoopcoefa(string CloseLoopcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "CLOSELOOPCOEFA"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, CloseLoopcoefa, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET CLOSELOOPCOEFA" + CloseLoopcoefa);
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
                        Log.SaveLogToTxt("there is no CLOSELOOPCOEFA");

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
        public override bool SetCloseLoopcoefb(string CloseLoopcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "CLOSELOOPCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, CloseLoopcoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET CLOSELOOPCOEFB" + CloseLoopcoefb);
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
                        Log.SaveLogToTxt("there is no CLOSELOOPCOEFB");

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
        public override bool SetCloseLoopcoefc(string CloseLoopcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "CLOSELOOPCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, CloseLoopcoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET CLOSELOOPCOEFC" + CloseLoopcoefc);
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
                        Log.SaveLogToTxt("there is no CLOSELOOPCOEFC");

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
        #region PID
        public override bool SetPIDSetpoint(string setpoint)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "SETPOINT"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET SETPOINT" + setpoint);
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
                        Log.SaveLogToTxt("there is no SETPOINT");

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
        public override bool SetcoefP(string coefp)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "COEFP"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, coefp, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET COEFP" + coefp);
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
                        Log.SaveLogToTxt("there is no COEFP");

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
        public override bool SetcoefI(string coefi)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "COEFI"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, coefi, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET COEFI" + coefi);
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
                        Log.SaveLogToTxt("there is no COEFI");

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
        public override bool SetcoefD(string coefd)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "COEFD"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET COEFD" + coefd);
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
                        Log.SaveLogToTxt("there is no COEFD");

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
        public override bool PIDCloseOpen(bool pidswitch) { return false; }
        public override bool SetcoefP1(string p) { return false; }
        public override bool SetcoefI1(string i) { return false; }
        public override bool SetcoefD1(string d) { return false; }
        public override bool SetPIDSetpoint1(string setpoint) { return false; }
        public override bool SetcoefP2(string p) { return false; }
        public override bool SetcoefI2(string i) { return false; }
        public override bool SetcoefD2(string d) { return false; }
        public override bool SetPIDSetpoint2(string setpoint) { return false; }
        #endregion
        #region bias adc threshold
        public override bool SetBiasadcThreshold(byte threshold)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] adcthreshold = new byte[1];
                try
                {


                    if (FindFiledNameChannel(out i, "BIASADCTHRESHOLD"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            adcthreshold[0] = threshold;
                            WriteReg(DUT.deviceIndex, 0xa0, DutStruct[i].StartAddress, adcthreshold);
                            //flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET BIASADCTHRESHOLD" + threshold);
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
                        Log.SaveLogToTxt("there is no BIASADCTHRESHOLD");

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
        public override bool SetRXPadcThreshold(UInt16 threshold)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] adcthreshold = new byte[1];
                try
                {


                    if (FindFiledNameChannel(out i, "RXADCTHRESHOLD"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        // DutStruct[i].
                        if (DutStruct[i].Length == 1 && threshold > 255)
                        {
                            Log.SaveLogToTxt(" RXADCTHRESHOLD TO Large");
                            return false;
                        }
                        try
                        {
                            adcthreshold = Algorithm.Uint16DataConvertoBytes(threshold);
                            WriteReg(DUT.deviceIndex, 0xa0, DutStruct[i].StartAddress, adcthreshold);
                            //flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET RXADCTHRESHOLD" + threshold);
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
        public override bool SetReferenceTemp(string referencetempcoef)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "REFERENCETEMPERATURECOEF", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, referencetempcoef, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET REFERENCETEMPERATURECOEF" + referencetempcoef);
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
        public override bool SetTxpProportionLessCoef(string Lesscoef)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXPPROPORTIONLESSCOEF"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, Lesscoef, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXPPROPORTIONLESSCOEF" + Lesscoef);
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

        public override bool SetTxpProportionGreatCoef(string Greatcoef)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {

                    if (FindFiledNameChannel(out i, "TXPPROPORTIONGREATCOEF"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, Greatcoef, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXPPROPORTIONGREATCOEF" + Greatcoef);
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

  
		 public override float GetTxpProportionGreatCoef()
        {
            lock (syncRoot)
            {
                int i = 0;

                if (FindFiledNameChannel(out i, "TXPPROPORTIONGREATCOEF"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        string Str = EEPROM.ReadCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);

                        Log.SaveLogToTxt("Read TXPPROPORTIONGREATCOEF" + Str);
                        return Convert.ToSingle(Str);

                    }
                    catch (Exception error)
                    {
                        Log.SaveLogToTxt(error.ToString());
                        return 9999;
                    }
                }
                else
                {
                    Log.SaveLogToTxt("there is no TXPPROPORTIONGREATCOEF");

                    return 9999;
                }
            }
        }
        public override bool SetTxpFitsCoefa(string txpfitscoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXPFITSCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txpfitscoefa, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET TXPFITSCOEFA" + txpfitscoefa);
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
                    Log.SaveLogToTxt("there is no TXPFITSCOEFA");

                    return true;
                }
            }
        }

        public override bool SetTxpFitsCoefb(string txpfitscoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXPFITSCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txpfitscoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXPFITSCOEFB" + txpfitscoefb);
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
                        Log.SaveLogToTxt("there is no TXPFITSCOEFB");

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
        public override bool SetTxpFitsCoefc(string txpfitscoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "TXPFITSCOEFC"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, txpfitscoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET TXPFITSCOEFC" + txpfitscoefc);
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
                        Log.SaveLogToTxt("there is no TXPFITSCOEFC");

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
        


        //new add as cgr4 new map
        public override bool SetRxAdCorSlopcoefb(string rxadcorslopcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                try
                {


                    if (FindFiledNameChannel(out i, "RXADCORSLOPCOEFB"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcorslopcoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET RXADCORSLOPCOEFB" + rxadcorslopcoefb);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcorslopcoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET RXADCORSLOPCOEFC" + rxadcorslopcoefc);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcoroffsetcoefb, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET RXADCOROFFSCOEFB" + rxadcoroffsetcoefb);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcoroffsetcoefc, DutStruct[i].Format);
                            Log.SaveLogToTxt("SET RXADCOROFFSCOEFC" + rxadcoroffsetcoefc);
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
            lock (syncRoot)
            {
                rxrawadc = 0;
                int i = 0;

                try
                {


                    if (Algorithm.FindFileName(DutStruct, "RX2RAWADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            rxrawadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            Log.SaveLogToTxt("there is RX2RAWADC" + rxrawadc);

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
                        Log.SaveLogToTxt("there is no RX2RAWADC");
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
                                Engmod(DutStruct[j].EngPage);
                                strcoef = EEPROM.ReadALLCoef(DUT.deviceIndex, 0xA0, DutStruct[j].StartAddress, DutStruct[j].Format);
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
                try
                {


                    DutList = new DutEEPROMInitializeStuct[EEpromInitStruct.Length];
                    if (EEpromInitStruct.Length > 0)
                    {
                        for (int j = 0; j < EEpromInitStruct.Length; j++)
                        {
                            Engmod(EEpromInitStruct[j].Page);
                            strcoef = EEPROM.ReadALLEEprom(DUT.deviceIndex, EEpromInitStruct[j].SlaveAddress, EEpromInitStruct[j].StartAddress);
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
        public override double ReadEvbVcc()
        {
            lock (syncRoot)
            {
                double EvbVcc = 0;
                //Int32 VAdc=0;
                try
                {


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

        private bool ReadEvbVoltage_Coef()
        {
            lock (syncRoot)
            {
                try
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
        public override bool CheckEvbVcADC_Coef_flag() 
        {
            lock (syncRoot)
            {
                try
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

        private bool CDR_Enable()
        {
            lock (syncRoot)
            {
                byte[] dataToWrite = new byte[1];
                byte[] dataReadArray;

                try
                {


                    if (IsCDROn)
                    {
                        dataToWrite[0] = 0xff;
                    }
                    else
                    {
                        dataToWrite[0] = 0;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        IOPort.WriteReg(DUT.deviceIndex, 0XA0, 98, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
                        Thread.Sleep(100);
                        dataReadArray = IOPort.ReadReg(DUT.deviceIndex, 0XA0, 98, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

                        if (dataReadArray[0] == dataToWrite[0])
                        {
                            return true;
                        }
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

        public override bool TxAllChannelEnable()
        {

            lock (syncRoot)
            {

                byte[] dataToWrite = { 0X00 };
                byte[] dataReadArray;
                try
                {


                    for (int i = 0; i < 3; i++)
                    {
                        IOPort.WriteReg(DUT.deviceIndex, 0XA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
                        dataReadArray = IOPort.ReadReg(DUT.deviceIndex, 0XA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        if (dataReadArray[0] == 0x00)
                        {
                            return true;
                        }
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
        private bool HightPowerClass_Enable()
        {
            lock (syncRoot)
            {
                try
                {


                    byte[] dataToWrite = { 0X04 };
                    byte[] dataReadArray;
                    for (int i = 0; i < 3; i++)
                    {
                        IOPort.WriteReg(DUT.deviceIndex, 0XA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
                        Thread.Sleep(100);
                        dataReadArray = IOPort.ReadReg(DUT.deviceIndex, 0XA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        if (dataReadArray[0] == 0x04)
                        {
                            return true;
                        }
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
        public override bool FullFunctionEnable()
        {
            lock (syncRoot)
            {
                int i = 0;
                try
                {


                    for (i = 0; i < 3; i++)
                    {


                        if (CDR_Enable() && HightPowerClass_Enable() && TxAllChannelEnable())
                        {
                            return true;
                        }

                    }
                    if (i == 3)
                    {

                        System.Windows.Forms.MessageBox.Show(" CDR ByPass Error");
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
        #region  new add 
        public override bool ReadLaTempADC(out UInt16 latempadc)
        {
            latempadc = 0;
            return false;


        }
        public override double ReadDmiLaTemp()
        {
            double dmirxp = 0.0;
            return dmirxp;
        }
        public override bool ChkLaTempHA()
        {
            return false;
        }
        public override bool ChkLaTempLA()
        {
            return false;
        }
        public override bool ChkLaTempHW()
        {
            return false;
        }
        public override bool ChkLaTempLW()
        {
            return false;
        }
        public override bool SetTxpcoefa(string Txpcoefa)
        {
            return false;
        }
        public override bool SetTxpcoefb(string Txpcoefb)
        {
            return false;
        }
        public override bool ReadTxpcoefb(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxpcoefc(string Txpcoefc)
        {

            return false;
        }
        public override bool ReadTxpcoefc(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxAuxcoefa(string TxAuxcoefa)
        {

            return false;
        }
        public override bool ReadTxAuxcoefa(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxAuxcoefb(string TxAuxcoefb)
        {

            return false;
        }
        public override bool ReadTxAuxcoefb(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetTxAuxcoefc(string TxAuxcoefc)
        {

            return false;
        }
        public override bool ReadTxAuxcoefc(out string strcoef)
        {
            strcoef = "";
            return false;
        }

        public override bool SetLaTempcoefa(string LaTempcoefa)
        {

            return false;
        }
        public override bool ReadLaTempcoefa(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetLaTempcoefb(string LaTempcoefb)
        {

            return false;
        }
        public override bool ReadLaTempcoefb(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetLaTempcoefc(string LaTempcoefc)
        {
            return false;
        }
        public override bool ReadLaTempcoefc(out string strcoef)
        {
            strcoef = "";
            return false;
        }
        public override bool SetdmiRxOffset(string dmiRxOffset) { return false; }

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
        public override bool GetRegistValueLimmit(byte ItemType, out int Max)
        {
            lock (syncRoot)
            {
                int i = 1;
                try
                {


                    switch (ItemType)
                    {
                        case 0:
                            if (!FindFiledNameDACLength(out i, "BIASDAC"))
                            {
                                i = 1;
                                Log.SaveLogToTxt("GetIbiasRegistLength Error");
                            }
                            break;
                        case 1:
                            if (!FindFiledNameDACLength(out i, "MODDAC"))
                            {
                                i = 1;
                                Log.SaveLogToTxt("GetIModRegistLength Error");
                            }
                            break;
                        default:
                            break;
                    }

                    Max = Convert.ToInt32(Math.Pow(255, i));

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
     
        
#region  AdjustEml
        public override bool WriteEA(byte ControlType, object DAC)
        {// <param name="ControlType">  1=Write 2=store </param>
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                byte chipset = 0x01;
                if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                {
                    engpage = DutStruct[j].EngPage;
                    startaddr = DutStruct[j].StartAddress;

                }
                else
                {
                    Log.SaveLogToTxt("there is no DEBUGINTERFACE");

                }
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "EADAC"))
                {
                    byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    switch (DriverStruct[i].DriverType)
                    {
                        case 0:
                            chipset = 0x01;
                            break;
                        case 1:
                            chipset = 0x02;
                            break;
                        case 2:
                            chipset = 0x04;
                            break;
                        case 3:
                            chipset = 0x08;
                            break;
                        default:
                            chipset = 0x01;
                            break;

                    }
                    Engmod(engpage);
                    if (ControlType == 1)
                    {
                        WriteDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);

                    }
                    else
                    {
                        StoreDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                    }
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no EADAC");
                    return true;
                }
            }
        }
        public override bool WriteVC(byte ControlType, object DAC)
        {// <param name="ControlType">  1=Write 2=store </param>
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                byte chipset = 0x01;
                if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                {
                    engpage = DutStruct[j].EngPage;
                    startaddr = DutStruct[j].StartAddress;

                }
                else
                {
                    Log.SaveLogToTxt("there is no DEBUGINTERFACE");

                }
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "VCDAC"))
                {
                    byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    switch (DriverStruct[i].DriverType)
                    {
                        case 0:
                            chipset = 0x01;
                            break;
                        case 1:
                            chipset = 0x02;
                            break;
                        case 2:
                            chipset = 0x04;
                            break;
                        case 3:
                            chipset = 0x08;
                            break;
                        default:
                            chipset = 0x01;
                            break;

                    }
                    Engmod(engpage);
                    if (ControlType == 1)
                    {
                        WriteDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);

                    }
                    else
                    {
                        StoreDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                    }
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no VCDAC");
                    return true;
                }
            }
        }
        public override bool WriteVLD(byte ControlType, object DAC)
        {// <param name="ControlType">  1=Write 2=store </param>
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                byte chipset = 0x01;
                if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                {
                    engpage = DutStruct[j].EngPage;
                    startaddr = DutStruct[j].StartAddress;

                }
                else
                {
                    Log.SaveLogToTxt("there is no DEBUGINTERFACE");

                }
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "VLDDAC"))
                {
                    byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    switch (DriverStruct[i].DriverType)
                    {
                        case 0:
                            chipset = 0x01;
                            break;
                        case 1:
                            chipset = 0x02;
                            break;
                        case 2:
                            chipset = 0x04;
                            break;
                        case 3:
                            chipset = 0x08;
                            break;
                        default:
                            chipset = 0x01;
                            break;

                    }
                    Engmod(engpage);
                    if (ControlType == 1)
                    {
                        WriteDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);

                    }
                    else
                    {
                        StoreDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                    }
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no VLDDAC");
                    return true;
                }
            }
        }
        public override bool WriteVG(byte ControlType, object DAC)
        {// <param name="ControlType">  1=Write 2=store </param>
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                byte chipset = 0x01;
                if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                {
                    engpage = DutStruct[j].EngPage;
                    startaddr = DutStruct[j].StartAddress;

                }
                else
                {
                    Log.SaveLogToTxt("there is no DEBUGINTERFACE");

                }
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "VGDAC"))
                {
                    byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    switch (DriverStruct[i].DriverType)
                    {
                        case 0:
                            chipset = 0x01;
                            break;
                        case 1:
                            chipset = 0x02;
                            break;
                        case 2:
                            chipset = 0x04;
                            break;
                        case 3:
                            chipset = 0x08;
                            break;
                        default:
                            chipset = 0x01;
                            break;

                    }
                    Engmod(engpage);
                    if (ControlType == 1)
                    {
                        WriteDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);

                    }
                    else
                    {
                        StoreDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                    }
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no VGDAC");
                    return true;
                }
            }
        }

 
#endregion

        #region  Leo Debug

        public override bool WriteBiasDac1(object DAC)
        {//database 0: LDD 1: AMP 2: DAC 3: CDR
            lock (syncRoot)
            {

                return WriteDac("BiasDac", DAC);
            }
        }

        public override bool WriteModDac1(object DAC)
        {
            lock (syncRoot)
            {
                return WriteDac("ModDAC", DAC);
            }
        }

        private bool WriteDac(string StrItem, object DAC)
        {
            lock (syncRoot)
            {
                int ReadDacValue = 0;

                int i = 0;

                try
                {


                    if (FindFiledNameChannelDAC(out i, StrItem))
                    {

                        bool flag = Algorithm.BitNeedManage(DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);

                        if (!flag)//寄存器全位,不需要做任何处理
                        {
                            for (int k = 0; k < 3; k++)
                            {

                                if (!Write_Store_DriverRegist(StrItem, DAC, Writer_Store.Writer)) return false;//写DAC值

                                if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读DAC值

                                if (ReadDacValue == Convert.ToInt16(DAC))
                                {
                                    return true;
                                }

                            }
                        }
                        else//寄存器位缺,需要做任何处理
                        {

                            for (int k = 0; k < 3; k++)
                            {
                                int TempReadValue;





                                if (!ReadDAC(StrItem, out TempReadValue)) return false;//读寄存器的全位DAC值

                                //Int32 K =Convert.ToInt32( DAC);
                                //Int32 K1 = Convert.ToInt32(TempReadValue);
                                //TempReadValue;
                                int JoinValue = Algorithm.WriteJointBitValue(Convert.ToInt32(DAC), Convert.ToInt32(TempReadValue), DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理

                                if (!Write_Store_DriverRegist(StrItem, JoinValue, Writer_Store.Writer)) return false;//写入寄存器的全位DAC值

                                if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读取寄存器的全位DAC值

                                int ReadJoinValue = Algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                                if (ReadJoinValue == Convert.ToInt16(DAC))
                                {
                                    return true;
                                }

                            }

                        }
                        Log.SaveLogToTxt("Writer " + StrItem + " Error");
                        return false;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Writer " + StrItem + " Error");
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

        private bool StoreDac(string StrItem, object DAC)
        {
            lock (syncRoot)
            {
                int ReadDacValue = 0;

                int i = 0;

                try
                {



                    if (FindFiledNameChannelDAC(out i, StrItem))
                    {

                        bool flag = Algorithm.BitNeedManage(DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);

                        if (!flag)//寄存器全位,不需要做任何处理
                        {
                            for (int k = 0; k < 3; k++)
                            {

                                if (!Write_Store_DriverRegist(StrItem, DAC, Writer_Store.Store)) return false;//写DAC值

                                if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读DAC值

                                if (ReadDacValue == Convert.ToInt16(DAC))
                                {
                                    return true;
                                }

                            }
                        }
                        else//寄存器位缺,需要做任何处理
                        {

                            for (int k = 0; k < 3; k++)
                            {
                                int TempReadValue;

                                if (!ReadDAC(StrItem, out TempReadValue)) return false;//读寄存器的全位DAC值

                                int JoinValue = Algorithm.WriteJointBitValue(Convert.ToInt32(DAC), Convert.ToInt32(TempReadValue), DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理

                                if (!Write_Store_DriverRegist(StrItem, JoinValue, Writer_Store.Store)) return false;//写入寄存器的全位DAC值

                                if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读取寄存器的全位DAC值

                                int ReadJoinValue = Algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                                if (ReadJoinValue == Convert.ToInt16(DAC))
                                {
                                    return true;
                                }
                            }

                        }
                        Log.SaveLogToTxt("Writer " + StrItem + " Error");
                        return false;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Writer " + StrItem + " Error");
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

        private bool Write_Store_DriverRegist(string StrItem, object DAC, Writer_Store operate)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;

                try
                {
                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, StrItem))
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
                                chipset = 0x04;
                                break;
                            case 3:
                                chipset = 0x08;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        byte[] WriteDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);
                        Engmod(engpage);

                        if (operate == 0)//写
                        {

                            //WriteDriver40g(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteModDacByteArray);
                            WriteDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteDacByteArray, ChipsetControll);
                            //  
                        }
                        else//存
                        {

                            StoreDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteDacByteArray, ChipsetControll);


                        }
                        // Log.SaveLogToTxt("Writer " + StrItem + "Error");
                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Writer " + StrItem + " Error");
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
        private bool ReadDAC(string StrItem, out int ReadDAC)
        {
            lock (syncRoot)
            {

                byte chipset = 0x01;
                // BiasDac = new byte[length];
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                int i = 0;
                ReadDAC = 0;
                // int ReadDAC = 0;

                try
                {


                    //if (FindFiledName(out j, "DEBUGINTERFACE"))
                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    if (FindFiledNameChannelDAC(out i, StrItem))
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
                                chipset = 0x04;
                                break;
                            case 3:
                                chipset = 0x08;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }

                        Engmod(engpage);

                        byte[] TempDacArray = ReadDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, DriverStruct[i].Length, ChipsetControll);

                        for (j = TempDacArray.Length; j > 0; j--)
                        {
                            ReadDAC += Convert.ToUInt16(TempDacArray[j - 1] * Math.Pow(256, DriverStruct[i].Length - j));
                        }

                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Read " + StrItem + "Error");
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

        #region driver


      


        public byte[] WriteDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, byte[] dataToWrite, bool Switch)
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ReadWriteDriverQSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x02, chipset, dataToWrite, Switch);
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
        public byte[] ReadDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, int readLength, bool Switch)
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ReadWriteDriverQSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x01, chipset, new byte[readLength], Switch);
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
        public byte[] StoreDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, byte[] dataToWrite, bool Switch)
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ReadWriteDriverQSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x06, chipset, dataToWrite, Switch);

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

            ////chipset 1tx,2rx,4dac
            lock (syncRoot)
            {
                int j = 0;
                int k = 0;
                byte engpage = 0;
                int startaddr = 0;
                byte chipset = 0x01;
                byte[] temp;
                bool flag = true;
                try
                {


                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        engpage = DutStruct[j].EngPage;
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
                                byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(DriverInitStruct[i].ItemValue, DriverInitStruct[i].Length, DriverInitStruct[i].Endianness);
                                switch (DriverInitStruct[i].DriverType)
                                {
                                    case 0:
                                        chipset = 0x01;
                                        break;
                                    case 1:
                                        chipset = 0x02;
                                        break;
                                    case 2:
                                        chipset = 0x04;
                                        break;
                                    case 3:
                                        chipset = 0x08;
                                        break;
                                    default:
                                        chipset = 0x01;
                                        break;

                                }
                                Engmod(engpage);
                                for (k = 0; k < 3; k++)
                                {
                                    WriteDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                                    // Thread.Sleep(200);  
                                    StoreDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                                    // Thread.Sleep(200);  
                                    temp = new byte[DriverInitStruct[i].Length];
                                    temp = ReadDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, DriverInitStruct[i].Length, ChipsetControll);

                                    if (BitConverter.ToString(temp) == BitConverter.ToString(WriteBiasDacByteArray))
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
            // int j = 0;
            lock (syncRoot)
            {
                int k = 0;
                byte engpage = 0;
                byte[] temp;
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
                            byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(EEpromInitStruct[i].ItemValue, 1, true);
                            engpage = EEpromInitStruct[i].Page;
                            Engmod(engpage);
                            for (k = 0; k < 3; k++)
                            {
                                WriteReg(DUT.deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, WriteBiasDacByteArray);
                                temp = new byte[1];
                                temp = ReadReg(DUT.deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, 1);
                                if (BitConverter.ToString(temp) == BitConverter.ToString(WriteBiasDacByteArray))
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
        public override bool WriteCrossDac(object DAC)
        {//database 0: LDD 1: AMP 2: DAC 3: CDR


            lock (syncRoot)
            {
                try
                {
                    return WriteDac("CrossDac", DAC);
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

        public override bool WriteBiasDac(object DAC)
        {
         #region  Old Code
            //database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac
            //int j = 0;
            //byte engpage = 0;
            //int startaddr = 0;
            //byte chipset = 0x01;
            ////if (FindFiledName(out j, "DEBUGINTERFACE"))
            //if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            //{
            //    engpage = DutStruct[j].EngPage;
            //    startaddr = DutStruct[j].StartAddress;

            //}
            //else
            //{
            //    Log.SaveLogToTxt("there is no DEBUGINTERFACE");

            //}
            //int i = 0;
            //if (FindFiledNameChannelDAC(out i, "BIASDAC"))
            //{
            //    byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
            //    switch (DriverStruct[i].DriverType)
            //    {
            //        case 0:
            //            chipset = 0x01;
            //            break;
            //        case 1:
            //            chipset = 0x02;
            //            break;
            //        case 2:
            //            chipset = 0x04;
            //            break;
            //        case 3:
            //            chipset = 0x08;
            //            break;
            //        default:
            //            chipset = 0x01;
            //            break;

            //    }
            //    Engmod(engpage);
            //    WriteDriver40g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
            //    return true;
            //}
            //else
            //{
            //    Log.SaveLogToTxt("there is no BIASDAC");
            //    return true;
            //}

        #endregion
            lock (syncRoot)
            {
                try
                {
                    return WriteDac("BiasDAC", DAC);
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
        public override int ReadBiasDac()
        {
            lock (syncRoot)
            {
                int Dac;
                try
                {
                    ReadDAC("BiasDAC", out Dac);
                    return Dac;
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
        public override int ReadModDac()
        {
            lock (syncRoot)
            {
                int Dac;
                try
                {


                    ReadDAC("ModDAC", out Dac);
                    return Dac;
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
        public override bool WriteModDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return WriteDac("ModDAC", DAC);
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

        public override bool WriteLOSDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return WriteDac("LOSDAC", DAC);
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

        public override bool WriteLOSDDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {

                    return WriteDac("LOSDDAC", DAC);
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

        public override bool WriteAPDDac(object DAC)
        {
            lock (syncRoot)
            {

                try
                {
                    return WriteDac("APDDAC", DAC);
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

        public override bool WriteEA(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return WriteDac("EADAC", DAC);
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

        public override bool StoreCrossDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return StoreDac("CrossDac", DAC);
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
        public override bool StoreBiasDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return StoreDac("BIASDAC", DAC);
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
        public override bool StoreModDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return StoreDac("MODDAC", DAC);
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
        public override bool StoreLOSDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return StoreDac("LOSDAC", DAC);
                    // if (FindFiledNameChannelDAC(out i, "LOSDAC"))
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
        public override bool StoreLOSDDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {
                    return StoreDac("LOSDDAC", DAC);
                    //if (FindFiledNameChannelDAC(out i, "LOSDDAC"))
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
        public override bool StoreAPDDac(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return StoreDac("APDDAC", DAC);
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
        public override bool StoreEA(object DAC)
        {
            lock (syncRoot)
            {
                try
                {


                    return StoreDac("EADAC", DAC);
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

#region  AdjustTxAdc
        public override bool WriterTxAdcBacklightOffset(object offset)
        {
            lock (syncRoot)
            {
                try
                {


                    if (offset == null)
                    {
                        Log.SaveLogToTxt("there is no offset.");
                        return false;
                    }

                    int i = 0;
                    bool flag = false;
                    if (FindFiledNameChannel(out i, "TxAdcBacklightOffset"))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            //add
                            if (double.IsNaN((double)offset))
                            {
                                byte[] writeData = new byte[2] { 0xFF, 0xFF };
                                this.WriteReg(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, writeData);
                                byte[] readData = this.ReadReg(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, 2);

                                for (int j = 0; j < writeData.Length; j++)
                                {
                                    if (readData[j] != writeData[j])
                                    {
                                        return false;
                                    }
                                }
                                Log.SaveLogToTxt("SET TxAdcBacklightOffset To 0xFF");
                                flag = true;
                            }
                            else
                            {
                                flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, offset.ToString(), DutStruct[i].Format);
                                Log.SaveLogToTxt("SET TxAdcBacklightOffset To" + offset);
                            }
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
                        Log.SaveLogToTxt("there is no offset");
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
                //return false;
            }
        }
        public override bool WriterTxAdcCalibrateMatrix(double[,] matrix)
        { // 
            lock (syncRoot)
            {
                try
                {


                    if (matrix == null)
                    {
                        Log.SaveLogToTxt("there is no coeff.");
                        return false;
                    }

                    int i = 0;
                    bool flag = true;
                    if (Algorithm.FindFileName(DutStruct, "TxAdcCalibrateMatrix", out i))
                    {
                        int StartAddress = DutStruct[i].StartAddress;
                        int WriterAddress = StartAddress;
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            for (int RowNo = 0; RowNo < matrix.GetLength(0); RowNo++)
                            {
                                for (int ColumsNo = 0; ColumsNo < matrix.GetLength(1); ColumsNo++)
                                {
                                    string WriterData = matrix[RowNo, ColumsNo].ToString();
                                    WriterAddress = StartAddress + (RowNo * matrix.GetLength(1) + ColumsNo) * 4;

                                    for (int j = 0; j < 3; j++)
                                    {
                                        //add
                                        if (double.IsNaN(matrix[RowNo, ColumsNo]))
                                        {
                                            byte[] writeData = new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF };
                                            this.WriteReg(DUT.deviceIndex, 0xA0, WriterAddress, writeData);
                                            byte[] readData = this.ReadReg(DUT.deviceIndex, 0xA0, WriterAddress, 4);
                                            for (int k = 0; k < writeData.Length; k++)
                                            {
                                                if (readData[k] != writeData[k])
                                                {
                                                    return false;
                                                }
                                            }
                                            break;
                                        }
                                        //

                                        if (EEPROM.SetCoef(DUT.deviceIndex, 0xA0, WriterAddress, WriterData, DutStruct[i].Format))
                                        {
                                            break;
                                        }

                                        if (j == 2)
                                        {
                                            Log.SaveLogToTxt("TxAdcCalibrateMatrix Row[" + RowNo + "][" + ColumsNo + "]" + "WriterAddress=" + WriterAddress + "WriterData=" + WriterData + " Error");
                                            return false;
                                        }
                                    }
                                }
                            }

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
                        Log.SaveLogToTxt("there is no TARGETLOPCOEFC");
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

    }
}
