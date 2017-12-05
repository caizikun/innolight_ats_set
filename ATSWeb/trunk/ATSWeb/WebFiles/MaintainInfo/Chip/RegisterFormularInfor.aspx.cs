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
public partial class WebFiles_Chip_RegisterFormularInfor : BasePage
{
    string funcItemName = "寄存器信息";
    ASCXOptionButtons UserOptionButton;
    ASCX_Chip_FormulaINFor[] FormulaSelfInforlist;
    ASCX_Chip_FormulaINFor chipsetControl;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   private string moduleTypeID = "";
   private int rowCount;
   private int columCount;
   private bool AddNew;
   private byte chipChannelCount;
  private string logTracingString = "";

    protected override void OnInit(EventArgs e)
    {
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        {
            IsSessionNull();

            columCount = 0;
            chipChannelCount = 0;
            AddNew = false;
            rowCount = 0;
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
            SetSessionBlockType(3);
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
        FormulaSelfInforlist = new ASCX_Chip_FormulaINFor[rowCount];
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
            for (byte i = 0; i < FormulaSelfInforlist.Length; i++)
            {
                FormulaSelfInforlist[i] = (ASCX_Chip_FormulaINFor)Page.LoadControl("~/Frame/Chip/FormulaINFor.ascx");
                //FormulaSelfInforlist[i].ID = mydt.Rows[0]["ID"].ToString().Trim();
                FormulaSelfInforlist[i].Colum1TextConfig = mydt.Rows[i]["ItemName"].ToString().Trim();             
                FormulaSelfInforlist[i].Colum2TextConfig = mydt.Rows[i]["WriteFormula"].ToString().Trim();
                FormulaSelfInforlist[i].Colum4TextConfig = mydt.Rows[i]["ReadFormula"].ToString().Trim();
                FormulaSelfInforlist[i].Colum3TextSelected = UnitIndex(mydt.Rows[i]["AnalogueUnit"].ToString().Trim());

                FormulaSelfInforlist[i].Colum5TextConfig = mydt.Rows[i]["Address"].ToString().Trim();
                FormulaSelfInforlist[i].Colum6TextConfig = mydt.Rows[i]["StartBit"].ToString().Trim();
                FormulaSelfInforlist[i].Colum7TextConfig = mydt.Rows[i]["EndBit"].ToString().Trim();
                FormulaSelfInforlist[i].Colum8TextSelected = UnitLengthIndex(mydt.Rows[i]["UnitLength"].ToString().Trim());
                FormulaSelfInforlist[i].ClearDropDownList();
                ConfigChipChannel(FormulaSelfInforlist[i]);
                double temp = Convert.ToDouble(mydt.Rows[i]["ChipLine"]);
                if (temp < 0 || temp > chipChannelCount)
                {
                    FormulaSelfInforlist[i].Colum9TextSelected = 0;
                }
                else
                {
                    FormulaSelfInforlist[i].Colum9TextSelected = Convert.ToByte(temp);
                }
               

               
                FormulaSelfInforlist[i].EnableColum1Text = false;
                FormulaSelfInforlist[i].EnableColum2Text = false;
                FormulaSelfInforlist[i].EnableColum3Text = false;
                FormulaSelfInforlist[i].EnableColum4Text = false;
                FormulaSelfInforlist[i].EnableColum5Text = false;
                FormulaSelfInforlist[i].EnableColum6Text = false;
                FormulaSelfInforlist[i].EnableColum7Text = false;
                FormulaSelfInforlist[i].EnableColum8Text = false;
                FormulaSelfInforlist[i].EnableColum9Text = false;

                this.FormulaSelfInfor.Controls.Add(FormulaSelfInforlist[i]);
            }
        }
        

    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from ChipRegisterList where ID=" + moduleTypeID, "ChipRegisterList");
                rowCount = mydt.Rows.Count;
                columCount = mydt.Columns.Count;
                if (rowCount == 1 && !AddNew)
                {
                    DataTable temp1 = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + mydt.Rows[0]["ChipID"].ToString().Trim(), "ChipBaseInfo");
                    chipChannelCount = Convert.ToByte(temp1.Rows[0]["Channels"]);
                }
                if (AddNew)
                {                    
                    DataTable temp1 = pDataIO.GetDataTable("select * from ChipBaseInfo where ID=" + moduleTypeID, "ChipBaseInfo");
                    chipChannelCount = Convert.ToByte(temp1.Rows[0]["Channels"]);
                }
                bindData();
                string parentItem = "";
                if (AddNew)
                {
                    parentItem = "添加新项";
                }
                else
                {

                    parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from ChipRegisterList where id =" + moduleTypeID).ToString();
                }
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
                Response.Redirect("~/WebFiles/MaintainInfo/Chip/RegisterFormulaList.aspx?uId=" + moduleTypeID.Trim());
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
        string updataStr = "select * from ChipRegisterList where ID=" + moduleTypeID;
        try
        {
            if (AddNew)
            {
                if (!IsRangeValidator())
                {
                    return false;
                }
                DataTable inserTable = pDataIO.GetDataTable(updataStr, "ChipRegisterList");
                DataRow dr = inserTable.NewRow();
                dr[0] = -1;
                dr[1] = moduleTypeID;
                dr[2] = chipsetControl.Colum1TextConfig;
                dr[3] = chipsetControl.Colum2TextConfig;
                dr[4] = chipsetControl.Colum3TextConfig;
                dr[5] = chipsetControl.Colum4TextConfig;

                dr[7] = chipsetControl.Colum5TextConfig;
                dr[8] = chipsetControl.Colum6TextConfig;
                dr[6] = chipsetControl.Colum7TextConfig;
                dr[9] = chipsetControl.Colum8TextConfig;
                dr[10] = chipsetControl.Colum9TextConfig;
                  inserTable.Rows.Add(dr);

                  int result = -1;
                  if (Session["DB"].ToString().ToUpper() == "ATSDB")
                  {
                      result = pDataIO.UpdateWithProc("ChipRegisterList", inserTable, updataStr, logTracingString, "ATS_V2");
                  }
                  else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                  {
                      result = pDataIO.UpdateWithProc("ChipRegisterList", inserTable, updataStr, logTracingString, "ATS_VXDEBUG");
                  }      

                  if (result > 0)
                  {
                      inserTable.AcceptChanges();
                      Response.Redirect("~/WebFiles/MaintainInfo/Chip/RegisterFormulaList.aspx?uId=" + moduleTypeID.Trim());
                  }
                  else
                  {
                      pDataIO.AlertMsgShow("数据更新失败!",Request.Url.ToString());
                  }
              
                  return true;
               
            } 
            else
            {
                if (!IsRangeValidator())
                {
                    FormulaSelfInforlist[0].EnableColum1Text = false;
                    FormulaSelfInforlist[0].EnableColum2Text = false;
                    FormulaSelfInforlist[0].EnableColum3Text = false;
                    FormulaSelfInforlist[0].EnableColum4Text = false;
                    FormulaSelfInforlist[0].EnableColum5Text = false;
                    FormulaSelfInforlist[0].EnableColum6Text = false;
                    FormulaSelfInforlist[0].EnableColum7Text = false;
                    FormulaSelfInforlist[0].EnableColum8Text = false;
                    FormulaSelfInforlist[0].EnableColum9Text = false;
                    return false;
                }
                if (rowCount != 1)
                {
                    return false;
                }
                else
                {

                    for (byte j = 0; j < FormulaSelfInforlist.Length; j++)
                    {
                        mydt.Rows[0]["ItemName"] = FormulaSelfInforlist[j].Colum1TextConfig;
                        mydt.Rows[0]["WriteFormula"] = FormulaSelfInforlist[j].Colum2TextConfig;
                        mydt.Rows[0]["AnalogueUnit"] = FormulaSelfInforlist[j].Colum3TextConfig;
                        mydt.Rows[0]["ReadFormula"] = FormulaSelfInforlist[j].Colum4TextConfig;

                        mydt.Rows[0]["EndBit"] = FormulaSelfInforlist[j].Colum7TextConfig;
                        mydt.Rows[0]["Address"] = FormulaSelfInforlist[j].Colum5TextConfig;
                        mydt.Rows[0]["StartBit"] = FormulaSelfInforlist[j].Colum6TextConfig;
                        mydt.Rows[0]["UnitLength"] = FormulaSelfInforlist[j].Colum8TextConfig;
                        mydt.Rows[0]["ChipLine"] = FormulaSelfInforlist[j].Colum9TextConfig;
                    }

                    int result = -1;
                    if (Session["DB"].ToString().ToUpper() == "ATSDB")
                    {
                        result = pDataIO.UpdateWithProc("ChipRegisterList", mydt, updataStr, logTracingString, "ATS_V2");
                    }
                    else if (Session["DB"].ToString().ToUpper() == "ATSDEBUGDB")
                    {
                        result = pDataIO.UpdateWithProc("ChipRegisterList", mydt, updataStr, logTracingString, "ATS_VXDEBUG");
                    }      

                    if (result > 0)
                    {
                        mydt.AcceptChanges();
                    }
                    else
                    {
                        pDataIO.AlertMsgShow("数据更新失败!");
                    }
                    for (byte i = 0; i < FormulaSelfInforlist.Length; i++)
                        {
                            FormulaSelfInforlist[i].EnableColum1Text = false;
                            FormulaSelfInforlist[i].EnableColum2Text = false;
                            FormulaSelfInforlist[i].EnableColum3Text = false;
                            FormulaSelfInforlist[i].EnableColum4Text = false;

                            FormulaSelfInforlist[i].EnableColum5Text = false;
                            FormulaSelfInforlist[i].EnableColum6Text = false;
                            FormulaSelfInforlist[i].EnableColum7Text = false;
                            FormulaSelfInforlist[i].EnableColum8Text = false;
                            FormulaSelfInforlist[i].EnableColum9Text = false;
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
           for (int i = 0; i < FormulaSelfInforlist.Length;i++)
           {
               FormulaSelfInforlist[i].EnableColum1Text = true;
               FormulaSelfInforlist[i].EnableColum2Text = true;
               FormulaSelfInforlist[i].EnableColum3Text = true;
               FormulaSelfInforlist[i].EnableColum4Text = true;
               FormulaSelfInforlist[i].EnableColum5Text = true;
               FormulaSelfInforlist[i].EnableColum6Text = true;
               FormulaSelfInforlist[i].EnableColum7Text = true;
               FormulaSelfInforlist[i].EnableColum8Text = true;
               FormulaSelfInforlist[i].EnableColum9Text = true;
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
            bool editVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ChipInfo, CommCtrl.CheckAccess.MofifyChipInfor, myAccessCode);
            OptionButtons1.ConfigBtCancelVisible = false;
            OptionButtons1.ConfigBtEditVisible = editVisible;
        }
    
        OptionButtons1.ConfigBtDeleteVisible = false;
        
    }
    public void ClearTextValue()
    {
        chipsetControl = new ASCX_Chip_FormulaINFor();
        try
        {
            {
                chipsetControl = (ASCX_Chip_FormulaINFor)Page.LoadControl("~/Frame/Chip/FormulaINFor.ascx");
                chipsetControl.Colum2TextConfig = "";
                chipsetControl.Colum3TextConfig = "";
                chipsetControl.Colum4TextConfig = "";
               
                chipsetControl.Colum1TextConfig = "";
                chipsetControl.Colum5TextConfig = "";
                chipsetControl.Colum6TextConfig = "";
                chipsetControl.Colum7TextConfig = "";

                chipsetControl.Colum8TextConfig = "";
                chipsetControl.Colum9TextConfig = "";

                chipsetControl.ClearDropDownList();
                ConfigChipChannel(chipsetControl);

                chipsetControl.EnableColum4Text = true;
                chipsetControl.EnableColum3Text = true;
                chipsetControl.EnableColum2Text = true;
                chipsetControl.EnableColum1Text = true;
                chipsetControl.EnableColum9Text = true;
                chipsetControl.EnableColum5Text = true;
                chipsetControl.EnableColum6Text = true;
                chipsetControl.EnableColum7Text = true;
                chipsetControl.EnableColum8Text = true;
                this.FormulaSelfInfor.Controls.Add(chipsetControl);
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    
    
    public int UnitIndex(string input)
    {
        int index = 0;
        try
        {
            switch (input.ToUpper())
            {

                case "MA":
                    {
                        index = 0;
                        break;
                    }
                case "MV":
                    {
                        index = 1;
                        break;
                    }
                case "":
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
    public int NameIndex(string input)
    {
        int index = 0;
        try
        {
            switch (input.ToUpper())
            {

                case "IMOD":
                    {
                        index = 0;
                        break;
                    }
                case "IBIAS":
                    {
                        index = 1;
                        break;
                    }
                case "CROSS":
                    {
                        index = 2;
                        break;
                    }
                case "LOSS":
                    {
                        index = 3;
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
    public bool ConfigChipChannel(ASCX_Chip_FormulaINFor input)
    {

        try
        {
            for (int i = 0; i < chipChannelCount + 1; i++)
            {
                input.InsertColum9Text(i, new ListItem(Convert.ToString(i)));

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
            if (Convert.ToDouble(chipsetControl.Colum6TextConfig) > Convert.ToDouble(chipsetControl.Colum7TextConfig) ||
                Convert.ToDouble(chipsetControl.Colum7TextConfig) < Convert.ToDouble(chipsetControl.Colum6TextConfig))
            {
                this.Page.RegisterStartupScript("", "<script>alert('请确认开始位小于结束位！');</script>");
                return false;
            }
        }
        else
        {
            if (rowCount != 1)
            {
                return false;
            }
            if (Convert.ToDouble(FormulaSelfInforlist[0].Colum6TextConfig) > Convert.ToDouble(FormulaSelfInforlist[0].Colum7TextConfig) ||
                Convert.ToDouble(FormulaSelfInforlist[0].Colum7TextConfig) < Convert.ToDouble(FormulaSelfInforlist[0].Colum6TextConfig))
            {
                this.Page.RegisterStartupScript("", "<script>alert('请确认开始位小于结束位！');</script>");
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