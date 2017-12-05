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
    public class C86122 : Spectrograph
    {


       private Double WavelengthMax;
       private Double WavelengthMin;

       private static object syncRoot = new object();//used for thread synchronization


       public override bool Initialize(TestModeEquipmentParameters[] WLStruct)
       {
           try
           {

               int i = 0;
               if (Algorithm.FindFileName(WLStruct, "ADDR", out i))
               {
                   Addr = Convert.ToByte(WLStruct[i].DefaultValue);
               }
               else
               {
                   Log.SaveLogToTxt("there is no ADDR");
                   return false;
               }

               if (Algorithm.FindFileName(WLStruct, "IOTYPE", out i))
               {
                   IOType = WLStruct[i].DefaultValue;
               }
               else
               {
                   Log.SaveLogToTxt("there is no IOTYPE");
                   return false;
               }



               if (Algorithm.FindFileName(WLStruct, "WavelengthMax", out i))
               {
                   WavelengthMax = Convert.ToDouble(WLStruct[i].DefaultValue);
               }
               else
               {
                   Log.SaveLogToTxt("there is no DUTVOLTAGE");
                   return false;
               }

               if (Algorithm.FindFileName(WLStruct, "WavelengthMin", out i))
               {
                   WavelengthMin = Convert.ToDouble(WLStruct[i].DefaultValue);
               }
               else
               {
                   Log.SaveLogToTxt("there is no DUTVOLTAGE");
                   return false;
               }
         
        
              
             
               if (!Connect()) return false;
           }
           catch (InnoExCeption error)
           {
               Log.SaveLogToTxt("ErrorCode=" + error.ID + "Reason=" + error.TargetSite.Name + "Fail");
               throw error;
           }

           catch (Exception error)
           {

               Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
               throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
               // throw new InnoExCeption(ex);
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

                       lock (syncRoot)
                       {
                           this.WriteString("*IDN?");
                           string content = this.ReadString();
                           this.EquipmentConnectflag = content.Contains("86122");
                       }

                       break;
                   default:
                       Log.SaveLogToTxt("GPIB类型错误");
                       break;
               }
               return EquipmentConnectflag;
           }
           catch (Exception error)
           {

               Log.SaveLogToTxt(error.ToString());
               return false;
           }
       }

       public override bool Configure(int syn = 0)
       {
           lock (syncRoot)
           {
               try
               {
                   this.WriteString("*CLS");
                   this.WriteString("*RST");
                   SetWavelength("left", WavelengthMin);
                   SetWavelength("right", WavelengthMax);
                   // this.WriteString();
                   OpenContinuMode(false);

               }
               catch (InnoExCeption error)
               {
                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw error;
               }

               catch (Exception error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                   // throw new InnoExCeption(ex);
               }

               return true;
           }
       }

       private bool OpenContinuMode(bool flag)
       {
           lock (syncRoot)
           {
               try
               {
                   for (int i = 0; i < 3; i++)
                   {
                       this.WriteString(":INITiate:CONTinuous " + Convert.ToByte(flag));
                       System.Threading.Thread.Sleep(500);
                       this.WriteString("*opc?");
                       double state = Convert.ToByte(this.ReadString());
                       if (state == 1)
                       {
                           return true;
                       }
                   }

               }
               catch (InnoExCeption error)
               {
                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw error;
               }

               catch (Exception error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                   // throw new InnoExCeption(ex);
               }
               return false;
           }
       }

       private bool SetWavelength(string direction, double Wavelength)
       //direction -> "Left" OR "Right"
       {

           lock (syncRoot)
           {
               try
               {


                   string Str = ":DISPlay:WINDow2:TRACe:" + direction;

                   for (int i = 0; i < 2; i++)
                   {
                       this.WriteString(Str + " " + Wavelength + "nm");
                       this.WriteString(Str + "?");
                       string StrRea = this.ReadString();
                       if (Math.Abs(Wavelength - Convert.ToDouble(StrRea)) < 5e-9)
                       {
                           return true;
                       }
                   }
               }
               catch (InnoExCeption error)
               {
                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw error;
               }

               catch (Exception error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                   // throw new InnoExCeption(ex);
               }
               return false;
           }
       }

       public Double GetWavelength()
       {
           lock (syncRoot)
           {
               try
               {


                   return GetCenterWavelength();
               }
               catch (InnoExCeption error)
               {
                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw error;
               }

               catch (Exception error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                   // throw new InnoExCeption(ex);
               }
           }
       }

       public override double GetCenterWavelength()

       {
           lock (syncRoot)
           {
               try
               {
                   OpenContinuMode(true);
                   StartSweep();

                   this.WriteString(":fetch:power:wavelength?");
                   string Str = this.ReadString();

                   OpenContinuMode(false);
                   return Math.Round(Convert.ToDouble(Str) * 1e9, 2);
               }
               catch (InnoExCeption error)
               {
                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw error;
               }

               catch (Exception error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                   // throw new InnoExCeption(ex);
               }
           }
       }

       public override bool StartSweep()
       {
           lock (syncRoot)
           {
               try
               {


                   //:DISPlay:WINDow2:TRACe:AUTOmeasure
                   this.WriteString(":DISPlay:WINDow2:TRACe:AUTOmeasure");
                   return true;
               }
               catch (InnoExCeption error)
               {
                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw error;
               }

               catch (Exception error)
               {

                   Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                   throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                   // throw new InnoExCeption(ex);
               }
           }
       }
    }
}
