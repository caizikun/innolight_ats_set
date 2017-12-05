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
public partial class ASPXModuleTypeSelfInfor : BasePage
{
    string funcItemName = "ProductionTypeInfor";
    ASCXOptionButtons UserOptionButton;
    private SortedList<int, int> MsaIDMap = new SortedList<int, int>();
    ASCXModuleTypeSelfInfor[] MConfigInitSelfInfor;
    ASCXModuleTypeSelfInfor MConfigInit;
    DropDownList ddlist = new DropDownList();
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   public DataTable mydtMSA = new DataTable();
   private string moduleTypeID = "";
    private int rowCount;
    private int columCount;
    private bool AddNew;
    private string logTracingString = "";
    public ASPXModuleTypeSelfInfor()
    {
        columCount = 0;
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
        MsaIDMap.Clear();
    }
    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();
            SetSessionBlockType(2);
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

        MConfigInitSelfInfor = new ASCXModuleTypeSelfInfor[rowCount];
        if (rowCount == 0 && AddNew == false)
        {
            return;
        }
        if (AddNew)
        {
            ClearTextValue();
        }
        else
        {
            for (byte i = 0; i < MConfigInitSelfInfor.Length; i++)
            {
                MConfigInitSelfInfor[i] = (ASCXModuleTypeSelfInfor)Page.LoadControl("../../Frame/Production/ModuleTypeSelfInfor.ascx");
                MConfigInitSelfInfor[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                MConfigInitSelfInfor[i].Colum2TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();
                MConfigInitSelfInfor[i].TH3Text = mydt.Columns[2].ColumnName;
                MConfigInitSelfInfor[i].ClearDropDownList();
                ConfigMSA(MConfigInitSelfInfor[i]);
                //MConfigInitSelfInfor[i].SetDropDownList = ddlist;
                int temp = ddlist.Items.Count;
                MConfigInitSelfInfor[i].ConfigSeletedIndex = MsaIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["MSAID"]));
                MConfigInitSelfInfor[i].TH2Text = mydt.Columns[1].ColumnName;
                MConfigInitSelfInfor[i].TH3Text = mydt.Columns[2].ColumnName;
                MConfigInitSelfInfor[i].EnableColum2Text = false;
                MConfigInitSelfInfor[i].EnableColum3Text = false;
                this.ModuleTypeSelfInfor.Controls.Add(MConfigInitSelfInfor[i]);
            }
        }
       

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalProductionType where ID=" + moduleTypeID, "GlobalProductionType");
                if (AddNew)
                {
                    mydtMSA = pDataIO.GetDataTable("select * from GlobalMSA  where IgnoreFlag='false'", "GlobalMSA");
                } 
                else
                {
                    if (IsPostBack)
                    {
                        mydtMSA = pDataIO.GetDataTable("select * from GlobalMSA  where IgnoreFlag='false'", "GlobalMSA");
                    } 
                    else
                    {
                        mydtMSA = pDataIO.GetDataTable("select * from GlobalMSA", "GlobalMSA");
                    }
                    
                }
                
                rowCount = mydt.Rows.Count;
                columCount = mydt.Columns.Count;
              
                bindData();
                  string parentItem="";
                  if (AddNew)
                  {
                      parentItem = "AddNewItem";
                  }
                  else
                  {
                      parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from GlobalProductionType where id = " + moduleTypeID).ToString();
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
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/Production/ProductionModuleTypeList.aspx?BlockType=" + Session["BlockType"]);
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
        //string updataStr = "select * from GlobalProductionType where ID=" + moduleTypeID;
        string updataStr = "select * from GlobalProductionType";
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "GlobalProductionType");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = MConfigInit.Colum2TextConfig;
                dr[2] = MsaIDMap[MConfigInit.ConfigSeletedIndex];
                dr[3] = false;
                    inserTable.Rows.Add(dr);
                    int result = pDataIO.UpdateWithProc("GlobalProductionType", inserTable, updataStr, logTracingString);
                    if (result > 0)
                    {
                        inserTable.AcceptChanges();
                        if (funcItemName == "ProductionTypeInfor")
                        {
                            Response.Redirect("~/WebFiles/Production/ProductionModuleTypeList.aspx?BlockType=" + Session["BlockType"]);
                        } 
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("Update data fail!",Request.Url.ToString());
                    }                    
                   
                    
                    return true;
               
            } 
            else
            {
                if (rowCount != 1)
                {
                    return false;
                }
                else
                {
                  
                    for (byte j = 0; j < MConfigInitSelfInfor.Length; j++)
                    {                      
                        mydt.Rows[0]["ItemName"] = MConfigInitSelfInfor[j].Colum2TextConfig;
                        mydt.Rows[0]["MSAID"] = MsaIDMap[MConfigInitSelfInfor[j].ConfigSeletedIndex];
                    }
                }
                int result = pDataIO.UpdateWithProc("GlobalProductionType", mydt, updataStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!");
                }                 
                for (byte i = 0; i < MConfigInitSelfInfor.Length; i++)
                {
                    MConfigInitSelfInfor[i].EnableColum2Text = false;
                    MConfigInitSelfInfor[i].EnableColum3Text = false;
                }
                return true;
            }
            
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;

        }
        
    }
    public bool EditData(object obj, string prameter)
    {
       try
       {
           
           for (byte i = 0; i < MConfigInitSelfInfor.Length; i++)
           {
               MConfigInitSelfInfor[i].EnableColum3Text = true;
               MConfigInitSelfInfor[i].EnableColum2Text = true;             
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
    public void ConfigOptionButtonsVisible()
    {
      
        OptionButtons1.ConfigBtAddVisible = false;
        if (AddNew)
        {
            OptionButtons1.ConfigBtEditVisible = false;
            OptionButtons1.ConfigBtSaveVisible = true;
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

            OptionButtons1.ConfigBtSaveVisible = false;
            OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.MofifyProduction, myAccessCode);      
            OptionButtons1.ConfigBtCancelVisible = false;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        MConfigInit = new ASCXModuleTypeSelfInfor();
        try
        {


            {
                MConfigInit = (ASCXModuleTypeSelfInfor)Page.LoadControl("../../Frame/Production/ModuleTypeSelfInfor.ascx");
                MConfigInit.Colum2TextConfig = "";
                MConfigInit.Colum3TextConfig = "";
                MConfigInit.EnableColum3Text = true;
                MConfigInit.EnableColum2Text = true;
                ConfigMSA(MConfigInit);
                MConfigInit.ConfigSeletedIndex = -1;
                MConfigInit.TH2Text = mydt.Columns[1].ColumnName;
                MConfigInit.TH3Text = mydt.Columns[2].ColumnName;
                

                this.ModuleTypeSelfInfor.Controls.Add(MConfigInit);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool ConfigMSA(ASCXModuleTypeSelfInfor input)
    {

        MsaIDMap.Clear();
        ddlist.Items.Clear();
        try
        {
            for (int i = 0; i < mydtMSA.Rows.Count;i++)
            {
                input.InsertColum3Text(i, new ListItem(mydtMSA.Rows[i]["ItemName"].ToString().Trim()));
                
                MsaIDMap.Add(i, Convert.ToInt32(mydtMSA.Rows[i]["ID"]));
            }
          
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        
    }
}