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

public partial class WebFiles_UserRoleFunc_FuncBlockInfo : BasePage
{
    private const string funcItemName = "功能块信息";
    private DataIO pDataIO;
    private string queryStr = "";
    private string currID = "";
    private bool AddNew = false;
    private Frame_UserRoleFunc_blockInfo[] ControlList;
    private int rowCount = 0;
    private string logTracingString = "";
    string allIdItemStr = "";

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
        if (Request.QueryString["uId"] != null)
        {
            currID = Request.QueryString["uId"];
        }
        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            currID = "-1";
        }
        queryStr = "select * from FunctionTable where ID=" + currID;
        initPageInfo();
    }

    void initPageInfo()
    {
        try
        {
            ConfigOptionButtonsVisible(false);
            if (currID != null && currID.Length > 0)
            {
                getInfo(queryStr, AddNew);
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

    bool getInfo(string filterStr, bool isEditMode = false)
    {
        Table pTable = new Table();

        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where id = " + currID).ToString();
            if (AddNew)
            {                
                parentItem = "添加新项";
            }
            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            pTable.ID = "ListTable";
            List<string> pLst = new List<string>();
            //BlockLevel	BlockTypeID	ItemName	AliasName	Title	FunctionCode	Remarks
            pLst.Add("Level");
            pLst.Add("Type");
            pLst.Add("Item");
            pLst.Add("Description");
            pLst.Add("FunctionName");
            pLst.Add("Code");
            pLst.Add("Remarks");

            TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pLst);
            if (thr != null)
            {
                pTable.Controls.Add(thr);
            }

            DataTable pReaderTable = pDataIO.GetDataTable(filterStr, "FunctionTable");
            DataTable blockListDt = pDataIO.GetDataTable("select * from FunctionTable where blockLevel = 0 and PID=0", "BlockList");
            rowCount = pReaderTable.Rows.Count;
            if (rowCount > 0 && !AddNew)
            {
                ControlList = new Frame_UserRoleFunc_blockInfo[rowCount];
                for (int i = 0; i < pReaderTable.Rows.Count; i++)
                {
                    DataTable parentDt = pDataIO.GetDataTable("select * from FunctionTable where BlockTypeID =" + pReaderTable.Rows[i]["BlockTypeID"], "BlockItemInfo");
                    ControlList[i] = (Frame_UserRoleFunc_blockInfo)Page.LoadControl("~/Frame/UserRoleFunc/blockInfo.ascx");
                    ControlList[i].ID = "FuncID_" + pReaderTable.Rows[i]["ID"].ToString();
                    ControlList[i].DdlBlockLevel = pReaderTable.Rows[i]["BlockLevel"].ToString();                    

                    for (int j = 0; j < blockListDt.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(blockListDt.Rows[i]["BlockLevel"]) <= Convert.ToInt32(pReaderTable.Rows[i]["BlockLevel"]))
                        {
                            ControlList[i].AddDdlBlockTypeLstItem(new ListItem(blockListDt.Rows[j]["ItemName"].ToString()));
                        }
                    }
                    for (int k = 0; k < parentDt.Rows.Count; k++)
                    {
                        if (Convert.ToInt32(parentDt.Rows[i]["BlockLevel"]) < Convert.ToInt32(pReaderTable.Rows[i]["BlockLevel"]))
                        {
                            ControlList[i].AddDdlParentLstItem(new ListItem(parentDt.Rows[k]["ItemName"].ToString()));
                        }
                    }
                    string typeName = blockListDt.Select("blockLevel = 0 and BlockTypeID=" + pReaderTable.Rows[i]["BlockTypeID"].ToString())[0]["ItemName"].ToString();
                    ControlList[i].DdlBlockTypeID = typeName;

                    if (pReaderTable.Rows[i]["PID"].ToString() == "0")
                    {
                    
                    }
                    else
                    {
                        ControlList[i].DdlParentItem = parentDt.Select("ID=" + pReaderTable.Rows[i]["PID"].ToString())[0]["ItemName"].ToString();
                    }
                    ControlList[i].TxtItemName = pReaderTable.Rows[i]["ItemName"].ToString();
                    ControlList[i].TxtAliasName = pReaderTable.Rows[i]["AliasName"].ToString();

                    if (pReaderTable.Rows[i]["BlockLevel"].ToString().Trim() == "0")
                    {
                        ControlList[i].SetFuncItemVisable(true);
                        ControlList[i].TxtTitle = pReaderTable.Rows[i]["Title"].ToString();
                        ControlList[i].TxtFunctionCode = pReaderTable.Rows[i]["FunctionCode"].ToString();
                        ControlList[i].TxtRemarks = pReaderTable.Rows[i]["Remarks"].ToString();
                    }
                    else
                    {
                        ControlList[i].SetFuncItemVisable(false);
                        ControlList[i].TxtTitle = pReaderTable.Rows[i]["Title"].ToString();
                        ControlList[i].TxtFunctionCode = pReaderTable.Rows[i]["FunctionCode"].ToString();
                        ControlList[i].TxtRemarks = pReaderTable.Rows[i]["Remarks"].ToString();
                    }
                    ControlList[i].SetAllItemEnable(isEditMode);
                    this.plhMain.Controls.Add(ControlList[i]);
                }
            }
            else if (AddNew && rowCount == 0)
            {
                ControlList = new Frame_UserRoleFunc_blockInfo[1];
                ControlList[0] = new Frame_UserRoleFunc_blockInfo();
                ControlList[0].ID = "FuncBlock_New_ID";
                ControlList[0] = (Frame_UserRoleFunc_blockInfo)Page.LoadControl("~/Frame/UserRoleFunc/blockInfo.ascx");
                
                ControlList[0].DdlParentItem = "";
                ControlList[0].DdlBlockLevel = "0";
                ControlList[0].DdlBlockTypeID = "";
                ControlList[0].TxtItemName = "";
                ControlList[0].TxtAliasName = "";
                ControlList[0].TxtFunctionCode = "0";
                ControlList[0].TxtTitle = "";
                ControlList[0].TxtRemarks = "";
                ControlList[0].SetFuncItemVisable(true);
                ControlList[0].SetAllItemEnable(AddNew,true);
                this.plhMain.Controls.Add(ControlList[0]);
                EditData("", "");
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
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null)
                {
                    if (ControlList[i].DdlBlockTypeID.ToUpper() == "NewBlock".ToUpper())
                    {
                        ControlList[i].SetAllItemEnable(true, AddNew);
                    }
                    else
	                {
                        ControlList[i].SetAllItemEnable(true);
	                }
                }
            }

            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    string getAllIdItem(string id, DataTable dt)
    {
        if (!allIdItemStr.Contains(id))
        {
            if (allIdItemStr.Length > 0)
            {
                allIdItemStr += "," + id;
            }
            else
            {
                allIdItemStr += id;
            }
        }
        if (dt != null)
        {
            DataRow[] drs = dt.Select("PID =" + id);
            for (int i = 0; i < drs.Length; i++)
            {
                getAllIdItem(drs[i]["ID"].ToString(), dt);

            }
        }
        return allIdItemStr;
    }

    public bool SaveData(object obj, string prameter)
    {
        try
        {
            string currBlockTypeID = "";
            DataTable myDt = pDataIO.GetDataTable("select * from FunctionTable", "FunctionTable");
            //DataTable myDt = pDataIO.GetDataTable(queryStr, "FunctionTable");
            DataRow[] blockTypeDrs = myDt.Select("ItemName='" + ControlList[0].DdlBlockTypeID + "' and blocklevel =0");
            if (blockTypeDrs.Length > 0)
            {
                currBlockTypeID = blockTypeDrs[0]["BlockTypeID"].ToString();
            }
            else
            {
                DataRow[] drss = myDt.Select("", "BlockTypeID Desc");
                if (drss.Length > 0)
                {
                    currBlockTypeID = (Convert.ToInt32(drss[0]["BlockTypeID"]) + 1).ToString();
                }
            }
            DataRow[] drs = myDt.Select("ID=" + currID);
            if (drs.Length == 1)
            {
                //BlockLevel	BlockTypeID	ItemName	AliasName	Title	FunctionCode	Remarks
                if (ControlList[0].DdlBlockLevel == "0")
                {
                    drs[0]["BlockLevel"] = "0";
                    drs[0]["Title"] = ControlList[0].TxtTitle;
                    drs[0]["FunctionCode"] = ControlList[0].TxtFunctionCode;
                    drs[0]["Remarks"] = ControlList[0].TxtRemarks;
                }
                else
                {
                    drs[0]["BlockLevel"] = ControlList[0].DdlBlockLevel;
                    drs[0]["Title"] = ControlList[0].TxtTitle;
                    drs[0]["FunctionCode"] = ControlList[0].TxtFunctionCode;
                    drs[0]["Remarks"] = ControlList[0].TxtRemarks;
                    DataRow[] pidDrs = myDt.Select("ItemName='" + ControlList[0].DdlParentItem + "' and BlockTypeID =" + currBlockTypeID, "ID");
                    string itemPID = "-1";
                    if (pidDrs.Length > 0)
                    {
                        itemPID = pidDrs[0]["ID"].ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!获取上一级失败')</Script>", false);
                        return false;
                    }
                    drs[0]["PID"] = itemPID; 
                }
                drs[0]["BlockTypeID"] = currBlockTypeID;
                drs[0]["AliasName"] = ControlList[0].TxtAliasName;
                drs[0]["ItemName"] = ControlList[0].TxtItemName;

                string effectIDItems = getAllIdItem(drs[0]["ID"].ToString(), myDt);
                if (effectIDItems.Length > 0)
                {
                    int effectRowCount = 0;
                    DataRow[] effectDrs = myDt.Select("ID in (" + effectIDItems + ")");
                    foreach (DataRow pDr in effectDrs)
                    {
                        pDr["BlockTypeID"] = currBlockTypeID;
                        effectRowCount++;
                    } 
                }

                if (pDataIO.UpdateDataTable(queryStr, myDt))
                {
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('Error!数据更新失败!');location.href='" + Request.Url.ToString() + "'</Script>", false);
                }

            }
            else if (AddNew)
            {
                #region //New
                long currNewID = pDataIO.GetLastInsertData("FunctionTable") + 1;
                DataRow newDr = myDt.NewRow();
                
                newDr["ID"] = currNewID;
                newDr["BlockTypeID"] = currBlockTypeID;
                newDr["AliasName"] = ControlList[0].TxtAliasName;
                newDr["ItemName"] = ControlList[0].TxtItemName;
                if (ControlList[0].DdlBlockLevel != "0")
                {//ControlList[i].DdlParentItem = parentDt.Select("ID=" + pReaderTable.Rows[i]["PID"].ToString())[0]["ItemName"].ToString();
                    DataRow[] pidDrs = myDt.Select("ItemName='" + ControlList[0].DdlParentItem + "' and BlockTypeID =" + currBlockTypeID,"ID");
                    string itemPID = "-1";
                    if (pidDrs.Length > 0)
                    {
                        itemPID = pidDrs[0]["ID"].ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!获取上一级失败')</Script>", false);
                        return false;
                    }
                    newDr["BlockLevel"] = ControlList[0].DdlBlockLevel;
                    newDr["Title"] = ControlList[0].TxtTitle;
                    newDr["FunctionCode"] = ControlList[0].TxtFunctionCode;
                    newDr["Remarks"] = ControlList[0].TxtRemarks;
                    newDr["PID"] = itemPID;                    
                }
                else
                {
                    newDr["Title"] = ControlList[0].TxtTitle;
                    newDr["FunctionCode"] = ControlList[0].TxtFunctionCode;
                    newDr["Remarks"] = ControlList[0].TxtRemarks;
                }
                myDt.Rows.Add(newDr);
                long newID = -1;
                if (pDataIO.UpdateDataTable(queryStr, myDt, out newID))
                {
                    Response.Redirect("~/WebFiles/Management/FuncList.aspx?", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('Error!数据更新失败!');location.href='" + Request.Url.ToString() + "'</Script>", false);
                }

                #endregion
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!当前项已被删除!')</Script>", false);
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
                Response.Redirect("~/WebFiles/Management/FuncList.aspx?", true);
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

    public void ConfigOptionButtonsVisible(bool isSaveMode = false)
    {
        OptionButtons1.ConfigBtSaveVisible = isSaveMode;
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtEditVisible = !isSaveMode;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = isSaveMode;
    }
}