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
        public double CurrentTemp;//当前的实际温度（C）       
        public byte CurrentChannel; //当前通道，添加到相应的数组下标       
        public byte TotalTempCount;//总温度数
        public byte TotalVccCount;//总电压数
        public byte TotalChCount;//总通道数
        public String CurrentSN;
        public byte ApcStyle;
        public String PN;
        public string StrPathOEyeDiagram;//眼图存放路径
        public string StrPathEEyeDiagram;//眼图存放路径
        public string OpticalSourseERArray;//OpticalSourceER
    }
    #endregion
    #region DutStruct
    public struct DutStruct
    {
        public string FiledName;
        public byte Channel;
        public bool CoefFlag;
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
    #region Driver_Initialize_Struct
    public struct DriverInitializeStruct
    {
        public byte ChipLine;
        public int RegisterAddress;
        public byte DriverType;
        public byte Length;
        public object ItemValue;
        public bool Endianness;
    }
    #endregion 
    #region DutCoefValueStuct

    public struct DutCoefValueStuct
    {

        public byte Page;
        public int StartAddress;
        public byte Length;
        public string CoefValue;
    }
    #endregion
    #region  DutEEPROMInitializeStuct
     public struct DutEEPROMInitializeStuct
     {
       public  int SlaveAddress;
       public byte Page;
       public int StartAddress;
       public byte Length;
       public object ItemValue;
     }
    #endregion
}
