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
        EEPROM CFP;
        public bool awstatus_flag = false;// aw status check
        public Algorithm algorithm = new Algorithm();
        public CFP4DUT(logManager logmanager)
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
                CFP = new EEPROM(deviceIndex, logger);
                phycialAdress = 7;
                if (!Connect()) return false;

            }

            catch (Exception error)
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
        public override bool ChangeChannel(string channel, int syn = 0)
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
            UInt16[] buff = new UInt16[2];
            buff[0] = 0xca2d;
            buff[1] = 0x815f;
            USBIO.WriteMDIO(deviceIndex, 1, phycialAdress, 0xA000, IOPort.MDIOSoftHard.SOFTWARE, buff);
        }
        #region dmi
        public override double ReadDmiTemp()
        {//A02F
            return CFP.readdmitemp(deviceIndex, 1, 0xA02F, phycialAdress, 1);

        }
        public override double ReadDmiVcc()
        {//A030

            return CFP.readdmivcc(deviceIndex, 1, 0xA030, phycialAdress, 1);

        }
        public override double ReadDmiBias()
        {//A2A0,A2A1,A2A2,A2A3
            double dmibias = 0.0;
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        dmibias = CFP.readdmibias(deviceIndex, 1, 0xA2A0, phycialAdress, 1);
                        break;
                    case 2:
                        dmibias = CFP.readdmibias(deviceIndex, 1, 0xA2A1, phycialAdress, 1);
                        break;
                    case 3:
                        dmibias = CFP.readdmibias(deviceIndex, 1, 0xA2A2, phycialAdress, 1);
                        break;
                    case 4:
                        dmibias = CFP.readdmibias(deviceIndex, 1, 0xA2A3, phycialAdress, 1);
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
        {//A2B0,A2B1,A2B2,A2B3
            double dmitxp = 0.0;
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        dmitxp = CFP.readdmitxp(deviceIndex, 1, 0xA2B0, phycialAdress, 1);
                        break;
                    case 2:
                        dmitxp = CFP.readdmitxp(deviceIndex, 1, 0xA2B1, phycialAdress, 1);
                        break;
                    case 3:
                        dmitxp = CFP.readdmitxp(deviceIndex, 1, 0xA2B2, phycialAdress, 1);
                        break;
                    case 4:
                        dmitxp = CFP.readdmitxp(deviceIndex, 1, 0xA2B3, phycialAdress, 1);
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
        {//A2D0,A2D1,A2D2,A2D3
            double dmirxp = 0.0;
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        dmirxp = CFP.readdmirxp(deviceIndex, 1, 0xA2D0, phycialAdress, 1);
                        break;
                    case 2:
                        dmirxp = CFP.readdmirxp(deviceIndex, 1, 0xA2D1, phycialAdress, 1);
                        break;
                    case 3:
                        dmirxp = CFP.readdmirxp(deviceIndex, 1, 0xA2D2, phycialAdress, 1);
                        break;
                    case 4:
                        dmirxp = CFP.readdmirxp(deviceIndex, 1, 0xA2D3, phycialAdress, 1);
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
        public override double ReadDmiLaTemp()
        {//A2C0,A2C1,A2C2,A2C3
            double dmirxp = 0.0;
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        dmirxp = CFP.readdmitemp(deviceIndex, 1, 0xA2C0, phycialAdress, 1);
                        break;
                    case 2:
                        dmirxp = CFP.readdmitemp(deviceIndex, 1, 0xA2C1, phycialAdress, 1);
                        break;
                    case 3:
                        dmirxp = CFP.readdmitemp(deviceIndex, 1, 0xA2C2, phycialAdress, 1);
                        break;
                    case 4:
                        dmirxp = CFP.readdmitemp(deviceIndex, 1, 0xA2C3, phycialAdress, 1);
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
        //a/w 未全部实现，只是用了check功能，read以及set不用
        #region read aw
        public override double ReadTempHA()
        {
            double tempha = 0.0;
            //Engmod(3);
            //tempha = CFP.readtempaw(deviceIndex, 0xA0, 128);
            return tempha;
        }
        public override double ReadTempLA()
        {
            double templa = 0.0;
            //Engmod(3);
            //templa = CFP.readtempaw(deviceIndex, 0xA0, 130);
            return templa;
        }
        public override double ReadTempLW()
        {
            double templw = 0.0;
            //Engmod(3);
            //templw = CFP.readtempaw(deviceIndex, 0xA0, 134);
            return templw;

        }
        public override double ReadTempHW()
        {
            double temphw = 0.0;
            //Engmod(3);
            //temphw = CFP.readtempaw(deviceIndex, 0xA0, 132);
            return temphw;
        }
        public override double ReadVccLA()
        {
            double vccla = 0.0;
            //Engmod(3);
            //vccla = CFP.readvccaw(deviceIndex, 0xA0, 146);
            return vccla;

        }
        public override double ReadVccHA()
        {
            double vccha = 0.0;
            //Engmod(3);
            //vccha = CFP.readvccaw(deviceIndex, 0xA0, 144);
            return vccha;

        }
        public override double ReadVccLW()
        {
            double vcclw = 0.0;
            //Engmod(3);
            //vcclw = CFP.readvccaw(deviceIndex, 0xA0, 150);
            return vcclw;

        }
        public override double ReadVccHW()
        {
            double vcchw = 0.0;
            //Engmod(3);
            //vcchw = CFP.readvccaw(deviceIndex, 0xA0, 148);
            return vcchw;

        }
        public override double ReadBiasLA()
        {
            double biasla = 0.0;
            //Engmod(3);
            //biasla = CFP.readbiasaw(deviceIndex, 0xA0, 186);
            return biasla;

        }
        public override double ReadBiasHA()
        {
            double biasha = 0.0;
            //Engmod(3);
            //biasha = CFP.readbiasaw(deviceIndex, 0xA0, 184);
            return biasha;

        }
        public override double ReadBiasLW()
        {
            double biaslw = 0.0;
            //Engmod(3);
            //biaslw = CFP.readbiasaw(deviceIndex, 0xA0, 190);
            return biaslw;

        }
        public override double ReadBiasHW()
        {
            double biashw = 0.0;
            //Engmod(3);
            //biashw = CFP.readbiasaw(deviceIndex, 0xA0, 188);
            return biashw;

        }
        public override double ReadTxpLA()
        {
            double txpla = 0.0;
            //Engmod(3);
            //txpla = CFP.readtxpaw(deviceIndex, 0xA0, 194);
            return txpla;

        }
        public override double ReadTxpLW()
        {
            double txplw = 0.0;
            //Engmod(3);
            //txplw = CFP.readtxpaw(deviceIndex, 0xA0, 198);
            return txplw;

        }
        public override double ReadTxpHA()
        {
            double txpha = 0.0;
            //Engmod(3);
            //txpha = CFP.readtxpaw(deviceIndex, 0xA0, 192);
            return txpha;

        }
        public override double ReadTxpHW()
        {
            double txphw = 0.0;
            //Engmod(3);
            //txphw = CFP.readtxpaw(deviceIndex, 0xA0, 196);
            return txphw;

        }
        public override double ReadRxpLA()
        {
            double rxpla = 0.0;
            //Engmod(3);
            //rxpla = CFP.readrxpaw(deviceIndex, 0xA0, 178);
            return rxpla;

        }
        public override double ReadRxpLW()
        {
            double rxplw = 0.0;
            //Engmod(3);
            //rxplw = CFP.readrxpaw(deviceIndex, 0xA0, 182);
            return rxplw;

        }
        public override double ReadRxpHA()
        {
            double rxpha = 0.0;
            //Engmod(3);
            //rxpha = CFP.readrxpaw(deviceIndex, 0xA0, 176);
            return rxpha;

        }
        public override double ReadRxpHW()
        {
            double rxphw = 0.0;
            //Engmod(3);
            //rxphw = CFP.readrxpaw(deviceIndex, 0xA0, 180);
            return rxphw;

        }

        #endregion
        #region check a/w
        private int biasawaddr()
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

        public override bool ChkTempHA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTempLA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkVccHA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkVccLA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }

        public override bool ChkBiasHA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkBiasLA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTxpHA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTxpLA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkRxpHA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkRxpLA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTempHW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkTempLW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool ChkVccHW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkVccLW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA01F, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkBiasHW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkBiasLW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkTxpHW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkTxpLW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkRxpHW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkRxpLW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkLaTempHA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkLaTempLA()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkLaTempHW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkLaTempLW()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = biasawaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            UInt16[] buff = new UInt16[1];
            try
            {

                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        private int txrxlosaddr()
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
        public override bool ChkTxFault()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = txrxlosaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);

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
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChkRxLos()
        {
            UInt16[] buff = new UInt16[1];
            try
            {
                int biashaadd = txrxlosaddr();
                buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, biashaadd, IOPort.MDIOSoftHard.SOFTWARE, 1);

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
            //Engmod(3);
            //CFP.settempaw(deviceIndex, 0xA0, 130, templa);
        }
        public override void SetTempHA(decimal tempha)
        {
            //Engmod(3);
            //CFP.settempaw(deviceIndex, 0xA0, 128, tempha);
        }
        public override void SetTempLW(decimal templw)
        {
            //Engmod(3);
            //CFP.settempaw(deviceIndex, 0xA0, 134, templw);
        }
        public override void SetTempHW(decimal temphw)
        {
            //Engmod(3);
            //CFP.settempaw(deviceIndex, 0xA0, 132, temphw);
        }
        public override void SetVccHW(decimal vcchw)
        {
            //Engmod(3);
            //CFP.setvccaw(deviceIndex, 0xA0, 148, vcchw);
        }
        public override void SetVccLW(decimal vcclw)
        {
            //Engmod(3);
            //CFP.setvccaw(deviceIndex, 0xA0, 150, vcclw);
        }
        public override void SetVccLA(decimal vccla)
        {
            //Engmod(3);
            //CFP.setvccaw(deviceIndex, 0xA0, 146, vccla);
        }
        public override void SetVccHA(decimal vccha)
        {
            //Engmod(3);
            //CFP.setvccaw(deviceIndex, 0xA2, 144, vccha);
        }
        public override void SetBiasLA(decimal biasla)
        {
            //Engmod(3);
            //CFP.setbiasaw(deviceIndex, 0xA0, 186, biasla);
        }
        public override void SetBiasHA(decimal biasha)
        {
            //Engmod(3);
            //CFP.setbiasaw(deviceIndex, 0xA0, 184, biasha);
        }
        public override void SetBiasHW(decimal biashw)
        {
            //Engmod(3);
            //CFP.setbiasaw(deviceIndex, 0xA0, 188, biashw);
        }
        public override void SetBiasLW(decimal biaslw)
        {
            //Engmod(3);
            //CFP.setbiasaw(deviceIndex, 0xA0, 190, biaslw);
        }
        public override void SetTxpLW(decimal txplw)
        {
            //Engmod(3);
            //CFP.settxpaw(deviceIndex, 0xA0, 198, txplw);
        }
        public override void SetTxpHW(decimal txphw)
        {
            //Engmod(3);
            //CFP.settxpaw(deviceIndex, 0xA0, 196, txphw);
        }
        public override void SetTxpLA(decimal txpla)
        {
            //Engmod(3);
            //CFP.settxpaw(deviceIndex, 0xA0, 194, txpla);
        }
        public override void SetTxpHA(decimal txpha)
        {
            //Engmod(3);
            //CFP.settxpaw(deviceIndex, 0xA0, 192, txpha);
        }
        public override void SetRxpHA(decimal rxpha)
        {
            //Engmod(3);
            //CFP.setrxpaw(deviceIndex, 0xA0, 176, rxpha);
        }
        public override void SetRxpLA(decimal rxpla)
        {
            //Engmod(3);
            //CFP.setrxpaw(deviceIndex, 0xA0, 178, rxpla);
        }
        public override void SetRxpHW(decimal rxphw)
        {
            //Engmod(3);
            //CFP.setrxpaw(deviceIndex, 0xA0, 180, rxphw);
        }
        public override void SetRxpLW(decimal rxplw)
        {
            //Engmod(3);
            //CFP.setrxpaw(deviceIndex, 0xA0, 182, rxplw);
        }

        public override bool SetSoftTxDis()
        {
            UInt16[] buff = new UInt16[1];
            try
            {


                switch (MoudleChannel)
                {
                    case 1:
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                        buff[0] = (UInt16)(buff[0] | 0x0001);
                        USBIO.WriteMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                        break;
                    case 2:
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                        buff[0] = (UInt16)(buff[0] | 0x0002);
                        USBIO.WriteMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                        break;
                    case 3:
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                        buff[0] = (UInt16)(buff[0] | 0x0004);
                        USBIO.WriteMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                        break;
                    case 4:
                        buff = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
                        buff[0] = (UInt16)(buff[0] | 0x0008);
                        USBIO.WriteMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
                        break;
                    default:
                        break;
                }
                return true;

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;

            }
        }
        public override bool TxAllChannelEnable()
        {
            UInt16[] buff = new UInt16[1];
            UInt16[] buffRead;
            buff[0] = 0;
            USBIO.WriteMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, buff);
            Thread.Sleep(50);
            buffRead = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0xA013, IOPort.MDIOSoftHard.SOFTWARE, 1);
            if (buffRead[0]==0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion
        //w/r  sn/pn 只需实现读取既可以
        #region w/r  sn/pn
        public override string ReadSn()
        {
            string sn = "";
            sn = CFP.ReadSn(deviceIndex, 1, 0x8044, phycialAdress, 1);
            return sn;

        }
        public override string ReadPn()
        {
            string pn = "";
            pn = CFP.ReadPn(deviceIndex, 1, 0x8034, phycialAdress, 1);
            return pn;

        }
        public override void SetSn(string sn)
        {
            //Engmod(0);
            //CFP.SetSn(deviceIndex, 0xA0, 196, sn);
            ////System.Threading.Thread.Sleep(1000);

        }
        public override void SetPn(string pn)
        {
            //Engmod(0);
            //CFP.SetPn(deviceIndex, 0xA0, 168, pn);
            ////System.Threading.Thread.Sleep(1000);

        }
        #endregion
        //read manufacture data
        #region fwrev
        public override string ReadFWRev()
        {
            string fwrev = "";
            Engmod(3);
            fwrev = CFP.ReadFWRev(deviceIndex, 1, 0x9000, phycialAdress, 1);
            return fwrev;
        }
        #endregion
        #region ADC
        public override bool ReadTempADC(out UInt16 tempadc)
        {
            tempadc = 0;
            int i = 0;

            if (algorithm.FindFileName(DutStruct, "TEMPERATUREADC", out i))
            {
                Engmod(3);
                try
                {
                    tempadc = CFP.readadc(deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
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
        public override bool ReadTempADC(out UInt16 tempadc, byte TempSelect)
        { 
        return ReadTempADC(out tempadc);
        }
        public override bool ReadLaTempADC(out UInt16 latempadc)
        {
            latempadc = 0;
            int i = 0;

            if (FindFiledNameChannel(out i, "LATEMPERATUREADC"))
            {
                Engmod(3);
                try
                {
                    latempadc = CFP.readadc(deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
                    logger.AdapterLogString(1, "Current LATEMPERATUREADC is" + latempadc);
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
                logger.AdapterLogString(4, "there is no LATEMPERATUREADC");
                return true;
            }


        }
        public override bool ReadVccADC(out UInt16 vccadc)
        {//vccselect 1 VCC1ADC
            vccadc = 0;
            int i = 0;

            if (algorithm.FindFileName(DutStruct, "VCCADC", out i))
            {
                Engmod(3);
                try
                {
                    vccadc = CFP.readadc(deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
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
                Engmod(3);
                try
                {
                    biasadc = CFP.readadc(deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
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
        public override bool ReadTxpADC(out UInt16 txpadc)
        {
            txpadc = 0;
            int i = 0;

            if (FindFiledNameChannel(out i, "TXPOWERADC"))
            {
                Engmod(3);
                try
                {
                    txpadc = CFP.readadc(deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
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
        public override bool ReadRxpADC(out UInt16 rxpadc)
        {
            rxpadc = 0;
            int i = 0;

            if (FindFiledNameChannel(out i, "RXPOWERADC"))
            {
                Engmod(3);
                try
                {
                    rxpadc = CFP.readadc(deviceIndex, 1, DutStruct[i].StartAddress, phycialAdress, 1);
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
            return USBIO.ReadMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, readLength);

        }
        public override byte[] WriteMDIO(int deviceIndex, int deviceAddress, int regAddress, UInt16[] dataToWrite)
        {
            return USBIO.WriteMDIO(deviceIndex, deviceAddress, phycialAdress, regAddress, IOPort.MDIOSoftHard.SOFTWARE, dataToWrite);
        }
        #endregion
       
       
        #region driver
        public UInt16 MDIOWriteDriver(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, UInt16 dataToWrite)
        {
            return CFP.ReadWriteDriverCFP(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x02, chipset, dataToWrite, phycialAdress);
        }
        public UInt16 MDIOReadDriver(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset)
        {
            return CFP.ReadWriteDriverCFP(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x01, chipset, 0, phycialAdress);
        }
        public UInt16 MDIOStoreDriver(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte chipset, UInt16 dataToWrite)
        {
            return CFP.ReadWriteDriverCFP(deviceIndex, deviceAddress, StartAddress, regAddress, channel, 0x04, chipset, dataToWrite, phycialAdress);
        }
        //driver innitialize
        public override bool DriverInitialize()
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1ldd,2amp,3cdr,0dac
            int j = 0;
            int k = 0;
            //byte engpage = 0;
            int startaddr = 0;
            byte chipset = 0x01;
            UInt16 temp;
            bool flag = true;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                            MDIOWriteDriver(deviceIndex, 1, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                            // Thread.Sleep(200);  
                            MDIOStoreDriver(deviceIndex, 1, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
                            // Thread.Sleep(200); 
                            temp = MDIOReadDriver(deviceIndex, 1, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].ChipLine, chipset);

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
        //eeprominit
        public override bool EEpromInitialize()
        {
            int j = 0;
            int k = 0;
            byte engpage = 0;
            UInt16[] temp;
            bool flag = true;
            if (EEpromInitStruct.Length == 0)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < EEpromInitStruct.Length; i++)
                {
                    byte[] WriteeeArray = algorithm.ObjectTOByteArray(EEpromInitStruct[i].ItemValue, 1, true);
                    ushort[] WriteBiasDacByteArray = new ushort[1];
                    WriteBiasDacByteArray[0] = (ushort)(WriteeeArray[0]);
                    engpage = EEpromInitStruct[j].Page;
                    Engmod(engpage);
                    for (k = 0; k < 3; k++)
                    {
                        WriteMDIO(deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, WriteBiasDacByteArray);
                        temp = new ushort[1];
                        temp = ReadMDIO(deviceIndex, EEpromInitStruct[i].SlaveAddress, EEpromInitStruct[i].StartAddress, 1);
                        if (temp[0] == WriteBiasDacByteArray[0])
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
            //byte engpage=0;
            int startaddr = 0;
            byte chipset = 0x01;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOWriteDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage=0;
            int startaddr = 0;
            byte chipset = 0x01;
            //if (FindFiledName(out j, "DEBUGINTERFACE"))
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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

                MDIOWriteDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOWriteDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOWriteDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOWriteDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            ////byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOWriteDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOStoreDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOStoreDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOStoreDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOStoreDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOStoreDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                MDIOStoreDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset, WriteBiasDacByteArray);
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
            //byte engpage = 0;
            int startaddr = 0;
            if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
            {
                //engpage = DutStruct[j].EngPage;
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
                UInt16 temp = MDIOReadDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset);
                crossdac[0] = (byte)(temp / 256);
                crossdac[1] = (byte)(temp & 0xFF);
                return true;
            }
            else
            {
                logger.AdapterLogString(4, "there is no CROSSDAC");
                return true;
            }
        }
    
        //public override bool ReadModDac(int length, out byte[] ModDac)
        //{
        //    byte chipset = 0x01;
        //    ModDac = new byte[length];
        //    int j = 0;
        //    //byte engpage = 0;
        //    int startaddr = 0;
        //    if (algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
        //    {
        //        //engpage = DutStruct[j].EngPage;
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
        //        UInt16 temp = MDIOReadDriver(deviceIndex, 1, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].ChipLine, chipset);
        //        ModDac[0] = (byte)(temp / 256);
        //        ModDac[1] = (byte)(temp & 0xFF);
        //        return true;
        //    }
        //    else
        //    {
        //        logger.AdapterLogString(4, "there is no MODDAC");
        //        return true;
        //    }
        //}
      
        #endregion

        #region set coef


        public override bool SetTempcoefa(string tempcoefa)
        {
            bool flag = false;
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFA", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, tempcoefa, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET DMITEMPCOEFA To" + tempcoefa);
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
                logger.AdapterLogString(4, "there is no DMITEMPCOEFA");
                return true;
            }


        }
        public override bool SetTempcoefb(string tempcoefb, byte TempSelect)
        {
            return SetTempcoefb(tempcoefb);
        }
        public override bool SetTempcoefb(string tempcoefb)
        {
            bool flag = false;
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, tempcoefb, DutStruct[i].Format, phycialAdress, 1);
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
            return ReadTempcoefb(out strcoef);
        }
        public override bool ReadTempcoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFA", out i))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMITEMPCOEFA");
                return true;
            }


        }
        public override bool SetTempcoefc(string tempcoefc, byte TempSelect)
        {
            return SetTempcoefc(tempcoefc);
        }
        public override bool ReadTempcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFB", out i))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
        public override bool ReadTempcoefc(out string strcoef, byte TempSelect)
        {
            return ReadTempcoefc(out strcoef);
        }
        public override bool SetTempcoefc(string tempcoefc)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, tempcoefc, DutStruct[i].Format, phycialAdress, 1);
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
        public override bool SetVcccoefb(string vcccoefb, byte vccselect)
        {
            return SetVcccoefb(vcccoefb);
        }
        public override bool ReadTempcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMITEMPCOEFC", out i))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
        public override bool ReadVcccoefb(out string strcoef, byte vccselect)
        {
            return ReadVcccoefb(out strcoef);
        }
        public override bool SetVcccoefc(string vcccoefc, byte vccselect)
        {
            return SetVcccoefc(vcccoefc);

        }
        public override bool ReadVcccoefc(out string strcoef, byte vccselect)
        {
            return ReadVcccoefc(out strcoef);
        }
        public override bool SetVcccoefa(string vcccoefa)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFA", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, vcccoefa, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET DMIVCCCOEFA To" + vcccoefa);
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
                logger.AdapterLogString(4, "there is no DMIVCCCOEFA");
                return true;
            }

        }
        public override bool SetVcccoefb(string vcccoefb)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, vcccoefb, DutStruct[i].Format, phycialAdress, 1);
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
        public override bool ReadVcccoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFA", out i))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMIVCCCOEFA");
                return true;
            }


        }
        public override bool ReadVcccoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFB", out i))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
            bool flag = false;
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, vcccoefc, DutStruct[i].Format, phycialAdress, 1);
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
        public override bool ReadVcccoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;
            if (algorithm.FindFileName(DutStruct, "DMIVCCCOEFC", out i))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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

        #region  DmiRxPower

        public override bool SetRxpcoefa(string rxcoefa)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMIRXPOWERCOEFA"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, rxcoefa, DutStruct[i].Format, phycialAdress, 1);
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
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, rxcoefb, DutStruct[i].Format, phycialAdress, 1);
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
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, rxcoefc, DutStruct[i].Format, phycialAdress, 1);
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

                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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

        //NEW ADD
      

        public override bool SetTxAuxcoefa(string TxAuxcoefa)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXAUXCOEFA"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, TxAuxcoefa, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET DMITXAUXCOEFA To" + TxAuxcoefa);
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
                logger.AdapterLogString(4, "there is no DMITXAUXCOEFA");
                return true;
            }
        }
        public override bool ReadTxAuxcoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXAUXCOEFA"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMITXAUXCOEFA");
                return true;
            }
        }
        public override bool SetTxAuxcoefb(string TxAuxcoefb)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXAUXCOEFB"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, TxAuxcoefb, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET DMITXAUXCOEFB To" + TxAuxcoefb);
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
                logger.AdapterLogString(4, "there is no DMITXAUXCOEFB");
                return true;
            }
        }
        public override bool ReadTxAuxcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXAUXCOEFB"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMITXAUXCOEFB");
                return true;
            }
        }
        public override bool SetTxAuxcoefc(string TxAuxcoefc)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXAUXCOEFC"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, TxAuxcoefc, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET DMITXAUXCOEFC To" + TxAuxcoefc);
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
                logger.AdapterLogString(4, "there is no DMITXAUXCOEFC");
                return true;
            }
        }
        public override bool ReadTxAuxcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXAUXCOEFC"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMITXAUXCOEFC");
                return true;
            }
        }
        public override bool SetLaTempcoefa(string LaTempcoefa)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMILATMPCOEFA"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, LaTempcoefa, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET DMILATMPCOEFA To" + LaTempcoefa);
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
                logger.AdapterLogString(4, "there is no DMILATMPCOEFA");
                return true;
            }
        }
        public override bool ReadLaTempcoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMILATMPCOEFA"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMILATMPCOEFA");
                return false;
            }
        }
        public override bool SetLaTempcoefb(string LaTempcoefb)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMILATMPCOEFB"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, LaTempcoefb, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET DMILATMPCOEFB To" + LaTempcoefb);
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
                logger.AdapterLogString(4, "there is no DMILATMPCOEFB");
                return false;
            }
        }
        public override bool ReadLaTempcoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMILATMPCOEFB"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMILATMPCOEFB");
                return true;
            }
        }
        public override bool SetLaTempcoefc(string LaTempcoefc)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMILATMPCOEFC"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, LaTempcoefc, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET DMILATMPCOEFC To" + LaTempcoefc);
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
                logger.AdapterLogString(4, "there is no DMILATMPCOEFC");
                return false;
            }
        }
        public override bool ReadLaTempcoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMILATMPCOEFC"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMILATMPCOEFC");
                return false;
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
                        Engmod(3);
                        strcoef = CFP.ReadALLCoef(deviceIndex, 1, DutStruct[j].StartAddress, DutStruct[j].Format, phycialAdress, 1);
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
                    strcoef = CFP.ReadALLEEprom(deviceIndex, EEpromInitStruct[j].SlaveAddress, EEpromInitStruct[j].StartAddress, phycialAdress, 1);
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
        #region pid
        public override bool PIDCloseOpen(bool pidswitch)
        {
            UInt16[] bcoefmdio = new UInt16[1];
            Engmod(3);
            if (pidswitch)
            {

                bcoefmdio = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0x93F6, IOPort.MDIOSoftHard.SOFTWARE, 1);
                bcoefmdio[0] |= 0x0080;
            }
            else
            {

                bcoefmdio = USBIO.ReadMDIO(deviceIndex, 1, phycialAdress, 0x93F6, IOPort.MDIOSoftHard.SOFTWARE, 1);
                bcoefmdio[0] &= 0xff7f;
            }

            try
            {
                USBIO.WriteMDIO(deviceIndex, 1, phycialAdress, 0x93F6, IOPort.MDIOSoftHard.SOFTWARE, bcoefmdio);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool SetcoefP1(string coefp)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "COEFP1", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, coefp, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET COEFP1" + coefp);
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
                logger.AdapterLogString(4, "there is no COEFP1");

                return true;
            }
        }
        public override bool SetcoefI1(string coefi)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "COEFI1", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, coefi, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET COEFI1" + coefi);
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
                logger.AdapterLogString(4, "there is no COEFI1");

                return true;
            }
        }
        public override bool SetcoefD1(string coefd)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "COEFD1", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, coefd, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET COEFD1" + coefd);
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
                logger.AdapterLogString(4, "there is no COEFD1");

                return true;
            }
        }

        public override bool SetPIDSetpoint1(string setpoint)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "SETPOINT1", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET SETPOINT1" + setpoint);
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
                logger.AdapterLogString(4, "there is no SETPOINT1");

                return true;
            }
        }
        public override bool SetcoefP2(string coefp)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "COEFP2", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, coefp, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET COEFP2" + coefp);
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
                logger.AdapterLogString(4, "there is no COEFP2");

                return true;
            }
        }
        public override bool SetcoefI2(string coefi)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "COEFI2", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, coefi, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET COEFI2" + coefi);
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
                logger.AdapterLogString(4, "there is no COEFI2");

                return true;
            }
        }
        public override bool SetcoefD2(string coefd)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "COEFD2", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, coefd, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET COEFD2" + coefd);
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
                logger.AdapterLogString(4, "there is no COEFD2");

                return true;
            }
        }

        public override bool SetPIDSetpoint2(string setpoint)
        {
            int i = 0;
            bool flag = false;
            if (algorithm.FindFileName(DutStruct, "SETPOINT2", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, setpoint, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET SETPOINT2" + setpoint);
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
                logger.AdapterLogString(4, "there is no SETPOINT2");

                return true;
            }
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
                Engmod(3);
                try
                {

                   bool   flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, buff[0].ToString(), DutStruct[i].Format, phycialAdress, 1);
              
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
                    bool flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, buff[0].ToString(), DutStruct[i].Format, phycialAdress, 1);
                  
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
                    bool flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, buff[0].ToString(), DutStruct[i].Format, phycialAdress, 1);

                    string   strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);

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
        public override bool SetdmiRxOffset(string dmiRxOffset)
        {//format 2 uint16

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMIRXOFFSET"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, dmiRxOffset, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET DMIRXOFFSET To" + dmiRxOffset);
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
                logger.AdapterLogString(4, "there is no DMIRXOFFSET");
                return true;
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
        public override bool SetRXPadcThreshold(byte threshold)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "RXADCTHRESHOLD"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, threshold.ToString(), DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET threshold To" + threshold);
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
                logger.AdapterLogString(4, "there is no RXADCTHRESHOLD");
                return true;
            }
        }
        #endregion
        #region new add as cgr4 new map

        #region  DmiTxPower

        public override bool SetReferenceTemp(string referencetempcoef)
        {
            bool flag = false;
            int i = 0;
            //if (FindFiledNameChannel(out i, "REFERENCETEMPERATURECOEF"))
            if (algorithm.FindFileName(DutStruct, "REFERENCETEMPERATURECOEF", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, referencetempcoef, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET REFERENCETEMPERATURECOEF To" + referencetempcoef);
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
                return false;
            }
        }


        public override bool SetTxpProportionLessCoef(string Lesscoef)
        {

            bool flag = false;
            int i = 0;
            if (FindFiledNameChannel(out i, "TXPPROPORTIONLESSCOEF"))
           // if (algorithm.FindFileName(DutStruct, "TXPPROPORTIONLESSCOEF", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, Lesscoef, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET TXPPROPORTIONLESSCOEF To" + Lesscoef);
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
                return false;
            }
         
        }

        public override bool SetTxpProportionGreatCoef(string Greatcoef)
        {
            bool flag = false;
            int i = 0;

            if (FindFiledNameChannel(out i, "TXPPROPORTIONGREATCOEF"))
            //if (algorithm.FindFileName(DutStruct, "TXPPROPORTIONGREATCOEF", out i))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, Greatcoef, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(1, "SET TXPPROPORTIONGREATCOEF To" + Greatcoef);
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
                return false;
            }

        }

        public override bool SetTxpFitsCoefa(string Txpcoefa)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWERCOEFA"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, Txpcoefa, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET Txpfitscoefa To" + Txpcoefa);
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
                logger.AdapterLogString(4, "there is no Txpfitscoefa");
                return true;
            }
        }
        public override bool SetTxpFitsCoefb(string Txpcoefb)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWERCOEFB"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, Txpcoefb, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET Txpfitscoefb To" + Txpcoefb);
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
                logger.AdapterLogString(4, "there is no Txpfitscoefb");
                return true;
            }
        }

        public override bool SetTxpFitsCoefc(string Txpcoefc)
        {

            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "DMITXPOWERCOEFC"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, Txpcoefc, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET TxpfitscoefC To" + Txpcoefc);
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
                logger.AdapterLogString(4, "there is no TxpfitscoefC");
                return true;
            }
        }


        public override bool ReadTxpFitsCoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXPFITSCOEFA"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMITXPOWERCOEFA");
                return true;
            }
        }

     
        public override bool ReadTxpFitsCoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXPOWERCOEFB"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMITXPOWERCOEFB");
                return true;
            }
        }
     
        public override bool ReadTxpFitsCoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "DMITXPOWERCOEFC"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                logger.AdapterLogString(4, "there is no DMITXPOWERCOEFC");
                return true;
            }
        }


