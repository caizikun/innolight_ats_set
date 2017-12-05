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

public partial class Account_ChangePwd : BasePage
{
    DataIO pDataIO;
    string currID = "";
    CommCtrl pCommCtrl = new CommCtrl();

    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();

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

        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);

        if (Session["UserID"] != null)
        {
            currID = Session["UserID"].ToString();
            lblInfo.Text = "";
        }
        else
        {
            this.ChangeFunc.Visible = false;
            ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>", false);
        }

    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        DataTable pReaderTable = pDataIO.GetDataTable("select * from UserInfo where ID=" + currID, "UserInfo");

        if (pReaderTable.Rows.Count == 1)
        {
            if (txtNewPwd.Text.Length == 0 || txtConfirmPwd.Text.Length == 0)
            {
                lblInfo.Text = "新密码或确认密码为空!";
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!新密码或确认密码为空!')</Script>");
            }
            else if (txtNewPwd.Text != txtConfirmPwd.Text)
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!新密码与确认密码两次输入不符!')</Script>");
            }
            else
            {
                //if (pReaderTable.Rows[0]["loginpassword"].ToString() != txtPwd.Text)
                if (pReaderTable.Rows[0]["loginpassword"].ToString() != pCommCtrl.Encrypt(txtPwd.Text))   
                {
                    lblInfo.Text = "原密码验证错误!";
                    ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!原密码验证错误!')</Script>");
                }
                else
                {                    
                    //pReaderTable.Rows[0]["loginpassword"] = txtNewPwd.Text;
                    pReaderTable.Rows[0]["loginpassword"] = pCommCtrl.Encrypt(txtNewPwd.Text);
                    pDataIO.UpdateDataTable("select * from UserInfo where ID=" + currID, pReaderTable);
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<Script>alert('密码已经修改成功');window.top.location.href='" + "../Home.aspx" + "'</Script>", false);
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>", false);
        }
    }
}