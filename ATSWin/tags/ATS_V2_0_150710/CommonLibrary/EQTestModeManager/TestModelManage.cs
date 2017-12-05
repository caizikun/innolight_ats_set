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
#region AdjustTx
                case "ADJUSTEYE":
                       return new AdjustEye(pDut, pLogManager);

#endregion
#region AdjustRx
                case "ADJUSTAPD":
                       return new AdjustAPD(pDut, pLogManager);

                case "ADJUSTLOS":
                       return new AdjustLos(pDut, pLogManager);
               
#endregion
#region AdjustDMI
              case "ADJUSTTXPOWERDMI":
                    return new AdjustTxPowerDmi(pDut, pLogManager);
              case "ADJUSTVCCDMI":
                    return new AdjustVccDmi(pDut, pLogManager);
              case "ADJUSTTEMPDMI":
                    return new AdjustTempDmi(pDut, pLogManager);
              case "ADJUSTRXDMI":
                       return new AdjustRxDmi(pDut, pLogManager);
#endregion
#region TestTx
              case "TESTTXEYE":
                    return new TestTxEye(pDut, pLogManager);
          
#endregion
#region TestRx
              case "TESTBER":
                    return new TestBer(pDut, pLogManager);
              case "TESTRXLOSAD":
                    return new TestRXLosAD(pDut, pLogManager);
              case "TESTTRANSFER":
                    return new TestTransfer(pDut, pLogManager);
              case "TESTRXEYE":
                    return new TestRxEye(pDut, pLogManager);
#endregion
#region TestDmi
              case "TESTTXPOWERDMI":
                    return new TestTxPowerDmi(pDut, pLogManager);

              case "TESTIBIASDMI":
                    return new TestIBiasDmi(pDut, pLogManager);
              case "TESTRXPOWERDMI":
                    return new TestRxPowerDmi(pDut, pLogManager);

              case "TESTTEMPDMI":
                    return new TestTempDmi(pDut, pLogManager);

              case "TESTVCCDMI":
                    return new TestVccDmi(pDut, pLogManager);
#endregion
#region Test Other
              case "ALARMWARNING":
                    return new AlarmWarning(pDut, pLogManager);   
                case "TESTICC":
                    return new TestIcc(pDut, pLogManager);
#endregion
                default:
                    return null;
            }


        }
    }
}