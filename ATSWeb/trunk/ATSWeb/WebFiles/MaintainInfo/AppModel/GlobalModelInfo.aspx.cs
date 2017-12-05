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

public partial class WebFiles_APPModel_GlobalModelInfo : BasePage
{
    const string funcItemName = "测试模型信息(维护)";
    Frame_APPModel_GlobalModelInfo[] ControlList;

    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string ModelID = "-1";
    bool AddNew = false;
    DataTable mydt = new DataTable();
    string queryStr = "";
    private string logTracingString = "";
    int modelWeight = 0;

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
            ModelID = Request.QueryString["uId"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            ModelID = "-1";
        }
        queryStr = "select * from GlobalAllTestModelList where id=" + ModelID;
        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select showName from GlobalAllTestModelList where id = " + ModelID).ToString();
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
            if (ModelID != null && ModelID.Length > 0)
            {
                createNavilnks();
                getInfo(queryStr, AddNew);
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

    bool getInfo(string filterStr, bool isEditState = false)
    {
        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            mydt = pDataIO.GetDataTable(filterStr, "GlobalAllTestModelList");
            DataTable appNameDt = pDataIO.GetDataTable("select * from GlobalAllAppModelList", "GlobalAllAppModelList");

            DataRow[] drs = mydt.Select("");
            ControlList = new Frame_APPModel_GlobalModelInfo[1];
            ControlList[0] = (Frame_APPModel_GlobalModelInfo)Page.LoadControl("~/Frame/APPModel/GlobalModelInfo.ascx");
            for (int j = 0; j < appNameDt.Rows.Count; j++)
            {
                ControlList[0].AddDdlAppName(new ListItem(appNameDt.Rows[j]["ItemName"].ToString(), appNameDt.Rows[j]["ID"].ToString()));
            }

            ControlList[0].SetModelEnableState(isEditState);           
            if (drs.Length == 1)
            {
                //ControlList[0].ID = drs[0]["ID"].ToString();               
                ControlList[0].TxtItemName = drs[0]["ItemName"].ToString();
                ControlList[0].TxtShowName = drs[0]["ShowName"].ToString();
                for (int j = 0; j < ControlList[0].DdlAppName.Items.Count; j++)
                {
                    if (ControlList[0].DdlAppName.Items[j].Value.ToString() == drs[0]["PID"].ToString())
                    {
                        ControlList[0].DdlAppName.SelectedValue = drs[0]["PID"].ToString();
                        break;
                    }
                }
                ControlList[0].TxtItemDescription = drs[0]["ItemDescription"].ToString();
                ControlList[0].TxtModelWeight = drs[0]["ItemPriority"].ToString();
                modelWeight = Convert.ToInt32(drs[0]["ItemPriority"]);
            }
            else if (isEditState && drs.Length ==0)
            {
                ControlList[0].ID = "Model_New";
                ControlList[0].TxtItemName = "NewModelName";
                ControlList[0].TxtShowName = "NewShowName";
                ControlList[0].DdlAppName.SelectedIndex = -1;
                ControlList[0].TxtItemDescription = "Description";
                ControlList[0].TxtModelWeight = "0";
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
                ControlList[i].SetModelEnableState(true);
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
                mydt.Rows[0]["PID"] = ControlList[0].DdlAppName.SelectedValue;
                mydt.Rows[0]["ItemName"] = ControlList[0].TxtItemName;
                mydt.Rows[0]["ShowName"] = ControlList[0].TxtShowName;
                mydt.Rows[0]["ItemDescription"] = ControlList[0].TxtItemDescription;
                mydt.Rows[0]["ItemPriority"] = ControlList[0].TxtModelWeight;

                //150527 ---------------------更新方式变更>>Start
                int result = -1;
                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalAllTestModelList", mydt, queryStr, logTracingString, "ATS_V2");
                }
                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalAllTestModelList", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
                }      

                if (result > 0)
                {
                    mydt.AcceptChanges();                   
                }
                else
                {
                    pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End


                //判断权限是否被修改: 如果修改为0，需删除其在关系表中的记录；
                //                    如果修改但不为0，检查关系表，确保PID对应model的权限 < ConflictID的，存在ID互换和记录删除两种情况
                if (ControlList[0].TxtModelWeight.ToString().Trim() == "0")
                {
                    string sql = "select * from GlobalTestModelRelation where PID=" + ModelID + " or ConflictID=" + ModelID;
                    DataTable dt = pDataIO.GetDataTable(sql, "ModelRelation");
                    DataTable totalDt = pDataIO.GetDataTable("select * from GlobalTestModelRelation", "GlobalTestModelRelation");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < totalDt.Rows.Count; j++)
                        {
                            if (totalDt.Rows[j].RowState != DataRowState.Deleted)
                            {
                                if (totalDt.Rows[j]["id"].ToString() == dt.Rows[i]["id"].ToString())
                                {
                                    totalDt.Rows[j].Delete();
                                    break;
                                }
                            }
                        }
                    }

                    int result2 = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result2 = pDataIO.UpdateWithProc("GlobalTestModelRelation", totalDt, "select * from GlobalTestModelRelation", logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result2 = pDataIO.UpdateWithProc("GlobalTestModelRelation", totalDt, "select * from GlobalTestModelRelation", logTracingString, "ATS_VXDEBUG");
                    }      
                
                    if (result2 > 0)
                    {
                        totalDt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!");
                    }
                }

