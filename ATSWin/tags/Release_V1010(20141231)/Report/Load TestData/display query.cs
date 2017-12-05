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
        public display_query(string list,DataTable form1dt)
        {
            list = list.Replace(" ","");
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
                MessageBox.Show("筛选条件选择未完成，请继续!");
            }
            else
            {
                string[] temp = new string[listBox2.Items.Count];
                listBox2.Items.CopyTo(temp, 0);
                for (int i = 0; i < listBox2.Items.Count; i++)
                {

                    if (listitemvalue.Contains(temp[i]))
                    {
                        continue;
                    }
                    else
                    {
                        
                        listitemvalue += temp[i] + ",";
                    }
                    if (displaynamelist.Contains(temp[i]))
                    {
                        continue;
                    }
                    else
                    {
                        displaynamelist.Add(temp[i]);
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
        }
        private void display_query_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
