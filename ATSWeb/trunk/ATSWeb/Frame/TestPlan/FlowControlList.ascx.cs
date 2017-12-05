using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
public partial class ASCXFlowControlList : System.Web.UI.UserControl
{  
    

    protected void Page_Load(object sender, EventArgs e)
    {

    }
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
    public string LbSEQText
    {
        get
        {
            return this.LbSEQ.Text;

        }
        set
        {
            this.LbSEQ.Text = value;
        }
    }
    public string LbChannelText
    {
        get
        {
            return this.LbChannel.Text;

        }
        set
        {
            this.LbChannel.Text = value;
        }
    }
    public string LbTempText
    {
        get
        {
            return this.LbTemp.Text;

        }
        set
        {
            this.LbTemp.Text = value;
        }
    }
    public string LbVccText
    {
        get
        {
            return this.LbVcc.Text;

        }
        set
        {
            this.LbVcc.Text = value;
        }
    }
    public string LbIgnoreText
    {
        get
        {
            return this.IgnorFlag.Text;

        }
        set
        {
            this.IgnorFlag.Text = value;
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

    #endregion
    #region ConfigTHVisible
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
    protected void LinkBItemName_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebFiles/Production_ATS/TestPlan/FlowContrlSelfInfor.aspx?uId=" + this.ID.Trim());
    }
    protected void LinkBReviewTestMode_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ModelList.aspx?uId=" + this.ID.Trim());
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
}