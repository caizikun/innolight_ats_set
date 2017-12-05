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
using System.Reflection;
using System.Web.Compilation;

public partial class ASPXProductionPNList : BasePage
{
    string funcItemName = "产品类型";
    public DataTable mydt = new DataTable();
    ASCXProducPNList[] prductionPNList;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    public DataTable mydtCoefs = new DataTable();
    private SortedList<int, string> MCoefsIDMap = new SortedList<int, string>();

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
        MCoefsIDMap.Clear();

        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(1);
        moduleTypeID = Request["uId"];

        connectDataBase();
        LoadOptionButton();
        ConfigOptionButtonsVisible();

    }

    public void bindData()
    {
        ClearCurrenPage();
        if (rowCount == 0)
        {
            prductionPNList = new ASCXProducPNList[1];

            for (byte i = 0; i < prductionPNList.Length; i++)
            {
                prductionPNList[i] = (ASCXProducPNList)Page.LoadControl("~/Frame/Production/ProducPNList.ascx");

                prductionPNList[i].LbTH2 = "品名";
                prductionPNList[i].LbTH3 = "描述";
                prductionPNList[i].LbTH4 = "通道数";
                prductionPNList[i].LbTH5 = "电压数";
                prductionPNList[i].LbTH6 = "传感器类型";
                prductionPNList[i].LbTH7 = "系数组名";
                prductionPNList[i].ContentTRVisible = false;
                this.PRPNList.Controls.Add(prductionPNList[i]);
            }
        }
        else
        {
            prductionPNList = new ASCXProducPNList[rowCount];

            for (byte i = 0; i < prductionPNList.Length; i++)
            {
                prductionPNList[i] = (ASCXProducPNList)Page.LoadControl("~/Frame/Production/ProducPNList.ascx");
                prductionPNList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                prductionPNList[i].LbTH2Text = mydt.Rows[i]["PN"].ToString().Trim();
                prductionPNList[i].LbTH3Text = mydt.Rows[i]["ItemName"].ToString().Trim();
                prductionPNList[i].LbTH4Text = mydt.Rows[i]["Channels"].ToString().Trim();
                prductionPNList[i].LbTH5Text = mydt.Rows[i]["Voltages"].ToString().Trim();
                prductionPNList[i].LbTH6Text = mydt.Rows[i]["Tsensors"].ToString().Trim();
                ConfigMCoefs();
                prductionPNList[i].LbTH7Text = MCoefsIDMap[Convert.ToInt32(mydt.Rows[i]["MCoefsID"])];
                prductionPNList[i].LbTH2 = "品名";
                prductionPNList[i].LbTH3 = "描述";
                prductionPNList[i].LbTH4 = "通道数";
                prductionPNList[i].LbTH5 = "电压数";
                prductionPNList[i].LbTH6 = "传感器类型";
                prductionPNList[i].LbTH7 = "系数组名";
                prductionPNList[i].LinkBItemNameID = mydt.Rows[i]["ID"].ToString().Trim();               
                prductionPNList[i].BeSelected = false;
                if (i >= 1)
                {
                    prductionPNList[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        prductionPNList[i].TrBackgroundColor = "#F2F2F2";
                    }
                }

                //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
                this.PRPNList.Controls.Add(prductionPNList[i]);
            }
        }


    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalProductionName where IgnoreFlag='false'and PID=" + moduleTypeID, "GlobalProductionName");
                rowCount = mydt.Rows.Count;
                mydtCoefs = pDataIO.GetDataTable("select * from GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficientsGroup");
                bindData();
                funcItemName += "_";
                funcItemName += pDataIO.getDbCmdExecuteScalar("select itemName from GlobalProductionType where id = " + moduleTypeID).ToString();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "产品与测试", Session["BlockType"].ToString(), pDataIO, out logTracingString);
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



    public bool DeleteData(object obj, string prameter)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('Are you sure you want to delete？')){DeleteData1();}else{}</script>");
        bool isSelected = false;
        string deletStr = "select * from GlobalProductionName where IgnoreFlag='False'and PID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < prductionPNList.Length; i++)
            {
                ASCXProducPNList cb = (ASCXProducPNList)PRPNList.FindControl(prductionPNList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {

                        mydt.Rows[i]["IgnoreFlag"] = true;
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
                result = pDataIO.UpdateWithProc("GlobalProductionName", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalProductionName", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();

                DataTable dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + moduleTypeID, "GlobalProductionTypeNum");
                Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;
                Session["iframe_src"] = "WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + moduleTypeID;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
                Response.Redirect(Request.Url.ToString());
            }

              
            //Response.Redirect(Request.Url.ToString());
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
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/Production_ATS/Production/PNSelfInfor.aspx?AddNew=true&uId=" + moduleTypeID);

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    public void ConfigOptionButtonsVisible()
    {
        int myAccessCode = 0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
        if (rowCount <= 0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        }
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
        }

        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (prductionPNList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < prductionPNList.Length; i++)
        {
            prductionPNList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (prductionPNList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < prductionPNList.Length; i++)
        {
            prductionPNList[i].BeSelected = false;
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
    public bool ConfigMCoefs()
    {
        MCoefsIDMap.Clear();
        try
        {
            for (int i = 0; i < mydtCoefs.Rows.Count; i++)
            {
                MCoefsIDMap.Add(Convert.ToInt32(mydtCoefs.Rows[i]["ID"]), mydtCoefs.Rows[i]["ItemName"].ToString());
            }
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}