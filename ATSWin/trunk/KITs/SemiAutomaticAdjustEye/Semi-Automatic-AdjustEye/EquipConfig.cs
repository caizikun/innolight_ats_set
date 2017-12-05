using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ATS_Framework;
using ATS_Driver;
using System.Collections;
using ATS;

namespace Semi_Automatic_AdjustEye
{
    public partial class EquipConfig : Form
    {
        logManager MylogManager = new logManager();
        public ArrayList EquipmenList = new ArrayList();
        TestModeEquipmentParameters[] CurrentEquipmentStruct;

        public E3631 E3631Power;
        public DP811A DP811APower;
        public TPO4300 XSteam;
        public AQ2211OpticalSwitch OptSwitch;
        public FLEX86100 FlexScope;

        public bool FlagEquipmentConfigOK = false;
        public  ConfigXmlIO myXml;
        public EquipConfig(ConfigXmlIO pXml)
        {
            InitializeComponent();
            myXml = pXml;
        }

        private void btConfirmConfig_Click(object sender, EventArgs e)
        {
            EquipmentManage MyEquipmentManager = new EquipmentManage(MylogManager);
            if (chk_Power.Checked)
            {
                if (cbPower.SelectedItem.ToString() == "E3631")
                {

                    E3631Power.IOType = "GPIB";
                    E3631Power.Addr = Convert.ToByte(tbPowerGPIB.Text);
                    if (!E3631Power.Connect())
                    {
                        MessageBox.Show("E3631 connect error.");
                    }
                    else
                    {
                        E3631Power.EquipmentConnectflag = true;
                        EquipmenList.Add("E3631");
                        //MyEquipment.PowerE3631.OutPutSwitch(true);
                        //MyConfigXmlIO.DeleteSingleEquipmentNode("DP811A");
                    }
                }
                else if (cbPower.SelectedItem.ToString() == "DP811A")
                {

                    DP811APower.IOType = "GPIB";
                    DP811APower.Addr = Convert.ToByte(tbPowerGPIB.Text);
                    if (!DP811APower.Connect())
                    {
                        MessageBox.Show("DP811A connect error.");
                    }
                    else
                    {
                        DP811APower.EquipmentConnectflag = true;
                        EquipmenList.Add("DP811A");
                        //MyConfigXmlIO.DeleteSingleEquipmentNode("E3631");
                    }
                }
                
            }
            if (chk_TPO4300.Checked)
            {

                XSteam.IOType = "GPIB";
                XSteam.Addr = Convert.ToByte(tbTPOGPIB.Text);
                if (!XSteam.Connect())
                {
                    MessageBox.Show("Tpo4300 connect error.");
                }
                else
                {
                    XSteam.EquipmentConnectflag = true;
                    EquipmenList.Add("TPO4300");
                    //Tpo4300ParameterStruct = GetCurrentEquipmentStruct("TPO4300");
                }
            }
            if (chk_Scope.Checked)
            {

                FlexScope.IOType = "GPIB";
                FlexScope.Addr = Convert.ToByte(tbFLEX86100GPIB.Text);
                if (!FlexScope.Connect())
                {
                    MessageBox.Show("FLEX86100 connect error.");
                }
                else
                {
                    FlexScope.EquipmentConnectflag = true;
                    EquipmenList.Add("FLEX86100");
                }
            }
            if (chk_OpticalSwitch.Checked)
            {

                OptSwitch.IOType = "GPIB";
                OptSwitch.Addr = Convert.ToByte(tbAQSwitchGPIB.Text);
                if (!OptSwitch.Connect())
                {
                    MessageBox.Show("AQ2211OpticalSwitch connect error.");
                }
                else
                {
                    FlexScope.EquipmentConnectflag = true;
                    EquipmenList.Add("AQ2211OPTICALSWITCH");
                }
            }
            for (int i = 0; i < EquipmenList.Count; i++)
            {
                myXml.DeleteSingleEquipmentNode(EquipmenList[i].ToString());

                CurrentEquipmentStruct = GetCurrentEquipmentStruct(EquipmenList[i].ToString());
                EquipmentBase EuipmentObject = (EquipmentBase)MyEquipmentManager.Createobject(EquipmenList[i].ToString());
                if (!EuipmentObject.Initialize(CurrentEquipmentStruct) || !EuipmentObject.Configure(1))
                {
                        Exception ex = new Exception(EquipmenList[i].ToString() + " Configure Error");
                        throw ex;
                }
                else
                {
                    FlagEquipmentConfigOK = true;
                    MessageBox.Show("ConfigEuipment OK.");
                }

                string[] HeadArray = new string[CurrentEquipmentStruct.Length];

                string[] DataArray = new string[CurrentEquipmentStruct.Length];

                for (int j = 0; j < CurrentEquipmentStruct.Length; j++)
                {
                    HeadArray[j] = CurrentEquipmentStruct[j].FiledName;
                    DataArray[j] = CurrentEquipmentStruct[j].DefaultValue.ToString();
                }

                myXml.FitEquipmentInputParameterToXml("Node", EquipmenList[i].ToString(), HeadArray, DataArray);
            }
        }

