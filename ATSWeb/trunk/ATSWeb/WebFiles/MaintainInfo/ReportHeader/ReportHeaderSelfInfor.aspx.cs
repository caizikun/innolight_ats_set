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

public partial class ASPXReportHeaderSelfInfor : BasePage
{
    const string funcItemName = "报表表头信息";
    Frame_ReportHeaderInfo [] ControlList;
    private string logTracingString = "";
    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string HeaderID = "-1";
    bool AddNew = false;
    DataTable mydt = new DataTable();
    string queryStr = "";

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
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);

        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(3);
        if (Request.QueryString["uId"] != null)
        {
            HeaderID = Request.QueryString["uId"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            HeaderID = "-1";
        }
        queryStr = "select * from ReportHeader where id =" + HeaderID;
        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from ReportHeader where id = " + HeaderID).ToString();
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
            if (HeaderID != null && HeaderID.Length > 0)
            {
                createNavilnks();
                getInfo(queryStr, AddNew);
            }
            else
            {
                OptionButtons1.Visible = false;
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>",false);
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    bool getInfo(string filterStr, bool isEditState = false)
    {
        
        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            //----------------------------
            mydt = pDataIO.GetDataTable(filterStr, "ReportHeader");

            DataRow[] drs = mydt.Select("");
            ControlList = new Frame_ReportHeaderInfo[1];
            ControlList[0] = (Frame_ReportHeaderInfo)Page.LoadControl("~/Frame/TestReport/ReportHeaderInfo.ascx");
            ControlList[0].SetEquipEnableState(isEditState);
            if (drs.Length == 1)
            {
                //ControlList[0].ID = drs[0]["ID"].ToString();
                ControlList[0].TxtItemName = drs[0]["ItemName"].ToString();
                ControlList[0].TxtItemDescription = drs[0]["Description"].ToString();
            }
            else if (isEditState && drs.Length ==0)
            {
                ControlList[0].ID = "Header_New";
                ControlList[0].TxtItemName = "";
                ControlList[0].TxtItemDescription = "";
                EditData("", "");
            }
            
            this.plhMain.Controls.Add(ControlList[0]);
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
            for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i].SetEquipEnableState(true);
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
            if (mydt.Rows.Count == 1)
            {
                #region //已经获取到当前设备的信息
                mydt.Rows[0]["ItemName"] = ControlList[0].TxtItemName;
                mydt.Rows[0]["Description"] = ControlList[0].TxtItemDescription;
                //150527 ---------------------更新方式变更>>Start
                int result = -1;
                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                {
                    result = pDataIO.UpdateWithProc("ReportHeader", mydt, queryStr, logTracingString, "ATS_V2");
                }
                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                {
                    result = pDataIO.UpdateWithProc("ReportHeader", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
                }      

                if (result > 0)
                {                    
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderList.aspx?", true);
                }
                else
                {
                    pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End

                #endregion
            }
            else if (AddNew && mydt.Rows.Count == 0)
            {
                #region //New

                long currEqID = pDataIO.GetLastInsertData("ReportHeader") + 1;
                DataRow eqNewDr = mydt.NewRow();
                eqNewDr["ID"] = currEqID;
                eqNewDr["ItemName"] = ControlList[0].TxtItemName;
                eqNewDr["Description"] = ControlList[0].TxtItemDescription;

                mydt.Rows.Add(eqNewDr);

                //150527 ---------------------更新方式变更>>Start
                int result = -1;
                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                {
                    result = pDataIO.UpdateWithProc("ReportHeader", mydt, queryStr, logTracingString, "ATS_V2");
                }
                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                {
                    result = pDataIO.UpdateWithProc("ReportHeader", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
                }      

                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderList.aspx?", true);
                }
                else
                {
                    pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());                
                }
                //150527 ---------------------更新方式变更<<End
                #endregion
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!当前项已被删除!')</Script>",false);
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
                Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderList.aspx?", true);
            }
            else
            {
                Response.Redirect(Request.Url.ToString(), true);
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
        OptionButtons1.ConfigBtAddVisible =false;
        OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ReportHeader, CommCtrl.CheckAccess.MofifyReportHeader, myAccessCode);
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = false;
    }
}