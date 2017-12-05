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

public partial class _Default : System.Web.UI.Page
{
    DataIO pDataIO;
    long myAccessCode =0;
    public _Default()
    {
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (pDataIO.OpenDatabase(true)) //先尝试连接一次!
            {
                pDataIO.OpenDatabase(false);
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] != null || Session["UserName"] != null)
        {
            string message = "a user had log in,please exit first!";
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('" + message + "');", true);  
           
        }
        else if (showAccess())
        {
            Response.Redirect("~/Home.aspx");
        }
    }

    string updateUserLoginInfo()    //150415_0
    {
        string myID = "";
        try
        {
            DataTable userLoginInfoDt = pDataIO.GetDataTable("select * from UserLoginInfo where ID=-1", "UserLoginInfo");
            DataRow dr = userLoginInfoDt.NewRow();
            
            //string hostname = System.Net.Dns.GetHostName(); //主机
            //System.Net.IPAddress [] ips = System.Net.Dns.GetHostAddresses(hostname);//网卡IP地址集合
            string IP4 = GetLoginIp();
            //for (int i = 0; i < ips.Length; i++)
            //{
            //    if (ips[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        IP4 = ips[i].ToString();
            //        break;
            //    }
            //}
            //string IP6 = ipEntry.AddressList[0].ToString();//取一个IP
            //string IP4 = ipEntry.AddressList[ipEntry.AddressList.Length - 1].ToString();//取一个IP
            string currTime = pDataIO.GetCurrTime().ToString();
            string userName = this.txtUserName.Text.ToLower().Trim();
            dr["UserName"] = userName;
            dr["LogInTime"] = currTime;
            dr["LogOffTime"] = "2000-1-1 12:00:00";
            dr["Apptype"] = "ATSWeb";
            dr["LoginInfo"] =  IP4;
            dr["Remark"] = "";
            userLoginInfoDt.Rows.Add(dr);
            long lstID = -1;
            pDataIO.UpdateDataTable("select * from UserLoginInfo where ID=-1", userLoginInfoDt, out lstID);
            myID = lstID.ToString();

            return myID;
        }
        catch (Exception ex)
        {
            return pDataIO.GetLastInsertData("UserLoginInfo").ToString();
        }
    }

    bool showAccess()
    {
        try
        {
            {
                bool PWDOKFlag = false;

                string pUserID = "";
                DataTable myInfo = pDataIO.GetDataTable("select * from UserInfo where LoginName='" + this.txtUserName.Text.Trim() + "'", "Username");

                DataRow[] myInfoRows = myInfo.Select("LoginName='" + this.txtUserName.Text.Trim() + "'");
                if (myInfoRows.Length == 1)
                {
                    //140612_2 Password-->UserPassword
                    if (myInfoRows[0]["LoginPassword"].ToString() == txtPwd.Text.ToString())
                    {
                        pUserID = myInfoRows[0]["ID"].ToString();
                        Session["UserID"] = pUserID;
                        Session["UserName"] = this.txtUserName.Text.Trim();
                        Session["UserLoginID"] = updateUserLoginInfo(); //150415_0 保存UserLoginInfo生成的ID资料

                        PWDOKFlag = true;
                        Session["loginTryCount"] = 0;
                    }
                    else
                    {
                        PWDOKFlag = false;
                        Session["loginTryCount"] = Convert.ToInt32(Session["loginTryCount"]) + 1;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Message", "<Script>alert('用户不存在!')</Script>", false);
                    Session["loginTryCount"] = Convert.ToInt32(Session["loginTryCount"]) + 1;
                    PWDOKFlag = false;
                }

                if (!PWDOKFlag)
                {
                    int loginTryCount = Convert.ToInt32(Session["loginTryCount"]);
                    if (loginTryCount < 3)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Message", "<Script>alert('Sorry, either Login Name or Password is not correct. Please try again.')</Script>", false);
                        return false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Message", "<Script>alert('Sorry, either Login Name or Password is not correct more than 3 times!')</Script>", false);
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "", "<script>window.opener = null;window.open('','_self');window.close();</script>", false);
                        Session["loginTryCount"] = 0;
                        return false;
                    }
                }
                else
                {
                    //150325 待切换至新表FunctionTable 获取权限--> 正常来讲一个账号角色分配为1个...
                    Session["loginTryCount"] = 0;
                    string queryCMD = "select sum(FunctionCode) from FunctionTable where id in "
                        + "(select RoleFunctionTable.FunctionID from RoleFunctionTable where RoleFunctionTable.RoleID  in "
                        + "(select roleID from UserRoleTable where UserRoleTable.UserID = " + pUserID + "))";
                    //+ pUserID + "))";--> ToString+ "(select id from UserInfo	where UserInfo.LoginName='" + this.txtUserID.Text.ToString() + "')))";
                    DataTable myFunctions = pDataIO.GetDataTable(queryCMD, "UserRoleFuncTable");


                    if (myFunctions.Rows.Count > 0 && myFunctions.Rows[0][0].ToString().Trim().Length > 0)
                    {
                        myAccessCode = Convert.ToInt64(myFunctions.Rows[0][0].ToString());
                    }
                    else
                    {
                        myAccessCode = 0;
                    }
                }
                Session["AccCode"] = myAccessCode;
                pDataIO.OpenDatabase(false);

                System.Threading.Thread.Sleep(100);
                return true;
            }
           
        }
        catch
        {
            return false;
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