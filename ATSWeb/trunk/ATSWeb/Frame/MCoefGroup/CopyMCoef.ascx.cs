using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Configuration;
using System.Data;

public partial class ASCXCopyMCoef : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #region ConfigColumNameText
    public string TH2Text
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
    
    #endregion
    #region ConfigText
    public string TBItemNameText
    {
        get
        {
            return TBItemName.Text;
        }
        set
        {
            TBItemName.Text = value;
        }

    }

    public string TxtItemDescription
    {
        get { return txtItemDescription.Text; }
        set { this.txtItemDescription.Text = value; }
    }
    
    #endregion
    
    #region  ConfigEnable
    public bool EnableTBItemName
    {
        get
        {
            return TBItemName.Enabled;
        }
        set
        {
            TBItemName.Enabled = value;
        }
    }

    public bool EnabletxtItemDescription
    {
        get
        {
            return txtItemDescription.Enabled;
        }
        set
        {
            txtItemDescription.Enabled = value;
        }
    }
    #endregion
 
}