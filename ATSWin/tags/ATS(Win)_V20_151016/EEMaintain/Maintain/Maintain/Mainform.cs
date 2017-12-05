using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ATSDataBase;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.IO;
using Authority;

namespace Maintain
{
    public partial class MainForm : Form
    {
        DataIO myDataIO;
        string user = "";
        string myLoginID = "";
        
        bool isMaintainUser = false;
        bool isReadEEPROM = false;

        bool blnISDBSQLserver = true; // 默认为DBSQLServer
        DataSet GlobalDS; // 缓存
        EEPROMOperation myEEPROMOperation;
        string EEPROMTabPID = "-1";
        int showFristRowID = 0;
        int showRowsCount = 256;
        string strNewFileName = "";
        string strPageNo = "";
        string dataname = "";
        string[,] dataInfo = new string[2, 256];
        string[] copyDataInfo = new string[256];
        bool isReadOnly = true;
        bool isCopy = false;
        
        string GlobalCopyPID = "-1";
        string GlobalCopyItemName = "";

        byte[] dataVaules = new byte[256];
        byte[] crcdataVaules = new byte[256];
        private float X;
        private float Y;

        int myLastPageNo = -1;       

        ToolTip mytip = new ToolTip();
       
        string[] ConstGlobalListTables = new string[] { "GlobalProductionType", "GlobalProductionName", "TopoMSAEEPROMSet" };

        bool blnAddNew = false;
        int mylastIDEEPROMID = -1;

        byte[] Data0 = new byte[256];   //默认EEPROM 出货资料
        byte[] Data1 = new byte[256];
        byte[] Data2 = new byte[256];
        byte[] Data3 = new byte[256];

        // 全局索引记住变量
        int GlobalTempPNTypeIndex = -1;
        int GlobalTempPNIndex = -1;
        int GlobalTempLstFNmIndex = -1;

        string tempDgvCurrCellValue = "FF";
        LoginInfoStruct myLoginInfoStruct;
        public MainForm(LoginInfoStruct pLoginInfoStruct)
        {
            myLoginInfoStruct = pLoginInfoStruct;
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            ValidationRule pValidationRule = new ValidationRule();
            //150115_0 验证权限变更...
            isReadEEPROM = pValidationRule.CheckLoginAccess(LoginAppName.EEPROMMaintain, CheckAccess.ViewEEPROM, myLoginInfoStruct.myAccessCode);
            isMaintainUser = pValidationRule.CheckLoginAccess(LoginAppName.EEPROMMaintain, CheckAccess.MofifyEEPROM, myLoginInfoStruct.myAccessCode);

            blnISDBSQLserver = myLoginInfoStruct.blnISDBSQLserver;
            user = myLoginInfoStruct.UserName;

            if (isReadEEPROM || isMaintainUser)
            {
            }
            else
            {
                MessageBox.Show("The current user doesn't have login permission, pls confirm!");
                this.Close();
            }

            if (blnISDBSQLserver)
            {
                myDataIO = new SqlManager(myLoginInfoStruct.ServerName, myLoginInfoStruct.DBName, myLoginInfoStruct.DBUser, myLoginInfoStruct.DBPassword);
            }
            else
            {
                myDataIO = new AccessManager(myLoginInfoStruct.AccessFilePath);
            }

            myLoginID = updateUserLoginInfo();  //141027_0

            formLoad();
            //在Form_Load里面添加:
            this.Resize += new EventHandler(Form_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            Form_Resize(new object(), new EventArgs());//x,y可在实例化时赋值,最后这句是新加的，在MDI时有用
            this.WindowState = FormWindowState.Maximized;
            
        }

        /// <summary>
        /// 初始化界面信息...
        /// </summary>
        void formLoad()
        {
            try
            {
                if (isMaintainUser)
                {
                    tsbtnEdit.Visible = true;
                    tsbtnEdit.Enabled = false;
                    tsbtnCancel.Visible = false;
                }
                else
                {
                    tsbtnEdit.Visible = false;
                    tsbtnCancel.Visible = false;

                    toolStrip1.Enabled = false;
                    cmsLstFNm.Enabled = false;
                    cmsLstFNm.Visible = false;
                }

                timerDate.Enabled = true;
                this.Text = "EEPROM Maintain Version:" + Application.ProductVersion + " (DataSoure=" + myLoginInfoStruct.DBName + ")";      //140912_0
                this.tsuserInfo.Text = "  User:" + user + "[Login time:" + DateTime.Now.ToString() + "]  ";

                this.cboPNType.Items.Clear();
                this.cboPNType.Text = "";
                initPN();

                cboPNType.SelectedIndex = -1;
                cboPN.SelectedIndex = -1;
                lstFileName.SelectedIndex = -1;

                // Disable PN、FileName
                cboPN.Enabled = false;
                lstFileName.Enabled = false;
                tabEEPROMInfo.Enabled = false;

                //if (listBoxFileName.Enabled)
                //{
                LoadTabPage();
                //}
                //this.dgvPageData.ColumnHeadersVisible = false;

                //if (isMaintainUser == false)
                //{
                //    this.tsmUpdate.Visible = false;
                //    this.tsmSave.Visible = false;
                //    this.tsmDelete.Visible = false;
                //    this.tsmNew.Visible = false;
                //    this.tsmCancel.Visible = false;
                //}
                //else
                //{
                //    this.tsmUpdate.Visible = true;
                //    this.tsmSave.Visible = true;
                //    this.tsmDelete.Visible = true;
                //    this.tsmNew.Visible = true;
                //    this.tsmCancel.Visible = true;
                //}

                this.tsbtnRefresh.Visible = true;
                this.tsbtnUpdate.Visible = true;
                this.tsbtnSave.Visible = true;
                this.tsbtnDelete.Visible = true;
                this.tsbtnNew.Visible = true;
                this.tsbtnCancel.Visible = true;

                // 初始化工具栏
                this.tsbtnRefresh.Enabled = true;
                this.tsbtnUpdate.Enabled = true;
                this.tsbtnSave.Visible = false;
                this.tsbtnSave.Enabled = false;
                this.tsbtnDelete.Enabled = false;
                this.tsbtnNew.Enabled = false;
                this.tsbtnCancel.Enabled = false;
                this.tsbtnCancel.Visible = false;
                //this.txtAllDescription.Enabled = false;

                if (getDSInfo() == false)
                {
                    MessageBox.Show("Fail to get data...");
                    sslRunMsg.Text = "Fail to get data... Time: " + DateTime.Now.ToString();
                    runMsg.Refresh();
                    return;
                }
                else
                {
                    sslRunMsg.Text = "Getting data successful!!! Time: " + DateTime.Now.ToString();
                    runMsg.Refresh();
                }
                sslRunMsg.Text = "User login successful! Login Time: " + DateTime.Now.ToString();
                runMsg.Refresh();
            }
            catch (Exception ex)
            {
                GlobalDS.Tables.Clear();
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns></returns>
        bool getDSInfo()
        {
            try
            {
                runMsgState((byte)MsgState.Clear);
                GlobalDS = new DataSet("GlobalDS");
                for (int i = 0; i < ConstGlobalListTables.Length; i++)
                {
                    string queryConditions = "select * from " + ConstGlobalListTables[i];
                    GlobalDS.Tables.Add(myDataIO.GetDataTable(queryConditions, ConstGlobalListTables[i]));
                }
                GlobalDS.Tables[0].PrimaryKey = new DataColumn[] { GlobalDS.Tables[0].Columns["ID"] };
                GlobalDS.Tables[1].PrimaryKey = new DataColumn[] { GlobalDS.Tables[1].Columns["ID"] };
                GlobalDS.Tables[2].PrimaryKey = new DataColumn[] { GlobalDS.Tables[2].Columns["ID"] };

                GlobalDS.Relations.Add("relation1", GlobalDS.Tables[0].Columns["id"], GlobalDS.Tables[1].Columns["pid"]);
                GlobalDS.Relations.Add("relation2", GlobalDS.Tables[1].Columns["id"], GlobalDS.Tables[2].Columns["pid"]);
                //string[] ConstGlobalListTables = new string[] { "GlobalProductionType", "GlobalProductionName", "TopoMSAEEPROMSet" };


                mylastIDEEPROMID = Convert.ToInt32(myDataIO.GetLastInsertData("TopoMSAEEPROMSet"));

                cboPNType.Items.Clear();
                for (int i = 0; i < GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                {
                    this.cboPNType.Items.Add(GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString());
                }

                if (cboPNType.Items.Count > 0)
                {
                    cboPNType.SelectedIndex = -1;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                GlobalDS.Tables.Clear();
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 初始化PN资料
        /// </summary>
        void initPN()
        {
            try
            {
                cboPN.Items.Clear();
                cboPN.Text = "";
                initFileName();
                showDGVRowsInfo("", this.dgvPage00hData, 0, 0, 0, false);
                showDGVRowsInfo("", this.dgvPage01hData, 0, 0, 0, false);
                showDGVRowsInfo("", this.dgvPage02hData, 0, 0, 0, false);
                showDGVRowsInfo("", this.dgvPage03hData, 0, 0, 0, false);
                GlobalTempPNTypeIndex = -1;
                GlobalTempPNIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 初始化FileName listbox
        /// </summary>
        void initFileName()
        {
            lstFileName.Items.Clear();
            lstFileName.Text = "";
            //strNewFileName = "";
            //listBoxFileName.Enabled = false;
            GlobalTempLstFNmIndex = -1;
        }

        /// <summary>
        /// 希望强制触发该事件时，请清空对应的全局索引变量GlobalTempPNTypeIndex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPNType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (GlobalTempPNTypeIndex == cboPNType.SelectedIndex)
                {
                    return;
                }

                SetDgvReadOnly(true);

                int tempPNTypeIndex = cboPNType.SelectedIndex;
                int tempPNIndex = cboPN.SelectedIndex;
                int tempLstFileName = lstFileName.SelectedIndex;

                initPN();
                //string[] ConstGlobalListTables = new string[] { "GlobalProductionType", "GlobalProductionName", "TopoMSAEEPROMSet" };
                string currTypeID = "-1";
                for (int i = 0; i < GlobalDS.Tables["GlobalProductionType"].Rows.Count; i++)
                {
                    if (GlobalDS.Tables["GlobalProductionType"].Rows[i]["ItemName"].ToString().ToUpper() == cboPNType.Text.ToString().ToUpper())
                    {
                        currTypeID = GlobalDS.Tables["GlobalProductionType"].Rows[i]["ID"].ToString();
                        break;
                    }
                }
                string sqlCondition = "PID=" + currTypeID;
                DataRow[] mrDRs = GlobalDS.Tables["GlobalProductionName"].Select(sqlCondition);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    this.cboPN.Items.Add(mrDRs[i]["PN"].ToString());
                }

                showcboDataInfo(cboPNType.Text);

                if (cboPNType.Text.ToString().Trim().ToUpper() == "QSFP")
                {
                    myEEPROMOperation = new QSFP();
                    myEEPROMOperation.Data0length = 128;
                    myEEPROMOperation.Data1length = 128;
                    myEEPROMOperation.Data2length = 128;
                    myEEPROMOperation.Data3length = 128;
                    myEEPROMOperation.Data0FristIndex = 128;
                    myEEPROMOperation.Data1FristIndex = 128;
                    myEEPROMOperation.Data2FristIndex = 128;
                    myEEPROMOperation.Data3FristIndex = 128;

                    myEEPROMOperation.Data0Name = "A0H_Page0";
                    myEEPROMOperation.Data1Name = "A0H_Page1";
                    myEEPROMOperation.Data2Name = "A0H_Page2";
                    myEEPROMOperation.Data3Name = "A0H_Page3";

                }
                else if (cboPNType.Text.ToString().Trim().ToUpper() == "SFP")
                {
                    myEEPROMOperation = new SFP();
                    myEEPROMOperation.Data0length = 256;
                    myEEPROMOperation.Data1length = 128;
                    myEEPROMOperation.Data2length = 128;
                    myEEPROMOperation.Data3length = 0;
                    myEEPROMOperation.Data0FristIndex = 0;
                    myEEPROMOperation.Data1FristIndex = 0;
                    myEEPROMOperation.Data2FristIndex = 128;
                    myEEPROMOperation.Data3FristIndex = 128;

                    myEEPROMOperation.Data0Name = "A0H";
                    myEEPROMOperation.Data1Name = "A2H_LowMemory";
                    myEEPROMOperation.Data2Name = "A2H_Page0";
                    myEEPROMOperation.Data3Name = "N/A";
                }
                else if (cboPNType.Text.ToString().Trim().ToUpper() == "XFP")
                {
                    myEEPROMOperation = new XFP();
                    myEEPROMOperation.Data0length = 128;
                    myEEPROMOperation.Data1length = 128;
                    myEEPROMOperation.Data2length = 128;
                    myEEPROMOperation.Data3length = 0;
                    myEEPROMOperation.Data0FristIndex = 128;
                    myEEPROMOperation.Data1FristIndex = 0;
                    myEEPROMOperation.Data2FristIndex = 128;
                    myEEPROMOperation.Data3FristIndex = 128;

                    myEEPROMOperation.Data0Name = "A0H_Page1";
                    myEEPROMOperation.Data1Name = "A0H_LowMemory";
                    myEEPROMOperation.Data2Name = "A0H_Page2";
                    myEEPROMOperation.Data3Name = "N/A";
                }
                else if (cboPNType.Text.ToString().Trim().ToUpper() == "CFP")
                {
                    myEEPROMOperation = new CFP();
                    myEEPROMOperation.Data0length = 0;
                    myEEPROMOperation.Data1length = 0;
                    myEEPROMOperation.Data2length = 0;
                    myEEPROMOperation.Data3length = 0;
                    myEEPROMOperation.Data0FristIndex = 0;
                    myEEPROMOperation.Data1FristIndex = 0;
                    myEEPROMOperation.Data2FristIndex = 0;
                    myEEPROMOperation.Data3FristIndex = 0;
                    myEEPROMOperation.Data0Name = "N/A";
                    myEEPROMOperation.Data1Name = "N/A";
                    myEEPROMOperation.Data2Name = "N/A";
                    myEEPROMOperation.Data3Name = "N/A";
                }

                Data0 = new byte[myEEPROMOperation.Data0length];
                Data1 = new byte[myEEPROMOperation.Data1length];
                Data2 = new byte[myEEPROMOperation.Data2length];
                Data3 = new byte[myEEPROMOperation.Data3length];

                tabPage1.Text = myEEPROMOperation.Data0Name;
                tabPage2.Text = myEEPROMOperation.Data1Name;
                tabPage3.Text = myEEPROMOperation.Data2Name;
                tabPage4.Text = myEEPROMOperation.Data3Name;

                myLastPageNo = -1;
               
                if (cboPNType.SelectedIndex >= 0)
                {
                    cboPN.Enabled = true;
                    cboPN.SelectedIndex = -1;
                    tabEEPROMInfo.Enabled = false;

                    if (cboPN.Items.Count > 0 && cboPN.SelectedIndex == -1)
                    {
                        sslRunMsg.Text = "Pls choose PN!!! Time: " + DateTime.Now.ToString();
                        this.sslRunMsg.BackColor = Color.Yellow;
                    }
                }
                else
                {
                    cboPN.Enabled = false;
                    lstFileName.Enabled = false;
                    tabEEPROMInfo.Enabled = false;
                }

                if (cboPN.Items.Count == 0)
                {
                    sslRunMsg.Text = "There is no any PN under the current Type! Time: " + DateTime.Now.ToString();
                    this.sslRunMsg.BackColor = Color.Yellow;
                }

                if (lstFileName.SelectedIndex == -1)
                {
                    txtAllDescription.Text = "";
                    tabEEPROMInfo.Enabled = false;
                }

                setTspState(false);

                GlobalTempPNTypeIndex = cboPNType.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                GlobalDS.Tables.Clear();
            }
        }

        /// <summary>
        /// 执行该方法后，系统会从GlobalDS本地缓存中重新下载当前选中项的EEPROM资料
        /// 希望强制触发该事件时，请清空对应的全局索引变量GlobalLstFileNameIndex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstFileName.SelectedIndex != -1)
                {
                    if (lstFileName.SelectedIndex == GlobalTempLstFNmIndex)
                    {
                        return;
                    }

                    if (lstFileName.Text.Length > 0)
                    {
                        DataRow[] dr = GlobalDS.Tables["TopoMSAEEPROMSet"].
                            Select("PID=" + EEPROMTabPID + " and ItemName ='" + lstFileName.Text + "'");

                        if (dr.Length == 1)
                        {
                            SetDgvReadOnly(true);
                            if (this.tabPage1.Text.ToUpper() != "N/A")
                            {
                                showDGVRowsInfo(myEEPROMOperation.Data0Name, this.dgvPage00hData, myEEPROMOperation.Data0length, 0, myEEPROMOperation.Data0FristIndex, isCurrItemNameExisted("data0"));
                            }
                            if (this.tabPage2.Text.ToUpper() != "N/A")
                            {
                                showDGVRowsInfo(myEEPROMOperation.Data1Name, this.dgvPage01hData, myEEPROMOperation.Data1length, 0, myEEPROMOperation.Data1FristIndex, isCurrItemNameExisted("data1"));
                            }
                            if (this.tabPage3.Text.ToUpper() != "N/A")
                            {
                                showDGVRowsInfo(myEEPROMOperation.Data2Name, this.dgvPage02hData, myEEPROMOperation.Data2length, 0, myEEPROMOperation.Data2FristIndex, isCurrItemNameExisted("data2"));
                            }
                            if (this.tabPage4.Text.ToUpper() != "N/A")
                            {
                                showDGVRowsInfo(myEEPROMOperation.Data3Name, this.dgvPage03hData, myEEPROMOperation.Data3length, 0, myEEPROMOperation.Data3FristIndex, isCurrItemNameExisted("data3"));
                            }
                        }
                    }
                    else
                    {
                        //lstFileName.Text = strNewFileName;
                    }

                    // 设置工具栏按钮Enable状态
                    if (blnAddNew)
                    {
                        setTspState(true);
                        tsbtnEdit.Enabled = false;
                        SetDgvReadOnly(false);
                        if (this.tabPage1.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data0Name, this.dgvPage00hData, myEEPROMOperation.Data0length, 0, myEEPROMOperation.Data0FristIndex, isCurrItemNameExisted("data0"));
                        }
                        if (this.tabPage2.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data1Name, this.dgvPage01hData, myEEPROMOperation.Data1length, 0, myEEPROMOperation.Data1FristIndex, isCurrItemNameExisted("data1"));
                        }
                        if (this.tabPage3.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data2Name, this.dgvPage02hData, myEEPROMOperation.Data2length, 0, myEEPROMOperation.Data2FristIndex, isCurrItemNameExisted("data2"));
                        }
                        if (this.tabPage4.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data3Name, this.dgvPage03hData, myEEPROMOperation.Data3length, 0, myEEPROMOperation.Data3FristIndex, isCurrItemNameExisted("data3"));
                        }
                    }
                    else if (isCopy)
                    {
                        setTspState(true);
                        tsbtnEdit.Enabled = false;
                        SetDgvReadOnly(false);
                        if (this.tabPage1.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data0Name, this.dgvPage00hData, myEEPROMOperation.Data0length, 0, myEEPROMOperation.Data0FristIndex, isCurrItemNameExisted("data0", isCopy));
                        }
                        if (this.tabPage2.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data1Name, this.dgvPage01hData, myEEPROMOperation.Data1length, 0, myEEPROMOperation.Data1FristIndex, isCurrItemNameExisted("data1", isCopy));
                        }
                        if (this.tabPage3.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data2Name, this.dgvPage02hData, myEEPROMOperation.Data2length, 0, myEEPROMOperation.Data2FristIndex, isCurrItemNameExisted("data2", isCopy));
                        }
                        if (this.tabPage4.Text.ToUpper() != "N/A")
                        {
                            showDGVRowsInfo(myEEPROMOperation.Data3Name, this.dgvPage03hData, myEEPROMOperation.Data3length, 0, myEEPROMOperation.Data3FristIndex, isCurrItemNameExisted("data3", isCopy));
                        }
                    }
                    else
                    {
                        setTspState(false);
                    }

