using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using ATSDataBase;
using System.IO;

namespace GlobalInfo
{
    public partial class MainForm : Form
    {  
        public static bool ISNeedUpdateflag = false;        

        public static DataSet GlobalDS;
        public static string[] ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
        public static string[] ConstMSAItemTables = new string[] { "GlobalMSA", "GlobalMSADefintionInf" };  //140630_1 { "GlobalMSA", "GlobalProductionType", "GlobalMSADefintionInf" };
        public static string[] ConstGlobalPNInfo = new string[] { "GlobalProductionType", "GlobalProductionName"};
        public static string[] ConstGlobalMCoefGroupInfo = new string[] { "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };
        public static string[] ConstGlobalPNInit = new string[] {"GlobalManufactureChipsetControl","GlobalManufactureChipsetInitialize","GlobalMSAEEPROMInitialize"};

        //140701_1 ConstGlobalPNInfo = new string[] { "GlobalProductionType", "GlobalProductionName", "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };

        public static bool blnIsSQLDB;

        public long currTypeID;
                        
        public DataTable GlobalMSADefineDT;    
 
        public long currPNID;
        string currMCoefGroupID;
        string currMSAID;
        public string myLoginID = "";

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

        //140703_4 >>>>>>>>>>>>
        public static long origIDGlobalMCoefGroup = 0;
        public static long origIDMCoefPrmtr = 0;      
        public static long mylastIDGlobalMCoefGroup = -1;
        public static long mylastIDGlobalMCoefPrmtr = -1;
        public static long mynewIDGlobalGlobalMCoefGroup = 0;
        public static long mynewIDGlobalMCoefPrmtr = 0;
        public static long myDeletedCountGlobalMCoefGroup = 0;
        public static long myDeletedCountGlobalMCoefPrmtr = 0;
        public static long myAddCountGlobalMCoefGroup = 0;
        public static long myAddCountGlobalMCoefPrmtr = 0;
        //140703_4 <<<<<<<<<<<<


        //140703_4 >>>>>>>>>>>>
        
        public static long origIDChipCtrl = 0;
        public static long mylastIDGlobalChipCtrl = -1;
        public static long mynewIDGlobalChipCtrl = 0;
        public static long myDeletedCountGlobalChipCtrl = 0;
        public static long myAddCountGlobalChipCtrl = 0;

        public static long origIDChipInit = 0;
        public static long mylastIDGlobalChipInit = -1;
        public static long mynewIDGlobalChipInit = 0;
        public static long myDeletedCountGlobalChipInit = 0;
        public static long myAddCountGlobalChipInit = 0;

        public static long origIDEEPROMInit = 0;
        public static long mylastIDGlobalEEPROMInit = -1;
        public static long mynewIDGlobalEEPROMInit = 0;
        public static long myDeletedCountGlobalEEPROMInit = 0;
        public static long myAddCountGlobalEEPROMInit = 0;

        //origIDChipCtrl origIDChipInit origIDEEPROMInit
        //140703_4 <<<<<<<<<<<<

        public static long origIDGlobalType = -1;
        public static long origIDGlobalPN = -1;
        public static long origIDGlobalMSA = -1;
        public static long origIDGlobalMSAPrmtr = -1;

        public static long origIDGlobalAPP = -1;
        public static long origIDGlobalModel = -1;
        public static long origIDGlobalPrmtr = -1;
        public static long origIDGlobalEquip = -1;
        public static long origIDGlobalEquipPrmtr = -1;
        
        public static long mylastIDGlobalType=-1;
        public static long mylastIDGlobalPN = -1;

        public static long mylastIDGlobalMSA = -1;        //140702_1
        public static long mylastIDGlobalMSAPrmtr = -1;   //140702_1

        public static long mylastIDGlobalAPP = -1;
        public static long mylastIDTestModel = -1;
        public static long mylastIDTestPrmtr = -1;
        public static long mylastIDGlobalEquip = -1;
        public static long mylastIDGlobalEquipPrmtr = -1;

        //每新增一条记录对应的mynewIDTestPlan=mylastIDGlobalType+1;
        public static long mynewIDMSA = 0;   //140702_1  
        public static long mynewIDMSAPrmtr = 0;     //140702_1

        public static long mynewIDGlobalType = 0;
        public static long mynewIDGlobalPN = 0;

        

        public static long mynewIDGlobalAPP = 0;
        public static long mynewIDTestModel = 0;
        public static long mynewIDTestPrmtr = 0;
        public static long mynewIDGlobalEquip = 0;
        public static long mynewIDGlobalEquipPrmtr =0;
        
        //每删除一条记录对应的myDeletedCountTestPlan +1;
        public static long myDeletedCountMSA = 0;   //140702_1  
        public static long myDeletedCountMSAPrmtr = 0;     //140702_1

        public static long myDeletedCountGlobalType = 0;
        public static long myDeletedCountGlobalPN = 0;

        public static long myDeletedCountGlobalAPP = 0;
        public static long myDeletedCountTestModel = 0;
        public static long myDeletedCountTestPrmtr = 0;
        public static long myDeletedCountClobalEquip = 0;
        public static long myDeletedCountClobalEquipPrmtr = 0;

        //每新增一条记录对应的myAddCountGlobalType +1;
        public static long myAddCountMSA = 0;   //140702_1  
        public static long myAddCountMSAPrmtr = 0;     //140702_1

        public static long myAddCountGlobalType = 0;
        public static long myAddCountGlobalPN = 0;
        public static long myAddCountGlobalAPP = 0;
        public static long myAddCountTestModel = 0;
        public static long myAddCountTestPrmtr = 0;
        public static long myAddCountGlobalEquip = 0;
        public static long myAddCountGlobalEquipPrmtr = 0;

        //每个表当前是否为新增flag~ //140702_1 TBD删除? or
        public static bool myGlobalTypeISNewFlag = false;
        public static bool myGlobalPNISNewFlag = false;
        public static bool myGlobalAPPlISNewFlag = false;
        public static bool myGlobalModelISNewFlag = false;
        public static bool myGlobalPrmtrISNewFlag = false;
        public static bool myGlobalEquipISNewFlag = false;
        public static bool myGlobalEquipPrmtrISNewFlag = false;

        public static bool myGlobalTypeAddOKFlag = true;
        public static bool myGlobalPNAddOKFlag = true;
        public static bool myGlobalEquipAddOKFlag = true;
        public static bool myGlobalEquipPrmtrAddOKFlag = true;
        public static bool myGlobalAPPAddOKFlag = true;
        public static bool myGlobalModelAddOKFlag = true;
        public static bool myGlobalPrmtrAddOKFlag = true;        

        public MainForm()
        {
            InitializeComponent();           
        }

        #region 公共方法
        //*****************************************************************************
        //公共方法 Start>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        //*****************************************************************************
        public static bool checkItemLength(string cloumnName,string cloumnValue, int myLength)
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
                    MessageBox.Show(dr.Length + " record existed! The number of records is not unique!");
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
                    MessageBox.Show("The result of query '" + filterString + "' error!\n" + myRows.Length +" records existed!");
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

        public static bool IsExistPID(DataTable dt,string queryCMD)  //140521
        {
            bool MyPIDResult=false;
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
                if (dgv.CurrentRow.Index != -1)
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
                MainForm.hideMyIDPID(dgv);
                //MainForm.SetHeadtextToChinese(dgv);
                dgv.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //141106_0 Modify to remark
        void SetHeadtextToChinese(DataGridView dgv)
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
                if (dgv.Columns.Contains("SpecMin")) //140530_1
                {
                    dgv.Columns["SpecMin"].HeaderText = "规格下限";
                }
                if (dgv.Columns.Contains("SpecMax")) //140530_1
                {
                    dgv.Columns["SpecMax"].HeaderText = "规格上限";
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

                if (dgv.Columns.Contains("TestModelName")) //140530_1
                {
                    dgv.Columns["TestModelName"].HeaderText = "名称";
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
                        dgv.Rows[i].Cells[0].Value = "Operate";
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
                string StrSelectconditions = "select * from " + StrTableName + sqlQueryCmd;
                mydt = mySqlIO.GetDataTable(StrSelectconditions, StrTableName);// 获得环境的DataTable
                return mydt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
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
                sw.WriteLine("================**" + mySqlIO.GetCurrTime().ToString()
                            + "**================\r\n" + ss);
            }
            sw.Close();
            fs.Close();
        }

        void writeAllTableChangesLogToLocal(string ss)  //调试时确认资料使用...
        {
            FileStream fs;
            if (blnIsSQLDB)
            {
                fs = new FileStream(Application.StartupPath + @"\SQLAllTableChangesLog.txt", FileMode.Append);
            }
            else
            {
                fs = new FileStream(Application.StartupPath + @"\AccdbAllTableChangesLog.txt", FileMode.Append);
            }
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            if (ss.Length > 0)
            {
                sw.WriteLine("================**" + mySqlIO.GetCurrTime().ToString()
                            + "**================\r\n" + ss);
            }
            sw.Close();
            fs.Close();
        }

        string[] getMSAChangeLog(out string[] operationType) //将分为一个PItem一条维护记录!
        {
            int modifyCount = 0, currCount = 0;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            try
            {
                //ConstMSAItemTables = new string[] { "GlobalMSA", "GlobalMSADefintionInf" };  //140630_1 { "GlobalMSA", "GlobalProductionType", "GlobalMSADefintionInf" };
                
                string toatalSS = "";
                string currOperationType = "";

                DataRow[] drsDelPItem = GlobalDS.Tables["GlobalMSA"].Select("", "ID ASC", DataViewRowState.Deleted);

                #region tablesName is not deleted

                DataRow[] drsPItem = GlobalDS.Tables["GlobalMSA"].Select("", "ID ASC");
                modifyCount = drsPItem.Length + drsDelPItem.Length;
                detailLogs = new string[modifyCount];
                operationType = new string[modifyCount];

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName;// = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    if (drTestPlan.RowState == DataRowState.Added)
                    {
                        currOperationType += "Added [MSA=" + drTestPlan["ItemName"] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Current].ToString();
                    }
                    else if (drTestPlan.RowState == DataRowState.Deleted)
                    {
                        currOperationType += "Deleted [MSA=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        currOperationType += "Modified [MSA=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }

                    string ss = getDRChangeInfo(drTestPlan, "GlobalMSA");    
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalMSA]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }
                    string ss0 = "";

                    #region drsEquip is not deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalMSADefintionInf"].Select("PID=" + drTestPlan["ID"]);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 += getDRChangeInfo(drEquip, "GlobalMSADefintionInf");
                    }

                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalMSADefintionInf]**:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                    }
                    # endregion

