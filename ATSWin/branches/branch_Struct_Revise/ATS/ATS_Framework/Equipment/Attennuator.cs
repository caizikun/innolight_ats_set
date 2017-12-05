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
    public class Attennuator : EquipmentBase 
    {
       
        public Attennuator()
        {
        }
        #region ATT
        public string AttSlot;
        public string AttValue;
        //public string AttWavelength;
        public string AttChannel;
        public string[] AttChannelArray;
        public string TotalChannel;
        public string Wavelength;
        #endregion
        //public string IOType;
        //public string Addr;
        //public string Name;
        //public bool Reset;

        public int opendelay;
        public int closedelay;
        public int setattdelay;
        public int SleepTime;
        public Double PowerVariation;
        public Double LastAttValue = -5;
        public byte LastAttChannel = 0;

        public virtual bool ConfigWavelength( int syn = 0) { return true; }
        public virtual bool AttnValue(string InputPow, int syn = 1) { return true; }
        public virtual bool AddCalFactor(string CalFactor) { return true; }
        public virtual double GetAtten() { return 0; }
        public virtual bool SetAllChannnel_RxOverLoad(float RxOverLoad) { return true; }

        public virtual bool SetAttnValue(Double AttValue, int syn = 1) { return false; }
        public virtual bool AdjustAttnValue(Double AttValue, int syn = 1) { return true; }// 在当前衰减器的衰减值上调整
        public virtual bool SetInputPow(double InputPower, int syn = 1) { return false; }

    }
}
