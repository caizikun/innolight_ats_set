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
public partial class ASPXTopEEROMList : BasePage
{
    string funcItemName = "TopE2ROMList";
    public DataTable mydt = new DataTable();
    ASCXTopE2ROMList[] e2romList;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    public DataTable mydtCoefs = new DataTable();
    private SortedList<int, string> MCoefsIDMap = new SortedList<int, string>();
    private algorithm alg = new algorithm();
    private string ModuleTypeName = "";
    public ASPXTopEEROMList()
    {
        conn = "inpcsz0518\\ATS_HOME";
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
        mydt.Clear();
        MCoefsIDMap.Clear();
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
            e2romList = new ASCXTopE2ROMList[1];

            for (byte i = 0; i < e2romList.Length; i++)
            {
                e2romList[i] = (ASCXTopE2ROMList)Page.LoadControl("../../Frame/Production/TopE2ROMList.ascx");
                e2romList[i].TH2TEXT = mydt.Columns[2].ColumnName;
                e2romList[i].TH3TEXT = mydt.Columns[3].ColumnName;
                e2romList[i].TH5TEXT = mydt.Columns[5].ColumnName;
                e2romList[i].TH7TEXT = mydt.Columns[7].ColumnName;
                e2romList[i].TH9TEXT = mydt.Columns[9].ColumnName;
                e2romList[i].ContentTRVisible = false;
            
               
                this.EEPROMList.Controls.Add(e2romList[i]);
            }
        } 
        else
        {
            e2romList = new ASCXTopE2ROMList[rowCount];

            for (byte i = 0; i < e2romList.Length; i++)
            {
                e2romList[i] = (ASCXTopE2ROMList)Page.LoadControl("../../Frame/Production/TopE2ROMList.ascx");
                e2romList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();

                e2romList[i].LinkButtonSelfInforText = mydt.Rows[i]["ItemName"].ToString().Trim();
                if (CheckDataCRC(mydt.Rows[i]["Data0"].ToString(), mydt.Rows[i]["CRCData0"].ToString()))
                {
                    e2romList[i].ConfigData0StatusText = "pass";
                }
                else
                {
                    e2romList[i].ConfigData0StatusText = "fail";
                }
                if (CheckDataCRC(mydt.Rows[i]["Data1"].ToString(), mydt.Rows[i]["CRCData1"].ToString()))
                {
                    e2romList[i].ConfigData1StatusText = "pass";
                }
                else
                {
                    e2romList[i].ConfigData1StatusText = "fail";
                }
                if (CheckDataCRC(mydt.Rows[i]["Data2"].ToString(), mydt.Rows[i]["CRCData2"].ToString()))
                {
                    e2romList[i].ConfigData2StatusText = "pass";
                }
                else
                {
                    e2romList[i].ConfigData2StatusText = "fail";
                }
                if (CheckDataCRC(mydt.Rows[i]["Data3"].ToString(), mydt.Rows[i]["CRCData3"].ToString()))
                {
                    e2romList[i].ConfigData3StatusText = "pass";
                }
                else
                {
                    e2romList[i].ConfigData3StatusText = "fail";
                }
                e2romList[i].TH2TEXT = mydt.Columns[2].ColumnName;
                e2romList[i].TH3TEXT = mydt.Columns[3].ColumnName;
                e2romList[i].TH5TEXT = mydt.Columns[5].ColumnName;
                e2romList[i].TH7TEXT = mydt.Columns[7].ColumnName;
                e2romList[i].TH9TEXT = mydt.Columns[9].ColumnName;
                e2romList[i].PostBackUrlStringSelfInfor = "~/WebFiles/Production/E2ROMSelfInfor.aspx?AddNew=false&uId=" + mydt.Rows[i]["ID"].ToString().Trim();

                ConfigPageNumber(e2romList[i], mydt.Rows[i]["ID"].ToString().Trim());
                e2romList[i].BeSelected = false;
                if (i >= 1)
                {
                    e2romList[i].EnabelTH2Visible = false;
                    e2romList[i].EnabelTH3Visible = false;
                    e2romList[i].EnabelTH4Visible = false;
                    e2romList[i].EnabelTH5Visible = false;
                    e2romList[i].EnabelTH6Visible = false;
                    e2romList[i].EnabelTH7Visible = false;
                    e2romList[i].EnabelTH8Visible = false;
                    e2romList[i].EnabelTH9Visible = false;
                    e2romList[i].EnabelTH10Visible = false;
                    e2romList[i].LBTHTitleVisible(false);
                }
                //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
                this.EEPROMList.Controls.Add(e2romList[i]);
            }
        }
       

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoMSAEEPROMSet where PID=" + moduleTypeID, "TopoMSAEEPROMSet");
                //mydt = pDataIO.GetDataTable("select * from TopoMSAEEPROMSet where PID='6'", "TopoMSAEEPROMSet");
                string tempPid="";
                DataTable temp = pDataIO.GetDataTable("select * from GlobalProductionName where ID=" + moduleTypeID, "GlobalProductionName");
                //DataTable temp = pDataIO.GetDataTable("select * from GlobalProductionName where ID='6'", "GlobalProductionName");
                tempPid = temp.Rows[0]["PID"].ToString().Trim();
                temp = pDataIO.GetDataTable("select * from GlobalProductionType where ID=" + tempPid, "GlobalProductionType");
                tempPid = temp.Rows[0]["MSAID"].ToString().Trim();
                temp = pDataIO.GetDataTable("select * from GlobalMSA where ID=" + tempPid, "GlobalMSA");
                ModuleTypeName = temp.Rows[0]["ItemName"].ToString().Trim();
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
        string deletStr = "select * from TopoMSAEEPROMSet and where PID=" + moduleTypeID;
        try
        {
            for (int i = 0; i < e2romList.Length; i++)
            {
                ASCXTopE2ROMList cb = (ASCXTopE2ROMList)EEPROMList.FindControl(e2romList[i].ID);
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
            int result = pDataIO.UpdateWithProc("TopoMSAEEPROMSet", mydt, deletStr, logTracingString);
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
            Response.Redirect("~/WebFiles/Production/E2ROMSelfInfor.aspx?AddNew=true&uId=" + moduleTypeID);

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
        OptionButtons1.ConfigBtCancelVisible = false;
        OptionButtons1.ConfigBtEditVisible = false;       
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
        if (e2romList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < e2romList.Length; i++)
        {
            e2romList[i].BeSelected = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (e2romList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < e2romList.Length; i++)
        {
            e2romList[i].BeSelected = false;
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
    public bool ConfigMCoefs()
    {
        MCoefsIDMap.Clear();
        try
        {
            for (int i = 0; i < mydtCoefs.Rows.Count; i++)
            { 
                MCoefsIDMap.Add(Convert.ToInt32(mydtCoefs.Rows[i]["ID"]),mydtCoefs.Rows[i]["ItemName"].ToString());
            }           
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
    public bool CheckDataCRC(string checkdata,string crcdata)
    {
        bool CRCStatus = false;
        byte crcData = 0;
        try
        {
            if (crcdata != "" && crcdata!=null)
            {
              crcData= Convert.ToByte(crcdata);
            }
            if (alg.CRC8(alg.strToHexByte(checkdata)) == crcData)
          {
              CRCStatus = true;
          }
            return CRCStatus;

        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public void ConfigPageNumber(ASCXTopE2ROMList input,string ItemID)
    {
        try
        {
            switch (ModuleTypeName)
            {
                case "SFF8636":                   
                    {
                        input.PostBackUrlStringData0 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=0&uId=" + ItemID;
                        input.PostBackUrlStringData1 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=1&uId=" + ItemID;
                        input.PostBackUrlStringData2 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=2&uId=" + ItemID;
                        input.PostBackUrlStringData3 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=3&uId=" + ItemID;
                        break;
                    }
                case "SFF8472":
                    {
                        input.PostBackUrlStringData0 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=0&uId=" + ItemID;
                        input.PostBackUrlStringData1 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=1&uId=" + ItemID;
                        input.PostBackUrlStringData2 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=2&uId=" + ItemID;
                        input.PostBackUrlStringData3 = "~/WebFiles/Production/E2ROMDataInfor.aspx?PageNumber=3&uId=" + ItemID;
                        
                        break;
                    }
                    default:
                    break;
            }
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