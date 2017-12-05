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

                SelectCondition = "SELECT  TopologRecord.ID as LogID,TopoRunRecordTable.SN AS SN,TopologRecord.StartTime as StartTime,TopologRecord.EndTime as Endtime , TopoTestControl.vcc as Vcc ,TopoTestControl.temp as Temp,TopoLogRecord.Channel as Channel,TopologRecord.Result as Result " +
" from ((TopoRunRecordTable inner join TopologRecord  on   TopologRecord.RunRecordID=TopoRunRecordTable.id )  inner join  TopoTestControl on  TopologRecord.pid = TopoTestControl.id) where TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) order by  TopologRecord.ID";
                TableName = "TopologRecord";


            }
            else//全部导出
            {
                StrAllSelectSnNo = "select id from  TopoRunRecordTable where PID=" + TestPlanID + StrTime;

                SelectCondition = "SELECT  TopologRecord.ID as LogID,TopoRunRecordTable.SN AS SN,TopologRecord.StartTime as StartTime,TopologRecord.EndTime as Endtime , TopoTestControl.vcc as Vcc ,TopoTestControl.temp as Temp,TopoLogRecord.Channel as Channel,TopologRecord.Result as Result " +
" from ((TopoRunRecordTable inner join TopologRecord  on   TopologRecord.RunRecordID=TopoRunRecordTable.id )  inner join  TopoTestControl on  TopologRecord.pid = TopoTestControl.id) where TopoLogRecord.RunRecordID IN (" + StrAllSelectSnNo + " ) order by  TopologRecord.ID";
                TableName = "TopologRecord";
            }

            //            SelectCondition = "SELECT  TopologRecord.ID as ID,TopoRunRecordTable.SN AS SN,TopoTestplan.Name AS TestplanName," +
            //"TopologRecord.StartTime as StartTime,TopologRecord.EndTime as Endtime,Channel,Temp,Vcc "
            //+ "FROM (((TopoTestplan inner join  Topo TestPlanRunRecordTable on TopoTestplan.id = TopoRunRecordTable.Pid ) inner join TopologRecord  on TopologRecord.SNID=TopoRunRecordTable.id )"
            //+ "inner join  TopoTestControl on  TopologRecord.pid = TopoTestControl.id )" + StrSnIdCondition + " order by TopologRecord.ID";




     

            TotalResult = pflowControl.MyDataio.GetDataTable(SelectCondition, TableName);
           // TotalResult = pflowControl.MyDataio.GetDataTable(SelectCondition, TableName);
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
                    DataCloumsName.Add(Dt.Rows[i]["ItemName"]);
                    // DataResultTableCloumsName.Add(Dt.Rows[i]["ItemName"]);
                }
                //  DataCloumsName.Add("Result");

                //-------------------------------------------
                for (int i = 0; i < DataCloumsName.Count; i++)
                {
                    TotalResult.Columns.Add(DataCloumsName[i].ToString());

                }

               // TotalResult.Columns.Add("Result");

                labelShow.Text = "数据分析中........";
                labelShow.Refresh();
                for (int TotalRowNo = 0; TotalRowNo < TotalResult.Rows.Count; TotalRowNo++)
                {
                    CurrentConditionResult = true;

                    CurrentLogID = TotalResult.Rows[TotalRowNo]["logID"].ToString();
                    SelectCondition = "SELECT * from TopoTestData where pid=" + CurrentLogID;
                    TableName = "TopoTestData";
                    CurrentResultTable = pflowControl.MyDataio.GetDataTable(SelectCondition, TableName);
                    if (CurrentResultTable.Rows.Count > 0)
                    {
                        for (int ResultRow = 0; ResultRow < CurrentResultTable.Rows.Count; ResultRow++)
                        {
                            CurrentItemName = CurrentResultTable.Rows[ResultRow]["ItemName"].ToString();
                            CurrentItemValue = CurrentResultTable.Rows[ResultRow]["ItemValue"].ToString();
                            CurrentResult = CurrentResultTable.Rows[ResultRow]["Result"].ToString();

                            TotalResult.Rows[TotalRowNo][CurrentItemName] = CurrentItemValue;

                        }
                    }

                    progressBar1.Value = (int)(TotalRowNo * 100 / (TotalResult.Rows.Count));
                }
            }

            TotalResult.Columns.RemoveAt(0);

            dataGridView1.DataSource = TotalResult;


            TableName = "TopoTestData";


            cExportExcel(TotalResult);
            MessageBox.Show("数据已经导出....");
            this.Close();
           
        }
   
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
                StrTime = " and (StartTime between datevalue('" + dateTimePickerStartTime.Value.ToString("yyyy-MM-dd") + " 08:30:00')" + " and datevalue('" + dateTimePickerEndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59'))";
           
            }
            else
            {
                StrTime = " and (StartTime between ('" + dateTimePickerStartTime.Value.ToString("yyyy-MM-dd") + " 08:30:00')" + " and ('" + dateTimePickerEndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59'))";
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

        }

 




 


 







                    


         

     

    }
    


}
