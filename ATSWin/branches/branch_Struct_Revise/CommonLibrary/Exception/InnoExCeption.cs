using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace ATS_Framework
{
    public class ExceptionDictionary : Dictionary<int, string>
    {
        public enum Code : int
        {
            #region Test Model

            _0x02000 = 0x02000,
            _0x02001 = 0x02001,

            _0x02100 = 0x02100,
            _0x02101 = 0x02101,
            _0x02102 = 0x02102,
            _0x02103 = 0x02103,
            _0x02104 = 0x02104,
            _0x02105 = 0x02105,
            _0x02106 = 0x02106,
            _0x02107 = 0x02107,
            _0x02108 = 0x02108,
            _0x02109 = 0x02109,
            _0x02110 = 0x02110,
            _0x02111 = 0x02111,
            
            _0x02500 = 0x02500,
            _0x02501 = 0x02501,
            _0x02502 = 0x02502,
            _0x02503 = 0x02503,
            _0x02F00 = 0x02F00,

            _0x02F03 = 0x02F03,
            _0x02F04 = 0x02F04,
            _0x02F05 = 0x02F05,

            _0x02FFF = 0x02FFF,

            _0xFFFFF = 0xFFFFF,

            #endregion 

            #region Equipment 

            _UnConnect_0x05000 = 0x05000,//Can't Connect
            _UnControl_0x05001 = 0x05001,// Los Of Lock
            _Funtion_Fatal_0x05002 = 0x05002,//fatal
            _Funtion_UnFatal_0x05003 = 0x05003,// Unfatal
           

            #endregion

            #region Dut

            _Write_DAC_Fail_0x05100 = 0x05100,// Write DAC Fail
            _Read_DAC_Fail_0x05101 = 0x05101,// Read Dac Fail
            _Los_parameter_0x05102 = 0x05102,// Can't find DacRegist


            #endregion
        }



        public static string GetMessage(Code code)
        {
            Dictionary<Code, string> dic = new Dictionary<Code, string>();

            #region Test Model

            dic.Add(Code._0x02000, "nonstandard spec");
            dic.Add(Code._0x02001, "nonstandard input parameters");

            dic.Add(Code._0x02100, "Coeff not write in");
            dic.Add(Code._0x02101, "Failed to ACCouple");
            dic.Add(Code._0x02102, "Failed to collect curving parameters");
            dic.Add(Code._0x02103, "Failed to DCCouple");
            dic.Add(Code._0x02104, "Calculate step error");
            dic.Add(Code._0x02105, "Calculate regist error");
            dic.Add(Code._0x02106, "Adjust ERbyIbias error");
            dic.Add(Code._0x02107, "Adjust APbyMod error");
            dic.Add(Code._0x02108, "not find target point");
            dic.Add(Code._0x02109, "failed to adjust APD");
            dic.Add(Code._0x02110, "failed to adjust Jitter");
            dic.Add(Code._0x02111, "failed to adjust mask");

            dic.Add(Code._0x02500, "low optical power");
            dic.Add(Code._0x02501, "high optical power");
            dic.Add(Code._0x02502, "low ER");
            dic.Add(Code._0x02503, "hight ER");

            dic.Add(Code._0x02F00, "lack equipment");

            dic.Add(Code._0x02F03, "equipment is normal, but it is disfunction.");
            dic.Add(Code._0x02F04, "nonstandard output data");
            dic.Add(Code._0x02F05, "nonstandard process data");

            dic.Add(Code._0x02FFF, "can't save log");

            dic.Add(Code._0xFFFFF, "unknown");

            #endregion 

            #region Equipment

            dic.Add(Code._UnConnect_0x05000, "Can't Connect");
            dic.Add(Code._UnControl_0x05001, "Los Of Lock");
            dic.Add(Code._Funtion_Fatal_0x05002, "Fatal");
            dic.Add(Code._Funtion_UnFatal_0x05003, "Unfatal");
           
            #endregion 
            #region Dut

            dic.Add(Code._Write_DAC_Fail_0x05100, "Write DAC Fail");
            dic.Add(Code._Read_DAC_Fail_0x05101, "Read Dac Fail");
            dic.Add(Code._Los_parameter_0x05102, "Can't find DacRegist");//  _0x05101 = 0x05102,// Can't find DacRegist

            #endregion 
            return dic[code];
        }
    }

    public class InnoExCeption: ApplicationException, ISerializable
    {
        public const ushort NaN = 9999;

        private ExceptionDictionary.Code id;
        private string trace;


        public ExceptionDictionary.Code ID
        {
            get
            {
                return this.id;
            }
        }

        public override string Message
        {
            get
            {
                return ExceptionDictionary.GetMessage(id);
            }
        }


        public override string StackTrace
        {
            get
            {
                return this.trace;
            }
        }

        public InnoExCeption(ExceptionDictionary.Code code)
        {
            id = code;

            if (trace == "" || trace==null)
            {
                trace = GetCallClassMethodName();
            }
        }

        public InnoExCeption(ExceptionDictionary.Code code, string stackTrace)
        {
            id = code;
            trace = stackTrace;
            if (trace == "" || trace == null)
            {
                trace = GetCallClassMethodName();
            }
        }
        private string GetCallClassMethodName()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(2);
            string methodName = sf.GetMethod().Name;
            string className = sf.GetMethod().ReflectedType.Name;
            sf = null;
            st = null;
            GC.Collect();
            return className + "." + methodName;
        } 
    }
}
