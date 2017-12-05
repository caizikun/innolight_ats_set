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

public partial class WebFiles_UserRoleFunc_RoleFuncInfo : BasePage
{
    const string funcItemName = "RoleFuncInfo";
    CheckBox[] currTableChks;
    CheckBox[] globalTableChks;
    DataIO pDataIO;
    string currID = "";
    string queryStr = "";
    private string logTracingString = "";
    public WebFiles_UserRoleFunc_RoleFuncInfo()
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
        if (Request.QueryString["uId"] != null)
        {
            currID = Request.QueryString["uId"];
            queryStr = "select * from FunctionTable where id in (select FunctionID from RoleFunctionTable where RoleID=" + currID + ")  and blockLevel=0";

        }
        initPageInfo();
    }

    bool getInfo(string filterStr, bool isEditMode = false)
    {
        Table pTable = new Table();

        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            string parentItem = pDataIO.getDbCmdExecuteScalar("select RoleName from RolesTable where id = " + currID).ToString();

            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            pTable.ID = "ListTable";
            List<string> pLst = new List<string>();

            pLst.Add("已经存在权限");
            pLst.Add("");
            pLst.Add("服务器权限列表");

            TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pLst);
            if (thr != null)
            {
                pTable.Controls.Add(thr);
            }

            #region 获取当前已有的列表
            DataTable pReaderTable = pDataIO.GetDataTable(filterStr, "CurrFunctionInfo");
            Table currTable = new Table();
            currTable.ID = "currTable";
            currTableChks = new CheckBox[pReaderTable.Rows.Count];

            for (int i = 0; i < pReaderTable.Rows.Count; i++)
            {
                currTableChks[i] = new CheckBox();
                currTableChks[i].ID = "chkCurrID_" + pReaderTable.Rows[i]["ID"].ToString();
                currTableChks[i].Checked = true;

                Label lblName = new Label();
                lblName.Text = pReaderTable.Rows[i]["Title"].ToString();

                Label lblRemark = new Label();
                lblRemark.Text = pReaderTable.Rows[i]["Remarks"].ToString();

                TableCell[] tcs = new TableCell[3];
                tcs[0] = pCommCtrl.CreateMyTableCell(currTableChks[i]);
                tcs[1] = pCommCtrl.CreateMyTableCell(lblName);
                tcs[2] = pCommCtrl.CreateMyTableCell(lblRemark);
                //string[] lblItem = new string[3] { "select", "RoleName", "Remark" };
                TableRow tr = new TableRow();
                for (int j = 0; j < tcs.Length; j++)
                {
                    tr.Cells.Add(tcs[j]);
                }

                currTable.Rows.Add(tr);
            }
            #endregion

            #region 服务器存在的列表
            DataTable rolesListTable = pDataIO.GetDataTable("select * from FunctionTable where blockLevel=0", "FunctionTable");
            Table rolesTable = new Table();
            rolesTable.ID = "GlobalTable";
            globalTableChks = new CheckBox[rolesListTable.Rows.Count];

            for (int i = 0; i < rolesListTable.Rows.Count; i++)
            {
                globalTableChks[i] = new CheckBox();
                globalTableChks[i].ID = "chkGlobalID_" + rolesListTable.Rows[i]["ID"].ToString();

                Label lblName = new Label();
                lblName.Text = rolesListTable.Rows[i]["Title"].ToString();

                Label lblRemark = new Label();
                lblRemark.Text = rolesListTable.Rows[i]["Remarks"].ToString();

                TableCell[] tcs = new TableCell[3];
                tcs[0] = pCommCtrl.CreateMyTableCell(globalTableChks[i]);
                tcs[1] = pCommCtrl.CreateMyTableCell(lblName);
                tcs[2] = pCommCtrl.CreateMyTableCell(lblRemark);
                TableRow tr = new TableRow();
                for (int j = 0; j < tcs.Length; j++)
                {
                    tr.Cells.Add(tcs[j]);
                }

                rolesTable.Rows.Add(tr);
            }
            #endregion

            TableCell dispTc0 = new TableCell();
            dispTc0.Controls.Add(currTable);

            TableCell dispTc1 = new TableCell();
            dispTc1.Text = "";
            TableCell dispTc2 = new TableCell();

            dispTc2.Controls.Add(rolesTable);
            TableRow dispTr = new TableRow();

            dispTr.Cells.Add(dispTc0);
            dispTr.Cells.Add(dispTc1);
            dispTr.Cells.Add(dispTc2);
            pTable.Rows.Add(dispTr);

            setItemEnabled(isEditMode);

            this.plhMain.Controls.Add(pTable);
            this.plhMain.Controls.Add(new HtmlGenericControl("hr"));
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
            if (currID != null && currID.Length > 0)
            {
                if (getInfo(queryStr))
                {
                    //暂未导入权限部分
                }
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
            ConfigOptionButtonsVisible(true);
            setItemEnabled(true);
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
            long currTableNewID = pDataIO.GetLastInsertData("UserRoleTable") + 1;
            string saveStr = "select * from RoleFunctionTable where RoleID=" + currID;
            DataTable myDt = pDataIO.GetDataTable(saveStr, "");

            //找到删除的资料
            for (int i = 0; i < currTableChks.Length; i++)
            {
                if (currTableChks[i] != null && currTableChks[i].Checked == false)
                {
                    for (int j = 0; j < myDt.Rows.Count; j++)
                    {
                        if (myDt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if ((myDt.Rows[j]["FunctionID"].ToString()).ToLower() == currTableChks[i].ID.ToString().Replace("chkCurrID_", "").ToLower())
                            //if ("chkCurrID_" + myDt.Rows[j]["FunctionID"].ToString() == currTableChks[i].ID)
                            {
                                myDt.Rows[j].Delete();
                                break;
                            }
                        }
                    }
                }
            }

            //找到新增的资料
            for (int i = 0; i < globalTableChks.Length; i++)
            {
                if (globalTableChks[i] != null && globalTableChks[i].Checked == true)
                {
                    DataRow[] drs = myDt.Select("FunctionID=" + globalTableChks[i].ID.ToString().Replace("chkGlobalID_", ""));
                    if (drs.Length == 0)
                    {
                        DataRow newDr = myDt.NewRow();
                        newDr["ID"] = currTableNewID;
                        newDr["RoleID"] = currID;
                        newDr["FunctionID"] = globalTableChks[i].ID.ToString().Replace("chkGlobalID_", "");
                        myDt.Rows.Add(newDr);
                        currTableNewID++;
                    }
                }
            }
            #region 更新
            if (pDataIO.UpdateDataTable(saveStr, myDt))
            {
                Response.Redirect(Request.Url.ToString());
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any Result,Current item has been deleted!~')</Script>",false);
            }
            #endregion
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
            Response.Redirect("~/WebFiles/UserRoleFunc/RoleList.aspx", true);
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
        OptionButtons1.ConfigBtSaveVisible = isSaveMode;
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtEditVisible = !isSaveMode;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = isSaveMode;
    }

    void setItemEnabled(bool state)
    {
        for (int i = 0; i < currTableChks.Length; i++)
        {
            if (currTableChks[i] != null)
            {
                currTableChks[i].Enabled = state;
            }
        }

        for (int i = 0; i < globalTableChks.Length; i++)
        {
            if (globalTableChks[i] != null)
            {
                globalTableChks[i].Enabled = state;
                foreach (CheckBox chk in currTableChks)
                {
                    if (chk != null)
                    {
                        if (chk.ID.ToString().Replace("chkCurrID_", "") == globalTableChks[i].ID.ToString().Replace("chkGlobalID_", ""))
                        {
                            globalTableChks[i].Enabled = false;
                        }
                    }
                }
            }
        }
    }
}
