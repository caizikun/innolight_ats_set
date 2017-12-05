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
    private string funcItemName = "CtrlSelfInfo";
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
   public ASPXAddNewFlowControl()
    {
        
        rowCount = 0;
        conn = "inpcsz0518\\ATS_HOME";
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        //pDataIO = new SqlManager(conn);
        mydt.Clear();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    { 
        //if (!IsPostBack)
        {
            IsSessionNull();
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
            FlowcontrolSelfInfor = (ASCXFlowControlSelfInfor)Page.LoadControl("../../Frame/TestPlan/FlowControlSelfInfor.ascx");
            ConfigControlTypeID(FlowcontrolSelfInfor);
            FlowcontrolSelfInfor.TH2Text = mydt.Columns[2].ColumnName;
            FlowcontrolSelfInfor.Colum2TextConfig = "";

            FlowcontrolSelfInfor.TH4Text = mydt.Columns[4].ColumnName;
            FlowcontrolSelfInfor.Colum4TextConfig = "";

            FlowcontrolSelfInfor.TH5Text = mydt.Columns[5].ColumnName;
            FlowcontrolSelfInfor.Colum5TextConfig = "";

            FlowcontrolSelfInfor.TH6Text = mydt.Columns[6].ColumnName;
            FlowcontrolSelfInfor.Colum6TextConfig = "";

            FlowcontrolSelfInfor.TH7Text = mydt.Columns[7].ColumnName;
            FlowcontrolSelfInfor.Colum7TextConfig = "";

            FlowcontrolSelfInfor.TH8Text = mydt.Columns[8].ColumnName;
            FlowcontrolSelfInfor.Colum8TextConfig = "";
            FlowcontrolSelfInfor.TH9Text = mydt.Columns[9].ColumnName;
            FlowcontrolSelfInfor.ConfigSeletedCtrolType = -1;
            FlowcontrolSelfInfor.TH10Text = mydt.Columns[10].ColumnName;
            FlowcontrolSelfInfor.Colum10TextConfig = "";
            FlowcontrolSelfInfor.TH11Text = mydt.Columns[11].ColumnName;
            FlowcontrolSelfInfor.Colum11TextConfig = "";
            FlowcontrolSelfInfor.TH12Text = mydt.Columns[12].ColumnName;
            FlowcontrolSelfInfor.Colum12TextConfig = "";
            FlowcontrolSelfInfor.TH13Text = mydt.Columns[13].ColumnName;
           
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
                string parentItem = "AddNewItem";
                //string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();
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
            int result = pDataIO.UpdateWithProc("TopoTestControl", inserTable, updataStr, logTracingString);
            if (result > 0)
            {
                inserTable.AcceptChanges();
                Response.Redirect("~/WebFiles/TestPlan/FlowControlList.aspx?uId=" + moduleTypeID.Trim());
            }
            else
            {
                //pDataIO.AlertMsgShow("Update data fail!",Request.Url.ToString());

                this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('Update data fail！\\nPlease input another ItemName.');", true);
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
            Response.Redirect("~/WebFiles/TestPlan/FlowControlList.aspx?uId=" + moduleTypeID.Trim());
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