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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;


public partial class WebFiles_TestReport_Report : BasePage
{
    //string id;
    public DataIO mysql;
    string funcItemName = "报表";
    private string logTracingString = "";
    public DataTable dt = new DataTable();
    public static DataTable dtform1 = new DataTable();

    Dictionary<string, int> ItemCountList = new Dictionary<string, int>();

    // _listKeepColumn	-->不需要轉置的欄位	 
    //_strPivotColumnName	-->需要轉置的欄位
    //_strPivotColumnValue	-->轉置欄位對應的值
    //_listOrderColumnName	-->轉置後需要排序的欄位
    public DataTable DataTable_Pivot(DataTable _dtSource, List<string> _listKeepColumn,
                       string _strPivotColumnName, string _strPivotColumnValue, List<string> _listOrderColumnName)
    {
        string selectSpec = param1.Value;   //筛选表头

        DataTable showNameDT = new DataTable();
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
        int Index = 0;

        if (selectSpec != "")
        {
            string sql = "select ReportHeaderSpecs.*,GlobalSpecs.*,ReportHeader.ID as HeaderID from ReportHeaderSpecs,GlobalSpecs,ReportHeader where ReportHeaderSpecs.SpecID = GlobalSpecs.ID and ReportHeaderSpecs.PID = ReportHeader.ID" +
                         " and ReportHeader.ItemName ='" + DropDownList4.Text + "' and ReportHeaderSpecs.ShowName!=''";
            showNameDT = mysql.GetDataTable(sql, "showNameDT");
        }

        pValIndex = _dtSource.Columns.IndexOf(_strPivotColumnValue);
        pNameIndex = _dtSource.Columns.IndexOf(_strPivotColumnName);
        #region 建立欄位
        foreach (string _strColumnName in _listKeepColumn)
        {
            dt.Columns.Add(_dtSource.Columns[_strColumnName].ColumnName.ToString(), _dtSource.Columns[_strColumnName].DataType);
        }

        if (selectSpec != "") 
        {
            string[] ss = selectSpec.Split(',');

            for (int m = 0; m < ss.Length; m++)
            {
                dt.Columns.Add(ss[m].Trim());           //添加固定表头
            }    
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

            if (selectSpec == "")
            {
                strTmpColumn = row[pNameIndex].ToString();
                if (strTmpColumn.Length > 0)
                {
                    if (!dt.Columns.Contains(strTmpColumn) && strTmpColumn != null)
                    {
                        dt.Columns.Add(strTmpColumn, _dtSource.Columns[pValIndex].DataType);

                        sela.Items.Add(strTmpColumn);
                        sela.Items[Index].Selected = true;
                        Index++;
                    }
                    dr[strTmpColumn] = row[pValIndex];
                }
            }
            else
            {
                strTmpColumn = row[pNameIndex].ToString();
                bool SpecsExit = false;
                if (strTmpColumn.Length > 0)
                {
                    for (int n = _listKeepColumn.Count; n < dt.Columns.Count; n++)
                    {
                        string ColumnName = ""; 
                        for (int cnt = 0; cnt < showNameDT.Rows.Count; cnt++)
                        {
                            if (dt.Columns[n].ColumnName.ToUpper() == showNameDT.Rows[cnt]["ShowName"].ToString().ToUpper())
                            {
                                if (showNameDT.Rows[cnt]["Unit"].ToString() != "")
                                {
                                    ColumnName = showNameDT.Rows[cnt]["ItemName"].ToString() + "(" + showNameDT.Rows[cnt]["Unit"].ToString() + ")";
                                }
                                else
                                {
                                    ColumnName = showNameDT.Rows[cnt]["ItemName"].ToString();
                                }
                                break;
                            }
                        }

                        if (ColumnName == "")
                        {
                            ColumnName = dt.Columns[n].ColumnName;
                        }

                        if (ColumnName.ToUpper() == strTmpColumn.ToUpper() && strTmpColumn != null) 
                        {
                            strTmpColumn = dt.Columns[n].ColumnName;
                            SpecsExit = true;
                            break;
                        }
                    }

                    if (SpecsExit == true)
                    {
                        dr[strTmpColumn] = row[pValIndex];
                    }                                   
                }
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
        Session["TreeNodeExpand"] = null;
        SetSessionBlockType(2);
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
        mysql = new SqlManager(serverName, dbName, userId, pwd);
        creattNavi();
        if (!this.IsPostBack)
        {
            TextBox5.Text = DateTime.Now.AddDays(-5).ToString();
            TextBox6.Text = DateTime.Now.ToString();

            Session["SN"] = "";
            Session["StartTime"] = TextBox5.Text;
            Session["StopTime"] = TextBox6.Text;

            string selectcmd = "select distinct (ItemName) from GlobalProductionType where IgnoreFlag=0";
            bool flag = mysql.OpenDatabase(true);
            if (flag == false)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('数据库连接失败！');", true);
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

                DropDownList4.Items.Add("");
                //DropDownList4.Items.Add("全表头");
                DataTable HeaderDt = mysql.GetDataTable("select * from ReportHeader", "ReportHeader");
                for (int i = 0; i < HeaderDt.Rows.Count; i++)
                {
                    DropDownList4.Items.Add(HeaderDt.Rows[i]["ItemName"].ToString());
                }
            }         
        }

        foreach (ListItem item in DropDownList3.Items)
        {
            item.Attributes.Add("Title", item.Text);
        }

        foreach (ListItem item in DropDownList2.Items)
        {
            item.Attributes.Add("Title", item.Text);
        }

        Image1.Visible = false;

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
            databind();
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('没有数据，无法导出！  注：如在未选择类型的情况下，使用SN进行查询，请输入完整的SN。');", true); 
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

        string type = "GlobalProductionType.[ItemName] =''";
        string PN = "GlobalProductionName.[PN] =''";

        if (DropDownList1.Text == "" & TextBox1.Text != "")      //输入SN查询
        {
            string sql = "select distinct GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoRunRecordTable.SN "
                       + "from GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoRunRecordTable "
                       + "where GlobalProductionType.ID=GlobalProductionName.PID and GlobalProductionName.ID=TopoTestPlan.PID and TopoTestPlan.ID=TopoRunRecordTable.PID and TopoRunRecordTable.SN ='" + TextBox1.Text.Trim()  + "'";

            mysql.OpenDatabase(true);
            DataTable table = mysql.GetDataTable(sql, "");

            ArrayList typeList = new ArrayList();
            ArrayList PNList = new ArrayList();

            if (table.Rows.Count != 0)
            {               
                typeList.Add(table.Rows[0]["类型"].ToString());
                PNList.Add(table.Rows[0]["品名"].ToString());

                for (int cnt = 1; cnt < table.Rows.Count; cnt++)
                {
                    for (int typeCnt = 0; typeCnt < typeList.Count; typeCnt++)
                    {
                        if (table.Rows[cnt]["类型"].ToString() != typeList[typeCnt].ToString())
                        {
                            typeList.Add(table.Rows[cnt]["类型"].ToString());
                        }
                    }

                    for (int PNCnt = 0; PNCnt < PNList.Count; PNCnt++)
                    {
                        if (table.Rows[cnt]["品名"].ToString() != PNList[PNCnt].ToString())
                        {
                            PNList.Add(table.Rows[cnt]["品名"].ToString());
                        }
                    }
                }                
            }

            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            selectcmd = "";
            selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp] as 温度,TopoLogRecord.[Voltage] as 电压,TopoLogRecord.[Channel] as 通道,TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime] as 开始时间,(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as 结果,TopoRunRecordTable.[EndTime] as 结束时间," + "TopoRunRecordTable.[LightSource] as LightSourceMessage,";
            selectcmd_Second = "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID])) AND [TopoLogRecord].[CtrlType]=2 ";

            for (int i = 0; i < typeList.Count; i++)
            {
                if (i == 0)
                {
                    type = "GlobalProductionType.[ItemName] ='" + typeList[0].ToString() + "'";
                }
                else
                {
                    type += " or GlobalProductionType.[ItemName] ='" + typeList[i].ToString() + "'";
                }                                
            }

            for (int j = 0; j < PNList.Count; j++)
            {
                if (j == 0)
                {
                    PN = "GlobalProductionName.[PN] ='" + PNList[0].ToString() + "'";
                }
                else
                {
                    PN += " or GlobalProductionName.[PN] ='" + PNList[j].ToString() + "'";
                }
            }
                                                    
            selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                   "AND (" + type + ") AND (" + PN + ") AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%'";
            
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
            selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp] as 温度,TopoLogRecord.[Voltage] as 电压,TopoLogRecord.[Channel] as 通道,TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime] as 开始时间,(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as 结果,TopoRunRecordTable.[EndTime] as 结束时间," + "TopoRunRecordTable.[LightSource] as LightSourceMessage,";
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

        selectcmd += " order by TopoRunRecordTable.[StartTime] DESC, TopoTestData.PID";  
        mysql.OpenDatabase(true);

        dtform1 = mysql.GetDataTable(selectcmd, "TopoTestData");
        if (dtform1.Rows.Count != 0)
        {
            List<string> oliw = new List<string>();
            List<string> oo = new List<string>();
            oo.Add("SN");
           // oliw.Add("PID");
            oliw.Add("类型");
            oliw.Add("品名");
            oliw.Add("测试方案");
            oliw.Add("SN");
            oliw.Add("开始时间");
            oliw.Add("结束时间");
            oliw.Add("电压");
            oliw.Add("温度");
            oliw.Add("通道");
            oliw.Add("结果");
            dt = DataTable_Pivot(dtform1, oliw, "ItemName", "ItemValue", oo);
           // dt.Columns.Remove("PID");

            dt.DefaultView.Sort = "开始时间 desc";
            dt.AcceptChanges();

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

            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "showMultiselect();", true); 
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('没有数据！  注：如在未选择类型的情况下，使用SN进行查询，请输入完整的SN。');", true); 
            //Response.Write("<script>alert(' No Data！');</script>"); 
           
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != Session["SN"].ToString() || TextBox5.Text != Session["StartTime"].ToString() || TextBox6.Text != Session["StopTime"].ToString())
        {
            Session["SN"] = TextBox1.Text;
            Session["StartTime"] = TextBox5.Text;
            Session["StopTime"] = TextBox6.Text;

            if (DropDownList4.Text == "")
            {
                sela.Items.Clear();
                param1.Value = "";
            }            
        }

        if (param1.Value == "")
        {
            DropDownList4.SelectedIndex = 0;
            sela.Items.Clear();
        }

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

