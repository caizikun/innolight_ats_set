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
    const string funcItemName = "设备列表";
    Frame_TestPlan_EquipList[] ControlList;
    public DataTable mydt = new DataTable();
    public DataTable mydtProcess = new DataTable();
    private int rowCount = 0;
    private CommCtrl pCommCtrl = new CommCtrl();

    private DataIO pDataIO;
    private string TestPlanID = "-1";
    private string queryStr = "";
    private string logTracingString = "";
    private string CheckedID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();

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

        Session["TreeNodeExpand"] = null;
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
            HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "0"] = pDataIO.getDbCmdExecuteScalar("select ItemName from FunctionTable where BlockLevel=0 and BlockTypeID = " + Session["BlockType"].ToString()).ToString();
            HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "0_Page"] = "";

            HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "2"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.PN from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + TestPlanID).ToString();
            string uid2 = pDataIO.getDbCmdExecuteScalar("select GlobalProductionName.ID from GlobalProductionName,TopoTestPlan where TopoTestPlan.PID=GlobalProductionName.ID and TopoTestPlan.id = " + TestPlanID).ToString();
            HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "2_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestPlanList.aspx?uId=" + uid2;

            HttpContext.Current.Session["txtLevelID_" + Session["BlockType"].ToString() + "1"] = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ItemName from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
            string uid = pDataIO.getDbCmdExecuteScalar("select GlobalProductionType.ID from GlobalProductionName,GlobalProductionType where GlobalProductionName.PID=GlobalProductionType.ID and GlobalProductionName.id = " + uid2).ToString();
            HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "1_Page"] = "~/WebFiles/Production_ATS/Production/ProductionPNList.aspx?uId=" + uid;

            HttpContext.Current.Session["LevelID_" + Session["BlockType"].ToString() + "3_Page"] = "~/WebFiles/Production_ATS/TestPlan/TestplanSelfInfor.aspx?uId=" + TestPlanID;               
            string parentItem = pDataIO.getDbCmdExecuteScalar("select ItemName from TopoTestPlan where id = " + TestPlanID).ToString();

            Control myCtrl = pCommCtrl.CreateNaviCtrl(funcItemName, parentItem, Session["BlockType"].ToString(), pDataIO, out logTracingString);
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
                ControlList[i].LnkItemNamePostBackUrl = "~/WebFiles/Production_ATS/TestPlan/EquipmentInfo.aspx?uId=" + drs[i]["ID"] + "&TestPlanID=" + TestPlanID;
                ControlList[i].LnkItemName = pDataIO.getDbCmdExecuteScalar("select showName from GlobalAllEquipmentList where ID=(select GID from TopoEquipment where id = " + drs[i]["ID"] + ")").ToString();
                ControlList[i].TxtItemType = pDataIO.getDbCmdExecuteScalar("select itemType from GlobalAllEquipmentList where ID=(select GID from TopoEquipment where id = " + drs[i]["ID"] + ")").ToString();
                ControlList[i].RoleTxt = drs[i]["Role"].ToString();
                ControlList[i].LblSeq = (i + 1).ToString(); //drs[i]["SEQ"]

                ControlList[i].SetItemDescriptionState(false);
                ControlList[i].TxtItemDescription = "";  //ItemDesc;
                ControlList[i].SetEquipEnableState(false);

                if (CheckedID != "")
                {
                    if (ControlList[i].ID == CheckedID)
                    {
                        ControlList[i].chkIDEquipChecked = true;
                    }
                }

                if (i >= 1)
                {
                    ControlList[i].LBTHTitleVisible(false);
                    if (i % 2 != 0)
                    {
                        ControlList[i].TrBackgroundColor = "#F2F2F2";
                    }
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
        Response.Redirect("~/WebFiles/Production_ATS/TestPlan/EquipmentInfo.aspx?AddNew=true&TestPlanID=" + TestPlanID, true);
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
                this.Page.RegisterStartupScript("", "<script>alert('请至少选择一个！');</script>");
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
                pDataIO.AlertMsgShow("数据更新失败!");
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

    public bool OrderInfor(object obj, string prameter)
    {
        try
        {
            if (rowCount > 1)
            {
                OptionButtons1.ConfigBtSaveVisible = true;
                OptionButtons1.ConfigBtCancelVisible = true;
                OptionButtons1.ConfigBtEditVisible = false;
                OptionButtons1.ConfigBtOrderVisible = false;
                OptionButtons1.ConfigBtAddVisible = false;
                OptionButtons1.ConfigBtDeleteVisible = false;

                OptionButtons1.ConfigBtOrderUpVisible = true;
                OptionButtons1.ConfigBtOrderDownVisible = true;

                SelectAll.Visible = false;
                DeSelectAll.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('仅有一项，无法排序！')", true);
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
                pDataIO.AlertMsgShow("数据更新失败!", Request.Url.ToString());
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
        if (!IsPostBack)
        {
            int myAccessCode = 0;
            if (Session["AccCode"] != null)
            {
                myAccessCode = Convert.ToInt32(Session["AccCode"]);
            }
            CommCtrl mCommCtrl = new CommCtrl();

            OptionButtons1.ConfigBtSaveVisible = false;
            OptionButtons1.ConfigBtCancelVisible = false;
            OptionButtons1.ConfigBtEditVisible = false;

            #region TestPlanAuthority
            bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
            bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
            bool editVidible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ProductionATS, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);

            if (addVisible == false && deleteVisible == false && editVidible == false)
            {
                bool deletePlan;
                bool testplanEdit = GetTestPlanAuthority(out deletePlan);
                OptionButtons1.ConfigBtAddVisible = testplanEdit;
                if (mydt.Rows.Count <= 0)
                {
                    OptionButtons1.ConfigBtOrderVisible = false;
                    OptionButtons1.ConfigBtDeleteVisible = false;
                }
                else
                {
                    if (rowCount == 1)
                    {
                        OptionButtons1.ConfigBtOrderVisible = false;
                    }
                    else
                    {
                        OptionButtons1.ConfigBtOrderVisible = testplanEdit;
                    }
                    
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
                    OptionButtons1.ConfigBtOrderVisible = false;
                }
                else
                {
                    if (rowCount == 1)
                    {
                        OptionButtons1.ConfigBtOrderVisible = false;
                    }
                    else
                    {
                        OptionButtons1.ConfigBtOrderVisible = editVidible;
                    }

                    OptionButtons1.ConfigBtDeleteVisible = deleteVisible;
                }
            }
            #endregion        
        }                   
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (ControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < ControlList.Length; i++)
        {
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
                DataTable pnId = pDataIO.GetDataTable("select * from TopoTestPlan where ID=" + TestPlanID, "TopoTestPlan");

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
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + TestPlanID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                        //tpAuthority = false;
                        deleteAuthority = false;
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

    public string CheckSelected()
    {
        string id = "none";

        for (int i = 0; i < ControlList.Length; i++)
        {
            if (ControlList[i].chkIDEquipChecked == true)
            {
                if (id == "none")
                {
                    id = ControlList[i].ID;
                }
                else
                {
                    id = "NotOne";
                    break;
                }
            }
        }

        return id;
    }

    public bool OrderUp(object obj, string prameter)
    {
        try
        {
            string ss = CheckSelected();

            if (ss == "none" || ss == "NotOne")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('请勾选一项进行排序！')", true);
                return false;
            }
            else
            {
                #region SEQ-1;
                if (Convert.ToInt64(ss) <= 1)
                {
                    return true;
                }
                else
                {
                    for (int i = 0; i < ControlList.Length; i++)
                    {
                        if (ControlList[i].ID == ss)
                        {
                            if (i <= 0)
                            {
                                return true;
                            }
                            else
                            {
                                Label finLB = (Label)tableFC.FindControl("SEQ" + ss);
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

                this.plhMain.Controls.Clear();
                CheckedID = ss;
                bindData(mydtProcess, true);
                return true;
            }
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

    public bool OrderDown(object obj, string prameter)
    {
        try
        {
            string ss = CheckSelected();

            if (ss == "none" || ss == "NotOne")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(UpdatePanel), " ", "alert('请勾选一项进行排序！')", true);
                return false;
            }
            else
            {
                #region SEQ+1
                for (int i = 0; i < ControlList.Length; i++)
                {
                    if (ControlList[i].ID == ss)
                    {
                        if (i >= (ControlList.Length - 1))
                        {
                            return true;
                        }
                        else
                        {
                            Label finLB = (Label)tableFC.FindControl("SEQ" + ss);
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

                this.plhMain.Controls.Clear();
                CheckedID = ss;
                bindData(mydtProcess, true);
                return true;
            }
        }
        catch (System.Exception ex)
        {
            pDataIO.WriteErrorLogs(ex.ToString());
            throw ex;
        }
    }

}