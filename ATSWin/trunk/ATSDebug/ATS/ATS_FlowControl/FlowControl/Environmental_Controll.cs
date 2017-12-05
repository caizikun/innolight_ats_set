using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;
using System.Threading;

using System.Windows.Forms;
namespace ATS
{
    public class EnvironmentalControll
    {

       
        private Powersupply PS;
        public bool NeedxStreamflag = true;
        private Thermocontroller Xstream;
        private Attennuator pAtt;

        private DUT pDut;
        public EquipmentList pEquipmentList;
        private String PowersupplyName;
        private String XstreamName;
        private String XAttName;
        public bool Flag_NeedAutoCheck_EvBVoltage = false;
        CommonMethod pCommonMethod = new CommonMethod();

        private double CurrentTemp = -99;
        public double LastTemp=-100;
        private double CurrentVoltage = -50;
        public double LastVoltage = -50;
        //private int CurrentTemp = -99;

        private logManager plogManager;
        public EnvironmentalControll(DUT dut,logManager alogManager)
        {
            pDut = dut;
            plogManager = alogManager;
           
        }

        public  bool SelectEquipment(EquipmentList aEquipList)
        {
           // string[] NameArray = EquipmentNameArray.Split(',');

            pEquipmentList = aEquipList;

            foreach(string EqupmentName in aEquipList.Keys)
            {
                if (EqupmentName.Contains("POWERSUPPLY"))
                {
                    PowersupplyName = EqupmentName;
                    PS = (Powersupply)pEquipmentList[PowersupplyName];
                }

                if (EqupmentName.Contains("THERMOCONTROLLER"))
                {
                    XstreamName = EqupmentName;
                    Xstream = (Thermocontroller)pEquipmentList[XstreamName];
                }
                if (EqupmentName.Contains("ATTENNUATOR"))
                {
                    XAttName = EqupmentName;
                    pAtt = (Attennuator)pEquipmentList[XAttName];
                }

            }
            return true;
        }
      
        public bool ConfigEnvironmental(double Temp, double Voltage, byte Channel,Double TempOffset,int TempWaitTime,float RxOverload)
        {
            CurrentTemp = Temp;
            CurrentVoltage = Voltage;
            int i = 0;

           if (!ConfigTemp(Temp, TempWaitTime,TempOffset)) return false;

           if (pAtt!=null) pAtt.SetAllChannnel_RxOverLoad(RxOverload);

            for ( i = 0; i < pEquipmentList.Count; i++)
            {
                pEquipmentList.Values[i].ChangeChannel(Channel.ToString());
              
            }
            plogManager.AdapterLogString(0, "LastTemp=" + LastTemp + "LastVoltage=" + LastVoltage);
            plogManager.AdapterLogString(0, "CurrentTemp=" + Temp + "CurrentVoltage=" + Voltage);
           
            if (!ConfigInputVcc(Voltage)) return false;

            pDut.MoudleChannel = Channel;// 告诉Dut 当前为通道几;
           // LastTemp = Temp;
            LastVoltage = CurrentVoltage;
            return true;
        }

        private bool ConfigTemp(double Temp, int SleepTime,double TenpOffset)
        {
            int i;
            int TempProcess;
            double RealTemp = Temp + TenpOffset;

            if (XstreamName != null && XstreamName != "")
            {
                if (Temp != LastTemp)
                {
                    Xstream.SetPointTemp(RealTemp);

                    double CurrentTemp = Convert.ToDouble(Xstream.ReadCurrentTemp());

                    i = 0;

                    while (Math.Abs(CurrentTemp - RealTemp) > 0.5)
                    {
                        Thread.Sleep(2000);
                        CurrentTemp = Convert.ToDouble(Xstream.ReadCurrentTemp());
                        i++;
                        if (i > 100)
                        {
                            MessageBox.Show("无法调整到当前温度");
                            return false;
                        }
                    }
                }

            }
            else
            {
                if (NeedxStreamflag)
                {
                    if (DialogResult.Yes == MessageBox.Show("是否需要热流仪?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        NeedxStreamflag = true;
                        MessageBox.Show("TopoControl数据库仪器列表中没有热流仪切换温度,请重新选择TestPlan~~");
                        return false;
                    }
                    else
                    {
                        NeedxStreamflag = false;
                    }
                }
            }

            return true;
        }
        private bool ConfigInputVcc(double Voltage)
        {
            plogManager.AdapterLogString(0, "准备调试电压....");
            if (PS != null)
            {
                if (Flag_NeedAutoCheck_EvBVoltage)//程序通过采样自动调节电压
                {
                    if (CurrentTemp != LastTemp || LastVoltage != CurrentVoltage)
                    {
                        if (!MatchVoltage(Voltage))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    PS.ConfigVoltageCurrent(Voltage.ToString());
                }
            }
            else
            {
                MessageBox.Show("TopoControl数据库仪器列表中没有电源切换电压~~");
                return false;
            }
        
            return true;
        }

        private bool MatchVoltage(double Voltage)
        {
            double CurrentVoltage = Voltage;
           // double StartVoltage=Voltage;
            double TargateVoltage=Voltage;
            double Vadc = 0;
            double EvbVoltage=0 ;
            int AdjustTime = 0;
            PS.voltageoffset = "0";
            PS.ConfigVoltageCurrent(Voltage.ToString());
           // Thread.Sleep(100);
            EvbVoltage = pDut.ReadEvbVcc();
            if (EvbVoltage==0)
            {
                MessageBox.Show("Evb 采样电压为0,请检查EVB");
                plogManager.AdapterLogString(3, "Evb 采样电压为0");
                return false;
            }
           // EvbVoltage = 2 * (Vadc * 2.5 / 1023);

            while (Math.Abs(TargateVoltage - EvbVoltage) > 0.025)
            {
                if (Math.Abs(EvbVoltage - TargateVoltage) > 0.1)//差值超过0.1 那么Step=0.1
                {
                    if (TargateVoltage > EvbVoltage)
                    {
                        CurrentVoltage += 0.1;
                    }
                    else
                    {
                        CurrentVoltage -= 0.1;
                    }
                }
                else//差值小于0.1 那么Step=0.01
                {
                    if (TargateVoltage > EvbVoltage)
                    {
                        CurrentVoltage += 0.01;
                    }
                    else
                    {
                        CurrentVoltage -= 0.01;
                    }

                }

                if (CurrentVoltage>3.8)
               {
                   MessageBox.Show("电源输出电源已经达到 3.8V,无法调整到我们所需要的输出电压");
                   plogManager.AdapterLogString(3, "电源输出电源已经达到 3.8V,无法调整到我们所需要的输出电压");

                   return false;
               }

               PS.ConfigVoltageCurrent(CurrentVoltage.ToString());

               Thread.Sleep(50);

               EvbVoltage = pDut.ReadEvbVcc();
              // EvbVoltage = 2 * (Vadc * 2.5 / 1023);

               plogManager.AdapterLogString(0, "InputVoltage=" + CurrentVoltage.ToString() +  " EvbVoltage=" + EvbVoltage);


               AdjustTime++;

               if (AdjustTime > 50)
               {
                   plogManager.AdapterLogString(3, "无法调整到我们所需要的输出电压");
                   break;
               }

            };

            PS.voltageoffset = (CurrentVoltage - Voltage).ToString();

            return true;
        }

        public bool Dispose()
        {
            if (PS != null) PS.OutPutSwitch(false);
            if (Xstream!=null) Xstream.SetPositionUPDown("0");
            return true;
        }
        
    }
   


}
