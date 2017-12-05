using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ATS_Framework
{           
    public class Thermocontroller : EquipmentBase
    {
       
        #region XSTREAM
        public string FLSE;
        public string ULIM;
        public string LLIM;
        public string Sensor; //0 No Sensor,1 T,2 k,3 rtd,4 diode 
        #endregion
        public string IOType;
        public string Addr;
        public string Name;
        public bool Reset;

        public virtual string ReadCurrentTemp() { return "0"; }
        public virtual bool AirFlowSetting() { return true; }
        public virtual bool AirTempsUpperlimit() { return true; }
        public virtual bool AirTempslowerlimit() { return true; }
        public virtual bool SensorType() { return true; }//0 No Sensor,1 T,2 k,3 rtd,default diode 
        public virtual bool DUTControlModeOnOFF(byte Switch) { return true; }//1 ON,0 OFF
        public virtual bool lockHeadPosition(byte Switch) { return true; }//1 UP,0 DOWN
        public virtual string ReadSetpoint() { return "0"; }
        public virtual string ReadSetpointTemp() { return "0"; }
        public virtual bool SetPositionUPDown(string position) { return true; }//0 UP,1 DOWN
        public virtual bool SetPoint(string Point) { return true; }//0 HOT,1 AMB,2 code
        public virtual bool SetPointTemp(double Temp) { return true; }

    }
}
