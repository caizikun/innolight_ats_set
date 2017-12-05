using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_Production_PNChannelMap : System.Web.UI.UserControl
{
    #region ConfigID
    public string LinkBItemNameID
    {
        set
        {
            this.TH2Text.ID = value;
        }
        get
        {
            return this.TH2Text.ID;
        }
    }
    public string ControlID
    {
        set
        {
            this.ID = value;
        }
        get
        {
            return this.ID;
        }
    }
    #endregion
    #region ConfigText
    public string LbTH2Text
    {
        get
        {
            return this.TH2Text.Text;

        }
        set
        {
            this.TH2Text.Text = value;
        }
    }
    public string LbTH3Text
    {
        get
        {
            return this.TH3Text.Text;

        }
        set
        {
            this.TH3Text.Text = value;
        }
    }
    public string LbTH4Text
    {
        get
        {
            return this.TH4Text.Text;

        }
        set
        {
            this.TH4Text.Text = value;
        }
    }

    public string LbTH5Text
    {
        get
        {
            return this.TH5Text.Text;

        }
        set
        {
            this.TH5Text.Text = value;
        }
    }



    #endregion
    #region ConfigTHVisible
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

    public void LBTHTitleVisible(bool status)
    {
        this.TH0Title.Visible = status;
        this.TH2Title.Visible = status;
        this.TH3Title.Visible = status;
        this.TH4Title.Visible = status;
        this.TH5Title.Visible = status;
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
    #region ConfigPostBackURL
  

    public string PostBackUrlStringPNChipMapSelf
    {
        get
        {
            return TH2Text.PostBackUrl;
        }
        set
        {
            TH2Text.PostBackUrl = value;
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}