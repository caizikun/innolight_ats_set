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
        //public string IOType;
        //public string Addr;
        //public string Name;
        //public bool Reset;

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
        public string opticalMaskName2;
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
        public byte DiffSwitch; //0 off,1 on
        public byte BandWidth;//1,2,3,4
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
        virtual public bool DisplayOmA() { return true; }
        virtual public bool  SetScaleOffset(double PowerVaule,int syn=0) { return true; }
        public virtual bool OpticalEyeTest(out double[] testResults, int syn = 0) { testResults = new double[9]; return false; }
       // public virtual bool EyeDiagramSave(int syn = 0) { return false; }

        public virtual bool ElecEyeTest(out double[] testResults, int syn = 0) { testResults = new double[9]; return false; }
        public virtual bool DisplayThreeEyes(int syn = 0) { return true; }

        public virtual bool DisplayEyeHeight() { return false; }
        public virtual bool DisplayEyeWidth() { return false; }
        public virtual bool DisplayCrossing() { return false; }
        public virtual bool DisplayJitter(byte jitterFormat) { return false; }//[20160419]Nate: add
        public virtual double GetCrossing() { return 0.0; }

        public virtual double GetAMPLitude() { return 0; }
        public virtual bool EyeTuningDisplay(byte Switch, int syn = 0) { return true; }
        public virtual bool SetMaskLimmit(int acqLimitNumber){return true;}

        public  virtual void GetAllChannel_Mask_OMA(int ChannelCount,out double[] MaskArray,out double[] OMAArray)
        {
            MaskArray=null;
            OMAArray=null;
        }
        public virtual int GetMask()
        {
            return -10;
        }
        public virtual bool LoadMask()
        {
            return true;
        }
        public virtual double GetJitterPP()
        {
            return 0;
        }
        public virtual double GetTxRinOmA()
        {
            return 200;
        }
        public virtual double GetTxTDEC(int syn = 0)
        {
            return 0;
        }
        public virtual double[] MeasureVCMI()
        {
            double[] data = {200,200};
            return data;
        }//nate add
        public virtual bool MeasureVEC(out double[] outData, int syn = 0)
        {
            outData = new double[3];
            return false; 
        }//nate add
        public virtual double GetAMP_5()
        {
            return 0;
        }//nate add
        //GetTxRinOmA();
        public virtual double GetJitterRSM()
        {
            return 0;
        }
        /// <summary>
        /// 模板余量测试方法设置
        /// </summary>
        /// <param name="marginOnOff"> 是否打开Margin,1=ON,0=OFF </param>
        /// <param name="marginAutoManul">手动测试Margin还是自动测试，1=自动，0=手动 </param>
        /// <param name="manualMarginPercent">手动测试Margin时，输入Margin值，自动测试时忽略该参数 </param>
        /// <param name="autoMarginType">自动测试Margin时，Margin类型选择，1=HitRatio,0=HitCount </param>
        /// <param name="hitRatio"> HitRatio的数值，例如1E-6 </param>
        /// <param name="hitCount">HitCount的数值，例如0 </param>
        /// <returns>结果</returns>
        virtual public bool MaskTestMarginSetup(byte marginOnOff, byte marginAutoManul, int manualMarginPercent, byte autoMarginType, double hitRatio, int hitCount, int syn = 0)
        {

            /// <summary>
            /// 模板余量测试方法设置
            /// </summary>
            /// <param name="marginOnOff">
            /// 是否打开Margin,1=ON,0=OFF
            /// </param>
            /// <param name="marginAutoManul">
            /// 手动测试Margin还是自动测试，1=自动，0=手动
            /// </param>
            /// <param name="manualMarginPercent">
            /// 手动测试Margin时，输入Margin值，自动测试时忽略该参数
            /// </param>
            /// <param name="autoMarginType">
            /// 自动测试Margin时，Margin类型选择，1=HitRatio,0=HitCount
            /// </param>
            /// <param name="hitRatio">
            /// HitRatio的数值，例如1E-6
            /// </param>
            /// <param name="hitCount">
            /// HitCount的数值，例如0
            /// </param>
            /// <returns></returns>
            return true;
          }

        public virtual bool SavaScreen(string filePath)
        {
            return true;
        }

        public virtual bool PtimebaseStatue(bool isON)
        {
            return true;
        }

        public virtual bool TriggerSourceFpanel()
        {
            return true;
        }

        public virtual bool TriggerPlock(bool isON)
        {
            return false;
        }

        public virtual bool MeasureRxEyeVecEW(ref Dictionary<string,double> result, int syn = 0)
        {
            return false;
        }//nate add
    }
}
