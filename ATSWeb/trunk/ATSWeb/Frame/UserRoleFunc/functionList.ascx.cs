using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXfunctionList : System.Web.UI.UserControl
{
    #region ConfigTHVisible
    public bool EnableTH1Visible
    {
        set
        {

            TH1.Visible = value;
        }
        get
        {
            return TH1.Visible;
        }
    }
    public bool EnableTH2Visible
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
    public bool EnableTH3Visible
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

    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
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
    #region ConfigText
    public string ConfigLink1Text
    {
        set
        {
            lnkItemName.Text = value;
        }
    }

    public string ConfigLB1Text
    {
        set
        {
            Label1.Text = value;
        }
    }
    public string ConfigLB2Text
    {
        set
        {
            Label2.Text = value;
        }
    }
    #endregion
    #region ConfigPostURL
    public string ConfigLinkPostBackURL
    {
        set
        {
            lnkItemName.PostBackUrl = value;
        }
    }
    #endregion
    public bool configSelected
    {
        set
        {
            CheckBox1.Checked = value;
        }
        get
        {
            return CheckBox1.Checked;
        }
    }
    #region ConfigTrColor
    public string TrBackgroundColor
    {
        set
        {
            ContentTR.Attributes.Add("style", "background-color:" + value);
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}