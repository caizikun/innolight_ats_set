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

public partial class WebFiles_MCoefGroup_GlobalMCoefList : BasePage
{
    const string funcItemName = "模块系数列表";
    string queryStr = "";
    Frame_MCoefGroup_GlobalMCoefList[] ControlList;
    DataTable mydt = new DataTable();
    DataTable appNameDt = new DataTable();
    int rowCount = 0;
    DataIO pDataIO;
    string currTypeID = "-1";
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
        if (Request.QueryString["uId"] != null)
        {
            currTypeID = Request.QueryString["uId"];
        }
        queryStr = "select * from GlobalManufactureCoefficientsGroup where TypeID=" + currTypeID + " and IgnoreFlag = 'false'";

        initPageInfo();
        ConfigOptionButtonsVisible();
    }

    void initPageInfo()
    {
        try
        {
           
            if (getInfo(queryStr))
            {

            }
            else
            {
                OptionButtons1.Visible = false;
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>", false);
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    bool getInfo(string filterStr)
    {
        Table pTable = new Table();
        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from GlobalProductionType where id = " + currTypeID).ToString();

            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            //----------------------------
            mydt = pDataIO.GetDataTable(filterStr, "GlobalManufactureCoefficientsGroup");
            appNameDt = pDataIO.GetDataTable("select * from GlobalProductionType", "GlobalProductionType");
            DataRow[] drs = mydt.Select("");
            rowCount = drs.Length;
            if (mydt.Rows.Count == 0)
            {
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
            }
            if (rowCount==0)
            {
                ControlList = new Frame_MCoefGroup_GlobalMCoefList[1];
                for (int i = 0; i < ControlList.Length; i++)
                {
                    ControlList[i] = (Frame_MCoefGroup_GlobalMCoefList)Page.LoadControl("~/Frame/MCoefGroup/GlobalMCoefList.ascx");
                    ControlList[i].ContentTRVisible = false;
                    this.plhMain.Controls.Add(ControlList[i]);
                }
            } 
            else
            {
                ControlList = new Frame_MCoefGroup_GlobalMCoefList[rowCount];
                for (int i = 0; i < drs.Length; i++)
                {
                    ControlList[i] = (Frame_MCoefGroup_GlobalMCoefList)Page.LoadControl("~/Frame/MCoefGroup/GlobalMCoefList.ascx");
                    //for (int j = 0; j < appNameDt.Rows.Count; j++)
                    //{
                    //   ControlList[i].AddDdlTypeName(new ListItem(appNameDt.Rows[j]["ItemName"].ToString(),appNameDt.Rows[j]["ID"].ToString()));
                    //}

                    ControlList[i].ID = drs[i]["ID"].ToString();
                    ControlList[i].LnkItemNamePostBackUrl = "~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefInfo.aspx?uId=" + drs[i]["ID"];
                    ControlList[i].LnkItemName = drs[i]["ItemName"].ToString();
                    ControlList[i].txtDdlTypeName = MSAIDtoName(Convert.ToInt32(drs[i]["TypeID"]));
                    ControlList[i].TxtItemDescription = drs[i]["ItemDescription"].ToString();
                    ControlList[i].LnkViewParamsPostBackUrl = "~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + drs[i]["ID"];
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
       


            return true;
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            throw ex;
        }
    }

    public bool AddData(object sender, string prameter)
    {
        Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefInfo.aspx?AddNew=true&TypeID=" + currTypeID, true);
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;
        try
        {
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null && ControlList[i].chkIDMCoefChecked == true)
                {
                    for (int j = 0; j < mydt.Rows.Count; j++)
                    {
                        if (mydt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if (mydt.Rows[j]["id"].ToString() == ControlList[i].ID)
                            {
                                mydt.Rows[j]["IgnoreFlag"] = true;
                                row++;
                                break;
                            }
                        }
                    }
                }
            }

            if (row == 0)
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<script>alert('请至少选择一个！');</script>",false);
                return false;
            }

            //150527 ---------------------更新方式变更>>Start
            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("GlobalManufactureCoefficientsGroup", mydt, queryStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalManufactureCoefficientsGroup", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
            }
            //150527 ---------------------更新方式变更<<End
            //pDataIO.UpdateDataTable(queryStr, mydt);
            Response.Redirect(Request.Url.ToString(), true);
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool EditData(object obj, string prameter)
    {
        return true;
    }

    public bool updateData(object obj, string parameters)
    {
        return true;
    }

    public bool SaveData(object obj, string prameter)
    {
        return true;
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

    public bool CopyData(object obj, string prameter)
    {
        bool isSelected = false;
        int selectCount = 0;
        string copySourceID = "";
        try
        {
            for (int i = 0; i < ControlList.Length; i++)
            {
                Frame_MCoefGroup_GlobalMCoefList cb = (Frame_MCoefGroup_GlobalMCoefList)plhMain.FindControl(ControlList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        selectCount++;
                        isSelected = true;
                        copySourceID = cb.ID;
                    }
                }
                else
                {
                    Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false || selectCount > 1)
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请至少选择一个！');return false;</script>");                
                this.Page.RegisterStartupScript("", "<script>alert('只能选择一个！');</script>");
                //Response.Write("<script>alert('请至少选择一个！');</script>");
                return false;
            }

            Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/CopyMCoef.aspx?uId=" + currTypeID.Trim() + "&sourceID=" + copySourceID);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefGroup, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false; //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        if (mydt.Rows.Count<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
            OptionButtons1.ConfigBtCopyVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefGroup, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
            OptionButtons1.ConfigBtCopyVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefGroup, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        }
        
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (ControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ControlList.Length; i++)
        {
            ControlList[i].chkIDMCoefChecked = true;
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
            ControlList[i].chkIDMCoefChecked = false;
        }
    }
    public string MSAIDtoName(int msaid)
    {
        string msaname = "";
        try
        {
            {
                for (int i = 0; i < appNameDt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(appNameDt.Rows[i]["ID"]) == msaid)
                    {
                        msaname = appNameDt.Rows[i]["ItemName"].ToString().Trim();
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
}