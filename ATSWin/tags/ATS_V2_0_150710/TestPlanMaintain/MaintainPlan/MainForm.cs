using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using ATSDataBase;
using Authority;

namespace Maintain
{    
    public partial class MainForm : Form
    {
        LoginInfoStruct myLoginInfoStruct;
        ToolTip mytip = new ToolTip();
        Excel.Application excelApp;
        long currTestPlanID = -1, currTestCtrlID = -1, currTestModelID = -1, currTestEquipID = -1;
        string myLoginID = "";
        public string IDstrTopoTestPlan = "";
        bool blnIsSQLDB;
        long currTypeID;
        long currPNID;

        DataTable GlobalManufactureCoefficientsDT;
        DataTable GlobalManufactureChipsetControlDT;
        DataTable GlobalManufactureChipsetInitializeDT;
        DataTable TopoMSAEEPROMSetDT;
        DataTable GlobalTypeDT;
        DataTable GlobalMSADefineDT;

        DataIO mySqlIO;

        public static bool ISNeedUpdateflag = false;

        public static string[] ConstTestPlanTables = new string[] { "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter","TopoManufactureConfigInit" };
        public static string[] ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
        public static string[] ConstMSAItemTables = new string[] { "GlobalProductionType", "GlobalMSADefintionInf", "GlobalMSADTable" };
        public static string[] ConstGlobalManufactureMemoryTables = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalManufactureCoefficients", "GlobalManufactureCoefficientsGroup" };
        public static string[] ConstGlobalSpecs = new string[] { "GlobalSpecs", "TopoPNSpecsParams" };   //150430

        public static DataSet TopoToatlDS;
        public static DataSet GlobalTotalDS;

        //更新时顺序为 更新[Add/Edit]主表-->更新[Add/Edit]子表-->删除子表-->删除主表 -->END
        //载入所有TestPlan信息的最后一个生成id EquipPrmtr 和TestPrmtr 可以省略,但是为了方便查询最终数据故保留!

        public static long origIDTestPlan = -1;
        public static long origIDTestCtrl = -1;
        public static long origIDTestModel = -1;
        public static long origIDTestPrmtr = -1;
        public static long origIDTestEquip = -1;
        public static long origIDTestEquipPrmtr = -1;
        public static long origIDMConfigInit = -1;  //150203_1

        public static long mylastIDTestPlan = -1;
        public static long mylastIDTestCtrl = -1;
        public static long mylastIDTestModel = -1;
        public static long mylastIDTestPrmtr = -1;
        public static long mylastIDTestEquip = -1;
        public static long mylastIDTestEquipPrmtr = -1;
        public static long mylastIDMConfigInit = -1;  //150203_1

        //每新增一条记录对应的mynewIDTestPlan=mylastIDTestPlan+1;
        public static long mynewIDTestPlan = 0;
        public static long mynewIDTestCtrl = 0;
        public static long mynewIDTestModel = 0;
        public static long mynewIDTestPrmtr = 0;
        public static long mynewIDTestEquip = 0;
        public static long mynewIDTestEquipPrmtr = 0;
        public static long mynewIDMConfigInit = 0;  //150203_1

        //每删除一条记录对应的myDeletedCountTestPlan +1;
        public static long myDeletedCountTestPlan = 0;
        public static long myDeletedCountTestCtrl = 0;
        public static long myDeletedCountTestModel = 0;
        public static long myDeletedCountTestPrmtr = 0;
        public static long myDeletedCountTestEquip = 0;
        public static long myDeletedCountTestEquipPrmtr = 0;
        public static long myDeletedCountMConfigInit = 0;  //150203_1

        //每新增一条记录对应的myAddCountTestPlan +1;
        public static long myAddCountTestPlan = 0;
        public static long myAddCountTestCtrl = 0;
        public static long myAddCountTestModel = 0;
        public static long myAddCountTestPrmtr = 0;
        public static long myAddCountTestEquip = 0;
        public static long myAddCountTestEquipPrmtr = 0;
        public static long myAddCountMConfigInit = 0;  //150203_1

        //每个表当前是否为新增flag~
        public static bool myTestPlanISNewFlag = false;
        public static bool myTestCtrlISNewFlag = false;
        public static bool myTestModelISNewFlag = false;
        public static bool myTestPrmtrISNewFlag = false;
        public static bool myTestEquipISNewFlag = false;
        public static bool myTestEquipPrmtrISNewFlag = false;
        public static long myMConfigInit = 0;  //150203_1

        public static bool myTestPlanAddOKFlag = true;
        public static bool myTestEquipAddOKFlag = true;
        public static bool myTestEquipPrmtrAddOKFlag = true;
        public static bool myTestCtrlAddOKFlag = true;
        public static bool myTestModelAddOKFlag = true;
        public static bool myTestPrmtrAddOKFlag = true;
        public static bool myMConfigInitAddOKFlag = true;  //150203_1

        //140626_1载入标志位,隐藏显示最方便
        public static bool blnReadable = false;
        public static bool blnWritable = false;
        public static bool blnAddable = false;
        public static bool blnDeletable = false;
        public static bool blnDuplicable = false;
        public static bool blnExportable = false;
        public static bool blnImportable = false;


        public MainForm(LoginInfoStruct pLoginStruct)
        {
            myLoginInfoStruct = pLoginStruct;
            InitializeComponent();
        }

