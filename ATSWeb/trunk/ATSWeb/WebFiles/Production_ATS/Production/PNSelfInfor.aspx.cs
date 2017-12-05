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
using System.Reflection;
using System.Web.Compilation;

public partial class ASPXPNSelfInfor : BasePage
{
    string funcItemName = "产品信息";
    ASCXOptionButtons UserOptionButton;
    ASCXPNSelfInfor[] pnSelfInforList;
    ASCXPNSelfInfor pnSelf;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   public DataTable mydtCoefs = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private bool AddNew;
    private string logTracingString = "";
    private SortedList<int, int> MCoefsIDMap = new SortedList<int, int>();
    private SortedList<int, int> APCStyleIDMap = new SortedList<int, int>();
    private SortedList<int, int> APCTypeIDMap = new SortedList<int, int>();
    private SortedList<int, int> TecPresentIDMap = new SortedList<int, int>();
    private SortedList<int, int> CoupleTypeIDMap = new SortedList<int, int>();
    private SortedList<int, int> BERIDMap = new SortedList<int, int>();
    private SortedList<int, int> MaxRateIDMap = new SortedList<int, int>();
    private SortedList<int, int> DriverIDMap = new SortedList<int, int>();
    ValidateExpression expression = new ValidateExpression();
    string[] expressionList;
    public DataTable mydtFather = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();

            AddNew = false;
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
            string teme = Convert.ToString(sender) +"e="+ e.ToString();
            
