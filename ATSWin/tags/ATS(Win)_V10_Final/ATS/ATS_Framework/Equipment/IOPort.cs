using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivi.Visa.Interop;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ATS_Framework
{
    
    public class IOPort
    {
        //protected Semaphore semaphore;
        public logManager log;
        public bool Connect_flag = false;// have used
        public Ivi.Visa.Interop.ResourceManager rm;   // VIsa  GPIB
        public Ivi.Visa.Interop.FormattedIO488 myDmm;// VIsa  GPIB
        protected string Str_IO_interface;
        public string ioaddr;
        [DllImport("CH375DLL.dll")]
        static extern int CH375ResetDevice(byte iIndex);// 复位CH375设备,返回句柄,出错则无效
        [DllImport("CH375DLL.dll")]
        static extern int CH375OpenDevice(byte iIndex);// 打开CH375设备,返回句柄,出错则无效
        [DllImport("CH375DLL.dll")]
        static extern int CH375CloseDevice(byte iIndex);//关闭USB设备，此函数一定要被调用。建议在关闭设备并退出应用程序后再拔出USB电缆。
        [DllImport("CH375DLL.dll")]
        static extern void CH375SetTimeout(byte iIndex, ushort iwritetimeout, ushort ireadtimeout);
        [DllImport("CH375DLL.dll")]
        static extern int CH375ReadData(Int16 iIndex, // 指定USB设备序号，下同
                                       [MarshalAs(UnmanagedType.LPArray)] byte[] buff, // 指向一个足够大的缓冲区,用于保存读取的数据
                                       ref int ioLength); // 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度，最大长度为64个字节。
        [DllImport("CH375DLL.dll")]
        static extern bool CH375WriteData(Int16 iIndex, // 指定USB设备序号 
                                        [MarshalAs(UnmanagedType.LPArray)] byte[] buff, // 指向一个缓冲区,放置准备写出的数据
                                        ref int ioLength); // 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度，最大长度为64个字节。

        public static bool CH375WriteData(int iIndex, byte[] iBuffer) // 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度，最大长度为64个字节。
        {
            int l = iBuffer.Length;
            return CH375WriteData((byte)iIndex, iBuffer, ref l);
        }
        public static int CH375ReadData(int iIndex, byte[] iBuffer) // 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度，最大长度为64个字节。
        {
            int l = iBuffer.Length;
            CH375ReadData((byte)iIndex, iBuffer, ref l);
            return l;
        }

        public static byte[] CH375ReadData(int iIndex, int len) // 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度，最大长度为64个字节。
        {
            byte[] buff = new byte[len];
            int l = buff.Length;
            CH375ReadData((byte)iIndex, buff, ref l);

            byte[] values = new byte[l];
            Array.Copy(buff, values, l);
            return values;
        }

        public static void CloseDevice(int deviceSelect)
        {
            CH375CloseDevice((byte)deviceSelect);
        }

        public enum ReadWrite : byte
        {
            Read = 0,
            Write = 1,
        }

        public enum SoftHard : byte
        {
            HARDWARE_SEQUENT = 0xA8,
            SOFTWARE_SEQUENT = 0xA9,
            HARDWARE_SINGLE = 0xA3,
            SOFTWARE_SINGLE = 0xA4,
        }
        public enum MDIOSoftHard : byte
        {
            HARDWARE = 0xAB,
            SOFTWARE = 0xA7,
        }
        public static bool resetdevice(int deviceSelect)
        {
           int x=  CH375ResetDevice((byte)deviceSelect);
            return x!=0;
        }
        public static bool OpenDevice(int deviceSelect)
        {
            int x = CH375ResetDevice((byte)deviceSelect);
           // Thread.Sleep(20);
            int r = CH375OpenDevice((byte)deviceSelect);
           // Thread.Sleep(10);
            return r != 0;
        }

        public static int ReadID(int driverIndex)
        {
            return IDDetect((byte)driverIndex, ReadWrite.Read, 0);
        }

        public static int IDDetect(int driverSelect, ReadWrite readWrtie, byte idInput)
        {
            CH375SetTimeout((byte)driverSelect, 1000, 1000);

            byte[] buff = new byte[5];
            buff[0] = buff[1] = 0;
            buff[2] = 0xB3;
            buff[3] = (byte)readWrtie;
            buff[4] = idInput;

            CH375WriteData(driverSelect, buff);

            byte[] readBuff = new byte[1];
            CH375ReadData(driverSelect, readBuff);

            return readBuff[0];
        }

        public enum CFKType : byte
        {
            _400K = 0,
            _200K = 1,
            _100K = 2,
            _50K = 3,
            _25K = 4,
        }
        public enum MDIOCFKType : byte
        {
            _4M = 0,
            _2M = 1,
            _1M = 2,
            _500K = 3,
            _250K = 4,
            _125K = 5,
            _625K = 6,//62.5K
        }
        public IOPort(string IO_Type, string Addr, logManager logmanager)
        {
            log = logmanager;
            ioaddr = Addr;
            Str_IO_interface = IO_Type;
            
        }
        public bool IOConnect()
        {
            try
            {
                switch (Str_IO_interface)
                {

                    case "GPIB":

                        rm = new Ivi.Visa.Interop.ResourceManager(); //Open up a new resource manager
                        myDmm = new Ivi.Visa.Interop.FormattedIO488(); //Open a new Formatted IO 488 session 
                        myDmm.IO = (IMessage)rm.Open(ioaddr, AccessMode.NO_LOCK, 5000, ""); //Open up a handle to the DMM with a 2 second timeout
                        myDmm.IO.Timeout = 5000; //You can also set your timeout by doing this command, sets to 3 seconds
                        myDmm.IO.Clear(); //Send a device clear first to stop any measurements in process    
                        //myDmm.WriteString("*RST", true);

                        break;
                    case "USB":
                        OpenDevice(Convert.ToByte(ioaddr));
                        break;
                    case "RJ45":// 网口
                        break;
                    case "RS232":// 网口
                        break;
                    default:
                        break;
                }
                Connect_flag = true;
                return Connect_flag;
            }
            catch (Exception me)
            {
                log.AdapterLogString(3, me.Message);
                return Connect_flag;
            }
        
        
        }
        static Semaphore semaphore = new Semaphore(1, 1);
        public static byte[] ReadWriteReg(int deviceIndex, int deviceAddress, int regAddress, bool regAddressWide, SoftHard softHard,
            ReadWrite operate, CFKType cfk, byte[] buffer)
        {
            semaphore.WaitOne();
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);

            byte[] arr = new byte[buffer.Length + 8];

            arr[0] = regAddressWide ? (byte)1 : (byte)0;
            arr[1] = (byte)(regAddress / 256);
            arr[2] = (byte)softHard;
            arr[3] = (byte)operate;
            arr[4] = (byte)deviceAddress;
            arr[5] = (byte)(regAddress & 0xFF);
            arr[6] = (byte)buffer.Length;
            arr[7] = (byte)cfk;
            buffer.CopyTo(arr, 8);
            bool b = CH375WriteData(deviceIndex, arr);
            byte[] arrRead = CH375ReadData(deviceIndex, buffer.Length);
            CloseDevice(deviceIndex);
            semaphore.Release();
            System.Threading.Thread.Sleep(200);
            return arrRead;
            

        }
        public static byte[] ReadWriteMDIO(int deviceIndex, int deviceAddress, int phycialAddress, int regAddress, bool regAddressWide, MDIOSoftHard softHard,
            ReadWrite operate, MDIOCFKType cfk, byte[] buffer)
        {
            //semaphore.WaitOne();
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);

            byte[] arr = new byte[buffer.Length + 10];
            arr[0] = 0;
            arr[1] = 0;
            arr[2] = (byte)softHard;
            arr[3] = (byte)operate;
            arr[4] = (byte)cfk;
            arr[5] = (byte)phycialAddress;
            arr[6] = (byte)deviceAddress;
            arr[7] = (byte)(regAddress / 256);
            arr[8] = (byte)(regAddress & 0xFF);
            arr[9] = (byte)(buffer.Length);
            buffer.CopyTo(arr, 10);
            bool b = CH375WriteData(deviceIndex, arr);
            byte[] arrRead = CH375ReadData(deviceIndex, buffer.Length);
            CloseDevice(deviceIndex);
            //semaphore.Release();
            System.Threading.Thread.Sleep(200);
            return arrRead;


        }
        public static byte[] ReadWriteMDIO(int deviceIndex, int deviceAddress, int phycialAddress, int regAddress, bool regAddressWide, MDIOSoftHard softHard,
           ReadWrite operate, MDIOCFKType cfk, UInt32 buffer)
        {
            //semaphore.WaitOne();
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);

            byte[] arr = new byte[2 + 10];
            arr[0] = 0;
            arr[1] = 0;
            arr[2] = (byte)softHard;
            arr[3] = (byte)operate;
            arr[4] = (byte)cfk;
            arr[5] = (byte)phycialAddress;
            arr[6] = (byte)deviceAddress;
            arr[7] = (byte)(regAddress / 256);
            arr[8] = (byte)(regAddress & 0xFF);
            arr[9] = (byte)(1);
            arr[8] = (byte)(regAddress & 0xFF);
            arr[9] = (byte)(1);

            bool b = CH375WriteData(deviceIndex, arr);

            byte[] arrRead = CH375ReadData(deviceIndex, 1);
            CloseDevice(deviceIndex);
            //semaphore.Release();
            System.Threading.Thread.Sleep(200);
            return arrRead;


        }
        public UInt16[] ReadMDIO(int deviceIndex, int deviceAddress, int phycialAddress, int regAddress, MDIOSoftHard softHard, int readLength)
        {
            UInt16[] returndata = new UInt16[readLength];
            byte[] readdata = new byte[readLength * 2];
            readdata = ReadWriteMDIO(deviceIndex, deviceAddress, phycialAddress, regAddress, false, softHard, ReadWrite.Read,
                (MDIOCFKType)2, new byte[readLength*2]);
            int j = 0;
            for (int i = 0; i < readdata.Length; i=i+2)
            {
                returndata[j]=(UInt16)((readdata[i] * 256) + readdata[i + 1]);
                j++;
            }
                return returndata;
        }

        public byte[] WriteMDIO(int deviceIndex, int deviceAddress, int phycialAddress, int regAddress, MDIOSoftHard softHard, UInt16[] dataToWrite)
        {
            byte[] writedata=new byte[dataToWrite.Length*2];
            int j = 0;
            for (int i = 0; i < dataToWrite.Length; i++)
            {
                writedata[j] = (byte)(dataToWrite[i] / 256);
                writedata[j+1] = (byte)(dataToWrite[i] & 0xFF);
                j = j + 2;
            }
            return ReadWriteMDIO(deviceIndex, deviceAddress, phycialAddress,regAddress, false, softHard, ReadWrite.Write,
                (MDIOCFKType)2, writedata);
        }
        public  bool WritePort(int id, int deviceIndex, int Port, int DDR)
        {
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);
            byte[] buffer = new byte[6];
            buffer[0] = (byte)id;
            buffer[1] = 0;
            buffer[2] = 0xA2;
            buffer[3] = 1;
            buffer[4] = (byte)Port;
            buffer[5] = (byte)DDR;

            CH375WriteData(deviceIndex, buffer);

            byte[] buf = CH375ReadData(deviceIndex, 1);
            CloseDevice(0);
            if (buf.Length < 1) return false;
            return buf[0] == 0xAA;
        }
        public byte[] ReadPort(int id, int deviceIndex, int Port, int DDR)
        {
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);
            byte[] buffer = new byte[6];
            buffer[0] = (byte)id;
            buffer[1] = 0;
            buffer[2] = 0xA2;
            buffer[3] = 0;
            buffer[4] = (byte)Port;
            buffer[5] = (byte)DDR;
            CH375WriteData(deviceIndex, buffer);

            byte[] buf = CH375ReadData(deviceIndex, 1);
            CloseDevice(0);
            return buf;
        }

        public  byte[] ReadReg(int deviceIndex, int deviceAddress, int regAddress, SoftHard softHard, int readLength)
        {
            return ReadWriteReg(deviceIndex, deviceAddress, regAddress, false, softHard, ReadWrite.Read,
                (CFKType)0, new byte[readLength]);
        }

        public  byte[] WrtieReg(int deviceIndex, int deviceAddress, int regAddress, SoftHard softHard, byte[] dataToWrite)
        {
            return ReadWriteReg(deviceIndex, deviceAddress, regAddress, false, softHard,ReadWrite.Write,
                (CFKType)0, dataToWrite);
        }
       // logManager log=new logManager();
        public bool WriteString(string Str_Write)
        {
            try
            {
                switch (Str_IO_interface)
                {
                    case "GPIB":
                        myDmm.WriteString(Str_Write, true);
                        break;
                    case "USB":
                        
                        break;
                    case "RJ45":// 网口
                        break;
                    case "RS232":// 网口
                        break;

                }
                return true;
            }
           
            catch (Exception error)
            {
                log.AdapterLogString(3, error.Message);
                return false;
            }
        }
        public string ReadString(int count)
        {
            byte[] arr = new byte[count];
            string Str_Read = null;
            try
            {
                arr = myDmm.IO.Read(count);

                Str_Read = System.Text.Encoding.Default.GetString(arr);
                return Str_Read;
            }
            catch (Exception ex)
            {
                log.AdapterLogString(3, ex.Message);
                return Str_Read;
            }
            
        }
        public string ReadString()
        {
            string Str = "";
            try
            {
                switch (Str_IO_interface)
                {
                    case "GPIB":
                        Str = myDmm.ReadString();
                        break;
                    case "USB":
                        break;
                    case "RJ45":// 网口
                        break;
                    case "RS232":// 网口
                        break;

                }
                return Str;
            }
           
            catch (Exception error)
            {
                log.AdapterLogString(3, error.Message);
                return Str;
            }
        }

