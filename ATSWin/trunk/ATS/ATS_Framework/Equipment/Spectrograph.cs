using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ivi.Visa.Interop;
using System.Reflection;
using System.IO;



namespace ATS_Framework
{
    public class Spectrograph : EquipmentBase
    {

        public Spectrograph()
        {
        }
        public globalParameters pglobalParameters;

        #region Spectrograph

        public double centerWavelength;   //中心波长
        public double Span;               //扫描范围
        public double Resolution;         //分辨率，默认值0.02

        public double refLevel;    //参考功率，默认值-10
        public double logScale;    //对数刻度，默认值10

        public byte sensitivity;   //灵敏度,0~6对应NORM/HOLD、NORM/AUTO、MID、HIGH1、HIGH2、HIGH3、NORMAL，默认值6（NORMAL）

        public double threshLevel;    //分析参数，仅多模时需要设置，默认值20
        public double kFactor;        //分析参数：谱宽放大倍数，仅多模时需要设置，默认值1

        public int analysisMode;   //分析算法及功能，1：RMS(SPEC WIDTH)，2：DFB-LD(ANALYSIS1)，3：THRESH(SPEC WIDTH),4：PEAK RMS(SPEC WIDTH)；
                                   //多模选择RMS，单模选择DFB-LD

        public byte Destination;   //保存目的地，0：INTernal(内存)，1：EXTernal(USB存储介质)

        #endregion

        public virtual bool StartSweep() { return true; }
        public virtual double GetSpectralWidth() { return 0; }
        public virtual double GetCenterWavelength() { return 0; }
        public virtual double GetSMSR() { return 0; }
        public virtual double GetOSNR() { return 0; }
        public virtual bool SaveWaveformScreen() { return true; }
    }
    
}
