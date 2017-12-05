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
public partial class ASPXMConfigInitSelfInfor : BasePage
{
    string funcItemName = "模块配置信息";
    ASCXOptionButtons UserOptionButton;
    ASCXMConfigInitSelfInfor[] MConfigInitSelfInfor;
    private string conn;
    private DataIO pDataIO;
    public DataTable mydt = new DataTable();
    private string moduleTypeID = "";
    private int rowCount;
    private int columCount;
    private string logTracingString = "";
    private string Index = "";

    public ASPXMConfigInitSelfInfor()
    {

    }
    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
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
        Index = Request["uIndex"];
        connectDataBase();
        LoadOptionButton();
        ConfigOptionButtonsVisible();
    }

    public void bindData()
    {

        MConfigInitSelfInfor = new ASCXMConfigInitSelfInfor[columCount];
        for (byte i = 2; i < MConfigInitSelfInfor.Length; i++)
        {
            MConfigInitSelfInfor[i] = (ASCXMConfigInitSelfInfor)Page.LoadControl("~/Frame/TestPlan/MConfigInitSelfInfor.ascx");
            //MConfigInitSelfInfor[i].ID = i.ToString().Trim();
            MConfigInitSelfInfor[i].ConfigFiledValueID = mydt.Rows[0]["ID"].ToString().Trim();
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
            MConfigInitSelfInfor[i].EnableFiledValue = false;
            if (rowCount!=1)
            {
                MConfigInitSelfInfor[i].ConfigFiledValue = "";
            }
            else
            {
                MConfigInitSelfInfor[i].ConfigFiledValue = mydt.Rows[0][mydt.Columns[i].ColumnName].ToString().Trim();
            }


            this.MConfigInitSelfInforPH.Controls.Add(MConfigInitSelfInfor[i]);
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
                //string parentItem = Index;
                string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from TopoManufactureConfigInit where id = " + moduleTypeID).ToString();

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
        string updataStr = "select * from TopoManufactureConfigInit where ID=" + moduleTypeID;
        try
        {
            for (byte i = 2; i < MConfigInitSelfInfor.Length; i++)
            {
                if (rowCount != 1)
                {
                    return false;
                }
                else
                {
                    mydt.Rows[0][mydt.Columns[i].ColumnName] = MConfigInitSelfInfor[i].ConfigFiledValue;
                }
               
            }

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("TopoManufactureConfigInit", mydt, updataStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoManufactureConfigInit", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
            }      
           
            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
            }
            for (byte i = 2; i < MConfigInitSelfInfor.Length; i++)
            {
                MConfigInitSelfInfor[i].EnableFiledValue = false;
            }
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
           for (byte i = 2; i < MConfigInitSelfInfor.Length; i++)
           {
               MConfigInitSelfInfor[i].EnableFiledValue = true;
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
        int myAccessCode =0;
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
       
        OptionButtons1.ConfigBtDeleteVisible =false;
        OptionButtons1.ConfigBtCancelVisible = false;
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
                    DataTable planIDTable = pDataIO.GetDataTable("select PID from TopoManufactureConfigInit where ID=" + moduleTypeID, "TopoManufactureConfigInit");
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