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
public partial class WebFiles_Production_ChannelMapList : BasePage
{
    string funcItemName = "通道映射";
    public DataTable mydt = new DataTable();
    Frame_Production_PNChannelMap[] pnChannelMap;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    string parentItem = "";

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
            pnChannelMap = new Frame_Production_PNChannelMap[1];
      
        for (byte i = 0; i < pnChannelMap.Length; i++)
        {
            pnChannelMap[i] = (Frame_Production_PNChannelMap)Page.LoadControl("~/Frame/Production/PNChannelMap.ascx");
           
            pnChannelMap[i].ContentTRVisible = false;
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.PNChipMapList.Controls.Add(pnChannelMap[i]);
        }

        } 
        else
        {
            pnChannelMap = new Frame_Production_PNChannelMap[rowCount];
      
        for (byte i = 0; i < pnChannelMap.Length; i++)
        {
            pnChannelMap[i] = (Frame_Production_PNChannelMap)Page.LoadControl("~/Frame/Production/PNChannelMap.ascx");
            pnChannelMap[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            pnChannelMap[i].LbTH2Text = parentItem;
            pnChannelMap[i].LbTH3Text = mydt.Rows[i]["ModuleLine"].ToString().Trim();
            pnChannelMap[i].LbTH4Text = mydt.Rows[i]["ChipLine"].ToString().Trim();
            pnChannelMap[i].LbTH5Text = mydt.Rows[i]["DebugLine"].ToString().Trim();
            pnChannelMap[i].PostBackUrlStringPNChipMapSelf = "~/WebFiles/Production_ATS/Production/pnChannelMapSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
          
            pnChannelMap[i].BeSelected = false;
            if (i >= 1)
            {
                pnChannelMap[i].LBTHTitleVisible(false);
                if (i % 2 != 0)
                {
                    pnChannelMap[i].TrBackgroundColor = "#F2F2F2";
                }
                
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.PNChipMapList.Controls.Add(pnChannelMap[i]);
        }

        }
        
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChannelMap where PNChipID=" + moduleTypeID, "ChannelMap");
                rowCount = mydt.Rows.Count;
                string pnid = pDataIO.getDbCmdExecuteScalar("select PNID from PNChipMap where id = " + moduleTypeID).ToString();
                parentItem = pDataIO.getDbCmdExecuteScalar("select PN from GlobalProductionName where id = " + pnid).ToString();
                CommCtrl pCtrl = new CommCtrl();
                bindData();

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
        string deletStr = "select * from ChannelMap where PNChipID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < pnChannelMap.Length; i++)
            {
                Frame_Production_PNChannelMap cb = (Frame_Production_PNChannelMap)PNChipMapList.FindControl(pnChannelMap[i].ID);
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
                result = pDataIO.UpdateWithProc("ChannelMap", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("ChannelMap", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
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
            Response.Redirect("~/WebFiles/Production_ATS/Production/pnChannelMapSelfInfor.aspx?AddNew=true&uId=" + moduleTypeID);

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
        OptionButtons1.ConfigBtEditVisible =false;
        OptionButtons1.ConfigBtCancelVisible = false;
        #region PNAuthority
        bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
        if (addVisible == false && deleteVisible==false)
        {
            bool pnEdit=  PNAuthority();
            OptionButtons1.ConfigBtAddVisible = pnEdit;

            if (rowCount <= 0)
            {
                 OptionButtons1.ConfigBtDeleteVisible = false;
            }
            else
            {
                 OptionButtons1.ConfigBtDeleteVisible = pnEdit;
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
        if (pnChannelMap.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < pnChannelMap.Length; i++)
        {
            pnChannelMap[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (pnChannelMap.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < pnChannelMap.Length; i++)
        {
            pnChannelMap[i].BeSelected = false;
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
    public bool PNAuthority()
    {
        bool editVisibal = false;
        string userID = Session["UserID"].ToString().Trim();
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                DataTable selectPNid = pDataIO.GetDataTable("SELECT * FROM PNChipMap,ChannelMap where PNChipMap.ID = ChannelMap.PNChipID and ChannelMap.ID =" + moduleTypeID, "ChannelMap");
                string StrPNId = selectPNid.Rows[0]["PNID"].ToString().Trim();
                DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + StrPNId, "UserPNAction");
                //DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where PNID=" + moduleTypeID, "UserPNAction");
                if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                {
                    return false;
                }
                else
                {
                    if (temp.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "1")
                    {
                        editVisibal = true;
                    }
                    else
                    {
                        editVisibal = false;
                    }

                }
            }
            return editVisibal;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}