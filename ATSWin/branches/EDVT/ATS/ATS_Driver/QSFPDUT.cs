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
    public class QSFPDUT:DUT
    {
        EEPROM QSFP;
        public bool awstatus_flag = false;// aw status check
        public Algorithm algorithm = new Algorithm();
        public QSFPDUT(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList, string AuxAttribles)
        {
            try
            {
                DutStruct = DutList;
                DriverStruct = DriverList;
                string temp = AuxAttribles.ToUpper().Replace(" ","");
                ChipsetControll = temp.Contains("OLDDRIVER=1");
                
                QSFP = new EEPROM(deviceIndex);
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
                USBIO = new IOPort("USB", deviceIndex.ToString());
                EquipmentConnectflag = true;
                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChangeChannel(string channel)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset)
        {
            return true;
        }
        public override bool Configure()
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
        //dmi
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
        //a/w
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
        //check a/w
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
        //read optional status /control bit
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
        //set a/w
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
        //EDVT
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
        //public override void SetSoftTxDis(byte channel)
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {


        //        switch (channel)
        //        {
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x01);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x02);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x04);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x08);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override void SetSoftTxEnable()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {


        //        switch (MoudleChannel)
        //        {
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xfe);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xfd);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xfb);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xf7);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override bool ReadLatch()
        //{
        //    bool flag = true;
        //    byte[] buff = new byte[16];
        //    try
        //    {
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 3, IOPort.SoftHard.HARDWARE_SEQUENT, 16);
        //                for (int i = 0; i < 16; i++)
        //                {
        //                    if (buff[i] != 0x00)
        //                        flag = false;
                           
        //                }
        //                return flag;
        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());
        //        return false;
                

        //    }
        //}
        //public override void MaskTxpOn()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {
        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 246, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0xf0);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 246, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x0f);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 247, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0xf0);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 247, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x0f);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override void MaskTxpOff()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {
        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 246, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0x0f);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 246, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xf0);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 247, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0x0f);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 247, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xf0);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override bool PowerSet(bool onoff)
        //{
        //    byte[] buff = new byte[1];
        //        try
        //        {
        //            if (onoff)
        //            {
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x02);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //            }
        //            else
        //            {
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xfd);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //            }
        //            return true;
        //        }
        //        catch (Exception error)
        //        {
        //            logger.AdapterLogString(3, error.ToString());
        //            return false;
        //        }
         
        //}
        //public override bool PowerOverride(bool onoff)
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {
        //        if (onoff)
        //        {
        //            buff = USBIO.ReadReg(deviceIndex, 0xA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //            buff[0] = (byte)(buff[0] | 0x01);
        //            USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //        }
        //        else
        //        {
        //            buff = USBIO.ReadReg(deviceIndex, 0xA0, 93, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //            buff[0] = (byte)(buff[0] & 0xfe);
        //            USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //        }
        //        return true;
        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());
        //        return false;
        //    }

        //}
        //public override void SetSoftRxDis()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {

        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x80);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x40);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x20);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x10);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override void SetSoftRxEnable()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {

        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0x7f);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xbf);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xdf);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 241, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xef);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override void SetRxSquelchDis()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {

        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x80);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x40);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x20);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x10);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override void SetRxSquelchEnable()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {

        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0x7f);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xbf);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xdf);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xef);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override void SetTxSquelchDis()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {

        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x01);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x02);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x04);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] | 0x08);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //public override void SetTxSquelchEnable()
        //{
        //    byte[] buff = new byte[1];
        //    try
        //    {

        //        Engmod(3);
        //        switch (MoudleChannel)
        //        {
        //            case 1:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xfe);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 2:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xfd);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 3:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xfb);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            case 4:
        //                buff = USBIO.ReadReg(deviceIndex, 0xA0, 240, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
        //                buff[0] = (byte)(buff[0] & 0xf7);
        //                USBIO.WrtieReg(0, 0xA0, 86, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception error)
        //    {
        //        logger.AdapterLogString(3, error.ToString());

        //    }
        //}
        //w/r  sn/pn
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
        //read manufacture data
        //fwrev
        public override string ReadFWRev()
        {
            string fwrev = "";
            Engmod(4);
            fwrev = QSFP.ReadFWRev(deviceIndex, 0xA0, 128);
            return fwrev;
        }
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
                            log += "there is VccAdc" + tempadc + "\n";
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
                        logger.AdapterLogString(3, "there is no TemperatureAdc");
                        return false;
                    }

                case 2:
                    if (algorithm.FindFileName(DutStruct, "TEMPERATURE2ADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            tempadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            log += "there is VccAdc" + tempadc + "\n";
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
                        logger.AdapterLogString(3, "there is no TemperatureAdc");
                        return false;
                    }

                

                default:
                    if (algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            tempadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            log += "there is VccAdc" + tempadc + "\n";
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
                       logger.AdapterLogString(3, "there is no TemperatureAdc");
                        return false;

                    }

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
                            logger.AdapterLogString(1, "there is VccAdc" + vccadc);
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
                        logger.AdapterLogString(4, "there is no VccAdc");
                        return false;
                    }

                case 2:
                    if (algorithm.FindFileName(DutStruct, "VCC2ADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "there is VccAdc" + vccadc);
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
                        logger.AdapterLogString(4, "there is no VccAdc");
                        return false;
                    }

                case 3:
                    if (algorithm.FindFileName(DutStruct, "VCC3ADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                           logger.AdapterLogString(1, "there is VccAdc" + vccadc);
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
                        logger.AdapterLogString(4, "there is no VccAdc");
                        return false;
                    }

                default:
                    if (algorithm.FindFileName(DutStruct, "VCCADC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            vccadc = QSFP.readadc(deviceIndex, 0xA0, DutStruct[i].StartAddress);
                            logger.AdapterLogString(1, "there is VccAdc" + vccadc);
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
                        logger.AdapterLogString(4, "there is no VccAdc");
                        return false;

                    }

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
                    logger.AdapterLogString(1, "there is TxBiasAdc" + biasadc);
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
                logger.AdapterLogString(4, "there is no TxBiasAdc");
                return false;
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
                    logger.AdapterLogString(1, "there is TxPowerAdc" + txpadc);
                   
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
                logger.AdapterLogString(4, "there is no TxPowerAdc");
                return false;
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
                     logger.AdapterLogString(3, "there is RxPowerAdc" + rxpadc);
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
                logger.AdapterLogString(4, "there is no RxPowerAdc");
                return false;
            }
        }
        
        //read/writereg
        public override byte[] ReadReg(int deviceAddress, int regAddress, int readLength)
        {
            return USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, readLength);

        }
        public override byte[] WrtieReg( int deviceAddress, int regAddress, byte[] dataToWrite)
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
        public byte[] WriteDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, byte[] dataToWrite, bool Switch)
        {
            return QSFP.ReadWriteDriver40g(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x02, chipset, dataToWrite, Switch);
        }
        public byte[] ReadDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, int readLength, bool Switch)
        {
            return QSFP.ReadWriteDriver40g(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x01, chipset, new byte[readLength], Switch);
        }
        public byte[] StoreDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, byte[] dataToWrite, bool Switch)
        {
            return QSFP.ReadWriteDriver40g(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x04, chipset, dataToWrite, Switch);
        }
        //set biasmoddac
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
                return false;
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
                return false;
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
                return false;
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
                return false;
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
                return false;
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
                return false;
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
                return false;
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
                return false;
        }
        //read biasmoddac
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
                return false;
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
                return false;
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
                return false;
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
                return false;
        }
        //set coef
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
                            logger.AdapterLogString(1, "SET DmiTempCoefB" + tempcoefb);
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
                        logger.AdapterLogString(4, "there is no DmiTempCoefB");
                        return false;
                    }
                case 2:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMP2COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DmiTempCoefB" + tempcoefb);
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
                        logger.AdapterLogString(4, "there is no DmiTempCoefB");
                        return false;
                    }
                default:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DmiTempCoefB" + tempcoefb);
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
                        logger.AdapterLogString(4, "there is no DmiTempCoefB");
                        return false;
                    }
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
                        return false;
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
                        return false;
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
                        return false;
            
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
                            logger.AdapterLogString(1, "SET DmiTempCoefC" + tempcoefc);
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
                        logger.AdapterLogString(4, "there is no DmiTempCoefC");
                        return false;
                    }
                case 2:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMP2COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DmiTempCoefC" + tempcoefc);
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
                        logger.AdapterLogString(4, "there is no DmiTempCoefC");
                        return false;
                    }
                default:
                    //if (FindFiledNameTempSelect(out i, "DMITEMPCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DmiTempCoefC" + tempcoefc);
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
                        logger.AdapterLogString(4, "there is no DmiTempCoefC");
                        return false;
                    }
            
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
                        return false;
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
                        return false;
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
                        return false;

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
                            logger.AdapterLogString(1, "SET DmiVccCoefB" + vcccoefb);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefB");
                        return false;
                    }
                case 2:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC2COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DmiVccCoefB" + vcccoefb);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefB");
                        return false;
                    }
                case 3:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC3COEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DmiVccCoefB" + vcccoefb);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefB");
                        return false;
                    }
                default:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFB"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format);
                            logger.AdapterLogString(1, "SET DmiVccCoefB" + vcccoefb);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefB");
                        return false;
                    }
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
                    { return false; }
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
                    { return false; }
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
                    { return false; }
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
                        return false;
            
            
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
                            logger.AdapterLogString(0, "SET DmiVccCoefC" + vcccoefc);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefC");
                        return false;
                    }
                case 2:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC2COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                           logger.AdapterLogString(0, "SET DmiVccCoefC" + vcccoefc);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefC");
                        return false;
                    }
                case 3:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCC3COEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                            logger.AdapterLogString(0, "SET DmiVccCoefC" + vcccoefc);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefC");
                        return false;
                    }
                default:
                    //if (FindFiledNameVccSelect(out i, "DMIVCCCOEFC"))
                    if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
                    {
                        Engmod(DutStruct[i].EngPage);
                        try
                        {
                            flag = QSFP.SetCoef(deviceIndex, 0xA0, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format);
                            logger.AdapterLogString(0, "SET DmiVccCoefC" + vcccoefc);
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
                        logger.AdapterLogString(4, "there is no DmiVccCoefC");
                        return false;
                    }

            
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
                        return false;
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
                        return false;
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
                        return false;
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
                        return false;
                    }
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
                    logger.AdapterLogString(0, "SET DmiRxpowerCoefA" + rxcoefa);
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
                logger.AdapterLogString(4, "there is no DmiRxpowerCoefA");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET DmiRxpowerCoefB" + rxcoefb);
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
                logger.AdapterLogString(4, "there is no DmiRxpowerCoefB");
               
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET DmiRxpowerCoefC" + rxcoefc);
                    
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
                logger.AdapterLogString(4, "there is no DmiRxpowerCoefC");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET DmiTxPowerSlopCoefA" + txslopcoefa);
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
                logger.AdapterLogString(4, "there is no DmiTxPowerSlopCoefA");
                
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET DmiTxPowerSlopCoefB" + txslopcoefb);
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
                logger.AdapterLogString(4, "there is no DmiTxPowerSlopCoefB");
                return false;
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
                return false;
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
                   logger.AdapterLogString(1, "SET DmiTxPowerSlopCoefC" + txslopcoefc);
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
                logger.AdapterLogString(4, "there is no DmiTxPowerSlopCoefC");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET DmiTxPowerOffsetCoefA" + txoffsetcoefa);
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
                logger.AdapterLogString(4, "there is no DmiTxPowerOffsetCoefA");
               
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET DmiTxPowerOffsetCoefB" + txoffsetcoefb);
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
                logger.AdapterLogString(4, "there is no DmiTxPowerOffsetCoefB");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET DmiTxPowerOffsetCoefC" + txoffsetcoefc);
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
                logger.AdapterLogString(4, "there is no DmiTxPowerOffsetCoefC");
               
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TxTargetBiasDacCoefA" + biasdaccoefa);
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
                logger.AdapterLogString(4, "there is no TxTargetBiasDacCoefA");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TxTargetBiasDacCoefB" + biasdaccoefb);
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
                logger.AdapterLogString(4, "there is no TxTargetBiasDacCoefB");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TxTargetBiasDacCoefC" + biasdaccoefc);
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
                logger.AdapterLogString(4, "there is no TxTargetBiasDacCoefC");
                
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TxTargetModDacCoefA" + moddaccoefa);                 

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
                logger.AdapterLogString(4, "there is no TxTargetModDacCoefA");
              
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TxTargetModDacCoefB" + moddaccoefb);
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
                logger.AdapterLogString(4, "there is no TxTargetModDacCoefB");
              
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TxTargetModDacCoefC" + moddaccoefc);
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
                logger.AdapterLogString(4, "there is no TxTargetModDacCoefC");
              
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TargetLopcoefA" + targetlopcoefa);
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
                logger.AdapterLogString(4, "there is no TargetLopcoefA");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TargetLopcoefB" + targetlopcoefb);
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
                logger.AdapterLogString(4, "there is no TargetLopcoefB");
                return false;
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
                return false;
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
                    logger.AdapterLogString(1, "SET TargetLopcoefC" + targetlopcoefc);
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
                logger.AdapterLogString(4, "there is no TargetLopcoefC");
               
                return false;
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
                return false;
        }

        public override bool APCON()
        {

            int i = 0;
            byte[] buff = new byte[1];
            buff[0] = 0x55;
            //if (FindFiledName(out i, "APCCONTROLL"))
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
                return false;
        }
        public override bool APCOFF()
        {

            int i = 0;
            byte[] buff = new byte[1];
            buff[0] = 0x05;
            //if (FindFiledName(out i, "APCCONTROLL"))
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
                return false;
        }

        public override bool APCStatus(out string flag)
        {
            //0 OFF.1 ON
            flag = "FF";
            int i = 0;
            byte[] buff = new byte[1];
            //if (FindFiledName(out i, "APCCONTROLL"))
            if (algorithm.FindFileName(DutStruct, "APCCONTROLL", out i))
            {
                Engmod(DutStruct[i].EngPage);
                try
                {
                    buff = USBIO.ReadReg(deviceIndex, 0xA0, DutStruct[i].StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);

                    if (buff[0] == 0x55)
                        flag = "ON";
                    else if (buff[0] == 0x05)
                        flag = "OFF";
                    else
                        flag = "FF";
                    return true;
                }
                catch (Exception error)
                {
                    logger.AdapterLogString(3, error.ToString());
                    return false;
                }
            }
            else
                return false;


        }

        //close loop
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
                    logger.AdapterLogString(1, "SET CloseLoopcoefa" + CloseLoopcoefa);
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
                logger.AdapterLogString(4, "there is no CloseLoopcoefa");

                return false;
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
                    logger.AdapterLogString(1, "SET CloseLoopcoefa" + CloseLoopcoefb);
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
                logger.AdapterLogString(4, "there is no CloseLoopcoefb");

                return false;
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
                    logger.AdapterLogString(1, "SET CloseLoopcoefa" + CloseLoopcoefc);
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
                logger.AdapterLogString(4, "there is no CloseLoopcoefc");

                return false;
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

                return false;
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

                return false;
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

                return false;
            }
        }
    }
}
