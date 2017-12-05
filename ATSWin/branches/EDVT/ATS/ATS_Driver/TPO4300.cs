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
namespace ATS_Driver
{

    public class TPO4300 : Thermocontroller
    {
        public Algorithm algorithm = new Algorithm();
        public TPO4300(logManager logmanager)
        {
            logger = logmanager;
        }
        public override bool Connect()
        {
            try
            {

                switch (IOType)
                {
                    case "GPIB":

                        MyIO = new IOPort(IOType, "GPIB::" + Addr);
                        EquipmentConnectflag = true; 
                        break;
                    default:
                        break;
                }
                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool ChangeChannel(string channel)
        {
            return true;
        }
        public override bool configoffset(string channel, string offset)
        {
            return true;
        }
        public override bool Initialize(TestModeEquipmentParameters[] TPO4300Struct)
        {
            try
            {
            int i = 0;
            if (algorithm.FindFileName(TPO4300Struct,"ADDR",out i))
            {
                Addr = TPO4300Struct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(TPO4300Struct,"IOTYPE",out i))
            {
                IOType = TPO4300Struct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(TPO4300Struct,"RESET",out i))
            {
                Reset = Convert.ToBoolean(TPO4300Struct[i].DefaultValue);
            }
            else
                return false;
            if (algorithm.FindFileName(TPO4300Struct,"FLSE",out i))
            {
                FLSE = TPO4300Struct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(TPO4300Struct,"ULIM",out i))
            {
                ULIM = TPO4300Struct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(TPO4300Struct,"LLIM",out i))
            {
                LLIM = TPO4300Struct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(TPO4300Struct,"SENSOR",out i))
            {
                Sensor = TPO4300Struct[i].DefaultValue;
            }
            else
                return false;
            if (algorithm.FindFileName(TPO4300Struct,"NAME",out i))
            {
                Name = TPO4300Struct[i].DefaultValue;
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

            if (EquipmentConfigflag)//曾经设定过
            {

                return true;

            }
            else
            {
                if (Reset == true)
                {
                    ReSet();
                }
                MyIO.WriteString("LGIN X-Stream");
                Thread.Sleep(500);
                MyIO.WriteString("DTYP 1");   
                MyIO.WriteString("DUTC 70");
                AirFlowSetting();
                AirTempsUpperlimit();
                AirTempslowerlimit();
                SensorType();
                MyIO.WriteString("WNDW 0.5");
                MyIO.WriteString("TRKL 1");
                DUTControlModeOnOFF(1);
                EquipmentConfigflag = true;
            }
            return true;
        }

        public override bool DUTControlModeOnOFF(byte Switch)//1 ON,0 OFF
        {
           
            try
            {

                return MyIO.WriteString("DUTM " + Switch.ToString());
               
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override string ReadCurrentTemp()
        {
            string Str_Temp = "0";
            try
            {
                MyIO.WriteString("TEMP?");
                Str_Temp = MyIO.ReadString(32);

            }
            catch
            {
                Str_Temp = "10000";
            }
            return Str_Temp;
        }

        public override bool AirFlowSetting()
        {
            bool flag = false;
            try
            {
                flag= MyIO.WriteString("FLSE " + FLSE);
                logger.AdapterLogString(0, "AirFlowSet is" + FLSE);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool AirTempsUpperlimit()
        {
            bool flag = false;
            try
            {
                flag= MyIO.WriteString("ULIM " + ULIM);
                logger.AdapterLogString(0, "TempsUpperlimit is" + ULIM);
              
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool AirTempslowerlimit()
        {
            bool flag = false;
            try
            {
                flag= MyIO.WriteString("LLIM " + LLIM);
                logger.AdapterLogString(0, "Tempslowerlimit is" + LLIM);
                return flag;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool SensorType()//0 No Sensor,1 T,2 k,3 rtd,4 diode 
        {
            try
            {

              return   MyIO.WriteString("DUTM " + Sensor);
               
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override bool lockHeadPosition(byte Switch)//1 UP,0 DOWN
        {
            
            try
            {
              return  MyIO.WriteString("HDLK " + Switch.ToString());
                
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }

        public override string ReadSetpoint()
        {
            string Str_Read = "";
            try
            {
                MyIO.WriteString("SETN?");
                Str_Read = MyIO.ReadString();
                return Str_Read;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return Str_Read;
            }
        }

        public override string ReadSetpointTemp()
        {
            string Str_Read = "";
            try
            {
                MyIO.WriteString("SETP?");
                Str_Read = MyIO.ReadString();
                return Str_Read;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return Str_Read;
            }
        }

        public override bool SetPositionUPDown(string position)//0 UP,1 DOWN
        {
            try
            {

               return  MyIO.WriteString("HEAD " + position);
                   
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }

        }

        public override bool SetPoint(string Point)//0 HOT,1 AMB,2 code
        {
            try
            {

                MyIO.WriteString("SETN " + Point);
                MyIO.WriteString("SOAK 15");
                logger.AdapterLogString(0, "point is " + Point);
               
                return true;
            }
            catch (Exception error)
            {
                logger.AdapterLogString(3, error.ToString());
                return false;
            }
        }
        public override bool SetPointTemp(double Temp)
        {
            bool flag = false;
            try
            {
                string Str = string.Format("{0:N1}", Temp);
               flag= MyIO.WriteString("Setp " + Str);
               logger.AdapterLogString(0, "Temp is " + Temp);
             
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
