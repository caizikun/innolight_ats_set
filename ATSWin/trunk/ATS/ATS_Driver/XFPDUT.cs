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
        EEPROM xfp;
        //dmi
        public Algorithm algorithm = new Algorithm();
        public XFPDUT(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(DutStruct[] List)
        {
            try
            {
                DutStruct = List;
                xfp = new EEPROM(deviceIndex, logger);
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
                USBIO = new IOPort("USB", deviceIndex.ToString(), logger);
                USBIO.IOConnect();
                EquipmentConnectflag = true; ;
                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
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
            byte[] buff = new byte[5];
            buff[0] = 0xca;
            buff[1] = 0x2d;
            buff[2] = 0x81;
            buff[3] = 0x5f;
            buff[4] = engpage;
            USBIO.WrtieReg(deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        }
       
        #region dmi
        public override double ReadDmiTemp()
        {
            //double dmitemp = 0.0;
            //dmitemp = xfp.readdmitemp(0xA0, 96);
            //return dmitemp;
            return xfp.readdmitemp(deviceIndex,0xA0, 96);
        }
        public override double ReadDmiVcc()
        {
            //double dmivcc = 0.0;
            //dmivcc = xfp.readdmivcc(0xA0, 106);
            //return dmivcc;
            return xfp.readdmivcc(deviceIndex, 0xA0, 106);
        }
        public override double ReadDmiBias()
        {
            //double dmibias = 0.0;
            //dmibias = xfp.readdmibias(0xA0, 100);
            //return dmibias;
            return xfp.readdmibias(deviceIndex, 0xA0, 100);
        }
        public override double ReadDmiTxp()
        {
            //double dmitxp = 0.0;
            //dmitxp = xfp.readdmitxp(0xA0, 102);
            //return dmitxp;
            return xfp.readdmitxp(deviceIndex, 0xA0, 102);
        }
        public override double ReadDmiRxp()
        {
            //double dmirxp = 0.0;
            //dmirxp = xfp.readdmirxp(0xA0, 104);
            //return dmirxp;
            return xfp.readdmirxp(deviceIndex, 0xA0, 104);
        }
        #endregion
        #region read a/w
        public override double ReadTempHA()
        {
            //double tempha = 0.0;
            //tempha = xfp.readtempaw(0xA0, 2);
            //return tempha;
            return xfp.readtempaw(deviceIndex, 0xA0, 2);
        }
        public override double ReadTempLA()
        {
            //double templa = 0.0;
            //templa = xfp.readtempaw(0xA0, 4);
            //return templa;
            return xfp.readtempaw(deviceIndex, 0xA0, 4);
        }
        public override double ReadTempLW()
        {
            //double templw = 0.0;
            //templw = xfp.readtempaw(0xA0, 8);
            //return templw;
            return xfp.readtempaw(deviceIndex, 0xA0, 8);
        }
        public override double ReadTempHW()
        {
            //double temphw = 0.0;
            //temphw = xfp.readtempaw(0xA0, 6);
            //return temphw;
            return xfp.readtempaw(deviceIndex, 0xA0, 6);
        }
        public override double ReadVccLA()
        {
            //double vccla = 0.0;
            //vccla = xfp.readvccaw(0xA0, 44);
            //return vccla;
            return xfp.readvccaw(deviceIndex, 0xA0, 44);
        }
        public override double ReadVccHA()
        {
            //double vccha = 0.0;
            //vccha = xfp.readvccaw(0xA0, 42);
            //return vccha;
            return xfp.readvccaw(deviceIndex, 0xA0, 42);
        }
        public override double ReadVccLW()
        {
            //double vcclw = 0.0;
            //vcclw = xfp.readvccaw(0xA0, 48);
            //return vcclw;
            return xfp.readvccaw(deviceIndex, 0xA0, 48);
        }
        public override double ReadVccHW()
        {
            //double vcchw = 0.0;
            //vcchw = xfp.readvccaw(0xA0, 46);
            //return vcchw;
            return xfp.readvccaw(deviceIndex, 0xA0, 46);
        }
        public override double ReadBiasLA()
        {
            //double biasla = 0.0;
            //biasla = xfp.readbiasaw(0xA0, 20);
            //return biasla;
            return xfp.readbiasaw(deviceIndex, 0xA0, 20);
        }
        public override double ReadBiasHA()
        {
            //double biasha = 0.0;
            //biasha = xfp.readbiasaw(0xA0, 18);
            //return biasha;
            return xfp.readbiasaw(deviceIndex, 0xA0, 18);
        }
        public override double ReadBiasLW()
        {
            //double biaslw = 0.0;
            //biaslw = xfp.readbiasaw(0xA0, 24);
            //return biaslw;
            return xfp.readbiasaw(deviceIndex, 0xA0, 24);
        }
        public override double ReadBiasHW()
        {
            //double biashw = 0.0;
            //biashw = xfp.readbiasaw(0xA0, 22);
            //return biashw;
            return xfp.readbiasaw(deviceIndex, 0xA0, 22);
        }
        public override double ReadTxpLA()
        {
            //double txpla = 0.0;
            //txpla = xfp.readtxpaw(0xA0, 28);
            //return txpla;
            return xfp.readtxpaw(deviceIndex, 0xA0, 28);
        }
        public override double ReadTxpLW()
        {
            //double txplw = 0.0;
            //txplw = xfp.readtxpaw(0xA0, 32);
            //return txplw;
            return xfp.readtxpaw(deviceIndex, 0xA0, 32);
        }
        public override double ReadTxpHA()
        {
            //double txpha = 0.0;
            //txpha = xfp.readtxpaw(0xA0, 26);
            //return txpha;
            return xfp.readtxpaw(deviceIndex, 0xA0, 26);
        }
        public override double ReadTxpHW()
        {
            //double txphw = 0.0;
            //txphw = xfp.readtxpaw(0xA0, 30);
            //return txphw;
            return xfp.readtxpaw(deviceIndex, 0xA0, 30);
        }
        public override double ReadRxpLA()
        {
            //double rxpla = 0.0;
            //rxpla = xfp.readrxpaw(0xA0, 36);
            //return rxpla;
            return xfp.readrxpaw(deviceIndex, 0xA0, 36);
        }
        public override double ReadRxpLW()
        {
            //double rxplw = 0.0;
            //rxplw = xfp.readrxpaw(0xA0, 40);
            //return rxplw;
            return xfp.readrxpaw(deviceIndex, 0xA0, 40);
        }
        public override double ReadRxpHA()
        {
            //double rxpha = 0.0;
            //rxpha = xfp.readrxpaw(0xA0, 34);
            //return rxpha;
            return xfp.readrxpaw(deviceIndex, 0xA0, 34);
        }
        public override double ReadRxpHW()
        {
            //double rxphw = 0.0;
            //rxphw = xfp.readrxpaw(0xA0, 38);
            //return rxphw;
            return xfp.readrxpaw(deviceIndex, 0xA0, 38);
        }
        #endregion
        #region check a/w
        public override bool ChkTempHA()
        {
            //bool tempha;
            //tempha = xfp.ChkTempHA(0xA0, 80);
            //return tempha;
            return xfp.ChkTempHA(deviceIndex, 0xA0, 80);
        }
        public override bool ChkTempLA()
        {
            //bool templa;
            //templa = xfp.ChkTempLA(0xA0, 80);
            //return templa;
            return xfp.ChkTempLA(deviceIndex, 0xA0, 80);

        }
        public override bool ChkVccHA()
        {
            //bool vccha;
            //vccha = xfp.ChkVccHA(0xA0, 81);
            //return vccha;
            return xfp.ChkVccHA(deviceIndex, 0xA0, 81);

        }
        public override bool ChkVccLA()
        {
            //bool vccla;
            //vccla = xfp.ChkVccLA(0xA0, 81);
            //return vccla;
            return xfp.ChkVccLA(deviceIndex, 0xA0, 81);

        }
        public override bool ChkBiasHA()
        {
            //bool biasha;
            //biasha = xfp.ChkBiasHA(0xA0, 80);
            //return biasha;
            return xfp.ChkBiasHA(deviceIndex, 0xA0, 80);

        }
        public override bool ChkBiasLA()
        {
            //bool biasla;
            //biasla = xfp.ChkBiasLA(0xA0, 80);
            //return biasla;
            return xfp.ChkBiasLA(deviceIndex, 0xA0, 80);

        }
        public override bool ChkTxpHA()
        {
            //bool txpha;
            //txpha = xfp.ChkTxpHA(0xA0, 80);
            //return txpha;
            return xfp.ChkTxpHA(deviceIndex, 0xA0, 80);

        }
        public override bool ChkTxpLA()
        {
            //bool txpla;
            //txpla = xfp.ChkTxpLA(0xA0, 80);
            //return txpla;
            return xfp.ChkTxpLA(deviceIndex, 0xA0, 80);

        }
        public override bool ChkRxpHA()
        {
            //bool rxpha;
            //rxpha = xfp.ChkRxpHA(0xA0, 81);
            //return rxpha;
            return xfp.ChkRxpHA(deviceIndex, 0xA0, 81);

        }
        public override bool ChkRxpLA()
        {
            //bool rxpla;
            //rxpla = xfp.ChkRxpLA(0xA0, 81);
            //return rxpla;
            return xfp.ChkRxpLA(deviceIndex, 0xA0, 81);

        }
        public override bool ChkTempHW()
        {
            //bool temphw;
            //temphw = xfp.ChkTempHW(0xA0, 82);
            //return temphw;
            return xfp.ChkTempHW(deviceIndex, 0xA0, 82);

        }
        public override bool ChkTempLW()
        {
            //bool templw;
            //templw = xfp.ChkTempLW(0xA0, 82);
            //return templw;
            return xfp.ChkTempLW(deviceIndex, 0xA0, 82);

        }
        public override bool ChkVccHW()
        {
            //bool vcchw;
            //vcchw = xfp.ChkVccHW(0xA0, 83);
            //return vcchw;
            return xfp.ChkVccHW(deviceIndex, 0xA0, 83);

        }
        public override bool ChkVccLW()
        {
            //bool vcclw;
            //vcclw = xfp.ChkVccLW(0xA0, 83);
            //return vcclw;
            return xfp.ChkVccLW(deviceIndex, 0xA0, 83);

        }
        public override bool ChkBiasHW()
        {
            //bool biashw;
            //biashw = xfp.ChkBiasHW(0xA0, 82);
            //return biashw;
            return xfp.ChkBiasHW(deviceIndex, 0xA0, 82);
        }
        public override bool ChkBiasLW()
        {
            //bool biaslw;
            //biaslw = xfp.ChkBiasLW(0xA0, 82);
            //return biaslw;
            return xfp.ChkBiasLW(deviceIndex, 0xA0, 82);
        }
        public override bool ChkTxpHW()
        {
            //bool txphw;
            //txphw = xfp.ChkTxpHW(0xA0, 82);
            //return txphw;
            return xfp.ChkTxpHW(deviceIndex, 0xA0, 82);
        }
        public override bool ChkTxpLW()
        {
            //bool txplw;
            //txplw = xfp.ChkTxpLW(0xA0, 82);
            //return txplw;
            return xfp.ChkTxpLW(deviceIndex, 0xA0, 82);
        }
        public override bool ChkRxpHW()
        {
            //bool rxphw;
            //rxphw = xfp.ChkRxpHW(0xA0, 83);
            //return rxphw;
            return xfp.ChkRxpHW(deviceIndex, 0xA0, 83);
        }
        public override bool ChkRxpLW()
        {
            //bool rxplw;
            //rxplw = xfp.ChkRxpLW(0xA0, 83);
            //return rxplw;
            return xfp.ChkRxpLW(deviceIndex, 0xA0, 83);
        }
        #endregion
        #region read optional status /control bit 110
        public override bool ChkTxDis()
        {
            //bool txdis;
            //txdis = xfp.ChkRxpLW(0xA0, 110);
            //return txdis;
            return xfp.ChkTxDis(deviceIndex, 0xA0, 110);
        }
        public override bool ChkTxFault()
        {
            //bool txfault;
            //txfault = xfp.ChkTxFault(0xA0, 110);
            //return txfault;
            return xfp.ChkTxFault(deviceIndex, 0xA0, 110);
        }
        public override bool ChkRxLos()
        {
            //bool rxlos;
            //rxlos = xfp.ChkRxLos(0xA0, 110);
            //return rxlos;
            return xfp.ChkRxLos(deviceIndex, 0xA0, 110);
        }
        #endregion
        #region set a/w
        public override void SetTempLA(decimal templa)
        {
            xfp.settempaw(deviceIndex, 0xA0, 4, templa);
        }
        public override void SetTempHA(decimal tempha)
        {
            xfp.settempaw(deviceIndex, 0xA0, 2, tempha);
        }
        public override void SetTempLW(decimal templw)
        {
            xfp.settempaw(deviceIndex, 0xA0, 8, templw);
        }
        public override void SetTempHW(decimal temphw)
        {
            xfp.settempaw(deviceIndex, 0xA0, 6, temphw);
        }
        public override void SetVccHW(decimal vcchw)
        {
            xfp.setvccaw(deviceIndex, 0xA0, 46, vcchw);
        }
        public override void SetVccLW(decimal vcclw)
        {
            xfp.setvccaw(deviceIndex, 0xA0, 48, vcclw);
        }
        public override void SetVccLA(decimal vccla)
        {
            xfp.setvccaw(deviceIndex, 0xA0, 44, vccla);
        }
        public override void SetVccHA(decimal vccha)
        {
            xfp.setvccaw(deviceIndex, 0xA2, 42, vccha);
        }
        public override void SetBiasLA(decimal biasla)
        {
            xfp.setbiasaw(deviceIndex, 0xA0, 20, biasla);
        }
        public override void SetBiasHA(decimal biasha)
        {
            xfp.setbiasaw(deviceIndex, 0xA0, 18, biasha);
        }
        public override void SetBiasHW(decimal biashw)
        {
            xfp.setbiasaw(deviceIndex, 0xA0, 22, biashw);
        }
        public override void SetBiasLW(decimal biaslw)
        {
            xfp.setbiasaw(deviceIndex, 0xA0, 24, biaslw);
        }
        public override void SetTxpLW(decimal txplw)
        {
            xfp.settxpaw(deviceIndex, 0xA0, 32, txplw);
        }
        public override void SetTxpHW(decimal txphw)
        {
            xfp.settxpaw(deviceIndex, 0xA0, 30, txphw);
        }
        public override void SetTxpLA(decimal txpla)
        {
            xfp.settxpaw(deviceIndex, 0xA0, 28, txpla);
        }
        public override void SetTxpHA(decimal txpha)
        {
            xfp.settxpaw(deviceIndex, 0xA0, 26, txpha);
        }
        public override void SetRxpHA(decimal rxpha)
        {
            xfp.setrxpaw(deviceIndex, 0xA0, 34, rxpha);
        }
        public override void SetRxpLA(decimal rxpla)
        {
            xfp.setrxpaw(deviceIndex, 0xA0, 36, rxpla);
        }
        public override void SetRxpHW(decimal rxphw)
        {
            xfp.setrxpaw(deviceIndex, 0xA0, 38, rxphw);
        }
        public override void SetRxpLW(decimal rxplw)
        {
            xfp.setrxpaw(deviceIndex, 0xA0, 40, rxplw);
        }
        public override bool SetSoftTxDis()
        {
            return true;
           // xfp.SetSoftTxDis(deviceIndex, 0xA0, 110);
        }
        #endregion
        #region w/r  sn/pn
        public override string ReadSn()
        {
            string sn = "";
            Engmod(1);
            sn = xfp.ReadSn(deviceIndex, 0xA0, 196);
            return sn;

        }
        public override string ReadPn()
        {
            string pn = "";
            Engmod(1);
            pn = xfp.ReadPn(deviceIndex, 0xA0, 168);
            return pn;

        }
        public override void SetSn(string sn)
        {
            Engmod(1);
            xfp.SetSn(deviceIndex, 0xA0, 196, sn);
            //System.Threading.Thread.Sleep(1000);

        }
        public override void SetPn(string pn)
        {
            Engmod(1);
            xfp.SetPn(deviceIndex, 0xA0, 168, pn);
            //System.Threading.Thread.Sleep(1000);

        }
        #endregion

        //read manufacture data
        #region fwrev
        public override string ReadFWRev()
        {
            string fwrev = "";
            Engmod(3);
            fwrev = xfp.ReadFWRev(deviceIndex, 0xA0, 132);
            return fwrev;
        }
        #endregion
        #region adc
        public override bool ReadTempADC(out UInt16 tempadc)
        {
            tempadc = 0;
            int i = 0;

            //if (FindFiledNameTempSelect(out i, "TEMPERATUREADC"))
            if (algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    tempadc = xfp.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "there is TEMPERATUREADC" + tempadc);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TEMPERATUREADC");
                return true;
            }
        }
        public override bool ReadTempADC(out UInt16 tempadc, byte TempSelect)
        { 
            return ReadTempADC(out tempadc);
        }
        public override bool ReadVccADC(out UInt16 vccadc)
        {
            vccadc = 0;
            int i = 0;

            //if (FindFiledNameVccSelect(out i, "VCCADC"))
            if (algorithm.FindFileName(DutStruct, "VCCADC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    vccadc = xfp.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "there is VCCADC" + vccadc);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no VCCADC");
                return true;
            }
        }
        public override bool ReadVccADC(out UInt16 vccadc, byte vccselect) 
        { 
            return ReadVccADC(out vccadc);
        }
        public override bool ReadBiasADC(out UInt16 biasadc)
        {
            biasadc = 0;
            int i = 0;
            if (FindFiledNameChannel(out i, "TXBIASADC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    biasadc = xfp.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "there is TXBIASADC" + biasadc);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXBIASADC");
                return true;
            }
        }
        public override bool ReadTxpADC( out UInt16 txpadc)
        {
            txpadc = 0;
            int i = 0;

            if (FindFiledNameChannel(out i, "TXPOWERADC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    txpadc = xfp.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "there is TXPOWERADC" + txpadc);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXPOWERADC");
                return true;
            }
        }
        public override bool ReadRxpADC(out UInt16 rxpadc)
        {
            rxpadc = 0;
            int i = 0;

            if (FindFiledNameChannel(out i, "RXPOWERADC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    rxpadc = xfp.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "there is RXPOWERADC" + rxpadc);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no RXPOWERADC");
                return true;
            }
        }
        #endregion

        #region read/write reg/port
        public override byte[] ReadReg(int deviceIndex, int deviceAddress, int regAddress, int readLength)
        {
            return USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, readLength);

        }
        public override byte[] WriteReg(int deviceIndex, int deviceAddress, int regAddress, byte[] dataToWrite)
        {
            return USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
        }

        public override bool WritePort(int id, int deviceIndex, int Port, int DDR)
        {
            return USBIO.WritePort(id, deviceIndex, Port, DDR);
        }
        public override byte[] ReadPort(int id, int deviceIndex, int Port, int DDR)
        {
            return USBIO.ReadPort(id, deviceIndex, Port, DDR);
        }
        #endregion

        public  byte[] WriteDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte[] dataToWrite)
        {
            return xfp.ReadWriteDriverXFP(deviceIndex, deviceAddress, StartAddress, regAddress, 0x80, dataToWrite);
        }
        public  byte[] ReadDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress)
        {
            return xfp.ReadWriteDriverXFP(deviceIndex, deviceAddress, StartAddress, regAddress, 0x40, new byte[2]);
        }
        public  byte[] StoreDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte[] dataToWrite)
        {
            return xfp.ReadWriteDriverXFP(deviceIndex, deviceAddress, StartAddress, regAddress, 0x20, dataToWrite);
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
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
            {
                byte[] WriteBiasDacByteArray = algorithm.ObjectTOByteArray(crossdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                Engmod(engpage);
                WriteDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteBiasDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CROSSDAC");
                return true;
            }
        }
        public override bool WriteBiasDac(object biasdac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "BIASDAC"))
            {
                byte[] WriteBiasDacByteArray = algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                
                Engmod(engpage);
                WriteDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteBiasDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no BIASDAC");
                return true;
            }
        }
        public override bool WriteModDac(object moddac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");

            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "MODDAC"))
            {
                byte[] WriteModDacByteArray = algorithm.ObjectTOByteArray(moddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                
                Engmod(engpage);
                WriteDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteModDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no MODDAC");
                return true;
            }
        }
        public override bool WriteLOSDac(object losdac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "LOSDAC"))
            {
                byte[] WriteLOSDacByteArray = algorithm.ObjectTOByteArray(losdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                
                Engmod(engpage);
                WriteDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteLOSDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDAC");
                return true;
            }
        }
        public override bool WriteLOSDDac(object losddac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "LOSDDAC"))
            {
                byte[] WriteLOSDacByteArray = algorithm.ObjectTOByteArray(losddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                Engmod(engpage);
                WriteDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteLOSDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDDAC");
                return true;
            }
        }
        public override bool WriteAPDDac(object apddac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "APDDAC"))
            {
                byte[] WriteAPDDacByteArray = algorithm.ObjectTOByteArray(apddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                
                Engmod(engpage);
                WriteDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, WriteAPDDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no APDDAC");
                return true;
            }
        }
        public override bool StoreCrossDac(object crossdac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
            {
                byte[] StoreBiasDacByteArray = algorithm.ObjectTOByteArray(crossdac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                Engmod(engpage);
                StoreDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreBiasDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CROSSDAC");
                return true;
            }
        }
        public override bool StoreBiasDac(object biasdac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "BIASDAC"))
            {
                byte[] StoreBiasDacByteArray = algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                
                Engmod(engpage);
                StoreDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreBiasDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no BIASDAC");
                return true;
            }
        }
        public override bool StoreModDac(object moddac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "MODDAC"))
            {
                byte[] StoreModDacByteArray = algorithm.ObjectTOByteArray(moddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                
                Engmod(engpage);
                StoreDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreModDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no MODDAC");
                return true;
            }
        }
        public override bool StoreLOSDac(object losdac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "LOSDAC"))
            {
                byte[] StoreLOSDacByteArray = algorithm.ObjectTOByteArray(losdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                
                Engmod(engpage);
                StoreDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreLOSDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDAC");
                return true;
            }
        }
        public override bool StoreLOSDDac(object losddac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "LOSDDAC"))
            {
                byte[] StoreLOSDacByteArray = algorithm.ObjectTOByteArray(losddac, DriverStruct[i].Length, DriverStruct[i].Endianness);

                Engmod(engpage);
                StoreDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreLOSDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDDAC");
                return true;
            }
        }
        public override bool StoreAPDDac(object apddac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "APDDAC"))
            {
                byte[] StoreAPDDacByteArray = algorithm.ObjectTOByteArray(apddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
               
                Engmod(engpage);
                StoreDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, StoreAPDDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no APDDAC");
                return true;
            }
        }
        //read biasmoddac
        public override bool ReadCrossDac(int length, out byte[] crossdac)
        {
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
            }
            crossdac = new byte[length];
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "CROSSDAC"))
            {
                Engmod(engpage);
                crossdac = ReadDriver10g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CROSSDAC");
                return true;
            }
        }
      
    
        #endregion

        #region set coef
        public override bool SetTempcoefb(string tempcoefb)
        {
            int i = 0;
            bool flag = false;
            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag =  xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITEMPCOEFB To" + tempcoefb);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITEMPCOEFB");
                return true;
            }
        }
        public override bool SetTempcoefb(string tempcoefb, byte TempSelect)
        {
            return SetTempcoefb(tempcoefb);
        }
        public override bool ReadTempcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITEMPCOEFB");
                return true;
            }
        }
        public override bool ReadTempcoefb(out string strcoef, byte TempSelect)
        {
            return ReadTempcoefb(out strcoef);
        }
        public override bool SetTempcoefc(string tempcoefc)
        {
            int i = 0;
            
            bool flag = false;
            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITEMPCOEFC To" + tempcoefc);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());

                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITEMPCOEFC");

                return true;
            }
        }
        public override bool SetTempcoefc(string tempcoefc, byte TempSelect)
        {
            return SetTempcoefc(tempcoefc);
        }
        public override bool ReadTempcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITEMPCOEFC");

                return true;
            }
        }
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        {
            return ReadTempcoefc(out strcoef);
        }
        public override bool SetVcccoefb(string vcccoefb)
        {
            int i = 0;
            
            bool flag = false;
            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMIVCCCOEFB To" + vcccoefb);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());

                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIVCCCOEFB");
                return true;
            }
        }
        public override bool SetVcccoefb(string vcccoefb, byte vccselect)
        {
            return SetVcccoefb(vcccoefb);
        }
        public override bool ReadVcccoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIVCCCOEFB");
                return true;
            }
        }
        public override bool ReadVcccoefb(out string strcoef, byte vccselect)
        {
            return ReadVcccoefb(out strcoef);
        }
        public override bool SetVcccoefc(string vcccoefc)
        {
            int i = 0;
            
            bool flag = false;
            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMIVCCCOEFC To" + vcccoefc);
                    return flag;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIVCCCOEFC");
                return true;
            }
        }
        public override bool SetVcccoefc(string vcccoefc, byte vccselect)
        {
            return SetVcccoefc(vcccoefc);

        }
        public override bool ReadVcccoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIVCCCOEFC");
                return true;
            }
        }
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        {
            return ReadVcccoefc(out strcoef);
        }
        public override bool SetRxpcoefa(string rxcoefa)
        {
            int i = 0;
            bool flag = false;

            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMIRXPOWERCOEFA To" + rxcoefa);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIRXPOWERCOEFA");

                return true;
            }
        }
        public override bool ReadRxpcoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIRXPOWERCOEFA");

                return true;
            }
        }
        public override bool SetRxpcoefb(string rxcoefb)
        {
            int i = 0; bool flag = false;
            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMIRXPOWERCOEFB To" + rxcoefb);
                  
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIRXPOWERCOEFB");
                
                return true;
            }
        }
        public override bool ReadRxpcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIRXPOWERCOEFB");

                return true;
            }
        }
        public override bool SetRxpcoefc(string rxcoefc)
        {
            int i = 0; bool flag = false;
            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMIRXPOWERCOEFC To" + rxcoefc);                   
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIRXPOWERCOEFC");

                return true;
            }
        }
        public override bool ReadRxpcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFC"))
            {

                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMIRXPOWERCOEFC");

                return true;
            }
        }
        public override bool SetTxSlopcoefa(string txslopcoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITXPOWERSLOPCOEFA To" + txslopcoefa);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWERSLOPCOEFA");

                return true;
            }
        }
        public override bool ReadTxSlopcoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWERSLOPCOEFA");

                return true;
            }
        }
        public override bool SetTxSlopcoefb(string txslopcoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITXPOWERSLOPCOEFB To" + txslopcoefb);
                   
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWERSLOPCOEFB");

                return true;
            }
        }
        public override bool ReadTxSlopcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWERSLOPCOEFB");

                return true;
            }
        }
        public override bool SetTxSlopcoefc(string txslopcoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITXPOWERSLOPCOEFC To" + txslopcoefc);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWERSLOPCOEFC");

                return true;
            }
        }
        public override bool ReadTxSlopcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXPOWERSLOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3,error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWERSLOPCOEFC");
                return true;
            }
        }
        public override bool SetTxOffsetcoefa(string txoffsetcoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITXPOWEROFFSETCOEFA To" + txoffsetcoefa); 
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWEROFFSETCOEFA");
              
                return true;
            }
        }
        public override bool ReadTxOffsetcoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWEROFFSETCOEFA");

                return true;
            }
        }
        public override bool SetTxOffsetcoefb(string txoffsetcoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITXPOWEROFFSETCOEFB To" + txoffsetcoefb);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWEROFFSETCOEFB");
             
                return true;
            }
        }
        public override bool ReadTxOffsetcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWEROFFSETCOEFB");

                return true;
            }
        }
        public override bool SetTxOffsetcoefc(string txoffsetcoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMITXPOWEROFFSETCOEFC To" + txoffsetcoefc); 
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWEROFFSETCOEFC");
               
                return true;
            }
        }
        public override bool ReadTxOffsetcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (FindFiledNameChannel(out i, "DMITXPOWEROFFSETCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no DMITXPOWEROFFSETCOEFC");

                return true;
            }
        }
        public override bool SetBiasdaccoefa(string biasdaccoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXTARGETBIASDACCOEFA To" + biasdaccoefa);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETBIASDACCOEFA");
               
                return true;
            }
        }
        public override bool ReadBiasdaccoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETBIASDACCOEFA");
                return true;
            }
        }
        public override bool SetBiasdaccoefb(string biasdaccoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXTARGETBIASDACCOEFB To" + biasdaccoefb);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETBIASDACCOEFB");
               
                return true;
            }
        }
        public override bool ReadBiasdaccoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETBIASDACCOEFB");

                return true;
            }
        }
        public override bool SetBiasdaccoefc(string biasdaccoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXTARGETBIASDACCOEFC To" + biasdaccoefc);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());

                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETBIASDACCOEFC");
              
                return true;
            }
        }
        public override bool ReadBiasdaccoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETBIASDACCOEFC");
                return true;
            }
        }
        public override bool SetModdaccoefa(string moddaccoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXTARGETMODDACCOEFA To" + moddaccoefa);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFA");
                
                return true;
            }
        }
        public override bool ReadModdaccoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFA");

                return true;
            }
        }
        public override bool SetModdaccoefb(string moddaccoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXTARGETMODDACCOEFB To" + moddaccoefb);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFB");
                
                return true;
            }
        }
        public override bool ReadModdaccoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;

                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFB");

                return true;
            }
        }
        public override bool SetModdaccoefc(string moddaccoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXTARGETMODDACCOEFC To" + moddaccoefc);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFC");
               
                return true;
            }
        }
        public override bool ReadModdaccoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFC");

                return true;
            }
        }
        public override bool SetTargetLopcoefa(string targetlopcoefa)
        {
            bool flag = false;
            int i = 0;
            if (FindFiledNameChannel(out i, "TARGETLOPCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TARGETLOPCOEFA To" + targetlopcoefa);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TARGETLOPCOEFA");
               
                return true;
            }
        }
        public override bool ReadTargetLopcoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TARGETLOPCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TARGETLOPCOEFA");

                return true;
            }
        }
        public override bool SetTargetLopcoefb(string targetlopcoefb)
        {
            bool flag = false;
            int i = 0;
            if (FindFiledNameChannel(out i, "TARGETLOPCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TARGETLOPCOEFB To" + targetlopcoefb);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TARGETLOPCOEFB");
               
                return true;
            }
        }
        public override bool ReadTargetLopcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TARGETLOPCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());

                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TARGETLOPCOEFB");

                return true;
            }
        }
        public override bool SetTargetLopcoefc(string targetlopcoefc)
        {
            bool flag = false;
            int i = 0;
            if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = xfp.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TARGETLOPCOEFC To" + targetlopcoefc);
                    return flag;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TARGETLOPCOEFC");
               
                return true;
            }
        }
        public override bool ReadTargetLopcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = xfp.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
                    return true;

                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no TARGETLOPCOEFC");
                return true;
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

            if (algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    USBIO.WrtieReg(deviceIndex, 0xA0, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                    return true;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no APCCONTROLL");

                return true;
            }
        }
        public override bool APCOFF(byte apcswitch)
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
            if (algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    USBIO.WrtieReg(deviceIndex, 0xA0, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                    return true;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no APCCONTROLL");

                return true;
            }
        }

        public override bool APCStatus(out byte apcflag)
        {
            //0 OFF.1 ON
            apcflag = 0x00;
            int i = 0;
            byte[] buff = new byte[1];
            if (algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    buff = USBIO.ReadReg(deviceIndex, 0xA0, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

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
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
            {
                logger.AdapterLogString(4, "there is no APCCONTROLL");

                return true;
            }


        }
        #endregion
    }
}
