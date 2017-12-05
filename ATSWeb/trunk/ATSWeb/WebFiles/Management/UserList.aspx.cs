﻿using System;
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

public partial class WebFiles_UserRoleFunc_UserList : BasePage
{
    const string funcItemName = "用户列表";
    const string queryStr = "select * from UserInfo order by LoginName";
    ASCXacountList[] ControlList;
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
        SetSessionBlockType(4);
        initPageInfo();
    }

    bool getInfo(string filterStr)
    {
        Table pTable = new Table();

        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, "管理", Session["BlockType"].ToString(), pDataIO,out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            pTable.ID = "ListTable";
            //List<string> pLst = new List<string>();
            //pLst.Add("select");
            //pLst.Add("UserName");
            //pLst.Add("ViewRoles of User");

            //TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pLst);
            //if (thr != null)
            //{
            //    pTable.Controls.Add(thr);
            //}

            DataTable pReaderTable = pDataIO.GetDataTable(filterStr, "UserInfo");
            if (pReaderTable.Rows.Count == 0)
            {
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
                OptionButtons1.ConfigBtDeleteVisible = false;
            }
            if (pReaderTable.Rows.Count==0)
            {
                   ControlList = new ASCXacountList[1];
                   for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (ASCXacountList)Page.LoadControl("~/Frame/UserRoleFunc/acountList.ascx");

                ControlList[i].ContentTRVisible = false;
               
                this.plhMain.Controls.Add(ControlList[i]);
            }

            } 
            else
            {                                   
                ControlList = new ASCXacountList[pReaderTable.Rows.Count];

                for (int i = 0; i < pReaderTable.Rows.Count; i++)
                {
                    ControlList[i] = (ASCXacountList)Page.LoadControl("~/Frame/UserRoleFunc/acountList.ascx");
                    ControlList[i].ID = "ControlList_" + pReaderTable.Rows[i]["ID"].ToString();

                    if (pReaderTable.Rows[i]["TrueName"].ToString() != "")
                    {
                        ControlList[i].ConfigLink1Text = pReaderTable.Rows[i]["LoginName"].ToString() + "—" + pReaderTable.Rows[i]["TrueName"].ToString();
                    }
                    else
                    {
                        ControlList[i].ConfigLink1Text = pReaderTable.Rows[i]["LoginName"].ToString();
                    }
                    
                    ControlList[i].ConfigLinkPostBackURL = "~/WebFiles/Management/UserInfo.aspx?uId=" + pReaderTable.Rows[i]["ID"];


                    ControlList[i].ConfigLink2Text = "查看";
                    ControlList[i].ConfigLinkPostBackURL2 = "~/WebFiles/Management/UserRoleInfo.aspx?uId=" + pReaderTable.Rows[i]["ID"];

                    string sql;
                    DataTable dt;

                    sql = "select distinct FunctionTable.FunctionCode from UserInfo,UserRoleTable,RolesTable,RoleFunctionTable,FunctionTable "
                        + "where UserInfo.ID=UserRoleTable.UserID and UserRoleTable.RoleID=RolesTable.ID and RolesTable.ID=RoleFunctionTable.RoleID and RoleFunctionTable.FunctionID=FunctionTable.ID "
                        + "and UserInfo.ID=" + pReaderTable.Rows[i]["ID"];

                    dt = pDataIO.GetDataTable(sql, " ");

                    bool ActionFlag = false;
                    int ActionType;

                    bool ViewPlanFlag = FindFuncCode(dt, "FunctionCode", 1);
                    bool MofifyPlanFlag = FindFuncCode(dt, "FunctionCode", 2);
                    bool AddPlanFlag = FindFuncCode(dt, "FunctionCode", 4);
                    bool DeletePlanFlag = FindFuncCode(dt, "FunctionCode", 8);

                    //bool ViewPNFlag = FindFuncCode(dt, "FunctionCode", 16);
                    //bool MofifyPNFlag = FindFuncCode(dt, "FunctionCode", 32);
                    //bool AddPNFlag = FindFuncCode(dt, "FunctionCode", 64);
                    //bool DeletePNFlag = FindFuncCode(dt, "FunctionCode", 128);
                    
                    //ActionFlag = (ViewPlanFlag & !MofifyPlanFlag & !AddPlanFlag & !DeletePlanFlag) | (ViewPNFlag & !MofifyPNFlag & !AddPNFlag & !DeletePNFlag);
                    ActionFlag = ViewPlanFlag & !MofifyPlanFlag & !AddPlanFlag & !DeletePlanFlag;

                    //if (ViewPlanFlag & !MofifyPlanFlag & !AddPlanFlag & !DeletePlanFlag & ViewPNFlag & !MofifyPNFlag & !AddPNFlag & !DeletePNFlag)
                    //{
                    //    ActionType = 1;     //仅读TestPlan，仅读PN
                    //}
                    //else if (ViewPlanFlag & !MofifyPlanFlag & !AddPlanFlag & !DeletePlanFlag)
                    //{
                    //    ActionType = 2;     //仅读TestPlan
                    //}
                    //else if (ViewPNFlag & !MofifyPNFlag & !AddPNFlag & !DeletePNFlag)
                    //{
                    //    ActionType = 3;     //仅读PN
                    //}
                    //else
                    //    ActionType = -1;     //其他情况

                    if (ViewPlanFlag & !MofifyPlanFlag & !AddPlanFlag & !DeletePlanFlag)
                    {
                        ActionType = 1;     //仅读TestPlan，仅读PN
                    }
                    else
                        ActionType = -1;     //其他情况

                    if (ActionFlag == false)
                    {
                        ControlList[i].ConfigLink3Text = "—";
                        ControlList[i].Ink3ItemNameEnable = false;
                    }
                    else
                    {
                        ControlList[i].ConfigLinkPostBackURL3 = "~/WebFiles/Management/UserActionInfo.aspx?uId=" + pReaderTable.Rows[i]["ID"] + "&uActionType=" + ActionType;
                    }
              

                    //TableCell[] tcs = new TableCell[3];

                    //tcs[0] = pCommCtrl.CreateMyTableCell(ControlList[i]);
                    //tcs[1] = pCommCtrl.CreateMyTableCell(pLinkBtn);
                    //tcs[2] = pCommCtrl.CreateMyTableCell(pLinkBtnUserRole);
                    //TableRow tr = pCommCtrl.CreateMyTableRow(tcs);

                    //pTable.Controls.Add(ControlList[i]);
                    if (i>=1)
                    {
                        ControlList[i].EnableTH1Visible = false;
                        ControlList[i].EnableTH2Visible = false;
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
        Response.Redirect("~/WebFiles/Management/UserInfo.aspx?AddNew=true", true);
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;
        DataTable myDt = pDataIO.GetDataTable(queryStr, "");
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
                ClientScript.RegisterStartupScript(GetType(),"Message", "<script>alert('请至少选择一个！');</script>",false);
                return false;
            }
            pDataIO.UpdateDataTable(queryStr, myDt);
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

    public bool FindFuncCode(DataTable dt, string columnName, int code)
    {
        bool flag = false;

        for (int i=0; i < dt.Rows.Count; i++)
        {
            if (Convert .ToInt32 ( dt.Rows[i][columnName]) == code)
            {
                flag = true;
                break;
            }
            else
            {
                flag = false;
            }
        }

        return flag;
    }

}