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
        //public string IOType;
        //public string Addr;
        //public string Name;
        //public bool Reset;
        public string[] BidiTx_Channel;
        public string[] BidiRx_Channel;
        public virtual bool Switchchannel(int syn = 0) { return true; }
        /// <summary>
        /// 切换模式 (0=长光纤;1=短光纤)
        /// </summary>
        /// <param name="Type">类别 0=长光纤;1=短光纤</param>
        /// <param name="syn">同步 1=同步;0=异步</param>
        /// <returns></returns>
        virtual public bool SelectMode(byte Type, int syn = 0) { return true;}

    }
}

