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
public partial class ASPXE2ROMSelfInfor : BasePage
{
    string funcItemName = "EEPROM信息";
    ASCXOptionButtons UserOptionButton;
    ASCXE2ROMSelinfor[] e2romIniSelfInforlist;
    ASCXE2ROMSelinfor e2romIControl;
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
            SetSessionBlockType(1);
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
        e2romIniSelfInforlist = new ASCXE2ROMSelinfor[rowCount];
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
            for (byte i = 0; i < e2romIniSelfInforlist.Length; i++)
            {
                e2romIniSelfInforlist[i] = (ASCXE2ROMSelinfor)Page.LoadControl("~/Frame/Production/E2ROMSelinfor.ascx");
                //e2romIniSelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().ToUpper().Trim();
                e2romIniSelfInforlist[i].TH2Text = "名称";
                e2romIniSelfInforlist[i].Colum2TextConfig = mydt.Rows[i]["ItemName"].ToString().ToUpper().Trim();
                
                e2romIniSelfInforlist[i].EnableColum2Text = false;
                

                this.E2ROMSelinfor.Controls.Add(e2romIniSelfInforlist[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoMSAEEPROMSet where ID=" + moduleTypeID, "TopoMSAEEPROMSet");
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
                    parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from TopoMSAEEPROMSet where id =" + moduleTypeID).ToString();
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
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/Production_ATS/Production/TopEEROMList.aspx?uId=" + moduleTypeID.Trim());
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
        string updataStr = "select * from TopoMSAEEPROMSet where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "TopoMSAEEPROMSet");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = e2romIControl.Colum2TextConfig;
                dr[3] = "";
                dr[4] = 0;
                dr[5] = "";
                dr[6] = 0;
                dr[7] = "";
                dr[8] = 0;
                dr[9] = "";
                dr[10] = 0;
                  inserTable.Rows.Add(dr);

                  int result = -1;
                  if (Session["DB"].ToString().ToUpper() == "ATSDB")
                  {
                      result = pDataIO.UpdateWithProc("TopoMSAEEPROMSet", inserTable, updataStr, logTracingString, "ATS_V2");
                  }
                  else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                  {
                      result = pDataIO.UpdateWithProc("TopoMSAEEPROMSet", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
                  }      

                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/Production_ATS/Production/TopEEROMList.aspx?uId=" + moduleTypeID.Trim());
                  }
                  else
                  {
                      pDataIO.AlertMsgShow("数据更新失败!",Request.Url.ToString());
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

                    for (byte j = 0; j < e2romIniSelfInforlist.Length; j++)
                    {
                        mydt.Rows[0]["ItemName"] = e2romIniSelfInforlist[j].Colum2TextConfig;
                    }

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("TopoMSAEEPROMSet", mydt, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("TopoMSAEEPROMSet", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!");
                    }
                    for (byte i = 0; i < e2romIniSelfInforlist.Length; i++)
                        {
                            e2romIniSelfInforlist[i].EnableColum2Text = false;
                           
                        }
                        return true;
                   
                }
               
               
              
            }

        }
        catch (System.Exception ex)
        {
            throw ex;

        }

    }
    public bool EditData(object obj, string prameter)
    {
       try
       {
           for (int i = 0; i < e2romIniSelfInforlist.Length;i++)
           {
               e2romIniSelfInforlist[i].EnableColum2Text = true;
              
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
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        e2romIControl = new ASCXE2ROMSelinfor();
        try
        {


            {
                e2romIControl = (ASCXE2ROMSelinfor)Page.LoadControl("~/Frame/Production/E2ROMSelinfor.ascx");
                e2romIControl.Colum2TextConfig = "";               
               
                
                e2romIControl.EnableColum2Text = true;
                e2romIControl.TH2Text = "名称";
               

                this.E2ROMSelinfor.Controls.Add(e2romIControl);
            }
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
                DataTable selectPNid = pDataIO.GetDataTable("select PID from TopoMSAEEPROMSet where ID=" + moduleTypeID, "TopoMSAEEPROMSet");
                string StrPNId = selectPNid.Rows[0]["PID"].ToString().Trim();
                DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + StrPNId, "UserPNAction");
                //DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where PNID=" + moduleTypeID, "UserPNAction");
                if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                {
                    return false;
                }
                else
                {
                    if (temp.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "1")
                    {
                        editVisibal = true;
                    }
                    else
                    {
                        editVisibal = false;
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