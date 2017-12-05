using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class ASCXPNActionList : System.Web.UI.UserControl
{
    #region ConfigTHVisible
    public void Column1Visible(bool status)
    {
        TH0Title.Visible = status;
        TD1.Visible = status;
    }

    public void Column3Visible(bool status)
    {
        TH2Title.Visible = status;
        TD3.Visible = status;
    }

    public void Column4Visible(bool status)
    {
        TH3Title.Visible = status;
        TD4.Visible = status;
    }

    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
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

    #region ConfigText
    public string ConfigLabelPNText
    {
        get
        {
            return LabelPN.Text;
        }
        set
        {
            LabelPN.Text = value;
        }
    }

    public string ConfigLabelActionID
    {
        get
        {
            return LabelActionID.Text;
        }
        set
        {
            LabelActionID.Text = value;
        }
    } 

    #endregion

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
    public string ConfigLabelPNID
    {
        get
        {
            return LabelPN.ID;
        }
        set
        {
            LabelPN.ID = value;
        }
    }
    #endregion

    public bool CheckBoxAddPlanSelected
    {
        get
        {
            return CheckBoxAddPlan.Checked;
        }
        set 
        {
            CheckBoxAddPlan.Checked = value;
        }
    }

    public bool CheckBoxEditSelected
    {
        get
        {
            return CheckBoxEdit.Checked;
        }
        set
        {
            CheckBoxEdit.Checked = value;
        }
    }

    public string RadioButtonName
    {
        get
        {
            return this.RadioButton1.GroupName;
        }
        set
        {
            this.RadioButton1.GroupName = value;
        }
    }

    public bool RadioButtonChecked
    {
        get
        {
            return this.RadioButton1.Checked; 
        }
        set
        {
            this.RadioButton1.Checked = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name
            MethodInfo mi = pageType.GetMethod("PNCheckChanged");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    public void RadioEnable(bool status)
    {
        CheckBoxAddPlan.Enabled = status;
        CheckBoxEdit.Enabled  = status;
    }
}