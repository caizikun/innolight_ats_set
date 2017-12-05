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

public partial class ASPXReportHeaderSpecsSelfInfor : BasePage
{
    const string funcItemName = "表头格式信息";
    Frame_ReportHeaderSpecsInfo [] ControlList;
    private string logTracingString = "";
    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string HeaderSpecID = "-1";
    string HeaderID = "-1";
    bool AddNew = false;
    DataTable mydt = new DataTable();
    string queryStr = "";

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
            HeaderSpecID = Request.QueryString["uId"];
        }

        if (Request.QueryString["HearderID"] != null)
        {
            HeaderID = Request.QueryString["HearderID"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            HeaderSpecID = "-1";
        }
        queryStr = "select * from ReportHeaderSpecs where id =" + HeaderSpecID;
        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select GlobalSpecs.ItemName from GlobalSpecs,ReportHeaderSpecs where ReportHeaderSpecs.SpecID = GlobalSpecs.ID  and ReportHeaderSpecs.id = " + HeaderSpecID).ToString();
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
            if (HeaderSpecID != null && HeaderSpecID.Length > 0)
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
            mydt = pDataIO.GetDataTable(filterStr, "ReportHeaderSpecs");

            DataRow[] drs = mydt.Select("");
            ControlList = new Frame_ReportHeaderSpecsInfo[1];
            ControlList[0] = (Frame_ReportHeaderSpecsInfo)Page.LoadControl("~/Frame/TestReport/ReportHeaderSpecsInfo.ascx");
            ControlList[0].SetColum1TextState(isEditState);
            if (drs.Length == 1)
            {
                //ControlList[0].ID = drs[0]["ID"].ToString();
                ControlList[0].ClearDropDownList();
                ConfigSpecDD(ControlList[0]);
                ControlList[0].ConfigSeletedSpec = 0;
                ControlList[0].txtShowNameConfig = drs[0]["ShowName"].ToString();
            }
            else if (isEditState && drs.Length ==0)
            {
                ControlList[0].ID = "Header_New";
                ControlList[0].ClearDropDownList();
                ConfigSpecDD(ControlList[0]);
                ControlList[0].ConfigSeletedSpec = 0;
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

    public bool ConfigSpecDD(Frame_ReportHeaderSpecsInfo input)
    {
        try
        {
            int Index;
            DataTable AllSpecdt = new DataTable();
            DataTable HeaderSpecdt = new DataTable();

            if (HeaderSpecID != "-1")
            {
                string SpecItem = pDataIO.getDbCmdExecuteScalar("select GlobalSpecs.ItemName from GlobalSpecs,ReportHeaderSpecs where ReportHeaderSpecs.SpecID = GlobalSpecs.ID  and ReportHeaderSpecs.id = " + HeaderSpecID + " order by GlobalSpecs.ItemName").ToString();
                string SpecUnit = pDataIO.getDbCmdExecuteScalar("select GlobalSpecs.Unit from GlobalSpecs,ReportHeaderSpecs where ReportHeaderSpecs.SpecID = GlobalSpecs.ID  and ReportHeaderSpecs.id = " + HeaderSpecID + " order by GlobalSpecs.ItemName").ToString();
                string SpecID = mydt.Rows[0]["SpecID"].ToString();

                if (SpecUnit != "")
                {
                    input.InsertColum1Text(0, new ListItem(SpecItem + "(" + SpecUnit + ")"));
                }
                else
                {
                    input.InsertColum1Text(0, new ListItem(SpecItem));
                }
                
                Index = 1;

                AllSpecdt = pDataIO.GetDataTable("select * from GlobalSpecs where ID != " + SpecID + " order by GlobalSpecs.ItemName", "GlobalSpecs");
                HeaderSpecdt = pDataIO.GetDataTable("select * from ReportHeaderSpecs where PID = " + mydt.Rows[0]["PID"] + " and SpecID !=" + SpecID, "ReportHeaderSpecsDD");
            }
            else
            {
                Index = 0;

                AllSpecdt = pDataIO.GetDataTable("select * from GlobalSpecs order by GlobalSpecs.ItemName", "GlobalSpecs");
                HeaderSpecdt = pDataIO.GetDataTable("select * from ReportHeaderSpecs where PID = " + HeaderID, "ReportHeaderSpecsDD");
            }
           
            bool haveExitFlag = false;
            for (int i = 0; i < AllSpecdt.Rows.Count; i++)
            {
                for (int j = 0; j < HeaderSpecdt.Rows.Count; j++)
                {
                    if (AllSpecdt.Rows[i]["ID"].ToString() == HeaderSpecdt.Rows[j]["SpecID"].ToString())
                    {
                        haveExitFlag = true;
                        break;
                    }
                    else
                    {
                        haveExitFlag = false;
                    }
                }

                if (haveExitFlag == false)
                {
                    if (AllSpecdt.Rows[i]["Unit"].ToString() != "")
                    {
                        input.InsertColum1Text(Index, new ListItem(AllSpecdt.Rows[i]["ItemName"].ToString() + "(" + AllSpecdt.Rows[i]["Unit"].ToString() + ")"));
                    }
                    else
                    {
                        input.InsertColum1Text(Index, new ListItem(AllSpecdt.Rows[i]["ItemName"].ToString()));
                    }
                    Index++;
                }
            }

            if (ControlList[0].Colum1TextConfig == "")
            {
                ControlList[0].SetLabel1Visible = "block";
            }

            return true;
        }
        catch (System.Exception ex)
        {
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
            if (ControlList[0].Colum1TextConfig == "")
            {
                OptionButtons1.ConfigBtSaveVisible = false;
            }
            else
            {
                OptionButtons1.ConfigBtSaveVisible = true;
            }
            
            OptionButtons1.ConfigBtCancelVisible = true;            
            OptionButtons1.ConfigBtEditVisible = false;
            for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i].SetColum1TextState(true);
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
            if (ControlList[0].Colum1TextConfig == "")
            {
                if (mydt.Rows.Count == 0)
                {
                    Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + HeaderID, true);
                }
                else
                {
                    Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + mydt.Rows[0]["PID"], true);
                }
            }
            else
            {
                string SpecName = "";
                string SpecUnit = "";
                string SpecId = "";

                if (mydt.Rows.Count == 1)
                {
                    #region
                    if (ControlList[0].Colum1TextConfig.Contains("("))
                    {
                        SpecName = ControlList[0].Colum1TextConfig.Split('(')[0];
                        int length = ControlList[0].Colum1TextConfig.Split('(')[1].Length;
                        SpecUnit = ControlList[0].Colum1TextConfig.Split('(')[1].Substring(0, length - 1);
                        SpecId = pDataIO.getDbCmdExecuteScalar("select ID from GlobalSpecs where ItemName = '" + SpecName + "' and Unit = '" + SpecUnit + "'").ToString();
                    }
                    else
                    {
                        SpecName = ControlList[0].Colum1TextConfig;
                        SpecId = pDataIO.getDbCmdExecuteScalar("select ID from GlobalSpecs where ItemName = '" + SpecName + "'").ToString();
                    }

                    mydt.Rows[0]["SpecID"] = SpecId;
                    mydt.Rows[0]["ShowName"] = ControlList[0].txtShowNameConfig;

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
                        Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + mydt.Rows[0]["PID"], true);
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

                    long currEqID = pDataIO.GetLastInsertData("ReportHeaderSpecs") + 1;
                    DataRow eqNewDr = mydt.NewRow();
                    eqNewDr["ID"] = currEqID;
                    eqNewDr["PID"] = HeaderID;

                    if (ControlList[0].Colum1TextConfig.Contains("("))
                    {
                        SpecName = ControlList[0].Colum1TextConfig.Split('(')[0];
                        int length = ControlList[0].Colum1TextConfig.Split('(')[1].Length;
                        SpecUnit = ControlList[0].Colum1TextConfig.Split('(')[1].Substring(0, length - 1);
                        SpecId = pDataIO.getDbCmdExecuteScalar("select ID from GlobalSpecs where ItemName = '" + SpecName + "' and Unit = '" + SpecUnit + "'").ToString();
                    }
                    else
                    {
                        SpecName = ControlList[0].Colum1TextConfig;
                        SpecId = pDataIO.getDbCmdExecuteScalar("select ID from GlobalSpecs where ItemName = '" + SpecName + "'").ToString();
                    }
                    eqNewDr["SpecID"] = SpecId;
                    string MaxSeq = pDataIO.getDbCmdExecuteScalar("select max(Seq) from ReportHeaderSpecs where PID =" + HeaderID).ToString();
                    int myNewSeq = 1;
                    if (MaxSeq.Length > 0)
                    {
                        myNewSeq = Convert.ToInt32(MaxSeq) + 1;
                    }
                    eqNewDr["Seq"] = myNewSeq;
                    eqNewDr["ShowName"] = ControlList[0].txtShowNameConfig;
                    mydt.Rows.Add(eqNewDr);

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
                        Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + HeaderID, true);
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
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('Error!当前项已被删除!')</Script>", false);
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

    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/MaintainInfo/ReportHeader/ReportHeaderSpecsList.aspx?uId=" + HeaderID, true);
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
        OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ReportHeader, CommCtrl.CheckAccess.MofifyReportHeader, myAccessCode);
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = false;
    }
}