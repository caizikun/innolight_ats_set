using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXChipBaseChipList : System.Web.UI.UserControl
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
            return this.LBFormula.Text;
        }
        set
        {
            this.LBFormula.Text = value;
        }
    }


    public string LbTH1Text
    {
        get
        {
            return this.TH1.Text;

        }
        set
        {
            this.TH1.Text = value;
        }
    }
    public string LbTH2Text
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
    public string LbTH3Text
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
    public string LbTH4Text
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
    public string LbTH5Text
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
    public string LbTH6Text
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

    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        TH5Title.Visible = status;
        TH6Title.Visible = status;

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
    public string PostBackUrlStringChipSelf
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
    public string PostBackUrlStringChipFormula
    {
        get
        {
            return LBFormula.PostBackUrl;
        }
        set
        {
            LBFormula.PostBackUrl = value;
        }
    }

    #endregion
    #endregion
    #region ConfigAllDescription
    public string configAllDescription
    {
        set
        {
            AllDescription.Attributes["title"] = value;
        }
        get
        {
            return AllDescription.Attributes["title"];
        }
    }
    #endregion
    #region ConfigTrColor
    public string TrBackgroundColor
    {
        set
        {
            ContentTR.Attributes.Add("style", "background-color:" + value);
        }
    }
    #endregion
}