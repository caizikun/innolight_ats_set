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
        public string TotalChannel;
        public string Wavelength;
        #endregion
        public string IOType;
        public string Addr;
        public string Name;
        public bool Reset;

        public int opendelay;
        public int closedelay;
        public int setattdelay;

       

        public virtual bool ConfigWavelength( int syn = 0) { return true; }
        public virtual bool AttnValue(string Value, int syn = 0) { return true; }
        public virtual bool AddCalFactor(string CalFactor) { return true; }
        public virtual double GetAtten() { return 0; }
    }
}
