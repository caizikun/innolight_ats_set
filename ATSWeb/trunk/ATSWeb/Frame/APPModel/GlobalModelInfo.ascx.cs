using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_APPModel_GlobalModelInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool SetModelEnableState(bool state = false)
    {
        try
        {
            this.txtShowName.Enabled = state;
            this.txtItemName.Enabled = state;
            this.ddlAppName.Enabled = state;
            this.txtItemDescription.Enabled = state;
            this.txtModelWeight.Enabled = state;
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

    public string TxtItemName
    {
        get { return txtItemName.Text; }
        set
        {
            this.txtItemName.Text = value;
        }
    }


    public string TxtShowName
    {
        get { return txtShowName.Text; }
        set { txtShowName.Text = value; }
    }

    public void AddDdlAppName(ListItem li)
    {
        if (!this.ddlAppName.Items.Contains(li))
        {
            ddlAppName.Items.Add(li);
        }
    }

    public DropDownList DdlAppName
    {
        get
        {
            return ddlAppName;
        }
        set
        {
            ddlAppName = value;
        }
    }

    public ListItem CurrDdlAppName
    {
        get
        {
            return ddlAppName.SelectedItem;
        }
        set
        {
            if (ddlAppName.Items.Contains((ListItem)value))
            {
                ddlAppName.SelectedValue = value.ToString();
            }
            else
            {
                ddlAppName.Items.Add(value.ToString());
                ddlAppName.SelectedValue = value.ToString();
            }
        }
    }

    public string txtDdlAppName
    {
        get { return this.ddlAppName.Text; }
        set { ddlAppName.Text = value; }
    }

    public string TxtModelWeight
    {
        get { return txtModelWeight.Text; }
        set
        {
            this.txtModelWeight.Text = value;
        }
    }
}