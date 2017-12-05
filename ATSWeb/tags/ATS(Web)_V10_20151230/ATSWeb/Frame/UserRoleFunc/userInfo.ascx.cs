using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_UserRoleFunc_userInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool SetEnableState(bool state = false)
    {
        try
        {
            this.txtLoginName.Enabled = state;
            this.txtPwd.Enabled = state;
            this.txtTrueName.Enabled = state;
            this.txtRemarks.Enabled = state;
            return true;
        }
        catch
        { return false; }
    }

    public string MyClientID
    {
        get { return this.ClientID; }
    }

    public string ToolTipLoginName
    {
        get { return txtLoginName.ToolTip; }
        set { txtLoginName.ToolTip = value; }
    }

    public string ToolTipRemarks
    {
        get { return txtRemarks.ToolTip; }
        set
        {
            txtRemarks.ToolTip = value;
        }
    }

    public string TxtRemarks
    {
        get { return txtRemarks.Text; }
        set
        {
            txtRemarks.Text = value;
        }
    }

    public string TxtLoginName
    {
        get { return txtLoginName.Text; }
        set
        {
            this.txtLoginName.Text = value;
        }
    }

    public string TxtPwd
    {
        get { return txtPwd.Text; }
        set
        {
            this.txtPwd.Text = value;
        }
    }

    public string TxtTrueName
    {
        get { return txtTrueName.Text; }
        set
        {
            this.txtTrueName.Text = value;
        }
    }
}