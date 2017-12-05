using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ivi.Visa.Interop;
using System.Reflection;

namespace ATS_Framework
{
    public class PPG : EquipmentBase
    {

       public PPG()
        {
        }
      
       #region Bert
       public string TriggerDRatio;//2
       public string TriggerMode;//"DCL""PATT"
       public string ClockHigVoltage;
       public string ClockLowVoltage;
       public string DataHigVoltage;
       public string DataLowVoltage;

       public string BertDataRate;
       public string PRBS;//7,31..
       #endregion
       //MP1800
       public string configFilePath;
       public byte slot;
       public byte clockSource;
       public byte auxOutputClkDiv;
       public byte totalChannels;
       public byte prbsLength;
       public byte patternType;
       public byte dataSwitch;
       public byte dataTrackingSwitch;
       public byte dataLevelGuardSwitch;
       public byte dataAcModeSwitch;
       public byte dataLevelMode;
       public byte clockSwitch;
       public byte outputSwitch;

       public string dataRate;
       public double dataLevelGuardAmpMax;
       public double dataLevelGuardOffsetMax;
       public double dataLevelGuardOffsetMin;
       public double dataAmplitude;
       public double dataCrossPoint;

       //public string IOType;
       //public string Addr;
       //public string Name;
       //public bool Reset;

       public string patternfile;
        
#region // GBIP order
        //public virtual bool ConfigureDataRate() { return true; }
        //public virtual bool ConfigurePRBS() { return true; }
        //public virtual bool ConfigureDataVoltage() { return true; }
        //public virtual bool ConfigureClockVoltage() { return true; }
        //public virtual bool ConfigureTriggerMode() { return true; }
        //public virtual bool ConfigureTriggerDRatio() { return true; }
        
# endregion
#region // USB order

        //PG_Trigger_DRatio
        //------------------------------ 10G Bert
       
        public virtual bool ConfigureDataInput(byte Channel) { return true; }
        //Function:设置接收方式 datainput为0代表光口输入（Optical Receiver），1代表电口输入（Electrical）2代表两者都有(Both Receiver）
        public virtual bool ConfigureSYNCTest(){ return true; }
        //Function:测试同步状况 返回值为同步情况，true代表同步，false代表没有   
        public virtual bool ConfigureLaserOnoff(bool Swith) { return true; }
        //Function:设定激光状态 Parameter:status status为0代表off状态，1代表on状态 Default:

        public virtual bool ConfigureETxPolarity(bool polarity) { return true; }
        //Function:电口输出端极性设定 Parameter:polarity polarity为1代表（Non-invert不翻转），0代表（Invert翻转）

        public virtual bool ConfigureOTxPolarity(bool polarity) { return true; }
        //Function:光口输出端极性设定 Parameter:polarity polarity为1代表（Non-invert不翻转），0代表（Invert翻转）
        public virtual bool ConfigureERxPolarity(bool polarity) { return true; }
         //  public override bool Configure_ERxPolarity(string Str_polarity)
      //  public virtual bool Configure_ERxPolarity(string Str_polarity){return true;}

        public virtual bool ConfigureORxPolarity(bool polarity) { return true; }

# endregion
    }
}
