using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXPNType : System.Web.UI.UserControl
{
#region EnableVisible
public bool EnableTH2Visible
    {
      get
        {
            return TH2.Visible;
        }
       set
        {
             TH2.Visible = value;
        }
    }
public bool EnableTH3Visible
{
    get
    {
        return TH3.Visible;
    }
    set
    {
        TH3.Visible = value;
    }
}
public bool EnableTH4Visible
{
    get
    {
        return TH4.Visible;
    }
    set
    {
        TH4.Visible = value;
    }
}
public bool EnableTH5Visible
{
    get
    {
        return TH5.Visible;
    }
    set
    {
        TH5.Visible = value;
    }
}
public bool EnableTH6Visible
{
    get
    {
        return TH6.Visible;
    }
    set
    {
        TH6.Visible = value;
    }
}
public bool EnableTH7Visible
{
    get
    {
        return TH7.Visible;
    }
    set
    {
        TH7.Visible = value;
    }
}
public void LBTHTitleVisible(bool status)
{
   
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
#region configTH
    public string configTH2
    {
        set
        {
            TH2.Text = value;
        }
        get 
        {
            return TH2.Text;
        }
    }
    public string configTH3
    {
        set
        {
            TH3.Text = value;
        }
        get
        {
            return TH3.Text;
        }
    }
    public string configTH4
    {
        set
        {
            TH4.Text = value;
        }
        get
        {
            return TH4.Text;
        }
    }
    public string configTH5
    {
        set
        {
            TH5.Text = value;
        }
        get
        {
            return TH5.Text;
        }
    }
    public string configTH6
    {
        set
        {
            TH6.Text = value;
        }
        get
        {
            return TH6.Text;
        }
    }
    public string configTH7
    {
        set
        {
            TH7.Text = value;
        }
        get
        {
            return TH7.Text;
        }
    }
#endregion
#region configTHTEXT

public string configTHText7
{
    get
    {
        return TH7Text.Text;
    }
    set
    {
        TH7Text.Text=value;
    }
}
public string configTHText6
{
    get
    {
        return TH6Text.Text;
    }
    set
    {
        TH6Text.Text = value;
    }
}
public string configTHText5
{
    get
    {
        return TH5Text.Text;
    }
    set
    {
        TH5Text.Text = value;
    }
}
public string configTHText4
{
    get
    {
        return TH4Text.Text;
    }
    set
    {
        TH4Text.Text = value;
    }
}
public string configTHText3
{
    get
    {
        return TH3Text.Text;
    }
    set
    {
        TH3Text.Text = value;
    }
}

#endregion
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
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void HyperLink1_Load(object sender, EventArgs e)
    {

    }
    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["ChannelNumber"] = TH3Text.Text.Trim();
        Response.Redirect("~/WebFiles/TestPlan/TestPlanList.aspx?uId="+ pIDString.Trim());
        
    }
}