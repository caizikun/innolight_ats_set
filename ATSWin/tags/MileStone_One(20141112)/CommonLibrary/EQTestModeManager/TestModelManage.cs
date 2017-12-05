﻿using System;
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
                case "ADJUSTEYE":
                    return new AdjustEye(pDut, pLogManager);

                case "ADJUSTTXPOWERDMI":
                    return new AdjustTxPowerDmi(pDut, pLogManager);

                case "TESTEYE":
                    return new TestEye(pDut, pLogManager);

                case "TESTTXPOWERDMI":
                    return new TestTxPowerDmi(pDut, pLogManager);

                case "TESTIBIASDMI":
                    return new TestIBiasDmi(pDut, pLogManager);

                case "ADJUSTAPD":
                    return new AdjustAPD(pDut, pLogManager);

                case "ADJUSTLOS":
                    return new AdjustLos(pDut, pLogManager);

                case "CALRXDMI":
                    return new CalRxDmi(pDut, pLogManager);

                case "TESTBER":
                    return new TestBer(pDut, pLogManager);

                case "TESTRXLOSAD":
                    return new TestRXLosAD(pDut, pLogManager);

                case "TESTRXPOWERDMI":
                    return new TestRxPowerDmi(pDut, pLogManager);

                case "CALVCCDMI":
                    return new CalVccDmi(pDut, pLogManager);

                case "CALTEMPDMI":
                    return new CalTempDmi(pDut, pLogManager);

                case "TESTTEMPDMI":
                    return new TestTempDmi(pDut, pLogManager);

                case "TESTVCCDMI":
                    return new TestVccDmi(pDut, pLogManager);
                case "TESTICC":
                    return new TestIcc(pDut, pLogManager);
                case "CALRXDMINOPROCESSINGCOEF":
                    return new CalRxDminoProcessingCoef(pDut, pLogManager);
                case "CALVCCDMINOPROCESSINGCOEF":
                    return new CalVccDminoProcessingCoef(pDut, pLogManager);
                case "CALTEMPDMINOPROCESSINGCOEF":
                    return new CalTempDminoProcessingCoef(pDut, pLogManager);
               
                default:
                    return null;
            }


        }
    }
}