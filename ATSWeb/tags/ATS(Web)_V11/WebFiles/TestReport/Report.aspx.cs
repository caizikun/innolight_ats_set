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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
public partial class WebFiles_TestReport_Report : BasePage
{
    //string id;
   public DataIO mysql;
   string funcItemName = "Report";
   private string logTracingString = "";
   public DataTable dt = new DataTable();
    public static DataTable dtform1 = new DataTable();

    // _listKeepColumn	-->不需要轉置的欄位	 
    //_strPivotColumnName	-->需要轉置的欄位
    //_strPivotColumnValue	-->轉置欄位對應的值
    //_listOrderColumnName	-->轉置後需要排序的欄位
    public static DataTable DataTable_Pivot(DataTable _dtSource, List<string> _listKeepColumn,
                       string _strPivotColumnName, string _strPivotColumnValue, List<string> _listOrderColumnName)
    {

        DataTable dt = new DataTable();
        DataRow dr;
        List<string> oLastKey = new List<string>();
        int i = 0;
        int keyColumnIndex = 0;
        int pValIndex = 0;
        int pNameIndex = 0;
        string strTmpColumn = string.Empty;
        bool FirstRow = true;
        bool keyChanged = false;

        pValIndex = _dtSource.Columns.IndexOf(_strPivotColumnValue);
        pNameIndex = _dtSource.Columns.IndexOf(_strPivotColumnName);
        #region 建立欄位
        foreach (string _strColumnName in _listKeepColumn)
        {
            dt.Columns.Add(_dtSource.Columns[_strColumnName].ColumnName.ToString(), _dtSource.Columns[_strColumnName].DataType);
        }
        #endregion
        dr = dt.NewRow();
        #region 建立資料
        foreach (DataRow row in _dtSource.Rows)
        {
            keyColumnIndex = 0;
            keyChanged = false;
            if (!FirstRow)
            {
                while (!keyChanged && keyColumnIndex < _listKeepColumn.Count)
                {
                    if (row[_listKeepColumn[keyColumnIndex]].ToString() != oLastKey[keyColumnIndex])
                    {
                        keyChanged = true;
                    }
                    keyColumnIndex++;
                }
            }
            else
            {
                for (keyColumnIndex = 0; keyColumnIndex < _listKeepColumn.Count; keyColumnIndex++)
                {
                    oLastKey.Add(row[_listKeepColumn[keyColumnIndex]].ToString());
                }
            }
            if (keyChanged || FirstRow)
            {
                if (!FirstRow)
                {
                    dt.Rows.Add(dr);
                }
                dr = dt.NewRow();
                i = 0;
                foreach (string _strColumnName in _listKeepColumn)
                {
                    dr[i] = row[dt.Columns[_strColumnName].ColumnName];
                    i++;
                }
                FirstRow = false;
                for (keyColumnIndex = 0; keyColumnIndex < _listKeepColumn.Count; keyColumnIndex++)
                {
                    oLastKey[keyColumnIndex] = row[_listKeepColumn[keyColumnIndex]].ToString();
                }
            }
            strTmpColumn = row[pNameIndex].ToString();
            if (strTmpColumn.Length > 0)
            {
                if (!dt.Columns.Contains(strTmpColumn) && strTmpColumn != null)
                {
                    dt.Columns.Add(strTmpColumn, _dtSource.Columns[pValIndex].DataType);
                }
                dr[strTmpColumn] = row[pValIndex];
            }
        }
        #endregion
        dt.Rows.Add(dr);
        if (_listOrderColumnName != null)
        {
            if (_listOrderColumnName.Count > 0)
            {
                dt.DefaultView.Sort = string.Join(",", _listOrderColumnName.ToArray());
            }
        }
        return dt;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        IsSessionNull();
        SetSessionBlockType(7);
        string serverName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
        string userId = ConfigurationManager.AppSettings["UserId"].ToString();
        string pwd = ConfigurationManager.AppSettings["Pwd"].ToString();
        mysql = new SqlManager(serverName, dbName, userId, pwd);
        creattNavi();
        if (!this.IsPostBack)
        {
            TextBox5.Text = DateTime.Now.AddDays(-5).ToString();
            //TextBox5.Text = "2014/4/7 15:36:01";
            TextBox6.Text = DateTime.Now.ToString();
           

            string selectcmd = "select distinct (ItemName) from GlobalProductionType where IgnoreFlag=0";
            bool flag = mysql.OpenDatabase(true);
            if (flag == false)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('database failure in link！');", true); 
                //Response.Write("<script>alert('database failue in link！');</script>");
            }
            else
            {
                DropDownList1.Items.Add("");
                DataTable dt1 = mysql.GetDataTable(selectcmd, "GlobalProductionType");
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(dt1.Rows[i]["ItemName"].ToString());
                }
            }
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
       
