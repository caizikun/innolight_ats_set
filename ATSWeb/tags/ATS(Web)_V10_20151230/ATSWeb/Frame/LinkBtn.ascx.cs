using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_LinkBtn : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string setBtnLinkValidationGroup
    {
        get { return this.linkBtn.ValidationGroup; }
        set { this.linkBtn.ValidationGroup = value; }
    }

    public string setBtnLinkToolTip
    {
        get { return this.linkBtn.ToolTip; }
        set { this.linkBtn.ToolTip = value; }
    }

    public bool setBtnLinkVisable(bool isVisable)
    {
        try
        {
            this.linkBtn.Visible = isVisable;
            return true;
        }
        catch 
        {
            return  false;
        }
    }

    public string linkBtnTxt
    {
        get
        {
            return this.linkBtn.Text;
        }
        set { this.linkBtn.Text = value; }
    }

    public string linkPostBackUrl
    {
        get
        {
            return this.linkBtn.PostBackUrl;
        }
        set { this.linkBtn.PostBackUrl = value; }
    }
}