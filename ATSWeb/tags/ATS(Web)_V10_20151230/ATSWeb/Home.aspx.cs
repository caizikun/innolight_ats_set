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

public partial class _Home : BasePage
{
    DataIO pDataIO;
    long myAccessCode =0;
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        try
        {
            string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
            string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
            string userId = ConfigurationManager.AppSettings["UserId"].ToString();
            string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();

            pDataIO = null;
            pDataIO = new SqlManager(serverName, dbName, userId, pwd);
            Session["LevelID_0_Page"] = ""; // Request.RawUrl;
            Session["txtLevelID_0"] = "Home";

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    

    

}