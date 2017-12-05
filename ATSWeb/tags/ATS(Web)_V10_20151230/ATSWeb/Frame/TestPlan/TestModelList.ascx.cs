using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXTestModelList : System.Web.UI.UserControl
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
    public string LbStateText
    {
        get
        {
            return this.LbState.Text;

        }
        set
        {
            this.LbState.Text = value;
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
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        
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
    #region ConfigUPDownButton1
    public string ConfigUPDownButton1FID
    {
        set
        {
            this.UPDownButton1.ConfigFatherControlID = value;
        }
    }
    public string ConfigUPDownButton1SEQ
    {
        set
        {
            this.UPDownButton1.ConfigFatherControlSeq = value;
        }
    }
    public bool EnableUPButton1
    {
        get
        {
            return this.UPDownButton1.EnableButtonUP;
        }
        set
        {
             this.UPDownButton1.EnableButtonUP = value;
        }

    }
    public bool EnableDownButton1
    {
        get
        {
            return this.UPDownButton1.EnableButtonDown;
        }
        set
        {
             this.UPDownButton1.EnableButtonDown = value;
        }

    }
    #endregion
#region ConfigURL

    public string ConfigLBPostBackURL
    {
        set
        {
            LinkBItemName.PostBackUrl=value;
        }
        get
        {
            return  LinkBItemName.PostBackUrl;
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