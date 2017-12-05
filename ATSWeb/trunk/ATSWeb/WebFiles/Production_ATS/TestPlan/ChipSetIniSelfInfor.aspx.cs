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
public partial class ASPXChipSetIniSelfInfor : BasePage
{
    string funcItemName = "芯片初始化信息(测试方案)";
    ASCXOptionButtons UserOptionButton;
    ASCXChipsetIniSelfInfor[] ChipsetIniSelfInforlist;
    ASCXChipsetIniSelfInfor chipsetControl;
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
        ChipsetIniSelfInforlist = new ASCXChipsetIniSelfInfor[rowCount];
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
            for (byte i = 0; i < ChipsetIniSelfInforlist.Length; i++)
            {
                ChipsetIniSelfInforlist[i] = (ASCXChipsetIniSelfInfor)Page.LoadControl("~/Frame/Production/ChipsetIniSelfInfor.ascx");
                //ChipsetIniSelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().Trim();
                ChipsetIniSelfInforlist[i].THnameText = "名称";
                ChipsetIniSelfInforlist[i].nameTextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();
                ChipsetIniSelfInforlist[i].TH2Text = "芯片类型";
                byte tempDriverType = 0;
                if (Convert.ToDouble(mydt.Rows[i]["DriveType"]) > byte.MaxValue || Convert.ToDouble(mydt.Rows[i]["DriveType"]) < byte.MinValue)
                {
                    tempDriverType = 0;
                }
                else
                {
                    tempDriverType = Convert.ToByte(mydt.Rows[i]["DriveType"]);
                }
                ChipsetIniSelfInforlist[i].Colum2TextSelected = tempDriverType;
                ChipsetIniSelfInforlist[i].TH3Text = "调试通道";
                ChipsetIniSelfInforlist[i].Colum3TextConfig = mydt.Rows[i]["ChipLine"].ToString().Trim();

                ChipsetIniSelfInforlist[i].TH4Text = "寄存器地址";
                ChipsetIniSelfInforlist[i].Colum4TextConfig = mydt.Rows[i]["RegisterAddress"].ToString().Trim();

                ChipsetIniSelfInforlist[i].TH5Text = "长度";
                ChipsetIniSelfInforlist[i].Colum5TextConfig = mydt.Rows[i]["Length"].ToString().Trim();

                ChipsetIniSelfInforlist[i].TH6Text = "值";
                ChipsetIniSelfInforlist[i].Colum6TextConfig = mydt.Rows[i]["ItemValue"].ToString().Trim();
                
                if (mydt.Rows[i]["Endianness"].ToString().ToUpper().Trim() == "FALSE")
                {
                    ChipsetIniSelfInforlist[i].Colum7TextSelected = 0;
                }
                else
                {
                    ChipsetIniSelfInforlist[i].Colum7TextSelected = 1;
                }
                ChipsetIniSelfInforlist[i].EnablenameText = false;
                ChipsetIniSelfInforlist[i].EnableColum2Text = false;
                ChipsetIniSelfInforlist[i].EnableColum3Text = false;
                ChipsetIniSelfInforlist[i].EnableColum4Text = false;
                ChipsetIniSelfInforlist[i].EnableColum5Text = false;
                ChipsetIniSelfInforlist[i].EnableColum6Text = false;
                ChipsetIniSelfInforlist[i].EnableColum7Text = false;
                
                this.ChipsetIniSelfInfor.Controls.Add(ChipsetIniSelfInforlist[i]);
            }
        }      
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoManufactureChipsetInitialize where ID=" + moduleTypeID, "TopoManufactureChipsetInitialize");
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
                     //parentItem = Request["uIndex"];
                    parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from TopoManufactureChipsetInitialize where id =" + moduleTypeID).ToString();
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
                Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ChipSetIniList.aspx?uId=" + moduleTypeID.Trim());
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
        string updataStr = "select * from TopoManufactureChipsetInitialize where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "TopoManufactureChipsetInitialize");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = chipsetControl.nameTextConfig;
                dr[3] = chipsetControl.Colum2TextSelected;
                dr[4] = chipsetControl.Colum3TextConfig;
                dr[5] = chipsetControl.Colum4TextConfig;
                dr[6] = chipsetControl.Colum5TextConfig;
                dr[7] = chipsetControl.Colum6TextConfig;
                dr[8] = chipsetControl.Colum7TextSelected;
                  inserTable.Rows.Add(dr);

                  int result = -1;
                  if (Session["DB"].ToString().ToUpper() == "ATSDB")
                  {
                      result = pDataIO.UpdateWithProc("TopoManufactureChipsetInitialize", inserTable, updataStr, logTracingString, "ATS_V2");
                  }
                  else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                  {
                      result = pDataIO.UpdateWithProc("TopoManufactureChipsetInitialize", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
                  }      

                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ChipSetIniList.aspx?uId=" + moduleTypeID.Trim());
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

                    for (byte j = 0; j < ChipsetIniSelfInforlist.Length; j++)
                    {
                        mydt.Rows[0]["ItemName"] = ChipsetIniSelfInforlist[j].nameTextConfig;
                        mydt.Rows[0]["DriveType"] = ChipsetIniSelfInforlist[j].Colum2TextSelected;
                        mydt.Rows[0]["ChipLine"] = ChipsetIniSelfInforlist[j].Colum3TextConfig;
                        mydt.Rows[0]["RegisterAddress"] = ChipsetIniSelfInforlist[j].Colum4TextConfig;
                        mydt.Rows[0]["Length"] = ChipsetIniSelfInforlist[j].Colum5TextConfig;
                        mydt.Rows[0]["ItemValue"] = ChipsetIniSelfInforlist[j].Colum6TextConfig;
                        mydt.Rows[0]["Endianness"] = ChipsetIniSelfInforlist[j].Colum7TextSelected;

                    }

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("TopoManufactureChipsetInitialize", mydt, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("TopoManufactureChipsetInitialize", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!");
                    }
                    for (byte i = 0; i < ChipsetIniSelfInforlist.Length; i++)
                    {
                        ChipsetIniSelfInforlist[i].EnablenameText = false;
                        ChipsetIniSelfInforlist[i].EnableColum2Text = false;
                        ChipsetIniSelfInforlist[i].EnableColum3Text = false;
                        ChipsetIniSelfInforlist[i].EnableColum4Text = false;
                        ChipsetIniSelfInforlist[i].EnableColum5Text = false;
                        ChipsetIniSelfInforlist[i].EnableColum6Text = false;
                        ChipsetIniSelfInforlist[i].EnableColum7Text = false;
                            
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
           for (int i = 0; i < ChipsetIniSelfInforlist.Length;i++)
           {
               ChipsetIniSelfInforlist[i].EnablenameText = true;
               ChipsetIniSelfInforlist[i].EnableColum2Text = true;
               ChipsetIniSelfInforlist[i].EnableColum3Text = true;
               ChipsetIniSelfInforlist[i].EnableColum4Text = true;
               ChipsetIniSelfInforlist[i].EnableColum5Text = true;
               ChipsetIniSelfInforlist[i].EnableColum6Text = true;
               ChipsetIniSelfInforlist[i].EnableColum7Text = true;
               
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
        int myAccessCode = 0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = false;
        bool editVidible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);

        if (editVidible)
        {
            OptionButtons1.ConfigBtEditVisible = true;
        }
        else
        {
            OptionButtons1.ConfigBtEditVisible = GetTestPlanAuthority();
        }

        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = false;        
    }
    public void ClearTextValue()
    {
        chipsetControl = new ASCXChipsetIniSelfInfor();
        try
        {
            {
                chipsetControl = (ASCXChipsetIniSelfInfor)Page.LoadControl("~/Frame/Production/ChipsetIniSelfInfor.ascx");
                chipsetControl.nameTextConfig = "";
                chipsetControl.Colum2TextSelected=0;
                chipsetControl.Colum3TextConfig = "";
                chipsetControl.Colum4TextConfig = "";
                chipsetControl.Colum5TextConfig = "";
                chipsetControl.Colum6TextConfig = "";               
                chipsetControl.Colum7TextSelected = 0; ;

                chipsetControl.EnablenameText = true;
                chipsetControl.EnableColum7Text = true;
                chipsetControl.EnableColum6Text = true;
                chipsetControl.EnableColum5Text = true;
                chipsetControl.EnableColum4Text = true;
                chipsetControl.EnableColum3Text = true;
                chipsetControl.EnableColum2Text = true;
                chipsetControl.THnameText = "名称";
                chipsetControl.TH2Text = "芯片类型";
                chipsetControl.TH3Text = "调试通道";
                chipsetControl.TH4Text = "寄存器地址";
                chipsetControl.TH5Text = "长度";
                chipsetControl.TH6Text = "值";
                //chipsetControl.TH7Text = mydt.Columns[7].ColumnName;
                

                this.ChipsetIniSelfInfor.Controls.Add(chipsetControl);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
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
                    DataTable planIDTable = pDataIO.GetDataTable("select PID from TopoManufactureChipsetInitialize where ID=" + moduleTypeID, "TopoManufactureChipsetInitialize");
                    string planID = planIDTable.Rows[0]["PID"].ToString().Trim();

                    DataTable pnId = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + planID, "TopoTestPlan");

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

                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + planID, "UserPlanAction");

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