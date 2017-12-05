using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ATS_Framework
{
    public class Scope : EquipmentBase
    {

        public globalParameters pglobalParameters;
       
        public Scope()
        {
        }
        #region Scope
        public string OptChannel;
        public string ElecChannel;
        public string Scale;
        public string Offset;
        //public string MaskName; //"10GbE_10_3125_May02.msk"
        public string DcaAtt;
        public string FilterFreq;
        public byte Percentage;  //margin Percentage
        public string DcaThreshold;
        public string TriggerBwlimit; // 0 HIGH\1 LOW\2 DIV
        public string DcaWavelength;  //0 850,1 1310,2 1550 default 850
        public string DcaDataRate;
        public int WaveformCount;
        public int setscaledelay;
        #endregion
        public string IOType;
        public string Addr;
        public string Name;
        public bool Reset;

        #region Flex86100Scope
        public string configFilePath;
        public byte FilterSwitch;
        public byte triggerSource;
        public byte opticalSlot;
        public byte elecSlot;
        public byte opticalAttSwitch;
        public byte erFactorSwitch;
        public double erFactor;
        public string FlexOptChannel;
        public string  FlexElecChannel;
        public byte precisionTimebaseModuleSlot;
        public byte precisionTimebaseSynchMethod;
        public byte rapidEyeSwitch;
        public byte Threshold;
        public byte reference;
        public byte marginType;
        public byte marginHitType;
        public byte acqLimitType;
        public double precisionTimebaseRefClk;
        public double marginHitRatio;
        public string opticalMaskName;
        public string elecMaskName;
        public string opticalEyeSavePath;
        public string elecEyeSavePath;
        public int marginHitCount;
        public int acqLimitNumber;
        public byte FlexTriggerBwlimit;
        public byte FlexDcaWavelength;
        public double FlexDcaDataRate;
        public double FlexFilterFreq;
        public double FlexDcaAtt;
        public double FlexScale;
        public double FlexOffset;
        public int flexsetscaledelay;
        #endregion


        virtual public bool OpenOpticalChannel(bool Switch,int syn=0) { return true; }

        virtual public bool ClearDisplay() { return true; }
        virtual public bool SetMode(byte Mode, int syn = 0) { return true; }
        virtual public bool AutoScale(int syn = 0) { return true; }
        virtual public bool RunStop(bool run) { return true; }

        virtual public double GetEratio() { return 0; }

        virtual public double GetAveragePowerWatt() { return 0; }
        virtual public double GetAveragePowerdbm() { return 0; }
        virtual public bool DisplayPowerWatt() { return false; }
        virtual public bool DisplayPowerdbm() { return false; }

        virtual public bool MaskONOFF(bool MaskOn, int syn = 0) { return true; }

        virtual public bool SetMaskAlignMethod(byte method, int syn = 0) { return true; }
        virtual public bool SetRunTilOff(int syn = 0) { return true; }

        virtual public bool DisplayER() { return true; }

        virtual public bool SetScaleOffset(int syn = 0) { return true; }
        public virtual double[] OpticalEyeTest(int syn=0) { return new double[16]; }
        public virtual double[] ElecEyeTest(int syn=0) { return new double[16]; }
        public virtual bool DisplayThreeEyes(int syn = 0) { return true; }
    }
}
