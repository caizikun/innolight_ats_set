using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
public partial class Frame_Equipment_GlobalEquipParam : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
    public bool SetEnableState(bool state = false)
    {
        try
        {
            this.txtItemName.Enabled = state;
            this.txtShowName.Enabled = state;
            this.txtItemTypeDDList.Enabled = state;
            this.txtItemValue.Enabled = state;
            this.txtItemDescription.Enabled = state;
            this.txtOptionalparams.Enabled = state;
            this.ddlNeedCheckParams.Enabled = state;
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

    public string ToolTipItemValue
    {
        get { return txtItemValue.ToolTip; }
        set { txtItemValue.ToolTip = value; }
    }

    public string ToolTipItemDescription
    {
        get { return txtItemDescription.ToolTip; }
        set { txtItemDescription.ToolTip = value; }
    }

    public string TxtItemDescription
    {
        get { return txtItemDescription.Text; }
        set { txtItemDescription.Text = value; }
    }

    public string TxtItemValue
    {
        get { return txtItemValue.Text; }
        set { txtItemValue.Text = value; }
    }

    public string TxtItemType
    {
        get { return txtItemTypeDDList.Text; }
        set { txtItemTypeDDList.Text = value; }
    }

    public string TxtItemName
    {
        get { return this.txtItemName.Text; }
        set { txtItemName.Text = value; }
    }

    public string TxtShowName
    {
        get { return this.txtShowName.Text; }
        set { txtShowName.Text = value; }
    }
    public string TxtOptionalparams
    {
        get { return this.txtOptionalparams.Text; }
        set { txtOptionalparams.Text = value; }
    }
    public string TxtddlNeedCheckParams
    {
        get { return this.ddlNeedCheckParams.Text; }
        set { ddlNeedCheckParams.Text = value.ToLower(); }
    }

   

    
    public int GSItemTypeDropDownList
    {
        get
        {
            return txtItemTypeDDList.SelectedIndex;
        }
        set
        {
            txtItemTypeDDList.SelectedIndex = value;
        }
    }
    public bool EnableRangeValidatorItemValue
    {
        set
        {
            valcTxtItemValue.Enabled = value;
        }
    }
    public bool EnableRegularExpressionValidatorItemValue
    {
        set
        {
            RegularExpressionValidatorItemValue.Enabled = value;
        }
    }
    public string ConfigRegularExpressionValidatorVExprssion
    {
        set
        {
            RegularExpressionValidatorItemValue.ValidationExpression = value;
        }
    }
    public string ConfigRegularExpressionValidatorErrMessage
    {
        set
        {
            RegularExpressionValidatorItemValue.ErrorMessage = value;
        }
    }
    public ValidationDataType ConfigRangeValidatorItemValueType
    {

        set
        {
            valcTxtItemValue.Type = value;
        }
    }
    public string ConfigRangeValidatorItemValueMax
    {
        set
        {
            valcTxtItemValue.MaximumValue = value;
        }
    }
    public string ConfigRangeValidatorItemValueMin
    {
        set
        {
            valcTxtItemValue.MinimumValue = value;
        }
    }
    protected void txtItemTypeDDList_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.DropDownList odb = (System.Web.UI.WebControls.DropDownList)sender;
        txtItemTypeDDList.SelectedIndex = odb.SelectedIndex;
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("GetIndex");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}