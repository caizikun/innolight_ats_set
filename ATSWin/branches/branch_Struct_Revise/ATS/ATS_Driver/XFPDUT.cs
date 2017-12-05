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
    public class XFPDUT:DUT
    {
        //dmi
        private static object syncRoot = new Object();//used for thread synchronization
        public override bool Initialize(DutStruct[] List)
        {
            try
            {
                DutStruct = List;
                
                if (!Connect()) return false;
                
            }

            catch (Error_Message error)
            {

                throw new Error_Message(this.GetType().ToString() + " " + error.Message, error);
            }
            return true;
        }
        public override bool Connect()
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
        public override bool Configure(int syn = 0)
        { return true; }
        public override bool ChangeChannel(string channel,int syn=0)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset,int syn=0)
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
                IOPort.WriteReg(DUT.deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
            }
        }
        #region dmi
        public override double ReadDmiTemp()
        {
            //double dmitemp = 0.0;
            //dmitemp = EEPROM.readdmitemp(0xA0, 96);
            //return dmitemp;
            lock (syncRoot)
            {
                return EEPROM.readdmitemp(DUT.deviceIndex, 0xA0, 96);
            }
        }
        public override double ReadDmiVcc()
        {
            //double dmivcc = 0.0;
            //dmivcc = EEPROM.readdmivcc(0xA0, 106);
            //return dmivcc;
            lock (syncRoot)
            {
                return EEPROM.readdmivcc(DUT.deviceIndex, 0xA0, 106);
            }
        }
        public override double ReadDmiBias()
        {
            //double dmibias = 0.0;
            //dmibias = EEPROM.readdmibias(0xA0, 100);
            //return dmibias;
            lock (syncRoot)
            {
                return EEPROM.readdmibias(DUT.deviceIndex, 0xA0, 100);
            }
        }
        public override double ReadDmiTxp()
        {
            //double dmitxp = 0.0;
            //dmitxp = EEPROM.readdmitxp(0xA0, 102);
            //return dmitxp;
            lock (syncRoot)
            {
                return EEPROM.readdmitxp(DUT.deviceIndex, 0xA0, 102);
            }
        }
        public override double ReadDmiRxp()
        {
            //double dmirxp = 0.0;
            //dmirxp = EEPROM.readdmirxp(0xA0, 104);
            //return dmirxp;
            lock (syncRoot)
            {
                return EEPROM.readdmirxp(DUT.deviceIndex, 0xA0, 104);
            }
        }
        #endregion
        #region read a/w
        public override double ReadTempHA()
        {
            //double tempha = 0.0;
            //tempha = EEPROM.readtempaw(0xA0, 2);
            //return tempha;
            lock (syncRoot)
            {
                return EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 2);
            }
        }
        public override double ReadTempLA()
        {
            //double templa = 0.0;
            //templa = EEPROM.readtempaw(0xA0, 4);
            //return templa;
            lock (syncRoot)
            {
                return EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 4);
            }
        }
        public override double ReadTempLW()
        {
            //double templw = 0.0;
            //templw = EEPROM.readtempaw(0xA0, 8);
            //return templw;
            lock (syncRoot)
            {
                return EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 8);
            }
        }
        public override double ReadTempHW()
        {
            //double temphw = 0.0;
            //temphw = EEPROM.readtempaw(0xA0, 6);
            //return temphw;
            lock (syncRoot)
            {
                return EEPROM.readtempaw(DUT.deviceIndex, 0xA0, 6);
            }
        }
        public override double ReadVccLA()
        {
            //double vccla = 0.0;
            //vccla = EEPROM.readvccaw(0xA0, 44);
            //return vccla;
            lock (syncRoot)
            {
                return EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 44);
            }
        }
        public override double ReadVccHA()
        {
            //double vccha = 0.0;
            //vccha = EEPROM.readvccaw(0xA0, 42);
            //return vccha;
            lock (syncRoot)
            {
                return EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 42);
            }
        }
        public override double ReadVccLW()
        {
            //double vcclw = 0.0;
            //vcclw = EEPROM.readvccaw(0xA0, 48);
            //return vcclw;
            lock (syncRoot)
            {
                return EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 48);
            }
        }
        public override double ReadVccHW()
        {
            //double vcchw = 0.0;
            //vcchw = EEPROM.readvccaw(0xA0, 46);
            //return vcchw;
            lock (syncRoot)
            {
                return EEPROM.readvccaw(DUT.deviceIndex, 0xA0, 46);
            }
        }
        public override double ReadBiasLA()
        {
            //double biasla = 0.0;
            //biasla = EEPROM.readbiasaw(0xA0, 20);
            //return biasla;
            lock (syncRoot)
            {
                return EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 20);
            }
        }
        public override double ReadBiasHA()
        {
            //double biasha = 0.0;
            //biasha = EEPROM.readbiasaw(0xA0, 18);
            //return biasha;
            lock (syncRoot)
            {
                return EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 18);
            }
        }
        public override double ReadBiasLW()
        {
            //double biaslw = 0.0;
            //biaslw = EEPROM.readbiasaw(0xA0, 24);
            //return biaslw;
            lock (syncRoot)
            {
                return EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 24);
            }
        }
        public override double ReadBiasHW()
        {
            //double biashw = 0.0;
            //biashw = EEPROM.readbiasaw(0xA0, 22);
            //return biashw;
            lock (syncRoot)
            {
                return EEPROM.readbiasaw(DUT.deviceIndex, 0xA0, 22);
            }
        }
        public override double ReadTxpLA()
        {
            //double txpla = 0.0;
            //txpla = EEPROM.readtxpaw(0xA0, 28);
            //return txpla;
            lock (syncRoot)
            {
                return EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 28);
            }
        }
        public override double ReadTxpLW()
        {
            //double txplw = 0.0;
            //txplw = EEPROM.readtxpaw(0xA0, 32);
            //return txplw;
            lock (syncRoot)
            {
                return EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 32);
            }
        }
        public override double ReadTxpHA()
        {
            //double txpha = 0.0;
            //txpha = EEPROM.readtxpaw(0xA0, 26);
            //return txpha;
            lock (syncRoot)
            {
                return EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 26);
            }
        }
        public override double ReadTxpHW()
        {
            //double txphw = 0.0;
            //txphw = EEPROM.readtxpaw(0xA0, 30);
            //return txphw;
            lock (syncRoot)
            {
                return EEPROM.readtxpaw(DUT.deviceIndex, 0xA0, 30);
            }
        }
        public override double ReadRxpLA()
        {
            //double rxpla = 0.0;
            //rxpla = EEPROM.readrxpaw(0xA0, 36);
            //return rxpla;
            lock (syncRoot)
            {
                return EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 36);
            }
        }
        public override double ReadRxpLW()
        {
            //double rxplw = 0.0;
            //rxplw = EEPROM.readrxpaw(0xA0, 40);
            //return rxplw;
            lock (syncRoot)
            {
                return EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 40);
            }
        }
        public override double ReadRxpHA()
        {
            //double rxpha = 0.0;
            //rxpha = EEPROM.readrxpaw(0xA0, 34);
            //return rxpha;
            lock (syncRoot)
            {
                return EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 34);
            }
        }
        public override double ReadRxpHW()
        {
            //double rxphw = 0.0;
            //rxphw = EEPROM.readrxpaw(0xA0, 38);
            //return rxphw;
            lock (syncRoot)
            {
                return EEPROM.readrxpaw(DUT.deviceIndex, 0xA0, 38);
            }
        }
        #endregion
        #region check a/w
        public override bool ChkTempHA()
        {
            //bool tempha;
            //tempha = EEPROM.ChkTempHA(0xA0, 80);
            //return tempha;
            lock (syncRoot)
            {
                return EEPROM.ChkTempHA(DUT.deviceIndex, 0xA0, 80);
            }
        }
        public override bool ChkTempLA()
        {
            //bool templa;
            //templa = EEPROM.ChkTempLA(0xA0, 80);
            //return templa;
            lock (syncRoot)
            {
                return EEPROM.ChkTempLA(DUT.deviceIndex, 0xA0, 80);

            }
        }
        public override bool ChkVccHA()
        {
            //bool vccha;
            //vccha = EEPROM.ChkVccHA(0xA0, 81);
            //return vccha;
            lock (syncRoot)
            {
                return EEPROM.ChkVccHA(DUT.deviceIndex, 0xA0, 81);

            }
        }
        public override bool ChkVccLA()
        {
            //bool vccla;
            //vccla = EEPROM.ChkVccLA(0xA0, 81);
            //return vccla;
            lock (syncRoot)
            {
                return EEPROM.ChkVccLA(DUT.deviceIndex, 0xA0, 81);
            }
        }
        public override bool ChkBiasHA()
        {
            //bool biasha;
            //biasha = EEPROM.ChkBiasHA(0xA0, 80);
            //return biasha;
            lock (syncRoot)
            {
                return EEPROM.ChkBiasHA(DUT.deviceIndex, 0xA0, 80);
            }
        }
        public override bool ChkBiasLA()
        {
            //bool biasla;
            //biasla = EEPROM.ChkBiasLA(0xA0, 80);
            //return biasla;
            lock (syncRoot)
            {
                return EEPROM.ChkBiasLA(DUT.deviceIndex, 0xA0, 80);
            }
        }
        public override bool ChkTxpHA()
        {
            //bool txpha;
            //txpha = EEPROM.ChkTxpHA(0xA0, 80);
            //return txpha;
            lock (syncRoot)
            {
                return EEPROM.ChkTxpHA(DUT.deviceIndex, 0xA0, 80);
            }
        }
        public override bool ChkTxpLA()
        {
            //bool txpla;
            //txpla = EEPROM.ChkTxpLA(0xA0, 80);
            //return txpla;
            lock (syncRoot)
            {
                return EEPROM.ChkTxpLA(DUT.deviceIndex, 0xA0, 80);
            }
        }
        public override bool ChkRxpHA()
        {
            //bool rxpha;
            //rxpha = EEPROM.ChkRxpHA(0xA0, 81);
            //return rxpha;
            lock (syncRoot)
            {
                return EEPROM.ChkRxpHA(DUT.deviceIndex, 0xA0, 81);
            }
        }
        public override bool ChkRxpLA()
        {
            //bool rxpla;
            //rxpla = EEPROM.ChkRxpLA(0xA0, 81);
            //return rxpla;
            lock (syncRoot)
            {
                return EEPROM.ChkRxpLA(DUT.deviceIndex, 0xA0, 81);
            }
        }
        public override bool ChkTempHW()
        {
            //bool temphw;
            //temphw = EEPROM.ChkTempHW(0xA0, 82);
            //return temphw;
            lock (syncRoot)
            {
                return EEPROM.ChkTempHW(DUT.deviceIndex, 0xA0, 82);
            }
        }
        public override bool ChkTempLW()
        {
            //bool templw;
            //templw = EEPROM.ChkTempLW(0xA0, 82);
            //return templw;
            lock (syncRoot)
            {
                return EEPROM.ChkTempLW(DUT.deviceIndex, 0xA0, 82);
            }
        }
        public override bool ChkVccHW()
        {
            //bool vcchw;
            //vcchw = EEPROM.ChkVccHW(0xA0, 83);
            //return vcchw;
            lock (syncRoot)
            {
                return EEPROM.ChkVccHW(DUT.deviceIndex, 0xA0, 83);
            }
        }
        public override bool ChkVccLW()
        {
            //bool vcclw;
            //vcclw = EEPROM.ChkVccLW(0xA0, 83);
            //return vcclw;
            lock (syncRoot)
            {
                return EEPROM.ChkVccLW(DUT.deviceIndex, 0xA0, 83);

            }
        }
        public override bool ChkBiasHW()
        {
            //bool biashw;
            //biashw = EEPROM.ChkBiasHW(0xA0, 82);
            //return biashw;
            lock (syncRoot)
            {
                return EEPROM.ChkBiasHW(DUT.deviceIndex, 0xA0, 82);
             }
        }
        public override bool ChkBiasLW()
        {
            //bool biaslw;
            //biaslw = EEPROM.ChkBiasLW(0xA0, 82);
            //return biaslw;
            lock (syncRoot)
            {
                return EEPROM.ChkBiasLW(DUT.deviceIndex, 0xA0, 82);
            }
        }
        public override bool ChkTxpHW()
        {
            //bool txphw;
            //txphw = EEPROM.ChkTxpHW(0xA0, 82);
            //return txphw;
            lock (syncRoot)
            {
                return EEPROM.ChkTxpHW(DUT.deviceIndex, 0xA0, 82);
            }
        }
        public override bool ChkTxpLW()
        {
            //bool txplw;
            //txplw = EEPROM.ChkTxpLW(0xA0, 82);
            //return txplw;
            lock (syncRoot)
            {
                return EEPROM.ChkTxpLW(DUT.deviceIndex, 0xA0, 82);
            }
        }
        public override bool ChkRxpHW()
        {
            //bool rxphw;
            //rxphw = EEPROM.ChkRxpHW(0xA0, 83);
            //return rxphw;
            lock (syncRoot)
            {
                return EEPROM.ChkRxpHW(DUT.deviceIndex, 0xA0, 83);
            }
        }
        public override bool ChkRxpLW()
        {
            //bool rxplw;
            //rxplw = EEPROM.ChkRxpLW(0xA0, 83);
            //return rxplw;
            lock (syncRoot)
            {
                return EEPROM.ChkRxpLW(DUT.deviceIndex, 0xA0, 83);
            }
        }
        #endregion
        #region read optional status /control bit 110
        public override bool ChkTxDis()
        {
            //bool txdis;
            //txdis = EEPROM.ChkRxpLW(0xA0, 110);
            //return txdis;
            lock (syncRoot)
            {
                return EEPROM.ChkTxDis(DUT.deviceIndex, 0xA0, 110);
            }
        }
        public override bool ChkTxFault()
        {
            //bool txfault;
            //txfault = EEPROM.ChkTxFault(0xA0, 110);
            //return txfault;
            lock (syncRoot)
            {
                return EEPROM.ChkTxFault(DUT.deviceIndex, 0xA0, 110);
            }
        }
        public override bool ChkRxLos()
        {
            //bool rxlos;
            //rxlos = EEPROM.ChkRxLos(0xA0, 110);
            //return rxlos;
            lock (syncRoot)
            {
                return EEPROM.ChkRxLos(DUT.deviceIndex, 0xA0, 110);
            }
        }
        #endregion
        #region set a/w
        public override void SetTempLA(decimal templa)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA0, 4, templa);
            }
        }
        public override void SetTempHA(decimal tempha)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA0, 2, tempha);
            }
        }
        public override void SetTempLW(decimal templw)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA0, 8, templw);
            }
        }
        public override void SetTempHW(decimal temphw)
        {
            lock (syncRoot)
            {
                EEPROM.settempaw(DUT.deviceIndex, 0xA0, 6, temphw);
            }
        }
        public override void SetVccHW(decimal vcchw)
        {
            lock (syncRoot)
            {
                EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 46, vcchw);
            }
        }
        public override void SetVccLW(decimal vcclw)
        {
            lock (syncRoot)
            {
                EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 48, vcclw);
            }
        }
        public override void SetVccLA(decimal vccla)
        {
            lock (syncRoot)
            {
                EEPROM.setvccaw(DUT.deviceIndex, 0xA0, 44, vccla);
            }
        }
        public override void SetVccHA(decimal vccha)
        {
            lock (syncRoot)
            {
                EEPROM.setvccaw(DUT.deviceIndex, 0xA2, 42, vccha);
            }
        }
        public override void SetBiasLA(decimal biasla)
        {
            lock (syncRoot)
            {
                EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 20, biasla);
            }
        }
        public override void SetBiasHA(decimal biasha)
        {
            lock (syncRoot)
            {
                EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 18, biasha);
            }
        }
        public override void SetBiasHW(decimal biashw)
        {
            lock (syncRoot)
            {
                EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 22, biashw);
            }
        }
        public override void SetBiasLW(decimal biaslw)
        {
            lock (syncRoot)
            {
                EEPROM.setbiasaw(DUT.deviceIndex, 0xA0, 24, biaslw);
            }
        }
        public override void SetTxpLW(decimal txplw)
        {
            lock (syncRoot)
            {
                EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 32, txplw);
            }
        }
        public override void SetTxpHW(decimal txphw)
        {
            lock (syncRoot)
            {
                EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 30, txphw);
            }
        }
        public override void SetTxpLA(decimal txpla)
        {
            lock (syncRoot)
            {
                EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 28, txpla);
            }
        }
        public override void SetTxpHA(decimal txpha)
        {
            lock (syncRoot)
            {
                EEPROM.settxpaw(DUT.deviceIndex, 0xA0, 26, txpha);
            }
        }
        public override void SetRxpHA(decimal rxpha)
        {
           lock (syncRoot)
            {
                EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 34, rxpha);
            }
        }
        public override void SetRxpLA(decimal rxpla)
        {
            lock (syncRoot)
            {
                EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 36, rxpla);
            }
        }
        public override void SetRxpHW(decimal rxphw)
        {
            lock (syncRoot)
            {
                EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 38, rxphw);
            }
        }
        public override void SetRxpLW(decimal rxplw)
        {
            lock (syncRoot)
            {
                EEPROM.setrxpaw(DUT.deviceIndex, 0xA0, 40, rxplw);
            }
        }
        public override bool SetSoftTxDis()
        {
            return true;
           // EEPROM.SetSoftTxDis(DUT.deviceIndex, 0xA0, 110);
            
        }
        #endregion
        #region w/r  sn/pn
        public override string ReadSn()
        {
            lock (syncRoot)
            {
                string sn = "";
                Engmod(1);
                sn = EEPROM.ReadSn(DUT.deviceIndex, 0xA0, 196);
                return sn;
            }
        }
        public override string ReadPn()
        {
            lock (syncRoot)
            {
                string pn = "";
                Engmod(1);
                pn = EEPROM.ReadPn(DUT.deviceIndex, 0xA0, 168);
                return pn;
            }
        }
        public override void SetSn(string sn)
        {
            lock (syncRoot)
            {
                Engmod(1);
                EEPROM.SetSn(DUT.deviceIndex, 0xA0, 196, sn);
                //System.Threading.Thread.Sleep(1000);
            }
        }
        public override void SetPn(string pn)
        {
            lock (syncRoot)
            {
                Engmod(1);
                EEPROM.SetPn(DUT.deviceIndex, 0xA0, 168, pn);
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
                string fwrev = "";
                Engmod(3);
                fwrev = EEPROM.ReadFWRev(DUT.deviceIndex, 0xA0, 132);
                return fwrev;
            }
        }
        #endregion
        #region adc
        public override bool ReadTempADC(out UInt16 tempadc)
        {
            lock (syncRoot)
            {
                tempadc = 0;
                int i = 0;

                //if (FindFiledNameTempSelect(out i, "TEMPERATUREADC"))
                if (Algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        tempadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        }
        public override bool ReadTempADC(out UInt16 tempadc, byte TempSelect)
        {
            lock (syncRoot)
            {
                return ReadTempADC(out tempadc);
            }
        }
        public override bool ReadVccADC(out UInt16 vccadc)
        {
            lock (syncRoot)
            {
                vccadc = 0;
                int i = 0;

                //if (FindFiledNameVccSelect(out i, "VCCADC"))
                if (Algorithm.FindFileName(DutStruct, "VCCADC", out i))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        vccadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        }
        public override bool ReadVccADC(out UInt16 vccadc, byte vccselect)
        {
            lock (syncRoot)
            {
                return ReadVccADC(out vccadc);
            }
        }
        public override bool ReadBiasADC(out UInt16 biasadc)
        {
            lock (syncRoot)
            {
                biasadc = 0;
                int i = 0;
                if (FindFiledNameChannel(out i, "TXBIASADC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        biasadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
                        Log.SaveLogToTxt("there is TXBIASADC" + biasadc);
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
        }
        public override bool ReadTxpADC(out UInt16 txpadc)
        {
            lock (syncRoot)
            {
                txpadc = 0;
                int i = 0;

                if (FindFiledNameChannel(out i, "TXPOWERADC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        txpadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
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
        }
        public override bool ReadRxpADC(out UInt16 rxpadc)
        {
            lock (syncRoot)
            {
                rxpadc = 0;
                int i = 0;

                if (FindFiledNameChannel(out i, "RXPOWERADC"))
                {
                    Engmod(DutStruct[i].EngPage);
                    try
                    {
                        rxpadc = EEPROM.readadc(DUT.deviceIndex, 0xA0, DutStruct[i].StartAddress);
                        Log.SaveLogToTxt("there is RXPOWERADC" + rxpadc);
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
        }
        #endregion

        #region read/write reg/port
        public override byte[] ReadReg(int deviceIndex, int deviceAddress, int regAddress, int readLength)
        {
            lock (syncRoot)
            {
                return IOPort.ReadReg(DUT.deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, readLength);
            }
        }
        public override byte[] WriteReg(int deviceIndex, int deviceAddress, int regAddress, byte[] dataToWrite)
        {
            lock (syncRoot)
            {
                return IOPort.WriteReg(DUT.deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
            }
        }

        public override bool WritePort(int id, int deviceIndex, int Port, int DDR)
        {
            lock (syncRoot)
            {
                return IOPort.WritePort(id, deviceIndex, Port, DDR);
            }
        }
        public override byte[] ReadPort(int id, int deviceIndex, int Port, int DDR)
        {
            lock (syncRoot)
            {
                return IOPort.ReadPort(id, deviceIndex, Port, DDR);
            }
        }
        #endregion

        public  byte[] WriteDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte[] dataToWrite)
        {
            lock (syncRoot)
            {
                return EEPROM.ReadWriteDriverXFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x80, dataToWrite);
            }
        }
        public  byte[] ReadDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress)
        {
            lock (syncRoot)
            {
                return EEPROM.ReadWriteDriverXFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x40, new byte[2]);
            }
        }
        public  byte[] StoreDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte[] dataToWrite)
        {
            lock (syncRoot)
            {
                return EEPROM.ReadWriteDriverXFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x20, dataToWrite);
            }
        }
        #region driver
        public override bool DriverInitialize()
        {
            return false;
        
        }
        public override bool EEpromInitialize()
        {
            return false;
        }
        //set biasmoddac
        public override bool WriteCrossDac(object crossdac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
                {
                    byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(crossdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    WriteDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteBiasDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no CROSSDAC");
                    return true;
                }
            }
        }
        public override bool WriteBiasDac(object biasdac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "BIASDAC"))
                {
                    byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    WriteDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteBiasDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no BIASDAC");
                    return true;
                }
            }
        }
        public override bool WriteModDac(object moddac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "MODDAC"))
                {
                    byte[] WriteModDacByteArray = Algorithm.ObjectTOByteArray(moddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    WriteDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteModDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no MODDAC");
                    return true;
                }
            }
        }
        public override bool WriteLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "LOSDAC"))
                {
                    byte[] WriteLOSDacByteArray = Algorithm.ObjectTOByteArray(losdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    WriteDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteLOSDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no LOSDAC");
                    return true;
                }
            }
        }
        public override bool WriteLOSDDac(object losddac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "LOSDDAC"))
                {
                    byte[] WriteLOSDacByteArray = Algorithm.ObjectTOByteArray(losddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    WriteDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteLOSDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no LOSDDAC");
                    return true;
                }
            }
        }
        public override bool WriteAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "APDDAC"))
                {
                    byte[] WriteAPDDacByteArray = Algorithm.ObjectTOByteArray(apddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    WriteDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteAPDDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no APDDAC");
                    return true;
                }
            }
        }
        public override bool StoreCrossDac(object crossdac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
                {
                    byte[] StoreBiasDacByteArray = Algorithm.ObjectTOByteArray(crossdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    StoreDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreBiasDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no CROSSDAC");
                    return true;
                }
            }
        }
        public override bool StoreBiasDac(object biasdac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "BIASDAC"))
                {
                    byte[] StoreBiasDacByteArray = Algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    StoreDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreBiasDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no BIASDAC");
                    return true;
                }
            }
        }
        public override bool StoreModDac(object moddac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "MODDAC"))
                {
                    byte[] StoreModDacByteArray = Algorithm.ObjectTOByteArray(moddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    StoreDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreModDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no MODDAC");
                    return true;
                }
            }
        }
        public override bool StoreLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "LOSDAC"))
                {
                    byte[] StoreLOSDacByteArray = Algorithm.ObjectTOByteArray(losdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    StoreDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreLOSDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no LOSDAC");
                    return true;
                }
            }
        }
        public override bool StoreLOSDDac(object losddac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "LOSDDAC"))
                {
                    byte[] StoreLOSDacByteArray = Algorithm.ObjectTOByteArray(losddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    StoreDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreLOSDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no LOSDDAC");
                    return true;
                }
            }
        }
        public override bool StoreAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "APDDAC"))
                {
                    byte[] StoreAPDDacByteArray = Algorithm.ObjectTOByteArray(apddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                    Engmod(engpage);
                    StoreDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreAPDDacByteArray);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no APDDAC");
                    return true;
                }
            }
        }
        //read biasmoddac
        public override bool ReadCrossDac(int length, out byte[] crossdac)
        {
            lock (syncRoot)
            {
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
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
                crossdac = new byte[length];
                int i = 0;
                if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
                {
                    Engmod(engpage);
                    crossdac = ReadDriver10g(DUT.deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress);
                    return true;
                }
                else
                {
                    Log.SaveLogToTxt("there is no CROSSDAC");
                    return true;
                }
            }
        }
    
        #endregion

        #region set coef
        public override bool SetTempcoefb(string tempcoefb)
        {
            lock (syncRoot)
            {
                int i = 0;
                bool flag = false;
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
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        {
            lock (syncRoot)
            {
                return ReadTempcoefc(out strcoef);
            }
        }
        public override bool SetVcccoefb(string vcccoefb)
        {
            lock (syncRoot)
            {
                int i = 0;

                bool flag = false;
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
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        {
            lock (syncRoot)
            {
                return ReadVcccoefc(out strcoef);
            }
        }
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
        }
        public override bool SetRxpcoefb(string rxcoefb)
        {
            lock (syncRoot)
            {
                int i = 0; bool flag = false;
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
        }
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
        }
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
        }
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
        }
        public override bool APCOFF(byte apcswitch)
        {
            lock (syncRoot)
            {
                int i = 0;
                byte[] buff = new byte[1];
                buff[0] = 0x05;
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
        }
        #endregion
    }
}
