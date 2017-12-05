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
using System.Threading;

public partial class SyncPage : BasePage
{
    DataIO pDataIO2;   //DebugDB
    long myAccessCode =0;
    CommCtrl pCommCtrl = new CommCtrl();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName2"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO2 = null;
        pDataIO2 = new SqlManager(serverName, dbName, userId, pwd);
        initPage();       
    }

    bool initPage()
    {
        try
        {
            UserAccountInfo.btnChangePwdVisalbe = false;

            if (Session["UserName"] != null && Session["UserID"] != null)
            {
                UserAccountInfo.hlkUserTxt = Session["UserName"].ToString();
                UserAccountInfo.confighlkUserIDDIV = Session["UserName"].ToString();
            }
            else
            {
                //Response.Write("<script>alert('未找到当前用户!系统自动关闭!')</script>");
                //Response.Write("<script>window.opener = null;window.open('','_self');window.close();</script>");
                //return false;
            }

            if (!IsPostBack)
            {
                LabelRoot.Text = "Copyright &copy; ATS信息管理系统 V2.0 &nbsp;&lt;UpdateTime" + ConfigurationManager.AppSettings["UpdataTime"].ToString() + "&gt;";
            }
         
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btDebugDB_Click(object sender, EventArgs e)
    {
        string pUserID = "";
        DataTable myInfo = pDataIO2.GetDataTable("select * from UserInfo where LoginName='" + UserAccountInfo.hlkUserTxt.Trim() + "'", "Username");
        DataRow[] myInfoRows = myInfo.Select("LoginName='" + UserAccountInfo.hlkUserTxt.Trim() + "'");
        if (myInfoRows.Length == 1)
        {
            pUserID = myInfoRows[0]["ID"].ToString();
            Session["UserID"] = pUserID;
            Session["UserName"] = UserAccountInfo.hlkUserTxt.Trim();
            Session["UserLoginID"] = updateUserLoginInfo(); //150415_0 保存UserLoginInfo生成的ID资料
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('用户不存在!')</Script>", false);
        }

        //150325 待切换至新表FunctionTable 获取权限--> 正常来讲一个账号角色分配为1个...
        string queryCMD = "select sum(FunctionCode) from FunctionTable where id in "
            + "(select RoleFunctionTable.FunctionID from RoleFunctionTable where RoleFunctionTable.RoleID  in "
            + "(select roleID from UserRoleTable where UserRoleTable.UserID = " + pUserID + "))";
        //+ pUserID + "))";--> ToString+ "(select id from UserInfo	where UserInfo.LoginName='" + this.txtUserID.Text.ToString() + "')))";
        DataTable myFunctions = pDataIO2.GetDataTable(queryCMD, "UserRoleFuncTable");

        if (myFunctions.Rows.Count > 0 && myFunctions.Rows[0][0].ToString().Trim().Length > 0)
        {
            myAccessCode = Convert.ToInt64(myFunctions.Rows[0][0].ToString());
        }
        else
        {
            myAccessCode = 0;
        }

        Session["AccCode"] = myAccessCode;
        pDataIO2.OpenDatabase(false);

        Session["DB"] = "ATSDEBUGdb";
        Response.Redirect("~/Home.aspx");
    }

    protected void btDB_Click(object sender, EventArgs e)
    {
        Session["DB"] = "ATSdb";
        Response.Redirect("~/Home.aspx");
    }

    protected void btSyncDB_Click(object sender, EventArgs e)
    {
        Hashtable ht = new Hashtable();
        ht.Add("post", "true");
        Session["Default"] = ht;

        string currentTime = System.DateTime.Now.ToString();
        string IP = GetLoginIp();
        pDataIO2.WriteLogs("[" + currentTime + "]User IP:" + IP + "----- Start sync All db data -----", "SyncLog.txt");
    }

    string updateUserLoginInfo()    //150415_0
    {
        string myID = "";
        try
        {
            DataTable userLoginInfoDt = pDataIO2.GetDataTable("select * from UserLoginInfo where ID=-1", "UserLoginInfo");
            DataRow dr = userLoginInfoDt.NewRow();

            string IP4 = GetLoginIp();

            string currTime = pDataIO2.GetCurrTime().ToString();
            string userName = UserAccountInfo.hlkUserTxt.ToLower().Trim();
            dr["UserName"] = userName;
            dr["LogInTime"] = currTime;
            dr["LogOffTime"] = "2000-1-1 12:00:00";
            dr["Apptype"] = "ATSWeb";
            dr["LoginInfo"] = IP4;
            dr["Remark"] = "";
            userLoginInfoDt.Rows.Add(dr);
            long lstID = -1;
            pDataIO2.UpdateDataTable("select * from UserLoginInfo where ID=-1", userLoginInfoDt, out lstID);
            myID = lstID.ToString();

            return myID;
        }
        catch (Exception ex)
        {
            return pDataIO2.GetLastInsertData("UserLoginInfo").ToString();
        }
    }

    /// <summary>
    /// 获取远程访问用户的Ip地址
    /// </summary>
    /// <returns>返回Ip地址</returns>
    protected string GetLoginIp()
    {
        string loginip = "";
        //Request.ServerVariables[""]--获取服务变量集合 
        if (Request.ServerVariables["REMOTE_ADDR"] != null) //判断发出请求的远程主机的ip地址是否为空
        {
            //获取发出请求的远程主机的Ip地址
            loginip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        //判断登记用户是否使用设置代理
        else if (Request.ServerVariables["HTTP_VIA"] != null)
        {
            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                //获取代理的服务器Ip地址
                loginip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                //获取客户端IP
                loginip = Request.UserHostAddress;
            }
        }
        else
        {
            //获取客户端IP
            loginip = Request.UserHostAddress;
        }
        if (loginip == "127.0.0.1")
        {
            System.Net.IPAddress[] ips = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());//网卡IP地址集合

            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    loginip = ips[i].ToString();
                    break;
                }
            }
        }
        return loginip;
    }
}