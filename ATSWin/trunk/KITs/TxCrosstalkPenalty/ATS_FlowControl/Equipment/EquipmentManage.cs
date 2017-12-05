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
          
                case "TPO4300":
                    return new TPO4300(plogManager);
                case "SFP28DUT":
                    return new SFP28DUT(plogManager);
               
                case "QSFP28DUT":
                    return new QSFP28DUT(plogManager);
                case "CFP4DUT":
                    return new CFP4DUT(plogManager);
              
                case "FLEX86100":
                    return new FLEX86100(plogManager);
                case "MP1800PPG":
                    return new MP1800PPG(plogManager);
                case "MP1800ED":
                    return new MP1800ED(plogManager);
          
              
                default:
                    return null;
            }

        }
    }
}
