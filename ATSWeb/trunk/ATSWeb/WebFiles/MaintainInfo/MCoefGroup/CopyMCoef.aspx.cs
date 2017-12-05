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
using System.Collections;

public partial class ASPXCopyMCoef :BasePage
{
    string funcItemName = "模块系数信息";
    ASCXCopyMCoef testMCoefSelfInfor;
    ASCXOptionButtons UserOptionButton;

    private string conn;
    private DataIO pDataIO;
    private string moduleTypeID = "";
    private string logTracingString = "";
    private string SourceMCoefID = "";

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

        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(3);
        moduleTypeID = Request["uId"];
        SourceMCoefID = Request["sourceID"];
        connectDataBase();
        ConfigOptionButtonsVisible();
       
        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }       
    }

    public void bindData()
    {
        testMCoefSelfInfor = new ASCXCopyMCoef();

        testMCoefSelfInfor = (ASCXCopyMCoef)Page.LoadControl("~/Frame/MCoefGroup/CopyMCoef.ascx");
        testMCoefSelfInfor.TBItemNameText = "";
        testMCoefSelfInfor.EnableTBItemName = true;
        testMCoefSelfInfor.TxtItemDescription = "";
        testMCoefSelfInfor.EnabletxtItemDescription = true;

        this.AddNewMCoef.Controls.Add(testMCoefSelfInfor);
    }

    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {               
                bindData();
                string parentItem = "复制新项";
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

    public bool SaveData(object obj, string prameter)
    {       
        try
        {
            string errMsg = "";
            int result;

            result = pDataIO.CopyMCoef(Convert.ToInt32(SourceMCoefID), testMCoefSelfInfor.TBItemNameText.Trim(), testMCoefSelfInfor.TxtItemDescription.Trim(), Convert.ToInt32(moduleTypeID), logTracingString, out  errMsg);

            if (result>0)
            {
                Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefList.aspx?uId=" + moduleTypeID.Trim());               
            } 
            else
            {
                errMsg = errMsg.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");               
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + errMsg + "');</script>", false);
                testMCoefSelfInfor.TBItemNameText = "";
                testMCoefSelfInfor.TxtItemDescription = "";
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
            Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefList.aspx?uId=" + moduleTypeID.Trim());
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    public void ConfigOptionButtonsVisible()
    {
        OptionButtons1.ConfigBtSaveVisible = true;
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtCopyVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = true;
    }
    
}