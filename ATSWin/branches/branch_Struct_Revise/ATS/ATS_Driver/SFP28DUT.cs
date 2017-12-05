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
    public class SFP28DUT : DUT
    {
        private static object syncRoot = new Object();//used for thread synchronization
        enum Writer_Store:byte
        {
            Writer = 0,
            Store=1
        }


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

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
                return true;
            }
        }
        public override bool ChangeChannel(string channel, int syn = 0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            return true;
        }
        public override bool Connect()
        {
            lock (syncRoot)
            {
                try
                {

                    EquipmentConnectflag = true; ;
                    return EquipmentConnectflag;
                }
                catch (Exception error)
                {
                    Log.SaveLogToTxt(error.ToString());
                    return false;
                }
            }
        }
        public override bool Configure(int syn = 0)
        {
            return true;
        }
        public override void Engmod(byte engpage)
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[5];
                buff[0] = 0xca;
                buff[1] = 0x2d;
                buff[2] = 0x81;
                buff[3] = 0x5f;
                buff[4] = engpage;
                IOPort.WriteReg(DUT.deviceIndex, 0xA2, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
            }
        }
      #region FullFuntion
        public override bool FullFunctionEnable()
        {
            lock (syncRoot)
            {
                int i = 0;
                try
                {


                    for (i = 0; i < 3; i++)
                    {


                        if (CDR_Enable() && TxAllChannelEnable())
                        {
                            return true;
                        }

                    }
                    if (i == 3)
                    {

                        throw new Exception();

                    }


                    return true;
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");

                    // throw new InnoExCeption(error.ID, error.StackTrace);
                    string s = error.StackTrace;
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, s);
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
      #endregion
        #region dmi
        public override double ReadDmiTemp()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readdmitemp(DUT.deviceIndex, 0xA2, 96);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadDmiVcc()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readdmivcc(DUT.deviceIndex, 0xA2, 98);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadDmiBias()
        {
            lock (syncRoot)
            {
                try
                {
                    return EEPROM.readdmibias(DUT.deviceIndex, 0xA2, 100);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadDmiTxp()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readdmitxp(DUT.deviceIndex, 0xA2, 102);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadDmiRxp()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readdmirxp(DUT.deviceIndex, 0xA2, 104);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

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


                    return EEPROM.readtempaw(DUT.deviceIndex, 0xA2, 0);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadTempLA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readtempaw(DUT.deviceIndex, 0xA2, 2);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadTempLW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readtempaw(DUT.deviceIndex, 0xA2, 6);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadTempHW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readtempaw(DUT.deviceIndex, 0xA2, 4);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadVccLA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readvccaw(DUT.deviceIndex, 0xA2, 10);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadVccHA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readvccaw(DUT.deviceIndex, 0xA2, 8);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadVccLW()
        {
            lock (syncRoot)
            {
                try
                {

                    return EEPROM.readvccaw(DUT.deviceIndex, 0xA2, 14);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadVccHW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readvccaw(DUT.deviceIndex, 0xA2, 12);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadBiasLA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readbiasaw(DUT.deviceIndex, 0xA2, 18);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadBiasHA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readbiasaw(DUT.deviceIndex, 0xA2, 16);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadBiasLW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readbiasaw(DUT.deviceIndex, 0xA2, 22);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }

            }
        }
        public override double ReadBiasHW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readbiasaw(DUT.deviceIndex, 0xA2, 20);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadTxpLA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readtxpaw(DUT.deviceIndex, 0xA2, 26);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadTxpLW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readtxpaw(DUT.deviceIndex, 0xA2, 30);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadTxpHA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readtxpaw(DUT.deviceIndex, 0xA2, 24);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadTxpHW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readtxpaw(DUT.deviceIndex, 0xA2, 28);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadRxpLA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readrxpaw(DUT.deviceIndex, 0xA2, 34);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadRxpLW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readrxpaw(DUT.deviceIndex, 0xA2, 38);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadRxpHA()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readrxpaw(DUT.deviceIndex, 0xA2, 32);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override double ReadRxpHW()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.readrxpaw(DUT.deviceIndex, 0xA2, 36);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        #endregion
        #region check a/w
        public override bool ChkTempHA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTempHA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkTempLA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTempLA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkVccHA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkVccHA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkVccLA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkVccLA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkBiasHA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkBiasHA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkBiasLA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkBiasLA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkTxpHA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTxpHA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkTxpLA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTxpLA(DUT.deviceIndex, 0xA2, 112);
            }
        }
        public override bool ChkRxpHA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkRxpHA(DUT.deviceIndex, 0xA2, 113);
            }
        }
        public override bool ChkRxpLA()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkRxpLA(DUT.deviceIndex, 0xA2, 113);
            }
        }
        public override bool ChkTempHW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTempHW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkTempLW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTempLW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkVccHW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkVccHW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkVccLW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkVccLW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkBiasHW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkBiasHW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkBiasLW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkBiasLW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkTxpHW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTxpHW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkTxpLW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkTxpLW(DUT.deviceIndex, 0xA2, 116);
            }
        }
        public override bool ChkRxpHW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkRxpHW(DUT.deviceIndex, 0xA2, 117);
            }
        }
        public override bool ChkRxpLW()
        {
            lock (syncRoot)
            {
                return EEPROM.ChkRxpLW(DUT.deviceIndex, 0xA2, 117);
            }
        }
        #endregion
        #region read optional status /control bit 110

        public override bool ChkTxDis()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ChkTxDis(DUT.deviceIndex, 0xA2, 110);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }

            }
        }

        public override bool ChkTxFault()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ChkTxFault(DUT.deviceIndex, 0xA2, 110);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override bool ChkRxLos()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ChkRxLos(DUT.deviceIndex, 0xA2, 110);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        #endregion
        #region set a/w
        public override void SetTempLA(decimal templa)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA2, 2, templa);
            }
        }
        public override void SetTempHA(decimal tempha)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA2, 0, tempha);
            }
        }
        public override void SetTempLW(decimal templw)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA2, 6, templw);
            }
        }
        public override void SetTempHW(decimal temphw)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA2, 4, temphw);
            }
        }
        public override void SetVccHW(decimal vcchw)
        {
            lock (syncRoot)
            {
              EEPROM.setvccaw(DUT.deviceIndex, 0xA2, 12, vcchw);
            }
        }
        public override void SetVccLW(decimal vcclw)
        {
            lock (syncRoot)
            {
              EEPROM.setvccaw(DUT.deviceIndex, 0xA2, 14, vcclw);
            }
        }
        public override void SetVccLA(decimal vccla)
        {
            lock (syncRoot)
            {
                EEPROM.setvccaw(DUT.deviceIndex, 0xA2, 10, vccla);
            }
        }
        public override void SetVccHA(decimal vccha)
        {
            lock (syncRoot)
            {
              EEPROM.setvccaw(DUT.deviceIndex, 0xA2, 8, vccha);
            }
        }
        public override void SetBiasLA(decimal biasla)
        {
            lock (syncRoot)
            {
              EEPROM.setbiasaw(DUT.deviceIndex, 0xA2, 18, biasla);
            }
        }
        public override void SetBiasHA(decimal biasha)
        {
            lock (syncRoot)
            {
              EEPROM.setbiasaw(DUT.deviceIndex, 0xA2, 16, biasha);
            }
        }
        public override void SetBiasHW(decimal biashw)
        {
            lock (syncRoot)
            {
              EEPROM.setbiasaw(DUT.deviceIndex, 0xA2, 20, biashw);
            }
        }
        public override void SetBiasLW(decimal biaslw)
        {
            lock (syncRoot)
            {
              EEPROM.setbiasaw(DUT.deviceIndex, 0xA2, 22, biaslw);
            }
        }
        public override void SetTxpLW(decimal txplw)
        {
            lock (syncRoot)
            {
              EEPROM.settxpaw(DUT.deviceIndex, 0xA2, 30, txplw);
            }
        }
        public override void SetTxpHW(decimal txphw)
        {
            lock (syncRoot)
            {
              EEPROM.settxpaw(DUT.deviceIndex, 0xA2, 28, txphw);
            }
        }
        public override void SetTxpLA(decimal txpla)
        {
            lock (syncRoot)
            {
              EEPROM.settxpaw(DUT.deviceIndex, 0xA2, 26, txpla);
            }
        }
        public override void SetTxpHA(decimal txpha)
        {
            lock (syncRoot)
            {
              EEPROM.settxpaw(DUT.deviceIndex, 0xA2, 24, txpha);
            }
        }
        public override void SetRxpHA(decimal rxpha)
        {
            lock (syncRoot)
            {
              EEPROM.setrxpaw(DUT.deviceIndex, 0xA2, 32, rxpha);
            }
        }
        public override void SetRxpLA(decimal rxpla)
        {
            lock (syncRoot)
            {
              EEPROM.setrxpaw(DUT.deviceIndex, 0xA2, 34, rxpla);
            }
        }
        public override void SetRxpHW(decimal rxphw)
        {
           lock (syncRoot)
            {
               EEPROM.setrxpaw(DUT.deviceIndex, 0xA2, 36, rxphw);
            }
        }
        public override void SetRxpLW(decimal rxplw)
        {
            lock (syncRoot)
            {
              EEPROM.setrxpaw(DUT.deviceIndex, 0xA2, 38, rxplw);
            }
        }
        public override bool SetSoftTxDis()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    buff[0] = (byte)(buff[0] | 0x40);
                    byte TempBuff = buff[0];
                    for (int i = 0; i < 3; i++)
                    {

                        IOPort.WriteReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        System.Threading.Thread.Sleep(200);
                        buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        if (buff[0] == TempBuff)
                        {
                            return true;
                        }
                    }

                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002);

                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override bool TxAllChannelEnable()
        {

            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    buff[0] = (byte)(buff[0] & 0xBF);
                    byte TempBuff = buff[0];
                    for (int i = 0; i < 3; i++)
                    {

                        IOPort.WriteReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        System.Threading.Thread.Sleep(200);
                        buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        if (buff[0] == TempBuff)
                        {
                            return true;
                        }
                    }
                    throw new Exception();

                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        private bool CDR_Enable()
        {
            lock (syncRoot)
            {
                byte[] buff = new byte[1];
                try
                {
                    if (IsCDROn)
                    {

                        buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        buff[0] = (byte)(buff[0] | 0x08);//确保Bit 3=1
                        byte TempBuff = buff[0];
                        for (int i = 0; i < 3; i++)
                        {

                            IOPort.WriteReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            System.Threading.Thread.Sleep(200);
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if (buff[0] == TempBuff)
                            {
                                break;
                            }

                        }
                        if (buff[0] != TempBuff)
                        {
                            throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002);
                        }

                        buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 118, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        buff[0] = (byte)(buff[0] | 0x08);//确保Bit 3=1
                        TempBuff = buff[0];
                        for (int i = 0; i < 3; i++)
                        {

                            IOPort.WriteReg(DUT.deviceIndex, 0XA2, 118, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                            System.Threading.Thread.Sleep(200);
                            buff = IOPort.ReadReg(DUT.deviceIndex, 0XA2, 118, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                            if (buff[0] == TempBuff)
                            {
                                break;
                            }
                        }
                        if (buff[0] != TempBuff)
                        {
                            throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002);
                        }
                        return true;
                    }
                    else
                    {

                    }
                    throw new Exception();
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

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


                    return EEPROM.ReadSn(DUT.deviceIndex, 0xA0, 68);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override string ReadPn()
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ReadPn(DUT.deviceIndex, 0xA0, 40);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public override void SetSn(string sn)
        {
            lock (syncRoot)
            {
                try
                {


                    EEPROM.SetSn(DUT.deviceIndex, 0xA0, 68, sn);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
                //System.Threading.Thread.Sleep(1000);
            }
        }
        public override void SetPn(string pn)
        {
            lock (syncRoot)
            {
                try
                {
                    EEPROM.SetPn(DUT.deviceIndex, 0xA0, 40, pn);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
                //System.Threading.Thread.Sleep(1000);
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


                    this.Engmod(0);
                    return EEPROM.ReadFWRev(DUT.deviceIndex, 0xA2, 121);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        #endregion
        #region adc
        public override bool ReadTempADC( out UInt16 tempadc)
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
                            tempadc = EEPROM.readadc(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress);
                            Log.SaveLogToTxt("there is TEMPERATUREADC" + tempadc);
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

                }
            }
        }
        public override bool ReadTempADC(out UInt16 tempadc, byte tempselect)
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


                    //if (FindFiledNameVccSelect(out i, "VCCADC"))
                    if (Algorithm.FindFileName(DutStruct, "VCCADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = EEPROM.readadc(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress);
                            Log.SaveLogToTxt("there is VCCADC" + vccadc);
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


                    FindFiledNameChannel(out i, "TXBIASADC");
                    Engmod(DutStruct[i].EngPage);

                    biasadc = EEPROM.readadc(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress);
                    Log.SaveLogToTxt("there is TXBIASADC" + biasadc);
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
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            txpadc = EEPROM.readadc(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress);
                            Log.SaveLogToTxt("there is TXPOWERADC" + txpadc);
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
                        Engmod(DutStruct[i].EngPage);

                        rxpadc = EEPROM.readadc(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress);
                        Log.SaveLogToTxt("there is RXPOWERADC" + rxpadc);
                        return true;


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

                }
            }
        }
        #endregion
        
        #region driver
        public byte[] WriteDriverSFP28(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset, byte[] dataToWrite)
        {
            lock (syncRoot)
            {
                try
                {


                    return EEPROM.ReadWriteDriverSFP28(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x02, chipset, dataToWrite);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        public byte[] ReadDriverSFP28(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset)// 有问题,但是需要确认,默认写长度为2不合适 Leo 2016-3-7
        {
            lock (syncRoot)
            {
                try
                {
                    return EEPROM.ReadWriteDriverSFP28(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x01, chipset, new byte[2]);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        //public byte[] ReadDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset, int Readlength)
        //{
        //    return EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x01, chipset, new byte[Readlength]);
        //}
        public byte[] StoreDriverSFP28(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset, byte[] dataToWrite)
        {
           lock (syncRoot)
           {
               try
               {


                   return EEPROM.ReadWriteDriverSFP28(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x06, chipset, dataToWrite);
               }
               catch (InnoExCeption error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw error;
               }

               catch (Exception error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
               }
            }
        }

        //driver innitialize
        public override bool DriverInitialize()
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac,3cdr
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
                                byte[] WriteBiasDacByteArray = WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(DriverInitStruct[i].ItemValue, 2, DriverInitStruct[i].Endianness);

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
                                        chipset = 0x03;
                                        break;
                                    default:
                                        chipset = 0x01;
                                        break;

                                }
                                Engmod(engpage);
                                for (k = 0; k < 3; k++)
                                {
                                    WriteDriverSFP28(DUT.deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress, chipset, WriteBiasDacByteArray);

                                    StoreDriverSFP28(DUT.deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress, chipset, WriteBiasDacByteArray);
                                    // Thread.Sleep(200);  
                                    //  temp = new byte[DriverInitStruct[i].Length];
                                    temp = ReadDriverSFP28(DUT.deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress, chipset);

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
            }
        }
        //eeprominit
        //联调通过ee
        public override bool EEpromInitialize()
        {
            lock (syncRoot)
            {
                int j = 0;
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
                            byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(EEpromInitStruct[i].ItemValue, EEpromInitStruct[i].Length, true);
                            engpage = EEpromInitStruct[j].Page;
                            Engmod(engpage);
                            for (k = 0; k < 3; k++)
                            {
                                WriteReg(DUT.deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, WriteBiasDacByteArray);
                                // temp = new byte[1];
                                temp = ReadReg(DUT.deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, EEpromInitStruct[i].Length);
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

                }
            }
        }


        //set biasmoddac
        public override bool WriteCrossDac(object crossdac)
        {
            lock (syncRoot)
            {
                try
                {



                    return WriteDac("CROSSDAC", crossdac);
                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }

        #region  OldDriver
        //public override bool WriteBiasDac(object biasdac)
        //{
        //    int j = 0;
        //    byte engpage = 0;
        //    int startaddr = 0;
        //    byte chipset = 0x01;
        //    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        engpage = DutStruct[j].EngPage;
        //        startaddr = DutStruct[j].StartAddress;

        //    }
        //    else
        //    {
        //        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
        //    }
        //    int i = 0;
        //    if (FindFiledNameChannelDAC(out i, "BIASDAC"))
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
        //                chipset = 0x04;
        //                break;
        //            case 3:
        //                chipset = 0x03;
        //                break;
        //            default:
        //                chipset = 0x01;
        //                break;

        //        }
        //        byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
        //        Engmod(engpage);
        //        WriteDriver10g(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteBiasDacByteArray);
        //        return true;
        //    }
        //    else
        //    {
        //        Log.SaveLogToTxt("there is no BIASDAC");
        //        return true;
        //    }
        //}
        //public override bool WriteModDac(object moddac)
        //{
        //    byte chipset = 0x01;
        //    int j = 0;
        //    byte engpage = 0;
        //    int startaddr = 0;
        //    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        engpage = DutStruct[j].EngPage;
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
        //                chipset = 0x04;
        //                break;
        //            case 3:
        //                chipset = 0x03;
        //                break;
        //            default:
        //                chipset = 0x01;
        //                break;

        //        }
        //        byte[] WriteModDacByteArray = Algorithm.ObjectTOByteArray(moddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
        //        Engmod(engpage);
        //        WriteDriver10g(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteModDacByteArray);
        //        return true;
        //    }
        //    else
        //    {
        //        Log.SaveLogToTxt("there is no MODDAC");
        //        return true;
        //    }
        //}
        //public override bool WriteMaskDac(object DAC)
        //{
        //    byte chipset = 0x01;
        //    int j = 0;
        //    byte engpage = 0;
        //    int startaddr = 0;
        //    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        engpage = DutStruct[j].EngPage;
        //        startaddr = DutStruct[j].StartAddress;

        //    }
        //    else
        //    {
        //        Log.SaveLogToTxt("there is no DEBUGINTERFACE");

        //    }
        //    int i = 0;
        //    if (FindFiledNameChannelDAC(out i, "MaskDAC"))
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
        //                chipset = 0x04;
        //                break;
        //            case 3:
        //                chipset = 0x03;
        //                break;
        //            default:
        //                chipset = 0x01;
        //                break;

        //        }
        //        byte[] WriteModDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);
        //        Engmod(engpage);
        //        WriteDriver10g(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteModDacByteArray);
        //        return true;
        //    }
        //    else
        //    {
        //        Log.SaveLogToTxt("there is no MODDAC");
        //        return true;
        //    }
        //}
#endregion
        

  
        //read biasmoddac


        public override int ReadBiasDac()
        {
            lock (syncRoot)
            {
                int DacValue = 0;

                // ReadDAC("ModDac",out ModDac);

                if (ReadDAC("BIASDac", out DacValue))
                {
                    return DacValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no ModDac");

                    return DacValue;
                }
            }
        }

        public override int ReadModDac()
        {
            lock (syncRoot)
            {
                int ModDac = 0;

                if (ReadDAC("ModDac", out ModDac))
                {
                    return ModDac;
                }
                else
                {
                    Log.SaveLogToTxt("there is no ModDac");

                    return ModDac;
                }
            }
        }

 
        #endregion


        #region set coef


        #region  TempDmi

        public override bool SetTempcoefb(string tempcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    double buf = DutStruct[i].AmplifyCoeff * Convert.ToDouble(tempcoefb);
                    tempcoefb = buf.ToString();
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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

        public override bool SetTempcoefb(string tempcoefb, byte TempSelect)
        {
            lock (syncRoot)
            {
                return SetTempcoefb(tempcoefb);
            }
        }
        public override bool ReadTempcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool ReadTempcoefb(out string strcoef, byte TempSelect)
        {
            lock (syncRoot)
            {
                return ReadTempcoefb(out strcoef);
            }
        }
        public override bool SetTempcoefc(string tempcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    double buf = DutStruct[i].AmplifyCoeff * Convert.ToDouble(tempcoefc);
                    tempcoefc = buf.ToString();
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
        public override bool SetTempcoefc(string tempcoefc, byte TempSelect)
        {
            lock (syncRoot)
            {
                return SetTempcoefc(tempcoefc);
            }
        }
        public override bool ReadTempcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                if (Algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        {
            lock (syncRoot)
            {
                return ReadTempcoefc(out strcoef);
            }
        }

        #endregion

        #region  Vcc

        public override bool SetVcccoefb(string vcccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    double buf = DutStruct[i].AmplifyCoeff * Convert.ToDouble(vcccoefb);
                    vcccoefb = buf.ToString();
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET DMIVCCCOEFB" + vcccoefb);
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
        public override bool SetVcccoefb(string vcccoefb, byte vccselect)
        {
            lock (syncRoot)
            {
                return SetVcccoefb(vcccoefb);
            }
        }
        public override bool ReadVcccoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool ReadVcccoefb(out string strcoef, byte vccselect)
        {
            lock (syncRoot)
            {
                return ReadVcccoefb(out strcoef);
            }
        }
        public override bool SetVcccoefc(string vcccoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
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
        public override bool SetVcccoefc(string vcccoefc, byte vccselect)
        {
            lock (syncRoot)
            {
                return SetVcccoefc(vcccoefc);
            }
        }
        public override bool ReadVcccoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                if (Algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        {
            lock (syncRoot)
            {
                return ReadVcccoefc(out strcoef);
            }
        }

       #endregion

        #region  RxPowerDmi

        public override bool SetRxpcoefa(string rxcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, rxcoefa, DutStruct[i].Format);
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
        }
        public override bool ReadRxpcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetRxpcoefb(string rxcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, rxcoefb, DutStruct[i].Format);
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
        }
        public override bool ReadRxpcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetRxpcoefc(string rxcoefc)
        {
            lock (syncRoot)
            {
                int i = 0; bool flag = false;
                if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, rxcoefc, DutStruct[i].Format);
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
        }
        public override bool ReadRxpcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFC"))
                {

                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }

        #endregion

        #region  TxPowerDmi

        public override bool SetTxSlopcoefa(string txslopcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txslopcoefa, DutStruct[i].Format);
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
        }
        public override bool ReadTxSlopcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetTxSlopcoefb(string txslopcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txslopcoefb, DutStruct[i].Format);
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
        }
        public override bool ReadTxSlopcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetTxSlopcoefc(string txslopcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txslopcoefc, DutStruct[i].Format);
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
        }


        public override bool ReadTxSlopcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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

        }
        public override bool SetTxOffsetcoefa(string txoffsetcoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txoffsetcoefa, DutStruct[i].Format);
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
        }

        public override bool ReadTxOffsetcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }

        public override bool SetTxOffsetcoefb(string txoffsetcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txoffsetcoefb, DutStruct[i].Format);
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
        }

        public override bool ReadTxOffsetcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }

        public override bool SetTxOffsetcoefc(string txoffsetcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txoffsetcoefc, DutStruct[i].Format);
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
        }

        public override bool ReadTxOffsetcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
     #endregion

        #region  TxPower

        public override bool SetBiasdaccoefa(string biasdaccoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, biasdaccoefa, DutStruct[i].Format);
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
        }
        public override bool ReadBiasdaccoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetBiasdaccoefb(string biasdaccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, biasdaccoefb, DutStruct[i].Format);
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
        }
        public override bool ReadBiasdaccoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetBiasdaccoefc(string biasdaccoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, biasdaccoefc, DutStruct[i].Format);
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
        }
        public override bool ReadBiasdaccoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;
                if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetTargetLopcoefa(string targetlopcoefa)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                if (FindFiledNameChannel(out i, "TARGETLOPCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, targetlopcoefa, DutStruct[i].Format);
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
        }
        public override bool ReadTargetLopcoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TARGETLOPCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetTargetLopcoefb(string targetlopcoefb)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                if (FindFiledNameChannel(out i, "TARGETLOPCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, targetlopcoefb, DutStruct[i].Format);
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
        }
        public override bool ReadTargetLopcoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TARGETLOPCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetTargetLopcoefc(string targetlopcoefc)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, targetlopcoefc, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET TARGETLOPCOEFC To" + targetlopcoefc); return flag;
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
        }
        public override bool ReadTargetLopcoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }


     #endregion

        #region  TxMod

        public override bool SetModdaccoefa(string moddaccoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, moddaccoefa, DutStruct[i].Format);
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
        }
        public override bool ReadModdaccoefa(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetModdaccoefb(string moddaccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, moddaccoefb, DutStruct[i].Format);
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
        }
        public override bool ReadModdaccoefb(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }
        public override bool SetModdaccoefc(string moddaccoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, moddaccoefc, DutStruct[i].Format);
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
        }
        public override bool ReadModdaccoefc(out string strcoef)
        {
            lock (syncRoot)
            {
                strcoef = "";
                int i = 0;

                if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        strcoef = EEPROM.ReadCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        }

   #endregion    
     

        #region  Mask

        public override bool SetMaskcoefa(float coefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TxMaskCoefA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, coefa.ToString(), DutStruct[i].Format);
                        Log.SaveLogToTxt("SET coefA To" + coefa);
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
                    Log.SaveLogToTxt("there is no TxMaskCoefA");

                    return true;
                }
            }
        }
        public override bool SetMaskcoefb(float coefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TxMaskCoefb"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, coefb.ToString(), DutStruct[i].Format);
                        Log.SaveLogToTxt("SET coefb To" + coefb);
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
                    Log.SaveLogToTxt("there is no TxMaskCoefb");

                    return true;
                }
            }
        }
        public override bool SetMaskcoefc(float coefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TxMaskCoefc"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, coefc.ToString(), DutStruct[i].Format);
                        Log.SaveLogToTxt("SET coefC To" + coefc);
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
                    Log.SaveLogToTxt("there is no TxMaskCoefC");

                    return true;
                }
            }
        }
