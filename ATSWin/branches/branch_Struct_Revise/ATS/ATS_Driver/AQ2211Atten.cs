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


    public class AQ2211Atten : Attennuator
    {
        private double CurrentOffset;
        private static object syncRoot = SyncRoot_AQ2211.Get_SyncRoot_AQ2211();//used for thread synchronization
      //  private string StrAttSlot;
        private string[] SlotList;
        //private Double LastAttValue=-5;
        //private Double LastAttChannel = -2;


        public override bool Initialize(TestModeEquipmentParameters[] AQ2211list)
        {
            try
            {
                int i = 0;
                if (Algorithm.FindFileName(AQ2211list,"ADDR",out i))
                {
                    Addr = Convert.ToByte(AQ2211list[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no ADDR");
                    return false;
                }
                    
                if (Algorithm.FindFileName(AQ2211list,"IOTYPE",out i))
                {
                    IOType = AQ2211list[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no IOTYPE");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list,"RESET",out i))
                {
                    Reset = Convert.ToBoolean(AQ2211list[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no RESET");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list,"ATTSLOT",out i))
                {
                  
                    SlotList = AQ2211list[i].DefaultValue.Split(',');
                }
                else
                {
                    Log.SaveLogToTxt("there is no ATTSLOT");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list,"ATTCHANNEL",out i))
                {
                    AttChannel = AQ2211list[i].DefaultValue;

                    AttChannelArray = AttChannel.Split(',');
                    
                }
                else
                {
                    Log.SaveLogToTxt("there is no ATTCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list,"ATTVALUE",out i))
                {
                    AttValue = AQ2211list[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no ATTVALUE");
                    return false;
                }
               
                if (Algorithm.FindFileName(AQ2211list,"TOTALCHANNEL",out i))
                {
                    TotalChannel = AQ2211list[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no TOTALCHANNEL");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list,"WAVELENGTH",out i))
                {
                    Wavelength = AQ2211list[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no WAVELENGTH");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list,"NAME",out i))
                {
                    Name = AQ2211list[i].DefaultValue;
                }
                else
                {
                    Log.SaveLogToTxt("there is no NAME");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list, "OPENDELAY", out i))
                {
                    opendelay = Convert.ToInt32(AQ2211list[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no OPENDELAY");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list, "CLOSEDELAY", out i))
                {
                    closedelay = Convert.ToInt32(AQ2211list[i].DefaultValue);
                }
                else
                {
                    Log.SaveLogToTxt("there is no CLOSEDELAY");
                    return false;
                }
                if (Algorithm.FindFileName(AQ2211list, "SETATTDELAY", out i))
                {
                    setattdelay = Convert.ToInt32(AQ2211list[i].DefaultValue);
                }
                //else
                //{
                //    Log.SaveLogToTxt("there is no SETATTDELAY");
                //    return false;
                //}
                if (!Connect()) return false;
            }
            catch (InnoExCeption error)
            {
                Log.SaveLogToTxt("ErrorCode="+  ExceptionDictionary.Code._Funtion_Fatal_0x05002 +"Reason=" +error.TargetSite.Name + "Fail");
                  throw error;
            }

            catch (Exception error)
            {

                Log.SaveLogToTxt("ErrorCode="+  ExceptionDictionary.Code._Funtion_Fatal_0x05002 +"Reason=" +error.TargetSite.Name + "Fail");
                throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                // throw new InnoExCeption(ex);
            }
            return true;
        }
       
        public override  bool Connect()
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
                            this.EquipmentConnectflag = content.Contains("AQ22");
                        }
                        break;
                    default:
                        Log.SaveLogToTxt("GPIB类型错误");
                        EquipmentConnectflag = false;

                        break;
                }
                if (!EquipmentConnectflag)
                {
                    Log.SaveLogToTxt(  ExceptionDictionary.Code._UnConnect_0x05000 + "无法连接仪器");
                    EquipmentConnectflag = false;
                    throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);
                }
                return EquipmentConnectflag;
            }
            catch (Exception error)
            {
                Log.SaveLogToTxt("ErrorCode="+  ExceptionDictionary.Code._Funtion_Fatal_0x05002 +"Reason=" +error.TargetSite.Name + "Fail");
                EquipmentConnectflag = false;
                throw new InnoExCeption(ExceptionDictionary.Code._UnConnect_0x05000);

              //  return false;
            }
          
        }

        public bool ReSet()
        {
            lock (syncRoot)
            {
                if (this.WriteString("*RST"))
                {
                    Thread.Sleep(3000);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool Configure(int syn = 0)
        {
            lock (syncRoot)
            {
                List<string> ch = new List<string>();

                try
                {
                    if (EquipmentConfigflag)
                    {
                        return true;
                    }
                    else
                    {
                        if (Reset == true)
                        {
                            ReSet();
                        }
                        for (int i = 0; i < SlotList.Length; i++)
                        {
                            //if (!(ch.Contains(SlotList[i])))
                            //{
                            ch.Add(SlotList[i]);
                            AttSlot = SlotList[i].ToString();
                            if (AttChannelArray.Length == 1)
                            {
                                AttChannel = AttChannelArray[0];
                            }
                            else
                            {
                                AttChannel = AttChannelArray[i];
                            }

                            ConfigWavelength((i + 1).ToString(), syn);
                            AttnValue(AttValue, 0);
                            OutPutSwitch(true, syn);
                            // }
                        }
                        EquipmentConfigflag = true;
                    }
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

        public override bool ChangeChannel(string channel, int syn = 0)
        {
            lock (syncRoot)
            {
                string Wavelength = "";
                bool flag1, flag2;
                string offset = "";
                CurrentChannel = channel;
                try
                {
                    //AttChannel=AttChannelArray[]

                    if (AttChannelArray.Length == 1)
                    {
                        AttChannel = AttChannelArray[0];
                    }
                    else if (AttChannelArray.Length == Convert.ToInt16(TotalChannel))
                    {
                        AttChannel = AttChannelArray[Convert.ToByte(CurrentChannel) - 1];
                    }
                    else
                    {
                        return false;
                    }

                    AttSlot = SlotList[Convert.ToByte(CurrentChannel) - 1].ToString();
                    flag1 = ConfigWavelength(CurrentChannel);
                    if (offsetlist.ContainsKey(CurrentChannel))
                        offset = offsetlist[CurrentChannel];
                    flag2 = SetOffset("0");//将所有 Offset设置为了 0
                    CurrentOffset = Convert.ToDouble(offset);
                    AttnValue("-10", 0);
                    Log.SaveLogToTxt("ATT CurrentChannel is" + CurrentChannel + "ATT Wavelength is" + Wavelength + "ATT offset is" + offset);


                    if (flag1 && flag2)
                        return true;
                    else
                        return false;
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

        public override bool configoffset(string channel, string offset, int syn = 0)
        {
            lock (syncRoot)
            {
                try
                {
                    offsetlist.Add(channel, offset);
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

        protected bool SetOffset(string offset,int syn=0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    //offset = Math.Abs((Convert.ToDouble(AttValue) + Convert.ToDouble(offset))).ToString();
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("AttSlot is" + AttSlot + "offset is" + offset);
                        return this.WriteString(":INP" + AttSlot + ":OFFS " + offset);
                    }
                    else
                    {
                        double temp = (Convert.ToDouble(offset));
                        temp = temp * -1;
                        offset = Convert.ToString(temp);
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":INP" + AttSlot + ":OFFS " + offset);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INP" + AttSlot + ":OFFS?");
                                readtemp = this.ReadString();
                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(offset))
                                    break;
                            }
                            if (k <= 3)
                            {
                                flag = true;
                                Log.SaveLogToTxt("AttSlot is" + AttSlot + "offset is" + offset);
                            }
                            else
                            {
                                Log.SaveLogToTxt("ATT set offset wrong");
                            }

                        }


                        return flag;
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

            }
        }

        public override bool SetAllChannnel_RxOverLoad(float RxOverLoad)
        {
            lock (syncRoot)
            {
                try
                {


                    for (int i = 0; i < Convert.ToInt32(TotalChannel); i++)
                    {
                        ChangeChannel((i + 1).ToString(), 0);
                        //CurrentOffset = 0;
                        //AttSlot = SlotList[i].ToString();
                        AttnValue(RxOverLoad.ToString(), 0);
                    }
                    ChangeChannel(CurrentChannel, 0);
                    AttnValue(RxOverLoad.ToString(), 0);
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

        protected bool ConfigWavelength(string dutcurrentchannel, int syn = 0)
        {
            lock (syncRoot)
            {
                //string CurrnentWavelength = "";
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                double waveinput;
                double waveoutput;
                string[] wavtemp = new string[4];
                Wavelength = Wavelength.Trim();
                wavtemp = Wavelength.Split(new char[] { ',' });
                byte i = Convert.ToByte(Convert.ToInt16(dutcurrentchannel) - 1);
                try
                {
                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("AttSlot is" + AttSlot + ":Channel" + AttChannel + "Wavelength is" + wavtemp[i] + "nm");
                        return this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":WAV " + wavtemp[i] + "nm");
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            flag1 = this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":WAV " + wavtemp[i] + "nm");
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":WAV?");
                                readtemp = this.ReadString();
                                waveinput = Convert.ToDouble(wavtemp[i]);
                                waveoutput = Convert.ToDouble(readtemp) * Math.Pow(10, 9);
                                if (waveinput == waveoutput)
                                    break;
                            }
                            if (k <= 3)
                            {
                                flag = true;
                                Log.SaveLogToTxt("AttSlot is" + AttSlot + ":Channel" + AttChannel + "Wavelength is" + wavtemp[i] + "nm");
                            }
                            else
                            {
                                Log.SaveLogToTxt("ATT set Wavelength wrong");

                            }

                        }

                        return flag;
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
            }
        
        }

        public override bool ConfigWavelength(int syn=0)
        {
            lock (syncRoot)
            {
                bool flag = false;
                try
                {
                    flag = ConfigWavelength(AttChannel, syn);
                    return flag;
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

        //private void CalculateSleepTime(double Att)
        //{
        //    if (LastAttChannel.ToString()==CurrentChannel)
        //    {

        //        PowerVariation = Math.Abs(Att - LastAttValue);
        //        SleepTime = Convert.ToInt16(PowerVariation * 200);
        //        if (SleepTime > 10000) SleepTime = 4000;//防止出现错误的入射光，导致衰减器等待太久
        //    }
        //    else
        //    {
        //         SleepTime = 4000;//防止出现错误的入射光，导致衰减器等待太久
        //    }
        //}
#region  Old

        //public override bool AttnValue(string InputPower, int syn = 1)
        //{
        //    bool flag = false;
        //    bool flag1 = false;
        //    int k = 0;
        //    string readtemp = "";
        //    try
        //    {
        //        AttValue = (CurrentOffset - Convert.ToDouble(InputPower)).ToString("F2");// 仪器中的Offset将不在添加.在计算中使用.
                
        //        if (Convert.ToDouble(AttValue) < 0)
        //         {
        //             AttValue = "0";
        //            Log.SaveLogToTxt("Light Source Power Too Smal");
        //         }
        //        //CalculateSleepTime(Convert.ToDouble(AttValue));

        //        try
        //        {
        //            string Str="";

        //            int i=0;
        //            do 
        //            {
        //                  this.WriteString(":INP" + AttSlot + ":ATT?");
        //                  Str=this.ReadString();
        //                  Thread.Sleep(200);
        //                 i++;
        //            } while (Str==null&&i<3);

        //            this.WriteString(":INP" + AttSlot + ":ATT?");
                  
        //            double TempVale = double.Parse(Str);
        //            SleepTime = Convert.ToInt16(Math.Abs(double.Parse(AttValue) - TempVale) * 120);
        //        }
        //        catch
        //        {
        //            SleepTime = 2000;
        //        }
                
        //        if (syn == 0)
        //        {
        //            Log.SaveLogToTxt("AttSlot is" + AttSlot + "ATT VALUE IS" + AttValue);
        //            flag = this.WriteString(":INP" + AttSlot + ":ATT " + AttValue);
        //           // Thread.Sleep(SleepTime);
        //            //Thread.Sleep(setattdelay);
        //        }
        //        else
        //        {
        //            for (int i = 0; i < 3; i++)
        //            {
        //                flag1 = this.WriteString(":INP" + AttSlot + ":ATT " + AttValue);
        //                Thread.Sleep(SleepTime);
        //               // Thread.Sleep(setattdelay);
        //                if (flag1 == true)
        //                    break;
        //            }
        //            if (flag1 == true)
        //            {
        //                for (k = 0; k < 3; k++)
        //                {
                           
        //                    this.WriteString(":INP" + AttSlot + ":ATT?");
        //                    Thread.Sleep(100);
        //                    readtemp = this.ReadString();
                           
        //                    if (Convert.ToDouble(readtemp) == Convert.ToDouble(AttValue))
        //                        flag = true;
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                flag = true;
        //            }

        //        }

        //        if (flag)
        //        {

        //            Log.SaveLogToTxt("AttSlot is" + AttSlot + "ATT VALUE IS" + AttValue);
        //        }
        //        else
        //        {
        //            Log.SaveLogToTxt("set ATT VALUE wrong");
        //        }
              
        //        return flag;
        //    }
        //    catch (Exception error)
        //    {

        //        Log.SaveLogToTxt(error.ToString());
        //        return false;
        //    }
        //}
#endregion
        public override bool AttnValue(string InputPower, int syn = 1)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    AttValue = (CurrentOffset - Convert.ToDouble(InputPower)).ToString("F2");// 仪器中的Offset将不在添加.在计算中使用.

                    if (Convert.ToDouble(AttValue) < 0)
                    {
                        AttValue = "0";
                        Log.SaveLogToTxt("Light Source Power Too Smal");
                    }
                    //CalculateSleepTime(Convert.ToDouble(AttValue));

                    try
                    {
                        string Str = "";

                        int i = 0;
                        do
                        {
                            this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT?");
                            Str = this.ReadString();
                            Thread.Sleep(200);
                            i++;
                        } while (Str == null && i < 3);

                        this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT?");

                        double TempVale = double.Parse(Str);
                        SleepTime = Convert.ToInt16(Math.Abs(double.Parse(AttValue) - TempVale) * 120);
                    }
                    catch
                    {
                        SleepTime = 2000;
                    }

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("AttSlot is" + AttSlot + "ATT VALUE IS" + AttValue);
                        flag = this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT " + AttValue);
                        // Thread.Sleep(SleepTime);
                        //Thread.Sleep(setattdelay);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            //:INP7:Channel1:Att 8
                            flag1 = this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT " + AttValue);
                            Thread.Sleep(SleepTime);
                            // Thread.Sleep(setattdelay);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT?");
                                Thread.Sleep(100);
                                readtemp = this.ReadString();

                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(AttValue))
                                    flag = true;
                                break;
                            }
                        }
                        else
                        {
                            flag = true;
                        }

                    }

                    if (flag)
                    {

                        Log.SaveLogToTxt("AttSlot is" + AttSlot + "ATT VALUE IS" + AttValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("set ATT VALUE wrong");
                    }

                    return flag;
                }
                catch (InnoExCeption error)
                {
                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw error;
                }
            }
        }
        public override bool SetInputPow(double InputPower, int syn = 1)
        {
            lock (syncRoot)
            {
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                try
                {
                    AttValue = (CurrentOffset - InputPower).ToString("F2");// 仪器中的Offset将不在添加.在计算中使用.

                    if (Convert.ToDouble(AttValue) < 0)
                    {
                        AttValue = "0";
                        Log.SaveLogToTxt("Light Source Power Too Smal");
                    }
                    //CalculateSleepTime(Convert.ToDouble(AttValue));

                    try
                    {
                        string Str = "";

                        int i = 0;
                        do
                        {
                            this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT?");
                            Str = this.ReadString();
                            Thread.Sleep(200);
                            i++;
                        } while (Str == null && i < 3);

                        this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT?");

                        double TempVale = double.Parse(Str);
                        SleepTime = Convert.ToInt16(Math.Abs(double.Parse(AttValue) - TempVale) * 120);
                    }
                    catch
                    {
                        SleepTime = 2000;
                    }

                    if (syn == 0)
                    {
                        Log.SaveLogToTxt("AttSlot is" + AttSlot + "ATT VALUE IS" + AttValue);
                        flag = this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT " + AttValue);
                        // Thread.Sleep(SleepTime);
                        //Thread.Sleep(setattdelay);
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            //:INP7:Channel1:Att 8
                            flag1 = this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT " + AttValue);
                            Thread.Sleep(SleepTime);
                            // Thread.Sleep(setattdelay);
                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT?");
                                Thread.Sleep(100);
                                readtemp = this.ReadString();

                                if (Convert.ToDouble(readtemp) == Convert.ToDouble(AttValue))
                                    flag = true;
                                break;
                            }
                        }
                        else
                        {
                            flag = true;
                        }

                    }

                    if (flag)
                    {

                        Log.SaveLogToTxt("AttSlot is" + AttSlot + "ATT VALUE IS" + AttValue);
                    }
                    else
                    {
                        Log.SaveLogToTxt("set ATT VALUE wrong");
                    }

                    return flag;
                }
                catch (Exception error)
                {

                    Log.SaveLogToTxt("ErrorCode=" + ExceptionDictionary.Code._Funtion_Fatal_0x05002 + "Reason=" + error.TargetSite.Name + "Fail");
                    throw new InnoExCeption(ExceptionDictionary.Code._Funtion_Fatal_0x05002, error.StackTrace);
                    // throw new InnoExCeption(ex);
                }
            }
        }

        public override bool OutPutSwitch(bool Swith, int syn = 0)
        {
            lock (syncRoot)
            {
                string index;
                bool flag = false;
                bool flag1 = false;
                int k = 0;
                string readtemp = "";
                string intswitch = "";
                int delay = 0;
                if (Swith)
                {
                    index = "ON";
                    intswitch = "1";
                    delay = opendelay;
                }
                else
                {
                    index = "OFF";
                    intswitch = "0";
                    delay = closedelay;
                }
                try
                {
                    if (syn == 0)
                    {//this.WriteString(":INP" + AttSlot+"Channel"+AttChannel + ":ATT " + AttValue);
                        //":OUTP" + AttSlot + ":STAT " + index  :OUTP7:STAT 0    :OUTP7:Channel1:STAT 0
                        Log.SaveLogToTxt("AttSlot is" + AttSlot + "state IS" + index);
                        flag = this.WriteString(":OUTP" + AttSlot + ":Channel" + AttChannel + ":STAT " + index);
                        Thread.Sleep(delay);
                        return flag;
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            flag1 = this.WriteString(":OUTP" + AttSlot + ":Channel" + AttChannel + ":STAT " + index);

                            if (flag1 == true)
                                break;
                        }
                        if (flag1 == true)
                        {
                            for (k = 0; k < 3; k++)
                            {

                                this.WriteString(":OUTP" + AttSlot + ":Channel" + AttChannel + ":STAT?");
                                readtemp = this.ReadString();
                                if (readtemp == intswitch)
                                    break;
                            }
                            if (k <= 3)
                            {
                                Log.SaveLogToTxt("AttSlot is" + AttSlot + "state IS" + index);
                                flag = true;
                            }
                            else
                            {
                                Log.SaveLogToTxt("ATT set switch wrong");

                            }

                        }
                        Thread.Sleep(delay);
                        return flag;
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
            }
        }

        public override bool AddCalFactor(string CalFactor)
        {
            lock (syncRoot)
            {
                bool flag = false;
                try
                {
                    flag = this.WriteString(":sens" + AttSlot + ":channel" + AttChannel + ":CORR:LOSS:INP:MAGN" + CalFactor);
                    Log.SaveLogToTxt("AttSlot is" + AttSlot + "Channel is" + AttChannel + "CalFactor is" + CalFactor);
                    return flag;
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

        public override double GetAtten()
        {
            lock (syncRoot)
            {
                double atten = 0;
                try
                {
                    this.WriteString(":INP" + AttSlot + ":Channel" + AttChannel + ":ATT?");
                    atten = Convert.ToDouble(this.ReadString(16));
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
                return atten;
            }
        }

    }


}
