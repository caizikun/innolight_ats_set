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

public partial class WebFiles_APPModel_GlobalModelParamsList : BasePage
{
    const string funcItemName = "测试模型参数";
    Frame_APPModel_GlobalModelParamsList[] ControlList;

    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string ModelID = "-1";
    string queryStr = "";
    private string logTracingString = "";
    DataTable mydt = new DataTable();

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
            queryStr = "select * from GlobalTestModelParamterList where pid=" + ModelID;
        }
        initPageInfo();
        ConfigOptionButtonsVisible();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select showName from GlobalAllTestModelList where id = " + ModelID).ToString();
        Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
        this.plhNavi.Controls.Add(myCtrl);
    }

    void initPageInfo()
    {
        try
        {
            
            if (ModelID != null && ModelID.Length > 0)
            {
                createNavilnks();
                getParamList(queryStr);
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

    bool getParamList(string filterStr, bool isEditState = false)
    {
        try
        {
            mydt = pDataIO.GetDataTable(filterStr, "GlobalTestModelParamterList");
            if (mydt.Rows.Count == 0)
            {
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
            }
            if (mydt.Rows.Count==0)
            {
                ControlList = new Frame_APPModel_GlobalModelParamsList[1];
                for (int i = 0; i < ControlList.Length; i++)
                {
                    ControlList[i] = (Frame_APPModel_GlobalModelParamsList)Page.LoadControl("~/Frame/APPModel/GlobalModelParamsList.ascx");

                    ControlList[i].ContentTRVisible = false; 
                    this.plhMain.Controls.Add(ControlList[i]);
                }
            } 
            else
            {
                ControlList = new Frame_APPModel_GlobalModelParamsList[mydt.Rows.Count];
                for (int i = 0; i < mydt.Rows.Count; i++)
                {
                    ControlList[i] = (Frame_APPModel_GlobalModelParamsList)Page.LoadControl("~/Frame/APPModel/GlobalModelParamsList.ascx");
                    ControlList[i].ID = "ModelParam_" + mydt.Rows[i]["ID"].ToString();
                    ControlList[i].TxtItemNamePostBackUrl = "~/WebFiles/MaintainInfo/APPModel/CurrModelParamInfo.aspx?uId=" + mydt.Rows[i]["ID"] + "&ModelID=" + mydt.Rows[i]["PID"].ToString();
                    ControlList[i].TxtItemName = mydt.Rows[i]["ItemName"].ToString();
                    ControlList[i].TxtShowName = mydt.Rows[i]["ShowName"].ToString();
                    ControlList[i].TxtItemType = mydt.Rows[i]["ItemType"].ToString();
                    ControlList[i].TxtItemValue = mydt.Rows[i]["ItemValue"].ToString();
                    ControlList[i].TxtItemDescription = mydt.Rows[i]["ItemDescription"].ToString(); ;
                    ControlList[i].SetModelParamsEnableState(false);
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

    public bool AddData(object obj, string prameter)
    {
        Response.Redirect("~/WebFiles/MaintainInfo/APPModel/CurrModelParamInfo.aspx?AddNew=true&ModelId=" + this.ModelID);
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;
        try
        {
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null && ControlList[i].ChkIDModelParamChecked == true)
                {
                    for (int j = 0; j < mydt.Rows.Count; j++)
                    {
                        if (mydt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if (mydt.Rows[j]["id"].ToString() == ControlList[i].ID.Replace("ModelParam_", ""))
                            {
                                mydt.Rows[j].Delete();        
                                row++;
                                break;
                            }
                        }
                    }
                }
            }

            if (row == 0)
            {
                this.Page.RegisterStartupScript("", "<script>alert('请至少选择一个！');</script>");
                return false;
            }
            //150527 ---------------------更新方式变更>>Start
            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("GlobalTestModelParamterList", mydt, queryStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalTestModelParamterList", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.MofifyAppModel, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false; //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.MofifyAppModel, myAccessCode);
        if (mydt.Rows.Count<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.MofifyAppModel, myAccessCode);
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
            ControlList[i].ChkIDModelParamChecked = true;
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
            ControlList[i].ChkIDModelParamChecked = false;
        }
    }
}