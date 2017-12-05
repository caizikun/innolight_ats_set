using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;

namespace ATS
{
    public class AlgorithmModel
    {
        private double Temp;
        public bool bAdjustFlag;
        public bool bTestFlag;
        public int ConditionId;

        protected EquipmentList aEquipList;

        virtual public void Adjust()
        {

        }
        virtual public void Test()
        {

        }
        virtual  public bool RunTest()
        {
            if (bAdjustFlag)
            {
                Adjust();
            }

            if (bTestFlag)
            {
                Test();
            }
            return true;
        }
       
    }
}
