using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Ivi.Visa.Interop;
using System.Collections;
using System.Reflection;
using ATS_Framework;
using System.Windows.Forms;

namespace ATS_Driver
{
    public class C86120 : Spectrograph
    {
       private Algorithm algorithm = new Algorithm();

       private Double WavelengthMax;
       private Double WavelengthMin;

       public C86120(logManager logmanager)
       {
            logger = logmanager;
       }


       public override bool Initialize(TestModeEquipmentParameters[] WLStruct)
       {
           try
           {

               int i = 0;
               if (algorithm.FindFileName(WLStruct, "ADDR", out i))
               {
                   Addr = Convert.ToByte(WLStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no ADDR");
                   return false;
               }

               if (algorithm.FindFileName(WLStruct, "IOTYPE", out i))
               {
                   IOType = WLStruct[i].DefaultValue;
               }
               else
               {
                   logger.AdapterLogString(4, "there is no IOTYPE");
                   return false;
               }



               if (algorithm.FindFileName(WLStruct, "WavelengthMax", out i))
               {
                   WavelengthMax = Convert.ToDouble(WLStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DUTVOLTAGE");
                   return false;
               }

               if (algorithm.FindFileName(WLStruct, "WavelengthMin", out i))
               {
                   WavelengthMin = Convert.ToDouble(WLStruct[i].DefaultValue);
               }
               else
               {
                   logger.AdapterLogString(4, "there is no DUTVOLTAGE");
                   return false;
               }
         
        
              
             
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

                       MyIO = new IOPort(IOType, "GPIB::" + Addr, logger);
                       MyIO.IOConnect();
                       EquipmentConnectflag = true;
                       MyIO.WriteString("*IDN?");
                       EquipmentConnectflag = MyIO.ReadString().Contains("86120");
                       break;
                   default:
                       logger.AdapterLogString(4, "GPIB类型错误");
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

       public override bool Configure(int syn = 0)
       {
           try
           { 
               MyIO.WriteString("*CLS");
               MyIO.WriteString("*RST");
               //SetWavelength("left", WavelengthMin);
               //SetWavelength("right", WavelengthMax);
              // MyIO.WriteString();
               OpenContinuMode(false);

           }
           catch (System.Exception ex)
           {
               return false;
           }
          
           return true;
           
       }

       private bool OpenContinuMode(bool flag)
       {
           try
           {
               for (int i = 0; i < 3;i++ )
               {
                   MyIO.WriteString(":INITiate:CONTinuous " + Convert.ToByte(flag));
                   System.Threading.Thread.Sleep(500);
                   MyIO.WriteString("*opc?");
                   double state = Convert.ToByte(MyIO.ReadString());
                   if (state==1)
                   {
                       return true;
                   }
               }
               
           }
           catch
           {
               return false;
           }
           return false;
       }

       private bool SetWavelength(string direction, double Wavelength)
       //direction -> "Left" OR "Right"
       {

          

           string Str=":DISPlay:WINDow2:TRACe:" + direction;

           for (int i = 0; i < 2;i++ )
           {
               MyIO.WriteString(Str + " " + Wavelength + "nm");
                MyIO.WriteString(Str + "?");
                string StrRea = MyIO.ReadString();
                if (Math.Abs(Wavelength - Convert.ToDouble(StrRea))<5e-9)
                {
                    return true;
                }
           }
           return false;
       }

       public Double GetWavelength()
       {
           return GetCenterWavelength();
       }

       public override double GetCenterWavelength()

       {
           try
           {
               OpenContinuMode(true);
               //StartSweep();

               MyIO.WriteString(":fetch:power:wavelength?");
               string Str = MyIO.ReadString();

               OpenContinuMode(false);
               return Math.Round(Convert.ToDouble(Str) * 1e9, 3);
           }
           catch
           {
               return 0;
           }
          
       }

       public override bool StartSweep()
       {
         //:DISPlay:WINDow2:TRACe:AUTOmeasure
         MyIO.WriteString(":DISPlay:WINDow2:TRACe:AUTOmeasure");
         return true;
       }
    }
}