#endregion

#region  RXAPD

        public override bool SetAPDdaccoefa(string apddaccoefa)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DmiAPDCoefA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, apddaccoefa, DutStruct[i].Format);
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
        }
        public override bool SetAPDdaccoefb(string apddaccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "DmiAPDCoefb"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, apddaccoefb, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET DmiAPDCoefA To" + apddaccoefb);
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
                    Log.SaveLogToTxt("there is no apddaccoefb");

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
                if (FindFiledNameChannel(out i, "DmiAPDCoefC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, apddaccoefc, DutStruct[i].Format);
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
        }
#endregion


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
        #endregion

        #region new add as cgr4 new map
        public override bool SetReferenceTemp(string referencetempcoef)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (Algorithm.FindFileName(DutStruct, "REFERENCETEMPERATURECOEF", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, referencetempcoef, DutStruct[i].Format);
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
        }

        public override bool SetTxpProportionLessCoef(string Lesscoef)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXPPROPORTIONLESSCOEF"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, Lesscoef, DutStruct[i].Format);
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
        }

        public override bool SetTxpProportionGreatCoef(string Greatcoef)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXPPROPORTIONGREATCOEF"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, Greatcoef, DutStruct[i].Format);
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
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txpfitscoefa, DutStruct[i].Format);
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
                if (FindFiledNameChannel(out i, "TXPFITSCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txpfitscoefb, DutStruct[i].Format);
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
        }
        public override bool SetTxpFitsCoefc(string txpfitscoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "TXPFITSCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, txpfitscoefc, DutStruct[i].Format);
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
        }

        public override bool SetRxAdCorSlopcoefb(string rxadcorslopcoefb)
        {
            return false;
        }
        public override bool SetRxAdCorSlopcoefc(string rxadcorslopcoefc)
        {
            return false;
        }
        public override bool SetRxAdCorOffscoefb(string rxadcoroffsetcoefb)
        {
            return false;
        }
        public override bool SetRxAdCorOffscoefc(string rxadcoroffsetcoefc)
        {
            return false;
        }
        public override bool ReadRx2RawADC(out UInt16 rxrawadc)
        {
            rxrawadc = 0;
            return false;
        }
        #endregion
        #region pid 
        public override bool SetPIDSetpoint(string setpoint)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "SETPOINT"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format);
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
        }
        public override bool SetcoefP(string coefp)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "COEFP"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, coefp, DutStruct[i].Format);
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
        }
        public override bool SetcoefI(string coefi)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "COEFI"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, coefi, DutStruct[i].Format);
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
        }
        public override bool SetcoefD(string coefd)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "COEFD"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
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
        #region apc
        public override bool APCON(byte apcswitch)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] buff = new byte[1];
                buff[0] = 0x00;
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
                    try
                    {
                        IOPort.WriteReg(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
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
        }
        public override bool APCOFF(byte apcswitch)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] buff = new byte[1];
                buff[0] = 0xff;
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
                        IOPort.WriteReg(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
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
        }
        public override bool APCStatus(out byte apcflag)
        {
            //0 OFF.1 ON
            lock (syncRoot)
            {
                apcflag = 0x00;
                int i = 0;
                byte[] buff = new byte[1];
                if (Algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        buff = IOPort.ReadReg(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

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
                if (FindFiledNameChannel(out i, "CLOSELOOPCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, CloseLoopcoefa, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET CLOSELOOPCOEFA To" + CloseLoopcoefa);
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
        }
        public override bool SetCloseLoopcoefb(string CloseLoopcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "CLOSELOOPCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, CloseLoopcoefb, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET CLOSELOOPCOEFB To" + CloseLoopcoefb);
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
        }
        public override bool SetCloseLoopcoefc(string CloseLoopcoefc)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
                if (FindFiledNameChannel(out i, "CLOSELOOPCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, CloseLoopcoefc, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET CLOSELOOPCOEFC To" + CloseLoopcoefc);
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
        }

        #endregion
        #region bias adc threshold
        public override bool SetBiasadcThreshold(byte threshold)
        {
            return false;
        }
        //public override bool SetRXPadcThreshold(byte threshold)
        //{
        //    return false;
        //}
        public override bool SetRXPadcThreshold(UInt16 threshold)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] adcthreshold = new byte[1];
                if (FindFiledNameChannel(out i, "RXADCTHRESHOLD"))
                {
                    Engmod(DutStruct[i].EngPage);

                    if (DutStruct[i].Length == 1 && threshold > 255)
                    {
                        Log.SaveLogToTxt(" RXADCTHRESHOLD TO Large");
                        return false;
                    }
                    try
                    {
                        adcthreshold = Algorithm.Uint16DataConvertoBytes(threshold);
                        // adcthreshold[0] =Convert. threshold;
                        //  Algorithm.Uint16DataConvertoBytes
                        WriteReg(DUT.deviceIndex, 0xa2, DutStruct[i].StartAddress, adcthreshold);
                        //flag = QEEPROM.SetCoef(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
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
        }
        #endregion

        public override bool ReadALLcoef(out DutCoefValueStuct[] DutList)
        {
            lock (syncRoot)
            {
                string strcoef = "";
                int coefsum = 0;
                int coefcount = 0;
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
        }
        public override bool ReadALLEEprom(out DutEEPROMInitializeStuct[] DutList)
        {
            lock (syncRoot)
            {
                string strcoef = "";
                int coefcount = 0;
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
        public override bool SetdmiRxOffset(string dmiRxOffset) { return false; }
        public override bool SetTxEQcoefa(string TxEQcoefa) 
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                if (FindFiledNameChannel(out i, "TXEQCOEFA"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, TxEQcoefa, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET TXEQCOEFA To" + TxEQcoefa);
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
                    Log.SaveLogToTxt("there is no TXEQCOEFA");

                    return true;
                }
            }
        }
        public override bool SetTxEQcoefb(string TxEQcoefb) 
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                if (FindFiledNameChannel(out i, "TXEQCOEFB"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, TxEQcoefb, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET TXEQCOEFB To" + TxEQcoefb);
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
                    Log.SaveLogToTxt("there is no TXEQCOEFB");

                    return true;
                }
            }
        }
        public override bool SetTxEQcoefc(string TxEQcoefc)
        {
            lock (syncRoot)
            {
                bool flag = false;
                int i = 0;
                if (FindFiledNameChannel(out i, "TXEQCOEFC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        flag = EEPROM.SetCoef(DUT.deviceIndex, 0xA2, DutStruct[i].StartAddress, TxEQcoefc, DutStruct[i].Format);
                        Log.SaveLogToTxt("SET TXEQCOEFC To" + TxEQcoefc);
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
                    Log.SaveLogToTxt("there is no TXEQCOEFC");

                    return true;
                }
            }
        }
        #endregion

        public override bool GetRegistValueLimmit(byte ItemType, out int Max)
        {
            lock (syncRoot)
            {
                int i = 1;

                switch (ItemType)
                {
                    case 0:
                        FindFiledNameDACLength(out i, "BIASDAC");
                        break;
                    case 1:
                        FindFiledNameDACLength(out i, "MODDAC");
                        break;
                    default:
                        break;
                }

                Max = Convert.ToInt32(Math.Pow(256, i));

                return true;

            }
        }


        #region  Operate Drvier Regist

        private bool WriteDac(string StrItem, object DAC)
        {
            lock (syncRoot)
            {
                int ReadDacValue = 0;

                int i = 0;

                // string StrItem = "BiasDac";
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

                                int JoinValue = Algorithm.WriteJointBitValue(Convert.ToInt32(DAC), Convert.ToInt32(TempReadValue), DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理
                                // int JoinValue = Algorithm.WriteJointBitValue((int)(DAC), Convert.ToInt32(TempReadValue), DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理

                                if (!Write_Store_DriverRegist(StrItem, JoinValue, Writer_Store.Writer)) return false;//写入寄存器的全位DAC值

                                if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读取寄存器的全位DAC值

                                int ReadJoinValue = Algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                                if (ReadJoinValue == Convert.ToInt16(DAC))
                                {
                                    return true;
                                }
                            }

                            // int ReadJoinValue = Algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                            // if (ReadJoinValue == Convert.ToInt16(DAC))
                            // {
                            //     return true;
                            // }


                        }
                        Log.SaveLogToTxt("Writer " + StrItem + " Error");
                        throw new InnoExCeption(ExceptionDictionary.Code._Write_DAC_Fail_0x05100, new System.Diagnostics.StackTrace().GetFrame(0).ToString());

                    }
                    else
                    {
                        throw new InnoExCeption(ExceptionDictionary.Code._Los_parameter_0x05102);

                    }

                }
                catch (InnoExCeption error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(error.ID, error.StackTrace);
                }

                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);

                }
            }
        }
        private bool StoreDac(string StrItem, object DAC)
        {
            lock (syncRoot)
            {
                int ReadDacValue = 0;

                int i = 0;

                // string StrItem = "BiasDac";

                if (FindFiledNameChannelDAC(out i, StrItem))
                {

                    bool flag = Algorithm.BitNeedManage(DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);

                    if (!flag)//寄存器全位,不需要做任何处理
                    {
                        for (int k = 0; k < 3; k++)
                        {

                            if (!Write_Store_DriverRegist(StrItem, DAC, Writer_Store.Store)) return false;//存DAC值

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

                            if (!Write_Store_DriverRegist(StrItem, JoinValue, Writer_Store.Store)) return false;//存入寄存器的全位DAC值

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
        }
        private bool Write_Store_DriverRegist(string StrItem, object DAC, Writer_Store operate)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                bool ResultFlag = true;
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
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        byte[] WriteDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);
                        Engmod(engpage);

                        for (int J = 0; J < 3; J++)
                        {

                            if (operate == 0)//写
                            {

                                WriteDriverSFP28(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteDacByteArray);
                            }
                            else//存
                            {

                                StoreDriverSFP28(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteDacByteArray);

                            }

                            byte[] ReadDacByteArray = ReadDriverSFP28(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset);


                            for (int m = 0; m < WriteDacByteArray.Length; m++)
                            {
                                if (WriteDacByteArray[m] != ReadDacByteArray[m])
                                {
                                    ResultFlag = false;
                                    break;
                                }

                            }

                            if (ResultFlag)
                            {
                                break;
                            }

                        }
                        if (ResultFlag)
                        {
                            Log.SaveLogToTxt("Writer " + StrItem + "= " + DAC);

                        }
                        else
                        {
                            Log.SaveLogToTxt("Writer " + StrItem + "= " + DAC);
                        }
                        return ResultFlag;
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

                }
            }
        }
        private bool ReadDAC(string StrItem, out int ReadDAC)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                ReadDAC = 0;
                // int ReadDAC = 0;

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
                    //  BiasDac = new byte[length];
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
                                chipset = 0x03;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }

                        Engmod(engpage);

                        byte[] DacArray = ReadDriverSFP28(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset);

                        for (j = DacArray.Length; j > 0; j--)
                        {
                            ReadDAC += Convert.ToUInt16(DacArray[j - 1] * Math.Pow(256, DriverStruct[i].Length - j));
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

                }
            }

        }

        public override bool WriteBiasDac(object DAC)
        {
            lock (syncRoot)
            {

                return WriteDac("BiasDAC", DAC);
            }
        }

        public override bool WriteModDac(object DAC)
        {

            lock (syncRoot)
            {
                return WriteDac("ModDAC", DAC);
            }
            
        }

        public override bool WriteMaskDac(object DAC)
        {
            lock (syncRoot)
            {


                return WriteDac("MaskDAC", DAC);
            }
            
        }

        public override bool StoreBiasDac(object biasdac)
        {

            lock (syncRoot)
            {

                return StoreDac("BiasDAC", biasdac);
            }

        }

        public override bool StoreModDac(object moddac)
        {
            lock (syncRoot)
            {
                return StoreDac("MODDAC", moddac);
            }
        }

        public override bool StoreMaskDac(object DAC)
        {

            lock (syncRoot)
            {
                return StoreDac("MaskDAC", DAC);
            }
        }
      
        public override bool StoreLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                return StoreDac("LOSDAC", losdac);
            }
        }

        public override bool StoreLOSDDac(object losddac)
        {

            lock (syncRoot)
            {
                return StoreDac("LOSDDAC", losddac);
            }
        }

        public override bool StoreAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                return StoreDac("APDDAC", apddac);
            }
        }
        public override bool StoreEA(object DAC)
        {
            lock (syncRoot)
            {
                return StoreDac("EADAC", DAC);
            }
        }

        public override bool WriteLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                return WriteDac("LOSDAC", losdac);
            }
        }
        public override bool WriteLOSDDac(object losddac)
        {
            lock (syncRoot)
            {
                return WriteDac("LOSDDAC", losddac);
            }
        }
        public override bool WriteAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                return WriteDac("APDDAC", apddac);
            }
        }
        public override bool StoreCrossDac(object crossdac)
        {
            lock (syncRoot)
            {
                return StoreDac("CROSSDAC", crossdac);

            }
        }
        public override bool WriteEA(object DAC)
        {
            lock (syncRoot)
            {
                return WriteDac("EADAC", DAC);
            }
        }
    
#endregion

    }
}