    protected void Button3_Click(object sender, EventArgs e)
    {
        sela.Items.Clear();

        if (TextBox1.Text != Session["SN"].ToString() || TextBox5.Text != Session["StartTime"].ToString() || TextBox6.Text != Session["StopTime"].ToString())
        {
            Session["SN"] = TextBox1.Text;
            Session["StartTime"] = TextBox5.Text;
            Session["StopTime"] = TextBox6.Text;
        }

        if (DropDownList5.Text == "")
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('请选择数据分析类型！');", true);
        }
        else if (DropDownList1.Text == "" & TextBox1.Text == "")
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('请选择Type或输入SN！');", true);
        }
        else if (DropDownList1.Text != "" & DropDownList2.Text == "")
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('PN不能为空！');", true);
        }
        else
        {
            if (DropDownList5.SelectedIndex == 1)
            {
                dataStatistics();             //失效项数目统计柱状图
            }
            else if (DropDownList5.SelectedIndex == 2)
            {
                if (DropDownList6.SelectedItem.Text == "")
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('请选择需要分析的测试项！');", true);
                }
                else
                {
                    dataNormalDistribution();         //测试项正态分布统计图  
                }                          
            }
            else if (DropDownList5.SelectedIndex == 3)
            {
                if (DropDownList6.SelectedItem.Text == "")
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('请选择需要分析的测试项！');", true);
                }
                else
                {
                    dataChannelChart();         //模块各通道测试项折线图或箱线图  
                }
            } 
        }
    }

    public void dataStatistics()
    {
        string selectcmd = "";
        string selectcmd_First = "";
        string selectcmd_Second = "";
        string selectcmd_Third = "";

        string type = "GlobalProductionType.[ItemName] =''";
        string PN = "GlobalProductionName.[PN] =''";

        if (DropDownList1.Text == "" & TextBox1.Text != "")      //输入SN查询
        {
            string sql = "select distinct GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoRunRecordTable.SN "
                       + "from GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoRunRecordTable "
                       + "where GlobalProductionType.ID=GlobalProductionName.PID and GlobalProductionName.ID=TopoTestPlan.PID and TopoTestPlan.ID=TopoRunRecordTable.PID and TopoRunRecordTable.SN ='" + TextBox1.Text.Trim() + "'";

            mysql.OpenDatabase(true);
            DataTable table = mysql.GetDataTable(sql, "");

            ArrayList typeList = new ArrayList();
            ArrayList PNList = new ArrayList();

            if (table.Rows.Count != 0)
            {
                typeList.Add(table.Rows[0]["类型"].ToString());
                PNList.Add(table.Rows[0]["品名"].ToString());

                for (int cnt = 1; cnt < table.Rows.Count; cnt++)
                {
                    for (int typeCnt = 0; typeCnt < typeList.Count; typeCnt++)
                    {
                        if (table.Rows[cnt]["类型"].ToString() != typeList[typeCnt].ToString())
                        {
                            typeList.Add(table.Rows[cnt]["类型"].ToString());
                        }
                    }

                    for (int PNCnt = 0; PNCnt < PNList.Count; PNCnt++)
                    {
                        if (table.Rows[cnt]["品名"].ToString() != PNList[PNCnt].ToString())
                        {
                            PNList.Add(table.Rows[cnt]["品名"].ToString());
                        }
                    }
                }
            }

            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            selectcmd = "";         
            //selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp] as 温度,TopoLogRecord.[Voltage] as 电压,TopoLogRecord.[Channel] as 通道,TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime] as 开始时间,(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as 结果,TopoRunRecordTable.[EndTime] as 结束时间,";
            //selectcmd_Second = "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            selectcmd_First = "select TopoRunRecordTable.[SN],TopoLogRecord.[Channel] as 通道,";
            selectcmd_Second = "TopoTestData.ItemName as 失效项,(case when TopoTestData.ItemValue > TopoTestData.SpecMax then '过大' when TopoTestData.ItemValue < TopoTestData.SpecMin then '过小' else '通过' end) as 结果 FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID])) AND [TopoLogRecord].[CtrlType]=2 ";
     
            for (int i = 0; i < typeList.Count; i++)
            {
                if (i == 0)
                {
                    type = "GlobalProductionType.[ItemName] ='" + typeList[0].ToString() + "'";
                }
                else
                {
                    type += " or GlobalProductionType.[ItemName] ='" + typeList[i].ToString() + "'";
                }
            }

            for (int j = 0; j < PNList.Count; j++)
            {
                if (j == 0)
                {
                    PN = "GlobalProductionName.[PN] ='" + PNList[0].ToString() + "'";
                }
                else
                {
                    PN += " or GlobalProductionName.[PN] ='" + PNList[j].ToString() + "'";
                }
            }

            selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                   "AND (" + type + ") AND (" + PN + ") AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%'";

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
            //selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp] as 温度,TopoLogRecord.[Voltage] as 电压,TopoLogRecord.[Channel] as 通道,TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime] as 开始时间,(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as 结果,TopoRunRecordTable.[EndTime] as 结束时间,";
            //selectcmd_Second = "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            selectcmd_First = "select TopoRunRecordTable.[SN],TopoLogRecord.[Channel] as 通道,";
            selectcmd_Second = "TopoTestData.ItemName as 失效项,(case when TopoTestData.ItemValue > TopoTestData.SpecMax then '过大' when TopoTestData.ItemValue < TopoTestData.SpecMin then '过小' else '通过' end) as 结果 FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
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

        selectcmd += " order by 失效项,SN,通道";
        mysql.OpenDatabase(true);

        dtform1 = mysql.GetDataTable(selectcmd, "TopoTestData");
        if (dtform1.Rows.Count != 0)
        {
            DataTable dtCount = new DataTable();
            DataTable dtPercent = new DataTable();

            ItemCountList.Clear();

            dtCount.Columns.Add("Item");
            dtCount.Columns.Add("Count");
            dtPercent.Columns.Add("Item");
            dtPercent.Columns.Add("Percent");

            int totalData = dtform1.Rows.Count;
            int failData = 0;

            for (int i = 0; i < dtform1.Rows.Count; i++)
            {
                if (dtform1.Rows[i]["结果"].ToString() != "通过")
                {
                    if (dtform1.Rows[i]["结果"].ToString() == "过小")
                    {
                        if (FindFileName(ItemCountList, dtform1.Rows[i]["失效项"].ToString() + "过小"))
                        {
                            ItemCountList[dtform1.Rows[i]["失效项"].ToString() + "过小"] = ItemCountList[dtform1.Rows[i]["失效项"].ToString() + "过小"] + 1;
                        }
                        else
                        {
                            ItemCountList.Add(dtform1.Rows[i]["失效项"].ToString() + "过小", 1);
                        }
                    }
                    else if (dtform1.Rows[i]["结果"].ToString() == "过大")
                    {
                        if (FindFileName(ItemCountList, dtform1.Rows[i]["失效项"].ToString() + "过大"))
                        {
                            ItemCountList[dtform1.Rows[i]["失效项"].ToString() + "过大"] = ItemCountList[dtform1.Rows[i]["失效项"].ToString() + "过大"] + 1;
                        }
                        else
                        {
                            ItemCountList.Add(dtform1.Rows[i]["失效项"].ToString() + "过大", 1);
                        }
                    }

                    failData++;
                }             
            }

            var dicSort = from objDic in ItemCountList orderby objDic.Value descending select objDic;   //Dictionary 按value排序
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                DataRow dr = dtCount.NewRow();
                dr["Item"] = kvp.Key;
                dr["Count"] = kvp.Value;
                dtCount.Rows.Add(dr);
            }

            Chart1.Series[0].YValueMembers = "数量";//Y轴数据成员列
            Chart1.Series[1].YValueMembers = "百分比";//Y轴数据成员列

            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);  //网格线颜色 浅灰
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
            Chart1.Series[0].IsValueShownAsLabel = true;   //在柱形上显示数值  
            Chart1.Series[0].YAxisType = AxisType.Primary;           //设为主Y轴
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "通道数量";   //左边纵坐标名  
            Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;

            string totalItemNum = dtform1.Rows.Count.ToString();

            System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
            Title title = new Title("测试失效项（通道数目）统计图", Docking.Top, F, Color.Black);
            Chart1.Titles.Add(title);
            title.Alignment = ContentAlignment.TopCenter;

            System.Drawing.Font F1 = new Font("微软雅黑", 11);
            Title title1 = new Title("总测试项：" + totalItemNum + "   (失效项：" + failData + ")", Docking.Top, F1, Color.Black);
            Chart1.Titles.Add(title1);
            title1.Alignment = ContentAlignment.TopRight;

            if (0 < ItemCountList.Count && ItemCountList.Count < 20)
            {
                Chart1.Series[0]["PixelPointWidth"] = "30";
            }

            for (int i = 0; i < ItemCountList.Count; i++)
            {
                Chart1.Series[0].Points.AddXY(dtCount.Rows[i]["Item"], dtCount.Rows[i]["Count"]);    
            }

            Chart1.ChartAreas["ChartArea1"].AxisY2.Title = "百分比(%)";   //右边纵坐标名
            Chart1.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
            Chart1.ChartAreas["ChartArea1"].AxisY2.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
            Chart1.ChartAreas["ChartArea1"].AxisY2.Maximum = 100;
            Chart1.ChartAreas["ChartArea1"].AxisY2.Minimum = 0; 
            Chart1.Series[1].Color = Color.Red;
            Chart1.Series[1].LabelForeColor = Color.Red;
            Chart1.Series[1].IsValueShownAsLabel = true;
            Chart1.Series[1].YAxisType = AxisType.Secondary;           //设为副Y轴

            double accumPercent = 0;
            for (int i = 0; i < ItemCountList.Count; i++)
            {
                accumPercent = accumPercent + Convert.ToDouble(dtCount.Rows[i]["Count"]) / failData * 100;

                double showPercent = Math.Round(accumPercent, 1);

                Chart1.Series[1].Points.AddXY(dtCount.Rows[i]["Item"], showPercent);
            }

            Chart1.Series[1].ChartType = SeriesChartType.Line;          
           
            Chart1.Visible = true;

            if (ItemCountList.Count > 0)
            {
                DataRow[] drs = dtform1.Select("结果='过大' or 结果='过小'");

                DataTable dtfail = dtform1.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    dtfail.Rows.Add(drs[i].ItemArray);
                }
                DataView viewfail = dtfail.DefaultView;
                DataTable dtSNfail = viewfail.ToTable(true, "SN");
                string totalSNfailnum = dtSNfail.Rows.Count.ToString();

                Hashtable ht = new Hashtable();
                DataTable dt = dtform1.Clone();
                dt.Columns[1].ColumnName = "模块数"; 
                int Index = 0;
                string lastSN = "";

                for (int i = 0; i < drs.Length; i++)
                {
                    drs[i]["通道"] = 0;
                    if (ht.ContainsKey(drs[i]["失效项"].ToString() + drs[i]["结果"].ToString()))
                    {
                        int index = (int)ht[drs[i]["失效项"].ToString() + drs[i]["结果"].ToString()];
                        if (drs[i]["SN"].ToString() != lastSN)
                        {
                            dt.Rows[index]["SN"] = dt.Rows[index]["SN"].ToString() + "；" + drs[i]["SN"].ToString();
                            dt.Rows[index]["模块数"] = Convert.ToInt32(dt.Rows[index]["模块数"]) + 1;
                            lastSN = drs[i]["SN"].ToString();
                        }                      
                    }
                    else
                    {
                        ht.Add(drs[i]["失效项"].ToString() + drs[i]["结果"].ToString(), Index);
                        lastSN = drs[i]["SN"].ToString();
                        drs[i]["通道"] = Convert.ToInt32(drs[i]["通道"]) + 1;
                        Index++;
                        DataRow dr = dt.NewRow();
                        dt.Rows.Add(drs[i].ItemArray);
                    }
                }

                dt.DefaultView.Sort = "模块数 desc,失效项";
                dt.AcceptChanges();

                Chart2.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);  //网格线颜色 浅灰
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                Chart2.Series[0].IsValueShownAsLabel = true;   //在柱形上显示数值  
                Chart2.Series[0].YAxisType = AxisType.Primary;           //设为主Y轴
                Chart2.ChartAreas["ChartArea1"].AxisY.Title = "模块数量";   //左边纵坐标名  
                Chart2.ChartAreas["ChartArea1"].AxisY.Minimum = 0;

                DataView view = dtform1.DefaultView;
                DataTable dtSN = view.ToTable(true, "SN");
                string totalSNnum = dtSN.Rows.Count.ToString();             

                title = new Title("测试失效项（模块数目）统计图", Docking.Top, F, Color.Black);
                Chart2.Titles.Add(title);
                title.Alignment = ContentAlignment.TopCenter;

                title1 = new Title("总测试模块：" + totalSNnum + "   (失效模块：" + totalSNfailnum + ")", Docking.Top, F1, Color.Black);
                Chart2.Titles.Add(title1);
                title1.Alignment = ContentAlignment.TopRight;

                if (0 < dt.Rows.Count && dt.Rows.Count < 20)
                {
                    Chart2.Series[0]["PixelPointWidth"] = "30";
                }

                DataView dtView = dt.DefaultView;
                dtView.Sort = "模块数 desc,失效项";
                DataTable Dt = dtView.ToTable();
                int columnIndex = 0;

                for (int i = 0; i < Dt.Rows.Count; i++)
                {                  
                    Chart2.Series[0].Points.AddXY(Dt.Rows[i]["失效项"].ToString() + Dt.Rows[i]["结果"].ToString(), Dt.Rows[i]["模块数"]);
                    Chart2.Series[0].Points[columnIndex].ToolTip = Dt.Rows[i]["SN"].ToString();
                    columnIndex++;

                }

                Chart2.Visible = true;
            }                   
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('没有数据！  注：如在未选择类型的情况下，使用SN进行查询，请输入完整的SN。');", true);
        }
    }

    public bool FindFileName(Dictionary<string, int> inputStructArray, string filename)
    {
        foreach (KeyValuePair<string, int> kvp in inputStructArray)
        {
            if (kvp.Key.ToString().ToUpper() == filename.ToUpper().Trim())
            {
                return true;
            }           
        }

        return false;
    }

    public void dataNormalDistribution()
    {
        string selectcmd = "";
        string selectcmd_First = "";
        string selectcmd_Second = "";
        string selectcmd_Third = "";

        string type = "GlobalProductionType.[ItemName] =''";
        string PN = "GlobalProductionName.[PN] =''";

        if (DropDownList1.Text == "" & TextBox1.Text != "")      //输入SN查询
        {
            string sql = "select distinct GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoRunRecordTable.SN "
                       + "from GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoRunRecordTable "
                       + "where GlobalProductionType.ID=GlobalProductionName.PID and GlobalProductionName.ID=TopoTestPlan.PID and TopoTestPlan.ID=TopoRunRecordTable.PID and TopoRunRecordTable.SN ='" + TextBox1.Text.Trim() + "'";

            mysql.OpenDatabase(true);
            DataTable table = mysql.GetDataTable(sql, "");

            ArrayList typeList = new ArrayList();
            ArrayList PNList = new ArrayList();

            if (table.Rows.Count != 0)
            {
                typeList.Add(table.Rows[0]["类型"].ToString());
                PNList.Add(table.Rows[0]["品名"].ToString());

                for (int cnt = 1; cnt < table.Rows.Count; cnt++)
                {
                    for (int typeCnt = 0; typeCnt < typeList.Count; typeCnt++)
                    {
                        if (table.Rows[cnt]["类型"].ToString() != typeList[typeCnt].ToString())
                        {
                            typeList.Add(table.Rows[cnt]["类型"].ToString());
                        }
                    }

                    for (int PNCnt = 0; PNCnt < PNList.Count; PNCnt++)
                    {
                        if (table.Rows[cnt]["品名"].ToString() != PNList[PNCnt].ToString())
                        {
                            PNList.Add(table.Rows[cnt]["品名"].ToString());
                        }
                    }
                }
            }

            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            selectcmd = "";
            selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,TopoTestPlan.[ID] as PlanID,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp] as 温度,TopoLogRecord.[Voltage] as 电压,TopoLogRecord.[Channel] as 通道,TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime] as 开始时间,(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as 结果,TopoRunRecordTable.[EndTime] as 结束时间,";
            selectcmd_Second = "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID])) AND [TopoLogRecord].[CtrlType]=2 ";

            for (int i = 0; i < typeList.Count; i++)
            {
                if (i == 0)
                {
                    type = "GlobalProductionType.[ItemName] ='" + typeList[0].ToString() + "'";
                }
                else
                {
                    type += " or GlobalProductionType.[ItemName] ='" + typeList[i].ToString() + "'";
                }
            }

            for (int j = 0; j < PNList.Count; j++)
            {
                if (j == 0)
                {
                    PN = "GlobalProductionName.[PN] ='" + PNList[0].ToString() + "'";
                }
                else
                {
                    PN += " or GlobalProductionName.[PN] ='" + PNList[j].ToString() + "'";
                }
            }

            selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                   "AND (" + type + ") AND (" + PN + ") AND TopoRunRecordTable.[SN] like '" + TextBox1.Text.Trim() + "%'";

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
            selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,TopoTestPlan.[ID] as PlanID,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp] as 温度,TopoLogRecord.[Voltage] as 电压,TopoLogRecord.[Channel] as 通道,TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime] as 开始时间,(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as 结果,TopoRunRecordTable.[EndTime] as 结束时间,";
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

        selectcmd += " and UPPER(TopoTestData.[ItemName])='" + DropDownList6.SelectedItem.Text.ToUpper() + "' order by TopoRunRecordTable.[StartTime] DESC, TopoTestData.PID";
      
        mysql.OpenDatabase(true);

        dtform1 = mysql.GetDataTable(selectcmd, "TopoTestData");
        if (dtform1.Rows.Count != 0)
        {
            List<double> scores = new List<double>();
            for (int i = 0; i < dtform1.Rows.Count; i++)
            {
                string ModelItem = "";
                if (DropDownList6.SelectedItem.Text.ToUpper().Contains("SMSR") || DropDownList6.SelectedItem.Text.ToUpper().Contains("WAVELENGTH") || DropDownList6.SelectedItem.Text.ToUpper().Contains("SPANWIDE") || DropDownList6.SelectedItem.Text.ToUpper().Contains("OSNR"))
                {
                    ModelItem = "Wavelength";                 
                }
                else if (DropDownList6.SelectedItem.Text.ToUpper().Contains("CSEN"))
                {
                    ModelItem = "Csen";                  
                }
                else if (DropDownList6.SelectedItem.Text.ToUpper().Contains("OVERLOAD"))
                {
                    ModelItem = "Overload";
                }
                else if (DropDownList6.SelectedItem.Text.ToUpper().Contains("CROSSING"))
                {
                    ModelItem = "TxEye";
                }
                else if (DropDownList6.SelectedItem.Text.ToUpper().Contains("DMIVCC("))
                {
                    ModelItem = "DmiVcc";
                } 

                switch (ModelItem)
                {
                    case "Wavelength":
                        if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) != -1)
                        {
                            scores.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                        }
                        break;
                    case "Csen":
                        if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -1000 || ( Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < -1005 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -5000))
                        {
                            scores.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                        }
                        break;
                    case "Overload":
                        if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -1000 )
                        {
                            scores.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                        }
                        break;
                    case "TxEye":
                        if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) !=0 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9E+9)
                        {
                            scores.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                        }
                        break;
                    //case "DmiVcc":
                    //    if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) != 0 )
                    //    {
                    //        scores.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                    //    }
                    //    break;
                    default :
                        if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9E+9 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -5000)
                        {
                            scores.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                        }
                        break;
                }                                   
            }
           
            if (scores.Distinct().Count() == 1)  //此时不需要统计 因为每个样本数据都相同
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('方差为0，所有样本值相同，不在进行统计分析');", true);
            }
            else
            {
                StandardDistribution mathX = new StandardDistribution(scores);
                Bitmap bitmap = mathX.GetGaussianDistributionGraph(520, 300);       //规格项测试结果分布图
                bitmap.Save(Server.MapPath("stu2.gif"), System.Drawing.Imaging.ImageFormat.Gif);
                Image1.ImageUrl = "stu2.gif";
                Image1.Visible = true;

                DataView dataView = dtform1.DefaultView;
                DataTable dataTableDistinct = dataView.ToTable(true, "PlanID");

                string PlanID = "";
                for (int i = 0; i < dataTableDistinct.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        PlanID = dataTableDistinct.Rows[i]["PlanID"].ToString();
                    }
                    else
                    {
                        PlanID = PlanID + "," + dataTableDistinct.Rows[i]["PlanID"].ToString();
                    }
                }
                string SID = DropDownList6.SelectedItem.Value;

                string ss = "select * from TopoPNSpecsParams where SID=" + SID + " and PID IN (" + PlanID + ")";
                DataTable dtSpec = mysql.GetDataTable(ss, "TopoPNSpecsParams");

                DataView SpecDataView = dtSpec.DefaultView;
                DataTable SpecDistinct = SpecDataView.ToTable(true, "SpecMin", "SpecMax");

                ArrayList specMinList = new ArrayList();
                ArrayList specMaxList = new ArrayList();
                ArrayList CPKList = new ArrayList();

                for (int i = 0; i < SpecDistinct.Rows.Count; i++)
                {
                    double CPK = mathX.GetCPK(Convert.ToDouble(SpecDistinct.Rows[i]["SpecMax"]), Convert.ToDouble(SpecDistinct.Rows[i]["SpecMin"]));
                    CPKList.Add(CPK);
                    specMinList.Add(SpecDistinct.Rows[i]["SpecMin"]);
                    specMaxList.Add(SpecDistinct.Rows[i]["SpecMax"]);
                }

                HtmlTableRow tr0 = new HtmlTableRow();
                HtmlTableCell cell01 = new HtmlTableCell();
                cell01.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                cell01.InnerText = "统计测试项";
                tr0.Cells.Add(cell01);
                HtmlTableCell cell02 = new HtmlTableCell();
                cell02.Attributes.Add("style", "width:230px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                cell02.Attributes.Add("colspan", "5");
                cell02.InnerText = DropDownList6.SelectedItem.Text;
                tr0.Cells.Add(cell02);
                CPKtable.Rows.Add(tr0);

                HtmlTableRow tr1 = new HtmlTableRow();
                HtmlTableCell cell11 = new HtmlTableCell();
                cell11.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                cell11.InnerText = "总样本数";
                tr1.Cells.Add(cell11);
                HtmlTableCell cell12 = new HtmlTableCell();
                cell12.Attributes.Add("style", "width:65px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                cell12.InnerText = dtform1.Rows.Count.ToString();
                //cell12.InnerText = scores.Count.ToString();
                tr1.Cells.Add(cell12);
                HtmlTableCell cell13 = new HtmlTableCell();
                cell13.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                cell13.InnerText = "无效样本数";
                tr1.Cells.Add(cell13);
                HtmlTableCell cell14 = new HtmlTableCell();
                cell14.Attributes.Add("style", "width:65px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                cell14.InnerText = (dtform1.Rows.Count - scores.Count).ToString();
                tr1.Cells.Add(cell14);
                HtmlTableCell cell15 = new HtmlTableCell();
                cell15.Attributes.Add("style", "width:165px;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;");
                cell15.Attributes.Add("colspan", "2");
                tr1.Cells.Add(cell15);
                CPKtable.Rows.Add(tr1);

                HtmlTableRow tr2 = new HtmlTableRow();
                HtmlTableCell cell21 = new HtmlTableCell();
                cell21.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                cell21.InnerText = "最小值";
                tr2.Cells.Add(cell21);
                HtmlTableCell cell22 = new HtmlTableCell();
                cell22.Attributes.Add("style", "width:65px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                cell22.InnerText = scores.Min().ToString();
                tr2.Cells.Add(cell22);
                HtmlTableCell cell23 = new HtmlTableCell();
                cell23.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                cell23.InnerText = "最大值";
                tr2.Cells.Add(cell23);
                HtmlTableCell cell24 = new HtmlTableCell();
                cell24.Attributes.Add("style", "width:65px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                cell24.InnerText = scores.Max().ToString();
                tr2.Cells.Add(cell24);
                HtmlTableCell cell25 = new HtmlTableCell();
                cell25.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                cell25.InnerText = "平均值";
                tr2.Cells.Add(cell25);
                HtmlTableCell cell26 = new HtmlTableCell();
                cell26.Attributes.Add("style", "width:65px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                cell26.InnerText = Math.Round(mathX.Average, 3).ToString();
                tr2.Cells.Add(cell26);
                CPKtable.Rows.Add(tr2);

                HtmlTableRow tr3 = new HtmlTableRow();
                HtmlTableCell cell31 = new HtmlTableCell();
                cell31.Attributes.Add("style", "width:495px;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;");
                cell31.Attributes.Add("colspan", "6");
                tr3.Cells.Add(cell31);
                CPKtable.Rows.Add(tr3);

                for (int i = 0; i < CPKList.Count; i++)
                {
                    HtmlTableRow tr1CPK = new HtmlTableRow();
                    HtmlTableCell cell11CPK = new HtmlTableCell();
                    cell11CPK.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                    + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                    cell11CPK.InnerText = "规格下限";
                    tr1CPK.Cells.Add(cell11CPK);
                    HtmlTableCell cell12CPK = new HtmlTableCell();
                    cell12CPK.Attributes.Add("style", "width:65px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                    + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                    cell12CPK.InnerText = specMinList[i].ToString();
                    tr1CPK.Cells.Add(cell12CPK);
                    HtmlTableCell cell13CPK = new HtmlTableCell();
                    cell13CPK.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                    + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                    cell13CPK.InnerText = "规格上限";
                    tr1CPK.Cells.Add(cell13CPK);
                    HtmlTableCell cell14CPK = new HtmlTableCell();
                    cell14CPK.Attributes.Add("style", "width:65px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                    + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                    cell14CPK.InnerText = specMaxList[i].ToString();
                    tr1CPK.Cells.Add(cell14CPK);
                    HtmlTableCell cell15CPK = new HtmlTableCell();
                    cell15CPK.Attributes.Add("style", "width:165px;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;");
                    cell15CPK.Attributes.Add("colspan", "2");
                    tr1CPK.Cells.Add(cell15CPK);
                    CPKtable.Rows.Add(tr1CPK);

                    HtmlTableRow tr2CPK = new HtmlTableRow();
                    HtmlTableCell cell21CPK = new HtmlTableCell();
                    cell21CPK.Attributes.Add("style", "width:100px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                    + "font-weight:600;font-size:15px;text-align:center;color: #365E9E;border:solid #ABB9C9 1px;background:#F2F0E3;height:27px;");
                    cell21CPK.InnerText = "CPK";
                    tr2CPK.Cells.Add(cell21CPK);
                    HtmlTableCell cell22CPK = new HtmlTableCell();
                    cell22CPK.Attributes.Add("style", "width:395px;font-family:'Times New Roman Negreta', 'Times New Roman';"
                    + "font-size:14px;color: #365E9E;border:solid #ABB9C9 1px;background:#FFFFFF;height:27px;padding-left: 5px;");
                    cell22CPK.Attributes.Add("colspan", "5");
                    cell22CPK.InnerText = Math.Round(Convert.ToDouble(CPKList[i]), 3).ToString();
                    tr2CPK.Cells.Add(cell22CPK);
                    CPKtable.Rows.Add(tr2CPK);
                }
            }           
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('没有数据！  注：如在未选择类型的情况下，使用SN进行查询，请输入完整的SN。');", true);
        }
    }

    public void dataChannelChart()
    {
        string selectcmd = "";
        string selectcmd_First = "";
        string selectcmd_Second = "";
        string selectcmd_Third = "";
        string ss_Fourth = "";

        string type = "GlobalProductionType.[ItemName] =''";
        string PN = "GlobalProductionName.[PN] =''";

        if (DropDownList1.Text == "" & TextBox1.Text != "")      //输入SN查询
        {
            string sql = "select distinct GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoRunRecordTable.SN "
                       + "from GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoRunRecordTable "
                       + "where GlobalProductionType.ID=GlobalProductionName.PID and GlobalProductionName.ID=TopoTestPlan.PID and TopoTestPlan.ID=TopoRunRecordTable.PID and TopoRunRecordTable.SN ='" + TextBox1.Text.Trim() + "'";

            mysql.OpenDatabase(true);
            DataTable table = mysql.GetDataTable(sql, "");

            ArrayList typeList = new ArrayList();
            ArrayList PNList = new ArrayList();

            if (table.Rows.Count != 0)
            {
                typeList.Add(table.Rows[0]["类型"].ToString());
                PNList.Add(table.Rows[0]["品名"].ToString());

                for (int cnt = 1; cnt < table.Rows.Count; cnt++)
                {
                    for (int typeCnt = 0; typeCnt < typeList.Count; typeCnt++)
                    {
                        if (table.Rows[cnt]["类型"].ToString() != typeList[typeCnt].ToString())
                        {
                            typeList.Add(table.Rows[cnt]["类型"].ToString());
                        }
                    }

                    for (int PNCnt = 0; PNCnt < PNList.Count; PNCnt++)
                    {
                        if (table.Rows[cnt]["品名"].ToString() != PNList[PNCnt].ToString())
                        {
                            PNList.Add(table.Rows[cnt]["品名"].ToString());
                        }
                    }
                }
            }

            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            selectcmd = "";
            selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,A.[Temp] as 温度,A.[Voltage] as 电压,A.[Channel] as 通道,A.StartTime,C.[SN],TopoTestData.ItemName,TopoTestData.ItemValue ";
            selectcmd_Second = "FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord  A,TopoTestData,TopoRunRecordTable C ";
            selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((C.PID)=[TopoTestPlan].[ID]) AND((A.RunRecordID)=C.[ID])  AND ((TopoTestData.PID)=A.[ID])) AND A.[CtrlType]=2 ";

            for (int i = 0; i < typeList.Count; i++)
            {
                if (i == 0)
                {
                    type = "GlobalProductionType.[ItemName] ='" + typeList[0].ToString() + "'";
                }
                else
                {
                    type += " or GlobalProductionType.[ItemName] ='" + typeList[i].ToString() + "'";
                }
            }

            for (int j = 0; j < PNList.Count; j++)
            {
                if (j == 0)
                {
                    PN = "GlobalProductionName.[PN] ='" + PNList[0].ToString() + "'";
                }
                else
                {
                    PN += " or GlobalProductionName.[PN] ='" + PNList[j].ToString() + "'";
                }
            }

            selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                   "AND (" + type + ") AND (" + PN + ") AND C.[SN] like '" + TextBox1.Text.Trim() + "%'";
            ss_Fourth = "AND (" + type + ") AND (" + PN + ") AND D.[SN] like '" + TextBox1.Text.Trim() + "%'";

            if ((TextBox5.Text == "") && (TextBox6.Text == ""))
            {
                selectcmd += "";
            }
            else if ((TextBox5.Text == "") && (TextBox6.Text != ""))
            {
                selectcmd += " AND C.[StartTime] <='" + TextBox6.Text + "'";
                ss_Fourth += " AND D.[StartTime] <='" + TextBox6.Text + "'";
            }
            else if ((TextBox5.Text != "") && (TextBox6.Text == ""))
            {
                selectcmd += " AND C.[StartTime] >='" + TextBox5.Text + "'";
                ss_Fourth += " AND D.[StartTime] >='" + TextBox5.Text + "'";
            }
            else if ((TextBox5.Text != "") && (TextBox6.Text != ""))
            {
                selectcmd += " AND C.[StartTime] >='" + TextBox5.Text + "' AND C.[StartTime] <='" + TextBox6.Text + "'";
                ss_Fourth += " AND D.[StartTime] >='" + TextBox5.Text + "' AND D.[StartTime] <='" + TextBox6.Text + "'";
            }
        }
        else
        {
            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            selectcmd = "";
            selectcmd_First = "select GlobalProductionType.[ItemName] as 类型,GlobalProductionName.[PN] as 品名,TopoTestPlan.[ItemName] as 测试方案,A.[Temp] as 温度,A.[Voltage] as 电压,A.[Channel] as 通道,A.StartTime,C.[SN],TopoTestData.ItemName,TopoTestData.ItemValue ";
            selectcmd_Second = "FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord A,TopoTestData,TopoRunRecordTable C ";
            selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((C.PID)=[TopoTestPlan].[ID]) AND((A.RunRecordID)=[C].[ID])  AND ((TopoTestData.PID)=A.[ID])) AND A.[CtrlType]=2 ";
         
            if ((DropDownList3.Text == "") && (TextBox1.Text == "") && (TextBox5.Text == "") && (TextBox6.Text == ""))
            {
                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                   "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'";
                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'";

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
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND C.[StartTime]>='" + TextBox5.Text + "' AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND D.[StartTime]>='" + TextBox5.Text + "' AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND C.[StartTime]>='" + TextBox5.Text + "' ";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND D.[StartTime]>='" + TextBox5.Text + "' ";

                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "'";

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
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND C.[StartTime]>='" + TextBox5.Text + "' AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND D.[StartTime]>='" + TextBox5.Text + "' AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND C.[StartTime]>='" + TextBox5.Text + "' ";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND D.[StartTime]>='" + TextBox5.Text + "' ";

                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND TopoTestPlan.[ItemName] ='" + DropDownList3.SelectedItem + "'";

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
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND C.[StartTime]>='" + TextBox5.Text + "' AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND D.[StartTime]>='" + TextBox5.Text + "' AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND C.[StartTime]>='" + TextBox5.Text + "' ";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND D.[StartTime]>='" + TextBox5.Text + "' ";

                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "' AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND C.[SN] like '" + TextBox1.Text.Trim() + "%" + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND D.[SN] like '" + TextBox1.Text.Trim() + "%" + "'";

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
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND C.[StartTime]>='" + TextBox5.Text + "' AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND D.[StartTime]>='" + TextBox5.Text + "' AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND C.[StartTime]>='" + TextBox5.Text + "' ";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND D.[StartTime]>='" + TextBox5.Text + "' ";

                            }

                        }
                        else
                        {
                            if (TextBox6.Text != "")
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND C.[StartTime] <='" + TextBox6.Text + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'AND D.[StartTime] <='" + TextBox6.Text + "'";
                            }
                            else
                            {
                                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'";
                                ss_Fourth = "AND GlobalProductionType.[ItemName] ='" + DropDownList1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + DropDownList2.SelectedItem + "'";
                            }
                        }

                    }

                }

            }
        }

        selectcmd += " and UPPER(TopoTestData.[ItemName])='" + DropDownList6.SelectedItem.Text.ToUpper() + "' and TopoTestPlan.ItemName not like 'DVT%' and A.StartTime IN(SELECT  MAX(B.StartTime) " +
                     " FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord B,TopoTestData,TopoRunRecordTable D " +
                     " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((D.PID)=[TopoTestPlan].[ID]) AND((B.RunRecordID)=[D].[ID])  AND ((TopoTestData.PID)=B.[ID])) AND B.[CtrlType]=2 " +
                      ss_Fourth + " and UPPER(TopoTestData.[ItemName])='" + DropDownList6.SelectedItem.Text.ToUpper() + "' and TopoTestPlan.ItemName not like 'DVT%' AND A.Temp =B.Temp AND A.Channel =B.Channel AND C.SN =D.SN ) order by 温度,SN";
        //筛选满足条件的各模块最近一次的数据(不包含DVT数据)

        mysql.OpenDatabase(true);

        dtform1 = mysql.GetDataTable(selectcmd, "TopoTestData");

        if (dtform1.Rows.Count != 0)
        {
            DataView view = dtform1.DefaultView;
            DataTable SNdt = view.ToTable(true, "SN");
            DataTable TEMPdt = view.ToTable(true, "温度");

            if (SNdt.Rows.Count <= 10)    //模块个数小于10，统计各温度折线图
            {
                string currentTemp = TEMPdt.Rows[0]["温度"].ToString();
                string currentSN = SNdt.Rows[0]["SN"].ToString();
                int TempCnt = 0;
                int SNcnt = 0;
                int PonitCnt = 0;
                int cnt = 0;

                for (int i = 0; i < dtform1.Rows.Count; i++)
                {
                    if (dtform1.Rows[i]["SN"].ToString() != currentSN)
                    {
                        SNcnt++;
                        PonitCnt = 0;
                        currentSN = dtform1.Rows[i]["SN"].ToString();
                    }
                    if (dtform1.Rows[i]["温度"].ToString() != currentTemp)
                    {
                        TempCnt++;
                        SNcnt = 0;
                        PonitCnt = 0;
                        cnt = 0;
                        currentTemp = dtform1.Rows[i]["温度"].ToString();
                    }

                    switch (TempCnt)    //一个温度对应一个折线图，一个模块对应一条折线
                    {
                        case 0:
                            {
                                if (cnt == 0)
                                {
                                    LineChartTemp1.Visible = true;
                                    LineChartTemp1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224); 
                                    LineChartTemp1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                                    LineChartTemp1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    LineChartTemp1.ChartAreas["ChartArea1"].AxisY.Title = "Value";   //左边纵坐标名
                                    LineChartTemp1.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
                                    LineChartTemp1.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false; 

                                    System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
                                    Title title = new Title(DropDownList6.SelectedItem.Text + "@" + currentTemp + "℃", Docking.Top, F, Color.Black);
                                    LineChartTemp1.Titles.Add(title);
                                    title.Alignment = ContentAlignment.TopCenter;

                                    cnt++;
                                }

                                LineChartTemp1.Series[SNcnt].IsVisibleInLegend = true;
                                LineChartTemp1.Series[SNcnt].LegendText = currentSN;

                                if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9e+9 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -9e+9)
                                {
                                    LineChartTemp1.Series[SNcnt].Points.AddXY("CH" + dtform1.Rows[i]["通道"].ToString(), dtform1.Rows[i]["ItemValue"].ToString());
                                }
                                else
                                {
                                    LineChartTemp1.Series[SNcnt].Points.AddXY("CH" + dtform1.Rows[i]["通道"].ToString(), 9999);
                                }

                                LineChartTemp1.Series[SNcnt].Points[PonitCnt].ToolTip = dtform1.Rows[i]["ItemValue"].ToString();
                                PonitCnt++;
                               
                            }
                            break;
                        case 1:
                            {
                                if (cnt == 0)
                                {
                                    LineChartTemp2.Visible = true;
                                    LineChartTemp2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224); 
                                    LineChartTemp2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                                    LineChartTemp2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    LineChartTemp2.ChartAreas["ChartArea1"].AxisY.Title = "Value";   //左边纵坐标名
                                    LineChartTemp2.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
                                    LineChartTemp2.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false; 

                                    System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
                                    Title title = new Title(DropDownList6.SelectedItem.Text + "@" + currentTemp + "℃", Docking.Top, F, Color.Black);
                                    LineChartTemp2.Titles.Add(title);
                                    title.Alignment = ContentAlignment.TopCenter;

                                    //System.Drawing.Font F1 = new Font("微软雅黑", 10);
                                    //Title title1 = new Title("测试项：" + DropDownList6.SelectedItem.Text + "   温度：" + currentTemp, Docking.Top, F1, Color.Black);
                                    //LineChartTemp2.Titles.Add(title1);
                                    //title1.Alignment = ContentAlignment.TopRight;

                                    cnt++;
                                }

                                LineChartTemp2.Series[SNcnt].IsVisibleInLegend = true;
                                LineChartTemp2.Series[SNcnt].LegendText = currentSN;

                                if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9e+9 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -9e+9)
                                {
                                    LineChartTemp2.Series[SNcnt].Points.AddXY("CH" + dtform1.Rows[i]["通道"].ToString(), dtform1.Rows[i]["ItemValue"].ToString());
                                }
                                else
                                {
                                    LineChartTemp2.Series[SNcnt].Points.AddXY("CH" + dtform1.Rows[i]["通道"].ToString(), 9999);
                                }

                                LineChartTemp2.Series[SNcnt].Points[PonitCnt].ToolTip = dtform1.Rows[i]["ItemValue"].ToString();
                                PonitCnt++;
                            }
                            break;
                        case 2:
                            {
                                if (cnt == 0)
                                {
                                    LineChartTemp3.Visible = true;
                                    LineChartTemp3.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224); 
                                    LineChartTemp3.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                                    LineChartTemp3.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    LineChartTemp3.ChartAreas["ChartArea1"].AxisY.Title = "Value";   //左边纵坐标名
                                    LineChartTemp3.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
                                    LineChartTemp3.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false; 

                                    System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
                                    Title title = new Title(DropDownList6.SelectedItem.Text + "@" + currentTemp + "℃", Docking.Top, F, Color.Black);
                                    LineChartTemp3.Titles.Add(title);
                                    title.Alignment = ContentAlignment.TopCenter;

                                    cnt++;
                                }

                                LineChartTemp3.Series[SNcnt].IsVisibleInLegend = true;
                                LineChartTemp3.Series[SNcnt].LegendText = currentSN;
                                if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9e+9 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -9e+9)
                                {
                                    LineChartTemp3.Series[SNcnt].Points.AddXY("CH" + dtform1.Rows[i]["通道"].ToString(), dtform1.Rows[i]["ItemValue"].ToString());
                                }
                                else
                                {
                                    LineChartTemp3.Series[SNcnt].Points.AddXY("CH" + dtform1.Rows[i]["通道"].ToString(), 9999);
                                }

                                LineChartTemp3.Series[SNcnt].Points[PonitCnt].ToolTip = dtform1.Rows[i]["ItemValue"].ToString();
                                PonitCnt++;
                            }
                            break;
                        default :
                            break;
                    }                   
                }
            }
            else                          //模块个数大于10，统计各温度箱线图
            {
                ArrayList Temp1CH1Ponit = new ArrayList();
                ArrayList Temp1CH2Ponit = new ArrayList();
                ArrayList Temp1CH3Ponit = new ArrayList();
                ArrayList Temp1CH4Ponit = new ArrayList();
                ArrayList Temp2CH1Ponit = new ArrayList();
                ArrayList Temp2CH2Ponit = new ArrayList();
                ArrayList Temp2CH3Ponit = new ArrayList();
                ArrayList Temp2CH4Ponit = new ArrayList();
                ArrayList Temp3CH1Ponit = new ArrayList();
                ArrayList Temp3CH2Ponit = new ArrayList();
                ArrayList Temp3CH3Ponit = new ArrayList();
                ArrayList Temp3CH4Ponit = new ArrayList();
                ArrayList TempAllCH1Ponit = new ArrayList();
                ArrayList TempAllCH2Ponit = new ArrayList();
                ArrayList TempAllCH3Ponit = new ArrayList();
                ArrayList TempAllCH4Ponit = new ArrayList();

                string currentTemp = TEMPdt.Rows[0]["温度"].ToString();
                int TempCnt = 0;
                int cnt = 0;

                for (int i = 0; i < dtform1.Rows.Count; i++)
                {
                    if (dtform1.Rows[i]["温度"].ToString() != currentTemp)
                    {
                        TempCnt++;
                        currentTemp = dtform1.Rows[i]["温度"].ToString();
                        cnt = 0;
                    }

                    switch (TempCnt)    //一个温度对应一个箱线图
                    {
                        case 0:
                            {
                                if (cnt == 0)
                                {
                                    BoxChartTemp1.Visible = true;
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisX.Title = "Channel";
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisY.Title = "Value";   //左边纵坐标名
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisX.TitleAlignment = StringAlignment.Center;
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                                    BoxChartTemp1.ChartAreas["ChartArea1"].AxisX.Maximum = 4.9;

                                    System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
                                    Title title = new Title(DropDownList6.SelectedItem.Text + "@" + currentTemp + "℃", Docking.Top, F, Color.Black);
                                    BoxChartTemp1.Titles.Add(title);
                                    title.Alignment = ContentAlignment.TopCenter;

                                    BoxChartTemp1.Series[1].ChartType = SeriesChartType.Point;

                                    cnt++;
                                }

                                if (dtform1.Rows[i]["通道"].ToString() == "1")
                                {
                                    Temp1CH1Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH1Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "2")
                                {
                                    Temp1CH2Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH2Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "3")
                                {
                                    Temp1CH3Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH3Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "4")
                                {
                                    Temp1CH4Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH4Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }

                                if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9e+9 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -9e+9)
                                {
                                    BoxChartTemp1.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), dtform1.Rows[i]["ItemValue"].ToString());
                                    BoxChartTempAll.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), dtform1.Rows[i]["ItemValue"].ToString());
                                }
                                else
                                {
                                    BoxChartTemp1.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), 9999);
                                    BoxChartTempAll.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), 9999);
                                }
                            }
                            break;
                        case 1:
                            {
                                if (cnt == 0)
                                {
                                    BoxChartTemp2.Visible = true;
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisX.Title = "Channel";
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisY.Title = "Value";   //左边纵坐标名
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisX.TitleAlignment = StringAlignment.Center;
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                                    BoxChartTemp2.ChartAreas["ChartArea1"].AxisX.Maximum = 4.9;

                                    System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
                                    Title title = new Title(DropDownList6.SelectedItem.Text + "@" + currentTemp + "℃", Docking.Top, F, Color.Black);
                                    BoxChartTemp2.Titles.Add(title);
                                    title.Alignment = ContentAlignment.TopCenter;

                                    //System.Drawing.Font F1 = new Font("微软雅黑", 10);
                                    //Title title1 = new Title("测试项：" + DropDownList6.SelectedItem.Text + "   温度：" + currentTemp, Docking.Top, F1, Color.Black);
                                    //BoxChartTemp2.Titles.Add(title1);
                                    //title1.Alignment = ContentAlignment.TopRight;
                                    BoxChartTemp2.Series[1].ChartType = SeriesChartType.Point; 

                                    cnt++;
                                }

                                if (dtform1.Rows[i]["通道"].ToString() == "1")
                                {
                                    Temp2CH1Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH1Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "2")
                                {
                                    Temp2CH2Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH2Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "3")
                                {
                                    Temp2CH3Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH3Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "4")
                                {
                                    Temp2CH4Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH4Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }

                                if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9e+9 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -9e+9)
                                {
                                    BoxChartTemp2.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), dtform1.Rows[i]["ItemValue"].ToString());
                                    BoxChartTempAll.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), dtform1.Rows[i]["ItemValue"].ToString());
                                }
                                else
                                {
                                    BoxChartTemp2.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), 9999);
                                    BoxChartTempAll.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), 9999);
                                }
                            }
                            break;
                        case 2:
                            {
                                if (cnt == 0)
                                {
                                    BoxChartTemp3.Visible = true;
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisX.Title = "Channel";
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisY.Title = "Value";   //左边纵坐标名
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisX.TitleAlignment = StringAlignment.Center;
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                                    BoxChartTemp3.ChartAreas["ChartArea1"].AxisX.Maximum = 4.9;

                                    System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
                                    Title title = new Title(DropDownList6.SelectedItem.Text + "@" + currentTemp + "℃", Docking.Top, F, Color.Black);
                                    BoxChartTemp3.Titles.Add(title);
                                    title.Alignment = ContentAlignment.TopCenter;

                                    BoxChartTemp3.Series[1].ChartType = SeriesChartType.Point; 

                                    cnt++;
                                }

                                if (dtform1.Rows[i]["通道"].ToString() == "1")
                                {
                                    Temp3CH1Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH1Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "2")
                                {
                                    Temp3CH2Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH2Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "3")
                                {
                                    Temp3CH3Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH3Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }
                                else if (dtform1.Rows[i]["通道"].ToString() == "4")
                                {
                                    Temp3CH4Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                    TempAllCH4Ponit.Add(Convert.ToDouble(dtform1.Rows[i]["ItemValue"]));
                                }

                                if (Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) < 9e+9 && Convert.ToDouble(dtform1.Rows[i]["ItemValue"]) > -9e+9)
                                {
                                    BoxChartTemp3.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), dtform1.Rows[i]["ItemValue"].ToString());
                                    BoxChartTempAll.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), dtform1.Rows[i]["ItemValue"].ToString());
                                }
                                else
                                {
                                    BoxChartTemp3.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), 9999);
                                    BoxChartTempAll.Series[1].Points.AddXY(Convert.ToInt32(dtform1.Rows[i]["通道"]), 9999);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                if (Temp1CH1Ponit.Count > 2)    //Temp1箱线图CH1
                {
                    double Q1T1CH1 = 0;
                    double Q2T1CH1 = 0;
                    double Q3T1CH1 = 0;
                    double A1T1CH1 = 0;    //上边缘
                    double A2T1CH1 = 0;    //下边缘

                    CalculateQuartile(Temp1CH1Ponit, out Q1T1CH1, out Q2T1CH1, out  Q3T1CH1);
                    A1T1CH1 = Q3T1CH1 + 1.5 * (Q3T1CH1 - Q1T1CH1);
                    A2T1CH1 = Q1T1CH1 - 1.5 * (Q3T1CH1 - Q1T1CH1);

                    double[] BoxPointT1CH1 = { A2T1CH1, A1T1CH1, Q1T1CH1, Q3T1CH1, Q2T1CH1, Q2T1CH1 };
                    BoxChartTemp1.Series[0].Points.Add(BoxPointT1CH1);
                    BoxChartTemp1.Series[0]["BoxPlotShowAverage"] = "false";
                    BoxChartTemp1.Series[0]["PixelPointWidth"] = "50";
                }

                if (Temp1CH2Ponit.Count > 2)      //Temp1箱线图CH2
                {
                    double Q1T1CH2 = 0;
                    double Q2T1CH2 = 0;
                    double Q3T1CH2 = 0;
                    double A1T1CH2 = 0;    //上边缘
                    double A2T1CH2 = 0;    //下边缘

                    CalculateQuartile(Temp1CH2Ponit, out Q1T1CH2, out Q2T1CH2, out  Q3T1CH2);
                    A1T1CH2 = Q3T1CH2 + 1.5 * (Q3T1CH2 - Q1T1CH2);
                    A2T1CH2 = Q1T1CH2 - 1.5 * (Q3T1CH2 - Q1T1CH2);

                    double[] BoxPointT1CH2 = { A2T1CH2, A1T1CH2, Q1T1CH2, Q3T1CH2, Q2T1CH2, Q2T1CH2};
                    BoxChartTemp1.Series[0].Points.Add(BoxPointT1CH2);
                }

                if (Temp1CH3Ponit.Count > 2)      //Temp1箱线图CH3
                {
                    double Q1T1CH3 = 0;
                    double Q2T1CH3 = 0;
                    double Q3T1CH3 = 0;
                    double A1T1CH3 = 0;    //上边缘
                    double A2T1CH3 = 0;    //下边缘

                    CalculateQuartile(Temp1CH3Ponit, out Q1T1CH3, out Q2T1CH3, out  Q3T1CH3);
                    A1T1CH3 = Q3T1CH3 + 1.5 * (Q3T1CH3 - Q1T1CH3);
                    A2T1CH3 = Q1T1CH3 - 1.5 * (Q3T1CH3 - Q1T1CH3);

                    double[] BoxPointT1CH3 = { A2T1CH3, A1T1CH3, Q1T1CH3, Q3T1CH3, Q2T1CH3, Q2T1CH3};
                    BoxChartTemp1.Series[0].Points.Add(BoxPointT1CH3);
                }

                if (Temp1CH4Ponit.Count > 2)      //Temp1箱线图CH4
                {
                    double Q1T1CH4 = 0;
                    double Q2T1CH4 = 0;
                    double Q3T1CH4 = 0;
                    double A1T1CH4 = 0;    //上边缘
                    double A2T1CH4 = 0;    //下边缘

                    CalculateQuartile(Temp1CH4Ponit, out Q1T1CH4, out Q2T1CH4, out  Q3T1CH4);
                    A1T1CH4 = Q3T1CH4 + 1.5 * (Q3T1CH4 - Q1T1CH4);
                    A2T1CH4 = Q1T1CH4 - 1.5 * (Q3T1CH4 - Q1T1CH4);

                    double[] BoxPointT1CH4 = { A2T1CH4, A1T1CH4, Q1T1CH4, Q3T1CH4, Q2T1CH4, Q2T1CH4};
                    BoxChartTemp1.Series[0].Points.Add(BoxPointT1CH4);
                }

                if (Temp2CH1Ponit.Count > 2)    //Temp2箱线图CH1
                {
                    double Q1T2CH1 = 0;
                    double Q2T2CH1 = 0;
                    double Q3T2CH1 = 0;
                    double A1T2CH1 = 0;    //上边缘
                    double A2T2CH1 = 0;    //下边缘

                    CalculateQuartile(Temp2CH1Ponit, out Q1T2CH1, out Q2T2CH1, out  Q3T2CH1);
                    A1T2CH1 = Q3T2CH1 + 1.5 * (Q3T2CH1 - Q1T2CH1);
                    A2T2CH1 = Q1T2CH1 - 1.5 * (Q3T2CH1 - Q1T2CH1);

                    double[] BoxPointT2CH1 = { A2T2CH1, A1T2CH1, Q1T2CH1, Q3T2CH1, Q2T2CH1, Q2T2CH1 };
                    BoxChartTemp2.Series[0].Points.Add(BoxPointT2CH1);
                    BoxChartTemp2.Series[0]["BoxPlotShowAverage"] = "false";
                    BoxChartTemp2.Series[0]["PixelPointWidth"] = "50";
                }

                if (Temp2CH2Ponit.Count > 2)      //Temp2箱线图CH2
                {
                    double Q1T2CH2 = 0;
                    double Q2T2CH2 = 0;
                    double Q3T2CH2 = 0;
                    double A1T2CH2 = 0;    //上边缘
                    double A2T2CH2 = 0;    //下边缘

                    CalculateQuartile(Temp2CH2Ponit, out Q1T2CH2, out Q2T2CH2, out  Q3T2CH2);
                    A1T2CH2 = Q3T2CH2 + 1.5 * (Q3T2CH2 - Q1T2CH2);
                    A2T2CH2 = Q1T2CH2 - 1.5 * (Q3T2CH2 - Q1T2CH2);

                    double[] BoxPointT2CH2 = { A2T2CH2, A1T2CH2, Q1T2CH2, Q3T2CH2, Q2T2CH2, Q2T2CH2 };
                    BoxChartTemp2.Series[0].Points.Add(BoxPointT2CH2);
                }

                if (Temp2CH3Ponit.Count > 2)      //Temp2箱线图CH3
                {
                    double Q1T2CH3 = 0;
                    double Q2T2CH3 = 0;
                    double Q3T2CH3 = 0;
                    double A1T2CH3 = 0;    //上边缘
                    double A2T2CH3 = 0;    //下边缘

                    CalculateQuartile(Temp2CH3Ponit, out Q1T2CH3, out Q2T2CH3, out  Q3T2CH3);
                    A1T2CH3 = Q3T2CH3 + 1.5 * (Q3T2CH3 - Q1T2CH3);
                    A2T2CH3 = Q1T2CH3 - 1.5 * (Q3T2CH3 - Q1T2CH3);

                    double[] BoxPointT2CH3 = { A2T2CH3, A1T2CH3, Q1T2CH3, Q3T2CH3, Q2T2CH3, Q2T2CH3 };
                    BoxChartTemp2.Series[0].Points.Add(BoxPointT2CH3);
                }

                if (Temp2CH4Ponit.Count > 2)      //Temp2箱线图CH4
                {
                    double Q1T2CH4 = 0;
                    double Q2T2CH4 = 0;
                    double Q3T2CH4 = 0;
                    double A1T2CH4 = 0;    //上边缘
                    double A2T2CH4 = 0;    //下边缘

                    CalculateQuartile(Temp2CH4Ponit, out Q1T2CH4, out Q2T2CH4, out  Q3T2CH4);
                    A1T2CH4 = Q3T2CH4 + 1.5 * (Q3T2CH4 - Q1T2CH4);
                    A2T2CH4 = Q1T2CH4 - 1.5 * (Q3T2CH4 - Q1T2CH4);

                    double[] BoxPointT2CH4 = { A2T2CH4, A1T2CH4, Q1T2CH4, Q3T2CH4, Q2T2CH4, Q2T2CH4 };
                    BoxChartTemp2.Series[0].Points.Add(BoxPointT2CH4);
                }

                if (Temp3CH1Ponit.Count > 2)    //Temp3箱线图CH1
                {
                    double Q1T3CH1 = 0;
                    double Q2T3CH1 = 0;
                    double Q3T3CH1 = 0;
                    double A1T3CH1 = 0;    //上边缘
                    double A2T3CH1 = 0;    //下边缘

                    CalculateQuartile(Temp3CH1Ponit, out Q1T3CH1, out Q2T3CH1, out  Q3T3CH1);
                    A1T3CH1 = Q3T3CH1 + 1.5 * (Q3T3CH1 - Q1T3CH1);
                    A2T3CH1 = Q1T3CH1 - 1.5 * (Q3T3CH1 - Q1T3CH1);

                    double[] BoxPointT3CH1 = { A2T3CH1, A1T3CH1, Q1T3CH1, Q3T3CH1, Q2T3CH1, Q2T3CH1 };
                    BoxChartTemp3.Series[0].Points.Add(BoxPointT3CH1);
                    BoxChartTemp3.Series[0]["BoxPlotShowAverage"] = "false";
                    BoxChartTemp3.Series[0]["PixelPointWidth"] = "50";
                }

                if (Temp3CH2Ponit.Count > 2)      //Temp3箱线图CH2
                {
                    double Q1T3CH2 = 0;
                    double Q2T3CH2 = 0;
                    double Q3T3CH2 = 0;
                    double A1T3CH2 = 0;    //上边缘
                    double A2T3CH2 = 0;    //下边缘

                    CalculateQuartile(Temp3CH2Ponit, out Q1T3CH2, out Q2T3CH2, out  Q3T3CH2);
                    A1T3CH2 = Q3T3CH2 + 1.5 * (Q3T3CH2 - Q1T3CH2);
                    A2T3CH2 = Q1T3CH2 - 1.5 * (Q3T3CH2 - Q1T3CH2);

                    double[] BoxPointT3CH2 = { A2T3CH2, A1T3CH2, Q1T3CH2, Q3T3CH2, Q2T3CH2, Q2T3CH2 };
                    BoxChartTemp3.Series[0].Points.Add(BoxPointT3CH2);
                }

                if (Temp3CH3Ponit.Count > 2)      //Temp3箱线图CH3
                {
                    double Q1T3CH3 = 0;
                    double Q2T3CH3 = 0;
                    double Q3T3CH3 = 0;
                    double A1T3CH3 = 0;    //上边缘
                    double A2T3CH3 = 0;    //下边缘

                    CalculateQuartile(Temp3CH3Ponit, out Q1T3CH3, out Q2T3CH3, out  Q3T3CH3);
                    A1T3CH3 = Q3T3CH3 + 1.5 * (Q3T3CH3 - Q1T3CH3);
                    A2T3CH3 = Q1T3CH3 - 1.5 * (Q3T3CH3 - Q1T3CH3);

                    double[] BoxPointT3CH3 = { A2T3CH3, A1T3CH3, Q1T3CH3, Q3T3CH3, Q2T3CH3, Q2T3CH3 };
                    BoxChartTemp3.Series[0].Points.Add(BoxPointT3CH3);
                }

                if (Temp3CH4Ponit.Count > 2)      //Temp3箱线图CH4
                {
                    double Q1T3CH4 = 0;
                    double Q2T3CH4 = 0;
                    double Q3T3CH4 = 0;
                    double A1T3CH4 = 0;    //上边缘
                    double A2T3CH4 = 0;    //下边缘

                    CalculateQuartile(Temp3CH4Ponit, out Q1T3CH4, out Q2T3CH4, out  Q3T3CH4);
                    A1T3CH4 = Q3T3CH4 + 1.5 * (Q3T3CH4 - Q1T3CH4);
                    A2T3CH4 = Q1T3CH4 - 1.5 * (Q3T3CH4 - Q1T3CH4);

                    double[] BoxPointT3CH4 = { A2T3CH4, A1T3CH4, Q1T3CH4, Q3T3CH4, Q2T3CH4, Q2T3CH4 };
                    BoxChartTemp3.Series[0].Points.Add(BoxPointT3CH4);
                }

                if (TEMPdt.Rows.Count > 1)
                {
                    BoxChartTempAll.Visible = true;
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;    //网格线虚线
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(224, 224, 224);
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisX.Title = "Channel";
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisY.Title = "Value";   //左边纵坐标名
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisX.TitleAlignment = StringAlignment.Center;
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisY.TitleAlignment = StringAlignment.Center;
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
                    BoxChartTempAll.ChartAreas["ChartArea1"].AxisX.Maximum = 4.9;

                    System.Drawing.Font F = new Font("微软雅黑", 13, FontStyle.Bold);
                    Title title = new Title(DropDownList6.SelectedItem.Text + "@AllTemp", Docking.Top, F, Color.Black);
                    BoxChartTempAll.Titles.Add(title);
                    title.Alignment = ContentAlignment.TopCenter;

                    BoxChartTempAll.Series[1].ChartType = SeriesChartType.Point;

                    if (TempAllCH1Ponit.Count > 2)    //TempALL箱线图CH1
                    {
                        double Q1CH1 = 0;
                        double Q2CH1 = 0;
                        double Q3CH1 = 0;
                        double A1CH1 = 0;    //上边缘
                        double A2CH1 = 0;    //下边缘

                        CalculateQuartile(TempAllCH1Ponit, out Q1CH1, out Q2CH1, out  Q3CH1);
                        A1CH1 = Q3CH1 + 1.5 * (Q3CH1 - Q1CH1);
                        A2CH1 = Q1CH1 - 1.5 * (Q3CH1 - Q1CH1);

                        double[] BoxPointCH1 = { A2CH1, A1CH1, Q1CH1, Q3CH1, Q2CH1, Q2CH1 };
                        BoxChartTempAll.Series[0].Points.Add(BoxPointCH1);
                        BoxChartTempAll.Series[0]["BoxPlotShowAverage"] = "false";
                        BoxChartTempAll.Series[0]["PixelPointWidth"] = "50";
                    }

                    if (TempAllCH2Ponit.Count > 2)      //TempAll箱线图CH2
                    {
                        double Q1CH2 = 0;
                        double Q2CH2 = 0;
                        double Q3CH2 = 0;
                        double A1CH2 = 0;    //上边缘
                        double A2CH2 = 0;    //下边缘

                        CalculateQuartile(TempAllCH2Ponit, out Q1CH2, out Q2CH2, out  Q3CH2);
                        A1CH2 = Q3CH2 + 1.5 * (Q3CH2 - Q1CH2);
                        A2CH2 = Q1CH2 - 1.5 * (Q3CH2 - Q1CH2);

                        double[] BoxPointCH2 = { A2CH2, A1CH2, Q1CH2, Q3CH2, Q2CH2, Q2CH2 };
                        BoxChartTempAll.Series[0].Points.Add(BoxPointCH2);
                    }

                    if (TempAllCH3Ponit.Count > 2)      //TempAll箱线图CH3
                    {
                        double Q1CH3 = 0;
                        double Q2CH3 = 0;
                        double Q3CH3 = 0;
                        double A1CH3 = 0;    //上边缘
                        double A2CH3 = 0;    //下边缘

                        CalculateQuartile(TempAllCH3Ponit, out Q1CH3, out Q2CH3, out  Q3CH3);
                        A1CH3 = Q3CH3 + 1.5 * (Q3CH3 - Q1CH3);
                        A2CH3 = Q1CH3 - 1.5 * (Q3CH3 - Q1CH3);

                        double[] BoxPointCH3 = { A2CH3, A1CH3, Q1CH3, Q3CH3, Q2CH3, Q2CH3 };
                        BoxChartTempAll.Series[0].Points.Add(BoxPointCH3);
                    }

                    if (TempAllCH4Ponit.Count > 2)      //TempAll箱线图CH4
                    {
                        double Q1CH4 = 0;
                        double Q2CH4 = 0;
                        double Q3CH4 = 0;
                        double A1CH4 = 0;    //上边缘
                        double A2CH4 = 0;    //下边缘

                        CalculateQuartile(TempAllCH4Ponit, out Q1CH4, out Q2CH4, out  Q3CH4);
                        A1CH4 = Q3CH4 + 1.5 * (Q3CH4 - Q1CH4);
                        A2CH4 = Q1CH4 - 1.5 * (Q3CH4 - Q1CH4);

                        double[] BoxPointCH4 = { A2CH4, A1CH4, Q1CH4, Q3CH4, Q2CH4, Q2CH4 };
                        BoxChartTempAll.Series[0].Points.Add(BoxPointCH4);
                    }
                }
            }
        }
        else
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), " ", "alert('没有数据！  注：如在未选择类型的情况下，使用SN进行查询，请输入完整的SN。');", true);
        }     
    }

    public void CalculateQuartile(ArrayList list, out double Q1, out double Q2, out double Q3)    //计算四分位数，list至少包含3个数
    {
        list.Sort();
        int listCnt = list.Count;

        double w1 = (listCnt + 1) * 0.25;   //第一四分位数位置
        int w1Int = (int)w1;
        double w1Fraction = w1 - w1Int;
        Q1 = Convert.ToDouble(list[w1Int - 1]) * (1 - w1Fraction) + Convert.ToDouble(list[w1Int]) * w1Fraction;    //Q1即为下分位数

        double w2 = (listCnt + 1) * 0.5;    //第二四分位数位置
        int w2Int = (int)w2;
        double w2Fraction = w2 - w2Int;
        Q2 = Convert.ToDouble(list[w2Int - 1]) * (1 - w2Fraction) + Convert.ToDouble(list[w2Int]) * w2Fraction;    //Q2即为中位数

        double w3 = (listCnt + 1) * 0.75;   //第三四分位数位置
        int w3Int = (int)w3;
        double w3Fraction = w3 - w3Int;
        if (w3Fraction != 0)
        {
            Q3 = Convert.ToDouble(list[w3Int - 1]) * (1 - w3Fraction) + Convert.ToDouble(list[w3Int]) * w3Fraction;    //Q2即为上分位数
        }
        else
        {
            Q3 = Convert.ToDouble(list[w3Int - 1]);
        }
    }

    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList4.Text == "")
        {
            sela.Items.Clear();
        }
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

            foreach (ListItem item in DropDownList2.Items)
            {
                item.Attributes.Add("Title", item.Text);
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
        if (DropDownList4.Text == "")
        {
            sela.Items.Clear();
        }
        
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

            foreach (ListItem item in DropDownList3.Items)
            {
                item.Attributes.Add("Title", item.Text);
            }
        }
        else
        {
            DropDownList3.Items.Clear();
            //TextBox1.Text = "";
        }
    }

    protected void DropDownList3_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList4.Text == "")
        {
            sela.Items.Clear();
        }
    }

    protected void DropDownList4_TextChanged(object sender, EventArgs e)
    {
        string selectcmd;
        DataTable dtSpecsHeader = new DataTable();

        sela.Items.Clear();
        
        //if (DropDownList4.Text=="全表头")
        //{
        //    selectcmd = "select * from GlobalSpecs order by ItemName";
        //}
        //else
        //{
        selectcmd = "select ReportHeader.ID,ReportHeader.ItemName as HeaderName,GlobalSpecs.ItemName,GlobalSpecs.Unit,ReportHeaderSpecs.ShowName from ReportHeader,ReportHeaderSpecs,GlobalSpecs " +
                    "where ReportHeaderSpecs.PID=ReportHeader.ID and  ReportHeaderSpecs.SpecID=GlobalSpecs.ID " +
                    "and ReportHeader.ItemName='" + DropDownList4.Text + "' order by ReportHeaderSpecs.Seq";          
        //}

        dtSpecsHeader = mysql.GetDataTable(selectcmd, "SpecsHeader");

        for (int i = 0; i < dtSpecsHeader.Rows.Count; i++)
        {
            if (dtSpecsHeader.Rows[i]["ShowName"].ToString() != "")
            {
                sela.Items.Add(dtSpecsHeader.Rows[i]["ShowName"].ToString());
            }
            else if (dtSpecsHeader.Rows[i]["Unit"].ToString() != "")
            {
                sela.Items.Add(dtSpecsHeader.Rows[i]["ItemName"].ToString() + "(" + dtSpecsHeader.Rows[i]["Unit"].ToString() + ")");
            }
            else
            {
                sela.Items.Add(dtSpecsHeader.Rows[i]["ItemName"].ToString());
            }
            sela.Items[i].Selected = true;
        }
    }

    public void creattNavi()
    {
        try
        {
            CommCtrl pCtrl = new CommCtrl();

            Control myCtrl = pCtrl.CreateNaviCtrl(funcItemName, "测试数据", Session["BlockType"].ToString(), mysql, out logTracingString);
            this.plhNavi.Controls.Add(myCtrl);
        }
        catch (System.Exception ex)
        {
            throw ex;
        }

    }

    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        sela.Items.Clear();

        if (DropDownList4.Items.Count != 0)
        {
            DropDownList4.SelectedIndex = 0;
        }

        if (DropDownList5.SelectedIndex == 2 || DropDownList5.SelectedIndex == 3)
        {
            if (DropDownList6.Items.Count == 0)
            {
                DropDownList6.Items.Add("");
                string sql = "select ID,ItemName + '(' + Unit + ')' as Spec from GlobalSpecs";
                DataTable dt = mysql.GetDataTable(sql, "GlobalSpecs");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem specItem = new ListItem();

                    if (dt.Rows[i]["Spec"].ToString().Contains("()"))
                    {
                        specItem.Text = dt.Rows[i]["Spec"].ToString().Split('(')[0];
                    }
                    else
                    {
                        specItem.Text = dt.Rows[i]["Spec"].ToString();
                    }
                   
                    specItem.Value = dt.Rows[i]["ID"].ToString();
                    //DropDownList6.Items.Add(dt.Rows[i]["Spec"].ToString());
                    DropDownList6.Items.Add(specItem);
                }
            }

            DropDownList6.Enabled = true;
        }
        else
        {
            if (DropDownList6.Items.Count != 0)
            {
                DropDownList6.SelectedIndex = 0;
            }

            DropDownList6.Enabled = false;
        }
    }
}

