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

public partial class WebFiles_UserRoleFunc_UserRoleFuncList : BasePage
{
    const string funcItemName = "ManagementList";
    DataIO pDataIO;
    ASCXRoleFunctionList[] ControlList;
    private string logTracingString = "";
    public WebFiles_UserRoleFunc_UserRoleFuncList()
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
        if (Request.QueryString["BlockType"] != null)
        {
            Session["BlockType"] = Request.QueryString["BlockType"];
        }
        else
        {
            Session["BlockType"] = null;
        }
        string myPid = pDataIO.getDbCmdExecuteScalar("select id from FunctionTable where ItemName ='ManagementList'").ToString();
        if (myPid.ToUpper().Trim() == "Not Found Data".ToUpper() && Session["BlockType"]!=null)
        {
            DataTable pDt= pDataIO.GetDataTable("select * from FunctionTable","");
            DataRow dr = pDt.NewRow();
            //[PID]      ,[BlockLevel]      ,[BlockTypeID]      ,[ItemName]      ,[AliasName]      ,[Title]      ,[FunctionCode]      ,[Remarks]
            DataRow[] drs = pDt.Select("BlockTypeID=" + Session["BlockType"]);
            if (drs.Length > 0)
            {
                dr["PID"] = drs[0]["ID"];
                dr["BlockLevel"] = 1;
                dr["BlockTypeID"] = Session["BlockType"];
                dr["ItemName"] = "ManagementList";
                pDt.Rows.Add(dr);
                if (pDataIO.UpdateDataTable("select * from FunctionTable where ItemName ='ManagementList'", pDt))
                {
                    pDt = pDataIO.GetDataTable("select * from FunctionTable", "");
                    DataRow dr0 = pDt.NewRow();
                    myPid = pDataIO.getDbCmdExecuteScalar("select id from FunctionTable where ItemName ='ManagementList'").ToString();
                    dr0["PID"] = myPid;
                    dr0["BlockLevel"] = 2;
                    dr0["BlockTypeID"] = Session["BlockType"].ToString();
                    dr0["ItemName"] = "UserList";
                    pDt.Rows.Add(dr0);

                    DataRow dr1 = pDt.NewRow();
                    dr1["PID"] = myPid;
                    dr1["BlockLevel"] = 2;
                    dr1["BlockTypeID"] = Session["BlockType"].ToString();
                    dr1["ItemName"] = "RoleList";
                    pDt.Rows.Add(dr1);

                    DataRow dr2 = pDt.NewRow();
                    dr2["PID"] = myPid;
                    dr2["BlockLevel"] = 2;
                    dr2["BlockTypeID"] = Session["BlockType"].ToString();
                    dr2["ItemName"] = "FuncBlockList";
                    pDt.Rows.Add(dr2);

                    DataRow dr3 = pDt.NewRow();
                    dr3["PID"] = myPid;
                    dr3["BlockLevel"] = 2;
                    dr3["BlockTypeID"] = Session["BlockType"].ToString();
                    dr3["ItemName"] = "AscxList";
                    pDt.Rows.Add(dr3);
                    if (pDataIO.UpdateDataTable("select * from FunctionTable", pDt))
                    {
                        getInfo("select * from FunctionTable where pid = " + myPid + " and blockLevel=2 and BlockTypeID = " + Request.QueryString["BlockType"]);
                    }
                }
            }
        }
        else
        {
            getInfo("select * from FunctionTable where pid = " + myPid + " and blockLevel=2 and BlockTypeID = " + Request.QueryString["BlockType"]);
        }
        OptionButtons1.Visible = false;
    }

    bool getInfo(string filterStr)
    {
        Table pTable = new Table();

        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, "UserRoleFunction", Session["BlockType"].ToString(), pDataIO,out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            pTable.ID = "ListTable";
            pTable.CssClass = "showTableData";
            
            DataTable pReaderTable = pDataIO.GetDataTable(filterStr, "List");
            if (pReaderTable.Rows.Count==0)
            {
                 ControlList = new ASCXRoleFunctionList[1];
                 for (int i = 0; i < ControlList.Length; i++)
            {
                ControlList[i] = (ASCXRoleFunctionList)Page.LoadControl("~/Frame/UserRoleFunc/RolefunctionList.ascx");
                ControlList[i].ContentTRVisible = false;
                
                this.plhMain.Controls.Add(ControlList[i]);
            }

            } 
            else
            {
                 ControlList = new ASCXRoleFunctionList[pReaderTable.Rows.Count];
            for (int i = 0; i < pReaderTable.Rows.Count; i++)
            {
                ControlList[i] = (ASCXRoleFunctionList)Page.LoadControl("~/Frame/UserRoleFunc/RolefunctionList.ascx");
                string lstName = pReaderTable.Rows[i]["ItemName"].ToString();
                if (lstName.ToLower() == "UserList".ToLower())
                {
                    ControlList[i].ConfigLinkText = " View Account List";
                    ControlList[i].ConfigLinkPostBackURL= "~/WebFiles/UserRoleFunc/UserList.aspx";
                }
                else if (lstName.ToLower() == "RoleList".ToLower())
                {
                    ControlList[i].ConfigLinkText = " View Role List";
                    ControlList[i].ConfigLinkPostBackURL = "~/WebFiles/UserRoleFunc/RoleList.aspx";
                }
                else if (lstName.ToLower() == "FuncBlockList".ToLower())
                {
                    ControlList[i].ConfigLinkText = " View Function List";
                    ControlList[i].ConfigLinkPostBackURL = "~/WebFiles/UserRoleFunc/FuncList.aspx";
                }
                else if (lstName.ToLower() == "AscxFileList".ToLower())
                {
                    ControlList[i].ConfigLinkText = " View AscxFile List";
                    ControlList[i].ConfigLinkPostBackURL = "~/WebFiles/UserRoleFunc/AscxFileList.aspx";
                }

                //Label lblRemark = new Label();
                ControlList[i].ConfigLableText= pReaderTable.Rows[i]["AliasName"].ToString();
                if (i>=1)
                {
                    ControlList[i].EnableTH1Visible = false;
                    ControlList[i].EnableTH2Visible = false;
                    ControlList[i].LBTHTitleVisible(false);
                }
                //TableCell[] tcs = new TableCell[2];

                //tcs[0] = pCommCtrl.CreateMyTableCell(pCommCtrl.CreateHtmlGenericControl("Li", pLinkBtn));
                //tcs[1] = pCommCtrl.CreateMyTableCell(lblRemark);
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

}