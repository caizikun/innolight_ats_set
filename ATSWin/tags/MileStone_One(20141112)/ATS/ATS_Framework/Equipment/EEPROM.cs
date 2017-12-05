using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS_Framework
{
    public class EEPROM : EquipmentBase
    {

        public EEPROM(int INDEX)
        {
            USBIO = new IOPort("USB", INDEX.ToString());
        }
        //read DMI
        public double readdmitemp(int deviceIndex,int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double temperature = 0.0;
            try
            {

                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                if (buff[0] > Convert.ToByte(127))
                {
                    temperature = (buff[0] + (buff[1] / 256.0)) - 256;
                }
                else
                {
                    temperature = (buff[0] + (buff[1] / 256.0));
                }
                temperature = Math.Round(temperature, 4);
                return temperature;

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        public double readdmivcc(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double vcc = 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                vcc = (buff[0] * 256 + buff[1]) / 10000.0;
                vcc = Math.Round(vcc, 4);
                return vcc;

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        public double readdmibias(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double bias = 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                bias = (buff[0] * 256 + buff[1]) / 500.0;
                bias = Math.Round(bias, 4);
                return bias;

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        public double readdmitxp(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double txp = 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                txp = 10 * (Math.Log10((buff[0] * 256 + buff[1]) * (1E-4)));
                txp = Math.Round(txp, 4);
                return txp;

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        public double readdmirxp(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double rxp = 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                rxp = 10 * (Math.Log10((buff[0] * 256 + buff[1]) * (1E-4)));

                rxp = Math.Round(rxp, 4);
                return rxp;

            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //read a/w
        //temp  alarm/waring
        public double readtempaw(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double tempaw = 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                if (buff[0] > Convert.ToByte(127))
                {
                    tempaw = ((buff[0] * 256 + buff[1]) - 65536) / 256.0;
                    
                }
                else
                {
                     tempaw = (buff[0] * 256 + buff[1]) / 256.0;

                }
                tempaw = Math.Round(tempaw, 4);
                return tempaw;

            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //vcc alarm/waring
        public double readvccaw(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double vccaw = 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                vccaw = (buff[0] * 256 + buff[1]) * 0.0001;
                vccaw = Math.Round(vccaw, 4);
                return vccaw;

            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //bias alarm/waring
        public double readbiasaw(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double biasaw = 0.0;
            try
            {

                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                biasaw = (buff[0] * 256 + buff[1]) * 0.002;
                biasaw = Math.Round(biasaw, 4);
                return biasaw;

            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //txp alarm/waring
        public double readtxpaw(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double txpaw= 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                txpaw = (buff[0] * 256 + buff[1]) * 0.1;
                //txpaw = 10 * (Math.Log10((buff[0] * 256 + buff[1]) * (1E-4)));
                txpaw = Math.Round(txpaw, 4);
                return txpaw;
               
            }
            catch (Exception error)
            {

                throw error;
            }
        }
        //rxp alarm/waring
        public double readrxpaw(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            double rxpaw = 0.0;
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                rxpaw = (buff[0] * 256 + buff[1]) * 0.1;
                //rxpaw = 10 * (Math.Log10((buff[0] * 256 + buff[1]) * (1E-4)));
                rxpaw = Math.Round(rxpaw, 4);
                return rxpaw;

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        
        //check temp high alarm
        public bool ChkTempHA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check temp low alarm
        public bool ChkTempLA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check vcc high alarm
        public bool ChkVccHA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check vcc low alarm
        public bool ChkVccLA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check bias high alarm
        public bool ChkBiasHA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x08) != 0)
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

                throw error;
            }
        }
        //check bias low alarm
        public bool ChkBiasLA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x04) != 0)
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

                throw error;
            }
        }
        //check txp high alarm
        public bool ChkTxpHA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x02) != 0)
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

                throw error;
            }
        }
        //check txp low alarm
        public bool ChkTxpLA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x01) != 0)
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

                throw error;
            }
        }
        //check rxp high alarm
        public bool ChkRxpHA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check rxp low alarm
        public bool ChkRxpLA(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check temp high warning
        public bool ChkTempHW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check temp low warning
        public bool ChkTempLW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check vcc high warning
        public bool ChkVccHW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check vcc low warning
        public bool ChkVccLW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check bias high warning
        public bool ChkBiasHW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x08) != 0)
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

                throw error;
            }
        }
        //check bias low warning
        public bool ChkBiasLW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x04) != 0)
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

                throw error;
            }
        }
        //check txp high warning
        public bool ChkTxpHW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x02) != 0)
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

                throw error;
            }
        }
        //check txp low warning
        public bool ChkTxpLW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x01) != 0)
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

                throw error;
            }
        }
        //check rxp high warning
        public bool ChkRxpHW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //check rxp low warning
        public bool ChkRxpLW(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
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

                throw error;
            }
        }
        //read optional status /control bit 110
        //tx disable
        public bool ChkTxDis(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if (((buff[0] & 0x80) != 0) || ((buff[0] & 0x40) != 0))
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

                throw error;
            }
        }
        //tx fault
        public bool ChkTxFault(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x04) != 0)
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

                throw error;
            }
        }
        //rx los
        public bool ChkRxLos(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                if ((buff[0] & 0x02) != 0)
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

                throw error;
            }
        }
        
        //set temp  alarm warning
        public void settempaw(int deviceIndex, int deviceAddress, int regAddress, decimal tempaw)
        {
            byte[] buff = new byte[2];
            Int64 itest1;
            try
            {
                if ((Convert.ToInt64(tempaw) * 256) >= 0)
                {
                    itest1 = Convert.ToInt64(tempaw) * 256;
                    buff[0] = Convert.ToByte(itest1 / 256);
                    buff[1] = Convert.ToByte(itest1 % 256);
                }
                else
                {
                    itest1 = 65536 - Math.Abs(Convert.ToInt64(tempaw) * 256);
                    buff[0] = Convert.ToByte(itest1 / 256);
                    buff[1] = Convert.ToByte(itest1 % 256);

                }
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);


            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //set vcc alarm warning 
        public void setvccaw(int deviceIndex, int deviceAddress, int regAddress, decimal vccaw)
        {
            byte[] buff = new byte[2];
            Int64 itest1;
            try
            {
                itest1 = Convert.ToInt64(vccaw) * 10000;
                buff[0] = Convert.ToByte(itest1 / 256);
                buff[1] = Convert.ToByte(itest1 % 256);
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //set bias alarm warning 
        public void setbiasaw(int deviceIndex, int deviceAddress, int regAddress, decimal biasaw)
        {
            byte[] buff = new byte[2];
            Int64 itest1;
            try
            {
                itest1 = Convert.ToInt64(biasaw) * 500;
                buff[0] = Convert.ToByte(itest1 / 256);
                buff[1] = Convert.ToByte(itest1 % 256);
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //set txp alarm warning 
        public void settxpaw(int deviceIndex, int deviceAddress, int regAddress, decimal txpaw)
        {
            byte[] buff = new byte[2];
            Int64 itest1;
            try
            {
                itest1 = Convert.ToInt64(txpaw) * 10;
                buff[0] = Convert.ToByte(itest1 / 256);
                buff[1] = Convert.ToByte(itest1 % 256);
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //set rxp alarm warning 
        public void setrxpaw(int deviceIndex, int deviceAddress, int regAddress, decimal rxpaw)
        {
            byte[] buff = new byte[2];
            Int64 itest1;
            try
            {
                itest1 = Convert.ToInt64(rxpaw) * 10;
                buff[0] = Convert.ToByte(itest1 / 256);
                buff[1] = Convert.ToByte(itest1 % 256);
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        
        //set txdis soft
        public void SetSoftTxDis(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[1];
            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 1);
                buff[0] = (byte)(buff[0] | 0x40);
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);

            }
            catch (Exception error)
            {

                throw error;
            }
        }

        //w/r  sn/pn
        public string ReadSn(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff1 = new byte[16];
            byte[] buff = new byte[5];
            string sn = "ffffffff";
            try
            {

                buff1 = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 16);
                sn = Convert.ToChar(Convert.ToInt64(buff1[0])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[1])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[2])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[3])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[4])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[5])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[6])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[7])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[8])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[9])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[10])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[11])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[12])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[13])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[14])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[15])).ToString();
                return sn.Trim();

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        //read pn
        public string ReadPn(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff1 = new byte[16];
            byte[] buff = new byte[5];
            string pn = "ffffffff";
            try
            {


                buff1 = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 16);
                pn = Convert.ToChar(Convert.ToInt64(buff1[0])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[1])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[2])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[3])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[4])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[5])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[6])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[7])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[8])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[9])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[10])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[11])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[12])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[13])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[14])).ToString() + Convert.ToChar(Convert.ToInt64(buff1[15])).ToString();
                return pn.Trim();

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        //write sn
        public void SetSn(int deviceIndex, int deviceAddress, int regAddress, string sn)
        {
            byte[] buff = new byte[16];
            try
            {


                buff = Encoding.Default.GetBytes(sn);
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);
               

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        //write pn
        public void SetPn(int deviceIndex, int deviceAddress, int regAddress, string pn)
        {
            byte[] buff = new byte[16];
            try
            {


                buff = Encoding.Default.GetBytes(pn.Trim());
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception error)
            {

                throw error;
            }
        }
        //read manufacture data
        //read fwrev
        public string ReadFWRev(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            string fwrev = "ff";

            try
            {
                buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                fwrev = Convert.ToString((buff[0] * 256 + buff[1]), 16);
                return fwrev.Trim();

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        //read adc
        public UInt16 readadc(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] buff = new byte[2];
            UInt16 adc = 0;
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    buff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                    if (buff[0] != 0)
                        break;

                }

                adc = (UInt16)((buff[0]) * 256 + buff[1]);
                return adc;

            }
            catch (Exception error)
            {

                throw error;
            }
        }
       
        //coef ieee754
        public bool floattoieee(int deviceIndex, int deviceAddress, int regAddress, float fcoef)
        {
            bool flag = false;
            byte[] bcoef = new byte[4];
            bcoef = BitConverter.GetBytes(fcoef);
            bcoef.Reverse();
            USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, bcoef);
            System.Threading.Thread.Sleep(200);
            
            for (int i = 0; i < 4; i++)
            {
                byte[] rcoef = new byte[4];
                rcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 4);
                if ((bcoef[0] != rcoef[0]) || (bcoef[1] != rcoef[1]) || (bcoef[2] != rcoef[2]) || (bcoef[3] != rcoef[3]))
                {
                    USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, bcoef);
                    System.Threading.Thread.Sleep(200);
                }
                else
                {
                    flag = true;
                    break;
                }
            
            }
            return flag;
        }
        public float ieeetofloat(int deviceIndex, int deviceAddress, int regAddress)
        {
            float fcoef;
            byte[] bcoef = new byte[4];
            bcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 4);
            System.Threading.Thread.Sleep(200);
            bcoef.Reverse();
            fcoef = BitConverter.ToSingle(bcoef, 0);
            return fcoef;
        }
        public bool u16tobyte(int deviceIndex, int deviceAddress, int regAddress, UInt16 coef)
        {
            bool flag = false;
            byte[] buff = new byte[2];
            buff[0] = (byte)((coef >> 8) & 0xff);
            buff[1] = (byte)(coef & 0xff);
            USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
            System.Threading.Thread.Sleep(200);
            
            for (int i = 0; i < 4; i++)
            {
                byte[] rcoef = new byte[2];
                rcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
                if ((buff[0] != rcoef[0]) || (buff[1] != rcoef[1]))
                {
                    USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                    System.Threading.Thread.Sleep(200);
                }
                else
                {
                    flag = true;
                    break;
                }

            }
            return flag;
        }
        public UInt16 bytetou16(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] bcoef = new byte[2];
            bcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 2);
            System.Threading.Thread.Sleep(200);

            UInt16 U16coef = (UInt16)((bcoef[0] << 8) + bcoef[1]);
            return U16coef;

        }
        public bool u32tobyte(int deviceIndex, int deviceAddress, int regAddress, UInt32 coef)
        {
            bool flag = false;
            byte[] buff = new byte[2];
            buff[0] = (byte)((coef >> 24) & 0xff);
            buff[1] = (byte)((coef >> 16) & 0xff);
            buff[2] = (byte)((coef >> 8) & 0xff);
            buff[3] = (byte)(coef & 0xff);
            USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
            System.Threading.Thread.Sleep(200);
            
            for (int i = 0; i < 4; i++)
            {
                byte[] rcoef = new byte[4];
                rcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 4);
                if ((buff[0] != rcoef[0]) || (buff[1] != rcoef[1]) || (buff[2] != rcoef[2]) || (buff[3] != rcoef[3]))
                {
                    USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                    System.Threading.Thread.Sleep(200);
                }
                else
                {
                    flag = true;
                    break;
                }

            }
            return flag;
        }
        public UInt32 bytetou32(int deviceIndex, int deviceAddress, int regAddress)
        {
            byte[] bcoef = new byte[4];
            bcoef = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 4);
            UInt32 U32coef = (UInt32)((bcoef[0] << 24) + (bcoef[1] << 16) + (bcoef[2] << 8) + bcoef[3]);
            System.Threading.Thread.Sleep(200);
            return U32coef;
        }

        public bool SetCoef(int deviceIndex, int deviceAddress, int regAddress, string coef, byte format)
        {//1 ieee754;2 UInt16;3 UInt32
            bool flag = false;
            try
            {

                switch (format)
                {
                    case 1:
                        float fcoef = Convert.ToSingle(coef);
                        flag = floattoieee(deviceIndex, deviceAddress, regAddress, fcoef);
                        break;
                    case 2:
                        //UInt16 u16coef = Convert.ToUInt16(Convert.ToDouble(coef));
                        UInt16 u16coef = (UInt16)(Convert.ToDouble(coef));
                        flag = u16tobyte(deviceIndex, deviceAddress, regAddress, u16coef);
                        break;
                    case 3:
                        //UInt32 u32coef = Convert.ToUInt32(Convert.ToDouble(coef));
                        UInt32 u32coef = (UInt32)(Convert.ToDouble(coef));
                        flag = u32tobyte(deviceIndex, deviceAddress, regAddress, u32coef);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception error)
            {

                throw error;
            }
            return flag;
        }
        public string ReadCoef(int deviceIndex, int deviceAddress, int regAddress, byte format)
        {//1 ieee754;2 UInt16;3 UInt32
            try
            {
                string strcoef;
                switch (format)
                {
                    case 1:
                        float fcoef = ieeetofloat(deviceIndex, deviceAddress, regAddress);
                        strcoef = fcoef.ToString();
                        break;
                    case 2:
                        UInt16 u16coef = bytetou16(deviceIndex, deviceAddress, regAddress);
                        strcoef = u16coef.ToString();
                        break;
                    case 3:
                        UInt32 u32coef = bytetou32(deviceIndex, deviceAddress, regAddress);
                        strcoef = u32coef.ToString();
                        break;
                    default:
                        strcoef = "";
                        break;
                }

                return strcoef;
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public  byte[] ReadWriteDriver40g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte channel, byte operation, byte chipset, byte[] buffer,bool Switch)
        {
            //database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac
            ////operation 1 READ,2 WRITE,4 store
            byte chipoperation;
            byte[] tempData=new byte[0];
            if (Switch == false)
            {
                tempData = new byte[buffer.Length + 5];
                chipoperation = (byte)((chipset << 4) + operation);
                tempData[0] = chipoperation;
                tempData[1] = (byte)(channel - 1);
                tempData[2] = (byte)((regAddress >> 8) & 0xff);
                tempData[3] = (byte)(regAddress & 0xff);
                tempData[4] = (byte)(buffer.Length);
                buffer.CopyTo(tempData, 5);
            }
            else if (Switch == true)
            {
                tempData = new byte[buffer.Length + 4];
                chipoperation = (byte)((chipset << 4) + operation);
                tempData[0] = chipoperation;
                tempData[1] = (byte)(channel - 1);
                tempData[2] = (byte)(regAddress & 0xff);
                tempData[3] = (byte)(buffer.Length);
                buffer.CopyTo(tempData, 4);
            }
            USBIO.WrtieReg(deviceIndex, deviceAddress, StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, tempData);
            byte[] arrRead = new byte[buffer.Length];
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(50);
                byte[] temp = USBIO.ReadReg(deviceIndex, deviceAddress, StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT,1);
                if (temp[0] == 0)
                {
                    arrRead = USBIO.ReadReg(deviceIndex, deviceAddress, StartAddress + 4, IOPort.SoftHard.HARDWARE_SEQUENT, buffer.Length);
                    break;
                }
            }
            System.Threading.Thread.Sleep(200);
            return arrRead;

        }
        public byte[] ReadWriteDriver10g(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte operation, byte[] buffer)
        {
            //operation 80 write ldd,40 read ldd,20 store ldd
            //08 store dac,04 read dac,02 write dac
            byte[] tempData = new byte[buffer.Length + 4];
            tempData[0] = buffer[0];
            tempData[1] = buffer[1];
            tempData[2] = (byte)regAddress;
            tempData[3] = (operation);
            buffer.CopyTo(tempData, 4);
            USBIO.WrtieReg(deviceIndex, deviceAddress, StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, tempData);
            byte[] arrRead = USBIO.ReadReg(deviceIndex, deviceAddress, StartAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buffer.Length);
            System.Threading.Thread.Sleep(200);
            return arrRead;
        }
        
  

    }
}
