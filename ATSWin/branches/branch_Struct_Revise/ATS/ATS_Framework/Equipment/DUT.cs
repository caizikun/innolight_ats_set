using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ivi.Visa.Interop;
using System.Reflection;
namespace ATS_Framework
{
    public class DUT:EquipmentBase
    {
        public DUT()
        {
         //Semaphore  semaphore = new Semaphore(0, 1);
        }
       
        //dmi
        protected float EvbVolTageCoef_b=0, EvbVolTageCoef_c=0;
        public int phycialAdress;
        public bool ChipsetControll;
        public byte MoudleChannel;
        public static int deviceIndex;
        public DutStruct[] DutStruct;
        public DriverStruct[] DriverStruct;
        public DriverInitializeStruct[] DriverInitStruct;
        public DutEEPROMInitializeStuct[] EEpromInitStruct;
        public double DutDatarate=0;
        public byte DutPRBS=0;
        public bool IsCDROn = true;
        public bool FindFiledNameChannel(out int i, string filedname)
        {
            i = 0;
            while ((DutStruct[i].FiledName.ToUpper() != filedname.ToUpper()) || (DutStruct[i].Channel != MoudleChannel))
            {
                i++;
                if (i== DutStruct.Length)
                    break;
            }
            if (i == DutStruct.Length)
                return false;
            else
                return true;

        }
        public bool FindFiledNameChannelDAC(out int i, string filedname)
        {
            i = 0;
          
            do
            {
                if (DriverStruct[i].FiledName.ToUpper() == filedname.ToUpper() && DriverStruct[i].MoudleLine == MoudleChannel)
                {
                    break;
                }
                  i++;
            } while (i < DriverStruct.Length);

            if (i >= DriverStruct.Length)
                return false;
            else
                
                return true;

        }
        public bool FindFiledNameDACLength(out int RegistLength, string filedname)
        {
            RegistLength = 1;
            int i = 0;
            if (DriverStruct.Length==0)
            {
                return true;
            }
            do
            {
                if (DriverStruct[i].FiledName == filedname)
                {
                    break;
                }
                i++;
            } while (i < DriverStruct.Length);

            if (i >= DriverStruct.Length)
                return false;
            else
                RegistLength = DriverStruct[i].Length;
            return true;

        }
        public virtual double ReadDmiTemp() { return 0.0; }
        public virtual double ReadDmiVcc() { return 0.0; }
        public virtual double ReadDmiBias() { return 0.0; }
        public virtual double ReadDmiTxp() { return 0.0; }
        public virtual double ReadDmiRxp() { return 0.0; }
        //a/w
        public virtual double ReadTempHA() { return 0.0; }
        public virtual double ReadTempLA() { return 0.0; }
        public virtual double ReadTempLW() { return 0.0; }
        public virtual double ReadTempHW() { return 0.0; }
        public virtual double ReadVccLA() { return 0.0; }
        public virtual double ReadVccHA() { return 0.0; }
        public virtual double ReadVccLW() { return 0.0; }
        public virtual double ReadVccHW() { return 0.0; }
        public virtual double ReadBiasLA() { return 0.0; }
        public virtual double ReadBiasHA() { return 0.0; }
        public virtual double ReadBiasLW() { return 0.0; }
        public virtual double ReadBiasHW() { return 0.0; }
        public virtual double ReadTxpLA() { return 0.0; }
        public virtual double ReadTxpLW() { return 0.0; }
        public virtual double ReadTxpHA() { return 0.0; }
        public virtual double ReadTxpHW() { return 0.0; }
        public virtual double ReadRxpLA() { return 0.0; }
        public virtual double ReadRxpLW() { return 0.0; }
        public virtual double ReadRxpHA() { return 0.0; }
        public virtual double ReadRxpHW() { return 0.0; }
        //check a/w
        public virtual bool ChkTempHA() { return false; }
        public virtual bool ChkTempLA() { return false; }
        public virtual bool ChkVccHA() { return false; }
        public virtual bool ChkVccLA() { return false; }
        public virtual bool ChkBiasHA() { return false; }
        public virtual bool ChkBiasLA() { return false; }
        public virtual bool ChkTxpHA() { return false; }
        public virtual bool ChkTxpLA() { return false; }
        public virtual bool ChkRxpHA() { return false; }
        public virtual bool ChkRxpLA() { return false; }
        public virtual bool ChkTempHW() { return false; }
        public virtual bool ChkTempLW() { return false; }
        public virtual bool ChkVccHW() { return false; }
        public virtual bool ChkVccLW() { return false; }
        public virtual bool ChkBiasHW() { return false; }
        public virtual bool ChkBiasLW() { return false; }
        public virtual bool ChkTxpHW() { return false; }
        public virtual bool ChkTxpLW() { return false; }
        public virtual bool ChkRxpHW() { return false; }
        public virtual bool ChkRxpLW() { return false; }
        
