using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXMConfigInitSelfInfor : System.Web.UI.UserControl
{
#region ConfigID
    public string ConfigControlID
    {
        get
        {
            return this.ID;
        }
        set
        {
            this.ID = value;
        }
    }
    public string ConfigFiledValueID
    {
        get
        {
            return FiledName.ID;
        }
        set
        {
            FiledName.ID = value; 
            
        }
    }

#endregion
#region ConfigText
    public string ConfigFiledName
    {
        get
        {
            return this.FiledName.Text;
        }
        set
        {
            this.FiledName.Text = value;
        }
    }
    public string ConfigFiledValue
    {
        get
        {
            return this.FiledValue.Text;
        }
        set
        {
            this.FiledValue.Text = value;
        }
    }
#endregion
#region ConfigEnable
    public bool EnableFiledValue
    {
        get
        {
            return FiledValue.Enabled;
        }
        set
        {
            FiledValue.Enabled = value;
        }
    }
    public bool EnableRangeValidator1
    {
        get
        {
            return RangeValidator1.Enabled;
        }
        set
        {
            RangeValidator1.Enabled = value;
        }
    }
#endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}