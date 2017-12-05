using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ATSDataBase;
using System.Threading;

public partial class ASPXTestPlanList : BasePage
{
    string funcItemName = "测试方案";
    public DataTable mydt = new DataTable();
    private ASCXTestPlanList[] testTestplanList;
    private int rowCount;
    private string moduleTypeID = "";
    private string channelNumber = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    private TestPlanAuthoriy[] testplanAuthority;
    private bool deleteFunctionAuthority = false;
    private string currentTime;
    
    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        conn = "inpcsz0518\\ATS_HOME";
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
        mydt.Clear();

        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(1);
        if (IsPostBack)
        {
            Session["IsAlert"] = "null";
        }
        else
        {
            Session["IsAlert"] = "yes";
        }
        
        {
            moduleTypeID = Request["uId"];
            string temp = Convert.ToString(Session["ChannelNumber"]);
            if (Session["ChannelNumber"] == null)
            {
                channelNumber = "0";
            }
            else
            {
                channelNumber = Convert.ToString(Session["ChannelNumber"]);
            }

            currentTime = System.DateTime.Now.ToString("yyyyMMdd");

            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }
        
    }

    public void bindData()
    {        
        ClearCurrenPage();
        if (rowCount==0)
        {
            testTestplanList = new ASCXTestPlanList[1];
            for (byte i = 0; i < testTestplanList.Length; i++)
            {
                testTestplanList[i] = (ASCXTestPlanList)Page.LoadControl("~/Frame/TestPlan/TestPlanList.ascx");
                testTestplanList[i].LbTH1Text = "名称";
                testTestplanList[i].LbTH2Text = "软件版本号";
                testTestplanList[i].LbTH3Text = "硬件版本号";
                testTestplanList[i].LbTH4Text = "描述";
                testTestplanList[i].LbTHVersionText = "修订号";
                testTestplanList[i].LbTH5Text = "是否在线";
                testTestplanList[i].ContentTRVisible = false;                
                this.TestPlanList.Controls.Add(testTestplanList[i]);
            }
        } 
        else
        {
            string sql = "select A1.PID,A1.IP from TopoRunRecordTable A1,TopoTestPlan where A1.PID=TopoTestPlan.ID "
            + "and StartTime > '" + currentTime + "' and EndTime ='20000101 08:00:00' and Remark like '正常' and TopoTestPlan.PID =" + moduleTypeID
            + " and A1.StartTime=(select MAX(StartTime) from TopoRunRecordTable A2 WHERE A1.IP =A2.IP)"
            + " order by A1.PID,A1.IP";
            DataTable planID = pDataIO.GetDataTable(sql, "TopoRunRecordTable");

            testTestplanList = new ASCXTestPlanList[rowCount];
            testplanAuthority = new TestPlanAuthoriy[rowCount];
            for (byte i = 0; i < testTestplanList.Length; i++)
            {
                string lastIP = "";
                bool onlineFlag = false;

                testTestplanList[i] = (ASCXTestPlanList)Page.LoadControl("~/Frame/TestPlan/TestPlanList.ascx");
                testTestplanList[i].IsSelectedID = "CHECK" + mydt.Rows[i]["ID"].ToString().Trim();
                
                testTestplanList[i].TestplanSelfText = mydt.Rows[i]["ItemName"].ToString().Trim();
                testTestplanList[i].LabelSWVerText = mydt.Rows[i]["SWVersion"].ToString().Trim();
                testTestplanList[i].LabelHWVerText = mydt.Rows[i]["HwVersion"].ToString().Trim();
                testTestplanList[i].LabelDescription = mydt.Rows[i]["ItemDescription"].ToString().Trim();            
                testTestplanList[i].ConfigLabelVersion = mydt.Rows[i]["Version"].ToString().Trim();
                testTestplanList[i].TestplanSelfID = mydt.Rows[i]["ID"].ToString().Trim();
                testTestplanList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                testplanAuthority[i].planid = testTestplanList[i].ID;
                testTestplanList[i].LbTH1Text = "名称";
                testTestplanList[i].LbTH2Text = "软件版本号";
                testTestplanList[i].LbTH3Text = "硬件版本号";
                testTestplanList[i].LbTH4Text = "描述";
                testTestplanList[i].LbTHVersionText = "修订号";
                testTestplanList[i].LbTH5Text = "是否在线";

                if (planID.Rows.Count == 0)
                {
                    testTestplanList[i].ConfigOnlineImageVisible = false;
                    testTestplanList[i].ConfigOnlineLabelVisible = true;
                }
                else
                {
                    for (int j = 0; j < planID.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(planID.Rows[j]["PID"]) == Convert.ToInt32(mydt.Rows[i]["ID"]))
                        {
                            onlineFlag = true;                                                     
                            if (lastIP != planID.Rows[j]["IP"].ToString())
                            {
                                testTestplanList[i].ConfigOnlineImageToolTip += planID.Rows[j]["IP"].ToString() + "；";
                                lastIP = planID.Rows[j]["IP"].ToString();
                            }                            
                        }
                    }

                    if (onlineFlag == true)
                    {
                        testTestplanList[i].ConfigOnlineImageVisible = true;
                        testTestplanList[i].ConfigOnlineLabelVisible = false;
                    }
                    else
                    {
                        testTestplanList[i].ConfigOnlineImageVisible = false;
                        testTestplanList[i].ConfigOnlineLabelVisible = true;
                    }
                }

                testTestplanList[i].BeSelected = false;               
                if (i >= 1)
                {
                    testTestplanList[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                       testTestplanList[i].TrBackgroundColor = "#F2F2F2";
                    }
                }
                //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
                this.TestPlanList.Controls.Add(testTestplanList[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoTestPlan where IgnoreFlag='False'and PID=" + moduleTypeID, "TopoTestPlan");
                rowCount = mydt.Rows.Count;
                bindData();

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + moduleTypeID).ToString();
                string uid = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + moduleTypeID).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + uid;

                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim();

                string parentItem = pDataIO.getDbCmdExecuteScalar("select PN from GlobalProductionName where id = " + moduleTypeID).ToString();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
                this.plhNavi.Controls.Add(myCtrl);
            }
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

   
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/AddNewTestPlan.aspx?uId=" + moduleTypeID.Trim());            
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    public bool CopyData(object obj, string prameter)
    {
        bool isSelected = false;
        int selectCount = 0;
        string copySourceID = "";
        try
        {
            for (int i = 0; i < testTestplanList.Length; i++)
            {
                ASCXTestPlanList cb = (ASCXTestPlanList)TestPlanList.FindControl(testTestplanList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        selectCount++;
                        isSelected = true;
                        copySourceID = cb.TestplanSelfID;
                    }
                }
                else
                {
                    Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false||selectCount>1)
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请至少选择一个！');return false;</script>");                
                this.Page.RegisterStartupScript("", "<script>alert('只能选择一个！');</script>");
                //Response.Write("<script>alert('请至少选择一个！');</script>");
                return false;
            }

            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/CopyTestPlan.aspx?uId=" + moduleTypeID.Trim() + "&sourceID=" + copySourceID);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    public bool DeleteData(object obj, string prameter)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('Are you sure you want to delete？')){DeleteData1();}else{}</script>");
        bool isSelected = false;
        bool deleteAction = false;
        string noAuthorityIDStr = "";
        string deletStr = "select * from TopoTestPlan where IgnoreFlag='False'and PID=" + moduleTypeID;
        try
        {           
            for (int i = 0; i < testTestplanList.Length; i++)
            {
                ASCXTestPlanList cb = (ASCXTestPlanList)TestPlanList.FindControl(testTestplanList[i].ID);
                if (cb != null )
                {
                    if (cb.BeSelected == true)
                    {
                        //mydt.Rows[i].Delete();
                        if (deleteFunctionAuthority)
                        {
                            mydt.Rows[i]["IgnoreFlag"] = true;
                            isSelected = true;
                            deleteAction = true;
                        } 
                        else
                        {
                            if (GetTestPlanDeleteAuthority(testTestplanList[i].ID))
                            {
                                mydt.Rows[i]["IgnoreFlag"] = true;
                                isSelected = true;
                                deleteAction = true;
                            } 
                            else
                            {
                                isSelected = true;
                                noAuthorityIDStr += testTestplanList[i].TestplanSelfText + ";";
                            }
                        }
                        
                    }                    
                }
                else
                {
                    Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false)
            {               
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请至少选择一个！');return false;</script>");                
                this.Page.RegisterStartupScript("", "<script>alert('请至少选择一个！');</script>");   
                //Response.Write("<script>alert('请至少选择一个！');</script>");
                return false;
            }
            if (deleteAction)
            {
                int result = -1;
                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                {
                    result = pDataIO.UpdateWithProc("TopoTestPlan", mydt, deletStr, logTracingString, "ATS_V2");
                }
                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                {
                    result = pDataIO.UpdateWithProc("TopoTestPlan", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
                }      

                if (result > 0)
                {
                    mydt.AcceptChanges();
                }
                else
                {
                    pDataIO.AlertMsgShow("数据更新失败!");
                } 
            }
            if (noAuthorityIDStr != "")
            {
                noAuthorityIDStr += "你没有操作权限!";
                noAuthorityIDStr = noAuthorityIDStr.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");
                string tempurl = Request.Url.ToString();
                string myscript = @"alert('"+noAuthorityIDStr+"');window.location.href='" + tempurl + "';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            else
            {
                DataTable dt = pDataIO.GetDataTable("select PID from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                int typeID = Convert.ToInt32(dt.Rows[0]["PID"]);
                dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + typeID, "GlobalProductionTypeNum");
                Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.ID ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + typeID + ") as newtable where newtable.ID =" + moduleTypeID, "GlobalProductionNameNum");
                Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();
                Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+1";

                Session["iframe_src"] = "WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID;

                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);  
                //Response.Redirect(Request.Url.ToString());
            }
                    
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;           
        }
        
    }
    
    protected void LoadOptionButton()
    {
        //ASCXOptionButtons UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool SaveData(object obj, string prameter)
    {
        return true;
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
        bool addTestPlan = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);

        if (addTestPlan)
        {
            OptionButtons1.ConfigBtAddVisible = true;
            OptionButtons1.ConfigBtCopyVisible = true;
        } 
        else
        {
            bool addPlan = PNAuthority();
            OptionButtons1.ConfigBtAddVisible = addPlan;
            OptionButtons1.ConfigBtCopyVisible =addPlan;
        }
        
        OptionButtons1.ConfigBtEditVisible = false;
        if (rowCount<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
            OptionButtons1.ConfigBtCopyVisible = false;
        } 
        else
        {
            deleteFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
            if (deleteFunctionAuthority)
            {
                OptionButtons1.ConfigBtDeleteVisible = true;
            } 
            else
            {
                GetTestPlanAuthority();
                OptionButtons1.ConfigBtDeleteVisible = DeleteButtonVisibleWhenReadAuthority();
            }
            
        }
       
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (testTestplanList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < testTestplanList.Length; i++)
        {
            testTestplanList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (testTestplanList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < testTestplanList.Length; i++)
        {
            testTestplanList[i].BeSelected = false;
        }
    }
    public void ClearCurrenPage()
    {
        if (rowCount == 0)
        {
            SelectAll.Visible = false;
            DeSelectAll.Visible = false;
        }
    }
    public bool PNAuthority()
    {
        bool editVisibal = false;
        string userID = Session["UserID"].ToString().Trim();
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + moduleTypeID, "UserPNAction");
                //DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where PNID=" + moduleTypeID, "UserPNAction");
                if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                {
                    return false;
                }
                else
                {
                    if (temp.Rows[0]["AddPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["AddPlan"].ToString().Trim().ToUpper() == "1")
                    {
                        editVisibal = true;
                    }
                    else
                    {
                        editVisibal = false;
                    }

                }
            }
            return editVisibal;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public void GetTestPlanAuthority()
    {        
        string userID = Session["UserID"].ToString().Trim();
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                for (int i = 0; i < testplanAuthority.Length;i++)
                {
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + testplanAuthority[i].planid, "UserPlanAction");
                   
                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                        testplanAuthority[i].modifyPlan = false;
                        testplanAuthority[i].deletePlan = false;
                        testplanAuthority[i].runPlan = false;
                    }
                    else
                    {
                        if (temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            testplanAuthority[i].modifyPlan = true;
                        }
                        else
                        {
                            testplanAuthority[i].modifyPlan = false;
                        }
                        if (temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "1")
                        {
                            testplanAuthority[i].deletePlan = true;
                        }
                        else
                        {
                            testplanAuthority[i].deletePlan = false;
                        }
                        if (temp.Rows[0]["RunPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["RunPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            testplanAuthority[i].runPlan = true;
                        }
                        else
                        {
                            testplanAuthority[i].runPlan = false;
                        }
                    }
                }
               
            }
         
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool GetTestPlanDeleteAuthority(string inPutPlanID)
    {
        bool deletePlan = false;
        try
        {
            for (int i = 0; i < testplanAuthority.Length;i++ )
            {
                if (testplanAuthority[i].planid == inPutPlanID)
                {
                    if (testplanAuthority[i].deletePlan==true)
                    {
                        deletePlan = true;
                    } 
                    else
                    {
                        deletePlan = false;
                    }
                }
            }
            return deletePlan;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool DeleteButtonVisibleWhenReadAuthority()
    {
        bool deleButtonVisible = false;
        try
        {
            for (int i = 0; i < testplanAuthority.Length;i++ )
            {
                if (testplanAuthority[i].deletePlan==true)
                {
                    deleButtonVisible = true;
                    break;
                }
            }
            return deleButtonVisible;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}