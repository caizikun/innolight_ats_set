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
public partial class ASPX_Chip_ChipSelfInfor : BasePage
{
    string funcItemName = "芯片详细信息(维护)";
    ASCXOptionButtons UserOptionButton;
    ASCX_Chip_ChipSelfInfor[] ChipIniSelfInforlist;
    ASCX_Chip_ChipSelfInfor chipsetControl;
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
        ChipIniSelfInforlist = new ASCX_Chip_ChipSelfInfor[rowCount];
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
            for (byte i = 0; i < ChipIniSelfInforlist.Length; i++)
            {
                ChipIniSelfInforlist[i] = (ASCX_Chip_ChipSelfInfor)Page.LoadControl("~/Frame/Chip/ChipSelfInfor.ascx");
                //ChipIniSelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().Trim();
                ChipIniSelfInforlist[i].Colum1TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();
                ChipIniSelfInforlist[i].TH2Text = "通道";
                ChipIniSelfInforlist[i].Colum2TextSelected = ChannelIndex(mydt.Rows[i]["Channels"].ToString().Trim());
                ChipIniSelfInforlist[i].TH3Text = "描述";
                ChipIniSelfInforlist[i].Colum3TextConfig = mydt.Rows[i]["Description"].ToString().Trim();
                ChipIniSelfInforlist[i].TH4Text = "寄存器宽度";
                ChipIniSelfInforlist[i].Colum4TextSelected = WidthIndex(mydt.Rows[i]["Width"].ToString().Trim());
                
                ChipIniSelfInforlist[i].TH5Text = "低位在前高位在后?";

                if (mydt.Rows[i]["LittleEndian"].ToString().ToUpper().Trim() == "FALSE")
                {
                    ChipIniSelfInforlist[i].Colum5TextSelected = 0;
                }
                else
                {
                    ChipIniSelfInforlist[i].Colum5TextSelected = 1;
                }
                ChipIniSelfInforlist[i].EnableColum1Text = false;
                ChipIniSelfInforlist[i].EnableColum2Text = false;
                ChipIniSelfInforlist[i].EnableColum3Text = false;
                ChipIniSelfInforlist[i].EnableColum4Text = false;
                ChipIniSelfInforlist[i].EnableColum5Text = false;
                this.ChipInfor.Controls.Add(ChipIniSelfInforlist[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + moduleTypeID, "ChipBaseInfo");
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

                    parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from ChipBaseInfo where id =" + moduleTypeID).ToString();
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
                Response.Redirect("~/WebFiles/MaintainInfo/Chip/ChipBaseList.aspx?BlockType=" + Session["BlockType"]);
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
        string updataStr = "select * from ChipBaseInfo where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "ChipBaseInfo");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = chipsetControl.Colum1TextConfig;
                dr[2] = chipsetControl.Colum2TextConfig;
                dr[3] = chipsetControl.Colum3TextConfig;
                dr[4] = chipsetControl.Colum4TextConfig;
                dr[5] = chipsetControl.Colum5TextConfig;
                
                  inserTable.Rows.Add(dr);

                  int result = -1;
                  if (Session["DB"].ToString().ToUpper() == "ATSDB")
                  {
                      result = pDataIO.UpdateWithProc("ChipBaseInfo", inserTable, updataStr, logTracingString, "ATS_V2");
                  }
                  else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                  {
                      result = pDataIO.UpdateWithProc("ChipBaseInfo", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
                  }      

                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/MaintainInfo/Chip/ChipBaseList.aspx?BlockType=" + Session["BlockType"]);
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

                    for (byte j = 0; j < ChipIniSelfInforlist.Length; j++)
                    {
                        mydt.Rows[0]["ItemName"] = ChipIniSelfInforlist[j].Colum1TextConfig;
                        mydt.Rows[0]["Channels"] = ChipIniSelfInforlist[j].Colum2TextConfig;
                        mydt.Rows[0]["Description"] = ChipIniSelfInforlist[j].Colum3TextConfig;
                        mydt.Rows[0]["Width"] = ChipIniSelfInforlist[j].Colum4TextConfig;
                        mydt.Rows[0]["LittleEndian"] = ChipIniSelfInforlist[j].Colum5TextConfig;
                    }

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("ChipBaseInfo", mydt, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("ChipBaseInfo", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!");
                    }
                    for (byte i = 0; i < ChipIniSelfInforlist.Length; i++)
                        {
                            ChipIniSelfInforlist[i].EnableColum1Text = false;
                            ChipIniSelfInforlist[i].EnableColum2Text = false;
                            ChipIniSelfInforlist[i].EnableColum3Text = false;
                            ChipIniSelfInforlist[i].EnableColum4Text = false;
                            ChipIniSelfInforlist[i].EnableColum5Text = false;
                            
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
           for (int i = 0; i < ChipIniSelfInforlist.Length;i++)
           {
               ChipIniSelfInforlist[i].EnableColum1Text = true;
               ChipIniSelfInforlist[i].EnableColum2Text = true;
               ChipIniSelfInforlist[i].EnableColum3Text = true;
               ChipIniSelfInforlist[i].EnableColum4Text = true;
               ChipIniSelfInforlist[i].EnableColum5Text = true;               
               
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
            bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ChipInfo, CommCtrl.CheckAccess.MofifyChipInfor, myAccessCode);
            OptionButtons1.ConfigBtCancelVisible = false;
            OptionButtons1.ConfigBtEditVisible = editVisible;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        chipsetControl = new ASCX_Chip_ChipSelfInfor();
        try
        {


            {
                chipsetControl = (ASCX_Chip_ChipSelfInfor)Page.LoadControl("~/Frame/Chip/ChipSelfInfor.ascx");
                chipsetControl.Colum2TextConfig = "";
                chipsetControl.Colum3TextConfig = "";
                chipsetControl.Colum4TextConfig = "";
                chipsetControl.Colum5TextConfig = "";
                chipsetControl.Colum1TextConfig = "";               
                
                chipsetControl.EnableColum5Text = true;
                chipsetControl.EnableColum4Text = true;
                chipsetControl.EnableColum3Text = true;
                chipsetControl.EnableColum2Text = true;
                chipsetControl.EnableColum1Text = true;
                chipsetControl.TH2Text = "通道";
                chipsetControl.TH3Text = "描述";
                chipsetControl.TH4Text = "寄存器宽度";
                chipsetControl.TH5Text = "低位在前高位在后?";
              

                this.ChipInfor.Controls.Add(chipsetControl);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    
    public int ChannelIndex(string input)
    {
        int index = 0;
        try
        {
            switch (input)
            {
               
                case "0":
                    {
                        index = 0;
                        break;
                    }
                case "1":
                    {
                        index = 1;
                        break;
                    }
                case "2":
                    {
                        index = 2;
                        break;
                    }
                case "3":
                    {
                        index = 3;
                        break;
                    }
                case "4":
                    {
                        index = 4;
                        break;
                    }
                default:
                    {
                        index = 0;
                        break;
                    }
            }
            return index;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public int WidthIndex(string input)
    {
        int index = 0;
        try
        {
            switch (input)
            {

                case "1":
                    {
                        index = 0;
                        break;
                    }
                case "2":
                    {
                        index = 1;
                        break;
                    }
                case "4":
                    {
                        index = 2;
                        break;
                    }
               
                default:
                    {
                        index = 0;
                        break;
                    }
            }
            return index;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}