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

public partial class WebFiles_GlobalSpecs_GlobalSpecsInfo : BasePage
{
    const string funcItemName = "GlobalSpecsInfo";
    Frame_GlobalSpecs_GlobalSpecsInfo [] ControlList;
    private string logTracingString = "";
    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string EquipID = "-1";
    bool AddNew = false;
    DataTable mydt = new DataTable();
    string queryStr = "";


    public WebFiles_GlobalSpecs_GlobalSpecsInfo()
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
        SetSessionBlockType(9);
        if (Request.QueryString["uId"] != null)
        {
            EquipID = Request.QueryString["uId"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            EquipID = "-1";
        }
        queryStr = "select * from GlobalSpecs where id =" + EquipID;
        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalSpecs where id = " + EquipID).ToString();
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
            if (EquipID != null && EquipID.Length > 0)
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
            mydt = pDataIO.GetDataTable(filterStr, "GlobalSpecs");

            DataRow[] drs = mydt.Select("");
            ControlList = new Frame_GlobalSpecs_GlobalSpecsInfo[1];
            ControlList[0] = (Frame_GlobalSpecs_GlobalSpecsInfo)Page.LoadControl("~/Frame/GlobalSpecs/GlobalSpecsInfo.ascx");
            ControlList[0].SetEquipEnableState(isEditState);
            if (drs.Length == 1)
            {
                ControlList[0].ID = drs[0]["ID"].ToString();
                ControlList[0].TxtItemName = drs[0]["ItemName"].ToString();
                ControlList[0].TxtUnit = drs[0]["Unit"].ToString();
                ControlList[0].TxtItemDescription = drs[0]["ItemDescription"].ToString();
            }
            else if (isEditState && drs.Length ==0)
            {
                ControlList[0].ID = "Specs_New";
                ControlList[0].TxtItemName = "NewName";
                ControlList[0].TxtUnit = "NewUnit";
                ControlList[0].TxtItemDescription = "Description";
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
                mydt.Rows[0]["Unit"] = ControlList[0].TxtUnit;
                mydt.Rows[0]["ItemName"] = ControlList[0].TxtItemName;
                mydt.Rows[0]["ItemDescription"] = ControlList[0].TxtItemDescription;
                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("GlobalSpecs", mydt, queryStr, logTracingString);
                if (result > 0)
                {                    
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/GlobalSpecs/GlobalSpecsList.aspx?BlockType=" + Session["BlockType"], true);
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

                long currEqID = pDataIO.GetLastInsertData("GlobalSpecs") + 1;
                DataRow eqNewDr = mydt.NewRow();
                eqNewDr["ID"] = currEqID;
                eqNewDr["Unit"] = ControlList[0].TxtUnit;
                eqNewDr["ItemName"] = ControlList[0].TxtItemName;
                eqNewDr["ItemDescription"] = ControlList[0].TxtItemDescription;

                mydt.Rows.Add(eqNewDr);

                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("GlobalSpecs", mydt, queryStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/GlobalSpecs/GlobalSpecsList.aspx?BlockType=" + Session["BlockType"], true);
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
                Response.Redirect("~/WebFiles/GlobalSpecs/GlobalSpecsList.aspx?BlockType=" + Session["BlockType"], true);
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
        OptionButtons1.ConfigBtEditVisible =mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ConfigSpecs, CommCtrl.CheckAccess.EditConfigSpecs, myAccessCode);
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = false;
    }
}