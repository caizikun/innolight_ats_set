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


public partial class ReportHeaderSpecsEdit : BasePage
{
    private const string funcItemName = "表头格式选取";
    private DataIO pDataIO;
    private string ReportHeaderID = "";
    private ASCXReportHeaderSpecsEditList[] ControlList;
    private DataTable mydt = new DataTable();
    private DataTable globalSpecListDt = new DataTable();
    private int rowCount = 0;
    private string queryStr = "";
    private CommCtrl pCommCtrl = new CommCtrl();
    private string logTracingString = "";

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
        globalSpecListDt = pDataIO.GetDataTable("select * from GlobalSpecs order by ItemName", "GlobalSpecs");
        if (Request.QueryString["uId"] != null)
        {
            ReportHeaderID = Request.QueryString["uId"];
        }
        queryStr = "select * from ReportHeaderSpecs where PID=" + ReportHeaderID;
        mydt = pDataIO.GetDataTable(queryStr, "ReportHeaderSpecs");
        connectDataBase();
        ConfigOptionButtonsVisible();
    }

    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                rowCount = globalSpecListDt.Rows.Count;
                bindData();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "表头格式", Session["BlockType"].ToString(), pDataIO, out logTracingString);
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

    public void bindData()
    {
        ClearCurrenPage();

        if (rowCount == 0)
        {
            ControlList = new ASCXReportHeaderSpecsEditList[1];
            for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (ASCXReportHeaderSpecsEditList)Page.LoadControl("~/Frame/TestReport/ReportHeaderSpecsEditList.ascx");
                ControlList[i].ContentTRVisible = false;
                this.plhMain.Controls.Add(ControlList[i]);
            }
        }    
        else
        {
            ControlList = new ASCXReportHeaderSpecsEditList[rowCount];

            string sql = "select ReportHeaderSpecs.PID,ReportHeaderSpecs.SpecID, GlobalSpecs.ID,GlobalSpecs.ItemName from ReportHeaderSpecs,GlobalSpecs" +
                         " where ReportHeaderSpecs.SpecID = GlobalSpecs.ID and ReportHeaderSpecs.PID=" + ReportHeaderID + " order by GlobalSpecs.ItemName";

            DataTable dt = pDataIO.GetDataTable(sql, "SelectedSpecs");

            for (byte i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (ASCXReportHeaderSpecsEditList)Page.LoadControl("~/Frame/TestReport/ReportHeaderSpecsEditList.ascx");
                ControlList[i].ID = globalSpecListDt.Rows[i]["ID"].ToString().Trim();

                if (globalSpecListDt.Rows[i]["Unit"].ToString().Trim() != "")
                {
                    ControlList[i].LiBItemNameText = globalSpecListDt.Rows[i]["ItemName"].ToString().Trim() + "(" + globalSpecListDt.Rows[i]["Unit"].ToString().Trim() + ")";
                }
                else
                {
                    ControlList[i].LiBItemNameText = globalSpecListDt.Rows[i]["ItemName"].ToString().Trim();
                }

                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    if (dt.Rows[m]["ID"].ToString() == globalSpecListDt.Rows[i]["ID"].ToString())
                    {
                        ControlList[i].BeSelected = true;
                        ControlList[i].TrBackgroundColor = "#F2F2F2";
                        break;
                    }                
                }

                if (i >= 1)
                {
                    ControlList[i].LBTHTitleVisible(false);
                }
                this.plhMain.Controls.Add(ControlList[i]);
            }
        }
    }

    public bool SaveData(object obj, string prameter)
    {
        try
        {
            int addCount = 0;
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null && ControlList[i].TrBackgroundColor.Contains("F2F2F2") && ControlList[i].BeSelected == false)    //原有项需删除
                {
                    for (int j = 0; j < mydt.Rows.Count; j++)
                    {
                        if (mydt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if ((mydt.Rows[j]["SpecID"].ToString()).ToLower() == ControlList[i].ID.ToLower())
                            {
                                mydt.Rows[j].Delete();
                                         
                                break;
                            }
                        }
                    }
                }
                else if (ControlList[i] != null && ControlList[i].TrBackgroundColor.Contains("White") && ControlList[i].BeSelected == true)    //原没有项需新增
                {
                    long currEqID = pDataIO.GetLastInsertData("ReportHeaderSpecs") + 1;
                    DataRow eqNewDr = mydt.NewRow();
                    eqNewDr["ID"] = currEqID;
                    eqNewDr["PID"] = ReportHeaderID;
                    eqNewDr["SpecID"] = ControlList[i].ID;

                    string MaxSeq = pDataIO.getDbCmdExecuteScalar("select max(Seq) from ReportHeaderSpecs where PID =" + ReportHeaderID).ToString();
                    int myNewSeq = 1;
                    if (MaxSeq.Length > 0)
                    {
                        myNewSeq = Convert.ToInt32(MaxSeq) + 1;
                    }
                    eqNewDr["Seq"] = myNewSeq +addCount;
                    mydt.Rows.Add(eqNewDr);
                    addCount++;
                }
            }

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("ReportHeaderSpecs", mydt, queryStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("ReportHeaderSpecs", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
            }

            Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + ReportHeaderID);
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
            Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + ReportHeaderID);
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

        bool ModifyVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ReportHeader, CommCtrl.CheckAccess.MofifyReportHeader, myAccessCode);

        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;

        if (rowCount <= 0)
        {
            OptionButtons1.ConfigBtSaveVisible = false;
            OptionButtons1.ConfigBtCancelVisible = false;
        }
        else
        {
            OptionButtons1.ConfigBtSaveVisible = ModifyVisible;
            OptionButtons1.ConfigBtCancelVisible = ModifyVisible;
        }   
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (ControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ControlList.Length; i++)
        {
            ControlList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (ControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ControlList.Length; i++)
        {
            ControlList[i].BeSelected = false;
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