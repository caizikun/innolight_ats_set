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
public partial class WebFiles_OperateLog_DetailLogs : BasePage
{
    public string id;
    DataIO mysql;
    public DataTable dt;
    string funcItemName = "操作详情";
    private string logTracingString = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(4);
        id = Request.QueryString["uId"];
        Label7.Text = Session["CurrUsername"].ToString();
        Label8.Text = Session["modifytime"].ToString();
        Label9.Text = Session["logininfo"].ToString();
        Label10.Text = Session["optype"].ToString();
        Label11.Text = Session["blocktype1"].ToString();
        //Label12.Text = Session["OperateItem"].ToString();
        //Label14.Text = Session["ChlidItem"].ToString();
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
        if (Label10.Text == "&nbsp;")
        {
            Label10.Text = "";
        }
        if (Label11.Text == "&nbsp;")
        {
            Label11.Text = "";
        }
        //if (Label12.Text == "&nbsp;")
        //{
        //    Label12.Text = "";
        //}
        //if (Label14.Text == "&nbsp;")
        //{
        //    Label14.Text = "";
        //}
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

        string selectcmd = "select OperationLogs.[DetailLogs],OperationLogs.TracingInfo from OperationLogs where OperationLogs.[ID]= '" + id + "' ";

        mysql.OpenDatabase(true);
        
        dt = mysql.GetDataTable(selectcmd, "OperationLogs");

        if (dt.Rows.Count != 0)
        {
            TextBox2.Text = " " + dt.Rows[0]["DetailLogs"].ToString();
            Label12.Text = dt.Rows[0]["TracingInfo"].ToString();

        }
        creattNavi();
    }
    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();
            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "操作信息列表", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
   
}