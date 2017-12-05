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
public partial class ASPXGlobalMSASelfInfor : BasePage
{
    string funcItemName = "MSAInfor";
    ASCXOptionButtons UserOptionButton;
    ASCXMSASelfInfor[] GlobalMSASelfInforlist;
    ASCXMSASelfInfor MSASelfInfor;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private int columCount;
   private bool AddNew;
    private string logTracingString = "";
    public ASPXGlobalMSASelfInfor()
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
    }
    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();
            SetSessionBlockType(3);
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
        GlobalMSASelfInforlist = new ASCXMSASelfInfor[rowCount];
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
            for (byte i = 0; i < GlobalMSASelfInforlist.Length; i++)
            {
                GlobalMSASelfInforlist[i] = (ASCXMSASelfInfor)Page.LoadControl("../../Frame/MSAInfo/MSASelfInfor.ascx");
                GlobalMSASelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().ToUpper().Trim();
                GlobalMSASelfInforlist[i].TH2Text = mydt.Columns[1].ColumnName;
                GlobalMSASelfInforlist[i].Colum2TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();
                GlobalMSASelfInforlist[i].TH3Text = mydt.Columns[2].ColumnName;
                GlobalMSASelfInforlist[i].Colum3TextConfig = mydt.Rows[i]["AccessInterface"].ToString().Trim();

                GlobalMSASelfInforlist[i].TH4Text = mydt.Columns[3].ColumnName;
                GlobalMSASelfInforlist[i].Colum4TextConfig = mydt.Rows[i]["SlaveAddress"].ToString().Trim();

               
             
                GlobalMSASelfInforlist[i].EnableColum2Text = false;
                GlobalMSASelfInforlist[i].EnableColum3Text = false;
                GlobalMSASelfInforlist[i].EnableColum4Text = false;
            


                this.GlobalMSASelfInfor.Controls.Add(GlobalMSASelfInforlist[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalMSA where ID=" + moduleTypeID, "GlobalMSA");
                rowCount = mydt.Rows.Count;
                columCount = mydt.Columns.Count;
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "AddNewItem";
                }
                else
                {
                    parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalMSA where id = " + moduleTypeID).ToString();
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
    public bool SaveData(object obj, string prameter)
    {
        string updataStr = "select * from GlobalMSA where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "GlobalMSA");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = MSASelfInfor.Colum2TextConfig;
                dr[2] = MSASelfInfor.Colum3TextConfig;
                dr[3] = MSASelfInfor.Colum4TextConfig;
                dr[4] = false;
                    inserTable.Rows.Add(dr);
                    int result = pDataIO.UpdateWithProc("GlobalMSA", inserTable, updataStr, logTracingString);
                    if (result > 0)
                    {
                        inserTable.AcceptChanges();
                        Response.Redirect("~/WebFiles/MSAInfo/MSAModuleTypeList.aspx?BlockType=" + Session["BlockType"]);
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("Update data fail!", Request.Url.ToString());
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

                    for (byte j = 0; j < GlobalMSASelfInforlist.Length; j++)
                                {
                                    mydt.Rows[0]["ItemName"] = GlobalMSASelfInforlist[j].Colum2TextConfig;
                                    mydt.Rows[0]["AccessInterface"] = GlobalMSASelfInforlist[j].Colum3TextConfig;
                                    mydt.Rows[0]["SlaveAddress"] = GlobalMSASelfInforlist[j].Colum4TextConfig;
                                  
                                }                       
                        int result = pDataIO.UpdateWithProc("GlobalMSA", mydt, updataStr, logTracingString);
                        if (result > 0)
                        {
                            mydt.AcceptChanges();
                        }
                        else
                        {
                            pDataIO.AlertMsgShow("Update data fail!");
                        }  
                        for (byte i = 0; i < GlobalMSASelfInforlist.Length; i++)
                        {
                            GlobalMSASelfInforlist[i].EnableColum2Text = false;
                            GlobalMSASelfInforlist[i].EnableColum3Text = false;
                            GlobalMSASelfInforlist[i].EnableColum4Text = false;
                           
                        }
                        return true;
                   
                }
               
               
              
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
           for (int i = 0; i < GlobalMSASelfInforlist.Length; i++)
           {
               GlobalMSASelfInforlist[i].EnableColum2Text = true;
               GlobalMSASelfInforlist[i].EnableColum3Text = true;
               GlobalMSASelfInforlist[i].EnableColum4Text = true;
              
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
                Response.Redirect("~/WebFiles/MSAInfo/MSAModuleTypeList.aspx?BlockType=" + Session["BlockType"]);
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
            OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MSA, CommCtrl.CheckAccess.MofifyMSA, myAccessCode);      
            OptionButtons1.ConfigBtCancelVisible = false;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        MSASelfInfor = new ASCXMSASelfInfor();
        try
        {


            {
                MSASelfInfor = (ASCXMSASelfInfor)Page.LoadControl("../../Frame/MSAInfo/MSASelfInfor.ascx");
                MSASelfInfor.Colum2TextConfig = "";
                MSASelfInfor.Colum3TextConfig = "";
                MSASelfInfor.Colum4TextConfig = "";
             

              
                MSASelfInfor.EnableColum4Text = true;
                MSASelfInfor.EnableColum3Text = true;
                MSASelfInfor.EnableColum2Text = true;
                MSASelfInfor.TH2Text = mydt.Columns[1].ColumnName;
                MSASelfInfor.TH3Text = mydt.Columns[2].ColumnName;
                MSASelfInfor.TH4Text = mydt.Columns[3].ColumnName;
               

                this.GlobalMSASelfInfor.Controls.Add(MSASelfInfor);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}