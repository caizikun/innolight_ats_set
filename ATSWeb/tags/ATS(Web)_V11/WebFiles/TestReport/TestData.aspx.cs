using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ATSDataBase;
using System.Threading;
public partial class WebFiles_TestReport_TestData : BasePage
{
    string funcItemName = "FunctionList";
    private DataIO pDataIO;
    private string logTracingString = "";
    private string conn;
    public WebFiles_TestReport_TestData()
      {
          conn = "inpcsz0518\\ATS_HOME";
          string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
          string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
          string userId = ConfigurationManager.AppSettings["UserId"].ToString();
          string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
          pDataIO = null;
          pDataIO = new SqlManager(serverName, dbName, userId, pwd);
      }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        IsSessionNull();
        SetSessionBlockType(7);
        creattNavi();
        Session["initsn"] = null;
        Session["QueryPage"] = 0;

        Session["LogUsername"] = null;
        Session["LogPage"] = 0;
    }

    public void creattNavi()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                CommCtrl pCtrl = new CommCtrl();
                if (Request.QueryString["BlockType"] != null)
                {
                    Session["BlockType"] = Request.QueryString["BlockType"];
                }
                else
                {
                    Session["BlockType"] = null;
                }
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "TestData", Session["BlockType"].ToString(), pDataIO, out logTracingString);
                this.plhNavi.Controls.Add(myCtrl);
             }
           
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}