using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_Chip_RegisterINfor : System.Web.UI.UserControl
{
    #region configDropDownList

    public void ClearDropDownList()
    {
        Colum5Text.Items.Clear();
    }
    public void InsertColum5Text(int i, ListItem li)
    {
        if (!this.Colum5Text.Items.Contains(li))
        {
            Colum5Text.Items.Insert(i, li);
        }
    }

    public int ConfigSeletedIndexDD5
    {
        get
        {
            return Colum5Text.SelectedIndex;
        }
        set
        {
            Colum5Text.SelectedIndex = value;
        }
    }
    public int Colum4TextSelected
    {
        set
        {
            Colum4Text.SelectedIndex = value;
        }
        get
        {
            return Colum4Text.SelectedIndex;
        }
    }
    #endregion
    #region ConfigColumValueText
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
    public string Colum2TextConfig
    {
        get
        {
            return this.Colum2Text.Text;
        }
        set
        {
            this.Colum2Text.Text = value;
        }
    }
    public string Colum3TextConfig
    {
        get
        {
            return this.Colum3Text.Text;
        }
        set
        {
            this.Colum3Text.Text = value;
        }
    }
    public string Colum4TextConfig
    {
        get
        {
            return this.Colum4Text.Text;
        }
        set
        {
            this.Colum4Text.Text = value;
        }
    }
    public string Colum5TextConfig
    {
        get
        {
            return this.Colum5Text.Text;
        }
        set
        {
            this.Colum5Text.Text = value;
        }
    }


    #endregion
    #region ConfigEnable
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
    public bool EnableColum4Text
    {
        set
        {
            Colum4Text.Enabled = value;
        }
        get
        {
            return Colum4Text.Enabled;
        }
    }
    public bool EnableColum5Text
    {
        set
        {
            Colum5Text.Enabled = value;
        }
        get
        {
            return Colum5Text.Enabled;
        }
    }

    #endregion
#region ConfigSelectDL
    public int Colum5Slected
    {
        get
        {
            return this.Colum5Text.SelectedIndex;
        }
        set
        {
            this.Colum5Text.SelectedIndex = value;
        }
    }
   
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}