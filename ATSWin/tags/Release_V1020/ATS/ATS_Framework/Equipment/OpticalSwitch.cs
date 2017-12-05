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
    public class OpticalSwitch : EquipmentBase
    {

        public OpticalSwitch()
        {
        }

        public string OpticalSwitchSlot;
        public string SwitchChannel;
        public string ToChannel;
        public string IOType;
        public string Addr;
        public string Name;
        public bool Reset;

        public virtual bool Switchchannel(int syn = 0) { return true; }


    }
}