        //read optional status /control bit
        public virtual bool ChkTxDis() { return false; }
        public virtual bool ChkTxFault() { return false; }
        public virtual bool ChkRxLos() { return false; }
        //set a/w
        public virtual void SetTempLA(decimal templa) { }
        public virtual void SetTempHA(decimal tempha) { }
        public virtual void SetTempLW(decimal templw) { }
        public virtual void SetTempHW(decimal temphw) { }
        public virtual void SetVccHW(decimal vcchw) { }
        public virtual void SetVccLW(decimal vcclw) { }
        public virtual void SetVccLA(decimal vccla) { }
        public virtual void SetVccHA(decimal vccha) { }
        public virtual void SetBiasLA(decimal biasla) { }
        public virtual void SetBiasHA(decimal biasha) { }
        public virtual void SetBiasHW(decimal biashw) { }
        public virtual void SetBiasLW(decimal biaslw) { }
        public virtual void SetTxpLW(decimal txplw) { }
        public virtual void SetTxpHW(decimal txphw) { }
        public virtual void SetTxpLA(decimal txpla) { }
        public virtual void SetTxpHA(decimal txpha) { }
        public virtual void SetRxpHA(decimal rxpha) { }
        public virtual void SetRxpLA(decimal rxpla) { }
        public virtual void SetRxpHW(decimal rxphw) { }
        public virtual void SetRxpLW(decimal rxplw) { }
        public virtual bool SetSoftTxDis() { return true; }
        public virtual bool SetSingleChannelTxEnable(byte Channel) {return false;}
        //w/r  sn/pn
        public virtual string ReadSn() { return ""; }
        public virtual string ReadPn() { return ""; }
        public virtual void SetSn(string sn) { }
        public virtual void SetPn(string pn) { }

        //read manufacture data
        public virtual string ReadFWRev() { return ""; }
        public virtual bool ReadTempADC(out UInt16 tempadc) { tempadc = 0; return false; }
        public virtual bool ReadVccADC(out UInt16 vccadc) { vccadc = 0; return false; }
        public virtual bool ReadBiasADC(out UInt16 biasadc) { biasadc = 0; return false; }
        public virtual bool ReadTxpADC(out UInt16 txpadc) { txpadc = 0; return false; }
        public virtual bool ReadRxpADC(out UInt16 rxpadc) { rxpadc = 0; return false; }
        public virtual bool ReadTECADC(out UInt16 Tecadc) { Tecadc = 0; return false; }
        public virtual bool ReadVccADC(out UInt16 vccadc, byte vccselect) { vccadc = 0; return false; }
        public virtual bool ReadTempADC(out UInt16 tempadc, byte TempSelect) { tempadc = 0; return false; }

       
        public virtual void Engmod(byte engpage) {  }
        public virtual bool WritePort(int id, int deviceIndex, int Port, int DDR) { return false; }
        public virtual byte[] ReadPort(int id, int deviceIndex, int Port, int DDR) { return new byte[16]; }
        public virtual byte[] ReadReg(int deviceIndex, int deviceAddress, int regAddress, int readLength) { return new byte[16]; }
        public virtual byte[] WriteReg(int deviceIndex, int deviceAddress, int regAddress, byte[] dataToWrite) { return new byte[16]; }

