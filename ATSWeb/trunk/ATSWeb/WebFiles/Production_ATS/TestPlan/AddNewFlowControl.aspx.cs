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
using System.Collections;
public partial class ASPXAddNewFlowControl : BasePage
{
    private string funcItemName = "流程控制信息";
    //private string funcItemName = "FlowCtrlList";
    private ASCXFlowControlSelfInfor FlowcontrolSelfInfor;
    private ASCXOptionButtons UserOptionButton;
   private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private string logTracingString = "";
   private SortedList<int, int> ControlTypeIDMap = new SortedList<int, int>();
    
    protected void Page_Load(object sender, EventArgs e)
    { 
        //if (!IsPostBack)
        {
            IsSessionNull();

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
            //pDataIO = new SqlManager(conn);
            mydt.Clear();

            Session["TreeNodeExpand"] = null;
            SetSessionBlockType(1);
            moduleTypeID = Request["uId"];
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }


    }

    public void bindData()
    {

        FlowcontrolSelfInfor = new ASCXFlowControlSelfInfor();       
        {
            FlowcontrolSelfInfor = (ASCXFlowControlSelfInfor)Page.LoadControl("~/Frame/TestPlan/FlowControlSelfInfor.ascx");
            ConfigControlTypeID(FlowcontrolSelfInfor);
            FlowcontrolSelfInfor.TH2Text = "名称";
            FlowcontrolSelfInfor.Colum2TextConfig = "";

            FlowcontrolSelfInfor.TH4Text = "通道";
            FlowcontrolSelfInfor.Colum4TextConfig = "";

            FlowcontrolSelfInfor.TH5Text = "温度";
            FlowcontrolSelfInfor.Colum5TextConfig = "";

            FlowcontrolSelfInfor.TH6Text = "电压";
            FlowcontrolSelfInfor.Colum6TextConfig = "";

            FlowcontrolSelfInfor.TH7Text = "码型";
            FlowcontrolSelfInfor.Colum7TextConfig = "";

            FlowcontrolSelfInfor.TH8Text = "速率";
            FlowcontrolSelfInfor.Colum8TextConfig = "";
            FlowcontrolSelfInfor.TH9Text = "流程类型";
            FlowcontrolSelfInfor.ConfigSeletedCtrolType = -1;
            FlowcontrolSelfInfor.TH10Text = "温度补偿(℃)";
            FlowcontrolSelfInfor.Colum10TextConfig = "";
            FlowcontrolSelfInfor.TH11Text = "温度等待时间(s)";
            FlowcontrolSelfInfor.Colum11TextConfig = "";
            FlowcontrolSelfInfor.TH12Text = "描述";
            FlowcontrolSelfInfor.Colum12TextConfig = "";
            FlowcontrolSelfInfor.TH13Text = "是否跳过";
           
            this.AddNewFlowControl.Controls.Add(FlowcontrolSelfInfor);
            FlowcontrolSelfInfor.EnableColum13Text = true;
            FlowcontrolSelfInfor.EnableColum12Text = true;
            FlowcontrolSelfInfor.EnableColum11Text = true;
            FlowcontrolSelfInfor.EnableColum10Text = true;
            FlowcontrolSelfInfor.EnableColum9Text = true;
            FlowcontrolSelfInfor.EnableColum8Text = true;
            FlowcontrolSelfInfor.EnableColum7Text = true;
            FlowcontrolSelfInfor.EnableColum6Text = true;
            FlowcontrolSelfInfor.EnableColum5Text = true;
            FlowcontrolSelfInfor.EnableColum4Text = true;
            FlowcontrolSelfInfor.EnableColum2Text = true;            
            FlowcontrolSelfInfor.Colum13TextSelected = -1;

            
        }

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoTestControl  where PID=" + moduleTypeID + "ORDER BY SEQ", "TopoTestControl");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = "添加新项";
                //string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();

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
    public bool EditData(object obj, string prameter)
    {
        try
        {
           
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool SaveData(object obj, string prameter)
    {
        string updataStr = "select * from TopoTestControl where ID=" + moduleTypeID;
        DataTable inserTable = pDataIO.GetDataTable(updataStr, "TopoTestControl");
        DataRow dr = inserTable.NewRow();
        try
        {
            {
                dr[0]=-1;
                dr[1] = moduleTypeID;
                dr[2] = FlowcontrolSelfInfor.Colum2TextConfig;
                if (rowCount==0)
                {
                    dr[3] = 1;
                }
                else
                {
                    dr[3] = Convert.ToString(Convert.ToInt64(mydt.Rows[rowCount - 1]["SEQ"]) + 1);
                }
               
                dr[4] = FlowcontrolSelfInfor.Colum4TextConfig;
                dr[5] = FlowcontrolSelfInfor.Colum5TextConfig;
                dr[6] = FlowcontrolSelfInfor.Colum6TextConfig;
                dr[7] = FlowcontrolSelfInfor.Colum7TextConfig;
                dr[8] = FlowcontrolSelfInfor.Colum8TextConfig;
                dr[9] = ControlTypeIDMap[FlowcontrolSelfInfor.ConfigSeletedCtrolType];                
                dr[10] = FlowcontrolSelfInfor.Colum10TextConfig;
                dr[11] = FlowcontrolSelfInfor.Colum11TextConfig;
                dr[12] = FlowcontrolSelfInfor.Colum12TextConfig;
                dr[13] = FlowcontrolSelfInfor.Colum13TextSelected;
               
            }

            inserTable.Rows.Add(dr);

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestControl", inserTable, updataStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoTestControl", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                inserTable.AcceptChanges();
                Response.Redirect("~/WebFiles/Production_ATS/TestPlan/FlowControlList.aspx?uId=" + moduleTypeID.Trim());
            }
            else
            {
                //pDataIO.AlertMsgShow("数据更新失败!",Request.Url.ToString());

                this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('数据更新失败！\\n请输入其他名称.');", true);
                FlowcontrolSelfInfor.Colum2TextConfig = "";

            }
            //pDataIO.UpdateDataTable(updataStr, inserTable);
            
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
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/FlowControlList.aspx?uId=" + moduleTypeID.Trim());
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }


    }
    public void ConfigOptionButtonsVisible()
    {
        OptionButtons1.ConfigBtSaveVisible = true;
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = true;
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
    
}