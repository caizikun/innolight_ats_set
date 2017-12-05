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
public partial class ASPXAddNewMConfigInit : BasePage
{
    string funcItemName = "模块配置信息";
    //string funcItemName = "MConfigInitList";
    ASCXOptionButtons UserOptionButton;
    ASCXMConfigInitSelfInfor[]MConfigInitSelfInfor;
    private string conn;
    private DataIO pDataIO;
    public DataTable mydt = new DataTable();
    private string moduleTypeID = "";
    private int rowCount;
    private int columCount;
    private string logTracingString = "";

    protected void Page_Load(object sender, EventArgs e)
    { 
        //if (!IsPostBack)
        {
            IsSessionNull();

            columCount = 0;
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
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }


    }

    public void bindData()
    {
        MConfigInitSelfInfor = new ASCXMConfigInitSelfInfor[columCount];
        for (byte i = 2; i < MConfigInitSelfInfor.Length; i++)
        {
            MConfigInitSelfInfor[i] = (ASCXMConfigInitSelfInfor)Page.LoadControl("~/Frame/TestPlan/MConfigInitSelfInfor.ascx");
            //MConfigInitSelfInfor[i].ID = i.ToString().Trim();            
            if (i == 2)
            {
                MConfigInitSelfInfor[i].ConfigFiledName = "名称";
            }
            else if (i == 3)
            {
                MConfigInitSelfInfor[i].ConfigFiledName = "模块地址";
            }
            else if (i == 4)
            {
                MConfigInitSelfInfor[i].ConfigFiledName = "页数";
            }
            else if (i == 5)
            {
                MConfigInitSelfInfor[i].ConfigFiledName = "开始地址";
            }
            else if (i == 6)
            {
                MConfigInitSelfInfor[i].ConfigFiledName = "长度";
            }
            else if (i == 7)
            {
                MConfigInitSelfInfor[i].ConfigFiledName = "值";
            }
            MConfigInitSelfInfor[i].EnableFiledValue = true;           
            MConfigInitSelfInfor[i].ConfigFiledValue ="";

            if (i == 2)
            {
                MConfigInitSelfInfor[i].EnableCompareValidator1 = true;
                MConfigInitSelfInfor[i].EnableRangeValidator1 = false;
            }
            else
            {
                MConfigInitSelfInfor[i].EnableCompareValidator1 = false;
                MConfigInitSelfInfor[i].EnableRangeValidator1 = true;
            }

            this.AddNewMConfigInit.Controls.Add(MConfigInitSelfInfor[i]);
        }

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoManufactureConfigInit where ID=" + moduleTypeID, "TopoManufactureConfigInit");
                rowCount = mydt.Rows.Count;
                columCount = mydt.Columns.Count;
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
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ManufaConfigInit.aspx?uId=" + moduleTypeID.Trim());
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }


    }
    public bool SaveData(object obj, string prameter)
    {
        string updataStr = "select * from TopoManufactureConfigInit where ID=" + moduleTypeID;
        DataTable inserTable = pDataIO.GetDataTable(updataStr, "TopoTestPlan");
        DataRow dr = inserTable.NewRow();
        try
        {
            dr[0] = -1;
            dr[1] = moduleTypeID;
            for (byte i = 2; i < MConfigInitSelfInfor.Length; i++)
            {
                dr[i] = MConfigInitSelfInfor[i].ConfigFiledValue;
            }
            
            inserTable.Rows.Add(dr);

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoManufactureConfigInit", inserTable, updataStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoManufactureConfigInit", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                inserTable.AcceptChanges();
                Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ManufaConfigInit.aspx?uId=" + moduleTypeID.Trim());
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!",Request.Url.ToString());
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
        OptionButtons1.ConfigBtSaveVisible = true;
        OptionButtons1.ConfigBtAddVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = true;
    }
}