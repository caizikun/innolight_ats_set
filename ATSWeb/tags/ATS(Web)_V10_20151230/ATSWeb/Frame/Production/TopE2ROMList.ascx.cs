using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXTopE2ROMList : System.Web.UI.UserControl
{   
    public string ConfigData3StatusText
    {
        get
        {
            return Data3Status.Text;
        }
        set
        {
            Data3Status.Text = value;
        }
    }
    public string ConfigData2StatusText
    {
        get
        {
            return Data2Status.Text;
        }
        set
        {
            Data2Status.Text = value;
        }
    }
    public string ConfigData1StatusText
    {
        get
        {
            return Data1Status.Text;
        }
        set
        {
            Data1Status.Text = value;
        }
    }
    public string ConfigData0StatusText
    {
        get
        {
            return Data0Status.Text;
        }
        set
        {
            Data0Status.Text = value;
        }
    }
    public string LinkButtonSelfInforText
    {
        get
        {
            return this.LinkButtonSelfInfor.Text;
        }
        set
        {
            this.LinkButtonSelfInfor.Text = value;
        }
    }
    
    public string PostBackUrlStringSelfInfor
    {
        get
        {
            return this.LinkButtonSelfInfor.PostBackUrl;
        }
        set
        {
            this.LinkButtonSelfInfor.PostBackUrl = value;
        }
    }
    public string PostBackUrlStringData0
    {
        get
        {
            return this.LinkButtonData0.PostBackUrl;
        }
        set
        {
            this.LinkButtonData0.PostBackUrl = value;
        }
    }
    public string PostBackUrlStringData1
    {
        get
        {
            return this.LinkButtonData1.PostBackUrl;
        }
        set
        {
            this.LinkButtonData1.PostBackUrl = value;
        }
    }
    public string PostBackUrlStringData2
    {
        get
        {
            return this.LinkButtonData2.PostBackUrl;
        }
        set
        {
            this.LinkButtonData2.PostBackUrl = value;
        }
    }
    public string PostBackUrlStringData3
    {
        get
        {
            return this.LinkButtonData3.PostBackUrl;
        }
        set
        {
            this.LinkButtonData3.PostBackUrl = value;
        }
    }
    #region EnableTHVisible
    public bool EnabelTH2Visible
    {
        set
        {
            TH2.Visible = value;
        }
        get
        {
            return TH2.Visible;
        }
    }
    public bool EnabelTH3Visible
    {
        set
        {
            TH3.Visible = value;
        }
        get
        {
            return TH3.Visible;
        }
    }
    public bool EnabelTH4Visible
    {
        set
        {
            TH4.Visible = value;
        }
        get
        {
            return TH4.Visible;
        }
    }
    public bool EnabelTH5Visible
    {
        set
        {
            TH5.Visible = value;
        }
        get
        {
            return TH5.Visible;
        }
    }
    public bool EnabelTH6Visible
    {
        set
        {
            TH6.Visible = value;
        }
        get
        {
            return TH6.Visible;
        }
    }
    public bool EnabelTH7Visible
    {
        set
        {
            TH7.Visible = value;
        }
        get
        {
            return TH7.Visible;
        }
    }
    public bool EnabelTH8Visible
    {
        set
        {
            TH8.Visible = value;
        }
        get
        {
            return TH8.Visible;
        }
    }
    public bool EnabelTH9Visible
    {
        set
        {
            TH9.Visible = value;
        }
        get
        {
            return TH9.Visible;
        }
    }
    public bool EnabelTH10Visible
    {
        set
        {
            TH10.Visible = value;
        }
        get
        {
            return TH10.Visible;
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
        TH8Title.Visible = status;
        TH9Title.Visible = status;
        TH10Title.Visible = status;
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
    #region ConfigTHText
    public string TH2TEXT
    {
        get
        {
            return TH2.Text;
        }
        set
        {
            TH2.Text = value;
        }
    }
    public string TH3TEXT
    {
        get
        {
            return TH3.Text;
        }
        set
        {
            TH3.Text = value;
        }
    }
    public string TH5TEXT
    {
        get
        {
            return TH5.Text;
        }
        set
        {
            TH5.Text = value;
        }
    }
    public string TH7TEXT
    {
        get
        {
            return TH7.Text;
        }
        set
        {
            TH7.Text = value;
        }
    }
    public string TH9TEXT
    {
        get
        {
            return TH9.Text;
        }
        set
        {
            TH9.Text = value;
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