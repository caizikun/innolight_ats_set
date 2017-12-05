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

public partial class WebFiles_TestReport_TestControll : BasePage
{
    public string id;
    DataIO mysql;
    public DataTable dt;
    public string initsn;
    string funcItemName = "TestControl";
    private string logTracingString = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        SetSessionBlockType(7);
        initsn = Session["initsn"].ToString();
        Label4.Text= Session["sn"].ToString();
        Label5.Text = Session["testplansel"].ToString();
        Label6.Text = Session["startime"].ToString();
        if (Label5.Text == "&nbsp;")
        {
            Label5.Text = "";
        }
        if (Label6.Text == "&nbsp;")
        {
            Label6.Text = "";
        }
        id = Request.QueryString["uId"];
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        mysql = new SqlManager(serverName, dbName, userId, pwd);
        //if (!this.IsPostBack)
        //{
        string selectcmd = "select distinct Temp,TopoLogRecord.[Voltage] as Vcc,Channel,(case when CtrlType='1' then 'LP' when CtrlType='2' then 'FMT' else 'LP&FMT' end) as  CtrlType,(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as Result ,ID from TopoLogRecord where RunRecordID= '" + id + "' ";

        mysql.OpenDatabase(true);
        creattNavi();
        dt = mysql.GetDataTable(selectcmd, "TopoLogRecord");
     
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
                    bool strpassfail;

                    if (GridView1.Rows[i].Cells[4].Text == "pass")
                    {
                        strpassfail = true;
                    }
                    else
                    {
                        GridView1.Rows[i].Cells[4].BackColor = System.Drawing.Color.Red;
                        strpassfail = false;
                    }

                    string selectcmd1 = "";
                    string selectcmd_First = "select TopoTestData.[PID],TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[Result],TopoLogRecord.[RunRecordID] ";

                    string selectcmd_Second = "FROM TopoLogRecord,TopoTestData ";
                    string selectcmd_Third = " WHERE (TopoTestData.PID)=[TopoLogRecord].[ID] AND [TopoLogRecord].[CtrlType]=2 ";
                    selectcmd1 = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                      " AND [TopoLogRecord].[ID] ='" + GridView1.Rows[i].Cells[7].Text + "' ";

                    
                    DataTable dt1 = mysql.GetDataTable(selectcmd1, "TopoTestData");
                    if (dt1.Rows.Count == 0)
                    {
                        GridView1.Rows[i].Cells[5].Controls.Clear();
                        GridView1.Rows[i].Cells[5].Text = "NA";
                    }
                    selectcmd_First = "select TopoProcData.[PID],TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[Result],TopoLogRecord.[RunRecordID] ";

                    selectcmd_Second = "FROM TopoLogRecord,TopoProcData ";

