using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS_Framework;

namespace ATS
{
    public  class EquipmentArray:IDisposable
    {
        public EquipmentList  MyEquipList = new EquipmentList();

        public bool Add(string StrEquipmenName, EquipmentBase pEquipmentBase)
        {
           // MyEquipList.Add(EquipmenNameArray.Keys[i].ToString(), (EquipmentBase)MyEquipmentManage.Createobject(CurrentEquipmentName));
            MyEquipList.Add(StrEquipmenName, (EquipmentBase)pEquipmentBase);
           // MyEquipList.Clear();
            return true;
        }
        public void Clear()
        {
            MyEquipList.Clear();
        }
        /*
         * DutStruct[] MyDutManufactureCoefficientsStructArray;
            DriverStruct[] MyManufactureChipsetStructArray;
            int i = 0;
            try
            {
                if (MyEquipList != null)
                {
                    MyEquipList.Clear();
                }
                pDut = (DUT)MyEquipmentManage.Createobject(ProductionTypeName.ToUpper() + "DUT");
                pDut.deviceIndex = 0;
                MyDutManufactureCoefficientsStructArray = GetDutManufactureCoefficients();
                MyManufactureChipsetStructArray = GetManufactureChipsetControl();

                pDut.Initialize(MyDutManufactureCoefficientsStructArray, MyManufactureChipsetStructArray, StrAuxAttribles);
           
                for (i = 0; i < EquipmenNameArray.Count; i++)
                {
                    TestModeEquipmentParameters[] CurrentEquipmentStruct = GetCurrentEquipmentInf(EquipmenNameArray.Values[i].ToString());
                    string[] List = EquipmenNameArray.Keys[i].ToString().Split('_');
                    string CurrentEquipmentName = List[0];
                    string CurrentEquipmentID = List[1];
                    string CurrentEquipmentType = List[2];

                    MyEquipList.Add(EquipmenNameArray.Keys[i].ToString(), (EquipmentBase)MyEquipmentManage.Createobject(CurrentEquipmentName));
                    if (!MyEquipList[EquipmenNameArray.Keys[i].ToString()].Initialize(CurrentEquipmentStruct))
                    {
                        return false;
                    }
                    if (!MyEquipList[EquipmenNameArray.Keys[i].ToString()].Configure())
                    {
                        return false;
                    }
                }
                pEnvironmentcontroll = new EnvironmentalControll(pDut);
                return true;
            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.Message);
                return false;
            }
         */


        #region  Dispose()

        // Public implementation of Dispose pattern callable by consumers.
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }
#endregion
    }
}
