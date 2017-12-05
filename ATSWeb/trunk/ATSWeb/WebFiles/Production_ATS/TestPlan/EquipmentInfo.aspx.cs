﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.Common;

public partial class WebFiles_TestPlan_TopoEquipment : BasePage
{
    const string funcItemName = "设备信息";
    Frame_TestPlan_TopoEquipInfo EqControl;
    Frame_TestPlan_TopoEquipParam[] ControlList;
    int rowCount = 0;

   private CommCtrl pCommCtrl = new CommCtrl();
   private DataIO pDataIO;
   private string EquipID = "";
   string TestPlanID = "";
   private bool AddNew = false;
   private string logTracingString = "";
   DropDownList ddlItemName = new DropDownList();
   DropDownList ddlItemType = new DropDownList();
   DropDownList ddlRole = new DropDownList();
   private ValidateExpression validExpre;

    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();

        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = "";
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();

        if (Session["DB"] == null)
        {
            Response.Redirect("~/Default.aspx", true);
        }
        else if (Session["DB"].ToString().ToUpper() == "ATSDB")
        {
            dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        }
        else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
        {
            dbName = ConfigurationManager.AppSettings["DbName2"].ToString();
        }
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        validExpre = new ValidateExpression();

        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(1);
        if (Request.QueryString["uId"] != null)
        {
            EquipID = Request.QueryString["uId"];
        }

        if (Request.QueryString["TestPlanID"] != null)
        {
            TestPlanID = Request.QueryString["TestPlanID"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            EquipID = "-1";
        }

        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ShowName from GlobalAllEquipmentList where ID=(select GID from TopoEquipment where id = " + EquipID + ")").ToString();
        if (AddNew)
        {
            parentItem = "添加新项";
        }
        Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
        this.plhNavi.Controls.Add(myCtrl);
    }

