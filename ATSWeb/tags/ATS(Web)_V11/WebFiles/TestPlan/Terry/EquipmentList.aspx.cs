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

public partial class WebFiles_TestPlan_Terry_EquipmentList : BasePage
{
    const string funcItemName = "EquipmentList";
    Frame_TestPlan_EquipList[] ControlList;
    public DataTable mydt = new DataTable();
    public DataTable mydtProcess = new DataTable();
    private int rowCount = 0;
    private CommCtrl pCommCtrl = new CommCtrl();

    private DataIO pDataIO;
    private string TestPlanID = "-1";
    private string queryStr = "";
    private string logTracingString = "";
    public WebFiles_TestPlan_Terry_EquipmentList()
    {
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        pDataIO = null;
        pDataIO = new SqlManager(serverName, dbName, userId, pwd);        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        SetSessionBlockType(1);
        if (Request.QueryString["uId"] != null)
        {
            TestPlanID = Request.QueryString["uId"];
        }
        queryStr = "select * from topoEquipment where pid=" + TestPlanID + " order by seq";

        initPageInfo();
        ConfigOptionButtonsVisible();
    }

    void initPageInfo()
    {
        try
        {
            
            if (TestPlanID != null && TestPlanID.Length > 0)
            {
                getInfo(queryStr);
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

    bool getInfo(string filterStr)
    {
        try
        {
            string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from TopoTestPlan where id = " + TestPlanID).ToString();

            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO,out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);

            mydt = pDataIO.GetDataTable(filterStr, "topoEquipment");            
            mydtProcess = mydt;
            SeqRecord();
            if (IsPostBack)
            {
                bindData(mydtProcess, true);
            }
            else
            {
                bindData(mydt);
            }
            return true;
        }
        catch (Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            pDataIO.OpenDatabase(false);
            throw ex;
        }
    }

    public void bindData(DataTable dt, bool isEditMode = false)
    {
        if (IsPostBack)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label finLB = (Label)tableFC.FindControl("SEQ" + dt.Rows[i]["ID"].ToString().Trim());
                if (dt.Rows[i]["SEQ"].ToString().ToLower() != finLB.Text.ToLower()) //150529 防止出现多条记录被修改 而实际内容没变
                {
                    dt.Rows[i]["SEQ"] = finLB.Text;
                }
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "SEQ";
            dt = dv.ToTable();
        }

        DataRow[] drs = dt.Select("", "SEQ ASC");
        rowCount = drs.Length;
        ClearCurrenPage();
        if (dt.Rows.Count==0)
        {
        ControlList = new Frame_TestPlan_EquipList[1];
        for (int i = 0; i < ControlList.Length; i++)
        {
            ControlList[i] = (Frame_TestPlan_EquipList)Page.LoadControl("~/Frame/TestPlan/TopoEquipList.ascx");

            ControlList[i].ContentTRVisible = false;
            this.plhMain.Controls.Add(ControlList[i]);
        }
        } 
        else
        {
               ControlList = new Frame_TestPlan_EquipList[dt.Rows.Count];
        for (int i = 0; i < drs.Length; i++)
        {
            ControlList[i] = (Frame_TestPlan_EquipList)Page.LoadControl("~/Frame/TestPlan/TopoEquipList.ascx");
            ControlList[i].ID = drs[i]["ID"].ToString();
            ControlList[i].LnkItemNamePostBackUrl = "~/WebFiles/TestPlan/Terry/EquipmentInfo.aspx?uId=" + drs[i]["ID"] + "&TestPlanID=" + TestPlanID;
            ControlList[i].LnkItemName = pDataIO.getDbCmdExecuteScalar("select showName from GlobalAllEquipmentList where ID=(select GID from TopoEquipment where id = " + drs[i]["ID"] + ")").ToString();
            ControlList[i].TxtItemType = pDataIO.getDbCmdExecuteScalar("select itemType from GlobalAllEquipmentList where ID=(select GID from TopoEquipment where id = " + drs[i]["ID"] + ")").ToString();
            ControlList[i].RoleTxt = drs[i]["Role"].ToString();
            ControlList[i].LblSeq = (i + 1).ToString(); //drs[i]["SEQ"]
            ControlList[i].ConfigUPDownSeqBtnFID = drs[i]["ID"].ToString();
            ControlList[i].ConfigUPDownSeqBtnSEQ = drs[i]["SEQ"].ToString();

            ControlList[i].SetItemDescriptionState(false);
            ControlList[i].TxtItemDescription = "";  //ItemDesc;
            ControlList[i].SetEquipEnableState(false);
            ControlList[i].UPDownSeqBtnVisable = true;
            ControlList[i].EnableDownButton1 = isEditMode;
            ControlList[i].EnableUPButton1 = isEditMode;
            if (i >= 1)
            {
                ControlList[i].LBTH1Visible = false;
                ControlList[i].LBTH2Visible = false;
                ControlList[i].LBTH3Visible = false;

                ControlList[i].LBTH4Visible = false;
                ControlList[i].LBTH5Visible = false;
                ControlList[i].LBTHTitleVisible(false);
            }
            this.plhMain.Controls.Add(ControlList[i]);
        }
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
    public void SeqRecord()
    {
        HtmlTableCell cell = new HtmlTableCell();
        for (int i = 0; i < mydt.Rows.Count; i++)
        {
            Label Lb = new Label();
            Lb.Text = mydtProcess.Rows[i]["SEQ"].ToString().Trim();
            Lb.ID = "SEQ" + mydtProcess.Rows[i]["ID"].ToString().Trim();
            cell.Controls.Add(Lb);
            trFC.Cells.Add(cell);
        }
    }

    public bool AddData(object sender, string prameter)
    {
        Response.Redirect("~/WebFiles/TestPlan/Terry/EquipmentInfo.aspx?AddNew=true&TestPlanID=" + TestPlanID, true);
        return true;
    }

    public bool DeleteData(object obj, string prameter)
    {
        int row = 0;       
        try
        {
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null && ControlList[i].chkIDEquipChecked == true)
                {
                    for (int j = 0; j < mydt.Rows.Count; j++)
                    {
                        if (mydt.Rows[j].RowState != DataRowState.Deleted)
                        {
                            if (mydt.Rows[j]["id"].ToString() == ControlList[i].ID)
                            {
                                mydt.Rows[j].Delete();
                                row++;
                                break;
                            }
                        }
                    }
                }
            }

            if (row == 0)
            {
                this.Page.RegisterStartupScript("", "<script>alert('Did not choose any one！');</script>");
                return false;
            }

            //150527 ---------------------更新方式变更>>Start
            DataTable paramsDt = new DataTable();
            int result = pDataIO.UpdateWithProc("topoEquipment", mydt, paramsDt, logTracingString);
            if (result > 0)
            {
                mydt.AcceptChanges();
            }
            else
            {
                pDataIO.AlertMsgShow("Update data fail!");
            }
            //150527 ---------------------更新方式变更<<End
            //pDataIO.UpdateDataTable(deletStr, mydt);
            Response.Redirect(Request.Url.ToString(), true);
            return true;
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool EditData(object obj, string prameter)
    {
        try
        {
            OptionButtons1.ConfigBtAddVisible = false;
            OptionButtons1.ConfigBtDeleteVisible = false;
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtCancelVisible = true;
            OptionButtons1.ConfigBtEditVisible = false;
            for (int i = 0; i < ControlList.Length; i++)
            {
                if (ControlList[i] != null)
                {
                    //ControlList[i].LblSeq = (i+1).ToString();
                    ControlList[i].EnableUPButton1 = true;
                    ControlList[i].EnableDownButton1 = true;
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

    public bool updateData(object obj, string parameters)
    {
        try
        {
            Frame_TestPlan_EquipList fc = (Frame_TestPlan_EquipList)plhMain.FindControl(Convert.ToString(obj));

            if (fc != null)
            {
                //EditData(obj, parameters);
                string tempSeq = fc.ID;
                if (parameters.ToUpper() == "True".ToUpper())
                {
                    #region SEQ-1;
                    if (Convert.ToInt64(fc.ID) <= 1)
                    {
                        return true;
                    }
                    else
                    {
                        for (int i = 0; i < ControlList.Length; i++)
                        {
                            if (ControlList[i].ID == fc.ID)
                            {
                                if (i <= 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    Label finLB = (Label)tableFC.FindControl("SEQ" + fc.ID);
                                    Label finLB1 = (Label)tableFC.FindControl("SEQ" + ControlList[i - 1].ID);
                                    if (finLB != null && finLB1 != null)
                                    {
                                        string temptext = finLB.Text;
                                        finLB.Text = finLB1.Text;
                                        finLB1.Text = temptext;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region SEQ+1
                    for (int i = 0; i < ControlList.Length; i++)
                    {
                        if (ControlList[i].LblSeq == fc.LblSeq)
                        {
                            if (i >= (ControlList.Length - 1))
                            {
                                return true;
                            }
                            else
                            {
                                Label finLB = (Label)tableFC.FindControl("SEQ" + fc.ID);
                                Label finLB1 = (Label)tableFC.FindControl("SEQ" + ControlList[i + 1].ID);
                                if (finLB != null && finLB1 != null)
                                {
                                    string temptext = finLB.Text;
                                    finLB.Text = finLB1.Text;
                                    finLB1.Text = temptext;
                                }
                            }
                        }
                    }
                    #endregion
                }

                this.plhMain.Controls.Clear();
                bindData(mydtProcess, true);
                return true;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(),"Message", "<script>alert('can not find user control！');</script>");
                return false;
            }
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
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                Label lb = (Label)tableFC.FindControl("SEQ" + Convert.ToString(mydt.Rows[i]["ID"]));
                if (lb != null)
                {
                    if (mydt.Rows[i]["SEQ"].ToString().ToLower() != lb.Text.ToLower())
                    {
                        mydt.Rows[i]["SEQ"] = lb.Text;
                    }
                }
            }

            //150527 ---------------------更新方式变更>>Start
            DataTable paramsDt = new DataTable();
            int result = pDataIO.UpdateWithProc("topoEquipment", mydt, paramsDt, logTracingString);
            if (result > 0)
            {
                mydt.AcceptChanges();
                Response.Redirect(Request.Url.ToString(), true);
                return true;
            }
            else
            {
                pDataIO.AlertMsgShow("Update data fail!", Request.Url.ToString());
                return false;
            }
            //150527 ---------------------更新方式变更<<End
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
            Response.Redirect(Request.Url.ToString());
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
        OptionButtons1.ConfigBtCancelVisible = false;
        #region TestPlanAuthority
        bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
        bool editVidible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);
        if (addVisible == false && deleteVisible == false && editVidible == false)
        {
            bool deletePlan;
            bool testplanEdit = GetTestPlanAuthority(out deletePlan);
            OptionButtons1.ConfigBtAddVisible = testplanEdit;
            if (mydt.Rows.Count <= 0)
            {
                OptionButtons1.ConfigBtEditVisible = false;
                OptionButtons1.ConfigBtDeleteVisible = false;
            }
            else
            {                
                OptionButtons1.ConfigBtEditVisible = testplanEdit;
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
            if (mydt.Rows.Count <= 0)
            {
                OptionButtons1.ConfigBtDeleteVisible = false;
                OptionButtons1.ConfigBtEditVisible = false;
            }
            else
            {
                OptionButtons1.ConfigBtDeleteVisible = deleteVisible;
                OptionButtons1.ConfigBtEditVisible = editVidible;
            }
        }


        #endregion
      
       
        
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (ControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ControlList.Length; i++)
        {
            ControlList[i].EnableDownButton1 = false;
            ControlList[i].EnableUPButton1 = false;
            ControlList[i].chkIDEquipChecked = true;
        }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (ControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ControlList.Length; i++)
        {
            ControlList[i].EnableDownButton1 = false;
            ControlList[i].EnableUPButton1 = false;
            ControlList[i].chkIDEquipChecked = false;
        }
    }
    public bool GetTestPlanAuthority(out bool deleteAuthority)
    {
        string userID = Session["UserID"].ToString().Trim();
        bool tpAuthority = false;
        deleteAuthority = false;
        try
        {

            if (pDataIO.OpenDatabase(true))
            {

                {
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + TestPlanID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {

                        tpAuthority = false;
                        deleteAuthority = false;
                    }
                    else
                    {
                        if (temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["ModifyPlan"].ToString().Trim().ToUpper() == "1")
                        {
                            tpAuthority = true;
                        }
                        else
                        {
                            tpAuthority = false;
                        }
                        if (temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "TRUE" || temp.Rows[0]["DeletePlan"].ToString().Trim().ToUpper() == "1")
                        {
                            deleteAuthority = true;
                        } 
                        else
                        {
                            deleteAuthority = false;
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