        public virtual bool DriverInitialize() { return false; }

       #region  DAC

        public virtual bool WriteBiasDac(object DAC) { return false; }
        public virtual bool WriteModDac(object DAC) { return false; }
        public virtual bool WriteLOSDac(object DAC) { return false; }
        public virtual bool WriteAPDDac(object DAC) { return false; }
        public virtual bool StoreBiasDac(object DAC) { return false; }
        public virtual bool StoreModDac(object DAC) { return false; }
        public virtual bool StoreLOSDac(object DAC) { return false; }
        public virtual bool StoreAPDDac(object DAC) { return false; }


   
        public virtual int ReadBiasDac() { return 0; }
        public virtual int ReadModDac() { return 0; }
        //public virtual bool ReadModDac(int length, out byte[] ModDac) { ModDac = new byte[16]; return false; }

        public virtual int ReadLOSDac() {  return 0; }
        public virtual int ReadAPDDac() { return 0; }

        //add losd
        public virtual bool WriteLOSDDac(object DAC) { return false; }
        public virtual bool StoreLOSDDac(object DAC) { return false; }
        //public virtual bool ReadLOSDDac(int length, out byte[] LOSDDac) { LOSDDac = new byte[16]; return false; }

        /// <summary>
        /// 填写TxADC 校验矩阵,一行一行的写,必须要在Ch1填写
        /// </summary>
        /// <param name="Matrix">填写TxADC 校验矩阵</param>
        /// <returns>填写结果</returns>
        ///
        public virtual bool WriterTxAdcCalibrateMatrix(double[,] Matrix)
        {
            return false;
        }
        public virtual bool WriterTxAdcBacklightOffset(object Offset)
        {
            return false;
        }

       #region  Mask
         
        public virtual bool WriteMaskDac(object DAC) { return false; }
       // public virtual bool ReadMaskDac( out ushort Dac) { Dac =0; return false; }
        public virtual bool StoreMaskDac(object DAC) { return false; }

       // public virtual bool WriteMaskCoefA(float DAC) { return false; }
        

       #endregion
        //set coef

#endregion


       #region  Coef 

      #region  TempDmi

        public virtual bool SetTempcoefb(string tempcoefb) { return false; }
        public virtual bool SetTempcoefb(string tempcoefb, byte TempSelect) { return false; }
        public virtual bool SetTempcoefc(string tempcoefc) { return false; }
        public virtual bool SetTempcoefc(string tempcoefc, byte TempSelect) { return false; }
#endregion

      #region  Wavelength
        public virtual bool WriteWavelengthDac(object Wavelengthdac) { return false; }
        public virtual bool ReadWavelengthDac(object crossdac) { return false; }
#endregion

      #region  Vcc
        public virtual bool SetVcccoefb(string vcccoefb) { return false; }
        public virtual bool SetVcccoefb(string vcccoefb, byte vccselect) { return false; }
        public virtual bool SetVcccoefc(string vcccoefc) { return false; }
        public virtual bool SetVcccoefc(string vcccoefc, byte vccselect) { return false; }


#endregion
       
      #region  RxDmi

        public virtual bool SetRxpcoefa(string rxcoefa) { return false; }
        public virtual bool SetRxpcoefb(string rxcoefb) { return false; }
        public virtual bool SetRxpcoefc(string rxcoefc) { return false; }
#endregion

      #region  TxPowerDmi

        public virtual bool SetTxSlopcoefa(string txslopcoefa) { return false; }
        public virtual bool SetTxSlopcoefb(string txslopcoefb) { return false; }
        public virtual bool SetTxSlopcoefc(string txslopcoefc) { return false; }
        public virtual bool SetTxOffsetcoefa(string txoffsetcoefa) { return false; }
        public virtual bool SetTxOffsetcoefb(string txoffsetcoefb) { return false; }
        public virtual bool SetTxOffsetcoefc(string txoffsetcoefc) { return false; }

