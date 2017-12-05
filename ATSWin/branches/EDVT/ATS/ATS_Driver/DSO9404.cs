using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using ATS_Framework;
using System.Windows.Forms;
using System.IO;
namespace ATS_Driver
{
   public  class DSO9404:ESCOPE
    {
       public Algorithm algorithm = new Algorithm();
        public DSO9404(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Initialize(TestModeEquipmentParameters[] DSO9404list)
        {
            try
            {
                int i = 0;
                if (algorithm.FindFileName(DSO9404list, "ADDR", out i))
                {
                    Addr = DSO9404list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "IOTYPE", out i))
                {
                    IOType = DSO9404list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "RESET", out i))
                {
                    Reset = Convert.ToBoolean(DSO9404list[i].DefaultValue);
                }
                else
                    return false;

                if (algorithm.FindFileName(DSO9404list, "NAME", out i))
                {
                    Name = DSO9404list[i].DefaultValue;
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "ELECSIGNALCH", out i))
                {
                    ElecSignalCH = Convert.ToByte(DSO9404list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "OPTTOELECCH", out i))
                {
                    OPTtoElecCH = Convert.ToByte(DSO9404list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "SCLCH", out i))
                {
                    SCLCH = Convert.ToByte(DSO9404list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "EDVTCH", out i))
                {
                    EDVTCH = Convert.ToByte(DSO9404list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "APROBECH", out i))
                {
                    AProbeCH = Convert.ToByte(DSO9404list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "VPROBECH", out i))
                {
                    VProbeCH = Convert.ToByte(DSO9404list[i].DefaultValue);
                }
                else
                    return false;
                if (algorithm.FindFileName(DSO9404list, "RESETLCH", out i))
                {
                    ResetLCH = Convert.ToByte(DSO9404list[i].DefaultValue);
                }
                else
                    return false;
                if (!Connect()) return false;
            }
            catch (Error_Message error)
            {

                logger.AdapterLogString(3, error.ToString());
                return false;
            }
            return true;
        }
        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":

                       // MyIO = new IOPort(IOType, "GPIB::" + Addr);
                        MyIO = new IOPort(IOType, Addr);
                        EquipmentConnectflag = true;
                        break;
                    case "USB":
                        MyIO = new IOPort(IOType, Addr.ToString());
                        EquipmentConnectflag = true; ;
                        
                        break;
                    default:
                        EquipmentConnectflag = false;
                        break;
                }

                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                EquipmentConnectflag = false;
                return false;
            }
        }
        public bool ReSet()
        {
            if (MyIO.WriteString("*RST"))
            {
                Thread.Sleep(3000);
                return true;
            }
            else
            {
                return false;
            }

        }
        public override bool Configure()
        {

            try
            {

                if (EquipmentConfigflag)//曾经设定过
                {

                    return true;

                }
                else//未曾经设定过
                {
                    if (Reset == true)
                    {
                        ReSet();
                    }
                   

                    EquipmentConfigflag = true;
                }
                return true;

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }

        public override bool Acquire(bool AverageSwitch,bool pointautoswitch,double avercount,double memorydepth)
        {
            bool flag = false;
            string average;
            string points;
            try
            {
                if (AverageSwitch)
                    average = "ON+;:ACQuire:AVERage:COUNt " + avercount.ToString();
                else
                    average = "OFF ";
                if (pointautoswitch)
                    points = "ON  ";
                else
                    points = "OFF+;:ACQuire:POINts:ANALog " + memorydepth.ToString();

                flag = MyIO.WriteString(":ACQuire:AVERage " + average + ";:ACQuire:POINts:AUTO " + points);
                logger.AdapterLogString(0, "avercount is" + average + "memorydepth is" + points);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }

        public override bool ChannelSet(byte channel, bool bwlimitswitch, bool displayswitch, byte input,byte unit,double scale,double offset)
        {
            bool flag = false;
            string bw;
            string display;
            string acdc;
            string strunit;
            try
            {
                if((channel<0)||(channel>4))
                {
                   channel=0;
                }
                if (bwlimitswitch)
                { 
                    bw = ":BWLimit ON";
                }
                else
                { 
                    bw = ":BWLimit OFF"; 
                }
                if (displayswitch)
                { 
                    display = ";DISPLAY ON"; 
                }
                else
                { 
                    display = ";DISPLAY OFF"; 
                }
                switch (input)
                {
                    case 0:
                        acdc = ";INPut DC";
                        break;
                    case 1:
                        acdc = ";INPut AC";
                        break;
                    case 2:
                        acdc = ";INPut DC50";
                        break;
                    default:
                        acdc = ";INPut DC";
                        break;
                }
                switch (unit)
                {
                    case 0:
                        strunit = ";UNITS VOLT";
                        break;
                    case 1:
                        strunit = ";UNITS WATT";
                        break;
                    case 2:
                        strunit = ";UNITS AMPere";
                        break;
                    case 3:
                        strunit = ";UNITS UNKNown";
                        break;
                    default:
                        strunit = ";UNITS VOLT";
                        break;
                }
                flag = MyIO.WriteString(":CHANNEL" + channel + bw + display + acdc + strunit + ";SCALE " + scale.ToString() + ";OFFSET "+offset.ToString());
                logger.AdapterLogString(0, "channel is" + channel + "unit is" + strunit);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool DisplayOFF() //turn off channel x
        {
            
            try 
            {
                for (int i = 1; i < 5; i++)

                { 
                    MyIO.WriteString(":CHANNEL" + i + ":DISPLAY OFF"); 
                }
               
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }


            return true; 
        }
        public override bool CheckTriggerOccurs(int rechecktime)
        {

            int t=0;
            try
            {
                while (t != 1)
                {
                    MyIO.WriteString(":TER?");
                    string tretime = MyIO.ReadString(1024);
                    t = Convert.ToInt32(tretime);
                    System.Threading.Thread.Sleep(rechecktime);

                }

                logger.AdapterLogString(0, "rechecktime is" + rechecktime);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        public override bool CLS()
        {
            try
            {
                return MyIO.WriteString("*CLS");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        override public bool AutoScale()
        {
            try
            {
                return MyIO.WriteString(":AUToscale");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        override public bool CDISplay()
        {
            try
            {
                return MyIO.WriteString(":CDISplay");
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }
        override public bool RunStop(byte control)
        {

            bool flag = false;
            string index;
            try
            {

                switch (control)
                {
                    case 0:
                        index = ":RUN";
                        break;
                    case 1:
                        index = ":SINGle";
                        break;
                    case 2:
                        index = ":STOP";
                        break;
                    default:
                        index = ":RUN";
                        break;
                }
               
                flag = MyIO.WriteString(index);
                logger.AdapterLogString(0, "runstop is" + index);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        override public string MeasureDeltatime(byte channelA,byte channelB,byte startdir,byte startposition,byte stopdir,byte  stopposition,double startnum,double stopnum)
        {

            string strstartdir;
            string strstartposition;
            string strstopdir;
            string strstopposition;
            string deltatime = "";
            try
            {

                switch (startdir)
                {
                    case 0:
                        strstartdir = "RISing";
                        break;
                    case 1:
                        strstartdir = "EITHer";
                        break;
                    case 2:
                        strstartdir = "FALLing";
                        break;
                    default:
                        strstartdir = "RISing";
                        break;
                }
                switch (startposition)
                {
                    case 0:
                        strstartposition = "MIDDle";
                        break;
                    case 1:
                        strstartposition = "UPPer";
                        break;
                    case 2:
                        strstartposition = "LOWer";
                        break;
                    default:
                        strstartposition = "MIDDle";
                        break;
                }
                switch (stopdir)
                {
                    case 0:
                        strstopdir = "RISing";
                        break;
                    case 1:
                        strstopdir = "EITHer";
                        break;
                    case 2:
                        strstopdir = "FALLing";
                        break;
                    default:
                        strstopdir = "RISing";
                        break;
                }
                switch (stopposition)
                {
                    case 0:
                        strstopposition = "MIDDle";
                        break;
                    case 1:
                        strstopposition = "UPPer";
                        break;
                    case 2:
                        strstopposition = "LOWer";
                        break;
                    default:
                        strstopposition = "MIDDle";
                        break;
                }
                MyIO.WriteString(":MEASure:DELTatime " + "CHANNEL" + channelA + "," + "CHANNEL" + channelB + ";:MEASURE:DELTATIME:DEFINE " + strstartdir + "," + startnum.ToString() + "," + strstartposition + "," + strstopdir + "," + stopnum.ToString() + strstopposition);
                MyIO.WriteString(":MEASure:DELTatime? " + "CHANNEL" + channelA + "," + "CHANNEL" + channelB);
                deltatime = MyIO.ReadString(1024);
                // flag = MyIO.WriteString(index);
                logger.AdapterLogString(0, "DELTatime is" + deltatime);
                return deltatime;

            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return "";
            }
        }
        override public bool RunAuto()
        {

            bool flag = false;
            try
            {

                flag = MyIO.WriteString(":RUN;:TRIGger:SWEep AUTO ");
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        override public bool SavePngToPC()
        {

            byte[] bmpData;
            string savePath = null;
            string diagramName = null;
            MyIO.WriteString(":Disp:Data? BMP,SCREEN");

            //System.Threading.Thread.Sleep(2000);
            bmpData = (byte[])MyIO.myDmm.ReadIEEEBlock(IEEEBinaryType.BinaryType_UI1);
            int len = bmpData.Length;
            savePath = Application.StartupPath + "\\EyeDiagram\\";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            System.DateTime CurrentTime = new System.DateTime();
            CurrentTime = System.DateTime.Now;
            string Str_Time = CurrentTime.ToString();
            string Str_Year = CurrentTime.Year.ToString();
            string Str_Month = CurrentTime.Month.ToString();
            string Str_Day = CurrentTime.Day.ToString();
            string Str_Hour = CurrentTime.Hour.ToString();
            string Str_minute = CurrentTime.Minute.ToString();
            diagramName = Str_Year + "_" + Str_Month + "_" + Str_Day + "_" + Str_Hour + "_" + Str_minute + ".bmp";
            File.WriteAllBytes(@savePath + diagramName, bmpData);
            return true;
        }
        override public string SearchTime(byte channel)
        {
            string tmax;
            string vmax;
            string tmin;
            string vmin;
            string tabmax;
            //bool flag = false;
            try
            {
                MyIO.WriteString(":MEASURE:TMAX? "+"CHANNEL"+channel);
                tmax = MyIO.ReadString(1024);
                MyIO.WriteString(":MEASURE:VMAX? " + "CHANNEL" + channel);
                vmax = MyIO.ReadString(1024);
                MyIO.WriteString(":MEASURE:TMIN? " + "CHANNEL" + channel);
                tmin = MyIO.ReadString(1024);
                MyIO.WriteString(":MEASURE:VMIN? " + "CHANNEL" + channel);
                vmin = MyIO.ReadString(1024);
                int intvmax = Convert.ToInt32(vmax);
                int intvmin = Convert.ToInt32(vmin);
                if (intvmax >= Math.Abs(intvmin))
                {
                    tabmax = tmax;
                }
                else
                {
                    tabmax = tmin;
                }
                logger.AdapterLogString(0, "time is" + tabmax);
                return tabmax;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return "";
            }
        }

        override public string SetMarker(string tstart,string tstop)
        {
            string XDELta;
            //bool flag = false;
            try
            {
                MyIO.WriteString(":MARKer:TSTArt " + tstart + ";:MARKer:TSTOP " + tstop);

                MyIO.WriteString(";:MARKer:XDELta?");
                XDELta = MyIO.ReadString(1024);
                MyIO.WriteString(":MARKer:VSTArt  -10;:MARKer:VSTOP  10");
                logger.AdapterLogString(0, "XDELta is" + XDELta);
                return XDELta;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return "";
            }
        }
        override public bool SetSingle()
        {

            //bool flag = false;
            try
            {

                MyIO.WriteString(":RSTATE?");
                string t = MyIO.ReadString(1024);
                if (t != "SING")
                {
                    MyIO.WriteString(":SINGLE");
                }
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        override public bool SetTimeBase(byte view, byte reference,bool rollenable,double delayt,double scale,double windownsacle,double position)
        {
            string strrollenable;
            string strreference;
            string strview;
            string delayscale;
            bool flag = false;
            try
            {
                switch (view)
                {
                    case 0:
                        strview = "MAIN";
                        delayscale = "";
                        break;
                    case 1:
                        strview = "WINDow";
                        delayscale = ";WINDOW:DELAY  " + delayt.ToString() + ";WINDOW:SCALE " + windownsacle.ToString();
                        break;
                    default:
                        strview = "MAIN";
                        delayscale = "";
                        break;
                }
                switch (reference)
                {
                    case 0:
                        strreference = "CENTer";
                        break;
                    case 1:
                        strreference = "RIGHt";
                        break;
                    case 2:
                        strreference = "LEFT";
                        break;
                    default:
                        strreference = "CENTer";
                        break;
                }
                if (rollenable)
                {
                    strrollenable = "ON";

                }
                else
                {
                    strrollenable = "OFF";
                }
              flag=  MyIO.WriteString(":TIMebase:VIEW " + strview + ";POSITION " + position.ToString() + ";SCALE " + scale.ToString() + ";REFERENCE  " + strreference + ";ROLL:ENABLE " + strrollenable + delayscale);
                logger.AdapterLogString(0, "windowndelay is" + delayt + "scale is" + scale + "position is " + position + "windownsacle is" + windownsacle);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        override public bool TriggerCmd(byte trigenable, byte andsourcechannel, byte andsourceHL, double holdofftime, byte sourcechannel, double triglevel, byte trigmode,byte sweepmode)
        {
            string strtrigmode;
            string strsweepmode;
            string strandsource;
            string strtrigenable;
            string strandsourcehl;
            bool flag = false;
            try
            {
                switch (andsourceHL)
                {
                    case 0:
                        strandsourcehl = ",HIGH";
                        break;
                    case 1:
                        strandsourcehl = ",LOW";
                        break;
                    case 2:
                        strandsourcehl = ",DONTcare";
                        break;
                    default:
                        strandsourcehl = ",HIGH";
                        break;
                }
                switch (trigenable)
                {
                    case 0:
                        strtrigenable = "OFF";
                        strandsource = "";
                        break;
                    case 1:
                        strtrigenable = "ON";
                        strandsource = ";:TRIGger:AND:SOURce " + "CHANNEL" + andsourcechannel + strandsourcehl;
                        break;
                    default:
                        strtrigenable = "OFF";
                        strandsource = "";
                        break;
                }
                switch (trigmode)
                {
                    case 0:
                        strtrigmode = "EDGE";
                        break;
                    case 1:
                        strtrigmode = "ADVanced";
                        break;
                    case 2:
                        strtrigmode = "PWIDth";
                        break;
                    case 3:
                        strtrigmode = "WINDow";
                        break;
                    case 4:
                        strtrigmode = "TRANsition";
                        break;
                    case 5:
                        strtrigmode = "SHOLd";
                        break;
                    case 6:
                        strtrigmode = "RUNT";
                        break;
                    case 7:
                        strtrigmode = "COMM";
                        break;
                    case 8:
                        strtrigmode = "TV";
                        break;
                    case 9:
                        strtrigmode = "TIMeout";
                        break;
                    case 10:
                        strtrigmode = "DELay";
                        break;
                    case 11:
                        strtrigmode = "STATe";
                        break;
                    case 12:
                        strtrigmode = "PATTern";
                        break;
                    case 13:
                        strtrigmode = "GLITch";
                        break;
                    default:
                        strtrigmode = "EDGE";
                        break;
                }
                switch (sweepmode)
                {
                    case 0:
                        strsweepmode = ";SWEep AUTO";
                        break;
                    case 1:
                        strsweepmode = ";SWEep TRIGgered";
                        break;
                   
                    default:
                        strsweepmode = ";SWEep AUTO";
                        break;
                }
               flag= MyIO.WriteString(":TRIGger:AND:ENABle " + strtrigenable + strandsource + ";:TRIGger:HOLDoff:MODE FIXed" + ";:TRIGger:HOLDoff " + holdofftime.ToString() + ";:TRIGger:LEVel " + "CHANNEL" + sourcechannel + "," + triglevel.ToString() + ";:TRIGger:MODE " + strtrigmode+strsweepmode );
                logger.AdapterLogString(0, "holdofftime is" + holdofftime + "trigmode is" + strtrigmode + "sweepmode is " + strsweepmode );
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        override public bool TriggerEdge(byte coupling,byte slope,byte sourcechannel,bool AnalogNoiseReject)
        {
            string strcoupling;
            string strslope;
            string strAnalogNoiseReject;
            bool flag = false;
            try
            {
                switch (coupling)
                {
                    case 0:
                        strcoupling = "DC";
                        break;
                    case 1:
                        strcoupling = "HFReject";
                        break;
                    case 2:
                        strcoupling = "LFReject";
                        break;
                    case 3:
                        strcoupling = "AC";
                        break;
                    default:
                        strcoupling = "DC";
                        break;
                }
                switch (slope)
                {
                    case 0:
                        strslope = "EITHer";
                        break;
                    case 1:
                        strslope = "NEGative";
                        break;
                    case 2:
                        strslope = "POSitive";
                        break;
                    default:
                        strslope = "EITHer";
                        break;
                }
                if (AnalogNoiseReject)
                {
                    strAnalogNoiseReject = "NREJect";
                }
                else
                {
                    strAnalogNoiseReject = "NORMal";
                }

               flag= MyIO.WriteString(":TRIGger:EDGE:COUPling " + strcoupling + ";:TRIGger:EDGE:SLOPe " + strslope + ";:TRIGger:EDGE:SOURce CHANNEL" + sourcechannel + ";:TRIGger:HYSTeresis " + strAnalogNoiseReject );
                logger.AdapterLogString(0, "COUPling is" + strcoupling + "SLOPe is" + strslope + "SOURce CHANNEL is " + sourcechannel + "HYSTeresis is" + strAnalogNoiseReject);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        
        
        }
        override public bool TriggerTimeout(byte condition,byte sourcechannel,double time)
        {
            string strcondition;
            bool flag = false;
            try
            {
                switch (condition)
                {
                    case 0:
                        strcondition = "LOW";
                        break;
                    case 1:
                        strcondition = "HIGH";
                        break;
                    case 2:
                        strcondition = "UNCHanged";
                        break;
                    default:
                        strcondition = "UNCHanged";
                        break;
                }
                flag = MyIO.WriteString(":TRIGger:TIMeout:CONDition " + strcondition + ";:TRIGger:TIMeout:TIME " + time + ";:TRIGger:TIMeout:SOURce " + sourcechannel);
                logger.AdapterLogString(0, "CONDition is" + strcondition + "SOURce CHANNEL is " + sourcechannel + "TIMeout is" + time);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }


        }
         
       override public bool WriteCmd(string cmd)
        {

            // bool flag = false;
            try
            {

                MyIO.WriteString(cmd);
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        override public string  ReadCmd(string cmd)
        {

            // bool flag = false;
            string readdata="";
            try
            {

                MyIO.WriteString(cmd);
                readdata = MyIO.ReadString(1024);
                return readdata;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return readdata;
            }
        }

        override public string TriggerOccure()
        {

            string flag = "";
            try
            {

                flag = ReadCmd(":TER?");
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return flag;
            }
        }
        override public string MeasureTstop(double vstop,bool slope,byte crossing,byte channel)
        {
            //slope true +,false -
            //crossing the number of crossing to be reportede
            string strslope;
            string flag = "";
            try
            {
                if (slope)
                    strslope = "+";
                else
                    strslope = "-";
                flag = ReadCmd(":MEASURE:TVOLT? " + vstop + "," + strslope + crossing + "," + "CHANNEL"+channel);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return flag;
            }
        }
        override public bool MeasureClear()
        {

            bool flag = false;
            try
            {

                flag = MyIO.WriteString(":MEASURE:CLEAR");
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
   }
}
