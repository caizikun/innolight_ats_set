using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_TestPlan_ModelInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    public bool ItemDescVisable
    {
        get { return this.lblItemDescription.Visible; }
        set { this.lblItemDescription.Visible = value; }
    }

    public bool SetModelInfoEnabled(bool state)
    {
        try
        {
            //this.lblItemName.Enabled = state;
            this.ddlFailBreak.Enabled = state;
            this.ddlIgnoreFlag.Enabled = state;
            //this.txtAppName.Enabled = false;
            //this.lblItemDescription.Enabled =false;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool TxtItemDescriptionEnabled
    {
        get { return this.lblItemDescription.Enabled; }
        set { this.lblItemDescription.Enabled = value; }
    }

    public string TxtItemDesc
    {
        get { return this.lblItemDescription.Text; }
        set { this.lblItemDescription.Text = value; }
    }

    public string LblItemName
    {
        get { return this.lblItemName.Text; }
        set { this.lblItemName.Text = value; }
    }

    public string DdlIgnoreFlag
    {
        get { return this.ddlIgnoreFlag.Text.ToLower(); }
        set { this.ddlIgnoreFlag.Text = value.ToLower(); }
    }

    public string DdlFailBreak
    {
        get { return (this.ddlFailBreak.Text.ToLower()); }
        set
        {
            this.ddlFailBreak.Text = value.ToLower();;
        }
    }
}