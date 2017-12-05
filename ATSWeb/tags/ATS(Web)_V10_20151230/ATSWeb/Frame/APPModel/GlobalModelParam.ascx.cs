using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
public partial class Frame_APPModel_GlobalModelParam : System.Web.UI.UserControl
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
            RangeValidatorItemValue.Type=value;
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
    public int GSItemTypeDropDownList
    {
        get
        {
            return ItemTypeDropDownList.SelectedIndex;
        }
        set
        {
            ItemTypeDropDownList.SelectedIndex = value;
        }
    }
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

    public bool SetModelParamsEnableState(bool state)
    {
        try
        {           
            this.txtItemName.Enabled = state;
            this.txtShowName.Enabled = state;
            this.ItemTypeDropDownList.Enabled = state;
            this.txtItemValue.Enabled = state;
            this.txtItemDescription.Enabled = state;
            this.ddlNeedCheckParams.Enabled = state;
            this.txtOptionalparams.Enabled = state;
            return true;
        }
        catch
        { return false; }
    }

    public string MyClientID
    {
        get { return this.ClientID; }
    }

    public string ToolTipItemName
    {
        get { return txtItemName.ToolTip; }
        set { txtItemName.ToolTip = value; }
    }
    public string ToolTipShowName
    {
        get { return txtShowName.ToolTip; }
        set { txtShowName.ToolTip = value; }
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

    public string TxtItemName
    {
        get { return txtItemName.Text; }
        set { txtItemName.Text = value; }
    }
    public string TxtShowName
    {
        get { return txtShowName.Text; }
        set { txtShowName.Text = value; }
    }
    
    public string TxtItemValue
    {
        get { return this.txtItemValue.Text; }
        set { txtItemValue.Text = value; }
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
    

    #region oldDDL+TXT

    //public void AddSpecMaxListitems(ListItem pli)
    //{
    //    if (!ddlSpecMax.Items.Contains(pli))
    //    {
    //        ddlSpecMax.Items.Add(pli);
    //    }
    //}
    //public void AddSpecMinListitems(ListItem li)
    //{
    //    if (!ddlSpecMin.Items.Contains(li))
    //    {
    //        ddlSpecMin.Items.Add(li);
    //    }
    //}
    //public ListItem DdlSpecMin
    //{
    //    get
    //    {
    //        return ddlSpecMin.SelectedItem;
    //    }
    //    set
    //    {
    //        if (ddlSpecMin.Items.Contains((ListItem)value))
    //        {

    //            ddlSpecMin.SelectedValue = value.ToString();
    //        }
    //        else
    //        {
    //            ddlSpecMin.Items.Add(value.ToString());
    //            ddlSpecMin.SelectedValue = value.ToString();
    //        }
    //    }
    //}

    //public ListItem DdlSpecMax
    //{
    //    get
    //    {
    //        return ddlSpecMax.SelectedItem;
    //    }
    //    set
    //    {
    //        if (ddlSpecMax.Items.Contains((ListItem)value))
    //        {

    //            ddlSpecMax.SelectedValue = value.ToString();
    //        }
    //        else
    //        {
    //            ddlSpecMax.Items.Add(value.ToString());
    //            ddlSpecMax.SelectedValue = value.ToString();
    //        }
    //    }
    //}
    //protected void txtSpecMax_TextChanged(object sender, EventArgs e)
    //{
    //    ListItem li = new ListItem(txtSpecMax.Text.Trim());
    //    if (!ddlSpecMax.Items.Contains(li))
    //    {
    //        ddlSpecMax.Items.Add(li);
    //    }
    //    ddlSpecMax.Text = li.Text;
    //}
    //protected void txtSpecMin_TextChanged(object sender, EventArgs e)
    //{
    //    ListItem li = new ListItem(txtSpecMin.Text.Trim());
    //    if (!ddlSpecMin.Items.Contains(li))
    //    {
    //        ddlSpecMin.Items.Add(li);
    //    }
    //    ddlSpecMin.Text = li.Text;
    //}
    //protected void ddlSpecMax_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtSpecMax.Text = ddlSpecMax.SelectedItem.Text;
    //}
    //protected void ddlSpecMin_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtSpecMin.Text = ddlSpecMin.SelectedItem.Text;
    //}

    //public bool SetSpecAutoPostBack(bool state)
    //{
    //    this.ddlSpecMax.AutoPostBack = state;
    //    this.ddlSpecMin.AutoPostBack = state;
    //    this.txtSpecMax.AutoPostBack = state;
    //    this.txtSpecMax.AutoPostBack = state;
    //    return true;
    //}
    #endregion
    protected void ItemTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.DropDownList odb = (System.Web.UI.WebControls.DropDownList)sender;
        ItemTypeDropDownList.SelectedIndex = odb.SelectedIndex;
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