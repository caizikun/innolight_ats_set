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
public partial class WebFiles_TestReport_Query : BasePage
{
    DataIO mysql;
    public DataTable dt;
    string funcItemName = "查询";
    private string logTracingString = "";

    private SortedList<int, bool> CoefFlag = new SortedList<int, bool>();    //0722

    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(2);
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
        string sntest = Request.QueryString["uSN"];
        if (TextBox1.Text == "")
        {
            TextBox1.Text = sntest;
        }
        if (TextBox1.Text != "")
        {
            gridviewdatabind();
        }
        
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["initsn"] = TextBox1.Text;
        //Session["QueryPage"] = 0;
        gridviewdatabind();
    }

    public void gridviewdatabind()
    {
        if (TextBox1.Text == "")
        {
            this.Page .ClientScript.RegisterStartupScript(GetType(), " ", "alert('请输入SN！');",true);        
        }
        else
        {

            string selectcmd = "";
            string selectcmd_First = "select distinct TopoTestPlan.[ItemName] as TestPlan, TopoRunRecordTable.[SN],TopoRunRecordTable.[StartTime], TopoRunRecordTable.[EndTime],TopoRunRecordTable.[ID] ";
            string selectcmd_Second = "FROM TopoTestPlan,TopoRunRecordTable ";
            string selectcmd_Third = " WHERE TopoRunRecordTable.PID=[TopoTestPlan].[ID] ";
            selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                              "AND TopoRunRecordTable.[SN] like '" + TextBox1.Text + "%" + "'";
            selectcmd += " order by TopoRunRecordTable.[StartTime] DESC";
                    
            mysql.OpenDatabase(true);
            dt = mysql.GetDataTable(selectcmd, "TopoTestData");

            CoefFlag.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CoefFlag.Add(Convert.ToInt32(dt.Rows[i]["ID"]), false); 
            }

            selectcmd = "select distinct TopoTestCoefBackup.PID FROM TopoTestCoefBackup";
            DataTable dt1 = mysql.GetDataTable(selectcmd, "");

            for (int i = 0; i < CoefFlag.Count; i++)
            {
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (CoefFlag.Keys[i] == Convert.ToInt32(dt1.Rows[j]["PID"]))
                    {
                        CoefFlag[CoefFlag.Keys[i]] = true;
                        break;
                    }
                }
                    
            }

            if (dt.Rows.Count != 0)
            {
                dt.Columns.Add("Result");
                selectcmd = "select distinct TopoRunRecordTable.ID,TopoRunRecordTable.SN,TopoRunRecordTable.StartTime,TopoLogRecord.Result from TopoRunRecordTable full join TopoLogRecord on TopoRunRecordTable.ID=TopoLogRecord.RunRecordID"
                          + " where TopoRunRecordTable.SN like '" + TextBox1.Text + "%" + "' and TopoRunRecordTable.PID!=0 order by TopoRunRecordTable.StartTime DESC";
                mysql.OpenDatabase(true);
                DataTable dtResult = mysql.GetDataTable(selectcmd, "dtResult");
               
                for (int i = 0, count = 0; i < dtResult.Rows.Count; i++,count++)
                {
                    if (i != dtResult.Rows.Count - 1 && Convert.ToInt32(dtResult.Rows[i]["ID"]) == Convert.ToInt32(dtResult.Rows[i + 1]["ID"]))
                    {
                        dt.Rows[count]["Result"] = "fail";
                        i++;
                    }
                    else
                    {
                        if (dtResult.Rows[i]["Result"].GetType().Name == "DBNull")
                        {
                            dt.Rows[count]["Result"] = "NA";
                        }                        
                        else if (Convert.ToBoolean(dtResult.Rows[i]["Result"]) == true)
                        {
                            dt.Rows[count]["Result"] = "pass";
                        }
                        else if (Convert.ToBoolean(dtResult.Rows[i]["Result"]) == false)
                        {
                            dt.Rows[count]["Result"] = "fail";
                        }
                    }
                }

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

                    if (GridView1.Rows[i].Cells[4].Text == "fail")
                    {
                        GridView1.Rows[i].Cells[4].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                dt.Columns.Add("Result");
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Rows[0].Cells[5].Controls.Clear();
                GridView1.Rows[0].Cells[6].Controls.Clear();
                //Response.Write("<script>alert('can not find testdata！');</script>");

            }
        }
    
    }
      
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {               
            int index;
            //string strpassfail;
            //string teatdataid;
            string testplansel = "";
            string startime = "";
            string stoptime = "";
            string id = "";
            //bool passfial = false;
            string sn = "";
            if (e.CommandName == "TestControll")
            {
                //teatdataid = "";
                index = Convert.ToInt32(e.CommandArgument);
                testplansel = GridView1.Rows[index].Cells[1].Text;
                startime = GridView1.Rows[index].Cells[2].Text;
                stoptime = GridView1.Rows[index].Cells[3].Text;
                id = GridView1.Rows[index].Cells[7].Text;
                sn = GridView1.Rows[index].Cells[0].Text;
                //if (strpassfail == "pass")
                //{
                //    passfial = true;
                //}
                //else
                //{
                //    passfial = false;
                //}

                if (testplansel == "&nbsp;")
                {
                    testplansel = "";

                }
                if (startime == "&nbsp;")
                {
                    startime = "";

                }
                if (stoptime == "&nbsp;")
                {
                    stoptime = "";

                }
                if (sn == "&nbsp;")
                {
                    sn = "";

                }

                Session["sn"] = sn;
                Session["testplansel"] = testplansel;
                Session["startime"] = startime;
                Session["initsn"] = TextBox1.Text;
                Response.Redirect("~/WebFiles/TestReport/TestControll.aspx?uId=" + id.Trim());
            }
            else if (e.CommandName == "BackCoef")
            {
                //teatdataid = "";
                index = Convert.ToInt32(e.CommandArgument);
                testplansel = GridView1.Rows[index].Cells[1].Text;
                startime = GridView1.Rows[index].Cells[2].Text;
                stoptime = GridView1.Rows[index].Cells[3].Text;
                id = GridView1.Rows[index].Cells[7].Text;
                //strpassfail = GridView1.Rows[index].Cells[4].Text;
                sn = GridView1.Rows[index].Cells[0].Text;
                //if (strpassfail == "pass")
                //{
                //    passfial = true;
                //}
                //else
                //{
                //    passfial = false;
                //}

                Session["sn"] = sn;
                Session["testplansel"] = testplansel;
                Session["startime"] = startime;
                Session["initsn"] = TextBox1.Text;
                Response.Redirect("~/WebFiles/TestReport/BackCoef.aspx?uId=" + id.Trim());
            }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton TestDataLinkButton = (LinkButton)e.Row.FindControl("TestControllLinkButton");
            TestDataLinkButton.CommandArgument = e.Row.RowIndex.ToString();
            LinkButton BackCoefLinkButton = (LinkButton)e.Row.FindControl("BackCoefLinkButton");
            BackCoefLinkButton.CommandArgument = e.Row.RowIndex.ToString();
                                          
            if (CoefFlag.Count != 0)
            {
                 //int i = GridView1.PageIndex * 20 + e.Row.RowIndex;
                int i = e.Row.RowIndex;

                if (CoefFlag[CoefFlag.Keys[CoefFlag.Count-1-i]] == true)
                {
                    BackCoefLinkButton.Text = "查看";
                    BackCoefLinkButton.Enabled = true;
                }
                else
                {
                    BackCoefLinkButton.Text = "无";
                    BackCoefLinkButton.Enabled = false;
                }       
            }                      
        }
    }

    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();

            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "测试数据", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }

    protected void plhNavi_PreRender(object sender, EventArgs e)
    {
        if (Session["initsn"]!=null)
        {
            TextBox1.Text = Session["initsn"].ToString(); 
            //GridView1.PageIndex = Convert.ToInt32(Session["QueryPage"]);
            gridviewdatabind();
        }
        
    }
}