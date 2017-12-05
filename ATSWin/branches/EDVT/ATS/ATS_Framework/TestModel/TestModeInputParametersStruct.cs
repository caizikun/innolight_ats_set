using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
namespace ATS_Framework
{

#region AdjustEYE
    public struct AdjustEYEStruce
        {
            public bool AutoTune;
            public double TxLOPTarget;
            public double TxLOPTolerance;
            public UInt32 FixedMod;
            public UInt32 IbiasMax;
            public UInt32 IbiasMin;
            public UInt32 IbiasStart;
            public byte IbiasStep;
            public byte IbiasMethod;
            public UInt32 ImodMax;
            public UInt32 ImodMin;
            public double TxErTarget;
            public double TxErTolerance;
            public UInt32 ImodStart;
            public byte ImodMethod;
            public byte ImodStep;
            public double TxLOPULSPEC;
            public double TxLOPLLSPEC;
            public double ErULSPEC;
            public double ErLLSPEC;
            public byte isTxPower1Stor2StorPid;
            public byte isEr1Stor2StorPid;
            public byte isOpenLooporCloseLooporBoth;
            public bool isDCtoDC;
            public UInt32 FIXEDCrossDac;
            public ArrayList pidCoefArray;
        }
#endregion
#region AdjustTxPowerDmi
    public struct AdjustTxPowerDmiStruct
        {
        
          public bool AutoTune;
          public ArrayList ArrayIbias;
          public UInt32 FixedModDac;
          public byte IBiasADCorTxPowerADC;
          public bool isTempRelative;
          public byte is1Stor2StorPid;
          public bool isDCtoDC;
          public UInt32 FIXEDCrossDac;
        }
#endregion
//#region CalTxPower
//        struct CalTxPowerStruct
//        {
//          public ArrayList ArrayListLOPTargetADC;
//          public ArrayList ArrayListBiasTarget;
//          public ArrayList ArrayListTempADC;
//          public byte is1Stor2StorPid;
//          public byte isOpenLooporCloseLooporBoth;          
//        }
//#endregion
//#region CalTxDmi
//        struct CalTxDmiStruct
//        {
           
//           public DataTable DataTableTxLop;
//           public DataTable DataTableTxAdc;
//           public ArrayList ArrayListTempADC;             
//           public bool TempRelative;
//           public byte is1Stor2StorPid;
          
//        }
//#endregion
//#region CalErValue
//        struct CalErValueStruct
//        {
//          public ArrayList  ArrayListImodTarget;
//          public ArrayList ArrayListTempADC;
//          public byte is1Stor2StorPid;

//        }
//#endregion
#region TestEye
    public struct TestEyeStruct
        {
        public bool isOpticalEyeORElecEye;
        }
#endregion
#region TestTxPowerDmi
    public struct TestTxPowerDmiStruct
        {
            
        }
 #endregion






#region AdjustAPD
    public struct AdjustAPDStruct
{
          public bool  AutoTune;
          public double ApdCalPoint;
          public ArrayList  ArrayListApdBiasPoints;
          public byte ApdBiasStep;
          public byte is1Stor2StorPid;
}
#endregion
#region AdjustLos
    public struct AdjustLosStruct
        {
            public bool AutoTune;
            public double LosAInputPower;
            public UInt32 LosAVoltageStartValue;
            public UInt32 LosAVoltageUperLimit;
            public UInt32 LosAVoltageLowLimit;
            public byte LosAVoltageTuneStep;
            public byte LosToleranceStep;
            public double LosDInputPower;
            public UInt32 LosDVoltageStartValue;
            public UInt32 LosDVoltageUperLimit;
            public UInt32 LosDVoltageLowLimit;
            public byte LosDVoltageTuneStep;            
            public bool islosalosdcombin;

        }
#endregion

//#region CalApd
//        struct CalApdStruct
//        {
//          public ArrayList  ArrayListApdBiasTarget;
//          public ArrayList ArrayListTempADC;
//          public byte is1Stor2StorPid;
//        }
//#endregion

#region CalRxDmi
    public struct CalRxDmiStruct
    {
          public ArrayList ArrayListRxPower;
          public byte is1Stor2StorPid;

    }
#endregion
#region CalTempDmi
    public struct CalTempDmiStruct
        {            
            public byte is1Stor2StorPid;
        }
#endregion
#region CalVccDmi
    public struct CalVccDmiStruct
        {
           public ArrayList ArrayListVcc;
           public byte is1Stor2StorPid;          
           public double generalVcc;
        }
#endregion
#region TestBer
    public struct TestBerStruct
        {
            public double CsenAlignRxPwr;
            public double CsenStartingRxPwr;
            public double SearchTargetBerUL;
            public double SearchTargetBerLL;
            public double SearchTargetBerAddStep;
            public double SearchTargetBerSubStep;
            public double CsenTargetBER;
            public double CoefCsenSubStep;
            public double CoefCsenAddStep;
            public bool IsBerQuickTest;
        }
#endregion
#region TestIBiasDmi
    public struct TestIBiasDmiStruct
        {

        }
#endregion
//#region TestIcc
//    public struct TestIccStruct
//        {
//            public UInt16 iccoffset;
//        }
//#endregion
#region TestRXLosAD
    public struct TestRXLosADStruct
        {
            
            public double LosAMax;
            public double LosAMin;
            public double LosDMax;
            public double LosADStep;
            public bool isLosDetail;
        }
#endregion
#region TestRxPowerDmi
    public struct TestRxPowerDmiStruct
        {
            public ArrayList ArrayListRxInputPower;

        }
#endregion
//#region TestTempDmi
//    public struct TestTempDmiStruct
//        {
//            public double CurrentTemp;
//        }
//#endregion
//#region TestVccDmi
//    public struct TestVccDmiStruct
//        {
//            public double CurrentVcc;
//            public double VccOffset;
//        }
//#endregion


    // all channel records
    public struct AdjustEyeTargetValueRecordsStruct
        {          
            public ArrayList ibiasDacArray;
            public ArrayList imodulaDacArray;
            public ArrayList targetTxPowerADCArray;
            public ArrayList targetTxPowerUWArray;
        }
    public struct AdjustTxPowerDmitValueRecordsStruct
         {
             public DataTable DataTableTxLop;
             public DataTable DataTableTxPowerAdc;
             public DataTable DataTableIBiasAdc;
         }
    
}
