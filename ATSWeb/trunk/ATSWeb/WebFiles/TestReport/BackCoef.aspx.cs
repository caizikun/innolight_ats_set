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

public partial class WebFiles_TestReport_BackCoef : BasePage
{
    string id;
    DataIO mysql;
    public DataTable dt;
    string funcItemName = "备份系数";
    private string logTracingString = "";
    public string initsn;
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(2);
        initsn = Session["initsn"].ToString();
        Label4.Text = Session["sn"].ToString();
        Label5.Text = Session["testplansel"].ToString();
        Label6.Text = Session["startime"].ToString();
        id = Request.QueryString["uId"];
       
        if (Label5.Text == "&nbsp;")
        {
            Label5.Text = "";
        }
        if (Label6.Text == "&nbsp;")
        {
            Label6.Text = "";
        }
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
        mysql = new SqlManager(serverName, dbName, userId, pwd);
        creattNavi();
        string selectcmd = "select Page as 页数,StartAddr as 开始地址,ItemValue as 值,ItemSize as 长度 from TopoTestCoefBackup where PID= '" + id + "' order by Page ASC,StartAddr ASC ";

        mysql.OpenDatabase(true);
        dt = mysql.GetDataTable(selectcmd, "TopoTestCoefBackup");

        if (dt.Rows.Count != 0)
        {
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
            dt.Rows.Add(dt.NewRow());
            GridView1.DataSource = dt;
            GridView1.DataBind();
            
            //Response.Write("<script>alert('can not find testdata！');</script>");
            //Response.Redirect("~/WebFiles/TestReport/Query.aspx?uSN=" + TextBox1.Text);
            
        }
    }

    //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();
    //    GridView1.HeaderRow.Style.Add("word-break", "keep-all");
    //    for (int i = 0; i < GridView1.Rows.Count; i++)
    //    {
    //        if (i % 2 == 0)
    //        {
    //            GridView1.Rows[i].BackColor = System.Drawing.Color.Azure;

    //        }
    //        for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
    //        {
    //            GridView1.Rows[i].Cells[j].Wrap = false;
    //            GridView1.Rows[i].Cells[j].Style.Add("word-break", "keep-all");
    //        }
    //    }
    //}
    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();
            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "查询", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}