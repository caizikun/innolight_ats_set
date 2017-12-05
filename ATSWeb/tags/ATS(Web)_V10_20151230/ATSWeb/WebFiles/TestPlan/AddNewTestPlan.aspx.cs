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
using System.Collections;
public partial class ASPXAddNewTestPlan : BasePage
{
    string funcItemName = "PlanSelfInfo";
   // string funcItemName = "TopoTestPlanList";
    
    ASCXTestPlanInfor testTestplanSelfInfor;
    ASCXOptionButtons UserOptionButton;
    ValidateExpression expression = new ValidateExpression();
    string[] expressionList;
   private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   public DataTable mydtFather = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private string logTracingString = "";
   private bool testPlanAddFunctionAuthority = false;
   public ASPXAddNewTestPlan()
    {        
        rowCount = 0;
        conn = "inpcsz0518\\ATS_HOME";
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        mydt.Clear();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    { 
        //if (!IsPostBack)
        {
            IsSessionNull();
            SetSessionBlockType(1);
            moduleTypeID = Request["uId"];
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
            int myAccessCode =0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();
            testPlanAddFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        }


    }

    public void bindData()
    {

        testTestplanSelfInfor = new ASCXTestPlanInfor();
       
            testTestplanSelfInfor = (ASCXTestPlanInfor)Page.LoadControl("../../Frame/TestPlan/TestPlanInfor.ascx");
            testTestplanSelfInfor.TBItemNameText = "";
            testTestplanSelfInfor.TBSWVersionText = "";
            testTestplanSelfInfor.TBHwVersionText ="";
            testTestplanSelfInfor.TBUSBPortText = "";            
            testTestplanSelfInfor.TBDescriptionText = "";
            testTestplanSelfInfor.TH2Text = mydt.Columns[2].ColumnName;
            testTestplanSelfInfor.TH3Text = mydt.Columns[3].ColumnName;
            testTestplanSelfInfor.TH4Text = mydt.Columns[4].ColumnName;
            testTestplanSelfInfor.TH5Text = mydt.Columns[5].ColumnName;
            testTestplanSelfInfor.TH6Text = mydt.Columns[6].ColumnName;
            testTestplanSelfInfor.TH7Text = mydt.Columns[7].ColumnName;
            testTestplanSelfInfor.TH8Text = mydt.Columns[8].ColumnName;
            testTestplanSelfInfor.TH9Text = mydt.Columns[9].ColumnName;
            testTestplanSelfInfor.TH10Text = mydt.Columns[10].ColumnName;
            testTestplanSelfInfor.TH11Text = mydt.Columns[11].ColumnName;
            testTestplanSelfInfor.TH12Text = mydt.Columns[12].ColumnName;
            testTestplanSelfInfor.EnableDDIgnoreBackupCoef = true;
            testTestplanSelfInfor.EnableDDIsEEPROMIni = true;
            testTestplanSelfInfor.EnableDDIsChipIni = true;
            testTestplanSelfInfor.EnableTBUSBPort = true;
            testTestplanSelfInfor.EnableTBHwVersion = true;
            testTestplanSelfInfor.EnableTBSWVersion = true;
            testTestplanSelfInfor.EnableTBItemName = true;
            testTestplanSelfInfor.EnableDDIgnoreFlag = true;
            testTestplanSelfInfor.EnableDDSNCheck = true;
            testTestplanSelfInfor.EnableTBDescription = true;
            testTestplanSelfInfor.EnableTextVersion = false;
             testTestplanSelfInfor.DDIsChipIniSelectedIndex = -1; 
            testTestplanSelfInfor.DDIgnoreFlagSelectedIndex = -1;
            testTestplanSelfInfor.DDIsEEPROMIniSelectedIndex = -1;
            testTestplanSelfInfor.DDIgnoreBackupCoefSelectedIndex = -1;
            testTestplanSelfInfor.DDCheckSNCoefSelectedIndex = -1;
            this.AddNewTestPlan.Controls.Add(testTestplanSelfInfor);
            ConfigExpressionList();
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + moduleTypeID, "TopoTestPlan");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = "AddNewItem";
                //string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from GlobalProductionName where id = " + moduleTypeID).ToString();
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
    protected void LoadOptionButton()
    {
        //UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool EditData(object obj, string prameter)
    {
        try
        {
           
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool SaveData(object obj, string prameter)
    {
        string updataStr = "select * from TopoTestPlan where ID=" + moduleTypeID;

        DataTable inserTable = pDataIO.GetDataTable(updataStr, "TopoTestPlan");
        DataRow dr = inserTable.NewRow();
        try
        {
            dr[0] = -1;
            dr[1] = moduleTypeID;
            dr[2] = testTestplanSelfInfor.TBItemNameText;
            dr[3] = testTestplanSelfInfor.TBSWVersionText;
            dr[4] = testTestplanSelfInfor.TBHwVersionText;
            dr[5] = testTestplanSelfInfor.TBUSBPortText;
            dr[6] = testTestplanSelfInfor.DDIsChipIniSelectedIndex;
            dr[7] = testTestplanSelfInfor.DDIsEEPROMIniSelectedIndex;
            dr[8] = testTestplanSelfInfor.DDIgnoreBackupCoefSelectedIndex;
            dr[9] = testTestplanSelfInfor.DDCheckSNCoefSelectedIndex;
            dr[10] = testTestplanSelfInfor.DDIgnoreFlagSelectedIndex;
            dr[11] = testTestplanSelfInfor.TBDescriptionText;
            dr[12] = 0;
            inserTable.Rows.Add(dr);
           int result= pDataIO.UpdateWithProc("TopoTestPlan", inserTable, updataStr, logTracingString);
            if (result > 0)
            {
                inserTable.AcceptChanges();
                if (testPlanAddFunctionAuthority==false)
                {
                    string userPlanStr = "select * from UserPlanAction where ID='-1'";
                    DataTable userPlanTable = pDataIO.GetDataTable(userPlanStr, "UserPlanAction");
                    DataRow userDR = userPlanTable.NewRow();
                    userDR[0] = -1;
                    userDR[1] =Session["UserID"].ToString().Trim();
                    userDR[2] = result;
                    userDR[3] = true;
                    userDR[4] = true;
                    userDR[5] = true;
                    userPlanTable.Rows.Add(userDR);
                    int resltUserPlan = pDataIO.UpdateWithProc("UserPlanAction", userPlanTable,userPlanStr, logTracingString);
                    if (resltUserPlan<0)
                   {                  
                      this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('Update data fail！\\nPlease input another ItemName.');", true);
                      
                   }
                    else
                    {
                        Response.Redirect("~/WebFiles/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
                    }

                }
                else
                {
                    Response.Redirect("~/WebFiles/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
                }
                
            }
            else
            {
               // string msg = "Update data fail!";
               ////string Url="TestPlanList.aspx?uId=" + moduleTypeID.Trim();
               //pDataIO.AlertMsgShow(msg, Request.Url.ToString());

               this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('Update data fail！\\nPlease input another ItemName.');", true);
               testTestplanSelfInfor.TBItemNameText = "";
            }
            
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;

        }
    }
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }


    }
    public void ConfigOptionButtonsVisible()
    {
        OptionButtons1.ConfigBtSaveVisible = true;
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = true;
    }
    public bool ReadExpression(string key)
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydtFather = pDataIO.GetDataTable("select * from TopoTestPlan where PID=" + moduleTypeID, "TopoTestPlan");
            }

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;

    }
    public void ConfigExpressionList()
    {
        string tempExpression = "";
        
        {
            ReadExpression(moduleTypeID);
            expressionList = new string[mydtFather.Rows.Count];
            for (byte i = 0; i < mydtFather.Rows.Count; i++)
            {
                expressionList[i] = mydtFather.Rows[i]["ItemName"].ToString().Trim();
            }
            
            {
                tempExpression = expression.GSPre;
                for (int j = 0; j < expressionList.Length; j++)
                {
                    if (j < expressionList.Length - 1)
                    {
                        tempExpression += expressionList[j] + expression.GSMid;
                    }
                    else
                    {
                        tempExpression += expressionList[j];
                    }

                }
                tempExpression += expression.GSEnd;
                testTestplanSelfInfor.configExpression = tempExpression;
                tempExpression = "";
            }
        }
    }
}