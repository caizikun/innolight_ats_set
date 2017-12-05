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
    string funcItemName = "ChipsetIniList";
    public DataTable mydt = new DataTable();
    ASCXChipSetContrl[] chipsetIni;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    public ASPXChipSetIniList()
    {
        conn = "inpcsz0518\\ATS_HOME";
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
        
        {
            IsSessionNull();
            SetSessionBlockType(2);
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
            chipsetIni[i] = (ASCXChipSetContrl)Page.LoadControl("../../Frame/Production/ChipSetContrl.ascx");
          

            chipsetIni[i].LbTH2 = "Index";
            chipsetIni[i].LbTH3 = mydt.Columns[2].ColumnName;
            chipsetIni[i].LbTH4 = "DebugLine";
            chipsetIni[i].LbTH5 = mydt.Columns[4].ColumnName;
            chipsetIni[i].LbTH6 = mydt.Columns[5].ColumnName;
            chipsetIni[i].LbTH7 = mydt.Columns[6].ColumnName;
            chipsetIni[i].ContentTRVisible = false;
         
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.ChipsetIniList.Controls.Add(chipsetIni[i]);
        }
        } 
        else
        {
              chipsetIni = new ASCXChipSetContrl[rowCount];
        for (byte i = 0; i < chipsetIni.Length; i++)
        {
            chipsetIni[i] = (ASCXChipSetContrl)Page.LoadControl("../../Frame/Production/ChipSetContrl.ascx");
            chipsetIni[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
            chipsetIni[i].LbTH2Text =i.ToString().Trim();
            chipsetIni[i].LbTH3Text = ChangeDriverTypeNubertoName(mydt.Rows[i]["DriveType"].ToString().Trim());
            chipsetIni[i].LbTH4Text = mydt.Rows[i]["ChipLine"].ToString().Trim();
            chipsetIni[i].LbTH5Text = mydt.Rows[i]["RegisterAddress"].ToString().Trim();
            chipsetIni[i].LbTH6Text = mydt.Rows[i]["Length"].ToString().Trim();
            chipsetIni[i].LbTH7Text = mydt.Rows[i]["ItemValue"].ToString().Trim();

            chipsetIni[i].LbTH2 = "Index";
            chipsetIni[i].LbTH3 = mydt.Columns[2].ColumnName;
            chipsetIni[i].LbTH4 = "DebugLine";
            chipsetIni[i].LbTH5 = mydt.Columns[4].ColumnName;
            chipsetIni[i].LbTH6 = mydt.Columns[5].ColumnName;
            chipsetIni[i].LbTH7 = mydt.Columns[6].ColumnName;
            chipsetIni[i].PostBackUrlStringPNSelf = "~/WebFiles/Production/ChipSetIniSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim() + "&uIndex=" + chipsetIni[i].LbTH2Text;

            chipsetIni[i].BeSelected = false;
            if (i >= 1)
            {
                chipsetIni[i].LBTH2Visible = false;
                chipsetIni[i].LBTH3Visible = false;
                chipsetIni[i].LBTH4Visible = false;
                chipsetIni[i].LBTH5Visible = false;
                chipsetIni[i].LBTH6Visible = false;
                chipsetIni[i].LBTH7Visible = false;
                chipsetIni[i].LBTHTitleVisible(false);
                
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
                mydt = pDataIO.GetDataTable("select * from GlobalManufactureChipsetInitialize where PID=" + moduleTypeID + "ORDER BY DriveType,RegisterAddress,ChipLine", "GlobalManufactureChipsetInitialize");
                rowCount = mydt.Rows.Count;
                bindData();
                string parentItem = pDataIO.getDbCmdExecuteScalar("select PN from GlobalProductionName where id = " + moduleTypeID).ToString();
                CommCtrl pCtrl = new CommCtrl();
                Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
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
        string deletStr = "select * from GlobalManufactureChipsetInitialize where PID=" + moduleTypeID + "ORDER BY DriveType,RegisterAddress,ChipLine";
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
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('Did not choose any one！');return false;</script>");
                this.Page.RegisterStartupScript("", "<script>alert('Did not choose any one！');</script>");
                return false;
            }
            int result = pDataIO.UpdateWithProc("GlobalManufactureChipsetInitialize", mydt, deletStr, logTracingString);
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
            Response.Redirect("~/WebFiles/Production/ChipSetIniSelfInfor.aspx?AddNew=true&uId=" + moduleTypeID);

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
        bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.AddProduction, myAccessCode);
        bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.DeleteProduction, myAccessCode);
        if (addVisible == false && deleteVisible == false)
        {
            bool pnEdit = PNAuthority();
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