using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS_Driver
{
    public class SyncRoot_PPG_ED//used for thread synchronization of PPG and ED
    {
        private static volatile SyncRoot_PPG_ED instance = null;
        private static object syncRoot = new Object();

        private SyncRoot_PPG_ED() { }

        public static SyncRoot_PPG_ED Get_SyncRoot_PPG_ED()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SyncRoot_PPG_ED();
                    }
                }
            }
            return instance;
        }
    }

    public class SyncRoot_AQ2211//used for thread synchronization of AQ2211PowerMeter, AQ2211Atten and AQ2211OpticalSwitch
    {
        private static volatile SyncRoot_AQ2211 instance = null;
        private static object syncRoot = new Object();

        private SyncRoot_AQ2211() { }

        public static SyncRoot_AQ2211 Get_SyncRoot_AQ2211()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SyncRoot_AQ2211();
                    }
                }
            }
            return instance;
        }
    }

    public class SyncRoot_MAP200//used for thread synchronization of MAP200PowerMeter, MAP200Atten and MAP200OpticalSwitch
    {
        private static volatile SyncRoot_MAP200 instance = null;
        private static object syncRoot = new Object();

        private SyncRoot_MAP200() { }

        public static SyncRoot_MAP200 Get_SyncRoot_MAP200()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SyncRoot_MAP200();
                    }
                }
            }
            return instance;
        }
    }

    public class SyncRoot_CFP_ED//used for thread synchronization of CFP, CFPExclusiveED
    {
        private static volatile SyncRoot_CFP_ED instance = null;
        private static object syncRoot = new Object();

        private SyncRoot_CFP_ED() { }

        public static SyncRoot_CFP_ED Get_SyncRoot_CFP_ED()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SyncRoot_CFP_ED();
                    }
                }
            }
            return instance;
        }
    }
}
