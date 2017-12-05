using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_TestPlan_ModelParams : System.Web.UI.UserControl
{
    #region ConfigRangeandRegular
    public bool EnableRangeValidatorItemValue
    {
        set
        {
            RangeValidatorItemValue.Enabled = value;
        }
    }
    public bool EnableRegularExpressionValidatorItem
    {
        set
        {
            RegularExpressionValidatorItemValue.Enabled = value;
        }
    }
    public ValidationDataType ConfigRangeValidatorItemValueType
    {

        set
        {
            RangeValidatorItemValue.Type = value;
        }
    }
    public string ConfigRangeValidatorItemValueMax
    {
        set
        {
            RangeValidatorItemValue.MaximumValue = value;
        }
    }
    public string ConfigRangeValidatorItemValueMin
    {
        set
        {
            RangeValidatorItemValue.MinimumValue = value;
        }
    }
    public string ConfigRegularExpressionValidatorItemVExpression
    {
        set
        {
            RegularExpressionValidatorItemValue.ValidationExpression = value;
        }
    }
    public string ConfigRegularExpressionValidatorItemErrMessage
    {
        set
        {
            RegularExpressionValidatorItemValue.ErrorMessage = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public string myControlID
    {
        get { return this.ID; }
        set { this.ID = value; }
    }

    public string ToolTipItemDescription
    {
        get { return txtItemDescription.ToolTip; }
        set { txtItemDescription.ToolTip = value; }
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

    public bool SetModelParamsEnableState(bool state,bool isViewInfo=true)
    {
        try
        {
            this.lnkItemName.Enabled = false;
            this.txtItemValue.Enabled = state;
            //this.lblItemValue.Visible = !state;
            return true;
        }
        catch
        { return false; }
    }
    
    public string MyClientID
    {
        get{return this.ClientID;}
    }
    //public string TxtItemNamePostBackUrl
    //{
    //    get { return lnkItemName.PostBackUrl; }
    //    set { lnkItemName.PostBackUrl = ""; }    //150526 取消该超链接value
    //}

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
    public string TxtItemType
    {
        get { return txtItemType.Text; }
        set { txtItemType.Text = value; }
    }
    
    public string TxtItemValue
    {
        get { return this.txtItemValue.Text; }
        set { 
                txtItemValue.Text = value;
                //lblItemValue.Text = value;
                //AscxDDLTxt1.TxtItem.Text = value;
                //AscxDDLTxt1.DdlItem.Text = value;
            }
    }    
}