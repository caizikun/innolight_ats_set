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
public partial class ASPXChipBaseList : BasePage
{
    string funcItemName = "芯片信息(维护)";
    public DataTable mydt = new DataTable();
    ASCXChipBaseChipList[] chipBaseList;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    public DataTable mydtCoefs = new DataTable();

    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {       
        
        {
            IsSessionNull();

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
            SetSessionBlockType(3);
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
            chipBaseList = new ASCXChipBaseChipList[1];

            for (byte i = 0; i < chipBaseList.Length; i++)
        {
            chipBaseList[i] = (ASCXChipBaseChipList)Page.LoadControl("~/Frame/Chip/BaseChipList.ascx");

             chipBaseList[i].LbTH1Text = "名称";
            chipBaseList[i].LbTH2Text = "通道";
            chipBaseList[i].LbTH3Text = "描述";
            chipBaseList[i].LbTH4Text = "寄存器宽度";
            chipBaseList[i].LbTH5Text = "低位在前高位在后?";
       
            chipBaseList[i].ContentTRVisible = false;
            this.chipBase_List.Controls.Add(chipBaseList[i]);
        }
        } 
        else
        {
            chipBaseList = new ASCXChipBaseChipList[rowCount];

            for (byte i = 0; i < chipBaseList.Length; i++)
        {
            chipBaseList[i] = (ASCXChipBaseChipList)Page.LoadControl("~/Frame/Chip/BaseChipList.ascx");
            chipBaseList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            chipBaseList[i].ConfigTh1Text = mydt.Rows[i]["ItemName"].ToString().Trim();
            chipBaseList[i].ConfigTh2Text = mydt.Rows[i]["Channels"].ToString().Trim();
            chipBaseList[i].ConfigTh3Text = mydt.Rows[i]["Description"].ToString().Trim();
            chipBaseList[i].ConfigTh4Text = mydt.Rows[i]["Width"].ToString().Trim();
            chipBaseList[i].ConfigTh5Text = mydt.Rows[i]["LittleEndian"].ToString().Trim();

            chipBaseList[i].LbTH1Text = "名称";
            chipBaseList[i].LbTH2Text = "通道";
            chipBaseList[i].LbTH3Text = "描述";
            chipBaseList[i].LbTH4Text = "寄存器宽度";
            chipBaseList[i].LbTH5Text = "低位在前高位在后?";

            chipBaseList[i].PostBackUrlStringChipSelf = "~/WebFiles/MaintainInfo/Chip/ChipSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            chipBaseList[i].PostBackUrlStringChipFormula = "~/WebFiles/MaintainInfo/Chip/RegisterFormulaList.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
          
            chipBaseList[i].BeSelected = false;
            if (i >= 1)
            {             
                chipBaseList[i].LBTHTitleVisible(false);
                if (i % 2 != 0)
                {
                    chipBaseList[i].TrBackgroundColor = "#F2F2F2";
                }
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.chipBase_List.Controls.Add(chipBaseList[i]);
        }
        }
      

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChipBaseInfo","ChipBaseInfo");
                rowCount = mydt.Rows.Count;

                bindData();
                CommCtrl pCtrl = new CommCtrl();
                //if (Request.QueryString["BlockType"] != null)
                //{
                //    Session["BlockType"] = Request.QueryString["BlockType"];
                //}
                //else
                //{
                //    Session["BlockType"] = null;
                //}
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "维护", Session["BlockType"].ToString(), pDataIO, out logTracingString);
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
        string deletStr = "select * from ChipBaseInfo where ID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < chipBaseList.Length; i++)
            {
                ASCXChipBaseChipList cb = (ASCXChipBaseChipList)chipBase_List.FindControl(chipBaseList[i].ID);
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
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请至少选择一个！');return false;</script>");
                this.Page.RegisterStartupScript("", "<script>alert('请至少选择一个！');</script>");
                return false;
            }

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("ChipBaseInfo", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("ChipBaseInfo", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
            }      

            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("数据更新失败!");
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
            Response.Redirect("~/WebFiles/MaintainInfo/Chip/ChipSelfInfor.aspx?AddNew=true&uId=0");

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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ChipInfo, CommCtrl.CheckAccess.MofifyChipInfor, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
        if (rowCount<=0)
        {
            OptionButtons1.ConfigBtDeleteVisible=false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ChipInfo, CommCtrl.CheckAccess.MofifyChipInfor, myAccessCode);
        }
        
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (chipBaseList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < chipBaseList.Length; i++)
        {
            chipBaseList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (chipBaseList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < chipBaseList.Length; i++)
        {
            chipBaseList[i].BeSelected = false;
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