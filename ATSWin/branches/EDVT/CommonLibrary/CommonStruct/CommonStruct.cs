using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
namespace ATS_Framework
{

    public class EquipmentList : SortedList<string, EquipmentBase>
    {
    }
    #region  TestModeEquipmentParameters
    public struct TestModeEquipmentParameters
    {
        public string FiledName;
        public string DefaultValue;
    }
    #endregion
    #region  GlobalParameters
    public struct globalParameters
    {
        public double CurrentVcc;// 当前的电压值
        public double VccOffset;//电压的偏移量
        public double Iccoffset;//电流的偏移量
        public double CurrentTemp;//当前的实际温度（C）       
        public byte CurrentChannel; //当前通道，添加到相应的数组下标       
        public byte TotalTempCount;//总温度数
        public byte TotalVccCount;//总电压数
    }
    #endregion
#region DutStruct
    public struct DutStruct
    {
        public string FiledName;
        public byte Channel;
        //public int SlaveAdress;
        public byte EngPage;
        public int StartAddress;
        public byte Length;
        public byte Format;
        //public byte TempSelect;
        //public byte VccSelect;
        //public byte DebugStartAddress;//DRIVER DEBUG

    }
#endregion
#region DriverStruct
    public struct DriverStruct
    {
        public string FiledName;
        public byte MoudleLine;
        public byte ChipLine;
        public int RegisterAddress;
        public byte DriverType;
        public byte Length;
        public bool Endianness;
    }
#endregion
   
}
