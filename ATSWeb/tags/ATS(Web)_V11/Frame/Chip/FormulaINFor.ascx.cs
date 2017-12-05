using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCX_Chip_FormulaINFor : System.Web.UI.UserControl
{
    
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
    public string Colum6TextConfig
    {
        get
        {
            return this.Colum6Text.Text;
        }
        set
        {
            this.Colum6Text.Text = value;
        }
    }
    public string Colum7TextConfig
    {
        get
        {
            return this.Colum7Text.Text;
        }
        set
        {
            this.Colum7Text.Text = value;
        }
    }
    public string Colum8TextConfig
    {
        get
        {
            return this.Colum8Text.Text;
        }
        set
        {
            this.Colum8Text.Text = value;
        }
    }
    public string Colum9TextConfig
    {
        get
        {
            return this.Colum9Text.Text;
        }
        set
        {
            this.Colum9Text.Text = value;
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
    public bool EnableColum6Text
    {
        set
        {
            Colum6Text.Enabled = value;
        }
        get
        {
            return Colum6Text.Enabled;
        }
    }
    public bool EnableColum7Text
    {
        set
        {
            Colum7Text.Enabled = value;
        }
        get
        {
            return Colum7Text.Enabled;
        }
    }
    public bool EnableColum8Text
    {
        set
        {
            Colum8Text.Enabled = value;
        }
        get
        {
            return Colum8Text.Enabled;
        }
    }
    public bool EnableColum9Text
    {
        set
        {
            Colum9Text.Enabled = value;
        }
        get
        {
            return Colum9Text.Enabled;
        }
    }
    #endregion
    #region ConfigSelectIndex
    //public int Colum1TextSelected
    //{
    //    set
    //    {
    //        Colum1Text.SelectedIndex = value;
    //    }
    //    get
    //    {
    //        return Colum1Text.SelectedIndex;
    //    }
    //}
    public int Colum3TextSelected
    {
        set
        {
            Colum3Text.SelectedIndex = value;
        }
        get
        {
            return Colum3Text.SelectedIndex;
        }
    }
   
    #endregion
    #region configDropDownList

    public void ClearDropDownList()
    {
        Colum9Text.Items.Clear();
    }
    public void InsertColum9Text(int i, ListItem li)
    {
        if (!this.Colum9Text.Items.Contains(li))
        {
            Colum9Text.Items.Insert(i, li);
        }
    }

    public int Colum8TextSelected
    {
        get
        {
            return Colum8Text.SelectedIndex;
        }
        set
        {
            Colum8Text.SelectedIndex = value;
        }
    }
    public int Colum9TextSelected
    {
        set
        {
            Colum9Text.SelectedIndex = value;
        }
        get
        {
            return Colum9Text.SelectedIndex;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}