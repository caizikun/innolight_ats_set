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
    public class PowerMeter : EquipmentBase 
    {
        public PowerMeter()
        {
        }
       
        #region ATTPowerMeter
        public string PowerMeterSlot;
        public string PowerMeterWavelength;
        public string PowerMeterChannel;
        public string UnitType;//0 "dBm",1 "W"
        #endregion
        //public string IOType;
        //public string Addr;
        //public string Name;
        //public bool Reset;


        public virtual bool ConfigWavelength(int syn = 0) { return true; }
        public virtual double ReadPower() { return 0; }
        public virtual bool Selectunit(int syn = 0) { return true; }
       


    }
}
