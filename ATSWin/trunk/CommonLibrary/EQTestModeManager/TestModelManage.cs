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
                case "ADJUSTEYECURVEERROR":
                       return new AdjustEYECurveError(pDut, pLogManager);
                //AdjustEye_CustomTEC
               
                case "ADJUSTCROSSING":
                       return new AdjustCrossing(pDut, pLogManager);
                case "ADJUSTMASK":
                       return new AdjustMask(pDut, pLogManager);
                case "ADJUSTJITTER":
                       return new AdjustJitter(pDut, pLogManager);
                case "ADJUSTTXADC":
                       return new AdjustTxADC(pDut, pLogManager);
                   // AdjustEye_Optimize

#endregion
#region AdjustRx
                case "ADJUSTAPD":
                       return new AdjustAPD(pDut, pLogManager);

                case "ADJUSTLOS":
                       return new AdjustLos(pDut, pLogManager);
                case "ADJUSTLOSMATA37044":
                       return new AdjustLosMata37044(pDut, pLogManager);
               
#endregion
#region AdjustDMI
              case "ADJUSTTXPOWERDMI":
                    return new AdjustTxPowerDmi(pDut, pLogManager);
              case "ADJUSTVCCDMI":
                    return new AdjustVccDmi(pDut, pLogManager);
              case "ADJUSTTEMPDMI":
                    return new AdjustTempDmi(pDut, pLogManager);
              case "ADJUSTTEMPDMIBYDIE":
                    return new AdjustTempDmiByDie(pDut, pLogManager);
              case "ADJUSTRXDMI":
                       return new AdjustRxDmi(pDut, pLogManager);
#endregion
#region TestTx
              case "TESTTXEYE":
                    return new TestTxEye(pDut, pLogManager);
              case "TESTITEC":
                    return new TestITec(pDut, pLogManager);
              case "TESTWAVELENGTH":
                    return new TestWavelength(pDut, pLogManager);
              case "TESTTXEYERINOMA":
                    return new TestTxEyeRinOMA(pDut, pLogManager);
                case "TESTTXDMIPOWCURVE":
                    return new TestTxDmiPowCurve(pDut, pLogManager);
                case"TESTTXDP":
                    return new TestTxDP(pDut, pLogManager);
				case "TESTTXRETURNLOSTTOLERANCE":
                    return new TestTxReturnLostTolerance(pDut, pLogManager);
                case "TESTTXEYETDEC":
                    return new TestTxEyeTDEC(pDut, pLogManager);
          
#endregion
#region TestRx
              case "TESTBER":
                    return new TestBer(pDut, pLogManager);
              case "TESTBERINTENSIFY":
                    return new TestBerIntensify(pDut, pLogManager);
              case "TESTRXLOSAD":
                    return new TestRXLosAD(pDut, pLogManager);
              case "TESTTRANSFER":
                    return new TestTransfer(pDut, pLogManager);
              case "TESTRXEYE":
                    return new TestRxEye(pDut, pLogManager);
              case "TESTRXRESPONSIVITY":
                    return new TestRxResponsivity(pDut, pLogManager);
              case "TESTRXOVERLOAD":
                    return new TestRxOverload(pDut, pLogManager);
              case "TESTRXEYEVCMIVEC":
                    return new TestRxEyeVcmiVec(pDut, pLogManager);
               
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
              case "TESTRXDMIPOWERRORCURVE":
                    return new TestRxDmiPowErrorCurve(pDut, pLogManager);
#endregion
#region Test Other
              case "ALARMWARNING":
                    return new AlarmWarning(pDut, pLogManager);  
                case "TESTICC":
                    return new TestIcc(pDut, pLogManager);

                case "TESTPOLARITY":
                    return new TestPolarity(pDut, pLogManager);

                case "SETTXDMIASSOURCELIGHT":
                    return new SetTxDmiAsSourceLight(pDut, pLogManager);

                case "TESTRXEYEVECEW":
                    return new TestRxEyeVecEW(pDut, pLogManager);

                case "SETBIASADCOFFSET":
                    return new SetBiasAdcOffset(pDut, pLogManager);
                    
#endregion
                default:
                    return null;
            }


        }
    }
}