        databind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (dtform1.Rows.Count != 0)
        {
            GridView1.AllowPaging = false;
            GridView1.AllowSorting = false;
            databind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "gb2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.AppendHeader("content-disposition", "attachment;filename=\"" + System.Web.HttpUtility.UrlEncode(DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8) + ".xls\"");

            Response.ContentType = "application/ms-excel";
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            GridView1.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.Flush();
            Response.End();
            GridView1.AllowSorting = true;
            GridView1.AllowPaging = true;
            databind();
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('No Data to export！');", true); 
            //Response.Write("<script>alert(' No Data！');</script>");

        }
        
    }

    public override void VerifyRenderingInServerForm(Control control)
    { }

    public void databind()
    {
        string selectcmd = "";
        string selectcmd_First = "";
        string selectcmd_Second = "";
        string selectcmd_Third = "";

        string type = "";
        string PN = "";

        if (DropDownList1.Text == "" & TextBox1.Text != "")      //输入SN查询
        {
            string sql = "select distinct GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN],TopoRunRecordTable.SN "
                       + "from GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoRunRecordTable "
                       + "where GlobalProductionType.ID=GlobalProductionName.PID and GlobalProductionName.ID=TopoTestPlan.PID and TopoTestPlan.ID=TopoRunRecordTable.PID and TopoRunRecordTable.SN ='" + TextBox1.Text.Trim()  + "'";

            mysql.OpenDatabase(true);
            DataTable table = mysql.GetDataTable(sql, "");

            if (table.Rows.Count != 0)
            {
                type = table.Rows[0]["ProductType"].ToString();
                PN = table.Rows[0]["PN"].ToString();
            }

            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            selectcmd = "";
            selectcmd_First = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime],(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as Result,TopoRunRecordTable.[EndTime]," + "TopoRunRecordTable.[LightSource] as LightSourceMessage,";
            selectcmd_Second = "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID])) AND [TopoLogRecord].[CtrlType]=2 ";
            
            selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                   "AND GlobalProductionType.[ItemName] ='" + type + "' AND GlobalProductionName.[PN] ='" + PN + "' AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%'";
            
            if ((TextBox5.Text == "") && (TextBox6.Text == ""))
            {
                selectcmd += ""; 
            }
            else if ((TextBox5.Text == "") && (TextBox6.Text != ""))
            {
                selectcmd += " AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";
            }
            else if ((TextBox5.Text != "") && (TextBox6.Text == ""))
            {
                selectcmd += " AND TopoRunRecordTable.[StartTime] >='" + TextBox5.Text + "'";
            }
            else if ((TextBox5.Text != "") && (TextBox6.Text != ""))
            {
                selectcmd += " AND TopoRunRecordTable.[StartTime] >='" + TextBox5.Text + "' AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'"; 
            }
        }
        else
        {
            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            selectcmd = "";
            selectcmd_First = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime],(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as Result,TopoRunRecordTable.[EndTime]," + "TopoRunRecordTable.[LightSource] as LightSourceMessage,";
            selectcmd_Second = "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID])) AND [TopoLogRecord].[CtrlType]=2 ";
            if ((DropDownList3.Text == "") && (TextBox1.Text == "") && (TextBox5.Text == "") && (TextBox6.Text == ""))
            {
                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                   "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'";


            }
            else
            {
                if (DropDownList3.Text != "")
                {
                    if (TextBox1.Text != "")
                    {
                        if (TextBox5.Text != "")
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' ";


                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "'";


                            }
                        }

                    }
                    else
                    {
                        if (TextBox5.Text != "")
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' ";


                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'";


                            }
                        }

                    }
                }
                else
                {
                    if (TextBox1.Text != "")
                    {
                        if (TextBox5.Text != "")
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' ";


                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%" + "'";


                            }
                        }

                    }
                    else
                    {
                        if (TextBox5.Text != "")
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoRunRecordTable.[StartTime]>='" + TextBox5.Text + "' ";


                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <='" + TextBox6.Text + "'";

                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'";

                            }
                        }

                    }

                }

            }        
        }

        selectcmd += " order by TopoRunRecordTable.[StartTime] DESC";  
        mysql.OpenDatabase(true);

        dtform1 = mysql.GetDataTable(selectcmd, "TopoTestData");
        if (dtform1.Rows.Count != 0)
        {
        List<string> oliw = new List<string>();
        List<string> oo = new List<string>();
        oo.Add("SN");
       // oliw.Add("PID");
        oliw.Add("ProductType");
        oliw.Add("ProductName");
        oliw.Add("TestPlan");
        oliw.Add("SN");
        oliw.Add("StartTime");
        oliw.Add("EndTime");
        oliw.Add("Vcc");
        oliw.Add("Temp");
        oliw.Add("Channel");
        oliw.Add("Result");
        dt = DataTable_Pivot(dtform1, oliw, "ItemName", "ItemValue", oo);
       // dt.Columns.Remove("PID");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.HeaderRow.Style.Add("word-break", "keep-all");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.Azure;

                }
                for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                {
                    GridView1.Rows[i].Cells[j].Wrap = false;
                    GridView1.Rows[i].Cells[j].Style.Add("word-break", "keep-all");
                }
            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('No Data！');", true); 
            //Response.Write("<script>alert(' No Data！');</script>"); 
           
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;

        if (DropDownList1.Text == "" & TextBox1.Text == "")
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('请选择Type或输入SN！');", true);
            //Response.Write("<script>alert('Type不能为空！');</script>"); 
            dt = new DataTable();

        }
        else if (DropDownList1.Text != "" & DropDownList2.Text == "")
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('PN不能为空！');", true);
            //Response.Write("<script>alert('PN不能为空！');</script>");
            dt = new DataTable();

        }
        else
        {
            databind();
        }

    }

    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList1.Text != "")
        {
            mysql.OpenDatabase(true);

            string SelectPN = "select distinct GlobalProductionName.PN from GlobalProductionName,GlobalProductionType"
                            + " where GlobalProductionType.ID=GlobalProductionName.PID and GlobalProductionType.ItemName='" + DropDownList1.SelectedItem + "' and GlobalProductionName.IgnoreFlag=0";
            DataTable dt2 = mysql.GetDataTable(SelectPN, "GlobalProductionName");
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //TextBox1.Text = "";
            dt = new DataTable();
            DropDownList2.Items.Add("");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DropDownList2.Items.Add(dt2.Rows[i]["PN"].ToString());
            }
        }
        else
        {
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            //TextBox1.Text = "";
        }
        
    }

    protected void DropDownList2_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList2.Text != "")
        {
            mysql.OpenDatabase(true);

            string SelectName = "select distinct TopoTestPlan.ItemName from TopoTestPlan,GlobalProductionName"
                                + " where GlobalProductionName.ID=TopoTestPlan.PID and GlobalProductionName.PN='" + DropDownList2.SelectedItem + "' and TopoTestPlan.IgnoreFlag=0";
            DataTable dt2 = mysql.GetDataTable(SelectName, "TopoTestPlan");
            DropDownList3.Items.Clear();
            //TextBox1.Text = "";
            dt = new DataTable();
            DropDownList3.Items.Add("");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                DropDownList3.Items.Add(dt2.Rows[i]["ItemName"].ToString());
            }          
        }
        else
        {
            DropDownList3.Items.Clear();
            //TextBox1.Text = "";
        }
    }

    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();

            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "FunctionList", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }
}