                    tabEEPROMInfo.Enabled = true;
                    tabEEPROMInfo.SelectedIndex = -1; // 为了触发tabEEPROMInfo_SelectedIndexChanged事件
                    tabEEPROMInfo.SelectedIndex = 0;

                    sslRunMsg.Text = "Getting data successful!!! Time: " + DateTime.Now.ToString();
                    this.sslRunMsg.BackColor = Color.Yellow;

                    GlobalTempLstFNmIndex = lstFileName.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        int calcCheckSum(byte[] objectData, int startAddr, int endAddr)
        {
            int cs = 0;

            for (int i = startAddr; i <= endAddr; i++)
            {
                cs += objectData[i];
            }

            return (cs & 0xff);
        }

        /// <summary>
        /// 刷新当前时间...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 新增EEPROM项目资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnNew_Click(object sender, EventArgs e)
        {
            int count = lstFileName.Items.Count;
            string[] fileName = new string[count];

            for (int i = 0; i < count; i++)
            {
                fileName[i] = lstFileName.Items[i].ToString().ToUpper().Trim();
            }

            NewFileName myname = new NewFileName(fileName);
            myname.ShowDialog();

            if (myname.isNewFileNameOK)
            {
                strNewFileName = myname.name;
                runMsgState((byte)MsgState.AddNew);
            }
        }

        /// <summary>
        /// 删除EEPROM项目资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int tempPNTypeIndex = cboPNType.SelectedIndex;
                int tempPNIndex = cboPN.SelectedIndex;
                int templstFileName = lstFileName.SelectedIndex;

                string myDeleteItemName = this.lstFileName.SelectedItem.ToString(); // 获取当前选中项的FileName
                int deleteItemCount = currPrmtrCountExisted(GlobalDS.Tables["TopoMSAEEPROMSet"], "ItemName='" + myDeleteItemName + "' AND PID=" + EEPROMTabPID);

