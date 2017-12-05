﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXMSAModuleTypeList : System.Web.UI.UserControl
{
    public string ConfigLbText
    {
        get
        {
            return Lb.Text;
        }
        set
        {
            Lb.Text = value;
        }
    }
    public string ConfigLb2Text
    {
        get
        {
            return Lb2.Text;
        }
        set
        {
            Lb2.Text = value;
        }
    }
   
    public string LinkText1
    {
        get
        {
            return this.LinkButton1.Text;
        }
        set
        {
            this.LinkButton1.Text = value;
        }
    }
    public string LinkText2
    {
        get
        {
            return this.LinkButton2.Text;
        }
        set
        {
            this.LinkButton2.Text = value;
        }
    }
    
    public string PostBackUrlString
    {
        get
        {
            return this.LinkButton1.PostBackUrl;
        }
        set
        {
            this.LinkButton1.PostBackUrl = value;
        }
    }
    public string PostBackUrlStringPN    {
        get
        {
            return this.LinkButton2.PostBackUrl;
        }
        set
        {
            this.LinkButton2.PostBackUrl = value;
        }
    }
   
    #region EnableTHVisible

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
    public bool EnabelTH1Visible
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
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH5Title.Visible = status;
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
    public string TH1TEXT
    {
        get
        {
            return TH1.Text;
        }
        set
        {
            TH1.Text = value;
        }
    }
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
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}