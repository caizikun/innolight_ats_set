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
    const string funcItemName = "MCoefList";
    string queryStr = "";
    Frame_MCoefGroup_GlobalMCoefList[] ControlList;
    DataTable mydt = new DataTable();
    DataTable appNameDt = new DataTable();
    int rowCount = 0;
    DataIO pDataIO;
    string currTypeID = "-1";
    private string logTracingString = "";
    public WebFiles_MCoefGroup_GlobalMCoefList()
    {
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
        SetSessionBlockType(4);
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

            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
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
                    ControlList[i].LnkItemNamePostBackUrl = "~/WebFiles/MCoefGroup/GlobalMCoefInfo.aspx?uId=" + drs[i]["ID"];
                    ControlList[i].LnkItemName = drs[i]["ItemName"].ToString();
                    ControlList[i].txtDdlTypeName = MSAIDtoName(Convert.ToInt32(drs[i]["TypeID"]));
                    ControlList[i].TxtItemDescription = drs[i]["ItemDescription"].ToString();
                    ControlList[i].LnkViewParamsPostBackUrl = "~/WebFiles/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + drs[i]["ID"];
                    if (i >= 1)
                    {
                        ControlList[i].EnableTH1Visible = false;
                        ControlList[i].EnableTH2Visible = false;
                        ControlList[i].EnableTH3Visible = false;
                        ControlList[i].EnableTH4Visible = false;
                        ControlList[i].LBTHTitleVisible(false);
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
        Response.Redirect("~/WebFiles/MCoefGroup/GlobalMCoefInfo.aspx?AddNew=true&TypeID=" + currTypeID, true);
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
                ClientScript.RegisterStartupScript(GetType(),"Message", "<script>alert('Did not choose any one！');</script>",false);
                return false;
            }

            //150527 ---------------------更新方式变更>>Start
            int result = pDataIO.UpdateWithProc("GlobalManufactureCoefficientsGroup", mydt, queryStr, logTracingString);
            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("Update data fail!");
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


    public void ConfigOptionButtonsVisible()
    {
        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.AddMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false; //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        if (mydt.Rows.Count<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.DeleteMCoefInfo, myAccessCode);
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