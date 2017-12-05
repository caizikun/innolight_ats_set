using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_ReportHeaderSpecsInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string MyClientID
    {
        get { return this.ClientID; }
    }

    public string Colum1TextConfig
    {
        get
        {
            return this.Colum1Text.Text;
        }
        set
        {
            this.Colum1Text.Text = value;
        }
    }
    public string txtShowNameConfig
    {
        get
        {
            return this.txtShowName.Text;
        }
        set
        {
            this.txtShowName.Text = value;
        }
    }
    public bool SetColum1TextState(bool state = false)
    {
        try
        {
            this.Colum1Text.Enabled = state;
            txtShowName.Enabled = state;
            return true;
        }
        catch
        { return false; }
    }

    public void ClearDropDownList()
    {
        Colum1Text.Items.Clear();
    }
    public void InsertColum1Text(int i, ListItem li)
    {
        if (!this.Colum1Text.Items.Contains(li))
        {
            Colum1Text.Items.Insert(i, li);
        }
    }

    public int ConfigSeletedSpec
    {
        get
        {
            return Colum1Text.SelectedIndex;
        }
        set
        {
            Colum1Text.SelectedIndex = value;
        }
    }

    public string SetLabel1Visible
    {
        set
        {
            Label1.Attributes.Add("style", "display:" + value);
        }
    }
}