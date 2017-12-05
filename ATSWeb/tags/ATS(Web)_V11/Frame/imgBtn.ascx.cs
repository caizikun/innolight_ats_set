using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_imgBtn : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string setIbtnValidationGroup
    {
        get { return this.iBtn.ValidationGroup; }
        set { this.iBtn.ValidationGroup = value; }
    }

    public string setIbtnToolTip
    {
        get { return this.iBtn.ToolTip; }
        set { this.iBtn.ToolTip = value; }
    }

    public bool setIbtnVisable(bool isVisable)
    {
        try
        {
            this.iBtn.Visible = isVisable;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public ImageAlign imgAlign
    {
        get
        {
            return this.iBtn.ImageAlign;
        }
        set { this.iBtn.ImageAlign = value; }
    }

    public string imgUrl
    {
        get
        {
            return this.iBtn.ImageUrl;
        }
        set { this.iBtn.ImageUrl = value; }
    }

    public string DescriptionUrl
    {
        get
        {
            return this.iBtn.DescriptionUrl;
        }
        set { this.iBtn.DescriptionUrl = value; }
    }

    public string linkPostBackUrl
    {
        get
        {
            return this.iBtn.PostBackUrl;
        }
        set { this.iBtn.PostBackUrl = value; }
    }
}