using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Configuration;
using System.Data;

public partial class Frame_MCoefGroup_GlobalMCoefParam : System.Web.UI.UserControl
{
    DataIO pDataIO;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
            string dbName = "";
            string userId = ConfigurationManager.AppSettings["UserId"].ToString();
            string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();

            if (Session["DB"] == null)
            {
                Response.Redirect("~/Default.aspx", true);
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                dbName = ConfigurationManager.AppSettings["DbName"].ToString();
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                dbName = ConfigurationManager.AppSettings["DbName2"].ToString();
            }

            pDataIO = new SqlManager(serverName, dbName, userId, pwd);

            if (!IsPostBack)
            {
                string type = ddlItemType.SelectedItem.ToString();
                string ItemName = txtItemName.Text;
                txtItemName.ToolTip = ItemName;

                DataTable dtFirmwareItemName = new DataTable();
                if (pDataIO.OpenDatabase(true))
                {
                    string sql = "select distinct GlobalManufactureCoefficients.ItemName from GlobalManufactureCoefficients,GlobalManufactureCoefficientsGroup where GlobalManufactureCoefficientsGroup.ID=GlobalManufactureCoefficients.PID and GlobalManufactureCoefficients.ItemTYPE='" + type + "' and GlobalManufactureCoefficientsGroup.IgnoreFlag=0";
                    dtFirmwareItemName = pDataIO.GetDataTable(sql, "FirmwareItemName");                  
                }

                ddlItem.Items.Clear();
                ddlItem.Items.Add(" ");

                for (int i = 0; i < dtFirmwareItemName.Rows.Count; i++)
                {
                    ddlItem.Items.Add(dtFirmwareItemName.Rows[i]["ItemName"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    public bool SetMCoefParamsEnableState(bool state)
    {
        try
        {            
            this.txtItemName.Enabled = state;
            this.ddlItem.Enabled = state;
            this.ddlItemType.Enabled = state;
            this.ddlChannel.Enabled = state;
            this.txtPage.Enabled = state;
            this.txtStartAddress.Enabled = state;
            this.txtLength.Enabled = state;
            this.ddlFormat.Enabled = state;
            this.txtAmplify.Enabled = state;
            return true;
        }
        catch
        { return false; }
    }

    public string MyClientID
    {
        get { return this.ClientID; }
    }

    public string ToolTipItemName
    {
        get { return txtItemName.ToolTip; }
        set { txtItemName.ToolTip = value; }
    }

    public string TxtItemName
    {
        get { return txtItemName.Text; }
        set { txtItemName.Text = value; }
    }

    public DropDownList DdlItemType
    {
        get { return ddlItemType; }
        set { ddlItemType = value; }
    }

    public string TxtItemType
    {
        get { return ddlItemType.Text; }
        set { ddlItemType.Text = value; }
    }

    public DropDownList DdlChannel
    {
        get { return ddlChannel; }
        set { ddlChannel = value; }
    }

    public string TxtDdlChannel
    {
        get { return ddlChannel.Text; }
        set
        {
            ddlChannel.Text = value;
        }
    }

    public string TxtPage
    {
        get { return this.txtPage.Text; }
        set { txtPage.Text = value; }
    }
    public string TxtStartAddress
    {
        get { return this.txtStartAddress.Text; }
        set { txtStartAddress.Text = value; }
    }
    public string TxtLength
    {
        get { return txtLength.Text; }
        set
        {
            txtLength.Text = value;
        }
    }
    public DropDownList DdlFormat
    {
        get { return ddlFormat; }
        set
        {
            ddlFormat = value;
        }
    }

    public string TxtDdlFormat
    {
        get { return ddlFormat.Text; }
        set
        {
            ddlFormat.Text = value;
        }
    }

    public string TxtAmplify
    {
        get { return txtAmplify.Text; }
        set
        {
            txtAmplify.Text = value;
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtItemName.Text = ddlItem.SelectedItem.Text;
        txtItemName.ToolTip = ddlItem.SelectedItem.Text;
    }

    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtItemName.Text = null;
        txtItemName.ToolTip = null;
        DataTable dtItemName = new DataTable();
        if (pDataIO.OpenDatabase(true))
        {
            string sql = "select distinct GlobalManufactureCoefficients.ItemName from GlobalManufactureCoefficients,GlobalManufactureCoefficientsGroup where GlobalManufactureCoefficientsGroup.ID=GlobalManufactureCoefficients.PID and GlobalManufactureCoefficients.ItemTYPE='" + ddlItemType.SelectedItem.ToString()  + "' and GlobalManufactureCoefficientsGroup.IgnoreFlag=0";
            dtItemName = pDataIO.GetDataTable(sql, "FirmwareItemName");
        }

        ddlItem.Items.Clear();
        ddlItem.Items.Add(" ");

        for (int i = 0; i < dtItemName.Rows.Count; i++)
        {
            ddlItem.Items.Add(dtItemName.Rows[i]["ItemName"].ToString());
        }
    }
}