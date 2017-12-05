using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Load_TestData
{
    public partial class display_query : Form
    {
        private string listitemvalue;
        private List<string> displaynamelist = new List<string>();
        public DataTable dt = new DataTable();
        private MainForm mainform;
        public display_query(string list,DataTable form1dt,MainForm form)
        {
            mainform = form;
            string[] test = list.Split(',');
            dt = form1dt;
            InitializeComponent();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (listBox1.Items.Contains(dt.Rows[i]["ItemName"].ToString()) == false)
                {
                    listBox1.Items.Add(dt.Rows[i]["ItemName"].ToString());
                }

            }
            if (list=="")
            {
                listBox2.Text = null;

            }
            else
            {
                if (test.Length != listBox1.Items.Count)
                {
                    for (int i = 0; i < test.Length; i++)
                    {
                        if (!(test[i] == ""))
                        {
                            listBox2.Items.Add(test[i]);
                            listBox1.Items.Remove(test[i]);
                        }
                    }
                }
                
            }
        }
        public List<string> Displaynamelist
        {
            get { return displaynamelist; }
            set { displaynamelist = value; }
        }
        public string Listitemvalue
        {
            get { return listitemvalue; }
            set { listitemvalue = value; }
        }
        string listtemp="";
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem!=null)
            {
                int i = 0;
                if (listBox2.Items.Count == 0)
                {
                    listBox2.Items.Add(listBox1.SelectedItem.ToString());
                    listBox1.Items.Remove(listBox1.SelectedItem.ToString());
                }
                else
                {
                    string[] temp = new string[listBox2.Items.Count];
                    listBox2.Items.CopyTo(temp, 0);
                    for (i = 0; i < temp.Length; i++)
                    {
                        if ((temp[i].Contains(listBox1.SelectedItem.ToString())))
                        {
                            MessageBox.Show("条件已存在");
                            listtemp += temp[i];
                            continue;
                        }

                    }
                    if ((i >= temp.Length) && (!(listtemp.Contains(listBox1.SelectedItem.ToString()))))
                    {
                        listBox2.Items.Add(listBox1.SelectedItem.ToString());
                        listBox1.Items.Remove(listBox1.SelectedItem.ToString());
                    }
                }
            }
            else
            {

                MessageBox.Show("请选择条件");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                listBox1.Items.Add(listBox2.SelectedItem.ToString());
                listBox2.Items.Remove(listBox2.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("没有在右边选中要移除的项");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listitemvalue = "";
            displaynamelist = new List<string>();
            if (listBox2.Items.Count == 0)
            {
                if (listBox1.Items.Count == 0)
                {
                    this.Close();

                }
                else
                {
                    string[] temp = new string[listBox1.Items.Count];
                    listBox1.Items.CopyTo(temp, 0);
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        listitemvalue += temp[i] + ",";
                        displaynamelist.Add(temp[i]);
                    }
                }
            }
            else
            {
                string[] temp = new string[listBox2.Items.Count];
                listBox2.Items.CopyTo(temp, 0);
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    listitemvalue += temp[i] + ",";
                    displaynamelist.Add(temp[i]);
                }

            }
            if (listitemvalue != "")
            {
                listitemvalue = listitemvalue.Remove(listitemvalue.LastIndexOf(","), 1);
            }
            this.DialogResult = DialogResult.OK;
            string[] temp1 = new string[displaynamelist.Count];
            for (int i = 0; i < displaynamelist.Count; i++)
            {
                temp1[i] = displaynamelist[i];
            }

            if (mainform.texttemp != "")
            {
                mainform.advancedreport();
                mainform.displayreport(temp1, mainform.advanceddt);
            }
            else
            {
                mainform.displayreport(temp1, mainform.dt);
            }
           
                

          
        }
        private void display_query_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
