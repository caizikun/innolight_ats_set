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
public partial class ASPXTestplanSelfInfor : BasePage
{
    string funcItemName = "PlanSelfInfo";
    bool inEdit;
    ASCXTestPlanInfor[] testTestplanSelfInfor;
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
    public ASPXTestplanSelfInfor()
    {
        inEdit = false;
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
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }


    }

    public void bindData()
    {

        testTestplanSelfInfor = new ASCXTestPlanInfor[rowCount];
        for (byte i = 0; i < testTestplanSelfInfor.Length; i++)
        {
            testTestplanSelfInfor[i] = (ASCXTestPlanInfor)Page.LoadControl("../../Frame/TestPlan/TestPlanInfor.ascx");
            testTestplanSelfInfor[i].TBItemNameText = mydt.Rows[i]["ItemName"].ToString().Trim();
            testTestplanSelfInfor[i].TBSWVersionText = mydt.Rows[i]["SWVersion"].ToString().Trim();
            testTestplanSelfInfor[i].TBHwVersionText = mydt.Rows[i]["HwVersion"].ToString().Trim();
            testTestplanSelfInfor[i].TBUSBPortText = mydt.Rows[i]["USBPort"].ToString().Trim();
            testTestplanSelfInfor[i].TBDescriptionText = mydt.Rows[i]["ItemDescription"].ToString().Trim();
            testTestplanSelfInfor[i].TBVersionText = mydt.Rows[i]["Version"].ToString().Trim();
            testTestplanSelfInfor[i].TH2Text = mydt.Columns[2].ColumnName;
            testTestplanSelfInfor[i].TH3Text = mydt.Columns[3].ColumnName;
            testTestplanSelfInfor[i].TH4Text = mydt.Columns[4].ColumnName;
            testTestplanSelfInfor[i].TH5Text = mydt.Columns[5].ColumnName;
            testTestplanSelfInfor[i].TH6Text = mydt.Columns[6].ColumnName;
            testTestplanSelfInfor[i].TH7Text = mydt.Columns[7].ColumnName;
            testTestplanSelfInfor[i].TH8Text = mydt.Columns[8].ColumnName;
            testTestplanSelfInfor[i].TH9Text = mydt.Columns[9].ColumnName;
            testTestplanSelfInfor[i].TH10Text = mydt.Columns[10].ColumnName;
            testTestplanSelfInfor[i].TH11Text = mydt.Columns[11].ColumnName;
            testTestplanSelfInfor[i].TH12Text = mydt.Columns[12].ColumnName;
            testTestplanSelfInfor[i].EnableDDIgnoreBackupCoef = false;
            testTestplanSelfInfor[i].EnableDDIsEEPROMIni = false;
            testTestplanSelfInfor[i].EnableDDIsChipIni = false;
            testTestplanSelfInfor[i].EnableTBUSBPort = false;
            testTestplanSelfInfor[i].EnableTBHwVersion = false;
            testTestplanSelfInfor[i].EnableTBSWVersion = false;
            testTestplanSelfInfor[i].EnableTBItemName = false;
            testTestplanSelfInfor[i].EnableDDIgnoreFlag = false;
            testTestplanSelfInfor[i].EnableDDSNCheck = false;
            testTestplanSelfInfor[i].EnableTBDescription = false;
            testTestplanSelfInfor[i].EnableTextVersion = false;
            if (mydt.Rows[i]["IsChipInitialize"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDIsChipIniSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDIsChipIniSelectedIndex = 1;
            }
            if (mydt.Rows[i]["IgnoreFlag"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDIgnoreFlagSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDIgnoreFlagSelectedIndex = 1;
            }
            if (mydt.Rows[i]["IsEEPROMInitialize"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDIsEEPROMIniSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDIsEEPROMIniSelectedIndex = 1;
            }
            if (mydt.Rows[i]["IgnoreBackupCoef"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDIgnoreBackupCoefSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDIgnoreBackupCoefSelectedIndex = 1;
            }
            if (mydt.Rows[i]["SNCheck"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDCheckSNCoefSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDCheckSNCoefSelectedIndex = 1;
            }
            this.TestPlanSelfInfor.Controls.Add(testTestplanSelfInfor[i]);
        }
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
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
                this.plhNavi.Controls.Add(myCtrl);
            }

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
     
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
            for (byte i = 0; i < testTestplanSelfInfor.Length; i++)
            {
                testTestplanSelfInfor[i].EnableDDIgnoreBackupCoef = true;
                testTestplanSelfInfor[i].EnableDDIsEEPROMIni = true;
                testTestplanSelfInfor[i].EnableDDIsChipIni = true;
                testTestplanSelfInfor[i].EnableTBUSBPort = true;
                testTestplanSelfInfor[i].EnableTBHwVersion = true;
                testTestplanSelfInfor[i].EnableTBSWVersion = true;
                testTestplanSelfInfor[i].EnableTBItemName = true;
                testTestplanSelfInfor[i].EnableDDIgnoreFlag = true;
                testTestplanSelfInfor[i].EnableDDSNCheck = true;
                testTestplanSelfInfor[i].EnableTBDescription = true;
            }
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtAddVisible = false;
            OptionButtons1.ConfigBtEditVisible = false;
            OptionButtons1.ConfigBtDeleteVisible = false;
            OptionButtons1.ConfigBtCancelVisible = true;
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
       
        
    }
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
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
        try
        {
            for (byte i = 0; i < testTestplanSelfInfor.Length; i++)
            {
                mydt.Rows[i]["ItemName"] = testTestplanSelfInfor[i].TBItemNameText;
                mydt.Rows[i]["SWVersion"]=testTestplanSelfInfor[i].TBSWVersionText;
                mydt.Rows[i]["HwVersion"] = testTestplanSelfInfor[i].TBHwVersionText;
                mydt.Rows[i]["USBPort"]=testTestplanSelfInfor[i].TBUSBPortText;              
                mydt.Rows[i]["IsChipInitialize"] = testTestplanSelfInfor[i].DDIsChipIniSelectedIndex;
                mydt.Rows[i]["IgnoreFlag"] = testTestplanSelfInfor[i].DDIgnoreFlagSelectedIndex;
                mydt.Rows[i]["IsEEPROMInitialize"] = testTestplanSelfInfor[i].DDIsEEPROMIniSelectedIndex;
                mydt.Rows[i]["SNCheck"] = testTestplanSelfInfor[i].DDCheckSNCoefSelectedIndex;
                mydt.Rows[i]["IgnoreBackupCoef"] = testTestplanSelfInfor[i].DDIgnoreBackupCoefSelectedIndex;
                mydt.Rows[i]["ItemDescription"] = testTestplanSelfInfor[i].TBDescriptionText;
            }
            int result = pDataIO.UpdateWithProc("TopoTestPlan", mydt, updataStr, logTracingString);
            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("Update data fail!");
            }
            for (byte i = 0; i < testTestplanSelfInfor.Length; i++)
            {
                testTestplanSelfInfor[i].EnableDDIgnoreBackupCoef = false;
                testTestplanSelfInfor[i].EnableDDIsEEPROMIni = false;
                testTestplanSelfInfor[i].EnableDDIsChipIni = false;
                testTestplanSelfInfor[i].EnableTBUSBPort = false;
                testTestplanSelfInfor[i].EnableTBHwVersion = false;
                testTestplanSelfInfor[i].EnableTBSWVersion = false;
                testTestplanSelfInfor[i].EnableTBItemName = false;
                testTestplanSelfInfor[i].EnableDDIgnoreFlag = false;
                testTestplanSelfInfor[i].EnableDDSNCheck = false;
                testTestplanSelfInfor[i].EnableTBDescription = false;
            } 
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;

        }
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
        OptionButtons1.ConfigBtAddVisible = false;
        bool editFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);
        if (editFunctionAuthority)
        {
            OptionButtons1.ConfigBtEditVisible=true;
        } 
        else
        {
            OptionButtons1.ConfigBtEditVisible = GetTestPlanAuthority();
        }     
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    public void ConfigExpressionList()
    {
        string tempExpression="";
       
        if (rowCount == 0)
        {
            expressionList[0] = "";
        }
        else
        {
            ReadExpression(mydt.Rows[0]["PID"].ToString().Trim());
            expressionList = new string[mydtFather.Rows.Count];
            for (byte i = 0; i < mydtFather.Rows.Count; i++)
            {
                expressionList[i]  = mydtFather.Rows[i]["ItemName"].ToString().Trim();
            }
            for (byte i = 0; i < testTestplanSelfInfor.Length; i++)
            {
                tempExpression=expression.GSPre;
                for (int j=0;j<expressionList.Length;j++)
                {
                    if (j < expressionList.Length-1)
                    {
                        tempExpression += expressionList[j] + expression.GSMid;
                    } 
                    else
                    {
                        tempExpression += expressionList[j];
                    }
                 
                }
                tempExpression+=expression.GSEnd;
                testTestplanSelfInfor[i].configExpression = tempExpression;
                tempExpression = "";
            }
        }
    }
    public bool ReadExpression(string key)
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydtFather = pDataIO.GetDataTable("select * from TopoTestPlan where ID!="+moduleTypeID+ "and PID=" + key, "TopoTestPlan");
            }

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
     
    }
    public bool GetTestPlanAuthority()
    {
        string userID = Session["UserID"].ToString().Trim();
        bool tpAuthority = false;
        try
        {
           
            if (pDataIO.OpenDatabase(true))
            {
               
                {
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + moduleTypeID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                       
                        tpAuthority = false;
                    }
                    else
                    {
                        if (temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                        else
                        {
                            tpAuthority = false;
                        }
                      
                    }
                }

            }
            return tpAuthority;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}