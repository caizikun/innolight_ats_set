using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivi.Visa.Interop;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ATS_Framework;

namespace Maintain
{
    
    public class IOPort
    {
        //protected Semaphore semaphore;
        public bool Connect_flag = false;// have used
        public Ivi.Visa.Interop.ResourceManager rm;   // VIsa  GPIB
        public Ivi.Visa.Interop.FormattedIO488 myDmm;// VIsa  GPIB
        protected string Str_IO_interface;
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

        static bool CH375WriteData(int iIndex, byte[] iBuffer) // 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度，最大长度为64个字节。
        {
            int l = iBuffer.Length;
            return CH375WriteData((byte)iIndex, iBuffer, ref l);
        }
        static int CH375ReadData(int iIndex, byte[] iBuffer) // 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度，最大长度为64个字节。
        {
            int l = iBuffer.Length;
            CH375ReadData((byte)iIndex, iBuffer, ref l);
            return l;
        }

        static byte[] CH375ReadData(int iIndex, int len) // 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度，最大长度为64个字节。
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
        public static bool resetdevice(int deviceSelect)
        {
           int x=  CH375ResetDevice((byte)deviceSelect);
            return x!=0;
        }
        public static bool OpenDevice(int deviceSelect)
        {
            int x = CH375ResetDevice((byte)deviceSelect);
            int r = CH375OpenDevice((byte)deviceSelect);
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
        public IOPort(string IO_Type, string Addr)
        {
            try
            {
                //semaphore = new Semaphore(0, 1);
                Str_IO_interface = IO_Type;
                switch (IO_Type)
                {

                    case "GPIB":
                        
                            rm = new Ivi.Visa.Interop.ResourceManager(); //Open up a new resource manager
                            myDmm = new Ivi.Visa.Interop.FormattedIO488(); //Open a new Formatted IO 488 session 
                            myDmm.IO = (IMessage)rm.Open(Addr, AccessMode.NO_LOCK, 5000, ""); //Open up a handle to the DMM with a 2 second timeout
                            myDmm.IO.Timeout = 5000; //You can also set your timeout by doing this command, sets to 3 seconds
                            myDmm.IO.Clear(); //Send a device clear first to stop any measurements in process    
                            //myDmm.WriteString("*RST", true);
                        
                        break;
                    case "USB":
                        OpenDevice(Convert.ToByte(Addr));
                        break;
                    case "RJ45":// 网口
                        break;
                    case "RS232":// 网口
                        break;
                    default:
                        break;
                }
                Connect_flag = true;
            }
            catch(Exception me)
            {
                //MessageBox.Show(me.Message);
                log.AdapterLogString(3, me.Message);
                Connect_flag = false;
                throw me;
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
        logManager log=new logManager();
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

               // MessageBox.Show(error.Message);
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

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                log.AdapterLogString(3, ex.Message);
                return Str_Read;
            }

            return Str_Read;

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
                //MessageBox.Show(error.Message);
                log.AdapterLogString(3, error.Message);
                throw error;
            }
        }

        
    }
    
}