       public virtual bool TxAllChannelEnable() { return true; }
        // private bool TxAllChannelEnable()
#endregion

      #region  AdjustEye

        public virtual bool SetBiasdaccoefa(string biasdaccoefa) { return false; }
        public virtual bool SetBiasdaccoefb(string biasdaccoefb) { return false; }
        public virtual bool SetBiasdaccoefc(string biasdaccoefc) { return false; }
        public virtual bool SetModdaccoefa(string moddaccoefa) { return false; }
        public virtual bool SetModdaccoefb(string moddaccoefb) { return false; }
        public virtual bool SetModdaccoefc(string moddaccoefc) { return false; }

        public virtual bool SetMaskcoefa(float coefa) { return false; }
        public virtual bool SetMaskcoefb(float coefb) { return false; }
        public virtual bool SetMaskcoefc(float coefc) { return false; }




#endregion
        
#endregion


        public virtual bool SetTargetLopcoefa(string targetlopcoefa) { return false; }
        public virtual bool SetTargetLopcoefb(string targetlopcoefb) { return false; }
        public virtual bool SetTargetLopcoefc(string targetlopcoefc) { return false; }
        public virtual bool SetAPDdaccoefa(string apddaccoefa) { return false; }
        public virtual bool SetAPDdaccoefb(string apddaccoefb) { return false; }
        public virtual bool SetAPDdaccoefc(string apddaccoefc) { return false; }

        

       
        public virtual bool ReadTempcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadVcccoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTempcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTempcoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadVcccoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadVcccoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTempcoefb(out string strcoef, byte TempSelect) { strcoef = ""; return false; }
        public virtual bool ReadTempcoefc(out string strcoef, byte TempSelect) { strcoef = ""; return false; }
        public virtual bool ReadVcccoefb(out string strcoef, byte vccselect) { strcoef = ""; return false; }
        public virtual bool ReadVcccoefc(out string strcoef, byte vccselect) { strcoef = ""; return false; }

        public virtual bool ReadRxpcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadRxpcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadRxpcoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxSlopcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxSlopcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxSlopcoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxOffsetcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxOffsetcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxOffsetcoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadBiasdaccoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadBiasdaccoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadBiasdaccoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadModdaccoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadModdaccoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadModdaccoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTargetLopcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTargetLopcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTargetLopcoefc(out string strcoef) { strcoef = ""; return false; }

       

        //NEW ADD
        //close loop
        public virtual bool SetCloseLoopcoefa(string CloseLoopcoefa) { return false; }
        public virtual bool SetCloseLoopcoefb(string CloseLoopcoefb) { return false; }
        public virtual bool SetCloseLoopcoefc(string CloseLoopcoefc) { return false; }
        //pid
        public virtual bool SetcoefP(string p) { return false; }
        public virtual bool SetcoefI(string i) { return false; }
        public virtual bool SetcoefD(string d) { return false; }
        public virtual bool SetPIDSetpoint(string setpoint) { return false; }
        //bias adc threshold
        public virtual bool SetBiasadcThreshold(byte threshold) { return false; }
        public virtual bool SetRXPadcThreshold(UInt16 threshold) { return false; }
       //reference temperature
        public virtual bool SetReferenceTemp(string referencetempcoef) { return false; }
        //txp proportion 
        public virtual bool SetTxpProportionLessCoef(string Lesscoef) { return false; }
        public virtual bool SetTxpProportionGreatCoef(string Greatcoef) { return false; }

        public virtual float GetTxpProportionLessCoef() { return 9999; }
        public virtual float GetTxpProportionGreatCoef() { return 9999; }
        //txp fits coef
        public virtual bool SetTxpFitsCoefa(string txpfitscoefa) { return false; }
        public virtual bool SetTxpFitsCoefb(string txpfitscoefb) { return false; }
        public virtual bool SetTxpFitsCoefc(string txpfitscoefc) { return false; }

