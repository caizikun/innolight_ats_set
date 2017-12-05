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
public partial class ASPXChipSetIniList : BasePage
{
    string funcItemName = "芯片初始化(测试方案)";
    public DataTable mydt = new DataTable();
    ASCXChipSetContrl[] chipsetIni;
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
        chipsetIni = new ASCXChipSetContrl[1];
        for (byte i = 0; i < chipsetIni.Length; i++)
        {
            chipsetIni[i] = (ASCXChipSetContrl)Page.LoadControl("~/Frame/Production/ChipSetContrl.ascx");

            chipsetIni[i].LbTH2 = "名称";
            chipsetIni[i].LbTH3 = "芯片类型";
            chipsetIni[i].LbTH4 = "调试通道";
            chipsetIni[i].LbTH5 = "寄存器地址";
            chipsetIni[i].LbTH6 = "长度";
            chipsetIni[i].LbTH7 = "值";
            chipsetIni[i].ContentTRVisible = false;
            chipsetIni[i].Column8Visible = false;
            chipsetIni[i].Column9Visible = false;
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.ChipsetIniList.Controls.Add(chipsetIni[i]);
        }
        } 
        else
        {
              chipsetIni = new ASCXChipSetContrl[rowCount];
        for (byte i = 0; i < chipsetIni.Length; i++)
        {
            chipsetIni[i] = (ASCXChipSetContrl)Page.LoadControl("~/Frame/Production/ChipSetContrl.ascx");
            chipsetIni[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            chipsetIni[i].LbTH2Text = mydt.Rows[i]["ItemName"].ToString().Trim();
            chipsetIni[i].LbTH3Text = ChangeDriverTypeNubertoName(mydt.Rows[i]["DriveType"].ToString().Trim());
            chipsetIni[i].LbTH4Text = mydt.Rows[i]["ChipLine"].ToString().Trim();
            chipsetIni[i].LbTH5Text = mydt.Rows[i]["RegisterAddress"].ToString().Trim();
            chipsetIni[i].LbTH6Text = mydt.Rows[i]["Length"].ToString().Trim();
            chipsetIni[i].LbTH7Text = mydt.Rows[i]["ItemValue"].ToString().Trim();

            chipsetIni[i].LbTH2 = "名称";
            chipsetIni[i].LbTH3 = "芯片类型";
            chipsetIni[i].LbTH4 = "调试通道";
            chipsetIni[i].LbTH5 = "寄存器地址";
            chipsetIni[i].LbTH6 = "长度";
            chipsetIni[i].LbTH7 = "值";
            chipsetIni[i].PostBackUrlStringPNSelf = "~/WebFiles/Production_ATS/TestPlan/ChipSetIniSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim() + "&uIndex=" + chipsetIni[i].LbTH2Text;

            chipsetIni[i].BeSelected = false;
            chipsetIni[i].Column8Visible = false;
            chipsetIni[i].Column9Visible = false;
            if (i >= 1)
            {
                chipsetIni[i].LBTHTitleVisible(false);
                if (i % 2 != 0)
                {
                    chipsetIni[i].TrBackgroundColor = "#F2F2F2";
                }
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.ChipsetIniList.Controls.Add(chipsetIni[i]);
        }
        }
      

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoManufactureChipsetInitialize where PID=" + moduleTypeID + "ORDER BY DriveType,RegisterAddress,ChipLine", "GlobalManufactureChipsetInitialize");
                rowCount = mydt.Rows.Count;
                bindData();

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "2"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.PN from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                string uid2 = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.ID from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + moduleTypeID).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + uid2;

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                string uid = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + uid;

                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "3_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + moduleTypeID;
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();

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
        string deletStr = "select * from TopoManufactureChipsetInitialize where PID=" + moduleTypeID + "ORDER BY DriveType,RegisterAddress,ChipLine";
        try
        {
            for (int i = 0; i < chipsetIni.Length; i++)
            {
                ASCXChipSetContrl cb = (ASCXChipSetContrl)ChipsetIniList.FindControl(chipsetIni[i].ID);
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
                result = pDataIO.UpdateWithProc("TopoManufactureChipsetInitialize", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("TopoManufactureChipsetInitialize", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
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
            Response.Redirect("~/WebFiles/Production_ATS/TestPlan/ChipSetIniSelfInfor.aspx?AddNew=true&uId=" + moduleTypeID);

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
        if (chipsetIni.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < chipsetIni.Length; i++)
        {
            chipsetIni[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (chipsetIni.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < chipsetIni.Length; i++)
        {
            chipsetIni[i].BeSelected = false;
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
    public string ChangeDriverTypeNubertoName(string number)
    {
        string name="";
        try
        {
            switch (number)
            {
                case "0":
                    {
                        name = "LDD";
                        break;
                    }
                case "1":
                    {
                        name = "AMP";
                        break;
                    }
                case "2":
                    {
                        name = "DAC";
                        break;
                    }
                case "3":
                    {
                        name = "CDR";
                        break;
                    }
                default:
                    break;
            }
            return name;
        }
        catch (System.Exception ex)
        {
            throw ex;
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

                DataTable pnId = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + moduleTypeID, "TopoTestPlan");

                if (pnId.Rows.Count == 1)
                {
                    string PNid = pnId.Rows[0]["PID"].ToString();
                    DataTable pnAuthority = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + PNid, "UserPNAction");
                    if (pnAuthority.Rows.Count == 1)
                    {
                        if (pnAuthority.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "TRUE" || pnAuthority.Rows[0]["ModifyPN"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                    }              
                }


                {
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + moduleTypeID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                        deletePlan = false;
                        //tpAuthority = false;
                    }
                    else
                    {
                        if (temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                        //else
                        //{
                        //    tpAuthority = false;
                        //}
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