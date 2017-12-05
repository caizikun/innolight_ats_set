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
    const string funcItemName = "MCoefParamsList";
    Frame_MCoefGroup_GlobalMCoefParamsList[] ControlList;

    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string MCoefId = "-1";
    string queryStr = "";
    private string logTracingString = "";
    DataTable mydt = new DataTable();

    public WebFiles_MCoefGroup_GlobalMCoefParamsList()
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
        Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
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
                    ControlList[i].TxtItemNamePostBackUrl = "~/WebFiles/MCoefGroup/CurrMCoefParamInfo.aspx?uId=" + mydt.Rows[i]["ID"] + "&MCoefId=" + mydt.Rows[i]["PID"].ToString();
                    ControlList[i].TxtItemName = mydt.Rows[i]["ItemName"].ToString();
                    ControlList[i].TxtItemType = mydt.Rows[i]["ItemType"].ToString();
                    ControlList[i].TxtChannel = mydt.Rows[i]["Channel"].ToString();
                    ControlList[i].TxtPage = mydt.Rows[i]["Page"].ToString();
                    ControlList[i].TxtStartAddress = mydt.Rows[i]["StartAddress"].ToString();
                    ControlList[i].TxtLength = mydt.Rows[i]["Length"].ToString();
                    ControlList[i].TxtFormat = mydt.Rows[i]["Format"].ToString();
                    if (i >= 1)
                    {
                        ControlList[i].EnableTH1Visible = false;
                        ControlList[i].EnableTH2Visible = false;
                        ControlList[i].EnableTH3Visible = false;
                        ControlList[i].EnableTH4Visible = false;
                        ControlList[i].EnableTH5Visible = false;
                        ControlList[i].EnableTH6Visible = false;
                        ControlList[i].EnableTH7Visible = false;
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

    public bool AddData(object obj, string prameter)
    {
        Response.Redirect("~/WebFiles/MCoefGroup/CurrMCoefParamInfo.aspx?AddNew=true&MCoefId=" + this.MCoefId);
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
                ClientScript.RegisterStartupScript(GetType(),"Message", "<script>alert('Did not choose any one！');</script>",false);
                return false;
            }
            //150527 ---------------------更新方式变更>>Start
            int result = pDataIO.UpdateWithProc("GlobalManufactureCoefficients", mydt, queryStr, logTracingString);
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