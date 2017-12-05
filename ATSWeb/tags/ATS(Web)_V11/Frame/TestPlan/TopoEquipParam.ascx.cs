using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_TestPlan_TopoEquipParam : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string myControlID
    {
        get { return this.ID; }
        set { this.ID = value; }
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
    public bool EnableRangeValidatorItemValue
    {
        set
        {
            RangeValidatorItemValue.Enabled = value;
        }
    }
    public bool EnableRegularExpressionValidatorItemValue
    {
        set
        {
            RegularExpressionValidatorItemValue.Enabled = value;
        }
    }
    public string ConfigRegularExpressionValidatorItemValueVExprssion
    {
        set
        {
            RegularExpressionValidatorItemValue.ValidationExpression = value;
        }
    }
    public string ConfigRegularExpressionValidatorItemValueVErrMessage
    {
        set
        {
            RegularExpressionValidatorItemValue.ErrorMessage = value;
        }
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
            this.txtItemValue.Enabled = state;            
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

    public string pChkIDEquipParam
    {
        get { return chkIDEquipParam.ID.ToString(); }
        set { chkIDEquipParam.ID = value; }
    }

    public string pChkEquipParamTxt
    {
        get { return chkIDEquipParam.Text.ToString(); }
        set { chkIDEquipParam.Text = value; }
    }

    public bool chkIDEquipParamVisable
    {
        get { return this.chkIDEquipParam.Visible; }
        set { this.chkIDEquipParam.Visible = value; }
    }

    public string pToolTipItemValue
    {
        get { return txtItemValue.ToolTip; }
        set { txtItemValue.ToolTip = value; }
    }

    public string pToolTipItemDescription
    {
        get { return lblItemDescription.ToolTip; }
        set { lblItemDescription.ToolTip = value; }
    }

    public string pLblItemDescription
    {
        get { return lblItemDescription.Text; }
        set { lblItemDescription.Text = value; }
    }

    public string pTxtItemValue
    {
        get { return txtItemValue.Text; }
        set { txtItemValue.Text = value; }
    }

    public string pLblItemType
    {
        get { return lblItemType.Text; }
        set { lblItemType.Text = value; }
    }

    public string pLblItem
    {
        get { return this.lblItem.Text; }
        set { lblItem.Text = value; }
    }
}