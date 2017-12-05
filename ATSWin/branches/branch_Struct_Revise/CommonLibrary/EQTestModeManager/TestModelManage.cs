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



        public void SetDutObject(DUT aDut)
        {
            pDut = aDut;

        }
        public TestModelBase Createobject(string Str_name)
        {
           
            switch (Str_name.ToUpper())
            {
#region AdjustTx
                case "ADJUSTEYE":
                       return new AdjustEye(pDut);
                case "ADJUSTCROSSING":
                       return new AdjustCrossing(pDut);
                case "ADJUSTMASK":
                       return new AdjustMask(pDut);
                case "ADJUSTJITTER":
                       return new AdjustJitter(pDut);
                case "ADJUSTTXADC":
                       return new AdjustTxADC(pDut);
                   // AdjustEye_Optimize

#endregion
#region AdjustRx
                case "ADJUSTAPD":
                       return new AdjustAPD(pDut);

                case "ADJUSTLOS":
                       return new AdjustLos(pDut);
                case "ADJUSTLOSMATA37044":
                       return new AdjustLosMata37044(pDut);
               
#endregion
#region AdjustDMI
              case "ADJUSTTXPOWERDMI":
                    return new AdjustTxPowerDmi(pDut);
              case "ADJUSTVCCDMI":
                    return new AdjustVccDmi(pDut);
              case "ADJUSTTEMPDMI":
                    return new AdjustTempDmi(pDut);
              case "ADJUSTRXDMI":
                       return new AdjustRxDmi(pDut);
              case "ADJUSTTEMPDMIBYDIE":
                       return new AdjustTempDmiByDie(pDut);     
#endregion
#region TestTx
              case "TESTTXEYE":
                    return new TestTxEye(pDut);
              case "TESTITEC":
                    return new TestITec(pDut);
              case "TESTWAVELENGTH":
                    return new TestWavelength(pDut);
              case "TESTTXEYERINOMA":
                    return new TestTxEyeRinOMA(pDut);
                case "TESTTXDMIPOWCURVE":
                    return new TestTxDmiPowCurve(pDut);
                case"TESTTXDP":
                    return new TestTxDP(pDut);
				case "TESTTXRETURNLOSTTOLERANCE":
                    return new TestTxReturnLostTolerance(pDut);
                case "TESTTXEYETDEC":
                    return new TestTxEyeTDEC(pDut);
          
#endregion
#region TestRx
              case "TESTBER":
                    return new TestBer(pDut);
              case "TESTBERINTENSIFY":
                    return new TestBerIntensify(pDut);
              case "TESTRXLOSAD":
                    return new TestRXLosAD(pDut);
              case "TESTTRANSFER":
                    return new TestTransfer(pDut);
              case "TESTRXEYE":
                    return new TestRxEye(pDut);
              case "TESTRXRESPONSIVITY":
                    return new TestRxResponsivity(pDut);
              case "TESTRXOVERLOAD":
                    return new TestRxOverload(pDut);
              case "TESTRXEYEVCMIVEC":
                    return new TestRxEyeVcmiVec(pDut);
               
#endregion
#region TestDmi
              case "TESTTXPOWERDMI":
                    return new TestTxPowerDmi(pDut);

              case "TESTIBIASDMI":
                    return new TestIBiasDmi(pDut);
              case "TESTRXPOWERDMI":
                    return new TestRxPowerDmi(pDut);

              case "TESTTEMPDMI":
                    return new TestTempDmi(pDut);

              case "TESTVCCDMI":
                    return new TestVccDmi(pDut);
              case "TESTRXDMIPOWERRORCURVE":
                    return new TestRxDmiPowErrorCurve(pDut);
#endregion
#region Test Other
              case "ALARMWARNING":
                    return new AlarmWarning(pDut);  
                case "TESTICC":
                    return new TestIcc(pDut);

                case "TESTPOLARITY":
                    return new TestPolarity(pDut);

                case "SETTXDMIASSOURCELIGHT":
                    return new SetTxDmiAsSourceLight(pDut);

                case "TESTRXEYEVECEW":
                    return new TestRxEyeVecEW(pDut);

                case "SETBIASADCOFFSET":
                    return new SetBiasAdcOffset(pDut);
                    
#endregion
                default:
                    return null;
            }


        }
    }
}