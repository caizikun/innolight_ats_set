using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXManufConfigInitList : System.Web.UI.UserControl
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

    public string ConfigColum2Text
    {
        get
        {
            return this.Colum2Text.Text;

        }
        set
        {
            this.Colum2Text.Text = value;
        }
    }
    public string ConfigColum3Text
    {
        get
        {
            return this.Colum3Text.Text;

        }
        set
        {
            this.Colum3Text.Text = value;
        }
    }
    public string ConfigColum4Text
    {
        get
        {
            return this.Colum4Text.Text;

        }
        set
        {
            this.Colum4Text.Text = value;
        }
    }
    public string ConfigColum5Text
    {
        get
        {
            return this.Colum5Text.Text;

        }
        set
        {
            this.Colum5Text.Text = value;
        }
    }
    public string ConfigColum6Text
    {
        get
        {
            return this.Colum6Text.Text;

        }
        set
        {
            this.Colum6Text.Text = value;
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
    protected void LinkBItemName_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebFiles/Production_ATS/TestPlan/MConfigInitSelfInfor.aspx?uId=" + this.ID.Trim() + "&uIndex=" + this.LiBItemNameText);       
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