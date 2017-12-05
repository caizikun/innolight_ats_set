using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frame_TestPlan_EquipList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    #region ConfigTHVisible
    public bool LBTH1Visible
    {
        get
        {
            return this.TH1.Visible;
        }
        set
        {
            this.TH1.Visible = value;
        }
    }
    public bool LBTH2Visible
    {
        get
        {
            return this.TH2.Visible;
        }
        set
        {
            this.TH2.Visible = value;
        }
    }
    public bool LBTH3Visible
    {
        get
        {
            return this.TH3.Visible;
        }
        set
        {
            this.TH3.Visible = value;
        }
    }
    public bool LBTH4Visible
    {
        get
        {
            return this.TH4.Visible;
        }
        set
        {
            this.TH4.Visible = value;
        }
    }
    public bool LBTH5Visible
    {
        get
        {
            return this.TH5.Visible;
        }
        set
        {
            this.TH5.Visible = value;
        }
    }
    public void LBTHTitleVisible(bool status)
    {
        TH0Title.Visible = status;
        TH1Title.Visible = status;
        TH2Title.Visible = status;
        TH3Title.Visible = status;
        TH4Title.Visible = status;
        TH5Title.Visible = status;
        TH6Title.Visible = status;
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
    public bool SetItemDescriptionState(bool state)
    {
        try
        {
            this.txtItemDescription.Visible = state;
            return true;
        }
        catch
        {
            return false;
        }
    }

    //public bool SetEquipVisableState(bool isTopoEquip)
    //{
    //    try
    //    {
    //        this.ddlRole.Visible = isTopoEquip;   //TopoEquip 需要显示
    //        this.lnkItemName.Visible = isTopoEquip; //TopoEquip 需要显示
    //        //this.txtItemName.Visible = !isTopoEquip;//TopoEquip 不需要显示
    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    public bool SetEquipEnableState(bool state = false, bool lnkState = true)
    {
        try
        {
            this.txtItemType.Enabled = false;
            this.lnkItemName.Enabled = lnkState;           
            this.txtItemDescription.Enabled = false;
            this.ddlRole.Enabled = state;
            return true;
        }
        catch
        { return false; }
    }

    public string LblSeq
    {
        get { return lblSeq.Text; }
        set { lblSeq.Text = value; }
    }

    public bool LblSeqVisable
    {
        get { return lblSeq.Visible; }
        set { lblSeq.Visible = value; }
    }

    public string MyClientID
    {
        get{return this.ClientID;}
    }

    public string ChkIDEquip
    {
        get { return chkIDEquip.ID.ToString(); }
        set { chkIDEquip.ID = value; }
    }

    public bool chkIDEquipVisable
    {
        get { return this.chkIDEquip.Visible; }
        set { this.chkIDEquip.Visible = value; }
    }

    public bool chkIDEquipChecked
    {
        get { return this.chkIDEquip.Checked; }
        set { this.chkIDEquip.Checked = value; }
    }

    public string ChkEquipTxt
    {
        get { return chkIDEquip.Text.ToString(); }
        set { chkIDEquip.Text = value; }
    }
    public string LnkItemNamePostBackUrl
    {
        get { return lnkItemName.PostBackUrl; }
        set { lnkItemName.PostBackUrl = value; }
    }
    //public string ToolTipItemName
    //{
    //    get { return txtItemName.ToolTip; }
    //    set { txtItemName.ToolTip = value; }
    //}
    
    public string ToolTipItemDescription
    {
        get { return txtItemDescription.ToolTip; }
        set { txtItemDescription.ToolTip = value; }
    }

    public string TxtItemDescription
    {
        get { return txtItemDescription.Text; }
        set { txtItemDescription.Text = value; }
    }
    
    public string LnkItemName
    {
        get { return lnkItemName.Text; }
        set { this.lnkItemName.Text = value; }
    }

    public string RoleTxt
    {
        get {
                if (ddlRole.Text == "NA")
                {
                    return "0";
                }
                else if (ddlRole.Text == "TX")
                {
                    return "1";
                }
                else if (ddlRole.Text == "RX")
                {
                    return "2";
                }
                else
                {
                    return "0";
                }
        }
        set
        {
            if (value.ToUpper().Trim() == "0")
            {
                ddlRole.Text = "NA";
            }
            else if (value.ToUpper().Trim() == "1")
            {
                ddlRole.Text = "TX";
            }
            else if (value.ToUpper().Trim() == "2")
            {
                ddlRole.Text = "RX";
            }
        }
    }

    public string TxtItemType
    {
        get { return txtItemType.Text; }
        set { txtItemType.Text = value; }
    }

    #region ConfigUPDownSeqBtn
    public string ConfigUPDownSeqBtnFID
    {
        set
        {
            this.UPDownSeqBtn.ConfigFatherControlID = value;
        }
    }
    public string ConfigUPDownSeqBtnSEQ
    {
        set
        {
            this.UPDownSeqBtn.ConfigFatherControlSeq = value;
        }
    }
    public bool UPDownSeqBtnVisable
    {
        get { return UPDownSeqBtn.Visible; }
        set { UPDownSeqBtn.Visible = value; }
    }
    public bool EnableUPButton1
    {
        get
        {
            return this.UPDownSeqBtn.EnableButtonUP;
        }
        set
        {
            this.UPDownSeqBtn.EnableButtonUP = value;
        }

    }
    public bool EnableDownButton1
    {
        get
        {
            return this.UPDownSeqBtn.EnableButtonDown;
        }
        set
        {
            this.UPDownSeqBtn.EnableButtonDown = value;
        }

    }

    #endregion
}