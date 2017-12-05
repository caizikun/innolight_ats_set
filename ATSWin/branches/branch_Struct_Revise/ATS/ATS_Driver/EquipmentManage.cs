using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;

namespace ATS_Driver
{
    public class EquipmentManage :EQManage
    {
        public override  Object Createobject(string Name)
        {
            switch (Name.Trim())
            {
                case "E3631":
                    return new E3631();
                case "N490XPPG":
                    return new N490XPPG();
                case "N490XED":
                    return new N490XED();
                case "D86100":
                    return new D86100();
                case "AQ2211ATTEN":
                    return new AQ2211Atten();
                case "TPO4300":
                    return new TPO4300();
                case "SFP28DUT":
                    return new SFP28DUT();
                case "SFPDUT":
                    return new SFPDUT();
                case "XFPDUT":
                    return new XFPDUT();
                case "QSFP28DUT":
                    return new QSFP28DUT();
                case "CFP4DUT":
                    return new CFP4DUT();
                case "AQ2211POWERMETER":
                    return new AQ2211PowerMeter();
                case "AQ2211OPTICALSWITCH":
                    return new AQ2211OpticalSwitch();
                case "FLEX86100":
                    return new FLEX86100();
                case "MP1800PPG":
                    return new MP1800PPG();
                case "MP1800ED":
                    return new MP1800ED();
                case"ELECTRICALSWITCH":
                    return new ElectricalSwitch();
                case "MAP200ATTEN":
                    return new MAP200Atten();
                case "MAP200OPTICALSWITCH":
                    return new MAP200OpticalSwitch();
                case "MAP200POWERMETER":
                    return new MAP200PowerMeter(); 
                case "INNO25GBERTPPG":
                    return new Inno25GBertPPG();
                case "INNO25GBERTED":
                    return new Inno25GBertED();
                case "INNO25GBERT_V2_PPG":
                    return new Inno25GBert_V2_PPG();
                case "INNO25GBERT_V2_ED":
                    return new Inno25GBert_V2_ED();
                case "C86122":
                    return new C86122();
                case "C86120":
                    return new C86120();
                case "N4960PPG":
                   return new N4960PPG();
                case "N4960ED":
                   return new N4960ED();
                case "AQ6370":
                   return new AQ6370();
                case "INNOBERT14G24CH_PPG":
                   return new InnoBert14G24CH_PPG();
                case "INNOBERT14G24CH_ED":
                   return new InnoBert14G24CH_ED();
                case "MAP200COMPSWTICH":
                   return new MAP200CompSwtich();
                case "CFPEXCLUSIVEED":
                   return new CFPExclusiveED();
                case "TA5000":
                   return new TA5000();  
                default:
                    return null;
            }

        }
    }
}
