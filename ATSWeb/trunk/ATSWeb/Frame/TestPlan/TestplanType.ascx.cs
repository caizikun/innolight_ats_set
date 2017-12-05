using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXTestplanType : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
#region configStylle
   public string thTitleStyle
   {
       get
       {

           return TH1Title.Style["Style"];
       }
      set
       {
           TH1Title.Style["Style"] = value;
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
    public void LBTHTitleVisible(bool status)
    {


        this.TH2Title.Visible = status;
        this.TH1Title.Visible = status;
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
    public string ConfitTh1text
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
    public string ConfitTh2text
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
    public string pIDString
    {
        get
        {
            return this.LinkButton1.ID;
        }
        set
        {
            this.LinkButton1.ID = value;
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
    protected void HyperLink1_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/WebFiles/TestPlan/PN.aspx?uId=" + pIDString.Trim());
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