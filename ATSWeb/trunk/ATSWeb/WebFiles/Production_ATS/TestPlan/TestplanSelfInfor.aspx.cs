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
    string funcItemName = "测试方案信息";
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

    protected override void OnInit(EventArgs e)
    {
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();

        inEdit = false;
        rowCount = 0;
        conn = "inpcsz0518\\ATS_HOME";
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
        mydt.Clear();

        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(1);
        moduleTypeID = Request["uId"];
        connectDataBase();
        LoadOptionButton();
        ConfigOptionButtonsVisible();
    }

    public void bindData()
    {

        testTestplanSelfInfor = new ASCXTestPlanInfor[rowCount];
        for (byte i = 0; i < testTestplanSelfInfor.Length; i++)
        {
            testTestplanSelfInfor[i] = (ASCXTestPlanInfor)Page.LoadControl("~/Frame/TestPlan/TestPlanInfor.ascx");
            testTestplanSelfInfor[i].TBItemNameText = mydt.Rows[i]["ItemName"].ToString().Trim();
            testTestplanSelfInfor[i].TBSWVersionText = mydt.Rows[i]["SWVersion"].ToString().Trim();
            testTestplanSelfInfor[i].TBHwVersionText = mydt.Rows[i]["HwVersion"].ToString().Trim();
            testTestplanSelfInfor[i].TBUSBPortText = mydt.Rows[i]["USBPort"].ToString().Trim();
            testTestplanSelfInfor[i].TBDescriptionText = mydt.Rows[i]["ItemDescription"].ToString().Trim();
            testTestplanSelfInfor[i].TBVersionText = mydt.Rows[i]["Version"].ToString().Trim();
            testTestplanSelfInfor[i].TH2Text = "名称";
            testTestplanSelfInfor[i].TH3Text = "软件版本号";
            testTestplanSelfInfor[i].TH4Text = "硬件版本号";
            testTestplanSelfInfor[i].TH5Text = "USB端口";
            testTestplanSelfInfor[i].TH6Text = "是否初始化芯片";
            testTestplanSelfInfor[i].TH7Text = "是否初始化EEPROM";
            testTestplanSelfInfor[i].TH8Text = "是否不备份系数";
            testTestplanSelfInfor[i].TH9Text = "是否检查SN";           
            testTestplanSelfInfor[i].TH13Text = "是否检查品名";
            testTestplanSelfInfor[i].TH14Text = "是否检查软件版本号";
            testTestplanSelfInfor[i].TH10Text = "是否跳过";
            testTestplanSelfInfor[i].TH11Text = "描述";
            testTestplanSelfInfor[i].TH12Text = "修订号";
            testTestplanSelfInfor[i].TH15Text = "是否开启CDR";
            testTestplanSelfInfor[i].EnableDDIgnoreBackupCoef = false;
            testTestplanSelfInfor[i].EnableDDIsEEPROMIni = false;
            testTestplanSelfInfor[i].EnableDDIsChipIni = false;
            testTestplanSelfInfor[i].EnableTBUSBPort = false;
            testTestplanSelfInfor[i].EnableTBHwVersion = false;
            testTestplanSelfInfor[i].EnableTBSWVersion = false;
            testTestplanSelfInfor[i].EnableTBItemName = false;
            testTestplanSelfInfor[i].EnableDDIgnoreFlag = false;
            testTestplanSelfInfor[i].EnableDDSNCheck = false;
            testTestplanSelfInfor[i].EnableDDPNCheck = false;
            testTestplanSelfInfor[i].EnableDDSWCheck = false;
            testTestplanSelfInfor[i].EnableTBDescription = false;
            testTestplanSelfInfor[i].EnableTextVersion = false;
            testTestplanSelfInfor[i].EnableDDCDROn = false;
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
            if (mydt.Rows[i]["PNCheck"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDCheckPNCoefSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDCheckPNCoefSelectedIndex = 1;
            }
            if (mydt.Rows[i]["SWCheck"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDCheckSWCoefSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDCheckSWCoefSelectedIndex = 1;
            }
            if (mydt.Rows[i]["IsCDROn"].ToString().ToUpper().Trim() == "FALSE")
            {
                testTestplanSelfInfor[i].DDCDROnSelectedIndex = 0;
            }
            else
            {
                testTestplanSelfInfor[i].DDCDROnSelectedIndex = 1;
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

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";               

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "2"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.PN from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                string uid2 = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.ID from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + uid2;

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                string uid = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + uid;

                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "3_Page"] = "";                
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();                
               
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
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
                testTestplanSelfInfor[i].EnableDDPNCheck = true;
                testTestplanSelfInfor[i].EnableDDSWCheck = true;
                testTestplanSelfInfor[i].EnableTBDescription = true;
                testTestplanSelfInfor[i].EnableDDCDROn = true;
            }
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtAddVisible = false;
            OptionButtons1.ConfigBtEditVisible = false;
            OptionButtons1.ConfigBtDeleteVisible = false;
            OptionButtons1.ConfigBtCancelVisible = true;

            tdStandard.Attributes.Add("style", "visibility:visible;");
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
            bool refreshFlag = false;  //修改PlanFlag
            bool refreshFlagDelete = false;  //删除PlanFlag(Ignore=true)
            for (byte i = 0; i < testTestplanSelfInfor.Length; i++)
            {
                if (mydt.Rows[i]["ItemName"] != testTestplanSelfInfor[i].TBItemNameText)
                {
                    refreshFlag = true;
                }
                
                mydt.Rows[i]["ItemName"] = testTestplanSelfInfor[i].TBItemNameText;
                mydt.Rows[i]["SWVersion"]=testTestplanSelfInfor[i].TBSWVersionText;
                mydt.Rows[i]["HwVersion"] = testTestplanSelfInfor[i].TBHwVersionText;
                mydt.Rows[i]["USBPort"]=testTestplanSelfInfor[i].TBUSBPortText;              
                mydt.Rows[i]["IsChipInitialize"] = testTestplanSelfInfor[i].DDIsChipIniSelectedIndex;
                mydt.Rows[i]["IgnoreFlag"] = testTestplanSelfInfor[i].DDIgnoreFlagSelectedIndex;
                mydt.Rows[i]["IsEEPROMInitialize"] = testTestplanSelfInfor[i].DDIsEEPROMIniSelectedIndex;
                mydt.Rows[i]["SNCheck"] = testTestplanSelfInfor[i].DDCheckSNCoefSelectedIndex;
                mydt.Rows[i]["PNCheck"] = testTestplanSelfInfor[i].DDCheckPNCoefSelectedIndex;
                mydt.Rows[i]["SWCheck"] = testTestplanSelfInfor[i].DDCheckSWCoefSelectedIndex;
                mydt.Rows[i]["IgnoreBackupCoef"] = testTestplanSelfInfor[i].DDIgnoreBackupCoefSelectedIndex;
                mydt.Rows[i]["ItemDescription"] = testTestplanSelfInfor[i].TBDescriptionText;
                mydt.Rows[i]["IsCDROn"] = testTestplanSelfInfor[i].DDCDROnSelectedIndex;

                if (testTestplanSelfInfor[i].DDIgnoreFlagSelectedIndex == 1)
                {
                    refreshFlagDelete = true;
                }
            }

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestPlan", mydt, updataStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestPlan", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();

                if (refreshFlagDelete || refreshFlag)
                {
                    DataTable dt = pDataIO.GetDataTable("select PID from TopoTestPlan where ID=" + moduleTypeID, "TopoTestPlan");
                    int pnID = Convert.ToInt32(dt.Rows[0]["PID"]);
                    dt = pDataIO.GetDataTable("select PID from GlobalProductionName where ID=" + pnID, "GlobalProductionName");
                    int typeID = Convert.ToInt32(dt.Rows[0]["PID"]);

                    dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + typeID, "GlobalProductionTypeNum");
                    Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                    dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.ID ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + typeID + ") as newtable where newtable.ID =" + pnID, "GlobalProductionNameNum");
                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+1";

                    if (refreshFlag)
                    {
                        dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY TopoTestPlan.ID ASC) AS num,TopoTestPlan.* from TopoTestPlan where IgnoreFlag='false' and PID =" + pnID + ") as newtable where newtable.ID =" + moduleTypeID, "TopoTestPlanNum");
                        Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();
                    }
                    
                    Session["iframe_src"] = "WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + moduleTypeID;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);
                }
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
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
                testTestplanSelfInfor[i].EnableDDPNCheck = false;
                testTestplanSelfInfor[i].EnableDDSWCheck = false;
                testTestplanSelfInfor[i].EnableTBDescription = false;
                testTestplanSelfInfor[i].EnableDDCDROn = false;
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
        bool editFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);

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
                DataTable pnId = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + moduleTypeID, "TopoTestPlan");

                if (pnId.Rows.Count == 1)
                {
                    string PNid = pnId.Rows[0]["PID"].ToString();
                    DataTable pnAuthority = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + PNid, "UserPNAction");
                    if (pnAuthority.Rows.Count == 1)
                    {
                        if (pnAuthority.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "TRUE" || pnAuthority.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                    }
                }
               
                {
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + moduleTypeID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                       
                        //tpAuthority = false;
                    }
                    else
                    {
                        if (temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                        //else
                        //{
                        //    tpAuthority = false;
                        //}
                      
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