                if (ControlList[0].TxtModelWeight.ToString().Trim() != modelWeight.ToString())
                {
                    string sql = "select GlobalTestModelRelation.*,GlobalAllTestModelList.ItemPriority from GlobalTestModelRelation,GlobalAllTestModelList " +
                    "where (GlobalTestModelRelation.PID=" + ModelID + " or GlobalTestModelRelation.ConflictID=" + ModelID + ") " +
                    "and (GlobalAllTestModelList.ID = GlobalTestModelRelation.PID or GlobalAllTestModelList.ID = GlobalTestModelRelation.ConflictID) " +
                    "and GlobalAllTestModelList.ID !=" + ModelID + " order by GlobalTestModelRelation.ID";

                    DataTable dt = pDataIO.GetDataTable(sql, "ModelRelation");

                    string Sql = "select * from GlobalTestModelRelation " +
                    "where GlobalTestModelRelation.PID=" + ModelID + " or GlobalTestModelRelation.ConflictID=" + ModelID + " order by GlobalTestModelRelation.ID";
                    DataTable relationDt = pDataIO.GetDataTable(Sql, "ModelRelation");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ControlList[0].TxtModelWeight) > Convert.ToInt32(dt.Rows[i]["ItemPriority"]))
                        {
                            if (dt.Rows[i]["PID"].ToString() == ModelID)
                            {
                                relationDt.Rows[i]["PID"] = dt.Rows[i]["ConflictID"];
                                relationDt.Rows[i]["ConflictID"] = ModelID;
                            }                           
                        }
                        else if (Convert.ToInt32(ControlList[0].TxtModelWeight) < Convert.ToInt32(dt.Rows[i]["ItemPriority"]))
                        {
                            if (dt.Rows[i]["ConflictID"].ToString() == ModelID)
                            {
                                relationDt.Rows[i]["PID"] = ModelID;
                                relationDt.Rows[i]["ConflictID"] = dt.Rows[i]["PID"];
                            }                            
                        }
                        else if (Convert.ToInt32(ControlList[0].TxtModelWeight) == Convert.ToInt32(dt.Rows[i]["ItemPriority"]))
                        {
                            if (relationDt.Rows[i].RowState != DataRowState.Deleted)
                            {
                                relationDt.Rows[i].Delete();
                            }                          
                        }
                    }

                    int result3 = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result3 = pDataIO.UpdateWithProc("GlobalTestModelRelation", relationDt, Sql, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result3 = pDataIO.UpdateWithProc("GlobalTestModelRelation", relationDt, Sql, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result3 > 0)
                    {
                        relationDt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!");
                    }
                }
           
                if (result > 0)
                {
                    Response.Redirect("~/WebFiles/MaintainInfo/APPModel/GlobalModelList.aspx?", true);
                }

                #endregion
            }              
            else if (AddNew && mydt.Rows.Count == 0)
            {
                #region //New

                long currEqID = pDataIO.GetLastInsertData("GlobalAllTestModelList") + 1;
                DataRow eqNewDr = mydt.NewRow();
                eqNewDr["ID"] = currEqID;
                eqNewDr["PID"] = ControlList[0].DdlAppName.SelectedValue;
                eqNewDr["ItemName"] = ControlList[0].TxtItemName;
                eqNewDr["ShowName"] = ControlList[0].TxtShowName;
                eqNewDr["ItemDescription"] = ControlList[0].TxtItemDescription;
                eqNewDr["ItemPriority"] = ControlList[0].TxtModelWeight;

                mydt.Rows.Add(eqNewDr);
                //150527 ---------------------更新方式变更>>Start
                int result = -1;
                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalAllTestModelList", mydt, queryStr, logTracingString, "ATS_V2");
                }
                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                {
                    result = pDataIO.UpdateWithProc("GlobalAllTestModelList", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
                }      

                if (result > 0)
                {
                    mydt.AcceptChanges();
                    ModelID = result.ToString();
                    Response.Redirect("~/WebFiles/MaintainInfo/APPModel/CurrModelParamInfo.aspx?AddNew=true&ModelID=" + ModelID, true);
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
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!当前项已被删除!')</Script>");
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
                Response.Redirect("~/WebFiles/MaintainInfo/APPModel/GlobalModelList.aspx?", true);
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
        OptionButtons1.ConfigBtAddVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.AddAppModel, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.MofifyAppModel, myAccessCode);
        OptionButtons1.ConfigBtDeleteVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.DeleteAppModel, myAccessCode);
        OptionButtons1.ConfigBtCancelVisible = false;
    }
}