                if ((blnAddNew == true) && (deleteItemCount == 0))
                {
                    // 新增状态下（即资料尚未保存）的资料删除

                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("The current data has not been saved！\nConfirm delete unsaved data -->" + myDeleteItemName + "\n \n Choose 'Y' to continue?",
                        "Attention",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        if (lstFileName.Items.Contains(myDeleteItemName))
                        {
                            lstFileName.Items.Remove(myDeleteItemName);
                            lstFileName.Text = "";
                        }
                        else
                        {
                            lstFileName.Text = "";
                        }
                        blnAddNew = false;
                        this.sslRunMsg.Text = "";
                        MessageBox.Show("EEPROM FileName: " + myDeleteItemName + " has been removed!");
                        ResfeshList(GlobalDS.Tables["TopoMSAEEPROMSet"], "PID=" + EEPROMTabPID, this.lstFileName, "ItemName");
                        runMsgState((byte)MsgState.Delete);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    // 非新增状态下资料删除
                    string currID = getDTColumnInfo(GlobalDS.Tables["TopoMSAEEPROMSet"], "ID", "ItemName='" + myDeleteItemName + "' AND PID=" + EEPROMTabPID);

                    DialogResult drst = new DialogResult();
                    drst = (MessageBox.Show("Confirm delete data -->" + myDeleteItemName + "\n \n Choose 'Y' to continue?",
                        "Attention",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2));

                    if (drst == DialogResult.Yes)
                    {
                        bool result = DeleteItemForDT(GlobalDS.Tables["TopoMSAEEPROMSet"], "ID=" + currID + "and ItemName='" + myDeleteItemName + "'");
                        if (result)
                        {
                            MessageBox.Show("EEPROM FileName: " + myDeleteItemName + " has been removed!");
                            ResfeshList(GlobalDS.Tables["TopoMSAEEPROMSet"], "PID=" + EEPROMTabPID, this.lstFileName, "ItemName");
                            runMsgState((byte)MsgState.Delete);
                        }
                        else
                        {
                            MessageBox.Show("EEPROM FileName: " + myDeleteItemName + "fail to remove!");
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                // 必须重置
                GlobalTempPNTypeIndex = -1;
                GlobalTempPNIndex = -1;
                GlobalTempLstFNmIndex = -1;

                this.cboPNType.SelectedIndex = -1;
                this.cboPNType.SelectedIndex = tempPNTypeIndex;
                this.cboPN.SelectedIndex = -1;
                this.cboPN.SelectedIndex = tempPNIndex;
                lstFileName.SelectedIndex = -1;
                showDGVRowsInfo(myEEPROMOperation.Data0Name, this.dgvPage00hData, 0, 0, 0, false);
                showDGVRowsInfo(myEEPROMOperation.Data1Name, this.dgvPage01hData, 0, 0, 0, false);
                showDGVRowsInfo(myEEPROMOperation.Data2Name, this.dgvPage02hData, 0, 0, 0, false);
                showDGVRowsInfo(myEEPROMOperation.Data3Name, this.dgvPage03hData, 0, 0, 0, false);

                tabEEPROMInfo.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// 刷新当前现有的ComboBox资料
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filterString"></param>
        /// <param name="lbo"></param>
        /// <param name="showColumnName"></param>
        void ResfeshList(DataTable dt, string filterString, ListBox lbo, string showColumnName)
        {
            try
            {
                lbo.Items.Clear();
                lbo.Text = "";

                DataRow[] mrDRs = dt.Select(filterString);
                for (int i = 0; i < mrDRs.Length; i++)
                {
                    lbo.Items.Add(mrDRs[i][showColumnName].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 删除指定条件的表资料
        /// </summary>
        /// <param name="mydt">表名</param>
        /// <param name="delCondition">条件</param>
        /// <returns></returns>
        bool DeleteItemForDT(DataTable mydt, string delCondition)
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
        /// <summary>
        /// 获取表的某项符合条件资料[唯一]
        /// </summary>
        /// <param name="dt">表名</param>
        /// <param name="CloumnName">CloumnName</param>
        /// <param name="filterString">条件</param>
        /// <returns></returns>
        string getDTColumnInfo(DataTable dt, string CloumnName, string filterString)
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
        /// <summary>
        /// 当前表中已有符合条件的资料数目
        /// </summary>
        /// <param name="mydt">表</param>
        /// <param name="FullfilterString">条件</param>
        /// <returns></returns>
        int currPrmtrCountExisted(DataTable mydt, string FullfilterString)
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

        /// <summary>
        /// 更新datatable行的资料
        /// </summary>
        /// <param name="mydt">datatable</param>
        /// <param name="columnName">界面上的Page</param>
        /// <returns></returns>
        bool EditEEPROMdata(DataTable mydt, string columnName)
        {

            bool result = false;
            try
            {
                string myData = "";

                for (int i = 0; i < this.dgvPage00hData.Rows.Count; i++)
                {
                    myData += dgvPage00hData.Rows[i].Cells["Data"].Value.ToString().Trim();
                }

                string filterString = this.lstFileName.Text;
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' AND PID=" + EEPROMTabPID);
                if (myROWS.Length == 1)
                {
                    //if (this.blnAddNew)
                    //{
                    //    MessageBox.Show("新增资料有误!请确认!!! 已有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' AND PID=" + EEPROMTabPID ));
                    //    return result;
                    //}
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.EEPROMTabPID;
                    myROWS[0]["ItemName"] = this.lstFileName.Text;
                    myROWS[0][columnName] = myData;
                    myROWS[0].EndEdit();
                    result = true;
                }
                else if (this.blnAddNew)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = mylastIDEEPROMID + 1;
                    myNewRow["PID"] = this.EEPROMTabPID;
                    myNewRow["ItemType"] = this.cboPNType.Text;
                    myNewRow["ItemName"] = this.lstFileName.Text;
                    myNewRow[columnName] = myData;
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    mylastIDEEPROMID++;
                    blnAddNew = false;
                    result = true;
                }
                else
                {
                    MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' AND PID=" + EEPROMTabPID));
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        /// <summary>
        /// 更新datatable行的资料(更新四个Page)
        /// </summary>
        /// <param name="mydt"></param>
        /// <returns></returns>
        bool EditEEPROMdata(DataTable mydt)
        {
            bool result = false;
            try
            {
                string myData0 = "", myCRCData0 = "0";
                string myData1 = "", myCRCData1 = "0";
                string myData2 = "", myCRCData2 = "0";
                string myData3 = "", myCRCData3 = "0";

                for (int i = 0; i < this.dgvPage00hData.Rows.Count; i++)
                {
                    myData0 += dgvPage00hData.Rows[i].Cells["Data"].Value.ToString().Trim();
                    Data0[i] = Convert.ToByte("0x" + dgvPage00hData.Rows[i].Cells["Data"].Value, 16);
                }

                for (int i = 0; i < this.dgvPage01hData.Rows.Count; i++)
                {
                    myData1 += dgvPage01hData.Rows[i].Cells["Data"].Value.ToString().Trim();
                    Data1[i] = Convert.ToByte("0x" + dgvPage01hData.Rows[i].Cells["Data"].Value, 16);
                }

                for (int i = 0; i < this.dgvPage02hData.Rows.Count; i++)
                {
                    myData2 += dgvPage02hData.Rows[i].Cells["Data"].Value.ToString().Trim();
                    Data2[i] = Convert.ToByte("0x" + dgvPage02hData.Rows[i].Cells["Data"].Value, 16);
                }

                for (int i = 0; i < this.dgvPage03hData.Rows.Count; i++)
                {
                    myData3 += dgvPage03hData.Rows[i].Cells["Data"].Value.ToString().Trim();
                    Data3[i] = Convert.ToByte("0x" + dgvPage03hData.Rows[i].Cells["Data"].Value, 16);
                }

                myCRCData0 = Convert.ToString(Crc8(Data0));
                myCRCData1 = Convert.ToString(Crc8(Data1));
                myCRCData2 = Convert.ToString(Crc8(Data2));
                myCRCData3 = Convert.ToString(Crc8(Data3));


                if (myEEPROMOperation.Data0Name.ToUpper() == "N/A".ToUpper())
                {
                    myData0 = "";
                    myCRCData0 = "0";
                }
                if (myEEPROMOperation.Data1Name.ToUpper() == "N/A".ToUpper())
                {
                    myData1 = "";
                    myCRCData1 = "0";
                }
                if (myEEPROMOperation.Data2Name.ToUpper() == "N/A".ToUpper())
                {
                    myData2 = "";
                    myCRCData2 = "0";
                }
                if (myEEPROMOperation.Data3Name.ToUpper() == "N/A".ToUpper())
                {
                    myData3 = "";
                    myCRCData3 = "0";
                }

                string filterString = this.lstFileName.Text;
                DataRow[] myROWS = mydt.Select("ItemName='" + filterString + "' AND PID=" + EEPROMTabPID);
                if (myROWS.Length == 1)
                {
                    myROWS[0].BeginEdit();
                    myROWS[0]["PID"] = this.EEPROMTabPID;
                    myROWS[0]["ItemName"] = this.lstFileName.Text;
                    myROWS[0]["DATA0"] = myData0;
                    myROWS[0]["DATA1"] = myData1;
                    myROWS[0]["DATA2"] = myData2;
                    myROWS[0]["DATA3"] = myData3;

                    myROWS[0]["CRCDATA0"] = myCRCData0;
                    myROWS[0]["CRCDATA1"] = myCRCData1;
                    myROWS[0]["CRCDATA2"] = myCRCData2;
                    myROWS[0]["CRCDATA3"] = myCRCData3;
                    myROWS[0].EndEdit();
                    result = true;

                    this.txtAllDescription.Text = "CRC0=" + myCRCData0 + ";CRC1=" + myCRCData1 + ";CRC2=" + myCRCData2 + ";CRC3=" + myCRCData3;

                    //MessageBox.Show("CRC0=" + myCRCData0 + ";CRC1=" + myCRCData1 + ";CRC2=" + myCRCData2 + ";CRC3=" + myCRCData3);
                }
                else if (this.blnAddNew)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = mylastIDEEPROMID + 1;
                    myNewRow["PID"] = this.EEPROMTabPID;
                    myNewRow["ItemType"] = this.cboPNType.Text;
                    myNewRow["ItemName"] = this.lstFileName.Text;
                    myNewRow["DATA0"] = myData0;
                    myNewRow["DATA1"] = myData1;
                    myNewRow["DATA2"] = myData2;
                    myNewRow["DATA3"] = myData3;

                    myNewRow["CRCDATA0"] = myCRCData0;
                    myNewRow["CRCDATA1"] = myCRCData1;
                    myNewRow["CRCDATA2"] = myCRCData2;
                    myNewRow["CRCDATA3"] = myCRCData3;
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    mylastIDEEPROMID++;
                    blnAddNew = false;
                    result = true;
                    this.txtAllDescription.Text = "CRC0=" + myCRCData0 + ";CRC1=" + myCRCData1 + ";CRC2=" + myCRCData2 + ";CRC3=" + myCRCData3;

                    //MessageBox.Show("CRC0=" + myCRCData0 + ";CRC1=" + myCRCData1 + ";CRC2=" + myCRCData2 + ";CRC3=" + myCRCData3);
                }
                else if (this.isCopy)
                {
                    DataRow myNewRow = mydt.NewRow();
                    myNewRow.BeginEdit();
                    myNewRow["ID"] = mylastIDEEPROMID + 1;
                    myNewRow["PID"] = this.EEPROMTabPID;
                    myNewRow["ItemType"] = this.cboPNType.Text;
                    myNewRow["ItemName"] = this.lstFileName.Text;
                    myNewRow["DATA0"] = myData0;
                    myNewRow["DATA1"] = myData1;
                    myNewRow["DATA2"] = myData2;
                    myNewRow["DATA3"] = myData3;

                    myNewRow["CRCDATA0"] = myCRCData0;
                    myNewRow["CRCDATA1"] = myCRCData1;
                    myNewRow["CRCDATA2"] = myCRCData2;
                    myNewRow["CRCDATA3"] = myCRCData3;
                    mydt.Rows.Add(myNewRow);

                    myNewRow.EndEdit();
                    mylastIDEEPROMID++;
                    isCopy = false;
                    result = true;
                    this.txtAllDescription.Text = "CRC0=" + myCRCData0 + ";CRC1=" + myCRCData1 + ";CRC2=" + myCRCData2 + ";CRC3=" + myCRCData3;
                }
                else
                {
                    MessageBox.Show("资料有误!请确认!!! 共有" + myROWS.Length + ("条记录; \n 条件--> ItemName='" + filterString + "' AND PID=" + EEPROMTabPID));
                }


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        void showcboDataInfo(string itemType)
        {
            try
            {
                //tabEEPROMInfo.Items.Clear();
                //tabEEPROMInfo.Text = "";
                //cboDataPage.Items.Add("DATA0");
                //cboDataPage.Items.Add("DATA1");
                //cboDataPage.Items.Add("DATA2");
                //cboDataPage.Items.Add("DATA3");
                //cboWritePage.Items.Clear();
                //cboWritePage.Text = "";
                //cboWritePage.Items.Add("DATA0");
                //cboWritePage.Items.Add("DATA1");
                //cboWritePage.Items.Add("DATA2");
                //cboWritePage.Items.Add("DATA3");

                #region 显示资料&长度确认 (未启用)
                //switch (itemType.ToUpper())
                //{
                //    //QSFP:DATA0->A0H_Page0,DATA1->A0H_Page3
                //    case "QSFP":

                //        showFristRowID = 128;
                //        showRowsCount = 256 - showFristRowID;

                //        if (cboDataPage.Text.Trim() == "DATA0")
                //        {
                //            this.lblPageNo.Text = "选择:A0H_Page0";
                //            myLastPageNo = "DATA0";
                //        }
                //        else if (cboDataPage.Text.Trim().ToUpper() == "DATA1")
                //        {
                //            this.lblPageNo.Text = "选择:A0H_Page3";
                //            myLastPageNo = "DATA1";
                //        }
                //        break;

                //    case "SFP":
                //        //SFP:DATA0->A0H,DATA1->A2H_LowMemory,DATA2->A2H_Page0
                //        if (cboDataPage.Text.Trim() == "DATA0")
                //        {
                //            this.lblPageNo.Text = "选择:A0H";
                //            showFristRowID = 0;
                //            showRowsCount = 256 - showFristRowID;
                //            myLastPageNo = "DATA0";
                //        }
                //        else if (cboDataPage.Text.Trim().ToUpper() == "DATA1")
                //        {
                //            this.lblPageNo.Text = "选择:A2H_LowMemory";
                //            showFristRowID = 0;
                //            showRowsCount = 128 - showFristRowID;
                //            myLastPageNo = "DATA1";
                //        }
                //        else if (cboDataPage.Text.Trim().ToUpper() == "DATA2")
                //        {
                //            this.lblPageNo.Text = "选择:A2H_Page0";
                //            showFristRowID = 128;
                //            showRowsCount = 256 - showFristRowID;
                //            myLastPageNo = "DATA2";
                //        }
                //        break;

                //    case "XFP":
                //        //SFP:DATA0->A0H_LowMemory,DATA1->A0H_Page0,DATA2->A0H_Page1,DATA3->A0H_Page2

                //            showFristRowID = 128;
                //            showRowsCount = 256 - showFristRowID;
                //        if (cboDataPage.Text.Trim() == "DATA0")
                //        {
                //            this.lblPageNo.Text = "选择:A0H_LowMemory";
                //            myLastPageNo = "DATA0";
                //        }
                //        else if (cboDataPage.Text.Trim().ToUpper() == "DATA1")
                //        {
                //            this.lblPageNo.Text = "选择:A0H_Page1";
                //            myLastPageNo = "DATA1";
                //        }
                //        else if (cboDataPage.Text.Trim().ToUpper() == "DATA2")
                //        {
                //            this.lblPageNo.Text = "选择:A0H_Page2";
                //            myLastPageNo = "DATA2";
                //        }

                //        break;

                //    case "CFP":

                //        if (cboDataPage.Text.Trim() == "DATA0")
                //        {
                //            this.lblPageNo.Text = "选择:";
                //            myLastPageNo = "DATA0";
                //        }
                //        else if (cboDataPage.Text.Trim().ToUpper() == "DATA1")
                //        {
                //            this.lblPageNo.Text = "选择:";
                //            myLastPageNo = "DATA1";
                //        }
                //        else
                //        {
                //            MessageBox.Show("暂时无需执行此页面资料维护!");
                //            cboDataPage.SelectedIndex = -1;
                //            cboDataPage.SelectedItem = myLastPageNo;
                //            return;
                //        }
                //        break;
                //    default:
                //        MessageBox.Show("当前输入的机种类型可能有问题-->" + this.cboPNType.Text.Trim().ToUpper());
                //        myLastPageNo = "";
                //        return;
                //}
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 设置需要显示DGV信息
        /// </summary>
        /// <param name="rowsCount">行数</param>
        /// <param name="fristRowID">第一行RowID</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="isExistedDT">是否有发现表中有该项资料</param>
        void showDGVRowsInfo(string pageName, DataGridView dgv, int rowsCount, int fristRowID, int startAddress, bool isExistedDT)
        {
            try
            {
                dgv.Rows.Clear();
                if (!tabEEPROMInfo.Enabled)
                {
                    tabEEPROMInfo.Enabled = true;
                }
                if (!dgv.Enabled)
                {
                    dgv.Enabled = true;
                }

                if (rowsCount > 0)
                {
                    dgv.RowCount = rowsCount;
                    for (int i = 0; i < rowsCount; i++)
                    {
                        dgv.Rows[i].Cells["AddrDec"].Value = startAddress + i;
                        string HexString = (startAddress + i).ToString("x").ToUpper();
                        if (HexString.Length < 2)
                        {
                            HexString = "0" + HexString + "h";
                        }
                        else
                        {
                            HexString = HexString + "h";
                        }
                        dgv.Rows[i].Cells["AddrHex"].Value = HexString;

                        if (isCopy)
                        {
                            dgv.Rows[i].Cells["Data"].Value = copyDataInfo[i];
                            dgv.Columns["Data"].DefaultCellStyle.BackColor = Color.GreenYellow;
                        }
                        else if (isExistedDT)
                        {
                            dgv.Rows[i].Cells["Data"].Value = dataInfo[0, i];
                            dgv.Columns["Data"].DefaultCellStyle.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            dgv.Rows[i].Cells["Data"].Value = "FF";
                            dgv.Columns["Data"].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        dgv.Columns["Data"].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    if (rowsCount > 0)
                    {
                        dgv.FirstDisplayedScrollingRowIndex = fristRowID;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void setDGVInfo(DataGridView dgv, int startAddress, int rowCount, bool isFormLoad)
        {
            //int rowCount = 16 * 2;
            dgv.Rows.Clear();
            int columnCount = 8;
            if (isFormLoad)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    dgv.Columns.Add(i.ToString(), i.ToString("X").ToUpper() + @"/" + ((int)(i + columnCount)).ToString("X").ToUpper());
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgv.Columns[i].ReadOnly = true;
                }
            }
            if (rowCount > 0)
            {
                dgv.Rows.Add(rowCount);
                dgv.RowHeadersWidth = 48;

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        string myValue = (i * columnCount + j).ToString("X").ToUpper();
                        if (myValue.Length < 2)
                        {
                            myValue = "0" + myValue;
                        }
                        dgv.Rows[i].Cells[j].Value = myValue;
                        dgv.Columns[j].Width = 32;
                    }
                    if (i % 2 == 0)
                    {
                        dgv.Rows[i].HeaderCell.Value = ((int)(i / 2)).ToString("X").ToUpper() + "0";  //(i + startAddress)
                    }
                    else
                    {
                        dgv.Rows[i].HeaderCell.Value = ((int)(i / 2)).ToString("X").ToUpper() + "8";  //(i + startAddress)
                    }

                }
            }
            //if (rowCount <= 128)
            //{
            //    resizeDGV(dgv);
            //}
        }

        void initDGVInfo(DataGridView dgv)
        {
            try
            {
                dgv.DataSource = null;        //141008_0

                dgv.Columns.Clear();
                dgv.Rows.Clear();

                dgv.Columns.Add("AddrDec", "Addr(Dec)");
                dgv.Columns.Add("AddrHex", "Addr(Hex)");
                dgv.Columns.Add("Data", "Data(Hex)");
                dgv.Columns.Add("Item", "Item");
                dgv.Columns.Add("ItemDescription", "ItemDescription");
                dgv.Rows.Clear();
                int myWidth = 0;
                dgv.Columns[0].Width = 100;
                myWidth += dgv.Columns[0].Width;
                dgv.Columns[1].Width = 100;
                myWidth += dgv.Columns[1].Width;
                dgv.Columns[2].Width = 100;
                myWidth += dgv.Columns[2].Width;
                dgv.Columns[3].Width = 150;
                myWidth += dgv.Columns[3].Width;
                dgv.Columns[4].Width = dgv.Width - myWidth;
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                resizeDGV(dgv);

                dgv.Columns["AddrDec"].ReadOnly = true;
                dgv.Columns["AddrHex"].ReadOnly = true;
                dgv.Columns["Data"].ReadOnly = true;
                dgv.Columns["Item"].ReadOnly = true;
                dgv.Columns["ItemDescription"].ReadOnly = true;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (dgv.Columns[i].ReadOnly == true)
                    {
                        dgv.Columns[i].DefaultCellStyle.BackColor = Color.Silver;
                        dgv.Columns[i].DefaultCellStyle.ForeColor = Color.DarkBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void LoadTabPage()
        {
            initDGVInfo(this.dgvPage00hData);
            initDGVInfo(this.dgvPage01hData);
            initDGVInfo(this.dgvPage02hData);
            initDGVInfo(this.dgvPage03hData);
        }

        /// <summary>
        /// 重新设置dgv显示大小
        /// </summary>
        /// <param name="dgv">dgv</param>
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
            //for (int i = 0; i < dgv.Columns.Count; i++)
            //{
            //    if (mySize < dgv.Width)
            //    {
            //        if (dgv.Columns[i].Visible)
            //        {
            //            dgv.Columns[i].Width += (dgv.Width - mySize) / j;
            //        }
            //    }
            //}
            dgv.Columns[dgv.Columns.Count - 1].Width += (dgv.Width - mySize);
        }

        enum MsgState
        {
            SaveOK = 0,
            Edit = 1,
            AddNew = 2,
            Delete = 3,
            Clear = 4
        }

        void runMsgState(byte state)
        {
            try
            {
                //this.dgvPageData.Enabled = true;
                //this.tsmNew.Enabled = true;
                //this.tsmDelete.Enabled = false;
                //this.tsmSave.Enabled = false;

                tabEEPROMInfo.Enabled = false;
                //strNewFileName.Enabled = false;
                //strNewFileName.BackColor = Color.White;
                tabEEPROMInfo.BackColor = Color.White;

                //listBoxFileName.Enabled = true;
                //cboPNType.Enabled = true;
                //cboPN.Enabled = true;

                if (state == (byte)MsgState.AddNew)
                {
                    blnAddNew = true;
                    this.sslRunMsg.BackColor = Color.Yellow;
                    //this.dgvPage00hData.Columns["Data"].DefaultCellStyle.BackColor = Color.Yellow;

                    if (lstFileName.Items.Contains(strNewFileName) == false)
                    {
                        lstFileName.Items.Add(strNewFileName);
                    }

                    // 新增状态下，需要禁用
                    cboPNType.Enabled = false;
                    cboPN.Enabled = false;
                    lstFileName.Enabled = false;

                    // 新增状态，设置工具栏按钮状态
                    setTspState(true);

                    // 新增状态，需要重置或清除
                    int tempIndex = lstFileName.Items.IndexOf(strNewFileName);
                    lstFileName.SelectedIndex = tempIndex; // 新增状态下，listBoxFileName默认选项为当前新增的选项

                    // lstFileName选定当前新增项后，立即对strNewFileName执行重置命令
                    strNewFileName = "";
                    this.sslRunMsg.Text = "Pls input new EEPROM Information data!";
                }
                else if (state == (byte)MsgState.Edit)
                {
                    tabEEPROMInfo.Enabled = true;
                    this.sslRunMsg.Text = "目前为编辑参数状态,请在确认资料信息!";
                    this.sslRunMsg.BackColor = Color.GreenYellow;
                    this.dgvPage00hData.Columns["Data"].DefaultCellStyle.BackColor = Color.GreenYellow;
                    this.dgvPage01hData.Columns["Data"].DefaultCellStyle.BackColor = Color.GreenYellow;
                    this.dgvPage02hData.Columns["Data"].DefaultCellStyle.BackColor = Color.GreenYellow;
                    this.dgvPage03hData.Columns["Data"].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (state == (byte)MsgState.SaveOK)
                {
                    this.sslRunMsg.Text = "EEPROM File Name " + this.lstFileName.Text + " has been maintained!";
                    this.sslRunMsg.BackColor = Color.YellowGreen;
                    this.dgvPage00hData.Columns["Data"].DefaultCellStyle.BackColor = Color.YellowGreen;
                    this.dgvPage01hData.Columns["Data"].DefaultCellStyle.BackColor = Color.YellowGreen;
                    this.dgvPage02hData.Columns["Data"].DefaultCellStyle.BackColor = Color.YellowGreen;
                    this.dgvPage03hData.Columns["Data"].DefaultCellStyle.BackColor = Color.YellowGreen;

                    // SaveOK后，设置工具栏按钮状态
                    setTspState(false);

                    this.cboPNType.Enabled = true;
                    this.cboPN.Enabled = true;
                    this.lstFileName.Enabled = true;
                    tabEEPROMInfo.Enabled = true;
                }
                else if (state == (byte)MsgState.Delete)
                {
                    this.sslRunMsg.Text = "Data information has been deleted! " + this.lstFileName.Text;
                    this.sslRunMsg.BackColor = Color.Pink;

                    // 删除成功，需要禁用
                    tabEEPROMInfo.Enabled = false;

                    // 删除成功，需要启用
                    cboPNType.Enabled = true;
                    cboPN.Enabled = true;
                    lstFileName.Enabled = true;

                    // 删除成功，工具栏按钮设置
                    setTspState(false);

                    // 删除成功，需要重置或清除
                    SetDgvReadOnly(true);

                    txtAllDescription.Text = "";
                    strNewFileName = "";

                    if (blnAddNew)
                    {
                        blnAddNew = false;
                    }
                }
                else if (state == (byte)MsgState.Clear)
                {
                    //this.sslRunMsg.Text = "" ;

                    this.sslRunMsg.BackColor = Color.White;
                    this.dgvPage00hData.Columns["Data"].DefaultCellStyle.BackColor = Color.White;
                    this.dgvPage01hData.Columns["Data"].DefaultCellStyle.BackColor = Color.White;
                    this.dgvPage02hData.Columns["Data"].DefaultCellStyle.BackColor = Color.White;
                    this.dgvPage03hData.Columns["Data"].DefaultCellStyle.BackColor = Color.White;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 设置工具栏按钮Enable、Visible状态
        /// </summary>
        /// <param name="isAddNew">是否为新增状态</param>
        void setTspState(bool isAddNew)
        {
            if (isAddNew)
            {
                // 新增状态下，需要禁用
                tsbtnNew.Enabled = false;
                tsbtnEdit.Enabled = false;
                tsbtnDelete.Enabled = false;
                tsbtnRefresh.Enabled = false;
                tsbtnUpdate.Enabled = false;

                // 新增状态，需要启用
                tsbtnSave.Visible = true;
                tsbtnSave.Enabled = true;
                tsbtnCancel.Visible = true;
                tsbtnCancel.Enabled = true;
            }
            else
            {
                if (lstFileName.SelectedIndex >= 0)
                {
                    tsbtnNew.Enabled = true;
                    tsbtnDelete.Enabled = true;
                    tsbtnRefresh.Enabled = true;
                    tsbtnUpdate.Enabled = true;

                    if (isReadOnly)
                    {
                        // Read Only Model
                        tsbtnSave.Visible = false;
                        tsbtnSave.Enabled = false;
                        tsbtnCancel.Enabled = false;
                        tsbtnCancel.Visible = false;
                        tsbtnEdit.Enabled = true;
                    }
                    else
                    {
                        // Edit Modle
                        tsbtnSave.Visible = true;
                        tsbtnSave.Enabled = true;
                        tsbtnCancel.Enabled = true;
                        tsbtnCancel.Visible = true;
                        tsbtnEdit.Enabled = false;
                    }
                }
                else
                {
                    tsbtnSave.Enabled = false;
                    tsbtnSave.Visible = false;
                    tsbtnCancel.Visible = false;
                    tsbtnCancel.Enabled = false;
                    tsbtnDelete.Enabled = false;
                    tsbtnRefresh.Enabled = true;
                    tsbtnUpdate.Enabled = true;
                    tsbtnEdit.Enabled = false;

                    if (cboPN.SelectedIndex >= 0)
                    {
                        tsbtnNew.Enabled = true;
                    }
                    else
                    {
                        tsbtnNew.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 希望强制触发该事件时，请清空对应的全局索引变量GlobalPNIndex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int tempPNTypeIndex = cboPNType.SelectedIndex;
                int tempPNIndex = cboPN.SelectedIndex;
                int tempLstFileName = lstFileName.SelectedIndex;

                if (cboPN.SelectedIndex >= 0)
                {
                    if (cboPN.SelectedIndex == GlobalTempPNIndex)
                    {
                        return;
                    }

                    SetDgvReadOnly(true);

                    //string[] ConstGlobalListTables = new string[] { "GlobalProductionType", "GlobalProductionName", "TopoMSAEEPROMSet" };
                    EEPROMTabPID = getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "ID", "PN='" + this.cboPN.Text + "'");
                    initFileName();
                    string sqlCondition = "PID=" + EEPROMTabPID;
                    DataRow[] mrDRs = GlobalDS.Tables["TopoMSAEEPROMSet"].Select(sqlCondition);

                    this.lstFileName.Enabled = true;
                    for (int i = 0; i < mrDRs.Length; i++)
                    {
                        this.lstFileName.Items.Add(mrDRs[i]["ItemName"].ToString());
                    }

                    if (lstFileName.Items.Count > 0)
                    {
                        lstFileName.SelectedIndex = -1;
                        txtAllDescription.Text = "";
                        sslRunMsg.Text = "Pls choose EEPROM FileName!";
                        setTspState(false);
                    }
                    else
                    {
                        sslRunMsg.Text = "No EEPROM FileName, pls click <New> to add a new Filename!";
                        txtAllDescription.Text = "";
                        sslRunMsg.BackColor = Color.Yellow;
                        tsbtnNew.Select();
                        //mouse_event(MOUSEEVENTF_MOVE,tsmNew.l
                        //showDGVRowsInfo(showRowsCount, 0, showFristRowID, false);
                        showDGVRowsInfo(myEEPROMOperation.Data0Name, this.dgvPage00hData, 0, 0, 0, false);
                        showDGVRowsInfo(myEEPROMOperation.Data1Name, this.dgvPage01hData, 0, 0, 0, false);
                        showDGVRowsInfo(myEEPROMOperation.Data2Name, this.dgvPage02hData, 0, 0, 0, false);
                        showDGVRowsInfo(myEEPROMOperation.Data3Name, this.dgvPage03hData, 0, 0, 0, false);

                        setTspState(false);
                    }

                    if (tempPNIndex == GlobalTempPNIndex)
                    {
                        lstFileName.SelectedIndex = tempLstFileName;
                    }

                    if (lstFileName.SelectedIndex == -1)
                    {
                        tabEEPROMInfo.Enabled = false;
                    }

                    GlobalTempPNIndex = cboPN.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                GlobalDS.Tables.Clear();
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 表中的指定资料信息是否有发现?
        /// </summary>
        /// <param name="str">指定资料信息</param>
        /// <returns></returns>
        bool isCurrItemNameExisted(string str)
        {
            bool isExisted = false;
            if (str.ToUpper() == "Data0".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data0length;
            }

            else if (str.ToUpper() == "Data1".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data1length;
            }

            else if (str.ToUpper() == "Data2".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data2length;
            }

            else if (str.ToUpper() == "Data3".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data3length;
            }
            try
            {
                for (int i = 0; i < showRowsCount; i++)
                {
                    dataInfo[0, i] = "FF";
                    dataInfo[1, i] = "FF";
                }
                if (currPrmtrCountExisted(GlobalDS.Tables["TopoMSAEEPROMSet"], "PID=" + EEPROMTabPID + " and ItemName ='" + lstFileName.Text + "' and " + str + " is not null") > 0)
                {
                    isExisted = true;
                    DataRow[] DR = GlobalDS.Tables["TopoMSAEEPROMSet"].Select("PID=" + EEPROMTabPID + " and ItemName ='" + lstFileName.Text + "' and " + str + " is not null");
                    if (DR.Length == 1)
                    {
                        if (DR[0][str].ToString().Length == showRowsCount * 2)
                        {
                            for (int i = 0; i < showRowsCount; i++)
                            {
                                dataInfo[0, i] = DR[0][str].ToString().Substring(i * 2, 2);
                            }
                        }
                        else
                        {
                            MessageBox.Show(str + " Error\n当前初始化内容资料的长度不为: " + showRowsCount * 2 + ",系统将预设资料默认值: 'FF'");
                            for (int i = 0; i < showRowsCount; i++)
                            {
                                dataInfo[0, i] = "FF";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("当前资料记录不为唯一: 共发现  " + DR.Length + "条记录!");
                    }
                }
                return isExisted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return isExisted;
            }
        }

        /// <summary>
        /// 表中的指定资料信息是否有发现?
        /// </summary>
        /// <param name="str">指定资料信息</param>
        /// <param name="isCopy"></param>
        /// <returns></returns>
        bool isCurrItemNameExisted(string str, bool isCopy)
        {
            bool isExisted = false;
            if (str.ToUpper() == "Data0".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data0length;
            }

            else if (str.ToUpper() == "Data1".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data1length;
            }

            else if (str.ToUpper() == "Data2".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data2length;
            }

            else if (str.ToUpper() == "Data3".ToUpper())
            {
                showRowsCount = myEEPROMOperation.Data3length;
            }
            try
            {
                for (int i = 0; i < showRowsCount; i++)
                {
                    copyDataInfo[i] = "FF";
                }
                if (currPrmtrCountExisted(GlobalDS.Tables["TopoMSAEEPROMSet"], "PID=" + GlobalCopyPID + " and ItemName ='" + GlobalCopyItemName + "' and " + str + " is not null") > 0)
                {
                    isExisted = true;
                    DataRow[] DR = GlobalDS.Tables["TopoMSAEEPROMSet"].Select("PID=" + GlobalCopyPID + " and ItemName ='" + GlobalCopyItemName + "' and " + str + " is not null");
                    if (DR.Length == 1)
                    {
                        if (DR[0][str].ToString().Length == showRowsCount * 2)
                        {
                            for (int i = 0; i < showRowsCount; i++)
                            {
                                copyDataInfo[i] = DR[0][str].ToString().Substring(i * 2, 2);
                            }
                        }
                        else
                        {
                            MessageBox.Show(str + " Error\n当前复制内容资料的长度不为: " + showRowsCount * 2 + ",系统将预设资料默认值: 'FF'");
                            for (int i = 0; i < showRowsCount; i++)
                            {
                                copyDataInfo[i] = "FF";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("当前复制的资料记录不为唯一: 共发现  " + DR.Length + "条记录!");
                    }
                }
                return isExisted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return isExisted;
            }
        }

        /// <summary>
        /// 点击SAVE按钮，会将Page00h~Page03h所有数据数据上传至本地DataSet缓存中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            txtAllDescription.Focus();
            bool saveOK = false;
            tsbtnSave.Enabled = false;
            try
            {
                string tempDgvAddrHex = "";
                // Page00h保存
                for (int i = 0; i < dgvPage00hData.Rows.Count; i++)
                {
                    if (dgvPage00hData.Rows[i].Cells["Data"].Value.ToString().Length != 2)
                    {
                        tempDgvAddrHex = dgvPage00hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        MessageBox.Show(tabEEPROMInfo.TabPages[0].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料长度不为2,请确认!");
                        return;
                    }

                    try
                    {
                        tempDgvAddrHex = dgvPage00hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        string temp = dgvPage00hData.Rows[i].Cells["Data"].Value.ToString();
                        if ((Convert.ToByte(("0x" + temp), 16) > 0xFF))
                        {
                            MessageBox.Show(tabEEPROMInfo.TabPages[0].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(tabEEPROMInfo.TabPages[0].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                        return;
                    }
                }

                // Page01h保存
                for (int i = 0; i < dgvPage01hData.Rows.Count; i++)
                {
                    if (dgvPage01hData.Rows[i].Cells["Data"].Value.ToString().Length != 2)
                    {
                        tempDgvAddrHex = dgvPage01hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        MessageBox.Show(tabEEPROMInfo.TabPages[1].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料长度不为2,请确认!");
                        return;
                    }

                    try
                    {
                        tempDgvAddrHex = dgvPage01hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        string temp = dgvPage01hData.Rows[i].Cells["Data"].Value.ToString();
                        if ((Convert.ToByte(("0x" + temp), 16) > 0xFF))
                        {
                            MessageBox.Show(tabEEPROMInfo.TabPages[1].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(tabEEPROMInfo.TabPages[1].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                        return;
                    }
                }

                // Page02h保存
                for (int i = 0; i < dgvPage02hData.Rows.Count; i++)
                {
                    if (dgvPage02hData.Rows[i].Cells["Data"].Value.ToString().Length != 2)
                    {
                        tempDgvAddrHex = dgvPage02hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        MessageBox.Show(tabEEPROMInfo.TabPages[2].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料长度不为2,请确认!");
                        return;
                    }

                    try
                    {
                        tempDgvAddrHex = dgvPage02hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        string temp = dgvPage02hData.Rows[i].Cells["Data"].Value.ToString();
                        if ((Convert.ToByte(("0x" + temp), 16) > 0xFF))
                        {
                            MessageBox.Show(tabEEPROMInfo.TabPages[2].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(tabEEPROMInfo.TabPages[2].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                        return;
                    }
                }

                // Page03h保存
                for (int i = 0; i < dgvPage03hData.Rows.Count; i++)
                {
                    if (dgvPage03hData.Rows[i].Cells["Data"].Value.ToString().Length != 2)
                    {
                        tempDgvAddrHex = dgvPage03hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        MessageBox.Show(tabEEPROMInfo.TabPages[3].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料长度不为2,请确认!");
                        return;
                    }

                    try
                    {
                        tempDgvAddrHex = dgvPage03hData.Rows[i].Cells["AddrHex"].Value.ToString();
                        string temp = dgvPage03hData.Rows[i].Cells["Data"].Value.ToString();
                        if ((Convert.ToByte(("0x" + temp), 16) > 0xFF))
                        {
                            MessageBox.Show(tabEEPROMInfo.TabPages[3].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(tabEEPROMInfo.TabPages[3].Text + ", Addr(Hex) = " + tempDgvAddrHex + "的<初始化内容>资料无法转换为Byte类型: 00h~FFh，请确认！");
                        return;
                    }
                }

                if (blnAddNew)
                {
                    if (currPrmtrCountExisted(GlobalDS.Tables["TopoMSAEEPROMSet"], "PID=" + EEPROMTabPID + " and ItemName ='" + this.lstFileName.Text + "'") > 0)
                    {
                        MessageBox.Show("当前项目资料已经存在,请确认后再保存资料!!!");
                        return;
                    }
                }

                saveOK = EditEEPROMdata(GlobalDS.Tables["TopoMSAEEPROMSet"]);

                if (saveOK)
                {
                    runMsgState((byte)MsgState.SaveOK);

                    //tabEEPROMInfo.SelectedIndexChanged+=new EventHandler(tabEEPROMInfo_SelectedIndexChanged);
                    //if (lstFileName.Items.Contains(strNewFileName) == false)
                    //{
                    //    lstFileName.Items.Add(strNewFileName);
                    //}
                    int tempLstIndex = lstFileName.SelectedIndex;
                    GlobalTempLstFNmIndex = -1;
                    SetDgvReadOnly(true);
                    lstFileName.SelectedIndex = -1;
                    lstFileName.SelectedIndex = tempLstIndex;

                    int tempIndex = tabEEPROMInfo.SelectedIndex;
                    tabEEPROMInfo.SelectedIndex = -1;
                    tabEEPROMInfo.SelectedIndex = tempIndex;
                }
                //runMsgState((byte)MsgState.SaveOK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                System.Threading.Thread.Sleep(300);
                tsbtnSave.Enabled = true;
            }
        }

        /// <summary>
        /// Cancel按钮：放弃当前EEPROM更改内容，并退出编辑模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCancel_Click(object sender, EventArgs e)
        {
            if (blnAddNew)
            {
                tsbtnDelete_Click(sender, e);
                return;
            }

            DialogResult drst = new DialogResult();
            drst = (MessageBox.Show("Confirm undo changes to current FileName and exit the edit mode!" + "\n \n Choose 'Y' to continue?",
                "Attention",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2));

            if (drst == DialogResult.Yes)
            {
                // 重置或清除
                blnAddNew = false;
                this.strNewFileName = "";

                // 设置工具栏按钮Enable状态
                setTspState(false);

                // 设置其他按钮Enable状态
                this.cboPNType.Enabled = true;
                this.cboPN.Enabled = true;
                this.lstFileName.Enabled = true;
                this.tabEEPROMInfo.Enabled = true;

                int tempIndex = lstFileName.SelectedIndex;
                // 必须重置GlobalTempLstFileNameIndex,否则Cancel功能会失效
                GlobalTempLstFNmIndex = -1;
                SetDgvReadOnly(true);
                lstFileName.SelectedIndex = -1;
                lstFileName.SelectedIndex = tempIndex;

                sslRunMsg.Text = "Undo changes to curren EEPROM and exit edit model...";
                sslRunMsg.BackColor = Color.Yellow;
                runMsg.Refresh();
            }

        }

        //void saveModifyLog()
        //{
        //    string ss = getTablesChanges();
        //    FileStream fs;
        //    if (blnISDBSQLserver)
        //    {
        //        fs = new FileStream(Application.StartupPath + @"\SQLChangeLogs.txt", FileMode.Append);
        //    }
        //    else
        //    {
        //        fs = new FileStream(Application.StartupPath + @"\AccdbChangeLogs.txt", FileMode.Append);
        //    }
        //    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
        //    sw.WriteLine(ss);
        //    sw.Close();
        //    fs.Close();
        //    myOPlog += ss;
        //    if (blnISDBSQLserver && isMaintainUser)
        //    {
        //        updateUserLoginInfo("", false, myOPlog);
        //    }

        //}

        string updateUserLoginInfo()
        {
            string myID = "";
            try
            {
                DataTable userLoginInfoDt = myDataIO.GetDataTable("select * from UserLoginInfo", "UserLoginInfo");
                DataRow dr = userLoginInfoDt.NewRow();
                string hostname = System.Net.Dns.GetHostName(); //主机
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
                string currTime = myDataIO.GetCurrTime().ToString();
                dr["UserName"] = user;
                dr["LoginOntime"] = currTime;
                dr["LoginOfftime"] = "2000-1-1 12:00:00";
                dr["Apptype"] = Application.ProductName;
                dr["LoginInfo"] = "用户" + user + "已经在电脑" + hostname + "[" + IP4 + "]" + "登入";
                dr["OPLogs"] = "";
                userLoginInfoDt.Rows.Add(dr);
                myDataIO.UpdateDataTable("select * from UserLoginInfo", userLoginInfoDt);
                myID = myDataIO.GetDataTable("select * from UserLoginInfo where LoginOntime='" + currTime + "' and UserName ='" + user + "'", "UserLoginInfo").Rows[0]["ID"].ToString();

                return myID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return myDataIO.GetLastInsertData("UserLoginInfo").ToString();
            }
        }

        void updateUserLoginInfo(string loginOfftime, bool isLoginOFF, string logs)
        {
            try
            {
                DataTable userLoginInfoDt = myDataIO.GetDataTable("select * from UserLoginInfo", "UserLoginInfo");
                DataRow[] dr = userLoginInfoDt.Select("ID=" + myLoginID);
                string hostname = System.Net.Dns.GetHostName(); //主机
                System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);//网卡IP地址集合
                string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
                string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
                string currTime = myDataIO.GetCurrTime().ToString();
                if (dr.Length == 1)
                {
                    if (loginOfftime.Trim().Length > 0)
                    {
                        dr[0]["LoginOfftime"] = currTime;
                    }
                    if (isLoginOFF)
                    {
                        dr[0]["LoginInfo"] = "用户" + this.user + "已经在电脑" + hostname + "[" + IP4 + "]" + "登出";
                    }
                    if (logs.Trim().Length > 0)
                    {
                        dr[0]["OPLogs"] = logs;
                    }

                    myDataIO.UpdateDataTable("select * from UserLoginInfo", userLoginInfoDt);
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

        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string[] operationType = new string[1] { "" };
                string[] detailLogs = new string[1] { "" };

                if (GlobalDS.Tables["TopoMSAEEPROMSet"].GetChanges() == null)
                {
                    MessageBox.Show("Nothing is modified...Pls click this button after modifiy anything...");
                    return;
                }

                DialogResult drst = new DialogResult();
                drst = (MessageBox.Show("Confirm update modification to server!" + "\n \n Choose 'Y' to continue?",
                    "Attention",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2));

                if (drst == DialogResult.Yes)
                {
                    int tempPNTypeIndex = cboPNType.SelectedIndex;
                    int tempPNIndex = cboPN.SelectedIndex;
                    int tempFileNameIndex = lstFileName.SelectedIndex;
                    //int tempTabEEPROMInfoIndex = tabEEPROMInfo.SelectedIndex;

                    //tsmUpdate.Enabled = false;
                    sslRunMsg.Text = "Updating...Pls wait...Start time:[" + DateTime.Now.ToString() + "]";
                    runMsg.Refresh();
                    System.Threading.Thread.Sleep(10);

                    if (myDataIO.UpdateDataTable("select * from TopoMSAEEPROMSet", GlobalDS.Tables["TopoMSAEEPROMSet"]))
                    {
                        string time2 = "";
                        time2 = this.myDataIO.GetCurrTime().ToString();
                        detailLogs = getEEPROMChangeLog(out operationType);
                        updateDetailLogs(time2, operationType, detailLogs);

                        sslRunMsg.Text = "Update completed！End time:[" + DateTime.Now.ToString() + "]";
                        runMsg.Refresh();

                        ShowUpdateState showUpdateState = new ShowUpdateState();
                        showUpdateState.Text = "Update successful!";
                        showUpdateState.picShowUpdateState.Image = showUpdateState.imgUpdateState.Images[0];
                        showUpdateState.ShowDialog();
                        System.Threading.Thread.Sleep(100);
                        //saveModifyLog();

                    }
                    else
                    {
                        MessageBox.Show("Fail to update!\n System will reload the database information...");
                        sslRunMsg.Text = "Fail to update: End time:[" + DateTime.Now.ToString() + "] \n System will reload the database information...(Refresh)";
                        runMsg.Refresh();

                        ShowUpdateState showUpdateState = new ShowUpdateState();
                        showUpdateState.Text = "Update failed!";
                        showUpdateState.picShowUpdateState.Image = showUpdateState.imgUpdateState.Images[1];
                        showUpdateState.ShowDialog();
                        System.Threading.Thread.Sleep(100);
                    }

                    getDSInfo();

                    // 重置或清除
                    blnAddNew = false;
                    this.strNewFileName = "";

                    // 必须重置GlobalTemp...Index
                    GlobalTempPNTypeIndex = -1;
                    GlobalTempPNIndex = -1;
                    GlobalTempLstFNmIndex = -1;

                    this.cboPNType.SelectedIndex = -1;
                    this.cboPNType.SelectedIndex = tempPNTypeIndex;
                    this.cboPN.SelectedIndex = -1;
                    this.cboPN.SelectedIndex = tempPNIndex;
                    this.lstFileName.SelectedIndex = -1;
                    this.lstFileName.SelectedIndex = tempFileNameIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string getTablesChanges() //更新后方可调用,否则datatable的RowState会被清除
        {
            string SS = "==User:" + this.user + "于" + DateTime.Now.ToString() + "修改==\n\r";
            try
            {

                SS += "**表[" + GlobalDS.Tables["TopoMSAEEPROMSet"] + "]修改如下**\r\n";
                DataTable myDeletedDt = new DataTable();
                DataTable myAddDt = new DataTable();
                DataTable myChangeDt = new DataTable();
                myDeletedDt = GlobalDS.Tables["TopoMSAEEPROMSet"].GetChanges(DataRowState.Deleted);
                myAddDt = GlobalDS.Tables["TopoMSAEEPROMSet"].GetChanges(DataRowState.Added);
                myChangeDt = GlobalDS.Tables["TopoMSAEEPROMSet"].GetChanges(DataRowState.Modified);

                #region 每行的资料
                if (myChangeDt != null)
                {
                    for (int j = 0; j < myChangeDt.Rows.Count; j++)
                    {
                        //DataRow dataRow = TopoToatlDS.Tables[i].Rows[j];
                        DataRow dataRow = myChangeDt.Rows[j];
                        string ss1 = "修改前资料为:";
                        string ss2 = "修改后资料为:";

                        for (int k = 0; k < dataRow.ItemArray.Length; k++)
                        {
                            if (dataRow[k, DataRowVersion.Current].ToString() != dataRow[k, DataRowVersion.Original].ToString())
                            {
                                ss1 += GlobalDS.Tables["TopoMSAEEPROMSet"].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Original].ToString() + ";";
                                ss2 += GlobalDS.Tables["TopoMSAEEPROMSet"].Columns[k].ColumnName + ":" + dataRow[k, DataRowVersion.Current].ToString() + ";";
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
                        string ss1 = "已经删除资料:ID=";
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
                        string ss2 = "新增一笔资料为:ID=";
                        DataRow dataRow = myAddDt.Rows[j];
                        for (int k = 0; k < dataRow.ItemArray.Length; k++)
                        {
                            ss2 += dataRow[k, DataRowVersion.Current].ToString() + ";";
                        }
                        SS += ss2 + "\r\n";
                    }
                }
                #endregion
                //MessageBox.Show(SS);
                //TopoToatlDS.Tables[i].AcceptChanges();

                return SS;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return SS;
            }
        }
        /// <summary>
        /// 显示当前选择的资料描述
        /// </summary>
        /// <returns></returns>
        bool showCurrItemDescription(DataGridView dgv)
        {
            string ItemName, ItemDescription, allItemDescription;
            bool result = false;
            try
            {
                try
                {
                    if (dgv.CurrentCell.ColumnIndex == dgv.Columns["Data"].Index)
                    {
                        try
                        {
                            if ((Convert.ToByte(("0x" + dgv.CurrentCell.Value), 16) >= 0x0) && (Convert.ToByte(("0x" + dgv.CurrentCell.Value), 16) <= 0xFF))
                            {
                                tempDgvCurrCellValue = dgv.CurrentCell.Value.ToString();
                            }
                        }
                        catch
                        {
                            dgv.CurrentCell.Value = tempDgvCurrCellValue;
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("当前的单元格资料无法转换为Byte类型: 00h~FFh:" + ex.Message + "\n 系统将预设默认值: 'FF'");
                    dgv.CurrentCell.Value = "FF";

                    return result;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dataVaules[i] = Convert.ToByte(("0x" + dgv.Rows[i].Cells["DATA"].Value.ToString()), 16);
                    //crcdataVaules[i] = Convert.ToByte(("0x" + dgvPageData.Rows[i].Cells["crcDATA"].Value.ToString()), 16);
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    ItemDescription = myEEPROMOperation.CurrItemDescription(dataVaules, strPageNo
                            , Convert.ToByte(("0x" + dgv.Rows[i].Cells["AddrHex"].Value.ToString().Replace("h", "")), 16)
                            , Convert.ToByte(("0x" + dgv.Rows[i].Cells["DATA"].Value), 16)
                    , out ItemName, out  allItemDescription);
                    dgv.Rows[i].Cells["Item"].Value = ItemName;

                    dgv.Rows[i].Cells["ItemDescription"].Value = ItemDescription;
                    if (i == dgv.CurrentCell.RowIndex) txtAllDescription.Text = allItemDescription;
                }

                result = true;
                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return result;
            }
        }

        private void tabEEPROMInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabEEPROMInfo.SelectedIndex > -1)
                {
                    if (tabEEPROMInfo.SelectedIndex == 0)
                    {
                        dataname = "DATA0";
                        showFristRowID = myEEPROMOperation.Data0FristIndex;
                        showRowsCount = myEEPROMOperation.Data0length;
                        if (showRowsCount > 0)
                        {
                            strPageNo = myEEPROMOperation.Data0Name;
                            myLastPageNo = 0;
                        }
                        else
                        {
                            MessageBox.Show("This page does not need data maintaining now!");
                            tabEEPROMInfo.SelectedIndex = -1;
                            tabEEPROMInfo.SelectedIndex = myLastPageNo;
                            return;
                        }
                    }
                    else if (tabEEPROMInfo.SelectedIndex == 1)
                    {
                        dataname = "DATA1";
                        showFristRowID = myEEPROMOperation.Data1FristIndex;
                        showRowsCount = myEEPROMOperation.Data1length;
                        if (showRowsCount > 0)
                        {
                            strPageNo = myEEPROMOperation.Data1Name;
                            myLastPageNo = 1;
                        }
                        else
                        {
                            MessageBox.Show("This page does not need data maintaining now!");
                            tabEEPROMInfo.SelectedIndex = -1;
                            tabEEPROMInfo.SelectedIndex = myLastPageNo;
                            return;
                        }
                    }
                    else if (tabEEPROMInfo.SelectedIndex == 2)
                    {
                        dataname = "DATA2";
                        showFristRowID = myEEPROMOperation.Data2FristIndex;
                        showRowsCount = myEEPROMOperation.Data2length;
                        if (showRowsCount > 0)
                        {
                            strPageNo = myEEPROMOperation.Data2Name;
                            myLastPageNo = 2;
                        }
                        else
                        {
                            MessageBox.Show("This page does not need data maintaining now!");
                            tabEEPROMInfo.SelectedIndex = -1;
                            tabEEPROMInfo.SelectedIndex = myLastPageNo;
                            return;
                        }
                    }
                    else if (tabEEPROMInfo.SelectedIndex == 3)
                    {
                        dataname = "DATA3";
                        showFristRowID = myEEPROMOperation.Data3FristIndex;
                        showRowsCount = myEEPROMOperation.Data3length;
                        if (showRowsCount > 0)
                        {
                            strPageNo = myEEPROMOperation.Data3Name;
                            myLastPageNo = 3;
                        }
                        else
                        {
                            MessageBox.Show("This page does not need data maintaining now!");
                            tabEEPROMInfo.SelectedIndex = -1;
                            tabEEPROMInfo.SelectedIndex = myLastPageNo;
                            return;
                        }
                    }

                    System.Threading.Thread.Sleep(100);

                    if (tabEEPROMInfo.SelectedIndex == 0)
                    {
                        showCurrItemDescription(dgvPage00hData);
                    }
                    else if (tabEEPROMInfo.SelectedIndex == 1)
                    {
                        showCurrItemDescription(dgvPage01hData);
                    }
                    else if (tabEEPROMInfo.SelectedIndex == 2)
                    {
                        showCurrItemDescription(dgvPage02hData);
                    }
                    else if (tabEEPROMInfo.SelectedIndex == 3)
                    {
                        showCurrItemDescription(dgvPage03hData);
                    }
                }
                else
                {
                    //showDGVRowsInfo(256, 0, 0, isCurrItemNameExisted(cboDataPage.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void dgvCellParsing(DataGridView dgv, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgv.Columns["DATA"].Index)
                {
                    string ss = e.Value.ToString().ToUpper();
                    if (ss.Length > 2)
                    {
                        if (showCurrItemDescription(dgv) == false)
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                        }
                        //else
                        //{
                        //    dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.MistyRose;
                        //}
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = ss.PadLeft(2, '0');
                        e.ParsingApplied = true;
                        if (showCurrItemDescription(dgv) == false)
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.MistyRose;
                        }
                    }
                }
                else
                {
                    if (dgv.Rows[e.RowIndex].Cells["DATA"].Value != null)
                    {
                        string temp = dgv.Rows[e.RowIndex].Cells["DATA"].Value.ToString();
                        try
                        {
                            if ((Convert.ToByte(("0x" + temp), 16) >= 0x0) && (Convert.ToByte(("0x" + temp), 16) <= 0xFF))
                            {
                                showCurrItemDescription(dgv);
                            }
                        }
                        catch
                        {
                            dgv.Rows[e.RowIndex].Cells["DATA"].Style.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //执行转换为大写的功能!
        private void dgvPage00hData_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            dgvCellParsing(this.dgvPage00hData, e);
        }

        void dgvCellChanged(DataGridView dgv, DataGridViewCellStateChangedEventArgs e)
        {
            try
            {
                if (e.Cell.ColumnIndex == dgv.Columns["DATA"].Index)
                {
                    if (showCurrItemDescription(dgv) == false)
                    {
                        dgv.Rows[e.Cell.RowIndex].Cells[e.Cell.ColumnIndex].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        //dgv.Rows[e.Cell.RowIndex].Cells[e.Cell.ColumnIndex].Style.BackColor = Color.MistyRose;
                    }
                }
                else
                {
                    if (dgv.Rows[e.Cell.RowIndex].Cells["DATA"].Value != null)
                    {
                        string temp = dgv.Rows[e.Cell.RowIndex].Cells["DATA"].Value.ToString();
                        try
                        {
                            if ((Convert.ToByte(("0x" + temp), 16) >= 0x0) && (Convert.ToByte(("0x" + temp), 16) <= 0xFF))
                            {
                                showCurrItemDescription(dgv);
                            }
                        }
                        catch
                        {
                            dgv.Rows[e.Cell.RowIndex].Cells["DATA"].Style.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPage00hData_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            dgvCellChanged(this.dgvPage00hData, e);
        }

        void dgvMouseClick(DataGridView dgv, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == dgv.Columns["DATA"].Index)
                    {
                        if (showCurrItemDescription(dgv) == false)
                        {
                            dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            //dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.MistyRose;
                        }
                    }
                    else
                    {
                        if (dgv.Rows[e.RowIndex].Cells["DATA"].Value != null)
                        {
                            string temp = dgv.Rows[e.RowIndex].Cells["DATA"].Value.ToString();
                            try
                            {
                                if ((Convert.ToByte(("0x" + temp), 16) >= 0x0) && (Convert.ToByte(("0x" + temp), 16) <= 0xFF))
                                {
                                    showCurrItemDescription(dgv);
                                }
                            }
                            catch
                            {
                                dgv.Rows[e.RowIndex].Cells["DATA"].Style.BackColor = Color.Red;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPage00hData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvMouseClick(this.dgvPage00hData, e);
        }

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }

        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }

        void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                float newx = (this.Width) / X;
                float newy = this.Height / Y;
                setControls(newx, newy, this);
                int tempY = 0;
                int tabConY = 0;
                tabConY = this.grpEEPROMInfo.Location.Y;
                tabConY += (this.grpEEPROMInfo.Size.Height + 3);
                tempY = this.runMsg.Location.Y;
                this.txtAllDescription.Location = new Point(txtAllDescription.Location.X, tabConY);
                this.txtAllDescription.Size = new Size(this.txtAllDescription.Size.Width, tempY - 3 - tabConY);
                //this.Text = this.Width.ToString() + " " + this.Height.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void saveWriteData(string SN, byte[] WData, string SaveFileName)
        {
            try
            {
                FileStream fs;
                fs = new FileStream(Application.StartupPath + @"\" + SaveFileName + ".txt", FileMode.Append);

                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                string ss = "===" + myDataIO.GetCurrTime().ToString() + "===\r\n" + SN + "\r\n";
                for (int i = 0; i < Data0.Length; i++)
                {
                    ss += Data0[i].ToString("X").PadLeft(2, '0') + "\r\n";
                }
                sw.WriteLine(ss);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 直接计算CRC8的结果[非查表法]
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public byte Crc8(byte[] buffer)
        {
            byte crc = 0;
            for (int j = 0; j < buffer.Length; j++)
            {
                crc ^= buffer[j];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x01) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0x8c;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
                //MessageBox.Show("CRC_Result =" + crc.ToString());
            }

            return crc;
        }

        private void tsmAddData_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabEEPROMInfo.SelectedIndex == 0)
                {
                    getTXTData(this.dgvPage00hData);
                }
                else if (tabEEPROMInfo.SelectedIndex == 1)
                {
                    getTXTData(this.dgvPage01hData);
                }
                else if (tabEEPROMInfo.SelectedIndex == 2)
                {
                    getTXTData(this.dgvPage02hData);
                }
                else if (tabEEPROMInfo.SelectedIndex == 3)
                {
                    getTXTData(this.dgvPage03hData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        void getTXTData(DataGridView dgv)
        {
            bool isImportOK = true;

            string[] currDataBackup = new string[dgv.RowCount];

            for (int i = 0; i < dgv.RowCount; i++)
            {
                currDataBackup[i] = dgv.Rows[i].Cells["DATA"].Value.ToString();
            }

            try
            {
                OpenFileDialog myOpenFileDialog = new OpenFileDialog();
                myOpenFileDialog.InitialDirectory = Application.StartupPath;
                myOpenFileDialog.Filter = "txt files (*.txt)|*.txt";
                myOpenFileDialog.FilterIndex = 1;
                myOpenFileDialog.RestoreDirectory = true;
                if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string ss = myOpenFileDialog.FileName;
                    string[] strings = File.ReadAllLines(ss);
                    FileStream fs = new FileStream(ss, FileMode.Open);

                    StreamReader sr = new StreamReader(fs);
                    string ssTXT = "";
                    string readValue;

                    if (strings.Length != showRowsCount)
                    {
                        MessageBox.Show(ss + ".txt 资料有共" + strings.Length + "行,请确认-->而要求资料为" + showRowsCount + "行资料");
                    }

                    int i = 0;
                    do
                    {
                        readValue = sr.ReadLine().Trim().ToString().PadLeft(2, '0');
                        if (readValue.Length != 2)
                        {
                            isImportOK = false;
                            MessageBox.Show(ss + ".txt 资料有误,请确认-->第" + i + "行资料为: " + readValue);
                            dgv.Rows[i].Cells["DATA"].Value = "FF";
                            dgv.Rows[i].Cells["DATA"].Style.BackColor = Color.Red;
                            i++;
                            continue;
                        }
                        else
                        {
                            try
                            {
                                if ((Convert.ToByte(("0x" +  readValue), 16) > 0xFF))
                                {
                                    isImportOK = false;
                                    MessageBox.Show(ss + ".txt 资料有误,请确认-->第" + i + "行资料为: " + readValue);
                                    dgv.Rows[i].Cells["DATA"].Value = "FF";
                                    dgv.Rows[i].Cells["DATA"].Style.BackColor = Color.Red
;
                                    i++;
                                    continue;
                                }
                            }
                            catch
                            {
                                isImportOK = false;
                                MessageBox.Show(ss + ".txt 资料有误,请确认-->第" + i + "行资料为: " + readValue);
                                dgv.Rows[i].Cells["DATA"].Value = "FF";
                                dgv.Rows[i].Cells["DATA"].Style.BackColor = Color.Red;
                                i++;
                                continue;
                            }
                            ssTXT += readValue;
                            dgv.Rows[i].Cells["DATA"].Value = readValue;
                            dgv.Rows[i].Cells["DATA"].Style.BackColor = Color.Yellow;
                            i++;
                        }
                    }
                    while (i < showRowsCount && i < strings.Length);

                    txtAllDescription.Text = ssTXT;
                    sr.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                isImportOK = false;
                MessageBox.Show("===导入资料失败=== \n" + ex.ToString());
                for (int i = 0; i < showRowsCount; i++)
                {
                    dgv.Rows[i].Cells["DATA"].Value = "FF";
                    dgv.Rows[i].Cells["DATA"].Style.BackColor = Color.Red;
                }
            }

            if (!isImportOK)
            {
                if (MessageBox.Show("Find error during import data! Cancel all the import data to the original data？",
                    "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        dgv.Rows[i].Cells["DATA"].Value = currDataBackup[i];

                        if (dgv.Rows[i].Cells["DATA"].Style.BackColor == Color.Red || dgv.Rows[i].Cells["DATA"].Style.BackColor == Color.Yellow)
                        {
                            dgv.Rows[i].Cells["DATA"].Style.BackColor = Color.GreenYellow;
                        }
                    }
                }
            }
        }

        void saveTXTData(DataGridView dgv)
        {
            try
            {
                SaveFileDialog mySaveFileDialog = new SaveFileDialog();
                mySaveFileDialog.InitialDirectory = Application.StartupPath;
                mySaveFileDialog.Filter = "txt files (*.txt)|*.txt";
                mySaveFileDialog.FilterIndex = 1;
                mySaveFileDialog.RestoreDirectory = true;
                //mySaveFileDialog.FileName = this.cboPN.Text + "_" + lstFileName.Text.Trim() + "_" + columnName + tabEEPROMInfo.SelectedIndex.ToString();
                mySaveFileDialog.FileName = this.cboPN.Text + "_" + lstFileName.Text.Trim() + "_" + tabEEPROMInfo.SelectedTab.Text.ToString();
                if (mySaveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string ss = mySaveFileDialog.FileName;
                    FileStream fs = new FileStream(ss, FileMode.Create);

                    StreamWriter sw = new StreamWriter(fs);
                    string ssTXT = "";
                    string readValue;

                    int i = 0;
                    do
                    {
                        readValue = dgv.Rows[i].Cells["DATA"].Value.ToString();
                        sw.WriteLine(readValue);
                        ssTXT += readValue;
                        i++;
                    }
                    while (i < showRowsCount);

                    txtAllDescription.Text = ssTXT;
                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("===Fail to export data=== \n" + ex.ToString());
            }
        }
        private void tsmSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabEEPROMInfo.SelectedIndex == 0)
                {
                    saveTXTData(this.dgvPage00hData);
                }
                else if (tabEEPROMInfo.SelectedIndex == 1)
                {
                    saveTXTData(this.dgvPage01hData);
                }
                else if (tabEEPROMInfo.SelectedIndex == 2)
                {
                    saveTXTData(this.dgvPage02hData);
                }
                else if (tabEEPROMInfo.SelectedIndex == 3)
                {
                    saveTXTData(this.dgvPage03hData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("===Fail to export data=== \n" + ex.ToString());
            }
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isReadEEPROM || isMaintainUser)
            {
                if (MessageBox.Show("Confirm close EEPROM Maintain Program？\n\nPls make sure all changes have been updated to server before close!", "Confirm close？", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (blnISDBSQLserver && isMaintainUser)
                    {
                        updateUserLoginInfo(myDataIO.GetCurrTime().ToString(), true, "");    //141022_0
                    }
                }
            }
        }

        /// <summary>
        /// Refresh按钮定义为从服务器重新读取所有信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefresh_Click(object sender, EventArgs e)
        {
            DialogResult drst = new DialogResult();
            drst = (MessageBox.Show("Confirm refresh all data from server!" + "\nThe data which has not been updated will be discarded, pls be careful!" + "\n \n Choose 'Y' to continue?",
                "提示",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2));

            if (drst == DialogResult.Yes)
            {
                int tempPNTypeIndex = cboPNType.SelectedIndex;
                int tempPNIndex = cboPN.SelectedIndex;
                int templstFileName = lstFileName.SelectedIndex;

                if (getDSInfo() == false)
                {
                    MessageBox.Show("Fail to get data...");
                    sslRunMsg.Text = "Fail to get data... Time: " + DateTime.Now.ToString();
                    runMsg.Refresh();
                }
                else
                {
                    //// 设置工具栏按钮Enable状态
                    //tspBtnState(false);

                    //// 设置其他按钮Enable状态
                    //this.cboPNType.Enabled = true;
                    //this.cboPN.Enabled = false;
                    //this.lstFileName.Enabled = false;
                    //this.tabEEPROMInfo.Enabled = false;

                    initFileName();
                    this.dgvPage00hData.Rows.Clear();
                    this.dgvPage01hData.Rows.Clear();
                    this.dgvPage02hData.Rows.Clear();
                    this.dgvPage03hData.Rows.Clear();
                    this.txtAllDescription.Text = "";

                    // 重置或清除
                    blnAddNew = false;
                    this.strNewFileName = "";

                    // 必须重置GlobalTemp...Index
                    GlobalTempPNTypeIndex = -1;
                    GlobalTempPNIndex = -1;
                    GlobalTempLstFNmIndex = -1;

                    this.cboPNType.SelectedIndex = -1;
                    this.cboPNType.SelectedIndex = tempPNTypeIndex;
                    this.cboPN.SelectedIndex = -1;
                    this.cboPN.SelectedIndex = tempPNIndex;
                    this.lstFileName.SelectedIndex = -1;

                    if (lstFileName.Items.Count > 0)
                    {
                        this.lstFileName.SelectedIndex = 0;
                    }

                    sslRunMsg.Text = "Refresh data from server has been done...Time:[" + DateTime.Now.ToString() + "]";
                    sslRunMsg.BackColor = Color.Yellow;
                    runMsg.Refresh();
                }
            }
        }

        private void dgvPage01hData_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            dgvCellParsing(this.dgvPage01hData, e);
        }

        private void dgvPage01hData_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            dgvCellChanged(this.dgvPage01hData, e);
        }

        private void dgvPage01hData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvMouseClick(this.dgvPage01hData, e);
        }

        private void dgvPage02hData_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            dgvCellParsing(this.dgvPage02hData, e);
        }

        private void dgvPage02hData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvMouseClick(this.dgvPage02hData, e);
        }

        private void dgvPage02hData_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            dgvCellChanged(this.dgvPage02hData, e);
        }

        private void dgvPage03hData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvMouseClick(this.dgvPage03hData, e);
        }

        private void dgvPage03hData_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            dgvCellParsing(this.dgvPage03hData, e);
        }

        private void dgvPage03hData_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            dgvCellChanged(this.dgvPage03hData, e);
        }

        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            SetDgvReadOnly(!isReadOnly);
        }

        void SetLock()
        {
            cboPNType.Enabled = false;
            cboPN.Enabled = false;
            lstFileName.Enabled = false;
            sslRunMsg.Text = "Under edit mode!!! Time: " + DateTime.Now.ToString();
            tsslblEditStatus.Text = "Edit mode";
            tsslblEditStatus.BackColor = Color.Red;

            tsbtnNew.Enabled = false;
            tsbtnEdit.Enabled = false;
            tsbtnSave.Visible = true;
            tsbtnSave.Enabled = true;
            tsbtnCancel.Visible = true;
            tsbtnCancel.Enabled = true;
            tsbtnDelete.Enabled = false;
            tsbtnRefresh.Enabled = false;
            tsbtnUpdate.Enabled = false;
            this.cmsDgv.Items[0].Enabled = true;
        }

        void SetUnlock()
        {
            tsslblEditStatus.BackColor = Color.GreenYellow;
            tsslblEditStatus.Text = "Read only model";

            cboPNType.Enabled = true;
            cboPN.Enabled = true;
            lstFileName.Enabled = true;

            this.cmsDgv.Items[0].Enabled = false;
        }

        void SetDgvReadOnly(bool bln)
        {
            if (bln)
            {
                isReadOnly = true;

                if (lstFileName.SelectedIndex >= 0)
                {
                    tsbtnEdit.Enabled = true;
                }
                else
                {
                    tsbtnEdit.Enabled = false;
                }

                SetUnlock();
            }
            else
            {
                isReadOnly = false;
                tsbtnEdit.Enabled = false;
                SetLock();
            }

            dgvPage00hData.Columns["DATA"].ReadOnly = bln;
            dgvPage01hData.Columns["DATA"].ReadOnly = bln;
            dgvPage02hData.Columns["DATA"].ReadOnly = bln;
            dgvPage03hData.Columns["DATA"].ReadOnly = bln;
        }

        private void dgvPage00hData_SizeChanged(object sender, EventArgs e)
        {
            resizeDGV(dgvPage00hData);
        }

        private void dgvPage01hData_SizeChanged(object sender, EventArgs e)
        {
            resizeDGV(dgvPage01hData);
        }

        private void dgvPage02hData_SizeChanged(object sender, EventArgs e)
        {
            resizeDGV(dgvPage02hData);
        }

        private void dgvPage03hData_SizeChanged(object sender, EventArgs e)
        {
            resizeDGV(dgvPage03hData);
        }

        private void cmsFNm_Click(object sender, EventArgs e)
        {

        }

        //void getLoginStutes()   //140912_0
        //{
        //    try
        //    {
        //        string lastLoginInfo =
        //            "select spid,dbid,loginame,hostname,login_time,last_batch,status,program_name,hostprocess from master..sysprocesses " +
        //            "where   dbid=db_id('" + DBName + "') and loginame ='BackGround'";
        //        //string lastLoginInfo = "SELECT * FROM sys.dm_exec_sessions WHERE login_name ='background' order by login_time";
        //        DataTable myLoginInfo = mySQL.GetDataTable(lastLoginInfo, "myLoginInfo");
        //        if (myLoginInfo.Rows.Count > 1)// 已有用户登入...不允许编辑信息!
        //        {
        //            string loginTime = myLoginInfo.Rows[0]["login_time"].ToString();
        //            //string last_request_start_time = myLoginInfo.Rows[0]["last_request_start_time"].ToString();
        //            string last_request_start_time = myLoginInfo.Rows[0]["last_batch"].ToString();
        //            string lastComputerName = myLoginInfo.Rows[0]["host_Name"].ToString(); //= System.Environment.MachineName.ToString()

        //            if (lastComputerName.ToUpper().Trim() != System.Environment.MachineName.ToString().ToUpper().Trim())
        //            {
        //                MessageBox.Show("当前已经存在用户登入,请知悉!-->"
        //                    + "\n 登入电脑名称: " + lastComputerName
        //                    + "\n 登入时间: " + loginTime
        //                    + "\n 最后一次处理时间: " + last_request_start_time
        //                    + "\n 由于多个用户可能导致资料紊乱,故限制此次登入只能读取,不能进行更新资料到数据库! "
        //                    + "\n 如需要使用请联系已经登入的使用者!!!或稍后再登入!!!"
        //                    , "注意!"
        //                    , MessageBoxButtons.OK
        //                    , MessageBoxIcon.Information
        //                     );
        //                blnOnlyToReadFlag = true;
        //            }
        //            else
        //            {
        //                blnOnlyToReadFlag = false;
        //            }
        //        }
        //        else
        //        {
        //            blnOnlyToReadFlag = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString() + "\n 连接发生问题,可能导致资料紊乱,故限制此次登入只能读取,不能进行更新资料到数据库!");
        //        blnOnlyToReadFlag = true;
        //    }
        //}

        void updateDetailLogs(string modifyTime, string[] opType, string[] logs)   //141112_1
        {
            try
            {
                DataTable detailLogsDT = myDataIO.GetDataTable("select * from operationLogs", "operationLogs");

                string allOpType = "";
                string allLogs = "";
                for (int i = 0; i < opType.Length; i++)
                {
                    DataRow dr = detailLogsDT.NewRow();
                    if (opType[i].Trim().Length > 0)
                    {
                        //if (i + 1 < opType.Length)
                        //{
                        //    allOpType += opType[i].Trim() + "\r\n"; //141113_0
                        //}
                        //else
                        //{
                        allOpType = opType[i].Trim();
                        //}
                    }
                    if (logs[i].Trim().Length > 0)
                    {
                        //if (i + 1 < logs.Length)
                        //{
                        //    allLogs = logs[i].Trim() + "\r\n"; //141113_0
                        //}
                        //else
                        //{
                        allLogs = logs[i].Trim();
                        //}
                    }

                    if (allOpType.Trim().Length > 0)
                    {
                        dr["PID"] = myLoginID;
                        dr["ModifyTime"] = modifyTime;
                        dr["Optype"] = allOpType;
                        dr["DetailLogs"] = allLogs;
                        detailLogsDT.Rows.Add(dr);
                    }
                }
                //if (allOpType.Trim().Length > 0)
                //{
                //    dr["PID"] = myLoginID;
                //    dr["ModifyTime"] = modifyTime;
                //    dr["Optype"] = allOpType;
                //    dr["DetailLogs"] = allLogs;
                //    detailLogsDT.Rows.Add(dr);
                myDataIO.UpdateDataTable("select * from operationLogs", detailLogsDT);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Get or Save OperationLogs Error: --> \n" + ex.ToString());
            }
        }

        void writeLogToLocal(string ss)
        {
            FileStream fs = new FileStream(Application.StartupPath + @"\SQLChangeLogs.txt", FileMode.Append);

            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            if (ss.Length > 0)
            {
                sw.WriteLine("================**" + System.DateTime.Now.ToString()
                            + "**================\r\n" + ss);
            }
            sw.Close();
            fs.Close();
        }

        string getDRChangeInfo(DataRow myDatarow, string tableName)
        {
            string ss = "";
            string ss2 = "";
            try
            {
                if (myDatarow.RowState == DataRowState.Added)
                {
                    if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <EEPROMFileName=" + myDatarow["ItemName"].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <EEPROMFileName=" + myDatarow["Item"].ToString() + ">";
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
                                if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                                {
                                    sss1 += "Modified--> <EEPROMFileName=" + myDatarow["ItemName"].ToString() + "> \r\nOriginalData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                                {
                                    sss1 += "Modified--> <EEPROMFileName=" + myDatarow["Item"].ToString() + "> \r\nOriginalData:";
                                }
                                else
                                {
                                    sss1 += "Modified--> <ID=" + myDatarow["ID"].ToString() + "> \r\nOriginalData:";
                                }
                            }
                            if (sss2.Length <= 0)
                            {
                                if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                                {
                                    sss2 += "Modified--> <EEPROMFileName=" + myDatarow["ItemName"].ToString() + "> \r\nModifiedData:";
                                }
                                else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                                {
                                    sss2 += "Modified--> <EEPROMFileName=" + myDatarow["Item"].ToString() + "> \r\nModifiedData:";
                                }
                                else
                                {
                                    sss2 += "Modified--> <ID=" + myDatarow["ID"].ToString() + "> \r\nModifiedData:";
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
                    if (GlobalDS.Tables[tableName].Columns.Contains("ItemName"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <EEPROMFileName=" + myDatarow["ItemName", DataRowVersion.Original].ToString() + ">";
                    }
                    else if (GlobalDS.Tables[tableName].Columns.Contains("Item"))
                    {
                        ss2 += myDatarow.RowState.ToString() + "--> <EEPROMFileName=" + myDatarow["Item", DataRowVersion.Original].ToString() + ">";
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

        string[] getEEPROMChangeLog(out string[] operationType) //将分为一个PItem一条维护记录!
        {
            int modifyCount = 0, currCount = 0;
            string[] detailLogs = new string[1] { "" };
            operationType = new string[1] { "" };
            try
            {
                //ConstGlobalListTables = new string[] { "TopoMSAEEPROMSet", "GlobalAllEquipmentParamterList" };

                string toatalSS = "";
                string currOperationType = "";

                DataRow[] drsDelPItem = GlobalDS.Tables["TopoMSAEEPROMSet"].Select("", "ID ASC", DataViewRowState.Deleted);

                #region tablesName is not deleted

                DataRow[] drsPItem = GlobalDS.Tables["TopoMSAEEPROMSet"].Select("", "ID ASC");
                modifyCount = drsPItem.Length + drsDelPItem.Length;
                detailLogs = new string[modifyCount];
                operationType = new string[modifyCount];

                foreach (DataRow drTestPlan in drsPItem)
                {
                    string PN = "";
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName;// = drTestPlan["ItemName", DataRowVersion.Original].ToString();

                    if (drTestPlan.RowState == DataRowState.Added)
                    {
                        PN = getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "PN", "ID= " + drTestPlan["PID"]);
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Current].ToString();
                        currOperationType += "Added [PN = " + PN + "; EEPROMFileName = " + testPlanName + "]";
                    }
                    else if (drTestPlan.RowState == DataRowState.Deleted)
                    {
                        PN = getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "PN", "ID= " + drTestPlan["PID", DataRowVersion.Original]);
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                        currOperationType += "Deleted [PN = " + PN + "; EEPROMFileName = " + testPlanName + "]";
                    }
                    else
                    {
                        PN = getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "PN", "ID= " + drTestPlan["PID"]);
                        testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                        currOperationType += "Modified [PN = " + PN + "; EEPROMFileName = " + testPlanName + "]";

                    }

                    string ss = getDRChangeInfo(drTestPlan, "TopoMSAEEPROMSet");
                    if (ss.Length > 0)
                    {
                        ss = "*[TopoMSAEEPROMSet]*:  \r\n" + "<" + "PN = " + PN
                            + ", EEPROMFileName = " + testPlanName + ">\r\n" + ss;
                    }

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
                drsPItem = GlobalDS.Tables["TopoMSAEEPROMSet"].Select("", "ID ASC", DataViewRowState.Deleted);

                foreach (DataRow drTestPlan in drsPItem)
                {
                    toatalSS = "";
                    currOperationType = "";
                    string testPlanName = drTestPlan["ItemName", DataRowVersion.Original].ToString();
                    string PN = getDTColumnInfo(GlobalDS.Tables["GlobalProductionName"], "PN", "ID= " + drTestPlan["PID", DataRowVersion.Original]);
                    currOperationType += "Deleted [PN = " + PN + "; EEPROMFileName = " + testPlanName + "]";

                    string ss = getDRChangeInfo(drTestPlan, "TopoMSAEEPROMSet"); ;
                    if (ss.Length > 0)
                    {
                        ss = "*[TopoMSAEEPROMSet]*:  \r\n" + "<" + "PN = " + PN
                            + ", EEPROMFileName = " + testPlanName + ">\r\n" + ss;
                    }

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

        private void tsmRnmCurrItem_Click(object sender, EventArgs e)
        {
            int count = lstFileName.Items.Count;
            string[] fileName = new string[count];
            int tempLstFnmIndex = lstFileName.SelectedIndex;

            for (int i = 0; i < count; i++)
            {
                fileName[i] = lstFileName.Items[i].ToString().ToUpper().Trim();
            }

            NewFileName myname = new NewFileName(fileName);
            myname.Text = "Pls input a new file name!";
            myname.ShowDialog();

            if (myname.isNewFileNameOK)
            {
                string filterString = this.lstFileName.Text;
                DataRow[] myROWS = GlobalDS.Tables["TopoMSAEEPROMSet"].Select("ItemName='" + filterString + "' AND PID=" + EEPROMTabPID);
                if (myROWS.Length == 1)
                {
                    myROWS[0].BeginEdit();
                    myROWS[0]["ItemName"] = myname.name;
                    myROWS[0].EndEdit();
                    initFileName();
                    string sqlCondition = "PID=" + EEPROMTabPID;
                    DataRow[] mrDRs = GlobalDS.Tables["TopoMSAEEPROMSet"].Select(sqlCondition);

                    this.lstFileName.Enabled = true;
                    for (int i = 0; i < mrDRs.Length; i++)
                    {
                        this.lstFileName.Items.Add(mrDRs[i]["ItemName"].ToString());
                    }
                }

                GlobalTempLstFNmIndex = -1;
                lstFileName.SelectedIndex = -1;
                lstFileName.SelectedIndex = tempLstFnmIndex;
            }
        }

        private void tsmCopyCurrLstItem_Click(object sender, EventArgs e)
        {
            GlobalCopyPID = EEPROMTabPID;
            GlobalCopyItemName = lstFileName.Text;

            CopyLstFNmItem myCopyLstFNmItem = new CopyLstFNmItem(GlobalDS, cboPNType.SelectedIndex);
            myCopyLstFNmItem.ShowDialog();

            if (myCopyLstFNmItem.isCopyOK)
            {
                isCopy = true;
                GlobalTempPNIndex = -1;
                GlobalTempLstFNmIndex = -1;
              
                cboPN.SelectedIndex = -1;
                cboPN.SelectedIndex = myCopyLstFNmItem.GlobalTempPNIndex;

                if (lstFileName.Items.Contains(myCopyLstFNmItem.name) == false)
                {
                    lstFileName.Items.Add(myCopyLstFNmItem.name);
                }

                cboPNType.Enabled = false;
                cboPN.Enabled = false;
                lstFileName.Enabled = false;

                setTspState(true);

                int tempIndex = lstFileName.Items.IndexOf(myCopyLstFNmItem.name);
                lstFileName.SelectedIndex = tempIndex; 

                tsbtnSave_Click(sender, e);
                sslRunMsg.Text = "Successed copy data!!! Time: " + DateTime.Now.ToString();
                isCopy = false;
                myCopyLstFNmItem.name = "";
            }
        }

        private void lstFileName_MouseDown(object sender, MouseEventArgs e)
        {
            if (isMaintainUser)
            {
                try
                {
                    if (this.lstFileName.SelectedIndex >= 0)
                    {
                        if (e.Button == MouseButtons.Right)//判断是否右键点击
                        {
                            Point p = e.Location;//获取点击的位置
                            int index = this.lstFileName.IndexFromPoint(p);//根据位置获取右键点击项的索引

                            if (this.lstFileName.SelectedIndex == index)
                            {
                                this.cmsLstFNm.Visible = true;
                                this.cmsLstFNm.Enabled = true;
                                this.tsmRnmCurrItem.Enabled = true;
                                this.tsmCopyCurrLstItem.Enabled = true;

                                this.cmsLstFNm.Show(MousePosition);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            tsbtnEdit_Click(sender, e);
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            tsbtnDelete_Click(sender, e);
        }
    }
}