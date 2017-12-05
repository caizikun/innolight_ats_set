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
public partial class WebFiles_Production_pnChannelMapSelfInfor : BasePage
{
    string funcItemName = "通道映射信息";
    ASCXOptionButtons UserOptionButton;
    Frame_Production_ChannelMapSelf[] ChannelMapSelfInforlist;
    Frame_Production_ChannelMapSelf channelMap;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   private string moduleTypeID = "";
    private int rowCount;
    private int columCount;
    private bool AddNew;
    private string logTracingString = "";
    private byte chipChannelCount = 0;
    private byte moduleChannelCount = 0;

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
        ChannelMapSelfInforlist = new Frame_Production_ChannelMapSelf[rowCount];
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
            for (byte i = 0; i < ChannelMapSelfInforlist.Length; i++)
            {
                ChannelMapSelfInforlist[i] = (Frame_Production_ChannelMapSelf)Page.LoadControl("~/Frame/Production/ChannelMapSelf.ascx");
                //ChannelMapSelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().Trim();
                ChannelMapSelfInforlist[i].ClearDropDownList();
                ConfigChipChannel(ChannelMapSelfInforlist[i]);
                double temp = Convert.ToDouble(mydt.Rows[i]["ModuleLine"]);
                double temp1 = Convert.ToDouble(mydt.Rows[i]["ChipLine"]);
                double temp2 = Convert.ToDouble(mydt.Rows[i]["DebugLine"]);
                if (temp < 0 || temp > moduleChannelCount)
                {
                    ChannelMapSelfInforlist[i].Colum1TextSelected = 0;
                }
                else
                {
                    ChannelMapSelfInforlist[i].Colum1TextSelected = Convert.ToByte(temp);
                    
                }
                if (temp1 < 0 || temp1 > chipChannelCount)
                {
                    ChannelMapSelfInforlist[i].Colum2TextSelected = 0;
                }
                else
                {
                    ChannelMapSelfInforlist[i].Colum2TextSelected = Convert.ToByte(temp1);
                }
                if (temp2 < 0 || temp2 > moduleChannelCount)
                {
                    ChannelMapSelfInforlist[i].Colum3TextSelected = 0;
                }
                else
                {
                    ChannelMapSelfInforlist[i].Colum3TextSelected = Convert.ToByte(temp2);
                }
                ChannelMapSelfInforlist[i].EnableColum3Text = false;
                ChannelMapSelfInforlist[i].EnableColum2Text = false;
                ChannelMapSelfInforlist[i].EnableColum1Text = false;
                this.ChnnelMAPSelfInfor.Controls.Add(ChannelMapSelfInforlist[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChannelMap where ID=" + moduleTypeID, "ChannelMap");
                rowCount = mydt.Rows.Count;
                if (rowCount == 1 && !AddNew)
                {
                    DataTable temp = pDataIO.GetDataTable("select * from PNChipMap where ID=" + mydt.Rows[0]["PNChipID"].ToString().Trim(), "PNChipMap");
                    DataTable temp1 = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + temp.Rows[0]["ChipID"].ToString().Trim(), "ChipBaseInfo");
                    DataTable temp2 = pDataIO.GetDataTable("select * from GlobalProductionName where ID=" + temp.Rows[0]["PNID"].ToString().Trim(), "GlobalProductionName");
                    chipChannelCount = Convert.ToByte(temp1.Rows[0]["Channels"]);
                    moduleChannelCount = Convert.ToByte(temp2.Rows[0]["Channels"]);
                }
                if (AddNew)
                {
                    DataTable temp = pDataIO.GetDataTable("select * from PNChipMap where ID=" + moduleTypeID, "PNChipMap");
                    DataTable temp1 = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + temp.Rows[0]["ChipID"].ToString().Trim(), "ChipBaseInfo");
                    DataTable temp2 = pDataIO.GetDataTable("select * from GlobalProductionName where ID=" + temp.Rows[0]["PNID"].ToString().Trim(), "GlobalProductionName");
                    chipChannelCount = Convert.ToByte(temp1.Rows[0]["Channels"]);
                    moduleChannelCount = Convert.ToByte(temp2.Rows[0]["Channels"]);
                    
                }
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "添加新项";
                }
                else
                {
                    parentItem ="通道映射";
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
        string updataStr = "select * from ChannelMap where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "ChannelMap");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = channelMap.ConfigColum1Text;
                dr[3] = channelMap.ConfigColum2Text;
                dr[4] = channelMap.ConfigColum3Text;
                  inserTable.Rows.Add(dr);

                  int result = -1;
                  if (Session["DB"].ToString().ToUpper() == "ATSDB")
                  {
                      result = pDataIO.UpdateWithProc("ChannelMap", inserTable, updataStr, logTracingString, "ATS_V2");
                  }
                  else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                  {
                      result = pDataIO.UpdateWithProc("ChannelMap", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
                  }      

                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/Production_ATS/Production/ChannelMapList.aspx?uId=" + moduleTypeID.Trim());
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
                     
                                for (byte j = 0; j < ChannelMapSelfInforlist.Length; j++)
                                {
                                    mydt.Rows[0]["ModuleLine"] = ChannelMapSelfInforlist[j].ConfigColum1Text;
                                    mydt.Rows[0]["ChipLine"] = ChannelMapSelfInforlist[j].ConfigColum2Text;
                                    mydt.Rows[0]["DebugLine"] = ChannelMapSelfInforlist[j].ConfigColum3Text;

                                }

                                int result = -1;
                                if (Session["DB"].ToString().ToUpper() == "ATSDB")
                                {
                                    result = pDataIO.UpdateWithProc("ChannelMap", mydt, updataStr, logTracingString, "ATS_V2");
                                }
                                else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                                {
                                    result = pDataIO.UpdateWithProc("ChannelMap", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
                                }      

                                if (result > 0)
                                {
                                    mydt.AcceptChanges();
                                }
                                else
                                {
                                    pDataIO.AlertMsgShow("数据更新失败!");
                                }
                                for (byte i = 0; i < ChannelMapSelfInforlist.Length; i++)
                        {
                            ChannelMapSelfInforlist[i].EnableColum2Text = false;
                            ChannelMapSelfInforlist[i].EnableColum1Text = false;
                            ChannelMapSelfInforlist[i].EnableColum3Text = false;
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
           for (int i = 0; i < ChannelMapSelfInforlist.Length;i++)
           {
               ChannelMapSelfInforlist[i].EnableColum2Text = true;
               ChannelMapSelfInforlist[i].EnableColum1Text = true;
               ChannelMapSelfInforlist[i].EnableColum3Text = true;
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
                Response.Redirect("~/WebFiles/Production_ATS/Production/ChannelMapList.aspx?uId=" + moduleTypeID.Trim());
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
            bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);
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
        channelMap = new Frame_Production_ChannelMapSelf();
        try
        {


            {
                channelMap = (Frame_Production_ChannelMapSelf)Page.LoadControl("~/Frame/Production/ChannelMapSelf.ascx");
                channelMap.ClearDropDownList();
                ConfigChipChannel(channelMap);
                channelMap.ConfigColum2Text = "";
                channelMap.ConfigColum1Text = "";
                channelMap.ConfigColum3Text = "";

                channelMap.EnableColum1Text = true;
                channelMap.EnableColum2Text = true;
                channelMap.EnableColum3Text = true;
              
                //ChannelMapSelfInforlist.TH8Text = mydt.Columns[8].ColumnName;

                this.ChnnelMAPSelfInfor.Controls.Add(channelMap);
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
                DataTable selectPNid = pDataIO.GetDataTable("SELECT * FROM PNChipMap,ChannelMap where PNChipMap.ID = ChannelMap.PNChipID and ChannelMap.ID =" + moduleTypeID, "ChannelMap");
                string StrPNId = selectPNid.Rows[0]["PNID"].ToString().Trim();
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
    public bool ConfigChipChannel(Frame_Production_ChannelMapSelf input)
    {

        try
        {
            for (int i = 0; i < chipChannelCount+1; i++)
            {
                input.InsertColum2Text(i, new ListItem(Convert.ToString(i)));

            }
            for (int i = 0; i < moduleChannelCount+1; i++)
            {
                input.InsertColum1Text(i, new ListItem(Convert.ToString(i)));

            }
            for (int i = 0; i < moduleChannelCount+1; i++)
            {
                input.InsertColum3Text(i, new ListItem(Convert.ToString(i)));

            }
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}