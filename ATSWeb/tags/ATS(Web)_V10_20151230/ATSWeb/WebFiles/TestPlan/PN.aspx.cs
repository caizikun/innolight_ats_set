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
public partial class AXPXTestPlanPN : BasePage
{
    private string funcItemName = "PNList";
    private ASCXPNType[] testModuleType;
    private int rowCount;
    private string conn;
    private DataIO pDataIO;
    public DataTable mydt = new DataTable();
    private string moduleTypeID = "";
    private string logTracingString = "";
    public DataTable mydtCoefs = new DataTable();
    private SortedList<int, string> MCoefsIDMap = new SortedList<int, string>();
    public AXPXTestPlanPN()
    {
        conn = "inpcsz0518\\ATS_HOME";
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        rowCount = 0;
        mydt.Clear();
        MCoefsIDMap.Clear();
    }
    protected override void OnInit(EventArgs e)
    {
        

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();
            SetSessionBlockType(1);
            moduleTypeID = Request["uId"];
            connectDataBase();
        }
       
    }

    public void bindData()
    {
        
        if (rowCount==0)
       {
           testModuleType = new ASCXPNType[1];
      
        for (byte i = 0; i < testModuleType.Length; i++)
        {
            testModuleType[i] = (ASCXPNType)Page.LoadControl("../../Frame/TestPlan/PNType.ascx");            
           
           
            testModuleType[i].configTH2 = mydt.Columns[2].ColumnName;
            testModuleType[i].configTH3 = mydt.Columns[4].ColumnName;
            testModuleType[i].configTH4 = mydt.Columns[5].ColumnName;
            testModuleType[i].configTH5 = mydt.Columns[6].ColumnName;
            testModuleType[i].configTH6 = "MCoefsName";
            testModuleType[i].configTH7 = "Description";
            testModuleType[i].ContentTRVisible = false;
            this.PNtype.Controls.Add(testModuleType[i]);
            
        }

       } 
       else
       {
           testModuleType = new ASCXPNType[rowCount];
      
        for (byte i = 0; i < testModuleType.Length; i++)
        {
            testModuleType[i] = (ASCXPNType)Page.LoadControl("../../Frame/TestPlan/PNType.ascx");            
            testModuleType[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            testModuleType[i].LinkText1 = mydt.Rows[i]["PN"].ToString().Trim();
            testModuleType[i].pIDString = mydt.Rows[i]["ID"].ToString().Trim();
            testModuleType[i].configTHText3 = mydt.Rows[i]["Channels"].ToString().Trim();
            testModuleType[i].configTHText4 = mydt.Rows[i]["Voltages"].ToString().Trim();
            testModuleType[i].configTHText5 = mydt.Rows[i]["Tsensors"].ToString().Trim();
            ConfigMCoefs();
            testModuleType[i].configTHText6 = MCoefsIDMap[Convert.ToInt32(mydt.Rows[i]["MCoefsID"])];
            testModuleType[i].configTHText7 = mydt.Rows[i]["ItemName"].ToString().Trim();          

            testModuleType[i].configTH2 = mydt.Columns[2].ColumnName;
            testModuleType[i].configTH3 = mydt.Columns[4].ColumnName;
            testModuleType[i].configTH4 = mydt.Columns[5].ColumnName;
            testModuleType[i].configTH5 = mydt.Columns[6].ColumnName;
            testModuleType[i].configTH6 = "MCoefsName";
            testModuleType[i].configTH7 = "Description";            
            if (i>=1)
            {
                
                testModuleType[i].EnableTH7Visible = false;
                testModuleType[i].EnableTH6Visible = false;
                testModuleType[i].EnableTH5Visible = false;
                testModuleType[i].EnableTH4Visible = false;
                testModuleType[i].EnableTH3Visible = false;
                testModuleType[i].EnableTH2Visible = false;
                testModuleType[i].LBTHTitleVisible(false);
            }
            this.PNtype.Controls.Add(testModuleType[i]);
            
        }

       }
       
    }
    public void configTHText()
    {
       
       
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalProductionName where PID=" + moduleTypeID, "GlobalProductionName");
                configTHText();
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
    public bool ConfigMCoefs()
    {
        MCoefsIDMap.Clear();
        try
        {
            for (int i = 0; i < mydtCoefs.Rows.Count; i++)
            {
                MCoefsIDMap.Add(Convert.ToInt32(mydtCoefs.Rows[i]["ID"]), mydtCoefs.Rows[i]["ItemName"].ToString());
            }
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
    
}