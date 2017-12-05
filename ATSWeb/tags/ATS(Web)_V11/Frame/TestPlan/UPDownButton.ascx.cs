using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
public partial class Frame_TestPlan_UPDownButton : System.Web.UI.UserControl
{
    private string FatherControlID = "";
    private string FatherControlSeq = "";
    public string ConfigFatherControlID
    {
        get
        {
            return FatherControlID;
        }
        set
        {
            FatherControlID = value;
        }
    }
    public string ConfigFatherControlSeq
    {
        get
        {
            return FatherControlSeq;
        }
        set
        {
            FatherControlSeq = value;
        }
    }
    public bool EnableButtonUP
    {
        get
        {
            return ButtonUP.Enabled;
        }
        set
        {
            ButtonUP.Enabled = value;
        }
    }
    public bool EnableButtonDown
    {
        get
        {
            return ButtonDown.Enabled;
        }
        set
        {
            ButtonDown.Enabled = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ButtonDown_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name
            
            MethodInfo mi = pageType.GetMethod("updateData");
            string[] ss = new string[2] { this.FatherControlID,false.ToString() };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
    public void ButtonUP_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name
            MethodInfo mi = pageType.GetMethod("updateData");
            string[] ss = new string[2] { this.FatherControlID, true.ToString()};
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}