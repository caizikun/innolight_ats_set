using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS_Framework
{
  

    public class TestModelList : SortedList<string, TestModelBase>
    {
    }


    public class TestModelManage : TestModelBase
    {
        private DUT pDut = new DUT();
        private logManager pLogManager;


        public void SetDutObject(DUT aDut, logManager aLogManager)
        {
            pDut = aDut;
            pLogManager = aLogManager;
        }
        public TestModelBase Createobject(string Str_name)
        {
           
            switch (Str_name.ToUpper())
            {

#region TestTx
              case "TESTTXEYE":
                    return new TestTxEye(pDut, pLogManager);
             
          
#endregion
#region TestRx
              case "TESTBER":
                    return new TestBer(pDut, pLogManager);
            
#endregion

                default:
                    return null;
            }


        }
    }
}