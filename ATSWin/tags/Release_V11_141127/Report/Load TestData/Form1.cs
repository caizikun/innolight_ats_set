using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.Threading;
//using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
using System.Data.OleDb;
using System.IO;
using ATSDataBase;
using System.Xml.Serialization;
namespace Load_TestData
{
    public partial class Form1 : Form
    {
        

        public DataIO mysql = new DataIO();
        private string databasename;
        public Form1()
        {
            InitializeComponent();
            
        }
        //导出datagridview
        private void button9_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows.Count == 0)
            //{
            //    MessageBox.Show("there are no data");
            //}
            //else
            //{
            //    int collindex = 0;
            //    int rowindex = 0;
            //    Excel.Application excel = new Excel.Application();
            //    excel.Application.Workbooks.Add(true);
            //    excel.Visible = true;
            //    //写入标题
            //    for (int i = 0; i < dataGridView1.ColumnCount; i++)
            //    {
            //        if (dataGridView1.Columns[i].Visible)
            //        {
            //            collindex++;
            //            excel.Cells[1, collindex] = dataGridView1.Columns[i].HeaderText;
            //        }
            //    }
            //    //写入数值
            //    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            //    {
            //        if (dataGridView1.Rows[i].Visible)
            //        {
            //            rowindex++;
            //            collindex = 0;
            //            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            //            {
            //                if (dataGridView1.Columns[j].Visible)
            //                {
            //                    collindex++;
            //                    excel.Cells[rowindex + 1, collindex] = dataGridView1[j, i].Value.ToString();
            //                }
            //            }
            //        }
            //    }
            //    MessageBox.Show("ok");

            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

                ConfigXmlIO myConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml"); 
                databasename = myConfigXmlIO.ServerName; 
                if (databasename.ToUpper().Trim() == "local".ToUpper().Trim())//知道为LOCAL
                {
                    mysql = new LocalDatabaseIO(Application.StartupPath + "\\SQL.accdb");
                }
                else
                {
                    mysql = new ServerDatabaseIO(myConfigXmlIO.ServerName, myConfigXmlIO.ATSDBName, myConfigXmlIO.ATSUser, myConfigXmlIO.ATSPWD);

                }

            string selectcmd = "select distinct (ItemName) from GlobalProductionType";
            mysql.OpenDatabase(true);
            DataTable dt = mysql.GetDataTable(selectcmd, "GlobalProductionType");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //comboBox1.Items.Add(dt.Rows[i]["Name"].ToString());

