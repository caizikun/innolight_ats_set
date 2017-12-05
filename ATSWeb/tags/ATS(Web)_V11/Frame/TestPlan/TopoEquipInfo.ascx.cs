using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_TestPlan_TopoEquipInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool SetEquipEnableState(bool state = false)
    {
        try
        {
            this.ddlRole.Enabled = state;
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
    public string TxtItemType
    {
        get { return txtItemType.Text; }
        set
        {
            this.txtItemType.Text = value;
        }
    }

    public string TxtLblSeq
    {
        get { return lblSeq.Text; }
        set
        {
            this.lblSeq.Text = value;
        }
    }

    public DropDownList DdlRole
    {
        get { return ddlRole; }
        set { ddlRole = value; }
    }

    public string TxtDdlRole
    {
        get
        {
            if (ddlRole.SelectedValue == "NA")
            {
                return "0";
            }
            else if (ddlRole.SelectedValue == "TX")
            {
                return "1";
            }
            else if (ddlRole.SelectedValue == "RX")
            {
                return "2";
            }
            else
            {
                return "0";
            }
        }
        set
        {
            if (value.ToUpper().Trim() == "0")
            {
                ddlRole.SelectedValue = "NA";
            }
            else if (value.ToUpper().Trim() == "1")
            {
                ddlRole.SelectedValue = "TX";
            }
            else if (value.ToUpper().Trim() == "2")
            {
                ddlRole.SelectedValue = "RX";
            }
        }
    }
}