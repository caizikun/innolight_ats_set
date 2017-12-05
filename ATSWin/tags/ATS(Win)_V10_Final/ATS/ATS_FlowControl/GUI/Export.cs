using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace ATS
{
    public partial class Export : Form
    {
        //---------------------------- From  Form1
        public FlowControll pflowControl;
        public int TestPlanID;

        //----------------
        private DataTable dtform1 = new DataTable();
        private DataTable dtform3 = new DataTable();
        private DataTable TotalResult = new DataTable();

        private String StrTime = "";
        string StrAllSelectSnNo = "";

        public ArrayList strID = new ArrayList();
       
        public Export()
        {
            InitializeComponent();
        }

        private void Export_Load(object sender, EventArgs e)
        {
            buttonEnter.Enabled = false;   
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
           labelShow.Text = "开始从数据库中获取数据........";
           labelShow.Refresh();

            progressBar1.Value = 0;
            progressBar1.Refresh();

            ArrayList SnIdArray = new ArrayList();
            SnIdArray.Clear();
            int SelectRowsCoun = 0;

            string StrSnIdCondition = "";
            SelectRowsCoun = dataGridView1.SelectedRows.Count;

            StrAllSelectSnNo = "";

            if (SelectRowsCoun > 0)
            {
                for (int i = SelectRowsCoun - 1; i >= 0; i--)
                {
                    int LL = dataGridView1.SelectedRows[i].Index;

                    SnIdArray.Add(strID[LL]);

                }

                StrAllSelectSnNo = SnIdArray[0].ToString();
                StrSnIdCondition = "Where TopoRunRecordTable.id=" + SnIdArray[0];

                for (int i = 1; i < SnIdArray.Count; i++)
                {
                    StrSnIdCondition += " or TopoRunRecordTable.id=" + SnIdArray[i];
                    StrAllSelectSnNo += "," + SnIdArray[i].ToString();
                }
            }
            else//全部导出
            {
                StrAllSelectSnNo = strID[0].ToString();
                StrSnIdCondition = "Where TopoRunRecordTable.id=" + strID[0];

                for (int i = 1; i < strID.Count; i++)
                {
                    StrSnIdCondition += " or TopoRunRecordTable.id=" + strID[i];
                    StrAllSelectSnNo += "," + strID[i].ToString();
                }                              
            }

            if (GetDataInf())
            {
                //TotalResult = dtform3;
                TotalResult = DataTableJoin(dtform1, dtform3);       //获取TotalResult

                int TotalResultColumnNum = TotalResult.Columns.Count;
                TotalResult.Columns[12].SetOrdinal(TotalResultColumnNum - 1);
                TotalResult.Columns.RemoveAt(0);

                //TotalResult.Columns.RemoveAt(TotalResult.Columns.Count-1);

                for (int K = 0; K < TotalResult.Rows.Count; K++)
                {
                    TotalResult.Rows[K]["FWRev"] = "OX" + TotalResult.Rows[K]["FWRev"].ToString();
                }

                cExportExcel(changeDT(TotalResult));
                //cExportExcel(TotalResult);

                MessageBox.Show("数据已经导出....");

                this.Close();
            }                      
        }

        public bool GetDataInf()
        {
            try
            {
                string selectcmd = "";
                // LogID  IP  Remark  SN  FWRev	 LightSourceMessage	 StartTime  Endtime	 Vcc  Temp  Channel	 Result  TestLog
                string selectcmd_First = "select TopoLogRecord.ID as LogID,TopoRunRecordTable.IP,TopoRunRecordTable.Remark,TopoRunRecordTable.SN,TopoRunRecordTable.FWRev,TopoRunRecordTable.LightSource AS LightSourceMessage,TopologRecord.StartTime,TopologRecord.EndTime, TopoLogRecord.Voltage as Vcc,TopoLogRecord.Temp,TopoLogRecord.Channel,TopologRecord.Result,TopologRecord.TestLog";              
                string selectcmd_Second = " FROM TopoTestPlan,TopoLogRecord,TopoRunRecordTable";
                string selectcmd_Third = " WHERE TopoRunRecordTable.PID=" + TestPlanID + " AND TopoLogRecord.RunRecordID=[TopoRunRecordTable].[ID]" +
               " AND TopoTestPlan.[ID] ='" + TestPlanID + "' AND TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) AND TopoLogRecord.CtrlType=2 order by  TopologRecord.ID";

                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third;

                dtform1 = pflowControl.MyDataio.GetDataTable(selectcmd, "TopoLogRecord");

                // 测试指标列
                selectcmd_First = "select DISTINCT TopoTestData.ItemName";
                selectcmd_Second = " FROM TopoLogRecord,TopoRunRecordTable,TopoTestData";
                selectcmd_Third = " WHERE TopoLogRecord.RunRecordID=TopoRunRecordTable.ID AND TopoLogRecord.ID=TopoTestData.PID AND TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) order by TopoTestData.ItemName";

                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third;

                DataTable dtform2 = pflowControl.MyDataio.GetDataTable(selectcmd, "TopoTestData");

                // 测试指标列转置
                selectcmd_First = "select TopoTestData.PID as LogID,";
                selectcmd_Second = " ";
                for (int i = 0; i < dtform2.Rows.Count - 1; i++)
                {
                    selectcmd_Second += "sum(case when TopoTestData.ItemName='" + dtform2.Rows[i]["ItemName"] + "' then TopoTestData.ItemValue end)as '" + dtform2.Rows[i]["ItemName"] + "',";
                }
                selectcmd_Second += "sum(case when TopoTestData.ItemName='" + dtform2.Rows[dtform2.Rows.Count - 1]["ItemName"] + "' then TopoTestData.ItemValue end)as '" + dtform2.Rows[dtform2.Rows.Count - 1]["ItemName"] + "'";
                selectcmd_Third = " FROM TopoLogRecord,TopoRunRecordTable,TopoTestData";
                string selectcmd_Fourth = " WHERE TopoLogRecord.RunRecordID=TopoRunRecordTable.ID AND TopoLogRecord.ID=TopoTestData.PID AND TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) group by TopoTestData.PID order by TopoTestData.PID";

                selectcmd = selectcmd_First + selectcmd_Second + selectcmd_Third + selectcmd_Fourth;

                dtform3 = pflowControl.MyDataio.GetDataTable(selectcmd, "TopoTestData");

                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("没有任何行"))
                    MessageBox.Show("表TopoLogRecord中，没有RunRecordID = " + StrAllSelectSnNo + "的行");
                else
                    MessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataTable DataTableJoin(DataTable dt1, DataTable dt2)
        {
            labelShow.Text = "数据分析中........";
            labelShow.Refresh();

            for (int i = 1; i < dt2.Columns.Count; i++)
            {
                dt1.Columns.Add(dt2.Columns[i].ColumnName);
            }

            int TotalRowNo = 0;

            for (int i = 0, j = 0; i < dt1.Rows.Count; i++, j++)
            {
                progressBar1.Value = (int)(TotalRowNo * 100 / (dt1.Rows.Count));

                if (dt1.Rows[i]["LogID"].ToString() == dt2.Rows[j]["LogID"].ToString())
                {
                    for (int m = 1; m < dt2.Columns.Count; m++)
                    {
                        dt1.Rows[i][dt2.Columns[m].ColumnName] = dt2.Rows[j][dt2.Columns[m].ColumnName];
                    }
                }
                else
                {
                    j--;
                }

                TotalRowNo++;
            }

            return dt1;
        }
             
        //-------------------
      private DataTable changeDT(DataTable dt)
    {
        DataTable datatable = new DataTable();
        //克隆表结构，表的数据并没有克隆
        datatable = dt.Clone();
        datatable.Columns["RESULT"].DataType = typeof(string);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = datatable.NewRow();

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                switch (dt.Columns[j].ColumnName.Trim().ToUpper())
                {
                    case "IBIASHA":
                    case "IBIASHW":
                    case "IBIASLW":
                    case "IBIASLA":

                    case "RXPOWERHA":
                    case "RXPOWERHW":
                    case "RXPOWERLA":
                    case "RXPOWERLW":

                    case "TEMPHA":
                    case "TEMPHW":
                    case "TEMPLA":
                    case "TEMPLW":

                    case "TXPOWERHA":
                    case "TXPOWERHW":
                    case "TXPOWERLA":
                    case "TXPOWERLW":

                    case "VCCHA":
                    case "VCCHW":
                    case "VCCLA":
                    case "VCCLW":

                    //datatable.Columns[j].DataType = Type.GetType("System.string");
                    string ss = dt.Columns[j].DataType.ToString();
                    if (dt.Rows[i][j].ToString()=="1")
                    {
                        dr[j] = "PASS";
                    }
                    else if (dt.Rows[i][j].ToString() == "")
                    {
                        dr[j] = "";
                    }
                    else
                    {
                        dr[j] = "FAIL";
                    }

                        break;
                    case"RESULT":


                        if (dt.Rows[i][j].ToString().ToUpper() == "TRUE")
                        {
                            dr[j] = "PASS";
                        }
                        else
                        {
                            dr[j] = "FAIL";
                        }

                        break;

                    default:
                        dr[j] = dt.Rows[i][j];
                        break;

                }               
            }
           
            datatable.Rows.Add(dr);
        }
        return datatable;
    }
       //-----------------
        public void cExportExcel(DataTable dv)
        { 
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Excel   files(*.xls)|*.xls";

            saveFileDialog1.FilterIndex = 0;

            saveFileDialog1.RestoreDirectory = true;

            saveFileDialog1.CreatePrompt = true;

            saveFileDialog1.Title = "导出Excel文件到 ";

            DateTime now = DateTime.Now;

            saveFileDialog1.FileName = now.Second.ToString().PadLeft(2, '0');
            saveFileDialog1.ShowDialog();
            Stream myStream;

            myStream = saveFileDialog1.OpenFile();


            labelShow.Text = "数据导出中.......";
            labelShow.Refresh();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            String str = " ";

            for (int i = 0; i < dv.Columns.Count; i++)
            {

                if (i > 0)
                {
                    str += "\t ";
                }
                str += dv.Columns[i].ColumnName;
            }

            sw.WriteLine(str);


            for (int rowNo = 0; rowNo < dv.Rows.Count; rowNo++)
            {
                String tempstr = " ";
                for (int columnNo = 0; columnNo < dv.Columns.Count; columnNo++)
                {
                    if (columnNo > 0)
                    {
                        tempstr += "\t ";
                    }
                    //tempstr+=dg.Rows[rowNo,columnNo].ToString();         
                    tempstr += dv.Rows[rowNo][columnNo].ToString();
                }
                if (dv.Rows[rowNo]["Result"].ToString().Trim().ToUpper() != "" && dv.Rows[rowNo]["Result"].ToString().Trim().ToUpper() != null)
                {
                    sw.WriteLine(tempstr);
                }
               
            }

            progressBar1.Value = 100;

            labelShow.Text = "数据导出已经完成.......";
            sw.Close();

            myStream.Close();
        }

        private void buttonGetInf_Click(object sender, EventArgs e)
        {
            strID.Clear();

            string StrDataBaseName=   pflowControl.MyDataio.GetType().Name.ToUpper();

            string StrTableName = "TopoRunRecordTable";

            string StrSelectconditions = "";

            if (StrDataBaseName == "LOCALDATABASE")
            {
                StrTime = " and (StartTime between #" + dateTimePickerStartTime.Value.ToString("yyyy-MM-dd") + " 08:30:00#" + " and #" + dateTimePickerEndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59#)";
           
            }
            else
            {
                StrTime = " and (StartTime between ('" + dateTimePickerStartTime.Value.ToString() + "')" + " and ('" + dateTimePickerEndTime.Value.ToString() + " '))";
            }

            //StrSelectconditions = "select * from " + StrTableName + " where PID=" + TestPlanID+StrTime + " order by id";
           // pflowControl.StrIpAddress = "10.160.47.8";
            StrSelectconditions = "select * from " + StrTableName + " where PID=" + TestPlanID + StrTime + "and (IP='" + pflowControl.StrIpAddress + "')" + " order by id";

            DataTable aDt= pflowControl.MyDataio.GetDataTable(StrSelectconditions, StrTableName);

            if (aDt.Rows.Count > 0)
            {
                buttonEnter.Enabled = true;
            }
            else
            {
                buttonEnter.Enabled = false;
                MessageBox.Show("该时间段内没有符合条件的测试数据!"+"\r\n"+"或者 没有IP号为当前电脑（" + pflowControl.StrIpAddress + " )的测试数据!");
            }


            for (int aDtRowCount = 0; aDtRowCount < aDt.Rows.Count; aDtRowCount++)
            {
                strID.Add(aDt.Rows[aDtRowCount]["ID"].ToString ());
            }

            aDt.Columns.RemoveAt(0);
            aDt.Columns.RemoveAt(1);

            dataGridView1.DataSource = aDt;
            dataGridView1.Refresh();
            System.Threading.Thread.Sleep(2000);
            //dataGridView1.Columns["ID"].Visible = false;
           // dataGridView1.Columns["PID"].Visible = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            buttonGetInf.Focus();

        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            buttonEnter.Enabled = false;
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            buttonEnter.Enabled = false;
        }

 




 


 







                    


         

     

    }
    


}
