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

public partial class WebFiles_OperateLog_UserOpInfo : BasePage
{
    //public static string id="";
    public DataIO mysql;
    public DataTable dt = new DataTable();
    string funcItemName = "用户操作日志";
    private string logTracingString = "";
  
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(4);
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
        if (!this.IsPostBack)
        {
            TextBox5.Text = DateTime.Now.AddDays(-5).ToString();
            //TextBox5.Text = "2014/4/7 15:36:01";
            TextBox6.Text = DateTime.Now.ToString();


            string selectcmd = "select distinct (UserName) from UserLoginInfo";
            bool flag = mysql.OpenDatabase(true);
            if (flag == false)
            {
                Response.Write("<script>alert('数据库连接失败！');</script>");
            }
            else
            {
                DropDownList1.Items.Add("");
                DataTable dt1 = mysql.GetDataTable(selectcmd, "UserLoginInfo");
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(dt1.Rows[i]["UserName"].ToString());
                }
            }
        }
    }
    public void databind()
    {
        string selectcmd = "";
        string selectcmd_First = "select UserLoginInfo.[ID], UserLoginInfo.[UserName],UserLoginInfo.[LogInTime],UserLoginInfo.[LogInTime],UserLoginInfo.[LoginInfo] "; //150529 Add UserLoginInfo.[ID],
        string selectcmd_Second = "FROM UserLoginInfo  WHERE ";
        
        if (DropDownList1.Text != "")
        {
            if (TextBox5.Text != "")
            {
                if (TextBox6.Text != "")
                {
                    selectcmd = selectcmd_First + selectcmd_Second +
                       "UserLoginInfo.[UserName]='" + DropDownList1.SelectedItem + "' AND UserLoginInfo.[LogInTime]>'" + TextBox5.Text + "' AND UserLoginInfo.[LogInTime] <'" + TextBox6.Text + "'";

                }
                else
                {
                    selectcmd = selectcmd_First + selectcmd_Second+
                                      "UserLoginInfo.[UserName]='" + DropDownList1.SelectedItem + "' AND UserLoginInfo.[LogInTime]>'" + TextBox5.Text + "' ";


                }


            }
            else
            {
                if (TextBox6.Text != "")
                {
                    selectcmd = selectcmd_First + selectcmd_Second +
                       "UserLoginInfo.[UserName]='" + DropDownList1.SelectedItem + "' AND UserLoginInfo.[LogInTime] <'" + TextBox6.Text + "'";

                }
                else
                {
                    selectcmd = selectcmd_First + selectcmd_Second +
                       "UserLoginInfo.[UserName]='" + DropDownList1.SelectedItem + "'";
                }
            }
        }
        else
        {
            if (TextBox5.Text != "")
            {
                if (TextBox6.Text != "")
                {
                    selectcmd = selectcmd_First + selectcmd_Second +
                       "UserLoginInfo.[LogInTime]>'" + TextBox5.Text + "' AND UserLoginInfo.[LogInTime] <'" + TextBox6.Text + "'";

                }
                else
                {
                    selectcmd = selectcmd_First + selectcmd_Second + 
                                      " UserLoginInfo.[LogInTime]>'" + TextBox5.Text + "' ";


                }


            }
            else
            {
                if (TextBox6.Text != "")
                {
                    selectcmd = selectcmd_First + selectcmd_Second +
                       "UserLoginInfo.[LogInTime] <'" + TextBox6.Text + "'";

                }
                else
                {
                    selectcmd = selectcmd_First + "FROM UserLoginInfo";
                }

            }


        }


        selectcmd += " order by UserLoginInfo.[LogInTime] DESC";
        mysql.OpenDatabase(true);
        dt = mysql.GetDataTable(selectcmd, "UserLoginInfo");

        if (dt.Rows.Count != 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
           // GridView1.Columns[5].Visible = false;
            GridView1.HeaderRow.Style.Add("word-break", "keep-all");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridView1.HeaderRow.Cells[0].Visible = false;
                GridView1.Rows[i].Cells[0].Visible = false;
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
           // Response.Write("<script>alert(' No Data！');</script>");
            dt.Rows.Add(dt.NewRow());
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Rows[0].Cells[4].Controls.Clear();
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //username = GridView1.Rows[e.NewSelectedIndex].Cells[0].Text;
        //LogInTime = GridView1.Rows[e.NewSelectedIndex].Cells[1].Text;
        //LogOffTime = GridView1.Rows[e.NewSelectedIndex].Cells[2].Text;
        //logininfo = GridView1.Rows[e.NewSelectedIndex].Cells[3].Text;
       
       // id = GridView1.Rows[e.NewSelectedIndex].Cells[5].Text;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton TestDataLinkButton = (LinkButton)e.Row.FindControl("OpLogsLinkButton");
            TestDataLinkButton.CommandArgument = e.Row.RowIndex.ToString();

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index;
        string oplogsid="";
        string logininfo = "";
        string username = "";
        string LogInTime = "";
        //string LogOffTime = "";
        if (e.CommandName == "OpLogs")
        {
            index = Convert.ToInt32(e.CommandArgument);
            oplogsid = GridView1.Rows[index].Cells[0].Text;
            
            username = GridView1.Rows[index].Cells[1].Text;
            LogInTime = GridView1.Rows[index].Cells[2].Text;
            //LogOffTime = GridView1.Rows[index].Cells[3].Text;
            logininfo = GridView1.Rows[index].Cells[3].Text;
            if (username == "&nbsp;")
            {
                username = "";
            }
            if (LogInTime == "&nbsp;")
            {
                LogInTime = "";
            }
            //if (LogOffTime == "&nbsp;")
            //{
            //    LogOffTime = "";
            //}
            if (logininfo == "&nbsp;")
            {
                logininfo = "";
            }
            Session["CurrUsername"] = username;
            Session["logininfo"] = logininfo;
            Session["LogInTime"] = LogInTime;

            Response.Redirect("~/WebFiles/Management/OPLogs.aspx?uId=" + oplogsid.Trim());
            #region 未使用
            //string selectcmd = "";
            //string selectcmd_First = "select OperationLogs.[PID]";
            //string selectcmd_Second = " FROM OperationLogs,UserLoginInfo ";
            //string selectcmd_Third = " WHERE OperationLogs.[PID]=UserLoginInfo.[ID] ";
            //selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
            //                    "AND UserLoginInfo.[UserName]='" + username + "' AND UserLoginInfo.[LogInTime]='" + LogInTime + "' AND UserLoginInfo.[LogOffTime] ='" + LogOffTime + "' AND cast (UserLoginInfo.[LoginInfo] as nvarchar(500)) ='" + logininfo + "'";
            //mysql.OpenDatabase(true);
            //dt = mysql.GetDataTable(selectcmd, "OperationLogs");
            //if (dt.Rows.Count != 0)
            //{
            //    //oplogsid = id;
            //    oplogsid = dt.Rows[0]["PID"].ToString();
            //    Session["CurrUsername"] = username;
            //    Session["logininfo"] = logininfo;
            //    Session["LogInTime"] = LogInTime;
            //    Response.Redirect("~/WebFiles/OperateLog/OPLogs.aspx?uId=" + oplogsid.Trim());
            //}
            //else
            //{
            //    oplogsid = " ";
            //    Session["CurrUsername"] = username;
            //    Session["logininfo"] = logininfo;
            //    Session["LogInTime"] = LogInTime;
            //    Response.Redirect("~/WebFiles/OperateLog/OPLogs.aspx?uId=" + oplogsid.Trim());

            //}
            #endregion
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        databind();

        Session["LogPage"] = GridView1.PageIndex;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["LogUsername"] = DropDownList1.Text;
        Session["LogStartTime"] = TextBox5.Text;
        Session["LogStopTime"] = TextBox6.Text;
        Session["LogPage"] = 0;

        databind();
       
    }
    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();

            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "管理", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }

    protected void plhNavi_PreRender(object sender, EventArgs e)
    {
        if (Session["LogUsername"] != null)
        {
            DropDownList1.Text = Session["LogUsername"].ToString();
            TextBox5.Text = Session["LogStartTime"].ToString();
            TextBox6.Text = Session["LogStopTime"].ToString();
            GridView1.PageIndex = Convert.ToInt32(Session["LogPage"]);

            databind();        
        }
    }
}