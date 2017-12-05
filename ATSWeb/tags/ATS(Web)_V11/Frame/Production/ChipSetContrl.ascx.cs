using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXChipSetContrl : System.Web.UI.UserControl
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
    public string LbTH6Text
    {
        get
        {
            return this.TH6Text.Text;

        }
        set
        {
            this.TH6Text.Text = value;
        }
    }
    public string LbTH7Text
    {
        get
        {
            return this.TH7Text.Text;

        }
        set
        {
            this.TH7Text.Text = value;
        }
    }
  

    public string LbTH2
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
    public string LbTH3
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
    public string LbTH4
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
    public string LbTH5
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
    public string LbTH6
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
    public string LbTH7
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
        TH0Title.Visible = status;
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
    #region ConfigPostBackURL
    
   
    public string PostBackUrlStringPNSelf
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