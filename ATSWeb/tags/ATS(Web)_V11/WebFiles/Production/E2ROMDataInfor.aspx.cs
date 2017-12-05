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
using System.Text.RegularExpressions;
public partial class ASPXE2ROMDataInfor : BasePage
{
    string funcItemName = "E2ROMData0Infor";
    public DataTable mydt = new DataTable();
    ASCXE2ROMDataInfor[] e2romDataList;
    byte[] contents;
    private int rowCount;
    string moduleTypeID = "";
    private string conn;
    private DataIO pDataIO;
    private string logTracingString = "";
    private SByte PageData = -1;
  
    private algorithm alg = new algorithm();
    private analysisMSA analyMSA = new analysisMSA();
    private ValidateExpression ValiExpression = new ValidateExpression();
    private UInt32 pagelength = 0;
    private UInt32 pageStartAddress = 0;
    public ASPXE2ROMDataInfor()
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
            if (Request.QueryString["PageNumber"] != null)
            {
                PageData = Convert.ToSByte((Request.QueryString["PageNumber"]));
            }
            SelectFuncItemName();
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
        }
        
    }

    public void bindData()
    {
       
        if (rowCount==0)
        {
            e2romDataList = new ASCXE2ROMDataInfor[1];
            for (byte i = 0; i < e2romDataList.Length; i++)
            {
                e2romDataList[i] = (ASCXE2ROMDataInfor)Page.LoadControl("../../Frame/Production/E2ROMDataInfor.ascx");
                e2romDataList[i].ContentTRVisible = false;
                this.E2ROMDataInfor.Controls.Add(e2romDataList[i]);
            }
        } 
        else
        {
              e2romDataList = new ASCXE2ROMDataInfor[rowCount];
        contents= PageContent();
        string ItemName="";
        string ItemDescription="";
        for (int i = 0; i < e2romDataList.Length; i++)
        {
            
            e2romDataList[i] = (ASCXE2ROMDataInfor)Page.LoadControl("../../Frame/Production/E2ROMDataInfor.ascx");
           
            e2romDataList[i].ID = i.ToString().Trim();
            e2romDataList[i].ConfigAddressHexText = alg.byteToHexStr((byte)(i + pageStartAddress));
            e2romDataList[i].ConfigAddressDecText = (i + pageStartAddress).ToString().Trim();
           
            
                e2romDataList[i].ConfigContentText = alg.byteToHexStr(contents[i]);

                if (Regex.IsMatch(e2romDataList[i].ConfigContentText, "[\\da-fA-F]{2}") && e2romDataList[i].ConfigContentText.Length == 2)
            {
                string temp = analyMSA.CurrItemDescription(contents, Convert.ToString(PageData), Convert.ToByte(e2romDataList[i].ConfigAddressDecText), Convert.ToByte(e2romDataList[i].ConfigContentText, 16), out ItemName, out ItemDescription);
                ItemName = ItemName.Replace(" ", "");
                temp = temp.Replace(" ", "");
                e2romDataList[i].ConfigFiledDescriptionText = temp;
                e2romDataList[i].ConfigFiledNameText = ItemName;
                //e2romDataList[i].configFileNameDescription = ItemName;
                //e2romDataList[i].configFiledDescription = temp;
                ItemName = "";
                ItemDescription = "";
            }
            e2romDataList[i].ConfigREVContext = ValiExpression.GTwohexadecimalcharacters;
            e2romDataList[i].EnableContentText = false;
            
            if (i >= 1)
            {
                e2romDataList[i].LBTH1Visible = false;
                e2romDataList[i].LBTH2Visible = false;
                e2romDataList[i].LBTH3Visible = false;
                e2romDataList[i].LBTH4Visible = false;
                e2romDataList[i].LBTH5Visible = false;
                e2romDataList[i].LBTHTitleVisible(false);          
                
                
            }
            //testTestplanList[i].IsSelected_CheckedChanged.CheckedChanged+=
            this.E2ROMDataInfor.Controls.Add(e2romDataList[i]);
        }
        }

        
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoMSAEEPROMSet where ID=" + moduleTypeID, "TopoMSAEEPROMSet");
                PageLength();
                rowCount =(int) pagelength;
                bindData();
               string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from TopoMSAEEPROMSet where id =" + moduleTypeID).ToString();
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
        //ASCXOptionButtons UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool SaveData(object obj, string prameter)
    {
        string updataStr = "select * from TopoMSAEEPROMSet where ID=" + moduleTypeID;
        try
        {
            {
                if (mydt.Rows.Count != 1)
                {
                    return false;
                }
                else
                {
                    string contentText="";
                    switch (PageData)
                    {
                        case 0:
                            {
                                
                                for (byte j = 0; j < e2romDataList.Length; j++)
                                {
                                    contentText += e2romDataList[j].ConfigContentText;
                                }
                                mydt.Rows[0]["Data0"] = contentText;
                                mydt.Rows[0]["CRCData0"] = alg.CRC8(alg.strToHexByte(contentText));
                                break;
                            }
                    }
                   
                    int result = pDataIO.UpdateWithProc("TopoMSAEEPROMSet", mydt, updataStr, logTracingString);
                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("Update data fail!");
                    }
                    for (byte i = 0; i < e2romDataList.Length; i++)
                    {
                        e2romDataList[i].EnableContentText = false;
                       
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
            for (int i = 0; i < e2romDataList.Length; i++)
            {
                
                e2romDataList[i].EnableContentText = true;
              
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
        int myAccessCode =0;
        if (Session["AccCode"] != null)
        {
            myAccessCode = Convert.ToInt32(Session["AccCode"]);
        }
        CommCtrl mCommCtrl = new CommCtrl();
        bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.Production, CommCtrl.CheckAccess.MofifyProduction, myAccessCode);  
        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = false;
        if (editVisible)
        {
            OptionButtons1.ConfigBtEditVisible = true;
        }
        else
        {
            OptionButtons1.ConfigBtEditVisible = PNAuthority();
        }  
        OptionButtons1.ConfigBtDeleteVisible = false;
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    public void SelectFuncItemName()
    {
        try
        {
            switch (PageData)
            {
                 case (sbyte)MSAPages.SFF8636_A0H_Page0:
                 case (sbyte)MSAPages.SFF8472_A0:
                    {
                        funcItemName = "E2ROMData0Infor";
                        break;
                    }
                 case (sbyte)MSAPages.SFF8636_A0H_Page1:
                 case (sbyte)MSAPages.SFF8472_A2:
                    {
                        funcItemName = "E2ROMData1Infor";
                        break;
                    }
                 case (sbyte)MSAPages.SFF8636_A0H_Page2:
                    {
                        funcItemName = "E2ROMData2Infor";
                        break;
                    }
                 case (sbyte)MSAPages.SFF8636_A0H_Page3:
                    {
                        funcItemName = "E2ROMData3Infor";
                        break;
                    }
                    default:
                    {
                        funcItemName = "E2ROMData0Infor";
                        break;
                    }
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public void PageLength()
    {
      
        try
        {
            switch (PageData)
            {
                case (sbyte)MSAPages.SFF8636_A0H_Page0:
                    {
                        pageStartAddress = 128;
                        pagelength = 128;
                        break;
                    }
                case (sbyte)MSAPages.SFF8636_A0H_Page1:
                    {
                        pageStartAddress = 128;
                        pagelength = 128;
                        break;
                    }
                case (sbyte)MSAPages.SFF8636_A0H_Page2:
                    {
                        pageStartAddress = 128;
                        pagelength = 128;
                        break;
                    }
                case (sbyte)MSAPages.SFF8636_A0H_Page3:
                    {
                        pageStartAddress = 128;
                        pagelength = 128;
                        break;
                    }
                case (sbyte)MSAPages.SFF8472_A0:
                    {
                        pageStartAddress = 0;
                        pagelength = 256;
                        break;
                    }
                case (sbyte)MSAPages.SFF8472_A2:
                    {
                        pageStartAddress = 0;
                        pagelength = 256;
                        break;
                    }
                    default:
                    {
                        pageStartAddress = 0;
                        pagelength = 0;
                        break;
                    }
            }
            
        }
        catch (System.Exception ex)
        {
        	throw ex;
        }
        
    }
    public byte[] PageContent()
    {
        string pageContent = "";
        byte[] tempcontents = new byte[pagelength];
        try
        {
            switch (PageData)
            {
                case (sbyte)MSAPages.SFF8636_A0H_Page0:
                case (sbyte)MSAPages.SFF8472_A0:
                    {
                        pageContent = mydt.Rows[0]["Data0"].ToString().ToUpper().Trim();
                        break;
                    }
                case (sbyte)MSAPages.SFF8636_A0H_Page1:
                case (sbyte)MSAPages.SFF8472_A2:
                    {
                        pageContent = mydt.Rows[0]["Data1"].ToString().ToUpper().Trim();
                        break;
                    }
                case (sbyte)MSAPages.SFF8636_A0H_Page2:
                    {
                        pageContent = mydt.Rows[0]["Data2"].ToString().ToUpper().Trim();
                        break;
                    }
                case (sbyte)MSAPages.SFF8636_A0H_Page3:
                    {
                        pageContent = mydt.Rows[0]["Data3"].ToString().ToUpper().Trim();
                        break;
                    }
                default:
                    {
                        pageContent = mydt.Rows[0]["Data0"].ToString().ToUpper().Trim();
                        break;
                    }
            }
            byte[] temp1 = alg.strToHexByte(pageContent);
            if (temp1.Length < pagelength)
            {
                byte[] temp2 = new byte[pagelength-temp1.Length];
                for (int i = 0; i < temp2.Length;i++)
                {
                    temp2[i] = 255;
                }
                temp1.CopyTo(tempcontents, 0);
                temp2.CopyTo(tempcontents, temp1.Length);
            }
            else if(temp1.Length >pagelength)
            {
                Array.Copy(temp1, tempcontents, pagelength);
            }
            else
            {
                tempcontents = temp1;
            }
            return tempcontents;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool ContetTextChange(object obj, string prameter)
    {

        try
        {
            ASCXE2ROMDataInfor fc = (ASCXE2ROMDataInfor)E2ROMDataInfor.FindControl(Convert.ToString(obj));
            if (fc!=null)
            {
                string ItemName = "";
                string ItemDescription = "";
                for (int i = 0; i < e2romDataList.Length; i++)
                {
                   
                   
                    if (e2romDataList[i].ID==fc.ID)
                    {
                        if (Regex.IsMatch(e2romDataList[i].ConfigContentText, "[\\da-fA-F]{2}") && e2romDataList[i].ConfigContentText.Length==2)
                        {
                            string temp = analyMSA.CurrItemDescription(contents, Convert.ToString(PageData), Convert.ToByte(e2romDataList[i].ConfigAddressDecText), Convert.ToByte(e2romDataList[i].ConfigContentText, 16), out ItemName, out ItemDescription);
                            ItemName = ItemName.Replace(" ", "");
                            temp = temp.Replace(" ", "");
                            e2romDataList[i].ConfigFiledNameText = ItemName;
                            //e2romDataList[i].configFileNameDescription = ItemName;
                            //e2romDataList[i].configFiledDescription = temp;
                            e2romDataList[i].ConfigFiledDescriptionText = temp;
                            ItemName = "";
                            ItemDescription = "";
                        }
                        else
                        {
                            e2romDataList[i].ConfigREVContext = ValiExpression.GTwohexadecimalcharacters;
                        }
                    }
                }
                return true;
            }
             else
            {
                Response.Write("<script>alert('can not find user control！');</script>");
                return false;
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
                DataTable selectPNid = pDataIO.GetDataTable("select PID from TopoMSAEEPROMSet where ID=" + moduleTypeID, "TopoMSAEEPROMSet");
                string StrPNId = selectPNid.Rows[0]["PID"].ToString().Trim();
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