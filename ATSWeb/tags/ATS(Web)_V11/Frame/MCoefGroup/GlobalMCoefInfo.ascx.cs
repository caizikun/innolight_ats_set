using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_MCoefGroup_GlobalMCoefInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool SetMCoefEnableState(bool state = false)
    {
        try
        {
            this.txtItemName.Enabled = state;
            this.ddlTypeName.Enabled = state;
            this.ddlIgnoreFlag.Enabled = state;
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

    public string TxtItemName
    {
        get { return txtItemName.Text; }
        set { this.txtItemName.Text = value; }
    }

    public string TxtItemDescription
    {
        get { return txtItemDescription.Text; }
        set { this.txtItemDescription.Text = value; }
    }

    public void AddDdlTypeName(ListItem li)
    {
        if (!this.ddlTypeName.Items.Contains(li))
        {
            ddlTypeName.Items.Add(li);
        }
    }

    public DropDownList DdlTypeName
    {
        get
        {
            return ddlTypeName;
        }
        set
        {
            ddlTypeName = value;
        }
    }

    public ListItem CurrDdlTypeName
    {
        get
        {
            return ddlTypeName.SelectedItem;
        }
        set
        {
            if (ddlTypeName.Items.Contains((ListItem)value))
            {
                ddlTypeName.SelectedValue = value.ToString();
            }
            else
            {
                ddlTypeName.Items.Add(value.ToString());
                ddlTypeName.SelectedValue = value.ToString();
            }
        }
    }

    public string txtDdlTypeName
    {
        get { return this.ddlTypeName.Text; }
        set { ddlTypeName.Text = value; }
    }

    public string DdlIgnoreFlag
    {
        get { return this.ddlIgnoreFlag.Text.ToLower(); }
        set { this.ddlIgnoreFlag.Text = value.ToLower(); }
    }
}