#region 正态分布统计
/// <summary>
/// 提供正态分布的数据和图片
/// </summary>
public class StandardDistribution
{
    /// <summary>
    /// 样本数据
    /// </summary>
    public List<double> Xs { get; private set; }
    static float offset = 0;

    public StandardDistribution(List<double> Xs)
    {
        this.Xs = Xs;

        Average = Xs.Average();
        Variance = GetVariance(Xs);
        StandardVariance = Math.Sqrt(Variance);
    }

    /// <summary>
    /// 方差/标准方差的平方
    /// </summary>
    public double Variance { get; private set; }

    /// <summary>
    /// 标准方差
    /// </summary>
    public double StandardVariance { get; private set; }

    /// <summary>
    /// 算数平均值/数学期望
    /// </summary>
    public double Average { get; private set; }

    /// <summary>
    /// 1/ (2π的平方根)的值
    /// </summary>
    public static double InverseSqrt2PI = 1 / Math.Sqrt(2 * Math.PI);

    /// <summary>
    /// 获取指定X值的Y值  计算正态分布的公式
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public double GetGaussianDistributionY(double x)
    {
        double PowOfE = -(Math.Pow(Math.Abs(x - Average), 2) / (2 * Variance));

        double result = (StandardDistribution.InverseSqrt2PI / StandardVariance) * Math.Pow(Math.E, PowOfE);

        return result;
    }

