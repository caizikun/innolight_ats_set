using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ASCXFlowControlSelfInfor : System.Web.UI.UserControl
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
    public string TH6Text
    {
        get
        {
            return this.TH6.Text;
        }
        set
        {
            this.TH6.Text = value;
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
    public string TH8Text
    {
        get
        {
            return this.TH8.Text;
        }
        set
        {
            this.TH8.Text = value;
        }
    }
    public string TH9Text
    {
        get
        {
            return this.TH9.Text;
        }
        set
        {
            this.TH9.Text = value;
        }
    }
    public string TH10Text
    {
        get
        {
            return this.TH10.Text;
        }
        set
        {
            this.TH10.Text = value;
        }
    }
    public string TH11Text
    {
        get
        {
            return this.TH11.Text;
        }
        set
        {
            this.TH11.Text = value;
        }
    }
    public string TH12Text
    {
        get
        {
            return this.TH12.Text;
        }
        set
        {
            this.TH12.Text = value;
        }
    }
    public string TH13Text
    {
        get
        {
            return this.TH13.Text;
        }
        set
        {
            this.TH13.Text = value;
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
    public string Colum7TextConfig
    {
        get
        {
            return this.Colum7Text.Text;
        }
        set
        {
            this.Colum7Text.Text = value;
        }
    }
    public string Colum8TextConfig
    {
        get
        {
            return this.Colum8Text.Text;
        }
        set
        {
            this.Colum8Text.Text = value;
        }
    }
    public string Colum9TextConfig
    {
        get
        {
            return this.Colum9Text.Text;
        }
        set
        {
            this.Colum9Text.Text = value;
        }
    }
    public string Colum10TextConfig
    {
        get
        {
            return this.Colum10Text.Text;
        }
        set
        {
            this.Colum10Text.Text = value;
        }
    }
    public string Colum11TextConfig
    {
        get
        {
            return this.Colum11Text.Text;
        }
        set
        {
            this.Colum11Text.Text = value;
        }
    }
    public string Colum12TextConfig
    {
        get
        {
            return this.Colum12Text.Text;
        }
        set
        {
            this.Colum12Text.Text = value;
        }
    }
    public string Colum13TextConfig
    {
        get
        {
            return this.Colum13Text.Text;
        }
        set
        {
            this.Colum13Text.Text = value;
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
    public bool EnableColum8Text
    {
        set
        {
            Colum8Text.Enabled = value;
        }
        get
        {
            return Colum8Text.Enabled;
        }
    }
    public bool EnableColum9Text
    {
        set
        {
            Colum9Text.Enabled = value;
        }
        get
        {
            return Colum9Text.Enabled;
        }
    }
    public bool EnableColum10Text
    {
        set
        {
            Colum10Text.Enabled = value;
        }
        get
        {
            return Colum10Text.Enabled;
        }
    }
    public bool EnableColum11Text
    {
        set
        {
            Colum11Text.Enabled = value;
        }
        get
        {
            return Colum11Text.Enabled;
        }
    }
    public bool EnableColum12Text
    {
        set
        {
            Colum12Text.Enabled = value;
        }
        get
        {
            return Colum12Text.Enabled;
        }
    }
    public bool EnableColum13Text
    {
        set
        {
            Colum13Text.Enabled = value;
        }
        get
        {
            return Colum13Text.Enabled;
        }
    }
#endregion
#region ConfigCtrolTypeDD
    public void ClearDropDownList()
    {
        
        Colum9Text.Items.Clear();
       
    }
    public void InsertColum9Text(int i, ListItem li)
    {
        if (!this.Colum9Text.Items.Contains(li))
        {
            Colum9Text.Items.Insert(i, li);
        }
    }

    public int ConfigSeletedCtrolType
    {
        get
        {
            return Colum9Text.SelectedIndex;
        }
        set
        {
            Colum9Text.SelectedIndex = value;
        }
    }
#endregion
    public int Colum13TextSelected
    {
        get
        {
            return Colum13Text.SelectedIndex;
        }
        set
        {
            Colum13Text.SelectedIndex = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

}