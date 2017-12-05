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
    public class SFPDUT : DUT
    {
        EEPROM sfp;
        public Algorithm algorithm = new Algorithm();
        public SFPDUT(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(DutStruct[] List)
        {
            try
            {
                DutStruct = List;
                sfp = new EEPROM(deviceIndex, logger);
                if (!Connect()) return false;
               
            }

            catch (Error_Message error)
            {

                throw new Error_Message(this.GetType().ToString() + " " + error.Message, error);
            }
            return true;
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
            USBIO.WrtieReg(deviceIndex, 0xA2, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        }
        //dmi
        public override double ReadDmiTemp()
        {
            //double dmitemp=0.0;
            //dmitemp = sfp.readdmitemp(0xA2, 96);
            //return dmitemp;
            return sfp.readdmitemp(deviceIndex,0xA2, 96);

        }
        public override double ReadDmiVcc()
        {
            //double dmivcc = 0.0;
            //dmivcc = sfp.readdmivcc(0xA2, 98);
            //return dmivcc;
            return sfp.readdmivcc(deviceIndex, 0xA2, 98);
        }
        public override double ReadDmiBias()
        {
            //double dmibias = 0.0;
            //dmibias = sfp.readdmibias(0xA2, 100);
            //return dmibias;
            return sfp.readdmibias(deviceIndex, 0xA2, 100);
        }
        public override double ReadDmiTxp()
        {
            //double dmitxp = 0.0;
            //dmitxp = sfp.readdmitxp(0xA2, 102);
            //return dmitxp;
            return sfp.readdmitxp(deviceIndex, 0xA2, 102);
        }
        public override double ReadDmiRxp()
        {
            //double dmirxp = 0.0;
            //dmirxp = sfp.readdmirxp(0xA2, 104);
            //return dmirxp;
            return sfp.readdmirxp(deviceIndex, 0xA2, 104);
        }
        //a/w
        public override double ReadTempHA()
        {
            //double tempha = 0.0;
            //tempha = sfp.readtempaw(0xA2, 0);
            //return tempha;
            return sfp.readtempaw(deviceIndex, 0xA2, 0);
        }
        public override double ReadTempLA()
        {
            //double templa = 0.0;
            //templa = sfp.readtempaw(0xA2, 2);
            //return templa;
            return sfp.readtempaw(deviceIndex, 0xA2, 2);
        }
        public override double ReadTempLW()
        {
            //double templw = 0.0;
            //templw = sfp.readtempaw(0xA2, 6);
            //return templw;
            return sfp.readtempaw(deviceIndex, 0xA2, 6);

        }
        public override double ReadTempHW()
        {
            //double temphw = 0.0;
            //temphw = sfp.readtempaw(0xA2, 4);
            //return temphw;
            return sfp.readtempaw(deviceIndex, 0xA2, 4);
        }
        public override double ReadVccLA()
        {
            //double vccla = 0.0;
            //vccla = sfp.readvccaw(0xA2, 10);
            //return vccla;
            return sfp.readvccaw(deviceIndex, 0xA2, 10);

        }
        public override double ReadVccHA()
        {
            //double vccha = 0.0;
            //vccha = sfp.readvccaw(0xA2, 8);
            //return vccha;
            return sfp.readvccaw(deviceIndex, 0xA2, 8);

        }
        public override double ReadVccLW()
        {
            //double vcclw = 0.0;
            //vcclw = sfp.readvccaw(0xA2, 14);
            //return vcclw;
            return sfp.readvccaw(deviceIndex, 0xA2, 14);
        }
        public override double ReadVccHW()
        {
            //double vcchw = 0.0;
            //vcchw = sfp.readvccaw(0xA2, 12);
            //return vcchw;
            return sfp.readvccaw(deviceIndex, 0xA2, 12);

        }
        public override double ReadBiasLA()
        {
            //double biasla = 0.0;
            //biasla = sfp.readbiasaw(0xA2, 18);
            //return biasla;
            return sfp.readbiasaw(deviceIndex, 0xA2, 18);

        }
        public override double ReadBiasHA()
        {
            //double biasha = 0.0;
            //biasha = sfp.readbiasaw(0xA2, 16);
            //return biasha;
            return sfp.readbiasaw(deviceIndex, 0xA2, 16);

        }
        public override double ReadBiasLW()
        {
            //double biaslw = 0.0;
            //biaslw = sfp.readbiasaw(0xA2, 22);
            //return biaslw;
            return sfp.readbiasaw(deviceIndex, 0xA2, 22);

        }
        public override double ReadBiasHW()
        {
            //double biashw = 0.0;
            //biashw = sfp.readbiasaw(0xA2, 20);
            //return biashw;
            return sfp.readbiasaw(deviceIndex, 0xA2, 20);
        }
        public override double ReadTxpLA()
        {
            //double txpla = 0.0;
            //txpla = sfp.readtxpaw(0xA2, 26);
            //return txpla;
            return sfp.readtxpaw(deviceIndex, 0xA2, 26);

        }
        public override double ReadTxpLW()
        {
            //double txplw = 0.0;
            //txplw = sfp.readtxpaw(0xA2, 30);
            //return txplw;
            return sfp.readtxpaw(deviceIndex, 0xA2, 30);

        }
        public override double ReadTxpHA()
        {
            //double txpha = 0.0;
            //txpha = sfp.readtxpaw(0xA2, 24);
            //return txpha;
            return sfp.readtxpaw(deviceIndex, 0xA2, 24);

        }
        public override double ReadTxpHW()
        {
            //double txphw = 0.0;
            //txphw = sfp.readtxpaw(0xA2, 28);
            //return txphw;
            return sfp.readtxpaw(deviceIndex, 0xA2, 28);
        }
        public override double ReadRxpLA()
        {
            //double rxpla = 0.0;
            //rxpla = sfp.readrxpaw(0xA2, 34);
            //return rxpla;
            return sfp.readrxpaw(deviceIndex, 0xA2, 34);

        }
        public override double ReadRxpLW()
        {
            //double rxplw = 0.0;
            //rxplw = sfp.readrxpaw(0xA2, 38);
            //return rxplw;
            return sfp.readrxpaw(deviceIndex, 0xA2, 38);
        }
        public override double ReadRxpHA()
        {
            //double rxpha = 0.0;
            //rxpha = sfp.readrxpaw(0xA2, 32);
            //return rxpha;
            return sfp.readrxpaw(deviceIndex, 0xA2, 32);
        }
        public override double ReadRxpHW()
        {
            //double rxphw = 0.0;
            //rxphw = sfp.readrxpaw(0xA2, 36);
            //return rxphw;
            return sfp.readrxpaw(deviceIndex, 0xA2, 36);
        }
        //check a/w
        public override bool ChkTempHA()
        {
            //bool tempha;
            //tempha = sfp.ChkTempHA(0xA2, 112);
            //return tempha;
            return sfp.ChkTempHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkTempLA()
        {
            //bool templa;
            //templa = sfp.ChkTempLA(0xA2, 112);
            //return templa;
            return sfp.ChkTempLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkVccHA()
        {
            //bool vccha;
            //vccha = sfp.ChkVccHA(0xA2, 112);
            //return vccha;
            return sfp.ChkVccHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkVccLA()
        {
            //bool vccla;
            //vccla = sfp.ChkVccLA(0xA2, 112);
            //return vccla;
            return sfp.ChkVccLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkBiasHA()
        {
            //bool biasha;
            //biasha = sfp.ChkBiasHA(0xA2, 112);
            //return biasha;
            return sfp.ChkBiasHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkBiasLA()
        {
            //bool biasla;
            //biasla = sfp.ChkBiasLA(0xA2, 112);
            //return biasla;
            return sfp.ChkBiasLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkTxpHA()
        {
            //bool txpha;
            //txpha = sfp.ChkTxpHA(0xA2, 112);
            //return txpha;
            return sfp.ChkTxpHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkTxpLA()
        {
            //bool txpla;
            //txpla = sfp.ChkTxpLA(0xA2, 112);
            //return txpla;
            return sfp.ChkTxpLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkRxpHA()
        {
            //bool rxpha;
            //rxpha = sfp.ChkRxpHA(0xA2, 113);
            //return rxpha;
            return sfp.ChkRxpHA(deviceIndex, 0xA2, 113);
        }
        public override bool ChkRxpLA()
        {
            //bool rxpla;
            //rxpla = sfp.ChkRxpLA(0xA2, 113);
            //return rxpla;
            return sfp.ChkRxpLA(deviceIndex, 0xA2, 113);
        }
        public override bool ChkTempHW()
        {
            //bool temphw;
            //temphw = sfp.ChkTempHW(0xA2, 116);
            //return temphw;
            return sfp.ChkTempHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkTempLW()
        {
            //bool templw;
            //templw = sfp.ChkTempLW(0xA2, 116);
            //return templw;
            return sfp.ChkTempLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkVccHW()
        {
            //bool vcchw;
            //vcchw = sfp.ChkVccHW(0xA2, 116);
            //return vcchw;
            return sfp.ChkVccHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkVccLW()
        {
            //bool vcclw;
            //vcclw = sfp.ChkVccLW(0xA2, 116);
            //return vcclw;
            return sfp.ChkVccLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkBiasHW()
        {
            //bool biashw;
            //biashw = sfp.ChkBiasHW(0xA2, 116);
            //return biashw;
            return sfp.ChkBiasHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkBiasLW()
        {
            //bool biaslw;
            //biaslw = sfp.ChkBiasLW(0xA2, 116);
            //return biaslw;
            return sfp.ChkBiasLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkTxpHW()
        {
            //bool txphw;
            //txphw = sfp.ChkTxpHW(0xA2, 116);
            //return txphw;
            return sfp.ChkTxpHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkTxpLW()
        {
            //bool txplw;
            //txplw = sfp.ChkTxpLW(0xA2, 116);
            //return txplw;
            return sfp.ChkTxpLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkRxpHW()
        {
            //bool rxphw;
            //rxphw = sfp.ChkRxpHW(0xA2, 117);
            //return rxphw;
            return sfp.ChkRxpHW(deviceIndex, 0xA2, 117);
        }
        public override bool ChkRxpLW()
        {
            //bool rxplw;
            //rxplw = sfp.ChkRxpLW(0xA2, 117);
            //return rxplw;
            return sfp.ChkRxpLW(deviceIndex, 0xA2, 117);
        }
        //read optional status /control bit 110
        public override bool ChkTxDis()
        {
            //bool txdis;
            //txdis = sfp.ChkTxDis(0xA2, 110);
            //return txdis;
            return sfp.ChkTxDis(deviceIndex, 0xA2, 110);
        }
        public override bool ChkTxFault()
        {
            //bool txfault;
            //txfault = sfp.ChkTxFault(0xA2, 110);
            //return txfault;
            return sfp.ChkTxFault(deviceIndex, 0xA2, 110);
        }
        public override bool ChkRxLos()
        {
            //bool rxlos;
            //rxlos = sfp.ChkRxLos(0xA2, 110);
            //return rxlos;
            return sfp.ChkRxLos(deviceIndex, 0xA2, 110);
        }
        //set a/w
        public override void SetTempLA(decimal templa)
        {
            sfp.settempaw(deviceIndex, 0xA2, 2, templa);
        }
        public override void SetTempHA(decimal tempha)
        {
            sfp.settempaw(deviceIndex, 0xA2, 0, tempha);
        }
        public override void SetTempLW(decimal templw)
        {
            sfp.settempaw(deviceIndex, 0xA2, 6, templw);
        }
        public override void SetTempHW(decimal temphw)
        {
            sfp.settempaw(deviceIndex, 0xA2, 4, temphw);
        }
        public override void SetVccHW(decimal vcchw)
        {
            sfp.setvccaw(deviceIndex, 0xA2, 12, vcchw);
        }
        public override void SetVccLW(decimal vcclw)
        {
            sfp.setvccaw(deviceIndex, 0xA2, 14, vcclw);
        }
        public override void SetVccLA(decimal vccla)
        {
            sfp.setvccaw(deviceIndex, 0xA2, 10, vccla);
        }
        public override void SetVccHA(decimal vccha)
        {
            sfp.setvccaw(deviceIndex, 0xA2, 8, vccha);
        }
        public override void SetBiasLA(decimal biasla)
        {
            sfp.setbiasaw(deviceIndex, 0xA2, 18, biasla);
        }
        public override void SetBiasHA(decimal biasha)
        {
            sfp.setbiasaw(deviceIndex, 0xA2, 16, biasha);
        }
        public override void SetBiasHW(decimal biashw)
        {
            sfp.setbiasaw(deviceIndex, 0xA2, 20, biashw);
        }
        public override void SetBiasLW(decimal biaslw)
        {
            sfp.setbiasaw(deviceIndex, 0xA2, 22, biaslw);
        }
        public override void SetTxpLW(decimal txplw)
        {
            sfp.settxpaw(deviceIndex, 0xA2, 30, txplw);
        }
        public override void SetTxpHW(decimal txphw)
        {
            sfp.settxpaw(deviceIndex, 0xA2, 28, txphw);
        }
        public override void SetTxpLA(decimal txpla)
        {
            sfp.settxpaw(deviceIndex, 0xA2, 26, txpla);
        }
        public override void SetTxpHA(decimal txpha)
        {
            sfp.settxpaw(deviceIndex, 0xA2, 24, txpha);
        }
        public override void SetRxpHA(decimal rxpha)
        {
            sfp.setrxpaw(deviceIndex, 0xA2, 32, rxpha);
        }
        public override void SetRxpLA(decimal rxpla)
        {
            sfp.setrxpaw(deviceIndex, 0xA2, 34, rxpla);
        }
        public override void SetRxpHW(decimal rxphw)
        {
            sfp.setrxpaw(deviceIndex, 0xA2, 36, rxphw);
        }
        public override void SetRxpLW(decimal rxplw)
        {
            sfp.setrxpaw(deviceIndex, 0xA2, 38, rxplw);
        }
        public override void SetSoftTxDis()
        {
            sfp.SetSoftTxDis(deviceIndex, 0xA2, 110);
        }
        //w/r  sn/pn
        public override string ReadSn()
        {
            //string sn = "";
            //sn = sfp.ReadSn(0xA0, 68);
            //return sn;
            return sfp.ReadSn(deviceIndex, 0xA0, 68);
        }
        public override string ReadPn()
        {
            //string pn = "";
            //pn = sfp.ReadPn(0xA0, 40);
            //return pn;
            return sfp.ReadPn(deviceIndex, 0xA0, 40);
        }
        public override void SetSn(string sn)
        {
            sfp.SetSn(deviceIndex, 0xA0, 68, sn);
            //System.Threading.Thread.Sleep(1000);
        }
        public override void SetPn(string pn)
        {
            sfp.SetPn(deviceIndex, 0xA0, 40, pn);
            //System.Threading.Thread.Sleep(1000);

        }
        //read manufacture data
        //fwrev
        public override string ReadFWRev()
        {
            //string fwrev = "";
            //fwrev = sfp.ReadFWRev(0xA2, 121);
            //return fwrev;
            return sfp.ReadFWRev(deviceIndex, 0xA2, 121);
        }
        public override bool ReadTempADC( out UInt16 tempadc)
        {
            tempadc = 0;
            int i = 0;

            //if (FindFiledNameTempSelect(out i, "TEMPERATUREADC"))
            if (algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    tempadc = sfp.readadc(deviceIndex, 0xA2, DutStruct[i].StartAddress);
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
                    vccadc = sfp.readadc(deviceIndex, 0xA2, DutStruct[i].StartAddress);
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
        public override bool ReadBiasADC( out UInt16 biasadc)
        {
            biasadc = 0;
            int i = 0;
            if (FindFiledNameChannel(out i, "TXBIASADC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    biasadc = sfp.readadc(deviceIndex, 0xA2, DutStruct[i].StartAddress);
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
        public override bool ReadTxpADC(out UInt16 txpadc)
        {
            txpadc = 0;
            int i = 0;

            if (FindFiledNameChannel(out i, "TXPOWERADC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    txpadc = sfp.readadc(deviceIndex, 0xA2, DutStruct[i].StartAddress);
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
                    rxpadc = sfp.readadc(deviceIndex, 0xA2, DutStruct[i].StartAddress);
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

        //read/writereg
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
        public  byte[] WriteDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte[] dataToWrite)
        {
            return sfp.ReadWriteDriver10g(deviceIndex, deviceAddress, StartAddress, regAddress, 0x80, dataToWrite);
        }
        public  byte[] ReadDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress)
        {
            return sfp.ReadWriteDriver10g(deviceIndex, deviceAddress, StartAddress, regAddress, 0x40, new byte[2]);
        }
        public  byte[] StoreDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte[] dataToWrite)
        {
            return sfp.ReadWriteDriver10g(deviceIndex, deviceAddress, StartAddress, regAddress, 0x20, dataToWrite);
        }
        //set biasmoddac
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
                WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, WriteBiasDacByteArray);
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
                WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, WriteModDacByteArray);
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
                WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, WriteLOSDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDAC");
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
                WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, WriteAPDDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no APDDAC");
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
                StoreDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, StoreBiasDacByteArray);
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
                StoreDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, StoreModDacByteArray);
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
                StoreDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, StoreLOSDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDAC");
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
                StoreDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, StoreAPDDacByteArray);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no APDDAC");
                return true;
            }
        }
        //read biasmoddac
        public override bool ReadBiasDac(int length, out byte[] BiasDac)
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
            BiasDac = new byte[length];
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "BIASDAC"))
            {
                Engmod(engpage);
                BiasDac = ReadDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no BIASDAC");
                return true;
            }
        }
        public override bool ReadModDac(int length, out byte[] ModDac)
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
            ModDac = new byte[length];
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "MODDAC"))
            {
                Engmod(engpage);
                ModDac = ReadDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no MODDAC");
                return true;
            }
        }
        public override bool ReadLOSDac(int length, out byte[] LOSDac)
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
            LOSDac = new byte[length];
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "LOSDAC"))
            {
                Engmod(engpage);
                LOSDac = ReadDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDAC");
                return true;
            }
        }
        public override bool ReadAPDDac(int length, out byte[] APDDac)
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
            APDDac = new byte[length];
            int i = 0;
            if (FindFiledNameChannelDAC(out i, "APDDAC"))
            {
                Engmod(engpage);
                APDDac = ReadDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no APDDAC");
                return true;
            }
        }

        //set coef
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET DMIVCCCOEFB" + vcccoefb);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetRxpcoefa(string rxcoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, rxcoefa, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, rxcoefb, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, rxcoefc, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txslopcoefa, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txslopcoefb, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txslopcoefc, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txoffsetcoefa, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txoffsetcoefb, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txoffsetcoefc, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, biasdaccoefa, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, biasdaccoefb, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, biasdaccoefc, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, moddaccoefa, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, moddaccoefb, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, moddaccoefc, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, targetlopcoefa, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, targetlopcoefb, DutStruct[i].Format);
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, targetlopcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TARGETLOPCOEFC To" + targetlopcoefc); return flag;
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
                    strcoef = sfp.ReadCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        
    }
}
