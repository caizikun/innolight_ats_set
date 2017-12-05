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
using System.IO;



namespace ATS_Framework
{
    public class Powersupply : EquipmentBase
    {
        
        public Powersupply()
        {
        }
        #region Powersupply

        public string DutChannel;
        public string OptSourceChannel;

        public string DutVoltage;
        public string DutCurrent;

        public string OptVoltage;
        public string OptCurrent;

        public string voltageoffset;
        public string currentoffset;
        public int opendelay;
        public int closedelay;

        #endregion

        //public string IOType;
        //public string Addr;
        ////public string Name;
        //public bool Reset;



        public virtual bool ConfigVoltageCurrent(string voltage, int syn = 0) { return true; }
       public virtual double GetCurrent() { return 0; }
       public virtual double GetVoltage() { return 0; }

    }
    
}
