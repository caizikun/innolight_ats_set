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
        public bool ChipsetControll;
        public byte MoudleChannel;
        public int deviceIndex;
        public DutStruct[] DutStruct;
        public DriverStruct[] DriverStruct;
        public DriverInitializeStruct[] DriverInitStruct;
        public bool FindFiledNameChannel(out int i, string filedname)
        {
            i = 0;
            while ((DutStruct[i].FiledName != filedname) || (DutStruct[i].Channel != MoudleChannel))
            {
                i++;
                if (i > DutStruct.Length)
                    break;
            }
            if (i > DutStruct.Length)
                return false;
            else
                return true;

        }
        public bool FindFiledNameChannelDAC(out int i, string filedname)
        {
            i = 0;
            while ((DriverStruct[i].FiledName != filedname) || (DriverStruct[i].MoudleLine != MoudleChannel))
            {
                i++;
                if (i > DriverStruct.Length)
                    break;
            }
            if (i > DriverStruct.Length)
                return false;
            else
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
        public virtual void SetSoftTxDis() { }
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

        public virtual bool ReadVccADC(out UInt16 vccadc, byte vccselect) { vccadc = 0; return false; }
        public virtual bool ReadTempADC(out UInt16 vccadc, byte TempSelect) { vccadc = 0; return false; }



        public virtual void Engmod(byte engpage) {  }
        public virtual bool WritePort(int id, int deviceIndex, int Port, int DDR) { return false; }
        public virtual byte[] ReadPort(int id, int deviceIndex, int Port, int DDR) { return new byte[16]; }
        public virtual byte[] ReadReg(int deviceIndex, int deviceAddress, int regAddress, int readLength) { return new byte[16]; }
        public virtual byte[] WriteReg(int deviceIndex, int deviceAddress, int regAddress, byte[] dataToWrite) { return new byte[16]; }

        public virtual bool DriverInitialize() { return false; }
        public virtual bool WriteBiasDac(object biasdac) { return false; }
        public virtual bool WriteModDac(object moddac) { return false; }
        public virtual bool WriteLOSDac(object losdac) { return false; }
        public virtual bool WriteAPDDac(object apddac) { return false; }
        public virtual bool StoreBiasDac(object biasdac) { return false; }
        public virtual bool StoreModDac(object moddac) { return false; }
        public virtual bool StoreLOSDac(object losdac) { return false; }
        public virtual bool StoreAPDDac(object apddac) { return false; }


        public virtual bool ReadBiasDac(int length, out byte[] BiasDac) { BiasDac=new byte[16];return false; }
        public virtual bool ReadModDac(int length, out byte[] ModDac) { ModDac = new byte[16]; return false; }
        public virtual bool ReadLOSDac(int length, out byte[] LOSDac) { LOSDac = new byte[16]; return false; }
        public virtual bool ReadAPDDac(int length, out byte[] APDDac) { APDDac = new byte[16]; return false; }




        //set coef
        public virtual bool SetTempcoefb(string tempcoefb) { return false; }
        public virtual bool SetTempcoefb(string tempcoefb, byte TempSelect) { return false; }
        public virtual bool SetTempcoefc(string tempcoefc) { return false; }
        public virtual bool SetTempcoefc(string tempcoefc, byte TempSelect) { return false; }
        public virtual bool SetVcccoefb(string vcccoefb) { return false; }
        public virtual bool SetVcccoefb(string vcccoefb, byte vccselect) { return false; }
        public virtual bool SetVcccoefc(string vcccoefc) { return false; }
        public virtual bool SetVcccoefc(string vcccoefc, byte vccselect) { return false; }
        public virtual bool SetRxpcoefa(string rxcoefa) { return false; }
        public virtual bool SetRxpcoefb(string rxcoefb) { return false; }
        public virtual bool SetRxpcoefc(string rxcoefc) { return false; }
        public virtual bool SetTxSlopcoefa(string txslopcoefa) { return false; }
        public virtual bool SetTxSlopcoefb(string txslopcoefb) { return false; }
        public virtual bool SetTxSlopcoefc(string txslopcoefc) { return false; }
        public virtual bool SetTxOffsetcoefa(string txoffsetcoefa) { return false; }
        public virtual bool SetTxOffsetcoefb(string txoffsetcoefb) { return false; }
        public virtual bool SetTxOffsetcoefc(string txoffsetcoefc) { return false; }
        public virtual bool SetBiasdaccoefa(string biasdaccoefa) { return false; }
        public virtual bool SetBiasdaccoefb(string biasdaccoefb) { return false; }
        public virtual bool SetBiasdaccoefc(string biasdaccoefc) { return false; }
        public virtual bool SetModdaccoefa(string moddaccoefa) { return false; }
        public virtual bool SetModdaccoefb(string moddaccoefb) { return false; }
        public virtual bool SetModdaccoefc(string moddaccoefc) { return false; }
        public virtual bool SetTargetLopcoefa(string targetlopcoefa) { return false; }
        public virtual bool SetTargetLopcoefb(string targetlopcoefb) { return false; }
        public virtual bool SetTargetLopcoefc(string targetlopcoefc) { return false; }

        

        //read coef
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
        public virtual bool SetRXPadcThreshold(byte threshold) { return false; }
       //reference temperature
        public virtual bool SetReferenceTemp(string referencetempcoef) { return false; }
        //txp proportion 
        public virtual bool SetTxpProportionLessCoef(string Lesscoef) { return false; }
        public virtual bool SetTxpProportionGreatCoef(string Greatcoef) { return false; }

        //txp fits coef
        public virtual bool SetTxpFitsCoefa(string txpfitscoefa) { return false; }
        public virtual bool SetTxpFitsCoefb(string txpfitscoefb) { return false; }
        public virtual bool SetTxpFitsCoefc(string txpfitscoefc) { return false; }

        //apc
        public virtual bool APCON(byte apcswitch) { return false; }
        public virtual bool APCOFF(byte apcswitch) { return false; }
        public virtual bool APCStatus(out byte apcflag) { apcflag = 0x00; return false; }
       
    }
}
