using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_Production_PNChipSelfInfor : System.Web.UI.UserControl
{
    #region ConfigColumValueText
    
   

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
    public bool EnableColum1Text
    {
        set
        {
            Colum1Text.Enabled = value;
        }
        get
        {
            return Colum1Text.Enabled;
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
    public int Colum1TextSelected
    {
        get
        {
            return Colum1Text.SelectedIndex;
        }
        set
        {
            Colum1Text.SelectedIndex = value;
        }
    }
    public int Colum2TextSelected
    {
        get
        {
            return Colum2Text.SelectedIndex;
        }
        set
        {
            Colum2Text.SelectedIndex = value;
        }
    }
    public int Colum3TextSelected
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
    public void ClearDropDownList()
    {
        Colum1Text.Items.Clear();
    }
    public void InsertColum1Text(int i, ListItem li)
    {
        if (!this.Colum1Text.Items.Contains(li))
        {
            Colum1Text.Items.Insert(i, li);
        }
    }

   
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}