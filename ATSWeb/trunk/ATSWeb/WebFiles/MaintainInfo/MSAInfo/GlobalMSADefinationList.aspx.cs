using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using ATSDataBase;
using System.Threading;
public partial class ASPXGlobalMSADefinationList : BasePage
{
    string funcItemName = "MSA定义";
    public DataTable mydt = new DataTable();
    ASCXChipSetContrl[] GlobleMSADefine;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";

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
            GlobleMSADefine = new ASCXChipSetContrl[1];
            for (byte i = 0; i < GlobleMSADefine.Length; i++)
            {
                GlobleMSADefine[i] = (ASCXChipSetContrl)Page.LoadControl("~/Frame/Production/ChipSetContrl.ascx");
              

                GlobleMSADefine[i].LbTH2 = "字段名";
                GlobleMSADefine[i].LbTH3 = "通道";
                GlobleMSADefine[i].LbTH4 = "模块地址";
                GlobleMSADefine[i].LbTH5 = "页数";
                GlobleMSADefine[i].LbTH6 = "开始地址";
                GlobleMSADefine[i].LbTH7 = "长度";
                GlobleMSADefine[i].ContentTRVisible = false;
                GlobleMSADefine[i].Column8Visible = false;
                GlobleMSADefine[i].Column9Visible = false;
                this.GlobalMSADefinList.Controls.Add(GlobleMSADefine[i]);
            }
        } 
        else
        {
            GlobleMSADefine = new ASCXChipSetContrl[rowCount];
            for (byte i = 0; i < GlobleMSADefine.Length; i++)
            {
                GlobleMSADefine[i] = (ASCXChipSetContrl)Page.LoadControl("~/Frame/Production/ChipSetContrl.ascx");
                GlobleMSADefine[i].ID = mydt.Rows[i]["ID"].ToString().ToUpper().Trim();
                GlobleMSADefine[i].LbTH2Text = mydt.Rows[i]["FieldName"].ToString().Trim();
                GlobleMSADefine[i].LbTH3Text = mydt.Rows[i]["Channel"].ToString().Trim();
                GlobleMSADefine[i].LbTH4Text = mydt.Rows[i]["SlaveAddress"].ToString().Trim();
                GlobleMSADefine[i].LbTH5Text = mydt.Rows[i]["Page"].ToString().Trim();
                GlobleMSADefine[i].LbTH6Text = mydt.Rows[i]["StartAddress"].ToString().Trim();
                GlobleMSADefine[i].LbTH7Text = mydt.Rows[i]["Length"].ToString().Trim();

                GlobleMSADefine[i].LbTH2 = "字段名";
                GlobleMSADefine[i].LbTH3 ="通道";
                GlobleMSADefine[i].LbTH4 = "模块地址";
                GlobleMSADefine[i].LbTH5 = "页数";
                GlobleMSADefine[i].LbTH6 = "开始地址";
                GlobleMSADefine[i].LbTH7 = "长度";
                GlobleMSADefine[i].PostBackUrlStringPNSelf = "~/WebFiles/MaintainInfo/MSAInfo/GlobleMSADefineSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().ToUpper().Trim();

                GlobleMSADefine[i].BeSelected = false;
                GlobleMSADefine[i].Column8Visible = false;
                GlobleMSADefine[i].Column9Visible = false;
                if (i >= 1)
                {
                    GlobleMSADefine[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        GlobleMSADefine[i].TrBackgroundColor = "#F2F2F2";
                    }

                }
                //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
                this.GlobalMSADefinList.Controls.Add(GlobleMSADefine[i]);
            }
        }
      

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalMSADefintionInf where PID=" + moduleTypeID + "ORDER BY SlaveAddress,Page,StartAddress,Channel", "GlobalMSADefintionInf");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalMSA where id = " + moduleTypeID).ToString();
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
        string deletStr = "select * from GlobalMSADefintionInf where PID=" + moduleTypeID + "ORDER BY SlaveAddress,Page,StartAddress,Channel";
        try
        {
            for (int i = 0; i < GlobleMSADefine.Length; i++)
            {
                ASCXChipSetContrl cb = (ASCXChipSetContrl)GlobalMSADefinList.FindControl(GlobleMSADefine[i].ID);
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
                result = pDataIO.UpdateWithProc("GlobalMSADefintionInf", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalMSADefintionInf", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
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
            Response.Redirect("~/WebFiles/MaintainInfo/MSAInfo/GlobleMSADefineSelfInfor.aspx?AddNew=true&uId=" + moduleTypeID);

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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MSA, CommCtrl.CheckAccess.MofifyMSA, myAccessCode);
        OptionButtons1.ConfigBtEditVisible =false;
        if (rowCount<=0)
        {
          OptionButtons1.ConfigBtDeleteVisible = false;
        } 
        else
        {
            OptionButtons1.ConfigBtDeleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MSA, CommCtrl.CheckAccess.MofifyMSA, myAccessCode);
        }
      
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (GlobleMSADefine.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < GlobleMSADefine.Length; i++)
        {
            GlobleMSADefine[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (GlobleMSADefine.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < GlobleMSADefine.Length; i++)
        {
            GlobleMSADefine[i].BeSelected = false;
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