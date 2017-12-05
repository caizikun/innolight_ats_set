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
public partial class ASPXTopPNSpecSeflInfor : BasePage
{
    string funcItemName = "TopPNSpecInfor";
    ASCXOptionButtons UserOptionButton;
    private SortedList<int, int> GlobalPNSpecIDMap = new SortedList<int, int>();
    private SortedList<int, string> GlobalPNSpecIDMap1 = new SortedList<int, string>();
    private SortedList<int, string> GlobalDDIDUnitMap = new SortedList<int, string>();
    private SortedList<byte, byte> PNChannelsIDMap = new SortedList<byte, byte>();
    ASCXPNSpecInfor[] PNSpecSelfInforList;
    ASCXPNSpecInfor PNSpecInfor;
    DropDownList ddlist = new DropDownList();
    private string conn;
    private DataIO pDataIO;
    public DataTable mydt = new DataTable();
    public DataTable mydtGlobalSpces = new DataTable();
    private string moduleTypeID = "";
    private string channelNumber = "";
    private int rowCount;
    private int columCount;
    private bool AddNew;
    
    private string logTracingString = "";
    public ASPXTopPNSpecSeflInfor()
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
        GlobalPNSpecIDMap.Clear();
        PNChannelsIDMap.Clear();
    }
    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();
            SetSessionBlockType(1);
            moduleTypeID = Request["uId"];
            if (Session["ChannelNumber"] == null)
            {
                channelNumber = "0";
            }
            else
            {
               channelNumber = Convert.ToString(Session["ChannelNumber"]); ;
            }
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

        PNSpecSelfInforList = new ASCXPNSpecInfor[rowCount];
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
            for (byte i = 0; i < PNSpecSelfInforList.Length; i++)
            {
                PNSpecSelfInforList[i] = (ASCXPNSpecInfor)Page.LoadControl("../../Frame/TestPlan/PNSpecInfor.ascx");
                PNSpecSelfInforList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                
                
                PNSpecSelfInforList[i].ClearDropDownList();
                PNSpecsIDtoName(PNSpecSelfInforList[i]);
                ConfigChannelMap(PNSpecSelfInforList[i]);
                //PNSpecSelfInforList[i].SetDropDownList = ddlist;
                int temp = ddlist.Items.Count;
                PNSpecSelfInforList[i].ConfigSeletedIndex = GlobalPNSpecIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["SID"]));
                PNSpecSelfInforList[i].ConfigSeletedIndexChannel = PNChannelsIDMap.IndexOfValue(Convert.ToByte(mydt.Rows[i]["Channel"]));
                PNSpecSelfInforList[i].TH2Text = mydt.Columns[2].ColumnName;
                PNSpecSelfInforList[i].TH3Text = mydt.Columns[3].ColumnName;
                PNSpecSelfInforList[i].TH4Text = mydt.Columns[4].ColumnName;
                PNSpecSelfInforList[i].TH5Text = mydt.Columns[5].ColumnName;
                PNSpecSelfInforList[i].TH7Text = mydt.Columns[6].ColumnName;
                PNSpecSelfInforList[i].Colum3TextConfig = mydt.Rows[i]["Typical"].ToString().Trim();
                PNSpecSelfInforList[i].Colum4TextConfig = mydt.Rows[i]["SpecMin"].ToString().Trim();
                PNSpecSelfInforList[i].Colum5TextConfig = mydt.Rows[i]["SpecMax"].ToString().Trim();
                PNSpecSelfInforList[i].Colum6TextConfig = GlobalDDIDUnitMap[PNSpecSelfInforList[i].ConfigSeletedIndex];
                PNSpecSelfInforList[i].EnableColum2Text = false;
                PNSpecSelfInforList[i].EnableColum3Text = false;
                PNSpecSelfInforList[i].EnableColum4Text = false;
                PNSpecSelfInforList[i].EnableColum5Text = false;
                PNSpecSelfInforList[i].EnableColum7Text = false;
                this.PNSpecSelfInfor.Controls.Add(PNSpecSelfInforList[i]);
               
            }
        }

        
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoPNSpecsParams where ID=" + moduleTypeID, "TopoPNSpecsParams");

                //if (AddNew)
                //{
                //    mydtGlobalSpces = pDataIO.GetDataTable("select * from GlobalSpecs where ID NOT IN (select SID from TopoPNSpecsParams where PID=" + moduleTypeID + ")", "GlobalSpecs");
                //}
                //else
                {
                    mydtGlobalSpces = pDataIO.GetDataTable("select * from GlobalSpecs", "GlobalSpecs");
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
                      parentItem = pDataIO.getDbCmdExecuteScalar("select SID from TopoPNSpecsParams where id = " + moduleTypeID).ToString();
                      parentItem = GlobalPNSpecIDMap1[Convert.ToInt32(parentItem)];   
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
                Response.Redirect("~/WebFiles/TestPlan/TopPNSpecList.aspx?uId=" + moduleTypeID.Trim());
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
        string updataStr = "select * from TopoPNSpecsParams";
       
        //ConfigOptionButtonsVisible();
        try
        {
            if (AddNew)
            {
                //ConfigRangeValidator(PNSpecInfor);
                if (!IsRangeValidator())
                {
                    return false;
                }
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "TopoPNSpecsParams");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = GlobalPNSpecIDMap[PNSpecInfor.ConfigSeletedIndex];
                dr[3] = PNSpecInfor.Colum3TextConfig;
                dr[4] = PNSpecInfor.Colum4TextConfig;
                dr[5] = PNSpecInfor.Colum5TextConfig;
                dr[6] = PNChannelsIDMap[(byte)PNSpecInfor.ConfigSeletedIndexChannel]; 
                inserTable.Rows.Add(dr);               
                int result = pDataIO.UpdateWithProc("TopoPNSpecsParams", inserTable, updataStr, logTracingString);
                if (result > 0)
                {
                    inserTable.AcceptChanges();
                    Response.Redirect("~/WebFiles/TestPlan/TopPNSpecList.aspx?uId=" + moduleTypeID.Trim());
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!",Request.Url.ToString());
                }

                

                return true;
               
            } 
            else
            {
                //ConfigRangeValidator(PNSpecSelfInforList[0]);
                if (!IsRangeValidator())
                {
                    PNSpecSelfInforList[0].EnableColum2Text = false;
                    PNSpecSelfInforList[0].EnableColum3Text = false;
                    PNSpecSelfInforList[0].EnableColum4Text = false;
                    PNSpecSelfInforList[0].EnableColum5Text = false;
                    PNSpecSelfInforList[0].EnableColum7Text = false;
                    return false;
                }
                if (rowCount != 1)
                {
                    return false;
                }
                else
                {
                    for (byte j = 0; j < PNSpecSelfInforList.Length; j++)
                    {                        
                        mydt.Rows[0]["SID"] = GlobalPNSpecIDMap[PNSpecSelfInforList[j].ConfigSeletedIndex];
                        mydt.Rows[0]["Typical"] = PNSpecSelfInforList[j].Colum3TextConfig;
                        mydt.Rows[0]["SpecMin"] = PNSpecSelfInforList[j].Colum4TextConfig;
                        mydt.Rows[0]["SpecMax"] = PNSpecSelfInforList[j].Colum5TextConfig;
                        mydt.Rows[0]["Channel"] = PNChannelsIDMap[(byte)PNSpecSelfInforList[j].ConfigSeletedIndexChannel]; ;
                    }
                }
                int result = pDataIO.UpdateWithProc("TopoPNSpecsParams", mydt, updataStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!");
                }               
                for (byte i = 0; i < PNSpecSelfInforList.Length; i++)
                {
                    PNSpecSelfInforList[i].EnableColum2Text = false;
                    PNSpecSelfInforList[i].EnableColum3Text = false;
                    PNSpecSelfInforList[i].EnableColum4Text = false;
                    PNSpecSelfInforList[i].EnableColum5Text = false;
                    PNSpecSelfInforList[i].EnableColum7Text = false;
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
           for (byte i = 0; i < PNSpecSelfInforList.Length; i++)
           {
               PNSpecSelfInforList[i].EnableColum3Text = true;
               PNSpecSelfInforList[i].EnableColum4Text = true;
               PNSpecSelfInforList[i].EnableColum5Text = true;
               PNSpecSelfInforList[i].EnableColum2Text = false;
               PNSpecSelfInforList[i].EnableColum7Text = true;
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
            bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);
            if (editVisible)
            {
                OptionButtons1.ConfigBtEditVisible = true;
            } 
            else
            {
                OptionButtons1.ConfigBtEditVisible = GetTestPlanAuthority();
            }
            
            OptionButtons1.ConfigBtCancelVisible = false;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        PNSpecInfor = new ASCXPNSpecInfor();
        try
        {


            {
                PNSpecInfor = (ASCXPNSpecInfor)Page.LoadControl("../../Frame/TestPlan/PNSpecInfor.ascx");
                PNSpecInfor.Colum2TextConfig = "";
                PNSpecInfor.Colum3TextConfig = "";
                PNSpecInfor.EnableColum3Text = true;
                PNSpecInfor.EnableColum2Text = true;
                PNSpecInfor.EnableColum4Text = true;
                PNSpecInfor.EnableColum5Text = true;
                PNSpecInfor.EnableColum7Text = true;
                PNSpecInfor.ClearDropDownList();
                PNSpecsIDtoName(PNSpecInfor);
                ConfigChannelMap(PNSpecInfor);
                PNSpecInfor.ConfigSeletedIndex = -1;
                PNSpecInfor.TH2Text = mydt.Columns[2].ColumnName;
                PNSpecInfor.TH3Text = mydt.Columns[3].ColumnName;
                PNSpecInfor.TH4Text = mydt.Columns[4].ColumnName;
                PNSpecInfor.TH5Text = mydt.Columns[5].ColumnName;
                PNSpecInfor.TH7Text = mydt.Columns[6].ColumnName;
                PNSpecInfor.ConfigSeletedIndexChannel = -1;
                PNSpecInfor.Colum6TextConfig = GlobalDDIDUnitMap[PNSpecInfor.ConfigSeletedIndex];
                this.PNSpecSelfInfor.Controls.Add(PNSpecInfor);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool PNSpecsIDtoName(ASCXPNSpecInfor input)
    {
        GlobalPNSpecIDMap.Clear();
        GlobalPNSpecIDMap1.Clear();
        GlobalDDIDUnitMap.Clear();
        ddlist.Items.Clear();
        try
        {
            for (int i = 0; i < mydtGlobalSpces.Rows.Count; i++)
            {
                input.InsertColum2Text(i, new ListItem(mydtGlobalSpces.Rows[i]["ItemName"].ToString().Trim()));

                GlobalPNSpecIDMap.Add(i, Convert.ToInt32(mydtGlobalSpces.Rows[i]["ID"]));
                GlobalPNSpecIDMap1.Add(Convert.ToInt32(mydtGlobalSpces.Rows[i]["ID"]),mydtGlobalSpces.Rows[i]["ItemName"].ToString());
                GlobalDDIDUnitMap.Add(i, mydtGlobalSpces.Rows[i]["Unit"].ToString());
            }

            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool ConfigRangeValidator(ASCXPNSpecInfor input)
    {

        {
            input.ConfigRangeValidatorMaxMin = input.Colum4TextConfig;
            input.ConfigRangeValidatorMinMax = input.Colum5TextConfig;
            input.ConfigRangeValidatorTypicalMax = input.Colum5TextConfig;
            input.ConfigRangeValidatorTypicalMin = input.Colum4TextConfig;
        }
      
        return true;
    }
    public bool IsRangeValidator()
    {
        if (AddNew)
        {
            if (Convert.ToDouble(PNSpecInfor.Colum3TextConfig)>Convert.ToDouble(PNSpecInfor.Colum5TextConfig)||
                Convert.ToDouble(PNSpecInfor.Colum3TextConfig) < Convert.ToDouble(PNSpecInfor.Colum4TextConfig)||
                Convert.ToDouble(PNSpecInfor.Colum5TextConfig) < Convert.ToDouble(PNSpecInfor.Colum4TextConfig)||
                Convert.ToDouble(PNSpecInfor.Colum4TextConfig) > Convert.ToDouble(PNSpecInfor.Colum5TextConfig))
            {
                this.Page.RegisterStartupScript("", "<script>alert('please make sure TypicalValue between Specmin and SpecMax and Specmin less than Specmax greater than Specmin ！');</script>");
                return false;
            }
        }
        else
        {
            if (rowCount != 1)
            {
                return false;
            }
            if (Convert.ToDouble(PNSpecSelfInforList[0].Colum3TextConfig) > Convert.ToDouble(PNSpecSelfInforList[0].Colum5TextConfig) ||
                Convert.ToDouble(PNSpecSelfInforList[0].Colum3TextConfig) < Convert.ToDouble(PNSpecSelfInforList[0].Colum4TextConfig) ||
                Convert.ToDouble(PNSpecSelfInforList[0].Colum5TextConfig) < Convert.ToDouble(PNSpecSelfInforList[0].Colum4TextConfig) ||
                Convert.ToDouble(PNSpecSelfInforList[0].Colum4TextConfig) > Convert.ToDouble(PNSpecSelfInforList[0].Colum5TextConfig))
             {
                 this.Page.RegisterStartupScript("", "<script>alert('please make sure TypicalValue between Specmin and SpecMax and Specmin less than Specmax greater than Specmin ！');</script>");
                 return false;
             }
        }

        return true;
    }
    public bool GetSpceUnit(object obj, string prameter)
    {
         try
         {
             PNSpecInfor.Colum6TextConfig = GlobalDDIDUnitMap[PNSpecInfor.ConfigSeletedIndex];
             return true;
         }
         catch (System.Exception ex)
         {
             throw ex;
         }
    }
    public void ConfigChannelMap(ASCXPNSpecInfor input)
    {
        PNChannelsIDMap.Clear();
        try
        {
            if (channelNumber=="0")
            {
                input.InsertColum7Text(0, new ListItem(0.ToString().Trim()));
                PNChannelsIDMap.Add(0, 0);
            } 
            else
            {
                for (int i = 0; i < Convert.ToInt16(channelNumber)+1; i++)
                {
                    input.InsertColum7Text(i, new ListItem((i).ToString().Trim()));
                    PNChannelsIDMap.Add((byte)(i), (byte)(i));
                }
            }
           
        }
        catch (System.Exception ex)
        {
        	
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
                    DataTable planIDTable = pDataIO.GetDataTable("select * from TopoPNSpecsParams where ID=" + moduleTypeID, "TopoPNSpecsParams");
                    string planID = planIDTable.Rows[0]["PID"].ToString().Trim();
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + planID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {

                        tpAuthority = false;
                    }
                    else
                    {
                        if (temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                        else
                        {
                            tpAuthority = false;
                        }

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