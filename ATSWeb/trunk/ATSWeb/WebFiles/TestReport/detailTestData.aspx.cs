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

public partial class WebFiles_TestReport_AdvancedSelect : BasePage
{
    public string uid;
    public string id;
    DataIO mysql;
    public DataTable dt;
    public string ctrltype;
    string funcItemName = "";
    private string logTracingString = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(2);
        uid = Session["pid"].ToString();
        Label7.Text = Session["sn"].ToString();
        Label8.Text = Session["testplansel"].ToString();
        Label9.Text = Session["startime"].ToString();
        Label10.Text = Session["temp"].ToString();
        Label11.Text = Session["vcc"].ToString();
        Label12.Text = Session["channel"].ToString();
        if (Label8.Text == "&nbsp;")
        {
            Label8.Text = "";
        }
        if (Label9.Text == "&nbsp;")
        {
            Label9.Text = "";
        }
        id = Request.QueryString["uId"];
        ctrltype = Session["ctrltype"].ToString();
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
        mysql.OpenDatabase(true);
       
        if (ctrltype == "FMT")
        {
            string selectcmd = "select ItemName as 名称,ItemValue as 值,(case when Result='true' then 'pass'else 'fail' end) as 结果,SpecMin as 规格下限,SpecMax as 规格上限 from TopoTestData where PID= '" + id + "' ";
            
        dt = mysql.GetDataTable(selectcmd, "TopoTestData");
        funcItemName = "详细测试数据";
        creattNavi();
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
                    if (GridView1.Rows[i].Cells[2].Text == "fail")
                    {
                        GridView1.Rows[i].Cells[2].BackColor = System.Drawing.Color.Red;
                    }
                   
                }

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //Response.Redirect("~/WebFiles/TestReport/Query.aspx?uSN=" + TextBox1.Text);
                //Response.Write("<script>alert('can not find testdata！');</script>");
            }
        }
        else if (ctrltype == "LP")  
        {
            string selectcmd = "SELECT (SELECT GlobalAllTestModelList.ShowName FROM GlobalAllTestModelList where GlobalAllTestModelList.ItemName = TopoProcData.ModelName) as 测试模型,TopoProcData.ItemName as 名称, TopoProcData.ItemValue as 值 FROM TopoProcData where PID ='" + id + "'";

            mysql.OpenDatabase(true);
            funcItemName = "详细校准数据";
            
            dt = mysql.GetDataTable(selectcmd, "TopoProcData");
            creattNavi();
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
                    if (GridView1.Rows[i].Cells[2].Text == "fail")
                    {
                        GridView1.Rows[i].Cells[2].BackColor = System.Drawing.Color.Red;
                    }

                }

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //Response.Redirect("~/WebFiles/TestReport/Query.aspx?uSN=" + TextBox1.Text);
                //Response.Write("<script>alert('can not find testdata！');</script>");
            }
        
        
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
    //        if (GridView1.Rows[i].Cells[2].Text == "fail")
    //        {
    //            GridView1.Rows[i].Cells[2].BackColor = System.Drawing.Color.Red;
    //        }
    //    }
    //}

    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();
            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "测试流程数据", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}