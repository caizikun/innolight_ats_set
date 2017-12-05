using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATSDataBase;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Collections;


public partial class A_MasterPage : System.Web.UI.MasterPage
{

    DataIO pDataIO;
    string currID = "";
    long myAccessCode =0;
    public A_MasterPage()
    {
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        if (myAccessCode == 85004561 && Request.Url.ToString().Contains("UserRoleFunc"))
        {
            Response.Redirect("~/Home.aspx");  
        }
        else
        {
            initPage();
        }
        
       
    }

    bool initPage()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();
            Table pTable = new Table();
            //OptionButtons1.Visible = false;
            if (Session["UserName"] != null && Session["UserID"] != null && Session["AccCode"] != null)
            {
                UserAccountInfo.hlkUserTxt = Session["UserName"].ToString();
                UserAccountInfo.confighlkUserIDDIV = Session["UserName"].ToString();
            }
            else
            {
                //Response.Write("<script>alert('未找到当前用户!系统自动关闭!')</script>");
                //Response.Write("<script>window.opener = null;window.open('','_self');window.close();</script>");
                //return false;
            }


            if (pDataIO.OpenDatabase(true))
            {
                TableRow tr = new TableRow();
                HyperLink hlkHome = pCtrl.CreateHlk("Home");
                TableCell tcHome = new TableCell();
                tcHome.Controls.Add(hlkHome);
                tr.Cells.Add(tcHome);

                DataTable funcBlockInfoDt = pDataIO.GetDataTable("select * from FunctionTable where blockLevel =0 order by BlockTypeID", "FunctionTable");
                string[] columns = new string[2] { "BlockTypeID", "ItemName" };
                DataTable dt = funcBlockInfoDt.DefaultView.ToTable(true, columns);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string funcName = dt.Rows[i]["ItemName"].ToString().ToUpper();
                    HyperLink hlk = pCtrl.CreateHlk(dt.Rows[i]["ItemName"].ToString(), dt.Rows[i]["BlockTypeID"].ToString());
                    
                    TableCell tc = new TableCell();
                    if ((myAccessCode & 0x0F) > 0 && funcName=="ATSPlan".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0xF0) > 0 && funcName == "ProductionInfo".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }

                    else if ((myAccessCode & 0xF00) > 0 && funcName == "MSAInfo".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0xF000) > 0 && funcName == "MCoefGroup".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0xF0000) > 0 && funcName == "AppModel".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0xF00000) > 0 && funcName == "Equipment".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0x1000000) > 0 && funcName == "TestData".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0x40000000) > 0 && funcName == "UserRoleFunction".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0xC000000) > 0 && funcName == "GlobalSpecs".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                    else if ((myAccessCode & 0x30000000) > 0 && funcName == "ChipINfor".ToUpper())
                    {
                        tc.Controls.Add(hlk);
                        tr.Cells.Add(tc);
                    }
                }
                pTable.Rows.Add(tr);

                phlList.Controls.Add(pTable);
                phlList.Controls.Add(new HtmlGenericControl("hr"));
            }
            return true;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
}