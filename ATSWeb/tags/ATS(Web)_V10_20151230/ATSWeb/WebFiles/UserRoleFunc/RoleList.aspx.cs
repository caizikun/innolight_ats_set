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

public partial class WebFiles_UserRoleFunc_RoleList : BasePage
{
    const string funcItemName = "RoleList";
    const string queryStr = "select * from RolesTable";
    ASCXacountList[] ControlList;
    DataIO pDataIO;
    private string logTracingString = "";
    public WebFiles_UserRoleFunc_RoleList()
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
        SetSessionBlockType(8);
        initPageInfo();
    }

    bool getInfo(string filterStr)
    {
        Table pTable = new Table();

        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, "ManagementList", Session["BlockType"].ToString(), pDataIO,out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
            pTable.ID = "ListTable";
            //pTable.CssClass = "tableParams";
            //string ss = pTable.Attributes.CssStyle;
            string ss = pTable.Style.Value;
            //List<string> pLst = new List<string>();
            //pLst.Add("select");
            //pLst.Add("Item");
            //pLst.Add("View Access of Role");

            //TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pLst);
            //if (thr != null)
            //{
            //    pTable.Controls.Add(thr);
            //}

            DataTable pReaderTable = pDataIO.GetDataTable(filterStr, "RoleInfo");
            if (pReaderTable.Rows.Count == 0)
            {
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
                OptionButtons1.ConfigBtDeleteVisible = false;
            }
            if (pReaderTable.Rows.Count==0)
            {
                ControlList = new ASCXacountList[1];

                for (int i = 0; i < pReaderTable.Rows.Count; i++)
                {
                    ControlList[i] = (ASCXacountList)Page.LoadControl("../../Frame/UserRoleFunc/acountList.ascx");
                    ControlList[i].ContentTRVisible = false;
                    this.plhMain.Controls.Add(ControlList[i]);
                }
            } 
            else
            {
                 ControlList = new ASCXacountList[pReaderTable.Rows.Count];
         
            for (int i = 0; i < pReaderTable.Rows.Count; i++)
            {
                ControlList[i] = (ASCXacountList)Page.LoadControl("../../Frame/UserRoleFunc/acountList.ascx");
               
                ControlList[i].ID = "ControlList_" + pReaderTable.Rows[i]["ID"].ToString();

               ControlList[i].ConfigLink1Text= pReaderTable.Rows[i]["RoleName"].ToString();

               ControlList[i].ConfigLinkPostBackURL = "~/WebFiles/UserRoleFunc/RoleInfo.aspx?uId=" + pReaderTable.Rows[i]["ID"];
               ControlList[i].ConfigTH1 = "Item";
               ControlList[i].ConfigTH2 = "RoleAccess";
               ControlList[i].ConfigLink2Text = "View";
                ControlList[i].ConfigLinkPostBackURL2= "~/WebFiles/UserRoleFunc/RoleFuncInfo.aspx?uId=" + pReaderTable.Rows[i]["ID"];

                ControlList[i].THTD3Visible(false);

                if (i>=1)
                {
                    ControlList[i].EnableTH1Visible = false;
                    ControlList[i].EnableTH2Visible = false;
                    ControlList[i].LBTHTitleVisible(false);
                }
                //TableCell[] tcs = new TableCell[3];

                //tcs[0] = pCommCtrl.CreateMyTableCell(ControlList[i]);
                //tcs[1] = pCommCtrl.CreateMyTableCell(pLinkBtn);
                //tcs[2] = pCommCtrl.CreateMyTableCell(pLinkBtnUserRole);
                //TableRow tr = pCommCtrl.CreateMyTableRow(tcs);

                //pTable.Controls.Add(tr);
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

    void initPageInfo()
    {
        try
        {
            ConfigOptionButtonsVisible();
            if (getInfo(queryStr))
            {

            }
            else
            {
                OptionButtons1.Visible = false;
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>", false);
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            throw ex;
        }
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

    public void ConfigOptionButtonsVisible(bool isSaveMode = false)
    {
        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = !isSaveMode;
        OptionButtons1.ConfigBtEditVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = !isSaveMode;
        OptionButtons1.ConfigBtCancelVisible = false;
    }

    public bool AddData(object obj, string prameter)
    {
        Response.Redirect("~/WebFiles/UserRoleFunc/RoleInfo.aspx?AddNew=true",true);
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;
        string deletStr = "select * from RolesTable";
        DataTable myDt = pDataIO.GetDataTable(deletStr, "");
        try
        {
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null && ControlList[i].configSelected == true)
                {
                    for (int j = 0; j < myDt.Rows.Count; j++)
                    {
                        if (myDt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if ((myDt.Rows[j]["id"].ToString()).ToLower() == ControlList[i].ID.ToString().Replace("ControlList_", "").ToLower())
                            {
                                DataRow[] drs = myDt.Select("id =" + myDt.Rows[j]["id"].ToString());
                                foreach (DataRow pDr in drs)
                                {
                                    pDr.Delete();
                                    row++;
                                }
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
            pDataIO.UpdateDataTable(deletStr, myDt);
            Response.Redirect(Request.Url.ToString(), true);
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
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
            ControlList[i].configSelected = true;
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
            ControlList[i].configSelected = false;
        }
    }
}