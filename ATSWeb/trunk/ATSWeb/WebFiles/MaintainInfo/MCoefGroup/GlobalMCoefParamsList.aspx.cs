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

public partial class WebFiles_MCoefGroup_GlobalMCoefParamsList : BasePage
{
    const string funcItemName = "模块系数参数";
    Frame_MCoefGroup_GlobalMCoefParamsList[] ControlList;

    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string MCoefId = "-1";
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
            MCoefId = Request.QueryString["uId"];
        }
        queryStr = "select * from GlobalManufactureCoefficients where pid=" + MCoefId + " Order by Page,StartAddress,Channel";

        initPageInfo();
        ConfigOptionButtonsVisible();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalManufactureCoefficientsGroup where id = " + MCoefId).ToString();
        Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
        this.plhNavi.Controls.Add(myCtrl);
    }

    void initPageInfo()
    {
        try
        {
           
            if (MCoefId != null && MCoefId.Length > 0)
            {
                createNavilnks();
                getParamList(queryStr);
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

    bool getParamList(string filterStr, bool isEditState = false)
    {
        try
        {
            mydt = pDataIO.GetDataTable(filterStr, "GlobalManufactureCoefficients");
            
            if (mydt.Rows.Count == 0)
            {
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
            }
            if (mydt.Rows.Count==0)
            {
                ControlList = new Frame_MCoefGroup_GlobalMCoefParamsList[1];
                for (int i = 0; i < ControlList.Length; i++)
                {

                    ControlList[i] = (Frame_MCoefGroup_GlobalMCoefParamsList)Page.LoadControl("~/Frame/MCoefGroup/GlobalMCoefParamsList.ascx");
                    ControlList[i].ContentTRVisible = false;
                    this.plhMain.Controls.Add(ControlList[i]);
                }

            } 
            else
            {
                ControlList = new Frame_MCoefGroup_GlobalMCoefParamsList[mydt.Rows.Count];
                for (int i = 0; i < mydt.Rows.Count; i++)
                {

                    ControlList[i] = (Frame_MCoefGroup_GlobalMCoefParamsList)Page.LoadControl("~/Frame/MCoefGroup/GlobalMCoefParamsList.ascx");
                    ControlList[i].ID = "MCoefParam_" + mydt.Rows[i]["ID"].ToString();
                    ControlList[i].TxtItemNamePostBackUrl = "~/WebFiles/MaintainInfo/MCoefGroup/CurrMCoefParamInfo.aspx?uId=" + mydt.Rows[i]["ID"] + "&MCoefId=" + mydt.Rows[i]["PID"].ToString();
                    ControlList[i].TxtItemName = mydt.Rows[i]["ItemName"].ToString();
                    ControlList[i].TxtItemType = mydt.Rows[i]["ItemType"].ToString();
                    ControlList[i].TxtChannel = mydt.Rows[i]["Channel"].ToString();
                    ControlList[i].TxtPage = mydt.Rows[i]["Page"].ToString();
                    ControlList[i].TxtStartAddress = mydt.Rows[i]["StartAddress"].ToString();
                    ControlList[i].TxtLength = mydt.Rows[i]["Length"].ToString();
                    ControlList[i].TxtFormat = mydt.Rows[i]["Format"].ToString();
                    ControlList[i].TxtAmplify = mydt.Rows[i]["AmplifyCoeff"].ToString();
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
        Response.Redirect("~/WebFiles/MaintainInfo/MCoefGroup/CurrMCoefParamInfo.aspx?AddNew=true&MCoefId=" + this.MCoefId);
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {        
        int row = 0;
        try
        {
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null && ControlList[i].ChkIDMCoefParamChecked == true)
                {
                    for (int j = 0; j < mydt.Rows.Count; j++)
                    {
                        if (mydt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if (mydt.Rows[j]["id"].ToString() == ControlList[i].ID.Replace("MCoefParam_", ""))
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
                ClientScript.RegisterStartupScript(GetType(),"Message", "<script>alert('请至少选择一个！');</script>",false);
                return false;
            }
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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefGroup, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false; //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefInfo, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
        if (mydt.Rows.Count<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MCoefGroup, CommCtrl.CheckAccess.MofifyMCoefInfo, myAccessCode);
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
            ControlList[i].ChkIDMCoefParamChecked = true;
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
            ControlList[i].ChkIDMCoefParamChecked = false;
        }
    }
}