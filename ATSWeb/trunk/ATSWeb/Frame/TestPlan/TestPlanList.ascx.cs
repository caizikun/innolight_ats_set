using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
public partial class ASCXTestPlanList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public delegate void CalculateDelegate(string ddlValue, string keyWords); //定义全局委托
    public event CalculateDelegate CalculateEvent; //定义事件
    private string idarray = "";    
#region ConfigID
    public string TestplanSelfID
    {
        get
        {
            return this.LBTestplanSelf.ID;
            
        }
        set
        {
            this.LBTestplanSelf.ID = value;
        }
    }
    
    public string IsSelectedID
    {
        get
        {
            return this.IsSelected.ID;
        }
        set
        {
            this.IsSelected.ID = value;
        }
    }
    public string LabelSWVerID
    {
        get
        {
            return this.LabelSWVer.ID;
        }
        set
        {
            this.LabelSWVer.ID = value;
        }
    }
    public string LabelHWVerID
    {
        get
        {
            return this.LabelHWVer.ID;
        }
        set
        {
            this.LabelHWVer.ID = value;
        }
    }
   
#endregion
#region ConfigText
    public string TestplanSelfText
    {
        get
        {
            return this.LBTestplanSelf.Text;
        }
        set
        {
            this.LBTestplanSelf.Text = value;
        }
    }

    public string LabelSWVerText
    {
        get
        {
            return this.LabelSWVer.Text;
        }
        set
        {
            this.LabelSWVer.Text = value;
        }
    }
    public string LabelHWVerText
    {
        get
        {
            return this.LabelHWVer.Text;
        }
        set
        {
            this.LabelHWVer.Text = value;
        }
    }
    public string LabelDescription
    {
        get
        {
            return this.LBDescription.Text;
        }
        set
        {
            this.LBDescription.Text = value;
        }
    }
    public string ConfigLabelVersion
    {
        get
        {
            return this.LabelVersion.Text;
        }
        set
        {
            this.LabelVersion.Text = value;
        }
    }

    public bool ConfigOnlineImageVisible
    {
        get
        {
            return this.onlineImage.Visible;
        }
        set
        {
            this.onlineImage.Visible = value;
        }
    }

    public string ConfigOnlineImageToolTip
    {
        get
        {
            return this.onlineImage.ToolTip;
        }
        set
        {
            this.onlineImage.ToolTip = value;
        }
    }

    public bool ConfigOnlineLabelVisible
    {
        get
        {
            return this.onlineLabel.Visible;
        }
        set
        {
            this.onlineLabel.Visible = value;
        }
    }

    public string ConfigOnlineLabelText
    {
        get
        {
            return this.onlineLabel.Text;
        }
        set
        {
            this.onlineLabel.Text = value;
        }
    }

    public string LbTH1Text
    {
        get
        {
            return this.TH1.Text;

        }
        set
        {
            this.TH1.Text = value;
        }
    }
    public string LbTH2Text
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
    public string LbTH3Text
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
    public string LbTH4Text
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
    public string LbTHVersionText
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
    public string LbTH5Text
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
#endregion
#region GetSetSelected
    public bool BeSelected
    {
        get
        {
            return IsSelected.Checked;
        }
        set
        {
            this.IsSelected.Checked = value;

        }
    }
#endregion
#region ConfigTHVisible
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        TH8Title.Visible = status;
        TH5Title.Visible = status;
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
    public void LBTestplanSelf_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WebFiles/Production_ATS/TestPlan/FlowControlList.aspx?uId=" + TestplanSelfID.Trim());
    }
    //public void IsSelected_CheckedChanged(object sender, EventArgs e)
    //{
    //    //idarray += this.IsSelected.ID; 
    //    //string ddlSelectedTextValue = this.IsSelected.ID;
    //    //string keyWords = Convert.ToString(this.IsSelected.Checked);
    //    ////如果该事件已经被订阅，则抛出事件,
    //    //// 这里也预示着函数已经执行完毕，开始抛出执行后的结果了
    //    //if (CalculateEvent != null)
    //    //    CalculateEvent(ddlSelectedTextValue, keyWords);

    //}
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