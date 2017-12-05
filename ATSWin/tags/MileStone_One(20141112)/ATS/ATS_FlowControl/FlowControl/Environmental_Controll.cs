using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;
using ATS_Driver;
using System.Threading;

using System.Windows.Forms;
namespace ATS
{
    public class EnvironmentalControll:TestModelBase
    {

        private double LastTemp=100;
        private Powersupply PS;
        private bool NeedxStreamflag = true;
        private Thermocontroller Xstream;

        private DUT pDut;
        private EquipmentList pEquipmentList;
        private String PowersupplyName;
        private String XstreamName;
        CommonMethod pCommonMethod = new CommonMethod();
        public EnvironmentalControll(DUT dut)
        {
            pDut = dut;
           
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
                }

                if (EqupmentName.Contains("THERMOCONTROLLER"))
                {
                    XstreamName = EqupmentName;
                }
            }
            return true;
        }
      
        public bool ConfigEnvironmental(double Temp, double Voltage, byte Channel,String StrAuxAttribles)
        {
            int i = 0;
            for ( i = 0; i < pEquipmentList.Count; i++)
            {
                pEquipmentList.Values[i].ChangeChannel(Channel.ToString());
              
            }

            //-----Config Voltage
            if (PowersupplyName != null && PowersupplyName!="")
            {
            
                PS = new Powersupply();
                PS = (Powersupply)pEquipmentList[PowersupplyName];
                PS.ConfigVoltageCurrent(Voltage.ToString());
                PS.Switch(false);
                Thread.Sleep(1000);
                PS.Switch(true);
                Thread.Sleep(2000);
            }
            else
            {
                MessageBox.Show("TopoControl数据库仪器列表中没有电源切换电压~~");
            }
            //-----Config Temp
            if (XstreamName != null && XstreamName != "")
            {
                Xstream = new Thermocontroller();
                Xstream = (Thermocontroller)pEquipmentList[XstreamName];

                Xstream.SetPositionUPDown("1");
                if (Temp < 20)// Low
                {
                    Xstream.SetPoint("2");

                }
                else if (Temp > 30)//Hig
                {
                    Xstream.SetPoint("0");
                }
                else//AMB
                {
                    Xstream.SetPoint("1");
                }

                Xstream.SetPointTemp(Temp);

                double CurrentTemp = Convert.ToDouble(Xstream.ReadCurrentTemp());

                i = 0;

                while (Math.Abs(CurrentTemp - Temp) > 0.5)
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
                if (Temp != LastTemp)
                {

                   
                    string SleepTime=  pCommonMethod.AnalysisString("Temp", StrAuxAttribles);

                    if (SleepTime != null && Channel==1)
                    {
                        Thread.Sleep(Convert.ToInt16(SleepTime)*1000);
                    }
                  
                }
                LastTemp = Temp;
            }
            else
            {
                if (NeedxStreamflag)
                {
                    if (DialogResult.Yes == MessageBox.Show("是否需要热流仪?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        NeedxStreamflag = true;
                        MessageBox.Show("TopoControl数据库仪器列表中没有热流仪切换温度,请重新选择TestPlan~~");
              
                    }
                    else
                    {
                        NeedxStreamflag = false ;
                    }
                }
               
            }

            pDut.MoudleChannel = Channel;// 告诉Dut 当前为通道几;
          
            //pDut.tempselect =0 ;

            //pDut.vccselect = 0;

            Thread.Sleep(2000);
          

           
            return true;
        }

        public bool Dispose()
        {
            if (PS!=null) PS.Switch(false);
            if (Xstream!=null) Xstream.SetPositionUPDown("0");
            return true;
        }
    }
   


}
