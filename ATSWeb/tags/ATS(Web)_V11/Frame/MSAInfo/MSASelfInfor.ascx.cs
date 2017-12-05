using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXMSASelfInfor : System.Web.UI.UserControl
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}