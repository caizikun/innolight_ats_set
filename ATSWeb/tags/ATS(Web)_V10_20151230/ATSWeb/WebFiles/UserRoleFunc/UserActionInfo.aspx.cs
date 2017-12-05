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

public partial class WebFiles_UserRoleFunc_UserActionInfo : BasePage
{
    const string funcItemName = "UserActionInfo";

    ASCXPNActionList[] PNActionList;
    ASCXPNActionList PNActionTitle;

    DataIO pDataIO;
    DataTable PNAction;
    DataTable PlanAction;

    int userID;
    int ActionType;  
    int PNID;
    
    private string logTracingString = "";

    public WebFiles_UserRoleFunc_UserActionInfo()
    {
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        SetSessionBlockType(8);
        if (Request.QueryString["uId"] != null)
        {
            userID = Convert.ToInt32(Request.QueryString["uId"]);
        }
        ActionType = Convert.ToInt32(Request.QueryString["uActionType"]);

        initPageInfo();
    }

    void initPageInfo()
    {
        try
        {
            if (ActionType == 1)
            {
                PNActionTitle = (ASCXPNActionList)Page.LoadControl("../../Frame/UserRoleFunc/PNActionList.ascx");
            }
            else if (ActionType == 2)
            {
                PNActionTitle = (ASCXPNActionList)Page.LoadControl("../../Frame/UserRoleFunc/PNActionList.ascx");
                PNActionTitle.Column4Visible(false);
                Label2.Text = "[AddTestPlan权限]";
            }
            else if (ActionType == 3)
            {
                PNActionTitle = (ASCXPNActionList)Page.LoadControl("../../Frame/UserRoleFunc/PNActionList.ascx");
                PNActionTitle.Column1Visible(false);
                PNActionTitle.Column3Visible(false);

                Label3.ForeColor = System.Drawing.Color.White;
                GridView1.Visible = false;
            }

            PNActionTitle.ContentTRVisible = false;
            this.plhMain.Controls.Add(PNActionTitle);


            if (pDataIO.OpenDatabase(true))
            {
                if (!IsPostBack)
                {
                    if (ActionType != 3)
                    {
                        GridView1.DataSource = PlanAction;
                        GridView1.DataBind();
                    }

                    Session["LastType"] = null;
                    Session["lastPNID"] = null;
                    Session["SaveButtonVisible"] = null;
                    ViewState["allPN"] = null;
                    ViewState["AllPlan"] = null;

                    ConfigOptionButtonsVisible();
                    OptionButtons1.ConfigBtEditVisible = false;

                    DataTable dt = new DataTable();
                    dt = pDataIO.GetDataTable("select ItemName from GlobalProductionType where IgnoreFlag=0", "");

                    DropDownListType.Items.Clear();
                    DropDownListType.Items.Add(" ");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownListType.Items.Add(dt.Rows[i]["ItemName"].ToString());
                    }

                }
                else
                {

                    if (Convert.ToBoolean(Session["SaveButtonVisible"]) == true)
                    {
                        PNAction = ViewState["allPN"] as DataTable;
                    }
                    else 
                    {
                        GetPNData();                         
                    }

                    if (PNAction.Rows.Count > 0)
                    {
                        bindPNData();
                    }     
                    
                }

                string userName = pDataIO.getDbCmdExecuteScalar("select LoginName from UserInfo where id = " + userID).ToString();
                CommCtrl pCommCtrl = new CommCtrl();
                Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, userName, Session["BlockType"].ToString(), pDataIO, out logTracingString);
                this.plhNavi.Controls.Add(myCtrl);
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    protected void DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(Session["SaveButtonVisible"]) == true)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('请先保存当前权限信息！');", true);
                DropDownListType.Text = Session["LastType"].ToString();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (Session["lastPNID"] != null)
                    {
                        if ((GridView1.Rows[i].FindControl("PNIDLabel") as Label).Text == Session["lastPNID"].ToString())
                        {
                            CheckBox cbEdit = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxEdit");
                            CheckBox cbDelete = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxDelete");
                            CheckBox cbRun = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxRun");

                            if (cbEdit.Checked == true)
                            {
                                PlanAction.Rows[i]["Edit"] = "true";
                            }
                            else
                            {
                                PlanAction.Rows[i]["Edit"] = "false";
                            }

                            if (cbDelete.Checked == true)
                            {
                                PlanAction.Rows[i]["Delete"] = "true";
                            }
                            else
                            {
                                PlanAction.Rows[i]["Delete"] = "false";
                            }

                            if (cbRun.Checked == true)
                            {
                                PlanAction.Rows[i]["Run"] = "true";
                            }
                            else
                            {
                                PlanAction.Rows[i]["Run"] = "false";
                            }
                        }                   
                    }
                }

                bindPlanData();
            }
            else
            {
                for (int i = 0; i < PNAction.Rows.Count; i++)
                {
                    if (PNAction.Rows[i]["AddPlan"].ToString().ToLower() == "true")
                    {
                        PNActionList[i].CheckBoxAddPlanSelected = true;
                    }
                    else
                    {
                        PNActionList[i].CheckBoxAddPlanSelected = false;
                    }

                    if (PNAction.Rows[i]["ModifyPN"].ToString().ToLower() == "true")
                    {
                        PNActionList[i].CheckBoxEditSelected = true;
                    }
                    else
                    {
                        PNActionList[i].CheckBoxEditSelected = false;
                    }
                }

                Session["lastPNID"] = null;

                if (PlanAction != null)
                {
                    for (int i = 0; i < PlanAction.Rows.Count; )
                    {
                        PlanAction.Rows.RemoveAt(i);
                    }
                }

                GridView1.DataSource = PlanAction;
                GridView1.DataBind();

                GetAllPlan();

                if (PNActionList != null)
                {
                    for (int i = 0; i < PNActionList.Length; i++)
                    {
                        PNActionList[i].RadioButtonChecked = false;
                    }

                    OptionButtons1.ConfigBtEditVisible = true;
                }
                else
                {
                    OptionButtons1.ConfigBtEditVisible = false;
                }

                Session["LastType"] = DropDownListType.SelectedItem.Text;
            }       
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public void GetPNData()
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        string sql;

        sql = "select GlobalProductionName.ID,GlobalProductionName.PN from GlobalProductionName,GlobalProductionType "
            + "where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.IgnoreFlag=0 and GlobalProductionType.ItemName='" + DropDownListType.SelectedItem.Text + "' order by ID";
        dt1 = pDataIO.GetDataTable(sql, "");

        sql = "select UserPNAction.ID as ActionID,UserPNAction.PNID,UserPNAction.AddPlan,UserPNAction.ModifyPN from UserPNAction,UserInfo,GlobalProductionName where UserPNAction.UserID=UserInfo.ID and UserPNAction.PNID=GlobalProductionName.ID "
          + "and UserPNAction.UserID=" + userID + " order by PNID";

        dt2 = pDataIO.GetDataTable(sql, "");
        
        dt1.Columns.Add("AddPlan");
        dt1.Columns.Add("ModifyPN");
        dt1.Columns.Add("ActionID");

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (dt2.Rows.Count == 0)
            {
                dt1.Rows[i]["AddPlan"] = 0;
                dt1.Rows[i]["ModifyPN"] = 0;
                dt1.Rows[i]["ActionID"] = -1;
            }
            else
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt1.Rows[i]["ID"].ToString() == dt2.Rows[j]["PNID"].ToString())
                    {
                        dt1.Rows[i]["AddPlan"] = dt2.Rows[j]["AddPlan"];
                        dt1.Rows[i]["ModifyPN"] = dt2.Rows[j]["ModifyPN"];
                        dt1.Rows[i]["ActionID"] = dt2.Rows[j]["ActionID"];
                        break;
                    }
                    else
                    {
                        dt1.Rows[i]["AddPlan"] = 0;
                        dt1.Rows[i]["ModifyPN"] = 0;
                        dt1.Rows[i]["ActionID"] = -1;
                    }
                }           
            }
        }

        PNAction = dt1;
        ViewState["allPN"] = PNAction;
    }


    public void bindPNData()
    {
        PNActionList = new ASCXPNActionList[PNAction.Rows.Count];

        for (int i=0; i < PNAction.Rows.Count; i++)
        {
            PNActionList[i] = (ASCXPNActionList)Page.LoadControl("../../Frame/UserRoleFunc/PNActionList.ascx");
            PNActionList[i].ConfigLabelPNText = PNAction.Rows[i]["PN"].ToString();
            PNActionList[i].ConfigLabelActionID = PNAction.Rows[i]["ActionID"].ToString();
            //PNActionList[i].ConfigControlID = PNAction.Rows[i]["ID"].ToString();

            if (PNAction.Rows[i]["AddPlan"].ToString().ToLower() == "true" )
            {
                PNActionList[i].CheckBoxAddPlanSelected = true;
            }
            else
            {
                PNActionList[i].CheckBoxAddPlanSelected = false;
            }

            if (PNAction.Rows[i]["ModifyPN"].ToString().ToLower() == "true")
            {
                PNActionList[i].CheckBoxEditSelected = true;
            }
            else
            {
                PNActionList[i].CheckBoxEditSelected = false;
            }

            if (ActionType == 2)
            {
                PNActionList[i].Column4Visible(false);
            }
            else if (ActionType == 3)
            {
                PNActionList[i].Column1Visible(false);
                PNActionList[i].Column3Visible(false);
            }

            PNActionList[i].RadioButtonName = "ChoosePN";
            PNActionList[i].LBTHTitleVisible(false);           
            this.plhMain.Controls.Add(PNActionList[i]);
        }     
    }

    public void GetAllPlan()
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        string sql;

        sql = "select GlobalProductionName.ID as PNID,TopoTestPlan.ID as PlanID,TopoTestPlan.ItemName as TestPlan from TopoTestPlan,GlobalProductionName,GlobalProductionType "
                   + "where GlobalProductionName.PID=GlobalProductionType.ID and TopoTestPlan.PID=GlobalProductionName.ID "
                   + "and GlobalProductionType.IgnoreFlag=0 and GlobalProductionName.IgnoreFlag=0 and TopoTestPlan.IgnoreFlag=0 and GlobalProductionType.ItemName='" + DropDownListType.SelectedItem.Text + "' order by PNID";
        dt1 = pDataIO.GetDataTable(sql, "");   //TopoTestPlan.IgnoreFlag=0

        sql = "select UserPlanAction.ID as ActionID,UserPlanAction.PlanID,UserPlanAction.ModifyPlan,UserPlanAction.DeletePlan,UserPlanAction.RunPlan from UserPlanAction,UserInfo,TopoTestPlan where UserPlanAction.UserID=UserInfo.ID and UserPlanAction.PlanID=TopoTestPlan.ID "
            + "and UserPlanAction.UserID=" + userID + " order by PlanID";

        dt2 = pDataIO.GetDataTable(sql, "");

        dt1.Columns.Add("Edit");
        dt1.Columns.Add("Delete");
        dt1.Columns.Add("Run");
        dt1.Columns.Add("ActionID");

        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (dt2.Rows.Count == 0)
            {
                dt1.Rows[i]["Edit"] = 0;
                dt1.Rows[i]["Delete"] = 0;
                dt1.Rows[i]["Run"] = 0;
                dt1.Rows[i]["ActionID"] = -1;
            }
            else
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt1.Rows[i]["PlanID"].ToString() == dt2.Rows[j]["PlanID"].ToString())
                    {
                        dt1.Rows[i]["Edit"] = dt2.Rows[j]["ModifyPlan"];
                        dt1.Rows[i]["Delete"] = dt2.Rows[j]["DeletePlan"];
                        dt1.Rows[i]["Run"] = dt2.Rows[j]["RunPlan"];
                        dt1.Rows[i]["ActionID"] = dt2.Rows[j]["ActionID"];
                        break;
                    }
                    else
                    {
                        dt1.Rows[i]["Edit"] = 0;
                        dt1.Rows[i]["Delete"] = 0;
                        dt1.Rows[i]["Run"] = 0;
                        dt1.Rows[i]["ActionID"] = -1;
                    }
                }            
            }
        }

        if (dt1.Rows.Count > 0)
        {
            ViewState["AllPlan"] = dt1;                     
        }

              
    }

    int currPNID;

    public void PNCheckChanged(object obj, string prameter)
    {
        if (Convert.ToBoolean(Session["SaveButtonVisible"]) == true)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (Session["lastPNID"] != null)
                {
                    if ((GridView1.Rows[i].FindControl("PNIDLabel") as Label).Text == Session["lastPNID"].ToString())
                    {
                        CheckBox cbEdit = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxEdit");
                        CheckBox cbDelete = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxDelete");
                        CheckBox cbRun = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxRun");

                        if (cbEdit.Checked == true)
                        {
                            PlanAction.Rows[i]["Edit"] = "true";
                        }
                        else
                        {
                            PlanAction.Rows[i]["Edit"] = "false";
                        }

                        if (cbDelete.Checked == true)
                        {
                            PlanAction.Rows[i]["Delete"] = "true";
                        }
                        else
                        {
                            PlanAction.Rows[i]["Delete"] = "false";
                        }

                        if (cbRun.Checked == true)
                        {
                            PlanAction.Rows[i]["Run"] = "true";
                        }
                        else
                        {
                            PlanAction.Rows[i]["Run"] = "false";
                        }

                    }                
                }
            }
        }
                   
        if (Session["lastPNID"] == null)
        {
            for (int i = 0; i < PNActionList.Length; i++)
            {
                if (PNActionList[i].RadioButtonChecked == true)
                {
                    PNID = Convert.ToInt32(PNAction.Rows[i]["ID"]);
                    Session["lastPNID"] = Convert.ToInt32(PNAction.Rows[i]["ID"]);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < PNActionList.Length; i++)
            {
                if (PNActionList[i].RadioButtonChecked == true)
                {
                    currPNID = Convert.ToInt32(PNAction.Rows[i]["ID"]);
                    if (currPNID < Convert.ToInt32(Session["lastPNID"]))
                    {
                        PNID = Convert.ToInt32(PNAction.Rows[i]["ID"]);
                    }
                    else if (currPNID == Convert.ToInt32(Session["lastPNID"]))
                    {
                        PNActionList[i].RadioButtonChecked = false;
                    }
                    else
                    {
                        PNID = Convert.ToInt32(PNAction.Rows[i]["ID"]);
                    }
                }
            }

            Session["lastPNID"] = PNID;
        }

        bindPlanData();
                   
    }

    public void bindPlanData()   //获取当前显示数据
    {
        try
        {
            int PN; 
            PN = Convert.ToInt32(Session["lastPNID"]);

            PlanAction = ViewState["AllPlan"] as DataTable;
          
            if (ActionType != 3)
            {
                GridView1.DataSource = PlanAction;
                GridView1.DataBind();
            }
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }
      
    public bool EditData(object obj, string prameter)
    {
        try
        {
            if (PNActionList != null)
            {
                ConfigOptionButtonsVisible(true);
                Session["SaveButtonVisible"] = true;

                for (int i = 0; i < PNActionList.Length; i++)
                {
                    PNActionList[i].RadioEnable(true);
                }
            }

            if (Session["lastPNID"] != null)
            {
                bindPlanData();
            }
          
            for (int i=0; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Cells[3].Enabled = true;
                GridView1.Rows[i].Cells[4].Enabled = true;
                GridView1.Rows[i].Cells[5].Enabled = true;
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
            DataTable dtPN = new DataTable();
            DataTable dtPlan = new DataTable();

#region Save PNAction
            for (int i = 0; i < PNActionList.Length; i++)
            {
                if (PNActionList[i].CheckBoxAddPlanSelected == true)
                {
                    PNAction.Rows[i]["AddPlan"] = "true";
                }
                else
                {
                    PNAction.Rows[i]["AddPlan"] = "false";
                }

                if (PNActionList[i].CheckBoxEditSelected == true)
                {
                    PNAction.Rows[i]["ModifyPN"] = "true";
                }
                else
                {
                    PNAction.Rows[i]["ModifyPN"] = "false";
                }
            }

            string updataStr = "select * from UserPNAction where UserID=" + userID;
             
            dtPN = pDataIO.GetDataTable(updataStr, "UserPNAction");            

            for (int i = 0; i < PNAction.Rows.Count; i++)
            {
                DataRow[] drs = dtPN.Select("PNID=" + PNAction.Rows[i]["ID"]);
                if (drs.Length ==1)
                {
                    if (PNAction.Rows[i]["AddPlan"].ToString() == "true" | PNAction.Rows[i]["ModifyPN"].ToString() == "true")
                    {
                        if (PNAction.Rows[i]["AddPlan", DataRowVersion.Original].ToString() != PNAction.Rows[i]["AddPlan", DataRowVersion.Current].ToString())
                        {
                            drs[0]["AddPlan"] = PNAction.Rows[i]["AddPlan"];
                        }
                        if (PNAction.Rows[i]["ModifyPN", DataRowVersion.Original].ToString() != PNAction.Rows[i]["ModifyPN", DataRowVersion.Current].ToString())
                        {
                            drs[0]["ModifyPN"] = PNAction.Rows[i]["ModifyPN"];
                        }
                    }
                    else
                    {
                        drs[0].Delete();
                    }
                }
                else
                {
                    if (PNAction.Rows[i]["AddPlan"].ToString() == "true" | PNAction.Rows[i]["ModifyPN"].ToString() == "true")
                    {
                        DataRow dr = dtPN.NewRow();
                        dr["ID"] = PNAction.Rows[i]["ActionID"];
                        dr["UserID"] = userID;
                        dr["PNID"] = PNAction.Rows[i]["ID"];
                        dr["AddPlan"] = PNAction.Rows[i]["AddPlan"];
                        dr["ModifyPN"] = PNAction.Rows[i]["ModifyPN"];
                        dtPN.Rows.Add(dr);
                    }
                }
            }

            int result;

            if (dtPN.Rows.Count > 0)
            {
                result = pDataIO.UpdateWithProc("UserPNAction", dtPN, updataStr, logTracingString);
                if (result > 0)
                {
                    dtPN.AcceptChanges();
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!");
                }            
            }

            for (int i = 0; i < PNActionList.Length; i++)
            {
                PNActionList[i].RadioEnable(false);
            }
#endregion

#region Save PlanAction
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cbEdit = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxEdit");
                CheckBox cbDelete = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxDelete");
                CheckBox cbRun = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxRun");

                if (cbEdit.Checked == true)
                {
                    PlanAction.Rows[i]["Edit"] = "true";
                }
                else
                {
                    PlanAction.Rows[i]["Edit"] = "false";
                }

                if (cbDelete.Checked == true)
                {
                    PlanAction.Rows[i]["Delete"] = "true";
                }
                else
                {
                    PlanAction.Rows[i]["Delete"] = "false";
                }

                if (cbRun.Checked == true)
                {
                    PlanAction.Rows[i]["Run"] = "true";
                }
                else
                {
                    PlanAction.Rows[i]["Run"] = "false";
                }
            }

            if (GridView1.Rows.Count>0)
            {
                updataStr = "select * from UserPlanAction where UserID=" + userID;

                dtPlan = pDataIO.GetDataTable(updataStr, "UserPlanAction");

                for (int i = 0; i < PlanAction.Rows.Count; i++)
                {
                    DataRow[] drs = dtPlan.Select("PlanID=" + PlanAction.Rows[i]["PlanID"]);
                    if (drs.Length == 1)
                    {
                        if (PlanAction.Rows[i]["Edit"].ToString() == "true" | PlanAction.Rows[i]["Delete"].ToString() == "true" | PlanAction.Rows[i]["Run"].ToString() == "true")
                        {
                            if (PlanAction.Rows[i]["Edit", DataRowVersion.Original].ToString() != PlanAction.Rows[i]["Edit", DataRowVersion.Current].ToString())
                            {
                                drs[0]["ModifyPlan"] = PlanAction.Rows[i]["Edit"];
                            }
                            if (PlanAction.Rows[i]["Delete", DataRowVersion.Original].ToString() != PlanAction.Rows[i]["Delete", DataRowVersion.Current].ToString())
                            {
                                drs[0]["DeletePlan"] = PlanAction.Rows[i]["Delete"];
                            }
                            if (PlanAction.Rows[i]["Run", DataRowVersion.Original].ToString() != PlanAction.Rows[i]["Run", DataRowVersion.Current].ToString())
                            {
                                drs[0]["RunPlan"] = PlanAction.Rows[i]["Run"];
                            }
                        }
                        else
                        {
                            drs[0].Delete();
                        }
                    }
                    else
                    {
                        if (PlanAction.Rows[i]["Edit"].ToString() == "true" | PlanAction.Rows[i]["Delete"].ToString() == "true" | PlanAction.Rows[i]["Run"].ToString() == "true")
                        {
                            DataRow dr = dtPlan.NewRow();
                            dr["ID"] = PlanAction.Rows[i]["ActionID"];
                            dr["UserID"] = userID;
                            dr["PlanID"] = PlanAction.Rows[i]["PlanID"];
                            dr["ModifyPlan"] = PlanAction.Rows[i]["Edit"];
                            dr["DeletePlan"] = PlanAction.Rows[i]["Delete"];
                            dr["RunPlan"] = PlanAction.Rows[i]["Run"];
                            dtPlan.Rows.Add(dr);
                        }
                    }
                }

                if (dtPlan.Rows.Count > 0)
                {
                    result = pDataIO.UpdateWithProc("UserPlanAction", dtPlan, updataStr, logTracingString);
                    if (result > 0)
                    {
                        dtPlan.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("Update data fail!");
                    }               
                }

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridView1.Rows[i].Cells[3].Enabled = false;
                    GridView1.Rows[i].Cells[4].Enabled = false;
                    GridView1.Rows[i].Cells[5].Enabled = false;
                }            
            }
#endregion
            ConfigOptionButtonsVisible(false);          

            Session["SaveButtonVisible"] = false;

            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;

        }
    }

    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            Session["LastType"] = null;
            Session["lastPNID"] = null;
            Session["SaveButtonVisible"] = null;
            ViewState["allPN"] = null;
            ViewState["AllPlan"] = null;

            Response.Redirect(Request.Url.ToString(), true);
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public void ConfigOptionButtonsVisible(bool isSaveMode = false)
    {
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;

        OptionButtons1.ConfigBtSaveVisible = isSaveMode;
        OptionButtons1.ConfigBtEditVisible = !isSaveMode;
        OptionButtons1.ConfigBtCancelVisible = isSaveMode;
    }
  
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["AllPlan"]!=null)
        {
           PlanAction = ViewState["AllPlan"] as DataTable;        
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex;

            CheckBox CheckBoxEdit = (CheckBox)e.Row.FindControl("CheckBoxEdit");         
            CheckBox CheckBoxDelete = (CheckBox)e.Row.FindControl("CheckBoxDelete");
            CheckBox CheckBoxRun = (CheckBox)e.Row.FindControl("CheckBoxRun");
           
            Label PNIDLabel = (Label)e.Row.FindControl("PNIDLabel");
            Label PlanIDLabel = (Label)e.Row.FindControl("PlanIDLabel");
            Label ActionIDLabel = (Label)e.Row.FindControl("ActionIDLabel");

            PNIDLabel.Text = PlanAction.Rows[i]["PNID"].ToString ();
            PlanIDLabel.Text = PlanAction.Rows[i]["PlanID"].ToString();
            ActionIDLabel.Text = PlanAction.Rows[i]["ActionID"].ToString();

            if (PlanAction.Rows[i]["Edit"].ToString().ToLower() == "true")
            {
                CheckBoxEdit.Checked = true;
            }
            else
            {
                CheckBoxEdit.Checked = false;
            }

            if (PlanAction.Rows[i]["Delete"].ToString().ToLower() == "true")
            {
                CheckBoxDelete.Checked = true;
            }
            else
            {
                CheckBoxDelete.Checked = false;
            }

            if (PlanAction.Rows[i]["Run"].ToString().ToLower() == "true")
            {
                CheckBoxRun.Checked = true;
            }
            else
            {
                CheckBoxRun.Checked = false;
            }


            if (Convert.ToBoolean(Session["SaveButtonVisible"]) == true)
            {
                CheckBoxEdit.Enabled = true;
                CheckBoxDelete.Enabled = true;
                CheckBoxRun.Enabled = true;
            }
            else
            {
                CheckBoxEdit.Enabled = false;
                CheckBoxDelete.Enabled = false;
                CheckBoxRun.Enabled = false;
            }

            if (Convert.ToInt32 ( PlanAction.Rows[i]["PNID"]) != Convert .ToInt32 ( Session["lastPNID"]) | Session["lastPNID"] == null)
            {
                e.Row.Visible = false;
            }
            else
            {
                e.Row.Visible = true;
            }
        }
    }
}