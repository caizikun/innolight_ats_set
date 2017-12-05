﻿using System;
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
    string funcItemName = "ChipsetINIInfor";
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
    public ASPXChipSetIniSelfInfor()
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
                ChipsetIniSelfInforlist[i] = (ASCXChipsetIniSelfInfor)Page.LoadControl("../../Frame/Production/ChipsetIniSelfInfor.ascx");
                ChipsetIniSelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().Trim();
                ChipsetIniSelfInforlist[i].TH2Text = mydt.Columns[2].ColumnName;
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
                ChipsetIniSelfInforlist[i].TH3Text = "DebugLine";
                ChipsetIniSelfInforlist[i].Colum3TextConfig = mydt.Rows[i]["ChipLine"].ToString().Trim();

                ChipsetIniSelfInforlist[i].TH4Text = mydt.Columns[4].ColumnName;
                ChipsetIniSelfInforlist[i].Colum4TextConfig = mydt.Rows[i]["RegisterAddress"].ToString().Trim();

                ChipsetIniSelfInforlist[i].TH5Text = mydt.Columns[5].ColumnName;
                ChipsetIniSelfInforlist[i].Colum5TextConfig = mydt.Rows[i]["Length"].ToString().Trim();

                ChipsetIniSelfInforlist[i].TH6Text = mydt.Columns[6].ColumnName;
                ChipsetIniSelfInforlist[i].Colum6TextConfig = mydt.Rows[i]["ItemValue"].ToString().Trim();
                
                if (mydt.Rows[i]["Endianness"].ToString().ToUpper().Trim() == "FALSE")
                {
                    ChipsetIniSelfInforlist[i].Colum7TextSelected = 0;
                }
                else
                {
                    ChipsetIniSelfInforlist[i].Colum7TextSelected = 1;
                }
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
                mydt = pDataIO.GetDataTable("select * from GlobalManufactureChipsetInitialize where ID=" + moduleTypeID, "GlobalManufactureChipsetInitialize");
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
                     parentItem = Request["uIndex"];
                     //parentItem = pDataIO.getDbCmdExecuteScalar("select ID from GlobalManufactureChipsetInitialize where id =" + moduleTypeID).ToString();
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
                Response.Redirect("~/WebFiles/Production/ChipSetIniList.aspx?uId=" + moduleTypeID.Trim());
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
        string updataStr = "select * from GlobalManufactureChipsetInitialize where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "GlobalManufactureChipsetInitialize");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = chipsetControl.Colum2TextSelected;
                dr[3] = chipsetControl.Colum3TextConfig;
                dr[4] = chipsetControl.Colum4TextConfig;
                dr[5] = chipsetControl.Colum5TextConfig;
                dr[6] = chipsetControl.Colum6TextConfig;
                dr[7] = chipsetControl.Colum7TextSelected;
                  inserTable.Rows.Add(dr);
                  int result = pDataIO.UpdateWithProc("GlobalManufactureChipsetInitialize", inserTable, updataStr, logTracingString);
                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/Production/ChipSetIniList.aspx?uId=" + moduleTypeID.Trim());
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

                    for (byte j = 0; j < ChipsetIniSelfInforlist.Length; j++)
                    {
                        mydt.Rows[0]["DriveType"] = ChipsetIniSelfInforlist[j].Colum2TextSelected;
                        mydt.Rows[0]["ChipLine"] = ChipsetIniSelfInforlist[j].Colum3TextConfig;
                        mydt.Rows[0]["RegisterAddress"] = ChipsetIniSelfInforlist[j].Colum4TextConfig;
                        mydt.Rows[0]["Length"] = ChipsetIniSelfInforlist[j].Colum5TextConfig;
                        mydt.Rows[0]["ItemValue"] = ChipsetIniSelfInforlist[j].Colum6TextConfig;
                        mydt.Rows[0]["Endianness"] = ChipsetIniSelfInforlist[j].Colum7TextSelected;

                    }
                    int result = pDataIO.UpdateWithProc("GlobalManufactureChipsetInitialize", mydt, updataStr, logTracingString);
                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("Update data fail!");
                    }
                    for (byte i = 0; i < ChipsetIniSelfInforlist.Length; i++)
                        {
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
            bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.MofifyProduction, myAccessCode);

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
        chipsetControl = new ASCXChipsetIniSelfInfor();
        try
        {


            {
                chipsetControl = (ASCXChipsetIniSelfInfor)Page.LoadControl("../../Frame/Production/ChipsetIniSelfInfor.ascx");
                chipsetControl.Colum2TextSelected=0;
                chipsetControl.Colum3TextConfig = "";
                chipsetControl.Colum4TextConfig = "";
                chipsetControl.Colum5TextConfig = "";
                chipsetControl.Colum6TextConfig = "";               
                chipsetControl.Colum7TextSelected = 0; ;
                
                chipsetControl.EnableColum7Text = true;
                chipsetControl.EnableColum6Text = true;
                chipsetControl.EnableColum5Text = true;
                chipsetControl.EnableColum4Text = true;
                chipsetControl.EnableColum3Text = true;
                chipsetControl.EnableColum2Text = true;
                chipsetControl.TH2Text = mydt.Columns[2].ColumnName;
                chipsetControl.TH3Text = "DebugLine";
                chipsetControl.TH4Text = mydt.Columns[4].ColumnName;
                chipsetControl.TH5Text = mydt.Columns[5].ColumnName;
                chipsetControl.TH6Text = mydt.Columns[6].ColumnName;
                //chipsetControl.TH7Text = mydt.Columns[7].ColumnName;
                

                this.ChipsetIniSelfInfor.Controls.Add(chipsetControl);
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
                DataTable selectPNid = pDataIO.GetDataTable("select PID from GlobalManufactureChipsetInitialize where ID=" + moduleTypeID, "GlobalManufactureChipsetInitialize");
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