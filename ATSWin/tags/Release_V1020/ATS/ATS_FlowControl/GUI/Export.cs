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
    //public enum CloumsName :UInt16 
    //{ Morning, Afternoon, Evening 
    //} 

        //public enum CloumsName     
        //{  
        //    ProductType,
        //    productName,
        //    TestPlanName,
        //    SN,
        //    LogID,
        //    Channel,
        //    Temp,
        //    Vcc,
        //    StartTime,
        //    EndTime,
        //    Result
        //}
    public partial class Export : Form
    {
        //---------------------------- From  Form1
        public FlowControll pflowControl;
        public int TestPlanID;
        public string ProductType;
        public string ProductName;
        public string TestPlanName;
        //----------------
        private DataTable DtCurrentSN, DtCurrentCondition, DtCurrentLog, DtCurrentResult;
        private DataTable TotalResult = new DataTable();
        public ArrayList DataCloumsName = new ArrayList();
        public ArrayList DataResultTableCloumsName = new ArrayList();
        public DataTable Dt=new DataTable();
        private String StrTime = "";
       // public DataTable ResultTable = new DataTable();
        public DataTable CurrentResultTable = new DataTable();
//-----------------------id
        private string CurrentSnID="";
        private string CurrentSN = "";
        private string CurrentTemp = "";
        private string CurrentVcc = "";
        private string CurrentChannel = "";
        private string CurrentLogID = "";
        //private string CurrentResult = "";
        private string CurrentConditionID = "";
        private string CurrentStartTime = "";
        private string CurrentEndTime = "";
        private string CurrentItemName = "";
        private string CurrentItemValue = "";
        private string CurrentResult = "";
        private bool CurrentConditionResult ;

        private int LogId = -1;
        public Export()
        {
            InitializeComponent();
           // DtSN = new DataTable();
            DtCurrentCondition = new DataTable();
            DtCurrentLog = new DataTable();
            DtCurrentResult = new DataTable();
        }

        private void Export_Load(object sender, EventArgs e)
        {


          
           
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
           labelShow.Text = "开始从数据库中获取数据........";
           labelShow.Text = "";
            progressBar1.Value = 0;
            progressBar1.Refresh();
            string SelectCondition = "";
            string TableName = "";

            ArrayList SnIdArray = new ArrayList();
            int SelectRowsCoun = 0;

            string StrSnIdCondition = "";
            SelectRowsCoun = dataGridView1.SelectedRows.Count;

            string StrAllSelectSnNo = "";
            if (SelectRowsCoun > 0)
            {
                for (int i = SelectRowsCoun - 1; i >= 0; i--)
                {
                    string LL = dataGridView1.SelectedRows[i].Cells["id"].Value.ToString();

                    SnIdArray.Add(LL);

                }

                StrAllSelectSnNo = SnIdArray[0].ToString();
                StrSnIdCondition = "Where TopoRunRecordTable.id=" + SnIdArray[0];

                for (int i = 1; i < SnIdArray.Count; i++)
                {
                    StrSnIdCondition += " or TopoRunRecordTable.id=" + SnIdArray[i];
                    StrAllSelectSnNo += "," + SnIdArray[i].ToString();
                }

                SelectCondition = "SELECT  TopologRecord.ID as LogID,TopoRunRecordTable.SN AS SN,TopoRunRecordTable.LightSource AS LightSourceMessage,TopologRecord.StartTime as StartTime,TopologRecord.EndTime as Endtime , TopoTestControl.vcc as Vcc ,TopoTestControl.temp as Temp,TopoLogRecord.Channel as Channel,TopologRecord.Result as Result " +
" from ((TopoRunRecordTable inner join TopologRecord  on   TopologRecord.RunRecordID=TopoRunRecordTable.id )  inner join  TopoTestControl on  TopologRecord.pid = TopoTestControl.id) where TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) and TopoTestControl.ItemName like '%FMT%' order by  TopologRecord.ID";
                TableName = "TopologRecord";


            }
            else//全部导出
            {
                StrAllSelectSnNo = "select id from  TopoRunRecordTable where PID=" + TestPlanID + StrTime;

                SelectCondition = "SELECT  TopologRecord.ID as LogID,TopoRunRecordTable.SN AS SN,TopoRunRecordTable.LightSource AS LightSourceMessage,TopologRecord.StartTime as StartTime,TopologRecord.EndTime as Endtime , TopoTestControl.vcc as Vcc ,TopoTestControl.temp as Temp,TopoLogRecord.Channel as Channel,TopologRecord.Result as Result " +
" from ((TopoRunRecordTable inner join TopologRecord  on   TopologRecord.RunRecordID=TopoRunRecordTable.id )  inner join  TopoTestControl on  TopologRecord.pid = TopoTestControl.id) where TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) and TopoTestControl.ItemName like '%FMT%' order by  TopologRecord.ID";

                //SelectCondition = "SELECT  TopologRecord.ID as LogID,TopoRunRecordTable.SN AS SN,TopologRecord.StartTime as StartTime,TopologRecord.EndTime as Endtime , TopoLogRecord.vcc as Vcc ,TopoLogRecord.temp as Temp,TopoLogRecord.Channel as Channel,TopologRecord.Result as Result " +
                //" from  TopologRecord where TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) and TopoTestControl.ItemName like '%FMT%' order by  TopologRecord.ID";
             
                TableName = "TopologRecord";
            }
            StrAllSelectSnNo = "select id from  TopoRunRecordTable where PID=" + TestPlanID + StrTime;

           DataTable TotalResult = pflowControl.MyDataio.GetDataTable(SelectCondition, TableName);

          // TotalResult = TotalResult1.Clone();
           TableName = "TopoTestData";
            string TopoTestDataIdCondition = "";

            if (TotalResult.Rows.Count > 0)
            {


                SelectCondition = "SELECT distinct (ItemName) from " + TableName;

                TopoTestDataIdCondition = " Where pid in(Select )";

                //------------------------------------------- 获取TestData表头


                SelectCondition = "select  distinct (ItemName) from TopoTestData Where TopoTestData.pid in (Select TopoLogRecord.ID FROM TopoLogRecord where TopoLogRecord.RunRecordID IN (" +
        " SELECT TopoRunRecordTable.id from TopoRunRecordTable where TopoRunRecordTable.id in(" + StrAllSelectSnNo + ") ))";

                Dt = pflowControl.MyDataio.GetDataTable(SelectCondition, TableName);

                DataCloumsName.Clear();

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    string ss = Dt.Rows[i]["ItemName"].ToString();
                   
                    DataCloumsName.Add(Dt.Rows[i]["ItemName"]);
                   
                
                }

                DataCloumsName.Add("ErrorItemName");// 添加单项测试不合格的项目名称
                //-------------------------------------------
                for (int i = 0; i < DataCloumsName.Count; i++)
                {
                    TotalResult.Columns.Add(DataCloumsName[i].ToString());
                   // TotalResult.Columns[DataCloumsName[i].ToString()].DataType = Type.GetType("System.string");
                }

               // TotalResult.Columns.Add("Result");

                labelShow.Text = "数据分析中........";
                labelShow.Refresh();
                //TotalResult.Columns["ItemValue"].DataType = Type.GetType("System.string");
               // TotalResult.Columns[0].DataType = Type.GetType("System.string");
                for (int TotalRowNo = 0; TotalRowNo < TotalResult.Rows.Count; TotalRowNo++)
                {
                    CurrentConditionResult = true;

                    CurrentLogID = TotalResult.Rows[TotalRowNo]["logID"].ToString();
                    SelectCondition = "SELECT * from TopoTestData where pid=" + CurrentLogID;
                    TableName = "TopoTestData";
                    CurrentResultTable = pflowControl.MyDataio.GetDataTable(SelectCondition, TableName);

                    TotalResult.Rows[TotalRowNo]["ErrorItemName"] = "";
                    string StrErrorItem = "";
                    if (CurrentResultTable.Rows.Count > 0)
                    {
                        for (int ResultRow = 0; ResultRow < CurrentResultTable.Rows.Count; ResultRow++)
                        {
                            CurrentItemName = CurrentResultTable.Rows[ResultRow]["ItemName"].ToString();                           
                            CurrentItemValue = CurrentResultTable.Rows[ResultRow]["ItemValue"].ToString();
                                 
                            if (!Convert.ToBoolean( CurrentResultTable.Rows[ResultRow]["Result"]))
                            {
                                StrErrorItem += CurrentItemName + ",";
                            }
                             //CurrentResultTable.Rows[ResultRow]["Result"]n

                            TotalResult.Rows[TotalRowNo][CurrentItemName] = CurrentItemValue;

                        }
                        TotalResult.Rows[TotalRowNo]["ErrorItemName"] = StrErrorItem;
                    }

                    progressBar1.Value = (int)(TotalRowNo * 100 / (TotalResult.Rows.Count));
                }
            }

            TotalResult.Columns.RemoveAt(0);

           // changeDT(TotalResult);

           // dataGridView1.DataSource = changeDT(TotalResult);


            cExportExcel(changeDT(TotalResult));
            //TableName = "TopoTestData";


           // cExportExcel(TotalResult);
            MessageBox.Show("数据已经导出....");
            this.Close();
           
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
                { sw.WriteLine(tempstr);
                }
               
            }

            progressBar1.Value = 100;

            labelShow.Text = "数据导出已经完成.......";
            sw.Close();

            myStream.Close();
        }

        private void buttonGetInf_Click(object sender, EventArgs e)
        {
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

            StrSelectconditions = "select * from " + StrTableName + " where PID=" + TestPlanID+StrTime + " order by id";
            DataCloumsName.Clear();
            DataResultTableCloumsName.Clear();
            dataGridView1.DataSource = pflowControl.MyDataio.GetDataTable(StrSelectconditions, StrTableName);
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["PID"].Visible = false;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = dataGridView1.Width/(dataGridView1.ColumnCount-2);
            }
            buttonGetInf.Focus();

        }

 




 


 







                    


         

     

    }
    


}
