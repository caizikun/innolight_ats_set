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
            if (value == false)
            {
                Add.Width = "0px";
            }
            else
            {
                Add.Width = "70px";
            }
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
            if (value == false)
            {
                Delete.Width = "0px";
            }
            else
            {
                Delete.Width = "63px";
            }
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
            if (value == false)
            {
                Edit.Width = "0px";
            }
            else
            {
                Edit.Width = "65px";
            }
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
            if (value == false)
            {
                Save.Width = "0px";
            }
            else
            {
                Save.Width = "68px";
            }
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
            if (value == false)
            {
                Cancel.Width = "0px";
            }
            else
            {
                Cancel.Width = "68px";
            }
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
            if (value == false)
            {
                Copy.Width = "0px";
            }
            else
            {
                Copy.Width = "70px";
            }
        }
    }
    public bool ConfigBtOrderVisible
    {
        get
        {
            return BtOrder.Visible;
        }
        set
        {
            BtOrder.Visible = value;
            if (value == false)
            {
                Order.Width = "0px";
            }
            else
            {
                Order.Width = "68px";
            }
        }
    }
    public bool ConfigBtOrderUpVisible
    {
        get
        {
            return BtOrderUp.Visible;
        }
        set
        {
            BtOrderUp.Visible = value;
            if (value == false)
            {
                OrderUp.Width = "0px";
            }
            else
            {
                OrderUp.Width = "68px";
            }
        }
    }
    public bool ConfigBtOrderDownVisible
    {
        get
        {
            return BtOrderDown.Visible;
        }
        set
        {
            BtOrderDown.Visible = value;
            if (value == false)
            {
                OrderDown.Width = "0px";
            }
            else
            {
                OrderDown.Width = "68px";
            }
        }
    }
#endregion

    #region ConfigTdWidth
    public string ConfigTdAddWidth
    {
        get
        {
            return Add.Width; 
        }
        set
        {
            Add.Width = value;
        }
    }
    public string ConfigTdDeleteWidth
    {
        get
        {
            return Delete.Width;
        }
        set
        {
            Delete.Width = value;
        }
    }
    public string ConfigTdCopyWidth
    {
        get
        {
            return Copy.Width;
        }
        set
        {
            Copy.Width = value;
        }
    }
    public string ConfigTdEditWidth
    {
        get
        {
            return Edit.Width;
        }
        set
        {
            Edit.Width = value;
        }
    }
    public string ConfigTdSaveWidth
    {
        get
        {
            return Save.Width;
        }
        set
        {
            Save.Width = value;
        }
    }
    public string ConfigTdCancelWidth
    {
        get
        {
            return Cancel.Width;
        }
        set
        {
            Cancel.Width = value;
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
    protected void BtOrder_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("OrderInfor");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    protected void BtOrderUp_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("OrderUp");
            string[] ss = new string[2] { "", "" };
            object[] objs = Array.ConvertAll<string, object>(ss, s => { return (object)s; });
            mi.Invoke(p, objs);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    protected void BtOrderDown_Click(object sender, EventArgs e)
    {
        try
        {
            Page p = this.Parent.Page;
            Type pageType = p.GetType();
            //father control name

            MethodInfo mi = pageType.GetMethod("OrderDown");
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