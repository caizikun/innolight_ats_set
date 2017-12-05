using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;

namespace ATS_Driver
{
    public class EquipmentManage :EQManage
    {
        logManager plogManager=new logManager();
        public EquipmentManage( logManager alogManager)
        {
            plogManager=alogManager;
        }
        public override  Object Createobject(string Name)
        {
            switch (Name.Trim())
            {
                case "E3631":
                    return new E3631(plogManager);
                case "N490XPPG":
                    return new N490XPPG(plogManager);
                case "N490XED":
                    return new N490XED(plogManager);
               
                case "AQ2211ATTEN":
                    return new AQ2211Atten(plogManager);
                case "TPO4300":
                    return new TPO4300(plogManager);
                case "SFP28DUT":
                    return new SFP28DUT(plogManager);
                case "SFPDUT":
                    return new SFPDUT(plogManager);
            
                case "QSFP28DUT":
                    return new QSFP28DUT(plogManager);
                case "CFP4DUT":
                    return new CFP4DUT(plogManager);
                case "AQ2211POWERMETER":
                    return new AQ2211PowerMeter(plogManager);
                case "AQ2211OPTICALSWITCH":
                    return new AQ2211OpticalSwitch(plogManager);
                case "FLEX86100":
                    return new FLEX86100(plogManager);
                case "MP1800PPG":
                    return new MP1800PPG(plogManager);
                case "MP1800ED":
                    return new MP1800ED(plogManager);
                case"ELECTRICALSWITCH":
                    return new ElectricalSwitch(plogManager);
                case "MAP200ATTEN":
                    return new MAP200Atten(plogManager);
                case "MAP200OPTICALSWITCH":
                    return new MAP200OpticalSwitch(plogManager);
                case "MAP200POWERMETER":
                    return new MAP200PowerMeter(plogManager); 
              
                case "INNO25GBERT_V2_PPG":
                    return new Inno25GBert_V2_PPG(plogManager);
                case "INNO25GBERT_V2_ED":
                    return new Inno25GBert_V2_ED(plogManager);
                case "C86122":
                    return new C86122(plogManager);
                case "C86120":
                    return new C86120(plogManager);
                case "N4960PPG":
                   return new N4960PPG(plogManager);
                case "N4960ED":
                   return new N4960ED(plogManager);
           
                case "TA5000":
                   return new TA5000(plogManager);  
                default:
                    return null;
            }

        }
    }
}
