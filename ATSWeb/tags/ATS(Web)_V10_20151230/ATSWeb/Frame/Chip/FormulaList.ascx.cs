using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCX_Chip_FormulaList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region ConfigID

    public string IsSelectedID
    {
        get
        {
            return this.IsSelected.ID;
        }
        set
        {
            this.IsSelected.ID = value;
        }
    }


    #endregion
    #region ConfigText
    public string ConfigTh1Text
    {
        get
        {
            return this.TextTH1.Text;
        }
        set
        {
            this.TextTH1.Text = value;
        }
    }
    public string ConfigTh2Text
    {
        get
        {
            return this.TextTH2.Text;
        }
        set
        {
            this.TextTH2.Text = value;
        }
    }
    public string ConfigTh3Text
    {
        get
        {
            return this.TextTH3.Text;
        }
        set
        {
            this.TextTH3.Text = value;
        }
    }
    public string ConfigTh4Text
    {
        get
        {
            return this.TextTH4.Text;
        }
        set
        {
            this.TextTH4.Text = value;
        }
    }
    public string ConfigTh5Text
    {
        get
        {
            return this.TextTH5.Text;
        }
        set
        {
            this.TextTH5.Text = value;
        }
    }
    public string ConfigTh6Text
    {
        get
        {
            return this.TextTH6.Text;
        }
        set
        {
            this.TextTH6.Text = value;
        }
    }
    public string ConfigTh7Text
    {
        get
        {
            return this.TextTH7.Text;
        }
        set
        {
            this.TextTH7.Text = value;
        }
    }
    public string ConfigTh8Text
    {
        get
        {
            return this.TextTH8.Text;
        }
        set
        {
            this.TextTH8.Text = value;
        }
    }
    public string ConfigTh9Text
    {
        get
        {
            return this.TextTH9.Text;
        }
        set
        {
            this.TextTH9.Text = value;
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
    #endregion
    #region ConfigTHVisible
    public bool LBTH1Visible
    {
        get
        {
            return this.TH1.Visible;
        }
        set
        {
            this.TH1.Visible = value;
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
    public bool LBTH8Visible
    {
        get
        {
            return this.TH8.Visible;
        }
        set
        {
            this.TH8.Visible = value;
        }
    }
    public bool LBTH9Visible
    {
        get
        {
            return this.TH9.Visible;
        }
        set
        {
            this.TH9.Visible = value;
        }
    }
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        TH5Title.Visible = status;
        TH6Title.Visible = status;
        TH7Title.Visible = status;
        TH8Title.Visible = status;
        TH9Title.Visible = status;
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
    #region ConfigPostBackURL
    public string PostBackUrlStringFormulaSelf
    {
        get
        {
            return TextTH1.PostBackUrl;
        }
        set
        {
            TextTH1.PostBackUrl = value;
        }
    }
 

    #endregion
    #endregion
}