    void initPageInfo()
    {
        try
        {
            ConfigOptionButtonsVisible();
            if (AddNew && TestPlanID.Trim().Length > 0)
            {
                createNavilnks();
                OptionButtons1.ConfigBtCancelVisible = true;
                OptionButtons1.ConfigBtSaveVisible = true;
                OptionButtons1.ConfigBtEditVisible = false;
                ddlItemName.Items.Clear();
                ddlItemType.Items.Clear();
                ddlRole.Items.Clear();
                ddlRole.Items.Add("NA");
                ddlRole.Items.Add("TX");
                ddlRole.Items.Add("RX");
                ddlItemName.Enabled = false;
                ddlRole.Enabled = false;

                ddlItemType.SelectedIndexChanged += new EventHandler(ddlItemType_SelectedIndexChanged);
                ddlItemType.AutoPostBack = true;

                ddlItemName.SelectedIndexChanged += new EventHandler(ddlItemName_SelectedIndexChanged);
                ddlItemName.AutoPostBack = true;
                DataTable globalEquip = pDataIO.GetDataTable("select * from GlobalAllEquipmentList", "GlobalAllEquipmentList");
                DataTable dtType = globalEquip.DefaultView.ToTable(true, "ItemType");

                ddlItemType.Items.Add("");
                foreach (DataRow dr in dtType.Rows)
                {
                    string ss = dr["ItemType"].ToString();

                    ddlItemType.Items.Add(ss);
                }
                rowCount = 0;
                Label lb1 = new Label();
                lb1.Text = "设备类型：";
                lb1.Attributes.Add("style", "font-family :微软雅黑;font-size: 16px;padding-left:5px;line-height :31px;");
                ddlItemType.Attributes.Add("style", "width:150px; height:24px;");
                this.plhMain.Controls.Add(lb1);
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("Span", ddlItemType));
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("br", ""));

                Label lb2 = new Label();
                lb2.Text = "设备名称：";
                lb2.Attributes.Add("style", "font-family :微软雅黑;font-size: 16px;padding-left:5px;");
                ddlItemName.Attributes.Add("style", "width:150px; height:24px;");
                this.plhMain.Controls.Add(lb2);
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("Span", ddlItemName));
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("br", ""));

                Label lb3 = new Label();
                lb3.Text = "设备功能：";
                lb3.Attributes.Add("style", "font-family :微软雅黑;font-size: 16px;padding-left:5px;");
                ddlRole.Attributes.Add("style", "width:150px; height:24px;");
                this.plhMain.Controls.Add(lb3); 
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("Span", ddlRole));
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("br", ""));
                //this.plhMain.Controls.Add(new HtmlGenericControl("<tr/>"));
                //this.plhMain.Controls.Add(new HtmlGenericControl("hr"));
                if (ddlItemName.Text.Trim().Length > 0)
                {
                    disposeParamsTable();
                    DataTable globalDtParam = pDataIO.GetDataTable("select * from GlobalAllEquipmentParamterList where pid=(select id from GlobalAllEquipmentList where ShowName='"
                + ddlItemName.Text + "') order by ID", "GlobalAllEquipmentParamterList");                    
                    getParamList("-1", globalDtParam, true);
                }
            }
            else if (EquipID != null && EquipID.Length > 0)
            {
                createNavilnks();
                object obj = pDataIO.getDbCmdExecuteScalar("select ShowName from GlobalAllEquipmentList where ID=(select GID from TopoEquipment where id = " + EquipID + ")");
                if (obj != null)
                {
                    string ShowName = obj.ToString();
                    //if (ShowName.ToString().Contains('_'))
                    //{
                    //    int findFirstChar = ShowName.IndexOf('_');
                    //    ShowName = ShowName.Substring(0, findFirstChar).ToString();
                    //}
                    
                    getInfo("select * from topoEquipment where id=" + EquipID);
                    DataTable globalDtParam =
                        pDataIO.GetDataTable("select * from GlobalAllEquipmentParamterList where PID = (select id from GlobalAllEquipmentList where ShowName='"
                            + ShowName + "') order by ID", "GlobalAllEquipmentParamterList");

                    getParamList(EquipID, globalDtParam);
                }
                else
                {
                    OptionButtons1.Visible = false;
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('Error!找不到任何参数名称')</Script>");
                }
            }
            else
            {
                OptionButtons1.Visible = false;
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>");
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemType.SelectedIndex != -1)
        {
            disposeParamsTable();
            DataTable globalEquip = pDataIO.GetDataTable("select * from GlobalAllEquipmentList", "GlobalAllEquipmentList");
            DataRow[] drNames = globalEquip.Select("ItemType='" + ddlItemType.Text + "'");
            ddlItemName.Items.Clear();
            ddlItemName.Items.Add("");
            foreach (DataRow dr in drNames)
            {
                string ss = dr["ShowName"].ToString();
                ddlItemName.Items.Add(ss);
                ddlRole.Enabled = true;
                ddlItemName.Enabled = true;
            }
            ddlItemName.SelectedIndex = -1;
            ddlRole.SelectedIndex = -1;
        }
    }

    void disposeParamsTable()
    {
        Control pTitle = plhMain.FindControl("EquipmentInfo");
        if (pTitle != null)
        {
            plhMain.Controls.Remove(pTitle);
        }
        Control paramTitle = plhMain.FindControl("EquipParamsInfo");
        if (paramTitle != null)
        {
            plhMain.Controls.Remove(paramTitle);
        }
        Control paramLog = plhMain.FindControl("EquipParamsLog");
        if (paramLog != null)
        {
            plhMain.Controls.Remove(paramLog);
        }
        Control pTable = plhMain.FindControl("EqParamsTable");
        if (pTable != null)
        {
            plhMain.Controls.Remove(pTable);
        }
    }

    void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemName.SelectedIndex != -1)
        {
            disposeParamsTable();
            DataTable globalDtParam = pDataIO.GetDataTable("select * from GlobalAllEquipmentParamterList where pid=(select id from GlobalAllEquipmentList where ShowName='"
                + ddlItemName.Text + "') order by ID", "GlobalAllEquipmentParamterList");
            getParamList("-1", globalDtParam, true);
            if (ddlItemName.SelectedIndex != 0)
            {
                OptionButtons1.ConfigBtSaveVisible = true;
            }
            else
            {
                OptionButtons1.ConfigBtSaveVisible = false;
            }
        }

    }

    bool getInfo(string filterStr, bool isEditState = false)
    {
        DataTable pReader;
        Table pTable = new Table();
        pTable.ID = "EqInfoTable";
        
        try
        {
            pReader = pDataIO.GetDataTable(filterStr,"");
            DataTable globalEqDt = pDataIO.GetDataTable("select * from GlobalAllEquipmentList", "GlobalAllEquipmentList");

            //先缓存当前的Global资料!
            string ShowName = "", ItemType = "", ItemDesc = "";
            
            if (pReader.Rows.Count==1)
            {
                DataRow[] drs = globalEqDt.Select("ID=" + pReader.Rows[0]["Gid"]);
                //必须先执行查询到Global的资料后再开始填充数据!
                if (drs.Length == 1)
                {
                    ShowName = drs[0]["ShowName"].ToString();
                    ItemType = drs[0]["ItemType"].ToString();
                    ItemDesc = drs[0]["ItemDescription"].ToString();
                }

                EqControl = (Frame_TestPlan_TopoEquipInfo)Page.LoadControl("~/Frame/TestPlan/TopoEquipInfo.ascx");

                EqControl.ID = "EquipList_" + pReader.Rows[0]["ID"].ToString();
                EqControl.TxtItemName = ShowName;
                EqControl.TxtItemType = ItemType;
                EqControl.TxtDdlRole = pReader.Rows[0]["Role"].ToString();
                EqControl.TxtLblSeq = pReader.Rows[0]["SEQ"].ToString();
                EqControl.TxtItemDescription = ItemDesc;
                EqControl.SetEquipEnableState(false);

                TableCell tc = new TableCell();
                tc.Controls.Add(EqControl);
                TableRow tr = new TableRow();
                tr.Controls.Add(tc);
                pTable.Rows.Add(tr);
            }
            //Control title = pCommCtrl.CreateHtmlGenericControl("h4", "<EquipmentInfo>");
            //title.ID = "EquipmentInfo";
            //this.plhMain.Controls.Add(title);
            this.plhMain.Controls.Add(pTable);

            //this.plhMain.Controls.Add(new HtmlGenericControl("hr"));
            //this.plhMain.Controls.Add(new HtmlGenericControl("tr"));           

            return true;
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            throw ex;
        }
    }

    bool getParamList(string paramsPID, DataTable globalDtParam, bool isEditState = false)
    {
        DataTable pReaderTable;
        Table pTable=new Table();
        try
        {
            pTable.ID = "EqParamsTable";            
            List<string> pHeaderLst = new List<string>();
            pHeaderLst.Add("");
            pHeaderLst.Add("名称");
            pHeaderLst.Add("参数类型");
            pHeaderLst.Add("值");
            pHeaderLst.Add("描述");

            TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pHeaderLst);
            thr.Cells[0].Attributes.Add("style", "width:0px;border:none;background:#ffffff;");
            thr.Cells[1].Attributes.Add("style", "width:160px;font-family:'Times New Roman Negreta', 'Times New Roman';"
            + "font-weight:700;font-size:15px;text-align:center;color:White;vertical-align:middle;border:solid #CCCCCC 1px;background-image:url('../../../Images/TableHeader.png');background-repeat:repeat-x;height:27px;");
            thr.Cells[2].Attributes.Add("style", "width:80px;font-family:'Times New Roman Negreta', 'Times New Roman';"
           + "font-weight:700;font-size:15px;text-align:center;color:White;vertical-align:middle;border:solid #CCCCCC 1px;background-image:url('../../../Images/TableHeader.png');background-repeat:repeat-x;height:27px;");
            thr.Cells[3].Attributes.Add("style", "width:150px;font-family:'Times New Roman Negreta', 'Times New Roman';"
           + "font-weight:700;font-size:15px;text-align:center;color:White;vertical-align:middle;border:solid #CCCCCC 1px;background-image:url('../../../Images/TableHeader.png');background-repeat:repeat-x;height:27px;");
            thr.Cells[4].Attributes.Add("style", "width:260px;font-family:'Times New Roman Negreta', 'Times New Roman';"
           + "font-weight:700;font-size:15px;text-align:center;color:White;vertical-align:middle;border:solid #CCCCCC 1px;background-image:url('../../../Images/TableHeader.png');background-repeat:repeat-x;height:27px;");
            if (thr != null)
            {
                pTable.Controls.Add(thr);
            }
            rowCount = globalDtParam.Rows.Count;
            ControlList = new Frame_TestPlan_TopoEquipParam[rowCount];

            string filterStr = "select * from TopoEquipmentParameter where pid=" + paramsPID;

            for (int i = 0; i < globalDtParam.Rows.Count; i++)
            {
                //先缓存当前的Global资料!
                string ShowName = "", ItemType = "", ItemDesc = "", ItemValue = "", GID = "";
                DataRow[] drs = globalDtParam.Select("ItemName='" + globalDtParam.Rows[i]["ItemName"].ToString() + "'");
                //必须先执行查询到Global的资料后再开始填充数据!
                if (drs.Length == 1)
                {
                    GID = drs[0]["ID"].ToString();
                    ItemValue = drs[0]["ItemValue"].ToString();
                    ShowName = drs[0]["ShowName"].ToString();
                    ItemType = drs[0]["ItemType"].ToString();
                    ItemDesc = drs[0]["ItemDescription"].ToString();
                    string ItemTypeString = ItemType.Replace(" ", "");
                    ControlList[i] = (Frame_TestPlan_TopoEquipParam)Page.LoadControl("~/Frame/TestPlan/TopoEquipParam.ascx");
                    ConfigValidator(ItemTypeString.ToUpper().Trim(), ControlList[i]);
                    pReaderTable = pDataIO.GetDataTable(filterStr + " and GID=" + GID, "TopoEquipmentParameter");
                    ControlList[i].setEnableState(isEditState);
                    //ControlList[i].chkIDEquipParamVisable = false;

                    ControlList[i].pToolTipItemValue = "default:" + ItemValue;
                    if (pReaderTable != null && pReaderTable.Rows.Count == 1)     //因为每次就一条记录故改用if(){}
                    {
                        ControlList[i].ID = "EquipParam_" + pReaderTable.Rows[0]["ID"].ToString();
                        ControlList[i].pLblItem = ShowName;
                        ControlList[i].pTxtItemValue = pReaderTable.Rows[0]["ItemValue"].ToString();
                        ControlList[i].pLblItemType = ItemType;
                        ControlList[i].pLblItemDescription = ItemDesc;
                        ControlList[i].myControlID = "ID_" + GID;
                    }
                    else
                    {
                        ControlList[i].ID = "EquipParam_New_" + i.ToString();
                        //ControlList[i].pChkEquipParamTxt = "New: ";
                        ControlList[i].pLblItem = "[NewParam]" + ShowName;
                        ControlList[i].pTxtItemValue = ItemValue;
                        ControlList[i].pLblItemType = ItemType;
                        ControlList[i].pLblItemDescription = ItemDesc;
                        ControlList[i].myControlID = "ID_" + GID;
                    }

                    TableCell tc = new TableCell();
                    tc.Controls.Add(ControlList[i]);
                    TableRow tr = new TableRow();
                    tr.Controls.Add(tc);
                    pTable.Rows.Add(tr);
                }
                else
                {

                }
            }
            //Control title = pCommCtrl.CreateHtmlGenericControl("h4", "EquipParamsInfo");
            Image img = new Image();
            img.ImageUrl = "~/Images/List.gif";
            img.ID = "EquipParamsLog";
            img.Attributes.Add("style", "Height:20px;position:relative;padding-top:15px;");
            Label title = new Label();
            title.ID = "EquipParamsInfo";
            title.Text = "设备参数信息";
            title.Attributes.Add("style", "font-family: 'Times New Roman Negreta', 'Times New Roman';font-size: 16px;font-weight: 800;line-height :30px;");
            pTable.Attributes.Add("style", "cellspacing:0; cellpadding:0; border:none; border-collapse:collapse;word-break:break-word;word-break : break-all;");
            this.plhMain.Controls.Add(img); 
            this.plhMain.Controls.Add(title);            
            this.plhMain.Controls.Add(pTable);
            return true;
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            throw ex;
        }
    }

    public bool AddData(object obj, string prameter)
    {
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {

        return true;
    }

    public bool EditData(object obj, string prameter)
    {
        try
        {
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtCancelVisible = true;
            OptionButtons1.ConfigBtEditVisible = false;
            if (EqControl != null)
            {
                EqControl.SetEquipEnableState(true);
            }
            if (ControlList != null)
            {
                for (int i = 0; i < ControlList.Length; i++)
                {
                    ControlList[i].setEnableState(true);
                }
            }

            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool SaveData(object obj, string prameter)
    {
        try
        {
            DataSet EqDS = new DataSet();
            string strEqInfo = "select * from TopoEquipment where id =" + EquipID;
            string strEqParam = "select * from TopoEquipmentParameter where pid =" + EquipID;
            DataTable eqDt = pDataIO.GetDataTable(strEqInfo, "TopoEquipment");
            DataTable eqParamsDt = pDataIO.GetDataTable(strEqParam, "TopoEquipmentParameter");

            DataTable GlobalEqDt = pDataIO.GetDataTable("select * from GlobalAllEquipmentList", "GlobalAllEquipmentList");
            DataTable GlobalEqParamsDt = pDataIO.GetDataTable("select * from GlobalAllEquipmentParamterList", "GlobalAllEquipmentParamterList");

            EqDS.Tables.Add(eqDt);
            EqDS.Tables.Add(eqParamsDt);
            EqDS.Relations.Add("EqRelation", eqDt.Columns["ID"], eqParamsDt.Columns["pID"]);

            if (eqDt.Rows.Count == 1)
            {
                #region //已经获取到当前设备的信息
                string EqGid =eqDt.Rows[0]["GID"].ToString();
                eqDt.Rows[0]["Role"] = EqControl.TxtDdlRole;

                #region EqParams
                string paramsPid = eqDt.Rows[0]["ID"].ToString();
                for (int i = 0; i < ControlList.Length; i++)
                {
                    //若为新增的参数则需要Add
                    if (ControlList[i] != null)
                    {
                        string myGid = ControlList[i].myControlID.ToString().Split('_')[1];
                        //string myGid = pCommCtrl.getDTColumnInfo(GlobalEqParamsDt, "ID", "PID=" + EqGid + " and ID ='" + ControlList[i].myControlID + "'");
                        if (ControlList[i].ID.ToString().Contains("EquipParam_New_"))
                        {                            
                            DataRow dr = eqParamsDt.NewRow();
                            dr["pid"] = paramsPid;
                            dr["Gid"] = myGid;
                            dr["ItemValue"] = ControlList[i].pTxtItemValue;
                            eqParamsDt.Rows.Add(dr);
                        }
                        else
                        {
                            for (int j = 0; j < eqParamsDt.Rows.Count; j++)
                            {
                                if (eqParamsDt.Rows[j]["GID"].ToString().ToLower().Trim() == myGid)
                                {
                                    eqParamsDt.Rows[j]["ItemValue"] = ControlList[i].pTxtItemValue;
                                }
                            }
                        }
                    }
                }
                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("TopoEquipment", eqDt, eqParamsDt, logTracingString);
                if (result > 0)
                {
                    eqDt.AcceptChanges();
                    eqParamsDt.AcceptChanges();
                    Response.Redirect("~/WebFiles/Production_ATS/TestPlan/EquipmentList.aspx?uId=" + TestPlanID, false);
                }
                else
                {
                    pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End
                #endregion
            }
                #endregion

            else if (AddNew && ddlItemName.Text.Length > 0)
            {
                #region //New
                string EqGid = pCommCtrl.getDTColumnInfo(GlobalEqDt, "ID", "ShowName='" + ddlItemName.Text + "'");
                string MaxSeq = pDataIO.getDbCmdExecuteScalar("select max(SEQ) from TopoEquipment where PID =" + TestPlanID).ToString();
                long currEqID = pDataIO.GetLastInsertData("TopoEquipment") + 1;
                long currEqParamID = pDataIO.GetLastInsertData("TopoEquipmentParameter") + 1;
                int myNewSeq = 1;
                if (MaxSeq.Length > 0)
                {
                    myNewSeq = Convert.ToInt32(MaxSeq) + 1;
                }
                DataRow eqNewDr = eqDt.NewRow();
                eqNewDr["ID"] = currEqID;
                eqNewDr["PID"] = TestPlanID;
                eqNewDr["GID"] = EqGid;
                eqNewDr["SEQ"] = myNewSeq;

                if (ddlRole.SelectedValue == "NA")
                {
                    eqNewDr["Role"] = "0";
                }
                else if (ddlRole.SelectedValue == "TX")
                {
                    eqNewDr["Role"] = "1";
                }
                else if (ddlRole.SelectedValue == "RX")
                {
                    eqNewDr["Role"] = "2";
                }
                else
                {
                    eqNewDr["Role"] = "0";
                }
                eqDt.Rows.Add(eqNewDr);

                //-->Create NewEqParamsDt rows
                for (int i = 0; i < ControlList.Length; i++)
                {
                    string myGid = ControlList[i].myControlID.ToString().Split('_')[1];
                    //string myGid = pCommCtrl.getDTColumnInfo(GlobalEqParamsDt, "ID", "PID=" + EqGid + " and ID ='" + ControlList[i].myControlID + "'");
                    DataRow dr = eqParamsDt.NewRow();
                    dr["id"] = currEqParamID;
                    dr["pid"] = currEqID;
                    dr["Gid"] = myGid;
                    dr["ItemValue"] = ControlList[i].pTxtItemValue;
                    eqParamsDt.Rows.Add(dr);
                    currEqParamID++;
                }
                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("TopoEquipment", eqDt, eqParamsDt, logTracingString);
                if (result > 0)
                {
                    eqDt.AcceptChanges();
                    eqParamsDt.AcceptChanges();
                    Response.Redirect("~/WebFiles/Production_ATS/TestPlan/EquipmentList.aspx?uId=" + TestPlanID, false);
                }
                else
                {
                    pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End               

                #endregion
            }
            else if (AddNew && ddlItemName.Text.Length == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('请选择设备类型!')</Script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('Error!当前项已被删除!')</Script>");
            }

            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/Production_ATS/TestPlan/EquipmentList.aspx?uId=" + TestPlanID, false);
            }
            else
            {
                Response.Redirect(Request.Url.ToString(), false);
            }
            
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public void ConfigOptionButtonsVisible()
    {
        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);
        if (editVisible)
        {
             OptionButtons1.ConfigBtEditVisible =true;
        } 
        else
        {
             OptionButtons1.ConfigBtEditVisible =GetTestPlanAuthority();
        }
       
        OptionButtons1.ConfigBtDeleteVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    public void ConfigValidator(string inputType, Frame_TestPlan_TopoEquipParam inputControl)
    {
        try
        {
            switch (inputType)
            {
                case "STRING":
                case "string":
                    {                       
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItemValue = false;
                        break;
                    }
                case "BOOL":
                case "bool":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItemValue = true;
                        inputControl.ConfigRegularExpressionValidatorItemValueVExprssion = validExpre.GBoolExpression;
                        inputControl.ConfigRegularExpressionValidatorItemValueVErrMessage = "请输入0或false、1或true";
                        break;
                    }
                case "BYTE":
                case "byte":
                    {
                        inputControl.EnableRangeValidatorItemValue = true;
                        inputControl.EnableRegularExpressionValidatorItemValue = false;
                        inputControl.ConfigRangeValidatorItemValueType = ValidationDataType.Integer;
                        inputControl.ConfigRangeValidatorItemValueMax = Convert.ToString(255);
                        inputControl.ConfigRangeValidatorItemValueMin = Convert.ToString(0);

                        break;
                    }
                case "DOUBLE":
                case "double":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItemValue = true;
                        inputControl.ConfigRegularExpressionValidatorItemValueVExprssion = validExpre.GDoubleorScientificNotation;
                        inputControl.ConfigRegularExpressionValidatorItemValueVErrMessage = "请输入双精度浮点数，可用科学计数法表示";

                        break;
                    }
                case "INT":
                case "int":
                    {
                        inputControl.EnableRangeValidatorItemValue = true;
                        inputControl.EnableRegularExpressionValidatorItemValue = false;
                        inputControl.ConfigRangeValidatorItemValueType = ValidationDataType.Integer;
                        inputControl.ConfigRangeValidatorItemValueMax = Convert.ToString(32767);
                        inputControl.ConfigRangeValidatorItemValueMin = Convert.ToString(-32768);

                        break;
                    }
                case "ARRAYLIST":
                case "ArrayList":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItemValue = true;
                        inputControl.ConfigRegularExpressionValidatorItemValueVExprssion = validExpre.GFloatIntandCommaExpression;
                        inputControl.ConfigRegularExpressionValidatorItemValueVErrMessage = "请输入单精度浮点数，并用逗号隔开";
                        break;
                    }
                default:
                    break;
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool GetTestPlanAuthority()
    {
        string userID = Session["UserID"].ToString().Trim();
        bool tpAuthority = false;
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                DataTable pnId = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + TestPlanID, "TopoTestPlan");

                if (pnId.Rows.Count == 1)
                {
                    string PNid = pnId.Rows[0]["PID"].ToString();
                    DataTable pnAuthority = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + PNid, "UserPNAction");
                    if (pnAuthority.Rows.Count == 1)
                    {
                        if (pnAuthority.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "TRUE" || pnAuthority.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                    }
                }


                {

                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + TestPlanID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {

                        //tpAuthority = false;
                    }
                    else
                    {
                        if (temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                        //else
                        //{
                        //    tpAuthority = false;
                        //}

                    }
                }

            }
            return tpAuthority;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}