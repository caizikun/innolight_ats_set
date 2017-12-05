using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using Ivi.Visa.Interop;

namespace ATS_Framework
{
    public class TestModelBase
    {
#region Attribute
        protected TestModeEquipmentParameters[] outputParameters;
        protected TestModeEquipmentParameters[] inputParameters;
        protected TestModeEquipmentParameters[] procData;
        protected DataTable specParameters = new DataTable();
        protected string logoStr = null;
        protected globalParameters GlobalParameters;
        protected EquipmentList selectedEquipList = new EquipmentList();
        protected DUT dut;
        protected logManager logger;

        protected Algorithm algorithm = new Algorithm();

        protected bool flagCurrentTestModel = true;
        protected SpecTableStruct[] SpecTableStructArray;
        public TestModeEquipmentParameters[] GetoutputParameters
        {
            get
            {
                return outputParameters;
            }

        }
        public TestModeEquipmentParameters[] GetProcData
        {
            get
            {
                return procData;
            }

        }
        public TestModeEquipmentParameters[] SetinputParameters
        {
            set
            {
                inputParameters = value;
            }

        }
        public globalParameters SetGlobalParameters
        {
            set
            {
                GlobalParameters = value;
            }

        }
        public string GetLogInfor
        {
            get
            {
                return logoStr;
            }

        }
        public DataTable SetGetSpecParametersDataTable
        {
            set
            {
                specParameters = value;
            }
            get
            {
                return specParameters;
            }
        }
#endregion
       
#region Method
        public TestModelBase()
        {
            specParameters.Clear();
        }
        virtual public bool SelectEquipment(EquipmentList aEquipList)
        {

            return true;

        }

        //virtual public bool SelectConfigData(ConditionConfigData cc)
        //{
        //    return true;
        //}
        public bool RunTest()
        {
            // config重复先屏蔽掉以前代码
            //if (PrepareTest())
            //{
            //    if (ConfigureEquipment(selectedEquipList) && StartTest())
            //        return PostTest();
            //    else
            //    {
            //        PostTest();
            //        return false;
            //    }
            //}
            // 去掉ConfigureEquipment(selectedEquipList)代码
            if (!CheckEquipmentReadiness()) return false;          
            if (PrepareTest())
            {
                try
                {
                    if (StartTest())
                        return PostTest();
                    else
                    {
                        PostTest();
                        return false;
                    }
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.ToString());
                    PostTest();
                    return false;
                }
               
            }
            return false;
        }

        virtual protected bool CheckEquipmentReadiness()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
                if (!selectedEquipList.Values[i].bReady) return false;
            return true;
        }


        virtual protected bool PrepareTest()
        {
            for (int i = 0; i < selectedEquipList.Count; i++)
                selectedEquipList.Values[i].IncreaseReferencedTimes();
            return AssembleEquipment();
        }

        protected virtual bool ConfigureEquipment(EquipmentList selectedEquipmentList)
        {
            return true;
        }

        virtual protected bool StartTest()
        {
            return true;
        }
        virtual protected bool PostTest()
        {//note: for inherited class, they need to call base function first,
            //then do other post-test process task
            bool flag = DeassembleEquipment();
            for (int i = 0; i < selectedEquipList.Count; i++)
                selectedEquipList.Values[i].DecreaseReferencedTimes();
            return flag;
        }

        virtual protected bool DeassembleEquipment()
        {
            return true;
        }
        virtual protected bool AssembleEquipment()
        {
            return true;
        }

        virtual protected bool AnalysisInputParameters(TestModeEquipmentParameters[] inputParameters)
        {
            return false;
        }
        virtual protected bool AnalysisOutputParameters(TestModeEquipmentParameters[] outputParameters)
        {
            return false;
        }
        virtual public bool Test()
        {
            return true;
        }
        virtual protected bool AnalysisOutputProcData(TestModeEquipmentParameters[] outputParameters)
        {
            return false;
        }
        public bool CloseandOpenAPC(byte mode)
        {
            bool isOK = false;
            if (GlobalParameters.APCType == Convert.ToByte(apctype.none))
            {
                logoStr += logger.AdapterLogString(0, "no apc");
                return true;
            }
            try
            {
                switch (mode)
                {
                    case (byte)APCMODE.IBAISandIMODON:
                        {
                            logoStr += logger.AdapterLogString(0, "Open apc");
                            isOK = dut.APCON(0x11);
                           // isOK = dut.APCON(0x00);
                            logoStr += logger.AdapterLogString(0, "Open apc" + isOK.ToString());   
                            break;
                        }
                    case (byte)APCMODE.IBAISandIMODOFF:
                        {
                            logoStr += logger.AdapterLogString(0, " Close apc");
                            isOK = dut.APCOFF(0x11);
                            logoStr += logger.AdapterLogString(0, "Close apc" + isOK.ToString());                           
                        }
                        break;
                    case (byte)APCMODE.IBIASONandIMODOFF:
                        {
                            logoStr += logger.AdapterLogString(0, " Close IModAPCand Open IBiasAPC");
                            isOK = dut.APCON(0x01);
                            logoStr += logger.AdapterLogString(0, "Close IModAPCand Open IBiasAPC" + isOK.ToString()); 
                            break;
                        }
                    case (byte)APCMODE.IBIASOFFandIMODON:
                        {
                            logoStr += logger.AdapterLogString(0, " Close IBiasAPCand Open IModAPC");
                            isOK = dut.APCON(0x10);
                            logoStr += logger.AdapterLogString(0, "Close IBiasAPCand Open IModAPC" + isOK.ToString());
                            break;
                        }
                        default:
                        {
                            break;
                        }
                           
                }
                return isOK;
            }
            catch (System.Exception ex)
            {
            	throw ex;
            }
        }
        public void GenerateSpecList(SortedList<byte, string> input)
        {
            SpecTableStructArray = new SpecTableStruct[input.Count];
            try
            {
                for (int i = 0; i < SpecTableStructArray.Length; i++)
                {
                    SpecTableStructArray[i].ItemName = input.Values[i].Trim();
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
#endregion
        
      
    }
        


}
