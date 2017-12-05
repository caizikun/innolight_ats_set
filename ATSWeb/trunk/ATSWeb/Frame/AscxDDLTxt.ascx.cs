using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_AscxDDLTxt : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtItem.Text = ddlItem.SelectedItem.Text;  
    }

    protected void txtItem_TextChanged(object sender, EventArgs e)
    {
        ListItem li = new ListItem(txtItem.Text.Trim(), txtItem.Text.Trim());
        if (!ddlItem.Items.Contains(li))
        {
            ddlItem.Items.Add(li);
        }
        ddlItem.Text = li.Text;
    }

    public void AddListItems(ListItem pli)
    {
        if (!ddlItem.Items.Contains(pli))
        {
            ddlItem.Items.Add(pli);
        }
    }

    public ListItem CurrDdlItem
    {
        get
        {
            return ddlItem.SelectedItem;
        }
        set
        {
            if (ddlItem.Items.Contains((ListItem)value))
            {

                ddlItem.SelectedValue = value.ToString();
            }
            else
            {
                ddlItem.Items.Add(value.ToString());
                ddlItem.SelectedValue = value.ToString();
            }
        }
    }

    public DropDownList DdlItem
    {
        set { this.ddlItem = value; }
        get { return ddlItem; }
    }

    public TextBox TxtItem
    {
        set { this.txtItem = value; }
        get { return txtItem; }
    }

    public void setNewDDLWidth(UInt16 width)
    {
        if (width>15)
        {
            this.ddlItem.Width =   new Unit (width);
            this.txtItem.Width = new Unit(width-15);
            this.txtItem.Style["Style"] = "position:absolute; margin-left:-" + width + "px;";          
        }
    }
}