        //apc
        public virtual bool APCON(byte apcswitch) { return false; }
        public virtual bool APCOFF(byte apcswitch) { return false; }
        public virtual bool APCStatus(out byte apcflag) { apcflag = 0x00; return false; }
        //new add as cgr4 new map
        public virtual bool SetRxAdCorSlopcoefb(string rxadcorslopcoefb) { return false; }
        public virtual bool SetRxAdCorSlopcoefc(string rxadcorslopcoefc) { return false; }
        public virtual bool SetRxAdCorOffscoefb(string rxadcoroffsetcoefb) { return false; }
        public virtual bool SetRxAdCorOffscoefc(string rxadcoroffsetcoefc) { return false; }
        public virtual bool ReadRx2RawADC(out UInt16 rxrawadc) { rxrawadc = 0; return false; }

        //read all coef
        public virtual bool ReadALLcoef(out DutCoefValueStuct[] DutList) { DutList = new DutCoefValueStuct[16]; return false; }
//read all eeprom
        public virtual bool ReadALLEEprom(out DutEEPROMInitializeStuct[] DutList) { DutList = new DutEEPROMInitializeStuct[16]; return false; }

        //new add for cfp
        public virtual bool SetTempcoefa(string tempcoefa) { return false; }
        public virtual bool SetVcccoefa(string vcccoefa) { return false; }

        public virtual UInt16[] ReadMDIO(int deviceIndex, int deviceAddress, int regAddress, int readLength) { return new UInt16[16]; }
        public virtual byte[] WriteMDIO(int deviceIndex, int deviceAddress, int regAddress, UInt16[] dataToWrite) { return new byte[16]; }

        public virtual bool ReadLaTempADC(out UInt16 tempadc) { tempadc = 0; return false; }
        public virtual bool ReadAPDTempAdc(out  int APDTempAdc) { APDTempAdc = 0; return false; }
       
        public virtual double ReadDmiLaTemp() { return 0.0; }
        public virtual bool ChkLaTempHA() { return false; }
        public virtual bool ChkLaTempLA() { return false; }
        public virtual bool ChkLaTempHW() { return false; }
        public virtual bool ChkLaTempLW() { return false; }

