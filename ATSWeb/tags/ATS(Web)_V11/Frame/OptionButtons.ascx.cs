using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Threading;
public partial class ASCXOptionButtons : System.Web.UI.UserControl
{
    #region Attribute
#region ConfigButtonVisible
    public bool ConfigBtAddVisible
    {
        get
        {
            return BtAdd.Visible;
        }
        set
        {
            BtAdd.Visible = value;
        }
    }
    public bool ConfigBtDeleteVisible
    {
        get
        {
            return BtDelete.Visible;
        }
        set
        {
            BtDelete.Visible = value;
        }
    }
    public bool ConfigBtEditVisible
    {
        get
        {
            return BtEdit.Visible;
        }
        set
        {
            BtEdit.Visible = value;
        }
    }
    public bool ConfigBtSaveVisible
    {
        get
        {
            return BtSave.Visible;
        }
        set
        {
            BtSave.Visible = value;
        }
    }
    public bool ConfigBtCancelVisible
    {
        get
        {
            return BtCancel.Visible;
        }
        set
        {
            BtCancel.Visible = value;
        }
    }
    public bool ConfigBtCopyVisible
    {
        get
        {
            return BtCopy.Visible;
        }
        set
        {
            BtCopy.Visible = value;
        }
    }
#endregion
    #endregion
    public ASCXOptionButtons()
   {

   }
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    public void BtAdd_Click(object sender, EventArgs e)
    {

        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name
            MethodInfo mi = pageType.GetMethod("AddData");
            string[] ss = new string[2] {"", ""};
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
    public void BtDelete_Click(object sender, EventArgs e)
    {       
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("DeleteData");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        
    }
    public void BtEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("EditData");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public void BtSave_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("SaveData");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    protected void BtCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("CancelUpdata");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
    protected void BtCopy_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("CopyData");
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