using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_UserRoleFunc_ascxFileInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool SetEnableState(bool state = false)
    {
        try
        {
            this.txtAscxFileName.Enabled = state;
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

    public string ToolTipAscxFileName
    {
        get { return txtAscxFileName.ToolTip; }
        set { txtAscxFileName.ToolTip = value; }
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

    public string TxtAscxFileName
    {
        get { return txtAscxFileName.Text; }
        set
        {
            this.txtAscxFileName.Text = value;
        }
    }
}