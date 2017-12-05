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

        enum Writer_Store:byte
        {
            Writer = 0,
            Store=1
        }

        EEPROM sfp;
        public Algorithm algorithm = new Algorithm();
        public SFP28DUT(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList, DriverInitializeStruct[] DriverinitList, DutEEPROMInitializeStuct[] EEpromInitList, string AuxAttribles)
        {
            try
            {
                DutStruct = DutList;
                DriverStruct = DriverList;
                DriverInitStruct = DriverinitList;
                EEpromInitStruct = EEpromInitList;
                string temp = AuxAttribles.ToUpper().Replace(" ", "");
                ChipsetControll = temp.Contains("OLDDRIVER=1");
                sfp = new EEPROM(deviceIndex, logger);
                if (!Connect()) return false;

            }

            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;

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
      
        #region dmi
        public override double ReadDmiTemp()
        {
            return sfp.readdmitemp(deviceIndex,0xA2, 96);

        }
        public override double ReadDmiVcc()
        {
            return sfp.readdmivcc(deviceIndex, 0xA2, 98);
        }
        public override double ReadDmiBias()
        {
           
            return sfp.readdmibias(deviceIndex, 0xA2, 100);
        }
        public override double ReadDmiTxp()
        {
            return sfp.readdmitxp(deviceIndex, 0xA2, 102);
        }
        public override double ReadDmiRxp()
        {
            return sfp.readdmirxp(deviceIndex, 0xA2, 104);
        }
        #endregion
        #region read a/w
        public override double ReadTempHA()
        {
            return sfp.readtempaw(deviceIndex, 0xA2, 0);
        }
        public override double ReadTempLA()
        {
            return sfp.readtempaw(deviceIndex, 0xA2, 2);
        }
        public override double ReadTempLW()
        {
            return sfp.readtempaw(deviceIndex, 0xA2, 6);

        }
        public override double ReadTempHW()
        {
            return sfp.readtempaw(deviceIndex, 0xA2, 4);
        }
        public override double ReadVccLA()
        {
            return sfp.readvccaw(deviceIndex, 0xA2, 10);

        }
        public override double ReadVccHA()
        {
            return sfp.readvccaw(deviceIndex, 0xA2, 8);

        }
        public override double ReadVccLW()
        {
            return sfp.readvccaw(deviceIndex, 0xA2, 14);
        }
        public override double ReadVccHW()
        {
            return sfp.readvccaw(deviceIndex, 0xA2, 12);

        }
        public override double ReadBiasLA()
        {
            return sfp.readbiasaw(deviceIndex, 0xA2, 18);

        }
        public override double ReadBiasHA()
        {
            return sfp.readbiasaw(deviceIndex, 0xA2, 16);

        }
        public override double ReadBiasLW()
        {
            return sfp.readbiasaw(deviceIndex, 0xA2, 22);

        }
        public override double ReadBiasHW()
        {
            return sfp.readbiasaw(deviceIndex, 0xA2, 20);
        }
        public override double ReadTxpLA()
        {
            return sfp.readtxpaw(deviceIndex, 0xA2, 26);

        }
        public override double ReadTxpLW()
        {
            return sfp.readtxpaw(deviceIndex, 0xA2, 30);

        }
        public override double ReadTxpHA()
        {
            return sfp.readtxpaw(deviceIndex, 0xA2, 24);

        }
        public override double ReadTxpHW()
        {
            return sfp.readtxpaw(deviceIndex, 0xA2, 28);
        }
        public override double ReadRxpLA()
        {
            return sfp.readrxpaw(deviceIndex, 0xA2, 34);

        }
        public override double ReadRxpLW()
        {
            return sfp.readrxpaw(deviceIndex, 0xA2, 38);
        }
        public override double ReadRxpHA()
        {
            return sfp.readrxpaw(deviceIndex, 0xA2, 32);
        }
        public override double ReadRxpHW()
        {
            return sfp.readrxpaw(deviceIndex, 0xA2, 36);
        }
        #endregion
        #region check a/w
        public override bool ChkTempHA()
        {
            return sfp.ChkTempHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkTempLA()
        {
            return sfp.ChkTempLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkVccHA()
        {
            return sfp.ChkVccHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkVccLA()
        {
            return sfp.ChkVccLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkBiasHA()
        {
            return sfp.ChkBiasHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkBiasLA()
        {
            return sfp.ChkBiasLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkTxpHA()
        {
            return sfp.ChkTxpHA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkTxpLA()
        {
            return sfp.ChkTxpLA(deviceIndex, 0xA2, 112);
        }
        public override bool ChkRxpHA()
        {
            return sfp.ChkRxpHA(deviceIndex, 0xA2, 113);
        }
        public override bool ChkRxpLA()
        {
            return sfp.ChkRxpLA(deviceIndex, 0xA2, 113);
        }
        public override bool ChkTempHW()
        {
            return sfp.ChkTempHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkTempLW()
        {
            return sfp.ChkTempLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkVccHW()
        {
            return sfp.ChkVccHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkVccLW()
        {
            return sfp.ChkVccLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkBiasHW()
        {
            return sfp.ChkBiasHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkBiasLW()
        {
            return sfp.ChkBiasLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkTxpHW()
        {
            return sfp.ChkTxpHW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkTxpLW()
        {
            return sfp.ChkTxpLW(deviceIndex, 0xA2, 116);
        }
        public override bool ChkRxpHW()
        {
            return sfp.ChkRxpHW(deviceIndex, 0xA2, 117);
        }
        public override bool ChkRxpLW()
        {
            return sfp.ChkRxpLW(deviceIndex, 0xA2, 117);
        }
        #endregion
        #region read optional status /control bit 110

        public override bool ChkTxDis()
        {
            return sfp.ChkTxDis(deviceIndex, 0xA2, 110);


        
        }

        public override bool ChkTxFault()
        {
            return sfp.ChkTxFault(deviceIndex, 0xA2, 110);
        }
        public override bool ChkRxLos()
        {
            return sfp.ChkRxLos(deviceIndex, 0xA2, 110);
        }
        #endregion
        #region set a/w
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
        public override bool SetSoftTxDis()
        {
         
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                buff[0] = (byte)(buff[0] | 0x40);
                byte TempBuff = buff[0];
                for (int i = 0; i < 3; i++)
                {

                    USBIO.WrtieReg(deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                    System.Threading.Thread.Sleep(200);
                    buff = USBIO.ReadReg(deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if (buff[0] == TempBuff)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.Message);
                return false;
            }

        }
        public override bool TxAllChannelEnable()
        {
      
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                buff[0] = (byte)(buff[0] & 0xBF);
                byte TempBuff = buff[0];
                for (int i = 0; i < 3;i++ )
                {

                    USBIO.WrtieReg(deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                    System.Threading.Thread.Sleep(200);
                    buff = USBIO.ReadReg(deviceIndex, 0XA2, 110, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                    if (buff[0] == TempBuff)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.Message);
                return false;
            }
        }
        #endregion
        #region w/r  sn/pn
        public override string ReadSn()
        {
            return sfp.ReadSn(deviceIndex, 0xA0, 68);
        }
        public override string ReadPn()
        {
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
        #endregion
        //read manufacture data
        #region fwrev
        public override string ReadFWRev()
        {
            return sfp.ReadFWRev(deviceIndex, 0xA2, 121);
        }
        #endregion
        #region adc
        public override bool ReadTempADC( out UInt16 tempadc)
        {
            tempadc = 0;
            int i = 0;
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
        public override bool ReadTempADC(out UInt16 tempadc, byte tempselect)
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
        public override bool ReadVccADC(out UInt16 vccadc, byte vccselect)
        {
            return ReadVccADC(out vccadc);
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
        
        #region driver
        public byte[] WriteDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset, byte[] dataToWrite)
        {
            return sfp.ReadWriteDriverSFP(deviceIndex, deviceAddress, StartAddress, regAddress, 0x02, chipset, dataToWrite);
        }
        public byte[] ReadDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset)// 有问题,但是需要确认,默认写长度为2不合适 Leo 2016-3-7
        {
            return sfp.ReadWriteDriverSFP(deviceIndex, deviceAddress, StartAddress, regAddress, 0x01, chipset, new byte[2]);
        }
        public byte[] ReadDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset, int Readlength)
        {
            return sfp.ReadWriteDriverSFP(deviceIndex, deviceAddress, StartAddress, regAddress, 0x01, chipset, new byte[Readlength]);
        }
        public byte[] StoreDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte chipset, byte[] dataToWrite)
        {
            return sfp.ReadWriteDriverSFP(deviceIndex, deviceAddress, StartAddress, regAddress, 0x06, chipset, dataToWrite);
        }

        //driver innitialize
        public override bool DriverInitialize()
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac,3cdr
            int j = 0;
            int k = 0;
            byte engpage = 0;
            int startaddr = 0;
            byte chipset = 0x01;
            byte[] temp;
            bool flag = true;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                engpage = DutStruct[j].EngPage;
                startaddr = DutStruct[j].StartAddress;

            }
            else
            {
                logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
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
                        byte[] WriteBiasDacByteArray = algorithm.ObjectTOByteArray(DriverInitStruct[i].ItemValue, DriverInitStruct[i].Length, DriverInitStruct[i].Endianness);
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
                            WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteBiasDacByteArray);
 
                            StoreDriver10g(deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress, chipset, WriteBiasDacByteArray);
                            // Thread.Sleep(200);  
                            temp = new byte[DriverInitStruct[i].Length];
                            temp = ReadDriver10g(deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress,  chipset);

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
        //eeprominit
        //联调通过ee
        public override bool EEpromInitialize()
        {
            int j = 0;
            int k = 0;
            byte engpage = 0;
            byte[] temp;
            bool flag = true;
            if (EEpromInitStruct.Length == 0)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < EEpromInitStruct.Length; i++)
                {
                    byte[] WriteBiasDacByteArray = algorithm.ObjectTOByteArray(EEpromInitStruct[i].ItemValue, 1, true);
                    engpage = EEpromInitStruct[j].Page;
                    Engmod(engpage);
                    for (k = 0; k < 3; k++)
                    {
                        WriteReg(deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, WriteBiasDacByteArray);
                        temp = new byte[1];
                        temp = ReadReg(deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, 1);
                        if (BitConverter.ToString(temp) == BitConverter.ToString(WriteBiasDacByteArray))
                            break;
                    }
                    if (k >= 3)
                        flag = false;

                }
                return flag;
            }
        }


        //set biasmoddac
        public override bool WriteCrossDac(object crossdac)
        {


            return WriteDac("CROSSDAC", crossdac);
              
        }

        #region  OldDriver
        //public override bool WriteBiasDac(object biasdac)
        //{
        //    int j = 0;
        //    byte engpage = 0;
        //    int startaddr = 0;
        //    byte chipset = 0x01;
        //    if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        engpage = DutStruct[j].EngPage;
        //        startaddr = DutStruct[j].StartAddress;

        //    }
        //    else
        //    {
        //        logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
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
        //        byte[] WriteBiasDacByteArray = algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
        //        Engmod(engpage);
        //        WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteBiasDacByteArray);
        //        return true;
        //    }
        //    else
        //    {
        //        logger.AdapterLogString(4, "there is no BIASDAC");
        //        return true;
        //    }
        //}
        //public override bool WriteModDac(object moddac)
        //{
        //    byte chipset = 0x01;
        //    int j = 0;
        //    byte engpage = 0;
        //    int startaddr = 0;
        //    if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        engpage = DutStruct[j].EngPage;
        //        startaddr = DutStruct[j].StartAddress;

        //    }
        //    else
        //    {
        //        logger.AdapterLogString(4, "there is no DEBUGINTERFACE");

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
        //        byte[] WriteModDacByteArray = algorithm.ObjectTOByteArray(moddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
        //        Engmod(engpage);
        //        WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteModDacByteArray);
        //        return true;
        //    }
        //    else
        //    {
        //        logger.AdapterLogString(4, "there is no MODDAC");
        //        return true;
        //    }
        //}
        //public override bool WriteMaskDac(object DAC)
        //{
        //    byte chipset = 0x01;
        //    int j = 0;
        //    byte engpage = 0;
        //    int startaddr = 0;
        //    if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        engpage = DutStruct[j].EngPage;
        //        startaddr = DutStruct[j].StartAddress;

        //    }
        //    else
        //    {
        //        logger.AdapterLogString(4, "there is no DEBUGINTERFACE");

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
        //        byte[] WriteModDacByteArray = algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);
        //        Engmod(engpage);
        //        WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteModDacByteArray);
        //        return true;
        //    }
        //    else
        //    {
        //        logger.AdapterLogString(4, "there is no MODDAC");
        //        return true;
        //    }
        //}
#endregion
        

  
        //read biasmoddac
    

        public override int ReadBiasDac()
        {
            int DacValue = 0;

            // ReadDAC("ModDac",out ModDac);

            if (ReadDAC("BIASDac", out DacValue))
            {
                return DacValue;
            }
            else
            {
                logger.AdapterLogString(4, "there is no ModDac");

                return DacValue;
            }
        }

        public override int ReadModDac()
        {
            int ModDac=0;

            if (ReadDAC("ModDac", out ModDac))
            {
                return ModDac;
            }
            else
            {
                logger.AdapterLogString(4, "there is no ModDac");

                return ModDac;
            }
        }

 
        #endregion


        #region set coef


        #region  TempDmi

        public override bool SetTempcoefb(string tempcoefb)
        {
            int i = 0;
            bool flag = false;
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

        public override bool SetTempcoefb(string tempcoefb, byte TempSelect)
        {
            return SetTempcoefb(tempcoefb);
        }
        public override bool ReadTempcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;
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
        public override bool ReadTempcoefb(out string strcoef, byte TempSelect)
        {
            return ReadTempcoefb(out strcoef);
        }
        public override bool SetTempcoefc(string tempcoefc)
        {
            int i = 0;
            bool flag = false;
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
        public override bool SetTempcoefc(string tempcoefc, byte TempSelect)
        {
            return SetTempcoefc(tempcoefc);
        }
        public override bool ReadTempcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;
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
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        { 
            return ReadTempcoefc(out strcoef);
        }

        #endregion

        #region  Vcc

        public override bool SetVcccoefb(string vcccoefb)
        {
            int i = 0;
            bool flag = false;
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
        public override bool SetVcccoefb(string vcccoefb, byte vccselect)
        {
            return SetVcccoefb(vcccoefb);
        }
        public override bool ReadVcccoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;
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
        public override bool ReadVcccoefb(out string strcoef, byte vccselect)
        { 
        return ReadVcccoefb(out strcoef);
        }
        public override bool SetVcccoefc(string vcccoefc)
        {
            int i = 0;
            bool flag = false;
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
        public override bool SetVcccoefc(string vcccoefc, byte vccselect)
        {
            return SetVcccoefc(vcccoefc);
        }
        public override bool ReadVcccoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;
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
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        { 
        return  ReadVcccoefc(out strcoef);
        }

       #endregion

        #region  RxPowerDmi

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

        #endregion

        #region  TxPowerDmi

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

     #endregion

        #region  TxPower

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


     #endregion

        #region  TxMod

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

   #endregion    
     

        #region  Mask

        public override bool SetMaskcoefa(float coefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TxMaskCoefA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, coefa.ToString(), DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET coefA To" + coefa);
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
                logger.AdapterLogString(4, "there is no TxMaskCoefA");

                return true;
            }
        }
        public override bool SetMaskcoefb(float coefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TxMaskCoefb"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, coefb.ToString(), DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET coefb To" + coefb);
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
                logger.AdapterLogString(4, "there is no TxMaskCoefb");

                return true;
            }
        }
        public override bool SetMaskcoefc(float coefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TxMaskCoefc"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, coefc.ToString(), DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET coefC To" + coefc);
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
                logger.AdapterLogString(4, "there is no TxMaskCoefC");

                return true;
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
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "REFERENCETEMPERATURECOEF", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, referencetempcoef, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET REFERENCETEMPERATURECOEF" + referencetempcoef);
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
                logger.AdapterLogString(4, "there is no REFERENCETEMPERATURECOEF");
                return true;
            }
        }


        public override bool SetTxpProportionLessCoef(string Lesscoef)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXPPROPORTIONLESSCOEF"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, Lesscoef, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXPPROPORTIONLESSCOEF" + Lesscoef);
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
                logger.AdapterLogString(4, "there is no TXPPROPORTIONLESSCOEF");

                return true;
            }
        }

        public override bool SetTxpProportionGreatCoef(string Greatcoef)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXPPROPORTIONGREATCOEF"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, Greatcoef, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXPPROPORTIONGREATCOEF" + Greatcoef);
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
                logger.AdapterLogString(4, "there is no TXPPROPORTIONGREATCOEF");

                return true;
            }
        }

        public override bool SetTxpFitsCoefa(string txpfitscoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXPFITSCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txpfitscoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXPFITSCOEFA" + txpfitscoefa);
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
                logger.AdapterLogString(4, "there is no TXPFITSCOEFA");

                return true;
            }
        }
        public override bool SetTxpFitsCoefb(string txpfitscoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXPFITSCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txpfitscoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXPFITSCOEFB" + txpfitscoefb);
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
                logger.AdapterLogString(4, "there is no TXPFITSCOEFB");

                return true;
            }
        }
        public override bool SetTxpFitsCoefc(string txpfitscoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXPFITSCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, txpfitscoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXPFITSCOEFC" + txpfitscoefc);
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
                logger.AdapterLogString(4, "there is no TXPFITSCOEFC");

                return true;
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
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "SETPOINT"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET SETPOINT" + setpoint);
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
                logger.AdapterLogString(4, "there is no SETPOINT");

                return true;
            }
        }
        public override bool SetcoefP(string coefp)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "COEFP"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, coefp, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET COEFP" + coefp);
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
                logger.AdapterLogString(4, "there is no COEFP");

                return true;
            }
        }
        public override bool SetcoefI(string coefi)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "COEFI"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, coefi, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET COEFI" + coefi);
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
                logger.AdapterLogString(4, "there is no COEFI");

                return true;
            }
        }
        public override bool SetcoefD(string coefd)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "COEFD"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET COEFD" + coefd);
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
                logger.AdapterLogString(4, "there is no COEFD");

                return true;
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
                    USBIO.WrtieReg(deviceIndex, 0xA2, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
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
            buff[0] = 0xff;
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
                    USBIO.WrtieReg(deviceIndex, 0xA2, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
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
                    buff = USBIO.ReadReg(deviceIndex, 0xA2, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

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
        public override bool APCCloseOpen(bool apcswitch) { return false; }
        #endregion
        #region close loop
        public override bool SetCloseLoopcoefa(string CloseLoopcoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "CLOSELOOPCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, CloseLoopcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET CLOSELOOPCOEFA To" + CloseLoopcoefa);
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
                logger.AdapterLogString(4, "there is no CLOSELOOPCOEFA");

                return true;
            }
        }
        public override bool SetCloseLoopcoefb(string CloseLoopcoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "CLOSELOOPCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, CloseLoopcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET CLOSELOOPCOEFB To" + CloseLoopcoefb);
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
                logger.AdapterLogString(4, "there is no CLOSELOOPCOEFB");

                return true;
            }
        }
        public override bool SetCloseLoopcoefc(string CloseLoopcoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "CLOSELOOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, CloseLoopcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET CLOSELOOPCOEFC To" + CloseLoopcoefc);
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
                logger.AdapterLogString(4, "there is no CLOSELOOPCOEFC");

                return true;
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
        public override bool SetRXPadcThreshold(byte threshold)
        {
            int i = 0;
            byte[] adcthreshold = new byte[1];
            if (FindFiledNameChannel(out i, "RXADCTHRESHOLD"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    adcthreshold[0] = threshold;
                    WriteReg(deviceIndex, 0xa2, DutStruct[i].StartAddress, adcthreshold);
                    //flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET RXADCTHRESHOLD" + threshold);
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
                logger.AdapterLogString(4, "there is no RXADCTHRESHOLD");

                return true;
            }
        }
        #endregion
        
        public override bool ReadALLcoef(out DutCoefValueStuct[] DutList)
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
                        strcoef = sfp.ReadALLCoef(deviceIndex, 0xA0, DutStruct[j].StartAddress, DutStruct[j].Format);
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
                logger.AdapterLogString(4, "there is no coefficent data");
                return true;

            }
        }

        public override bool ReadALLEEprom(out DutEEPROMInitializeStuct[] DutList)
        {
            string strcoef = "";
            int coefcount = 0;
            DutList = new DutEEPROMInitializeStuct[EEpromInitStruct.Length];
            if (EEpromInitStruct.Length > 0)
            {
                for (int j = 0; j < EEpromInitStruct.Length; j++)
                {
                    Engmod(EEpromInitStruct[j].Page);
                    strcoef = sfp.ReadALLEEprom(deviceIndex, EEpromInitStruct[j].SlaveAddress, EEpromInitStruct[j].StartAddress);
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
                logger.AdapterLogString(4, "there is no eeprom data");
                return true;

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

                logger.AdapterLogString(1, "EvbVadc= " + ArrRead[0] + "," + ArrRead[1]);

                if (EvbVolTageCoef_b == 0 && EvbVolTageCoef_c == 0)
                {


                    if (!ReadEvbVoltage_Coef())
                    {
                        logger.AdapterLogString(3, "EvbCoef 读取失败");
                        // return 0;
                    }
                    else
                    {
                        EvbVcc = (ArrRead[1] * 256 + ArrRead[0]) * EvbVolTageCoef_b + EvbVolTageCoef_c;
                        logger.AdapterLogString(1, "EvbVolTageCoef_b=" + EvbVolTageCoef_b + " EvbVolTageCoef_c=" + EvbVolTageCoef_c);
                        // logger.AdapterLogString(1, "EvbCoef 读取失败");
                    }

                }
                else
                {
                    EvbVcc = (ArrRead[1] * 256 + ArrRead[0]) * EvbVolTageCoef_b + EvbVolTageCoef_c;
                    logger.AdapterLogString(1, "EvbVolTageCoef_b=" + EvbVolTageCoef_b + " EvbVolTageCoef_c=" + EvbVolTageCoef_c);

                }
                logger.FlushLogBuffer();

                if (EvbVcc > 2)
                {
                    break;
                }

            }
            return EvbVcc;

        }

        private bool ReadEvbVoltage_Coef()
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

            logger.AdapterLogString(1, "EvbVolTageCoef_b=" + EvbVolTageCoef_b + StrMess);


            StrMess = "";
            for (int i = 0; i < 4; i++)
            {
                StrMess += " OX" + Convert.ToString(ArrRead[i + 4], 16).ToUpper();
            }

            logger.AdapterLogString(1, "EvbVolTageCoef_c=" + EvbVolTageCoef_c + StrMess);
            logger.FlushLogBuffer();

            return true;
        }
        public override bool CheckEvbVcADC_Coef_flag()
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
        public override bool SetdmiRxOffset(string dmiRxOffset) { return false; }
        public override bool SetTxEQcoefa(string TxEQcoefa) 
        {
            bool flag = false;
            int i = 0;
            if (FindFiledNameChannel(out i, "TXEQCOEFA"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, TxEQcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXEQCOEFA To" + TxEQcoefa);
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
                logger.AdapterLogString(4, "there is no TXEQCOEFA");

                return true;
            }
        }
        public override bool SetTxEQcoefb(string TxEQcoefb) 
        {
            bool flag = false;
            int i = 0;
            if (FindFiledNameChannel(out i, "TXEQCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, TxEQcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXEQCOEFB To" + TxEQcoefb);
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
                logger.AdapterLogString(4, "there is no TXEQCOEFB");

                return true;
            }
        }
        public override bool SetTxEQcoefc(string TxEQcoefc) 
        {
            bool flag = false;
            int i = 0;
            if (FindFiledNameChannel(out i, "TXEQCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = sfp.SetCoef(deviceIndex, 0xA2, DutStruct[i].StartAddress, TxEQcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET TXEQCOEFC To" + TxEQcoefc);
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
                logger.AdapterLogString(4, "there is no TXEQCOEFC");

                return true;
            }
        }

        #endregion

        public override bool GetRegistValueLimmit(byte ItemType, out int Max)
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


        #region  Operate Drvier Regist

         private  bool WriteDac(string StrItem, object DAC)
        {
            int ReadDacValue = 0;

            int i = 0;

            // string StrItem = "BiasDac";

            if (FindFiledNameChannelDAC(out i, StrItem))
            {

                bool flag = algorithm.BitNeedManage(DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);

                if (!flag)//寄存器全位,不需要做任何处理
                {
                    for (int k = 0; k < 3; k++)
                    {

                        if (!Write_Store_DriverRegist(StrItem, DAC,Writer_Store.Writer)) return false;//写DAC值

                        if (!ReadDAC("BiasDac", out ReadDacValue)) return false;//读DAC值

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

                        int JoinValue = algorithm.WriteJointBitValue((Int32)DAC, (Int32)TempReadValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理

                        if (!Write_Store_DriverRegist(StrItem, JoinValue,Writer_Store.Writer)) return false;//写入寄存器的全位DAC值

                        if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读取寄存器的全位DAC值

                        int ReadJoinValue = algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                        if (ReadJoinValue == Convert.ToInt16(DAC))
                        {
                            return true;
                        }
                    }

                }
                logger.AdapterLogString(3, "Writer " + StrItem + " Error");
                return false;
            }
            else
            {
                logger.AdapterLogString(4, "Writer " + StrItem + " Error");
                return false;
            }
        }

         private bool StoreDac(string StrItem, object DAC)
         {
             int ReadDacValue = 0;

             int i = 0;

             // string StrItem = "BiasDac";

             if (FindFiledNameChannelDAC(out i, StrItem))
             {

                 bool flag = algorithm.BitNeedManage(DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);

                 if (!flag)//寄存器全位,不需要做任何处理
                 {
                     for (int k = 0; k < 3; k++)
                     {

                         if (!Write_Store_DriverRegist(StrItem, DAC, Writer_Store.Store)) return false;//存DAC值

                         if (!ReadDAC("BiasDac", out ReadDacValue)) return false;//读DAC值

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

                         int JoinValue = algorithm.WriteJointBitValue((Int32)DAC, (Int32)TempReadValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理

                         if (!Write_Store_DriverRegist(StrItem, JoinValue, Writer_Store.Store)) return false;//存入寄存器的全位DAC值

                         if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读取寄存器的全位DAC值

                         int ReadJoinValue = algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                         if (ReadJoinValue == Convert.ToInt16(DAC))
                         {
                             return true;
                         }
                     }

                 }
                 logger.AdapterLogString(3, "Writer " + StrItem + " Error");
                 return false;
             }
             else
             {
                 logger.AdapterLogString(4, "Writer " + StrItem + " Error");
                 return false;
             }
         }

         private bool Write_Store_DriverRegist(string StrItem, object DAC, Writer_Store operate)
         {
             byte chipset = 0x01;
             int j = 0;
             byte engpage = 0;
             int startaddr = 0;

             try
             {
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
                     byte[] WriteModDacByteArray = algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);
                     Engmod(engpage);

                     if (operate == 0)//写
                     {

                         WriteDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteModDacByteArray);
                     }
                     else//存
                     {

                         StoreDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, WriteModDacByteArray);

                     }
                     logger.AdapterLogString(3, "Writer " + StrItem + "Error");
                     return true;
                 }
                 else
                 {
                     logger.AdapterLogString(4, "Writer " + StrItem + " Error");
                     return false;
                 }
             }
             catch
             {
                 return false;

             }
         }

         private bool ReadDAC(string StrItem, out int ReadDAC)
         {
             byte chipset = 0x01;
             int j = 0;
             byte engpage = 0;
             int startaddr = 0;
             ReadDAC = 0;
             // int ReadDAC = 0;

             try
             {

                 if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                 {
                     engpage = DutStruct[j].EngPage;
                     startaddr = DutStruct[j].StartAddress;

                 }
                 else
                 {
                     logger.AdapterLogString(4, "there is no DEBUGINTERFACE");
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

                     byte[] DacArray = ReadDriver10g(deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, chipset, DriverStruct[i].Length);

                     for (j = DacArray.Length; j > 0; j--)
                     {
                         ReadDAC += Convert.ToUInt16(DacArray[j - 1] * Math.Pow(256, DriverStruct[i].Length - j));
                     }

                     return true;
                 }
                 else
                 {
                     logger.AdapterLogString(4, "Read " + StrItem + "Error");
                     return false;
                 }
             }
             catch
             {
                 return false;
             }
         }



        public override bool WriteBiasDac(object DAC)
        {
            return WriteDac("BiasDAC", DAC);
        }

        public override bool WriteModDac(object DAC)
        {
            return WriteDac("ModDAC", DAC);
        }

        public override bool WriteMaskDac(object DAC)
        {
            return WriteDac("MaskDAC", DAC);
            
        }

        public override bool StoreBiasDac(object biasdac)
        {
            return StoreDac("BiasDAC", biasdac);

        }

        public override bool StoreModDac(object moddac)
        {
            return StoreDac("MODDAC", moddac);
        }

        public override bool StoreMaskDac(object DAC)
        {
            return StoreDac("MaskDAC", DAC);
        }
      
     
        public override bool StoreLOSDac(object losdac)
        {
            return StoreDac("LOSDAC", losdac);
        }

        public override bool StoreLOSDDac(object losddac)
        {
            return StoreDac("LOSDDAC", losddac);
        }

        public override bool StoreAPDDac(object apddac)
        {
            return StoreDac("APDDAC", apddac);
        }
        public override bool StoreEA(object DAC)
        {
            return StoreDac("EADAC", DAC);
        }

        
        public override bool WriteLOSDac(object losdac)
        {
            return WriteDac("LOSDAC", losdac);
        }

        public override bool WriteLOSDDac(object losddac)
        {
             return WriteDac("LOSDDAC", losddac);
        }
        public override bool WriteAPDDac(object apddac)
        {
             return WriteDac("APDDAC", apddac);
           
        }
        public override bool StoreCrossDac(object crossdac)
        {
            return StoreDac("CROSSDAC", crossdac);

        }
        public override bool WriteEA(object DAC)
        {
            return WriteDac("EADAC", DAC);
        }
       
    
#endregion

    }
}
