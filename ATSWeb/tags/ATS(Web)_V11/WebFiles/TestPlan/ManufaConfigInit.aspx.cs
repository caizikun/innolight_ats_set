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
public partial class ASPXManufaConfigInit : BasePage
{
    string funcItemName = "MConfigInitList";
    ASCXManufConfigInitList[] MeConfigInitList;
    ASCXOptionButtons UserOptionButton;
    private int rowCount;
    private string conn;
    private DataIO pDataIO;
    public DataTable mydt = new DataTable();
    private string moduleTypeID = "";
    private string logTracingString = "";
    public ASPXManufaConfigInit()
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
            MeConfigInitList = new ASCXManufConfigInitList[1];
            for (byte i = 0; i < MeConfigInitList.Length; i++)
            {
                MeConfigInitList[i] = (ASCXManufConfigInitList)Page.LoadControl("../../Frame/TestPlan/ManufConfigInitList.ascx");
               

                MeConfigInitList[i].LbTH2Text = mydt.Columns[2].ColumnName;
                MeConfigInitList[i].LbTH3Text = mydt.Columns[3].ColumnName;
                MeConfigInitList[i].LbTH4Text = mydt.Columns[4].ColumnName;
                MeConfigInitList[i].LbTH5Text = mydt.Columns[5].ColumnName;
                MeConfigInitList[i].LbTH6Text = mydt.Columns[6].ColumnName;
                MeConfigInitList[i].ContentTRVisible = false;
              
                this.MConfigInitList.Controls.Add(MeConfigInitList[i]);
            }
        } 
        else
        {
            MeConfigInitList = new ASCXManufConfigInitList[rowCount];
         
        
            for (byte i = 0; i < MeConfigInitList.Length; i++)
            {
                MeConfigInitList[i] = (ASCXManufConfigInitList)Page.LoadControl("../../Frame/TestPlan/ManufConfigInitList.ascx");
                MeConfigInitList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                MeConfigInitList[i].LinkBItemNameID = mydt.Rows[i]["ID"].ToString().Trim();
                MeConfigInitList[i].LiBItemNameText = i.ToString().Trim();
                MeConfigInitList[i].ConfigColum2Text = mydt.Rows[i]["SlaveAddress"].ToString().Trim();
                MeConfigInitList[i].ConfigColum3Text = mydt.Rows[i]["Page"].ToString().Trim();
                MeConfigInitList[i].ConfigColum4Text = mydt.Rows[i]["StartAddress"].ToString().Trim();
                MeConfigInitList[i].ConfigColum5Text = mydt.Rows[i]["Length"].ToString().Trim();
                MeConfigInitList[i].ConfigColum6Text = mydt.Rows[i]["ItemValue"].ToString().Trim();
                MeConfigInitList[i].BeSelected = false;

                MeConfigInitList[i].LbTH2Text = mydt.Columns[2].ColumnName;
                MeConfigInitList[i].LbTH3Text = mydt.Columns[3].ColumnName;
                MeConfigInitList[i].LbTH4Text = mydt.Columns[4].ColumnName;
                MeConfigInitList[i].LbTH5Text = mydt.Columns[5].ColumnName;
                MeConfigInitList[i].LbTH6Text = mydt.Columns[6].ColumnName;

                if (i >= 1)
                {
                    MeConfigInitList[i].LBTH1Visible = false;
                    MeConfigInitList[i].LBTH2Visible = false;
                    MeConfigInitList[i].LBTH3Visible = false;
                    MeConfigInitList[i].LBTH4Visible = false;
                    MeConfigInitList[i].LBTH5Visible = false;
                    MeConfigInitList[i].LBTH6Visible = false;
                    MeConfigInitList[i].LBTHTitleVisible(false);
                }
                this.MConfigInitList.Controls.Add(MeConfigInitList[i]);
            }
        }
       

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoManufactureConfigInit where PID=" + moduleTypeID, "TopoManufactureConfigInit");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();
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
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/TestPlan/AddNewMConfigInit.aspx?uId=" + moduleTypeID.Trim());
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    public bool DeleteData(object obj, string prameter)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('Are you sure you want to delete？')){DeleteData1();}else{}</script>");
        
        int row = 0;
        bool isSelected = false;
        string deletStr = "select * from TopoManufactureConfigInit where PID=" + moduleTypeID;
        try
        {
            for (byte i = 0; i < MeConfigInitList.Length; i++)
            {
                ASCXManufConfigInitList cb = (ASCXManufConfigInitList)MConfigInitList.FindControl(MeConfigInitList[i].ID);
                if (cb != null)
                {
                    if ( cb.BeSelected == true)
                    {
                        row++;
                        isSelected = true;
                        mydt.Rows[i].Delete();
                    }
                    
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('Did not choose any one！');return false;</script>");
                    this.Page.RegisterStartupScript("", "<script>alert('Did not choose any one！');</script>");
                    //Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false)
            {
                Response.Write("<script>alert('Did not choose any one！');</script>");
                return false;
            }
            //pDataIO.UpdateDataTable(deletStr, mydt);

            int result = pDataIO.UpdateWithProc("TopoManufactureConfigInit", mydt, deletStr, logTracingString);
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
        OptionButtons1.ConfigBtCancelVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;
  #region TestPlanAuthority
        bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
        if (addVisible == false && deleteVisible == false)
        {
            bool deletePlan;
            bool testplanEdit = GetTestPlanAuthority(out deletePlan);
            OptionButtons1.ConfigBtAddVisible = testplanEdit;
            if (rowCount <= 0)
            {
                OptionButtons1.ConfigBtDeleteVisible = false;
            }
            else
            {
               
                if (testplanEdit)
                {
                    OptionButtons1.ConfigBtDeleteVisible = testplanEdit;
                } 
                else
                {
                    OptionButtons1.ConfigBtDeleteVisible = deletePlan;
                }
            }

        }
        else
        {
            OptionButtons1.ConfigBtAddVisible = addVisible;
            if (rowCount <= 0)
            {
                OptionButtons1.ConfigBtDeleteVisible = false;
            }
            else
            {
                OptionButtons1.ConfigBtDeleteVisible = deleteVisible;
            }
        }


        #endregion

    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (MeConfigInitList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < MeConfigInitList.Length; i++)
        {
            MeConfigInitList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (MeConfigInitList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < MeConfigInitList.Length; i++)
        {
            MeConfigInitList[i].BeSelected = false;
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
    public bool GetTestPlanAuthority(out bool deletePlan)
    {
        string userID = Session["UserID"].ToString().Trim();
        bool tpAuthority = false;
        deletePlan = false;
        try
        {

            if (pDataIO.OpenDatabase(true))
            {

                {
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + moduleTypeID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                        deletePlan = false;
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
                        if (temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "1")
                        {
                            deletePlan = true;
                        }
                        else
                        {
                            deletePlan = false;
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