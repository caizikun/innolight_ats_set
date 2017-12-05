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

namespace TestPlan
{
    public partial class PNInfo : Form
    {
        public static bool ISNeedUpdateflag = false;

        public static string[] ConstTestPlanTables = new string[] { "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
        public static string[] ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
        public static string[] ConstMSAItemTables = new string[] { "GlobalProductionType", "GlobalMSADefintionInf", "GlobalMSADTable" };
        public static string[] ConstGlobalManufactureMemoryTables = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalManufactureCoefficients", "GlobalManufactureCoefficientsGroup" };

        public static DataSet TopoToatlDS;
        public static DataSet GlobalTotalDS;

        public static bool blnIsSQLDB;

        public DataTable GlobalTypeDT;

        public long currTypeID;

        public DataTable GlobalMSADefineDT;

        public long currPNID;

        //140701_1 //修改表名并显示>>>>>>>>>>>>>>>>>>>>>
        public DataTable GlobalManufactureCoefficientsDT;  
        public DataTable GlobalManufactureChipsetControlDT;
        public DataTable GlobalManufactureChipsetInitializeDT;
        public DataTable GlobalMSAEEPROMInitializeDT;
        //140701_1 //修改表名并显示<<<<<<<<<<<<<<<<<<<<<

        public long currIndexManufactureMemory;

        //---140605_3 >>>>>>>>>>>>
        //private static SqlManager mySqlIO = new SqlManager();
        //private static AccessManager myAccessIO = new AccessManager();
        static DataIO mySqlIO;
        //---140605_3 <<<<<<<<<<<<

        //更新时顺序为 更新[Add/Edit]主表-->更新[Add/Edit]子表-->删除子表-->删除主表 -->END
        //载入所有TestPlan信息的最后一个生成id EquipPrmtr 和TestPrmtr 可以省略,但是为了方便查询最终数据故保留!

        public static long origIDTestPlan = -1;
        public static long origIDTestCtrl = -1;
        public static long origIDTestModel = -1;
        public static long origIDTestPrmtr = -1;
        public static long origIDTestEquip = -1;
        public static long origIDTestEquipPrmtr = -1;

        public static long mylastIDTestPlan = -1;
        public static long mylastIDTestCtrl = -1;
        public static long mylastIDTestModel = -1;
        public static long mylastIDTestPrmtr = -1;
        public static long mylastIDTestEquip = -1;
        public static long mylastIDTestEquipPrmtr = -1;

        //每新增一条记录对应的mynewIDTestPlan=mylastIDTestPlan+1;
        public static long mynewIDTestPlan = 0;
        public static long mynewIDTestCtrl = 0;
        public static long mynewIDTestModel = 0;
        public static long mynewIDTestPrmtr = 0;
        public static long mynewIDTestEquip = 0;
        public static long mynewIDTestEquipPrmtr = 0;

        //每删除一条记录对应的myDeletedCountTestPlan +1;
        public static long myDeletedCountTestPlan = 0;
        public static long myDeletedCountTestCtrl = 0;
        public static long myDeletedCountTestModel = 0;
        public static long myDeletedCountTestPrmtr = 0;
        public static long myDeletedCountTestEquip = 0;
        public static long myDeletedCountTestEquipPrmtr = 0;

        //每新增一条记录对应的myAddCountTestPlan +1;
        public static long myAddCountTestPlan = 0;
        public static long myAddCountTestCtrl = 0;
        public static long myAddCountTestModel = 0;
        public static long myAddCountTestPrmtr = 0;
        public static long myAddCountTestEquip = 0;
        public static long myAddCountTestEquipPrmtr = 0;

        //每个表当前是否为新增flag~
        public static bool myTestPlanISNewFlag = false;
        public static bool myTestCtrlISNewFlag = false;
        public static bool myTestModelISNewFlag = false;
        public static bool myTestPrmtrISNewFlag = false;
        public static bool myTestEquipISNewFlag = false;
        public static bool myTestEquipPrmtrISNewFlag = false;

        public static bool myTestPlanAddOKFlag = true;
        public static bool myTestEquipAddOKFlag = true;
        public static bool myTestEquipPrmtrAddOKFlag = true;
        public static bool myTestCtrlAddOKFlag = true;
        public static bool myTestModelAddOKFlag = true;
        public static bool myTestPrmtrAddOKFlag = true;

        public Excel.Application excelApp ;//= new Excel.Application();

        long currTestPlanID = -1, currTestCtrlID = -1,currTestModelID=-1,currTestEquipID=-1;

        //140626_1载入标志位,隐藏显示最方便
        public static bool blnReadable = false;
        public static bool blnWritable = false;
        public static bool blnAddable = false;
        public static bool blnDeletable = false;
        public static bool blnDuplicable = false;
        public static bool blnExportable = false;
        public static bool blnImportable = false;

        public PNInfo()
        {
            InitializeComponent();
        }

        #region 公共方法
        //*****************************************************************************
        //公共方法 Start>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //*****************************************************************************
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
                    MessageBox.Show("发现不确定的记录值...-->共有" + dr.Length + " 条记录!");
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
                    MessageBox.Show("查询" + filterString + "记录有问题...无法获得正确的ID值,请确认! 目前共有:" + myRows.Length + "条记录!");
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

        public static long getPID(string SQLcondition)  //140521
        {
            long MyPID = -1;
            try
            {
                MyPID = mySqlIO.GetPID(SQLcondition);
                return MyPID;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return MyPID;
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

        public static void showTablefilterStrInfo(DataTable dt, DataGridView dgv, string filterStr)
        {
            try
            {
                dt.DefaultView.RowFilter = filterStr;
                dgv.DataSource = dt.DefaultView;
                if (dgv.Columns.Contains("SEQ"))
                {
                    dgv.Sort(dgv.Columns["SEQ"], ListSortDirection.Ascending);
                }

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                PNInfo.hideMyIDPID(dgv);
                PNInfo.SetHeadtextToChinese(dgv);
                dgv.Refresh();
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
                if (dgv.Columns.Contains("Item")) //140530_1
                {
                    dgv.Columns["Item"].HeaderText = "项目";
                }
                if (dgv.Columns.Contains("ItemValue")) //140530_1
                {
                    dgv.Columns["ItemValue"].HeaderText = "内容";
                }
                if (dgv.Columns.Contains("ItemName")) //140530_1
                {
                    dgv.Columns["ItemName"].HeaderText = "名称";
                }
                if (dgv.Columns.Contains("ItemType")) //140530_1
                {
                    dgv.Columns["ItemType"].HeaderText = "类型";
                }
                if (dgv.Columns.Contains("ItemDescription")) //140530_1
                {
                    dgv.Columns["ItemDescription"].HeaderText = "描述";
                }
                if (dgv.Columns.Contains("Direction")) //140530_1
                {
                    dgv.Columns["Direction"].HeaderText = "输入|输出";
                }
                if (dgv.Columns.Contains("DefaultLowLimit")) //140530_1
                {
                    dgv.Columns["DefaultLowLimit"].HeaderText = "规格下限";
                }
                if (dgv.Columns.Contains("DefaultUpperLimit")) //140530_1
                {
                    dgv.Columns["DefaultUpperLimit"].HeaderText = "规格上限";
                }
                if (dgv.Columns.Contains("ItemSpecific")) //140530_1
                {
                    dgv.Columns["ItemSpecific"].HeaderText = "指定规格";
                }
                if (dgv.Columns.Contains("LogRecord")) //140530_1
                {
                    dgv.Columns["LogRecord"].HeaderText = "存储调整信息";
                }
                if (dgv.Columns.Contains("Failbreak")) //140530_1
                {
                    dgv.Columns["Failbreak"].HeaderText = "超出规格停止";
                }
                if (dgv.Columns.Contains("DataRecord")) //140530_1
                {
                    dgv.Columns["DataRecord"].HeaderText = "测试结果存档";
                }

                /////////
                if (dgv.Columns.Contains("Vcc")) //140530_1
                {
                    dgv.Columns["Vcc"].HeaderText = "电压";
                }
                if (dgv.Columns.Contains("Temp")) //140530_1
                {
                    dgv.Columns["Temp"].HeaderText = "温度设定";
                }
                if (dgv.Columns.Contains("USBPort")) //140530_1
                {
                    dgv.Columns["USBPort"].HeaderText = "USB端口号";
                }
                if (dgv.Columns.Contains("SEQ")) //140530_1
                {
                    dgv.Columns["SEQ"].HeaderText = "顺序";
                }

                if (dgv.Columns.Contains("Channel")) //140530_1
                {
                    dgv.Columns["Channel"].HeaderText = "模块通道号";
                }
                if (dgv.Columns.Contains("Pattent")) //140530_1
                {
                    dgv.Columns["Pattent"].HeaderText = "码型(PRBS)";
                }
                if (dgv.Columns.Contains("DataRate")) //140530_1
                {
                    dgv.Columns["DataRate"].HeaderText = "速率";
                }
                if (dgv.Columns.Contains("AuxAttribles")) //140530_1
                {
                    dgv.Columns["AuxAttribles"].HeaderText = "其它属性";
                }

                if (dgv.Columns.Contains("ItemName")) //140530_1
                {
                    dgv.Columns["ItemName"].HeaderText = "名称";
                }

                //AppModeID EquipmentList
                if (dgv.Columns.Contains("AppModeID")) //140530_1
                {
                    dgv.Columns["AppModeID"].HeaderText = "程序编号";
                }

                if (dgv.Columns.Contains("EquipmentList")) //140530_1
                {
                    dgv.Columns["EquipmentList"].HeaderText = "设备列表";
                }

                //ADD MSAPrmtr 140703_0 >>>>>>>>>>>>>>>>>>>
                if (dgv.Columns.Contains("FieldName")) //140530_1
                {
                    dgv.Columns["FieldName"].HeaderText = "字段名";
                }
                if (dgv.Columns.Contains("SlaveAddress")) //140530_1
                {
                    dgv.Columns["SlaveAddress"].HeaderText = "从机地址";
                }
                if (dgv.Columns.Contains("StartAddress")) //140530_1
                {
                    dgv.Columns["StartAddress"].HeaderText = "起始地址";
                }
                if (dgv.Columns.Contains("Length")) //140530_1
                {
                    dgv.Columns["Length"].HeaderText = "长度";
                }
                if (dgv.Columns.Contains("Format")) //140530_1
                {
                    dgv.Columns["Format"].HeaderText = "格式类型";
                }
                if (dgv.Columns.Contains("Page")) //140530_1
                {
                    dgv.Columns["Page"].HeaderText = "页号";
                }
                //ADD MSAPrmtr 140703_0 <<<<<<<<<<<<<<<<<<<

                //140710_1>>>>>>>>>>>>>>>>>>>>
                if (dgv.Columns.Contains("RegisterAddress")) //140530_1
                {
                    dgv.Columns["RegisterAddress"].HeaderText = "内存地址";
                }
                if (dgv.Columns.Contains("Endianness")) //140530_1
                {
                    dgv.Columns["Endianness"].HeaderText = "大字节序";
                }
                //140710_1<<<<<<<<<<<<<<<<<<<<
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static string SetHeadTextToExcel(DataColumn dc)
        {
            try
            {

                //if (dc.ColumnName.ToString().ToUpper() == ("PID".ToUpper())) //140618_0
                //{
                //    return "上级项目";
                //}
                if (dc.ColumnName.ToString().ToUpper() == ("PN".ToUpper())) //140618_0
                {
                    return "机种名称";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("Channels".ToUpper())) //140618_0
                {
                    return "通道总数";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("SWVersion".ToUpper())) //140618_0
                {
                    return "软件版本";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("HwVersion".ToUpper())) //140618_0
                {
                    return "硬件版本";
                }

                if (dc.ColumnName.ToString().ToUpper() ==("Item".ToUpper())) //140530_1
                {
                    return  "项目";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("ItemValue".ToUpper())) //140530_1
                {
                    return "内容";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("ItemName".ToUpper())) //140530_1
                {
                    return "项目名称";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("ItemType".ToUpper())) //140530_1
                {
                    return "类型";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("ItemDescription".ToUpper())) //140530_1
                {
                    return "描述";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("Direction".ToUpper())) //140530_1
                {
                    return "输入|输出";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("DefaultLowLimit".ToUpper())) //140530_1
                {
                    return "规格下限";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("DefaultUpperLimit".ToUpper())) //140530_1
                {
                    return "规格上限";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("ItemSpecific".ToUpper())) //140530_1
                {
                    return "指定规格";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("LogRecord".ToUpper())) //140530_1
                {
                    return "存储调整信息";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("Failbreak".ToUpper())) //140530_1
                {
                    return "超出规格停止";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("DataRecord".ToUpper())) //140530_1
                {
                    return "测试结果存档";
                }

                /////////
                if (dc.ColumnName.ToString().ToUpper() ==("Vcc".ToUpper())) //140530_1
                {
                    return "电压";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("Temp".ToUpper())) //140530_1
                {
                    return "温度设定";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("USBPort".ToUpper())) //140530_1
                {
                    return "USB端口号";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("SEQ".ToUpper())) //140530_1
                {
                    return "顺序";
                }

                if (dc.ColumnName.ToString().ToUpper() == ("Channel".ToUpper())) //140530_1
                {
                    return "模块通道号";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("Pattent".ToUpper())) //140530_1
                {
                    return "码型(PRBS)";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("DataRate".ToUpper())) //140530_1
                {
                    return "速率";
                }
                if (dc.ColumnName.ToString().ToUpper() ==("AuxAttribles".ToUpper())) //140530_1
                {
                    return "其它属性";
                }

                if (dc.ColumnName.ToString().ToUpper() ==("ItemName".ToUpper())) //140530_1
                {
                    return "名称";
                }
                //AppModeID EquipmentList
                if (dc.ColumnName.ToString().ToUpper() ==("AppModeID".ToUpper())) //140530_1
                {
                    return "程序编号";
                }

                if (dc.ColumnName.ToString().ToUpper() ==("EquipmentList".ToUpper())) //140530_1
                {
                    return "设备列表";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("Voltages".ToUpper())) //140618_0
                {
                    return "电压总数";
                }
                if (dc.ColumnName.ToString().ToUpper() == ("Tsensors".ToUpper())) //140618_0
                {
                    return "温度总数";
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
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

        //以DataTable方式获取信息
        public DataTable GetTestPlanInfo(string StrTableName, string sqlQueryCmd)
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

        public static void TopoToatlDSAcceptChanges() //更新后方可调用,否则datatable的RowState会被清除
        {
            try
            {
                for (int i = 1; i < TopoToatlDS.Tables.Count; i++)
                {
                    TopoToatlDS.Tables[i].AcceptChanges();
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

        public static void ChangeSEQ(int direction, DataGridView dgv, DataTable dt, string filterStr1, string filterStr2)
        {
            int myCurrRowSEQ = -1, myPrevRowSEQ = -1;
            try
            {
                if (direction == 1)
                {
                    myCurrRowSEQ = Convert.ToInt32(dgv.CurrentRow.Cells["SEQ"].Value);
                    myPrevRowSEQ = Convert.ToInt32(dgv.Rows[dgv.CurrentRow.Index - 1].Cells["SEQ"].Value);
                }
                else if (direction == -1)
                {
                    myCurrRowSEQ = Convert.ToInt32(dgv.CurrentRow.Cells["SEQ"].Value);
                    myPrevRowSEQ = Convert.ToInt32(dgv.Rows[dgv.CurrentRow.Index + 1].Cells["SEQ"].Value);
                }
                else
                {
                    MessageBox.Show("direction只能为 +1 和 -1");
                    return;
                }

                DataRow[] dr1 = dt.Select(filterStr1);

                if (dr1.Length == 1)
                {
                    dr1[0]["SEQ"] = myPrevRowSEQ;
                }
                else
                {
                    MessageBox.Show("资料不为唯一!请确认! 共 " + dr1.Length + "条记录!");
                }

                DataRow[] dr2 = dt.Select(filterStr2);

                if (dr2.Length == 1)
                {
                    dr2[0]["SEQ"] = myCurrRowSEQ;
                }
                else
                {
                    MessageBox.Show("资料不为唯一!请确认! 共 " + dr2.Length + "条记录!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                    MessageBox.Show(ControlName + "资料维护可能不在有效范围内!");
                    return true;
                }
                return result;
            }
            catch
            {
                MessageBox.Show(ControlName + "资料维护可能不在有效范围内!");
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
                    MessageBox.Show(ControlName + "资料维护可能不正常! 不为: " + OnlyAvalue);
                    return true;
                }
                return result;
            }
            catch
            {
                MessageBox.Show(ControlName + "资料维护可能不正常! 不为: " + OnlyAvalue);
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
                    MessageBox.Show("暂不支持该类型的转换-->" +myType);
                }
                return result;
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
                MessageBox.Show("输入的类型不为 \"" + myType + "\",请确认!");        //140604_1        
                return false;
            }

        }
        //*****************************************************************************
        //公共方法 END<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //*****************************************************************************

        #endregion

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
                formLoad();
                if (blnIsSQLDB) //140703_1>>>>
                {
                    tsmRefresh.Visible = (blnReadable ? true : false);

                    tsmCancel.Visible = (blnWritable ? true : false);
                    tsmEditTestPlan.Visible = (blnWritable ? true : false);
                    tsmUpdate.Visible = (blnWritable ? true : false);

                    tsmAddTestPlan.Visible = (blnAddable ? true : false);
                    tsmDeleteTestPlan.Visible = (blnDeletable ? true : false);
                    tsmCopyPlan.Visible = (blnDuplicable ? true : false);
                    tsmExportPlan.Visible = (blnExportable ? true : false);
                    tsmLoadExcel.Visible = (blnImportable ? true : false);
                }
                else
                {
                    blnReadable = true;
                    blnWritable = true;
                    blnAddable = true;
                    blnDeletable = true;
                    blnDuplicable = true;
                    blnExportable = true;
                    blnImportable = true;
                }//140703_1<<<<
                
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
                ToolTip mytip = new ToolTip();

                mytip.SetToolTip(btnShowMemory, "显示Memory配置信息");
                mytip.SetToolTip(btnShowMSADefine, "显示MSADefine信息");

                mytip.SetToolTip(cboPN, "当前类别下存在的机种");
                mytip.SetToolTip(cboItemType, "系统中存在的类别");
                mytip.SetToolTip(cklTestPlan, "当前机种存在的测试计划");
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
                blnIsSQLDB = Login.blnISDBSQLserver;
                if (blnIsSQLDB)
                {
                    mySqlIO = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0
                }
                else
                {
                    mySqlIO = new AccessManager(Login.AccessFilePath);  //140714_1
                }
                this.Text = "MaintainTestPlan Version:" + Application.ProductVersion + "(DataSoure=" + Login.DBName + ")";      //140912_0
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


                if (Login.blnOnlyToReadFlag)
                {
                    tsmUpdate.Enabled = false;
                    tsmUpdate.Text = "只读(不允许更新)";
                }
                this.tsuserInfo.Text = "User:" + Login.UserName;

                string StrTableName = "GlobalProductionType";
                string StrSelectconditions = " order by ID";
                GlobalTypeDT = GetTestPlanInfo(StrTableName, StrSelectconditions);

                for (int i = 0; i < GlobalTypeDT.Rows.Count; i++)
                {
                    this.cboItemType.Items.Add(GlobalTypeDT.Rows[i]["ItemName"].ToString());
                    btnShowMSADefine.Enabled = true;
                }

                //载入当前Server 的各表中最后一笔插入记录!
                origIDTestPlan = mySqlIO.GetLastInsertData(ConstTestPlanTables[1]);
                origIDTestCtrl = mySqlIO.GetLastInsertData(ConstTestPlanTables[2]);
                origIDTestModel = mySqlIO.GetLastInsertData(ConstTestPlanTables[3]);
                origIDTestPrmtr = mySqlIO.GetLastInsertData(ConstTestPlanTables[4]);
                origIDTestEquip = mySqlIO.GetLastInsertData(ConstTestPlanTables[5]);
                origIDTestEquipPrmtr = mySqlIO.GetLastInsertData(ConstTestPlanTables[6]);

                mylastIDTestPlan = origIDTestPlan;
                mylastIDTestCtrl = origIDTestCtrl;
                mylastIDTestModel = origIDTestModel;
                mylastIDTestPrmtr = origIDTestPrmtr;
                mylastIDTestEquip = origIDTestEquip;
                mylastIDTestEquipPrmtr = origIDTestEquipPrmtr;

                sslRunMsg.Text = "IDTestPlan =" + mylastIDTestPlan +
                       ";IDCtrl =" + mylastIDTestCtrl +
                       ";IDModel =" + mylastIDTestModel +
                       ";IDrmtr =" + mylastIDTestPrmtr +
                       ";IDEquip =" + mylastIDTestEquip +
                       ";IDEquipPrmtr =" + mylastIDTestEquipPrmtr;
                InitinalALLTablesInfo();   //获取所有表的信息!
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
                    TopoToatlDS.Tables.Add(GetTestPlanInfo(ConstTestPlanTables[i], ""));
                }
                for (int i = 0; i < ConstGlobalListTables.Length; i++)
                {
                    GlobalTotalDS.Tables.Add(GetTestPlanInfo(ConstGlobalListTables[i], ""));
                }
                TopoToatlDS.Tables[1].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[1].Columns["ID"] };
                TopoToatlDS.Tables[2].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[2].Columns["ID"] };
                TopoToatlDS.Tables[3].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[3].Columns["ID"] };
                TopoToatlDS.Tables[4].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[4].Columns["ID"] };
                TopoToatlDS.Tables[5].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[5].Columns["ID"] };
                TopoToatlDS.Tables[6].PrimaryKey = new DataColumn[] { TopoToatlDS.Tables[6].Columns["ID"] };

                //TopoToatlDS.Tables[1].Columns["ItemName"].Unique = true; //字段唯一
                //TopoToatlDS.Tables[1].Columns["ItemName"].AllowDBNull = false;// 不允许为空
                //TopoToatlDS.Tables[1].Columns["ItemName"].AutoIncrement = false;//自动递增

                TopoToatlDS.Relations.Add("relation1", TopoToatlDS.Tables[0].Columns["id"], TopoToatlDS.Tables[1].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation2", TopoToatlDS.Tables[1].Columns["id"], TopoToatlDS.Tables[2].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation3", TopoToatlDS.Tables[2].Columns["id"], TopoToatlDS.Tables[3].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation4", TopoToatlDS.Tables[3].Columns["id"], TopoToatlDS.Tables[4].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation5", TopoToatlDS.Tables[1].Columns["id"], TopoToatlDS.Tables[5].Columns["pid"]);
                TopoToatlDS.Relations.Add("relation6", TopoToatlDS.Tables[5].Columns["id"], TopoToatlDS.Tables[6].Columns["pid"]);
                //TopoToatlDS.Tables[5].Rows[0]["ID"] = 100;
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

                //140701_1
                GlobalManufactureCoefficientsDT = null; 
                GlobalManufactureChipsetControlDT = null;
                GlobalManufactureChipsetInitializeDT = null;
                GlobalMSAEEPROMInitializeDT = null;


                tsmExportPlan.Enabled = false;  //140616_0
                tsmCopyPlan.Enabled = false;    //140618_1
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
                        MessageBox.Show("找不到" + cboItemType.Text.ToString().ToUpper() + "的资料!");
                        initCboPN();
                        return;
                    }
                    else
                    {
                        //string sqlCondition = " where PID=" + currTypeID;
                        //GlobalPNDT = GetTestPlanInfo(ConstTestPlanTables[0], sqlCondition);
                        this.cboPN.Items.Clear();
                        string sqlCondition = "PID=" + currTypeID;

                        DataRow[] mrDRs = PNInfo.TopoToatlDS.Tables["GlobalProductionName"].Select(sqlCondition);
                        for (int i = 0; i < mrDRs.Length; i++)
                        {
                            cboPN.Items.Add(mrDRs[i]["PN"].ToString());
                            cboPN.Enabled = true;
                            btnShowMemory.Enabled = true;
                        }
                        if (cboPN.Items.Count > 0)
                        {
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
                    MessageBox.Show("请选择相应的Type资料!");
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

                    for (int i = 0; i < cboPN.Items.Count; i++)
                        if (PNInfo.TopoToatlDS.Tables["GlobalProductionName"].Rows[i]["PN"].ToString().ToUpper() == cboPN.Text.ToString().ToUpper())
                        {
                            currPNID = Convert.ToInt64(PNInfo.TopoToatlDS.Tables["GlobalProductionName"].Rows[i]["ID"]);
                            break;
                        }

                    if (currPNID == -999)
                    {
                        MessageBox.Show("找不到" + cboPN.Text.ToString().ToUpper() + "的资料!");
                        initcklTestPlan();
                        return;
                    }
                    else
                    {
                        RefreshTestPlanList();
                    }
                }
                else
                {
                    MessageBox.Show("请选择相应的PN资料!");
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
                cklTestPlan.CheckOnClick = true;            //140523
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
                    int dgvWidth = dgvTestEquip.Size.Width;
                    currPNID = Convert.ToInt64(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["GlobalProductionName"], "ID", "PN='" + cboPN.Text.ToString() + "'"));
                    currTestPlanID = Convert.ToInt64(PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestPlan"], "ID", "ItemName='" + cklTestPlan.SelectedItem.ToString() + "' and PID=" + currPNID));
                    PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoTestControl"], dgvTestCtrl, "PID=" + currTestPlanID);
                    PNInfo.showTablefilterStrInfo(PNInfo.TopoToatlDS.Tables["TopoEquipment"], dgvTestEquip, "PID=" + currTestPlanID);

                    this.tabPNtype.SelectedIndex = this.tabPNtype.TabCount - 1;
                    hideMyIDPID(dgvTestEquip);    //140527
                    //140709_3
                    if (dgvTestEquip.Columns[0].Width+ dgvTestEquip.Columns["ItemName"].Width + dgvTestEquip.Columns["ItemType"].Width < dgvTestEquip.Size.Width - 20)
                    {
                        dgvTestEquip.Columns["ItemName"].Width = dgvTestEquip.Size.Width - dgvTestEquip.Columns[0].Width - dgvTestEquip.Columns["ItemType"].Width;
                    }
                    //setDGVfstColumnValue(dgvTestEquip);   //140603_2

                    this.tabPNtype.SelectedIndex = 0;
                    hideMyIDPID(dgvTestCtrl);     //140527
                    //setDGVfstColumnValue(dgvTestCtrl);    //140603_2
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
                        for (int i = 0; i < cboPN.Items.Count; i++)
                            if (PNInfo.TopoToatlDS.Tables["GlobalProductionName"].Rows[i]["PN"].ToString().ToUpper() == cboPN.Text.ToString().ToUpper())
                            {
                                MCoefsID = Convert.ToInt64(PNInfo.TopoToatlDS.Tables["GlobalProductionName"].Rows[i]["MCoefsID"]);
                                break;
                            }

                        //获取GlobalManufactureCoefficients
                        string sqlCondition = " where PID=" + MCoefsID;
                        GlobalManufactureCoefficientsDT = new DataTable();

                        GlobalManufactureCoefficientsDT = GetTestPlanInfo("GlobalManufactureCoefficients", sqlCondition);

                        ManufactureMemory myManufactureMemoryForm = new ManufactureMemory();

                        myManufactureMemoryForm.dgvGlobalManufactureMemory.DataSource = GlobalManufactureCoefficientsDT;
                        hideMyIDPID(myManufactureMemoryForm.dgvGlobalManufactureMemory);

                        GlobalManufactureChipsetControlDT = new DataTable();
                        GlobalManufactureChipsetControlDT = GetTestPlanInfo("GlobalManufactureChipsetControl", " where PID=" + currPNID);
                        myManufactureMemoryForm.dgvManufactureChipsetControl.DataSource = GlobalManufactureChipsetControlDT;
                        hideMyIDPID(myManufactureMemoryForm.dgvManufactureChipsetControl);

                        GlobalManufactureChipsetInitializeDT = new DataTable();
                        GlobalManufactureChipsetInitializeDT = GetTestPlanInfo("GlobalManufactureChipsetInitialize", " where PID=" + currPNID);
                        myManufactureMemoryForm.dgvManufactureChipsetInitialize.DataSource = GlobalManufactureChipsetInitializeDT;
                        hideMyIDPID(myManufactureMemoryForm.dgvManufactureChipsetInitialize);

                        GlobalMSAEEPROMInitializeDT = new DataTable();
                        GlobalMSAEEPROMInitializeDT = GetTestPlanInfo("GlobalMSAEEPROMInitialize", " where PID=" + currPNID);
                        myManufactureMemoryForm.dgvMSAEEPROMInitialize.DataSource = GlobalManufactureChipsetInitializeDT;
                        hideMyIDPID(myManufactureMemoryForm.dgvMSAEEPROMInitialize);


                        myManufactureMemoryForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("请选择PN 后再点击此按钮!");
                    }
                }
                else
                {
                    Application.OpenForms["PNInfo"].Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                        for (int i = 0; i < this.GlobalTypeDT.Rows.Count; i++)
                        {
                            if (GlobalTypeDT.Rows[i]["ItemName"].ToString().ToUpper() == cboItemType.Text.ToString().ToUpper())
                            {
                                MSAID = Convert.ToInt64(GlobalTypeDT.Rows[i]["MSAID"]);
                                break;
                            }
                        }
                        //获取GlobalManufactureCoefficients
                        string sqlCondition = " where PID=" + MSAID;//+"'";
                        GlobalMSADefineDT = new DataTable();

                        GlobalMSADefineDT = GetTestPlanInfo(ConstMSAItemTables[1], sqlCondition);
                        MASDefine myMASDefine = new MASDefine();
                        myMASDefine.grpMSADefine.Text = cboItemType.Text.ToString().ToUpper();
                        myMASDefine.dgvMSADefine.DataSource = GlobalMSADefineDT;
                        hideMyIDPID(myMASDefine.dgvMSADefine);
                        myMASDefine.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("请选择Type 后再点击此按钮!");
                    }
                }
                else
                {
                    Application.OpenForms["PNInfo"].Show();
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
                myTestPlanForm.PID = getPID("Select ID from " + ConstTestPlanTables[0] + " where PN='" + cboPN.Text.ToString().Trim() + "'");
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
                string sqlCondition = "PID=" + currPNID;
                cklTestPlan.Enabled = true;
                
                this.cklTestPlan.Items.Clear();
                DataRow[] mrDRs = PNInfo.TopoToatlDS.Tables["TopoTestPlan"].Select(sqlCondition);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    tsmExportPlan.Enabled = true;   //140616_0
                    tsmCopyPlan.Enabled = true;     //140618_1
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

        private void tsmDeleteTestPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cklTestPlan.SelectedIndex != -1)
                {
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("即将进行删除资料TestPlan -->" + cklTestPlan.SelectedItem.ToString() + "\n \n 选择 'Y' (是) 继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        int CurrIndex = cklTestPlan.SelectedIndex;
                        string sName = cklTestPlan.Items[CurrIndex].ToString();
                        long myPID = getPID("Select ID from " + ConstTestPlanTables[0] + " where PN='" + cboPN.Text.ToString().Trim() + "'");

                        cklTestPlan.Items.RemoveAt(CurrIndex);
                        //DataTable资料移除部分待新增!!!

                        bool result = DeleteItemForDT(TopoToatlDS.Tables[ConstTestPlanTables[1]], "PID=" + myPID + "and ItemName='" + sName + "'");
                        if (result)
                        {
                            PNInfo.ISNeedUpdateflag = true;
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + ";Name =" + sName + "已经移除!");
                        }
                        else
                        {
                            MessageBox.Show("项目资料 序号为: " + CurrIndex + "!Name =" + sName + "移除失败!");
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
                if (dgvTestCtrl.CurrentRow != null && dgvTestCtrl.CurrentRow.Index != -1 && e.ColumnIndex == 0) //140710_2 dgvTestCtrl.CurrentRow !=null || 
                {
                    string strTestPlanName = PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestPlan"], "ItemName", "ID=" + dgvTestCtrl.CurrentRow.Cells["PID"].Value);
                    //140603_1
                    //DialogResult drst = new DialogResult();
                    //drst = (MessageBox.Show("即将进入TestPlan Name=" + strTestPlanName + "的TestControl维护界面!", "提示", MessageBoxButtons.YesNo));
                    //if (drst == DialogResult.Yes)
                    //{
                    TestCtrlForm myTestCtrlForm = new TestCtrlForm();
                    myTestCtrlForm.TestPlanName = strTestPlanName;
                    //string filterstring = "ItemName='" + myTestCtrlForm.TestPlanName.Trim() + "' and ID=" + dgvTestCtrl.CurrentRow.Cells["PID"] + "";

                    myTestCtrlForm.myCtrlPID = Convert.ToInt64(this.dgvTestCtrl.CurrentRow.Cells["PID"].Value);
                    myTestCtrlForm.ShowDialog();   //show NextForm...

                    //}

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
                    string strTestPlanName = PNInfo.getDTColumnInfo(PNInfo.TopoToatlDS.Tables["TopoTestPlan"], "ItemName", "ID=" + this.dgvTestEquip.CurrentRow.Cells["PID"].Value);
                    //140603_1
                    //DialogResult drst = new DialogResult();
                    //drst = (MessageBox.Show("即将进入TestPlan Name=" + strTestPlanName + "的TestEquip维护界面!", "提示", MessageBoxButtons.YesNo));
                    //if (drst == DialogResult.Yes)
                    //{
                    EquipmentForm myEquip = new EquipmentForm();

                    myEquip.TestPlanName = strTestPlanName;

                    myEquip.PID = Convert.ToInt64(this.dgvTestEquip.CurrentRow.Cells["PID"].Value);
                    myEquip.ShowDialog();   //show NextForm...

                    //}
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


        void updateStutes(bool value) //userName --> LoginName 140626
        {
            try
            {
                
                DataTable myInfo = mySqlIO.GetDataTable("select * from UserInfo where LoginName='" + Login.UserName + "'", "UserInfo");

                DataRow[] myRows = myInfo.Select();
                if (myRows.Length == 1)
                {
                    myRows[0].BeginEdit();
                    //myRows[0]["ISLogin"] = value;
                    myRows[0]["lastLoginOffTime"] = DateTime.Now.ToString();     //140605_0
                    myRows[0].EndEdit();

                    mySqlIO.UpdateDataTable("select * from UserInfo where LoginName='" + Login.UserName + "'", myInfo, false);
                }
                else
                {
                    MessageBox.Show("未发现用户名为: ->" + Login.UserName + "的账户信息!");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void exitForm()
        {
            try
            {
                if (Login.blnOnlyToReadFlag == false && blnIsSQLDB)   //只有连接为SQL Server且已经登入OK且具有更新权限的人退出时才出发此项!
                {
                    updateStutes(false);
                }
                Application.OpenForms["Login"].Show();
                Login.startTime = 1;
                this.Dispose();
                GC.Collect();   //140618_2
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            exitForm();
        }

        private void tsmCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drst = new DialogResult();
                drst = (MessageBox.Show("即将取消所有的维护变更资料!并重新查询数据?", "提示", MessageBoxButtons.YesNo));
                if (drst == DialogResult.Yes)
                {
                    formLoad(); //140527 OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        bool IsCheckStatesOK()
        {
            bool result = true;
            try
            {
                if (myTestPlanAddOKFlag == false)
                {
                    MessageBox.Show("TestPlan尚未维护完整!请确认!");
                    result = false;
                }
                else if (myTestCtrlAddOKFlag == false)
                {
                    MessageBox.Show("TestControll尚未维护完整!请确认!");
                    result = false;
                }
                else if (myTestModelAddOKFlag == false)
                {
                    MessageBox.Show("TestModel尚未维护完整!请确认!");    //140530_0
                    result = false;
                }
                else if (myTestPrmtrAddOKFlag == false)
                {
                    MessageBox.Show("TestParameter尚未维护完整!请确认!");
                    result = false;
                }
                else if (myTestEquipAddOKFlag == false)
                {
                    MessageBox.Show("TestEquipemnt尚未维护完整!请确认!");
                    result = false;
                }
                else if (myTestEquipPrmtrAddOKFlag == false)
                {
                    MessageBox.Show("TestEquipParameter尚未维护完整!请确认!");
                    result = false;
                }

                if (result == false)
                    tsmUpdate.Enabled = true;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                tsmUpdate.Enabled = true;
                return result;
            }
        }

        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                sslRunMsg.Text = "数据更新中...请稍后....";

                tsmUpdate.Enabled = false;
                if (IsCheckStatesOK())
                {
                    //140610
                    //if (blnIsSQLDB) 
                    //{
                    bool myResult = mySqlIO.UpdateDT();
                    Thread.Sleep(300);

                    if (myResult)
                    {
                        RefreshLastStateInfo();
                        sslRunMsg.Text = "数据更新OK....";
                    }
                    else
                    {
                        if (blnIsSQLDB)
                        {
                            sslRunMsg.Text = "数据更新中出现问题!系统已经自动回滚到更新前的状态...";
                            MessageBox.Show(sslRunMsg.Text);
                        }
                        else
                        {
                            sslRunMsg.Text = "数据更新中出现问题!系统 恢复到更新前的状态...";    //140616
                            MessageBox.Show(sslRunMsg.Text);
                        }
                    }
                    ISNeedUpdateflag = false;
                    tsmUpdate.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            try
            {
                DialogResult drst = new DialogResult();
                drst = (MessageBox.Show("即将重新从服务器刷新所有资料!", "提示", MessageBoxButtons.YesNo));
                if (drst == DialogResult.Yes)
                {
                    RefreshLastStateInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                    drst = (MessageBox.Show("即将离开程序返回到登陆界面,但是可能并未更新修改到服务器!\n" +
                    "请确认是否继续退出 \n ",
                    "提示",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1));

                    if (drst == DialogResult.OK)
                    {
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
            try
            {
                if (cklTestPlan.SelectedItem == null)
                {
                    MessageBox.Show("尚未选择需要导出的'测试计划',请确认!");
                    return;
                }
                else
                {

                    tsmExportPlan.Enabled = false;
                    sslRunMsg.Text = "系统正在导出PN= " + cboPN.Text + " 的名称为: '"
                            + cklTestPlan.SelectedItem.ToString() + "' 的测试计划...请稍后...";
                    ssrRunMsg.Refresh();
                    Thread.Sleep(10);
                    //140616_1 需要过滤当前PN下的各个表资料! TBD
                    DataSet myNewDS = new DataSet();
                    myNewDS = getCurrTestPlanDS(cklTestPlan.SelectedItem.ToString());

                    //creatNewExcel(myNewDS);   140715_1

                    creatNewExcelSheets(myNewDS);   //140711_0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { tsmExportPlan.Enabled = true; }
        }

        DataSet getCurrTestPlanDS(string TestPlanName)
        {
            DataSet myNewDS = new DataSet();
            try
            {
                if (TestPlanName.Trim().Length == 0)
                {
                    MessageBox.Show("尚未选择需要导出的'测试计划',请确认!");
                    return null;
                }
                else
                {
                    for (int i = 0; i < ConstTestPlanTables.Length; i++)
                    {   //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
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
                    }
                    myNewDS.Tables[1].PrimaryKey = new DataColumn[] { myNewDS.Tables[1].Columns["ID"] };
                    myNewDS.Tables[2].PrimaryKey = new DataColumn[] { myNewDS.Tables[2].Columns["ID"] };
                    myNewDS.Tables[3].PrimaryKey = new DataColumn[] { myNewDS.Tables[3].Columns["ID"] };
                    myNewDS.Tables[4].PrimaryKey = new DataColumn[] { myNewDS.Tables[4].Columns["ID"] };
                    myNewDS.Tables[5].PrimaryKey = new DataColumn[] { myNewDS.Tables[5].Columns["ID"] };
                    myNewDS.Tables[6].PrimaryKey = new DataColumn[] { myNewDS.Tables[6].Columns["ID"] };

                    myNewDS.Relations.Add("relation1", myNewDS.Tables[0].Columns["id"], myNewDS.Tables[1].Columns["pid"]);
                    myNewDS.Relations.Add("relation2", myNewDS.Tables[1].Columns["id"], myNewDS.Tables[2].Columns["pid"]);
                    myNewDS.Relations.Add("relation3", myNewDS.Tables[2].Columns["id"], myNewDS.Tables[3].Columns["pid"]);
                    myNewDS.Relations.Add("relation4", myNewDS.Tables[3].Columns["id"], myNewDS.Tables[4].Columns["pid"]);
                    myNewDS.Relations.Add("relation5", myNewDS.Tables[1].Columns["id"], myNewDS.Tables[5].Columns["pid"]);
                    myNewDS.Relations.Add("relation6", myNewDS.Tables[5].Columns["id"], myNewDS.Tables[6].Columns["pid"]);

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

                        sslRunMsg.Text = "系统正在导出PN= " + PNName + " 的" + tableName + "的资料...请稍后...";
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
                
                sslRunMsg.Text = "系统导出PN= " + cboPN.Text + "的资料完成!请确认...";
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
                excelApp = new Excel.Application();
               
                Excel.Workbook workbookData;
                Excel.Worksheet worksheetData;

                workbookData = excelApp.Workbooks.Add(true);    
                excelApp.Visible = false;    //140620_0
                string PNName = cboPN.Text.ToString();   //140619_0

                int excelRowsCount = 0;  //140619_0
                int[] myColumnsAddCount = new int [] { 0, 0, 1, 2, 3, 1, 2 };
                string[] myCtrlColumnsParentItem = new string[] {  "机种名称", "测试计划名称", "测试流程名称", "测试模型名称" };
                string[] myEquipColumnsParentItem = new string[] { "机种名称", "测试计划名称", "设备名称" };
                string[] myColumnsParentIDName = new string[] { "PN", "ItemName", "ItemName", "ItemName", "ItemName", "ItemName", "ItemName" };
                int[] myCtrlParentTables = new int[] { 0, 1, 2, 3};
                int[] myEquipParentTables = new int[] { 0, 1, 5 };
                
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

                        sslRunMsg.Text = "系统正在导出PN= " + PNName + " 的" + tableName + "的资料...请稍后...";
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

                string strFileName =  Application.StartupPath + "\\" +DateTime.Now.ToString("yyMMdd_HHmmss")+ ds.DataSetName + ".xlsx";
                workbookData.SaveCopyAs(strFileName);
                
                sslRunMsg.Text = "系统导出PN= " + cboPN.Text + "的资料完成!请确认...";
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
                tsmCopyPlan.Enabled = false;
                if (cklTestPlan.SelectedItem == null)
                {
                    MessageBox.Show("尚未选择的被复制的测试计划的名称!系统将离开复制功能!");
                    return;
                }
                else
                {
                    string myNewPlanName = "";
                    NewPlanName myNewPlanInfo = new NewPlanName();
                    
                    MessageBox.Show("请在弹出的窗体中输入新的测试计划的名称:");
                    myNewPlanInfo.ShowDialog();
                    myNewPlanName = myNewPlanInfo.txtNewName.Text.ToString();
                    if (myNewPlanInfo.blnCancelNewPlan == true)
                    {
                        return;
                    }
                    else if (myNewPlanName.Trim().Length != 0)
                    {
                        if (PNInfo.currPrmtrCountExisted(TopoToatlDS.Tables["TopoTestPlan"]
                            , "ItemName='" + myNewPlanName + "' and PID=" + currPNID) > 0)
                        {
                            MessageBox.Show("已存在当前输入新的测试计划的名称!系统将离开复制功能!");
                            return;
                        }
                        //增加判断当前选择的TestPlan在数据库中是否被发现,不支持本地未更新的TestPlan复制!
                        else if (GetTestPlanInfo("TopoTestPlan", " Where ItemName='" + cklTestPlan.SelectedItem + "' and PID=" + currPNID).Rows.Count == 0)
                        {
                            MessageBox.Show("当前数据库中不存在该测试计划的名称: '" + cklTestPlan.SelectedItem + "' !\n系统无法直接从本地未更新的TestPlan复制!请确认!");
                            return;
                        }
                        else
                        {
                            sslRunMsg.Text = "即将开始复制新的测试计划...请稍后...";
                            ssrRunMsg.Refresh();

                            //140617 TBD...依据当前选择的部分进行复制...
                            DataSet ds =new DataSet();
                            ds = getCurrTestPlanDS(cklTestPlan.SelectedItem.ToString());
                            //{ "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
                            //TopoTestPlan Start----------->
                            string myFilterString = "";

                            myFilterString = "PID=" + currPNID + " and ItemName ='" + cklTestPlan.SelectedItem.ToString() +"'";
                            DataRow[] DRSTopoTestPlan = ds.Tables["TopoTestPlan"].Select(myFilterString);

                            for (int i = 0; i < DRSTopoTestPlan.Length; i++)
                            {
                                PNInfo.mylastIDTestPlan++;
                                DataRow drPlan = TopoToatlDS.Tables["TopoTestPlan"].NewRow();
                                drPlan = convertNewDR(DRSTopoTestPlan[i], TopoToatlDS.Tables["TopoTestPlan"], i, currPNID, mylastIDTestPlan);
                                drPlan["ItemName"] = myNewPlanName; //myNewPlanName 需要手动指定;
                                TopoToatlDS.Tables["TopoTestPlan"].Rows.Add(drPlan);
                                currTestPlanID = mylastIDTestPlan;
                                myFilterString = "PID=" + DRSTopoTestPlan[i]["ID"];

                                //TopoTestControl---------->
                                DataRow[] DRSTopoTestControl = ds.Tables["TopoTestControl"].Select(myFilterString);
                                for (int m = 0; m < DRSTopoTestControl.Length; m++)
                                {
                                    PNInfo.mylastIDTestCtrl++;
                                    DataRow drCtrl = TopoToatlDS.Tables["TopoTestControl"].NewRow();
                                    drCtrl = convertNewDR(DRSTopoTestControl[m], TopoToatlDS.Tables["TopoTestControl"], m, currTestPlanID, mylastIDTestCtrl);
                                    TopoToatlDS.Tables["TopoTestControl"].Rows.Add(drCtrl);

                                    currTestCtrlID = mylastIDTestCtrl;

                                    myFilterString = "PID=" + DRSTopoTestControl[m]["ID"];
                                    //TopoTestModel---------->
                                    DataRow[] DRSTopoTestModel = ds.Tables["TopoTestModel"].Select(myFilterString);
                                    for (int n = 0; n < DRSTopoTestModel.Length; n++)
                                    {
                                        PNInfo.mylastIDTestModel++;
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
                                            PNInfo.mylastIDTestPrmtr++;
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
                                    PNInfo.mylastIDTestEquip++;
                                    DataRow drEquip = TopoToatlDS.Tables["TopoEquipment"].NewRow();
                                    drEquip = convertNewDR(DRSTopoEquipment[m], TopoToatlDS.Tables["TopoEquipment"], m, currTestPlanID, mylastIDTestEquip);
                                    
                                    //EquipmentName 需要手动指定-------START
                                    string tempString = drEquip["ItemName"].ToString();
                                    int findLastChar = tempString.LastIndexOf("_");
                                    int findFirstChar = tempString.IndexOf("_");
                                    string currItemType = tempString.Substring(findLastChar + 1, tempString.Length - (findLastChar + 1)).ToString();
                                    string  currItemName = tempString.Substring(0, findFirstChar).ToString();

                                    drEquip["ItemName"] = currItemName + "_" + drEquip["ID"] + "_" + currItemType;
                                    //EquipmentName 需要手动指定-------END

                                    TopoToatlDS.Tables["TopoEquipment"].Rows.Add(drEquip);

                                    currTestEquipID = mylastIDTestEquip;

                                    //TopoEquipmentParameter---------->
                                    myFilterString = "PID=" + DRSTopoEquipment[m]["ID"];

                                    //140619_1 非固定长度的部分需要处理 TBD
                                    DataRow[] DRSTopoEquipmentParameter = ds.Tables["TopoEquipmentParameter"].Select(myFilterString);
                                    for (int n = 0; n < DRSTopoEquipmentParameter.Length; n++)
                                    {
                                        PNInfo.mylastIDTestEquipPrmtr++;
                                        DataRow drEquipPrmtr = TopoToatlDS.Tables["TopoEquipmentParameter"].NewRow();
                                        drEquipPrmtr = convertNewDR(DRSTopoEquipmentParameter[n], TopoToatlDS.Tables["TopoEquipmentParameter"], n, currTestEquipID, mylastIDTestEquipPrmtr);
                                        
                                        TopoToatlDS.Tables["TopoEquipmentParameter"].Rows.Add(drEquipPrmtr);
                                    }
                                }
                            }
                            sslRunMsg.Text = "复制新的测试计划: " + myNewPlanName + " 完成...请确认...";
                            ssrRunMsg.Refresh();
                        }
                        RefreshTestPlanList();
                    }
                    else
                    {
                        MessageBox.Show("当前输入新的测试计划的名称为空!系统将离开复制功能");                        
                        return;
                    }

                    myNewPlanInfo.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                formLoad();
            }
            finally
            {
                tsmCopyPlan.Enabled = true;
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
        {   //140710_1730 TBD
            string excelFilePath = "";
            DataSet excelDS = new DataSet();  
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;//注意这里写路径时要用c:\\而不是c:\
            //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.Filter = "Excel文件|*.xls|Excel文件|*.xlsx|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 2;
            DialogResult blnISselected = openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length != 0 && blnISselected == DialogResult.OK)
            {
                DataIO mySQLIO = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140918_0 //140722_2   //140912_0
                excelFilePath = openFileDialog.FileName.Trim();
                getExecleDs(excelFilePath, excelDS);
                //140717_0 TBD开始转换资料! TBD
                DataSet newExcelDS = new DataSet("newExcelDS");
                //string[] ConstTestPlanTables = new string[] { "GlobalProductionName", "TopoTestPlan", "TopoTestControl", "TopoTestModel", "TopoTestParameter", "TopoEquipment", "TopoEquipmentParameter" };
                newExcelDS = PNInfo.TopoToatlDS.Clone();
                DataTable dt = mySQLIO.GetDataTable("select * from GlobalProductionName ", "GlobalProductionName");
                //newExcelDS.Tables[0] = dt;

            }
        }

        private DataSet getExecleDs(string filePath, DataSet ds)
        {
            string strConn ="" ;
            if (filePath.ToUpper().Contains(".xlsx".ToUpper())) //  区分2007以上版本 和2003以下版本 
            //Excel 12.0 [表示Excel版本号];HDR=Yes [表示第一行为列头];IMEX=1 [表示所有资料视为文本]
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0 ;" + "data source=" + filePath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";
            }
            else
            {
                strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "Data Source=" + filePath + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
            }
               
            OleDbConnection conn = new OleDbConnection(strConn);

            try
            {
                if (conn == null) conn.Open();          //140625_0
                if (conn.State != ConnectionState.Open) //140625_0
                {
                    conn.Open();
                }   

                ds = new DataSet();

                for (int i = 1; i < ConstTestPlanTables.Length; i++) //从TopoTestPlan开始,无需GlobalProductionName
                {
                    //OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + "TR-QQ13L-N00" + "$]", conn);
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + ConstTestPlanTables[i] + "$]", conn);
                    da.Fill(ds, ConstTestPlanTables[i]);
                    ds.Tables[ConstTestPlanTables[i]].PrimaryKey = new DataColumn[] { ds.Tables[ConstTestPlanTables[i]].Columns["ID"] };                    
                }

                //for (int i = 1; i < ConstTestPlanTables.Length-1; i++) //从TopoTestPlan开始,无需GlobalProductionName
                //{
                //    ds.Relations.Add("Excelrelation" + i.ToString(), ds.Tables[i].Columns["id"], TopoToatlDS.Tables[i + 1].Columns["pid"]);
                //}

                //TopoToatlDS.Tables[1].Columns["ItemName"].Unique = true; //字段唯一
                //TopoToatlDS.Tables[1].Columns["ItemName"].AllowDBNull = false;// 不允许为空
                //TopoToatlDS.Tables[1].Columns["ItemName"].AutoIncrement = false;//自动递增

                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

    }
}
