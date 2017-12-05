using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_MCoefGroup_GlobalMCoefParamsList : System.Web.UI.UserControl
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
    public bool EnableTH5Visible
    {
        set
        {

            TH5.Visible = value;
        }
        get
        {
            return TH5.Visible;
        }
    }
    public bool EnableTH6Visible
    {
        set
        {

            TH6.Visible = value;
        }
        get
        {
            return TH6.Visible;
        }
    }
    public bool EnableTH7Visible
    {
        set
        {

            TH7.Visible = value;
        }
        get
        {
            return TH7.Visible;
        }
    }
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        TH5Title.Visible = status;
        TH6Title.Visible = status;
        TH7Title.Visible = status;
        TH8Title.Visible = status;
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

    public string ToolTipItemName
    {
        get { return lnkItemName.ToolTip; }
        set { lnkItemName.ToolTip = value; }
    }

    public string MyClientID
    {
        get { return this.ClientID; }
    }

    public bool ChkIDMCoefParamChecked
    {
        get { return chkIDMCoefParam.Checked; }
        set { chkIDMCoefParam.Checked = value; }
    }

    public string ChkIDMCoefParam
    {
        get { return chkIDMCoefParam.ID.ToString(); }
        set { chkIDMCoefParam.ID = value; }
    }

    public bool chkIDMCoefParamVisable
    {
        get { return this.chkIDMCoefParam.Visible; }
        set { this.chkIDMCoefParam.Visible = value; }
    }

    public string ChkMCoefParamTxt
    {
        get { return chkIDMCoefParam.Text.ToString(); }
        set { chkIDMCoefParam.Text = value; }
    }
    public string TxtItemNamePostBackUrl
    {
        get { return lnkItemName.PostBackUrl; }
        set { lnkItemName.PostBackUrl = value; }
    }

    public string TxtItemName
    {
        get { return lnkItemName.Text; }
        set { lnkItemName.Text = value; }
    }
    public string TxtItemType
    {
        get { return txtItemType.Text; }
        set { txtItemType.Text = value; }
    }
    public string TxtChannel
    {
        get { return this.txtChannel.Text; }
        set { txtChannel.Text = value; }
    }
    public string TxtPage
    {
        get { return this.txtPage.Text; }
        set { txtPage.Text = value; }
    }
    public string TxtStartAddress
    {
        get { return this.txtStartAddress.Text; }
        set { txtStartAddress.Text = value; }
    }
    public string TxtLength
    {
        get { return this.txtLength.Text; }
        set { txtLength.Text = value; }
    }
    public string TxtFormat
    {
        get { return this.txtFormat.Text; }
        set { txtFormat.Text = value; }
    }
    public string TxtAmplify
    {
        get { return this.txtAmplify.Text; }
        set { txtAmplify.Text = value; }
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