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
using System.Data.Common;

public partial class WebFiles_APPModel_CurrModelParamInfo : BasePage
{
    const string funcItemName = "ModelParamInfo";
    Frame_APPModel_GlobalModelParam[] ControlList;
    private ValidateExpression validExpre;
    CommCtrl pCommCtrl = new CommCtrl();
    DataIO pDataIO;
    string paramID = "-1";
    string ModelID = "-1";
    bool AddNew = false;
    DataTable mydt = new DataTable();
    string queryStr = "";
    private string logTracingString = "";
    public WebFiles_APPModel_CurrModelParamInfo()
    {
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        validExpre = new ValidateExpression();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        SetSessionBlockType(5);
        if (Request.QueryString["uId"] != null)
        {
            paramID = Request.QueryString["uId"];
        }

        if (Request.QueryString["ModelID"] != null)
        {
            ModelID = Request.QueryString["ModelID"];
        }

        if (Request.QueryString["AddNew"] != null)
        {
            AddNew = (Request.QueryString["AddNew"].ToLower() == "true" ? true : false);
            paramID = "-1";            
        }
        queryStr = "select * from GlobalTestModelParamterList where id =" + paramID;
        initPageInfo();
    }

    void createNavilnks()
    {
        this.plhNavi.Controls.Clear();

        string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from GlobalTestModelParamterList where id = " + paramID).ToString();
        if (AddNew)
        {
            parentItem = "AddNewItem";
        }
        Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
        this.plhNavi.Controls.Add(myCtrl);
    }

