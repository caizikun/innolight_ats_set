using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maintain
{
    public class EEPROMOperation
    {       
        static bool blnMsgresult = false;

        public EEPROMOperation()    //新增一个构造函数
        {
        }
        
        public EEPROMOperation(int INDEX,bool isFristLoad)
        {
            //141016_1 如加上此项将报错...TBD 是否需要连接IIC
            if (isFristLoad)
            {
                System.Windows.Forms.DialogResult Msgresult =
                System.Windows.Forms.MessageBox.Show("Please confirm that the I2C or MIDO board has already been connected?", "Attention:",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);
                if (Msgresult == System.Windows.Forms.DialogResult.Yes)
                {
                    USBIO = new IOPort("USB", INDEX.ToString());
                    deviceIndex = INDEX;
                    blnMsgresult = true;
                }                
            }
            else if ( blnMsgresult)
            {
                USBIO = new IOPort("USB", INDEX.ToString());
                deviceIndex = INDEX;
            } 
        }

        public int deviceIndex = -1;
        public int Data0length, Data1length, Data2length, Data3length;
        public int Data0FristIndex, Data1FristIndex, Data2FristIndex, Data3FristIndex;
        public string Data0Name, Data1Name, Data2Name, Data3Name;
        protected IOPort USBIO ;

        public byte[] Data0;
        public byte[] Data1;
        public byte[] Data2;
        public byte[] Data3;

        public byte[] ReadData0;
        public byte[] ReadData1;
        public byte[] ReadData2;
        public byte[] ReadData3;
                
        virtual protected bool EngMode(byte pageNo)
        {
            return false;
        }

        virtual public bool EEPROMRead()
        {
            return false;
        }

        virtual public bool EEPROMWrite()
        {
            return false;
        }
        virtual public string CurrItemDescription(byte[] objValues, string page, byte Address, byte addrValue, out string itemName, out string addressAllDescription)
        {
            itemName = "";
            addressAllDescription = "";
            return "";
        }

        #region 实例方法[执行读/写]
        //w/r  sn/pn
        public string ReadSn(int deviceAddress, int regAddress)
        {
            byte[] buff1 = new byte[16];
            try
            {
                buff1 = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 16);                
                string tempSN = "";
                for (int i = 0; i < 16; i++)
                {
                    tempSN += Convert.ToChar(Convert.ToByte(buff1[i])).ToString();
                }
                return tempSN.Trim();

            }
            catch (Exception error)
            {
                return "";
                throw error;
            }
        }
        //read pn
        public string ReadPn(int deviceAddress, int regAddress)
        {
            byte[] buff1 = new byte[16];
            try
            {
                buff1 = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, 16);
                string tempPN = "";
                for (int i = 0; i < 16; i++)
                {
                    tempPN += Convert.ToChar(Convert.ToByte(buff1[i])).ToString();
                }
                return tempPN.Trim();

            }
            catch (Exception error)
            {
                return "";
                throw error;
            }
        }
        
        //write sn
        public bool SetSn(int deviceAddress, int regAddress, string sn)
        {
            byte[] buff = new byte[16];
            try
            {
                buff = Encoding.Default.GetBytes(sn.Trim().PadRight(16, (char)0x20));
                USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(120);
                return true;
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.ToString());
                return false;
            }
        }


        public bool ReadBytes(int deviceAddress, int regAddress, int length,out byte[] readData )
        {
            byte[] buff1 = new byte[length];
            try
            {
                if (length > 256)
                {
                    System.Windows.Forms.MessageBox.Show("侦测到 length >256: 系统将自动将设置 length为 256");
                    length = 256;
                }
                else if (regAddress + length > 256)
                {

                    System.Windows.Forms.MessageBox.Show("侦测到 regAddress + length > 256:(regAddress=" + regAddress + ";length=" + length +
                        ") \n 系统将自动将设置 length为 " + length);
                    length = 256 - regAddress;
                }
                int totaltimes = 0;
                int lastTimeCount = 0;
                if (length % 16 == 0)
                {
                    totaltimes = length / 16;
                    lastTimeCount = 0;
                }
                else
                {
                    totaltimes = length / 16 + 1;
                    lastTimeCount = length % 16;    // totaltimes * 16 - (regAddress + length);
                }

                for (int i = 0; i < totaltimes; i++)
                {
                    if (lastTimeCount == 0)
                    {
                        byte[] tempBuff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress + i * 16, IOPort.SoftHard.HARDWARE_SEQUENT, 16);

                        for (int j = 0; j < 16; j++)
                        {
                            buff1[i * 16 + j] = tempBuff[j];
                        }
                    }
                    else
                    {
                        if (i + 1 < totaltimes)
                        {
                            byte[] tempBuff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress + i * 16, IOPort.SoftHard.HARDWARE_SEQUENT, 16);

                            for (int j = 0; j < 16; j++)
                            {
                                buff1[i * 16 + j] = tempBuff[j];
                            }
                        }
                        else
                        {
                            byte[] tempBuff = USBIO.ReadReg(deviceIndex, deviceAddress, regAddress + i * 16, IOPort.SoftHard.HARDWARE_SEQUENT, lastTimeCount);

                            for (int j = 0; j < lastTimeCount; j++)
                            {
                                buff1[i * 16 + j] = tempBuff[j];
                            }
                        } 
                    }
                    System.Threading.Thread.Sleep(60);
                }
                readData = buff1;
                return true;
            }
            catch 
            {
                readData = buff1;
                return false;
            }
        }

        public bool WriteBytes(int deviceAddress, int regAddress, int length, byte[] writeData)
        {
            try
            {
                if (length > 256)
                {
                    System.Windows.Forms.MessageBox.Show("侦测到 length >256: 系统将自动将设置 length为 256");
                    length = 256;
                }
                else if (regAddress + length > 256)
                {
                    System.Windows.Forms.MessageBox.Show("侦测到 regAddress + length > 256:(regAddress=" + regAddress + ";length=" + length +
                        ") \n 系统将自动将设置 length为 " + length);
                    length = 256 - regAddress;
                }
                int totaltimes = 0;
                int lastTimeCount = 0;
                if (length % 16 == 0)
                {
                    totaltimes = length / 16;
                    lastTimeCount = 0;
                }
                else
                {
                    totaltimes = length / 16 + 1;
                    lastTimeCount = length % 16;  //totaltimes * 16 - (regAddress + length);
                }
                for (int i = 0; i < totaltimes; i++)
                {
                    byte[] tempBuff = new byte[16];
                    //byte[] ReadBuff = new byte[16];
                    if (lastTimeCount == 0)
                    {
                        for (int j = 0; j < 16; j++)
                        {
                            tempBuff[j] = writeData[i * 16 + j];
                        }
                        USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress + i * 16, IOPort.SoftHard.HARDWARE_SEQUENT, tempBuff);
                    }
                    else
                    {
                        if (i + 1 < totaltimes)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                tempBuff[j] = writeData[i * 16 + j];
                            }
                            USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress + i * 16, IOPort.SoftHard.HARDWARE_SEQUENT, tempBuff);
                        }
                        else
                        {
                            byte[] lastBuff = new byte[lastTimeCount];
                            //ReadBuff = new byte[lastTimeCount];

                            for (int j = 0; j < lastTimeCount; j++)
                            {
                                lastBuff[j] = writeData[i * 16 + j];
                            }
                            USBIO.WrtieReg(deviceIndex, deviceAddress, regAddress + i * 16, IOPort.SoftHard.HARDWARE_SEQUENT, lastBuff);
                        }
                    }
                    //System.Threading.Thread.Sleep(60);
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return false;
            }
        }
        #endregion
    }

    public class QSFP : EEPROMOperation
    {
        public QSFP(int INDEX,bool isFristLoad) : base(INDEX,isFristLoad) { }
        public QSFP() : base() { }       
        
        override protected bool EngMode(byte pageNo)
        {
            try
            {
                byte[] buff = new byte[5];
                buff[0] = 0xca;
                buff[1] = 0x2d;
                buff[2] = 0x81;
                buff[3] = 0x5f;
                buff[4] = pageNo;
                USBIO.WrtieReg(deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(100);
                return true;
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.ToString());
                return false;
            }
        }

        override public bool EEPROMWrite()
        {
            if (writeA0HPage0() && writeA0HPage1() && writeA0HPage2() && writeA0HPage3())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool writeA0HPage3()
        {
            EngMode(3);
            if (WriteBytes(0xa0, 128, 96, Data3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool writeA0HPage2()
        {
            EngMode(2);
            if (WriteBytes(0xa0, 128, 128, Data2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool writeA0HPage1()
        {
            EngMode(1);
            if (WriteBytes(0xa0, 128, 128, Data1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool writeA0HPage0()
        {
            EngMode(0);
            if (WriteBytes(0xa0, 128, 128, Data0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool readA0HPage0()
        {
            byte  [] buff=new byte [1]{0};
            WriteBytes(0xa0, 0x7f, 1, buff);
            
            if (ReadBytes(0xa0, 128, 128, out ReadData0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool readA0HPage1()
        {
            byte[] buff = new byte[1] { 1 };
            WriteBytes(0xa0, 0x7f, 1, buff);

            if (ReadBytes(0xa0, 128, 128, out ReadData1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool readA0HPage2()
        {
            byte[] buff = new byte[1] { 2 };
            WriteBytes(0xa0, 0x7f, 1, buff);

            if (ReadBytes(0xa0, 128, 128, out ReadData2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool readA0HPage3()
        {
            byte[] buff = new byte[1] { 3 };
            WriteBytes(0xa0, 0x7f, 1, buff);
            if (ReadBytes(0xa0, 128, 128, out ReadData3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        override public bool EEPROMRead()
        {
            if (readA0HPage0() && readA0HPage1() && readA0HPage2() && readA0HPage3())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //QSFP:DATA0->A0H_Page0,DATA1->A0H_Page3
        public enum QSFPPages
        {
            A0H_Page0 = 0, A0H_Page1 = 1, A0H_Page2 = 2, A0H_Page3 = 3
        }

        override public string CurrItemDescription(byte[] objValues, string page, byte Address, byte addrValue, out string itemName, out string addressAllDescription)
        {
            string currItemDescription = "";
            itemName = "";
            addressAllDescription = "";

            if (page.ToUpper() == QSFPPages.A0H_Page0.ToString().ToUpper())
            {
                //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page0的地址 是 从128开始的
                #region  Upper Page 00h
                if (Address == 0x80)
                {
                    #region Byte Address = 128

                    itemName = "Identifier";
                    addressAllDescription = "Identifier Type of free side device";

                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "Unknown or unspecified";
                            break;
                        case 0x01:
                            currItemDescription = "GBIC";
                            break;
                        case 0x02:
                            currItemDescription = "Module/connector soldered to motherboard";
                            break;
                        case 0x03:
                            currItemDescription = "SFP";
                            break;
                        case 0x04:
                            currItemDescription = "300 pin XBI";
                            break;
                        case 0x05:
                            currItemDescription = "XENPAK";
                            break;
                        case 0x06:
                            currItemDescription = "XFP";
                            break;
                        case 0x07:
                            currItemDescription = "XFF";
                            break;
                        case 0x08:
                            currItemDescription = "XFP-E";
                            break;
                        case 0x09:
                            currItemDescription = "XPAK";
                            break;
                        case 0x0A:
                            currItemDescription = "X2";
                            break;
                        case 0x0B:
                            currItemDescription = "DWDM-SFP";
                            break;
                        //QSFP---------------------------------
                        case 0x0C:
                            currItemDescription = "QSFP";
                            break;
                        case 0x0D:
                            currItemDescription = "QSFP+";
                            break;
                        default:
                            currItemDescription = "ERROR";
                            break;
                    }
                    #endregion
                }
                else if (Address == 0x81)
                {
                    #region Byte Address = 129
                    //141023_0 以 '与'的方式来做好像有问题! TBD
                    itemName = "Ext. Identifier";
                    addressAllDescription = "Extended Identifier of free side device. "
                        + "Includes power classes, CLEI codes, CDR capability";

                    switch (Convert.ToInt32(Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 2), 2))
                    {
                        case 0:
                            currItemDescription = "Power Class 1 (1.5 W max. )";
                            break;
                        case 1:
                            currItemDescription = "Power Class 2 (2.0 W max. )";
                            break;
                        case 2:
                            currItemDescription = "Power Class 3 (2.5 W max. )";
                            break;
                        case 3:
                            currItemDescription = "Power Class 4 (3.5 W max. )";
                            break;
                    }

                    switch (Convert.ToInt32(Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 2), 2))
                    {
                        case 0:
                            currItemDescription += "; unused (legacy setting)";
                            break;
                        case 1:
                            currItemDescription = "Power Class 5 (4.0 W max. )";
                            break;
                        case 2:
                            currItemDescription = "Power Class 6 (4.5 W max. )";
                            break;
                        case 3:
                            currItemDescription = "Power Class 7 (5.0 W max. )";
                            break;
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription += "; CLEI code present in Page 02h";
                    }
                    else
                    {
                        currItemDescription += "; No CLEI code present in Page 02h";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription += "; CDR present in TX";
                    }
                    else
                    {
                        currItemDescription += "; No CDR in TX";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += "; CDR present in RX";
                    }
                    else
                    {
                        currItemDescription += "; No CDR in RX";
                    }
                    #endregion
                }
                else if (Address == 0x82)
                {
                    #region Byte Address = 130
                    itemName = "Connector, Media";
                    addressAllDescription = "Code for media connector type";

                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "Unknown or unspecified";
                            break;
                        case 0x01:
                            currItemDescription = "SC";
                            break;
                        case 0x02:
                            currItemDescription = "FC Style 1 copper connector";
                            break;
                        case 0x03:
                            currItemDescription = "FC Style 2 copper connector";
                            break;
                        case 0x04:
                            currItemDescription = "BNC/TNC";
                            break;
                        case 0x05:
                            currItemDescription = "FC coax headers";
                            break;
                        case 0x06:
                            currItemDescription = "Fiberjack";
                            break;
                        case 0x07:
                            currItemDescription = "LC";
                            break;
                        case 0x08:
                            currItemDescription = "MT-RJ";
                            break;
                        case 0x09:
                            currItemDescription = "MU";
                            break;
                        case 0x0A:
                            currItemDescription = "SG";
                            break;
                        case 0x0B:
                            currItemDescription = "Optical Pigtail";
                            break;
                        case 0x0C:
                            currItemDescription = "MPO";
                            break;
                        case 0x20:
                            currItemDescription = "HSSDC II";
                            break;
                        case 0x21:
                            currItemDescription = "Copper pigtail";
                            break;
                        case 0x22:
                            currItemDescription = "RJ45";
                            break;
                        case 0x23:
                            currItemDescription = "No separable connector";
                            break;
                        default:
                            currItemDescription = "Error";
                            break;
                    }
                    #endregion
                }
                else if (Address == 0x83)
                {
                    #region Byte Address = 131

                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription = "Extended";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription = "10GBASE-LRM";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription = "10GBASE-LR";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription = "10GBASE-SR";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription = "40GBASE-CR4";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription = "40GBASE-SR4";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription = "40GBASE-LR4";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription = "40G Active Cable (XLPPI)";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }

                else if (Address == 0x84)
                {
                    #region Byte Address = 132
                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription = "OC 48, long reach";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription = "OC 48, intermediate reach";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription = "OC 48 short reach";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }

                else if (Address == 0x85)
                {
                    #region Byte Address = 133
                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription = "Reserved SAS";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription = "SAS 12.0 Gbps";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription = "SAS 6.0 Gbps";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription = "SAS 3.0 Gbps";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }

                else if (Address == 0x86)
                {
                    #region Byte Address = 134
                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription = "1000BASE-T";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription = "1000BASE-CX";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription = "1000BASE-LX";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription = "1000BASE-SX";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }

                else if (Address == 0x87)
                {
                    #region Byte Address = 135
                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription = "Very long distance (V)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription = "Short distance (S)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription = "Intermediate distance (I)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription = "Long distance (L)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription = "Medium (M)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription = "Reserved";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription = "Longwave laser (LC)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription = "Electrical inter-enclosure (EL)";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }

                else if (Address == 0x88)
                {
                    #region Byte Address = 136
                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription = "Electrical intra-enclosure";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription = "Shortwave laser w/o OFC (SN)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription = "Shortwave laser w OFC (SL)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription = "Longwave Laser (LL)";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }
                else if (Address == 0x89)
                {
                    #region Byte Address = 137
                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription = "Twin Axial Pair (TW)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription = "Shielded Twisted Pair (TP)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription = "Miniature Coax (MI)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription = "Video Coax (TV)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription = "Multi-mode 62.5 m (M6)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription = "Multi-mode 50 m (M5)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription = "Multi-mode 50 um (OM3)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription = "Single Mode (SM)";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }

                else if (Address == 0x8a)
                {
                    #region Byte Address = 138
                    itemName = "Specification Compliance";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription = "1200 MBps (per channel)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription = "800 MBps";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription = "1600 MBps (per channel)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription = "400 MBps";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription = "3200 MBps (per channel)";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription = "200 MBps";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription = "Extended";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription = "100 MBps";
                    }
                    else
                    {
                        currItemDescription = "";
                    }
                    #endregion
                }
                else if (Address == 0x8b)
                {
                    #region Byte Address = 139
                    //00h Unspecified
                    //01h 8B10B
                    //02h 4B5B
                    //03h NRZ
                    //04h SONET Scrambled
                    //05h 64B66B
                    //06h Manchester
                    //-FFh Reserved
                    itemName = "Encoding";
                    addressAllDescription = "Code for serial encoding algorithm.";

                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "Unspecified";
                            break;
                        case 0x01:
                            currItemDescription = "8B10B";
                            break;
                        case 0x02:
                            currItemDescription = "4B5B";
                            break;
                        case 0x03:
                            currItemDescription = "NRZ";
                            break;
                        case 0x04:
                            currItemDescription = "SONET Scrambled";
                            break;
                        case 0x05:
                            currItemDescription = "64B66B";
                            break;
                        case 0x06:
                            currItemDescription = "Manchester";
                            break;
                        default:
                            currItemDescription = "Error";
                            break;
                    }
                    #endregion
                }

                else if (Address == 0x8c)
                {
                    #region Byte Address = 140
                    itemName = "BR, nominal";
                    addressAllDescription = "Nominal bit rate, units of 100 Mbps."
                        + " For BR > 25.4G, set this to FFh and use Byte 222.";

                    if (addrValue == 0x00)
                    {
                        currItemDescription = "the bit rate is not specified and must be determined from the Module technology";
                    }
                    else if (addrValue == 0xFF)
                    {
                        currItemDescription = "the bit rate exceeds 25.4Gb/s and Byte 222 must be used to determine nominal bit rate";
                    }
                    else
                    {
                        currItemDescription = addrValue.ToString() + "00Mbps";
                    }
                    #endregion
                }

                else if (Address == 0x8d)
                {
                    #region Byte Address = 141
                    itemName = "Extended RateSelect Compliance";
                    addressAllDescription = "Tags for extended rate select compliance";

                    // 141119,TBD
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription = "QSFP+ Rate Select Version 2.";
                    }
                    else if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription = "QSFP+ Rate Select Version 1.";
                    }
                    else
                    {
                        currItemDescription = "Not applicable";
                    }
                    #endregion
                }

                else if (Address == 0x8e)
                {
                    #region Byte Address = 142
                    itemName = "Length(SMF)";
                    addressAllDescription = "Link length supported for SMF fiber in km";

                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else
                    {
                        currItemDescription = addrValue.ToString() + "km";
                    }
                    #endregion
                }

                else if (Address == 0x8f)
                {
                    #region Byte Address = 143
                    itemName = "Length(OM3 50 um)";
                    addressAllDescription = "Link length supported for EBW 50/125 um fiber (OM3), units of 2 m";

                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else
                    {
                        currItemDescription = (2 * addrValue).ToString() + "m";
                    }
                    #endregion
                }

                else if (Address == 0x90)
                {
                    #region Byte Address = 144
                    itemName = "Length(OM2 50 um)";
                    addressAllDescription = "Link length supported for 50/125 um fiber (OM2), units of 1 m";

                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else
                    {
                        currItemDescription = addrValue.ToString() + "m";
                    }
                    #endregion
                }
                else if (Address == 0x91)
                {
                    #region Byte Address = 145
                    itemName = "Length(OM1 62.5 um)";
                    addressAllDescription = "Link length supported for 62.5/125 um fiber (OM1), units of 1 m";

                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else
                    {
                        currItemDescription = addrValue.ToString() + "m";
                    }
                    #endregion
                }

                else if (Address == 0x92)
                {
                    #region Byte Address = 146
                    itemName = "Length(Passive copper or active cable or OM4 50 um)";
                    addressAllDescription = "Link length supported for passive "
                        + "or active cable assembly (units of 1 m) "
                        + "or OM4 50/125um fiber (units of 2 m) as indicated by byte 147.";

                    // TBD,141119
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        //currItemDescription = "Link Length(cooper) more than 255m";
                        currItemDescription = "the VCSEL free side device supports a link length greater than 508 meters or the Cable assembly has a link length greater than 254 meters.";
                    }
                    else
                    {
                        currItemDescription = "For (OM4):" + (addrValue * 2).ToString() + "m; \n For Cable assembly (copper or AOC)" + (addrValue).ToString() + "m";
                    }
                    #endregion
                }

                else if (Address == 0x93)
                {
                    #region Byte Address = 147
                    itemName = "Device tech";
                    addressAllDescription = "Device technology";

                    // TRANSMITTER TECHNOLOGY (PAGE 00, BYTE 147, BITS 7-4) //有问题141023_0
                    // 修正问题141023_0，修改人albert，2014-10-23 10:23:45
                    // 思路：
                    // 1、将addrValue转化为8位（不足8位需在左边补上0)二进制，提取指定位的二进制数值
                    // 2、再将提取的二进制数值转换为十进制与协议标准进行比较
                    switch (Convert.ToInt32(Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 4), 2))
                    {
                        case 0:
                            currItemDescription = "850 nm VCSEL";
                            break;
                        case 1:
                            currItemDescription = "1310 nm VCSEL";
                            break;
                        case 2:
                            currItemDescription = "1550 nm VCSEL";
                            break;
                        case 3:
                            currItemDescription = "1310 nm FP";
                            break;
                        case 4:
                            currItemDescription = "1310 nm DFB";
                            break;
                        case 5:
                            currItemDescription = "1550 nm DFB";
                            break;
                        case 6:
                            currItemDescription = "1310 nm EML";
                            break;
                        case 7:
                            currItemDescription = "1550 nm EML";
                            break;
                        case 8:
                            currItemDescription = "Others";
                            break;
                        case 9:
                            currItemDescription = "1490 nm DFB";
                            break;
                        case 10:
                            currItemDescription = "Copper cable unequalized";
                            break;
                        case 11:
                            currItemDescription = "Copper cable passive equalized";
                            break;
                        case 12:
                            currItemDescription = "Copper cable, near and far end limiting active equalizers";
                            break;
                        case 13:
                            currItemDescription = "Copper cable, far end limiting active equalizers";
                            break;
                        case 14:
                            currItemDescription = "Copper cable, near end limiting active equalizers";
                            break;
                        case 15:
                            currItemDescription = "Copper cable, linear active equalizers";
                            break;
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription += "; Active wavelength control";
                    }
                    else
                    {
                        currItemDescription += "; No wavelength control";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += "; Cooled transmitter";
                    }
                    else
                    {
                        currItemDescription += "; Uncooled transmitter device";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription += "; APD detector";
                    }
                    else
                    {
                        currItemDescription += "; Pin detector";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription += "; Transmitter tuneable";
                    }
                    else
                    {
                        currItemDescription += "; Transmitter not tuneable";
                    }
                    #endregion
                }

                else if (Address >= 0x94 && Address <= 0xa3)
                {
                    #region Byte Address = 148-163
                    itemName = "Vendor name";
                    addressAllDescription = "Free side device vendor name (ASCII)";
                    currItemDescription = ((char)addrValue).ToString();
                    #endregion
                }

                else if (Address == 0xa4)
                {
                    #region Byte Address = 164
                    itemName = "Extended Module";
                    addressAllDescription = "Extended Module codes for InfiniBand";

                    currItemDescription = "";

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription += "EDR ";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription += "FDR ";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += "QDR ";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription += "DDR ";
                    }

                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription += "SDR ";
                    }
                    #endregion
                }

                // 2014-10-16，165-167缺一个将十进制转换为16进制                
                else if (Address >= 0xa5 && Address <= 0xa7)
                {
                    #region Byte Address = 165-167
                    itemName = "Vendor OUI";
                    addressAllDescription = "Free side device vendor IEEE company ID";
                    currItemDescription = addrValue.ToString("X").PadLeft(2, '0');  //141024_0
                    #endregion
                }

                else if (Address >= 0xa8 && Address <= 0xb7)
                {
                    #region Byte Address = 168-183
                    itemName = "Vendor PN";
                    addressAllDescription = "Part number provided by free side device vendor(ASCII)";
                    currItemDescription = ((char)addrValue).ToString();
                    #endregion
                }

                else if (Address >= 0xb8 && Address <= 0xb9)
                {
                    #region Byte Address = 184-185
                    itemName = "Vendor rev";
                    addressAllDescription = "Revision level for part number provided by vendor(ASCII)";
                    currItemDescription = ((char)addrValue).ToString();
                    #endregion
                }

                // 186-187波长计算，代码未完成
                else if (Address >= 0xba && Address <= 0xbb)
                {
                    #region Byte Address = 186 ,187
                    itemName = "Wavelength or Copper Cable Attenuation";
                    addressAllDescription = "Nominal laser wavelength (wavelength=value/20 in nm) "
                        + "or copper cable attenuation in dB at 2.5 GHz (Adrs 186) and 5.0 GHz (Adrs 187)";
                    currItemDescription = ((objValues[186 - 0x80] * 256 + objValues[187 - 0x80]) / 20).ToString() + "nm"; //141023_0
                    if (Address == 0xba)
                    {
                        currItemDescription +=
                        "; For copper cable: the copper cable attenuation at 2.5 GHz in" + addrValue.ToString() + "dB";
                    }
                    else if (Address == 0xbb)
                    {
                        currItemDescription +=
                        "; For copper cable: the copper cable attenuation at 5 GHz in" + addrValue.ToString() + "dB";
                    }
                    #endregion
                }
                else if (Address >= 0xbc && Address <= 0xbd)
                {
                    #region Byte Address = 188,189
                    itemName = "Wavelength tolerance or Copper Cable Attenuation";
                    addressAllDescription = "Guaranteed range of laser wavelength(+/- value) "
                        + "from nominal wavelength.(wavelength Tol.=value/200 in nm) or "
                        + "copper cable attenuation in dB at 7.0 GHz (Adrs 188) and 12GHz (Adrs 189)";
                    currItemDescription = "Wavelength Tolerance (±" + ((objValues[188 - 0x80] * 256 + objValues[189 - 0x80]) / (float)200) + "nm)";

                    if (Address == 0xbc)
                    {
                        currItemDescription +=
                        "; For copper cable: the copper cable attenuation at 7.0 GHz in" + addrValue.ToString() + "dB";
                    }
                    else if (Address == 0xbd)
                    {
                        currItemDescription +=
                        "; For copper cable: the copper cable attenuation at 12.9 GHz in" + addrValue.ToString() + "dB";
                    }
                    #endregion
                }
                else if (Address == 0xbe)
                {
                    #region Byte Address = 190
                    itemName = "Max case temp.";
                    addressAllDescription = "Maximum case temperature in degrees C";
                    currItemDescription = "Max Case Temp " + addrValue.ToString() + "°C";
                    #endregion
                }
                else if (Address == 0xbf)
                {
                    #region Byte Address = 191
                    itemName = "CC_BASE";
                    addressAllDescription = "Check code for base ID fields (bytes 128-190)";
                    currItemDescription = "CS=" + addrValue.ToString("X");
                    #endregion
                }
                else if (Address == 0xc0)
                {
                    #region Byte Address = 192
                    itemName = "Link codes";
                    addressAllDescription = "Extended Specification Compliance Codes";
                    currItemDescription = "The Extended Specification Compliance Codes";
                    #endregion
                }

                else if (Address == 0xc1)
                {
                    #region Byte Address = 193
                    itemName = "Options";
                    addressAllDescription = "Rate Select, TX Disable, TX Fault, LOS, Warning indicators "
                        + "for: Temperature, VCC, RX power, TX Bias, TX EQ, Adaptive TX EQ, "
                        + "RX EMPH, CDR Bypass, CDR LOL Flag.";

                    // bit 7-3
                    currItemDescription = "";

                    // bit 3
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription += "TX Input Equalization Auto Adaptive Capable implemented";
                    }
                    else
                    {
                        currItemDescription += "TX Input Equalization Auto Adaptive Capable not implemented";
                    }

                    // bit 2
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += ",TX Input Equalization Fixed Programmable Settings implemented";
                    }
                    else
                    {
                        currItemDescription += ",TX Input Equalization Fixed Programmable Settings not implemented";
                    }

                    // bit 1
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription += ",RX Output Emphasis Fixed Programmable Settings implemented";
                    }
                    else
                    {
                        currItemDescription += ",RX Output Emphasis Fixed Programmable Settings not implemented";
                    }

                    // bit 0
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription += ",RX Output Amplitude Fixed Programmable Settings implemented";
                    }
                    else
                    {
                        currItemDescription += ",RX Output Amplitude Fixed Programmable Settings not implemented";
                    }
                    #endregion

                }

                else if (Address == 0xc2)
                {
                    #region Byte Address = 194
                    itemName = "Options";
                    addressAllDescription = "Rate Select, TX Disable, TX Fault, LOS, Warning indicators "
                        + "for: Temperature, VCC, RX power, TX Bias, TX EQ, Adaptive TX EQ, "
                        + "RX EMPH, CDR Bypass, CDR LOL Flag.";

                    currItemDescription = "";

                    // bit 7
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription += "TX CDR On/Off Control implemented, controllable.";
                    }
                    else
                    {
                        currItemDescription += "TX CDR On/Off Control implemented, fixed.";
                    }

                    // bit 6
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription += " RX CDR On/Off Control implemented, controllable.";
                    }
                    else
                    {
                        currItemDescription += " RX CDR On/Off Control implemented, fixed.";
                    }

                    // bit 5
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription += " Tx CDR Loss of Lock (LOL) Flag implemented.";
                    }
                    else
                    {
                        currItemDescription += " Tx CDR Loss of Lock (LOL) Flag not implemented.";
                    }

                    // bit 4
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription += " Rx CDR Loss of Lock (LOL) Flag implemented.";
                    }
                    else
                    {
                        currItemDescription += " Rx CDR Loss of Lock (LOL) Flag not implemented.";
                    }

                    // bit 3
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription += " Rx Squelch Disable implemented.";
                    }
                    else
                    {
                        currItemDescription += " Rx Squelch Disable not implemented.";
                    }

                    // bit 2
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += " Rx Output Disable capable implemented.";
                    }
                    else
                    {
                        currItemDescription += " Rx Output Disable capable not implemented.";
                    }

                    // bit 1
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription += " Tx Squelch Disable implemented.";
                    }
                    else
                    {
                        currItemDescription += " Tx Squelch Disable not implemented.";
                    }

                    // bit 0
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(7, 1) == "1")
                    {
                        currItemDescription += " Tx Squelch implemented.";
                    }
                    else
                    {
                        currItemDescription += " Tx Squelch not implemented.";
                    }
                    #endregion
                }
                else if (Address == 0xc3)
                {
                    #region Byte Address = 195
                    itemName = "Options";
                    addressAllDescription = "Rate Select, TX Disable, TX Fault, LOS, Warning indicators "
                        + "for: Temperature, VCC, RX power, TX Bias, TX EQ, Adaptive TX EQ, "
                        + "RX EMPH, CDR Bypass, CDR LOL Flag.";

                    // bit 7
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(0, 1) == "1")
                    {
                        currItemDescription = "Memory page 02 provided implemented.";
                    }
                    else
                    {
                        currItemDescription = "Memory page 02 provided not implemented.";
                    }

                    // bit 6
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(1, 1) == "1")
                    {
                        currItemDescription += " Memory Page 01h provided implemented.";
                    }
                    else
                    {
                        currItemDescription += " Memory Page 01h provided not implemented.";
                    }

                    // bit 5
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(2, 1) == "1")
                    {
                        currItemDescription += " RATE_SELECT is implemented,"
                            + " active control of the select bits in the upper memory table is required to change rates";
                    }
                    else
                    {
                        currItemDescription += " RATE_SELECT is implemented,"
                            + " no control of the rate select bits in the upper memory table is required.";
                    }

                    // bit 4
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(3, 1) == "1")
                    {
                        currItemDescription += " Tx_DISABLE is implemented and disables the serial output.";
                    }
                    else
                    {
                        currItemDescription += " Tx_DISABLE is not implemented.";
                    }

                    // bit 3
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription += " Tx_FAULT signal implemented";
                    }
                    else
                    {
                        currItemDescription += " Tx_FAULT signal not implemented";
                    }

                    // bit 2
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += " Tx Squelch implemented to reduce Pave.";
                    }
                    else
                    {
                        currItemDescription += " Tx Squelch implemented to reduce OMA.";
                    }

                    // bit 1
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(6, 1) == "1")
                    {
                        currItemDescription += " Tx Loss of Signal implemented";
                    }
                    else
                    {
                        currItemDescription += " Tx Loss of Signal not implemented";
                    }

                    // bit 0, Reserved
                    #endregion
                }

                else if (Address >= 0xc4 && Address <= 0xd3)
                {
                    #region Byte Address = 196-211
                    itemName = "Vendor SN";
                    addressAllDescription = "Serial number provided by vendor (ASCII)";
                    currItemDescription = ((char)addrValue).ToString();
                    #endregion
                }

                else if (Address >= 0xd4 && Address <= 0xdb)
                {
                    #region Byte Address =  212-219
                    itemName = "Date Code";
                    addressAllDescription = "Vendor's manufacturing date code";
                    currItemDescription = ((char)addrValue).ToString();
                    #endregion
                }
                else if (Address == 0xdc)
                {
                    #region Byte Address = 220
                    itemName = "Diagnostic Monitoring Type";
                    addressAllDescription = "Indicates which type of diagnostic monitoring is"
                        + " implemented (if any) in the free side device. Bit 7-4,1,0 Reserved.";

                    // bit 3
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription = "Received power measurements type: Average Power, ";
                    }
                    else
                    {
                        currItemDescription = "Received power measurements type: OMA, ";
                    }

                    // bit 2
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += "Transmitter power measurement supported.";
                    }
                    else
                    {
                        currItemDescription += "Transmitter power measurement not supported.";
                    }
                    #endregion
                }

                else if (Address == 0xdd)
                {
                    #region Byte Address = 221
                    itemName = "Enhanced Options";
                    addressAllDescription = "Indicates which optional enhanced features are implemented in the free side device.";

                    // bit 3
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(4, 1) == "1")
                    {
                        currItemDescription = "rate selection is implemented using extended rate selection, ";
                    }
                    else
                    {
                        currItemDescription = "the free side device does not support rate selection, ";
                    }

                    // bit 2
                    if (Convert.ToString(addrValue, 2).PadLeft(8, '0').Substring(5, 1) == "1")
                    {
                        currItemDescription += "the free side device supports rate selection using application select table mechanism.";
                    }
                    else
                    {
                        currItemDescription += "the free side device does not support application select and Page 01h does not exist.";
                    }
                    #endregion
                }
                else if (Address == 0xde)
                {
                    #region Byte Address = 222
                    itemName = "BR, nominal";
                    addressAllDescription = "Nominal bit rate per channel, units of 250 Mbps. Complements Byte 140.";

                    currItemDescription = (250 * addrValue).ToString() + "Mbps";
                    #endregion
                }

                else if (Address == 0xdf)
                {
                    #region Byte Address = 223
                    itemName = "CC_EXT";
                    addressAllDescription = "Check code for the Extended ID Fields (bytes 192-222)";
                    currItemDescription = "CS2=" + addrValue.ToString("X");
                    #endregion
                }

                else if (Address >= 0xe0 && Address <= 0xff)
                {
                    #region Byte Address = 224-255
                    itemName = "Vendor Specific";
                    addressAllDescription = "Vendor Specific EEPROM";
                    currItemDescription = (addrValue).ToString("X");
                    #endregion
                }

                #endregion
            }
            else if (page.ToUpper() == QSFPPages.A0H_Page3.ToString().ToUpper())
            {
                //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page3的地址 是 从128开始的
                #region Page 03h (Cable Assemblies)
                float mytemp;
                switch (Address)
                {
                    #region Byte Address = 128
                    case 128:
                        itemName = "Temp High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x80 - 0x80] + objValues[0x81 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 129
                    case 129:
                        itemName = "Temp High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x80 - 0x80] + objValues[0x81 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 130
                    case 130:
                        itemName = "Temp Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x82 - 0x80] + objValues[0x83 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 131
                    case 131:
                        itemName = "Temp Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x82 - 0x80] + objValues[0x83 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 132
                    case 132:
                        itemName = "Temp High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x84 - 0x80] + objValues[0x85 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 133
                    case 133:
                        itemName = "Temp High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x84 - 0x80] + objValues[0x85 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 134
                    case 134:
                        itemName = "Temp Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x86 - 0x80] + objValues[0x87 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 135
                    case 135:
                        itemName = "Temp Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        mytemp = objValues[0x86 - 0x80] + objValues[0x87 - 0x80] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";
                        break;
                    #endregion

                    #region Byte Address = 136-143
                    case 136:
                    case 137:
                    case 138:
                    case 139:
                    case 140:
                    case 141:
                    case 142:
                    case 143:
                        itemName = "Reserved";
                        break;
                    #endregion

                    #region Byte Address = 144
                    case 144:
                        itemName = "Vcc High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[144 - 0x80] * 256 + objValues[145 - 0x80]) * 0.0001).ToString("F4") + " V";
                        break;
                    #endregion

                    #region Byte Address = 145
                    case 145:
                        itemName = "Vcc High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[144 - 0x80] * 256 + objValues[145 - 0x80]) * 0.0001).ToString("F4") + " V";

                        break;
                    #endregion

                    #region Byte Address = 146
                    case 146:
                        itemName = "Vcc Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[146 - 0x80] * 256 + objValues[147 - 0x80]) * 0.0001).ToString("F4") + " V";

                        break;
                    #endregion

                    #region Byte Address = 147
                    case 147:
                        itemName = "Vcc Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[146 - 0x80] * 256 + objValues[147 - 0x80]) * 0.0001).ToString("F4") + " V";

                        break;
                    #endregion

                    #region Byte Address = 148
                    case 148:
                        itemName = "Vcc High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[148 - 0x80] * 256 + objValues[149 - 0x80]) * 0.0001).ToString("F4") + " V";

                        break;
                    #endregion

                    #region Byte Address = 149
                    case 149:
                        itemName = "Vcc High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[148 - 0x80] * 256 + objValues[149 - 0x80]) * 0.0001).ToString("F4") + " V";

                        break;
                    #endregion

                    #region Byte Address = 150
                    case 150:
                        itemName = "Vcc Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[150 - 0x80] * 256 + objValues[151 - 0x80]) * 0.0001).ToString("F4") + " V";

                        break;
                    #endregion

                    #region Byte Address = 151
                    case 151:
                        itemName = "Vcc Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[150 - 0x80] * 256 + objValues[151 - 0x80]) * 0.0001).ToString("F4") + " V";

                        break;
                    #endregion

                    #region Byte Address = 152-159
                    case 152:
                    case 153:
                    case 154:
                    case 155:
                    case 156:
                    case 157:
                    case 158:
                    case 159:
                        itemName = "Reserved";
                        break;
                    #endregion

                    #region Byte Address = 160-175
                    case 160:
                    case 161:
                    case 162:
                    case 163:
                    case 164:
                    case 165:
                    case 166:
                    case 167:
                    case 168:
                    case 169:
                    case 170:
                    case 171:
                    case 172:
                    case 173:
                    case 174:
                    case 175:
                        itemName = "Vendor Specific";
                        break;
                    #endregion

                    #region Byte Address = 176
                    case 176:
                        itemName = "RX Power High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[176 - 0x80] * 256 + objValues[177 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 177
                    case 177:
                        itemName = "RX Power High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[176 - 0x80] * 256 + objValues[177 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 178
                    case 178:
                        itemName = "RX Power Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[178 - 0x80] * 256 + objValues[179 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 179
                    case 179:
                        itemName = "RX Power Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[178 - 0x80] * 256 + objValues[179 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 180
                    case 180:
                        itemName = "RX Power High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[180 - 0x80] * 256 + objValues[181 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 181
                    case 181:
                        itemName = "RX Power High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[180 - 0x80] * 256 + objValues[181 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 182
                    case 182:
                        itemName = "RX Power Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[182 - 0x80] * 256 + objValues[183 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 183
                    case 183:
                        itemName = "RX Power Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[182 - 0x80] * 256 + objValues[183 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 184
                    case 184:
                        itemName = "Tx Bias High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[184 - 0x80] * 256 + objValues[185 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 185
                    case 185:
                        itemName = "Tx Bias High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[184 - 0x80] * 256 + objValues[185 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 186
                    case 186:
                        itemName = "Tx Bias Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[186 - 0x80] * 256 + objValues[187 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 187
                    case 187:
                        itemName = "Tx Bias Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[186 - 0x80] * 256 + objValues[187 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 188
                    case 188:
                        itemName = "Tx Bias High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[188 - 0x80] * 256 + objValues[189 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 189
                    case 189:
                        itemName = "Tx Bias High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[188 - 0x80] * 256 + objValues[189 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 190
                    case 190:
                        itemName = "Tx Bias Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[190 - 0x80] * 256 + objValues[191 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 191
                    case 191:
                        itemName = "Tx Bias Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = ((objValues[190 - 0x80] * 256 + objValues[191 - 0x80]) * 0.002).ToString("F2") + " mA";

                        break;
                    #endregion

                    #region Byte Address = 192
                    case 192:
                        itemName = "Tx Power High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[192 - 0x80] * 256 + objValues[193 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 193
                    case 193:
                        itemName = "Tx Power High Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[192 - 0x80] * 256 + objValues[193 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 194
                    case 194:
                        itemName = "Tx Power Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[194 - 0x80] * 256 + objValues[195 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 195
                    case 195:
                        itemName = "Tx Power Low Alarm";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[194 - 0x80] * 256 + objValues[195 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 196
                    case 196:
                        itemName = "Tx Power High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[196 - 0x80] * 256 + objValues[197 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 197
                    case 197:
                        itemName = "Tx Power High Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[196 - 0x80] * 256 + objValues[197 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 198
                    case 198:
                        itemName = "Tx Power Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[198 - 0x80] * 256 + objValues[199 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 199
                    case 199:
                        itemName = "Tx Power Low Warning";
                        addressAllDescription = "MSB at lower byte address";
                        currItemDescription = (10 * Math.Log10(((objValues[198 - 0x80] * 256 + objValues[199 - 0x80]) * 0.0001))).ToString("F2") + " dBm";

                        break;
                    #endregion

                    #region Byte Address = 200-207
                    case 200:
                    case 201:
                    case 202:
                    case 203:
                    case 204:
                    case 205:
                    case 206:
                    case 207:
                        itemName = "Reserved";
                        break;
                    #endregion

                    #region Byte Address = 208-223
                    case 208:
                    case 209:
                    case 210:
                    case 211:
                    case 212:
                    case 213:
                    case 214:
                    case 215:
                    case 216:
                    case 217:
                    case 218:
                    case 219:
                    case 220:
                    case 221:
                    case 222:
                    case 223:
                        itemName = "Vendor Specific";
                        break;
                    #endregion

                    // 224-255, 2014-10-16，代码未完成
                    //141017_0  TBD QSFP无需检查???
                    #region Byte Address = 224~255
                    #region Byte Address = 224
                    case 224:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    //141017_0TBD
                    #region Byte Address = 225
                    case 225:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 226-233
                    case 226:
                    case 227:
                    case 228:
                    case 229:
                    case 230:
                    case 231:
                    case 232:
                    case 233:
                        itemName = "Vendor Specific";
                        break;
                    #endregion

                    //141017_0TBD
                    #region Byte Address = 234
                    case 234:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 235
                    case 235:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 236
                    case 236:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 237
                    case 237:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    //141017_0TBD
                    #region Byte Address = 238
                    case 238:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 239
                    case 239:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 240
                    case 240:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 241
                    case 241:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 242
                    case 242:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 243
                    case 243:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 244
                    case 244:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 245
                    case 245:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 246
                    case 246:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 247
                    case 247:
                        itemName = "";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 248-249
                    case 248:
                    case 249:
                        itemName = "Reserved";
                        addressAllDescription = "Reserved channel monitor masks set 4";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 250-251
                    case 250:
                    case 251:
                        itemName = "Reserved";
                        addressAllDescription = "Reserved channel monitor masks";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 252-253
                    case 252:
                    case 253:
                        itemName = "Reserved";
                        addressAllDescription = "Reserved channel monitor masks";
                        currItemDescription = "";
                        break;
                    #endregion

                    #region Byte Address = 254-255
                    case 254:
                    case 255:
                        itemName = "Reserved";
                        addressAllDescription = "";
                        currItemDescription = "";
                        break;
                    #endregion
                    #endregion
                    default:
                        break;
                }
                #endregion
            }
            else if (page.ToUpper() == QSFPPages.A0H_Page2.ToString().ToUpper())
            {
                //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page3的地址 是 从128开始的
                #region Page 02h (Cable Assemblies)
                itemName = "UserEEPROM Data";
                addressAllDescription = "UserEEPROM Data";
                #endregion
            }
            else if (page.ToUpper() == QSFPPages.A0H_Page1.ToString().ToUpper())
            {
                //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page1的地址 是 从128开始的
                #region Page 01h (conditional on the state of bit 2 in byte 221.)

                switch (Address)
                {
                    #region Byte Address = 128
                    case 128:
                        itemName = "CC_APPS";
                        currItemDescription = "Check code for the AST: thecheck code shall be the low order bits of the sum of the " +
                            "contents of all the bytes from byte 129 to byte 255,inclusive.";
                        addressAllDescription = currItemDescription;
                        break;
                    #endregion

                    #region Byte Address = 129
                    case 129:
                        itemName = "AST Table Length,TL (length - 1)";
                        addressAllDescription = "<Bit:7-6 Reserved> A 6 bit binary number. TL,specifies the offset of the last application table entry" +
                            " defined in bytes 130-255. TL is valid between 0 (1 entry) and 62 (for a total of 63 entries)";
                        if ((objValues[129 - 0x80] & 63) < 63)
                        {
                            currItemDescription = (130 + 2 * (objValues[129 - 0x80] & 63)) + "," + (131 + 2 * (objValues[129 - 0x80] & 63)) + "--> Application code TL";
                        }
                        else
                        {
                            currItemDescription = "Error<overflow>!!!" + "Address = " + (130 + 2 * (objValues[129 - 0x80] & 63))
                                + "," + (131 + 2 * (objValues[129 - 0x80] & 63)) + "; Application code TL Address> 255";
                        }
                        break;
                    #endregion

                    #region Byte Address = 130,131
                    case 130:
                    case 131:
                        itemName = "Application Code 0";
                        addressAllDescription = "Definition of first application supported";
                        currItemDescription = "";
                        break;

                    #endregion

                    default:
                        itemName = "";
                        addressAllDescription = "Bytes 130 to 256 contain the application code table entries";
                        currItemDescription = "";
                        break;
                }

                try
                {
                    if (((objValues[129 - 0x80] & 63) != 63) && ((objValues[129 - 0x80] & 63) != 0))
                    {
                        byte mytemp1 = Convert.ToByte(130 + 2 * (objValues[129 - 0x80] & 63));
                        byte mytemp2 = Convert.ToByte(131 + 2 * (objValues[129 - 0x80] & 63));

                        if (Address == mytemp1 || Address == mytemp2)
                        {
                            itemName = "Application code TL";
                            addressAllDescription = "130+2*TL 131+2*TL --> Application code TL ";
                            currItemDescription = "Application code TL";
                        }
                    }
                }
                catch
                {
                }

                #endregion
            }
            return currItemDescription;
        }
    
    }         
    public class SFP : EEPROMOperation 
    {
        public SFP(int INDEX, bool isFristLoad) : base(INDEX, isFristLoad) { }
        public SFP() : base() { }

        //SFP:DATA0->A0H,DATA1->A2H_LowMemory,DATA2->A2H_Page0
        public enum SFPPages
        {
            A0H = 0, A2H_LowMemory = 1, A2H_Page0 = 2
        }
        override public string CurrItemDescription(byte[] objValues, string page, byte Address, byte addrValue, out string itemName, out string addressAllDescription)
        {
            string currItemDescription = "";
            addressAllDescription = "";
            itemName = "";

            if (page.ToUpper() == SFPPages.A0H.ToString().ToUpper())
            {
                #region A0H
                if (Address == 0x00)
                {
                    #region 0x00
                    itemName = "Identifier";
                    addressAllDescription = "Type of transceiver";
                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "Unknown or unspecified";
                            break;
                        case 0x01:
                            currItemDescription = "GBIC";
                            break;
                        case 0x02:
                            currItemDescription = "Module/connector soldered to motherboard";
                            break;
                        case 0x03:
                            currItemDescription = "SFP or SFP “Plus”";
                            break;
                        case 0x04:
                            currItemDescription = "300 pin XBI";
                            break;
                        case 0x05:
                            currItemDescription = "XENPAK";
                            break;
                        case 0x06:
                            currItemDescription = "XFP";
                            break;
                        case 0x07:
                            currItemDescription = "XFF";
                            break;
                        case 0x08:
                            currItemDescription = "XFP-E";
                            break;
                        case 0x09:
                            currItemDescription = "XPAK";
                            break;
                        case 0x0A:
                            currItemDescription = "X2";
                            break;
                        case 0x0B:
                            currItemDescription = "DWDM-SFP";
                            break;
                        case 0x0c:
                            currItemDescription = "QSFP";
                            break;
                        case 0x0d:
                            currItemDescription = "QSFP+";
                            break;
                        default:
                            currItemDescription = "ERROR";
                            break;
                    }
                    #endregion
                }

                else if (Address == 0x01)
                {
                    #region 0x01
                    itemName = "Ext. Identifier";
                    addressAllDescription = "Extended identifier of type of transceiver";
                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "GBIC definition is not specified or the GBIC definition is not " +
                                "compliant with a defined MOD_DEF.";
                            break;
                        case 0x01:
                            currItemDescription = "GBIC is compliant with MOD_DEF 1";
                            break;
                        case 0x02:
                            currItemDescription = "GBIC is compliant with MOD_DEF 2";
                            break;
                        case 0x03:
                            currItemDescription = "GBIC is compliant with MOD_DEF 3";
                            break;
                        case 0x04:
                            currItemDescription = "GBIC/SFP function is defined by two-wire interface ID only";
                            break;
                        case 0x05:
                            currItemDescription = "GBIC is compliant with MOD_DEF 5y";
                            break;
                        case 0x06:
                            currItemDescription = "GBIC is compliant with MOD_DEF 6";
                            break;
                        case 0x07:
                            currItemDescription = "GBIC is compliant with MOD_DEF 7";
                            break;
                        default:
                            currItemDescription = "ERROR";
                            break;
                    }
                    #endregion
                }

                else if (Address == 0x02)
                {
                    #region 0x02
                    itemName = "Connector values";
                    addressAllDescription = "Code for connector type";
                    if (addrValue == 0x00)
                    {
                        currItemDescription = "Unknown or unspecified";
                    }
                    else if (addrValue == 0x01)
                    {
                        currItemDescription = "SC";
                    }
                    else if (addrValue == 0x02)
                    {
                        currItemDescription = "Fibre Channel Style 1 copper connector";
                    }
                    else if (addrValue == 0x03)
                    {
                        currItemDescription = "Fibre Channel Style 2 copper connector";
                    }
                    else if (addrValue == 0x04)
                    {
                        currItemDescription = "BNC/TNC";
                    }
                    else if (addrValue == 0x05)
                    {
                        currItemDescription = "Fibre Channel coaxial headers";
                    }
                    else if (addrValue == 0x06)
                    {
                        currItemDescription = "FiberJack";
                    }
                    else if (addrValue == 0x07)
                    {
                        currItemDescription = "LC";
                    }
                    else if (addrValue == 0x08)
                    {
                        currItemDescription = "MT-RJ";
                    }
                    else if (addrValue == 0x09)
                    {
                        currItemDescription = "MU";
                    }
                    else if (addrValue == 0x0a)
                    {
                        currItemDescription = "SG";
                    }
                    else if (addrValue == 0x0b)
                    {
                        currItemDescription = "Optical pigtail";
                    }
                    else if (addrValue == 0x0c)
                    {
                        currItemDescription = "MPO Parallel Optic";
                    }
                    else if (addrValue == 0x20)
                    {
                        currItemDescription = "HSSDC II";
                    }
                    else if (addrValue == 0x21)
                    {
                        currItemDescription = "Copper pigtail";
                    }
                    else if (addrValue == 0x22)
                    {
                        currItemDescription = "RJ45";
                    }
                    else if (addrValue >= 0x80 && addrValue <= 0xff)
                    {
                        currItemDescription = "Vendor specific";
                    }
                    else
                    {
                        currItemDescription = "ERROR";
                    }
                    #endregion
                }
                else if (Address >= 0x03 && Address <= 0x0a)
                {
                    #region 0x03~0x0a
                    itemName = "Transceiver";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Address == 0x03)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "10G Base-ER";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "10G Base-LRM";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "10G Base-LR";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "10G Base-SR";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "1X SX";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "1X LX";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "1X Copper Active";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "1X Copper Passive";
                        }
                    }
                    if (Address == 0x04)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "ESCON MMF, 1310nm LED";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "ESCON SMF, 1310nm Laser";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = " OC-192, short reach";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "SONET reach specifier bit1";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "SONET reach specifier bit2";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "OC-48, long reach ";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "OC-48, intermediate reach";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "OC-48, short reach";
                        }
                    }
                    if (Address == 0x05)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "Unallocated";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "OC-12, single mode, long reach";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "OC-12, single mode, inter. reach";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "OC-12, short reach";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "Unallocated";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "OC-3, single mode, long reach";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "OC-3, single mode, inter. reach";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "OC-3, short reach ";
                        }
                    }

                    if (Address == 0x06)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "BASE-PX";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "BASE-BX10 ";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "100BASE-FX";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "100BASE-LX/LX10";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "1000BASE-T";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "1000BASE-CX";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "1000BASE-LX";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "1000BASE-SX";
                        }
                    }
                    if (Address == 0x07)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "very long distance (V)";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "short distance (S)";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "intermediate distance (I)";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "long distance (L)";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "medium distance (M)";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "Shortwave laser, linear Rx (SA)";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "Longwave laser (LC)";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Electrical inter-enclosure (EL)";
                        }
                    }
                    if (Address == 0x08)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "Electrical intra-enclosure (EL)";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "Shortwave laser w/o OFC (SN)";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "Shortwave laser with OFC (SL)";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "Longwave laser (LL)";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "Active Cable";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "Passive Cable";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "Unallocated";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Unallocated";
                        }
                    }
                    if (Address == 0x09)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "Twin Axial Pair (TW)";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "Twisted Pair (TP)";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "Miniature Coax (MI)";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "Video Coax (TV)";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "Multimode, 62.5um (M6)";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "Multimode, 50um (M5, M5E)";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "Unallocated";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Single Mode (SM)";
                        }
                    }
                    if (Address == 0x0a)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "1200 MBytes/sec";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "800 MBytes/sec";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "400 MBytes/sec";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "Unallocated";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "200 MBytes/sec";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "Multimode, 50um (M5, M5E)";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "Unallocated";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "100 MBytes/sec";
                        }
                    }
                    #endregion
                }
                else if (Address == 11)
                {
                    #region 0x0b
                    itemName = "Encoding";
                    addressAllDescription = "Code for high speed serial encoding algorithm";
                    if (addrValue == 0x00)
                    {
                        currItemDescription = "Unspecified";
                    }
                    else if (addrValue == 0x01)
                    {
                        currItemDescription = "8B/10B";
                    }
                    else if (addrValue == 0x02)
                    {
                        currItemDescription = "4B/5B";
                    }
                    else if (addrValue == 0x03)
                    {
                        currItemDescription = "NRZ";
                    }
                    else if (addrValue == 0x04)
                    {
                        currItemDescription = "Manchester";
                    }
                    else if (addrValue == 0x05)
                    {
                        currItemDescription = "SONET Scrambled";
                    }
                    else if (addrValue == 0x06)
                    {
                        currItemDescription = "64B/66B";
                    }
                    else
                    {
                        currItemDescription = "Unallocated";
                    }
                    #endregion
                }
                else if (Address == 12)
                {
                    #region 0x0c
                    itemName = "BR, Nominal";
                    addressAllDescription = "Nominal signalling rate, units of 100MBd.";
                    currItemDescription = addrValue + "00Mbps";
                    #endregion
                }
                else if (Address == 13)
                {
                    #region 0x0d
                    itemName = "Rate Identifier";
                    addressAllDescription = "Type of rate select functionality";
                    if (addrValue == 0x00)
                    {
                        currItemDescription = "Unspecified";
                    }
                    else if (addrValue == 0x01)
                    {
                        currItemDescription = "Defined for SFF-8079 (4/2/1G Rate_Select & AS0/AS1)";
                    }
                    else if (addrValue == 0x02)
                    {
                        currItemDescription = "Defined for SFF-8431 (8/4/2G Rx Rate_Select only)";
                    }
                    else if (addrValue == 0x03)
                    {
                        currItemDescription = "Unspecified *";
                    }
                    else if (addrValue == 0x04)
                    {
                        currItemDescription = "Defined for SFF-8431 (8/4/2G Tx Rate_Select only)";
                    }
                    else if (addrValue == 0x05)
                    {
                        currItemDescription = "Unspecified *";
                    }
                    else if (addrValue == 0x06)
                    {
                        currItemDescription = "Defined for SFF-8431 (8/4/2G Independent Rx & Tx Rate_select)";
                    }
                    else if (addrValue == 0x07)
                    {
                        currItemDescription = "Unspecified *";
                    }
                    else if (addrValue == 0x08)
                    {
                        currItemDescription = "Defined for FC-PI-5 (16/8/4G Rx Rate_select only) High=16G only, Low=8G/4G";
                    }
                    else if (addrValue == 0x09)
                    {
                        currItemDescription = "Unspecified *";
                    }
                    else if (addrValue == 0x0a)
                    {
                        currItemDescription = "Defined for FC-PI-5 (16/8/4G Independent Rx, Tx Rate_select) High=16G only, Low=8G/4G";
                    }
                    else
                    {
                        currItemDescription = "Unallocated";
                    }

                    #endregion
                }
                else if (Address == 14)
                {
                    #region 0x0e
                    itemName = "Length(SMF,km)";
                    addressAllDescription = "Link length supported for single mode fiber, units of km";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 254 km";
                    }
                    else
                    {
                        currItemDescription = addrValue + "km";
                    }
                    #endregion
                }
                else if (Address == 15)
                {
                    #region 0x0f
                    itemName = "Length (SMF)";
                    addressAllDescription = "Link length supported for single mode fiber, units of 100 m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 25.4 km";
                    }
                    else
                    {
                        currItemDescription = addrValue + "00m";
                    }
                    #endregion
                }
                else if (Address == 16)
                {
                    #region 0x10
                    itemName = "Length (50um)";
                    addressAllDescription = "Link length supported for 50 um OM2 fiber, units of 10 m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 2.54 km";
                    }
                    else
                    {
                        currItemDescription = addrValue + "0m";
                    }
                    #endregion
                }
                else if (Address == 17)
                {
                    #region 0x11
                    itemName = "Length (62.5um)";
                    addressAllDescription = "Link length supported for 62.5 um OM1 fiber, units of 10 m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 2.54 km";
                    }
                    else
                    {
                        currItemDescription = addrValue + "0m";
                    }
                    #endregion
                }
                else if (Address == 18)
                {
                    #region 0x12
                    itemName = "Length (cable)";
                    addressAllDescription = "Link length supported for copper or direct attach cable, units of m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 254 meters.";
                    }
                    else
                    {
                        currItemDescription = addrValue + "m";
                    }
                    #endregion
                }
                else if (Address == 19)
                {
                    #region 0x13
                    itemName = "Length (OM3)";
                    addressAllDescription = "Link length supported for 50 um OM3 fiber, units of 10 m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 2.54 km";
                    }
                    else
                    {
                        currItemDescription = addrValue + "0m";
                    }
                    #endregion
                }
                else if (Address >= 20 && Address <= 35)
                {
                    #region 0x14~0x23
                    itemName = "Vendor name";
                    addressAllDescription = "SFP vendor name (ASCII)";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }
                else if (Address == 36)
                {
                    #region 0x24
                    itemName = "Transceiver";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    currItemDescription = "Unallocated";

                    #endregion
                }
                else if (Address >= 37 && Address <= 39)
                {
                    #region 0x25~0x27
                    itemName = "Vendor OUI";
                    addressAllDescription = "SFP vendor IEEE company ID:"
                    + "\n The vendor organizationally unique identifier field (vendor OUI) is a 3-byte field that contains"
                    + " the IEEE Company Identifier for the vendor. A value of all zero in the 3-byte field indicates that "
                    + " the Vendor OUI is unspecified.";

                    currItemDescription = addrValue.ToString("X").PadLeft(2, '0');  //141024_0

                    #endregion
                }
                else if (Address >= 40 && Address <= 55)
                {
                    //TBD 同时...
                    #region 0x28~0x37
                    itemName = "Vendor PN";
                    addressAllDescription = "Part number provided by SFP vendor (ASCII)";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }

                else if (Address >= 56 && Address <= 59)
                {
                    #region 0x38~0x3b
                    itemName = "Vendor rev";
                    addressAllDescription = "Revision level for part number provided by vendor (ASCII)";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }

                else if (Address >= 60 && Address <= 61)
                {
                    //TBD 同时...
                    #region 0x3c~0x3d
                    itemName = "Wavelength";
                    addressAllDescription = "Laser wavelength (Passive/Active Cable Specification Compliance)";

                    currItemDescription = (objValues[60] * 256 + objValues[61]).ToString();

                    #endregion
                }
                else if (Address == 62)
                {
                    #region 0x3e
                    itemName = "Unallocated";
                    addressAllDescription = "";
                    #endregion
                }

                else if (Address == 63)
                {
                    #region 0x3f
                    //CC_BASE Check code for Base ID Fields (addresses 0 to 62)
                    itemName = "CC_BASE";
                    addressAllDescription = "Check code for Base ID Fields (addresses 0 to 62)";
                    currItemDescription = "CS=" + (addrValue).ToString("X");

                    #endregion
                }

                else if (Address >= 64 && Address <= 65)
                {
                    #region 0x40,0x41
                    //Options Indicates which optional transceiver signals are implemented
                    itemName = "Options";
                    addressAllDescription = "Indicates which optional transceiver signals are implemented";

                    if (addrValue == 64)
                    {
                        if ((addrValue & Convert.ToInt32("100", 2)) == Convert.ToInt32("100", 2))
                        {
                            currItemDescription += "Cooled Transceiver Declaration:" +
                                "a cooled laser transmitter implementation.";
                        }
                        else
                        {
                            currItemDescription += "Cooled Transceiver Declaration:" +
                               " a conventional uncooled (or unspecified) laser";
                        }
                        if ((addrValue & Convert.ToInt32("10", 2)) == Convert.ToInt32("10", 2))
                        {
                            currItemDescription += "\n Power Level Declaration:" +
                               "Power Level 2 requirement.";
                        }
                        else
                        {
                            currItemDescription += "\n Power Level Declaration:" +
                             "Power Level 1 (or unspecified) requirements.";
                        }
                        if ((addrValue & Convert.ToInt32("1", 2)) == Convert.ToInt32("1", 2))
                        {
                            currItemDescription += "\n Linear Receiver Output Implemented:" +
                                " a linear receiver output.";
                        }
                        else
                        {
                            currItemDescription += "\n Linear Receiver Output Implemented:" +
                                " a conventional limiting (or unspecified) receiver output.";
                        }
                    }
                    else if (addrValue == 65)
                    {
                        if ((addrValue & Convert.ToInt32("100000", 2)) == Convert.ToInt32("100000", 2))
                        {
                            currItemDescription += "RATE_SELECT functionality is implemented \n";
                            //+"NOTE: Lack of implemention does not indicate lack of simultaneous compliance" +
                            //" with multiple standard rates. Compliance with particular standards should be"+
                            //" determined from Transceiver Code Section (Table 3.5). Refer to Table 3.6a for"+
                            //" Rate_Select functionality type identifiers.";
                        }
                        if ((addrValue & Convert.ToInt32("10000", 2)) == Convert.ToInt32("10000", 2))
                        {
                            currItemDescription += "\n TX_DISABLE is implemented and disables the high speed serial output.";
                        }
                        if ((addrValue & Convert.ToInt32("1000", 2)) == Convert.ToInt32("1000", 2))
                        {
                            currItemDescription += "\n TX_FAULT signal implemented. (See SFP MSA).";
                        }
                        if ((addrValue & Convert.ToInt32("100", 2)) == Convert.ToInt32("100", 2))
                        {
                            currItemDescription += "\n Loss of Signal implemented, signal inverted from standard definition in SFP MSA" +
                                "(often called “Signal Detect”). \n" +
                                "NOTE: This is not standard SFP/GBIC behavior and should be avoided, since noninteroperable behavior results.";
                        }
                        if ((addrValue & Convert.ToInt32("10", 2)) == Convert.ToInt32("10", 2))
                        {
                            currItemDescription += "\n Loss of Signal implemented, signal as defined in SFP MSA (often called “Rx_LOS”).";
                        }
                    }
                    #endregion
                }
                else if (Address == 66)
                {
                    #region 0x42
                    itemName = "BR, max";
                    addressAllDescription = "Upper bit rate margin, units of %";
                    if (addrValue == 0)
                    {
                        currItemDescription = "not specified.";
                    }
                    else
                    {
                        currItemDescription = addrValue.ToString() + "%";
                    }
                    #endregion
                }
                else if (Address == 67)
                {
                    #region 0x43
                    itemName = "BR, min";
                    addressAllDescription = "Lower bit rate margin, units of %";
                    if (addrValue == 0)
                    {
                        currItemDescription = "not specified.";
                    }
                    else
                    {
                        currItemDescription = addrValue.ToString() + "%";
                    }

                    #endregion
                }
                else if (Address >= 68 && Address <= 83)
                {
                    //TBD 同时...
                    #region 0x44~0x53
                    itemName = "Vendor SN";
                    addressAllDescription = "Serial number provided by vendor (ASCII)";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }
                else if (Address >= 84 && Address <= 91)
                {
                    //TBD 同时...
                    #region 0x54~0x5b
                    itemName = "Date code";
                    addressAllDescription = "Vendor’s manufacturing date code";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }
                else if (Address == 92)
                {
                    #region 0x5c
                    itemName = "Diagnostic Monitoring Type";
                    addressAllDescription = "Indicates which type of diagnostic monitoring is implemented (if any) in the transceiver";
                    if ((addrValue | Convert.ToInt32("11", 2)) != Convert.ToInt32("11", 2))
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "Reserved for legacy diagnostic implementations. Must be ‘0’ for compliance with this document.";
                        }
                        if ((addrValue & Convert.ToInt32("1000000", 2)) == Convert.ToInt32("1000000", 2))
                        {
                            currItemDescription = "\n Digital diagnostic monitoring implemented (described in this document). Must be ‘1’ for compliance with this document.";
                        }
                        if ((addrValue & Convert.ToInt32("100000", 2)) == Convert.ToInt32("100000", 2))
                        {
                            currItemDescription += "\n Internally calibrated";
                        }
                        if ((addrValue & Convert.ToInt32("10000", 2)) == Convert.ToInt32("10000", 2))
                        {
                            currItemDescription += "\n Externally calibrated";
                        }
                        if ((addrValue & Convert.ToInt32("1000", 2)) == Convert.ToInt32("1000", 2))
                        {
                            currItemDescription += "\n Received power measurement type :average";
                        }
                        else
                        {
                            currItemDescription += "\n Received power measurement type :OMA";
                        }
                        if ((addrValue & Convert.ToInt32("100", 2)) == Convert.ToInt32("100", 2))
                        {
                            currItemDescription += "\n Address change required see section above,“addressing modes”";
                        }
                    }
                    else
                    {
                        currItemDescription = "Unallocated";
                    }
                    #endregion
                }
                else if (Address == 93)
                {
                    #region 0x5d
                    itemName = "Enhanced Options";
                    addressAllDescription = "Indicates which optional enhanced features are implemented (if any) in the transceiver";
                    if ((addrValue | Convert.ToInt32("1", 2)) != Convert.ToInt32("1", 2))
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "Optional Alarm/warning flags implemented for all monitored quantities.";
                        }
                        if ((addrValue & Convert.ToInt32("1000000", 2)) == Convert.ToInt32("1000000", 2))
                        {
                            currItemDescription = "\n Optional soft TX_DISABLE control and monitoring implemented";
                        }
                        if ((addrValue & Convert.ToInt32("100000", 2)) == Convert.ToInt32("100000", 2))
                        {
                            currItemDescription += "\n Optional soft TX_FAULT monitoring implemented";
                        }
                        if ((addrValue & Convert.ToInt32("10000", 2)) == Convert.ToInt32("10000", 2))
                        {
                            currItemDescription += "\n Optional soft RX_LOS monitoring implemented";
                        }
                        if ((addrValue & Convert.ToInt32("1000", 2)) == Convert.ToInt32("1000", 2))
                        {
                            currItemDescription += "\n Optional soft RATE_SELECT control and monitoring implemented";
                        }
                        if ((addrValue & Convert.ToInt32("100", 2)) == Convert.ToInt32("100", 2))
                        {
                            currItemDescription += "\n Optional Application Select control implemented per SFF-8079";
                        }
                        if ((addrValue & Convert.ToInt32("10", 2)) == Convert.ToInt32("10", 2))
                        {
                            currItemDescription += "\n Optional soft Rate Select control implemented per SFF-8431";
                        }
                    }
                    else
                    {
                        currItemDescription = "Unallocated";
                    }
                    #endregion
                }
                else if (Address == 94)
                {
                    #region 0x5e
                    itemName = "SFF-8472 Compliance";
                    addressAllDescription = "ndicates which revision of SFF-8472 the transceiver complies with.";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Digital diagnostic functionality not included or undefined";
                    }
                    else if (addrValue == 0x01)
                    {
                        currItemDescription = "Includes functionality described in Rev 9.3 of SFF-8472.";
                    }
                    else if (addrValue == 0x02)
                    {
                        currItemDescription = "Includes functionality described in Rev 9.5 of SFF-8472.";
                    }
                    else if (addrValue == 0x03)
                    {
                        currItemDescription = "Includes functionality described in Rev 10.2 of SFF-8472.";
                    }
                    else if (addrValue == 0x04)
                    {
                        currItemDescription = "Includes functionality described in Rev 10.4 of SFF-8472.";
                    }
                    else if (addrValue == 0x05)
                    {
                        currItemDescription = "Includes functionality described in Rev 11.0 of SFF-8472.";
                    }
                    else
                    {
                        currItemDescription = "Unallocated";
                    }
                    #endregion
                }
                else if (Address == 95)
                {
                    #region 0x5f
                    itemName = "CC_EXT";
                    addressAllDescription = "Check code for the Extended ID Fields (addresses 64 to 94)";
                    currItemDescription = "CS2=" + (addrValue).ToString("X");

                    #endregion
                }
                else if (Address >= 96 && Address <= 127)
                {
                    //TBD 同时...
                    #region 0x60~0x7f
                    itemName = "Vendor Specific";
                    addressAllDescription = "Vendor Specific EEPROM";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }
                else if (Address >= 0x80 && Address <= 0xff)
                {
                    //TBD 同时...
                    #region 0x80~0xff
                    itemName = "Reserved";
                    addressAllDescription = "Reserved for SFF-8079";
                    currItemDescription = (addrValue).ToString();

                    #endregion
                }
                else
                {
                    currItemDescription = "ERROR";
                }
                #endregion
            }
            else if (page.ToUpper() == SFPPages.A2H_LowMemory.ToString().ToUpper())
            {
                #region A2H_LowMemory
                //0-39 40 A/W Thresholds Diagnostic Flag Alarm and Warning Thresholds
                //Temp
                if (Address >= 0x00 && Address <= 0x01)
                {
                    #region 0x00,0x01
                    itemName = "Temp High Alarm";
                    addressAllDescription = "MSB at low address";
                    float mytemp = objValues[0x00] + objValues[0x01] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";

                    #endregion
                }
                if (Address >= 0x02 && Address <= 0x03)
                {
                    #region 0x02,0x03
                    itemName = "Temp Low Alarm";
                    addressAllDescription = "MSB at low address";
                    float mytemp = objValues[0x02] + objValues[0x03] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";

                    #endregion
                }
                if (Address >= 0x04 && Address <= 0x05)
                {
                    #region 0x04,0x05
                    itemName = "Temp High Warning";
                    addressAllDescription = "MSB at low address";
                    float mytemp = objValues[0x04] + objValues[0x05] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";

                    #endregion
                }
                if (Address >= 0x06 && Address <= 0x07)
                {
                    #region 0x06,0x7
                    itemName = "Temp Low Warning";
                    addressAllDescription = "MSB at low address";
                    float mytemp = objValues[0x06] + objValues[0x7] / (float)256;
                    if (mytemp > 128)
                        mytemp -= 256;
                    currItemDescription = mytemp.ToString("F4") + " °C";

                    #endregion
                }

                //Vcc
                if (Address >= 0x08 && Address <= 0x09)
                {
                    #region 0x08,0x09
                    itemName = "Voltage High Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x08] * 256 + objValues[0x09]) * 0.0001).ToString("F4") + " V";

                    #endregion
                }
                if (Address >= 0x0a && Address <= 0x0b)
                {
                    #region 0x0a,0x0b
                    itemName = "Voltage Low Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x0a] * 256 + objValues[0x0b]) * 0.0001).ToString("F4") + " V";

                    #endregion
                }
                if (Address >= 0x0c && Address <= 0x0d)
                {
                    #region 0x0c,0x0d
                    itemName = "Voltage High Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x0c] * 256 + objValues[0x0d]) * 0.0001).ToString("F4") + " V";

                    #endregion
                }
                if (Address >= 0x0e && Address <= 0x0f)
                {
                    #region 0x0e,0x0f
                    itemName = "Voltage Low Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x0e] * 256 + objValues[0x0f]) * 0.0001).ToString("F4") + " V";

                    #endregion
                }
                //Bias
                if (Address >= 0x10 && Address <= 0x11)
                {
                    #region 0x10,0x11
                    itemName = "Bias High Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x10] * 256 + objValues[0x11]) * 0.002).ToString("F2") + " mA";

                    #endregion
                }
                if (Address >= 0x12 && Address <= 0x13)
                {
                    #region 0x12,0x13
                    itemName = "Bias Low Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x12] * 256 + objValues[0x13]) * 0.002).ToString("F2") + " mA";

                    #endregion
                }
                if (Address >= 0x14 && Address <= 0x15)
                {
                    #region 0x14,0x15
                    itemName = "Bias High Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x14] * 256 + objValues[0x15]) * 0.002).ToString("F2") + " mA";

                    #endregion
                }
                if (Address >= 0x16 && Address <= 0x17)
                {
                    #region 0x16,0x7
                    itemName = "Bias Low Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = ((objValues[0x16] * 256 + objValues[0x17]) * 0.002).ToString("F2") + " mA";

                    #endregion
                }

                //TX Power
                if (Address >= 0x18 && Address <= 0x19)
                {
                    #region 0x18,0x19
                    itemName = "TX Power High Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10(((objValues[0x18] * 256 + objValues[0x19]) * 0.0001))).ToString("F2") + " dBm";

                    #endregion
                }
                if (Address >= 0x1a && Address <= 0x1b)
                {
                    #region 0x1a,0x1b
                    itemName = "TX Power Low Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10(((objValues[0x1a] * 256 + objValues[0x1b]) * 0.0001))).ToString("F2") + " dBm";

                    #endregion
                }
                if (Address >= 0x1c && Address <= 0x1d)
                {
                    #region 0x1c,0x1d
                    itemName = "TX Power High Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10(((objValues[0x1c] * 256 + objValues[0x1d]) * 0.0001))).ToString("F2") + " dBm";

                    #endregion
                }
                if (Address >= 0x1e && Address <= 0x1f)
                {
                    #region 0x1e,0x1f
                    itemName = "TX Power Low Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10((objValues[0x1e] * 256 + objValues[0x1f]) * 0.0001)).ToString("F2") + " dBm";

                    #endregion
                }

                //RX Power
                if (Address >= 0x20 && Address <= 0x21)
                {
                    #region 0x20,0x21
                    itemName = "RX Power High Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10(((objValues[0x20] * 256 + objValues[0x21]) * 0.0001))).ToString("F2") + " dBm";

                    #endregion
                }
                if (Address >= 0x22 && Address <= 0x23)
                {
                    #region 0x22,0x23
                    itemName = "RX Power Low Alarm";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10(((objValues[0x22] * 256 + objValues[0x23]) * 0.0001))).ToString("F2") + " dBm";

                    #endregion
                }
                if (Address >= 0x24 && Address <= 0x25)
                {
                    #region 0x24,0x25
                    itemName = "RX Power High Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10(((objValues[0x24] * 256 + objValues[0x25]) * 0.0001))).ToString("F2") + " dBm";

                    #endregion
                }
                if (Address >= 0x26 && Address <= 0x27)
                {
                    #region 0x26,0x27
                    itemName = "RX Power Low Warning";
                    addressAllDescription = "MSB at low address";
                    currItemDescription = (10 * Math.Log10((objValues[0x26] * 256 + objValues[0x27]) * 0.0001)).ToString("F2") + " dBm";

                    #endregion
                }
                if (Address >= 0x28 && Address <= 0x37)
                {
                    #region 0x28~0x37
                    itemName = "Unallocated";
                    addressAllDescription = "Reserved for future monitored quantities";
                    currItemDescription = "";

                    #endregion
                }

                if (Address >= 0x38 && Address <= 0x3B)
                {
                    #region 0x38~0x3B
                    itemName = "Rx_PWR(4)";
                    addressAllDescription = "Single precision floating point calibration data - Rx optical power." 
                        + " Bit 7 of byte 56 is MSB. Bit 0 of byte 59 is LSB." 
                        + " Rx_PWR(4) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Single precision floating point calibration data - Rx optical power.";

                    #endregion
                }

                if (Address >= 0x3C && Address <= 0x3F)
                {
                    #region 0x3C~0x3F
                    itemName = "Rx_PWR(3)";
                    addressAllDescription = "Single precision floating point calibration data - Rx optical power." 
                        + " Bit 7 of byte 60 is MSB. Bit 0 of byte 63 is LSB." 
                        + " Rx_PWR(3) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Single precision floating point calibration data - Rx optical power.";

                    #endregion
                }

                if (Address >= 0x40 && Address <= 0x43)
                {
                    #region 0x40~0x43
                    itemName = "Rx_PWR(2)";
                    addressAllDescription = "Single precision floating point calibration data, Rx optical power."
                        + " Bit 7 of byte 64 is MSB, bit 0 of byte 67 is LSB." 
                        + " Rx_PWR(2) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Single precision floating point calibration data, Rx optical power.";

                    #endregion
                }

                if (Address >= 0x44 && Address <= 0x47)
                {
                    #region 0x44~0x47
                    itemName = "Rx_PWR(1)";
                    addressAllDescription = "Single precision floating point calibration data, Rx optical power."
                        + " Bit 7 of byte 68 is MSB, bit 0 of byte 71 is LSB." 
                        + " Rx_PWR(1) should be set to 1 for “internally calibrated” devices.";
                    currItemDescription = "Single precision floating point calibration data, Rx optical power.";

                    #endregion
                }

                if (Address >= 0x48 && Address <= 0x4B)
                {
                    #region 0x48~0x4B
                    itemName = "Rx_PWR(0)";
                    addressAllDescription = "Single precision floating point calibration data, Rx optical power." 
                        + " Bit 7 of byte 72 is MSB, bit 0 of byte 75 is LSB." 
                        + " Rx_PWR(0) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Single precision floating point calibration data, Rx optical power.";

                    #endregion
                }

                if (Address >= 0x4c && Address <= 0x4d)
                {
                    #region 0x4c~0x4d
                    itemName = "Tx_I(Slope)";
                    addressAllDescription = "Fixed decimal (unsigned) calibration data, laser bias current." 
                        + " Bit 7 of byte 76 is MSB, bit 0 of byte 77 is LSB." 
                        + " Tx_I(Slope) should be set to 1 for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (unsigned) calibration data, laser bias current.";

                    #endregion
                }

                if (Address >= 0x4e && Address <= 0x4f)
                {
                    #region 0x4e~0x4f
                    itemName = "Tx_I(Offset)";
                    addressAllDescription = "Fixed decimal (signed two’s complement) calibration data, laser bias current."
                        + " Bit 7 of byte 78 is MSB, bit 0 of byte 79 is LSB." 
                        + " Tx_I(Offset) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (signed two’s complement) calibration data, laser bias current.";

                    #endregion
                }

                if (Address >= 0x50 && Address <= 0x51)
                {
                    #region 0x50~0x51
                    itemName = "Tx_PWR(Slope)";
                    addressAllDescription = "Fixed decimal (unsigned) calibration data, transmitter coupled output power." 
                        + " Bit 7 of byte 80 is MSB, bit 0 of byte 81 is LSB."
                        + " Tx_PWR(Slope) should be set to 1 for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (unsigned) calibration data, transmitter coupled output power.";

                    #endregion
                }

                if (Address >= 0x52 && Address <= 0x53)
                {
                    #region 0x52~0x53
                    itemName = "Tx_PWR(Offset)";
                    addressAllDescription = "Fixed decimal (signed two’s complement) calibration data, transmitter coupled output power." 
                        + " Bit 7 of byte 82 is MSB, bit 0 of byte 83 is LSB." 
                        + " Tx_PWR(Offset) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (signed two’s complement) calibration data, transmitter coupled output power.";

                    #endregion
                }

                if (Address >= 0x54 && Address <= 0x55)
                {
                    #region 0x54~0x55
                    itemName = "T (Slope)";
                    addressAllDescription = "Fixed decimal (unsigned) calibration data, internal module temperature."
                        + " Bit 7 of byte 84 is MSB, bit 0 of byte 85 is LSB." 
                        + " T(Slope) should be set to 1 for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (unsigned) calibration data, internal module temperature.";

                    #endregion
                }

                if (Address >= 0x56 && Address <= 0x57)
                {
                    #region 0x56~0x57
                    itemName = "T (Offset)";
                    addressAllDescription = "Fixed decimal (signed two’s complement) calibration data, internal module temperature." 
                        + " Bit 7 of byte 86 is MSB, bit 0 of byte 87 is LSB." 
                        + " T(Offset) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (signed two’s complement) calibration data, internal module temperature.";

                    #endregion
                }

                if (Address >= 0x58 && Address <= 0x59)
                {
                    #region 0x58~0x59
                    itemName = "V (Slope)";
                    addressAllDescription = "Fixed decimal (unsigned) calibration data, internal module supply voltage." 
                        + " Bit 7 of byte 88 is MSB, bit 0 of byte 89 is LSB." 
                        + " V(Slope) should be set to 1 for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (unsigned) calibration data, internal module supply voltage.";

                    #endregion
                }

                if (Address >= 0x5A && Address <= 0x5B)
                {
                    #region 0x5a~0x5b
                    itemName = "V (Offset)";
                    addressAllDescription = "Fixed decimal (signed two’s complement) calibration data, internal module supply voltage." 
                        + " Bit 7 of byte 90 is MSB. Bit 0 of byte 91 is LSB." 
                        + " V(Offset) should be set to zero for “internally calibrated” devices.";
                    currItemDescription = "Fixed decimal (signed two’s complement) calibration data, internal module supply voltage.";

                    #endregion
                }

                if (Address >= 0x5C && Address <=0x5E)
                {  
                    #region 0x5c~0x5E
                    itemName = "Unallocated";
                    addressAllDescription = "";
                    currItemDescription = "";

                    #endregion
                }

                if (Address == 0x5f)
                {  
                    #region 0x5F
                    itemName = "CC_DMI";
                    addressAllDescription = "Check code for Base Diagnostic Fields (addresses 0 to 94)";
                    currItemDescription = "";

                    #endregion
                }
                #endregion

            }
            else if (page.ToUpper() == SFPPages.A2H_Page0.ToString().ToUpper())
            {
                #region A2H_Page0

                itemName = "USER EEPROM DATA";
                addressAllDescription = "USER EEPROM DATA";
                currItemDescription = (addrValue).ToString("X");

                #endregion
            }
            return currItemDescription;
        }
        

        override protected bool EngMode(byte pageNo)
        {            
            try
            {
                byte[] buff = new byte[5];
                buff[0] = 0xca;
                buff[1] = 0x2d;
                buff[2] = 0x81;
                buff[3] = 0x5f;
                buff[4] = pageNo;
                USBIO.WrtieReg(deviceIndex, 0xA2, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(100);
                return true;
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.ToString());
                return false;
            }
        }

        bool readA0H()
        {
            if (ReadBytes(0xa0, 0, 256, out ReadData0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool readA2H()
        {
            EngMode(0);

            if (ReadBytes(0xa2, 0, 128, out ReadData1) && ReadBytes(0xa2, 128, 128, out ReadData2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        override public bool EEPROMRead()
        {
            if (readA2H() && readA0H())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        override public bool EEPROMWrite()
        {
            if (writeA2H() && writeA0H())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool writeA2H()
        {
            EngMode(0);
            if (WriteBytes(0xa2, 0, 96, Data1) && WriteBytes(0xa2, 0x80, 128, Data2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool writeA0H()
        {
            EngMode(0);
            if (WriteBytes(0xa0, 0, 256, Data0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            
    }

    public class XFP : EEPROMOperation
    {
        public XFP(int INDEX, bool isFristLoad) : base(INDEX, isFristLoad) { }
        public XFP() : base() { }

        //XFP:DATA1->A0H_LowMemory,DATA0->A0H_Page1,DATA2->A0H_Page2
        public enum XFPPages
        {
            A0H_Page1 = 0, A0H_LowMemory = 1, A0H_Page2 = 2
        }
        override public string CurrItemDescription(byte[] objValues, string page, byte Address, byte addrValue, out string itemName, out string addressAllDescription)
        {
            string currItemDescription = "";
            addressAllDescription = "";
            itemName = "";

            if (page.ToUpper() == XFPPages.A0H_LowMemory.ToString().ToUpper())
            {
                #region A0H_LowMemory
                if (Address == 0x00)
                {
                    #region 0x00
                    itemName = "Identifier";
                    addressAllDescription = "physical device";
                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "Unknown or unspecified";
                            break;
                        case 0x01:
                            currItemDescription = "GBIC";
                            break;
                        case 0x02:
                            currItemDescription = "Module/connector soldered to motherboard";
                            break;
                        case 0x03:
                            currItemDescription = "SFP or SFP “Plus”";
                            break;
                        case 0x04:
                            currItemDescription = "300 pin XBI";
                            break;
                        case 0x05:
                            currItemDescription = "XENPAK";
                            break;
                        case 0x06:
                            currItemDescription = "XFP";
                            break;
                        case 0x07:
                            currItemDescription = "XFF";
                            break;
                        case 0x08:
                            currItemDescription = "XFP-E";
                            break;
                        case 0x09:
                            currItemDescription = "XPAK";
                            break;
                        case 0x0A:
                            currItemDescription = "X2";
                            break;
                        case 0x0B:
                            currItemDescription = "DWDM-SFP";
                            break;
                        case 0x0c:
                            currItemDescription = "QSFP";
                            break;
                        case 0x0d:
                            currItemDescription = "QSFP+";
                            break;
                        default:
                            currItemDescription = "ERROR";
                            break;
                    }
                    #endregion
                }

                else if (Address == 0x01)
                {
                    #region 0x01
                    itemName = "SIGNAL CONDITIONER CONTROL";
                    addressAllDescription = "";
                    currItemDescription = "";
                    #endregion
                }

                else if (Address >= 0x02 && Address <= 57)
                {
                    //Temp
                    if (Address >= 0x02 && Address <= 0x03)
                    {
                        #region 0x02,0x03
                        itemName = "Temp High Alarm";
                        addressAllDescription = "MSB at low address";
                        float mytemp = objValues[0x02] + objValues[0x03] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";

                        #endregion
                    }
                    if (Address >= 0x04 && Address <= 0x05)
                    {
                        #region 0x04,0x05
                        itemName = "Temp Low Alarm";
                        addressAllDescription = "MSB at low address";
                        float mytemp = objValues[0x04] + objValues[0x05] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";

                        #endregion
                    }
                    if (Address >= 0x06 && Address <= 0x07)
                    {
                        #region 0x06,0x07
                        itemName = "Temp High Warning";
                        addressAllDescription = "MSB at low address";
                        float mytemp = objValues[0x06] + objValues[0x07] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";

                        #endregion
                    }
                    if (Address >= 0x08 && Address <= 0x09)
                    {
                        #region 0x08,0x09
                        itemName = "Temp Low Warning";
                        addressAllDescription = "MSB at low address";
                        float mytemp = objValues[0x08] + objValues[0x09] / (float)256;
                        if (mytemp > 128)
                            mytemp -= 256;
                        currItemDescription = mytemp.ToString("F4") + " °C";

                        #endregion
                    }

                    //Reserved A/D Flag Thresholds
                    if (Address >= 0x0a && Address <= 0x11)
                    {
                        #region 0x08~0x11
                        itemName = "Reserved A/D Flag Thresholds";
                        addressAllDescription = "Reserved A/D Flag Thresholds";
                        currItemDescription = "";
                        #endregion
                    }

                    //Bias
                    if (Address >= 0x12 && Address <= 0x13)
                    {
                        #region 0x12,0x13
                        itemName = "Bias High Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x12] * 256 + objValues[0x13]) * 0.002).ToString("F2") + " mA";

                        #endregion
                    }
                    if (Address >= 0x14 && Address <= 0x15)
                    {
                        #region 0x14,0x15
                        itemName = "Bias Low Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x14] * 256 + objValues[0x15]) * 0.002).ToString("F2") + " mA";

                        #endregion
                    }
                    if (Address >= 0x16 && Address <= 0x17)
                    {
                        #region 0x16,0x17
                        itemName = "Bias High Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x16] * 256 + objValues[0x17]) * 0.002).ToString("F2") + " mA";

                        #endregion
                    }
                    if (Address >= 0x18 && Address <= 0x19)
                    {
                        #region 0x18,0x7
                        itemName = "Bias Low Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x18] * 256 + objValues[0x19]) * 0.002).ToString("F2") + " mA";

                        #endregion
                    }

                    //TX Power
                    if (Address >= 0x1a && Address <= 0x1b)
                    {
                        #region 0x1a,0x1b
                        itemName = "TX Power High Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10(((objValues[0x1a] * 256 + objValues[0x1b]) * 0.0001))).ToString("F2") + " dBm";

                        #endregion
                    }
                    if (Address >= 0x1c && Address <= 0x1d)
                    {
                        #region 0x1c,0x1d
                        itemName = "TX Power Low Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10(((objValues[0x1c] * 256 + objValues[0x1d]) * 0.0001))).ToString("F2") + " dBm";

                        #endregion
                    }
                    if (Address >= 0x1e && Address <= 0x1f)
                    {
                        #region 0x1e,0x1f
                        itemName = "TX Power High Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10(((objValues[0x1e] * 256 + objValues[0x1f]) * 0.0001))).ToString("F2") + " dBm";

                        #endregion
                    }
                    if (Address >= 0x20 && Address <= 0x21)
                    {
                        #region 0x20,0x21
                        itemName = "TX Power Low Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10((objValues[0x20] * 256 + objValues[0x21]) * 0.0001)).ToString("F2") + " dBm";

                        #endregion
                    }

                    //RX Power
                    if (Address >= 0x22 && Address <= 0x23)
                    {
                        #region 0x22,0x23
                        itemName = "RX Power High Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10(((objValues[0x22] * 256 + objValues[0x23]) * 0.0001))).ToString("F2") + " dBm";

                        #endregion
                    }
                    if (Address >= 0x24 && Address <= 0x25)
                    {
                        #region 0x24,0x25
                        itemName = "RX Power Low Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10(((objValues[0x24] * 256 + objValues[0x25]) * 0.0001))).ToString("F2") + " dBm";

                        #endregion
                    }
                    if (Address >= 0x26 && Address <= 0x27)
                    {
                        #region 0x26,0x27
                        itemName = "RX Power High Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10(((objValues[0x26] * 256 + objValues[0x27]) * 0.0001))).ToString("F2") + " dBm";

                        #endregion
                    }
                    if (Address >= 0x28 && Address <= 0x29)
                    {
                        #region 0x28,0x29
                        itemName = "RX Power Low Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = (10 * Math.Log10((objValues[0x28] * 256 + objValues[0x29]) * 0.0001)).ToString("F2") + " dBm";

                        #endregion
                    }
                    //AUX 1
                    if (Address >= 0x2a && Address <= 0x2b)
                    {
                        #region 0x2a,0x2b
                        itemName = "AUX1 High Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x2a] * 256 + objValues[0x2b]) * 0.0001).ToString("F4") + " V";

                        #endregion
                    }
                    if (Address >= 0x2c && Address <= 0x2d)
                    {
                        #region 0x2c,0x2d
                        itemName = "AUX1 Low Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x2c] * 256 + objValues[0x2d]) * 0.0001).ToString("F4") + " V";

                        #endregion
                    }
                    if (Address >= 0x2e && Address <= 0x2f)
                    {
                        #region 0x2e,0x2f
                        itemName = "AUX1 High Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x2e] * 256 + objValues[0x2f]) * 0.0001).ToString("F4") + " V";

                        #endregion
                    }
                    if (Address >= 0x30 && Address <= 0x31)
                    {
                        #region 0x30,0x31
                        itemName = "AUX1 Low Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x30] * 256 + objValues[0x31]) * 0.0001).ToString("F4") + " V";

                        #endregion
                    }

                    //AUX 2
                    if (Address >= 0x32 && Address <= 0x33)
                    {
                        #region 0x32,0x33
                        itemName = "AUX2 High Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x32] * 256 + objValues[0x33]) * 0.0001).ToString("F4") + " V";

                        #endregion
                    }
                    if (Address >= 0x34 && Address <= 0x35)
                    {
                        #region 0x34,0x35
                        itemName = "AUX2 Low Alarm";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x34] * 256 + objValues[0x35]) * 0.0001).ToString("F4") + " V";
                        #endregion
                    }
                    if (Address >= 0x36 && Address <= 0x37)
                    {
                        #region 0x36,0x37
                        itemName = "AUX2 High Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x36] * 256 + objValues[0x37]) * 0.0001).ToString("F4") + " V";

                        #endregion
                    }
                    if (Address >= 0x38 && Address <= 0x39)
                    {
                        #region 0x38,0x39
                        itemName = "AUX2 Low Warning";
                        addressAllDescription = "MSB at low address";
                        currItemDescription = ((objValues[0x38] * 256 + objValues[0x39]) * 0.0001).ToString("F4") + " V";

                        #endregion
                    }
                }
                #endregion
            }
            else if (page.ToUpper() == XFPPages.A0H_Page1.ToString().ToUpper())
            {
                //objValues[0x80 - 0x80] 请特别注意: 因为A0H_Page0的地址 是 从128开始的
                #region A0H_Page1
                if (Address == 0x80)
                {
                    #region 0x80
                    itemName = "Identifier";
                    addressAllDescription = "physical device";
                    switch (addrValue)
                    {
                        case 0x00:
                            currItemDescription = "Unknown or unspecified";
                            break;
                        case 0x01:
                            currItemDescription = "GBIC";
                            break;
                        case 0x02:
                            currItemDescription = "Module/connector soldered to motherboard";
                            break;
                        case 0x03:
                            currItemDescription = "SFP or SFP “Plus”";
                            break;
                        case 0x04:
                            currItemDescription = "300 pin XBI";
                            break;
                        case 0x05:
                            currItemDescription = "XENPAK";
                            break;
                        case 0x06:
                            currItemDescription = "XFP";
                            break;
                        case 0x07:
                            currItemDescription = "XFF";
                            break;
                        case 0x08:
                            currItemDescription = "XFP-E";
                            break;
                        case 0x09:
                            currItemDescription = "XPAK";
                            break;
                        case 0x0A:
                            currItemDescription = "X2";
                            break;
                        case 0x0B:
                            currItemDescription = "DWDM-SFP";
                            break;
                        case 0x0c:
                            currItemDescription = "QSFP";
                            break;
                        case 0x0d:
                            currItemDescription = "QSFP+";
                            break;
                        default:
                            currItemDescription = "ERROR";
                            break;
                    }
                    #endregion
                }
                else if (Address == 0x81)
                {
                    #region 0x81
                    itemName = "Ext. Identifier";
                    addressAllDescription = "Extended identifier of type of serial transceiver";

                    //bit7,6
                    if ((addrValue & Convert.ToInt32("11000000", 2)) == Convert.ToInt32("11000000", 2))
                    {
                        currItemDescription = "[bit7,6] Power Level 4 Module (>3.5W max. power dissipation.)";
                    }
                    else if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription = "[bit7,6] Power Level 3 Module (3.5W max. power dissipation.)";
                    }
                    else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                    {
                        currItemDescription = "[bit7,6] Power Level 2 Module (2.5W Max)";
                    }
                    else if ((addrValue & Convert.ToInt32("00000000", 2)) == Convert.ToInt32("00000000", 2))
                    {
                        currItemDescription = "[bit7,6] Power Level 1 Module (1.5 W max. power dissipation.)";
                    }

                    //bit5
                    if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                    {
                        currItemDescription += "\n ;[bit5]Non-CDR version of XFP";
                    }
                    else
                    {
                        currItemDescription += "\n ;[bit5]Module with CDR function";
                    }
                    //bit4
                    if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                    {
                        currItemDescription += "\n ;[bit4] TX Ref Clock Input Not Required";
                    }
                    else
                    {
                        currItemDescription += "\n ;[bit4] TX Ref Clock Input Required";
                    }

                    //bit3
                    if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                    {
                        currItemDescription += "\n ;[bit3] CLEI code present in Table 02h";
                    }
                    else
                    {
                        currItemDescription += "\n ;[bit3] No CLEI code present in Table 02h";
                    }
                    #endregion
                }

                else if (Address == 0x82)
                {
                    #region 0x82
                    itemName = "Connector values";
                    addressAllDescription = "Code for connector type";
                    if (addrValue == 0x00)
                    {
                        currItemDescription = "Unknown or unspecified";
                    }
                    else if (addrValue == 0x01)
                    {
                        currItemDescription = "SC";
                    }
                    else if (addrValue == 0x02)
                    {
                        currItemDescription = "Fibre Channel Style 1 copper connector";
                    }
                    else if (addrValue == 0x03)
                    {
                        currItemDescription = "Fibre Channel Style 2 copper connector";
                    }
                    else if (addrValue == 0x04)
                    {
                        currItemDescription = "BNC/TNC";
                    }
                    else if (addrValue == 0x05)
                    {
                        currItemDescription = "Fibre Channel coaxial headers";
                    }
                    else if (addrValue == 0x06)
                    {
                        currItemDescription = "FiberJack";
                    }
                    else if (addrValue == 0x07)
                    {
                        currItemDescription = "LC";
                    }
                    else if (addrValue == 0x08)
                    {
                        currItemDescription = "MT-RJ";
                    }
                    else if (addrValue == 0x09)
                    {
                        currItemDescription = "MU";
                    }
                    else if (addrValue == 0x0a)
                    {
                        currItemDescription = "SG";
                    }
                    else if (addrValue == 0x0b)
                    {
                        currItemDescription = "Optical pigtail";
                    }
                    else if (addrValue == 0x0c)
                    {
                        currItemDescription = "MPO Parallel Optic";
                    }
                    else if (addrValue == 0x20)
                    {
                        currItemDescription = "HSSDC II";
                    }
                    else if (addrValue == 0x21)
                    {
                        currItemDescription = "Copper pigtail";
                    }
                    else if (addrValue == 0x22)
                    {
                        currItemDescription = "RJ45";
                    }
                    else if (addrValue >= 0x80 && addrValue <= 0xff)
                    {
                        currItemDescription = "Vendor specific";
                    }
                    else
                    {
                        currItemDescription = "ERROR";
                    }
                    #endregion
                }
                else if (Address >= 0x83 && Address <= 0x8a)
                {
                    #region 0x83~0x8a
                    itemName = "Transceiver";
                    addressAllDescription = "Code for electronic or optical compatibility";

                    if (Address == 0x83)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "10GBASE-SR";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "10GBASE-LR";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "10GBASE-ER";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "10GBASE-LRM";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "10GBASE-SW";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "10GBASE-LW";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "10GBASE-EW";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Reserved";
                        }
                    }
                    if (Address == 0x84)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "1200-MX-SN-I";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "1200-SM-LL-L";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "Extended Reach 1550 nm";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "Intermediate Reach 1300 nm FP";
                        }
                        else
                        {
                            currItemDescription = "Reserved";
                        }
                    }
                    if (Address == 0x85)
                    {
                        currItemDescription = "Reserved";
                    }

                    if (Address == 0x86)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "1000BASE-SX / 1xFC MMF";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "1000BASE-LX/1xFC SMF";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "2xFC MMF";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "2xFC SMF";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "OC 48-SR";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "OC-48-IR";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "OC-48-LR";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Reserved";
                        }
                    }
                    if (Address == 0x87)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "I-64.1r";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "I-64.1";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "I-64.2r";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "I-64.2";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "I-64.3";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "I-64.5";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "Reserved";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Reserved";
                        }
                    }
                    if (Address == 0x88)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "S-64.1";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "S-64.2a";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "S-64.2b";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "S-64.3a";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "S-64.3b";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "S-64.5a";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "S-64.5b";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Reserved";
                        }
                    }
                    if (Address == 0x89)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "L-64.1";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "L-64.2a";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "L-64.2b";
                        }
                        else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                        {
                            currItemDescription = "L-64.2c";
                        }
                        if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                        {
                            currItemDescription = "L-64.3";
                        }
                        else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                        {
                            currItemDescription = "G.959.1 P1L1-2D2";
                        }
                        else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                        {
                            currItemDescription = "Reserved";
                        }
                        else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                        {
                            currItemDescription = "Reserved";
                        }
                    }
                    if (Address == 0x8a)
                    {
                        if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                        {
                            currItemDescription = "V-64.2a";
                        }
                        else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                        {
                            currItemDescription = "V-64.2b";
                        }
                        else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                        {
                            currItemDescription = "V-64.3";
                        }
                        else
                        {
                            currItemDescription = "Reserved";
                        }
                    }
                    #endregion
                }
                else if (Address == 0X8b)
                {
                    #region 0x8b
                    itemName = "Encoding";
                    addressAllDescription = "Code for high speed serial encoding algorithm";
                    if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription = "64B/66B";
                    }
                    else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                    {
                        currItemDescription = "8B10B";
                    }
                    else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                    {
                        currItemDescription = "SONET Scrambled";
                    }
                    else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                    {
                        currItemDescription = "NRZ";
                    }
                    else if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                    {
                        currItemDescription = "RZ";
                    }
                    else
                    {
                        currItemDescription = "Reserved";
                    }
                    #endregion
                }
                else if (Address == 0x8c)
                {
                    #region 0x0c
                    itemName = "BR-Min";
                    addressAllDescription = "Minimum bit rate, units of 100 MBits/s.";
                    currItemDescription = addrValue + "00Mbps";
                    #endregion
                }
                else if (Address == 0x8d)
                {
                    #region 0x8d
                    itemName = "BR-Max";
                    addressAllDescription = "Maximum bit rate, units of 100 MBits/s.";
                    currItemDescription = addrValue + "00Mbps";
                    #endregion
                }
                else if (Address == 14)
                {
                    #region 0x0e
                    itemName = "Length(SMF)-km";
                    addressAllDescription = "Link length supported for SMF fiber in km";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 254 km.";
                    }
                    else
                    {
                        currItemDescription = addrValue + "km";
                    }
                    #endregion
                }
                else if (Address == 0x8f)
                {
                    #region 0x8f
                    itemName = "Length (E-50μm)";
                    addressAllDescription = "Link length supported for EBW 50/125 μm fiber, units of 2 m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 508 m.";
                    }
                    else
                    {
                        currItemDescription = addrValue * 2 + "m";
                    }
                    #endregion
                }
                else if (Address == 0x90)
                {
                    #region 0x90
                    itemName = "Length (50 μm)";
                    addressAllDescription = "Link length supported for 50/125 μm fiber, units of 1 m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 254 m.";
                    }
                    else
                    {
                        currItemDescription = addrValue + "m";
                    }
                    #endregion
                }
                else if (Address == 0x91)
                {
                    #region 0x91
                    itemName = "Length (62.5 μm)";
                    addressAllDescription = "Link length supported for 62.5/125 μm fiber, units of 1 m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 254 m.";
                    }
                    else
                    {
                        currItemDescription = addrValue + "m";
                    }
                    #endregion
                }
                else if (Address == 0x92)
                {
                    #region 0x92
                    itemName = "Length (Copper)";
                    addressAllDescription = "Link length supported for copper, units of 1m";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Not supported";
                    }
                    else if (addrValue == 255)
                    {
                        currItemDescription = "supports a link length greater than 254 meters.";
                    }
                    else
                    {
                        currItemDescription = addrValue + "m";
                    }
                    #endregion
                }
                else if (Address == 0x93)
                {
                    #region 0x93
                    //bit7-4 Transmitter technology (see Table 52)                    
                    itemName = "Device Tech";
                    addressAllDescription = "Device technology";
                    if ((addrValue & Convert.ToInt32("00000000", 2)) == Convert.ToInt32("00000000", 2))
                    {
                        currItemDescription = "850 nm VCSEL";
                    }
                    else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                    {
                        currItemDescription = "1310 nm VCSEL";
                    }
                    else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                    {
                        currItemDescription = "1550 nm VCSEL";
                    }
                    else if ((addrValue & Convert.ToInt32("00110000", 2)) == Convert.ToInt32("00110000", 2))
                    {
                        currItemDescription = "1310 nm FP";
                    }
                    else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                    {
                        currItemDescription = "1310 nm DFB";
                    }
                    else if ((addrValue & Convert.ToInt32("01010000", 2)) == Convert.ToInt32("01010000", 2))
                    {
                        currItemDescription = "1550 nm DFB";
                    }
                    else if ((addrValue & Convert.ToInt32("01100000", 2)) == Convert.ToInt32("01100000", 2))
                    {
                        currItemDescription = "1310 nm EML";
                    }
                    else if ((addrValue & Convert.ToInt32("01110000", 2)) == Convert.ToInt32("01110000", 2))
                    {
                        currItemDescription = "1550 nm EML";
                    }
                    else if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription = "Copper or others";
                    }
                    else
                    {
                        currItemDescription = "Reserved";
                    }

                    //bit3
                    if ((addrValue & Convert.ToInt32("1000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription += "\n ;Active wavelength control";
                    }
                    else
                    {
                        currItemDescription += "\n ;No wavelength control";
                    }
                    //bit2
                    if ((addrValue & Convert.ToInt32("100", 2)) == Convert.ToInt32("100", 2))
                    {
                        currItemDescription += "\n ;Cooled transmitter";
                    }
                    else
                    {
                        currItemDescription += "\n ;Uncooled transmitter device";
                    }
                    //bit1
                    if ((addrValue & Convert.ToInt32("10", 2)) == Convert.ToInt32("10", 2))
                    {
                        currItemDescription += "\n ;Detector Type:APD detector";
                    }
                    else
                    {
                        currItemDescription += "\n ;Detector Type:PIN detector";
                    }
                    //bit0
                    if ((addrValue & Convert.ToInt32("1", 2)) == Convert.ToInt32("1", 2))
                    {
                        currItemDescription += "\n ;Transmitter Tunable";
                    }
                    else
                    {
                        currItemDescription += "\n ;Transmitter not Tunable";
                    }
                    #endregion
                }
                else if (Address >= 0x94 && Address <= 0xA3)
                {
                    #region 0x94~0xA3
                    itemName = "Vendor name";
                    addressAllDescription = "XFP vendor name (ASCII)";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }
                else if (Address == 0xA4)
                {
                    #region 0xA4
                    itemName = "CDR Support";
                    addressAllDescription = "CDR Rate Support";
                    //7 CDR support for 9.95 Gb/s
                    //6 CDR support for 10.3 Gb/s
                    //5 CDR support for 10.5 Gb/s
                    //4 CDR support for 10.7 Gb/s
                    //3 CDR support for 11.1 Gb/s
                    //2 Reserved
                    //1 Lineside Loopback Mode Supported
                    //0 XFI Loopback Mode Supported
                    if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription = "CDR support for 9.95 Gb/s";
                    }
                    else if ((addrValue & Convert.ToInt32("01000000", 2)) == Convert.ToInt32("01000000", 2))
                    {
                        currItemDescription = "CDR support for 10.3 Gb/s";
                    }
                    else if ((addrValue & Convert.ToInt32("00100000", 2)) == Convert.ToInt32("00100000", 2))
                    {
                        currItemDescription = "CDR support for 10.5 Gb/s";
                    }
                    else if ((addrValue & Convert.ToInt32("00010000", 2)) == Convert.ToInt32("00010000", 2))
                    {
                        currItemDescription = "CDR support for 10.7 Gb/s";
                    }
                    else if ((addrValue & Convert.ToInt32("00001000", 2)) == Convert.ToInt32("00001000", 2))
                    {
                        currItemDescription = "CDR support for 11.1 Gb/s";
                    }
                    else if ((addrValue & Convert.ToInt32("00000100", 2)) == Convert.ToInt32("00000100", 2))
                    {
                        currItemDescription = "Reserved";
                    }
                    else if ((addrValue & Convert.ToInt32("00000010", 2)) == Convert.ToInt32("00000010", 2))
                    {
                        currItemDescription = "Lineside Loopback Mode Supported";
                    }
                    else if ((addrValue & Convert.ToInt32("00000001", 2)) == Convert.ToInt32("00000001", 2))
                    {
                        currItemDescription = "XFI Loopback Mode Supported";
                    }
                    #endregion
                }
                else if (Address >= 0xa5 && Address <= 0xa7)
                {
                    #region 0xa5~0xa7
                    itemName = "Vendor OUI";
                    addressAllDescription = "XFP vendor IEEE company ID:"
                    + "\n The vendor organizationally unique identifier field (vendor OUI) is a 3-byte field that contains"
                    + " the IEEE Company Identifier for the vendor. A value of all zero in the 3-byte field indicates that "
                    + " the Vendor OUI is unspecified.";

                    currItemDescription = addrValue.ToString("X").PadLeft(2, '0');  //141024_0

                    #endregion
                }
                else if (Address >= 0xa6 && Address <= 0xb5)
                {
                    //TBD 同时...
                    #region 0xa6~0xb5
                    itemName = "Vendor PN";
                    addressAllDescription = "Part number provided by XFP vendor (ASCII)";
                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }

                else if (Address >= 0xb6 && Address <= 0xb9)
                {
                    #region 0xb6~0xb9
                    itemName = "Vendor rev";
                    addressAllDescription = "Revision level for part number provided by vendor (ASCII)";
                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }

                else if (Address >= 0xba && Address <= 0xbb)
                {
                    //TBD 同时...
                    #region 0xba~0xbb
                    itemName = "Wavelength";
                    addressAllDescription = "Nominal laser wavelength (Wavelength = value / 20 in nm)";
                    currItemDescription = ((objValues[0xba - 0x80] * 256 + objValues[0xbb - 0x80]) / 20).ToString() + "nm";

                    #endregion
                }
                else if (Address >= 0xbc && Address <= 0xbd)
                {
                    #region 0xbc,0xbd
                    itemName = "Wavelength Tolerance";
                    addressAllDescription = "Guaranteed range of laser wavelength (+/- value) from Nominal wavelength.(Wavelength Tol. = value/200 in nm)";
                    currItemDescription = ((objValues[0xbc - 0x80] * 256 + objValues[0xbd - 0x80]) / 20).ToString() + "nm";
                    #endregion
                }
                else if (Address == 0xbe)
                {
                    #region 0xbe
                    itemName = "Max Case Temp";
                    addressAllDescription = "Maximum Case Temperature in Degrees C.[Allows specification of a maximum case temperature other than the MSA "
                        + "standard of 70C. Maximum case temperature is an 8-bit value in Degrees C.]";
                    currItemDescription = addrValue.ToString();
                    #endregion
                }

                else if (Address == 0xbf)
                {
                    #region 0xbf
                    itemName = "CC_BASE";
                    addressAllDescription = "Check code for Base ID Fields (addresses 120-190)";
                    currItemDescription = "CS=" + (addrValue).ToString("X");
                    #endregion
                }

                else if (Address >= 0xc0 && Address <= 0xc3)
                {
                    #region 0xc0~0xc3
                    //Options Indicates which optional transceiver signals are implemented
                    itemName = "Power Supply";
                    addressAllDescription = "Power supply current requirements and max power dissipation";
                    //192 7-0 Maximum Power Dissipation
                    //Max power is 8 bit value * 20 mW.
                    if (Address == 0xc0)
                    {
                        currItemDescription = "[Maximum Power Dissipation]:" + (addrValue * 20).ToString() + "mW";
                    }
                    //193 7-0 Maximum Total Power Dissipation in Power Down Mode
                    //Max Power is 8 bit value * 10 mW.
                    if (Address == 0xc1)
                    {
                        currItemDescription = "[Maximum Total Power Dissipation in Power Down Mode]:" + (addrValue * 10).ToString() + "mW";
                    }
                    //194 7-4 Maximum current required by +5V Supply.
                    //Max current is 4 bit value * 50 mA. [500 mA max]
                    //194 3-0 Maximum current required by +3.3V Supply.
                    //Max current is 4 bit value * 100 mA.
                    if (Address == 0xc2)
                    {
                        currItemDescription = "[Maximum current required by +5V Supply[500 mA max]]:" + ((addrValue & (Convert.ToInt32("11110000", 2) >> 4)) * 50).ToString() + "mA";
                        currItemDescription += "[ Maximum current required by +3.3V Supply.]:" + (addrValue & (Convert.ToInt32("11110000", 2)) * 100).ToString() + "mA";
                    }
                    //195 7-4 Maximum current required by +1.8V Supply
                    //Max current is 4 bit value * 100 mA.
                    //195 3-0 Maximum current required by -5.2V Supply.
                    //Max current is 4 bit value * 50 mA. [500 mA max]
                    if (Address == 0xc3)
                    {
                        currItemDescription = "[Maximum current required by +1.8V Supply]:" + ((addrValue & (Convert.ToInt32("11110000", 2) >> 4)) * 100).ToString() + "mA";
                        currItemDescription += "[ Maximum current required by -5.2V Supply[500 mA max]]:" + (addrValue & (Convert.ToInt32("11110000", 2)) * 50).ToString() + "mA";
                    }

                    #endregion
                }
                else if (Address >= 0xc4 && Address <= 0xd3)
                {
                    //TBD 同时...
                    #region 0xc4~0xd3
                    itemName = "Vendor SN";
                    addressAllDescription = "Serial number provided by vendor (ASCII)";

                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }
                else if (Address >= 0xd4 && Address <= 0xdb)
                {
                    //TBD 同时...
                    #region 0xd4~0xdb
                    itemName = "Date code";
                    addressAllDescription = "Vendor's manufacturing date code";
                    currItemDescription = ((char)addrValue).ToString();

                    #endregion
                }
                else if (Address == 0xdc)
                {
                    #region 0xdc
                    itemName = "Diagnostic Monitoring Type";
                    addressAllDescription = "Indicates which type of diagnostic monitoring is implemented (if any) in the transceiver";
                    if ((addrValue & Convert.ToInt32("10000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription += "Module Respond to FEC BER =BER Support";
                    }
                    else
                    {
                        currItemDescription += "Module Respond to FEC BER  No BER Support";
                    }
                    if ((addrValue & Convert.ToInt32("10000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription += "Received power measurement type = Average Power";
                    }
                    else
                    {
                        currItemDescription += "Received power measurement type = OMA";
                    }
                    #endregion
                }
                else if (Address == 0xdd)
                {
                    #region 0xdd
                    itemName = "Enhanced Options";
                    addressAllDescription = "Indicates which optional enhanced features are implemented (if any) in the transceiver";

                    if ((addrValue & Convert.ToInt32("10000000", 2)) == Convert.ToInt32("10000000", 2))
                    {
                        currItemDescription += "Module Supports Optional VPS.";
                    }
                    if ((addrValue & Convert.ToInt32("1000000", 2)) == Convert.ToInt32("1000000", 2))
                    {
                        currItemDescription += "\n Optional Soft TX_DISABLE implemented";
                    }
                    if ((addrValue & Convert.ToInt32("100000", 2)) == Convert.ToInt32("100000", 2))
                    {
                        currItemDescription += "\n Optional Soft P_down implemented";
                    }
                    if ((addrValue & Convert.ToInt32("10000", 2)) == Convert.ToInt32("10000", 2))
                    {
                        currItemDescription += "\n Supports VPS LV regulator mode";
                    }
                    if ((addrValue & Convert.ToInt32("1000", 2)) == Convert.ToInt32("1000", 2))
                    {
                        currItemDescription += "\n Supports VPS bypassed regulator Mode";
                    }
                    if ((addrValue & Convert.ToInt32("100", 2)) == Convert.ToInt32("100", 2))
                    {
                        currItemDescription += "\n Active FEC control functions implemented";
                    }
                    if ((addrValue & Convert.ToInt32("10", 2)) == Convert.ToInt32("10", 2))
                    {
                        currItemDescription += "\n Wavelength tunability implemented";
                    }
                    if ((addrValue & Convert.ToInt32("1", 2)) == Convert.ToInt32("1", 2))
                    {
                        currItemDescription += "\n Optional CMU Support Mode";
                    }

                    #endregion
                }
                else if (Address == 0xde)
                {
                    #region 0xde
                    itemName = "Aux Monitoring";
                    addressAllDescription = "Defines quantities reported by Aux. A/D channels.";
                    if (addrValue == 0)
                    {
                        currItemDescription = "Auxiliary monitoring not implemented";
                    }
                    else if (addrValue == 0x01)
                    {
                        currItemDescription = "APD Bias Voltage (16 bit value is Voltage in units of 10 mV)";
                    }
                    else if (addrValue == 0x02)
                    {
                        currItemDescription = "Reserved";
                    }
                    else if (addrValue == 0x03)
                    {
                        currItemDescription = "TEC Current (mA) (16 bit value is Current in units of 100 uA)";
                    }
                    else if (addrValue == 0x04)
                    {
                        currItemDescription = "Laser Temperature (Same encoding as module temperature)";
                    }
                    else if (addrValue == 0x05)
                    {
                        currItemDescription = "Laser Wavelength (same Encoding as Bytes 186-187)";
                    }
                    else if (addrValue == 0x06)
                    {
                        currItemDescription = "+5V Supply Voltage";
                    }
                    else if (addrValue == 0x07)
                    {
                        currItemDescription = "+3.3V Supply Voltage";
                    }
                    else if (addrValue == 0x08)
                    {
                        currItemDescription = "+1.8V Supply Voltage";
                    }
                    else if (addrValue == 0x09)
                    {
                        currItemDescription = "-5.2V Supply Voltage (Absolute Value Encoded as primary Voltage Monitor)";
                    }
                    else if (addrValue == 0x0a)
                    {
                        currItemDescription = "+5V Supply Current (16 bit Value is Current in 100 uA)";
                    }
                    else if (addrValue == 0x0d)
                    {
                        currItemDescription = "+3.3V Supply Current (16 bit Value is Current in 100 uA)";
                    }
                    else if (addrValue == 0x0e)
                    {
                        currItemDescription = "+1.8V Supply Current (16 bit Value is Current in 100 uA)";
                    }
                    else if (addrValue == 0x0f)
                    {
                        currItemDescription = "-5.2V Supply Current(16 bit Value is Current in 100 uA)";
                    }
                    else
                    {
                        currItemDescription = "Error";
                    }
                    #endregion
                }
                else if (Address == 0xdf)
                {
                    #region 0xdf
                    itemName = "CC_EXT";
                    addressAllDescription = "Check code for the Extended ID Fields (addresses 192 to 222)";
                    currItemDescription = "CS2=" + (addrValue).ToString("X");

                    #endregion
                }
                else if (Address >= 0xe0 && Address <= 0xFF)
                {
                    //TBD 同时...
                    #region 0xe0~0xFF
                    itemName = "Vendor Specific";
                    addressAllDescription = "Vendor Specific EEPROM";

                    currItemDescription = ((char)addrValue).ToString();
                    #endregion
                }
                #endregion
            }
            else if (page.ToUpper() == XFPPages.A0H_Page2.ToString().ToUpper())
            {
                #region A0H_Page2
                //DESCRIPTION OF UPPER MEMORY MAP TABLE 02H – USER EEPROM DATA
                //Table 02h is provided as user writable EEPROM. The host system may
                //read or write this memory for any purpose. If bit 3 of Table 01h Byte 129
                //is set, however, the first 10 bytes of Table 02h [128-137h] will be used to
                //store the CLEI code for the module.
                if (Address >= 0x80 && Address <= 0x8a)
                {
                    #region 0x80~0x8a
                    itemName = "Transceiver";
                    addressAllDescription = "Code for electronic or optical compatibility";
                    currItemDescription = "store the CLEI code for the module";
                    #endregion
                }
                else
                {
                    itemName = "USER EEPROM DATA";
                    addressAllDescription = "USER EEPROM DATA";
                    currItemDescription = (addrValue).ToString("X");
                }
                #endregion
            }
            return currItemDescription;
        }

        override protected bool EngMode(byte pageNo)
        {
            try
            {
                byte[] buff = new byte[5];
                buff[0] = 0xca;
                buff[1] = 0x2d;
                buff[2] = 0x81;
                buff[3] = 0x5f;
                buff[4] = pageNo;
                USBIO.WrtieReg(deviceIndex, 0xA0, 123, IOPort.SoftHard.HARDWARE_SEQUENT, buff);
                System.Threading.Thread.Sleep(100);
                return true;
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.ToString());
                return false;
            }
        }

        bool readA0HLowmemory()
        {
            byte[] buff = new byte[1] { 0 };
            WriteBytes(0xa0, 0x7f, 1, buff);
            if (ReadBytes(0xa0, 0, 128, out ReadData1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool readA0HPage1()
        {
            byte[] buff = new byte[1] { 1 };
            WriteBytes(0xa0, 0x7f, 1, buff);
            if (ReadBytes(0xa0, 128, 128, out ReadData0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool readA0HPage2()
        {
            byte[] buff = new byte[1] { 2 };
            WriteBytes(0xa0, 0x7f, 1, buff);
            if (ReadBytes(0xa0, 128, 128, out ReadData2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        override public bool EEPROMRead()
        {
            if (readA0HLowmemory() && readA0HPage1() && readA0HPage2())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        override public bool EEPROMWrite()
        {
            if (writeA0HLowMemory() && writeA0HPage1() && writeA0HPage2())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool writeA0HLowMemory()
        {
            EngMode(0);
            if (WriteBytes(0xa0, 0, 58, Data1) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool writeA0HPage1()
        {
            EngMode(1);
            if (WriteBytes(0xa0, 128, 128, Data0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool writeA0HPage2()
        {
            EngMode(2);
            if (WriteBytes(0xa0, 128, 128, Data2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
    }

    public class CFP : EEPROMOperation
    {
        public CFP(int INDEX, bool isFristLoad) : base(INDEX, isFristLoad) { }
        public CFP() : base() { }
    }
}