                comboBox1.Items.Add(dt.Rows[i]["ItemName"].ToString());
            }
            mysql.OpenDatabase(false);
            //this.reportViewer1.RefreshReport();
        }
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
       

        string selectcmd;


        private void button1_Click(object sender, EventArgs e)
        {
            mysql.OpenDatabase(true);
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Type不能为空");
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("PN不能为空");
            }
            else
            {
                MessageBox.Show("请稍等。。。。。。");
                if ((checkBox1.Checked == false) && (checkBox2.Checked == false) && (checkBox3.Checked == false) && (checkBox4.Checked == false))
                {
                    MessageBox.Show("没有选择筛选条件，显示全部信息");
                    selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP], TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                          "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                          " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))";

                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev], TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                         "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                          " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'";
                    }


                    if (checkBox2.Checked == true)
                    {
                        if (checkBox1.Checked == false)
                        {
                            selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev], TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                               "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                                " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))AND TopoRunRecordTable.[SN] ='" + comboBox5.SelectedItem + "'";
                        }
                        else
                        {
                            selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP], TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                               "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                                    " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.SelectedItem + "'";
                       
                        }
                    }
                    if (checkBox3.Checked == true)
                    {
                        selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev], TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                                  "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan, TopoTestControl,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                           " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID]) AND((TopoTestControl.PID)=[TopoTestPlan].[ID]) AND ((TopoLogRecord.PID)=[TopoTestControl].[ID]) AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))AND TopoRunRecordTable.[StartTime] >'" + textBox1.Text + " " + "00:00:00" + "'";
                       
                        if (checkBox4.Checked == true)
                        {
                            selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP], TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                                 "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan, TopoTestControl,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                                " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID]) AND((TopoTestControl.PID)=[TopoTestPlan].[ID]) AND ((TopoLogRecord.PID)=[TopoTestControl].[ID]) AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))AND TopoRunRecordTable.[StartTime] >'" + textBox1.Text + " " + "00:00:00" + "'AND TopoRunRecordTable.[StartTime] <'" + textBox1.Text + " 23:59:59" + "'";

                        }
                    }
                    if (checkBox4.Checked == true)
                    {
                        selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev], TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                                 "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan, TopoTestControl,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                          " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID]) AND((TopoTestControl.PID)=[TopoTestPlan].[ID]) AND ((TopoLogRecord.PID)=[TopoTestControl].[ID]) AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))AND TopoRunRecordTable.[StartTime] <'" + textBox1.Text + " 23:59:59" + "'";
                       
                        if (checkBox3.Checked == true)
                        {
                            selectcmd = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev], TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime], TopoLogRecord.[Result],TopoRunRecordTable.[EndTime]," +
                                   "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan, TopoTestControl,TopoLogRecord,TopoTestData,TopoRunRecordTable " +
                            " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID]) AND((TopoTestControl.PID)=[TopoTestPlan].[ID]) AND ((TopoLogRecord.PID)=[TopoTestControl].[ID]) AND ((TopoTestData.PID)=[TopoLogRecord].[ID]))AND TopoRunRecordTable.[StartTime] <'" + textBox1.Text + " 23:59:59" + "'AND TopoRunRecordTable.[StartTime] >'" + textBox1.Text + " " + "00:00:00" + "'";
                       
                        }
                    }
                }
                DataTable dt = mysql.GetDataTable(selectcmd, "TopoTestData");
                List<string> oliw = new List<string>();
                List<string> oo = new List<string>();
                oo.Add("PID");
                //oo.Add("SN");
                //oliw.Add("ID");
                oliw.Add("PID");
                oliw.Add("ProductType");
                oliw.Add("ProductName");
                oliw.Add("TestPlan");
                oliw.Add("FWRev");
                oliw.Add("SN");
                oliw.Add("IP");
                oliw.Add("Channel");
                oliw.Add("Temp");
                oliw.Add("Vcc");
                oliw.Add("StartTime");
                oliw.Add("EndTime");
                oliw.Add("RunRecordID");
                oliw.Add("Result");
                dt = DataTable_Pivot(dt, oliw, "ItemName", "ItemValue", oo);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Report.rdlc");

                for (int i = 1; i < dt.Columns.Count; i++)
                {

                    string collname = dt.Columns[i].ToString();
                    string ctest = collname.Replace("(", "");
                    ctest = ctest.Replace(")", "");
                    ctest = ctest.Replace("%", "");

                    if ((collname != "PID") && (collname != "ProductType") && (collname != "ProductName") && (collname != "TestPlan") && (collname != "SN") && (collname != "Channel") && (collname != "Temp") && (collname != "Vcc") && (collname != "StartTime") && (collname != "EndTime") && (collname != "RunRecordID") && (collname != "Result") && (collname != "FWRev") && (collname != "IP"))
                    {
                        //添加Field节点
                        XmlNodeList fileds = xmlDoc.GetElementsByTagName("Fields");

                        XmlNode filedNode = fileds.Item(0).FirstChild.CloneNode(true);
                        filedNode.Attributes["Name"].Value = ctest;
                        filedNode.FirstChild.InnerText = collname;
                        fileds.Item(0).AppendChild(filedNode);
                        //添加TablixColumn

                        XmlNodeList tablixColumns = xmlDoc.GetElementsByTagName("TablixColumns");
                        XmlNode tablixColumn = tablixColumns.Item(0).FirstChild;
                        XmlNode newtablixColumn = tablixColumn.CloneNode(true);
                        tablixColumns.Item(0).AppendChild(newtablixColumn);

                        //TablixMember
                        XmlNodeList tablixMembers = xmlDoc.GetElementsByTagName("TablixColumnHierarchy");

                        XmlNode tablixMember = tablixMembers.Item(0).FirstChild.FirstChild;
                        XmlNode newTablixMember = tablixMember.CloneNode(true);
                        tablixMembers.Item(0).FirstChild.AppendChild(newTablixMember);

                        XmlNodeList tablixRows = xmlDoc.GetElementsByTagName("TablixRows");

                        //TablixRows1
                        var tablixRowsRowCells1 = tablixRows.Item(0).FirstChild.ChildNodes[1];
                        XmlNode tablixRowCell1 = tablixRowsRowCells1.FirstChild;
                        XmlNode newtablixRowCell1 = tablixRowCell1.CloneNode(true);
                        var textBox1 = newtablixRowCell1.FirstChild.ChildNodes[0];
                        textBox1.Attributes["Name"].Value = ctest + 1;

                        var paragraphs = textBox1.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
                        paragraphs.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = collname;
                        var defaultName1 = textBox1.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = collname + 1;

                        tablixRowsRowCells1.AppendChild(newtablixRowCell1);
                        //TablixRows2
                        var tablixRowsRowCells2 = tablixRows.Item(0).ChildNodes[1].ChildNodes[1];
                        XmlNode tablixRowCell2 = tablixRowsRowCells2.FirstChild;
                        XmlNode newtablixRowCell2 = tablixRowCell2.CloneNode(true);
                        var textBox2 = newtablixRowCell2.FirstChild.ChildNodes[0];
                        textBox2.Attributes["Name"].Value = ctest;

                        var paragraphs2 = textBox2.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
                        //paragraphs2.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = "=Fields!GPA.Value";
                        paragraphs2.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = "=Fields!" + ctest + ".Value";
                        var defaultName2 = textBox2.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = collname;

                        tablixRowsRowCells2.AppendChild(newtablixRowCell2);

                    }

                }

                xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + "Report1.rdlc");
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReportPath = @"Report1.rdlc";
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.RefreshReport();

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectPID = "select * from GlobalProductionType where ItemName='" + comboBox1.SelectedItem + "'";
            mysql.OpenDatabase(true);
            DataTable dt1 = mysql.GetDataTable(SelectPID, "GlobalProductionType");
            UInt64 PID = Convert.ToUInt64(dt1.Rows[0]["ID"]);
            string SelectPN = "select distinct (PN) from GlobalProductionName where PID=" + PID;
            DataTable dt2 = mysql.GetDataTable(SelectPN, "GlobalProductionName");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt2.Rows[i]["PN"].ToString());

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            string SelectPID = "select * from GlobalProductionName where PN='" + comboBox2.SelectedItem + "'";
            mysql.OpenDatabase(true);
            DataTable dt1 = mysql.GetDataTable(SelectPID, "GlobalProductionName");
            UInt64 PID = Convert.ToUInt64(dt1.Rows[0]["ID"]);
            string SelectName = "select distinct (ItemName) from TopoTestPlan where PID=" + PID;
            DataTable dt2 = mysql.GetDataTable(SelectName, "TopoTestPlan");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                comboBox3.Items.Add(dt2.Rows[i]["ItemName"].ToString());
               

                    string SelectPID1 = "select * from TopoTestPlan where ItemName ='" + dt2.Rows[i]["ItemName"].ToString() + "'";
                    //mysql.OpenDatabase(true);
                    DataTable dt3 = mysql.GetDataTable(SelectPID1, "TopoTestPlan");
                    UInt64 PID1 = Convert.ToUInt64(dt3.Rows[0]["ID"]);

                    string SelectSN = "select distinct (SN) from TopoRunRecordTable where PID=" + PID1;
                    DataTable dt4 = mysql.GetDataTable(SelectSN, "TopoRunRecordTable");
                    for (int j = 0; j < dt4.Rows.Count; j++)
                    {
                        comboBox5.Items.Add(dt4.Rows[j]["SN"].ToString());
                    }

                    
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string SelectPID = "select * from TopoTestPlan where ItemName='" + comboBox3.SelectedItem + "'";
            //mysql.OpenDatabase(true);
            //DataTable dt1 = mysql.GetDataTable(SelectPID, "TopoTestPlan");
            //UInt64 PID = Convert.ToUInt64(dt1.Rows[0]["ID"]);
            //string SelectName = "select * from TopoTestControl where PID=" + PID;
            //DataTable dt2 = mysql.GetDataTable(SelectName, "TopoTestControl");
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
            //    comboBox4.Items.Add(dt2.Rows[i]["ItemName"].ToString());
            //}
            //string SelectStartTime = "select * from TopoRunRecordTable where PID=" + PID;
            //DataTable dt3 = mysql.GetDataTable(SelectStartTime, "TopoRunRecordTable");
            //for (int i = 0; i < dt3.Rows.Count; i++)
            //{
            //    comboBox7.Items.Add(dt3.Rows[i]["StartTime"].ToString());
            //}
            //string SelectSN = "select distinct (SN) from TopoRunRecordTable where PID=" + PID;
            //DataTable dt4 = mysql.GetDataTable(SelectSN, "TopoRunRecordTable");
            //for (int i = 0; i < dt4.Rows.Count; i++)
            //{
            //    comboBox5.Items.Add(dt4.Rows[i]["SN"].ToString());
            //}
        }


        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
            monthCalendar2.Visible = false;

        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            textBox1.Text = monthCalendar1.SelectionStart.Date.ToString("yyyy/MM/dd");
            monthCalendar1.Visible = false;

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar2.Visible = true;
            monthCalendar1.Visible = false;
        }

        private void monthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            textBox2.Text = monthCalendar2.SelectionStart.Date.ToString("yyyy/MM/dd");
            monthCalendar2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form1 = new Form();
            form1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            comboBox3.SelectedItem = null;
            comboBox5.SelectedItem = null;
            textBox1.Text = null;
            textBox2.Text = null;
            monthCalendar1.Visible = false;
            monthCalendar2.Visible = false;
            
        }

       

       

       

       


       


       

        

       

    }
}
