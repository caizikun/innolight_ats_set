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
public partial class WebFiles_Chip_RegisterFormulaList : BasePage
{
    string funcItemName = "ChipRegisterList";
    public DataTable mydt = new DataTable();
    ASCX_Chip_FormulaList[] RegisterFormulaList;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    public DataTable mydtCoefs = new DataTable();
    public WebFiles_Chip_RegisterFormulaList()
    {
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
        
        {
            IsSessionNull();
            SetSessionBlockType(10);
            moduleTypeID = Request["uId"];

            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }
        
    }

    public void bindData()
    {
       
        ClearCurrenPage();
        
        if (rowCount==0)
        {
            RegisterFormulaList = new ASCX_Chip_FormulaList[1];

            for (byte i = 0; i < RegisterFormulaList.Length; i++)
        {
            RegisterFormulaList[i] = (ASCX_Chip_FormulaList)Page.LoadControl("../../Frame/Chip/FormulaList.ascx");

           
            RegisterFormulaList[i].ContentTRVisible = false;
            this.RegisterFormular_List.Controls.Add(RegisterFormulaList[i]);
        }
        } 
        else
        {
            RegisterFormulaList = new ASCX_Chip_FormulaList[rowCount];

            for (byte i = 0; i < RegisterFormulaList.Length; i++)
        {
            RegisterFormulaList[i] = (ASCX_Chip_FormulaList)Page.LoadControl("../../Frame/Chip/FormulaList.ascx");
            RegisterFormulaList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh1Text = mydt.Rows[i]["ItemName"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh2Text = mydt.Rows[i]["WriteFormula"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh3Text = mydt.Rows[i]["AnalogueUnit"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh4Text = mydt.Rows[i]["ReadFormula"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh5Text = mydt.Rows[i]["Address"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh6Text = mydt.Rows[i]["StartBit"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh7Text = mydt.Rows[i]["EndBit"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh8Text = mydt.Rows[i]["UnitLength"].ToString().Trim();
            RegisterFormulaList[i].ConfigTh9Text = mydt.Rows[i]["ChipLine"].ToString().Trim();
            RegisterFormulaList[i].PostBackUrlStringFormulaSelf = "~/WebFiles/Chip/RegisterFormularInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            
          
            RegisterFormulaList[i].BeSelected = false;
            if (i >= 1)
            {
                RegisterFormulaList[i].LBTH1Visible = false;
                RegisterFormulaList[i].LBTH2Visible = false;
                RegisterFormulaList[i].LBTH3Visible = false;
                RegisterFormulaList[i].LBTH4Visible = false;
                RegisterFormulaList[i].LBTH5Visible = false;
                RegisterFormulaList[i].LBTH6Visible = false;
                RegisterFormulaList[i].LBTH7Visible = false;
                RegisterFormulaList[i].LBTH8Visible = false;
                RegisterFormulaList[i].LBTH9Visible = false;

                RegisterFormulaList[i].LBTHTitleVisible(false);
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.RegisterFormular_List.Controls.Add(RegisterFormulaList[i]);
        }
        }
      

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChipRegisterList where ChipID= " + moduleTypeID, "ChipRegisterList");
                rowCount = mydt.Rows.Count;

                bindData();
                string parentItem = "";
                parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from ChipBaseInfo where id =" + moduleTypeID).ToString();
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

   
   
    public bool DeleteData(object obj, string prameter)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('Are you sure you want to delete？')){DeleteData1();}else{}</script>");
        bool isSelected = false;
        string deletStr = "select * from ChipRegisterList where ID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < RegisterFormulaList.Length; i++)
            {
                ASCX_Chip_FormulaList cb = (ASCX_Chip_FormulaList)RegisterFormular_List.FindControl(RegisterFormulaList[i].ID);
                if (cb != null )
                {
                    if (cb.BeSelected == true)
                    {

                        mydt.Rows[i].Delete();
                        isSelected = true;
                    }                    
                }
                else
                {
                    Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false)
            {               
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('Did not choose any one！');return false;</script>");
                this.Page.RegisterStartupScript("", "<script>alert('Did not choose any one！');</script>");
                return false;
            }
            int result = pDataIO.UpdateWithProc("ChipRegisterList", mydt, deletStr, logTracingString);
            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("Update data fail!");
            }           
            Response.Redirect(Request.Url.ToString());
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
        //ASCXOptionButtons UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool SaveData(object obj, string prameter)
    {
        return true;
    }
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/Chip/RegisterFormularInfor.aspx?AddNew=true&uId=" + moduleTypeID);

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.AddProduction, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
        if (rowCount<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible=false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.DeleteProduction, myAccessCode);
        }
        
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (RegisterFormulaList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < RegisterFormulaList.Length; i++)
        {
            RegisterFormulaList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (RegisterFormulaList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < RegisterFormulaList.Length; i++)
        {
            RegisterFormulaList[i].BeSelected = false;
        }
    }
    public void ClearCurrenPage()
    {
        if (rowCount == 0)
        {
            SelectAll.Visible = false;
            DeSelectAll.Visible = false;
        }
    }
   
}