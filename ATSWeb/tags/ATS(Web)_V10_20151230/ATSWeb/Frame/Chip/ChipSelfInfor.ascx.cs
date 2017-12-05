using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCX_Chip_ChipSelfInfor : System.Web.UI.UserControl
{
    #region ConfigColumNameText
    public string TH2Text
    {
        get
        {
            return this.TH2.Text;
        }
        set
        {
            this.TH2.Text = value;
        }
    }
    public string TH3Text
    {
        get
        {
            return this.TH3.Text;
        }
        set
        {
            this.TH3.Text = value;
        }
    }
    public string TH4Text
    {
        get
        {
            return this.TH4.Text;
        }
        set
        {
            this.TH4.Text = value;
        }
    }
    public string TH5Text
    {
        get
        {
            return this.TH5.Text;
        }
        set
        {
            this.TH5.Text = value;
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
#region ConfigSelectIndex
    public int Colum2TextSelected
    {
        set
        {
            Colum2Text.SelectedIndex = value;
        }
        get
        {
            return Colum2Text.SelectedIndex;
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
    public int Colum5TextSelected
    {
        set
        {
            Colum5Text.SelectedIndex = value;
        }
        get
        {
            return Colum5Text.SelectedIndex;
        }
    }
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }

}