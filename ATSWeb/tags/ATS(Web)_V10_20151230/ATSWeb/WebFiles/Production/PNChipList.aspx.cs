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
public partial class WebFiles_Production_PNChipList : BasePage
{
    string funcItemName = "PNChipList";
    public DataTable mydt = new DataTable();
    Frame_Production_PNChipList[]prductionChipList;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
   
    public WebFiles_Production_PNChipList()
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
              prductionChipList = new Frame_Production_PNChipList[1];
        
        for (byte i = 0; i < prductionChipList.Length; i++)
        {
            prductionChipList[i] = (Frame_Production_PNChipList)Page.LoadControl("../../Frame/Production/PNChipList.ascx");
           
           
            prductionChipList[i].ContentTRVisible = false;
            this.ChipPNList.Controls.Add(prductionChipList[i]);
        }
        } 
        else
        {
              prductionChipList = new Frame_Production_PNChipList[rowCount];
        
        for (byte i = 0; i < prductionChipList.Length; i++)
        {
            prductionChipList[i] = (Frame_Production_PNChipList)Page.LoadControl("../../Frame/Production/PNChipList.ascx");
            prductionChipList[i].ID =  mydt.Rows[i]["ID"].ToString().Trim();
            prductionChipList[i].LbTH2Text = GetChipName(Convert.ToInt32(mydt.Rows[i]["ChipID"]));
            prductionChipList[i].LbTH3Text = ChangeDriverTypeNubertoName(mydt.Rows[i]["ChipRoleID"].ToString().Trim());
            prductionChipList[i].LbTH4Text = mydt.Rows[i]["ChipDirection"].ToString().Trim();
            if ( mydt.Rows[i]["ChipDirection"].ToString().Trim() == "0")
            {
                prductionChipList[i].LbTH4Text = "Tx";
            }
            else if (mydt.Rows[i]["ChipDirection"].ToString().Trim() == "1")
            {
                 prductionChipList[i].LbTH4Text = "Rx";
            }
            else if (mydt.Rows[i]["ChipDirection"].ToString().Trim() == "2")
            {
                prductionChipList[i].LbTH4Text = "Tx&Rx";
            }

            prductionChipList[i].PostBackUrlStringPNChipSelf = "~/WebFiles/Production/PNChipSelf.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
            prductionChipList[i].PostBackUrlStringChannelMap = "~/WebFiles/Production/ChannelMapList.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();
          
            prductionChipList[i].BeSelected = false;
            if (i >= 1)
            {
                prductionChipList[i].LBTH2Visible = false;
                prductionChipList[i].LBTH3Visible = false;
                prductionChipList[i].LBTH4Visible = false;
                prductionChipList[i].LBTH5Visible = false;
               
                prductionChipList[i].LBTHTitleVisible(false);
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.ChipPNList.Controls.Add(prductionChipList[i]);
        }
        }
      

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from PNChipMap where PNID=" + moduleTypeID, "PNChipMap");
                rowCount = mydt.Rows.Count;               
                bindData();
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
        string deletStr = "select * from GlobalProductionName IgnoreFlag='False'and where PID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < prductionChipList.Length; i++)
            {
                Frame_Production_PNChipList cb = (Frame_Production_PNChipList)ChipPNList.FindControl(prductionChipList[i].ID);
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
            int result = pDataIO.UpdateWithProc("PNChipMap", mydt, deletStr, logTracingString);
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
            Response.Redirect("~/WebFiles/Production/PNChipSelf.aspx?AddNew=true&uId=" + moduleTypeID);

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    public void ConfigOptionButtonsVisible()
    {
        int myAccessCode = 0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;
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
        if (prductionChipList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < prductionChipList.Length; i++)
        {
            prductionChipList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (prductionChipList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < prductionChipList.Length; i++)
        {
            prductionChipList[i].BeSelected = false;
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
    public string GetChipName(int chipid)
    {
        string chipName = "";
        try
        {

            if (pDataIO.OpenDatabase(true))
            {
                DataTable chipTable = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + chipid, "ChipBaseInfo");
                if (chipTable.Rows.Count==1)
                {
                    chipName = chipTable.Rows[0]["ItemName"].ToString();
                } 
               
            }
            return chipName;
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
}