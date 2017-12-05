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
public partial class ASPXMSAModuleTypeList : BasePage
{
    const string funcItemName = "MSATypeList";
    ASCXMSAModuleTypeList[] testModuleTypeSelfInfor;
    
     int rowCount;
    HyperLink[] hlkList;
    DataTable NaviDt = new DataTable();
    private string logTracingString = "";
     private string conn;
    private DataIO pDataIO ;   
    public DataTable mydt = new DataTable();
    public ASPXMSAModuleTypeList()
    {
        rowCount = 0;
        conn ="inpcsz0518\\ATS_HOME";
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
            SetSessionBlockType(3);
            connectDataBase();
            ConfigOptionButtonsVisible();
        } 
        
    }
    public void bindData()
    {
        ClearCurrenPage();
        if (rowCount==0)
        {
            testModuleTypeSelfInfor = new ASCXMSAModuleTypeList[1];
            for (byte i = 0; i < testModuleTypeSelfInfor.Length; i++)
            {
                testModuleTypeSelfInfor[i] = (ASCXMSAModuleTypeList)Page.LoadControl("../../Frame/MSAInfo/MSAModuleTypeList.ascx");
                testModuleTypeSelfInfor[i].TH1TEXT = mydt.Columns[1].ColumnName;
                testModuleTypeSelfInfor[i].TH2TEXT = mydt.Columns[2].ColumnName;
                testModuleTypeSelfInfor[i].TH3TEXT = mydt.Columns[3].ColumnName;
                testModuleTypeSelfInfor[i].TH5TEXT = "GlobalMSADefintion";
                testModuleTypeSelfInfor[i].ContentTRVisible = false;
               

                this.msamodutype.Controls.Add(testModuleTypeSelfInfor[i]);


            }
        } 
        else
        {
            testModuleTypeSelfInfor = new ASCXMSAModuleTypeList[rowCount];
            for (byte i = 0; i < testModuleTypeSelfInfor.Length; i++)
            {
                testModuleTypeSelfInfor[i] = (ASCXMSAModuleTypeList)Page.LoadControl("../../Frame/MSAInfo/MSAModuleTypeList.ascx");
                testModuleTypeSelfInfor[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                testModuleTypeSelfInfor[i].LinkText1 = mydt.Rows[i]["ItemName"].ToString().Trim();
                testModuleTypeSelfInfor[i].PostBackUrlString = "~/WebFiles/MSAInfo/GlobalMSASelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().ToUpper().Trim();


                testModuleTypeSelfInfor[i].ConfigLbText = mydt.Rows[i]["AccessInterface"].ToString().Trim();


                testModuleTypeSelfInfor[i].ConfigLb2Text = mydt.Rows[i]["SlaveAddress"].ToString().Trim();



                testModuleTypeSelfInfor[i].LinkText2 = "View";
                testModuleTypeSelfInfor[i].PostBackUrlStringPN = "~/WebFiles/MSAInfo/GlobalMSADefinationList.aspx?uId=" + mydt.Rows[i]["ID"].ToString().Trim();

                testModuleTypeSelfInfor[i].TH1TEXT = mydt.Columns[1].ColumnName;
                testModuleTypeSelfInfor[i].TH2TEXT = mydt.Columns[2].ColumnName;
                testModuleTypeSelfInfor[i].TH3TEXT = mydt.Columns[3].ColumnName;

                testModuleTypeSelfInfor[i].TH5TEXT = "GlobalMSADefintion";

                if (i >= 1)
                {
                    testModuleTypeSelfInfor[i].EnabelTH5Visible = false;

                    testModuleTypeSelfInfor[i].EnabelTH3Visible = false;
                    testModuleTypeSelfInfor[i].EnabelTH2Visible = false;
                    testModuleTypeSelfInfor[i].EnabelTH1Visible = false;
                    testModuleTypeSelfInfor[i].LBTHTitleVisible(false);
                }

                this.msamodutype.Controls.Add(testModuleTypeSelfInfor[i]);


            }
        }
     
       
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalMSA where IgnoreFlag='false'", "GlobalMSA");
                rowCount = mydt.Rows.Count;
                configTHText();
                bindData();
                CommCtrl pCtrl = new CommCtrl();
                if (Request.QueryString["BlockType"] != null)
                {
                    Session["BlockType"] = Request.QueryString["BlockType"];
                }
                else
                {
                    Session["BlockType"] = null;
                }
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "MSAInfo", Session["BlockType"].ToString(), pDataIO,out logTracingString);
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
    public void configTHText()
    {
       
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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MSA, CommCtrl.CheckAccess.AddMSA, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
        if (rowCount<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MSA, CommCtrl.CheckAccess.DeleteMSA, myAccessCode);
        }
        
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/MSAInfo/GlobalMSASelfInfor.aspx?AddNew=true&uId=0");

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
        string deletStr = "select * from GlobalMSA where IgnoreFlag='false'";
        try
        {
            for (int i = 0; i < testModuleTypeSelfInfor.Length; i++)
            {
                ASCXMSAModuleTypeList cb = (ASCXMSAModuleTypeList)msamodutype.FindControl(testModuleTypeSelfInfor[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        row++;
                        isSelected = true;
                        mydt.Rows[i]["IgnoreFlag"] = true;
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
                //Response.Write("<script>alert('Did not choose any one！');</script>");               
                return false;
            }
            int result = pDataIO.UpdateWithProc("GlobalMSA", mydt, deletStr, logTracingString);
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
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (testModuleTypeSelfInfor.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < testModuleTypeSelfInfor.Length; i++)
        {
            testModuleTypeSelfInfor[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (testModuleTypeSelfInfor.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < testModuleTypeSelfInfor.Length; i++)
        {
            testModuleTypeSelfInfor[i].BeSelected = false;
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