                    #region drsEquip is deleted ===================
                    ss0 = "";   //141112_1
                    drsEquip = GlobalDS.Tables["GlobalMSADefintionInf"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        //drEquip.GetParentRows("1", DataRowVersion.Original);
                        ss0 = getDRChangeInfo(drEquip, "GlobalMSADefintionInf");

                        if (ss0.Length > 0)
                        {
                            ss += "**[GlobalMSADefintionInf]**:\r\n" + "<" + testPlanName + ">\r\n" + ss0;
                        }
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }
                #endregion


                #region drsPItem is deleted
                drsPItem = GlobalDS.Tables["GlobalMSA"].Select("", "ID ASC", DataViewRowState.Deleted);

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    currOperationType += "Deleted [MSA=" + testPlanName + "]";

                    string ss = getDRChangeInfo(drTestPlan, "GlobalMSA");    ;
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalMSA]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }

                    string ss0 = "";

                    #region drsEquip is deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalMSADefintionInf"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 += getDRChangeInfo(drEquip, "GlobalMSADefintionInf");                        
                    }
                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalMSADefintionInf]**:\r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" + 
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }

                #endregion

                return detailLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return detailLogs;
            }
        }

        string[] getMCoefGroupChangeLog(out string[] operationType) //将分为一个PItem一条维护记录!
        {
            int modifyCount = 0, currCount = 0;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            try
            {   //ConstGlobalMCoefGroupInfo = new string[] { "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };
                
                string toatalSS = "";
                string currOperationType = "";

                DataRow[] drsDelPItem = GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Select("", "ID ASC", DataViewRowState.Deleted);

                #region tablesName is not deleted

                DataRow[] drsPItem = GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Select("", "ID ASC");
                modifyCount = drsPItem.Length + drsDelPItem.Length;
                detailLogs = new string[modifyCount];
                operationType = new string[modifyCount];

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName;// = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    if (drTestPlan.RowState == DataRowState.Added)
                    {
                        currOperationType += "Added [MCoefGroup=" + drTestPlan["ItemName"] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Current].ToString();
                    }
                    else if (drTestPlan.RowState == DataRowState.Deleted)
                    {
                        currOperationType += "Deleted [MCoefGroup=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        currOperationType += "Modified [MCoefGroup=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }

                    string ss = getDRChangeInfo(drTestPlan, "GlobalManufactureCoefficientsGroup");    
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalManufactureCoefficientsGroup]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }
                    string ss0 = "";

                    #region drsEquip is not deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalManufactureCoefficients"].Select("PID=" + drTestPlan["ID"]);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 += getDRChangeInfo(drEquip, "GlobalManufactureCoefficients");
                    }

                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalManufactureCoefficients]**:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                    }
                    # endregion

                    #region drsEquip is deleted ===================
                    ss0 = "";   //141112_1
                    drsEquip = GlobalDS.Tables["GlobalManufactureCoefficients"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        //drEquip.GetParentRows("1", DataRowVersion.Original);
                        ss0 += getDRChangeInfo(drEquip, "GlobalManufactureCoefficients");

                    }

                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalManufactureCoefficients]**:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }
                #endregion


                #region drsPItem is deleted
                drsPItem = GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Select("", "ID ASC", DataViewRowState.Deleted);

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    currOperationType += "Deleted [MCoefGroup=" + testPlanName + "]";

                    string ss = getDRChangeInfo(drTestPlan, "GlobalManufactureCoefficientsGroup");    ;
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalManufactureCoefficientsGroup]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }

                    string ss0 = "";

                    #region drsEquip is deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalManufactureCoefficients"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 += getDRChangeInfo(drEquip, "GlobalManufactureCoefficients");

                    }

                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalManufactureCoefficients]**:\r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }

                #endregion

                return detailLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return detailLogs;
            }
        }

        string[] getEquipChangeLog(out string[] operationType) //将分为一个PItem一条维护记录!
        {
            int modifyCount = 0, currCount = 0;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            try
            {
                //ConstGlobalListTables = new string[] { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList" };
               
                string toatalSS = "";
                string currOperationType = "";

                DataRow[] drsDelPItem = GlobalDS.Tables["GlobalAllEquipmentList"].Select("", "ID ASC", DataViewRowState.Deleted);

                #region tablesName is not deleted

                DataRow[] drsPItem = GlobalDS.Tables["GlobalAllEquipmentList"].Select("", "ID ASC");
                modifyCount = drsPItem.Length + drsDelPItem.Length;
                detailLogs = new string[modifyCount];
                operationType = new string[modifyCount];

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName;// = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    if (drTestPlan.RowState == DataRowState.Added)
                    {
                        currOperationType += "Added [EquipmentList=" + drTestPlan["ItemName"] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Current].ToString();
                    }
                    else if (drTestPlan.RowState == DataRowState.Deleted)
                    {
                        currOperationType += "Deleted [EquipmentList=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        currOperationType += "Modified [EquipmentList=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }

                    string ss = getDRChangeInfo(drTestPlan, "GlobalAllEquipmentList");    
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalAllEquipmentList]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }
                    string ss0 = "";

                    #region drsEquip is not deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalAllEquipmentParamterList"].Select("PID=" + drTestPlan["ID"]);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 += getDRChangeInfo(drEquip, "GlobalAllEquipmentParamterList");                        
                    }
                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalAllEquipmentParamterList]**:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                    }
                    # endregion

                    #region drsEquip is deleted ===================
                    ss0 = "";
                    drsEquip = GlobalDS.Tables["GlobalAllEquipmentParamterList"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        //drEquip.GetParentRows("1", DataRowVersion.Original);
                        ss0 += getDRChangeInfo(drEquip, "GlobalAllEquipmentParamterList");

                    }

                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalAllEquipmentParamterList]**:\r\n" + "<" + testPlanName + ">\r\n" + ss0;
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }
                #endregion


                #region drsPItem is deleted
                drsPItem = GlobalDS.Tables["GlobalAllEquipmentList"].Select("", "ID ASC", DataViewRowState.Deleted);

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    currOperationType += "Deleted [EquipmentList=" + testPlanName + "]";

                    string ss = getDRChangeInfo(drTestPlan, "GlobalAllEquipmentList");    ;
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalAllEquipmentList]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }

                    string ss0 = "";

                    #region drsEquip is deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalAllEquipmentParamterList"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 += getDRChangeInfo(drEquip, "GlobalAllEquipmentParamterList");

                    }

                    if (ss0.Length > 0)
                    {
                        ss += "**[GlobalAllEquipmentParamterList]**:\r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }

                #endregion

                return detailLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return detailLogs;
            }
        }

        string[] getAPPModelChangeLog(out string[] operationType) //将分为一个PItem一条维护记录!
        {
            int modifyCount = 0, currCount = 0;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            try
            {
                //ConstGlobalListTables = new string[] { "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
                
                string toatalSS = "";
                string currOperationType = "";

                DataRow[] drsDelPItem = GlobalDS.Tables["GlobalAllAppModelList"].Select("", "ID ASC", DataViewRowState.Deleted);

                #region tablesName is not deleted

                DataRow[] drsPItem = GlobalDS.Tables["GlobalAllAppModelList"].Select("", "ID ASC");
                modifyCount = drsPItem.Length + drsDelPItem.Length;
                detailLogs = new string[modifyCount];
                operationType = new string[modifyCount];

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName;// = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    if (drTestPlan.RowState == DataRowState.Added)
                    {
                        currOperationType += "Added [AppModelList=" + drTestPlan["ItemName"] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Current].ToString();
                    }
                    else if (drTestPlan.RowState == DataRowState.Deleted)
                    {
                        currOperationType += "Deleted [AppModelList=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        currOperationType += "Modified [AppModelList=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }

                    string ss = getDRChangeInfo(drTestPlan, "GlobalAllAppModelList");    
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalAllAppModelList]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }
                    string ss0 = "";
                    string ss1 = "";
                    #region drsEquip is not deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalAllTestModelList"].Select("PID=" + drTestPlan["ID"]);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 = getDRChangeInfo(drEquip, "GlobalAllTestModelList");
                        
                        if (ss0.Length > 0)
                        {
                            ss += "**[GlobalAllTestModelList]**:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                        }

                        #region drsModelPrmtr
                        //Data is not Deleted========================
                        ss1 = "";
                        DataRow[] drsModelPrmtr = GlobalDS.Tables["GlobalTestModelParamterList"].Select("PID=" + drEquip["ID"], "PID ASC");
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalTestModelParamterList");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalTestModelParamterList]****:\r\n" + "<==" + drEquip["ItemName"] + "==>\r\n" + ss1;
                        }

                        //Data is Deleted========================
                        ss1 = "";
                        drsModelPrmtr = GlobalDS.Tables["GlobalTestModelParamterList"].Select("PID=" + drEquip["ID"], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalTestModelParamterList");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalTestModelParamterList]****:\r\n" + "<==" + drEquip["ItemName"] + "==>\r\n" + ss1;
                        }
                        #endregion
                    }
                    # endregion

                    #region drsEquip is deleted ===================
                    drsEquip = GlobalDS.Tables["GlobalAllTestModelList"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        //drEquip.GetParentRows("1", DataRowVersion.Original);
                        ss0 = getDRChangeInfo(drEquip, "GlobalAllTestModelList");
                        
                        if (ss0.Length > 0)
                        {
                            ss += "**[GlobalAllTestModelList]**:\r\n" + "<" + testPlanName + ">\r\n" + ss0;
                        }

                        #region drsModelPrmtr

                        //Data Deleted===============================
                        ss1 = "";
                        DataRow[] drsModelPrmtr = GlobalDS.Tables["GlobalTestModelParamterList"].Select("PID=" + drEquip["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {
                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalTestModelParamterList");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalTestModelParamterList]****:\r\n" + "<==" + drEquip["ItemName", DataRowVersion.Original] + "==>\r\n" + ss1;
                        }
                        #endregion
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }
                #endregion


                #region drsPItem is deleted
                drsPItem = GlobalDS.Tables["GlobalAllAppModelList"].Select("", "ID ASC", DataViewRowState.Deleted);

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    currOperationType += "Deleted [AppModelList=" + testPlanName + "]";

                    string ss = getDRChangeInfo(drTestPlan, "GlobalAllAppModelList");    ;
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalAllAppModelList]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }

                    string ss0 = "";
                    string ss1 = "";
                    #region drsEquip is deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalAllTestModelList"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 = getDRChangeInfo(drEquip, "GlobalAllTestModelList");

                        if (ss0.Length > 0)
                        {
                            ss += "**[GlobalAllTestModelList]**:\r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                        }

                        //Data Deleted===============================
                        ss1 = "";
                        DataRow[] drsModelPrmtr = GlobalDS.Tables["GlobalTestModelParamterList"].Select("PID=" + drEquip["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {
                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalTestModelParamterList");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalTestModelParamterList]****:\r\n" + "<==" + drEquip["ItemName", DataRowVersion.Original] + "==>\r\n" + ss1;
                        }
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }

                #endregion

                return detailLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return detailLogs;
            }
        }

        string[] getPNInfoChangeLog(out string[] operationType) //将分为一个PItem一条维护记录!
        {
            int modifyCount = 0, currCount = 0;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            try
            {
                //{"GlobalProductionType", "GlobalProductionName"};
                //{"GlobalManufactureChipsetControl","GlobalManufactureChipsetInitialize","GlobalMSAEEPROMInitialize"};

                string toatalSS = "";
                string currOperationType = "";

                DataRow[] drsDelPItem = GlobalDS.Tables["GlobalProductionType"].Select("", "ID ASC", DataViewRowState.Deleted);

                #region tablesName is not deleted

                DataRow[] drsPItem = GlobalDS.Tables["GlobalProductionType"].Select("", "ID ASC");
                modifyCount = drsPItem.Length + drsDelPItem.Length;
                detailLogs = new string[modifyCount];
                operationType = new string[modifyCount];

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName;// = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    if (drTestPlan.RowState == DataRowState.Added)
                    {
                        currOperationType += "Added [ProductionType=" + drTestPlan["ItemName"] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Current].ToString();
                    }
                    else if (drTestPlan.RowState == DataRowState.Deleted)
                    {
                        currOperationType += "Deleted [ProductionType=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }
                    else
                    {
                        currOperationType += "Modified [ProductionType=" + drTestPlan["ItemName", DataRowVersion.Original] + "]";
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    }

                    string ss = getDRChangeInfo(drTestPlan, "GlobalProductionType");
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalProductionType]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }
                    string ss0 = "";
                    string ss1 = "";

                    #region drsEquip is not deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalProductionName"].Select("PID=" + drTestPlan["ID"]);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 = getDRChangeInfo(drEquip, "GlobalProductionName");

                        if (ss0.Length > 0)
                        {
                            ss += "**[GlobalProductionName]**:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                        }

                        #region drsModelPrmtr
                        //Data is not Deleted========================
                        ss1 = "";
                        DataRow[] drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetControl"].Select("PID=" + drEquip["ID"], "PID ASC");
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetControl");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetControl]****:\r\n" + "<==" + drEquip["PN"] + "==>\r\n" + ss1;
                        }

                        ss1 = "";
                        drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Select("PID=" + drEquip["ID"], "PID ASC");
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetInitialize");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetInitialize]****:\r\n" + "<==" + drEquip["PN"] + "==>\r\n" + ss1;
                        }

                        //Data is Deleted========================
                        ss1 = "";
                        drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetControl"].Select("PID=" + drEquip["ID"], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetControl");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetControl]****:\r\n" + "<==" + drEquip["PN"] + "==>\r\n" + ss1;
                        }

                        ss1 = "";
                        drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Select("PID=" + drEquip["ID"], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetInitialize");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetInitialize]****:\r\n" + "<==" + drEquip["PN"] + "==>\r\n" + ss1;
                        }
                        #endregion
                    }
                    # endregion

                    #region drsEquip is deleted ===================
                    drsEquip = GlobalDS.Tables["GlobalProductionName"].Select("PID=" + drTestPlan["ID"], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        //drEquip.GetParentRows("1", DataRowVersion.Original);
                        ss0 = getDRChangeInfo(drEquip, "GlobalProductionName");

                        if (ss0.Length > 0)
                        {
                            ss += "**[GlobalProductionName]**:\r\n" + "<" + testPlanName + ">\r\n" + ss0;
                        }

                        #region drsModelPrmtr

                        //Data Deleted===============================
                        ss1 = "";
                        DataRow[] drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetControl"].Select("PID=" + drEquip["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetControl");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetControl]****:\r\n" + "<==" + drEquip["PN", DataRowVersion.Original] + "==>\r\n" + ss1;
                        }

                        ss1 = "";
                        drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Select("PID=" + drEquip["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetInitialize");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetInitialize]****:\r\n" + "<==" + drEquip["PN", DataRowVersion.Original] + "==>\r\n" + ss1;
                        }
                        #endregion
                    }
                    # endregion
                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }
                #endregion


                #region drsPItem is deleted
                drsPItem = GlobalDS.Tables["GlobalProductionType"].Select("", "ID ASC", DataViewRowState.Deleted);

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    currOperationType += "Deleted [ProductionType=" + testPlanName + "]";

                    string ss = getDRChangeInfo(drTestPlan, "GlobalProductionType"); ;
                    if (ss.Length > 0)
                    {
                        ss = "*[GlobalProductionType]*:  \r\n" + "<==" + testPlanName + "==>\r\n" + ss;
                    }

                    string ss0 = "";
                    string ss1 = "";
                    #region drsEquip is deleted ===================
                    DataRow[] drsEquip = GlobalDS.Tables["GlobalProductionName"].Select("PID=" + drTestPlan["ID", DataRowVersion.Original], "PID ASC", DataViewRowState.Deleted);

                    foreach (DataRow drEquip in drsEquip)
                    {
                        ss0 = getDRChangeInfo(drEquip, "GlobalProductionName");

                        if (ss0.Length > 0)
                        {
                            ss += "**[GlobalProductionName]**:\r\n" + "<==" + testPlanName + "==>\r\n" + ss0;
                        }

                        //Data Deleted===============================
                        //Data Deleted===============================
                        ss1 = "";
                        DataRow[] drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetControl"].Select("PID=" + drEquip["ID"], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetControl");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetControl]****:\r\n" + "<==" + drEquip["PN", DataRowVersion.Original] + "==>\r\n" + ss1;
                        }

                        ss1 = "";
                        drsModelPrmtr = GlobalDS.Tables["GlobalManufactureChipsetInitialize"].Select("PID=" + drEquip["ID"], "PID ASC", DataViewRowState.Deleted);
                        foreach (DataRow drModelPrmtr in drsModelPrmtr)
                        {

                            ss1 += getDRChangeInfo(drModelPrmtr, "GlobalManufactureChipsetInitialize");
                        }
                        if (ss1.Length > 0)
                        {
                            ss += "****[GlobalManufactureChipsetInitialize]****:\r\n" + "<==" + drEquip["PN", DataRowVersion.Original] + "==>\r\n" + ss1;
                        }
                    }
                    # endregion

                    if (ss.Length > 0)
                    {
                        toatalSS += "<**********************" +
                            testPlanName + "**********************>\r\n" + ss + "\r\n";
                    }
                    detailLogs[currCount] = toatalSS;

                    if (toatalSS.Length > 0)
                    {
                        operationType[currCount] = currOperationType;
                    }
                    else
                    {
                        operationType[currCount] = "";
                    }

                    //writeLogToLocal(operationType[currCount]);
                    writeLogToLocal(detailLogs[currCount]);
                    currCount++;
                }

                #endregion

                return detailLogs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return detailLogs;
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
                    if (GlobalDS.Tables[tableName].Columns.Contains("PN"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <PN=" + myDatarow["PN"].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName"].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item"].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("FieldName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <FieldName=" + myDatarow["FieldName"].ToString() + ">";
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
                                if (GlobalDS.Tables[tableName].Columns.Contains("PN"))
                                {
                                    sss1 += "Modified--> <PN=" + myDatarow["PN"].ToString() + ">OriginalData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                                {
                                    sss1 += "Modified--> <ItemName=" + myDatarow["ItemName"].ToString() + ">OriginalData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                                {
                                    sss1 += "Modified--> <Item=" + myDatarow["Item"].ToString() + ">OriginalData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("FieldName"))
                                {
                                    sss1 += "Modified--> <FieldName=" + myDatarow["FieldName"].ToString() + ">OriginalData:";
                                }
                                else
                                {
                                    sss1 += "Modified--> <ID=" + myDatarow["ID"].ToString() + ">OriginalData:";
                                }
                            }
                            if (sss2.Length <= 0)
                            {
                                if (GlobalDS.Tables[tableName].Columns.Contains("PN"))
                                {
                                    sss2 += "Modified--> <PN=" + myDatarow["PN"].ToString() + ">ModifiedData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                                {
                                    sss2 += "Modified--> <ItemName=" + myDatarow["ItemName"].ToString() + ">ModifiedData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                                {
                                    sss2 += "Modified--> <Item=" + myDatarow["Item"].ToString() + ">ModifiedData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("FieldName"))
                                {
                                    sss2 += "Modified--> <FieldName=" + myDatarow["FieldName"].ToString() + ">ModifiedData:";
                                }
                                else
                                {
                                    sss2 += "Modified--> <ID=" + myDatarow["ID"].ToString() + ">ModifiedData:";
                                }
                            }
                            sss1 += "[" + GlobalDS.Tables[tableName].Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Original].ToString() + ";";
                            sss2 += "[" + GlobalDS.Tables[tableName].Columns[k].ColumnName + "]=" + myDatarow[k, DataRowVersion.Current].ToString() + ";";
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
                    if (GlobalDS.Tables[tableName].Columns.Contains("PN"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <PN=" + myDatarow["PN", DataRowVersion.Original].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["ItemName", DataRowVersion.Original].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <Item=" + myDatarow["Item", DataRowVersion.Original].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("FieldName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <FieldName=" + myDatarow["FieldName", DataRowVersion.Original].ToString() + ">";
                    }
                    else
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <ID=" + myDatarow["ID", DataRowVersion.Original].ToString() + ">";
                    }
                    for (int k = 0; k < GlobalDS.Tables[tableName].Columns.Count; k++)
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

        string DSAcceptChanges() //更新后方可调用,否则datatable的RowState会被清除
        {
            string SS = "==User :" + Login.UserName + " Operation Logs at " + DateTime.Now.ToString() + " ==\n\r";
            try
            {
                for (int i = 0; i < GlobalDS.Tables.Count; i++)
                {
                    if (GlobalDS.Tables[i].GetChanges() != null)
                    {
                        SS += "**[" + GlobalDS.Tables[i].TableName + "]**\r\n";

                        DataTable myDeletedDt = new DataTable();
                        DataTable myAddDt = new DataTable();
                        DataTable myChangeDt = new DataTable();
                        myDeletedDt = GlobalDS.Tables[i].GetChanges(DataRowState.Deleted);
                        myAddDt = GlobalDS.Tables[i].GetChanges(DataRowState.Added);
                        myChangeDt = GlobalDS.Tables[i].GetChanges(DataRowState.Modified);

                        #region 每行的资料
                        if (myChangeDt != null)
                        {
                            for (int j = 0; j < myChangeDt.Rows.Count; j++)
                            {
                                //DataRow dataRow = GlobalDS.Tables[i].Rows[j];
                                DataRow dataRow = myChangeDt.Rows[j];
                                string ss1 = "";
                                string ss2 = "";

                                for (int k = 0; k < dataRow.ItemArray.Length; k++)
                                {
                                    if (dataRow[k, DataRowVersion.Current].ToString() != dataRow[k, DataRowVersion.Original].ToString())
                                    {
                                        if (ss1.Length <= 0) ss1 += "OriginalData:" + "ID=" + dataRow["ID"].ToString() + ",";
                                        if (ss2.Length <= 0) ss2 += "New     Data:" + "ID=" + dataRow["ID"].ToString() + ",";
                                        ss1 += GlobalDS.Tables[i].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Original].ToString() + ";";
                                        ss2 += GlobalDS.Tables[i].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Current].ToString() + ";";
                                    }
                                }
                                SS += ss1 + "\r\n";
                                SS += ss2 + "\r\n";
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
                        //        ss1 += GlobalDS.Tables[i].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Original].ToString() + " ;";                                    
                        //    }                            
                        //    SS += ss1 + "\r\n";
                        //}                    
                        #endregion
                        //MessageBox.Show(SS);
                        //GlobalDS.Tables[i].AcceptChanges();
                    }
                }
                return SS;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return SS;
            }
        }

        //140529获取指定条件下的parameter数目,Topo与Global各自执行和比较EquipParameter的参数是否一致!
        public static int currPrmtrCountExisted(DataTable mydt,string FullfilterString)
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

        public static int getMAXColumnsItem(DataTable dt,string ColumnName,string queryCMD)
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
                    MessageBox.Show("direction must be +1 or -1");
                    return;
                }

                DataRow[] dr1 = dt.Select(filterStr1);

                if (dr1.Length == 1)
                {
                    dr1[0]["SEQ"] = myPrevRowSEQ;
                }
                else
                {
                    MessageBox.Show("" + dr1.Length + " records has existed!<Violate unique rule>");
                }

                DataRow[] dr2 = dt.Select(filterStr2);

                if (dr2.Length == 1)
                {
                    dr2[0]["SEQ"] = myCurrRowSEQ;
                }
                else
                {
                    MessageBox.Show("" + dr2.Length + " records has existed!<Violate unique rule>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }        

        public static void ResfeshList(DataTable dt, string filterString, ComboBox cbo, string showColumnName)
        {
            try
            {
                cbo.Items.Clear();
                cbo.Text = "";

                DataRow[] mrDRs = dt.Select(filterString);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    cbo.Items.Add(mrDRs[i][showColumnName].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void ResfeshList(DataTable dt, string filterString, ListBox lst, string showColumnName)
        {
            try
            {
                lst.Items.Clear();

                DataRow[] mrDRs = dt.Select(filterString);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    lst.Items.Add(mrDRs[i][showColumnName].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {

                blnIsSQLDB = Login.blnISDBSQLserver;

                //140922_0 ----------------
                this.tsmFuncInfo.Visible = blnIsSQLDB;
                this.tsmRoleInfo.Visible = blnIsSQLDB;
                this.tsmUserInfo.Visible = blnIsSQLDB;
                this.tsmUserRoleFunc.Visible = blnIsSQLDB;
                sslRunMsg.Text = "Operation state!";
                sslInfo.Text = "BackGround";
                //-------------------------

                if (blnIsSQLDB)
                {
                    mySqlIO = new SqlManager(Login.ServerName, Login.DBName, Login.DBUser, Login.DBPassword);   //140917_2    //140911_0
                }
                else
                {
                    mySqlIO = new AccessManager(Login.AccessFilePath);
                }
                this.Text = "ChangeGlaobalInfo Ver:" + Application.ProductVersion + "(DataSource=" + Login.DBName + ")";      //140911_1

                formLoad();
                
                timerDate.Enabled = true;
                ISNeedUpdateflag = false;
                showMCoefGroupStates(false);
                showMSAStates(false);
                showAPPStates(false);
                showEquipStates(false);
                
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

                //mytip.SetToolTip(btnShowMemory, "显示Memory配置信息");
                //mytip.SetToolTip(btnShowMSADefine, "显示MSADefine信息");

                //mytip.SetToolTip(cboPN, "当前类别下存在的机种");
                //mytip.SetToolTip(cboType, "系统中存在的类别");
                //mytip.SetToolTip(cklTestPlan, "当前机种存在的测试计划");
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
                grpMSA.Enabled = true;
                grpAppModel.Enabled = true;
                grpEquip.Enabled = true;
                grpType.Enabled = true;
                grpPN.Enabled = false;               

                initMSA();
                initMCoefGroup();
                initType();
                initAPP();
                initEquip();
                                
                if (Login.blnOnlyToReadFlag)
                {
                    tsmUpdate.Enabled = false;
                    tsmUpdate.Text = "(ReadOnly)";
                }
                this.tsuserInfo.Text="User:" + Login.UserName;

                //140609---------------------------------------
                //载入当前Server 的各表中最后一笔插入记录!
                origIDGlobalEquip = mySqlIO.GetLastInsertData("GlobalAllEquipmentList");
                origIDGlobalEquipPrmtr = mySqlIO.GetLastInsertData("GlobalAllEquipmentParamterList");
                origIDGlobalAPP = mySqlIO.GetLastInsertData("GlobalAllAppModelList");
                origIDGlobalModel = mySqlIO.GetLastInsertData("GlobalAllTestModelList");
                origIDGlobalPrmtr = mySqlIO.GetLastInsertData("GlobalTestModelParamterList");

                origIDGlobalType = mySqlIO.GetLastInsertData("GlobalProductionType");
                origIDGlobalPN = mySqlIO.GetLastInsertData("GlobalProductionName");
                origIDGlobalMSA = mySqlIO.GetLastInsertData("GlobalMSA");
                origIDGlobalMSAPrmtr = mySqlIO.GetLastInsertData("GlobalMSADefintionInf");
                origIDGlobalMCoefGroup = mySqlIO.GetLastInsertData("GlobalManufactureCoefficientsGroup");
                origIDMCoefPrmtr = mySqlIO.GetLastInsertData("GlobalManufactureCoefficients");

                origIDChipCtrl = mySqlIO.GetLastInsertData("GlobalManufactureChipsetControl");
                origIDChipInit = mySqlIO.GetLastInsertData("GlobalManufactureChipsetInitialize");
                origIDEEPROMInit = mySqlIO.GetLastInsertData("GlobalMSAEEPROMInitialize");

                mylastIDGlobalAPP = origIDGlobalAPP ;
                mylastIDTestModel = origIDGlobalModel;
                mylastIDTestPrmtr = origIDGlobalPrmtr ;
                mylastIDGlobalEquip = origIDGlobalEquip;
                mylastIDGlobalEquipPrmtr = origIDGlobalEquipPrmtr;

                mylastIDGlobalType = origIDGlobalType;
                mylastIDGlobalPN = origIDGlobalPN;
                mylastIDGlobalMSA = origIDGlobalMSA;
                mylastIDGlobalMSAPrmtr = origIDGlobalMSAPrmtr;

                //140704_4 >>>>>>>>>>>>>>>>>>
                mylastIDGlobalMCoefGroup = origIDGlobalMCoefGroup;  
                mylastIDGlobalMCoefPrmtr = origIDMCoefPrmtr;        

                mylastIDGlobalChipCtrl = origIDChipCtrl;
                mylastIDGlobalChipInit = origIDChipInit;
                mylastIDGlobalEEPROMInit = origIDEEPROMInit;
                
                InitinalALLTablesInfo();   //获取所有表的信息!
                refreshAllItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void refreshAllItem()
        {
            try
            {
                //MSADefine
                this.ListMSA.Items.Clear();
                for (int i = 0; i < GlobalDS.Tables["GlobalMSA"].Rows.Count; i++)
                {
                    if (GlobalDS.Tables["GlobalMSA"].Rows[i].RowState != DataRowState.Deleted)  //140911_2 修正删除后返回刷新报错的问题
                    {
                        this.ListMSA.Items.Add(GlobalDS.Tables["GlobalMSA"].Rows[i]["ItemName"].ToString());
                    }
                    grpMSA.Enabled = true;  //140923_0
                    //showMSAStates(true);
                }

                //MCoefGroup
                this.listMCoefGroup.Items.Clear();
                for (int i = 0; i < GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows.Count; i++)
                {
                    if (GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i].RowState != DataRowState.Deleted)  //140911_2 修正删除后返回刷新报错的问题
                    {
                        this.listMCoefGroup.Items.Add(GlobalDS.Tables["GlobalManufactureCoefficientsGroup"].Rows[i]["ItemName"].ToString());
                    }
                        this.grpMCoefGroup.Enabled = true;
                    //showTypeStates(true);
                }

                //Type + PN
                this.cboType.Items.Clear();
                this.cboType.Text = "";
                for (int i = 0; i < GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                {
                    if (GlobalDS.Tables["GlobalProductionType"].Rows[i].RowState != DataRowState.Deleted)  //140911_2 修正删除后返回刷新报错的问题
                    {
                        this.cboType.Items.Add(GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString());
                    }
                    //grpPN.Enabled = true;   //140923_0 delete
                    //showTypeStates(true);
                }

                //GlobalAllAppModelList
                this.listApp.Items.Clear();
                //DataRow[] CurrLst = MainForm.GlobalDS.Tables["GlobalAllAppModelList"].Select();
                //// = PNInfo.mySqlIO.GetDataTable("", "");
                //foreach (DataRow dr in CurrLst)
                //{
                //    listApp.Items.Add(dr["ItemName"]);
                //}
                for (int i = 0; i < GlobalDS.Tables["GlobalAllAppModelList"].Rows.Count; i++)
                {
                    if (GlobalDS.Tables["GlobalAllAppModelList"].Rows[i].RowState != DataRowState.Deleted)  //140911_2 修正删除后返回刷新报错的问题
                    {
                        this.listApp.Items.Add(GlobalDS.Tables["GlobalAllAppModelList"].Rows[i]["ItemName"].ToString());
                    }
                    //grpPN.Enabled = true;
                    //showAPPStates(true);
                }

                //GlobalAllEquipmentList
                this.listEquip.Items.Clear();
                for (int i = 0; i < GlobalDS.Tables["GlobalAllEquipmentList"].Rows.Count; i++)
                {
                    if (GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i].RowState != DataRowState.Deleted)  //140911_2 修正删除后返回刷新报错的问题
                    {
                        this.listEquip.Items.Add(GlobalDS.Tables["GlobalAllEquipmentList"].Rows[i]["ItemName"].ToString());
                    }
                    //grpPN.Enabled = true;
                    //showEquipStates(true);
                }
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
                GlobalDS = new DataSet("GlobalTotalDS");
                string TabName="";

                for (int i = 0; i < ConstGlobalListTables.Length; i++)
                {
                    TabName=ConstGlobalListTables[i];
                    GlobalDS.Tables.Add(GetTestPlanInfo(TabName, ""));
                }

                for (int i = 0; i < ConstGlobalPNInfo.Length; i++)
                {
                    TabName = ConstGlobalPNInfo[i];
                    GlobalDS.Tables.Add(GetTestPlanInfo(TabName, ""));
                }

                for (int i = 0; i < ConstGlobalMCoefGroupInfo.Length; i++)
                {
                    TabName = ConstGlobalMCoefGroupInfo[i];
                    GlobalDS.Tables.Add(GetTestPlanInfo(TabName, ""));
                }

                for (int i = 0; i < ConstMSAItemTables.Length; i++)
                {
                    
                    TabName = ConstMSAItemTables[i];
                    if (TabName.ToUpper() != "GlobalProductionType".ToUpper())
                    {
                        GlobalDS.Tables.Add(GetTestPlanInfo(TabName, ""));
                    }
                }

                //140704_4
                for (int i = 0; i < MainForm.ConstGlobalPNInit.Length; i++)
                {
                    TabName = ConstGlobalPNInit[i];
                    GlobalDS.Tables.Add(GetTestPlanInfo(TabName, ""));
                }

                //TopoToatlDS.Tables[1].Columns["ItemName"].Unique = true; //字段唯一
                //TopoToatlDS.Tables[1].Columns["ItemName"].AllowDBNull = false;// 不允许为空
                //TopoToatlDS.Tables[1].Columns["ItemName"].AutoIncrement = false;//自动递增

                
                for (int j = 0; j < GlobalDS.Tables.Count; j++)
                {
                    GlobalDS.Tables[j].PrimaryKey = new DataColumn[] { GlobalDS.Tables[j].Columns["ID"] };
                }

                int k=0,m=0;
                //ConstMSAItemTables { "GlobalMSA" , "GlobalMSADefintionInf" };
                string TabName1 = ConstMSAItemTables[k];
                string TabName2 = ConstMSAItemTables[k + 1];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;

                //140630_1
                //k = 1;
                //TabName1 = ConstMSAItemTables[k];
                //TabName2 = ConstMSAItemTables[k + 1];
                //SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "MSAID", GlobalDS.Tables[TabName2], "PID");
                //m++;

                //ConstGlobalPNInfo { "GlobalProductionType", "GlobalProductionName", "GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficients" };
                k = 0;
                TabName1 = ConstGlobalPNInfo[k];
                TabName2 = ConstGlobalPNInfo[k + 1];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;

                //140703_4 >>>>>>>>>>>>>>>>>
                k = 0;
                TabName1 = ConstGlobalMCoefGroupInfo[k];
                TabName2 = ConstGlobalMCoefGroupInfo[k + 1];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;

                TabName1 = "GlobalManufactureCoefficientsGroup";
                TabName2 = "GlobalProductionName";
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "MCoefsID");
                m++;
                //140703_4 <<<<<<<<<<<<<<<<<

                //140704_4 >>>>>>>>>>>>>>>>>
                TabName1 = "GlobalProductionName";
                TabName2 = ConstGlobalPNInit[0];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;
                TabName1 = "GlobalProductionName";
                TabName2 = ConstGlobalPNInit[1];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;
                TabName1 = "GlobalProductionName";
                TabName2 = ConstGlobalPNInit[2];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;
                //140703_4 <<<<<<<<<<<<<<<<<

                //ConstGlobalListTables  { "GlobalAllEquipmentList", "GlobalAllEquipmentParamterList", "GlobalAllAppModelList", "GlobalAllTestModelList", "GlobalTestModelParamterList" };
                k = 0;
                TabName1 = ConstGlobalListTables[k];
                TabName2 = ConstGlobalListTables[k + 1];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;
                k = 2;
                TabName1 = ConstGlobalListTables[k];
                TabName2 = ConstGlobalListTables[k + 1];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;
                k = 3;
                TabName1 = ConstGlobalListTables[k];
                TabName2 = ConstGlobalListTables[k + 1];
                SetRelation(GlobalDS, m, GlobalDS.Tables[TabName1], "ID", GlobalDS.Tables[TabName2], "PID");
                m++;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        void SetRelation(DataSet DS, int RelationsID, DataTable dt1, string Column1, DataTable dt2, string Column2)
        {
            try
            {
                DS.Relations.Add("relation_" + RelationsID, dt1.Columns[Column1], dt2.Columns[Column2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        void initMCoefGroup()
        {
            this.listMCoefGroup.Items.Clear();
        }

        void initType()
        {
            try
            {
                this.cboType.Items.Clear();
                this.cboType.Text = "";
                this.btnShowMSA.Enabled = false;
                btnPNAdd.Enabled = false;   //140923_0
                showTypeStates(false);
                initPN();     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        void initPN()
        {
            try
            {                
                this.listPN.Items.Clear();
                showPNStates(false);
                grpPN.Enabled = false;  //141016_0
                btnShowMemory.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        void showMSAStates(bool state)
        {
            try
            {
                btnMSAEdit.Enabled = state;
                btnMSADelete.Enabled = state;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showTypeStates(bool state)
        {
            try
            {
                btnTypeEdit.Enabled = state;
                btnTypeDelete.Enabled = state;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showPNStates(bool state)
        {
            try
            {
                if (state)
                {
                    btnPNEdit.Enabled = state;
                    btnPNDelete.Enabled = state;
                }
                else
                {
                    btnPNEdit.Enabled = false;
                    btnPNDelete.Enabled = false;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showAPPStates(bool state)
        {
            try
            {
                this.btnAppEdit.Enabled = state;
                this.btnAppDelete.Enabled = state;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showEquipStates(bool state)
        {
            try
            {
                btnEquipEdit.Enabled = state;
                btnEquipDelete.Enabled = state;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        void initMSA()
        {
            try
            {
                this.ListMSA.Items.Clear();
                showMSAStates(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void initAPP()
        {
            try
            {
                this.listApp.Items.Clear(); 
                showAPPStates(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        void initEquip()
        {
            try
            {
                this.listEquip.Items.Clear();
                showEquipStates(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
                
        //----------------------
        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            currTypeID = -999;
            try
            {
                if (cboType.SelectedIndex != -1)
                {
                    initPN();   //141016_0
                    btnPNAdd.Enabled = true;   //140923_0
                    listPN.Enabled = true;
                    for (int i = 0; i <  GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                    {
                        if (GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString().ToUpper() == cboType.Text.ToString().ToUpper())
                        {
                            currTypeID = Convert.ToInt64(GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"]);
                            break;
                        }
                    }

                    if (currTypeID == -999)
                    {
                        MessageBox.Show("The result of query '" + cboType.Text.ToString().ToUpper() + "' was not existed!");
                        //141016_0  //initPN();
                        return;
                    }
                    else
                    {
                        this.listPN.Items.Clear();
                        string sqlCondition = "PID=" + currTypeID;
                        showTypeStates(true);
                        this.btnShowMSA.Enabled = true;
                        DataRow[] mrDRs = GlobalDS.Tables["GlobalProductionName"].Select(sqlCondition);
                        for (int i = 0; i < mrDRs.Length; i++)
                        {
                            listPN.Items.Add(mrDRs[i]["PN"].ToString());                            
                        }
                       
                        grpPN.Enabled = true;                        
                    }
                }
                else
                {
                    MessageBox.Show("Pls choose a 'PNType' first!");
                    initType();    
                }
            }
            catch (Exception ex)
            {
                initType();                
                MessageBox.Show(" Error,Pls Check Again \n" + ex.ToString());
            }
        }        

        void showMfMemory(bool ISNeedShow)
        {
            try
            {
                if (ISNeedShow)
                {
                    if (this.listPN.SelectedItem.ToString().Trim().Length > 0)
                    {
                        long MCoefsID = Convert.ToInt64(MainForm.getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "MCoefsID", "PN='" + this.listPN.SelectedItem.ToString() + "'"));
                        currPNID = Convert.ToInt64(getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + listPN.SelectedItem.ToString() + "'"));
                        
                        ///获取GlobalManufactureCoefficients
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
                        myManufactureMemoryForm.dgvMSAEEPROMInitialize.DataSource = GlobalMSAEEPROMInitializeDT;
                        hideMyIDPID(myManufactureMemoryForm.dgvMSAEEPROMInitialize);
                       

                        resizeDGV(myManufactureMemoryForm.dgvGlobalManufactureMemory);
                        resizeDGV(myManufactureMemoryForm.dgvManufactureChipsetControl);
                        resizeDGV(myManufactureMemoryForm.dgvManufactureChipsetInitialize);
                        resizeDGV(myManufactureMemoryForm.dgvMSAEEPROMInitialize);

                        myManufactureMemoryForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Pls choose a 'PN' from PNList first!");
                    }
                }
                else
                {
                    
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
                    if (cboType.Text.ToString().Trim().Length > 0)
                    {
                        long MSAID = Convert.ToInt64(MainForm.getDTColumnInfo(GlobalDS.Tables["GlobalProductionType"], "MSAID", "ItemName='" + this.cboType.SelectedItem.ToString() + "'"));
                        
                        //获取GlobalManufactureCoefficients
                        string sqlCondition = " where PID=" + MSAID;//+"'";
                        GlobalMSADefineDT = new DataTable();

                        GlobalMSADefineDT = GetTestPlanInfo("GlobalMSADefintionInf", sqlCondition);
                        MASDefine myMASDefine = new MASDefine();
                        myMASDefine.grpMSADefine.Text = cboType.Text.ToString().ToUpper();
                        myMASDefine.dgvMSADefine.DataSource = GlobalMSADefineDT;
                        //hideMyIDPID(myMASDefine.dgvMSADefine); //140609
                        myMASDefine.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Pls choose a 'PNType' first!");
                    }
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnShowMemory_Click(object sender, EventArgs e)
        {                           
            showMfMemory(true);           
        }
        
        void showAPPInfoForm(bool isNewForm)
        {
            try
            {
                APPInfo myAPPInfo = new APPInfo();
                myAPPInfo.blnAddNew = isNewForm;
                this.Hide();    //140706_2                
                myAPPInfo.ShowDialog();                
                this.Show();    //140706_2
                refreshAllItem();   //140708_1
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showEquipForm(bool isNewForm)
        {
            try
            {
                EquipmentForm myEquipmentForm = new EquipmentForm();
                myEquipmentForm.blnAddNewEquip = isNewForm;
                this.Hide();    //140706_2
                myEquipmentForm.ShowDialog();
                this.Show();    //140706_2
                refreshAllItem();   //140708_1
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool DeleteSelectItem(ListBox mylst, DataTable dt, string deleteQueryCMD)
        {
            bool OPResult = false;
            try
            {
                if (mylst.SelectedIndex != -1)
                {
                    string myDeleteItemName = mylst.SelectedItem.ToString();
                    DialogResult drst = new DialogResult();
                    
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        int CurrIndex = mylst.SelectedIndex;
                        
                        mylst.Items.RemoveAt(CurrIndex);
                        
                        bool result = DeleteItemForDT(dt, myDeleteItemName);
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            OPResult = result;
                            return OPResult;
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete failed !");
                            return OPResult;
                        }
                    }
                    else
                    {
                        return true;
                    }
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

        bool DeleteSelectItem(ComboBox mycbo, DataTable dt, string deleteQueryCMD)
        {
            bool OPResult = false;
            try
            {
                if (mycbo.SelectedIndex != -1)
                {
                    string myDeleteItemName = mycbo.SelectedItem.ToString();
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        int CurrIndex = mycbo.SelectedIndex;

                        mycbo.Items.RemoveAt(CurrIndex);
                        
                        bool result = DeleteItemForDT(dt, myDeleteItemName);
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            OPResult = result;
                            return OPResult;
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete failed !");
                            return OPResult;
                        }
                    }
                    else
                    {
                        return true;
                    }
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

        private void btnShowMSADefine_Click(object sender, EventArgs e)
        {
            showMSADefine(true);            
        }

        void updateStutes(bool value)
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
                    MessageBox.Show("The LoginName = ->" + Login.UserName + " not existed!");
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
                    updateUserLoginInfo(mySqlIO.GetCurrTime().ToString(), true, "");    
                }                
                this.Dispose();
                GC.Collect();   //140707_0
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
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

        private void tsmCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drst = new DialogResult();
                drst = (MessageBox.Show("System will Load the all Data from the Server!", "Notice"
                    , MessageBoxButtons.YesNo)); 
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

        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string time1 = mySqlIO.GetCurrTime().ToString();
                sslRunMsg.Text = "System is updating the data Now...Pls wait...." + time1;
                ssrRunMsg.Refresh();
                if (GlobalDS.GetChanges() == null)
                {
                    MessageBox.Show("Nothing is modified...Pls click this button after modifiy anything...");
                    return;
                }
                this.Enabled = false;
                tsmUpdate.Enabled = false;

                bool myResult = mySqlIO.UpdateDT();

                string time2 = mySqlIO.GetCurrTime().ToString();
                TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(time1).Ticks);
                TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(time2).Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                string dateDiff = " Use:" + ts.Minutes.ToString() + " Minutes and" + ts.Seconds.ToString() + "." + ts.Milliseconds.ToString() + " Seconds";

                if (myResult)
                {
                    getAndSaveAllItemLogs(time2);    //后续在确认是否需要判定存储logOK?

                    formLoad();
                    sslRunMsg.Text = "Update data successful." + time2 + ":" + dateDiff;
                    ssrRunMsg.Refresh();
                }
                else
                {
                    if (blnIsSQLDB)
                    {
                        sslRunMsg.Text = "Update data Failed!System RollBack data." + time2;//+ ":" + dateDiff;      //141021_1
                        MessageBox.Show(sslRunMsg.Text);
                    }
                    else
                    {
                        sslRunMsg.Text = "Update data Failed!System RollBack data." + time2;// + ":" + dateDiff;       //141021_1 //140616
                        MessageBox.Show(sslRunMsg.Text);
                    }
                }
                ISNeedUpdateflag = false;
                tsmUpdate.Enabled = true;
                //myOPlog += "Update data Failed!System RollBack datas ..." + time2 + ":" + dateDiff;  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                tsmUpdate.Enabled = true;
                this.Enabled = true;
            }
        }
        
        private void tsmRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drst = new DialogResult();
                drst = (MessageBox.Show("System will Refresh the all Data from the Server!", "Notice", MessageBoxButtons.YesNo));
                if (drst == DialogResult.Yes)
                {
                    formLoad(); //TBD ---140606
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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
                        e.Cancel =true;
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
               
        //TBD140606------------------
        private void listPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listPN.SelectedIndex != -1)
                {
                    showPNStates(true);
                    btnShowMemory.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListMSA_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListMSA.SelectedIndex != -1)
                {
                    showMSAStates(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listApp.SelectedIndex != -1)
                {
                    showAPPStates(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listEquip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listEquip.SelectedIndex != -1)
                {
                    showEquipStates(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnMSAAdd_Click(object sender, EventArgs e)
        {
            showMSAInfoForm(true);
        }

        private void btnMSAEdit_Click(object sender, EventArgs e)
        {
            showMSAInfoForm(false);
        }

        private void btnMSADelete_Click(object sender, EventArgs e)
        {
            if (this.ListMSA.SelectedIndex == -1)
            {
                MessageBox.Show("Pls choose a item of MSAInfo first!");
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = this.ListMSA.SelectedItem.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));
                    currMSAID = getDTColumnInfo(GlobalDS.Tables["GlobalMSA"], "ID", "ItemName='" + ListMSA.SelectedItem.ToString() + "'");

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalMSA"], "ID=" + currMSAID + "and ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalMSA"], "", ListMSA, "ItemName");
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnAppAdd_Click(object sender, EventArgs e)
        {
            showAPPInfoForm(true);   
        }

        private void btnAppEdit_Click(object sender, EventArgs e)
        {
            showAPPInfoForm(false);
        }

        private void btnAppDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listApp.SelectedIndex != -1)
                {
                    string myDeleteItemName = listApp.SelectedItem.ToString();
                    int CurrIndex = this.listApp.SelectedIndex;
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));
                    

                    if (drst == DialogResult.Yes)
                    {
                        
                        
                        //DataTable资料移除部分待新增!!!

                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalAllAppModelList"], "ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true; //140603_2
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalAllAppModelList"], "", listApp, "ItemName");   //140911_2 修复未显示刷新的问题!
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnEquipAdd_Click(object sender, EventArgs e)
        {
            showEquipForm(true);
        }

        private void btnEquipEdit_Click(object sender, EventArgs e)
        {
            showEquipForm(false);
        }

        private void btnEquipDelete_Click(object sender, EventArgs e)   //140911_2 修复未执行的问题!
        {
            try
            {
                if (this.listEquip.SelectedIndex != -1)
                {
                    string myDeleteItemName = listEquip.SelectedItem.ToString();
                    int CurrIndex = this.listEquip.SelectedIndex;
                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));
                    
                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"], "ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true; 
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalAllEquipmentList"], "", listEquip, "ItemName");   
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void btnShowMSA_Click(object sender, EventArgs e)
        {
            showMSADefine(true);
        }

        private void btnShowMemory_Click_1(object sender, EventArgs e)
        {
            showMfMemory(true);
        }

        private void tsmUserInfo_Click(object sender, EventArgs e)
        {
            UserInfo myUserForm = new UserInfo();
            myUserForm.ShowDialog();
        }

        private void tsmRoleInfo_Click(object sender, EventArgs e)
        {
            RoleInfo myRoleForm = new RoleInfo();
            myRoleForm.ShowDialog();
        }

        private void tsmFuncInfo_Click(object sender, EventArgs e)
        {
            FunctionInfo myFuncForm = new FunctionInfo();
            myFuncForm.ShowDialog();
        }

        private void tsmUserRole_Click(object sender, EventArgs e)
        {
            UserRoleFunctionInfo myForm = new UserRoleFunctionInfo();
            myForm.ShowDialog();
        }

        void showTypeForm(bool isNewForm)
        {
            
            try
            {                
                TypeForm myForm = new TypeForm();
                myForm.PTypeName = this.cboType.Text.ToString();
               
                myForm.blnAddNew = isNewForm;
                myGlobalTypeISNewFlag = isNewForm;
                this.Hide();    //140706_2
                myForm.ShowDialog();
                
                this.Show();    //140706_2
                ResfeshList(MainForm.GlobalDS.Tables["GlobalProductionType"],"",this.cboType, "ItemName");
                initPN();   //140917_0
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
        void showPNForm(bool isNewForm)
        {
            
            try
            {
                PNInfoForm myPNInfoForm = new PNInfoForm();
                myPNInfoForm.blnAddNew = isNewForm;
                myPNInfoForm.PID = Convert.ToInt64(MainForm.getDTColumnInfo(MainForm.GlobalDS.Tables["GlobalProductionType"], "ID", "ItemName='" + this.cboType.Text.ToString() + "'"));
                this.Hide();   
                myPNInfoForm.ShowDialog();   //show NextForm...
                this.Show();   
                ResfeshList(MainForm.GlobalDS.Tables["GlobalProductionName"], "PID=" + myPNInfoForm.PID, this.listPN, "PN");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        void showMSAInfoForm(bool isNewForm)
        {
            try
            {
                MSAInfo myMSAInfo = new MSAInfo();
                if (isNewForm == false) 
                { 
                    myMSAInfo.MSAName = this.ListMSA.SelectedItem.ToString();
                }
                myMSAInfo.blnAddNew = isNewForm;
                this.Hide();   
                myMSAInfo.ShowDialog();
                this.Show();    
              
                refreshAllItem();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showMCoefGroupForm(bool isNewForm)
        {
            try
            {
                MCoefGroup myMCoefGroup = new MCoefGroup();
                if (isNewForm == false)
                {
                    myMCoefGroup.MCoefGroupName = this.listMCoefGroup.SelectedItem.ToString();
                }
                myMCoefGroup.blnAddNew = isNewForm;
                this.Hide();   
                myMCoefGroup.ShowDialog();
                this.Show();    
                refreshAllItem();   

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void showMCoefGroupStates(bool states)
        {
            btnMCoefGroupDelete.Enabled = states;
            btnMCoefGroupEdit.Enabled = states;
        }

        private void btnTypeAdd_Click(object sender, EventArgs e)
        {
            if (cboType.Text.Trim().Length > 0)
            {
                showTypeForm(true);
            }
            else
            {
                cboType.Focus();
            }                    
        }

        private void btnTypeEdit_Click(object sender, EventArgs e)
        {
            showTypeForm(false);
        }

        private void btnTypeDelete_Click(object sender, EventArgs e)
        {
            if (this.cboType.SelectedIndex == -1)
            {
                MessageBox.Show("Pls choose a 'PNType' first!");
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = this.cboType.SelectedItem.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    currTypeID =  Convert.ToInt64(getDTColumnInfo(GlobalDS.Tables["GlobalProductionType"], "ID", "ItemName='" + myDeleteItemName + "'"));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalProductionType"], "ID=" + currTypeID + "and ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalProductionType"], "", this.cboType, "ItemName");   //140923_0 this.cboType
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void btnPNAdd_Click(object sender, EventArgs e)
        {
            showPNForm(true);
        }

        private void btnPNEdit_Click(object sender, EventArgs e)
        {
            showPNForm(false);
        }

        private void btnMCoefGroupAdd_Click(object sender, EventArgs e)
        {
            showMCoefGroupForm(true);
        }

        private void btnMCoefGroupEdit_Click(object sender, EventArgs e)
        {
            showMCoefGroupForm(false);
        }

        private void btnMCoefGroupDelete_Click(object sender, EventArgs e)
        {
            if (this.listMCoefGroup.SelectedIndex == -1)
            {
                MessageBox.Show("Pls choose a 'MCoefGroup' first!");
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = this.listMCoefGroup.SelectedItem.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));
                    currMCoefGroupID = getDTColumnInfo(GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + listMCoefGroup.SelectedItem.ToString() + "'");

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID=" + currMCoefGroupID + "and ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "", listMCoefGroup, "ItemName");
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void listMCoefGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMCoefGroup.SelectedIndex !=-1)
            {
                showMCoefGroupStates(true);
                currMCoefGroupID = getDTColumnInfo(GlobalDS.Tables["GlobalManufactureCoefficientsGroup"], "ID", "ItemName='" + listMCoefGroup.SelectedItem.ToString() +"'");
            }
        }

        private void btnPNDelete_Click(object sender, EventArgs e)
        {
            if (this.listPN.SelectedIndex == -1)
            {
                MessageBox.Show("Pls choose a 'PN' first!");
            }
            else
            {
                try
                {
                    DialogResult drst = new DialogResult();
                    string myDeleteItemName = this.listPN.SelectedItem.ToString();
                    drst = (MessageBox.Show("Delete Item -->" + myDeleteItemName + "\n \n Pls choose 'Y' (是) to continue?",
                        "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));

                    currPNID = Convert.ToInt64( getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + listPN.SelectedItem.ToString() + "'"));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = MainForm.DeleteItemForDT(MainForm.GlobalDS.Tables["GlobalProductionName"], "ID=" + currPNID + "and PN='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MainForm.ISNeedUpdateflag = true;
                            MessageBox.Show("Item: " + myDeleteItemName + " has been deleted successful!");
                            MainForm.ResfeshList(MainForm.GlobalDS.Tables["GlobalProductionName"], "", listPN, "PN");
                        }
                        else
                        {
                            MessageBox.Show("Item: " + myDeleteItemName + " delete Failed!");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void resizeDGV(DataGridView dgv)
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

        void updateUserLoginInfo(string loginOfftime, bool isLoginOFF, string logs)
        {
            try
            {
                DataTable userLoginInfoDt = mySqlIO.GetDataTable("select * from UserLoginInfo", "UserLoginInfo");
                DataRow[] dr = userLoginInfoDt.Select("ID=" + myLoginID);
                string hostname = System.Net.Dns.GetHostName(); //主机
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
                string currTime = mySqlIO.GetCurrTime().ToString();
                if (dr.Length == 1)
                {
                    if (loginOfftime.Trim().Length > 0)
                    {
                        dr[0]["LoginOfftime"] = currTime;
                    }
                    if (isLoginOFF)
                    {
                        dr[0]["LoginInfo"] = "Login Name =" + Login.UserName + " log off successfully at computer=" + hostname + "[" + IP4 + "]" + ";";
                    }
                    if (logs.Trim().Length > 0)
                    {
                        dr[0]["OPLogs"] = logs;
                    }

                    mySqlIO.UpdateDataTable("select * from UserLoginInfo", userLoginInfoDt);
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

        void updateDetailLogs(string modifyTime,string [] opType, string [] logs)   //141112_1
        {
            try
            {
                DataTable detailLogsDT = mySqlIO.GetDataTable("select * from operationLogs", "operationLogs");
                DataRow dr = detailLogsDT.NewRow();
                string allOpType = "";
                string allLogs = "";
                for (int i = 0; i < opType.Length; i++)
                {
                    if (opType[i].Trim().Length > 0)
                    {
                        if (i + 1 < opType.Length)
                        {
                            allOpType += opType[i].Trim() + "\r\n"; //141113_0
                        }
                        else
                        {
                            allOpType += opType[i].Trim();
                        } 
                    }
                    if (logs[i].Trim().Length > 0)
                    {
                        if (i + 1 < logs.Length)
                        {
                            allLogs += logs[i].Trim() + "\r\n"; //141113_0
                        }
                        else
                        {
                            allLogs += logs[i].Trim();
                        } 
                    }
                }
                if (allOpType.Trim().Length > 0)
                {
                    dr["PID"] = myLoginID;
                    dr["ModifyTime"] = modifyTime;
                    dr["Optype"] = allOpType;
                    dr["DetailLogs"] = allLogs;
                    detailLogsDT.Rows.Add(dr);
                    mySqlIO.UpdateDataTable("select * from operationLogs", detailLogsDT);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Get or Save OperationLogs Error: --> \n" +ex.ToString());
            }
        }

        bool getAndSaveAllItemLogs(string time0)
        {
            try
            {
                string[] MSAOPlogs;
                string[] MSAdetailLogs = getMSAChangeLog(out MSAOPlogs);
                string[] MCoefOPlogs;
                string[] MCoefdetailLogs = getMCoefGroupChangeLog(out MCoefOPlogs);

                string[] EQOPlogs;
                string[] EQdetailLogs = getEquipChangeLog(out EQOPlogs);
                string[] PNOPlogs;
                string[] PNdetailLogs = getPNInfoChangeLog(out PNOPlogs);
                string[] AppOPlogs;
                string[] AppdetailLogs = getAPPModelChangeLog(out AppOPlogs);

                //ForDebug-----------------
                string ss = DSAcceptChanges();
                writeAllTableChangesLogToLocal(ss);
                //--------------------------

                updateDetailLogs(time0, MSAOPlogs, MSAdetailLogs);
                updateDetailLogs(time0, MCoefOPlogs, MCoefdetailLogs);
                updateDetailLogs(time0, EQOPlogs, EQdetailLogs);
                updateDetailLogs(time0, PNOPlogs, PNdetailLogs);
                updateDetailLogs(time0, AppOPlogs, AppdetailLogs);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        //*********************************************************
    }
}
