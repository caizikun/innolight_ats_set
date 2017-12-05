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
    public class ElecSwitch : EquipmentBase 
    {
        
        #region ElecSwitch
        public string ElecSwitchChannel;//electrical switch
        #endregion
        ////public string IOType;
        ////public byte Addr;
        ////public string Name;
        //public bool Reset;
       
        public virtual bool ChangeElecSwitchChannel() { return true; }
    }
}
