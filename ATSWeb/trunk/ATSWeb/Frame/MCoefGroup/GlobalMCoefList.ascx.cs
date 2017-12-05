using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_MCoefGroup_GlobalMCoefList : System.Web.UI.UserControl
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
 
    public string MyClientID
    {
        get{return this.ClientID;}
    }

    public string ChkIDMCoef
    {
        get { return chkIDMCoef.ID.ToString(); }
        set { chkIDMCoef.ID = value; }
    }

    public bool chkIDMCoefVisable
    {
        get { return this.chkIDMCoef.Visible; }
        set { this.chkIDMCoef.Visible = value; }
    }

    public bool chkIDMCoefChecked
    {
        get { return this.chkIDMCoef.Checked; }
        set { this.chkIDMCoef.Checked = value; }
    }

    public string ChkMCoefTxt
    {
        get { return chkIDMCoef.Text.ToString(); }
        set { chkIDMCoef.Text = value; }
    }
    public string LnkItemNamePostBackUrl
    {
        get { return lnkItemName.PostBackUrl; }
        set { lnkItemName.PostBackUrl = value; }
    }


    public string LnkItemName
    {
        get { return lnkItemName.Text; }
        set        {            this.lnkItemName.Text = value; 
        }
    }

    public string TxtItemDescription
    {
        get { return this.txtItemDescription.Text; }
        set { txtItemDescription.Text = value; }
    }

    public string txtDdlTypeName
    {
        get { return this.ddlTypeName.Text; }
        set { ddlTypeName.Text = value; }
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

    #region GetSetSelected
    public bool BeSelected
    {
        get
        {
            return chkIDMCoef.Checked;
        }
        set
        {
            this.chkIDMCoef.Checked = value;

        }
    }
    #endregion
}