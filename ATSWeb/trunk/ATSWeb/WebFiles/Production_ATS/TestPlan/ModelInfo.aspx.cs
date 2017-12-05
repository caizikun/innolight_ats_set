using System;
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

public partial class WebFiles_TestPlan_ModelInfo : BasePage
{
    const string funcItemName = "测试模型信息";
    Frame_TestPlan_ModelInfo ModelControl;
    Frame_TestPlan_ModelParams[] ControlList;

    //Frame_TestPlan_ModelInfo ModelControl;
    //Frame_TestPlan_ModelParams[] ControlList;
    int rowCount = 0;
    private string logTracingString = "";
    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string ModelID = "";
    string CtrlID = "";
    bool AddNew = false;
    DropDownList ddlItemName = new DropDownList();
    DropDownList ddlIgnoreFlag = new DropDownList();
    DropDownList ddlFailBreak = new DropDownList();
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
        validExpre = new ValidateExpression();
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);

        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(1);
        if (Request.QueryString["uId"] != null)
        {
            ModelID = Request.QueryString["uId"];
        }

        if (Request.QueryString["CtrlID"] != null)
        {
            CtrlID = Request.QueryString["CtrlID"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            ModelID = "-1";
        }

        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ShowName from GlobalAllTestModelList where ID=(select GID from topoTestModel where id = " + ModelID + ")").ToString();
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
            if (AddNew && CtrlID.Trim().Length > 0)
            {
                createNavilnks();
                OptionButtons1.ConfigBtCancelVisible = true;
                OptionButtons1.ConfigBtSaveVisible = true;
                OptionButtons1.ConfigBtEditVisible = false;
                ddlItemName.Items.Clear();
                ddlIgnoreFlag.Items.Clear();
                ddlIgnoreFlag.Items.Add("false");
                ddlIgnoreFlag.Items.Add("true");
                ddlIgnoreFlag.Enabled = false;

                ddlFailBreak.Items.Clear();
                ddlFailBreak.Items.Add("false");
                ddlFailBreak.Items.Add("true");
                ddlFailBreak.Enabled = false;


                ddlItemName.SelectedIndexChanged += new EventHandler(ddlItemName_SelectedIndexChanged);
                ddlItemName.AutoPostBack = true;
                DataTable globalInfo = pDataIO.GetDataTable("select * from GlobalAllTestModelList", "GlobalAllTestModelList");
                DataTable dtType = globalInfo.DefaultView.ToTable(true, "ShowName");

                ddlItemName.Items.Add("");
                foreach (DataRow dr in dtType.Rows)
                {
                    string ss = dr["ShowName"].ToString();
                    ddlItemName.Items.Add(ss);
                }
                rowCount = 0;
                Label lb1 = new Label();
                lb1.Text = "选择测试模型：";
                lb1.Attributes.Add("style", "font-family :微软雅黑;font-size: 16px;padding-left:5px;line-height :31px;");
                ddlItemName.Attributes.Add("style", "width:150px; height:24px;");
                this.plhMain.Controls.Add(lb1);
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("Span", ddlItemName));
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("br", ""));

                Label lb2 = new Label();
                lb2.Text = "是否跳过：";
                lb2.Attributes.Add("style", "font-family :微软雅黑;font-size: 16px;padding-left:5px;");
                ddlIgnoreFlag.Attributes.Add("style", "width:150px; height:24px;");
                this.plhMain.Controls.Add(lb2);
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("Span", ddlIgnoreFlag));
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("br", ""));

                Label lb3 = new Label();
                lb3.Text = "出错是否停止：";
                lb3.Attributes.Add("style", "font-family :微软雅黑;font-size: 16px;padding-left:5px;");
                ddlFailBreak.Attributes.Add("style", "width:150px; height:24px;");
                this.plhMain.Controls.Add(lb3);
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("Span", ddlFailBreak));
                this.plhMain.Controls.Add(pCommCtrl.CreateHtmlGenericControl("br", ""));

                //this.plhMain.Controls.Add(new HtmlGenericControl("hr"));

                if (ddlItemName.Text.Trim().Length > 0)
                {
                    disposeParamsTable();
                    DataTable globalDtParam = pDataIO.GetDataTable("select * from GlobalTestModelParamterList where pid=(select id from GlobalAllTestModelList where ShowName='"
                        + ddlItemName.Text + "')", "GlobalTestModelParamterList");
                    getParamList("-1", globalDtParam, true);
                    //getParamInfo(globalDtParam, "-1", "-1", true);
                    ddlIgnoreFlag.Enabled = true;
                }
            }
            else if (ModelID != null && ModelID.Length > 0)
            {
                createNavilnks();
                object obj = pDataIO.getDbCmdExecuteScalar("select ShowName from GlobalAllTestModelList where ID=(select GID from topoTestModel where id = " + ModelID + ")");
                if (obj != null)
                {
                    string ShowName = obj.ToString();
                    //if (ShowName.ToString().Contains('_'))
                    //{
                    //    int findFirstChar = ShowName.IndexOf('_');
                    //    ShowName = ShowName.Substring(0, findFirstChar).ToString();
                    //}
                    
                    getInfo("select * from topoTestModel where id=" + ModelID);
                    DataTable globalDtParam =
                        pDataIO.GetDataTable("select * from GlobalTestModelParamterList where PID = (select id from GlobalAllTestModelList where ShowName='"
                            + ShowName + "')", "GlobalTestModelParamterList");

                    getParamList(ModelID, globalDtParam);
                }
                else
                {
                    OptionButtons1.Visible = false;
                    //ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any Model Name string~')</Script>");
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
    
    void disposeParamsTable()
    {
        Control pTitle = plhMain.FindControl("ModelInfo");
        if (pTitle != null)
        {
            plhMain.Controls.Remove(pTitle);
        }
        Control paramTitle = plhMain.FindControl("ModelParamsInfo");
        if (paramTitle != null)
        {
            plhMain.Controls.Remove(paramTitle);
        }
        Control paramLog = plhMain.FindControl("ModelParamsLog");
        if (paramLog != null)
        {
            plhMain.Controls.Remove(paramLog);
        }
        Control pTable = plhMain.FindControl("ModelParamsTable");
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
            DataTable globalDtParam = pDataIO.GetDataTable("select * from GlobalTestModelParamterList where pid=(select id from GlobalAllTestModelList where ShowName='"
                + ddlItemName.Text + "')", "GlobalTestModelParamterList");
            getParamList("-1", globalDtParam, true);
            ddlIgnoreFlag.Enabled = true;
            ddlFailBreak.Enabled = true;
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
        pTable.ID = "ModelInfoTable";
        try
        {
            pReader = pDataIO.GetDataTable(filterStr,"");
            DataTable globalModelDt = pDataIO.GetDataTable("select * from GlobalAllTestModelList", "GlobalAllTestModelList");

            //先缓存当前的Global资料!
            string ShowName = "", ItemDesc = "";
            
            if (pReader.Rows.Count==1)
            {
                DataRow[] drs = globalModelDt.Select("ID=" + pReader.Rows[0]["Gid"]);
                //必须先执行查询到Global的资料后再开始填充数据!
                if (drs.Length == 1)
                {
                    ShowName = drs[0]["ShowName"].ToString();
                    ItemDesc = drs[0]["ItemDescription"].ToString();
                }

                ModelControl = (Frame_TestPlan_ModelInfo)Page.LoadControl("~/Frame/TestPlan/ModelInfo.ascx");

                ModelControl.ID = "ModelList_" + pReader.Rows[0]["ID"].ToString();
                ModelControl.LblItemName = ShowName;
                ModelControl.DdlFailBreak = pReader.Rows[0]["FailBreak"].ToString();
                ModelControl.DdlIgnoreFlag = pReader.Rows[0]["IgnoreFlag"].ToString();
                ModelControl.TxtItemDesc = ItemDesc;
                ModelControl.SetModelInfoEnabled(false);

                TableCell tc = new TableCell();
                tc.Controls.Add(ModelControl);
                TableRow tr = new TableRow();
                tr.Controls.Add(tc);
                pTable.Rows.Add(tr);
            }
            //Control title = pCommCtrl.CreateHtmlGenericControl("h4", "<ModelInfo>");
            //title.ID = "ModelInfo";
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
            pTable.ID = "ModelParamsTable";            
            List<string> pHeaderLst = new List<string>();
            pHeaderLst.Add("");         //因为该用户控件是<td></td>以Table的形式会空一格
            pHeaderLst.Add("名称");            
            pHeaderLst.Add("参数类型");
            pHeaderLst.Add("值");
            pHeaderLst.Add("描述");

            TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pHeaderLst);
            thr.Cells[0].Attributes.Add("style", "width:0px;border:none;background:#ffffff;");
            thr.Cells[1].Attributes.Add("style", "width:180px;font-family:'Times New Roman Negreta', 'Times New Roman';"
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
            ControlList = new Frame_TestPlan_ModelParams[rowCount];

            string filterStr = "select * from TopoTestParameter where pid=" + paramsPID;

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

                    ControlList[i] = (Frame_TestPlan_ModelParams)Page.LoadControl("~/Frame/TestPlan/ModelParams.ascx");
                    string ItemTypeString = ItemType.Replace(" ", "");
                    //ConfigValidator(ItemTypeString.ToUpper().Trim(), ControlList[i]);
                    pReaderTable = pDataIO.GetDataTable(filterStr + " and GID=" + GID, "TopoTestParameter");
                    ControlList[i].SetModelParamsEnableState(isEditState);

                    ControlList[i].ToolTipItemValue = "default:" + ItemValue;
                    if (pReaderTable != null && pReaderTable.Rows.Count == 1)     //因为每次就一条记录故改用if(){}
                    {
                        ControlList[i].ID = "ModelParam_" + pReaderTable.Rows[0]["ID"].ToString();
                        ControlList[i].TxtItemName = ShowName;
                        ControlList[i].TxtItemValue = pReaderTable.Rows[0]["ItemValue"].ToString();
                        ControlList[i].TxtItemType = ItemType;
                        ControlList[i].TxtItemDescription = ItemDesc;
                        ControlList[i].myControlID = "ID_" + GID;
                    }
                    else
                    {
                        ControlList[i].ID = "ModelParam_New_" + i.ToString();
                        ControlList[i].TxtItemName = "[NewParam]" + ShowName;
                        ControlList[i].TxtItemValue = ItemValue;
                        ControlList[i].TxtItemType = ItemType;
                        ControlList[i].TxtItemDescription = ItemDesc;
                        ControlList[i].myControlID = "ID_" + GID;
                    }
                    ConfigValidator(ItemTypeString.ToUpper().Trim(), ControlList[i]);
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

            Image img = new Image();
            img.ImageUrl = "~/Images/List.gif";
            img.ID = "ModelParamsLog";
            img.Attributes.Add("style", "Height:20px;position:relative;padding-top:15px;");
            Label title = new Label();
            title.ID = "ModelParamsInfo";
            title.Text = "模型参数信息";
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
            if (ModelControl != null)
            {
                ModelControl.SetModelInfoEnabled(true);
            }
            if (ControlList != null)
            {
                for (int i = 0; i < ControlList.Length; i++)
                {
                    ControlList[i].SetModelParamsEnableState(true);
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
            DataSet ModelDS = new DataSet();
            string strModelInfo = "select * from topoTestModel where id =" + ModelID;
            string strModelParam = "select * from TopoTestParameter where pid =" + ModelID;
            DataTable ModelDt = pDataIO.GetDataTable(strModelInfo, "topoTestModel");
            DataTable ModelParamsDt = pDataIO.GetDataTable(strModelParam, "TopoTestParameter");

            DataTable GlobalModelDt = pDataIO.GetDataTable("select * from GlobalAllTestModelList", "GlobalAllTestModelList");
            DataTable GlobalModelParamsDt = pDataIO.GetDataTable("select * from GlobalTestModelParamterList", "GlobalTestModelParamterList");

            ModelDS.Tables.Add(ModelDt);
            ModelDS.Tables.Add(ModelParamsDt);
            ModelDS.Relations.Add("ModelRelation", ModelDt.Columns["ID"], ModelParamsDt.Columns["pID"]);

            if (ModelDt.Rows.Count == 1)
            {
                #region //已经获取到当前设备的信息
                string ModelGid = ModelDt.Rows[0]["GID"].ToString();
                ModelDt.Rows[0]["IgnoreFlag"] = ModelControl.DdlIgnoreFlag;
                ModelDt.Rows[0]["FailBreak"] = ModelControl.DdlFailBreak;

                #region Params
                string paramsPid = ModelDt.Rows[0]["ID"].ToString();
                for (int i = 0; i < ControlList.Length; i++)
                {
                    //若为新增的参数则需要Add
                    if (ControlList[i] != null)
                    {
                        string myGid = ControlList[i].myControlID.ToString().Split('_')[1];
                        //string myGid = pCommCtrl.getDTColumnInfo(GlobalModelParamsDt, "ID", "PID=" + ModelGid + " and ItemName ='" + ControlList[i].TxtItemName.Replace("[NewParam]", "") + "'");
                        if (ControlList[i].ID.ToString().Contains("ModelParam_New_"))
                        {
                            DataRow dr = ModelParamsDt.NewRow();
                            dr["pid"] = paramsPid;
                            dr["Gid"] = myGid;
                            dr["ItemValue"] = ControlList[i].TxtItemValue;
                            ModelParamsDt.Rows.Add(dr);
                        }
                        else
                        {
                            for (int j = 0; j < ModelParamsDt.Rows.Count; j++)
                            {
                                if (ModelParamsDt.Rows[j]["GID"].ToString().ToLower().Trim() == myGid)
                                {
                                    ModelParamsDt.Rows[j]["ItemValue"] = ControlList[i].TxtItemValue;
                                }
                            }
                        }
                    }
                }
                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("topoTestModel", ModelDt, ModelParamsDt, logTracingString);
                if (result > 0)
                {
                    ModelDt.AcceptChanges();
                    ModelParamsDt.AcceptChanges();
                    Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ModelList.aspx?uId=" + CtrlID, false);
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
                string ModelGid = pCommCtrl.getDTColumnInfo(GlobalModelDt, "ID", "ShowName='" + ddlItemName.Text + "'");
                string MaxSeq = pDataIO.getDbCmdExecuteScalar("select max(SEQ) from topoTestModel where PID =" + CtrlID).ToString();
                long currModelID = pDataIO.GetLastInsertData("topoTestModel") + 1;
                long currModelParamID = pDataIO.GetLastInsertData("TopoTestParameter") + 1;
                int myNewSeq = 1;
                if (MaxSeq.Length > 0)
                {
                    myNewSeq = Convert.ToInt32(MaxSeq) + 1;
                }
                DataRow ModelNewDr = ModelDt.NewRow();
                ModelNewDr["ID"] = currModelID;
                ModelNewDr["PID"] = CtrlID;
                ModelNewDr["GID"] = ModelGid;
                ModelNewDr["SEQ"] = myNewSeq;
                ModelNewDr["IgnoreFlag"] = ddlIgnoreFlag.Text;
                ModelNewDr["FailBreak"] = ddlFailBreak.Text;
                ModelDt.Rows.Add(ModelNewDr);
                //-->Create NewEqParamsDt rows
                for (int i = 0; i < ControlList.Length; i++)
                {
                    string myGid = ControlList[i].myControlID.ToString().Split('_')[1];
                    //string myGid = pCommCtrl.getDTColumnInfo(GlobalModelParamsDt, "ID", "PID=" + ModelGid + " and ItemName ='" + ControlList[i].TxtItemName.Replace("[NewParam]", "") + "'");
                    DataRow dr = ModelParamsDt.NewRow();
                    dr["id"] = currModelParamID;
                    dr["pid"] = currModelID;
                    dr["Gid"] = myGid;
                    dr["ItemValue"] = ControlList[i].TxtItemValue;
                    ModelParamsDt.Rows.Add(dr);
                    currModelParamID++;
                }

                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("topoTestModel", ModelDt, ModelParamsDt, logTracingString);
                if (result > 0)
                {
                    ModelDt.AcceptChanges();
                    ModelParamsDt.AcceptChanges();
                    Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ModelList.aspx?uId=" + CtrlID, false);
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
                ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('请选择测试模型!')</Script>");
            }
            else
            {
                //ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any Result,Current item has been deleted!~')</Script>");
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
                Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ModelList.aspx?uId=" + CtrlID, false);
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
        bool editVidible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);

        if (editVidible)
        {
            OptionButtons1.ConfigBtEditVisible=true;
        } 
        else
        {
            OptionButtons1.ConfigBtEditVisible=GetTestPlanAuthority();
        }
        
        OptionButtons1.ConfigBtDeleteVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    public void ConfigValidator(string inputType, Frame_TestPlan_ModelParams inputControl)
    {
        try
        {
            switch (inputType)
            {
                case "ArrayList":
                case "ARRAYLIST":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItem = true;
                        inputControl.ConfigRegularExpressionValidatorItemVExpression = validExpre.GFloatIntandCommaExpression;
                        inputControl.ConfigRegularExpressionValidatorItemErrMessage = "请输入整数或单精度浮点数，并用逗号隔开";
                        break;
                    }
                case "byte":
                case "BYTE":
                    {
                        inputControl.EnableRangeValidatorItemValue = true;
                        inputControl.EnableRegularExpressionValidatorItem = false;
                        inputControl.ConfigRangeValidatorItemValueType = ValidationDataType.Integer;
                        inputControl.ConfigRangeValidatorItemValueMax = Convert.ToString(255);
                        inputControl.ConfigRangeValidatorItemValueMin = Convert.ToString(0);

                        break;
                    }
                case "UInt16":
                case "UINT16":
                    {
                        inputControl.EnableRangeValidatorItemValue = true;
                        inputControl.EnableRegularExpressionValidatorItem = false;
                        inputControl.ConfigRangeValidatorItemValueType = ValidationDataType.Integer;
                        inputControl.ConfigRangeValidatorItemValueMax = Convert.ToString(65535);
                        inputControl.ConfigRangeValidatorItemValueMin = Convert.ToString(0);

                        break;
                    }
                case "double":
                case "DOUBLE":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItem = true;
                        inputControl.ConfigRegularExpressionValidatorItemVExpression = validExpre.GDoubleorScientificNotation;
                        inputControl.ConfigRegularExpressionValidatorItemErrMessage = "请输入双精度浮点数，可用科学计数法表示";

                        break;
                    }
                case "Bool":
                case "BOOL":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItem = true;
                        inputControl.ConfigRegularExpressionValidatorItemVExpression = validExpre.GBoolExpression;
                        inputControl.ConfigRegularExpressionValidatorItemErrMessage = "请输入0或false、1或true";
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

                {
                    DataTable planIDTable = pDataIO.GetDataTable("select PID from TopoTestControl where ID=" + CtrlID, "TopoTestControl");
                    string planID = planIDTable.Rows[0]["PID"].ToString().Trim();

                    DataTable pnId = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + planID, "TopoTestPlan");

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

                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + planID, "UserPlanAction");

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