#region  25G-BERT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceIndex"> Use地址 </param>
        /// <param name="RegistAddr">  寄存器地址 </param>
        /// <param name="WriteData">  写入值 </param>
        /// <param name="Operate">0=Read, 1=Write,2=Modesel_Set,3=Modsel_Enable</param>
        ///
        public Int32 ClockChip_WriteData_24bit(int deviceIndex, int RegistAddr, int WriteData,IOPort.ReadWrite Opreate)//Oerate 0=Read; 1=Write;2=Modesel_Set;3=Modsel_Enable
        {
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);

            byte[] Buff = new byte[8];
            int ReadData=-1;
            try
            {
                Buff[0] = 0;
                Buff[1] = 0;
                Buff[2] =0XC0;
                Buff[3] = Convert.ToByte(Opreate);
                Buff[4] = Convert.ToByte(RegistAddr);
                Buff[5] = Convert.ToByte(WriteData / 65536);
                int k = Convert.ToInt32(WriteData % 65536);
                Buff[6] = Convert.ToByte(k / 256);
                Buff[7] = Convert.ToByte(k % 256);

                CH375WriteData(deviceIndex, Buff);
                Thread.Sleep(30);
                if (Opreate==ReadWrite.Read)
                {

                    byte[] ReadBuff = CH375ReadData(deviceIndex, 3);

                    ReadData = ReadBuff[0] * 65536 + ReadBuff[1] * 256 + ReadBuff[2];

                }
                else
                {
                    byte[] ReadBuff = CH375ReadData(deviceIndex, 1);

                    ReadData = Convert.ToInt32(ReadBuff[0]);

                }
                CloseDevice(deviceIndex);
                 return ReadData;

            }
            catch (System.Exception ex)
            {
                return ReadData;
            }
            

           
        }       
  
        public Int32[] Regist_24Bit_GT174(int deviceIndex, int DelayCoef, int[] WriteData, int Length, int StartRegistAddr,ReadWrite Opreat, byte deviceAddr, IOPort.CFKType CFK, byte Select1724)
        {
            byte[] ReadArray1 ;
            Int32[] ReadArray ;

            byte[] arr;

            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);

             if (Opreat==ReadWrite.Write)
             {
                arr = new byte[WriteData.Length + 9];
                ReadArray = new Int32[1];
             }
             else
             {
                 arr = new byte[9];
                 ReadArray = new Int32[Length];
             }
            try
             {


                 arr[0] = 1;
                 arr[1] = Convert.ToByte(StartRegistAddr / 65536);

                 if (Select1724 == 0)
                 {
                     arr[2] = 0xBC;
                 }
                 else
                 {
                     arr[2] = 0xBE;
                 }
                 arr[3] = Convert.ToByte(Opreat);
                 arr[4] = deviceAddr;
                 arr[5] = Convert.ToByte((StartRegistAddr % 65536) / 256);
                 arr[6] = Convert.ToByte(Length);
                 arr[7] = Convert.ToByte(DelayCoef + Convert.ToByte(CFK) * 0X20);
                 arr[8] = Convert.ToByte((StartRegistAddr % 65536) % 256);

                 if (Opreat == ReadWrite.Write)
                 {
                     for (int i = 0; i < WriteData.Length; i++)
                     {
                         arr[9 + i] = Convert.ToByte(WriteData[i]);
                     }
                 }

                 IOPort.CH375WriteData(deviceIndex, arr);
                 Thread.Sleep(30);
                 if (Opreat == ReadWrite.Write)
                 {
                     ReadArray1 = IOPort.CH375ReadData(deviceIndex, 1);
                 }
                 else
                 {
                     ReadArray1 = IOPort.CH375ReadData(deviceIndex, Length);
                 }
                 ReadArray1.CopyTo(ReadArray, 0);
             }
            catch
             {

             }
             CloseDevice(deviceIndex);
            return ReadArray;
        }
        public Int32[] Regist_16Bit_GT174(int deviceIndex, int DelayCoef, int[] WriteData, int Length, int StartRegistAddr, ReadWrite Opreat, byte deviceAddr, IOPort.CFKType CFK, byte Select1724)
        {
            byte[] ReadArray1;
            Int32[] ReadArray;
            byte[] arr;
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);
            if (Opreat == ReadWrite.Write)
            {
                arr = new byte[WriteData.Length + 8];
                ReadArray = new Int32[1];
            }
            else
            {
                arr = new byte[8];
                ReadArray = new Int32[Length];
            }


            arr[0] = 1;
            arr[1] = Convert.ToByte(StartRegistAddr / 256);

            if (Select1724 == 0)
            {
                arr[2] = 0xA9;
            }
            else
            {
                arr[2] = 0xBD;
            }
            arr[3] = Convert.ToByte(Opreat);
            arr[4] = deviceAddr;
            arr[5] = Convert.ToByte(StartRegistAddr % 256);
            arr[6] = Convert.ToByte(Length);
            arr[7] = Convert.ToByte(DelayCoef + Convert.ToByte(CFK) * 0X20);

            if (Opreat == ReadWrite.Write)
            {
                for (int i = 0; i < WriteData.Length; i++)
                {
                    arr[8 + i] = Convert.ToByte(WriteData[i]);
                }
            }

            IOPort.CH375WriteData(deviceIndex, arr);
            Thread.Sleep(30);

            if (Opreat == ReadWrite.Write)
            {
                ReadArray1 = IOPort.CH375ReadData(deviceIndex, 1);
            }
            else
            {
                ReadArray1 = IOPort.CH375ReadData(deviceIndex, Length);
            }
            ReadArray1.CopyTo(ReadArray, 0);
            CloseDevice(deviceIndex);
            return ReadArray;
        }  
        public bool WriteMDIO_Bert_25G(int deviceIndex, int deviceAddress, int phycialAddress, int regAddress, Int32 dataToWrite, byte Length)
        {
            try
            {
                OpenDevice(deviceIndex);
                CH375SetTimeout((byte)deviceIndex, 100, 100);

                byte[] arr = new byte[2 + 10];
                arr[0] = 0;
                arr[1] = 0;
                arr[2] = Convert.ToByte(0xA7);
                arr[3] = 1;//Write
                arr[4] = (byte)0;
                arr[5] = (byte)phycialAddress;
                arr[6] = (byte)deviceAddress;
                arr[7] = (byte)(regAddress / 256);
                arr[8] = (byte)(regAddress % 256);
                arr[9] = (byte)(Length);
                arr[10] = (byte)(dataToWrite / 256);
                arr[11] = (byte)(dataToWrite % 256);
                //buffer.CopyTo(arr, 10);
                bool b = CH375WriteData(deviceIndex, arr);


                CloseDevice(deviceIndex);
                //semaphore.Release();
                System.Threading.Thread.Sleep(20);
                return true;
            }
            catch (System.Exception ex)
            {
                CloseDevice(deviceIndex);
                return false;
            }

        }
        public Int32 ReadMDIO_Bert_25G(int deviceIndex, int deviceAddress, int phycialAddress, int regAddress, byte Length)
        {
            Int32 ReadData = 0;
            try
            {
                OpenDevice(deviceIndex);
                CH375SetTimeout((byte)deviceIndex, 100, 100);

                byte[] arr = new byte[10];
                arr[0] = 0;
                arr[1] = 0;
                arr[2] = Convert.ToByte(0xA7);
                arr[3] = 0;//Read
                arr[4] = (byte)0;
                arr[5] = (byte)phycialAddress;
                arr[6] = (byte)deviceAddress;
                arr[7] = (byte)(regAddress / 256);
                arr[8] = (byte)(regAddress % 256);
                arr[9] = (byte)(Length);
                //arr[10] = (byte)(dataToWrite / 256);
                //arr[11] = (byte)(dataToWrite % 256);
                //buffer.CopyTo(arr, 10);
                bool b = CH375WriteData(deviceIndex, arr);
                byte[] arrRead = CH375ReadData(deviceIndex, Length * 2);
                // CloseDevice(deviceIndex);
                ReadData = arrRead[0] * 256 + arrRead[1];
                CloseDevice(deviceIndex);

                System.Threading.Thread.Sleep(20);
                return ReadData;
            }
            catch (System.Exception ex)
            {
                CloseDevice(deviceIndex);
                return ReadData;
            }

        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceIndex">Usb 地址 </param>
        /// <param name="HlLeverl"> 0=Low_Leverl 1=H_Leverl</param>
        /// <param name="iotype">1=Clock Power EN ,0=Clock_Sen ,2=Bert_Power_EN,3=3V3_IPHYCDR_RST1,4=3V3_IPHYCDR_RST2,5=GT1724_RST1,6=GT1724_RST2</param>
        /// <returns>封装之后的类</returns>
        ///
        public void IoLevelControl(int deviceIndex, byte HlLeverl, byte iotype = 1)
        {
            OpenDevice(deviceIndex);
            CH375SetTimeout((byte)deviceIndex, 100, 100);
            byte[] BUff = new byte[8];
            BUff[0] = 0;
            BUff[1] = 0;
            BUff[2] = 0xBF;
            BUff[3] = Convert.ToByte(iotype + 2);
            BUff[4] = HlLeverl;
            BUff[5] = 0;
            BUff[6] = 0;
            BUff[7] = 0;
            IOPort.CH375WriteData(deviceIndex, BUff);
            CloseDevice(deviceIndex);
        }
		
        /// <summary>
        /// USBI2C Read
        /// </summary>
        /// <param name="deviceAddress"></param>
        /// <param name="registerAddress">寄存器地址</param>
        /// <param name="dataLength"></param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="deviceIndex"></param>
        /// <param name="Select1724">0=1724_1, 1=1724_2</param>
        /// <param name="regAddressWide">寄存器地址宽度</param>
        /// <param name="initUSB">默认为true</param>
        /// <param name="coeffMultiply10">默认为false</param>
        /// <param name="softI2CDelayCoeff">默认为0</param>
        /// <returns></returns>
        public byte[] USBI2CRead(byte deviceAddress, int registerAddress, byte dataLength, IOPort.CFKType FCK, byte deviceIndex, byte Select1724, byte regAddressWide, bool initUSB = true, bool coeffMultiply10 = false, byte softI2CDelayCoeff = 0)
        {
            if (initUSB)
            {
                OpenDevice(deviceIndex); //  执行了CH375ResetDevice()及CH375OpenDevice();
            }

            CH375SetTimeout(deviceIndex, 100, 100);
            byte[] buff = new byte[8];

            buff[0] = 1;
            buff[1] = (byte)(registerAddress / 256); // 寄存器地址高8位

            if (Select1724 == 0)
            {
                buff[2] = (byte)0xA9;
            }
            else // Select1724 == 1
            {
                buff[2] = (byte)0xBD;
            }

            buff[3] = 0; // 0=Read
            buff[4] = deviceAddress;
            buff[5] = (byte)(registerAddress % 256); // 寄存器地址低8位
            buff[6] = dataLength;
            buff[7] = (byte)(Convert.ToByte(FCK) * 0x20 + softI2CDelayCoeff);
            if (coeffMultiply10)
            {
                buff[7] = (byte)(buff[7] + 0x10);
            }

            CH375WriteData(deviceIndex, buff);
            System.Threading.Thread.Sleep(10);
            byte[] arrRead = CH375ReadData(deviceIndex, dataLength);

            IOPort.CloseDevice(deviceIndex);

            if (Select1724 == 0) // 1724 Select == BERT_I2C(0)
            {
                System.Threading.Thread.Sleep(5);
            }

            return arrRead;
        }
        /// <summary>
        /// USBI2C Write
        /// </summary>
        /// <param name="deviceAddress"></param>
        /// <param name="registerAddress">寄存器地址</param>
        /// <param name="dataLength"></param>
        /// <param name="FCK">0=400K, 1=200K, 2=100K, 3=50K, 4=25K</param>
        /// <param name="deviceIndex"></param>
        /// <param name="Select1724">0=1724_1, 1=1724_2</param>
        /// <param name="regAddressWide">寄存器地址宽度</param>
        /// <param name="dataToWrite"></param>
        /// <param name="initUSB">默认为true</param>
        /// <param name="coeffMultiply10">默认为false</param>
        /// <param name="softI2CDelayCoeff">默认为0</param>
        /// <returns>是否写入成功</returns>
        public bool USBI2CWrite(byte deviceAddress, int registerAddress, byte dataLength, IOPort.CFKType FCK, byte deviceIndex, byte Select1724, byte regAddressWide, byte[] dataToWrite, bool initUSB = true, bool coeffMultiply10 = false, byte softI2CDelayCoeff = 0)
        {
            bool flag = false; // 指示是否写入成功

            if (initUSB)
            {
                 OpenDevice(deviceIndex); //  执行了CH375ResetDevice()及CH375OpenDevice();
            }

            CH375SetTimeout(deviceIndex, 100, 100);
            byte[] buff = new byte[dataToWrite.Length + 8];

            buff[0] = 1;
            buff[1] = (byte)(registerAddress / 256); // 寄存器地址高8位

            if (Select1724 == 0)
            {
                buff[2] = (byte)0xA9;
            }
            else // Select1724 == 1
            {
                buff[2] = (byte)0xBD;
            }

            buff[3] = 1; // 1=Write
            buff[4] = deviceAddress;
            buff[5] = (byte)(registerAddress % 256); // 寄存器地址低8位
            buff[6] = dataLength;
            buff[7] = (byte)(Convert.ToByte(FCK) * 0x20 + softI2CDelayCoeff);
            if (coeffMultiply10)
            {
                buff[7] = (byte)(buff[7] + 0x10);
            }

            dataToWrite.CopyTo(buff, 8);

            CH375WriteData(deviceIndex, buff);
            System.Threading.Thread.Sleep(10);
            byte[] arrRead = CH375ReadData(deviceIndex, dataLength);

            CloseDevice(deviceIndex);

            if (Select1724 == 0) // 1724 Select == BERT_I2C(0)
            {
                System.Threading.Thread.Sleep(5);
            }

            if (arrRead[0] == 1)
            {
                flag = true;
            }

            return flag;
        }

         
#endregion
    


    }
    
}
