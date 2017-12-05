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
public partial class WebFiles_OperateLog_OPLogs : BasePage
{
    public string id;
    public DataIO mysql;
    public DataTable dt = new DataTable();
    string funcItemName = "OPLogsInfor";
    private string logTracingString = "";
    private SortedList<int, string> OPLogsID = new SortedList<int, string>();
    bool flag;

    protected void Page_Load(object sender, EventArgs e)
    {        
        IsSessionNull();
        SetSessionBlockType(7);
        id = Request.QueryString["uId"];
        Label7.Text = Session["CurrUsername"].ToString();
        Label8.Text = Session["LogInTime"].ToString();
        Label9.Text = Session["logininfo"].ToString();
        if (Label7.Text == "&nbsp;")
        {
            Label7.Text = "";
        }
        if (Label8.Text == "&nbsp;")
        {
            Label8.Text = "";
        }
        if (Label9.Text == "&nbsp;")
        {
            Label9.Text = "";
        }

        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        mysql = new SqlManager(serverName, dbName, userId, pwd);
        string selectcmd = "select OperationLogs.[ModifyTime],OperationLogs.[BlockType],OperationLogs.[OpType],TracingInfo,ID from OperationLogs where OperationLogs.[PID]= '" + id + "' ";
        //delete ,OperationLogs.[OperateItem],OperationLogs.[ChlidItem]
        mysql.OpenDatabase(true);
        creattNavi();
        dt = mysql.GetDataTable(selectcmd, "OperationLogs");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            OPLogsID.Add(i,dt.Rows [i]["ID"].ToString());
        }

        dt.Columns.RemoveAt(4);

            if (dt.Rows.Count != 0)
            {
                flag = true;

                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.HeaderRow.Style.Add("word-break", "keep-all");
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        GridView1.Rows[i].BackColor = System.Drawing.Color.Azure;

                    }
                    for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                    {
                        GridView1.Rows[i].Cells[j].Wrap = false;
                        GridView1.Rows[i].Cells[j].Style.Add("word-break", "keep-all");
                    }
                }                
            }
            else
            {
                flag = false;

                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Rows[0].Cells[3].Controls.Clear();
            }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index;
        int page;
        int LogId;
        string oplogsid;
        string modifytime = "";
        string optype = "";
        string blocktype = "";
        //string OperateItem = "";
        //string ChlidItem = "";
        if (e.CommandName == "DetailLogs")
        {
            index = Convert.ToInt32(e.CommandArgument);
            page = GridView1.PageIndex;
            LogId = page * 20 + index;
            modifytime = GridView1.Rows[index].Cells[0].Text;
            optype = GridView1.Rows[index].Cells[1].Text;
            blocktype = GridView1.Rows[index].Cells[2].Text;          
            //OperateItem = GridView1.Rows[index].Cells[3].Text;
            //ChlidItem = GridView1.Rows[index].Cells[4].Text;
            if (modifytime == "&nbsp;")
            {
                modifytime = "";

            }
            if (optype == "&nbsp;")
            {
                optype = "";

            }
            if (blocktype == "&nbsp;")
            {
                blocktype = "";
                
            }
            //if (OperateItem == "&nbsp;")
            //{
            //    OperateItem = "";

            //}
            //if (ChlidItem == "&nbsp;")
            //{
            //    ChlidItem = "";

            //}

            //string selectcmd = "";
            //string selectcmd_First = "select OperationLogs.[ID]";
            //string selectcmd_Second = " FROM OperationLogs WHERE ";
            //selectcmd = selectcmd_First + selectcmd_Second +                                 
            //                    "OperationLogs.[ModifyTime] = '" + modifytime + "' AND OperationLogs.[OpType]='" + optype + "' AND OperationLogs.[BlockType]='" + blocktype + "'  AND OperationLogs.[PID]='" + id + "'";    // "' AND cast (OperationLogs.[OperateItem] as nvarchar(500))='" + OperateItem + "' AND OperationLogs.[ChlidItem]='" + ChlidItem +
            //mysql.OpenDatabase(true);
            //dt = mysql.GetDataTable(selectcmd, "OperationLogs");
            if (OPLogsID[LogId] != " ")
            {

                oplogsid = OPLogsID[LogId];
                Session["modifytime"] = modifytime;
                Session["optype"] = optype;
                Session["blocktype1"] = blocktype;
                //Session["OperateItem"] = OperateItem;
                //Session["ChlidItem"] = ChlidItem;
                Response.Redirect("~/WebFiles/OperateLog/DetailLogs.aspx?uId=" + oplogsid.Trim());
            }
            else
            {
                oplogsid = " ";
                Session["modifytime"] = modifytime;
                Session["optype"] = optype;
                Session["blocktype1"] = blocktype;
                //Session["OperateItem"] = OperateItem;
                //Session["ChlidItem"] = ChlidItem;
                Response.Redirect("~/WebFiles/OperateLog/DetailLogs.aspx?uId=" + oplogsid.Trim());

            }
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton TestDataLinkButton = (LinkButton)e.Row.FindControl("DetailLogsLinkButton");
            TestDataLinkButton.CommandArgument = e.Row.RowIndex.ToString();

            if (flag == true)
            {
                TestDataLinkButton.Text = "View";
                TestDataLinkButton.Enabled = true;
            }             
        }
    }
      
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        GridView1.HeaderRow.Style.Add("word-break", "keep-all");
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (i % 2 == 0)
            {
                GridView1.Rows[i].BackColor = System.Drawing.Color.Azure;

            }
            for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
            {
                GridView1.Rows[i].Cells[j].Wrap = false;
                GridView1.Rows[i].Cells[j].Style.Add("word-break", "keep-all");
            }
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();

            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "UserOPLogs", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}