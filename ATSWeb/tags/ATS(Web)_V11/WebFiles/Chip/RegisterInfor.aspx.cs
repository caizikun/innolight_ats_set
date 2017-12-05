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
public partial class WebFiles_Chip_RegisterInfor : BasePage
{
    string funcItemName = "RegisterInfor";
    ASCXOptionButtons UserOptionButton;
    Frame_Chip_RegisterINfor[] RegisterSelfInforList;
    Frame_Chip_RegisterINfor RegisterSelfInfor;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private byte chipChannelCount;
   private bool AddNew;
  private string logTracingString = "";
  public WebFiles_Chip_RegisterInfor()
    {
        chipChannelCount = 0;
        AddNew = false;
        rowCount = 0;
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
        //if (!IsPostBack)
        {
            IsSessionNull();
            SetSessionBlockType(10);
            moduleTypeID = Request["uId"];
            if (Request.QueryString["AddNew"] != null)
            {
                AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            }
            connectDataBase();
            LoadOptionButton();
            ConfigOptionButtonsVisible();
          
        }

    }

    public void bindData()
    {
        RegisterSelfInforList = new Frame_Chip_RegisterINfor[rowCount];
        if (rowCount == 0 && AddNew == false)
        {
            return;
        }
        if (AddNew)
        {
            ClearTextValue();
        }
        else
        {
            for (byte i = 0; i < RegisterSelfInforList.Length; i++)
            {
                RegisterSelfInforList[i] = (Frame_Chip_RegisterINfor)Page.LoadControl("../../Frame/Chip/RegisterINfor.ascx");
                RegisterSelfInforList[i].ID = mydt.Rows[0]["ID"].ToString().Trim();
                RegisterSelfInforList[i].Colum1TextConfig = mydt.Rows[i]["EndBit"].ToString().Trim();
                RegisterSelfInforList[i].Colum2TextConfig = mydt.Rows[i]["Address"].ToString().Trim();
                RegisterSelfInforList[i].Colum3TextConfig = mydt.Rows[i]["StartBit"].ToString().Trim();  
                RegisterSelfInforList[i].Colum4TextSelected = UnitLengthIndex(mydt.Rows[i]["UnitLength"].ToString().Trim());
                RegisterSelfInforList[i].ClearDropDownList();
                ConfigChipChannel(RegisterSelfInforList[i]);
                double temp = Convert.ToDouble(mydt.Rows[i]["ChipLine"]);
                if (temp < 0 || temp>chipChannelCount)
                {
                    RegisterSelfInforList[i].Colum5Slected =0;
                } 
                else
                {
                    RegisterSelfInforList[i].Colum5Slected = Convert.ToByte(temp);
                }
               
                RegisterSelfInforList[i].EnableColum1Text = false;
                RegisterSelfInforList[i].EnableColum2Text = false;
                RegisterSelfInforList[i].EnableColum3Text = false;
                RegisterSelfInforList[i].EnableColum4Text = false;
                RegisterSelfInforList[i].EnableColum5Text = false;
             

                this.Register_Infor.Controls.Add(RegisterSelfInforList[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChipRegister where ID=" + moduleTypeID, "ChipRegister");
                rowCount = mydt.Rows.Count;

                if (rowCount == 1 && !AddNew)
                {
                    DataTable temp = pDataIO.GetDataTable("select * from RegisterFormula where ID=" + mydt.Rows[0]["FormulaID"].ToString().Trim(), "RegisterFormula");
                    DataTable temp1 = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + temp.Rows[0]["ChipID"].ToString().Trim(), "ChipBaseInfo");
                    chipChannelCount = Convert.ToByte(temp1.Rows[0]["Channels"]);
                }
                if (AddNew)
                {
                    DataTable temp = pDataIO.GetDataTable("select * from RegisterFormula where ID=" + moduleTypeID, "RegisterFormula");
                    DataTable temp1 = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + temp.Rows[0]["ChipID"].ToString().Trim(), "ChipBaseInfo");
                    chipChannelCount = Convert.ToByte(temp1.Rows[0]["Channels"]);
                }
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "AddNewItem";
                }
                else
                {

                    parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from RegisterFormula where id =" + mydt.Rows[0]["FormulaID"].ToString().Trim()).ToString();
                }
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
    protected void LoadOptionButton()
    {
        //UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            if (AddNew)
            {
                Response.Redirect("~/WebFiles/Chip/RegisterList.aspx?uId=" + moduleTypeID.Trim());
            }
            else
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
    public bool SaveData(object obj, string prameter)
    {
        string updataStr = "select * from ChipRegister where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                if (!IsRangeValidator())
                {
                    return false;
                }
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "ChipRegister");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = RegisterSelfInfor.Colum1TextConfig;
                dr[3] = RegisterSelfInfor.Colum2TextConfig;
                dr[4] = RegisterSelfInfor.Colum3TextConfig;
                dr[5] = RegisterSelfInfor.Colum4TextConfig;
                dr[6] = RegisterSelfInfor.Colum5TextConfig;
                  inserTable.Rows.Add(dr);
                  int result = pDataIO.UpdateWithProc("ChipRegister", inserTable, updataStr, logTracingString);
                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/Chip/RegisterList.aspx?uId=" + moduleTypeID.Trim());
                  }
                  else
                  {
                      pDataIO.AlertMsgShow("Update data fail!",Request.Url.ToString());
                  }
              
                  return true;
               
            } 
            else
            {
                if (!IsRangeValidator())
                {                   
                    RegisterSelfInforList[0].EnableColum1Text = false;
                    RegisterSelfInforList[0].EnableColum2Text = false;
                    RegisterSelfInforList[0].EnableColum3Text = false;
                    RegisterSelfInforList[0].EnableColum4Text = false;
                    RegisterSelfInforList[0].EnableColum5Text = false;
                   
                    return false;
                }
                if (rowCount != 1)
                {
                    
                    return false;
                }
                else
                {

                    for (byte j = 0; j < RegisterSelfInforList.Length; j++)
                    {
                        mydt.Rows[0]["EndBit"] = RegisterSelfInforList[j].Colum1TextConfig;
                        mydt.Rows[0]["Address"] = RegisterSelfInforList[j].Colum2TextConfig;
                        mydt.Rows[0]["StartBit"] = RegisterSelfInforList[j].Colum3TextConfig;
                        mydt.Rows[0]["UnitLength"] = RegisterSelfInforList[j].Colum4TextConfig;
                        mydt.Rows[0]["ChipLine"] = RegisterSelfInforList[j].Colum5TextConfig;
                    }
                    int result = pDataIO.UpdateWithProc("ChipRegister", mydt, updataStr, logTracingString);
                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("Update data fail!");
                    }
                    for (byte i = 0; i < RegisterSelfInforList.Length; i++)
                        {
                            RegisterSelfInforList[i].EnableColum1Text = false;
                            RegisterSelfInforList[i].EnableColum2Text = false;
                            RegisterSelfInforList[i].EnableColum3Text = false;
                            RegisterSelfInforList[i].EnableColum4Text = false;
                            RegisterSelfInforList[i].EnableColum5Text = false;
                           
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
           for (int i = 0; i < RegisterSelfInforList.Length;i++)
           {
               RegisterSelfInforList[i].EnableColum1Text = true;
               RegisterSelfInforList[i].EnableColum2Text = true;
               RegisterSelfInforList[i].EnableColum3Text = true;
               RegisterSelfInforList[i].EnableColum4Text = true;
               RegisterSelfInforList[i].EnableColum5Text = true;
              
               
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
    public void ConfigOptionButtonsVisible()
    {
      
        OptionButtons1.ConfigBtAddVisible = false;
        if (AddNew)
        {
            OptionButtons1.ConfigBtEditVisible = false;
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtCancelVisible = true;
        } 
        else
        {
          
            int myAccessCode =0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();

            OptionButtons1.ConfigBtSaveVisible = false;
            bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ChipInfor, CommCtrl.CheckAccess.EditChipInfor, myAccessCode);
            OptionButtons1.ConfigBtCancelVisible = false;
            OptionButtons1.ConfigBtEditVisible = editVisible;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        RegisterSelfInfor = new Frame_Chip_RegisterINfor();
        try
        {


            {
                RegisterSelfInfor = (Frame_Chip_RegisterINfor)Page.LoadControl("../../Frame/Chip/RegisterINfor.ascx");
                RegisterSelfInfor.Colum2TextConfig = "";
                RegisterSelfInfor.Colum3TextConfig = "";
                RegisterSelfInfor.Colum4TextConfig = "";
                RegisterSelfInfor.Colum5TextConfig = "";
                RegisterSelfInfor.Colum1TextConfig = "";
                RegisterSelfInfor.ClearDropDownList();
                ConfigChipChannel(RegisterSelfInfor);
                RegisterSelfInfor.EnableColum5Text = true;
                RegisterSelfInfor.EnableColum4Text = true;
                RegisterSelfInfor.EnableColum3Text = true;
                RegisterSelfInfor.EnableColum2Text = true;
                RegisterSelfInfor.EnableColum1Text = true;
             
                this.Register_Infor.Controls.Add(RegisterSelfInfor);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    
    public int NameIndex(string input)
    {
        int index = 0;
        try
        {
            switch (input)
            {
               
                case "0":
                    {
                        index = 0;
                        break;
                    }
                case "1":
                    {
                        index = 1;
                        break;
                    }
                case "2":
                    {
                        index = 2;
                        break;
                    }
               
                default:
                    {
                        index = 0;
                        break;
                    }
            }
            return index;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool ConfigChipChannel(Frame_Chip_RegisterINfor input)
    {

        try
        {
            for (int i = 0; i < chipChannelCount+1; i++)
            {
                input.InsertColum5Text(i, new ListItem(Convert.ToString(i)));
               
            }
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
    public bool IsRangeValidator()
    {
        if (AddNew)
        {
            if (Convert.ToDouble(RegisterSelfInfor.Colum3TextConfig) > Convert.ToDouble(RegisterSelfInfor.Colum1TextConfig) ||
                Convert.ToDouble(RegisterSelfInfor.Colum1TextConfig) < Convert.ToDouble(RegisterSelfInfor.Colum3TextConfig))
            {
                this.Page.RegisterStartupScript("", "<script>alert('please make sure StartBit less than Endbit and Endbit greater than StartBit ！');</script>");
                return false;
            }
        }
        else
        {
            if (rowCount != 1)
            {
                return false;
            }
            if (Convert.ToDouble(RegisterSelfInforList[0].Colum3TextConfig) > Convert.ToDouble(RegisterSelfInforList[0].Colum1TextConfig) ||
                Convert.ToDouble(RegisterSelfInforList[0].Colum1TextConfig) < Convert.ToDouble(RegisterSelfInforList[0].Colum3TextConfig))
            {
                this.Page.RegisterStartupScript("", "<script>alert('please make sure StartBit less than Endbit and Endbit greater than StartBit ！');</script>");
                return false;
            }
        }

        return true;
    }
    public int UnitLengthIndex(string input)
    {
        int index = 0;
        try
        {
            switch (input)
            {

                case "1":
                    {
                        index = 0;
                        break;
                    }
                case "2":
                    {
                        index = 1;
                        break;
                    }
                case "4":
                    {
                        index = 2;
                        break;
                    }

                default:
                    {
                        index = 0;
                        break;
                    }
            }
            return index;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
}