        public TestModeEquipmentParameters[] GetCurrentEquipmentStruct(string EuipmentName)
        {
            TestModeEquipmentParameters[] EquipmentStruct;
            switch (EuipmentName)
            {
                case("E3631"):
                    EquipmentStruct = new TestModeEquipmentParameters[2];
                    EquipmentStruct[0].FiledName = "Addr";
                    EquipmentStruct[0].DefaultValue = tbPowerGPIB.Text;
                    EquipmentStruct[1].FiledName = "IOType";
                    EquipmentStruct[1].DefaultValue = "GPIB";
                    break;
                case ("DP811A"):
                    EquipmentStruct = new TestModeEquipmentParameters[2];
                    EquipmentStruct[0].FiledName = "Addr";
                    EquipmentStruct[0].DefaultValue = tbPowerGPIB.Text;
                    EquipmentStruct[1].FiledName = "IOType";
                    EquipmentStruct[1].DefaultValue = "GPIB";
                    break;
                 case("TPO4300"):
                    EquipmentStruct = new TestModeEquipmentParameters[2];
                    EquipmentStruct[0].FiledName = "Addr";
                    EquipmentStruct[0].DefaultValue = tbTPOGPIB.Text;
                    EquipmentStruct[1].FiledName = "IOType";
                    EquipmentStruct[1].DefaultValue = "GPIB";
                    break;
                 case ("AQ2211OPTICALSWITCH"):
                    EquipmentStruct = new TestModeEquipmentParameters[4];
                    EquipmentStruct[0].FiledName = "Addr";
                    EquipmentStruct[0].DefaultValue = tbAQSwitchGPIB.Text;
                    EquipmentStruct[1].FiledName = "IOType";
                    EquipmentStruct[1].DefaultValue = "GPIB";
                    EquipmentStruct[2].FiledName = "OpticalSwitchSlot";
                    EquipmentStruct[2].DefaultValue = tbAQSwitchSlot.Text;
                    EquipmentStruct[3].FiledName = "SwitchChannel";
                    EquipmentStruct[3].DefaultValue = "1";
                    break;
                 case ("FLEX86100"):
                    EquipmentStruct = new TestModeEquipmentParameters[7];
                    EquipmentStruct[0].FiledName = "Addr";
                    EquipmentStruct[0].DefaultValue = tbFLEX86100GPIB.Text;
                    EquipmentStruct[1].FiledName = "IOType";
                    EquipmentStruct[1].DefaultValue = "GPIB";
                    EquipmentStruct[2].FiledName = "FlexOptChannel";
                    EquipmentStruct[2].DefaultValue = tbTxDCAoChannel.Text;
                    EquipmentStruct[3].FiledName = "FlexDcaWavelength";
                    EquipmentStruct[3].DefaultValue = cbTxDCAwavelength.SelectedIndex.ToString();
                    EquipmentStruct[4].FiledName = "FilterSwitch";
                    if (cbTxDCAFilterSwitch.Checked == true)
                    {
                        EquipmentStruct[4].DefaultValue = "1";
                    }
                    else
                    {
                        EquipmentStruct[4].DefaultValue = "0";
                    }
                    EquipmentStruct[5].FiledName = "FlexDcaDataRate";
                    EquipmentStruct[5].DefaultValue = tbDCADataRate.Text;
                    EquipmentStruct[6].FiledName = "opticalMaskName";
                    EquipmentStruct[6].DefaultValue = tbTxDCAoMask.Text;
                    break;
                default:
                    EquipmentStruct = new TestModeEquipmentParameters[0];
                    break;
            }
            return EquipmentStruct;
        }

        private void cbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPower.SelectedItem.ToString() == "E3631")
            {
                tbPowerGPIB.Text = myXml.E3631Addr;
            }
            if (cbPower.SelectedItem.ToString() == "DP811A")
            {
                tbPowerGPIB.Text = myXml.DP811AAddr;
            }
        }

        private void EquipConfig_Load(object sender, EventArgs e)
        {
            E3631Power = new E3631(MylogManager);
            DP811APower = new DP811A(MylogManager);
            XSteam = new TPO4300(MylogManager);
            FlexScope = new FLEX86100(MylogManager);
            OptSwitch = new AQ2211OpticalSwitch(MylogManager);

            tbPowerGPIB.Text = myXml.E3631Addr;
            tbTPOGPIB.Text = myXml.TPO4300Addr;
            tbAQSwitchGPIB.Text = myXml.AQ2211OptSwitchAddr;
            tbAQSwitchSlot.Text = myXml.AQ2211OptSwitchSlot;
            tbFLEX86100GPIB.Text = myXml.FLEXAddr;
            tbTxDCAoChannel.Text = myXml.FLEXOptChannel;
            cbTxDCAwavelength.SelectedIndex = Convert.ToInt32(myXml.FLEX86100WaveLength);
            if (myXml.FLEXFilterSwitch == "1")
            {
                cbTxDCAFilterSwitch.Checked = true;
            }
            else
            {
                cbTxDCAFilterSwitch.Checked = false;
            }
            tbDCADataRate.Text = myXml.FLEXDCADataRate;
            tbTxDCAoMask.Text = myXml.FLEXMaskName;
        }

    }
}
