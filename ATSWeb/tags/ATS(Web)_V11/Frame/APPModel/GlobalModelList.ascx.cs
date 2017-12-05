using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_APPModel_GlobalModelList : System.Web.UI.UserControl
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
            this.txtItemDescription.Visible = state;
            this.lblItemDescription.Visible = !state;
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool SetModelEnableState(bool state = false)
    {
        try
        {
            this.lnkItemName.Visible = !state;
            this.txtItemName.Visible = state;
            this.ddlAppName.Enabled = state;            
            this.txtItemDescription.Enabled = state;
            this.txtItemDescription.Visible = state;
            this.lblItemDescription.Visible = !state;
            this.lnkViewParams.Visible = !state;
            return true;
        }
        catch
        { return false; }
    }

    public string MyClientID
    {
        get{return this.ClientID;}
    }

    public string ChkIDModel
    {
        get { return chkIDModel.ID.ToString(); }
        set { chkIDModel.ID = value; }
    }

    public bool chkIDModelVisable
    {
        get { return this.chkIDModel.Visible; }
        set { this.chkIDModel.Visible = value; }
    }

    public bool chkIDModelChecked
    {
        get { return this.chkIDModel.Checked; }
        set { this.chkIDModel.Checked = value; }
    }

    public string ChkModelTxt
    {
        get { return chkIDModel.Text.ToString(); }
        set { chkIDModel.Text = value; }
    }
    public string LnkItemNamePostBackUrl
    {
        get { return lnkItemName.PostBackUrl; }
        set { lnkItemName.PostBackUrl = value; }
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
            lblItemDescription.ToolTip = value;
        }
    }

    public string TxtItemDescription
    {
        get { return txtItemDescription.Text; }
        set
        {
            txtItemDescription.Text = value;
            lblItemDescription.Text = value;
        }
    }

    public string LnkItemName
    {
        get { return lnkItemName.Text; }
        set
        {
            this.lnkItemName.Text = value;
            this.txtItemName.Text = value;
        }
    }


    

    //public void AddDdlAppName(ListItem li)
    //{
    //    if (!this.ddlAppName.Items.Contains(li))
    //    {
    //        ddlAppName.Items.Add(li);
    //    }
    //}

    //public DropDownList DdlAppName
    //{
    //    get
    //    {
    //        return ddlAppName;
    //    }
    //    set
    //    {
    //        ddlAppName = value;
    //    }
    //}

    //public ListItem CurrDdlAppName
    //{
    //    get
    //    {
    //        return ddlAppName.SelectedItem;
    //    }
    //    set
    //    {
    //        if (ddlAppName.Items.Contains((ListItem)value))
    //        {
    //            ddlAppName.SelectedValue = value.ToString();
    //        }
    //        else
    //        {
    //            ddlAppName.Items.Add(value.ToString());
    //            ddlAppName.SelectedValue = value.ToString();
    //        }
    //    }
    //}

    public string TxtDdlAppName
    {
        get { return this.ddlAppName.Text; }
        set { ddlAppName.Text = value; }
    }

    public string LnkViewParamsPostBackUrl
    {
        get { return this.lnkViewParams.PostBackUrl; }
        set { lnkViewParams.PostBackUrl = value; }
    }
}