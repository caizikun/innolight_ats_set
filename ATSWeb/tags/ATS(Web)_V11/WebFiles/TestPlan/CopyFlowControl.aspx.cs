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

public partial class ASPXCopyFlowControl :BasePage
{
    string funcItemName = "CtrlSelfInfo";
    ASCXCopyFlowControl testFlowControlSelfInfor;
    ASCXOptionButtons UserOptionButton;
    ValidateExpression expression = new ValidateExpression();
    string[] expressionList;
    private string conn;
    private DataIO pDataIO;
    private string moduleTypeID = "";
    private string logTracingString = "";
    private string SourceFlowControlID = ""; 

    public ASPXCopyFlowControl()
    {        
        conn = "inpcsz0518\\ATS_HOME";
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
        SetSessionBlockType(1);
        moduleTypeID = Request["uId"];
        SourceFlowControlID = Request["sourceID"];
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
        testFlowControlSelfInfor = new ASCXCopyFlowControl();

        testFlowControlSelfInfor = (ASCXCopyFlowControl)Page.LoadControl("../../Frame/TestPlan/CopyFlowControl.ascx");
        testFlowControlSelfInfor.TBItemNameText = "";
        testFlowControlSelfInfor.EnableTBItemName = true;

        this.AddNewFlowControl.Controls.Add(testFlowControlSelfInfor);
    }

    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {               
                bindData();
                string parentItem = "CopyNewItem";             
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

    public bool SaveData(object obj, string prameter)
    {       
        try
        {
            string errMsg = "";
            int result;

            result = pDataIO.CopyFlowCtrl(Convert.ToInt32(SourceFlowControlID), testFlowControlSelfInfor.TBItemNameText.Trim(), Convert.ToInt32(moduleTypeID), logTracingString, out  errMsg);

            if (result>0)
            {
                Response.Redirect("~/WebFiles/TestPlan/FlowControlList.aspx?uId=" + moduleTypeID.Trim());               
            } 
            else
            {
                errMsg = errMsg.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");               
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + errMsg + "');</script>", false);
                testFlowControlSelfInfor.TBItemNameText = "";
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
            Response.Redirect("~/WebFiles/TestPlan/FlowControlList.aspx?uId=" + moduleTypeID.Trim());
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