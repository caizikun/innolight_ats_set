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
    public partial class MainForm : Form
    {

        public DataIO mysql = new DataIO();
        private string databasename = "";
        public DataTable dtform1= new DataTable();
        public DataTable dt= new DataTable();
        public List<string> mainformtestnamelist = new List<string>();
        public List<string> mainformcomparelist = new List<string>();
        public List<string> mainformspeclist = new List<string>();
        public List<string> mainformlogiclist = new List<string>();
        public List<string> mainformdisplaylist = new List<string>();
        public DataTable advanceddt = new DataTable();
        public string displaylist = "";
        public string texttemp = "";
        public bool snflag = true;
        public bool testplanflag = true;
        public bool ipflag = true;
        public MainForm()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Text = DateTime.Now.AddDays(-5).ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();
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
           bool flag= mysql.OpenDatabase(true);
           if (flag == false)
           {
               MessageBox.Show("database failue in link");
           }
           else
           {
               DataTable dt = mysql.GetDataTable(selectcmd, "GlobalProductionType");
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   comboBox1.Items.Add(dt.Rows[i]["ItemName"].ToString());
               }
           }
           // mysql.OpenDatabase(false);

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
       

        
        public void displayreport(string[] colum,DataTable dt)
        {
            bool flag = true;
            XmlDocument xmlDoc = new XmlDocument();            
            //xmlDoc.Load(@"..\..\Report.rdlc");   
            xmlDoc.Load(Application.StartupPath + @"\Report.rdlc");      //运行EXE时找不到，继续选择复制到debug里
            Array.Sort(colum);//使用该函数可以直接排序
            for (int i = 0; i < colum.Length; i++)
            {
                if (dt.Columns.Contains(colum[i]))
                {
                    string collname = colum[i].ToString();
                    string ctest = collname.Replace("(", "");
                    ctest = ctest.Replace(")", "");
                    ctest = ctest.Replace("%", "");
                    ctest = ctest.Replace(" ", "");
                    if ((collname != "PID") && (collname != "ProductType") && (collname != "ProductName") && (collname != "TestPlan") && (collname != "SN") && (collname != "Channel") && (collname != "Temp") && (collname != "Vcc") && (collname != "StartTime") && (collname != "EndTime") && (collname != "RunRecordID") && (collname != "Result") && (collname != "FWRev") && (collname != "IP") && (collname != "TestLog") && (collname != "LightSourceMessage"))
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
                else
                {
                   
                        flag = false;
                        MessageBox.Show("不含选所列" + colum[i] + "请重新筛选或清除筛选列");
                        break;
                    
                }
            }
            if (flag)
            {
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
       
        public void customerreport()
        {
            ipflag = true;
            testplanflag = true;
            snflag = true;
            string selectcmd = "";
            // SN	 FWRev	 LightSourceMessage	 StartTime	 Endtime	 Vcc	 Temp	 Channel	 Result
            string selectcmd_First = "select GlobalProductionType.[ItemName] as ProductType,GlobalProductionName.[PN] as ProductName,TopoTestPlan.[ItemName] as TestPlan,TopoLogRecord.[ID] as LogID,TopoLogRecord.[Temp],TopoLogRecord.[Voltage] as Vcc,TopoLogRecord.[Channel],TopoLogRecord.[RunRecordID],TopoLogRecord.[TestLog],TopoRunRecordTable.[SN],TopoRunRecordTable.[FWRev],TopoRunRecordTable.[IP],TopoRunRecordTable.[StartTime],(case when TopoLogRecord.[Result]='true' then 'pass'else 'fail' end) as Result,TopoRunRecordTable.[EndTime]," + "TopoRunRecordTable.[LightSource] as LightSourceMessage,";
            string selectcmd_Second = "TopoTestData.* FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoLogRecord,TopoTestData,TopoRunRecordTable ";
            string selectcmd_Third = " WHERE (((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) AND((TopoLogRecord.RunRecordID)=[TopoRunRecordTable].[ID])  AND ((TopoTestData.PID)=[TopoLogRecord].[ID])) AND [TopoLogRecord].[CtrlType]=2 ";
            if ((checkBox1.Checked == false) && (checkBox2.Checked == false) && (checkBox3.Checked == false) && (checkBox4.Checked == false) && (checkBox5.Checked == false))
                {
                    selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                          "AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";

                }
                else
                {
                   
                    if (checkBox1.Checked == true)
                    {
                        if (comboBox3.Text == "")
                        {
                            testplanflag = false;
                            MessageBox.Show("TestPlan为空!请输入TestPlan或者取消勾选");
                        }
                        if (checkBox2.Checked == true)
                        {
                            if (comboBox5.Text == "")
                            {
                                snflag = false;
                                MessageBox.Show("SN为空!请输入SN或者取消勾选");
                            }
                            if (checkBox5.Checked == true)
                            {
                                if (comboBox4.Text == "")
                                {
                                    ipflag = false;
                                    MessageBox.Show("IP为空!请输入IP或者取消勾选");
                                }

                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                             "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";


                                    }

                                }
                            }
                            else
                            {
                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                             "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";


                                }

                            }

                            }


                        }
                        else
                        {
                            if (checkBox5.Checked == true)
                            {
                                if (comboBox4.Text == "")
                                {
                                    ipflag = false;
                                    MessageBox.Show("IP为空!请输入IP或者取消勾选");
                                }
                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                             "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";


                                    }

                                }

                            }
                            else
                            {
                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                             "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";


                                }

                            }

                            
                            }
                        }

                    }
                    else
                    {
                        if (checkBox2.Checked == true)
                        {
                            if (comboBox5.Text == "")
                            {
                                snflag = false;
                                MessageBox.Show("SN为空!请输入SN或者取消勾选");
                            }
                            if (checkBox5.Checked == true)
                            {
                                if (comboBox4.Text == "")
                                {
                                    ipflag = false;
                                    MessageBox.Show("IP为空!请输入IP或者取消勾选");
                                }
                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                             "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";


                                    }

                                }
                            }
                            else
                            {
                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                             "AND TopoRunRecordTable.[SN] ='" + comboBox5.Text + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";


                                    }

                                }

                            }

                        }
                        else
                        {
                            if (checkBox5.Checked == true)
                            {
                                if (comboBox4.Text == "")
                                {
                                    ipflag = false;
                                    MessageBox.Show("IP为空!请输入IP或者取消勾选");
                                }
                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                                "AND TopoRunRecordTable.[IP] ='" + comboBox4.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                            }
                            else
                            {
                                if (checkBox3.Checked == true)
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                            "AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                           "AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " + "'";
                                    }

                                }
                                else
                                {
                                    if (checkBox4.Checked == true)
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                              "AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text + "'";
                                    }
                                    else
                                    {
                                        selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third +
                                                "AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'";
                                    }

                                }
                            
                            }
                        }
                    }
                }
                selectcmd += " order by TopoRunRecordTable.[IP],TestPlan,TopoRunRecordTable.[StartTime],LogID";   //150216@terry Add sort string-->TestPlanName,RunRecordTable.starttime,LogID
                dtform1 = mysql.GetDataTable(selectcmd,"TopoTestData");
                List<string> oliw = new List<string>();
                List<string> oo = new List<string>();
                oo.Add("PID");
                oliw.Add("PID");
                oliw.Add("ProductType");
                oliw.Add("ProductName");
                oliw.Add("TestPlan");              
                oliw.Add("SN");
                oliw.Add("FWRev");
                oliw.Add("LightSourceMessage");
                oliw.Add("StartTime");
                oliw.Add("EndTime");                
                oliw.Add("Vcc");               
                oliw.Add("Temp");
                oliw.Add("Channel");
                oliw.Add("RunRecordID");
                oliw.Add("Result");
                oliw.Add("TestLog");
                oliw.Add("IP");
                dt = DataTable_Pivot(dtform1,oliw,"ItemName","ItemValue",oo);
                
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;

         
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] temp;
            if ((checkBox2.Checked == true) && (comboBox5.Text != ""))
            {
                if (!(comboBox5.Items.Contains(comboBox5.Text)))
                {
                    comboBox5.Items.Add(comboBox5.Text);
                
                }
                
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Type不能为空");
                dtform1 = new DataTable();
                displaylist = "";
                texttemp = "";
                textBox3.Text = "";
                mainformtestnamelist.Clear();
                mainformcomparelist.Clear();
                mainformspeclist.Clear();
                mainformlogiclist.Clear();
                textBox4.Text = "";
                mainformdisplaylist.Clear();
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
            }
            else if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("PN不能为空");
                dtform1 = new DataTable();
                displaylist = "";
                texttemp = "";
                textBox3.Text = "";
                mainformtestnamelist.Clear();
                mainformcomparelist.Clear();
                mainformspeclist.Clear();
                mainformlogiclist.Clear();
                textBox4.Text = "";
                mainformdisplaylist.Clear();
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
            }
            else
            {
               
                customerreport();
                if (textBox3.Text != "")
                {
                    advancedreport();
                    dt = advanceddt;

                    }

                    if (textBox4.Text != "")
                    {
                        temp = new string[mainformdisplaylist.Count];
                        for (int i = 0; i < mainformdisplaylist.Count; i++)
                        {
                            temp[i] = mainformdisplaylist[i];
                        }

                    }
                    else
                    {
                        temp = new string[dt.Columns.Count];
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            temp[i] = dt.Columns[i].ToString(); 
                        }
                    }

                    displayreport(temp, dt);

            }
           
        }
       
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
           

        }

      
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
           
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            customerreport();
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("PN不能为空");
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
            }
            else
            {
                if ((snflag == true) && (testplanflag == true) && (ipflag == true))
                {
                    Advanced_query Advancedform = new Advanced_query(texttemp, dtform1, this);
                    Advancedform.Listitemvalue = texttemp;
                    Advancedform.Testnamelist = mainformtestnamelist;
                    Advancedform.Comparelist = mainformcomparelist;
                    Advancedform.Speclist = mainformspeclist;
                    Advancedform.Logiclist = mainformlogiclist;
                    if (Advancedform.ShowDialog() == DialogResult.OK)
                    {
                        textBox3.Text = Advancedform.Listitemvalue;
                        texttemp = Advancedform.Listitemvalue;
                        textBox3.Text = textBox3.Text.Replace(",", "");

                    }
                    else
                    {
                        Advancedform.Show();
                    }
                }
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                textBox3.Text = "";
                texttemp = "";
                string[] temp;
                customerreport();
                if (textBox4.Text != "")
                {
                    temp = new string[mainformdisplaylist.Count];
                    for (int i = 0; i < mainformdisplaylist.Count; i++)
                    {
                        temp[i] = mainformdisplaylist[i];
                    }

                }
                else
                {
                    temp = new string[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        temp[i] = dt.Columns[i].ToString();
                    }
                }

                displayreport(temp, dt);
            }
        }
        public void advancedreport()
        {
            string selectcondition = "";
            DataTable dt1;
            DataRow[] dr = new DataRow[1];
            List<string> oliw = new List<string>();
            List<string> oo = new List<string>();
            List<float> specmax = new List<float>();
            bool flag = true;
            for (int i = 0; i < mainformtestnamelist.Count; i++)
            {
                if ((mainformspeclist[i] == "SpecMax") || (mainformspeclist[i] == "SpecMin"))
                {
                    dt1 = DataTable_Pivot(dtform1, oliw, "ItemName", mainformspeclist[i], oo);
                    if (dt1.Columns.Contains(mainformtestnamelist[i]))
                    {
                        specmax.Add(Convert.ToSingle(dt1.Rows[0][mainformtestnamelist[i]]));
                    }
                    else
                    {
                        flag = false;
                        MessageBox.Show("不含高级查询选所选内容" + mainformtestnamelist[i] + "请重新筛选或清除高级查询内容");
                        break;
                    }

                }
                else
                {
                    dt1 = DataTable_Pivot(dtform1, oliw, "ItemName", "ItemValue", oo);
                    if (dt1.Columns.Contains(mainformtestnamelist[i]))
                    {
                        specmax.Add(Convert.ToSingle(mainformspeclist[i]));
                        
                    }
                    else
                    {
                        flag = false;
                        MessageBox.Show("不含高级查询选所选内容" + mainformtestnamelist[i]+ "请重新筛选或清除高级查询内容");
                        break;
                    }
                    
                }
            }
            if (flag)
            {
                advanceddt = dt.Clone();
                for (int i = 0; i < mainformtestnamelist.Count; i++)
                {
                    if (selectcondition == "")
                    {
                        selectcondition = "[" + mainformtestnamelist[i] + "]" + mainformcomparelist[i] + specmax[i];
                    }
                    else
                    {

                        if (mainformlogiclist[i - 1] == "与")
                        {
                            selectcondition = "(" + selectcondition + " and " + "[" + mainformtestnamelist[i] + "]" + mainformcomparelist[i] + specmax[i] + ")";
                        }
                        else
                        {
                            selectcondition = "(" + selectcondition + " or " + "[" + mainformtestnamelist[i] + "]" + mainformcomparelist[i] + specmax[i] + ")";
                        }

                    }
                }
                dr = dt.Select(selectcondition, "PID ASC");

                if (dr.Length == 0)
                {
                    MessageBox.Show("没有符合所选条件的信息");
                }
                else
                {
                    for (int j = 0; j < dr.Length; j++)
                    {
                        advanceddt.ImportRow((DataRow)dr[j]);
                    }
                }
            }
            else
            {
              
                advanceddt = new DataTable();
            }
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            customerreport();
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("PN不能为空");
                reportViewer1.Reset();
                reportViewer1.LocalReport.DataSources.Clear();
            }
            else
            {
                if ((snflag == true) && (testplanflag == true) && (ipflag == true))
                {
                    display_query displayform = new display_query(displaylist, dtform1, this);
                    displayform.Displaynamelist = mainformdisplaylist;
                    displayform.Listitemvalue = textBox4.Text.Replace(" ", "").Trim();
                    if (displayform.ShowDialog() == DialogResult.OK)
                    {
                        textBox4.Text = displayform.Listitemvalue;
                        displaylist = textBox4.Text;
                        mainformdisplaylist = displayform.Displaynamelist;

                    }
                    else
                    {
                        displayform.Show();
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                string[] temp = new string[mainformdisplaylist.Count];
                for (int i = 0; i < mainformdisplaylist.Count; i++)
                {
                    temp[i] = mainformdisplaylist[i];
                }
                if (textBox3.Text != "")
                {
                    advancedreport();
                    displayreport(temp, advanceddt);
                }
                else
                {
                    displayreport(temp, dt);

                }

            }
            else
            {

                MessageBox.Show("没有选择条件");
            }
        }
       
       
       
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                textBox4.Text = "";
                displaylist = "";
                string[] temp;
                customerreport();
                if (textBox3.Text != "")
                {
                    advancedreport();
                    dt = advanceddt;

                }
                temp = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    temp[i] = dt.Columns[i].ToString();
                }
                displayreport(temp, dt);

            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string SelectPID = "select * from GlobalProductionType where ItemName='" + comboBox1.SelectedItem + "'";

            DataTable dt1 = mysql.GetDataTable(SelectPID, "GlobalProductionType");
            UInt64 PID = Convert.ToUInt64(dt1.Rows[0]["ID"]);
            string SelectPN = "select distinct (PN) from GlobalProductionName where PID=" + PID;
            DataTable dt2 = mysql.GetDataTable(SelectPN, "GlobalProductionName");
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            //comboBox5.Items.Clear();
            //comboBox5.Text = "";
            dtform1 = new DataTable();
            displaylist = "";
            texttemp = "";
            textBox3.Text = "";
            mainformtestnamelist.Clear();
            mainformcomparelist.Clear();
            mainformspeclist.Clear();
            mainformlogiclist.Clear();
            mainformdisplaylist.Clear();
            textBox4.Text = "";
            //checkBox1.Checked = false;
            //checkBox2.Checked = false;
            //checkBox3.Checked = false;
            //checkBox4.Checked = false;
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt2.Rows[i]["PN"].ToString());

            }
           
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string SelectPID = "select * from GlobalProductionName where PN='" + comboBox2.SelectedItem + "'";
                DataTable dt1 = mysql.GetDataTable(SelectPID, "GlobalProductionName");
                UInt64 PID = Convert.ToUInt64(dt1.Rows[0]["ID"]);
                string SelectName = "select distinct (ItemName) from TopoTestPlan where PID=" + PID;
                DataTable dt2 = mysql.GetDataTable(SelectName, "TopoTestPlan");
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                //comboBox5.Items.Clear();
                //comboBox5.Text = "";
                dtform1 = new DataTable();
                displaylist = "";
                texttemp = "";
                textBox3.Text = "";
                mainformdisplaylist.Clear();
                mainformtestnamelist.Clear();
                mainformcomparelist.Clear();
                mainformspeclist.Clear();
                mainformlogiclist.Clear();
                textBox4.Text = "";
                //checkBox1.Checked = false;
                //checkBox2.Checked = false;
                //checkBox3.Checked = false;
                //checkBox4.Checked = false;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dt2.Rows[i]["ItemName"].ToString());
                }
            }
            else
            {
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                //comboBox5.Items.Clear();

            }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                string SelectPID = "select * from GlobalProductionName where PN='" + comboBox2.SelectedItem + "'";
                DataTable dt1 = mysql.GetDataTable(SelectPID, "GlobalProductionName");
                UInt64 PID = Convert.ToUInt64(dt1.Rows[0]["ID"]);
                string SelectName = "select * from TopoTestPlan where PID='" + PID + "' AND ItemName ='" + comboBox3.SelectedItem + "'";

                DataTable dt2 = mysql.GetDataTable(SelectName, "TopoTestPlan");
                //string SelectPID1 = "select * from TopoTestPlan where ItemName ='" + comboBox3.SelectedItem + "'";
                //DataTable dt3 = mysql.GetDataTable(SelectPID1, "TopoTestPlan");
                UInt64 PID1 = Convert.ToUInt64(dt2.Rows[0]["ID"]);
                string SelectSN = "select distinct (IP) from TopoRunRecordTable where PID=" + PID1;
                DataTable dt4 = mysql.GetDataTable(SelectSN, "TopoRunRecordTable");
                comboBox4.Items.Clear();
                //comboBox5.Items.Clear();
                //comboBox5.Text = "";
                //checkBox2.Checked = false;
                for (int j = 0; j < dt4.Rows.Count; j++)
                {
                    comboBox4.Items.Add(dt4.Rows[j]["IP"].ToString());
                }
            }

        }

       

       

       

       


       


       

        

       

    }
}
