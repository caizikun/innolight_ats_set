using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
namespace ATS_Framework
{

   
    public class EquipmentBase
    {
        //public  Semaphore semaphore;
        public string CurrentChannel;
        public SortedList<string, string> offsetlist = new SortedList<string, string>();
        
        public string log = "";
        public IOPort MyIO;
        public IOPort USBIO;
        public bool EquipmentConnectflag = false;// have used
        public bool EquipmentConfigflag = false;// config ok

        //----------------------Leo Add
        public int Referenced_Times = 0;// 仪器被使用的次数
        public  bool bReusable = false; // 仪器可以被复用
        public bool EquipmentErrorflag = false;// equipment Error

        public logManager logger;
        public bool bReady //equipment is ready to be used by test model.
        {
            get
            {
                if ((Referenced_Times == 0) || bReusable) return true;
                else return false;
            }
        }

        public void IncreaseReferencedTimes()
        {
            Referenced_Times++;
        }
        public void DecreaseReferencedTimes()
        {
            Referenced_Times--;
            if (Referenced_Times < 0) Referenced_Times = 0;
        }
        public virtual bool Switch(bool Switch, int syn = 0) { return true; } 
        //-----------------------
        public virtual bool Configure(int syn=0)
        {
            return true;
        }
        public virtual bool Initialize(TestModeEquipmentParameters[] LIST)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] List)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList,string AuxAttribles)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList,DriverInitializeStruct[] DriverinitList, string AuxAttribles)
        {
            return true;
        }
        public virtual bool Initialize(DutStruct[] DutList, DriverStruct[] DriverList, DriverInitializeStruct[] DriverinitList, DutEEPROMInitializeStuct[] EEpromInitList, string AuxAttribles)
        {
            return true;
        }
        public string GetLogInfo()
        {
            return log;
        
        }
        public virtual bool Connect() { return true; }
        public virtual bool ChangeChannel(string channel,int syn=0) { return true; }
        public virtual bool configoffset(string channel, string offset, int syn = 0) { return true; }

       
    }
}
