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
public partial class ASPXReportHeaderList : BasePage
{
    const string funcItemName = "报表表头";
    ASCXReportHeaderList[] ReportHeaderInfor;
     
     int rowCount;
    HyperLink[] hlkList;
    DataTable NaviDt = new DataTable();
    
     private string conn;
    private DataIO pDataIO ;   
    public DataTable mydt = new DataTable();
    private string logTracingString = "";

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
            Session["iframe_src"] = null;
            SetSessionBlockType(3);
            connectDataBase();
            ConfigOptionButtonsVisible();
        }        
    }

    public void bindData()
    {       
        ClearCurrenPage();
        if (rowCount==0)
        {
            ReportHeaderInfor = new ASCXReportHeaderList[1];

            for (byte i = 0; i < ReportHeaderInfor.Length; i++)
            {
                ReportHeaderInfor[i] = (ASCXReportHeaderList)Page.LoadControl("~/Frame/TestReport/ReportHeaderList.ascx");

                ReportHeaderInfor[i].TH1TEXT = "名称";
                ReportHeaderInfor[i].TH2TEXT = "描述"; 
                ReportHeaderInfor[i].ContentTRVisible = false;
                this.headerList.Controls.Add(ReportHeaderInfor[i]);
            }
        } 
        else
        {
            ReportHeaderInfor = new ASCXReportHeaderList[rowCount];

            for (byte i = 0; i < ReportHeaderInfor.Length; i++)
            {
                ReportHeaderInfor[i] = (ASCXReportHeaderList)Page.LoadControl("~/Frame/TestReport/ReportHeaderList.ascx");
                ReportHeaderInfor[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                ReportHeaderInfor[i].LinkText1 = mydt.Rows[i]["ItemName"].ToString().Trim();
                ReportHeaderInfor[i].PostBackUrlString = "~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSelfInfor.aspx?uId=" + mydt.Rows[i]["ID"].ToString().Trim();
                ReportHeaderInfor[i].LnkViewParamsPostBackUrl = "~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + mydt.Rows[i]["ID"].ToString().Trim();

                ReportHeaderInfor[i].ConfigLbText = mydt.Rows[i]["Description"].ToString().Trim();

                ReportHeaderInfor[i].TH1TEXT = "名称";
                ReportHeaderInfor[i].TH2TEXT = "描述";

                if (i >= 1)
                {
                    ReportHeaderInfor[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        ReportHeaderInfor[i].TrBackgroundColor = "#F2F2F2";
                    }
                }
                this.headerList.Controls.Add(ReportHeaderInfor[i]);
            }
        }            
    }

    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ReportHeader", "ReportHeader");
                rowCount = mydt.Rows.Count;              
                bindData();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "维护", Session["BlockType"].ToString(), pDataIO, out logTracingString);
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
        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ReportHeader, CommCtrl.CheckAccess.MofifyReportHeader, myAccessCode);
        OptionButtons1.ConfigBtEditVisible =false;
        if (rowCount<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ReportHeader, CommCtrl.CheckAccess.MofifyReportHeader, myAccessCode);
        }
      
        OptionButtons1.ConfigBtCancelVisible = false;
    }

    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSelfInfor.aspx?AddNew=true&uId=0");
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;
        string deletStr = "select * from ReportHeader";
        try
        {
            for (int i = 0; i < ReportHeaderInfor.Length; i++)
            {
                if (ReportHeaderInfor[i] != null && ReportHeaderInfor[i].BeSelected == true)
                {
                    for (int j = 0; j < mydt.Rows.Count; j++)
                    {
                        if (mydt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if (mydt.Rows[j]["id"].ToString() == ReportHeaderInfor[i].ID)
                            {
                                mydt.Rows[j].Delete();
                                row++;
                                break;
                            }
                        }
                    }
                }
            }

            if (row == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('请至少选择一个！');</script>", false);
                return false;
            }
            //150527 ---------------------更新方式变更>>Start
            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("ReportHeader", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("ReportHeader", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
            }

            Response.Redirect(Request.Url.ToString(), true);
            //150527 ---------------------更新方式变更<<End
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
        
    }

    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (ReportHeaderInfor.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ReportHeaderInfor.Length; i++)
        {
            ReportHeaderInfor[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (ReportHeaderInfor.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ReportHeaderInfor.Length; i++)
        {
            ReportHeaderInfor[i].BeSelected = false;
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
}