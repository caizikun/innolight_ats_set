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
public partial class ASPXChipSetControlList : BasePage
{
    string funcItemName = "芯片控制";
    public DataTable mydt = new DataTable();
    ASCXChipSetContrl[] chipsetControl;
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
            chipsetControl = new ASCXChipSetContrl[1];
      
        for (byte i = 0; i < chipsetControl.Length; i++)
        {
            chipsetControl[i] = (ASCXChipSetContrl)Page.LoadControl("~/Frame/Production/ChipSetContrl.ascx");
           
            chipsetControl[i].LbTH2= "名称";
            chipsetControl[i].LbTH3= "模块通道";
            chipsetControl[i].LbTH4 ="调试通道";
            chipsetControl[i].LbTH5= "芯片类型";
            chipsetControl[i].LbTH6 = "寄存器地址";
            chipsetControl[i].LbTH7 = "字节长度";
            chipsetControl[i].LbTH8 = "开始位";
            chipsetControl[i].LbTH9 = "结束位";
            chipsetControl[i].ContentTRVisible = false;
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.ChipsetContrlList.Controls.Add(chipsetControl[i]);
        }

        } 
        else
        {
            chipsetControl = new ASCXChipSetContrl[rowCount];
      
        for (byte i = 0; i < chipsetControl.Length; i++)
        {
            chipsetControl[i] = (ASCXChipSetContrl)Page.LoadControl("~/Frame/Production/ChipSetContrl.ascx");
            chipsetControl[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            chipsetControl[i].LbTH2Text = mydt.Rows[i]["ItemName"].ToString().Trim();
            chipsetControl[i].LbTH3Text = mydt.Rows[i]["ModuleLine"].ToString().Trim();
            chipsetControl[i].LbTH4Text = mydt.Rows[i]["ChipLine"].ToString().Trim();
            chipsetControl[i].LbTH5Text = ChangeDriverTypeNubertoName(mydt.Rows[i]["DriveType"].ToString().Trim());
            chipsetControl[i].LbTH6Text = mydt.Rows[i]["RegisterAddress"].ToString().Trim();
            chipsetControl[i].LbTH7Text = mydt.Rows[i]["Length"].ToString().Trim();
            chipsetControl[i].LbTH8Text = mydt.Rows[i]["StartBit"].ToString().Trim();
            chipsetControl[i].LbTH9Text = mydt.Rows[i]["EndBit"].ToString().Trim();

            chipsetControl[i].LbTH2 = "名称";
            chipsetControl[i].LbTH3 = "模块通道";
            chipsetControl[i].LbTH4 = "调试通道";
            chipsetControl[i].LbTH5 = "芯片类型";
            chipsetControl[i].LbTH6 = "寄存器地址";
            chipsetControl[i].LbTH7 = "字节长度";
            chipsetControl[i].LbTH8 = "开始位";
            chipsetControl[i].LbTH9 = "结束位";
            chipsetControl[i].PostBackUrlStringPNSelf = "~/WebFiles/Production_ATS/Production/ChipSetControlSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
          
            chipsetControl[i].BeSelected = false;
            if (i >= 1)
            {
                chipsetControl[i].LBTHTitleVisible(false);
                if (i % 2 != 0)
                {
                    chipsetControl[i].TrBackgroundColor = "#F2F2F2";
                }
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.ChipsetContrlList.Controls.Add(chipsetControl[i]);
        }

        }
        
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from GlobalManufactureChipsetControl where PID=" + moduleTypeID + "ORDER BY DriveType,RegisterAddress,ItemName,ModuleLine,ChipLine", "GlobalManufactureChipsetControl");
                rowCount = mydt.Rows.Count;
                bindData();

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";

                HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + moduleTypeID).ToString();
                string uid = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + moduleTypeID).ToString();
                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + uid;

                HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + moduleTypeID.Trim();
                string parentItem = pDataIO.getDbCmdExecuteScalar("select PN from GlobalProductionName where id = " + moduleTypeID).ToString();
               
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
        string deletStr = "select * from GlobalManufactureChipsetControl where PID=" + moduleTypeID+ "ORDER BY DriveType,RegisterAddress,ItemName,ModuleLine,ChipLine";
        try
        {
            for (int i = 0; i < chipsetControl.Length; i++)
            {
                ASCXChipSetContrl cb = (ASCXChipSetContrl)ChipsetContrlList.FindControl(chipsetControl[i].ID);
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
                result = pDataIO.UpdateWithProc("GlobalManufactureChipsetControl", mydt, deletStr, logTracingString, "ATS_V2");
            }
            else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
            {
                result = pDataIO.UpdateWithProc("GlobalManufactureChipsetControl", mydt, deletStr, logTracingString, "ATS_VXDEBUG");
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
            Response.Redirect("~/WebFiles/Production_ATS/Production/ChipSetControlSelfInfor.aspx?AddNew=true&uId=" + moduleTypeID);

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
        if (chipsetControl.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < chipsetControl.Length; i++)
        {
            chipsetControl[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (chipsetControl.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < chipsetControl.Length; i++)
        {
            chipsetControl[i].BeSelected = false;
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
        string name = "";
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
    public bool PNAuthority()
    {
        bool editVisibal = false;
        string userID = Session["UserID"].ToString().Trim();
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                DataTable temp = pDataIO.GetDataTable("select * from UserPNAction where UserID=" + userID + "and PNID=" + moduleTypeID, "UserPNAction");
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