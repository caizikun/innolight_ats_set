using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS_Framework
{
    public class ESCOPE : EquipmentBase
    {
        public ESCOPE()
        { }
        public string IOType;
        public string  Addr;
        public string Name;
        public bool Reset;
        public byte ElecSignalCH;//用于切换PPG和RX电信号的ESCOPE通道号
        public byte OPTtoElecCH;//光信号转为电信号的通道号
        public byte SCLCH;//SCL信号通道号
        public byte EDVTCH;//EDVT控制切换信号的通道号
        public byte AProbeCH;//电流探头通道号
        public byte VProbeCH;//电压探头通道号
        public byte ResetLCH;//Resetl信号探头通道号
        virtual public bool Acquire(bool AverageSwitch, bool pointautoswitch, double avercount, double memorydepth) { return true; }
        virtual public bool ChannelSet(byte channel, bool bwlimitswitch, bool displayswitch, byte input, byte unit, double scale, double offset) { return true; }
        virtual public bool CheckTriggerOccurs(int rechecktime) { return true; }
        virtual public bool CLS() { return true; }
        virtual public bool AutoScale() { return true; }
        virtual public bool CDISplay() { return true; }
        virtual public bool RunStop(byte control) { return true; }
        virtual public string MeasureDeltatime(byte channelA, byte channelB, byte startdir, byte startposition, byte stopdir, byte stopposition, double startnum, double stopnum) { return ""; }
        virtual public bool RunAuto() { return true; }
        virtual public bool SavePngToPC() { return true; }
        virtual public string SearchTime(byte channel) { return ""; }
        virtual public string SetMarker(string tstart, string tstop) { return ""; }
        virtual public bool SetSingle() { return true; }
        virtual public bool SetTimeBase(byte view, byte reference, bool rollenable, double delayt, double scale, double windownsacle, double position) { return true; }
        virtual public bool TriggerCmd(byte trigenable, byte andsourcechannel, byte andsourceHL, double holdofftime, byte sourcechannel, double triglevel, byte trigmode, byte sweepmode) { return true; }
        virtual public bool TriggerEdge(byte coupling, byte slope, byte sourcechannel, bool AnalogNoiseReject) { return true; }
        virtual public bool TriggerTimeout(byte condition, byte sourcechannel, double time) { return true; }
       
        virtual public bool WriteCmd(string cmd) { return true; }
        virtual public string ReadCmd(string cmd) { return ""; }
        virtual public bool DisplayOFF() { return true; }

        virtual public string TriggerOccure() { return ""; }
        virtual public bool MeasureClear() { return true; }
        virtual public string MeasureTstop(double vstop, bool slope, byte crossing, byte channel) { return ""; }
    }
}
