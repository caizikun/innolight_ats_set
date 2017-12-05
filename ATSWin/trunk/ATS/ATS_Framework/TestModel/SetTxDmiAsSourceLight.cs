using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ATS_Framework
{
   public class SetTxDmiAsSourceLight : TestModelBase
    {
        private Powersupply supply;

        private Attennuator attennuator;

        private double[] offsetOpticalPath;

        protected override bool CheckEquipmentReadiness()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].bReady)
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool PrepareTest()
        {//note: for inherited class, they need to do its own preparation process task,
            //then call this base function
            //for (int i = 0; i < pEquipList.Count; i++)
            ////pEquipList.Values[i].IncreaseReferencedTimes();
            //{
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].IncreaseReferencedTimes();
            }
            return AssembleEquipment();
        }

        protected override bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].Configure())
                {
                    return false;
                }
            }
            return true;
        }

        protected override bool AssembleEquipment()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                if (!selectedEquipList.Values[i].OutPutSwitch(true))
                {
                    return false;
                }
            }

            return true;
        }

        public SetTxDmiAsSourceLight(DUT inDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inDut;
        }

        public override bool SelectEquipment(EquipmentList equipmentList)
        {
            selectedEquipList.Clear();

            if (equipmentList.Count == 0)
            {
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;

                for (byte i = 0; i < equipmentList.Count; i++)
                {
                    if (equipmentList.Keys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        supply = (Powersupply)equipmentList.Values[i];
                        isOK = true;
                    }

                    if (equipmentList.Keys[i].ToUpper().Contains("AQ2211ATTEN"))
                    {
                        attennuator = (Attennuator)equipmentList.Values[i];
                        isOK = true;
                    }

                    if (equipmentList.Keys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        equipmentList.Values[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }

                if (supply != null)
                {
                    isOK = true;
                }
                else
                {
                    if (supply == null)
                    {
                        logoStr += logger.AdapterLogString(3, "POWERSUPPLY =NULL");
                    }
                    isOK = false;
                    OutPutandFlushLog();
                }

                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);
                }
                return isOK;
            }
        }

        private void OutPutandFlushLog()
        {
            try
            {
                //AnalysisOutputProcData(procData);
                logger.FlushLogBuffer();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        protected override bool StartTest()
        {
            logger.FlushLogBuffer();
            logoStr = "";

            if (supply == null || attennuator == null)
            {
                return false;
            }

            CloseandOpenAPC(Convert.ToByte(APCMODE.IBIASOFFandIMODON));

            dut.FullFunctionEnable();
            dut.TxAllChannelEnable();

            if (offsetOpticalPath == null)
            {
                string configFile = @"Config.xml";
                XmlDocument document = new XmlDocument();
                document.Load(configFile);
                string xpath = @"/Config/Node[@Item='EquipmentOffset']/Attennuator";
                XmlNodeList nodes = document.DocumentElement.SelectNodes(xpath);//such as xpath="story[author[name='Tang Chao']]"
                string buf = "";
                foreach (XmlNode node in nodes)
                {
                    if (node is XmlElement)
                    {
                        buf = node.InnerText;
                        break;
                    }
                    return false;
                }

                string[] buffer = buf.Split(',');
                offsetOpticalPath = new double[buffer.Length];
                for (int i = 0; i < buffer.Length; i++)
                {
                    offsetOpticalPath[i] = Convert.ToDouble(buffer[i]);
                }
            }

            if (dut.MoudleChannel == 1)
            { 
                attennuator.offsetlist.Clear();
            }

            double sourceLight_dBm = dut.ReadDmiTxp();

            if (sourceLight_dBm < -10)
            {
                logoStr += logger.AdapterLogString(3, "sourceLight_dBm is less than -10. value is " + sourceLight_dBm.ToString("f2"));
                return false;
            }
            sourceLight_dBm -= offsetOpticalPath[dut.MoudleChannel - 1];
            attennuator.configoffset(dut.MoudleChannel.ToString(), sourceLight_dBm.ToString());

            logger.AdapterLogString(0, "channel: " + attennuator.CurrentChannel + ". sourceLight_dBm: " + sourceLight_dBm);
            OutPutandFlushLog();
            return true;
        }

    }
}
