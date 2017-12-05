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
    string funcItemName = "测试方案信息";
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
    
    protected void Page_Load(object sender, EventArgs e)
    { 
        //if (!IsPostBack)
        {
            IsSessionNull();

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
            int myAccessCode =0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();
            testPlanAddFunctionAuthority = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        }


    }

    public void bindData()
    {

        testTestplanSelfInfor = new ASCXTestPlanInfor();
       
            testTestplanSelfInfor = (ASCXTestPlanInfor)Page.LoadControl("~/Frame/TestPlan/TestPlanInfor.ascx");
            testTestplanSelfInfor.TBItemNameText = "";
            testTestplanSelfInfor.TBSWVersionText = "";
            testTestplanSelfInfor.TBHwVersionText ="";
            testTestplanSelfInfor.TBUSBPortText = "";            
            testTestplanSelfInfor.TBDescriptionText = "";
            testTestplanSelfInfor.TH2Text = "名称";
            testTestplanSelfInfor.TH3Text = "软件版本号";
            testTestplanSelfInfor.TH4Text = "硬件版本号";
            testTestplanSelfInfor.TH5Text = "USB端口";
            testTestplanSelfInfor.TH6Text = "是否初始化芯片";
            testTestplanSelfInfor.TH7Text = "是否初始化EEPROM";
            testTestplanSelfInfor.TH8Text = "是否不备份系数";
            testTestplanSelfInfor.TH9Text = "是否检查SN";
            testTestplanSelfInfor.TH13Text = "是否检查品名";
            testTestplanSelfInfor.TH14Text = "是否检查软件版本号";
            testTestplanSelfInfor.TH10Text = "是否跳过";
            testTestplanSelfInfor.TH11Text = "描述";
            testTestplanSelfInfor.TH12Text = "修订号";
            testTestplanSelfInfor.TH15Text = "是否开启CDR";
            testTestplanSelfInfor.EnableDDIgnoreBackupCoef = true;
            testTestplanSelfInfor.EnableDDIsEEPROMIni = true;
            testTestplanSelfInfor.EnableDDIsChipIni = true;
            testTestplanSelfInfor.EnableTBUSBPort = true;
            testTestplanSelfInfor.EnableTBHwVersion = true;
            testTestplanSelfInfor.EnableTBSWVersion = true;
            testTestplanSelfInfor.EnableTBItemName = true;
            testTestplanSelfInfor.EnableDDIgnoreFlag = false;
            testTestplanSelfInfor.EnableDDSNCheck = true;
            testTestplanSelfInfor.EnableDDPNCheck = true;
            testTestplanSelfInfor.EnableDDSWCheck = true;
            testTestplanSelfInfor.EnableTBDescription = true;
            testTestplanSelfInfor.EnableTextVersion = false;
            testTestplanSelfInfor.EnableDDCDROn = true;
             testTestplanSelfInfor.DDIsChipIniSelectedIndex = -1; 
            testTestplanSelfInfor.DDIgnoreFlagSelectedIndex = 0;
            testTestplanSelfInfor.DDIsEEPROMIniSelectedIndex = -1;
            testTestplanSelfInfor.DDIgnoreBackupCoefSelectedIndex = -1;
            testTestplanSelfInfor.DDCheckSNCoefSelectedIndex = -1;
            testTestplanSelfInfor.DDCDROnSelectedIndex = 1;
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
                string parentItem = "添加新项";
                //string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from GlobalProductionName where id = " + moduleTypeID).ToString();

                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
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
            dr[10] = testTestplanSelfInfor.DDCheckSWCoefSelectedIndex;
            dr[11] = testTestplanSelfInfor.DDCheckPNCoefSelectedIndex;
            dr[12] = testTestplanSelfInfor.DDIgnoreFlagSelectedIndex;
            dr[13] = testTestplanSelfInfor.TBDescriptionText;
            dr[14] = 0;
            dr[15] = testTestplanSelfInfor.DDCDROnSelectedIndex;

            inserTable.Rows.Add(dr);

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestPlan", inserTable, updataStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestPlan", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
            }      

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

                    int resltUserPlan = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        resltUserPlan = pDataIO.UpdateWithProc("UserPlanAction", userPlanTable, userPlanStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        resltUserPlan = pDataIO.UpdateWithProc("UserPlanAction", userPlanTable, userPlanStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (resltUserPlan<0)
                   {
                       this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('数据更新失败！\\n请输入其他名称.');", true);
                      
                   }
                    else
                    {
                        //Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
                        DataTable dt = pDataIO.GetDataTable("select PID from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                        int typeID = Convert.ToInt32(dt.Rows[0]["PID"]);
                        dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + typeID, "GlobalProductionTypeNum");
                        Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                        dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.ID ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + typeID + ") as newtable where newtable.ID =" + moduleTypeID, "GlobalProductionNameNum");
                        Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                        Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+1";

                        dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY TopoTestPlan.ID ASC) AS num,TopoTestPlan.* from TopoTestPlan where IgnoreFlag='false' and PID =" + moduleTypeID + ") as newtable where newtable.ItemName ='" + testTestplanSelfInfor.TBItemNameText + "'", "TopoTestPlanNum");
                        Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                        Session["iframe_src"] = "WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + Convert.ToInt32(dt.Rows[0]["ID"]);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);
                    }

                }
                else
                {
                    DataTable dt = pDataIO.GetDataTable("select PID from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                    int typeID = Convert.ToInt32(dt.Rows[0]["PID"]);
                    dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + typeID, "GlobalProductionTypeNum");
                    Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                    dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.ID ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + typeID + ") as newtable where newtable.ID =" + moduleTypeID, "GlobalProductionNameNum");
                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+1";

                    dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY TopoTestPlan.ID ASC) AS num,TopoTestPlan.* from TopoTestPlan where IgnoreFlag='false' and PID =" + moduleTypeID + ") as newtable where newtable.ItemName ='" + testTestplanSelfInfor.TBItemNameText + "'", "TopoTestPlanNum");
                    Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                    Session["iframe_src"] = "WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + Convert.ToInt32(dt.Rows[0]["ID"]);

                    //Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);
                }
                
            }
            else
            {
               // string msg = "数据更新失败!";
               ////string Url="TestPlanList.aspx?uId=" + moduleTypeID.Trim();
               //pDataIO.AlertMsgShow(msg, Request.Url.ToString());

               this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('数据更新失败！\\n请输入其他名称.');", true);
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
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim());
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