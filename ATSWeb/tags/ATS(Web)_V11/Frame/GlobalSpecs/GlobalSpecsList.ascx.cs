using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_GlobalSpecs_GlobalSpecsList : System.Web.UI.UserControl
{
#region ConfigTHVisible
    public bool EnableTH1Visible
    {
        set
        {
           
            TH1.Visible = value;
        }
        get
        {
            return TH1.Visible;
        }
    }
    public bool EnableTH2Visible
    {
        set
        {

            TH2.Visible = value;
        }
        get
        {
            return TH2.Visible;
        }
    }
    public bool EnableTH3Visible
    {
        set
        {

            TH3.Visible = value;
        }
        get
        {
            return TH3.Visible;
        }
    }
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
       
    }
    public bool ContentTRVisible
    {
        get
        {
            return this.ContentTR.Visible;
        }
        set
        {
            this.ContentTR.Visible = value;
        }
    }
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
 
    public bool SetItemDescriptionState(bool state)
    {
        try
        {
            this.lblItemDescription.Visible = state;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string MyClientID
    {
        get{return this.ClientID;}
    }

    public string ChkID
    {
        get { return chkID.ID.ToString(); }
        set { chkID.ID = value; }
    }

    public bool chkIDVisable
    {
        get { return this.chkID.Visible; }
        set { this.chkID.Visible = value; }
    }

    public bool chkIDChecked
    {
        get { return this.chkID.Checked; }
        set { this.chkID.Checked = value; }
    }

    public string ChkEquipTxt
    {
        get { return chkID.Text.ToString(); }
        set { chkID.Text = value; }
    }
    public string LnkItemNamePostBackUrl
    {
        get { return lnkItemName.PostBackUrl; }
        set { lnkItemName.PostBackUrl = value; }
    }

    public string ToolTipItemName
    {
        get { return this.lnkItemName.ToolTip; }
        set { lnkItemName.ToolTip = value; }
    }

    public string ToolTipItemDescription
    {
        get { return lblItemDescription.ToolTip; }
        set
        {
            lblItemDescription.ToolTip = value;
        }
    }

    public string TxtItemDescription
    {
        get { return lblItemDescription.Text; }
        set
        {
            lblItemDescription.Text = value;
        }
    }

    public string LnkItemName
    {
        get { return lnkItemName.Text; }
        set
        {
            this.lnkItemName.Text = value;
        }
    }

    public string TxtUnit
    {
        get { return lblUnit.Text; }
        set
        {
            this.lblUnit.Text = value;
        }
    }
   
}