                    selectcmd_Third = " WHERE (TopoProcData.PID)=[TopoLogRecord].[ID] AND [TopoLogRecord].[CtrlType]=1 ";
                    selectcmd1 = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                      "AND [TopoLogRecord].[ID] ='" + GridView1.Rows[i].Cells[7].Text + "' ";
                    DataTable dt2 = mysql.GetDataTable(selectcmd1, "TopoProcData");
                    if (dt2.Rows.Count == 0)
                    {
                        GridView1.Rows[i].Cells[6].Controls.Clear();
                        GridView1.Rows[i].Cells[6].Text = "NA";
                    }
                }

            }
            else
            {
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Rows[0].Cells[5].Controls.Clear();
                GridView1.Rows[0].Cells[6].Controls.Clear();
                //Response.Redirect("~/WebFiles/TestReport/Query.aspx?uSN=" + TextBox1.Text);
                //Response.Write("<script>alert('can not find testdata！');</script>");
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

            bool strpassfail;

            if (GridView1.Rows[i].Cells[4].Text == "pass")
            {
                strpassfail = true;
            }
            else
            {
                GridView1.Rows[i].Cells[4].BackColor = System.Drawing.Color.Red;
                strpassfail = false;
            }

            string selectcmd1 = "";
            string selectcmd_First = "select TopoTestData.[PID],TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[Result],TopoLogRecord.[RunRecordID] ";

            string selectcmd_Second = "FROM TopoLogRecord,TopoTestData ";
            string selectcmd_Third = " WHERE (TopoTestData.PID)=[TopoLogRecord].[ID] AND [TopoLogRecord].[CtrlType]=2 ";
            selectcmd1 = selectcmd_First + selectcmd_Second + selectcmd_Third +
                              "AND [TopoLogRecord].[ID] ='" + GridView1.Rows[i].Cells[7].Text + "' ";


            DataTable dt1 = mysql.GetDataTable(selectcmd1, "TopoTestData");
            if (dt1.Rows.Count == 0)
            {
                GridView1.Rows[i].Cells[5].Controls.Clear();
                GridView1.Rows[i].Cells[5].Text = "NA";
            }
            selectcmd_First = "select TopoProcData.[PID],TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[Result],TopoLogRecord.[RunRecordID] ";

            selectcmd_Second = "FROM TopoLogRecord,TopoProcData ";

            selectcmd_Third = " WHERE (TopoProcData.PID)=[TopoLogRecord].[ID] AND [TopoLogRecord].[CtrlType]=1 ";
            selectcmd1 = selectcmd_First + selectcmd_Second + selectcmd_Third +
                              "AND [TopoLogRecord].[ID] ='" + GridView1.Rows[i].Cells[7].Text + "'";
            DataTable dt2 = mysql.GetDataTable(selectcmd1, "TopoTestData");
            if (dt2.Rows.Count == 0)
            {
                GridView1.Rows[i].Cells[6].Controls.Clear();
                GridView1.Rows[i].Cells[6].Text = "NA";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebFiles/TestReport/Query.aspx?uSN=" + initsn);

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        ////testplansel = GridView1.Rows[GridView1.SelectedIndex].Cells[0].Text;
        //string strpassfail;
        //GridView1.Rows[e.NewSelectedIndex].BackColor = System.Drawing.Color.Red;

        //temp = GridView1.Rows[e.NewSelectedIndex].Cells[0].Text;
        //vcc = GridView1.Rows[e.NewSelectedIndex].Cells[1].Text;
        //channel = GridView1.Rows[e.NewSelectedIndex].Cells[2].Text;
        //strpassfail = GridView1.Rows[e.NewSelectedIndex].Cells[3].Text;
        //if (strpassfail == "pass")
        //{
        //    passfial = true;
        //}
        //else
        //{
        //    passfial = false;
        //}
        ////testplansel = this.GridView1.SelectedRow.Cells[0].Text;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index;
        string strpassfail;
        string testdataid;
        string temp = "";
        string vcc = "";
        string channel = "";
        bool passfial = false;
        string ctrltype = "";
        if (GridView1.Rows.Count != 0)
        {
            
            if (e.CommandName == "TestData")
            {
                index = Convert.ToInt32(e.CommandArgument);
                testdataid = "";
                temp = GridView1.Rows[index].Cells[0].Text;
                vcc = GridView1.Rows[index].Cells[1].Text;
                channel = GridView1.Rows[index].Cells[2].Text;
                strpassfail = GridView1.Rows[index].Cells[4].Text;
                ctrltype = GridView1.Rows[index].Cells[3].Text;
                if (strpassfail == "pass")
                {
                    passfial = true;
                }
                else
                {
                    passfial = false;
                }

                if (temp == "&nbsp;")
                {
                    temp = "";

                }
                if (vcc == "&nbsp;")
                {
                    vcc = "";

                }
                if (channel == "&nbsp;")
                {
                    channel = "";

                }
                //testplansel = this.GridView1.SelectedRow.Cells[0].Text;
                string selectcmd = "";
                string selectcmd_First = "select TopoTestData.[PID],TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[Result],TopoLogRecord.[RunRecordID] ";

                string selectcmd_Second = "FROM TopoLogRecord,TopoTestData ";
                string selectcmd_Third = " WHERE (TopoTestData.PID)=[TopoLogRecord].[ID] AND [TopoLogRecord].[CtrlType]=2 ";
                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                  "AND [TopoLogRecord].[ID] ='" + GridView1.Rows[index].Cells[7].Text + "'";
                
                mysql.OpenDatabase(true);
                dt = mysql.GetDataTable(selectcmd, "TopoTestData");
                if (dt.Rows.Count != 0)
                {
                    testdataid = dt.Rows[0]["PID"].ToString();
                    //Session["sn"] = TextBox1.Text;
                    //Session["testplansel"] = TextBox2.Text;
                    //Session["startime"] = TextBox3.Text;
                    Session["temp"] = temp;
                    Session["vcc"] = vcc;
                    Session["channel"] = channel;
                    Session["pid"] = id;
                    Session["ctrltype"] = ctrltype;
                    Response.Redirect("~/WebFiles/TestReport/detailTestData.aspx?uId=" + testdataid.Trim());
                }
                else
                {
                    testdataid = " ";
                    //Session["sn"] = TextBox1.Text;
                    //Session["testplansel"] = TextBox2.Text;
                    //Session["startime"] = TextBox3.Text;
                    Session["temp"] = temp;
                    Session["vcc"] = vcc;
                    Session["channel"] = channel;
                    Session["pid"] = id;
                    Session["ctrltype"] = ctrltype;
                    Response.Redirect("~/WebFiles/TestReport/detailTestData.aspx?uId=" + testdataid.Trim());
                    //Response.Write("<script>alert('can not find testdata！');</script>");
                    
                }


            }

            if (e.CommandName == "LPData")
            {
                index = Convert.ToInt32(e.CommandArgument);
                testdataid = "";
                temp = GridView1.Rows[index].Cells[0].Text;
                vcc = GridView1.Rows[index].Cells[1].Text;
                channel = GridView1.Rows[index].Cells[2].Text;
                strpassfail = GridView1.Rows[index].Cells[4].Text;
                ctrltype = GridView1.Rows[index].Cells[3].Text;
                if (strpassfail == "pass")
                {
                    passfial = true;
                }
                else
                {
                    passfial = false;
                }

                //testplansel = this.GridView1.SelectedRow.Cells[0].Text;
                string selectcmd = "";
                string selectcmd_First = "select TopoProcData.[PID],TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[Result],TopoLogRecord.[RunRecordID] ";

                string selectcmd_Second = "FROM TopoLogRecord,TopoProcData ";
                string selectcmd_Third = " WHERE (TopoProcData.PID)=[TopoLogRecord].[ID] AND [TopoLogRecord].[CtrlType]=1 ";
                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                  "AND [TopoLogRecord].[ID] ='" + GridView1.Rows[index].Cells[7].Text + "' ";
                
                mysql.OpenDatabase(true);
                dt = mysql.GetDataTable(selectcmd, "TopoTestData");
                if (dt.Rows.Count != 0)
                {
                    testdataid = dt.Rows[0]["PID"].ToString();
                    //Session["sn"] = TextBox1.Text;
                    //Session["testplansel"] = TextBox2.Text;
                    //Session["startime"] = TextBox3.Text;
                    Session["temp"] = temp;
                    Session["vcc"] = vcc;
                    Session["channel"] = channel;
                    Session["pid"] = id;
                    Session["ctrltype"] = ctrltype;
                    Response.Redirect("~/WebFiles/TestReport/detailTestData.aspx?uId=" + testdataid.Trim());
                }
                else
                {
                    testdataid = " ";
                    //Session["sn"] = TextBox1.Text;
                    //Session["testplansel"] = TextBox2.Text;
                    //Session["startime"] = TextBox3.Text;
                    Session["temp"] = temp;
                    Session["vcc"] = vcc;
                    Session["channel"] = channel;
                    Session["pid"] = id;
                    Session["ctrltype"] = ctrltype;
                    Response.Redirect("~/WebFiles/TestReport/detailTestData.aspx?uId=" + testdataid.Trim());
                    //Response.Write("<script>alert('can not find testdata！');</script>");

                }


            }
           
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton TestDataLinkButton = (LinkButton)e.Row.FindControl("TestDataLinkButton");
            TestDataLinkButton.CommandArgument = e.Row.RowIndex.ToString();
            
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton TestDataLinkButton = (LinkButton)e.Row.FindControl("LPDataLinkButton");
            TestDataLinkButton.CommandArgument = e.Row.RowIndex.ToString();

        }
    }
    public void creattNavi()
    {
        try
        {

            CommCtrl pCtrl = new CommCtrl();
            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "Query", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}