    /// <summary>
    /// 获取正太分布的坐标<x,y>
    /// </summary>
    /// <returns></returns>
    public List<Tuple<double, double>> GetGaussianDistributionYs()
    {
        List<Tuple<double, double>> XYs = new List<Tuple<double, double>>();

        Tuple<double, double> xy = null;

        foreach (double x in Xs)
        {
            xy = new Tuple<double, double>(x, GetGaussianDistributionY(x));
            XYs.Add(xy);
        }
        return XYs;
    }

    /// <summary>
    /// 获取整型列表的方差
    /// </summary>
    /// <param name="src">要计算方差的数据列表</param>
    /// <returns></returns>
    public static double GetVariance(List<double> src)
    {
        double average = src.Average();
        double SumOfSquares = 0;
        src.ForEach(x => { SumOfSquares += Math.Pow(x - average, 2); });
        return SumOfSquares / src.Count;//方差
    }

    /// <summary>
    /// 获取整型列表的方差
    /// </summary>
    /// <param name="src">要计算方差的数据列表</param>
    /// <returns></returns>
    public static float GetVariance(List<float> src)
    {
        float average = src.Average();
        double SumOfSquares = 0;
        src.ForEach(x => { SumOfSquares += Math.Pow(x - average, 2); });
        return (float)SumOfSquares / src.Count;//方差
    }

