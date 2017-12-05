using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Session["UserID"] = null;
        Session["UserName"] = null;
        Session["AccCode"] = -1;
        Response.Redirect("~/Default.aspx");             
    }
    //protected void btnChangePWD_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Account/ChangePwd.aspx");
        
    //}

    public string hlkUserTxt
    {
        get { return hlkUserID.Text; }
        set
        {
            hlkUserID.Text = value;
        }
    }
    #region ConfighlkUserIDDIV
    public string confighlkUserIDDIV
    {
        set
        {
            hlkUserIDDIV.Attributes["title"] = value;
        }
        get
        {
            return hlkUserIDDIV.Attributes["title"];
        }
    }
    #endregion
    
    public bool btnChangePwdVisalbe
    {
        get { return btnChangePWD.Visible; }
        set
        {
            btnChangePWD.Visible = value;
        }
    }

    public string btnChangePwdPostBackUrl
    {
        get { return btnChangePWD.PostBackUrl; }
        set
        {
            btnChangePWD.PostBackUrl = value;
        }
    }

    public string UserImgPath
    {
        get { return userImg.Src; }
        set
        {
            userImg.Src = value;
        }
    }

    public string UserImgAlt
    {
        get { return userImg.Alt; }
        set
        {
            userImg.Alt = value;
        }
    }
  
}