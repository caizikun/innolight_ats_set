﻿using System;
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
            public double TxLOPTarget;
            public double TxLOPUL;
            public double TxLOPLL;
            public double AdjustTxLOPUL;
            public double AdjustTxLOPLL;
          
            public UInt32 IbiasDACStart;
            public byte IbiasStep;
           
            public UInt32 IbiasDACMin;
            public UInt32 IbiasDACMax;
            public byte IbiasMethod;

            public UInt32 ModDacMin;
            public UInt32 ModDacMax;

          

            public UInt32 ImodDACStart;
            public double TxErTarget;
            public double TxErUL;
            public double TxErLL;
            public double AdjustErUL;
            public double AdjustErLL;
            public byte ImodStep;
            public ArrayList pidCoefArray;
            public ArrayList FixedModArray;
            public ArrayList FixedIBiasArray;
            public ArrayList FixedCrossDacArray;
            public ArrayList TargetCurrentArray;
            public UInt16 SleepTime;
            public double CrossingTarget;
            public double CrossingUL;
            public double CrossingLL;
            public byte DCCouple_AdjustMehtod;
            public double TecTemp;
        }
    public struct AdjustEYE_SFP28_Struce
    {
        public bool AutoTune;
        public double TxLOPTarget;
        public double TxLOPTolerance;
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
        public double Spc_TxPower_Max;
        public double Spc_TxPower_Min;
        public double Spc_Er_Max;
        public double Spc_Er_Min;
        public byte TxPowCoef_1st_2st_Pid;
        public byte ErCoef_1st_2st_Pid;
        public byte Loop_open_close_both;
        public byte AC_DC_Coupling;
        public UInt32 FixedCrossDac;
        public ArrayList pidCoefArray;
        public ArrayList FixedModArray;
        public ArrayList FixedIBiasArray;
        public UInt16 SleepTime;

    }
#endregion

   #region AdjustCrossing
    public struct AdjustCrossingStuct
    {

        public Int32 DACMax;
        public Int32 DACStart;
        public Int32 DACMin;
        public Int32 DacStep;
        public byte AdjustMethod;

        public byte SpecMax_PR;
        public byte SpecMin_PR;

    }
#endregion

   #region AdjustMask
    public struct AdjustMaskStuct
    {

        public Int32 DACMax;
        public Int32 DACStart;
        public Int32 DACMin;
        public Int32 DacStep;
        public byte AdjustMethod;
        public int MaskLimit;
        public byte IC_Type;


    }
    #endregion

   #region AdjustTxPowerDmi
    public struct AdjustTxPowerDmiStruct
        {        
          
          public ArrayList ArrayIbias;
          public ArrayList ArrayFixedModDac;          
          public bool isTracingErr;          
          public double HighestCalTemp;
          public double LowestCalTemp;
          public bool ISNewAlgorithm;
          public UInt16 SleepTime;
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

   #region TestTxPowerDmi
    public struct TestTxPowerDmiStruct
        {
            
        }
 #endregion

   #region AlarmWarning
    public struct TestAlarmWarningStruct
    {
        public double RxPowerAWPoint;
    }
    #endregion

   #region TestEleEye
    public struct TestEleEyeStruct
    {
        public double CensePoint;
    }
    #endregion


   #region AdjustAPD

    public struct AdjustAPDStruct
    {
          public double RxInputPower;
          public Int32[] SetPoints;
          public byte ScanCount;
          public byte Formula_X_Type ;// 0=TempAdc;1=ApdTempAdc
          public byte AdjustMethod;    
    }

#endregion

   #region AdjustLos
    public struct AdjustLosStruct
        {
           
            public double LosAVoltageStartValue;
            public double LosAVoltageUperLimit;
            public double LosAVoltageLowLimit;
            public byte LosAVoltageTuneStep;
            public double LosASetPower;

            public double LosDSetPower;
            public double LosDVoltageStartValue;
            public double LosDVoltageUperLimit;
            public double LosDVoltageLowLimit;
            public byte LosDVoltageTuneStep;            
            public bool isAdjustLosA;
            public bool isAdjustLosD;

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
          public byte ExtraOffset;
          public byte ReadRxADCCount;
          public UInt16 SleepTime;
          public double minRxPowerInut;
          public ArrayList ArrayListVcc;        
          public ArrayList RelatedChannels;         
    }
#endregion

   #region CalVccDmi
    public struct CalVccDmiStruct
        {
           public ArrayList ArrayListVcc;
        }
#endregion

   #region CalWavelength
    public struct CalWavelengthStruct
    {
        public Int32 WavelengthDacMax;
        public Int32 WavelengthDacMin;
        public Int32 WavelengthDacStep;
        public Int32 WavelengthDacStart;

        public double WavelengthTargetMax;
        public double WavelengthTargetMin;
        public double WavelengthTypcalValue;
      
    }
    #endregion
   #region TestBer
    public struct TestBerStruct
        {
            public double CsenAlignRxPwr;
            public double CsenStartingRxPwr;
            public double SearchTargetBerUL;
            public double SearchTargetBerLL;
            public double SearchTargetBerStep;
            public double SearchTargetBerRxpowerUL;
            public double SearchTargetBerRxpowerLL;            
            public double CoefCsenSubStep;
            public double CoefCsenAddStep;
            public bool IsBerQuickTest;
            public bool IsOpticalSourceUnitOMA;
            public byte SearchTargetBerMethod;
            public double Ber_ERP;
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

    #region TestRxDmiPowErrorCurve
    public struct TestRxDmiPowErrorCurveStruct
    {
        public double RxInputPowerMax;
        public double RxInputPowerMin;
        public double AttStep;
    }
    #endregion

    #region TestRxOverload
    public struct TestRxOverloadStruct
    {
        public double LoopTime;
        public double AttStep;
        public double GatingTime;
        //public bool IsOptSourceUnitOMA;
        //public double SpecDelta;
        //public double OverloadSpecMax;
        //public double OverloadSpecMin;
        public double CsenAlignRxPwr;
    }
    #endregion

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
         }
    public struct AdjustRxPowerDmitValueRecordsStruct
    {
        public DataTable DataTableRxRawAdc;
        public DataTable DataTableVccADC;       
    }

    #region TestTxReturnLostTolerance
    public struct TestTxReturnLostToleranceStruct
    {
        public double TargetPower;
        public byte ReturnLosTolerancePRBS;

        public double RXAttStep;
        public double TXAttStep;
        public double CsenAlignRxPwr;
        public double StartRxPwr;
        public double LoopTime;
        public double GatingTime;
    }
    #endregion
}
