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
using System.Web.UI.HtmlControls;
public partial class ASPXFlowControlList : BasePage
{
    string funcItemName = "流程控制";
    ASCXFlowControlList[] TestplanFlowControlList;
    ASCXOptionButtons UserOptionButton;
    private int rowCount;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   public DataTable mydtProcess = new DataTable();
   private string moduleTypeID = "";
   private string logTracingString = "";
   private string CheckedID = "";

    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();

            rowCount = 0;
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
            moduleTypeID = Request["uId"];
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
            TestplanFlowControlList = new ASCXFlowControlList[1];
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {

                TestplanFlowControlList[i] = (ASCXFlowControlList)Page.LoadControl("~/Frame/TestPlan/FlowControlList.ascx");

                TestplanFlowControlList[i].LbTH1Text = "名称";
                TestplanFlowControlList[i].LbTH3Text = "通道";
                TestplanFlowControlList[i].LbTH4Text = "温度";
                TestplanFlowControlList[i].LbTH5Text = "电压";
                TestplanFlowControlList[i].LbTH2Text = "排序";  

                TestplanFlowControlList[i].ContentTRVisible = false;
                this.FlowControlList.Controls.Add(TestplanFlowControlList[i]);

            }
        } 
        else
        {
            TestplanFlowControlList = new ASCXFlowControlList[rowCount];
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {

                TestplanFlowControlList[i] = (ASCXFlowControlList)Page.LoadControl("~/Frame/TestPlan/FlowControlList.ascx");
                TestplanFlowControlList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                TestplanFlowControlList[i].LinkBItemNameID = mydt.Rows[i]["ID"].ToString().Trim();
                TestplanFlowControlList[i].LiBItemNameText = mydt.Rows[i]["ItemName"].ToString().Trim();
                TestplanFlowControlList[i].LbSEQText = (i + 1).ToString().Trim();
                TestplanFlowControlList[i].LbChannelText = mydt.Rows[i]["Channel"].ToString().Trim();
                TestplanFlowControlList[i].LbTempText = mydt.Rows[i]["Temp"].ToString().Trim();
                TestplanFlowControlList[i].LbVccText = mydt.Rows[i]["Vcc"].ToString().Trim();
                TestplanFlowControlList[i].BeSelected = false;

                TestplanFlowControlList[i].LbTH1Text = "名称";
                TestplanFlowControlList[i].LbTH3Text = "通道";
                TestplanFlowControlList[i].LbTH4Text = "温度";
                TestplanFlowControlList[i].LbTH5Text = "电压";
                TestplanFlowControlList[i].LbTH2Text = "排序";  

                TestplanFlowControlList[i].LbIgnoreText = mydtProcess.Rows[i]["IgnoreFlag"].ToString().Trim();
                if (i >= 1)
                {
                    TestplanFlowControlList[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        TestplanFlowControlList[i].TrBackgroundColor = "#F2F2F2";
                    }
                }
                this.FlowControlList.Controls.Add(TestplanFlowControlList[i]);

            }
        }
       

    }
    public void PostBackData()
    {
        TestplanFlowControlList = new ASCXFlowControlList[rowCount];
        ConfigTDProcess();
        for (int i = 0; i < TestplanFlowControlList.Length; i++)
        {

            TestplanFlowControlList[i] = (ASCXFlowControlList)Page.LoadControl("~/Frame/TestPlan/FlowControlList.ascx");
            TestplanFlowControlList[i].ID = mydtProcess.Rows[i]["ID"].ToString().Trim();
            TestplanFlowControlList[i].LinkBItemNameID = mydtProcess.Rows[i]["ID"].ToString().Trim();
            TestplanFlowControlList[i].LiBItemNameText = mydtProcess.Rows[i]["ItemName"].ToString().Trim();
            TestplanFlowControlList[i].LbSEQText = (i+1).ToString().Trim();
            TestplanFlowControlList[i].LbChannelText = mydtProcess.Rows[i]["Channel"].ToString().Trim();
            TestplanFlowControlList[i].LbTempText = mydtProcess.Rows[i]["Temp"].ToString().Trim();
            TestplanFlowControlList[i].LbVccText = mydtProcess.Rows[i]["Vcc"].ToString().Trim();
            //TestplanFlowControlList[i].BeSelected = false;            
            TestplanFlowControlList[i].LbIgnoreText = mydtProcess.Rows[i]["IgnoreFlag"].ToString().Trim();

            TestplanFlowControlList[i].LbTH1Text = "名称";
            TestplanFlowControlList[i].LbTH3Text = "通道";
            TestplanFlowControlList[i].LbTH4Text = "温度";
            TestplanFlowControlList[i].LbTH5Text = "电压";
            TestplanFlowControlList[i].LbTH2Text = "排序";

            if (CheckedID != "")
            {
                if (TestplanFlowControlList[i].ID == CheckedID)
                {
                    TestplanFlowControlList[i].BeSelected = true;
                }
            }

            if (i >= 1)
            {
                TestplanFlowControlList[i].LBTHTitleVisible(false);
                if (i % 2 != 0)
                {
                    TestplanFlowControlList[i].TrBackgroundColor = "#F2F2F2";
                }
            }
            this.FlowControlList.Controls.Add(TestplanFlowControlList[i]);

        }
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoTestControl where PID=" + moduleTypeID + "ORDER BY SEQ", "TopoTestControl");
                mydtProcess = mydt;              
                rowCount = mydt.Rows.Count;
                SeqRecord();
                if (!IsPostBack)
                {
                    bindData();
                }
                else
                {
                    PostBackData();
                }

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "2"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.PN from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                string uid2 = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.ID from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + uid2;

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                string uid = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + uid;

                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "3_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + moduleTypeID;   
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();
               
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

    protected void LoadOptionButton()
    {
        //UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/AddNewFlowControl.aspx?uId=" + moduleTypeID.Trim());
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
        int row = 0;
        bool isSelected = false;
        string deletStr = "select * from TopoTestControl  where IgnoreFlag='False'and PID=" + moduleTypeID + "ORDER BY SEQ";
        try
        {
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {
                ASCXFlowControlList cb = (ASCXFlowControlList)FlowControlList.FindControl(TestplanFlowControlList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        row++;
                        isSelected = true;
                        //mydt.Rows[i].Delete();
                        mydt.Rows[i]["IgnoreFlag"] = true;
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

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestControl", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestControl", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
            }        
            Response.Redirect(Request.Url.ToString());
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    public bool OrderInfor(object obj, string prameter)
    {
        try
        {
            if (rowCount > 1)
            {
                OptionButtons1.ConfigBtSaveVisible = true;
                OptionButtons1.ConfigBtCancelVisible = true;
                OptionButtons1.ConfigBtEditVisible = false;
                OptionButtons1.ConfigBtOrderVisible = false;
                OptionButtons1.ConfigBtAddVisible = false;
                OptionButtons1.ConfigBtDeleteVisible = false;

                OptionButtons1.ConfigBtOrderUpVisible = true;
                OptionButtons1.ConfigBtOrderDownVisible = true;

                OptionButtons1.ConfigBtCopyVisible = false;
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('仅有一项，无法排序！')", true);
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
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {
                Label lb = (Label)tableFC.FindControl("SEQ" + Convert.ToString(mydt.Rows[i]["ID"]));
                if (lb!=null)
                {
                    mydt.Rows[i]["SEQ"] = lb.Text;
                }
                
            }
            string cmdString = "select * from TopoTestControl  where PID=" + moduleTypeID + "ORDER BY SEQ";

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestControl", mydt, cmdString, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestControl", mydt, cmdString, logTracingString, "ATS_VXDEBUG");
            }      

           if (result > 0)
           {
               mydt.AcceptChanges();
           }
           else
           {
               pDataIO.AlertMsgShow("数据更新失败!");
           }
            Response.Redirect(Request.Url.ToString());
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
            Response.Redirect(Request.Url.ToString());
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
       

    }   
    public void ConfigOptionButtonsVisible()
    {
        if (!IsPostBack)
        {
            int myAccessCode = 0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();
            OptionButtons1.ConfigBtCancelVisible = false;
            OptionButtons1.ConfigBtSaveVisible = false;
            OptionButtons1.ConfigBtEditVisible = false;
            #region TestPlanAuthority
            bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
            bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
            bool editVidible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);

            if (addVisible == false && deleteVisible == false && editVidible == false)
            {
                bool deletePlan;
                bool testplanEdit = GetTestPlanAuthority(out deletePlan);
                OptionButtons1.ConfigBtAddVisible = testplanEdit;

                if (rowCount <= 0)
                {
                    OptionButtons1.ConfigBtOrderVisible = false;
                    OptionButtons1.ConfigBtDeleteVisible = false;
                    OptionButtons1.ConfigBtCopyVisible = false;
                }
                else
                {
                    if (rowCount == 1)
                    {
                        OptionButtons1.ConfigBtOrderVisible = false;
                    }
                    else
                    {
                        OptionButtons1.ConfigBtOrderVisible = testplanEdit;
                    }

                    OptionButtons1.ConfigBtCopyVisible = testplanEdit;
                    if (testplanEdit)
                    {
                        OptionButtons1.ConfigBtDeleteVisible = testplanEdit;
                    }
                    else
                    {
                        OptionButtons1.ConfigBtDeleteVisible = deletePlan;
                    }
                }

            }
            else
            {
                OptionButtons1.ConfigBtAddVisible = addVisible;

                if (rowCount <= 0)
                {
                    OptionButtons1.ConfigBtDeleteVisible = false;
                    OptionButtons1.ConfigBtOrderVisible = false;
                    OptionButtons1.ConfigBtCopyVisible = false;
                }
                else
                {
                    if (rowCount == 1)
                    {
                        OptionButtons1.ConfigBtOrderVisible = false;
                    }
                    else
                    {
                        OptionButtons1.ConfigBtOrderVisible = editVidible;
                    }

                    OptionButtons1.ConfigBtDeleteVisible = deleteVisible;
                    OptionButtons1.ConfigBtCopyVisible = addVisible;
                }
            }
            #endregion       
        }
    }
    public void SeqRecord()
    {
        //if (!IsPostBack)
        {
            //HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            
            for (int i = 0; i < mydt.Rows.Count;i++ )
            {
                Label Lb = new Label();
                Lb.Text = mydtProcess.Rows[i]["SEQ"].ToString().Trim();
                Lb.ID = "SEQ"+mydtProcess.Rows[i]["ID"].ToString().Trim();
                cell.Controls.Add(Lb);

                trFC.Cells.Add(cell);
                
            }
            //this.tableFC.Rows.Add(trFC);
        }
        
    }
    public void ConfigTDProcess()
    {
        for (int i = 0; i < mydtProcess.Rows.Count; i++)
        {
            Label finLB = (Label)tableFC.FindControl("SEQ" + mydtProcess.Rows[i]["ID"].ToString().Trim());           
            if (mydtProcess.Rows[i]["SEQ"].ToString().ToLower() != finLB.Text.ToLower()) //150529 防止出现多条记录被修改 而实际内容没变
            {
                mydtProcess.Rows[i]["SEQ"] = finLB.Text;
            }
        }
        DataView dv = mydtProcess.DefaultView;
        dv.Sort = "SEQ";
        mydtProcess = dv.ToTable();
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (TestplanFlowControlList.Length<=0)
        {
            return;
        }
        for (int i = 0; i < TestplanFlowControlList.Length; i++)
        {
            TestplanFlowControlList[i].BeSelected = true;
         }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (TestplanFlowControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < TestplanFlowControlList.Length; i++)
        {
            TestplanFlowControlList[i].BeSelected = false;
        }   
    }
    public void ClearCurrenPage()
    {
        if (rowCount==0)
        {
            SelectAll.Visible = false;
            DeSelectAll.Visible = false;
        }
    }

    public bool GetTestPlanAuthority(out bool deletePlan)
    {
        string userID = Session["UserID"].ToString().Trim();
        bool tpAuthority = false;
        deletePlan = false;
        try
        {

            if (pDataIO.OpenDatabase(true))
            {
                DataTable pnId = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + moduleTypeID, "TopoTestPlan");

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
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + moduleTypeID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                        deletePlan = false;
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
                        if (temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "1")
                        {
                            deletePlan = true;
                        }
                        else
                        {
                            deletePlan = false;
                        }
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

    public bool CopyData(object obj, string prameter)
    {
        bool isSelected = false;
        int selectCount = 0;
        string copySourceID = "";
        try
        {
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {
                ASCXFlowControlList cb = (ASCXFlowControlList)FlowControlList.FindControl(TestplanFlowControlList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        selectCount++;
                        isSelected = true;
                        copySourceID = cb.LinkBItemNameID;
                    }
                }
                else
                {
                    Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false || selectCount > 1)
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请至少选择一个！');return false;</script>");                
                this.Page.RegisterStartupScript("", "<script>alert('只能选择一个！');</script>");
                //Response.Write("<script>alert('请至少选择一个！');</script>");
                return false;
            }

            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/CopyFlowControl.aspx?uId=" + moduleTypeID.Trim() + "&sourceID=" + copySourceID);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }

    public string CheckSelected()
    {
        string id = "none";

        for (int i = 0; i < TestplanFlowControlList.Length; i++)
        {
            if (TestplanFlowControlList[i].BeSelected == true)
            {
                if (id == "none")
                {
                    id = TestplanFlowControlList[i].ID;
                }
                else
                {
                    id = "NotOne";
                    break;
                }
            }
        }

        return id;
    }

    public bool OrderUp(object obj, string prameter)
    {
        try
        {
            string ss = CheckSelected();

            if (ss == "none" || ss == "NotOne")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('请勾选一项进行排序！')", true);
                return false;
            }
            else
            {
                #region SEQ-1;
                if (Convert.ToInt64(ss) <= 1)
                {
                    return true;
                }
                else
                {
                    for (int i = 0; i < TestplanFlowControlList.Length; i++)
                    {
                        if (TestplanFlowControlList[i].ID == ss)
                        {
                            if (i <= 0)
                            {
                                return true;
                            }
                            else
                            {
                                Label finLB = (Label)tableFC.FindControl("SEQ" + ss);
                                Label finLB1 = (Label)tableFC.FindControl("SEQ" + TestplanFlowControlList[i - 1].ID);
                                if (finLB != null && finLB1 != null)
                                {
                                    string temptext = finLB.Text;
                                    finLB.Text = finLB1.Text;
                                    finLB1.Text = temptext;
                                }
                            }
                        }
                    }
                }
                #endregion

                this.FlowControlList.Controls.Clear();
                CheckedID = ss;
                PostBackData();
                return true;
            }
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool OrderDown(object obj, string prameter)
    {
        try
        {
            string ss = CheckSelected();

            if (ss == "none" || ss == "NotOne")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('请勾选一项进行排序！')", true);
                return false;
            }
            else
            {
                #region SEQ+1
                for (int i = 0; i < TestplanFlowControlList.Length; i++)
                {
                    if (TestplanFlowControlList[i].ID == ss)
                    {
                        if (i >= (TestplanFlowControlList.Length - 1))
                        {
                            return true;
                        }
                        else
                        {
                            Label finLB = (Label)tableFC.FindControl("SEQ" + ss);
                            Label finLB1 = (Label)tableFC.FindControl("SEQ" + TestplanFlowControlList[i + 1].ID);
                            if (finLB != null && finLB1 != null)
                            {
                                string temptext = finLB.Text;
                                finLB.Text = finLB1.Text;
                                finLB1.Text = temptext;
                            }
                        }
                    }
                }
                #endregion
            
                this.FlowControlList.Controls.Clear();
                CheckedID = ss;
                PostBackData();
                return true;
            }
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }
    
}
