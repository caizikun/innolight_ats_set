using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_Equipment_GlobalEquipParamList : System.Web.UI.UserControl
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
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public bool setItemDescriptionState(bool state)
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
    public bool setEnableState(bool state = false)
    {
        try
        {
            this.lblItemValue.Enabled = state;            
            return true;
        }
        catch
        { return false; }
    }

    //public string myTrID
    //{
    //    get { return this._trEquipParam.ID; }
    //    set { _trEquipParam.ID = value; }
    //}

    public string MyClientID
    {
        get { return this.ClientID; }
    }


    public string ChkIDEquip
    {
        get { return this.chkIDEquipParam.ID.ToString(); }
        set { chkIDEquipParam.ID = value; }
    }

    public bool chkIDEquipVisable
    {
        get { return this.chkIDEquipParam.Visible; }
        set { this.chkIDEquipParam.Visible = value; }
    }

    public bool chkIDEquipChecked
    {
        get { return this.chkIDEquipParam.Checked; }
        set { this.chkIDEquipParam.Checked = value; }
    }

    public string ChkEquipTxt
    {
        get { return chkIDEquipParam.Text.ToString(); }
        set { chkIDEquipParam.Text = value; }
    }

    public string pToolTipItemValue
    {
        get { return lblItemValue.ToolTip; }
        set { lblItemValue.ToolTip = value; }
    }

    public string pToolTipItemDescription
    {
        get { return lblItemDescription.ToolTip; }
        set { lblItemDescription.ToolTip = value; }
    }

    public string pToolTipShowName
    {
        get { return lblShowName.ToolTip; }
        set { lblShowName.ToolTip = value; }
    }

    public string pLblItemDescription
    {
        get { return lblItemDescription.Text; }
        set { lblItemDescription.Text = value; }
    }

    public string pLblShowName
    {
        get { return lblShowName.Text; }
        set { lblShowName.Text = value; }
    }

    public string pTxtItemValue
    {
        get { return lblItemValue.Text; }
        set { lblItemValue.Text = value; }
    }

    public string pLblItemType
    {
        get { return lblItemType.Text; }
        set { lblItemType.Text = value; }
    }

    public string txtLnkItem
    {
        get { return this.lnkItem.Text; }
        set { lnkItem.Text = value; }
    }

    public string LnkItemPostBackUrl
    {
        get { return this.lnkItem.PostBackUrl; }
        set { lnkItem.PostBackUrl = value; }
    }
}