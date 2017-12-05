using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXTopPNSpecsList : System.Web.UI.UserControl
{
    #region ConfigText 
    
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

    public string ConfigCloum2Text
    {
        get
        {
            return this.Cloum2Text.Text;

        }
        set
        {
            this.Cloum2Text.Text = value;
        }
    }
    public string ConfigCloum3Text
    {
        get
        {
            return this.Cloum3Text.Text;

        }
        set
        {
            this.Cloum3Text.Text = value;
        }
    }
    public string ConfigCloum4Text
    {
        get
        {
            return this.Cloum4Text.Text;

        }
        set
        {
            this.Cloum4Text.Text = value;
        }
    }
    public string ConfigCloum5Text
    {
        get
        {
            return this.Cloum5Text.Text;

        }
        set
        {
            this.Cloum5Text.Text = value;
        }
    }
    public string ConfigCloum6Text
    {
        get
        {
            return this.Cloum6Text.Text;

        }
        set
        {
            this.Cloum6Text.Text = value;
        }
    }
    public string ConfigCloum7Text
    {
        get
        {
            return this.Cloum7Text.Text;

        }
        set
        {
            this.Cloum7Text.Text = value;
        }
    }
    #endregion
    #region ConfigURL

    public string ConfigCloum2TextURL
    {
        set
        {
            Cloum2Text.PostBackUrl = value;
        }
        get
        {
            return Cloum2Text.PostBackUrl;
        }
    }

    #endregion
    #region ConfigTHVisible
    public bool THSelectedVisible
    {
        get
        {
            return this.THSelected.Visible;
        }
        set
        {
            this.THSelected.Visible = value;
        }
    }
    
    public bool LBTH2Visible
    {
        get
        {
            return this.TH2.Visible;
        }
        set
        {
            this.TH2.Visible = value;
        }
    }
    public bool LBTH3Visible
    {
        get
        {
            return this.TH3.Visible;
        }
        set
        {
            this.TH3.Visible = value;
        }
    }
    public bool LBTH4Visible
    {
        get
        {
            return this.TH4.Visible;
        }
        set
        {
            this.TH4.Visible = value;
        }
    }
    public bool LBTH5Visible
    {
        get
        {
            return this.TH5.Visible;
        }
        set
        {
            this.TH5.Visible = value;
        }
    }
    public bool LBTH6Visible
    {
        get
        {
            return this.TH6.Visible;
        }
        set
        {
            this.TH6.Visible = value;
        }
    }
    public bool LBTH7Visible
    {
        get
        {
            return this.TH7.Visible;
        }
        set
        {
            this.TH7.Visible = value;
        }
    }
    public void LBTHTitleVisible(bool status)
    {
        THSelected.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        TH5Title.Visible = status;
        TH6Title.Visible = status;
        TH7Title.Visible = status;
    }
    public bool ContentTRVisible
    {
        get
        {
            return this.ContentTR.Visible;
        }
        set
        {
            this.ContentTR.Visible = value;
        }
    }
    #endregion
    #region GetSetSelected
    public bool BeSelected
    {
        get
        {
            return IsSelected.Checked;
        }
        set
        {
            this.IsSelected.Checked = value;

        }
    }
    public bool SelectedVisible
    {
        get
        {
            return tdSelected.Visible;
        }
        set
        {
            tdSelected.Visible = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}