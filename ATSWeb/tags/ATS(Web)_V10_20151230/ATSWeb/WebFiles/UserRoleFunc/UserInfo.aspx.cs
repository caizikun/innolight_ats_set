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

public partial class WebFiles_UserRoleFunc_UserInfo : BasePage
{
    const string funcItemName = "UserInfo";
    string queryStr = "";
    //TextBox txtLoginName = new TextBox(), txtPwd = new TextBox(), txtTrueName = new TextBox(), txtRemark = new TextBox();
    DataIO pDataIO;
    string currID = "";
    bool AddNew = false;
    private string logTracingString = "";
    Frame_UserRoleFunc_userInfo ControlList;

    public WebFiles_UserRoleFunc_UserInfo()
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
        }
        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            currID = "-1";
        }
        queryStr = "select * from UserInfo where ID=" + currID;
        initPageInfo();
    }

    bool getInfo(string filterStr, bool isEditMode = false)
    {
        Table pTable = new Table();

        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            string parentItem = pDataIO.getDbCmdExecuteScalar("select LoginName from UserInfo where id = " + currID).ToString();
            if (AddNew)
            {
                parentItem = "AddNewItem";
                EditData("", "");
            }
            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
            pTable.ID = "ListTable";
            
            DataTable pReaderTable = pDataIO.GetDataTable(filterStr, "UserInfo");
            ControlList = (Frame_UserRoleFunc_userInfo)Page.LoadControl("~/Frame/UserRoleFunc/userInfo.ascx");
            ControlList.SetEnableState(isEditMode);        
            if (pReaderTable.Rows.Count ==1 && !AddNew)
            {
                ControlList.TxtLoginName = pReaderTable.Rows[0]["LoginName"].ToString();
                ControlList.TxtPwd = pReaderTable.Rows[0]["LoginPassword"].ToString();
                ControlList.TxtTrueName = pReaderTable.Rows[0]["TrueName"].ToString();
                ControlList.TxtRemarks = pReaderTable.Rows[0]["Remarks"].ToString();
            }

            TableCell  tc = new TableCell();
            TableRow tr = new TableRow();
            tc.Controls.Add(ControlList);
            tr.Cells.Add(tc);
            pTable.Controls.Add(tr);
            
            //setItemEnabled(isEditMode);
            this.plhMain.Controls.Add(pTable);

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
                if (getInfo(queryStr, AddNew))
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
            DataTable myDt = pDataIO.GetDataTable(queryStr, "UserInfo");
            if (myDt.Rows.Count == 1)
            {
                #region //已经获取到当前的信息    LoginName LoginPassword TrueName Remarks
                myDt.Rows[0]["LoginName"] = ControlList.TxtLoginName;
                myDt.Rows[0]["LoginPassword"] = ControlList.TxtPwd;
                myDt.Rows[0]["TrueName"] = ControlList.TxtTrueName;
                myDt.Rows[0]["Remarks"] = ControlList.TxtRemarks;

                if (pDataIO.UpdateDataTable(queryStr, myDt))
                {
                    Response.Redirect(Request.Url.ToString());
                }
                #endregion
            }
            else if (AddNew)
            {
                long currNewID = pDataIO.GetLastInsertData("UserInfo") + 1;
                DataRow dr = myDt.NewRow();
                dr["ID"] = currNewID;
                dr["LoginName"] = ControlList.TxtLoginName;
                dr["LoginPassword"] = ControlList.TxtPwd;
                dr["TrueName"] = ControlList.TxtTrueName;
                dr["Remarks"] = ControlList.TxtRemarks;
                myDt.Rows.Add(dr);
                long newID = -1;
                if (pDataIO.UpdateDataTable(queryStr, myDt, out newID))
                {
                    Response.Redirect("~/WebFiles/UserRoleFunc/UserRoleInfo.aspx?uId=" + newID,true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('Error!Update data fail!~');location.href='" + Request.Url.ToString() + "'</Script>", false);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any Result,Current item has been deleted!~')</Script>", false);
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
        OptionButtons1.ConfigBtSaveVisible = isSaveMode;
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtEditVisible = !isSaveMode;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = isSaveMode;
    }

    void setItemEnabled(bool state)
    {
        if(ControlList!=null)
        {
            ControlList.SetEnableState(state);
        }
    }
}