using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXChipsetIniSelfInfor : System.Web.UI.UserControl
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
    public string TH6Text
    {
        get
        {
            return this.TH6.Text;
        }
        set
        {
            this.TH6.Text = value;
        }
    }
    public string TH7Text
    {
        get
        {
            return this.TH7.Text;
        }
        set
        {
            this.TH7.Text = value;
        }
    }
   

    #endregion
    #region ConfigColumValueText
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
    public int Colum7TextSelected
    {
        get
        {
            return Colum7Text.SelectedIndex;
        }
        set
        {
            Colum7Text.SelectedIndex = value;
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}