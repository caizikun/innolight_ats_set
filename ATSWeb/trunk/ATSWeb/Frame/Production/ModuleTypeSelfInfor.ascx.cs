using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXModuleTypeSelfInfor : System.Web.UI.UserControl
{
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
    public string TH3Text
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
    #region ConfigColumValueText
    public string Colum2TextConfig
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
    public string Colum3TextConfig
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
   

    #endregion
    #region ConfigEnable
    public bool EnableColum2Text
    {
        set
        {
            Colum2Text.Enabled = value;
        }
        get
        {
            return Colum2Text.Enabled;
        }
    }
    
    public bool EnableColum3Text
    {
        set
        {
            Colum3Text.Enabled = value;
        }
        get
        {
            return Colum3Text.Enabled;
        }
    }

    #endregion
#region configDropDownList
    
   
    public void InsertColum3Text(int i, ListItem li)
    {
        if (!this.Colum3Text.Items.Contains(li))
        {
            Colum3Text.Items.Insert(i,li);
        }
    }
    public void ClearDropDownList()
    {
         Colum3Text.Items.Clear();
    }
    public int ConfigSeletedIndex
    {
        get
        {
            return Colum3Text.SelectedIndex;
        }
        set
        {
            Colum3Text.SelectedIndex = value;
        }
    }

   
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}