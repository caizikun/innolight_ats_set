using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_APPModel_ModelRelationList : System.Web.UI.UserControl
{
    #region ConfigTHVisible
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
    public string MyClientID
    {
        get{return this.ClientID;}
    }

    public bool ChkIDModelRelationChecked
    {
        get { return chkIDModelRelation.Checked; }
        set { chkIDModelRelation.Checked = value; }
    }

    public string ChkIDModelRelation
    {
        get { return chkIDModelRelation.ID.ToString(); }
        set { chkIDModelRelation.ID = value; }
    }

    public bool chkIDModelRelationVisable
    {
        get { return this.chkIDModelRelation.Visible; }
        set { this.chkIDModelRelation.Visible = value; }
    }

    public string TxtModelName
    {
        get { return txtModelName.Text; }
        set { txtModelName.Text = value; }
    }

    public string TxtModelWeight
    {
        get { return txtModelWeight.Text; }
        set { txtModelWeight.Text = value; }
    }

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