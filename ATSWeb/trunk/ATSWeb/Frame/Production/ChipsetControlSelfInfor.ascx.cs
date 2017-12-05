﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Configuration;
using System.Data;

public partial class ASCXChipsetControlSelfInfor : System.Web.UI.UserControl
{
    DataIO pDataIO;

    #region ConfigColumNameText
    public string TH2Text
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
    public string TH3Text
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
    public string TH4Text
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
    public string TH5Text
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
    public string TH6Text
    {
        get
        {
            return this.TH6.Text;
        }
        set
        {
            this.TH6.Text = value;
        }
    }
    public string TH7Text
    {
        get
        {
            return this.TH7.Text;
        }
        set
        {
            this.TH7.Text = value;
        }
    }
    public string TH8Text
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
    public string TH9Text
    {
        get
        {
            return this.TH9.Text;
        }
        set
        {
            this.TH9.Text = value;
        }
    }
    public string TH10Text
    {
        get
        {
            return this.TH10.Text;
        }
        set
        {
            this.TH10.Text = value;
        }
    }
    #endregion
    #region ConfigColumValueText
    public string Colum2TextConfig
    {
        get
        {
            return this.Colum2Text.Text;
        }
        set
        {
            this.Colum2Text.Text = value;
        }
    }
    public string Colum3TextConfig
    {
        get
        {
            return this.Colum3Text.Text;
        }
        set
        {
            this.Colum3Text.Text = value;
        }
    }
    public string Colum4TextConfig
    {
        get
        {
            return this.Colum4Text.Text;
        }
        set
        {
            this.Colum4Text.Text = value;
        }
    }
    public string Colum5TextConfig
    {
        get
        {
            return this.Colum5Text.Text;
        }
        set
        {
            this.Colum5Text.Text = value;
        }
    }
    public string Colum6TextConfig
    {
        get
        {
            return this.Colum6Text.Text;
        }
        set
        {
            this.Colum6Text.Text = value;
        }
    }
    public string Colum7TextConfig
    {
        get
        {
            return this.Colum7Text.Text;
        }
        set
        {
            this.Colum7Text.Text = value;
        }
    }
    public string Colum8TextConfig
    {
        get
        {
            return this.Colum8Text.Text;
        }
        set
        {
            this.Colum8Text.Text = value;
        }
    }
    public string Colum9TextConfig
    {
        get
        {
            return this.Colum9Text.Text;
        }
        set
        {
            this.Colum9Text.Text = value;
        }
    }
    public string Colum10TextConfig
    {
        get
        {
            return this.Colum10Text.Text;
        }
        set
        {
            this.Colum10Text.Text = value;
        }
    }

    #endregion
    #region ConfigEnable
    public bool EnableColum2Text
    {
        set
        {
            ddlItem.Enabled = value;
            Colum2Text.Enabled = value;
        }
        get
        {
            return Colum2Text.Enabled;
        }
    }
    public bool EnableColum4Text
    {
        set
        {
            Colum4Text.Enabled = value;
        }
        get
        {
            return Colum4Text.Enabled;
        }
    }
    public bool EnableColum5Text
    {
        set
        {
            Colum5Text.Enabled = value;
        }
        get
        {
            return Colum5Text.Enabled;
        }
    }
    public bool EnableColum6Text
    {
        set
        {
            Colum6Text.Enabled = value;
        }
        get
        {
            return Colum6Text.Enabled;
        }
    }
    public bool EnableColum7Text
    {
        set
        {
            Colum7Text.Enabled = value;
        }
        get
        {
            return Colum7Text.Enabled;
        }
    }
    public bool EnableColum8Text
    {
        set
        {
            Colum8Text.Enabled = value;
        }
        get
        {
            return Colum8Text.Enabled;
        }
    }
    public bool EnableColum9Text
    {
        set
        {
            Colum9Text.Enabled = value;
        }
        get
        {
            return Colum9Text.Enabled;
        }
    }
    public bool EnableColum10Text
    {
        set
        {
            Colum10Text.Enabled = value;
        }
        get
        {
            return Colum10Text.Enabled;
        }
    }
    public bool EnableColum3Text
    {
        set
        {
            Colum3Text.Enabled = value;
        }
        get
        {
            return Colum3Text.Enabled;
        }
    }

    #endregion
    public int Colum8TextSelected
    {
        get
        {
            return Colum8Text.SelectedIndex;
        }
        set
        {
            Colum8Text.SelectedIndex = value;
        }
    }
    public int Colum5TextSelected
    {
        get
        {
            return Colum5Text.SelectedIndex;
        }
        set
        {
            Colum5Text.SelectedIndex = value;
        }
    }
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
                string ItemName = Colum2Text.Text;
                Colum2Text.ToolTip = ItemName;

                DataTable dtItemName = new DataTable();
                if (pDataIO.OpenDatabase(true))
                {
                    string sql = "select distinct GlobalManufactureChipsetControl.ItemName from GlobalManufactureChipsetControl,GlobalProductionName where GlobalProductionName.ID=GlobalManufactureChipsetControl.PID and GlobalProductionName.IgnoreFlag=0";
                    dtItemName = pDataIO.GetDataTable(sql, "ItemName");
                }

                ddlItem.Items.Clear();
                ddlItem.Items.Add(" ");

                for (int i = 0; i < dtItemName.Rows.Count; i++)
                {
                    ddlItem.Items.Add(dtItemName.Rows[i]["ItemName"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        } 
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        Colum2Text.Text = ddlItem.SelectedItem.Text;
        Colum2Text.ToolTip = ddlItem.SelectedItem.Text;
    }

}