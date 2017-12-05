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
public partial class WebFiles_Chip_RegisterList : BasePage
{
    string funcItemName = "RegisterList";
    public DataTable mydt = new DataTable();
    Frame_Chip_RegisterList[] registerList;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    private string RegisterName = "";
    public DataTable mydtCoefs = new DataTable();
    private SortedList <int ,string> AccessTypeMapping=new SortedList<int,string>();
    public WebFiles_Chip_RegisterList()
    {
        conn = "inpcsz0518\\ATS_HOME";
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        mydt.Clear();
        AccessTypeMapping.Clear();
        AccessTypeMapping.Add(0,"MultyBytes");
        AccessTypeMapping.Add(1,"SingleByte");
        AccessTypeMapping.Add(2,"Bits");
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
            registerList = new Frame_Chip_RegisterList[1];

            for (byte i = 0; i < registerList.Length; i++)
        {
            registerList[i] = (Frame_Chip_RegisterList)Page.LoadControl("../../Frame/Chip/RegisterList.ascx");

           
            registerList[i].ContentTRVisible = false;
            this.Register_List.Controls.Add(registerList[i]);
        }
        } 
        else
        {
            registerList = new Frame_Chip_RegisterList[rowCount];

            for (byte i = 0; i < registerList.Length; i++)
        {
            registerList[i] = (Frame_Chip_RegisterList)Page.LoadControl("../../Frame/Chip/RegisterList.ascx");
            registerList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            registerList[i].ConfigTh1Text = RegisterName;

            registerList[i].ConfigTh2Text = mydt.Rows[i]["EndBit"].ToString().Trim();
             registerList[i].ConfigTh3Text = mydt.Rows[i]["Address"].ToString().Trim();
             registerList[i].ConfigTh4Text = mydt.Rows[i]["StartBit"].ToString().Trim();
             registerList[i].ConfigTh5Text = mydt.Rows[i]["UnitLength"].ToString().Trim();
             registerList[i].ConfigTh6Text = mydt.Rows[i]["ChipLine"].ToString().Trim();

             registerList[i].PostBackUrlStringRegisterSelf = "~/WebFiles/Chip/RegisterInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
 
          
            registerList[i].BeSelected = false;
            if (i >= 1)
            {
                registerList[i].LBTH1Visible = false;
                registerList[i].LBTH2Visible = false;
                registerList[i].LBTH3Visible = false;
                registerList[i].LBTH4Visible = false;
                registerList[i].LBTH5Visible = false;
                registerList[i].LBTH6Visible = false;                
                registerList[i].LBTHTitleVisible(false);
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.Register_List.Controls.Add(registerList[i]);
        }
        }
      

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChipRegister where FormulaID="+moduleTypeID, "ChipRegister");
                rowCount = mydt.Rows.Count;
                string parentItem = "";


                parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from RegisterFormula where id =" + moduleTypeID).ToString();
                RegisterName = parentItem;
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
                this.plhNavi.Controls.Add(myCtrl);
                bindData();
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
        string deletStr = "select * from ChipRegister where ID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < registerList.Length; i++)
            {
                Frame_Chip_RegisterList cb = (Frame_Chip_RegisterList)Register_List.FindControl(registerList[i].ID);
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
            int result = pDataIO.UpdateWithProc("ChipRegister", mydt, deletStr, logTracingString);
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
            Response.Redirect("~/WebFiles/Chip/RegisterInfor.aspx?AddNew=true&uId="+moduleTypeID);

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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ChipInfor, CommCtrl.CheckAccess.EditChipInfor, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
        if (rowCount<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible=false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ChipInfor, CommCtrl.CheckAccess.EditChipInfor, myAccessCode);
        }
        
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (registerList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < registerList.Length; i++)
        {
            registerList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (registerList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < registerList.Length; i++)
        {
            registerList[i].BeSelected = false;
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