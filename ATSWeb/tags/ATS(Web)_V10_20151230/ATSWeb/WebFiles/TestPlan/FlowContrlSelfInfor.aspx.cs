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
using System.Threading;
public partial class ASPXFlowContrlSelfInfor : BasePage
{
    string funcItemName = "CtrlSelfInfo";
    ASCXOptionButtons UserOptionButton;
    ASCXFlowControlSelfInfor[] FlowcontrolSelfInfor;
    private string conn;
    private DataIO pDataIO;
    public DataTable mydt = new DataTable();
    private string moduleTypeID = "";
    private int rowCount;
    private string logTracingString = "";
    private SortedList<int, int> ControlTypeIDMap = new SortedList<int, int>();
    public ASPXFlowContrlSelfInfor()
    {
         
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();
            SetSessionBlockType(1);
            string teme = Convert.ToString(sender) +"e="+ e.ToString();
            moduleTypeID = Request["uId"];
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();

        }
    }

    public void bindData()
    {

        FlowcontrolSelfInfor = new ASCXFlowControlSelfInfor[rowCount];
        for (byte i = 0; i < FlowcontrolSelfInfor.Length; i++)
        {
            FlowcontrolSelfInfor[i] = (ASCXFlowControlSelfInfor)Page.LoadControl("../../Frame/TestPlan/FlowControlSelfInfor.ascx");
            FlowcontrolSelfInfor[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            FlowcontrolSelfInfor[i].TH2Text = mydt.Columns[2].ColumnName;
            FlowcontrolSelfInfor[i].Colum2TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();

            FlowcontrolSelfInfor[i].TH4Text = mydt.Columns[4].ColumnName;
            FlowcontrolSelfInfor[i].Colum4TextConfig = mydt.Rows[i]["Channel"].ToString().Trim();

            FlowcontrolSelfInfor[i].TH5Text = mydt.Columns[5].ColumnName;
            FlowcontrolSelfInfor[i].Colum5TextConfig = mydt.Rows[i]["Temp"].ToString().Trim();

            FlowcontrolSelfInfor[i].TH6Text = mydt.Columns[6].ColumnName;
            FlowcontrolSelfInfor[i].Colum6TextConfig = mydt.Rows[i]["Vcc"].ToString().Trim();

            FlowcontrolSelfInfor[i].TH7Text = mydt.Columns[7].ColumnName;
            FlowcontrolSelfInfor[i].Colum7TextConfig = mydt.Rows[i]["Pattent"].ToString().Trim();

            FlowcontrolSelfInfor[i].TH8Text = mydt.Columns[8].ColumnName;
            FlowcontrolSelfInfor[i].Colum8TextConfig = mydt.Rows[i]["DataRate"].ToString().Trim();
            FlowcontrolSelfInfor[i].ClearDropDownList();
            ConfigControlTypeID(FlowcontrolSelfInfor[i]);
            FlowcontrolSelfInfor[i].TH9Text = mydt.Columns[9].ColumnName;
            FlowcontrolSelfInfor[i].ConfigSeletedCtrolType = ControlTypeIDMap.IndexOfValue(Convert.ToInt32(mydt.Rows[i]["CtrlType"]));
               
            FlowcontrolSelfInfor[i].TH10Text = mydt.Columns[10].ColumnName;
            FlowcontrolSelfInfor[i].Colum10TextConfig = mydt.Rows[i]["TempOffset"].ToString().Trim();
            FlowcontrolSelfInfor[i].TH11Text = mydt.Columns[11].ColumnName;
            FlowcontrolSelfInfor[i].Colum11TextConfig = mydt.Rows[i]["TempWaitTimes"].ToString().Trim();
            FlowcontrolSelfInfor[i].TH12Text = mydt.Columns[12].ColumnName;
            FlowcontrolSelfInfor[i].Colum12TextConfig = mydt.Rows[i]["ItemDescription"].ToString().Trim();
            FlowcontrolSelfInfor[i].TH13Text = mydt.Columns[13].ColumnName;

            FlowcontrolSelfInfor[i].EnableColum13Text = false;
            FlowcontrolSelfInfor[i].EnableColum12Text = false;
            FlowcontrolSelfInfor[i].EnableColum11Text = false;
            FlowcontrolSelfInfor[i].EnableColum10Text = false;
            FlowcontrolSelfInfor[i].EnableColum9Text = false;
            FlowcontrolSelfInfor[i].EnableColum8Text = false;
            FlowcontrolSelfInfor[i].EnableColum7Text = false;
            FlowcontrolSelfInfor[i].EnableColum6Text = false;
            FlowcontrolSelfInfor[i].EnableColum5Text = false;
            FlowcontrolSelfInfor[i].EnableColum4Text = false;
            FlowcontrolSelfInfor[i].EnableColum2Text = false;
            FlowcontrolSelfInfor[i].TH10Text = mydt.Columns[10].ColumnName;
            if (mydt.Rows[i]["IgnoreFlag"].ToString().ToUpper().Trim() == "TRUE")
            {
                FlowcontrolSelfInfor[i].Colum13TextSelected = 1;
            }
            else
            {
                FlowcontrolSelfInfor[i].Colum13TextSelected = 0;
            }


            this.FlowControSelfInfor.Controls.Add(FlowcontrolSelfInfor[i]);
        }

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoTestControl where ID=" + moduleTypeID, "TopoTestControl");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestControl where id = " + moduleTypeID).ToString();
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
            Response.Redirect(Request.Url.ToString());
            return true;
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
            for (byte i = 0; i < FlowcontrolSelfInfor.Length; i++)
            {
                FlowcontrolSelfInfor[i].EnableColum13Text = true;
                FlowcontrolSelfInfor[i].EnableColum12Text = true;
                FlowcontrolSelfInfor[i].EnableColum11Text = true;
                FlowcontrolSelfInfor[i].EnableColum10Text = true;
                FlowcontrolSelfInfor[i].EnableColum9Text = true;
                FlowcontrolSelfInfor[i].EnableColum8Text = true;
                FlowcontrolSelfInfor[i].EnableColum7Text = true;
                FlowcontrolSelfInfor[i].EnableColum6Text = true;
                FlowcontrolSelfInfor[i].EnableColum5Text = true;
                FlowcontrolSelfInfor[i].EnableColum4Text = true;
                FlowcontrolSelfInfor[i].EnableColum2Text = true;
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
   public bool SaveData(object obj, string prameter)
    {       
       string updataStr="select * from TopoTestControl where ID=" + moduleTypeID;
       try
       {
           for (byte i = 0; i < FlowcontrolSelfInfor.Length; i++)
           {
               mydt.Rows[i]["ItemName"]= FlowcontrolSelfInfor[i].Colum2TextConfig;
               mydt.Rows[i]["Channel"] = FlowcontrolSelfInfor[i].Colum4TextConfig;
               mydt.Rows[i]["Temp"] = FlowcontrolSelfInfor[i].Colum5TextConfig;
               mydt.Rows[i]["Vcc"] = FlowcontrolSelfInfor[i].Colum6TextConfig;
               mydt.Rows[i]["Pattent"] = FlowcontrolSelfInfor[i].Colum7TextConfig;
               mydt.Rows[i]["DataRate"]=FlowcontrolSelfInfor[i].Colum8TextConfig;
               mydt.Rows[i]["CtrlType"] = ControlTypeIDMap[FlowcontrolSelfInfor[i].ConfigSeletedCtrolType];
               mydt.Rows[i]["TempOffset"] = FlowcontrolSelfInfor[i].Colum10TextConfig;
               mydt.Rows[i]["TempWaitTimes"] = FlowcontrolSelfInfor[i].Colum11TextConfig;
               mydt.Rows[i]["ItemDescription"] = FlowcontrolSelfInfor[i].Colum12TextConfig;
               mydt.Rows[i]["IgnoreFlag"] = FlowcontrolSelfInfor[i].Colum13TextSelected;
               
           }
           int result = pDataIO.UpdateWithProc("TopoTestControl", mydt, updataStr, logTracingString);
           if (result > 0)
           {
               mydt.AcceptChanges();
           }
           else
           {
               pDataIO.AlertMsgShow("Update data fail!");
           }
           for (byte i = 0; i < FlowcontrolSelfInfor.Length; i++)
           {
               FlowcontrolSelfInfor[i].EnableColum13Text = false;
               FlowcontrolSelfInfor[i].EnableColum12Text = false;
               FlowcontrolSelfInfor[i].EnableColum11Text = false;
               FlowcontrolSelfInfor[i].EnableColum10Text = false;
               FlowcontrolSelfInfor[i].EnableColum9Text = false;
               FlowcontrolSelfInfor[i].EnableColum8Text = false;
               FlowcontrolSelfInfor[i].EnableColum7Text = false;
               FlowcontrolSelfInfor[i].EnableColum6Text = false;
               FlowcontrolSelfInfor[i].EnableColum5Text = false;
               FlowcontrolSelfInfor[i].EnableColum4Text = false;
               FlowcontrolSelfInfor[i].EnableColum2Text = false;
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
       int myAccessCode =0;
       if (Session["AccCode"] != null)
       {
           myAccessCode = Convert.ToInt32(Session["AccCode"]);
       }
       CommCtrl mCommCtrl = new CommCtrl();

       OptionButtons1.ConfigBtSaveVisible = false;
       OptionButtons1.ConfigBtAddVisible = false;
       bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);
       if (editVisible)
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
   public bool ConfigControlTypeID(ASCXFlowControlSelfInfor input)
   {
       ControlTypeIDMap.Clear();
       try
       {

           input.InsertColum9Text(0, new ListItem("LP"));
           input.InsertColum9Text(1, new ListItem("FMT"));
           input.InsertColum9Text(2, new ListItem("LP&FMT"));
           ControlTypeIDMap.Add(0, 1);
           ControlTypeIDMap.Add(1, 2);
           ControlTypeIDMap.Add(2, 3);
          

        
           return true;
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
                   DataTable planIDTable = pDataIO.GetDataTable("select PID from TopoTestControl where ID=" + moduleTypeID, "TopoTestControl");
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