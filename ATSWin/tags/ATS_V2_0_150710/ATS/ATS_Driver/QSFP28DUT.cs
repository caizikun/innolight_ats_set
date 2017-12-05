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
        EEPROM QSFP;
        public bool awstatus_flag = false;// aw status check
        public Algorithm algorithm = new Algorithm();
        public QSFP28DUT(logManager logmanager)
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
                string temp = AuxAttribles.ToUpper().Replace(" ","");
                ChipsetControll = temp.Contains("OLDDRIVER=1");
                QSFP = new EEPROM(deviceIndex, logger);
                if (!Connect()) return false;
                
            }

            catch (Error_Message error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;

            }
            return true;
        }
        public override bool Connect()
        {
            
            try
            {
                USBIO = new IOPort("USB", deviceIndex.ToString(), logger);
                USBIO.IOConnect();
                EquipmentConnectflag = true;
                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChangeChannel(string channel,int syn=0)
        {
            return true;
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
            byte[] buff = new byte[5];
            buff[0] = 0xca;
            buff[1] = 0x2d;
            buff[2] = 0x81;
            buff[3] = 0x5f;
            buff[4] = engpage;
            USBIO.WrtieReg(deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        }
        public override void Engmod()
        {
            byte[] buff = new byte[4];
            buff[0] = 0xca;
            buff[1] = 0x2d;
            buff[2] = 0x81;
            buff[3] = 0x5f;
            USBIO.WrtieReg(deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        }

        #region dmi
        public override double ReadDmiTemp()
        {
            return QSFP.readdmitemp(deviceIndex,0xA0, 22);
        }
        public override double ReadDmiVcc()
        {

            return QSFP.readdmivcc(deviceIndex, 0xA0, 26);
            
        }
        public override double ReadDmiBias()
        {
            double dmibias = 0.0;
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        dmibias = QSFP.readdmibias(deviceIndex, 0xA0, 42);
                        break;
                    case 2:
                        dmibias = QSFP.readdmibias(deviceIndex, 0xA0, 44);
                        break;
                    case 3:
                        dmibias = QSFP.readdmibias(deviceIndex, 0xA0, 46);
                        break;
                    case 4:
                        dmibias = QSFP.readdmibias(deviceIndex, 0xA0, 48);
                        break;
                    default:
                        break;
                }
                return dmibias;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return dmibias;

            }

            
        }
        public override double ReadDmiTxp()
        {
            double dmitxp =0.0;
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        dmitxp = QSFP.readdmitxp(deviceIndex, 0xA0, 50);
                        break;
                    case 2:
                        dmitxp = QSFP.readdmitxp(deviceIndex, 0xA0, 52);
                        break;
                    case 3:
                        dmitxp = QSFP.readdmitxp(deviceIndex, 0xA0, 54);
                        break;
                    case 4:
                        dmitxp = QSFP.readdmitxp(deviceIndex, 0xA0, 56);
                        break;
                    default:
                        break;
                }
                return dmitxp;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return dmitxp;

            }


        }
        public override double ReadDmiRxp()
        {
            double dmirxp = 0.0;
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        dmirxp = QSFP.readdmirxp(deviceIndex, 0xA0, 34);
                        break;
                    case 2:
                        dmirxp = QSFP.readdmirxp(deviceIndex, 0xA0, 36);
                        break;
                    case 3:
                        dmirxp = QSFP.readdmirxp(deviceIndex, 0xA0, 38);
                        break;
                    case 4:
                        dmirxp = QSFP.readdmirxp(deviceIndex, 0xA0, 40);
                        break;
                    default:
                        break;
                }
                return dmirxp;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return dmirxp;

            }


        }
        #endregion
        #region read a/w
        public override double ReadTempHA()
        {
            double tempha = 0.0;
            Engmod(3);
            tempha = QSFP.readtempaw(deviceIndex, 0xA0, 128);
            return tempha;
        }
        public override double ReadTempLA()
        {
            double templa;
            Engmod(3);
            templa = QSFP.readtempaw(deviceIndex, 0xA0, 130);
            return templa;
        }
        public override double ReadTempLW()
        {
            double templw = 0.0;
            Engmod(3);
            templw = QSFP.readtempaw(deviceIndex, 0xA0, 134);
            return templw;

        }
        public override double ReadTempHW()
        {
            double temphw = 0.0;
            Engmod(3);
            temphw = QSFP.readtempaw(deviceIndex, 0xA0, 132);
            return temphw;
        }
        public override double ReadVccLA()
        {
            double vccla = 0.0;
            Engmod(3);
            vccla = QSFP.readvccaw(deviceIndex, 0xA0, 146);
            return vccla;

        }
        public override double ReadVccHA()
        {
            double vccha = 0.0;
            Engmod(3);
            vccha = QSFP.readvccaw(deviceIndex, 0xA0, 144);
            return vccha;

        }
        public override double ReadVccLW()
        {
            double vcclw = 0.0;
            Engmod(3);
            vcclw = QSFP.readvccaw(deviceIndex, 0xA0, 150);
            return vcclw;

        }
        public override double ReadVccHW()
        {
            double vcchw = 0.0;
            Engmod(3);
            vcchw = QSFP.readvccaw(deviceIndex, 0xA0, 148);
            return vcchw;

        }
        public override double ReadBiasLA()
        {
            double biasla = 0.0;
            Engmod(3);
            biasla = QSFP.readbiasaw(deviceIndex, 0xA0, 186);
            return biasla;

        }
        public override double ReadBiasHA()
        {
            double biasha = 0.0;
            Engmod(3);
            biasha = QSFP.readbiasaw(deviceIndex, 0xA0, 184);
            return biasha;

        }
        public override double ReadBiasLW()
        {
            double biaslw = 0.0;
            Engmod(3);
            biaslw = QSFP.readbiasaw(deviceIndex, 0xA0, 190);
            return biaslw;

        }
        public override double ReadBiasHW()
        {
            double biashw = 0.0;
            Engmod(3);
            biashw = QSFP.readbiasaw(deviceIndex, 0xA0, 188);
            return biashw;

        }
        public override double ReadTxpLA()
        {
            double txpla = 0.0;
            Engmod(3);
            txpla = QSFP.readtxpaw(deviceIndex, 0xA0, 194);
            return txpla;

        }
        public override double ReadTxpLW()
        {
            double txplw = 0.0;
           Engmod(3);
           txplw = QSFP.readtxpaw(deviceIndex, 0xA0, 198);
            return txplw;

        }
        public override double ReadTxpHA()
        {
            double txpha = 0.0;
            Engmod(3);
            txpha = QSFP.readtxpaw(deviceIndex, 0xA0, 192);
            return txpha;

        }
        public override double ReadTxpHW()
        {
            double txphw = 0.0;
            Engmod(3);
            txphw = QSFP.readtxpaw(deviceIndex, 0xA0, 196);
            return txphw;

        }
        public override double ReadRxpLA()
        {
            double rxpla = 0.0;
            Engmod(3);
            rxpla = QSFP.readrxpaw(deviceIndex, 0xA0, 178);
            return rxpla;

        }
        public override double ReadRxpLW()
        {
            double rxplw = 0.0;
            Engmod(3);
            rxplw = QSFP.readrxpaw(deviceIndex, 0xA0, 182);
            return rxplw;

        }
        public override double ReadRxpHA()
        {
            double rxpha = 0.0;
            Engmod(3);
            rxpha = QSFP.readrxpaw(deviceIndex, 0xA0, 176);
            return rxpha;

        }
        public override double ReadRxpHW()
        {
            double rxphw = 0.0;
            Engmod(3);
            rxphw = QSFP.readrxpaw(deviceIndex, 0xA0, 180);
            return rxphw;

        }
        #endregion
        #region check a/w
        public override bool ChkTempHA()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTempLA()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkVccHA()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkVccLA()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkBiasHA()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        if ((buff[0] & 0x80) != 0)
                        {
                            awstatus_flag= true;
                        }

                        else
                        {
                            awstatus_flag= false;
                        }
                        break;
                    case 2:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkBiasLA()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTxpHA()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTxpLA()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkRxpHA()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkRxpLA()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTempHW()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTempLW()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 6, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3,error.ToString());
                return false;

            }
        }
        public override bool ChkVccHW()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkVccLW()
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, 0xA0, 7, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkBiasHW()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkBiasLW()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 11, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 12, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkTxpHW()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkTxpLW()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 13, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 14, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkRxpHW()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkRxpLW()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 9, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 10, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        #endregion
        #region read optional status /control bit
        public override bool ChkTxDis()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkTxFault()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 4, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkRxLos()
        {
            byte[] buff = new byte[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        #endregion
        #region set a/w
        public override void SetTempLA(decimal templa)
        {
           Engmod(3);
           QSFP.settempaw(deviceIndex, 0xA0, 130, templa);
        }
        public override void SetTempHA(decimal tempha)
        {
            Engmod(3);
            QSFP.settempaw(deviceIndex, 0xA0, 128, tempha);
        }
        public override void SetTempLW(decimal templw)
        {
            Engmod(3);
            QSFP.settempaw(deviceIndex, 0xA0, 134, templw);
        }
        public override void SetTempHW(decimal temphw)
        {
            Engmod(3);
            QSFP.settempaw(deviceIndex, 0xA0, 132, temphw);
        }
        public override void SetVccHW(decimal vcchw)
        {
            Engmod(3);
            QSFP.setvccaw(deviceIndex, 0xA0, 148, vcchw);
        }
        public override void SetVccLW(decimal vcclw)
        {
            Engmod(3);
            QSFP.setvccaw(deviceIndex, 0xA0, 150, vcclw);
        }
        public override void SetVccLA(decimal vccla)
        {
            Engmod(3);
            QSFP.setvccaw(deviceIndex, 0xA0, 146, vccla);
        }
        public override void SetVccHA(decimal vccha)
        {
            Engmod(3);
            QSFP.setvccaw(deviceIndex, 0xA2, 144, vccha);
        }
        public override void SetBiasLA(decimal biasla)
        {
            Engmod(3);
            QSFP.setbiasaw(deviceIndex, 0xA0, 186, biasla);
        }
        public override void SetBiasHA(decimal biasha)
        {
            Engmod(3);
            QSFP.setbiasaw(deviceIndex, 0xA0, 184, biasha);
        }
        public override void SetBiasHW(decimal biashw)
        {
            Engmod(3);
            QSFP.setbiasaw(deviceIndex, 0xA0, 188, biashw);
        }
        public override void SetBiasLW(decimal biaslw)
        {
            Engmod(3);
            QSFP.setbiasaw(deviceIndex, 0xA0, 190, biaslw);
        }
        public override void SetTxpLW(decimal txplw)
        {
            Engmod(3);
            QSFP.settxpaw(deviceIndex, 0xA0, 198, txplw);
        }
        public override void SetTxpHW(decimal txphw)
        {
            Engmod(3);
            QSFP.settxpaw(deviceIndex, 0xA0, 196, txphw);
        }
        public override void SetTxpLA(decimal txpla)
        {
            Engmod(3);
            QSFP.settxpaw(deviceIndex, 0xA0, 194, txpla);
        }
        public override void SetTxpHA(decimal txpha)
        {
            Engmod(3);
            QSFP.settxpaw(deviceIndex, 0xA0, 192, txpha);
        }
        public override void SetRxpHA(decimal rxpha)
        {
           Engmod(3);
           QSFP.setrxpaw(deviceIndex, 0xA0, 176, rxpha);
        }
        public override void SetRxpLA(decimal rxpla)
        {
            Engmod(3);
            QSFP.setrxpaw(deviceIndex, 0xA0, 178, rxpla);
        }
        public override void SetRxpHW(decimal rxphw)
        {
            Engmod(3);
            QSFP.setrxpaw(deviceIndex, 0xA0, 180, rxphw);
        }
        public override void SetRxpLW(decimal rxplw)
        {
            Engmod(3);
            QSFP.setrxpaw(deviceIndex, 0xA0, 182, rxplw);
        }

        public override void SetSoftTxDis()
        {
            byte[] buff = new byte[1];
            try
            {


                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        buff[0] = (byte)(buff[0] | 0x01);
                        USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        break;
                    case 2:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        buff[0] = (byte)(buff[0] | 0x02);
                        USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        break;
                    case 3:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        buff[0] = (byte)(buff[0] | 0x04);
                        USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        break;
                    case 4:
                        buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                        buff[0] = (byte)(buff[0] | 0x08);
                        USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());

            }
        }
        #endregion
        #region w/r  sn/pn
        public override string ReadSn()
        {
            string sn="";
            Engmod(0);
            sn = QSFP.ReadSn(deviceIndex, 0xA0, 196);
            return sn;

        }
        public override string ReadPn()
        {
            string pn = "";
           Engmod(0);
           pn = QSFP.ReadPn(deviceIndex, 0xA0, 168);
            return pn;

        }
        public override void SetSn(string sn)
        {
            Engmod(0);
            QSFP.SetSn(deviceIndex, 0xA0, 196, sn);
            //System.Threading.Thread.Sleep(1000);

        }
        public override void SetPn(string pn)
        {
            Engmod(0);
            QSFP.SetPn(deviceIndex, 0xA0, 168, pn);
            //System.Threading.Thread.Sleep(1000);

        }
        #endregion
        //read manufacture data
        #region fwrev
        public override string ReadFWRev()
        {
            string fwrev = "";
            Engmod(4);
            fwrev = QSFP.ReadFWRev(deviceIndex, 0xA0, 128);
            return fwrev;
        }
        #endregion
        #region  adc
        public override bool ReadTempADC( out UInt16 tempadc,byte tempselect)
        {
            tempadc = 0;
            int i=0;
            switch (tempselect)
            {
                case 1:
                    if (algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            tempadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "Current TEMPERATUREADC is" + tempadc);
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

                case 2:
                    if (algorithm.FindFileName(DutStruct, "TEMPERATURE2ADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            tempadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "Current TEMPERATURE2ADC is" + tempadc);
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
                        logger.AdapterLogString(4, "there is no TEMPERATURE2ADC");
                        return true;
                    }

                

                default:
                    if (algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            tempadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "Current TEMPERATUREADC is" + tempadc);
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

        }
        public override bool ReadTempADC(out UInt16 tempadc)
        {
            tempadc = 0;
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    tempadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "Current TEMPERATUREADC is" + tempadc);
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
        public override bool ReadVccADC(out UInt16 vccadc, byte vccselect)
        {//vccselect 1 VCC1ADC
            vccadc = 0;
            int i = 0;
            switch (vccselect)
            {
                case 1:
                    if (algorithm.FindFileName(DutStruct, "VCCADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "Current VCCADC is" + vccadc);
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

                case 2:
                    if (algorithm.FindFileName(DutStruct, "VCC2ADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "Current VCC2ADC is" + vccadc);
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
                        logger.AdapterLogString(4, "there is no VCC2ADC");
                        return true;
                    }

                case 3:
                    if (algorithm.FindFileName(DutStruct, "VCC3ADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "Current VCC3ADC is" + vccadc);
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
                        logger.AdapterLogString(4, "there is no VCC3ADC");
                        return true;
                    }

                default:
                    if (algorithm.FindFileName(DutStruct, "VCCADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "Current VCCADC is" + vccadc);
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


        }
        public override bool ReadVccADC(out UInt16 vccadc)
        {
            vccadc = 0;
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "VCCADC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "Current VCCADC is" + vccadc);
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
                    biasadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "Current TXBIASADC is" + biasadc);
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
                    txpadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "Current TXPOWERADC is " + txpadc);
                   
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
        public override bool ReadRxpADC( out UInt16 rxpadc)
        {
            rxpadc = 0;
            int i = 0;
           
            if (FindFiledNameChannel(out i, "RXPOWERADC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    rxpadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "Current RXPOWERADC is" + rxpadc);
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
        public byte[] WriteDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, byte[] dataToWrite, bool Switch)
        {
            return QSFP.ReadWriteDriverQSFP(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x02, chipset, dataToWrite, Switch);
        }
        public byte[] ReadDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, int readLength, bool Switch)
        {
            return QSFP.ReadWriteDriverQSFP(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x01, chipset, new byte[readLength], Switch);
        }
        public byte[] StoreDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, byte[] dataToWrite, bool Switch)
        {
            return QSFP.ReadWriteDriverQSFP(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x04, chipset, dataToWrite, Switch);
        }
        //driver innitialize
        public override bool DriverInitialize()
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac
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
                                chipset = 0x08;
                                break;
                            default:
                                chipset = 0x01;
                                break;

                        }
                        Engmod(engpage);
                        for (k = 0; k < 3; k++)
                        {
                            WriteDriver40g(deviceIndex, 0xA0, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                            // Thread.Sleep(200);  
                            StoreDriver40g(deviceIndex, 0xA0, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                            // Thread.Sleep(200);  
                            temp = new byte[DriverInitStruct[i].Length];
                            temp = ReadDriver40g(deviceIndex, 0xA0, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, DriverInitStruct[i].Length, ChipsetControll);

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
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
            byte chipset = 0x01;
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
                WriteDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CROSSDAC");
                return true;
            }

        }
        public override bool WriteBiasDac(object biasdac)
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac
             int j = 0;
             byte engpage=0;
             int startaddr=0;
             byte chipset = 0x01;
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
                 WriteDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
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
                WriteDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteModDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
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
                byte[] LOSDacByteArray = algorithm.ObjectTOByteArray(losdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
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
                WriteDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, LOSDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
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
                byte[] LOSDacByteArray = algorithm.ObjectTOByteArray(losddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
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
                WriteDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, LOSDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
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
                byte[] APDDacByteArray = algorithm.ObjectTOByteArray(apddac, DriverStruct[i].Length, DriverStruct[i].Endianness);
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
                WriteDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, APDDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
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
                byte[] StoreBiasDacByteArray = algorithm.ObjectTOByteArray(crossdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                StoreDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, StoreBiasDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
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
                byte[] StoreBiasDacByteArray = algorithm.ObjectTOByteArray(biasdac, DriverStruct[i].Length, DriverStruct[i].Endianness);
                StoreDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, StoreBiasDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
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
                StoreDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, StoreModDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
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
                StoreDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, StoreLOSDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
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
                StoreDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, StoreLOSDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
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
                StoreDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, StoreAPDDacByteArray, ChipsetControll);
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
            byte chipset = 0x01;
            crossdac = new byte[length];
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
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
                crossdac = ReadDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, length, ChipsetControll);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CROSSDAC");
                return true;
            }
        }
        public override bool ReadBiasDac(int length, out byte[] BiasDac)
        {
            byte chipset = 0x01;
            BiasDac = new byte[length];
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
                BiasDac = ReadDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, length, ChipsetControll);
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
            byte chipset = 0x01;
            ModDac = new byte[length];
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
                ModDac = ReadDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, length, ChipsetControll);
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
            byte chipset = 0x01;
            LOSDac = new byte[length];
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
                LOSDac = ReadDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, length, ChipsetControll);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDAC");
                return true;
            }
        }
        public override bool ReadLOSDDac(int length, out byte[] LOSDDac)
        {
            byte chipset = 0x01;
            LOSDDac = new byte[length];
            int j = 0;
            byte engpage = 0;
            int startaddr = 0;
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
                LOSDDac = ReadDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, length, ChipsetControll);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no LOSDDAC");
                return true;
            }
        }
        public override bool ReadAPDDac(int length, out byte[] APDDac)
        {
            byte chipset = 0x01;
            APDDac = new byte[length];
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
                APDDac = ReadDriver40g(deviceIndex, 0xA0, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, length, ChipsetControll);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no APDDAC");
                return true;
            }
        }
        #endregion
        #region set coef
        public override bool SetTempcoefb(string tempcoefb, byte TempSelect) 
        {
            bool flag = false;
            int i = 0;
            switch (TempSelect)
            {
                case 1:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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
                case 2:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMP2COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DMITEMP2COEFB To" + tempcoefb);
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
                        logger.AdapterLogString(4, "there is no DMITEMP2COEFB");
                        return true;
                    }
                default:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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
            
        }
        public override bool SetTempcoefb(string tempcoefb)
        {
            bool flag = false;
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
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
        public override bool ReadTempcoefb(out string strcoef, byte TempSelect)
        {
            strcoef = "";
            int i = 0;
            switch (TempSelect)
            {
                case 1:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                case 2:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMP2COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        logger.AdapterLogString(4, "there is no DMITEMP2COEFB");
                        return true;
                    }
                default:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetTempcoefc(string tempcoefc, byte TempSelect) 
        {
            int i = 0;
            bool flag = false;
            switch (TempSelect)
            {
                case 1:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
                case 2:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMP2COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DMITEMP2COEFC To" + tempcoefc);
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
                        logger.AdapterLogString(4, "there is no DMITEMP2COEFC");
                        return true;
                    }
                default:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
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
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        {
            strcoef = "";
            int i = 0;
            switch (TempSelect)
            {
                case 1:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                case 2:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMP2COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        logger.AdapterLogString(4, "there is no DMITEMP2COEFC");
                        return true;
                    }
                default:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetVcccoefb(string vcccoefb, byte vccselect)
        {
            int i = 0;
            bool flag = false;
            switch (vccselect)
            {
                case 1:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
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
                case 2:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC2COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DMIVCC2COEFB To" + vcccoefb);
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
                        logger.AdapterLogString(4, "there is no DMIVCC2COEFB");
                        return true;
                    }
                case 3:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC3COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DMIVCC3COEFB To" + vcccoefb);
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
                        logger.AdapterLogString(4, "there is no DMIVCC3COEFB");
                        return true;
                    }
                default:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
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
        }
        public override bool SetVcccoefb(string vcccoefb)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
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
        public override bool ReadVcccoefb(out string strcoef, byte vccselect)
        {
            strcoef = "";
            int i = 0;
            switch (vccselect)
            {
             case 1:
             //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
             case 2:
             //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC2COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        logger.AdapterLogString(4, "there is no DMIVCC2COEFB");
                        return true;
                    }
             case 3:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC3COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        logger.AdapterLogString(4, "there is no DMIVCC3COEFB");
                        return true;
                    }
                default:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
        public override bool SetVcccoefc(string vcccoefc, byte vccselect)
        {
            bool flag = false;
            int i = 0;
            switch (vccselect)
            { 
                case 1:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                            logger.AdapterLogString(0, "SET DMIVCCCOEFC To" + vcccoefc);
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
                case 2:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC2COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                            logger.AdapterLogString(0, "SET DMIVCC2COEFC To" + vcccoefc);
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
                        logger.AdapterLogString(4, "there is no DMIVCC2COEFC");
                        return true;
                    }
                case 3:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC3COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                            logger.AdapterLogString(0, "SET DMIVCC3COEFC To" + vcccoefc);
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
                        logger.AdapterLogString(4, "there is no DMIVCC3COEFC");
                        return true;
                    }
                default:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                            logger.AdapterLogString(0, "SET DMIVCCCOEFC To" + vcccoefc);
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
           
        }
        public override bool SetVcccoefc(string vcccoefc)
        {
            bool flag = false;
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                    logger.AdapterLogString(0, "SET DMIVCCCOEFC To" + vcccoefc);
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
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        {
            strcoef = "";
            int i = 0;
            switch (vccselect)
            {
                case 1:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                case 2:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC2COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        logger.AdapterLogString(4, "there is no DMIVCC2COEFC");
                        return true;
                    }
                case 3:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC3COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                        logger.AdapterLogString(4, "there is no DMIVCC3COEFC");
                        return true;
                    }
                default:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(0, "SET DMIRXPOWERCOEFA To" + rxcoefa);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefb, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxcoefc, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefa, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefb, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txslopcoefc, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefa, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefb, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txoffsetcoefc, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefa, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefb, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, biasdaccoefc, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefa, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefb, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, moddaccoefc, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefa, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TARGETLOPCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefb, DutStruct[i].Format);
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
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, targetlopcoefc, DutStruct[i].Format);
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
        public override bool ReadTargetLopcoefc( out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TARGETLOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    strcoef = QSFP.ReadCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, CloseLoopcoefa, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET CLOSELOOPCOEFA" + CloseLoopcoefa);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, CloseLoopcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET CLOSELOOPCOEFB" + CloseLoopcoefb);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, CloseLoopcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET CLOSELOOPCOEFC" + CloseLoopcoefc);
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
        #region PID
        public override bool SetPIDSetpoint(string setpoint)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "SETPOINT"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, coefp, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, coefi, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
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
        #region bias adc threshold
        public override bool SetBiasadcThreshold(byte threshold)
        {
            int i = 0;
            byte [] adcthreshold=new byte[1];
            if (FindFiledNameChannel(out i, "BIASADCTHRESHOLD"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    adcthreshold[0]=threshold;
                    WriteReg(deviceIndex, 0xa0, DutStruct[i].StartAddress, adcthreshold);
                    //flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, coefd, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET BIASADCTHRESHOLD" + threshold);
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
                logger.AdapterLogString(4, "there is no BIASADCTHRESHOLD");

                return true;
            }
        }
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
                    WriteReg(deviceIndex, 0xa0, DutStruct[i].StartAddress, adcthreshold);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, referencetempcoef, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, Lesscoef, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, Greatcoef, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txpfitscoefa, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txpfitscoefb, DutStruct[i].Format);
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
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, txpfitscoefc, DutStruct[i].Format);
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
        


        //new add as cgr4 new map
        public override bool SetRxAdCorSlopcoefb(string rxadcorslopcoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "RXADCORSLOPCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcorslopcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET RXADCORSLOPCOEFB" + rxadcorslopcoefb);
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
                logger.AdapterLogString(4, "there is no RXADCORSLOPCOEFB");

                return true;
            }
        }
        public override bool SetRxAdCorSlopcoefc(string rxadcorslopcoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "RXADCORSLOPCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcorslopcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET RXADCORSLOPCOEFC" + rxadcorslopcoefc);
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
                logger.AdapterLogString(4, "there is no RXADCORSLOPCOEFC");

                return true;
            }
        }
        public override bool SetRxAdCorOffscoefb(string rxadcoroffsetcoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "RXADCOROFFSCOEFB"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcoroffsetcoefb, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET RXADCOROFFSCOEFB" + rxadcoroffsetcoefb);
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
                logger.AdapterLogString(4, "there is no RXADCOROFFSCOEFB");

                return true;
            }
        }
        public override bool SetRxAdCorOffscoefc(string rxadcoroffsetcoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "RXADCOROFFSCOEFC"))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, rxadcoroffsetcoefc, DutStruct[i].Format);
                    logger.AdapterLogString(1, "SET RXADCOROFFSCOEFC" + rxadcoroffsetcoefc);
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
                logger.AdapterLogString(4, "there is no RXADCOROFFSCOEFC");

                return true;
            }
        }
        public override bool ReadRx2RawADC(out UInt16 rxrawadc)
        {
            rxrawadc = 0;
            int i = 0;

            if (algorithm.FindFileName(DutStruct, "RX2RAWADC",out i ))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    rxrawadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                    logger.AdapterLogString(1, "there is RX2RAWADC" + rxrawadc);

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
                logger.AdapterLogString(4, "there is no RX2RAWADC");
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
            if(DutList.Length>0)
            {
                for (int j = 0; j < DutStruct.Length; j++)
                {
                    if (DutStruct[j].CoefFlag == true)
                    {
                        Engmod(DutStruct[j].EngPage);
                        strcoef = QSFP.ReadALLCoef(deviceIndex, 0xA0, DutStruct[j].StartAddress, DutStruct[j].Format);
                        DutList[coefcount].Page = DutStruct[j].EngPage;
                        DutList[coefcount].StartAddress = DutStruct[j].StartAddress;
                        DutList[coefcount].Length = DutStruct[j].Length;
                        DutList[coefcount].CoefValue = "0x"+strcoef;
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
                        strcoef = QSFP.ReadALLEEprom(deviceIndex, EEpromInitStruct[j].SlaveAddress, EEpromInitStruct[j].StartAddress);
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
        public override double ReadEvbVcc()
        {
            double EvbVcc=0;
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

                if (EvbVcc>2)
                {
                    break;
                }

            }

            return EvbVcc;
                
        }

        private bool ReadEvbVoltage_Coef()
        {

            float[]Coef=new float[2];
        
            IOPort.OpenDevice(0);

            byte[] Arr = new byte[] { 0, 0, 178, 0, 0, 0, 8 };//byte4:Read=0/Write=1; Byte5:RegistAdd/256,Byte6:RegistAdd%256,Byte7=ReadLength

            IOPort.CH375WriteData(0, Arr);

            byte[] ArrRead = new byte[8];

            IOPort.CH375ReadData(0, ArrRead);

            IOPort.CloseDevice(0);

            for(int i=0;i<2;i++)
            {
                 byte[] Arr1= new byte[4];
                Arr1[0] = ArrRead[i * 4];
                Arr1[1] = ArrRead[i * 4+1];
                Arr1[2] = ArrRead[i * 4+2];
                Arr1[3] = ArrRead[i * 4+3];
               
                Coef[i] = BitConverter.ToSingle(Arr1, 0);
               // logger.AdapterLogString(1,)
            }
             
            EvbVolTageCoef_b = Coef[0];
            EvbVolTageCoef_c = Coef[1];

            string StrMess="";
            for(int i=0;i<4;i++)
            {
                StrMess+=" OX"+Convert.ToString( ArrRead[i],16).ToUpper();
            }
            
            logger.AdapterLogString(1, "EvbVolTageCoef_b=" + EvbVolTageCoef_b + StrMess);


             StrMess = "";
            for (int i = 0; i < 4; i++)
            {
                StrMess += " OX" + Convert.ToString(ArrRead[i+4], 16).ToUpper();
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
        private bool CDR_Enable()
        {
            byte[] dataToWrite = { 0XFF };
            byte[] dataReadArray;
            for (int i = 0; i < 3; i++)
            {
                USBIO.WrtieReg(deviceIndex, 0XA0, 98, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
                dataReadArray = USBIO.ReadReg(deviceIndex, 0XA0, 98, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

                if (dataReadArray[0] == 0xff)
                {
                    return true;
                }
            }
            return false;

        }
        private bool HightPowerClass_Enable()
        {
            byte[] dataToWrite = { 0X04 };
            byte[] dataReadArray;
            for (int i = 0; i < 3; i++)
            {
                USBIO.WrtieReg(deviceIndex, 0XA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, dataToWrite);
                dataReadArray = USBIO.ReadReg(deviceIndex, 0XA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if (dataReadArray[0] == 0x04)
                {
                    return true;
                }
            }
            return false;
        }
        public override bool FullFunctionEnable()
        {
            if (CDR_Enable() && HightPowerClass_Enable())
            {
                return true;
            }

            else
            {
                System.Windows.Forms.MessageBox.Show(" CDR ByPass Error");
                return false;
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
    }
}
