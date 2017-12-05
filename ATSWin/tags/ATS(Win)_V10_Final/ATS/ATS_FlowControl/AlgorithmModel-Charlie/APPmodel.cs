using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ATS
{
    public class TestAPP
    {
        private string my_string;

        public string My_string
        {
            get { return my_string; }
            set { my_string = value; }
        }
        TxAPPmodel tx = new TxAPPmodel();

        public void aa()
        {
            tx.MyTxModelInterface.Ibias = 20;
        }

    }
    public class APPmodel
    {

        AppModelInterface MyModelInterface = new AppModelInterface();

        TxModelInterface MyTxModelInterface = new TxModelInterface();
 
    }
    public class TxAPPmodel : APPmodel
    {
       public TxModelInterface MyTxModelInterface = new TxModelInterface();

        public void Initialize()
        {
            MyTxModelInterface.Ibias = 20;

            float i = MyTxModelInterface.Ibias;
        }

    }
    public class AppModelInterface//
    {
        public int size=100;
        public byte [] MemoryMap = new byte[100];


        protected UInt16 BaseAddress
        {
            get
            {
              return  (UInt16)(MemoryMap[0]*256+MemoryMap[1]);
               
            }

            set
            {
                MemoryMap[0] = (byte)(value>>8 );
                MemoryMap[1] = (byte)( value -MemoryMap[0]<< 8);
            }

        }
        public byte AppModelID;
        public byte AppModelType;
        public UInt16 AppModelConfiguration;

        

    }
    public class TxModelInterface : AppModelInterface
    {

        public float Ibias
        {
            get
            {
                return (float)((MemoryMap[8] * 256 + MemoryMap[9])*0.2);

            }

            set
            {
               
                
                int i= (int)(value/0.2);

                if (i>65535*0.2||i<0)
                {
                   // 报错
                }
                MemoryMap[8] = (byte)(i >> 8);
               
                MemoryMap[9] = (byte)(i - MemoryMap[8] << 8);
            }
        }
        public float Imod;
    }
    public class RxModelInterface : AppModelInterface
    {

    }


}
