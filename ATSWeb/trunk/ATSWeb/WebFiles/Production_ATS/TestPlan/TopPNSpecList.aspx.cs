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

public partial class ASPXTopPNSpecList : BasePage
{
    const string funcItemName = "规格参数";
    ASCXTopPNSpecsList[] topASCXPNSpcesList;
     
     int rowCount;
    HyperLink[] hlkList;
    DataTable NaviDt = new DataTable();

    private string conn;
    private DataIO pDataIO;
    public DataTable mydt = new DataTable();
    public DataTable mydtGlobalSpces = new DataTable();
    private string moduleTypeID = "";
    private string logTracingString = "";
    private string specUnit = "";
    private string channelNumber = "";

    protected override void OnInit(EventArgs e)  
    {        
       
    }
    protected void Page_Load(object sender, EventArgs e)
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
        if (Session["ChannelNumber"] == null)
        {
            channelNumber = "0";
        }
        else
        {
            channelNumber = Convert.ToString(Session["ChannelNumber"]);
        }
             
        connectDataBase();
        ConfigOptionButtonsVisible();
    }
    public void bindData()
    {
       
        ClearCurrenPage();
        if (rowCount==0)
        {
            topASCXPNSpcesList = new ASCXTopPNSpecsList[1];

            for (byte i = 0; i < topASCXPNSpcesList.Length; i++)
            {
                topASCXPNSpcesList[i] = (ASCXTopPNSpecsList)Page.LoadControl("~/Frame/TestPlan/TopPNSpecsList.ascx");
             
                topASCXPNSpcesList[i].TH3Text = "典型值";
                topASCXPNSpcesList[i].TH4Text = "最小值";
                topASCXPNSpcesList[i].TH5Text = "最大值";
                topASCXPNSpcesList[i].TH7Text = "通道";
                topASCXPNSpcesList[i].ContentTRVisible = false;
                this.topPNSpecsList.Controls.Add(topASCXPNSpcesList[i]);


            }
        } 
        else
        {
            topASCXPNSpcesList = new ASCXTopPNSpecsList[rowCount];

            for (byte i = 0; i < topASCXPNSpcesList.Length; i++)
            {
                topASCXPNSpcesList[i] = (ASCXTopPNSpecsList)Page.LoadControl("~/Frame/TestPlan/TopPNSpecsList.ascx");
                topASCXPNSpcesList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                topASCXPNSpcesList[i].ConfigCloum2TextURL = "~/WebFiles/Production_ATS/TestPlan/TopPNSpecSeflInfor.aspx?uId=" + mydt.Rows[i]["ID"].ToString().ToUpper().Trim();
                topASCXPNSpcesList[i].ConfigCloum2Text = PNSpecsIDtoName(Convert.ToInt32(mydt.Rows[i]["SID"]), out specUnit);
                topASCXPNSpcesList[i].ConfigCloum3Text = mydt.Rows[i]["Typical"].ToString().Trim();
                topASCXPNSpcesList[i].ConfigCloum4Text = mydt.Rows[i]["SpecMin"].ToString().Trim();
                topASCXPNSpcesList[i].ConfigCloum5Text = mydt.Rows[i]["SpecMax"].ToString().Trim();
                topASCXPNSpcesList[i].ConfigCloum7Text = mydt.Rows[i]["Channel"].ToString().Trim();
                topASCXPNSpcesList[i].ConfigCloum6Text = specUnit;
                topASCXPNSpcesList[i].TH3Text = "典型值";
                topASCXPNSpcesList[i].TH4Text = "最小值";
                topASCXPNSpcesList[i].TH5Text = "最大值";
                topASCXPNSpcesList[i].TH7Text = "通道";
                if (i >= 1)
                {
                    topASCXPNSpcesList[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        topASCXPNSpcesList[i].TrBackgroundColor = "#F2F2F2";
                    }

                }
                {
                    topASCXPNSpcesList[i].SelectedVisible = true;
                }
                this.topPNSpecsList.Controls.Add(topASCXPNSpcesList[i]);


            }
        }
       
       
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoPNSpecsParams where PID=" + moduleTypeID, "TopoPNSpecsParams");
                rowCount = mydt.Rows.Count;
                mydtGlobalSpces = pDataIO.GetDataTable("select * from GlobalSpecs", "GlobalSpecs");             
                bindData();

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "2"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.PN from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                string uid2 = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.ID from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + uid2;

                string channels = pDataIO.getDbCmdExecuteScalar("select Channels from GlobalProductionName where id = " + uid2).ToString();
                Session["ChannelNumber"] = channels;

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                string uid = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + uid;

                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "3_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + moduleTypeID;   
                string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from TopoTestPlan where id = " + moduleTypeID).ToString();
                
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
    
    public void ConfigOptionButtonsVisible()
    {
        
        {           
            int myAccessCode =0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();

            OptionButtons1.ConfigBtSaveVisible = false;
            OptionButtons1.ConfigBtEditVisible = false;
            OptionButtons1.ConfigBtCancelVisible = false;
            #region TestPlanAuthority
            bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
            bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);

            if (addVisible == false && deleteVisible == false)
            {
                bool deletePlan;
                bool testplanEdit = GetTestPlanAuthority(out deletePlan);
                OptionButtons1.ConfigBtAddVisible = testplanEdit;
                if (rowCount <= 0)
                {
                    OptionButtons1.ConfigBtDeleteVisible = false;
                }
                else
                {

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
                }
                else
                {
                    OptionButtons1.ConfigBtDeleteVisible = deleteVisible;
                }
            }


            #endregion
        }
       
        

    }
    
    
    public string PNSpecsIDtoName(int msaid,out string SpecUnit)
    {
        string msaname = "";
        SpecUnit="";
        try
        {
            {
                for (int i = 0; i < mydtGlobalSpces.Rows.Count; i++)
                {
                    if (Convert.ToInt32(mydtGlobalSpces.Rows[i]["ID"]) == msaid)
                    {
                        msaname = mydtGlobalSpces.Rows[i]["ItemName"].ToString().Trim();
                        SpecUnit = mydtGlobalSpces.Rows[i]["Unit"].ToString().Trim();
                    }
                }

                return msaname;
            }

        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw (ex);
        }
    }

    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TopPNSpecSeflInfor.aspx?AddNew=true&uId=" + moduleTypeID.Trim());
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (topASCXPNSpcesList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < topASCXPNSpcesList.Length; i++)
        {
            topASCXPNSpcesList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (topASCXPNSpcesList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < topASCXPNSpcesList.Length; i++)
        {
            topASCXPNSpcesList[i].BeSelected = false;
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
    public bool DeleteData(object obj, string prameter)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('Are you sure you want to delete？')){DeleteData1();}else{}</script>");
        bool isSelected = false;
        string deletStr = "select * from TopoPNSpecsParams where PID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < topASCXPNSpcesList.Length; i++)
            {
                ASCXTopPNSpecsList cb = (ASCXTopPNSpecsList)topPNSpecsList.FindControl(topASCXPNSpcesList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        mydt.Rows[i].Delete();
                        isSelected = true;
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
                return false;
            }

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoPNSpecsParams", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoPNSpecsParams", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
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
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
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

                        //tpAuthority = false;
                        deletePlan = false;
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
}