        #region 公共方法
        //*****************************************************************************
        //公共方法 Start>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //*****************************************************************************
        public static bool checkItemLength(string cloumnName, string cloumnValue, int myLength)
        {
            try
            {
                if (cloumnValue.Length > myLength)
                {
                    MessageBox.Show("Length of " + cloumnName + " > " + myLength + ",Pls confirm again!");
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static string getDTColumnInfo(DataTable dt, string CloumnName, string filterString)
        {
            DataRow[] dr = dt.Select(filterString.Trim());
            string ReturnValue = "";
            try
            {
                if (dr.Length == 1)
                {
                    ReturnValue = dr[0][CloumnName].ToString();
                }
                else
                {
                    MessageBox.Show("Error!--> " + dr.Length + " records existed!");
                }
                return ReturnValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ReturnValue;
            }
        }

        public static long getNextTablePIDFromDT(DataTable mydt, string filterString)
        {
            long myID = -1;
            try
            {
                DataRow[] myRows = mydt.Select(filterString);

                if (myRows.Length == 1)
                {
                    myID = System.Convert.ToInt64(myRows[0]["ID"]);
                }
                else
                {
                    MessageBox.Show("Error!!!Query with filterString = " + filterString + " ...Data is not unique" + myRows.Length + " records existed!");
                }

                return myID;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return myID;
            }
        }

        //140516~Delete Rows With Condition
        public static bool DeleteItemForDT(DataTable mydt, string delCondition)
        {
            try
            {
                DataRow[] DelRowS = mydt.Select(delCondition);
                foreach (DataRow dr in DelRowS)
                {
                    dr.Delete();
                }
                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static bool IsExistPID(DataTable dt, string queryCMD)  //140521
        {
            bool MyPIDResult = false;
            try
            {
                DataRow[] myDyRows = dt.Select(queryCMD);
                if (myDyRows.Length > 0)
                {
                    MyPIDResult = true;
                }
                return MyPIDResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return MyPIDResult;
            }

        }

        public static int getNextPIDFromdgv(DataGridView dgv, int RowIndex)
        {
            int myPID = -1;
            try
            {
                if (dgv.CurrentRow  !=null && dgv.CurrentRow.Index != -1) //140710_2
                {
                    myPID = Convert.ToInt32(dgv.Rows[RowIndex].Cells["ID"].Value);
                }
                return myPID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return myPID;
            }

        }

        public static void showTablefilterStrInfo(DataTable dt, DataGridView dgv, string filterStr,string sortStr="")
        {
            try
            {
                dt.DefaultView.RowFilter = filterStr;
                if (sortStr.Length > 0)
                {
                    dt.DefaultView.Sort = sortStr;
                }

                dgv.DataSource = dt.DefaultView;
                if (dgv.Columns.Contains("SEQ"))
                {
                    dgv.Sort(dgv.Columns["SEQ"], ListSortDirection.Ascending);
                }
                
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;                
                MainForm.hideMyIDPID(dgv);
                //MainForm.SetHeadtextToChinese(dgv);
                dgv.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void setDgvCurrCell(DataGridView dgv, string ColumnName, string fieldValue)
        {
            try
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Cells[ColumnName].Value.ToString().ToUpper() == fieldValue.ToString().ToUpper())
                    {
                        if (dgv.Rows[i].Cells[ColumnName].Visible)
                        {
                            dgv.CurrentCell = dgv.Rows[i].Cells[ColumnName];
                        }
                        else
                        {
                            for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                            {
                                if (dgv.Rows[i].Cells[j].Visible)
                                {
                                    dgv.CurrentCell = dgv.Rows[i].Cells[j];
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void SetHeadtextToChinese(DataGridView dgv)
        {
            try
            {
                //141104_1
                //if (dgv.Columns.Contains("IgnoreFlag")) //140530_1
                //{
                //    dgv.Columns["IgnoreFlag"].HeaderText = "是否忽略项目?";
                //}
                //if (dgv.Columns.Contains("Item")) //140530_1
                //{
                //    dgv.Columns["Item"].HeaderText = "项目";
                //}
                //if (dgv.Columns.Contains("ItemValue")) //140530_1
                //{
                //    dgv.Columns["ItemValue"].HeaderText = "内容";
                //}
                //if (dgv.Columns.Contains("ItemName")) //140530_1
                //{
                //    dgv.Columns["ItemName"].HeaderText = "名称";
                //}
                //if (dgv.Columns.Contains("ItemType")) //140530_1
                //{
                //    dgv.Columns["ItemType"].HeaderText = "类型";
                //}
                //if (dgv.Columns.Contains("ItemDescription")) //140530_1
                //{
                //    dgv.Columns["ItemDescription"].HeaderText = "描述";
                //}
                //if (dgv.Columns.Contains("Direction")) //140530_1
                //{
                //    dgv.Columns["Direction"].HeaderText = "输入|输出";
                //}
                //if (dgv.Columns.Contains("SpecMin")) //140530_1
                //{
                //    dgv.Columns["SpecMin"].HeaderText = "规格下限";
                //}
                //if (dgv.Columns.Contains("SpecMax")) //140530_1
                //{
                //    dgv.Columns["SpecMax"].HeaderText = "规格上限";
                //}
                //if (dgv.Columns.Contains("ItemSpecific")) //140530_1
                //{
                //    dgv.Columns["ItemSpecific"].HeaderText = "指定规格";
                //}
                //if (dgv.Columns.Contains("LogRecord")) //140530_1
                //{
                //    dgv.Columns["LogRecord"].HeaderText = "存储调整信息";
                //}
                //if (dgv.Columns.Contains("Failbreak")) //140530_1
                //{
                //    dgv.Columns["Failbreak"].HeaderText = "超出规格停止";
                //}
                //if (dgv.Columns.Contains("DataRecord")) //140530_1
                //{
                //    dgv.Columns["DataRecord"].HeaderText = "测试结果存档";
                //}

                ///////////
                //if (dgv.Columns.Contains("Vcc")) //140530_1
                //{
                //    dgv.Columns["Vcc"].HeaderText = "电压";
                //}
                //if (dgv.Columns.Contains("Temp")) //140530_1
                //{
                //    dgv.Columns["Temp"].HeaderText = "温度设定";
                //}
                //if (dgv.Columns.Contains("USBPort")) //140530_1
                //{
                //    dgv.Columns["USBPort"].HeaderText = "USB端口号";
                //}
                //if (dgv.Columns.Contains("SEQ")) //140530_1
                //{
                //    dgv.Columns["SEQ"].HeaderText = "顺序";
                //}

                //if (dgv.Columns.Contains("Channel")) //140530_1
                //{
                //    dgv.Columns["Channel"].HeaderText = "模块通道号";
                //}
                //if (dgv.Columns.Contains("Pattent")) //140530_1
                //{
                //    dgv.Columns["Pattent"].HeaderText = "码型(PRBS)";
                //}
                //if (dgv.Columns.Contains("DataRate")) //140530_1
                //{
                //    dgv.Columns["DataRate"].HeaderText = "速率";
                //}
                //if (dgv.Columns.Contains("AuxAttribles")) //140530_1
                //{
                //    dgv.Columns["AuxAttribles"].HeaderText = "其它属性";
                //}
                
                ////AppModeID EquipmentList
                //if (dgv.Columns.Contains("AppModeID")) //140530_1
                //{
                //    dgv.Columns["AppModeID"].HeaderText = "程序编号";
                //}

                //if (dgv.Columns.Contains("EquipmentList")) //140530_1
                //{
                //    dgv.Columns["EquipmentList"].HeaderText = "设备列表";
                //}

                ////ADD MSAPrmtr 140703_0 >>>>>>>>>>>>>>>>>>>
                //if (dgv.Columns.Contains("FieldName")) //140530_1
                //{
                //    dgv.Columns["FieldName"].HeaderText = "字段名";
                //}
                //if (dgv.Columns.Contains("SlaveAddress")) //140530_1
                //{
                //    dgv.Columns["SlaveAddress"].HeaderText = "从机地址";
                //}
                //if (dgv.Columns.Contains("StartAddress")) //140530_1
                //{
                //    dgv.Columns["StartAddress"].HeaderText = "起始地址";
                //}
                //if (dgv.Columns.Contains("Length")) //140530_1
                //{
                //    dgv.Columns["Length"].HeaderText = "长度";
                //}
                //if (dgv.Columns.Contains("Format")) //140530_1
                //{
                //    dgv.Columns["Format"].HeaderText = "格式类型";
                //}
                //if (dgv.Columns.Contains("Page")) //140530_1
                //{
                //    dgv.Columns["Page"].HeaderText = "页号";
                //}
                ////ADD MSAPrmtr 140703_0 <<<<<<<<<<<<<<<<<<<

                ////140710_1>>>>>>>>>>>>>>>>>>>>
                //if (dgv.Columns.Contains("RegisterAddress")) //140530_1
                //{
                //    dgv.Columns["RegisterAddress"].HeaderText = "内存地址";
                //}
                //if (dgv.Columns.Contains("Endianness")) //140530_1
                //{
                //    dgv.Columns["Endianness"].HeaderText = "大字节序";
                //}
                //140710_1<<<<<<<<<<<<<<<<<<<<
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        public static void hideMyColumn(DataGridView dgv, string ColumnName)
        {
            try
            {
                if (dgv.Columns.Contains(ColumnName))
                    dgv.Columns[ColumnName].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void hideMyColumn(DataGridView dgv, string[] ColumnArray)
        {
            try
            {
                foreach (string s in ColumnArray)
                {
                    dgv.Columns[s].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public static void hideMyIDPID(DataGridView dgv)
        {
            try
            {
                hideMyColumn(dgv, "ID");
                hideMyColumn(dgv, "PID");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void hideMyRow(DataGridView dgv, int RowIndex)
        {
            try
            {
                dgv.Rows[RowIndex].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void hideMyRow(DataGridView dgv, int[] RowArray)
        {
            try
            {
                foreach (int s in RowArray)
                {
                    dgv.Rows[s].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void setDGVfstColumnValue(DataGridView dgv)
        {
            try
            {
                lock (dgv)
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        dgv.Rows[i].Cells[0].Value = "选择";
                    }
                    dgv.ReadOnly = true;
                    dgv.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //140529获取指定条件下的parameter数目,Topo与Global各自执行和比较EquipParameter的参数是否一致!
        public static int currPrmtrCountExisted(DataTable mydt, string FullfilterString)
        {
            int result = -1;
            try
            {
                DataRow[] myROWS = mydt.Select(FullfilterString);
                result = myROWS.Length;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        public static int getMAXColumnsItem(DataTable dt, string ColumnName, string queryCMD)
        {
            int myMaxValue = 0;
            try
            {
                DataRow[] DRs = dt.Select(queryCMD);

                for (int i = 0; i < DRs.Length; i++)
                {
                    int myValue = Convert.ToInt32(DRs[i][ColumnName]);
                    if (myMaxValue < myValue)
                        myMaxValue = myValue;
                }

                return myMaxValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return myMaxValue;
            }

        }
        
        public static bool ISNotInSpec(string ControlName, string myValue, double LowSpec, double HighSpec)
        {
            bool result = false;
            try
            {
                double CheckValue = Convert.ToDouble(myValue);
                if ((CheckValue <= HighSpec && CheckValue >= LowSpec) == false)
                {
                    MessageBox.Show(ControlName + "Data is invalid! Range:" + LowSpec + "~" + HighSpec);
                    return true;
                }
                return result;
            }
            catch
            {
                MessageBox.Show(ControlName + "Data is invalid! Range:" + LowSpec + "~" + HighSpec);
                return true;
            }
        }

        public static bool ISNotInSpec(string ControlName, string myValue, double OnlyAvalue)
        {
            bool result = false;
            try
            {
                double CheckValue = Convert.ToDouble(myValue);
                if (CheckValue != OnlyAvalue)
                {
                    MessageBox.Show(ControlName + "Data is invalid! Must be: " + OnlyAvalue);
                    return true;
                }
                return result;
            }
            catch
            {
                MessageBox.Show(ControlName + "Data is invalid! Must be: " + OnlyAvalue);
                return true;
            }
        }

        public static bool checkTypeOK(string value, string myType)
        {
            bool result = false;
            try
            {
                if (myType.ToUpper() == "BOOL".ToUpper())
                {
                    Convert.ToBoolean(value);
                    result = true;
                }
                else if (myType.ToUpper() == "BYTE".ToUpper())
                {
                    Convert.ToByte(value);
                    result = true;
                }
                else if (myType.ToUpper() == "DateTime".ToUpper())
                {
                    Convert.ToDateTime(value);
                    result = true;
                }
                else if (myType.ToUpper() == "Double".ToUpper())
                {
                    Convert.ToDouble(value);
                    result = true;
                }
                else if (myType.ToUpper() == "Single".ToUpper())
                {
                    Convert.ToSingle(value);
                    result = true;
                }
                else if (myType.ToUpper() == "INT".ToUpper())
                {
                    Convert.ToUInt32(value);
                    result = true;
                }
                else if (myType.ToUpper() == "LONG".ToUpper())
                {
                    Convert.ToUInt64(value);
                    result = true;
                }
                else if (myType.ToUpper() == "STRING".ToUpper())
                {
                    Convert.ToString(value);
                    result = true;

                }
                else if (myType.ToUpper() == "U16".ToUpper() || myType.ToUpper() == "UInt16".ToUpper())
                {
                    Convert.ToUInt16(value);
                    result = true;

                }
                else if (myType.ToUpper() == "U32".ToUpper() || myType.ToUpper() == "UInt32".ToUpper())
                {
                    Convert.ToUInt32(value);
                    result = true;

                }
                else if (myType.ToUpper() == "U64".ToUpper() || myType.ToUpper() == "UInt64".ToUpper())
                {
                    Convert.ToUInt64(value);
                    result = true;

                }
                else
                {
                    result = true;
                    MessageBox.Show("Not support this type-->" +myType + ",Pls confirm again!");
                }
                return result;
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("The type is invalid \"" + myType + "\"!");        //140604_1        
                return false;
            }

        }
        
        string SetHeadTextToExcel(DataColumn dc)
        {
            try
            {
                return dc.ToString();
                //141104_1
                ////if (dc.ColumnName.ToString().ToUpper() == ("PID".ToUpper())) //140618_0
                ////{
                ////    return "上级项目";
                ////}
                //if (dc.ColumnName.ToString().ToUpper() == ("PN".ToUpper())) //140618_0
                //{
                //    return "机种名称";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("Channels".ToUpper())) //140618_0
                //{
                //    return "通道总数";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("SWVersion".ToUpper())) //140618_0
                //{
                //    return "软件版本";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("HwVersion".ToUpper())) //140618_0
                //{
                //    return "硬件版本";
                //}

                //if (dc.ColumnName.ToString().ToUpper() ==("Item".ToUpper())) //140530_1
                //{
                //    return  "项目";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("ItemValue".ToUpper())) //140530_1
                //{
                //    return "内容";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("ItemName".ToUpper())) //140530_1
                //{
                //    return "项目名称";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("ItemType".ToUpper())) //140530_1
                //{
                //    return "类型";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("ItemDescription".ToUpper())) //140530_1
                //{
                //    return "描述";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("Direction".ToUpper())) //140530_1
                //{
                //    return "输入|输出";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("SpecMin".ToUpper())) //140530_1
                //{
                //    return "规格下限";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("SpecMax".ToUpper())) //140530_1
                //{
                //    return "规格上限";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("ItemSpecific".ToUpper())) //140530_1
                //{
                //    return "指定规格";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("LogRecord".ToUpper())) //140530_1
                //{
                //    return "存储调整信息";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("Failbreak".ToUpper())) //140530_1
                //{
                //    return "超出规格停止";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("DataRecord".ToUpper())) //140530_1
                //{
                //    return "测试结果存档";
                //}

                ///////////
                //if (dc.ColumnName.ToString().ToUpper() ==("Vcc".ToUpper())) //140530_1
                //{
                //    return "电压";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("Temp".ToUpper())) //140530_1
                //{
                //    return "温度设定";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("USBPort".ToUpper())) //140530_1
                //{
                //    return "USB端口号";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("SEQ".ToUpper())) //140530_1
                //{
                //    return "顺序";
                //}

                //if (dc.ColumnName.ToString().ToUpper() == ("Channel".ToUpper())) //140530_1
                //{
                //    return "模块通道号";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("Pattent".ToUpper())) //140530_1
                //{
                //    return "码型(PRBS)";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("DataRate".ToUpper())) //140530_1
                //{
                //    return "速率";
                //}
                //if (dc.ColumnName.ToString().ToUpper() ==("AuxAttribles".ToUpper())) //140530_1
                //{
                //    return "其它属性";
                //}

                //if (dc.ColumnName.ToString().ToUpper() ==("ItemName".ToUpper())) //140530_1
                //{
                //    return "名称";
                //}
                ////AppModeID EquipmentList
                //if (dc.ColumnName.ToString().ToUpper() ==("AppModeID".ToUpper())) //140530_1
                //{
                //    return "程序编号";
                //}

                //if (dc.ColumnName.ToString().ToUpper() ==("EquipmentList".ToUpper())) //140530_1
                //{
                //    return "设备列表";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("Voltages".ToUpper())) //140618_0
                //{
                //    return "电压总数";
                //}
                //if (dc.ColumnName.ToString().ToUpper() == ("Tsensors".ToUpper())) //140618_0
                //{
                //    return "温度总数";
                //}
                //return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        //以DataTable方式获取信息
        DataTable GetTestPlanInfo(string StrTableName, string sqlQueryCmd)
        {
            try
            {
                DataTable mydt = new DataTable(StrTableName);
                string StrSelectconditions = "select * from " + StrTableName + " " + sqlQueryCmd;
                mydt = mySqlIO.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable
                return mydt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        string getAnyDTColumnInfo(DataTable dt, string CloumnName, string filterString)
        {
            DataRow[] dr = dt.Select(filterString.Trim());
            string ReturnValue = "";
            try
            {
                for (int i = 0; i < dr.Length; i++)
                {
                    ReturnValue += dr[i][CloumnName].ToString() + ",";
                }

                return ReturnValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ReturnValue;
            }
        }

        string getPNParamsDRChangeInfo(DataRow myDatarow)
        {
            string ss = "";
            string ss2 = "";
            try
            {
                if (myDatarow.RowState == DataRowState.Added)
                {

                    if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName"].ToString() + ">";
                    }
                    else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item"].ToString() + ">";
                    }
                    else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("SID"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + myDatarow["SID"].ToString()) + ">";
                    }
                    else
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <ID=" + myDatarow["ID"].ToString() + ">";
                    }

                    for (int k = 0; k < myDatarow.ItemArray.Length; k++)
                    {
                        ss2 += myDatarow[k, DataRowVersion.Current].ToString() + ";";
                    }
                    ss2 += "\r\n";
                }
                else if (myDatarow.RowState == DataRowState.Modified)
                {
                    string sss1 = "", sss2 = "";
                    for (int k = 0; k < myDatarow.ItemArray.Length; k++)
                    {
                        if (myDatarow[k, DataRowVersion.Current].ToString() != myDatarow[k, DataRowVersion.Original].ToString())
                        {
                            if (sss1.Length <= 0)
                            {
                                if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("ItemName"))
                                {
                                    sss1 += "Modified--> <ItemName=" + myDatarow["ItemName"].ToString() + ">OriginalData:";
                                }
                                else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("Item"))
                                {
                                    sss1 += "Modified--> <Item=" + myDatarow["Item"].ToString() + ">OriginalData:";
                                }
                                else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("SID"))
                                {
                                    sss1 += "Modified--> <Item=" + MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + myDatarow["SID"].ToString()) +">OriginalData:";
                                }
                                else
                                {
                                    sss1 += "Modified--> <ID=" + myDatarow["ID"].ToString() + ">OriginalData:";
                                }
                            }
                            if (sss2.Length <= 0)
                            {
                                if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("ItemName"))
                                {
                                    sss2 += "Modified--> <ItemName=" + myDatarow["ItemName"].ToString() + ">ModifiedData:";
                                }
                                else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("Item"))
                                {
                                    sss2 += "Modified--> <Item=" + myDatarow["Item"].ToString() + ">ModifiedData:";
                                }
                                else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("SID"))
                                {
                                    sss2 += "Modified--> <Item=" + MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + myDatarow["SID"].ToString()) + ">ModifiedData:";
                                }
                                else
                                {
                                    sss2 += "Modified--> <ID=" + myDatarow["ID"].ToString() + ">ModifiedData:";
                                }
                            }
                            sss1 += "[" + MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Original].ToString() + ";";
                            sss2 += "[" + MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Current].ToString() + ";";
                        }
                    }
                    if (sss1.Length > 0)
                    {
                        ss2 += sss1 + "\r\n";
                    }
                    if (sss2.Length > 0)
                    {
                        ss2 += sss2 + "\r\n";
                    }
                }
                else if (myDatarow.RowState == DataRowState.Deleted)
                {
                    //ss2 += myDatarow.RowState.ToString() + "--> ItemRecord:";
                    if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName", DataRowVersion.Original].ToString() + ">";
                    }
                    else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item", DataRowVersion.Original].ToString() + ">";
                    }
                    else if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Contains("SID"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + myDatarow["SID"].ToString()) + ">";
                    }
                    else
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <ID=" + myDatarow["ID", DataRowVersion.Original].ToString() + ">";
                    }

                    for (int k = 0; k < MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Columns.Count; k++)
                    {
                        ss2 += myDatarow[k, DataRowVersion.Original].ToString() + ";";
                    }
                    ss2 += "\r\n";
                }

                return ss2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ss;
            }
        }

        string getDRChangeInfo(DataRow myDatarow, string tableName)
        {
            string ss = "";
            string ss2 = "";
            try
            {
                if (myDatarow.RowState == DataRowState.Added)
                {

                    if (TopoToatlDS.Tables[tableName].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName"].ToString() + ">";
                    }
                    else if (TopoToatlDS.Tables[tableName].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item"].ToString() + ">";
                    }
                    else
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <ID=" + myDatarow["ID"].ToString() + ">";
                    }

                    for (int k = 0; k < myDatarow.ItemArray.Length; k++)
                    {
                        ss2 += myDatarow[k, DataRowVersion.Current].ToString() + ";";
                    }
                    ss2 += "\r\n";
                }
                else if (myDatarow.RowState == DataRowState.Modified)
                {
                    string sss1 = "", sss2 = "";
                    for (int k = 0; k < myDatarow.ItemArray.Length; k++)
                    {
                        if (myDatarow[k, DataRowVersion.Current].ToString() != myDatarow[k, DataRowVersion.Original].ToString())
                        {
                            if (sss1.Length <= 0)
                            {
                                if (TopoToatlDS.Tables[tableName].Columns.Contains("ItemName"))
                                {
                                    sss1 += "Modified--> <ItemName=" + myDatarow["ItemName"].ToString() + ">OriginalData:";
                                }
                                else if (TopoToatlDS.Tables[tableName].Columns.Contains("Item"))
                                {
                                    sss1 += "Modified--> <Item=" + myDatarow["Item"].ToString() + ">OriginalData:";
                                }
                                else
                                {
                                    sss1 += "Modified--> <ID=" + myDatarow["ID"].ToString() + ">OriginalData:";
                                }
                            }
                            if (sss2.Length <= 0)
                            {
                                if (TopoToatlDS.Tables[tableName].Columns.Contains("ItemName"))
                                {
                                    sss2 += "Modified--> <ItemName=" + myDatarow["ItemName"].ToString() + ">ModifiedData:";
                                }
                                else if (TopoToatlDS.Tables[tableName].Columns.Contains("Item"))
                                {
                                    sss2 += "Modified--> <Item=" + myDatarow["Item"].ToString() + ">ModifiedData:";
                                }
                                else
                                {
                                    sss2 += "Modified--> <ID=" + myDatarow["ID"].ToString() + ">ModifiedData:";
                                }
                            }
                            sss1 += "[" + TopoToatlDS.Tables[tableName].Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Original].ToString() + ";";
                            sss2 += "[" + TopoToatlDS.Tables[tableName].Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Current].ToString() + ";";
                        }
                    }
                    if (sss1.Length > 0)
                    {
                        ss2 += sss1 + "\r\n";
                    }
                    if (sss2.Length > 0)
                    {
                        ss2 += sss2 + "\r\n";
                    }
                    if (ss2.Length > 0)
                    {
                        ss2 += "\r\n";
                    }
                }
                else if (myDatarow.RowState == DataRowState.Deleted)
                {
                    //ss2 += myDatarow.RowState.ToString() + "--> ItemRecord:";
                    if (TopoToatlDS.Tables[tableName].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName", DataRowVersion.Original].ToString() + ">";
                    }
                    else if (TopoToatlDS.Tables[tableName].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item", DataRowVersion.Original].ToString() + ">";
                    }
                    else
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <ID=" + myDatarow["ID", DataRowVersion.Original].ToString() + ">";
                    }

                    for (int k = 0; k < TopoToatlDS.Tables[tableName].Columns.Count; k++)
                    {
                        ss2 += myDatarow[k, DataRowVersion.Original].ToString() + ";";
                    }
                    ss2 += "\r\n";
                }

                return ss2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return ss;
            }
        }

        void writeLogToLocal(string ss)
        {
            FileStream fs;
            if (blnIsSQLDB)
            {
                fs = new FileStream(Application.StartupPath + @"\SQLChangeLogs.txt", FileMode.Append);
            }
            else
            {
                fs = new FileStream(Application.StartupPath + @"\AccdbChangeLogs.txt", FileMode.Append);
            }
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            if (ss.Length > 0)
            {
                sw.WriteLine("<<<<<<<<<<<<<<<<**" + this.Text + "**>>>>>>>>>>>>>>>>");  //150114_0 增加APPVersion+DataSource
                sw.WriteLine("================**" + mySqlIO.GetCurrTime().ToString() + "**================\r\n" + ss);
            }
            sw.Close();
            fs.Close();
        }

        string[] getTopoPNSpecParamsLog(out string[] operationType, out string[] currItem, out string[] childItem) //将分为一个TestPlan一条维护记录!
        {
            int modifyCount = 0, currCount = -1;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            currItem = new string[1] { "" };
            childItem = new string[1] { "" };
            try
            {
                string currPN= "";
                string currPNID = "";
                string toatalSS = "";
                string currOperationType = "";
                string IDstrTopoPNSpecParams="";
                for (int i = 0; i < MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Rows.Count; i++)
                {
                    if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Rows[i].RowState != DataRowState.Unchanged)
                    {
                        if (MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Rows[i].RowState != DataRowState.Added)
                        {
                            IDstrTopoPNSpecParams += MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Rows[i]["ID", DataRowVersion.Original].ToString() + ",";
                        }
                        else  //实际没有Add的资料
                        {
                            IDstrTopoPNSpecParams += MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Rows[i]["ID", DataRowVersion.Current].ToString() + ",";
                        }
                    }
                }

                if (IDstrTopoPNSpecParams.Length > 0)
                {
                    DataRow[] drsParams = MainForm.GlobalTotalDS.Tables["TopoPNSpecsParams"].Select("id in (" + IDstrTopoPNSpecParams + ")", "PID ASC");
                    modifyCount = drsParams.Length;
                    detailLogs = new string[modifyCount];
                    operationType = new string[modifyCount];
                    for (int i = 0; i < modifyCount; i++)
                    {
                        detailLogs[i] = "";
                        operationType[i] = "";
                    }
                    foreach (DataRow dr in drsParams)
                    {
                        if (currPNID != dr["PID"].ToString())
                        {
                            toatalSS = "";
                            currOperationType = "";
                            currPNID = dr["PID"].ToString();
                            currCount++;
                        }
                        else
                        {
                            currOperationType = "";
                        }
                        currPN = "PN=" + getDTColumnInfo(TopoToatlDS.Tables["GlobalProductionName"], "PN", "ID=" + dr["PID"]);

                        string ItemName = "ItemName=" + getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + dr["SID"]);

                        if (dr.RowState == DataRowState.Added)
                        {
                            currOperationType += "Added";
                        }
                        else if (dr.RowState == DataRowState.Deleted)
                        {
                            currOperationType += "Deleted";
                        }
                        else
                        {
                            currOperationType += "Modified";
                        }

                        string ss = getPNParamsDRChangeInfo(dr);
                        if (ss.Length > 0)
                        {                           
                            if (toatalSS.Length == 0)
                            {
                                ss = "*[TopoPNSpecsParams]*:  \r\n" + ss;
                                toatalSS += "<**********************" + currPN + "**********************>\r\n" + ss;
                            }
                            else
                            {
                                toatalSS += ss;
                            }
                            detailLogs[currCount] = toatalSS;
                            operationType[currCount] = currOperationType;
                            currItem[currCount] = currPN;
                            childItem[currCount] = "PNSpecParams";
                        }                        
                    }
                    for (int i = 0; i < modifyCount; i++)
                    {
                        writeLogToLocal(detailLogs[i]);
                    }
                }

                return detailLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return detailLogs;
            }
        }

        string[] getTopoTablesChangeLog(out string[] operationType, out string[] currItem, out string[] childItem) //将分为一个TestPlan一条维护记录!
        {
            int modifyCount = 0, currCount = 0;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            currItem = new string[1] { "" };
            childItem = new string[1] { "" };
            try
            {
                //DateTime t1 = DateTime.Now;               
                //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };

                string toatalSS = "";
                string currOperationType = "";

                for (int ii = 0; ii < TopoToatlDS.Tables["TopoTestPlan"].Rows.Count; ii++)
                {
                    if (TopoToatlDS.Tables["TopoTestPlan"].Rows[ii].RowState != DataRowState.Unchanged)
                    {
                        if (TopoToatlDS.Tables["TopoTestPlan"].Rows[ii].RowState != DataRowState.Added)
                        {
                            IDstrTopoTestPlan += TopoToatlDS.Tables["TopoTestPlan"].Rows[ii]["ID", DataRowVersion.Original].ToString() + ",";
                        }
                        else 
                        {
                            IDstrTopoTestPlan += TopoToatlDS.Tables["TopoTestPlan"].Rows[ii]["ID", DataRowVersion.Current].ToString() + ",";
                        }
                    }
                }
                
                if (IDstrTopoTestPlan.Length > 0)   //141117_1未修改直接点击更新了时报错...
                {
                    DataRow[] drsDelTestPlan = TopoToatlDS.Tables["TopoTestPlan"].Select("id in (" + IDstrTopoTestPlan + ")", "PID ASC", DataViewRowState.Deleted);

                    #region drsTestPlan is not deleted

                    DataRow[] drsTestPlan = TopoToatlDS.Tables["TopoTestPlan"].Select("id in (" + IDstrTopoTestPlan + ")", "PID ASC");
                    modifyCount = drsTestPlan.Length + drsDelTestPlan.Length;
                    detailLogs = new string[modifyCount];
                    operationType = new string[modifyCount];
                    currItem = new string[modifyCount];
                    childItem = new string[modifyCount];

                    foreach (DataRow drTestPlan in drsTestPlan)
                    {
                        toatalSS = "";
                        currOperationType = "";
                        string testPlanPN = "PN=" + getDTColumnInfo(TopoToatlDS.Tables["GlobalProductionName"], "PN", "ID=" + drTestPlan["PID"]);
                        string testPlanName = "";
                       
                        if (drTestPlan.RowState == DataRowState.Added)
                        {
                            currOperationType += "Added";
                            testPlanName = "TestPlan =" + drTestPlan["ItemName"].ToString();                            
                        }
                        else if (drTestPlan.RowState == DataRowState.Deleted)
                        {
                            currOperationType += "Deleted";
                            testPlanName = "TestPlan =" + drTestPlan["ItemName", DataRowVersion.Original].ToString();
                        }
                        else
                        {
                            currOperationType += "Modified";
                            testPlanName = "TestPlan =" + drTestPlan["ItemName",DataRowVersion.Original].ToString();
                        }

                        string ss = getDRChangeInfo(drTestPlan, "TopoTestPlan");    //目前testplan无法被删除!
                        if (ss.Length > 0)
                        {
                            ss = "*[TopoTestPlan]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                        }
                        string ss0 = "";

                        //150203_1
                        #region drsMConfigInit is not deleted ===================
                        DataRow[] drsMConfigInit = TopoToatlDS.Tables["TopoManufactureConfigInit"].Select("PID=" + drTestPlan["ID"]);

                        string ssdrMConfigInit = "";
                        foreach (DataRow drMConfigInit in drsMConfigInit)
                        {
                            ss0 = getDRChangeInfo(drMConfigInit, "TopoManufactureConfigInit");

                            if (ss0.Length > 0)
                            {
                                ssdrMConfigInit +=
                                    "<==SlaveAddress=" + drMConfigInit["SlaveAddress"] + ";Page=" + drMConfigInit["Page"] + ";StartAddress=" + drMConfigInit["StartAddress"] + "==>\r\n" + ss0;
                            }
                        }
                        # endregion

                        #region drsMConfigInit is deleted ===================
                        drsMConfigInit = TopoToatlDS.Tables["TopoManufactureConfigInit"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);

                        foreach (DataRow drMConfigInit in drsMConfigInit)
                        {
                            //drEquip.GetParentRows("1", DataRowVersion.Original);
                            ss0 = getDRChangeInfo(drMConfigInit, "TopoManufactureConfigInit");

                            if (ss0.Length > 0)
                            {
                                ssdrMConfigInit +=
                                    "<==SlaveAddress=" + drMConfigInit["SlaveAddress", DataRowVersion.Original] + ";Page=" + drMConfigInit["Page", DataRowVersion.Original] + ";StartAddress=" + drMConfigInit["StartAddress", DataRowVersion.Original] + "==>\r\n" + ss0;
                            }
                        }
                        if (ssdrMConfigInit.Length > 0)
                        {
                            ss = ss + "**[TopoManufactureConfigInit]**:  \r\n" + ssdrMConfigInit;
                        }
                        # endregion


                        #region drsEquip is not deleted ===================
                        DataRow[] drsEquip = TopoToatlDS.Tables["TopoEquipment"].Select("PID=" + drTestPlan["ID"]);

                        foreach (DataRow drEquip in drsEquip)
                        {
                            ss0 = getDRChangeInfo(drEquip, "TopoEquipment");

                            if (ss0.Length > 0)
                            {
                                ss += "**[TopoEquipment]**:  \r\n" + "<==" + drEquip["ItemName"] + "==>\r\n" + ss0;
                            }

                            string ss1 = "";
                            DataRow[] drsEquipPrmtr = TopoToatlDS.Tables["TopoEquipmentParameter"].Select("PID=" + drEquip["ID"]);
                            foreach (DataRow drEquipPrmtr in drsEquipPrmtr)
                            {
                                ss1 += getDRChangeInfo(drEquipPrmtr, "TopoEquipmentParameter");
                            }
                            if (ss1.Length > 0)
                            {
                                ss += "***[TopoEquipmentParameter]***:\r\n" + "<==" + drEquip["ItemName"] + "==>\r\n" + ss1;
                            }
                        }
                        # endregion

                        #region drsEquip is deleted ===================
                        drsEquip = TopoToatlDS.Tables["TopoEquipment"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);

                        foreach (DataRow drEquip in drsEquip)
                        {
                            //drEquip.GetParentRows("1", DataRowVersion.Original);
                            ss0 = getDRChangeInfo(drEquip, "TopoEquipment");

                            if (ss0.Length > 0)
                            {
                                ss += "**[TopoEquipment]**:\r\n" + "<" + drEquip["ItemName", DataRowVersion.Original] + ">\r\n" + ss0;
                            }

                            string ss1 = "";
                            DataRow[] drsEquipPrmtr = TopoToatlDS.Tables["TopoEquipmentParameter"].Select("PID=" + drEquip["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                            foreach (DataRow drEquipPrmtr in drsEquipPrmtr)
                            {
                                ss1 += getDRChangeInfo(drEquipPrmtr, "TopoEquipmentParameter");
                            }
                            if (ss1.Length > 0)
                            {
                                ss += "***[TopoEquipmentParameter]***:\r\n" + "<==" + drEquip["ItemName", DataRowVersion.Original] + "==>\r\n" + ss1;
                            }
                        }
                        # endregion

                        string ss2 = "";
                        string ss3 = "";
                        string ss4 = "";
                        #region drsCtrl is not deleted ===================
                        DataRow[] drsCtrl = TopoToatlDS.Tables["TopoTestControl"].Select("PID=" + drTestPlan["ID"]);
                        foreach (DataRow drCtrl in drsCtrl)
                        {
                            ss2 = getDRChangeInfo(drCtrl, "TopoTestControl");
                            string testCtrlName = drCtrl["ItemName"].ToString();
                            if (ss2.Length > 0)
                            {
                                ss += "**[TopoTestControl]**:\r\n" + "<==" + drCtrl["ItemName"] + "==>\r\n" + ss2;
                            }

                            #region drsModel not deleted ===================

                            DataRow[] drsModel = TopoToatlDS.Tables["TopoTestModel"].Select("PID=" + drCtrl["ID"], "PID ASC");
                            foreach (DataRow drModel in drsModel)
                            {
                                ss3 = getDRChangeInfo(drModel, "TopoTestModel");
                                if (ss3.Length > 0)
                                {
                                    ss += "***[TopoTestModel]***:\r\n" + "<==" + testCtrlName + "__" + drModel["ItemName"] + "==>\r\n" + ss3;
                                }

                                #region drsModelPrmtr
                                //Data is not Deleted========================
                                DataRow[] drsModelPrmtr = TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + drModel["ID"], "PID ASC");
                                foreach (DataRow drModelPrmtr in drsModelPrmtr)
                                {

                                    ss4 += getDRChangeInfo(drModelPrmtr, "TopoTestParameter");
                                }
                                if (ss4.Length > 0)
                                {
                                    ss += "****[TopoTestParameter]****:\r\n" + "<==" + testCtrlName +"__"+ drModel["ItemName"] + "==>\r\n" + ss4;
                                }
                                #endregion

                                ss3 = "";   //执行完一次Model后清空内容
                                ss4 = "";   //执行完一次Model后清空内容
                            }
                            #endregion

                            #region drsModel Deleted=======================

                            drsModel = TopoToatlDS.Tables["TopoTestModel"].Select("PID=" + drCtrl["ID"], "PID ASC", DataViewRowState.Deleted);
                            foreach (DataRow drModel in drsModel)
                            {
                                ss3 = getDRChangeInfo(drModel, "TopoTestModel");
                                if (ss3.Length > 0)
                                {
                                    ss += "***[TopoTestModel]***:\r\n" + "<==" + testCtrlName + "__" + drModel["ItemName", DataRowVersion.Original] + "==>\r\n" + ss3;

                                }

                                #region drsModelPrmtr

                                //Data Deleted===============================
                                DataRow[] drsModelPrmtr = TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + drModel["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                                foreach (DataRow drModelPrmtr in drsModelPrmtr)
                                {
                                    ss4 += getDRChangeInfo(drModelPrmtr, "TopoTestParameter");
                                }
                                if (ss4.Length > 0)
                                {
                                    ss += "****[TopoTestParameter]****:\r\n" + "<==" + testCtrlName + "__" + drModel["ItemName", DataRowVersion.Original] + "==>\r\n" + ss4;
                                }
                                #endregion

                                ss3 = "";   //执行完一次Model后清空内容
                                ss4 = "";   //执行完一次Model后清空内容
                            }
                            #endregion
                            ss2 = "";       //执行完一次Ctrl后清空内容             
                        }
                        #endregion

                        #region drsCtrl is deleted ===================

                        drsCtrl = TopoToatlDS.Tables["TopoTestControl"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drCtrl in drsCtrl)
                        {
                            ss2 = getDRChangeInfo(drCtrl, "TopoTestControl");
                            string testCtrlName = drCtrl["ItemName", DataRowVersion.Original].ToString();
                            if (ss2.Length > 0)
                            {
                                ss += "**[TopoTestControl]**:\r\n" + "<==" + drCtrl["ItemName", DataRowVersion.Original] + "==>\r\n" + ss2;
                            }

                            #region drsModel Deleted=======================

                            DataRow[] drsModel = TopoToatlDS.Tables["TopoTestModel"].Select("PID=" + drCtrl["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                            foreach (DataRow drModel in drsModel)
                            {
                                ss3 = getDRChangeInfo(drModel, "TopoTestModel");
                                if (ss3.Length > 0)
                                {
                                    ss += "***[TopoTestModel]***:\r\n" + "<==" + testCtrlName + "__" + drModel["ItemName", DataRowVersion.Original] + "==>\r\n" + ss3;
                                }

                                #region drsModelPrmtr

                                //Data Deleted===============================
                                DataRow[] drsModelPrmtr = TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + drModel["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                                foreach (DataRow drModelPrmtr in drsModelPrmtr)
                                {
                                    ss4 += getDRChangeInfo(drModelPrmtr, "TopoTestParameter");

                                }
                                if (ss4.Length > 0)
                                {
                                    ss += "****[TopoTestParameter]****:\r\n" + "<==" + testCtrlName + "__" + drModel["ItemName", DataRowVersion.Original] + "==>\r\n" + ss4;
                                }
                                #endregion

                                ss3 = "";   //执行完一次Model后清空内容
                                ss4 = "";   //执行完一次Model后清空内容
                            }
                            #endregion
                            ss2 = "";       //执行完一次Ctrl后清空内容             
                        }

                        #endregion
                        if (ss.Length > 0)
                        {
                            toatalSS += "<**********************" + testPlanPN +
                                ":" + testPlanName + "**********************>\r\n" + ss + "\r\n";
                            //141117_1 deleted ================**" + mySqlIO.GetCurrTime().ToString() + "**================\r\n
                        }

                        detailLogs[currCount] = toatalSS;

                        if (toatalSS.Length > 0)
                        {
                            operationType[currCount] = currOperationType;
                            currItem[currCount] = testPlanPN;
                            childItem[currCount] = testPlanName;
                        }
                        else
                        {
                            operationType[currCount] = "";
                            currItem[currCount] = "";
                            childItem[currCount] = "";
                        }

                        //writeLogToLocal(operationType[currCount]);
                        writeLogToLocal(detailLogs[currCount]);
                        currCount++;

                    }
                    #endregion


                    #region drsTestPlan is deleted
                    drsTestPlan = TopoToatlDS.Tables["TopoTestPlan"].Select("id in (" + IDstrTopoTestPlan + ")", "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drTestPlan in drsTestPlan)
                    {
                        toatalSS = "";
                        currOperationType = "";

                        string testPlanPN = "PN=" + getDTColumnInfo(TopoToatlDS.Tables["GlobalProductionName"], "PN", "ID=" + drTestPlan["PID", DataRowVersion.Original]);
                        string testPlanName = "TestPlan =" + drTestPlan["ItemName", DataRowVersion.Original].ToString();

                        currOperationType += "Deleted";

                        string ss = getDRChangeInfo(drTestPlan, "TopoTestPlan");    //目前testplan无法被删除!;
                        if (ss.Length > 0)
                        {
                            ss = "*[TopoTestPlan]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                        }

                        string ss0 = "";
                        //150203_1
                        #region drsMConfigInit is deleted ===================
                        DataRow[] drsMConfigInit = TopoToatlDS.Tables["TopoManufactureConfigInit"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                        string ssdrMConfigInit = "";
                        foreach (DataRow drMConfigInit in drsMConfigInit)
                        {
                            //drEquip.GetParentRows("1", DataRowVersion.Original);
                            ss0 = getDRChangeInfo(drMConfigInit, "TopoManufactureConfigInit");

                            if (ss0.Length > 0)
                            {
                                ssdrMConfigInit +=
                                    "<==SlaveAddress=" + drMConfigInit["SlaveAddress", DataRowVersion.Original] + ";Page=" + drMConfigInit["Page", DataRowVersion.Original] + ";StartAddress=" + drMConfigInit["StartAddress", DataRowVersion.Original] + "==>\r\n" + ss0;
                            }
                        }
                        if (ssdrMConfigInit.Length > 0)
                        {
                            ss = ss + "**[TopoManufactureConfigInit]**:  \r\n" + ssdrMConfigInit;
                        }

                        # endregion

                        #region drsEquip is deleted ===================
                        DataRow[] drsEquip = TopoToatlDS.Tables["TopoEquipment"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);

                        foreach (DataRow drEquip in drsEquip)
                        {
                            ss0 = getDRChangeInfo(drEquip, "TopoEquipment");

                            if (ss0.Length > 0)
                            {
                                ss += "**[TopoEquipment]**:\r\n" + "<==" + drEquip["ItemName", DataRowVersion.Original] + "==>\r\n" + ss0;
                            }

                            string ss1 = "";
                            DataRow[] drsEquipPrmtr = TopoToatlDS.Tables["TopoEquipmentParameter"].Select("PID=" + drEquip["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                            foreach (DataRow drEquipPrmtr in drsEquipPrmtr)
                            {
                                ss1 += getDRChangeInfo(drEquipPrmtr, "TopoEquipmentParameter");
                            }
                            if (ss1.Length > 0)
                            {
                                ss += "****[TopoEquipmentParameter]****:\r\n" + "<==" + drEquip["ItemName", DataRowVersion.Original] + "==>\r\n" + ss1;
                            }
                        }
                        # endregion

                        string ss2 = "";
                        string ss3 = "";
                        string ss4 = "";

                        #region drsCtrl is deleted ===================
                        DataRow[] drsCtrl = TopoToatlDS.Tables["TopoTestControl"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drCtrl in drsCtrl)
                        {
                            ss2 = getDRChangeInfo(drCtrl, "TopoTestControl");
                            if (ss2.Length > 0)
                            {
                                ss += "**[TopoTestControl]**:\r\n" + "<==" + drCtrl["ItemName", DataRowVersion.Original] + "==>\r\n" + ss2;
                            }

                            #region drsModel Deleted=======================

                            DataRow[] drsModel = TopoToatlDS.Tables["TopoTestModel"].Select("PID=" + drCtrl["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                            foreach (DataRow drModel in drsModel)
                            {
                                ss3 = getDRChangeInfo(drModel, "TopoTestModel");
                                if (ss3.Length > 0)
                                {
                                    ss += "***[TopoTestModel]***:\r\n" + "<==" + drModel["ItemName", DataRowVersion.Original] + "==>\r\n" + ss0;

                                    ss += ss3;
                                }

                                #region drsModelPrmtr

                                //Data Deleted===============================
                                DataRow[] drsModelPrmtr = TopoToatlDS.Tables["TopoTestParameter"].Select("PID=" + drModel["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                                foreach (DataRow drModelPrmtr in drsModelPrmtr)
                                {
                                    ss4 = getDRChangeInfo(drModelPrmtr, "TopoTestParameter");
                                }
                                if (ss4.Length > 0)
                                {
                                    ss += "****[TopoTestParameter]****:\r\n" + "<==" + drModel["ItemName", DataRowVersion.Original] + "==>\r\n" + ss0;
                                    ss += ss4;
                                }
                                #endregion

                                ss3 = "";   //执行完一次Model后清空内容
                                ss4 = "";   //执行完一次Model后清空内容
                            }
                            #endregion
                            ss2 = "";       //执行完一次Ctrl后清空内容             
                        }
                        #endregion
                        if (ss.Length > 0)
                        {
                            toatalSS += "<**********************" + testPlanPN +
                                ":" + testPlanName + "**********************>\r\n" + ss + "\r\n";
                            //141117_1 deleted ================**" + mySqlIO.GetCurrTime().ToString() + "**================\r\n

                        }
                        detailLogs[currCount] = toatalSS;

                        if (toatalSS.Length > 0)
                        {
                            operationType[currCount] = currOperationType;
                            currItem[currCount] = testPlanPN;
                            childItem[currCount] = testPlanName;
                        }
                        else
                        {
                            operationType[currCount] = "";
                            currItem[currCount] = "";
                            childItem[currCount] = "";
                        }

                        //writeLogToLocal(operationType[currCount]);
                        writeLogToLocal(detailLogs[currCount]);
                        currCount++;
                    }

                    #endregion

                    //DateTime t2 = DateTime.Now;
                    //TimeSpan ts = t1.Subtract(t2).Duration();
                    //MessageBox.Show(" Use:" + ts.Minutes.ToString() + " Minutes and" + ts.Seconds.ToString() + "." + ts.Milliseconds.ToString() + " Seconds");
                }
                return detailLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return detailLogs;
            }
        }

        string getTopoTablesChanges() //更新后方可调用,否则datatable的RowState会被清除
        {
            string SS = "==User :" + myLoginInfoStruct.UserName + " Operation Logs at " + DateTime.Now.ToString() + " ==\n\r";
            try
            {
                for (int i = 1; i < TopoToatlDS.Tables.Count; i++)
                {
                    if (TopoToatlDS.Tables[i].GetChanges() != null)
                    {
                        SS += "**[" + TopoToatlDS.Tables[i].TableName + "]**\r\n";
                        DataTable myDeletedDt = new DataTable();
                        DataTable myAddDt = new DataTable();
                        DataTable myChangeDt = new DataTable();
                        myDeletedDt = TopoToatlDS.Tables[i].GetChanges(DataRowState.Deleted);
                        myAddDt = TopoToatlDS.Tables[i].GetChanges(DataRowState.Added);
                        myChangeDt = TopoToatlDS.Tables[i].GetChanges(DataRowState.Modified);

                        #region 每行的资料
                        if (myChangeDt != null)
                        {
                            for (int j = 0; j < myChangeDt.Rows.Count; j++)
                            {
                                //DataRow dataRow = TopoToatlDS.Tables[i].Rows[j];
                                DataRow dataRow = myChangeDt.Rows[j];
                                string ss1 = "";
                                string ss2 = "";

                                for (int k = 0; k < dataRow.ItemArray.Length; k++)
                                {
                                    if (dataRow[k, DataRowVersion.Current].ToString() != dataRow[k, DataRowVersion.Original].ToString())
                                    {
                                        if (ss1.Length <= 0) ss1 += "OriginalData:" + "ID=" + dataRow["ID"].ToString() + ",";
                                        if (ss2.Length <= 0) ss2 += "New     Data:" + "ID=" + dataRow["ID"].ToString() + ",";
                                        ss1 += TopoToatlDS.Tables[i].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Original].ToString() + ";";
                                        ss2 += TopoToatlDS.Tables[i].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Current].ToString() + ";";
                                    }
                                }
                                if (ss1.Length > 0)
                                {
                                    SS += ss1 + "\r\n";
                                }
                                if (ss2.Length > 0)
                                {
                                    SS += ss2 + "\r\n";
                                }
                            }
                        }
                        if (myDeletedDt != null)
                        {
                            for (int j = 0; j < myDeletedDt.Rows.Count; j++)
                            {
                                string ss1 = "Data Deleted:ID=";
                                DataRow dataRow = myDeletedDt.Rows[j];
                                ;
                                for (int k = 0; k < myDeletedDt.Columns.Count; k++)
                                {
                                    ss1 += dataRow[k, DataRowVersion.Original].ToString() + ";";
                                }

                                SS += ss1 + "\r\n";
                            }
                        }
                        if (myAddDt != null)
                        {
                            for (int j = 0; j < myAddDt.Rows.Count; j++)
                            {
                                string ss2 = "Data Added:ID=";
                                DataRow dataRow = myAddDt.Rows[j];
                                for (int k = 0; k < dataRow.ItemArray.Length; k++)
                                {
                                    ss2 += dataRow[k, DataRowVersion.Current].ToString() + ";";
                                }
                                SS += ss2 + "\r\n";
                            }
                        }
                        //else if (dataRow.RowState == DataRowState.Detached)
                        //{
                        //    string ss1 = "未保存的Detached资料: ";

                        //    for (int k = 0; k < dataRow.ItemArray.Length; k++)
                        //    {
                        //        ss1 += TopoToatlDS.Tables[i].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Original].ToString() + " ;";                                    
                        //    }                            
                        //    SS += ss1 + "\r\n";
                        //}                
                    }
                        #endregion
                    //MessageBox.Show(SS);
                    //TopoToatlDS.Tables[i].AcceptChanges();
                }
                return SS;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return SS;
            }
        }

        //*****************************************************************************
        //公共方法 END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //*****************************************************************************

        #endregion

        string updateUserLoginInfo()
        {
            string myID = "";
            try
            {
                DataTable userLoginInfoDt = mySqlIO.GetDataTable("select * from UserLoginInfo", "UserLoginInfo");
                DataRow dr = userLoginInfoDt.NewRow();
                string hostname = System.Net.Dns.GetHostName(); //主机
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                string IP4 = "";
                for (int i = 0; i < ipEntry.AddressList.Length; i++)
                {
                    if (ipEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IP4 = ipEntry.AddressList[i].ToString();
                        break;
                    }
                }
                //string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                //string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
                string currTime = mySqlIO.GetCurrTime().ToString();
                dr["UserName"] = myLoginInfoStruct.UserName;
                dr["LoginOntime"] = currTime;
                dr["LoginOfftime"] = "2000-1-1 12:00:00";
                dr["Apptype"] = Application.ProductName;
                dr["LoginInfo"] = IP4;
                dr["OPLogs"] = "";
                userLoginInfoDt.Rows.Add(dr);
                mySqlIO.UpdateDataTable("select * from UserLoginInfo", userLoginInfoDt);
                myID = mySqlIO.GetDataTable("select * from UserLoginInfo where LoginOntime='" + currTime + "' and UserName ='" + myLoginInfoStruct.UserName + "'", "UserLoginInfo").Rows[0]["ID"].ToString();

                return myID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return mySqlIO.GetLastInsertData("UserLoginInfo").ToString();
            }
        }

        /// <summary>
        /// PNInfo_Load
        /// 1.需要将目前界面已有信息清空...
        /// 2.载入Server中已有的GloblaType
        /// 3.载入当前Server 的各个Topo表中最后一笔插入记录!
        /// 4.获取所有相关表的信息![Global/Topo]其它子界面直接修改当前表资料!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PNInfo_Load(object sender, EventArgs e)
        {
            try
            {
                blnIsSQLDB = myLoginInfoStruct.blnISDBSQLserver;
                
                if (blnIsSQLDB)
                {
                    ValidationRule pValidationRule=new ValidationRule();
                    blnReadable = pValidationRule.CheckLoginAccess(LoginAppName.MaintainATSPlan, CheckAccess.ViewATSPlan, myLoginInfoStruct.myAccessCode);
                    blnWritable = pValidationRule.CheckLoginAccess(LoginAppName.MaintainATSPlan, CheckAccess.MofifyATSPlan, myLoginInfoStruct.myAccessCode);
                    blnAddable = pValidationRule.CheckLoginAccess(LoginAppName.MaintainATSPlan, CheckAccess.AddATSPlan, myLoginInfoStruct.myAccessCode);
                    blnDeletable = pValidationRule.CheckLoginAccess(LoginAppName.MaintainATSPlan, CheckAccess.DeleteATSPlan, myLoginInfoStruct.myAccessCode);
                    blnDuplicable = pValidationRule.CheckLoginAccess(LoginAppName.MaintainATSPlan, CheckAccess.DuplicateATSPlan, myLoginInfoStruct.myAccessCode);
                    blnExportable = pValidationRule.CheckLoginAccess(LoginAppName.MaintainATSPlan, CheckAccess.ExportATSPlan, myLoginInfoStruct.myAccessCode);
                    blnImportable = pValidationRule.CheckLoginAccess(LoginAppName.MaintainATSPlan, CheckAccess.ImportATSPlan, myLoginInfoStruct.myAccessCode);
                    
                    if ((blnReadable || blnWritable || blnAddable
                         || blnDeletable || blnDuplicable || blnExportable || blnImportable)==false)
                    {
                        MessageBox.Show("Access denied!Current user :" + myLoginInfoStruct.UserName + "  is not a MaintainUser!");
                        this.Close();
                    }

                    mySqlIO = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);   //140918_0 //140722_2   //140912_0

                    myLoginID = updateUserLoginInfo();
                    this.Text = Application.ProductName + " Ver:" + Application.ProductVersion + "(DataSource=" + myLoginInfoStruct.DBName + ")";
                }
                else
                {
                    mySqlIO = new AccessManager(myLoginInfoStruct.AccessFilePath); 
                    blnReadable = true;
                    blnWritable = true;
                    blnAddable = true;
                    blnDeletable = true;
                    blnDuplicable = true;
                    blnExportable = true;
                    blnImportable = true;
                    this.Text = Application.ProductName + " Ver:" + Application.ProductVersion + "(DataSource=" + myLoginInfoStruct.AccessFilePath + ")";
                }
                this.btnConfigSpec.Visible = blnWritable;
                tsmRefresh.Visible = (blnReadable ? true : false);
                tsmCancel.Visible = (blnWritable ? true : false);
                tsmEditTestPlan.Visible = (blnWritable ? true : false);
                tsmUpdate.Visible = (blnWritable ? true : false);
                tsmAddTestPlan.Visible = (blnAddable ? true : false);
                tsmDeleteTestPlan.Visible = (blnDeletable ? true : false);
                tsmCopyPlan.Visible = (blnDuplicable ? true : false);
                tsmExportPlan.Visible = (blnExportable ? true : false);
                tsmLoadExcel.Visible = (blnImportable ? true : false);
                if (this.btnConfigSpec.Visible)
                {
                    this.cklTestPlan.Location = new Point(9, 103);
                    this.cklTestPlan.Size = new Size(184, 324);
                }
                else
                {
                    this.cklTestPlan.Location = new Point(9, 71);
                    this.cklTestPlan.Size = new Size(184, 356);
                }

                formLoad();

                timerDate.Enabled = true;
                ISNeedUpdateflag = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void ShowMyTip()
        {
            try
            {
                mytip.SetToolTip(this.ssrRunMsg, sslRunMsg.Text);
                mytip.SetToolTip(btnShowMSADefine, "show MSADefine info");
                mytip.SetToolTip(btnShowMemory, "show other Info :Memory,ChipSet,ChipControl,etc...");

                mytip.SetToolTip(cboPN, "ProductionName");
                mytip.SetToolTip(cboItemType, "ProductionType");
                mytip.SetToolTip(cklTestPlan, "TestPlan list of current PN");
                mytip.ShowAlways = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void formLoad()
        {
            try
            {
                DateTime t1 = DateTime.Now;
                this.cboItemType.Items.Clear();
                this.cboItemType.Text = "";
                initCboPN();
                btnShowMSADefine.Enabled = false;
                btnShowMemory.Enabled = false;
                myTestPlanAddOKFlag = true;
                myTestEquipAddOKFlag = true;
                myTestEquipPrmtrAddOKFlag = true;
                myTestCtrlAddOKFlag = true;
                myTestModelAddOKFlag = true;
                myTestPrmtrAddOKFlag = true;

                this.tsuserInfo.Text = "User:" + myLoginInfoStruct.UserName;

                string StrTableName = "GlobalProductionType";
                string StrSelectconditions = " order by ID";
                GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                {
                    if (GlobalTypeDT.Columns.Contains("IgnoreFlag"))    //150411_0
                    {
                        if (!Convert.ToBoolean(GlobalTypeDT.Rows[i]["IgnoreFlag"]))
                        {
                            this.cboItemType.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                            btnShowMSADefine.Enabled = true;
                        }
                    }
                    else
                    {
                        this.cboItemType.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                        btnShowMSADefine.Enabled = true;
                    }
                }

                //载入当前Server 的各表中最后一笔插入记录!
                origIDTestPlan = mySqlIO.GetLastInsertData(ConstTestPlanTables[1]);
                origIDTestCtrl = mySqlIO.GetLastInsertData(ConstTestPlanTables[2]);
                origIDTestModel = mySqlIO.GetLastInsertData(ConstTestPlanTables[3]);
                origIDTestPrmtr = mySqlIO.GetLastInsertData(ConstTestPlanTables[4]);
                origIDTestEquip = mySqlIO.GetLastInsertData(ConstTestPlanTables[5]);
                origIDTestEquipPrmtr = mySqlIO.GetLastInsertData(ConstTestPlanTables[6]);
                origIDMConfigInit = mySqlIO.GetLastInsertData(ConstTestPlanTables[7]);  //150203_1

                mylastIDTestPlan = origIDTestPlan;
                mylastIDTestCtrl = origIDTestCtrl;
                mylastIDTestModel = origIDTestModel;
                mylastIDTestPrmtr = origIDTestPrmtr;
                mylastIDTestEquip = origIDTestEquip;
                mylastIDTestEquipPrmtr = origIDTestEquipPrmtr;
                mylastIDMConfigInit = origIDMConfigInit;    //150203_1

                sslRunMsg.Text = "IDPlan =" + mylastIDTestPlan +
                       ";IDCtrl =" + mylastIDTestCtrl +
                       ";IDModel =" + mylastIDTestModel +
                       ";IDrmtr =" + mylastIDTestPrmtr +
                       ";IDEquip =" + mylastIDTestEquip +
                       ";IDEqPrmtr =" + mylastIDTestEquipPrmtr
                       + ";IDMCInit = " + mylastIDMConfigInit; //150203_1
                
                InitinalALLTablesInfo();   //获取所有表的信息!
                ShowMyTip();
                writeTimeToLocal("formloadElapseTime", t1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 获取Global/Topo表资料
        /// </summary>
        void InitinalALLTablesInfo()
        {
            try
            {
                TopoToatlDS = new DataSet("TopoToatlDS");
                GlobalTotalDS = new DataSet("GlobalTotalDS");

                for (int i = 0; i < ConstTestPlanTables.Length; i++)
                {
                    TopoToatlDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " order by PID,ID"));    //150114_0
                }
                for (int i = 0; i < ConstGlobalListTables.Length; i++)
                {
                    GlobalTotalDS.Tables.Add(GetTestPlanInfo(ConstGlobalListTables[i], ""));
                }

                for (int i = 0; i < ConstGlobalSpecs.Length; i++)
                {
                    GlobalTotalDS.Tables.Add(GetTestPlanInfo(ConstGlobalSpecs[i], ""));
                }

                for (int i = 0; i < GlobalTotalDS.Tables.Count; i++)
                {
                    GlobalTotalDS.Tables[i].PrimaryKey = new DataColumn[] { GlobalTotalDS.Tables[i].Columns["ID"] };
                }

                TopoToatlDS.Tables[1].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[1].Columns["ID"] };
                TopoToatlDS.Tables[2].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[2].Columns["ID"] };
                TopoToatlDS.Tables[3].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[3].Columns["ID"] };
                TopoToatlDS.Tables[4].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[4].Columns["ID"] };
                TopoToatlDS.Tables[5].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[5].Columns["ID"] };
                TopoToatlDS.Tables[6].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[6].Columns["ID"] };
                TopoToatlDS.Tables[7].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[7].Columns["ID"] };    //150203_1

                //TopoToatlDS.Tables[1].Columns["ItemName"].Unique = true; //字段唯一
                //TopoToatlDS.Tables[1].Columns["ItemName"].AllowDBNull = false;// 不允许为空
                //TopoToatlDS.Tables[1].Columns["ItemName"].AutoIncrement = false;//自动递增

                TopoToatlDS.Relations.Add("relation1", TopoToatlDS.Tables[0].Columns["id"], TopoToatlDS.Tables[1].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation2", TopoToatlDS.Tables[1].Columns["id"], TopoToatlDS.Tables[2].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation3", TopoToatlDS.Tables[2].Columns["id"], TopoToatlDS.Tables[3].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation4", TopoToatlDS.Tables[3].Columns["id"], TopoToatlDS.Tables[4].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation5", TopoToatlDS.Tables[1].Columns["id"], TopoToatlDS.Tables[5].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation6", TopoToatlDS.Tables[5].Columns["id"], TopoToatlDS.Tables[6].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation7", TopoToatlDS.Tables[1].Columns["id"], TopoToatlDS.Tables[7].Columns["pid"]);  //150203_1                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void initCboPN()
        {
            try
            {
                initcklTestPlan();

                this.cboPN.Items.Clear();
                cboPN.Text = "";
                btnShowMemory.Enabled = false;
                tabPNtype.Enabled = false;

                GlobalManufactureCoefficientsDT = null; 
                GlobalManufactureChipsetControlDT = null;
                GlobalManufactureChipsetInitializeDT = null;
                TopoMSAEEPROMSetDT = null;

                tsmExportPlan.Enabled = false;  
                //tsmCopyPlan.Enabled = false;    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void initcklTestPlan()
        {
            try
            {
                this.cklTestPlan.Items.Clear();
                dgvTestCtrl.DataSource = null;
                dgvTestEquip.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            currTypeID = -999;
            try
            {
                if (cboItemType.SelectedIndex != -1)
                {
                    mytip.SetToolTip(cboItemType, "ProductionType=" + cboItemType.Text);
                    
                    for (int i = 0; i < this.GlobalTypeDT.Rows.Count; i++)
                    {
                        if (GlobalTypeDT.Rows[i]["ItemName"].ToString().ToUpper() == cboItemType.Text.ToString().ToUpper())
                        {
                            currTypeID = Convert.ToInt64(GlobalTypeDT.Rows[i]["ID"]);
                            break;
                        }
                    }

                    if (currTypeID == -999)
                    {
                        MessageBox.Show("Data not existed :" + cboItemType.Text.ToString().ToUpper() + "!");
                        initCboPN();
                        return;
                    }
                    else
                    {
                        
                        this.cboPN.Items.Clear();
                        string sqlCondition = "PID=" + currTypeID;

                        DataRow[] mrDRs = MainForm.TopoToatlDS.Tables["GlobalProductionName"].Select(sqlCondition);
                        for (int i = 0; i < mrDRs.Length; i++)
                        {
                            if (MainForm.TopoToatlDS.Tables["GlobalProductionName"].Columns.Contains("IgnoreFlag"))    //150411_0
                            {
                                if (!Convert.ToBoolean(mrDRs[i]["IgnoreFlag"]))
                                {
                                    cboPN.Items.Add(mrDRs[i]["PN"].ToString());
                                }
                            }
                            else
                            {
                                cboPN.Items.Add(mrDRs[i]["PN"].ToString());                                
                            }
                        }
                        if (cboPN.Items.Count > 0)
                        {
                            cboPN.Enabled = true;
                            btnShowMemory.Enabled = true;
                            cboPN.Text = cboPN.Items[0].ToString();
                        }
                        else
                        {
                            initCboPN();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a Type first!");
                    initCboPN();
                }
            }
            catch (Exception ex)
            {
                initCboPN();
                MessageBox.Show(" Error,Pls Check Again \n" + ex.ToString());
            }
        }

        private void cboPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            currPNID = -999;
            try
            {
                if (cboPN.SelectedIndex != -1)
                {
                    mytip.SetToolTip(cboPN, "ProductionName=" + cboPN.Text);
                    string sqlCondition = "PID=" + currTypeID ;                    
                    DataRow[] mrDRs = MainForm.TopoToatlDS.Tables["GlobalProductionName"].Select(sqlCondition);
                    for (int i = 0; i < mrDRs.Length; i++)
                    {
                        if (mrDRs[i]["PN"].ToString().ToUpper() == cboPN.Text.ToString().ToUpper())
                        {
                            currPNID = Convert.ToInt64(mrDRs[i]["ID"]);
                            break;
                        }
                    }

                    if (currPNID == -999)
                    {
                        MessageBox.Show("Data not existed :" + cboPN.Text.ToString().ToUpper() + "!");
                        initcklTestPlan();
                        return;
                    }
                    else
                    {
                        btnConfigSpec.Enabled = true;
                        RefreshTestPlanList();
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a PN first!");
                    initcklTestPlan();
                }
            }
            catch (Exception ex)
            {
                this.cklTestPlan.Items.Clear();
                MessageBox.Show(" Error,Pls Check Again!\n" + ex.ToString());
            }
        }

        private void cklTestPlan_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                cklTestPlan.CheckOnClick = true;            
                if (cklTestPlan.CheckedItems.Count > 0)
                {
                    for (int i = 0; i < cklTestPlan.Items.Count; i++)
                    {
                        if (i != e.Index)
                        {
                            this.cklTestPlan.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked);
                        }
                    }
                }
                else
                {
                    e.NewValue = CheckState.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cklTestPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cklTestPlan.SelectedIndex != -1)
                {                    
                    currPNID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["GlobalProductionName"], "ID", "PN='" + cboPN.Text.ToString() + "'"));
                    currTestPlanID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestPlan"], "ID", "ItemName='" + cklTestPlan.SelectedItem.ToString() + "' and PID=" + currPNID));
                    MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoTestControl"], dgvTestCtrl, "PID=" + currTestPlanID);
                    MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoEquipment"], dgvTestEquip, "PID=" + currTestPlanID);   
                    MainForm.showTablefilterStrInfo(MainForm.TopoToatlDS.Tables["TopoManufactureConfigInit"], dgvMConfigInit, "PID=" + currTestPlanID,"SlaveAddress,Page,StartAddress");   //150203_1
                        

                    this.tabPNtype.SelectedIndex = this.tabPNtype.TabCount - 1;                    
                    
                    resizeDGV(dgvTestCtrl);
                    resizeDGV(dgvTestEquip);
                    resizeDGV(dgvMConfigInit);
                    this.tabPNtype.SelectedIndex = 0;
                   
                    IDstrTopoTestPlan += currTestPlanID.ToString() +","; //141107 记录用户点击了哪些TestPlan,以确认获取Log的查询范围
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showMfMemory(bool ISNeedShow)
        {
            try
            {
                if (ISNeedShow)
                {
                    if (cboPN.Text.ToString().Trim().Length > 0)
                    {
                        long MCoefsID = -999;
                        for (int i = 0; i < MainForm.TopoToatlDS.Tables["GlobalProductionName"].Rows.Count; i++)
                        {
                            if (MainForm.TopoToatlDS.Tables["GlobalProductionName"].Rows[i]["PN"].ToString().ToUpper() == cboPN.Text.ToString().ToUpper())
                            {
                                MCoefsID = Convert.ToInt64(MainForm.TopoToatlDS.Tables["GlobalProductionName"].Rows[i]["MCoefsID"]);
                                break;
                            }
                        }
                        //150202_1---------------------
                        ManufactureMemory myManufactureMemoryForm = new ManufactureMemory();
                        //获取GlobalManufactureCoefficients
                        string sqlCondition = " where PID=" + MCoefsID + " order by Page,StartAddress,Channel";
                        GlobalManufactureCoefficientsDT = new DataTable();
                        GlobalManufactureCoefficientsDT = GetTestPlanInfo("GlobalManufactureCoefficients", sqlCondition);
                        showTablefilterStrInfo(GlobalManufactureCoefficientsDT, myManufactureMemoryForm.dgvGlobalManufactureMemory, "");

                        GlobalManufactureChipsetControlDT = new DataTable();
                        GlobalManufactureChipsetControlDT = GetTestPlanInfo("GlobalManufactureChipsetControl", " where PID=" + currPNID + " order by DriveType,RegisterAddress,ItemName,ModuleLine,ChipLine");
                        showTablefilterStrInfo(GlobalManufactureChipsetControlDT, myManufactureMemoryForm.dgvManufactureChipsetControl, "");

                        GlobalManufactureChipsetInitializeDT = new DataTable();
                        GlobalManufactureChipsetInitializeDT = GetTestPlanInfo("GlobalManufactureChipsetInitialize", " where PID=" + currPNID + " order by DriveType,RegisterAddress,ChipLine");
                        showTablefilterStrInfo(GlobalManufactureChipsetInitializeDT, myManufactureMemoryForm.dgvManufactureChipsetInitialize, "");

                        TopoMSAEEPROMSetDT = new DataTable();
                        TopoMSAEEPROMSetDT = GetTestPlanInfo("TopoMSAEEPROMSet", " where PID=" + currPNID);
                        myManufactureMemoryForm.dgvMSAEEPROMInitialize.DataSource = TopoMSAEEPROMSetDT;    //141031_0 修正资料源错误
                        hideMyIDPID(myManufactureMemoryForm.dgvMSAEEPROMInitialize);
                        //---------------------------------

                        showTablefilterStrInfo(TopoToatlDS.Tables["GlobalProductionName"], myManufactureMemoryForm.dgvPNInfo, "ID=" + currPNID);
                        hideMyColumn(myManufactureMemoryForm.dgvPNInfo, "MCoefsID");

                        //141031_0 刷新大小
                        resizeDGV(myManufactureMemoryForm.dgvMSAEEPROMInitialize);
                        resizeDGV(myManufactureMemoryForm.dgvManufactureChipsetInitialize);
                        resizeDGV(myManufactureMemoryForm.dgvManufactureChipsetControl);
                        resizeDGV(myManufactureMemoryForm.dgvGlobalManufactureMemory);
                        resizeDGV(myManufactureMemoryForm.dgvPNInfo);


                        DataTable TopoPNSpecsParamsDt = GetTestPlanInfo("TopoPNSpecsParams", " where PID=" + currPNID);
                        TopoPNSpecsParamsDt.Columns.Add("ItemName");
                        for (int i = 0; i < TopoPNSpecsParamsDt.Rows.Count; i++)
                        {
                            if (TopoPNSpecsParamsDt.Rows[i].RowState != DataRowState.Deleted)
                            {
                                TopoPNSpecsParamsDt.Rows[i]["ItemName"] = MainForm.getDTColumnInfo(MainForm.GlobalTotalDS.Tables["GlobalSpecs"], "ItemName", "ID=" + TopoPNSpecsParamsDt.Rows[i]["SID"]);
                            }
                        }
                        showTablefilterStrInfo(TopoPNSpecsParamsDt, myManufactureMemoryForm.dgvPNSpecsParams, "PID=" + currPNID);
                        hideMyColumn(myManufactureMemoryForm.dgvPNSpecsParams, "SID");
                        myManufactureMemoryForm.dgvPNSpecsParams.Columns["ItemName"].DisplayIndex = 0;
                        resizeDGV(myManufactureMemoryForm.dgvPNSpecsParams);
                        myManufactureMemoryForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Pls choose a PN first!!");
                    }
                }
                else
                {
                    //141104_1 Application.OpenForms["MainForm"].Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void resizeDGV(DataGridView dgv)    //141031_0 Add
        {
            int mySize = 0;
            int j = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                {
                    j++;
                    mySize += dgv.Columns[i].Width;
                }
            }
            if (dgv.RowHeadersVisible)
            {
                mySize += dgv.RowHeadersWidth;
            }
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (mySize < dgv.Width)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        dgv.Columns[i].Width += (dgv.Width - mySize) / j;
                    }
                }
            }
        }

        void showMSADefine(bool ISNeedShow)
        {
            try
            {
                if (ISNeedShow)
                {
                    if (cboItemType.Text.ToString().Trim().Length > 0)
                    {
                        long MSAID = -999;
                        string MSAName = "";
                        for (int i = 0; i < this.GlobalTypeDT.Rows.Count; i++)
                        {
                            if (GlobalTypeDT.Rows[i]["ItemName"].ToString().ToUpper() == cboItemType.Text.ToString().ToUpper())
                            {
                                MSAID = Convert.ToInt64(GlobalTypeDT.Rows[i]["MSAID"]);
                                DataTable MSADT = mySqlIO.GetDataTable("Select * from GlobalMSA where ID=" + MSAID, "GlobalMSA");
                                MSAName = " MSAName=" + MSADT.Rows[0]["ItemName"].ToString()
                                    + "; AccessInterface=" + MSADT.Rows[0]["AccessInterface"].ToString()
                                    + "; SlaveAddress=" + MSADT.Rows[0]["SlaveAddress"].ToString();     //GlobalTypeDT.Rows[i]["MSAID"];
                                break;
                            }
                        }
                        if (MSAID == -999)
                        {
                            MessageBox.Show("Can not find [MSAID]!Pls reload all the data~");
                            return;
                        }
                        //150202_1---------------------
                        //获取GlobalManufactureCoefficients
                        string sqlCondition = " where PID=" + MSAID + " order by SlaveAddress,Page,StartAddress,Channel";
                        GlobalMSADefineDT = new DataTable();

                        GlobalMSADefineDT = GetTestPlanInfo(ConstMSAItemTables[1], sqlCondition);
                        MASDefine myMASDefine = new MASDefine();
                        myMASDefine.grpMSADefine.Text = cboItemType.Text.ToString().ToUpper() + ":" + MSAName;
                        showTablefilterStrInfo(GlobalMSADefineDT, myMASDefine.dgvMSADefine, "");
                        //-----------------------------
                        myMASDefine.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Pls choose a Type first!!");
                    }
                }
                else
                {
                    //141104_1 Application.OpenForms["MainForm"].Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnShowMemory_Click(object sender, EventArgs e)
        {
            tsmUpdate.Enabled = true;
            showMfMemory(true);
        }

        void showTestPlanForm(bool isNewForm)
        {
            try
            {
                TestPlanForm myTestPlanForm = new TestPlanForm();
                myTestPlanForm.PName = this.cboPN.Text.ToString();
                myTestPlanForm.PID = Convert.ToInt32( getDTColumnInfo(TopoToatlDS.Tables[ConstTestPlanTables[0]], "ID", "PN='" + cboPN.Text.ToString().Trim() + "'"));
                //myTestPlanForm.PID = getPID("Select ID from " + ConstTestPlanTables[0] + " where PN='" + cboPN.Text.ToString().Trim() + "'");
                myTestPlanForm.blnAddNew = isNewForm;
                myTestPlanForm.ShowDialog();
                RefreshTestPlanList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void RefreshTestPlanList()
        {
            try
            {
                //获取TestPlan
                string sqlCondition = "PID=" + currPNID + " and IgnoreFlag = 'false'";
                cklTestPlan.Enabled = true;
                
                this.cklTestPlan.Items.Clear();
                DataRow[] mrDRs = MainForm.TopoToatlDS.Tables["TopoTestPlan"].Select(sqlCondition);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    tsmExportPlan.Enabled = true;  
                    tsmCopyPlan.Enabled = true;     
                    cklTestPlan.Items.Add(mrDRs[i]["ItemName"].ToString());
                    tabPNtype.Enabled = true;
                }

                if (cklTestPlan.Items.Count < 1)
                    initcklTestPlan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmEditTestPlan_Click(object sender, EventArgs e)
        {
            showTestPlanForm(false);
        }

        private void tsmAddTestPlan_Click(object sender, EventArgs e)
        {
            showTestPlanForm(true);
        }

        bool setTestPlanDisable(bool state, string queryCmd) //141105_0
        {
            try
            {
                DataRow[] DelRowS = TopoToatlDS.Tables["TopoTestPlan"].Select(queryCmd);
                foreach (DataRow dr in DelRowS)
                {
                    dr["IgnoreFlag"] = state.ToString();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void tsmDeleteTestPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cklTestPlan.SelectedIndex != -1)
                {
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Delete TestPlan -->" + cklTestPlan.SelectedItem.ToString()
                        + "\n \n Choose  'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        int CurrIndex = cklTestPlan.SelectedIndex;
                        string sName = cklTestPlan.Items[CurrIndex].ToString();
                        long myPID = Convert.ToInt32(getDTColumnInfo(TopoToatlDS.Tables[ConstTestPlanTables[0]], "ID", "PN='" + cboPN.Text.ToString().Trim() + "'"));
                            //getPID("Select ID from " + ConstTestPlanTables[0] + " where PN='" + cboPN.Text.ToString().Trim() + "'");


                        bool result = setTestPlanDisable(true, "PID=" + myPID + "and ItemName='" + sName + "'");
                        if (result)
                        {
                            cklTestPlan.Items.RemoveAt(CurrIndex);  //141105_0
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item No: " + CurrIndex + ";Name =" + sName + " deleted OK!");
                        }
                        else
                        {
                            MessageBox.Show("Item No: " + CurrIndex + "!Name =" + sName + "deleted Fail!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvTestCtrl_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvTestCtrl.CurrentRow != null && dgvTestCtrl.CurrentRow.Index != -1 && e.ColumnIndex == 0) 
                {
                    string strTestPlanName = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestPlan"], "ItemName", "ID=" + dgvTestCtrl.CurrentRow.Cells["PID"].Value);
                    
                    TestCtrlForm myTestCtrlForm = new TestCtrlForm();
                    myTestCtrlForm.TestPlanName = strTestPlanName;
                    
                    myTestCtrlForm.myCtrlPID = Convert.ToInt64(this.dgvTestCtrl.CurrentRow.Cells["PID"].Value);
                    myTestCtrlForm.ShowDialog();   

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvTestEquip_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvTestEquip.CurrentRow !=null && dgvTestEquip.CurrentRow.Index != -1 && e.ColumnIndex == 0) //140710_2 
                {
                    string strTestPlanName = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestPlan"], "ItemName", "ID=" + this.dgvTestEquip.CurrentRow.Cells["PID"].Value);
                    
                    EquipmentForm myEquip = new EquipmentForm();

                    myEquip.TestPlanName = strTestPlanName;

                    myEquip.PID = Convert.ToInt64(this.dgvTestEquip.CurrentRow.Cells["PID"].Value);
                    myEquip.ShowDialog();   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvMConfigInit_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvMConfigInit.CurrentRow != null && dgvMConfigInit.CurrentRow.Index != -1 && e.ColumnIndex == 0)
                {
                    string strTestPlanName = MainForm.getDTColumnInfo(MainForm.TopoToatlDS.Tables["TopoTestPlan"], "ItemName", "ID=" + dgvMConfigInit.CurrentRow.Cells["PID"].Value);

                    MConfigInit myMConfigInit = new MConfigInit();
                    myMConfigInit.TestPlanName = strTestPlanName;

                    myMConfigInit.PID = Convert.ToInt64(this.dgvMConfigInit.CurrentRow.Cells["PID"].Value);
                    myMConfigInit.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnShowMSADefine_Click(object sender, EventArgs e)
        {
            showMSADefine(true);
        }


        
        void exitForm()
        {
            try
            {
                if (blnIsSQLDB && myLoginID!="")   //只有连接为SQL Server且已经登入OK且具有更新权限的人退出时才出发此项!
                {                    
                    updateUserLoginInfo(mySqlIO.GetCurrTime().ToString(), true, "");    
                }
                //Application.OpenForms["Login"].Show();    //141031_0 取消
                
                this.Dispose();
                GC.Collect();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ISNeedUpdateflag)
                {
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Application will return to the login page,but the modify data is not updated to Server!\n" +
                    "Do you really want to eixt now? \n ",
                    "Notice",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1));
                    
                    if (drst == DialogResult.OK)
                    {
                        ISNeedUpdateflag = false;   //141117_0
                        exitForm();
                    }
                    else
                    {                        
                        return;
                    }
                }
                else
                {
                    exitForm();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmCancel_Click(object sender, EventArgs e)
        {
            mspMain.Enabled = false;
            try
            {
                DialogResult drst = new DialogResult();
                drst = (MessageBox.Show("System will Load the all Data from the Server!", "Notice"
                    , MessageBoxButtons.YesNo));
                if (drst == DialogResult.Yes)
                {
                    //tsmUpdate.Enabled = true;
                    formLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mspMain.Enabled = true;
            }
        }

        void updateUserLoginInfo(string loginOfftime, bool isLoginOFF , string logs)
        {
            try
            {
                DataTable userLoginInfoDt = mySqlIO.GetDataTable("select * from UserLoginInfo where ID=" + myLoginID, "UserLoginInfo");
                DataRow [] dr = userLoginInfoDt.Select("ID=" + myLoginID);
                string hostname = System.Net.Dns.GetHostName(); //主机
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                string IP4 = "";
                for (int i = 0; i < ipEntry.AddressList.Length; i++)
                {
                    if (ipEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IP4 = ipEntry.AddressList[i].ToString();
                        break;
                    }
                }
                //string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                //string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
                string currTime = mySqlIO.GetCurrTime().ToString();
                if (dr.Length == 1)
                {
                    if (loginOfftime.Trim().Length > 0)
                    {
                        dr[0]["LoginOfftime"] = currTime;
                    }
                    if (isLoginOFF)
                    {
                        dr[0]["LoginInfo"] = IP4;
                    }
                    if (logs.Trim().Length > 0)
                    {
                        dr[0]["OPLogs"] = logs;
                    }

                    mySqlIO.UpdateDataTable("select * from UserLoginInfo where ID=" + myLoginID, userLoginInfoDt);
                }
                else
                {
                    MessageBox.Show("");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void updateDetailLogs(string modifyTime, string[] opType, string[] logs, string[] currItem, string[] childItem)
        {
            try
            {
                DataTable detailLogsDT = mySqlIO.GetDataTable("select * from operationLogs where PID=" + myLoginID, "operationLogs");
                for (int i = 0; i < logs.Length; i++)
                {
                    if (logs[i] !=null && opType[i] !=null && currItem[i] !=null && childItem[i]!=null  && logs[i].Trim().Length > 0)
                    {
                        DataRow dr = detailLogsDT.NewRow();
                        dr["PID"] = myLoginID;
                        dr["BlockType"] = "ATSPlan";
                        dr["ModifyTime"] = modifyTime;
                        dr["OperateItem"] = currItem[i];
                        dr["ChlidItem"] = childItem[i];
                        dr["Optype"] = opType[i];
                        dr["DetailLogs"] = logs[i];
                        detailLogsDT.Rows.Add(dr);
                    }
                }
                mySqlIO.UpdateDataTable("select * from operationLogs where PID=" + myLoginID, detailLogsDT);    //变更查询条件,提高执行速度...
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            bool myResult = false;
            string[] opType = new string[1]{""};
            string[] opItem = new string[1] { "" };
            string[] opCItem = new string[1] { "" };
            string[] detailLogs = new string[1] { "" };   //= getTopoTablesChangeLog(out opType);

            string[] PNParams_opType = new string[1] { "" };
            string[] PNopItem = new string[1] { "" };
            string[] PNopCItem = new string[1] { "" };
            string[] PNParams_detailLogs = new string[1] { "" };   //= getTopoTablesChangeLog(out opType);  
            try
            {
                if (TopoToatlDS.GetChanges() == null && GlobalTotalDS.GetChanges() == null)
                {
                    MessageBox.Show("Nothing is modified...Pls click this button after modifiy anything...");
                    return;
                }
                DateTime t1 = DateTime.Now;
                string time1 = mySqlIO.GetCurrTime().ToString();
                sslRunMsg.Text = "System is updating the data Now...Pls wait...." + time1;
                ssrRunMsg.Refresh();

                mspMain.Enabled = false;
                myResult = mySqlIO.UpdateDT();
                string time2 = mySqlIO.GetCurrTime().ToString();
                TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(time1).Ticks);
                TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(time2).Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                string dateDiff = " Use:" + ts.Minutes.ToString() + " Minutes and " + ts.Seconds.ToString() + "." + ts.Milliseconds.ToString() + " Seconds";
                if (File.Exists(Application.StartupPath + @"\timeConsume.txt"))
                {
                    try
                    {
                        File.Delete(Application.StartupPath + @"\timeConsume.txt");
                    }
                    catch
                    {
                    }
                }
                
                writeTimeToLocal(0,Convert.ToDateTime(t1));
                
                if (myResult)
                {
                    detailLogs = getTopoTablesChangeLog(out opType, out opItem, out opCItem);
                    PNParams_detailLogs = getTopoPNSpecParamsLog(out PNParams_opType, out PNopItem,out PNopCItem);
                    writeTimeToLocal(1, Convert.ToDateTime(t1));
                    //formLoad();
                    RefreshLastStateInfo();
                    sslRunMsg.Text = "Update data successful...." + time2 + ":" + dateDiff;
                    writeTimeToLocal(2, Convert.ToDateTime(t1));
                    Application.DoEvents();
                    Thread.Sleep(1);
                    if (blnIsSQLDB)
                    {
                        //141110_0 新增表OperationLogs存放每个TestPlan的修改资料i]
                        string myTime = time2;
                        updateDetailLogs(time2, opType, detailLogs, opItem, opCItem);
                        updateDetailLogs(time2, PNParams_opType, PNParams_detailLogs, PNopItem, PNopCItem);
                    }
                    writeTimeToLocal(3, Convert.ToDateTime(t1));
                }
                else
                {
                    if (blnIsSQLDB)
                    {
                        sslRunMsg.Text = "Update data Failed!System RollBack data ..." + time2 + ":" + dateDiff;      //141021_1
                        MessageBox.Show(sslRunMsg.Text);
                    }
                    else
                    {
                        sslRunMsg.Text = "Update data Failed!System RollBack data ..." + time2 + ":" + dateDiff;       //141021_1 //140616
                        MessageBox.Show(sslRunMsg.Text);
                    }
                    
                }
                ISNeedUpdateflag = false;                
                Thread.Sleep(1);
                writeTimeToLocal(4, Convert.ToDateTime(t1));
            }
           
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());               
            }
            finally
            {
                mspMain.Enabled = true;
            }
        }

        void RefreshLastStateInfo()
        {
            try
            {
                //140522>>TBD 将所有的资料表刷新
                string lastType = this.cboItemType.Text;
                string lastPN = this.cboPN.Text;

                formLoad(); //140527 OK;
                this.cboItemType.Text = lastType;
                this.cboPN.Text = lastPN;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void tsmRefresh_Click(object sender, EventArgs e)
        {
            mspMain.Enabled = false;
            try
            {
                DialogResult drst = new DialogResult();
                drst = (MessageBox.Show("System will Refresh the all Data from the Server!", "Notice", MessageBoxButtons.YesNo));
                if (drst == DialogResult.Yes)
                {
                    RefreshLastStateInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                mspMain.Enabled = true;
            }
        }

        private void timerDate_Tick(object sender, EventArgs e)
        {
            try
            {
                tssTimelbl.Text = System.DateTime.Now.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PNInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ISNeedUpdateflag)
                {
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Application will return to the login page,but the modify data is not updated to Server!\n" +
                    "Do you really want to eixt now? \n ",
                    "Notice",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1));

                    if (drst == DialogResult.OK)
                    {
                        exitForm();
                    }
                    else
                    {
                        e.Cancel = true; //141103_0
                        return;
                    }
                }
                else
                {
                    exitForm();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cklTestPlan_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right) //140530_3
                {
                    if (cklTestPlan.Items.Count <= 0)
                    {
                        tsmEditTestPlan.Enabled = false;
                        tsmDeleteTestPlan.Enabled = false;
                    }
                    else
                    {
                        tsmEditTestPlan.Enabled = true;
                        tsmDeleteTestPlan.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        //*********************************************************

        private void tsmExportPlan_Click(object sender, EventArgs e)
        {
            mspMain.Enabled = false;
            try
            {
                if (cklTestPlan.SelectedItem == null)
                {
                    MessageBox.Show("Pls choose a TestPlan first!");
                    return;
                }
                else
                {

                    tsmExportPlan.Enabled = false;
                    sslRunMsg.Text = "System is exporting the testPlan of< PN= '" + cboPN.Text + "' and PlanName: '"
                            + cklTestPlan.SelectedItem.ToString() + "'> Now,Pls wait...";
                    ssrRunMsg.Refresh();
                    Thread.Sleep(1);
                    
                    DataSet myNewDS = new DataSet();
                    myNewDS = getCurrTestPlanDS(cklTestPlan.SelectedItem.ToString());
                    
                    creatNewExcelSheets(myNewDS);  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { tsmExportPlan.Enabled = true; mspMain.Enabled = true; }
        }

        DataSet getCurrTestPlanDS(string TestPlanName)
        {
            DataSet myNewDS = new DataSet();
            try
            {
                if (TestPlanName.Trim().Length == 0)
                {
                    MessageBox.Show("Pls choose a TestPlan first!");
                    return null;
                }
                else
                {
                    for (int i = 0; i < ConstTestPlanTables.Length; i++)
                    {   //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter","TopoManufactureConfigInit" };
                        if (i == 0)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " where PN='" + cboPN.Text.ToString() + "'"));
                        }
                        else if (i == 1)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where ItemName='" + cklTestPlan.SelectedItem + "' and PID=" + currPNID));
                        }
                        else if (i == 2)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " where PID = " + currTestPlanID));
                        }
                        else if (i == 3)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where PID in ( select id from TopoTestControl  where PID=" + currTestPlanID + ")"));
                        }
                        else if (i == 4)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where PID in ( select id from TopoTestModel where PID in ( select id from TopoTestControl  where PID=" + currTestPlanID + "))"));
                        }
                        else if (i == 5)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " where PID = " + currTestPlanID));
                        }
                        else if (i == 6)
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i],
                                " where PID in ( select id from TopoEquipment where PID=" + currTestPlanID + ")"));
                        }
                        else if (i == 7)     //150203_1
                        {
                            myNewDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], " where PID = " + currTestPlanID));
                        }
                    }
                    myNewDS.Tables[1].PrimaryKey = new DataColumn[] { myNewDS.Tables[1].Columns["ID"] };
                    myNewDS.Tables[2].PrimaryKey = new DataColumn[] { myNewDS.Tables[2].Columns["ID"] };
                    myNewDS.Tables[3].PrimaryKey = new DataColumn[] { myNewDS.Tables[3].Columns["ID"] };
                    myNewDS.Tables[4].PrimaryKey = new DataColumn[] { myNewDS.Tables[4].Columns["ID"] };
                    myNewDS.Tables[5].PrimaryKey = new DataColumn[] { myNewDS.Tables[5].Columns["ID"] };
                    myNewDS.Tables[6].PrimaryKey = new DataColumn[] { myNewDS.Tables[6].Columns["ID"] };
                    myNewDS.Tables[7].PrimaryKey = new DataColumn[] { myNewDS.Tables[7].Columns["ID"] };    //150203_1

                    myNewDS.Relations.Add("relation1", myNewDS.Tables[0].Columns["id"], myNewDS.Tables[1].Columns["pid"]);
                    myNewDS.Relations.Add("relation2", myNewDS.Tables[1].Columns["id"], myNewDS.Tables[2].Columns["pid"]);
                    myNewDS.Relations.Add("relation3", myNewDS.Tables[2].Columns["id"], myNewDS.Tables[3].Columns["pid"]);
                    myNewDS.Relations.Add("relation4", myNewDS.Tables[3].Columns["id"], myNewDS.Tables[4].Columns["pid"]);
                    myNewDS.Relations.Add("relation5", myNewDS.Tables[1].Columns["id"], myNewDS.Tables[5].Columns["pid"]);
                    myNewDS.Relations.Add("relation6", myNewDS.Tables[5].Columns["id"], myNewDS.Tables[6].Columns["pid"]);
                    myNewDS.Relations.Add("relation7", myNewDS.Tables[1].Columns["id"], myNewDS.Tables[7].Columns["pid"]);   //150203_1
                    return myNewDS;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        string getPIDItemName(DataTable dt,string filterString) //140616_2 TBD
        {
            string myItemName = "";
            try
            {                
                switch (dt.TableName.Trim())
                {
                    case "GlobalProductionName":
                        myItemName = GetTestPlanInfo("GlobalProductionType", " where ID=" + currTypeID).Rows[0]["ItemName"].ToString();
                        break;
                    case "TopoTestPlan":
                        myItemName = getDTColumnInfo(TopoToatlDS.Tables[0], "PN", filterString);
                        break;
                    case "TopoTestControl":
                        myItemName = getDTColumnInfo(TopoToatlDS.Tables["TopoTestPlan"], "ItemName", filterString);
                        break;
                    case "TopoTestModel":
                        myItemName = getDTColumnInfo(TopoToatlDS.Tables["TopoTestControl"], "ItemName", filterString);
                        break;
                    case "TopoTestParameter":
                        myItemName = getDTColumnInfo(TopoToatlDS.Tables["TopoTestModel"], "ItemName", filterString);
                        break;
                    case "TopoEquipment":
                        myItemName = getDTColumnInfo(TopoToatlDS.Tables["TopoTestPlan"], "ItemName", filterString);
                        break;
                    case "TopoEquipmentParameter":
                        myItemName = getDTColumnInfo(TopoToatlDS.Tables["TopoEquipment"], "ItemName", filterString);
                        break;
                    case "TopoManufactureConfigInit":
                        myItemName = getDTColumnInfo(TopoToatlDS.Tables["TopoTestPlan"], "ItemName", filterString);
                        break;
                    default:
                        myItemName = "";
                        break;

                }
                return myItemName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return myItemName;
            }
        }

        #region Excel导出数据为一张表
        //void cExportExcel(DataTable dv)
        //{
        //    SaveFileDialog saveExcelFileDialog = new SaveFileDialog();
        //    saveExcelFileDialog.Filter = "Excel   files(*.xls)|*.xls|Excel   files(*.xlsx)|*.xlsx";
        //    saveExcelFileDialog.FilterIndex = 1;
        //    saveExcelFileDialog.RestoreDirectory = true;
        //    saveExcelFileDialog.CreatePrompt = true;

        //    saveExcelFileDialog.Title = "导出Excel文件到 ";

        //    DateTime now = DateTime.Now;

        //    saveExcelFileDialog.FileName = now.Second.ToString().PadLeft(2, '0');
        //    saveExcelFileDialog.ShowDialog();
        //    Stream myStream;
        //    myStream = saveExcelFileDialog.OpenFile();

        //    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
        //    String str = " ";

        //    for (int i = 0; i < dv.Columns.Count; i++)
        //    {

        //        if (i > 0)
        //        {
        //            str += "\t ";
        //        }
        //        str += dv.Columns[i].ColumnName;
        //    }

        //    sw.WriteLine(str);


        //    for (int rowNo = 0; rowNo < dv.Rows.Count; rowNo++)
        //    {
        //        String tempstr = " ";
        //        for (int columnNo = 0; columnNo < dv.Columns.Count; columnNo++)
        //        {
        //            if (columnNo > 0)
        //            {
        //                tempstr += "\t ";
        //            }
        //            //tempstr+=dg.Rows[rowNo,columnNo].ToString();         
        //            tempstr += dv.Rows[rowNo][columnNo].ToString();
        //        }
        //        if (dv.Rows[rowNo]["Result"].ToString().Trim().ToUpper() != "" && dv.Rows[rowNo]["Result"].ToString().Trim().ToUpper() != null)
        //        {
        //            sw.WriteLine(tempstr);
        //        }

        //    }
        //    sw.Close();
        //    myStream.Close();
        //}

        void creatNewExcel(DataSet ds)
        {
            try
            {
                excelApp = new Excel.Application();
                Excel.Workbook workbookData;
                Excel.Worksheet worksheetData;

                workbookData = excelApp.Workbooks.Add(true);    //(DBNull.Value);
                worksheetData = (Excel.Worksheet)workbookData.Worksheets.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
                excelApp.Visible = false;    //140620_0
                string PNName = cboPN.Text.ToString();   //140619_0
                worksheetData.Name = PNName;    //140619_0

                int excelRowsCount = 0;  //140619_0
                int[] myColumnsAddCount = new int [] { 0, 0, 1, 2, 3, 1, 2 };
                string[] myCtrlColumnsParentItem = new string[] { "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestPlan", "TopoEquipment" };
                string[] myEquipColumnsParentItem = new string[] { "GlobalProductionName", "TopoTestPlan", "TopoEquipment" };
                string[] myColumnsParentIDName = new string[] { "PN", "ItemName", "ItemName", "ItemName", "ItemName", "ItemName", "ItemName" };
                int[] myCtrlParentTables = new int[] { 0, 1, 2, 3};
                int[] myEquipParentTables = new int[] { 0, 1, 5 };

                int maxColumnCount = -1;

                Application.DoEvents(); Thread.Sleep(1);

                for (int k = 1; k < ds.Tables.Count; k++)//(int k = ds.Tables.Count-1; k >=0; k--) //140619_0 K=1 PN的部分不需要导出...
                {
                    //140619_0
                    //worksheetData = (Excel.Worksheet)workbookData.Worksheets.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
                    //worksheetData.Name = ds.Tables[k].TableName;

                    if (ds.Tables[k] != null)
                    {
                        string tableName = ds.Tables[k].TableName;
                        //get the columns

                        sslRunMsg.Text = "System is exporting the testPlan of <PN= '" + PNName + " Table=" + tableName + ",Pls Wait...";
                        ssrRunMsg.Refresh();
                        Application.DoEvents(); Thread.Sleep(1);
                        
                        int n = 0;
                        
                        for (int i = 0; i < ds.Tables[k].Columns.Count; i++)
                        {
                            string myColunmName = ds.Tables[k].Columns[i].ColumnName.ToString().ToUpper();
                            if (myColunmName != "ID".ToUpper()
                                && myColunmName != "AppModeID".ToUpper()
                                && myColunmName != "MCoefsID".ToUpper())                                 
                            {
                                n++;
                                string myHeadTxt= SetHeadTextToExcel(ds.Tables[k].Columns[i]);
                                worksheetData.Cells[excelRowsCount+1, n + myColumnsAddCount[k]] = myHeadTxt; 
                            }
                        }
                        if (k < 5)  //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter"};
                        {
                            for (int i = 0; i < myColumnsAddCount[k] + 1; i++)  //140619_2 TBD表头资料处理
                            {
                                //worksheetData.Cells[excelRowsCount + 1, myColumnsAddCount[k]+1] = myColumnsParentItem[k-1];   //因为是从K=1开始                         
                                worksheetData.Cells[excelRowsCount + 1, 1 + i] = myCtrlColumnsParentItem[i];   //因为是从K=1开始  
                            }
                        }
                        else
                        {
                            for (int i = 0; i < myColumnsAddCount[k] + 1; i++)  //140619_2 TBD表头资料处理
                            {
                                //worksheetData.Cells[excelRowsCount + 1, myColumnsAddCount[k]+1] = myColumnsParentItem[k-1];   //因为是从K=1开始                         
                                worksheetData.Cells[excelRowsCount + 1, 1 + i] = myEquipColumnsParentItem[i];   //因为是从K=1开始  
                            }
                        }
                        if (maxColumnCount < n + myColumnsAddCount[k]) //140619_0
                        {
                            maxColumnCount = n + myColumnsAddCount[k];
                        }

                        int myRowsCount = ds.Tables[k].Rows.Count;
                        for (int i = 0; i < myRowsCount; i++)
                        {
                            n = 0;

                            //sslRunMsg.Text = "系统正在导出PN= " + PNName + " 的" + tableName + "的表第 " + i + " 行资料...";
                            //ssrRunMsg.Refresh();
                            //Application.DoEvents(); Thread.Sleep(1);
                            //------------------------------
                            //140619_4 //填充前面的空白部分! TBD  ("ID=" + ds.Tables[currTableList].Rows[i]["PID"])会子表无法查询父表所在的IPD所在的ID号
                            int currTableList = k;
                            string ChilPID = ds.Tables[currTableList].Rows[i]["PID"].ToString();

                            if (k < 5)  //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter"};
                            {
                                for (int m = myColumnsAddCount[k] + 1; m > 0; m--)
                                {
                                    string ParentIDName = getDTColumnInfo(ds.Tables[myCtrlParentTables[m - 1]], myColumnsParentIDName[myCtrlParentTables[m - 1]], "ID=" + ChilPID);
                                    worksheetData.Cells[i + excelRowsCount + 2, m ] = ParentIDName;

                                    if (currTableList > 0 && myCtrlParentTables[currTableList - 1] > 0)
                                    {
                                        string tempPID = ChilPID;
                                        ChilPID = getDTColumnInfo(ds.Tables[myCtrlParentTables[currTableList - 1]], "PID", "ID=" + tempPID);
                                    }
                                    currTableList--;
                                }
                            }
                            else
                            {   //{ "GlobalProductionName", "TopoTestPlan", "TopoEquipment", "TopoEquipmentParameter"};
                                for (int m = myColumnsAddCount[k] + 1; m > 0; m--)
                                {
                                    string ParentIDName = getDTColumnInfo(ds.Tables[myEquipParentTables[m - 1]], myColumnsParentIDName[myEquipParentTables[m - 1]], "ID=" + ChilPID);
                                    worksheetData.Cells[i + excelRowsCount + 2, m ] = ParentIDName;

                                    if (currTableList > 0 && myEquipParentTables[m - 1] > 0)
                                    {
                                        string tempPID = ChilPID;
                                        ChilPID = getDTColumnInfo(ds.Tables[myEquipParentTables[m - 1]], "PID", "ID=" + tempPID);
                                    }
                                    currTableList--;
                                }
                            }
                            //------------------------------
                            for (int j = 0; j < ds.Tables[k].Columns.Count; j++)
                            {
                                string myColunmName = ds.Tables[k].Columns[j].ColumnName.ToString().ToUpper();
                                
                                if (myColunmName != "ID".ToUpper()
                                   && myColunmName != "AppModeID".ToUpper()
                                   && myColunmName != "MCoefsID".ToUpper())
                                {
                                    n++;
                                    if (myColunmName == "PID".ToUpper())
                                    {   
                                        string  PIDItemName= getPIDItemName(ds.Tables[k], "ID=" + ds.Tables[k].Rows[i][j].ToString());
                                        worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] =PIDItemName;
                                    }
                                    else
                                    {   
                                        //140619_0
                                        //worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] = ds.Tables[k].Rows[i][j].ToString();
                                        string ItemValue = ds.Tables[k].Rows[i][j].ToString();
                                        if (ItemValue == "-32768" ||
                                            ItemValue == "32767")
                                        {
                                            worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] = "INF";
                                        }
                                        else
                                        {
                                            worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] = ItemValue;
                                        }
                                    }    
                                }
                            }
                            Application.DoEvents(); Thread.Sleep(1);
                            
                        }                       
                        excelRowsCount += ds.Tables[k].Rows.Count;   //140619_0                        
                        excelRowsCount++;
                        excelRowsCount++;
                    }

                    //调整Excel的样式。 excelRowsCount
                    //Excel.Range rg = worksheetData.Cells.get_Range("A1", worksheetData.Cells[ds.Tables[k].Rows.Count + 1, n]); //140619_0
                    Excel.Range rg = worksheetData.Cells.get_Range("A1", worksheetData.Cells[excelRowsCount, maxColumnCount]);
                    rg.Borders.LineStyle = 1; //单元格加边框。
                    worksheetData.Columns.AutoFit(); //自动调整列宽。
                    Application.DoEvents(); Thread.Sleep(1);
                    
                    worksheetData.Columns.EntireColumn.AutoFit();
                    workbookData.Saved = true;
                    Thread.Sleep(5);
                }

                string strFileName =  Application.StartupPath + "\\" +DateTime.Now.ToString("yyMMdd_HHmmss")+ ds.DataSetName + ".xlsx";
                workbookData.SaveCopyAs(strFileName);
                
                sslRunMsg.Text = "Exoprt TestPlan Data Finish...";
                ssrRunMsg.Refresh();
                Thread.Sleep(1);
                // 关掉内存中的进程
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                EndReport();
            }
        }
        #endregion

        //=======================================
        #region Excel 多个sheet   //140716_0
        void creatNewExcelSheets(DataSet ds)
        {
            try
            {
                if (ds == null)
                {
                    MessageBox.Show("Can not exist any data!Export data Failed!");
                    return;
                }
                excelApp = new Excel.Application();
               
                Excel.Workbook workbookData;
                Excel.Worksheet worksheetData;

                workbookData = excelApp.Workbooks.Add(true);    
                excelApp.Visible = false;    //140620_0
                string PNName = cboPN.Text.ToString();   //140619_0

                int excelRowsCount = 0;  //140619_0
                int[] myColumnsAddCount = new int [] { 0, 0, 1, 2, 3, 1, 2 ,1};
                string[] myCtrlColumnsParentItem = new string[] {  "PN", "TestPlanName", "TestControlName", "TestModelName" };
                string[] myEquipColumnsParentItem = new string[] { "PN", "TestPlanName", "EqipmentName" };
                string[] myColumnsParentIDName = new string[] { "PN", "ItemName", "ItemName", "ItemName", "ItemName", "ItemName", "ItemName" };
                int[] myCtrlParentTables = new int[] { 0, 1, 2, 3};
                int[] myEquipParentTables = new int[] { 0, 1, 5 };
                string[] myMConfigInitColumnsParentItem = new string[] { "PN", "TestPlanName", "TopoManufactureConfigInit" };   //150203_1
                int[] myMConfigInitParentTables = new int[] { 0, 1 };    //150203_1

                Application.DoEvents(); Thread.Sleep(1);

                for (int k = ds.Tables.Count-1; k >0; k--)
                {
                    if (k == 0)
                    {
                    }

                    worksheetData = (Excel.Worksheet)workbookData.Worksheets.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);
                    worksheetData.Name = ds.Tables[k].TableName;

                    if (ds.Tables[k] != null)
                    {
                        string tableName = ds.Tables[k].TableName;
                        //get the columns

                        sslRunMsg.Text = "Exoprt PN= " + PNName + " table = " + tableName + ",Pls wait...";
                        ssrRunMsg.Refresh();
                        Application.DoEvents(); Thread.Sleep(1);
                        
                        int n = 0;
                        
                        for (int i = 0; i < ds.Tables[k].Columns.Count; i++)
                        {
                            string myColunmName = ds.Tables[k].Columns[i].ColumnName.ToString().ToUpper();
                            if (myColunmName != "ID".ToUpper()
                                && myColunmName != "AppModeID".ToUpper()
                                && myColunmName != "MCoefsID".ToUpper())                                 
                            {
                                n++;
                                string myHeadTxt= SetHeadTextToExcel(ds.Tables[k].Columns[i]);
                                worksheetData.Cells[excelRowsCount+1, n + myColumnsAddCount[k]] = myHeadTxt; 
                            }
                        }
                        if (k < 5)  //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter"};
                        {
                            for (int i = 0; i < myColumnsAddCount[k] + 1; i++)  //140619_2 TBD表头资料处理
                            {                                
                                worksheetData.Cells[excelRowsCount + 1, 1 + i] = myCtrlColumnsParentItem[i];   //因为是从K=1开始  
                            }
                        }
                        else if (k==7)  //150203_1
                        {
                            for (int i = 0; i < myColumnsAddCount[k] + 1; i++)  //140619_2 TBD表头资料处理
                            {
                                worksheetData.Cells[excelRowsCount + 1, 1 + i] = myMConfigInitColumnsParentItem[i];   //因为是从K=1开始  
                            }
                        }     
                        else
                        {
                            for (int i = 0; i < myColumnsAddCount[k] + 1; i++)  //140619_2 TBD表头资料处理
                            {                        
                                worksheetData.Cells[excelRowsCount + 1, 1 + i] = myEquipColumnsParentItem[i];   //因为是从K=1开始  
                            }
                        }

                        int myRowsCount = ds.Tables[k].Rows.Count;
                        //excelApp.Visible = true;
                        for (int i = 0; i < myRowsCount; i++)
                        {
                            n = 0;

                            //140619_4 //填充前面的空白部分! TBD  ("ID=" + ds.Tables[currTableList].Rows[i]["PID"])会子表无法查询父表所在的IPD所在的ID号
                            int currTableList = k;
                            string ChilPID = ds.Tables[currTableList].Rows[i]["PID"].ToString();

                            if (k < 5)  //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter"};
                            {
                                for (int m = myColumnsAddCount[k] + 1; m > 0; m--)
                                {
                                    string ParentIDName = getDTColumnInfo(ds.Tables[myCtrlParentTables[m - 1]], myColumnsParentIDName[myCtrlParentTables[m - 1]], "ID=" + ChilPID);
                                    worksheetData.Cells[i + excelRowsCount + 2, m ] = ParentIDName;

                                    if (currTableList > 0 && myCtrlParentTables[currTableList - 1] > 0)
                                    {
                                        string tempPID = ChilPID;
                                        ChilPID = getDTColumnInfo(ds.Tables[myCtrlParentTables[currTableList - 1]], "PID", "ID=" + tempPID);
                                    }
                                    currTableList--;
                                }
                            }
                            else if (k == 7)  //150203_1
                            {
                                for (int m = myColumnsAddCount[k] + 1; m > 0; m--)
                                {
                                    string ParentIDName = getDTColumnInfo(ds.Tables[myMConfigInitParentTables[m - 1]], myColumnsParentIDName[myMConfigInitParentTables[m - 1]], "ID=" + ChilPID);
                                    worksheetData.Cells[i + excelRowsCount + 2, m] = ParentIDName;

                                    if (currTableList > 0 && myEquipParentTables[m - 1] > 0)
                                    {
                                        string tempPID = ChilPID;
                                        ChilPID = getDTColumnInfo(ds.Tables[myMConfigInitParentTables[m - 1]], "PID", "ID=" + tempPID);
                                    }
                                    currTableList--;
                                }
                            }  

                            else
                            {   //{ "GlobalProductionName", "TopoTestPlan", "TopoEquipment", "TopoEquipmentParameter"};
                                for (int m = myColumnsAddCount[k] + 1; m > 0; m--)
                                {
                                    string ParentIDName = getDTColumnInfo(ds.Tables[myEquipParentTables[m - 1]], myColumnsParentIDName[myEquipParentTables[m - 1]], "ID=" + ChilPID);
                                    worksheetData.Cells[i + excelRowsCount + 2, m ] = ParentIDName;

                                    if (currTableList > 0 && myEquipParentTables[m - 1] > 0)
                                    {
                                        string tempPID = ChilPID;
                                        ChilPID = getDTColumnInfo(ds.Tables[myEquipParentTables[m - 1]], "PID", "ID=" + tempPID);
                                    }
                                    currTableList--;
                                }
                            }
                            //------------------------------
                            for (int j = 0; j < ds.Tables[k].Columns.Count; j++)
                            {
                                string myColunmName = ds.Tables[k].Columns[j].ColumnName.ToString().ToUpper();
                                
                                if (myColunmName != "ID".ToUpper()
                                   && myColunmName != "AppModeID".ToUpper()
                                   && myColunmName != "MCoefsID".ToUpper())
                                {
                                    n++;
                                    if (myColunmName == "PID".ToUpper())
                                    {   
                                        string  PIDItemName= getPIDItemName(ds.Tables[k], "ID=" + ds.Tables[k].Rows[i][j].ToString());
                                        worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] =PIDItemName;
                                    }
                                    else
                                    {   
                                        //140619_0
                                        //worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] = ds.Tables[k].Rows[i][j].ToString();
                                        string ItemValue = ds.Tables[k].Rows[i][j].ToString();
                                        if (ItemValue == "-32768" ||
                                            ItemValue == "32767")
                                        {
                                            worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] = "INF";
                                        }
                                        else
                                        {
                                            worksheetData.Cells[i + excelRowsCount + 2, n + myColumnsAddCount[k]] = ItemValue;
                                        }
                                    }    
                                }
                            }
                            Application.DoEvents(); Thread.Sleep(1);
                            
                        }                       
                        //excelRowsCount = ds.Tables[k].Rows.Count;   //140619_0                        
                        //调整Excel的样式。 excelRowsCount
                        Excel.Range rg = worksheetData.Cells.get_Range("A1", worksheetData.Cells[myRowsCount+1, n + myColumnsAddCount[k]]);
                        rg.Borders.LineStyle = 1; //单元格加边框。
                        worksheetData.Columns.AutoFit(); //自动调整列宽。
                        Application.DoEvents(); Thread.Sleep(1);
                        worksheetData.Columns.EntireColumn.AutoFit();
                    }                                        
                    workbookData.Saved = true;
                    Thread.Sleep(5);
                }

                //141103_0 保存TestPlan的部分名称变更
                string strFileName = Application.StartupPath + "\\" + cboItemType.Text + "_"
                    + cboPN.Text +"_" + cklTestPlan.SelectedItem + ".xlsx";
                workbookData.SaveCopyAs(strFileName);
                
                sslRunMsg.Text = "Exoprt TestPlan Data Finish...";
                ssrRunMsg.Refresh();
                Thread.Sleep(1);
                // 关掉内存中的进程
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                EndReport();
            }
        }
        #endregion
        //=======================================

        /// <summary>
        /// 退出报表时关闭Excel和清理垃圾Excel进程
        /// </summary>
        private void EndReport()
        {
            object DBNull = System.Reflection.Missing.Value;
            try
            {
                excelApp.Workbooks.Close();
                excelApp.Workbooks.Application.Quit();
                excelApp.Application.Quit();
                excelApp.Quit();
            }
            catch  {  }
            finally
            {
                try
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp.Workbooks);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp.Application);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    excelApp = null;
                }
                catch { }
                try
                {
                    //清理垃圾进程
                    this.killProcessThread();
                }
                catch { }
            }
        }
        /// <summary>
        /// 杀掉不死进程
        /// </summary>
        private void killProcessThread()
        {
            ArrayList myProcess = new ArrayList();
            for (int i = 0; i < myProcess.Count; i++)
            {
                try
                {
                    System.Diagnostics.Process.GetProcessById(int.Parse((string)myProcess[i])).Kill();
                }
                catch { }
            }
        }

        private void tsmCopyPlan_Click(object sender, EventArgs e)
        {
            try
            {
                mspMain.Enabled = false;
                tsmCopyPlan.Enabled = false;
                //if (cklTestPlan.SelectedItem == null)
                //{
                //    //MessageBox.Show("Not config the new testplan name and then Exit");
                //    //return;
                //}
                //else
                //{
                    string myNewPlanName = "";
                    NewPlanName myNewPlanInfo = new NewPlanName();
                    
                    MessageBox.Show("Pls input a new TestPlan Name:");
                    myNewPlanInfo.ShowDialog();
                    myNewPlanName = myNewPlanInfo.txtNewName.Text.ToString();
                    if (myNewPlanInfo.blnCancelNewPlan == true)
                    {
                        return;
                    }
                    else if (myNewPlanName.Trim().Length != 0)
                    {
                        if (MainForm.currPrmtrCountExisted(TopoToatlDS.Tables["TopoTestPlan"]
                            , "ItemName='" + myNewPlanName + "' and PID=" + currPNID) > 0)
                        {
                            MessageBox.Show("Error !!! : The new TestPlan Name was existed! ");
                            return;
                        }
                        else
                        {
                            if (myNewPlanInfo.isChangedDataSource == false)
                            {
                                if (cklTestPlan.SelectedItem == null)
                                {
                                    MessageBox.Show("Pls choose a source testplan name \n from current TestPlan List first and try again!");
                                    return;
                                }
                                
                                // 复制当前选择的TestPlan
                                //增加判断当前选择的TestPlan在数据库中是否被发现,不支持本地未更新的TestPlan复制!
                                if (GetTestPlanInfo("TopoTestPlan", " Where ItemName='" + cklTestPlan.SelectedItem + "' and PID=" + currPNID).Rows.Count == 0)
                                {
                                    MessageBox.Show("Error !!! : The new TestPlan Name was not existed from Server!: '" + cklTestPlan.SelectedItem
                                        + "' !\n Pls do not copy a testplan which is not updated to server!");
                                    return;
                                }
                                else
                                {
                                    sslRunMsg.Text = "Start copy now ...";
                                    ssrRunMsg.Refresh();
                                    copyCurrTestPlan(myNewPlanName);    //150203_1 暂时不执行Copy MConfigInit的资料
                                }
                            }
                            else
                            {
                                if (myNewPlanInfo.ds != null)
                                {
                                    sslRunMsg.Text = "Start copy now ...";
                                    ssrRunMsg.Refresh();
                                    copyOtherDSPlan(myNewPlanName, myNewPlanInfo.ds);   //150203_1 暂时不执行Copy MConfigInit的资料
                                }
                                else
                                {
                                    MessageBox.Show("PlanName of DataSource is null!Pls try again!");
                                }
                            }
                        }
                        RefreshTestPlanList();
                    }
                    else
                    {
                        MessageBox.Show("Error ! The new planName is null");                        
                        return;
                    }

                    myNewPlanInfo.Dispose();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                formLoad();
            }
            finally
            {
                tsmCopyPlan.Enabled = true;
                mspMain.Enabled = true;
            }
        }

        bool copyOtherDSPlan(string NewPlanName, DataSet ds)
        {
            try
            {                
                //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
                //TopoTestPlan Start----------->
                string myFilterString = "";

                //myFilterString = "ItemName ='" + cklTestPlan.SelectedItem.ToString() + "'";
                DataRow[] DRSTopoTestPlan = ds.Tables["TopoTestPlan"].Select();

                for (int i = 0; i < DRSTopoTestPlan.Length; i++)
                {
                    MainForm.mylastIDTestPlan++;
                    DataRow drPlan = TopoToatlDS.Tables["TopoTestPlan"].NewRow();
                    drPlan = convertNewDR(DRSTopoTestPlan[i], TopoToatlDS.Tables["TopoTestPlan"], i, currPNID, mylastIDTestPlan);
                    drPlan["ItemName"] = NewPlanName; //myNewPlanName 需要手动指定;
                    TopoToatlDS.Tables["TopoTestPlan"].Rows.Add(drPlan);
                    currTestPlanID = mylastIDTestPlan;
                    myFilterString = "PID=" + DRSTopoTestPlan[i]["ID"];

                    //TopoTestControl---------->
                    DataRow[] DRSTopoTestControl = ds.Tables["TopoTestControl"].Select(myFilterString);
                    for (int m = 0; m < DRSTopoTestControl.Length; m++)
                    {
                        MainForm.mylastIDTestCtrl++;
                        DataRow drCtrl = TopoToatlDS.Tables["TopoTestControl"].NewRow();
                        drCtrl = convertNewDR(DRSTopoTestControl[m], TopoToatlDS.Tables["TopoTestControl"], m, currTestPlanID, mylastIDTestCtrl);
                        TopoToatlDS.Tables["TopoTestControl"].Rows.Add(drCtrl);

                        currTestCtrlID = mylastIDTestCtrl;

                        myFilterString = "PID=" + DRSTopoTestControl[m]["ID"];
                        //TopoTestModel---------->
                        DataRow[] DRSTopoTestModel = ds.Tables["TopoTestModel"].Select(myFilterString);
                        for (int n = 0; n < DRSTopoTestModel.Length; n++)
                        {
                            MainForm.mylastIDTestModel++;
                            DataRow drModel = TopoToatlDS.Tables["TopoTestModel"].NewRow();
                            drModel = convertNewDR(DRSTopoTestModel[n], TopoToatlDS.Tables["TopoTestModel"], n, currTestCtrlID, mylastIDTestModel);
                            TopoToatlDS.Tables["TopoTestModel"].Rows.Add(drModel);

                            currTestModelID = mylastIDTestModel;
                            myFilterString = "PID=" + DRSTopoTestModel[n]["ID"];
                            //TopoTestParameter---------->
                            //140619_1 非固定长度的部分需要处理 TBD
                            DataRow[] DRSTopoTestParameter = ds.Tables["TopoTestParameter"].Select(myFilterString);
                            for (int k = 0; k < DRSTopoTestParameter.Length; k++)
                            {
                                //mylastIDTestModel--;
                                MainForm.mylastIDTestPrmtr++;
                                DataRow drPrmtr = TopoToatlDS.Tables["TopoTestParameter"].NewRow();
                                drPrmtr = convertNewDR(DRSTopoTestParameter[k], TopoToatlDS.Tables["TopoTestParameter"], k, currTestModelID, mylastIDTestPrmtr);
                                TopoToatlDS.Tables["TopoTestParameter"].Rows.Add(drPrmtr);
                            }
                        }
                    }

                    //TopoEquipment---------->
                    myFilterString = "PID=" + ds.Tables["TopoTestPlan"].Rows[i]["ID"];
                    DataRow[] DRSTopoEquipment = ds.Tables["TopoEquipment"].Select(myFilterString);
                    for (int m = 0; m < DRSTopoEquipment.Length; m++)
                    {
                        MainForm.mylastIDTestEquip++;
                        DataRow drEquip = TopoToatlDS.Tables["TopoEquipment"].NewRow();
                        drEquip = convertNewDR(DRSTopoEquipment[m], TopoToatlDS.Tables["TopoEquipment"], m, currTestPlanID, mylastIDTestEquip);

                        //141029_0 不再需要指定
                        ////EquipmentName 需要手动指定-------START
                        //string tempString = drEquip["ItemName"].ToString();
                        //int findLastChar = tempString.LastIndexOf("_");
                        //int findFirstChar = tempString.IndexOf("_");
                        //string currItemType = tempString.Substring(findLastChar + 1, tempString.Length - (findLastChar + 1)).ToString();
                        //string  currItemName = tempString.Substring(0, findFirstChar).ToString();
                        //string currItemFunction = drEquip["ItemName"].ToString().Substring(findFirstChar, (findLastChar - findFirstChar));  //141028_1
                        //drEquip["ItemName"] = currItemName + "_" +  currItemFunction+ "_" + currItemType;//drEquip["ID"]
                        ////EquipmentName 需要手动指定-------END

                        TopoToatlDS.Tables["TopoEquipment"].Rows.Add(drEquip);

                        currTestEquipID = mylastIDTestEquip;

                        //TopoEquipmentParameter---------->
                        myFilterString = "PID=" + DRSTopoEquipment[m]["ID"];

                        //140619_1 非固定长度的部分需要处理 TBD
                        DataRow[] DRSTopoEquipmentParameter = ds.Tables["TopoEquipmentParameter"].Select(myFilterString);
                        for (int n = 0; n < DRSTopoEquipmentParameter.Length; n++)
                        {
                            MainForm.mylastIDTestEquipPrmtr++;
                            DataRow drEquipPrmtr = TopoToatlDS.Tables["TopoEquipmentParameter"].NewRow();
                            drEquipPrmtr = convertNewDR(DRSTopoEquipmentParameter[n], TopoToatlDS.Tables["TopoEquipmentParameter"], n, currTestEquipID, mylastIDTestEquipPrmtr);

                            TopoToatlDS.Tables["TopoEquipmentParameter"].Rows.Add(drEquipPrmtr);
                        }
                    }
                }
                sslRunMsg.Text = "Copy TestPlan OK: " + NewPlanName + " ";
                ssrRunMsg.Refresh();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        bool copyCurrTestPlan(string NewPlanName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = getCurrTestPlanDS(cklTestPlan.SelectedItem.ToString());
                //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
                //TopoTestPlan Start----------->
                if (ds == null)
                {
                    return false;
                }
                string myFilterString = "";

                myFilterString = "PID=" + currPNID + " and ItemName ='" + cklTestPlan.SelectedItem.ToString() + "'";
                DataRow[] DRSTopoTestPlan = ds.Tables["TopoTestPlan"].Select(myFilterString);

                for (int i = 0; i < DRSTopoTestPlan.Length; i++)
                {
                    MainForm.mylastIDTestPlan++;
                    DataRow drPlan = TopoToatlDS.Tables["TopoTestPlan"].NewRow();
                    drPlan = convertNewDR(DRSTopoTestPlan[i], TopoToatlDS.Tables["TopoTestPlan"], i, currPNID, mylastIDTestPlan);
                    drPlan["ItemName"] = NewPlanName; //myNewPlanName 需要手动指定;
                    TopoToatlDS.Tables["TopoTestPlan"].Rows.Add(drPlan);
                    currTestPlanID = mylastIDTestPlan;
                    myFilterString = "PID=" + DRSTopoTestPlan[i]["ID"];

                    //TopoTestControl---------->
                    DataRow[] DRSTopoTestControl = ds.Tables["TopoTestControl"].Select(myFilterString);
                    for (int m = 0; m < DRSTopoTestControl.Length; m++)
                    {
                        MainForm.mylastIDTestCtrl++;
                        DataRow drCtrl = TopoToatlDS.Tables["TopoTestControl"].NewRow();
                        drCtrl = convertNewDR(DRSTopoTestControl[m], TopoToatlDS.Tables["TopoTestControl"], m, currTestPlanID, mylastIDTestCtrl);
                        TopoToatlDS.Tables["TopoTestControl"].Rows.Add(drCtrl);

                        currTestCtrlID = mylastIDTestCtrl;

                        myFilterString = "PID=" + DRSTopoTestControl[m]["ID"];
                        //TopoTestModel---------->
                        DataRow[] DRSTopoTestModel = ds.Tables["TopoTestModel"].Select(myFilterString);
                        for (int n = 0; n < DRSTopoTestModel.Length; n++)
                        {
                            MainForm.mylastIDTestModel++;
                            DataRow drModel = TopoToatlDS.Tables["TopoTestModel"].NewRow();
                            drModel = convertNewDR(DRSTopoTestModel[n], TopoToatlDS.Tables["TopoTestModel"], n, currTestCtrlID, mylastIDTestModel);
                            TopoToatlDS.Tables["TopoTestModel"].Rows.Add(drModel);

                            currTestModelID = mylastIDTestModel;
                            myFilterString = "PID=" + DRSTopoTestModel[n]["ID"];
                            //TopoTestParameter---------->
                            //140619_1 非固定长度的部分需要处理 TBD
                            DataRow[] DRSTopoTestParameter = ds.Tables["TopoTestParameter"].Select(myFilterString);
                            for (int k = 0; k < DRSTopoTestParameter.Length; k++)
                            {
                                //mylastIDTestModel--;
                                MainForm.mylastIDTestPrmtr++;
                                DataRow drPrmtr = TopoToatlDS.Tables["TopoTestParameter"].NewRow();
                                drPrmtr = convertNewDR(DRSTopoTestParameter[k], TopoToatlDS.Tables["TopoTestParameter"], k, currTestModelID, mylastIDTestPrmtr);
                                TopoToatlDS.Tables["TopoTestParameter"].Rows.Add(drPrmtr);
                            }
                        }
                    }

                    //TopoEquipment---------->
                    //myFilterString = "PID=" + ds.Tables["TopoTestPlan"].Rows[i]["ID"];
                    myFilterString = "PID=" + DRSTopoTestPlan[i]["ID"];

                    DataRow[] DRSTopoEquipment = ds.Tables["TopoEquipment"].Select(myFilterString);
                    for (int m = 0; m < DRSTopoEquipment.Length; m++)
                    {
                        MainForm.mylastIDTestEquip++;
                        DataRow drEquip = TopoToatlDS.Tables["TopoEquipment"].NewRow();
                        drEquip = convertNewDR(DRSTopoEquipment[m], TopoToatlDS.Tables["TopoEquipment"], m, currTestPlanID, mylastIDTestEquip);

                        //141029_0 不再需要指定
                        ////EquipmentName 需要手动指定-------START
                        //string tempString = drEquip["ItemName"].ToString();
                        //int findLastChar = tempString.LastIndexOf("_");
                        //int findFirstChar = tempString.IndexOf("_");
                        //string currItemType = tempString.Substring(findLastChar + 1, tempString.Length - (findLastChar + 1)).ToString();
                        //string  currItemName = tempString.Substring(0, findFirstChar).ToString();
                        //string currItemFunction = drEquip["ItemName"].ToString().Substring(findFirstChar, (findLastChar - findFirstChar));  //141028_1
                        //drEquip["ItemName"] = currItemName + "_" +  currItemFunction+ "_" + currItemType;//drEquip["ID"]
                        ////EquipmentName 需要手动指定-------END

                        TopoToatlDS.Tables["TopoEquipment"].Rows.Add(drEquip);

                        currTestEquipID = mylastIDTestEquip;

                        //TopoEquipmentParameter---------->
                        myFilterString = "PID=" + DRSTopoEquipment[m]["ID"];

                        //140619_1 非固定长度的部分需要处理 TBD
                        DataRow[] DRSTopoEquipmentParameter = ds.Tables["TopoEquipmentParameter"].Select(myFilterString);
                        for (int n = 0; n < DRSTopoEquipmentParameter.Length; n++)
                        {
                            MainForm.mylastIDTestEquipPrmtr++;
                            DataRow drEquipPrmtr = TopoToatlDS.Tables["TopoEquipmentParameter"].NewRow();
                            drEquipPrmtr = convertNewDR(DRSTopoEquipmentParameter[n], TopoToatlDS.Tables["TopoEquipmentParameter"], n, currTestEquipID, mylastIDTestEquipPrmtr);

                            TopoToatlDS.Tables["TopoEquipmentParameter"].Rows.Add(drEquipPrmtr);
                        }
                    }
                }
                sslRunMsg.Text = "Copy TestPlan OK: " + NewPlanName + " ";
                ssrRunMsg.Refresh();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        DataRow convertNewDR(DataRow sourceDR,  DataTable destDT, int myRowIndex, long myPID, long myID)
        {
            try
            {
                DataRow dr = destDT.NewRow();
                for (int n = 0; n < destDT.Columns.Count; n++)
                {
                    if (destDT.Columns[n].ColumnName.ToUpper() == "ID")
                    {
                        dr["ID"] = myID;
                    }
                    else if (destDT.Columns[n].ColumnName.ToUpper() == "PID")
                    {
                        dr["PID"] = myPID;
                    }
                    else
                    {
                        dr[n] = sourceDR[n].ToString();
                    }
                }
                return dr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void tsmLoadExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not support this funciton!Sorry");
            #region 141031_0 取消此部分
            //string excelFilePath = "";
            //DataSet excelDS = new DataSet();  
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = Application.StartupPath;//注意这里写路径时要用c:\\而不是c:\
            ////openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            //openFileDialog.Filter = "Excel文件|*.xls|Excel文件|*.xlsx|所有文件|*.*";
            //openFileDialog.RestoreDirectory = true;
            //openFileDialog.FilterIndex = 2;
            //DialogResult blnISselected = openFileDialog.ShowDialog();
            //if (openFileDialog.FileName.Length != 0 && blnISselected == DialogResult.OK)
            //{
            //    DataIO mySQLIO = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0
            //    excelFilePath = openFileDialog.FileName.Trim();
            //    getExecleDs(excelFilePath, excelDS);
            //    //140717_0 TBD开始转换资料! TBD
            //    DataSet newExcelDS = new DataSet("newExcelDS");
            //    //string[] ConstTestPlanTables = new string[] { "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
            //    newExcelDS = MainForm.TopoToatlDS.Clone();
            //    DataTable dt = mySQLIO.GetDataTable("select * from GlobalProductionName ", "GlobalProductionName");
            //    //newExcelDS.Tables[0] = dt;

            //}
            #endregion
        }
        #region 141031_0 取消此部分
        //private DataSet getExecleDs(string filePath, DataSet ds)
        //{
        //    string strConn ="" ;
        //    if (filePath.ToUpper().Contains(".xlsx".ToUpper())) //  区分2007以上版本 和2003以下版本 
        //    //Excel 12.0 [表示Excel版本号];HDR=Yes [表示第一行为列头];IMEX=1 [表示所有资料视为文本]
        //    {
        //        strConn = "Provider=Microsoft.ACE.OLEDB.12.0 ;" + "data source=" + filePath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";
        //    }
        //    else
        //    {
        //        strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
        //    }
               
        //    OleDbConnection conn = new OleDbConnection(strConn);

        //    try
        //    {
        //        if (conn == null) conn.Open();          //140625_0
        //        if (conn.State != ConnectionState.Open) //140625_0
        //        {
        //            conn.Open();
        //        }   

        //        ds = new DataSet();

        //        for (int i = 1; i < ConstTestPlanTables.Length; i++) //从TopoTestPlan开始,无需GlobalProductionName
        //        {
        //            //OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + "TR-QQ13L-N00" + "$]", conn);
        //            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + ConstTestPlanTables[i] + "$]", conn);
        //            da.Fill(ds, ConstTestPlanTables[i]);
        //            ds.Tables[ConstTestPlanTables[i]].PrimaryKey = new DataColumn[] { ds.Tables[ConstTestPlanTables[i]].Columns["ID"] };                    
        //        }

        //        //for (int i = 1; i < ConstTestPlanTables.Length-1; i++) //从TopoTestPlan开始,无需GlobalProductionName
        //        //{
        //        //    ds.Relations.Add("Excelrelation" + i.ToString(), ds.Tables[i].Columns["id"], TopoToatlDS.Tables[i + 1].Columns["pid"]);
        //        //}

        //        //TopoToatlDS.Tables[1].Columns["ItemName"].Unique = true; //字段唯一
        //        //TopoToatlDS.Tables[1].Columns["ItemName"].AllowDBNull = false;// 不允许为空
        //        //TopoToatlDS.Tables[1].Columns["ItemName"].AutoIncrement = false;//自动递增

        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return null;
        //    }
        //    finally
        //    {
        //        if (conn != null && conn.State != ConnectionState.Closed)
        //            conn.Close();
        //    }
        //}
        #endregion

        void writeTimeToLocal(int seq,DateTime t0)
        {

            TimeSpan ts1 = new TimeSpan(t0.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            string dateDiff = " Use:" + ts.Minutes.ToString() + " Minutes and " + ts.Seconds.ToString() + "." + ts.Milliseconds.ToString() + " Seconds";

            FileStream fs = new FileStream(Application.StartupPath + @"\timeConsume.txt", FileMode.Append);
            
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            if (dateDiff.Length > 0)
            {
                sw.WriteLine(seq);
                sw.WriteLine(dateDiff);
            }
            sw.Close();
            fs.Close();
        }

        void writeTimeToLocal(string fileName, DateTime t0)
        {

            TimeSpan ts1 = new TimeSpan(t0.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            string dateDiff = " Use:" + ts.Minutes.ToString() + " Minutes and " + ts.Seconds.ToString() + "." + ts.Milliseconds.ToString() + " Seconds";
            FileStream fs = new FileStream(Application.StartupPath + @"\"+ fileName+".txt", FileMode.Append);

            if (fs.Length > 100000 && File.Exists(Application.StartupPath + @"\" + fileName + ".txt"))
            {
                try
                {
                    fs.Close();
                    File.Delete(Application.StartupPath + @"\" + fileName + ".txt");
                    fs = new FileStream(Application.StartupPath + @"\" + fileName + ".txt", FileMode.Append);
                }
                catch(Exception ex)
                {
                    
                }
            }
            
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            if (dateDiff.Length > 0)
            {
                sw.WriteLine(DateTime.Now.ToString() + "\t" +dateDiff);
            }
            sw.Close();
            fs.Close();
        }

        private void sslRunMsg_TextChanged(object sender, EventArgs e)
        {
            mytip.SetToolTip(this.ssrRunMsg, sslRunMsg.Text);
        }

        private void btnConfigSpec_Click(object sender, EventArgs e)
        {
            PNSpecItemInfo pForm = new PNSpecItemInfo();
            pForm.currPNID = currPNID.ToString();
            pForm.ShowDialog();
        }

   
    }
}
