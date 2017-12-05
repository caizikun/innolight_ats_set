﻿using System;
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

public partial class ASPXCopyTestPlan :BasePage
{
    string funcItemName = "测试方案信息";
    ASCXCopyTestPlan testTestplanSelfInfor;
    ASCXOptionButtons UserOptionButton;
    ValidateExpression expression = new ValidateExpression();
    string[] expressionList;
   private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   public DataTable mydtFather = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private string logTracingString = "";
   private string SourceTestPlanID = "";
   private string PNText;
   private string TypeText;
   private bool testPlanAddFunctionAuthority = false;
    
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
            SourceTestPlanID = Request["sourceID"];
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }
        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();
        testPlanAddFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
    }

    public void bindData()
    {

        testTestplanSelfInfor = new ASCXCopyTestPlan();

        testTestplanSelfInfor = (ASCXCopyTestPlan)Page.LoadControl("~/Frame/TestPlan/CopyTestPlan.ascx");
        testTestplanSelfInfor.TBItemNameText = "";
        testTestplanSelfInfor.EnableTBItemName = true;
           
        this.AddNewTestPlan.Controls.Add(testTestplanSelfInfor);
    }

    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + moduleTypeID, "TopoTestPlan");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = "复制新项";
                //string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from GlobalProductionName where id = " + moduleTypeID).ToString();

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
    public bool EditData(object obj, string prameter)
    {
        try
        {
           
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool SaveData(object obj, string prameter)
    {
       
        try
        {
            string errMsg = "";
            int result;

            if (testTestplanSelfInfor.CheckBox1CheckedFlag == false)
            {
                result = pDataIO.CopyTestPlan(Convert.ToInt32(SourceTestPlanID), testTestplanSelfInfor.TBItemNameText.Trim(), Convert.ToInt32(moduleTypeID), logTracingString, out  errMsg);
            }
            else
            {
                PNText = testTestplanSelfInfor.DropDownList2Text;
                TypeText = testTestplanSelfInfor.DropDownList1Text;

                pDataIO.OpenDatabase(true);

                string sql;
                sql = "select GlobalProductionName.ID from GlobalProductionName,GlobalProductionType "
                    + "where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionType.ItemName='" + TypeText + "' and GlobalProductionName.PN='" + PNText + "'";

                DataTable dt = pDataIO.GetDataTable(sql, "");

                if (dt.Rows.Count != 0)
                {
                    moduleTypeID = dt.Rows[0]["ID"].ToString ();
                }
                 
                result = pDataIO.CopyTestPlan(Convert.ToInt32(SourceTestPlanID), testTestplanSelfInfor.TBItemNameText.Trim(), Convert.ToInt32(moduleTypeID), logTracingString, out  errMsg);
            }
            
            if (result>0)
            {
                if (testPlanAddFunctionAuthority == false)
                {
                    string userPlanStr = "select * from UserPlanAction where ID='-1'";
                    DataTable userPlanTable = pDataIO.GetDataTable(userPlanStr, "UserPlanAction");
                    DataRow userDR = userPlanTable.NewRow();
                    userDR[0] = -1;
                    userDR[1] = Session["UserID"].ToString().Trim();
                    userDR[2] = result;
                    userDR[3] = true;
                    userDR[4] = true;
                    userDR[5] = true;
                    userPlanTable.Rows.Add(userDR);

                    int resltUserPlan = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        resltUserPlan = pDataIO.UpdateWithProc("UserPlanAction", userPlanTable, userPlanStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        resltUserPlan = pDataIO.UpdateWithProc("UserPlanAction", userPlanTable, userPlanStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (resltUserPlan < 0)
                    {
                        errMsg = errMsg.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");
                        ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + errMsg + "');</script>", false);
                        testTestplanSelfInfor.TBItemNameText = "";

                    }
                    else
                    {
                        //Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
                        DataTable dt = pDataIO.GetDataTable("select PID from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                        int typeID = Convert.ToInt32(dt.Rows[0]["PID"]);
                        dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + typeID, "GlobalProductionTypeNum");
                        Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                        dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.ID ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + typeID + ") as newtable where newtable.ID =" + moduleTypeID, "GlobalProductionNameNum");
                        Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                        Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+1";

                        dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY TopoTestPlan.ID ASC) AS num,TopoTestPlan.* from TopoTestPlan where IgnoreFlag='false' and PID =" + moduleTypeID + ") as newtable where newtable.ItemName ='" + testTestplanSelfInfor.TBItemNameText.Trim() + "'", "TopoTestPlanNum");
                        Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                        Session["iframe_src"] = "WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + Convert.ToInt32(dt.Rows[0]["ID"]);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true); 
                    }

                }
                else
                {
                    //Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
                    DataTable dt = pDataIO.GetDataTable("select PID from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                    int typeID = Convert.ToInt32(dt.Rows[0]["PID"]);
                    dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + typeID, "GlobalProductionTypeNum");
                    Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                    dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.ID ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + typeID + ") as newtable where newtable.ID =" + moduleTypeID, "GlobalProductionNameNum");
                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+1";

                    dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY TopoTestPlan.ID ASC) AS num,TopoTestPlan.* from TopoTestPlan where IgnoreFlag='false' and PID =" + moduleTypeID + ") as newtable where newtable.ItemName ='" + testTestplanSelfInfor.TBItemNameText.Trim() + "'", "TopoTestPlanNum");
                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                    Session["iframe_src"] = "WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + Convert.ToInt32(dt.Rows[0]["ID"]);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);   
                }              
            } 
            else
            {
                errMsg = errMsg.Replace("'", @"""").Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t");
                //string Url = Request.Url.ToString();
                //HttpContext.Current.Response.Write("<script>alert('" + msg + "');javascript:location='"+Url+"';</script>");
                //ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + errMsg + "');javascript:location='" + Url + "';</script>", false);
                ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "aaa", "<script>alert('" + errMsg + "');</script>", false);
                testTestplanSelfInfor.TBItemNameText = "";
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
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
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
        OptionButtons1.ConfigBtEditVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = true;
    }
    
}