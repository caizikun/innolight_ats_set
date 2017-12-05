﻿using System;
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
    const string funcItemName = "MSA";
    ASCXMSAModuleTypeList[] testModuleTypeSelfInfor;
    
     int rowCount;
    HyperLink[] hlkList;
    DataTable NaviDt = new DataTable();
    private string logTracingString = "";
     private string conn;
    private DataIO pDataIO ;   
    public DataTable mydt = new DataTable();

    protected override void OnInit(EventArgs e)  
    {        
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();

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
                testModuleTypeSelfInfor[i] = (ASCXMSAModuleTypeList)Page.LoadControl("~/Frame/MSAInfo/MSAModuleTypeList.ascx");
                testModuleTypeSelfInfor[i].TH1TEXT = "名称";
                testModuleTypeSelfInfor[i].TH2TEXT = "接口";
                testModuleTypeSelfInfor[i].TH3TEXT = "模块地址";
                testModuleTypeSelfInfor[i].TH5TEXT = "MSA定义";
                testModuleTypeSelfInfor[i].ContentTRVisible = false;
               

                this.msamodutype.Controls.Add(testModuleTypeSelfInfor[i]);


            }
        } 
        else
        {
            testModuleTypeSelfInfor = new ASCXMSAModuleTypeList[rowCount];
            for (byte i = 0; i < testModuleTypeSelfInfor.Length; i++)
            {
                testModuleTypeSelfInfor[i] = (ASCXMSAModuleTypeList)Page.LoadControl("~/Frame/MSAInfo/MSAModuleTypeList.ascx");
                testModuleTypeSelfInfor[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                testModuleTypeSelfInfor[i].LinkText1 = mydt.Rows[i]["ItemName"].ToString().Trim();
                testModuleTypeSelfInfor[i].PostBackUrlString = "~/WebFiles/MaintainInfo/MSAInfo/GlobalMSASelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().ToUpper().Trim();


                testModuleTypeSelfInfor[i].ConfigLbText = mydt.Rows[i]["AccessInterface"].ToString().Trim();


                testModuleTypeSelfInfor[i].ConfigLb2Text = mydt.Rows[i]["SlaveAddress"].ToString().Trim();



                testModuleTypeSelfInfor[i].LinkText2 = "查看";
                testModuleTypeSelfInfor[i].PostBackUrlStringPN = "~/WebFiles/MaintainInfo/MSAInfo/GlobalMSADefinationList.aspx?uId=" + mydt.Rows[i]["ID"].ToString().Trim();

                testModuleTypeSelfInfor[i].TH1TEXT = "名称";
                testModuleTypeSelfInfor[i].TH2TEXT = "接口";
                testModuleTypeSelfInfor[i].TH3TEXT = "模块地址";

                testModuleTypeSelfInfor[i].TH5TEXT = "MSA定义";

                if (i >= 1)
                {
                    testModuleTypeSelfInfor[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        testModuleTypeSelfInfor[i].TrBackgroundColor = "#F2F2F2";
                    }
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
        OptionButtons1.ConfigBtAddVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.MSA, CommCtrl.CheckAccess.MofifyMSA, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = false;
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
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/MaintainInfo/MSAInfo/GlobalMSASelfInfor.aspx?AddNew=true&uId=0");

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
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请至少选择一个！');return false;</script>");
                this.Page.RegisterStartupScript("", "<script>alert('请至少选择一个！');</script>");
                //Response.Write("<script>alert('请至少选择一个！');</script>");               
                return false;
            }

            int result = -1;
            if (Session["DB"].ToString().ToUpper() == "ATSDB")
            {
                result = pDataIO.UpdateWithProc("GlobalMSA", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalMSA", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
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