using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;
using System.Threading;
using System.Data;
using System.Collections;

namespace ATS_Framework
{

    public class SetBiasAdcOffset: TestModelBase
    {       
        UInt16[] biasADCs;   

        private Powersupply supply;

        private bool result = true;

        public SetBiasAdcOffset(DUT inDut, logManager logmanager)
        {
            logger = logmanager;
            dut = inDut;
        }

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

        public override bool SelectEquipment(EquipmentList eqList)
        {
            selectedEquipList.Clear();
            if (eqList.Count == 0)
            {
                selectedEquipList.Add("DUT", dut);
                return false;
            }
            else
            {
                bool isOK = false;
                selectedEquipList.Clear();
                IList<string> tempKeys = eqList.Keys;
                IList<EquipmentBase> tempValues = eqList.Values;
                for (byte i = 0; i < eqList.Count; i++)
                {  
                    if (tempKeys[i].ToUpper().Contains("POWERSUPPLY"))
                    {
                        selectedEquipList.Add("POWERSUPPLY", tempValues[i]);
                    }

                    if (tempKeys[i].ToUpper().Contains("NA_OPTICALSWITCH"))
                    {
                        tempValues[i].CheckEquipmentRole(1, GlobalParameters.CurrentChannel);
                    }
                }

                supply = (Powersupply)selectedEquipList["POWERSUPPLY"];

                if (supply != null)
                {
                    isOK = true;
                }
                else
                {                   
                    if (supply == null)
                    {
                        logoStr += logger.AdapterLogString(3, "supply =NULL");
                    }

                    OutPutandFlushLog();
                    isOK = false;
                }
                if (isOK)
                {
                    selectedEquipList.Add("DUT", dut);
                    return isOK;
                }
                return isOK;
            }
        }

        private void OutPutandFlushLog()
        {
            try
            {
                AnalysisOutputParameters(outputParameters);
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
            if (!Test())
            {
                //OutPutandFlushLog();
                return false;
            }
            else
            {
                //OutPutandFlushLog();
                return true;
            }
        }

        public override bool Test()
        {
            try
            {
                logger.FlushLogBuffer();
                logoStr = "";
                //// 是否要测试需要添加判定

                if (AnalysisInputParameters(inputParameters) == false)
                {
                    OutPutandFlushLog();
                    return false;
                }               

                if (supply != null)
                {
                    // open apc 
                    CloseandOpenAPC(Convert.ToByte(APCMODE.IBAISandIMODON));
                   

                    SetTxBiasAdcOffset();
                    
                    OutPutandFlushLog();
                    return true;
                }
                else
                {
                    logoStr += logger.AdapterLogString(4, "Equipments are not enough!");
                    OutPutandFlushLog();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }        

        protected override bool AnalysisInputParameters(TestModeEquipmentParameters[] InformationList)
        {
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

        protected override bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            bool flag = DeassembleEquipment();
            for (int i = 0; i < selectedEquipList.Count; i++)
            {
                selectedEquipList.Values[i].DecreaseReferencedTimes();

            }
            return flag;
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

        protected override bool AnalysisOutputParameters(TestModeEquipmentParameters[] infoList)
        {
            return true;
        }

        protected bool SetTxBiasAdcOffset()
        {
            try
            {
                if (biasADCs == null)
                {
                    biasADCs = new ushort[GlobalParameters.TotalChCount];

                    for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                    {
                        dut.ChangeChannel((i + 1).ToString());
                        dut.SetSoftTxDis();
                        Thread.Sleep(20);
                    }

                    for (int i = 0; i < GlobalParameters.TotalChCount; i++)
                    {
                        dut.ChangeChannel((i + 1).ToString());
                        dut.ReadBiasADC(out biasADCs[i]);
                        logoStr += logger.AdapterLogString(0, "biasADC of this channel is " + biasADCs[i]);

                        if (biasADCs[dut.MoudleChannel - 1] == 65535)
                        {
                            logoStr += logger.AdapterLogString(4, "biasADC of this channel is 65535.");
                            return false;
                        }
                    }
                    dut.TxAllChannelEnable();
                    //return to current channel
                    dut.ChangeChannel(GlobalParameters.CurrentChannel.ToString());

                    int sum = 0;
                    for (int i = 0; i < biasADCs.Length; i++)
                    {
                        sum += biasADCs[i];
                    }
                    ushort avg = (ushort)(sum / biasADCs.Length);
                    dut.SetBiasAdcOffset(avg);
                    logoStr += logger.AdapterLogString(0, "completed to set biasAdcOffset to " + avg);

                    ushort returndata = 0;
                    dut.ReadBiasAdcOffset(out returndata);
                    result = (avg == returndata) ? true : false;
                    return result;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
