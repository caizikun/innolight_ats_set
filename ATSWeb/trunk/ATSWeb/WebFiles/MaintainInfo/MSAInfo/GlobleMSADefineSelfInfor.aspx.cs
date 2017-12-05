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
public partial class ASPXGlobleMSADefineSelfInfor : BasePage
{
    string funcItemName = "MSA定义信息";
    ASCXOptionButtons UserOptionButton;
    ASCXGloblaMSADefinationSelfInfor[] GlobalMSADefSelfInforlist;
    ASCXGloblaMSADefinationSelfInfor GlobalMSASelfInfor;
    private string conn;
   private DataIO pDataIO;
   private DataTable mydt = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private int columCount;
   private bool AddNew;
    private string logTracingString = "";

    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();

            columCount = 0;
            AddNew = false;
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
            SetSessionBlockType(3);
            moduleTypeID = Request["uId"];
            if (Request.QueryString["AddNew"] != null)
            {
                AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            }
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
          
        }

    }

    public void bindData()
    {
        GlobalMSADefSelfInforlist = new ASCXGloblaMSADefinationSelfInfor[rowCount];
        if (rowCount == 0 && AddNew == false)
        {
            return;
        }
        if (AddNew)
        {
            ClearTextValue();
        }
        else
        {
            for (byte i = 0; i < GlobalMSADefSelfInforlist.Length; i++)
            {
                GlobalMSADefSelfInforlist[i] = (ASCXGloblaMSADefinationSelfInfor)Page.LoadControl("~/Frame/MSAInfo/GloblaMSADefinationSelfInfor.ascx");
                //GlobalMSADefSelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().ToUpper().Trim();
                GlobalMSADefSelfInforlist[i].TH2Text = "字段名";
                GlobalMSADefSelfInforlist[i].Colum2TextConfig = mydt.Rows[i]["FieldName"].ToString().Trim();
                GlobalMSADefSelfInforlist[i].TH3Text = "通道";
                GlobalMSADefSelfInforlist[i].Colum3TextConfig = mydt.Rows[i]["Channel"].ToString().Trim();

                GlobalMSADefSelfInforlist[i].TH4Text = "模块地址";
                GlobalMSADefSelfInforlist[i].Colum4TextConfig = mydt.Rows[i]["SlaveAddress"].ToString().Trim();

                GlobalMSADefSelfInforlist[i].TH5Text = "页数";
                GlobalMSADefSelfInforlist[i].Colum5TextConfig = mydt.Rows[i]["Page"].ToString().Trim();

                GlobalMSADefSelfInforlist[i].TH6Text = "开始地址";
                GlobalMSADefSelfInforlist[i].Colum6TextConfig = mydt.Rows[i]["StartAddress"].ToString().Trim();

                GlobalMSADefSelfInforlist[i].TH7Text = "长度";
                GlobalMSADefSelfInforlist[i].Colum7TextConfig = mydt.Rows[i]["Length"].ToString().Trim();

                GlobalMSADefSelfInforlist[i].TH8Text = "格式";
                GlobalMSADefSelfInforlist[i].Colum8TextConfig = mydt.Rows[i]["Format"].ToString().Trim();
                GlobalMSADefSelfInforlist[i].EnableColum2Text = false;
                GlobalMSADefSelfInforlist[i].EnableColum3Text = false;
                GlobalMSADefSelfInforlist[i].EnableColum4Text = false;
                GlobalMSADefSelfInforlist[i].EnableColum5Text = false;
                GlobalMSADefSelfInforlist[i].EnableColum6Text = false;
                GlobalMSADefSelfInforlist[i].EnableColum7Text = false;
                GlobalMSADefSelfInforlist[i].EnableColum8Text = false;


                this.GlobalMSADefSelfInfor.Controls.Add(GlobalMSADefSelfInforlist[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalMSADefintionInf where ID=" + moduleTypeID, "GlobalMSADefintionInf");
                rowCount = mydt.Rows.Count;
                columCount = mydt.Columns.Count;
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "添加新项";
                }
                else
                {
                     parentItem = pDataIO.getDbCmdExecuteScalar("select FieldName from GlobalMSADefintionInf where id = " + moduleTypeID).ToString();
                }

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
        string updataStr = "select * from GlobalMSADefintionInf where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "GlobalMSADefintionInf");
                DataRow dr = inserTable.NewRow();
                  dr[0] = -1;
                  dr[1] = moduleTypeID;
                  dr[2] = GlobalMSASelfInfor.Colum2TextConfig;
                  dr[3] = GlobalMSASelfInfor.Colum3TextConfig;
                  dr[4] = GlobalMSASelfInfor.Colum4TextConfig;
                  dr[5] = GlobalMSASelfInfor.Colum5TextConfig;
                  dr[6] = GlobalMSASelfInfor.Colum6TextConfig;
                  dr[7] = GlobalMSASelfInfor.Colum7TextConfig;
                  dr[8] = GlobalMSASelfInfor.Colum8TextConfig;
                    inserTable.Rows.Add(dr);

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSADefintionInf", inserTable, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSADefintionInf", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result > 0)
                    {
                        inserTable.AcceptChanges();
                        Response.Redirect("~/WebFiles/MaintainInfo/MSAInfo/GlobalMSADefinationList.aspx?uId=" + moduleTypeID.Trim());
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
                    } 
                  
                    return true;
               
            } 
            else
            {
                if (rowCount != 1)
                {
                    return false;
                }
                else
                {

                    for (byte j = 0; j < GlobalMSADefSelfInforlist.Length; j++)
                                {
                                    mydt.Rows[0]["FieldName"] = GlobalMSADefSelfInforlist[j].Colum2TextConfig;
                                    mydt.Rows[0]["Channel"] = GlobalMSADefSelfInforlist[j].Colum3TextConfig;
                                    mydt.Rows[0]["SlaveAddress"] = GlobalMSADefSelfInforlist[j].Colum4TextConfig;
                                    mydt.Rows[0]["Page"] = GlobalMSADefSelfInforlist[j].Colum5TextConfig;
                                    mydt.Rows[0]["StartAddress"] = GlobalMSADefSelfInforlist[j].Colum6TextConfig;
                                    mydt.Rows[0]["Length"] = GlobalMSADefSelfInforlist[j].Colum7TextConfig;
                                    mydt.Rows[0]["Format"] = GlobalMSADefSelfInforlist[j].Colum8TextConfig;
                                }

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSADefintionInf", mydt, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("GlobalMSADefintionInf", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!");
                    }
                    for (byte i = 0; i < GlobalMSADefSelfInforlist.Length; i++)
                        {
                            GlobalMSADefSelfInforlist[i].EnableColum2Text = false;
                            GlobalMSADefSelfInforlist[i].EnableColum3Text = false;
                            GlobalMSADefSelfInforlist[i].EnableColum4Text = false;
                            GlobalMSADefSelfInforlist[i].EnableColum5Text = false;
                            GlobalMSADefSelfInforlist[i].EnableColum6Text = false;
                            GlobalMSADefSelfInforlist[i].EnableColum7Text = false;
                            GlobalMSADefSelfInforlist[i].EnableColum8Text = false;
                        }
                        return true;
                   
                }
               
               
              
            }

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
           for (int i = 0; i < GlobalMSADefSelfInforlist.Length; i++)
           {
               GlobalMSADefSelfInforlist[i].EnableColum2Text = true;
               GlobalMSADefSelfInforlist[i].EnableColum3Text = true;
               GlobalMSADefSelfInforlist[i].EnableColum4Text = true;
               GlobalMSADefSelfInforlist[i].EnableColum5Text = true;
               GlobalMSADefSelfInforlist[i].EnableColum6Text = true;
               GlobalMSADefSelfInforlist[i].EnableColum7Text = true;
               GlobalMSADefSelfInforlist[i].EnableColum8Text = true;
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
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/MaintainInfo/MSAInfo/GlobalMSADefinationList.aspx?uId=" + moduleTypeID.Trim());
            }          
            else
            {
                Response.Redirect(Request.Url.ToString(), true);
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
      
        OptionButtons1.ConfigBtAddVisible = false;
        if (AddNew)
        {
            OptionButtons1.ConfigBtEditVisible = false;
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtCancelVisible = true;
        } 
        else
        {
            int myAccessCode =0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();
            OptionButtons1.ConfigBtSaveVisible = false;       
            OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MSA, CommCtrl.CheckAccess.MofifyMSA, myAccessCode);            
            OptionButtons1.ConfigBtCancelVisible = false;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        GlobalMSASelfInfor = new ASCXGloblaMSADefinationSelfInfor();
        try
        {


            {
                GlobalMSASelfInfor = (ASCXGloblaMSADefinationSelfInfor)Page.LoadControl("~/Frame/MSAInfo/GloblaMSADefinationSelfInfor.ascx");
                GlobalMSASelfInfor.Colum2TextConfig = "";
                GlobalMSASelfInfor.Colum3TextConfig = "";
                GlobalMSASelfInfor.Colum4TextConfig = "";
                GlobalMSASelfInfor.Colum5TextConfig = "";
                GlobalMSASelfInfor.Colum6TextConfig = "";
                GlobalMSASelfInfor.Colum7TextConfig = "";
                GlobalMSASelfInfor.Colum8TextConfig = "";

                GlobalMSASelfInfor.EnableColum8Text = true;
                GlobalMSASelfInfor.EnableColum7Text = true;
                GlobalMSASelfInfor.EnableColum6Text = true;
                GlobalMSASelfInfor.EnableColum5Text = true;
                GlobalMSASelfInfor.EnableColum4Text = true;
                GlobalMSASelfInfor.EnableColum3Text = true;
                GlobalMSASelfInfor.EnableColum2Text = true;
                GlobalMSASelfInfor.TH2Text = "字段名";
                GlobalMSASelfInfor.TH3Text = "通道";
                GlobalMSASelfInfor.TH4Text = "模块地址";
                GlobalMSASelfInfor.TH5Text = "页数";
                GlobalMSASelfInfor.TH6Text = "开始地址";
                GlobalMSASelfInfor.TH7Text = "长度";
                GlobalMSASelfInfor.TH8Text = "格式";

                this.GlobalMSADefSelfInfor.Controls.Add(GlobalMSASelfInfor);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}