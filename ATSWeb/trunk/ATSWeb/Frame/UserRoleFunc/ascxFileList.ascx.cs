using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXascxFileList : System.Web.UI.UserControl
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
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;

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
    public string ConfigLinkText
    {
        set
        {
            lnkItemName.Text = value;
        }
    }
    public string ConfigLableText
    {
        set
        {
            LableName.Text = value;
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}