using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_Production_ChannelMapSelf : System.Web.UI.UserControl
{
    #region ConfigColumValueText
    public string ConfigColum2Text
    {
        get
        {
            return Colum2Text.Text;
        }
        set
        {
            Colum2Text.Text = value;
        }
    }
    public string ConfigColum1Text
    {
        get
        {
            return Colum1Text.Text;
        }
        set
        {
            Colum1Text.Text = value;
        }
    }
    public string ConfigColum3Text
    {
        get
        {
            return Colum3Text.Text;
        }
        set
        {
            Colum3Text.Text = value;
        }
    }
    #endregion
    #region ConfigEnable
    public bool EnableColum2Text
    {
        set
        {
            Colum2Text.Enabled = value;
        }
        get
        {
            return Colum2Text.Enabled;
        }
    }
    public bool EnableColum1Text
    {
        set
        {
            Colum1Text.Enabled = value;
        }
        get
        {
            return Colum1Text.Enabled;
        }
    }
    public bool EnableColum3Text
    {
        set
        {
            Colum3Text.Enabled = value;
        }
        get
        {
            return Colum3Text.Enabled;
        }
    }


    #endregion
    public int Colum1TextSelected
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
    public int Colum2TextSelected
    {
        get
        {
            return Colum2Text.SelectedIndex;
        }
        set
        {
            Colum2Text.SelectedIndex = value;
        }
    }
    public int Colum3TextSelected
    {
        get
        {
            return Colum3Text.SelectedIndex;
        }
        set
        {
            Colum3Text.SelectedIndex = value;
        }
    }
    public void ClearDropDownList()
    {
        Colum1Text.Items.Clear();
        Colum2Text.Items.Clear();
        Colum3Text.Items.Clear();
    }
    public void InsertColum1Text(int i, ListItem li)
    {
        if (!this.Colum1Text.Items.Contains(li))
        {
            Colum1Text.Items.Insert(i, li);
        }
    }
    public void InsertColum2Text(int i, ListItem li)
    {
        if (!this.Colum2Text.Items.Contains(li))
        {
            Colum2Text.Items.Insert(i, li);
        }
    }
    public void InsertColum3Text(int i, ListItem li)
    {
        if (!this.Colum3Text.Items.Contains(li))
        {
            Colum3Text.Items.Insert(i, li);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}