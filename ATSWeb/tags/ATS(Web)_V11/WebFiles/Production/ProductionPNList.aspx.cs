using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ATSDataBase;
using System.Threading;
public partial class ASPXProductionPNList : BasePage
{
    string funcItemName = "ProductionPNList";
    public DataTable mydt = new DataTable();
    ASCXProducPNList[]prductionPNList;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    public DataTable mydtCoefs = new DataTable();
    private SortedList<int, string> MCoefsIDMap = new SortedList<int, string>();
    public ASPXProductionPNList()
    {
        conn = "inpcsz0518\\ATS_HOME";
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        mydt.Clear();
        MCoefsIDMap.Clear();
    }
    
    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {       
        
        {
            IsSessionNull();
            SetSessionBlockType(2);
            moduleTypeID = Request["uId"];

            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }
        
    }

    public void bindData()
    {
       
        ClearCurrenPage();
        if (rowCount==0)
        {
              prductionPNList = new ASCXProducPNList[1];
        
        for (byte i = 0; i < prductionPNList.Length; i++)
        {
            prductionPNList[i] = (ASCXProducPNList)Page.LoadControl("../../Frame/Production/ProducPNList.ascx");
           
            prductionPNList[i].LbTH2= mydt.Columns[2].ColumnName;
            prductionPNList[i].LbTH3= "Description";
            prductionPNList[i].LbTH4 = mydt.Columns[4].ColumnName;
            prductionPNList[i].LbTH5= mydt.Columns[5].ColumnName;
            prductionPNList[i].LbTH6 = mydt.Columns[6].ColumnName;
            prductionPNList[i].LbTH7 = "MCoefsName";
            prductionPNList[i].ContentTRVisible = false;
            this.PRPNList.Controls.Add(prductionPNList[i]);
        }
        } 
        else
        {
              prductionPNList = new ASCXProducPNList[rowCount];
        
        for (byte i = 0; i < prductionPNList.Length; i++)
        {
            prductionPNList[i] = (ASCXProducPNList)Page.LoadControl("../../Frame/Production/ProducPNList.ascx");
            prductionPNList[i].ID =  mydt.Rows[i]["ID"].ToString().Trim();
            prductionPNList[i].LbTH2Text = mydt.Rows[i]["PN"].ToString().Trim();
            prductionPNList[i].LbTH3Text = mydt.Rows[i]["ItemName"].ToString().Trim();
            prductionPNList[i].LbTH4Text = mydt.Rows[i]["Channels"].ToString().Trim();
            prductionPNList[i].LbTH5Text = mydt.Rows[i]["Voltages"].ToString().Trim();
            prductionPNList[i].LbTH6Text = mydt.Rows[i]["Tsensors"].ToString().Trim();
            ConfigMCoefs();            
            prductionPNList[i].LbTH7Text = MCoefsIDMap[Convert.ToInt32(mydt.Rows[i]["MCoefsID"])];
            prductionPNList[i].LbTH2= mydt.Columns[2].ColumnName;
            prductionPNList[i].LbTH3 = "Description";
            prductionPNList[i].LbTH4 = mydt.Columns[4].ColumnName;
            prductionPNList[i].LbTH5= mydt.Columns[5].ColumnName;
            prductionPNList[i].LbTH6 = mydt.Columns[6].ColumnName;
            prductionPNList[i].LbTH7 = "MCoefsName";
            prductionPNList[i].PostBackUrlStringPNSelf = "~/WebFiles/Production/PNSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            prductionPNList[i].PostBackUrlStringChipSetControl = "~/WebFiles/Production/ChipSetControlList.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            prductionPNList[i].PostBackUrlStringChipSetINI = "~/WebFiles/Production/ChipSetIniList.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            prductionPNList[i].PostBackUrlStringE2PROM = "~/WebFiles/Production/TopEEROMList.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            prductionPNList[i].PostBackUrlStringLinkButtonChip = "~/WebFiles/Production/PNChipList.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            prductionPNList[i].BeSelected = false;
            if (i >= 1)
            {
                prductionPNList[i].LBTH2Visible = false;
                prductionPNList[i].LBTH3Visible = false;
                prductionPNList[i].LBTH4Visible = false;
                prductionPNList[i].LBTH5Visible = false;
                prductionPNList[i].LBTH6Visible = false;
                prductionPNList[i].LBTH7Visible = false;               
                prductionPNList[i].LBTH8Visible = false; 
                prductionPNList[i].LBTH9Visible = false;
                prductionPNList[i].LBTH10Visible = false;
                prductionPNList[i].LBTH11Visible = false;
                prductionPNList[i].LBTHTitleVisible(false);
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.PRPNList.Controls.Add(prductionPNList[i]);
        }
        }
      

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalProductionName where IgnoreFlag='False'and PID=" + moduleTypeID, "GlobalProductionName");
                rowCount = mydt.Rows.Count;
                mydtCoefs = pDataIO.GetDataTable("select * from GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficientsGroup");
                bindData();
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from GlobalProductionType where id = " + moduleTypeID).ToString();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
                this.plhNavi.Controls.Add(myCtrl);
            }
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
        
    }

   
   
    public bool DeleteData(object obj, string prameter)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('Are you sure you want to delete？')){DeleteData1();}else{}</script>");
        bool isSelected = false;
        string deletStr = "select * from GlobalProductionName IgnoreFlag='False'and where PID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < prductionPNList.Length; i++)
            {
                ASCXProducPNList cb = (ASCXProducPNList)PRPNList.FindControl(prductionPNList[i].ID);
                if (cb != null )
                {
                    if (cb.BeSelected == true)
                    {
                        
                        mydt.Rows[i]["IgnoreFlag"] = true;
                        isSelected = true;
                    }                    
                }
                else
                {
                    Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false)
            {               
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('Did not choose any one！');return false;</script>");
                this.Page.RegisterStartupScript("", "<script>alert('Did not choose any one！');</script>");
                return false;
            }
            int result = pDataIO.UpdateWithProc("GlobalProductionName", mydt, deletStr, logTracingString);
            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("Update data fail!");
            }           
            Response.Redirect(Request.Url.ToString());
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;           
        }
        
    }
    
    protected void LoadOptionButton()
    {
        //ASCXOptionButtons UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool SaveData(object obj, string prameter)
    {
        return true;
    }
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/Production/PNSelfInfor.aspx?AddNew=true&uId=" +moduleTypeID);

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    public void ConfigOptionButtonsVisible()
    {      
        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.AddProduction, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
        if (rowCount<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible=false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.DeleteProduction, myAccessCode);
        }
        
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (prductionPNList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < prductionPNList.Length; i++)
        {
            prductionPNList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (prductionPNList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < prductionPNList.Length; i++)
        {
            prductionPNList[i].BeSelected = false;
        }
    }
    public void ClearCurrenPage()
    {
        if (rowCount == 0)
        {
            SelectAll.Visible = false;
            DeSelectAll.Visible = false;
        }
    }
    public bool ConfigMCoefs()
    {
        MCoefsIDMap.Clear();
        try
        {
            for (int i = 0; i < mydtCoefs.Rows.Count; i++)
            { 
                MCoefsIDMap.Add(Convert.ToInt32(mydtCoefs.Rows[i]["ID"]),mydtCoefs.Rows[i]["ItemName"].ToString());
            }           
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}