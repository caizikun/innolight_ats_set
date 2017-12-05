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
using System.Web.UI.HtmlControls;
public partial class ASPXFlowControlList : BasePage
{
    string funcItemName = "FlowCtrlList";
    ASCXFlowControlList[] TestplanFlowControlList;
    ASCXOptionButtons UserOptionButton;
    private int rowCount;
    private string conn;
   private DataIO pDataIO;
   public DataTable mydt = new DataTable();
   public DataTable mydtProcess = new DataTable();
   private string moduleTypeID = "";
   private string logTracingString = "";
   public ASPXFlowControlList()
    {
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
            TestplanFlowControlList = new ASCXFlowControlList[1];
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {

                TestplanFlowControlList[i] = (ASCXFlowControlList)Page.LoadControl("../../Frame/TestPlan/FlowControlList.ascx");
              

                TestplanFlowControlList[i].LbTH1Text = mydt.Columns[2].ColumnName;
                TestplanFlowControlList[i].LbTH2Text = mydt.Columns[3].ColumnName;
                TestplanFlowControlList[i].LbTH3Text = mydt.Columns[4].ColumnName;
                TestplanFlowControlList[i].LbTH4Text = mydt.Columns[5].ColumnName;
                TestplanFlowControlList[i].LbTH5Text = mydt.Columns[6].ColumnName;

                TestplanFlowControlList[i].ContentTRVisible = false;
                this.FlowControlList.Controls.Add(TestplanFlowControlList[i]);

            }
        } 
        else
        {
            TestplanFlowControlList = new ASCXFlowControlList[rowCount];
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {

                TestplanFlowControlList[i] = (ASCXFlowControlList)Page.LoadControl("../../Frame/TestPlan/FlowControlList.ascx");
                TestplanFlowControlList[i].ID = mydt.Rows[i]["ID"].ToString().Trim();
                TestplanFlowControlList[i].LinkBItemNameID = mydt.Rows[i]["ID"].ToString().Trim();
                TestplanFlowControlList[i].LiBItemNameText = mydt.Rows[i]["ItemName"].ToString().Trim();
                TestplanFlowControlList[i].LbSEQText = (i + 1).ToString().Trim();
                TestplanFlowControlList[i].LbChannelText = mydt.Rows[i]["Channel"].ToString().Trim();
                TestplanFlowControlList[i].LbTempText = mydt.Rows[i]["Temp"].ToString().Trim();
                TestplanFlowControlList[i].LbVccText = mydt.Rows[i]["Vcc"].ToString().Trim();
                TestplanFlowControlList[i].ConfigUPDownButton1FID = mydt.Rows[i]["ID"].ToString().Trim();
                TestplanFlowControlList[i].ConfigUPDownButton1SEQ = mydt.Rows[i]["SEQ"].ToString().Trim();
                TestplanFlowControlList[i].EnableUPButton1 = false;
                TestplanFlowControlList[i].EnableDownButton1 = false;
                TestplanFlowControlList[i].BeSelected = false;

                TestplanFlowControlList[i].LbTH1Text = mydt.Columns[2].ColumnName;
                TestplanFlowControlList[i].LbTH2Text = mydt.Columns[3].ColumnName;
                TestplanFlowControlList[i].LbTH3Text = mydt.Columns[4].ColumnName;
                TestplanFlowControlList[i].LbTH4Text = mydt.Columns[5].ColumnName;
                TestplanFlowControlList[i].LbTH5Text = mydt.Columns[6].ColumnName;
                TestplanFlowControlList[i].LbIgnoreText = mydtProcess.Rows[i]["IgnoreFlag"].ToString().Trim();
                if (i >= 1)
                {
                    TestplanFlowControlList[i].LBTH1Visible = false;
                    TestplanFlowControlList[i].LBTH2Visible = false;
                    TestplanFlowControlList[i].LBTH3Visible = false;
                    TestplanFlowControlList[i].LBTH4Visible = false;
                    TestplanFlowControlList[i].LBTH5Visible = false;
                    TestplanFlowControlList[i].LBTH6Visible = false;
                    TestplanFlowControlList[i].LBTH7Visible = false;
                    TestplanFlowControlList[i].LBTHTitleVisible(false);
                }
                this.FlowControlList.Controls.Add(TestplanFlowControlList[i]);

            }
        }
       

    }
    public void PostBackData()
    {
        TestplanFlowControlList = new ASCXFlowControlList[rowCount];
        ConfigTDProcess();
        for (int i = 0; i < TestplanFlowControlList.Length; i++)
        {

            TestplanFlowControlList[i] = (ASCXFlowControlList)Page.LoadControl("../../Frame/TestPlan/FlowControlList.ascx");
            TestplanFlowControlList[i].ID = mydtProcess.Rows[i]["ID"].ToString().Trim();
            TestplanFlowControlList[i].LinkBItemNameID = mydtProcess.Rows[i]["ID"].ToString().Trim();
            TestplanFlowControlList[i].LiBItemNameText = mydtProcess.Rows[i]["ItemName"].ToString().Trim();
            TestplanFlowControlList[i].LbSEQText = (i+1).ToString().Trim();
            TestplanFlowControlList[i].LbChannelText = mydtProcess.Rows[i]["Channel"].ToString().Trim();
            TestplanFlowControlList[i].LbTempText = mydtProcess.Rows[i]["Temp"].ToString().Trim();
            TestplanFlowControlList[i].LbVccText = mydtProcess.Rows[i]["Vcc"].ToString().Trim();
            TestplanFlowControlList[i].ConfigUPDownButton1FID = mydtProcess.Rows[i]["ID"].ToString().Trim();
            TestplanFlowControlList[i].ConfigUPDownButton1SEQ = mydtProcess.Rows[i]["SEQ"].ToString().Trim();
            TestplanFlowControlList[i].EnableUPButton1 = true;
            TestplanFlowControlList[i].EnableDownButton1 = true;
            //TestplanFlowControlList[i].BeSelected = false;            
            TestplanFlowControlList[i].LbIgnoreText = mydtProcess.Rows[i]["IgnoreFlag"].ToString().Trim();

            TestplanFlowControlList[i].LbTH1Text = mydtProcess.Columns[2].ColumnName;
            TestplanFlowControlList[i].LbTH2Text = mydtProcess.Columns[3].ColumnName;
            TestplanFlowControlList[i].LbTH3Text = mydtProcess.Columns[4].ColumnName;
            TestplanFlowControlList[i].LbTH4Text = mydtProcess.Columns[5].ColumnName;
            TestplanFlowControlList[i].LbTH5Text = mydtProcess.Columns[6].ColumnName;

            if (i >= 1)
            {
                TestplanFlowControlList[i].LBTH1Visible = false;
                TestplanFlowControlList[i].LBTH2Visible = false;
                TestplanFlowControlList[i].LBTH3Visible = false;
                TestplanFlowControlList[i].LBTH4Visible = false;
                TestplanFlowControlList[i].LBTH5Visible = false;
                TestplanFlowControlList[i].LBTH6Visible = false;
                TestplanFlowControlList[i].LBTH7Visible = false;
                TestplanFlowControlList[i].LBTHTitleVisible(false);
            }
            this.FlowControlList.Controls.Add(TestplanFlowControlList[i]);

        }
    }
    public bool connectDataBase()
    {
        try
        {
            if (pDataIO.OpenDatabase(true))
            {
                mydt = pDataIO.GetDataTable("select * from TopoTestControl where PID=" + moduleTypeID + "ORDER BY SEQ", "TopoTestControl");
                mydtProcess = mydt;              
                rowCount = mydt.Rows.Count;
                SeqRecord();
                if (!IsPostBack)
                {
                    bindData();
                }
                else
                {
                    PostBackData();
                }
                string parentItem = pDataIO.getDbCmdExecuteScalar("select itemName from TopoTestPlan where id = " + moduleTypeID).ToString();
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
    public bool updateData(object obj, string parameters)
    {
        string tempLPSEQText = "";
        try
        {
            ASCXFlowControlList fc = (ASCXFlowControlList)FlowControlList.FindControl(Convert.ToString(obj));
            tempLPSEQText = fc.ID;
            if (fc != null)
            {
                if (parameters=="True")
                {
                    if (Convert.ToInt64(tempLPSEQText) <= 0)
                    {
                        return true;
                    }
                    else
                    {
                        for (int i = 0; i < TestplanFlowControlList.Length; i++)
                        {
                           
                            if (TestplanFlowControlList[i].ID == fc.ID)
                            {
                                if (i<=0)
                                {
                                    return true;
                                }
                                else
                                {
                                    if (Convert.ToUInt32(mydtProcess.Rows[i]["SEQ"])<=1)
                                     {
                                         return true;
                                     }
                                    Label finLB = (Label)tableFC.FindControl("SEQ" + fc.ID);
                                    Label finLB1 = (Label)tableFC.FindControl("SEQ" + TestplanFlowControlList[i-1].ID);
                                    if (finLB != null&&finLB1!=null)
                                    {
                                        string temptext = finLB.Text;
                                        finLB.Text = finLB1.Text;
                                       finLB1.Text = temptext;
                                    }
                                    //mydt.Rows[i]["SEQ"] = mydt.Rows[i - 1]["SEQ"];
                                    //mydt.Rows[i-1]["SEQ"] = fc.LbSEQText;
                                    //TestplanFlowControlList[i].LbSEQText = TestplanFlowControlList[i - 1].LbSEQText;
                                    //TestplanFlowControlList[i - 1].LbSEQText = tempLPSEQText;
                                    //string cmdString = "select * from TopoTestControl  where PID=" + moduleTypeID + "ORDER BY SEQ";
                                    //pDataIO.UpdateDataTable(cmdString, mydt);
                                    //Response.Redirect(Request.Url.ToString());
                                    
                                }


                            }

                        }

                    }
                }
                else
                {
                    for (int i = 0; i < TestplanFlowControlList.Length; i++)
                    {
                        if (TestplanFlowControlList[i].ID == fc.ID)
                        {
                            if (i >= rowCount-1)
                            {
                                return true;
                            }
                            else
                            {
                                Label finLB = (Label)tableFC.FindControl("SEQ" + fc.ID);
                                Label finLB1 = (Label)tableFC.FindControl("SEQ" + TestplanFlowControlList[i + 1].ID);
                                if (finLB != null && finLB1 != null)
                                {
                                    string temptext = finLB.Text;
                                    finLB.Text = finLB1.Text;
                                    finLB1.Text = temptext;
                                }
                                //mydt.Rows[i]["SEQ"] = mydt.Rows[i +1]["SEQ"];
                                //mydt.Rows[i +1]["SEQ"] = fc.LbSEQText;
                                //TestplanFlowControlList[i].LbSEQText = TestplanFlowControlList[i + 1].LbSEQText;
                                //TestplanFlowControlList[i + 1].LbSEQText = tempLPSEQText;
                                //string cmdString = "select * from TopoTestControl  where PID=" + moduleTypeID + "ORDER BY SEQ";
                                //pDataIO.UpdateDataTable(cmdString, mydt);
                                //mydt.Clear();
                                //mydt = pDataIO.GetDataTable("select * from TopoTestControl  where PID=" + moduleTypeID + "ORDER BY SEQ", "TopoTestControl");
                                //Response.Redirect(Request.Url.ToString());
                                
                            }


                        }

                    }
                }
                this.FlowControlList.Controls.Clear();
                PostBackData();
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
    protected void LoadOptionButton()
    {
        //UserOptionButton = new ASCXOptionButtons();
        //UserOptionButton = (ASCXOptionButtons)Page.LoadControl("../../Frame/OptionButtons.ascx");
        //UserOptionButton.ID = "0";
        //this.OptionButton.Controls.Add(UserOptionButton);
    }
    public bool AddData(object obj, string prameter)
    {
        try
        {
            Response.Redirect("~/WebFiles/TestPlan/AddNewFlowControl.aspx?uId=" + moduleTypeID.Trim());
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    public bool DeleteData(object obj, string prameter)
    {
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('Are you sure you want to delete？')){DeleteData1();}else{}</script>");
        int row = 0;
        bool isSelected = false;
        string deletStr = "select * from TopoTestControl  where IgnoreFlag='False'and PID=" + moduleTypeID + "ORDER BY SEQ";
        try
        {
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {
                ASCXFlowControlList cb = (ASCXFlowControlList)FlowControlList.FindControl(TestplanFlowControlList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        row++;
                        isSelected = true;
                        //mydt.Rows[i].Delete();
                        mydt.Rows[i]["IgnoreFlag"] = true;
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
                //Response.Write("<script>alert('Did not choose any one！');</script>");               
                return false;
            }
           int result= pDataIO.UpdateWithProc("TopoTestControl", mydt, deletStr, logTracingString);
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
    public bool EditData(object obj, string prameter)
    {
        try
        {
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {
                TestplanFlowControlList[i].EnableUPButton1 = true;
                TestplanFlowControlList[i].EnableDownButton1 = true;
            }
            OptionButtons1.ConfigBtSaveVisible = true;
            OptionButtons1.ConfigBtAddVisible = false;
            OptionButtons1.ConfigBtCopyVisible = false;
            OptionButtons1.ConfigBtEditVisible = false;
            OptionButtons1.ConfigBtDeleteVisible = false;
            OptionButtons1.ConfigBtCancelVisible = true;
            SelectAll.Visible = false;
            DeSelectAll.Visible = false;
            return true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }
    public bool SaveData(object obj, string prameter)
    {
        try
        {
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {
                Label lb = (Label)tableFC.FindControl("SEQ" + Convert.ToString(mydt.Rows[i]["ID"]));
                if (lb!=null)
                {
                    mydt.Rows[i]["SEQ"] = lb.Text;
                }
                
            }
            string cmdString = "select * from TopoTestControl  where PID=" + moduleTypeID + "ORDER BY SEQ";
           int result= pDataIO.UpdateWithProc("TopoTestControl", mydt, cmdString, logTracingString);
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
    public bool CancelUpdata(object obj, string prameter)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
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
        OptionButtons1.ConfigBtCancelVisible = false;
        OptionButtons1.ConfigBtSaveVisible = false;
        #region TestPlanAuthority
        bool addVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.AddATSPlan, myAccessCode);
        bool deleteVisible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.DeleteATSPlan, myAccessCode);
        bool editVidible = mCommCtrl.CheckLoginAccess(CommCtrl.FunctionName.ATSPlan, CommCtrl.CheckAccess.MofifyATSPlan, myAccessCode);
        if (addVisible == false && deleteVisible == false && editVidible==false)
        {
            bool deletePlan;
            bool testplanEdit = GetTestPlanAuthority(out deletePlan);
            OptionButtons1.ConfigBtAddVisible = testplanEdit;          
            if (rowCount <= 0)
            {
                OptionButtons1.ConfigBtEditVisible = false;
                OptionButtons1.ConfigBtDeleteVisible = false;
                OptionButtons1.ConfigBtCopyVisible = false;
            }
            else
            {              
                OptionButtons1.ConfigBtEditVisible = testplanEdit;
                OptionButtons1.ConfigBtCopyVisible = testplanEdit;
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
            
            if (rowCount <= 0)
            {
                OptionButtons1.ConfigBtDeleteVisible = false;
                OptionButtons1.ConfigBtEditVisible = false;
                OptionButtons1.ConfigBtCopyVisible = false;
            }
            else
            {
                OptionButtons1.ConfigBtDeleteVisible = deleteVisible;
                OptionButtons1.ConfigBtEditVisible = editVidible;
                OptionButtons1.ConfigBtCopyVisible = addVisible;
            }
        }


        #endregion
    }
    public void SeqRecord()
    {
        //if (!IsPostBack)
        {
            //HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            
            for (int i = 0; i < mydt.Rows.Count;i++ )
            {
                Label Lb = new Label();
                Lb.Text = mydtProcess.Rows[i]["SEQ"].ToString().Trim();
                Lb.ID = "SEQ"+mydtProcess.Rows[i]["ID"].ToString().Trim();
                cell.Controls.Add(Lb);

                trFC.Cells.Add(cell);
                
            }
            //this.tableFC.Rows.Add(trFC);
        }
        
    }
    public void ConfigTDProcess()
    {
        for (int i = 0; i < mydtProcess.Rows.Count; i++)
        {
            Label finLB = (Label)tableFC.FindControl("SEQ" + mydtProcess.Rows[i]["ID"].ToString().Trim());           
            if (mydtProcess.Rows[i]["SEQ"].ToString().ToLower() != finLB.Text.ToLower()) //150529 防止出现多条记录被修改 而实际内容没变
            {
                mydtProcess.Rows[i]["SEQ"] = finLB.Text;
            }
        }
        DataView dv = mydtProcess.DefaultView;
        dv.Sort = "SEQ";
        mydtProcess = dv.ToTable();
    }
    protected void SelectAll_Click(object sender, EventArgs e)
    {
        if (TestplanFlowControlList.Length<=0)
        {
            return;
        }
        for (int i = 0; i < TestplanFlowControlList.Length; i++)
        {
            TestplanFlowControlList[i].BeSelected = true;
         }
    }
    protected void DeselectAll_Click(object sender, EventArgs e)
    {
        if (TestplanFlowControlList.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < TestplanFlowControlList.Length; i++)
        {
            TestplanFlowControlList[i].BeSelected = false;
        }   
    }
    public void ClearCurrenPage()
    {
        if (rowCount==0)
        {
            SelectAll.Visible = false;
            DeSelectAll.Visible = false;
        }
    }

    public bool GetTestPlanAuthority(out bool deletePlan)
    {
        string userID = Session["UserID"].ToString().Trim();
        bool tpAuthority = false;
        deletePlan = false;
        try
        {

            if (pDataIO.OpenDatabase(true))
            {

                {
                    DataTable temp = pDataIO.GetDataTable("select * from UserPlanAction where UserID=" + userID + "and PlanID=" + moduleTypeID, "UserPlanAction");

                    if (temp == null || temp.Rows.Count == 0 || temp.Rows.Count > 1)
                    {
                        deletePlan = false;
                        tpAuthority = false;
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
                            deletePlan = true;
                        }
                        else
                        {
                            deletePlan = false;
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

    public bool CopyData(object obj, string prameter)
    {
        bool isSelected = false;
        int selectCount = 0;
        string copySourceID = "";
        try
        {
            for (int i = 0; i < TestplanFlowControlList.Length; i++)
            {
                ASCXFlowControlList cb = (ASCXFlowControlList)FlowControlList.FindControl(TestplanFlowControlList[i].ID);
                if (cb != null)
                {
                    if (cb.BeSelected == true)
                    {
                        selectCount++;
                        isSelected = true;
                        copySourceID = cb.LinkBItemNameID;
                    }
                }
                else
                {
                    Response.Write("<script>alert('can not find user control！');</script>");
                    return false;
                }
            }
            if (isSelected == false || selectCount > 1)
            {
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('Did not choose any one！');return false;</script>");                
                this.Page.RegisterStartupScript("", "<script>alert('please choose one only！');</script>");
                //Response.Write("<script>alert('Did not choose any one！');</script>");
                return false;
            }

            Response.Redirect("~/WebFiles/TestPlan/CopyFlowControl.aspx?uId=" + moduleTypeID.Trim() + "&sourceID=" + copySourceID);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return true;
    }
    
}
