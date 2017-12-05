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
using System.Data.OleDb;
using System.IO;
using ATSDataBase;
using System.Xml.Serialization;
using Microsoft.Reporting.WinForms;
namespace BackcoefData
{
    public partial class MainForm : Form
    {
        public DataIO mysql = new DataIO();
        private string databasename = "";
        public DataTable dtform1 = new DataTable();
        public DataTable dt = new DataTable();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            string selectcmd = "";
            if ((comboBox1.SelectedItem == null) || (comboBox2.SelectedItem == null) || (comboBox3.SelectedItem == null))
            {
                MessageBox.Show("条件选择未完成");
            }
            else
            {
                if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
                {
                    MessageBox.Show("时间选择有误");
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        if (textBox1.Text == "")
                        {
                            MessageBox.Show("请输入SN");
                        }
                        selectcmd = "select TopoRunRecordTable.[SN],TopoRunRecordTable.[StartTime], TopoRunRecordTable.[EndTime],TopoRunRecordTable.[ID]" +
                             "FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoRunRecordTable " +
                             " WHERE ((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) " +
                            "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'AND TopoRunRecordTable.[SN]='" + textBox1.Text + "'" +
                             "AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text +  "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " +  "'";

                    }
                    else
                    {
                        selectcmd = "select TopoRunRecordTable.[SN],TopoRunRecordTable.[StartTime], TopoRunRecordTable.[EndTime],TopoRunRecordTable.[ID]" +
                            "FROM GlobalProductionType,GlobalProductionName,TopoTestPlan,TopoRunRecordTable " +
                            " WHERE ((TopoTestPlan.PID)=[GlobalProductionName].[ID]) AND((GlobalProductionName.PID)=[GlobalProductionType].[ID])AND((TopoRunRecordTable.PID)=[TopoTestPlan].[ID]) " +
                            "AND TopoTestPlan.[ItemName] ='" + comboBox3.SelectedItem + "'AND GlobalProductionType.[ItemName] ='" + comboBox1.SelectedItem + "'AND GlobalProductionName.[PN] ='" + comboBox2.SelectedItem + "'" +
                            "AND TopoRunRecordTable.[StartTime] <'" + dateTimePicker2.Text  + "'AND TopoRunRecordTable.[StartTime] >'" + dateTimePicker1.Text + " " +  "'";


                    }
                   
                    dtform1 = mysql.GetDataTable(selectcmd, "TopoRunRecordTable");
                    if (dtform1.Rows.Count == 0)
                    {
                        MessageBox.Show("没有数据，请重新选择");
                        dataGridView1.DataSource = null;
                        dataGridView2.DataSource = null;
                    }
                    else
                    {

                        dataGridView1.DataSource = dtform1;
                        dataGridView1.Columns[3].Visible = false;
                        //dataGridView1.Height = dataGridView1.Rows.Count * dataGridView1.RowTemplate.Height + dataGridView1.ColumnHeadersHeight;
                        //if (dataGridView1.Rows.Count * dataGridView1.RowTemplate.Height < splitContainer1.Panel1.Height)
                        //{
                        //    if (dataGridView1.Rows.Count != 0)
                        //    { dataGridView1.Height = dataGridView1.Rows.Count * dataGridView1.RowTemplate.Height + dataGridView1.ColumnHeadersHeight; }
                        //}
                        //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
                        //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
                        //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                    }
                }
               
            }
            

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.AddDays(-5).ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();
            //控件随窗体大小改变
            this.Resize += new EventHandler(MainForm_Resize_1);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            MainForm_Resize_1(new object(), new EventArgs());

            ConfigXmlIO myConfigXmlIO = new ConfigXmlIO(Application.StartupPath + @"\Config.xml");
            databasename = myConfigXmlIO.ServerName;
            if (databasename.ToUpper().Trim() == "local".ToUpper().Trim())
            {
                mysql = new LocalDatabaseIO(Application.StartupPath + "\\SQL.accdb");
            }
            else
            {
                mysql = new ServerDatabaseIO(myConfigXmlIO.ServerName, myConfigXmlIO.ATSDBName, myConfigXmlIO.ATSUser, myConfigXmlIO.ATSPWD);

            }

            string selectcmd = "select distinct (ItemName) from GlobalProductionType";
            bool flag = mysql.OpenDatabase(true);
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
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
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
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    comboBox3.Items.Add(dt2.Rows[i]["ItemName"].ToString());
                }
            }
            else
            {
                comboBox3.Items.Clear();

            }
        }

       
       

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }

        #region 控件跟随界面大小变动

        private float X;

        private float Y;

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }
        private void MainForm_Resize_1(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            //if (dataGridView1.Rows.Count * dataGridView1.RowTemplate.Height < splitContainer1.Panel1.Height)
            //{
            //    if (dataGridView1.Rows.Count != 0)
            //    { dataGridView1.Height = dataGridView1.Rows.Count * dataGridView1.RowTemplate.Height + dataGridView1.ColumnHeadersHeight; }
            //}
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                   
            ////if (dataGridView2.Rows.Count != 0)
            ////{ dataGridView2.Height = dataGridView2.Rows.Count * dataGridView2.RowTemplate.Height + dataGridView2.ColumnHeadersHeight; }
                             
        }
       
        #endregion

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                dataGridView2.DataSource = null;
                UInt64 pid1 = Convert.ToUInt64(this.dataGridView1.CurrentRow.Cells["ID"].Value);
                string SelectPID = "select Page,StartAddr,ItemValue,ItemSize from TopoTestCoefBackup where PID= '" + pid1 + "' order by Page ASC,StartAddr ASC ";

                DataTable dt1 = mysql.GetDataTable(SelectPID, "TopoTestCoefBackup");
                dataGridView2.DataSource = dt1;
                //dataGridView2.Height = dataGridView2.Rows.Count * dataGridView2.RowTemplate.Height + dataGridView2.ColumnHeadersHeight;
            }

        }

        
    }
}