    /// <summary>
    /// 画一组数据的正态分布
    /// </summary>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
    /// <param name="Scores">分数，Y值</param>
    /// <param name="familyName"></param>
    /// <returns></returns>
    public Bitmap GetGaussianDistributionGraph(int Width, int Height, string familyName = "宋体")
    {
        //横轴 规格上下限和典型值；纵轴 正态分布的值
        Bitmap bitmap = new Bitmap(Width, Height);

        Graphics gdi = Graphics.FromImage(bitmap);

        gdi.Clear(Color.White);
        gdi.SmoothingMode = SmoothingMode.HighQuality;
        gdi.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        gdi.PixelOffsetMode = PixelOffsetMode.HighQuality;      

        List<Tuple<double, double>> Scores = GetGaussianDistributionYs().OrderBy(x => x.Item1).ToList();//排序 方便后面点与点之间的连线  保证 分数低的 在左边

        float YHeight = 0.6F * Height;// 相对左下角 YHeight*0.9F 将表示 maxY
        float XWidth = 0.8F * Width;//相对左下角 XWidth*0.9F 将表示 maxX

        float marginX = (Width - XWidth) / 2F;//x轴相对左右图片边缘的像素
        float marginY = (Height - YHeight) / 2F;//y轴相对上下图片边缘的像素

        PointF leftTop = new PointF(marginX, marginY);

        PointF leftBottom = new PointF(marginX, marginY + YHeight);//坐标轴的左下角

        PointF rightBottom = new PointF(marginX + XWidth, marginY + YHeight);//坐标轴的右下角

        gdi.DrawLine(Pens.Gray, leftBottom, rightBottom);//x轴
        gdi.DrawLine(Pens.Gray, leftBottom, leftTop);//Y轴

        //两个箭头 四条线 6个坐标 另需4个坐标

        PointF YArrowLeft = new PointF(leftTop.X - 5, leftTop.Y + 5);
        PointF YArrowRight = new PointF(leftTop.X + 5, leftTop.Y + 5);
        PointF XArrowTop = new PointF(rightBottom.X - 5, rightBottom.Y - 5);
        PointF XArrowBottom = new PointF(rightBottom.X - 5, rightBottom.Y + 5);

        gdi.DrawLine(Pens.Gray, leftTop, YArrowLeft);
        gdi.DrawLine(Pens.Gray, leftTop, YArrowRight);
        gdi.DrawLine(Pens.Gray, rightBottom, XArrowTop);
        gdi.DrawLine(Pens.Gray, rightBottom, XArrowBottom);

        float unitX = 0.0F;//X轴转换比率
        float unitY = 0.0F;//Y轴转换比率

        List<PointF> pointFs = ConvertToPointF(Scores, XWidth * 0.9F, YHeight * 0.9F, leftTop, out unitX, out unitY);//将分数和概率 转换成 坐标

        gdi.DrawCurve(Pens.Black, pointFs.ToArray(), 0.0F);//基数样条

        //平均分 与 Y轴平行
        PointF avg_top = new PointF(leftTop.X + (float)Average * unitX - offset, leftTop.Y);
        PointF avg_bottom = new PointF(leftTop.X + (float)Average * unitX - offset, leftBottom.Y);
        gdi.DrawLine(Pens.Gray, avg_top, avg_bottom);
        gdi.DrawString(string.Format("{0}", ((float)Average).ToString("F2")), new Font("宋体", 11), Brushes.Black, avg_bottom.X, avg_bottom.Y - 25);

        //将标准差写在横轴下方中间
        PointF variance_pf1 = new PointF(leftBottom.X, avg_bottom.Y + 30);
        gdi.DrawString(string.Format("Sigma = {0}", StandardVariance.ToString("F2")), new Font("宋体", 11), Brushes.Black, variance_pf1.X, variance_pf1.Y);

        //将标题写在上方中间
        PointF variance_pf = new PointF(leftBottom.X + (XWidth / 2) - 100, 0);
        gdi.DrawString("测试项结果分布统计图", new Font("微软雅黑", 13, FontStyle.Bold), Brushes.Black, variance_pf.X, variance_pf.Y);

        //将最小分数 和 最大分数 分成4段 标记在坐标轴横轴上
        double minX = Scores.Min(x => x.Item1);
        double maxX = Scores.Max(x => x.Item1);

        double perSegment = (double)(maxX - minX) / 5;// (maxX - minX) / 9F;//每一段表示的分数

        List<double> segs = new List<double>();//每一个分段分界线横轴的值

        segs.Add(leftBottom.X + (float)minX * unitX - offset);

        for (int i = 1; i < 6; i++)
        {
            segs.Add(leftBottom.X + (float)minX * unitX + perSegment * i * unitX - offset);
        }
        for (int i = 0; i < 6; i++)
        {
            gdi.DrawPie(Pens.Black, (float)segs[i] - 1, leftBottom.Y - 1, 2, 2, 0, 360);

            gdi.DrawString(string.Format("{0}", ((minX + perSegment * (i))).ToString("F")), new Font("宋体", 11), Brushes.Black, (float)segs[i] - 15, leftBottom.Y + 5);
        }
        return bitmap;
    }

