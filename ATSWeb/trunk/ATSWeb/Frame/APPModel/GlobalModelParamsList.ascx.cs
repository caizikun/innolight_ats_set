using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_APPModel_GlobalModelParamsList : System.Web.UI.UserControl
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
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        TH5Title.Visible = status;
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
    //public bool EnableTH5Visible
    //{
    //    set
    //    {

    //        TH5.Visible = value;
    //    }
    //    get
    //    {
    //        return TH5.Visible;
    //    }
    //}
    //public bool EnableTH6Visible
    //{
    //    set
    //    {

    //        TH6.Visible = value;
    //    }
    //    get
    //    {
    //        return TH6.Visible;
    //    }
    //}
    //public bool EnableTH7Visible
    //{
    //    set
    //    {

    //        TH7.Visible = value;
    //    }
    //    get
    //    {
    //        return TH7.Visible;
    //    }
    //}
    //public bool EnableTH8Visible
    //{
    //    set
    //    {

    //        TH8.Visible = value;
    //    }
    //    get
    //    {
    //        return TH8.Visible;
    //    }
    //}
    //public bool EnableTH9Visible
    //{
    //    set
    //    {

    //        TH9.Visible = value;
    //    }
    //    get
    //    {
    //        return TH9.Visible;
    //    }
    //}
    //public bool EnableTH10Visible
    //{
    //    set
    //    {

    //        TH10.Visible = value;
    //    }
    //    get
    //    {
    //        return TH10.Visible;
    //    }
    //}
   
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public string ToolTipItemDescription
    {
        get { return txtItemDescription.ToolTip; }
        set { txtItemDescription.ToolTip = value; }
    }

    public string ToolTipShowName
    {
        get { return txtShowName.ToolTip; }
        set { txtShowName.ToolTip = value; }
    }

    public string ToolTipItemName
    {
        get { return lnkItemName.ToolTip; }
        set { lnkItemName.ToolTip = value; }
    }
    
    public string ToolTipItemValue
    {
        get { return txtItemValue.ToolTip; }
        set { txtItemValue.ToolTip = value; }
    }

    public bool SetItemDescriptionState(bool state)
    {
        try
        {
            this.txtItemDescription.Visible = state;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SetModelParamsEnableState(bool state,bool isViewInfo=true,bool isGlobalMode=false)
    {
        try
        {
            this.lnkItemName.Enabled = isViewInfo;
            this.txtShowName.Enabled = isGlobalMode;
            this.txtItemType.Enabled = isGlobalMode;
            this.txtItemValue.Enabled = isGlobalMode;            
            this.txtItemDescription.Enabled = state;
            return true;
        }
        catch
        { return false; }
    }
    
    public string MyClientID
    {
        get{return this.ClientID;}
    }

    public bool ChkIDModelParamChecked
    {
        get { return chkIDModelParam.Checked; }
        set { chkIDModelParam.Checked = value; }
    }

    public string ChkIDModelParam
    {
        get { return chkIDModelParam.ID.ToString(); }
        set { chkIDModelParam.ID = value; }
    }

    public bool chkIDModelParamVisable
    {
        get { return this.chkIDModelParam.Visible; }
        set { this.chkIDModelParam.Visible = value; }
    }

    public string ChkModelParamTxt
    {
        get { return chkIDModelParam.Text.ToString(); }
        set { chkIDModelParam.Text = value; }
    }
    public string TxtItemNamePostBackUrl
    {
        get { return lnkItemName.PostBackUrl; }
        set { lnkItemName.PostBackUrl = value; }
    }

    public string TxtItemDescription
    {
        get { return txtItemDescription.Text; }
        set { txtItemDescription.Text = value; }
    }

    public string TxtItemName
    {
        get {return lnkItemName.Text;}
        set { lnkItemName.Text = value; }
    }

    public string TxtShowName
    {
        get { return txtShowName.Text; }
        set { txtShowName.Text = value; }
    }

    public string TxtItemType
    {
        get { return txtItemType.Text; }
        set { txtItemType.Text = value; }
    }
    
    public string TxtItemValue
    {
        get { return this.txtItemValue.Text; }
        set { txtItemValue.Text = value; }
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