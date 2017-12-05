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

public partial class WebFiles_MCoefGroup_CurrMCoefParamInfo : BasePage
{
    const string funcItemName = "MCoefParamInfo";
    Frame_MCoefGroup_GlobalMCoefParam[] ControlList;

    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string paramID = "-1";
    string MCoefID = "-1";
    bool AddNew = false;
    DataTable mydt = new DataTable();
    string queryStr = "";
    private string logTracingString = "";
    public WebFiles_MCoefGroup_CurrMCoefParamInfo()
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
            paramID = Request.QueryString["uId"];
        }

        if (Request.QueryString["MCoefID"] != null)
        {
            MCoefID = Request.QueryString["MCoefID"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            paramID = "-1";
        }
        queryStr = "select * from GlobalManufactureCoefficients where id=" + paramID;
        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalManufactureCoefficients where id = " + paramID).ToString();
        if (AddNew)
        {
            parentItem = "AddNewItem";
        }
        Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
        this.plhNavi.Controls.Add(myCtrl);
    }

    void initPageInfo()
    {
        try
        {
            ConfigOptionButtonsVisible();
            if (paramID != null && paramID.Length > 0)
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
        Table pTable = new Table();
        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            pTable.ID = "ListTable";
            //----------------------------
            mydt = pDataIO.GetDataTable(filterStr, "GlobalManufactureCoefficients");

            DataRow[] drs = mydt.Select("");
            ControlList = new Frame_MCoefGroup_GlobalMCoefParam[1];
            ControlList[0] = (Frame_MCoefGroup_GlobalMCoefParam)Page.LoadControl("~/Frame/MCoefGroup/GlobalMCoefParam.ascx");
            ControlList[0].SetMCoefParamsEnableState(isEditState);
            if (drs.Length == 1)
            {
                MCoefID = drs[0]["PID"].ToString();
                ControlList[0].ID = "ParamID_" + drs[0]["ID"].ToString();
                ControlList[0].TxtItemName = drs[0]["ItemName"].ToString();
                ControlList[0].TxtItemType = drs[0]["ItemType"].ToString();
                ControlList[0].TxtDdlChannel = drs[0]["Channel"].ToString();
                ControlList[0].TxtPage = drs[0]["Page"].ToString();
                ControlList[0].TxtStartAddress = drs[0]["StartAddress"].ToString();
                ControlList[0].TxtLength = drs[0]["Length"].ToString();
                ControlList[0].TxtDdlFormat = drs[0]["Format"].ToString();
            }
            else if (isEditState && drs.Length == 0)
            {
                ControlList[0].ID = "Param_New";
                ControlList[0].TxtItemName = "NewParam";
                ControlList[0].TxtItemType = "";
                ControlList[0].TxtDdlChannel = "0";
                ControlList[0].TxtPage = "0";
                ControlList[0].TxtStartAddress = "0";
                ControlList[0].TxtLength = "1";
                ControlList[0].TxtDdlFormat = "0";
                EditData("", "");
            }
            TableCell tc = new TableCell();
            tc.Controls.Add(ControlList[0]);
            TableRow tr = new TableRow();
            tr.Controls.Add(tc);
            pTable.Rows.Add(tr);


            this.plhMain.Controls.Add(pTable);
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
                if (ControlList[i] != null)
                {
                    ControlList[i].SetMCoefParamsEnableState(true);
                }
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
                mydt.Rows[0]["ItemType"] = ControlList[0].TxtItemType;
                mydt.Rows[0]["Channel"] = ControlList[0].TxtDdlChannel;
                mydt.Rows[0]["Page"] = ControlList[0].TxtPage;
                mydt.Rows[0]["StartAddress"] = ControlList[0].TxtStartAddress;
                mydt.Rows[0]["Length"] = ControlList[0].TxtLength;
                mydt.Rows[0]["Format"] = ControlList[0].TxtDdlFormat;

                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("GlobalManufactureCoefficients", mydt, queryStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + MCoefID, true);
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End

                #endregion
            }
            else if (AddNew && mydt.Rows.Count == 0)
            {
                #region //New

                long currParamID = pDataIO.GetLastInsertData("GlobalManufactureCoefficients") + 1;
                DataRow eqNewDr = mydt.NewRow();
                eqNewDr["ID"] = currParamID;
                eqNewDr["PID"] = MCoefID;
                eqNewDr["ItemName"] = ControlList[0].TxtItemName;
                eqNewDr["ItemType"] = ControlList[0].TxtItemType;
                eqNewDr["Channel"] = ControlList[0].TxtDdlChannel;
                eqNewDr["Page"] = ControlList[0].TxtPage;
                eqNewDr["StartAddress"] = ControlList[0].TxtStartAddress;
                eqNewDr["Length"] = ControlList[0].TxtLength;
                eqNewDr["Format"] = ControlList[0].TxtDdlFormat;

                mydt.Rows.Add(eqNewDr);
                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("GlobalManufactureCoefficients", mydt, queryStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + MCoefID, true);
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End
                #endregion
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any Result,Current item has been deleted!~')</Script>",false);
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
                Response.Redirect("~/WebFiles/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + MCoefID, true);
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
        OptionButtons1.ConfigBtAddVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.AddMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtDeleteVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.DeleteMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtCancelVisible = false;
    }
}