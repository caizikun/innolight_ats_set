using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_Equipment_GlobalEquipInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool SetEquipEnableState(bool state = false)
    {
        try
        {
            this.txtShowName.Enabled = state;
            this.txtItemName.Enabled = state;
            this.txtItemType.Enabled = state;
            this.txtItemDescription.Enabled = state;
            return true;
        }
        catch
        { return false; }
    }

    public string MyClientID
    {
        get { return this.ClientID; }
    }

    public string ToolTipItemName
    {
        get { return txtItemName.ToolTip; }
        set { txtItemName.ToolTip = value; }
    }

    public string ToolTipItemDescription
    {
        get { return txtItemDescription.ToolTip; }
        set
        {
            txtItemDescription.ToolTip = value;
        }
    }

    public string TxtItemDescription
    {
        get { return txtItemDescription.Text; }
        set
        {
            txtItemDescription.Text = value;
        }
    }

    public string TxtShowName
    {
        get { return this.txtShowName.Text; }
        set { txtShowName.Text = value; }
    }

    public string TxtItemName
    {
        get { return txtItemName.Text; }
        set
        {
            this.txtItemName.Text = value;
        }
    }
    public string TxtItemType
    {
        get { return txtItemType.Text; }
        set
        {
            this.txtItemType.Text = value;
        }
    }
}