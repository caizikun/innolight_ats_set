<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncLog.aspx.cs" Inherits="SyncLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<script runat="server">
    private const string SessionKey = "Default";
    ATSDataBase.DataIO db2;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string startHTML = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" + Environment.NewLine
            + "<html xmlns=\"http://www.w3.org/1999/xhtml\" >" + Environment.NewLine
            + "<head>" + Environment.NewLine
            + "</head>" + Environment.NewLine
            + "<body>" + Environment.NewLine;

        startHTML += new String(' ', 1024) + Environment.NewLine;

        Response.Write(startHTML);
        Response.Flush();

        if (Session[SessionKey] != null)
        {
            Hashtable ht = (Hashtable)Session[SessionKey];
            Session[SessionKey] = null;

            Output(ht);
        }

        string endhTML = Environment.NewLine + "</body>" + Environment.NewLine
            + "</html>";

        Response.Write(endhTML);
        Response.Flush();
    }

    private void Output(Hashtable ht)
    {
        try
        {
            if (ht.ContainsKey("post") && ht["post"].ToString() == "true")
            {
                string js = "<script type=\"text/javascript\">window.scrollTo(0, document.body.scrollHeight);<" + "/script>";

                Response.Write("DB data is syncing...<br/>");
                Response.Flush();

                string server = ConfigurationManager.AppSettings["ServerName"].ToString();
                string dbname = ConfigurationManager.AppSettings["DbName"].ToString();
                string uid = ConfigurationManager.AppSettings["UserId"].ToString();
                string password = ConfigurationManager.AppSettings["Pwd"].ToString();

                string server2 = ConfigurationManager.AppSettings["ServerName"].ToString();
                string dbname2 = ConfigurationManager.AppSettings["DbName2"].ToString();
                string uid2 = ConfigurationManager.AppSettings["UserId"].ToString();
                string password2 = ConfigurationManager.AppSettings["Pwd"].ToString();

                ArrayList TestDataTableList = new ArrayList();   //存放与测试数据及Log信息相关的表，同步时只清空
                TestDataTableList.Add("OperationLogs");
                TestDataTableList.Add("TopoLogRecord");
                TestDataTableList.Add("TopoProcData");
                TestDataTableList.Add("TopoRunRecordTable");
                TestDataTableList.Add("TopoTestCoefBackup");
                TestDataTableList.Add("TopoTestData");
                TestDataTableList.Add("UserLoginInfo");

                ATSDataBase.DataIO db = new ATSDataBase.SqlManager(server, dbname, uid, password);
                db2 = new ATSDataBase.SqlManager(server2, dbname2, uid2, password2);

                System.Data.DataSet ds = db.ExecuteDS("SELECT sobjects.name FROM sysobjects sobjects WHERE sobjects.xtype = 'U' order by name");
                System.Data.DataRowCollection drc = ds.Tables[0].Rows;

                foreach (System.Data.DataRow dr in drc)
                {
                    string tableName = dr[0].ToString();

                    System.Data.DataTable currentDt = db2.GetDataTable("SELECT * FROM " + tableName, "");

                    if (currentDt.Rows.Count != 0)
                    {
                        db2.getDbCmdExecuteNonQuery("delete from " + tableName); 
                    }                 
                }
                
                foreach (System.Data.DataRow dr in drc)
                {
                    bool TestDataFlag = false;
                    string tableName = dr[0].ToString();

                    for (int i = 0; i < TestDataTableList.Count; i++)
                    {
                        if (tableName == TestDataTableList[i].ToString())
                        {
                            TestDataFlag = true;    //只删除不同步
                            break;
                        }
                    }
                    
                    string currentTime = System.DateTime.Now.ToString();
                    Response.Write(currentTime + " Current Syncing table is [" + tableName + "]<br/>");
                    Response.Write(js);
                    Response.Flush();
                    db2.WriteLogs(currentTime + " Current Syncing table is [" + tableName + "]", "SyncLog.txt");

                    //db2.getDbCmdExecuteNonQuery("delete from " + tableName);         

                    if (TestDataFlag == false)
                    {
                        db.BulkCopyTo(server2, dbname2, uid2, password2, tableName);
                    }                  
                }
                
                Response.Write("Sync all db data successfully!");
                Response.Write(js);     
                Response.Flush();
                string currentEndTime = System.DateTime.Now.ToString();
                db2.WriteLogs("[" + currentEndTime + "]" + "Sync all db data successfully!", "SyncLog.txt");
            }
        }
        catch (Exception ex)
        {
            string js = "<script type=\"text/javascript\">window.scrollTo(0, document.body.scrollHeight);<" + "/script>";
            Response.Write("Sync all db data fail!");
            Response.Write(js); 
            Response.Flush();
            db2.WriteLogs("Sync all db data fail!", "SyncLog.txt");
            db2.WriteLogs(ex.ToString(), "SyncLog.txt");
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
