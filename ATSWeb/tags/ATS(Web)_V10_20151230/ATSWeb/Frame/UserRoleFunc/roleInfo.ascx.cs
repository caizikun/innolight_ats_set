using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_UserRoleFunc_roleInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool SetEnableState(bool state = false)
    {
        try
        {
            this.txtRoleName.Enabled = state;
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

    public string ToolTipRoleName
    {
        get { return txtRoleName.ToolTip; }
        set { txtRoleName.ToolTip = value; }
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

    public string TxtRoleName
    {
        get { return txtRoleName.Text; }
        set
        {
            this.txtRoleName.Text = value;
        }
    }
}