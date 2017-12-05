using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_Equipment_GlobalEquipList : System.Web.UI.UserControl
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
    public bool EnableTH4Visible
    {
        set
        {

            TH4.Visible = value;
        }
        get
        {
            return TH4.Visible;
        }
    }
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
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

    public string ChkIDEquip
    {
        get { return chkIDEquip.ID.ToString(); }
        set { chkIDEquip.ID = value; }
    }

    public bool chkIDEquipVisable
    {
        get { return this.chkIDEquip.Visible; }
        set { this.chkIDEquip.Visible = value; }
    }

    public bool chkIDEquipChecked
    {
        get { return this.chkIDEquip.Checked; }
        set { this.chkIDEquip.Checked = value; }
    }

    public string ChkEquipTxt
    {
        get { return chkIDEquip.Text.ToString(); }
        set { chkIDEquip.Text = value; }
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

    
    public string TxtItemType
    {
        get { return lblItemType.Text; }
        set
        {
            this.lblItemType.Text = value;
        }
    }

    public string LnkViewParamsPostBackUrl
    {
        get { return this.lnkViewParams.PostBackUrl; }
        set { lnkViewParams.PostBackUrl = value; }
    }
    #region ConfigTrColor
    public string TrBackgroundColor
    {
        set
        {
            ContentTR.Attributes.Add("style", "background-color:" + value);
        }
    }
    #endregion
}