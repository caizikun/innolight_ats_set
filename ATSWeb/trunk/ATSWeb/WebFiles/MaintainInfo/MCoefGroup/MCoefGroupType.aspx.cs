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
using System.Threading; 
public partial class WebFiles_MCoefGroup_MCoefGroupType : BasePage
{
    const string funcItemName = "模块系数";
    ASCXTestplanType[] testModuleType; 
    DataTable NaviDt = new DataTable();
    int rowCount;
     private string conn;
    private DataIO pDataIO ;   
    public DataTable mydt = new DataTable();
    public DataTable mydtMSA = new DataTable();
    private string logTracingString = "";

    protected override void OnInit(EventArgs e)  
    {        
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();

            conn = "inpcsz0518\\ATS_HOME";
            rowCount = 0;
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
        } 
        
    }
    public void bindData()
    {
        
        if (rowCount==0)
        {
            testModuleType = new ASCXTestplanType[1];

            for (byte i = 0; i < testModuleType.Length; i++)
            {
                testModuleType[i] = (ASCXTestplanType)Page.LoadControl("~/Frame/TestPlan/TestplanType.ascx");
      
                testModuleType[i].ConfitTh1text = "名称";
                testModuleType[i].ConfitTh2text = "MSA名称";
                testModuleType[i].ContentTRVisible = false;
             
                this.plhMain.Controls.Add(testModuleType[i]);

            }
        } 
        else
        {
            testModuleType = new ASCXTestplanType[rowCount];

            for (byte i = 0; i < testModuleType.Length; i++)
            {
                testModuleType[i] = (ASCXTestplanType)Page.LoadControl("~/Frame/TestPlan/TestplanType.ascx");
                testModuleType[i].ConfigLbText = MSAIDtoName(Convert.ToInt32(mydt.Rows[i]["MSAID"]));
                //testModuleType[i].ConfigLb2Text = mydt.Rows[i]["IgnoreFlag"].ToString().ToUpper().Trim();
                testModuleType[i].ID = mydt.Rows[i]["ID"].ToString().ToUpper().Trim();
                testModuleType[i].LinkText1 = mydt.Rows[i]["ItemName"].ToString().Trim();
                testModuleType[i].pIDString = mydt.Rows[i]["ID"].ToString().ToUpper().Trim();
                testModuleType[i].PostBackUrlString = "~/WebFiles/MaintainInfo/MCoefGroup/GlobalMCoefList.aspx?uId=" + mydt.Rows[i]["ID"].ToString();
                testModuleType[i].ConfitTh1text = "名称";
                testModuleType[i].ConfitTh2text = "MSA名称";
                //testModuleType[i].ConfitTh3text = mydt.Columns[3].ColumnName;
                if (i >= 1)
                {
                    testModuleType[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        testModuleType[i].TrBackgroundColor = "#F2F2F2";
                    }
                }
                this.plhMain.Controls.Add(testModuleType[i]);

            }
        }
      
       
    }
    public void configTHText()
    {
      
    }
   
    public string MSAIDtoName(int msaid)
    {
        string msaname = "";
        try
        {
            {
                for (int i = 0; i < mydtMSA.Rows.Count;i++)
                {
                    if (Convert.ToInt32(mydtMSA.Rows[i]["ID"]) == msaid)
                    {
                        msaname = mydtMSA.Rows[i]["ItemName"].ToString().Trim();
                    }                    
                }

                return msaname;
            }
            
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw (ex);
        }
    }    
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalProductionType where IgnoreFlag='False'", "GlobalProductionType");
                rowCount = mydt.Rows.Count;
                mydtMSA = pDataIO.GetDataTable("select * from GlobalMSA", "GlobalMSA");
                bindData();                
                CommCtrl pCtrl = new CommCtrl();
                //if (Request.QueryString["BlockType"]!=null)                
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
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
        return true;

    }
}