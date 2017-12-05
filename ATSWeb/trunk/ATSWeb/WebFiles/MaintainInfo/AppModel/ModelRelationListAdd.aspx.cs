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


public partial class ModelRelationListAdd : BasePage
{
    private const string funcItemName = "新增冲突模型";
    private DataIO pDataIO;
    private string ModelID = "";
    private ASCXModelRelationAddList[] ControlList;
    private DataTable mydt = new DataTable();
    private DataTable ModelListDt = new DataTable();
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
        if (Request.QueryString["ModelId"] != null)
        {
            ModelID = Request.QueryString["ModelId"];
        }

        DataTable dt = pDataIO.GetDataTable("select * from GlobalAllTestModelList where ID=" + ModelID, "GlobalTestModelList");
        string modelWeigth = "0";
        if (dt.Rows.Count == 1)
        {
            modelWeigth = dt.Rows[0]["ItemPriority"].ToString();
        }
        else
        {
            modelWeigth = "0";
        }

        queryStr = "select * from GlobalTestModelRelation where ConflictID=" + ModelID;
        mydt = pDataIO.GetDataTable(queryStr, "GlobalTestModelRelation");

        ModelListDt = pDataIO.GetDataTable("select * from GlobalAllTestModelList where GlobalAllTestModelList.ItemPriority < " + modelWeigth + " and GlobalAllTestModelList.ItemPriority != 0 "
            + "and ID not in (select PID from GlobalTestModelRelation where ConflictID = " + ModelID + ")", "GlobalAllTestModelList");

        connectDataBase();
        ConfigOptionButtonsVisible();
    }

    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                rowCount = ModelListDt.Rows.Count;
                bindData();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "测试模型关系", Session["BlockType"].ToString(), pDataIO, out logTracingString);
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
            ControlList = new ASCXModelRelationAddList[1];
            for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (ASCXModelRelationAddList)Page.LoadControl("~/Frame/APPModel/ModelRelationAddList.ascx");              
                ControlList[i].ContentTRVisible = false;
                this.plhMain.Controls.Add(ControlList[i]);
            }
        }    
        else
        {
            ControlList = new ASCXModelRelationAddList[rowCount];

            for (byte i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (ASCXModelRelationAddList)Page.LoadControl("~/Frame/APPModel/ModelRelationAddList.ascx");
                ControlList[i].ID = ModelListDt.Rows[i]["ID"].ToString().Trim();
                ControlList[i].LiBItemNameText = ModelListDt.Rows[i]["ShowName"].ToString().Trim();

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
                if (ControlList[i] != null && ControlList[i].BeSelected == true)    //原没有项需新增
                {
                    long currEqID = pDataIO.GetLastInsertData("GlobalTestModelRelation") + 1;
                    DataRow eqNewDr = mydt.NewRow();
                    eqNewDr["ID"] = currEqID;
                    eqNewDr["PID"] = ModelListDt.Rows[i]["ID"].ToString();
                    eqNewDr["ConflictID"] = ModelID;

                    mydt.Rows.Add(eqNewDr);
                    addCount++;
                }
            }

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("GlobalTestModelRelation", mydt, queryStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalTestModelRelation", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
            }

            Response.Redirect("~/WebFiles/MaintainInfo/AppModel/ModelRelationList.aspx?uId=" + ModelID);
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
            Response.Redirect("~/WebFiles/MaintainInfo/AppModel/ModelRelationList.aspx?uId=" + ModelID);
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

        bool ModifyVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.MofifyAppModel, myAccessCode);

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