    /// <summary>
    /// 将数据转换为坐标
    /// </summary>
    /// <param name="Scores"></param>
    /// <param name="X">最长利用横轴</param>
    /// <param name="Y">最长利用纵轴 </param>
    /// <param name="leftTop">左上角原点</param>
    /// <returns></returns>
    private static List<PointF> ConvertToPointF(List<Tuple<double, double>> Scores, float X, float Y, PointF leftTop, out  float unitX, out  float unitY)
    {
        double maxY = Scores.Max(x => x.Item2);
        double maxX = Scores.Max(x => x.Item1);
        double minX = Scores.Min(x => x.Item1);

        maxX = maxX - minX;

        List<PointF> result = new List<PointF>();

        float paddingY = Y * 0.01F;
        float paddingX = X * 0.01F;

        unitY = (float)((Y - paddingY) / maxY);//单位纵轴表示出来需要的高度 计算出来的纵坐标需要 leftTop.Y+(Y-item2*unitY)+paddingY
        unitX = (float)((X - paddingX) / maxX);//单位横轴表示出来需要的宽度 计算出来的横坐标需要 leftTop.X+item1*unitX

        offset = (float)minX * unitX - leftTop.X / 2;  
     
        PointF pf = new PointF();
        foreach (Tuple<double, double> item in Scores)
        {
            pf = new PointF(leftTop.X + (float)item.Item1 * unitX - offset, leftTop.Y + (Y - (float)item.Item2 * unitY) + paddingY);
            result.Add(pf); 
        }

        return result;
    }