    void initPageInfo()
    {
        try
        {
            ConfigOptionButtonsVisible();
            if (paramID != null && paramID.Length > 0)
            {
                createNavilnks();
                getInfo(queryStr, AddNew);
            }
            else
            {
                OptionButtons1.Visible = false;
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any user control~')</Script>");
            }
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    bool getInfo(string filterStr, bool isEditState = false)
    {
        CommCtrl pCommCtrl = new CommCtrl();
        try
        {
            mydt = pDataIO.GetDataTable(filterStr, "GlobalTestModelParamterList");

            DataRow[] drs = mydt.Select("");
            ControlList = new Frame_APPModel_GlobalModelParam[1];
            ControlList[0] = (Frame_APPModel_GlobalModelParam)Page.LoadControl("~/Frame/APPModel/GlobalModelParam.ascx");
            ControlList[0].SetModelParamsEnableState(isEditState);
            if (drs.Length == 1)
            {
                ModelID = drs[0]["PID"].ToString();
               ControlList[0].ID = "ParamID_" + drs[0]["ID"].ToString();               
               ControlList[0].TxtItemName = drs[0]["ItemName"].ToString();
               ControlList[0].TxtShowName = drs[0]["ShowName"].ToString();
               int temp = ControlList[0].GSItemTypeDropDownList;
                string strItemType=drs[0]["ItemType"].ToString().Replace(" ", "");
                ControlList[0].GSItemTypeDropDownList = ChangeItemTypeNametoIndex(strItemType.ToUpper().Trim());
                ConfigValidator(strItemType, ControlList[0]);              
               ControlList[0].TxtItemValue = drs[0]["ItemValue"].ToString();

               ControlList[0].TxtddlNeedCheckParams = drs[0]["NeedSelect"].ToString();
               ControlList[0].TxtOptionalparams = drs[0]["Optionalparams"].ToString();
               ControlList[0].TxtItemDescription = drs[0]["ItemDescription"].ToString();
               ControlList[0].SetModelParamsEnableState(false);
            }
            else if (isEditState && drs.Length == 0)
            {
                ControlList[0].ID = "Param_New";
                ControlList[0].TxtItemName = "NewParamName";
                ControlList[0].TxtShowName = "NewShowName";
                ControlList[0].TxtItemValue = "NewValue";
                ControlList[0].GSItemTypeDropDownList =0;
                ConfigValidator(ChangeItemTypeIndextoName(ControlList[0].GSItemTypeDropDownList), ControlList[0]);
                ControlList[0].TxtItemDescription = "Description";

                EditData("", "");
            }
            this.plhMain.Controls.Add(ControlList[0]);
            return true;
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            throw ex;
        }
    }

    public bool AddData(object obj, string prameter)
    {
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {

        return true;
    }

    public bool EditData(object obj, string prameter)
    {
        try
        {
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtCancelVisible = true;
            OptionButtons1.ConfigBtEditVisible = false;

            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null)
                {
                   ControlList[i].SetModelParamsEnableState(true);
                }
            }
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool SaveData(object obj, string prameter)
    {
        try
        {
            if (mydt.Rows.Count == 1)
            {
                #region //已经获取到当前设备的信息
                mydt.Rows[0]["ItemName"] = ControlList[0].TxtItemName;
                mydt.Rows[0]["ShowName"] = ControlList[0].TxtShowName;
                mydt.Rows[0]["ItemType"] = ChangeItemTypeIndextoName(ControlList[0].GSItemTypeDropDownList);
                mydt.Rows[0]["ItemValue"] = ControlList[0].TxtItemValue;
                mydt.Rows[0]["NeedSelect"] = ControlList[0].TxtddlNeedCheckParams;
                mydt.Rows[0]["Optionalparams"] = ControlList[0].TxtOptionalparams;
                mydt.Rows[0]["ItemDescription"] = ControlList[0].TxtItemDescription;

                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("GlobalTestModelParamterList", mydt, queryStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/APPModel/GlobalModelParamsList.aspx?uId=" + ModelID, false);
                }
                else
                {                  
                    pDataIO.AlertMsgShow("Update data fail!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End
                #endregion
            }
            else if (AddNew && mydt.Rows.Count == 0)
            {
                #region //New

                long currParamID = pDataIO.GetLastInsertData("GlobalTestModelParamterList") + 1;
                DataRow eqNewDr = mydt.NewRow();
                eqNewDr["ID"] = currParamID;
                eqNewDr["PID"] = ModelID;
                eqNewDr["ItemName"] = ControlList[0].TxtItemName;
                eqNewDr["ShowName"] = ControlList[0].TxtShowName;
                eqNewDr["ItemType"] = ChangeItemTypeIndextoName(ControlList[0].GSItemTypeDropDownList);
                eqNewDr["ItemValue"] = ControlList[0].TxtItemValue;

                eqNewDr["NeedSelect"] = ControlList[0].TxtddlNeedCheckParams;
                eqNewDr["Optionalparams"] = ControlList[0].TxtOptionalparams;
                eqNewDr["ItemDescription"] = ControlList[0].TxtItemDescription;

                mydt.Rows.Add(eqNewDr);

                //150527 ---------------------更新方式变更>>Start
                int result = pDataIO.UpdateWithProc("GlobalTestModelParamterList", mydt, queryStr, logTracingString);
                if (result > 0)
                {
                    mydt.AcceptChanges();
                    Response.Redirect("~/WebFiles/APPModel/GlobalModelParamsList.aspx?uId=" + ModelID, false);
                }
                else
                {
                    pDataIO.AlertMsgShow("Update data fail!", Request.Url.ToString());
                }
                //150527 ---------------------更新方式变更<<End
                #endregion
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<Script>alert('Error!Can not find any Result,Current item has been deleted!~')</Script>");
            }

            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/APPModel/GlobalModelParamsList.aspx?uId=" + ModelID, true);
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
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

        OptionButtons1.ConfigBtSaveVisible = false;
        OptionButtons1.ConfigBtAddVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.AddAppModel, myAccessCode);
        OptionButtons1.ConfigBtEditVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.MofifyAppModel, myAccessCode);
        OptionButtons1.ConfigBtDeleteVisible = false;  //mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.AppModel, CommCtrl.CheckAccess.DeleteAppModel, myAccessCode);
        OptionButtons1.ConfigBtCancelVisible = false;
    }
    public void ConfigValidator(string inputType, Frame_APPModel_GlobalModelParam inputControl)
    {
        try
        {
            switch (inputType)
            {
                case "ArrayList":
                case "ARRAYLIST":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItem = true;
                        inputControl.ConfigRegularExpressionValidatorItemVExpression = validExpre.GFloatIntandCommaExpression;
                        inputControl.ConfigRegularExpressionValidatorItemErrMessage = "Please input int or float with  decollator comma symbol ";
                        break;
                    }
                case "byte":
                case "BYTE":
                    {
                        inputControl.EnableRangeValidatorItemValue = true;
                        inputControl.EnableRegularExpressionValidatorItem = false;
                        inputControl.ConfigRangeValidatorItemValueType = ValidationDataType.Integer;
                        inputControl.ConfigRangeValidatorItemValueMax = Convert.ToString(255);
                        inputControl.ConfigRangeValidatorItemValueMin = Convert.ToString(0);
                
                        break;
                    }
                case "UInt16":
                case "UINT16":
                    {
                        inputControl.EnableRangeValidatorItemValue = true;
                        inputControl.EnableRegularExpressionValidatorItem = false;
                        inputControl.ConfigRangeValidatorItemValueType = ValidationDataType.Integer;
                        inputControl.ConfigRangeValidatorItemValueMax = Convert.ToString(65535);
                        inputControl.ConfigRangeValidatorItemValueMin = Convert.ToString(0);
                        
                        break;
                    }
                case "double":
                case "DOUBLE":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItem = true;
                        inputControl.ConfigRegularExpressionValidatorItemVExpression = validExpre.GDoubleorScientificNotation;
                        inputControl.ConfigRegularExpressionValidatorItemErrMessage = "Please input double or Scientific Notation number";
                        
                        break;
                    }
                case "Bool":
                case "BOOL":
                    {
                        inputControl.EnableRangeValidatorItemValue = false;
                        inputControl.EnableRegularExpressionValidatorItem = true;
                        inputControl.ConfigRegularExpressionValidatorItemVExpression = validExpre.GBoolExpression;
                        inputControl.ConfigRegularExpressionValidatorItemErrMessage = "Please input 0 or 1 or true or false";
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
    public int ChangeItemTypeNametoIndex(string inputStr)
    {
        int index = 0;
        try
        {
            switch (inputStr)
            {
                case "ArrayList":
                case "ARRAYLIST":
                    {
                        index = 0;
                        break;
                    }
                case "byte":
                case "BYTE":
                    {
                        index = 1;
                        break;
                    }
                case "UInt16":
                case "UINT16":
                    {
                        index = 2;
                        break;
                    }
                case "double":
                case "DOUBLE":
                    {
                        index = 3;
                        break;
                    }
                case "Bool":
                case "BOOL":
                    {
                        index = 4;
                        break;
                    }
                default: break;

            }

            return index;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public string ChangeItemTypeIndextoName(int inputIndex)
    {
        string typeName = "";
        try
        {
            switch (inputIndex)
            {
                case 0:
                    {
                        typeName = "ArrayList";
                        break;
                    }
                case 1:
                    {
                        typeName = "byte";

                        break;
                    }
                case 2:
                    {
                        typeName = "UInt16";
                        break;
                    }
                case 3:
                    {
                        typeName = "double";
                        break;
                    }
                case 4:
                    {
                        typeName = "Bool";
                        break;
                    }
                default: break;

            }

            return typeName;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool GetIndex(object obj, string prameter)
    {

        ConfigValidator(ChangeItemTypeIndextoName(ControlList[0].GSItemTypeDropDownList), ControlList[0]);

        return true;
    }
}