#endregion



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
        #region new add
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


            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFA"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, biasdaccoefa, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET TxTargetBiasDacCoefA  To" + biasdaccoefa);
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
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                return false;
            }
        }
        public override bool SetBiasdaccoefb(string biasdaccoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, biasdaccoefb, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET TxTargetBiasDacCoefB   To" + biasdaccoefb);
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
                logger.AdapterLogString(4, "there is no TxTargetBiasDacCoefB ");
                return false;
            }
        }
        public override bool ReadBiasdaccoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFB"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                return false;
            }
        }
        public override bool SetBiasdaccoefc(string biasdaccoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, biasdaccoefc, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET TXTARGETBIASDACCOEFC   To" + biasdaccoefc);
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
                logger.AdapterLogString(4, "there is no TXTARGETBIASDACCOEFC ");
                return false;
            }
        }
        public override bool ReadBiasdaccoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETBIASDACCOEFC"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                return false;
            }
        }
        public override bool SetModdaccoefa(string moddaccoefa)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, moddaccoefa, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET TXTARGETMODDACCOEFB   To" + moddaccoefa);
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
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFA ");
                return false;
            }
        }
        public override bool ReadModdaccoefa(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFA"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                return false;
            }
        }
        public override bool SetModdaccoefb(string moddaccoefb)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, moddaccoefb, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET TXTARGETMODDACCOEFB   To" + moddaccoefb);
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
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFB ");
                return false;
            }
        }
        public override bool ReadModdaccoefb(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFB"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                return false;
            }
        }
        public override bool SetModdaccoefc(string moddaccoefc)
        {
            int i = 0;
            bool flag = false;
            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
            {
                Engmod(3);
                try
                {
                    flag = CFP.SetCoef(deviceIndex, 1, DutStruct[i].StartAddress, moddaccoefc, DutStruct[i].Format, phycialAdress, 1);
                    logger.AdapterLogString(0, "SET TXTARGETMODDACCOEFB   To" + moddaccoefc);
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
                logger.AdapterLogString(4, "there is no TXTARGETMODDACCOEFC ");
                return false;
            }
        }
        public override bool ReadModdaccoefc(out string strcoef)
        {
            strcoef = "";
            int i = 0;

            if (FindFiledNameChannel(out i, "TXTARGETMODDACCOEFC"))
            {
                Engmod(3);
                try
                {
                    strcoef = CFP.ReadCoef(deviceIndex, 1, DutStruct[i].StartAddress, DutStruct[i].Format, phycialAdress, 1);
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
                return false;
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
