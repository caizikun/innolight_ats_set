using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maintain
{
    public class EEPROMOperation
    {       
        static bool blnMsgresult = false;
        public EEPROMOperation(int INDEX,bool isFristLoad)
        {
            //141016_1 如加上此项将报错...TBD 是否需要连接IIC
            if (isFristLoad)
            {
                System.Windows.Forms.DialogResult Msgresult =
                System.Windows.Forms.MessageBox.Show("请确认目前已经连接好了I2C or MIDO 控制板?", "注意:",
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
        //QSFP:DATA0->A0H_Page0,DATA1->A0H_Page3

        //pwd TBD..141016_1
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
  
    }         
    public class SFP : EEPROMOperation //pwd TBD..141016_1
    {
        public SFP(int INDEX, bool isFristLoad) : base(INDEX, isFristLoad) { }
        //SFP:DATA0->A0H,DATA1->A2H_LowMemory,DATA2->A2H_Page0

        //pwd TBD..141016_1
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
            if (WriteBytes(0xa2, 0, 56, Data1) && WriteBytes(0xa2, 0x80, 128, Data2))
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
        //XFP:DATA1->A0H_LowMemory,DATA0->A0H_Page1,DATA2->A0H_Page2

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
    }
}
