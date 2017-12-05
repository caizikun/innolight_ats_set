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

public partial class WebFiles_UserRoleFunc_FuncList : BasePage
{
    const string funcItemName = "功能块列表";
    string queryStr = "select * from FunctionTable order by BlockTypeID,BlockLevel,ID";
    DataIO pDataIO;
    ASCXfunctionList[] ControlList;
    //LinkButton[] lnkBtnList;    //ItemName可能对应多个ID,故此处的删除时调用lnkBtnList[i].Text
    DataTable myDt;
    private string logTracingString = "";
    string allIdDelStr = "";

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
        ConfigOptionButtonsVisible();

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
            //pLst.Add("Item");
            //pLst.Add("Description");
            //pLst.Add("Function");
            //pLst.Add("View AscxFiles");
            

            //TableHeaderRow thr = pCommCtrl.CreateMyTableHeader(pLst);
            //if (thr != null)
            //{
            //    pTable.Controls.Add(thr);
            //}

            myDt = pDataIO.GetDataTable(filterStr, "FunctionTable");
            if (myDt.Rows.Count == 0)
            {
                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
            }
            int myCount = myDt.Rows.Count;  //myDt.DefaultView.ToTable(true, "ItemName").Rows.Count;
            if (myCount==0)
            {
                  ControlList = new ASCXfunctionList[1];
               for (int i = 0; i < ControlList.Length; i++)
            {
               
                ControlList[i] = (ASCXfunctionList)Page.LoadControl("~/Frame/UserRoleFunc/functionList.ascx");
                ControlList[i].ContentTRVisible = false;
                this.plhMain.Controls.Add(ControlList[i]);

            }
            } 
            else
            {
                  ControlList = new ASCXfunctionList[myCount];
            //lnkBtnList = new LinkButton[myCount];
            //List<string> lstItemName = new List<string>();

            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                //150413 改为全部显示
                //if (lstItemName.Contains(myDt.Rows[i]["ItemName"].ToString()))
                //{
                //    continue;
                //}

                ControlList[i] = (ASCXfunctionList)Page.LoadControl("~/Frame/UserRoleFunc/functionList.ascx");
                ControlList[i].ID = "ControlList_" + myDt.Rows[i]["ID"].ToString();
                ControlList[i].ConfigLink1Text= myDt.Rows[i]["ItemName"].ToString();
                ControlList[i].ConfigLinkPostBackURL = "~/WebFiles/Management/FuncBlockInfo.aspx?uId=" + myDt.Rows[i]["ID"];
                ControlList[i].ConfigLB1Text= myDt.Rows[i]["AliasName"].ToString();             

                Label lblFunc = new Label();
                //if (myDt.Rows[i]["BlockLevel"].ToString() == "0")
                //{
                ControlList[i].ConfigLB2Text = myDt.Rows[i]["Remarks"].ToString();
                //}
                //else
                //{
                //    ControlList[i].ConfigLB2Text = "";
                //}
                if (i>=1)
                {
                   ControlList[i].LBTHTitleVisible(false);
                   if (i % 2 != 0)
                   {
                       ControlList[i].TrBackgroundColor = "#F2F2F2";
                   }
                }

                //TableCell[] tcs = new TableCell[5];

                //tcs[0] = pCommCtrl.CreateMyTableCell(ControlList[i]);
                //tcs[1] = pCommCtrl.CreateMyTableCell(lnkBtnList[i]);
                //tcs[2] = pCommCtrl.CreateMyTableCell(lblAliasName);
                //tcs[3] = pCommCtrl.CreateMyTableCell(lblFunc);
                //tcs[4] = pCommCtrl.CreateMyTableCell(pLinkBtnUserRole);
                
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
        if (myDt.Rows.Count<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = !isSaveMode;
        }
        
        OptionButtons1.ConfigBtCancelVisible = false;
    }

    public bool AddData(object obj, string prameter)
    {
        Response.Redirect("~/WebFiles/Management/FuncBlockInfo.aspx?AddNew=true", true);
        return true;
    }
    
    string getAllDeleteItem(string id,DataTable dt)
    {
        if (!allIdDelStr.Contains(id))
        {
            if (allIdDelStr.Length > 0)
            {
                allIdDelStr += "," + id;
            }
            else
            {
                allIdDelStr += id;
            }
        }
        if (dt != null)
        {
            DataRow [] drs= dt.Select("PID =" + id);
            for (int i = 0; i < drs.Length; i++)
            {
                getAllDeleteItem(drs[i]["ID"].ToString(), dt);
                
            }
        }
        return allIdDelStr;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;
        string delStrIDs = "";
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
                            //150520 取消此部分:一个ItemName可能对应多个ID,故此处的删除时调用lnkBtnList[i].Text
                            if ((myDt.Rows[j]["id"].ToString()).ToLower() == ControlList[i].ID.ToString().Replace("ControlList_", "").ToLower())
                            {
                                //150520 取消此部分:DataRow[] drs = myDt.Select("itemName ='" + lnkBtnList[i].Text + "'");
                                DataRow[] drs = myDt.Select("ID =" + myDt.Rows[j]["id"].ToString());    // ControlList[i].ID
                                
                                foreach (DataRow pDr in drs)
                                {
                                    delStrIDs = getAllDeleteItem(pDr["id"].ToString(), myDt);
                                    //pDr.Delete();
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
                ClientScript.RegisterStartupScript(GetType(), "Message", "<script>alert('请至少选择一个！');</script>", false);
                return false;
            }
            else
            {
                int deletedRowCount = 0;
                DataRow[] delDrs = myDt.Select("ID in (" + delStrIDs + ")");
                foreach (DataRow pDr in delDrs)
                {
                    pDr.Delete();
                    deletedRowCount++;
                }  
                pDataIO.UpdateDataTable(queryStr, myDt);
                Response.Redirect(Request.Url.ToString());
                return true;
            }
            
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