            moduleTypeID = Request["uId"];
            if (Request.QueryString["AddNew"] != null)
            {
                AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            }
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();

        }
    }

    public void bindData()
    {
        pnSelfInforList = new ASCXPNSelfInfor[rowCount];
        if (rowCount==0&&AddNew==false)
        {
            return;
        }
        if (AddNew==true)
        {
            ClearTextValue();
        }
        else
        {
            for (byte i = 0; i < pnSelfInforList.Length; i++)
            {
                pnSelfInforList[i] = (ASCXPNSelfInfor)Page.LoadControl("~/Frame/Production/PNSelfInfor.ascx");
                pnSelfInforList[i].ID = "a" + mydt.Rows[i]["ID"].ToString().Trim();
                pnSelfInforList[i].TH2Text = "品名";
                pnSelfInforList[i].Colum2TextConfig = mydt.Rows[i]["PN"].ToString().Trim();
                pnSelfInforList[i].TH3Text = "描述";
                pnSelfInforList[i].Colum3TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();

                pnSelfInforList[i].TH4Text = "通道数";
                pnSelfInforList[i].Colum4TextConfig = mydt.Rows[i]["Channels"].ToString().Trim();

                pnSelfInforList[i].TH5Text = "电压数";
                pnSelfInforList[i].Colum5TextConfig = mydt.Rows[i]["Voltages"].ToString().Trim();

                pnSelfInforList[i].TH6Text = "传感器类型";
                pnSelfInforList[i].Colum6TextConfig = mydt.Rows[i]["Tsensors"].ToString().Trim();

                pnSelfInforList[i].ClearDropDownList();
                ConfigMCoefsandPN(pnSelfInforList[i]);               
                pnSelfInforList[i].TH7Text = "系数组";
                pnSelfInforList[i].ConfigSeletedIndexDD7 = MCoefsIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["MCoefsID"]));
                
                pnSelfInforList[i].TH8Text = mydt.Columns[9].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD8 = DriverIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["OldDriver"]));
                
             
                pnSelfInforList[i].TH10Text = "是否存在TEC";
                pnSelfInforList[i].ConfigSeletedIndexDD10 = TecPresentIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["TEC_Present"]));
                pnSelfInforList[i].TH11Text = "耦合类型";
                pnSelfInforList[i].ConfigSeletedIndexDD11 = CoupleTypeIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["Couple_Type"]));
                pnSelfInforList[i].TH12Text = "APC类型";
                pnSelfInforList[i].ConfigSeletedIndexDD12 = APCTypeIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["APC_Type"]));
                pnSelfInforList[i].TH13Text = "误码率（BER）";
                pnSelfInforList[i].ConfigSeletedIndexDD13 = BERIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["BER"]));
                pnSelfInforList[i].TH14Text = "最大速率";
                pnSelfInforList[i].ConfigSeletedIndexDD14 = MaxRateIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["MaxRate"]));

                pnSelfInforList[i].TH15Text = "正式品名";
                pnSelfInforList[i].Colum15TextConfig = mydt.Rows[i]["Publish_PN"].ToString().Trim();
                pnSelfInforList[i].TH16Text = "别名";
                pnSelfInforList[i].Colum16TextConfig = mydt.Rows[i]["NickName"].ToString().Trim();
                pnSelfInforList[i].TH17Text = "Ibias公式";
                pnSelfInforList[i].Colum17TextConfig = mydt.Rows[i]["IbiasFormula"].ToString().Trim();
                pnSelfInforList[i].Colum17TextToolTip = mydt.Rows[i]["IbiasFormula"].ToString().Trim();
                pnSelfInforList[i].TH18Text = "IMod公式";
                pnSelfInforList[i].Colum18TextConfig = mydt.Rows[i]["IModFormula"].ToString().Trim();
                pnSelfInforList[i].Colum18TextToolTip = mydt.Rows[i]["IModFormula"].ToString().Trim();
                pnSelfInforList[i].TH19Text = "是否用摄氏温度";
                if (mydt.Rows[i]["UsingCelsiusTemp"].ToString().ToUpper().Trim() == "FALSE")
                {
                    pnSelfInforList[i].DropDownTextSelected = 0;
                }
                else
                {
                    pnSelfInforList[i].DropDownTextSelected = 1;
                }
                pnSelfInforList[i].TH20Text = "接收端饱和入射光(dbm)";
                pnSelfInforList[i].Colum20TextConfig = mydt.Rows[i]["RxOverLoadDBm"].ToString().Trim();
                pnSelfInforList[i].TH21Text = "是否已转产";
                if (mydt.Rows[i]["wsFlag"].ToString().ToUpper().Trim() == "FALSE")
                {
                    pnSelfInforList[i].DropDown21TextSelected = 0;
                }
                else
                {
                    pnSelfInforList[i].DropDown21TextSelected = 1;
                }

                {
                    pnSelfInforList[i].EnableColum8Text = false;
                    pnSelfInforList[i].EnableColum7Text = false;
                    pnSelfInforList[i].EnableColum6Text = false;
                    pnSelfInforList[i].EnableColum5Text = false;
                    pnSelfInforList[i].EnableColum4Text = false;
                    pnSelfInforList[i].EnableColum3Text = false;
                    pnSelfInforList[i].EnableColum2Text = false;
                   
                    pnSelfInforList[i].EnableColum10Text = false;
                    pnSelfInforList[i].EnableColum11Text = false;
                    pnSelfInforList[i].EnableColum12Text = false;
                    pnSelfInforList[i].EnableColum13Text = false;
                    pnSelfInforList[i].EnableColum14Text = false;
                    pnSelfInforList[i].EnableColum15Text = false;
                    pnSelfInforList[i].EnableColum16Text = false;
                    pnSelfInforList[i].EnableColum17Text = false;
                    pnSelfInforList[i].EnableColum18Text = false;
                    pnSelfInforList[i].EnableColum19Text = false;
                    pnSelfInforList[i].EnableColum20Text = false;
                    pnSelfInforList[i].EnableColum21Text = false;
                }

                this.PNSelfInfor.Controls.Add(pnSelfInforList[i]);
            }
        }

        ConfigExpressionList();

    }
    public void ConfigExpressionList()
    {
        string tempExpression = "";

        if (!AddNew)
        {
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
                    expressionList[i] = mydtFather.Rows[i]["PN"].ToString().Trim();
                }
                for (byte i = 0; i < pnSelfInforList.Length; i++)
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
                    pnSelfInforList[i].configExpression = tempExpression;
                    tempExpression = "";
                }
            }
        }
        else
        {
            ReadExpression(moduleTypeID);
            expressionList = new string[mydtFather.Rows.Count];
            for (byte i = 0; i < mydtFather.Rows.Count; i++)
            {
                expressionList[i] = mydtFather.Rows[i]["PN"].ToString().Trim();
            }

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
            pnSelf.configExpression = tempExpression;
            tempExpression = "";
        }
    }

    public bool ReadExpression(string key)
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                if (AddNew == true)
                {
                    mydtFather = pDataIO.GetDataTable("select * from GlobalProductionName where PID=" + moduleTypeID, "GlobalProductionName");
                }
                else
                {
                    mydtFather = pDataIO.GetDataTable("select * from GlobalProductionName where ID!=" + moduleTypeID + "and PID=" + key, "GlobalProductionName");
                }                
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
   
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                string typeID = "";
                if (AddNew)
                {
                    typeID = moduleTypeID;
                }
                else
                {
                    typeID = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + moduleTypeID).ToString();
                }
                mydt = pDataIO.GetDataTable("select * from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                mydtCoefs = pDataIO.GetDataTable("select * from GlobalManufactureCoefficientsGroup where IgnoreFlag='false' and TypeID=" + Convert.ToInt32(typeID), "GlobalManufactureCoefficientsGroup");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "添加新项";
                    HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalProductionType where ID = " + moduleTypeID).ToString();
                    HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + moduleTypeID;

                    HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "";
                }
                else
                {
                    parentItem = pDataIO.getDbCmdExecuteScalar("select PN from GlobalProductionName where id = " + moduleTypeID).ToString();
                    
                    HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + moduleTypeID).ToString();                   
                    HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + typeID;

                    HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim();
                }

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";
                                                
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
                this.plhNavi.Controls.Add(myCtrl);

                //mydtCoefs = pDataIO.GetDataTable("select * from GlobalManufactureCoefficientsGroup where IgnoreFlag='false' and TypeID=" + Convert.ToInt32(typeID), "GlobalManufactureCoefficientsGroup");
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
            for (byte i = 0; i < pnSelfInforList.Length; i++)
            {

                pnSelfInforList[i].EnableColum8Text = true;
                pnSelfInforList[i].EnableColum7Text = true;
                pnSelfInforList[i].EnableColum6Text = true;
                pnSelfInforList[i].EnableColum5Text = true;
                pnSelfInforList[i].EnableColum4Text = true;
                pnSelfInforList[i].EnableColum3Text = true;
                pnSelfInforList[i].EnableColum2Text = true;
                
                pnSelfInforList[i].EnableColum10Text = true;
                pnSelfInforList[i].EnableColum11Text = true;
                pnSelfInforList[i].EnableColum12Text = true;
                pnSelfInforList[i].EnableColum13Text = true;
                pnSelfInforList[i].EnableColum14Text = true;
                pnSelfInforList[i].EnableColum15Text = true;
                pnSelfInforList[i].EnableColum16Text = true;
                pnSelfInforList[i].EnableColum17Text = true;
                pnSelfInforList[i].EnableColum18Text = true;
                pnSelfInforList[i].EnableColum19Text = true;
                pnSelfInforList[i].EnableColum20Text = true;
                pnSelfInforList[i].EnableColum21Text = true;
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
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + moduleTypeID.Trim());
            }
            
            else
            {
                Response.Redirect(Request.Url.ToString(), true);
            }
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }


    }
   public bool SaveData(object obj, string prameter)
    {              
       string updataStr = "select * from GlobalProductionName where ID=" + moduleTypeID;
       try
       {
           bool refreshFlag = false;
           if (AddNew)
           {
               refreshFlag = true;
               DataTable inserTable = pDataIO.GetDataTable(updataStr, "GlobalProductionName");
               DataRow dr = inserTable.NewRow();
               {
                   dr[0] = -1;
                   dr[1] = moduleTypeID;
                   dr[2] = pnSelf.Colum2TextConfig;
                   dr[3] = pnSelf.Colum3TextConfig;
                   dr[4] = pnSelf.Colum4TextConfig;
                   dr[5] = pnSelf.Colum5TextConfig;
                   dr[6] = pnSelf.Colum6TextConfig;
                   dr[7] = MCoefsIDMap[pnSelf.ConfigSeletedIndexDD7];
                   dr[8] = false;
                   dr[9] = pnSelf.ConfigSeletedIndexDD8;
                   
                   dr[10] = pnSelf.ConfigSeletedIndexDD10;
                   dr[11] = pnSelf.ConfigSeletedIndexDD11;
                   dr[12] = pnSelf.ConfigSeletedIndexDD12;
                   dr[13] = pnSelf.ConfigSeletedIndexDD13;
                   dr[14] = pnSelf.ConfigSeletedIndexDD14;
                   dr[15] = pnSelf.Colum15TextConfig;
                   dr[16] = pnSelf.Colum16TextConfig;
                   dr[17] = pnSelf.Colum17TextConfig;
                   dr[18] = pnSelf.Colum18TextConfig;
                   dr[19] = pnSelf.DropDownTextSelected;
                   dr[20] = pnSelf.Colum20TextConfig;
                   dr[21] = pnSelf.DropDown21TextSelected;
                  
               }
               inserTable.Rows.Add(dr);

               int result = -1;
               if (Session["DB"].ToString().ToUpper() == "ATSDB")
               {
                   result = pDataIO.UpdateWithProc("GlobalProductionName", inserTable, updataStr, logTracingString, "ATS_V2");
               }
               else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
               {
                   result = pDataIO.UpdateWithProc("GlobalProductionName", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
               }      

               if (result > 0)
               {
                   inserTable.AcceptChanges();
                   //Response.Redirect("~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + moduleTypeID.Trim());

                   DataTable dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + moduleTypeID, "GlobalProductionTypeNum");
                   Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                   dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.wsFlag ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + moduleTypeID + ") as newtable where newtable.PN ='" + pnSelf.Colum2TextConfig + "'", "GlobalProductionNameNum");
                   Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                   Session["iframe_src"] = "WebFiles/Production_ATS/Production/PNSelfInfor.aspx?AddNew=false&uId=" + Convert.ToInt32(dt.Rows[0]["ID"]);
                   ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);
               }
               else
               {
                   //pDataIO.AlertMsgShow("数据更新失败!",Request.Url.ToString());
                   pnSelf.Colum2TextConfig = "";
                   pDataIO.AlertMsgShow("数据更新失败!");
               }                        
           }
           else
           {
               for (byte i = 0; i < pnSelfInforList.Length; i++)
               {
                   if (mydt.Rows[i]["PN"].ToString() != pnSelfInforList[i].Colum2TextConfig || Convert.ToInt32(mydt.Rows[i]["wsFlag"]) != pnSelfInforList[i].DropDown21TextSelected)
                   {
                       refreshFlag = true;
                   }
                   else
                   {
                       refreshFlag = false;
                   }

                   mydt.Rows[i]["PN"] = pnSelfInforList[i].Colum2TextConfig;
                   mydt.Rows[i]["ItemName"] = pnSelfInforList[i].Colum3TextConfig;
                   mydt.Rows[i]["Channels"] = pnSelfInforList[i].Colum4TextConfig;
                   mydt.Rows[i]["Voltages"] = pnSelfInforList[i].Colum5TextConfig;
                   mydt.Rows[i]["Tsensors"] = pnSelfInforList[i].Colum6TextConfig;
                   mydt.Rows[i]["MCoefsID"] = MCoefsIDMap[pnSelfInforList[i].ConfigSeletedIndexDD7];
                   mydt.Rows[i]["OldDriver"] = pnSelfInforList[i].ConfigSeletedIndexDD8;                   
                   mydt.Rows[i]["TEC_Present"] = pnSelfInforList[i].ConfigSeletedIndexDD10;
                   mydt.Rows[i]["Couple_Type"] = pnSelfInforList[i].ConfigSeletedIndexDD11;
                   mydt.Rows[i]["APC_Type"] = pnSelfInforList[i].ConfigSeletedIndexDD12;
                   mydt.Rows[i]["BER"] = pnSelfInforList[i].ConfigSeletedIndexDD13;
                   mydt.Rows[i]["MaxRate"] = pnSelfInforList[i].ConfigSeletedIndexDD14;
                   mydt.Rows[i]["Publish_PN"] = pnSelfInforList[i].Colum15TextConfig;
                   mydt.Rows[i]["NickName"] = pnSelfInforList[i].Colum16TextConfig;
                   mydt.Rows[i]["IbiasFormula"] = pnSelfInforList[i].Colum17TextConfig;
                   mydt.Rows[i]["IModFormula"] = pnSelfInforList[i].Colum18TextConfig;
                   mydt.Rows[i]["UsingCelsiusTemp"] = pnSelfInforList[i].DropDownTextSelected;
                   mydt.Rows[i]["RxOverLoadDBm"] = pnSelfInforList[i].Colum20TextConfig;
                   mydt.Rows[i]["wsFlag"] = pnSelfInforList[i].DropDown21TextSelected;
               }

               int result = -1;
               if (Session["DB"].ToString().ToUpper() == "ATSDB")
               {
                   result = pDataIO.UpdateWithProc("GlobalProductionName", mydt, updataStr, logTracingString, "ATS_V2");
               }
               else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
               {
                   result = pDataIO.UpdateWithProc("GlobalProductionName", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
               }      

               if (result > 0)
               {
                   mydt.AcceptChanges();

                   if (refreshFlag)
                   {
                       DataTable dt = pDataIO.GetDataTable("select PID from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                       int typeID = Convert.ToInt32(dt.Rows[0]["PID"]);
                       dt = pDataIO.GetDataTable("select newtable.num from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionType.ID ASC) AS num,GlobalProductionType.ID from GlobalProductionType where GlobalProductionType.IgnoreFlag='false') as newtable where newtable.ID=" + typeID, "GlobalProductionTypeNum");
                       Session["TreeNodeExpand"] = Convert.ToInt32(dt.Rows[0]["num"]) - 1;

                       dt = pDataIO.GetDataTable("select newtable.num,newtable.ID from (select ROW_NUMBER() OVER (ORDER BY GlobalProductionName.wsFlag ASC) AS num,GlobalProductionName.* from GlobalProductionName where IgnoreFlag='false' and PID =" + typeID + ") as newtable where newtable.PN ='" + pnSelfInforList[0].Colum2TextConfig + "'", "GlobalProductionNameNum");
                       Session["TreeNodeExpand"] = Session["TreeNodeExpand"].ToString() + "+" + (Convert.ToInt32(dt.Rows[0]["num"]) - 1).ToString();

                       Session["iframe_src"] = "WebFiles/Production_ATS/Production/PNSelfInfor.aspx?AddNew=false&uId=" + moduleTypeID;
                       ScriptManager.RegisterStartupScript(this, typeof(Page), "", "window.parent.RefreshTreeNode();", true);
                   }
               }
               else
               {
                   pDataIO.AlertMsgShow("数据更新失败!");
               }               
               for (byte i = 0; i < pnSelfInforList.Length; i++)
               {
                   pnSelfInforList[i].EnableColum8Text = false;
                   pnSelfInforList[i].EnableColum7Text = false;
                   pnSelfInforList[i].EnableColum6Text = false;
                   pnSelfInforList[i].EnableColum5Text = false;
                   pnSelfInforList[i].EnableColum4Text = false;
                   pnSelfInforList[i].EnableColum3Text = false;
                   pnSelfInforList[i].EnableColum2Text = false;
                  
                   pnSelfInforList[i].EnableColum10Text = false;
                   pnSelfInforList[i].EnableColum11Text = false;
                   pnSelfInforList[i].EnableColum12Text = false;
                   pnSelfInforList[i].EnableColum13Text = false;
                   pnSelfInforList[i].EnableColum14Text = false;
                   pnSelfInforList[i].EnableColum15Text = false;
                   pnSelfInforList[i].EnableColum16Text = false;
                   pnSelfInforList[i].EnableColum17Text = false;
                   pnSelfInforList[i].EnableColum18Text = false;
                   pnSelfInforList[i].EnableColum19Text = false;
                   pnSelfInforList[i].EnableColum20Text = false;
                   pnSelfInforList[i].EnableColum21Text = false;
               }
           }
           
           return true;
       }
       catch (System.Exception ex)
       {
           pDataIO.WriteErrorLogs(ex.ToString());
           throw ex;
       }
        
    }
   public void ConfigOptionButtonsVisible()
   {
       if (AddNew)
       {
           OptionButtons1.ConfigBtSaveVisible = true;
           OptionButtons1.ConfigBtEditVisible = false;
           OptionButtons1.ConfigBtCancelVisible = true;

           tdStandard.Attributes.Add("style", "visibility:visible;");
       } 
       else
       {         
           int myAccessCode =0;
           if (Session["AccCode"] != null)
           {
               myAccessCode = Convert.ToInt32(Session["AccCode"]);
           }
           CommCtrl mCommCtrl = new CommCtrl();
           bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);  

           OptionButtons1.ConfigBtSaveVisible = false;
           if (editVisible)
           {
               OptionButtons1.ConfigBtEditVisible = true;
           } 
           else
           {
               OptionButtons1.ConfigBtEditVisible = PNAuthority();
           }
            
           OptionButtons1.ConfigBtCancelVisible = false;
       }
       
       OptionButtons1.ConfigBtAddVisible = false;
       OptionButtons1.ConfigBtDeleteVisible = false;
   }
    public void ClearTextValue()
   {
       pnSelf = new ASCXPNSelfInfor();
        try
        {            
            {
                pnSelf = (ASCXPNSelfInfor)Page.LoadControl("~/Frame/Production/PNSelfInfor.ascx");
                ConfigMCoefsandPN(pnSelf);
                pnSelf.Colum2TextConfig = "";
                pnSelf.Colum3TextConfig = "";
                pnSelf.Colum4TextConfig = "";
                pnSelf.Colum5TextConfig = "";
                pnSelf.Colum6TextConfig = "";
                pnSelf.ConfigSeletedIndexDD7 =-1;
                pnSelf.ConfigSeletedIndexDD8 = -1;
                
                pnSelf.ConfigSeletedIndexDD10 = -1;
                pnSelf.ConfigSeletedIndexDD11 = -1;
                pnSelf.ConfigSeletedIndexDD12 = -1;
                pnSelf.ConfigSeletedIndexDD13 = -1;
                pnSelf.ConfigSeletedIndexDD14 = -1;
                pnSelf.Colum15TextConfig = "";
                pnSelf.Colum16TextConfig = "";
                pnSelf.Colum17TextConfig = "";
                pnSelf.Colum17TextToolTip = "";
                pnSelf.Colum18TextConfig = "";
                pnSelf.Colum18TextToolTip = "";
                pnSelf.DropDownTextSelected = -1;
                pnSelf.Colum20TextConfig = "";
                pnSelf.DropDown21TextSelected = -1;
                pnSelf.EnableColum8Text = true;
                pnSelf.EnableColum7Text = true;
                pnSelf.EnableColum6Text = true;
                pnSelf.EnableColum5Text = true;
                pnSelf.EnableColum4Text = true;
                pnSelf.EnableColum3Text = true;
                pnSelf.EnableColum2Text = true;
                
                pnSelf.EnableColum10Text = true;
                pnSelf.EnableColum11Text = true;
                pnSelf.EnableColum12Text = true;
                pnSelf.EnableColum13Text = true;
                pnSelf.EnableColum14Text = true;
                pnSelf.EnableColum15Text = true;
                pnSelf.EnableColum16Text = true;
                pnSelf.EnableColum17Text = true;
                pnSelf.EnableColum18Text = true;
                pnSelf.EnableColum19Text = true;
                pnSelf.EnableColum20Text = true;
                pnSelf.EnableColum21Text = true;
                pnSelf.TH2Text = "品名";
                pnSelf.TH3Text = "描述";
                pnSelf.TH4Text = "通道数";
                pnSelf.TH5Text = "电压数";
                pnSelf.TH6Text = "传感器类型";
                pnSelf.TH7Text = "系数组";
                pnSelf.TH8Text = mydt.Columns[9].ColumnName;

                pnSelf.TH10Text = "是否存在TEC";
                pnSelf.TH11Text = "耦合类型";
                pnSelf.TH12Text = "APC类型";
                pnSelf.TH13Text = "误码率（BER）";
                pnSelf.TH14Text = "最大速率";
                pnSelf.TH15Text = "正式品名";
                pnSelf.TH16Text = "别名";
                pnSelf.TH17Text = "Ibias公式";
                pnSelf.TH18Text = "Imod公式";
                pnSelf.TH19Text = "是否用摄氏温度";
                pnSelf.TH20Text = "接收端饱和入射光(dbm)";
                pnSelf.TH21Text = "是否已转产";
                this.PNSelfInfor.Controls.Add(pnSelf);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
   }
    public bool ConfigMCoefsandPN(ASCXPNSelfInfor input)
    {
        MCoefsIDMap.Clear();       
        try
        {
            for (int i = 0; i < mydtCoefs.Rows.Count; i++)
            {
                input.InsertColum7Text(i, new ListItem(mydtCoefs.Rows[i]["ItemName"].ToString().Trim()));

                MCoefsIDMap.Add(i, Convert.ToInt32(mydtCoefs.Rows[i]["ID"]));
            }
            input.InsertColum8Text(0, new ListItem("old"));
            input.InsertColum8Text(1, new ListItem("new"));
            DriverIDMap.Add(0, 0);
            DriverIDMap.Add(1, 1);

            //input.InsertColum9Text(0, new ListItem("old"));
            //input.InsertColum9Text(1, new ListItem("new"));
            //APCStyleIDMap.Add(0, 0);
            //APCStyleIDMap.Add(1, 1);

            input.InsertColum12Text(0, new ListItem("None"));
            input.InsertColum12Text(1, new ListItem("Open-Loop"));
            input.InsertColum12Text(2, new ListItem("Close-Loop"));
            input.InsertColum12Text(3, new ListItem("PIDClose-Loop"));
            APCTypeIDMap.Add(0, 0);
            APCTypeIDMap.Add(1, 1);
            APCTypeIDMap.Add(2, 2);
            APCTypeIDMap.Add(3, 3);

            input.InsertColum10Text(0, new ListItem("not TEC present"));
            input.InsertColum10Text(1, new ListItem("1 TEC Present"));
            input.InsertColum10Text(2, new ListItem("2 TEC Present"));
            input.InsertColum10Text(3, new ListItem("3 TEC Present"));
            input.InsertColum10Text(4, new ListItem("4 TEC Present"));
            TecPresentIDMap.Add(0, 0);
            TecPresentIDMap.Add(1, 1);
            TecPresentIDMap.Add(2, 2);
            TecPresentIDMap.Add(3, 3);
            TecPresentIDMap.Add(4, 4);

            input.InsertColum11Text(0, new ListItem("DC"));
            input.InsertColum11Text(1, new ListItem("AC"));
            CoupleTypeIDMap.Add(0, 0);
            CoupleTypeIDMap.Add(1, 1);
            input.InsertColum13Text(0, new ListItem("5E-5"));
            input.InsertColum13Text(1, new ListItem("1E-1"));
            input.InsertColum13Text(2, new ListItem("1E-2"));
            input.InsertColum13Text(3, new ListItem("1E-3"));
            input.InsertColum13Text(4, new ListItem("1E-4"));
            input.InsertColum13Text(5, new ListItem("1E-5"));
            input.InsertColum13Text(6, new ListItem("1E-6"));
            input.InsertColum13Text(7, new ListItem("1E-7"));
            input.InsertColum13Text(8, new ListItem("1E-8"));
            input.InsertColum13Text(9, new ListItem("1E-9"));
            input.InsertColum13Text(10, new ListItem("1E-10"));
            input.InsertColum13Text(11, new ListItem("1E-11"));
            input.InsertColum13Text(12, new ListItem("1E-12"));
            input.InsertColum13Text(13, new ListItem("1E-15"));
            BERIDMap.Add(0, 0);
            BERIDMap.Add(1, 1);
            BERIDMap.Add(2, 2);
            BERIDMap.Add(3, 3);
            BERIDMap.Add(4, 4);
            BERIDMap.Add(5, 5);
            BERIDMap.Add(6, 6);
            BERIDMap.Add(7, 7);
            BERIDMap.Add(8, 8);
            BERIDMap.Add(9,9);
            BERIDMap.Add(10, 10);
            BERIDMap.Add(11,11);
            BERIDMap.Add(12, 12);
            input.InsertColum14Text(0, new ListItem("1G"));
            input.InsertColum14Text(1, new ListItem("10G"));
            input.InsertColum14Text(2, new ListItem("14G"));
            input.InsertColum14Text(3, new ListItem("25G"));
            input.InsertColum14Text(4, new ListItem("28G"));
            MaxRateIDMap.Add(0,0);
            MaxRateIDMap.Add(1,1);
            MaxRateIDMap.Add(2,2);
            MaxRateIDMap.Add(3,3);
            MaxRateIDMap.Add(4,4);
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
    public bool PNAuthority()
    {
        bool editVisibal = false;
        string userID = Session["UserID"].ToString().Trim();
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + moduleTypeID, "UserPNAction");
                //DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where PNID=" + moduleTypeID, "UserPNAction");
                if (temp==null|| temp.Rows.Count==0||temp.Rows.Count>1)
                {
                    return false;
                } 
                else
                {
                    if (temp.Rows[0]["ModifyPN"].ToString().Trim().ToUpper()=="TRUE"||temp.Rows[0]["ModifyPN"].ToString().Trim().ToUpper()=="1")
                    {
                        editVisibal=true;
                    } 
                    else
                    {
                        editVisibal=false;
                    }
                  
                }
            }
            return editVisibal;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}