     /// <summary>
    /// 计算CPK值
    /// CPK=Cp*（1-|Ca|）
    /// 存在上下规格时：Cp=(规格上限 - 规格下限) / (6 * Sigma)
    /// Ca= （平均值-（规格上限 + 规格下限）/2）/（（规格上限 - 规格下限）/2）
    /// 
    /// 只有上限时：Cpu=|规格上限-平均值|/(3 * Sigma)
    /// 只有下限时：Cpl=|平均值-规格下限|/(3 * Sigma)
    /// 
    /// </summary>
    public double GetCPK(double specMax, double specMin)
    {
        double CPK = 0;
        double Cp = 0;
        double Ca = 0;

        if (specMax < 32767 && specMin > -32767)
        {
            Cp = (specMax - specMin) / (6 * StandardVariance);
            Ca = (Average - (specMax + specMin) / 2) / ((specMax - specMin) / 2);
            CPK = Cp * (1 - Math.Abs(Ca));
        }
        else if (specMax >= 32767)   //没有上限
        {
            CPK = Math.Abs(Average - specMin) / (3 * StandardVariance);
        }
        else if (specMax <= -32767)   //没有下限
        {
            CPK = Math.Abs(specMax - Average) / (3 * StandardVariance);
        }

        return CPK;
    }

}
#endregion