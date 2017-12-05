using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXReportHeaderSpecsEditList : System.Web.UI.UserControl
{
    #region ConfigID
    public string LinkBItemNameID
    {
        set
        {
            this.LinkBItemName.ID = value;
        }
        get
        {
            return this.LinkBItemName.ID;
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
    public string LiBItemNameText
    {
        get
        {
            return this.LinkBItemName.Text;

        }
        set
        {
            this.LinkBItemName.Text = value;
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

    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;       
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
    #endregion

    #region ConfigTrColor
    public string TrBackgroundColor
    {
        set
        {
            ContentTR.Attributes.Add("style", "background-color:" + value);
        }
        get
        {
            return ContentTR.Style.Value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}