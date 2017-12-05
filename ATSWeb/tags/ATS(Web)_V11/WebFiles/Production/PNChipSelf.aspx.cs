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
public partial class WebFiles_Production_PNChipSelf : BasePage
{
     string funcItemName = "PNChipSelfInfor";
    ASCXOptionButtons UserOptionButton;
    Frame_Production_PNChipSelfInfor[] PNChipSelfInforList;
    Frame_Production_PNChipSelfInfor pnchipSelf;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   private string moduleTypeID = "";
    private int rowCount;
    private int columCount;
    private bool AddNew;
    private string logTracingString = "";
    private SortedList<int, int> ChipIDMap = new SortedList<int, int>();
    public DataTable mydtChip = new DataTable();
    public WebFiles_Production_PNChipSelf()
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
        ChipIDMap.Clear();
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
        PNChipSelfInforList = new Frame_Production_PNChipSelfInfor[rowCount];
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
            for (byte i = 0; i < PNChipSelfInforList.Length; i++)
            {
                PNChipSelfInforList[i] = (Frame_Production_PNChipSelfInfor)Page.LoadControl("../../Frame/Production/PNChipSelfInfor.ascx");
                PNChipSelfInforList[i].ID = mydt.Rows[0]["ID"].ToString().Trim();
                PNChipSelfInforList[i].ClearDropDownList();
                ConfigChipID(PNChipSelfInforList[i]);
                PNChipSelfInforList[i].Colum1TextSelected = ChipIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["ChipID"]));
                byte tempDriverType = 0;
                if (Convert.ToDouble(mydt.Rows[i]["ChipRoleID"]) > 3 || Convert.ToDouble(mydt.Rows[i]["ChipRoleID"]) < 0)
                {
                    tempDriverType = 0;
                }
                else
                {
                    tempDriverType = Convert.ToByte(mydt.Rows[i]["ChipRoleID"]);
                }

                PNChipSelfInforList[i].Colum2TextSelected = tempDriverType;              
              
             

                //PNChipSelfInforList[i].TH8Text = mydt.Columns[8].ColumnName;
                if (mydt.Rows[i]["ChipDirection"].ToString().ToUpper().Trim() == "0")
                {
                    PNChipSelfInforList[i].Colum3TextSelected = 0;
                }
                else if (mydt.Rows[i]["ChipDirection"].ToString().ToUpper().Trim() == "1")
                {
                    PNChipSelfInforList[i].Colum3TextSelected = 1;
                }
                else if (mydt.Rows[i]["ChipDirection"].ToString().ToUpper().Trim() == "2")
                {
                    PNChipSelfInforList[i].Colum3TextSelected = 2;
                }
                PNChipSelfInforList[i].EnableColum1Text = false;
                PNChipSelfInforList[i].EnableColum2Text = false;
                PNChipSelfInforList[i].EnableColum3Text = false;
               
                
                this.PNChipSelfInfor.Controls.Add(PNChipSelfInforList[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from PNChipMap where ID=" + moduleTypeID, "PNChipMap");
                mydtChip = pDataIO.GetDataTable("select * from ChipBaseInfo", "ChipBaseInfo");
                rowCount = mydt.Rows.Count;
               
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "AddNewItem";
                }
                else
                {
                    parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from ChipBaseInfo where id = " + mydt.Rows[0]["ChipID"]).ToString();
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
        string updataStr = "select * from PNChipMap where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "PNChipMap");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = ChipIDMap[pnchipSelf.Colum1TextSelected];
                dr[3] = pnchipSelf.Colum2TextSelected;
                dr[4] = pnchipSelf.Colum3TextSelected;
               
                  inserTable.Rows.Add(dr);
                  int result = pDataIO.UpdateWithProc("PNChipMap", inserTable, updataStr, logTracingString);
                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/Production/PNChipList.aspx?uId=" + moduleTypeID.Trim());
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
                     
                                for (byte j = 0; j < PNChipSelfInforList.Length; j++)
                                {
                                    mydt.Rows[0]["ChipID"] = ChipIDMap[PNChipSelfInforList[j].Colum1TextSelected];
                                    mydt.Rows[0]["ChipRoleID"] = PNChipSelfInforList[j].Colum2TextSelected;
                                    mydt.Rows[0]["ChipDirection"] = PNChipSelfInforList[j].Colum3TextSelected;
                                    
                                }
                                int result = pDataIO.UpdateWithProc("PNChipMap", mydt, updataStr, logTracingString);
                                if (result > 0)
                                {
                                    mydt.AcceptChanges();
                                }
                                else
                                {
                                    pDataIO.AlertMsgShow("Update data fail!");
                                }
                                for (byte i = 0; i < PNChipSelfInforList.Length; i++)
                        {
                            PNChipSelfInforList[i].EnableColum2Text = false;
                            PNChipSelfInforList[i].EnableColum3Text = false;
                            PNChipSelfInforList[i].EnableColum1Text = false;
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
           for (int i = 0; i < PNChipSelfInforList.Length;i++)
           {
               PNChipSelfInforList[i].EnableColum2Text = true;
               PNChipSelfInforList[i].EnableColum3Text = true;
               PNChipSelfInforList[i].EnableColum1Text = true;
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
                Response.Redirect("~/WebFiles/Production/PNChipList.aspx?uId=" + moduleTypeID.Trim());
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
            bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.MofifyProduction, myAccessCode);
           
            if (editVisible)
            {
                OptionButtons1.ConfigBtEditVisible = true;
            }
            else
            {
                OptionButtons1.ConfigBtEditVisible = PNAuthority();
            } 
            OptionButtons1.ConfigBtSaveVisible = false;        
                   
            OptionButtons1.ConfigBtCancelVisible = false;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        pnchipSelf = new Frame_Production_PNChipSelfInfor();
        try
        {


            {
                pnchipSelf = (Frame_Production_PNChipSelfInfor)Page.LoadControl("../../Frame/Production/PNChipSelfInfor.ascx");
                pnchipSelf.ClearDropDownList();
                ConfigChipID(pnchipSelf);
               
                pnchipSelf.Colum1TextSelected = 0;
                pnchipSelf.Colum2TextSelected = 0;
                pnchipSelf.Colum3TextSelected = 0;
               
                pnchipSelf.EnableColum3Text = true;
                pnchipSelf.EnableColum2Text = true;
                pnchipSelf.EnableColum1Text = true;
                //chipsetControl.TH8Text = mydt.Columns[8].ColumnName;

                this.PNChipSelfInfor.Controls.Add(pnchipSelf);
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
                DataTable selectPNid = pDataIO.GetDataTable("select PID from GlobalManufactureChipsetControl where ID=" + moduleTypeID, "GlobalManufactureChipsetControl");
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
    public bool ConfigChipID(Frame_Production_PNChipSelfInfor input)
    {
        ChipIDMap.Clear();
        try
        {
            for (int i = 0; i < mydtChip.Rows.Count; i++)
            {
                input.InsertColum1Text(i, new ListItem(mydtChip.Rows[i]["ItemName"].ToString().Trim()));

                ChipIDMap.Add(i, Convert.ToInt32(mydtChip.Rows[i]["ID"]));
            }
            
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}