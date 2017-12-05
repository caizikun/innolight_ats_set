using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
public partial class ASCXPNSpecInfor : System.Web.UI.UserControl
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
    public string TH4Text
    {
        get
        {
            return this.TH4.Text;
        }
        set
        {
            this.TH4.Text = value;
        }
    }
    public string TH5Text
    {
        get
        {
            return this.TH5.Text;
        }
        set
        {
            this.TH5.Text = value;
        }
    }
    public string TH7Text
    {
        get
        {
            return this.TH7.Text;
        }
        set
        {
            this.TH7.Text = value;
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
    public string Colum4TextConfig
    {
        get
        {
            return this.Colum4Text.Text;
        }
        set
        {
            this.Colum4Text.Text = value;
        }
    }
    public string Colum5TextConfig
    {
        get
        {
            return this.Colum5Text.Text;
        }
        set
        {
            this.Colum5Text.Text = value;
        }
    }
    public string Colum6TextConfig
    {
        get
        {
            return this.Colum6Text.Text;
        }
        set
        {
            this.Colum6Text.Text = value;
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

    public bool EnableColum4Text
    {
        set
        {
            Colum4Text.Enabled = value;
        }
        get
        {
            return Colum4Text.Enabled;
        }
    }
    public bool EnableColum5Text
    {
        set
        {
            Colum5Text.Enabled = value;
        }
        get
        {
            return Colum5Text.Enabled;
        }
    }
    public bool EnableColum6Text
    {
        set
        {
            Colum6Text.Enabled = value;
        }
        get
        {
            return Colum6Text.Enabled;
        }
    }
    public bool EnableColum7Text
    {
        set
        {
            Colum7Text.Enabled = value;
        }
        get
        {
            return Colum7Text.Enabled;
        }
    }
    #endregion
    #region configDropDownList


    public void InsertColum2Text(int i, ListItem li)
    {       
            Colum2Text.Items.Insert(i, li);        
    }
    public void InsertColum7Text(int i, ListItem li)
    {
        Colum7Text.Items.Insert(i, li);
    }
    public void ClearDropDownList()
    {
        Colum2Text.Items.Clear();
        Colum7Text.Items.Clear();
    }
    public int ConfigSeletedIndex
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
    public int ConfigSeletedIndexChannel
    {
        get
        {
            return Colum7Text.SelectedIndex;
        }
        set
        {
            Colum7Text.SelectedIndex = value;
        }
    }


    #endregion
#region ConfigRange
    public string ConfigRangeValidatorTypicalMin
    {
        set
        {
            RangeValidatorTypical.MinimumValue = value;
        }
    }
    public string ConfigRangeValidatorTypicalMax
    {
        set
        {
            RangeValidatorTypical.MaximumValue = value;
        }
    }
    public string ConfigRangeValidatorMinMax
    {
        set
        {
            RangeValidatorMin.MaximumValue = value;
        }
    }
    public string ConfigRangeValidatorMaxMin
    {
        set
        {
            RangeValidatorMax.MinimumValue = value;
        }
    }
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //protected void Colum4Text_TextChanged(object sender, EventArgs e)
    //{
    //    RangeValidator1.MinimumValue = Colum4Text.Text;
    //    RangeValidator3.MinimumValue = Colum4Text.Text;
    //}
    //protected void Colum5Text_TextChanged(object sender, EventArgs e)
    //{
    //    RangeValidator1.MaximumValue = Colum5Text.Text;
    //    RangeValidator2.MaximumValue = Colum5Text.Text;
    //}
    //protected void Colum3Text_TextChanged(object sender, EventArgs e)
    //{
    //    RangeValidator1.MinimumValue = Colum4Text.Text;
    //    RangeValidator1.MaximumValue = Colum5Text.Text;
    //}
    protected void Colum2Text_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.DropDownList odb = (System.Web.UI.WebControls.DropDownList)sender;
        Colum2Text.SelectedIndex = odb.SelectedIndex;
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("GetSpceUnit");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}