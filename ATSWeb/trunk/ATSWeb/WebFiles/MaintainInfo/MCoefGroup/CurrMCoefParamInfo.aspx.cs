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
    const string funcItemName = "模块系数参数信息";
    Frame_MCoefGroup_GlobalMCoefParam[] ControlList;

    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string paramID = "-1";
    string MCoefID = "-1";
    bool AddNew = false;
    DataTable mydt = new DataTable();
    string queryStr = "";
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
                ControlList[0].TxtAmplify = drs[0]["AmplifyCoeff"].ToString();
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
                ControlList[0].TxtAmplify = "1";
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
                mydt.Rows[0]["AmplifyCoeff"] = ControlList[0].TxtAmplify;

                //150527 ---------------------更新方式变更>>Start
                int result = -1;
                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalManufactureCoefficients", mydt, queryStr, logTracingString, "ATS_V2");
                }
                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalManufactureCoefficients", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
                }      

                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + MCoefID, true);
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
                eqNewDr["AmplifyCoeff"] = ControlList[0].TxtAmplify;

                mydt.Rows.Add(eqNewDr);
                //150527 ---------------------更新方式变更>>Start
                int result = -1;
                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalManufactureCoefficients", mydt, queryStr, logTracingString, "ATS_V2");
                }
                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalManufactureCoefficients", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
                }      

                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + MCoefID, true);
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
                Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefParamsList.aspx?uId=" + MCoefID, true);
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
        OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefGroup, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtDeleteVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.DeleteMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtCancelVisible = false;
    }
}