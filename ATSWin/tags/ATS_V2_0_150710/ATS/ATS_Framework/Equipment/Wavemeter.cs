using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS_Framework
{
    public class Wavemeter:EquipmentBase
    {
        public Double WavelengthMax;
        public Double WavelengthMin;

        public Wavemeter()
        {

        }

        public virtual Double GetWavelength()
        {
            return  0;
        }
    
      
    }
}
