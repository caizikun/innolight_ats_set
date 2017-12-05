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


public partial class ASPXReportHeaderSpecsList : BasePage
{
    private const string funcItemName = "表头格式";
    private DataIO pDataIO;
    private string ReportHeaderID = "";
    private ASCXReportHeaderSpecsList[] ControlList;
    private DataTable mydt = new DataTable();
    private DataTable mydtProcess = new DataTable();
    private DataTable globalSpecListDt = new DataTable();
    private int rowCount = 0;
    private string queryStr = "";
    private CommCtrl pCommCtrl = new CommCtrl();
    private string logTracingString = "";
    private string CheckedID = "";

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
        globalSpecListDt = pDataIO.GetDataTable("select * from GlobalSpecs", "GlobalSpecs");
        if (Request.QueryString["uId"] != null)
        {
            ReportHeaderID = Request.QueryString["uId"];
        }
        queryStr = "select * from ReportHeaderSpecs where PID=" + ReportHeaderID + " order by Seq";
        initPageInfo();
        ConfigOptionButtonsVisible();
    }

    void initPageInfo()
    {
        try
        {

            if (ReportHeaderID != null && ReportHeaderID.Length > 0)
            {
                if (getInfo(queryStr))
                {
                    //暂未导入权限部分
                }
            }
            else
            {
                OptionButtons1.Visible = false;
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>");
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public void bindData(DataTable dt, bool isEditMode = false)
    {
        if (IsPostBack)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label finLB = (Label)tableFC.FindControl("SEQ" + dt.Rows[i]["ID"].ToString().Trim());
                if (dt.Rows[i]["SEQ"].ToString().ToLower() != finLB.Text.ToLower()) //150529 防止出现多条记录被修改 而实际内容没变
                {
                    dt.Rows[i]["SEQ"] = finLB.Text;
                }
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "SEQ";
            dt = dv.ToTable();
        }

        DataRow[] drs = dt.Select("", "SEQ ASC");
        rowCount = drs.Length;

        if (rowCount==0)
        {
            ControlList = new ASCXReportHeaderSpecsList[1];
            for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (ASCXReportHeaderSpecsList)Page.LoadControl("~/Frame/TestReport/ReportHeaderSpecsList.ascx");
                ControlList[i].ContentTRVisible = false;         
                this.plhMain.Controls.Add(ControlList[i]);
             }
        } 
        else
        {
            ControlList = new ASCXReportHeaderSpecsList[rowCount];
            for (int i = 0; i < drs.Length; i++)
            {
                ControlList[i] = (ASCXReportHeaderSpecsList)Page.LoadControl("~/Frame/TestReport/ReportHeaderSpecsList.ascx");
                ControlList[i].ID = drs[i]["ID"].ToString();
                ControlList[i].ConfigLBPostBackURL = "~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsSelfInfor.aspx?uId=" + drs[i]["ID"];
                DataTable SpecDt = pDataIO.GetDataTable("select * from GlobalSpecs", "GlobalSpecs");

                string unit = "";
                unit = pCommCtrl.getDTColumnInfo(SpecDt, "Unit", "ID=" + drs[i]["SpecID"]);
                if (unit != "")
                {
                    ControlList[i].LiBItemNameText = pCommCtrl.getDTColumnInfo(SpecDt, "ItemName", "ID=" + drs[i]["SpecID"]) + "(" + unit + ")";
                }
                else 
                {
                    ControlList[i].LiBItemNameText = pCommCtrl.getDTColumnInfo(SpecDt, "ItemName", "ID=" + drs[i]["SpecID"]);
                }

                ControlList[i].LbShowNameText = drs[i]["ShowName"].ToString();
                ControlList[i].LbSEQText = (i + 1).ToString();

                if (CheckedID != "")
                {
                    if (ControlList[i].ID == CheckedID)
                    {
                        ControlList[i].BeSelected = true;
                    }
                }

                if (i >= 1)
                {
                    ControlList[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        ControlList[i].TrBackgroundColor = "#F2F2F2";
                    }
                }
                this.plhMain.Controls.Add(ControlList[i]);
            }
        }
    }

    public void SeqRecord()
    {
        HtmlTableCell cell = new HtmlTableCell();
        for (int i = 0; i < mydt.Rows.Count; i++)
        {
            Label Lb = new Label();
            Lb.Text = mydtProcess.Rows[i]["SEQ"].ToString().Trim();
            Lb.ID = "SEQ" + mydtProcess.Rows[i]["ID"].ToString().Trim();
            cell.Controls.Add(Lb);
            trFC.Cells.Add(cell);
        }
    }

    bool getInfo(string filterStr)
    {
        try
        {
            string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from ReportHeader where id = " + ReportHeaderID).ToString();

            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            mydt = pDataIO.GetDataTable(filterStr, "ReportHeaderSpecs");
            mydtProcess = mydt;
            SeqRecord();
            if (IsPostBack)
            {
                bindData(mydtProcess, true);
            }
            else
            {
                bindData(mydt);
            }
            return true;
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            return false;
        }
    }

    public bool EditData(object obj, string prameter)
    {
        Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsEdit.aspx?uId=" + ReportHeaderID, true);
        return true;
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
            }
            else
            { 
              ScriptManager.RegisterStartupScript(this.UpdatePanel1,typeof(UpdatePanel)," ","alert('仅有一项，无法排序！')",true); 
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
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                Label lb = (Label)tableFC.FindControl("SEQ" + Convert.ToString(mydt.Rows[i]["ID"]));
                if (lb != null)
                {
                    mydt.Rows[i]["SEQ"] = lb.Text;
                }
            }
            //150527 ---------------------更新方式变更>>Start
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
                Response.Redirect(Request.Url.ToString(), true);
                return true;
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
                return false;
            }
            //150527 ---------------------更新方式变更<<End
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
            Response.Redirect(Request.Url.ToString());
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
        if (!IsPostBack)
        {
            int myAccessCode = 0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();
            OptionButtons1.ConfigBtSaveVisible = false;
            OptionButtons1.ConfigBtCancelVisible = false;

            bool orderVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ReportHeader, CommCtrl.CheckAccess.MofifyReportHeader, myAccessCode);
            bool editVidible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ReportHeader, CommCtrl.CheckAccess.MofifyReportHeader, myAccessCode);

            OptionButtons1.ConfigBtAddVisible = false;
            OptionButtons1.ConfigBtDeleteVisible = false;
            OptionButtons1.ConfigBtEditVisible = editVidible;

            if (rowCount <= 1)
            {
                OptionButtons1.ConfigBtOrderVisible = false;
            }
            else
            {
                OptionButtons1.ConfigBtOrderVisible = orderVisible;
            }          
        }          
    }

    public string CheckSelected()
    {
        string id = "none";

        for (int i = 0; i < ControlList.Length; i++)
        {
            if (ControlList[i].BeSelected == true)
            {
                if (id == "none")
                {
                    id = ControlList[i].ID;
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
                    for (int i = 0; i < ControlList.Length; i++)
                    {
                        if (ControlList[i].ID == ss)
                        {
                            if (i <= 0)
                            {
                                return true;
                            }
                            else
                            {
                                Label finLB = (Label)tableFC.FindControl("SEQ" + ss);
                                Label finLB1 = (Label)tableFC.FindControl("SEQ" + ControlList[i - 1].ID);
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
               
                this.plhMain.Controls.Clear();
                CheckedID = ss;
                bindData(mydtProcess, true);
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
                for (int i = 0; i < ControlList.Length; i++)
                {
                    if (ControlList[i].ID == ss)
                    {
                        if (i >= (ControlList.Length - 1))
                        {
                            return true;
                        }
                        else
                        {
                            Label finLB = (Label)tableFC.FindControl("SEQ" + ss);
                            Label finLB1 = (Label)tableFC.FindControl("SEQ" + ControlList[i + 1].ID);
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

                this.plhMain.Controls.Clear();
                CheckedID = ss;
                bindData(mydtProcess, true);
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