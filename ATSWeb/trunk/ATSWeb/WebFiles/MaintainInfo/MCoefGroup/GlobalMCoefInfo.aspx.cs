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

public partial class WebFiles_MCoefGroup_GlobalMCoefInfo : BasePage
{
    const string funcItemName = "模块系数信息";
    Frame_MCoefGroup_GlobalMCoefInfo[] ControlList;
    int rowCount = 0;

    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string TypeID = "-1";
    string MCoefID = "";
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
            MCoefID = Request.QueryString["uId"];
        }

        if (Request.QueryString["TypeID"] != null)
        {
            TypeID = Request.QueryString["TypeID"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            MCoefID = "-1";
        }
        queryStr = "select * from GlobalManufactureCoefficientsGroup where id=" + MCoefID;
        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalManufactureCoefficientsGroup where id = " + MCoefID).ToString();
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
            if (MCoefID != null && MCoefID.Length > 0)
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
        //Table pTable = new Table();
        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            //pTable.ID = "ListTable";
            
            //----------------------------
            mydt = pDataIO.GetDataTable(filterStr, "GlobalManufactureCoefficientsGroup");
            DataTable appNameDt = new DataTable();
            if (AddNew)
            {
                appNameDt = pDataIO.GetDataTable("select * from GlobalProductionType  where IgnoreFlag='false'", "GlobalProductionType");
            }
            else
            {
                if (IsPostBack)
                {
                    appNameDt = pDataIO.GetDataTable("select * from GlobalProductionType  where IgnoreFlag='false'", "GlobalProductionType");
                }
                else
                {
                    appNameDt = pDataIO.GetDataTable("select * from GlobalProductionType", "GlobalProductionType");
                }

            }
           

            DataRow[] drs = mydt.Select("");
            ControlList = new Frame_MCoefGroup_GlobalMCoefInfo[1];
            ControlList[0] = (Frame_MCoefGroup_GlobalMCoefInfo)Page.LoadControl("~/Frame/MCoefGroup/GlobalMCoefInfo.ascx");
            this.MCoefSelfInfor.Controls.Add(ControlList[0]);

            for (int j = 0; j < appNameDt.Rows.Count; j++)
            {
                ControlList[0].AddDdlTypeName(new ListItem(appNameDt.Rows[j]["ItemName"].ToString(), appNameDt.Rows[j]["ID"].ToString()));
            }
            
            ControlList[0].SetMCoefEnableState(isEditState);           
            if (drs.Length == 1)
            {
                TypeID = drs[0]["TypeID"].ToString();
                //ControlList[0].ID = drs[0]["ID"].ToString();               
                ControlList[0].TxtItemName = drs[0]["ItemName"].ToString();
                for (int j = 0; j < ControlList[0].DdlTypeName.Items.Count; j++)
                {
                    if (ControlList[0].DdlTypeName.Items[j].Value.ToString() == drs[0]["TypeID"].ToString())
                    {
                        //ControlList[0].DdlTypeName.SelectedValue = drs[0]["TypeID"].ToString();
                        ControlList[0].DdlTypeName.SelectedIndex = j;
                        break;
                    }
                }
                ControlList[0].TxtItemDescription = drs[0]["ItemDescription"].ToString();
                ControlList[0].DdlIgnoreFlag = drs[0]["IgnoreFlag"].ToString().ToLower();
            }
            else if (isEditState && drs.Length ==0)
            {
                ControlList[0].ID = "MCoef_New";
                ControlList[0].TxtItemName = "NewName";
                ControlList[0].DdlTypeName.SelectedValue = TypeID;
                ControlList[0].TxtItemDescription = "Description";
                ControlList[0].DdlIgnoreFlag = false.ToString();
                EditData("", "");
            }
            //TableCell tc = new TableCell();
            //tc.Controls.Add(ControlList[0]);
            //TableRow tr = new TableRow();
            //tr.Controls.Add(tc);
            //pTable.Rows.Add(tr);
            
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
                ControlList[i].SetMCoefEnableState(true);
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
                #region //已经获取到当前信息
                mydt.Rows[0]["TypeID"] = ControlList[0].DdlTypeName.SelectedValue;
                mydt.Rows[0]["ItemName"] = ControlList[0].TxtItemName;
                mydt.Rows[0]["ItemDescription"] = ControlList[0].TxtItemDescription;
                mydt.Rows[0]["IgnoreFlag"] = ControlList[0].DdlIgnoreFlag;
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
                    Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefList.aspx?uId=" + TypeID, true);
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

                long currEqID = pDataIO.GetLastInsertData("GlobalManufactureCoefficientsGroup") + 1;
                DataRow eqNewDr = mydt.NewRow();
                eqNewDr["ID"] = currEqID;
                eqNewDr["TypeID"] = ControlList[0].DdlTypeName.SelectedValue;
                eqNewDr["ItemName"] = ControlList[0].TxtItemName;
                eqNewDr["ItemDescription"] = ControlList[0].TxtItemDescription;
                eqNewDr["IgnoreFlag"] = false;
                mydt.Rows.Add(eqNewDr);
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
                    MCoefID = result.ToString();
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/CurrMCoefParamInfo.aspx?AddNew=true&MCoefID=" + MCoefID, true);
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
                Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefList.aspx?uId=" + TypeID, true);
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