        public virtual bool SetTxpcoefa(string txcoefa) { return false; }
        public virtual bool SetTxpcoefb(string txcoefb) { return false; }
        public virtual bool SetTxpcoefc(string txcoefc) { return false; }
        public virtual bool ReadTxpcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxpcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxpcoefc(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxpFitsCoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxpFitsCoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxpFitsCoefc(out string strcoef) { strcoef = ""; return false; }

        public virtual bool SetTxAuxcoefa(string txcoefa) { return false; }
        public virtual bool SetTxAuxcoefb(string txcoefb) { return false; }
        public virtual bool SetTxAuxcoefc(string txcoefc) { return false; }
        public virtual bool ReadTxAuxcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxAuxcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadTxAuxcoefc(out string strcoef) { strcoef = ""; return false; }

        public virtual bool SetLaTempcoefa(string Latempcoefa) { return false; }
        public virtual bool SetLaTempcoefb(string Latempcoefb) { return false; }
        public virtual bool SetLaTempcoefc(string Latempcoefc) { return false; }
        public virtual bool ReadLaTempcoefa(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadLaTempcoefb(out string strcoef) { strcoef = ""; return false; }
        public virtual bool ReadLaTempcoefc(out string strcoef) { strcoef = ""; return false; }

        //EEPROMINIT;
        public virtual bool EEpromInitialize() { return false; }
        //读取EVB VCC
        public virtual double ReadEvbVcc() { return 0; }//读取EVB采样电压

        public virtual bool CheckEvbVcADC_Coef_flag() { return false;}//检测EVB Vcc是否被校验过系数
	public virtual bool APCCloseOpen(bool apcswitch) { return false; }
        //CFP4  开关PID，pid1,pid2
        public virtual bool PIDCloseOpen(bool pidswitch) { return false; }
        public virtual bool SetcoefP1(string p) { return false; }
        public virtual bool SetcoefI1(string i) { return false; }
        public virtual bool SetcoefD1(string d) { return false; }
        public virtual bool SetPIDSetpoint1(string setpoint) { return false; }
        public virtual bool SetcoefP2(string p) { return false; }
        public virtual bool SetcoefI2(string i) { return false; }
        public virtual bool SetcoefD2(string d) { return false; }
        public virtual bool SetPIDSetpoint2(string setpoint) { return false; }

        //CFP dmiRxOffset
        public virtual bool SetdmiRxOffset(string dmiRxOffset) { return false; }

       //new add for sfplr 1f map
        public virtual bool SetTxEQcoefa(string TxEQcoefa) { return false; }
        public virtual bool SetTxEQcoefb(string TxEQcoefb) { return false; }
        public virtual bool SetTxEQcoefc(string TxEQcoefc) { return false; }


        //add for ats 2.0writeCrossDac
        public virtual bool WriteCrossDac(object crossdac) { return false; }

        public virtual bool WriteEA( object DAC) { return false; }
        public virtual bool StoreEA(object DAC) { return false; }
        public virtual bool StoreCrossDac(object crossdac) { return false; }
        public virtual bool ReadCrossDac(int length, out byte[] crossdac) { crossdac = new byte[16]; return false; }
        public virtual bool FullFunctionEnable() { return true; }

        public virtual bool SetBiasAdcOffset(UInt16 value) { return false; }

        public virtual bool ReadBiasAdcOffset(out ushort value) { value = 0; return false; }


        #region  EML-AdjustEye

        /// <summary>
        /// 获得寄存器值的上下限
        /// </summary>
        /// <param name="ItemType">类型ibias or Imod  0=Ibias 1=Imod </param>
        /// <param name="Max">最大值</param>
        /// <param name="min">最小值</param>
        /// <returns>封装之后的类</returns>
        ///
        public virtual bool GetRegistValueLimmit(byte ItemType, out Int32 Max) 
        {
            Max = 0;
           
            return true;
        }


        /// <summary>
        /// 控制EA寄存器
        /// </summary>
        /// <param name="ControlType">  1=Write 2=store </param>
        /// <param name="DAC">Value</param>
        /// <returns>结果</returns>
        public virtual bool WriteEA(byte ControlType,object DAC) { return false; }
        /// <summary>
        /// 控制VLD寄存器
        /// </summary>
        /// <param name="ControlType">  1=Write 2=store </param>
        /// <param name="DAC">Value</param>
        /// <returns>结果</returns>
        public virtual bool WriteVLD(byte ControlType, object DAC) { return false; }
        /// <summary>
        /// 控制VC寄存器
        /// </summary>
        /// <param name="ControlType">  1=Write 2=store </param>
        /// <param name="DAC">Value</param>
        /// <returns>结果</returns>
        public virtual bool WriteVC(byte ControlType, object DAC) { return false; }
        /// <summary>
        /// 控制VG寄存器
        /// </summary>
        /// <param name="ControlType">  1=Write 2=store </param>
        /// <param name="DAC">Value</param>
        /// <returns>结果</returns>
        public virtual bool WriteVG(byte ControlType, object DAC) { return false; }

#region Leo Debug
        public virtual bool WriteBiasDac1(object biasdac) { return false; }
        public virtual bool WriteModDac1(object moddac) { return false; }
        public virtual bool WriteMaskDac1(object DAC) { return false; }
     

#endregion

        public virtual bool SetDutDataRate(double datarate)
        { return true; }
        public enum CFPEVBPin
        {
            TxDisable, LpMode, ModRst, RxLos, GlbAlarm, Alarm1, Alarm2, Alarm3, Ctrl1, Ctrl2, Ctrl3, MDCIN, MDIO1, MDIO2, SCK, DR
        }
        public virtual bool EVBInitialize()
        {
            return true;
        }
        public virtual bool PinCtrl(CFPEVBPin pin, bool enable)
        {
            return true;
        }
        public virtual bool ChkRxLosHW()
        {
            return true;
        }
        public virtual bool SetHWTxDis(bool enable)
        {
            return true;
        }
       
#endregion
    }
}
