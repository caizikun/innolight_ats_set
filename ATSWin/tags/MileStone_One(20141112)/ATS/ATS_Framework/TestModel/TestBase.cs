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
        protected string logoStr = null;
        protected globalParameters GlobalParameters;
        protected EquipmentList selectedEquipList = new EquipmentList();
        protected DUT dut;
        protected logManager logger;
        protected Algorithm algorithm = new Algorithm(); 
        public TestModeEquipmentParameters[] GetoutputParameters
        {
            get
            {
                return outputParameters;
            }

        }
        public TestModeEquipmentParameters[] SetoutputParameters
        {
            set
            {
                outputParameters = value;
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
#endregion
       
#region Method
        public TestModelBase()
        {
           
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

            if (!CheckEquipmentReadiness()) return false;
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
            if (PrepareTest())
            {
                if (StartTest())
                    return PostTest();
                else
                {
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
       
#endregion
        
      
    }
        


}
