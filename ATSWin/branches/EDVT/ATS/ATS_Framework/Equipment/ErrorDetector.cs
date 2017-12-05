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
    public class ErrorDetector : EquipmentBase
    {
       
        #region Bert
        public bool CDRSwitch;
        public string CDRFreq;
        public string PRBS;//7,31..
        #endregion
        public string IOType;
        public string Addr;
        public string Name;
        public bool Reset;
        //MP1800
        public byte slot;
        public byte totalChannels;
        public byte currentChannel;
        public byte dataInputInterface;
        public byte prbsLength;
        public byte patternType;
        public byte errorResultZoom;
        public byte edGatingMode;
        public byte edGatingUnit;
        public int edGatingTime;

        public virtual double GetErrorRate() { return 1; }
        public virtual bool AutoAlaign(bool becenter) { return true; }
        #region // USB order
        public virtual bool ConfigureLaserOnoff(bool Swith) { return true; }

        public virtual bool ConfigureDataInput(byte datainput) { return true; }
        public virtual bool ConfigureSetGating(byte gating) { return true; }

        public virtual bool ConfigureORxPolarity(bool polarity) { return true; }
        public virtual string GetErrorRate(int i, int gating) { return "0"; }
        public virtual bool ConfigureERxPolarity(bool polarity) { return true; }
        # endregion
    }

}
