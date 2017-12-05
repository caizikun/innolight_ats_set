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
    string funcItemName = "TopoTestPlanList";
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
    public ASPXTestPlanList()
    {
        conn = "inpcsz0518\\ATS_HOME";
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        mydt.Clear();
    }
    
    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
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
                testTestplanList[i] = (ASCXTestPlanList)Page.LoadControl("../../Frame/TestPlan/TestPlanList.ascx");
                testTestplanList[i].LbTH1Text = mydt.Columns[2].ColumnName;
                testTestplanList[i].LbTH2Text = mydt.Columns[3].ColumnName;
                testTestplanList[i].LbTH3Text = mydt.Columns[4].ColumnName;
                testTestplanList[i].LbTH4Text = mydt.Columns[11].ColumnName;
                testTestplanList[i].LbTHVersionText = mydt.Columns[12].ColumnName;
                testTestplanList[i].ContentTRVisible = false;                
                this.TestPlanList.Controls.Add(testTestplanList[i]);
            }
        } 
        else
        {
            testTestplanList = new ASCXTestPlanList[rowCount];
            testplanAuthority = new TestPlanAuthoriy[rowCount];
            for (byte i = 0; i < testTestplanList.Length; i++)
            {
                testTestplanList[i] = (ASCXTestPlanList)Page.LoadControl("../../Frame/TestPlan/TestPlanList.ascx");
                testTestplanList[i].IsSelectedID = "CHECK" + mydt.Rows[i]["ID"].ToString().Trim();
                
                testTestplanList[i].TestplanSelfText = mydt.Rows[i]["ItemName"].ToString().Trim();
                testTestplanList[i].LabelSWVerText = mydt.Rows[i]["SWVersion"].ToString().Trim();
                testTestplanList[i].LabelHWVerText = mydt.Rows[i]["HwVersion"].ToString().Trim();
                testTestplanList[i].LabelDescription = mydt.Rows[i]["ItemDescription"].ToString().Trim();
                testTestplanList[i].configAllDescription = mydt.Rows[i]["ItemDescription"].ToString().Trim();
                testTestplanList[i].ConfigLabelVersion = mydt.Rows[i]["Version"].ToString().Trim();
                testTestplanList[i].TestplanSelfID = mydt.Rows[i]["ID"].ToString().Trim();
                testTestplanList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                testplanAuthority[i].planid = testTestplanList[i].ID;
                testTestplanList[i].LbTH1Text = mydt.Columns[2].ColumnName;
                testTestplanList[i].LbTH2Text = mydt.Columns[3].ColumnName;
                testTestplanList[i].LbTH3Text = mydt.Columns[4].ColumnName;
                testTestplanList[i].LbTH4Text = mydt.Columns[11].ColumnName;
                testTestplanList[i].LbTHVersionText = mydt.Columns[12].ColumnName;
                testTestplanList[i].BeSelected = false;
                testTestplanList[i].PostBackUrlStringTopPNSpec = "~/WebFiles/TestPlan/TopPNSpecList.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
                if (i >= 1)
                {
                    testTestplanList[i].LBTH1Visible = false;
                    testTestplanList[i].LBTH2Visible = false;
                    testTestplanList[i].LBTH3Visible = false;
                    testTestplanList[i].LBTH4Visible = false;
                    testTestplanList[i].LBTH5Visible = false;
                    testTestplanList[i].LBTH6Visible = false;
                    testTestplanList[i].LBTH7Visible = false;
                    testTestplanList[i].LBTH8Visible = false;
                    testTestplanList[i].LBTH9Visible = false;
                    testTestplanList[i].LBTHTitleVisible(false);
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
                string parentItem = pDataIO.getDbCmdExecuteScalar("select PN from GlobalProductionName where id = " + moduleTypeID).ToString();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
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
            Response.Redirect("~/WebFiles/TestPlan/AddNewTestPlan.aspx?uId=" + moduleTypeID.Trim());            
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
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('Did not choose any one！');return false;</script>");                
                this.Page.RegisterStartupScript("", "<script>alert('please choose one only！');</script>");
                //Response.Write("<script>alert('Did not choose any one！');</script>");
                return false;
            }

            Response.Redirect("~/WebFiles/TestPlan/CopyTestPlan.aspx?uId=" + moduleTypeID.Trim()+"&sourceID=" + copySourceID);
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
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('Did not choose any one！');return false;</script>");                
                this.Page.RegisterStartupScript("", "<script>alert('Did not choose any one！');</script>");   
                //Response.Write("<script>alert('Did not choose any one！');</script>");
                return false;
            }
            if (deleteAction)
            {
                int result = pDataIO.UpdateWithProc("TopoTestPlan", mydt, deletStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!");
                } 
            }
            if (noAuthorityIDStr != "")
            {
                noAuthorityIDStr += "you have no authority!";
                noAuthorityIDStr = noAuthorityIDStr.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");
                string tempurl = Request.Url.ToString();
                string myscript = @"alert('"+noAuthorityIDStr+"');window.location.href='" + tempurl + "';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            else
            {
                Response.Redirect(Request.Url.ToString());
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
        bool addTestPlan = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
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
            deleteFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
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