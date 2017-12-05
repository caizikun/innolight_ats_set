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
    public class SFPDUT : SFP28DUT
    {

        enum Writer_Store : byte
        {
            Writer = 0,
            Store = 1
        }
        private static object syncRoot = new Object();//used for thread synchronization

        //  public Algorithm Algorithm = new Algorithm();





        #region driver

        public byte[] WriteDriverSFP_old(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte Length, byte chipset, byte[] dataToWrite)
        {
            lock (syncRoot)
            {
                byte[] ReturnArray = new byte[1];

                try
                {
                    for (int i = 0; i < Length; i++)
                    {
                        byte[] WriteDacByteArray = Algorithm.ObjectTOByteArray(dataToWrite[i], DriverStruct[i].Length, DriverStruct[i].Endianness);

                        EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x02, chipset, dataToWrite);
                    }
                    ReturnArray[0] = 0xAA;

                    return ReturnArray;
                }
                catch
                {
                    return ReturnArray;
                }
            }
        }
        public byte[] WriteDriverSFP(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte Length, byte chipset, int dataToWrite, bool LittleEndian)
        {
            lock (syncRoot)
            {
                byte[] ReturnArray = new byte[1];

                try
                {


                    byte[] WriteDacByteArray;

                    if (chipset == 4)
                    {
                        WriteDacByteArray = Algorithm.ObjectTOByteArray(dataToWrite, 2, LittleEndian);
                        EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x02, chipset, WriteDacByteArray);
                    }
                    else
                    {
                        WriteDacByteArray = Algorithm.ObjectTOByteArray(dataToWrite, Length, LittleEndian);

                        for (int i = Length - 1; i > -1; i--)
                        {
                            byte[] WriteData = Algorithm.ObjectTOByteArray(WriteDacByteArray[i], 2, LittleEndian);
                            EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress + i, 0x02, chipset, WriteData);

                        }
                    }
                    ReturnArray[0] = 0xAA;

                    return ReturnArray;
                }
                catch
                {
                    return ReturnArray;
                }
            }
        }
        public int ReadDriverSFP(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte Length, byte chipset)// 有问题,但是需要确认,默认写长度为2符合FW接口协议 Leo 2016-10-25
        {

            lock (syncRoot)
            {
                int[] ReturnArray = new int[Length];
                int ReadData = 0;
                try
                {


                    if (chipset == 4)
                    {

                        byte[] ReadArray = EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x01, chipset, new byte[2]);

                        ReadData = ReadArray[0] * 256 + ReadArray[1];

                    }
                    else
                    {
                        for (int i = 0; i < Length; i++)
                        {

                            byte[] ReadArray = EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress + i, 0x01, chipset, new byte[2]);

                            ReturnArray[i] = ReadArray[0] * 256 + ReadArray[1];

                        }

                        for (int i = Length - 1; i > -1; i--)
                        {
                            ReadData += Convert.ToInt16(ReturnArray[i] * Math.Pow(256, Length - 1 - i));
                        }
                    }


                    return ReadData;
                }
                catch
                {
                    return ReadData;
                }
            }
          //  return EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x01, chipset, new byte[2]);
        }
        public byte[] StoreDriverSFP_old(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, int Length, byte chipset, byte[] dataToWrite)
        {

            lock (syncRoot)
            {
                byte[] ReturnArray = new byte[1];

                try
                {
                    for (int i = 0; i < Length; i++)
                    {
                        byte[] WriteDacByteArray = Algorithm.ObjectTOByteArray(dataToWrite[i], DriverStruct[i].Length, DriverStruct[i].Endianness);

                        EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x06, chipset, dataToWrite);
                    }
                    ReturnArray[0] = 0xAA;

                    return ReturnArray;
                }
                catch
                {
                    return ReturnArray;
                }
            }
          //  return EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x06, chipset, dataToWrite);
        }
        public byte[] StoreDriverSFP(int deviceIndex, int deviceAddress, int StartAddress, int regAddress, byte Length, byte chipset, int dataToWrite, bool LittleEndian)
        {
            lock (syncRoot)
            {
                byte[] ReturnArray = new byte[1];

                try
                {


                    byte[] WriteDacByteArray;

                    if (chipset == 4)
                    {
                        WriteDacByteArray = Algorithm.ObjectTOByteArray(dataToWrite, 2, LittleEndian);
                        EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress, 0x06, chipset, WriteDacByteArray);
                    }
                    else
                    {
                        WriteDacByteArray = Algorithm.ObjectTOByteArray(dataToWrite, Length, LittleEndian);

                        for (int i = Length - 1; i > -1; i--)
                        {
                            byte[] WriteData = Algorithm.ObjectTOByteArray(WriteDacByteArray[i], 2, LittleEndian);
                            EEPROM.ReadWriteDriverSFP(DUT.deviceIndex, deviceAddress, StartAddress, regAddress + i, 0x06, chipset, WriteData);

                        }
                    }
                    ReturnArray[0] = 0xAA;

                    return ReturnArray;
                }
                catch
                {
                    return ReturnArray;
                }
            }
        }

        //driver innitialize
        public override bool DriverInitialize()
        {//database 0: LDD 1: AMP 2: DAC 3: CDR

            ////chipset 1tx,2rx,4dac,3cdr
            lock (syncRoot)
            {
                int j = 0;
                int k = 0;
                byte engpage = 0;
                int startaddr = 0;
                byte chipset = 0x01;
                int temp;
                bool flag = true;
                if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                {
                    engpage = DutStruct[j].EngPage;
                    startaddr = DutStruct[j].StartAddress;

                }
                else
                {
                    Log.SaveLogToTxt("there is no DEBUGINTERFACE");
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


                            // byte[] WriteBiasDacByteArray = Algorithm.ObjectTOByteArray(DriverInitStruct[i].ItemValue, 2, DriverInitStruct[i].Endianness);

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
                                // WriteDriverSFP(DUT.deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress, chipset, WriteBiasDacByteArray);

                                StoreDriverSFP(DUT.deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].Length, chipset, Convert.ToInt16(DriverInitStruct[i].ItemValue), DriverInitStruct[i].Endianness);

                                temp = ReadDriverSFP(DUT.deviceIndex, 0xA2, startaddr, DriverInitStruct[i].RegisterAddress, DriverInitStruct[i].Length, chipset);


                                if (Convert.ToInt16(DriverInitStruct[i].ItemValue) == temp)
                                    break;
                            }
                            if (k >= 3)
                                flag = false;
                        }
                    }
                    return flag;
                }
            }
        }

        #region  Operate Drvier Regist

        private bool WriteDac(string StrItem, object DAC)
        {
            lock (syncRoot)
            {
                int ReadDacValue = 0;

                int i = 0;


                if (FindFiledNameChannelDAC(out i, StrItem))
                {

                    bool flag = Algorithm.BitNeedManage(DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);

                    if (!flag)//寄存器全位,不需要做任何处理
                    {
                        for (int k = 0; k < 3; k++)
                        {

                            if (!Write_Store_DriverRegist(StrItem, DAC, Writer_Store.Writer)) return false;//写DAC值

                            if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读DAC值

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

                            int JoinValue = Algorithm.WriteJointBitValue(Convert.ToInt32(DAC), Convert.ToInt32(TempReadValue), DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理
                            // int JoinValue = Algorithm.WriteJointBitValue((int)(DAC), Convert.ToInt32(TempReadValue), DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理

                            if (!Write_Store_DriverRegist(StrItem, JoinValue, Writer_Store.Writer)) return false;//写入寄存器的全位DAC值

                            if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读取寄存器的全位DAC值

                            int ReadJoinValue = Algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                            if (ReadJoinValue == Convert.ToInt16(DAC))
                            {
                                return true;
                            }
                        }

                    }
                    Log.SaveLogToTxt("Writer " + StrItem + " Error");
                    return false;
                }
                else
                {
                    Log.SaveLogToTxt("Writer " + StrItem + " Error");
                    return false;
                }
            }
        }
        private bool StoreDac(string StrItem, object DAC)
        {
            lock (syncRoot)
            {
                int ReadDacValue = 0;

                int i = 0;

                // string StrItem = "BiasDac";

                if (FindFiledNameChannelDAC(out i, StrItem))
                {

                    bool flag = Algorithm.BitNeedManage(DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);

                    if (!flag)//寄存器全位,不需要做任何处理
                    {
                        for (int k = 0; k < 3; k++)
                        {

                            if (!Write_Store_DriverRegist(StrItem, DAC, Writer_Store.Store)) return false;//存DAC值

                            if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读DAC值

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

                            int JoinValue = Algorithm.WriteJointBitValue(Convert.ToInt32(DAC), Convert.ToInt32(TempReadValue), DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//写入值处理

                            if (!Write_Store_DriverRegist(StrItem, JoinValue, Writer_Store.Store)) return false;//存入寄存器的全位DAC值

                            if (!ReadDAC(StrItem, out ReadDacValue)) return false;//读取寄存器的全位DAC值

                            int ReadJoinValue = Algorithm.ReadJointBitValue(ReadDacValue, DriverStruct[i].Length, DriverStruct[i].StartBit, DriverStruct[i].EndBit);//读取值处理

                            if (ReadJoinValue == Convert.ToInt16(DAC))
                            {
                                return true;
                            }
                        }

                    }
                    Log.SaveLogToTxt("Writer " + StrItem + " Error");
                    return false;
                }
                else
                {
                    Log.SaveLogToTxt("Writer " + StrItem + " Error");
                    return false;
                }
            }
        }
        private bool Write_Store_DriverRegist(string StrItem, object DAC, Writer_Store operate)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                bool ResultFlag = true;
                try
                {
                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;
                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
                    }
                    int i = 0;
                    if (FindFiledNameChannelDAC(out i, StrItem))//LDD=1;Amp=2;CDR=3;DAC=4
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

                        if (chipset == 4)
                        {
                            //byte Length = Convert.ToByte(DriverStruct[i].Length * 2);
                            //byte[] WriteDacByteArray = Algorithm.ObjectTOByteArray(DAC, Length, DriverStruct[i].Endianness);
                            byte[] WriteDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);
                        }
                        else
                        {
                            byte Length = Convert.ToByte(DriverStruct[i].Length * 2);
                            byte[] WriteDacByteArray = Algorithm.ObjectTOByteArray(DAC, DriverStruct[i].Length, DriverStruct[i].Endianness);
                        }
                        for (int J = 0; J < 3; J++)
                        {

                            if (operate == 0)//写
                            {

                                WriteDriverSFP(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].Length, chipset, Convert.ToInt16(DAC), DriverStruct[i].Endianness);
                            }
                            else//存
                            {
                                StoreDriverSFP(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].Length, chipset, Convert.ToInt16(DAC), DriverStruct[i].Endianness);


                            }

                            int ReadData = ReadDriverSFP(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, DriverStruct[i].Length, chipset);

                            if (Convert.ToInt16(DAC) == ReadData)
                            {
                                ResultFlag = true;
                                break;
                            }


                        }






                        if (ResultFlag)
                        {
                            Log.SaveLogToTxt("Writer " + StrItem + "= " + DAC);

                        }
                        else
                        {
                            Log.SaveLogToTxt("Writer " + StrItem + "= " + DAC);
                        }
                        return ResultFlag;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Writer " + StrItem + " Error");
                        return false;
                    }
                }
                catch
                {
                    return false;

                }
            }
        }
        private bool ReadDAC(string StrItem, out int ReadDAC)
        {
            lock (syncRoot)
            {
                byte chipset = 0x01;
                int j = 0;
                byte engpage = 0;
                int startaddr = 0;
                ReadDAC = 0;
                // int ReadDAC = 0;

                try
                {

                    if (Algorithm.FindFileName(DutStruct, "DEBUGINTERFACE", out j))
                    {
                        engpage = DutStruct[j].EngPage;
                        startaddr = DutStruct[j].StartAddress;

                    }
                    else
                    {
                        Log.SaveLogToTxt("there is no DEBUGINTERFACE");
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

                        ReadDAC = ReadDriverSFP(DUT.deviceIndex, 0xA2, startaddr, DriverStruct[i].RegisterAddress, DriverInitStruct[i].Length, chipset);



                        return true;
                    }
                    else
                    {
                        Log.SaveLogToTxt("Read " + StrItem + "Error");
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

        }

        public override bool WriteBiasDac(object DAC)
        {
            lock (syncRoot)
            {
                return WriteDac("BiasDAC", DAC);
            }
        }
        public override bool WriteModDac(object DAC)
        {
            lock (syncRoot)
            {
                return WriteDac("ModDAC", DAC);
            }
        }
        public override bool WriteMaskDac(object DAC)
        {
            lock (syncRoot)
            {
                return WriteDac("MaskDAC", DAC);

            }
        }
        public override bool StoreBiasDac(object biasdac)
        {
            lock (syncRoot)
            {
                return StoreDac("BiasDAC", biasdac);

            }
        }
        public override bool StoreModDac(object moddac)
        {
            lock (syncRoot)
            {
                return StoreDac("MODDAC", moddac);
            }
        }
        public override bool StoreMaskDac(object DAC)
        {
            lock (syncRoot)
            {
                return StoreDac("MaskDAC", DAC);
            }
        }

        public override bool StoreLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                return StoreDac("LOSDAC", losdac);
            }
        }
        public override bool StoreLOSDDac(object losddac)
        {
            lock (syncRoot)
            {
                return StoreDac("LOSDDAC", losddac);
            }
        }
        public override bool StoreAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                return StoreDac("APDDAC", apddac);
            }
        }
        public override bool StoreEA(object DAC)
        {
            lock (syncRoot)
            {
                return StoreDac("EADAC", DAC);
            }
        }

        public override bool WriteLOSDac(object losdac)
        {
            lock (syncRoot)
            {
                return WriteDac("LOSDAC", losdac);
            }
        }

        public override bool WriteLOSDDac(object losddac)
        {
            lock (syncRoot)
            {
                return WriteDac("LOSDDAC", losddac);
            }
        }
        public override bool WriteAPDDac(object apddac)
        {
            lock (syncRoot)
            {
                return WriteDac("APDDAC", apddac);

            }
        }
        public override bool StoreCrossDac(object crossdac)
        {
            lock (syncRoot)
            {
                return StoreDac("CROSSDAC", crossdac);

            }
        }
        public override bool WriteEA(object DAC)
        {
            lock (syncRoot)
            {
                return WriteDac("EADAC", DAC);
            }

        }
        #endregion

        //set biasmoddac





        //read biasmoddac




        #endregion




    }
}
