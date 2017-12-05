using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.Common;


public partial class Frame_UserRoleFunc_blockInfo : System.Web.UI.UserControl
{
    bool loadOK = false;
    DataIO pDataIO;
    protected void Page_Load(object sender, EventArgs e)
    {

        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();

        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        if (pDataIO.OpenDatabase(true))
        {
            DataTable blockListDt = pDataIO.GetDataTable("select * from FunctionTable where blockLevel = 0 and PID=0", "BlockList");
            for (int j = 0; j < blockListDt.Rows.Count; j++)
            {
                AddDdlBlockTypeLstItem(new ListItem(blockListDt.Rows[j]["ItemName"].ToString()));
            }
            
        }
        
        loadOK = true;
        
    }
    //BlockLevel	BlockTypeID	ItemName	AliasName	Title	FunctionCode	Remarks
    public bool SetAllItemEnable(bool state,bool isAddNew=false)
    {
        if(isAddNew)
        {
                this.ddlBlockLevel.Enabled = false;
        }
        else
	    {
            this.ddlBlockLevel.Enabled = state;
	    }
        this.ddlParentItem.Enabled = state;
        this.ddlBlockType.Enabled = state;
        this.txtItemName.Enabled = state;
        this.txtAliasName.Enabled = state;
        
        lblFunctionCode.Enabled = state;
        lblTitle.Enabled = state;
        lblRemarks.Enabled = state;
        txtFunctionCode.Enabled = state;
        txtTitle.Enabled = state;
        txtRemarks.Enabled = state;
        return true;
    }
    

    public bool SetFuncItemVisable(bool isLevel0)
    {
        lblParentItem.Visible = !isLevel0;
        ddlParentItem.Visible = !isLevel0;
        lblFunctionCode.Visible = isLevel0;
        lblTitle.Visible = isLevel0;
        lblRemarks.Visible = isLevel0;
        txtFunctionCode.Visible = isLevel0;
        txtTitle.Visible = isLevel0;
        txtRemarks.Visible = isLevel0;
        return true;
    }
#region ParentLstItem
    public void AddDdlParentLstItem(ListItem li)
    {
        if (!ddlParentItem.Items.Contains(li))
        {
            ddlParentItem.Items.Add(li);
        }
    }
    public ListItem DdlParentLstItem
    {
        get
        {
            return ddlParentItem.SelectedItem;
        }
        set
        {
            if (ddlParentItem.Items.Contains((ListItem)value))
            {

                ddlParentItem.SelectedValue = value.ToString();
            }
            else
            {
                ddlParentItem.Items.Add(value.ToString());
                ddlParentItem.SelectedValue = value.ToString();
            }
        }
    }

    public string DdlParentItem
    {
        get { return this.ddlParentItem.Text; }
        set { this.ddlParentItem.Text = value; }
    }
#endregion

    public string DdlBlockLevel
    {
        get { return this.ddlBlockLevel.Text; }
        set { this.ddlBlockLevel.Text = value; }
    }
    #region ParentLstItem
    public void AddDdlBlockTypeLstItem(ListItem li)
    {
        if (!ddlBlockType.Items.Contains(li))
        {
            ddlBlockType.Items.Add(li);
        }
    }
    public ListItem DdlBlockTypeItem
    {
        get
        {
            return ddlBlockType.SelectedItem;
        }
        set
        {
            if (ddlBlockType.Items.Contains((ListItem)value))
            {

                ddlBlockType.SelectedValue = value.ToString();
            }
            else
            {
                ddlBlockType.Items.Add(value.ToString());
                ddlBlockType.SelectedValue = value.ToString();
            }
        }
    }

    public string DdlBlockTypeID
    {
        get { return this.ddlBlockType.Text; }
        set { this.ddlBlockType.Text = value; }
    }
    #endregion
    
    public string TxtItemName
    {
        get { return this.txtItemName.Text; }
        set { this.txtItemName.Text = value; }
    }
    public string TxtAliasName
    {
        get { return this.txtAliasName.Text; }
        set { this.txtAliasName.Text = value; }
    }

