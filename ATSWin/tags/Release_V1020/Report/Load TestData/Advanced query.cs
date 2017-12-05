using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
namespace Load_TestData
{
    public partial class Advanced_query : Form
    {
        private MainForm mainform;
        private string listitemvalue;
        private List<string> testnamelist=new List<string>();
        private List<string> comparelist = new List<string>();
        private List<string> speclist = new List<string>();
        private List<string> logiclist = new List<string>();
        public DataTable dt = new DataTable();
        public Advanced_query(string selectstring, DataTable form1dt,MainForm form)
        {
            mainform = form;
            string[] test = selectstring.Split(',');
            dt = form1dt;
            InitializeComponent();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (comboBox1.Items.Contains(dt.Rows[i]["ItemName"].ToString()) == false)
                {
                    comboBox1.Items.Add(dt.Rows[i]["ItemName"].ToString());
                }

            }
            if (selectstring == "")
            {
                listBox1.Text = null;

            }
            else
            {
                for (int i = 0; i < test.Length; i++)
                {
                    if (!(test[i] == ""))
                    { listBox1.Items.Add(test[i]); }
                }
            }
        }
        public string Listitemvalue
        {
            get { return listitemvalue; }
            set { listitemvalue = value; } 
        }
        public List<string> Testnamelist
        {
            get { return testnamelist; }
            set { testnamelist = value; } 
        }
        public List<string> Comparelist
        {
            get { return comparelist; }
            set { comparelist = value; }
        }
        public List<string> Speclist
        {
            get { return speclist; }
            set { speclist = value; }
        }
        public List<string> Logiclist
        {
            get { return logiclist; }
            set { logiclist = value; }
        }
        string listtemp = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedItem == null) || (comboBox2.SelectedItem == null) || (comboBox3.SelectedItem == null))
            {
                MessageBox.Show("条件选择未完成");
            }
            else
            {
                string test = comboBox1.SelectedItem.ToString() + comboBox2.SelectedItem + comboBox3.SelectedItem;
                if ((listBox1.Items.Count % 2) == 0)
                {
                    int i = 0;
                    if (listBox1.Items.Count == 0)
                    {
                        listBox1.Items.Add(test);
                    }
                    else
                    {
                        string[] temp = new string[listBox1.Items.Count];
                        listBox1.Items.CopyTo(temp, 0);
                       
                        for (i = 0; i < temp.Length; i++)
                        {
                            
                            if (temp[i].Contains(test))
                            {
                                MessageBox.Show("条件已存在");
                                listtemp += temp[i];
                                continue;
                            }
                        }
                        if ((i >= temp.Length) && (!(listtemp.Contains(test))))
                        {
                            listBox1.Items.Add(test);

                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择逻辑关系");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
       
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("逻辑选择为空");
            }
            else
            {
                if ((listBox1.Items.Count % 2) != 0)
                {
                    listBox1.Items.Add(comboBox4.SelectedItem.ToString());
                }
                else
                {
                    MessageBox.Show("请选择条件");
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            listitemvalue = "";
            testnamelist=new List<string>();
            comparelist = new List<string>();
            speclist = new List<string>();
            logiclist = new List<string>();
            string[] test = new string[3];
            string[] test1 = new string[3];
            string[] temp = new string[listBox1.Items.Count];
            string[] temp1;
            if (mainform.displaylist.Length!=0)
            {
                temp1 = new string[mainform.mainformdisplaylist.Count];
                for (int i = 0; i < mainform.mainformdisplaylist.Count; i++)
                {
                    temp1[i] = mainform.mainformdisplaylist[i];
                }

            }
            else
            {

                temp1 = new string[mainform.dt.Columns.Count];
                for (int i = 0; i < mainform.dt.Columns.Count; i++)
                {
                    temp1[i] = mainform.dt.Columns[i].ToString(); ;
                }
            }
            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.CopyTo(temp, 0);
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                  
                        this.listitemvalue += temp[i] + ",";
                }
                listitemvalue = listitemvalue.Remove(listitemvalue.LastIndexOf(","), 1);
                for (int i = 0; i < listBox1.Items.Count; i = i + 2)
                {

                    if (temp[i].Contains(">"))
                    {
                        if (temp[i].Contains("="))
                        {
                            test = temp[i].Trim().Split('>');
                            testnamelist.Add(test[0]);
                            test1 = temp[i].Trim().Split('=');
                            speclist.Add(test1[1]);
                            comparelist.Add(">=");
                        }
                        else
                        {
                            test = temp[i].Trim().Split('>');
                            testnamelist.Add(test[0]);
                            speclist.Add(test[1]);
                            comparelist.Add(">");
                        }
                    }
                    else if (temp[i].Contains("<"))
                    {
                        if (temp[i].Contains("="))
                        {
                            test = temp[i].Trim().Split('<');
                            testnamelist.Add(test[0]);
                            test1 = temp[i].Trim().Split('=');
                            speclist.Add(test1[1]);
                            comparelist.Add("<=");
                        }
                        else
                        {
                            test = temp[i].Trim().Split('<');
                            testnamelist.Add(test[0]);
                            speclist.Add(test[1]);
                            comparelist.Add("<");
                        }
                    }
                    else if (temp[i].Contains("="))
                    {
                        test = temp[i].Trim().Split('=');
                        testnamelist.Add(test[0]);
                        speclist.Add(test[1]);
                        comparelist.Add("=");
                    }
                }
                for (int i = 1; i < listBox1.Items.Count; i = i + 2)
                {
                    logiclist.Add(temp[i]);
                }
                this.DialogResult = DialogResult.OK;
                mainform.mainformtestnamelist = this.testnamelist;
                mainform.mainformtestnamelist = this.Testnamelist;
                mainform.mainformcomparelist = this.Comparelist;
                mainform.mainformspeclist = this.Speclist;
                mainform.mainformlogiclist = this.Logiclist;
                mainform.advancedreport();
                mainform.displayreport(temp1, mainform.advanceddt);
           
            }
            else
            {
                mainform.displayreport(temp1, mainform.dt);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
             if (listBox1.SelectedItem != null)
            {
                if ((listBox1.SelectedItem.ToString() == "与") || (listBox1.SelectedItem.ToString() == "或"))
                {
                    MessageBox.Show("不能删除逻辑条件，请选择具体条件进行删除操作!");
                }
                else
                {
                    int i = listBox1.SelectedIndex;
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    if (listBox1.Items.Count != 0)
                    {
                        if (i == 0)
                        {
                            listBox1.Items.RemoveAt(i);
                        }
                        else
                        {
                            listBox1.Items.RemoveAt(i - 1);

                        }
                    }
                        
                      
                }
            }
             else
             {
                 MessageBox.Show("没有在右边选中要移除的项");
             }
        }

        private void Advanced_query_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
