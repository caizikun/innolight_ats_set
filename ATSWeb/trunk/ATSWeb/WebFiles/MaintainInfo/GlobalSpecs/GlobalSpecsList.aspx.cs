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

public partial class WebFiles_GlobalSpecs_GlobalSpecsList : BasePage
{
    const string funcItemName = "规格参数(维护)";
    const string queryStr = "select * from GlobalSpecs";
    Frame_GlobalSpecs_GlobalSpecsList[] ControlList;
    DataTable mydt = new DataTable();
    int rowCount = 0;
    DataIO pDataIO;
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
        initPageInfo();
        ConfigOptionButtonsVisible();
    }

    void initPageInfo()
    {
        try
        {
            //if (Request.QueryString["BlockType"] != null)
            //{
            //    Session["BlockType"] = Request.QueryString["BlockType"];
            //}
            //else
            //{
            //    Session["BlockType"] = null;
            //}

            if (getInfo(queryStr))
            {

            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>",false);
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    bool getInfo(string filterStr)
    {
        //HtmlTable pTable = new HtmlTable();
        //Table pTable = new Table();

        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            string parentItem = "维护";

            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            //pTable.ID = "ListTable";
            //List<string> pHeaderLst = new List<string>();
            //pHeaderLst.Add("");         //因为该用户控件是<td></td>以Table的形式会空一格
            //pHeaderLst.Add("Select");
            //pHeaderLst.Add("EquipName");
            //pHeaderLst.Add("EquipType");
            //pHeaderLst.Add("Description");
            //pHeaderLst.Add("Params");            
            //TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pHeaderLst);
            //this.plhMain.Controls.Add(thr);
            //if (thr != null)
            //{
            //    pTable.Controls.Add(thr);
            //}
            //----------------------------
            mydt = pDataIO.GetDataTable(filterStr, "GlobalSpecs");
            if (mydt.Rows.Count == 0)
            {
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
            }
            DataRow[] drs = mydt.Select("");
            rowCount = drs.Length;
            if (rowCount==0)
            {
               ControlList = new Frame_GlobalSpecs_GlobalSpecsList[1];
               for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (Frame_GlobalSpecs_GlobalSpecsList)Page.LoadControl("~/Frame/GlobalSpecs/GlobalSpecsList.ascx");

                ControlList[i].ContentTRVisible = false;
               
                this.plhMain.Controls.Add(ControlList[i]);
            }
            } 
            else
            {
                 ControlList = new Frame_GlobalSpecs_GlobalSpecsList[rowCount];
            for (int i = 0; i < drs.Length; i++)
            {
                ControlList[i] = (Frame_GlobalSpecs_GlobalSpecsList)Page.LoadControl("~/Frame/GlobalSpecs/GlobalSpecsList.ascx");
                ControlList[i].ID = drs[i]["ID"].ToString();
                ControlList[i].LnkItemNamePostBackUrl = "~/WebFiles/MaintainInfo/GlobalSpecs/GlobalSpecsInfo.aspx?uId=" + drs[i]["ID"];
                ControlList[i].LnkItemName = drs[i]["ItemName"].ToString();
                ControlList[i].TxtUnit = drs[i]["Unit"].ToString();
                ControlList[i].TxtItemDescription = drs[i]["ItemDescription"].ToString();

                if (i >= 1)
                {
                    ControlList[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        ControlList[i].TrBackgroundColor = "#F2F2F2";
                    }
                }
                //HtmlTableCell tc = new HtmlTableCell();
                //tc.Controls.Add(ControlList[i]);
                //tc.Attributes.Add("style", "border:solid #000 1px");
                //HtmlTableRow tr = new HtmlTableRow();
                //tr.Controls.Add(tc);
                //pTable.Rows.Add(tr);
                this.plhMain.Controls.Add(ControlList[i]);
            }
            }
           
            //pTable.Attributes.Add("style", "  border:none; border-collapse:collapse;word-wrap: break-word;word-break : break-all;");


            return true;
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            throw ex;
        }
    }

    public bool AddData(object sender, string prameter)
    {
        Response.Redirect("~/WebFiles/MaintainInfo/GlobalSpecs/GlobalSpecsInfo.aspx?AddNew=true", true);
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;
        try
        {
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null && ControlList[i].chkIDChecked == true)
                {
                    for (int j = 0; j < mydt.Rows.Count; j++)
                    {
                        if (mydt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if (mydt.Rows[j]["id"].ToString() == ControlList[i].ID)
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
                result = pDataIO.UpdateWithProc("GlobalSpecs", mydt, queryStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalSpecs", mydt, queryStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
            }
            //pDataIO.UpdateDataTable(queryStr, mydt);
            Response.Redirect(Request.Url.ToString(), true);
            //150527 ---------------------更新方式变更<<End
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

    public bool updateData(object obj, string parameters)
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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.GlobalSpecs, CommCtrl.CheckAccess.MofifyGlobalSpecs, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
        if (mydt.Rows.Count<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.GlobalSpecs, CommCtrl.CheckAccess.MofifyGlobalSpecs, myAccessCode);
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
            ControlList[i].chkIDChecked = true;
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
            ControlList[i].chkIDChecked = false;
        }
    }
}