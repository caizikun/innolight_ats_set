using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Configuration;
using System.Data;

public partial class ASCXCopyTestPlan : System.Web.UI.UserControl
{
    DataIO pDataIO;
    DataTable dt;
    private bool testPlanAddFunctionAuthority = false;

    protected void Page_Load(object sender, EventArgs e)
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

        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);

        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();
        testPlanAddFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        string userID = Session["UserID"].ToString().Trim();
        try
        {
            if (!IsPostBack)
            {
                if (pDataIO.OpenDatabase(true))
                {
                    if (testPlanAddFunctionAuthority)
                    {
                       dt = pDataIO.GetDataTable("select ItemName from GlobalProductionType where IgnoreFlag=0", "");
                    } 
                    else
                    {
                        dt = pDataIO.GetDataTable("select * from GlobalProductionType where ID  IN (select PID from GlobalProductionName where ID IN (select PNID from UserPNAction where (AddPlan='True'or AddPlan='1') and UserID=" + userID + ")" + ")", "GlobalProductionType");
                    }
                  
                    
                    
                }

                DropDownList1.Items.Clear();

                DropDownList1.Items.Add(" ");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(dt.Rows[i]["ItemName"].ToString());
                }            
            }          
        }
        catch (Exception ex)
        {
            throw ex;
        }       
    }

    #region GSFocus

    #endregion
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
    
    #endregion
    #region ConfigText
    public string TBItemNameText
    {
        get
        {
            return TBItemName.Text;
        }
        set
        {
            TBItemName.Text = value;
        }

    }
    
    #endregion
    
    #region  ConfigEnable
    public bool EnableTBItemName
    {
        get
        {
            return TBItemName.Enabled;
        }
        set
        {
            TBItemName.Enabled = value;
        }
    }

    
    #endregion

    public bool CheckBox1CheckedFlag
    {
        get
        {
            return this.CheckBox1.Checked;
        }
        set
        {
            this.CheckBox1.Checked = value;
        }
    }

    public bool CheckBox1Visible
    {
        get
        {
            return this.CheckBox1.Visible;
        }
        set
        {
            this.CheckBox1.Visible = value;
        }
    }

    public string DropDownList1Text
    {
        get
        {
            return this.DropDownList1.SelectedItem.Text;
        }
        set
        {
            this.DropDownList1.SelectedItem.Text = value;
        }
    }

    public string DropDownList2Text
    {
        get
        {
            return this.DropDownList2.SelectedItem.Text ;
        }
        set
        {
            this.DropDownList2.SelectedItem.Text = value;
        }
    }

    public void InsertDropDownList2Text(int i, ListItem li)
    {
        if (!this.DropDownList2.Items.Contains(li))
        {
            DropDownList2.Items.Insert(i,li);
        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Panel1.Visible = true;
            DropDownList1.SelectedIndex = 0;            
        }
        else
        {
            Panel1.Visible = false;          
        }
        DropDownList2.Items.Clear();
    }

    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        string sql;
        string userID = Session["UserID"].ToString().Trim();
        DataTable pnTypeTable =new DataTable();
         if (testPlanAddFunctionAuthority)
        {
            sql = "select GlobalProductionName.PN from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID = GlobalProductionType.ID and GlobalProductionType.ItemName='" + DropDownList1.SelectedItem.Text + "' and GlobalProductionName.IgnoreFlag=0";
        } 
        else
        {
            string pnTypeName = DropDownList1.SelectedItem.Text.Trim();
            if (pDataIO.OpenDatabase(true))
            {
                pnTypeTable = pDataIO.GetDataTable("select * from GlobalProductionType where ItemName=" + "'" + pnTypeName + "'", "GlobalProductionType");
            }
            string pnTypeID = pnTypeTable.Rows[0]["ID"].ToString().Trim();

            sql = "select * from GlobalProductionName where ID in(select PNID from UserPNAction where AddPlan='True' and UserID=" + userID + ") and IgnoreFlag='False'and PID=" + pnTypeID; 
        }
        
    
        if (pDataIO.OpenDatabase(true))
        {
            dt = pDataIO.GetDataTable(sql, "GlobalProductionName");
        }

        DropDownList2.Items.Clear();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DropDownList2.Items.Add(dt.Rows[i]["PN"].ToString());
        }                 
    }
   
}