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
    string funcItemName = "MSA信息";
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

    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();

            columCount = 0;
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
                GlobalMSASelfInforlist[i] = (ASCXMSASelfInfor)Page.LoadControl("~/Frame/MSAInfo/MSASelfInfor.ascx");
                //GlobalMSASelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().ToUpper().Trim();
                GlobalMSASelfInforlist[i].TH2Text = "名称";
                GlobalMSASelfInforlist[i].Colum2TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();
                GlobalMSASelfInforlist[i].TH3Text = "接口";
                GlobalMSASelfInforlist[i].Colum3TextConfig = mydt.Rows[i]["AccessInterface"].ToString().Trim();

                GlobalMSASelfInforlist[i].TH4Text = "模块地址";
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
                    parentItem = "添加新项";
                }
                else
                {
                    parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalMSA where id = " + moduleTypeID).ToString();
                }
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

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSA", inserTable, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSA", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result > 0)
                    {
                        inserTable.AcceptChanges();
                        Response.Redirect("~/WebFiles/MaintainInfo/MSAInfo/MSAModuleTypeList.aspx?");
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
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

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSA", mydt, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSA", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                        if (result > 0)
                        {
                            mydt.AcceptChanges();
                        }
                        else
                        {
                            pDataIO.AlertMsgShow("数据更新失败!");
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
                Response.Redirect("~/WebFiles/MaintainInfo/MSAInfo/MSAModuleTypeList.aspx?");
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
                MSASelfInfor = (ASCXMSASelfInfor)Page.LoadControl("~/Frame/MSAInfo/MSASelfInfor.ascx");
                MSASelfInfor.Colum2TextConfig = "";
                MSASelfInfor.Colum3TextConfig = "";
                MSASelfInfor.Colum4TextConfig = "";
             

              
                MSASelfInfor.EnableColum4Text = true;
                MSASelfInfor.EnableColum3Text = true;
                MSASelfInfor.EnableColum2Text = true;
                MSASelfInfor.TH2Text = "名称";
                MSASelfInfor.TH3Text = "接口";
                MSASelfInfor.TH4Text = "模块地址";
               

                this.GlobalMSASelfInfor.Controls.Add(MSASelfInfor);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}