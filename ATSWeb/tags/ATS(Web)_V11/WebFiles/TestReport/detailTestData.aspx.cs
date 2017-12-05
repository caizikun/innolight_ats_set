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
        SetSessionBlockType(7);
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
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        mysql = new SqlManager(serverName, dbName, userId, pwd);
        mysql.OpenDatabase(true);
       
        if (ctrltype == "FMT")
        {
            string selectcmd = "select ItemName,ItemValue,(case when Result='true' then 'pass'else 'fail' end) as Result,SpecMin,SpecMax from TopoTestData where PID= '" + id + "' ";
            
        dt = mysql.GetDataTable(selectcmd, "TopoTestData");
        funcItemName = "DetailTestDataFMT";
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
            string selectcmd = "select ModelName,ItemName,ItemValue from TopoProcData where PID= '" + id + "' ";

            mysql.OpenDatabase(true);
            funcItemName = "DetailTestDataLP";
            
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
            if (GridView1.Rows[i].Cells[2].Text == "fail")
            {
                GridView1.Rows[i].Cells[2].BackColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebFiles/TestReport/TestControll.aspx?uId=" + uid);

    }
    public void creattNavi()
    {
        try
        {
          
            CommCtrl pCtrl = new CommCtrl();
            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "TestControl", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}