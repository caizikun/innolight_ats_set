using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
public partial class ASCXE2ROMDataInfor : System.Web.UI.UserControl
{
    #region ConfigID
   
    public string ControlID
    {
        set
        {
            this.ID = value;
        }
        get
        {
            return this.ID;
        }
    }
    #endregion
    #region ConfigText
    public string ConfigAddressHexText
    {
        get
        {
            return this.AddressHexText.Text;

        }
        set
        {
            this.AddressHexText.Text = value;
        }
    }
    public string ConfigAddressDecText
    {
        get
        {
            return this.AddressDecText.Text;

        }
        set
        {
            this.AddressDecText.Text = value;
        }
    }
    public string ConfigContentText
    {
        get
        {
            return this.ContentText.Text;

        }
        set
        {
            this.ContentText.Text = value;
        }
    }
    public string ConfigFiledNameText
    {
        get
        {
            return this.FiledNameText.Text;

        }
        set
        {
            this.FiledNameText.Text = value;
        }
    }
    public string ConfigFiledDescriptionText
    {
        get
        {
            return this.FiledDescriptionText.Text;

        }
        set
        {
            this.FiledDescriptionText.Text = value;
        }
    }
   


  
    
    
   
   
   
    #endregion
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
        this.TH1Title.Visible = status;
        this.TH2Title.Visible = status;
        this.TH3Title.Visible = status;
        this.TH4Title.Visible = status;
        this.TH5Title.Visible = status;
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
#region EnableText
    
    
    public bool EnableContentText
    {
        get
        {
            return this.ContentText.Enabled;

        }
        set
        {
            this.ContentText.Enabled = value;
        }
    }
   
#endregion
    #region ConfigAllDescription
    //public string configFiledDescription
    //{
    //    set
    //    {
    //        FiledDescription.Attributes["title"] = value;
    //    }
    //    get
    //    {
    //        return FiledDescription.Attributes["title"];
    //    }
    //}
    //public string configFileNameDescription
    //{
    //    set
    //    {
    //        FileNameDescription.Attributes["title"] = value;
    //    }
    //    get
    //    {
    //        return FileNameDescription.Attributes["title"];
    //    }
    //}
    #endregion
#region ConfigREVContext
    public string ConfigREVContext
    {
        set
        {
            REVContentText.ValidationExpression = value;
        }
    }
    public bool GetValidiExpressionStatus
    {
        get
        {
            return REVContentText.IsValid;
        }
    }
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ContentText_TextChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.TextBox tet = (System.Web.UI.WebControls.TextBox)sender;
        ContentText.Text = tet.Text;
        
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("ContetTextChange");
            string[] ss = new string[2] {this.ID,""};
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });            
                    mi.Invoke(p, objs);
               
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}