    public string TxtFunctionCode
    {
        get { return this.txtFunctionCode.Text; }
        set { this.txtFunctionCode.Text = value; }
    }

    public string TxtTitle
    {
        get { return this.txtTitle.Text; }
        set { this.txtTitle.Text = value; }
    }

    public string TxtRemarks
    {
        get { return this.txtRemarks.Text; }
        set { this.txtRemarks.Text = value; }
    }

    void getDdlInfo(bool isChangeType=false)
    {
        try
        {
            if (loadOK)
            {
                this.ddlParentItem.Items.Clear();
                string BlockTypeID = pDataIO.getDbCmdExecuteScalar("select BlockTypeID from FunctionTable where ItemName ='" + ddlBlockType.Text + "'").ToString();
                //AddDdlParentLstItem(new ListItem("NewBlock"));
                if (isChangeType && BlockTypeID.ToUpper() !="NOT Found Data".ToUpper())
                {
                    this.ddlBlockLevel.Items.Clear();
                    DataTable dt = pDataIO.GetDataTable("select * from FunctionTable where BlockTypeID = " + BlockTypeID , "BlockItemInfo");
                    foreach (DataRow dr in dt.Rows)
                    {
                        int maxLevel = Convert.ToInt32(dr["blockLevel"]);
                        ListItem li = new ListItem(dr["blockLevel"].ToString());                        
                        if (!ddlBlockLevel.Items.Contains(li))
                        {
                            ddlBlockLevel.Items.Add(li);
                        }
                        ListItem nextli = new ListItem((maxLevel + 1).ToString());
                        if (!ddlBlockLevel.Items.Contains(nextli))
                        {
                            ddlBlockLevel.Items.Add(nextli);
                        }
                    }
                }
                int blockLevel = Convert.ToInt32(ddlBlockLevel.Text);
                if (blockLevel > 0)
                {
                    
                    DataTable parentDt = pDataIO.GetDataTable("select * from FunctionTable where BlockTypeID = " + BlockTypeID + " and blockLevel =" + (blockLevel - 1), "BlockItemInfo");

                    for (int k = 0; k < parentDt.Rows.Count; k++)
                    {
                        AddDdlParentLstItem(new ListItem(parentDt.Rows[k]["ItemName"].ToString()));
                    }
                }
                else
                {
                    SetFuncItemVisable(true);
                }
            }
            else
            {
                this.ddlBlockLevel.Enabled = false;
                if (isChangeType)
                {
                    this.ddlBlockLevel.Items.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlBlockType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBlockType.Text.Trim().Length > 0)
        {
            getDdlInfo(true);
            if (ddlBlockType.Text.Trim() == "NewBlock")
            {
                ddlBlockLevel.Enabled =false;
                ddlBlockLevel.Text = "0";
                ddlParentItem.Visible = false;
                lblParentItem.Visible = false;
                SetFuncItemVisable(true);
                //this.txtAliasName.Text = "";
                //this.txtItemName.Text = "";
                //this.txtTitle.Text = "";
                //this.valeTxtRemarks.Text = "";
                //this.txtFunctionCode.Text = "0";

                //ddlBlockLevel.SelectedIndexChanged += new EventHandler(ddlBlockLevel_SelectedIndexChanged);
            }
            else
            {
                this.ddlBlockLevel.Enabled = true;
            }
        }
    }

    public DropDownList DDLBlockLevel
    {
        get { return this.ddlBlockLevel; }
        set { this.ddlBlockLevel = value; }
    }
    protected void ddlBlockLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBlockLevel.Text.Trim().Length > 0)
        {
            getDdlInfo(false);
            if (ddlBlockLevel.Text.Trim() == "0")
            {
                //ddlBlockType.Text = "NewBlock";                
                ddlParentItem.Visible = false;
                lblParentItem.Visible = false;
                SetFuncItemVisable(true);
                //ddlBlockType.SelectedIndexChanged += new EventHandler(ddlBlockType_SelectedIndexChanged);
            }
            else
            {
                //ddlBlockType.Items.Remove("NewBlock");
                ddlParentItem.Visible = true;
                lblParentItem.Visible = true;
                SetFuncItemVisable(false);
            }
            
        }
    }
}