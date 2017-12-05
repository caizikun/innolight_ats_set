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
public partial class ASPXPNSelfInfor : BasePage
{
    string funcItemName = "ProductionPNInfor";
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
    public ASPXPNSelfInfor()
    {
        AddNew = false;
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
            SetSessionBlockType(2);
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
                pnSelfInforList[i] = (ASCXPNSelfInfor)Page.LoadControl("../../Frame/Production/PNSelfInfor.ascx");
                pnSelfInforList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                pnSelfInforList[i].TH2Text = mydt.Columns[2].ColumnName;
                pnSelfInforList[i].Colum2TextConfig = mydt.Rows[i]["PN"].ToString().Trim();
                pnSelfInforList[i].TH3Text = "Description";
                pnSelfInforList[i].Colum3TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();

                pnSelfInforList[i].TH4Text = mydt.Columns[4].ColumnName;
                pnSelfInforList[i].Colum4TextConfig = mydt.Rows[i]["Channels"].ToString().Trim();

                pnSelfInforList[i].TH5Text = mydt.Columns[5].ColumnName;
                pnSelfInforList[i].Colum5TextConfig = mydt.Rows[i]["Voltages"].ToString().Trim();

                pnSelfInforList[i].TH6Text = mydt.Columns[6].ColumnName;
                pnSelfInforList[i].Colum6TextConfig = mydt.Rows[i]["Tsensors"].ToString().Trim();

                pnSelfInforList[i].ClearDropDownList();
                ConfigMCoefsandPN(pnSelfInforList[i]);               
                pnSelfInforList[i].TH7Text = mydt.Columns[7].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD7 = MCoefsIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["MCoefsID"]));
                
                pnSelfInforList[i].TH8Text = mydt.Columns[9].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD8 = DriverIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["OldDriver"]));
                
             
                pnSelfInforList[i].TH10Text = mydt.Columns[10].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD10 = TecPresentIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["TEC_Present"]));
                pnSelfInforList[i].TH11Text = mydt.Columns[11].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD11 = CoupleTypeIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["Couple_Type"]));
                pnSelfInforList[i].TH12Text = mydt.Columns[12].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD12 = APCTypeIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["APC_Type"]));
                pnSelfInforList[i].TH13Text = mydt.Columns[13].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD13 = BERIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["BER"]));
                pnSelfInforList[i].TH14Text = mydt.Columns[14].ColumnName;
                pnSelfInforList[i].ConfigSeletedIndexDD14 = MaxRateIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["MaxRate"]));

                pnSelfInforList[i].TH15Text = mydt.Columns[15].ColumnName;
                pnSelfInforList[i].Colum15TextConfig = mydt.Rows[i]["Publish_PN"].ToString().Trim();
                pnSelfInforList[i].TH16Text = mydt.Columns[16].ColumnName;
                pnSelfInforList[i].Colum16TextConfig = mydt.Rows[i]["NickName"].ToString().Trim();
                pnSelfInforList[i].TH17Text = mydt.Columns[17].ColumnName;
                pnSelfInforList[i].Colum17TextConfig = mydt.Rows[i]["IbiasFormula"].ToString().Trim();
                pnSelfInforList[i].TH18Text = mydt.Columns[18].ColumnName;
                pnSelfInforList[i].Colum18TextConfig = mydt.Rows[i]["IModFormula"].ToString().Trim();
                pnSelfInforList[i].TH19Text = mydt.Columns[19].ColumnName;
                if (mydt.Rows[i]["UsingCelsiusTemp"].ToString().ToUpper().Trim() == "FALSE")
                {
                    pnSelfInforList[i].DropDownTextSelected = 0;
                }
                else
                {
                    pnSelfInforList[i].DropDownTextSelected = 1;
                }
                pnSelfInforList[i].TH20Text = mydt.Columns[20].ColumnName;
                pnSelfInforList[i].Colum20TextConfig = mydt.Rows[i]["RxOverLoadDBm"].ToString().Trim();
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
                }

                this.PNSelfInfor.Controls.Add(pnSelfInforList[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                mydtCoefs = pDataIO.GetDataTable("select * from GlobalManufactureCoefficientsGroup", "GlobalManufactureCoefficientsGroup");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "AddNewItem";
                }
                else
                {
                    parentItem = pDataIO.getDbCmdExecuteScalar("select PN from GlobalProductionName where id = " + moduleTypeID).ToString();
                }
               
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
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/Production/ProductionPNList.aspx?uId=" + moduleTypeID.Trim());
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
           if (AddNew)
           {
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
                  
               }
               inserTable.Rows.Add(dr);
               int result = pDataIO.UpdateWithProc("GlobalProductionName", inserTable, updataStr, logTracingString);
               if (result > 0)
               {
                   inserTable.AcceptChanges();
                   Response.Redirect("~/WebFiles/Production/ProductionPNList.aspx?uId=" + moduleTypeID.Trim());
               }
               else
               {
                   //pDataIO.AlertMsgShow("Update data fail!",Request.Url.ToString());
                   pnSelf.Colum2TextConfig = "";
                   pDataIO.AlertMsgShow("Update data fail!");
               }              
            
           }

           else
           {
               for (byte i = 0; i < pnSelfInforList.Length; i++)
               {
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
               }
               int result = pDataIO.UpdateWithProc("GlobalProductionName", mydt, updataStr, logTracingString);
               if (result > 0)
               {
                   mydt.AcceptChanges();
               }
               else
               {
                   pDataIO.AlertMsgShow("Update data fail!");
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
       } 
       else
       {
          
           int myAccessCode =0;
           if (Session["AccCode"] != null)
           {
               myAccessCode = Convert.ToInt32(Session["AccCode"]);
           }
           CommCtrl mCommCtrl = new CommCtrl();
           bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.MofifyProduction, myAccessCode);  
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
                pnSelf = (ASCXPNSelfInfor)Page.LoadControl("../../Frame/Production/PNSelfInfor.ascx");
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
                pnSelf.Colum18TextConfig = "";
                pnSelf.DropDownTextSelected = -1;
                pnSelf.Colum20TextConfig = "";
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
                pnSelf.TH2Text = mydt.Columns[2].ColumnName;
                pnSelf.TH3Text = "Description";
                pnSelf.TH4Text = mydt.Columns[4].ColumnName;
                pnSelf.TH5Text = mydt.Columns[5].ColumnName;
                pnSelf.TH6Text = mydt.Columns[6].ColumnName;
                pnSelf.TH7Text = mydt.Columns[7].ColumnName;
                pnSelf.TH8Text = mydt.Columns[9].ColumnName;
                
                pnSelf.TH10Text = mydt.Columns[10].ColumnName;
                pnSelf.TH11Text = mydt.Columns[11].ColumnName;
                pnSelf.TH12Text = mydt.Columns[12].ColumnName;
                pnSelf.TH13Text = mydt.Columns[13].ColumnName;
                pnSelf.TH14Text = mydt.Columns[14].ColumnName;
                pnSelf.TH15Text = mydt.Columns[15].ColumnName;
                pnSelf.TH16Text = mydt.Columns[16].ColumnName;
                pnSelf.TH17Text = mydt.Columns[17].ColumnName;
                pnSelf.TH18Text = mydt.Columns[18].ColumnName;
                pnSelf.TH19Text = mydt.Columns[19].ColumnName;
                pnSelf.TH20Text = mydt.Columns[20].ColumnName;
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