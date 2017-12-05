using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXProducPNList : System.Web.UI.UserControl
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
    public void LBTHTitleVisible(bool status)
    {
        this.TH0Title.Visible = status;
        this.TH2Title.Visible = status;
        this.TH3Title.Visible = status;
        this.TH4Title.Visible = status;
        this.TH5Title.Visible = status;
        this.TH6Title.Visible = status;
        this.TH7Title.Visible = status;        
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

    protected void TH2Text_Click(object sender, EventArgs e)
    {       
        Session["ChannelNumber"] = TH4Text.Text.Trim();
        Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + LinkBItemNameID.Trim());              
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